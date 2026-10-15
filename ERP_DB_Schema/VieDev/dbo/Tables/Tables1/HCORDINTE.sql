CREATE TABLE [dbo].[HCORDINTE] (
    [IDETIPHIS]                     CHAR (9)                                                                          NOT NULL,
    [NUMEFOLIO]                     NCHAR (10)                                                                        NOT NULL,
    [IPCODPACI]                     VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')  NOT NULL,
    [NUMINGRES]                     CHAR (10)                                                                         NOT NULL,
    [CODCENATE]                     CHAR (10)                                                                         NOT NULL,
    [UFUCODIGO]                     CHAR (10)                                                                         NOT NULL,
    [CODPROSAL]                     CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')     NOT NULL,
    [FECORDMED]                     DATETIME                                                                          NOT NULL,
    [CODSERIPS]                     CHAR (20)                                                                         NOT NULL,
    [CANSERIPS]                     INT                                                                               NOT NULL,
    [OBSSERIPS]                     VARCHAR (2000)                                                                    NULL,
    [PRISERIPS]                     CHAR (1)                                                                          NOT NULL,
    [ESTSERIPS]                     CHAR (1)                                                                          NULL,
    [MANEXTPRO]                     BIT                                                                               NOT NULL,
    [CODDIAGNO]                     CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')      NULL,
    [INDAUDFOR]                     NUMERIC (18)                                                                      NOT NULL,
    [CODESPECI]                     CHAR (3)                                                                          NOT NULL,
    [REPORAUTO]                     BIT                                                                               NULL,
    [CODPROENV]                     CHAR (20)                                                                         NULL,
    [MSGPENENV]                     BIT                                                                               NULL,
    [INTERPRET]                     VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "Interpretation_Ofuscado", 0)') NULL,
    [CODPROINT]                     CHAR (20)                                                                         NULL,
    [NUMFOLINT]                     NCHAR (10)                                                                        NULL,
    [CODPRONOT]                     CHAR (20)                                                                         NULL,
    [MOVPRONOT]                     CHAR (15)                                                                         NULL,
    [FECREGNOT]                     DATETIME                                                                          NULL,
    [AUTO]                          INT                                                                               IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [GENSERVICEORDER]               INT                                                                               NULL,
    [TIPOFACTURACION]               INT                                                                               NULL,
    [IDRIASCUPS]                    INT                                                                               NULL,
    [IDDESCRIPCIONRELACIONADA]      INT                                                                               NULL,
    [TraceabilityPaperworkEventsId] INT                                                                               NULL,
    [TraceabilityPaperworkId]       INT                                                                               NULL,
    [FECHAINT]                      DATETIME                                                                          NULL,
    [NUMFOLIORESIDENTE]             NCHAR (10)                                                                        NULL,
    CONSTRAINT [PK_HCORDINTE] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_HCORDINTE_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCORDINTE_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCORDINTE_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_HCORDINTE_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_HCORDINTE_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCORDINTE_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCORDINTE_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCORDINTE_INPROFSAL1] FOREIGN KEY ([CODPROENV]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCORDINTE_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCORDINTE_RIASCUPS] FOREIGN KEY ([IDRIASCUPS]) REFERENCES [dbo].[RIASCUPS] ([ID])
);


GO
ALTER TABLE [dbo].[HCORDINTE] NOCHECK CONSTRAINT [FK_HCORDINTE_ADcenaten];


GO
ALTER TABLE [dbo].[HCORDINTE] NOCHECK CONSTRAINT [FK_HCORDINTE_ADINGRESO];


GO
ALTER TABLE [dbo].[HCORDINTE] NOCHECK CONSTRAINT [FK_HCORDINTE_INCUPSIPS];


GO
ALTER TABLE [dbo].[HCORDINTE] NOCHECK CONSTRAINT [FK_HCORDINTE_INDIAGNOS];


GO
ALTER TABLE [dbo].[HCORDINTE] NOCHECK CONSTRAINT [FK_HCORDINTE_INESPECIA];


GO
ALTER TABLE [dbo].[HCORDINTE] NOCHECK CONSTRAINT [FK_HCORDINTE_INPacient];


GO
ALTER TABLE [dbo].[HCORDINTE] NOCHECK CONSTRAINT [FK_HCORDINTE_INPROFSAL];


GO
ALTER TABLE [dbo].[HCORDINTE] NOCHECK CONSTRAINT [FK_HCORDINTE_INPROFSAL1];


GO
ALTER TABLE [dbo].[HCORDINTE] NOCHECK CONSTRAINT [FK_HCORDINTE_INUNIFUNC];


GO
ALTER TABLE [dbo].[HCORDINTE] NOCHECK CONSTRAINT [FK_HCORDINTE_RIASCUPS];


GO
CREATE NONCLUSTERED INDEX [IX_HCORDINTE_CODCENATE_CODESPECI_ESTSERIPS_CODPROSAL_FECORDMED_IDETIPHIS_IPCODPACI_NUMFOLIORESIDENTE_NUMINGRES]
    ON [dbo].[HCORDINTE]([CODCENATE] ASC, [CODESPECI] ASC, [ESTSERIPS] ASC)
    INCLUDE([CODPROSAL], [FECORDMED], [IDETIPHIS], [IPCODPACI], [NUMFOLIORESIDENTE], [NUMINGRES]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Folio en la cual el medico residente asistio a una interconsulta del especialista y debe ser validada por el medico especialista.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'NUMFOLIORESIDENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuando campo TIPO FACTURACION sea 1 guardamos  el IdRiasCups a la que aplica de lo contrario queda NULL  Relacion con la tabla RIASCUPS.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'IDRIASCUPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'RIAS - Determina como se debe facturar el servicio:  1 - Rias  2 - Consulta Externa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'TIPOFACTURACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional Preferido al que se le envia el MSJ de Texto.    Si al momento de realizar el envio el profesional cambia, o ya no esta disponible, se notificara al de turno.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'CODPROENV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Servicio IPS  1: Solicitado  2: Solicitud Enviada  3: Interconsulta Realizada  4: Extramural  5: Anulado  6: Pendiente de verificación por parte del especialista, esto por el tema de los residentes.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prioridad del Servicio Solicitado  1: Urgente  2: Rutina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDINTE', @level2type = N'COLUMN', @level2name = N'PRISERIPS';

