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
ALTER TABLE [Billing].[ServiceOrderDetail] NOCHECK CONSTRAINT [FK_ServiceOrderDetail_BillingConcept];


GO
ALTER TABLE [Billing].[ServiceOrderDetail] NOCHECK CONSTRAINT [FK_ServiceOrderDetail_CareGroup1];


GO
ALTER TABLE [Billing].[ServiceOrderDetail] NOCHECK CONSTRAINT [FK_ServiceOrderDetail_ContractDescriptions];


GO
ALTER TABLE [Billing].[ServiceOrderDetail] NOCHECK CONSTRAINT [FK_ServiceOrderDetail_ContractPackage];


GO
ALTER TABLE [Billing].[ServiceOrderDetail] NOCHECK CONSTRAINT [FK_ServiceOrderDetail_CostCenter];


GO
ALTER TABLE [Billing].[ServiceOrderDetail] NOCHECK CONSTRAINT [FK_ServiceOrderDetail_CupsEntity];


GO
ALTER TABLE [Billing].[ServiceOrderDetail] NOCHECK CONSTRAINT [FK_ServiceOrderDetail_DefinitionRateDetail];


GO
ALTER TABLE [Billing].[ServiceOrderDetail] NOCHECK CONSTRAINT [FK_ServiceOrderDetail_DefinitionRateDetailCondition];


GO
ALTER TABLE [Billing].[ServiceOrderDetail] NOCHECK CONSTRAINT [FK_ServiceOrderDetail_FunctionalUnit1];


GO
ALTER TABLE [Billing].[ServiceOrderDetail] NOCHECK CONSTRAINT [FK_ServiceOrderDetail_GeneralLedgerIVA];


GO
ALTER TABLE [Billing].[ServiceOrderDetail] NOCHECK CONSTRAINT [FK_ServiceOrderDetail_HealthAdministrator];


GO
ALTER TABLE [Billing].[ServiceOrderDetail] NOCHECK CONSTRAINT [FK_ServiceOrderDetail_IncludeServiceOrderDetail];


GO
ALTER TABLE [Billing].[ServiceOrderDetail] NOCHECK CONSTRAINT [FK_ServiceOrderDetail_IPSService];


GO
ALTER TABLE [Billing].[ServiceOrderDetail] NOCHECK CONSTRAINT [FK_ServiceOrderDetail_MainAccounts];


GO
ALTER TABLE [Billing].[ServiceOrderDetail] NOCHECK CONSTRAINT [FK_ServiceOrderDetail_Product];


GO
ALTER TABLE [Billing].[ServiceOrderDetail] NOCHECK CONSTRAINT [FK_ServiceOrderDetail_QuotationServiceOrderDetail];


GO
ALTER TABLE [Billing].[ServiceOrderDetail] NOCHECK CONSTRAINT [FK_ServiceOrderDetail_RateManual];


GO
ALTER TABLE [Billing].[ServiceOrderDetail] NOCHECK CONSTRAINT [FK_ServiceOrderDetail_RateManualDetail];


GO
ALTER TABLE [Billing].[ServiceOrderDetail] NOCHECK CONSTRAINT [FK_ServiceOrderDetail_ServiceOrder];


GO
ALTER TABLE [Billing].[ServiceOrderDetail] NOCHECK CONSTRAINT [FK_ServiceOrderDetail_ServiceOrderDetail];


GO
ALTER TABLE [Billing].[ServiceOrderDetail] NOCHECK CONSTRAINT [FK_ServiceOrderDetail_ThirdParty];


GO
ALTER TABLE [Billing].[ServiceOrderDetail] NOCHECK CONSTRAINT [FK_ServiceOrderDetail_ThirdParty1];


GO
ALTER TABLE [Billing].[ServiceOrderDetail] NOCHECK CONSTRAINT [FK_ServiceOrderDetail_TraceabilityPaperworkEvents];


