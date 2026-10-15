CREATE TABLE [Payroll].[GroupAdjustConceptContractLiquidation] (
    [Id]           INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdGroup]      INT          NOT NULL,
    [IdConcept]    INT          NOT NULL,
    [ConceptType]  TINYINT      NOT NULL,
    [CreationUser] VARCHAR (20) NOT NULL,
    [CreationDate] DATETIME     NOT NULL,
    CONSTRAINT [PK_GroupAdjustConceptContractLiquidation] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GroupAdjustConceptContractLiquidation_Concept] FOREIGN KEY ([IdConcept]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_GroupAdjustConceptContractLiquidation_Group] FOREIGN KEY ([IdGroup]) REFERENCES [Payroll].[Group] ([Id])
);


GO
ALTER TABLE [Payroll].[GroupAdjustConceptContractLiquidation] NOCHECK CONSTRAINT [FK_GroupAdjustConceptContractLiquidation_Concept];


GO
ALTER TABLE [Payroll].[GroupAdjustConceptContractLiquidation] NOCHECK CONSTRAINT [FK_GroupAdjustConceptContractLiquidation_Group];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'GroupAdjustConceptContractLiquidation', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'GroupAdjustConceptContractLiquidation', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Concepto:  1. Sueldo  2. Auxilio de Transporte  3. Aporte Salud  4. Aporte Pensión  5. Fondo de Solidaridad Pensional', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'GroupAdjustConceptContractLiquidation', @level2type = N'COLUMN', @level2name = N'ConceptType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'GroupAdjustConceptContractLiquidation', @level2type = N'COLUMN', @level2name = N'IdConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Grupo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'GroupAdjustConceptContractLiquidation', @level2type = N'COLUMN', @level2name = N'IdGroup';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'GroupAdjustConceptContractLiquidation', @level2type = N'COLUMN', @level2name = N'Id';

