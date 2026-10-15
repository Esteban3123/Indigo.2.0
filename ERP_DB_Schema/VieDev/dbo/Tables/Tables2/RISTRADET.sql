CREATE TABLE [dbo].[RISTRADET] (
    [CODCONSEC] VARCHAR (10)                                                                  NOT NULL,
    [CODCENATE] CHAR (10)                                                                     NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                     NOT NULL,
    [IDETIPHIS] CHAR (9)                                                                      NOT NULL,
    [NUMEFOLIO] NCHAR (10)                                                                    NOT NULL,
    [NUMINGRES] CHAR (10)                                                                     NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [FECHORORD] DATETIME                                                                      NOT NULL,
    [CODPROTEC] CHAR (20)                                                                     NULL,
    [FECHORINI] DATETIME                                                                      NULL,
    [FECHORFIN] DATETIME                                                                      NULL,
    [CODPRORAD] CHAR (20)                                                                     NULL,
    [FECHORLEC] NCHAR (10)                                                                    NULL,
    [CODUSUARI] CHAR (20)                                                                     NULL,
    [FECHORTRA] DATETIME                                                                      NULL,
    CONSTRAINT [PK_ITRADTRAD] PRIMARY KEY CLUSTERED ([CODCONSEC] ASC),
    CONSTRAINT [FK_RISTRADET_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_RISTRADET_RISTRACAB] FOREIGN KEY ([CODCONSEC]) REFERENCES [dbo].[RISTRACAB] ([CODCONSEC])
);


GO
ALTER TABLE [dbo].[RISTRADET] NOCHECK CONSTRAINT [FK_RISTRADET_ADINGRESO];


GO
ALTER TABLE [dbo].[RISTRADET] NOCHECK CONSTRAINT [FK_RISTRADET_RISTRACAB];

