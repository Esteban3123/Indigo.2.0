CREATE TABLE [dbo].[HCANTPAII] (
    [IDETIPHIS]         CHAR (9)                                                                               NOT NULL,
    [NUMEFOLIO]         NCHAR (10)                                                                             NOT NULL,
    [IPCODPACI]         VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')       NOT NULL,
    [NUMINGRES]         CHAR (10)                                                                              NOT NULL,
    [CODCENATE]         CHAR (10)                                                                              NOT NULL,
    [UFUCODIGO]         CHAR (10)                                                                              NOT NULL,
    [CODPROSAL]         CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')          NOT NULL,
    [FECHISPAC]         DATETIME                                                                               NOT NULL,
    [ANTMEDPAC]         VARCHAR (8000) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTQUIPAC]         VARCHAR (8000) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTTRAPAC]         VARCHAR (8000) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTINMPAC]         VARCHAR (8000) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTALEPAC]         VARCHAR (8000) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTTRUPAC]         VARCHAR (8000) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTPSIPAC]         VARCHAR (8000) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTFARPAC]         VARCHAR (8000) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTFAMPAC]         VARCHAR (8000) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTTOXPAC]         VARCHAR (8000) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTGINPAC]         VARCHAR (8000) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTOTRPAC]         VARCHAR (8000) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTPERINA]         VARCHAR (8000) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [INDAUDFOR]         NUMERIC (18)                                                                           NOT NULL,
    [ANTESCOLARES]      VARCHAR (8000)                                                                         NULL,
    [ANTLABORALES]      VARCHAR (8000)                                                                         NULL,
    [ANTNUTRICION]      VARCHAR (8000) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTODONTOLOG]      VARCHAR (8000) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTSOCIOECON]      VARCHAR (8000)                                                                         NULL,
    [ANTUROLOGIASEXUAL] VARCHAR (8000)                                                                         NULL,
    CONSTRAINT [PK_HCANTPAII] PRIMARY KEY CLUSTERED ([IDETIPHIS] ASC, [NUMEFOLIO] ASC, [IPCODPACI] ASC, [NUMINGRES] ASC),
    CONSTRAINT [FK_HCANTPAII_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCANTPAII_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCANTPAII_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCANTPAII_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPAII].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPAII].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPAII].[ANTMEDPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPAII].[ANTQUIPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPAII].[ANTTRAPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPAII].[ANTINMPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPAII].[ANTALEPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPAII].[ANTTRUPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPAII].[ANTPSIPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPAII].[ANTFARPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPAII].[ANTFAMPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPAII].[ANTTOXPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPAII].[ANTGINPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPAII].[ANTOTRPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPAII].[ANTPERINA]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPAII].[ANTNUTRICION]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPAII].[ANTODONTOLOG]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes urológicos y sexuales del paciente. Historial de condiciones, procedimientos o consultas relacionadas con urología, función sexual y salud reproductiva masculina. VARCHAR(8000), PII sensible.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTUROLOGIASEXUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Urologia Sexual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTUROLOGIASEXUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTUROLOGIASEXUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes socioeconómicos del paciente. Información sobre situación laboral, nivel educativo, acceso a servicios, condiciones de vivienda y factores sociales relevantes para la atención. VARCHAR(8000), no ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTSOCIOECON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes SocioEconómicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTSOCIOECON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTSOCIOECON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes odontológicos del paciente. Historial de salud bucal, tratamientos dentales, prótesis, caries, periodontitis y procedimientos odontológicos previos. VARCHAR(8000), PII sensible.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTODONTOLOG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Odontológicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTODONTOLOG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTODONTOLOG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes nutricionales del paciente. Historial dietético, alergias alimentarias, trastornos de la alimentación, estado nutricional previo y recomendaciones nutricionales. VARCHAR(8000), PII sensible.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTNUTRICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Nutricionales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTNUTRICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTNUTRICION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes laborales del paciente. Historial de empleos, exposición ocupacional, riesgos laborales, incapacidades relacionadas con el trabajo. VARCHAR(8000), no ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTLABORALES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes  Laborales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTLABORALES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTLABORALES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes escolares del paciente. Historial educativo, desempeño académico, problemas de aprendizaje, adaptación escolar. VARCHAR(8000), no ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTESCOLARES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Escolares', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTESCOLARES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTESCOLARES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de auditoría, identificador único para trazabilidad y control de cambios en el registro clínico. NUMERIC(18), utilizado en procesos de auditoría de bases de datos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes perinatales del paciente. Historial de embarazo materno, parto, nacimiento, complicaciones perinatales, peso al nacer, Apgar, internación neonatal. VARCHAR(8000), PII sensible.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTPERINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Perinatales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTPERINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTPERINA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes otros del paciente. Información clínica adicional no clasificada en otras categorías, hallazgos relevantes diversos. VARCHAR(8000), PII sensible.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTOTRPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Otros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTOTRPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTOTRPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes ginecológicos del paciente. Historial de ciclos menstruales, embarazos, partos, abortos, menopausia, infecciones ginecológicas, procedimientos ginecológicos. VARCHAR(8000), PII sensible.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTGINPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Ginecologicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTGINPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTGINPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes tóxicos del paciente. Consumo de tabaco, alcohol, drogas ilícitas, inhalantes y otras sustancias tóxicas con frecuencia y cantidad. VARCHAR(8000), PII sensible.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTTOXPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Toxicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTTOXPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTTOXPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes familiares del paciente. Historial de enfermedades hereditarias, cáncer, diabetes, hipertensión, cardiopatías y otros padecimientos en familia cercana. VARCHAR(8000), PII sensible.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTFAMPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Familiares', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTFAMPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTFAMPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes farmacológicos del paciente. Medicamentos previos, reacciones adversas a fármacos, alergias medicamentosas, intolerancias, tratamientos prolongados. VARCHAR(8000), PII sensible.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTFARPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Farmacologicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTFARPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTFARPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes psicológicos y psiquiátricos del paciente. Historial de depresión, ansiedad, esquizofrenia, trastornos de personalidad, hospitalizaciones psiquiátricas, suicidio. VARCHAR(8000), PII sensible.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTPSIPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Psicologicos y Psiquiatricos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTPSIPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTPSIPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes traumáticos del paciente. Historial de accidentes, lesiones graves, cirugías por trauma, secuelas traumatológicas, eventos violentos. VARCHAR(8000), PII sensible.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTTRUPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Traumaticos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTTRUPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTTRUPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes alérgicos del paciente. Alergias a medicamentos, alimentos, sustancias ambientales, látex, con tipo de reacción alérgica. VARCHAR(8000), PII sensible.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTALEPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Alergicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTALEPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTALEPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes inmunológicos del paciente. Historial de vacunaciones, inmunizaciones, deficiencias inmunitarias, enfermedades autoinmunes, VIH/SIDA. VARCHAR(8000), PII sensible.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTINMPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Inmunologicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTINMPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTINMPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes transfusionales del paciente. Historial de transfusiones de sangre, plasma, componentes sanguíneos, reacciones transfusionales, enfermedades transmitidas por transfusión. VARCHAR(8000), PII sensible.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTTRAPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Transfucionales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTTRAPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTTRAPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes quirúrgicos del paciente. Historial de cirugías previas, anestesias, complicaciones quirúrgicas, implantes, órganos resecados. VARCHAR(8000), PII sensible.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTQUIPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Quirurgicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTQUIPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTQUIPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes médicos del paciente. Historial de enfermedades crónicas, agudas, diagnósticos previos, hospitalizaciones, procedimientos médicos. VARCHAR(8000), PII sensible.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTMEDPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes Medicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTMEDPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'ANTMEDPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación o registro de la historia clínica del paciente. Marca de tiempo de la consulta, atención o ingreso. DATETIME, clave para trazabilidad temporal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'FECHISPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Historia Clinica Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'FECHISPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'FECHISPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud (médico, enfermera, especialista) que registra o atiende al paciente. PII ofuscado, FK a tabla profesionales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional (departamento, servicio, área clínica) donde se atiende al paciente. FK a INUNIFUNC, identifica servicio de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (hospital, clínica, consultorio) donde ocurre la atención. FK a ADCENATEN, identifica institución de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del ingreso del paciente, identificador único del episodio de atención. FK a ADINGRESO, relaciona historia con ingreso hospitalario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente, equivalente a cédula, documento de identidad, documento nacional de identidad o identificación personal única. VARCHAR(25), PII ofuscado, FK a INPACIENT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio de la historia clínica, identificador único del documento clínico dentro del ingreso. NCHAR(10), ordena registros clínicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno del tipo de historia clínica (nombre técnico, código de template). CHAR(9), marca estructura y formato del registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes personales y clínicos del paciente registrados en la historia clínica durante un ingreso. Contiene los antecedentes médicos, quirúrgicos, familiares, toxicológicos, ginecológicos, farmacológicos, psicológicos, laborales, nutricionales y otros relevantes para la atención, asociados al paciente, profesional de salud, centro de atención y unidad funcional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPAII';
