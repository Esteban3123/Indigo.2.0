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



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concepto contable de conciliación; código o cuenta para registrar acuerdos entre entidades sobre glosas, facturación o cobro.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PrivateMethod', @level2type = N'COLUMN', @level2name = N'Conciliation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'concepto de conciliacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PrivateMethod', @level2type = N'COLUMN', @level2name = N'Conciliation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PrivateMethod', @level2type = N'COLUMN', @level2name = N'Conciliation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable de traslado a cobro jurídico; identifica traslados de facturas o glosas a proceso legal o cobranza externa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PrivateMethod', @level2type = N'COLUMN', @level2name = N'LegalProcess';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'cuenta contable de traslado a cobro juridico', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PrivateMethod', @level2type = N'COLUMN', @level2name = N'LegalProcess';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PrivateMethod', @level2type = N'COLUMN', @level2name = N'LegalProcess';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concepto para glosas subsanables o rectificables; indica si la glosa permite corrección o ajuste antes de cobro jurídico.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PrivateMethod', @level2type = N'COLUMN', @level2name = N'RectifiableGlosa';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'concepto para las glosas subsanables', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PrivateMethod', @level2type = N'COLUMN', @level2name = N'RectifiableGlosa';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PrivateMethod', @level2type = N'COLUMN', @level2name = N'RectifiableGlosa';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable para factura radicada; código contable asignado a facturas oficialmente presentadas o registradas en el sistema.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PrivateMethod', @level2type = N'COLUMN', @level2name = N'InvoiceRadicate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'cuenta contable factura radicada', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PrivateMethod', @level2type = N'COLUMN', @level2name = N'InvoiceRadicate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PrivateMethod', @level2type = N'COLUMN', @level2name = N'InvoiceRadicate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable para factura sin radicar; código contable para facturas pendientes de registro o presentación oficial.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PrivateMethod', @level2type = N'COLUMN', @level2name = N'InvoiceNotRadicate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta contable factura sin radicar', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PrivateMethod', @level2type = N'COLUMN', @level2name = N'InvoiceNotRadicate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PrivateMethod', @level2type = N'COLUMN', @level2name = N'InvoiceNotRadicate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o denominación de la agrupación de cuenta según norma 1121 de interfaces; agrupa cuentas contables para método privado FOX.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PrivateMethod', @level2type = N'COLUMN', @level2name = N'Denomination';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la agrupacion de cuenta norma 1121 de interfaces version FOX - Metodo Privado ', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PrivateMethod', @level2type = N'COLUMN', @level2name = N'Denomination';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PrivateMethod', @level2type = N'COLUMN', @level2name = N'Denomination';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de configuración de interfaz; clave foránea que referencia parámetros de interfaz de glosas (GlosasParametersInterface).', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PrivateMethod', @level2type = N'COLUMN', @level2name = N'ParametersInterfaceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo configuracion de interface', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PrivateMethod', @level2type = N'COLUMN', @level2name = N'ParametersInterfaceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PrivateMethod', @level2type = N'COLUMN', @level2name = N'ParametersInterfaceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (autoincremental) de la configuración de interfaz para método privado FOX; clave primaria de la tabla.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PrivateMethod', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la configuracion de interfaces version FOX - Metodo Privado ', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PrivateMethod', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PrivateMethod', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración de métodos privados de liquidación de cuentas para el módulo de Glosas (FOX). Almacena los parámetros que definen cómo se clasifican y gestionan las facturas según su estado de radicación, los tipos de glosa rectificable, los procesos legales y las conciliaciones en la gestión de cartera privada.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PrivateMethod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AccountSettingsFOX_PrivateMethod';
