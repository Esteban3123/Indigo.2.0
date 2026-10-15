CREATE TABLE [Cost].[CostEstimationNative] (
    [Id]                                    INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Year]                                  INT             NOT NULL,
    [Month]                                 INT             NOT NULL,
    [ProductionCenterId]                    INT             NOT NULL,
    [DirectCostDistribution]                DECIMAL (18, 2) CONSTRAINT [DF_CostEstimationNative_DirectCostDistribution] DEFAULT ((0)) NOT NULL,
    [AutoCostDistribution]                  DECIMAL (18, 2) CONSTRAINT [DF_CostEstimationNative_AutoCostDistribution] DEFAULT ((0)) NOT NULL,
    [ManPowerDistributionDirect]            DECIMAL (18, 2) CONSTRAINT [DF_CostEstimationNative_ManPowerDistributionDirect] DEFAULT ((0)) NOT NULL,
    [ManPowerDistributionInDirect]          DECIMAL (18, 2) CONSTRAINT [DF_CostEstimationNative_ManPowerDistributionInDirect] DEFAULT ((0)) NOT NULL,
    [FixedAssetDistribution]                DECIMAL (18, 2) CONSTRAINT [DF_CostEstimationNative_FixedAssetDistribution] DEFAULT ((0)) NOT NULL,
    [DispensingDistribution]                DECIMAL (18, 2) CONSTRAINT [DF_CostEstimationNative_DispensingDistribution] DEFAULT ((0)) NOT NULL,
    [TransferDistribution]                  DECIMAL (18, 2) CONSTRAINT [DF_CostEstimationNative_TransferDistribution] DEFAULT ((0)) NOT NULL,
    [InitialDistribution]                   DECIMAL (18, 2) CONSTRAINT [DF_CostEstimationNative_InitialDistribution] DEFAULT ((0)) NOT NULL,
    [IntermediateDistribution]              DECIMAL (18, 2) CONSTRAINT [DF_CostEstimationNative_IntermediateDistribution] DEFAULT ((0)) NOT NULL,
    [SecondaryDirectCostDistribution]       DECIMAL (18, 2) CONSTRAINT [DF_CostEstimationNative_SecondaryDirectCostDistribution] DEFAULT ((0)) NOT NULL,
    [SecondaryAutoCostDistribution]         DECIMAL (18, 2) CONSTRAINT [DF_CostEstimationNative_SecondaryAutoCostDistribution] DEFAULT ((0)) NOT NULL,
    [SecondaryManPowerDistributionDirect]   DECIMAL (18, 2) CONSTRAINT [DF_CostEstimationNative_SecondaryManPowerDistributionDirect] DEFAULT ((0)) NOT NULL,
    [SecondaryManPowerDistributionInDirect] DECIMAL (18, 2) CONSTRAINT [DF_CostEstimationNative_SecondaryManPowerDistributionInDirect] DEFAULT ((0)) NOT NULL,
    [SecondaryFixedAssetDistribution]       DECIMAL (18, 2) CONSTRAINT [DF_CostEstimationNative_SecondaryFixedAssetDistribution] DEFAULT ((0)) NOT NULL,
    [SecondaryDispensingDistribution]       DECIMAL (18, 2) CONSTRAINT [DF_CostEstimationNative_SecondaryDispensingDistribution] DEFAULT ((0)) NOT NULL,
    [SecondaryTransferDistribution]         DECIMAL (18, 2) CONSTRAINT [DF_CostEstimationNative_SecondaryTransferDistribution] DEFAULT ((0)) NOT NULL,
    [SecondaryDistribution]                 DECIMAL (18, 2) CONSTRAINT [DF_CostEstimationNative_SecondaryDistribution] DEFAULT ((0)) NOT NULL,
    [CreationUser]                          VARCHAR (20)    NOT NULL,
    [CreationDate]                          DATETIME        NOT NULL,
    [ModificationUser]                      VARCHAR (20)    NULL,
    [ModificationDate]                      DATETIME        NULL,
    [TimeStamp]                             ROWVERSION      NOT NULL,
    [CostAccountingAdjustment]              DECIMAL (18, 2) CONSTRAINT [DF_CostEstimationNative_CostAccountingAdjustment] DEFAULT ((0)) NOT NULL,
    [ManPowerAccountingAdjustment]          DECIMAL (18, 2) CONSTRAINT [DF_CostEstimationNative_ManPowerAccountingAdjustment] DEFAULT ((0)) NOT NULL,
    [FixedAssetAccountingAdjustment]        DECIMAL (18, 2) CONSTRAINT [DF_CostEstimationNative_FixedAssetAccountingAdjustment] DEFAULT ((0)) NOT NULL,
    [DispensingAccountingAdjustment]        DECIMAL (18, 2) CONSTRAINT [DF_CostEstimationNative_DispensingAccountingAdjustment] DEFAULT ((0)) NOT NULL,
    [TransferAccountingAdjustment]          DECIMAL (18, 2) CONSTRAINT [DF_CostEstimationNative_TransferAccountingAdjustment] DEFAULT ((0)) NOT NULL,
    [TotalSales]                            DECIMAL (18, 2) CONSTRAINT [DF_CostEstimationNative_TotalSales] DEFAULT ((0)) NOT NULL,
    [TotalSalesSecondary]                   DECIMAL (18, 2) CONSTRAINT [DF_CostEstimationNative_TotalSalesSecondary] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_CostEstimationNativeNative] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_CostEstimationNative_Year_Month_SecondaryDistribution]
    ON [Cost].[CostEstimationNative]([Year], [Month], [SecondaryDistribution]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de venta para distribucion secundaria', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'TotalSalesSecondary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica las ventas del periodo de acuerdo a las cuentas parametrizadas en el centro de producción', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'TotalSales';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor ajustado de consumo por comparación contra los saldos en contabilidad', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'TransferAccountingAdjustment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor ajustado de suministros por comparación contra los saldos en contabilidad', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'DispensingAccountingAdjustment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor ajustado de activos fijos por comparación contra los saldos en contabilidad', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'FixedAssetAccountingAdjustment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor ajustado de mano de obra por comparación contra los saldos en contabilidad', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'ManPowerAccountingAdjustment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor ajustado de gastos generales por comparación contra los saldos en contabilidad', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'CostAccountingAdjustment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el total de la distribucion secundaria lo cual es la suma de Mano de obra Directa, Indirecta, Activos Fijos, Dispensaciones y Traslados o Consumos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'SecondaryDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda los valores de las cuentas contables que se registraron en traslados o consumos de la distribucion secundaria', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'SecondaryTransferDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda los valores de las cuentas contables que se registraron en dispensaciones y/o suministros de la distribucion secundaria', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'SecondaryDispensingDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la ditribucion secundaria de Activos Fijos del centro de produccion', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'SecondaryFixedAssetDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la distribucion secundaria de mano de obra del centro de produccion que sea de tipo Administrativo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'SecondaryManPowerDistributionInDirect';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la distribucion secundaria de mano de obra del centro de produccion que sea de tipo Operativo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'SecondaryManPowerDistributionDirect';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la distribucion de elementos del costo de tipo Otros Gastos con distribucion Calculada y Buscada', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'SecondaryAutoCostDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la distribucion de elementos del costo de tipo Otros Gastos con distribucion directa', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'SecondaryDirectCostDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la Distribución intermedia', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'IntermediateDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la distribucion Inicial la cual es la suma de:  Distribucion de Gastos Directos  Distribucion de Mano de Obra  Distribucion de Activos Fijos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'InitialDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda los valores de las cuentas contables que se registraron en traslados o consumos y Los elementos del costo que sean de tipo materiales directos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'TransferDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda los valores de las cuentas contables que se registraron en dispensaciones y/o suministros y Los elementos del costo que sean de tipo materiales directos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'DispensingDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la ditribucion de Activos Fijos del centro de produccion', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'FixedAssetDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la distribucion de mano de obra del centro de produccion que sea de tipo Administrativo y Los elementos del costo que sean de tipo mano de obra indirecta', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'ManPowerDistributionInDirect';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la distribucion de mano de obra del centro de produccion que sea de tipo Operativo y Los elementos del costo que sean de tipo mano de obra directa', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'ManPowerDistributionDirect';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la distribucion de elementos del costo de tipo Otros Gastos con distribucion Calculada y Buscada', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'AutoCostDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la distribucion de elementos del costo de tipo Otros Gastos con distribucion directa', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'DirectCostDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de produccion', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el mes de la estimacion de costos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'Month';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el año de la estimacion de costos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'Year';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la estimacion de costos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'Id';

