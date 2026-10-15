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
ALTER TABLE [Contract].[CareGroup] NOCHECK CONSTRAINT [FK_CareGroup_BillingConceptCopay];


GO
ALTER TABLE [Contract].[CareGroup] NOCHECK CONSTRAINT [FK_CareGroup_Budget];


GO
ALTER TABLE [Contract].[CareGroup] NOCHECK CONSTRAINT [FK_CareGroup_Budget1];


GO
ALTER TABLE [Contract].[CareGroup] NOCHECK CONSTRAINT [FK_CareGroup_Budget2];


GO
ALTER TABLE [Contract].[CareGroup] NOCHECK CONSTRAINT [FK_CareGroup_Budget3];


GO
ALTER TABLE [Contract].[CareGroup] NOCHECK CONSTRAINT [FK_CareGroup_CompanyType];


GO
ALTER TABLE [Contract].[CareGroup] NOCHECK CONSTRAINT [FK_CareGroup_Contract];


GO
ALTER TABLE [Contract].[CareGroup] NOCHECK CONSTRAINT [FK_CareGroup_ContractAccountingStructure];


GO
ALTER TABLE [Contract].[CareGroup] NOCHECK CONSTRAINT [FK_CareGroup_CostCenter];


GO
ALTER TABLE [Contract].[CareGroup] NOCHECK CONSTRAINT [FK_CareGroup_OperatingUnit];


GO
ALTER TABLE [Contract].[CareGroup] NOCHECK CONSTRAINT [FK_CareGroup_ProcedureTemplate];


GO
ALTER TABLE [Contract].[CareGroup] NOCHECK CONSTRAINT [FK_CareGroup_ProductTemplate];


GO
ALTER TABLE [Contract].[CareGroup] NOCHECK CONSTRAINT [FK_CareGroup_RequirementTemplate];


GO
ALTER TABLE [Contract].[CareGroup] NOCHECK CONSTRAINT [FK_CareGroup_TechnicalNote];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_CareGroup__Code]
    ON [Contract].[CareGroup]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si las facturas monto fijo capitación/PGP reportan o no concepto y valor de recaudo. 1 - Recaudo real (Copagos y Cuotas Moderadoras)/Si reporta. 2 - Descuento pactado sobre Monto Fijo (No contable)/No reporta. ', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'MethodFixedAmountCollectionReport';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si debe Registrar Póliza (0 = No, 1 = Sí)', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroup', @level2type = N'COLUMN', @level2name = N'RegisterPolicy';

