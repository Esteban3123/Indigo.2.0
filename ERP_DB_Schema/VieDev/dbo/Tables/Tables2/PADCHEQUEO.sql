CREATE TABLE [dbo].[PADCHEQUEO] (
    [ID]              INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NUMINGRES]       CHAR (10)                                                                        NOT NULL,
    [IPCODPACI]       VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [IDLISTACHEQUEO]  INT                                                                              NOT NULL,
    [IDLISTACHEQUEOD] INT                                                                              NOT NULL,
    [CHEQUEADO]       BIT                                                                              NOT NULL,
    [CAMPOTEXTO]      VARCHAR (500)                                                                    NULL,
    [CAMPOFECHA]      DATETIME                                                                         NULL,
    [URLADJUNTO]      VARCHAR (100)                                                                    NULL,
    CONSTRAINT [PK_PADCHEQUEO] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PADCHEQUEO_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_PADCHEQUEO_HCLISTACC] FOREIGN KEY ([IDLISTACHEQUEO]) REFERENCES [dbo].[HCLISTACC] ([CODCONSEC]),
    CONSTRAINT [FK_PADCHEQUEO_HCLISTACD] FOREIGN KEY ([IDLISTACHEQUEOD]) REFERENCES [dbo].[HCLISTACD] ([CODCONSEC]),
    CONSTRAINT [FK_PADCHEQUEO_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[PADCHEQUEO] NOCHECK CONSTRAINT [FK_PADCHEQUEO_ADINGRESO];


GO
ALTER TABLE [dbo].[PADCHEQUEO] NOCHECK CONSTRAINT [FK_PADCHEQUEO_HCLISTACC];


GO
ALTER TABLE [dbo].[PADCHEQUEO] NOCHECK CONSTRAINT [FK_PADCHEQUEO_HCLISTACD];


GO
ALTER TABLE [dbo].[PADCHEQUEO] NOCHECK CONSTRAINT [FK_PADCHEQUEO_INPACIENT];

