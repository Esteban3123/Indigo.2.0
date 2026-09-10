#  ¿Que es Indigo Vie ERP?

Imagine controlar eficientemente su compañía dando alcance y cumplimiento a los objetivos estratégicos de su organización en una sola herramienta, Indigo Vie es un completo ERP que apoya a las áreas administrativas, financieras y de recursos humanos.  

## Introducción

A continuación le mostraremos como descargar e instalar las herramientas necesarias para obtener nuestra solución de Indigo Vie ERP. Tambien a como configurar nuestro sitio de servicios donde se alojaran los web services del ERP

#### Pre-requisitos 📋

* [.NET](https://docs.microsoft.com/es-es/dotnet/fundamentals/) - Framework gratuito y de código abierto para los sistemas operativos Windows, Linux y macOS. ​ Es un sucesor multiplataforma de .NET Framework. Se usa la versión  4.8. [Enlace de Descarga](https://dotnet.microsoft.com/download/dotnet-framework/thank-you/net48-developer-pack-offline-installer) 

* DevExpress - herramienta que ofrece unas de las suits más completas de componentes de interfaz de usuario (UI) en todas las plataformas .NET tales como Windows Forms, MVC, ASP.NET, Silverlight y Windows XAML. Se usa la versión 20.1.8<br>

## ¡Comenzamos! 🚀
Primero descagaremos nuestra solución, se divide en dos proyectos principales llamados servicios (services) y presentación (presentation). Para esto debemos decargar el IDE el cual usaremos, llamado [**Visual Studio Professional**](https://my.visualstudio.com/Downloads?q=visual%20studio%202017&wt.mc_id=o~msft~vscom~older-downloads, "Clic para descargar") en la versión 2017. Luego realizaremos los siguientes pasos:  

![image info](Images/Imagen54.png)<br><br>

> ### 1. Instalaremos visual studio  
Cuando estemos en los pasos de instalación, tener en cuenta estas dos opciones:

![image info](Images/Imagen0.png)

**Nota: Importante siempre ejecutar el visual en modo administrador**

> ### 2. Descargemos e instalaremos GIT
Git es una herramienta que usaremos para el control de versionamiento de producto, para descargarla damos clic [Aqui](https://git-scm.com/download/win) y seguimos los pasos del instalador.  

Recomendado escoger el editor Notepadd como editor de texto por defecto

![image info](Images/Imagen1.png)<br><br>

Es importante tener la opción "git credential manager" seleccionada<br>

![image info](Images/Imagen2.png)  


> ### 3. Descargaremos e instalaremos Azure CLI
Despues de descargar GIT, procedemos con Azure CLI. Para ello damos clic [Aqui](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli-windows?tabs=azure-cli) y seguimos los pasos de instalación.  

> ### 4. Agregamos la extensión azure devops   
Una vez tengamos instalado GIT y Azure CLI, abrimos la consola de comandos de GIT

![image info](Images/Imagen3.png)  
y agregamos el siguiente comando: 

    az extension add --name azure-devops
![image info](Images/Imagen4.png)  

Luego de esto de agregar la extensión, verificamos la sesión en Azure CLI. Para ellos ejecutamos  

    az login

Se abrira el nevagador pidiendonos ingresar las credenciales. Se usan las credenciales corporativas (de correo)

## Ahora procedemos a clonar los repositorios de cliente y servicio 🖇️

Ya estamos listos para descargar el código de la aplicación. por recomendación primero se descarga el proyeto de presentación (ERP_Presentation), seguimos con los siguientes pasos: 

> ### 1. Crear un directorio donde guardaremos las soluciones

Vamos a crear un directorio y alli vamos a guardar nuestra solución, para este ejemplo se creo una carpeta llamada Project y en ella se creo un subdirectorio de nombre "ERP". Usando la consola, nos movemos hasta la ruta donde creamos nuestro directorio. Para ello usamos el comando *cd + ruta* 

![image info](Images/Imagen5.png)

> ### 2. Clonamos los repositorios
Ahora nos dirigimos a los proyectos que están en Azure DevOps, entramos al proyecto que queremos decargar, en este caso ERP_Services, y vamos a la opción Repos -> File

![image info](Images/Imagen6.png)<br>

Ahora, damos clic en "Clone"

![image info](Images/Imagen7.png)<br>

Copiamos la URL

![image info](Images/Imagen8.png)

Ahora, usando la consola, escribimos el siguiente comando, despues de ejecutar, ya habremos descargado la solución del lado de los servicios

    git clone + url
![image info](Images/Imagen9.png)

Esto mismo lo hacemos sobre el proyecto de presentación (ERP_Presentation) para tener las dos soluciones del ERP

![image info](Images/Imagen10.png)

![image info](Images/Imagen11.png)

¡Listo! ya tenemos nuestra solución descargada. Ahora nos encargarmos de configurar el IIS

## Configurando nuestro IIS

Ahora solo nos queda configurar nuestro sitio IIS donde estaran alojados los servicios del ERP. Sigamos con los siguientes pasos.

> ### 1. Vamos a agregar el IIS

- Abrimos el panel de control, vamos a programas y aqui abrimos la opción

![image info](Images/Imagen38.png)

Nos ubicamos en la carpeta ".NET Framework 4.8 Advanced Services" y agregamos todas las carpetas

![image info](Images/Imagen46.png)

Nos ubicamos sobre la carpeta "Internet Information Services" y abrimos los subdirectorios

![image info](Images/Imagen44.png)

- Expandimos subdirectorio *FTP Server* y seleccionamos esta carpeta

![image info](Images/Imagen39.png)

- Expandimos el subdirectorio *Web Management Tools* y seleccionamos esta carpeta

![image info](Images/Imagen40.png)

- Nos ubicamos sobre la carpeta World Wide Web Services > Application Development Features y agregamos lo siguiente

![image info](Images/Imagen41.png)

- Nos ubicamos sobre la carpeta World Wide Web Services > Common HTTP Features y agregamos lo siguiente

![image info](Images/Imagen42.png)

- Es necesario Chequear los demas para la visualizacion del XML 

![image info](Images/Imagen53.png)


- Por último, nos ubicamos sobre la carpeta World Wide Web Services > Security y agregamos lo siguiente

![image info](Images/Imagen43.png)

**Nota:** *Aqui tambien verificamos que tengamos activo el .Net Framework v 4.8*

![image info](Images/Imagen45.png)

> ### 2. Crear un nuevo sitio 
Para crear un nuevo sitio, ingresamos al IIS de nuestro equipo

![image info](Images/Imagen12.png)

Nos ubicamos sobre el directorio "Sitios", damos clic derecho y seleccionamos "Agregar sitio web"

![image info](Images/Imagen13.png) 

Lo configuramos como se muestra a continuación:

![image info](Images/Imagen14.png)

Nos quedara un archivo como este

![image info](Images/Imagen15.png)

Ahora agregamos 2 puestos compartidos más, dando clic donde dice "Enlaces"

![image info](Images/Imagen16.png)

Agregarlos como se muestra a continuación:

**https - puerto 9001**

![image info](Images/Imagen17.png)

**net.tcp - puerto 9000:**

![image info](Images/Imagen18.png)

Ahora, con la solución de los servicios abierta en visual studio, podemos observar que ya se listan los diferentes servicios en el sitio que acabamos de crear

![image info](Images/Imagen33.png)

En cada servicio creado, damos clic en la opción "configuración avanzada"

![image info](Images/Imagen34.png)

Y aqui, agregamos 3 protocolos que son: **http,https,net.tcp**, luego dar clic en aceptar. Importante hacer esto en todos los servicios

![image info](Images/Imagen35.png)

### Ya tenemos el IIS configurado, pero ¿como sabemos que nuestra solución ya nos esta funcionando?

Iniciamos el Visual Studio y lo configuramos para "Ejecutar como administrador"

![image info](Images/Imagen47.png)

Abrimos nuestro proyecto descargado en visual studio, para ello es importante abrir el IDE dos veces, una para el proyecto presentacion (Presentation) y otra para el proyecto de servicios (Services) y procedemos a compilar la solución.

> ##Para EPR-Presentation: Se abre el proyecto llamado IndigoVieErp.sln

![image info](Images/Image55.png)

![image info](Images/Image56.png)

> ##Para EPR-Services: Se abre el proyecto llamado Indigo.ReferenceArchitecture.sln

![image info](Images/Image57.png)

Antes de continuar a la compilacion de la solucion, es necesario que nuestro proyecto presentacion cuente con la configuracion de Indigo. Esta se puede descargar en el siguiente link [indigo.config](https://indigosas-my.sharepoint.com/:u:/g/personal/psalazar_indigo_tech/ETvy5Y933f1EvzKpFh1AGTYBLaYf3FIhSQ6zkt4qs4sNyg?e=9FjZTG). La cual debemos copiar y pegar en (Presentation.Client)

![image info](Images/Imagen58.png)

-Ahora hacemos doble click y nos dirigimos a Open Folder in File Explorer

![image info](Images/Imagen59.png)

-Esto nos redirecciona al File Explorer de presentacion y entramos a la carpeta bin

![image info](Images/Imagen60.png) 

-Entramos en la carpeta debug y ahi es donde ubicamos nuestro (indigo.config)

![image info](Images/Imagen61.png)

-Luego abrimos el archivo y editamos el puerto web y xpo con el puerto de nosotros establecido.

![image info](Images/Imagen68.png)

En este punto ya podemos compilar el proyecto.

![image info](Images/Imagen30.png)

No nos debe generar ningún error

![image info](Images/Imagen31.png)

## NOTA: en caso de presentar este error

![image info](Images/Imagen48.png)

-Para dar solución nos ubicamos en Presentation.Client

![image info](Images/Imagen49.png)

1. removemos la referencia Microsoft.CSharp 

![image info](Images/Imagen50.png)

2. luego la agregamos nuevamente 

![image info](Images/Imagen51.png)
![image info](Images/Imagen52.png)

3. Posteriormente de agregar la referencia volvemos a compilar la solución

Despues, es necesario definir dos proyectos como principales para ejecutar, llamados  **DistributedService.Deployment** y **DistributedService.Xpo.Deployment** para ello, realizamos lo siguiente:

- Damos clic derecho sobre la solución y vamos a propiedades

![image info](Images/Imagen36.png)

- Luego marcamos "start" a los proyectos dichos anteriormente

![image info](Images/Imagen37.png)

### NOTA: En caso de salir este error

![image info](Images/Imagen63.png)

Ingresamos a las propiedades de los servicio **DistributedService.Deployment y DistributedService.Xpo.Deployment** como se muestra en la imagen

![image info](Images/Imagen64.png)

Configuramos los servicios según el puerto que tengamos asignados

-Servicio: Service.Deployment.

![image info](Images/Imagen65.png)

- Servicio: Service.Xpo.Deployment.

![image info](Images/Imagen66.png)

### **Aseguremos**

1- La configuracion del archivo Web.config este apuntando en "Server=ssindigodev" como se muestra en la imagen

![image info](Images/Imagen69.png)

### 📜 Profuncidemos un poco sobre algunos comandos necesarios en GIT para aplicar cambios a nuestra solución 

Imaginemos que vamos a trabajar en un PBI, para este caso el PBI 849, los que debes hacer es lo siguiente:

1. Ubicarnos donde tengamos nuestra solución y hacer git pull de la rama master para descargar los últimos cambios

![image info](Images/Imagen19.png)

2. Crear una nueva rama con el formato PBI-849-descripcion-corta, para esto usa el comando git checkout -b < nombre-rama >

![image info](Images/Imagen20.png)<br>
En el visual, nos aparecera la rama que acabamos de crear<br>

![image info](Images/Imagen21.png)

3. Una vez termines el desarrollo puedes ver los archivos modificados usando el comando **git status**

![image info](Images/Imagen22.png)

4.  Agrega los todos los cambios usando el comando **git add .**

![image info](Images/Imagen23.png)

5. Crea un commit usando el comando **git commit -m “<descripción de los cambios>”**

![image info](Images/Imagen24.png)

6. Ahora debes subir tus cambios al servidor de azure, para esto debes usar el comando **git push origin “rama donde vas a subir los cambios”**

![image info](Images/Imagen25.png)<br>

tu rama deberá aparecer en el repositorio de azure

![image info](Images/Imagen26.png)

7. Vamos a crear el Pull request<br>
un pull request es un proceso de validar un cambio aplicado al código de la aplicación que se va margear de una rama a otra, esta validación la realizan personas más capacitadas que pueden aprobar o no los cambios realizados. 
    - Ve a la sección de Pull request y dale click en crear nuevo PR<br>
    ![image info](Images/Imagen27.png)

    - Debemos usar el siguiente template para todos los Pull Request

    ```
    |   Item                 | Detail |
    | -----------------------|--------|
    | Ticket                 |        |
    | Dependencies           |        |
    | Backward compatibility | Yes/No |
    | DB Changes             | Yes/No |

   ** Descripcion **
   ** DB Changes **
    ```
    **Ejemplo:**

    ![image info](Images/Imagen28.png)


8. Por último, agremos a la personas que deben revisar y aprobar tus cambios

![image info](Images/Imagen29.png)

**Si algún desarrollador te comanta un cambio, debes corregirlo y volver a subir los nuevos cambios**<br>

---

## Bien. ¡Ya estamos listos para empezar!