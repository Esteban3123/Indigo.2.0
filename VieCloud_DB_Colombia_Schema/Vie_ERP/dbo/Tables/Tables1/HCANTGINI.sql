CREATE TABLE [dbo].[HCANTGINI] (
    [IDETIPHIS] CHAR (9)                                                                              NOT NULL,
    [NUMEFOLIO] NCHAR (10)                                                                            NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')      NOT NULL,
    [NUMINGRES] CHAR (10)                                                                             NOT NULL,
    [CODCENATE] CHAR (10)                                                                             NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                             NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')         NOT NULL,
    [FECHISPAC] DATETIME                                                                              NOT NULL,
    [MENARQUIA] INT MASKED WITH (FUNCTION = 'default()')                                              NULL,
    [CICLOSPAC] INT MASKED WITH (FUNCTION = 'default()')                                              NULL,
    [MENSTRDUR] INT MASKED WITH (FUNCTION = 'default()')                                              NULL,
    [CICLOREGU] BIT                                                                                   NULL,
    [EDADVIDSE] INT MASKED WITH (FUNCTION = 'default()')                                              NULL,
    [GESTACION] INT MASKED WITH (FUNCTION = 'default()')                                              NULL,
    [NUMPARTO]  INT MASKED WITH (FUNCTION = 'default()')                                              NULL,
    [NUMCESARE] INT MASKED WITH (FUNCTION = 'default()')                                              NULL,
    [NUMABORTO] INT MASKED WITH (FUNCTION = 'default()')                                              NULL,
    [NUMHIJVIV] INT MASKED WITH (FUNCTION = 'default()')                                              NULL,
    [NUMMORTIN] VARCHAR (50) MASKED WITH (FUNCTION = 'default()')                                     NULL,
    [NUMETOPIC] INT MASKED WITH (FUNCTION = 'default()')                                              NULL,
    [NUMEMOLAS] INT MASKED WITH (FUNCTION = 'default()')                                              NULL,
    [NUMEOVITO] INT MASKED WITH (FUNCTION = 'default()')                                              NULL,
    [FECULTMEN] VARCHAR (50)                                                                          NULL,
    [FECULTPAR] VARCHAR (50)                                                                          NULL,
    [FECULTCIT] VARCHAR (50) MASKED WITH (FUNCTION = 'default()')                                     NULL,
    [CODPLANIF] CHAR (2)                                                                              NULL,
    [OTROSANTE] VARCHAR (2000) MASKED WITH (FUNCTION = 'default()')                                   NULL,
    [CONTPRENA] BIT                                                                                   NULL,
    [CANTPRENA] INT                                                                                   NULL,
    [NOMSEMGES] NUMERIC (3, 1) MASKED WITH (FUNCTION = 'default()')                                   NULL,
    [INICONPRE] NUMERIC (3, 1) MASKED WITH (FUNCTION = 'default()')                                   NULL,
    [RIESOBTET] VARCHAR (200) MASKED WITH (FUNCTION = 'default()')                                    NULL,
    [RESCUAHEM] VARCHAR (400) MASKED WITH (FUNCTION = 'partial(0, "ResultLabObstetric_Ofuscado", 0)') NULL,
    [RESPARORI] VARCHAR (400) MASKED WITH (FUNCTION = 'partial(0, "ResultLabObstetric_Ofuscado", 0)') NULL,
    [TESTSULLI] VARCHAR (400) MASKED WITH (FUNCTION = 'partial(0, "ResultLabObstetric_Ofuscado", 0)') NULL,
    [GLUCBASAL] VARCHAR (400) MASKED WITH (FUNCTION = 'partial(0, "ResultLabObstetric_Ofuscado", 0)') NULL,
    [RESULVDRL] BIT MASKED WITH (FUNCTION = 'default()')                                              NULL,
    [DILUCVDRL] INT MASKED WITH (FUNCTION = 'default()')                                              NULL,
    [IGGTOXOPL] BIT MASKED WITH (FUNCTION = 'default()')                                              NULL,
    [CANTTOXO]  INT MASKED WITH (FUNCTION = 'default()')                                              NULL,
    [FECULTIGG] DATETIME MASKED WITH (FUNCTION = 'default()')                                         NULL,
    [IQMTOXOPL] CHAR (1) MASKED WITH (FUNCTION = 'partial(0, "ResultLabObstetric_Ofuscado", 0)')      NULL,
    [FECULTIQM] DATETIME                                                                              NULL,
    [RESULTHIV] BIT MASKED WITH (FUNCTION = 'default()')                                              NULL,
    [HEPATITIB] CHAR (1) MASKED WITH (FUNCTION = 'partial(0, "ResultLabObstetric_Ofuscado", 0)')      NULL,
    [CANTHEPAT] INT                                                                                   NULL,
    [OTROSOBST] VARCHAR (2000) MASKED WITH (FUNCTION = 'partial(0, "OtherObstetric_Ofuscado", 0)')    NULL,
    [INDAUDFOR] NUMERIC (18)                                                                          NOT NULL,
    [FECPROPAR] DATETIME                                                                              NULL,
    CONSTRAINT [PK_HCANTGINI] PRIMARY KEY CLUSTERED ([IDETIPHIS] ASC, [NUMEFOLIO] ASC, [IPCODPACI] ASC, [NUMINGRES] ASC),
    CONSTRAINT [FK_HCANTGINI_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCANTGINI_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCANTGINI_HCTIPPLAN] FOREIGN KEY ([CODPLANIF]) REFERENCES [dbo].[HCTIPPLAN] ([CODPLANIF]),
    CONSTRAINT [FK_HCANTGINI_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCANTGINI_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCANTGINI_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINI].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINI].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINI].[MENARQUIA]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINI].[CICLOSPAC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINI].[MENSTRDUR]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINI].[EDADVIDSE]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINI].[GESTACION]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINI].[NUMPARTO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINI].[NUMCESARE]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINI].[NUMABORTO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINI].[NUMHIJVIV]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINI].[NUMMORTIN]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINI].[NUMETOPIC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINI].[NUMEMOLAS]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINI].[NUMEOVITO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINI].[FECULTCIT]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINI].[OTROSANTE]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINI].[NOMSEMGES]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINI].[INICONPRE]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINI].[RIESOBTET]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINI].[RESCUAHEM]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINI].[RESPARORI]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINI].[TESTSULLI]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINI].[GLUCBASAL]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINI].[RESULVDRL]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINI].[DILUCVDRL]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINI].[IGGTOXOPL]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINI].[CANTTOXO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINI].[FECULTIGG]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINI].[IQMTOXOPL]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINI].[HEPATITIB]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTGINI].[OTROSOBST]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha probable de parto, estimada en semanas de gestación (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'FECPROPAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Probable Parto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'FECPROPAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'FECPROPAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de auditoría, identificador único de registro y trazabilidad (NUMERIC 18, PII)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Otros antecedentes obstétricos relevantes, ginecológicos adicionales (VARCHAR 2000, PII ofuscado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'OTROSOBST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ginecologicos -  Otros Obstetricos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'OTROSOBST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'OTROSOBST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad o número de referencia de exámenes Hepatitis B en control prenatal (INT, PII)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'CANTHEPAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Obstetricos - Cantidad o Numero Referenci Hepatitis B', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'CANTHEPAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'CANTHEPAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente Hepatitis B: 1=Positivo, 2=Negativo, 3=No tiene (CHAR 1, resultado laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'HEPATITIB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Obstetricos - Ant. Sup. Hepatitis B  1: + Positivo  2: - Negativo  3: No Tiene', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'HEPATITIB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'HEPATITIB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado VIH/HIV en control prenatal: True=Positivo, False=Negativo (BIT, PII)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'RESULTHIV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Obstetricos - HIV / VIH  True: Positivo  False: Negativo  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'RESULTHIV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'RESULTHIV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de último examen IqM (Inmunoglobulina cuantitativa Toxoplasma) (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'FECULTIQM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Ultimo Examen IqM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'FECULTIQM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'FECULTIQM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado IqM Toxoplasma en control prenatal (CHAR 1, resultado laboratorio PII)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'IQMTOXOPL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Obstetricos - IqM Toxoplasma  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'IQMTOXOPL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'IQMTOXOPL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de último examen IgG (Inmunoglobulina G Toxoplasma) (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'FECULTIGG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Ultimo Examen IgG', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'FECULTIGG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'FECULTIGG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad o número de referencia de exámenes IgG Toxoplasma (INT, resultado laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'CANTTOXO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Obstetricos - Cantidad o Numero referencia a IgG Toxoplasma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'CANTTOXO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'CANTTOXO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado IgG Toxoplasma en control prenatal obstetrico (BIT, serology)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'IGGTOXOPL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Obstetricos - IgG Toxoplasma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'IGGTOXOPL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'IGGTOXOPL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diluciones de VDRL (sífilis) en control prenatal obstetrico (INT, resultado laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'DILUCVDRL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Obstetricos - VDRL Diluciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'DILUCVDRL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'DILUCVDRL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado VDRL (sífilis): True=Positivo, False=Negativo en prenatal (BIT, serology)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'RESULVDRL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Obstetricos - Glucemia Basal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'RESULVDRL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'RESULVDRL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Glucemia basal en control prenatal, descarte diabetes gestacional (VARCHAR 400, laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'GLUCBASAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Obstetricos - Test Sullivan', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'GLUCBASAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'GLUCBASAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Test de Sullivan (tolerancia glucosa) en control prenatal obstetrico (VARCHAR 400, laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'TESTSULLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Obstetricos - Test Sullivan', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'TESTSULLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'TESTSULLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado parcial de orina en control prenatal obstetrico (VARCHAR 400, laboratorio PII)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'RESPARORI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Obstetricos - Parcial de Orina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'RESPARORI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'RESPARORI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado cuadro hematológico completo en control prenatal (VARCHAR 400, laboratorio PII)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'RESCUAHEM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Obstetricos - Cuadro Hematico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'RESCUAHEM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'RESCUAHEM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Factores de riesgo obstetrico identificados en control prenatal (VARCHAR 200, PII)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'RIESOBTET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Obstetricos - Inicio Control Prenatal Semanas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'RIESOBTET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'RIESOBTET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Semanas de gestación al inicio del control prenatal (NUMERIC 3,1, obstetrico)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'INICONPRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Obstetricos - Semanas de gestacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'INICONPRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'INICONPRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Semanas de gestación nominales al momento de registro (NUMERIC 3,1, obstetrico PII)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'NOMSEMGES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Obstetricos - Semanas de gestacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'NOMSEMGES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'NOMSEMGES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad total de controles prenatales realizados (INT, obstetrico)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'CANTPRENA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Obstetricos - Cantidad de Controles Prenatales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'CANTPRENA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'CANTPRENA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Control prenatal realizado: True=Sí, False=No (BIT, obstetrico)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'CONTPRENA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Obstetricos - Control Prenatal True:Si False:No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'CONTPRENA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'CONTPRENA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Otros antecedentes ginecológicos relevantes no categorizados (VARCHAR 2000, PII)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'OTROSANTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ginecologicos -  Otros Antecedentes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'OTROSANTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'OTROSANTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del tipo de planificación familiar utilizado (CHAR 2, FK HCTIPPLAN)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'CODPLANIF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ginecologicos - Codigo del Tipo de Planificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'CODPLANIF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'CODPLANIF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de última citología cervicovaginal, tamizaje cáncer (DATETIME, PII)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'FECULTCIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ginecologicos - Fecha Ultima Citologia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'FECULTCIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'FECULTCIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del último parto registrado en historia ginecológica (VARCHAR 50)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'FECULTPAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ginecologicos - Fecha Ultimo Parto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'FECULTPAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'FECULTPAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de última mola (embarazo molar) en antecedentes (VARCHAR 50, ginecológico)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'FECULTMEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ginecologicos - Mola', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'FECULTMEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'FECULTMEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número total de embarazos con ovitos (huevo sin embrión) (INT, ginecológico)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'NUMEOVITO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ginecologicos - Numero de Embarazos Ovitos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'NUMEOVITO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'NUMEOVITO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número total de embarazos ectópicos (fuera del útero) (INT, ginecológico)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'NUMEMOLAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ginecologicos - Numero de Embarazos Etopicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'NUMEMOLAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'NUMEMOLAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de embarazos ectópicos, gestación extrauterina (INT, ginecológico)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'NUMETOPIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ginecologicos - Numero de Embarazos Etopicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'NUMETOPIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'NUMETOPIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de mortinatos (fetos muertos al parto) en historia reproductiva (VARCHAR 50)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'NUMMORTIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ginecologicos - Numero de Mortinatos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'NUMMORTIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'NUMMORTIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de hijos nacidos vivos en historia reproductiva (INT, ginecológico)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'NUMHIJVIV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ginecologicos - Numero de Nacidos Vivos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'NUMHIJVIV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'NUMHIJVIV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número total de abortos en antecedentes ginecológicos (INT, obstetrico)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'NUMABORTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ginecologicos - Numero de Abortos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'NUMABORTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'NUMABORTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número total de partos por cesárea en antecedentes (INT, obstetrico)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'NUMCESARE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ginecologicos - Numero de Cesareas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'NUMCESARE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'NUMCESARE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número total de partos vaginales en antecedentes reproductivos (INT, ginecológico)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'NUMPARTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ginecologicos - Numero de Partos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'NUMPARTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'NUMPARTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número total de gestaciones, embarazos completados o no (INT, obstetrico)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'GESTACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ginecologicos - Numero de Gestaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'GESTACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'GESTACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad en años al inicio de vida sexual activa (INT, PII sensible)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'EDADVIDSE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad en la que Inicia Vida Sexual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'EDADVIDSE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'EDADVIDSE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ciclo menstrual regular: True=Sí, False=No (BIT, ginecológico)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'CICLOREGU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ciclo Regular:  True= Si  False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'CICLOREGU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'CICLOREGU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración de menstruación en días (INT, ginecológico)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'MENSTRDUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ginecologicos - Duracion de la menstracion expresados en Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'MENSTRDUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'MENSTRDUR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ciclo menstrual en días, intervalo entre menstruaciones (INT, ginecológico)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'CICLOSPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ginecologicos - Ciclos expresados en Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'CICLOSPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'CICLOSPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Menarquia expresada en años, edad de primera menstruación (INT, PII)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'MENARQUIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ginecologicos - Menarquia Epresado en Años', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'MENARQUIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'MENARQUIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de ingreso a historia clínica, consulta inicial (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'FECHISPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Historia Clinica Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'FECHISPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'FECHISPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud, médico/ginecólogo tratante (VARCHAR 25, FK INPROFSAL PII)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad funcional, servicio de ginecología/obstetricia (CHAR 10, FK INUNIFUNC)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención, institución prestadora (CHAR 10, FK ADCENATEN)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del ingreso, admisión a la institución de salud (CHAR 10, FK ADINGRESO)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente, identificación PII ofuscada (VARCHAR 25, FK INPACIENT, Identification_Ofuscado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio, secuencia de la historia clínica (NCHAR 10, PK)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno del tipo de historia clínica (CHAR 9, PK)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes ginecológicos y obstétricos de la paciente en la historia clínica. Registra información sobre el ciclo menstrual, paridad, gestaciones, resultados de exámenes de control prenatal (cuadro hemático, VDRL, toxoplasma, VIH, hepatitis B) y datos del embarazo actual, usada en la atención obstétrica y ginecológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI';
