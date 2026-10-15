CREATE TABLE [dbo].[HCURINTER] (
    [AUTO]       INT                                                                               IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]  VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')  NOT NULL,
    [NUMINGRES]  CHAR (10)                                                                         NOT NULL,
    [CODCENATE]  CHAR (10)                                                                         NOT NULL,
    [UFUCODIGO]  CHAR (10)                                                                         NOT NULL,
    [FECINIATE]  DATETIME                                                                          NOT NULL,
    [CODPROSAL]  CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')     NOT NULL,
    [MOTCONSUL]  VARCHAR (MAX)                                                                     NOT NULL,
    [ENFACTUAL]  VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "CurrentIllness_Ofuscado", 0)') NOT NULL,
    [REVSISTEMA] VARCHAR (MAX)                                                                     NOT NULL,
    [ANALISISP]  VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')       NOT NULL,
    [ORDENMED]   VARCHAR (MAX)                                                                     NOT NULL,
    CONSTRAINT [PK_HCURINTER] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_HCURINTER_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCURINTER_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCURINTER_HCURINTER] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCURINTER_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCURINTER_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [IX_HCURINTER] UNIQUE NONCLUSTERED ([IPCODPACI] ASC, [NUMINGRES] ASC)
);


GO
ALTER TABLE [dbo].[HCURINTER] NOCHECK CONSTRAINT [FK_HCURINTER_ADCENATEN];


GO
ALTER TABLE [dbo].[HCURINTER] NOCHECK CONSTRAINT [FK_HCURINTER_ADINGRESO];


GO
ALTER TABLE [dbo].[HCURINTER] NOCHECK CONSTRAINT [FK_HCURINTER_HCURINTER];


GO
ALTER TABLE [dbo].[HCURINTER] NOCHECK CONSTRAINT [FK_HCURINTER_INPROFSAL];


GO
ALTER TABLE [dbo].[HCURINTER] NOCHECK CONSTRAINT [FK_HCURINTER_INUNIFUNC];

