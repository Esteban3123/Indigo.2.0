CREATE TABLE [Payroll].[VacationDetail] (
    [Id]                      INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdVacation]              INT           NOT NULL,
    [IdConcept]               INT           NULL,
    [Description]             VARCHAR (500) NOT NULL,
    [Accrued]                 NUMERIC (18)  NOT NULL,
    [Deducted]                NUMERIC (18)  NOT NULL,
    [ConceptFormulate]        VARCHAR (MAX) NULL,
    [ReplaceConceptFormulate] VARCHAR (MAX) NULL,
    CONSTRAINT [PK_VacationDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_VacationDetail_Concept] FOREIGN KEY ([IdConcept]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_VacationDetail_Vacation] FOREIGN KEY ([IdVacation]) REFERENCES [Payroll].[Vacation] ([Id])
);


GO
ALTER TABLE [Payroll].[VacationDetail] NOCHECK CONSTRAINT [FK_VacationDetail_Concept];


GO
ALTER TABLE [Payroll].[VacationDetail] NOCHECK CONSTRAINT [FK_VacationDetail_Vacation];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Formula Reemplazada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationDetail', @level2type = N'COLUMN', @level2name = N'ReplaceConceptFormulate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Formula del Concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationDetail', @level2type = N'COLUMN', @level2name = N'ConceptFormulate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Deducido', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationDetail', @level2type = N'COLUMN', @level2name = N'Deducted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Devengado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationDetail', @level2type = N'COLUMN', @level2name = N'Accrued';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del Concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationDetail', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationDetail', @level2type = N'COLUMN', @level2name = N'IdConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Vacaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationDetail', @level2type = N'COLUMN', @level2name = N'IdVacation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationDetail', @level2type = N'COLUMN', @level2name = N'Id';

