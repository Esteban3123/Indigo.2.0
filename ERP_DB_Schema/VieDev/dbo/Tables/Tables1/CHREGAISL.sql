CREATE TABLE [dbo].[CHREGAISL] (
    [CODICAMAS] INT                                                                              NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [FECINIAIS] DATETIME                                                                         NOT NULL,
    [FECFINAIS] DATETIME                                                                         NOT NULL,
    [CODAISLAM] INT                                                                              NOT NULL,
    [REGANULAD] BIT                                                                              NOT NULL,
    [INDAUDFOR] NUMERIC (18)                                                                     NOT NULL,
    CONSTRAINT [PK_CHREGAISL] PRIMARY KEY CLUSTERED ([CODICAMAS] ASC, [NUMINGRES] ASC, [IPCODPACI] ASC, [FECINIAIS] ASC),
    CONSTRAINT [FK_CHREGAISL_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_CHREGAISL_CHCAMASHO] FOREIGN KEY ([CODICAMAS]) REFERENCES [dbo].[CHCAMASHO] ([CODICAMAS]),
    CONSTRAINT [FK_CHREGAISL_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[CHREGAISL] NOCHECK CONSTRAINT [FK_CHREGAISL_ADINGRESO];


GO
ALTER TABLE [dbo].[CHREGAISL] NOCHECK CONSTRAINT [FK_CHREGAISL_CHCAMASHO];


GO
ALTER TABLE [dbo].[CHREGAISL] NOCHECK CONSTRAINT [FK_CHREGAISL_INPacient];