GO
CREATE NONCLUSTERED INDEX [IDX_ServiceOrderDetail_[ApplyRIAS]
    ON [Billing].[ServiceOrderDetail]([ApplyRIAS] ASC)
    INCLUDE([ServiceOrderId], [CUPSEntityId]);


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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del IVA parametrizado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IvaId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Tarifa:    1 - Item Producción  2 - Detalle Producción    Sólo se inserta cuando el item corresponde a un producto', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ProductLiquidationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece el costo final del detalle.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'FinalProductCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del cubirmiento contractual:  1. Confirmado  2. Contratado  3. Cotizado    Este campo se asigna en el Dashboard Cubrimiento Contractual Hospitalario', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ContractCoverageStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica como se va redondear los valores  1 -- Peso  10 -- Decena  100 -- Centena  1000 -- Unidad de Mil    Este valor se obtiene del manual de tarifa asociado, al momento de calcular el TotalSalesPrice', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RoundService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de intervencion quirurgica  1 - Basico  2 - Bilateral -- SOAT  3 - MIVIE (Multiple Igual Via Igual Especialista) -- ISS  4 - MDVIE (Multiple Diferente Via Igual Especialista) -- ISS  5 - MIVDE (Multiple Igual Via Diferente Especialista) -- ISS  6 - MDVDE (Multiple Diferente Via Diferente Especialista) -- ISS  7 - PolitraumaIV (Politrauma Igual Via) -- ISS  8 - PolitraumaDV (Politrauma Diferente Via) -- ISS  9- No Cruento  ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SurgicalInterventionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Corresponde al valor total del item  TotalSalesPrice * InvoiceQuantity', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'GrandTotalSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Corresponde al valor unitario total cobrado por el Producto / Servicio a la Entidad EAPB = Subtotalsalesprice - ThirpartyDiscount    ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'TotalSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Corresponde al valor unitario calculado por el contrato o digitado por el usuario si este tiene permiso del Producto / Servicio  ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SubTotalSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el detalle de la condicion de la tarifa    Solo para servicios, estos campos se utilizan para cuando se quiere calcular el valor con recargo entonces no se vuelve a liquidar todo ya que con estos campos vamos directamente', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'DefinitionRateDetailConditionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id del detalle de la tarifa    Solo aplica para Servicios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'DefinitionRateDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el id del detalle del manual de tarifas de donde se tomo el valor.  Si el tipo de liquidacion en el Grupo de Atencion - Tarifas es Fijo este campo es null', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RateManualDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Tipo del manual tarifario con que se liquido el items  1 - ISS 2001  2 - ISS 2004  3 - SOAT      Solo se llena si el detalle es de tipo Servicios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RateManualType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de liquidacion   1. Por manual de tarifas (Defecto)  2. % de otro servicio cargado  3. 100% incluido dentro de otro servicio (No se cobra nada)  4. % del mismo servicio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SettlementType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Codigo del Profesional de la Salud que Realiza el Servicio y/o Administra el Producto.   Estos datos se sacan de la tabla de Crystal INPROFSAL  Para Qx Se especifica el codigo del profesional cirujano', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Acumula la Cantidad Devuelta de las Dispensaciones Farmaceuticas (Solo Productos).    ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'DevolutionQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Entregada (Solo Productos)  Productos: Almacena la cantidad entregada en la Dispensacion Farmaceutica (Pharmaceutical Dispensing)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SupplyQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Facturada (Producto / Servicio)  Productos: Almacena la diferencia entre SupplyAmount y DevolutionAmount', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'InvoicedQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Presentacion del producto  1 - No quirurgico  2 - Quirurgico  3 - Paquete', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'Presentation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de liquidacion  1 - Tipo de unidad  - quemados  2 - Especialidad  3 - Unidad Funcional  4 - Fija  5 - Estandar  6 - Calculado o Formula', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'LiquidationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Este campo especifica el Id del codigo registro generado en las tablas de Crystal ya que la tabla depende de la Clase de registro  
1 - Laboratorios --- Tabla ADMORDLAB   
2 - Patologias --- Tabla ADMORDPAT  
3 - Imagenes --- Tabla ADMORDIMA 
Para el resto de codigo la relacion es con ADCONCOEX
    Nota: Este campo solo se llena si la orden de servicio fue generada desde control de consulta externa o ambulatoria', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ControlExternalConsultationCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Este campo especifica el tipo o la clase del servicio de control de consulta externa 
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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Se especifica el tipo de servicio de detalle de la orden  1 - Servicios  2 - Medicamentos', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RecordType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de servicio, es decir como se cargo el item  1 - Soat  2 - ISS  3 - CUPS', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ServiceType';

