CREATE TABLE [dbo].[ADRECO3047] (
    [AUTO]      NUMERIC (18)   IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCENATE] CHAR (10)      NOT NULL,
    [CONPARENT] INT            NOT NULL,
    [CORREOENV] VARCHAR (60)   NOT NULL,
    [ASUNTOCOR] VARCHAR (100)  NOT NULL,
    [MENSAJECO] VARCHAR (5000) NOT NULL,
    [FECHAENVI] DATETIME       NOT NULL,
    [REPRIRENV] BIT            NULL,
    [RESEGRENV] BIT            NULL,
    [FEPRIRENV] DATETIME       NULL,
    [FESEGRENV] DATETIME       NULL,
    [NUMINFORM] CHAR (4)       NOT NULL,
    [CONSINFOR] NUMERIC (18)   NOT NULL,
    [TIPOREPOR] CHAR (1)       NULL,
    CONSTRAINT [PK_ADRECO3047] PRIMARY KEY CLUSTERED ([AUTO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de reporte: 1=Atención inicial urgencias, 2=Inconsistencias, 3=Solicitud de servicios. Clasificación de documento de comunicación clínico-administrativa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'TIPOREPOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo Reporte  1: Atencion Inicial Urgencias;  2:Inconsistencias;  3:Solicitud de Servicios;', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'TIPOREPOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'TIPOREPOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo único del informe. Identificador numérico secuencial que vincula el registro de comunicación con el documento informativo asociado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'CONSINFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo del Informe', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'CONSINFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'CONSINFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de informe (4 caracteres). Código de referencia del documento clínico, administrativo o de gestión que se comunica por correo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'NUMINFORM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de informe', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'NUMINFORM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'NUMINFORM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del segundo reenvío de correo. Timestamp de la segunda tentativa de distribución del mensaje cuando el primer envío no se completó.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'FESEGRENV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del Segundo Re Envio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'FESEGRENV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'FESEGRENV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del primer reenvío de correo. Timestamp de la primera tentativa de redistribución si el envío inicial falló.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'FEPRIRENV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del Primer Re Envio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'FEPRIRENV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'FEPRIRENV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: si se realizó el segundo reenvío de correo. Flag de reintentos de comunicación por correo electrónico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'RESEGRENV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Realiza el Segundo Re Envio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'RESEGRENV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'RESEGRENV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: si se realizó el primer reenvío de correo. Flag de primer intento de redistribución cuando hay fallos de entrega.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'REPRIRENV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Realizo el primer Re Envio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'REPRIRENV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'REPRIRENV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del envío inicial de correo. Timestamp del primer intento de distribución del mensaje de comunicación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'FECHAENVI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del Envio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'FECHAENVI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'FECHAENVI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuerpo o contenido del mensaje de correo (hasta 5000 caracteres). Texto de la comunicación clínica, administrativa o de gestión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'MENSAJECO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mensaje del correo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'MENSAJECO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'MENSAJECO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Asunto o línea de referencia del correo (hasta 100 caracteres). Encabezado temático del mensaje para identificación rápida de urgencias, inconsistencias o solicitudes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'ASUNTOCOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Asunto del correo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'ASUNTOCOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'ASUNTOCOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección de correo electrónico destinataria (hasta 60 caracteres). Cuenta de email PII donde se envía la comunicación; requiere ofuscación en auditorías.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'CORREOENV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Correo al que se va a enviar el correo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'CORREOENV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'CORREOENV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la entidad parametrizada (autonumérico). Clave foránea FK que vincula configuraciones o maestros de negocio relacionados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'CONPARENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la entidad parametrizada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'CONPARENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'CONPARENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (10 caracteres). Identificador del sitio o unidad funcional donde se origina la comunicación clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de registro (PK, autoincremental). Consecutivo principal de cada correo/comunicación registrado en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de correos electrónicos enviados a pacientes o contactos desde el sistema, asociados a informes o reportes generados en un centro de atención. Almacena el historial de envíos, reintentos y confirmaciones de entrega.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047';
