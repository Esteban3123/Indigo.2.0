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
CREATE NONCLUSTERED INDEX [IX_InventorySupplie_Code_SupplieName]
    ON [Inventory].[InventorySupplie]([Code] ASC)
    INCLUDE([SupplieName]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'define si es dispositivo medico   ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'MedicalDevice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite identificar si funciona como dispositivo de optometría.  ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'OptometryDevice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado:  1. Activo  0. Inactivo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventorySupplie', @level2type = N'COLUMN', @level2name = N'SupplieStatus';

