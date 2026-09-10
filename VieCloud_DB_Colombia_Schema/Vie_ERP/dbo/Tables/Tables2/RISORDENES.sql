CREATE TABLE [dbo].[RISORDENES] (
    [AUTO]           INT                IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ESTADO]         INT                NOT NULL,
    [IDHCORDIMAG]    INT                NULL,
    [IDAMBORDIMA]    INT                NULL,
    [NUMCONCIT]      INT                NULL,
    [FECAGENDO]      DATETIMEOFFSET (7) NULL,
    [FECREALIZANDO]  DATETIMEOFFSET (7) NULL,
    [FECREALIZADO]   DATETIMEOFFSET (7) NULL,
    [FECREMITE]      DATETIMEOFFSET (7) NULL,
    [FECANULADO]     DATETIMEOFFSET (7) NULL,
    [USUAGENDO]      VARCHAR (20)       NULL,
    [USUREALIZO]     VARCHAR (20)       NULL,
    [USUREMITE]      VARCHAR (20)       NULL,
    [USUANULO]       VARCHAR (20)       NULL,
    [MOTANULADO]     VARCHAR (50)       NULL,
    [JUSANULADO]     VARCHAR (50)       NULL,
    [CODCENLAB]      VARCHAR (50)       NULL,
    [JUSREMITE]      VARCHAR (50)       NULL,
    [CONCURREN]      ROWVERSION         NULL,
    [FECHCREA]       DATETIMEOFFSET (7) NOT NULL,
    [IDSALA]         INT                NULL,
    [FECHORAIN]      DATETIMEOFFSET (7) NULL,
    [FECHORAFI]      DATETIMEOFFSET (7) NULL,
    [USUDEVOLUCION]  VARCHAR (20)       NULL,
    [MOTDEVOLUCION]  VARCHAR (50)       NULL,
    [FECDEVOLUCION]  DATETIMEOFFSET (7) NULL,
    [OBSEDEVOLUCION] VARCHAR (50)       NULL,
    [CONSENTIMIENTO] INT                NULL,
    CONSTRAINT [PK_RISORDENES] PRIMARY KEY CLUSTERED ([AUTO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consentimiento informado del paciente: 1=Acepta, 2=No acepta procedimiento de imagenología', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'CONSENTIMIENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'especifica si acepto o no el consentimiento   1 - Si Acepto  2 - No Acepto  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'CONSENTIMIENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'CONSENTIMIENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación/justificación detallada de la devolución; complementa MOTDEVOLUCION', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'OBSEDEVOLUCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion/Justificacion de la devolucion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'OBSEDEVOLUCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'OBSEDEVOLUCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se registra la devolución del estudio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'FECDEVOLUCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en que se realiza la devolucion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'FECDEVOLUCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'FECDEVOLUCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo codificado de la devolución del estudio (ej: mala calidad técnica, no evaluable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'MOTDEVOLUCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo de devolucion selecionado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'MOTDEVOLUCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'MOTDEVOLUCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código/login del usuario que realiza la devolución/rechazo del estudio; PII_Ofuscado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'USUDEVOLUCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Usuario que realiza la devolucion del estudio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'USUDEVOLUCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'USUDEVOLUCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora final/end de la cita médica programada para el estudio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'FECHORAFI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora final de la Cita Médica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'FECHORAFI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'FECHORAFI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora inicial/start de la cita médica programada para el estudio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'FECHORAIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora inicial de la Cita Médica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'FECHORAIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'FECHORAIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la sala/consulta/área donde se agendó o realizará el estudio de imagen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'IDSALA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la Sala', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'IDSALA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'IDSALA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro en tabla RISORDENES; timestamp de origen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'FECHCREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en la que se creo el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'FECHCREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'FECHCREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación/observación clínica de la remisión a centro externo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'JUSREMITE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificación/Observacion de la remisión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'JUSREMITE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'JUSREMITE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de referencia/remisión seleccionado para enviar el estudio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'CODCENLAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Centro de remision seleccionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'CODCENLAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'CODCENLAB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación/explicación adicional de por qué se anuló; complementa MOTANULADO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'JUSANULADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificación de la anulación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'JUSANULADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'JUSANULADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo/razón codificada de la anulación del estudio (ej: cambio de procedimiento, contradicción)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'MOTANULADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo de anulación seleccionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'MOTANULADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'MOTANULADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código/login del usuario que anuló la orden; PII_Ofuscado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'USUANULO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que anuló', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'USUANULO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'USUANULO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código/login del usuario que remitió el estudio a centro externo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'USUREMITE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que remitió el estudio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'USUREMITE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'USUREMITE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código/login del usuario tecnólogo que realizó/ejecutó el estudio de imagen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'USUREALIZO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que realizó el estudio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'USUREALIZO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'USUREALIZO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código/login del usuario (tecnólogo) que agendó la cita de imagenología', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'USUAGENDO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que agendó', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'USUAGENDO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'USUAGENDO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de anulación del estudio; registra cuándo el registro pasa a estado Anulado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'FECANULADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de cuando se anuló el estudio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'FECANULADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'FECANULADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de remisión del estudio a centro de referencia; marca transición a estado Remitido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'FECREMITE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en la que se remite el estudio, cuando se pone en estado 7-Remitido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'FECREMITE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'FECREMITE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de finalización del estudio de imagen; registra cuándo pasa a estado Realizado (sin/con imagen)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'FECREALIZADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en la que se finaliza el estudio, cuando se pone en estado 4-Realizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'FECREALIZADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'FECREALIZADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de inicio de realización/ejecución del estudio de imagen por tecnólogo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'FECREALIZANDO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en la que se inicia el estudio, cuando se pone en estado 3-Realizando', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'FECREALIZANDO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'FECREALIZANDO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de agendamiento de la cita/orden; debe coincidir con fecha creación en AGASICITA si proviene de agendamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'FECAGENDO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en la que se agendó, Si viene de una cita de agendamiento debe ser la misma fecha de creación de la cita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'FECAGENDO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'FECAGENDO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cita agendada; clave foránea a tabla AGASICITA para vincular orden con agendamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'NUMCONCIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla AGASICITA (Cuando se crea una cita desde agendamiento también se crea un registro en esta tabla con el IDCITA)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'NUMCONCIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'NUMCONCIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea a tabla AMBORDIMA; identifica orden de imagenología ambulatoria cuando se factura desde CSA o consulta externa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'IDAMBORDIMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla AMBORDIMA (si es un registro ambulatorio)    Cuando se factura un procedimiento de imagenología desde CSA, se crea un registro en la tabla AMBORDIMA y si el registro quedó asociado a una cita de agendamiento (AMBORDIMA.NUMCONCIT) se debe relacionar el ID del registro de AMBORDIMA con el registro de IndiraRIS.Ordenes que tienen el mismo consecutivo de cita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'IDAMBORDIMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'IDAMBORDIMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea a tabla HCORDIMAG; identifica orden de imagenología intrahospitalaria/hospitalización cuando aplique', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'IDHCORDIMAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla HCORDIMAG (si es un registro intrahospitalario)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'IDHCORDIMAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'IDHCORDIMAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del estudio/procedimiento de imagen: 1=Solicitado, 2=Agendado, 3=Realizando, 4=Realizado sin imagen, 5=Realizado con imagen, 6=Remitido, 7=Con lectura radiológica, 8=Anulado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'estado del estudio  1. Solicitado - Tecnólogo  2. Agendado - Tecnólogo  3. Realizando - Tecnólogo  4. Realizado – Sin imagen (sin lectura) - Tecnólogo  5. Realizado – Con imagen (sin lectura) – Tecnólogo  6. Remitido – Tecnólogo  7. Con lectura – Radiólogo  8. Anulado – Tecnólogo – agendamiento  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código autonumérico (INT IDENTITY) identificador único de la orden de imagenología/RIS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo autonumerico de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de órdenes de imágenes diagnósticas (radiología, ecografía, etc.), con su ciclo de vida completo: agendamiento, realización, remisión, anulación y devolución, tanto para pacientes hospitalizados como ambulatorios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Control de concurrencia optimista (timestamp interno del sistema) para evitar conflictos cuando múltiples usuarios modifican la misma orden simultáneamente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'CONCURREN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISORDENES', @level2type = N'COLUMN', @level2name = N'CONCURREN';
