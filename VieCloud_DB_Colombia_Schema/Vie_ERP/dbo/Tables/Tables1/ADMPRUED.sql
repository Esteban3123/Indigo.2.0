CREATE TABLE [dbo].[ADMPRUED] (
    [ID]      INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCAB]   INT      NOT NULL,
    [CODDIAG] CHAR (4) NOT NULL,
    CONSTRAINT [PK_ADMPRUED] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ADMPRUED_ADMPRUEC] FOREIGN KEY ([IDCAB]) REFERENCES [dbo].[ADMPRUEC] ([ID])
);




GO


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de diagnósticos asociados a las pruebas o evidencias de admisión. Relaciona cada registro de cabecera de prueba con su código de diagnóstico clínico (CIE-10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMPRUED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMPRUED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de diagnóstico de prueba.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMPRUED', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMPRUED', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera o registro principal al que pertenece este diagnóstico (relación con el encabezado de la prueba de admisión).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMPRUED', @level2type = N'COLUMN', @level2name = N'IDCAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMPRUED', @level2type = N'COLUMN', @level2name = N'IDCAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del diagnóstico clínico asociado, según clasificación CIE-10 (ej: enfermedad, condición o motivo de consulta).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMPRUED', @level2type = N'COLUMN', @level2name = N'CODDIAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMPRUED', @level2type = N'COLUMN', @level2name = N'CODDIAG';
