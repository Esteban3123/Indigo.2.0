CREATE TABLE [dbo].[HCCONSENP] (
    [CODCONCEC] CHAR (10)                                                                        NOT NULL,
    [IDETIPHIS] CHAR (9)                                                                         NULL,
    [NUMEFOLIO] CHAR (10)                                                                        NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                        NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECCONINF] DATETIME                                                                         NOT NULL,
    [AUTCONINF] CHAR (80)                                                                        NULL,
    [CODSERIPS] CHAR (20)                                                                        NOT NULL,
    [OTRSERIPS] CHAR (100)                                                                       NULL,
    [TIPPROIPS] CHAR (1)                                                                         NOT NULL,
    [TIPANEPRO] CHAR (1)                                                                         NOT NULL,
    [ASPPROIPS] VARCHAR (4000)                                                                   NULL,
    [RIEPROIPS] VARCHAR (4000)                                                                   NULL,
    [DETPROIPS] VARCHAR (4000)                                                                   NULL,
    CONSTRAINT [PK_HCCONSENP] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC),
    CONSTRAINT [FK_HCCONSENP_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCCONSENP_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCCONSENP_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_HCCONSENP_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCCONSENP_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCCONSENP_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCCONSENP] NOCHECK CONSTRAINT [FK_HCCONSENP_ADcenaten];


GO
ALTER TABLE [dbo].[HCCONSENP] NOCHECK CONSTRAINT [FK_HCCONSENP_ADINGRESO];


GO
ALTER TABLE [dbo].[HCCONSENP] NOCHECK CONSTRAINT [FK_HCCONSENP_INCUPSIPS];


GO
ALTER TABLE [dbo].[HCCONSENP] NOCHECK CONSTRAINT [FK_HCCONSENP_INPacient];


GO
ALTER TABLE [dbo].[HCCONSENP] NOCHECK CONSTRAINT [FK_HCCONSENP_INPROFSAL];


GO
ALTER TABLE [dbo].[HCCONSENP] NOCHECK CONSTRAINT [FK_HCCONSENP_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Anestesia utilizada en el procedimiento  1: General  2: Local  3: Regional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONSENP', @level2type = N'COLUMN', @level2name = N'TIPANEPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Procedimiento IPS  1: Procedimiento Especial  2: Procedimiento Quirurgico  3: Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONSENP', @level2type = N'COLUMN', @level2name = N'TIPPROIPS';

