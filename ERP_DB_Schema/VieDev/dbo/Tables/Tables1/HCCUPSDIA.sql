CREATE TABLE [dbo].[HCCUPSDIA] (
    [IDETIPHIS]                CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO]                NCHAR (10)                                                                       NOT NULL,
    [IPCODPACI]                VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]                CHAR (10)                                                                        NOT NULL,
    [CODSERIPS]                CHAR (20)                                                                        NOT NULL,
    [CODDIAGNO]                CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NOT NULL,
    [MANEXTPRO]                BIT                                                                              NOT NULL,
    [INDAUDFOR]                NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDDESCRIPCIONRELACIONADA] INT                                                                              NULL,
    CONSTRAINT [PK_HCCUPSDIA] PRIMARY KEY CLUSTERED ([INDAUDFOR] ASC),
    CONSTRAINT [FK_HCCUPSDIA_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCCUPSDIA_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_HCCUPSDIA_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_HCCUPSDIA_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCCUPSDIA] NOCHECK CONSTRAINT [FK_HCCUPSDIA_ADINGRESO];


GO
ALTER TABLE [dbo].[HCCUPSDIA] NOCHECK CONSTRAINT [FK_HCCUPSDIA_INCUPSIPS];


GO
ALTER TABLE [dbo].[HCCUPSDIA] NOCHECK CONSTRAINT [FK_HCCUPSDIA_INDIAGNOS];


GO
ALTER TABLE [dbo].[HCCUPSDIA] NOCHECK CONSTRAINT [FK_HCCUPSDIA_INPacient];


GO
CREATE NONCLUSTERED INDEX [IX_HCCUPSDIA_CODDIAGNO_CODSERIPS_IDETIPHIS_IPCODPACI_NUMEFOLIO_NUMINGRES_MANEXTPRO]
    ON [dbo].[HCCUPSDIA]([CODDIAGNO] ASC, [CODSERIPS] ASC, [IDETIPHIS] ASC, [IPCODPACI] ASC, [NUMEFOLIO] ASC, [NUMINGRES] ASC, [MANEXTPRO] ASC);

