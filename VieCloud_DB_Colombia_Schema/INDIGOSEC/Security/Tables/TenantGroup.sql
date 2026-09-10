CREATE TABLE [Security].[TenantGroup] (
    [Id]       INT      IDENTITY (1, 1) NOT NULL,
    [TenantId] SMALLINT NOT NULL,
    [GroupId]  INT      NOT NULL,
    CONSTRAINT [PK_TenantGroup_1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TenantGroup_Group] FOREIGN KEY ([GroupId]) REFERENCES [Security].[Group] ([Id]),
    CONSTRAINT [FK_TenantGroup_Tenant] FOREIGN KEY ([TenantId]) REFERENCES [Security].[Tenant] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se utiliza para almacenar la relación grupos - tenant', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantGroup';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la tabla', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantGroup', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de tenant', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantGroup', @level2type = N'COLUMN', @level2name = N'TenantId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de grupo', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantGroup', @level2type = N'COLUMN', @level2name = N'GroupId';

