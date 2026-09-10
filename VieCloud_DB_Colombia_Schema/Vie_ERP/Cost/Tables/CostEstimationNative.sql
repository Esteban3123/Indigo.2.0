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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total de ventas para distribución secundaria; ingresos del período en fase secundaria de asignación de costos.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'TotalSalesSecondary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de venta para distribucion secundaria', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'TotalSalesSecondary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'TotalSalesSecondary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total de ventas del período según cuentas parametrizadas en el centro de producción; ingresos operativos para análisis rentabilidad.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'TotalSales';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica las ventas del periodo de acuerdo a las cuentas parametrizadas en el centro de producción', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'TotalSales';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'TotalSales';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ajuste contable de traslados/consumos (DECIMAL 18,2); reconciliación movimientos internos vs. contabilidad.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'TransferAccountingAdjustment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor ajustado de consumo por comparación contra los saldos en contabilidad', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'TransferAccountingAdjustment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'TransferAccountingAdjustment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ajuste contable de dispensaciones/suministros (DECIMAL 18,2); reconciliación consumos vs. saldos de inventarios.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'DispensingAccountingAdjustment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor ajustado de suministros por comparación contra los saldos en contabilidad', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'DispensingAccountingAdjustment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'DispensingAccountingAdjustment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ajuste contable de activos fijos (DECIMAL 18,2); reconciliación depreciación vs. saldos de activo fijo.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'FixedAssetAccountingAdjustment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor ajustado de activos fijos por comparación contra los saldos en contabilidad', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'FixedAssetAccountingAdjustment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'FixedAssetAccountingAdjustment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ajuste contable de mano de obra (DECIMAL 18,2); reconciliación nómina vs. saldos contables por diferencias.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'ManPowerAccountingAdjustment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor ajustado de mano de obra por comparación contra los saldos en contabilidad', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'ManPowerAccountingAdjustment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'ManPowerAccountingAdjustment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ajuste contable de gastos generales/costos (DECIMAL 18,2); reconciliación contra saldos de contabilidad general.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'CostAccountingAdjustment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor ajustado de gastos generales por comparación contra los saldos en contabilidad', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'CostAccountingAdjustment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'CostAccountingAdjustment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal (TIMESTAMP) de evento de creación, modificación o registro; control de versión automático del servidor SQL.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación; DATETIME nullable, actualizado en cambios posteriores a creación.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que modificó el registro (VARCHAR 20, nullable, PII); nombre/identificación del profesional que actualizó.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de estimación; marca temporal de generación inicial (DATETIME).', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro (VARCHAR 20, PII); nombre de usuario, cédula o identificación del analista de costos.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Distribución secundaria total (suma: mano obra directa/indirecta + activos fijos + dispensaciones + traslados); asignación final.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'SecondaryDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el total de la distribucion secundaria lo cual es la suma de Mano de obra Directa, Indirecta, Activos Fijos, Dispensaciones y Traslados o Consumos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'SecondaryDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'SecondaryDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Distribución secundaria de traslados/consumos; redistribución final de movimientos internos entre centros productivos.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'SecondaryTransferDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda los valores de las cuentas contables que se registraron en traslados o consumos de la distribucion secundaria', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'SecondaryTransferDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'SecondaryTransferDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Distribución secundaria de dispensaciones y suministros; asignación final de materiales consumidos por unidad funcional.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'SecondaryDispensingDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda los valores de las cuentas contables que se registraron en dispensaciones y/o suministros de la distribucion secundaria', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'SecondaryDispensingDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'SecondaryDispensingDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Distribución secundaria de activos fijos; redistribución de depreciación y mantenimiento en etapa secundaria del costeo.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'SecondaryFixedAssetDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la ditribucion secundaria de Activos Fijos del centro de produccion', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'SecondaryFixedAssetDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'SecondaryFixedAssetDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Distribución secundaria de mano de obra administrativa; asignación final de costos de personal administrativo.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'SecondaryManPowerDistributionInDirect';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la distribucion secundaria de mano de obra del centro de produccion que sea de tipo Administrativo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'SecondaryManPowerDistributionInDirect';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'SecondaryManPowerDistributionInDirect';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Distribución secundaria de mano de obra operativa; asignación final de costos de personal operativo entre servicios.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'SecondaryManPowerDistributionDirect';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la distribucion secundaria de mano de obra del centro de produccion que sea de tipo Operativo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'SecondaryManPowerDistributionDirect';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'SecondaryManPowerDistributionDirect';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Distribución secundaria de gastos calculados/automáticos; redistribución de costos derivados en etapa secundaria.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'SecondaryAutoCostDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la distribucion de elementos del costo de tipo Otros Gastos con distribucion Calculada y Buscada', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'SecondaryAutoCostDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'SecondaryAutoCostDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Distribución secundaria de otros gastos con distribución directa; redistribución de gastos en fase secundaria del costeo.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'SecondaryDirectCostDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la distribucion de elementos del costo de tipo Otros Gastos con distribucion directa', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'SecondaryDirectCostDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'SecondaryDirectCostDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Distribución intermedia; fase de redistribución de costos entre centros antes de distribución secundaria.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'IntermediateDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la Distribución intermedia', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'IntermediateDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'IntermediateDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Distribución inicial (suma: gastos directos + mano de obra + activos fijos); primer nivel de asignación de costos.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'InitialDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la distribucion Inicial la cual es la suma de:  Distribucion de Gastos Directos  Distribucion de Mano de Obra  Distribucion de Activos Fijos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'InitialDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'InitialDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Distribución de traslados/consumos internos y materiales directos; valores de cuentas contables por movimiento de inventarios.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'TransferDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda los valores de las cuentas contables que se registraron en traslados o consumos y Los elementos del costo que sean de tipo materiales directos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'TransferDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'TransferDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Distribución de dispensaciones, suministros y materiales directos consumidos; registrados en cuentas contables del centro.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'DispensingDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda los valores de las cuentas contables que se registraron en dispensaciones y/o suministros y Los elementos del costo que sean de tipo materiales directos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'DispensingDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'DispensingDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Distribución de activos fijos (depreciación, mantenimiento) asignada al centro de producción en período; base de amortización.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'FixedAssetDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la ditribucion de Activos Fijos del centro de produccion', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'FixedAssetDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'FixedAssetDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Distribución de mano de obra indirecta/administrativa del centro; costos de personal de soporte y gestión administrativa.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'ManPowerDistributionInDirect';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la distribucion de mano de obra del centro de produccion que sea de tipo Administrativo y Los elementos del costo que sean de tipo mano de obra indirecta', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'ManPowerDistributionInDirect';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'ManPowerDistributionInDirect';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Distribución de mano de obra directa/operativa del centro; salarios, prestaciones de personal asistencial y administrativo operativo.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'ManPowerDistributionDirect';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la distribucion de mano de obra del centro de produccion que sea de tipo Operativo y Los elementos del costo que sean de tipo mano de obra directa', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'ManPowerDistributionDirect';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'ManPowerDistributionDirect';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Distribución de otros gastos con distribución calculada/automática; elementos de costo derivados de fórmulas paramétrizadas.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'AutoCostDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la distribucion de elementos del costo de tipo Otros Gastos con distribucion Calculada y Buscada', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'AutoCostDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'AutoCostDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Distribución de gastos directos (otros gastos con distribución directa); base para cálculo inicial de costos operativos.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'DirectCostDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la distribucion de elementos del costo de tipo Otros Gastos con distribucion directa', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'DirectCostDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'DirectCostDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro de producción (unidad funcional, departamento, servicio) asignado a la estimación; FK a tabla de centros.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de produccion', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mes de la estimación de costos; período mensual para distribución de gastos por unidad funcional.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'Month';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el mes de la estimacion de costos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'Month';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'Month';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Año de la estimación de costos; período anual para análisis de gastos operativos del centro.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'Year';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el año de la estimacion de costos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'Year';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'Year';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) de la estimación de costos del período y centro de producción.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la estimacion de costos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estimación nativa de costos por centro de producción, mes y año. Concentra los valores distribuidos de costos directos, mano de obra, activos fijos, dispensación, transferencias y ajustes contables en sus etapas inicial, intermedia y secundaria, junto con las ventas totales asociadas al período.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative';
