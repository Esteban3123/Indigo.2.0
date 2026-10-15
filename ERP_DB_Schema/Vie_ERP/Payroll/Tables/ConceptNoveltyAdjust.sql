CREATE TABLE [Payroll].[ConceptNoveltyAdjust] (
    [Id]                INT        IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdPayrollSettings] INT        NOT NULL,
    [IdConcept]         INT        NOT NULL,
    [IdAdjustConcept]   INT        NOT NULL,
    [Timestamp]         ROWVERSION NOT NULL,
    CONSTRAINT [PK_ConceptNoveltyAdjust] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ConceptNoveltyAdjust_Concept] FOREIGN KEY ([IdConcept]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_ConceptNoveltyAdjust_Concept1] FOREIGN KEY ([IdAdjustConcept]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_ConceptNoveltyAdjust_PayrollSettings] FOREIGN KEY ([IdPayrollSettings]) REFERENCES [Payroll].[PayrollSettings] ([Id])
);


GO
ALTER TABLE [Payroll].[ConceptNoveltyAdjust] NOCHECK CONSTRAINT [FK_ConceptNoveltyAdjust_Concept];


GO
ALTER TABLE [Payroll].[ConceptNoveltyAdjust] NOCHECK CONSTRAINT [FK_ConceptNoveltyAdjust_Concept1];


GO
ALTER TABLE [Payroll].[ConceptNoveltyAdjust] NOCHECK CONSTRAINT [FK_ConceptNoveltyAdjust_PayrollSettings];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptNoveltyAdjust', @level2type = N'COLUMN', @level2name = N'Timestamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id concepto del ajuste ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptNoveltyAdjust', @level2type = N'COLUMN', @level2name = N'IdAdjustConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id relacion con conceptos ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptNoveltyAdjust', @level2type = N'COLUMN', @level2name = N'IdConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id. Configuración de nómina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptNoveltyAdjust', @level2type = N'COLUMN', @level2name = N'IdPayrollSettings';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptNoveltyAdjust', @level2type = N'COLUMN', @level2name = N'Id';

