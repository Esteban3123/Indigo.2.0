CREATE TABLE [dbo].[ADESTADOCONCIENCIA] (
    [CONCICODIGO] CHAR (3)  NOT NULL,
    [CONCIDESCRI] CHAR (35) NOT NULL,
    [ESTADO]      BIT       NOT NULL,
    CONSTRAINT [PK_ADESTADOCONCIENCIA] PRIMARY KEY CLUSTERED ([CONCICODIGO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de estados de conciencia del paciente (por ejemplo: consciente, semiconsciente, inconsciente) utilizados en el registro de admisión y valoración clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADESTADOCONCIENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADESTADOCONCIENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del estado de conciencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADESTADOCONCIENCIA', @level2type = N'COLUMN', @level2name = N'CONCICODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADESTADOCONCIENCIA', @level2type = N'COLUMN', @level2name = N'CONCICODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o descripción del estado de conciencia (ej: Consciente, Somnoliento, Comatoso).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADESTADOCONCIENCIA', @level2type = N'COLUMN', @level2name = N'CONCIDESCRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADESTADOCONCIENCIA', @level2type = N'COLUMN', @level2name = N'CONCIDESCRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el estado de conciencia está activo (1) o inactivo (0) para su uso en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADESTADOCONCIENCIA', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADESTADOCONCIENCIA', @level2type = N'COLUMN', @level2name = N'ESTADO';
