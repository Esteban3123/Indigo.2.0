CREATE TABLE [dbo].[HCARCAUD] (
    [ID]        INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODSERIPS] CHAR (20)                                                                        NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [NUMEFOLIO] INT                                                                              NULL,
    [FECHA]     DATETIME                                                                         NOT NULL,
    [NOMBRE]    VARCHAR (200)                                                                    NOT NULL,
    CONSTRAINT [PK_HCARCAUD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCARCAUD_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCARCAUD_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_HCARCAUD_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCARCAUD] NOCHECK CONSTRAINT [FK_HCARCAUD_ADINGRESO];


GO
ALTER TABLE [dbo].[HCARCAUD] NOCHECK CONSTRAINT [FK_HCARCAUD_INCUPSIPS];


GO
ALTER TABLE [dbo].[HCARCAUD] NOCHECK CONSTRAINT [FK_HCARCAUD_INPACIENT];

