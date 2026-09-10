CREATE TABLE [Inventory].[SettingInventory] (
    [Id]                                                    INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OperatingUnitId]                                       INT           NOT NULL,
    [Year]                                                  INT           NOT NULL,
    [Month]                                                 INT           NOT NULL,
    [StockControl]                                          TINYINT       NOT NULL,
    [PurchaseJournalVoucherTypeId]                          INT           NOT NULL,
    [SalesJournalVoucherTypeId]                             INT           NOT NULL,
    [RemissionEntranceJournalVoucherTypeId]                 INT           NOT NULL,
    [RemissionOutputJournalVoucherTypeId]                   INT           NOT NULL,
    [LoanJournalVoucherTypeId]                              INT           NOT NULL,
    [LoanReturnJournalVoucherTypeId]                        INT           NOT NULL,
    [SalesReturnJournalVoucherTypeId]                       INT           NOT NULL,
    [PurchaseReturnJournalVoucherTypeId]                    INT           NOT NULL,
    [OrderDispatchJournalVoucherTypeId]                     INT           NOT NULL,
    [OrderDispatchReturnJournalVoucherTypeId]               INT           NOT NULL,
    [InventoryAdjustmentJournalVoucherTypeId]               INT           NOT NULL,
    [FreightIVAId]                                          INT           CONSTRAINT [DF_SettingInventory_FreightIVAId] DEFAULT ((1)) NOT NULL,
    [IVAFreightAccountPayableConceptId]                     INT           NOT NULL,
    [FreightAccountPayableConceptId]                        INT           NOT NULL,
    [ProDevelopmentAccountPayableConceptId]                 INT           NULL,
    [ProElectrificationAccountPayableConceptId]             INT           NULL,
    [ProCultureAccountPayableConceptId]                     INT           NULL,
    [ProHospitalAccountPayableConceptId]                    INT           NULL,
    [ProGameAccountPayableConceptId1]                       INT           NULL,
    [IVARetention]                                          TINYINT       CONSTRAINT [DF_SettingInventory_IVARetention] DEFAULT ((1)) NOT NULL,
    [IVARetentionAccountPayableConceptId]                   INT           NULL,
    [FilingUnitId]                                          INT           NOT NULL,
    [IVAAccountPayableConceptId]                            INT           NOT NULL,
    [IVAGeneratedMainAccountId]                             INT           NOT NULL,
    [AdjustmentAccountPayableConceptId]                     INT           NOT NULL,
    [IVACost]                                               BIT           CONSTRAINT [DF__SettingIn__IVACo__6919AAED] DEFAULT ((0)) NOT NULL,
    [AssociateCostCenter]                                   TINYINT       NOT NULL,
    [DiscountSalesMainAccountId]                            INT           NOT NULL,
    [RefundAccountPayableConceptNoteId]                     INT           NOT NULL,
    [InputAdjustmentConceptId]                              INT           NOT NULL,
    [OutputAdjustmentConceptId]                             INT           NOT NULL,
    [TransferOrderThirdPartyId]                             INT           NOT NULL,
    [TakeTransferOrderThirdParty]                           TINYINT       NOT NULL,
    [PharmaceuticalDispensingGetThirdParty]                 TINYINT       NOT NULL,
    [PharmaceuticalDispensingThirdPartyId]                  INT           NULL,
    [CreationUser]                                          VARCHAR (20)  NOT NULL,
    [CreationDate]                                          DATETIME      NOT NULL,
    [ModificationUser]                                      VARCHAR (20)  NULL,
    [ModificationDate]                                      DATETIME      NULL,
    [TimeStamp]                                             ROWVERSION    NOT NULL,
    [RemissionOutputDevolutionJournalVoucherTypeId]         INT           NOT NULL,
    [RemissionEntranceDevolutionJournalVoucherTypeId]       INT           NOT NULL,
    [ReclassificationRemissionJournalVoucherTypeId]         INT           NOT NULL,
    [InventoryCloseAdjustmentJournalVoucherTypeId]          INT           NOT NULL,
    [ConsignmentMerchandiseJournalVoucherTypeId]            INT           NOT NULL,
    [ConsignmentMerchandiseDevolutionJournalVoucherTypeId]  INT           NOT NULL,
    [ConsignmentInventoryUseJournalVoucherTypeId]           INT           NOT NULL,
    [ConsignmentInventoryUseDevolutionJournalVoucherTypeId] INT           NOT NULL,
    [NomanualentybarCode]                                   BIT           CONSTRAINT [DF_SettingInventory_NomanualentybarCode] DEFAULT ((0)) NOT NULL,
    [AssociateCostMainAccount]                              TINYINT       CONSTRAINT [DF_SettingInventory_AssociateCostMainAccount] DEFAULT ((1)) NOT NULL,
    [PartialReturnSalesJournalVoucherTypeId]                INT           NULL,
    [ActivateAutomaticPharmacyRequestProcess]               BIT           NULL,
    [ValidateBatchSerialExpiredDate]                        BIT           CONSTRAINT [DF_SettingInventory_ValidateBatchSerialExpiredDate] DEFAULT ((0)) NOT NULL,
    [ValidatePOSPathologies]                                BIT           CONSTRAINT [DF_SettingInventory_ValidatePOSPathologies] DEFAULT ((1)) NOT NULL,
    [PharmacyDashboardReport]                               TINYINT       CONSTRAINT [DF_SettingInventory_PharmacyDashboardReport] DEFAULT ((1)) NOT NULL,
    [PharmacyDashboardFromRequestWarehouse]                 BIT           CONSTRAINT [DF_SettingInventory_PharmacyDashboardByFunctionalUnit] DEFAULT ((0)) NOT NULL,
    [CommitmentBudgetInterface]                             BIT           CONSTRAINT [DF__SettingIn__Commi__5E6ED1EC] DEFAULT ((0)) NOT NULL,
    [PurchaseOrderInterface]                                TINYINT       CONSTRAINT [DF__SettingIn__Purch__6B49AA1D] DEFAULT ((0)) NOT NULL,
    [PurchaseOrderURL]                                      VARCHAR (500) NULL,
    [PurchaseOrderIdentifier]                               VARCHAR (200) NULL,
    [PurchaseOrderUser]                                     VARCHAR (200) NULL,
    [PurchaseOrderPass]                                     VARCHAR (200) NULL,
    [DispensingWithoutAuthorization]                        BIT           CONSTRAINT [DF_SettingInventory_DispensingWithoutAuthorization] DEFAULT ((1)) NOT NULL,
    [WithoutCurrentAuthorizationColor]                      INT           CONSTRAINT [DF_SettingInventory_WithoutCurrentAuthorizationColor] DEFAULT ((0)) NOT NULL,
    [ChangeAfter]                                           TINYINT       CONSTRAINT [DF_SettingInventory_ChangeAfter] DEFAULT ((0)) NOT NULL,
    [ChangeAfterTimeUnit]                                   TINYINT       CONSTRAINT [DF_SettingInventory_ChangeAfterTimeUnit] DEFAULT ((1)) NOT NULL,
    [WithoutAuthorizationManagementColor]                   INT           CONSTRAINT [DF_SettingInventory_WithoutAuthorizationManagementColor] DEFAULT ((0)) NOT NULL,
    [AllowDispensingWithExhaustedAuthorization]             BIT           CONSTRAINT [DF_SettingInventory_AllowDispensingWithExhaustedAuthorization] DEFAULT ((1)) NOT NULL,
    [AllowBillingWithoutAuthorization]                      BIT           CONSTRAINT [DF_SettingInventory_AllowBillingWithoutAuthorization] DEFAULT ((1)) NOT NULL,
    [TaxRegistration]                                       TINYINT       CONSTRAINT [DF__SettingIn__TaxRe__19A70858] DEFAULT ((1)) NOT NULL,
    [TransferBetweenWarehousesConsignmentId]                INT           NULL,
    [PharmacySuppliesCostCenter]                            TINYINT       CONSTRAINT [DF_SettingInventory_PharmacySuppliesCostCenter] DEFAULT ((1)) NOT NULL,
    [PackageDispensingMethod]                               BIT           CONSTRAINT [DF__SettingIn__Packa__3D503748] DEFAULT ((1)) NOT NULL,
    [ValuationConsignmentPriceJournalVoucherTypesId]        INT           NULL,
    [isClosedMonth]                                         BIT           DEFAULT ((0)) NOT NULL,
    [MainWarehouseId]                                       INT           NULL,
    [AllowMedicationsControls]                              BIT           CONSTRAINT [DF_SettingInventory_AllowMedicationsControls] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_SettingInventory] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SettingInventory_AccountPayableConceptNotes] FOREIGN KEY ([RefundAccountPayableConceptNoteId]) REFERENCES [Payments].[AccountPayableConceptNotes] ([Id]),
    CONSTRAINT [FK_SettingInventory_AccountPayableConcepts] FOREIGN KEY ([IVAAccountPayableConceptId]) REFERENCES [Payments].[AccountPayableConcepts] ([Id]),
    CONSTRAINT [FK_SettingInventory_AccountPayableConcepts1] FOREIGN KEY ([IVAFreightAccountPayableConceptId]) REFERENCES [Payments].[AccountPayableConcepts] ([Id]),
    CONSTRAINT [FK_SettingInventory_AccountPayableConcepts2] FOREIGN KEY ([ProDevelopmentAccountPayableConceptId]) REFERENCES [Payments].[AccountPayableConcepts] ([Id]),
    CONSTRAINT [FK_SettingInventory_AccountPayableConcepts3] FOREIGN KEY ([ProElectrificationAccountPayableConceptId]) REFERENCES [Payments].[AccountPayableConcepts] ([Id]),
    CONSTRAINT [FK_SettingInventory_AccountPayableConcepts4] FOREIGN KEY ([ProCultureAccountPayableConceptId]) REFERENCES [Payments].[AccountPayableConcepts] ([Id]),
    CONSTRAINT [FK_SettingInventory_AccountPayableConcepts5] FOREIGN KEY ([ProHospitalAccountPayableConceptId]) REFERENCES [Payments].[AccountPayableConcepts] ([Id]),
    CONSTRAINT [FK_SettingInventory_AccountPayableConcepts7] FOREIGN KEY ([ProGameAccountPayableConceptId1]) REFERENCES [Payments].[AccountPayableConcepts] ([Id]),
    CONSTRAINT [FK_SettingInventory_AccountPayableConcepts8] FOREIGN KEY ([FreightAccountPayableConceptId]) REFERENCES [Payments].[AccountPayableConcepts] ([Id]),
    CONSTRAINT [FK_SettingInventory_AccountPayableConcepts9] FOREIGN KEY ([AdjustmentAccountPayableConceptId]) REFERENCES [Payments].[AccountPayableConcepts] ([Id]),
    CONSTRAINT [FK_SettingInventory_AdjustmentConcept] FOREIGN KEY ([InputAdjustmentConceptId]) REFERENCES [Inventory].[AdjustmentConcept] ([Id]),
    CONSTRAINT [FK_SettingInventory_AdjustmentConcept1] FOREIGN KEY ([OutputAdjustmentConceptId]) REFERENCES [Inventory].[AdjustmentConcept] ([Id]),
    CONSTRAINT [FK_SettingInventory_FilingUnit] FOREIGN KEY ([FilingUnitId]) REFERENCES [Payments].[FilingUnit] ([Id]),
    CONSTRAINT [FK_SettingInventory_GeneralLedgerIVA] FOREIGN KEY ([FreightIVAId]) REFERENCES [GeneralLedger].[GeneralLedgerIVA] ([Id]),
    CONSTRAINT [FK_SettingInventory_JournalVoucherType20] FOREIGN KEY ([ValuationConsignmentPriceJournalVoucherTypesId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingInventory_JournalVoucherTypes] FOREIGN KEY ([PurchaseJournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingInventory_JournalVoucherTypes1] FOREIGN KEY ([SalesJournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingInventory_JournalVoucherTypes10] FOREIGN KEY ([OrderDispatchJournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingInventory_JournalVoucherTypes11] FOREIGN KEY ([RemissionOutputDevolutionJournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingInventory_JournalVoucherTypes12] FOREIGN KEY ([RemissionEntranceDevolutionJournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingInventory_JournalVoucherTypes13] FOREIGN KEY ([ReclassificationRemissionJournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingInventory_JournalVoucherTypes14] FOREIGN KEY ([InventoryCloseAdjustmentJournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingInventory_JournalVoucherTypes15] FOREIGN KEY ([ConsignmentMerchandiseJournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingInventory_JournalVoucherTypes16] FOREIGN KEY ([ConsignmentMerchandiseDevolutionJournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingInventory_JournalVoucherTypes17] FOREIGN KEY ([ConsignmentInventoryUseJournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingInventory_JournalVoucherTypes18] FOREIGN KEY ([ConsignmentInventoryUseDevolutionJournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingInventory_JournalVoucherTypes19] FOREIGN KEY ([PartialReturnSalesJournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingInventory_JournalVoucherTypes2] FOREIGN KEY ([RemissionEntranceJournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingInventory_JournalVoucherTypes3] FOREIGN KEY ([RemissionOutputJournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingInventory_JournalVoucherTypes4] FOREIGN KEY ([InventoryAdjustmentJournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingInventory_JournalVoucherTypes5] FOREIGN KEY ([LoanJournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingInventory_JournalVoucherTypes6] FOREIGN KEY ([SalesReturnJournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingInventory_JournalVoucherTypes7] FOREIGN KEY ([PurchaseReturnJournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingInventory_JournalVoucherTypes8] FOREIGN KEY ([OrderDispatchReturnJournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingInventory_JournalVoucherTypes9] FOREIGN KEY ([LoanReturnJournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingInventory_MainAccounts1] FOREIGN KEY ([DiscountSalesMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_SettingInventory_MainWarehouse] FOREIGN KEY ([MainWarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id]),
    CONSTRAINT [FK_SettingInventory_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_SettingInventory_ThirdParty] FOREIGN KEY ([TransferOrderThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_SettingInventory_ThirdParty1] FOREIGN KEY ([PharmaceuticalDispensingThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Habilita semaforización/control de medicamentos por estado. 1=Sí (control activo), 0=No (sin control).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'AllowMedicationsControls';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Maneja semaforización de control de medicamentos | 1 = Si | 0 = No', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'AllowMedicationsControls';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'AllowMedicationsControls';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Identificador de la bodega/almacén designado como principal para operaciones de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'MainWarehouseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la bodega que se establece como principal', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'MainWarehouseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'MainWarehouseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Indicador de cierre mensual de inventario. 0=Período abierto, 1=Período cerrado/finalizado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'isClosedMonth';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Parametro para identificar si se esta realizando el cierre mensual de inventario, 0 - no , 1 - si', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'isClosedMonth';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'isClosedMonth';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. FK a tipo de comprobante contable para valoración de precio en inventario en consignación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ValuationConsignmentPriceJournalVoucherTypesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de tipos de comprobantes contables', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ValuationConsignmentPriceJournalVoucherTypesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ValuationConsignmentPriceJournalVoucherTypesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT. Método de dispensación de paquetes quirúrgicos. 1=Total (completo), 0=Parcial (por componente).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'PackageDispensingMethod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si la dispensacion de los paquetes quirurgicos son totales (1) o parciales (0) ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'PackageDispensingMethod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'PackageDispensingMethod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT. Origen del centro de costo para insumos dispensados en farmacia. 1=Unidad Funcional, 2=Concepto de facturación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'PharmacySuppliesCostCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Centro de costo de los insumos dispensados en farmacia : 1 - Unidad Funcional. 2 - Concepto de facturación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'PharmacySuppliesCostCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'PharmacySuppliesCostCenter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. FK a tipo de comprobante contable para traslados entre almacenes en consignación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'TransferBetweenWarehousesConsignmentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo de comprobante contables para el Traslado entre almacenes en consignación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'TransferBetweenWarehousesConsignmentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'TransferBetweenWarehousesConsignmentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT. Método de registro contable del IVA. 1=Al Costo, 2=Descontable, 3=Mixto.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'TaxRegistration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica como se va a llevar el Registro del IVA  1 - IVA al Costo  2 - IVA Descontable  3 - IVA Mixto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'TaxRegistration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'TaxRegistration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permitir facturación sin autorización | 1 = Si | 0 = No', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'AllowBillingWithoutAuthorization';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Permite dispensación cuando autorización está agotada/vencida. 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'AllowDispensingWithExhaustedAuthorization';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permitir dispensación con autorización agotada | 1 = Si | 0 = No', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'AllowDispensingWithExhaustedAuthorization';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'AllowDispensingWithExhaustedAuthorization';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Código RGB/color para avisos visuales de dispensación sin gestión de autorización vigente.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'WithoutAuthorizationManagementColor';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sin autorización Vigente - Se diligencia con el número del color elegido', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'WithoutAuthorizationManagementColor';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'WithoutAuthorizationManagementColor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT. Unidad temporal para cambio de autorización. 1=Minutos, 2=Horas, 3=Días, 4=Semanas.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ChangeAfterTimeUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Minutos  2 - Horas  3 - Días  4 - Semanas  ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ChangeAfterTimeUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ChangeAfterTimeUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT. Habilita carga diferida/demorada de comprobantes. 1=Sí (cargar después), 0=No (inmediato).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ChangeAfter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cargar después | 1 = Si | 0 = No', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ChangeAfter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ChangeAfter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Código RGB/color para avisos visuales de dispensación sin autorización vigente.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'WithoutCurrentAuthorizationColor';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sin gestión de autorización - Se diligencia con el número del color elegido', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'WithoutCurrentAuthorizationColor';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'WithoutCurrentAuthorizationColor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Autoriza dispensación de medicamentos sin preautorización. 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'DispensingWithoutAuthorization';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dispensación sin autorización | 1 = Si | 0 = No', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'DispensingWithoutAuthorization';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'DispensingWithoutAuthorization';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(200). Credencial/contraseña cifrada para integración con servicio externo de órdenes de compra.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'PurchaseOrderPass';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contraseña servicio orden de compra', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'PurchaseOrderPass';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'PurchaseOrderPass';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(200). Usuario/login para autenticación en interfaz de órdenes de compra (OC) externa.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'PurchaseOrderUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario servicio orden de compra', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'PurchaseOrderUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'PurchaseOrderUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(200). ID/código del cliente en sistema externo de gestión de órdenes de compra.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'PurchaseOrderIdentifier';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del cliente servicio orden de compra', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'PurchaseOrderIdentifier';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'PurchaseOrderIdentifier';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(500). Endpoint/URL del servicio web para sincronización de órdenes de compra.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'PurchaseOrderURL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Url servicio', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'PurchaseOrderURL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'PurchaseOrderURL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT. Proveedor de interfaz para OC. 0=Ninguna, 1=SBS, 2=Bionexo.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'PurchaseOrderInterface';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de interfaz que se utilizara para el manejo de las ordenes de compra  0 - Ninguna  1 - SBS  2 - Bionexo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'PurchaseOrderInterface';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'PurchaseOrderInterface';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Obliga asociación de compromisos presupuestales en contratos y órdenes de compra. 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'CommitmentBudgetInterface';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si es obligatoria la asociación del compromiso en los contratos y ordenes de compra', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'CommitmentBudgetInterface';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'CommitmentBudgetInterface';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Restringe dispensación desde dashboard. 0=Cualquier almacén, 1=Solo almacén de la solicitud.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'PharmacyDashboardFromRequestWarehouse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite identificar si la dispensación desde el dashboard de farmacia se realizará desde el almacén asociado a la solicitud.    0. Se podrá dispensar de cualquier almacén  1. Al realizar la entrega manual solo se dispensará del almacén de la solicitud, aunque no se tenga permiso', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'PharmacyDashboardFromRequestWarehouse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'PharmacyDashboardFromRequestWarehouse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT. Formato de reporte en dispensación farmacéutica. 1=Tirilla (comprobante corto), 2=Reporte detallado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'PharmacyDashboardReport';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Reporte a usar en la dispensación Farmacéutica desde el Dashboard de Farmacia:  1 - Tirilla  2 - Reporte', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'PharmacyDashboardReport';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'PharmacyDashboardReport';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Valida patologías cubiertas por POS al dispensar medicamentos. 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ValidatePOSPathologies';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Validar Patologías POS en Medicamentos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ValidatePOSPathologies';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ValidatePOSPathologies';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Verifica fecha de vencimiento del lote/serie al dispensar. 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ValidateBatchSerialExpiredDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si se valida o no la fecha de vencimiento del lote al momento de dispensar', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ValidateBatchSerialExpiredDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ValidateBatchSerialExpiredDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Activa generación automática de solicitudes a farmacia. 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ActivateAutomaticPharmacyRequestProcess';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si activa o no el proceso de solicitud automática a farmacia', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ActivateAutomaticPharmacyRequestProcess';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ActivateAutomaticPharmacyRequestProcess';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. FK a tipo de comprobante para devoluciones parciales de ventas.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'PartialReturnSalesJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo de comprobante para la devolución parcial de venta', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'PartialReturnSalesJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'PartialReturnSalesJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT. Origen de cuenta contable de costo/venta. 1=Parámetros Inventario, 2=Grupo del Producto.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'AssociateCostMainAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica de donde se va tomar la cuenta contable de costo y venta cuando se hacen los comprobantes contables  1 - Parámetros de Inventario  2 - Grupo del Producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'AssociateCostMainAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'AssociateCostMainAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Prohíbe ingreso manual de códigos de barras. 1=No permite manual, 0=Permite manual.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'NomanualentybarCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'esta Columna utiliza para el campo "No permitir código de barras Manualmente " y se muestra valor en el campo si o no por defecto no', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'NomanualentybarCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'NomanualentybarCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. FK a comprobante para devolución de dispensación/uso en consignación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ConsignmentInventoryUseDevolutionJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id del tipo del comprobante para cuando se realiza un registro de devolución de la dispensación o del uso del inventario en consignación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ConsignmentInventoryUseDevolutionJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ConsignmentInventoryUseDevolutionJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. FK a comprobante para dispensación/uso de inventario en consignación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ConsignmentInventoryUseJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id del tipo del comprobante para cuando se realiza un registro de dispensación o uso del inventario en consignación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ConsignmentInventoryUseJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ConsignmentInventoryUseJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. FK a comprobante para devolución de mercancía en consignación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ConsignmentMerchandiseDevolutionJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id del tipo del comprobante para cuando se realiza un registro de devolución de inventario en consignación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ConsignmentMerchandiseDevolutionJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ConsignmentMerchandiseDevolutionJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. FK a comprobante para registro de inventario en consignación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ConsignmentMerchandiseJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id del tipo del comprobante para cuando se realiza un registro de inventario en consignación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ConsignmentMerchandiseJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ConsignmentMerchandiseJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. FK a comprobante para ajustes al cierre mensual de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'InventoryCloseAdjustmentJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de comprobante contable que se va a crear cuando se realice un ajuste en el cierre mensual de inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'InventoryCloseAdjustmentJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'InventoryCloseAdjustmentJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. FK a comprobante para remisiones de reclasificación de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ReclassificationRemissionJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id. De tipo de comprobante de diario de remisión de reclasificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ReclassificationRemissionJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ReclassificationRemissionJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. FK a comprobante para devoluciones de remisiones de entrada.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'RemissionEntranceDevolutionJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id del tipo del comprobante para cuando se realiza una devolucion de una remision de entrada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'RemissionEntranceDevolutionJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'RemissionEntranceDevolutionJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. FK a comprobante para devoluciones de remisiones de salida.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'RemissionOutputDevolutionJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id del tipo del comprobante para cuando se realiza una devolucion de una remision de salida', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'RemissionOutputDevolutionJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'RemissionOutputDevolutionJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TIMESTAMP. Marca temporal automática del servidor (creación/modificación de registro).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME. Fecha y hora de última modificación del parámetro.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(20). Usuario que realizó la última modificación del registro.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME. Fecha y hora de creación del parámetro de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(20). Usuario que creó el registro de parámetros.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. FK a tercero (proveedor/empresa) para comprobantes en dispensación. Se usa si PharmaceuticalDispensingGetThirdParty=2.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'PharmaceuticalDispensingThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Tercero que se debe tomar cuando se haga un comprobante en dispensacion Farmaceutica    Nota: Este campo solo se solicita si el campo PharmaceuticalDispensingGetThirdParty esta en "2"', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'PharmaceuticalDispensingThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'PharmaceuticalDispensingThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT. Origen del tercero en dispensación farmacéutica. 1=Tercero del paciente, 2=Tercero específico parametrizado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'PharmaceuticalDispensingGetThirdParty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica de donde se va a obtener el tercero para el comprobante contable de la dispensacion farmaceutica  1 -  Tomar Tercero del paciente  2 -  Tercero Especifico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'PharmaceuticalDispensingGetThirdParty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'PharmaceuticalDispensingGetThirdParty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT. Origen del tercero en órdenes de traslado. 1=Del documento, 2=Del parámetro (afecta contabilidad).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'TakeTransferOrderThirdParty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'especifica de donde se obtiene el tercero cuando se realiza ordenes de traslado  1-tercero del documento  2-tercero del parametro    nota= este tercero es solo para afectar contabilidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'TakeTransferOrderThirdParty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'TakeTransferOrderThirdParty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. FK a tercero para órdenes de traslado, usado si cuenta de inventarios maneja tercero.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'TransferOrderThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id del tercero que se debe tomar para realizar las ordenes de traslado, este se toma si y solo si la cuenta contable de inventarios maneja tercero', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'TransferOrderThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'TransferOrderThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. FK a concepto de ajuste de salida en control de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'OutputAdjustmentConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el concepto de ajuste de inventarios de Tipo Salida que se debe utilizar cuando en ajuste de inventario se haga un registro de tipo Control de inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'OutputAdjustmentConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'OutputAdjustmentConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. FK a concepto de ajuste de entrada en control de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'InputAdjustmentConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el concepto de ajuste de inventarios de Tipo Entrada que se debe utilizar cuando en ajuste de inventario se haga un registro de tipo Control de inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'InputAdjustmentConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'InputAdjustmentConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. FK a concepto de nota de pago para devoluciones de comprobantes de entrada.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'RefundAccountPayableConceptNoteId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id del concepto de nota de pagos que se va usar cuando se haga una devolucion del comprobante de entrada    El concepto de la nota de pagos debe ser de tipo General', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'RefundAccountPayableConceptNoteId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'RefundAccountPayableConceptNoteId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. FK a cuenta contable donde se registran descuentos de dispensación farmacéutica.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'DiscountSalesMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cuenta contable del descuento para ventas, En esta cuenta se cargan los descuento realizados a través de la dispensacion farmaceutica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'DiscountSalesMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'DiscountSalesMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT. Asignación de centro de costo. 1=Sin CC/Unidad Funcional, 2=Grupo, 3=Almacén.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'AssociateCostCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica de donde se va tomar el centro de costo para cuando se hacen los comprobantes contables    1 - Inventarios(Sin CC), Costo(Unidad Funcional)  2 - Inventarios(Grupo), Costo(Grupo)  2 - Inventarios(Almacen), Costo(Almacen)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'AssociateCostCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'AssociateCostCenter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. IVA se suma al costo del producto (inventario) vs. cuenta contable separada. 1=Sí al costo, 0=No (CxP).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'IVACost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el IVA al costo, Si el Iva va al costo, cuando se realiza el comprobante de entrada el valor que se calcula del iva se suma al valor del item menos el descuento y se lleva a la cuenta de inventarios, es decir que se lleva como un mayor valor del producto, Si el iva NO va al costo pues simplementa el iva se contabiliza en la cuenta contable que se parametriza en settings', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'IVACost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'IVACost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. FK a concepto CxP para ajuste de redondeo (tipo Específico, sin retención).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'AdjustmentAccountPayableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de la cuenta por pagar para el ajuste de redondeo, El concepto de ajuste debe ser de Tipo Especifico y NO debe manejar retencion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'AdjustmentAccountPayableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'AdjustmentAccountPayableConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. FK a cuenta contable de IVA generado en facturación de productos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'IVAGeneratedMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id de la cuenta contable para almacenar el iva GENERADO, esta es utilizada al momento de generar una factura de productos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'IVAGeneratedMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'IVAGeneratedMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. FK a concepto CxP para IVA descontable (tipo Específico, sin retención).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'IVAAccountPayableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de pagos para el IVA, este concepto debe de ser de tipo Especifico y NO debe manejar Retencion, Este concepto es utilizado cuando se cimfirma el comprobante de entrada y genera la cuenta por pagar    este representaria al iva descontable', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'IVAAccountPayableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'IVAAccountPayableConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. FK a unidad de radicación/radicar para comprobantes contables.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'FilingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad de radicacion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'FilingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'FilingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. FK a concepto CxP para retención de IVA (tipo Específico, con retención).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'IVARetentionAccountPayableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de pagos para la retencion del IVA, este concepto debe de ser de tipo Especifico y debe manejar Retencion, Solo se llena cuando la rentencion del iva se obtiene del concepto (IVARetention - 2)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'IVARetentionAccountPayableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'IVARetentionAccountPayableConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT. Origen de retención de IVA. 1=Del tercero/proveedor, 2=Del concepto parametrizado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'IVARetention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica de donde se va a sacar la retencion del iva  1 - Tercero  2 - Concepto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'IVARetention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'IVARetention';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. FK a concepto CxP para impuesto distrital Pro-Juego (solo empresas públicas).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ProGameAccountPayableConceptId1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de cuenta por pagar para el impuesto distrital de pro Juego, Este concepto debe Manejar Retencion y debe ser de tipo Especifico    Este campo solo se solicita si la empresa es publica, pero no es obligatoria', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ProGameAccountPayableConceptId1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ProGameAccountPayableConceptId1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. FK a concepto CxP para impuesto distrital Pro-Hospital (solo empresas públicas).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ProHospitalAccountPayableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de cuenta por pagar para el impuesto distrital de pro Hospital, Este concepto debe Manejar Retencion y debe ser de tipo Especifico    Este campo solo se solicita si la empresa es publica, pero no es obligatoria', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ProHospitalAccountPayableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ProHospitalAccountPayableConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. FK a concepto CxP para impuesto distrital Pro-Cultura (solo empresas públicas).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ProCultureAccountPayableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de cuenta por pagar para el impuesto distrital de procultura, Este concepto debe Manejar Retencion y debe ser de tipo Especifico    Este campo solo se solicita si la empresa es publica, pero no es obligatoria', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ProCultureAccountPayableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ProCultureAccountPayableConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. FK a concepto CxP para impuesto distrital Pro-Electrificación (solo empresas públicas).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ProElectrificationAccountPayableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de cuenta por pagar para el impuestro distrital de proElectrificacion, Este concepto debe Manejar Retencion y debe ser de tipo Especifico    Este campo solo se solicita si la empresa es publica, pero no es obligatoria', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ProElectrificationAccountPayableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ProElectrificationAccountPayableConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. FK a concepto CxP para impuesto distrital Pro-Desarrollo (solo empresas públicas).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ProDevelopmentAccountPayableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de cuenta por pagar para el impuesto distrital de prodesarrollo, Este concepto debe Manejar Retencion y debe ser de tipo Especifico    Este campo solo se solicita si la empresa es publica, pero no es obligatoria', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ProDevelopmentAccountPayableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'ProDevelopmentAccountPayableConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. FK a concepto CxP para flete/transporte (tipo Específico, sin retención).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'FreightAccountPayableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de cuentas por pagar para el flete, Este concepto de cuenta por pagar NO debe Manejar Retencion y debe ser de tipo Especifico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'FreightAccountPayableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'FreightAccountPayableConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. FK a concepto CxP para IVA del flete (tipo Específico, sin retención).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'IVAFreightAccountPayableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de cuentas por pagar par a el porcentaje de IVA del flete,  este concepto debe de ser de tipo Especifico y NO debe manejar Retencion, Este concepto es utilizado cuando se cimfirma el comprobante de entrada y genera la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'IVAFreightAccountPayableConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'IVAFreightAccountPayableConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. FK a tarifa/porcentaje de IVA aplicable al flete en compras.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'FreightIVAId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id del iva del flete', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'FreightIVAId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'FreightIVAId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. FK a tipo de comprobante contable para ajustes de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'InventoryAdjustmentJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de comprobante contable que se va a crear cuando se realice un ajuste de inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'InventoryAdjustmentJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'InventoryAdjustmentJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. FK a comprobante para devoluciones de órdenes de traslado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'OrderDispatchReturnJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo del comprobante contable que se genera al hacer una devolucion de una orden de traslado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'OrderDispatchReturnJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'OrderDispatchReturnJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. FK a comprobante para creación de órdenes de traslado entre almacenes.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'OrderDispatchJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo comprobante contable que se debe generar al crear una orden de traslado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'OrderDispatchJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'OrderDispatchJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. FK a comprobante para devoluciones en compras de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'PurchaseReturnJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id del tipo del comprobante que se debe generar al hacer una devolucion en compras', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'PurchaseReturnJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'PurchaseReturnJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. FK a comprobante para devoluciones en ventas/dispensación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'SalesReturnJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id del tipo del comprobante que se debe generar al hacer una devolucion en ventas', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'SalesReturnJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'SalesReturnJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. FK a comprobante para devoluciones de préstamos de mercancía.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'LoanReturnJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id del tipo del comprobante que se debe generar al crear una devolucion de prestamo de mercancia', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'LoanReturnJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'LoanReturnJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. FK a comprobante para préstamos de mercancía entre unidades.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'LoanJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id del tipo del comprobante que se debe generar al crear un prestamo de mercancia', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'LoanJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'LoanJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. FK a comprobante para remisiones de salida de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'RemissionOutputJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id del tipo del comprobante para cuando se realiza remisiones de salida', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'RemissionOutputJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'RemissionOutputJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. FK a comprobante para remisiones de entrada de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'RemissionEntranceJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id del tipo del comprobante para cuando se ejecuta una remision de entrada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'RemissionEntranceJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'RemissionEntranceJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. FK a comprobante contable para ventas/dispensación de productos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'SalesJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de comprobante contable para cuando se realice una venta', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'SalesJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'SalesJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. FK a comprobante contable para compras de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'PurchaseJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de comprobante para las compras de inventarios', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'PurchaseJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'PurchaseJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT. Método de control de existencias. 1=General (centralizado), 2=Por Almacén (distribuido).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'StockControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica como se va a llevar el control del stock  1 - General  2 - Por Almacen', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'StockControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'StockControl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Mes del período contable actual (1-12) para cierre y reportes de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'Month';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mes del  periodo actual de inventarios', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'Month';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'Month';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Año del período contable actual (AAAA) para cierre y reportes de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'Year';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Año del periodo actual de inventarios', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'Year';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'Year';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. FK a unidad operativa/funcional a la que pertenecen estos parámetros de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id de la unidad operativa', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT IDENTITY. Clave primaria del registro de parámetros de inventario por período y unidad.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del parametro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros y configuración del módulo de inventario por unidad operativa y período (año/mes). Define los tipos de comprobante contable para cada movimiento de inventario (compras, ventas, remisiones, préstamos, ajustes, consignaciones), las cuentas contables y conceptos de cuentas por pagar asociados, y las reglas de negocio para dispensación farmacéutica, control de lotes/vencimientos, autorizaciones, centros de costo e integraciones externas de órdenes de compra.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventory';

GO
CREATE NONCLUSTERED INDEX [IX_SettingInventory_OperatingUnitId]
    ON [Inventory].[SettingInventory]([OperatingUnitId] ASC);
