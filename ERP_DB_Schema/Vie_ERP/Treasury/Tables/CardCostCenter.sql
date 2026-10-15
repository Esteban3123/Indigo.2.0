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


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de costo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CardCostCenter', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad operativa', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CardCostCenter', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la franquicia', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CardCostCenter', @level2type = N'COLUMN', @level2name = N'CardId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CardCostCenter', @level2type = N'COLUMN', @level2name = N'Id';

