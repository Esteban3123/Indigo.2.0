CREATE TABLE [dbo].[OTVARIABLES] (
    [ID]              INT           IDENTITY (1, 1) NOT NULL,
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
    CONSTRAINT [PK_OTVARIABLES] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_OTVARIABLES_OTGRUPO] FOREIGN KEY ([IDGRUPO]) REFERENCES [dbo].[OTGRUPO] ([ID])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Expresión o fórmula de cálculo automático (VARCHAR 250) cuando TIPO=8. Contiene lógica matemática para derivar variable calculada desde otros parámetros clínicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVARIABLES', @level2type = N'COLUMN', @level2name = N'VariableFormula';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la formula de expreciones cuano la variable es de tipo 8 -> Cálculo automático', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVARIABLES', @level2type = N'COLUMN', @level2name = N'VariableFormula';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVARIABLES', @level2type = N'COLUMN', @level2name = N'VariableFormula';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de escala o instrumento clínico estandarizado (0=NoAplica, 1=CAGE, 2=APGAR_Familiar, 3=EDPS, 4=Biopsicosocial, 5=Tamizaje Violencia, 6=Framingham, 7=Morisky, 8=FINDRISK, 9=MiniMental, 10=Nicotina, 11-14=Tanner, 15=Wagner, 16=Disnea, 17=CAT_COPD, 18=Exacerbaciones, 19=Clasificación EPOC, 20=Goodenough, 21=GOLD_EPOC, 22=Desarrollo Abreviada, 23=TISS_28, 24=Branden, 25=ApacheII, 26=Karnosfky, 27=Ecog, 28=Nems, 29-31=Glasgow (edad), 32=SOFA, 33=Charlson, 34=SAPS3, 35=Barthel, 36=Morse, 37=Macdems, 38=NSRAS, 39=MSTS, 40=Person, 41=Beck, 42=Zarit, 43=RQC, 44=BacterianaSilness, 45=VALE, 46=RASS, 47=Down, 48=Norton, 49=Vass, 50=Nutrición, 51=SQR, 52=M_CHAT, 53=WHOOLEY, 54=AUDIT, 55=LindaFried, 56=Lawton_Brody, 57=GAD, 58=MNA, 59=MNA_Simplificada, 60=Assist, 61=News, 62=CHA2DS2_VASc, 63=CRUSADE, 64=HAS-BLED, 65=HEMORR2HAGES, 66=EUROSCOREII, 67=NYHA, 68=KILLIP, 69=PADUA, 70=CAPRINI).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVARIABLES', @level2type = N'COLUMN', @level2name = N'TIPOESCALA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'NoAplica = 0     EscalaCAGE = 1     EscalaAPGAR_Familiar = 2     EscalaEDPS = 3     EscalaBiopsicosocial = 4     EscalaTamizajeViolenciaDomestica = 5     EscalaRiesgoFramingham = 6     EscalaMorisky = 7     TestFINDRISC = 8     TestMiniMental = 9     TestDependenciaNicotina = 10     EscalaTannerDesarrolloMamarioMujer = 11     EscalaTannerDesarrolloVelloPubianoMujer = 12     EscalaTannerDesarrolloGenitalHombre = 13     EscalaTannerDesarrolloVelloPubianoHombre = 14     EscalaWagner = 15     EscalaModificadaDisnea = 16     EscalaCAT_COPD_AssessmentTest = 17     Exacerbaciones = 18     ClasificacionEPOC = 19 ''''Guarda Detalle     TestGoodenough = 20     GOLD_EPOC = 21     EscalaAbreviadaDesarrollo = 22 ''''Guarda Detalle     EscalaTISS_28 = 23     Escala_Branden = 24     Escala_ApacheII = 25     Escala_Karnosfky = 26     Escala_Ecog = 27     Escala_Nems = 28     Escala_Glasgow_Mayor5Anos = 29 ''''Se guarda en otra tabla     Escala_Glasgow_de1a5Anos = 30 ''''Se guarda en otra tabla     Escala_Glasgow_Menor1Ano = 31 ''''Se guarda en otra tabla      Escala_SOFA = 32     Escala_Charlson = 33     Escala_SAPS3 = 34     Escala_Barthel = 35     Escala_Morse = 36     Escala_Macdems = 37     Escala_NSRAS = 38     Escala_MSTS = 39     Escala_Person = 40     Escala_beck = 41     Escala_Zarit = 42     Escala_RQC = 43     Escala_BacterianaSilness = 44     Escala_VALE = 45 ''''Se guarda en otra tabla     EscalaRASS = 46     EscalaDown = 47     EscalaNorton = 48     EscalaVass = 49     EscalaNutricion = 50     EscalaSQR = 51 ''''Guarda detalle     EscalaM_CHAT = 52     EscalaWHOOLEY = 53     EscalaAUDIT = 54     EscalaLindaFried = 55     EscalaLawton_Brody = 56     EscalaGAD = 57     EscalaMNA = 58     EscalaMNASimplificada = 59     EscalaAssist = 60     EscalaNews = 61     EscalaCHA2DS2_VASc = 62     EscalaCRUSADE = 63     EscalaHAS-BLED = 64     EscalaHEMORR2HAGES = 65     EscalaEUROSCOREII = 66     EscalaNYHA  = 67     EscalaKILLIP = 68     EscalaPADUA = 69     EscalaCAPRINI = 70', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVARIABLES', @level2type = N'COLUMN', @level2name = N'TIPOESCALA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVARIABLES', @level2type = N'COLUMN', @level2name = N'TIPOESCALA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación en DATETIME. Marca temporal de cambios en definición del parámetro (nullable).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVARIABLES', @level2type = N'COLUMN', @level2name = N'FECHAMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVARIABLES', @level2type = N'COLUMN', @level2name = N'FECHAMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVARIABLES', @level2type = N'COLUMN', @level2name = N'FECHAMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de usuario que realizó la última modificación. Auditoría de cambios (PII: Identification_Ofuscado, nullable).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVARIABLES', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo usuario modificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVARIABLES', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVARIABLES', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro en DATETIME. Marca temporal de inclusión del parámetro en catálogo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVARIABLES', @level2type = N'COLUMN', @level2name = N'FECHACREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVARIABLES', @level2type = N'COLUMN', @level2name = N'FECHACREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVARIABLES', @level2type = N'COLUMN', @level2name = N'FECHACREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de usuario que creó el registro. Auditoría de origen del parámetro (PII: Identification_Ofuscado).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVARIABLES', @level2type = N'COLUMN', @level2name = N'CODUSUCRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo usuario creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVARIABLES', @level2type = N'COLUMN', @level2name = N'CODUSUCRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVARIABLES', @level2type = N'COLUMN', @level2name = N'CODUSUCRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que especifica si la variable admite precisión decimal en su valor numérico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVARIABLES', @level2type = N'COLUMN', @level2name = N'MANEJADECIMAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina si maneja decimal o no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVARIABLES', @level2type = N'COLUMN', @level2name = N'MANEJADECIMAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVARIABLES', @level2type = N'COLUMN', @level2name = N'MANEJADECIMAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad máxima en años para aplicación válida de la variable. Define límite superior del rango etario en pacientes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVARIABLES', @level2type = N'COLUMN', @level2name = N'EDADMAX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad Máxima en años a la que aplica la variable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVARIABLES', @level2type = N'COLUMN', @level2name = N'EDADMAX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVARIABLES', @level2type = N'COLUMN', @level2name = N'EDADMAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad mínima en años para aplicación válida de la variable. Define límite inferior del rango etario en pacientes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVARIABLES', @level2type = N'COLUMN', @level2name = N'EDADMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad Minima en años a la que aplica la variable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVARIABLES', @level2type = N'COLUMN', @level2name = N'EDADMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVARIABLES', @level2type = N'COLUMN', @level2name = N'EDADMIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Aplicabilidad por sexo: 1=Masculino, 2=Femenino, 3=Ambos géneros. Define rango poblacional para la variable clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVARIABLES', @level2type = N'COLUMN', @level2name = N'GENERO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Genero a la que aplica la variable  1  - Masculino  2 - Femenino  3  - los dos generos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVARIABLES', @level2type = N'COLUMN', @level2name = N'GENERO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVARIABLES', @level2type = N'COLUMN', @level2name = N'GENERO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de dato y control UI: 1=BOOLEAN/CHECK, 2=STRING/MEMOEDIT, 3=NUMERICO, 4=LISTA/GRIDLOOKUPEDIT, 5=FECHA/DATETIME.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVARIABLES', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de variable  1 - BOOLEAN - CONTROL CHECK  2 - STRING - CONTROL MEMOEDIT  3 - NUMERICO - NUMERIC  4- LISTA - CONTROL GRIDLOOKUPEDIT  5- FECHA DATE TIMETipo de variable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVARIABLES', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVARIABLES', @level2type = N'COLUMN', @level2name = N'TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo de la variable clínica, parámetro o signo vital que se captura en la atención (ej: presión arterial, frecuencia cardíaca, saturación oxígeno).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVARIABLES', @level2type = N'COLUMN', @level2name = N'VARIABLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la Variable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVARIABLES', @level2type = N'COLUMN', @level2name = N'VARIABLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVARIABLES', @level2type = N'COLUMN', @level2name = N'VARIABLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo de parámetros (FK a OTGRUPO). Agrupa variables relacionadas en categorías temáticas para organización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVARIABLES', @level2type = N'COLUMN', @level2name = N'IDGRUPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id del grupo de parametros otros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVARIABLES', @level2type = N'COLUMN', @level2name = N'IDGRUPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVARIABLES', @level2type = N'COLUMN', @level2name = N'IDGRUPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (IDENTITY) de la variable clínica en el catálogo de parámetros de Otros.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVARIABLES', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVARIABLES', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVARIABLES', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Variables clínicas u operativas agrupadas por categoría, con configuración de tipo de dato, género, rango de edad aplicable y fórmula de cálculo. Se usan para definir los parámetros o indicadores que se capturan en formularios de historia clínica o escalas de valoración.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVARIABLES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVARIABLES';
