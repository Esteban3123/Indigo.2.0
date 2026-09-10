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



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable de débito para incapacidades cobradas a EPS (VARCHAR 20); registra los valores de incapacidad a cargo de la entidad de salud.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptAccountingStructure', @level2type = N'COLUMN', @level2name = N'InabilityDebitValueEPSAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta Contable de las Incapacidades que se les cobran a los EPS', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptAccountingStructure', @level2type = N'COLUMN', @level2name = N'InabilityDebitValueEPSAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptAccountingStructure', @level2type = N'COLUMN', @level2name = N'InabilityDebitValueEPSAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable de débito para incapacidades cobradas a empleados (VARCHAR 20); registra los primeros dos días de incapacidad a cargo del trabajador.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptAccountingStructure', @level2type = N'COLUMN', @level2name = N'InabilityDebitValueEmployeeAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta Contable de las Incapacidades que se les cobran a los Empleados. Los dos primeros días del valor de la incapacidad', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptAccountingStructure', @level2type = N'COLUMN', @level2name = N'InabilityDebitValueEmployeeAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptAccountingStructure', @level2type = N'COLUMN', @level2name = N'InabilityDebitValueEmployeeAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del usuario modificador (INT); referencia al usuario que realizó la última modificación; auditoría de cambios.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptAccountingStructure', @level2type = N'COLUMN', @level2name = N'ModificationUserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptAccountingStructure', @level2type = N'COLUMN', @level2name = N'ModificationUserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptAccountingStructure', @level2type = N'COLUMN', @level2name = N'ModificationUserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del usuario creador (INT); referencia al usuario que originó el registro; auditoría de creación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptAccountingStructure', @level2type = N'COLUMN', @level2name = N'CreationUserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptAccountingStructure', @level2type = N'COLUMN', @level2name = N'CreationUserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptAccountingStructure', @level2type = N'COLUMN', @level2name = N'CreationUserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación (DATETIME); marca temporal de auditoría de cambios al registro.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptAccountingStructure', @level2type = N'COLUMN', @level2name = N'ModifiedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptAccountingStructure', @level2type = N'COLUMN', @level2name = N'ModifiedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptAccountingStructure', @level2type = N'COLUMN', @level2name = N'ModifiedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro (DATETIME); marca temporal de auditoría del registro inicial en la tabla.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptAccountingStructure', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptAccountingStructure', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptAccountingStructure', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la interfaz o contenedor (VARCHAR 50); identifica el destino de exportación o integración contable de los datos de nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptAccountingStructure', @level2type = N'COLUMN', @level2name = N'InterfazName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la Interfaz (Contenedor)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptAccountingStructure', @level2type = N'COLUMN', @level2name = N'InterfazName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptAccountingStructure', @level2type = N'COLUMN', @level2name = N'InterfazName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de cuenta contable de crédito (VARCHAR 20); cuenta donde se registran las deducciones o descuentos del concepto de nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptAccountingStructure', @level2type = N'COLUMN', @level2name = N'DeductedAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta Crédito', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptAccountingStructure', @level2type = N'COLUMN', @level2name = N'DeductedAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptAccountingStructure', @level2type = N'COLUMN', @level2name = N'DeductedAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de cuenta contable de débito (VARCHAR 20); cuenta donde se registran los valores devengados o acumulados del concepto de nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptAccountingStructure', @level2type = N'COLUMN', @level2name = N'AccruedAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuente Débito', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptAccountingStructure', @level2type = N'COLUMN', @level2name = N'AccruedAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptAccountingStructure', @level2type = N'COLUMN', @level2name = N'AccruedAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK, INT) que referencia el Id de la estructura contable en Payroll.AccountingStructure; define el plan de cuentas contables aplicable.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptAccountingStructure', @level2type = N'COLUMN', @level2name = N'AccountingStructureId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'FK Id Estructura Contable', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptAccountingStructure', @level2type = N'COLUMN', @level2name = N'AccountingStructureId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptAccountingStructure', @level2type = N'COLUMN', @level2name = N'AccountingStructureId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK, INT) que referencia el Id del concepto de nómina en Payroll.Concept; vincula el concepto salarial o prestacional al mapeo contable.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptAccountingStructure', @level2type = N'COLUMN', @level2name = N'ConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'FK Id Concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptAccountingStructure', @level2type = N'COLUMN', @level2name = N'ConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptAccountingStructure', @level2type = N'COLUMN', @level2name = N'ConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) del registro de concepto en estructura contable; clave primaria de la tabla ConceptAccountingStructure.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptAccountingStructure', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Conceptos Estructura Contable', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptAccountingStructure', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptAccountingStructure', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona cada concepto de nómina con su estructura contable, definiendo las cuentas contables de causación y deducción que se deben afectar al generar la interfaz contable de la nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptAccountingStructure';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptAccountingStructure';
