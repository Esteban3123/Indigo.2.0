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
ALTER TABLE [Payroll].[BankDetail] NOCHECK CONSTRAINT [FK_BankDetail_Bank];


GO
ALTER TABLE [Payroll].[BankDetail] NOCHECK CONSTRAINT [FK_BankDetail_BankConciliationConcepts];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Detalle ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankDetail', @level2type = N'COLUMN', @level2name = N'Detail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción o Código del extracto ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankDetail', @level2type = N'COLUMN', @level2name = N'ExtractCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite relacionar la tabla BankConciliationConcepts  ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankDetail', @level2type = N'COLUMN', @level2name = N'BankConciliationConceptsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite relacionar la tabla bank de payroll ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankDetail', @level2type = N'COLUMN', @level2name = N'BankId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankDetail', @level2type = N'COLUMN', @level2name = N'Id';

