CREATE TABLE [dbo].[HCORDLABI] (
    [AUTO]      INT                                                                                IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDETIPHIS] CHAR (9)                                                                           NOT NULL,
    [NUMEFOLIO] NCHAR (10)                                                                         NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')   NOT NULL,
    [NUMINGRES] CHAR (10)                                                                          NOT NULL,
    [CODCENATE] CHAR (10)                                                                          NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                          NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')      NOT NULL,
    [FECORDMED] DATETIME                                                                           NOT NULL,
    [CODSERIPS] CHAR (20)                                                                          NOT NULL,
    [CANSERIPS] INT                                                                                NOT NULL,
    [OBSSERIPS] CHAR (250)                                                                         NULL,
    [PRISERIPS] CHAR (1)                                                                           NOT NULL,
    [ESTSERIPS] CHAR (1)                                                                           NULL,
    [MANEXTPRO] BIT                                                                                NOT NULL,
    [CODDIAGNO] CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')       NULL,
    [INTERPRET] VARCHAR (4000) MASKED WITH (FUNCTION = 'partial(0, "Interpretation_Ofuscado", 0)') NULL,
    [CODPROINT] CHAR (20)                                                                          NULL,
    [NUMFOLINT] NCHAR (10)                                                                         NULL,
    [SERREAINT] BIT                                                                                NOT NULL,
    [INDAUDFOR] NUMERIC (18)                                                                       NOT NULL,
    [ESTALELAB] BIT                                                                                NOT NULL,
    [FECRECMUE] DATETIME                                                                           NULL,
    [NOMARCLAB] CHAR (250)                                                                         NULL,
    [CONCURRE]  ROWVERSION                                                                         NULL,
    [USURECMUE] CHAR (20)                                                                          NULL,
    CONSTRAINT [PK_HCORDLABI] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_HCORDLABI_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCORDLABI_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCORDLABI_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_HCORDLABI_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_HCORDLABI_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCORDLABI_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [IX_HCORDLABI_1] UNIQUE NONCLUSTERED ([IDETIPHIS] ASC, [NUMEFOLIO] ASC, [IPCODPACI] ASC, [NUMINGRES] ASC, [CODSERIPS] ASC, [MANEXTPRO] ASC)
);


GO
ALTER TABLE [dbo].[HCORDLABI] NOCHECK CONSTRAINT [FK_HCORDLABI_ADcenaten];


GO
ALTER TABLE [dbo].[HCORDLABI] NOCHECK CONSTRAINT [FK_HCORDLABI_ADINGRESO];


GO
ALTER TABLE [dbo].[HCORDLABI] NOCHECK CONSTRAINT [FK_HCORDLABI_INCUPSIPS];


GO
ALTER TABLE [dbo].[HCORDLABI] NOCHECK CONSTRAINT [FK_HCORDLABI_INDIAGNOS];


GO
ALTER TABLE [dbo].[HCORDLABI] NOCHECK CONSTRAINT [FK_HCORDLABI_INPACIENT];


GO
ALTER TABLE [dbo].[HCORDLABI] NOCHECK CONSTRAINT [FK_HCORDLABI_INUNIFUNC];


GO
CREATE NONCLUSTERED INDEX [IX_HCORDLABI__IPCODPACI__NUMINGRES__NUMEFOLIO__AUTO]
    ON [dbo].[HCORDLABI]([IPCODPACI] ASC, [NUMINGRES] ASC, [NUMEFOLIO] ASC, [AUTO] ASC)
    INCLUDE([INTERPRET]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Servicio IPS  1: Solicitado  2: Muestra Recolectada  3: Resultado Entregado  4: Examen Interpretado  5: Remitido  6: Anulado  7: Extramural  8:Muestra Recolectada Parcialmente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prioridad del Servicio Solicitado  1: Urgente  2: Rutina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABI', @level2type = N'COLUMN', @level2name = N'PRISERIPS';

