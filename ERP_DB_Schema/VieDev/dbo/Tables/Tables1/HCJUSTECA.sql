CREATE TABLE [dbo].[HCJUSTECA] (
    [CODCONCEC]                INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDETIPHIS]                CHAR (9)                                                                         NULL,
    [NUMEFOLIO]                NCHAR (10)                                                                       NULL,
    [IPCODPACI]                VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]                CHAR (10)                                                                        NOT NULL,
    [CODCENATE]                CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]                CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]                CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECORDMED]                DATETIME                                                                         NOT NULL,
    [CODSERIPS]                CHAR (20)                                                                        NOT NULL,
    [DESAYUDIA]                VARCHAR (MAX)                                                                    NOT NULL,
    [DESJUSTEC]                VARCHAR (MAX)                                                                    NOT NULL,
    [DESINCAYU]                VARCHAR (MAX)                                                                    NOT NULL,
    [CODMINSALUD]              VARCHAR (30)                                                                     NULL,
    [IDDESCRIPCIONRELACIONADA] INT                                                                              NULL,
    [PRESCRIPCIONJSON]         VARCHAR (8000)                                                                   NULL,
    [CodCUPSMipres]            VARCHAR (262)                                                                    NULL,
    [ORDUNICA]                 BIT                                                                              NULL,
    CONSTRAINT [PK_HCJUSTECA] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC),
    CONSTRAINT [FK_HCJUSTECA_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCJUSTECA_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCJUSTECA_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_HCJUSTECA_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCJUSTECA_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCJUSTECA_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCJUSTECA] NOCHECK CONSTRAINT [FK_HCJUSTECA_ADCENATEN];


GO
ALTER TABLE [dbo].[HCJUSTECA] NOCHECK CONSTRAINT [FK_HCJUSTECA_ADINGRESO];


GO
ALTER TABLE [dbo].[HCJUSTECA] NOCHECK CONSTRAINT [FK_HCJUSTECA_INCUPSIPS];


GO
ALTER TABLE [dbo].[HCJUSTECA] NOCHECK CONSTRAINT [FK_HCJUSTECA_INPACIENT];


GO
ALTER TABLE [dbo].[HCJUSTECA] NOCHECK CONSTRAINT [FK_HCJUSTECA_INPROFSAL];


GO
ALTER TABLE [dbo].[HCJUSTECA] NOCHECK CONSTRAINT [FK_HCJUSTECA_INUNIFUNC];

