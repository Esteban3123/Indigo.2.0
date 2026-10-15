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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de confirmación ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Confirmación del envío', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 Correcto - 2 Error en algun item', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la entidad quien genera el registro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la entidad quien genera el registro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'EntityCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la entidad quien genera el registro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'respuesta HEON', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'JsonRespuestaHEON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Entrega', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'JsonEntrega';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Solicitud', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'JsonSolicitud';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id o código del centro de atención', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'CareCenterCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Despacho:  1. Ambito Ambulatorio  2. Ambito Hospitalario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'OfficeType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Operador logístico usado por el servicio', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'LogisticOperator';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'Id';

