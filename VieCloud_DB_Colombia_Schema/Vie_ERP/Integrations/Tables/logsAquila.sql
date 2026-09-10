CREATE TABLE [Integrations].[logsAquila] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [CreationDate] DATETIME       NOT NULL,
    [Transmitter]  VARCHAR (50)   NOT NULL,
    [Action]       VARCHAR (50)   NOT NULL,
    [MessajeJSON]  NVARCHAR (MAX) NULL,
    [LogMessage]   VARCHAR (MAX)  NULL,
    [Entity]       VARCHAR (50)   NULL,
    [IdOrder]      VARCHAR (50)   NULL,
    CONSTRAINT [PK_logsAquila] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico de la orden/solicitud de integración entre Indigo y Aquila (VARCHAR(50), clave de referencia a orden de atención, factura o procedimiento)', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'logsAquila', @level2type = N'COLUMN', @level2name = N'IdOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumérico', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'logsAquila', @level2type = N'COLUMN', @level2name = N'IdOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'logsAquila', @level2type = N'COLUMN', @level2name = N'IdOrder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de tabla origen en base de datos (AMBORDIMA, HCORDIMAG u otras entidades del ERP/EHR) de donde se originó el registro sincronizado', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'logsAquila', @level2type = N'COLUMN', @level2name = N'Entity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tabla en base de datos (AMBORDIMA-HCORDIMAG)', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'logsAquila', @level2type = N'COLUMN', @level2name = N'Entity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'logsAquila', @level2type = N'COLUMN', @level2name = N'Entity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mensaje de resultado obtenido en la integración: descripción legible del evento, respuesta o error generado durante la sincronización entre sistemas', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'logsAquila', @level2type = N'COLUMN', @level2name = N'LogMessage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mensaje Obtenido', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'logsAquila', @level2type = N'COLUMN', @level2name = N'LogMessage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'logsAquila', @level2type = N'COLUMN', @level2name = N'LogMessage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuerpo completo del mensaje en formato JSON intercambiado entre Indigo y Aquila (carga útil de solicitud o respuesta, puede contener datos sensibles de paciente/facturación)', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'logsAquila', @level2type = N'COLUMN', @level2name = N'MessajeJSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'JSON del mensaje ', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'logsAquila', @level2type = N'COLUMN', @level2name = N'MessajeJSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'logsAquila', @level2type = N'COLUMN', @level2name = N'MessajeJSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de acción ejecutada en la integración: VieSendOrder (envío de orden), VieCancelOrder (cancelación), VieUpdatePatient (actualización paciente), AquilaResult (resultado Aquila), AquilAdendum (adenda), AquilaCancelOrder (cancelación Aquila), AquilaChangeStatus (cambio estado)', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'logsAquila', @level2type = N'COLUMN', @level2name = N'Action';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la accion.

VieSendOrder
VieCancelOrder
VieUpdatePatient

AquilaResult
AquilaAdemdum
AquilaCancelOrder
AquilaChangeStatus', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'logsAquila', @level2type = N'COLUMN', @level2name = N'Action';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'logsAquila', @level2type = N'COLUMN', @level2name = N'Action';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Componente transmisor del evento: sistema Indigo o pasarela Aquila que originó o procesó la integración', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'logsAquila', @level2type = N'COLUMN', @level2name = N'Transmitter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indigo - Aquila', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'logsAquila', @level2type = N'COLUMN', @level2name = N'Transmitter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'logsAquila', @level2type = N'COLUMN', @level2name = N'Transmitter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Timestamp de registro del evento en la bitácora de integración (DATETIME, momento exacto de creación del log)', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'logsAquila', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de registro', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'logsAquila', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'logsAquila', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de logs de integración con el sistema Águila. Guarda el historial de mensajes, acciones y eventos intercambiados entre Indigo y Águila, incluyendo el contenido JSON transmitido y los resultados de cada operación.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'logsAquila';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'logsAquila';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental del registro de log.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'logsAquila', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'logsAquila', @level2type = N'COLUMN', @level2name = N'Id';
