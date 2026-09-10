CREATE TABLE [dbo].[HCORHEMBOL] (
    [ID]                            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HCORHEMCOID]                   INT             NOT NULL,
    [COMSAMID]                      INT             NOT NULL,
    [ESTADO]                        TINYINT         NOT NULL,
    [TIPOSOLICI]                    TINYINT         NOT NULL,
    [PENCONRES]                     BIT             NOT NULL,
    [AUTENTREGA]                    BIT             NOT NULL,
    [ESDEVOLUTIVO]                  BIT             NOT NULL,
    [FECSOLRES]                     DATETIME        NOT NULL,
    [FECSOLTRA]                     DATETIME        NULL,
    [FECRESERVA]                    DATETIME        NULL,
    [FECTRANSFU]                    DATETIME        NULL,
    [FECENTREGA]                    DATETIME        NULL,
    [PROFSOLRES]                    CHAR (20)       NOT NULL,
    [PROFSOLTRA]                    CHAR (20)       NULL,
    [PROFAPLICA]                    CHAR (20)       NULL,
    [AUXRESPRES]                    CHAR (20)       NULL,
    [AUXRESENTR]                    CHAR (20)       NULL,
    [BACRESPRES]                    CHAR (20)       NULL,
    [BACRESENTR]                    CHAR (20)       NULL,
    [UFUSOLRES]                     CHAR (10)       NOT NULL,
    [UFUSOLTRA]                     CHAR (10)       NULL,
    [UFUENTREGA]                    CHAR (10)       NULL,
    [UFUAPLICA]                     CHAR (10)       NULL,
    [REAPRUECRU]                    BIT             NULL,
    [NUMBOLSA]                      VARCHAR (20)    NULL,
    [SELLOCALIDAD]                  VARCHAR (20)    NULL,
    [GRUPOBOL]                      TINYINT         NULL,
    [FECHAEXPIRA]                   DATETIME        NULL,
    [ASPECFISICO]                   TINYINT         NULL,
    [TEMPENTREG]                    INT             NULL,
    [REGENF]                        BIT             CONSTRAINT [DF_HCORHEMBOL_REGENF] DEFAULT ((0)) NULL,
    [REGMED]                        BIT             NULL,
    [PROFLIBERO]                    CHAR (20)       NULL,
    [BOLRECIBIDA]                   BIT             NULL,
    [PROFRECIBIO]                   CHAR (20)       NULL,
    [FECRECIBIO]                    DATETIME        NULL,
    [ENFAPLICA]                     CHAR (20)       NULL,
    [FECAPLICMED]                   DATETIME        NULL,
    [FECAPLICENF]                   DATETIME        NULL,
    [MOTNOTRANS]                    VARCHAR (2000)  NULL,
    [ENTRANSFU]                     BIT             NULL,
    [FECHINITRA]                    DATETIME        NULL,
    [FECHFINTRA]                    DATETIME        NULL,
    [VOLTRANSF]                     INT             NULL,
    [COMPLICACIONES]                VARCHAR (2000)  NULL,
    [MOTNOREAPC]                    VARCHAR (5000)  NULL,
    [HCMOTNOREPCID]                 INT             NULL,
    [CONFRECBOL]                    INT             CONSTRAINT [DF_HCORHEMBOL_CONFRECBOL] DEFAULT ((1)) NOT NULL,
    [ASPECFISENFCONF]               TINYINT         NULL,
    [TEMPCONF]                      INT             NULL,
    [MOTLIBRESID]                   INT             NULL,
    [MOTLIBRESDES]                  VARCHAR (5000)  NULL,
    [HCRECHEMOID]                   INT             NULL,
    [MOTRECHAZ]                     VARCHAR (5000)  NULL,
    [ENFRECHAZ]                     CHAR (20)       NULL,
    [NUMFOLTRANS]                   NCHAR (10)      NULL,
    [USURECBOLSA]                   CHAR (20)       NULL,
    [FECRECBOLSA]                   DATETIME        NULL,
    [PRUEBACRUZADA]                 BIT             NULL,
    [OBSERVACIONLABORA]             VARCHAR (5000)  NULL,
    [TEMPID]                        INT             NULL,
    [VolumeTransfuse]               NUMERIC (18, 1) NULL,
    [DateOfReceptionUnprocessedBag] DATETIME        NULL,
    CONSTRAINT [PK_HCORHEMBOL] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCORHEMBOL_HCCOMSAN] FOREIGN KEY ([COMSAMID]) REFERENCES [dbo].[HCCOMSAN] ([ID]),
    CONSTRAINT [FK_HCORHEMBOL_HCMOTLIB] FOREIGN KEY ([MOTLIBRESID]) REFERENCES [dbo].[HCMOTLIB] ([ID]),
    CONSTRAINT [FK_HCORHEMBOL_HCMOTNOREPC] FOREIGN KEY ([HCMOTNOREPCID]) REFERENCES [dbo].[HCMOTNOREPC] ([ID]),
    CONSTRAINT [FK_HCORHEMBOL_HCORHEMBOL] FOREIGN KEY ([ID]) REFERENCES [dbo].[HCORHEMBOL] ([ID]),
    CONSTRAINT [FK_HCORHEMBOL_HCORHEMBOL1] FOREIGN KEY ([ID]) REFERENCES [dbo].[HCORHEMBOL] ([ID]),
    CONSTRAINT [FK_HCORHEMBOL_HCORHEMBOL2] FOREIGN KEY ([ID]) REFERENCES [dbo].[HCORHEMBOL] ([ID]),
    CONSTRAINT [FK_HCORHEMBOL_HCORHEMCO] FOREIGN KEY ([HCORHEMCOID]) REFERENCES [dbo].[HCORHEMCO] ([ID]),
    CONSTRAINT [FK_HCORHEMBOL_HCRECHEMO] FOREIGN KEY ([HCRECHEMOID]) REFERENCES [dbo].[HCRECHEMO] ([ID]),
    CONSTRAINT [FK_HCORHEMBOL_INCUPSIPS] FOREIGN KEY ([ENFAPLICA]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCORHEMBOL_INPROFSAL] FOREIGN KEY ([AUXRESENTR]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCORHEMBOL_INPROFSAL1] FOREIGN KEY ([AUXRESPRES]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCORHEMBOL_INPROFSAL2] FOREIGN KEY ([BACRESENTR]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCORHEMBOL_INPROFSAL3] FOREIGN KEY ([BACRESPRES]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCORHEMBOL_INPROFSAL4] FOREIGN KEY ([PROFAPLICA]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCORHEMBOL_INPROFSAL5] FOREIGN KEY ([PROFSOLRES]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCORHEMBOL_INPROFSAL6] FOREIGN KEY ([PROFSOLTRA]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCORHEMBOL_INPROFSAL7] FOREIGN KEY ([PROFLIBERO]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCORHEMBOL_INPROFSAL8] FOREIGN KEY ([PROFRECIBIO]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCORHEMBOL_INPROFSAL9] FOREIGN KEY ([ENFRECHAZ]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCORHEMBOL_INUNIFUNC] FOREIGN KEY ([UFUAPLICA]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCORHEMBOL_INUNIFUNC1] FOREIGN KEY ([UFUENTREGA]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCORHEMBOL_INUNIFUNC2] FOREIGN KEY ([UFUSOLRES]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCORHEMBOL_INUNIFUNC3] FOREIGN KEY ([UFUSOLTRA]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCORHEMBOL] NOCHECK CONSTRAINT [FK_HCORHEMBOL_HCORHEMCO];


