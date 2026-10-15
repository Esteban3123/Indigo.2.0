CREATE TABLE [Payroll].[ConceptAccountingStructure] (
    [Id]                                 INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ConceptId]                          INT          NULL,
    [AccountingStructureId]              INT          NULL,
    [AccruedAccount]                     VARCHAR (20) NULL,
    [DeductedAccount]                    VARCHAR (20) NULL,
    [InterfazName]                       VARCHAR (50) NULL,
    [CreationDate]                       DATETIME     NULL,
    [ModifiedDate]                       DATETIME     NULL,
    [CreationUserId]                     INT          NULL,
    [ModificationUserId]                 INT          NULL,
    [InabilityDebitValueEmployeeAccount] VARCHAR (20) NULL,
    [InabilityDebitValueEPSAccount]      VARCHAR (20) NULL,
    CONSTRAINT [PK_ConceptAccountingStructure] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ConceptAccountingStructure_AccountingStructure] FOREIGN KEY ([AccountingStructureId]) REFERENCES [Payroll].[AccountingStructure] ([Id]),
    CONSTRAINT [FK_ConceptAccountingStructure_Concept] FOREIGN KEY ([ConceptId]) REFERENCES [Payroll].[Concept] ([Id])
);


GO
ALTER TABLE [Payroll].[ConceptAccountingStructure] NOCHECK CONSTRAINT [FK_ConceptAccountingStructure_AccountingStructure];


GO
ALTER TABLE [Payroll].[ConceptAccountingStructure] NOCHECK CONSTRAINT [FK_ConceptAccountingStructure_Concept];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta Contable de las Incapacidades que se les cobran a los EPS', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptAccountingStructure', @level2type = N'COLUMN', @level2name = N'InabilityDebitValueEPSAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta Contable de las Incapacidades que se les cobran a los Empleados. Los dos primeros días del valor de la incapacidad', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptAccountingStructure', @level2type = N'COLUMN', @level2name = N'InabilityDebitValueEmployeeAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptAccountingStructure', @level2type = N'COLUMN', @level2name = N'ModificationUserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptAccountingStructure', @level2type = N'COLUMN', @level2name = N'CreationUserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptAccountingStructure', @level2type = N'COLUMN', @level2name = N'ModifiedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptAccountingStructure', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la Interfaz (Contenedor)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptAccountingStructure', @level2type = N'COLUMN', @level2name = N'InterfazName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta Crédito', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptAccountingStructure', @level2type = N'COLUMN', @level2name = N'DeductedAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuente Débito', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptAccountingStructure', @level2type = N'COLUMN', @level2name = N'AccruedAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FK Id Estructura Contable', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptAccountingStructure', @level2type = N'COLUMN', @level2name = N'AccountingStructureId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FK Id Concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptAccountingStructure', @level2type = N'COLUMN', @level2name = N'ConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Conceptos Estructura Contable', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptAccountingStructure', @level2type = N'COLUMN', @level2name = N'Id';

