CREATE TABLE [dbo].[HCORDINTI] (
    [IDETIPHIS] CHAR (9)                                                                          NOT NULL,
    [NUMEFOLIO] NCHAR (10)                                                                        NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')  NOT NULL,
    [NUMINGRES] CHAR (10)                                                                         NOT NULL,
    [CODCENATE] CHAR (10)                                                                         NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                         NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')     NOT NULL,
    [FECORDMED] DATETIME                                                                          NOT NULL,
    [CODSERIPS] CHAR (20)                                                                         NOT NULL,
    [CANSERIPS] INT                                                                               NOT NULL,
    [OBSSERIPS] CHAR (250)                                                                        NULL,
    [PRISERIPS] CHAR (1)                                                                          NOT NULL,
    [ESTSERIPS] CHAR (1)                                                                          NULL,
    [MANEXTPRO] BIT                                                                               NOT NULL,
    [CODDIAGNO] CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')      NULL,
    [INDAUDFOR] NUMERIC (18)                                                                      NOT NULL,
    [CODESPECI] CHAR (3)                                                                          NULL,
    [REPORAUTO] BIT                                                                               NULL,
    [CODPROENV] CHAR (20)                                                                         NULL,
    [MSGPENENV] BIT                                                                               NULL,
    [INTERPRET] VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "Interpretation_Ofuscado", 0)') NULL,
    [CODPROINT] CHAR (20)                                                                         NULL,
    [NUMFOLINT] NCHAR (10)                                                                        NULL,
    [CODPRONOT] CHAR (20)                                                                         NULL,
    [MOVPRONOT] CHAR (15)                                                                         NULL,
    [FECREGNOT] DATETIME                                                                          NULL,
    CONSTRAINT [PK_HCORDINTI] PRIMARY KEY CLUSTERED ([IDETIPHIS] ASC, [NUMEFOLIO] ASC, [IPCODPACI] ASC, [NUMINGRES] ASC, [CODSERIPS] ASC, [MANEXTPRO] ASC),
    CONSTRAINT [FK_HCORDINTI_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCORDINTI_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCORDINTI_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_HCORDINTI_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_HCORDINTI_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCORDINTI_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCORDINTI_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCORDINTI] NOCHECK CONSTRAINT [FK_HCORDINTI_ADcenaten];


GO
ALTER TABLE [dbo].[HCORDINTI] NOCHECK CONSTRAINT [FK_HCORDINTI_ADINGRESO];


GO
ALTER TABLE [dbo].[HCORDINTI] NOCHECK CONSTRAINT [FK_HCORDINTI_INCUPSIPS];


GO
ALTER TABLE [dbo].[HCORDINTI] NOCHECK CONSTRAINT [FK_HCORDINTI_INDIAGNOS];


GO
ALTER TABLE [dbo].[HCORDINTI] NOCHECK CONSTRAINT [FK_HCORDINTI_INESPECIA];


GO
ALTER TABLE [dbo].[HCORDINTI] NOCHECK CONSTRAINT [FK_HCORDINTI_INPACIENT];


GO
ALTER TABLE [dbo].[HCORDINTI] NOCHECK CONSTRAINT [FK_HCORDINTI_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional Preferido al que se le envia el MSJ de Texto.    Si al momento de realizar el envio el profesional cambia, o ya no esta disponible, se notificara al de turno.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTI', @level2type = N'COLUMN', @level2name = N'CODPROENV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Servicio IPS  1: Solicitado  2: Solicitud Enviada  3: Interconsulta Realizada  4: Extramural  5: Anulado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTI', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prioridad del Servicio Solicitado  1: Urgente  2: Rutina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTI', @level2type = N'COLUMN', @level2name = N'PRISERIPS';

