CREATE TABLE [FixedAsset].[SettingFixedAsset] (
    [Id]                                         INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OperatingUnitId]                            INT          NOT NULL,
    [ProcessDate]                                DATE         NOT NULL,
    [FilingUnitId]                               INT          NOT NULL,
    [IdIngressAccountingVoucher]                 INT          NOT NULL,
    [IdDepreciationAccountingVoucher]            INT          NOT NULL,
    [IdAditionAccountingVoucher]                 INT          NULL,
    [IdOutputAccountingVoucher]                  INT          NOT NULL,
    [IdValorizationDevaluationAccountingVoucher] INT          NOT NULL,
    [TransferJournalVoucherId]                   INT          CONSTRAINT [DF_SettingFixedAsset_TransferJournalVoucherId] DEFAULT ((36)) NOT NULL,
    [ServiceMainAccountId]                       INT          CONSTRAINT [DF_SettingFixedAsset_ServiceMainAccountId] DEFAULT ((4140)) NOT NULL,
    [OtherIngressMainAccountId]                  INT          NOT NULL,
    [DonationMainAccountId]                      INT          NOT NULL,
    [TransferPropertyMainAccountId]              INT          NOT NULL,
    [OtherConceptsMainAccountId]                 INT          NOT NULL,
    [RecuperationMainAccountId]                  INT          NOT NULL,
    [SalesMainAccountId]                         INT          NOT NULL,
    [ReplacementMainAccountId]                   INT          CONSTRAINT [DF_SettingFixedAsset_ReplacementMainAccountId] DEFAULT ((4100)) NOT NULL,
    [LowBidAmount]                               NUMERIC (18) NOT NULL,
    [TopMinorValue]                              NUMERIC (18) NOT NULL,
    [IvaCost]                                    BIT          CONSTRAINT [DF_SettingFixedAsset_IvaCost] DEFAULT ((0)) NULL,
    [IdThirdPartyResponsible]                    INT          NOT NULL,
    [IVAFreightAccountPayableConceptId]          INT          NOT NULL,
    [FreightAccountPayableConceptId]             INT          NOT NULL,
    [IVARetention]                               TINYINT      CONSTRAINT [DF_SettingFixedAsset_IVARetention] DEFAULT ((1)) NOT NULL,
    [IVARetentionAccountPayableConceptId]        INT          NULL,
    [IVAAccountPayableConceptId]                 INT          NULL,
    [RefundAccountPayableConceptNoteId]          INT          NULL,
    [ReclassificationJournalVoucherId]           INT          CONSTRAINT [DF__SettingFixedAsset_ReclassificationJournalVoucherId] DEFAULT ((36)) NOT NULL,
    [DevolutionJournalVoucherId]                 INT          NOT NULL,
    [CommitmentBudgetInterface]                  BIT          CONSTRAINT [DF__SettingFi__Commi__5D7AADB3] DEFAULT ((0)) NOT NULL,
    [TaxCostBuy]                                 BIT          CONSTRAINT [DF__SettingFi__TaxCo__57A43AD1] DEFAULT ((1)) NOT NULL,
    [Depreciation30Days]                         BIT          CONSTRAINT [DF__SettingFi__Depre__669243FD] DEFAULT ((0)) NOT NULL,
    [CurrencyId]                                 INT          NULL,
    [AppliesCatalogPropertyandServices]          BIT          CONSTRAINT [DF__SettingFi__Appli__312035E4] DEFAULT ((0)) NULL,
    [IdIntangibleAssetAmortizationVoucher]       INT          NULL,
    CONSTRAINT [PK_FixedAssetParameters] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetParameters_JournalVouchers] FOREIGN KEY ([IdIngressAccountingVoucher]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_FixedAssetParameters_JournalVouchers1] FOREIGN KEY ([IdDepreciationAccountingVoucher]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_FixedAssetParameters_JournalVouchers2] FOREIGN KEY ([IdAditionAccountingVoucher]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_FixedAssetParameters_JournalVouchers3] FOREIGN KEY ([IdOutputAccountingVoucher]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_FixedAssetParameters_JournalVoucherTypes] FOREIGN KEY ([IdValorizationDevaluationAccountingVoucher]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_FixedAssetParameters_MainAccounts] FOREIGN KEY ([OtherIngressMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetParameters_MainAccounts1] FOREIGN KEY ([DonationMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetParameters_MainAccounts2] FOREIGN KEY ([OtherConceptsMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetParameters_MainAccounts3] FOREIGN KEY ([TransferPropertyMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetParameters_MainAccounts4] FOREIGN KEY ([RecuperationMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetParameters_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_FixedAssetParameters_ThirdParty] FOREIGN KEY ([IdThirdPartyResponsible]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_SettingFixedAsset_AccountPayableConcepts] FOREIGN KEY ([IVAFreightAccountPayableConceptId]) REFERENCES [Payments].[AccountPayableConcepts] ([Id]),
    CONSTRAINT [FK_SettingFixedAsset_AccountPayableConcepts1] FOREIGN KEY ([FreightAccountPayableConceptId]) REFERENCES [Payments].[AccountPayableConcepts] ([Id]),
    CONSTRAINT [FK_SettingFixedAsset_AccountPayableConcepts2] FOREIGN KEY ([IVAAccountPayableConceptId]) REFERENCES [Payments].[AccountPayableConcepts] ([Id]),
    CONSTRAINT [FK_SettingFixedAsset_AccountPayableConcepts3] FOREIGN KEY ([RefundAccountPayableConceptNoteId]) REFERENCES [Payments].[AccountPayableConceptNotes] ([Id]),
    CONSTRAINT [FK_SettingFixedAsset_AccountReceivableConcept] FOREIGN KEY ([IVARetentionAccountPayableConceptId]) REFERENCES [Payments].[AccountPayableConcepts] ([Id]),
    CONSTRAINT [FK_SettingFixedAsset_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_SettingFixedAsset_FilingUnit] FOREIGN KEY ([FilingUnitId]) REFERENCES [Payments].[FilingUnit] ([Id]),
    CONSTRAINT [FK_SettingFixedAsset_JournalVoucherTypes] FOREIGN KEY ([TransferJournalVoucherId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingFixedAsset_JournalVoucherTypes1] FOREIGN KEY ([ReclassificationJournalVoucherId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingFixedAsset_JournalVoucherTypes2] FOREIGN KEY ([DevolutionJournalVoucherId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingFixedAsset_MainAccounts] FOREIGN KEY ([ServiceMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_SettingFixedAsset_MainAccounts1] FOREIGN KEY ([SalesMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_SettingFixedAsset_MainAccounts2] FOREIGN KEY ([ReplacementMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);


GO
ALTER TABLE [FixedAsset].[SettingFixedAsset] NOCHECK CONSTRAINT [FK_FixedAssetParameters_JournalVouchers];


GO
ALTER TABLE [FixedAsset].[SettingFixedAsset] NOCHECK CONSTRAINT [FK_FixedAssetParameters_JournalVouchers1];


GO
ALTER TABLE [FixedAsset].[SettingFixedAsset] NOCHECK CONSTRAINT [FK_FixedAssetParameters_JournalVouchers2];


GO
ALTER TABLE [FixedAsset].[SettingFixedAsset] NOCHECK CONSTRAINT [FK_FixedAssetParameters_JournalVouchers3];


GO
ALTER TABLE [FixedAsset].[SettingFixedAsset] NOCHECK CONSTRAINT [FK_FixedAssetParameters_JournalVoucherTypes];


GO
ALTER TABLE [FixedAsset].[SettingFixedAsset] NOCHECK CONSTRAINT [FK_FixedAssetParameters_MainAccounts];


GO
ALTER TABLE [FixedAsset].[SettingFixedAsset] NOCHECK CONSTRAINT [FK_FixedAssetParameters_MainAccounts1];


GO
ALTER TABLE [FixedAsset].[SettingFixedAsset] NOCHECK CONSTRAINT [FK_FixedAssetParameters_MainAccounts2];


GO
ALTER TABLE [FixedAsset].[SettingFixedAsset] NOCHECK CONSTRAINT [FK_FixedAssetParameters_MainAccounts3];


GO
ALTER TABLE [FixedAsset].[SettingFixedAsset] NOCHECK CONSTRAINT [FK_FixedAssetParameters_MainAccounts4];


GO
ALTER TABLE [FixedAsset].[SettingFixedAsset] NOCHECK CONSTRAINT [FK_FixedAssetParameters_OperatingUnit];


GO
ALTER TABLE [FixedAsset].[SettingFixedAsset] NOCHECK CONSTRAINT [FK_FixedAssetParameters_ThirdParty];


GO
ALTER TABLE [FixedAsset].[SettingFixedAsset] NOCHECK CONSTRAINT [FK_SettingFixedAsset_AccountPayableConcepts];


GO
ALTER TABLE [FixedAsset].[SettingFixedAsset] NOCHECK CONSTRAINT [FK_SettingFixedAsset_AccountPayableConcepts1];


GO
ALTER TABLE [FixedAsset].[SettingFixedAsset] NOCHECK CONSTRAINT [FK_SettingFixedAsset_AccountPayableConcepts3];


GO
ALTER TABLE [FixedAsset].[SettingFixedAsset] NOCHECK CONSTRAINT [FK_SettingFixedAsset_AccountReceivableConcept];


GO
ALTER TABLE [FixedAsset].[SettingFixedAsset] NOCHECK CONSTRAINT [FK_SettingFixedAsset_Currency];


GO
ALTER TABLE [FixedAsset].[SettingFixedAsset] NOCHECK CONSTRAINT [FK_SettingFixedAsset_FilingUnit];


GO
ALTER TABLE [FixedAsset].[SettingFixedAsset] NOCHECK CONSTRAINT [FK_SettingFixedAsset_JournalVoucherTypes];


GO
ALTER TABLE [FixedAsset].[SettingFixedAsset] NOCHECK CONSTRAINT [FK_SettingFixedAsset_JournalVoucherTypes1];


GO
ALTER TABLE [FixedAsset].[SettingFixedAsset] NOCHECK CONSTRAINT [FK_SettingFixedAsset_JournalVoucherTypes2];


GO
ALTER TABLE [FixedAsset].[SettingFixedAsset] NOCHECK CONSTRAINT [FK_SettingFixedAsset_MainAccounts];


GO
ALTER TABLE [FixedAsset].[SettingFixedAsset] NOCHECK CONSTRAINT [FK_SettingFixedAsset_MainAccounts1];


GO
ALTER TABLE [FixedAsset].[SettingFixedAsset] NOCHECK CONSTRAINT [FK_SettingFixedAsset_MainAccounts2];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del comprobante amortizacion intangibles - Se diligencia con la tabla "JournalVoucherTypes"', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IdIntangibleAssetAmortizationVoucher';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la moneda, tiene relación con la tabla "Common.Currency" ', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si se va a depreciar a 30 dias', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'Depreciation30Days';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valida si trae IVA al costo | 1 = si | 0 = No ', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'TaxCostBuy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el id del concepto de nota de pagos que se va usar cuando se haga una devolucion de Ingreso de Activos  El concepto de la nota de pagos debe ser de tipo General', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'RefundAccountPayableConceptNoteId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del concepto de pagos para el IVA, este concepto debe de ser de tipo Especifico y NO debe manejar Retencion, Este concepto es utilizado cuando se comfirma el Ingreso de Activos y genera la cuenta por pagar    este representaria al iva descontable', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IVAAccountPayableConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica de donde se va a sacar la retencion del iva  1 - Tercero  2 - Concepto', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IVARetention';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'En desuso por actualización.  Ahora se usará el parámetro IvaCost definido en SettingFixedAssetByLegalBook para determinar la forma en que se contabiliza y se lleva el valor del Iva en el activo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IvaCost';

