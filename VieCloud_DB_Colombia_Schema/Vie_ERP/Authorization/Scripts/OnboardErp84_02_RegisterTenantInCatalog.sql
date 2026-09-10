/*
================================================================================
 OnboardErp84_02_RegisterTenantInCatalog.sql
================================================================================
 Script operativo (NO DACPAC) para registrar el tenant de prueba (base 636) en
 Platform.TenantCatalog — necesario porque el schema Platform se acaba de crear
 vacio (OnboardErp84_01_CreatePlatformSchemaIfMissing.sql) y OnboardErp84Control
 PlaneUsers.sql SOLO inserta cursores para tenants con Status='ACTIVE' que ya
 existan aqui. Sin este registro, el cursor de outbox para el 636 NO se crearia,
 y TenantResolver.cs (Relay/Normalizer/AuthorizationControl) tampoco podria
 resolver {ServerName}/{DatabaseName} para ese tenant en runtime.

 DATOS CONFIRMADOS
 -----------------
   - DatabaseName : INDIGO636
   - ServerName   : ssindigo.database.windows.net   <-- OJO: es un servidor
     DISTINTO al de INDIGOSECV2 (ssindigodev.database.windows.net). Confirmado
     por el equipo el 2026-07-15.

 DATOS PENDIENTES DE CONFIRMAR (placeholders abajo)
 ---------------------------------------------------
   - @TenantCode      : codigo/nombre corto del cliente (hoy usamos '636' como
                         placeholder — cambiar si el cliente tiene un codigo
                         propio en otros catalogos de la plataforma).
   - @Region          : region de Azure del servidor ssindigo.database.windows.net
                         (NO asumir que es la misma region que el ambiente de
                         Autorizaciones — confirmar con el DBA).
   - @ElasticPoolName : si esta base vive en un elastic pool, poner su nombre;
                         si es una base standalone, dejar NULL.

 CUANDO EJECUTAR
 ---------------
 Despues de OnboardErp84_01_CreatePlatformSchemaIfMissing.sql y ANTES de
 OnboardErp84ControlPlaneUsers.sql.

 DONDE EJECUTAR
 --------------
 La base de Control Plane (INDIGOSECV2), NO en la base del tenant.

 IDEMPOTENCIA
 ------------
 INSERT guardado con NOT EXISTS por TenantCode. Seguro re-ejecutar.
================================================================================
*/

DECLARE @TenantCode      NVARCHAR(50)  = N'636';                              -- *** CONFIRMAR ***
DECLARE @DatabaseName    NVARCHAR(128) = N'INDIGO636';
DECLARE @ServerName      NVARCHAR(255) = N'ssindigo.database.windows.net';
DECLARE @ElasticPoolName NVARCHAR(128) = NULL;                                -- *** CONFIRMAR ***
DECLARE @Region          NVARCHAR(50)  = N'eastus2';                          -- *** CONFIRMAR ***

IF NOT EXISTS (SELECT 1 FROM [Platform].[TenantCatalog] WHERE [TenantCode] = @TenantCode)
BEGIN
    INSERT INTO [Platform].[TenantCatalog]
        ([TenantId], [TenantCode], [DatabaseName], [ServerName], [ElasticPoolName], [Region], [IsActive], [Status], [CreatedAtUtc], [UpdatedAtUtc])
    VALUES
        (NEWID(), @TenantCode, @DatabaseName, @ServerName, @ElasticPoolName, @Region, 1, 'ACTIVE', SYSUTCDATETIME(), SYSUTCDATETIME());

    PRINT CONCAT('Tenant registrado: ', @TenantCode, ' (', @ServerName, '/', @DatabaseName, ').');
END
ELSE
BEGIN
    PRINT CONCAT('El tenant ', @TenantCode, ' ya estaba registrado (sin cambios). Verificando datos...');

    SELECT TenantId, TenantCode, DatabaseName, ServerName, ElasticPoolName, Region, IsActive, Status
    FROM [Platform].[TenantCatalog]
    WHERE TenantCode = @TenantCode;
END
GO
