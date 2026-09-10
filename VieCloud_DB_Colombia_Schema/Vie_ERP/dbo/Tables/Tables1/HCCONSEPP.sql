CREATE TABLE [dbo].[HCCONSEPP] (
    [CODSERIPS]  CHAR (20)      NOT NULL,
    [DESTIPPLAN] CHAR (30)      NOT NULL,
    [TIPPROIPS]  CHAR (1)       NOT NULL,
    [TIPANEPRO]  CHAR (1)       NOT NULL,
    [ASPPROIPS]  VARCHAR (4000) NULL,
    [RIEPROIPS]  VARCHAR (4000) NULL,
    [DETPROIPS]  VARCHAR (4000) NULL,
    CONSTRAINT [PK_HCCONSEPP] PRIMARY KEY CLUSTERED ([CODSERIPS] ASC, [DESTIPPLAN] ASC),
    CONSTRAINT [FK_HCCONSEPP_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle adicional del procedimiento, descripción extendida de técnica quirúrgica, hallazgos, complicaciones intraoperatorias y notas clínicas complementarias (VARCHAR 4000, texto libre)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONSEPP', @level2type = N'COLUMN', @level2name = N'DETPROIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Detalle Adicional del Procedimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONSEPP', @level2type = N'COLUMN', @level2name = N'DETPROIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONSEPP', @level2type = N'COLUMN', @level2name = N'DETPROIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Riesgos del procedimiento, complicaciones potenciales, contraindicaciones, efectos adversos esperados y advertencias médicas asociadas (VARCHAR 4000, texto libre)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONSEPP', @level2type = N'COLUMN', @level2name = N'RIEPROIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Riesgos del Procedimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONSEPP', @level2type = N'COLUMN', @level2name = N'RIEPROIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONSEPP', @level2type = N'COLUMN', @level2name = N'RIEPROIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Aspectos del procedimiento, consideraciones técnicas, recursos utilizados, equipamiento especial y características particulares de la intervención (VARCHAR 4000, texto libre)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONSEPP', @level2type = N'COLUMN', @level2name = N'ASPPROIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Aspectos del Procedimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONSEPP', @level2type = N'COLUMN', @level2name = N'ASPPROIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONSEPP', @level2type = N'COLUMN', @level2name = N'ASPPROIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de anestesia utilizada en el procedimiento: 1=General, 2=Local, 3=Regional; clasificación de la anestesiología aplicada (CHAR 1)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONSEPP', @level2type = N'COLUMN', @level2name = N'TIPANEPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Anestesia utilizada en el procedimiento  1: General  2: Local  3: Regional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONSEPP', @level2type = N'COLUMN', @level2name = N'TIPANEPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONSEPP', @level2type = N'COLUMN', @level2name = N'TIPANEPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de procedimiento IPS: 1=Procedimiento Especial, 2=Procedimiento Quirúrgico, 3=Otro; categorización para RIPS y facturación (CHAR 1)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONSEPP', @level2type = N'COLUMN', @level2name = N'TIPPROIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Procedimiento IPS  1: Procedimiento Especial  2: Procedimiento Quirurgico  3: Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONSEPP', @level2type = N'COLUMN', @level2name = N'TIPPROIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONSEPP', @level2type = N'COLUMN', @level2name = N'TIPPROIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de la plantilla de justificación para medicamento no POS, amparo de cobertura especial y autorización de fármaco fuera del plan (CHAR 30)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONSEPP', @level2type = N'COLUMN', @level2name = N'DESTIPPLAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la Plantilla Justificacion Medicamento No POS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONSEPP', @level2type = N'COLUMN', @level2name = N'DESTIPPLAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONSEPP', @level2type = N'COLUMN', @level2name = N'DESTIPPLAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del servicio IPS, identificador único del servicio de salud o procedimiento CUPS registrado; referencia a INCUPSIPS (CHAR 20, FK)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONSEPP', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONSEPP', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONSEPP', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda las características clínicas y de seguridad de los procedimientos de historia clínica según el plan de beneficios (PBS/plan complementario), incluyendo aspectos relevantes, riesgos y detalles del procedimiento por tipo de anestesia y tipo de profesional IPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONSEPP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONSEPP';
