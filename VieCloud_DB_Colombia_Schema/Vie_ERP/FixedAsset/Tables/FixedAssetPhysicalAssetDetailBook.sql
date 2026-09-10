CREATE TABLE [FixedAsset].[FixedAssetPhysicalAssetDetailBook] (
    [Id]                           INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PhysicalAssetId]              INT             NOT NULL,
    [LegalBookId]                  INT             NOT NULL,
    [LifeTime]                     INT             NOT NULL,
    [UnitLifeTime]                 TINYINT         NOT NULL,
    [ValorizationDays]             INT             CONSTRAINT [DF_FixedAssetPhysicalAssetDetailBook_ValorizationDays] DEFAULT ((0)) NOT NULL,
    [DaysPendingDepreciate]        INT             NOT NULL,
    [DepreciatedDays]              INT             NOT NULL,
    [DepreciationType]             TINYINT         CONSTRAINT [DF_FixedAssetPhysicalAssetDetailBook_DepreciationType] DEFAULT ((1)) NOT NULL,
    [TotalProductionUnit]          NUMERIC (18)    CONSTRAINT [DF_FixedAssetPhysicalAssetDetailBook_TotalProductionUnit] DEFAULT ((0)) NOT NULL,
    [PercentageRescue]             NUMERIC (5, 2)  CONSTRAINT [DF_FixedAssetPhysicalAssetDetailBook_PercentageRescue] DEFAULT ((0)) NOT NULL,
    [Valorization]                 DECIMAL (18, 2) NOT NULL,
    [Devaluation]                  DECIMAL (18, 2) NOT NULL,
    [AdjustedValue]                DECIMAL (18, 2) NOT NULL,
    [TransactionValue]             DECIMAL (18, 2) CONSTRAINT [DF_FixedAssetPhysicalAssetDetailBook_TransactionValue] DEFAULT ((0)) NOT NULL,
    [DepreciatedValue]             DECIMAL (18, 2) NOT NULL,
    [DepreciatedValuePart]         DECIMAL (18, 2) NOT NULL,
    [InflationAdjustmentValue]     DECIMAL (18, 2) CONSTRAINT [DF_FixedAssetPhysicalAssetDetailBook_InflationAdjustmentValue] DEFAULT ((0)) NOT NULL,
    [ResidualValue]                DECIMAL (18, 2) NOT NULL,
    [ResidualValuePart]            DECIMAL (18, 2) NOT NULL,
    [HistoricalValue]              DECIMAL (18, 2) CONSTRAINT [DF_FixedAssetPhysicalAssetDetailBook_HistoricalValue] DEFAULT ((0)) NOT NULL,
    [ApplyMinimunAmount]           BIT             CONSTRAINT [DF_FixedAssetPhysicalAssetDetailBook_ApplyMinimunAmount] DEFAULT ((0)) NOT NULL,
    [FinantialDiscountDepreciated] DECIMAL (18, 2) CONSTRAINT [DF__FixedAsse__Finan__73580D95] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_FixedAssetPhysicalAssetDetailBook__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_FixedAsset_ResidualValue_Minimo] CHECK ([DepreciationType]<>(3) OR [ResidualValue]>=([HistoricalValue]*[PercentageRescue])/(100.0)),
    CONSTRAINT [CK_FixedAssetPhysicalAssetDetailBook_ValidateHistoricalValue] CHECK ([HistoricalValue]=(([DepreciatedValue]+[ResidualValue])-[TransactionValue])),
    CONSTRAINT [FK_FixedAssetPhysicalAssetDetailBook_FixedAssetPhysicalAsset] FOREIGN KEY ([PhysicalAssetId]) REFERENCES [FixedAsset].[FixedAssetPhysicalAsset] ([Id]),
    CONSTRAINT [FK_FixedAssetPhysicalAssetDetailBook_LegalBook] FOREIGN KEY ([LegalBookId]) REFERENCES [GeneralLedger].[LegalBook] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetPhysicalAssetDetailBook] NOCHECK CONSTRAINT [CK_FixedAssetPhysicalAssetDetailBook_ValidateHistoricalValue];


GO
ALTER TABLE [FixedAsset].[FixedAssetPhysicalAssetDetailBook] NOCHECK CONSTRAINT [FK_FixedAssetPhysicalAssetDetailBook_LegalBook];




GO



GO



GO
ALTER TABLE [FixedAsset].[FixedAssetPhysicalAssetDetailBook] NOCHECK CONSTRAINT [FK_FixedAssetPhysicalAssetDetailBook_LegalBook];


