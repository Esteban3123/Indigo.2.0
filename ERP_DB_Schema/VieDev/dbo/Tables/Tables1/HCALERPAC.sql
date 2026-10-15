CREATE TABLE [dbo].[HCALERPAC] (
    [IDETIPHIS] CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO] NCHAR (10)                                                                       NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODSERIPS] CHAR (20)                                                                        NOT NULL,
    [INDORIGEN] CHAR (1)                                                                         NOT NULL,
    CONSTRAINT [PK_HCALERPAC] PRIMARY KEY CLUSTERED ([IDETIPHIS] ASC, [NUMEFOLIO] ASC, [IPCODPACI] ASC, [NUMINGRES] ASC, [CODSERIPS] ASC),
    CONSTRAINT [FK_HCALERPAC_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCALERPAC_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_HCALERPAC_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCALERPAC] NOCHECK CONSTRAINT [FK_HCALERPAC_ADINGRESO];


GO
ALTER TABLE [dbo].[HCALERPAC] NOCHECK CONSTRAINT [FK_HCALERPAC_INCUPSIPS];


GO
ALTER TABLE [dbo].[HCALERPAC] NOCHECK CONSTRAINT [FK_HCALERPAC_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identifica el Tab en donde se mostrar la alerta  1: Laboratorio  2: Imagenologia  3: Patologia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALERPAC', @level2type = N'COLUMN', @level2name = N'INDORIGEN';

