# Docker Build and Run

docker build --tag goblin-landing:1.0 .

docker run --network bridge --publish 7001:80 --env-file DockerEnv --detach --name goblin-landing goblin-landing:1.0

---

# Docker Remove

docker rm --force goblin-Api_Base

---

# Network

docker network ls

docker network create -d bridge goblin

docker network inspect goblin

docker network rm goblin