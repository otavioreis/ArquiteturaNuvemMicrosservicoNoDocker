## Passo 4 - Operar a aplicação como um serviço Docker Swarm

Para operarmos a nossa aplicação como um serviço do Docker Swarm, abrimos o powershell e executamos a sequencia de comandos a seguir:
```bash
$ docker swarm init
$ token=$(docker -H 172.17.0.59:2345 swarm join-token -q worker) && echo $token
$ docker swarm join 172.17.0.59:2377 --token $token
$ docker node ls
```

Com isso conseguimos ver que existiam dois hosts ativos, sendo um deles sendo o lider, veja:
```text
ID                            HOSTNAME            STATUS              AVAILABILITY        MANAGER STATUS      ENGINE VERSION
kg6rhiog859qygznk8h7hdask *   host01              Ready               Active              Leader              18.03.0-ce
8kmbrlqt17k8aje36e4uy27k6     host02              Ready               Active                                  18.03.0-ce
```

Então criamos uma rede de sobreposição com o comando
```bash
$ docker network create -d overlay skynet
```

Então foi o momento de rodar a nossa aplicação, sendo executado o comando:
```bash
$ docker service create --name http --network skynet --replicas 2 -p 80:5000 otavioreis/apilivrarialinux
```

Verificamos se as replicas foram criadas com sucesso através do comando
```bash
docker service ls
```

Retornando o seguinte resultado
```text
ID                  NAME                MODE                REPLICAS            IMAGE                                PORTS
lc8rf43oy99s        http                replicated          2/2                 otavioreis/apilivrarialinux:latest   *:80->5000/tcp
```

Também listamos os containers nos dois hosts através do comando
```bash
$ docker ps
```

Então verificamos se ambos estavam rodando, veja os prints em ambos os hosts:
![alt text](https://i.snag.gy/8E9Rzd.jpg)
![alt text](https://i.snag.gy/G1owJq.jpg)

Além disso executamos alguns comandos adicionais para verificar algumas coisas, como:

Listar todas as tarefas associadas ao HTTP:
```bash
$ docker service ps http
```

Retorno:
```text
ID                  NAME                IMAGE                                NODE                DESIRED STATE       CURRENT STATE           ERROR         PORTS
z4nrperxkm8z        http.1              otavioreis/apilivrarialinux:latest   host01              Running             Running 5 minutes ago
0e4j6f5kxe9c        http.2              otavioreis/apilivrarialinux:latest   host02              Running             Running 5 minutes ago
```

Também acessamos os detalhes da configuração:
```bash
docker service inspect --pretty http
```

```text
ID:             lc8rf43oy99s3pyg2cj4d6jrl
Name:           http
Service Mode:   Replicated
 Replicas:      2
Placement:
UpdateConfig:
 Parallelism:   1
 On failure:    pause
 Monitoring Period: 5s
 Max failure ratio: 0
 Update order:      stop-first
RollbackConfig:
 Parallelism:   1
 On failure:    pause
 Monitoring Period: 5s
 Max failure ratio: 0
 Rollback order:    stop-first
ContainerSpec:
 Image:         otavioreis/apilivrarialinux:latest@sha256:30a2e334993e09d5317dba178599f84d4b032fe4ff5263aeb58cd37b09e23b44
Resources:
Networks: skynet
Endpoint Mode:  vip
Ports:
 PublishedPort = 80
  Protocol = tcp
  TargetPort = 5000
  PublishMode = ingress
$ ^C
$ docker service inspect --pretty http

ID:             lc8rf43oy99s3pyg2cj4d6jrl
Name:           http
Service Mode:   Replicated
 Replicas:      2
Placement:
UpdateConfig:
 Parallelism:   1
 On failure:    pause
 Monitoring Period: 5s
 Max failure ratio: 0
 Update order:      stop-first
RollbackConfig:
 Parallelism:   1
 On failure:    pause
 Monitoring Period: 5s
 Max failure ratio: 0
 Rollback order:    stop-first
ContainerSpec:
 Image:         otavioreis/apilivrarialinux:latest@sha256:30a2e334993e09d5317dba178599f84d4b032fe4ff5263aeb58cd37b09e23b44
Resources:
Networks: skynet
Endpoint Mode:  vip
Ports:
 PublishedPort = 80
  Protocol = tcp
  TargetPort = 5000
  PublishMode = ingress
```

Também verificamos em qual node o serviço estava rodando com o comando:
```bash
$ docker node ps self
```
Resposta:
```text
ID                  NAME                IMAGE                                NODE                DESIRED STATE       CURRENT STATE            ERROR          PORTS
z4nrperxkm8z        http.1              otavioreis/apilivrarialinux:latest   host01              Running             Running 10 minutes ago
```

Para finalizar escalamos o nosso serviço rodando em 5 containers distintos através do comando:
```bash
$ docker service scale http=5
```

Retorno
```text
http scaled to 5
overall progress: 5 out of 5 tasks
1/5: running   [==================================================>]
2/5: running   [==================================================>]
3/5: running   [==================================================>]
4/5: running   [==================================================>]
5/5: running   [==================================================>]
verify: Service converged
```

E então escalamos para baixo:
```bash
$ docker service scale http=2
```
Resposta:
```text
http scaled to 2
overall progress: 2 out of 2 tasks
1/2: running   [==================================================>]
2/2: running   [==================================================>]
verify: Service converged
```

Com isso finalizamos o nosso exercício do swarm.

* [Voltar](https://github.com/otavioreis/ArquiteturaNuvemMicrosservicoNoDocker)

