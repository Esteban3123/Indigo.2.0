CREATE TABLE [Inventory].[InventoryControlService] (
    [Id]                 INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [LogisticOperator]   INT           NOT NULL,
    [OfficeType]         INT           NOT NULL,
    [MedicalOrderRecipe] VARCHAR (20)  NOT NULL,
    [EntityId]           INT           NOT NULL,
    [EntityCode]         VARCHAR (20)  NOT NULL,
    [EntityName]         VARCHAR (250) NOT NULL,
    [jsonData]           VARCHAR (MAX) NOT NULL,
    [Status]             TINYINT       NOT NULL,
    [CreationDate]       DATETIME      NOT NULL,
    [ConfirmationDate]   DATETIME      NULL,
    [jsonResponseData]   VARCHAR (MAX) NULL,
    CONSTRAINT [PK_InventoryControlService] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'json Respuesta de Datos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryControlService', @level2type = N'COLUMN', @level2name = N'jsonResponseData';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Confirmación del envío', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryControlService', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryControlService', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del registro:  0 - Registrado  1 o Más - Indica que el registro ya fue enviado y se encuentra confirmado, además este número corresponderá al Id de confirmación del registro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryControlService', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Información que se envia al servicio', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryControlService', @level2type = N'COLUMN', @level2name = N'jsonData';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la entidad quien genera el registro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryControlService', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la entidad quien genera el registro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryControlService', @level2type = N'COLUMN', @level2name = N'EntityCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la entidad quien genera el registro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryControlService', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de la Orden Medica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryControlService', @level2type = N'COLUMN', @level2name = N'MedicalOrderRecipe';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Despacho:  1. Ambito Ambulatorio  2. Ambito Hospitalario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryControlService', @level2type = N'COLUMN', @level2name = N'OfficeType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Operador logístico usado por el servicio', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryControlService', @level2type = N'COLUMN', @level2name = N'LogisticOperator';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Registro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryControlService', @level2type = N'COLUMN', @level2name = N'Id';

