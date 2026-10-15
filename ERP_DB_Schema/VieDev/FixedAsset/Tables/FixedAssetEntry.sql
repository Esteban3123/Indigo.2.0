CREATE TABLE [FixedAsset].[FixedAssetEntry] (
    [Id]                         INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OperatingUnitId]            INT             CONSTRAINT [DF_FixedAssetEntry_OperatingUnitId] DEFAULT ((14)) NOT NULL,
    [Code]                       VARCHAR (20)    NOT NULL,
    [EntryDate]                  DATE            NOT NULL,
    [EntryNumber]                VARCHAR (100)   NOT NULL,
    [AdquisitionType]            TINYINT         NOT NULL,
    [SupplierId]                 INT             NOT NULL,
    [SupplierDistributionLineId] INT             NOT NULL,
    [SupplierTypeId]             INT             NOT NULL,
    [CostCenterId]               INT             NULL,
    [AccountPayableId]           INT             NULL,
    [Description]                VARCHAR (1000)  NOT NULL,
    [GetLocationResponsible]     TINYINT         NOT NULL,
    [LocationId]                 INT             NULL,
    [ResponsibleId]              INT             NULL,
    [RoundService]               INT             NOT NULL,
    [InvoiceNumber]              VARCHAR (100)   NOT NULL,
    [InvoiceDate]                DATETIME        NOT NULL,
    [DayPeriod]                  INT             NOT NULL,
    [IcaPercentage]              NUMERIC (5, 3)  NOT NULL,
    [FreightValue]               NUMERIC (20)    NOT NULL,
    [FreightIVAPercentage]       NUMERIC (5, 2)  NOT NULL,
    [FreightIVAValue]            DECIMAL (20, 2) NULL,
    [Value]                      DECIMAL (20, 2) NULL,
    [ValueDiscount]              DECIMAL (20, 2) NULL,
    [ValueTax]                   DECIMAL (20, 2) NULL,
    [WithholdingTax]             DECIMAL (20, 2) NULL,
    [WithholdingICA]             DECIMAL (20, 2) NULL,
    [RetentionSource]            DECIMAL (20, 2) NULL,
    [RetentionOther]             DECIMAL (20, 2) NULL,
    [DeductionOther]             DECIMAL (20, 2) NULL,
    [TotalValue]                 DECIMAL (20, 2) NULL,
    [Status]                     TINYINT         NOT NULL,
    [CreationUser]               VARCHAR (20)    NOT NULL,
    [CreationDate]               DATETIME        NOT NULL,
    [ModificationUser]           VARCHAR (20)    NULL,
    [ModificationDate]           DATETIME        NULL,
    [ConfirmationUser]           VARCHAR (20)    NULL,
    [ConfirmationDate]           DATETIME        NULL,
    [AnnulmentUser]              VARCHAR (20)    NULL,
    [AnnulmentDate]              DATETIME        NULL,
    [TimeStamp]                  ROWVERSION      NOT NULL,
    [NumberContractLeasing]      VARCHAR (50)    NULL,
    [InitialDateLeasing]         DATE            NULL,
    [EndDateLeasing]             DATE            NULL,
    [CommitmentDetailId]         INT             NULL,
    [DocumentSupportId]          INT             NULL,
    [CurrencyId]                 INT             CONSTRAINT [DF__FixedAsse__Offic__6FD19574] DEFAULT ((1)) NOT NULL,
    [TaxRegistration]            INT             NULL,
    [EconomicActivityId]         INT             NULL,
    CONSTRAINT [PK_FixedAssetIngress] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BillingAuthorization] FOREIGN KEY ([DocumentSupportId]) REFERENCES [Billing].[BillingAuthorization] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_EquipmentIngress_SuppliersDistributionLines] FOREIGN KEY ([SupplierDistributionLineId]) REFERENCES [Common].[SuppliersDistributionLines] ([Id]),
    CONSTRAINT [FK_FixedAssetEntry_AccountPayable] FOREIGN KEY ([AccountPayableId]) REFERENCES [Payments].[AccountPayable] ([Id]),
    CONSTRAINT [FK_FixedAssetEntry_CommitmentDetail] FOREIGN KEY ([CommitmentDetailId]) REFERENCES [Budget].[CommitmentDetail] ([Id]),
    CONSTRAINT [FK_FixedAssetEntry_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_FixedAssetEntry_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_FixedAssetEntry_EconomicActivity] FOREIGN KEY ([EconomicActivityId]) REFERENCES [Common].[EconomicActivity] ([Id]),
    CONSTRAINT [FK_FixedAssetEntry_FixedAssetLocation] FOREIGN KEY ([LocationId]) REFERENCES [FixedAsset].[FixedAssetLocation] ([Id]),
    CONSTRAINT [FK_FixedAssetEntry_FixedAssetResponsible] FOREIGN KEY ([ResponsibleId]) REFERENCES [FixedAsset].[FixedAssetResponsible] ([Id]),
    CONSTRAINT [FK_FixedAssetEntry_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_FixedAssetEntry_Supplier] FOREIGN KEY ([SupplierId]) REFERENCES [Common].[Supplier] ([Id]),
    CONSTRAINT [FK_FixedAssetEntry_SupplierType] FOREIGN KEY ([SupplierTypeId]) REFERENCES [Common].[SupplierType] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetEntry] NOCHECK CONSTRAINT [FK_BillingAuthorization];


