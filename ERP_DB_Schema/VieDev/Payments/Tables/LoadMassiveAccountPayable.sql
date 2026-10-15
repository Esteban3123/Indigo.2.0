CREATE TABLE [Payments].[LoadMassiveAccountPayable] (
    [Id]                 INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [LoadMassiveId]      INT             NOT NULL,
    [SupplierId]         INT             NOT NULL,
    [DistributionLineId] INT             NOT NULL,
    [CostCenterId]       INT             NULL,
    [BillNumber]         VARCHAR (100)   NOT NULL,
    [DocumentDate]       DATETIME        NOT NULL,
    [BillDate]           DATETIME        NOT NULL,
    [FilingUnitId]       INT             NOT NULL,
    [SupplierTypeId]     INT             NOT NULL,
    [Term]               INT             NOT NULL,
    [InvoiceValue]       DECIMAL (18, 2) NOT NULL,
    [Coments]            VARCHAR (MAX)   NULL,
    [AccountPayableId]   INT             NULL,
    CONSTRAINT [PK_Payments_LoadMassiveAccountPayable] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LoadMassiveAccountPayable_AccountPayable] FOREIGN KEY ([AccountPayableId]) REFERENCES [Payments].[AccountPayable] ([Id]),
    CONSTRAINT [FK_LoadMassiveAccountPayable_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_LoadMassiveAccountPayable_DistributionLine] FOREIGN KEY ([DistributionLineId]) REFERENCES [Common].[DistributionLines] ([Id]),
    CONSTRAINT [FK_LoadMassiveAccountPayable_FilingUnit] FOREIGN KEY ([FilingUnitId]) REFERENCES [Payments].[FilingUnit] ([Id]),
    CONSTRAINT [FK_LoadMassiveAccountPayable_LoadMassive] FOREIGN KEY ([LoadMassiveId]) REFERENCES [Payments].[LoadMassive] ([Id]),
    CONSTRAINT [FK_LoadMassiveAccountPayable_Supplier] FOREIGN KEY ([SupplierId]) REFERENCES [Common].[Supplier] ([Id]),
    CONSTRAINT [FK_LoadMassiveAccountPayable_SupplierType] FOREIGN KEY ([SupplierTypeId]) REFERENCES [Common].[SupplierType] ([Id])
);


GO
ALTER TABLE [Payments].[LoadMassiveAccountPayable] NOCHECK CONSTRAINT [FK_LoadMassiveAccountPayable_AccountPayable];


GO
ALTER TABLE [Payments].[LoadMassiveAccountPayable] NOCHECK CONSTRAINT [FK_LoadMassiveAccountPayable_CostCenter];


GO
ALTER TABLE [Payments].[LoadMassiveAccountPayable] NOCHECK CONSTRAINT [FK_LoadMassiveAccountPayable_DistributionLine];


GO
ALTER TABLE [Payments].[LoadMassiveAccountPayable] NOCHECK CONSTRAINT [FK_LoadMassiveAccountPayable_FilingUnit];


GO
ALTER TABLE [Payments].[LoadMassiveAccountPayable] NOCHECK CONSTRAINT [FK_LoadMassiveAccountPayable_LoadMassive];


GO
ALTER TABLE [Payments].[LoadMassiveAccountPayable] NOCHECK CONSTRAINT [FK_LoadMassiveAccountPayable_Supplier];


GO
ALTER TABLE [Payments].[LoadMassiveAccountPayable] NOCHECK CONSTRAINT [FK_LoadMassiveAccountPayable_SupplierType];

