CREATE TABLE [dbo].[HCRADDIAGNOSTICOS] (
    [ID]              INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCRADESQUEMAS] INT      NOT NULL,
    [CODDIAGNO]       CHAR (4) NOT NULL,
    CONSTRAINT [PK_HCRADDIAGNOSTICOS] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico (CIE-10), caracterizado como CHAR(4). Identifica la enfermedad, condición o motivo de consulta del paciente. Usado en RIPS, historias clínicas y reportes epidemiológicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDIAGNOSTICOS', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el codigo diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDIAGNOSTICOS', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDIAGNOSTICOS', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la radicación de esquemas de historia clínica. Vincula cada diagnóstico al esquema o plantilla de atención clínica correspondiente al ingreso o atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDIAGNOSTICOS', @level2type = N'COLUMN', @level2name = N'IDHCRADESQUEMAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el ID de  radicacion de esquemas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDIAGNOSTICOS', @level2type = N'COLUMN', @level2name = N'IDHCRADESQUEMAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDIAGNOSTICOS', @level2type = N'COLUMN', @level2name = N'IDHCRADESQUEMAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK, autoincremental INT IDENTITY). Consecutivo principal de la tabla de diagnósticos adicionales en historias clínicas radicadas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDIAGNOSTICOS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDIAGNOSTICOS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDIAGNOSTICOS', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diagnósticos asociados a los esquemas de historia clínica en radiología. Registra los códigos CIE-10 vinculados a cada esquema de atención radiológica del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDIAGNOSTICOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDIAGNOSTICOS';
