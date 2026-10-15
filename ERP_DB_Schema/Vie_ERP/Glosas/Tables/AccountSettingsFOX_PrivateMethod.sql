CREATE TABLE [Glosas].[AccountSettingsFOX_PrivateMethod] (
    [Id]                    INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ParametersInterfaceId] INT           NULL,
    [Denomination]          VARCHAR (100) NOT NULL,
    [InvoiceNotRadicate]    VARCHAR (20)  NULL,
    [InvoiceRadicate]       VARCHAR (20)  NULL,
    [RectifiableGlosa]      VARCHAR (5)   NULL,
    [LegalProcess]          VARCHAR (20)  NULL,
    [Conciliation]          VARCHAR (5)   NULL,
    CONSTRAINT [PK_AccountSettingsFOX_PrivateMethod] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountSettingsFOX_PrivateMethod_GlosasParametersInterface] FOREIGN KEY ([ParametersInterfaceId]) REFERENCES [Glosas].[GlosasParametersInterface] ([Id])
);


GO
ALTER TABLE [Glosas].[AccountSettingsFOX_PrivateMethod] NOCHECK CONSTRAINT [FK_AccountSettingsFOX_PrivateMethod_GlosasParametersInterface];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'concepto de conciliacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PrivateMethod', @level2type = N'COLUMN', @level2name = N'Conciliation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'cuenta contable de traslado a cobro juridico', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PrivateMethod', @level2type = N'COLUMN', @level2name = N'LegalProcess';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'concepto para las glosas subsanables', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PrivateMethod', @level2type = N'COLUMN', @level2name = N'RectifiableGlosa';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'cuenta contable factura radicada', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PrivateMethod', @level2type = N'COLUMN', @level2name = N'InvoiceRadicate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta contable factura sin radicar', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PrivateMethod', @level2type = N'COLUMN', @level2name = N'InvoiceNotRadicate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la agrupacion de cuenta norma 1121 de interfaces version FOX - Metodo Privado ', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PrivateMethod', @level2type = N'COLUMN', @level2name = N'Denomination';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo configuracion de interface', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PrivateMethod', @level2type = N'COLUMN', @level2name = N'ParametersInterfaceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la configuracion de interfaces version FOX - Metodo Privado ', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PrivateMethod', @level2type = N'COLUMN', @level2name = N'Id';

