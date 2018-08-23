## Passo 2 - Operar a aplicação com balanceamento de carga em 3 instâncias distintas

Para realizar o procedimento de balanceamento de carga em 3 instâncias distintas utilizamos o servidor nginx-proxy

```powershell
PS > docker run -d -p 80:80 -e DEFAULT_HOST=proxy.apilivraria -v /var/run/docker.sock:/tmp/docker.sock:ro --name nginx jwilder/nginx-proxy
```

Após rodar o container do nginx, executamos o comando de curl apenas para verificar se realmente estava funcionando conforme o esperado:
```powershell
PS > curl http://localhost
```

O retorno foi:
```text
curl : <html>
<head><title>503 Service Temporarily Unavailable</title></head>
<body bgcolor="white">
<center><h1>503 Service Temporarily Unavailable</h1></center>
<hr><center>nginx/1.14.0</center>
</body>
</html>
No linha:1 caractere:1
+ curl http://localhost
+ ~~~~~~~~~~~~~~~~~~~~~
    + CategoryInfo          : InvalidOperation: (System.Net.HttpWebRequest:HttpWebRequest) [Invoke-WebRequest], WebException
    + FullyQualifiedErrorId : WebCmdletWebResponseException,Microsoft.PowerShell.Commands.InvokeWebRequestCommand
```

Como é possível verificar NGINX respondeu com o erro 503, indicando que está ativo.

Para este cenário eu precisei criar um novo método publico na minha API que não exigisse uma autenticação, uma vez que os demais métodos exigiam, para mostrar qual o ID do container que havia executado a requisição.
![alt text](https://i.snag.gy/gMJ9OR.jpg)

Após criado o método repeti todo o procedimento para criar a imagem e então iniciei os containers apontando para o proxy criado.

```powershell
PS > docker run -d -p 5000 -e VIRTUAL_HOST=proxy.apilivraria --name teste apilivraria
PS > docker run -d -p 5000 -e VIRTUAL_HOST=proxy.apilivraria --name teste apilivraria
PS > docker run -d -p 5000 -e VIRTUAL_HOST=proxy.apilivraria --name teste apilivraria
```

Feito isso eu consegui acessar o endereço http://localhost/v1/public/Container e verifiquei que a cada requisição um container era o responsável pela resposta efetuando o (round-robin), conforme imagens abaixo:
![alt text](https://i.snag.gy/CiYqTz.jpg)
![alt text](https://i.snag.gy/yob0V5.jpg)
![alt text](https://i.snag.gy/0JhwEc.jpg)


* [Voltar](https://github.com/otavioreis/ArquiteturaNuvemMicrosservicoNoDocker)
