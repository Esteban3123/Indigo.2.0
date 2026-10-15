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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del concepto de la retención', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'RetentionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Naturaleza del movimiento, cuando NO se genera CxP:
1: Débito
2: Crédito', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del proceso de los detalles cuando NO se genera CxP', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'ProcessDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concepto de nómina de los detalles cuando NO se genera CxP', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'PayrollConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones de los detalles cuando NO se genera CxP', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Base de la retención de los detalles cuando NO se genera CxP', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'BaseRetention';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor facturado de los detalles cuando NO se genera CxP', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'InvoicedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del Empleado', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del cargo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'PositionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Horas contratadas', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'ManpowerHoursContracted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del producto', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Titulo Profesional', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'JobTitle';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Horas de mano de obra', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'HoursManpower';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del tercero', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del iva', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'IvaValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor Base', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'BaseValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la distribucion', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del costo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'CostValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad registrada', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'Count';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el porcentaje que le corresponde al centro de costo despues de realizar laq distribucion', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'Percentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad de medida, solo pueden ser de tipo 4 - sistema de costos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de costo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de produccion', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de la distribucion por gasto directo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'DistributionDirectCostId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la distribucion por gasto directo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'Id';

