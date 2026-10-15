CREATE TABLE [MixingStation].[Package] (
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
    [AssociatedPackageId]               INT             NULL,
    [OsmolarityTotal]                   DECIMAL (6, 4)  NULL,
    [VolumeTotalOrder]                  DECIMAL (8, 2)  NULL,
    [VolumeTotalOrderPurga]             DECIMAL (8, 2)  NULL,
    [WeightTotalSolution]               DECIMAL (8, 2)  NULL,
    [CreationUser]                      VARCHAR (20)    CONSTRAINT [DF__Package__Creatio__6E77559A] DEFAULT ('999') NOT NULL,
    [CreationDate]                      DATETIME        CONSTRAINT [DF__Package__Creatio__6F6B79D3] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]                  VARCHAR (20)    NULL,
    [ModificationDate]                  DATETIME        NULL,
    [TimeStamp]                         ROWVERSION      NOT NULL,
    [State]                             BIT             NOT NULL,
    [MeasurementUnitId]                 INT             CONSTRAINT [DF_Package_MeasurementUnitId] DEFAULT ((0)) NULL,
    [RefrigeratedTerm]                  INT             CONSTRAINT [DF_Package_RefrigeratedTerm] DEFAULT ((0)) NOT NULL,
    [EnvironmentalTemperatureTerm]      INT             CONSTRAINT [DF_Package_EnvironmentalTemperatureTerm] DEFAULT ((0)) NOT NULL,
    [Purge]                             DECIMAL (5, 2)  CONSTRAINT [DF_Package_Purge] DEFAULT ((0)) NOT NULL,
    [InfusionSpeed]                     INT             NULL,
    [PreparationInstructions]           VARCHAR (MAX)   NULL,
    [SpecialConsiderations]             VARCHAR (MAX)   NULL,
    [UnitDoseTypeId]                    INT             CONSTRAINT [DF_Package_UnitDoseTypeId] DEFAULT ((1)) NOT NULL,
    [Concentration]                     DECIMAL (18, 4) CONSTRAINT [DF_Package_Concentration] DEFAULT ((0)) NULL,
    [ATCId]                             INT             NULL,
    [Justification]                     NVARCHAR (400)  NULL,
    [StandardMix]                       BIT             NULL,
    [ConcentrationMeasurementUnitId]    INT             NULL,
    [PhotoProtection]                   BIT             CONSTRAINT [DF_Package_PhotoProtection] DEFAULT ((0)) NOT NULL,
    [PreparationType]                   TINYINT         NULL,
    [PersonalizedMasterPreparation]     BIT             CONSTRAINT [DF_Package_PersonalizedMasterPreparation] DEFAULT ((0)) NOT NULL,
    [VolumeTotalOrderMeasurementUnitId] INT             NULL,
    [LabelType]                         TINYINT         NULL,
    [ProductId]                         INT             NULL,
    [VehicleOptimization]               BIT             CONSTRAINT [DF_Package_VehicleOptimization] DEFAULT ((0)) NOT NULL,
    [Storage]                           INT             NULL,
    [Readjustments]                     TINYINT         CONSTRAINT [DF__Package__Readjus__1F89FEE0] DEFAULT ((1)) NOT NULL,
    [ConcentrationAntibiotic]           VARCHAR (50)    NULL,
    [VolumeTotalPrepared]               DECIMAL (18, 2) NULL,
    [MeasurementPreparedId]             INT             NULL,
    [NptId]                             INT             NULL,
    [StabilityHour]                     TIME (0)        NULL,
    [MainDrugId]                        INT             NULL,
    [TypeStability]                     TINYINT         NULL,
    [StabilityDays]                     INT             NULL,
    [Stability]                         INT             NULL,
    CONSTRAINT [PK_Package] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MeasurementPreparedId_InventoryMeasurementUnit] FOREIGN KEY ([MeasurementPreparedId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id]),
    CONSTRAINT [FK_Package_ATCEntity] FOREIGN KEY ([ATCId]) REFERENCES [Inventory].[ATCEntity] ([Id]),
    CONSTRAINT [FK_Package_InventoryMeasurementUnit] FOREIGN KEY ([ConcentrationMeasurementUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id]),
    CONSTRAINT [FK_Package_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_Package_MeasurementUnit] FOREIGN KEY ([MeasurementUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id]),
    CONSTRAINT [FK_Package_Package1] FOREIGN KEY ([AssociatedPackageId]) REFERENCES [MixingStation].[Package] ([Id]),
    CONSTRAINT [FK_Package_ProductGroup] FOREIGN KEY ([ProductGroupId]) REFERENCES [Inventory].[ProductGroup] ([Id]),
    CONSTRAINT [FK_Package_ProductSubGroup] FOREIGN KEY ([ProductSubGroupId]) REFERENCES [Inventory].[ProductSubGroup] ([Id]),
    CONSTRAINT [FK_Package_Reference_ATC] FOREIGN KEY ([MainDrugId]) REFERENCES [Inventory].[ATC] ([Id]),
    CONSTRAINT [FK_Package_RiskLevel] FOREIGN KEY ([RiskLevelId]) REFERENCES [Inventory].[InventoryRiskLevel] ([Id]),
    CONSTRAINT [FK_Package_StorageTemperature] FOREIGN KEY ([Storage]) REFERENCES [Inventory].[StorageTemperature] ([Id]),
    CONSTRAINT [FK_Package_UnitDoseType] FOREIGN KEY ([UnitDoseTypeId]) REFERENCES [MixingStation].[UnitDoseType] ([Id]),
    CONSTRAINT [FK_Package_VolumeMeasurementUnit] FOREIGN KEY ([VolumeTotalOrderMeasurementUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id])
);


GO
ALTER TABLE [MixingStation].[Package] NOCHECK CONSTRAINT [FK_MeasurementPreparedId_InventoryMeasurementUnit];


GO
ALTER TABLE [MixingStation].[Package] NOCHECK CONSTRAINT [FK_Package_ATCEntity];


GO
ALTER TABLE [MixingStation].[Package] NOCHECK CONSTRAINT [FK_Package_InventoryMeasurementUnit];


GO
ALTER TABLE [MixingStation].[Package] NOCHECK CONSTRAINT [FK_Package_InventoryProduct];


GO
ALTER TABLE [MixingStation].[Package] NOCHECK CONSTRAINT [FK_Package_MeasurementUnit];


GO
ALTER TABLE [MixingStation].[Package] NOCHECK CONSTRAINT [FK_Package_Package1];


GO
ALTER TABLE [MixingStation].[Package] NOCHECK CONSTRAINT [FK_Package_ProductGroup];


GO
ALTER TABLE [MixingStation].[Package] NOCHECK CONSTRAINT [FK_Package_ProductSubGroup];


GO
ALTER TABLE [MixingStation].[Package] NOCHECK CONSTRAINT [FK_Package_RiskLevel];


GO
ALTER TABLE [MixingStation].[Package] NOCHECK CONSTRAINT [FK_Package_UnitDoseType];


GO
ALTER TABLE [MixingStation].[Package] NOCHECK CONSTRAINT [FK_Package_VolumeMeasurementUnit];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del Atc de referencia, se usa para identificar el medicamento principal para npt, esto nos permite filtrar los productos terminados', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'MainDrugId';