GO 



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_FixedAssetPhysicalAssetDetailBook__PhysicalAssetId__LegalBookId]
    ON [FixedAsset].[FixedAssetPhysicalAssetDetailBook]([PhysicalAssetId] ASC, [LegalBookId] ASC);


GO
CREATE TRIGGER [FixedAsset].[tgg_DetectarEliminacion]
   ON  [FixedAsset].[FixedAssetPhysicalAssetDetailBook] 
   AFTER DELETE
AS 
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from<br>
    -- interfering with SELECT statements.<br>
    SET NOCOUNT ON;

        declare @count as integer = 0 

        /*select @count = count(*)  from FixedAsset.FixedAssetPhysicalAssetDetailBook d inner join FixedAsset.FixedAssetPhysicalAsset c on c.Id = d.PhysicalAssetId <br>
        where c.Depreciate = 1 and d.Id in (SELECT deleted.id FROM deleted)   */

        select @count = count(*)  
		from deleted d 
		inner join FixedAsset.FixedAssetPhysicalAsset c on c.Id = d.PhysicalAssetId 
        where c.Depreciate = 1 and (select count(*) from FixedAsset.FixedAssetDepreciationDetail where FixedAssetPhysicalAssetDetailBookId = d.Id) > 0

        --select @count<br>
    -- Insert statements for trigger here<br>

    if @count > 0 begin
            ;THROW 51000, 'Error generado por control de eliminacion desde trigger', 1
/*    end        else begin<br>
            select 'Error generado por control de eliminacion desde trigger. :) '<br>
            rollback */
    end 
END
GO
/* ==============================================================================================================
-- Author: Miguel Angel Ruiz (2026-05-07)
-- Modificación: DBA Senior / Optimización Arquitectónica

-- Análisis de Causa Raíz (Por qué colapsaba la BD):
-- 1. Falla de Lógica en Lote (SELECT TOP 1): En SQL Server los triggers se disparan por instrucción (Statement), 
--    no por fila. Al usar 'TOP 1' sobre la tabla INSERTED durante un proceso Batch masivo, el trigger evaluaba 
--    un solo activo al azar e ignoraba el resto del lote, generando falsos positivos/negativos.
-- 2. Bloqueos y Deadlocks: Ejecutar lecturas históricas agregadas (SUM) dentro de un trigger AFTER UPDATE 
--    mantenía la tabla principal bloqueada (Exclusive Locks) estrangulando la concurrencia del ERP.
-- 3. Falla de Predicate Pushdown: El uso de tablas derivadas con GROUP BY antes del LEFT JOIN obligaba 
--    al optimizador a escanear y sumar la depreciación histórica de TODOS los activos de la compañía 
--    en la memoria RAM (TempDB) para cada ejecución.
============================================================================================================== */
CREATE TRIGGER [FixedAsset].[tgg_ValidateValuesFromDepreciationAndInitialBalance]  
   ON  [FixedAsset].[FixedAssetPhysicalAssetDetailBook]  
   AFTER INSERT, UPDATE  
AS  
BEGIN  
    SET NOCOUNT ON;  
  
    -- Si no hay filas afectadas, salimos rápido (Buena práctica de Triggers)
    IF NOT EXISTS (SELECT 1 FROM INSERTED) RETURN;

    DECLARE @Errores NVARCHAR(MAX);

    -- Evaluamos en conjunto (SET-BASED) todas las filas afectadas
    SELECT @Errores = STRING_AGG(
        CONCAT('Placa: ', pa.Plate, 
               ' | Reg: ', CAST(i.DepreciatedValue AS NVARCHAR(50)), 
               ' | Esp: ', CAST(ISNULL(v.ExpectedValue, 0) AS NVARCHAR(50))
        ), CHAR(13))
    FROM INSERTED i
    JOIN FixedAsset.FixedAssetPhysicalAsset pa ON pa.Id = i.PhysicalAssetId
    CROSS APPLY (
        -- Forzamos a que sume ÚNICAMENTE el activo de la fila actual de INSERTED
        SELECT 
            (SELECT ISNULL(SUM(fadd.DepreciationValue - fadd.FinantialDiscountAdjusment), 0)
             FROM FixedAsset.FixedAssetDepreciation fad  
             JOIN FixedAsset.FixedAssetDepreciationDetail fadd ON fad.Id = fadd.FixedAssetDepreciationId  
             WHERE fad.Status = 2 AND fadd.FixedAssetPhysicalAssetId = i.PhysicalAssetId AND fadd.LegalBookId = i.LegalBookId)
            +
            (SELECT ISNULL(SUM(fadd.AmortizedValue), 0)
             FROM FixedAsset.FixedAssetDepreciation fad  
             JOIN FixedAsset.FixedAssetAmortizationDetail fadd ON fad.Id = fadd.FixedAssetDepreciationId  
             WHERE fad.Status = 2 AND fadd.FixedAssetPhysicalAssetDetailBookId = i.Id)
            +
            (SELECT ISNULL(SUM(faibidb.DepreciatedValue), 0)
             FROM FixedAsset.FixedAssetInitialBalance faib  
             JOIN FixedAsset.FixedAssetInitialBalanceItem faibi ON faib.id = faibi.FixedAssetInitialBalanceId  
             JOIN FixedAsset.FixedAssetInitialBalanceItemDetailBook faibidb ON faibi.id = faibidb.FixedAssetInitialBalanceItemId  
             WHERE faib.Status = 2 AND faibi.Plate = pa.Plate AND faibidb.LegalBookId = i.LegalBookId)
        AS ExpectedValue
    ) v
    WHERE pa.Depreciate = 1 
      AND i.DepreciatedValue <> ISNULL(v.ExpectedValue, 0);

    -- Si hubo descuadres, lanzamos el error con la lista de TODOS los activos malos
    IF @Errores IS NOT NULL
    BEGIN
        DECLARE @errorMessage NVARCHAR(2048) = LEFT('Error de validación de Depreciación. Activos con descuadre: ' + CHAR(13) + @Errores, 2048);
        RAISERROR(@errorMessage, 16, 1);
        ROLLBACK TRANSACTION;
    END
