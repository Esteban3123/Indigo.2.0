CREATE TABLE [dbo].[ESCALAVALE] (
    [ID]              INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NUMEFOLIO]       CHAR (10)                                                                        NULL,
    [IPCODPACI]       VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [FECHAREGISTRO]   DATETIME                                                                         NOT NULL,
    [NUMINGRES]       CHAR (10)                                                                        NOT NULL,
    [RESULTADO]       BIT                                                                              NOT NULL,
    [USUARIOREGISTRO] CHAR (20)                                                                        NOT NULL,
    [CODESPECI]       CHAR (3)                                                                         NULL,
    CONSTRAINT [PK_ESCALAVALE] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ESCALAVALE_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_ESCALAVALE_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_ESCALAVALE_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[ESCALAVALE] NOCHECK CONSTRAINT [FK_ESCALAVALE_ADINGRESO];


GO
ALTER TABLE [dbo].[ESCALAVALE] NOCHECK CONSTRAINT [FK_ESCALAVALE_INESPECIA];


GO
ALTER TABLE [dbo].[ESCALAVALE] NOCHECK CONSTRAINT [FK_ESCALAVALE_INPACIENT];

