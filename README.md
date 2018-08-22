# ArquiteturaBackEndApiLivraria
Projeto criado como resolução da Atividade da Aula 03 da disciplina Arquitetura de Nuvem do curso de pós-graduação de Arquitetura de Softwares Distribuídos e, requisitado pelo professor Marco Mendes.

## Grupo
**_MATEUS SORIANO_** <br />
**_OTÁVIO AUGUSTO DE QUEIROZ REIS_**

* [Passo 0 - Crie, teste e depois publique o seu projeto no GitHub](Passo0.md)

Após feito o check-in do códigos e ajustes a primeira coisa feita foi baixar o programa Docker CE for Windows através do link <br/>
https://store.docker.com/editions/community/docker-ce-desktop-windows

No caso optamos por utilizar os containers Linux ao invés de containers Windows, pois utilizando o containers Linux te possibilita o uso do kubernetes.

Como tudo do Windows a instalação basta clicar nos botões de avançar e pronto, irá instalar o aplicativo sem dificultades.
![alt text](https://i.snag.gy/c0tCAx.jpg)
![alt text](https://i.snag.gy/7wSkjD.jpg)


Após atestado o sucesso na instalação, iniciamos os procedimentos para realizar a conteinerização do nosso aplicativo dotnet core dando início a resolução da atividade proposta.

**Passo 0: Crie, teste e depois publique o seu projeto no GitHub.** <br/>
**Entregável: Código Java e C# no GitHub**

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
