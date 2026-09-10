CREATE TABLE [dbo].[HCFICHA356D] (
    [ID]                              INT           IDENTITY (1, 1) NOT NULL,
    [IDFICHANOTIFICACION]             INT           NULL,
    [CivilStatus]                     INT           NOT NULL,
    [CountryOriginOfCase]             VARCHAR (50)  NOT NULL,
    [DepartmentOriginOfCase]          VARCHAR (50)  NOT NULL,
    [MunicipalityOriginOfCase]        VARCHAR (50)  NOT NULL,
    [EducationLevel]                  INT           NOT NULL,
    [ClassificationOfInitialBehavior] INT           NOT NULL,
    [OccurrenceDate]                  DATE          NOT NULL,
    [PreviousAttempts]                BIT           NOT NULL,
    [NumberOfAttempts]                INT           NULL,
    [PatientComesAccompanied]         BIT           NOT NULL,
    [AccompanistName]                 VARCHAR (100) NULL,
    [CurrentlyConsumingSPA]           INT           NOT NULL,
    [SubstanceName]                   VARCHAR (100) NULL,
    [AccompanistPhone]                VARCHAR (10)  NULL,
    [CurrentlyInSchool]               BIT           NOT NULL,
    [AttendPrenatalCheckups]          BIT           NULL,
    [SexualOrientation]               INT           NOT NULL,
    [DifferentialPopulations]         INT           NOT NULL,
    [WhichDifferentialPopulation]     VARCHAR (30)  NULL,
    [CaseDetected]                    INT           NOT NULL,
    [WhichCaseDetected]               VARCHAR (30)  NULL,
    [Triggers]                        VARCHAR (MAX) NOT NULL,
    [RiskFactors]                     VARCHAR (MAX) NOT NULL,
    [Mechanism]                       VARCHAR (MAX) NOT NULL,
    [WhichMechanism]                  VARCHAR (30)  NULL,
    [SubstanceType]                   VARCHAR (MAX) NULL,
    [ProductName]                     VARCHAR (50)  NULL,
    [ExhibitionPathway]               VARCHAR (MAX) NOT NULL,
    [EventPlace]                      VARCHAR (MAX) NOT NULL,
    [WhichEventPlace]                 VARCHAR (30)  NULL,
    [MentalDisorder]                  CHAR (4)      NULL,
    [ReferredToMentalHealthServices]  VARCHAR (MAX) NOT NULL,
    [Observations]                    VARCHAR (500) NULL,
    [CODDIAGNO]                       CHAR (4)      NULL,
    [VERSION]                         VARCHAR (20)  NULL,
    [AreaOriginOfCase]                INT           NULL,
    CONSTRAINT [PK_HCFICHA356D] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Área de procedencia del caso: 1=Cabecera municipal, 2=Centro poblado, 3=Rural disperso. Ubicación geográfica donde se originó el evento notificado. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'AreaOriginOfCase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Area de procedencia del caso:  1. Cabecera municipal  2. Centro poblado  3. Rural disperso  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'AreaOriginOfCase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'AreaOriginOfCase';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión del registro de la ficha de notificación. Identificador de cambios o iteraciones del formulario. VARCHAR(20).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico CIE-10 asociado al evento reportado (intoxicación, suicidio, consumo de SPA). CHAR(4), PII_Médico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones adicionales, notas clínicas o detalles complementarios del caso notificado. VARCHAR(500).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Remisión a servicios de salud mental: 1=Psiquiatría, 2=Psicología, 3=Trabajo Social. JSON con datos de derivación a profesionales de salud mental.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'ReferredToMentalHealthServices';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Persona remitida a servicios de salud mental:  1. Psiquiatría  2. Psicología  3. Trabajo Social', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'ReferredToMentalHealthServices';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'ReferredToMentalHealthServices';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Trastorno mental o del compromiso psicológico asociado al evento (CIE-10). Código diagnóstico de alteración mental. CHAR(4), PII_Médico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'MentalDisorder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Trastorno mental o del compromiso asociado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'MentalDisorder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'MentalDisorder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especificación de lugar del evento cuando se selecciona ''''Otro'''' en EventPlace. Detalle complementario del sitio de ocurrencia. VARCHAR(30).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'WhichEventPlace';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cual (cuando en el campo Lugar donde se produjo la intoxicacion o evento seleccionan la opcion Otro)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'WhichEventPlace';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'WhichEventPlace';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'JSON con datos del lugar donde se produjo la intoxicación o evento: hogar, vía pública, institución educativa, laboral, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'EventPlace';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'JSON que guarda los datos del campo Lugar donde se produjo la intoxicacion o el evento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'EventPlace';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'EventPlace';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'JSON con datos de vía de exposición: ingestión, inhalación, dérmica, parenteral, ocular. Forma de contacto con la sustancia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'ExhibitionPathway';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'JSON que guarda los datos del campo Vía de exposición', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'ExhibitionPathway';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'ExhibitionPathway';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del producto, medicamento o sustancia implicado en el evento. Identificador del agente causal. VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'ProductName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'ProductName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'ProductName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'JSON con datos del tipo de sustancia: medicamentos, drogas ilícitas, pesticidas, alcohol, inhalables, otros. Clasificación de agente tóxico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'SubstanceType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'JSON que guarda los datos del campo Tipo de sustancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'SubstanceType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'SubstanceType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especificación de mecanismo cuando se selecciona ''''Otro'''' en Mechanism. Detalle de la forma de intoxicación o evento. VARCHAR(30).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'WhichMechanism';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cual mecanismo (cuando en el campo Mecanismo seleccionan la opcion Otro)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'WhichMechanism';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'WhichMechanism';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'JSON con datos del mecanismo: accidental, intencional, suicida, homicida, laboral. Circunstancia de cómo ocurrió el evento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'Mechanism';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'JSON que guarda los datos del campo Mecanismo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'Mechanism';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'Mechanism';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'JSON con datos de factores de riesgo: antecedentes de intentos, depresión, abuso, aislamiento social, acceso a medios letales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'RiskFactors';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'JSON que guarda los datos del campo Factores de riesgo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'RiskFactors';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'RiskFactors';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'JSON con datos de factores desencadenantes: pérdida laboral, ruptura sentimental, conflicto familiar, estrés, presión académica, bullying.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'Triggers';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'JSON que guarda los datos del campo Factores desencadenantes:  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'Triggers';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'Triggers';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especificación cuando se selecciona ''''Otro'''' en CaseDetected. Detalle de fuente de detección alternativa. VARCHAR(30).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'WhichCaseDetected';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cual (cuando en el campo Caso Detectado Por seleccionan la opción Otro)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'WhichCaseDetected';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'WhichCaseDetected';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Caso detectado por: 1=Consulta externa, 2=Urgencias, 3=DUES (CRUE), 4=Línea 106, 5=Establecimiento educativo, 6=Comunidad, 7=Otro. Punto de identificación del evento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'CaseDetected';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Caso detectado por:  1. Consulta externa  2. Servicio de urgencias  3. DUES (CRUE)  4. Línea 106  5. Establecimiento educativo  6. Comunidad  7. Otro ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'CaseDetected';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'CaseDetected';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especificación de población diferencial cuando se selecciona ''''Otra''''. Detalle de grupo vulnerable. VARCHAR(30).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'WhichDifferentialPopulation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cual poblacion diferencial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'WhichDifferentialPopulation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'WhichDifferentialPopulation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Poblaciones diferenciales: 1=Menor abandonado, 2=En protección, 3=LGTBI, 4=Servidor público, 5=Consumidor habitual SPA, 6=Trabajador informal, 7=Madre cabeza hogar, 8=Ninguna, 9=Otra. Clasificación de vulnerabilidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'DifferentialPopulations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Poblaciones diferenciales:  1. Menor abandonado  2. En protección  3. Población LGTBI  4. Servidor público  5. Consumidor habitual de SPA  6. Trabajador informal  7. Madre cabeza de hogar  8. Ninguna  9. Otra ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'DifferentialPopulations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'DifferentialPopulations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Orientación sexual: 1=Sin dato, 2=Heterosexual, 3=Lesbiana, 4=Gay, 5=Bisexual, 6=Transgénero. Identidad de género del paciente. INT, PII_Sensible.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'SexualOrientation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Orientacion sexual:  1. Sin dato  2. Heterosexual  3. Lesbiana  4. Gay  5. Bisexual  6. Transgénero', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'SexualOrientation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'SexualOrientation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si la paciente asiste a controles prenatales. Seguimiento obstétrico en gestantes. BIT, booleano.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'AttendPrenatalCheckups';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Asiste a controles prenatales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'AttendPrenatalCheckups';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'AttendPrenatalCheckups';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el paciente está actualmente escolarizado, matriculado en institución educativa. BIT, booleano.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'CurrentlyInSchool';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Paciente actualmente escolarizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'CurrentlyInSchool';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'CurrentlyInSchool';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Teléfono de contacto del acompañante del paciente. Número para comunicación con responsable. VARCHAR(10), PII_Contacto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'AccompanistPhone';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Telefono del acompañante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'AccompanistPhone';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'AccompanistPhone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre específico de la sustancia psicoactiva consumida: cocaína, marihuana, heroína, anfetaminas, éxtasis, opioides, etc. VARCHAR(100).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'SubstanceName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de sustancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'SubstanceName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'SubstanceName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Paciente consume sustancias psicoactivas: 1=Sí, 2=No, 3=Sin determinar. Uso actual de drogas o medicamentos de abuso. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'CurrentlyConsumingSPA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Paciente consume sustancias psicoactivas:  1. Si   2. No  3. Sin determinar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'CurrentlyConsumingSPA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'CurrentlyConsumingSPA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del acompañante del paciente al momento de la atención. Responsable o persona de apoyo. VARCHAR(100), PII.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'AccompanistName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre acompañante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'AccompanistName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'AccompanistName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el paciente se presenta acompañado a la atención. Presencia de tercero responsable. BIT, booleano.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'PatientComesAccompanied';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Paciente viene acompañado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'PatientComesAccompanied';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'PatientComesAccompanied';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de intentos previos: 1=Una vez, 2=Dos veces, 3=Tres veces, 4=Más de tres, 5=Sin dato. Frecuencia de conducta autolítica. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'NumberOfAttempts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de intentos:  1. Una vez  2. Dos veces  3. Tres veces  4. Más de tres veces  5. Sin dato  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'NumberOfAttempts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'NumberOfAttempts';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si existen antecedentes de intentos previos de suicidio o intoxicación intencional. BIT, booleano.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'PreviousAttempts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Intentos previos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'PreviousAttempts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'PreviousAttempts';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de ocurrencia del evento (intoxicación, intento suicida, consumo). Cuando sucedió el incidente. DATE, formato YYYY-MM-DD.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'OccurrenceDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de ocurrencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'OccurrenceDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'OccurrenceDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación inicial: 1=Ideación suicida, 2=Amenaza, 3=Intento suicida, 4=Suicidio consumado. Gravedad del evento de riesgo suicida. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'ClassificationOfInitialBehavior';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clasificacion inicial del caso:  1. Ideación suicida  2. Amenaza  3. Intento de suicidio  4. Suicidio consumado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'ClassificationOfInitialBehavior';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'ClassificationOfInitialBehavior';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel educativo: 1=Sin escuela, 2=Preescolar, 3=Primaria incompleta, 4=Primaria completa, 5=Secundaria incompleta, 6=Secundaria completa, 7-12=Técnico/Universidad/Postgrado, 13=Sin dato. Grado de formación académica. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'EducationLevel';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nivel educativo:  1. No fue a la escuela  2. Preescolar  3. Primaria incompleta  4. Primaria completa  5. Secundaria incompleta  6. Secundaria completa  7. Técnico post-secundaria incompleta  8. Técnico post-secundaria completa  9. Universidad incompleta  10. Universidad completa  11. Postgrado incompleto  12. Postgrado completo  13. Sin dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'EducationLevel';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'EducationLevel';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Municipio de procedencia del caso. Localidad donde se originó el evento notificado. VARCHAR(50), ubicación geográfica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'MunicipalityOriginOfCase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Municipio de procedencia del caso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'MunicipalityOriginOfCase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'MunicipalityOriginOfCase';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Departamento (estado/provincia) de procedencia del caso. Región administrativa donde ocurrió el evento. VARCHAR(50), ubicación territorial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'DepartmentOriginOfCase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Departamento de procedencia del caso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'DepartmentOriginOfCase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'DepartmentOriginOfCase';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'País de procedencia del caso. Nación donde se originó el evento reportado. VARCHAR(50), ubicación internacional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'CountryOriginOfCase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'País de procedencia del caso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'CountryOriginOfCase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'CountryOriginOfCase';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado civil: 1=Soltero(a), 2=Casado(a), 3=Divorciado(a), 4=Separado(a), 5=Viudo(a), 6=Unión libre, 7=Sin dato. Situación matrimonial del paciente. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'CivilStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado civil:  1. Soltero(a)  2. Casado(a)  3. Divorciado(a)  4. Separado(a)  5. Viudo(a)  6. Unión libre  7. Sin dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'CivilStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'CivilStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la ficha de notificación cabecera. Clave foránea que enlaza con tabla principal de notificaciones (HCFICHA356). INT, FK.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla cabebera de la ficha de notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro (consecutivo/PK). Número secuencial de identidad de la tabla HCFICHA356D. INT IDENTITY, clave primaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha de notificación 356 para eventos de conducta suicida (intentos de suicidio y lesiones autoinfligidas). Registra los datos clínicos, epidemiológicos y sociales del evento, incluyendo factores de riesgo, mecanismo utilizado, desencadenantes y derivación a salud mental, según el formulario oficial de vigilancia en salud pública.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356D';
