CREATE TABLE [InteropCost].[DistributionManpowerDetail] (
    [Id]                        INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DistributionManpowerId]    INT             NOT NULL,
    [ProductionCenterId]        INT             NOT NULL,
    [HoursQuantity]             INT             NOT NULL,
    [TotalAccrued]              NUMERIC (18, 2) CONSTRAINT [DF_DistributionManpowerDetail_TotalAccrued] DEFAULT ((0)) NOT NULL,
    [TotalProvision]            NUMERIC (18, 2) CONSTRAINT [DF_DistributionManpowerDetail_TotalProvision] DEFAULT ((0)) NOT NULL,
    [TotalEmployerContribution] NUMERIC (18, 2) CONSTRAINT [DF_DistributionManpowerDetail_TotalEmployerContribution] DEFAULT ((0)) NOT NULL,
    [TotalParafiscal]           NUMERIC (18, 2) CONSTRAINT [DF_DistributionManpowerDetail_TotalParafiscal] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_DistributionManpowerDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DistributionManpowerDetail_DistributionManpower] FOREIGN KEY ([DistributionManpowerId]) REFERENCES [InteropCost].[DistributionManpower] ([Id]),
    CONSTRAINT [FK_DistributionManpowerDetail_ProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [InteropCost].[ProductionCenter] ([Id])
);


GO
ALTER TABLE [InteropCost].[DistributionManpowerDetail] NOCHECK CONSTRAINT [FK_DistributionManpowerDetail_DistributionManpower];


GO
ALTER TABLE [InteropCost].[DistributionManpowerDetail] NOCHECK CONSTRAINT [FK_DistributionManpowerDetail_ProductionCenter];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el total de los parafiscales del empleado en el periodo seleccionado', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'TotalParafiscal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el total de los aportes patronales', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'TotalEmployerContribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el total de las provisiones del periodo actual', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'TotalProvision';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el total devengado (Sin ningun tipo de deducciones) del empleado en el periodo especifico', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'TotalAccrued';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Horas trabajadas del empleado', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'HoursQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de produccion', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de la distribucion', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'DistributionManpowerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la distribucion de la mano de obra', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'Id';

