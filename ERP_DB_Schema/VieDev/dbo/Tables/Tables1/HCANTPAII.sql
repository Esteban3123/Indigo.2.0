CREATE TABLE [dbo].[HCANTPAII] (
    [IDETIPHIS]         CHAR (9)                                                                               NOT NULL,
    [NUMEFOLIO]         NCHAR (10)                                                                             NOT NULL,
    [IPCODPACI]         VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')       NOT NULL,
    [NUMINGRES]         CHAR (10)                                                                              NOT NULL,
    [CODCENATE]         CHAR (10)                                                                              NOT NULL,
    [UFUCODIGO]         CHAR (10)                                                                              NOT NULL,
    [CODPROSAL]         CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')          NOT NULL,
    [FECHISPAC]         DATETIME                                                                               NOT NULL,
    [ANTMEDPAC]         VARCHAR (8000) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTQUIPAC]         VARCHAR (8000) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTTRAPAC]         VARCHAR (8000) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTINMPAC]         VARCHAR (8000) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTALEPAC]         VARCHAR (8000) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTTRUPAC]         VARCHAR (8000) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTPSIPAC]         VARCHAR (8000) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTFARPAC]         VARCHAR (8000) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTFAMPAC]         VARCHAR (8000) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTTOXPAC]         VARCHAR (8000) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTGINPAC]         VARCHAR (8000) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTOTRPAC]         VARCHAR (8000) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTPERINA]         VARCHAR (8000) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [INDAUDFOR]         NUMERIC (18)                                                                           NOT NULL,
    [ANTESCOLARES]      VARCHAR (8000)                                                                         NULL,
    [ANTLABORALES]      VARCHAR (8000)                                                                         NULL,
    [ANTNUTRICION]      VARCHAR (8000) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTODONTOLOG]      VARCHAR (8000) MASKED WITH (FUNCTION = 'partial(0, "ClinicalBackground_Ofuscado", 0)') NULL,
    [ANTSOCIOECON]      VARCHAR (8000)                                                                         NULL,
    [ANTUROLOGIASEXUAL] VARCHAR (8000)                                                                         NULL,
    CONSTRAINT [PK_HCANTPAII] PRIMARY KEY CLUSTERED ([IDETIPHIS] ASC, [NUMEFOLIO] ASC, [IPCODPACI] ASC, [NUMINGRES] ASC),
    CONSTRAINT [FK_HCANTPAII_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCANTPAII_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCANTPAII_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCANTPAII_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCANTPAII] NOCHECK CONSTRAINT [FK_HCANTPAII_ADcenaten];


GO
ALTER TABLE [dbo].[HCANTPAII] NOCHECK CONSTRAINT [FK_HCANTPAII_ADINGRESO];


GO
ALTER TABLE [dbo].[HCANTPAII] NOCHECK CONSTRAINT [FK_HCANTPAII_INPACIENT];


GO
ALTER TABLE [dbo].[HCANTPAII] NOCHECK CONSTRAINT [FK_HCANTPAII_INUNIFUNC];

