# Guía de Migración - Presentation.Reporter.ElectronicDocuments

## Estado Actual del Proyecto

**Fecha:** 2026-01-13  
**Target Framework actual:** `net8.0`  
**Estado:** En progreso - Requiere configuración adicional

---

## 📋 Resumen Ejecutivo

El proyecto `Presentation.Reporter.ElectronicDocuments` ha sido preparado para migrar los reportes de facturación electrónica DevExpress XtraReports desde .NET Framework 4.8 hacia .NET 8, **eliminando todas las dependencias de UI de Windows Forms**.

### Objetivo Principal
Generar reportes como `MemoryStream` o exportar a PDF/HTML para almacenar en Blob Storage, sin necesidad de `DocumentViewer` ni componentes de UI.

---

## ✅ Cambios Realizados

### 1. Eliminación de BindingSource (System.Windows.Forms)

Se eliminaron todas las referencias a `System.Windows.Forms.BindingSource` de los archivos Designer.vb:

| Archivo | Cambios |
|---------|---------|
| `rptBillingNote.Designer.vb` | Eliminado BindingSource1 |
| `rptBillingNoteDetail.Designer.vb` | Eliminado BindingSource1 |
| `rptSaleInvoice.Designer.vb` | Eliminado BindingSource1 |
| `rptSaleInvoiceCapitated.Designer.vb` | Eliminado BindingSource1 |
| `rptBasicBilling.Designer.vb` | Eliminado BindingSource1 |
| `rptInvoiceProducts.Designer.vb` | Eliminado BindingSource1 |

**Nota:** El `DataSource` ahora se asigna en runtime dentro del método `CargarDataSource()`.

### 2. Eliminación de MessageIndigo (MessageBox)

Se reemplazaron todos los usos de `MessageIndigo.Show()` por:
- `System.Diagnostics.Debug.WriteLine()` para logging
- `Throw` para re-lanzar excepciones al llamador

```vb
' ANTES
Catch ex As Exception
    MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
End Try

' DESPUÉS  
Catch ex As Exception
    System.Diagnostics.Debug.WriteLine($"Error en CargarDataSource: {GetExceptionDetails(ex)}")
    Throw
End Try
```

### 3. Cambio de PrintEventArgs

Se actualizó el tipo `PrintEventArgs` de `System.Drawing.Printing` a `DevExpress.XtraPrinting.PrintEventArgs`.

```vb
' ANTES
Private Sub rptSaleInvoice_BeforePrint(sender As Object, e As PrintEventArgs)

' DESPUÉS
Private Sub rptSaleInvoice_BeforePrint(sender As Object, e As DevExpress.XtraPrinting.PrintEventArgs)
```

### 4. Actualización de Image (Bitmap)

Se reemplazó el uso de `System.Drawing.Bitmap` por `DevExpress.XtraPrinting.Drawing.ImageSource`:

```vb
' ANTES
INDXrpbFirm.Image = New Bitmap(mem)

' DESPUÉS
INDXrpbFirm.ImageSource = New DevExpress.XtraPrinting.Drawing.ImageSource(Image.FromStream(mem))
```

### 5. Estructura de Archivos Reorganizada

```
Presentation.Reporter.ElectronicDocuments/
├── Presentation.Reporter.ElectronicDocuments.vbproj
├── IReport.vb                     # Interfaz base de reportes
├── Infrastructure/
│   ├── ConfigurationFile.vb       # Configuración de rutas
│   ├── SessionValues.vb           # Valores de sesión
│   └── XpoServiceEx.vb            # Servicio XPO
├── XpoEntities/
│   ├── BillingEntities.vb         # Entidades de Facturación
│   ├── InventoryEntities.vb       # Entidades de Inventario
│   └── SecurityEntities.vb        # Entidades de Seguridad
├── Utilities/
│   ├── Utils.vb                   # Funciones utilitarias (Num2Text, etc.)
│   └── ParameterExtensions.vb     # Extensiones para ParameterCollection
└── Reports/
    ├── BillingNote/
    │   ├── rptBillingNote.vb + .Designer.vb
    │   └── rptBillingNoteDetail.vb + .Designer.vb
    └── Invoice/
        ├── rptSaleInvoice.vb + .Designer.vb
        ├── rptSaleInvoiceCapitated.vb + .Designer.vb
        ├── rptBasicBilling.vb + .Designer.vb
        ├── rptInvoiceProducts.vb + .Designer.vb
        └── rptSubSaleInvoiceAll.vb       # Helper para layouts
```

---

## ⏳ Pasos Pendientes

### PASO 1: Configurar NuGet Feed de DevExpress

Las DLLs actuales son de .NET Framework y **NO son compatibles** con .NET 8.

