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
ALTER TABLE [Authorization].[AuthorizationOutsourcedServicesServiceOrderDetail] NOCHECK CONSTRAINT [FK_AuthorizationOutsourcedServicesServiceOrderDetail_AuthorizationOutsourcedServices];


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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la descripción contractual de la entidad CUPS registrada en el módulo de contratos; vinculado a condiciones de servicio y tarifa.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CUPSEntityContractDescriptionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la descripción del módulo de contratos', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CUPSEntityContractDescriptionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CUPSEntityContractDescriptionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del CUPS asociado al Régimen de Ingresos y Aportes Solidarios (RIAS) en Crystal; se visualiza solo si el grupo de atención y el servicio aplican a RIAS.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RIASCupsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del RIAS asociado al cups en Crystal, este campo se muestra siempre y cuando el grupo de atención aplique a RIAS y además el servicio seleccionado también aplique a RIAS', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RIASCupsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RIASCupsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera lógica que indica si el servicio aplica a Régimen de Ingresos y Aportes Solidarios (RIAS); se muestra solo si grupo de atención y servicio cumplen requisito RIAS.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ApplyRIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite saber si aplica a RIAS, este campo se muestra siempre y cuando el grupo de atención aplique a RIAS y además el servicio seleccionado también aplique a RIAS', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ApplyRIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ApplyRIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta contable de ingresos, ya sea de la entidad aseguradora (EAPB) o del paciente particular; usado para registros contables.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IncomeMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cuenta de ingresos ya sea de la entidad o de particular', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IncomeMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IncomeMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Eliminación lógica (booleano); aplica solo para registros homologados que fueron anulados (IsAnnulled=true), sin afectar integridad de datos históricos.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IsDelete';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Esta es una eliminacion logica la cual solo aplica para los homologos que ya hayan sido anulados es decir que tengan el campo IsAnnulled en true', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IsDelete';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IsDelete';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera que indica si el registro es un homólogo que fue facturado y posteriormente anulado; preserva integridad del módulo de honorarios médicos.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IsAnnulled';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Este campo solo es marcado cuando es un homologo y fue factura y posteriormente anulado, esto con el fin de no afectar el modulo de honorarios medicos', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IsAnnulled';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IsAnnulled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera que especifica si es el primer evento quirúrgico dentro del mismo acto quirúrgico (relacionado con SurgeryNumber).', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IsFirstEvent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si es el primer evento de la cirujia (SurgeryNumber)', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IsFirstEvent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IsFirstEvent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial de cirugía que ocurre en el mismo acto quirúrgico; permite identificar múltiples intervenciones simultáneas (tipo TINYINT, rango 0-255).', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SurgeryNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Cirugia que sucede en el mismo Acto Quirurgico', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SurgeryNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SurgeryNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del tipo de intervención quirúrgica (TINYINT): 1-Básico, 2-Bilateral SOAT, 3-Bilateral Múltiple SOAT, 4-MIVIE ISS, 5-MDVIE ISS, 6-MIVDE ISS, 7-MDVDE ISS, 8-Politrauma IV ISS, 9-Politrauma DV ISS, 10-No Cruento.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SurgicalInterventionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de intervencion quirurgica  1 - Basico  2 - Bilateral -- SOAT  3 - Bilateral Multiple -- SOAT  4 - MIVIE (Multiple Igual Via Igual Especialista) -- ISS  5 - MDVIE (Multiple Diferente Via Igual Especialista) -- ISS  6 - MIVDE (Multiple Igual Via Diferente Especialista) -- ISS  7 - MDVDE (Multiple Diferente Via Diferente Especialista) -- ISS  8 - PolitraumaIV (Politrauma Igual Via) -- ISS  9 - PolitraumaDV (Politrauma Diferente Via) -- ISS  10- No Cruento  ', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SurgicalInterventionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SurgicalInterventionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera booleana que indica si se aplicó recargo al cálculo del precio de venta unitario del producto o servicio.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SurchargeApply';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si se le aplico recargo al calculo del precio de venta', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SurchargeApply';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SurchargeApply';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total de línea = TotalSalesPrice × InvoicedQuantity; monto final facturado a la EAPB o particular (NUMERIC 18,2).', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'GrandTotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Corresponde al valor total del item  TotalSalesPrice * InvoiceQuantity', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'GrandTotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'GrandTotalSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Precio unitario total cobrado por producto/servicio a la EAPB = SubTotalSalesPrice - ThirdPartyDiscount; antes de multiplicar por cantidad (NUMERIC 18,2).', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'TotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Corresponde al valor unitario total cobrado por el Producto / Servicio a la Entidad EAPB = Subtotalsalesprice - ThirpartyDiscount    ', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'TotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'TotalSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de descuento aplicado al tercero responsable de la cuenta (NUMERIC 5,2); rango típico 0-100%.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyDiscountPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de descuento aplicado al tercero responsable de la cuenta', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyDiscountPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyDiscountPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto en pesos de descuento concedido al tercero responsable; reduce el subtotal de venta (NUMERIC 18).', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descuento realizado al tercero responsable de la cuenta', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Precio unitario base calculado por contrato o ingresado manualmente con permiso; antes de descuentos (NUMERIC 18,2).', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SubTotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Corresponde al valor unitario calculado por el contrato o digitado por el usuario si este tiene permiso del Producto / Servicio  ', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SubTotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SubTotalSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la condición de tarifa para servicios; usado en cálculos con recargo sin reliquidar todo el servicio (FK a Contract.DefinitionRateDetailCondition).', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'DefinitionRateDetailConditionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el detalle de la condicion de la tarifa    Solo para servicios, estos campos se utilizan para cuando se quiere calcular el valor con recargo entonces no se vuelve a liquidar todo ya que con estos campos vamos directamente', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'DefinitionRateDetailConditionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'DefinitionRateDetailConditionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle de tarifa contractual para servicios; referencia la estructura de precios por condición (FK a Contract.DefinitionRateDetail).', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'DefinitionRateDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id del detalle de la tarifa    Solo aplica para Servicios', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'DefinitionRateDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'DefinitionRateDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del registro en el detalle del manual tarifario del que se extrajo el valor; NULL si liquidación es fija (FK a manual de tarifas).', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RateManualDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id del detalle del manual de tarifas de donde se tomo el valor.  Si el tipo de liquidacion en el Grupo de Atencion - Tarifas es Fijo este campo es null', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RateManualDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RateManualDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de manual tarifario usado en liquidación (TINYINT): 1-ISS 2001, 2-ISS 2004, 3-SOAT; solo para servicios.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RateManualType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Tipo del manual tarifario con que se liquido el items  1 - ISS 2001  2 - ISS 2004  3 - SOAT      Solo se llena si el detalle es de tipo Servicios', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RateManualType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RateManualType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del manual tarifario parametrizado en contrato/grupo de atención; facilita consulta de porcentajes para eventos bilaterales, MIVIE, etc. (FK a manual de tarifas).', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RateManualId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del manual de tarifas que tenga parametrizado en las tarifas o grupo de atencion, esto lo hacemos con el fin de que cuando tengan algun tipo de evento (Bilateran, MIVIE, ETC) se pueda consultar una forma facil el porcentaje a eventos', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RateManualId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RateManualId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de cobro aplicado al servicio; usado en liquidaciones parciales (NUMERIC 5,2); rango típico 0-100%.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RecoveryRatio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de cobro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RecoveryRatio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RecoveryRatio';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del servicio inclusor cuyo porcentaje/valor se utiliza para calcular el cobro, o dentro del cual se incluye al 100% sin cargo adicional (FK).', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IncludeServiceOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id del servicio con el que se va a calcular el  porcentaje del valor a cobrar, o en el que se va incluir al 100%', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IncludeServiceOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IncludeServiceOrderDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de liquidación (TINYINT): 1-Manual tarifario (defecto), 2-Porcentaje de otro servicio, 3-Incluido 100% en otro servicio (no se cobra), 4-Porcentaje del mismo servicio.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SettlementType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de liquidacion   1. Por manual de tarifas (Defecto)  2. % de otro servicio cargado  3. 100% incluido dentro de otro servicio (No se cobra nada)  4. % del mismo servicio', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SettlementType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SettlementType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro de costo (unidad funcional/departamento) donde se prestó el servicio; usado para análisis financiero (FK a Payroll.CostCenter).', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Centro de Costo', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de facturación; tipifica la naturaleza del cargo en factura (consulta, medicamento, procedimiento, etc.) (FK a Billing.BillingConcept).', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'BillingConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de facturación.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'BillingConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'BillingConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tercero (profesional de salud) que ejecuta el servicio o administra el producto; vinculado a proveedor de servicios.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del tercero profesional de la salud que realiza el servicio', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de especialidad médica del profesional que realiza el servicio (CHAR 3); ej: MED (Medicina General), CIR (Cirugía), etc.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PerformsProfessionalSpecialty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la especialidad el profesional que realiza ', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PerformsProfessionalSpecialty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PerformsProfessionalSpecialty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del profesional de salud que realiza/administra el servicio (CHAR 20); para cirugía es el código del cirujano; extraído de tabla INPROFSAL.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Codigo del Profesional de la Salud que Realiza el Servicio y/o Administra el Producto.   Estos datos se sacan de la tabla de Crystal INPROFSAL  Para Qx Se especifica el codigo del profesional cirujano', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad funcional donde se encontraba el paciente al momento de la atención; vinculado a ubicación de prestación (FK a Payroll.FunctionalUnit).', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PerformsFunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad funcional donde se encontraba el paciente', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PerformsFunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PerformsFunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de autorización/radicación (VARCHAR 20); identificador único ante aseguradores para justificar prestación de servicio.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'AuthorizationNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Autorizacion', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'AuthorizationNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'AuthorizationNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se prestó el servicio o producto (DATETIME); base para facturación y auditoría de vigencia de autorización.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ServiceDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en que se presento el servicio', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ServiceDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ServiceDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del costo directo del producto/servicio (NUMERIC 18,2); usado para márgenes y análisis de rentabilidad.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CostValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor del costo', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CostValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CostValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Precio de venta unitario establecido en manual tarifario o inventario de contratos (NUMERIC 18); base antes de descuentos.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RateManualSalePrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del Venta del servicio y/o producto establecido en el manual de tarifas de contratos o de inventarios', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RateManualSalePrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RateManualSalePrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad devuelta acumulada en dispensaciones farmacéuticas (INT); solo para productos; resta del SupplyQuantity.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'DevolutionQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Acumula la Cantidad Devuelta de las Dispensaciones Farmaceuticas (Solo Productos).    ', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'DevolutionQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'DevolutionQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad entregada/dispensada de producto (INT); solo para medicamentos; registrada en dispensa farmacéutica.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SupplyQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Entregada (Solo Productos)  Productos: Almacena la cantidad entregada en la Dispensacion Farmaceutica (Pharmaceutical Dispensing)', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SupplyQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SupplyQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad facturada de producto/servicio (INT); para productos = SupplyQuantity - DevolutionQuantity.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'InvoicedQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Facturada (Producto / Servicio)  Productos: Almacena la diferencia entre SupplyAmount y DevolutionAmount', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'InvoicedQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'InvoicedQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del producto en inventario; se completa solo si ServiceType es producto/medicamento (FK a inventario).', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del producto, este se llena solo si el tipo de servicio es Producto', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presentación del producto (TINYINT): 1-No quirúrgico, 2-Quirúrgico, 3-Paquete; clasifica según uso en atención.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'Presentation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Presentacion del producto  1 - No quirurgico  2 - Quirurgico  3 - Paquete', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'Presentation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'Presentation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de liquidación de tarifa (TINYINT): 1-Por unidad (quemados), 2-Especialidad, 3-Unidad Funcional, 4-Fija, 5-Estándar, 6-Calculado/Fórmula.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'LiquidationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de liquidacion  1 - Tipo de unidad  - quemados  2 - Especialidad  3 - Unidad Funcional  4 - Fija  5 - Estandar  6 - Calculado o Formula', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'LiquidationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'LiquidationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle de orden de servicio principal donde se empaquetó este servicio; vincula servicios incluidos en paquete (FK).', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PackageServiceOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id del detalle de la orden de servicio donde se empaqueto el servicio', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PackageServiceOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PackageServiceOrderDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera booleana; indica si el item está incluido dentro de un paquete de servicios.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'Packaging';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica que el Item se encuentra incluido dentro de un paquete', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'Packaging';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'Packaging';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera booleana; indica si el item es un paquete que incluye otros servicios (agrupa múltiples ítems).', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IsPackage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el Item es un paquete, el cual esta incluyendo otros servicio', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IsPackage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IsPackage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de identificación para proceso de homologación (VARCHAR 50); todos los registros con mismo código son homólogos adicionales seleccionados.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CodeAssociateService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el numero de identificacion unica para el proceso de homologacion. Todos los registros del mismo codigo corresponden a los homologos adicionales seleccionados por el usuario.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CodeAssociateService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CodeAssociateService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera booleana; indica si el registro fue adicionado automáticamente por relación en homologación de servicios CUPS.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CUPSAssociateService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el registro corresponde a un registro adicionado automaticamente por estar relacionado en la homologacion de servicios', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CUPSAssociateService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CUPSAssociateService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del registro generado en Crystal según clase (NUMERIC 18): 8-ADCONCOEX (Consulta Externa), 1-ADMORDLAB (Laboratorio), 2-ADMORDPAT (Patología), 3-ADMORDIMA (Imagen); solo si orden originó en control ambulatorio.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ControlExternalConsultationCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Este campo especifica el Id del codigo registro generado en las tablas de Crystal ya que la tabla depende de la Clase de registro  8 - Consulta Externa --- Tabla ADCONCOEX   1 - Laboratorios --- Tabla ADMORDLAB   2 - Patologias --- Tabla ADMORDPAT   3 - Imagenes --- Tabla ADMORDIMA     Nota: Este campo solo se llena si la orden de servicio fue generada desde control de consulta externa o ambulatoria', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ControlExternalConsultationCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ControlExternalConsultationCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clase de servicio de control ambulatorio (TINYINT): 8-Consulta Externa, 1-Laboratorio, 2-Patología, 3-Imagen; solo si origen es consulta externa.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ControlExternalConsultation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Este campo especifica el tipo o la clase del servicio de control de consulta externa  8 - Consulta Externa  1 - Laboratorios  2 - Patologias  3 - Imagenes    Nota: Este campo solo se llena si la orden de servicio fue generada desde control de consulta externa o ambulatoria', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ControlExternalConsultation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ControlExternalConsultation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle de registro de estancia (CHREGESTADET); se completa solo si la orden es un registro de internación/estancia.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'HospitalStayDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id del detalle del registro de estancia (CHREGESTADET), este solo se llena cuando la orden de servicio es un registro de estancia', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'HospitalStayDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'HospitalStayDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del registro de estancia hospitalaria (CHREGESTA); se completa solo si la orden corresponde a internación (FK a HospitalStay).', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'HospitalStayId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro de la estancia (CHREGESTA), Este campo solo se llena si es un registro de estancia', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'HospitalStayId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'HospitalStayId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del servicio IPS que fue homologado a través del código CUPS; se llena solo si tipo de registro es Servicio (FK).', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IPSServiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del servicio IPS el cual es homologado a través del CUPS, Solo se llenan si el tipo de registro es Servicio', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IPSServiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IPSServiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la entidad o prestador de CUPS; se llena solo si tipo de registro es Servicio (FK a Contract.CUPSEntity).', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la entidad de CUPS, solo se llenan si el tipo de registro es Servicio', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de servicio/producto (TINYINT): 1-Servicios (consulta, procedimiento, etc.), 2-Medicamentos/Productos farmacéuticos.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RecordType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se especifica el tipo de servicio de detalle de la orden  1 - Servicios  2 - Medicamentos', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RecordType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RecordType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de cobertura/sistema (TINYINT): 1-SOAT, 2-ISS, 3-CUPS; especifica bajo qué régimen se cargó el item.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ServiceType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de servicio, es decir como se cargo el item  1 - Soat  2 - ISS  3 - CUPS', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ServiceType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ServiceType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tercero/proveedor de la entidad responsable del servicio; vinculado a contratante (FK a Contract.ThirdParty).', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero de la entidad', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tercero asegurador/EAPB con el que se realiza el contrato (FK a terceros); responsable del pago.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero con quien se realiza el contrato', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo de atención (plan de salud, línea de cobertura); define tarifas, deducibles y autorizaciones (FK a Contract.CareGroup).', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CareGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del grupo de atencion', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CareGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CareGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera/encabezado de la solicitud de servicios tercearizados; agrupa todos los detalles de una misma cotización (FK a AuthorizationOutsourcedServices).', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'AuthorizationOutsourcedServicesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de la cotización', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'AuthorizationOutsourcedServicesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'AuthorizationOutsourcedServicesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la línea de detalle de orden de servicio tercearizado (INT IDENTITY); clave primaria de la tabla.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los servicios incluidos en órdenes de servicio de autorizaciones de servicios tercerizados (outsourcing). Registra cada ítem facturado o suministrado —procedimientos CUPS, medicamentos, estancias hospitalarias— con sus cantidades, valores, tarifas, descuenttos y datos del profesional o unidad que los ejecuta, dentro del módulo de autorización y liquidación con terceros.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetail';
