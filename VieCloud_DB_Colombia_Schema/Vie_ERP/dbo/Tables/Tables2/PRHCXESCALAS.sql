CREATE TABLE [dbo].[PRHCXESCALAS] (
    [ID]         INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDMODELOHC] INT NULL,
    [TIPOESCALA] INT NULL,
    [MANDATORY]  BIT CONSTRAINT [DF_PRHCXESCALAS_MANDATORY] DEFAULT ((0)) NOT NULL,
    [Sex]        INT NULL,
    [MinimumAge] INT NULL,
    [MaximumAge] INT NULL,
    CONSTRAINT [PK_PRHCXESCALAS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PRHCXESCALAS_PRMODELOHC] FOREIGN KEY ([IDMODELOHC]) REFERENCES [dbo].[PRMODELOHC] ([ID])
);


GO
ALTER TABLE [dbo].[PRHCXESCALAS] NOCHECK CONSTRAINT [FK_PRHCXESCALAS_PRMODELOHC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad máxima en meses del paciente para aplicación de la escala (criterio de inclusión poblacional).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXESCALAS', @level2type = N'COLUMN', @level2name = N'MaximumAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad máxima en meses.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXESCALAS', @level2type = N'COLUMN', @level2name = N'MaximumAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXESCALAS', @level2type = N'COLUMN', @level2name = N'MaximumAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad mínima en meses del paciente para aplicación de la escala (criterio de inclusión poblacional).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXESCALAS', @level2type = N'COLUMN', @level2name = N'MinimumAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad mínima en meses.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXESCALAS', @level2type = N'COLUMN', @level2name = N'MinimumAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXESCALAS', @level2type = N'COLUMN', @level2name = N'MinimumAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Filtro demográfico de sexo/género del paciente: 1=Masculino, 2=Femenino, 3=Ambos (requerimiento poblacional para la escala).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXESCALAS', @level2type = N'COLUMN', @level2name = N'Sex';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sexo: 1 - Masculino  2 - Femenino  3 - Ambos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXESCALAS', @level2type = N'COLUMN', @level2name = N'Sex';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXESCALAS', @level2type = N'COLUMN', @level2name = N'Sex';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador binario (BIT 0/1) que marca si la escala es obligatoria (1) u opcional (0) en el modelo de historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXESCALAS', @level2type = N'COLUMN', @level2name = N'MANDATORY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si el registro de la escala es o no obligatoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXESCALAS', @level2type = N'COLUMN', @level2name = N'MANDATORY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXESCALAS', @level2type = N'COLUMN', @level2name = N'MANDATORY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de escala clínica (INT 0-117): CAGE, APGAR Familiar, EDPS, Biopsicosocial, Violencia Doméstica, Framingham, Morisky, FINDRISK, Mini Mental, Nicotina, Tanner, Wagner, Disnea, CAT-COPD, EPOC, Goodenough, GOLD, TISS-28, Braden, Apache II, Karnofsky, ECOG, NEMS, Glasgow, SOFA, Charlson, SAPS3, Barthel, Morse, Macdems, NSRAS, MSTS, Person, Beck, Zarit, RQC, Silness, VALE, RASS, Down, Norton, Vass, Nutrición, SQR, M-CHAT, WHOOOLEY, AUDIT, Linda Fried, Lawton-Brody, GAD, MNA, ASSIST, NEWS, CHA2DS2-VASc, CRUSADE, HAS-BLED, HEMORR2HAGES, EUROSCORE II, NYHA, KILLIP, PADUA, CAPRINI, MUST, STRONG KIDS, VGSDEN, TIMI CEST, WELLS TVP, WELLS TEP, NPC, GRACE, TIMI SEST, ANTHONISEN, DAS-28, MRS, HAQ, ASPECT, Índice O''''Leary, NIHSS, Humpty Dumpty, Enfermedades Transmisibles, PIPP-R, FLACC, OFRAS, FPSR, NRS, Gijón, CAM, Infección, Farmacológico, Psicosocial, Bishop, Finnegan, Shock-Obstétrico, EPOC, GAD2, Cardiovascular, Tabaquismo, Silverman-Anderson, Downton, Alvarado, Shock Index, Es-Grave, ABCD2-AIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXESCALAS', @level2type = N'COLUMN', @level2name = N'TIPOESCALA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el tipo de escala
 NoAplica = 0,
 EscalaCAGE = 1,
 EscalaAPGAR_Familiar = 2,
 EscalaEDPS = 3,
 EscalaBiopsicosocial = 4,
 EscalaTamizajeViolenciaDomestica = 5,
 EscalaRiesgoFramingham = 6,
 EscalaMorisky = 7,
 TestFINDRISC = 8,
 TestMiniMental = 9,
 TestDependenciaNicotina = 10,
 EscalaTannerDesarrolloMamarioMujer = 11,
 EscalaTannerDesarrolloVelloPubianoMujer = 12,
 EscalaTannerDesarrolloGenitalHombre = 13,
 EscalaTannerDesarrolloVelloPubianoHombre = 14,
 EscalaWagner = 15,
 EscalaModificadaDisnea = 16,
 EscalaCAT_COPD_AssessmentTest = 17,
 Exacerbaciones = 18,
 ClasificacionEPOC = 19,
 TestGoodenough = 20,
 GOLD_EPOC = 21,
 EscalaAbreviadaDesarrollo = 22,
 EscalaTISS_28 = 23,
 Escala_Branden = 24,
 Escala_ApacheII = 25,
 Escala_Karnosfky = 26,
 Escala_Ecog = 27,
 Escala_Nems = 28,
 Escala_Glasgow_Mayor5Anos = 29,
 Escala_Glasgow_de1a5Anos = 30,
 Escala_Glasgow_Menor1Ano = 31,
 Escala_SOFA = 32,
 Escala_Charlson = 33,
 Escala_SAPS3 = 34,
 Escala_Barthel = 35,
 Escala_Morse = 36,
 Escala_Macdems = 37,
 Escala_NSRAS = 38,
 Escala_MSTS = 39,
 Escala_Person = 40,
 Escala_beck = 41,
 Escala_Zarit = 42,
 Escala_RQC = 43,
 Escala_BacterianaSilness = 44,
 Escala_VALE = 45,
 EscalaRASS = 46,
 EscalaDown_Adaptada = 47,
 EscalaNorton = 48,
 EscalaVass = 49,
 EscalaNutricion = 50,
 EscalaSQR = 51,
 EscalaM_CHAT = 52,
 EscalaWHOOLEY = 53,
 EscalaAUDIT = 54,
 EscalaLindaFried = 55,
 EscalaLawton_Brody = 56,
 EscalaGAD = 57,
 EscalaMNA = 58,
 EscalaMNASimplificada = 59,
 EscalaAssist = 60,
 EscalaNews = 61,
 EscalaCHA2DS2_VASc = 62,
 EscalaCRUSADE = 63,
 EscalaHAS_BLED = 64,
 EscalaHEMORR2HAGES = 65,
 EscalaEUROSCOREII = 66,
 EscalaNYHA = 67,
 EscalaKILLIP = 68,
 EscalaPADUA = 69,
 EscalaCAPRINI = 70,
 EscalaMUST = 71,
 Escala_STRONG_KIDS = 72,
 Escala_VGSDEN = 73,
 Escala_TIMI_CEST = 74,
 Escala_WELLS_TVP = 75,
 Escala_WELLS_TEP = 76,
 Escala_NPC = 77,
 Escala_GRACE = 78,
 Escala_TIMI_SEST = 79,
 Escala_ANTHONISEN = 80,
 Escala_DAS_28 = 81,
 EscalaMRS = 82,
 EscalaHAQ = 83,
 EscalaASPECT = 84,
 EscalaAbreviadaDesarrolloV3 = 85,
 EscalaIndiceOLeary = 86,
 EscalaNIHSS = 87,
 EscalaHumptyDumpty = 88,
 EscalaRiesgoEnfermedadesPotencialTransmisibles = 89,
 Escala_PIPP_R = 90,
 Escala_FLACC = 91,
 Escala_OFRAS = 92,
 Escala_FPSR = 93,
 Escala_NRS = 94,
 Escala_Gijon = 95,
 Escala_CAM = 96,
 Escala_Valoracion_Infeccion = 97,
 Escala_RiesgoFarmacologico = 98,
 Escala_Valoracion_Riesgo_Psicosocial = 99,
 Escala_Gijon_Original = 100,
 Escala_ObtetricadeAlerta_Temprana = 101,
 EscalaMPEWS = 102,
 EscalaBPEWS = 103,
 Escala_EventosTromboembolicosVenosos = 104,
 Escala_Bishop = 105,
 Escala_Finnegan = 106,
 Escala_de_choque_y_respuesta_obstétrica = 107,
 Cuestionario_EPOC = 108,
 Escala_GAD2 = 109,
 Escala_Estratificacion_Riesgo_Cardiovascular = 110,
 Escala_Medicion_Grado_Tabaquismo = 111,
 Escala_de_Silverman_Anderson = 112,
 Escala_Downton = 113,
 Escala_Alvarado = 114,
 Shock_Index = 115,
 EscalaEsGravE = 116,
 Escala_ABCD2_AIT = 117', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXESCALAS', @level2type = N'COLUMN', @level2name = N'TIPOESCALA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXESCALAS', @level2type = N'COLUMN', @level2name = N'TIPOESCALA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia de clave foránea a tabla PRMODELOHC; vincula la escala al modelo de historia clínica específico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXESCALAS', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'tiene relación con la tabla PRMODELOHC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXESCALAS', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXESCALAS', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único consecutivo (INT IDENTITY) de la escala en el modelo de historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXESCALAS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXESCALAS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXESCALAS', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración de escalas clínicas asociadas a modelos de historia clínica. Define qué escalas de valoración (por ejemplo, escalas de dolor, riesgo, funcionalidad) aplican a cada modelo, indicando si son obligatorias y los criterios de aplicación según sexo y rango de edad del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXESCALAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXESCALAS';
