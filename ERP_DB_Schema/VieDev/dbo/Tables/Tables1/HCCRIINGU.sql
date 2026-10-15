CREATE TABLE [dbo].[HCCRIINGU] (
    [NUMEFOLIO] CHAR (10)                                                                        NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODTIPCRI] NUMERIC (18)                                                                     NOT NULL,
    [INDAUDFOR] NUMERIC (18)                                                                     NOT NULL,
    CONSTRAINT [PK_HCCRIINGU] PRIMARY KEY CLUSTERED ([NUMEFOLIO] ASC, [IPCODPACI] ASC, [CODTIPCRI] ASC),
    CONSTRAINT [FK_HCCRIINGU_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCCRIINGU_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCCRIINGU] NOCHECK CONSTRAINT [FK_HCCRIINGU_ADINGRESO];


GO
ALTER TABLE [dbo].[HCCRIINGU] NOCHECK CONSTRAINT [FK_HCCRIINGU_INPacient];


GO
CREATE NONCLUSTERED INDEX [IX_HCCRIINGU__IPCODPACI__NUMINGRES]
    ON [dbo].[HCCRIINGU]([IPCODPACI] ASC, [NUMINGRES] ASC);

