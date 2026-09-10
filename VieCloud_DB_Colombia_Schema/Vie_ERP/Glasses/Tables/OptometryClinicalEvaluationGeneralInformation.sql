CREATE TABLE [Glasses].[OptometryClinicalEvaluationGeneralInformation] (
    [Id]                                         INT            IDENTITY (1, 1) NOT NULL,
    [idOptometryClinicalEvaluationC]             INT            NOT NULL,
    [OphthalmologicalAnamnesisObservations]      VARCHAR (500)  NULL,
    [OphthalmologicalHistoryObservations]        VARCHAR (500)  NULL,
    [ExternalReviewObservations]                 VARCHAR (1000) NULL,
    [VisualAcuityObservations]                   VARCHAR (500)  NULL,
    [VisualAcuityCorrectionObservations]         VARCHAR (500)  NULL,
    [ObjectiveRefractionObservations]            VARCHAR (200)  NULL,
    [SubjectiveRefractionObservations]           VARCHAR (200)  NULL,
    [KeratometryRefractionObservations]          VARCHAR (200)  NULL,
    [AutokeratometryRefractionObservations]      VARCHAR (200)  NULL,
    [FurtherEvaluationPupilarExamObservations]   VARCHAR (200)  NULL,
    [FurtherEvaluationTonometryObservations]     VARCHAR (500)  NULL,
    [FurtherEvaluationColorVisionObservations]   VARCHAR (500)  NULL,
    [FurtherEvaluationContactologyObservations]  VARCHAR (200)  NULL,
    [VisualAcuityCorrection]                     TINYINT        NULL,
    [PupillaryDistance]                          VARCHAR (5)    NULL,
    [Dominance]                                  VARCHAR (200)  NULL,
    [OphthalmologicalDiagnoses]                  VARCHAR (8000) NULL,
    [Analysis]                                   VARCHAR (8000) NULL,
    [ManagementPlan]                             VARCHAR (8000) NULL,
    [WorkTeam]                                   VARCHAR (8000) NULL,
    [FurtherEvaluationBiomicroscopyObservations] VARCHAR (5000) NULL,
    [FurtherEvaluationFundusObservations]        VARCHAR (5000) NULL,
    [FurtherEvaluationGonioscopyObservations]    VARCHAR (500)  NULL,
    [FurtherEvaluationHasStereopsis]             TINYINT        NULL,
    [FurtherEvaluationStereopsisTechnique]       TINYINT        NULL,
    [FurtherEvaluationStereopsisArcSeconds]      VARCHAR (50)   NULL,
    CONSTRAINT [PK_OptometryClinicalEvaluationGeneralInformation] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OptometryClinicalEvaluationGeneralInformation_OptometryClinicalEvaluationC] FOREIGN KEY ([idOptometryClinicalEvaluationC]) REFERENCES [Glasses].[OptometryClinicalEvaluationC] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Arco de segundos estereopsis (visión estereoscópica). VARCHAR(50). Valor numérico que mide la capacidad de percepción de profundidad en segundos de arco. Traslado desde examen externo a valoración complementaria (San José 2023).', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'FurtherEvaluationStereopsisArcSeconds';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo Arco de segundos estereopsis (14-05-2023 - Debido a la actualización San José, este campo se crea ya que se traslada desde el page examen externo hasta valoración complementaria, pero al no tener dependencia con ninguna de las tablas creadas para ese page se crea el campo en esta tabla  -- Leonardo Rojas --)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'FurtherEvaluationStereopsisArcSeconds';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'FurtherEvaluationStereopsisArcSeconds';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Técnica de estereopsis (visión 3D) utilizada en valoración complementaria. TINYINT: 1=Tismus, 2=Random Dot, 3=Frisby, 4=Lang. Determina método de evaluación de profundidad. Traslado desde examen externo (San José 2023).', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'FurtherEvaluationStereopsisTechnique';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina la técnica usada para la esteriopsis: 1 Tismus, 2 Random, 3 Frisby, 4 Lang (14-05-2023 - Debido a la actualización San José, este campo se crea ya que se traslada desde el page examen externo hasta valoración complementaria, pero al no tener dependencia con ninguna de las tablas creadas para ese page se crea el campo en esta tabla  -- Leonardo Rojas --)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'FurtherEvaluationStereopsisTechnique';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'FurtherEvaluationStereopsisTechnique';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de presencia de estereopsis (visión de profundidad/3D) en el paciente. TINYINT: 1=Sí tiene estereopsis, 2=No tiene estereopsis. Evaluación de capacidad visual binocular.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'FurtherEvaluationHasStereopsis';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina si el paciente tiene esteriopsis: 1 Sí, 2 No (14-05-2023 - Debido a la actualización San José, este campo se crea ya que se traslada desde el page examen externo hasta valoración complementaria, pero al no tener dependencia con ninguna de las tablas creadas para ese page se crea el campo en esta tabla  -- Leonardo Rojas --)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'FurtherEvaluationHasStereopsis';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'FurtherEvaluationHasStereopsis';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dominancia ocular del paciente. VARCHAR(200). Determina ojo dominante (derecho/izquierdo) en funciones de visión y coordinación motora. Referencia en refracción y alineamiento.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'Dominance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dominancia', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'Dominance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'Dominance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Distancia pupilar (DP, IPD). VARCHAR(5). Medida en milímetros entre los centros de las pupilas. Requerida para centrado de lentes correctivos y gafas. Campo de refracción subjetiva.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'PupillaryDistance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del campo Distancia pupilar en grupo Refracción subjetiva (creado el 13-05-2023 por Leonardo Rojas - actualización San josé)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'PupillaryDistance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'PupillaryDistance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de corrección visual en uso actual del paciente. TINYINT: 1=Lentes de contacto, 2=Gafas/anteojos, 3=Ninguno (sin corrección). Documenta dispositivo óptico presente.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'VisualAcuityCorrection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina el uso de corrección actual: 1 Lentes de contacto, 2 Gafas, 3 Ninguno', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'VisualAcuityCorrection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'VisualAcuityCorrection';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones de Contactología en valoración complementaria. VARCHAR(200). Notas sobre adaptación, tolerancia y características de lentes de contacto. Campo deprecado post-actualización San José 2023.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'FurtherEvaluationContactologyObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones de Contactología del page Valoración complementaria (14-05-2023 - Este campo deja de ser utilizado tras actualización San José ya que el agrupador Contactología desaparece del page Valoracion Complementaria -- Leonardo Rojas --)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'FurtherEvaluationContactologyObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'FurtherEvaluationContactologyObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones de visión cromática (visión de colores) en valoración complementaria. VARCHAR(500). Evalúa percepción de cromaticidad y posibles defectos de daltonismo. Test de Ishihara u otros.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'FurtherEvaluationColorVisionObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones de Visión cromática del page Valoración complementaria', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'FurtherEvaluationColorVisionObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'FurtherEvaluationColorVisionObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones de tonometría en valoración complementaria. VARCHAR(500). Medición de presión intraocular (PIO). Detecta glaucoma y patologías de presión ocular.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'FurtherEvaluationTonometryObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones de Tonometría del page Valoración complementaria', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'FurtherEvaluationTonometryObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'FurtherEvaluationTonometryObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones de examen pupilar en valoración complementaria. VARCHAR(200). Evaluación de reflejo pupilar, forma, simetría y reactividad. Semiología neuroftalmológica.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'FurtherEvaluationPupilarExamObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones de Examen Pupilar del page Valoración complementaria', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'FurtherEvaluationPupilarExamObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'FurtherEvaluationPupilarExamObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones de autoqueratometría (refracción automática) en refracción. VARCHAR(200). Medida instrumental de curvatura corneal y componentes refractivos. Asistida por autorefractómetro.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'AutokeratometryRefractionObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones de Autoqueratometría de refracción', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'AutokeratometryRefractionObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'AutokeratometryRefractionObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones de queratometría de refracción. VARCHAR(200). Medición de curvatura corneal anterior (dioptrías). Base para cálculo de defectos refractivos y lentes.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'KeratometryRefractionObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones de Queratometría de Refracción', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'KeratometryRefractionObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'KeratometryRefractionObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones de refracción subjetiva. VARCHAR(200). Determinación de corrección óptica mediante respuesta del paciente (mejor visión). Incluye distancia pupilar y otros parámetros.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'SubjectiveRefractionObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones de Refracción subjetiva', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'SubjectiveRefractionObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'SubjectiveRefractionObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones de refracción objetiva. VARCHAR(200). Medición de error refractivo sin participación del paciente (retinoscopía). Determina miopía, hipermetropía, astigmatismo.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'ObjectiveRefractionObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones de Refracción objetiva', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'ObjectiveRefractionObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'ObjectiveRefractionObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones de corrección de agudeza visual. VARCHAR(500). Notas sobre mejora de visión con corrección óptica y tolerancia a lentes prescritos.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'VisualAcuityCorrectionObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones de Corrección de agudeza visual', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'VisualAcuityCorrectionObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'VisualAcuityCorrectionObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones de agudeza visual (AV, Snellen). VARCHAR(500). Evaluación de nitidez visual a distancia y cercana. Medida de capacidad de discriminación visual.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'VisualAcuityObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones de Agudeza visual', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'VisualAcuityObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'VisualAcuityObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones de examen externo (párpados, conjuntiva, córnea, cristalino anterior). VARCHAR(1000). Evaluación clínica de segmento anterior sin midriasis. Detecta patologías oculares externas.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'ExternalReviewObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones de Examen externo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'ExternalReviewObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'ExternalReviewObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones de antecedentes oftalmológicos. VARCHAR(500). Historial de patologías oculares previas, cirugías, traumas y condiciones crónicas del paciente.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'OphthalmologicalHistoryObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones de Antecedentes oftalmológicos del page Anamnesis / Antecedentes', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'OphthalmologicalHistoryObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'OphthalmologicalHistoryObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones de anamnesis oftalmológica. VARCHAR(500). Síntomas actuales y quejas visuales referidas por el paciente (visión borrosa, dolor, fotofobia, etc.).', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'OphthalmologicalAnamnesisObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones de Anamnesis oftalmológica del page Anamnesis / Antecedentes', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'OphthalmologicalAnamnesisObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'OphthalmologicalAnamnesisObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador FK de cabecera de valoración optométrica. INT. Referencia a tabla OptometryClinicalEvaluationC. Une detalles clínicos con evaluación general.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'idOptometryClinicalEvaluationC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de valoracion (OptometryClinicalEvaluationC)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'idOptometryClinicalEvaluationC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'idOptometryClinicalEvaluationC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) de registro en tabla. INT IDENTITY(1,1). Consecutivo secuencial de información clínica optométrica.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Información general de la evaluación clínica optométrica del paciente, incluyendo anamnesis, agudeza visual, refracción, queratometría, exámenes complementarios, diagnósticos oftalmológicos y plan de manejo. Corresponde al registro clínico de la consulta de optometría u oftalmología.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diagnósticos oftalmológicos registrados por el optómetra u oftalmólogo durante la evaluación clínica, como miopía, astigmatismo, glaucoma, entre otros.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'OphthalmologicalDiagnoses';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'OphthalmologicalDiagnoses';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Análisis clínico del profesional de la salud visual con base en los hallazgos de la evaluación optométrica; interpretación del caso del paciente.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'Analysis';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'Analysis';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plan de manejo o tratamiento indicado para el paciente, que puede incluir formulación óptica, medicamentos, remisiones o controles oftalmológicos.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'ManagementPlan';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'ManagementPlan';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Equipo de trabajo o profesionales de la salud involucrados en la atención del paciente durante la evaluación clínica optométrica.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'WorkTeam';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'WorkTeam';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones del examen de biomicroscopía (lámpara de hendidura), que evalúa estructuras del segmento anterior del ojo como córnea, cristalino y conjuntiva.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'FurtherEvaluationBiomicroscopyObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'FurtherEvaluationBiomicroscopyObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones del examen de fondo de ojo (fundoscopia), que permite evaluar retina, nervio óptico y vasos sanguíneos oculares.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'FurtherEvaluationFundusObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'FurtherEvaluationFundusObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones del examen de gonioscopía, utilizado para evaluar el ángulo iridocorneal y detectar condiciones como glaucoma de ángulo cerrado o abierto.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'FurtherEvaluationGonioscopyObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryClinicalEvaluationGeneralInformation', @level2type = N'COLUMN', @level2name = N'FurtherEvaluationGonioscopyObservations';
