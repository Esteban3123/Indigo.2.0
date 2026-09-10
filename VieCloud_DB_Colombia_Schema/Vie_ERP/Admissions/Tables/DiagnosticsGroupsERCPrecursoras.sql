CREATE TABLE [Admissions].[DiagnosticsGroupsERCPrecursoras] (
    [Id]                     INT      IDENTITY (1, 1) NOT NULL,
    [IdGroupsERCPrecursoras] INT      NOT NULL,
    [DiagnosticCode]         CHAR (4) NOT NULL,
    CONSTRAINT [PK__Diagnost__3214EC0742A5BFFE] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DiagnosticsGroupsERCPrecursora_INDIAGNOS] FOREIGN KEY ([DiagnosticCode]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_DiagnosticsGroupsERCPrecursoras_GroupsERCPrecursoras] FOREIGN KEY ([IdGroupsERCPrecursoras]) REFERENCES [Admissions].[GroupsERCPrecursoras] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico (CIE-10) referenciado de la tabla INDIAGNOS; identifica la condición clínica o enfermedad asociada al grupo ERC precursor; tipo CHAR(4), clave foránea', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'DiagnosticsGroupsERCPrecursoras', @level2type = N'COLUMN', @level2name = N'DiagnosticCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el codigo de diagnostico de la tabla INDIAGNOS', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'DiagnosticsGroupsERCPrecursoras', @level2type = N'COLUMN', @level2name = N'DiagnosticCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'DiagnosticsGroupsERCPrecursoras', @level2type = N'COLUMN', @level2name = N'DiagnosticCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del grupo ERC precursor (enfermedad renal crónica precursora) relacionado; referencia a tabla GroupsERCPrecursoras; clave foránea para vincular diagnósticos con grupos de riesgo', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'DiagnosticsGroupsERCPrecursoras', @level2type = N'COLUMN', @level2name = N'IdGroupsERCPrecursoras';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el identificador unico de cada registro de la tabla GroupsERCPrecursoras', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'DiagnosticsGroupsERCPrecursoras', @level2type = N'COLUMN', @level2name = N'IdGroupsERCPrecursoras';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'DiagnosticsGroupsERCPrecursoras', @level2type = N'COLUMN', @level2name = N'IdGroupsERCPrecursoras';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único y secuencial del registro en tabla DiagnosticsGroupsERCPrecursoras; clave primaria con auto-incremento; tipo INT IDENTITY', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'DiagnosticsGroupsERCPrecursoras', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el identificador unico de cada registro', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'DiagnosticsGroupsERCPrecursoras', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'DiagnosticsGroupsERCPrecursoras', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona grupos de condiciones precursoras de Enfermedad Renal Crónica (ERC) con sus códigos de diagnóstico (CIE-10). Permite identificar qué diagnósticos pertenecen a cada grupo de riesgo renal para seguimiento clínico y agrupación de pacientes.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'DiagnosticsGroupsERCPrecursoras';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'DiagnosticsGroupsERCPrecursoras';
