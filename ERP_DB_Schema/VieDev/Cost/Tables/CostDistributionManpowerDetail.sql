CREATE TABLE [Cost].[CostDistributionManpowerDetail] (
    [Id]                                INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DistributionManpowerId]            INT             NOT NULL,
    [ProductionCenterId]                INT             NOT NULL,
    [HoursQuantity]                     INT             NOT NULL,
    [TotalAccrued]                      DECIMAL (18, 2) CONSTRAINT [DF_CostDistributionManpowerDetail_TotalAccrued] DEFAULT ((0)) NOT NULL,
    [TotalProvision]                    DECIMAL (18, 2) CONSTRAINT [DF_CostDistributionManpowerDetail_TotalProvision] DEFAULT ((0)) NOT NULL,
    [TotalEmployerContribution]         DECIMAL (18, 2) CONSTRAINT [DF_CostDistributionManpowerDetail_TotalEmployerContribution] DEFAULT ((0)) NOT NULL,
    [TotalParafiscal]                   DECIMAL (18, 2) CONSTRAINT [DF_CostDistributionManpowerDetail_TotalParafiscal] DEFAULT ((0)) NOT NULL,
    [TotalAccruedRevalued]              DECIMAL (18, 2) NULL,
    [TotalProvisionRevalued]            DECIMAL (18, 2) NULL,
    [TotalEmployerContributionRevalued] DECIMAL (18, 2) NULL,
    [TotalParafiscalRevalued]           DECIMAL (18, 2) NULL,
    CONSTRAINT [PK_CostDistributionManpowerDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostDistributionManpowerDetail_CostDistributionManpower] FOREIGN KEY ([DistributionManpowerId]) REFERENCES [Cost].[CostDistributionManpower] ([Id]),
    CONSTRAINT [FK_CostDistributionManpowerDetail_CostProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [Cost].[CostProductionCenter] ([Id])
);


GO
ALTER TABLE [Cost].[CostDistributionManpowerDetail] NOCHECK CONSTRAINT [FK_CostDistributionManpowerDetail_CostDistributionManpower];


GO
ALTER TABLE [Cost].[CostDistributionManpowerDetail] NOCHECK CONSTRAINT [FK_CostDistributionManpowerDetail_CostProductionCenter];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del campo TotalParafiscal revalorizado a la moneda del módulo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'TotalParafiscalRevalued';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del campo TotalEmployerContribution revalorizado a la moneda del módulo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'TotalEmployerContributionRevalued';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del campo TotalProvision revalorizado a la moneda del módulo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'TotalProvisionRevalued';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del campo TotalAccrued revalorizado a la moneda del módulo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'TotalAccruedRevalued';

