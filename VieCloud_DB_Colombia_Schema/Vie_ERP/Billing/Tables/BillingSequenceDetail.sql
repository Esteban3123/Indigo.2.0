CREATE TABLE [Billing].[BillingSequenceDetail] (
    [Id]                 INT    IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdSequenseBillingC] INT    NOT NULL,
    [IdSequense]         INT    NOT NULL,
    [IdOperatingUnit]    INT    NULL,
    [Next]               BIGINT CONSTRAINT [DF_SequenseBillingD_Next] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_SequenseBillingD] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SequenseBillingD_OperatingUnit] FOREIGN KEY ([IdOperatingUnit]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_SequenseBillingD_Sequense] FOREIGN KEY ([IdSequense]) REFERENCES [Common].[Sequense] ([Id]),
    CONSTRAINT [FK_SequenseBillingD_SequenseBillingC] FOREIGN KEY ([IdSequenseBillingC]) REFERENCES [Billing].[BillingSequence] ([Id])
);


GO
ALTER TABLE [Billing].[BillingSequenceDetail] NOCHECK CONSTRAINT [FK_SequenseBillingD_SequenseBillingC];




GO



GO



GO
ALTER TABLE [Billing].[BillingSequenceDetail] NOCHECK CONSTRAINT [FK_SequenseBillingD_SequenseBillingC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Siguiente número secuencial a generar en la facturación; contador incremental (BIGINT) que controla el próximo valor de documento, factura o comprobante en la secuencia de billing', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingSequenceDetail', @level2type = N'COLUMN', @level2name = N'Next';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Siguiente numero a generar con la secuenacia', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingSequenceDetail', @level2type = N'COLUMN', @level2name = N'Next';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingSequenceDetail', @level2type = N'COLUMN', @level2name = N'Next';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad operativa (centro, sede, consultorio) asociada a la secuencia de facturación; solo aplica cuando el ámbito es a nivel de UO (Unidad Operativa), de lo contrario es NULL; referencia a [Common].[OperatingUnit]', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad operativa asignada a la secuencia. Solo cuando el ambito es UO-Unidad Operativa, de lo contrario el campo es nulo', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la secuencia base o maestra; referencia a [Common].[Sequense] que define el patrón de numeración para facturas, recibos, comprobantes de pago o documentos de billing', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequense';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la secuencia base', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequense';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequense';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera o registro principal de secuencia de facturación; referencia a [Billing].[BillingSequence] que agrupa y configura las reglas de generación de números de documento', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequenseBillingC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequenseBillingC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequenseBillingC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK, IDENTITY) del detalle de secuencia de facturación; clave primaria del registro en [Billing].[BillingSequenceDetail]', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingSequenceDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingSequenceDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingSequenceDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las secuencias de facturación: registra el consecutivo siguiente asignado a cada combinación de cabecera de secuencia de facturación y unidad operativa, permitiendo controlar la numeración automática de documentos de cobro por unidad.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingSequenceDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingSequenceDetail';
