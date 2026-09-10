CREATE TABLE [FixedAsset].[FixedAssetPhysicalAsset] (
    [Id]                       INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ItemId]                   INT             NOT NULL,
    [MainAccountId]            INT             NOT NULL,
    [Serie]                    VARCHAR (50)    NOT NULL,
    [Plate]                    VARCHAR (50)    NOT NULL,
    [LocationId]               INT             NOT NULL,
    [ResponsibleId]            INT             NOT NULL,
    [SupplierId]               INT             NULL,
    [HistoricalValue]          DECIMAL (18, 2) NOT NULL,
    [FairValue]                DECIMAL (18, 2) NOT NULL,
    [TrademarkId]              INT             NOT NULL,
    [Model]                    VARCHAR (100)   NOT NULL,
    [PolicyId]                 INT             NULL,
    [HandlesWarranty]          BIT             NOT NULL,
    [WarrantyExpirationDate]   DATE            NULL,
    [AdquisitionDate]          DATE            NOT NULL,
    [InstallationDate]         DATE            NULL,
    [Depreciate]               BIT             NOT NULL,
    [HasOutput]                BIT             CONSTRAINT [DF_FixedAssetPhysicalAsset_HasOutput] DEFAULT ((0)) NOT NULL,
    [OutputDate]               DATE            NULL,
    [OutputRefund]             BIT             CONSTRAINT [DF_FixedAssetPhysicalAsset_OutputRefund] DEFAULT ((0)) NOT NULL,
    [Observation]              VARCHAR (1000)  NULL,
    [StatusAssetId]            INT             NOT NULL,
    [Status]                   BIT             NOT NULL,
    [TimeStamp]                ROWVERSION      NOT NULL,
    [AdquisitionType]          TINYINT         CONSTRAINT [DF_FixedAssetPhysicalAsset_AdquisitionType] DEFAULT ((1)) NOT NULL,
    [NumberContractLeasing]    VARCHAR (50)    NULL,
    [InitialDateLeasing]       DATE            NULL,
    [EndDateLeasing]           DATE            NULL,
    [ApplyMinimunAmount]       BIT             CONSTRAINT [DF_FixedAssetPhysicalAsset_ApplyMinimunAmount] DEFAULT ((0)) NOT NULL,
    [AdquisitionTypeReal]      TINYINT         CONSTRAINT [DF_FixedAssetPhysicalAsset_AdquisitionType1] DEFAULT ((1)) NOT NULL,
    [PurchaseDate]             DATE            NULL,
    [EntryNumber]              VARCHAR (20)    NULL,
    [VoucherTransactionNumber] VARCHAR (20)    NULL,
    [HasReclassified]          BIT             CONSTRAINT [DF_FixedAssetPhysicalAsset_HasReclassified] DEFAULT ((0)) NOT NULL,
    [EndDateLeasingExecuted]   DATE            NULL,
    [RecoverableValue]         DECIMAL (18, 2) CONSTRAINT [DF_FixedAssetPhysicalAsset_RecoverableValue] DEFAULT ((0)) NOT NULL,
    [HasHighTech]              BIT             CONSTRAINT [DF_FixedAssetPhysicalAsset_HasHighTech] DEFAULT ((0)) NOT NULL,
    [NetHistoricalValue]       DECIMAL (18, 2) CONSTRAINT [DF__FixedAsse__NetHi__2DB9B954] DEFAULT ((0)) NOT NULL,
    [FinancialDiscount]        DECIMAL (18, 2) CONSTRAINT [DF__FixedAsse__Finan__2EADDD8D] DEFAULT ((0)) NOT NULL,
    [Amortize]                 BIT             CONSTRAINT [DF__FixedAsse__Amort__2E78D363] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_FixedAssetPhysicalAsset__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetPhysicalAsset_FixedAssetItem] FOREIGN KEY ([ItemId]) REFERENCES [FixedAsset].[FixedAssetItem] ([Id]),
    CONSTRAINT [FK_FixedAssetPhysicalAsset_FixedAssetLocation] FOREIGN KEY ([LocationId]) REFERENCES [FixedAsset].[FixedAssetLocation] ([Id]),
    CONSTRAINT [FK_FixedAssetPhysicalAsset_FixedAssetPoliza] FOREIGN KEY ([PolicyId]) REFERENCES [FixedAsset].[FixedAssetPolicy] ([Id]),
    CONSTRAINT [FK_FixedAssetPhysicalAsset_FixedAssetResponsible] FOREIGN KEY ([ResponsibleId]) REFERENCES [FixedAsset].[FixedAssetResponsible] ([Id]),
    CONSTRAINT [FK_FixedAssetPhysicalAsset_FixedAssetStatusAsset] FOREIGN KEY ([StatusAssetId]) REFERENCES [FixedAsset].[FixedAssetStatusAsset] ([Id]),
    CONSTRAINT [FK_FixedAssetPhysicalAsset_FixedAssetTrademark] FOREIGN KEY ([TrademarkId]) REFERENCES [FixedAsset].[FixedAssetTrademark] ([Id]),
    CONSTRAINT [FK_FixedAssetPhysicalAsset_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetPhysicalAsset_Supplier] FOREIGN KEY ([SupplierId]) REFERENCES [Common].[Supplier] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetPhysicalAsset] NOCHECK CONSTRAINT [FK_FixedAssetPhysicalAsset_FixedAssetStatusAsset];


