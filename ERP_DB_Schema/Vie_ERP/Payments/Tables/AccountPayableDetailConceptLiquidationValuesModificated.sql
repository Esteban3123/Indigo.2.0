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
ALTER TABLE [Payments].[AccountPayableDetailConceptLiquidationValuesModificated] NOCHECK CONSTRAINT [FK_AccountPayableDetailConceptLiquidationValuesModificated_LiquidationId];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de modificación del valor', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidationValuesModificated', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que modificó el valor', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidationValuesModificated', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidationValuesModificated', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor nuevo', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidationValuesModificated', @level2type = N'COLUMN', @level2name = N'NewValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor anterior', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidationValuesModificated', @level2type = N'COLUMN', @level2name = N'PreviousValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de concepto de retención:    1 - Renta exenta   2 - Total rentas exentas y deducciones', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidationValuesModificated', @level2type = N'COLUMN', @level2name = N'ConceptType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del liquidador', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidationValuesModificated', @level2type = N'COLUMN', @level2name = N'LiquidationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidationValuesModificated', @level2type = N'COLUMN', @level2name = N'Id';

