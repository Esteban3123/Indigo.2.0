CREATE TABLE [dbo].[HCANTGINE] (
    [IDETIPHIS]                              CHAR (9)                                                                              NOT NULL,
    [NUMEFOLIO]                              NCHAR (10)                                                                            NOT NULL,
    [IPCODPACI]                              VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')      NOT NULL,
    [NUMINGRES]                              CHAR (10)                                                                             NOT NULL,
    [CODCENATE]                              CHAR (10)                                                                             NOT NULL,
    [UFUCODIGO]                              CHAR (10)                                                                             NOT NULL,
    [CODPROSAL]                              CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')         NOT NULL,
    [FECHISPAC]                              DATETIME                                                                              NOT NULL,
    [MENARQUIA]                              INT MASKED WITH (FUNCTION = 'default()')                                              NULL,
    [CICLOSPAC]                              INT MASKED WITH (FUNCTION = 'default()')                                              NULL,
    [MENSTRDUR]                              INT MASKED WITH (FUNCTION = 'default()')                                              NULL,
    [CICLOREGU]                              BIT                                                                                   NULL,
    [EDADVIDSE]                              INT MASKED WITH (FUNCTION = 'default()')                                              NULL,
    [GESTACION]                              INT MASKED WITH (FUNCTION = 'default()')                                              NULL,
    [NUMPARTO]                               INT MASKED WITH (FUNCTION = 'default()')                                              NULL,
    [NUMCESARE]                              INT MASKED WITH (FUNCTION = 'default()')                                              NULL,
    [NUMABORTO]                              INT MASKED WITH (FUNCTION = 'default()')                                              NULL,
    [NUMHIJVIV]                              INT MASKED WITH (FUNCTION = 'default()')                                              NULL,
    [NUMMORTIN]                              VARCHAR (50) MASKED WITH (FUNCTION = 'default()')                                     NULL,
    [NUMETOPIC]                              INT MASKED WITH (FUNCTION = 'default()')                                              NULL,
    [NUMEMOLAS]                              INT MASKED WITH (FUNCTION = 'default()')                                              NULL,
    [NUMEOVITO]                              INT MASKED WITH (FUNCTION = 'default()')                                              NULL,
    [FECULTMEN]                              VARCHAR (50)                                                                          NULL,
    [FECULTPAR]                              VARCHAR (50)                                                                          NULL,
    [FECULTCIT]                              VARCHAR (50) MASKED WITH (FUNCTION = 'default()')                                     NULL,
    [CODPLANIF]                              CHAR (2)                                                                              NULL,
    [OTROSANTE]                              VARCHAR (2000) MASKED WITH (FUNCTION = 'default()')                                   NULL,
    [CONTPRENA]                              BIT                                                                                   NULL,
    [CANTPRENA]                              INT                                                                                   NULL,
    [NOMSEMGES]                              NUMERIC (3, 1) MASKED WITH (FUNCTION = 'default()')                                   NULL,
    [INICONPRE]                              NUMERIC (3, 1) MASKED WITH (FUNCTION = 'default()')                                   NULL,
    [RIESOBTET]                              VARCHAR (200) MASKED WITH (FUNCTION = 'default()')                                    NULL,
    [RESCUAHEM]                              VARCHAR (400) MASKED WITH (FUNCTION = 'partial(0, "ResultLabObstetric_Ofuscado", 0)') NULL,
    [RESPARORI]                              VARCHAR (400) MASKED WITH (FUNCTION = 'partial(0, "ResultLabObstetric_Ofuscado", 0)') NULL,
    [TESTSULLI]                              VARCHAR (400) MASKED WITH (FUNCTION = 'partial(0, "ResultLabObstetric_Ofuscado", 0)') NULL,
    [GLUCBASAL]                              VARCHAR (400) MASKED WITH (FUNCTION = 'partial(0, "ResultLabObstetric_Ofuscado", 0)') NULL,
    [RESULVDRL]                              CHAR (1) MASKED WITH (FUNCTION = 'partial(0, "ResultLabObstetric_Ofuscado", 0)')      NULL,
    [DILUCVDRL]                              INT MASKED WITH (FUNCTION = 'default()')                                              NULL,
    [IGGTOXOPL]                              CHAR (10) MASKED WITH (FUNCTION = 'partial(0, "ResultLabObstetric_Ofuscado", 0)')     NULL,
    [CANTTOXO]                               INT MASKED WITH (FUNCTION = 'default()')                                              NULL,
    [FECULTIGG]                              DATETIME MASKED WITH (FUNCTION = 'default()')                                         NULL,
    [IQMTOXOPL]                              CHAR (10) MASKED WITH (FUNCTION = 'partial(0, "ResultLabObstetric_Ofuscado", 0)')     NULL,
    [FECULTIQM]                              DATETIME                                                                              NULL,
    [RESULTHIV]                              CHAR (1) MASKED WITH (FUNCTION = 'partial(0, "ResultLabObstetric_Ofuscado", 0)')      NULL,
    [HEPATITIB]                              CHAR (1) MASKED WITH (FUNCTION = 'partial(0, "ResultLabObstetric_Ofuscado", 0)')      NULL,
    [CANTHEPAT]                              INT                                                                                   NULL,
    [OTROSOBST]                              VARCHAR (2000) MASKED WITH (FUNCTION = 'partial(0, "OtherObstetric_Ofuscado", 0)')    NULL,
    [INDAUDFOR]                              NUMERIC (18)                                                                          NOT NULL,
    [FECPROPAR]                              DATETIME                                                                              NULL,
    [IGMRUBEO]                               CHAR (1) MASKED WITH (FUNCTION = 'partial(0, "ResultLabObstetric_Ofuscado", 0)')      NULL,
    [IGGRUBEO]                               CHAR (1) MASKED WITH (FUNCTION = 'partial(0, "ResultLabObstetric_Ofuscado", 0)')      NULL,
    [PRURASIF]                               CHAR (1) MASKED WITH (FUNCTION = 'partial(0, "ResultLabObstetric_Ofuscado", 0)')      NULL,
    [UROCULTI]                               VARCHAR (400) MASKED WITH (FUNCTION = 'partial(0, "ResultLabObstetric_Ofuscado", 0)') NULL,
    [FECHAPTOG]                              DATETIME                                                                              NULL,
    [FECHAIGM]                               DATETIME                                                                              NULL,
    [FECHAIGG]                               DATETIME                                                                              NULL,
    [FECHASIFI]                              DATETIME                                                                              NULL,
    [FECHAURO]                               DATETIME                                                                              NULL,
    [FECHAORI]                               DATETIME                                                                              NULL,
    [FECHACUA]                               DATETIME                                                                              NULL,
    [FECHAGLI]                               DATETIME                                                                              NULL,
    [FECHAVDRL]                              DATETIME                                                                              NULL,
    [FECHAHIV]                               DATETIME                                                                              NULL,
    [FECHAHEPB]                              DATETIME                                                                              NULL,
    [HEMOGLO]                                VARCHAR (400) MASKED WITH (FUNCTION = 'partial(0, "ResultLabObstetric_Ofuscado", 0)') NULL,
    [PLAQUETA]                               VARCHAR (400) MASKED WITH (FUNCTION = 'partial(0, "ResultLabObstetric_Ofuscado", 0)') NULL,
    [FTAABS]                                 CHAR (1) MASKED WITH (FUNCTION = 'partial(0, "ResultLabObstetric_Ofuscado", 0)')      NULL,
    [FECHAHEMO]                              DATETIME                                                                              NULL,
    [FECHAPLA]                               DATETIME                                                                              NULL,
    [FECHAFTA]                               DATE                                                                                  NULL,
    [ANTOBSTE]                               BIT                                                                                   NULL,
    [DateLastHumanPapillomavirusTest]        VARCHAR (50)                                                                          NULL,
    [DateLastCytologyReport]                 INT                                                                                   NULL,
    [DateListHumanPapillomavirusTestReport]  INT                                                                                   NULL,
    [GestationalNumber]                      INT                                                                                   NULL,
    [IVEConsultancy]                         INT                                                                                   NULL,
    [MaternalBloodType]                      VARCHAR (2)                                                                           NULL,
    [MaternalHR]                             VARCHAR (1)                                                                           NULL,
    [PaternalBloodType]                      VARCHAR (2)                                                                           NULL,
    [PaternalHR]                             VARCHAR (1)                                                                           NULL,
    [ApplicationQuarter]                     INT                                                                                   NULL,
    [ApplicationQuarterDate]                 DATETIME                                                                              NULL,
    [RetrovaginalCulture]                    VARCHAR (50)                                                                          NULL,
    [RetrovaginalCultureDate]                DATETIME                                                                              NULL,
    [Chagas]                                 VARCHAR (50)                                                                          NULL,
    [ChagasDate]                             DATETIME                                                                              NULL,
    [PuerperiumAttention]                    TINYINT                                                                               NULL,
    [ContraceptiveAttention]                 TINYINT                                                                               NULL,
    [DischargeWithContraceptive]             TINYINT                                                                               NULL,
    [ContraceptiveMethodChosen]              CHAR (2)                                                                              NULL,
    [PuerperiumAttentionObservations]        VARCHAR (200)                                                                         NULL,
    [Planning]                               BIT                                                                                   NULL,
    [CervixCancerScreening]                  BIT                                                                                   NULL,
    [WhyNotCervixCancerScreening]            INT                                                                                   NULL,
    [CervixCancerScreeningType]              INT                                                                                   NULL,
    [CytologyResult]                         INT                                                                                   NULL,
    [LatestVisualInspectionTechniqueDate]    VARCHAR (50)                                                                          NULL,
    [PostVisualInspectionTechniqueTreatment] INT                                                                                   NULL,
    [LastColposcopy]                         VARCHAR (50)                                                                          NULL,
    [ColposcopyReport]                       INT                                                                                   NULL,
    [LastBiopsyCervixCancerDate]             VARCHAR (50)                                                                          NULL,
    [BiopsyCervixCancerReportDate]           DATETIME                                                                              NULL,
    [BiopsyCervixCancerResult]               INT                                                                                   NULL,
    [BreastCancerScreening]                  BIT                                                                                   NULL,
    [WhyNotBreastCancerScreening]            INT                                                                                   NULL,
    [BreastCancerScreeningType]              INT                                                                                   NULL,
    [LastMedicalManualBreastExamDate]        VARCHAR (50)                                                                          NULL,
    [MedicalManualBreastExamResult]          INT                                                                                   NULL,
    [LastMammogramDate]                      VARCHAR (50)                                                                          NULL,
    [MammogramResult]                        INT                                                                                   NULL,
    [LastBiopsyBreastCancerDate]             VARCHAR (50)                                                                          NULL,
    [BiopsyBreastCancerReportDate]           DATETIME                                                                              NULL,
    [BiopsyBreastCancerResult]               INT                                                                                   NULL,
    [LowBirthWeightNewborn]                  BIT                                                                                   NULL,
    [NewbornMacrosomicPreviousPregnancy]     BIT                                                                                   NULL,
    [PreviousPretermBirth]                   BIT                                                                                   NULL,
    [IntergenicPeriodLess24Months]           BIT                                                                                   NULL,
    [RHIncompatibilityInPreviousPregnancy]   BIT                                                                                   NULL,
    [PreviousGestationalPreeclampsia]        BIT                                                                                   NULL,
    [PreviousGestationalHemorrhage]          BIT                                                                                   NULL,
    [HistoryOfPostpartumDepression]          BIT                                                                                   NULL,
    CONSTRAINT [PK_HCANTGINE] PRIMARY KEY CLUSTERED ([IDETIPHIS] ASC, [NUMEFOLIO] ASC, [IPCODPACI] ASC, [NUMINGRES] ASC),
    CONSTRAINT [FK_HCANTGINE_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCANTGINE_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCANTGINE_HCTIPPLAN] FOREIGN KEY ([CODPLANIF]) REFERENCES [dbo].[HCTIPPLAN] ([CODPLANIF]),
    CONSTRAINT [FK_HCANTGINE_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCANTGINE_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCANTGINE_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINE].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINE].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINE].[MENARQUIA]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINE].[CICLOSPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINE].[MENSTRDUR]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINE].[EDADVIDSE]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINE].[GESTACION]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINE].[NUMPARTO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINE].[NUMCESARE]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINE].[NUMABORTO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINE].[NUMHIJVIV]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINE].[NUMMORTIN]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINE].[NUMETOPIC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINE].[NUMEMOLAS]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINE].[NUMEOVITO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINE].[FECULTCIT]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINE].[OTROSANTE]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINE].[NOMSEMGES]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINE].[INICONPRE]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINE].[RIESOBTET]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINE].[RESCUAHEM]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINE].[RESPARORI]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINE].[TESTSULLI]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINE].[GLUCBASAL]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINE].[RESULVDRL]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINE].[DILUCVDRL]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINE].[IGGTOXOPL]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINE].[CANTTOXO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINE].[FECULTIGG]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINE].[IQMTOXOPL]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINE].[RESULTHIV]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINE].[HEPATITIB]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINE].[OTROSOBST]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINE].[IGMRUBEO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINE].[IGGRUBEO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINE].[PRURASIF]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINE].[UROCULTI]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINE].[HEMOGLO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINE].[PLAQUETA]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINE].[FTAABS]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
