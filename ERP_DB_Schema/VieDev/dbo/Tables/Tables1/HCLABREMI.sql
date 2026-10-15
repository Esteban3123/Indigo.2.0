CREATE TABLE [dbo].[HCLABREMI] (
    [CODCONSEC] NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NUMEFOLIO] NCHAR (10)                                                                       NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                        NOT NULL,
    [CODSERIPS] CHAR (20)                                                                        NOT NULL,
    [CODCENLAB] CHAR (100)                                                                       NOT NULL,
    [OBSERVACI] CHAR (80)                                                                        NULL,
    [FECREMLAB] DATETIME                                                                         NOT NULL,
    [USUREMLAB] CHAR (20)                                                                        NOT NULL,
    CONSTRAINT [PK_HCLABREMI] PRIMARY KEY CLUSTERED ([CODCONSEC] ASC),
    CONSTRAINT [FK_HCLABREMI_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCLABREMI_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCLABREMI_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_HCLABREMI_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCLABREMI_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCLABREMI] NOCHECK CONSTRAINT [FK_HCLABREMI_ADCENATEN];


GO
ALTER TABLE [dbo].[HCLABREMI] NOCHECK CONSTRAINT [FK_HCLABREMI_ADINGRESO];


GO
ALTER TABLE [dbo].[HCLABREMI] NOCHECK CONSTRAINT [FK_HCLABREMI_INCUPSIPS];


GO
ALTER TABLE [dbo].[HCLABREMI] NOCHECK CONSTRAINT [FK_HCLABREMI_INPACIENT];


GO
ALTER TABLE [dbo].[HCLABREMI] NOCHECK CONSTRAINT [FK_HCLABREMI_INUNIFUNC];


GO
CREATE NONCLUSTERED INDEX [IX_HCLABREMI__NUMEFOLIO__IPCODPACI__NUMINGRES__UFUCODIGO__CODCENATE__CODSERIPS]
    ON [dbo].[HCLABREMI]([NUMEFOLIO] ASC, [IPCODPACI] ASC, [NUMINGRES] ASC, [UFUCODIGO] ASC, [CODCENATE] ASC, [CODSERIPS] ASC);

