## Passo 3 - Operar a aplicação publicada no Azure como um conteiner Docker

Novamente quisemos ir um passo além e fizemos a publicação da nossa aplicação utilizando o próprio Azure. 

Para realizar esta tarefa, primeiramente efetuamos o push da nossa imagem gerada no [Passo 1](https://github.com/otavioreis/ArquiteturaNuvemMicrosservicoNoDocker/blob/master/Passo1.md) então fizemos uma preparação para enviar esta para o http://hub.docker.com

Os comandos executados foram:
```bash
 $ docker login
 $ docker tag apilivraria otavioreis/apilivrarialinux
 $ docker push otavioreis/apilivrarialinux
```

Após realizado o procedimento acima, pudemos conferir que o procedimento foi bem sucedido, conforme imagem abaixo, ou se preferir, através do link (https://hub.docker.com/r/otavioreis/apilivrarialinux/):
![alt text](https://i.snag.gy/5CQstB.jpg)

Com a imagem armazenada no Docker Hub, então pudemos partir para o ambiente do Azure.

Para tanto, inicialmente criei um Serviço de Aplicativo situado nos EUA East que rodasse Linux, conforme imagem:
![alt text](https://i.snag.gy/sBDW7Q.jpg)

Além do plano de serviço também criei um grupo de recursos com o mesmo nome, veja:
![alt text](https://i.snag.gy/mpvndX.jpg)

Tendo a estrutura criada no azure, abrimos o bash de comandos do Azure e iniciamos os procedimentos para operar a nossa aplicação em um container no Azure. Os comandos executados foram:

```bash
otavioreis@Azure:~$ az container create -g OtavioPagoLinux --name apilivraria2 --port 5000 --image otavioreis/apilivrarialinux --ip-address public
```

Após executar este comando o container foi criado com sucesso e tive o seguinte retorno JSON, inclusive expondo a porta externa:
```json
{
  "containers": [
    {
      "command": null,
      "environmentVariables": [],
      "image": "otavioreis/apilivrarialinux",
      "instanceView": {
        "currentState": {
          "detailStatus": "",
          "exitCode": null,
          "finishTime": null,
          "startTime": "2018-08-24T00:59:30+00:00",
          "state": "Running"
        },
        "events": [
          {
            "count": 1,
            "firstTimestamp": "2018-08-24T00:58:59+00:00",
            "lastTimestamp": "2018-08-24T00:58:59+00:00",
            "message": "pulling image \"otavioreis/apilivrarialinux\"",
            "name": "Pulling",
            "type": "Normal"
          },
          {
            "count": 1,
            "firstTimestamp": "2018-08-24T00:59:27+00:00",
            "lastTimestamp": "2018-08-24T00:59:27+00:00",
            "message": "Successfully pulled image \"otavioreis/apilivrarialinux\"",
            "name": "Pulled",
            "type": "Normal"
          },
          {
            "count": 1,
            "firstTimestamp": "2018-08-24T00:59:30+00:00",
            "lastTimestamp": "2018-08-24T00:59:30+00:00",
            "message": "Created container",
            "name": "Created",
            "type": "Normal"
          },
          {
            "count": 1,
            "firstTimestamp": "2018-08-24T00:59:30+00:00",
            "lastTimestamp": "2018-08-24T00:59:30+00:00",
            "message": "Started container",
            "name": "Started",
            "type": "Normal"
          }
        ],
        "previousState": null,
        "restartCount": 0
      },
      "livenessProbe": null,
      "name": "apilivraria2",
      "ports": [
        {
          "port": 5000,
          "protocol": "TCP"
        }
      ],
      "readinessProbe": null,
      "resources": {
        "limits": null,
        "requests": {
          "cpu": 1.0,
          "memoryInGb": 1.5
        }
      },
      "volumeMounts": null
    }
  ],
  "diagnostics": null,
  "id": "/subscriptions/066c5e15-7d48-4673-aaca-e0ff1aed8cd4/resourceGroups/OtavioPagoLinux/providers/Microsoft.ContainerInstance/containerGroups/apilivraria2",
  "imageRegistryCredentials": null,
  "instanceView": {
    "events": [],
    "state": "Running"
  },
  "ipAddress": {
    "dnsNameLabel": null,
    "fqdn": null,
    "ip": "104.211.57.120",
    "ports": [
      {
        "port": 5000,
        "protocol": "TCP"
      }
    ]
  },
  "location": "eastus",
  "name": "apilivraria2",
  "osType": "Linux",
  "provisioningState": "Succeeded",
  "resourceGroup": "OtavioPagoLinux",
  "restartPolicy": "Always",
  "tags": {},
  "type": "Microsoft.ContainerInstance/containerGroups",
  "volumes": null
}
```
O IP Público exposto para acessar o container foi http://104.211.57.120:5000, veja a imagem:
![alt text](https://snag.gy/84XZaw.jpg)

Indo um pouco além, também quis efetuar um teste da criação de um WebApp para containers, executando o comando a seguir:
```bash
otavioreis@Azure:~$ az webapp create --resource-group OtavioPagoLinux --plan OtavioPagoLinux --name ApiLivrariaDocker --deployment-container-image-name otavioreis/apilivrarialinux
```

Obtive sucesso e o azure me retornou o seguinte json:
```json
{
  "availabilityState": "Normal",
  "clientAffinityEnabled": true,
  "clientCertEnabled": false,
  "cloningInfo": null,
  "containerSize": 0,
  "dailyMemoryTimeQuota": 0,
  "defaultHostName": "apilivrariadocker.azurewebsites.net",
  "enabled": true,
  "enabledHostNames": [
    "apilivrariadocker.azurewebsites.net",
    "apilivrariadocker.scm.azurewebsites.net"
  ],
  "ftpPublishingUrl": "ftp://waws-prod-blu-103.ftp.azurewebsites.windows.net/site/wwwroot",
  "hostNameSslStates": [
    {
      "hostType": "Standard",
      "ipBasedSslResult": null,
      "ipBasedSslState": "NotConfigured",
      "name": "apilivrariadocker.azurewebsites.net",
      "sslState": "Disabled",
      "thumbprint": null,
      "toUpdate": null,
      "toUpdateIpBasedSsl": null,
      "virtualIp": null
    },
    {
      "hostType": "Repository",
      "ipBasedSslResult": null,
      "ipBasedSslState": "NotConfigured",
      "name": "apilivrariadocker.scm.azurewebsites.net",
      "sslState": "Disabled",
      "thumbprint": null,
      "toUpdate": null,
      "toUpdateIpBasedSsl": null,
      "virtualIp": null
    }
  ],
  "hostNames": [
    "apilivrariadocker.azurewebsites.net"
  ],
  "hostNamesDisabled": false,
  "hostingEnvironmentProfile": null,
  "httpsOnly": false,
  "id": "/subscriptions/066c5e15-7d48-4673-aaca-e0ff1aed8cd4/resourceGroups/OtavioPagoLinux/providers/Microsoft.Web/sites/ApiLivrariaDocker",
  "identity": null,
  "isDefaultContainer": null,
  "kind": "app,linux,container",
  "lastModifiedTimeUtc": "2018-08-24T01:05:46.736666",
  "location": "East US",
  "maxNumberOfWorkers": null,
  "name": "ApiLivrariaDocker",
  "outboundIpAddresses": "40.121.32.232,40.114.29.199,104.211.16.51,40.121.36.86,40.121.38.194",
  "possibleOutboundIpAddresses": "40.121.32.232,40.114.29.199,104.211.16.51,40.121.36.86,40.121.38.194,40.121.35.60,40.121.44.194,40.121.36.231",
  "repositorySiteName": "ApiLivrariaDocker",
  "reserved": true,
  "resourceGroup": "OtavioPagoLinux",
  "scmSiteAlsoStopped": false,
  "serverFarmId": "/subscriptions/066c5e15-7d48-4673-aaca-e0ff1aed8cd4/resourceGroups/OtavioPagoLinux/providers/Microsoft.Web/serverfarms/OtavioPagoLinux",
  "siteConfig": null,
  "slotSwapStatus": null,
  "snapshotInfo": null,
  "state": "Running",
  "suspendedTill": null,
  "tags": null,
  "targetSwapSlot": null,
  "trafficManagerHostNames": null,
  "type": "Microsoft.Web/sites",
  "usageState": "Normal"
}
```

Porém para o WebApp foi necessário fazer uma configuração adicional para que a aplicação apontasse para a porta correta do container, o comando foi:
```bash
otavioreis@Azure:~$ az webapp config appsettings set --resource-group OtavioPagoLinux --name ApiLivrariaDocker --settings WEBSITES_PORT=5000
```

Com o retorno json:
```json
[
  {
    "name": "WEBSITES_ENABLE_APP_SERVICE_STORAGE",
    "slotSetting": false,
    "value": "false"
  },
  {
    "name": "WEBSITES_PORT",
    "slotSetting": false,
    "value": "5000"
  }
]
```

Após feito isso o site funcionou, através do host exposto http://apilivrariadocker.azurewebsites.net, conforme imagem:
![alt text](https://i.snag.gy/TP0rZS.jpg)

Finalizando executei o comando bash para exibir os containers criados:
```bash
otavioreis@Azure:~$ az container list -o table
```
E o azure me listou os recursos criados, conforme explicado anteriormente.
```text
Name          ResourceGroup    ProvisioningState    Image                        IP:ports             CPU/Memory       OsType    Location
------------  ---------------  -------------------  ---------------------------  -------------------  ---------------  --------  ----------
apilivraria   OtavioPagoLinux  Succeeded            otavioreis/apilivrarialinux  40.87.81.116:80      1.0 core/1.5 gb  Linux     eastus
apilivraria2  OtavioPagoLinux  Succeeded            otavioreis/apilivrarialinux  104.211.57.120:5000  1.0 core/1.5 gb  Linux     eastus
```

Também executei anteriormente um comandos para apagar os containers criados como teste no Azure, mas não para os containers deste exemplo, pois deixei eles rodando como case.
```bash
otavioreis@Azure:~$ az container delete --resource-group containerteste --name nginx
```

* [Voltar](https://github.com/otavioreis/ArquiteturaNuvemMicrosservicoNoDocker)
