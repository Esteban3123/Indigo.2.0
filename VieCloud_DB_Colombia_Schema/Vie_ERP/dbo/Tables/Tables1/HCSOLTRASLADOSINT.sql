CREATE TABLE [dbo].[HCSOLTRASLADOSINT] (
    [ID]              INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]       VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]       CHAR (10)                                                                        NOT NULL,
    [FECHAREGISTRO]   DATETIME                                                                         NOT NULL,
    [ESTADO]          INT                                                                              NOT NULL,
    [IDHCMOANULB]     CHAR (4)                                                                         NOT NULL,
    [CODCENATEOR]     CHAR (10)                                                                        NOT NULL,
    [UFUCODIGOOR]     CHAR (10)                                                                        NOT NULL,
    [CODCENATEDE]     CHAR (10)                                                                        NOT NULL,
    [UFUCODIGODE]     CHAR (10)                                                                        NOT NULL,
    [USUARIOSOLICITA] CHAR (20)                                                                        NOT NULL,
    [OBSERVACION]     VARCHAR (MAX)                                                                    NULL,
    [NOVEDAD]         VARCHAR (MAX)                                                                    NULL,
    [FECHAANULA]      DATETIME                                                                         NULL,
    [USUARIOANULA]    CHAR (20)                                                                        NULL,
    [IDHCMOANULA]     CHAR (4)                                                                         NULL,
    [JUSTIFICAANULA]  VARCHAR (MAX)                                                                    NULL,
    [USUARIOVISADO]   CHAR (20)                                                                        NULL,
    [FECHAVISADO]     DATETIME                                                                         NULL,
    CONSTRAINT [PK_HCSOLTRASLADOSINT] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCSOLTRASLADOSINT_ADCENATEN1] FOREIGN KEY ([CODCENATEDE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCSOLTRASLADOSINT_ADCENATEN2] FOREIGN KEY ([CODCENATEOR]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCSOLTRASLADOSINT_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCSOLTRASLADOSINT_HCMOANULB] FOREIGN KEY ([IDHCMOANULA]) REFERENCES [dbo].[HCMOANULB] ([CODMOTANU]),
    CONSTRAINT [FK_HCSOLTRASLADOSINT_HCMOANULB1] FOREIGN KEY ([IDHCMOANULB]) REFERENCES [dbo].[HCMOANULB] ([CODMOTANU]),
    CONSTRAINT [FK_HCSOLTRASLADOSINT_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCSOLTRASLADOSINT_INUNIFUNC] FOREIGN KEY ([UFUCODIGOOR]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCSOLTRASLADOSINT_INUNIFUNC1] FOREIGN KEY ([UFUCODIGODE]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCSOLTRASLADOSINT].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del visado o aprobación de la solicitud de traslado interno por usuario autorizado (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'FECHAVISADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del visado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'FECHAVISADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'FECHAVISADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario autorizado que realizó el visado o aprobación final de la solicitud de traslado interno (CHAR 20)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'USUARIOVISADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario del visado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'USUARIOVISADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'USUARIOVISADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación o motivo detallado de la anulación de la solicitud de traslado interno (VARCHAR MAX, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'JUSTIFICAANULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificacion de la anulación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'JUSTIFICAANULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'JUSTIFICAANULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del motivo de anulación de la solicitud; relación con tabla HCMOANULB de motivos de cancelación (FK HCMOANULB, CHAR 4, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'IDHCMOANULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relación con la tabla de motivos de la solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'IDHCMOANULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'IDHCMOANULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que ejecutó la anulación o cancelación de la solicitud de traslado interno (CHAR 20, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'USUARIOANULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Anulo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'USUARIOANULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'USUARIOANULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se registró la anulación o cancelación de la solicitud de traslado (DATETIME, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'FECHAANULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Anulación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'FECHAANULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'FECHAANULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo para registrar novedades o eventos relevantes del traslado, alimentado desde el dashboard de traslados internos (VARCHAR MAX, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'NOVEDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que se llena desde el dashboard de tralados internos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'NOVEDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'NOVEDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones adicionales o comentarios sobre la solicitud de traslado interno (VARCHAR MAX, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'OBSERVACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'OBSERVACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'OBSERVACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario del sistema que originó o solicitó el traslado interno del paciente (CHAR 20)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'USUARIOSOLICITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que solicita el traslado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'USUARIOSOLICITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'USUARIOSOLICITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional destino donde se trasladará al paciente (FK INUNIFUNC, CHAR 10)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'UFUCODIGODE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'unidad funcional destino', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'UFUCODIGODE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'UFUCODIGODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención destino donde se realizará el traslado (FK ADCENATEN, CHAR 10)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'CODCENATEDE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Centro atención destino', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'CODCENATEDE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'CODCENATEDE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional origen donde se inicia el traslado interno (FK INUNIFUNC, CHAR 10)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'UFUCODIGOOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad Funcional orginen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'UFUCODIGOOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'UFUCODIGOOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención origen desde donde se solicita el traslado (FK ADCENATEN, CHAR 10)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'CODCENATEOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Centro Atención origen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'CODCENATEOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'CODCENATEOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del motivo o tipo de solicitud de traslado interno; relación con tabla HCMOANULB (FK HCMOANULB, CHAR 4)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'IDHCMOANULB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo de la Solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'IDHCMOANULB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'IDHCMOANULB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado actual de la solicitud de traslado: 1=Solicitado, 2=Ambulancia asignada, 3=Traslado iniciado, 4=Traslado completado, 5=Cancelado (sin movimientos), 6=Visado/Aprobado (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado  1 - solicitado   2 - Ambulancia asignada   3 - Traslado Iniciado   4 - Traslado completado   5 - Cancelado (Pasa aqui cuando la solicitud del traslado no tenia movimientos es decir estaba en solicitada)  6  - Registro Visado  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación o registro de la solicitud de traslado interno en el sistema (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número único del ingreso o admisión del paciente; relación con tabla ADINGRESO (FK ADINGRESO, CHAR 10)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del paciente (cédula, documento, identificación); PII Ofuscado (FK INPACIENT, VARCHAR 25 MASKED)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único y clave primaria que registra la trazabilidad completa de cada solicitud de traslado interno (INT IDENTITY)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la trazabilidad de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Solicitudes de traslado interno de pacientes hospitalizados entre unidades funcionales o centros de atención. Registra el origen, destino, estado de la solicitud, usuario que solicita, anulaciones y visados de cada traslado interno.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOLTRASLADOSINT';
