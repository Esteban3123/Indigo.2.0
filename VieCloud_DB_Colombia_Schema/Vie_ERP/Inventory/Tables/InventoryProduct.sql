CREATE TABLE [Inventory].[InventoryProduct] (
    [Id]                                INT             IDENTITY (1, 1) NOT NULL,
    [Code]                              VARCHAR (20)    NOT NULL,
    [Name]                              VARCHAR (400)   NOT NULL,
    [ProductTypeId]                     INT             NOT NULL,
    [ATCId]                             INT             NULL,
    [CodeCUM]                           VARCHAR (20)    NULL,
    [CodeAlternative]                   VARCHAR (30)    NULL,
    [CodeAlternativeTwo]                VARCHAR (20)    NULL,
    [Description]                       VARCHAR (MAX)   NULL,
    [ProductGroupId]                    INT             CONSTRAINT [DF_InventoryProduct_ProductGroupId] DEFAULT ((2)) NULL,
    [ProductSubGroupId]                 INT             CONSTRAINT [DF_InventoryProduct_ProductSubGroupId] DEFAULT ((1)) NULL,
    [MeasurementUnitId]                 INT             NULL,
    [PackagingUnitId]                   INT             NOT NULL,
    [ManufacturerId]                    INT             NULL,
    [IVAId]                             INT             NULL,
    [Presentation]                      VARCHAR (200)   NULL,
    [CodeSICE]                          VARCHAR (20)    NULL,
    [HandlesSerial]                     BIT             NULL,
    [HandlesHealthRegistration]         BIT             NULL,
    [HealthRegistration]                VARCHAR (30)    NULL,
    [ExpirationDate]                    DATETIME        NULL,
    [BillingGroupId]                    INT             NULL,
    [ProductControl]                    BIT             NULL,
    [ProductWithPriceControl]           BIT             NULL,
    [POSProduct]                        BIT             NULL,
    [AuthorizationByOrderNumber]        INT             NULL,
    [ExpirationDay]                     INT             NULL,
    [MaximumControlPeriod]              BIT             NULL,
    [ControlDays]                       INT             NULL,
    [ControlOrderQuantity]              BIT             NULL,
    [ProductOrderAmount]                INT             NULL,
    [LastPurchase]                      DATETIME        NULL,
    [LastSale]                          DATETIME        NULL,
    [ProductOrigin]                     TINYINT         NULL,
    [MinimumStock]                      INT             NULL,
    [MaximumStock]                      INT             NULL,
    [CommissionPercentage]              NUMERIC (5, 2)  NULL,
    [RepositionPoint]                   INT             NULL,
    [ResetTime]                         INT             NULL,
    [CurrencyType]                      TINYINT         NULL,
    [ProductCost]                       DECIMAL (18, 2) NOT NULL,
    [FinalProductCost]                  NUMERIC (18, 2) NULL,
    [SellingPrice]                      NUMERIC (18, 2) NULL,
    [AllPOSPathologies]                 BIT             NULL,
    [Status]                            BIT             NOT NULL,
    [CreationUser]                      VARCHAR (20)    CONSTRAINT [DF_Product_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]                      DATETIME        CONSTRAINT [DF_Product_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]                  VARCHAR (20)    NULL,
    [ModificationDate]                  DATETIME        NULL,
    [TimeStamp]                         ROWVERSION      NOT NULL,
    [BillingGroupNoPosId]               INT             NULL,
    [ControlCostPercentage]             NUMERIC (5, 2)  NULL,
    [InventoryRiskLevelId]              INT             NULL,
    [SerialNumber]                      VARCHAR (40)    NULL,
    [DriveUnit]                         INT             NULL,
    [MinimumTemperature]                INT             NULL,
    [MaximumTemperature]                INT             NULL,
    [SanitaryRegistration]              INT             NULL,
    [Consumption]                       BIT             NULL,
    [JustificationSuppliesDispositives] BIT             NULL,
    [OsteosynthesisMaterial]            BIT             NULL,
    [Abbreviation]                      VARCHAR (20)    NULL,
    [SupplieId]                         INT             NULL,
    [IUM]                               VARCHAR (15)    NULL,
    [Storage]                           INT             NULL,
    [TaxedProduct]                      BIT             NOT NULL,
    [Osmolarity]                        DECIMAL (18, 2) CONSTRAINT [DF__Inventory__Osmol__44E66C18] DEFAULT ((0)) NOT NULL,
    [LiquidateSalesTaxes]               BIT             CONSTRAINT [DF__Inventory__Liqui__3BBCF491] DEFAULT ((0)) NOT NULL,
    [SismedReport]                      BIT             CONSTRAINT [DF__Inventory__Sisme__5670EACD] DEFAULT ((0)) NOT NULL,
    [DairyComponent]                    BIT             CONSTRAINT [DF_InventoryProduct_DairyComponent] DEFAULT ((0)) NOT NULL,
    [DairyComponentType]                INT             CONSTRAINT [DF_InventoryProduct_DairyComponentType] DEFAULT (NULL) NULL,
    [MedicationTypeId]                  INT             NULL,
    [WeightParenteralNutritionSupply]   DECIMAL (18, 2) DEFAULT ((0)) NULL,
    CONSTRAINT [PK_InventoryProduct__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InventoryProduct_BillingGroup] FOREIGN KEY ([BillingGroupNoPosId]) REFERENCES [Billing].[BillingGroup] ([Id]),
    CONSTRAINT [FK_InventoryProduct_InventoryMeasurementUnit] FOREIGN KEY ([MeasurementUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id]),
    CONSTRAINT [FK_InventoryProduct_InventorySupplie] FOREIGN KEY ([SupplieId]) REFERENCES [Inventory].[InventorySupplie] ([Id]),
    CONSTRAINT [FK_InventoryProduct_StorageTemperature] FOREIGN KEY ([Storage]) REFERENCES [Inventory].[StorageTemperature] ([Id]),
    CONSTRAINT [FK_Product_ATC] FOREIGN KEY ([ATCId]) REFERENCES [Inventory].[ATC] ([Id]),
    CONSTRAINT [FK_Product_GeneralLedgerIVA] FOREIGN KEY ([IVAId]) REFERENCES [GeneralLedger].[GeneralLedgerIVA] ([Id]),
    CONSTRAINT [FK_Product_InventoryRiskLevel] FOREIGN KEY ([InventoryRiskLevelId]) REFERENCES [Inventory].[InventoryRiskLevel] ([Id]),
    CONSTRAINT [FK_Product_Manufacturer] FOREIGN KEY ([ManufacturerId]) REFERENCES [Inventory].[Manufacturer] ([Id]),
    CONSTRAINT [FK_Product_MedicationTypeId] FOREIGN KEY ([MedicationTypeId]) REFERENCES [Inventory].[MedicationType] ([Id]),
    CONSTRAINT [FK_Product_PackagingUnit] FOREIGN KEY ([PackagingUnitId]) REFERENCES [Inventory].[PackagingUnit] ([Id]),
    CONSTRAINT [FK_Product_ProductGroup] FOREIGN KEY ([ProductGroupId]) REFERENCES [Inventory].[ProductGroup] ([Id]),
    CONSTRAINT [FK_Product_ProductSubGroup] FOREIGN KEY ([ProductSubGroupId]) REFERENCES [Inventory].[ProductSubGroup] ([Id]),
    CONSTRAINT [FK_Product_ProductType] FOREIGN KEY ([ProductTypeId]) REFERENCES [Inventory].[ProductType] ([Id])
);


