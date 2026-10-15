CREATE TABLE [Glosas].[TimeParameters] (
    [Id]                                        TINYINT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdOperatingUnit]                           INT          NULL,
    [IdCustomer]                                INT          NULL,
    [MaxTimeExtemporaneousGlosa]                TINYINT      NOT NULL,
    [MaxTimeResponse]                           TINYINT      NOT NULL,
    [MaxTimeSendingDocumentResponse]            TINYINT      NOT NULL,
    [MaxTimeReiterationResponse]                TINYINT      NOT NULL,
    [MaxTimeExtemporaneousReiteration]          TINYINT      NOT NULL,
    [MaxTimeSendingReiterationDocumentResponse] TINYINT      NOT NULL,
    [MaxTimeConciliation]                       TINYINT      NOT NULL,
    [MaxTimeJuridicalDebtCollectionStart]       TINYINT      NOT NULL,
    [NotificationPeriodicity]                   TINYINT      NOT NULL,
    [IsDaybusinessSunday]                       BIT          CONSTRAINT [DF_TimeParameters_IsDaybusinessSunday] DEFAULT ((0)) NOT NULL,
    [IsDaybusinessSaturday]                     BIT          CONSTRAINT [DF_TimeParameters_IsDaybusinessSaturday] DEFAULT ((0)) NOT NULL,
    [DiscountedMedicalFees]                     BIT          CONSTRAINT [DF_TimeParameters_DiscountedMedicalFees] DEFAULT ((0)) NOT NULL,
    [AffectedService]                           BIT          CONSTRAINT [DF_TimeParameters_AffectedService] DEFAULT ((0)) NULL,
    [DevolutionInjustificate]                   TINYINT      CONSTRAINT [DF_TimeParameters_DevolutionInjustificate] DEFAULT ((1)) NULL,
    [GeneralGlossConceptNoteId]                 INT          NULL,
    [DetailedGlossConceptNoteId]                INT          NULL,
    [PreviousLifetimesConceptNoteId]            INT          CONSTRAINT [DF_TimeParameters_PreviousLifetimesConceptNoteId] DEFAULT ((2)) NULL,
    [PreviousLifetimesCostCenterId]             INT          NULL,
    [PreviousLifetimesThirdPartyId]             INT          NULL,
    [RadicationJournalVoucherTypeId]            INT          NULL,
    [ReceptionObjectionJournalVoucherTypeId]    INT          CONSTRAINT [DF_TimeParameters_ReceptionObjectionJournalVoucherTypeId] DEFAULT ((1)) NULL,
    [ConciliationJournalVoucherTypeId]          INT          NULL,
    [DevolutionJournalVoucherTypeId]            INT          CONSTRAINT [DF_TimeParameters_DevolutionJournalVoucherTypeId] DEFAULT ((1)) NULL,
    [TransferLegalJournalVoucherTypeId]         INT          NULL,
    [GroupAccountingVoucher]                    BIT          CONSTRAINT [DF_TimeParameters_GroupAccountingVoucher] DEFAULT ((1)) NOT NULL,
    [CreationUser]                              VARCHAR (20) CONSTRAINT [DF_TimeParameters_CreationUser] DEFAULT ((4545)) NOT NULL,
    [CreationDate]                              DATETIME     CONSTRAINT [DF_TimeParameters_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]                          VARCHAR (20) NULL,
    [ModificationDate]                          DATETIME     NULL,
    [TimeStamp]                                 ROWVERSION   NOT NULL,
    [ManageDecimals]                            BIT          NULL,
    CONSTRAINT [PK_TimeParameters] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_JournalVoucherTypes] FOREIGN KEY ([RadicationJournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_TimeParameters_CostCenter] FOREIGN KEY ([PreviousLifetimesCostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_TimeParameters_Customer] FOREIGN KEY ([IdCustomer]) REFERENCES [Common].[Customer] ([Id]),
    CONSTRAINT [FK_TimeParameters_JournalVouchers1] FOREIGN KEY ([ReceptionObjectionJournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_TimeParameters_JournalVouchers2] FOREIGN KEY ([DevolutionJournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_TimeParameters_JournalVouchers3] FOREIGN KEY ([ConciliationJournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_TimeParameters_JournalVouchers4] FOREIGN KEY ([TransferLegalJournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_TimeParameters_OperatingUnit] FOREIGN KEY ([IdOperatingUnit]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_TimeParameters_PortfolioNoteConcept] FOREIGN KEY ([GeneralGlossConceptNoteId]) REFERENCES [Portfolio].[PortfolioNoteConcept] ([Id]),
    CONSTRAINT [FK_TimeParameters_PortfolioNoteConcept1] FOREIGN KEY ([DetailedGlossConceptNoteId]) REFERENCES [Portfolio].[PortfolioNoteConcept] ([Id]),
    CONSTRAINT [FK_TimeParameters_PortfolioNoteConcept2] FOREIGN KEY ([PreviousLifetimesConceptNoteId]) REFERENCES [Portfolio].[PortfolioNoteConcept] ([Id]),
    CONSTRAINT [FK_TimeParameters_ThirdParty] FOREIGN KEY ([PreviousLifetimesThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Glosas].[TimeParameters] NOCHECK CONSTRAINT [FK_JournalVoucherTypes];


GO
ALTER TABLE [Glosas].[TimeParameters] NOCHECK CONSTRAINT [FK_TimeParameters_CostCenter];


GO
ALTER TABLE [Glosas].[TimeParameters] NOCHECK CONSTRAINT [FK_TimeParameters_Customer];


GO
ALTER TABLE [Glosas].[TimeParameters] NOCHECK CONSTRAINT [FK_TimeParameters_JournalVouchers1];


GO
ALTER TABLE [Glosas].[TimeParameters] NOCHECK CONSTRAINT [FK_TimeParameters_JournalVouchers2];


GO
ALTER TABLE [Glosas].[TimeParameters] NOCHECK CONSTRAINT [FK_TimeParameters_JournalVouchers3];


GO
ALTER TABLE [Glosas].[TimeParameters] NOCHECK CONSTRAINT [FK_TimeParameters_JournalVouchers4];


GO
ALTER TABLE [Glosas].[TimeParameters] NOCHECK CONSTRAINT [FK_TimeParameters_OperatingUnit];


GO
ALTER TABLE [Glosas].[TimeParameters] NOCHECK CONSTRAINT [FK_TimeParameters_PortfolioNoteConcept];


GO
ALTER TABLE [Glosas].[TimeParameters] NOCHECK CONSTRAINT [FK_TimeParameters_PortfolioNoteConcept1];


GO
ALTER TABLE [Glosas].[TimeParameters] NOCHECK CONSTRAINT [FK_TimeParameters_PortfolioNoteConcept2];


GO
ALTER TABLE [Glosas].[TimeParameters] NOCHECK CONSTRAINT [FK_TimeParameters_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'bandera para determinar si la factura sale del radicado inicial, para realizar uno nuevo o en su caso, si se mantiene el original  1) liberar factura del radicado  2) mantener factura en el radicado  3) definido por el usuario', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'DevolutionInjustificate';

