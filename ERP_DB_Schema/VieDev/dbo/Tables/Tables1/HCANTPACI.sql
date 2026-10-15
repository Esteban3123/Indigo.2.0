CREATE TABLE [dbo].[HCANTPACI] (
    [IDETIPHIS]         CHAR (9)                                                                              NOT NULL,
    [NUMEFOLIO]         NCHAR (10)                                                                            NOT NULL,
    [IPCODPACI]         VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')      NOT NULL,
    [NUMINGRES]         CHAR (10)                                                                             NOT NULL,
    [CODCENATE]         CHAR (10)                                                                             NOT NULL,
    [UFUCODIGO]         CHAR (10)                                                                             NOT NULL,
    [CODPROSAL]         CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')         NOT NULL,
    [FECHISPAC]         DATETIME                                                                              NOT NULL,
    [ANTMEDPAC]         VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTQUIPAC]         VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTTRAPAC]         VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTINMPAC]         VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTALEPAC]         VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTTRUPAC]         VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTPSIPAC]         VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTFARPAC]         VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTFAMPAC]         VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTTOXPAC]         VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTGINPAC]         VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTOTRPAC]         VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTPERINA]         VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [INDAUDFOR]         NUMERIC (18)                                                                          NOT NULL,
    [ANTANESTE]         VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTHABVID]         VARCHAR (MAX)                                                                         NULL,
    [ANTESCOLARES]      VARCHAR (MAX)                                                                         NULL,
    [ANTLABORALES]      VARCHAR (MAX)                                                                         NULL,
    [ANTNUTRICION]      VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTODONTOLOG]      VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTSOCIOECON]      VARCHAR (MAX)                                                                         NULL,
    [ANTUROLOGIASEXUAL] VARCHAR (MAX)                                                                         NULL,
    [Ophthalmological]  VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    CONSTRAINT [PK_HCANTPACI] PRIMARY KEY CLUSTERED ([IDETIPHIS] ASC, [NUMEFOLIO] ASC, [IPCODPACI] ASC, [NUMINGRES] ASC),
    CONSTRAINT [FK_HCANTPACI_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCANTPACI_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCANTPACI_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCANTPACI_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCANTPACI_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCANTPACI] NOCHECK CONSTRAINT [FK_HCANTPACI_ADcenaten];


GO
ALTER TABLE [dbo].[HCANTPACI] NOCHECK CONSTRAINT [FK_HCANTPACI_ADINGRESO];


GO
ALTER TABLE [dbo].[HCANTPACI] NOCHECK CONSTRAINT [FK_HCANTPACI_INPacient];


GO
ALTER TABLE [dbo].[HCANTPACI] NOCHECK CONSTRAINT [FK_HCANTPACI_INPROFSAL];


GO
ALTER TABLE [dbo].[HCANTPACI] NOCHECK CONSTRAINT [FK_HCANTPACI_INUNIFUNC];


GO
CREATE NONCLUSTERED INDEX [IX_HCANTPACI_NUMINGRES]
    ON [dbo].[HCANTPACI]([NUMINGRES] ASC)
    INCLUDE([ANTALEPAC], [ANTFAMPAC], [ANTFARPAC], [ANTGINPAC], [ANTINMPAC], [ANTMEDPAC], [ANTOTRPAC], [ANTPERINA], [ANTPSIPAC], [ANTQUIPAC], [ANTTOXPAC], [ANTTRAPAC], [ANTTRUPAC]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antecedentes Oftalmológicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPACI', @level2type = N'COLUMN', @level2name = N'Ophthalmological';