GO
ALTER TABLE [dbo].[HCORHEMBOL] NOCHECK CONSTRAINT [FK_HCORHEMBOL_INPROFSAL4];


GO
ALTER TABLE [dbo].[HCORHEMBOL] NOCHECK CONSTRAINT [FK_HCORHEMBOL_INPROFSAL5];


GO
ALTER TABLE [dbo].[HCORHEMBOL] NOCHECK CONSTRAINT [FK_HCORHEMBOL_INPROFSAL6];




GO



GO



GO



GO



GO



GO



GO
ALTER TABLE [dbo].[HCORHEMBOL] NOCHECK CONSTRAINT [FK_HCORHEMBOL_HCORHEMCO];


GO



GO



GO



GO



GO



GO



GO
ALTER TABLE [dbo].[HCORHEMBOL] NOCHECK CONSTRAINT [FK_HCORHEMBOL_INPROFSAL4];


GO
ALTER TABLE [dbo].[HCORHEMBOL] NOCHECK CONSTRAINT [FK_HCORHEMBOL_INPROFSAL5];


GO
ALTER TABLE [dbo].[HCORHEMBOL] NOCHECK CONSTRAINT [FK_HCORHEMBOL_INPROFSAL6];


GO



GO



GO



GO



GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_HCORHEMBOL_CONFRECBOL_ESTADO]
    ON [dbo].[HCORHEMBOL]([CONFRECBOL] ASC, [ESTADO] ASC)
    INCLUDE([HCORHEMCOID]);


