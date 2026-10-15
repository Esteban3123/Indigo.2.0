CREATE TABLE [Inventory].[InventorySupplie] (
    [Id]                          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                        VARCHAR (20)  NOT NULL,
    [SupplieName]                 VARCHAR (100) NOT NULL,
    [RiskLevelId]                 INT           NOT NULL,
    [PBSProduct]                  BIT           NOT NULL,
    [SupplieStatus]               BIT           NOT NULL,
    [CreationUser]                VARCHAR (20)  NOT NULL,
    [CreationDate]                DATETIME      NOT NULL,
    [ModificationUser]            VARCHAR (20)  NULL,
    [ModificationDate]            DATETIME      NULL,
    [TimeStamp]                   ROWVERSION    NOT NULL,
    [JustificationOfInputs]       BIT           CONSTRAINT [DF_InventorySupplie_JustificationOfInputs] DEFAULT ((0)) NOT NULL,
    [OsteosynthesisMaterial]      BIT           CONSTRAINT [DF_InventorySupplie_OsteosynthesisMaterial] DEFAULT ((0)) NOT NULL,
    [Consumption]                 BIT           CONSTRAINT [DF_InventorySupplie_Consumption] DEFAULT ((0)) NOT NULL,
    [OptometryDevice]             BIT           CONSTRAINT [DF_InventorySupplie_OptometryDevice] DEFAULT ((0)) NOT NULL,
    [MedicalDevice]               BIT           CONSTRAINT [DF__Inventory__Medic__68CF922A] DEFAULT ((0)) NOT NULL,
    [IsParenteralNutritionSupply] BIT           CONSTRAINT [DF_InventorySupplie_IsParenteralNutritionSupply] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_InventorySupplie] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InventorySupplie_InventoryRiskLevel] FOREIGN KEY ([RiskLevelId]) REFERENCES [Inventory].[InventoryRiskLevel] ([Id])
);


GO
ALTER TABLE [Inventory].[InventorySupplie] NOCHECK CONSTRAINT [FK_InventorySupplie_InventoryRiskLevel];


GO
CREATE NONCLUSTERED INDEX [IX_InventorySupplie_RiskLevelId]
    ON [Inventory].[InventorySupplie]([RiskLevelId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_InventorySupplie_Code_SupplieName]
    ON [Inventory].[InventorySupplie]([Code] ASC)
    INCLUDE([SupplieName]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'define si es dispositivo medico   ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'MedicalDevice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite identificar si funciona como dispositivo de optometría.  ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'OptometryDevice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite saber si es de consumo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'Consumption';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite saber si es material osteosintesis', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'OsteosynthesisMaterial';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Exige justificacion de insumos / dispositivos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'JustificationOfInputs';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado:  1. Activo  0. Inactivo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'SupplieStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si o no el insumo esta en el plan de beneficios de salud', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'PBSProduct';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de nivel de riesgo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'RiskLevelId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de insumo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'SupplieName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de insumo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'Id';

