CREATE TABLE [Billing].[QuotationServiceOrderDetail] (
    [Id]                                     INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [QuotationId]                            INT             NOT NULL,
    [CareGroupId]                            INT             NOT NULL,
    [HealthAdministratorId]                  INT             NOT NULL,
    [ThirdPartyId]                           INT             NULL,
    [ServiceType]                            TINYINT         CONSTRAINT [DF_QuotationServiceOrderDetail_ServiceType] DEFAULT ((3)) NOT NULL,
    [RecordType]                             TINYINT         NOT NULL,
    [CUPSEntityId]                           INT             NULL,
    [IPSServiceId]                           INT             NULL,
    [HospitalStayId]                         INT             NULL,
    [HospitalStayDetailId]                   INT             NULL,
    [ControlExternalConsultation]            TINYINT         NULL,
    [ControlExternalConsultationCode]        NUMERIC (18)    NULL,
    [CUPSAssociateService]                   BIT             NOT NULL,
    [CodeAssociateService]                   VARCHAR (50)    NULL,
    [IsPackage]                              BIT             CONSTRAINT [DF_QuotationServiceOrderDetail_IsPackage] DEFAULT ((0)) NOT NULL,
    [Packaging]                              BIT             CONSTRAINT [DF_QuotationServiceOrderDetail_Packaging] DEFAULT ((0)) NOT NULL,
    [PackageServiceOrderDetailId]            INT             NULL,
    [LiquidationType]                        TINYINT         CONSTRAINT [DF_QuotationServiceOrderDetail_LiquidationType] DEFAULT ((1)) NOT NULL,
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
    [GrandTotalSalesPrice]                   NUMERIC (18)    CONSTRAINT [DF_QuotationServiceOrderDetail_GrandTotalSalesPrice] DEFAULT ((0)) NOT NULL,
    [SurchargeApply]                         BIT             NOT NULL,
    [SurgicalInterventionType]               TINYINT         NULL,
    [SurgeryNumber]                          TINYINT         CONSTRAINT [DF_QuotationServiceOrderDetail_SurgeryNumber] DEFAULT ((0)) NOT NULL,
    [IsFirstEvent]                           BIT             CONSTRAINT [DF_QuotationServiceOrderDetail_IsFirstEvent] DEFAULT ((0)) NOT NULL,
    [IsAnnulled]                             BIT             CONSTRAINT [DF_QuotationServiceOrderDetail_IsAnnulled] DEFAULT ((0)) NOT NULL,
    [IsDelete]                               BIT             CONSTRAINT [DF_QuotationServiceOrderDetail_IsDelete] DEFAULT ((0)) NOT NULL,
    [IncomeMainAccountId]                    INT             NOT NULL,
    [ApplyRIAS]                              BIT             NULL,
    [RIASCupsId]                             INT             NULL,
    [CUPSEntityContractDescriptionId]        INT             NULL,
    [EconomicActivityId]                     INT             NULL,
    CONSTRAINT [PK_QuotationServiceOrderDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_QuotationServiceOrderDetail_BillingConcept] FOREIGN KEY ([BillingConceptId]) REFERENCES [Billing].[BillingConcept] ([Id]),
    CONSTRAINT [FK_QuotationServiceOrderDetail_CareGroup] FOREIGN KEY ([CareGroupId]) REFERENCES [Contract].[CareGroup] ([Id]),
    CONSTRAINT [FK_QuotationServiceOrderDetail_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_QuotationServiceOrderDetail_CUPSEntity] FOREIGN KEY ([CUPSEntityId]) REFERENCES [Contract].[CUPSEntity] ([Id]),
    CONSTRAINT [FK_QuotationServiceOrderDetail_CUPSEntityContractDescriptions] FOREIGN KEY ([CUPSEntityContractDescriptionId]) REFERENCES [Contract].[CUPSEntityContractDescriptions] ([Id]),
    CONSTRAINT [FK_QuotationServiceOrderDetail_DefinitionRateDetail] FOREIGN KEY ([DefinitionRateDetailId]) REFERENCES [Contract].[DefinitionRateDetail] ([Id]),
    CONSTRAINT [FK_QuotationServiceOrderDetail_DefinitionRateDetailCondition] FOREIGN KEY ([DefinitionRateDetailConditionId]) REFERENCES [Contract].[DefinitionRateDetailCondition] ([Id]),
    CONSTRAINT [FK_QuotationServiceOrderDetail_EconomicActivity] FOREIGN KEY ([EconomicActivityId]) REFERENCES [Common].[EconomicActivity] ([Id]),
    CONSTRAINT [FK_QuotationServiceOrderDetail_FunctionalUnit] FOREIGN KEY ([PerformsFunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_QuotationServiceOrderDetail_HealthAdministrator] FOREIGN KEY ([HealthAdministratorId]) REFERENCES [Contract].[HealthAdministrator] ([Id]),
    CONSTRAINT [FK_QuotationServiceOrderDetail_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_QuotationServiceOrderDetail_IPSService] FOREIGN KEY ([IPSServiceId]) REFERENCES [Contract].[IPSService] ([Id]),
    CONSTRAINT [FK_QuotationServiceOrderDetail_MainAccounts] FOREIGN KEY ([IncomeMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_QuotationServiceOrderDetail_Quotation] FOREIGN KEY ([QuotationId]) REFERENCES [Billing].[Quotation] ([Id]),
    CONSTRAINT [FK_QuotationServiceOrderDetail_RateManual] FOREIGN KEY ([RateManualId]) REFERENCES [Contract].[RateManual] ([Id]),
    CONSTRAINT [FK_QuotationServiceOrderDetail_RateManualDetail] FOREIGN KEY ([RateManualDetailId]) REFERENCES [Contract].[RateManualDetail] ([Id]),
    CONSTRAINT [FK_QuotationServiceOrderDetail_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_QuotationServiceOrderDetail_ThirdParty1] FOREIGN KEY ([PerformsHealthProfessionalThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Billing].[QuotationServiceOrderDetail] NOCHECK CONSTRAINT [FK_QuotationServiceOrderDetail_CUPSEntity];


GO
ALTER TABLE [Billing].[QuotationServiceOrderDetail] NOCHECK CONSTRAINT [FK_QuotationServiceOrderDetail_Quotation];




GO



GO



GO



GO
ALTER TABLE [Billing].[QuotationServiceOrderDetail] NOCHECK CONSTRAINT [FK_QuotationServiceOrderDetail_CUPSEntity];


GO



GO



GO



GO



GO



GO



GO



GO



GO
ALTER TABLE [Billing].[QuotationServiceOrderDetail] NOCHECK CONSTRAINT [FK_QuotationServiceOrderDetail_Quotation];


GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID (INT) de la descripción del módulo de contratos asociada al código CUPS; referencia a Contract.CUPSEntityContractDescriptions.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CUPSEntityContractDescriptionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la descripción del módulo de contratos', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CUPSEntityContractDescriptionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CUPSEntityContractDescriptionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID (INT) del RIAS (Registro de Información y Atención en Salud) asociado al CUPS en Crystal; se completa solo si el grupo de atención y servicio aplican a RIAS.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RIASCupsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del RIAS asociado al cups en Crystal, este campo se muestra siempre y cuando el grupo de atención aplique a RIAS y además el servicio seleccionado también aplique a RIAS', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RIASCupsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RIASCupsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de aplicabilidad a RIAS; muestra si el servicio/grupo cumple requisitos para reportar a RIAS (Registro de Información y Atención en Salud).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ApplyRIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite saber si aplica a RIAS, este campo se muestra siempre y cuando el grupo de atención aplique a RIAS y además el servicio seleccionado también aplique a RIAS', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ApplyRIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ApplyRIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID (INT) de cuenta de ingresos contables; especifica si el ingreso se registra a la entidad (EPS/EAPB) o a particular; FK a GeneralLedger.MainAccounts.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IncomeMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cuenta de ingresos ya sea de la entidad o de particular', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IncomeMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IncomeMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera de eliminación lógica (BIT, 0=activo); solo aplica a registros homologados previamente anulados (IsAnnulled=1) para mantener integridad en honorarios médicos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IsDelete';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Esta es una eliminacion logica la cual solo aplica para los homologos que ya hayan sido anulados es decir que tengan el campo IsAnnulled en true', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IsDelete';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IsDelete';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera de anulación (BIT, 0=vigente); marcado cuando un registro homologado fue facturado y posteriormente anulado, sin afectar módulo de honorarios profesionales.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IsAnnulled';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Este campo solo es marcado cuando es un homologo y fue factura y posteriormente anulado, esto con el fin de no afectar el modulo de honorarios medicos', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IsAnnulled';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IsAnnulled';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) del primer evento quirúrgico; especifica si es el evento inicial en una serie de cirugías (SurgeryNumber) en el mismo acto.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IsFirstEvent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si es el primer evento de la cirujia (SurgeryNumber)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IsFirstEvent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IsFirstEvent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de cirugía secuencial (TINYINT, 0-255) dentro del mismo acto quirúrgico; permite rastrear múltiples procedimientos realizados simultáneamente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SurgeryNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Cirugia que sucede en el mismo Acto Quirurgico', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SurgeryNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SurgeryNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación (TINYINT 1-10) del tipo de intervención quirúrgica; incluye Básico, Bilateral SOAT, Bilateral Múltiple, MIVIE, MDVIE, MIVDE, MDVDE, Politrauma IV/DV, No Cruentas.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SurgicalInterventionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de intervencion quirurgica  1 - Basico  2 - Bilateral -- SOAT  3 - Bilateral Multiple -- SOAT  4 - MIVIE (Multiple Igual Via Igual Especialista) -- ISS  5 - MDVIE (Multiple Diferente Via Igual Especialista) -- ISS  6 - MIVDE (Multiple Igual Via Diferente Especialista) -- ISS  7 - MDVDE (Multiple Diferente Via Diferente Especialista) -- ISS  8 - PolitraumaIV (Politrauma Igual Via) -- ISS  9 - PolitraumaDV (Politrauma Diferente Via) -- ISS  10- No Cruento  ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SurgicalInterventionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SurgicalInterventionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de aplicación de recargo; señala si se aplicó incremento al cálculo del precio unitario de venta.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SurchargeApply';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si se le aplico recargo al calculo del precio de venta', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SurchargeApply';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SurchargeApply';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total (NUMERIC 18,2) del ítem facturado = TotalSalesPrice × InvoicedQuantity; monto bruto sin descuentos adicionales.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'GrandTotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Corresponde al valor total del item  TotalSalesPrice * InvoiceQuantity', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'GrandTotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'GrandTotalSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Precio unitario total cobrado (NUMERIC 18,2) al tercero pagador = SubTotalSalesPrice - ThirdPartyDiscount; valor por unidad facturada.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'TotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Corresponde al valor unitario total cobrado por el Producto / Servicio a la Entidad EAPB = Subtotalsalesprice - ThirpartyDiscount    ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'TotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'TotalSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de descuento (NUMERIC 5,2, 0-100%) aplicado al tercero responsable (EPS/EAPB/asegurador) sobre el subtotal.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyDiscountPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de descuento aplicado al tercero responsable de la cuenta', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyDiscountPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyDiscountPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto de descuento (NUMERIC 18) concedido al tercero pagador; reduce el subtotal contractual.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descuento realizado al tercero responsable de la cuenta', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Precio unitario base (NUMERIC 18,2) del producto/servicio según contrato o entrada manual autorizada; sin descuentos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SubTotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Corresponde al valor unitario calculado por el contrato o digitado por el usuario si este tiene permiso del Producto / Servicio  ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SubTotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SubTotalSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID (INT) de condición de tarifa aplicada; usado en servicios para recargos sin reliquidar, accediendo directamente a FK Contract.DefinitionRateDetailCondition.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'DefinitionRateDetailConditionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el detalle de la condicion de la tarifa    Solo para servicios, estos campos se utilizan para cuando se quiere calcular el valor con recargo entonces no se vuelve a liquidar todo ya que con estos campos vamos directamente', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'DefinitionRateDetailConditionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'DefinitionRateDetailConditionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID (INT) del detalle de tarifa contractual; aplicable solo a servicios para determinar valor según definición contratada; FK a Contract.DefinitionRateDetail.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'DefinitionRateDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id del detalle de la tarifa    Solo aplica para Servicios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'DefinitionRateDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'DefinitionRateDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID (INT) del detalle del manual de tarifas usado; nulo si liquidación en grupo de atención es Fija; referencia a desglose de valores ISS/SOAT.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RateManualDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id del detalle del manual de tarifas de donde se tomo el valor.  Si el tipo de liquidacion en el Grupo de Atencion - Tarifas es Fijo este campo es null', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RateManualDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RateManualDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de manual tarifario (TINYINT: 1=ISS 2001, 2=ISS 2004, 3=SOAT) usado en liquidación; solo se completa para servicios.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RateManualType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Tipo del manual tarifario con que se liquido el items  1 - ISS 2001  2 - ISS 2004  3 - SOAT      Solo se llena si el detalle es de tipo Servicios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RateManualType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RateManualType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID (INT) del manual de tarifas parametrizado; permite consultar porcentajes de eventos (Bilateral, MIVIE, etc.) de forma centralizada.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RateManualId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del manual de tarifas que tenga parametrizado en las tarifas o grupo de atencion, esto lo hacemos con el fin de que cuando tengan algun tipo de evento (Bilateran, MIVIE, ETC) se pueda consultar una forma facil el porcentaje a eventos', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RateManualId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RateManualId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de cobro (NUMERIC 5,2, 0-100%) aplicable al servicio o producto; usado en liquidaciones por porcentaje.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RecoveryRatio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de cobro', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RecoveryRatio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RecoveryRatio';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID (INT) del servicio referenciado; calcula el porcentaje a cobrar sobre su valor o incluye al 100% sin cargo adicional.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IncludeServiceOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id del servicio con el que se va a calcular el  porcentaje del valor a cobrar, o en el que se va incluir al 100%', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IncludeServiceOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IncludeServiceOrderDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de liquidación (TINYINT: 1=Manual tarifario, 2=% otro servicio, 3=100% incluido en otro, 4=% mismo servicio); determina método de cálculo.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SettlementType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de liquidacion   1. Por manual de tarifas (Defecto)  2. % de otro servicio cargado  3. 100% incluido dentro de otro servicio (No se cobra nada)  4. % del mismo servicio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SettlementType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SettlementType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID (INT) del centro de costo (unidad administrativa/departamento) donde se genera el gasto; FK a Payroll.CostCenter.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Centro de Costo', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID (INT) del concepto de facturación (grupo de servicios afín); FK a Billing.BillingConcept para clasificar ingresos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'BillingConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Grupo de Servicios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'BillingConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'BillingConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID (INT) del tercero (profesional de salud) que ejecuta el servicio; identifica al médico, cirujano, enfermero, etc.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del tercero profesional de la salud que realiza el servicio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código (CHAR 3) de especialidad médica del profesional que realiza el servicio (ej: 01=Medicina General, 02=Cirugía).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PerformsProfessionalSpecialty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la especialidad el profesional que realiza ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PerformsProfessionalSpecialty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PerformsProfessionalSpecialty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código (CHAR 20) identificador del profesional de salud ejecutor; para cirugías, especifica código del cirujano; proveniente de tablas Crystal INPROFSAL.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Codigo del Profesional de la Salud que Realiza el Servicio y/o Administra el Producto.   Estos datos se sacan de la tabla de Crystal INPROFSAL  Para Qx Se especifica el codigo del profesional cirujano', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID (INT) de la unidad funcional donde se prestó el servicio; identifica área física (quirófano, cama, consultorio, etc.); FK a Payroll.FunctionalUnit.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PerformsFunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad funcional donde se encontraba el paciente', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PerformsFunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PerformsFunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de autorización (VARCHAR 20) emitido por EPS/asegurador; requerido para servicios con preaprobación regulatoria.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'AuthorizationNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Autorizacion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'AuthorizationNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'AuthorizationNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se prestó/registró el servicio; marca momento de ejecución clínica o evento de atención.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ServiceDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en que se presento el servicio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ServiceDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ServiceDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del costo (NUMERIC 18,2) unitario del producto/servicio; usado en cálculos de margen y rentabilidad interna.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CostValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor del costo', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CostValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CostValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Precio de venta (NUMERIC 18) del servicio/producto según manual tarifario de contrato o inventario; valor de referencia contractual.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RateManualSalePrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del Venta del servicio y/o producto establecido en el manual de tarifas de contratos o de inventarios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RateManualSalePrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RateManualSalePrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad devuelta (INT) en dispensaciones farmacéuticas; acumula devoluciones de productos no consumidos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'DevolutionQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Acumula la Cantidad Devuelta de las Dispensaciones Farmaceuticas (Solo Productos).    ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'DevolutionQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'DevolutionQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad entregada (INT) en dispensación farmacéutica; registra unidades físicamente despachadas al paciente/servicio.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SupplyQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Entregada (Solo Productos)  Productos: Almacena la cantidad entregada en la Dispensacion Farmaceutica (Pharmaceutical Dispensing)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SupplyQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'SupplyQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad facturada (INT) = SupplyQuantity - DevolutionQuantity; unidades netas cobradas al tercero pagador.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'InvoicedQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Facturada (Producto / Servicio)  Productos: Almacena la diferencia entre SupplyAmount y DevolutionAmount', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'InvoicedQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'InvoicedQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID (INT) del producto de inventario; solo se completa si ServiceType=Producto; FK a Inventory.InventoryProduct.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del producto, este se llena solo si el tipo de servicio es Producto', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de presentación (TINYINT: 1=No quirúrgico, 2=Quirúrgico, 3=Paquete); clasifica producto por uso clínico.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'Presentation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Presentacion del producto  1 - No quirurgico  2 - Quirurgico  3 - Paquete', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'Presentation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'Presentation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de liquidación (TINYINT: 1=Unidad quemada, 2=Especialidad, 3=Unidad Funcional, 4=Fija, 5=Estándar, 6=Calculado/Fórmula); método de cálculo del valor.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'LiquidationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de liquidacion  1 - Tipo de unidad  - quemados  2 - Especialidad  3 - Unidad Funcional  4 - Fija  5 - Estandar  6 - Calculado o Formula', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'LiquidationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'LiquidationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID (INT) del ítem paquete padre; especifica en qué paquete se incluyó este servicio; referencia interna a empaque.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PackageServiceOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id del detalle de la orden de servicio donde se empaqueto el servicio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PackageServiceOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PackageServiceOrderDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de inclusión en paquete; señala que el ítem está contenido dentro de un paquete de servicios.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'Packaging';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica que el Item se encuentra incluido dentro de un paquete', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'Packaging';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'Packaging';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de que es un paquete; marca que este ítem agrupa/incluye otros servicios relacionados.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IsPackage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el Item es un paquete, el cual esta incluyendo otros servicio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IsPackage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IsPackage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único (VARCHAR 50) para proceso de homologación de servicios; todos los registros con igual código son homólogos adicionales del usuario.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CodeAssociateService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el numero de identificacion unica para el proceso de homologacion. Todos los registros del mismo codigo corresponden a los homologos adicionales seleccionados por el usuario.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CodeAssociateService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CodeAssociateService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de adición automática; marca registros agregados automáticamente por relación en homologación de servicios CUPS.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CUPSAssociateService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el registro corresponde a un registro adicionado automaticamente por estar relacionado en la homologacion de servicios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CUPSAssociateService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CUPSAssociateService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID (INT) del registro Crystal generado; varía según clase: 8=ADCONCOEX (Consulta), 1=ADMORDLAB (Lab), 2=ADMORDPAT (Patología), 3=ADMORDIMA (Imagen); solo si origen es control ambulatorio.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ControlExternalConsultationCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Este campo especifica el Id del codigo registro generado en las tablas de Crystal ya que la tabla depende de la Clase de registro  8 - Consulta Externa --- Tabla ADCONCOEX   1 - Laboratorios --- Tabla ADMORDLAB   2 - Patologias --- Tabla ADMORDPAT   3 - Imagenes --- Tabla ADMORDIMA     Nota: Este campo solo se llena si la orden de servicio fue generada desde control de consulta externa o ambulatoria', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ControlExternalConsultationCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ControlExternalConsultationCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clase de servicio ambulatorio (TINYINT: 8=Consulta Externa, 1=Laboratorios, 2=Patologías, 3=Imágenes); solo si generado desde control de consulta.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ControlExternalConsultation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Este campo especifica el tipo o la clase del servicio de control de consulta externa  8 - Consulta Externa  1 - Laboratorios  2 - Patologias  3 - Imagenes    Nota: Este campo solo se llena si la orden de servicio fue generada desde control de consulta externa o ambulatoria', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ControlExternalConsultation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ControlExternalConsultation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID (INT) del detalle de estancia hospitalaria (CHREGESTADET); se completa si la orden corresponde a hospitalización/internación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'HospitalStayDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id del detalle del registro de estancia (CHREGESTADET), este solo se llena cuando la orden de servicio es un registro de estancia', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'HospitalStayDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'HospitalStayDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID (INT) del registro de estancia (CHREGESTA); identifica período de hospitalización del paciente; solo para órdenes de estancia.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'HospitalStayId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro de la estancia (CHREGESTA), Este campo solo se llena si es un registro de estancia', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'HospitalStayId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'HospitalStayId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID (INT) del servicio IPS homologado a CUPS; identifica servicio en catálogo interno; FK a Contract.IPSService, solo si RecordType=Servicio.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IPSServiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del servicio IPS el cual es homologado a través del CUPS, Solo se llenan si el tipo de registro es Servicio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IPSServiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IPSServiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID (INT) de la entidad de CUPS (código CUPS estandarizado); solo se completa si RecordType=Servicio; FK a Contract.CUPSEntity.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la entidad de CUPS, solo se llenan si el tipo de registro es Servicio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de registro (TINYINT: 1=Servicios, 2=Medicamentos); clasifica naturaleza del ítem facturado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RecordType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se especifica el tipo de servicio de detalle de la orden  1 - Servicios  2 - Medicamentos', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RecordType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RecordType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Forma de carga (TINYINT: 1=SOAT, 2=ISS, 3=CUPS); especifica nomenclatura tarifaria de origen.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ServiceType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de servicio, es decir como se cargo el item  1 - Soat  2 - ISS  3 - CUPS', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ServiceType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ServiceType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID (INT) del tercero pagador (EPS, asegurador, particular); FK a entidad responsable del pago.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero de la entidad', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID (INT) del tercero administrador de salud con quien existe contrato; FK a Contract.HealthAdministrator.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero con quien se realiza el contrato', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID (INT) del grupo de atención/servicio contratado; FK a Contract.CareGroup que define cobertura y tarifas.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CareGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del grupo de atencion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CareGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CareGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID (INT) de la cotización/presupuesto cabecera; FK a tabla padre que agrupa todos los ítems de un presupuesto.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'QuotationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de la cotización', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'QuotationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'QuotationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, PK) del registro de detalle de orden de servicio en cotización; clave primaria clustered de la tabla.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de servicios incluidos en una cotización de orden de servicio. Registra cada ítem cotizado (procedimiento, medicamento, estancia hospitalaria) con sus valores de venta, descuentos, tarifas, cantidades y datos del profesional que realiza la atención, dentro del proceso de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la actividad económica asociada al servicio cotizado, usada para clasificación tributaria o de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'EconomicActivityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'EconomicActivityId';
