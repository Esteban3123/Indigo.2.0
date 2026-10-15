CREATE TABLE [Glosas].[AccountSettingsNET_PrivateMethod] (
    [Id]                    INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ParametersInterfaceId] INT           NULL,
    [Denomination]          VARCHAR (100) NOT NULL,
    [InvoiceNotRadicate]    VARCHAR (20)  NULL,
    [InvoiceRadicate]       VARCHAR (20)  NULL,
    [RectifiableGlosa]      VARCHAR (5)   NULL,
    [LegalProcess]          VARCHAR (20)  NULL,
    [Conciliation]          VARCHAR (5)   NULL,
    CONSTRAINT [PK_AccountSettingsNET_PrivateMethod] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountSettingsNET_PrivateMethod_GlosasParametersInterface] FOREIGN KEY ([ParametersInterfaceId]) REFERENCES [Glosas].[GlosasParametersInterface] ([Id])
);


GO
ALTER TABLE [Glosas].[AccountSettingsNET_PrivateMethod] NOCHECK CONSTRAINT [FK_AccountSettingsNET_PrivateMethod_GlosasParametersInterface];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concepto de conciliacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PrivateMethod', @level2type = N'COLUMN', @level2name = N'Conciliation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'cuenta contable traslado cobro juridico', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PrivateMethod', @level2type = N'COLUMN', @level2name = N'LegalProcess';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concepto de glosa subsanable', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PrivateMethod', @level2type = N'COLUMN', @level2name = N'RectifiableGlosa';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta radicada', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PrivateMethod', @level2type = N'COLUMN', @level2name = N'InvoiceRadicate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Factura no radicada', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PrivateMethod', @level2type = N'COLUMN', @level2name = N'InvoiceNotRadicate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de la agrupacion norma 1121', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PrivateMethod', @level2type = N'COLUMN', @level2name = N'Denomination';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la configuracion de interfaces version NET - Metodo Privado ', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PrivateMethod', @level2type = N'COLUMN', @level2name = N'ParametersInterfaceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PrivateMethod', @level2type = N'COLUMN', @level2name = N'Id';

