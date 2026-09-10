CREATE TABLE [Security].[TenantContainer] (
    [Id]          BIGINT   IDENTITY (1, 1) NOT NULL,
    [TenantId]    SMALLINT NOT NULL,
    [ContainerId] INT      NOT NULL,
    [Principal]   BIT      CONSTRAINT [DF_TenantContainer_Principal] DEFAULT ((0)) NOT NULL,
    [State]       BIT      DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_TenantContainer] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TenantContainer_Container] FOREIGN KEY ([ContainerId]) REFERENCES [Security].[Containers] ([Id]),
    CONSTRAINT [FK_TenantContainer_Tenant] FOREIGN KEY ([TenantId]) REFERENCES [Security].[Tenant] ([Id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_TenantContainer_ContainerId]
    ON [Security].[TenantContainer]([ContainerId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TenantContainer_TenantId]
    ON [Security].[TenantContainer]([TenantId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena los containers que hacen parte de un tenant', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantContainer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la tabla', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantContainer', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de tenant', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantContainer', @level2type = N'COLUMN', @level2name = N'TenantId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de container', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantContainer', @level2type = N'COLUMN', @level2name = N'ContainerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el contenedor es principal para el grupo de containers en el tenant', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantContainer', @level2type = N'COLUMN', @level2name = N'Principal';