GO
ALTER TABLE [FixedAsset].[FixedAssetPhysicalAsset] NOCHECK CONSTRAINT [FK_FixedAssetPhysicalAsset_FixedAssetTrademark];




GO



GO



GO



GO



GO
ALTER TABLE [FixedAsset].[FixedAssetPhysicalAsset] NOCHECK CONSTRAINT [FK_FixedAssetPhysicalAsset_FixedAssetStatusAsset];


GO
ALTER TABLE [FixedAsset].[FixedAssetPhysicalAsset] NOCHECK CONSTRAINT [FK_FixedAssetPhysicalAsset_FixedAssetTrademark];


GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_FixedAssetPhysicalAsset__Plate]
    ON [FixedAsset].[FixedAssetPhysicalAsset]([Plate] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el activo se amortiza (1=Sí, 0=No). Campo BIT que controla la depreciación/amortización contable del bien.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'Amortize';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Amortiza | 1 = Si | 0 = No | ', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'Amortize';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'Amortize';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descuento financiero aplicado al activo. Valor decimal (18,2) que reduce el valor histórico del bien.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'FinancialDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descuento financiero', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'FinancialDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'FinancialDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor histórico neto del activo (valor histórico menos descuento financiero). Decimal (18,2), usado para valuación contable.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'NetHistoricalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor histórico neto(valor histórico - valor descuento financiero)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'NetHistoricalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'NetHistoricalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el activo es de alta tecnología (1=Sí, 0=No). Campo BIT para clasificar bienes tecnológicos avanzados.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'HasHighTech';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiene alta tecnología | 1 = Si | 0 = No |', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'HasHighTech';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'HasHighTech';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor recuperable del activo. Decimal (18,2) que representa el importe que se espera recuperar al dar de baja el bien.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'RecoverableValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor recuperable', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'RecoverableValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'RecoverableValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de ejecución efectiva del fin del contrato de Leasing en el aplicativo. DATE, registro del cierre real del contrato.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'EndDateLeasingExecuted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en la cual se ejecuto la finalización del contrato de Leasing desde el aplicativo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'EndDateLeasingExecuted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'EndDateLeasingExecuted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el activo ingresado por Leasing ya fue reclasificado (1=Sí, 0=No). Campo BIT para control de reclasificación.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'HasReclassified';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el Activo (Ingresado por Leasing) ya fue reclasificado.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'HasReclassified';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'HasReclassified';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del comprobante de egreso con el cual se pagó el activo. VARCHAR(20), referencia al documento de pago.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'VoucherTransactionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del comprobante de egreso con el cual se paga el activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'VoucherTransactionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'VoucherTransactionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de documento de ingreso del activo. VARCHAR(20), identificador del asiento contable de entrada del bien.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'EntryNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de documento de ingreso de activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'EntryNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'EntryNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de compra del activo, derivada de la fecha del documento de ingreso. DATE, origen de la adquisición.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'PurchaseDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de compra, dada por la fecha del documento de ingreso de activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'PurchaseDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'PurchaseDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de adquisición específico (1=Compra Directa, 3=Comodato, 4=Donación, 5=Traspaso, 6=Otro, 7=Leasing Financiero). TINYINT, clasificación real del ingreso.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'AdquisitionTypeReal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Adquisición  Especirfica el Tipo de Adquisicion   1 - Compra Directa  3 - Comodato  4 - Donadacion  5 - Traspaso de Bienes  6 - Otro Concepto  7 - Leasing Financiero', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'AdquisitionTypeReal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'AdquisitionTypeReal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si se deprecia como Activo Fijo de Mínima Cuantía según Decreto 1625/2016 (1=Sí, 0=No). Campo BIT normativa tributaria.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'ApplyMinimunAmount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el activo se deprecia como Activo Fijo de Mínima Cuantía de acuerdo con el  artículo 1.2.1.18.5 del Decreto 1625 de 2016 Único Reglamentario en Materia Tributaria', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'ApplyMinimunAmount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'ApplyMinimunAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha final contractual del Leasing. DATE, vencimiento pactado del contrato de arrendamiento financiero.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'EndDateLeasing';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Final de Leasing', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'EndDateLeasing';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'EndDateLeasing';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicial del Leasing. DATE, inicio del período de arrendamiento financiero del activo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'InitialDateLeasing';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inicial de Leasing', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'InitialDateLeasing';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'InitialDateLeasing';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del contrato de Leasing. VARCHAR(50), identificador único del acuerdo de arrendamiento.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'NumberContractLeasing';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del contrato de Leasing', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'NumberContractLeasing';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'NumberContractLeasing';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de adquisición (1=Compra, 3=Comodato, 4=Donación, 5=Traspaso, 6=Otro, 7=Leasing Financiero, 8=Comodato Tercerizado, 9=Renting Financiero, 10=Renting Operativo). TINYINT.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'AdquisitionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Tipo de Adquisicion   1 - Compra Directa  3 - Comodato  4 - Donacion  5 - Traspaso de Bienes  6 - Otro Concepto  7 - Leasing Financiero  8 - Comodato Tercerizado  9 - Renting Financiero  10 - Renting Operativo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'AdquisitionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'AdquisitionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal del evento (creación, modificación, registro). TIMESTAMP automático que captura instante exacto del cambio.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del activo (1=Activo, 0=Inactivo). Campo BIT para control de vigencia operativa del bien.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el estado del activo  1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del estado del activo. INT, FK a [FixedAssetStatusAsset], clasificación del estado (activo, baja, dañado, etc.).', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'StatusAssetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Estado del Activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'StatusAssetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'StatusAssetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones sobre el activo. VARCHAR(1000), notas descriptivas visibles en reportes de bienes fijos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la observación de los artículos, esta observación es la que posteriormente podemos ver en la tabla de Activos Fijos (PhysicalAsset)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si la salida fue por devolución (1=Sí, 0=No). Campo BIT que marca devoluciones de activos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'OutputRefund';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica que la salida fue dada por una devolucion', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'OutputRefund';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'OutputRefund';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de salida del activo. DATE, momento en que el bien se dio de baja o fue retirado.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'OutputDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la Fecha de salida del Activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'OutputDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'OutputDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el activo tiene salida registrada (1=Sí, 0=No). Campo BIT para control de bienes en uso vs. dados de baja.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'HasOutput';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el Activo tiene Salida', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'HasOutput';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'HasOutput';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si deprecia (1=Sí, 0=No). Campo BIT dependiente de AllowDepreciate del artículo; usuario puede marcar si permite depreciar.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'Depreciate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Deprecia (1 - SI, 0 - NO)    Nota: este campo depende del AllowDeprecate que se encuentra en el articulo ya que si alla esta marcado como no permite depreciar aca tambien debe ir siempre 0, pero si alla esta marcado como que si permite depreciar entonces aca es opcional por el usuario', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'Depreciate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'Depreciate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de instalación del activo. DATE, capturada desde Activos Fijos o auto-llenada al trasladar a ubicación con Unidad Operativa.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'InstallationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la fecha de Instalacion del Activo, este se llena desde el formulario de "Activos Fijos" o cuando este nulo y se haga un traslado a una Ubicacion que tenga una Unidad Operativa o Administrativa', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'InstallationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'InstallationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de adquisición del activo. DATE, momento oficial de entrada del bien al patrimonio.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'AdquisitionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la Fecha de Adquisición', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'AdquisitionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'AdquisitionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de vencimiento de la garantía. DATE null, caducidad de cobertura de garantía del fabricante.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'WarrantyExpirationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la fecha de vencimiento de la garantia', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'WarrantyExpirationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'WarrantyExpirationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el ítem maneja garantía (1=Sí, 0=No). Campo BIT para gestión de cobertura de garantía.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'HandlesWarranty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el item maneja garantia', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'HandlesWarranty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'HandlesWarranty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la póliza de seguros. INT, FK a [FixedAssetPolicy], aseguramiento del activo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'PolicyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la poliza', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'PolicyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'PolicyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modelo del activo. VARCHAR(100), descripción técnica del producto (ej: ''''Tesla Model 3'''', ''''Servidor HP ProLiant'''').', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'Model';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Modelo del activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'Model';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'Model';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la marca. INT, FK a [FixedAssetTrademark], clasificación por fabricante del bien.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'TrademarkId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Marca', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'TrademarkId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'TrademarkId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor razonable del activo. Decimal (18,2), valuación de mercado del bien para normas NIIF/NIC.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'FairValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor razonable del articulo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'FairValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'FairValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor histórico del activo. Decimal (18,2), costo de adquisición original; deprecado, usar valor histórico por libro.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'HistoricalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Valor Historico del activo  En desuso, ahora se usa el valor historico del activo por libro, puesto que este puede variar por libro', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'HistoricalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'HistoricalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del proveedor. INT null, FK a [Common].[Supplier], origen comercial del bien.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'SupplierId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Proveedor', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'SupplierId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'SupplierId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del responsable. INT, FK a [FixedAssetResponsible], persona u área custodio del activo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'ResponsibleId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Responsable', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'ResponsibleId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'ResponsibleId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la ubicación/localización. INT, FK a [FixedAssetLocation], lugar físico donde se encuentra el activo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'LocationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Localización', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'LocationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'LocationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Placa del activo. VARCHAR(50), identificación única de matrícula (vehículos, equipos fijos).', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'Plate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Placa', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'Plate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'Plate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Serie del activo. VARCHAR(50), número de serie único del fabricante para identificación y trazabilidad.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'Serie';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la serie del articulo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'Serie';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'Serie';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la cuenta contable principal. INT, FK a [GeneralLedger].[MainAccounts], clasificación según tipo de adquisición.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Cuenta de la cuenta contable del activo    de acuerdo con su tipo de adquisición', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del artículo. INT, FK a [FixedAsset].[FixedAssetItem], vínculo al catálogo de ítems.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'ItemId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Artículo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'ItemId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'ItemId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro. INT IDENTITY (1,1), clave primaria de la tabla FixedAssetPhysicalAsset.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de activos fijos físicos de la organización: bienes muebles e inmuebles identificados con placa y serie, incluyendo su valor histórico, valor razonable, ubicación, responsable, estado, información de adquisición, garantía, depreciación, amortización y datos de contratos de leasing.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset';
