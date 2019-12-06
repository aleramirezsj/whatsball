### USAR DOCKER PARA BUILDEAR UNITY

***fuente: https://medium.com/@carlos_c/how-to-build-and-release-a-standalone-unity-app-using-travis-ci-docker-and-github-7798207bb865***

* #### Comando para crear un contenedor con la imagen de unity 2018.2.11f1(generando además que se descargue si no la tenemos)

  ```
  docker run --rm --name  miUnity gableroux/unity3d:2018.2.11f1-android
  ```

  

* #### Comando para ingresar al contenedor Unity haciendo bind mount a la carpeta de nuestro proyecto en el equipo host

  ```
  docker run -it --rm --env UNITY_USERNAME=aleramirezsj@gmail.com --env UNITY_PASSWORD=miContraseña --env  TEST_PLATFORM=Android --env WORKDIR=/root/project -v C:\Users\Ramirez\Documents\WhatsBallDefinitivo\WhatsBall:/root/project gableroux/unity3d:2018.2.11f1-android bash
  ```

  

* #### Comando para generar el archivo de licencia

  ```bash
  xvfb-run --auto-servernum --server-args='-screen 0 640x480x24' /opt/Unity/Editor/Unity -logFile /dev/stdout -batchmode -username
  "aleramirezsj@gmail.com" -password "miContraseña" 
  ```

  

  * Archivo de licencia obtenido(solo sirve para mi equipo)

  ```xml
  <?xml version="1.0" encoding="UTF-8"?><root><SystemInfo><IsoCode>en</IsoCode><UserName>(unset)</UserName><OperatingSystem>Linux 4.9 Ubuntu 16.04 64bit</OperatingSystem><OperatingSystemNumeric>409</OperatingSystemNumeric><ProcessorType>Intel(R) Core(TM) i7-4712HQ CPU @ 2.30GHz</ProcessorType><ProcessorSpeed>2075</ProcessorSpeed><ProcessorCount>2</ProcessorCount><ProcessorCores>2</ProcessorCores><PhysicalMemoryMB>1999</PhysicalMemoryMB><ComputerName>219db8d58170</ComputerName><ComputerModel>PC</ComputerModel><UnityVersion>2018.2.11f1</UnityVersion><SupportedLicenseVersion>6.x</SupportedLicenseVersion></SystemInfo><License id="Terms"><MachineID Value="fYK9VIRsgXWld8w1UwkFp3uAhYM=" /><MachineBindings><Binding Key="1" Value="db44533c56354d859b5ad7dd95d6eb33" /><Binding Key="2" Value="db44533c56354d859b5ad7dd95d6eb33" /></MachineBindings><UnityVersion Value="2018.2.11f1" /></License></root>
  ```

  * Guardarlo como `unity3d.alf`

  * Abrimos **https://license.unity3d.com/manual** y contestamos algunas preguntas 

  * Subimos el archivo `unity3d.alf` para que nos genere el archivo x.ulf (respondemos ***Unity Personal Edition*** y que no ganamos más de 100.000 dólares al año)

  * Descargamos el archivo `Unity_v2018.x.ulf`

  * Ciframos el archivo con el CLI de Travis

    * Necesitamos instalar **Ruby (http://www.ruby-lang.org/en/downloads/)**

    * Instalamos el **cliente de travis** con el comando

      ```bash
      $ gem install travis -v 1.8.10 
      ```

    * nos logueamos a github con el comando

      ```bash
      travis login --pro
      ```

    * comando para cifrar el archivo con el cliente de travis

      ```bash
      travis encrypt-file --com .\Unity_v2018.x.ulf
      (esto genera el archivo `Unity_v2018.x.ulf.enc`)
      ```

    * dentro de https://travis-ci.com/aleramirezsj/whatsball/settings ya podremos ver 2 nuevas variables de entorno

  * Copiamos la carpeta ci del repositorio (https://github.com/GabLeRoux/unity3d-ci-example/tree/master/ci) a la raiz de nuestro proyecto

  *  ***generamos un token de acceso a nuestro github*** nos vamos a https://github.com/settings/tokens 

    * Le colocamos un nombre al token, puede ser **acceso_travis**
    * tildamos el permiso de acceso a [*] repo y las 4 casillas de verificación que le siguen []

  * Nos vamos a **settings** del repo en travis y creamos una nueva variable de entorno, le ponemos de nombre **GITHUB_API_KEY** y le pegamos el valor del **Token**

  * Copiamos la carpeta **Editor** que se encuentra en **Assets/Scripts** 

  * Copiamos el archivo **.travis.yml**

    * ajustamos el nombre de la **IMAGE_NAME** y **BUILD_NAME** 
    * reemplazamos los argumentos **encrypted_xxxxxx** con las variables de entorno de su Travis que puede obtener en configuración.