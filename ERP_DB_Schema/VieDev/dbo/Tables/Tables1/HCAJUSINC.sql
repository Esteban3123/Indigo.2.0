CREATE TABLE [dbo].[HCAJUSINC] (
    [CODCONCEC]  INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FECHAREGIS] DATETIME                                                                         NOT NULL,
    [CODUSUARI]  CHAR (20)                                                                        NOT NULL,
    [IPCODPACI]  VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]  CHAR (10)                                                                        NOT NULL,
    [CODCENATE]  CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]  CHAR (10)                                                                        NOT NULL,
    [CODCONAJU]  CHAR (3)                                                                         NOT NULL,
    [TIPOAJUSTE] CHAR (1)                                                                         NOT NULL,
    [OBSERVACI]  VARCHAR (200)                                                                    NULL,
    CONSTRAINT [PK_HCAJUSINC] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC),
    CONSTRAINT [FK_HCAJUSINC_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCAJUSINC_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCAJUSINC_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCAJUSINC_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCAJUSINC] NOCHECK CONSTRAINT [FK_HCAJUSINC_ADCENATEN];


GO
ALTER TABLE [dbo].[HCAJUSINC] NOCHECK CONSTRAINT [FK_HCAJUSINC_ADINGRESO];


GO
ALTER TABLE [dbo].[HCAJUSINC] NOCHECK CONSTRAINT [FK_HCAJUSINC_INPACIENT];


GO
ALTER TABLE [dbo].[HCAJUSINC] NOCHECK CONSTRAINT [FK_HCAJUSINC_INUNIFUNC];

