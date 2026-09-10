CREATE TABLE [dbo].[HCANTPAHI] (
    [IPCODPACI]    VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')       NOT NULL,
    [ANTMEDPAC]    VARCHAR (8000) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTQUIPAC]    VARCHAR (8000) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTTRAPAC]    VARCHAR (8000) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTINMPAC]    VARCHAR (8000) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTALEPAC]    VARCHAR (8000) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTTRUPAC]    VARCHAR (8000) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTPSIPAC]    VARCHAR (8000) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTFARPAC]    VARCHAR (8000) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTFAMPAC]    VARCHAR (8000) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTTOXPAC]    VARCHAR (8000) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTOTRPAC]    VARCHAR (8000)                                                                         NULL,
    [INDAUDFOR]    NUMERIC (18)                                                                           NOT NULL,
    [ANTANESTE]    VARCHAR (8000) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTHABVID]    VARCHAR (8000)                                                                         NULL,
    [ANTESCOLARES] VARCHAR (8000)                                                                         NULL,
    [ANTLABORALES] VARCHAR (8000)                                                                         NULL,
    [ANTNUTRICION] VARCHAR (8000) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTODONTOLOG] VARCHAR (8000) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTSOCIOECON] VARCHAR (8000)                                                                         NULL,
    CONSTRAINT [PK_HCANTPAHI] PRIMARY KEY CLUSTERED ([IPCODPACI] ASC),
    CONSTRAINT [FK_HCANTPAHI_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPAHI].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPAHI].[ANTMEDPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPAHI].[ANTQUIPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPAHI].[ANTTRAPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPAHI].[ANTINMPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPAHI].[ANTALEPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPAHI].[ANTTRUPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPAHI].[ANTPSIPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPAHI].[ANTFARPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPAHI].[ANTFAMPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPAHI].[ANTTOXPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPAHI].[ANTANESTE]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPAHI].[ANTNUTRICION]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPAHI].[ANTODONTOLOG]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes socioeconómicos del paciente: situación económica, laboral, vivienda, acceso a servicios. VARCHAR(8000), no ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTSOCIOECON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes SocioEconómicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTSOCIOECON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTSOCIOECON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes odontológicos del paciente: historia dental, tratamientos, caries, prótesis. VARCHAR(8000), PII ofuscado (ClinicalBackground_Ofuscado).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTODONTOLOG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Odontológicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTODONTOLOG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTODONTOLOG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes nutricionales del paciente: hábitos dietéticos, alergias alimentarias, desnutrición. VARCHAR(8000), PII ofuscado (ClinicalBackground_Ofuscado).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTNUTRICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Nutricionales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTNUTRICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTNUTRICION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes laborales del paciente: ocupación, exposiciones ocupacionales, riesgos laborales. VARCHAR(8000), no ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTLABORALES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes  Laborales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTLABORALES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTLABORALES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes escolares del paciente: nivel educativo, desempeño académico, historiales escolares. VARCHAR(8000), no ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTESCOLARES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Escolares', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTESCOLARES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTESCOLARES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes de hábitos de vida del paciente: sedentarismo, ejercicio, tabaquismo, alcohol, drogas. VARCHAR(8000), no ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTHABVID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Habitos de Vida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTHABVID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTHABVID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes anestésicos del paciente: reacciones previas a anestesia, intolerancias, complicaciones. VARCHAR(8000), PII ofuscado (ClinicalBackground_Ofuscado).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTANESTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Anestésicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTANESTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTANESTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificador de auditoría y seguimiento. NUMERIC(18), requerido, PK compuesto con IPCODPACI.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes otros del paciente: historia clínica complementaria no clasificada. VARCHAR(8000), no ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTOTRPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Otros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTOTRPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTOTRPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes tóxicos del paciente: exposición a sustancias tóxicas, envenenamientos, intoxicaciones. VARCHAR(8000), PII ofuscado (ClinicalBackground_Ofuscado).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTTOXPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Toxicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTTOXPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTTOXPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes familiares del paciente: enfermedades hereditarias, historia genética, antecedentes de parientes. VARCHAR(8000), PII ofuscado (ClinicalBackground_Ofuscado).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTFAMPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Familiares', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTFAMPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTFAMPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes farmacológicos del paciente: medicamentos previos, alergias medicamentosas, interacciones. VARCHAR(8000), PII ofuscado (ClinicalBackground_Ofuscado).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTFARPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Farmacologicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTFARPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTFARPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes psicológicos y psiquiátricos del paciente: trastornos mentales, tratamientos psiquiátricos, hospitalizaciones. VARCHAR(8000), PII ofuscado (ClinicalBackground_Ofuscado).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTPSIPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Psicologicos y Psiquiatricos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTPSIPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTPSIPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes traumáticos del paciente: lesiones previas, accidentes, trauma psicológico. VARCHAR(8000), PII ofuscado (ClinicalBackground_Ofuscado).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTTRUPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Traumaticos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTTRUPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTTRUPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes alérgicos del paciente: alergias medicamentosas, ambientales, alimentarias. VARCHAR(8000), PII ofuscado (ClinicalBackground_Ofuscado).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTALEPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Alergicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTALEPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTALEPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes inmunológicos del paciente: vacunaciones, inmunodeficiencias, respuesta inmune. VARCHAR(8000), PII ofuscado (ClinicalBackground_Ofuscado).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTINMPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Inmunologicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTINMPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTINMPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes transfusionales del paciente: transfusiones previas, reacciones, tipo de sangre. VARCHAR(8000), PII ofuscado (ClinicalBackground_Ofuscado).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTTRAPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Transfucionales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTTRAPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTTRAPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes quirúrgicos del paciente: cirugías previas, procedimientos quirúrgicos, complicaciones quirúrgicas. VARCHAR(8000), PII ofuscado (ClinicalBackground_Ofuscado).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTQUIPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Quirurgicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTQUIPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTQUIPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes médicos del paciente: enfermedades crónicas, diagnósticos previos, patologías relevantes. VARCHAR(8000), PII ofuscado (ClinicalBackground_Ofuscado).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTMEDPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Medicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTMEDPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'ANTMEDPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del paciente: cédula, documento de identidad, número de afiliado. VARCHAR(25), PII ofuscado (Identification_Ofuscado), FK a INPACIENT, PK.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes de salud e historia clínica del paciente. Contiene los antecedentes médicos, quirúrgicos, familiares, farmacológicos, alérgicos, toxicológicos, psicológicos, nutricionales, odontológicos, laborales, escolares y socioeconómicos registrados en la historia clínica de cada paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAHI';
