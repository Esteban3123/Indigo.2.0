# Guía de implementación de Docker y Make para windows

Una guía rápida para instalación de Docker y GNU Make para su uso en pruebas.

## Docker

Lo primero es instalar y configurar Docker para usar contenedores Windows, para ello se puede entrar al [enlace de instalación](https://desktop.docker.com/win/main/amd64/Docker%20Desktop%20Installer.exe).

Al instalar Docker desktop se deben habilitar características de windows para poder correr contenedores windows, se habilitan las característica de Contendores

![Características](https://daniccardenas.com/wp-content/uploads/2019/02/image.png)

Al ya tener la característica habilitada se selecciona mostrar íconos ocultos en la derecha de la barra de tareas y al ícono de docker se da clic derecho y Cambiar a contenedores windows.

![Switch windows containers](https://learn.microsoft.com/en-us/virtualization/windowscontainers/quick-start/media/docker-for-win-switch.png)

Luego se puede abrir la Powershell en modo administrador preferiblemente y escrbir `docker version` para comprobar si se instaló correctamente, debe aparecer de esta forma: 

![docker version](https://pixelrobots.co.uk/wp-content/uploads/2018/01/Snip_227.png)

Una vez instalado Docker se pueden crear los contenedores que están establecidos en el archivo __docker-compose.yml__, este viene siendo un archivo de configuración que define cómo se van a crear contenedores y bajo qué imagen se van a crear.

## GNU Make

Con el fin de abreviar y facilitar el uso de comandos Docker se implementó una herramienta que facilita compilar, ejecutar programas y abreviar comandos llamada GNU Make, el Archivo con nombre Makefile contiene las directivas de uso de la herramienta.

Para ello se debe instalar Make, para ello se puede usar el gestor de paquetes Chocolatey que normalmente viene en Windows para la powershell, para comprobar su instalación se puede hacer uso del comando `choco`, aparecerá la versión instalada de chocolatey 

![chocolatey](https://www.bleepstatic.com/content/hl-images/2020/11/07/Choco-header.jpg)

Al ya tenerlo instalado e ejecuta el comando de instalación de GNU Make usando choco:

```powershell
choco install make
```

Ejecutado el comando se comprueba la instalación de make usando el comando `make -v`, este mostrará la versión e información extra sobre make.

El archivo llamado __Makefile__ es el que tiene las instrucciones necesarias para levantar y borrar los contenedores de Docker, para ejecutar cualquier comando de make se hace de la sieguiente manera:

Teniendo en cuenta que en el Makefile hay instrucciones como

```makefile
up:
	docker compose up -d 
```

Se ejecuta en la consola powershell dentro del proyecto DistributedService.RCM así:

```powershell
make up
```

Cada comando dentro del Makefile se ejecuta con el prefijo _make_ y luego la instrucción correspondiente, ese comando por ejemplo levanta los contenedores establecidos en el __docker-compose.yml__