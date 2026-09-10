CREATE TABLE [dbo].[HCMOTLIB] (
    [ID]        INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODMOTLIB] CHAR (3)      NOT NULL,
    [DESMOTLIB] VARCHAR (200) NOT NULL,
    [ESTMOTLIB] BIT           NOT NULL,
    CONSTRAINT [PK_HCMOTLIB] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del motivo de liberación (1=activo, 0=inactivo). Indicador booleano que controla si el motivo está disponible para registrar liberaciones de pacientes en la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTLIB', @level2type = N'COLUMN', @level2name = N'ESTMOTLIB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Motivo de Liberacion  1- activo  0- inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTLIB', @level2type = N'COLUMN', @level2name = N'ESTMOTLIB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTLIB', @level2type = N'COLUMN', @level2name = N'ESTMOTLIB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual del motivo de liberación. Texto de hasta 200 caracteres que explica la razón del alta, egreso o liberación del paciente (ej: curación, remisión a otro centro, abandono, fallecimiento).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTLIB', @level2type = N'COLUMN', @level2name = N'DESMOTLIB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del Motivo de Liberacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTLIB', @level2type = N'COLUMN', @level2name = N'DESMOTLIB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTLIB', @level2type = N'COLUMN', @level2name = N'DESMOTLIB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del motivo de liberación. Identificador alfanumérico de 3 caracteres que clasifica el tipo de salida, egreso o terminación de la atención sanitaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTLIB', @level2type = N'COLUMN', @level2name = N'CODMOTLIB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Motivo de Liberacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTLIB', @level2type = N'COLUMN', @level2name = N'CODMOTLIB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTLIB', @level2type = N'COLUMN', @level2name = N'CODMOTLIB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT, identity). Clave primaria de la tabla HCMOTLIB, generada automáticamente para cada registro de motivo de liberación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTLIB', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTLIB', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTLIB', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de motivos de liberación utilizados en historia clínica. Permite clasificar las razones por las cuales se libera o cierra un proceso clínico, registro o recurso dentro del sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTLIB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOTLIB';
