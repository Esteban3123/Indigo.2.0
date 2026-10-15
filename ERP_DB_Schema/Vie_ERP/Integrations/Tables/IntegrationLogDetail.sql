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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mensaje de respuesta adjunto al log', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLogDetail', @level2type = N'COLUMN', @level2name = N'LogMessage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLogDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del mensaje 1.Fallido 2.Valido ', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLogDetail', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Origen del mensaje', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLogDetail', @level2type = N'COLUMN', @level2name = N'Source';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de encolamiento', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLogDetail', @level2type = N'COLUMN', @level2name = N'EnqueuedTimeUtc';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del cuerpo interno del mensaje', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLogDetail', @level2type = N'COLUMN', @level2name = N'InternalBodyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del transmisor del mensaje', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLogDetail', @level2type = N'COLUMN', @level2name = N'Transmitter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador/código único del mensaje', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLogDetail', @level2type = N'COLUMN', @level2name = N'MessageId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del mensaje principal', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLogDetail', @level2type = N'COLUMN', @level2name = N'IntegrationLogId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLogDetail', @level2type = N'COLUMN', @level2name = N'Id';

