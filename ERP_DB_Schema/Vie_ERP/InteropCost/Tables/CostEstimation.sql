CREATE TABLE [InteropCost].[CostEstimation] (
    [Id]                                    INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Year]                                  INT             NOT NULL,
    [Month]                                 INT             NOT NULL,
    [ProductionCenterId]                    INT             NOT NULL,
    [DirectCostDistribution]                DECIMAL (18, 2) NOT NULL,
    [AutoCostDistribution]                  DECIMAL (18, 2) NOT NULL,
    [CostAccountingAdjustment]              DECIMAL (18, 2) CONSTRAINT [DF_CostEstimation_AccountingAdjustment] DEFAULT ((0)) NOT NULL,
    [ManPowerAccountingAdjustment]          DECIMAL (18, 2) CONSTRAINT [DF_CostEstimation_ManPowerAccountingAdjustment] DEFAULT ((0)) NOT NULL,
    [FixedAssetAccountingAdjustment]        DECIMAL (18, 2) CONSTRAINT [DF_CostEstimation_FixedAssetAccountingAdjustment] DEFAULT ((0)) NOT NULL,
    [ManPowerDistributionDirect]            DECIMAL (18, 2) CONSTRAINT [DF_CostEstimation_ManPowerDistributionDirect] DEFAULT ((0)) NOT NULL,
    [ManPowerDistributionInDirect]          DECIMAL (18, 2) CONSTRAINT [DF_CostEstimation_ManPowerDistributionInDirect] DEFAULT ((0)) NOT NULL,
    [FixedAssetDistribution]                DECIMAL (18, 2) NOT NULL,
    [DispensingDistribution]                DECIMAL (18, 2) NOT NULL,
    [TransferDistribution]                  DECIMAL (18, 2) NOT NULL,
    [InitialDistribution]                   DECIMAL (18, 2) NOT NULL,
    [IntermediateDistribution]              DECIMAL (18, 2) NOT NULL,
    [SecondaryDirectCostDistribution]       DECIMAL (18, 2) CONSTRAINT [DF_CostEstimation_SecondaryDirectCostDistribution] DEFAULT ((0)) NOT NULL,
    [SecondaryAutoCostDistribution]         DECIMAL (18, 2) CONSTRAINT [DF_CostEstimation_SecondaryAutoCostDistribution] DEFAULT ((0)) NOT NULL,
    [SecondaryManPowerDistributionDirect]   DECIMAL (18, 2) CONSTRAINT [DF_CostEstimation_ManPowerDistributionDirect1] DEFAULT ((0)) NOT NULL,
    [SecondaryManPowerDistributionInDirect] DECIMAL (18, 2) CONSTRAINT [DF_CostEstimation_ManPowerDistributionInDirect1] DEFAULT ((0)) NOT NULL,
    [SecondaryFixedAssetDistribution]       DECIMAL (18, 2) CONSTRAINT [DF_CostEstimation_SecondaryFixedAssetDistribution] DEFAULT ((0)) NOT NULL,
    [SecondaryDispensingDistribution]       DECIMAL (18, 2) CONSTRAINT [DF_CostEstimation_SecondaryDispensingDistribution] DEFAULT ((0)) NOT NULL,
    [SecondaryTransferDistribution]         DECIMAL (18, 2) CONSTRAINT [DF_CostEstimation_SecondaryTransferDistribution] DEFAULT ((0)) NOT NULL,
    [SecondaryDistribution]                 DECIMAL (18, 2) NOT NULL,
    [CreationUser]                          VARCHAR (20)    NOT NULL,
    [CreationDate]                          DATETIME        NOT NULL,
    [ModificationUser]                      VARCHAR (20)    NULL,
    [ModificationDate]                      DATETIME        NULL,
    [TimeStamp]                             ROWVERSION      NOT NULL,
    CONSTRAINT [PK_CostEstimation] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de tiempo', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el total de la distribucion secundaria lo cual es la suma de Mano de obra Directa, Indirecta, Activos Fijos, Dispensaciones y Traslados o Consumos y su distribucion inicial', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'SecondaryDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda los valores de las cuentas contables que se registraron en traslados o consumos de la distribucion secundaria', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'SecondaryTransferDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda los valores de las cuentas contables que se registraron en dispensaciones y/o suministros de la distribucion secundaria', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'SecondaryDispensingDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la ditribucion secundaria de Activos Fijos del centro de produccion', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'SecondaryFixedAssetDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la distribucion secundaria de mano de obra del centro de produccion que sea de tipo Administrativo', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'SecondaryManPowerDistributionInDirect';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la distribucion secundaria de mano de obra del centro de produccion que sea de tipo Operativo', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'SecondaryManPowerDistributionDirect';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la distribucion de elementos del costo de tipo Otros Gastos con distribucion Calculada y Buscada', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'SecondaryAutoCostDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la distribucion de elementos del costo de tipo Otros Gastos con distribucion directa', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'SecondaryDirectCostDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Total Distribución intermedia', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'IntermediateDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la distribucion Inicial la cual es la suma de:  Distribucion de Gastos Directos  Distribucion de Mano de Obra  Distribucion de Activos Fijos', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'InitialDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda los valores de las cuentas contables que se registraron en traslados o consumos y Los elementos del costo que sean de tipo materiales directos', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'TransferDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda los valores de las cuentas contables que se registraron en dispensaciones y/o suministros y Los elementos del costo que sean de tipo materiales directos', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'DispensingDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la ditribucion de Activos Fijos del centro de produccion', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'FixedAssetDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la distribucion de mano de obra del centro de produccion que sea de tipo Administrativo y Los elementos del costo que sean de tipo mano de obra indirecta', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'ManPowerDistributionInDirect';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la distribucion de mano de obra del centro de produccion que sea de tipo Operativo y Los elementos del costo que sean de tipo mano de obra directa', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'ManPowerDistributionDirect';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor ajustado por Activos Fijos para coincidir con contabilidad', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'FixedAssetAccountingAdjustment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor con el que se ajusto los costos por mano de obra para ser iguales con contabilidad', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'ManPowerAccountingAdjustment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor con el que se ajustaron los gastos generales para ser iguales con contabilidad', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'CostAccountingAdjustment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la distribucion de elementos del costo de tipo Otros Gastos con distribucion Calculada y Buscada', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'AutoCostDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la distribucion de elementos del costo de tipo Otros Gastos con distribucion directa', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'DirectCostDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de produccion', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el mes de la estimacion de costos', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'Month';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el año de la estimacion de costos', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'Year';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la estimacion de costos', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'Id';

