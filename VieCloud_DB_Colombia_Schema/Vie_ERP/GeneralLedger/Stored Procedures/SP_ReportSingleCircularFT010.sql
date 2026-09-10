
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera el reporte Formato FT010 de la Circular de Superintendencia, correspondiente a la revelación de activos fijos (propiedad, planta y equipo) para una línea de negocio de prestación de servicios. A partir de criterios de año, mes y libro contable legal recibidos como XML, consolida por cada activo físico su valor de reconocimiento histórico (incluyendo valorizaciones y desvalorizaciones), la depreciación acumulada (saldo inicial más movimientos del período) y el concepto NIIF según el tipo de inventario (muebles, equipos, vehículos, etc.), excluyendo los activos que ya fueron dados de baja. Integra tablas de tipos de inventario, ítems, activos físicos, transacciones, saldos iniciales y depreciaciones del módulo de Activos Fijos junto con la configuración del libro contable de Contabilidad General, para producir la información estructurada que se exporta al XML regulatorio FT010.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportSingleCircularFT010';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportSingleCircularFT010';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera los datos del reporte FT-010 (Circular) sobre propiedad, planta y equipo a un corte año/mes y libro contable, consolidando reconocimiento, depreciación y valorización/desvalorización por activo físico.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT010';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de criterios debe contener los nodos Year, Month y LegalBookId bajo /Data; Debe existir un registro en GeneralLedger.CompanySettings para obtener la línea de negocio; El LegalBookId proporcionado debe corresponder a un libro legal existente en GeneralLedger.LegalBook; Los activos físicos deben tener fecha de adquisición previa o igual al periodo (Year/Month) solicitado para ser incluidos', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT010';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen tipos de inventario 1, 2, 4, 8 y 9; Línea de negocio reportada siempre = 3 (Prestación de servicios); Finalidad siempre = 1 (Propiedad, planta y equipo) y Medición siempre = 1 (Valor Razonable); Deterioro, Estado y ValorMedida se reportan en 0; FechaMedida fija en ''00000000''; Solo se consideran transacciones, saldos iniciales, depreciaciones y bajas con Status = 2 (aprobado/confirmado); Solo se consideran movimientos con fecha (o cierre) hasta el mes/año del corte solicitado; Se excluyen activos físicos que tengan una baja (FixedAssetActiveOutput) confirmada hasta el periodo de corte; Si existe valor en FixedAssetPhysicalAssetDetailBook para el LegalBook, prevalece sobre el HistoricalValue del activo físico; Para transacciones, si no hay valor por libro (fatdb.Value), solo se toma el valor cuando el libro es OfficialBook = 1; Cualquier error se captura y se devuelve un resultado con CodeResult=''999'' y mensaje + número de línea', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT010';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Activo fijo; Placa de activo; Valor histórico; Depreciación; Valorización; Desvalorización (devaluation); Libro legal/contable; Saldo inicial de activos fijos; Baja de activos (Active Output); Reporte FT-010 (Circular Superintendencia); Línea de negocio Prestación de servicios; Propiedad, planta y equipo; Valor razonable', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT010';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ?: Devuelve un conjunto de resultados con LineaNegocio=3, Concepto (mapeado del tipo de inventario), Descripcion (Placa|Descripción), Finalidad=1, Medicion=1, Reconocimiento (valor histórico + valorización - desvalorización), Depreciacion (saldo inicial depreciado + depreciación acumulada), Deterioro=0, Estado=0, FechaMedida=''00000000'', ValorMedida=0, agrupado por tipo, placa, descripción y valores; [RETURN_RESULT] ?: En caso de excepción devuelve una fila con CodeResult=''999'' y MessageResult con el mensaje de error y número de línea', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT010';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Tipo de inventario del activo fijo (faitp.Type) = 1 o 2 → Concepto reportado = 3; si faitp.Type = 4 → Concepto reportado = 2; si faitp.Type = 8 → Concepto reportado = 1; si faitp.Type = 9 → Concepto reportado = 4; si En subconsulta de transacciones: TransactionType = 1 (valorización) → Si existe valor por libro (fatdb.Value) se usa ese; si no y el libro es oficial, se usa ftd.Value; sino 0 — acumulado como Valoritation; si En subconsulta de transacciones: TransactionType = 2 (desvalorización) → Misma lógica anterior, acumulado como Devaluation y restado del reconocimiento', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT010';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.CompanySettings; FixedAsset.FixedAssetInventoryType; FixedAsset.FixedAssetItemType; FixedAsset.FixedAssetItem; FixedAsset.FixedAssetPhysicalAsset; FixedAsset.FixedAssetPhysicalAssetDetailBook; FixedAsset.FixedAssetTransaction; FixedAsset.FixedAssetTransactionDetail; FixedAsset.FixedAssetTransactionDetailBook; GeneralLedger.LegalBook; FixedAsset.FixedAssetInitialBalance; FixedAsset.FixedAssetInitialBalanceItem; FixedAsset.FixedAssetInitialBalanceItemDetailBook; FixedAsset.FixedAssetDepreciation; FixedAsset.FixedAssetDepreciationDetail; FixedAsset.FixedAssetActiveOutput; FixedAsset.FixedAssetActiveOutputDetail', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT010';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT010';
-- GO
