CREATE TABLE [Payments].[AccountPayableDetailConceptLiquidationAdjusments] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [LiquidationId]    INT           NOT NULL,
    [ConceptType]      TINYINT       NOT NULL,
    [Nature]           TINYINT       NOT NULL,
    [Value]            DECIMAL (18)  NOT NULL,
    [Observations]     VARCHAR (500) NULL,
    [Status]           TINYINT       NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [TotalIncome]      DECIMAL (18)  CONSTRAINT [DF_AccountPayableDetailConceptLiquidationAdjusments_TotalIncome] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_AccountPayableDetailConceptLiquidationAdjusments__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountPayableDetailConceptLiquidationAdjusments_LiquidationId] FOREIGN KEY ([LiquidationId]) REFERENCES [Payments].[AccountPayableDetailConceptLiquidation] ([Id])
);


GO
ALTER TABLE [Payments].[AccountPayableDetailConceptLiquidationAdjusments] NOCHECK CONSTRAINT [FK_AccountPayableDetailConceptLiquidationAdjusments_LiquidationId];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ingresos totales', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidationAdjusments', @level2type = N'COLUMN', @level2name = N'TotalIncome';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidationAdjusments', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que modifico', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidationAdjusments', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidationAdjusments', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de creación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidationAdjusments', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidationAdjusments', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidationAdjusments', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del movimiento', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidationAdjusments', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Muestra la naturaleza del movimento para el concepto:  1. Débito (El monto registrado AUMENTA el valor del concepto)  2. Crédito (El monto registrado DISMINUYE el valor del concepto)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidationAdjusments', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de concepto de retención:    1 - Renta exenta   2 - Total rentas exentas y deducciones', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidationAdjusments', @level2type = N'COLUMN', @level2name = N'ConceptType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del liquidador de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidationAdjusments', @level2type = N'COLUMN', @level2name = N'LiquidationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro (Saldo inicial)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConceptLiquidationAdjusments', @level2type = N'COLUMN', @level2name = N'Id';

