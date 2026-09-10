# Infrastructure.CrossCutting.Base.Windows

## Descripción
Proyecto que contiene la funcionalidad **específica de Windows** de `Infrastructure.CrossCutting.Base`.

Este proyecto tiene dependencias de librerías que solo funcionan en Windows y **NO puede migrarse a .NET Standard 2.0** sin cambios significativos.

## Contenido

### ✅ Archivos incluidos (con dependencias Windows)
- **ConfigurationFile.vb** - Manejo de archivos de configuración (usa Utils/Helper de Windows)
- **ConfigurationHelper.vb** - Ayudantes de configuración (usa Helper de Windows)
- **CrossThreadExtentions.vb** - Extensiones para invocación cross-thread (ISynchronizeInvoke)
- **Extentions.vb** - Extensiones específicas de Windows (DevExpress, Unity)
- **Helper.vb** - Helpers con EventLog y dependencias Windows
- **IObservador.vb** - Interfaz para patrón Observer con Windows Forms
- **ISujeto.vb** - Interfaz para patrón Observer con Windows Forms
- **MDISingleInstance.vb** - Singleton para instancia MDI y patrón Observer
- **UserActivityHook.vb** - Hooks de teclado y mouse de Windows
- **Utils.vb** - Utilidades con dependencias Windows (Management, Web, Drawing, DevExpress)

### 🪟 Dependencias Windows
- **System.Windows.Forms** - Para hooks de teclado/mouse y controles
- **System.Drawing** - Para operaciones gráficas
- **System.Web** - Para configuración web
- **System.Management** - Para WMI y gestión del sistema
- **System.Diagnostics.EventLog** - Para logging en Event Viewer
- **DevExpress.Docs.v20.1** - Componentes DevExpress
- **DevExpress.Spreadsheet.v20.1.Core** - Manejo de hojas de cálculo
- **DevExpress.Win 20.1.8** - Componentes Windows Forms

### 📦 Otras Dependencias
- **Infrastructure.CrossCutting.Base.Core** ⚠️ Referencia al proyecto Core
- Domain.Base.Entities
- Domain.Security.Entities
- Infrastructure.CrossCutting.Resources
- Infrastructure.CrossCutting.Root
- Microsoft.Practices.Unity (DLL local)
- DevExpress.Win 20.1.8
- Microsoft.Windows.SDK.Contracts 10.0.19041.1
- Newtonsoft.Json 13.0.3

### 🎯 Target Framework
- .NET Framework 4.8
- **NO compatible con .NET Standard 2.0** sin refactorización

## Opciones de Migración Futura

### Opción 1: .NET 6+ Windows
- Migrar a .NET 6+ con soporte de Windows Forms
- Mantener toda la funcionalidad actual
- Mejor rendimiento y características modernas

### Opción 2: Refactorización
- Extraer interfaces para funcionalidad específica de plataforma
- Implementar abstracciones
- Permitir inyección de dependencias según plataforma

### Opción 3: Reemplazo de Dependencias
- **EventLog** → `Microsoft.Extensions.Logging`, `Serilog`, `NLog`
- **DevExpress** → `EPPlus`, `ClosedXML` (para Excel)
- **System.Drawing** → `ImageSharp`, `SkiaSharp`
- **UserActivityHook** → Mantener en proyecto separado Windows-only

## Notas
- Este proyecto **depende** de `Infrastructure.CrossCutting.Base.Core`
- Todos los proyectos que actualmente referencian `Infrastructure.CrossCutting.Base` y usan funcionalidad Windows deben referenciar este proyecto
- Los proyectos que solo necesitan funcionalidad core pueden referenciar únicamente `Infrastructure.CrossCutting.Base.Core`

