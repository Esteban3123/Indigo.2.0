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



GO



GO



GO



GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Identificador del producto terminado relacionado al paquete personalizado; referencia a Inventory.InventoryProduct para búsqueda de medicamento, insumo o solución.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Producto terminado relacionado al paquete personalizado', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Identificador ATC del medicamento principal para NPT (nutrición parenteral total); filtra productos terminados por fármaco, droga o principio activo de referencia.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'MainDrugId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Atc de referencia, se usa para identificar el medicamento principal para npt, esto nos permite filtrar los productos terminados', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'MainDrugId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'MainDrugId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT (1-5). Tipo de almacenamiento y condición de temperatura: 1=Ambiente 20-25°C, 2=Ambiente controlado, 3=Frío 8-15°C, 4=Refrigerador 2-8°C, 5=Congelador -25°C a -10°C.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'Storage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1. Ambiente: 20 ° C - 25 ° C (Permitida 15 ° C y 30 ° C)  2..Ambiente controlada: 20 ° C - 25 ° C  3. En frío: 8 ° C - 15 ° C  4. Refrigerador: 2 ° C - 8 ° C  5. Congelador: -25 ° C - 10 ° C', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'Storage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'Storage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT. Tipo de etiqueta de presentación: 1=Bolsa, 2=Mediana, 3=Jeringa; para identificación y etiquetado en farmacia.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'LabelType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo Etiqueta Preferencia    1- Bolsa  2- Mediana  3- Jeringa', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'LabelType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'LabelType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT FK. Identificador del paquete maestro asociado cuando PersonalizedMasterPreparation=True; asignado desde dashboard de confirmación de dosis unitarias.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'AssociatedPackageId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del paquete asociado, se asigna cuando PersonalizedMasterPreparation es True. Este campo solo se llena cuando se asigna un paquete con preparación magistral personalizada en el dashboard de confirmación de dosis unitarias', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'AssociatedPackageId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'AssociatedPackageId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Indicador si es preparación magistral personalizada (True=1) o estándar (False=0); generada desde dashboard de dosis unitarias.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'PersonalizedMasterPreparation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite saber si el paquete es una preparación magistral personalizada, es decir que viene desde el dashboard de confirmación de dosis unitarias', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'PersonalizedMasterPreparation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'PersonalizedMasterPreparation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT. Tipo de preparación farmacéutica: 1=Reconstitución, 2=Dilución, 3=No aplica, 4=Reconstitución-Dilución.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'PreparationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de preparación:  1. Reconstitución  2. Dilución  3. No Aplica  4. Reconstitución - Dilución', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'PreparationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'PreparationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Indicador de fotoprotección requerida (1=Sí, 0=No); protección contra luz para medicamentos fotosensibles.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'PhotoProtection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Foto protección', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'PhotoProtection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'PhotoProtection';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Indicador de mezcla estándar (1=Sí, 0=No); diferencia preparaciones estándar de personalizadas.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'StandardMix';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si es o no mezcla estándar', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'StandardMix';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'StandardMix';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NVARCHAR(400). Justificación clínica o administrativa de la preparación; soporte documentado para variaciones no estándar.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'Justification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'justificación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'Justification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'Justification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT FK. Identificador del Anatomical Therapeutic Chemical (ATC) o producto terminado; código internacional de clasificación farmacológica.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'ATCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del producto terminado', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'ATCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'ATCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT FK. Identificador de unidad de medida para concentración (mg/mL, g/100mL, etc.); referencia a Inventory.InventoryMeasurementUnit.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'ConcentrationMeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad de medida de la concentación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'ConcentrationMeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'ConcentrationMeasurementUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DECIMAL(18,4). Valor de concentración del fármaco o principio activo en el paquete; expresado en unidad especificada en ConcentrationMeasurementUnitId.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'Concentration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concentración', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'Concentration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'Concentration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT FK. Identificador del tipo de unidad de dosis (comprimido, ampolla, jeringa precargada, etc.); referencia a MixingStation.UnitDoseType.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'UnitDoseTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de tipo de unidad dosis', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'UnitDoseTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'UnitDoseTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(MAX). Consideraciones especiales o contraindicaciones clínicas; instrucciones críticas para manipulación, almacenamiento o administración.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'SpecialConsiderations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consideraciones Especiales', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'SpecialConsiderations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'SpecialConsiderations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(MAX). Instrucciones detalladas de preparación: pasos, tiempos, compatibilidades, técnica aséptica; guía para técnico farmacéutico.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'PreparationInstructions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Instrucciones de preparacion', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'PreparationInstructions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'PreparationInstructions';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Velocidad de infusión recomendada (mL/h o gotas/min); parámetro de administración intravenosa o enteral.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'InfusionSpeed';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Velocidad de infusión', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'InfusionSpeed';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'InfusionSpeed';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DECIMAL(5,2). Porcentaje o volumen de purga; volumen adicional para purgado de líneas intravenosas post-infusión.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'Purge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Purga', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'Purge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'Purge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Período o estabilidad en temperatura ambiente (días); validez del paquete a 20-25°C.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'EnvironmentalTemperatureTerm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Período de temperatura ambiental', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'EnvironmentalTemperatureTerm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'EnvironmentalTemperatureTerm';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Período o estabilidad en refrigeración (días); validez del paquete a 2-8°C.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'RefrigeratedTerm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Período refrigerado', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'RefrigeratedTerm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'RefrigeratedTerm';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT FK. Identificador de unidad de medida general del paquete (mL, g, unidades, etc.); referencia a Inventory.InventoryMeasurementUnit.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la unidad de medida', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Estado del registro: 1=Activado/Vigente, 0=Inactivo/Descontinuado; controla disponibilidad para prescripción.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Documento  1 - Activado  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME. Fecha y hora de última modificación del registro; auditoría de cambios en paquete personalizado.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(20). Usuario o login que realizó última modificación; trazabilidad de cambios.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME. Fecha y hora de creación del registro; auditoría de origen del paquete.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Creación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(20). Usuario o login que creó el registro; default=''''999'''' para creaciones automáticas.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DECIMAL(8,2). Peso total de la solución o paquete final (gramos); parámetro fisicoquímico de calidad.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'WeightTotalSolution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el peso total del paquete (producto)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'WeightTotalSolution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'WeightTotalSolution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DECIMAL(8,2). Volumen total incluyendo purga; volumen de preparación para asegurar dosis completa post-purgado.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'VolumeTotalOrderPurga';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el volumen total con purga del paquete (producto)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'VolumeTotalOrderPurga';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'VolumeTotalOrderPurga';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT FK. Identificador de unidad de medida para volumen total (mL, L, etc.); referencia a Inventory.InventoryMeasurementUnit.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'VolumeTotalOrderMeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad de medida para el volumen total pedido', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'VolumeTotalOrderMeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'VolumeTotalOrderMeasurementUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DECIMAL(8,2). Volumen total del paquete sin purga; cantidad de solución o suspensión a preparar.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'VolumeTotalOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el volumen total del paquete (producto)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'VolumeTotalOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'VolumeTotalOrder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DECIMAL(6,4). Osmolaridad total en mOsm/L; parámetro fisicoquímico crítico para NPT e infusiones.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'OsmolarityTotal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la osmolaridad total del paquete (producto)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'OsmolarityTotal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'OsmolarityTotal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Indicador de control máximo por período (1=Sí, 0=No); restricción regulatoria de renovaciones.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'MaximumControlPeriod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si tiene control maximo por periodo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'MaximumControlPeriod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'MaximumControlPeriod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Número de autorizaciones permitidas por pedido; límite de renovaciones sin reautorización.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'AuthorizationByOrderNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el numero de autorizaciones por pedido', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'AuthorizationByOrderNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'AuthorizationByOrderNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Indicador si tiene control de precios regulatorio (1=Sí, 0=No); PVP o precio máximo fijado.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'ProductWithPriceControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el paquete (producto) tiene control de precios', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'ProductWithPriceControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'ProductWithPriceControl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Indicador si es medicamento controlado o de control especial (1=Sí, 0=No); requiere vigilancia o reportes especiales.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'ProductControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el paquete (producto) es de control', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'ProductControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'ProductControl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT FK. Identificador del grupo de facturación o familia de cobro; agrupa paquetes para facturación conjunta.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'BillingGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del grupo de facturacion', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'BillingGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'BillingGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Indicador si está en POS (Punto de Servicio) o catálogo de prescripción (1=Sí, 0=No); disponibilidad para ordenes.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'POSProduct';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el paquete (producto) esta en el POS', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'POSProduct';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'POSProduct';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(20). Código SICE (Sistema de Información de Códigos de Medicamentos); código regulatorio nacional.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'CodeSICE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo SICE del paquete (producto)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'CodeSICE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'CodeSICE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT FK. Identificador del fabricante o laboratorio productor; trazabilidad de origen.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'ManufacturerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fabricante', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'ManufacturerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'ManufacturerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT FK. Identificador del subgrupo de producto (ej: antibióticos beta-lactámicos); clasificación farmacológica.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'ProductSubGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del subgrupo del paquete (producto)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'ProductSubGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'ProductSubGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT FK. Identificador del grupo de producto (ej: antibióticos, analgésicos); categorización principal.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'ProductGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del grupo de paquete (producto)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'ProductGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'ProductGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(20). Código alternativo secundario; código equivalente o histórico del paquete.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'CodeAlternativeTwo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Alternativo dos', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'CodeAlternativeTwo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'CodeAlternativeTwo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(30). Código alternativo principal; código sinónimo, antiguo o de compatibilidad con otros sistemas.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'CodeAlternative';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Alternativo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'CodeAlternative';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'CodeAlternative';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT FK. Identificador del nivel de riesgo (crítico, alto, medio, bajo); solo para medicamentos; referencia a Inventory.InventoryRiskLevel.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'RiskLevelId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Nivel de Riesgo, solo se llena si el tipo del paquete (producto) es "Item Medicamento"', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'RiskLevelId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'RiskLevelId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(MAX). Descripción larga y detallada del paquete; composición, indicaciones, información extendida.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion larga del paquete', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(200). Nombre corto del paquete personalizado; denominación comercial o código descriptivo para búsqueda.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del paquete, descripción corta', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(20). Código único del paquete personalizado; identificador principal en MixingStation.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del paquete', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT IDENTITY. Identificador único del registro de paquete personalizado (PK); clave primaria auto-incrementada.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del paquete', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de preparaciones personalizadas (mezclas magistrales, nutriciones parenterales, quimioterapias y otras soluciones) elaboradas en la estación de mezclas; define nombre, concentración, volumen, instrucciones de preparación, condiciones de almacenamiento y parámetros de control de cada fórmula personalizada.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de tiempo automática del sistema que registra la versión del registro para control de concurrencia y auditoría de cambios.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concentración del antibiótico en la preparación personalizada, expresada como texto (por ejemplo ''''500 mg/100 mL''''); identifica la dosificación del componente antibiótico en la mezcla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'ConcentrationAntibiotic';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'ConcentrationAntibiotic';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Volumen total efectivamente preparado de la mezcla o solución, expresado en la unidad de medida indicada en MeasurementPreparedId.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'VolumeTotalPrepared';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'VolumeTotalPrepared';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida (mL, L, etc.) correspondiente al volumen total preparado; referencia a la tabla maestra de unidades de medida.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'MeasurementPreparedId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PackagePersonalized', @level2type = N'COLUMN', @level2name = N'MeasurementPreparedId';
