CREATE TABLE [dbo].[AGAGEMEDE] (
    [CODAUTONU] INT      NOT NULL,
    [CODACTMED] CHAR (3) NOT NULL,
    CONSTRAINT [PK_AGAGEMEDE] PRIMARY KEY CLUSTERED ([CODAUTONU] ASC, [CODACTMED] ASC),
    CONSTRAINT [FK_AGAGEMEDE2_AGACTIMED] FOREIGN KEY ([CODACTMED]) REFERENCES [dbo].[AGACTIMED] ([CODACTMED])
);


GO
ALTER TABLE [dbo].[AGAGEMEDE] NOCHECK CONSTRAINT [FK_AGAGEMEDE2_AGACTIMED];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la Actividad Médica (CHAR 3). Identificador de la actividad clínica, procedimiento o servicio de salud. Referencia a tabla AGACTIMED. Búsqueda: actividad médica, procedimiento clínico, servicio sanitario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDE', @level2type = N'COLUMN', @level2name = N'CODACTMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Actividad medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDE', @level2type = N'COLUMN', @level2name = N'CODACTMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDE', @level2type = N'COLUMN', @level2name = N'CODACTMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código Autonumérico (INT). Identificador único autogenerado de la tabla AGAGEMEDC para relación de autorizaciones, gestión médica y procedimientos. Clave primaria compuesta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDE', @level2type = N'COLUMN', @level2name = N'CODAUTONU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Autonumerico de la tabla AGAGEMEDC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDE', @level2type = N'COLUMN', @level2name = N'CODAUTONU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDE', @level2type = N'COLUMN', @level2name = N'CODAUTONU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación entre autorizaciones de agendamiento y actividades médicas. Registra qué actividades o procedimientos médicos están asociados a cada autorización de agenda.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDE';
