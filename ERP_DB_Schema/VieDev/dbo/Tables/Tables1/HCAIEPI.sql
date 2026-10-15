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
ALTER TABLE [dbo].[HCAIEPI] NOCHECK CONSTRAINT [FK_HCAIEPI_ADCENATEN];


GO
ALTER TABLE [dbo].[HCAIEPI] NOCHECK CONSTRAINT [FK_HCAIEPI_ADINGRESO];


GO
ALTER TABLE [dbo].[HCAIEPI] NOCHECK CONSTRAINT [FK_HCAIEPI_INPACIENT];


GO
ALTER TABLE [dbo].[HCAIEPI] NOCHECK CONSTRAINT [FK_HCAIEPI_INPROFSAL];


GO
ALTER TABLE [dbo].[HCAIEPI] NOCHECK CONSTRAINT [FK_HCAIEPI_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'c. Plato familiar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'PLATOFAMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'b. Olla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'COMEOLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'El niño come en que plato?   a. En su Propio plato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'COMEPLATO';


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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'EVALUAR ALIMENTACION:  Recibe leche materna?   SI 1  NO 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'RECLECHMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'No. de ausencias de las condiciones del grupo anterior:   1   2   3   4 ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'NUMAUSCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Presenta ausencia en las condiciones del grupo anterior:?   SI 1  NO 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'PREAUSECON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'No. de ausencias de las condiciones para la edad:   1   2   3   4 ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'NUMAUSEDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'No. de condiciones para la edad:   Ninguna  = 0  1   2   3   4 ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'NUMCONEDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'perimetro Cefalico DE  1. > +2  2. = +2 a > +1  3. = +1 a 0  4. < 0 a = -1  5. < -1 a = -2  6. < -2   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'PERIMECEFA1';


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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuál es el comportamiento? (lista de selección única)    1. Desespero  2. Impaciencia  3. Intolerancia  4. Agresividad en la consulta   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'CUALCOMPOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Con que frecuencia se ve obligado a pegarle a su hijo?   1. Frecuentemente  2. Algunas veces  3. Nunca  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'FRECPEGARH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cual maltrato?  1. Físico  2. Sexual  3. Negligencia  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'CUALMALTRAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cual Maltrato?          1. Físico  2. Sexual  3. Negligencia  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'CUALMALTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Menor con diagnóstico de: (Selección única)     1. VIH  2. Gonorrea  3. Sífilis  4. Trichomona vaginalis   5. Chlamydia Trachomatis   6. Condilomatosis  7. Sin ETS  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'MENORDIAG';


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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Observaciones Anemia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'OBSANEMIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'La palidez conjuntival es?: (lista de selección única)  1. Intensa  2. Leve   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'PALIDEZCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'La palidez palmar es?: (lista de selección única)  1. Intensa  2. Leve  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'PALIPALMAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Palidez palmar?:  SI = 1  NO = 2  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'TIENEPALI';


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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Las manchas son: (lista de selección única)  1. Blancas  2. Cafés  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'TIPOMANCHA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'La herida se presenta en:  1. Mucosa bucal  2. Encía  3. Lengua    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'HERIDAEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de lesión dental: (lista de selección única)  1. Fractura  2. Movilidad  3. Desplazamiento  4. Extrusión  5. Intrusión  6. avulsión    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'TIPOLESION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'La inflamación de la encía es?: (lista de selección única)  1. Localizado  2. Generalizado  3. Deformación contorno de la encía  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'INFLAENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Respuesta social  1. Normal  2. Inadecuado  3. Sin respuesta  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'RESPSOCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Pliegue cutaneo:  1. Inmediato  2. Lento  3. Muy lento  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAIEPI', @level2type = N'COLUMN', @level2name = N'PLIECUTANE';

