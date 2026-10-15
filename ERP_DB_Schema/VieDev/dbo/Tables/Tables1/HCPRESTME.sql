CREATE TABLE [dbo].[HCPRESTME] (
    [NUMCONSEC]  NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FECREGPRE]  DATETIME                                                                         NOT NULL,
    [IPCODPACI]  VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]  CHAR (10)                                                                        NOT NULL,
    [CODPRODUC]  CHAR (20)                                                                        NOT NULL,
    [CANPRODUCT] INT                                                                              NOT NULL,
    [CANPROPEN]  INT                                                                              NOT NULL,
    [IPCODPACP]  VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGREP]  CHAR (10)                                                                        NOT NULL,
    [CODCENATE]  CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]  CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]  CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [PREANULAD]  BIT                                                                              NOT NULL,
    [FECANUPRE]  DATETIME                                                                         NULL,
    [CODPROANU]  CHAR (20)                                                                        NULL,
    CONSTRAINT [PK_HCPRESTME] PRIMARY KEY CLUSTERED ([NUMCONSEC] ASC),
    CONSTRAINT [FK_HCPRESTME_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCPRESTME_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCPRESTME_ADINGRESO1] FOREIGN KEY ([NUMINGREP]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCPRESTME_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC]),
    CONSTRAINT [FK_HCPRESTME_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCPRESTME_INPACIENT1] FOREIGN KEY ([IPCODPACP]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCPRESTME_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCPRESTME_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCPRESTME] NOCHECK CONSTRAINT [FK_HCPRESTME_ADCENATEN];


GO
ALTER TABLE [dbo].[HCPRESTME] NOCHECK CONSTRAINT [FK_HCPRESTME_ADINGRESO];


GO
ALTER TABLE [dbo].[HCPRESTME] NOCHECK CONSTRAINT [FK_HCPRESTME_ADINGRESO1];


GO
ALTER TABLE [dbo].[HCPRESTME] NOCHECK CONSTRAINT [FK_HCPRESTME_IHLISTPRO];


GO
ALTER TABLE [dbo].[HCPRESTME] NOCHECK CONSTRAINT [FK_HCPRESTME_INPACIENT];


GO
ALTER TABLE [dbo].[HCPRESTME] NOCHECK CONSTRAINT [FK_HCPRESTME_INPACIENT1];


GO
ALTER TABLE [dbo].[HCPRESTME] NOCHECK CONSTRAINT [FK_HCPRESTME_INPROFSAL];


GO
ALTER TABLE [dbo].[HCPRESTME] NOCHECK CONSTRAINT [FK_HCPRESTME_INUNIFUNC];

