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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Administrar decimales', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'ManageDecimals';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Modificacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de Modificacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de creacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si se agrupa los comprobantes contables generados desde los diferente procesos de glosas; esto con el fin de no generar gran cantidad de comprobante contable.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'GroupAccountingVoucher';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de comprobante contable para empresas privadas', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'TransferLegalJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tipo del comprobante, para las devoluciones, este aplica para las entidades publicas y privadas', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'DevolutionJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tipo del comprobante contable para el tramite de conciliaciones o realizacion de concilianes, este solo aplica para entidades privadas', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'ConciliationJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tipo de comprobante para la recepcion de objeciones, este solo aplica para las entidades privadas y publicas', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'ReceptionObjectionJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Tipo del comprobante para la radicacion de cuentas, este aplica tanto para el publico como el privado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'RadicationJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de terceros anteriores', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'PreviousLifetimesThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del centro de costos anteriores', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'PreviousLifetimesCostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del concepto de la nota de cartera, para contabilizar las facturas de las vigencias anteriores, Este concepto de nota debe ser de tipo Especifico', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'PreviousLifetimesConceptNoteId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del concepto de nota de cartera de la glosas detallada. Este campo se llena solo si se afecta por servicios (AffectedService), Si se habilita este campo solo se filtran los tipos de conceptos que sean Genericos', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'DetailedGlossConceptNoteId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del concepto de la nota de cartera de Glosas General, Este se habilita solo si NO se afecta por servicios (AffectedService), si se habilita solo se se pueden seleccionar conceptos de notas que sea de tipo Especifico', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'GeneralGlossConceptNoteId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'bandera para determinar si la factura sale del radicado inicial, para realizar uno nuevo o en su caso, si se mantiene el original  1) liberar factura del radicado  2) mantener factura en el radicado  3) definido por el usuario', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'DevolutionInjustificate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si se afecta por servicio en las aceptaciones de las glosas', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'AffectedService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Bandera que indica si descuenta honorarios medicos, en caso positivo se cargan los concepto de glosa para que se configuren cual aplica para descuentos', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'DiscountedMedicalFees';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si el sabado se toma como dia laboral', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'IsDaybusinessSaturday';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si el domingo se toma como dia laboral', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'IsDaybusinessSunday';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Periodicidad de Notificacion - Dias', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'NotificationPeriodicity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiempo Maximo Inicio Cobro Juridico - Dias', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'MaxTimeJuridicalDebtCollectionStart';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiempo Maximo Conciliacion - Dias', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'MaxTimeConciliation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiempo Maximo Envio de Oficio Respuesta Reiteracion - Dias', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'MaxTimeSendingReiterationDocumentResponse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiempo Maximo Respuesta Reiteracion - Dias', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'MaxTimeExtemporaneousReiteration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Reiteracion Extemporanea - Dias', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'MaxTimeReiterationResponse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiempo Maximo Envio de Oficio Respuesta - Dias', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'MaxTimeSendingDocumentResponse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiempo Maximo Respuesta - Dias', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'MaxTimeResponse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiempo Maximo para Glosa Extemporanea - Dias', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'MaxTimeExtemporaneousGlosa';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Relacion con tabla de clientes (por si se manejan Configuracion de Tiempo por cliente)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'IdCustomer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Unidad Operativa', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de parametros de tiempo', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TimeParameters', @level2type = N'COLUMN', @level2name = N'Id';

