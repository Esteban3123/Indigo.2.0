CREATE TABLE [dbo].[HCCONFTABPARAESP] (
    [ID]               INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCCONFTABPARAC] INT      NOT NULL,
    [CODESPECI]        CHAR (3) NOT NULL,
    CONSTRAINT [PK_HCCONFTABPARAESP] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCCONFTABPARAESP_HCCONFTABPARAC] FOREIGN KEY ([IDHCCONFTABPARAC]) REFERENCES [dbo].[HCCONFTABPARAC] ([ID]),
    CONSTRAINT [FK_HCCONFTABPARAESP_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la Especialidad de Excepción (FK a INESPECIA). Identifica la especialidad médica o profesional de la salud que se configura como excepción en las tablas de pruebas paraclínicas (laboratorio, imagen, diagnóstico).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONFTABPARAESP', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la Especialidad de Excepción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONFTABPARAESP', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONFTABPARAESP', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la tabla cabecera HCCONFTABPARAC. Referencia la configuración madre de tablas paraclínicas (exámenes, laboratorios, imágenes) que aplican por especialidad o unidad funcional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONFTABPARAESP', @level2type = N'COLUMN', @level2name = N'IDHCCONFTABPARAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla cabecera HCCONFTABPARAC = Configuración Tablas Paraclínicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONFTABPARAESP', @level2type = N'COLUMN', @level2name = N'IDHCCONFTABPARAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONFTABPARAESP', @level2type = N'COLUMN', @level2name = N'IDHCCONFTABPARAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autonumérico (INT IDENTITY) de la tabla de detalle. Clave primaria que identifica únicamente cada registro de especialidad en la configuración de tablas paraclínicas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONFTABPARAESP', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumérico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONFTABPARAESP', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONFTABPARAESP', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración de especialidades médicas asociadas a parámetros de historia clínica. Define qué especialidades aplican a cada configuración de tabla de parámetros clínicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONFTABPARAESP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONFTABPARAESP';
