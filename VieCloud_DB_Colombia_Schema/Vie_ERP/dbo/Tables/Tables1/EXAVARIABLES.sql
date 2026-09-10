CREATE TABLE [dbo].[EXAVARIABLES] (
    [ID]              INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDEXAGRUPO]      INT           NOT NULL,
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
    CONSTRAINT [PK_EXAVARIABLES] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_EXAVARIABLES_EXAGRUPO] FOREIGN KEY ([IDEXAGRUPO]) REFERENCES [dbo].[EXAGRUPO] ([ID])
);


GO
ALTER TABLE [dbo].[EXAVARIABLES] NOCHECK CONSTRAINT [FK_EXAVARIABLES_EXAGRUPO];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Expresión o fórmula de cálculo automático (VARCHAR 250) utilizada cuando TIPO=8; permite derivar valor de otras variables', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLES', @level2type = N'COLUMN', @level2name = N'VariableFormula';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la formula de expreciones cuano la variable es de tipo 8 -> Cálculo automático', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLES', @level2type = N'COLUMN', @level2name = N'VariableFormula';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLES', @level2type = N'COLUMN', @level2name = N'VariableFormula';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de escala clínica aplicable (0-70): CAGE, APGAR Familiar, EDPS, Biopsicosocial, Violencia Doméstica, Framingham, Morisky, FINDRISK, MiniMental, Nicotina, Tanner, Wagner, Disnea, CAT-COPD, EPOC, Goodenough, GOLD, Desarrollo, TISS-28, Braden, Apache II, Karnofsky, ECOG, NEMS, Glasgow (>5años/1-5años/<1año), SOFA, Charlson, SAPS3, Barthel, Morse, Macdems, NSRAS, MSTS, Person, Beck, Zarit, RQC, Silness, VALE, RASS, Down, Norton, VASS, Nutrición, SQR, M-CHAT, WHOOOLEY, AUDIT, Fried, Lawton-Brody, GAD-7, MNA, Assist, NEWS, CHA2DS2-VASc, CRUSADE, HAS-BLED, HEMORR2HAGES, EUROSCORE II, NYHA, KILLIP, PADUA, CAPRINI', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLES', @level2type = N'COLUMN', @level2name = N'TIPOESCALA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'NoAplica = 0     EscalaCAGE = 1     EscalaAPGAR_Familiar = 2     EscalaEDPS = 3     EscalaBiopsicosocial = 4     EscalaTamizajeViolenciaDomestica = 5     EscalaRiesgoFramingham = 6     EscalaMorisky = 7     TestFINDRISC = 8     TestMiniMental = 9     TestDependenciaNicotina = 10     EscalaTannerDesarrolloMamarioMujer = 11     EscalaTannerDesarrolloVelloPubianoMujer = 12     EscalaTannerDesarrolloGenitalHombre = 13     EscalaTannerDesarrolloVelloPubianoHombre = 14     EscalaWagner = 15     EscalaModificadaDisnea = 16     EscalaCAT_COPD_AssessmentTest = 17     Exacerbaciones = 18     ClasificacionEPOC = 19 ''''Guarda Detalle     TestGoodenough = 20     GOLD_EPOC = 21     EscalaAbreviadaDesarrollo = 22 ''''Guarda Detalle     EscalaTISS_28 = 23     Escala_Branden = 24     Escala_ApacheII = 25     Escala_Karnosfky = 26     Escala_Ecog = 27     Escala_Nems = 28     Escala_Glasgow_Mayor5Anos = 29 ''''Se guarda en otra tabla     Escala_Glasgow_de1a5Anos = 30 ''''Se guarda en otra tabla     Escala_Glasgow_Menor1Ano = 31 ''''Se guarda en otra tabla      Escala_SOFA = 32     Escala_Charlson = 33     Escala_SAPS3 = 34     Escala_Barthel = 35     Escala_Morse = 36     Escala_Macdems = 37     Escala_NSRAS = 38     Escala_MSTS = 39     Escala_Person = 40     Escala_beck = 41     Escala_Zarit = 42     Escala_RQC = 43     Escala_BacterianaSilness = 44     Escala_VALE = 45 ''''Se guarda en otra tabla     EscalaRASS = 46     EscalaDown = 47     EscalaNorton = 48     EscalaVass = 49     EscalaNutricion = 50     EscalaSQR = 51 ''''Guarda detalle     EscalaM_CHAT = 52     EscalaWHOOLEY = 53     EscalaAUDIT = 54     EscalaLindaFried = 55     EscalaLawton_Brody = 56     EscalaGAD = 57     EscalaMNA = 58     EscalaMNASimplificada = 59     EscalaAssist = 60     EscalaNews = 61     EscalaCHA2DS2_VASc = 62     EscalaCRUSADE = 63     EscalaHAS-BLED = 64     EscalaHEMORR2HAGES = 65     EscalaEUROSCOREII = 66     EscalaNYHA  = 67     EscalaKILLIP = 68     EscalaPADUA = 69     EscalaCAPRINI = 70', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLES', @level2type = N'COLUMN', @level2name = N'TIPOESCALA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLES', @level2type = N'COLUMN', @level2name = N'TIPOESCALA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Timestamp de última modificación; auditoria de cuándo se actualizó la variable; nullable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLES', @level2type = N'COLUMN', @level2name = N'FECHAMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLES', @level2type = N'COLUMN', @level2name = N'FECHAMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLES', @level2type = N'COLUMN', @level2name = N'FECHAMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de usuario modificador, identificación (PII Identification_Ofuscado) de último cambio; nullable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLES', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo usuario modificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLES', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLES', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Timestamp de creación del registro; auditoria de cuándo se definió la variable en el sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLES', @level2type = N'COLUMN', @level2name = N'FECHACREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLES', @level2type = N'COLUMN', @level2name = N'FECHACREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLES', @level2type = N'COLUMN', @level2name = N'FECHACREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de usuario creador, identificación (PII Identification_Ofuscado) de quién registró la variable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLES', @level2type = N'COLUMN', @level2name = N'CODUSUCRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo usuario creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLES', @level2type = N'COLUMN', @level2name = N'CODUSUCRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLES', @level2type = N'COLUMN', @level2name = N'CODUSUCRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (Bit) que determina si la variable numérica acepta decimales (precisión); SQL DECIMAL o INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLES', @level2type = N'COLUMN', @level2name = N'MANEJADECIMAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina si maneja decimal o no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLES', @level2type = N'COLUMN', @level2name = N'MANEJADECIMAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLES', @level2type = N'COLUMN', @level2name = N'MANEJADECIMAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad máxima en años a la que aplica clínicamente la variable; define rango etario superior de validación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLES', @level2type = N'COLUMN', @level2name = N'EDADMAX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad Máxima en años a la que aplica la variable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLES', @level2type = N'COLUMN', @level2name = N'EDADMAX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLES', @level2type = N'COLUMN', @level2name = N'EDADMAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad mínima en años a la que aplica clínicamente la variable; define rango etario inferior de validación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLES', @level2type = N'COLUMN', @level2name = N'EDADMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad Minima en años a la que aplica la variable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLES', @level2type = N'COLUMN', @level2name = N'EDADMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLES', @level2type = N'COLUMN', @level2name = N'EDADMIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Aplicabilidad por sexo/género: 1=Masculino, 2=Femenino, 3=Ambos géneros; define relevancia clínica por sexo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLES', @level2type = N'COLUMN', @level2name = N'GENERO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Genero a la que aplica la variable  1  - Masculino  2 - Femenino  3  - los dos generos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLES', @level2type = N'COLUMN', @level2name = N'GENERO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLES', @level2type = N'COLUMN', @level2name = N'GENERO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de dato de la variable: 1=Booleano/Control Check, 2=Texto/Memo, 3=Numérico, 4=Lista/Lookup, 5=Fecha/DateTime', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLES', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de variable  1 - BOOLEAN - CONTROL CHECK  2 - STRING - CONTROL MEMOEDIT  3 - NUMERICO - NUMERIC  4- LISTA - CONTROL GRIDLOOKUPEDIT  5- FECHA DATE TIME', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLES', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLES', @level2type = N'COLUMN', @level2name = N'TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o denominación de la variable de examen clínico, utilizado en formularios y reportes de atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLES', @level2type = N'COLUMN', @level2name = N'VARIABLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la Variable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLES', @level2type = N'COLUMN', @level2name = N'VARIABLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLES', @level2type = N'COLUMN', @level2name = N'VARIABLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo de parámetros de revisión por sistemas (FK→EXAGRUPO), agrupa variables por categoría clínica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLES', @level2type = N'COLUMN', @level2name = N'IDEXAGRUPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id del grupo de parametros revision por sistemas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLES', @level2type = N'COLUMN', @level2name = N'IDEXAGRUPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLES', @level2type = N'COLUMN', @level2name = N'IDEXAGRUPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (INT IDENTITY) de la variable de examen en el sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLES', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLES', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLES', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Variables o parámetros de laboratorio/exámenes clínicos que conforman un grupo de examen. Define qué se mide en cada examen (hemoglobina, glucosa, etc.), el tipo de dato, el género al que aplica, los rangos de edad válidos y si maneja decimales o escala.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLES';