GO
ALTER TABLE [FixedAsset].[FixedAssetEntry] NOCHECK CONSTRAINT [FK_EquipmentIngress_SuppliersDistributionLines];


GO
ALTER TABLE [FixedAsset].[FixedAssetEntry] NOCHECK CONSTRAINT [FK_FixedAssetEntry_AccountPayable];


GO
ALTER TABLE [FixedAsset].[FixedAssetEntry] NOCHECK CONSTRAINT [FK_FixedAssetEntry_CommitmentDetail];


GO
ALTER TABLE [FixedAsset].[FixedAssetEntry] NOCHECK CONSTRAINT [FK_FixedAssetEntry_CostCenter];


GO
ALTER TABLE [FixedAsset].[FixedAssetEntry] NOCHECK CONSTRAINT [FK_FixedAssetEntry_Currency];


GO
ALTER TABLE [FixedAsset].[FixedAssetEntry] NOCHECK CONSTRAINT [FK_FixedAssetEntry_FixedAssetLocation];


GO
ALTER TABLE [FixedAsset].[FixedAssetEntry] NOCHECK CONSTRAINT [FK_FixedAssetEntry_FixedAssetResponsible];


GO
ALTER TABLE [FixedAsset].[FixedAssetEntry] NOCHECK CONSTRAINT [FK_FixedAssetEntry_OperatingUnit];


GO
ALTER TABLE [FixedAsset].[FixedAssetEntry] NOCHECK CONSTRAINT [FK_FixedAssetEntry_Supplier];


GO
ALTER TABLE [FixedAsset].[FixedAssetEntry] NOCHECK CONSTRAINT [FK_FixedAssetEntry_SupplierType];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Registro del IVA      1-IVA al costo control fiscal.     2-IVA descontable.     4-IVA al costo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntry', @level2type = N'COLUMN', @level2name = N'TaxRegistration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica de donde se va a obtener la Ubicacion y el Responsable  1 - Responsable y Ubicacion General  2 - Responsable y Ubicacion Especifico    Nota: Cuando sea tipo General solo se pedira el responsable y la ubicacion en la cabecera y todos los detalles van a quedar con este responsable y ubicacion    Si es Especifico entonces la ubicacion y el responsable solo se pediran en los detalles de los articulos uno a uno', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntry', @level2type = N'COLUMN', @level2name = N'GetLocationResponsible';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Tipo de Adquisicion   1 - Compra Directa  3 - Comodato  4 - Donacion  5 - Traspaso de Bienes  6 - Otro Concepto  7 - Leasing Financiero  8 - Comodato Tercerizado  9 - Renting Financiero  10 - Renting Operativo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntry', @level2type = N'COLUMN', @level2name = N'AdquisitionType';

