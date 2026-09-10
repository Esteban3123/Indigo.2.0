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



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fórmula reemplazada o alternativa de concepto para reemplazo en vacaciones (VARCHAR MAX nullable)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationDetail', @level2type = N'COLUMN', @level2name = N'ReplaceConceptFormulate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Formula Reemplazada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationDetail', @level2type = N'COLUMN', @level2name = N'ReplaceConceptFormulate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationDetail', @level2type = N'COLUMN', @level2name = N'ReplaceConceptFormulate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fórmula del concepto de nómina usada para cálculo de vacaciones (VARCHAR MAX nullable)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationDetail', @level2type = N'COLUMN', @level2name = N'ConceptFormulate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Formula del Concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationDetail', @level2type = N'COLUMN', @level2name = N'ConceptFormulate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationDetail', @level2type = N'COLUMN', @level2name = N'ConceptFormulate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor deducido o descuento aplicado al concepto de vacaciones (NUMERIC 18, obligatorio)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationDetail', @level2type = N'COLUMN', @level2name = N'Deducted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Deducido', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationDetail', @level2type = N'COLUMN', @level2name = N'Deducted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationDetail', @level2type = N'COLUMN', @level2name = N'Deducted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor devengado, monto acumulado de vacaciones en período (NUMERIC 18, obligatorio)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationDetail', @level2type = N'COLUMN', @level2name = N'Accrued';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Devengado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationDetail', @level2type = N'COLUMN', @level2name = N'Accrued';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationDetail', @level2type = N'COLUMN', @level2name = N'Accrued';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o descripción del concepto de vacaciones (VARCHAR 500, obligatorio)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationDetail', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del Concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationDetail', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationDetail', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del Concepto de nómina, referencia a Payroll.Concept (INT FK nullable)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationDetail', @level2type = N'COLUMN', @level2name = N'IdConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationDetail', @level2type = N'COLUMN', @level2name = N'IdConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationDetail', @level2type = N'COLUMN', @level2name = N'IdConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de Vacaciones, referencia a Payroll.Vacation (INT FK, clave foránea)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationDetail', @level2type = N'COLUMN', @level2name = N'IdVacation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Vacaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationDetail', @level2type = N'COLUMN', @level2name = N'IdVacation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationDetail', @level2type = N'COLUMN', @level2name = N'IdVacation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la fila en VacationDetail (INT IDENTITY, clave primaria)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los conceptos de nómina asociados a cada liquidación de vacaciones: registra los devengados y deducidos por concepto (salario, auxilio, prestaciones, etc.) que componen el pago o descuento de vacaciones de un empleado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationDetail';
