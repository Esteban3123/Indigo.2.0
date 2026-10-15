CREATE TABLE [Cost].[CostDistributionDirectCostDetail] (
    [Id]                       INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DistributionDirectCostId] INT             NOT NULL,
    [ProductionCenterId]       INT             NULL,
    [MainAccountId]            INT             NOT NULL,
    [CostCenterId]             INT             NULL,
    [MeasurementUnitId]        INT             NULL,
    [Percentage]               NUMERIC (18, 4) CONSTRAINT [DF_CostDistributionDirectCostDetail_Percentage] DEFAULT ((0)) NULL,
    [Count]                    NUMERIC (18, 2) CONSTRAINT [DF_CostDistributionDirectCostDetail_Count] DEFAULT ((0)) NULL,
    [CostValue]                DECIMAL (18, 2) CONSTRAINT [DF_CostDistributionDirectCostDetail_CostValue] DEFAULT ((0)) NULL,
    [Value]                    NUMERIC (18, 2) NOT NULL,
    [BaseValue]                DECIMAL (18, 2) NULL,
    [IvaValue]                 DECIMAL (18, 2) NULL,
    [ThirdPartyId]             INT             NULL,
    [HoursManpower]            DECIMAL (18, 2) NULL,
    [JobTitle]                 VARCHAR (50)    NULL,
    [ProductId]                INT             NULL,
    [ManpowerHoursContracted]  DECIMAL (18, 2) NULL,
    [PositionId]               INT             NULL,
    [EmployeeId]               INT             NULL,
    [InvoicedValue]            DECIMAL (13, 2) NULL,
    [BaseRetention]            DECIMAL (11, 2) NULL,
    [Observations]             VARCHAR (200)   NULL,
    [PayrollConcept]           VARCHAR (200)   NULL,
    [ProcessDate]              DATETIME        NULL,
    [Nature]                   TINYINT         NULL,
    [RetentionId]              INT             NULL,
    CONSTRAINT [PK_CostDistributionDirectCostDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostDistributionDirectCostDetail_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_CostDistributionDirectCostDetail_CostDistributionDirectCost] FOREIGN KEY ([DistributionDirectCostId]) REFERENCES [Cost].[CostDistributionDirectCost] ([Id]),
    CONSTRAINT [FK_CostDistributionDirectCostDetail_CostProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [Cost].[CostProductionCenter] ([Id]),
    CONSTRAINT [FK_CostDistributionDirectCostDetail_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_CostDistributionDirectCostDetail_InventoryMeasurementUnit] FOREIGN KEY ([MeasurementUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id]),
    CONSTRAINT [FK_CostDistributionDirectCostDetail_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_CostDistributionDirectCostDetail_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_CostDistributionDirectCostDetail_PositionId] FOREIGN KEY ([PositionId]) REFERENCES [Payroll].[Position] ([Id]),
    CONSTRAINT [FK_CostDistributionDirectCostDetail_RetentionConcepts] FOREIGN KEY ([RetentionId]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id]),
    CONSTRAINT [FK_CostDistributionDirectCostDetail_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Cost].[CostDistributionDirectCostDetail] NOCHECK CONSTRAINT [FK_CostDistributionDirectCostDetail_CostCenter];


GO
ALTER TABLE [Cost].[CostDistributionDirectCostDetail] NOCHECK CONSTRAINT [FK_CostDistributionDirectCostDetail_CostDistributionDirectCost];


GO
ALTER TABLE [Cost].[CostDistributionDirectCostDetail] NOCHECK CONSTRAINT [FK_CostDistributionDirectCostDetail_CostProductionCenter];


GO
ALTER TABLE [Cost].[CostDistributionDirectCostDetail] NOCHECK CONSTRAINT [FK_CostDistributionDirectCostDetail_InventoryMeasurementUnit];


GO
ALTER TABLE [Cost].[CostDistributionDirectCostDetail] NOCHECK CONSTRAINT [FK_CostDistributionDirectCostDetail_InventoryProduct];


GO
ALTER TABLE [Cost].[CostDistributionDirectCostDetail] NOCHECK CONSTRAINT [FK_CostDistributionDirectCostDetail_MainAccounts];


GO
ALTER TABLE [Cost].[CostDistributionDirectCostDetail] NOCHECK CONSTRAINT [FK_CostDistributionDirectCostDetail_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor Base', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'BaseValue';

