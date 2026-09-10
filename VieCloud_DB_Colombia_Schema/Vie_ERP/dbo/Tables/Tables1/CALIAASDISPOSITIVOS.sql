CREATE TABLE [dbo].[CALIAASDISPOSITIVOS] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCALREPORTE]        INT           NOT NULL,
    [TIPOUCI]             INT           NOT NULL,
    [FECHAINGRESOUCI]     DATE          NOT NULL,
    [REINGRESO]           BIT           NOT NULL,
    [PACIENTEREMITIDO]    BIT           NOT NULL,
    [CASOIADEXTRAHOSPITA] BIT           NOT NULL,
    [NOMINSTITUCIONIAD]   VARCHAR (500) NULL,
    [PESONACER]           INT           NULL,
    [TIPOIAD]             INT           NOT NULL,
    [CRITERIONAV]         INT           NULL,
    [CRITERIOITS]         INT           NULL,
    [CRITERIOITSUAC]      INT           NULL,
    [FECHADIAGNOSTI]      DATE          NOT NULL,
    [IADPOLIMICROBIANA]   BIT           NOT NULL,
    [VENTILADORMECANICO]  BIT           NULL,
    [CATETERCENTRAL]      BIT           NULL,
    [CATETERURINARIO]     BIT           NULL,
    [FECHAINSER1]         DATE          NULL,
    [FECHAINSER2]         DATE          NULL,
    [FECHAINSER3]         DATE          NULL,
    [FECHARETIRO1]        DATE          NULL,
    [FECHARETIRO2]        DATE          NULL,
    [FECHARETIRO3]        DATE          NULL,
    [CANCER]              BIT           NULL,
    [CORTICOTERAPIA]      BIT           NULL,
    [DESNUTRICION]        BIT           NULL,
    [DIABETES]            BIT           NULL,
    [DIALISIS]            BIT           NULL,
    [EDADEXTREMA]         BIT           NULL,
    [ENFERMEDADRENAL]     BIT           NULL,
    [EPOC]                BIT           NULL,
    [INMUNOSUPRESION]     BIT           NULL,
    [PARALISIS]           BIT           NULL,
    [VIH]                 BIT           NULL,
    [INFEPREVIA]          BIT           NULL,
    [QUIMIOTERAPIA]       BIT           NULL,
    [TRAUMATISMO]         BIT           NULL,
    [OBESIDAD]            BIT           NULL,
    [PREMATUREZ]          BIT           NULL,
    [NINGUNO]             BIT           NULL,
    [OTRO]                BIT           NULL,
    [CUALOTRO]            VARCHAR (500) NULL,
    [FECHATOMAMUESTRA1]   DATE          NULL,
    [FECHATOMAMUESTRA2]   DATE          NULL,
    [FECHATOMAMUESTRA3]   DATE          NULL,
    [CODMUESTRA1]         INT           NULL,
    [CODMUESTRA2]         INT           NULL,
    [CODMUESTRA3]         INT           NULL,
    [CODPRUEBA1]          VARCHAR (5)   NULL,
    [CODPRUEBA2]          VARCHAR (5)   NULL,
    [CODPRUEBA3]          VARCHAR (5)   NULL,
    [MICROORGANISMO1]     VARCHAR (15)  NULL,
    [MICROORGANISMO2]     VARCHAR (15)  NULL,
    [MICROORGANISMO3]     VARCHAR (15)  NULL,
    CONSTRAINT [PK_CALIAASDISPOSITIVOS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CALIAASDISPOSITIVOS_CALREPORTE] FOREIGN KEY ([IDCALREPORTE]) REFERENCES [dbo].[CALREPORTE] ([ID])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Microorganismo aislado en tercera muestra; identificación de bacteria, hongo o virus en cultivo de laboratorio para diagnóstico de infección asociada a dispositivos (IAD)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'MICROORGANISMO3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Microorganismo aislado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'MICROORGANISMO3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'MICROORGANISMO3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Microorganismo aislado en segunda muestra; identificación de bacteria, hongo o virus en cultivo de laboratorio para diagnóstico de infección asociada a dispositivos (IAD)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'MICROORGANISMO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Microorganismo aislado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'MICROORGANISMO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'MICROORGANISMO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Microorganismo aislado en primera muestra; identificación de bacteria, hongo o virus en cultivo de laboratorio para diagnóstico de infección asociada a dispositivos (IAD)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'MICROORGANISMO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Microorganismo aislado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'MICROORGANISMO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'MICROORGANISMO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la prueba microbiológica realizada en tercera muestra; tipo de cultivo (sangre, orina, secreción, etc.)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CODPRUEBA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cód. de la prueba', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CODPRUEBA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CODPRUEBA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la prueba microbiológica realizada en segunda muestra; tipo de cultivo (sangre, orina, secreción, etc.)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CODPRUEBA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cód. de la prueba', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CODPRUEBA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CODPRUEBA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la prueba microbiológica realizada en primera muestra; tipo de cultivo (sangre, orina, secreción, etc.)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CODPRUEBA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cód. de la prueba', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CODPRUEBA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CODPRUEBA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador de tercera muestra biológica enviada a laboratorio para análisis microbiológico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CODMUESTRA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cód. de la muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CODMUESTRA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CODMUESTRA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador de segunda muestra biológica enviada a laboratorio para análisis microbiológico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CODMUESTRA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cód. de la muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CODMUESTRA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CODMUESTRA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador de primera muestra biológica enviada a laboratorio para análisis microbiológico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CODMUESTRA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cód. de la muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CODMUESTRA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CODMUESTRA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de toma de tercera muestra biológica para cultivo y aislamiento microbiológico (DATE)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'FECHATOMAMUESTRA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de toma de la muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'FECHATOMAMUESTRA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'FECHATOMAMUESTRA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de toma de segunda muestra biológica para cultivo y aislamiento microbiológico (DATE)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'FECHATOMAMUESTRA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de toma de la muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'FECHATOMAMUESTRA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'FECHATOMAMUESTRA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de toma de primera muestra biológica para cultivo y aislamiento microbiológico en laboratorio (DATE)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'FECHATOMAMUESTRA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  Datos de laboratorio  Fecha de toma de la muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'FECHATOMAMUESTRA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'FECHATOMAMUESTRA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del factor de riesgo endógeno adicional no categorizado; comorbilidad, condición clínica o complicación del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CUALOTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuál otro factor endógeno?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CUALOTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CUALOTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica presencia de otro factor de riesgo endógeno no listado; booleano (1=Sí, 0=No)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'OTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  1 = True   0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'OTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'OTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica ausencia de factores de riesgo endógenos; booleano (1=Sin factores, 0=Con factores)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'NINGUNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  1 = True   0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'NINGUNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'NINGUNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Factor de riesgo endógeno: nacimiento prematuro (<37 semanas); booleano (1=Sí, 0=No)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'PREMATUREZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  1 = True   0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'PREMATUREZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'PREMATUREZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Factor de riesgo endógeno: sobrepeso patológico (IMC>30); booleano (1=Sí, 0=No)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'OBESIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  1 = True   0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'OBESIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'OBESIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Factor de riesgo endógeno: trauma, lesión o herida; booleano (1=Sí, 0=No)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'TRAUMATISMO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  1 = True   0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'TRAUMATISMO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'TRAUMATISMO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Factor de riesgo endógeno: tratamiento oncológico citotóxico; booleano (1=Sí, 0=No)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'QUIMIOTERAPIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  1 = True   0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'QUIMIOTERAPIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'QUIMIOTERAPIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Factor de riesgo endógeno: infección previa documentada antes del evento actual; booleano (1=Sí, 0=No)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'INFEPREVIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  1 = True   0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'INFEPREVIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'INFEPREVIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Factor de riesgo endógeno: virus de inmunodeficiencia humana / SIDA; booleano (1=Sí, 0=No)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'VIH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  1 = True   0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'VIH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'VIH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Factor de riesgo endógeno: parálisis, pérdida de movilidad o funcionalidad motora; booleano (1=Sí, 0=No)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'PARALISIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  1 = True   0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'PARALISIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'PARALISIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Factor de riesgo endógeno: inmunosupresión medicamentosa o patológica; booleano (1=Sí, 0=No)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'INMUNOSUPRESION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  1 = True   0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'INMUNOSUPRESION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'INMUNOSUPRESION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Factor de riesgo endógeno: Enfermedad Pulmonar Obstructiva Crónica; booleano (1=Sí, 0=No)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'EPOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  1 = True   0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'EPOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'EPOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Factor de riesgo endógeno: insuficiencia renal crónica o enfermedad renal; booleano (1=Sí, 0=No)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'ENFERMEDADRENAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  1 = True   0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'ENFERMEDADRENAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'ENFERMEDADRENAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Factor de riesgo endógeno: edad avanzada (>65 años) o edad pediátrica extrema; booleano (1=Sí, 0=No)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'EDADEXTREMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  1 = True   0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'EDADEXTREMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'EDADEXTREMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Factor de riesgo endógeno: paciente en programa de diálisis renal; booleano (1=Sí, 0=No)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'DIALISIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  1 = True   0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'DIALISIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'DIALISIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Factor de riesgo endógeno: diabetes mellitus tipo 1 o 2, descompensada o controlada; booleano (1=Sí, 0=No)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'DIABETES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  1 = True   0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'DIABETES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'DIABETES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Factor de riesgo endógeno: malnutrición, bajo peso, albumina baja; booleano (1=Sí, 0=No)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'DESNUTRICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  1 = True   0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'DESNUTRICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'DESNUTRICION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Factor de riesgo endógeno: uso de corticosteroides sistémicos; booleano (1=Sí, 0=No)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CORTICOTERAPIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  1 = True   0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CORTICOTERAPIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CORTICOTERAPIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Factor de riesgo endógeno: cáncer activo, en remisión o antecedente; booleano (1=Sí, 0=No)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CANCER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Factores de riesgo endógenos  1 = True   0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CANCER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CANCER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de retiro del tercer dispositivo (catéter central, urinario o ventilador); DATE (YYYY-MM-DD)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'FECHARETIRO3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de retiro 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'FECHARETIRO3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'FECHARETIRO3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de retiro del segundo dispositivo (catéter central, urinario o ventilador); DATE (YYYY-MM-DD)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'FECHARETIRO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de retiro 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'FECHARETIRO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'FECHARETIRO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de retiro del primer dispositivo invasivo (catéter central, urinario o ventilador); DATE (YYYY-MM-DD)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'FECHARETIRO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de retiro 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'FECHARETIRO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'FECHARETIRO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inserción del tercer dispositivo invasivo (catéter central, urinario o tubo endotraqueal); DATE (YYYY-MM-DD)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'FECHAINSER3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de inserción 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'FECHAINSER3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'FECHAINSER3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inserción del segundo dispositivo invasivo (catéter central, urinario o tubo endotraqueal); DATE (YYYY-MM-DD)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'FECHAINSER2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de inserción 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'FECHAINSER2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'FECHAINSER2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inserción del primer dispositivo invasivo (catéter central, urinario o tubo endotraqueal); DATE (YYYY-MM-DD)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'FECHAINSER1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de inserción 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'FECHAINSER1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'FECHAINSER1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presencia de catéter urinario (sonda Foley, suprapúbico) durante internación UCI; booleano (1=Sí, 0=No)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CATETERURINARIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cateter urinario   1 = Si0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CATETERURINARIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CATETERURINARIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presencia de catéter venoso central (línea central) durante internación UCI; booleano (1=Sí, 0=No)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CATETERCENTRAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cateter central   1 = Si0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CATETERCENTRAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CATETERCENTRAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presencia de ventilación mecánica invasiva (tubo endotraqueal, traqueostomía); booleano (1=Sí, 0=No)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'VENTILADORMECANICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ventilador mecánico  1 = Si  0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'VENTILADORMECANICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'VENTILADORMECANICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Infección Asociada a Dispositivo (IAD) causada por múltiples microorganismos simultáneamente; booleano (1=Polimicrobiana, 0=Monomicrobiana)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'IADPOLIMICROBIANA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'IAD polimicrobiana  1 = Si  0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'IADPOLIMICROBIANA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'IADPOLIMICROBIANA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de diagnóstico confirmado de Infección Asociada a Dispositivo (IAD) en UCI; DATE (YYYY-MM-DD)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'FECHADIAGNOSTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de diagnóstico IAD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'FECHADIAGNOSTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'FECHADIAGNOSTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Criterio de clasificación para ITSAC (Infección del Tracto Urinario en UCI); 1=Criterio 1a, 2=Criterio 2a, 3=Criterio 3, 4=Criterio 4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CRITERIOITSUAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Criterio de clasificación para ITSUAC   1=Criterio 1a 2=Criterio 2a 3=Criterio 3   4 =Criterio 4 ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CRITERIOITSUAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CRITERIOITSUAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Criterio de clasificación para ITSAC (Infección Tracto Urinario Asociada a Catéter); 1=Criterio 1, 2=Criterio 2, 3=Criterio 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CRITERIOITS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Criterio de clasificación para ITSAC   1=Criterio 1 2=Criterio 23=Criterio 3 ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CRITERIOITS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CRITERIOITS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Criterio de clasificación para NAV (Neumonía Asociada a Ventilador mecánico); 1=NEU 1, 2=NEU 2, 3=NEU 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CRITERIONAV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Criterio de clasificación NAV  1=NEU 1  2=NEU 2  3=NEU 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CRITERIONAV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CRITERIONAV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de Infección Asociada a Dispositivo (IAD); 1=NAV (neumonía), 2=ITSUAC (infección urinaria), 3=ITSAC (bacteremia)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'TIPOIAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de IAD   1=NAV  2=ISTU-AC  3=ITS-AC ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'TIPOIAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'TIPOIAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso al nacimiento en UCIN para estratificación de riesgo; 1=≤750gr, 2=751-1000gr, 3=1001-1500gr, 4=1501-2500gr, 5=≥2501gr', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'PESONACER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Para UCIN, peso al nacer  1== 750 gr  2=751- 1000gr  3=1001-1500 gr  4=1501-2500 gr  5==2501 gr', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'PESONACER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'PESONACER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la institución u hospital de origen donde se atribuye la IAD antes del traslado a UCI actual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'NOMINSTITUCIONIAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la Institución a la que se atribuye la IAD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'NOMINSTITUCIONIAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'NOMINSTITUCIONIAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Caso de IAD originada en comunidad u otra institución antes del ingreso a la UCI actual; booleano (1=Extrahospitalaria, 0=Intrahospitalaria)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CASOIADEXTRAHOSPITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Caso de IAD extrahospitalaria  1= Si 0= No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CASOIADEXTRAHOSPITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CASOIADEXTRAHOSPITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Paciente remitido/trasladado de otra institución o unidad de atención; booleano (1=Remitido, 0=Ingreso directo)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'PACIENTEREMITIDO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Paciente remitido  1= Si 0= No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'PACIENTEREMITIDO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'PACIENTEREMITIDO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Paciente que reingresa a UCI en el mismo periodo de internación o reingreso posterior; booleano (1=Reingreso, 0=Ingreso inicial)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'REINGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Reingreso  1= Si   0= No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'REINGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'REINGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de ingreso a la Unidad de Cuidados Intensivos (UCI); DATE (YYYY-MM-DD)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'FECHAINGRESOUCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de ingreso a la UCI ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'FECHAINGRESOUCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'FECHAINGRESOUCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de Unidad de Cuidados Intensivos donde ocurrió el evento; 1=UCI-Adultos, 2=UCI-Pediátricos, 3=UCI-Neonatos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'TIPOUCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de UCI relacionado con ocurrencia del evento  1 =UCI-A  2 =UCI-P  3 =UCI-N', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'TIPOUCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'TIPOUCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador foráneo que referencia CALREPORTE; almacena el ID del reporte madre de vigilancia epidemiológica de IAD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'IDCALREPORTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del reporte que guarda igual que id del tabla CALREPORTE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'IDCALREPORTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'IDCALREPORTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (IDENTITY INT) de registro en tabla CALIAASDISPOSITIVOS; clave primaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registros de infecciones asociadas a dispositivos (IAD) en unidades de cuidados intensivos (UCI): ventilador mecánico, catéter central y catéter urinario. Captura datos de vigilancia epidemiológica intrahospitalaria incluyendo factores de riesgo del paciente, fechas de inserción y retiro de dispositivos, y resultados microbiológicos de muestras clínicas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS';
