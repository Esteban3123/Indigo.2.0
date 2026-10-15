CREATE TABLE [dbo].[AGAGEMEDC] (
    [CODAUTONU]             INT                                                                           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FECHORAIN]             DATETIME                                                                      NOT NULL,
    [FECHORAFI]             DATETIME                                                                      NOT NULL,
    [CODPROSAL]             CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NULL,
    [CODCENATE]             CHAR (10)                                                                     NOT NULL,
    [TIPAGEMED]             CHAR (1)                                                                      NOT NULL,
    [CODIGOCON]             CHAR (6)                                                                      NULL,
    [CODESPECI]             CHAR (3)                                                                      NULL,
    [CODUSUASI]             CHAR (20)                                                                     NOT NULL,
    [FECREGSIS]             DATETIME                                                                      NOT NULL,
    [AGENSALAC]             INT                                                                           NULL,
    [CODIGOUBIC]            CHAR (6)                                                                      NULL,
    [TURNOEMER]             BIT                                                                           NULL,
    [TURNOBLOQ]             BIT                                                                           NULL,
    [IDAGENDA]              INT                                                                           NULL,
    [DISPONIBILIDAD]        BIT                                                                           NULL,
    [IDPADREDISPONIBILIDAD] INT                                                                           NULL,
    [JORNADA]               INT                                                                           NULL,
    [MOSTRARWEB]            BIT                                                                           NULL,
    [IdMassiveAgenda]       UNIQUEIDENTIFIER                                                              NULL,
    [ProgrammingSummary]    VARCHAR (1000)                                                                NULL,
    [IndigoSyncId]          VARCHAR (36)                                                                  NULL,
    [BlockingReason]        INT                                                                           NULL,
    [Observation]           VARCHAR (500)                                                                 NULL,
    CONSTRAINT [PK_AGAGEMEDC] PRIMARY KEY CLUSTERED ([CODAUTONU] ASC),
    CONSTRAINT [FK_AGAGEMEDC_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_AGAGEMEDC_AGCONSULT] FOREIGN KEY ([CODIGOCON], [CODCENATE]) REFERENCES [dbo].[AGCONSULT] ([CODIGOCON], [CODCENATE]),
    CONSTRAINT [FK_AGAGEMEDC_AGENSALAC] FOREIGN KEY ([AGENSALAC]) REFERENCES [dbo].[AGENSALAC] ([CODCONCEC]),
    CONSTRAINT [FK_AGAGEMEDC_IDAGENDA] FOREIGN KEY ([IDAGENDA]) REFERENCES [dbo].[AGAGEMEDC] ([CODAUTONU]),
    CONSTRAINT [FK_AGAGEMEDC_IDDISPONIBILIDAD] FOREIGN KEY ([IDPADREDISPONIBILIDAD]) REFERENCES [dbo].[AGAGEMEDC] ([CODAUTONU]),
    CONSTRAINT [FK_AGAGEMEDC_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_AGAGEMEDC_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
ALTER TABLE [dbo].[AGAGEMEDC] NOCHECK CONSTRAINT [FK_AGAGEMEDC_ADCENATEN];


GO
ALTER TABLE [dbo].[AGAGEMEDC] NOCHECK CONSTRAINT [FK_AGAGEMEDC_AGCONSULT];


GO
ALTER TABLE [dbo].[AGAGEMEDC] NOCHECK CONSTRAINT [FK_AGAGEMEDC_AGENSALAC];


GO
ALTER TABLE [dbo].[AGAGEMEDC] NOCHECK CONSTRAINT [FK_AGAGEMEDC_IDAGENDA];


GO
ALTER TABLE [dbo].[AGAGEMEDC] NOCHECK CONSTRAINT [FK_AGAGEMEDC_IDDISPONIBILIDAD];


GO
ALTER TABLE [dbo].[AGAGEMEDC] NOCHECK CONSTRAINT [FK_AGAGEMEDC_INESPECIA];


GO
ALTER TABLE [dbo].[AGAGEMEDC] NOCHECK CONSTRAINT [FK_AGAGEMEDC_INPROFSAL];


GO
CREATE NONCLUSTERED INDEX [idx_IdMassiveAgenda_AGAGEMEDC]
    ON [dbo].[AGAGEMEDC]([IdMassiveAgenda] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AGAGEMEDC_CODPROSAL_FECHORAIN_FECHORAFI_CODAUTONU]
    ON [dbo].[AGAGEMEDC]([CODPROSAL] ASC, [FECHORAIN] ASC, [FECHORAFI] ASC)
    INCLUDE([CODAUTONU]);


GO
CREATE NONCLUSTERED INDEX [IX_AGAGEMEDC_CODPROSAL_CODCENATE_FECHORAIN_FECHORAFI_CODAUTONU_TIPAGEMED]
    ON [dbo].[AGAGEMEDC]([CODPROSAL] ASC, [CODCENATE] ASC, [FECHORAIN] ASC, [FECHORAFI] ASC)
    INCLUDE([CODAUTONU], [TIPAGEMED]);


GO
CREATE NONCLUSTERED INDEX [IX_AGAGEMEDC_CODCENATE_FECHORAIN_FECHORAFI_CODAUTONU_CODPROSAL_TIPAGEMED]
    ON [dbo].[AGAGEMEDC]([CODCENATE] ASC, [FECHORAIN] ASC, [FECHORAFI] ASC)
    INCLUDE([CODAUTONU], [CODPROSAL], [TIPAGEMED]);


GO
CREATE NONCLUSTERED INDEX [IX_AGAGEMEDC_CODCENATE_CODIGOCON_CODAUTONU_CODPROSAL_FECHORAFI_FECHORAIN]
    ON [dbo].[AGAGEMEDC]([CODCENATE] ASC, [CODIGOCON] ASC)
    INCLUDE([CODAUTONU], [CODPROSAL], [FECHORAFI], [FECHORAIN]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la observacion del bloqueo de la agenda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contiene el codigo del motivo de bloque de la agenda, correspondiente a la columna CODMOTANU de la tabla HCMOANULB', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'BlockingReason';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo para guardar el resumen de la programación cuando la agenda se graba masivamente (PBI 16723 -> Guardar desde formulario agenda en serie)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'ProgrammingSummary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo para guardar el identificador de la agenda masiva (PBI 16723 -> Guardar desde formulario agenda en serie)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'IdMassiveAgenda';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mostrar Agenda medica En Web  1 = Si   0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'MOSTRARWEB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la jornada de la agenda medica :   1 - Jornada Mañana  2 - Jornada Tarde  3 - Jornada Nocturna', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'JORNADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Proceso de cirugías , Id de la agenda medica base para adicionar disponibilidad a un medico y asi poder programar cirugia    Relacion con la misma tabla (AGAGEMEDC - CODAUTONU)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'IDPADREDISPONIBILIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla AGAGEMEDC que relaciona la disponibilidad de la sala   con la agenda del profesional para cirugías.  Solo se guarda para Agenda medica de Quirofanos (TIPAGEMED = 0 Profesional)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'IDAGENDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Agendamiento   0:Profesional   1:Programación Quirofano 2:Apoyo Diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'TIPAGEMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud  -----------------------------------------    CAMPO OBSOLETO -> PBI 13603', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';

