CREATE TABLE [dbo].[HCORDHEMO] (
    [IDETIPHIS] CHAR (9)                                                                           NOT NULL,
    [NUMEFOLIO] NCHAR (10)                                                                         NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')   NOT NULL,
    [NUMINGRES] CHAR (10)                                                                          NOT NULL,
    [CODCENATE] CHAR (10)                                                                          NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                          NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')      NOT NULL,
    [FECORDMED] DATETIME                                                                           NOT NULL,
    [CODPRODUC] TINYINT                                                                            NOT NULL,
    [CANSERIPS] INT                                                                                NOT NULL,
    [OBSSERIPS] CHAR (250)                                                                         NULL,
    [PRISERIPS] CHAR (1)                                                                           NOT NULL,
    [ESTSERIPS] CHAR (1)                                                                           NULL,
    [MANEXTPRO] BIT                                                                                NOT NULL,
    [CODDIAGNO] CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')       NOT NULL,
    [INTERPRET] VARCHAR (4000) MASKED WITH (FUNCTION = 'partial(0, "Interpretation_Ofuscado", 0)') NULL,
    [CODPROINT] CHAR (20)                                                                          NULL,
    [NUMFOLINT] NCHAR (10)                                                                         NULL,
    [INDAUDFOR] NUMERIC (18)                                                                       NOT NULL,
    [ESTALEHEM] BIT                                                                                CONSTRAINT [DF_HCORDHEMO_ESTALELAB] DEFAULT ((1)) NOT NULL,
    [FECRECHEM] DATETIME                                                                           NULL,
    [NOMARCHEM] CHAR (250)                                                                         NULL,
    [CONCURRE]  ROWVERSION                                                                         NULL,
    [USURECEXA] CHAR (20)                                                                          NULL,
    [INTERFAZ]  BIT                                                                                NULL,
    CONSTRAINT [PK_HCORDHEMO] PRIMARY KEY CLUSTERED ([IDETIPHIS] ASC, [NUMEFOLIO] ASC, [IPCODPACI] ASC, [NUMINGRES] ASC, [CODPRODUC] ASC, [MANEXTPRO] ASC),
    CONSTRAINT [FK_HCORDHEMO_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCORDHEMO_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCORDHEMO_HCHEMPROD] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[HCHEMPROD] ([CODPRODUC]),
    CONSTRAINT [FK_HCORDHEMO_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_HCORDHEMO_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCORDHEMO_INPROSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCORDHEMO_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCORDHEMO] NOCHECK CONSTRAINT [FK_HCORDHEMO_ADCENATEN];


GO
ALTER TABLE [dbo].[HCORDHEMO] NOCHECK CONSTRAINT [FK_HCORDHEMO_ADINGRESO];


GO
ALTER TABLE [dbo].[HCORDHEMO] NOCHECK CONSTRAINT [FK_HCORDHEMO_HCHEMPROD];


GO
ALTER TABLE [dbo].[HCORDHEMO] NOCHECK CONSTRAINT [FK_HCORDHEMO_INDIAGNOS];


GO
ALTER TABLE [dbo].[HCORDHEMO] NOCHECK CONSTRAINT [FK_HCORDHEMO_INPACIENT];


GO
ALTER TABLE [dbo].[HCORDHEMO] NOCHECK CONSTRAINT [FK_HCORDHEMO_INPROSAL];


GO
ALTER TABLE [dbo].[HCORDHEMO] NOCHECK CONSTRAINT [FK_HCORDHEMO_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina si la orden se envio o no a la interfaz.  1 - Si envio interfaz  0 - No envio interfaz', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'INTERFAZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Servicio IPS  1: Ordenado  2: Completado  3: Interpretado  4: Sin Interfaz', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prioridad del Servicio Solicitado  1: Urgente  2: Rutina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDHEMO', @level2type = N'COLUMN', @level2name = N'PRISERIPS';

