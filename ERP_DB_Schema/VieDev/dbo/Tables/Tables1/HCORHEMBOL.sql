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
ALTER TABLE [dbo].[HCORHEMBOL] NOCHECK CONSTRAINT [FK_HCORHEMBOL_HCCOMSAN];


GO
ALTER TABLE [dbo].[HCORHEMBOL] NOCHECK CONSTRAINT [FK_HCORHEMBOL_HCMOTLIB];


GO
ALTER TABLE [dbo].[HCORHEMBOL] NOCHECK CONSTRAINT [FK_HCORHEMBOL_HCMOTNOREPC];


GO
ALTER TABLE [dbo].[HCORHEMBOL] NOCHECK CONSTRAINT [FK_HCORHEMBOL_HCORHEMBOL];


GO
ALTER TABLE [dbo].[HCORHEMBOL] NOCHECK CONSTRAINT [FK_HCORHEMBOL_HCORHEMBOL1];


GO
ALTER TABLE [dbo].[HCORHEMBOL] NOCHECK CONSTRAINT [FK_HCORHEMBOL_HCORHEMBOL2];


GO
ALTER TABLE [dbo].[HCORHEMBOL] NOCHECK CONSTRAINT [FK_HCORHEMBOL_HCORHEMCO];


GO
ALTER TABLE [dbo].[HCORHEMBOL] NOCHECK CONSTRAINT [FK_HCORHEMBOL_HCRECHEMO];


GO
ALTER TABLE [dbo].[HCORHEMBOL] NOCHECK CONSTRAINT [FK_HCORHEMBOL_INCUPSIPS];


GO
ALTER TABLE [dbo].[HCORHEMBOL] NOCHECK CONSTRAINT [FK_HCORHEMBOL_INPROFSAL];


GO
ALTER TABLE [dbo].[HCORHEMBOL] NOCHECK CONSTRAINT [FK_HCORHEMBOL_INPROFSAL1];


GO
ALTER TABLE [dbo].[HCORHEMBOL] NOCHECK CONSTRAINT [FK_HCORHEMBOL_INPROFSAL2];


GO
ALTER TABLE [dbo].[HCORHEMBOL] NOCHECK CONSTRAINT [FK_HCORHEMBOL_INPROFSAL3];


GO
ALTER TABLE [dbo].[HCORHEMBOL] NOCHECK CONSTRAINT [FK_HCORHEMBOL_INPROFSAL4];


GO
ALTER TABLE [dbo].[HCORHEMBOL] NOCHECK CONSTRAINT [FK_HCORHEMBOL_INPROFSAL5];


GO
ALTER TABLE [dbo].[HCORHEMBOL] NOCHECK CONSTRAINT [FK_HCORHEMBOL_INPROFSAL6];


GO
ALTER TABLE [dbo].[HCORHEMBOL] NOCHECK CONSTRAINT [FK_HCORHEMBOL_INPROFSAL7];


GO
ALTER TABLE [dbo].[HCORHEMBOL] NOCHECK CONSTRAINT [FK_HCORHEMBOL_INPROFSAL8];


GO
ALTER TABLE [dbo].[HCORHEMBOL] NOCHECK CONSTRAINT [FK_HCORHEMBOL_INPROFSAL9];


GO
ALTER TABLE [dbo].[HCORHEMBOL] NOCHECK CONSTRAINT [FK_HCORHEMBOL_INUNIFUNC];


GO
ALTER TABLE [dbo].[HCORHEMBOL] NOCHECK CONSTRAINT [FK_HCORHEMBOL_INUNIFUNC1];


GO
ALTER TABLE [dbo].[HCORHEMBOL] NOCHECK CONSTRAINT [FK_HCORHEMBOL_INUNIFUNC2];


GO
ALTER TABLE [dbo].[HCORHEMBOL] NOCHECK CONSTRAINT [FK_HCORHEMBOL_INUNIFUNC3];


GO
CREATE NONCLUSTERED INDEX [IX_HCORHEMBOL_CONFRECBOL_ESTADO]
    ON [dbo].[HCORHEMBOL]([CONFRECBOL] ASC, [ESTADO] ASC)
    INCLUDE([HCORHEMCOID]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de recepción de la bolsa en solicitud de reserva sin procesar (Desde pestaña Solicitudes del Dashboard Hemocomponentes) *** PBI 13363 - Crear opción recibir solicitud Dashboard hemocomponentes ***', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'DateOfReceptionUnprocessedBag';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Volumen a transfundir, se guardar en el ordenamiento medico del hemocomponentes.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'VolumeTransfuse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prueba cruzada:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'PRUEBACRUZADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en que se recibe la bolsa desde enfermería  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'FECRECBOLSA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Aspecto fisico de la unidad cuando enfermería confirma recibido (bolsa) 1-> adecuado   2->inadecuado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'ASPECFISENFCONF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Confirmación de recepción o rechazo de bolsa  1 -> Pendiente de confirmación  2 -> Aceptada/confirmación recepción  3 -> Rechazo de bolsa - Hemocomponentes Rechazados  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'CONFRECBOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Aspecto fisico de la unidad   (bolsa) 1-> adecuado   2->inadecuado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'ASPECFISICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Grupo de bolsa:  1 - A+  2 - A-  3 - B+  4 - B-  5 - AB+  6 - AB-  7 - O+  8 - O-', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'GRUPOBOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si realiza prueba cruzada  1-> si  0-> no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'REAPRUECRU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de solicitud con que se crea el registro.   1-> Reserva,   2-> Transfusión  3 -> Reserva y Transfusión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'TIPOSOLICI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del componente sanguineo  1: Solicitud Reserva  2: Solicitud de Transfusión  3. Reserva sin Solicitud de Transfusión  4. Reserva con Solicitud de Transfusión  5. Liberado  6. Registro no realizado  7. Aplicado - registro realizado  8. No Aplicado - registro descartado  9. Anulado  10. Descartado por salida de paciente - descartado por medico  11. Extramural  12. Solicitud de reserva recibida sin procesar *** PBI 13363 - Crear opción recibir solicitud Dashboard hemocomponentes ***', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMBOL', @level2type = N'COLUMN', @level2name = N'ESTADO';

