CREATE TABLE [dbo].[HCORDPARAMIMA] (
    [ID]          INT                                                                                IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDETIPHIS]   CHAR (9)                                                                           NOT NULL,
    [NUMEFOLIO]   NCHAR (10)                                                                         NOT NULL,
    [IPCODPACI]   VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')   NOT NULL,
    [NUMINGRES]   CHAR (10)                                                                          NOT NULL,
    [CODCENATE]   CHAR (10)                                                                          NOT NULL,
    [UFUCODIGO]   CHAR (10)                                                                          NOT NULL,
    [CODPROSAL]   CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')      NOT NULL,
    [FECORDMED]   DATETIME                                                                           NOT NULL,
    [CODSERIPS]   CHAR (20)                                                                          NOT NULL,
    [CANSERIPS]   INT                                                                                NOT NULL,
    [HCPARAMBID]  INT                                                                                NOT NULL,
    [INTERPRET]   VARCHAR (4000) MASKED WITH (FUNCTION = 'partial(0, "Interpretation_Ofuscado", 0)') NULL,
    [RESULTADO]   VARCHAR (4000) MASKED WITH (FUNCTION = 'partial(0, "Result_Ofuscado", 0)')         NULL,
    [RESULTADOID] INT                                                                                NULL,
    CONSTRAINT [PK_HCORDPARAMIMA] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCORDPARAMIMA_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCORDPARAMIMA_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCORDPARAMIMA_HCPARAMB] FOREIGN KEY ([HCPARAMBID]) REFERENCES [dbo].[HCPARAMB] ([ID]),
    CONSTRAINT [FK_HCORDPARAMIMA_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_HCORDPARAMIMA_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCORDPARAMIMA_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCORDPARAMIMA_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCORDPARAMIMA] NOCHECK CONSTRAINT [FK_HCORDPARAMIMA_ADcenaten];


GO
ALTER TABLE [dbo].[HCORDPARAMIMA] NOCHECK CONSTRAINT [FK_HCORDPARAMIMA_ADINGRESO];


GO
ALTER TABLE [dbo].[HCORDPARAMIMA] NOCHECK CONSTRAINT [FK_HCORDPARAMIMA_HCPARAMB];


GO
ALTER TABLE [dbo].[HCORDPARAMIMA] NOCHECK CONSTRAINT [FK_HCORDPARAMIMA_INCUPSIPS];


GO
ALTER TABLE [dbo].[HCORDPARAMIMA] NOCHECK CONSTRAINT [FK_HCORDPARAMIMA_INPACIENT];


GO
ALTER TABLE [dbo].[HCORDPARAMIMA] NOCHECK CONSTRAINT [FK_HCORDPARAMIMA_INPROFSAL];


GO
ALTER TABLE [dbo].[HCORDPARAMIMA] NOCHECK CONSTRAINT [FK_HCORDPARAMIMA_INUNIFUNC];

