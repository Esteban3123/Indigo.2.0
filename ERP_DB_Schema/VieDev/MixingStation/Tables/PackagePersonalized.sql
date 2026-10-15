CREATE TABLE [MixingStation].[PackagePersonalized] (
    [Id]                                INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                              VARCHAR (20)    NOT NULL,
    [Name]                              VARCHAR (200)   NOT NULL,
    [Description]                       VARCHAR (MAX)   NULL,
    [RiskLevelId]                       INT             NULL,
    [CodeAlternative]                   VARCHAR (30)    NULL,
    [CodeAlternativeTwo]                VARCHAR (20)    NULL,
    [ProductGroupId]                    INT             NULL,
    [ProductSubGroupId]                 INT             NULL,
    [ManufacturerId]                    INT             NULL,
    [CodeSICE]                          VARCHAR (20)    NULL,
    [POSProduct]                        BIT             NULL,
    [BillingGroupId]                    INT             NULL,
    [ProductControl]                    BIT             NULL,
    [ProductWithPriceControl]           BIT             NULL,
    [AuthorizationByOrderNumber]        INT             NULL,
    [MaximumControlPeriod]              BIT             NULL,
    [OsmolarityTotal]                   DECIMAL (6, 4)  NULL,
    [VolumeTotalOrder]                  DECIMAL (8, 2)  NULL,
    [VolumeTotalOrderMeasurementUnitId] INT             NULL,
    [VolumeTotalOrderPurga]             DECIMAL (8, 2)  NULL,
    [WeightTotalSolution]               DECIMAL (8, 2)  NULL,
    [CreationUser]                      VARCHAR (20)    CONSTRAINT [DF__PackagePersonalized__Creatio__6E77559A] DEFAULT ('999') NOT NULL,
    [CreationDate]                      DATETIME        CONSTRAINT [DF__PackagePersonalized__Creatio__6F6B79D3] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]                  VARCHAR (20)    NULL,
    [ModificationDate]                  DATETIME        NULL,
    [TimeStamp]                         ROWVERSION      NOT NULL,
    [State]                             BIT             NOT NULL,
    [MeasurementUnitId]                 INT             CONSTRAINT [DF_PackagePersonalized_MeasurementUnitId] DEFAULT ((0)) NULL,
    [RefrigeratedTerm]                  INT             CONSTRAINT [DF_PackagePersonalized_RefrigeratedTerm] DEFAULT ((0)) NOT NULL,
    [EnvironmentalTemperatureTerm]      INT             CONSTRAINT [DF_PackagePersonalized_EnvironmentalTemperatureTerm] DEFAULT ((0)) NOT NULL,
    [Purge]                             DECIMAL (5, 2)  CONSTRAINT [DF_PackagePersonalized_Purge] DEFAULT ((0)) NOT NULL,
    [InfusionSpeed]                     INT             NULL,
    [PreparationInstructions]           VARCHAR (MAX)   NULL,
    [SpecialConsiderations]             VARCHAR (MAX)   NULL,
    [UnitDoseTypeId]                    INT             CONSTRAINT [DF_PackagePersonalized_UnitDoseTypeId] DEFAULT ((1)) NOT NULL,
    [Concentration]                     DECIMAL (18, 4) CONSTRAINT [DF_PackagePersonalized_Concentration] DEFAULT ((0)) NULL,
    [ConcentrationMeasurementUnitId]    INT             NULL,
    [ATCId]                             INT             NULL,
    [Justification]                     NVARCHAR (400)  NULL,
    [StandardMix]                       BIT             NULL,
    [PhotoProtection]                   BIT             CONSTRAINT [DF_PackagePersonalized_PhotoProtection] DEFAULT ((0)) NOT NULL,
    [PreparationType]                   TINYINT         NULL,
    [PersonalizedMasterPreparation]     BIT             CONSTRAINT [DF_PackagePersonalized_PersonalizedMasterPreparation] DEFAULT ((0)) NOT NULL,
    [AssociatedPackageId]               INT             NULL,
    [LabelType]                         TINYINT         NULL,
    [Storage]                           TINYINT         CONSTRAINT [DF__PackagePe__Stora__229B75B5] DEFAULT ((1)) NOT NULL,
    [ConcentrationAntibiotic]           VARCHAR (50)    NULL,
    [VolumeTotalPrepared]               DECIMAL (18, 2) NULL,
    [MeasurementPreparedId]             INT             NULL,
    [MainDrugId]                        INT             NULL,
    [ProductId]                         INT             NULL,
    CONSTRAINT [PK_PackagePersonalized] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PackagePersonalized_Inventory_Product] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_PackagePersonalized_InventoryMeasurementUnit] FOREIGN KEY ([ConcentrationMeasurementUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id]),
    CONSTRAINT [FK_PackagePersonalized_InventoryProduct] FOREIGN KEY ([ATCId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_PackagePersonalized_MeasurementUnit] FOREIGN KEY ([MeasurementUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id]),
    CONSTRAINT [FK_PackagePersonalized_Package1] FOREIGN KEY ([AssociatedPackageId]) REFERENCES [MixingStation].[Package] ([Id]),
    CONSTRAINT [FK_PackagePersonalized_ProductGroup] FOREIGN KEY ([ProductGroupId]) REFERENCES [Inventory].[ProductGroup] ([Id]),
    CONSTRAINT [FK_PackagePersonalized_ProductSubGroup] FOREIGN KEY ([ProductSubGroupId]) REFERENCES [Inventory].[ProductSubGroup] ([Id]),
    CONSTRAINT [FK_PackagePersonalized_Reference_ATC] FOREIGN KEY ([MainDrugId]) REFERENCES [Inventory].[ATC] ([Id]),
    CONSTRAINT [FK_PackagePersonalized_RiskLevel] FOREIGN KEY ([RiskLevelId]) REFERENCES [Inventory].[InventoryRiskLevel] ([Id]),
    CONSTRAINT [FK_PackagePersonalized_UnitDoseType] FOREIGN KEY ([UnitDoseTypeId]) REFERENCES [MixingStation].[UnitDoseType] ([Id]),
    CONSTRAINT [FK_PackagePersonalized_VolumeMeasurementUnit] FOREIGN KEY ([VolumeTotalOrderMeasurementUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id])
);