GO
ALTER TABLE [Inventory].[InventoryProduct] NOCHECK CONSTRAINT [FK_InventoryProduct_InventorySupplie];


GO
ALTER TABLE [Inventory].[InventoryProduct] NOCHECK CONSTRAINT [FK_Product_ProductGroup];


GO
ALTER TABLE [Inventory].[InventoryProduct] NOCHECK CONSTRAINT [FK_Product_ProductType];




GO



GO



GO
ALTER TABLE [Inventory].[InventoryProduct] NOCHECK CONSTRAINT [FK_InventoryProduct_InventorySupplie];


GO



GO



GO



GO



GO



GO



GO
ALTER TABLE [Inventory].[InventoryProduct] NOCHECK CONSTRAINT [FK_Product_ProductGroup];


GO



GO
ALTER TABLE [Inventory].[InventoryProduct] NOCHECK CONSTRAINT [FK_Product_ProductType];


GO
CREATE NONCLUSTERED INDEX [IDX_InventoryProduct_Code_Name]
    ON [Inventory].[InventoryProduct]([Code] ASC, [Name] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_InventoryProduct_SupplieId]
    ON [Inventory].[InventoryProduct]([SupplieId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_InventoryProduct__Id__INC__Code__Name]
    ON [Inventory].[InventoryProduct]([Id] ASC)
    INCLUDE([Code], [Name]);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_InventoryProduct]
    ON [Inventory].[InventoryProduct]([Code] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_InventoryProduct__ATCId__INC__CodeCUM__Id]
    ON [Inventory].[InventoryProduct]([ATCId] ASC)
    INCLUDE([Id], [CodeCUM]);


