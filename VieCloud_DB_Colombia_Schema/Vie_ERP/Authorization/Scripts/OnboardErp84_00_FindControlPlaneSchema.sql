/*
================================================================================
 OnboardErp84_00_FindControlPlaneSchema.sql
================================================================================
 Script de DIAGNOSTICO (NO DACPAC, NO hace cambios) para ubicar la base de datos
 que contiene el schema `Platform` (Platform.TenantCatalog, TenantOutboxCursor,
 TenantOutboxLease, PoolThrottlingConfig) — la base de "Control Plane" que usa
 AzClinicalOutboxRelay / AzClinicalAuthorizationControl para resolver tenants.

 CONTEXTO
 --------
 Se intento correr OnboardErp84ControlPlaneUsers.sql contra INDIGOSECV2 y luego
 contra INDIGOSEC (servidor ssindigodev.database.windows.net) y en NINGUNA de
 las dos aparecio el schema `Platform`. Ambas mostraron los mismos schemas
 (Application, Audit, az_func, Chat, Common, Confg, Feature, Management,
 Marketplace, Prometheus, Report, Security, SecuritySync, SelfService,
 WebAppointment...) que parecen ser de otro servicio, no de Autorizaciones.

 COMO USAR
 ---------
 PASO 1: correr el bloque "A" contra la base `master` del servidor
 (ssindigodev.database.windows.net) para listar TODAS las bases que existen ahi.

 PASO 2: para cada base candidata (o todas, si son pocas), conectarse a ella y
 correr el bloque "B" — es un chequeo rapido de una sola fila que dice SI/NO
 tiene el schema Platform y, si lo tiene, cuantos tenants ACTIVE hay.

 Azure SQL Database (PaaS) NO permite queries cross-database directas (no hay
 sp_MSforeachdb ni USE dentro del mismo batch entre bases logicas separadas),
 por eso el chequeo es base por base.
================================================================================
*/

-- ============================================================================
-- BLOQUE A — correr contra la base `master` (lista todas las bases del server)
-- ============================================================================
SELECT name AS NombreBaseDatos, state_desc, create_date
FROM sys.databases
ORDER BY name;
GO

-- ============================================================================
-- BLOQUE B — correr contra CADA base candidata (una por una)
-- Usa SQL dinamico a proposito: si se referenciara Platform.TenantCatalog
-- directo en un SELECT normal, el batch fallaria a nivel de compilacion
-- ("Invalid object name") en cualquier base donde la tabla no exista, en vez
-- de reportar limpiamente que no existe.
-- ============================================================================
DECLARE @TienePlatform BIT = CASE WHEN EXISTS (SELECT 1 FROM sys.schemas WHERE name = 'Platform') THEN 1 ELSE 0 END;
DECLARE @TieneTenantCatalog BIT = CASE WHEN EXISTS (
        SELECT 1 FROM sys.tables t WHERE SCHEMA_NAME(t.schema_id) = 'Platform' AND t.name = 'TenantCatalog'
    ) THEN 1 ELSE 0 END;
DECLARE @CantidadTablasEnPlatform INT = (SELECT COUNT(*) FROM sys.tables WHERE SCHEMA_NAME(schema_id) = 'Platform');
DECLARE @TenantsActivos NVARCHAR(20) = 'N/A (tabla no existe aqui)';

IF @TieneTenantCatalog = 1
BEGIN
    DECLARE @sql NVARCHAR(200) = N'SELECT @cnt = CAST(COUNT(*) AS NVARCHAR(20)) FROM Platform.TenantCatalog WHERE Status = ''ACTIVE''';
    EXEC sp_executesql @sql, N'@cnt NVARCHAR(20) OUTPUT', @cnt = @TenantsActivos OUTPUT;
END

SELECT
    DB_NAME() AS BaseActual,
    CASE WHEN @TienePlatform = 1 THEN 'SI tiene schema Platform' ELSE 'NO tiene schema Platform' END AS TienePlatform,
    @CantidadTablasEnPlatform AS CantidadTablasEnPlatform,
    @TenantsActivos AS TenantsActivosSiAplica;
GO