END
GO
CREATE TRIGGER [FixedAsset].[tgg_DetectarDiasEnCero]
   ON  [FixedAsset].[FixedAssetPhysicalAssetDetailBook] 
   AFTER UPDATE
AS 
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

declare @count as integer = 0

 
/* select @count = count(*)  from deleted d inner join FixedAsset.FixedAssetPhysicalAsset c on c.Id = d.PhysicalAssetId 
where c.Depreciate = 1 and d.Id = d.id */

 

--SELECT  @count = count(*)
--FROM INSERTED fpadb 
--inner join FixedAsset.FixedAssetPhysicalAsset fapa on fpadb.PhysicalAssetId=fapa.Id 
--where fapa.HasOutput = 0 and fpadb.ResidualValue > 0 and fpadb.DepreciatedDays = 0 

SELECT  @count = count(*)
FROM INSERTED i 
inner join deleted d on d.Id = i.Id
where i.DepreciatedDays < d.DepreciatedDays
    
if @count > 0 begin
	;THROW 51000, 'Error generado por control de actualización desde trigger. No puede pasar que hayan DepreciatedDays en Cero cuando ResidualValue  todavia tenga saldo', 1
end 
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor depreciado del descuento financiero acumulado. DECIMAL(18,2). Monto de descuento financiero ya depreciado del activo fijo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'FinantialDiscountDepreciated';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor depreciado del descuento financiero', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'FinantialDiscountDepreciated';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'FinantialDiscountDepreciated';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) si el activo se deprecia como Activo Fijo de Mínima Cuantía conforme Decreto 1625/2016 Art. 1.2.1.18.5 URT. Régimen tributario colombiano.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'ApplyMinimunAmount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el activo se deprecia como Activo Fijo de Mínima Cuantía de acuerdo con el  artículo 1.2.1.18.5 del Decreto 1625 de 2016 Único Reglamentario en Materia Tributaria', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'ApplyMinimunAmount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'ApplyMinimunAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor histórico del activo por libro contable. DECIMAL(18,2). Costo inicial de adquisición registrado en el libro legal correspondiente.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'HistoricalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Valor Historico del activo por Libro', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'HistoricalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'HistoricalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor residual de las partes del activo, saldo pendiente por depreciar. DECIMAL(18,2). Porción del valor en libros no depreciada aún de componentes.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'ResidualValuePart';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor en libros de las partes, es decir el valor que aun falta por depreciar', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'ResidualValuePart';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'ResidualValuePart';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor residual o en libros pendiente de depreciación. DECIMAL(18,2). Fórmula: Valor Histórico + Transacciones - Depreciaciones + Ajuste Inflación.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'ResidualValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor que falta por depreciar o el valor en libros    La formula para obtener el valor residual deberia ser   Valor En Libros = (Valor Historico + Transacciones - Depreciaciones + ValorAjustadoPorInflacion)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'ResidualValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'ResidualValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor acumulado de ajuste por inflación del activo. DECIMAL(18,2). Revaluación acumulada por cambios inflacionarios según normativa contable.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'InflationAdjustmentValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Valor del Ajuste por Inflacion Acumulado', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'InflationAdjustmentValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'InflationAdjustmentValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor depreciado de las partes del activo. DECIMAL(18,2). Porción ya consumida contablemente de los componentes del activo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'DepreciatedValuePart';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor de las partes que se han depreciado', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'DepreciatedValuePart';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'DepreciatedValuePart';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total depreciado acumulado del activo. DECIMAL(18,2). Monto de depreciación reconocida hasta la fecha según método aplicado.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'DepreciatedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor que se ha depreciado', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'DepreciatedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'DepreciatedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de transacciones que afectan libros (valorizaciones/desvalorizaciones). DECIMAL(18,2). Movimientos de revaluación que impactan valor en libros.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'TransactionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor de las transacciones, es decir las valorizaciones que afectaron el valor en libros', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'TransactionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'TransactionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor ajustado del activo en el libro. DECIMAL(18,2). Fórmula: Valor Histórico + Valorizaciones - Desvalorizaciones.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'AdjustedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor ajustado del activo, el cual debe se calcula y debe coincidir con la siguiente formula     Valor Hostorico + Valorizaciones - Desvalorizaciones', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'AdjustedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'AdjustedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Todas las desvalorizaciones acumuladas del activo. DECIMAL(18,2). Pérdidas de valor contable reconocidas en el período.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'Devaluation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica todas la desvalorizaciones que se le han realizado al activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'Devaluation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'Devaluation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Todas las valorizaciones acumuladas del activo en el libro. DECIMAL(18,2). Incrementos de valor contable reconocidos según revaluación.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'Valorization';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica todas las valorizaciones que se le han realizado al activo en el libro correspondiente', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'Valorization';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'Valorization';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de salvamento/valor residual. NUMERIC(5,2). Aplica solo si Tipo Depreciación es Reducción de Saldos; sino va cero.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'PercentageRescue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el porcentaje de salvamento. este solo se solicita si el Tipo de Depreciacion es de Reduccion de saldos, de lo contrario debe ir en cero', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'PercentageRescue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'PercentageRescue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total de unidades producidas del artículo. NUMERIC(18). Solo aplica cuando Método Depreciación es por Unidades de Producción.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'TotalProductionUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el total de unidades producidas del articulo, esto solo aplica para cuando el Metodo de Depreciacion es por Unidades Producidas', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'TotalProductionUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'TotalProductionUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Método de depreciación del activo. TINYINT. 1=Línea Recta, 2=Suma Dígitos, 3=Reducción Saldos, 4=Unidades Producción.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'DepreciationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el metodo de depreacion del Articulo  1 - Linea Recta  2 - Suma de Digitos  3 - Reduccion de Saldos  4 - Unidades de Produccion', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'DepreciationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'DepreciationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días ya depreciados del activo. INT. Cantidad de días consumidos según vida útil y depreciación acumulada.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'DepreciatedDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica los dias que ya fueron depreciados', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'DepreciatedDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'DepreciatedDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días pendientes por depreciar. INT. Se expresa en días; aumenta si transacción afecta vida útil del activo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'DaysPendingDepreciate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica los dias pendientes por depreciar    Nota: Este campo esta expresado en dias y cuando se registra el activo se registra la vida util con la que se ingreso y cuando se realice una transaccion que afecte el tiempo de vida util del activo este campo aumenta', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'DaysPendingDepreciate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'DaysPendingDepreciate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días durante los cuales se ha valorizado el activo. INT. Período acumulado de revaluación en unidades de tiempo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'ValorizationDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica los dias que se ha valorizado el activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'ValorizationDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'ValorizationDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de vida útil. TINYINT. 1=Año, 2=Mes, 3=Día. Según lo digitado por el usuario al registrar.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'UnitLifeTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Año,   2 - Mes,   3 - Dia    se guarda como la haya digitado el usuario', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'UnitLifeTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'UnitLifeTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vida útil expresada en días. INT. Período de depreciación registrado al crear el activo fijo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'LifeTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Vida Util expresada en dias, con el que se registra el activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'LifeTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'LifeTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del Libro Contable (Legal Book). INT. Referencia FK a [GeneralLedger].[LegalBook] donde se registra el activo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'LegalBookId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Libro Contable', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'LegalBookId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'LegalBookId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del Activo Fijo relacionado. INT. Referencia FK a [FixedAsset].[FixedAssetPhysicalAsset] principal.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'PhysicalAssetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Activo fijo relacionado', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'PhysicalAssetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'PhysicalAssetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID autoincrementable (IDENTITY). INT. Clave primaria única de la tabla de detalles de depreciación.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle contable y de depreciación de activos fijos físicos por libro contable (fiscal, NIIF, etc.). Registra la vida útil, método de depreciación, valores históricos, ajustados, residuales y acumulados de depreciación para cada activo en cada libro.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetDetailBook';
