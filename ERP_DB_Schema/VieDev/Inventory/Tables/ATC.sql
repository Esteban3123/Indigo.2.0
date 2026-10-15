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
ALTER TABLE [Inventory].[ATC] NOCHECK CONSTRAINT [FK_ATC_AdministrationRoute];


GO
ALTER TABLE [Inventory].[ATC] NOCHECK CONSTRAINT [FK_ATC_ATCEntity];


GO
ALTER TABLE [Inventory].[ATC] NOCHECK CONSTRAINT [FK_ATC_BillingGroup];


GO
ALTER TABLE [Inventory].[ATC] NOCHECK CONSTRAINT [FK_ATC_DCI];


GO
ALTER TABLE [Inventory].[ATC] NOCHECK CONSTRAINT [FK_ATC_InventoryRiskLevel];


GO
ALTER TABLE [Inventory].[ATC] NOCHECK CONSTRAINT [FK_ATC_PharmaceuticalForm];


GO
ALTER TABLE [Inventory].[ATC] NOCHECK CONSTRAINT [FK_ATC_PharmacologicalGroup];


GO
ALTER TABLE [Inventory].[ATC] NOCHECK CONSTRAINT [FK_ATC_UnitMeasure_AdministrationUnit];


GO
ALTER TABLE [Inventory].[ATC] NOCHECK CONSTRAINT [FK_ATC_UnitMeasure_Volume];


GO
ALTER TABLE [Inventory].[ATC] NOCHECK CONSTRAINT [FK_ATC_UnitMeasure_Weight];


GO
ALTER TABLE [Inventory].[ATC] NOCHECK CONSTRAINT [FK_ATC_UPRUnits];


GO
CREATE NONCLUSTERED INDEX [IX_ATC_PharmaceuticalFormId]
    ON [Inventory].[ATC]([PharmaceuticalFormId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_ATC__Code]
    ON [Inventory].[ATC]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'1 - Macronutriente
2 - Micronutrriente', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'ComponentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contraindicaciones del producto en formato HTML  ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'ContraIndicationsHtml';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Peso  2 - Volumen  3 - Peso - Volumen  4 - Unidad de administración', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'FormulationType';

