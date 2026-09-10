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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el informe de indicios de deterioro por activo fijo físico, utilizado en el módulo de activos fijos para evaluar si un bien ha perdido valor recuperable según las señales de deterioro registradas. Permite ejecutarse en modo detallado (mostrando cada indicio de deterioro con su tasa individual por activo) o en modo resumido (consolidando la tasa total de deterioro por activo). Toma como parámetros un rango de placas, un rango de cuentas contables principales, un libro contable legal y un período (año y mes), calculando el valor histórico, el valor residual ajustado con depreciaciones acumuladas hasta la fecha, los días depreciados pendientes y el valor de deterioro resultante de aplicar la tasa sobre el valor recuperable del activo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'PROCEDURE', @level1name = N'SP_ReportDeteriorationIndicationsByPhysicalAsset';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'PROCEDURE', @level1name = N'SP_ReportDeteriorationIndicationsByPhysicalAsset';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte (detallado o resumido) de indicios de deterioro de activos fijos físicos a una fecha (año/mes) y libro contable, calculando valor de deterioro y ajustando valor residual y días pendientes por depreciar según depreciaciones posteriores.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportDeteriorationIndicationsByPhysicalAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@ReportType debe ser 1 (detallado) o 2 (resumido); cualquier otro valor produce reporte vacío; Existencia del libro legal indicado por @LegalBookId para asociar el detalle contable; Las placas y números de cuenta principal deben permitir comparación BETWEEN (rangos válidos)', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportDeteriorationIndicationsByPhysicalAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'DeteriorationValue siempre se calcula como (Rate/100) * RecoverableValue del activo; HistoricalValue toma prioridad del detalle por libro (fapadb) y solo si no existe usa el del activo físico; La tasa de deterioro consolidada por activo es la SUMA de tasas de todos sus indicios; Sólo se consideran depreciaciones con Status=2 (cerradas/aprobadas) para los ajustes; El reporte resumido nunca expone códigos ni nombres de indicios individuales (siempre vacíos); Los activos adquiridos después del período de corte se excluyen del reporte', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportDeteriorationIndicationsByPhysicalAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Activo fijo físico; Placa; Indicio de deterioro; Tasa de deterioro; Valor histórico; Valor residual; Valor recuperable; Días pendientes por depreciar; Libro contable (LegalBook); Cuenta contable (PUC); Depreciación cerrada; Reporte detallado vs resumido', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportDeteriorationIndicationsByPhysicalAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @DeteriorationIndicationsByPhysicalAsset: Si @ReportType=1 inserta una fila por cada indicio de deterioro asociado al activo, marcando IsFirstRow=1 sólo cuando el Id del indicio coincide con el MIN(Id) agregado del activo; [INSERT] @DeteriorationIndicationsByPhysicalAsset: Si @ReportType=2 inserta una sola fila por activo con código/nombre/tasa de indicio vacíos o cero e IsFirstRow=1 (resumen); [INSERT] @DeteriorationIndicationsByPhysicalAsset: Sólo incluye activos cuya AdquisitionDate sea anterior o igual al año/mes solicitado, y cuyas Plate y MainAccount estén dentro de los rangos [Inicial,Final]; [UPDATE] @DeteriorationIndicationsByPhysicalAsset: Para cada fila se incrementa ResidualValue y DaysPendingDepreciate sumando los valores de depreciaciones con Status=2 cuyo cierre (ClosingYear/ClosingMonth) sea POSTERIOR al período solicitado, agrupados por activo y libro; [RETURN_RESULT] resultset: Devuelve todas las filas del reporte ordenadas por Plate y luego por IsFirstRow descendente', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportDeteriorationIndicationsByPhysicalAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @ReportType = 1 → Genera reporte DETALLADO incluyendo cada indicio de deterioro del activo y su tasa individual else Si @ReportType = 2 genera reporte RESUMIDO con una sola fila por activo y suma de tasas de deterioro; otros valores no generan filas; si ISNULL(dibpa.Id,0) = ISNULL(dis.Id,0) en modo detallado → Marca la fila como IsFirstRow=1 (primera fila del activo para presentación), de lo contrario 0; si fad.Status = 2 AND (ClosingYear > @Year OR (ClosingYear = @Year AND ClosingMonth > @Month)) → Las depreciaciones cerradas posteriores al período se restan del estado original sumándolas al ResidualValue y a los DaysPendingDepreciate para reflejar el estado a la fecha de corte', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportDeteriorationIndicationsByPhysicalAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'FixedAsset.FixedAssetPhysicalAsset; FixedAsset.FixedAssetItem; GeneralLedger.MainAccounts; FixedAsset.FixedAssetPhysicalAssetDetailBook; FixedAsset.DeteriorationIndicationByPhysicalAsset; FixedAsset.DeteriorationIndications; FixedAsset.FixedAssetDepreciation; FixedAsset.FixedAssetDepreciationDetail', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportDeteriorationIndicationsByPhysicalAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportDeteriorationIndicationsByPhysicalAsset';
-- GO
