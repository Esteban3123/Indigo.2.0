CREATE TABLE [dbo].[HCINFPROI] (
    [CODCONCEC] INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDETIPHIS] CHAR (9)                                                                         NULL,
    [NUMEFOLIO] NCHAR (10)                                                                       NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                        NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECREAPRO] DATETIME                                                                         NOT NULL,
    [CODSERIPS] CHAR (20)                                                                        NOT NULL,
    [DESHALLAZ] VARCHAR (3000)                                                                   NOT NULL,
    [DESCOMPLI] VARCHAR (3000)                                                                   NOT NULL,
    CONSTRAINT [PK_HCINFPROI] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC),
    CONSTRAINT [FK_HCINFPROI_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCINFPROI_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCINFPROI_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_HCINFPROI_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCINFPROI_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCINFPROI] NOCHECK CONSTRAINT [FK_HCINFPROI_ADCENATEN];


GO
ALTER TABLE [dbo].[HCINFPROI] NOCHECK CONSTRAINT [FK_HCINFPROI_ADINGRESO];


GO
ALTER TABLE [dbo].[HCINFPROI] NOCHECK CONSTRAINT [FK_HCINFPROI_INCUPSIPS];


GO
ALTER TABLE [dbo].[HCINFPROI] NOCHECK CONSTRAINT [FK_HCINFPROI_INPACIENT];


GO
ALTER TABLE [dbo].[HCINFPROI] NOCHECK CONSTRAINT [FK_HCINFPROI_INUNIFUNC];

