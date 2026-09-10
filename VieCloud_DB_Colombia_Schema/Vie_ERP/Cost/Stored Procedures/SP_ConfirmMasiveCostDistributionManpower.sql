-- ==================================================================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 12/07/2016
-- Description:	Procedimiento que se encarga de guardar masivamente la distribución de mano de obra
-- ==================================================================================================
CREATE PROCEDURE [Cost].[SP_ConfirmMasiveCostDistributionManpower] 
	@Year as int,
	@Month as int,
	@OperatingUnitId int,
	@CodeUser as varchar(20)
AS
BEGIN
	SET NOCOUNT ON

	/******************************************************** VARIABLES ******************************************************/

	DECLARE @CostDistributionManpowerXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX),
			@ConcatenatedMessage VARCHAR(MAX) = ''

	DECLARE @CostDistributionManpower AS TABLE
	(
		ManpowerType TINYINT, EntityId INT,
		GroupId INT, GroupCodeName VARCHAR(500),
		EmployeeId INT, ThirdPartyId INT, ThirdPartyNitName VARCHAR(500),
		PositionId INT, PositionCodeName VARCHAR(500),
		--------------------------------------
		ProductionCenterId INT, ProductionCenterCodeName VARCHAR(500),
		HoursQuantity INT DEFAULT(0),
		TotalAccrued DECIMAL(20,4) DEFAULT(0),
		TotalProvision DECIMAL(20,4) DEFAULT(0),
		TotalEmployerContribution DECIMAL(20,4) DEFAULT(0),
		TotalParafiscal DECIMAL(20,4) DEFAULT(0)
	)

	BEGIN TRY

		/**************************************************** CARGUE DETALLES ****************************************************/

		INSERT INTO @CostDistributionManpower
			EXEC Cost.SP_GetDistributionManpower @Year, @Month, NULL, NULL

		/**************************************************** DISTRIBUIR HORAS ***************************************************/

		DECLARE @CostDistributionManpowerManpowerTypeRows INT = 1,
				@CostDistributionManpowerManpowerType INT = 0,
				---------------------------------------
				@CostDistributionManpowerEntityRows INT,
				@CostDistributionManpowerEntityId INT				

		WHILE @CostDistributionManpowerManpowerTypeRows > 0
		BEGIN
			SELECT TOP 1
				@CostDistributionManpowerManpowerType = cdm.ManpowerType,
				---------------------------------------
				@CostDistributionManpowerEntityRows = 1,
				@CostDistributionManpowerEntityId = 0
			FROM @CostDistributionManpower cdm
			WHERE cdm.ManpowerType > @CostDistributionManpowerManpowerType
			ORDER BY cdm.ManpowerType

			SET @CostDistributionManpowerManpowerTypeRows = @@ROWCOUNT
			IF @CostDistributionManpowerManpowerTypeRows = 0 
			BEGIN
				BREAK
			END

			-----------------------------------------------------------------------------------------------------------------------

			WHILE @CostDistributionManpowerEntityRows > 0
			BEGIN
				SELECT TOP 1
					@CostDistributionManpowerEntityId = cdm.EntityId
				FROM @CostDistributionManpower cdm
				WHERE cdm.ManpowerType = @CostDistributionManpowerManpowerType
					AND cdm.EntityId > @CostDistributionManpowerEntityId
				ORDER BY cdm.EntityId

				SET @CostDistributionManpowerEntityRows = @@ROWCOUNT
				IF @CostDistributionManpowerEntityRows = 0 
				BEGIN
					BREAK
				END

				-------------------------------------------------------------------------------------------------------------------

				IF NOT EXISTS(SELECT 1 FROM Cost.CostDistributionManpower cdm WHERE cdm.Year = @Year AND cdm.Month = @Month AND cdm.ManpowerType = @CostDistributionManpowerManpowerType AND cdm.EntityId = @CostDistributionManpowerEntityId)
				BEGIN
					SELECT @CostDistributionManpowerXml = CONVERT
					(
						XML, 
						(
							SELECT 
								CostDistributionManpower.*,
								CostDistributionManpowerDetail.*
							FROM
							(
								SELECT	0 Id,
										@OperatingUnitId OperatingUnitId,
										'' Code,
										@Year Year, @Month Month,
										cdm.ManpowerType, cdm.EntityId, 
										cdm.EmployeeId, cdm.ThirdPartyId, cdm.PositionId, cdm.GroupId, 
										cdm.ThirdPartyNitName Description,
										SUM(cdm.HoursQuantity) HoursWorked,
										SUM(cdm.TotalAccrued) TotalAccrued,SUM(cdm.TotalProvision) TotalProvision,SUM(cdm.TotalEmployerContribution) TotalEmployerContribution,SUM(cdm.TotalParafiscal) TotalParafiscal,
										1 Status
								FROM @CostDistributionManpower cdm
								WHERE cdm.ManpowerType = @CostDistributionManpowerManpowerType
									AND cdm.EntityId = @CostDistributionManpowerEntityId
								GROUP BY cdm.ManpowerType, cdm.EntityId, cdm.EmployeeId, cdm.ThirdPartyId, cdm.PositionId, cdm.GroupId, cdm.ThirdPartyNitName
							) CostDistributionManpower
							JOIN
							( 
								SELECT
									0 CostDistributionManpowerId,
									cdm.ProductionCenterId,
									cdm.HoursQuantity,
									cdm.TotalAccrued,
									cdm.TotalProvision,
									cdm.TotalEmployerContribution,
									cdm.TotalParafiscal
								FROM @CostDistributionManpower cdm
								WHERE cdm.ManpowerType = @CostDistributionManpowerManpowerType
									AND cdm.EntityId = @CostDistributionManpowerEntityId
							) CostDistributionManpowerDetail ON CostDistributionManpower.Id = CostDistributionManpowerDetail.CostDistributionManpowerId
							For xml AUTO,TYPE, ELEMENTS
						)
					)

					EXEC [Cost].[SP_SaveCostDistributionManpower_Output] @CostDistributionManpowerXml, @CodeUser, @Code_Output OUT, @Message_Output OUT, NULL, NULL

					IF @Code_Output <> 0
					BEGIN
						SELECT @Code_Output as CodeMessage, @Message_Output as Message
						RETURN
					END

					SET @ConcatenatedMessage = @ConcatenatedMessage + @Message_Output + CHAR(13) + CHAR(10) 
				END
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Confirma masivamente la distribución de costos de mano de obra para un año, mes y unidad operativa específicos. Obtiene los datos de distribución llamando a SP_GetDistributionManpower y, por cada combinación de tipo de mano de obra y entidad (empleado o tercero) que aún no haya sido confirmada, construye un XML con el encabezado y el detalle por centro de producción (horas trabajadas, devengado, provisiones, aportes patronales y parafiscales) y lo guarda mediante SP_SaveCostDistributionManpower_Output. Si todos los registros ya estaban confirmados, informa que no hay pendientes; de lo contrario, devuelve un mensaje consolidado con los resultados de cada grabación.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmMasiveCostDistributionManpower';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmMasiveCostDistributionManpower';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Confirma y persiste de forma masiva la distribución de costos de mano de obra de un período (año/mes) para cada combinación de tipo de mano de obra y entidad que aún no haya sido registrada, consolidando totales y delegando el guardado.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmMasiveCostDistributionManpower';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir información de distribución de mano de obra recuperable por SP_GetDistributionManpower para el año y mes indicados; El usuario (CodeUser) y la unidad operativa deben ser válidos para el SP de guardado; El año y mes deben corresponder a un período sobre el cual se calculó la nómina/distribución', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmMasiveCostDistributionManpower';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'No se vuelve a guardar una distribución para una combinación Año/Mes/ManpowerType/EntityId que ya exista en Cost.CostDistributionManpower (idempotencia por entidad); Los totales (horas, devengado, provisión, aportes, parafiscales) se agregan vía SUM agrupando por entidad antes de persistir; Toda nueva distribución se persiste con Status = 1; Cualquier error en SQL es capturado y devuelto con CodeMessage 999 incluyendo línea del error; Un fallo en el guardado de una entidad detiene el proceso masivo completo', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmMasiveCostDistributionManpower';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Distribución de costos de mano de obra; Tipo de mano de obra (ManpowerType); Empleado; Tercero; Cargo (Position); Centro de producción; Horas trabajadas; Devengado; Provisión; Aportes patronales; Parafiscales; Unidad operativa; Período contable (Año/Mes)', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmMasiveCostDistributionManpower';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Cost.CostDistributionManpower: Cuando NOT EXISTS un registro para (Year, Month, ManpowerType, EntityId), se construye XML agregado y se invoca SP_SaveCostDistributionManpower_Output para insertar la distribución y su detalle por centro de producción; [RETURN_RESULT] RESULT: Si Code_Output del guardado <> 0 retorna (CodeMessage, Message) con el error y termina; [RETURN_RESULT] RESULT: Si no se procesó ninguna entidad (mensaje concatenado vacío) retorna CodeMessage 999 con ''No se encontraron registros pendientes por confirmar''; [RETURN_RESULT] RESULT: Al finalizar exitosamente retorna CodeMessage 0 con la concatenación de todos los mensajes de guardado; [RETURN_RESULT] RESULT: En CATCH devuelve CodeMessage 999 con ERROR_MESSAGE() y la línea del error', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmMasiveCostDistributionManpower';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si No existe registro previo en Cost.CostDistributionManpower para el Year, Month, ManpowerType y EntityId actuales → Construye XML agregado por entidad y llama a SP_SaveCostDistributionManpower_Output para persistir la distribución else Omite la entidad (ya está confirmada) y continúa con la siguiente; si El SP de guardado retorna Code_Output distinto de 0 → Devuelve inmediatamente el código y mensaje de error y termina la ejecución; si Al finalizar el recorrido no se acumuló ningún mensaje (no se procesó nada) → Retorna CodeMessage 999 con mensaje ''No se encontraron registros pendientes por confirmar'' else Retorna CodeMessage 0 con la concatenación de mensajes de cada grabación', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmMasiveCostDistributionManpower';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Cost.SP_GetDistributionManpower; Cost.SP_SaveCostDistributionManpower_Output', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmMasiveCostDistributionManpower';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostDistributionManpower', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmMasiveCostDistributionManpower';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmMasiveCostDistributionManpower';
-- GO
