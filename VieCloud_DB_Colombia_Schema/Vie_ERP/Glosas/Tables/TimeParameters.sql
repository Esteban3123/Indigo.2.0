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
    [PreviousLifetimesMainAccountId]            INT          NULL,
    [PreviousLifetimesThirdPartyType]           TINYINT      CONSTRAINT [DF_TimeParameters_PreviousLifetimesThirdPartyType] DEFAULT ((1)) NULL,
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
    CONSTRAINT [FK_TimeParameters_PreviousLifetimesMainAccount] FOREIGN KEY ([PreviousLifetimesMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_TimeParameters_ThirdParty] FOREIGN KEY ([PreviousLifetimesThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) que habilita administración/redondeo de decimales en cálculos de glosa y liquidación.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'ManageDecimals';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Administrar decimales', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'ManageDecimals';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'ManageDecimals';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal (TIMESTAMP) del sistema que registra instantáneamente creación, modificación o cambio del registro para auditoría.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de última modificación del registro.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Modificacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR(20), PII_Ofuscado) que realizó última modificación del parámetro de tiempo.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de Modificacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación del registro, autocalculada por sistema Common.getdate().', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR(20), PII_Ofuscado, default=''''4545'''') que registró/creó el parámetro de tiempo.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de creacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT, default=1) que agrupa comprobantes contables generados en procesos de glosa para reducir volumen de registros.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'GroupAccountingVoucher';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si se agrupa los comprobantes contables generados desde los diferente procesos de glosas; esto con el fin de no generar gran cantidad de comprobante contable.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'GroupAccountingVoucher';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'GroupAccountingVoucher';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK→JournalVoucherTypes) del tipo de comprobante contable para transferencia a cobro jurídico; entidades privadas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'TransferLegalJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de comprobante contable para empresas privadas', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'TransferLegalJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'TransferLegalJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK→JournalVoucherTypes, default=1) del tipo de comprobante contable para devoluciones de facturas; sector público y privado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'DevolutionJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo del comprobante, para las devoluciones, este aplica para las entidades publicas y privadas', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'DevolutionJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'DevolutionJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK→JournalVoucherTypes) del tipo de comprobante contable para procesos de conciliación; entidades privadas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'ConciliationJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo del comprobante contable para el tramite de conciliaciones o realizacion de concilianes, este solo aplica para entidades privadas', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'ConciliationJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'ConciliationJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK→JournalVoucherTypes, default=1) del tipo de comprobante contable para recepción de objeciones/reclamos.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'ReceptionObjectionJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo de comprobante para la recepcion de objeciones, este solo aplica para las entidades privadas y publicas', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'ReceptionObjectionJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'ReceptionObjectionJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK→JournalVoucherTypes) del tipo de comprobante contable para radicación/registro de cuentas por cobrar; aplica sector público y privado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'RadicationJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Tipo del comprobante para la radicacion de cuentas, este aplica tanto para el publico como el privado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'RadicationJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'RadicationJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK→ThirdParty) del tercero/proveedor para asientos de vigencias anteriores.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'PreviousLifetimesThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de terceros anteriores', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'PreviousLifetimesThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'PreviousLifetimesThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK→CostCenter) del centro de costos para registros contables de períodos anteriores.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'PreviousLifetimesCostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del centro de costos anteriores', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'PreviousLifetimesCostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'PreviousLifetimesCostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK→PortfolioNoteConcept, default=2) del concepto de nota para contabilizar facturas de vigencias/ejercicios anteriores.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'PreviousLifetimesConceptNoteId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de la nota de cartera, para contabilizar las facturas de las vigencias anteriores, Este concepto de nota debe ser de tipo Especifico', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'PreviousLifetimesConceptNoteId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'PreviousLifetimesConceptNoteId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK→PortfolioNoteConcept) del concepto de nota contable para glosa detallada por servicio; se habilita si AffectedService=1.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'DetailedGlossConceptNoteId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de nota de cartera de la glosas detallada. Este campo se llena solo si se afecta por servicios (AffectedService), Si se habilita este campo solo se filtran los tipos de conceptos que sean Genericos', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'DetailedGlossConceptNoteId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'DetailedGlossConceptNoteId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK→PortfolioNoteConcept) del concepto de nota contable para glosa general; aplica si NO afecta servicio (AffectedService=0).', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'GeneralGlossConceptNoteId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de la nota de cartera de Glosas General, Este se habilita solo si NO se afecta por servicios (AffectedService), si se habilita solo se se pueden seleccionar conceptos de notas que sea de tipo Especifico', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'GeneralGlossConceptNoteId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'GeneralGlossConceptNoteId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (TINYINT, default=1) del destino de factura en devolución: 1=liberar del radicado inicial, 2=mantener en radicado, 3=decisión del usuario.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'DevolutionInjustificate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'bandera para determinar si la factura sale del radicado inicial, para realizar uno nuevo o en su caso, si se mantiene el original  1) liberar factura del radicado  2) mantener factura en el radicado  3) definido por el usuario', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'DevolutionInjustificate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'DevolutionInjustificate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) que especifica si la aceptación/rechazo de glosa se afecta y liquida por servicio individual.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'AffectedService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si se afecta por servicio en las aceptaciones de las glosas', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'AffectedService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'AffectedService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) que habilita descuento de honorarios médicos; activa configuración de conceptos aplicables.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'DiscountedMedicalFees';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Bandera que indica si descuenta honorarios medicos, en caso positivo se cargan los concepto de glosa para que se configuren cual aplica para descuentos', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'DiscountedMedicalFees';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'DiscountedMedicalFees';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) que indica si el sábado se considera día laboral/hábil en cálculo de plazos de glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'IsDaybusinessSaturday';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si el sabado se toma como dia laboral', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'IsDaybusinessSaturday';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'IsDaybusinessSaturday';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) que indica si el domingo se considera día laboral/hábil en cálculo de plazos de glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'IsDaybusinessSunday';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si el domingo se toma como dia laboral', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'IsDaybusinessSunday';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'IsDaybusinessSunday';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Periodicidad en días (TINYINT) de notificación/comunicación de estados o cambios en proceso de glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'NotificationPeriodicity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Periodicidad de Notificacion - Dias', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'NotificationPeriodicity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'NotificationPeriodicity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plazo máximo en días (TINYINT) para iniciar cobro jurídico/cobranza extrajudicial de glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'MaxTimeJuridicalDebtCollectionStart';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiempo Maximo Inicio Cobro Juridico - Dias', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'MaxTimeJuridicalDebtCollectionStart';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'MaxTimeJuridicalDebtCollectionStart';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plazo máximo en días (TINYINT) para realizar conciliación; resolución amistosa de glosa entre IPS y asegurador.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'MaxTimeConciliation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiempo Maximo Conciliacion - Dias', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'MaxTimeConciliation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'MaxTimeConciliation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plazo máximo en días (TINYINT) para envío de oficio de respuesta en proceso de reiteración.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'MaxTimeSendingReiterationDocumentResponse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiempo Maximo Envio de Oficio Respuesta Reiteracion - Dias', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'MaxTimeSendingReiterationDocumentResponse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'MaxTimeSendingReiterationDocumentResponse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plazo máximo en días (TINYINT) para reiteración extemporánea de glosa fuera de término.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'MaxTimeExtemporaneousReiteration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiempo Maximo Respuesta Reiteracion - Dias', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'MaxTimeExtemporaneousReiteration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'MaxTimeExtemporaneousReiteration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plazo máximo en días (TINYINT) para responder glosa en reiteración (segunda instancia de reclamo).', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'MaxTimeReiterationResponse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Reiteracion Extemporanea - Dias', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'MaxTimeReiterationResponse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'MaxTimeReiterationResponse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plazo máximo en días (TINYINT) para enviar oficio/comunicación escrita de respuesta a glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'MaxTimeSendingDocumentResponse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiempo Maximo Envio de Oficio Respuesta - Dias', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'MaxTimeSendingDocumentResponse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'MaxTimeSendingDocumentResponse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plazo máximo en días (TINYINT) para responder glosa inicial; término de contestación de demanda.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'MaxTimeResponse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiempo Maximo Respuesta - Dias', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'MaxTimeResponse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'MaxTimeResponse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plazo máximo en días (TINYINT) para interponer glosa extemporánea, fuera de la oportunidad ordinaria.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'MaxTimeExtemporaneousGlosa';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiempo Maximo para Glosa Extemporanea - Dias', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'MaxTimeExtemporaneousGlosa';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'MaxTimeExtemporaneousGlosa';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK→Customer) del cliente/asegurador; permite configurar parámetros de tiempo diferenciados por cliente en procesos de glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'IdCustomer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Relacion con tabla de clientes (por si se manejan Configuracion de Tiempo por cliente)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'IdCustomer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'IdCustomer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK→OperatingUnit) de la unidad operativa, centro de atención o institución de salud asociada a estos parámetros.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Unidad Operativa', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (TINYINT) de parámetros de tiempo de glosas y procesos de recaudo.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de parametros de tiempo', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros de tiempos y configuración del proceso de glosas: define los plazos máximos (en días) para cada etapa del ciclo de glosas (radicación, respuesta, reitera­ción, conciliación, cobro jurídico), los días hábiles considerados, las cuentas contables asociadas a cada movimiento y otras opciones de comportamiento del módulo de glosas, configurables por unidad operativa o cliente (pagador/asegurador).', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters';
