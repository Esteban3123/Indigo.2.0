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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLog', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del mensaje 1.Registrado 2.Válido 3.Erróneo ', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLog', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Origen del mensaje', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLog', @level2type = N'COLUMN', @level2name = N'Source';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de encolamiento', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLog', @level2type = N'COLUMN', @level2name = N'EnqueuedTimeUtc';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del cuerpo interno del mensaje', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLog', @level2type = N'COLUMN', @level2name = N'InternalBodyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de reintentos de reenvio', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLog', @level2type = N'COLUMN', @level2name = N'RetryCount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador/código único del mensaje', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLog', @level2type = N'COLUMN', @level2name = N'MessageId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del transmisor del mensaje', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLog', @level2type = N'COLUMN', @level2name = N'Transmitter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'IntegrationLog', @level2type = N'COLUMN', @level2name = N'Id';

