CREATE TABLE [dbo].[HCVIAADMI] (
    [CODVIAADM] VARCHAR (20) NOT NULL,
    [DESVIAADM] CHAR (30)    NOT NULL,
    [CODHOMHV]  VARCHAR (20) NULL,
    CONSTRAINT [PK_HCVIAADMI] PRIMARY KEY CLUSTERED ([CODVIAADM] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código homólogo de la vía de administración (equivalente en Smart Health), usado para interoperabilidad e integración con sistemas externos; tipo VARCHAR(20), referencia de mapeo estándar.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVIAADMI', @level2type = N'COLUMN', @level2name = N'CODHOMHV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo homologo de la via de administración que pasa a smarth health', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVIAADMI', @level2type = N'COLUMN', @level2name = N'CODHOMHV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVIAADMI', @level2type = N'COLUMN', @level2name = N'CODHOMHV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual de la vía de administración (oral, intravenosa, intramuscular, tópica, etc.); tipo CHAR(100), usado en recetas, procedimientos y órdenes de medicamentos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVIAADMI', @level2type = N'COLUMN', @level2name = N'DESVIAADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la Via de Administracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVIAADMI', @level2type = N'COLUMN', @level2name = N'DESVIAADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVIAADMI', @level2type = N'COLUMN', @level2name = N'DESVIAADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de la vía de administración (PK); tipo VARCHAR(20), identificador interno para relacionar medicamentos, recetas y órdenes en la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVIAADMI', @level2type = N'COLUMN', @level2name = N'CODVIAADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Via de Administracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVIAADMI', @level2type = N'COLUMN', @level2name = N'CODVIAADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVIAADMI', @level2type = N'COLUMN', @level2name = N'CODVIAADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de vías de administración de medicamentos o tratamientos (oral, intravenosa, intramuscular, etc.) utilizadas en la historia clínica para registrar cómo se suministra un medicamento o procedimiento al paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVIAADMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVIAADMI';