GO
-- =============================================
-- Author:		Andres Alarcon
-- Create date: 2023-07-19
-- Description:	Se valida que el producto que no sea producto gravable no se le permita guardar un dato de IVA
-- =============================================

CREATE TRIGGER [Inventory].[tgg_ValidateTaxedProduct]
   ON  [Inventory].[InventoryProduct]
   AFTER INSERT, UPDATE
AS 
BEGIN
	SET NOCOUNT ON;

	DECLARE @count INT = 0
    
	IF EXISTS (SELECT * FROM INSERTED)
	BEGIN
		
		SELECT @count = i.Id
		from INSERTED i
		WHERE i.TaxedProduct = 0 AND (i.IVAId > 0 or i.IVAId IS NOT NULL)

	END


	IF @count > 0 
	BEGIN
		THROW 51000, 'Error generado por control desde trigger. No puede tener IVA parametrizado si el producto no es gravado', 1
	END
END
GO
CREATE TRIGGER [Inventory].[TriggerValidateDataATC]
   ON [Inventory].[InventoryProduct]
   AFTER INSERT,UPDATE
AS 
BEGIN
	if (select count(*) from inserted inner join Inventory.ProductType pt on pt.Id = inserted.ProductTypeId where pt.Class = 2 and inserted.ATCId is null) > 0 begin
			THROW 51000, 'Error El ATC no puede ser nulo', 1
	end
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de medicamento (INT, FK). Clasifica la categoría terapéutica o farmacológica del medicamento en inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'MedicationTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo de medicamento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'MedicationTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'MedicationTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de componente lácteo (INT, 1=Fórmula láctea/especial, 2=Leche materna, 3=Insumo). Solo se llena si DairyComponent=1.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'DairyComponentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Define el tipo de componente lácteo: 1 - Fórmula láctea/ especial, 2 - Leche materna, 3 - Insumo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'DairyComponentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'DairyComponentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de componente lácteo (BIT, 1=Sí, 0=No). Define si el producto es insumo, fórmula o leche materna para nutrición.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'DairyComponent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Define si es o no un componente lácteo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'DairyComponent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'DairyComponent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte Sismed (BIT, 1=Sí, 0=No). Indica si el medicamento debe reportarse al Sistema de Información de Medicamentos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'SismedReport';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Informe Sismed (1 - Si, 0 - No)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'SismedReport';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'SismedReport';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Liquidación de IVA en ventas salud (BIT, 1=Sí, 0=No). Determina si se liquida impuesto en transacciones sanitarias.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'LiquidateSalesTaxes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Liquida iva en ventas Salud- 1: true, 0: false', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'LiquidateSalesTaxes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'LiquidateSalesTaxes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Osmolaridad del producto (DECIMAL 18,2, mOsm/L). Parámetro físicoquímico de soluciones parenterales, nutrición enteral.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'Osmolarity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Osmolaridad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'Osmolarity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'Osmolarity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Producto gravado (BIT, 1=Sí, 0=No). Indica si aplica imposición tributaria (IVA) sobre el artículo.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'TaxedProduct';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Producto gravado (1 - Si, 0 - No)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'TaxedProduct';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'TaxedProduct';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Condición de almacenamiento (INT). Rango: 1=Ambiente 20-25°C (15-30°C), 2=Controlada 20-25°C, 3=Frío 8-15°C, 4=Refrigeración 2-8°C, 5=Congelación -25 a -10°C.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'Storage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1. Ambiente: 20 ° C - 25 ° C (Permitida 15 ° C y 30 ° C)  2..Ambiente controlada: 20 ° C - 25 ° C  3. En frío: 8 ° C - 15 ° C  4. Refrigerador: 2 ° C - 8 ° C  5. Congelador: -25 ° C - 10 ° C', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'Storage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'Storage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador Único de Medicamento (VARCHAR 15). Código nacional para medicamentos, relacionado con registro sanitario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'IUM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador único de medicamento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'IUM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'IUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de insumo (INT, FK a InventorySupplie). Vincula producto con catálogo de insumos, dispositivos médicos, consumibles.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'SupplieId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de insumo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'SupplieId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'SupplieId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Abreviatura del producto (VARCHAR 20). Código corto para referencia rápida en prescripciones, dispensación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'Abbreviation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Abreviatura', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'Abbreviation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'Abbreviation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Material de osteosíntesis (BIT). Flag para insumos quirúrgicos: placas, tornillos, implantes óseos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'OsteosynthesisMaterial';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Material Osteosisntesis, se asigna cuando sea item insumo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'OsteosynthesisMaterial';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'OsteosynthesisMaterial';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación insumos/dispositivos (BIT). Requiere documentación adicional si es consumible médico-quirúrgico.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'JustificationSuppliesDispositives';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificación Insumos/Dispositivos, solo se asigna cuando sea item insumo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'JustificationSuppliesDispositives';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'JustificationSuppliesDispositives';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Producto de consumo (BIT). Flag para insumos consumibles: gasas, apósitos, catéteres, jeringas.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'Consumption';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite saber si es de consumo, este campo se llena cuando es de item insumo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'Consumption';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'Consumption';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de registro sanitario (INT). Almacena estatus del registro INVIMA: activo, vencido, suspendido.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'SanitaryRegistration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que almacena el estado de registro sanitario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'SanitaryRegistration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'SanitaryRegistration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Temperatura máxima de almacenamiento (INT, °C). Límite superior de rango permitido para conservación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'MaximumTemperature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que contiene el valor de la temperatura maxima en el cual el producto puede se almacenado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'MaximumTemperature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'MaximumTemperature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Temperatura mínima de almacenamiento (INT, °C). Límite inferior de rango permitido para conservación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'MinimumTemperature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que contiene el valor de la temperatura minima en el cual el producto puede se almacenado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'MinimumTemperature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'MinimumTemperature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de descarga mínima (INT). Cantidad mínima permitida por salida/venta del producto.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'DriveUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que contiene la cantidad de descarga mínima a la que pretendemos manejar el producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'DriveUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'DriveUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de serie (VARCHAR 40). Identificación única cuando producto maneja trazabilidad serializada.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'SerialNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se especifica el número de serial cuando el producto maneja serial', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'SerialNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'SerialNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de nivel de riesgo (INT, FK a InventoryRiskLevel). Solo para medicamentos: bajo, medio, alto, psicotrópico.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'InventoryRiskLevelId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Nivel de Riesgo, solo se llena si el tipo del producto es "Item Medicamento"', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'InventoryRiskLevelId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'InventoryRiskLevelId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de control de costo (NUMERIC 5,2). Margen de variación permitido en costos de compra.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ControlCostPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de control de costo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ControlCostPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ControlCostPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de grupo facturación No-POS (INT, FK a BillingGroup). Agrupa productos de venta privada fuera POS.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'BillingGroupNoPosId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de grupo de facturación No pos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'BillingGroupNoPosId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'BillingGroupNoPosId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal de evento (TIMESTAMP). Registro automático de creación, modificación, auditoría en BD.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación (DATETIME). Última actualización del registro de producto.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de modificación (VARCHAR 20). Identidad de quien actualizó última vez el producto.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación (DATETIME, default=getdate()). Timestamp de ingreso del producto al catálogo.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de creación (VARCHAR 20, default=999). Identidad de quien registró originalmente el producto.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del producto (BIT, 1=Activo, 0=Inactivo). Activa/desactiva disponibilidad en operaciones.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Documento  1 - Activado  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Aplica todas patologías POS (BIT). Indica si producto autorizado para todas las condiciones de salud POS.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'AllPOSPathologies';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el producto aplica todas las patologias POS', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'AllPOSPathologies';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'AllPOSPathologies';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Precio de venta (NUMERIC 18,2). Tarifa al público o asegurador, neto o con IVA según regla.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'SellingPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el precio de venta del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'SellingPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'SellingPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Último costo de compra (NUMERIC 18,2). Valor unitario de la última entrada/compra registrada.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'FinalProductCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el precio del ultimo costo del producto, este es el ultimo valor con el que se compro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'FinalProductCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'FinalProductCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Costo promedio ponderado (DECIMAL 18,2). Actualiza con cada compra, base para márgenes.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ProductCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el costo promedio del producto, este se actualiza cada vez que se realiza una entrada del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ProductCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ProductCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de moneda (TINYINT, 1=Pesos COP, 2=Moneda extranjera). Define divisa de costos/precios.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'CurrencyType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de moneda  1 - Pesos(Colombia)  2  - Extranjera', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'CurrencyType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'CurrencyType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo de reposición (INT, días). Plazo de entrega del proveedor para reorden.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ResetTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tiempo de reposición', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ResetTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ResetTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Punto de reposición (INT, unidades). Stock mínimo que dispara orden de compra automática.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'RepositionPoint';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Punto de reposición', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'RepositionPoint';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'RepositionPoint';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de comisión (NUMERIC 5,2). Descuento comercial o margen para intermediarios.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'CommissionPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de comision', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'CommissionPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'CommissionPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Stock máximo (INT, unidades). Límite superior de inventario antes de detener compras.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'MaximumStock';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Stock maximo del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'MaximumStock';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'MaximumStock';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Stock mínimo (INT, unidades). Nivel crítico por debajo del cual se requiere reposición.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'MinimumStock';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Stock minimo del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'MinimumStock';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'MinimumStock';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Origen del producto (TINYINT, 1=Nacional, 2=Importado). País o región de fabricación/procedencia.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ProductOrigin';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Origen del producto  1 - Nacional  2 - Importado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ProductOrigin';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ProductOrigin';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Última venta (DATETIME). Timestamp de la transacción de venta más reciente del producto.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'LastSale';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la ultima venta del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'LastSale';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'LastSale';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Última compra (DATETIME). Timestamp de la entrada/compra más reciente del producto.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'LastPurchase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la ultima compra del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'LastPurchase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'LastPurchase';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad por orden (INT). Volumen fijo de unidades si producto tiene control por cantidad pedido.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ProductOrderAmount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cantidad de producto por orden, este campo se llena solo si el producto tiene control por cantidad de orden', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ProductOrderAmount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ProductOrderAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Control de cantidad por orden (BIT). Flag si producto requiere cantidad específica por pedido.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ControlOrderQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el producto tiene control de cantidad por orden', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ControlOrderQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ControlOrderQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días de control (INT). Período máximo de período si MaximumControlPeriod=1.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ControlDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica los dias de control que va tener, este campo se habilita solo si tiene control maximo por periodo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ControlDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ControlDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Control máximo por período (BIT). Flag si producto tiene límite de consumo/venta por período.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'MaximumControlPeriod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si tiene control maximo por periodo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'MaximumControlPeriod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'MaximumControlPeriod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días de expiración (INT). Plazo máximo de validez desde fabricación o dispensación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ExpirationDay';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica los dias de expiración del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ExpirationDay';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ExpirationDay';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de autorizaciones por pedido (INT). Cantidad de órdenes/prescripciones permitidas.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'AuthorizationByOrderNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el numero de autorizaciones por pedido', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'AuthorizationByOrderNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'AuthorizationByOrderNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Producto en POS (BIT, 1=Sí, 0=No). Indica si artículo está autorizado en Plan de Obligaciones en Salud.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'POSProduct';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el producto esta en el POS', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'POSProduct';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'POSProduct';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Control de precios (BIT). Flag si producto tiene techo de precios por regulación estatal.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ProductWithPriceControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el producto tiene control de precios', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ProductWithPriceControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ProductWithPriceControl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Producto de control (BIT). Flag si es medicamento controlado, psicotrópico, precursor químico.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ProductControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el producto es de control', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ProductControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ProductControl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador grupo facturación (INT, FK a ProductGroup, default=2). Agrupa productos por línea de negocio.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'BillingGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del grupo de facturacion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'BillingGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'BillingGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de vencimiento RS (DATETIME). Vencimiento del registro sanitario INVIMA, si aplica.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ExpirationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de vencimiento R.S del producto, solo se llena si el producto maneja registro sanitario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ExpirationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ExpirationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro sanitario (VARCHAR 30). Código de autorización INVIMA del medicamento/dispositivo.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'HealthRegistration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifican el registro sanitario del producto, solo se habilita si el producto maneja registro sanitario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'HealthRegistration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'HealthRegistration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Maneja registro sanitario (BIT). Flag si producto requiere registro INVIMA para comercializar.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'HandlesHealthRegistration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el producto maneja registro sanitario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'HandlesHealthRegistration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'HandlesHealthRegistration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Maneja serial (BIT). Flag si producto requiere número de serie único para trazabilidad.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'HandlesSerial';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si maneja serial', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'HandlesSerial';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'HandlesSerial';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código SICE (VARCHAR 20). Código del Sistema de Información de Costos Estándares.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'CodeSICE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo SICE del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'CodeSICE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'CodeSICE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presentación del producto (VARCHAR 200). Formato físico: tabletas, ampolla, vial, frasco, caja, etc.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'Presentation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Presentacion del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'Presentation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'Presentation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador IVA (INT, FK a GeneralLedgerIVA). Tarifa tributaria aplicable al producto.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'IVAId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del iva que se va aplicar al producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'IVAId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'IVAId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador fabricante (INT, FK a Manufacturer). Laboratorio, proveedor principal del artículo.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ManufacturerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fabricante', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ManufacturerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ManufacturerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador unidad empaques (INT, FK a PackagingUnit). Unidad comercial: caja, blíster, frasco.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'PackagingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de empaques', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'PackagingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'PackagingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador unidad medida (INT, FK a InventoryMeasurementUnit). Unidad base: mg, mL, UI, unidad.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id de la unidad de medida ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador subgrupo (INT, FK a ProductSubGroup, default=1). Clasificación secundaria: antihipertensivos, analgésicos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ProductSubGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del subgrupo del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ProductSubGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ProductSubGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador grupo producto (INT, FK a ProductGroup, default=2). Clasificación primaria: medicamentos, insumos, dispositivos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ProductGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del grupo de producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ProductGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ProductGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción larga (VARCHAR MAX). Detalles completos, indicaciones, composición, observaciones del producto.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion lasga del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alternativo dos (VARCHAR 20). Segundo código externo o histórico del producto.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'CodeAlternativeTwo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Alternativo Dos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'CodeAlternativeTwo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'CodeAlternativeTwo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alternativo (VARCHAR 30). Código secundario, alias, código de proveedor anterior.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'CodeAlternative';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Alternativo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'CodeAlternative';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'CodeAlternative';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CUM (VARCHAR 20). Código único de medicamento asignado por autoridad regulatoria.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'CodeCUM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo CUM del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'CodeCUM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'CodeCUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador ATC (INT, FK a ATC). Sistema de clasificación anatómico-terapéutico-químico, solo medicamentos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ATCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Sistema de Clasificación Anatómica, Terapéutica, Química del producto, solo se llena si el tipo del producto es "Item Medicamento"', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ATCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ATCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador tipo producto (INT, FK a ProductType). Categoría: medicamento, insumo, dispositivo médico, servicio.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ProductTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo de producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ProductTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ProductTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del producto (VARCHAR 400). Denominación comercial, principio activo o descripción corta.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del producto, descripcion corta', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto (VARCHAR 20, PK lógico). Identificador único interno del artículo en catálogo.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del producto (INT, PK). Llave primaria auto-incremental del registro de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo maestro de productos del inventario: medicamentos, insumos, dispositivos médicos y demás artículos gestionados en el sistema. Contiene información de identificación, clasificación, precios, control de stock, condiciones de almacenamiento y parámetros de facturación y dispensación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso o gramaje del insumo utilizado en la preparación de nutrición parenteral, expresado en decimales. Se usa para calcular la composición de mezclas de nutrición parenteral.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'WeightParenteralNutritionSupply';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'WeightParenteralNutritionSupply';
