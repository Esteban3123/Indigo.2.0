CREATE TABLE [dbo].[ADFURIPSU2] (
    [NUMDOCVIC]  CHAR (16) NOT NULL,
    [NUMINGRES1] CHAR (10) NOT NULL,
    [NUMINGRES2] CHAR (10) NOT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona dos ingresos o atenciones de un mismo paciente víctima, permitiendo vincular episodios clínicos duplicados o relacionados dentro del sistema de admisiones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de documento o cédula de la persona víctima (identificación del paciente).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU2', @level2type = N'COLUMN', @level2name = N'NUMDOCVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU2', @level2type = N'COLUMN', @level2name = N'NUMDOCVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del primer ingreso o atención relacionada del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU2', @level2type = N'COLUMN', @level2name = N'NUMINGRES1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU2', @level2type = N'COLUMN', @level2name = N'NUMINGRES1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del segundo ingreso o atención relacionada del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU2', @level2type = N'COLUMN', @level2name = N'NUMINGRES2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU2', @level2type = N'COLUMN', @level2name = N'NUMINGRES2';
