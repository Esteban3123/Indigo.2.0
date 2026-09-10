-- ==================================================================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 12/07/2016
-- Description:	Procedimiento que se encarga de guardar masivamente la distribución de mano de obra
-- ==================================================================================================
CREATE PROCEDURE [Cost].[SP_ImportCostDistributionManpower]
	@Year INT,
	@Month INT,
	@OperatingUnitId INT,
	@ImportXml AS XML,
	@CodeUser varchar(20)
AS
BEGIN
	SET NOCOUNT ON

	/******************************************************** VARIABLES ******************************************************/

	DECLARE @CostDistributionManpowerXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX),
			@ConcatenatedMessage VARCHAR(MAX) = ''

	DECLARE @ManpowerIds AS TABLE
	(
		Id INT
	)

	DECLARE @Details AS TABLE
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

	DECLARE @CostDistributionManpowerDetail AS TABLE
	(
		Id INT IDENTITY(1,1),
		ProductionCenterId INT,
		HoursQuantity INT DEFAULT(0),
		TotalAccrued DECIMAL(20,4) DEFAULT(0),
		TotalProvision DECIMAL(20,4) DEFAULT(0),
		TotalEmployerContribution DECIMAL(20,4) DEFAULT(0),
		TotalParafiscal DECIMAL(20,4) DEFAULT(0)
	)

	BEGIN TRY

		/**************************************************** CARGUE DETALLES ****************************************************/

		INSERT INTO @ManpowerIds
			SELECT DISTINCT
				t.x.value('Id[1]','int')
			FROM @ImportXml.nodes('/Data') t(x)

		INSERT INTO @Details
			EXEC Cost.SP_GetDistributionManpower @Year, @Month, NULL, NULL

		/************************************************* VALIDACIONES GENERALES ************************************************/

		IF EXISTS 
		(
			SELECT 1 
			FROM Cost.CostDistributionManpower dm 
			JOIN @ManpowerIds mi ON dm.Id = mi.Id
			JOIN Cost.CostDistributionManpower cdm ON dm.ManpowerType = cdm.ManpowerType AND dm.ThirdPartyId = cdm.ThirdPartyId AND cdm.Year = @Year AND cdm.Month = @Month
		)
		BEGIN
			SELECT @ConcatenatedMessage = STUFF((
					SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + tp.Nit + ' -' + tp.Name + ' de tipo ' + IIF(cdm.ManpowerType = 1, 'Empleado', 'Contratista') + ' en estado: ' + IIF(cdm.Status = 0, 'Inactivo', 'Activo')
					FROM Cost.CostDistributionManpower dm 
					JOIN @ManpowerIds mi ON dm.Id = mi.Id
					JOIN Cost.CostDistributionManpower cdm ON dm.ManpowerType = cdm.ManpowerType AND dm.ThirdPartyId = cdm.ThirdPartyId AND cdm.Year = @Year AND cdm.Month = @Month
					JOIN Common.ThirdParty tp ON cdm.ThirdPartyId = tp.Id
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeMessage, 
				   'Ya existe una Distribución de Mano de Obra en el periodo para los siguientes terceros: ' + CHAR(13) + CHAR(10) + ISNULL(@ConcatenatedMessage, '') AS Message
			RETURN
		END		
		
		/**************************************************** DISTRIBUIR HORAS ***************************************************/

		DECLARE @ManpowerRows INT = 1,
				@ManpowerId INT = 0,
				@ManpowerType TINYINT,
				@ThirdParty INT,
				@TotalHours DECIMAL,
				--------------------------
				@EntityRows INT,
				@EntityId INT,
				@TotalAccrued DECIMAL(20,4),
				@TotalProvision DECIMAL(20,4),
				@TotalEmployerContribution DECIMAL(20,4),
				@TotalParafiscal DECIMAL(20,4),
				--------------------------
				@outstandingAccrued DECIMAL(20,4),
				@outstandingProvision DECIMAL(20,4),
				@outstandingEmployerContribution DECIMAL(20,4),
				@outstandingParafiscal DECIMAL(20,4),				
				--------------------------
				@CostDistributionManpowerDetailRows INT,
				@CostDistributionManpowerDetailId INT,
				@adjustedAccrued DECIMAL(20, 4),
				@adjustedProvision DECIMAL(20, 4),
				@adjustedEmployerContribution DECIMAL(20, 4),
				@adjustedParafiscal DECIMAL(20, 4)

		WHILE @ManpowerRows > 0
		BEGIN
			SELECT TOP 1
				@ManpowerId = cdm.Id,
				@ManpowerType= cdm.ManpowerType,
				@ThirdParty = cdm.ThirdPartyId,
				@TotalHours = cdm.HoursWorked,
				--------------------------
				@EntityRows = 1,
				@EntityId = 0
			FROM @ManpowerIds m
			JOIN Cost.CostDistributionManpower cdm ON m.Id = cdm.Id
			WHERE cdm.Id > @ManpowerId
			ORDER BY cdm.Id

			SET @ManpowerRows = @@ROWCOUNT
			IF @ManpowerRows = 0 
			BEGIN
				BREAK
			END

			-----------------------------------------------------------------------------------------------------------------------

			IF NOT EXISTS (SELECT 1 FROM @Details WHERE ManpowerType = @ManpowerType AND ThirdPartyId = @ThirdParty)
			BEGIN
				SELECT @ConcatenatedMessage = @ConcatenatedMessage + 'No existen distribuciones de mano de obra en el periodo para el tercero: ' + tp.Nit + ' -' + tp.Name + ' de tipo ' + IIF(@ManpowerType = 1, 'Empleado', 'Contratista')
				FROM Common.ThirdParty tp
				WHERE tp.Id = @ThirdParty

				CONTINUE
			END

			-----------------------------------------------------------------------------------------------------------------------

			WHILE @EntityRows > 0
			BEGIN
				SELECT TOP 1
					@EntityId = d.EntityId,
					@TotalAccrued = SUM(d.TotalAccrued),
					@TotalProvision = SUM(d.TotalProvision),
					@TotalEmployerContribution = SUM(d.TotalEmployerContribution),
					@TotalParafiscal = SUM(d.TotalParafiscal),
					-------------------------------------------------------------
					@CostDistributionManpowerDetailRows = 1,
					@CostDistributionManpowerDetailId = 0
				FROM @Details d
				WHERE d.ManpowerType = @ManpowerType
					AND d.ThirdPartyId = @ThirdParty
					AND d.EntityId > @EntityId
				GROUP BY d.EntityId

				SET @EntityRows = @@ROWCOUNT
				IF @EntityRows = 0 
				BEGIN
					BREAK
				END

				-------------------------------------------------------------------------------------------------------------------

				DELETE FROM @CostDistributionManpowerDetail

				-------------------------------------------------------------------------------------------------------------------

				INSERT INTO @CostDistributionManpowerDetail
					SELECT 
						cdmd.ProductionCenterId, 
						cdmd.HoursQuantity, 
						(cdmd.HoursQuantity / @TotalHours * @TotalAccrued) TotalAccrued, 
						(cdmd.HoursQuantity / @TotalHours * @TotalProvision) TotalProvision, 
						(cdmd.HoursQuantity / @TotalHours * @TotalEmployerContribution) TotalEmployerContribution, 
						(cdmd.HoursQuantity / @TotalHours * @TotalParafiscal) TotalParafiscal
					FROM Cost.CostDistributionManpowerDetail cdmd
					WHERE cdmd.DistributionManpowerId = @ManpowerId

				SELECT	@outstandingAccrued = @TotalAccrued - SUM(cdmd.TotalAccrued),
						@outstandingProvision = @TotalProvision - SUM(cdmd.TotalProvision),
						@outstandingEmployerContribution = @TotalEmployerContribution - SUM(cdmd.TotalEmployerContribution),
						@outstandingParafiscal = @TotalParafiscal - SUM(cdmd.TotalParafiscal)
				FROM @CostDistributionManpowerDetail cdmd

				IF @outstandingAccrued <> 0 OR @outstandingProvision <> 0 OR @outstandingParafiscal <> 0 OR @outstandingParafiscal <> 0
				BEGIN
					WHILE @CostDistributionManpowerDetailRows > 0
					BEGIN
						SELECT TOP 1
							@CostDistributionManpowerDetailId = d.Id,
							@adjustedAccrued = Common.CalculateAdjustedValue(@outstandingAccrued, d.TotalAccrued, @outstandingAccrued),
							@adjustedProvision = Common.CalculateAdjustedValue(@outstandingProvision, d.TotalProvision, @outstandingProvision),
							@adjustedEmployerContribution = Common.CalculateAdjustedValue(@outstandingEmployerContribution, d.TotalEmployerContribution, @outstandingEmployerContribution),
							@adjustedParafiscal = Common.CalculateAdjustedValue(@outstandingParafiscal, d.TotalParafiscal, @outstandingParafiscal)
						FROM @CostDistributionManpowerDetail d
						WHERE d.Id > @CostDistributionManpowerDetailId
						ORDER BY d.Id

						SET @CostDistributionManpowerDetailRows = @@ROWCOUNT
						IF @CostDistributionManpowerDetailRows = 0 
						BEGIN
							BREAK
						END

						-------------------------------------------------------------------------------------------------------------------

						UPDATE d
							SET d.TotalAccrued = d.TotalAccrued + @adjustedAccrued,
								d.TotalProvision = d.TotalProvision + @adjustedProvision,
								d.TotalEmployerContribution = d.TotalEmployerContribution + @adjustedEmployerContribution,
								d.TotalParafiscal = d.TotalParafiscal + @adjustedParafiscal
						FROM @CostDistributionManpowerDetail d
						WHERE d.Id = @CostDistributionManpowerDetailId

						SELECT	@outstandingAccrued = @outstandingAccrued - @adjustedAccrued,
								@outstandingProvision = @outstandingProvision - @adjustedProvision,
								@outstandingEmployerContribution = @outstandingEmployerContribution - @adjustedEmployerContribution,
								@outstandingParafiscal = @outstandingParafiscal - @adjustedParafiscal

						IF @outstandingAccrued = 0 AND @outstandingProvision = 0 AND @outstandingParafiscal = 0 AND @outstandingParafiscal = 0
						BEGIN
							BREAK
						END
					END
				END

				/**************************************************** INSERTAR REGISTROS ***************************************************/

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
									@TotalHours HoursWorked,
									SUM(cdm.TotalAccrued) TotalAccrued,SUM(cdm.TotalProvision) TotalProvision,SUM(cdm.TotalEmployerContribution) TotalEmployerContribution,SUM(cdm.TotalParafiscal) TotalParafiscal,
									1 Status
							FROM @Details cdm
							WHERE cdm.ManpowerType = @ManpowerType
								AND cdm.ThirdPartyId = @ThirdParty
								AND cdm.EntityId = @EntityId
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
							FROM @CostDistributionManpowerDetail cdm
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que importa masivamente la distribución de costos de mano de obra (empleados y contratistas) para un año y mes específicos, a partir de un XML de entrada. Antes de procesar, valida que no exista ya una distribución registrada en el mismo período para los terceros incluidos, devolviendo un mensaje de error con los duplicados encontrados (NIT, nombre y tipo de personal). Luego distribuye proporcionalmente las horas trabajadas y los valores de nómina (devengado, provisiones, aportes patronales y parafiscales) entre los centros de producción, consultando la distribución vigente mediante SP_GetDistributionManpower. Afecta la tabla CostDistributionManpower y se utiliza en el cierre contable de costos de personal, garantizando que cada tercero —empleado o contratista— quede asignado correctamente a su centro de costo por período.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ImportCostDistributionManpower';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ImportCostDistributionManpower';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Importa masivamente la distribución de mano de obra del periodo, prorrateando devengados, provisiones, aportes patronales y parafiscales por centro de producción según horas, y persiste cada distribución por tercero/entidad.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ImportCostDistributionManpower';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe recibirse un XML con los Ids de mano de obra a importar (nodo /Data/Id).; Debe existir información de distribución base obtenida de Cost.SP_GetDistributionManpower para el año y mes.; No debe existir previamente una distribución para la misma combinación ManpowerType + ThirdPartyId en el Year/Month indicados.; Las filas referenciadas en @ManpowerIds deben existir en Cost.CostDistributionManpower con HoursWorked > 0 (se usa como divisor).', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ImportCostDistributionManpower';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca se inserta una distribución si ya existe una previa para el mismo tipo de mano de obra y tercero en el periodo.; El total prorrateado por centro de producción debe igualar el total de la entidad (se fuerza vía ajuste de residuos).; El prorrateo de valores se realiza proporcionalmente a HoursQuantity sobre HoursWorked totales.; Cada distribución insertada se crea con Status=1 (activa).; El procesamiento se realiza por tripleta ManpowerType+ThirdPartyId+EntityId, agrupando detalles por centro de producción.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ImportCostDistributionManpower';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Mano de obra; Distribución de costos; Centro de producción; Devengado; Provisión; Aportes patronales; Parafiscales; Empleado; Contratista; Tercero; Horas trabajadas; Unidad operativa; Periodo contable (año/mes)', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ImportCostDistributionManpower';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Cost.CostDistributionManpower: Cuando ya existe una distribución del mismo ManpowerType y ThirdPartyId para el Year/Month, retorna CodeMessage=999 con el listado de terceros duplicados y aborta sin insertar.; [INSERT] Cost.CostDistributionManpower: Por cada combinación ManpowerType+ThirdPartyId+EntityId con detalles existentes, se construye un XML y se delega la inserción a Cost.SP_SaveCostDistributionManpower_Output con OperatingUnitId, Year, Month, HoursWorked totales y Status=1.; [INSERT] Cost.CostDistributionManpowerDetail: Para cada distribución se insertan detalles por ProductionCenterId con valores prorrateados (HoursQuantity/TotalHours * total) de Accrued, Provision, EmployerContribution y Parafiscal, ajustando los residuos por redondeo.; [RETURN_RESULT] Cost.CostDistributionManpower: Si Cost.SP_SaveCostDistributionManpower_Output devuelve Code_Output<>0, se retorna ese código y mensaje y se aborta el proceso.; [RETURN_RESULT] Cost.CostDistributionManpower: Si al finalizar no hay mensajes acumulados, retorna CodeMessage=999 con ''No se encontraron registros pendientes por confirmar''.; [RAISERROR] Cost.CostDistributionManpower: Cualquier excepción en TRY se captura y retorna CodeMessage=999 con ERROR_MESSAGE() y línea.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ImportCostDistributionManpower';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe duplicado de distribución (mismo ManpowerType+ThirdPartyId en Year/Month) → Retorna mensaje 999 con lista de terceros y aborta else Continúa al ciclo de distribución; si No existen detalles en @Details para el ManpowerType+ThirdPartyId actual → Acumula mensaje ''No existen distribuciones...'' y salta al siguiente ManpowerId (CONTINUE) else Procesa las EntityId asociadas; si Diferencias residuales (@outstandingAccrued/Provision/Parafiscal <> 0) tras el prorrateo → Itera los detalles ajustando valores con Common.CalculateAdjustedValue hasta agotar el residuo else Inserta directamente sin ajuste; si Code_Output devuelto por SP_SaveCostDistributionManpower_Output <> 0 → Retorna ese código/mensaje y aborta el procedimiento else Concatena el mensaje y continúa', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ImportCostDistributionManpower';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Cost.SP_GetDistributionManpower; Cost.SP_SaveCostDistributionManpower_Output; Common.CalculateAdjustedValue', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ImportCostDistributionManpower';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostDistributionManpower; Cost.CostDistributionManpowerDetail; Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ImportCostDistributionManpower';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ImportCostDistributionManpower';
-- GO
