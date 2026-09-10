CREATE TABLE [Integrations].[IntegrationLog] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [Transmitter]     VARCHAR (100) NULL,
    [MessageId]       VARCHAR (50)  NOT NULL,
    [RetryCount]      INT           DEFAULT ((0)) NULL,
    [InternalBodyId]  VARCHAR (50)  NULL,
    [EnqueuedTimeUtc] DATETIME      NOT NULL,
    [Source]          VARCHAR (50)  NOT NULL,
    [Status]          TINYINT       NOT NULL,
    [CreationDate]    DATETIME      DEFAULT (getdate()) NULL,
    [CosmosId]        VARCHAR (36)  NOT NULL,
    CONSTRAINT [PK_IntegrationLog_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de integración; timestamp del sistema (DATETIME, default getdate()); útil para auditoría y trazabilidad de mensajes en flujos HL7/FHIR.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLog', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLog', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLog', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado actual del mensaje de integración: 1=Registrado, 2=Válido, 3=Erróneo (TINYINT); refleja el resultado de validación y procesamiento en el bus de mensajería.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLog', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del mensaje 1.Registrado 2.Válido 3.Erróneo ', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLog', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLog', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Origen o sistema emisor del mensaje (VARCHAR 50, obligatorio); identifica la aplicación, módulo o interfaz que originó la transmisión (ej: laboratorio, farmacia, facturación, RIPS).', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLog', @level2type = N'COLUMN', @level2name = N'Source';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Origen del mensaje', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLog', @level2type = N'COLUMN', @level2name = N'Source';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLog', @level2type = N'COLUMN', @level2name = N'Source';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora UTC en que el mensaje fue encolado para procesamiento (DATETIME, obligatorio); marca el instante de entrada en la cola de integración asíncrona.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLog', @level2type = N'COLUMN', @level2name = N'EnqueuedTimeUtc';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora de encolamiento', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLog', @level2type = N'COLUMN', @level2name = N'EnqueuedTimeUtc';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLog', @level2type = N'COLUMN', @level2name = N'EnqueuedTimeUtc';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del cuerpo/payload interno del mensaje (VARCHAR 50); referencia al contenido HL7, XML o JSON que acompaña la transmisión; útil para correlacionar con tablas de detalle.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLog', @level2type = N'COLUMN', @level2name = N'InternalBodyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del cuerpo interno del mensaje', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLog', @level2type = N'COLUMN', @level2name = N'InternalBodyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLog', @level2type = N'COLUMN', @level2name = N'InternalBodyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de reintentos de reenvío ejecutados en caso de fallo (INT, default 0); rastrea intentos de recuperación automática de mensajes fallidos.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLog', @level2type = N'COLUMN', @level2name = N'RetryCount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de reintentos de reenvio', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLog', @level2type = N'COLUMN', @level2name = N'RetryCount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLog', @level2type = N'COLUMN', @level2name = N'RetryCount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único global del mensaje (VARCHAR 50, obligatorio); código de referencia para rastreo, correlación y deduplicación en flujos de integración y auditoría regulatoria.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLog', @level2type = N'COLUMN', @level2name = N'MessageId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador/código único del mensaje', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLog', @level2type = N'COLUMN', @level2name = N'MessageId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLog', @level2type = N'COLUMN', @level2name = N'MessageId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del sistema o proceso transmisor del mensaje (VARCHAR 100, opcional); entidad responsable del envío (ej: servidor HL7, API cliente, programa batch, interfaz externa).', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLog', @level2type = N'COLUMN', @level2name = N'Transmitter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del transmisor del mensaje', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLog', @level2type = N'COLUMN', @level2name = N'Transmitter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLog', @level2type = N'COLUMN', @level2name = N'Transmitter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria del registro de log de integración (INT IDENTITY 1,1); identificador secuencial único para cada evento de transmisión capturado.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLog', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLog', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLog', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de mensajes de integración entre sistemas externos y el ERP/EHR. Guarda el estado, origen y trazabilidad de cada mensaje intercambiado, incluyendo reintentos y fechas de procesamiento.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLog';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLog';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del documento en Azure Cosmos DB asociado a este mensaje de integración.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLog', @level2type = N'COLUMN', @level2name = N'CosmosId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLog', @level2type = N'COLUMN', @level2name = N'CosmosId';
