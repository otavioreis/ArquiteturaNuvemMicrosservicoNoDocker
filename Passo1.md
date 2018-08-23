**Passo 1 - Conteinerizar a aplicação com o Docker**

Abaixo os comandos que foram executados
```powershell
PS > mkdir Docker
PS .\Docker\> cd .\Docker\
PS .\Docker\> git clone https://github.com/otavioreis/ArquiteturaBackEndApiLivraria
PS .\Docker\> cd .\ArquiteturaBackEndApiLivraria\ApiMicrosservicos\
PS .\Docker\ArquiteturaBackEndApiLivraria\ApiMicrosservicos\> bash -c "vi Dockerfile"
```

Após digitar o comando *bash -c "vi Dockerfile"* foi criado o documento DockerFile que já se encontra neste projeto. Através do link: <br/>
https://github.com/otavioreis/ArquiteturaNuvemMicrosservicoNoDocker/blob/master/ApiMicrosservicos/Dockerfile

Também disponibilizado abaixo:
```Dockerfile
FROM microsoft/aspnetcore-build:2.0 AS build-env
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# copy and build everything else
COPY . ./
RUN dotnet publish -c Release -o out

# Optimize size
FROM microsoft/aspnetcore:2.0
WORKDIR /app
EXPOSE 5000/tcp
COPY --from=build-env /app/out ./
ENTRYPOINT ["dotnet", "Livraria.Api.dll"]
```

Após criado o Dockerfile executamos o seguinte comando para criar o arquivo de .dockerignore
```powershell
PS .\Docker\ArquiteturaBackEndApiLivraria\ApiMicrosservicos\> bash -c "vi .dockerignore"
```

O arquivo pode ser acessado através do link: <br />
https://github.com/otavioreis/ArquiteturaNuvemMicrosservicoNoDocker/blob/master/ApiMicrosservicos/.dockerignore

```text
bin\
obj\
```

Após criados os arquivos de configuração do Docker, os próximo passo foi de criar a imagem, rodando o comando abaixo:

```powershell
PS .\Docker\ArquiteturaBackEndApiLivraria\ApiMicrosservicos\> docker build -t apilivraria .
```
Para verificar se a imagem havia sido criada com sucesso o comando foi executado:
```powershell
PS .\Docker\ArquiteturaBackEndApiLivraria\ApiMicrosservicos\> docker images
```
O valor retornado foi:
```text
REPOSITORY                   TAG                 IMAGE ID            CREATED             SIZE
apilivraria                  latest              0e2892824f0c        2 seconds ago       354MB
<none>                       <none>              61d6c9f53a0d        14 seconds ago      2.06GB
microsoft/aspnetcore-build   2.0                 06a6525397c2        7 days ago          2.02GB
microsoft/aspnetcore         2.0                 db030c19e94b        7 days ago          347MB
```

Tendo a imagem construida e otimizada, bastou executar a imagem para a criação do container:
```powershell
PS .\Docker\ArquiteturaBackEndApiLivraria\ApiMicrosservicos\> docker run -d -p 80:5000 --name livrariaserver apilivraria
```
Assim obtivemos sucesso e concluímos o primeiro exercício, conforme imagem de evidência:
![alt text](https://i.snag.gy/nNHStJ.jpg)

Apenas a titulo de curiosidade eu consegui rodar o projeto utilizando o ambiente de playground do katacoda: <br/>
https://www.katacoda.com/ 

Veja a evidência (repare a URL no navegador rodando diretamente no ambiente do katacoda):
![alt text](https://i.snag.gy/kBfvab.jpg)



* [Voltar](https://github.com/otavioreis/ArquiteturaNuvemMicrosservicoNoDocker)
