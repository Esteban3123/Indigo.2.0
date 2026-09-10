-- ==================================================================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 12/07/2016
-- Description:	Procedimiento que se encarga de guardar masivamente la distribución de activos fijos
-- ==================================================================================================
CREATE PROCEDURE [Cost].[SP_ImportCostDistributionFixedAsset]
	@Year INT,
	@Month INT,
	@OperatingUnitId INT,
	@ImportXml AS XML,
	@CodeUser varchar(20)
AS
BEGIN
	SET NOCOUNT ON
	
	/******************************************************** VARIABLES ******************************************************/

	DECLARE @CostDistributionFixedAssetXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX),
			@ConcatenatedMessage VARCHAR(MAX) = ''

	DECLARE @DistributionIds AS TABLE
	(
		Id INT
	)

	DECLARE @Details AS TABLE
	(
		FixedAssetPhysicalAssetId INT,
		Plate VARCHAR(20), FixedAssetItemCodeDescription VARCHAR(500),
		--------------------------------------
		ProductionCenterId INT, ProductionCenterCodeName VARCHAR(500),
		HoursQuantity INT DEFAULT(0),
		DepreciationValue DECIMAL(20,4) DEFAULT(0)
	)

	DECLARE @CostDistributionFixedAssetDetail AS TABLE
	(
		Id INT IDENTITY(1,1),
		ProductionCenterId INT,
		HoursQuantity INT DEFAULT(0),
		DepreciationValue DECIMAL(20,4) DEFAULT(0)
	)

	BEGIN TRY

		/**************************************************** CARGUE DETALLES ****************************************************/

		INSERT INTO @DistributionIds
			SELECT DISTINCT
				t.x.value('Id[1]','int')
			FROM @ImportXml.nodes('/Data') t(x)

		INSERT INTO @Details
			EXEC Cost.SP_GetDistributionFixedAssetDataByYearMonthAndPhysicalAssetId @Year, @Month, NULL

		/************************************************* VALIDACIONES GENERALES ************************************************/

		IF EXISTS 
		(
			SELECT 1 
			FROM Cost.CostDistributionFixedAsset dm 
			JOIN @DistributionIds mi ON dm.Id = mi.Id
			JOIN Cost.CostDistributionFixedAsset cdm ON dm.FixedAssetPhysicalAssetId = cdm.FixedAssetPhysicalAssetId AND cdm.Year = @Year AND cdm.Month = @Month
		)
		BEGIN
			SELECT @ConcatenatedMessage = STUFF((
					SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + cdm.Description + ' en estado: ' + IIF(cdm.Status = 0, 'Inactivo', 'Activo')
					FROM Cost.CostDistributionFixedAsset dm 
					JOIN @DistributionIds mi ON dm.Id = mi.Id
					JOIN Cost.CostDistributionFixedAsset cdm ON dm.FixedAssetPhysicalAssetId = cdm.FixedAssetPhysicalAssetId AND cdm.Year = @Year AND cdm.Month = @Month
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeMessage, 
				   'Ya existe una Distribución de Activos en el periodo para los siguientes artículos: ' + CHAR(13) + CHAR(10) + ISNULL(@ConcatenatedMessage, '') AS Message
			RETURN
		END		
		
		/**************************************************** DISTRIBUIR HORAS ***************************************************/

		DECLARE @DistributionRows INT = 1,
				@DistributionId INT = 0,
				@FixedAssetPhysicalAssetId INT,
				@TotalHours DECIMAL,
				--------------------------
				@DepreciationValue DECIMAL(20,4),
				--------------------------
				@outstandingDepreciationValue DECIMAL(20,4),		
				--------------------------
				@CostDistributionFixedAssetDetailRows INT,
				@CostDistributionFixedAssetDetailId INT,
				@adjusteDepreciationValue DECIMAL(20, 4)

		WHILE @DistributionRows > 0
		BEGIN
			SELECT TOP 1
				@DistributionId = cdm.Id,
				@FixedAssetPhysicalAssetId = cdm.FixedAssetPhysicalAssetId,
				@TotalHours = cdm.HoursWorked
			FROM @DistributionIds m
			JOIN Cost.CostDistributionFixedAsset cdm ON m.Id = cdm.Id
			WHERE cdm.Id > @DistributionId
			ORDER BY cdm.Id

			SET @DistributionRows = @@ROWCOUNT
			IF @DistributionRows = 0 
			BEGIN
				BREAK
			END

			-----------------------------------------------------------------------------------------------------------------------

			IF NOT EXISTS (SELECT 1 FROM @Details WHERE FixedAssetPhysicalAssetId = @FixedAssetPhysicalAssetId)
			BEGIN
				SELECT @ConcatenatedMessage = @ConcatenatedMessage + 'No existen distribuciones de activo fijo en el periodo para el artículo: ' + fapa.Plate + ' -' + fai.Code + ' - ' + fai.Description
				FROM FixedAsset.FixedAssetPhysicalAsset fapa
				JOIN FixedAsset.FixedAssetItem fai ON fapa.ItemId = fai.Id
				WHERE fapa.Id = @FixedAssetPhysicalAssetId

				CONTINUE
			END

			-----------------------------------------------------------------------------------------------------------------------

			SELECT TOP 1
				@DepreciationValue = SUM(d.DepreciationValue),
				-------------------------------------------------------------
				@CostDistributionFixedAssetDetailRows = 1,
				@CostDistributionFixedAssetDetailId = 0
			FROM @Details d
			WHERE d.FixedAssetPhysicalAssetId = @FixedAssetPhysicalAssetId

			-------------------------------------------------------------------------------------------------------------------

			DELETE FROM @CostDistributionFixedAssetDetail

			-------------------------------------------------------------------------------------------------------------------

			INSERT INTO @CostDistributionFixedAssetDetail
				SELECT 
					cdmd.ProductionCenterId, 
					cdmd.HoursQuantity, 
					(cdmd.HoursQuantity / @TotalHours * @DepreciationValue) DepreciationValue
				FROM Cost.CostDistributionFixedAssetDetail cdmd
				WHERE cdmd.DistributionFixedAssetId = @DistributionId

			SELECT	@outstandingDepreciationValue = @DepreciationValue - SUM(cdmd.DepreciationValue)
			FROM @CostDistributionFixedAssetDetail cdmd

			IF @outstandingDepreciationValue <> 0
			BEGIN
				WHILE @CostDistributionFixedAssetDetailRows > 0
				BEGIN
					SELECT TOP 1
						@CostDistributionFixedAssetDetailId = d.Id,
						@adjusteDepreciationValue = Common.CalculateAdjustedValue(@outstandingDepreciationValue, d.DepreciationValue, @outstandingDepreciationValue)
					FROM @CostDistributionFixedAssetDetail d
					WHERE d.Id > @CostDistributionFixedAssetDetailId
					ORDER BY d.Id

					SET @CostDistributionFixedAssetDetailRows = @@ROWCOUNT
					IF @CostDistributionFixedAssetDetailRows = 0 
					BEGIN
						BREAK
					END

					-------------------------------------------------------------------------------------------------------------------

					UPDATE d
						SET d.DepreciationValue = d.DepreciationValue + @adjusteDepreciationValue
					FROM @CostDistributionFixedAssetDetail d
					WHERE d.Id = @CostDistributionFixedAssetDetailId

					SELECT	@outstandingDepreciationValue = @outstandingDepreciationValue - @adjusteDepreciationValue

					IF @outstandingDepreciationValue = 0
					BEGIN
						BREAK
					END
				END
			END

			/**************************************************** INSERTAR REGISTROS ***************************************************/

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
								@TotalHours HoursWorked,
								@DepreciationValue DepreciationValue,
								1 Status
						FROM @Details cdm
						WHERE cdm.FixedAssetPhysicalAssetId = @FixedAssetPhysicalAssetId
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
						FROM @CostDistributionFixedAssetDetail cdm
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de importación masiva de distribución de costos de activos fijos por período (año/mes). Recibe un XML con los identificadores de distribuciones a procesar, valida que no exista ya una distribución registrada para el mismo activo y período evitando duplicados, y luego distribuye el valor de depreciación de cada activo fijo físico entre los distintos centros de producción en proporción a las horas trabajadas asignadas a cada uno. Utiliza SP_GetDistributionFixedAssetDataByYearMonthAndPhysicalAssetId para obtener los datos de depreciación del período, opera sobre la tabla CostDistributionFixedAsset (encabezado de distribución) y CostDistributionFixedAssetDetail (detalle por centro productivo), y aplica ajustes de redondeo para asegurar que la suma de valores distribuidos sea exactamente igual al valor total depreciado del activo.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ImportCostDistributionFixedAsset';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ImportCostDistributionFixedAsset';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Importa masivamente, para un periodo (año/mes) y unidad operativa, la distribución de depreciación de activos fijos hacia centros de producción, recalculando valores proporcionales a las horas trabajadas y persistiendo cada distribución vía SP de guardado.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ImportCostDistributionFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe contener nodos /Data con elemento Id que identifiquen distribuciones de activo fijo existentes en Cost.CostDistributionFixedAsset.; Debe existir información de distribución por activo en el periodo (año/mes) obtenida de Cost.SP_GetDistributionFixedAssetDataByYearMonthAndPhysicalAssetId.; Cada distribución origen referenciada debe tener detalles en Cost.CostDistributionFixedAssetDetail con HoursQuantity y un total de horas (HoursWorked) distinto de cero para evitar división por cero.; No debe existir previamente una distribución de activos para el mismo FixedAssetPhysicalAssetId en el año y mes indicados.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ImportCostDistributionFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La suma de DepreciationValue de los detalles insertados es igual al DepreciationValue total de la cabecera (se ajusta el residuo por redondeo).; Solo se procesan distribuciones cuyo activo físico tenga datos en el periodo; los demás generan mensaje informativo pero no abortan el proceso.; No se inserta una nueva distribución si ya existe una para el mismo activo físico en el mismo año y mes.; La cabecera siempre se crea con Status=1 (Activo) y Code vacío al delegar al SP de guardado.; El prorrateo de depreciación se basa en la proporción HoursQuantity/TotalHours de los detalles originales de la distribución.; Cualquier error en tiempo de ejecución se convierte en una respuesta controlada con CodeMessage=999 (no se relanza).', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ImportCostDistributionFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Distribución de activos fijos; Depreciación; Centro de producción; Activo fijo físico (placa); Horas trabajadas; Periodo contable (año/mes); Unidad operativa; Prorrateo por horas; Ajuste de redondeo de depreciación', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ImportCostDistributionFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Cost.CostDistributionFixedAsset: Para cada Id del XML cuyo activo físico tenga datos en @Details y no exista distribución previa en (Year, Month), se invoca Cost.SP_SaveCostDistributionFixedAsset_Output con un XML que crea una cabecera con Status=1, OperatingUnitId, Year, Month, HoursWorked y DepreciationValue agregados.; [INSERT] Cost.CostDistributionFixedAssetDetail: Por cada centro de producción del detalle origen, se inserta un detalle con DepreciationValue = (HoursQuantity / TotalHours) * DepreciationValue total, ajustado para que la suma cuadre exactamente con el DepreciationValue de la cabecera.; [RETURN_RESULT] ResultSet: Si ya existe distribución para el activo en el periodo, retorna CodeMessage=999 con mensaje listando los artículos en conflicto y su estado (Activo/Inactivo) y termina sin insertar.; [RETURN_RESULT] ResultSet: Si SP_SaveCostDistributionFixedAsset_Output retorna Code_Output<>0, propaga ese código y mensaje y termina la ejecución.; [RETURN_RESULT] ResultSet: Si tras procesar no se acumuló ningún mensaje (no hubo registros confirmados), retorna CodeMessage=999 ''No se encontraron registros pendientes por confirmar''.; [RETURN_RESULT] ResultSet: En caso de excepción capturada, retorna CodeMessage=999 con ERROR_MESSAGE() y número de línea.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ImportCostDistributionFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe en Cost.CostDistributionFixedAsset otra distribución para el mismo FixedAssetPhysicalAssetId en (Year, Month) → Construye lista concatenada de artículos en conflicto y retorna error 999 sin procesar else Continúa con el ciclo de distribución por cada Id; si No existen filas en @Details para el FixedAssetPhysicalAssetId actual → Acumula mensaje ''No existen distribuciones de activo fijo en el periodo para el artículo...'' y salta al siguiente Id (CONTINUE) else Procede a calcular depreciación distribuida por centro; si @outstandingDepreciationValue <> 0 después del prorrateo inicial → Itera sobre los detalles aplicando Common.CalculateAdjustedValue para ajustar diferencias de redondeo hasta que el saldo pendiente sea 0 else Procede directamente al armado del XML e inserción; si Code_Output devuelto por SP_SaveCostDistributionFixedAsset_Output es distinto de 0 → Retorna ese código y mensaje y aborta el procesamiento else Concatena el mensaje de éxito y continúa con el siguiente activo; si Al finalizar el bucle, @ConcatenatedMessage está vacío → Retorna CodeMessage=999 indicando que no hubo registros pendientes else Retorna CodeMessage=0 con el mensaje acumulado de las operaciones', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ImportCostDistributionFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Cost.SP_GetDistributionFixedAssetDataByYearMonthAndPhysicalAssetId; Cost.SP_SaveCostDistributionFixedAsset_Output; Common.CalculateAdjustedValue', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ImportCostDistributionFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostDistributionFixedAsset; Cost.CostDistributionFixedAssetDetail; FixedAsset.FixedAssetPhysicalAsset; FixedAsset.FixedAssetItem', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ImportCostDistributionFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ImportCostDistributionFixedAsset';
-- GO
