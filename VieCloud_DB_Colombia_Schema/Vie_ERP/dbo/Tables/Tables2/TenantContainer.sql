CREATE TABLE [dbo].[TenantContainer] (
    [Id]          BIGINT   NOT NULL,
    [TenantId]    SMALLINT NOT NULL,
    [ContainerId] INT      NOT NULL,
    [Principal]   BIT      NOT NULL,
    [State]       BIT      NOT NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de relación que asocia contenedores (posiblemente repositorios físicos o lógicos de almacenamiento) con inquilinos (tenants) en un esquema multitenant. Registra si el contenedor es el principal del tenant mediante un indicador booleano y controla su estado activo/inactivo. Permite que un tenant tenga múltiples contenedores asociados, distinguiendo cuál es el contenedor primario.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'TenantContainer';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'TenantContainer';
GO
