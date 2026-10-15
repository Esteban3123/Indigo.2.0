CREATE TABLE [dbo].[HCESCNTON] (
    [CODCONCEC]  NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]  VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]  CHAR (10)                                                                        NOT NULL,
    [CODCENATE]  CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]  CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]  CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECREGSIS]  DATETIME                                                                         NOT NULL,
    [ESTFISGEN]  TINYINT                                                                          NOT NULL,
    [ESTMENTAL]  TINYINT                                                                          NOT NULL,
    [MOVILIDAD]  TINYINT                                                                          NOT NULL,
    [ACTIVIDAD]  TINYINT                                                                          NOT NULL,
    [INCTINENCI] TINYINT                                                                          NOT NULL,
    [HCESCALAID] INT                                                                              NULL,
    [CODESPECI]  CHAR (3)                                                                         NULL,
    CONSTRAINT [PK_HCESCNTON] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC),
    CONSTRAINT [FK_HCESCNTON_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCESCNTON_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCESCNTON_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCESCNTON] NOCHECK CONSTRAINT [FK_HCESCNTON_ADINGRESO];


GO
ALTER TABLE [dbo].[HCESCNTON] NOCHECK CONSTRAINT [FK_HCESCNTON_INESPECIA];


GO
ALTER TABLE [dbo].[HCESCNTON] NOCHECK CONSTRAINT [FK_HCESCNTON_INPACIENT];