GO
ALTER TABLE [MixingStation].[PackagePersonalized] NOCHECK CONSTRAINT [FK_PackagePersonalized_InventoryMeasurementUnit];


GO
ALTER TABLE [MixingStation].[PackagePersonalized] NOCHECK CONSTRAINT [FK_PackagePersonalized_InventoryProduct];


GO
ALTER TABLE [MixingStation].[PackagePersonalized] NOCHECK CONSTRAINT [FK_PackagePersonalized_MeasurementUnit];


GO
ALTER TABLE [MixingStation].[PackagePersonalized] NOCHECK CONSTRAINT [FK_PackagePersonalized_Package1];


GO
ALTER TABLE [MixingStation].[PackagePersonalized] NOCHECK CONSTRAINT [FK_PackagePersonalized_ProductGroup];


GO
ALTER TABLE [MixingStation].[PackagePersonalized] NOCHECK CONSTRAINT [FK_PackagePersonalized_ProductSubGroup];


GO
ALTER TABLE [MixingStation].[PackagePersonalized] NOCHECK CONSTRAINT [FK_PackagePersonalized_RiskLevel];


GO
ALTER TABLE [MixingStation].[PackagePersonalized] NOCHECK CONSTRAINT [FK_PackagePersonalized_UnitDoseType];


GO
ALTER TABLE [MixingStation].[PackagePersonalized] NOCHECK CONSTRAINT [FK_PackagePersonalized_VolumeMeasurementUnit];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Producto terminado relacionado al paquete personalizado', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del Atc de referencia, se usa para identificar el medicamento principal para npt, esto nos permite filtrar los productos terminados', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'MainDrugId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1. Ambiente: 20 ° C - 25 ° C (Permitida 15 ° C y 30 ° C)  2..Ambiente controlada: 20 ° C - 25 ° C  3. En frío: 8 ° C - 15 ° C  4. Refrigerador: 2 ° C - 8 ° C  5. Congelador: -25 ° C - 10 ° C', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'Storage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo Etiqueta Preferencia    1- Bolsa  2- Mediana  3- Jeringa', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'LabelType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del paquete asociado, se asigna cuando PersonalizedMasterPreparation es True. Este campo solo se llena cuando se asigna un paquete con preparación magistral personalizada en el dashboard de confirmación de dosis unitarias', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'AssociatedPackageId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite saber si el paquete es una preparación magistral personalizada, es decir que viene desde el dashboard de confirmación de dosis unitarias', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'PersonalizedMasterPreparation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de preparación:  1. Reconstitución  2. Dilución  3. No Aplica  4. Reconstitución - Dilución', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'PreparationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Foto protección', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'PhotoProtection';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si es o no mezcla estándar', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'StandardMix';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'justificación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'Justification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del producto terminado', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'ATCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad de medida de la concentación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'ConcentrationMeasurementUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concentración', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'Concentration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de tipo de unidad dosis', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'UnitDoseTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consideraciones Especiales', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'SpecialConsiderations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Instrucciones de preparacion', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'PreparationInstructions';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Velocidad de infusión', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'InfusionSpeed';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Purga', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'Purge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Período de temperatura ambiental', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'EnvironmentalTemperatureTerm';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Período refrigerado', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'RefrigeratedTerm';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la unidad de medida', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Documento  1 - Activado  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Creación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el peso total del paquete (producto)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'WeightTotalSolution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el volumen total con purga del paquete (producto)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'VolumeTotalOrderPurga';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad de medida para el volumen total pedido', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'VolumeTotalOrderMeasurementUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el volumen total del paquete (producto)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'VolumeTotalOrder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la osmolaridad total del paquete (producto)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'OsmolarityTotal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si tiene control maximo por periodo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'MaximumControlPeriod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el numero de autorizaciones por pedido', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'AuthorizationByOrderNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el paquete (producto) tiene control de precios', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'ProductWithPriceControl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el paquete (producto) es de control', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'ProductControl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del grupo de facturacion', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'BillingGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el paquete (producto) esta en el POS', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'POSProduct';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo SICE del paquete (producto)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'CodeSICE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fabricante', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'ManufacturerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del subgrupo del paquete (producto)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'ProductSubGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del grupo de paquete (producto)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'ProductGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Alternativo dos', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'CodeAlternativeTwo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Alternativo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'CodeAlternative';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Nivel de Riesgo, solo se llena si el tipo del paquete (producto) es "Item Medicamento"', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'RiskLevelId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion larga del paquete', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del paquete, descripción corta', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del paquete', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del paquete', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'Id';

