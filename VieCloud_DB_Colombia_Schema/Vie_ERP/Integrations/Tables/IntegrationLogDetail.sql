CREATE TABLE [Integrations].[IntegrationLogDetail] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [IntegrationLogId] INT           NOT NULL,
    [MessageId]        VARCHAR (50)  NOT NULL,
    [Transmitter]      VARCHAR (20)  NOT NULL,
    [InternalBodyId]   VARCHAR (50)  NULL,
    [EnqueuedTimeUtc]  DATETIME      NOT NULL,
    [Source]           VARCHAR (50)  NOT NULL,
    [Status]           TINYINT       NOT NULL,
    [CreationDate]     DATETIME      DEFAULT (getdate()) NULL,
    [LogMessage]       VARCHAR (MAX) NULL,
    [CosmosDetailId]   VARCHAR (36)  NOT NULL,
    CONSTRAINT [PK_IntegrationLogDetail_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_IntegrationLogDetail_IntegrationLog] FOREIGN KEY ([IntegrationLogId]) REFERENCES [Integrations].[IntegrationLog] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mensaje de respuesta o error adjunto al log de integración; texto descriptivo de la transacción (VARCHAR MAX, puede contener detalles técnicos o de negocio de la sincronización)', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLogDetail', @level2type = N'COLUMN', @level2name = N'LogMessage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mensaje de respuesta adjunto al log', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLogDetail', @level2type = N'COLUMN', @level2name = N'LogMessage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLogDetail', @level2type = N'COLUMN', @level2name = N'LogMessage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de detalle en la base de datos (DATETIME, generado automáticamente con getdate())', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLogDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLogDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLogDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del mensaje de integración: 1=Fallido, 2=Válido; indica si la transacción fue exitosa o rechazada', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLogDetail', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del mensaje 1.Fallido 2.Valido ', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLogDetail', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLogDetail', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Origen o sistema emisor del mensaje (ERP, EHR, RIPS, interfaz externa); identifica de dónde proviene la integración', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLogDetail', @level2type = N'COLUMN', @level2name = N'Source';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Origen del mensaje', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLogDetail', @level2type = N'COLUMN', @level2name = N'Source';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLogDetail', @level2type = N'COLUMN', @level2name = N'Source';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora UTC en que el mensaje fue encolado para procesamiento asíncrono', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLogDetail', @level2type = N'COLUMN', @level2name = N'EnqueuedTimeUtc';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora de encolamiento', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLogDetail', @level2type = N'COLUMN', @level2name = N'EnqueuedTimeUtc';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLogDetail', @level2type = N'COLUMN', @level2name = N'EnqueuedTimeUtc';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del cuerpo/contenido interno del mensaje; referencia a la estructura de datos de la transacción (VARCHAR 50, puede ser NULL)', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLogDetail', @level2type = N'COLUMN', @level2name = N'InternalBodyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del cuerpo interno del mensaje', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLogDetail', @level2type = N'COLUMN', @level2name = N'InternalBodyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLogDetail', @level2type = N'COLUMN', @level2name = N'InternalBodyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o código del sistema/aplicación transmisor que envía el mensaje (ej: interfaz HL7, API, servicio web)', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLogDetail', @level2type = N'COLUMN', @level2name = N'Transmitter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del transmisor del mensaje', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLogDetail', @level2type = N'COLUMN', @level2name = N'Transmitter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLogDetail', @level2type = N'COLUMN', @level2name = N'Transmitter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador o código único del mensaje en la integración (VARCHAR 50, clave para rastreo y reconciliación)', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLogDetail', @level2type = N'COLUMN', @level2name = N'MessageId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador/código único del mensaje', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLogDetail', @level2type = N'COLUMN', @level2name = N'MessageId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLogDetail', @level2type = N'COLUMN', @level2name = N'MessageId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera/registro principal de log de integración (FK a IntegrationLog.Id); agrupa detalles bajo un evento de integración', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLogDetail', @level2type = N'COLUMN', @level2name = N'IntegrationLogId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del mensaje principal', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLogDetail', @level2type = N'COLUMN', @level2name = N'IntegrationLogId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLogDetail', @level2type = N'COLUMN', @level2name = N'IntegrationLogId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (IDENTITY) del registro de detalle de log de integración (clave primaria)', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLogDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLogDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLogDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro detallado de cada mensaje procesado en las integraciones del sistema, incluyendo su origen, estado de procesamiento, fecha de encola y trazabilidad hacia servicios externos como Cosmos.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLogDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLogDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de detalle en el servicio externo Cosmos, usado para trazabilidad y correlación entre sistemas.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLogDetail', @level2type = N'COLUMN', @level2name = N'CosmosDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLogDetail', @level2type = N'COLUMN', @level2name = N'CosmosDetailId';
