-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2018-07-23
-- Description:	Genera el informe de Indicios de Deterioro de Activos Fijos
-- =============================================
CREATE PROCEDURE [FixedAsset].[SP_ReportDeteriorationIndicationsByPhysicalAsset]
	@Year INT,
	@Month INT,
	@ReportType TINYINT,
	@InitialPlate VARCHAR(max),
	@FinalPlate VARCHAR(max),
	@LegalBookId INT,
	@InitialMainAccount VARCHAR(max),
	@FinalMainAccount VARCHAR(max)
AS
BEGIN
	
	--Tabla en la que se guardan los datos del reporte
	DECLARE @DeteriorationIndicationsByPhysicalAsset TABLE
	(
		Id INT IDENTITY(1,1), 
		FixedAssetItemCode VARCHAR(20),
		PhysicalAssetId INT,
		Plate VARCHAR(50), 
		LegalBookId INT, 
		MainAccountNumber VARCHAR(50), 
		HistoricalValue DECIMAL(20,4), 
		ResidualValue DECIMAL(20,4), 
		DaysPendingDepreciate INT,
		RecoverableValue DECIMAL(20,4),
		DeteriorationIndicationCode VARCHAR(20), 
		DeteriorationIndicationName VARCHAR(100), 
		DeteriorationIndicationRate DECIMAL(5,2),
		DeteriorationRate DECIMAL(5,2),
		DeteriorationValue DECIMAL(20,4),
		IsFirstRow BIT
	)
	
	IF @ReportType = 1 --DETALLADO
	BEGIN

		INSERT INTO @DeteriorationIndicationsByPhysicalAsset
			(
				FixedAssetItemCode, 
				PhysicalAssetId,
				Plate,
				LegalBookId,
				MainAccountNumber, 
				HistoricalValue, 
				ResidualValue, 
				DaysPendingDepreciate,
				RecoverableValue,
				DeteriorationIndicationCode, 
				DeteriorationIndicationName, 
				DeteriorationIndicationRate,
				DeteriorationRate,
				DeteriorationValue,
				IsFirstRow
			)
			SELECT 
				fai.Code FixedAssetItemCode,
				fapa.Id PhysicalAssetId,
				fapa.Plate,
				@LegalBookId LegalBookId,
				ma.Number MainAccountNumber,
				ISNULL(fapadb.HistoricalValue, fapa.HistoricalValue) HistoricalValue,
				ISNULL(fapadb.ResidualValue, 0) ResidualValue,
				ISNULL(fapadb.DaysPendingDepreciate, 0) DaysPendingDepreciate,
				fapa.RecoverableValue,
				ISNULL(di.Code, '') DeteriorationIndicationCode,
				ISNULL(di.Name, '') DeteriorationIndicationName,
				ISNULL(di.Rate, 0) DeteriorationIndicationRate,
				ISNULL(dis.Rate, 0) DeteriorationRate,
				ISNULL(dis.Rate, 0) / 100 * fapa.RecoverableValue DeteriorationValue,
				IIF(ISNULL(dibpa.Id, 0) = ISNULL(dis.Id, 0), 1, 0) IsFirstRow
			FROM FixedAsset.FixedAssetPhysicalAsset fapa
			JOIN FixedAsset.FixedAssetItem fai ON fapa.ItemId = fai.Id
			JOIN GeneralLedger.MainAccounts ma ON fapa.MainAccountId = ma.Id
			LEFT JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook fapadb ON fapa.Id = fapadb.PhysicalAssetId AND fapadb.LegalBookId = @LegalBookId
			LEFT JOIN FixedAsset.DeteriorationIndicationByPhysicalAsset dibpa ON fapa.Id = dibpa.PhysicalAssetId
			LEFT JOIN FixedAsset.DeteriorationIndications di ON dibpa.DeteriorationIndicationId = di.Id
			LEFT JOIN
			(
				SELECT dibpa.PhysicalAssetId, SUM(di.Rate) Rate, MIN(dibpa.Id) Id
				FROM FixedAsset.DeteriorationIndicationByPhysicalAsset dibpa
				JOIN FixedAsset.DeteriorationIndications di ON dibpa.DeteriorationIndicationId = di.Id
				GROUP BY dibpa.PhysicalAssetId
			) dis ON fapa.Id = dis.PhysicalAssetId
			WHERE  (YEAR(fapa.AdquisitionDate) < @Year OR (YEAR(fapa.AdquisitionDate) = @Year AND MONTH(fapa.AdquisitionDate) <= @Month))
				AND fapa.Plate BETWEEN @InitialPlate and @FinalPlate
				AND ma.Number BETWEEN @InitialMainAccount and @FinalMainAccount

	END	
	ELSE IF @ReportType = 2 --RESUMIDO
	BEGIN
		
		INSERT INTO @DeteriorationIndicationsByPhysicalAsset
			(
				FixedAssetItemCode,
				PhysicalAssetId, 
				Plate,
				LegalBookId,
				MainAccountNumber, 
				HistoricalValue, 
				ResidualValue, 
				DaysPendingDepreciate,
				RecoverableValue,
				DeteriorationIndicationCode, 
				DeteriorationIndicationName, 
				DeteriorationIndicationRate,
				DeteriorationRate,
				DeteriorationValue,
				IsFirstRow
			)
			SELECT 
				fai.Code FixedAssetItemCode,
				fapa.Id PhysicalAssetId,
				fapa.Plate,
				@LegalBookId LegalBookId,
				ma.Number MainAccountNumber,
				ISNULL(fapadb.HistoricalValue, fapa.HistoricalValue) HistoricalValue,
				ISNULL(fapadb.ResidualValue, 0) ResidualValue,
				ISNULL(fapadb.DaysPendingDepreciate, 0) DaysPendingDepreciate,
				fapa.RecoverableValue,
				'' DeteriorationIndicationCode,
				'' DeteriorationIndicationName,
				0 DeteriorationIndicationRate,
				ISNULL(dis.Rate, 0) DeteriorationRate,
				ISNULL(dis.Rate, 0) / 100 * fapa.RecoverableValue DeteriorationValue,
				1 IsFirstRow
			FROM FixedAsset.FixedAssetPhysicalAsset fapa
			JOIN FixedAsset.FixedAssetItem fai ON fapa.ItemId = fai.Id
			JOIN GeneralLedger.MainAccounts ma ON fapa.MainAccountId = ma.Id
			LEFT JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook fapadb ON fapa.Id = fapadb.PhysicalAssetId AND fapadb.LegalBookId = @LegalBookId
			LEFT JOIN
			(
				SELECT dibpa.PhysicalAssetId, SUM(di.Rate) Rate, MIN(dibpa.Id) Id
				FROM FixedAsset.DeteriorationIndicationByPhysicalAsset dibpa
				JOIN FixedAsset.DeteriorationIndications di ON dibpa.DeteriorationIndicationId = di.Id
				GROUP BY dibpa.PhysicalAssetId
			) dis ON fapa.Id = dis.PhysicalAssetId
			WHERE (YEAR(fapa.AdquisitionDate) < @Year OR (YEAR(fapa.AdquisitionDate) = @Year AND MONTH(fapa.AdquisitionDate) <= @Month))
				AND fapa.Plate BETWEEN @InitialPlate and @FinalPlate
				AND ma.Number BETWEEN @InitialMainAccount and @FinalMainAccount

	END	
	
	--Actualizamos el valor residual y los dias depreciados hasta la fecha seleccionada	
	UPDATE dibpa
		SET dibpa.ResidualValue = dibpa.ResidualValue + ISNULL(fad.DepreciationValue, 0),
			dibpa.DaysPendingDepreciate = dibpa.DaysPendingDepreciate + ISNULL(fad.DepreciatedDays, 0)
	FROM @DeteriorationIndicationsByPhysicalAsset dibpa
	LEFT JOIN
	(
		SELECT fadd.LegalBookId, fadd.FixedAssetPhysicalAssetId, SUM(fadd.DepreciationValue) DepreciationValue, SUM(fadd.DepreciatedDays) DepreciatedDays
		FROM FixedAsset.FixedAssetDepreciation fad 
		JOIN FixedAsset.FixedAssetDepreciationDetail fadd ON fad.Id = fadd.FixedAssetDepreciationId
		WHERE fad.Status = 2
			AND (fad.ClosingYear > @Year OR (fad.ClosingYear = @Year AND fad.ClosingMonth > @Month))
		GROUP BY fadd.LegalBookId, fadd.FixedAssetPhysicalAssetId
	) fad ON dibpa.PhysicalAssetId = fad.FixedAssetPhysicalAssetId AND dibpa.LegalBookId = fad.LegalBookId

	--Mostramos los resultados
	SELECT * FROM @DeteriorationIndicationsByPhysicalAsset dibpa ORDER BY Plate, IsFirstRow DESC
END