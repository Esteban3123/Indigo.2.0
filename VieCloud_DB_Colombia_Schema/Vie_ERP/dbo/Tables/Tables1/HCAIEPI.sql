CREATE TABLE [dbo].[HCAIEPI] (
    [ID]           INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]    VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]    CHAR (10)                                                                        NOT NULL,
    [NUMEFOLIO]    CHAR (10)                                                                        NOT NULL,
    [CODCENATE]    CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]    CHAR (10)                                                                        NOT NULL,
    [IDETIPHIS]    CHAR (9)                                                                         NOT NULL,
    [CODPROSAL]    CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECHAREGIS]   DATETIME                                                                         NOT NULL,
    [PGNOPUEPE]    BIT                                                                              NULL,
    [VOMITODO]     BIT                                                                              NULL,
    [CONVULSIO]    BIT                                                                              NULL,
    [LETARINCO]    BIT                                                                              NULL,
    [OBSERPELIG]   VARCHAR (400)                                                                    NULL,
    [NUMDIASTO]    TINYINT                                                                          NULL,
    [RESPXMIN]     TINYINT                                                                          NULL,
    [SAO2AIPEI]    BIT                                                                              NULL,
    [RESPRAPID]    BIT                                                                              NULL,
    [TIRASUBOC]    BIT                                                                              NULL,
    [TIRASUPRA]    BIT                                                                              NULL,
    [PRIEPISSI]    BIT                                                                              NULL,
    [SIBIRECUR]    BIT                                                                              NULL,
    [SIBILANCIA]   BIT                                                                              NULL,
    [ESTRIDOR]     BIT                                                                              NULL,
    [APNEA]        BIT                                                                              NULL,
    [SOMNOLIEN]    BIT                                                                              NULL,
    [CONFUSO]      BIT                                                                              NULL,
    [AGITADO]      BIT                                                                              NULL,
    [INCAHABLA]    BIT                                                                              NULL,
    [CUAGRIPRE]    BIT                                                                              NULL,
    [ANTPREMAT]    BIT                                                                              NULL,
    [OBSTOSREF]    VARCHAR (400)                                                                    NULL,
    [DIASDIARR]    TINYINT                                                                          NULL,
    [NUMDEP4H]     TINYINT                                                                          NULL,
    [NUMDEP24H]    TINYINT                                                                          NULL,
    [TIENEVOMI]    BIT                                                                              NULL,
    [NUMVULTIC]    TINYINT                                                                          NULL,
    [LETARCONT]    BIT                                                                              NULL,
    [INTRANQUI]    BIT                                                                              NULL,
    [OJOSHUNDI]    BIT                                                                              NULL,
    [BEBEMAL]      BIT                                                                              NULL,
    [BEBEAVIDA]    BIT                                                                              NULL,
    [PLIECUTANE]   CHAR (1)                                                                         NULL,
    [SANGHECE]     BIT                                                                              NULL,
    [OBSDIARREA]   VARCHAR (400)                                                                    NULL,
    [NUMDIFIEB]    TINYINT                                                                          NULL,
    [FIEBRETOD]    BIT                                                                              NULL,
    [FIEBMA38]     BIT                                                                              NULL,
    [FIEBMAGR]     BIT                                                                              NULL,
    [ZONDENGUE]    BIT                                                                              NULL,
    [ZONMALARI]    BIT                                                                              NULL,
    [CEFALEA]      BIT                                                                              NULL,
    [MIALGIAS]     BIT                                                                              NULL,
    [ARTRALGI]     BIT                                                                              NULL,
    [DOLOROCU]     BIT                                                                              NULL,
    [POSTRACION]   BIT                                                                              NULL,
    [PRUETORNI]    BIT                                                                              NULL,
    [LIPOTIMIA]    BIT                                                                              NULL,
    [HEPATOME]     BIT                                                                              NULL,
    [DISMDIURE]    BIT                                                                              NULL,
    [PULSORA]      BIT                                                                              NULL,
    [LLENACAPI]    BIT                                                                              NULL,
    [ASCITIS]      BIT                                                                              NULL,
    [RIGIDNUCA]    BIT                                                                              NULL,
    [APENFGRAVE]   BIT                                                                              NULL,
    [MANISSANG]    BIT                                                                              NULL,
    [ASPECTOXI]    BIT                                                                              NULL,
    [ERUPCUTAN]    BIT                                                                              NULL,
    [DOLORABD]     BIT                                                                              NULL,
    [AIEPIPIEL]    BIT                                                                              NULL,
    [RESPSOCIA]    CHAR (1)                                                                         NULL,
    [CHLEUCO]      BIT                                                                              NULL,
    [CHNEUTRO]     BIT                                                                              NULL,
    [CHPLAQUE]     BIT                                                                              NULL,
    [PARORINA]     BIT                                                                              NULL,
    [GOTAGRUE]     BIT                                                                              NULL,
    [OBSFIEBRE]    VARCHAR (400)                                                                    NULL,
    [TIENDOLOID]   BIT                                                                              NULL,
    [TIENSUPURA]   BIT                                                                              NULL,
    [NUMDIASUP]    TINYINT                                                                          NULL,
    [EPISOPREVI]   TINYINT                                                                          NULL,
    [NUMEPPREV]    TINYINT                                                                          NULL,
    [TUMDOLTACT]   BIT                                                                              NULL,
    [TIMPAROJO]    BIT                                                                              NULL,
    [OBSOIDO]      VARCHAR (400)                                                                    NULL,
    [TIENDOLGA]    BIT                                                                              NULL,
    [GANGCUEC]     BIT                                                                              NULL,
    [ERITEMA]      BIT                                                                              NULL,
    [EXUDADBLA]    BIT                                                                              NULL,
    [OBSGARGAN]    VARCHAR (400)                                                                    NULL,
    [DOLOBOCA]     BIT                                                                              NULL,
    [DOLALDIEN]    BIT                                                                              NULL,
    [TRAUCARBO]    BIT                                                                              NULL,
    [PADHERCAIRE]  BIT                                                                              NULL,
    [LIMBOCMAN]    BIT                                                                              NULL,
    [LIMBOCTAR]    BIT                                                                              NULL,
    [LIMBOCNOC]    BIT                                                                              NULL,
    [SUPLIMACO]    BIT                                                                              NULL,
    [SUPLIMSOL]    BIT                                                                              NULL,
    [UTICEPILLO]   BIT                                                                              NULL,
    [UTICREMAD]    BIT                                                                              NULL,
    [UTISEDADEN]   BIT                                                                              NULL,
    [USBIBERON]    BIT                                                                              NULL,
    [FECONSUOD]    DATETIME                                                                         NULL,
    [INFLADOLBL]   BIT                                                                              NULL,
    [ENRINFLOCA]   BIT                                                                              NULL,
    [INFLAENCIA]   CHAR (1)                                                                         NULL,
    [EXUPURENCI]   BIT                                                                              NULL,
    [VESICULAS]    BIT                                                                              NULL,
    [LOCENCIA]     BIT                                                                              NULL,
    [LOCLENGUA]    BIT                                                                              NULL,
    [LOCPALADAR]   BIT                                                                              NULL,
    [LESIODENTA]   BIT                                                                              NULL,
    [TIPOLESION]   CHAR (1)                                                                         NULL,
    [PREHERIDAS]   BIT                                                                              NULL,
    [HERIDAEN]     CHAR (1)                                                                         NULL,
    [MANCHAS]      BIT                                                                              NULL,
    [TIPOMANCHA]   CHAR (1)                                                                         NULL,
    [CARIECAVI]    BIT                                                                              NULL,
    [PLACBACTER]   BIT                                                                              NULL,
    [OBSERSALUD]   VARCHAR (400)                                                                    NULL,
    [EMACOMPLE]    BIT                                                                              NULL,
    [EDEAMPIES]    BIT                                                                              NULL,
    [APARNINO]     VARCHAR (500)                                                                    NULL,
    [PESOEDAD]     CHAR (1)                                                                         NULL,
    [TALLAEDAD]    CHAR (1)                                                                         NULL,
    [PESOTALLA]    CHAR (1)                                                                         NULL,
    [IMCEDADIN]    CHAR (1)                                                                         NULL,
    [TENPESON]     CHAR (1)                                                                         NULL,
    [OBSERVADES]   VARCHAR (400)                                                                    NULL,
    [RECIBHIER]    BIT                                                                              NULL,
    [TIENEPALI]    BIT                                                                              NULL,
    [PALIPALMAR]   CHAR (1)                                                                         NULL,
    [PALICONJU]    BIT                                                                              NULL,
    [PALIDEZCON]   CHAR (1)                                                                         NULL,
    [OBSANEMIA]    VARCHAR (400)                                                                    NULL,
    [LESIFISISU]   BIT                                                                              NULL,
    [LESCRANEO]    BIT                                                                              NULL,
    [QUEMADU]      BIT                                                                              NULL,
    [EQUIMOSIS]    BIT                                                                              NULL,
    [HEMATOMA]     BIT                                                                              NULL,
    [LACERACIO]    BIT                                                                              NULL,
    [MORDISCO]     BIT                                                                              NULL,
    [CICALEJOS]    BIT                                                                              NULL,
    [CICADIFERE]   BIT                                                                              NULL,
    [FRANTURA]     BIT                                                                              NULL,
    [TRAUVISCE]    BIT                                                                              NULL,
    [TRAUGRAVE]    BIT                                                                              NULL,
    [OTRA]         VARCHAR (400)                                                                    NULL,
    [PRELESTGEN]   BIT                                                                              NULL,
    [SANGVAGI]     BIT                                                                              NULL,
    [LACAGUDA]     BIT                                                                              NULL,
    [LACPERIAN]    BIT                                                                              NULL,
    [AUSEHIMEN]    BIT                                                                              NULL,
    [HIMECICAT]    BIT                                                                              NULL,
    [CICANAVI]     BIT                                                                              NULL,
    [ANODILATA]    BIT                                                                              NULL,
    [HALLSEMEN]    BIT                                                                              NULL,
    [FLUGENITAL]   BIT                                                                              NULL,
    [CUERPOEXTRA]  BIT                                                                              NULL,
    [VERRUGAS]     BIT                                                                              NULL,
    [EXPRESEXUA]   BIT                                                                              NULL,
    [MENORDIAG]    CHAR (1)                                                                         NULL,
    [COPROLESIO]   VARCHAR (500)                                                                    NULL,
    [NINOMALTRA]   BIT                                                                              NULL,
    [CUALMALTRA]   CHAR (1)                                                                         NULL,
    [QUIENMALTRA]  VARCHAR (500)                                                                    NULL,
    [TESTIGOMALT]  BIT                                                                              NULL,
    [CUALMALTRAT]  CHAR (1)                                                                         NULL,
    [QUIENMALTRAT] VARCHAR (500)                                                                    NULL,
    [INCTRAUMA]    BIT                                                                              NULL,
    [INCLESION]    BIT                                                                              NULL,
    [DIFVERSIONES] BIT                                                                              NULL,
    [TARDIACON]    BIT                                                                              NULL,
    [FRECPEGARH]   CHAR (1)                                                                         NULL,
    [HIJODESOBE]   BIT                                                                              NULL,
    [COMPORCUI]    BIT                                                                              NULL,
    [CUALCOMPOR]   CHAR (1)                                                                         NULL,
    [DESCUSALUD]   BIT                                                                              NULL,
    [CUALDESCUI]   VARCHAR (500)                                                                    NULL,
    [HIGIENE]      BIT                                                                              NULL,
    [PROTECCION]   BIT                                                                              NULL,
    [ALIMENTAC]    BIT                                                                              NULL,
    [NINOCALLE]    BIT                                                                              NULL,
    [NODESCUI]     BIT                                                                              NULL,
    [DISCAPACID]   BIT                                                                              NULL,
    [ACANORMAL]    BIT                                                                              NULL,
    [TEMEROSO]     BIT                                                                              NULL,
    [RETRAIDO]     BIT                                                                              NULL,
    [RECHADUL]     BIT                                                                              NULL,
    [DEPRIMIDO]    BIT                                                                              NULL,
    [EVICONTAC]    BIT                                                                              NULL,
    [TRASUENO]     BIT                                                                              NULL,
    [TRAALIMEN]    BIT                                                                              NULL,
    [PROBLEPSI]    BIT                                                                              NULL,
    [CONDUCRE]     BIT                                                                              NULL,
    [DESAESTAN]    BIT                                                                              NULL,
    [VIOLEINTRA]   BIT                                                                              NULL,
    [FAMICAOT]     BIT                                                                              NULL,
    [CUIDADICT]    BIT                                                                              NULL,
    [OBSERVAMAL]   VARCHAR (400)                                                                    NULL,
    [ANTEIMDESA]   BIT                                                                              NULL,
    [CUALANTEC]    VARCHAR (500)                                                                    NULL,
    [FACTORIESG]   BIT                                                                              NULL,
    [CUALRIESGO]   VARCHAR (500)                                                                    NULL,
    [ALTFENOTI]    BIT                                                                              NULL,
    [CUALALTERA]   VARCHAR (500)                                                                    NULL,
    [PERIMECEFA1]  CHAR (1)                                                                         NULL,
    [NUMCONEDA]    CHAR (1)                                                                         NULL,
    [AUSENCIAHI]   BIT                                                                              NULL,
    [NUMAUSEDA]    CHAR (1)                                                                         NULL,
    [PREAUSECON]   BIT                                                                              NULL,
    [NUMAUSCON]    CHAR (1)                                                                         NULL,
    [OBSERVAEVA]   VARCHAR (500)                                                                    NULL,
    [RECLECHMA]    BIT                                                                              NULL,
    [CUAVECEVEI]   TINYINT                                                                          NULL,
    [RECPECNOC]    BIT                                                                              NULL,
    [EXTRAELECH]   BIT                                                                              NULL,
    [COMOGUAR]     VARCHAR (500)                                                                    NULL,
    [RECIOTRAL]    BIT                                                                              NULL,
    [CUALALIMEN]   VARCHAR (500)                                                                    NULL,
    [CUANVECES]    VARCHAR (500)                                                                    NULL,
    [CONQUEDA]     VARCHAR (500)                                                                    NULL,
    [QUIEDACOM]    VARCHAR (500)                                                                    NULL,
    [CUANCOAYER]   TINYINT                                                                          NULL,
    [TAMANOPOR]    VARCHAR (500)                                                                    NULL,
    [CUANCOMESP]   TINYINT                                                                          NULL,
    [NORECIBIO]    BIT                                                                              NULL,
    [CARNE]        BIT                                                                              NULL,
    [PESCADO]      BIT                                                                              NULL,
    [MENUDENCIA]   BIT                                                                              NULL,
    [AVE]          BIT                                                                              NULL,
    [HUEVOS]       BIT                                                                              NULL,
    [CONAYERLAC]   BIT                                                                              NULL,
    [CONSLEGUM]    BIT                                                                              NULL,
    [COMVEGETA]    BIT                                                                              NULL,
    [AGREACEITE]   BIT                                                                              NULL,
    [QUIENINOAYE]  VARCHAR (500)                                                                    NULL,
    [COMEPLATO]    BIT                                                                              NULL,
    [COMEOLLA]     BIT                                                                              NULL,
    [PLATOFAMI]    BIT                                                                              NULL,
    [SUPLEVITAM]   BIT                                                                              NULL,
    [QUECOMIDO]    VARCHAR (500)                                                                    NULL,
    [FAMIOBESOS]   BIT                                                                              NULL,
    [HACEJERCICIO] BIT                                                                              NULL,
    [PROGRANUTRI]  BIT                                                                              NULL,
    [OBSERVALIM]   VARCHAR (250)                                                                    NULL,
    [GRUTOZRESP]   BIT                                                                              NULL,
    [GRUDIARREA]   BIT                                                                              NULL,
    [GRUFIEBRE]    BIT                                                                              NULL,
    [GRUOIDO]      BIT                                                                              NULL,
    [GRUGARGA]     BIT                                                                              NULL,
    [GRUBUCAL]     BIT                                                                              NULL,
    [GRUCRECI]     BIT                                                                              NULL,
    [GRUANEMIA]    BIT                                                                              NULL,
    [GRUMALTRA]    BIT                                                                              NULL,
    [GRUDESARRO]   BIT                                                                              NULL,
    [GRUALIMEN]    BIT                                                                              NULL,
    CONSTRAINT [PK_HCAIEPI] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCAIEPI_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCAIEPI_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCAIEPI_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCAIEPI_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCAIEPI_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCAIEPI].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCAIEPI].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Llena datos de alimentacion si=1 no=2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'GRUALIMEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Verifica desarrollo? si=1 no=2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'GRUDESARRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Presenta maltrato? si=1 no=2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'GRUMALTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'tiene problemas de anemia? si=1 no=2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'GRUANEMIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Verifica crecimiento? si=1 no=2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'GRUCRECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Verifica salud bucal? si=1 no=2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'GRUBUCAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiene dolor de garganta? si=1 no=2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'GRUGARGA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiene problemas de oído? si=1 no=2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'GRUOIDO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiene fiebre? si=1 no=2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'GRUFIEBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiene diarrea? si=1 no=2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'GRUDIARREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiene toz o dificultad para respirar? si=1 no=2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'GRUTOZRESP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'observaciones para la alimentacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'OBSERVALIM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Está asistiendo a un programa nutricional?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'PROGRANUTRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'El niño hace ejercicio?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'HACEJERCICIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Son los padres o hermanos obesos? ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'FAMIOBESOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Que ha comido durante la enfermedad? ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'QUECOMIDO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'El niño recibe alguna suplementación de vitaminas y minerales? ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'SUPLEVITAM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'c. Plato familiar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'PLATOFAMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'b. Olla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'COMEOLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'El niño come en que plato?   a. En su Propio plato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'COMEPLATO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Quien le dio la comida al niño ayer?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'QUIENINOAYE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Agrego una pequeña cantidad de aceite a la comida del niño ayer?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'AGREACEITE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Comió vegetales o frutas de color rojo o anaranjado y hojas de color verde oscuro ayer?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'COMVEGETA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consumió legumbres o semillas ayer? ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'CONSLEGUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consumió ayer productos lácteos?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'CONAYERLAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'f. Huevos ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'HUEVOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'e. Ave', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'AVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'd. Menudencias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'MENUDENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'c. Pescado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'PESCADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'b. Carne', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'CARNE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Comió alimentos de origen animal ayer?   a. No recibió', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'NORECIBIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuantas comidas de consistencia espesa recibió ayer? ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'CUANCOMESP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'De qué tamaño son las porciones que recibió ayer', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'TAMANOPOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuantas comidas y meriendas recibió ayer? ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'CUANCOAYER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'quien le da de comer', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'QUIEDACOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'que usa para dar de comer', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'CONQUEDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuantas Veces', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'CUANVECES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'cuales alimentos recibe', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'CUALALIMEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'recibe el menor de 6 meses otra leche o alimentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'RECIOTRAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Como la guarda y la administra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'COMOGUAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Se extrae la leche?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'EXTRAELECH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'recibe pecho en la noche', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'RECPECNOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuantas veces en 24 horas ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'CUAVECEVEI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'EVALUAR ALIMENTACION:  Recibe leche materna?   SI 1  NO 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'RECLECHMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones desarrollo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'OBSERVAEVA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'No. de ausencias de las condiciones del grupo anterior:   1   2   3   4 ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'NUMAUSCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Presenta ausencia en las condiciones del grupo anterior:?   SI 1  NO 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'PREAUSECON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'No. de ausencias de las condiciones para la edad:   1   2   3   4 ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'NUMAUSEDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Presenta ausencia en las condiciones para la edad?  1=Si  2=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'AUSENCIAHI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'No. de condiciones para la edad:   Ninguna  = 0  1   2   3   4 ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'NUMCONEDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'perimetro Cefalico DE  1. > +2  2. = +2 a > +1  3. = +1 a 0  4. < 0 a = -1  5. < -1 a = -2  6. < -2   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'PERIMECEFA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'cuales alteraciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'CUALALTERA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'alteracion fenotipica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'ALTFENOTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'cual riesgo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'CUALRIESGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'tiene algun factor de riesgo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'FACTORIESG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cual antecedente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'CUALANTEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'tiene algun antecedente importante para el desarrollo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'ANTEIMDESA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'observaciones de maltrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'OBSERVAMAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'm. Cuidadores adictos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'CUIDADICT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'l. Familia caótica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'FAMICAOT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'k. Violencia intrafamiliar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'VIOLEINTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'j. Desarrollo estancado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'DESAESTAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'i. Conductas regresivas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'CONDUCRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'h. Problemas Psicosomáticos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'PROBLEPSI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'g. Trastorno alimentario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'TRAALIMEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'f. Trastorno sueño', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'TRASUENO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'e. Evita contacto visual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'EVICONTAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'd. Deprimido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'DEPRIMIDO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'c. Rechazo adulto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'RECHADUL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'b. Retraído', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'RETRAIDO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cual actitud?     a. Temeroso  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'TEMEROSO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'El menor presenta una actitud anormal? ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'ACANORMAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'El menor presenta una discapacidad o es hiperactivo?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'DISCAPACID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'e. No esta descuidado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'NODESCUI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'd. Niño de la calle', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'NINOCALLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'c. Alimentación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'ALIMENTAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'b. Protección', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'PROTECCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta descuidado en algún aspecto relacionado con:  a. Higiene  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'HIGIENE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cual es el descuido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'CUALDESCUI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta descuidado el niño en su salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'DESCUSALUD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuál es el comportamiento? (lista de selección única)    1. Desespero  2. Impaciencia  3. Intolerancia  4. Agresividad en la consulta   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'CUALCOMPOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Comportamiento anormal de los padres o cuidador? ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'COMPORCUI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Es tan desobediente su hijo que se ve obligado a pegarle fuertemente? ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'HIJODESOBE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Con que frecuencia se ve obligado a pegarle a su hijo?   1. Frecuentemente  2. Algunas veces  3. Nunca  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'FRECPEGARH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Es tardía la consulta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'TARDIACON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hay diferentes versiones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'DIFVERSIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Existe incongruencia entre lesión – edad – desarrollo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'INCLESION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hay incongruencia para explicar un trauma significante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'INCTRAUMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Quien maltrata', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'QUIENMALTRAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cual maltrato?  1. Físico  2. Sexual  3. Negligencia  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'CUALMALTRAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Testigo relata maltrato? SI =1  NO =2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'TESTIGOMALT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Quien maltrata', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'QUIENMALTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cual Maltrato?          1. Físico  2. Sexual  3. Negligencia  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'CUALMALTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'El niño relata maltrato? SI = 1   NO = 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'NINOMALTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'como se produjeron las lesiones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'COPROLESIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Menor con diagnóstico de: (Selección única)     1. VIH  2. Gonorrea  3. Sífilis  4. Trichomona vaginalis   5. Chlamydia Trachomatis   6. Condilomatosis  7. Sin ETS  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'MENORDIAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Expresiones de actividad sexual inadecuadas (Juego con contenido sexual – boca en genitales)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'EXPRESEXUA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'k. Vesículas o verrugas genitales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'VERRUGAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'j. Cuerpo extraño en vagina o ano', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'CUERPOEXTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'i. Flujo genital', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'FLUGENITAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'h. Hallazgo semen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'HALLSEMEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'g. Ano dilatado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'ANODILATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'f. Cicatriz navicular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'CICANAVI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'e. Himen cicatrizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'HIMECICAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'd. Ausencia de himen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'AUSEHIMEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'c. Laceración perianal desde esfínter', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'LACPERIAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'b. Laceración aguda o equimosis himen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'LACAGUDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'a. Sangrado vaginal o anal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'SANGVAGI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Lesión o trauma genital? SI =1   NO = 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'PRELESTGEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'l. Otra ___________________ (cuadro de texto de 200 caracteres en el formulario)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'OTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'j. Trauma visceral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'TRAUGRAVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'j. Trauma visceral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'TRAUVISCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'i. Fracturas (costillas – huesos largos – espirales – oblicuas – metafisiarias – esternón – escapula - <5 años)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'FRANTURA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'h. Cicatrices de diferente evolución en niños que no deambulan', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'CICADIFERE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'g. Cicatrices lejos de las prominencia ósea con patrón del objeto agresor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'CICALEJOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'f. Mordiscos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'MORDISCO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'e. Laceraciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'LACERACIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'd. Hematomas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'HEMATOMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'c. Equimosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'EQUIMOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'b. Quemaduras (áreas cubiertas por ropa, patrón simétrico, limite bien demarcado, denota el objeto con que fue quemado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'QUEMADU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'a. Lesiones en cráneo (fracturas – hematomas – hemorragias retinianas)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'LESCRANEO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'lesion fisica sugestiva de maltrato  si=1 no=2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'LESIFISISU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Observaciones Anemia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'OBSANEMIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'La palidez conjuntival es?: (lista de selección única)  1. Intensa  2. Leve   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'PALIDEZCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Palidez conjuntival ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'PALICONJU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'La palidez palmar es?: (lista de selección única)  1. Intensa  2. Leve  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'PALIPALMAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Palidez palmar?:  SI = 1  NO = 2  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'TIENEPALI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ha recibido hierro en los ultimo 6 meses', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'RECIBHIER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones desnutricion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'OBSERVADES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tendencia peso:  1. Ascendente  2. Horizontal  3. Descendente  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'TENPESON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'IMC/edad (DE):  1. > +2  2. = +2 a > +1  3. = +1 a 0  4. < 0 a = -1  5. < -1 a = -2  6. < -2   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'IMCEDADIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Peso/talla (DE):  1. > +3  2. = +3 a > +2  3. = +2 a > +1  4. = +1 a 0  5. < 0 a = -1  6. < -1 a = -2  7. < -2 a = -3  8. < -3  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'PESOTALLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Talla/edad (DE):  1. > +2  2. = +2 a > +1  3. = +1 a 0  4. < 0 a = -1  5. < -1 a = -2  6. < -2   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'TALLAEDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Peso/edad (DE):   1. > +2  2. = +2 a > +1  3. = +1 a 0  4. < 0 a = -1  5. < -1 a = -2  6 < -2   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'PESOEDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Apariencia del niño', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'APARNINO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'edemas en ambos pies', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'EDEAMPIES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'emacion visible', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'EMACOMPLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones problemas salud bucal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'OBSERSALUD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'placa bacteriana', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'PLACBACTER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'caries cavitacionales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'CARIECAVI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Las manchas son: (lista de selección única)  1. Blancas  2. Cafés  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'TIPOMANCHA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Presenta Manchas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'MANCHAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'La herida se presenta en:  1. Mucosa bucal  2. Encía  3. Lengua    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'HERIDAEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Presenta heridas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'PREHERIDAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de lesión dental: (lista de selección única)  1. Fractura  2. Movilidad  3. Desplazamiento  4. Extrusión  5. Intrusión  6. avulsión    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'TIPOLESION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Presenta lesiones dentales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'LESIODENTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'La localización de las lesiones es: PALADAR', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'LOCPALADAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'La localización de las lesiones es: LENGUA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'LOCLENGUA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'La localización de las lesiones es: ENCIA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'LOCENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Vesículas, ulceras o placas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'VESICULAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Exudado-pus: SI 1 NO 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'EXUPURENCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'La inflamación de la encía es?: (lista de selección única)  1. Localizado  2. Generalizado  3. Deformación contorno de la encía  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'INFLAENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'enrojecimiento inflacion de la encia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'ENRINFLOCA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'inflamacion dolorsa del labio sin involucrar surco', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'INFLADOLBL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la ultima consulta odontologica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'FECONSUOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Utiliza chupo o biberón?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'USBIBERON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Que utiliza para limpiar la boca: Seda dental', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'UTISEDADEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Que utiliza para limpiar la boca: Crema dental', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'UTICREMAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Que utiliza para limpiar la boca: Cepillo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'UTICEPILLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Como supervisa la limpieza: el niño solo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'SUPLIMSOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Como supervisa la limpieza: Le limpian los dientes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'SUPLIMACO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuando limpia la boca: NOCHE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'LIMBOCNOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuando limpia la boca: MEDIO DIA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'LIMBOCTAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuando limpia la boca: MAÑANA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'LIMBOCMAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiene padre o hermanos caries', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'PADHERCAIRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Trauma en la cara o en la boca', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'TRAUCARBO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiene dolor en dientes?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'DOLALDIEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiene dolor al comer o masticar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'DOLOBOCA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'observaciones garganta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'OBSGARGAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Exudado blanquecino-amarillento en amígdalas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'EXUDADBLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Amígdalas eritematosas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'ERITEMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ganglios del cuello crecidos y doloros ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'GANGCUEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'tiene dolor de garganta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'TIENDOLGA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones de oído', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'OBSOIDO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Timpano rojo y abombado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'TIMPAROJO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tumefaccion dolorosa al tacto detras de la oreja', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'TUMDOLTACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de episodios previos en meses', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'NUMEPPREV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de episodias previos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'EPISOPREVI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Desde hace cuanto tiene supuracion ( Numero de dias)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'NUMDIASUP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'tiene supuracion ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'TIENSUPURA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'tiene dolor de odio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'TIENDOLOID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones de fiebre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'OBSFIEBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Gota gruesa Positiva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'GOTAGRUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Parcial de orina compatible con infección', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'PARORINA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CH - Plaquetas >100.000', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'CHPLAQUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CH - Neutrófilos >10.000', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'CHNEUTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CH - Leucocitosis >15.000 o <4.000', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'CHLEUCO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Respuesta social  1. Normal  2. Inadecuado  3. Sin respuesta  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'RESPSOCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Piel pálida, moteada, Ceniza o azul', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'AIEPIPIEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dolor abdominal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'DOLORABD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Erupcion cutanea generalizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'ERUPCUTAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Aspecto Toxico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'ASPECTOXI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manifestaciones de sangrado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'MANISSANG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Apariencia de enfermo grave', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'APENFGRAVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Rigidez de nuca', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'RIGIDNUCA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ascitis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'ASCITIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Llenado capilar >2seg', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'LLENACAPI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Pulso rápido y fino', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'PULSORA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Disminucion Diuresis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'DISMDIURE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hepatomegalia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'HEPATOME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Lipotimia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'LIPOTIMIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prueba de torniquete (+)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'PRUETORNI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Postracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'POSTRACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dolor retro-ocular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'DOLOROCU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Altralgias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'ARTRALGI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mialgias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'MIALGIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cefalea', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'CEFALEA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Vive o visito en los últimos 15 días zona de malaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'ZONMALARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Vive o visito en los últimos 15 días zona de dengue (altura <2.200m)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'ZONDENGUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fiebre >39 ºC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'FIEBMAGR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fiebre >38 ºC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'FIEBMA38';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si es >5 días: Todos los días', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'FIEBRETOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de dias con fiebre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'NUMDIFIEB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones Diarrea', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'OBSDIARREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sangre en las heces', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'SANGHECE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Pliegue cutaneo:  1. Inmediato  2. Lento  3. Muy lento  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'PLIECUTANE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Bebe ávidamente con sed', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'BEBEAVIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Bebe mal o no puede beber', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'BEBEMAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ojos hundidos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'OJOSHUNDI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Intranquilo o irritable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'INTRANQUI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Letargico o contoso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'LETARCONT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de vomitos en las ultimas 4 horas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'NUMVULTIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiene vomito', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'TIENEVOMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'No. de deposiciones en las últimas 24 horas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'NUMDEP24H';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'No. de deposiciones en las últimas 4 horas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'NUMDEP4H';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hace cuantos dias tiene diarrea', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'DIASDIARR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones al respirar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'OBSTOSREF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antecedente Prematurez', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'ANTPREMAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuadro gripal previo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'CUAGRIPRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Incapacidad para hablar o beber', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'INCAHABLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'agitado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'AGITADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'confuso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'CONFUSO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Somnoliento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'SOMNOLIEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Apnea', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'APNEA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estridor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'ESTRIDOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sibilancias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'SIBILANCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sibilancia Recurrente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'SIBIRECUR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Primer episodia de sibilancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'PRIEPISSI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiraje supraclavicular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'TIRASUPRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiraje Suboscal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'TIRASUBOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Respiracion Rapida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'RESPRAPID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sao2<92% altuta>2500msmm', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'SAO2AIPEI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Respiraciones por minuto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'RESPXMIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de dias con tos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'NUMDIASTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones signos de peligro general', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'OBSERPELIG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Letargico o Inconsiente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'LETARINCO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Convulsiones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'CONVULSIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Vomita Todo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'VOMITODO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'No puede beber o tomar del pecho', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'PGNOPUEPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de registro ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'FECHAREGIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro clínico del episodio de atención integrada pediátrica (AIEPI) por ingreso de paciente. Contiene la evaluación de signos de peligro, síntomas respiratorios, diarrea, fiebre, problemas de oído, garganta, salud bucal, crecimiento y nutrición, anemia, maltrato infantil, desarrollo y alimentación, usada en consultas de niño sano y urgencias pediátricas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI';
