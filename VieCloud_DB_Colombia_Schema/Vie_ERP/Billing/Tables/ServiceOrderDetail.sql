CREATE TABLE [Billing].[ServiceOrderDetail] (
    [Id]                                     INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ServiceOrderId]                         INT             NOT NULL,
    [CareGroupId]                            INT             NOT NULL,
    [HealthAdministratorId]                  INT             NULL,
    [ThirdPartyId]                           INT             NULL,
    [ServiceType]                            TINYINT         CONSTRAINT [DF_ServiceOrderDetail_ServiceType] DEFAULT ((3)) NOT NULL,
    [RecordType]                             TINYINT         NOT NULL,
    [CUPSEntityId]                           INT             NULL,
    [IPSServiceId]                           INT             NULL,
    [HospitalStayId]                         INT             NULL,
    [HospitalStayDetailId]                   INT             NULL,
    [ControlExternalConsultation]            TINYINT         NULL,
    [ControlExternalConsultationCode]        NUMERIC (18)    NULL,
    [CUPSAssociateService]                   BIT             NOT NULL,
    [CodeAssociateService]                   VARCHAR (50)    NULL,
    [IsPackage]                              BIT             CONSTRAINT [DF_ServiceOrderDetail_IsPackage] DEFAULT ((0)) NOT NULL,
    [Packaging]                              BIT             CONSTRAINT [DF_ServiceOrderDetail_Packaging] DEFAULT ((0)) NOT NULL,
    [PackageServiceOrderDetailId]            INT             NULL,
    [LiquidationType]                        TINYINT         CONSTRAINT [DF_ServiceOrderDetail_LiquidationType] DEFAULT ((1)) NOT NULL,
    [Presentation]                           TINYINT         NULL,
    [ProductId]                              INT             NULL,
    [InvoicedQuantity]                       INT             NOT NULL,
    [SupplyQuantity]                         INT             NOT NULL,
    [DevolutionQuantity]                     INT             NOT NULL,
    [RateManualSalePrice]                    NUMERIC (20, 2) NOT NULL,
    [CostValue]                              NUMERIC (20, 2) NOT NULL,
    [ServiceDate]                            DATETIME        NOT NULL,
    [AuthorizationNumber]                    VARCHAR (20)    NULL,
    [PerformsFunctionalUnitId]               INT             NOT NULL,
    [PerformsHealthProfessionalCode]         CHAR (20)       NULL,
    [PerformsProfessionalSpecialty]          CHAR (3)        NULL,
    [PerformsHealthProfessionalThirdPartyId] INT             NULL,
    [BillingConceptId]                       INT             NULL,
    [CostCenterId]                           INT             NOT NULL,
    [SettlementType]                         TINYINT         NOT NULL,
    [IncludeServiceOrderDetailId]            INT             NULL,
    [RecoveryRatio]                          NUMERIC (5, 2)  NULL,
    [RateManualId]                           INT             NULL,
    [RateManualType]                         TINYINT         NULL,
    [RateManualDetailId]                     INT             NULL,
    [DefinitionRateDetailId]                 INT             NULL,
    [DefinitionRateDetailConditionId]        INT             NULL,
    [SubTotalSalesPrice]                     NUMERIC (20, 2) NOT NULL,
    [ThirdPartyDiscount]                     NUMERIC (20, 2) NOT NULL,
    [ThirdPartyDiscountPercentage]           NUMERIC (5, 2)  NOT NULL,
    [TotalSalesPrice]                        NUMERIC (20, 2) NOT NULL,
    [GrandTotalSalesPrice]                   NUMERIC (20, 2) CONSTRAINT [DF_ServiceOrderDetail_GrandTotalSalesPrice] DEFAULT ((0)) NOT NULL,
    [SurchargeApply]                         BIT             NOT NULL,
    [SurgicalInterventionType]               TINYINT         NULL,
    [SurgeryNumber]                          TINYINT         CONSTRAINT [DF_ServiceOrderDetail_SurgeryNumber] DEFAULT ((0)) NOT NULL,
    [IsFirstEvent]                           BIT             CONSTRAINT [DF_ServiceOrderDetail_IsFirstEvent] DEFAULT ((0)) NOT NULL,
    [IsAnnulled]                             BIT             CONSTRAINT [DF_ServiceOrderDetail_IsAnnulled] DEFAULT ((0)) NOT NULL,
    [IsDelete]                               BIT             CONSTRAINT [DF_ServiceOrderDetail_IsDelete] DEFAULT ((0)) NOT NULL,
    [IncomeMainAccountId]                    INT             NOT NULL,
    [ApplyRIAS]                              BIT             NULL,
    [RIASCupsId]                             INT             NULL,
    [CUPSEntityContractDescriptionId]        INT             NULL,
    [QuotationServiceOrderDetailId]          INT             NULL,
    [RoundService]                           INT             CONSTRAINT [DF_ServiceOrderDetail_RoundService] DEFAULT ((1)) NOT NULL,
    [TraceabilityPaperworkEventsId]          INT             NULL,
    [ContractCoverageStatus]                 TINYINT         NULL,
    [ContractCoverageObservations]           VARCHAR (MAX)   NULL,
    [FinalProductCost]                       NUMERIC (20, 2) CONSTRAINT [DF__ServiceOr__Final__51965BC0] DEFAULT ((0)) NOT NULL,
    [ProductLiquidationType]                 TINYINT         CONSTRAINT [DF_ServiceOrderDetail_TarificType] DEFAULT ((1)) NOT NULL,
    [ContractPackageId]                      INT             NULL,
    [GrossValue]                             NUMERIC (20, 2) CONSTRAINT [DF__ServiceOr__Gross__5E313CCF] DEFAULT ((0)) NOT NULL,
    [TaxValue]                               NUMERIC (20, 2) CONSTRAINT [DF__ServiceOr__TaxVa__5F256108] DEFAULT ((0)) NOT NULL,
    [IvaId]                                  INT             NULL,
    [EconomicActivityId]                     INT             NULL,
    CONSTRAINT [PK_ServiceOrderDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ServiceOrderDetail_BillingConcept] FOREIGN KEY ([BillingConceptId]) REFERENCES [Billing].[BillingConcept] ([Id]),
    CONSTRAINT [FK_ServiceOrderDetail_CareGroup1] FOREIGN KEY ([CareGroupId]) REFERENCES [Contract].[CareGroup] ([Id]),
    CONSTRAINT [FK_ServiceOrderDetail_ContractDescriptions] FOREIGN KEY ([CUPSEntityContractDescriptionId]) REFERENCES [Contract].[CUPSEntityContractDescriptions] ([Id]),
    CONSTRAINT [FK_ServiceOrderDetail_ContractPackage] FOREIGN KEY ([ContractPackageId]) REFERENCES [Contract].[ContractPackage] ([Id]),
    CONSTRAINT [FK_ServiceOrderDetail_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_ServiceOrderDetail_CupsEntity] FOREIGN KEY ([CUPSEntityId]) REFERENCES [Contract].[CUPSEntity] ([Id]),
    CONSTRAINT [FK_ServiceOrderDetail_DefinitionRateDetail] FOREIGN KEY ([DefinitionRateDetailId]) REFERENCES [Contract].[DefinitionRateDetail] ([Id]),
    CONSTRAINT [FK_ServiceOrderDetail_DefinitionRateDetailCondition] FOREIGN KEY ([DefinitionRateDetailConditionId]) REFERENCES [Contract].[DefinitionRateDetailCondition] ([Id]),
    CONSTRAINT [FK_ServiceOrderDetail_EconomicActivity] FOREIGN KEY ([EconomicActivityId]) REFERENCES [Common].[EconomicActivity] ([Id]),
    CONSTRAINT [FK_ServiceOrderDetail_FunctionalUnit1] FOREIGN KEY ([PerformsFunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_ServiceOrderDetail_GeneralLedgerIVA] FOREIGN KEY ([IvaId]) REFERENCES [GeneralLedger].[GeneralLedgerIVA] ([Id]),
    CONSTRAINT [FK_ServiceOrderDetail_HealthAdministrator] FOREIGN KEY ([HealthAdministratorId]) REFERENCES [Contract].[HealthAdministrator] ([Id]),
    CONSTRAINT [FK_ServiceOrderDetail_IncludeServiceOrderDetail] FOREIGN KEY ([IncludeServiceOrderDetailId]) REFERENCES [Billing].[ServiceOrderDetail] ([Id]),
    CONSTRAINT [FK_ServiceOrderDetail_IPSService] FOREIGN KEY ([IPSServiceId]) REFERENCES [Contract].[IPSService] ([Id]),
    CONSTRAINT [FK_ServiceOrderDetail_MainAccounts] FOREIGN KEY ([IncomeMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ServiceOrderDetail_Product] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_ServiceOrderDetail_QuotationServiceOrderDetail] FOREIGN KEY ([QuotationServiceOrderDetailId]) REFERENCES [Billing].[QuotationServiceOrderDetail] ([Id]),
    CONSTRAINT [FK_ServiceOrderDetail_RateManual] FOREIGN KEY ([RateManualId]) REFERENCES [Contract].[RateManual] ([Id]),
    CONSTRAINT [FK_ServiceOrderDetail_RateManualDetail] FOREIGN KEY ([RateManualDetailId]) REFERENCES [Contract].[RateManualDetail] ([Id]),
    CONSTRAINT [FK_ServiceOrderDetail_ServiceOrder] FOREIGN KEY ([ServiceOrderId]) REFERENCES [Billing].[ServiceOrder] ([Id]),
    CONSTRAINT [FK_ServiceOrderDetail_ServiceOrderDetail] FOREIGN KEY ([PackageServiceOrderDetailId]) REFERENCES [Billing].[ServiceOrderDetail] ([Id]),
    CONSTRAINT [FK_ServiceOrderDetail_ThirdParty] FOREIGN KEY ([PerformsHealthProfessionalThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_ServiceOrderDetail_ThirdParty1] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_ServiceOrderDetail_TraceabilityPaperworkEvents] FOREIGN KEY ([TraceabilityPaperworkEventsId]) REFERENCES [Authorization].[TraceabilityPaperworkEvents] ([Id])
);


