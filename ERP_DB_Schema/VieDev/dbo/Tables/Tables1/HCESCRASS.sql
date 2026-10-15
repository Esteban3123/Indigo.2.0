CREATE TABLE [dbo].[HCESCRASS] (
    [CODCONCEC] NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                        NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECREGSIS] DATETIME                                                                         NOT NULL,
    [PUNTAJESC] VARCHAR (2)                                                                      NOT NULL,
    [NUMEFOLIO] NCHAR (10)                                                                       NULL,
    [CODESPECI] CHAR (3)                                                                         NULL,
    CONSTRAINT [PK_HCESCRASS] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC),
    CONSTRAINT [FK_HCESCRASS_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCESCRASS_HCESCRASS] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCESCRASS_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCESCRASS] NOCHECK CONSTRAINT [FK_HCESCRASS_ADINGRESO];


GO
ALTER TABLE [dbo].[HCESCRASS] NOCHECK CONSTRAINT [FK_HCESCRASS_HCESCRASS];


GO
ALTER TABLE [dbo].[HCESCRASS] NOCHECK CONSTRAINT [FK_HCESCRASS_INPACIENT];

