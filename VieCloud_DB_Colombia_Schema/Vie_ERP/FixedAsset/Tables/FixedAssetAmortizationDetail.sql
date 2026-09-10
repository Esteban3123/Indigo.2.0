CREATE TABLE [FixedAsset].[FixedAssetAmortizationDetail] (
    [Id]                                  INT             IDENTITY (1, 1) NOT NULL,
    [FixedAssetDepreciationId]            INT             NOT NULL,
    [FixedAssetPhysicalAssetDetailBookId] INT             NOT NULL,
    [AccumulatedAmortization]             DECIMAL (18, 2) NOT NULL,
    [ResidualValue]                       DECIMAL (18, 2) NOT NULL,
    [AmortizedDays]                       INT             NOT NULL,
    [AmortizedValue]                      DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_FixedAssetAmortizationDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetAmortizationDetail_FixedAssetDepreciation] FOREIGN KEY ([FixedAssetDepreciationId]) REFERENCES [FixedAsset].[FixedAssetDepreciation] ([Id]),
    CONSTRAINT [FK_FixedAssetAmortizationDetail_FixedAssetPhysicalAssetDetailBook] FOREIGN KEY ([FixedAssetPhysicalAssetDetailBookId]) REFERENCES [FixedAsset].[FixedAssetPhysicalAssetDetailBook] ([Id])
);




GO



GO



