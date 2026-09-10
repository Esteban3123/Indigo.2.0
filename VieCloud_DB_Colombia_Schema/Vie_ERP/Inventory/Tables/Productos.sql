CREATE TABLE [Inventory].[Productos] (
    [Code]                       NVARCHAR (255) NULL,
    [Name]                       NVARCHAR (255) NULL,
    [ProductTypeId]              NVARCHAR (255) NULL,
    [ATCId]                      NVARCHAR (255) NULL,
    [CodeCUM]                    NVARCHAR (255) NULL,
    [CodeAlternative]            NVARCHAR (255) NULL,
    [CodeAlternativeTwo]         NVARCHAR (255) NULL,
    [Description]                NVARCHAR (255) NULL,
    [ProductGroupId]             NVARCHAR (255) NULL,
    [ProductSubGroupId]          NVARCHAR (255) NULL,
    [MeasurementUnitId]          NVARCHAR (255) NULL,
    [PackagingUnitId]            NVARCHAR (255) NULL,
    [ManufacturerId]             NVARCHAR (255) NULL,
    [IVAId]                      NVARCHAR (255) NULL,
    [Presentation]               NVARCHAR (255) NULL,
    [CodeSICE]                   NVARCHAR (255) NULL,
    [HandlesSerial]              NVARCHAR (255) NULL,
    [HandlesHealthRegistration]  NVARCHAR (255) NULL,
    [HealthRegistration]         NVARCHAR (255) NULL,
    [ExpirationDate]             DATETIME       NULL,
    [BillingGroupId]             FLOAT (53)     NULL,
    [ProductControl]             NVARCHAR (255) NULL,
    [ProductWithPriceControl]    NVARCHAR (255) NULL,
    [POSProduct]                 FLOAT (53)     NULL,
    [AuthorizationByOrderNumber] NVARCHAR (255) NULL,
    [ExpirationDay]              NVARCHAR (255) NULL,
    [MaximumControlPeriod]       NVARCHAR (255) NULL,
    [ControlDays]                NVARCHAR (255) NULL,
    [ControlOrderQuantity]       NVARCHAR (255) NULL,
    [ProductOrderAmount]         NVARCHAR (255) NULL,
    [LastPurchase]               NVARCHAR (255) NULL,
    [LastSale]                   NVARCHAR (255) NULL,
    [ProductOrigin]              NVARCHAR (255) NULL,
    [MinimumStock]               NVARCHAR (255) NULL,
    [MaximumStock]               NVARCHAR (255) NULL,
    [CommissionPercentage]       NVARCHAR (255) NULL,
    [RepositionPoint]            NVARCHAR (255) NULL,
    [ResetTime]                  NVARCHAR (255) NULL,
    [CurrencyType]               NVARCHAR (255) NULL,
    [ProductCost]                NVARCHAR (255) NULL,
    [FinalProductCost]           NVARCHAR (255) NULL,
    [SellingPrice]               NVARCHAR (255) NULL,
    [AllPOSPathologies]          NVARCHAR (255) NULL,
    [Status]                     NVARCHAR (255) NULL,
    [CreationUser]               NVARCHAR (255) NULL,
    [CreationDate]               NVARCHAR (255) NULL,
    [ModificationUser]           NVARCHAR (255) NULL,
    [ModificationDate]           NVARCHAR (255) NULL,
    [TimeStamp]                  NVARCHAR (255) NULL,
    [BillingGroupNoPosId]        NVARCHAR (255) NULL,
    [ControlCostPercentage]      NVARCHAR (255) NULL,
    [InventoryRiskLevelId]       NVARCHAR (255) NULL,
    [SerialNumber]               NVARCHAR (255) NULL,
    [DriveUnit]                  NVARCHAR (255) NULL,
    [MinimumTemperature]         NVARCHAR (255) NULL,
    [MaximumTemperature]         NVARCHAR (255) NULL,
    [SanitaryRegistration]       NVARCHAR (255) NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo maestro de productos del inventario (medicamentos, dispositivos médicos e insumos). Contiene la configuración completa de cada ítem: códigos, clasificaciones, precios, controles regulatorios y parámetros de stock.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código interno del producto en el sistema, referencia principal para identificarlo en inventario y facturación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre comercial o descriptivo del producto, medicamento o insumo.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de producto (ej: medicamento, dispositivo médico, insumo, reactivo). Clasifica la naturaleza del ítem.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'ProductTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'ProductTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código ATC (Anatomical Therapeutic Chemical) de clasificación farmacológica internacional del medicamento.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'ATCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'ATCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código Único de Medicamentos (CUM) asignado por el INVIMA para identificar el medicamento en Colombia.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'CodeCUM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'CodeCUM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alternativo del producto, puede corresponder a un código externo, del proveedor o de otro sistema.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'CodeAlternative';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'CodeAlternative';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo código alternativo del producto para compatibilidad con otros sistemas o proveedores.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'CodeAlternativeTwo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'CodeAlternativeTwo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada del producto, características adicionales o especificaciones técnicas.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grupo al que pertenece el producto dentro de la clasificación del inventario (ej: antibióticos, material quirúrgico).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'ProductGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'ProductGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Subgrupo de clasificación más específica del producto dentro del grupo de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'ProductSubGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'ProductSubGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida del producto (ej: miligramos, mililitros, unidades). Unidad de dispensación o consumo.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de empaque o presentación de compra (ej: caja, blíster, frasco). Unidad de entrada al inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'PackagingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'PackagingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fabricante o laboratorio que produce el medicamento o insumo.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'ManufacturerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'ManufacturerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tarifa de IVA aplicable al producto para efectos de facturación y tributación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'IVAId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'IVAId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presentación farmacéutica o comercial del producto (ej: tableta 500mg, ampolla 10ml, crema 30g).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'Presentation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'Presentation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código SICE (Sistema de Información de Costos Eficientes) del producto, usado en regulación de precios de medicamentos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'CodeSICE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'CodeSICE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el producto maneja número de serie para trazabilidad individual (sí/no). Aplica especialmente a dispositivos médicos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'HandlesSerial';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'HandlesSerial';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el producto requiere registro sanitario del INVIMA para su comercialización y dispensación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'HandlesHealthRegistration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'HandlesHealthRegistration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de registro sanitario del INVIMA del producto o medicamento.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'HealthRegistration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'HealthRegistration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de vencimiento o vigencia del registro/autorización del producto.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'ExpirationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'ExpirationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grupo de facturación POS (Plan de Beneficios en Salud) al que pertenece el producto para liquidación con aseguradoras.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'BillingGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'BillingGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el producto es de control especial (ej: psicotrópicos, estupefacientes, precursores químicos).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'ProductControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'ProductControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el producto está sujeto a control de precios por regulación del gobierno (ej: circular de precios MINSALUD).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'ProductWithPriceControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'ProductWithPriceControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el producto hace parte del Plan de Beneficios en Salud (PBS/POS) cubierto por las EPS.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'POSProduct';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'POSProduct';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si la dispensación del producto requiere autorización previa vinculada a un número de orden médica.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'AuthorizationByOrderNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'AuthorizationByOrderNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de días de vigencia o vida útil del producto desde su apertura o fabricación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'ExpirationDay';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'ExpirationDay';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Período máximo de control o seguimiento para productos de dispensación controlada (en días o meses).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'MaximumControlPeriod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'MaximumControlPeriod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de días que cubre una fórmula o prescripción de este producto en el ciclo de control.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'ControlDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'ControlDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad máxima permitida por orden o receta para productos de control especial.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'ControlOrderQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'ControlOrderQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad estándar o máxima que se puede solicitar del producto por pedido u orden de compra.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'ProductOrderAmount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'ProductOrderAmount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha o referencia de la última compra o entrada registrada del producto al inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'LastPurchase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'LastPurchase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha o referencia de la última venta o dispensación registrada del producto.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'LastSale';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'LastSale';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Origen del producto (ej: nacional, importado). Útil para trazabilidad y regulación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'ProductOrigin';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'ProductOrigin';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad mínima de stock permitida antes de generar alerta de reposición o desabastecimiento.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'MinimumStock';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'MinimumStock';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad máxima de stock que se debe mantener en bodega o farmacia.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'MaximumStock';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'MaximumStock';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de comisión aplicable sobre la venta del producto para vendedores o distribuidores.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'CommissionPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'CommissionPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Punto de reorden: cantidad de stock en la que se debe generar la solicitud de reposición del producto.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'RepositionPoint';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'RepositionPoint';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo de reposición o lead time del producto: días esperados para recibir el producto desde que se realiza el pedido.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'ResetTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'ResetTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de moneda en que se expresan los costos y precios del producto (ej: COP, USD).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'CurrencyType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'CurrencyType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Costo de adquisición o compra del producto sin incluir impuestos ni ajustes adicionales.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'ProductCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'ProductCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Costo final del producto incluyendo todos los ajustes, impuestos y fletes. Costo real de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'FinalProductCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'FinalProductCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Precio de venta del producto al paciente, usuario o cliente final.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'SellingPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'SellingPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el producto aplica para todas las patologías cubiertas por el POS/PBS, sin restricción diagnóstica.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'AllPOSPathologies';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'AllPOSPathologies';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado actual del producto en el catálogo (activo, inactivo, descontinuado).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro del producto en el sistema.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se creó el producto en el catálogo del inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación al registro del producto.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación realizada al producto.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de tiempo para control de concurrencia y auditoría de cambios en el registro.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grupo de facturación para productos NO POS (servicios o medicamentos no cubiertos por el plan de beneficios).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'BillingGroupNoPosId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'BillingGroupNoPosId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de control sobre el costo del producto, usado para auditorías de precios o márgenes regulados.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'ControlCostPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'ControlCostPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel de riesgo del producto en el inventario (ej: alto, medio, bajo) para cadena de frío, manejo especial o seguridad.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'InventoryRiskLevelId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'InventoryRiskLevelId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de serie del producto o lote para trazabilidad individual, especialmente en dispositivos médicos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'SerialNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'SerialNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de manejo o despacho del producto (unidad con la que se mueve internamente en la institución).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'DriveUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'DriveUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Temperatura mínima de almacenamiento requerida para conservar correctamente el producto (cadena de frío).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'MinimumTemperature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'MinimumTemperature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Temperatura máxima de almacenamiento permitida para garantizar la estabilidad del producto.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'MaximumTemperature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'MaximumTemperature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de registro sanitario del INVIMA vigente para el producto, requerido para su comercialización legal en Colombia.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'SanitaryRegistration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Productos', @level2type = N'COLUMN', @level2name = N'SanitaryRegistration';