GO
CREATE NONCLUSTERED INDEX [IX_HCORHEMBOL]
    ON [dbo].[HCORHEMBOL]([HCORHEMCOID] ASC, [PENCONRES] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de recepción de bolsa hemocomponente en estado sin procesar desde solicitud de reserva. Timestamp DATETIME, vinculado a Dashboard Hemocomponentes para control de recepción pendiente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'DateOfReceptionUnprocessedBag';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de recepción de la bolsa en solicitud de reserva sin procesar (Desde pestaña Solicitudes del Dashboard Hemocomponentes) *** PBI 13363 - Crear opción recibir solicitud Dashboard hemocomponentes ***', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'DateOfReceptionUnprocessedBag';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'DateOfReceptionUnprocessedBag';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Volumen en mililitros (mL) a transfundir al paciente. Numérico INT, registrado en la orden médica de hemocomponentes, define cantidad de componente sanguíneo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'VolumeTransfuse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Volumen a transfundir, se guardar en el ordenamiento medico del hemocomponentes.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'VolumeTransfuse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'VolumeTransfuse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador temporal único INT. Campo de control interno para asociación transitoria de registros durante procesamiento de hemocomponentes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'TEMPID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID temporal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'TEMPID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'TEMPID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación de laboratorio o bacteriología VARCHAR(5000). Notas clínicas sobre hallazgos, condiciones o recomendaciones del hemocomponente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'OBSERVACIONLABORA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'OBSERVACIONLABORA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'OBSERVACIONLABORA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT de realización de prueba cruzada: 1=Sí realizada, 0=No realizada. Validación de compatibilidad ABO-Rh entre donante y receptor.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'PRUEBACRUZADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prueba cruzada:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'PRUEBACRUZADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'PRUEBACRUZADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora DATETIME en que enfermería recibe la bolsa hemocomponente desde banco de sangre. Marca el inicio de custodia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'FECRECBOLSA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en que se recibe la bolsa desde enfermería  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'FECRECBOLSA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'FECRECBOLSA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CHAR(20) del usuario/enfermero que recibe la bolsa hemocomponente. FK a INPROFSAL, PII identificación profesional de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'USURECBOLSA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Registra el usuario que recibe la bolsa desde enfermería', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'USURECBOLSA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'USURECBOLSA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio o expediente del paciente NCHAR(10). Identificador administrativo del registro clínico del receptor.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'NUMFOLTRANS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero folio paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'NUMFOLTRANS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'NUMFOLTRANS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CHAR(20) de enfermero(a) que rechaza la bolsa hemocomponente. FK a INPROFSAL, profesional de enfermería que ejecuta rechazo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'ENFRECHAZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Enfermera que rechazo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'ENFRECHAZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'ENFRECHAZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo de rechazo de bolsa hemocomponente por enfermería VARCHAR(5000). Razones clínicas o administrativas para no aplicar el componente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'MOTRECHAZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo de Rechazo Hemocomponentes por enfermería', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'MOTRECHAZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'MOTRECHAZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador INT de catálogo de motivos de rechazo hemocomponentes. FK a HCRECHEMO, referencia de motivo estandarizado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'HCRECHEMOID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del Motivo de Rechazo Hemocomponentes por enfermería', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'HCRECHEMOID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'HCRECHEMOID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción completa VARCHAR(5000) del motivo de liberación de bolsa hemocomponente reservada. Detalle de razón por la cual se libera reserva.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'MOTLIBRESDES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del Motivo de Liberación Hemocomponentes Reservados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'MOTLIBRESDES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'MOTLIBRESDES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador INT de motivo de liberación de reserva hemocomponente. FK a HCMOTLIB, catálogo de causas de liberación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'MOTLIBRESID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del Motivo de Liberación Hemocomponentes Reservados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'MOTLIBRESID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'MOTLIBRESID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Temperatura en grados Celsius INT de la bolsa hemocomponente confirmada por enfermería al recibir. Control de cadena de frío.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'TEMPCONF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Temperatura bolsa confirmada por enfermería', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'TEMPCONF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'TEMPCONF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Aspecto físico TINYINT de bolsa confirmado por enfermería: 1=Adecuado, 2=Inadecuado. Evaluación de integridad y condición visual.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'ASPECFISENFCONF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Aspecto fisico de la unidad cuando enfermería confirma recibido (bolsa) 1-> adecuado   2->inadecuado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'ASPECFISENFCONF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'ASPECFISENFCONF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de confirmación TINYINT de recepción/rechazo de bolsa: 1=Pendiente confirmación, 2=Aceptada/confirmada, 3=Rechazada. Flag de validación de entrega.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'CONFRECBOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Confirmación de recepción o rechazo de bolsa  1 -> Pendiente de confirmación  2 -> Aceptada/confirmación recepción  3 -> Rechazo de bolsa - Hemocomponentes Rechazados  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'CONFRECBOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'CONFRECBOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador INT del motivo de no realización de pruebas cruzadas. FK a HCMOTNOREPC, catálogo de causas de exclusión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'HCMOTNOREPCID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del motivo de no realización de pruebas cruzadas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'HCMOTNOREPCID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'HCMOTNOREPCID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo VARCHAR(5000) por el cual no se realiza prueba cruzada hemocomponente. Justificación clínica o administrativa del bypass.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'MOTNOREAPC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo de no realizar prueba cruzada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'MOTNOREAPC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'MOTNOREAPC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro VARCHAR(2000) de complicaciones presentadas durante o post-transfusión: reacciones adversas, hemólisis, sobrecarga. Evento clínico adverso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'COMPLICACIONES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Complicaciones en la transfusión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'COMPLICACIONES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'COMPLICACIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Volumen en mL INT efectivamente transfundido al paciente. Cantidad real de hemocomponente administrada, puede diferir del ordenado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'VOLTRANSF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Volumen transfundido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'VOLTRANSF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'VOLTRANSF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora DATETIME de finalización de la transfusión del hemocomponente. Marca cierre de administración al paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'FECHFINTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en que finaliza la transfusión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'FECHFINTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'FECHFINTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora DATETIME de inicio de transfusión del hemocomponente. Marca comienzo de administración al paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'FECHINITRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en que se inicia la transfusión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'FECHINITRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'FECHINITRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: 1=Bolsa en transfusión activa, 0=No en transfusión. Flag de estado de administración en tiempo real.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'ENTRANSFU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identifica si la bolsa esta en transfusión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'ENTRANSFU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'ENTRANSFU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo VARCHAR(2000) de no transfusión de bolsa hemocomponente. Razones por las cuales el componente no se administra al paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'MOTNOTRANS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Esta campo almacena el motivo de no transfusión de la bolsa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'MOTNOTRANS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'MOTNOTRANS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora DATETIME en que enfermera aplica/administra la bolsa hemocomponente al paciente. Timestamp de ejecución clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'FECAPLICENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en la que la enfermera aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'FECAPLICENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'FECAPLICENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora DATETIME en que médico prescribe u ordena aplicación de bolsa hemocomponente. Timestamp de autorización médica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'FECAPLICMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en la que el médico aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'FECAPLICMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'FECAPLICMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CHAR(20) de enfermero(a) que administra/aplica la bolsa hemocomponente. FK a INPROFSAL, profesional ejecutor.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'ENFAPLICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Enfermero(a) que aplica la bolsa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'ENFAPLICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'ENFAPLICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora DATETIME de recepción de bolsa hemocomponente por profesional de salud. Confirmación de entrega entre áreas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'FECRECIBIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de recibido de bolsa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'FECRECIBIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'FECRECIBIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CHAR(20) de profesional de salud que recibe bolsa hemocomponente. FK a INPROFSAL, identificación de receptor del componente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'PROFRECIBIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Registra el profesional que recibió la bolsa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'PROFRECIBIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'PROFRECIBIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT de recepción de bolsa: 1=Sí recibida, 0=No recibida. Confirmación de custodia del hemocomponente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'BOLRECIBIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si se recibió la bolsa 1-> si, 0-> no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'BOLRECIBIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'BOLRECIBIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CHAR(20) de profesional que cambia estado bolsa a liberada. FK a INPROFSAL, quien desbloquea componente para transfusión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'PROFLIBERO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profesional que pasa la bolsa a estado liberado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'PROFLIBERO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'PROFLIBERO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT de registro por médico: 1=Registrado por médico, 0=No. Flag de validación médica de procedimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'REGMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Registrado por medico 1--> si, 0--> no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'REGMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'REGMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT de registro por enfermería: 1=Registrado por enfermería, 0=No. Flag de validación de enfermería en aplicación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'REGENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Registrado por enfermeria: 1--> si, 0--> no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'REGENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'REGENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Temperatura en grados Celsius INT de bolsa hemocomponente en momento de entrega. Control de cadena de frío entre areas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'TEMPENTREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Temperatura de la bolsa en la entrega', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'TEMPENTREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'TEMPENTREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Aspecto físico TINYINT de unidad bolsa hemocomponente: 1=Adecuado, 2=Inadecuado. Evaluación de integridad en recepción.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'ASPECFISICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Aspecto fisico de la unidad   (bolsa) 1-> adecuado   2->inadecuado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'ASPECFISICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'ASPECFISICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora DATETIME de vencimiento/expiración de bolsa hemocomponente. Límite de validez para transfusión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'FECHAEXPIRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de expiración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'FECHAEXPIRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'FECHAEXPIRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grupo sanguíneo ABO-Rh TINYINT: 1=A+, 2=A-, 3=B+, 4=B-, 5=AB+, 6=AB-, 7=O+, 8=O-. Clasificación de donante.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'GRUPOBOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Grupo de bolsa:  1 - A+  2 - A-  3 - B+  4 - B-  5 - AB+  6 - AB-  7 - O+  8 - O-', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'GRUPOBOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'GRUPOBOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sello de calidad VARCHAR(20) de bolsa hemocomponente. Identificador de control de calidad y trazabilidad del banco.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'SELLOCALIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sello de calidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'SELLOCALIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'SELLOCALIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de identificación único VARCHAR(20) de bolsa hemocomponente. Código de trazabilidad del componente sanguíneo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'NUMBOLSA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de bolsa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'NUMBOLSA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'NUMBOLSA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT de realización prueba cruzada: 1=Sí realiza, 0=No realiza. Flag de validación de compatibilidad ABO-Rh requerida.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'REAPRUECRU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si realiza prueba cruzada  1-> si  0-> no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'REAPRUECRU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'REAPRUECRU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CHAR(10) de unidad funcional que aplica bolsa hemocomponente. FK a INUNIFUNC, departamento clínico ejecutor.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'UFUAPLICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'U Funcional Que Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'UFUAPLICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'UFUAPLICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CHAR(10) de unidad funcional que entrega bolsa hemocomponente. FK a INUNIFUNC, área de distribución.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'UFUENTREGA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'U Funcional que entrega', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'UFUENTREGA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'UFUENTREGA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CHAR(10) de unidad funcional que solicita transfusión de hemocomponente. FK a INUNIFUNC, servicio clínico solicitante.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'UFUSOLTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'U Funcional que solicita transfusión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'UFUSOLTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'UFUSOLTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CHAR(10) de unidad funcional que solicita reserva de bolsa hemocomponente. FK a INUNIFUNC, área que genera reserva.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'UFUSOLRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'U Funcional que solicita reserva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'UFUSOLRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'UFUSOLRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CHAR(20) de bacteriólogo responsable de entrega de bolsa. FK a INPROFSAL, profesional de banco de sangre.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'BACRESENTR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Bacteriologo responsable de entrega', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'BACRESENTR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'BACRESENTR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CHAR(20) de bacteriólogo responsable de reserva de bolsa hemocomponente. FK a INPROFSAL, profesional banco de sangre.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'BACRESPRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Bacteriologo Responsable de Reserva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'BACRESPRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'BACRESPRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CHAR(20) de auxiliar bacteriólogo responsable de entrega de componente. FK a INPROFSAL, personal técnico de banco.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'AUXRESENTR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Auxiliar bacteriologo Responsable de Entrega', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'AUXRESENTR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'AUXRESENTR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CHAR(20) de auxiliar bacteriólogo responsable de reserva de hemocomponente. FK a INPROFSAL, personal técnico de banco.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'AUXRESPRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Auxiliar bacteriologo Responsable de reserva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'AUXRESPRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'AUXRESPRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CHAR(20) de profesional de salud que aplica bolsa hemocomponente. FK a INPROFSAL, médico/enfermero executor.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'PROFAPLICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profesional que aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'PROFAPLICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'PROFAPLICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CHAR(20) de profesional que solicita transfusión de hemocomponente. FK a INPROFSAL, médico prescriptor.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'PROFSOLTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profesional que solicita la transfusión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'PROFSOLTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'PROFSOLTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CHAR(20) de profesional que solicita reserva de bolsa hemocomponente. FK a INPROFSAL, médico que genera reserva.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'PROFSOLRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profesional que Solicita la reserva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'PROFSOLRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'PROFSOLRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora DATETIME de entrega de bolsa hemocomponente desde banco a unidad clínica. Timestamp de distribución.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'FECENTREGA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de entrega de la bolsa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'FECENTREGA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'FECENTREGA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora DATETIME de transfusión de bolsa hemocomponente al paciente. Registro de administración.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'FECTRANSFU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Transfusión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'FECTRANSFU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'FECTRANSFU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora DATETIME de creación/confirmación de reserva de bolsa hemocomponente. Timestamp de bloqueo de componente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'FECRESERVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Reserva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'FECRESERVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'FECRESERVA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora DATETIME de solicitud de transfusión de bolsa hemocomponente. Timestamp de orden clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'FECSOLTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Solicitud de transfusión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'FECSOLTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'FECSOLTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora DATETIME de solicitud de reserva de bolsa hemocomponente. Timestamp de inicio de reserva.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'FECSOLRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha solicitud de reserva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'FECSOLRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'FECSOLRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT de devolución de registro al recibir bolsa: 1=Sí devolutivo, 0=No. Solo permitido si estado=No Aplicado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'ESDEVOLUTIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el registro es devolutivo al momento de recibir, este campo solo puede ser 1 si el estado del registro es: no aplicado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'ESDEVOLUTIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'ESDEVOLUTIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT de autorización para entrega de bolsa hemocomponente: 1=Autoriza, 0=No autoriza. Flag de validación de liberación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'AUTENTREGA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si autoriza entrega', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'AUTENTREGA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'AUTENTREGA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT de confirmación pendiente de reserva hemocomponente: 1=Pendiente confirmación, 0=Confirmada. Flag de validación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'PENCONRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si tiene pendiente Confirmación de reserva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'PENCONRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'PENCONRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo TINYINT de solicitud: 1=Reserva, 2=Transfusión, 3=Reserva y Transfusión. Clasificación de propósito del hemocomponente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'TIPOSOLICI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de solicitud con que se crea el registro.   1-> Reserva,   2-> Transfusión  3 -> Reserva y Transfusión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'TIPOSOLICI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'TIPOSOLICI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado TINYINT del hemocomponente: 1=Solicitud Reserva, 2=Solicitud Transfusión, 3=Reserva sin Transfusión, 4=Reserva con Transfusión, 5=Liberado, 6=No realizado, 7=Aplicado, 8=Descartado, 9=Anulado, 10=Descartado por salida paciente, 11=Extramural, 12=Recibido sin procesar. Ciclo de vida del componente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del componente sanguineo  1: Solicitud Reserva  2: Solicitud de Transfusión  3. Reserva sin Solicitud de Transfusión  4. Reserva con Solicitud de Transfusión  5. Liberado  6. Registro no realizado  7. Aplicado - registro realizado  8. No Aplicado - registro descartado  9. Anulado  10. Descartado por salida de paciente - descartado por medico  11. Extramural  12. Solicitud de reserva recibida sin procesar *** PBI 13363 - Crear opción recibir solicitud Dashboard hemocomponentes ***', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador INT del componente sanguíneo (tipo de hemocomponente). Referencia al tipo de sangre, plaquetas, plasma u otro producto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'COMSAMID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del componente sanguíneo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'COMSAMID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'COMSAMID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador INT de cabecera de solicitud de hemocomponentes. FK a HCORHEMCO, agrupa múltiples bolsas de una misma orden clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'HCORHEMCOID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que relaciona el ID de la tabla HCORHEMCO (Cabecera solictud de hemocomponentes)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'HCORHEMCOID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'HCORHEMCOID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único INT IDENTITY de registro de bolsa hemocomponente. Primary Key de HCORHEMBOL, clave autonumérica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de bolsas de hemoderivados (sangre y componentes sanguíneos) asociadas a órdenes de transfusión en historia clínica. Guarda el ciclo completo de cada bolsa: solicitud, reserva, entrega, aplicación, transfusión, complicaciones y rechazos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL';