GO
ALTER TABLE [Billing].[ServiceOrderDetail] NOCHECK CONSTRAINT [FK_ServiceOrderDetail_CostCenter];


GO
ALTER TABLE [Billing].[ServiceOrderDetail] NOCHECK CONSTRAINT [FK_ServiceOrderDetail_CupsEntity];


GO
ALTER TABLE [Billing].[ServiceOrderDetail] NOCHECK CONSTRAINT [FK_ServiceOrderDetail_DefinitionRateDetailCondition];


GO
ALTER TABLE [Billing].[ServiceOrderDetail] NOCHECK CONSTRAINT [FK_ServiceOrderDetail_ServiceOrder];




GO
ALTER TABLE [Billing].[ServiceOrderDetail] NOCHECK CONSTRAINT [FK_ServiceOrderDetail_CostCenter];


GO
ALTER TABLE [Billing].[ServiceOrderDetail] NOCHECK CONSTRAINT [FK_ServiceOrderDetail_CupsEntity];


GO
ALTER TABLE [Billing].[ServiceOrderDetail] NOCHECK CONSTRAINT [FK_ServiceOrderDetail_DefinitionRateDetailCondition];


GO
ALTER TABLE [Billing].[ServiceOrderDetail] NOCHECK CONSTRAINT [FK_ServiceOrderDetail_ServiceOrder];




