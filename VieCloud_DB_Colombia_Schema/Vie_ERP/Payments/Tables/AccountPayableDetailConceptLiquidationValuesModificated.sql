CREATE TABLE [Payments].[AccountPayableDetailConceptLiquidationValuesModificated] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [LiquidationId] INT           NOT NULL,
    [ConceptType]   TINYINT       NOT NULL,
    [PreviousValue] DECIMAL (18)  NOT NULL,
    [NewValue]      DECIMAL (18)  NOT NULL,
    [Observations]  VARCHAR (500) NULL,
    [CreationUser]  VARCHAR (20)  NOT NULL,
    [CreationDate]  DATETIME      NOT NULL,
    CONSTRAINT [PK_AccountPayableDetailConceptLiquidationValuesModificated__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountPayableDetailConceptLiquidationValuesModificated_LiquidationId] FOREIGN KEY ([LiquidationId]) REFERENCES [Payments].[AccountPayableDetailConceptLiquidation] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) exacta en que se modificó el valor; timestamp de auditoría del cambio', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidationValuesModificated', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación del valor', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidationValuesModificated', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidationValuesModificated', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que realizó la modificación del valor; identificación del operario o profesional responsable', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidationValuesModificated', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que modificó el valor', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidationValuesModificated', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidationValuesModificated', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones o notas (VARCHAR 500) que justifican o documentan la razón del cambio de valor en la liquidación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidationValuesModificated', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidationValuesModificated', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidationValuesModificated', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor nuevo (DECIMAL 18) del concepto después de la modificación; monto ajustado en pesos de la retención o deducción', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidationValuesModificated', @level2type = N'COLUMN', @level2name = N'NewValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor nuevo', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidationValuesModificated', @level2type = N'COLUMN', @level2name = N'NewValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidationValuesModificated', @level2type = N'COLUMN', @level2name = N'NewValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor anterior (DECIMAL 18) del concepto antes de la modificación; monto original en pesos de la retención o deducción', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidationValuesModificated', @level2type = N'COLUMN', @level2name = N'PreviousValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor anterior', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidationValuesModificated', @level2type = N'COLUMN', @level2name = N'PreviousValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidationValuesModificated', @level2type = N'COLUMN', @level2name = N'PreviousValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de concepto de retención (TINYINT): 1=Renta exenta, 2=Total rentas exentas y deducciones; clasifica el tipo de deducción o retención fiscal aplicada', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidationValuesModificated', @level2type = N'COLUMN', @level2name = N'ConceptType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de concepto de retención:    1 - Renta exenta   2 - Total rentas exentas y deducciones', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidationValuesModificated', @level2type = N'COLUMN', @level2name = N'ConceptType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidationValuesModificated', @level2type = N'COLUMN', @level2name = N'ConceptType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de la liquidación de cuenta por pagar asociada (FK a AccountPayableDetailConceptLiquidation), referencia al documento/factura liquidado', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidationValuesModificated', @level2type = N'COLUMN', @level2name = N'LiquidationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del liquidador', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidationValuesModificated', @level2type = N'COLUMN', @level2name = N'LiquidationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidationValuesModificated', @level2type = N'COLUMN', @level2name = N'LiquidationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) del registro de modificación de valores en liquidación de cuentas por pagar', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidationValuesModificated', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidationValuesModificated', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidationValuesModificated', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de modificaciones aplicadas a los valores de conceptos en liquidaciones de cuentas por pagar. Guarda el historial de cambios (valor anterior y nuevo) realizados sobre cada concepto de una liquidación, con el usuario y la fecha en que se efectuó la modificación.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidationValuesModificated';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidationValuesModificated';
