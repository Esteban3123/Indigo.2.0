
-- =============================================
-- Author:		Miguel Fonseca
-- Create date: 2020-02-24
-- Description:	SP que genera la informacion para el XML FormatoArchiveFT010
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ReportSingleCircularFT010]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @Year int,
			@Month int,			
			@LegalBookId int,
			@BusinessLine tinyint
	
	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		SELECT	@Year = t.x.value('Year[1]','int'),
				@Month = t.x.value('Month[1]','int'),
				@LegalBookId = t.x.value('LegalBookId[1]','int')
		FROM @xmlCriterias.nodes('/Data') t(x)

		SET @BusinessLine = (SELECT cs.BusinessLine FROM GeneralLedger.CompanySettings cs)

		/********************************** OBTENCION DE DATOS **********************************/

		SELECT	3 LineaNegocio,	-- Prestación de servicios
				CASE faitp.Type
					WHEN 1 THEN 3 
					WHEN 2 THEN 3 
					WHEN 4 THEN 2 
					WHEN 8 THEN 1 
					WHEN 9 THEN 4 
				END Concepto,
				CONCAT(fapa.Plate,'|',fai.Description) Descripcion,
				1 Finalidad,	-- Propiedad, planta y equipo
				1 Medicion,		-- Valor Razonable
				SUM(ISNULL(fapadb.HistoricalValue,fapa.HistoricalValue)+isnull(ftd.Valoritation,0)-ISNULL(ftd.Devaluation,0)) Reconocimiento,
				ISNULL(faib.DepreciatedValue, 0) + ISNULL(fad.DepreciationValue, 0) Depreciacion,
				0 Deterioro,
				0 Estado,
				'00000000' FechaMedida,
				0 ValorMedida
		FROM FixedAsset.FixedAssetInventoryType faitp
		JOIN FixedAsset.FixedAssetItemType fait ON faitp.Id = fait.InventoryTypeId
		JOIN FixedAsset.FixedAssetItem fai ON fait.Id = fai.ItemTypeId
		JOIN FixedAsset.FixedAssetPhysicalAsset fapa ON fai.Id = fapa.ItemId AND (YEAR(fapa.AdquisitionDate) < @Year OR(YEAR(fapa.AdquisitionDate)=@Year AND MONTH(fapa.AdquisitionDate)<=@Month))
		LEFT JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook fapadb ON fapa.Id = fapadb.PhysicalAssetId AND fapadb.LegalBookId = @LegalBookId
		LEFT JOIN (SELECT	ftd.PhysicalAssetId,
							SUM(iif(ftd.TransactionType =1,iif(fatdb.Value IS NULL,iif(b.OfficialBook =1,ftd.Value,0),fatdb.Value),0)) Valoritation,
							SUM(iif(ftd.TransactionType=2,iif(fatdb.Value IS NULL,iif(b.OfficialBook =1,ftd.Value,0),fatdb.Value),0)) Devaluation
					FROM FixedAsset.FixedAssetTransaction ft
					JOIN FixedAsset.FixedAssetTransactionDetail ftd ON ft.Id = ftd.FixedAssetTransactionId
					LEFT JOIN FixedAsset.FixedAssetTransactionDetailBook fatdb WITH (NOLOCK) ON ftd.Id = fatdb.FixedAssetTransactionDetailId
					LEFT JOIN GeneralLedger.LegalBook b WITH(NOLOCK) ON b.id = @LegalBookId
					WHERE	ft.Status = 2 
							AND (YEAR(ft.DocumentDate) < @Year OR(YEAR(ft.DocumentDate)=@Year AND MONTH(ft.DocumentDate)<=@Month))
							AND ftd.PhysicalAssetId IS NOT NULL
					GROUP BY ftd.PhysicalAssetId
					)ftd on fapa.Id =ftd.PhysicalAssetId
		LEFT JOIN
		(
			SELECT faibi.Plate, faibidb.DepreciatedValue
			FROM FixedAsset.FixedAssetInitialBalance faib
			JOIN FixedAsset.FixedAssetInitialBalanceItem faibi ON faib.id = faibi.FixedAssetInitialBalanceId
			JOIN FixedAsset.FixedAssetInitialBalanceItemDetailBook faibidb ON faibi.id = faibidb.FixedAssetInitialBalanceItemId
			WHERE faib.Status = 2 AND faibidb.LegalBookId = @LegalBookId
				AND 
				(
					YEAR(faib.DocumentDate) < @Year
					OR
					(YEAR(faib.DocumentDate) = @Year AND MONTH(faib.DocumentDate) <= @Month)
				)
		) faib ON fapa.Plate = faib.Plate
		LEFT JOIN 
		(
			SELECT fadd.FixedAssetPhysicalAssetId, SUM(fadd.DepreciationValue) DepreciationValue
			FROM FixedAsset.FixedAssetDepreciation fad	
			JOIN FixedAsset.FixedAssetDepreciationDetail fadd ON fad.Id = fadd.FixedAssetDepreciationId
			WHERE fad.Status = 2 AND fadd.LegalBookId = @LegalBookId
				AND 
				(
					
					fad.ClosingYear < @Year
					OR
					(fad.ClosingYear = @Year AND fad.ClosingMonth <= @Month)
				)
			GROUP BY fadd.LegalBookId, fadd.FixedAssetPhysicalAssetId
		) fad ON fapa.Id = fad.FixedAssetPhysicalAssetId
		WHERE faitp.Type IN (1, 2, 4, 8, 9) 
		AND NOT EXISTS(SELECT faaod.PhysicalAssetId
															FROM FixedAsset.FixedAssetActiveOutput faao 
															JOIN FixedAsset.FixedAssetActiveOutputDetail faaod on faao.Id= faaod.FixedAssetActiveOutputId
															WHERE faao.Status = 2 AND  (YEAR(faao.DocumentDate) < @Year OR(YEAR(faao.DocumentDate)=@Year AND MONTH(faao.DocumentDate)<=@Month))
																AND faaod.PhysicalAssetId = fapa.Id
															)
		GROUP BY faitp.Type,fapa.Plate ,fai.Description,fapa.HistoricalValue,faib.DepreciatedValue,fad.DepreciationValue
	END TRY
	BEGIN CATCH	
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH
END