**Acción requerida:** Configurar el feed NuGet de DevExpress en `nuget.config`:

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <add key="DevExpress" value="https://nuget.devexpress.com/{YOUR_FEED_KEY}/api" />
    <add key="nuget.org" value="https://api.nuget.org/v3/index.json" />
  </packageSources>
</configuration>
```

### PASO 2: Actualizar .vbproj con Paquetes NuGet

Reemplazar las referencias a DLLs locales por paquetes NuGet:

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <RootNamespace>Presentation.Reporter.ElectronicDocuments</RootNamespace>
    <AssemblyName>Presentation.Reporter.ElectronicDocuments</AssemblyName>
    <OptionInfer>On</OptionInfer>
    <OptionStrict>Off</OptionStrict>
  </PropertyGroup>

  <ItemGroup>
    <!-- DevExpress NuGet Packages para .NET 8 -->
    <PackageReference Include="DevExpress.Reporting.Core" Version="24.2.*" />
    <PackageReference Include="DevExpress.Xpo" Version="24.2.*" />
    <PackageReference Include="DevExpress.Data" Version="24.2.*" />
    
    <!-- System.Drawing para .NET 8 -->
    <PackageReference Include="System.Drawing.Common" Version="8.0.0" />
  </ItemGroup>
</Project>
```

> **Nota:** Usar versión `24.2.*` o la versión más reciente disponible en su feed.

### PASO 3: Resolver Errores de Namespace

Una vez instalados los paquetes NuGet correctos:

1. Verificar que `DevExpress.XtraPrinting.PrintEventArgs` esté disponible
2. Verificar que `DevExpress.XtraPrinting.BarCode.QRCodeGenerator` esté disponible
3. Ajustar cualquier namespace faltante

### PASO 4: Compilar y Probar

```bash
cd Presentation.Reporter.ElectronicDocuments
dotnet restore
dotnet build
```

### PASO 5: Implementar Generación de MemoryStream

Ejemplo de uso para exportar a PDF sin UI:

```vb
Public Function GenerateInvoicePdf(invoiceId As Integer) As Byte()
    Using report As New rptSaleInvoice()
        report.ParametrosReporte = New Object() {invoiceId}
        report.CargarDataSource()
        
        Using ms As New MemoryStream()
            report.ExportToPdf(ms)
            Return ms.ToArray()
        End Using
    End Using
End Function
```

---

## 🔴 Errores Actuales (24 errores)

### Error Principal: DLLs de .NET Framework

```
error BC30002: No está definido el tipo 'DevExpress.XtraPrinting.PrintEventArgs'
```

**Causa:** Las DLLs referenciadas son de .NET Framework 4.8 y no son compatibles con .NET 8.

**Solución:** Usar paquetes NuGet de DevExpress para .NET 8 (ver Paso 2).

---

## 📊 Comparación: Antes vs Después

| Aspecto | Antes (.NET Framework) | Después (.NET 8) |
|---------|------------------------|------------------|
| Target | net48 + WinForms | net8.0 (sin WinForms) |
| BindingSource | System.Windows.Forms | Eliminado - runtime assignment |
| MessageBox | MessageIndigo.Show() | Debug.WriteLine() + Throw |
| PrintEventArgs | System.Drawing.Printing | DevExpress.XtraPrinting |
| Image | System.Drawing.Bitmap | DevExpress.XtraPrinting.Drawing.ImageSource |
| Exportación | DocumentViewer | ExportToPdf(), ExportToHtml() |

---

## 🎯 Beneficios de la Migración

1. **Compatibilidad Cloud:** Puede ejecutarse en Azure Functions, AWS Lambda, contenedores Docker
2. **Sin dependencias de UI:** No requiere Windows Desktop
3. **Exportación directa:** PDF, HTML, XLSX directamente a MemoryStream
4. **Blob Storage:** Fácil almacenamiento en Azure Blob, S3, etc.
5. **Menor footprint:** Solo componentes necesarios para generación de reportes

---

## 📝 Notas Técnicas

### DevExpress XtraReports en .NET Core/.NET 5+

DevExpress soporta XtraReports en .NET Core desde la versión 19.2. Para .NET 8, se recomienda:

- Usar versión **23.2 o superior** para mejor compatibilidad
- Instalar `DevExpress.Reporting.Core` (no el paquete completo con WinForms)
- Para exportación web, usar `DevExpress.Reporting.Export`

### Recursos

- [DevExpress: XtraReports in .NET Core](https://docs.devexpress.com/XtraReports/401686/getting-started-in-net6)
- [DevExpress NuGet Feed Setup](https://docs.devexpress.com/GeneralInformation/116698/installation/install-devexpress-controls-using-nuget-packages)

---

## ✍️ Autor

Generado automáticamente durante la migración.  
Fecha: 2026-01-13
