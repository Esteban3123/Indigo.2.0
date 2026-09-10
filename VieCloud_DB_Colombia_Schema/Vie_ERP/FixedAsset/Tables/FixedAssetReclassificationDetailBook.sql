CREATE TABLE [FixedAsset].[FixedAssetReclassificationDetailBook] (
    [Id]                                 INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FixedAssetReclassificationDetailId] INT             NOT NULL,
    [LegalBookId]                        INT             NOT NULL,
    [DaysPendingDepreciate]              INT             NOT NULL,
    [DepreciatedDays]                    INT             NOT NULL,
    [Valorization]                       DECIMAL (18, 2) NOT NULL,
    [Devaluation]                        DECIMAL (18, 2) NOT NULL,
    [AdjustedValue]                      DECIMAL (18, 2) NOT NULL,
    [TransactionValue]                   DECIMAL (18, 2) NOT NULL,
    [DepreciatedValue]                   DECIMAL (18, 2) NOT NULL,
    [InflationAdjustmentValue]           DECIMAL (18, 2) NOT NULL,
    [ResidualValue]                      DECIMAL (18, 2) NOT NULL,
    [HistoricalValue]                    DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_FixedAssetReclassificationDetailBook] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetReclassificationDetailBook_FixedAssetReclassificationDetail] FOREIGN KEY ([FixedAssetReclassificationDetailId]) REFERENCES [FixedAsset].[FixedAssetReclassificationDetail] ([Id]),
    CONSTRAINT [FK_FixedAssetReclassificationDetailBook_LegalBook] FOREIGN KEY ([LegalBookId]) REFERENCES [GeneralLedger].[LegalBook] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetReclassificationDetailBook] NOCHECK CONSTRAINT [FK_FixedAssetReclassificationDetailBook_LegalBook];




GO



GO
ALTER TABLE [FixedAsset].[FixedAssetReclassificationDetailBook] NOCHECK CONSTRAINT [FK_FixedAssetReclassificationDetailBook_LegalBook];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor histórico del activo fijo por libro contable (DECIMAL 18,2). Costo original o base de depreciación registrada en el libro legal al momento de reclasificación.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'HistoricalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Valor Historico del activo por Libro', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'HistoricalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'HistoricalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor residual o salvamento pendiente por depreciar al momento de reclasificación (DECIMAL 18,2). Monto que aún falta depreciar del activo fijo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'ResidualValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor que falta por depreciar al momento de realizar la reclasificación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'ResidualValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'ResidualValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ajuste por inflación acumulado al activo fijo en reclasificación (DECIMAL 18,2). Revalorización acumulada por inflación según libro contable legal.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'InflationAdjustmentValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Valor del Ajuste por Inflacion Acumulado al momento de realizar la reclasificación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'InflationAdjustmentValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'InflationAdjustmentValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor depreciado acumulado del activo fijo al momento de reclasificación (DECIMAL 18,2). Monto total de depreciación registrada hasta la fecha de reclasificación.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'DepreciatedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor que se ha depreciado al momento de realizar la reclasificación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'DepreciatedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'DepreciatedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total de transacciones y valorizaciones del activo en reclasificación (DECIMAL 18,2). Suma de ajustes, incrementos o cambios de valor registrados.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'TransactionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor de las transacciones, es decir las valorizaciones al momento de realizar la reclasificación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'TransactionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'TransactionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor ajustado del activo fijo por libro en reclasificación (DECIMAL 18,2). Importe resultante tras aplicar ajustes contables y depreciación.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'AdjustedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'valor de ajuste', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'AdjustedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'AdjustedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Desvalorizaciones acumuladas del activo fijo en reclasificación (DECIMAL 18,2). Total de reducciones de valor registradas por deterioro o cambios contables.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'Devaluation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica todas la desvalorizaciones que se le han realizado al momento de realizar la reclasificación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'Devaluation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'Devaluation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valorizaciones acumuladas del activo fijo en reclasificación (DECIMAL 18,2). Total de incrementos de valor registrados por revalorización o ajustes positivos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'Valorization';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica todas las valorizaciones que se le han realizado al momento de realizar la reclasificación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'Valorization';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'Valorization';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días ya depreciados del activo fijo al momento de reclasificación (INT). Cantidad de días para los que se ha calculado y registrado depreciación.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'DepreciatedDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica los dias que ya fueron depreciados al momento de realizar la reclasificación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'DepreciatedDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'DepreciatedDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días pendientes por depreciar del activo fijo en reclasificación (INT). Cantidad de días restantes para completar el ciclo de depreciación del activo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'DaysPendingDepreciate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica los dias pendientes por depreciar al momento de realizar la reclasificación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'DaysPendingDepreciate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'DaysPendingDepreciate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del libro contable legal vinculado (INT, FK a GeneralLedger.LegalBook). Referencia al registro contable oficial del activo reclasificado.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'LegalBookId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Libro Contable', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'LegalBookId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'LegalBookId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle de reclasificación de activo fijo (INT, FK a FixedAssetReclassificationDetail). Referencia a la línea específica de reclasificación.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'FixedAssetReclassificationDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la reclasificación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'FixedAssetReclassificationDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'FixedAssetReclassificationDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental de la tabla (INT IDENTITY). Clave primaria de la tabla de detalle de reclasificación por libro.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valores contables por libro legal de cada detalle de reclasificación de activos fijos. Registra la información de depreciación, valorización, ajustes por inflación y valores históricos y residuales asociados a la reclasificación de un activo fijo en cada libro contable (NIIF, fiscal, etc.).', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetailBook';
