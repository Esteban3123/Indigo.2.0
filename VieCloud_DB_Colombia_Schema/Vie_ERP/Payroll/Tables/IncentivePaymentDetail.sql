CREATE TABLE [Payroll].[IncentivePaymentDetail] (
    [Id]                      INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IncentivePaymentId]      INT           NOT NULL,
    [ConceptId]               INT           NOT NULL,
    [AccruedValue]            NUMERIC (18)  NOT NULL,
    [DeductedValue]           NUMERIC (18)  NOT NULL,
    [ConceptFormulate]        VARCHAR (MAX) NULL,
    [ReplaceConceptFormulate] VARCHAR (MAX) NULL,
    CONSTRAINT [PK_IncentivePaymentDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_IncentivePaymentDetail_Concept] FOREIGN KEY ([ConceptId]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_IncentivePaymentDetail_IncentivePayment] FOREIGN KEY ([IncentivePaymentId]) REFERENCES [Payroll].[IncentivePayment] ([Id])
);

GO
CREATE NONCLUSTERED INDEX [IX_IncentivePaymentDetail_IncentivePaymentId_ConceptId]
    ON [Payroll].[IncentivePaymentDetail] ([IncentivePaymentId] ASC, [ConceptId] ASC)
    INCLUDE ([AccruedValue], [DeductedValue]);

GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fórmula de concepto de reemplazo o sustitución; VARCHAR(MAX) que almacena la expresión matemática o lógica alternativa para recalcular el concepto de incentivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePaymentDetail', @level2type = N'COLUMN', @level2name = N'ReplaceConceptFormulate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Reemplazar Concepto Formular', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePaymentDetail', @level2type = N'COLUMN', @level2name = N'ReplaceConceptFormulate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePaymentDetail', @level2type = N'COLUMN', @level2name = N'ReplaceConceptFormulate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fórmula o expresión del concepto de incentivo; VARCHAR(MAX) que define la regla matemática o lógica base para calcular el valor devengado del concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePaymentDetail', @level2type = N'COLUMN', @level2name = N'ConceptFormulate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concepto Formular', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePaymentDetail', @level2type = N'COLUMN', @level2name = N'ConceptFormulate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePaymentDetail', @level2type = N'COLUMN', @level2name = N'ConceptFormulate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor deducido, descuento o retención aplicado al concepto; NUMERIC(18,0) que representa montos restados (impuestos, aportes, descuentos) en la liquidación de incentivos', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePaymentDetail', @level2type = N'COLUMN', @level2name = N'DeductedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Deducido', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePaymentDetail', @level2type = N'COLUMN', @level2name = N'DeductedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePaymentDetail', @level2type = N'COLUMN', @level2name = N'DeductedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor devengado, acumulado o causado del concepto; NUMERIC(18,0) que registra el monto bruto generado antes de deducciones en el período de incentivos', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePaymentDetail', @level2type = N'COLUMN', @level2name = N'AccruedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Devengado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePaymentDetail', @level2type = N'COLUMN', @level2name = N'AccruedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePaymentDetail', @level2type = N'COLUMN', @level2name = N'AccruedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de nómina/incentivo; INT (FK a Payroll.Concept) que referencia la definición del rubro, componente salarial o bonificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePaymentDetail', @level2type = N'COLUMN', @level2name = N'ConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePaymentDetail', @level2type = N'COLUMN', @level2name = N'ConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePaymentDetail', @level2type = N'COLUMN', @level2name = N'ConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera de pago de incentivos; INT (FK a Payroll.IncentivePayment) que vincula el detalle al documento maestro de liquidación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePaymentDetail', @level2type = N'COLUMN', @level2name = N'IncentivePaymentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Cabecera', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePaymentDetail', @level2type = N'COLUMN', @level2name = N'IncentivePaymentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePaymentDetail', @level2type = N'COLUMN', @level2name = N'IncentivePaymentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del detalle de pago de incentivos; INT identity (clave primaria) que identifica unívocamente cada línea de concepto en la liquidación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePaymentDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePaymentDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePaymentDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los conceptos de pago asociados a cada incentivo de nómina, registrando los valores devengados y deducidos por concepto, junto con las fórmulas de liquidación originales y sus reemplazos.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePaymentDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePaymentDetail';
