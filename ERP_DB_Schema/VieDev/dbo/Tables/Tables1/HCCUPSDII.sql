CREATE TABLE [dbo].[HCCUPSDII] (
    [IDETIPHIS] CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO] NCHAR (10)                                                                       NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODSERIPS] CHAR (20)                                                                        NOT NULL,
    [CODDIAGNO] CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NOT NULL,
    [MANEXTPRO] BIT                                                                              NOT NULL,
    [INDAUDFOR] NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    CONSTRAINT [PK_HCCUPSDII] PRIMARY KEY CLUSTERED ([INDAUDFOR] ASC),
    CONSTRAINT [FK_HCCUPSDII_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCCUPSDII_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_HCCUPSDII_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_HCCUPSDII_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCCUPSDII] NOCHECK CONSTRAINT [FK_HCCUPSDII_ADINGRESO];


GO
ALTER TABLE [dbo].[HCCUPSDII] NOCHECK CONSTRAINT [FK_HCCUPSDII_INCUPSIPS];


GO
ALTER TABLE [dbo].[HCCUPSDII] NOCHECK CONSTRAINT [FK_HCCUPSDII_INDIAGNOS];


GO
ALTER TABLE [dbo].[HCCUPSDII] NOCHECK CONSTRAINT [FK_HCCUPSDII_INPACIENT];

