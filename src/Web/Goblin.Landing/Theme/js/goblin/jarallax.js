//
// jarallax.js
//

// Closing an alert changes height of document, so readjust position of parallax elements

import jQuery from 'jquery';
import jarallax from 'jarallax';
import goblinUtil from './util';

(($) => {
  if (typeof jarallax === 'function') {
    $('.alert-dismissible').on('closed.bs.alert', () => {
      jarallax(document.querySelectorAll('[data-jarallax],[data-jarallax-video]'), 'onScroll');
    });

    $(document).on('resized.goblin.overlayNav', () => {
      jarallax(document.querySelectorAll('[data-jarallax],[data-jarallax-video]'), 'onResize');
    });

    document.addEventListener('injected.goblin.SVGInjector', () => {
      jarallax(document.querySelectorAll('[data-jarallax],[data-jarallax-video]'), 'onResize');
    });

    const jarallaxOptions = {
      disableParallax: /iPad|iPhone|iPod|Android/,
      disableVideo: /iPad|iPhone|iPod|Android/,
    };

    $(window).on('load', () => {
      jarallax(document.querySelectorAll('[data-jarallax]'), jarallaxOptions);

      const jarallaxDelay = document.querySelectorAll('[data-jarallax-video-delay]');
      goblinUtil.forEach(jarallaxDelay, (index, elem) => {
        const source = elem.getAttribute('data-jarallax-video-delay');
        elem.removeAttribute('data-jarallax-video-delay');
        elem.setAttribute('data-jarallax-video', source);
      });

      jarallax(document.querySelectorAll('[data-jarallax-delay],[data-jarallax-video]'), jarallaxOptions);
    });
  }
})(jQuery);
