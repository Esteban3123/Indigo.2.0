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



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del comprobante contable de amortización de activos intangibles (INT, FK→JournalVoucherTypes). Referencia al tipo de asiento para registrar amortizaciones de intangibles.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IdIntangibleAssetAmortizationVoucher';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del comprobante amortizacion intangibles - Se diligencia con la tabla "JournalVoucherTypes"', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IdIntangibleAssetAmortizationVoucher';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IdIntangibleAssetAmortizationVoucher';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT, 1=Sí|0=No) que especifica si aplica el catálogo de bienes y servicios en la configuración de activos fijos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'AppliesCatalogPropertyandServices';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Aplica catálogo de bienes y servicios | 1 = Si | 0  = No', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'AppliesCatalogPropertyandServices';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'AppliesCatalogPropertyandServices';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de moneda (INT, FK→Common.Currency). Relaciona la configuración de activos con la moneda base o secundaria para valuación.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la moneda, tiene relación con la tabla "Common.Currency" ', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT, 1=Sí|0=No) que especifica si los activos se deprecian en período de 30 días vs. vida útil estándar.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'Depreciation30Days';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si se va a depreciar a 30 dias', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'Depreciation30Days';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'Depreciation30Days';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT, 1=Sí|0=No) que valida si el IVA se incluye en el costo de adquisición del activo fijo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'TaxCostBuy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valida si trae IVA al costo | 1 = si | 0 = No ', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'TaxCostBuy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'TaxCostBuy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT, 1=Sí|0=No) que especifica si es obligatoria la asociación del compromiso presupuestal en contratos y órdenes de compra de activos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'CommitmentBudgetInterface';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si es obligatoria la asociación del compromiso en los contratos y ordenes de compra', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'CommitmentBudgetInterface';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'CommitmentBudgetInterface';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de comprobante contable (INT, FK→JournalVoucherTypes) utilizado para registrar devoluciones de ingresos de activos fijos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'DevolutionJournalVoucherId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Tipo Comprobante de devolución de ingreso', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'DevolutionJournalVoucherId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'DevolutionJournalVoucherId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de comprobante contable (INT, FK→JournalVoucherTypes, por defecto 36) para reclasificación y traslados internos de activos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'ReclassificationJournalVoucherId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo de comprobante para reclasificación de activos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'ReclassificationJournalVoucherId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'ReclassificationJournalVoucherId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de nota de pagos (INT, FK→AccountPayableConceptNotes, tipo General) utilizado al registrar devoluciones de ingresos de activos fijos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'RefundAccountPayableConceptNoteId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id del concepto de nota de pagos que se va usar cuando se haga una devolucion de Ingreso de Activos  El concepto de la nota de pagos debe ser de tipo General', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'RefundAccountPayableConceptNoteId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'RefundAccountPayableConceptNoteId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de cuentas por pagar (INT, FK→AccountPayableConcepts, tipo Específico, sin retención) para IVA descontable al confirmar ingreso de activos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IVAAccountPayableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de pagos para el IVA, este concepto debe de ser de tipo Especifico y NO debe manejar Retencion, Este concepto es utilizado cuando se comfirma el Ingreso de Activos y genera la cuenta por pagar    este representaria al iva descontable', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IVAAccountPayableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IVAAccountPayableConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de cuentas por pagar (INT, FK→AccountPayableConcepts, tipo Específico, con retención) para retención de IVA cuando proviene del concepto (IVARetention=2).', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IVARetentionAccountPayableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de pagos para la retencion del IVA, este concepto debe de ser de tipo Especifico y debe manejar Retencion, Solo se llena cuando la rentencion del iva se obtiene del concepto (IVARetention - 2)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IVARetentionAccountPayableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IVARetentionAccountPayableConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (TINYINT: 1=Tercero|2=Concepto) que especifica de dónde se obtiene el cálculo de retención del IVA en compra de activos fijos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IVARetention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica de donde se va a sacar la retencion del iva  1 - Tercero  2 - Concepto', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IVARetention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IVARetention';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de cuentas por pagar (INT, FK→AccountPayableConcepts, tipo Específico, sin retención) para gastos de flete en adquisiciones de activos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'FreightAccountPayableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de cuentas por pagar para el flete, Este concepto de cuenta por pagar NO debe Manejar Retencion y debe ser de tipo Especifico', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'FreightAccountPayableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'FreightAccountPayableConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de cuentas por pagar (INT, FK→AccountPayableConcepts, tipo Específico, con retención) para IVA sobre flete de activos fijos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IVAFreightAccountPayableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de cuentas por pagar par a el porcentaje de IVA del flete, Este concepto de cuenta por pagar debe Manejar Retencion y debe ser de tipo Especifico', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IVAFreightAccountPayableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IVAFreightAccountPayableConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tercero (INT, FK→Common.ThirdParty) designado como Jefe/Responsable de Activos Fijos para la unidad operativa.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IdThirdPartyResponsible';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Jefe de Activos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IdThirdPartyResponsible';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IdThirdPartyResponsible';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT, DEPRECATED). En desuso; use SettingFixedAssetByLegalBook.IvaCost para determinar cómo se contabiliza y valúa el IVA en el activo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IvaCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'En desuso por actualización.  Ahora se usará el parámetro IvaCost definido en SettingFixedAssetByLegalBook para determinar la forma en que se contabiliza y se lleva el valor del Iva en el activo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IvaCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IvaCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario (NUMERIC 18,0) que establece el límite superior de menor cuantía para activos fijos en la unidad operativa.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'TopMinorValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor tope de menor cuantía', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'TopMinorValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'TopMinorValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario (NUMERIC 18,0) que establece el límite mínimo de menor cuantía para activos fijos; por debajo se considera gasto inmediato.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'LowBidAmount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor minimo de menor cuantia', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'LowBidAmount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'LowBidAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable principal (INT, FK→MainAccounts, por defecto 4100) para registrar reposiciones o responsabilidades de activos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'ReplacementMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cuenta contable de reposicion o de responsabilidades', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'ReplacementMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'ReplacementMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable principal (INT, FK→MainAccounts) para cuentas por cobrar y ventas de activos fijos retirados.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'SalesMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta Contable de Cuenta por Cobrar y Ventas', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'SalesMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'SalesMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable principal (INT, FK→MainAccounts) para ingresos por recuperación o venta de residuos y descartes de activos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'RecuperationMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Cuenta Contable de Recuperación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'RecuperationMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'RecuperationMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable principal (INT, FK→MainAccounts) para ingresos por otros conceptos no clasificados en activos fijos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'OtherConceptsMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Cuenta Contable Ingreso Otros Conceptos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'OtherConceptsMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'OtherConceptsMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable principal (INT, FK→MainAccounts) para ingresos por traslado o traspaso de bienes entre entidades.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'TransferPropertyMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Cuenta Contable Ingreso Traspaso de Bienes', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'TransferPropertyMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'TransferPropertyMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable principal (INT, FK→MainAccounts) para ingresos por donaciones de activos fijos recibidas.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'DonationMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Cuenta Contable Ingresos x Donación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'DonationMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'DonationMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable principal (INT, FK→MainAccounts) para otros ingresos diversos relacionados con activos fijos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'OtherIngressMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Cuenta Contable Otros Ingresos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'OtherIngressMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'OtherIngressMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable principal (INT, FK→MainAccounts, por defecto 4140) para ingresos por servicios relacionados con activos fijos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'ServiceMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable de servicios', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'ServiceMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'ServiceMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de comprobante contable (INT, FK→JournalVoucherTypes, por defecto 36) para registrar traslados y movimientos de activos entre ubicaciones.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'TransferJournalVoucherId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo de comprobante para traslados de activos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'TransferJournalVoucherId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'TransferJournalVoucherId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de comprobante contable (INT, FK→JournalVoucherTypes) para registrar revaluación y desvaluación de activos fijos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IdValorizationDevaluationAccountingVoucher';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Comprobante Contable de Valorización y Desvalorización', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IdValorizationDevaluationAccountingVoucher';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IdValorizationDevaluationAccountingVoucher';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de comprobante contable (INT, FK→JournalVoucherTypes) para registrar salidas, retiros y bajas de activos fijos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IdOutputAccountingVoucher';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Tipo Comprobante Salida', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IdOutputAccountingVoucher';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IdOutputAccountingVoucher';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de comprobante contable (INT, FK→JournalVoucherTypes, NULL en formulario) para adiciones y mejoras capitalizables de activos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IdAditionAccountingVoucher';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Tipo Comprobante Adición, se coloca como null porque en el formulario de parámetros se oculta el campo por petición de Jose Reyes', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IdAditionAccountingVoucher';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IdAditionAccountingVoucher';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de comprobante contable (INT, FK→JournalVoucherTypes) para registrar el gasto de depreciación periódica de activos fijos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IdDepreciationAccountingVoucher';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Tipo de Comprobante de Depreciación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IdDepreciationAccountingVoucher';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IdDepreciationAccountingVoucher';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de comprobante contable (INT, FK→JournalVoucherTypes) para registrar la adquisición e ingreso de activos fijos a la entidad.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IdIngressAccountingVoucher';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Tipo de Comprobante de Ingreso', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IdIngressAccountingVoucher';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IdIngressAccountingVoucher';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad de radicación (INT, FK→OperatingUnit) que recibe y procesa las cuentas por pagar generadas por ingreso de activos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'FilingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad de radicacion que se envia a la cxp', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'FilingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'FilingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATE) de procesamiento o vigencia de la configuración de parámetros de activos fijos en la unidad operativa.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'ProcessDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Proceso', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'ProcessDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'ProcessDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad operativa (INT, FK→Common.OperatingUnit) a la cual aplica esta configuración de parámetros de activos fijos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Unidad Operativa', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, PK, IDENTITY) de la configuración de parámetros de activos fijos por unidad operativa.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de los Parámetros de Activos Fijos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración general del módulo de activos fijos: define los comprobantes contables, cuentas principales, parámetros de depreciación, retenciones de IVA y umbrales de valor mínimo que se aplican a los procesos de ingreso, depreciación, traslado, baja y venta de activos fijos por unidad operativa.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset';
