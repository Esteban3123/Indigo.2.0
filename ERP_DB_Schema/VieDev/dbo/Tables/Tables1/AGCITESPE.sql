CREATE TABLE [dbo].[AGCITESPE] (
    [CODAUTONU] INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODAUTOCI] INT                                                                              NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [FECHACITA] DATETIME                                                                         NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODESPECI] CHAR (3)                                                                         NOT NULL,
    [CODACTMED] CHAR (3)                                                                         NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NULL,
    [FECREGSIS] DATETIME                                                                         NOT NULL,
    [CITAASIGN] BIT                                                                              NOT NULL,
    [CODUSUARI] CHAR (20)                                                                        NOT NULL,
    [FECHELIMI] DATETIME                                                                         NOT NULL,
    [CODCAUCAN] CHAR (3)                                                                         NOT NULL,
    [OBSCAUCAN] NCHAR (100)                                                                      NULL,
    CONSTRAINT [PK_AGCITESPE] PRIMARY KEY CLUSTERED ([CODAUTONU] ASC),
    CONSTRAINT [FK_AGCITESPE_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[AGCITESPE] NOCHECK CONSTRAINT [FK_AGCITESPE_INPACIENT];


GO
CREATE NONCLUSTERED INDEX [IX_AGCITESPE__IPCODPACI__CODESPECI]
    ON [dbo].[AGCITESPE]([IPCODPACI] ASC, [CODESPECI] ASC);

