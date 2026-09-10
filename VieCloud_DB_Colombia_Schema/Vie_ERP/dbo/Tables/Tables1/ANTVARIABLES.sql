CREATE TABLE [dbo].[ANTVARIABLES] (
    [ID]              INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODANTECEDENTE]  INT           NOT NULL,
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
    CONSTRAINT [PK_ANTVARIABLES] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Expresión/fórmula de cálculo automático (VARCHAR 250, nullable): cuando TIPO=8, contiene la lógica o ecuación para calcular el valor de la variable de manera automática en base a otras variables', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLES', @level2type = N'COLUMN', @level2name = N'VariableFormula';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la formula de expreciones cuano la variable es de tipo 8 -> Cálculo automático', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLES', @level2type = N'COLUMN', @level2name = N'VariableFormula';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLES', @level2type = N'COLUMN', @level2name = N'VariableFormula';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de escala/test clínico estandarizado (TINYINT, nullable): 0=No aplica, 1=CAGE, 2=APGAR Familiar, 3=EDPS, 4=Biopsicosocial, 5=Tamizaje Violencia Doméstica, 6=Riesgo Framingham, 7=Morisky, 8=FINDRISC, 9=Mini-Mental, 10=Dependencia Nicotina, 11-14=Tanner, 15=Wagner, 16=Disnea Modificada, 17=CAT COPD, 18=Exacerbaciones, 19=Clasificación EPOC, 20=Goodenough, 21=GOLD EPOC, 22=Desarrollo Abreviado, 23=TISS-28, 24=Braden, 25=Apache II, 26=Karnofsky, 27=ECOG, 28=NEMS, 29-31=Glasgow, 32=SOFA, 33=Charlson, 34=SAPS3, 35=Barthel, 36=Morse, 37=Macdems, 38=NSRAS, 39=MSTS, 40=Person, 41=Beck, 42=Zarit, 43=RQC, 44=Bacteria Silness, 45=VALE, 46=RASS, 47=Down, 48=Norton, 49=Vass, 50=Nutrición, 51=SQR, 52=M-CHAT, 53=WHO-5, 54=AUDIT, 55=Linda Fried, 56=Lawton-Brody, 57=GAD, 58=MNA, 59=MNA Simplificada, 60=ASSIST, 61=NEWS, 62=CHA2DS2-VASc, 63=CRUSADE, 64=HAS-BLED, 65=HEMORR2HAGES, 66=EUROSCORE II, 67=NYHA, 68=KILLIP, 69=PADUA, 70=CAPRINI', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLES', @level2type = N'COLUMN', @level2name = N'TIPOESCALA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'NoAplica = 0     EscalaCAGE = 1     EscalaAPGAR_Familiar = 2     EscalaEDPS = 3     EscalaBiopsicosocial = 4     EscalaTamizajeViolenciaDomestica = 5     EscalaRiesgoFramingham = 6     EscalaMorisky = 7     TestFINDRISC = 8     TestMiniMental = 9     TestDependenciaNicotina = 10     EscalaTannerDesarrolloMamarioMujer = 11     EscalaTannerDesarrolloVelloPubianoMujer = 12     EscalaTannerDesarrolloGenitalHombre = 13     EscalaTannerDesarrolloVelloPubianoHombre = 14     EscalaWagner = 15     EscalaModificadaDisnea = 16     EscalaCAT_COPD_AssessmentTest = 17     Exacerbaciones = 18     ClasificacionEPOC = 19 ''''Guarda Detalle     TestGoodenough = 20     GOLD_EPOC = 21     EscalaAbreviadaDesarrollo = 22 ''''Guarda Detalle     EscalaTISS_28 = 23     Escala_Branden = 24     Escala_ApacheII = 25     Escala_Karnosfky = 26     Escala_Ecog = 27     Escala_Nems = 28     Escala_Glasgow_Mayor5Anos = 29 ''''Se guarda en otra tabla     Escala_Glasgow_de1a5Anos = 30 ''''Se guarda en otra tabla     Escala_Glasgow_Menor1Ano = 31 ''''Se guarda en otra tabla      Escala_SOFA = 32     Escala_Charlson = 33     Escala_SAPS3 = 34     Escala_Barthel = 35     Escala_Morse = 36     Escala_Macdems = 37     Escala_NSRAS = 38     Escala_MSTS = 39     Escala_Person = 40     Escala_beck = 41     Escala_Zarit = 42     Escala_RQC = 43     Escala_BacterianaSilness = 44     Escala_VALE = 45 ''''Se guarda en otra tabla     EscalaRASS = 46     EscalaDown = 47     EscalaNorton = 48     EscalaVass = 49     EscalaNutricion = 50     EscalaSQR = 51 ''''Guarda detalle     EscalaM_CHAT = 52     EscalaWHOOLEY = 53     EscalaAUDIT = 54     EscalaLindaFried = 55     EscalaLawton_Brody = 56     EscalaGAD = 57     EscalaMNA = 58     EscalaMNASimplificada = 59     EscalaAssist = 60     EscalaNews = 61     EscalaCHA2DS2_VASc = 62     EscalaCRUSADE = 63     EscalaHAS-BLED = 64     EscalaHEMORR2HAGES = 65     EscalaEUROSCOREII = 66     EscalaNYHA  = 67     EscalaKILLIP = 68     EscalaPADUA = 69     EscalaCAPRINI = 70', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLES', @level2type = N'COLUMN', @level2name = N'TIPOESCALA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLES', @level2type = N'COLUMN', @level2name = N'TIPOESCALA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación (DATETIME, nullable): timestamp de cuándo se actualizó la variable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLES', @level2type = N'COLUMN', @level2name = N'FECHAMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLES', @level2type = N'COLUMN', @level2name = N'FECHAMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLES', @level2type = N'COLUMN', @level2name = N'FECHAMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código/usuario que modificó el registro (CHAR 20, nullable): auditoría de última edición de la variable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLES', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo usuario modificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLES', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLES', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro (DATETIME): timestamp de cuándo se registró la variable en ANTVARIABLES', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLES', @level2type = N'COLUMN', @level2name = N'FECHACREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLES', @level2type = N'COLUMN', @level2name = N'FECHACREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLES', @level2type = N'COLUMN', @level2name = N'FECHACREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código/usuario que creó el registro de variable (CHAR 20): auditoría de quién definió la variable en el sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLES', @level2type = N'COLUMN', @level2name = N'CODUSUCRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo usuario creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLES', @level2type = N'COLUMN', @level2name = N'CODUSUCRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLES', @level2type = N'COLUMN', @level2name = N'CODUSUCRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT): especifica si la variable numérica maneja decimales o solo enteros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLES', @level2type = N'COLUMN', @level2name = N'MANEJADECIMAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina si maneja decimal o no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLES', @level2type = N'COLUMN', @level2name = N'MANEJADECIMAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLES', @level2type = N'COLUMN', @level2name = N'MANEJADECIMAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad máxima en años para aplicar la variable (INT): rango etario superior de validación en paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLES', @level2type = N'COLUMN', @level2name = N'EDADMAX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad Máxima en años a la que aplica la variable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLES', @level2type = N'COLUMN', @level2name = N'EDADMAX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLES', @level2type = N'COLUMN', @level2name = N'EDADMAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad mínima en años para aplicar la variable (INT): rango etario inferior de validación en paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLES', @level2type = N'COLUMN', @level2name = N'EDADMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad Minima en años a la que aplica la variable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLES', @level2type = N'COLUMN', @level2name = N'EDADMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLES', @level2type = N'COLUMN', @level2name = N'EDADMIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Género al que aplica la variable (TINYINT): 1=Masculino, 2=Femenino, 3=Ambos géneros; determina elegibilidad en pacientes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLES', @level2type = N'COLUMN', @level2name = N'GENERO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Genero a la que aplica la variable  1  - Masculino  2 - Femenino  3  - los dos generos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLES', @level2type = N'COLUMN', @level2name = N'GENERO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLES', @level2type = N'COLUMN', @level2name = N'GENERO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de control/dato de la variable (TINYINT): 1=Booleano/Checkbox, 2=Texto/Memo, 3=Numérico, 4=Lista/GridLookup, 5=Fecha/DateEdit, 8=Cálculo automático', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLES', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de variable  1 - BOOLEAN - CONTROL CHECK  2 - STRING - CONTROL MEMOEDIT  3 - NUMERICO - NUMERIC  4- LISTA - CONTROL GRIDLOOKUPEDIT  5- FECHA - DATEDIT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLES', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLES', @level2type = N'COLUMN', @level2name = N'TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o etiqueta de la variable de antecedente (VARCHAR 150): denominación que utiliza el profesional de salud para registrar datos clínicos en la historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLES', @level2type = N'COLUMN', @level2name = N'VARIABLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la Variable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLES', @level2type = N'COLUMN', @level2name = N'VARIABLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLES', @level2type = N'COLUMN', @level2name = N'VARIABLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del tipo de antecedente clínico: médicos, quirúrgicos, anestésicos, transfusionales, inmunológicos, alérgicos, traumáticos, psicológicos, farmacológicos, familiares, gineco-obstétricos, urológico-sexual, perinatales, tóxicos, hábitos de vida, esquema de vacunación, escolares, laborales, nutricionales, odontológicos, socioeconómicos u otros (INT 1-22)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLES', @level2type = N'COLUMN', @level2name = N'CODANTECEDENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Antecedente:  1-Médicos  2-Quirurgicio  3-Anestésico  4-Transfunsionales  5-Inmunológicos  6-Alérgicos  7-Traumáticos  8-Psicológicos  9-Farmacológicos  10-Familiares  11-Gineco-Obstétricos  12-Urológico-sexual  13-Perinatales  14-Tóxicos  15-Hábitos de Vida  16-Esquema de Vacunación  17-Escolores  18-Laborales  19-Nutricionales  20-Odontológicos  21-SocioEconómicos  22- Otros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLES', @level2type = N'COLUMN', @level2name = N'CODANTECEDENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLES', @level2type = N'COLUMN', @level2name = N'CODANTECEDENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (INT IDENTITY) de la variable de antecedente en el sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLES', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLES', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLES', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Variables clínicas asociadas a tipos de antecedentes del paciente (por ejemplo, variables de antecedentes familiares, personales o patológicos), con sus rangos de edad, género aplicable, tipo de dato y fórmula de escala para la captura estructurada en historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVARIABLES';
