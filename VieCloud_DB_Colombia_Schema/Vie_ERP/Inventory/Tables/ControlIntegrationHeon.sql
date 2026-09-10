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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que autorizó y confirmó la integración HEON: validación manual o sistema automatizado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de confirmación ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se confirmó la recepción y procesamiento del envío por parte de la entidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Confirmación del envío', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la transacción (TINYINT): 1=Correcto/Exitoso, 2=Error en item(s) de la solicitud; indica validez de integración HEON', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 Correcto - 2 Error en algun item', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo (VARCHAR 250) de la entidad/IPS/farmacia que originó el registro de control', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la entidad quien genera el registro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único (VARCHAR 20) de la entidad/organización de salud: farmacia, hospital, centro de atención', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'EntityCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la entidad quien genera el registro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'EntityCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'EntityCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico (INT, FK) de la entidad/IPS/farmacia que genera y confirma el registro de integración', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la entidad quien genera el registro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Documento JSON con respuesta del sistema HEON: confirmación, validaciones, errores o rechazo de la transacción logística', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'JsonRespuestaHEON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'respuesta HEON', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'JsonRespuestaHEON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'JsonRespuestaHEON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Documento JSON con detalles de la entrega efectiva: cantidad, lotes, fechas, conformidad de medicamentos/insumos recibidos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'JsonEntrega';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Entrega', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'JsonEntrega';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'JsonEntrega';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Documento JSON que contiene la solicitud original de medicamentos/insumos enviada a HEON por la entidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'JsonSolicitud';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Solicitud', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'JsonSolicitud';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'JsonSolicitud';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificador del centro de atención/IPS (VARCHAR 20) origen del envío de medicamentos e insumos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'CareCenterCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id o código del centro de atención', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'CareCenterCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'CareCenterCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de despacho/ámbito de atención (INT): 1=Ambulatorio, 2=Hospitalario; define contexto de entrega de medicamentos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'OfficeType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Despacho:  1. Ambito Ambulatorio  2. Ambito Hospitalario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'OfficeType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'OfficeType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del operador logístico (INT, FK) encargado del transporte y distribución de medicamentos/insumos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'LogisticOperator';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Operador logístico usado por el servicio', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'LogisticOperator';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'LogisticOperator';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (INT) de cada registro de control de integración HEON en el sistema de inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de integración con el sistema externo HEON para operaciones logísticas de inventario: guarda los mensajes JSON de solicitud, entrega y respuesta intercambiados con HEON, junto con el estado y la confirmación de cada transacción por centro de atención.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeon';
