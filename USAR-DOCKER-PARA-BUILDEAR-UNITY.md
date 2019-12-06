### USAR DOCKER PARA BUILDEAR UNITY

* #### Comando para crear un contenedor con la imagen de unity 2018.2.11f1(generando además que se descargue si no la tenemos)

  ```
  docker run -rm --name  miUnity gableroux/unity3d:2018.2.11f1-android
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

  

* ####Archivo de licencia obtenido(solo sirve para mi equipo)

  ```xml
  <?xml version="1.0" encoding="UTF-8"?><root><SystemInfo><IsoCode>en</IsoCode><UserName>(unset)</UserName><OperatingSystem>Linux 4.9 Ubuntu 16.04 64bit</OperatingSystem><OperatingSystemNumeric>409</OperatingSystemNumeric><ProcessorType>Intel(R) Core(TM) i7-4712HQ CPU @ 2.30GHz</ProcessorType><ProcessorSpeed>2075</ProcessorSpeed><ProcessorCount>2</ProcessorCount><ProcessorCores>2</ProcessorCores><PhysicalMemoryMB>1999</PhysicalMemoryMB><ComputerName>219db8d58170</ComputerName><ComputerModel>PC</ComputerModel><UnityVersion>2018.2.11f1</UnityVersion><SupportedLicenseVersion>6.x</SupportedLicenseVersion></SystemInfo><License id="Terms"><MachineID Value="fYK9VIRsgXWld8w1UwkFp3uAhYM=" /><MachineBindings><Binding Key="1" Value="db44533c56354d859b5ad7dd95d6eb33" /><Binding Key="2" Value="db44533c56354d859b5ad7dd95d6eb33" /></MachineBindings><UnityVersion Value="2018.2.11f1" /></License></root>
  ```

  