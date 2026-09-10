CREATE TABLE [FixedAsset].[FixedAssetDepreciationDetail] (
    [Id]                                       INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FixedAssetDepreciationId]                 INT             NOT NULL,
    [LegalBookId]                              INT             NOT NULL,
    [ActiveClass]                              TINYINT         NOT NULL,
    [FixedAssetPhysicalAssetId]                INT             NULL,
    [FixedAssetPhysicalAssetDetailBookId]      INT             NULL,
    [FixedAssetPhysicalAssetPartsId]           INT             NULL,
    [FixedAssetPhysicalAssetPartsDetailBookId] INT             NULL,
    [DepreciationValue]                        DECIMAL (18, 2) NOT NULL,
    [LifeTime]                                 INT             NOT NULL,
    [RemainingLifeTime]                        INT             NOT NULL,
    [DepreciatedDays]                          INT             NOT NULL,
    [ValorizationValue]                        DECIMAL (18, 2) NOT NULL,
    [DevaluationValue]                         DECIMAL (18, 2) NOT NULL,
    [AdjustedValue]                            DECIMAL (18, 2) NOT NULL,
    [TransactionValue]                         DECIMAL (18, 2) CONSTRAINT [DF_FixedAssetDepreciationDetail_TransactionValue] DEFAULT ((0)) NOT NULL,
    [AccumulatedDepreciation]                  DECIMAL (18, 2) NOT NULL,
    [ResidualValue]                            DECIMAL (18, 2) NOT NULL,
    [FinantialDiscount]                        DECIMAL (18, 2) CONSTRAINT [DF__FixedAsse__Finan__6BB6EBCD] DEFAULT ((0)) NOT NULL,
    [FinantialDiscountAdjusment]               DECIMAL (18, 2) CONSTRAINT [DF__FixedAsse__Finan__6CAB1006] DEFAULT ((0)) NOT NULL,
    [FinantialDiscountDepreciation]            DECIMAL (18, 2) CONSTRAINT [DF__FixedAsse__Finan__7263E95C] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_FixedAssetDepreciationDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetDepreciationDetail_FixedAssetDepreciation] FOREIGN KEY ([FixedAssetDepreciationId]) REFERENCES [FixedAsset].[FixedAssetDepreciation] ([Id]),
    CONSTRAINT [FK_FixedAssetDepreciationDetail_LegalBook] FOREIGN KEY ([LegalBookId]) REFERENCES [GeneralLedger].[LegalBook] ([Id])
);




GO



GO





GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_FixedAssetDepreciationDetail__FixedAssetDepreciationId__INC__LegalBookId]
    ON [FixedAsset].[FixedAssetDepreciationDetail]([FixedAssetDepreciationId] ASC)
    INCLUDE([LegalBookId]);


GO
ALTER INDEX [IX_FixedAssetDepreciationDetail__FixedAssetDepreciationId__INC__LegalBookId]
    ON [FixedAsset].[FixedAssetDepreciationDetail] DISABLE;




GO
CREATE NONCLUSTERED INDEX [IX_FixedAssetDepreciationDetail__FixedAssetDepreciationId__LegalBookId__INC__FixedAssetPhysicalAssetId__Id]
    ON [FixedAsset].[FixedAssetDepreciationDetail]([FixedAssetDepreciationId] ASC, [LegalBookId] ASC)
    INCLUDE([FixedAssetPhysicalAssetId], [Id]);


