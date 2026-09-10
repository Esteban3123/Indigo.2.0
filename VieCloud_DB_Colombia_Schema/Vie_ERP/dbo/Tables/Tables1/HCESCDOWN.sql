CREATE TABLE [dbo].[HCESCDOWN] (
    [AUTO]       INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCENATE]  NCHAR (10)                                                                       NULL,
    [UFUCODIGO]  NCHAR (10)                                                                       NULL,
    [IPCODPACI]  VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NULL,
    [NUMINGRES]  CHAR (10)                                                                        NULL,
    [EDADPACIE]  TINYINT                                                                          NULL,
    [SELECIEDA]  TINYINT                                                                          NULL,
    [CAIDPREVI]  TINYINT                                                                          NULL,
    [TRANQUILI]  TINYINT                                                                          NULL,
    [DIURETICO]  TINYINT                                                                          NULL,
    [HIPOTENSO]  TINYINT                                                                          NULL,
    [ANTIPARKI]  TINYINT                                                                          NULL,
    [ANTIDEPRE]  TINYINT                                                                          NULL,
    [SELECIMED]  TINYINT                                                                          NULL,
    [MEDICAMEN]  VARCHAR (MAX)                                                                    NULL,
    [ALTERAVIS]  TINYINT                                                                          NULL,
    [ALTERAUDI]  TINYINT                                                                          NULL,
    [ICTUEXTRE]  TINYINT                                                                          NULL,
    [ESTADOMEN]  TINYINT                                                                          NULL,
    [SEGURAYUD]  TINYINT                                                                          NULL,
    [INSEGAYUD]  TINYINT                                                                          NULL,
    [IMPOSIBLE]  TINYINT                                                                          NULL,
    [PATOLOGIA]  TINYINT                                                                          NULL,
    [NUTRICION]  TINYINT                                                                          NULL,
    [FECREGSIS]  DATETIME                                                                         NULL,
    [CODPROSAL]  CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NULL,
    [CODESPECI]  CHAR (3)                                                                         NULL,
    [RESULTADO]  INT                                                                              NULL,
    [TIPOESCALA] INT                                                                              NULL,
    [NUMEFOLIO]  NCHAR (10)                                                                       NULL,
    CONSTRAINT [PK_HCESCDOWN] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_HCESCDOWN_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCESCDOWN_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCESCDOWN_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCESCDOWN].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCESCDOWN].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
