CREATE TABLE [Treasury].[CardCostCenter] (
    [Id]              INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CardId]          INT NOT NULL,
    [OperatingUnitId] INT NOT NULL,
    [CostCenterId]    INT NOT NULL,
    CONSTRAINT [PK_CardCostCenter__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CardCostCenter_Cards] FOREIGN KEY ([CardId]) REFERENCES [Treasury].[Cards] ([Id]),
    CONSTRAINT [FK_CardCostCenter_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_CardCostCenter_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [UQ_CardCostCenter__CardId__OperatingUnitId] UNIQUE NONCLUSTERED ([CardId] ASC, [OperatingUnitId] ASC)
);


GO
ALTER TABLE [Treasury].[CardCostCenter] NOCHECK CONSTRAINT [FK_CardCostCenter_Cards];


GO
ALTER TABLE [Treasury].[CardCostCenter] NOCHECK CONSTRAINT [FK_CardCostCenter_CostCenter];


GO
ALTER TABLE [Treasury].[CardCostCenter] NOCHECK CONSTRAINT [FK_CardCostCenter_OperatingUnit];

