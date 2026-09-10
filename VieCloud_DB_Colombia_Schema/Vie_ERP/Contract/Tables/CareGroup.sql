CREATE TABLE [Contract].[CareGroup] (
    [Id]                                        INT             IDENTITY (1, 1) NOT NULL,
    [Code]                                      VARCHAR (20)    NOT NULL,
    [Name]                                      VARCHAR (100)   NOT NULL,
    [CareGroupType]                             TINYINT         NOT NULL,
    [DefaultManual]                             TINYINT         CONSTRAINT [DF_CareGroup_DefaultManual] DEFAULT ((1)) NOT NULL,
    [ContractId]                                INT             NULL,
    [LiquidationType]                           TINYINT         NOT NULL,
    [CostCenterId]                              INT             NOT NULL,
    [BillingPeriod]                             TINYINT         NOT NULL,
    [MaximumIndividualBilling]                  NUMERIC (18, 2) NOT NULL,
    [PeriodMaximumBilling]                      NUMERIC (18, 2) NOT NULL,
    [TypeLiquidationEmergencyStays]             TINYINT         CONSTRAINT [DF_CareGroup_TypeLiquidationEmergencyStays] DEFAULT ((1)) NOT NULL,
    [MinimumObservationTime]                    TINYINT         CONSTRAINT [DF_CareGroup_MinimumObservationTime] DEFAULT ((0)) NOT NULL,
    [MaximumObservationTime]                    TINYINT         CONSTRAINT [DF_CareGroup_MaximumObservationTime] DEFAULT ((0)) NOT NULL,
    [HoursOfRecoveryIncluded]                   TINYINT         CONSTRAINT [DF_CareGroup_HoursOfRecoveryIncluded] DEFAULT ((0)) NOT NULL,
    [RequirementsTemplateId]                    INT             NOT NULL,
    [InvoiceDeadlines]                          INT             NOT NULL,
    [ProcedureTemplateId]                       INT             NOT NULL,
    [ProductRateId]                             INT             NOT NULL,
    [ConceptToBill]                             TINYINT         NOT NULL,
    [EntityType]                                TINYINT         NOT NULL,
    [ContractAccountingStructureId]             INT             NULL,
    [AffectBudget]                              BIT             CONSTRAINT [DF_CareGroup_AffectBudget] DEFAULT ((0)) NOT NULL,
    [BillingBudgetId]                           INT             NULL,
    [OperativeUnitId]                           INT             CONSTRAINT [DF_CareGroup_OperativeUnitId] DEFAULT ((14)) NOT NULL,
    [Status]                                    BIT             CONSTRAINT [DF_CareGroup_Status] DEFAULT ((1)) NOT NULL,
    [CreationUser]                              VARCHAR (20)    NOT NULL,
    [CreationDate]                              DATETIME        NOT NULL,
    [ModificationUser]                          VARCHAR (20)    NULL,
    [ModificationDate]                          DATETIME        NULL,
    [TimeStamp]                                 ROWVERSION      NOT NULL,
    [ExtramuralPharmaceuticalDispensing]        BIT             NULL,
    [ApplyRIAS]                                 BIT             NULL,
    [PromissoryNoteBudgetId]                    INT             NULL,
    [AuthorizationRequired]                     BIT             CONSTRAINT [DF_CareGroup_AuthorizationRequired] DEFAULT ((0)) NOT NULL,
    [AccountReceivablePreviousValidityBudgetId] INT             NULL,
    [PortfolioRecoveryBudgetId]                 INT             NULL,
    [TechnicalNoteId]                           INT             NULL,
    [LiquidateDayDischarge]                     BIT             CONSTRAINT [DF__CareGroup__Liqui__6F3BCD40] DEFAULT ((0)) NOT NULL,
    [MaximumTypeTop]                            BIT             NOT NULL,
    [DiscountContractedCustomer]                DECIMAL (18, 2) CONSTRAINT [DF__CareGroup__Disco__7B427CC9] DEFAULT ((0)) NOT NULL,
    [TypeLiquidationOxygen]                     TINYINT         CONSTRAINT [DF__CareGroup__TypeL__270BF06E] DEFAULT ((1)) NOT NULL,
    [BillingConceptCopayId]                     INT             NULL,
    [ThirdPartyInvoiceCopayFixedAmount]         TINYINT         CONSTRAINT [DF__CareGroup__Third__769E8E94] DEFAULT ((0)) NOT NULL,
    [MOSTRARWEB]                                BIT             NULL,
    [RegisterPolicy]                            BIT             DEFAULT ((0)) NOT NULL,
    [MethodFixedAmountCollectionReport]         INT             CONSTRAINT [DF_CareGroup_MethodFixedAmount] DEFAULT ((2)) NOT NULL,
    [ExcludeFEVRIPS]                            BIT             CONSTRAINT [DF_CareGroup_ExcludeFEVRIPS] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_CareGroup__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CareGroup_BillingConceptCopay] FOREIGN KEY ([BillingConceptCopayId]) REFERENCES [Billing].[BillingConcept] ([Id]),
    CONSTRAINT [FK_CareGroup_Budget] FOREIGN KEY ([BillingBudgetId]) REFERENCES [Budget].[Budget] ([Id]),
    CONSTRAINT [FK_CareGroup_Budget1] FOREIGN KEY ([PromissoryNoteBudgetId]) REFERENCES [Budget].[Budget] ([Id]),
    CONSTRAINT [FK_CareGroup_Budget2] FOREIGN KEY ([AccountReceivablePreviousValidityBudgetId]) REFERENCES [Budget].[Budget] ([Id]),
    CONSTRAINT [FK_CareGroup_Budget3] FOREIGN KEY ([PortfolioRecoveryBudgetId]) REFERENCES [Budget].[Budget] ([Id]),
    CONSTRAINT [FK_CareGroup_CompanyType] FOREIGN KEY ([EntityType]) REFERENCES [Contract].[CompanyType] ([Id]),
    CONSTRAINT [FK_CareGroup_Contract] FOREIGN KEY ([ContractId]) REFERENCES [Contract].[Contract] ([Id]),
    CONSTRAINT [FK_CareGroup_ContractAccountingStructure] FOREIGN KEY ([ContractAccountingStructureId]) REFERENCES [Contract].[ContractAccountingStructure] ([Id]),
    CONSTRAINT [FK_CareGroup_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_CareGroup_OperatingUnit] FOREIGN KEY ([OperativeUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_CareGroup_ProcedureTemplate] FOREIGN KEY ([ProcedureTemplateId]) REFERENCES [Contract].[ProcedureTemplate] ([Id]),
    CONSTRAINT [FK_CareGroup_ProductTemplate] FOREIGN KEY ([ProductRateId]) REFERENCES [Inventory].[ProductRate] ([Id]),
    CONSTRAINT [FK_CareGroup_RequirementTemplate] FOREIGN KEY ([RequirementsTemplateId]) REFERENCES [Contract].[RequirementTemplate] ([Id]),
    CONSTRAINT [FK_CareGroup_TechnicalNote] FOREIGN KEY ([TechnicalNoteId]) REFERENCES [Contract].[TechnicalNote] ([Id])
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
CREATE UNIQUE NONCLUSTERED INDEX [UQ_CareGroup__Code]
    ON [Contract].[CareGroup]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Método de reporte de recaudo en facturas de monto fijo/capitación/PGP: 1=Recaudo real (copagos y cuotas moderadoras reportables), 2=Descuento pactado no contable (sin reporte). INT, FK referencia a configuración de liquidación.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'MethodFixedAmountCollectionReport';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si las facturas monto fijo capitación/PGP reportan o no concepto y valor de recaudo. 1 - Recaudo real (Copagos y Cuotas Moderadoras)/Si reporta. 2 - Descuento pactado sobre Monto Fijo (No contable)/No reporta. ', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'MethodFixedAmountCollectionReport';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'MethodFixedAmountCollectionReport';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si debe registrar póliza contable en la liquidación del grupo de atención. BIT: 0=No registra, 1=Sí registra. Auditoria contable.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'RegisterPolicy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si debe Registrar Póliza (0 = No, 1 = Sí)', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'RegisterPolicy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'RegisterPolicy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de tercero para liquidación de copagos en monto fijo: 0=Tercero del grupo de atención, 1=Tercero factura monto fijo. TINYINT, configura tercero pagador.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'ThirdPartyInvoiceCopayFixedAmount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de liquidacion  0 - Tercero del grupo de atencion  1 - tercero factura monto fijo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'ThirdPartyInvoiceCopayFixedAmount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'ThirdPartyInvoiceCopayFixedAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del concepto de facturación para copagos y cuotas moderadoras. FK a [Billing].[BillingConcept]. Configura línea de factura de paciente.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'BillingConceptCopayId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concepto de facturacion copagos y cuotas moderadoras', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'BillingConceptCopayId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'BillingConceptCopayId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de unidad para liquidar oxígeno terapéutico: 1=Litros consumidos, 2=Horas suministro. TINYINT, afecta cálculo de factura.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'TypeLiquidationOxygen';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que determina el tipo para liquidar el Oxigeno: 1 => litros 2 => Horas.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'TypeLiquidationOxygen';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'TypeLiquidationOxygen';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descuento porcentual específico al cliente/asegurador sobre el monto fijo o servicios contratados. DECIMAL(18,2), default 0. Afecta ingreso neto.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'DiscountContractedCustomer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descuento especifico al cliente', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'DiscountContractedCustomer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'DiscountContractedCustomer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica si el grupo de atención maneja tope máximo de facturación por tipo: 1=Sí aplica tope, 0=No aplica. BIT, limita cobro máximo.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'MaximumTypeTop';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si maneja un tope máximo del tipo, 1 - Si, 0 - No.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'MaximumTypeTop';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'MaximumTypeTop';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si liquida el día de egreso/alta del paciente en estancias: 0=No liquida, 1=Sí liquida. BIT, afecta cálculo de días de estadía.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'LiquidateDayDischarge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo booleano que especifica 0 - No liquida dia de egreso, 1 - Liquida dia de egreso.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'LiquidateDayDischarge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'LiquidateDayDischarge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de la nota técnica/especificación del contrato. FK a [Contract].[TechnicalNote]. Documenta términos especiales.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'TechnicalNoteId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la nota tecnica', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'TechnicalNoteId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'TechnicalNoteId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del presupuesto de ingresos para pagos de facturas de períodos anteriores a vigencia inmediatamente anterior. FK a [Budget].[Budget]. Gestiona cartera vencida.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'PortfolioRecoveryBudgetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id del presupuesto de ingresos que corresponderá al pago de facturas de periodos mucho más antiguos que la anterior vigencia.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'PortfolioRecoveryBudgetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'PortfolioRecoveryBudgetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del presupuesto de ingresos para facturas de vigencia inmediatamente anterior. FK a [Budget].[Budget]. Cierre de períodos contables.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'AccountReceivablePreviousValidityBudgetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id del presupuesto de ingresos que corresponderá al pago de facturas de la vigencia inmediatamente anterior', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'AccountReceivablePreviousValidityBudgetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'AccountReceivablePreviousValidityBudgetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si órdenes de servicio/atención en el grupo requieren número de autorización obligatorio. BIT: 0=No, 1=Sí. Control acceso servicios.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'AuthorizationRequired';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite saber si al realizar una orden de servicio requiere que el no. de autorización sea obligatorio o no', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'AuthorizationRequired';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'AuthorizationRequired';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del presupuesto de ingresos para reconocimiento de deudas/pagarés del paciente. FK a [Budget].[Budget]. Contabiliza obligaciones.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'PromissoryNoteBudgetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id del presupuesto de ingresos a usar en el reconocimiento de deudas de paciente', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'PromissoryNoteBudgetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'PromissoryNoteBudgetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el grupo de atención aplica a RIAS (Red Integrada de Atención en Salud) en aseguradoras EAPB. BIT NULL, se llena solo en EAPB con contrato. Cumplimiento regulatorio.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'ApplyRIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite saber si aplica a RIAS, este campo se llena siempre y cuando el tipo de grupo de atención es EAPB con Contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'ApplyRIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'ApplyRIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el grupo autoriza dispensación farmacéutica extramural (fuera de instalaciones). BIT NULL, 1=Sí permite, 0=No. Servicios farmacéuticos domiciliarios.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'ExtramuralPharmaceuticalDispensing';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dispensación Farmacéutica Extramural', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'ExtramuralPharmaceuticalDispensing';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'ExtramuralPharmaceuticalDispensing';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal automática de auditoria en SQL Server. TIMESTAMP, registra instante de creación/modificación del registro. Control de cambios.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación del grupo de atención. DATETIME NULL. Auditoria de cambios.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó última modificación del grupo de atención. VARCHAR(20). Trazabilidad de cambios.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación inicial del grupo de atención en el contrato. DATETIME NOT NULL. Auditoria de registro.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el grupo de atención. VARCHAR(20) NOT NULL. Trazabilidad de registro.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado operativo del grupo de atención: 1=Activo (vigente), 0=Inactivo (suspendido). BIT, default 1. Habilita facturación y servicios.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del grupo de atencion  1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de la unidad operativa/sede que gestiona el grupo de atención. FK a [Common].[OperatingUnit], default 14. Asignación organizacional.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'OperativeUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad operativa seleccionada', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'OperativeUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'OperativeUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del presupuesto de ingresos principal para reconocimiento de facturas del grupo. FK a [Budget].[Budget]. Contabilización de facturación.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'BillingBudgetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id del presupuesto de ingresos a usar en el reconocimiento de facturas', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'BillingBudgetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'BillingBudgetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el grupo afecta presupuesto de ingresos automáticamente: 1=Sí crea reconocimiento al radicar factura, 0=No afecta. BIT, default 0. Control presupuestario.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'AffectBudget';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el grupo de atencion afecta presupuesto  1 - Si  0- No    Si esta como si, entonces cuando se vaya a radicar la factura este creara un reconocimiento automaticamente', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'AffectBudget';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'AffectBudget';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de estructura contable del contrato para asignación de cuentas. FK a [Contract].[ContractAccountingStructure] NULL. Mapeo contable.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'ContractAccountingStructureId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de estructura contrato de contabilidad ', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'ContractAccountingStructureId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'ContractAccountingStructureId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de entidad asegurador/tercero pagador. TINYINT FK a [Contract].[CompanyType]: 1=EPS Contributivo, 2=EPS Subsidiado, 3=ET Municipios, 4=ET Departamentos, 5=ARL, 6=Medicina Prepagada, 7=IPS Privada, 8=IPS Pública, 9=Régimen Especial, 10=SOAT, 11=Fosyga, 12=Otros, 13=Aseguradoras, 99=Particulares.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'EntityType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de entidad  Es una llave foranea de CompanyType donde se trae el ID  1 - EPS Contributivo  2 - EPS Subsidiado  3 - ET Vinculados Municipios  4 - ET Vinculados Departamentos  5 - ARL Riesgos Laborales  6 - MP Medicina Prepagada  7 - IPS Privada  8 - IPS Publica  9 - Regimen Especial  10 - Accidentes de transito  11 - Fosyga  12 - Otros  13 - Aseguradoras  99 - Particulares', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'EntityType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'EntityType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concepto de facturación que clasifica el servicio según régimen/fuente: 1=POS-EPS, 2=Plan complementario, 3=POSS-ARS, 4=Servicios EPS privada, 5=Medicina prepagada, 6=Aseguradoras, 7=Particulares, 8=IPS pública, 9=Régimen especial, 10=Subsidio oferta, 11=ARP, 12=Vinculados, 13=SOAT, 14=Fosyga ECAT, 15=Fosyga Trauma, 16=Min Salud IVA Social, 17=Otras cuentas por cobrar, 18=Presupuesto máximo, 19=Urgencia migrante.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'ConceptToBill';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concepto a facturar  1 - Plan Obligatorio Salud POS - EPS  2 - Plan Complementario EPS  3 - Plan Subsidiado salud POSS- ARS  4 - Servicios Salud EPS privadas  5 - Empresas medicinas pregagadas  6 - Servicios Salud Compañias Aseguradoras  7 - Servicios Salud Particulares  8 - Servicio Salud IPS publica  9 - Servicio Salud Empresas regimen Especial  10 - Atencion Cargo Subsidio oferta  11 - Riesgos profesionales ARP  12 - Cuota recuperacion vinculados  13 - Atencion Accidentes transito SOAT compañias Seguro  14 - Reclamaciones Fosyga ECAT  15 - Convenios Fosyga Trauma Mayor Desplazados  16 - Min Salud Recursos IVA Social  17 - Otras cuentas por cobrar servicio salud. 18 - Presupuesto Máximo. 19 - Urgencia Población migrante', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'ConceptToBill';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'ConceptToBill';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de plantilla de tarifas de productos/medicamentos del grupo. FK a [Inventory].[ProductRate]. Valida precios de facturación.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'ProductRateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la plantilla de productos', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'ProductRateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'ProductRateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de plantilla de procedimientos autorizados/tarifarios del grupo. FK a [Contract].[ProcedureTemplate]. Valida procedimientos facturables.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'ProcedureTemplateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la plantilla de procedimientos', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'ProcedureTemplateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'ProcedureTemplateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plazo máximo en días para presentar factura posterior a atención. INT, configura vencimiento administrativo.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'InvoiceDeadlines';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Plazo de la factura', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'InvoiceDeadlines';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'InvoiceDeadlines';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de plantilla de requerimientos/documentos obligatorios para facturación. FK a [Contract].[RequirementTemplate]. Validación de requisitos.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'RequirementsTemplateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la plantilla de requerimientos', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'RequirementsTemplateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'RequirementsTemplateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Horas de recuperación postoperatoria incluidas en la tarifa quirúrgica. TINYINT, tras superar este tiempo se cobra unidad de estancia. Incluido en honorarios.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'HoursOfRecoveryIncluded';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica las hora de recuperacios que estan incluuidas dentro de una intervencion quirurgica, es decir que despues de que pase este tiempo se podra cobrar la unidad de estancia de lo contrario no se cobrara nada ya que se encuentra incluido dentro de la intervencion', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'HoursOfRecoveryIncluded';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'HoursOfRecoveryIncluded';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo máximo en horas para cobro de observación en estancias SOAT. TINYINT, solo se carga si observación ≤ este valor.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'MaximumObservationTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiempo maximo para el cobro de la observacion en estancias, este campo solo se solicita cuando el tipo de liquidacion es SOAT, es decir que solo si las horas de estancas es menor o igual al valor digitado en este campo se podra cobrar la unidad de observacion', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'MaximumObservationTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'MaximumObservationTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo mínimo en horas para iniciar cobro de observación en estancias ISS. TINYINT, tras superar se liquida unidad de observación.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'MinimumObservationTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiempo minimo de liquidacion para la observacion en estancias, este campo solo se solicita si es ISS, es decir que despues de que la estancias supere el valor digitado se va poder liquidar una unidad por concepto de observacion  ', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'MinimumObservationTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'MinimumObservationTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de liquidación para estancias de urgencia/emergencia: 1=ISS, 2=SOAT. TINYINT, configura tarifa y cálculo.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'TypeLiquidationEmergencyStays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de liquidacion de estancias de emergencias  1 - ISS  2 - SOAT', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'TypeLiquidationEmergencyStays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'TypeLiquidationEmergencyStays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tope máximo de facturación permitida por período de liquidación. NUMERIC(18,2), limita ingresos por ciclo.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'PeriodMaximumBilling';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tope maximo de la facturacion por periodo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'PeriodMaximumBilling';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'PeriodMaximumBilling';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tope máximo de facturación individual por paciente/atención. NUMERIC(18,2), limita cobro máximo por caso.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'MaximumIndividualBilling';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tope Maximo de facturacion individual', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'MaximumIndividualBilling';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'MaximumIndividualBilling';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Período de facturación del grupo de atención: 1=Semanal, 2=Quincenal, 3=Mensual, 4=Bimestral, 5=Semestral, 6=Anual. TINYINT, cadencia de facturación.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'BillingPeriod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Periodo de facturacion   1 - Semanal  2 - Quincenal  3 - Mensual  4 - BiMensual  5 - Semestral  6 - Anual', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'BillingPeriod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'BillingPeriod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del centro de costo/departamento para imputación de ingresos. FK a [Payroll].[CostCenter] NOT NULL. Contabilización analítica.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id del centro de costo, este se pide siempre, ya que si alguna cuenta contable lo requiere se va a obtener de este', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de contrato/liquidación del grupo: 1=Pago por servicios, 2=Capitación, 3=Factura global, 4=Capitación global, 5=PGP (Pago Global Prospectivo). TINYINT, define modelo financiero.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'LiquidationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de liquidacion  1 - Pago por Servicios  2 - Capitacion  3 - Factura Global  4 - Capitacion Global  5 - Pago Global Prospectivo - PGP', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'LiquidationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'LiquidationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del contrato padre. FK a [Contract].[Contract] NULL, se llena solo si CareGroupType=EAPB con contrato. Relación contractual.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'ContractId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del contrato, este se llena solo si el tipo del grupo de atencion es EAPB con contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'ContractId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'ContractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Manual tarifario por defecto del grupo: 1=SOAT, 2=ISS, 3=Mixto. TINYINT, base de precios inicial.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'DefaultManual';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si tiene un manual tarifario por defecto  1 - SOAT  2 - ISS  3 - Mixto', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'DefaultManual';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'DefaultManual';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del grupo de atención: 1=EAPB con contrato, 2=EAPB sin contrato, 3=Particulares, 4=Aseguradoras. TINYINT, determina régimen.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'CareGroupType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de Grupo de atencion  1 - EAPB Con contrato  2 - EAPB Sin Contrato  3 - Particulares  4 - Aseguradoras', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'CareGroupType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'CareGroupType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo del grupo de atención/asegurador. VARCHAR(100) NOT NULL, ej: ''''EPS Salud Total'''', ''''Aseguradora XYZ''''. Búsqueda por nombre.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del grupo de atencion', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del grupo de atención en el sistema. VARCHAR(20) NOT NULL, ej: ''''EPST'''', ''''ASG001''''. Identificación rápida.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del grupo de atencion', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK IDENTITY) del grupo de atención en la tabla. INT NOT NULL, autoincremental. Referencia principal.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del grupo de atencion', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Excluye las facturas del grupo EAPB del envío al Mecanismo de Validación FEV RIPS. Por defecto permanece desmarcado para preservar el comportamiento actual.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'ExcludeFEVRIPS';
GO
CREATE NONCLUSTERED INDEX [IX_CareGroup_Id_Covering]
    ON [Contract].[CareGroup]([Id] ASC)
    INCLUDE([Code], [Name], [EntityType], [ContractId]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grupos de atención o cuidado definidos dentro de un contrato, que agrupan reglas de facturación, liquidación, presupuesto, copagos y parámetros clínicos (tiempos de observación, recuperación, urgencias) aplicables a una entidad pagadora. Determina cómo se cobran y liquidan los servicios prestados bajo cada modalidad contractual.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el grupo de atención es visible en el portal web o aplicación en línea para los usuarios o pacientes.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'MOSTRARWEB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'MOSTRARWEB';
