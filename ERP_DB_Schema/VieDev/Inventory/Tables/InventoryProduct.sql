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
ALTER TABLE [Inventory].[InventoryProduct] NOCHECK CONSTRAINT [FK_InventoryProduct_BillingGroup];


GO
ALTER TABLE [Inventory].[InventoryProduct] NOCHECK CONSTRAINT [FK_InventoryProduct_InventoryMeasurementUnit];


GO
ALTER TABLE [Inventory].[InventoryProduct] NOCHECK CONSTRAINT [FK_InventoryProduct_InventorySupplie];


GO
ALTER TABLE [Inventory].[InventoryProduct] NOCHECK CONSTRAINT [FK_Product_ATC];


GO
ALTER TABLE [Inventory].[InventoryProduct] NOCHECK CONSTRAINT [FK_Product_GeneralLedgerIVA];


GO
ALTER TABLE [Inventory].[InventoryProduct] NOCHECK CONSTRAINT [FK_Product_InventoryRiskLevel];


GO
ALTER TABLE [Inventory].[InventoryProduct] NOCHECK CONSTRAINT [FK_Product_Manufacturer];


GO
ALTER TABLE [Inventory].[InventoryProduct] NOCHECK CONSTRAINT [FK_Product_MedicationTypeId];


GO
ALTER TABLE [Inventory].[InventoryProduct] NOCHECK CONSTRAINT [FK_Product_PackagingUnit];


GO
ALTER TABLE [Inventory].[InventoryProduct] NOCHECK CONSTRAINT [FK_Product_ProductGroup];


GO
ALTER TABLE [Inventory].[InventoryProduct] NOCHECK CONSTRAINT [FK_Product_ProductSubGroup];


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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del tipo de medicamento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'MedicationTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Define el tipo de componente lácteo: 1 - Fórmula láctea/ especial, 2 - Leche materna, 3 - Insumo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'DairyComponentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Define si es o no un componente lácteo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'DairyComponent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Informe Sismed (1 - Si, 0 - No)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'SismedReport';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Liquida iva en ventas Salud- 1: true, 0: false', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'LiquidateSalesTaxes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Osmolaridad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'Osmolarity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Producto gravado (1 - Si, 0 - No)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'TaxedProduct';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1. Ambiente: 20 ° C - 25 ° C (Permitida 15 ° C y 30 ° C)  2..Ambiente controlada: 20 ° C - 25 ° C  3. En frío: 8 ° C - 15 ° C  4. Refrigerador: 2 ° C - 8 ° C  5. Congelador: -25 ° C - 10 ° C', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'Storage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador único de medicamento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'IUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de insumo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'SupplieId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Abreviatura', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'Abbreviation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Material Osteosisntesis, se asigna cuando sea item insumo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'OsteosynthesisMaterial';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Justificación Insumos/Dispositivos, solo se asigna cuando sea item insumo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'JustificationSuppliesDispositives';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite saber si es de consumo, este campo se llena cuando es de item insumo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'Consumption';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo que almacena el estado de registro sanitario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'SanitaryRegistration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo que contiene el valor de la temperatura maxima en el cual el producto puede se almacenado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'MaximumTemperature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo que contiene el valor de la temperatura minima en el cual el producto puede se almacenado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'MinimumTemperature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo que contiene la cantidad de descarga mínima a la que pretendemos manejar el producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'DriveUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Se especifica el número de serial cuando el producto maneja serial', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'SerialNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Nivel de Riesgo, solo se llena si el tipo del producto es "Item Medicamento"', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'InventoryRiskLevelId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de control de costo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ControlCostPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de grupo de facturación No pos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'BillingGroupNoPosId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Documento  1 - Activado  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el producto aplica todas las patologias POS', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'AllPOSPathologies';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el precio de venta del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'SellingPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el precio del ultimo costo del producto, este es el ultimo valor con el que se compro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'FinalProductCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el costo promedio del producto, este se actualiza cada vez que se realiza una entrada del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ProductCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de moneda  1 - Pesos(Colombia)  2  - Extranjera', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'CurrencyType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tiempo de reposición', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ResetTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Punto de reposición', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'RepositionPoint';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje de comision', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'CommissionPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Stock maximo del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'MaximumStock';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Stock minimo del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'MinimumStock';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Origen del producto  1 - Nacional  2 - Importado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ProductOrigin';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la ultima venta del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'LastSale';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la ultima compra del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'LastPurchase';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la cantidad de producto por orden, este campo se llena solo si el producto tiene control por cantidad de orden', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ProductOrderAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el producto tiene control de cantidad por orden', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ControlOrderQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica los dias de control que va tener, este campo se habilita solo si tiene control maximo por periodo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ControlDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si tiene control maximo por periodo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'MaximumControlPeriod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica los dias de expiración del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ExpirationDay';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el numero de autorizaciones por pedido', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'AuthorizationByOrderNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el producto esta en el POS', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'POSProduct';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el producto tiene control de precios', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ProductWithPriceControl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el producto es de control', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ProductControl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del grupo de facturacion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'BillingGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de vencimiento R.S del producto, solo se llena si el producto maneja registro sanitario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ExpirationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifican el registro sanitario del producto, solo se habilita si el producto maneja registro sanitario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'HealthRegistration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el producto maneja registro sanitario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'HandlesHealthRegistration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si maneja serial', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'HandlesSerial';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo SICE del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'CodeSICE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Presentacion del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'Presentation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del iva que se va aplicar al producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'IVAId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fabricante', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ManufacturerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de empaques', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'PackagingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id de la unidad de medida ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del subgrupo del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ProductSubGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del grupo de producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ProductGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion lasga del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Alternativo Dos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'CodeAlternativeTwo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Alternativo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'CodeAlternative';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo CUM del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'CodeCUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Sistema de Clasificación Anatómica, Terapéutica, Química del producto, solo se llena si el tipo del producto es "Item Medicamento"', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ATCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tipo de producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'ProductTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del producto, descripcion corta', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryProduct', @level2type = N'COLUMN', @level2name = N'Id';

