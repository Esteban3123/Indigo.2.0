CREATE TABLE [dbo].[HCJUSTECI] (
    [CODCONCEC] INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDETIPHIS] CHAR (9)                                                                         NULL,
    [NUMEFOLIO] NCHAR (10)                                                                       NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                        NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECORDMED] DATETIME                                                                         NOT NULL,
    [CODSERIPS] CHAR (20)                                                                        NOT NULL,
    [DESAYUDIA] VARCHAR (MAX)                                                                    NOT NULL,
    [DESJUSTEC] VARCHAR (MAX)                                                                    NOT NULL,
    [DESINCAYU] VARCHAR (MAX)                                                                    NOT NULL,
    CONSTRAINT [PK_HCJUSTECI] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC),
    CONSTRAINT [FK_HCJUSTECI_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCJUSTECI_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCJUSTECI_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_HCJUSTECI_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCJUSTECI_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCJUSTECI] NOCHECK CONSTRAINT [FK_HCJUSTECI_ADCENATEN];


GO
ALTER TABLE [dbo].[HCJUSTECI] NOCHECK CONSTRAINT [FK_HCJUSTECI_ADINGRESO];


GO
ALTER TABLE [dbo].[HCJUSTECI] NOCHECK CONSTRAINT [FK_HCJUSTECI_INCUPSIPS];


GO
ALTER TABLE [dbo].[HCJUSTECI] NOCHECK CONSTRAINT [FK_HCJUSTECI_INPACIENT];


GO
ALTER TABLE [dbo].[HCJUSTECI] NOCHECK CONSTRAINT [FK_HCJUSTECI_INUNIFUNC];

