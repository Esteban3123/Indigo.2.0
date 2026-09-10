CREATE TABLE [dbo].[INCTTRCOD] (
    [CONSECUTI] INT                                                                            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PACANTERI] CHAR (15) MASKED WITH (FUNCTION = 'partial(0, "Indentification_Ofuscado", 0)') NOT NULL,
    [FECREGIST] DATETIME                                                                       NOT NULL,
    [ESTADOPAC] CHAR (1)                                                                       NOT NULL,
    [PACINUEVO] CHAR (15) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')  NOT NULL,
    [CODUSUARI] CHAR (20)                                                                      NOT NULL,
    [TRASPACYA] BIT                                                                            NULL,
    [OBSERVACI] VARCHAR (250)                                                                  NULL,
    CONSTRAINT [PK_INCTTRCOD] PRIMARY KEY CLUSTERED ([CONSECUTI] ASC)
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INCTTRCOD].[PACANTERI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INCTTRCOD].[PACINUEVO]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas y observaciones adicionales del traslado de paciente; texto libre (VARCHAR 250) para registrar detalles relevantes de la transferencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCTTRCOD', @level2type = N'COLUMN', @level2name = N'OBSERVACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCTTRCOD', @level2type = N'COLUMN', @level2name = N'OBSERVACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCTTRCOD', @level2type = N'COLUMN', @level2name = N'OBSERVACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que determina si el paciente está disponible para traslado cuando se ejecuta ''''Iniciar Ya'''' en el administrador de servicios; flag de readiness para transferencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCTTRCOD', @level2type = N'COLUMN', @level2name = N'TRASPACYA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite saber si cuando en el administrador de los servicios se ejecuta iniciar ya, el paciente esta disponible para el traslado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCTTRCOD', @level2type = N'COLUMN', @level2name = N'TRASPACYA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCTTRCOD', @level2type = N'COLUMN', @level2name = N'TRASPACYA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario/profesional de salud (CHAR 20) que ejecutó o autorizó el traslado; código del operador del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCTTRCOD', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Usuario que ejecuto el traslado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCTTRCOD', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCTTRCOD', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente destino (CHAR 15, PII ofuscado); identificación equivalente a cédula/documento del paciente receptor en el traslado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCTTRCOD', @level2type = N'COLUMN', @level2name = N'PACINUEVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente Nuevo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCTTRCOD', @level2type = N'COLUMN', @level2name = N'PACINUEVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCTTRCOD', @level2type = N'COLUMN', @level2name = N'PACINUEVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del paciente en traslado: 1=Activo, 2=Inactivo (CHAR 1); indica disponibilidad o validez del registro de transferencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCTTRCOD', @level2type = N'COLUMN', @level2name = N'ESTADOPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1-Activo  2-Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCTTRCOD', @level2type = N'COLUMN', @level2name = N'ESTADOPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCTTRCOD', @level2type = N'COLUMN', @level2name = N'ESTADOPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de registro/creación del evento de traslado en el sistema; timestamp de la transferencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCTTRCOD', @level2type = N'COLUMN', @level2name = N'FECREGIST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Registro en el sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCTTRCOD', @level2type = N'COLUMN', @level2name = N'FECREGIST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCTTRCOD', @level2type = N'COLUMN', @level2name = N'FECREGIST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente origen (CHAR 15, PII ofuscado); identificación equivalente a cédula/documento del paciente emisor del traslado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCTTRCOD', @level2type = N'COLUMN', @level2name = N'PACANTERI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente Anterior', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCTTRCOD', @level2type = N'COLUMN', @level2name = N'PACANTERI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCTTRCOD', @level2type = N'COLUMN', @level2name = N'PACANTERI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de secuencia único (INT IDENTITY); identificador correlativo del registro de traslado de paciente en la tabla INCTTRCOD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCTTRCOD', @level2type = N'COLUMN', @level2name = N'CONSECUTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCTTRCOD', @level2type = N'COLUMN', @level2name = N'CONSECUTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCTTRCOD', @level2type = N'COLUMN', @level2name = N'CONSECUTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de traslados o cambios de código de identificación de pacientes. Guarda el historial de cuando un paciente cambia su documento, cédula o identificación en el sistema, conservando el documento anterior y el nuevo para trazabilidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCTTRCOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCTTRCOD';
