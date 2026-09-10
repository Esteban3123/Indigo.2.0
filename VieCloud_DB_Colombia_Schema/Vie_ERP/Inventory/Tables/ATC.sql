CREATE TABLE [Inventory].[ATC] (
    [Id]                           INT             IDENTITY (1, 1) NOT NULL,
    [DCIId]                        INT             NOT NULL,
    [ATCEntityId]                  INT             CONSTRAINT [DF_ATC_ATCEntityId] DEFAULT ((7)) NOT NULL,
    [Code]                         VARCHAR (20)    NOT NULL,
    [Name]                         VARCHAR (200)   NOT NULL,
    [AbbreviationName]             VARCHAR (100)   NOT NULL,
    [AdministrationRouteId]        INT             NOT NULL,
    [PharmacologicalGroupId]       INT             NOT NULL,
    [Presentations]                VARCHAR (160)   NULL,
    [Concentration]                VARCHAR (50)    NOT NULL,
    [InventoryRiskLevelId]         INT             NOT NULL,
    [StabilityMinimumHours]        INT             NOT NULL,
    [StabilityMaximumHours]        NUMERIC (18, 2) NOT NULL,
    [FormulationType]              TINYINT         NOT NULL,
    [Weight]                       NUMERIC (18, 2) NULL,
    [WeightMeasureUnit]            INT             NULL,
    [Volume]                       NUMERIC (18, 2) NULL,
    [VolumeMeasureUnit]            INT             NULL,
    [AdministrationUnitId]         INT             NULL,
    [Warning]                      VARCHAR (MAX)   NULL,
    [WarningHtml]                  VARCHAR (MAX)   NULL,
    [Dosage]                       VARCHAR (MAX)   NULL,
    [DosageHtml]                   VARCHAR (MAX)   NULL,
    [Indications]                  VARCHAR (MAX)   NULL,
    [IndicationsHtml]              VARCHAR (MAX)   NULL,
    [ContraIndications]            VARCHAR (MAX)   NULL,
    [ContraIndicationsHtml]        VARCHAR (MAX)   NULL,
    [Precautions]                  VARCHAR (MAX)   NULL,
    [PrecautionsHtml]              VARCHAR (MAX)   NULL,
    [AdverseReactions]             VARCHAR (MAX)   NULL,
    [AdverseReactionsHtml]         VARCHAR (MAX)   NULL,
    [AutomaticCalculation]         BIT             NOT NULL,
    [TransferSurplusProduct]       BIT             NOT NULL,
    [DiluentProduct]               BIT             NOT NULL,
    [SupplieProduct]               BIT             CONSTRAINT [DF_ATC_SupplieProduct] DEFAULT ((0)) NOT NULL,
    [JustificationForSpecialDrugs] BIT             NOT NULL,
    [JustificationOfInputs]        BIT             NOT NULL,
    [IndicatorDrug]                BIT             NOT NULL,
    [POSProduct]                   BIT             NULL,
    [AllPOSPathologies]            BIT             NULL,
    [BillingGroupNoPosId]          INT             NULL,
    [Status]                       BIT             NOT NULL,
    [CreationUser]                 VARCHAR (20)    CONSTRAINT [DF_ATC_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]                 DATETIME        CONSTRAINT [DF_ATC_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]             VARCHAR (20)    NULL,
    [ModificationDate]             DATETIME        NULL,
    [TimeStamp]                    ROWVERSION      NOT NULL,
    [ProductNPT]                   BIT             CONSTRAINT [DF_ATC_ProductNPT] DEFAULT ((0)) NOT NULL,
    [Osmolarity]                   DECIMAL (18, 2) NULL,
    [Density]                      DECIMAL (6, 4)  NULL,
    [Consumption]                  BIT             NULL,
    [PharmaceuticalFormId]         INT             NULL,
    [HasSupplieMedicine]           BIT             NULL,
    [Antibiotic]                   BIT             NULL,
    [RequireMedicalBoard]          BIT             NULL,
    [HighCost]                     BIT             NULL,
    [DefineProfessional]           BIT             NULL,
    [ClinicalJustification]        VARCHAR (2000)  NULL,
    [Conditioned]                  BIT             CONSTRAINT [DF_ATC_Conditioned] DEFAULT ((0)) NOT NULL,
    [UNIRS]                        BIT             CONSTRAINT [DF_ATC_UNIRS] DEFAULT ((0)) NOT NULL,
    [Multidose]                    BIT             NOT NULL,
    [Stability]                    BIT             NOT NULL,
    [SuitableForReconstitution]    BIT             NULL,
    [Combined]                     BIT             CONSTRAINT [DF__ATC__Combined__3954BDDB] DEFAULT ((0)) NOT NULL,
    [ConcentrationQuantity]        DECIMAL (18, 2) CONSTRAINT [DF__ATC__Concentrati__3A48E214] DEFAULT ((0)) NOT NULL,
    [ConcentrationMeasureUnitId]   INT             CONSTRAINT [DF__ATC__Concentrati__3B3D064D] DEFAULT ((2)) NULL,
    [UPRUnitsId]                   INT             NULL,
    [TotalSubstanceConcentration]  VARCHAR (50)    NULL,
    [ComponentType]                TINYINT         NULL,
    CONSTRAINT [PK_ATC__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ATC_AdministrationRoute] FOREIGN KEY ([AdministrationRouteId]) REFERENCES [Inventory].[AdministrationRoute] ([Id]),
    CONSTRAINT [FK_ATC_ATCEntity] FOREIGN KEY ([ATCEntityId]) REFERENCES [Inventory].[ATCEntity] ([Id]),
    CONSTRAINT [FK_ATC_BillingGroup] FOREIGN KEY ([BillingGroupNoPosId]) REFERENCES [Billing].[BillingGroup] ([Id]),
    CONSTRAINT [FK_ATC_DCI] FOREIGN KEY ([DCIId]) REFERENCES [Inventory].[DCI] ([Id]),
    CONSTRAINT [FK_ATC_InventoryRiskLevel] FOREIGN KEY ([InventoryRiskLevelId]) REFERENCES [Inventory].[InventoryRiskLevel] ([Id]),
    CONSTRAINT [FK_ATC_PharmaceuticalForm] FOREIGN KEY ([PharmaceuticalFormId]) REFERENCES [Inventory].[PharmaceuticalForm] ([Id]),
    CONSTRAINT [FK_ATC_PharmacologicalGroup] FOREIGN KEY ([PharmacologicalGroupId]) REFERENCES [Inventory].[PharmacologicalGroup] ([Id]),
    CONSTRAINT [FK_ATC_UnitMeasure_AdministrationUnit] FOREIGN KEY ([AdministrationUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id]),
    CONSTRAINT [FK_ATC_UnitMeasure_Concentration] FOREIGN KEY ([ConcentrationMeasureUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id]),
    CONSTRAINT [FK_ATC_UnitMeasure_Volume] FOREIGN KEY ([VolumeMeasureUnit]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id]),
    CONSTRAINT [FK_ATC_UnitMeasure_Weight] FOREIGN KEY ([WeightMeasureUnit]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id]),
    CONSTRAINT [FK_ATC_UPRUnits] FOREIGN KEY ([UPRUnitsId]) REFERENCES [Inventory].[UPRUnits] ([Id])
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



GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_ATC_PharmaceuticalFormId]
    ON [Inventory].[ATC]([PharmaceuticalFormId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_ATC__Code]
    ON [Inventory].[ATC]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de componente nutricional: 1=Macronutriente (proteínas, carbohidratos, grasas), 2=Micronutriente (vitaminas, minerales). TINYINT, clasificación de nutrientes en medicamentos/suplementos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'ComponentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Macronutriente
2 - Micronutrriente', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'ComponentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'ComponentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concentración total integrada de sustancias activas en medicamentos combinados o compuestos. VARCHAR(50), sumatoria de componentes.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'TotalSubstanceConcentration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Totalizado de concentración de sustancias de medicamentos combinados', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'TotalSubstanceConcentration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'TotalSubstanceConcentration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad de presentación/referencia UPR (Unidad de Presentación). INT, FK a InventoryMeasurementUnit, estandariza presentación comercial.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'UPRUnitsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la unidad de presentación UPR', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'UPRUnitsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'UPRUnitsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de unidad de medida para concentración (mg/mL, mg/dL, %). INT, FK a InventoryMeasurementUnit, por defecto=2.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'ConcentrationMeasureUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de medida de concentración', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'ConcentrationMeasureUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'ConcentrationMeasureUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad numérica de concentración del principio activo. DECIMAL(18,2), ej: 500 (mg), 0.5 (%), potencia del fármaco.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'ConcentrationQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad de Concentración', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'ConcentrationQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'ConcentrationQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de medicamento combinado/compuesto: 1=Sí (múltiples principios activos), 0=No (monofármaco). BIT, búsqueda: medicamento combinado, mezcla.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Combined';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Combinado | 1 = Si | 0 = No', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Combined';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Combined';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Aptitud para reconstitución/disolución antes de administración: 1=Apto (polvo, liofilizado), 0=No. BIT, búsqueda: reconstituir, preparación, diluir.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'SuitableForReconstitution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'es apto para la reconstrucción | 1 = Si | 0 = No', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'SuitableForReconstitution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'SuitableForReconstitution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estabilidad del medicamento después de abrir/preparar: 1=Tiene período de estabilidad definido, 0=Sin limitación. BIT, búsqueda: caducidad, degradación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Stability';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estabilidad | 1 = Si | 0 = No', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Stability';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Stability';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de presentación multidosis (múltiples administraciones): 1=Sí (frasco x10 dosis), 0=No (monodosis). BIT, búsqueda: multidosis, monodosis.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Multidose';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Multidosis | 1 = Si | 0 = No', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Multidose';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Multidose';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sistema UNIRS (registro/control específico): 1=Aplica, 0=No. BIT, marcaje de productos bajo vigilancia especial.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'UNIRS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'UNIRS', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'UNIRS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'UNIRS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Medicamento sujeto a condiciones especiales de dispensación/uso: 1=Sí (requiere requisitos adicionales), 0=No. BIT, búsqueda: condicionado, restricción.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Conditioned';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Condicionado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Conditioned';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Conditioned';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Argumento clínico-médico para clasificar medicamento como PBS (Plan de Beneficios en Salud) o de alto costo. VARCHAR(2000), justificación por profesional.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'ClinicalJustification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificación clínica cuando el profesional define que el medicamento es PBS', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'ClinicalJustification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'ClinicalJustification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de decisión profesional sobre clasificación PBS: 1=Profesional define si es PBS por condiciones clínicas, 0=Predeterminado. BIT.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'DefineProfessional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si el profesional define o no si el medicamento es PBS por condiciones clínicas', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'DefineProfessional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'DefineProfessional';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de medicamento de alto costo: 1=Sí (requiere autorización/trámites especiales), 0=No. BIT, búsqueda: alto costo, medicamento costoso.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'HighCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si el medicamento es o no de alto costo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'HighCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'HighCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Requisito de aprobación por junta médica: 1=Sí (requiere autorización colegiada), 0=No. BIT, búsqueda: junta médica, autorización.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'RequireMedicalBoard';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si el medicamento requiere o no junta médica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'RequireMedicalBoard';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'RequireMedicalBoard';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación farmacológica: 1=Es antibiótico, 0=No. BIT, búsqueda: antibiótico, antimicrobiano, infección.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Antibiotic';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si el medicamento es o no Antibiotico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Antibiotic';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Antibiotic';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Asociación con insumos/dispositivos médicos: 1=Sí (lleva insumos asociados), 0=No. BIT, búsqueda: insumo, dispositivo, acceorio.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'HasSupplieMedicine';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si asocia o no insumos o medicamentos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'HasSupplieMedicine';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'HasSupplieMedicine';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de forma farmacéutica (tableta, inyectable, solución, crema, etc.). INT, FK a PharmaceuticalForm.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'PharmaceuticalFormId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de forma farmacéutica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'PharmaceuticalFormId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'PharmaceuticalFormId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de medicamento/insumo de consumo hospitalario: 1=Sí, 0=No. BIT, búsqueda: consumo, stock, inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Consumption';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite saber si es de consumo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Consumption';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Consumption';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Densidad física del medicamento (g/mL, kg/L). DECIMAL(6,4), parámetro fisicoquímico para cálculos de volumen.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Density';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene la densidad del medicamento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Density';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Density';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Osmolaridad del medicamento (mOsm/L), relevante en inyectables/sueros. DECIMAL(18,2), compatibilidad vascular.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Osmolarity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene la osmolaridad del medicamento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Osmolarity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Osmolarity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Producto para Nutrición Parenteral Total: 1=Sí (componente NPT), 0=No. BIT, búsqueda: NPT, nutrición parenteral, soporte nutricional.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'ProductNPT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identifica si es un medicamento NPT', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'ProductNPT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'ProductNPT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal automática de última modificación del registro. TIMESTAMP, auditoria de cambios, versioning.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación del medicamento. DATETIME, búsqueda: modificado, actualizado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario que modificó el registro. VARCHAR(20), auditoría de cambios.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro. DATETIME, búsqueda: creado, registrado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario que creó el registro. VARCHAR(20), auditoría, trazabilidad.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del medicamento en el sistema: 1=Activo (disponible), 0=Inactivo (archivado/retirado). BIT, búsqueda: activo, inactivo, disponible.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Documento 1 - Activo 0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo de facturación NO POS (fuera del plan). INT, FK a BillingGroup, para tarificación privada.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'BillingGroupNoPosId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del grupo de facturación no pos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'BillingGroupNoPosId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'BillingGroupNoPosId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Aplicabilidad POS a todas las patologías: 1=Aplica a todas, 0=Patologías seleccionadas. BIT, búsqueda: POS universal.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'AllPOSPathologies';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el producto aplica todas las patologias POS', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'AllPOSPathologies';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'AllPOSPathologies';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cobertura del Plan Obligatorio de Salud (POS): 1=Sí, 0=No. BIT, búsqueda: POS, cubierto, asegurado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'POSProduct';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el producto esta en el POS', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'POSProduct';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'POSProduct';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Medicamento trazador (indicador de calidad/acceso): 1=Sí, 0=No. BIT, marcador de fármacos monitoreados.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'IndicatorDrug';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si es medicamento trazador', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'IndicatorDrug';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'IndicatorDrug';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Exigencia de justificación clínica para insumos/dispositivos: 1=Sí (requiere argumento), 0=No. BIT.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'JustificationOfInputs';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Exige justificacion de insumos / dispositivos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'JustificationOfInputs';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'JustificationOfInputs';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Exigencia de justificación para medicamentos especiales/restringidos: 1=Sí (requiere aprobación), 0=No. BIT.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'JustificationForSpecialDrugs';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Exige justificacion de medicamentos especiales', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'JustificationForSpecialDrugs';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'JustificationForSpecialDrugs';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación como insumo/supply: 1=Funciona como insumo, 0=No. BIT, búsqueda: insumo, material, supply.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'SupplieProduct';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el producto funciona como insumo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'SupplieProduct';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'SupplieProduct';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación como diluyente: 1=Funciona como diluyente (suero, agua destilada), 0=No. BIT, búsqueda: diluyente.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'DiluentProduct';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el producto funciona como diluyente', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'DiluentProduct';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'DiluentProduct';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Política de traslado de sobrantes en gestión hospitalaria: 1=Sí (se trasladan remanentes), 0=No. BIT.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'TransferSurplusProduct';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Realiza traslado sobrantes de productos en gestion hospitalario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'TransferSurplusProduct';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'TransferSurplusProduct';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cálculo automático de identificativos: 1=Sí (genera ID automático), 0=Manual. BIT, búsqueda: generación automática.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'AutomaticCalculation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si realiza calculo automatico para el numero del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'AutomaticCalculation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'AutomaticCalculation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reacciones adversas del medicamento en formato HTML (enriquecido con estilos). VARCHAR(MAX), presentación web.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'AdverseReactionsHtml';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Reacciones Adversa del medicamento en formato HTML', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'AdverseReactionsHtml';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'AdverseReactionsHtml';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reacciones adversas/efectos indeseados del medicamento en texto plano. VARCHAR(MAX), búsqueda: reacción adversa, efecto secundario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'AdverseReactions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Reacciones Adversa del medicamento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'AdverseReactions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'AdverseReactions';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Precauciones de uso en formato HTML. VARCHAR(MAX), presentación web.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'PrecautionsHtml';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Precauciones del medicamento en formato HTML', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'PrecautionsHtml';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'PrecautionsHtml';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Precauciones y cuidados especiales en administración/manejo. VARCHAR(MAX), búsqueda: precaución, advertencia.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Precautions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Precauciones del medicamento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Precautions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Precautions';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contraindicaciones en formato HTML. VARCHAR(MAX), presentación web.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'ContraIndicationsHtml';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contraindicaciones del producto en formato HTML  ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'ContraIndicationsHtml';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'ContraIndicationsHtml';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Situaciones en que NO debe administrarse el medicamento. VARCHAR(MAX), búsqueda: contraindicación, prohibido.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'ContraIndications';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contraindicaciones del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'ContraIndications';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'ContraIndications';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicaciones clínicas en formato HTML. VARCHAR(MAX), presentación web.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'IndicationsHtml';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicaciones del producto en formato HTML', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'IndicationsHtml';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'IndicationsHtml';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usos clínicos autorizados, patologías/síntomas para los que está indicado. VARCHAR(MAX), búsqueda: indicación, enfermedad, síntoma.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Indications';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicaciones del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Indications';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Indications';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Posología/dosis en formato HTML. VARCHAR(MAX), presentación web.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'DosageHtml';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Posologia o dosificacion del producto en formato HTML', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'DosageHtml';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'DosageHtml';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Posología: dosis, frecuencia, duración de administración. VARCHAR(MAX), búsqueda: dosis, posología, cantidad.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Dosage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Posologia o dosificacion del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Dosage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Dosage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Advertencias clínicas en formato HTML. VARCHAR(MAX), presentación web.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'WarningHtml';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Advertencia del producto pero guardado en formato HTML', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'WarningHtml';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'WarningHtml';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Advertencias importantes en texto plano. VARCHAR(MAX), búsqueda: advertencia, alerta, riesgo.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Warning';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Advertencias del producto en texto plano', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Warning';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Warning';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de unidad de administración (comprimido, ampolla, inyección). INT, FK a InventoryMeasurementUnit, solo si FormulationType=4.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'AdministrationUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad de administracion, solo se llena si el tipo de formulacion es unidad de administracion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'AdministrationUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'AdministrationUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de unidad de medida de volumen (mL, L). INT, FK a InventoryMeasurementUnit, solo si FormulationType=2 o 3.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'VolumeMeasureUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad de medida del volumen, solo se llena si el tipo de formulacion es Volumen o peso - Volumen', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'VolumeMeasureUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'VolumeMeasureUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de volumen del medicamento. NUMERIC(18,2), solo si FormulationType=Volumen o Peso-Volumen.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Volume';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad del volumen del ATC, solo se llena si el tipo de formulacion es Volumen o peso - Volumen', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Volume';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Volume';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de unidad de medida de peso (mg, g, kg). INT, FK a InventoryMeasurementUnit, solo si FormulationType=1 o 3.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'WeightMeasureUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Unidad de medida del peso, solo se llena si el tipo de formulacion es peso o peso - Volumen', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'WeightMeasureUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'WeightMeasureUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de peso del medicamento. NUMERIC(18,2), solo si FormulationType=Peso o Peso-Volumen.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Weight';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad de peso del ATC, solo se llena si el tipo de formulacion es peso o peso - Volumen', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Weight';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Weight';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de formulación: 1=Peso (mg/g), 2=Volumen (mL), 3=Peso-Volumen, 4=Unidad de administración. TINYINT.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'FormulationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Peso  2 - Volumen  3 - Peso - Volumen  4 - Unidad de administración', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'FormulationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'FormulationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Límite superior de estabilidad después de preparación/apertura. INT, búsqueda: caducidad, validez.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'StabilityMaximumHours';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Horas maximas de estabilidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'StabilityMaximumHours';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'StabilityMaximumHours';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Límite inferior de estabilidad después de preparación/apertura. INT, período de uso mínimo.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'StabilityMinimumHours';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Horas minima de estabilidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'StabilityMinimumHours';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'StabilityMinimumHours';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del nivel de riesgo de manipulación/almacenamiento. INT, FK a InventoryRiskLevel.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'InventoryRiskLevelId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del nivel de riesgo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'InventoryRiskLevelId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'InventoryRiskLevelId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concentración del principio activo. VARCHAR(50), ej: 500mg/mL, 2.5%, potencia nominal.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Concentration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concentracion del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Concentration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Concentration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presentaciones comerciales disponibles del medicamento. VARCHAR(160), ej: caja x10, frasco x100mL.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Presentations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Presentación del Medicamento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Presentations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Presentations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo farmacológico (analgésico, antibiótico, etc.). INT, FK a PharmacologicalGroup.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'PharmacologicalGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del grupo farmacologico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'PharmacologicalGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'PharmacologicalGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de vía de administración (oral, IV, IM, tópica, etc.). INT, FK a AdministrationRoute.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'AdministrationRouteId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la via de administracion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'AdministrationRouteId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'AdministrationRouteId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre abreviado/comercial del medicamento. VARCHAR(100), búsqueda: alias, marca comercial.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'AbbreviationName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre abreviado del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'AbbreviationName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'AbbreviationName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo/genérico del medicamento. VARCHAR(200), búsqueda: medicamento, fármaco, droga.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del ATC', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código ATC de clasificación anatómica-terapéutica-química. VARCHAR(20), búsqueda: código ATC, clasificación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del ATC', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la entidad ATC (clasificación del sistema). INT, FK a ATCEntity, default=7.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'ATCEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del ATC. El valor se saca del formulario nuevo llamado ATC ya que la tabla como tal ATC fue renombrada por Medicamentos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'ATCEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'ATCEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la Denominación Común Internacional (nombre genérico). INT, FK a DCI, búsqueda: DCI, principio activo.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'DCIId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la denominacion comun internacional', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'DCIId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'DCIId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro ATC (Classification Anatomical Therapeutic Chemical). INT, PK, autonumérico, búsqueda: código sistema.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del sistema de clasificacion anatómica, Terapéutica, Quimica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo maestro de medicamentos e insumos con clasificación ATC (Anatomical Therapeutic Chemical). Contiene la información técnica, farmacológica, clínica y de inventario de cada producto: nombre, concentración, vía de administración, grupo farmacológico, advertencias, indicaciones, contraindicaciones, reacciones adversas y parámetros de preparación/estabilidad utilizados en la gestión de farmacia, formulación y facturación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC';
