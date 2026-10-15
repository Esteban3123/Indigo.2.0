CREATE TABLE [dbo].[HCREGEST] (
    [IDETIPHIS] CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO] NCHAR (10)                                                                       NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                        NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [CODICAMAS] INT                                                                              NOT NULL,
    [CODTIPEST] CHAR (3)                                                                         NOT NULL,
    [FECINIEST] DATETIME                                                                         NULL,
    [INDAUDFOR] NUMERIC (18)                                                                     NOT NULL,
    [JUSCAMANT] VARCHAR (4000)                                                                   NULL,
    CONSTRAINT [PK_HCREGEST] PRIMARY KEY CLUSTERED ([NUMEFOLIO] ASC, [IPCODPACI] ASC, [NUMINGRES] ASC),
    CONSTRAINT [FK_HCREGEST_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCREGEST_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCREGEST_CHCAMASHO] FOREIGN KEY ([CODICAMAS]) REFERENCES [dbo].[CHCAMASHO] ([CODICAMAS]),
    CONSTRAINT [FK_HCREGEST_CHTIPESTA] FOREIGN KEY ([CODTIPEST]) REFERENCES [dbo].[CHTIPESTA] ([CODTIPEST]),
    CONSTRAINT [FK_HCREGEST_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCREGEST_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCREGEST_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCREGEST] NOCHECK CONSTRAINT [FK_HCREGEST_ADcenaten];


GO
ALTER TABLE [dbo].[HCREGEST] NOCHECK CONSTRAINT [FK_HCREGEST_ADINGRESO];


GO
ALTER TABLE [dbo].[HCREGEST] NOCHECK CONSTRAINT [FK_HCREGEST_CHCAMASHO];


GO
ALTER TABLE [dbo].[HCREGEST] NOCHECK CONSTRAINT [FK_HCREGEST_CHTIPESTA];


GO
ALTER TABLE [dbo].[HCREGEST] NOCHECK CONSTRAINT [FK_HCREGEST_INPacient];


GO
ALTER TABLE [dbo].[HCREGEST] NOCHECK CONSTRAINT [FK_HCREGEST_INPROFSAL];


GO
ALTER TABLE [dbo].[HCREGEST] NOCHECK CONSTRAINT [FK_HCREGEST_INUNIFUNC];


GO
CREATE NONCLUSTERED INDEX [IX_HCREGEST__IPCODPACI__NUMINGRES]
    ON [dbo].[HCREGEST]([IPCODPACI] ASC, [NUMINGRES] ASC);