GO



GO



GO



GO



GO
ALTER TABLE [Billing].[ServiceOrderDetail] NOCHECK CONSTRAINT [FK_ServiceOrderDetail_CostCenter];


GO
ALTER TABLE [Billing].[ServiceOrderDetail] NOCHECK CONSTRAINT [FK_ServiceOrderDetail_CupsEntity];


GO



GO
ALTER TABLE [Billing].[ServiceOrderDetail] NOCHECK CONSTRAINT [FK_ServiceOrderDetail_DefinitionRateDetailCondition];


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
ALTER TABLE [Billing].[ServiceOrderDetail] NOCHECK CONSTRAINT [FK_ServiceOrderDetail_ServiceOrder];


GO



GO



GO



GO





GO



GO



GO



GO



GO
ALTER TABLE [Billing].[ServiceOrderDetail] NOCHECK CONSTRAINT [FK_ServiceOrderDetail_CostCenter];


GO
ALTER TABLE [Billing].[ServiceOrderDetail] NOCHECK CONSTRAINT [FK_ServiceOrderDetail_CupsEntity];


GO



GO
ALTER TABLE [Billing].[ServiceOrderDetail] NOCHECK CONSTRAINT [FK_ServiceOrderDetail_DefinitionRateDetailCondition];


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
ALTER TABLE [Billing].[ServiceOrderDetail] NOCHECK CONSTRAINT [FK_ServiceOrderDetail_ServiceOrder];


GO



GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_ServiceOrderDetail__Presentation__INC__CareGroupId__CUPSEntityId__Id__IncludeServiceOrderDetailId__IPSServiceId__LiquidationT]
    ON [Billing].[ServiceOrderDetail]([Presentation] ASC)
    INCLUDE([CareGroupId], [CUPSEntityId], [Id], [IncludeServiceOrderDetailId], [IPSServiceId], [LiquidationType], [PackageServiceOrderDetailId], [PerformsProfessionalSpecialty], [RateManualId], [RateManualType], [ServiceDate]);


GO
CREATE NONCLUSTERED INDEX [IX_ServiceOrderDetail__ProductId__INC__Id__ServiceOrderId]
    ON [Billing].[ServiceOrderDetail]([ProductId] ASC)
    INCLUDE([Id], [ServiceOrderId]);


GO
ALTER INDEX [IX_ServiceOrderDetail__ProductId__INC__Id__ServiceOrderId]
    ON [Billing].[ServiceOrderDetail] DISABLE;




