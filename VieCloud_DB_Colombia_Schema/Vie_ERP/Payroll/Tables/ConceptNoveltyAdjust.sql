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



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal (TIMESTAMP) que registra automáticamente el instante exacto de creación, modificación o actualización del registro de ajuste de concepto; facilita auditoría y trazabilidad de cambios en novedades de nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptNoveltyAdjust', @level2type = N'COLUMN', @level2name = N'Timestamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptNoveltyAdjust', @level2type = N'COLUMN', @level2name = N'Timestamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptNoveltyAdjust', @level2type = N'COLUMN', @level2name = N'Timestamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) que referencia el concepto de ajuste (Payroll.Concept); concepto complementario o de compensación usado para ajustar, corregir o modificar el concepto base en novedades de nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptNoveltyAdjust', @level2type = N'COLUMN', @level2name = N'IdAdjustConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id concepto del ajuste ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptNoveltyAdjust', @level2type = N'COLUMN', @level2name = N'IdAdjustConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptNoveltyAdjust', @level2type = N'COLUMN', @level2name = N'IdAdjustConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) que referencia el concepto base de nómina (Payroll.Concept); concepto original o principal al que se aplica la novedad o ajuste.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptNoveltyAdjust', @level2type = N'COLUMN', @level2name = N'IdConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id relacion con conceptos ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptNoveltyAdjust', @level2type = N'COLUMN', @level2name = N'IdConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptNoveltyAdjust', @level2type = N'COLUMN', @level2name = N'IdConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) que referencia la configuración de nómina (Payroll.PayrollSettings); determina el contexto o perfil de configuración aplicable al ajuste de concepto.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptNoveltyAdjust', @level2type = N'COLUMN', @level2name = N'IdPayrollSettings';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id. Configuración de nómina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptNoveltyAdjust', @level2type = N'COLUMN', @level2name = N'IdPayrollSettings';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptNoveltyAdjust', @level2type = N'COLUMN', @level2name = N'IdPayrollSettings';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico (INT IDENTITY) y clave primaria de la tabla ConceptNoveltyAdjust; único para cada registro de ajuste de concepto en novedades de nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptNoveltyAdjust', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptNoveltyAdjust', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptNoveltyAdjust', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra los ajustes de conceptos de nómina, relacionando cada concepto de novedad con su concepto de ajuste correspondiente dentro de una configuración de nómina. Permite definir qué concepto se usa para corregir o compensar otro en el proceso de liquidación de nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptNoveltyAdjust';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ConceptNoveltyAdjust';
