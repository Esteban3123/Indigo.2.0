# 🏥 Vie_ERP Database Schema

Este repositorio contiene el **esquema completo de la base de datos Vie_ERP**, un sistema integral de gestión hospitalaria y de servicios de salud. El proyecto utiliza SQL Server Database Project (.sqlproj) para el control de versiones y gestión d## ⚙️ Pipeline de Integración Continua

Este proyecto incluye un **pipeline de Azure DevOps** que automáticamente:

- ✅ **Valida la compilación** cuando se integran cambios en `master`
- 🔍 **Verifica la integridad** del esquema de base de datos
- 📦 **Genera artefactos DACPAC** validados para despliegue manual
- 📊 **Reporta métricas** y análisis de calidad del código SQL

### Trigger del Pipeline
- Se ejecuta automáticamente en commits a la rama `master`
- Valida Pull Requests hacia `master`
- Solo se activa cuando cambian archivos del esquema de DB

> 📖 **Documentación detallada**: Ver [pipeline-documentation.md](docs/pipeline-documentation.md) para configuración completa del pipeline.

## 🔄 Flujo de Trabajo de Desarrollo

1. **Desarrollo Local** - Modificar esquemas en ramas de feature
2. **Pull Request** - Crear PR hacia `master` (el pipeline valida automáticamente)
3. **Integración** - Merge a `master` genera artefactos DACPAC validados
4. **Despliegue Manual** - DBAs usan los artefactos para despliegue controladode base de datos.

## 📋 Descripción del Sistema

**Vie_ERP** es un sistema de planificación de recursos empresariales (ERP) especializado en el sector salud, que incluye módulos para:

- 🏥 **Gestión Hospitalaria** - Admisiones, historia clínica, programación
- 💰 **Facturación y Pagos** - Facturación médica, pagos, contratos
- 👥 **Recursos Humanos** - Nómina, talento humano, autorizaciones
- 📊 **Gestión Financiera** - Contabilidad general, presupuesto, costos
- 🏪 **Inventarios** - Gestión de medicamentos, insumos médicos
- 📈 **Reportes y Analytics** - Reportes gerenciales, análisis de datose Validación Azure DevOps - Vie_ERP Database

Este repositorio contiene el **pipeline de validación automática** para el proyecto de base de datos **Vie_ERP**. El pipeline se ejecuta automáticamente cuando se integran cambios en la rama `master`, validando la integridad del esquema antes de generar artefactos.

## 🎯 Propósito del Pipeline

El pipeline **NO despliega automáticamente** a ningún ambiente. Su función es:

- 🔍 **Validar cambios** cuando se integran en la rama `master`
- ✅ **Verificar compilación** del proyecto .sqlproj sin errores
- 🛡️ **Detectar problemas** de esquema antes del despliegue
- 📦 **Generar artefactos** DACPAC validados para despliegue manual
- 📊 **Reportar métricas** y análisis de calidad del código SQL
- 🚨 **Notificar resultados** del proceso de validación

## �️ Estructura del Esquema de Base de Datos

```
Vie_ERP/                                # Proyecto principal de base de datos
├── 📄 Vie_ERP.sqlproj                  # Archivo del proyecto SQL Server
├── 📁 AccountManagement/               # Gestión de cuentas
├── 📁 Admissions/                      # Admisiones hospitalarias
├── 📁 Authorization/                   # Sistema de autorizaciones
├── 📁 Billing/                        # Facturación y cobros
├── 📁 Budget/                          # Gestión presupuestaria
├── 📁 Common/                          # Objetos comunes y utilitarios
├── 📁 Contract/                        # Gestión de contratos
├── 📁 Cost/                           # Análisis de costos
├── � EHR/                            # Historia clínica electrónica
├── 📁 GeneralLedger/                  # Contabilidad general
├── 📁 HumanTalent/                    # Recursos humanos
├── 📁 Inventory/                      # Gestión de inventarios
├── 📁 MedicalHistory/                 # Historia médica
├── � Nursing/                        # Enfermería
├── 📁 Payroll/                        # Nómina
├── 📁 Payments/                       # Gestión de pagos
├── � Scheduling/                     # Programación de citas
├── 📁 Security/                       # Seguridad del sistema
└── 📁 [Otros módulos especializados]  # Módulos adicionales del ERP

Cada esquema contiene:
├── 📁 Tables/                         # Tablas del módulo
├── 📁 Views/                          # Vistas del módulo
├── 📁 Stored Procedures/              # Procedimientos almacenados
├── � Functions/                      # Funciones del módulo
└── 📁 [Otros objetos]/                # Secuencias, tipos, etc.
```

