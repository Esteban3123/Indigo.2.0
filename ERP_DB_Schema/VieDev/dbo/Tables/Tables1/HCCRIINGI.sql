CREATE TABLE [dbo].[HCCRIINGI] (
    [NUMEFOLIO] CHAR (10)                                                                        NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODTIPCRI] NUMERIC (18)                                                                     NOT NULL,
    [INDAUDFOR] NUMERIC (18)                                                                     NOT NULL,
    CONSTRAINT [PK_HCCRIINGI] PRIMARY KEY CLUSTERED ([NUMEFOLIO] ASC, [IPCODPACI] ASC, [CODTIPCRI] ASC),
    CONSTRAINT [FK_HCCRIINGI_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCCRIINGI_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCCRIINGI] NOCHECK CONSTRAINT [FK_HCCRIINGI_ADINGRESO];


GO
ALTER TABLE [dbo].[HCCRIINGI] NOCHECK CONSTRAINT [FK_HCCRIINGI_INPACIENT];

