CREATE TABLE [dbo].[HCANESTQX] (
    [ID]                       INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDETIPHIS]                CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO]                CHAR (10)                                                                        NULL,
    [IPCODPACI]                VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]                CHAR (10)                                                                        NULL,
    [CODCENATE]                CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]                CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]                CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [CODSERIPS]                CHAR (20)                                                                        NULL,
    [FECREGSIS]                DATETIME                                                                         CONSTRAINT [DF_HCANESTQX_FECREGSIS] DEFAULT ([Common].[getdate]()) NOT NULL,
    [IDDESCRIPCIONRELACIONADA] INT                                                                              NULL,
    CONSTRAINT [PK_HCANESTQX] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCANESTQX_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCANESTQX_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCANESTQX_HCANESTQX] FOREIGN KEY ([ID]) REFERENCES [dbo].[HCANESTQX] ([ID]),
    CONSTRAINT [FK_HCANESTQX_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_HCANESTQX_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCANESTQX_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCANESTQX_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCANESTQX] NOCHECK CONSTRAINT [FK_HCANESTQX_ADCENATEN];


GO
ALTER TABLE [dbo].[HCANESTQX] NOCHECK CONSTRAINT [FK_HCANESTQX_ADINGRESO];


GO
ALTER TABLE [dbo].[HCANESTQX] NOCHECK CONSTRAINT [FK_HCANESTQX_HCANESTQX];


GO
ALTER TABLE [dbo].[HCANESTQX] NOCHECK CONSTRAINT [FK_HCANESTQX_INCUPSIPS];


GO
ALTER TABLE [dbo].[HCANESTQX] NOCHECK CONSTRAINT [FK_HCANESTQX_INPACIENT];


GO
ALTER TABLE [dbo].[HCANESTQX] NOCHECK CONSTRAINT [FK_HCANESTQX_INPROFSAL];


GO
ALTER TABLE [dbo].[HCANESTQX] NOCHECK CONSTRAINT [FK_HCANESTQX_INUNIFUNC];

