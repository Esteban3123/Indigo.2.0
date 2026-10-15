CREATE TABLE [dbo].[HCPATREMI] (
    [CODCONSEC] NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NUMEFOLIO] NCHAR (10)                                                                       NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                        NOT NULL,
    [CODSERIPS] CHAR (20)                                                                        NOT NULL,
    [CODCENLAB] CHAR (100)                                                                       NOT NULL,
    [OBSERVACI] CHAR (80)                                                                        NULL,
    [FECREMPAT] DATETIME                                                                         NOT NULL,
    [USUREMPAT] CHAR (20)                                                                        NOT NULL,
    CONSTRAINT [PK_HCPATREMI] PRIMARY KEY CLUSTERED ([CODCONSEC] ASC),
    CONSTRAINT [FK_HCPATREMI_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCPATREMI_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCPATREMI] NOCHECK CONSTRAINT [FK_HCPATREMI_ADINGRESO];


GO
ALTER TABLE [dbo].[HCPATREMI] NOCHECK CONSTRAINT [FK_HCPATREMI_INPACIENT];

