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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del registro:  0 - Registrado  1 o Más - Indica que el registro ya fue enviado y se encuentra confirmado, además este número corresponderá al Id de confirmación del registro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryControlService', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Despacho:  1. Ambito Ambulatorio  2. Ambito Hospitalario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryControlService', @level2type = N'COLUMN', @level2name = N'OfficeType';

