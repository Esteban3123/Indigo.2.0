CREATE TABLE [Glosas].[AccountSettingsNET_PublicMethod] (
    [Id]                    INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ParametersInterfaceId] INT           NULL,
    [PlanCode]              VARCHAR (20)  NOT NULL,
    [Denomination]          VARCHAR (500) NOT NULL,
    [InvoiceRadicate]       VARCHAR (20)  NULL,
    CONSTRAINT [PK_AccountSettingsNET_PublicMethod] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountSettingsNET_PublicMethod_GlosasParametersInterface] FOREIGN KEY ([ParametersInterfaceId]) REFERENCES [Glosas].[GlosasParametersInterface] ([Id])
);


GO
ALTER TABLE [Glosas].[AccountSettingsNET_PublicMethod] NOCHECK CONSTRAINT [FK_AccountSettingsNET_PublicMethod_GlosasParametersInterface];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta de orden facturas radicadas', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PublicMethod', @level2type = N'COLUMN', @level2name = N'InvoiceRadicate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de plan', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PublicMethod', @level2type = N'COLUMN', @level2name = N'Denomination';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Plan de beneficios', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PublicMethod', @level2type = N'COLUMN', @level2name = N'PlanCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo configuracion de interfaces', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PublicMethod', @level2type = N'COLUMN', @level2name = N'ParametersInterfaceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la configuracion de interfaces version NET - Metodo Publico', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PublicMethod', @level2type = N'COLUMN', @level2name = N'Id';

