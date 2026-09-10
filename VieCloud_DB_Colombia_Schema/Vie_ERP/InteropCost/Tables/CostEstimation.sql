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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de tiempo automática de SQL Server (TIMESTAMP), registra cada cambio en el registro para auditoría y sincronización de datos.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca de tiempo', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro de estimación de costos (DATETIME, nullable para registros nuevos).', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación del registro (VARCHAR 20, nullable si no ha sido editado).', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de estimación de costos (DATETIME, auditoría de creación).', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro de estimación de costos (VARCHAR 20, auditoría de creación).', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total de distribución secundaria de costos: suma de mano de obra directa/indirecta, activos fijos, dispensaciones, traslados y distribución inicial del centro de producción (DECIMAL 18,2).', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'SecondaryDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el total de la distribucion secundaria lo cual es la suma de Mano de obra Directa, Indirecta, Activos Fijos, Dispensaciones y Traslados o Consumos y su distribucion inicial', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'SecondaryDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'SecondaryDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valores contables de traslados o consumos en distribución secundaria; corresponde a movimientos de materiales directos entre centros (DECIMAL 18,2).', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'SecondaryTransferDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda los valores de las cuentas contables que se registraron en traslados o consumos de la distribucion secundaria', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'SecondaryTransferDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'SecondaryTransferDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valores contables de dispensaciones y suministros en distribución secundaria; registra salida de materiales directos de farmacia/almacén (DECIMAL 18,2).', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'SecondaryDispensingDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda los valores de las cuentas contables que se registraron en dispensaciones y/o suministros de la distribucion secundaria', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'SecondaryDispensingDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'SecondaryDispensingDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Distribución secundaria de depreciación y amortización de activos fijos asignada al centro de producción (DECIMAL 18,2).', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'SecondaryFixedAssetDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la ditribucion secundaria de Activos Fijos del centro de produccion', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'SecondaryFixedAssetDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'SecondaryFixedAssetDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Distribución secundaria de mano de obra indirecta (administrativa, supervisión, soporte) del centro de producción (DECIMAL 18,2).', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'SecondaryManPowerDistributionInDirect';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la distribucion secundaria de mano de obra del centro de produccion que sea de tipo Administrativo', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'SecondaryManPowerDistributionInDirect';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'SecondaryManPowerDistributionInDirect';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Distribución secundaria de mano de obra directa (operativa, clínica, asistencial) del centro de producción (DECIMAL 18,2).', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'SecondaryManPowerDistributionDirect';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la distribucion secundaria de mano de obra del centro de produccion que sea de tipo Operativo', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'SecondaryManPowerDistributionDirect';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'SecondaryManPowerDistributionDirect';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Distribución secundaria de otros gastos calculados automáticamente con base en factores de distribución buscados (DECIMAL 18,2).', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'SecondaryAutoCostDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la distribucion de elementos del costo de tipo Otros Gastos con distribucion Calculada y Buscada', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'SecondaryAutoCostDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'SecondaryAutoCostDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Distribución secundaria de otros gastos asignados directamente (sin prorrateo) al centro de producción (DECIMAL 18,2).', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'SecondaryDirectCostDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la distribucion de elementos del costo de tipo Otros Gastos con distribucion directa', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'SecondaryDirectCostDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'SecondaryDirectCostDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total de distribución intermedia: asignación de costos de centros auxiliares a centros productivos antes de distribución final (DECIMAL 18,2).', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'IntermediateDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total Distribución intermedia', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'IntermediateDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'IntermediateDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Distribución inicial: suma de gastos directos, mano de obra y activos fijos antes de prorrateos secundarios (DECIMAL 18,2).', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'InitialDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la distribucion Inicial la cual es la suma de:  Distribucion de Gastos Directos  Distribucion de Mano de Obra  Distribucion de Activos Fijos', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'InitialDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'InitialDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valores contables de traslados o consumos: movimientos de materiales directos entre centros y depósitos (DECIMAL 18,2).', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'TransferDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda los valores de las cuentas contables que se registraron en traslados o consumos y Los elementos del costo que sean de tipo materiales directos', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'TransferDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'TransferDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valores contables de dispensaciones y suministros: salida de materiales directos de farmacia, almacén o depósito (DECIMAL 18,2).', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'DispensingDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda los valores de las cuentas contables que se registraron en dispensaciones y/o suministros y Los elementos del costo que sean de tipo materiales directos', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'DispensingDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'DispensingDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Distribución de depreciación y amortización de activos fijos (bienes muebles e inmuebles) asignada al centro de producción (DECIMAL 18,2).', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'FixedAssetDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la ditribucion de Activos Fijos del centro de produccion', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'FixedAssetDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'FixedAssetDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Distribución de mano de obra indirecta (personal administrativo, supervisión, soporte) y costos indirectos de personal del centro (DECIMAL 18,2).', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'ManPowerDistributionInDirect';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la distribucion de mano de obra del centro de produccion que sea de tipo Administrativo y Los elementos del costo que sean de tipo mano de obra indirecta', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'ManPowerDistributionInDirect';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'ManPowerDistributionInDirect';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Distribución de mano de obra directa (personal operativo, clínico, asistencial) y costos directos de personal del centro (DECIMAL 18,2).', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'ManPowerDistributionDirect';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la distribucion de mano de obra del centro de produccion que sea de tipo Operativo y Los elementos del costo que sean de tipo mano de obra directa', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'ManPowerDistributionDirect';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'ManPowerDistributionDirect';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ajuste contable aplicado a depreciación de activos fijos para reconciliar costos estimados con registros contables (DECIMAL 18,2, default 0).', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'FixedAssetAccountingAdjustment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor ajustado por Activos Fijos para coincidir con contabilidad', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'FixedAssetAccountingAdjustment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'FixedAssetAccountingAdjustment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ajuste contable aplicado a costos de mano de obra para reconciliar estimación de costos con contabilidad general (DECIMAL 18,2, default 0).', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'ManPowerAccountingAdjustment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor con el que se ajusto los costos por mano de obra para ser iguales con contabilidad', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'ManPowerAccountingAdjustment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'ManPowerAccountingAdjustment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ajuste contable aplicado a gastos generales y otros costos para reconciliar estimación con registros contables (DECIMAL 18,2, default 0).', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'CostAccountingAdjustment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor con el que se ajustaron los gastos generales para ser iguales con contabilidad', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'CostAccountingAdjustment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'CostAccountingAdjustment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Distribución de otros gastos calculados automáticamente usando factores de prorrateo predeterminados (DECIMAL 18,2).', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'AutoCostDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la distribucion de elementos del costo de tipo Otros Gastos con distribucion Calculada y Buscada', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'AutoCostDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'AutoCostDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Distribución de otros gastos asignados directamente sin prorrateo al centro de producción (DECIMAL 18,2).', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'DirectCostDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la distribucion de elementos del costo de tipo Otros Gastos con distribucion directa', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'DirectCostDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'DirectCostDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro de producción (FK, INT); vincula estimación a servicio clínico: urgencia, hospitalización, consulta, laboratorio, etc.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de produccion', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mes de la estimación de costos (INT, 1-12); período mensual para análisis y control de costos por centro.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'Month';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el mes de la estimacion de costos', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'Month';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'Month';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Año de la estimación de costos (INT); período anual para presupuestación y análisis histórico de costos.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'Year';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el año de la estimacion de costos', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'Year';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'Year';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la estimación de costos (INT IDENTITY, PK); clave principal para auditoría y trazabilidad mensual por centro.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la estimacion de costos', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estimación y distribución de costos por centro de producción para cada período mensual. Registra los montos distribuidos por tipo de costo (directo, activos fijos, mano de obra, dispensación, traslados, distribuciones secundarias y ajustes contables) utilizados en la contabilidad de costos hospitalarios.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation';
