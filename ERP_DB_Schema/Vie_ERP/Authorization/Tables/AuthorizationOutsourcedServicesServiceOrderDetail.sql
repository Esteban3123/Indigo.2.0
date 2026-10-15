CREATE TABLE [Authorization].[AuthorizationOutsourcedServicesServiceOrderDetail] (
    [Id]                                     INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AuthorizationOutsourcedServicesId]      INT             NOT NULL,
    [CareGroupId]                            INT             NOT NULL,
    [HealthAdministratorId]                  INT             NULL,
    [ThirdPartyId]                           INT             NULL,
    [ServiceType]                            TINYINT         CONSTRAINT [DF_AuthorizationOutsourcedServicesServiceOrderDetail_ServiceType] DEFAULT ((3)) NOT NULL,
    [RecordType]                             TINYINT         NOT NULL,
    [CUPSEntityId]                           INT             NULL,
    [IPSServiceId]                           INT             NULL,
    [HospitalStayId]                         INT             NULL,
    [HospitalStayDetailId]                   INT             NULL,
    [ControlExternalConsultation]            TINYINT         NULL,
    [ControlExternalConsultationCode]        NUMERIC (18)    NULL,
    [CUPSAssociateService]                   BIT             NOT NULL,
    [CodeAssociateService]                   VARCHAR (50)    NULL,
    [IsPackage]                              BIT             CONSTRAINT [DF_AuthorizationOutsourcedServicesServiceOrderDetail_IsPackage] DEFAULT ((0)) NOT NULL,
    [Packaging]                              BIT             CONSTRAINT [DF_AuthorizationOutsourcedServicesServiceOrderDetail_Packaging] DEFAULT ((0)) NOT NULL,
    [PackageServiceOrderDetailId]            INT             NULL,
    [LiquidationType]                        TINYINT         CONSTRAINT [DF_AuthorizationOutsourcedServicesServiceOrderDetail_LiquidationType] DEFAULT ((1)) NOT NULL,
    [Presentation]                           TINYINT         NULL,
    [ProductId]                              INT             NULL,
    [InvoicedQuantity]                       INT             NOT NULL,
    [SupplyQuantity]                         INT             NOT NULL,
    [DevolutionQuantity]                     INT             NOT NULL,
    [RateManualSalePrice]                    NUMERIC (18)    NOT NULL,
    [CostValue]                              NUMERIC (18, 2) NOT NULL,
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
    [SubTotalSalesPrice]                     NUMERIC (18, 2) NOT NULL,
    [ThirdPartyDiscount]                     NUMERIC (18)    NOT NULL,
    [ThirdPartyDiscountPercentage]           NUMERIC (5, 2)  NOT NULL,
    [TotalSalesPrice]                        NUMERIC (18, 2) NOT NULL,
    [GrandTotalSalesPrice]                   NUMERIC (18)    CONSTRAINT [DF_AuthorizationOutsourcedServicesServiceOrderDetail_GrandTotalSalesPrice] DEFAULT ((0)) NOT NULL,
    [SurchargeApply]                         BIT             NOT NULL,
    [SurgicalInterventionType]               TINYINT         NULL,
    [SurgeryNumber]                          TINYINT         CONSTRAINT [DF_AuthorizationOutsourcedServicesServiceOrderDetail_SurgeryNumber] DEFAULT ((0)) NOT NULL,
    [IsFirstEvent]                           BIT             CONSTRAINT [DF_AuthorizationOutsourcedServicesServiceOrderDetail_IsFirstEvent] DEFAULT ((0)) NOT NULL,
    [IsAnnulled]                             BIT             CONSTRAINT [DF_AuthorizationOutsourcedServicesServiceOrderDetail_IsAnnulled] DEFAULT ((0)) NOT NULL,
    [IsDelete]                               BIT             CONSTRAINT [DF_AuthorizationOutsourcedServicesServiceOrderDetail_IsDelete] DEFAULT ((0)) NOT NULL,
    [IncomeMainAccountId]                    INT             NOT NULL,
    [ApplyRIAS]                              BIT             NULL,
    [RIASCupsId]                             INT             NULL,
    [CUPSEntityContractDescriptionId]        INT             NULL,
    CONSTRAINT [PK_AuthorizationOutsourcedServicesServiceOrderDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AuthorizationOutsourcedServicesServiceOrderDetail_AuthorizationOutsourcedServices] FOREIGN KEY ([AuthorizationOutsourcedServicesId]) REFERENCES [Authorization].[AuthorizationOutsourcedServices] ([Id]),
    CONSTRAINT [FK_AuthorizationOutsourcedServicesServiceOrderDetail_BillingConcept] FOREIGN KEY ([BillingConceptId]) REFERENCES [Billing].[BillingConcept] ([Id]),
    CONSTRAINT [FK_AuthorizationOutsourcedServicesServiceOrderDetail_CareGroup] FOREIGN KEY ([CareGroupId]) REFERENCES [Contract].[CareGroup] ([Id]),
    CONSTRAINT [FK_AuthorizationOutsourcedServicesServiceOrderDetail_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_AuthorizationOutsourcedServicesServiceOrderDetail_CUPSEntity] FOREIGN KEY ([CUPSEntityId]) REFERENCES [Contract].[CUPSEntity] ([Id]),
    CONSTRAINT [FK_AuthorizationOutsourcedServicesServiceOrderDetail_CUPSEntityContractDescriptions] FOREIGN KEY ([CUPSEntityContractDescriptionId]) REFERENCES [Contract].[CUPSEntityContractDescriptions] ([Id]),
    CONSTRAINT [FK_AuthorizationOutsourcedServicesServiceOrderDetail_DefinitionRateDetail] FOREIGN KEY ([DefinitionRateDetailId]) REFERENCES [Contract].[DefinitionRateDetail] ([Id]),
    CONSTRAINT [FK_AuthorizationOutsourcedServicesServiceOrderDetail_DefinitionRateDetailCondition] FOREIGN KEY ([DefinitionRateDetailConditionId]) REFERENCES [Contract].[DefinitionRateDetailCondition] ([Id]),
    CONSTRAINT [FK_AuthorizationOutsourcedServicesServiceOrderDetail_FunctionalUnit] FOREIGN KEY ([PerformsFunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_AuthorizationOutsourcedServicesServiceOrderDetail_HealthAdministrator] FOREIGN KEY ([HealthAdministratorId]) REFERENCES [Contract].[HealthAdministrator] ([Id]),
    CONSTRAINT [FK_AuthorizationOutsourcedServicesServiceOrderDetail_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_AuthorizationOutsourcedServicesServiceOrderDetail_IPSService] FOREIGN KEY ([IPSServiceId]) REFERENCES [Contract].[IPSService] ([Id]),
    CONSTRAINT [FK_AuthorizationOutsourcedServicesServiceOrderDetail_MainAccounts] FOREIGN KEY ([IncomeMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_AuthorizationOutsourcedServicesServiceOrderDetail_RateManual] FOREIGN KEY ([RateManualId]) REFERENCES [Contract].[RateManual] ([Id]),
    CONSTRAINT [FK_AuthorizationOutsourcedServicesServiceOrderDetail_RateManualDetail] FOREIGN KEY ([RateManualDetailId]) REFERENCES [Contract].[RateManualDetail] ([Id]),
    CONSTRAINT [FK_AuthorizationOutsourcedServicesServiceOrderDetail_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_AuthorizationOutsourcedServicesServiceOrderDetail_ThirdParty1] FOREIGN KEY ([PerformsHealthProfessionalThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Authorization].[AuthorizationOutsourcedServicesServiceOrderDetail] NOCHECK CONSTRAINT [FK_AuthorizationOutsourcedServicesServiceOrderDetail_AuthorizationOutsourcedServices];


GO
ALTER TABLE [Authorization].[AuthorizationOutsourcedServicesServiceOrderDetail] NOCHECK CONSTRAINT [FK_AuthorizationOutsourcedServicesServiceOrderDetail_BillingConcept];


GO
ALTER TABLE [Authorization].[AuthorizationOutsourcedServicesServiceOrderDetail] NOCHECK CONSTRAINT [FK_AuthorizationOutsourcedServicesServiceOrderDetail_CareGroup];


GO
ALTER TABLE [Authorization].[AuthorizationOutsourcedServicesServiceOrderDetail] NOCHECK CONSTRAINT [FK_AuthorizationOutsourcedServicesServiceOrderDetail_CostCenter];


GO
ALTER TABLE [Authorization].[AuthorizationOutsourcedServicesServiceOrderDetail] NOCHECK CONSTRAINT [FK_AuthorizationOutsourcedServicesServiceOrderDetail_CUPSEntity];


GO
ALTER TABLE [Authorization].[AuthorizationOutsourcedServicesServiceOrderDetail] NOCHECK CONSTRAINT [FK_AuthorizationOutsourcedServicesServiceOrderDetail_CUPSEntityContractDescriptions];


GO
ALTER TABLE [Authorization].[AuthorizationOutsourcedServicesServiceOrderDetail] NOCHECK CONSTRAINT [FK_AuthorizationOutsourcedServicesServiceOrderDetail_DefinitionRateDetail];


GO
ALTER TABLE [Authorization].[AuthorizationOutsourcedServicesServiceOrderDetail] NOCHECK CONSTRAINT [FK_AuthorizationOutsourcedServicesServiceOrderDetail_DefinitionRateDetailCondition];


GO
ALTER TABLE [Authorization].[AuthorizationOutsourcedServicesServiceOrderDetail] NOCHECK CONSTRAINT [FK_AuthorizationOutsourcedServicesServiceOrderDetail_FunctionalUnit];


GO
ALTER TABLE [Authorization].[AuthorizationOutsourcedServicesServiceOrderDetail] NOCHECK CONSTRAINT [FK_AuthorizationOutsourcedServicesServiceOrderDetail_HealthAdministrator];


GO
ALTER TABLE [Authorization].[AuthorizationOutsourcedServicesServiceOrderDetail] NOCHECK CONSTRAINT [FK_AuthorizationOutsourcedServicesServiceOrderDetail_InventoryProduct];


GO
ALTER TABLE [Authorization].[AuthorizationOutsourcedServicesServiceOrderDetail] NOCHECK CONSTRAINT [FK_AuthorizationOutsourcedServicesServiceOrderDetail_IPSService];


GO
ALTER TABLE [Authorization].[AuthorizationOutsourcedServicesServiceOrderDetail] NOCHECK CONSTRAINT [FK_AuthorizationOutsourcedServicesServiceOrderDetail_MainAccounts];


GO
ALTER TABLE [Authorization].[AuthorizationOutsourcedServicesServiceOrderDetail] NOCHECK CONSTRAINT [FK_AuthorizationOutsourcedServicesServiceOrderDetail_RateManual];


GO
ALTER TABLE [Authorization].[AuthorizationOutsourcedServicesServiceOrderDetail] NOCHECK CONSTRAINT [FK_AuthorizationOutsourcedServicesServiceOrderDetail_RateManualDetail];


GO
ALTER TABLE [Authorization].[AuthorizationOutsourcedServicesServiceOrderDetail] NOCHECK CONSTRAINT [FK_AuthorizationOutsourcedServicesServiceOrderDetail_ThirdParty];


GO
ALTER TABLE [Authorization].[AuthorizationOutsourcedServicesServiceOrderDetail] NOCHECK CONSTRAINT [FK_AuthorizationOutsourcedServicesServiceOrderDetail_ThirdParty1];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la descripción del módulo de contratos', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CUPSEntityContractDescriptionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del RIAS asociado al cups en Crystal, este campo se muestra siempre y cuando el grupo de atención aplique a RIAS y además el servicio seleccionado también aplique a RIAS', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RIASCupsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite saber si aplica a RIAS, este campo se muestra siempre y cuando el grupo de atención aplique a RIAS y además el servicio seleccionado también aplique a RIAS', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ApplyRIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la cuenta de ingresos ya sea de la entidad o de particular', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IncomeMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta es una eliminacion logica la cual solo aplica para los homologos que ya hayan sido anulados es decir que tengan el campo IsAnnulled en true', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IsDelete';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo solo es marcado cuando es un homologo y fue factura y posteriormente anulado, esto con el fin de no afectar el modulo de honorarios medicos', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IsAnnulled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si es el primer evento de la cirujia (SurgeryNumber)', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IsFirstEvent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Cirugia que sucede en el mismo Acto Quirurgico', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SurgeryNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de intervencion quirurgica  1 - Basico  2 - Bilateral -- SOAT  3 - Bilateral Multiple -- SOAT  4 - MIVIE (Multiple Igual Via Igual Especialista) -- ISS  5 - MDVIE (Multiple Diferente Via Igual Especialista) -- ISS  6 - MIVDE (Multiple Igual Via Diferente Especialista) -- ISS  7 - MDVDE (Multiple Diferente Via Diferente Especialista) -- ISS  8 - PolitraumaIV (Politrauma Igual Via) -- ISS  9 - PolitraumaDV (Politrauma Diferente Via) -- ISS  10- No Cruento  ', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SurgicalInterventionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si se le aplico recargo al calculo del precio de venta', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SurchargeApply';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Corresponde al valor total del item  TotalSalesPrice * InvoiceQuantity', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'GrandTotalSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Corresponde al valor unitario total cobrado por el Producto / Servicio a la Entidad EAPB = Subtotalsalesprice - ThirpartyDiscount    ', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'TotalSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje de descuento aplicado al tercero responsable de la cuenta', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyDiscountPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descuento realizado al tercero responsable de la cuenta', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Corresponde al valor unitario calculado por el contrato o digitado por el usuario si este tiene permiso del Producto / Servicio  ', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SubTotalSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el detalle de la condicion de la tarifa    Solo para servicios, estos campos se utilizan para cuando se quiere calcular el valor con recargo entonces no se vuelve a liquidar todo ya que con estos campos vamos directamente', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'DefinitionRateDetailConditionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id del detalle de la tarifa    Solo aplica para Servicios', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'DefinitionRateDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el id del detalle del manual de tarifas de donde se tomo el valor.  Si el tipo de liquidacion en el Grupo de Atencion - Tarifas es Fijo este campo es null', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RateManualDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Tipo del manual tarifario con que se liquido el items  1 - ISS 2001  2 - ISS 2004  3 - SOAT      Solo se llena si el detalle es de tipo Servicios', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RateManualType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del manual de tarifas que tenga parametrizado en las tarifas o grupo de atencion, esto lo hacemos con el fin de que cuando tengan algun tipo de evento (Bilateran, MIVIE, ETC) se pueda consultar una forma facil el porcentaje a eventos', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RateManualId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje de cobro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RecoveryRatio';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id del servicio con el que se va a calcular el  porcentaje del valor a cobrar, o en el que se va incluir al 100%', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IncludeServiceOrderDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de liquidacion   1. Por manual de tarifas (Defecto)  2. % de otro servicio cargado  3. 100% incluido dentro de otro servicio (No se cobra nada)  4. % del mismo servicio', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SettlementType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Centro de Costo', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del concepto de facturación.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'BillingConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del tercero profesional de la salud que realiza el servicio', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la especialidad el profesional que realiza ', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PerformsProfessionalSpecialty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Codigo del Profesional de la Salud que Realiza el Servicio y/o Administra el Producto.   Estos datos se sacan de la tabla de Crystal INPROFSAL  Para Qx Se especifica el codigo del profesional cirujano', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad funcional donde se encontraba el paciente', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PerformsFunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Autorizacion', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'AuthorizationNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en que se presento el servicio', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ServiceDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor del costo', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CostValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del Venta del servicio y/o producto establecido en el manual de tarifas de contratos o de inventarios', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RateManualSalePrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Acumula la Cantidad Devuelta de las Dispensaciones Farmaceuticas (Solo Productos).    ', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'DevolutionQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Entregada (Solo Productos)  Productos: Almacena la cantidad entregada en la Dispensacion Farmaceutica (Pharmaceutical Dispensing)', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SupplyQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Facturada (Producto / Servicio)  Productos: Almacena la diferencia entre SupplyAmount y DevolutionAmount', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'InvoicedQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del producto, este se llena solo si el tipo de servicio es Producto', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Presentacion del producto  1 - No quirurgico  2 - Quirurgico  3 - Paquete', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'Presentation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de liquidacion  1 - Tipo de unidad  - quemados  2 - Especialidad  3 - Unidad Funcional  4 - Fija  5 - Estandar  6 - Calculado o Formula', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'LiquidationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id del detalle de la orden de servicio donde se empaqueto el servicio', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PackageServiceOrderDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica que el Item se encuentra incluido dentro de un paquete', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'Packaging';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el Item es un paquete, el cual esta incluyendo otros servicio', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IsPackage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el numero de identificacion unica para el proceso de homologacion. Todos los registros del mismo codigo corresponden a los homologos adicionales seleccionados por el usuario.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CodeAssociateService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el registro corresponde a un registro adicionado automaticamente por estar relacionado en la homologacion de servicios', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CUPSAssociateService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo especifica el Id del codigo registro generado en las tablas de Crystal ya que la tabla depende de la Clase de registro  8 - Consulta Externa --- Tabla ADCONCOEX   1 - Laboratorios --- Tabla ADMORDLAB   2 - Patologias --- Tabla ADMORDPAT   3 - Imagenes --- Tabla ADMORDIMA     Nota: Este campo solo se llena si la orden de servicio fue generada desde control de consulta externa o ambulatoria', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ControlExternalConsultationCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo especifica el tipo o la clase del servicio de control de consulta externa  8 - Consulta Externa  1 - Laboratorios  2 - Patologias  3 - Imagenes    Nota: Este campo solo se llena si la orden de servicio fue generada desde control de consulta externa o ambulatoria', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ControlExternalConsultation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id del detalle del registro de estancia (CHREGESTADET), este solo se llena cuando la orden de servicio es un registro de estancia', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'HospitalStayDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro de la estancia (CHREGESTA), Este campo solo se llena si es un registro de estancia', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'HospitalStayId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del servicio IPS el cual es homologado a través del CUPS, Solo se llenan si el tipo de registro es Servicio', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IPSServiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la entidad de CUPS, solo se llenan si el tipo de registro es Servicio', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Se especifica el tipo de servicio de detalle de la orden  1 - Servicios  2 - Medicamentos', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RecordType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de servicio, es decir como se cargo el item  1 - Soat  2 - ISS  3 - CUPS', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ServiceType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tercero de la entidad', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tercero con quien se realiza el contrato', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del grupo de atencion', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CareGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de la cotización', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'AuthorizationOutsourcedServicesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'Id';

