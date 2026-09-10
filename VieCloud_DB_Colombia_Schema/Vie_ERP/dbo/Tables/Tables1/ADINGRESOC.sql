CREATE TABLE [dbo].[ADINGRESOC] (
    [INGRESO] NCHAR (10) NOT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de ingresos o admisiones de pacientes. Guarda información relacionada con el proceso de ingreso hospitalario, urgencias u hospitalización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número o código de ingreso del paciente, identifica el episodio de atención, admisión u hospitalización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESOC', @level2type = N'COLUMN', @level2name = N'INGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESOC', @level2type = N'COLUMN', @level2name = N'INGRESO';