GO
CREATE NONCLUSTERED INDEX [IDX_ServiceOrderDetail_[ApplyRIAS]
    ON [Billing].[ServiceOrderDetail]([ApplyRIAS] ASC)
    INCLUDE([ServiceOrderId], [CUPSEntityId]);


GO
ALTER INDEX [IDX_ServiceOrderDetail_[ApplyRIAS]
    ON [Billing].[ServiceOrderDetail] DISABLE;




GO
CREATE NONCLUSTERED INDEX [IX_ServiceOrderDetail]
    ON [Billing].[ServiceOrderDetail]([ServiceOrderId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ServiceOrderDetail__RecordType__IsDelete__SettlementType__INC__CUPSEntityId__Id__IPSServiceId__ProductId__ServiceDate__Servic]
    ON [Billing].[ServiceOrderDetail]([RecordType] ASC, [IsDelete] ASC, [SettlementType] ASC)
    INCLUDE([CUPSEntityId], [Id], [IPSServiceId], [ProductId], [ServiceDate], [ServiceOrderId], [TotalSalesPrice]);


GO
ALTER INDEX [IX_ServiceOrderDetail__RecordType__IsDelete__SettlementType__INC__CUPSEntityId__Id__IPSServiceId__ProductId__ServiceDate__Servic]
    ON [Billing].[ServiceOrderDetail] DISABLE;




GO
CREATE NONCLUSTERED INDEX [IX_ServiceOrderDetail__IsPackage__IsDelete__INC__Id__IPSServiceId__PerformsFunctionalUnitId__Presentation__ServiceOrderId]
    ON [Billing].[ServiceOrderDetail]([IsPackage] ASC, [IsDelete] ASC)
    INCLUDE([Id], [IPSServiceId], [PerformsFunctionalUnitId], [Presentation], [ServiceOrderId]);


GO
ALTER INDEX [IX_ServiceOrderDetail__IsPackage__IsDelete__INC__Id__IPSServiceId__PerformsFunctionalUnitId__Presentation__ServiceOrderId]
    ON [Billing].[ServiceOrderDetail] DISABLE;




GO
CREATE NONCLUSTERED INDEX [IX_ServiceOrderDetail__SettlementType__TotalSalesPrice__INC__CUPSEntityId__Id__InvoicedQuantity__IPSServiceId__Presentation__Ser]
    ON [Billing].[ServiceOrderDetail]([SettlementType] ASC, [TotalSalesPrice] ASC)
    INCLUDE([CUPSEntityId], [Id], [InvoicedQuantity], [IPSServiceId], [Presentation], [ServiceDate], [ThirdPartyDiscount]);


GO
CREATE NONCLUSTERED INDEX [IX_ServiceOrderDetail_ProductId_CostCenterId_IncomeMainAccountId]
    ON [Billing].[ServiceOrderDetail]([RecordType] ASC, [IsDelete] ASC, [SettlementType] ASC)
    INCLUDE([CostCenterId], [IncomeMainAccountId], [ProductId]);


GO
ALTER INDEX [IX_ServiceOrderDetail_ProductId_CostCenterId_IncomeMainAccountId]
    ON [Billing].[ServiceOrderDetail] DISABLE;




GO
CREATE NONCLUSTERED INDEX [IX_ServiceOrderDetail__IsDelete__INC__CareGroupId__CUPSEntityId__Id__IncludeServiceOrderDetailId__InvoicedQuantity__IPSServiceId]
    ON [Billing].[ServiceOrderDetail]([IsDelete] ASC)
    INCLUDE([CareGroupId], [CUPSEntityId], [Id], [IncludeServiceOrderDetailId], [InvoicedQuantity], [IPSServiceId], [IsPackage], [LiquidationType], [PerformsFunctionalUnitId], [PerformsProfessionalSpecialty], [Presentation], [RateManualId], [RateManualType], [RecordType], [ServiceDate], [ServiceOrderId]);


GO
CREATE TRIGGER [Billing].[tgServiceOrderDetailUpdate]
   ON  [Billing].[ServiceOrderDetail]
   AFTER INSERT, UPDATE
AS 
BEGIN
	SET NOCOUNT ON;

	if EXISTS(select 1
		from INSERTED i WITH(NOLOCK)
		inner join Contract.DefinitionRateDetailCondition drdc WITH(NOLOCK) on drdc.Id = i.DefinitionRateDetailConditionId
		inner join Contract.CareGroup cg WITH(NOLOCK) on cg.Id = i.CareGroupId
		inner join Contract.CUPSEntity ce WITH(NOLOCK) on ce.Id = i.CUPSEntityId
		where i.DefinitionRateDetailConditionId is not null and drdc.RIASId is not null 
		and cg.ApplyRIAS = 1 and ce.ApplyRIAS = 1
		and i.ApplyRIAS is null and i.RIASCupsId is null)
	begin
		THROW 51000, 'Validación tgServiceOrderDetailUpdate: No se puede insertar o actualizar un detalle de la orden de servicio con ApplyRIAS = NULL y RIASCupsId = NULL ya que el grupo de atención, el cups y la definición de tarifas aplican a RIAS', 1
	end
	
	if EXISTS(select 1
		from INSERTED i WITH(NOLOCK)
		where i.ServiceType = 0 and i.CUPSEntityId is null and i.ProductId is null 
		and i.RateManualId is null and i.RateManualType is null and i.RateManualDetailId is null
		and i.DefinitionRateDetailId is null and i.DefinitionRateDetailConditionId is null
		and i.TotalSalesPrice = 0 and i.GrandTotalSalesPrice = 0)
	begin
		THROW 51000, 'Validación tgServiceOrderDetailUpdate: No se puede insertar o actualizar un detalle de la orden de servicio con valor en cero', 1
	end
	    
END
GO
DISABLE TRIGGER [Billing].[tgServiceOrderDetailUpdate]
    ON [Billing].[ServiceOrderDetail];


GO
-- =============================================
-- Author:		Andres Alarcon
-- Create date: 2024-04-24
-- Description:	Se valida que servicio que se está tratando de eliminar no tenga incluido otro servicio
-- =============================================

CREATE TRIGGER [Billing].[tgServiceOrderDetailIncludedDeleted]
   ON  [Billing].[ServiceOrderDetail]
   AFTER DELETE
AS 
BEGIN
	SET NOCOUNT ON;

	IF EXISTS(
		SELECT 1
		FROM DELETED d WITH(NOLOCK)
		JOIN Billing.ServiceOrderDetail sod ON d.Id = sod.IncludeServiceOrderDetailId)
	BEGIN
		THROW 51000, 'No se puede eliminar el detalle de ordenes de servicio puesto que se le incluyo otro servicio', 1
	END
	    
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del impuesto IVA parametrizado en el sistema; tipo INT, referencia a tabla de configuración tributaria', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IvaId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del IVA parametrizado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IvaId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IvaId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del impuesto aplicado (IVA en Colombia); NUMERIC(20,2), calculado sobre el servicio o producto facturado', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'TaxValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del impuesto en colombia seria el IVA', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'TaxValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'TaxValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor bruto unitario sin descuentos ni impuestos; NUMERIC(20,2), base para cálculos posteriores', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'GrossValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Bruto', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'GrossValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'GrossValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del paquete contractual seleccionado cuando se empaquetan múltiples items; FK a Contract.ContractPackage', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ContractPackageId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Paquete seleccionado cuando se empaquetan items', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ContractPackageId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ContractPackageId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de tarifa del producto: 1=Item Producción, 2=Detalle Producción; TINYINT, solo se usa si el detalle es producto', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ProductLiquidationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Tarifa:    1 - Item Producción  2 - Detalle Producción    Sólo se inserta cuando el item corresponde a un producto', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ProductLiquidationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ProductLiquidationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Costo final establecido para el detalle del producto; NUMERIC(20,2), valor último después de ajustes', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'FinalProductCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el costo final del detalle.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'FinalProductCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'FinalProductCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones del cubrimiento contractual hospitalario; VARCHAR(MAX), asignadas desde Dashboard Cubrimiento Contractual', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ContractCoverageObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones, este campo se asigna en el Dashboard Cubrimiento Contractual Hospitalario', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ContractCoverageObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ContractCoverageObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del cubrimiento contractual: 1=Confirmado, 2=Contratado, 3=Cotizado; TINYINT, desde Dashboard Cubrimiento', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ContractCoverageStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del cubirmiento contractual:  1. Confirmado  2. Contratado  3. Cotizado    Este campo se asigna en el Dashboard Cubrimiento Contractual Hospitalario', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ContractCoverageStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ContractCoverageStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del evento autorizado en dashboard de autorizaciones; INT, asignado desde control servicios ambulatorios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del evento autorizado en el dashboard de autorizaciones, se asigna aca desde el formulario de control servicios ambulatorios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Forma de redondeo de valores: 1=Peso, 10=Decena, 100=Centena, 1000=Unidad Mil; INT, obtenido del manual tarifario', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RoundService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica como se va redondear los valores  1 -- Peso  10 -- Decena  100 -- Centena  1000 -- Unidad de Mil    Este valor se obtiene del manual de tarifa asociado, al momento de calcular el TotalSalesPrice', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RoundService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RoundService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle de cotización importada; INT, se anula al revocar orden para permitir re-solicitud', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'QuotationServiceOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la cotización, se asigna cuando se importa una cotización desde el formulario de ordenes de servicio, y al momento de anular la orden se nulea este campo para poder volverla a pedir', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'QuotationServiceOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'QuotationServiceOrderDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de descripción CUPS del módulo contratos; FK a Contract.CUPSEntityContractDescriptions', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CUPSEntityContractDescriptionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la descripción del módulo de contratos', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CUPSEntityContractDescriptionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CUPSEntityContractDescriptionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador RIAS asociado al CUPS en Crystal; INT, visible si grupo atención y servicio aplican RIAS', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RIASCupsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del RIAS asociado al cups en Crystal, este campo se muestra siempre y cuando el grupo de atención aplique a RIAS y además el servicio seleccionado también aplique a RIAS', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RIASCupsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RIASCupsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si aplica RIAS (Resolución 1448); BIT, depende de grupo atención y servicio seleccionado', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ApplyRIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite saber si aplica a RIAS, este campo se muestra siempre y cuando el grupo de atención aplique a RIAS y además el servicio seleccionado también aplique a RIAS', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ApplyRIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ApplyRIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta de ingresos contable (entidad o particular); INT FK, especifica destinación financiera', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IncomeMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cuenta de ingresos ya sea de la entidad o de particular', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IncomeMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IncomeMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Eliminación lógica de registros homologados anulados; BIT, solo aplica si IsAnnulled=true', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IsDelete';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Esta es una eliminacion logica la cual solo aplica para los homologos que ya hayan sido anulados es decir que tengan el campo IsAnnulled en true', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IsDelete';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IsDelete';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de anulación de homologo facturado; BIT, preserva datos para no afectar módulo honorarios médicos', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IsAnnulled';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Este campo solo es marcado cuando es un homologo y fue factura y posteriormente anulado, esto con el fin de no afectar el modulo de honorarios medicos', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IsAnnulled';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IsAnnulled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca si es primer evento de cirugía en acto quirúrgico; BIT, relacionado con SurgeryNumber', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IsFirstEvent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si es el primer evento de la cirujia (SurgeryNumber)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IsFirstEvent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IsFirstEvent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial de cirugía en mismo acto quirúrgico; TINYINT, identifica múltiples intervenciones simultáneas', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SurgeryNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Cirugia que sucede en el mismo Acto Quirurgico', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SurgeryNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SurgeryNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo intervención quirúrgica: 1=Básica, 2=Bilateral(SOAT), 3=MIVIE(ISS), 4=MDVIE(ISS), 5=MIVDE(ISS), 6=MDVDE(ISS), 7=PolitraumaIV(ISS), 8=PolitraumaDV(ISS), 9=No Cruento; TINYINT', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SurgicalInterventionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de intervencion quirurgica  1 - Basico  2 - Bilateral -- SOAT  3 - MIVIE (Multiple Igual Via Igual Especialista) -- ISS  4 - MDVIE (Multiple Diferente Via Igual Especialista) -- ISS  5 - MIVDE (Multiple Igual Via Diferente Especialista) -- ISS  6 - MDVDE (Multiple Diferente Via Diferente Especialista) -- ISS  7 - PolitraumaIV (Politrauma Igual Via) -- ISS  8 - PolitraumaDV (Politrauma Diferente Via) -- ISS  9- No Cruento  ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SurgicalInterventionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SurgicalInterventionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si se aplicó recargo al cálculo precio venta; BIT, afecta TotalSalesPrice', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SurchargeApply';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si se le aplico recargo al calculo del precio de venta', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SurchargeApply';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SurchargeApply';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total línea = TotalSalesPrice × InvoicedQuantity; NUMERIC(20,2), monto final facturado', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'GrandTotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Corresponde al valor total del item  TotalSalesPrice * InvoiceQuantity', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'GrandTotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'GrandTotalSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Precio unitario total cobrado = SubTotalSalesPrice - ThirdPartyDiscount; NUMERIC(20,2), valor a EAPB', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'TotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Corresponde al valor unitario total cobrado por el Producto / Servicio a la Entidad EAPB = Subtotalsalesprice - ThirpartyDiscount    ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'TotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'TotalSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje descuento aplicado a tercero responsable; NUMERIC(5,2), influye en cálculo final', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyDiscountPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de descuento aplicado al tercero responsable de la cuenta', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyDiscountPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyDiscountPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto descuento realizado a tercero responsable cuenta; NUMERIC(20,2), resta en liquidación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descuento realizado al tercero responsable de la cuenta', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Precio unitario calculado por contrato o digitado usuario; NUMERIC(20,2), base antes descuentos', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SubTotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Corresponde al valor unitario calculado por el contrato o digitado por el usuario si este tiene permiso del Producto / Servicio  ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SubTotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SubTotalSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador condición tarifa para recargos; FK a Contract.DefinitionRateDetailCondition, evita re-liquidar', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'DefinitionRateDetailConditionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el detalle de la condicion de la tarifa    Solo para servicios, estos campos se utilizan para cuando se quiere calcular el valor con recargo entonces no se vuelve a liquidar todo ya que con estos campos vamos directamente', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'DefinitionRateDetailConditionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'DefinitionRateDetailConditionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador detalle tarifa contractual; FK a Contract.DefinitionRateDetail, solo servicios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'DefinitionRateDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id del detalle de la tarifa    Solo aplica para Servicios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'DefinitionRateDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'DefinitionRateDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador detalle manual tarifario origen; INT, null si liquidación es Fija en Grupo Atención', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RateManualDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id del detalle del manual de tarifas de donde se tomo el valor.  Si el tipo de liquidacion en el Grupo de Atencion - Tarifas es Fijo este campo es null', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RateManualDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RateManualDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo manual tarifario: 1=ISS 2001, 2=ISS 2004, 3=SOAT; TINYINT, solo servicios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RateManualType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Tipo del manual tarifario con que se liquido el items  1 - ISS 2001  2 - ISS 2004  3 - SOAT      Solo se llena si el detalle es de tipo Servicios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RateManualType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RateManualType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador manual tarifario parametrizado; INT FK, consulta porcentajes para eventos especiales', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RateManualId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del manual de tarifas que tenga parametrizado en las tarifas o grupo de atencion, esto lo hacemos con el fin de que cuando tengan algun tipo de evento (Bilateran, MIVIE, ETC) se pueda consultar una forma facil el porcentaje a eventos', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RateManualId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RateManualId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de cobro aplicado; NUMERIC(5,2), determina monto a facturar', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RecoveryRatio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de cobro', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RecoveryRatio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RecoveryRatio';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador servicio para calcular porcentaje cobro o incluir 100%; INT, referencia liquidación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IncludeServiceOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id del servicio con el que se va a calcular el  porcentaje del valor a cobrar, o en el que se va incluir al 100%', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IncludeServiceOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IncludeServiceOrderDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo liquidación: 1=Manual tarifas, 2=% otro servicio, 3=100% incluido, 4=% mismo servicio; TINYINT', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SettlementType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de liquidacion   1. Por manual de tarifas (Defecto)  2. % de otro servicio cargado  3. 100% incluido dentro de otro servicio (No se cobra nada)  4. % del mismo servicio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SettlementType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SettlementType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador centro de costo; FK a Payroll.CostCenter, asigna gasto departamental', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Centro de Costo', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador grupo servicios/concepto facturación; FK a Billing.BillingConcept', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'BillingConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Grupo de Servicios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'BillingConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'BillingConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador tercero profesional salud ejecutor; INT, proveedor del servicio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del profesional de la salud tercero que realiza el servicio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código especialidad profesional ejecutor; CHAR(3), ej. MED, CIR, ORT', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PerformsProfessionalSpecialty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la especialidad el profesional que realiza', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PerformsProfessionalSpecialty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PerformsProfessionalSpecialty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código profesional salud ejecutor (tabla INPROFSAL); CHAR(20), cirujano principal en Qx', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Codigo del Profesional de la Salud que Realiza el Servicio y/o Administra el Producto.   Estos datos se sacan de la tabla de Crystal INPROFSAL  Para Qx Se especifica el codigo del profesional cirujano', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador unidad funcional donde se prestó servicio; INT FK, ubicación prestación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PerformsFunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad funcional donde se encontraba el paciente', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PerformsFunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PerformsFunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número autorización previo; VARCHAR(20), requerido para servicios controlados', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'AuthorizationNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Autorizacion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'AuthorizationNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'AuthorizationNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha prestación del servicio/producto; DATETIME, marca temporal de atención', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ServiceDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en que se presento el servicio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ServiceDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ServiceDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor costo del servicio o producto; NUMERIC(20,2), costo interno para análisis financiero', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CostValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor del costo', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CostValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CostValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Precio venta establecido en manual tarifario; NUMERIC(20,2), valor contrato o inventario', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RateManualSalePrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del Venta del servicio y/o producto establecido en el manual de tarifas de contratos o de inventarios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RateManualSalePrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RateManualSalePrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad devuelta de dispensación farmacéutica; INT, solo productos, acumula devoluciones', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'DevolutionQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Acumula la Cantidad Devuelta de las Dispensaciones Farmaceuticas (Solo Productos).    ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'DevolutionQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'DevolutionQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad entregada en dispensación farmacéutica; INT, solo productos, registra suministro', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SupplyQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Entregada (Solo Productos)  Productos: Almacena la cantidad entregada en la Dispensacion Farmaceutica (Pharmaceutical Dispensing)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SupplyQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SupplyQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad facturada (producto/servicio); INT, diferencia entre suministro y devoluciones', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'InvoicedQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Facturada (Producto / Servicio)  Productos: Almacena la diferencia entre SupplyAmount y DevolutionAmount', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'InvoicedQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'InvoicedQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador producto del inventario; INT FK, solo si ServiceType=Producto', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del producto, este se llena solo si el tipo de servicio es Producto', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presentación producto: 1=No quirúrgico, 2=Quirúrgico, 3=Paquete; TINYINT', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'Presentation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Presentacion del producto  1 - No quirurgico  2 - Quirurgico  3 - Paquete', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'Presentation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'Presentation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo liquidación: 1=Unidad/quemados, 2=Especialidad, 3=Unidad Funcional, 4=Fija, 5=Estándar, 6=Calculada/Fórmula; TINYINT', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'LiquidationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de liquidacion  1 - Tipo de unidad  - quemados  2 - Especialidad  3 - Unidad Funcional  4 - Fija  5 - Estandar  6 - Calculado o Formula', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'LiquidationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'LiquidationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador detalle orden donde se empaquetó servicio; INT, vincula ítems en paquete', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PackageServiceOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id del detalle de la orden de servicio donde se empaqueto el servicio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PackageServiceOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PackageServiceOrderDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si item está incluido en paquete; BIT, marca componentes de agrupación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'Packaging';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica que el Item se encuentra incluido dentro de un paquete', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'Packaging';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'Packaging';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si item es paquete con otros servicios; BIT, agrupa múltiples prestaciones', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IsPackage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el Item es un paquete, el cual esta incluyendo otros servicio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IsPackage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IsPackage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código homologación de servicios asociados; VARCHAR(50), identifica registros relacionados', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CodeAssociateService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el numero de identificacion unica para el proceso de homologacion. Todos los registros del mismo codigo corresponden a los homologos adicionales seleccionados por el usuario.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CodeAssociateService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CodeAssociateService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si registro es adicionado por homologación CUPS; BIT, automatiza servicios vinculados', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CUPSAssociateService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el registro corresponde a un registro adicionado automaticamente por estar relacionado en la homologacion de servicios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CUPSAssociateService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CUPSAssociateService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código evento control consulta externa (ADMORDLAB, ADMORDPAT, ADMORDIMA, ADCONCOEX); NUMERIC(18), depende clase registro', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ControlExternalConsultationCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Este campo especifica el Id del codigo registro generado en las tablas de Crystal ya que la tabla depende de la Clase de registro  
1 - Laboratorios --- Tabla ADMORDLAB   
2 - Patologias --- Tabla ADMORDPAT  
3 - Imagenes --- Tabla ADMORDIMA 
Para el resto de codigo la relacion es con ADCONCOEX
    Nota: Este campo solo se llena si la orden de servicio fue generada desde control de consulta externa o ambulatoria', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ControlExternalConsultationCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ControlExternalConsultationCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clase servicio control consulta: 1=Lab, 2=Patología, 3=Imagen, 4=Consulta Externa, 5=Quimio, 6=Radio, 7=Diálisis, 8=Ninguno, 9=Proc no Qx, 10=Proc Qx, 11=Interconsulta, 12=Otros; TINYINT', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ControlExternalConsultation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Este campo especifica el tipo o la clase del servicio de control de consulta externa 
 4- Consulta Externa 
 1 - Laboratorios 
 2 - Patologias
 3 - Imagenes 
5 - Quimioterapias
6 - Radioterapias, 
7 -  Diálisis
8 - Ninguno
9 - Procedimiento no Qx, 
10 - Procedimiento Qx
11 - Interconsultas,
12 - Otros Procedimientos 
  Nota: Este campo solo se llena si la orden de servicio fue generada desde control de consulta externa o ambulatoria', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ControlExternalConsultation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ControlExternalConsultation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador detalle estancia hospitalaria (CHREGESTADET); INT, solo registros estancia', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'HospitalStayDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id del detalle del registro de estancia (CHREGESTADET), este solo se llena cuando la orden de servicio es un registro de estancia', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'HospitalStayDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'HospitalStayDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador registro estancia (CHREGESTA); INT, solo si orden es de estancia', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'HospitalStayId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro de la estancia (CHREGESTA), Este campo solo se llena si es un registro de estancia', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'HospitalStayId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'HospitalStayId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador servicio IPS homologado a CUPS; INT, solo tipo Servicio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IPSServiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del servicio IPS el cual es homologado a través del CUPS, Solo se llenan si el tipo de registro es Servicio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IPSServiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IPSServiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador entidad CUPS del servicio; FK a Contract.CUPSEntity, solo Servicios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la entidad de CUPS, solo se llenan si el tipo de registro es Servicio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo detalle orden: 1=Servicios, 2=Medicamentos; TINYINT', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RecordType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se especifica el tipo de servicio de detalle de la orden  1 - Servicios  2 - Medicamentos', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RecordType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RecordType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo servicio forma carga: 1=SOAT, 2=ISS, 3=CUPS; TINYINT, origen tarifario', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ServiceType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de servicio, es decir como se cargo el item  1 - Soat  2 - ISS  3 - CUPS', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ServiceType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ServiceType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador tercero responsable (asegurador/EAPB); INT FK, entidad facturada', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero de la entidad', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador tercero contratante (EAPB/administrador); INT FK, aseguradora', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero con quien se realiza el contrato', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador grupo de atención (línea, cobertura); INT FK a Contract.CareGroup', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CareGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del grupo de atencion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CareGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CareGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador orden de servicios padre; INT FK a Billing.ServiceOrder', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ServiceOrderId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la orden de servicios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ServiceOrderId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ServiceOrderId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único detalle orden servicios; INT PK IDENTITY, clave primaria tabla', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la orden de servicios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
CREATE NONCLUSTERED INDEX [IX_ServiceOrderDetail_ServiceDate_IsDelete_Covering]
    ON [Billing].[ServiceOrderDetail]([ServiceDate] ASC, [IsDelete] ASC)
    INCLUDE([ServiceOrderId], [PerformsFunctionalUnitId], [CUPSEntityId], [ProductId], [InvoicedQuantity]) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [IX_ServiceOrderDetail_CostActivity]
    ON [Billing].[ServiceOrderDetail]([RecordType] ASC, [CUPSEntityId] ASC)
    INCLUDE([CostCenterId], [CUPSEntityContractDescriptionId], [InvoicedQuantity]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los ítems facturados en cada orden de servicio: procedimientos, medicamentos, insumos y estancias hospitalarias con sus cantidades, tarifas, descuentos, impuestos y valores totales facturados al paciente o al tercero pagador (EPS, aseguradora, particular).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Actividad económica asociada al ítem facturado, usada para clasificación tributaria y generación de reportes fiscales (por ejemplo, para IVA o retenciones).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'EconomicActivityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'EconomicActivityId';

GO
CREATE NONCLUSTERED INDEX [IX_ServiceOrderDetail_ServiceOrderId]
    ON [Billing].[ServiceOrderDetail]([ServiceOrderId] ASC);
