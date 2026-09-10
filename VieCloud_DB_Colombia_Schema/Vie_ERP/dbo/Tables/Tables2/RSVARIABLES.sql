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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Expresión o fórmula de cálculo automático cuando TIPO=8 (variable calculada). Hasta 250 caracteres. Ej: campo1 + campo2, (PAS+PAD)/2. Genera valor derivado de otras variables.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLES', @level2type = N'COLUMN', @level2name = N'VariableFormula';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la formula de expreciones cuano la variable es de tipo 8 -> Cálculo automático', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLES', @level2type = N'COLUMN', @level2name = N'VariableFormula';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLES', @level2type = N'COLUMN', @level2name = N'VariableFormula';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de escala o instrumento de valoración clínica (0-117). Ej: 1=CAGE, 2=APGAR Familiar, 3=EDPS, 24=Braden, 32=SOFA, 87=NIHSS, 110=Estratificación Riesgo Cardiovascular. Cataloga escalas estandarizadas de tamizaje, riesgo, severidad, funcionalidad y discapacidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLES', @level2type = N'COLUMN', @level2name = N'TIPOESCALA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'NoAplica = 0, EscalaCAGE = 1, EscalaAPGAR_Familiar = 2, EscalaEDPS = 3, EscalaBiopsicosocial = 4, EscalaTamizajeViolenciaDomestica = 5, EscalaRiesgoFramingham = 6, EscalaMorisky = 7, TestFINDRISC = 8, TestMiniMental = 9, TestDependenciaNicotina = 10, EscalaTannerDesarrolloMamarioMujer = 11, EscalaTannerDesarrolloVelloPubianoMujer = 12,    EscalaTannerDesarrolloGenitalHombre = 13, EscalaTannerDesarrolloVelloPubianoHombre = 14, EscalaWagner = 15, 
EscalaModificadaDisnea = 16, EscalaCAT_COPD_AssessmentTest = 17, 
Exacerbaciones = 18, ClasificacionEPOC = 19, TestGoodenough = 20, GOLD_EPOC = 21, EscalaAbreviadaDesarrollo = 22 ''''Guarda Detalle
EscalaTISS_28 = 23, Escala_Branden = 24, Escala_ApacheII = 25, 
Escala_Karnosfky = 26, Escala_Ecog = 27, Escala_Nems = 28, Escala_Glasgow_Mayor5Anos = 29, Escala_Glasgow_de1a5Anos = 30, 
Escala_Glasgow_Menor1Ano = 31, Escala_SOFA = 32, Escala_Charlson = 33, Escala_SAPS3 = 34, Escala_Barthel = 35, Escala_Morse = 36, Escala_Macdems = 37, Escala_NSRAS = 38, Escala_MSTS = 39, Escala_Person = 40, Escala_beck = 41, Escala_Zarit = 42, Escala_RQC = 43, Escala_BacterianaSilness = 44, Escala_VALE = 45, EscalaRASS = 46, EscalaDown_Adaptada = 47, EscalaNorton = 48, EscalaVass = 49, EscalaNutricion = 50, EscalaSQR = 51, EscalaM_CHAT = 52, EscalaWHOOLEY = 53, EscalaAUDIT = 54, EscalaLindaFried = 55, EscalaLawton_Brody = 56, EscalaGAD = 57, EscalaMNA = 58, EscalaMNASimplificada = 59, EscalaAssist = 60, EscalaNews = 61, EscalaCHA2DS2_VASc = 62, EscalaCRUSADE = 63, EscalaHAS_BLED = 64, EscalaHEMORR2HAGES = 65, EscalaEUROSCOREII = 66, EscalaNYHA = 67, EscalaKILLIP = 68, EscalaPADUA = 69, EscalaCAPRINI = 70, EscalaMUST = 71, Escala_STRONG_KIDS = 72, Escala_VGSDEN = 73, Escala_TIMI_CEST = 74, Escala_WELLS_TVP = 75, Escala_WELLS_TEP = 76,
Escala_NPC = 77, Escala_GRACE = 78, Escala_TIMI_SEST = 79, Escala_ANTHONISEN = 80, Escala_DAS_28 = 81, EscalaMRS = 82,    EscalaHAQ = 83, EscalaASPECT = 84, EscalaAbreviadaDesarrolloV3 = 85,
EscalaIndiceOLeary = 86, EscalaNIHSS = 87, EscalaHumptyDumpty = 88,
EscalaRiesgoEnfermedadesPotencialTransmisibles = 89, Escala_PIPP_R = 90, Escala_FLACC = 91, Escala_OFRAS = 92, Escala_FPSR = 93, Escala_NRS = 94, Escala_Gijon = 95, Escala_CAM = 96, Escala_Valoracion_Infeccion = 97, Escala_RiesgoFarmacologico = 98,    Escala_Valoracion_Riesgo_Psicosocial = 99, Escala_Gijon_Original = 100,
Escala_ObtetricadeAlerta_Temprana = 101, EscalaMPEWS = 102, EscalaBPEWS = 103, Escala_EventosTromboembolicosVenosos = 104,     Escala_Bishop = 105, Escala_Finnegan = 106, 
Escala_de_choque_y_respuesta_obstétrica = 107, Cuestionario_EPOC = 108, Escala_GAD2 = 109, Escala_Estratificacion_Riesgo_Cardiovascular = 110, Escala_Medicion_Grado_Tabaquismo = 111,  Escala_de_Silverman_Anderson = 112, Escala_Downton = 113, Escala_Alvarado = 114, Shock_Index = 115, EscalaEsGravE = 116, Escala_ABCD2_AIT = 117.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLES', @level2type = N'COLUMN', @level2name = N'TIPOESCALA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLES', @level2type = N'COLUMN', @level2name = N'TIPOESCALA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación del registro. Timestamp de auditoría. DATETIME nulo. Rastro de cambios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLES', @level2type = N'COLUMN', @level2name = N'FECHAMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLES', @level2type = N'COLUMN', @level2name = N'FECHAMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLES', @level2type = N'COLUMN', @level2name = N'FECHAMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código/identificación del usuario que modificó el registro. Hasta 20 caracteres. Nulo si no ha sido modificado. Auditoría.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLES', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo usuario modificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLES', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLES', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro. Timestamp de auditoría. Tipo DATETIME, no nulo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLES', @level2type = N'COLUMN', @level2name = N'FECHACREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLES', @level2type = N'COLUMN', @level2name = N'FECHACREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLES', @level2type = N'COLUMN', @level2name = N'FECHACREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código/identificación del usuario que creó el registro. Hasta 20 caracteres. Trazabilidad de auditoría de creación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLES', @level2type = N'COLUMN', @level2name = N'CODUSUCRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo usuario creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLES', @level2type = N'COLUMN', @level2name = N'CODUSUCRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLES', @level2type = N'COLUMN', @level2name = N'CODUSUCRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: si la variable numérica permite decimales (1=sí, 0=no). Controla precisión de valores capturados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLES', @level2type = N'COLUMN', @level2name = N'MANEJADECIMAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina si maneja decimal o no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLES', @level2type = N'COLUMN', @level2name = N'MANEJADECIMAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLES', @level2type = N'COLUMN', @level2name = N'MANEJADECIMAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad máxima en años para aplicar la variable. Límite superior de rango etario. Ej: 5 para pediátrico, 120 para sin límite.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLES', @level2type = N'COLUMN', @level2name = N'EDADMAX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad Máxima en años a la que aplica la variable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLES', @level2type = N'COLUMN', @level2name = N'EDADMAX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLES', @level2type = N'COLUMN', @level2name = N'EDADMAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad mínima en años para aplicar la variable. Límite inferior de rango etario. Ej: 0 para neonatos, 18 para adultos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLES', @level2type = N'COLUMN', @level2name = N'EDADMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad Minima en años a la que aplica la variable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLES', @level2type = N'COLUMN', @level2name = N'EDADMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLES', @level2type = N'COLUMN', @level2name = N'EDADMIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Género/sexo biológico al que aplica: 1=Masculino, 2=Femenino, 3=Ambos géneros. Filtro demográfico para relevancia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLES', @level2type = N'COLUMN', @level2name = N'GENERO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Genero a la que aplica la variable  1  - Masculino  2 - Femenino  3  - los dos generos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLES', @level2type = N'COLUMN', @level2name = N'GENERO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLES', @level2type = N'COLUMN', @level2name = N'GENERO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de dato de la variable: 1=Booleano (checkbox), 2=Texto (memo), 3=Numérico, 4=Lista (grid lookup), 5=Fecha/Hora. Define control UI de captura.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLES', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de variable  1 - BOOLEAN - CONTROL CHECK  2 - STRING - CONTROL MEMOEDIT  3 - NUMERICO - NUMERIC  4- LISTA - CONTROL GRIDLOOKUPEDIT  5- FECHA DATE TIME', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLES', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLES', @level2type = N'COLUMN', @level2name = N'TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o etiqueta descriptivo de la variable clínica. Texto de hasta 150 caracteres. Ej: presión arterial, frecuencia cardíaca, síntomas respiratorios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLES', @level2type = N'COLUMN', @level2name = N'VARIABLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la Variable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLES', @level2type = N'COLUMN', @level2name = N'VARIABLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLES', @level2type = N'COLUMN', @level2name = N'VARIABLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo de parámetros de revisión por sistemas. Referencia FK a RSGRUPO(ID). Agrupa variables por categoría clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLES', @level2type = N'COLUMN', @level2name = N'IDGRUPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id del grupo de parametros revision por sistemas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLES', @level2type = N'COLUMN', @level2name = N'IDGRUPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLES', @level2type = N'COLUMN', @level2name = N'IDGRUPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (IDENTITY) de la variable de revisión por sistemas. Clave primaria de la tabla RSVARIABLES.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLES', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLES', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLES', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Variables clínicas o de evaluación utilizadas en escalas de riesgo o formularios de salud. Cada variable tiene un grupo, tipo, rango de edad aplicable y puede incluir una fórmula de cálculo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLES';
