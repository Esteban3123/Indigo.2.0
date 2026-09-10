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



GO



GO



GO



GO





GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK a ATC) del fármaco principal; ATC de referencia para filtrar productos terminados y identificar medicamento NPT principal.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'MainDrugId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Atc de referencia, se usa para identificar el medicamento principal para npt, esto nos permite filtrar los productos terminados', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'MainDrugId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'MainDrugId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de medición preparada; referencia a unidad de medida del volumen efectivamente preparado.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'MeasurementPreparedId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la medicion IdPreparado', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'MeasurementPreparedId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'MeasurementPreparedId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Volumen total preparado (DECIMAL 18,2); cantidad efectiva de solución lista para administración después de reconstitución/dilución.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'VolumeTotalPrepared';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la VolumenTotal Preparado', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'VolumeTotalPrepared';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'VolumeTotalPrepared';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concentración de antibiótico (VARCHAR 50); potencia específica del agente antimicrobiano en formato texto para referencia rápida.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'ConcentrationAntibiotic';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la concentración del antibiotico', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'ConcentrationAntibiotic';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'ConcentrationAntibiotic';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reajustes (TINYINT, default 1): 1=0 ajustes, 2=1 ajuste, 3=2 ajustes, 4=3 ajustes, 5=4 ajustes, 6=5 ajustes; número de correcciones permitidas.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'Readjustments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1. 0  2..1  3. 2  4. 3  5. 4  6. 5', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'Readjustments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'Readjustments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de almacenamiento; referencia a condición de temperatura (refrigerado, ambiente, congelado) de conservación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'Storage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id que hace referencia a las temperaturas de almacenamiento', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'Storage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'Storage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de producto relacionado; referencia a producto base en inventario asociado a este paquete de preparación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Producto relacionado al paquete', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de etiqueta (TINYINT): 1=Bolsa, 2=Mediana, 3=Jeringa 10CC, 4=Tabletería 4x4cm, 5=Magistral; formato para identificación física.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'LabelType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo Etiqueta Preferencia  1- Bolsa  2- Mediana  3- Jeringa 10 CC   4- Tableteria 4X4 cm    5 - Magistral', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'LabelType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'LabelType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de unidad de medida para volumen total pedido; referencia a unidad (mL, L, etc.) del volumen de preparación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'VolumeTotalOrderMeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad de medida para el volumen total pedido', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'VolumeTotalOrderMeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'VolumeTotalOrderMeasurementUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Preparación magistral personalizada (BIT, default 0); indica si viene de dashboard de confirmación de dosis unitarias personalizadas.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'PersonalizedMasterPreparation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite saber si el paquete es una preparación magistral personalizada, es decir que viene desde el dashboard de confirmación de dosis unitarias', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'PersonalizedMasterPreparation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'PersonalizedMasterPreparation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de preparación (TINYINT): 1=Reconstitución, 2=Dilución, 3=No Aplica, 4=Reconstitución+Dilución, 5=Ninguna; proceso requerido en farmacia.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'PreparationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de preparación:  1. Reconstitución  2. Dilución  3. No Aplica  4. Reconstitución - Dilución  5. Ninguna', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'PreparationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'PreparationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Foto protección (BIT, default 0); indica si el paquete requiere protección de luz (bolsa opaca, ámbar) durante preparación y almacenamiento.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'PhotoProtection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Foto protección', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'PhotoProtection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'PhotoProtection';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de unidad de medida de concentración; referencia a unidad (mg/mL, %, etc.) de concentración del fármaco.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'ConcentrationMeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad de medida de la concentación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'ConcentrationMeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'ConcentrationMeasurementUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera booleana (BIT); indica si es mezcla estándar pre-formulada o si requiere preparación personalizada para cada prescripción.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'StandardMix';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si es o no mezcla estándar', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'StandardMix';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'StandardMix';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación (NVARCHAR 400); razón o fundamento clínico para la formulación, preparación magistral o desviación de protocolo.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'Justification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'justificación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'Justification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'Justification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de código ATC (Anatomical Therapeutic Chemical); clasificación farmacológica internacional del principio activo.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'ATCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id ATC', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'ATCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'ATCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concentración (DECIMAL 18,4, default 0); potencia del fármaco activo en unidades de medida especificadas (mg/mL, %, etc.).', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'Concentration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concentración', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'Concentration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'Concentration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK, default 1) de tipo de unidad de dosis; referencia a formato de presentación (vial, bolsa, jeringa, etc.).', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'UnitDoseTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de tipo de unidad dosis', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'UnitDoseTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'UnitDoseTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consideraciones especiales (VARCHAR MAX); notas críticas sobre compatibilidad, luz, temperatura, interacciones o precauciones.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'SpecialConsiderations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consideraciones Especiales', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'SpecialConsiderations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'SpecialConsiderations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Instrucciones de preparación (VARCHAR MAX); pasos detallados, secuencia y técnica aséptica para preparar la mezcla en farmacia.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'PreparationInstructions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Instrucciones de preparacion', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'PreparationInstructions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'PreparationInstructions';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Velocidad de infusión (INT) en mL/h; flujo recomendado para administración intravenosa segura del medicamento.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'InfusionSpeed';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Velocidad de infusión', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'InfusionSpeed';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'InfusionSpeed';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Purga (DECIMAL 5,2, default 0); volumen adicional (mL) necesario para descargar líneas intravenosas antes de infusión.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'Purge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Purga', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'Purge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'Purge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Período a temperatura ambiente (INT, default 0) en horas; estabilidad máxima a temperatura de 20-25°C post-preparación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'EnvironmentalTemperatureTerm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Período de temperatura ambiental', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'EnvironmentalTemperatureTerm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'EnvironmentalTemperatureTerm';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Período refrigerado (INT, default 0) en horas; estabilidad máxima almacenado en refrigeración (2-8°C) post-preparación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'RefrigeratedTerm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Período refrigerado', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'RefrigeratedTerm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'RefrigeratedTerm';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de unidad de medida base; referencia a unidad principal del paquete (mg, mL, unidad, etc.).', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la unidad de medida', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del paquete (BIT): 1=Activado/Vigente, 0=Inactivo/Descontinuado; indica disponibilidad para preparación y prescripción.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Documento  1 - Activado  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de tiempo (TIMESTAMP); versión de fila para control de concurrencia y detección de cambios simultáneos.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda Marca de tiempo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación (DATETIME); timestamp del último cambio realizado al registro del paquete.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario modificador (VARCHAR 20); identificación de quién cambió por última vez los datos del paquete (auditoría).', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación (DATETIME); timestamp de cuándo se creó el registro del paquete en la base de datos.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Creación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario creador (VARCHAR 20, default ''''999''''); identificación de quien registró el paquete en el sistema (auditoría).', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso total de la solución (DECIMAL 8,2) del paquete; masa en gramos o kilogramos del producto preparado para atención clínica.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'WeightTotalSolution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el peso total del paquete (producto)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'WeightTotalSolution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'WeightTotalSolution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Volumen total con purga (DECIMAL 8,2) del paquete; volumen adicional incluyendo pérdida por purga de líneas o catéteres.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'VolumeTotalOrderPurga';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el volumen total con purga del paquete (producto)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'VolumeTotalOrderPurga';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'VolumeTotalOrderPurga';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Volumen total pedido (DECIMAL 8,2) del paquete; cantidad en unidades de medida especificada, volumen de preparación o llenado.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'VolumeTotalOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el volumen total del paquete (producto)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'VolumeTotalOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'VolumeTotalOrder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Osmolaridad total (DECIMAL 6,4) del paquete; medida de tonicidad en mOsm/L, crítica para medicamentos intravenosos o parenterales.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'OsmolarityTotal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la osmolaridad total del paquete (producto)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'OsmolarityTotal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'OsmolarityTotal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK recursiva) del paquete asociado; referencia a paquete magistral base cuando PersonalizedMasterPreparation=1.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'AssociatedPackageId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del paquete asociado, se asigna cuando PersonalizedMasterPreparation es True. Este campo solo se llena cuando se asigna un paquete con preparación magistral personalizada en el dashboard de confirmación de dosis unitarias', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'AssociatedPackageId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'AssociatedPackageId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera booleana (BIT); indica si el paquete tiene período máximo de control o vigencia limitada por lote/vencimiento.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'MaximumControlPeriod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si tiene control maximo por periodo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'MaximumControlPeriod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'MaximumControlPeriod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número (INT) de autorizaciones requeridas por pedido; cantidad de aprobaciones necesarias antes de dispensar o preparar.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'AuthorizationByOrderNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el numero de autorizaciones por pedido', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'AuthorizationByOrderNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'AuthorizationByOrderNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera booleana (BIT); indica si el paquete/producto tiene control de precios regulado o fijado por autoridad sanitaria.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'ProductWithPriceControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el paquete (producto) tiene control de precios', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'ProductWithPriceControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'ProductWithPriceControl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera booleana (BIT); indica si el paquete/producto es de control especial, requiere registro o vigilancia regulatoria.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'ProductControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el paquete (producto) es de control', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'ProductControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'ProductControl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del grupo de facturación; agrupa paquetes para procesamiento de facturas, RIPS y recaudos de servicios.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'BillingGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del grupo de facturacion', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'BillingGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'BillingGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera booleana (BIT); indica si el paquete/producto está incluido en el Punto de Servicio (POS) para facturación de atención.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'POSProduct';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el paquete (producto) esta en el POS', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'POSProduct';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'POSProduct';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código SICE (VARCHAR 20); código regulatorio de identificación de medicamento o producto controlado en sistema de sanidad.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'CodeSICE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo SICE del paquete (producto)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'CodeSICE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'CodeSICE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del fabricante; laboratorio, casa productora o proveedor responsable del medicamento o producto.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'ManufacturerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fabricante', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'ManufacturerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'ManufacturerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del subgrupo de producto; clasificación secundaria, subcategoría de producto o medicamento del paquete.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'ProductSubGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del subgrupo del paquete (producto)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'ProductSubGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'ProductSubGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del grupo de producto; categoría principal para clasificación farmacéutica o de inventario del paquete.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'ProductGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del grupo de paquete (producto)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'ProductGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'ProductGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alternativo secundario (VARCHAR 20) del paquete; segundo código equivalente para identificación múltiple del producto.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'CodeAlternativeTwo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Alternativo dos', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'CodeAlternativeTwo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'CodeAlternativeTwo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alternativo primario (VARCHAR 30) del paquete; código secundario o equivalente para búsqueda y mapeo de productos.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'CodeAlternative';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Alternativo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'CodeAlternative';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'CodeAlternative';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del nivel de riesgo; clasificación de peligrosidad del medicamento o producto (llena solo si es Item Medicamento).', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'RiskLevelId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Nivel de Riesgo, solo se llena si el tipo del paquete (producto) es "Item Medicamento"', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'RiskLevelId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'RiskLevelId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción amplia (VARCHAR MAX) del paquete; detalle completo del producto, composición, indicaciones o notas de preparación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion larga del paquete', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o descripción corta (VARCHAR 200) del paquete; etiqueta legible del producto, medicamento o mezcla para atención clínica.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del paquete, descripción corta', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único (VARCHAR 20) del paquete; identificador principal para búsqueda y clasificación de productos, medicamentos y preparaciones.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del paquete', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) del paquete en la estación de mezcla; clave primaria para referencias de preparaciones de medicamentos y mezclas estándar.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del paquete', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de preparaciones o mezclas farmacéuticas (bolsas, infusiones, nutrición parenteral y dosis unitarias) que gestiona la estación de mezclas. Contiene la fórmula maestra de cada paquete: componentes, volumen, concentración, tipo de preparación, condiciones de almacenamiento y estabilidad.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si la preparación aplica optimización del vehículo (solvente/diluyente), es decir, si se ajusta automáticamente el volumen del vehículo para alcanzar la concentración objetivo.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'VehicleOptimization';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'VehicleOptimization';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo o protocolo de Nutrición Parenteral Total (NPT) asociado a esta preparación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'NptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'NptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora límite del día hasta la cual la preparación mantiene su estabilidad físico-química una vez elaborada (complementa los días de estabilidad).', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'StabilityHour';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'StabilityHour';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo o categoría de estabilidad de la mezcla (por ejemplo: refrigerada, temperatura ambiente, protegida de luz), expresada como código numérico.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'TypeStability';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'TypeStability';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de días que la preparación permanece estable y apta para su administración bajo las condiciones de almacenamiento indicadas.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'StabilityDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'StabilityDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo de estabilidad de la preparación expresado en horas (vigencia total desde la elaboración hasta su vencimiento).', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'Stability';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Package', @level2type = N'COLUMN', @level2name = N'Stability';
