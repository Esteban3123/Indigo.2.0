CREATE TABLE [Payroll].[ConceptGroup] (
    [Id]              INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ConceptId]       INT     NOT NULL,
    [GroupId]         INT     NOT NULL,
    [LiquidateBy]     TINYINT NOT NULL,
    [MaximunDiscount] BIT     NOT NULL,
    CONSTRAINT [PK_ConceptGroup] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ConceptGroup_Concept] FOREIGN KEY ([ConceptId]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_ConceptGroup_Group] FOREIGN KEY ([GroupId]) REFERENCES [Payroll].[Group] ([Id])
);


GO
ALTER TABLE [Payroll].[ConceptGroup] NOCHECK CONSTRAINT [FK_ConceptGroup_Concept];


GO
ALTER TABLE [Payroll].[ConceptGroup] NOCHECK CONSTRAINT [FK_ConceptGroup_Group];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descuento maximo 0-No 1-Si', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptGroup', @level2type = N'COLUMN', @level2name = N'MaximunDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Liquidar por: 1 - Grupo Completo; 2 - Seleccionados', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptGroup', @level2type = N'COLUMN', @level2name = N'LiquidateBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FK Id Grupo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptGroup', @level2type = N'COLUMN', @level2name = N'GroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FK Id Concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptGroup', @level2type = N'COLUMN', @level2name = N'ConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Concepto Grupo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptGroup', @level2type = N'COLUMN', @level2name = N'Id';

