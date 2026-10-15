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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Totalizado de concentración de sustancias de medicamentos combinados', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'TotalSubstanceConcentration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la unidad de presentación UPR', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'UPRUnitsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida de concentración', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'ConcentrationMeasureUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de Concentración', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'ConcentrationQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Combinado | 1 = Si | 0 = No', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Combined';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'es apto para la reconstrucción | 1 = Si | 0 = No', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'SuitableForReconstitution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estabilidad | 1 = Si | 0 = No', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Stability';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Multidosis | 1 = Si | 0 = No', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Multidose';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'UNIRS', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'UNIRS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Condicionado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Conditioned';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Justificación clínica cuando el profesional define que el medicamento es PBS', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'ClinicalJustification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si el profesional define o no si el medicamento es PBS por condiciones clínicas', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'DefineProfessional';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si el medicamento es o no de alto costo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'HighCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si el medicamento requiere o no junta médica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'RequireMedicalBoard';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si el medicamento es o no Antibiotico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Antibiotic';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si asocia o no insumos o medicamentos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'HasSupplieMedicine';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de forma farmacéutica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'PharmaceuticalFormId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite saber si es de consumo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Consumption';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene la densidad del medicamento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Density';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene la osmolaridad del medicamento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Osmolarity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identifica si es un medicamento NPT', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'ProductNPT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Documento 1 - Activo 0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del grupo de facturación no pos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'BillingGroupNoPosId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el producto aplica todas las patologias POS', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'AllPOSPathologies';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el producto esta en el POS', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'POSProduct';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si es medicamento trazador', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'IndicatorDrug';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Exige justificacion de insumos / dispositivos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'JustificationOfInputs';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Exige justificacion de medicamentos especiales', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'JustificationForSpecialDrugs';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el producto funciona como insumo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'SupplieProduct';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el producto funciona como diluyente', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'DiluentProduct';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Realiza traslado sobrantes de productos en gestion hospitalario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'TransferSurplusProduct';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si realiza calculo automatico para el numero del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'AutomaticCalculation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Reacciones Adversa del medicamento en formato HTML', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'AdverseReactionsHtml';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Reacciones Adversa del medicamento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'AdverseReactions';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Precauciones del medicamento en formato HTML', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'PrecautionsHtml';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Precauciones del medicamento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Precautions';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contraindicaciones del producto en formato HTML  ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'ContraIndicationsHtml';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contraindicaciones del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'ContraIndications';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indicaciones del producto en formato HTML', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'IndicationsHtml';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indicaciones del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Indications';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Posologia o dosificacion del producto en formato HTML', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'DosageHtml';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Posologia o dosificacion del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Dosage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Advertencia del producto pero guardado en formato HTML', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'WarningHtml';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Advertencias del producto en texto plano', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Warning';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad de administracion, solo se llena si el tipo de formulacion es unidad de administracion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'AdministrationUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad de medida del volumen, solo se llena si el tipo de formulacion es Volumen o peso - Volumen', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'VolumeMeasureUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad del volumen del ATC, solo se llena si el tipo de formulacion es Volumen o peso - Volumen', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Volume';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Unidad de medida del peso, solo se llena si el tipo de formulacion es peso o peso - Volumen', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'WeightMeasureUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad de peso del ATC, solo se llena si el tipo de formulacion es peso o peso - Volumen', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Weight';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Peso  2 - Volumen  3 - Peso - Volumen  4 - Unidad de administración', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'FormulationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Horas maximas de estabilidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'StabilityMaximumHours';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Horas minima de estabilidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'StabilityMinimumHours';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del nivel de riesgo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'InventoryRiskLevelId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concentracion del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Concentration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Presentación del Medicamento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Presentations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del grupo farmacologico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'PharmacologicalGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la via de administracion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'AdministrationRouteId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre abreviado del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'AbbreviationName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del ATC', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del ATC', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del ATC. El valor se saca del formulario nuevo llamado ATC ya que la tabla como tal ATC fue renombrada por Medicamentos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'ATCEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la denominacion comun internacional', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'DCIId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del sistema de clasificacion anatómica, Terapéutica, Quimica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATC', @level2type = N'COLUMN', @level2name = N'Id';

