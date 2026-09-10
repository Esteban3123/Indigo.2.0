CREATE TABLE [Security].[TenantContainerInt] (
    [Id]          BIGINT   NOT NULL,
    [TenantId]    SMALLINT NOT NULL,
    [ContainerId] INT      NOT NULL,
    [Principal]   BIT      NOT NULL,
    [State]       BIT      NOT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación entre los inquilinos (empresas o clientes del sistema) y los contenedores de recursos o módulos a los que tienen acceso, incluyendo si son el contenedor principal y si la relación está activa.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantContainerInt';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantContainerInt';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de asignación entre inquilino y contenedor.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantContainerInt', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantContainerInt', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del inquilino o empresa cliente dentro del sistema multitenant.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantContainerInt', @level2type = N'COLUMN', @level2name = N'TenantId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantContainerInt', @level2type = N'COLUMN', @level2name = N'TenantId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del contenedor de recursos, módulo o agrupación lógica asignada al inquilino.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantContainerInt', @level2type = N'COLUMN', @level2name = N'ContainerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantContainerInt', @level2type = N'COLUMN', @level2name = N'ContainerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si este contenedor es el principal o predeterminado para el inquilino (1 = principal, 0 = secundario).', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantContainerInt', @level2type = N'COLUMN', @level2name = N'Principal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantContainerInt', @level2type = N'COLUMN', @level2name = N'Principal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la asignación: activa o inactiva (1 = activo, 0 = inactivo).', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantContainerInt', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantContainerInt', @level2type = N'COLUMN', @level2name = N'State';