GO
-- =============================================
-- Author:		Miguel Angel Fonseca
-- Create date: 2018-05-21
-- Description:	Se valida que el valor residual del registro concuerde con el valor residual del registro anterior, luego de haberse realizado las respectivas operaciones
-- =============================================
CREATE TRIGGER [FixedAsset].[tgg_ValidateResidualValueInDepreciation]
   ON  [FixedAsset].[FixedAssetDepreciationDetail] 
   AFTER INSERT, UPDATE, DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @count INT = 0
    
	IF EXISTS (SELECT * FROM INSERTED) -- INSERT OR UPDATE
	BEGIN		
		--Se valida que el valor residual corresponda al valor residual anterior menor el valor de la depreciacion mas el valor de la transaccion del periodo
		SELECT @count = fadd.FixedAssetPhysicalAssetDetailBookId --COUNT(fad.Id)
		FROM FixedAsset.FixedAssetDepreciation fad
		JOIN INSERTED fadd ON fad.Id = fadd.FixedAssetDepreciationId
		CROSS APPLY (
			SELECT 
				TOP 1 faddp.ResidualValue, faddp.DepreciationValue, faddp.FinantialDiscountAdjusment, faddp.TransactionValue
			FROM FixedAsset.FixedAssetDepreciation fadp
			JOIN FixedAsset.FixedAssetDepreciationDetail faddp ON fadp.Id = faddp.FixedAssetDepreciationId
			WHERE fadp.Status = 2 
				AND faddp.FixedAssetPhysicalAssetId = fadd.FixedAssetPhysicalAssetId
				AND faddp.LegalBookId = fadd.LegalBookId 
				AND 
				(
					(fadp.ClosingYear = fad.ClosingYear AND fadp.ClosingMonth < fad.ClosingMonth)
					OR
					(fadp.ClosingYear < fad.ClosingYear)
				)
			ORDER BY fadp.ClosingYear DESC, fadp.ClosingMonth DESC
		) faddp
		WHERE (
			(faddp.ResidualValue - faddp.DepreciationValue + faddp.FinantialDiscountAdjusment) 
			<> 
			(fadd.ResidualValue - (fadd.TransactionValue - faddp.TransactionValue))
		)


		IF @count = 0 AND EXISTS (SELECT * FROM DELETED) -- UPDATE
		BEGIN
			-- Si es una actualización verifico registros posteriores
			SELECT @count = faddp.FixedAssetPhysicalAssetDetailBookId -- COUNT(fad.Id)
			FROM FixedAsset.FixedAssetDepreciation fadp
			JOIN INSERTED faddp ON fadp.Id = faddp.FixedAssetDepreciationId
			CROSS APPLY (
				SELECT 
					TOP 1 fadd.ResidualValue, fadd.TransactionValue
				FROM FixedAsset.FixedAssetDepreciation fad
				JOIN FixedAsset.FixedAssetDepreciationDetail fadd ON fad.Id = fadd.FixedAssetDepreciationId
				WHERE fad.Status = 2 
					AND faddp.FixedAssetPhysicalAssetId = fadd.FixedAssetPhysicalAssetId
					AND faddp.LegalBookId = fadd.LegalBookId 
					AND 
					(
						(fadp.ClosingYear = fad.ClosingYear AND fadp.ClosingMonth < fad.ClosingMonth)
						OR
						(fadp.ClosingYear < fad.ClosingYear)
					)
				ORDER BY fad.ClosingYear, fad.ClosingMonth
			) fadd
			WHERE (
				(faddp.ResidualValue - faddp.DepreciationValue + faddp.FinantialDiscountAdjusment) 
				<> 
				(fadd.ResidualValue - (fadd.TransactionValue - faddp.TransactionValue))
			)
		END

	END
	ELSE IF EXISTS (SELECT * FROM DELETED) -- DELETE
	BEGIN 		
		--Se valida que el valor residual corresponda al valor residual anterior menor el valor de la depreciacion mas el valor de la transaccion del periodo
		--Comparando el anterior mes con el mes siguiente de los que quedan sin eliminar		
		SELECT @count = COUNT(fad.Id)
		FROM FixedAsset.FixedAssetDepreciation fad
		JOIN DELETED fadd ON fad.Id = fadd.FixedAssetDepreciationId
		WHERE EXISTS
			(
				SELECT 1
				FROM FixedAsset.FixedAssetDepreciation fad2
				JOIN FixedAsset.FixedAssetDepreciationDetail fadd2 ON fad2.Id = fadd2.FixedAssetDepreciationId
				WHERE fad2.Status = 2 
					AND fadd2.FixedAssetPhysicalAssetId = fadd.FixedAssetPhysicalAssetId
					AND fadd2.LegalBookId = fadd.LegalBookId 
					AND fad2.ClosingYear >= fad.ClosingYear 
					AND fad2.ClosingMonth >= fad.ClosingMonth
					AND fadd2.Id <> fadd.Id
			)			
			AND ISNULL
			(
				(
					SELECT 
						TOP 1 fadd2.ResidualValue - fadd2.DepreciationValue + fadd2.FinantialDiscountAdjusment + (fadd.TransactionValue - fadd2.TransactionValue)
					FROM FixedAsset.FixedAssetDepreciation fad2
					JOIN FixedAsset.FixedAssetDepreciationDetail fadd2 ON fad2.Id = fadd2.FixedAssetDepreciationId
					WHERE fad2.Status = 2 
						AND fadd2.FixedAssetPhysicalAssetId = fadd.FixedAssetPhysicalAssetId
						AND fadd2.LegalBookId = fadd.LegalBookId 
						AND fad2.ClosingYear <= fad.ClosingYear 
						AND fad2.ClosingMonth <= fad.ClosingMonth
						AND fadd2.Id <> fadd.Id
					ORDER BY fad2.ClosingYear DESC, fad2.ClosingMonth DESC
				), 
				fadd.ResidualValue
			)
			<> 
			ISNULL
			(
				(
					SELECT 
						TOP 1 fadd2.ResidualValue - fadd2.DepreciationValue + fadd2.FinantialDiscountAdjusment + (fadd.TransactionValue - fadd2.TransactionValue)
					FROM FixedAsset.FixedAssetDepreciation fad2
					JOIN FixedAsset.FixedAssetDepreciationDetail fadd2 ON fad2.Id = fadd2.FixedAssetDepreciationId
					WHERE fad2.Status = 2 
						AND fadd2.FixedAssetPhysicalAssetId = fadd.FixedAssetPhysicalAssetId
						AND fadd2.LegalBookId = fadd.LegalBookId 
						AND fad2.ClosingYear >= fad.ClosingYear 
						AND fad2.ClosingMonth >= fad.ClosingMonth
						AND fadd2.Id <> fadd.Id
					ORDER BY fad2.ClosingYear, fad2.ClosingMonth
				), 
				fadd.ResidualValue
			)

	END

	IF @count > 0 
	
	BEGIN
		THROW 51000, 'Error generado por control desde trigger. El valor residual de la depreciación debe coincidir con el valor residual del mes anterior', 1
	END
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Depreciación del descuento financiero (DECIMAL 18,2). Porción de depreciación correspondiente al descuento financiero aplicado al activo fijo en el período contable.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'FinantialDiscountDepreciation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Depreciación de descuento Financiero', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'FinantialDiscountDepreciation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'FinantialDiscountDepreciation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ajuste del descuento financiero (DECIMAL 18,2). Corrección o reajuste aplicado al descuento financiero durante el cierre contable del activo fijo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'FinantialDiscountAdjusment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ajuste del Descuento Financiero', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'FinantialDiscountAdjusment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'FinantialDiscountAdjusment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descuento financiero (DECIMAL 18,2). Rebaja o beneficio financiero aplicado al valor en libros del activo fijo, afecta depreciación y ajustes.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'FinantialDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descuento Financiero', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'FinantialDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'FinantialDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor residual o en libros (DECIMAL 18,2). Importe pendiente por depreciar o valor contable del activo fijo antes del cierre del período, equivalente a valor de salvamento.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'ResidualValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor que falta por depreciar o el valor en libros que tenia antes de efectuar el cierre  ', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'ResidualValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'ResidualValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Depreciación acumulada (DECIMAL 18,2). Total de depreciaciones registradas del activo fijo acumuladas hasta antes del cierre contable del período.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'AccumulatedDepreciation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor acumulado de las depreciaciones antes de efectuar el cierre', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'AccumulatedDepreciation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'AccumulatedDepreciation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de transacciones o revaluaciones (DECIMAL 18,2, default=0). Importe de valorizaciones y ajustes que modificaron el valor en libros del activo fijo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'TransactionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor de las transacciones, es decir las valorizaciones que afectaron el valor en libros', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'TransactionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'TransactionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor ajustado del activo (DECIMAL 18,2). Importe final del activo fijo después de aplicar revalúos, depreciación y ajustes al momento del cierre contable.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'AdjustedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor ajustado del activo al momento de realizar el cierre', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'AdjustedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'AdjustedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de desvalorización (DECIMAL 18,2). Total de pérdidas de valor o depreciaciones extraordinarias realizadas al activo fijo en el libro legal.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'DevaluationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica todas la desvalorizaciones que se le han realizado al activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'DevaluationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'DevaluationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de revaluación o valorización (DECIMAL 18,2). Total de incrementos de valor o revalúos realizados al activo fijo en el libro legal correspondiente.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'ValorizationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica todas las valorizaciones que se le han realizado al activo en el libro correspondiente', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'ValorizationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'ValorizationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días depreciados (INT). Cantidad de días durante los cuales se ha depreciado el activo fijo en el período contable.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'DepreciatedDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cantidad de dias depreciados', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'DepreciatedDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'DepreciatedDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vida útil restante (INT, años/meses). Tiempo de depreciación pendiente del activo fijo después del cierre del período actual.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'RemainingLifeTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Vida util restante despues de efectuar el cierre', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'RemainingLifeTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'RemainingLifeTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vida útil total (INT, años/meses). Período total de depreciación estimado del activo fijo al momento del cierre contable.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'LifeTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la vida util al efectuar el cierre', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'LifeTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'LifeTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de depreciación del período (DECIMAL 18,2). Monto de depreciación mensual o periódica del activo fijo registrado en el cierre.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'DepreciationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la depreciacion del mes', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'DepreciationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'DepreciationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID detalle contable de componente (INT). Identificador del registro contable de la parte o componente del activo; se completa solo si ActiveClass=2 (parte de activo).', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetPhysicalAssetPartsDetailBookId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id del detalle contable de la parte    Este campo solo se llena si la Clase del Activo es 2', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetPhysicalAssetPartsDetailBookId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetPhysicalAssetPartsDetailBookId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de parte/componente del activo (INT, FK). Identificador de la pieza o componente que integra el activo fijo; se usa solo si ActiveClass=2 (componente separable).', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetPhysicalAssetPartsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id de la parte del activo    Este campo solo se llena si la Clase del Activo es 2', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetPhysicalAssetPartsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetPhysicalAssetPartsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID detalle contable del activo (INT, FK). Identificador del registro contable del activo principal en el libro legal; se completa solo si ActiveClass=1.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetPhysicalAssetDetailBookId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle contable del activo    Este campo solo se llena si la clase del activo es 1', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetPhysicalAssetDetailBookId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetPhysicalAssetDetailBookId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del activo físico principal (INT, FK). Identificador del activo fijo completo o componente superior; se usa solo si ActiveClass=1 (activo principal).', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetPhysicalAssetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id del activo dijo    Este campo solo se llena si la Clase del Activo es 1', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetPhysicalAssetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetPhysicalAssetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del activo (TINYINT: 1=Principal, 2=Componente). Tipo de activo que se deprecia: 1 para activo independiente, 2 para parte integrada de otro activo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'ActiveClass';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo del activo que se esta depreciando  1 - Activo Principal  2 - Parte del Activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'ActiveClass';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'ActiveClass';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del libro legal (INT, FK). Referencia al registro en GeneralLedger.LegalBook que contiene la contabilidad oficial del activo fijo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'LegalBookId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de libro legal', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'LegalBookId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'LegalBookId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de depreciación del activo (INT, FK). Referencia al registro padre en FixedAssetDepreciation que agrupa todos los detalles de depreciación del período.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetDepreciationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de amortización de activos fijos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetDepreciationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetDepreciationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, IDENTITY 1,1). Clave primaria autonumérica de cada detalle de depreciación de activo fijo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de la depreciación de activos fijos por libro contable. Registra los valores de depreciación, valorización, desvalorización, vida útil y descuentos financieros para cada activo o componente de activo en un proceso de depreciación específico.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciationDetail';
