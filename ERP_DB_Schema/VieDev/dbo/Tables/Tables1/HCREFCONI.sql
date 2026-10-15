CREATE TABLE [dbo].[HCREFCONI] (
    [AUTO]       INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]  VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]  CHAR (10)                                                                        NOT NULL,
    [CODCENATE]  CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]  CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]  CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [MUNCODIGO]  CHAR (5)                                                                         NULL,
    [SERVIDOREM] CHAR (50)                                                                        NOT NULL,
    [CODIGOIPS]  CHAR (100)                                                                       NULL,
    [NIVELREMIT] CHAR (10)                                                                        NOT NULL,
    [FECSOLICIT] DATETIME                                                                         NULL,
    [FECCONFIR]  DATETIME                                                                         NULL,
    [CODESPECI]  CHAR (3)                                                                         NULL,
    [CONFIRMAP]  CHAR (50)                                                                        NOT NULL,
    [FECLLEGADA] DATETIME                                                                         NULL,
    [QUIENSOLRE] CHAR (50)                                                                        NULL,
    [MOTREMISI]  CHAR (30)                                                                        NULL,
    [OBSERVACIO] VARCHAR (MAX)                                                                    NULL,
    [CODUSUMOD]  CHAR (20)                                                                        NULL,
    [FECMODREG]  DATETIME                                                                         NULL,
    [FECREGSIS]  DATETIME                                                                         NULL,
    CONSTRAINT [PK_HCREFCONI] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_HCREFCONI_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCREFCONI_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCREFCONI] NOCHECK CONSTRAINT [FK_HCREFCONI_ADINGRESO];


GO
ALTER TABLE [dbo].[HCREFCONI] NOCHECK CONSTRAINT [FK_HCREFCONI_INPACIENT];

