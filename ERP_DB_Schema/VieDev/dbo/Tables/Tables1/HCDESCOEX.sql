CREATE TABLE [dbo].[HCDESCOEX] (
    [AUTO]                          NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NUMEFOLIO]                     NCHAR (10)                                                                       NOT NULL,
    [IPCODPACI]                     VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]                     CHAR (10)                                                                        NOT NULL,
    [CODCENATE]                     CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]                     CHAR (10)                                                                        NOT NULL,
    [NUMCONTRL]                     INT                                                                              NOT NULL,
    [UNICONTRL]                     CHAR (1)                                                                         NOT NULL,
    [CODESPECI]                     CHAR (3)                                                                         NULL,
    [CODSERIPS]                     CHAR (20)                                                                        NULL,
    [TraceabilityPaperworkEventsId] INT                                                                              NULL,
    [TraceabilityPaperworkId]       INT                                                                              NULL,
    [IDDESCRIPCIONRELACIONADA]      INT                                                                              NULL,
    CONSTRAINT [PK_HCDESCOEX] PRIMARY KEY CLUSTERED ([AUTO] ASC, [NUMEFOLIO] ASC),
    CONSTRAINT [FK_HCDESCOEX_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCDESCOEX_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCDESCOEX_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_HCDESCOEX_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCDESCOEX_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCDESCOEX_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCDESCOEX] NOCHECK CONSTRAINT [FK_HCDESCOEX_ADCENATEN];


GO
ALTER TABLE [dbo].[HCDESCOEX] NOCHECK CONSTRAINT [FK_HCDESCOEX_ADINGRESO];


GO
ALTER TABLE [dbo].[HCDESCOEX] NOCHECK CONSTRAINT [FK_HCDESCOEX_INCUPSIPS];


GO
ALTER TABLE [dbo].[HCDESCOEX] NOCHECK CONSTRAINT [FK_HCDESCOEX_INESPECIA];


GO
ALTER TABLE [dbo].[HCDESCOEX] NOCHECK CONSTRAINT [FK_HCDESCOEX_INPACIENT];


GO
ALTER TABLE [dbo].[HCDESCOEX] NOCHECK CONSTRAINT [FK_HCDESCOEX_INUNIFUNC];


GO
CREATE NONCLUSTERED INDEX [IX_HCDESCOEX_NUMEFOLIO_IPCODPACI_NUMINGRES]
    ON [dbo].[HCDESCOEX]([NUMEFOLIO] ASC, [IPCODPACI] ASC, [NUMINGRES] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la ID de la Descripción relacionada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDESCOEX', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de dias o meses para el proximo control  1: Dias  2: Meses', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDESCOEX', @level2type = N'COLUMN', @level2name = N'UNICONTRL';

