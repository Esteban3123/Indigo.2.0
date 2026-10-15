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
ALTER TABLE [dbo].[HCANTGINI] NOCHECK CONSTRAINT [FK_HCANTGINI_ADcenaten];


GO
ALTER TABLE [dbo].[HCANTGINI] NOCHECK CONSTRAINT [FK_HCANTGINI_ADINGRESO];


GO
ALTER TABLE [dbo].[HCANTGINI] NOCHECK CONSTRAINT [FK_HCANTGINI_HCTIPPLAN];


GO
ALTER TABLE [dbo].[HCANTGINI] NOCHECK CONSTRAINT [FK_HCANTGINI_INPACIENT];


GO
ALTER TABLE [dbo].[HCANTGINI] NOCHECK CONSTRAINT [FK_HCANTGINI_INPROFSAL];


GO
ALTER TABLE [dbo].[HCANTGINI] NOCHECK CONSTRAINT [FK_HCANTGINI_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Obstetricos - Ant. Sup. Hepatitis B  1: + Positivo  2: - Negativo  3: No Tiene', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'HEPATITIB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Obstetricos - HIV / VIH  True: Positivo  False: Negativo  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'RESULTHIV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Obstetricos - IqM Toxoplasma  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'IQMTOXOPL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ciclo Regular:  True= Si  False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'CICLOREGU';

