CREATE TABLE [Inventory].[SismedConsolidationBatch] (
    [Id]              INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [StartDate]       DATE          NOT NULL,
    [EndDate]         DATE          NOT NULL,
    [TotalEvaluated]  INT           NOT NULL,
    [TotalReportable] INT           NOT NULL,
    [TotalExcluded]   INT           NOT NULL,
    [ExecutionUser]   VARCHAR (20)  NOT NULL,
    [ExecutionDate]   DATETIME      NOT NULL,
    [TimeStamp]       ROWVERSION    NOT NULL,
    CONSTRAINT [PK_SismedConsolidationBatch] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Encabezado de cada corrida de consolidacion de transacciones reportables SISMED (Circular 021 de 2026). Agrupa el periodo consultado y los totales de transacciones evaluadas, reportables y excluidas, conforme al PBI de Consolidacion de Transacciones Reportables.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SismedConsolidationBatch';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicial del periodo de reporte consultado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SismedConsolidationBatch', @level2type = N'COLUMN', @level2name = N'StartDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha final del periodo de reporte consultado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SismedConsolidationBatch', @level2type = N'COLUMN', @level2name = N'EndDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total de transacciones de medicamentos evaluadas en el periodo (compras y ventas), antes de aplicar exclusiones regulatorias.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SismedConsolidationBatch', @level2type = N'COLUMN', @level2name = N'TotalEvaluated';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total de transacciones evaluadas que resultaron reportables al SISMED (no excluidas por ninguna regla regulatoria).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SismedConsolidationBatch', @level2type = N'COLUMN', @level2name = N'TotalReportable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total de transacciones evaluadas que fueron excluidas del reporte SISMED por alguna de las reglas de la Circular 021 de 2026 (Preparacion Magistral o Traslado Interno).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SismedConsolidationBatch', @level2type = N'COLUMN', @level2name = N'TotalExcluded';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que ejecuto el proceso de consolidacion.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SismedConsolidationBatch', @level2type = N'COLUMN', @level2name = N'ExecutionUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se ejecuto el proceso de consolidacion.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SismedConsolidationBatch', @level2type = N'COLUMN', @level2name = N'ExecutionDate';
GO