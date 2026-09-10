-- ==================================================================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 12/07/2016
-- Description:	Procedimiento que se encarga de guardar masivamente la distribución de activos fijos
-- ==================================================================================================
CREATE PROCEDURE [Cost].[SP_ConfirmMasiveCostDistributionFixedAsset]
	@Year as int,
	@Month as int,
	@OperatingUnitId int,
	@CodeUser as varchar(20)
AS
BEGIN
	SET NOCOUNT ON

	/******************************************************** VARIABLES ******************************************************/

	DECLARE @CostDistributionFixedAssetXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX),
			@ConcatenatedMessage VARCHAR(MAX) = ''

	DECLARE @CostDistributionFixedAsset AS TABLE
	(
		FixedAssetPhysicalAssetId INT, Plate VARCHAR(50),
		FixedAssetItemCodeDescription VARCHAR(500),
		--------------------------------------
		ProductionCenterId INT DEFAULT(0), ProductionCenterCodeName VARCHAR(500),
		HoursQuantity INT DEFAULT(0),
		DepreciationValue DECIMAL(20,4) DEFAULT(0)
	)

	BEGIN TRY

		/**************************************************** CARGUE DETALLES ****************************************************/

		INSERT INTO @CostDistributionFixedAsset
			EXEC Cost.SP_GetDistributionFixedAssetDataByYearMonthAndPhysicalAssetId @Year, @Month, NULL
		
		/**************************************************** DISTRIBUIR HORAS ***************************************************/
		
		DECLARE @PhysicalAssetRows INT = 1,
				@PhysicalAssetId INT = 0	

		WHILE @PhysicalAssetRows > 0
		BEGIN
			SELECT TOP 1
				@PhysicalAssetId = cdm.FixedAssetPhysicalAssetId
			FROM @CostDistributionFixedAsset cdm
			WHERE cdm.FixedAssetPhysicalAssetId > @PhysicalAssetId
			ORDER BY cdm.FixedAssetPhysicalAssetId

			SET @PhysicalAssetRows = @@ROWCOUNT
			IF @PhysicalAssetRows = 0 
			BEGIN
				BREAK
			END

			-------------------------------------------------------------------------------------------------------------------

			IF NOT EXISTS(SELECT 1 FROM Cost.CostDistributionFixedAsset cdm WHERE cdm.Year = @Year AND cdm.Month = @Month AND cdm.FixedAssetPhysicalAssetId = @PhysicalAssetId)
			BEGIN
				SELECT @CostDistributionFixedAssetXml = CONVERT
				(
					XML, 
					(
						SELECT 
							CostDistributionFixedAsset.*,
							CostDistributionFixedAssetDetail.*
						FROM
						(
							SELECT	0 Id,
									@OperatingUnitId OperatingUnitId,
									'' Code,
									@Year Year, @Month Month,
									cdm.FixedAssetPhysicalAssetId, 
									CONCAT(cdm.Plate, ' - ', cdm.FixedAssetItemCodeDescription) Description,
									SUM(cdm.HoursQuantity) HoursWorked,
									SUM(cdm.DepreciationValue) DepreciationValue,
									1 Status
							FROM @CostDistributionFixedAsset cdm
							WHERE cdm.FixedAssetPhysicalAssetId = @PhysicalAssetId
							GROUP BY cdm.FixedAssetPhysicalAssetId, cdm.Plate, cdm.FixedAssetItemCodeDescription
						) CostDistributionFixedAsset
						JOIN
						( 
							SELECT
								0 DistributionFixedAssetId,
								cdm.ProductionCenterId,
								cdm.HoursQuantity,
								0 Proportion,
								cdm.DepreciationValue
							FROM @CostDistributionFixedAsset cdm
							WHERE cdm.FixedAssetPhysicalAssetId = @PhysicalAssetId
						) CostDistributionFixedAssetDetail ON CostDistributionFixedAsset.Id = CostDistributionFixedAssetDetail.DistributionFixedAssetId
						For xml AUTO,TYPE, ELEMENTS
					)
				)
				
				EXEC [Cost].[SP_SaveCostDistributionFixedAsset_Output] @CostDistributionFixedAssetXml, @CodeUser, @Code_Output OUT, @Message_Output OUT, NULL, NULL
				
				IF @Code_Output <> 0
				BEGIN
					SELECT @Code_Output as CodeMessage, @Message_Output as Message
					RETURN
				END

				SET @ConcatenatedMessage = @ConcatenatedMessage + @Message_Output + CHAR(13) + CHAR(10) 
			END
		END

		/******************************************************* RESULTADO *******************************************************/

		IF @ConcatenatedMessage = ''
		BEGIN
			SELECT 999 CodeMessage, 'No se encontraron registros pendientes por confirmar' Message
			RETURN
		END

		SELECT 0 CodeMessage, @ConcatenatedMessage Message
		
	END TRY
	BEGIN CATCH
		SELECT 999 CodeMessage, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) Message
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Confirma y guarda de forma masiva la distribución de costos de activos fijos para un período (año/mes) y unidad operativa determinados. Recorre cada activo físico pendiente de confirmar consultando sus datos de depreciación y horas trabajadas a través de SP_GetDistributionFixedAssetDataByYearMonthAndPhysicalAssetId, y para cada activo que aún no tenga registro en la tabla Cost.CostDistributionFixedAsset, construye un XML con el encabezado y el detalle por centro de producción y lo persiste mediante SP_SaveCostDistributionFixedAsset_Output. Existe para automatizar el cierre contable mensual de la depreciación de activos fijos distribuida entre centros de costo, evitando procesar activos ya confirmados y reportando el resultado consolidado de la operación.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmMasiveCostDistributionFixedAsset';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmMasiveCostDistributionFixedAsset';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Confirma de forma masiva la distribución mensual de costos (horas y depreciación) de los activos fijos por centro de producción para un año y mes dados, generando los registros faltantes.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmMasiveCostDistributionFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir información de distribución para el año y mes indicados obtenible vía SP_GetDistributionFixedAssetDataByYearMonthAndPhysicalAssetId; La unidad operativa indicada debe ser válida para asociar la distribución; El usuario indicado debe ser válido para auditoría del guardado', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmMasiveCostDistributionFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'No se vuelve a confirmar la distribución de un activo físico que ya tenga registro para el mismo año y mes; Cada activo físico se procesa una sola vez por ejecución (iteración ascendente por FixedAssetPhysicalAssetId); Las horas y la depreciación de la cabecera se totalizan (SUM) por activo físico; Cualquier error en el guardado interrumpe el proceso y retorna el error sin continuar con los siguientes activos; Errores no controlados se capturan y se retornan como CodeMessage 999 con el mensaje y línea del error', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmMasiveCostDistributionFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Distribución de costos; Activo fijo; Depreciación; Centro de producción; Horas trabajadas; Unidad operativa; Período contable (año/mes)', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmMasiveCostDistributionFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Cost.CostDistributionFixedAsset: Cuando no existe registro para (Year, Month, FixedAssetPhysicalAssetId), se construye un XML con cabecera y detalle y se delega la inserción a Cost.SP_SaveCostDistributionFixedAsset_Output; [RETURN_RESULT] RESULT: Si el guardado falla (Code_Output<>0) retorna el código y mensaje de error; si no se confirmó ningún activo retorna 999 con mensaje de no pendientes; en caso de éxito retorna 0 con el mensaje concatenado; [RETURN_RESULT] RESULT: Ante excepción capturada en CATCH retorna CodeMessage 999 con ERROR_MESSAGE() y la línea del error', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmMasiveCostDistributionFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si No existe registro en Cost.CostDistributionFixedAsset para el año, mes y activo físico actual → Construye XML con cabecera y detalle (centros de producción, horas, depreciación) e invoca Cost.SP_SaveCostDistributionFixedAsset_Output para persistirlo else Omite el activo físico (ya está confirmado para ese período); si El procedimiento de guardado retorna Code_Output distinto de 0 → Devuelve inmediatamente el código y mensaje de error y termina la ejecución else Acumula el mensaje exitoso en el mensaje concatenado; si Al finalizar el ciclo no se acumuló ningún mensaje → Devuelve CodeMessage 999 con ''No se encontraron registros pendientes por confirmar'' else Devuelve CodeMessage 0 con la concatenación de mensajes de los activos confirmados', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmMasiveCostDistributionFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Cost.SP_GetDistributionFixedAssetDataByYearMonthAndPhysicalAssetId; Cost.SP_SaveCostDistributionFixedAsset_Output', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmMasiveCostDistributionFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostDistributionFixedAsset', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmMasiveCostDistributionFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmMasiveCostDistributionFixedAsset';
-- GO
