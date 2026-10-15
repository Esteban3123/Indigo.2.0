CREATE TABLE [Inventory].[ControlIntegrationHeonDetail] (
    [Id]                       INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ControlIntegrationHeonId] INT           NOT NULL,
    [MedicalOrderRecipe]       VARCHAR (20)  NOT NULL,
    [ProductCodeHeon]          VARCHAR (20)  NOT NULL,
    [TotalQuantity]            INT           NOT NULL,
    [Status]                   TINYINT       NOT NULL,
    [Message]                  VARCHAR (MAX) NULL,
    CONSTRAINT [PK_ControlIntegrationHeonDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ControlIntegrationHeonDetail_ControlIntegrationHeon] FOREIGN KEY ([ControlIntegrationHeonId]) REFERENCES [Inventory].[ControlIntegrationHeon] ([Id])
);


GO
ALTER TABLE [Inventory].[ControlIntegrationHeonDetail] NOCHECK CONSTRAINT [FK_ControlIntegrationHeonDetail_ControlIntegrationHeon];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mensaje', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeonDetail', @level2type = N'COLUMN', @level2name = N'Message';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 Correcto - 2 Error HEON', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeonDetail', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad total', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeonDetail', @level2type = N'COLUMN', @level2name = N'TotalQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de producto Heon', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeonDetail', @level2type = N'COLUMN', @level2name = N'ProductCodeHeon';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de la Orden Medica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeonDetail', @level2type = N'COLUMN', @level2name = N'MedicalOrderRecipe';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion de integracion de controles Heon', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeonDetail', @level2type = N'COLUMN', @level2name = N'ControlIntegrationHeonId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeonDetail', @level2type = N'COLUMN', @level2name = N'Id';