CREATE NONCLUSTERED INDEX [_dta_index_HCESCDOWN_7_2071782538__K4_K5_K3_6_7_8_9_10_11_12_13_14_16_17_18_19_20_21_22_23_24]
    ON [dbo].[HCESCDOWN]([IPCODPACI] ASC, [NUMINGRES] ASC, [UFUCODIGO] ASC)
    INCLUDE([ALTERAUDI], [ALTERAVIS], [ANTIDEPRE], [ANTIPARKI], [CAIDPREVI], [DIURETICO], [EDADPACIE], [ESTADOMEN], [HIPOTENSO], [ICTUEXTRE], [IMPOSIBLE], [INSEGAYUD], [NUTRICION], [PATOLOGIA], [SEGURAYUD], [SELECIEDA], [SELECIMED], [TRANQUILI]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Folio/número de escala Down Adaptada vinculado a tabla HCESCALAS; identificador de seguimiento de tamizaje', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Folio de escala relaciona con la columna NUMEFOLIO de la tabla HCESCALAS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo/clasificación de escala clínica aplicada (0=NoAplica hasta 110=Estratificación Riesgo Cardiovascular); catálogo de instrumentos de evaluación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'TIPOESCALA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el tipo de escala (
NoAplica = 0
EscalaCAGE = 1
EscalaAPGAR_Familiar = 2
EscalaEDPS = 3
EscalaBiopsicosocial = 4
EscalaTamizajeViolenciaDomestica = 5
EscalaRiesgoFramingham = 6
EscalaMorisky = 7
TestFINDRISC = 8
TestMiniMental = 9
TestDependenciaNicotina = 10
EscalaTannerDesarrolloMamarioMujer = 11
EscalaTannerDesarrolloVelloPubianoMujer = 12
EscalaTannerDesarrolloGenitalHombre = 13
EscalaTannerDesarrolloVelloPubianoHombre = 14
EscalaWagner = 15
EscalaModificadaDisnea = 16
EscalaCAT_COPD_AssessmentTest = 17
Exacerbaciones = 18
ClasificacionEPOC = 19
TestGoodenough = 20
GOLD_EPOC = 21
EscalaAbreviadaDesarrollo = 22
EscalaTISS_28 = 23
Escala_Branden = 24
Escala_ApacheII = 25
Escala_Karnosfky = 26
Escala_Ecog = 27
Escala_Nems = 28
Escala_Glasgow_Mayor5Anos = 29
Escala_Glasgow_de1a5Anos = 30
Escala_Glasgow_Menor1Ano = 31
Escala_SOFA = 32
Escala_Charlson = 33
Escala_SAPS3 = 34
Escala_Barthel = 35
Escala_Morse = 36
Escala_Macdems = 37
Escala_NSRAS = 38
Escala_MSTS = 39
Escala_Person = 40
Escala_beck = 41
Escala_Zarit = 42
Escala_RQC = 43
Escala_BacterianaSilness = 44
Escala_VALE = 45
EscalaRASS = 46
EscalaDown_Adaptada = 47
EscalaNorton = 48
EscalaVass = 49
EscalaNutricion = 50
EscalaSQR = 51
EscalaM_CHAT = 52
EscalaWHOOLEY = 53
EscalaAUDIT = 54
EscalaLindaFried = 55
EscalaLawton_Brody = 56
EscalaGAD = 57
EscalaMNA = 58
EscalaMNASimplificada = 59
EscalaAssist = 60
EscalaNews = 61
EscalaCHA2DS2_VASc = 62
EscalaCRUSADE = 63
EscalaHAS_BLED = 64
EscalaHEMORR2HAGES = 65
EscalaEUROSCOREII = 66
EscalaNYHA = 67
EscalaKILLIP = 68
EscalaPADUA = 69
EscalaCAPRINI = 70
EscalaMUST = 71
Escala_STRONG_KIDS = 72
Escala_VGSDEN = 73 
Escala_TIMI_CEST = 74
Escala_WELLS_TVP = 75
Escala_WELLS_TEP = 76
Escala_NPC = 77
Escala_GRACE = 78
Escala_TIMI_SEST = 79
Escala_ANTHONISEN = 80
Escala_DAS_28 = 81
EscalaMRS = 82
EscalaHAQ = 83
EscalaASPECT = 84
EscalaAbreviadaDesarrolloV3 = 85
EscalaIndiceOLeary = 86
EscalaNIHSS = 87
EscalaHumptyDumpty = 88
EscalaRiesgoEnfermedadesPotencialTransmisibles = 89
Escala_PIPP_R = 90
Escala_FLACC = 91
Escala_OFRAS = 92
Escala_FPSR = 93
Escala_NRS = 94
Escala_Gijon = 95
Escala_CAM = 96
Escala_Valoracion_Infeccion = 97
Escala_RiesgoFarmacologico = 98
Escala_Valoracion_Riesgo_Psicosocial = 99
Escala_Gijon_Original = 100
Escala_ObtetricadeAlerta_Temprana = 101
EscalaMPEWS = 102
EscalaBPEWS = 103
Escala_EventosTromboembolicosVenosos = 104
Escala_Bishop = 105
Escala_Finnegan = 106
Escala_de_choque_y_respuesta_obstétrica = 107
Cuestionario_EPOC = 108
Escala_GAD2 = 109
Escala_Estratificacion_Riesgo_Cardiovascular = 110
Escala_Me
)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'TIPOESCALA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'TIPOESCALA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado/desenlace de escala Down Adaptada: 0=Asintomático, 1=Sin discapacidad, 2=Discapacidad leve, 3=Moderada, 4=Moderadamente grave, 5=Grave, 6=Muerte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el resultado (0 Asintomático, 1 Sin discapacidad significativa, 2 Discapacidad leve, 3 Discapacidad moderada, 4 Discapacidad moderadamente grave, 5 Discapacidad grave, 6 Muerte)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'RESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código especialidad (FK INESPECIA); especialidad médica/enfermería que diligencia escala de tamizaje', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guardamos la especialidad del medico o enfermera que diligencia la Escala. ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código profesional de salud PII ofuscado (FK INPROFES); identificación médico/enfermera que registra escala', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del profesional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha/datetime registro en sistema de escala Down Adaptada; timestamp auditoría', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntaje/score estado nutricional en escala; evaluación componente nutricional paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'NUTRICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Puntaje estado nutricional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'NUTRICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'NUTRICION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntaje/score patología; evaluación presencia/severidad condición patológica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'PATOLOGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Puntaje Patologia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'PATOLOGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'PATOLOGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntaje/score imposible; ítem indica imposibilidad realizar evaluación/prueba', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'IMPOSIBLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Puntaje Imposible', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'IMPOSIBLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'IMPOSIBLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntaje/score camina inseguro con ayuda; evaluación movilidad dependiente paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'INSEGAYUD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Puntaje camina inseguro con ayuda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'INSEGAYUD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'INSEGAYUD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntaje/score camina seguro con ayuda; evaluación marcha asistida estable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'SEGURAYUD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Puntaje camina seguro con ayuda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'SEGURAYUD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'SEGURAYUD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntaje/score estado mental; evaluación cognición, orientación, alerta paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'ESTADOMEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Puntaje Estado Mental', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'ESTADOMEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'ESTADOMEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntaje/score ICTU/extremidades; evaluación afectación motriz miembros superiores/inferiores', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'ICTUEXTRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Puntaje ICTU', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'ICTUEXTRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'ICTUEXTRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntaje/score alteración auditiva; evaluación pérdida audición/hipoacusia paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'ALTERAUDI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Puntaje Alteracion Auditiva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'ALTERAUDI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'ALTERAUDI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntaje/score alteración visual; evaluación déficit visión/ceguera paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'ALTERAVIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Puntaje Alteracion Visual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'ALTERAVIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'ALTERAVIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Medicamentos adicionales/otros; descripción VARCHAR MAX fármacos complementarios recibidos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'MEDICAMEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Otros medicamentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'MEDICAMEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'MEDICAMEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Selección medicamentos otros; indicador binario presencia medicación complementaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'SELECIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Seleccion otros medicamentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'SELECIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'SELECIMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntaje/score antidepresivos; evaluación uso medicación antidepresiva paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'ANTIDEPRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Puntaje Antidepresivos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'ANTIDEPRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'ANTIDEPRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntaje/score antiparkinson; evaluación medicación enfermedad Parkinson', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'ANTIPARKI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Puntaje Antiparkinson', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'ANTIPARKI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'ANTIPARKI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntaje/score hipotensores; evaluación medicación antihipertensiva/presión arterial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'HIPOTENSO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Puntaje hipotensores', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'HIPOTENSO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'HIPOTENSO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntaje/score diuréticos; evaluación medicación diurética recibida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'DIURETICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Puntaje Diureticos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'DIURETICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'DIURETICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntaje/score tranquilizantes; evaluación medicación sedante/ansiolítica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'TRANQUILI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Puntaje Tranquilizantes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'TRANQUILI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'TRANQUILI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntaje/score caídas previas; evaluación antecedentes caídas/riesgo caída paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'CAIDPREVI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Puntaje Caidas Previas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'CAIDPREVI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'CAIDPREVI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Selección edad; indicador binario categoría etaria/grupo edad paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'SELECIEDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Seleccion Edad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'SELECIEDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'SELECIEDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntaje/score edad paciente; evaluación factor edad en escala riesgo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'EDADPACIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Puntaje edad del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'EDADPACIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'EDADPACIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número ingreso; FK ADINGRESO; identificador único atención/internación paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'número del ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código paciente PII ofuscado; FK INPACIENT; identificación cédula/documento paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código unidad funcional; departamento/servicio clínico donde se aplica escala', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código centro de atención; institución/hospital sede evaluación paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'código den centro de atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autonumérico/identificador único; clave primaria identidad registro escala', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registros de la escala de valoración de riesgo de caídas aplicada a pacientes durante su ingreso o atención. Almacena los factores de riesgo evaluados (medicamentos, alteraciones sensoriales, movilidad, etc.), el resultado de la escala y el profesional que la realizó.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCDOWN';
