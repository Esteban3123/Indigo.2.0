CREATE TABLE [Cost].[CostDistributionDirectCost] (
    [Id]                           INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                         VARCHAR (20)    NOT NULL,
    [GeneralExpenseId]             INT             NOT NULL,
    [Description]                  VARCHAR (300)   NOT NULL,
    [Year]                         INT             NOT NULL,
    [Month]                        INT             NOT NULL,
    [Value]                        NUMERIC (20, 2) NOT NULL,
    [Status]                       TINYINT         NOT NULL,
    [CreationUser]                 VARCHAR (20)    NOT NULL,
    [CreationDate]                 DATETIME        NOT NULL,
    [ModificationUser]             VARCHAR (20)    NULL,
    [ModificationDate]             DATETIME        NULL,
    [TimeStamp]                    ROWVERSION      NOT NULL,
    [ThirdPartyId]                 INT             NOT NULL,
    [ConfirmUser]                  VARCHAR (20)    NULL,
    [ConfirmDate]                  DATETIME        NULL,
    [AnnulmentUser]                VARCHAR (20)    NULL,
    [AnnulmentDate]                DATETIME        NULL,
    [Observation]                  VARCHAR (500)   CONSTRAINT [DF_CostDistributionDirectCost_Observation] DEFAULT ('') NOT NULL,
    [BillNumber]                   VARCHAR (100)   CONSTRAINT [DF__CostDistr__BillN__11093764] DEFAULT ('') NOT NULL,
    [AccountPayableId]             INT             NULL,
    [OperatingUnitId]              INT             NULL,
    [SupplierId]                   INT             NULL,
    [SuppliersDistributionLinesId] INT             NULL,
    [Term]                         INT             NULL,
    [CostCenterId]                 INT             NULL,
    [FilingUnitId]                 INT             NULL,
    [SupplierTypeId]               INT             NULL,
    [BillDate]                     DATE            NULL,
    [MainAccountId]                INT             NULL,
    [ServicePeriodDate]            DATE            NULL,
    [PositionId]                   INT             NULL,
    [Hours]                        INT             NULL,
    [DeductibleIva]                BIT             NULL,
    [CurrencyId]                   INT             CONSTRAINT [DF__CostDistr__Curre__0ECA5289] DEFAULT ((1)) NOT NULL,
    [ProvisionDocument]            BIT             CONSTRAINT [DF__CostDistr__Provi__09517055] DEFAULT ((0)) NOT NULL,
    [AccountPayableSameSupplier]   BIT             CONSTRAINT [DF__CostDistr__Accou__50F80899] DEFAULT ((0)) NOT NULL,
    [TaxRegistration]              TINYINT         NULL,
    [DocumentDate]                 DATE            NULL,
    CONSTRAINT [PK_CostDistributionDirectCost__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostDistributionDirectCost_AccountPayable] FOREIGN KEY ([AccountPayableId]) REFERENCES [Payments].[AccountPayable] ([Id]),
    CONSTRAINT [FK_CostDistributionDirectCost_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_CostDistributionDirectCost_CostGeneralExpense] FOREIGN KEY ([GeneralExpenseId]) REFERENCES [Cost].[CostGeneralExpense] ([Id]),
    CONSTRAINT [FK_CostDistributionDirectCost_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_CostDistributionDirectCost_FilingUnit] FOREIGN KEY ([FilingUnitId]) REFERENCES [Payments].[FilingUnit] ([Id]),
    CONSTRAINT [FK_CostDistributionDirectCost_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_CostDistributionDirectCost_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_CostDistributionDirectCost_Position] FOREIGN KEY ([PositionId]) REFERENCES [Payroll].[Position] ([Id]),
    CONSTRAINT [FK_CostDistributionDirectCost_Supplier] FOREIGN KEY ([SupplierId]) REFERENCES [Common].[Supplier] ([Id]),
    CONSTRAINT [FK_CostDistributionDirectCost_SuppliersDistributionLines] FOREIGN KEY ([SuppliersDistributionLinesId]) REFERENCES [Common].[SuppliersDistributionLines] ([Id]),
    CONSTRAINT [FK_CostDistributionDirectCost_SupplierType] FOREIGN KEY ([SupplierTypeId]) REFERENCES [Common].[SupplierType] ([Id]),
    CONSTRAINT [FK_CostDistributionDirectCost_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [UQ_CostDistributionDirectCost__Id__GeneralExpenseId__Year__Month] UNIQUE NONCLUSTERED ([Id] ASC, [GeneralExpenseId] ASC, [Year] ASC, [Month] ASC)
);


GO
ALTER TABLE [Cost].[CostDistributionDirectCost] NOCHECK CONSTRAINT [FK_CostDistributionDirectCost_AccountPayable];


GO
ALTER TABLE [Cost].[CostDistributionDirectCost] NOCHECK CONSTRAINT [FK_CostDistributionDirectCost_CostCenter];


GO
ALTER TABLE [Cost].[CostDistributionDirectCost] NOCHECK CONSTRAINT [FK_CostDistributionDirectCost_CostGeneralExpense];


GO
ALTER TABLE [Cost].[CostDistributionDirectCost] NOCHECK CONSTRAINT [FK_CostDistributionDirectCost_Currency];


GO
ALTER TABLE [Cost].[CostDistributionDirectCost] NOCHECK CONSTRAINT [FK_CostDistributionDirectCost_FilingUnit];


GO
ALTER TABLE [Cost].[CostDistributionDirectCost] NOCHECK CONSTRAINT [FK_CostDistributionDirectCost_MainAccounts];


GO
ALTER TABLE [Cost].[CostDistributionDirectCost] NOCHECK CONSTRAINT [FK_CostDistributionDirectCost_OperatingUnit];


GO
ALTER TABLE [Cost].[CostDistributionDirectCost] NOCHECK CONSTRAINT [FK_CostDistributionDirectCost_Position];


GO
ALTER TABLE [Cost].[CostDistributionDirectCost] NOCHECK CONSTRAINT [FK_CostDistributionDirectCost_Supplier];


GO
ALTER TABLE [Cost].[CostDistributionDirectCost] NOCHECK CONSTRAINT [FK_CostDistributionDirectCost_SuppliersDistributionLines];


GO
ALTER TABLE [Cost].[CostDistributionDirectCost] NOCHECK CONSTRAINT [FK_CostDistributionDirectCost_SupplierType];


GO
ALTER TABLE [Cost].[CostDistributionDirectCost] NOCHECK CONSTRAINT [FK_CostDistributionDirectCost_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del documento cuando No se genera CxP', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCost', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Iva parametrizado', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCost', @level2type = N'COLUMN', @level2name = N'TaxRegistration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica el parametro de IVA descontable', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCost', @level2type = N'COLUMN', @level2name = N'DeductibleIva';

