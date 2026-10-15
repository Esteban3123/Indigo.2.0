CREATE TABLE [dbo].[HCREGDIEI] (
    [CONSECUTI] INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDETIPHIS] CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO] CHAR (10)                                                                        NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                        NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [CODTIPDIE] CHAR (3)                                                                         NOT NULL,
    [OBSERVACI] VARCHAR (500)                                                                    NULL,
    [INDAUDFOR] NUMERIC (18)                                                                     NOT NULL,
    CONSTRAINT [PK_HCREGDIEI] PRIMARY KEY CLUSTERED ([CONSECUTI] ASC),
    CONSTRAINT [FK_HCREGDIEI_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCREGDIEI_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCREGDIEI_CHTIPDIET] FOREIGN KEY ([CODTIPDIE]) REFERENCES [dbo].[CHTIPDIET] ([CODTIPDIE]),
    CONSTRAINT [FK_HCREGDIEI_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCREGDIEI_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCREGDIEI] NOCHECK CONSTRAINT [FK_HCREGDIEI_ADcenaten];


GO
ALTER TABLE [dbo].[HCREGDIEI] NOCHECK CONSTRAINT [FK_HCREGDIEI_ADINGRESO];


GO
ALTER TABLE [dbo].[HCREGDIEI] NOCHECK CONSTRAINT [FK_HCREGDIEI_CHTIPDIET];


GO
ALTER TABLE [dbo].[HCREGDIEI] NOCHECK CONSTRAINT [FK_HCREGDIEI_INPACIENT];


GO
ALTER TABLE [dbo].[HCREGDIEI] NOCHECK CONSTRAINT [FK_HCREGDIEI_INUNIFUNC];


GO
CREATE NONCLUSTERED INDEX [IX_HCREGDIEI__IPCODPACI__NUMINGRES__NUMEFOLIO]
    ON [dbo].[HCREGDIEI]([IPCODPACI] ASC, [NUMINGRES] ASC, [NUMEFOLIO] ASC);

