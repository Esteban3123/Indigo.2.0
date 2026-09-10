CREATE TABLE [dbo].[HCDIAGPCE] (
    [CODDIAGPCE] INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODDIAGENF] VARCHAR (5) NOT NULL,
    CONSTRAINT [FK_HCDIAG_PCE] PRIMARY KEY CLUSTERED ([CODDIAGPCE] ASC),
    CONSTRAINT [FK_HCDIAGPCE_HCDIAGENF] FOREIGN KEY ([CODDIAGENF]) REFERENCES [dbo].[HCDIAGENF] ([CODDIAGENF])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del diagnóstico de enfermería (diagnóstico enfermero, clasificación NANDA, identificador de problema de salud identificado por enfermería)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGPCE', @level2type = N'COLUMN', @level2name = N'CODDIAGENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del diagnostico e enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGPCE', @level2type = N'COLUMN', @level2name = N'CODDIAGENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGPCE', @level2type = N'COLUMN', @level2name = N'CODDIAGENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del diagnóstico del plan de cuidado de enfermería (diagnóstico enfermero en plan de atención, identificador de problema de salud en plan de cuidados de enfermería)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGPCE', @level2type = N'COLUMN', @level2name = N'CODDIAGPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del diagnostico del plan cuidado de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGPCE', @level2type = N'COLUMN', @level2name = N'CODDIAGPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGPCE', @level2type = N'COLUMN', @level2name = N'CODDIAGPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de diagnósticos de enfermería asociados a la historia clínica del paciente. Cada fila representa un diagnóstico de enfermería documentado durante la atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGPCE';
