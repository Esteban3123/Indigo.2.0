CREATE TABLE [Inventory].[ControlIntegrationHeon] (
    [Id]                INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [LogisticOperator]  INT           NOT NULL,
    [OfficeType]        INT           NOT NULL,
    [CareCenterCode]    VARCHAR (20)  NOT NULL,
    [JsonSolicitud]     VARCHAR (MAX) NOT NULL,
    [JsonEntrega]       VARCHAR (MAX) NOT NULL,
    [JsonRespuestaHEON] VARCHAR (MAX) NOT NULL,
    [EntityId]          INT           NOT NULL,
    [EntityCode]        VARCHAR (20)  NOT NULL,
    [EntityName]        VARCHAR (250) NOT NULL,
    [Status]            TINYINT       NOT NULL,
    [ConfirmationDate]  DATETIME      NOT NULL,
    [ConfirmationUser]  VARCHAR (20)  NOT NULL,
    CONSTRAINT [PK_ControlIntegrationHeon] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Despacho:  1. Ambito Ambulatorio  2. Ambito Hospitalario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'OfficeType';

