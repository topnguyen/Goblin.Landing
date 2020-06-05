/* eslint-disable */
const autoprefixer = require('gulp-autoprefixer'),
    browserSync = require('browser-sync'),
    cleanCSS = require('gulp-clean-css'),
    cssImporter = require('node-sass-css-importer')({
        import_paths: ['./scss']
    }),
    del = require('del'),
    eslint = require('gulp-eslint'),
    gulp = require('gulp'),
    log = require('fancy-log'),
    newer = require('gulp-newer'),
    path = require('path'),
    reload = browserSync.reload,
    rename = require('gulp-rename'),
    rollup = require('rollup'),
    rollupBabel = require('rollup-plugin-babel'),
    rollupCommonjs = require('rollup-plugin-commonjs'),
    rollupResolve = require('rollup-plugin-node-resolve'),
    rollupUglify = require('rollup-plugin-uglify').uglify,
    sass = require('gulp-sass'),
    sourcemaps = require('gulp-sourcemaps'),
    year = new Date().getFullYear(),
    themeYaml = './theme.yml',
    yaml = require('yamljs');

let theme = yaml.load(themeYaml);

const babelConfig = {
    presets: [
        [
            '@babel/env',
            {
                loose: true,
                modules: false,
                exclude: ['transform-typeof-symbol']
            }
        ]
    ],
    plugins: [
        '@babel/plugin-proposal-object-rest-spread'
    ],
    env: {
        test: {
            plugins: ['istanbul']
        }
    },
    exclude: 'node_modules/**', // Only transpile our source code
    externalHelpersWhitelist: [ // Include only required helpers
        'defineProperties',
        'createClass',
        'inheritsLoose',
        'defineProperty',
        'objectSpread2'
    ],
};

getPaths = () => {
    return {
        here: './',
        js: {
            all: "js/**/*",
            bootstrap: {
                all: [
                    "./js/bootstrap/util.js",
                    "./js/bootstrap/alert.js",
                    "./js/bootstrap/button.js",
                    "./js/bootstrap/carousel.js",
                    "./js/bootstrap/collapse.js",
                    "./js/bootstrap/dropdown.js",
                    "./js/bootstrap/modal.js",
                    "./js/bootstrap/tooltip.js",
                    "./js/bootstrap/popover.js",
                    "./js/bootstrap/scrollspy.js",
                    "./js/bootstrap/toast.js",
                    "./js/bootstrap/tab.js"
                ],
                index: "./js/bootstrap/index.js"
            },
            goblin: {
                all: "js/goblin/**/*.js",
                index: "js/goblin/index.js",
            }
        },
        scss: {
            folder: 'scss',
            all: 'scss/**/*',
            root: 'scss/*.scss',
            themeScss: ['scss/goblin.scss', '!scss/user.scss', '!scss/user-variables.scss'],
        },
        dist: {
            packageFolder: '',
            css: '../wwwroot/css',
            js: '../wwwroot/js',
            exclude: ['!**/desktop.ini', '!**/.DS_store'],
        }
    }
};

let paths = getPaths();

// DEFINE TASKS

// Build Scss
gulp.task('Build Scss', function () {
    return gulp.src(paths.scss.themeScss)
        .pipe(sourcemaps.init())
        .pipe(sass({
            importer: [cssImporter]
        }).on('error', sass.logError))
        .pipe(autoprefixer())
        .pipe(sourcemaps.write('.'))
        .pipe(gulp.dest(paths.dist.css))
        .pipe(browserSync.stream({
            match: "**/goblin*.css"
        }));
});

// Minify Scss
gulp.task('Minify Scss', function () {
    return gulp.src(paths.scss.themeScss)
        .pipe(sourcemaps.init())
        .pipe(sass({
            importer: [cssImporter]
        }).on('error', sass.logError))
        .pipe(cleanCSS({
            compatibility: 'ie9'
        }))
        .pipe(autoprefixer())
        .pipe(rename({
            suffix: '.min'
        }))
        .pipe(sourcemaps.write('.'))
        .pipe(gulp.dest(paths.dist.css))
        .pipe(browserSync.stream({
            match: "**/goblin*.css"
        }));
});

// Build Bootstrap and jQuery Bootstrap
gulp.task('Build Bootstrap and jQuery Bootstrap', async (done) => {
    let fileDest = 'bootstrap.js';
    const banner = `/*!
  * Bootstrap v${theme.bootstrap_version}
  * Copyright 2011-${year} The Bootstrap Authors (https://github.com/twbs/bootstrap/graphs/contributors)
  * Licensed under MIT (https://github.com/twbs/bootstrap/blob/master/LICENSE)
  */`;
    const external = ['jquery', 'popper.js'];
    const plugins = [
        rollupBabel(babelConfig),
        rollupUglify({
            output: {
                comments: "/^!/"
            }
        }),
    ];
    const globals = {
        jquery: 'jQuery', // Ensure we use jQuery which is always available even in noConflict mode
        'popper.js': 'Popper'
    };

    const bundle = await rollup.rollup({
        input: paths.js.bootstrap.index,
        external,
        plugins
    });

    await bundle.write({
        file: path.resolve(__dirname, `./${paths.dist.js}${path.sep}${fileDest}`),
        banner,
        globals,
        format: 'umd',
        name: 'bootstrap',
        sourcemap: true,
    });
    // Reload Browsersync clients
    reload();
    done();
});

// Build goblin js
gulp.task('Build Goblin JS', async (done) => {
    gulp.src(paths.js.goblin.all)
        .pipe(eslint())
        .pipe(eslint.format());

    let fileDest = 'goblin.js';
    const banner = `/*!
  * ${theme.name}
  * Copyright 2018-${year} Goblin
  */`;
    const external = [...theme.scripts.external];
    const plugins = [
        rollupCommonjs(),
        rollupResolve({
            browser: true,
        }),
        rollupBabel(babelConfig),
        theme.minify_scripts === true ? rollupUglify({
            output: {
                comments: "/^!/"
            }
        }) : null,
    ];
    const globals = theme.scripts.globals;

    const bundle = await rollup.rollup({
        input: paths.js.goblin.index,
        external,
        plugins,
        onwarn: function (warning) {
            // Skip certain warnings
            if (warning.code === 'THIS_IS_UNDEFINED') {
                return;
            }
            // console.warn everything else
            console.warn(warning.message);
        }
    });

    await bundle.write({
        file: path.resolve(__dirname, `./${paths.dist.js}${path.sep}${fileDest}`),
        banner,
        globals,
        format: 'umd',
        name: 'goblin',
        sourcemap: true,
        sourcemapFile: path.resolve(__dirname, `./${paths.dist.js}${path.sep}${fileDest}.map`),
    });
    // Reload Browsersync clients
    reload();
    done();
});

// BUILD ALL
gulp.task('build', gulp.series(gulp.series('Build Scss', 'Minify Scss', 'Build Bootstrap and jQuery Bootstrap', 'Build Goblin JS')));
