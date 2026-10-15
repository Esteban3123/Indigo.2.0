CREATE TABLE [Payments].[AccountPayable] (
    [Id]                           INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                         VARCHAR (20)    NOT NULL,
    [NumberFiling]                 BIGINT          CONSTRAINT [DF_AccountPayable_NumberFiling] DEFAULT ((1)) NOT NULL,
    [EntityId]                     INT             NULL,
    [EntityCode]                   VARCHAR (20)    NULL,
    [EntityName]                   VARCHAR (250)   NULL,
    [IdSupplier]                   INT             NOT NULL,
    [IdThirdParty]                 INT             NULL,
    [IdAccount]                    INT             NOT NULL,
    [IdCostCenter]                 INT             NULL,
    [BillNumber]                   VARCHAR (100)   NOT NULL,
    [BillDate]                     DATETIME        NOT NULL,
    [DocumentDate]                 DATETIME        NOT NULL,
    [ServicePeriodDate]            DATETIME        CONSTRAINT [DF_AccountPayable_ServicePeriodDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [FilingUnitId]                 INT             NOT NULL,
    [SupplierTypeId]               INT             NOT NULL,
    [Term]                         INT             NOT NULL,
    [ExpirationDate]               DATETIME        NOT NULL,
    [Coments]                      VARCHAR (MAX)   NULL,
    [Status]                       TINYINT         NOT NULL,
    [InitialBalance]               BIT             NOT NULL,
    [IdInitialBalance]             INT             NULL,
    [PreviousBudget]               BIT             NOT NULL,
    [Shares]                       INT             NOT NULL,
    [InvoiceValue]                 DECIMAL (18, 2) CONSTRAINT [DF_AccountPayable_ValueInvoice] DEFAULT ((0.0)) NOT NULL,
    [Value]                        DECIMAL (18, 2) NOT NULL,
    [Balance]                      DECIMAL (18, 2) NOT NULL,
    [IdOperatingUnit]              INT             NULL,
    [IdSuppliersDistributionLines] INT             NULL,
    [CostDistributionDirectCostId] INT             NULL,
    [CreationUser]                 VARCHAR (20)    NOT NULL,
    [CreationDate]                 DATETIME        NOT NULL,
    [ModificationUser]             VARCHAR (20)    NULL,
    [ModificationDate]             DATETIME        NULL,
    [ConfirmationUser]             VARCHAR (20)    NULL,
    [ConfirmationDate]             DATETIME        NULL,
    [AnnulmentUser]                VARCHAR (20)    NULL,
    [AnnulmentDate]                DATETIME        NULL,
    [TimeStamp]                    ROWVERSION      NOT NULL,
    [PositionId]                   INT             NULL,
    [Hours]                        INT             CONSTRAINT [DF_AccountPayable_Hours] DEFAULT ((0)) NOT NULL,
    [CommitmentDetailId]           INT             NULL,
    [HandlesDocumentSupport]       BIT             CONSTRAINT [DF__AccountPa__Handl__68775F13] DEFAULT ((0)) NOT NULL,
    [DocumentSupportId]            INT             NULL,
    [DeductibleIva]                BIT             NULL,
    [CurrencyId]                   INT             CONSTRAINT [DF__AccountPa__Curre__7CB5C3EF] DEFAULT ((1)) NULL,
    [TRMValue]                     NUMERIC (20, 5) CONSTRAINT [DF__AccountPa__TRMVa__7E9E0C61] DEFAULT ((1)) NOT NULL,
    [TaxRegistration]              TINYINT         NULL,
    [IdEconomicActivity]           INT             NULL,
    CONSTRAINT [PK_AccountPayable__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountPayable_Commitment] FOREIGN KEY ([CommitmentDetailId]) REFERENCES [Budget].[CommitmentDetail] ([Id]),
    CONSTRAINT [FK_AccountPayable_CostCenter] FOREIGN KEY ([IdCostCenter]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_AccountPayable_CostDistributionDirectCost] FOREIGN KEY ([CostDistributionDirectCostId]) REFERENCES [Cost].[CostDistributionDirectCost] ([Id]),
    CONSTRAINT [FK_AccountPayable_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_AccountPayable_EconomicActivity] FOREIGN KEY ([IdEconomicActivity]) REFERENCES [Common].[EconomicActivity] ([Id]),
    CONSTRAINT [FK_AccountPayable_FilingUnit] FOREIGN KEY ([FilingUnitId]) REFERENCES [Payments].[FilingUnit] ([Id]),
    CONSTRAINT [FK_AccountPayable_InitialBalance] FOREIGN KEY ([IdInitialBalance]) REFERENCES [Payments].[InitialBalance] ([Id]),
    CONSTRAINT [FK_AccountPayable_MainAccounts] FOREIGN KEY ([IdAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_AccountPayable_OperatingUnit] FOREIGN KEY ([IdOperatingUnit]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_AccountPayable_Position] FOREIGN KEY ([PositionId]) REFERENCES [Payroll].[Position] ([Id]),
    CONSTRAINT [FK_AccountPayable_Supplier] FOREIGN KEY ([IdSupplier]) REFERENCES [Common].[Supplier] ([Id]),
    CONSTRAINT [FK_AccountPayable_SuppliersDistributionLines] FOREIGN KEY ([IdSuppliersDistributionLines]) REFERENCES [Common].[SuppliersDistributionLines] ([Id]),
    CONSTRAINT [FK_AccountPayable_SupplierType] FOREIGN KEY ([SupplierTypeId]) REFERENCES [Common].[SupplierType] ([Id]),
    CONSTRAINT [FK_AccountPayable_ThirdParty] FOREIGN KEY ([IdThirdParty]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Payments].[AccountPayable] NOCHECK CONSTRAINT [FK_AccountPayable_Commitment];


GO
ALTER TABLE [Payments].[AccountPayable] NOCHECK CONSTRAINT [FK_AccountPayable_CostCenter];


GO
ALTER TABLE [Payments].[AccountPayable] NOCHECK CONSTRAINT [FK_AccountPayable_CostDistributionDirectCost];


GO
ALTER TABLE [Payments].[AccountPayable] NOCHECK CONSTRAINT [FK_AccountPayable_Currency];


GO
ALTER TABLE [Payments].[AccountPayable] NOCHECK CONSTRAINT [FK_AccountPayable_EconomicActivity];


GO
ALTER TABLE [Payments].[AccountPayable] NOCHECK CONSTRAINT [FK_AccountPayable_FilingUnit];


GO
ALTER TABLE [Payments].[AccountPayable] NOCHECK CONSTRAINT [FK_AccountPayable_InitialBalance];


GO
ALTER TABLE [Payments].[AccountPayable] NOCHECK CONSTRAINT [FK_AccountPayable_MainAccounts];


GO
ALTER TABLE [Payments].[AccountPayable] NOCHECK CONSTRAINT [FK_AccountPayable_OperatingUnit];


GO
ALTER TABLE [Payments].[AccountPayable] NOCHECK CONSTRAINT [FK_AccountPayable_Position];


GO
ALTER TABLE [Payments].[AccountPayable] NOCHECK CONSTRAINT [FK_AccountPayable_Supplier];


GO
ALTER TABLE [Payments].[AccountPayable] NOCHECK CONSTRAINT [FK_AccountPayable_SuppliersDistributionLines];


GO
ALTER TABLE [Payments].[AccountPayable] NOCHECK CONSTRAINT [FK_AccountPayable_SupplierType];


GO
ALTER TABLE [Payments].[AccountPayable] NOCHECK CONSTRAINT [FK_AccountPayable_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro fiscal', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'TaxRegistration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Iva deductible', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'DeductibleIva';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayable', @level2type = N'COLUMN', @level2name = N'Value';

