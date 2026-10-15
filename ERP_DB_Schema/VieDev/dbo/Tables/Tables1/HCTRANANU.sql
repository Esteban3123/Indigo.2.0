CREATE TABLE [dbo].[HCTRANANU] (
    [CODCONSEC] INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NUMEFOLIO] NCHAR (10)                                                                       NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                        NOT NULL,
    [CODSERIPS] CHAR (20)                                                                        NOT NULL,
    [CODMOTANU] INT                                                                              NOT NULL,
    [OBSERVACI] CHAR (80)                                                                        NULL,
    [FECANUTRA] DATETIME                                                                         NOT NULL,
    [USUANUTRA] CHAR (20)                                                                        NOT NULL,
    CONSTRAINT [PK_HCTRANANU] PRIMARY KEY CLUSTERED ([CODCONSEC] ASC),
    CONSTRAINT [FK_HCTRANANU_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCTRANANU_HCMOANTRA] FOREIGN KEY ([CODMOTANU]) REFERENCES [dbo].[HCMOANTRA] ([CODANUTRA]),
    CONSTRAINT [FK_HCTRANANU_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCTRANANU] NOCHECK CONSTRAINT [FK_HCTRANANU_ADINGRESO];


GO
ALTER TABLE [dbo].[HCTRANANU] NOCHECK CONSTRAINT [FK_HCTRANANU_HCMOANTRA];


GO
ALTER TABLE [dbo].[HCTRANANU] NOCHECK CONSTRAINT [FK_HCTRANANU_INPACIENT];

