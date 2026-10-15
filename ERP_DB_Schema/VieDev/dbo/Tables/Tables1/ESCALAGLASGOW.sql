CREATE TABLE [dbo].[ESCALAGLASGOW] (
    [ID]              INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NUMINGRES]       CHAR (10)                                                                        NOT NULL,
    [IPCODPACI]       VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [FECHAREGISTRO]   DATETIME                                                                         NOT NULL,
    [TIPOESCALA]      INT                                                                              NOT NULL,
    [APERTURAOCULAR]  INT                                                                              NOT NULL,
    [RESPUESTAVERVAL] INT                                                                              NOT NULL,
    [RESPUESTAMOTORA] INT                                                                              NOT NULL,
    [RESULTADO]       INT                                                                              NOT NULL,
    [CODPROSAL]       CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NULL,
    [CODCENATE]       CHAR (10)                                                                        NULL,
    [UFUCODIGO]       CHAR (10)                                                                        NULL,
    [IDHCESCALAS]     INT                                                                              NULL,
    [CODESPECI]       CHAR (3)                                                                         NULL,
    CONSTRAINT [PK_ESCALAGLASGOW] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ESCALAGLASGOW_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_ESCALAGLASGOW_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_ESCALAGLASGOW_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[ESCALAGLASGOW] NOCHECK CONSTRAINT [FK_ESCALAGLASGOW_ADINGRESO];


GO
ALTER TABLE [dbo].[ESCALAGLASGOW] NOCHECK CONSTRAINT [FK_ESCALAGLASGOW_INESPECIA];


GO
ALTER TABLE [dbo].[ESCALAGLASGOW] NOCHECK CONSTRAINT [FK_ESCALAGLASGOW_INPACIENT];


GO
CREATE NONCLUSTERED INDEX [IX_ESCALAGLASGOW_IDHCESCALAS_APERTURAOCULAR_RESPUESTAMOTORA_RESPUESTAVERVAL]
    ON [dbo].[ESCALAGLASGOW]([IDHCESCALAS] ASC)
    INCLUDE([APERTURAOCULAR], [RESPUESTAMOTORA], [RESPUESTAVERVAL]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mayor de 5 años/1 a 5 años/ Menor de 1 año  APERTURAOCULAR + RESPUESTAVERVAL + RESPUESTAMOTORA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ESCALAGLASGOW', @level2type = N'COLUMN', @level2name = N'RESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Respuesta motora:   Mayor de 5 años/1 a 5 años  1=Ausencia de respuesta motora2=Respuesta con extensión anormal de los miembros3=Respuesta con flexión anormal de los miembros4=Se retira al dolor5=Localiza estímulos dolorosos6=Obedece órdenes                  Menor de 1 año  1=Ausencia de respuesta motora2=Extensión al dolor3=Flexión al dolor4=Se retira al dolor5=Se retira al contacto6=Movimientos espontáneos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ESCALAGLASGOW', @level2type = N'COLUMN', @level2name = N'RESPUESTAMOTORA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Respuesta verbal:  Mayor de 5 años  1=Carencia de actividad verbal2=Lenguaje incomprensible3=Lenguaje inapropiado4=Paciente confuso5=Orientado correctamente          1 a 5 años  1=Sin respuesta3=Llora o grita4=Palabras inadecuadas5=Palabras adecuadas          Menor de 1 año  1=Sin respuesta2=Se queja ante el dolor3=Llora ante el dolor4=Llanto consolable5=Sonríe, balbucea', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ESCALAGLASGOW', @level2type = N'COLUMN', @level2name = N'RESPUESTAVERVAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Apertura Ocular:   Mayor de 5 años/1 a 5 años/ Menor de 1 año  1=Ausencia de apertura ocular2=Ante un estímulo doloroso3=A la orden4=Espontánea ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ESCALAGLASGOW', @level2type = N'COLUMN', @level2name = N'APERTURAOCULAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'NoAplica = 0     EscalaCAGE = 1     EscalaAPGAR_Familiar = 2     EscalaEDPS = 3     EscalaBiopsicosocial = 4     EscalaTamizajeViolenciaDomestica = 5     EscalaRiesgoFramingham = 6     EscalaMorisky = 7     TestFINDRISC = 8     TestMiniMental = 9     TestDependenciaNicotina = 10     EscalaTannerDesarrolloMamarioMujer = 11     EscalaTannerDesarrolloVelloPubianoMujer = 12     EscalaTannerDesarrolloGenitalHombre = 13     EscalaTannerDesarrolloVelloPubianoHombre = 14     EscalaWagner = 15     EscalaModificadaDisnea = 16     EscalaCAT_COPD_AssessmentTest = 17     Exacerbaciones = 18     ClasificacionEPOC = 19 ''Guarda Detalle     TestGoodenough = 20     GOLD_EPOC = 21     EscalaAbreviadaDesarrollo = 22 ''Guarda Detalle     EscalaTISS_28 = 23     Escala_Branden = 24     Escala_ApacheII = 25     Escala_Karnosfky = 26     Escala_Ecog = 27     Escala_Nems = 28     Escala_Glasgow_Mayor5Anos = 29 ''Se guarda en otra tabla     Escala_Glasgow_de1a5Anos = 30 ''Se guarda en otra tabla     Escala_Glasgow_Menor1Ano = 31 ''Se guarda en otra tabla      Escala_SOFA = 32     Escala_Charlson = 33     Escala_SAPS3 = 34     Escala_Barthel = 35     Escala_Morse = 36     Escala_Macdems = 37     Escala_NSRAS = 38     Escala_MSTS = 39     Escala_Person = 40     Escala_beck = 41     Escala_Zarit = 42     Escala_RQC = 43     Escala_BacterianaSilness = 44     Escala_VALE = 45 ''Se guarda en otra tabla     EscalaRASS = 46     EscalaDown = 47     EscalaNorton = 48     EscalaVass = 49     EscalaNutricion = 50     EscalaSQR = 51 ''Guarda detalle     EscalaM_CHAT = 52     EscalaWHOOLEY = 53     EscalaAUDIT = 54     EscalaLindaFried = 55     EscalaLawton_Brody = 56     EscalaGAD = 57     EscalaMNA = 58     EscalaMNASimplificada = 59     EscalaAssist = 60     EscalaNews = 61     EscalaCHA2DS2_VASc = 62     EscalaCRUSADE = 63     EscalaHAS-BLED = 64     EscalaHEMORR2HAGES = 65     EscalaEUROSCOREII = 66     EscalaNYHA  = 67     EscalaKILLIP = 68     EscalaPADUA = 69     EscalaCAPRINI = 70', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ESCALAGLASGOW', @level2type = N'COLUMN', @level2name = N'TIPOESCALA';