## 🚀 Configuración Rápida

### 1. Prerrequisitos

- Azure DevOps con acceso a pipelines
- Azure CLI instalado
- Permisos de administrador en el proyecto
- Personal Access Token (PAT) de Azure DevOps

### 2. Configuración Automática

Ejecutar el script de configuración:

```powershell
.\scripts\setup-pipeline.ps1 -Organization "tu-org" -Project "tu-proyecto" -PersonalAccessToken "tu-pat"
```

### 3. Configuración Manual Restante

1. **Crear Service Connections** en Azure DevOps para cada ambiente
2. **Configurar aprobadores** en los ambientes de staging y producción
3. **Validar conectividad** a los servidores SQL Server
4. **Ajustar variables** según tu infraestructura

## 🏗️ Tecnologías y Herramientas

- **🗄️ SQL Server** - Motor de base de datos principal
- **📄 SQL Server Data Tools (SSDT)** - Desarrollo y proyecto .sqlproj
- **🔧 Visual Studio / VS Code** - IDE para desarrollo
- **📦 DACPAC** - Paquetes de aplicación de capa de datos
- **🔄 Git** - Control de versiones del esquema
- **⚙️ Azure DevOps** - Pipeline de integración continua

## 📊 Estadísticas del Proyecto

El proyecto de base de datos incluye aproximadamente:

- **🗃️ Tablas**: +200 tablas distribuidas en múltiples esquemas
- **👁️ Vistas**: +50 vistas para consultas optimizadas
- **⚙️ Stored Procedures**: +100 procedimientos almacenados
- **🔧 Funciones**: +30 funciones personalizadas
- **📝 Esquemas**: +25 esquemas organizados por módulo funcional

## � Configuración del Pipeline (`azure-pipelines.yml`)

### 🎯 Triggers Configurados

**Ejecución Automática:**
- ✅ Push directo a la rama `master`
- ✅ Pull Requests hacia la rama `master`
- ✅ Solo cuando cambian archivos relacionados con la DB (`Vie_ERP/**`, `*.sql`, `*.sqlproj`)

**NO se ejecuta en:**
- ❌ Otras ramas (develop, feature, etc.)
- ❌ Cambios en documentación o scripts no relacionados
- ❌ Commits que no afecten el esquema de base de datos

### 📋 Etapas del Pipeline

1. **🏗️ Build Stage**
   - Compila el proyecto `Vie_ERP.sqlproj`
   - Verifica sintaxis SQL y dependencias
   - Genera archivo DACPAC
   - Valida generación exitosa de artefactos

2. **🔍 Database Validation Stage**
   - Valida estructura del DACPAC sin desplegar
   - Verifica integridad del esquema
   - Analiza dependencias entre objetos
   - Confirma que el esquema es deployable

3. **📊 Quality Gates Stage**
   - Análisis de código SQL (detecta `SELECT *`, `NOLOCK`, etc.)
   - Genera métricas del proyecto (tablas, views, SPs, etc.)
   - Crea reportes de calidad
   - Valida best practices de SQL

4. **📢 Notification Stage**
   - Notifica resultados del pipeline
   - Publica artefactos validados
   - Genera resumen para el equipo
   - Confirma disponibilidad para despliegue manual

## 🛡️ Validaciones y Controles

### Validaciones de Compilación
- ✅ Sintaxis SQL correcta
- ✅ Dependencias resueltas
- ✅ Generación exitosa de DACPAC

### Validaciones de Esquema
- ✅ Estructura de DACPAC válida
- ✅ Análisis de dependencias
- ✅ Verificación de integridad referencial
- ✅ Validación de objetos de base de datos
- ✅ Preparación de artefactos para despliegue

### Controles de Calidad
- ✅ Detección de `SELECT *`
- ✅ Detección de `NOLOCK`
- ✅ Detección de SQL dinámico
- ✅ Métricas de complejidad

## 📋 Variables de Configuración

### Servidores por Ambiente
- **Development**: `dev-sql.company.local`
- **Test**: `test-sql.company.local`
- **Staging**: `staging-sql.company.local`
- **Production**: `prod-sql.company.local`

