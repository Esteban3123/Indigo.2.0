CREATE TABLE [dbo].[RSVARIABLES] (
    [ID]              INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDGRUPO]         INT           NOT NULL,
    [VARIABLE]        VARCHAR (150) NOT NULL,
    [TIPO]            TINYINT       NOT NULL,
    [GENERO]          TINYINT       NOT NULL,
    [EDADMIN]         INT           NOT NULL,
    [EDADMAX]         INT           NOT NULL,
    [MANEJADECIMAL]   BIT           NULL,
    [CODUSUCRE]       CHAR (20)     NOT NULL,
    [FECHACREA]       DATETIME      NOT NULL,
    [CODUSUMOD]       CHAR (20)     NULL,
    [FECHAMOD]        DATETIME      NULL,
    [TIPOESCALA]      TINYINT       NULL,
    [VariableFormula] VARCHAR (250) NULL,
    CONSTRAINT [PK_RSVARIABLES] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_RSVARIABLES_RSGRUPO] FOREIGN KEY ([IDGRUPO]) REFERENCES [dbo].[RSGRUPO] ([ID])
);


GO
ALTER TABLE [dbo].[RSVARIABLES] NOCHECK CONSTRAINT [FK_RSVARIABLES_RSGRUPO];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda la formula de expreciones cuano la variable es de tipo 8 -> Cálculo automático', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLES', @level2type = N'COLUMN', @level2name = N'VariableFormula';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NoAplica = 0, EscalaCAGE = 1, EscalaAPGAR_Familiar = 2, EscalaEDPS = 3, EscalaBiopsicosocial = 4, EscalaTamizajeViolenciaDomestica = 5, EscalaRiesgoFramingham = 6, EscalaMorisky = 7, TestFINDRISC = 8, TestMiniMental = 9, TestDependenciaNicotina = 10, EscalaTannerDesarrolloMamarioMujer = 11, EscalaTannerDesarrolloVelloPubianoMujer = 12,    EscalaTannerDesarrolloGenitalHombre = 13, EscalaTannerDesarrolloVelloPubianoHombre = 14, EscalaWagner = 15, 
EscalaModificadaDisnea = 16, EscalaCAT_COPD_AssessmentTest = 17, 
Exacerbaciones = 18, ClasificacionEPOC = 19, TestGoodenough = 20, GOLD_EPOC = 21, EscalaAbreviadaDesarrollo = 22 ''Guarda Detalle
EscalaTISS_28 = 23, Escala_Branden = 24, Escala_ApacheII = 25, 
Escala_Karnosfky = 26, Escala_Ecog = 27, Escala_Nems = 28, Escala_Glasgow_Mayor5Anos = 29, Escala_Glasgow_de1a5Anos = 30, 
Escala_Glasgow_Menor1Ano = 31, Escala_SOFA = 32, Escala_Charlson = 33, Escala_SAPS3 = 34, Escala_Barthel = 35, Escala_Morse = 36, Escala_Macdems = 37, Escala_NSRAS = 38, Escala_MSTS = 39, Escala_Person = 40, Escala_beck = 41, Escala_Zarit = 42, Escala_RQC = 43, Escala_BacterianaSilness = 44, Escala_VALE = 45, EscalaRASS = 46, EscalaDown_Adaptada = 47, EscalaNorton = 48, EscalaVass = 49, EscalaNutricion = 50, EscalaSQR = 51, EscalaM_CHAT = 52, EscalaWHOOLEY = 53, EscalaAUDIT = 54, EscalaLindaFried = 55, EscalaLawton_Brody = 56, EscalaGAD = 57, EscalaMNA = 58, EscalaMNASimplificada = 59, EscalaAssist = 60, EscalaNews = 61, EscalaCHA2DS2_VASc = 62, EscalaCRUSADE = 63, EscalaHAS_BLED = 64, EscalaHEMORR2HAGES = 65, EscalaEUROSCOREII = 66, EscalaNYHA = 67, EscalaKILLIP = 68, EscalaPADUA = 69, EscalaCAPRINI = 70, EscalaMUST = 71, Escala_STRONG_KIDS = 72, Escala_VGSDEN = 73, Escala_TIMI_CEST = 74, Escala_WELLS_TVP = 75, Escala_WELLS_TEP = 76,
Escala_NPC = 77, Escala_GRACE = 78, Escala_TIMI_SEST = 79, Escala_ANTHONISEN = 80, Escala_DAS_28 = 81, EscalaMRS = 82,    EscalaHAQ = 83, EscalaASPECT = 84, EscalaAbreviadaDesarrolloV3 = 85,
EscalaIndiceOLeary = 86, EscalaNIHSS = 87, EscalaHumptyDumpty = 88,
EscalaRiesgoEnfermedadesPotencialTransmisibles = 89, Escala_PIPP_R = 90, Escala_FLACC = 91, Escala_OFRAS = 92, Escala_FPSR = 93, Escala_NRS = 94, Escala_Gijon = 95, Escala_CAM = 96, Escala_Valoracion_Infeccion = 97, Escala_RiesgoFarmacologico = 98,    Escala_Valoracion_Riesgo_Psicosocial = 99, Escala_Gijon_Original = 100,
Escala_ObtetricadeAlerta_Temprana = 101, EscalaMPEWS = 102, EscalaBPEWS = 103, Escala_EventosTromboembolicosVenosos = 104,     Escala_Bishop = 105, Escala_Finnegan = 106, 
Escala_de_choque_y_respuesta_obstétrica = 107, Cuestionario_EPOC = 108, Escala_GAD2 = 109, Escala_Estratificacion_Riesgo_Cardiovascular = 110, Escala_Medicion_Grado_Tabaquismo = 111,  Escala_de_Silverman_Anderson = 112, Escala_Downton = 113, Escala_Alvarado = 114, Shock_Index = 115, EscalaEsGravE = 116, Escala_ABCD2_AIT = 117.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLES', @level2type = N'COLUMN', @level2name = N'TIPOESCALA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Genero a la que aplica la variable  1  - Masculino  2 - Femenino  3  - los dos generos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLES', @level2type = N'COLUMN', @level2name = N'GENERO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de variable  1 - BOOLEAN - CONTROL CHECK  2 - STRING - CONTROL MEMOEDIT  3 - NUMERICO - NUMERIC  4- LISTA - CONTROL GRIDLOOKUPEDIT  5- FECHA DATE TIME', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLES', @level2type = N'COLUMN', @level2name = N'TIPO';

