CREATE TABLE [MixingStation].[CMExternalCareCenter] (
    [Id]                   INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CMConfigurationId]    INT NOT NULL,
    [CustomerId]           INT NOT NULL,
    [ProductionLineId]     INT NOT NULL,
    [ExternalCareCenterId] INT NOT NULL,
    [Status]               BIT NOT NULL,
    CONSTRAINT [PK_CMExternalCareCenter] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CMExternalCareCenter_CMConfiguration] FOREIGN KEY ([CMConfigurationId]) REFERENCES [MixingStation].[CMConfiguration] ([Id]),
    CONSTRAINT [FK_CMExternalCareCenter_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [Common].[Customer] ([Id]),
    CONSTRAINT [FK_CMExternalCareCenter_ExternalCareCenter] FOREIGN KEY ([ExternalCareCenterId]) REFERENCES [MixingStation].[ExternalCareCenter] ([Id]),
    CONSTRAINT [FK_CMExternalCareCenter_ProductionLine] FOREIGN KEY ([ProductionLineId]) REFERENCES [MixingStation].[ProductionLine] ([Id])
);


GO
ALTER TABLE [MixingStation].[CMExternalCareCenter] NOCHECK CONSTRAINT [FK_CMExternalCareCenter_CMConfiguration];


GO
ALTER TABLE [MixingStation].[CMExternalCareCenter] NOCHECK CONSTRAINT [FK_CMExternalCareCenter_Customer];


GO
ALTER TABLE [MixingStation].[CMExternalCareCenter] NOCHECK CONSTRAINT [FK_CMExternalCareCenter_ExternalCareCenter];


GO
ALTER TABLE [MixingStation].[CMExternalCareCenter] NOCHECK CONSTRAINT [FK_CMExternalCareCenter_ProductionLine];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMExternalCareCenter', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de atención externo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMExternalCareCenter', @level2type = N'COLUMN', @level2name = N'ExternalCareCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la línea de producción', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMExternalCareCenter', @level2type = N'COLUMN', @level2name = N'ProductionLineId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del cliente', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMExternalCareCenter', @level2type = N'COLUMN', @level2name = N'CustomerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMExternalCareCenter', @level2type = N'COLUMN', @level2name = N'CMConfigurationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMExternalCareCenter', @level2type = N'COLUMN', @level2name = N'Id';