### Service Connections
- **Dev**: `Dev-SQL-ServiceConnection`
- **Test**: `Test-SQL-ServiceConnection`
- **Staging**: `Staging-SQL-ServiceConnection`
- **Production**: `Prod-SQL-ServiceConnection`

## 🔧 Personalización

### Ajustar Servidores SQL
Editar variables en `.azure/variable-groups.yml`:

```yaml
DevSqlServer: 'tu-servidor-dev'
TestSqlServer: 'tu-servidor-test'
# etc...
```

### Modificar Validaciones
Editar `templates/database-deployment.yml` para agregar validaciones específicas:

```yaml
- task: PowerShell@2
  displayName: 'Custom Validation'
  inputs:
    script: |
      # Tu validación personalizada aquí
```

### Configurar Notificaciones
Ajustar las variables de notificación:

```yaml
NotificationEmail: 'tu-equipo@company.com'
SlackWebhook: 'tu-webhook-slack'
```

## 📚 Documentación Completa

Para información detallada, consultar:
- 📖 [Documentación Completa](docs/pipeline-documentation.md)
- 🔧 [Configuración Avanzada](docs/pipeline-documentation.md#configuración-inicial)
- 🔍 [Troubleshooting](docs/pipeline-documentation.md#mantenimiento-y-troubleshooting)

## 🚀 Cómo Funciona en la Práctica

### 1. **Desarrollo Normal**
   - Los desarrolladores trabajan en sus ramas locales
   - Cuando están listos, crean un PR hacia `master`
   - El pipeline valida automáticamente los cambios
   - Si la validación pasa, el PR puede ser aprobado y merged

### 2. **Integración en Master**
   - Al hacer merge a `master`, el pipeline se ejecuta automáticamente
   - Valida todo el esquema completo
   - Genera un DACPAC validado y listo para usar
   - Notifica al equipo sobre el resultado

### 3. **Despliegue Manual**
   - Los DBAs descargan el artefacto DACPAC validado
   - Realizan el despliegue manual a los ambientes correspondientes
   - Siguen los procedimientos de change management establecidos

## 🚀 Configuración del Proyecto

### 1. Prerrequisitos para Desarrollo

- **SQL Server** (2019 o superior)
- **SQL Server Management Studio (SSMS)** o **Azure Data Studio**
- **Visual Studio** con SQL Server Data Tools (SSDT) o **VS Code**
- **Git** para control de versiones

### 2. Clonar y Configurar el Proyecto

```bash
# Clonar el repositorio
git clone [URL_DEL_REPOSITORIO]
cd ERP_DB_Schema

# Abrir el proyecto
# En Visual Studio: abrir Database Schema.sln
# En VS Code: abrir la carpeta del proyecto
```

### 3. Compilar el Proyecto

```bash
# Compilar usando MSBuild
msbuild Vie_ERP/Vie_ERP.sqlproj /p:Configuration=Release

# O desde Visual Studio: Build > Build Solution
```

### 4. Generar DACPAC

El archivo DACPAC se genera en:
```
Vie_ERP/bin/Release/Vie_ERP.dacpac
```

## 📚 Documentación Adicional

- 📖 [Documentación del Pipeline](docs/pipeline-documentation.md) - Configuración detallada del CI/CD
- 🔧 [Scripts de Configuración](scripts/) - Herramientas de automatización
- 📋 [Templates](templates/) - Plantillas reutilizables del pipeline

## 🆘 Soporte y Contactos

- **Database Team**: dba@company.com
- **DevOps Team**: devops@company.com
- **Development Team**: dev-team@company.com

---

## 🏥 Sobre Vie_ERP

**Vie_ERP** es la plataforma integral de gestión hospitalaria que centraliza todos los procesos operativos, financieros y clínicos de instituciones de salud. Este repositorio contiene el núcleo de datos que soporta toda la operación del sistema, asegurando integridad, performance y escalabilidad para el sector salud.

## ✅ Beneficios del Pipeline

Con este pipeline implementado, obtienes:

- 🛡️ **Protección** - No más errores de compilación en master
- ⚡ **Rapidez** - Validación automática en segundos
- � **Artefactos Confiables** - DACPACs siempre validados
- 🔍 **Visibilidad** - Estado claro de cada cambio
- 🎯 **Simplicidad** - Enfoque en validación, despliegue manual controlado

**¡El esquema de base de datos Vie_ERP ahora tiene validación automática profesional!** 🎊