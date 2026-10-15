CREATE TABLE [dbo].[HCSOLINSC] (
    [CODCONCEC] CHAR (10)                                                                        NOT NULL,
    [FECHAORDE] DATETIME                                                                         NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [IDETIPHIS] CHAR (9)                                                                         NULL,
    [NUMEFOLIO] CHAR (10)                                                                        NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                        NOT NULL,
    [INDAUDFOR] NUMERIC (18)                                                                     NOT NULL,
    [ID]        INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    CONSTRAINT [PK_HCSOLINSC] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCSOLINSC_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCSOLINSC_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCSOLINSC_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCSOLINSC_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCSOLINSC_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCSOLINSC] NOCHECK CONSTRAINT [FK_HCSOLINSC_ADcenaten];


GO
ALTER TABLE [dbo].[HCSOLINSC] NOCHECK CONSTRAINT [FK_HCSOLINSC_ADINGRESO];


GO
ALTER TABLE [dbo].[HCSOLINSC] NOCHECK CONSTRAINT [FK_HCSOLINSC_INPacient];


GO
ALTER TABLE [dbo].[HCSOLINSC] NOCHECK CONSTRAINT [FK_HCSOLINSC_INPROFSAL];


GO
ALTER TABLE [dbo].[HCSOLINSC] NOCHECK CONSTRAINT [FK_HCSOLINSC_INUNIFUNC];


GO
CREATE NONCLUSTERED INDEX [IX_HCSOLINSC_CODCONCEC]
    ON [dbo].[HCSOLINSC]([CODCONCEC] ASC);

