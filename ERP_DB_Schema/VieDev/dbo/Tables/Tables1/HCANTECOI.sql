CREATE TABLE [dbo].[HCANTECOI] (
    [CONSECUTI] NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDETIPHIS] CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO] NCHAR (10)                                                                       NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                        NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECULTECO] DATETIME                                                                         NOT NULL,
    [NUMSEMULT] NUMERIC (3, 1)                                                                   NOT NULL,
    [FECHISPAC] DATETIME                                                                         NOT NULL,
    [NUMSEMHIS] NUMERIC (3, 1)                                                                   NOT NULL,
    CONSTRAINT [PK_HCANTECOI] PRIMARY KEY CLUSTERED ([CONSECUTI] ASC),
    CONSTRAINT [FK_HCANTECOI_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCANTECOI_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCANTECOI_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCANTECOI_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCANTECOI_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCANTECOI] NOCHECK CONSTRAINT [FK_HCANTECOI_ADCENATEN];


GO
ALTER TABLE [dbo].[HCANTECOI] NOCHECK CONSTRAINT [FK_HCANTECOI_ADINGRESO];


GO
ALTER TABLE [dbo].[HCANTECOI] NOCHECK CONSTRAINT [FK_HCANTECOI_INPACIENT];


GO
ALTER TABLE [dbo].[HCANTECOI] NOCHECK CONSTRAINT [FK_HCANTECOI_INPROFSAL];


GO
ALTER TABLE [dbo].[HCANTECOI] NOCHECK CONSTRAINT [FK_HCANTECOI_INUNIFUNC];

