CREATE TABLE [dbo].[HCAJUSIND] (
    [CODCONCEC] INT                                                                              NOT NULL,
    [CODPRODUC] CHAR (20)                                                                        NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                        NOT NULL,
    [CODUSUARI] CHAR (20)                                                                        NOT NULL,
    [CANTTOTAL] INT                                                                              NOT NULL,
    [CANTAJUST] INT                                                                              NOT NULL,
    CONSTRAINT [PK_HCAJUSIND] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC, [CODPRODUC] ASC),
    CONSTRAINT [FK_HCAJUSIND_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCAJUSIND_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCAJUSIND_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC]),
    CONSTRAINT [FK_HCAJUSIND_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCAJUSIND_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCAJUSIND] NOCHECK CONSTRAINT [FK_HCAJUSIND_ADCENATEN];


GO
ALTER TABLE [dbo].[HCAJUSIND] NOCHECK CONSTRAINT [FK_HCAJUSIND_ADINGRESO];


GO
ALTER TABLE [dbo].[HCAJUSIND] NOCHECK CONSTRAINT [FK_HCAJUSIND_IHLISTPRO];


GO
ALTER TABLE [dbo].[HCAJUSIND] NOCHECK CONSTRAINT [FK_HCAJUSIND_INPACIENT];


GO
ALTER TABLE [dbo].[HCAJUSIND] NOCHECK CONSTRAINT [FK_HCAJUSIND_INUNIFUNC];

