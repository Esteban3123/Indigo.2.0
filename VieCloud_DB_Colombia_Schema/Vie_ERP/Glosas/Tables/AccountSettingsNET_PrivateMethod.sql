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



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concepto de conciliación (VARCHAR(5)). Indica si la glosa permite proceso de conciliación entre acreedor y deudor.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PrivateMethod', @level2type = N'COLUMN', @level2name = N'Conciliation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concepto de conciliacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PrivateMethod', @level2type = N'COLUMN', @level2name = N'Conciliation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PrivateMethod', @level2type = N'COLUMN', @level2name = N'Conciliation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable para traslado de cobro en proceso juridico (VARCHAR(20)). Referencia contable cuando la glosa escala a vía legal.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PrivateMethod', @level2type = N'COLUMN', @level2name = N'LegalProcess';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'cuenta contable traslado cobro juridico', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PrivateMethod', @level2type = N'COLUMN', @level2name = N'LegalProcess';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PrivateMethod', @level2type = N'COLUMN', @level2name = N'LegalProcess';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concepto de glosa subsanable o rectificable (VARCHAR(5)). Marca si la glosa admite corrección o ajuste por parte del acreedor.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PrivateMethod', @level2type = N'COLUMN', @level2name = N'RectifiableGlosa';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concepto de glosa subsanable', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PrivateMethod', @level2type = N'COLUMN', @level2name = N'RectifiableGlosa';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PrivateMethod', @level2type = N'COLUMN', @level2name = N'RectifiableGlosa';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de cuenta o factura radicada en sistema (VARCHAR(20)). Identificación de factura que ha completado trámite de radicación.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PrivateMethod', @level2type = N'COLUMN', @level2name = N'InvoiceRadicate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta radicada', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PrivateMethod', @level2type = N'COLUMN', @level2name = N'InvoiceRadicate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PrivateMethod', @level2type = N'COLUMN', @level2name = N'InvoiceRadicate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de factura no radicada en sistema (VARCHAR(20)). Factura pendiente de radicación o que no cumplió requisitos de ingreso.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PrivateMethod', @level2type = N'COLUMN', @level2name = N'InvoiceNotRadicate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Factura no radicada', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PrivateMethod', @level2type = N'COLUMN', @level2name = N'InvoiceNotRadicate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PrivateMethod', @level2type = N'COLUMN', @level2name = N'InvoiceNotRadicate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de la agrupación según norma RIPS 1121 (VARCHAR(100)). Clasificación del método privado para glosas y facturación.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PrivateMethod', @level2type = N'COLUMN', @level2name = N'Denomination';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la agrupacion norma 1121', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PrivateMethod', @level2type = N'COLUMN', @level2name = N'Denomination';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PrivateMethod', @level2type = N'COLUMN', @level2name = N'Denomination';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autonumérico (INT, FK a GlosasParametersInterface). Referencia a configuración de interfaces version NET - Método Privado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PrivateMethod', @level2type = N'COLUMN', @level2name = N'ParametersInterfaceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la configuracion de interfaces version NET - Metodo Privado ', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PrivateMethod', @level2type = N'COLUMN', @level2name = N'ParametersInterfaceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PrivateMethod', @level2type = N'COLUMN', @level2name = N'ParametersInterfaceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autonumérico de tabla (INT IDENTITY). Identificador único de configuración de método privado en glosas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PrivateMethod', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PrivateMethod', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PrivateMethod', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración de métodos privados para la liquidación y gestión de cuentas en el módulo de glosas. Define los parámetros que controlan el comportamiento del proceso de glosas según el estado de las facturas: no radicadas, radicadas, con posibilidad de rectificación, en proceso legal o en conciliación.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PrivateMethod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsNET_PrivateMethod';