GO
-- =============================================
-- Author:		Andrea Pahola Coqueco Cuellar
-- Create date: 2024-03-26
-- Description:	Se valida que el valor residual del registro concuerde con el valor residual del registro anterior, luego de haberse realizado las respectivas operaciones
-- =============================================
CREATE TRIGGER [FixedAsset].[tgg_ValidateResidualValueInAmortization]
   ON  [FixedAsset].[FixedAssetAmortizationDetail] 
   AFTER INSERT, UPDATE, DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @count INT = 0
    
	IF EXISTS (SELECT * FROM INSERTED)
	BEGIN
		-- INSERT OR UPDATE
		
		SELECT @count = fadd.FixedAssetPhysicalAssetDetailBookId --COUNT(fad.Id)
		FROM FixedAsset.FixedAssetDepreciation fad
		JOIN INSERTED fadd ON fad.Id = fadd.FixedAssetDepreciationId
		WHERE 
			--Se valida que el valor residual corresponda al valor residual anterior menor el valor de la depreciacion mas el valor de la transaccion del periodo
			ISNULL
			(
				(
					SELECT 
						TOP 1 fadd2.ResidualValue - fadd2.AmortizedValue
					FROM FixedAsset.FixedAssetDepreciation fad2
					JOIN FixedAsset.FixedAssetAmortizationDetail fadd2 ON fad2.Id = fadd2.FixedAssetDepreciationId
					WHERE fad2.Status = 2 
						AND fadd2.FixedAssetPhysicalAssetDetailBookId = fadd.FixedAssetPhysicalAssetDetailBookId
						AND 
						(
							(fad2.ClosingYear = fad.ClosingYear AND fad2.ClosingMonth < fad.ClosingMonth)
							OR
							(fad2.ClosingYear < fad.ClosingYear)
						)
					ORDER BY fad2.ClosingYear DESC, fad2.ClosingMonth DESC
				), 
				fadd.ResidualValue
			) <> fadd.ResidualValue

		IF @count = 0 AND EXISTS (SELECT * FROM DELETED)
		BEGIN
			-- Si es una actualización verifico registros posteriores

			SELECT @count = fadd.FixedAssetPhysicalAssetDetailBookId -- COUNT(fad.Id)
			FROM FixedAsset.FixedAssetDepreciation fad
			JOIN INSERTED fadd ON fad.Id = fadd.FixedAssetDepreciationId
			WHERE 
				--Se valida que el valor residual corresponda al valor residual anterior menor el valor de la depreciacion mas el valor de la transaccion del periodo
				ISNULL
				(
					(
						SELECT 
							TOP 1 fadd2.ResidualValue - fadd2.AmortizedValue
						FROM FixedAsset.FixedAssetDepreciation fad2
						JOIN FixedAsset.FixedAssetAmortizationDetail fadd2 ON fad2.Id = fadd2.FixedAssetDepreciationId
						WHERE fad2.Status = 2 
							AND fadd2.FixedAssetPhysicalAssetDetailBookId = fadd.FixedAssetPhysicalAssetDetailBookId
							AND fad2.ClosingYear >= fad.ClosingYear 
							AND fad2.ClosingMonth > fad.ClosingMonth
						ORDER BY fad2.ClosingYear, fad2.ClosingMonth
					), 
					fadd.ResidualValue
				) <> fadd.ResidualValue
		END

	END
	ELSE IF EXISTS (SELECT * FROM DELETED)
	BEGIN 
		-- DELETE
		
		--Se valida que el valor residual corresponda al valor residual anterior menor el valor de la depreciacion mas el valor de la transaccion del periodo
		--Comparando el anterior mes con el mes siguiente de los que quedan sin eliminar

		SELECT @count = COUNT(fad.Id)
		FROM FixedAsset.FixedAssetDepreciation fad
		JOIN DELETED fadd ON fad.Id = fadd.FixedAssetDepreciationId
		WHERE EXISTS
			(
				SELECT 1
				FROM FixedAsset.FixedAssetDepreciation fad2
				JOIN FixedAsset.FixedAssetAmortizationDetail fadd2 ON fad2.Id = fadd2.FixedAssetDepreciationId
				WHERE fad2.Status = 2 
					AND fadd2.FixedAssetPhysicalAssetDetailBookId = fadd.FixedAssetPhysicalAssetDetailBookId
					AND fad2.ClosingYear >= fad.ClosingYear 
					AND fad2.ClosingMonth >= fad.ClosingMonth
					AND fadd2.Id <> fadd.Id
			)			
			AND ISNULL
			(
				(
					SELECT 
						TOP 1 fadd2.ResidualValue - fadd2.AmortizedValue
					FROM FixedAsset.FixedAssetDepreciation fad2
					JOIN FixedAsset.FixedAssetAmortizationDetail fadd2 ON fad2.Id = fadd2.FixedAssetDepreciationId
					WHERE fad2.Status = 2 
						AND fadd2.FixedAssetPhysicalAssetDetailBookId = fadd.FixedAssetPhysicalAssetDetailBookId
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
						TOP 1 fadd2.ResidualValue - fadd2.AmortizedValue
					FROM FixedAsset.FixedAssetDepreciation fad2
					JOIN FixedAsset.FixedAssetAmortizationDetail fadd2 ON fad2.Id = fadd2.FixedAssetDepreciationId
					WHERE fad2.Status = 2 
						AND fadd2.FixedAssetPhysicalAssetDetailBookId = fadd.FixedAssetPhysicalAssetDetailBookId
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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario (DECIMAL 18,2) de la amortización/depreciación del período mensual del activo fijo; monto deducible en ese ciclo contable.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetAmortizationDetail', @level2type = N'COLUMN', @level2name = N'AmortizedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la amortización del mes.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetAmortizationDetail', @level2type = N'COLUMN', @level2name = N'AmortizedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetAmortizationDetail', @level2type = N'COLUMN', @level2name = N'AmortizedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de días (INT) durante los cuales se aplicó amortización en el período; base temporal para cálculo proporcional de depreciación.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetAmortizationDetail', @level2type = N'COLUMN', @level2name = N'AmortizedDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cantidad de dias amortizados', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetAmortizationDetail', @level2type = N'COLUMN', @level2name = N'AmortizedDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetAmortizationDetail', @level2type = N'COLUMN', @level2name = N'AmortizedDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor residual o en libros (DECIMAL 18,2) del activo antes del cierre contable; monto pendiente de amortizar o valor neto remanente.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetAmortizationDetail', @level2type = N'COLUMN', @level2name = N'ResidualValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor que falta por amortizar o el valor en libros que tenia antes de efectuar el cierre  ', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetAmortizationDetail', @level2type = N'COLUMN', @level2name = N'ResidualValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetAmortizationDetail', @level2type = N'COLUMN', @level2name = N'ResidualValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor acumulado total (DECIMAL 18,2) de todas las amortizaciones/depreciaciones previas al cierre; suma histórica de gastos de depreciación.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetAmortizationDetail', @level2type = N'COLUMN', @level2name = N'AccumulatedAmortization';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor acumulado de las amortizaciones antes de efectuar el cierre.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetAmortizationDetail', @level2type = N'COLUMN', @level2name = N'AccumulatedAmortization';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetAmortizationDetail', @level2type = N'COLUMN', @level2name = N'AccumulatedAmortization';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del registro de detalle contable-físico del activo fijo en tabla FixedAssetPhysicalAssetDetailBook; vincula línea contable del bien.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetAmortizationDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetPhysicalAssetDetailBookId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle contable del activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetAmortizationDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetPhysicalAssetDetailBookId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetAmortizationDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetPhysicalAssetDetailBookId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de la cabecera/maestro de depreciación en tabla FixedAssetDepreciation; agrupa detalles de un ciclo amortizativo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetAmortizationDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetDepreciationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla cabecera de la amortización', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetAmortizationDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetDepreciationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetAmortizationDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetDepreciationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico (INT IDENTITY) único de la fila de detalle de amortización; clave primaria de la tabla.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetAmortizationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumérico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetAmortizationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetAmortizationDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle del cálculo de amortización (depreciación) de activos fijos por período: registra cuántos días se amortizó cada activo, el valor amortizado en el período, la amortización acumulada hasta la fecha y el valor residual restante.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetAmortizationDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetAmortizationDetail';
