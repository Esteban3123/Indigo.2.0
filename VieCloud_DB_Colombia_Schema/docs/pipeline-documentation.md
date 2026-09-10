# Documentación del Pipeline de CI/CD para Vie_ERP Database

## Descripción General

Este documento describe la implementación del pipeline de CI/CD para el proyecto de base de datos **Vie_ERP**, que automatiza la compilación, validación y despliegue del esquema de base de datos SQL Server.

## Arquitectura del Pipeline

### 🏗️ Pipeline Principal (`azure-pipelines.yml`)

El pipeline principal maneja la integración continua con las siguientes etapas:

1. **Build** - Compilación del proyecto .sqlproj
2. **Database Validation** - Validación del esquema en ambiente limpio
3. **Quality Gates** - Análisis de calidad y métricas
4. **Notification** - Notificaciones de resultados

### � Pipeline de Release (En Evaluación)

*El pipeline de release (`azure-pipelines-release.yml`) está actualmente en evaluación para definir el proceso de comparación y despliegue. Por ahora, el enfoque está en la integración continua con validación completa del esquema.*

## Configuración Inicial

### 📋 Prerrequisitos

1. **Azure DevOps** con acceso a pipelines
2. **Conexiones de servicio** configuradas para cada ambiente
3. **Grupos de variables** creados en Azure DevOps
4. **Ambientes** configurados con aprobaciones apropiadas

### 🔧 Variables Requeridas

Crear los siguientes grupos de variables en Azure DevOps:

#### Grupo: `Vie_ERP_Database_Variables`
```
DevSqlServer: dev-sql.company.local
TestSqlServer: test-sql.company.local  
StagingSqlServer: staging-sql.company.local
ProdSqlServer: prod-sql.company.local

DevServiceConnection: Dev-SQL-ServiceConnection
TestServiceConnection: Test-SQL-ServiceConnection
StagingServiceConnection: Staging-SQL-ServiceConnection
ProdServiceConnection: Prod-SQL-ServiceConnection
```

### 🔐 Service Connections

Crear conexiones de servicio para cada ambiente:

1. **Dev-SQL-ServiceConnection**
   - Tipo: Azure Resource Manager
   - Scope: Subscription o Resource Group
   - Permisos: Contributor en el grupo de recursos

2. **Test-SQL-ServiceConnection** (similar a Dev)
3. **Staging-SQL-ServiceConnection** (similar a Dev)
4. **Prod-SQL-ServiceConnection** (similar a Dev)

### 🌍 Configuración de Ambientes

Crear los siguientes ambientes en Azure DevOps con sus respectivas aprobaciones:

#### Development Environment
- **Nombre**: `development`
- **Aprobaciones**: No requeridas
- **Despliegue**: Automático

#### Test Environment
- **Nombre**: `test`
- **Aprobaciones**: No requeridas
- **Despliegue**: Automático después de Development

#### Staging Environment
- **Nombre**: `staging`
- **Aprobaciones**: 1 aprobador requerido (Team Lead)
- **Restricciones**: Solo desde rama `main`

#### Production Environment
- **Nombre**: `production`
- **Aprobaciones**: 2 aprobadores requeridos (Team Lead + DBA)
- **Restricciones**: Solo desde rama `main`
- **Horario**: Solo durante ventana de mantenimiento

## Flujo de Trabajo

### 📈 Desarrollo Normal

1. **Feature Branch** → Crear feature branch desde `develop`
2. **Development** → Hacer cambios en archivos .sql
3. **Pull Request** → Crear PR hacia `develop`
4. **CI Pipeline** → Se ejecuta automáticamente para validar cambios
5. **Code Review** → Revisión por pares
6. **Merge to Develop** → Merge después de aprobación
7. **Auto Deploy to Dev/Test** → Despliegue automático a ambientes lower

### � Validación de Cambios

1. **Release PR** → Crear PR desde `develop` hacia `main`
2. **CI Pipeline** → Validación completa del esquema
3. **Code Review** → Revisión exhaustiva de cambios
4. **Merge to Main** → Merge después de aprobaciones
5. **Artefactos Listos** → DACPAC validado listo para despliegue manual

### 🔥 Hotfixes

1. **Hotfix Branch** → Crear desde `main`
2. **Fix & Test** → Aplicar fix y validar con CI pipeline
3. **Direct to Main** → PR directo a `main` con validación
4. **Manual Deploy** → Despliegue manual usando artefactos validados
5. **Backport to Develop** → Sincronizar cambios

