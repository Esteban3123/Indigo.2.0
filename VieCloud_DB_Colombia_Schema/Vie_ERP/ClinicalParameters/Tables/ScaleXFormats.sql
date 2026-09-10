CREATE TABLE [ClinicalParameters].[ScaleXFormats] (
    [Id]                       INT IDENTITY (1, 1) NOT NULL,
    [IdClinicalHistoryFormats] INT NOT NULL,
    [CodeScale]                INT NOT NULL,
    [Mandatory]                BIT NOT NULL,
    CONSTRAINT [PK_ScaleXFormats] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que marca si la escala clínica es obligatoria en el formato de historia; registro requerido para validación de formularios de atención.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ScaleXFormats', @level2type = N'COLUMN', @level2name = N'Mandatory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Registro obligatorio', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ScaleXFormats', @level2type = N'COLUMN', @level2name = N'Mandatory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ScaleXFormats', @level2type = N'COLUMN', @level2name = N'Mandatory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código numérico de identificación de la escala clínica empleada (1-95): CAGE, APGAR Familiar, EDPS, Biopsicosocial, Tamizaje Violencia Doméstica, Framingham, Morisky, FINDRISK, Mini Mental, Dependencia Nicotina, Tanner (desarrollo), Wagner, Disnea Modificada, CAT-COPD, EPOC, Goodenough, GOLD, Desarrollo Abreviada, TISS-28, Braden, Apache II, Karnofsky, ECOG, NEMS, Glasgow (edades), SOFA, Charlson, SAPS3, Barthel, Morse, Macdems, NSRAS, MSTS, Person, Beck, Zarit, RQC, Silness, VALE, RASS, Down, Norton, Vass, Nutrición, SQR, M-CHAT, WHO-OLEY, AUDIT, Linda Fried, Lawton-Brody, GAD-7, MNA, Assist, NEWS, CHA2DS2-VASc, CRUSADE, HAS-BLED, HEMORR2HAGES, EUROSCORE II, NYHA, KILLIP, PADUA, CAPRINI, MUST, Strong Kids, VGSDEN, TIMI, Wells, NPC, GRACE, Anthonisen, DAS-28, MRS, HAQ, ASPECT, Índice O''''Leary, NIHSS, Humpty Dumpty, Riesgo Enfermedades Transmisibles, PIPP-R, FLACC, OFRAS, FPSR, Fugulin, CPOT.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ScaleXFormats', @level2type = N'COLUMN', @level2name = N'CodeScale';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' EscalaCAGE = 1      EscalaAPGAR_Familiar = 2      EscalaEDPS = 3      EscalaBiopsicosocial = 4      EscalaTamizajeViolenciaDomestica = 5      EscalaRiesgoFramingham = 6      EscalaMorisky = 7      TestFINDRISC = 8      TestMiniMental = 9      TestDependenciaNicotina = 10      EscalaTannerDesarrolloMamarioMujer = 11      EscalaTannerDesarrolloVelloPubianoMujer = 12      EscalaTannerDesarrolloGenitalHombre = 13      EscalaTannerDesarrolloVelloPubianoHombre = 14      EscalaWagner = 15      EscalaModificadaDisnea = 16      EscalaCAT_COPD_AssessmentTest = 17      Exacerbaciones = 18      ClasificacionEPOC = 19 ''''Guarda Detalle      TestGoodenough = 20      GOLD_EPOC = 21      EscalaAbreviadaDesarrollo = 22 ''''Guarda Detalle      EscalaTISS_28 = 23      Escala_Branden = 24      Escala_ApacheII = 25      Escala_Karnosfky = 26      Escala_Ecog = 27      Escala_Nems = 28      Escala_Glasgow_Mayor5Anos = 29 ''''Se guarda en otra tabla      Escala_Glasgow_de1a5Anos = 30 ''''Se guarda en otra tabla      Escala_Glasgow_Menor1Ano = 31 ''''Se guarda en otra tabla       Escala_SOFA = 32      Escala_Charlson = 33      Escala_SAPS3 = 34      Escala_Barthel = 35      Escala_Morse = 36      Escala_Macdems = 37      Escala_NSRAS = 38      Escala_MSTS = 39      Escala_Person = 40      Escala_beck = 41      Escala_Zarit = 42      Escala_RQC = 43      Escala_BacterianaSilness = 44      Escala_VALE = 45 ''''Se guarda en otra tabla      EscalaRASS = 46      EscalaDown = 47      EscalaNorton = 48      EscalaVass = 49      EscalaNutricion = 50      EscalaSQR = 51 ''''Guarda detalle      EscalaM_CHAT = 52      EscalaWHOOLEY = 53      EscalaAUDIT = 54      EscalaLindaFried = 55      EscalaLawton_Brody = 56      EscalaGAD = 57      EscalaMNA = 58      EscalaMNASimplificada = 59      EscalaAssist = 60      EscalaNews = 61      EscalaCHA2DS2_VASc = 62      EscalaCRUSADE = 63      EscalaHAS_BLED = 64      EscalaHEMORR2HAGES = 65      EscalaEUROSCOREII = 66      EscalaNYHA = 67      EscalaKILLIP = 68      EscalaPADUA = 69      EscalaCAPRINI = 70      EscalaMUST = 71      Escala_STRONG_KIDS = 72      Escala_VGSDEN = 73  '''' VGSDEN = VALORACIÓN GLOBAL SUBJETIVA DEL ESTADO NUTRICIONAL       Escala_TIMI_CEST = 74      Escala_WELLS_TVP = 75      Escala_WELLS_TEP = 76      Escala_NPC = 77      Escala_GRACE = 78      Escala_TIMI_SEST = 79      Escala_ANTHONISEN = 80      Escala_DAS_28 = 81      EscalaMRS = 82      EscalaHAQ = 83      EscalaASPECT = 84      EscalaAbreviadaDesarrolloV3 = 85      EscalaIndiceOLeary = 86      EscalaNIHSS = 87      EscalaHumptyDumpty = 88      EscalaRiesgoEnfermedadesPotencialTransmisibles = 89      Escala_PIPP_R = 90   '''' PIPP-R (The Premature Infant Pain Profile: Revised)      Escala_FLACC = 91 '''' FLACC (Face, Legs, Activity, Cry, Controlability)      Escala_OFRAS = 92      Escala_FPSR = 93      Escala_Fugulin = 94      EscalaCPOT = 95 ''''Critical-Care Pain Observation Tool', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ScaleXFormats', @level2type = N'COLUMN', @level2name = N'CodeScale';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ScaleXFormats', @level2type = N'COLUMN', @level2name = N'CodeScale';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) que referencia la tabla ClinicalHistoryFormats; vincula cada escala al formato específico de historia clínica donde se aplicará.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ScaleXFormats', @level2type = N'COLUMN', @level2name = N'IdClinicalHistoryFormats';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relación con la tabla ClinicalHistoryPages', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ScaleXFormats', @level2type = N'COLUMN', @level2name = N'IdClinicalHistoryFormats';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ScaleXFormats', @level2type = N'COLUMN', @level2name = N'IdClinicalHistoryFormats';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único secuencial (INT IDENTITY) de la asociación entre escala clínica y formato de historia; clave primaria de la tabla.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ScaleXFormats', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo del registro', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ScaleXFormats', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ScaleXFormats', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los formatos de historia clínica con las escalas de valoración clínica que deben aplicarse en cada formato, indicando si la escala es obligatoria o no.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ScaleXFormats';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ScaleXFormats';
