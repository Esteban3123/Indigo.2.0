CREATE TABLE [dbo].[HCRECNADI] (
    [NUMCONSEC] INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CONSECREC] INT      NOT NULL,
    [CODDIAGNO] CHAR (4) NOT NULL,
    CONSTRAINT [PK_HCRECNADI] PRIMARY KEY CLUSTERED ([NUMCONSEC] ASC),
    CONSTRAINT [FK_HCRECNADI_HCRECINAC] FOREIGN KEY ([CONSECREC]) REFERENCES [dbo].[HCRECINAC] ([NUMCONSEC]),
    CONSTRAINT [FK_HCRECNADI_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO])
);


GO
ALTER TABLE [dbo].[HCRECNADI] NOCHECK CONSTRAINT [FK_HCRECNADI_HCRECINAC];




GO
ALTER TABLE [dbo].[HCRECNADI] NOCHECK CONSTRAINT [FK_HCRECNADI_HCRECINAC];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del diagnóstico (CIE-10), referencia a catálogo de diagnósticos clínicos, patología, enfermedad registrada en la historia clínica del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECNADI', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECNADI', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECNADI', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo de cabecera de receta/atención, vinculación con registro maestro HCRECINAC para agrupar diagnósticos por ingreso o consulta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECNADI', @level2type = N'COLUMN', @level2name = N'CONSECREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo cabecera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECNADI', @level2type = N'COLUMN', @level2name = N'CONSECREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECNADI', @level2type = N'COLUMN', @level2name = N'CONSECREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo interno autoincremental, identificador único de cada relación diagnóstico-receta en la historia clínica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECNADI', @level2type = N'COLUMN', @level2name = N'NUMCONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo Interno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECNADI', @level2type = N'COLUMN', @level2name = N'NUMCONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECNADI', @level2type = N'COLUMN', @level2name = N'NUMCONSEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diagnósticos registrados en cada receta o prescripción médica de historia clínica. Relaciona cada receta con uno o más códigos de diagnóstico (CIE-10) que justifican los medicamentos o procedimientos recetados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECNADI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECNADI';
