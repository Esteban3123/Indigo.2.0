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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Aplica catálogo de bienes y servicios | 1 = Si | 0  = No', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'AppliesCatalogPropertyandServices';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la moneda, tiene relación con la tabla "Common.Currency" ', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si se va a depreciar a 30 dias', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'Depreciation30Days';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valida si trae IVA al costo | 1 = si | 0 = No ', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'TaxCostBuy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si es obligatoria la asociación del compromiso en los contratos y ordenes de compra', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'CommitmentBudgetInterface';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Tipo Comprobante de devolución de ingreso', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'DevolutionJournalVoucherId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tipo de comprobante para reclasificación de activos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'ReclassificationJournalVoucherId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el id del concepto de nota de pagos que se va usar cuando se haga una devolucion de Ingreso de Activos  El concepto de la nota de pagos debe ser de tipo General', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'RefundAccountPayableConceptNoteId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del concepto de pagos para el IVA, este concepto debe de ser de tipo Especifico y NO debe manejar Retencion, Este concepto es utilizado cuando se comfirma el Ingreso de Activos y genera la cuenta por pagar    este representaria al iva descontable', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IVAAccountPayableConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del concepto de pagos para la retencion del IVA, este concepto debe de ser de tipo Especifico y debe manejar Retencion, Solo se llena cuando la rentencion del iva se obtiene del concepto (IVARetention - 2)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IVARetentionAccountPayableConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica de donde se va a sacar la retencion del iva  1 - Tercero  2 - Concepto', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IVARetention';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del concepto de cuentas por pagar para el flete, Este concepto de cuenta por pagar NO debe Manejar Retencion y debe ser de tipo Especifico', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'FreightAccountPayableConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del concepto de cuentas por pagar par a el porcentaje de IVA del flete, Este concepto de cuenta por pagar debe Manejar Retencion y debe ser de tipo Especifico', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IVAFreightAccountPayableConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Jefe de Activos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IdThirdPartyResponsible';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'En desuso por actualización.  Ahora se usará el parámetro IvaCost definido en SettingFixedAssetByLegalBook para determinar la forma en que se contabiliza y se lleva el valor del Iva en el activo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IvaCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor tope de menor cuantía', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'TopMinorValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor minimo de menor cuantia', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'LowBidAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la cuenta contable de reposicion o de responsabilidades', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'ReplacementMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta Contable de Cuenta por Cobrar y Ventas', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'SalesMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Cuenta Contable de Recuperación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'RecuperationMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Cuenta Contable Ingreso Otros Conceptos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'OtherConceptsMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Cuenta Contable Ingreso Traspaso de Bienes', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'TransferPropertyMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Cuenta Contable Ingresos x Donación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'DonationMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Cuenta Contable Otros Ingresos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'OtherIngressMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable de servicios', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'ServiceMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tipo de comprobante para traslados de activos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'TransferJournalVoucherId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Comprobante Contable de Valorización y Desvalorización', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IdValorizationDevaluationAccountingVoucher';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Tipo Comprobante Salida', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IdOutputAccountingVoucher';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Tipo Comprobante Adición, se coloca como null porque en el formulario de parámetros se oculta el campo por petición de Jose Reyes', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IdAditionAccountingVoucher';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Tipo de Comprobante de Depreciación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IdDepreciationAccountingVoucher';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Tipo de Comprobante de Ingreso', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'IdIngressAccountingVoucher';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad de radicacion que se envia a la cxp', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'FilingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Proceso', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'ProcessDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Unidad Operativa', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de los Parámetros de Activos Fijos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAsset', @level2type = N'COLUMN', @level2name = N'Id';

