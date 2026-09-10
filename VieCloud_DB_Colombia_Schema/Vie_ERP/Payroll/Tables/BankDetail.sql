CREATE TABLE [Payroll].[BankDetail] (
    [Id]                         SMALLINT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [BankId]                     INT          NOT NULL,
    [BankConciliationConceptsId] INT          NOT NULL,
    [ExtractCode]                VARCHAR (60) NOT NULL,
    [Detail]                     VARCHAR (60) NULL,
    CONSTRAINT [PK_BankDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BankDetail_Bank] FOREIGN KEY ([BankId]) REFERENCES [Payroll].[Bank] ([Id]),
    CONSTRAINT [FK_BankDetail_BankConciliationConcepts] FOREIGN KEY ([BankConciliationConceptsId]) REFERENCES [Treasury].[BankConciliationConcepts] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle descriptivo adicional del concepto de conciliación bancaria; texto libre para anotaciones o especificaciones del movimiento bancario (VARCHAR 60, opcional)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankDetail', @level2type = N'COLUMN', @level2name = N'Detail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Detalle ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankDetail', @level2type = N'COLUMN', @level2name = N'Detail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankDetail', @level2type = N'COLUMN', @level2name = N'Detail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o descripción del extracto bancario; identificador único del movimiento en el estado de cuenta del banco (VARCHAR 60, obligatorio)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankDetail', @level2type = N'COLUMN', @level2name = N'ExtractCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción o Código del extracto ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankDetail', @level2type = N'COLUMN', @level2name = N'ExtractCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankDetail', @level2type = N'COLUMN', @level2name = N'ExtractCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de concepto de conciliación bancaria; FK a Treasury.BankConciliationConcepts para clasificar el tipo de movimiento (pago, depósito, deducción, ajuste) (INT, obligatorio)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankDetail', @level2type = N'COLUMN', @level2name = N'BankConciliationConceptsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite relacionar la tabla BankConciliationConcepts  ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankDetail', @level2type = N'COLUMN', @level2name = N'BankConciliationConceptsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankDetail', @level2type = N'COLUMN', @level2name = N'BankConciliationConceptsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del banco; FK a Payroll.Bank para vincular el detalle de extracto a la entidad bancaria correspondiente (INT, obligatorio)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankDetail', @level2type = N'COLUMN', @level2name = N'BankId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite relacionar la tabla bank de payroll ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankDetail', @level2type = N'COLUMN', @level2name = N'BankId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankDetail', @level2type = N'COLUMN', @level2name = N'BankId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (SMALLINT IDENTITY, clave primaria); generado automáticamente para cada registro de detalle bancario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de configuración bancaria para nómina: asocia cada banco con los conceptos de conciliación y los códigos de extracto utilizados en el proceso de pago de nómina y transferencias bancarias.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankDetail';