CREATE NONCLUSTERED INDEX [IX_HCANTGINE]
    ON [dbo].[HCANTGINE]([IPCODPACI] ASC, [NUMINGRES] ASC, [NUMEFOLIO] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_HCANTGINE_NUMINGRES, NOMSEMGES]
    ON [dbo].[HCANTGINE]([NUMINGRES] ASC)
    INCLUDE([NOMSEMGES]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obstétricos - Campo para dato de antecedente de depresion postparto 1.Si 0.No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'HistoryOfPostpartumDepression';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obstétricos - Campo para dato de hemorragia de gestacion anterior 1.Si 0.No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'PreviousGestationalHemorrhage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obstétricos - Campo para dato de preeclampsia de gestacion anterior 1.Si 0.No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'PreviousGestationalPreeclampsia';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obstétricos - Campo para dato de incompatibilidad de RH en gestación previa 1.Si 0.No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'RHIncompatibilityInPreviousPregnancy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obstétricos - Campo para dato de periodo intergenésico menor de 24 meses 1.Si 0.No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'IntergenicPeriodLess24Months';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obstétricos - Campo para dato de parto pretérmino previo 1.Si 0.No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'PreviousPretermBirth';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obstétricos - Campo para dato de macrosómico gestación anterior 1.Si 0.No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'NewbornMacrosomicPreviousPregnancy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obstétricos - Campo para dato de recien nacido de bajo peso (< de 2500 gr) 1.Si 0.No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'LowBirthWeightNewborn';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ginecológicos - Resultado biopsia  0- No aplica  1- Benigna  2- Atípica (indeterminada)  3- Malignidad sospechosa/probable  4- Maligna  5- No satisfactoria  6- Riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'BiopsyBreastCancerResult';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ginecológicos - Reporte biopsia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'BiopsyBreastCancerReportDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ginecológicos - Última biopsia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'LastBiopsyBreastCancerDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ginecológicos - Resultado mamografía  0- No aplica  1- BIRADS 0: necesidad de nuevo estudio imagenológico o mamograma previo para evaluación  2- BIRADS 1: negativo  3- BIRADS 2: hallazgos benignos  4- BIRADS 3: probablemente benigno  5- BIRADS 4: anormalidad sospechosa  6- BIRADS 5: altamente sospechoso de malignidad  7- BIRADS 6: malignidad por biopsia conocida  8- Riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'MammogramResult';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ginecológicos - Última mamografía', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'LastMammogramDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ginecológicos - Resultado examen  1- Normal  2- Anormal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'MedicalManualBreastExamResult';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ginecológicos - Último examen médico manual de mama', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'LastMedicalManualBreastExamDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ginecológicos - Tipo de tamizaje cáncer de mama  1- Examen médico manual de mama  2- Ecografía mamaria  3- Mamografía', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'BreastCancerScreeningType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ginecológicos - Por qué no Tamizaje cáncer de mama  0- No aplica  1- No se realiza por una tradición  2- No se realiza por una condición de salud  3- No se realiza por negación de la usuaria  4- No se realiza por tener datos de contacto de la usuaria no actualizados  5- No se realiza por otras razones  6- Riesgo no evaluado  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'WhyNotBreastCancerScreening';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ginecológicos - Tamizaje cáncer de mama', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'BreastCancerScreening';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ginecológicos - Resultado biopsia  0- No aplica  1- Negativo para neoplasia  2- NIC de bajo grado (NIC I)  3- NIC de alto grado (NIC II - NIC III)  4- Neoplasia micro infiltrante: escamocelular o adenocarcinoma  5- Neoplasia infiltrante: escamocelular o adenocarcinoma  6- Riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'BiopsyCervixCancerResult';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ginecológicos - Reporte biopsia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'BiopsyCervixCancerReportDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ginecológicos - Última biopsia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'LastBiopsyCervixCancerDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ginecológicos - Reporte colposcopia  1- Normal  2- Anormal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'ColposcopyReport';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ginecológicos - Última colposcopia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'LastColposcopy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ginecológicos - Tratamiento post técnica de inspección visual  0- No aplica  1- Si, se realizó tratamiento ablativo  2- Si, se realizó tratamiento de escisión  3- Si, se realizó tratamiento homologable a ablativo o de escisión  4- No se realizó ablación, escisión, ni tratamiento homologable, se requiere de otro procedimiento  5- No se realizó ablación ni escisión por otras razonas  6- Registro no evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'PostVisualInspectionTechniqueTreatment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ginecológicos - Ultima técnica de inspección visual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'LatestVisualInspectionTechniqueDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ginecológicos - Resultado citología  1- ASC-US (células escamosas atípicas de significado indeterminado)  2- ASC-H (células escamosas atípicas de significado indeterminado sugestivo de LEI de alto grado)  3- Lesión intraepitelial escamosa (LEI) de bajo grado -HPV (NIC I) (LEI BG)  4- Lesión intraepitelial escamosa (LEI) de alto grado (NIC II-III CA INSITU) (LEI AG)  5- Lesión intraepitelial escamosa de alto grado sospechosa de infiltración  6- Carcinoma de células escamosas (Escamocelular) glandulares  7- Células endocervicales atípicas sin ningún otro significado  8- Células endometriales atípicas sin ningún otro significado  9- Células glandulares atípicas sin ningún otro significado  10- Células endocervicales atípicas sospechosas de neoplasia  11- Células endometriales atípicas sospechosas de neoplasia  12- Células glandulares atípicas sospechosas de neoplasia  13- Adenocarcinoma endocervical in situ  14- Adenocarcinoma endocervical  15- Adenocarcinoma endometrial  16- Otras neoplasias  17- Negativa para lesión intraepitelial o neoplasia  18- Inadecuada para lectura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'CytologyResult';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ginecológicos - Tipo de Tamizaje cáncer cuello uterino  1- Citología cérvico uterina  2- Prueba ADN - VPH  3- Técnica de inspección visual  4- Prueba ADN - VPH y citología cérvico uterina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'CervixCancerScreeningType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ginecológicos - Por qué no Tamizaje cáncer cuello uterino  0- No aplica  1- No se realiza por una tradición  2- No se realiza por una condición de salud  3- No se realiza por negación de la usuaria  4- No se realiza por tener datos de contacto de la usuaria no actualizados  5- No se realiza por otras razones  6- Riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'WhyNotCervixCancerScreening';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ginecológicos - Tamizaje cáncer cuello uterino', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'CervixCancerScreening';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ginecológicos - Planifica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'Planning';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones atencion puerperio (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'PuerperiumAttentionObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'metodo anticonceptivo elegido (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'ContraceptiveMethodChosen';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Egreso con anticonceptivo 1: Sí, 0: No (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'DischargeWithContraceptive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Atencion anticonceptiva 1: Sí, 0: No (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'ContraceptiveAttention';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Atencion puerperio 1: Sí, 0: No (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'PuerperiumAttention';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'fecha de chagas (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'ChagasDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'chagas (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'Chagas';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'fecha de cultivo retrovaginal (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'RetrovaginalCultureDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'cultivo retrovaginal (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'RetrovaginalCulture';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'fecha de trimestre de aplicacion (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'ApplicationQuarterDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'trimestre de solicitud (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'ApplicationQuarter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'RH paterno (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'PaternalHR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grupo sanguíneo paterno (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'PaternalBloodType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'RH materno (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'MaternalHR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grupo sanguíneo materno (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'MaternalBloodType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Asesoría de interrupción voluntaria del embarazo: 1. Si, 2. No (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'IVEConsultancy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Gestacional número (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'GestationalNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dato de reporte de ultima fecha de prueba de vuris del papiloma humano: 1. Positivo, 2. Negativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'DateListHumanPapillomavirusTestReport';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Reporte de ultima fecha de citologia: 1. normal, 2. anormal ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'DateLastCytologyReport';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dato de última fecha de prueba del virus del papiloma humano', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'DateLastHumanPapillomavirusTest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina si registra antecedentes Obstétricos o NO y apartir de esto realizar una serie de validacion de campos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'ANTOBSTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha FTA-ABS (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'FECHAFTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha plaquetas (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'FECHAPLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha Hemoglobina (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'FECHAHEMO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FTA-ABS  1: + Positivo  2: - Negativo  3: No Tiene (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'FTAABS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plaquetas (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'PLAQUETA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hemoglobina (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'HEMOGLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Fecha Hepatitis B (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'FECHAHEPB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha HIV (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'FECHAHIV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha VDRL (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'FECHAVDRL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha Glicemia Basal (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'FECHAGLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha Cuadro Hematico (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'FECHACUA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha  parcial de orina (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'FECHAORI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha Urocultivo (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'FECHAURO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha Prueba rapida sífili (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'FECHASIFI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha IGG Rubeola (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'FECHAIGG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha IGM Rubeola (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'FECHAIGM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha PTOG  (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'FECHAPTOG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Urocultivo  (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'UROCULTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prueba rapida sífilis  1: + Positivo  2: - Negativo  3: No Tiene  (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'PRURASIF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' IGG Rubeola  1: + Positivo  2: - Negativo  3: No Tiene (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'IGGRUBEO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' IGM Rubeola  1: + Positivo  2: - Negativo  3: No Tiene (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'IGMRUBEO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obstetricos - Fecha Probable de Parto (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'FECPROPAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ginecologicos -  Otros Obstetricos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'OTROSOBST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obstetricos - Cantidad o Numero Referenci Hepatitis B (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'CANTHEPAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obstetricos - Ant. Sup. Hepatitis B  1: + Positivo  2: - Negativo  3: No Tiene (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'HEPATITIB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obstetricos - HIV / VIH  True: Positivo  False: Negativo  (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'RESULTHIV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Ultimo Examen IqM (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'FECULTIQM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obstetricos - IqM Toxoplasma  (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'IQMTOXOPL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Ultimo Examen IgG (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'FECULTIGG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obstetricos - Cantidad o Numero referencia a IgG Toxoplasma (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'CANTTOXO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obstetricos - IgG Toxoplasma (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'IGGTOXOPL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obstetricos - VDRL Diluciones (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'DILUCVDRL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obstetricos - Glucemia Basal (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'RESULVDRL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obstetricos - Glusemia Basal (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'GLUCBASAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obstetricos - Test Sullivan ó Test PTOG (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'TESTSULLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obstetricos - Parcial de Orina (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'RESPARORI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obstetricos - Cuadro Hematico (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'RESCUAHEM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obstetricos - Riesgo obstetrico (OBSOLETO desde el 05/09/2025 por el PBI 27235)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'RIESOBTET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obstetricos - Inicio de control prenatal (semanas)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'INICONPRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obstetricos - Semanas de gestacion (OBSOLETO desde el 05/09/2025 por el PBI 27235--->Se llenará este campo solamente cuando se inserte valor del campo Edad gestacional del control materno perinatal en las HC, ya que este valor se utiliza para la visualización de la grafica de Atalah (PBI 30529 - 3. Lógica de activación la gráfica de Atalah según campo "Edad gestacional" del page de valoración materno perinatal -NO FUNCIONAL)<---)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'NOMSEMGES';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Obstetricos - Cantidad de Controles Prenatales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'CANTPRENA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Obstetricos - Control Prenatal True:Si False:No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'CONTPRENA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ginecologicos -  Otros Antecedentes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'OTROSANTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ginecologicos - Codigo del Tipo de Planificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'CODPLANIF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ginecologicos - Fecha Ultima Citologia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'FECULTCIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obstétricos - Fecha Ultimo Parto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'FECULTPAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ginecologicos - Fecha ultima menstruacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'FECULTMEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obstétricos - Numero de Embarazos Ovitos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'NUMEOVITO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obstétricos - Numero de Molas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'NUMEMOLAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obstétricos - Numero de Embarazos Etopicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'NUMETOPIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obstétricos - Numero de Mortinatos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'NUMMORTIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obstétricos - Numero de Nacidos Vivos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'NUMHIJVIV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obstétricos - Numero de Abortos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'NUMABORTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obstétricos - Numero de Cesareas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'NUMCESARE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obstétricos - Numero de Partos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'NUMPARTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obstétricos - Numero de Gestaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'GESTACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad en la que Inicia Vida Sexual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'EDADVIDSE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ciclo Regular:  True= Si  False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'CICLOREGU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ginecologicos - Duracion de la menstracion expresados en Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'MENSTRDUR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ginecologicos - Ciclos expresados en Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'CICLOSPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ginecologicos - Menarquia Epresado en Años', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'MENARQUIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Historia Clinica Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'FECHISPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Historia clínica ginecológica y obstétrica de la paciente. Registra antecedentes reproductivos, resultados de laboratorio prenatal, datos de embarazo, parto, control prenatal, tamizajes de cáncer de cuello uterino y mama, y antecedentes de riesgo obstétrico por ingreso hospitalario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINE';
