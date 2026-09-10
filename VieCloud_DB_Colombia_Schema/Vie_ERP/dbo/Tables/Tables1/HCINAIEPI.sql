CREATE TABLE [dbo].[HCINAIEPI] (
    [IDETIPHIS]   CHAR (9)                                                                         NOT NULL,
    [CODPROSAL]   CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [NUMEFOLIO]   CHAR (10)                                                                        NOT NULL,
    [IPCODPACI]   VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]   CHAR (10)                                                                        NOT NULL,
    [CODCENATE]   CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]   CHAR (10)                                                                        NOT NULL,
    [PGNOPUEPE]   BIT                                                                              NULL,
    [LETARINCO]   BIT                                                                              NULL,
    [VOMITODO]    BIT                                                                              NULL,
    [CONVULSIO]   BIT                                                                              NULL,
    [OBSERPELIG]  VARCHAR (250)                                                                    NULL,
    [ENFMGRAVE]   BIT MASKED WITH (FUNCTION = 'default()')                                         NULL,
    [TOSDIFRSP]   BIT                                                                              NULL,
    [NUMDIASTO]   INT                                                                              NULL,
    [RESPXMIN]    INT                                                                              NULL,
    [RESPRAPID]   BIT                                                                              NULL,
    [PRIEPISSI]   BIT                                                                              NULL,
    [SIBIRECUR]   BIT                                                                              NULL,
    [CUAGRIPRE]   BIT                                                                              NULL,
    [ANTPREMAT]   BIT                                                                              NULL,
    [OBSTOSREF]   VARCHAR (250)                                                                    NULL,
    [TIRASUBOC]   BIT                                                                              NULL,
    [ESTRIDOR]    BIT                                                                              NULL,
    [APNEA]       BIT                                                                              NULL,
    [SAO2AIPEI]   BIT                                                                              NULL,
    [SIBILANCIA]  BIT                                                                              NULL,
    [BRONQUIOLI]  BIT                                                                              NULL,
    [BRONNORMA]   BIT                                                                              NULL,
    [SIBILANCIE]  BIT                                                                              NULL,
    [SIBIRECURE]  BIT                                                                              NULL,
    [CROUP]       BIT                                                                              NULL,
    [NEUMOGRAV]   BIT                                                                              NULL,
    [NEUMONIA]    BIT                                                                              NULL,
    [TOSRESFRIA]  BIT                                                                              NULL,
    [DIARREA]     BIT                                                                              NULL,
    [DIASDIARR]   BIT                                                                              NULL,
    [SANGHECE]    BIT                                                                              NULL,
    [TIENEVOMI]   BIT                                                                              NULL,
    [NUMVULTIC]   TINYINT                                                                          NULL,
    [NUMVULTIV]   TINYINT                                                                          NULL,
    [LETARCONT]   CHAR (1)                                                                         NULL,
    [OJOSHUNDI]   BIT                                                                              NULL,
    [PLIECUTANE]  CHAR (1)                                                                         NULL,
    [DIASINDESH]  BIT                                                                              NULL,
    [FORMABEB]    CHAR (1)                                                                         NULL,
    [DESHGRAVE]   BIT                                                                              NULL,
    [AGRADESHI]   BIT                                                                              NULL,
    [ALRIESDESH]  BIT                                                                              NULL,
    [SINDESHID]   BIT                                                                              NULL,
    [DIARREPER]   BIT                                                                              NULL,
    [DIARREGRA]   BIT                                                                              NULL,
    [DESINTERIA]  BIT                                                                              NULL,
    [OBSDIARREA]  VARCHAR (250)                                                                    NULL,
    [TIENEFIEB]   BIT                                                                              NULL,
    [NUMDIFIEB]   TINYINT                                                                          NULL,
    [FIEBRETOD]   BIT                                                                              NULL,
    [FIEBMAGR]    BIT                                                                              NULL,
    [ZONDENGUE]   BIT                                                                              NULL,
    [ZONMALARI]   CHAR (10)                                                                        NULL,
    [RIGIDNUCA]   BIT                                                                              NULL,
    [VOMIPERSI]   BIT                                                                              NULL,
    [APENFGRAVE]  BIT MASKED WITH (FUNCTION = 'default()')                                         NULL,
    [RESPSOCIA]   CHAR (1)                                                                         NULL,
    [VISITOULT]   BIT                                                                              NULL,
    [AIEPIPIEL]   BIT                                                                              NULL,
    [ERUPCUTAN]   BIT                                                                              NULL,
    [POSTRACION]  BIT                                                                              NULL,
    [LIPOTIMIA]   BIT                                                                              NULL,
    [ASPECTOXI]   BIT                                                                              NULL,
    [MANISSANG]   BIT                                                                              NULL,
    [DISMDIURE]   BIT                                                                              NULL,
    [ENFFEBALT]   BIT MASKED WITH (FUNCTION = 'default()')                                         NULL,
    [ENFRIESINT]  BIT MASKED WITH (FUNCTION = 'default()')                                         NULL,
    [ENFFEBBAJ]   BIT MASKED WITH (FUNCTION = 'default()')                                         NULL,
    [RIESMALCO]   BIT                                                                              NULL,
    [MALARIA]     BIT                                                                              NULL,
    [DENGGRAVE]   BIT                                                                              NULL,
    [DENSIGALAR]  BIT                                                                              NULL,
    [PROBADENG]   BIT                                                                              NULL,
    [NOTIDENGU]   BIT                                                                              NULL,
    [OBSFIEBRE]   VARCHAR (250)                                                                    NULL,
    [TIEPROBOID]  BIT                                                                              NULL,
    [TIENDOLOID]  BIT                                                                              NULL,
    [TIENSUPURA]  BIT                                                                              NULL,
    [NUMDIASUP]   INT                                                                              NULL,
    [EPISOPREVI]  BIT                                                                              NULL,
    [NUMEPPREV]   TINYINT                                                                          NULL,
    [SUPUOIDO]    BIT                                                                              NULL,
    [TIMPAROJO]   BIT                                                                              NULL,
    [TUMDOLTACT]  BIT                                                                              NULL,
    [MASTOIDITIS] BIT                                                                              NULL,
    [OTITISMED]   BIT                                                                              NULL,
    [OTITMECRO]   BIT                                                                              NULL,
    [OTITIMEREC]  BIT                                                                              NULL,
    [NOTIEOTIM]   BIT                                                                              NULL,
    [PROBGARGA]   BIT                                                                              NULL,
    [TIENDOLGA]   BIT                                                                              NULL,
    [EDADGARGA]   TINYINT                                                                          NULL,
    [TIENEFIEBRE] BIT                                                                              NULL,
    [ERITEMA]     BIT                                                                              NULL,
    [GANGCUEC]    BIT                                                                              NULL,
    [EXUDADBLA]   BIT                                                                              NULL,
    [OBSGARGAN]   VARCHAR (250)                                                                    NULL,
    [FARINGOAL]   BIT                                                                              NULL,
    [ESTREPTOC]   BIT                                                                              NULL,
    [FARINVIRAL]  BIT                                                                              NULL,
    [NOTIEFARIN]  BIT                                                                              NULL,
    [SALUDBUCA]   BIT                                                                              NULL,
    [FECONSUOD]   DATETIME                                                                         NULL,
    [DOLALDIEN]   BIT                                                                              NULL,
    [DOLOBOCA]    BIT                                                                              NULL,
    [TRAUCARBO]   BIT                                                                              NULL,
    [PADHERCAIRE] BIT                                                                              NULL,
    [CEPDIENIÑO]  NVARCHAR (50)                                                                    NULL,
    [UTSEDADENT]  BIT                                                                              NULL,
    [VECXDIACEP]  INT                                                                              NULL,
    [INFLADOLBL]  BIT                                                                              NULL,
    [ENRINFLOCA]  BIT                                                                              NULL,
    [CARIECAVI]   BIT                                                                              NULL,
    [VESICULAS]   BIT                                                                              NULL,
    [MANCHAS]     BIT                                                                              NULL,
    [TRAUMAAIE]   CHAR (1)                                                                         NULL,
    [LUXACION]    BIT                                                                              NULL,
    [EXUPURENCI]  BIT                                                                              NULL,
    [EDEMERITE]   BIT                                                                              NULL,
    [PLACASAIEPI] CHAR (1)                                                                         NULL,
    [PLACBACTER]  BIT                                                                              NULL,
    [AVULDIENT]   CHAR (1)                                                                         NULL,
    [USBIBERON]   BIT                                                                              NULL,
    [OBSERSALUD]  VARCHAR (250)                                                                    NULL,
    [ENDENGRAVE]  BIT MASKED WITH (FUNCTION = 'default()')                                         NULL,
    [TRAUBUCODE]  BIT                                                                              NULL,
    [GENGIESTOM]  BIT                                                                              NULL,
    [ALRIESCARI]  BIT                                                                              NULL,
    [BAJRIESCARI] BIT                                                                              NULL,
    [DESNUTRIC]   BIT                                                                              NULL,
    [EMACOMPLE]   BIT                                                                              NULL,
    [APARNIÑO]    VARCHAR (250)                                                                    NULL,
    [EDEAMPIES]   BIT                                                                              NULL,
    [PESOEDAD]    INT                                                                              NULL,
    [PESOEDADR]   CHAR (1)                                                                         NULL,
    [TALLAEDAD]   INT                                                                              NULL,
    [TALLAEDADR]  CHAR (10)                                                                        NULL,
    [PESOTALLA]   INT                                                                              NULL,
    [PESOTALLAR]  CHAR (1)                                                                         NULL,
    [IMCEDADIN]   INT                                                                              NULL,
    [IMCEDADINR]  CHAR (1)                                                                         NULL,
    [TENPESON]    CHAR (1)                                                                         NULL,
    [OBSERVADES]  VARCHAR (250)                                                                    NULL,
    [DESNUGRAV]   BIT                                                                              NULL,
    [PROBLECREC]  BIT                                                                              NULL,
    [OBESO]       BIT                                                                              NULL,
    [RIESPROBC]   BIT                                                                              NULL,
    [SOBREPESO]   BIT                                                                              NULL,
    [ADECUCREC]   BIT                                                                              NULL,
    [TIEANEMIA]   BIT                                                                              NULL,
    [RECIBHIER]   BIT                                                                              NULL,
    [CRECIBHIER]  DATETIME                                                                         NULL,
    [CURECIBHIE]  INT                                                                              NULL,
    [PALICONJU]   BIT                                                                              NULL,
    [PALIPALMAR]  CHAR (1)                                                                         NULL,
    [ANEMSEVE]    BIT                                                                              NULL,
    [ANEMIAAIPE]  BIT                                                                              NULL,
    [NOTIEANEM]   BIT                                                                              NULL,
    [TIEMALTRA]   BIT                                                                              NULL,
    [CUPROLESIO]  VARCHAR (250)                                                                    NULL,
    [COPROLESIO]  VARCHAR (250)                                                                    NULL,
    [FRECPEGARH]  VARCHAR (250)                                                                    NULL,
    [CORFUERHI]   VARCHAR (250)                                                                    NULL,
    [VIVECALLE]   BIT                                                                              NULL,
    [DISHISDESA]  BIT                                                                              NULL,
    [LESIFISISU]  VARCHAR (250)                                                                    NULL,
    [PRELESTGEN]  BIT                                                                              NULL,
    [VICTMALTRA]  CHAR (1)                                                                         NULL,
    [EXPRESEXUA]  BIT                                                                              NULL,
    [LESIOMENOR]  CHAR (1)                                                                         NULL,
    [DESCUHIGIE]  BIT                                                                              NULL,
    [ALTCOMCUI]   BIT                                                                              NULL,
    [OBSERVAMAL]  VARCHAR (250)                                                                    NULL,
    [MALTRAFIG]   BIT                                                                              NULL,
    [ABUSOSEXUA]  BIT                                                                              NULL,
    [MALFISICO]   BIT                                                                              NULL,
    [SOSABUSEXU]  BIT                                                                              NULL,
    [MALTEMOCIO]  BIT                                                                              NULL,
    [NOSOSMALT]   BIT                                                                              NULL,
    [EVDESARRO]   BIT                                                                              NULL,
    [ANTEIMDESA]  VARCHAR (250)                                                                    NULL,
    [FACTORIESG]  VARCHAR (250)                                                                    NULL,
    [AUSENCIAHI]  INT                                                                              NULL,
    [PERIMECEFA1] INT                                                                              NULL,
    [PERIMECEFA2] INT                                                                              NULL,
    [PERIMECEFA3] INT                                                                              NULL,
    [ALTFENOTI]   BIT                                                                              NULL,
    [OBSERVAEVA]  VARCHAR (250)                                                                    NULL,
    [SOSRETRADE]  BIT                                                                              NULL,
    [RIESPRODES]  BIT                                                                              NULL,
    [DESNORFARI]  BIT                                                                              NULL,
    [DESARNORM]   BIT                                                                              NULL,
    [ANTEVACUN]   BIT                                                                              NULL,
    [AIEPIBCG]    INT                                                                              NULL,
    [AIPEIVOP]    CHAR (2)                                                                         NULL,
    [ROTAVIRUS]   INT                                                                              NULL,
    [HEPATIBRN]   INT                                                                              NULL,
    [AIPEIDPT]    CHAR (2)                                                                         NULL,
    [HAEMTIPOB]   CHAR (2)                                                                         NULL,
    [STREPNEUM]   INT                                                                              NULL,
    [ULTIMADOSI]  DATETIME                                                                         NULL,
    [AIEPISRP]    INT                                                                              NULL,
    [FIEAMAEDA]   INT                                                                              NULL,
    [EDADPROVA]   INT                                                                              NULL,
    [VACUNEDAD]   VARCHAR (250)                                                                    NULL,
    [EVALIMENT]   BIT                                                                              NULL,
    [RECLECHMA]   BIT                                                                              NULL,
    [CUAVECEVEI]  INT                                                                              NULL,
    [RECPECNOC]   BIT                                                                              NULL,
    [RECIOTRAL]   BIT                                                                              NULL,
    [CUALALIMEN]  VARCHAR (250)                                                                    NULL,
    [CUANVECES]   INT                                                                              NULL,
    [USADARCOM]   VARCHAR (250)                                                                    NULL,
    [QUIEDACOM]   VARCHAR (250)                                                                    NULL,
    [RECIBLEVAN]  VARCHAR (250)                                                                    NULL,
    [MEDMAÑTA]    VARCHAR (250)                                                                    NULL,
    [ALMUERZO]    VARCHAR (250)                                                                    NULL,
    [NOCHECOM]    VARCHAR (250)                                                                    NULL,
    [RECIPROPO]   BIT                                                                              NULL,
    [CAMBALENF]   VARCHAR (250)                                                                    NULL,
    [OBSERVALIM]  VARCHAR (250)                                                                    NULL,
    CONSTRAINT [PK_HCINAIEPI] PRIMARY KEY CLUSTERED ([NUMEFOLIO] ASC, [IPCODPACI] ASC, [NUMINGRES] ASC),
    CONSTRAINT [FK_HCINAIEPI_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCINAIEPI_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCINAIEPI].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCINAIEPI].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCINAIEPI].[ENFMGRAVE]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCINAIEPI].[APENFGRAVE]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCINAIEPI].[ENFFEBALT]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCINAIEPI].[ENFRIESINT]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCINAIEPI].[ENFFEBBAJ]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCINAIEPI].[ENDENGRAVE]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'observaciones para la alimentacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'OBSERVALIM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'cambios en la alimentacion en esta enfermedad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'CAMBALENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'recibe su propia porcion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'RECIPROPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'en la noche', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'NOCHECOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'al almuerzo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'ALMUERZO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'que recibe en la mañana tarde', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'MEDMAÑTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'el niño mayor de 6 meses recibe al levantarse', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'RECIBLEVAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'quien le da de comer', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'QUIEDACOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'que usa para dar de comer', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'USADARCOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuantas Vces', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'CUANVECES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'cuales alimentos recibe', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'CUALALIMEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'recibe el menor de 6 meses otra leche o alimentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'RECIOTRAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'recibe pecho en la noche', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'RECPECNOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuantas veces en 24 horas ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'CUAVECEVEI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'recibe leche materna', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'RECLECHMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Evaluar Alimentacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'EVALIMENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cual es la edad de la proxima vacuna', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'VACUNEDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'edad de la proxima vacuna', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'EDADPROVA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fiebre amarilla edad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'FIEAMAEDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'SRP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'AIEPISRP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la ultima dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'ULTIMADOSI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Streptococo Neumoniae', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'STREPNEUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Haemophilus influenza tipo b', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'HAEMTIPOB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'DPT ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'AIPEIDPT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hepatitis BRN', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'HEPATIBRN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Rotavirus', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'ROTAVIRUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'VOP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'AIPEIVOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'BCG', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'AIEPIBCG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antecedentes de vacunacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'ANTEVACUN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Desarrollo Normal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'DESARNORM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Desarrolo0 normal con factor de riesgo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'DESNORFARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Riesgo del problema de desarrollo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'RIESPRODES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'sospecha del retraso del desarrollo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'SOSRETRADE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones desarrollo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'OBSERVAEVA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'alteracion fenotipica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'ALTFENOTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Parametro cefalico 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'PERIMECEFA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'perimetro cefalico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'PERIMECEFA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'perimetro defalico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'PERIMECEFA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ausencia de condiciones para la edad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'AUSENCIAHI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'tiene algun factor de riesgo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'FACTORIESG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'tiene algun antecedente importante para el desarrollo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'ANTEIMDESA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Evaluar desarollo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'EVDESARRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'no hay sospecha de maltrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'NOSOSMALT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'maltrato emocional y o negligencia abandono', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'MALTEMOCIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sospecha de abuso sexual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'SOSABUSEXU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'maltrato fisico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'MALFISICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'abuso sexual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'ABUSOSEXUA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'maltrato fisico grave', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'MALTRAFIG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'observaciones de maltrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'OBSERVAMAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Alteracion del comporamiento de los cuidadores', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'ALTCOMCUI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta descuidado en su higiene', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'DESCUHIGIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Lesiones menores', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'LESIOMENOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'expresiones de actividad sexual inapropiadas para su edad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'EXPRESEXUA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'expresa ser victima del maltrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'VICTMALTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'presenta lesiones en los genitales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'PRELESTGEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'lesion fisica sugestiva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'LESIFISISU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Discrepancia entre la historia y desarrollo (lesiones)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'DISHISDESA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'vive en situacion de calle', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'VIVECALLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'como corrige a su hijo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'CORFUERHI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'con que frecuencia se ve obligado a pegarle a su hijo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'FRECPEGARH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'como se produjeron las lesiones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'COPROLESIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuando se produjeros las lesiones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'CUPROLESIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiene maltrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'TIEMALTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'No tiene anemia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'NOTIEANEM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Anemia AIEPI', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'ANEMIAAIPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'anemia severa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'ANEMSEVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Palidez Palmar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'PALIPALMAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Palidez conjuntival ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'PALICONJU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'cuanto tiempo ha recibido hierro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'CURECIBHIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'cuando recibio hierro en los ultimos meses', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'CRECIBHIER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ha recibido hierro en los ultimo 6 meses', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'RECIBHIER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiene Anemia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'TIEANEMIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Crecimiento Adecuado Si=1;No=0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'ADECUCREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sobrepeso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'SOBREPESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Riesgo problema crecimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'RIESPROBC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Obeso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'OBESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'problema del crecimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'PROBLECREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Desnutricion Grave', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'DESNUGRAV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones desnutricion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'OBSERVADES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'tendencia del peso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'TENPESON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1- Normal 2- Alto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'IMCEDADINR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'IMC Edad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'IMCEDADIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1- Normal 2- Bajo 3- Alto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'PESOTALLAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Peso para la talla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'PESOTALLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1- Normal 2- Bajo 3- Alto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'TALLAEDADR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Talla para la edad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'TALLAEDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1- Normal 2- Bajo 3- Alto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'PESOEDADR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'peso para la edad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'PESOEDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'edemas en ambos pies', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'EDEAMPIES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Apariencia del niño', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'APARNIÑO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'emacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'EMACOMPLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiene desnutricion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'DESNUTRIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Bajo riesgo de caries', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'BAJRIESCARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Alto riesgo de caries', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'ALRIESCARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Gengivitis / Estomatitis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'GENGIESTOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Traumatismo Bucodental', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'TRAUBUCODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'enfermedad dental grave', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'ENDENGRAVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones problemas salud bucal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'OBSERSALUD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usa biberon', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'USBIBERON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Avulsion Diente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'AVULDIENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'placa bacteriana', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'PLACBACTER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Placas en encias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'PLACASAIEPI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edema o eritema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'EDEMERITE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'exudado purulento encia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'EXUPURENCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Luxacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'LUXACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Trauma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'TRAUMAAIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manchas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'MANCHAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Vesiculas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'VESICULAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'caries cavitacionales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'CARIECAVI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'enrojecimiento inflacion localizada o deformidad de encia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'ENRINFLOCA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'inflamacion dolorsa del labio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'INFLADOLBL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'cuantas veces por dia cepilla los diente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'VECXDIACEP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'utiliza seda dental', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'UTSEDADENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'quien cepilla los dientes del niño', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'CEPDIENIÑO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiene padre o hermanos caries', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'PADHERCAIRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Trauma en la cara o en la boca', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'TRAUCARBO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiene dolor en la boca', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'DOLOBOCA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiene dolor en algun diente ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'DOLALDIEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la consulta odontologica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'FECONSUOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiene problemas de salud bucal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'SALUDBUCA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'No tiene faringoadmidalitis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'NOTIEFARIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FARINGOAMIGDALITIS VIRAL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'FARINVIRAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ESTREPTOCOCICA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'ESTREPTOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Faringoadmidalitis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'FARINGOAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'observaciones garganta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'OBSGARGAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Exudado blanco', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'EXUDADBLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ganglios del cuello crecidos y doloros ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'GANGCUEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Eritema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'ERITEMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiene Fiebre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'TIENEFIEBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad garganta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'EDADGARGA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'tiene dolor de garganta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'TIENDOLGA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Problemas de garganta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'PROBGARGA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'no tiene otitis media', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'NOTIEOTIM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'otitis media recurrente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'OTITIMEREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'otitis media cronica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'OTITMECRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'otitis media aguda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'OTITISMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mastoiditis ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'MASTOIDITIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tumefaccion dolorosa al tacto detras de la oreja', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'TUMDOLTACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Timpano rojo y abombado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'TIMPAROJO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Supuracion de oido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'SUPUOIDO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de episodios previos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'NUMEPPREV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'episodias previos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'EPISOPREVI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de dias supuracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'NUMDIASUP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'tiene supuracion ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'TIENSUPURA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'tiene dolor de odio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'TIENDOLOID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tienen problema de oido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'TIEPROBOID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones fiebre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'OBSFIEBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'No tiene dengue', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'NOTIDENGU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Probable dengue', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'PROBADENG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'dengue signo de alarma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'DENSIGALAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dengue grave', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'DENGGRAVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Malaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'MALARIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Reisgo malaria complicada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'RIESMALCO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Enfermedad febril bajo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'ENFFEBBAJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'enfermedad febril riesgo intermedio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'ENFRIESINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'enfermedad febril de alto riesgo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'ENFFEBALT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Disminucion Diuresis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'DISMDIURE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manifestaciones de sangrado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'MANISSANG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Aspecto Toxico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'ASPECTOXI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Lipotimia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'LIPOTIMIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Postracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'POSTRACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Erupcion cutanea generalizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'ERUPCUTAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Piel', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'AIEPIPIEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Visito ultimos Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'VISITOULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Respuesta social', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'RESPSOCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'apariencia enfermedad grave', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'APENFGRAVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'vomito persistente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'VOMIPERSI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Rigidez nuca', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'RIGIDNUCA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Zona malaria 1- Urbana 2- Rural', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'ZONMALARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'visito zona dengue', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'ZONDENGUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fiebre mayor a 39%', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'FIEBMAGR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fiebre todo el dia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'FIEBRETOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de dias con fiebre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'NUMDIFIEB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiene fiebre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'TIENEFIEB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones Diarrera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'OBSDIARREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Desinteria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'DESINTERIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Diarrea persistente grave', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'DIARREGRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'diarrea persistente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'DIARREPER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'sin deshidratacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'SINDESHID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Alto riesgo de ajuste', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'ALRIESDESH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Algun grado de deshidratacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'AGRADESHI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Deshidratacion Grave', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'DESHGRAVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Forma de beber', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'FORMABEB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Diarrea sin Deshidratacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'DIASINDESH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Pliegue cutaneo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'PLIECUTANE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ojos hundidos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'OJOSHUNDI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Letargico o contoso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'LETARCONT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de vomitos en las ultimas 24 horas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'NUMVULTIV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de vomitos en las ultimas 4 horas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'NUMVULTIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiene vomito', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'TIENEVOMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sangre en las heces', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'SANGHECE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hace cuantos dias tiene diarrea', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'DIASDIARR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Diarrea', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'DIARREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tos o resfriado respuesta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'TOSRESFRIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Neumonia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'NEUMONIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Neumonia Grave respuesta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'NEUMOGRAV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CROUP Respuesta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'CROUP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sibililancia Recurrente Respuesta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'SIBIRECURE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sibilancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'SIBILANCIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Bronquilitis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'BRONNORMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Bronquiolitis Grave', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'BRONQUIOLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sibilancias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'SIBILANCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sao2<92% altuta>2500msmm', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'SAO2AIPEI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Apnea', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'APNEA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estridor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'ESTRIDOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiraje Suboscal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'TIRASUBOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones al respirar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'OBSTOSREF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antecedente Prematurez', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'ANTPREMAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuadro gripal previo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'CUAGRIPRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sibilancia Recurrente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'SIBIRECUR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Primer episodia de sibilancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'PRIEPISSI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Respiracion Rapida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'RESPRAPID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Respiraciones por minuto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'RESPXMIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de dias con tos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'NUMDIASTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tos o dificultas para respirar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'TOSDIFRSP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Enfermedad Muy Grave', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'ENFMGRAVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones signos de peligro general', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'OBSERPELIG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Convulsiones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'CONVULSIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Vomita Todo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'VOMITODO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Letargico o Inconsiente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'LETARINCO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'No puede beber o tomar del pecho', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'PGNOPUEPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINAIEPI', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla que almacena el formulario de Atención Integrada a las Enfermedades Prevalentes de la Infancia (AIEPI) asociado a un ingreso y paciente específicos. Registra hallazgos clínicos pediátricos por sistemas: respiratorio (sibilancias, estridor, neumonía), digestivo (diarrea, deshidratación), fiebre (dengue, malaria), oído, garganta, salud bucal, estado nutricional (peso/talla/IMC), anemia, maltrato infantil, desarrollo y esquema de vacunación. También captura la evaluación alimentaria con detalle de lactancia y comidas por tiempo del día.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'HCINAIEPI';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'HCINAIEPI';
GO
