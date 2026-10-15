CREATE TABLE [dbo].[HCCXANULA] (
    [AUTO]        INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]   VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]   CHAR (10)                                                                        NOT NULL,
    [CODMOTANU]   CHAR (3)                                                                         NOT NULL,
    [CRIJUSTIF]   VARCHAR (500)                                                                    NOT NULL,
    [CODPROSAL]   CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECHDILIG]   DATETIME                                                                         NOT NULL,
    [FECREGSIS]   DATETIME                                                                         NOT NULL,
    [NUMEFOLIO]   CHAR (10)                                                                        NULL,
    [IDHCORDPROQ] INT                                                                              NULL,
    CONSTRAINT [PK_HCCXANULA] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_HCCXANULA_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCCXANULA_HCCXANULA] FOREIGN KEY ([AUTO]) REFERENCES [dbo].[HCCXANULA] ([AUTO]),
    CONSTRAINT [FK_HCCXANULA_HCCXANULA1] FOREIGN KEY ([AUTO]) REFERENCES [dbo].[HCCXANULA] ([AUTO]),
    CONSTRAINT [FK_HCCXANULA_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCCXANULA_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
ALTER TABLE [dbo].[HCCXANULA] NOCHECK CONSTRAINT [FK_HCCXANULA_ADINGRESO];


GO
ALTER TABLE [dbo].[HCCXANULA] NOCHECK CONSTRAINT [FK_HCCXANULA_HCCXANULA];


GO
ALTER TABLE [dbo].[HCCXANULA] NOCHECK CONSTRAINT [FK_HCCXANULA_HCCXANULA1];


GO
ALTER TABLE [dbo].[HCCXANULA] NOCHECK CONSTRAINT [FK_HCCXANULA_INPACIENT];


GO
ALTER TABLE [dbo].[HCCXANULA] NOCHECK CONSTRAINT [FK_HCCXANULA_INPROFSAL];