## Características del Pipeline

### ✅ Validaciones Implementadas

- **Compilación del .sqlproj** - Verifica sintaxis SQL
- **Generación de DACPAC** - Empaqueta el esquema
- **Despliegue a DB temporal** - Prueba creación desde cero
- **Validaciones de integridad** - DBCC CHECKDB y consultas de validación
- **Análisis de calidad** - Detecta patrones problemáticos
- **Métricas del proyecto** - Conteo de objetos y estadísticas

### 🔧 Funcionalidades Avanzadas

- **Backup automático** - En ambientes staging/prod
- **Schema drift detection** - Compara diferencias
- **Rollback capability** - Capacidad de reversar cambios
- **Smoke testing** - Pruebas básicas post-despliegue
- **Performance baseline** - Verificación de rendimiento
- **Comprehensive reporting** - Reportes detallados

### 📊 Métricas y Reportes

El pipeline genera automáticamente:

- **Reporte de compilación** - Errores y warnings
- **Métricas de esquema** - Conteo de objetos por tipo
- **Reporte de despliegue** - Estado por ambiente
- **Análisis de calidad** - Issues detectados
- **Logs de validación** - Resultados de pruebas

## Mantenimiento y Troubleshooting

### 🔍 Resolución de Problemas Comunes

#### Error de Compilación
```
Error: SQL syntax error in file X.sql
```
**Solución**: Revisar sintaxis SQL en el archivo indicado

#### Fallo en Validación de Esquema
```
Error: Schema validation failed
```
**Solución**: Verificar dependencias entre objetos y orden de creación

#### Timeout en Despliegue
```
Error: Deployment timeout after 60 minutes
```
**Solución**: Revisar tamaño de scripts y optimizar queries pesadas

#### Fallo en Service Connection
```
Error: Cannot connect to SQL Server
```
**Solución**: Verificar credenciales y conectividad de red

### 📋 Lista de Verificación para Troubleshooting

- [ ] ¿Están las variables correctamente configuradas?
- [ ] ¿Las service connections tienen permisos apropiados?
- [ ] ¿El servidor SQL Server está accesible desde el agent?
- [ ] ¿Los archivos .sql tienen sintaxis correcta?
- [ ] ¿Las dependencias entre objetos están bien definidas?
- [ ] ¿El tamaño del DACPAC es razonable?

### 🔧 Configuración de Monitoreo

#### Alertas Recomendadas

1. **Pipeline Failures** - Notificar fallos inmediatamente
2. **Long Running Deployments** - Alertar si toma > 30 minutos
3. **Quality Gate Failures** - Notificar issues de calidad
4. **Production Deployment Success** - Confirmar despliegues exitosos

#### Dashboards

Crear dashboards para monitorear:
- **Build Success Rate** - % éxito por periodo
- **Deployment Frequency** - Frecuencia de despliegues
- **Lead Time** - Tiempo desde commit hasta producción
- **Database Growth** - Crecimiento del esquema en el tiempo

## Mejores Prácticas

### 📝 Desarrollo de Base de Datos

1. **Usar transacciones** en scripts de migración
2. **Incluir rollback scripts** para cambios críticos
3. **Testear en ambiente similar** a producción
4. **Documentar cambios** en comments SQL
5. **Seguir convenciones** de nomenclatura

### 🚀 Pipeline Management

1. **Monitorear regularmente** la salud del pipeline
2. **Actualizar variables** cuando cambien ambientes
3. **Revisar logs** de despliegues fallidos
4. **Mantener documentación** actualizada
5. **Entrenar al equipo** en uso del pipeline

### 🔐 Seguridad

1. **Usar service principals** en lugar de cuentas personales
2. **Rotar credenciales** regularmente
3. **Limitar permisos** al mínimo necesario
4. **Auditar accesos** a ambientes productivos
5. **Encriptar variables** sensibles

## Contactos

- **Database Team**: dba@company.com
- **DevOps Team**: devops@company.com  
- **Development Team**: dev-team@company.com

## Referencias

- [Azure DevOps Pipelines Documentation](https://docs.microsoft.com/en-us/azure/devops/pipelines/)
- [SQL Server Data Tools (SSDT)](https://docs.microsoft.com/en-us/sql/ssdt/)
- [Database DevOps Best Practices](https://www.redgate.com/solutions/database-devops)