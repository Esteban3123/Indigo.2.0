-- =============================================
-- Author:		Andrea Pahola Coqueco
-- Create date: 2023-08-17
-- Description:	Procedimiento que se encarga de realizar la distribución del costo por cuadro de turnos para cada empleado
-- =============================================

CREATE PROCEDURE [Payroll].[SP_DistributionByScheduleByEmployee]
(
	@CostDistributionsXML AS XML OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON

	/*===================== DECLARACIÓN DE VARIABLES ======================*/

	DECLARE @DayInitial INT,
			@DayEnd INT,
			@EmployeeId INT,
			@Period VARCHAR(7),
			@LiquidationId INT,
			@UnitFunctionalHours XML

	DECLARE @UnitFunctionalHoursRow AS INT,
			@UnitFunctionalHoursTotal AS INT,
			---------- Control While --------------
			@CostDistributionRow AS INT,
			@CostDistributionId AS INT,
			@FunctionalUnitRow AS INT,
			@FunctionalUnitId AS INT,
			@FunctionalUnitCounter AS INT,
			-------- Cálculo/Distribución  --------			
			@CostDistributionDebitValue DECIMAL(18,0),
			@CostDistributionCreditValue DECIMAL(18,0),
			@FunctionalUnitHour INT,
			@FunctionalUnitDebitValue DECIMAL(18,0),
			@FunctionalUnitCreditValue DECIMAL(18,0)

	DECLARE @CostDistributionsInput TABLE
	(
		[Id] [int]  NOT NULL,	
		[LiquidationId] [int] NOT NULL,	
		[LiquidationDetailId] [int] NOT NULL,	
		[JournalVoucherTypeId] [int] NOT NULL,
		[MainAccountId] [int] NULL,
		[MainAccountNumber] [varchar](50) NOT NULL,
		[ThirdPartyId] [int] NULL,
		[CostCenterId] [int] NULL,
		[DebitValue] DECIMAL(18,0) DEFAULT(0),
		[CreditValue] DECIMAL(18,0) DEFAULT(0),
		-----------------------------------------------
		[EmployeeId] INT,
		[NumberHours] INT DEFAULT(0),
		[Detail] VARCHAR(500),
		[BaseValue] DECIMAL(18,2) DEFAULT(0),
		[IdRetention] INT NULL
	)

	DECLARE @CostDistributionsOutput TABLE
	(
		[Id] [int] IDENTITY(1,1) NOT NULL,	
		[LiquidationId] [int] NOT NULL,	
		[LiquidationDetailId] [int] NOT NULL,	
		[JournalVoucherTypeId] [int] NOT NULL,
		[MainAccountId] [int] NULL,
		[MainAccountNumber] [varchar](50) NOT NULL,
		[ThirdPartyId] [int] NULL,
		[CostCenterId] [int] NULL,
		[DebitValue] DECIMAL(18,0) DEFAULT(0),
		[CreditValue] DECIMAL(18,0) DEFAULT(0),
		-----------------------------------------------
		[EmployeeId] INT,
		[NumberHours] INT DEFAULT(0),
		[Detail] VARCHAR(500),
		[BaseValue] DECIMAL(18,2) DEFAULT(0),
		[IdRetention] INT NULL
	)

	DECLARE @TableUnitFunctionalHours TABLE
	(
		FunctionalUnitId INT,
		[Hours] INT
	)

	/*===================== ASIGNACIÓN DE DATOS ======================*/

	INSERT INTO @CostDistributionsInput
		SELECT	t.x.value('Id[1]','int') AS Id,
				t.x.value('LiquidationId[1]','int') AS LiquidationId,
				t.x.value('LiquidationDetailId[1]','int') AS LiquidationDetailId,
				t.x.value('JournalVoucherTypeId[1]','int') AS JournalVoucherTypeId,
				t.x.value('MainAccountId[1]','int') AS MainAccountId,
				t.x.value('MainAccountNumber[1]','varchar(50)') AS MainAccountNumber,
				t.x.value('ThirdPartyId[1]','int') AS ThirdPartyId,
				t.x.value('CostCenterId[1]','int') AS CostCenterId,
				t.x.value('DebitValue[1]','decimal(18,0)') AS DebitValue,
				t.x.value('CreditValue[1]','decimal(18,0)') AS CreditValue,
				t.x.value('EmployeeId[1]','int') AS EmployeeId,
				t.x.value('NumberHours[1]','int') AS NumberHours,
				t.x.value('Detail[1]','varchar(500)') AS Detail,
				t.x.value('BaseValue[1]','decimal(18,0)') AS BaseValue,
				t.x.value('IdRetention[1]','int') AS IdRetention
		FROM @CostDistributionsXML.nodes('/CostDistributions') t(x)

	SELECT TOP 1
			@EmployeeId = cdi.EmployeeId,
			@LiquidationId = cdi.LiquidationId
	FROM @CostDistributionsInput cdi
	
	-- SP para obtener el periodo liquidado
	EXEC [Payroll].[SP_GetPeriodLiquidatedByLiquidationId]	@LiquidationId,
															@Period OUTPUT,
															@DayInitial OUTPUT,
															@DayEnd OUTPUT

	-- SP para obtener las unidades funcionales y horas por cada unidad funcional según cuadro de turnos
	EXEC [Payroll].[SP_GetUnitFunctionalHoursByScheduleOfEmployee]	@EmployeeId,
																	@Period,
																	@DayInitial,
																	@DayEnd,
																	@UnitFunctionalHours OUTPUT

	INSERT INTO @TableUnitFunctionalHours
		SELECT	t.x.value('FunctionalUnitId[1]','int') AS FunctionalUnitId,
				t.x.value('Hours[1]','int') AS [Hours]
		FROM @UnitFunctionalHours.nodes('/UnitFunctionalHours') t(x)	

	SELECT	@UnitFunctionalHoursRow = COUNT(1),
			@UnitFunctionalHoursTotal = SUM(Hours)
	FROM @TableUnitFunctionalHours

	-- Si el SP_GetUnitFunctionalHoursByScheduleOfEmployee no trae resultados
	IF @UnitFunctionalHoursRow = 0
	BEGIN
		RETURN
	END
	

	/*===================== PROCESAMIENTO DE DATOS ======================*/

	-- Proceso para registros que no tienen centro de costo (Cuentas que no manejan centro de costo No requieren distribuición)
	INSERT INTO @CostDistributionsOutput
	(
		LiquidationId, LiquidationDetailId, 
		JournalVoucherTypeId, MainAccountId, MainAccountNumber, ThirdPartyId, CostCenterId, 
		DebitValue, CreditValue, EmployeeId, NumberHours, Detail, BaseValue, IdRetention
	)
		SELECT	LiquidationId, LiquidationDetailId, 
				JournalVoucherTypeId, MainAccountId, MainAccountNumber, ThirdPartyId, CostCenterId, 
				DebitValue, CreditValue, EmployeeId, NumberHours, Detail, BaseValue, IdRetention
		FROM @CostDistributionsInput
		WHERE ISNULL(CostCenterId, 0) = 0

	DELETE cdi
	FROM @CostDistributionsInput cdi
	WHERE ISNULL(CostCenterId, 0) = 0

	-------------------------------------------------------------------------

	SET @CostDistributionRow = 1
	SET @CostDistributionId = 0

	-- Se recorren los registros del @CostDistributionsInput (Distribución Inicial)
	WHILE @CostDistributionRow > 0
	BEGIN
		SELECT TOP 1
			@CostDistributionId = cdi.Id,
			@CostDistributionDebitValue = cdi.DebitValue,
			@CostDistributionCreditValue = cdi.CreditValue,
			-----------------------------
			@FunctionalUnitRow = 1,
			@FunctionalUnitId = 0,
			@FunctionalUnitCounter = 0
		FROM @CostDistributionsInput cdi
		WHERE cdi.Id > @CostDistributionId
		ORDER BY cdi.Id

		SET @CostDistributionRow = @@ROWCOUNT
		IF @CostDistributionRow = 0 
		BEGIN
			BREAK
		END

		-------------------------------------------------------------------------

		-- Se recorren las unidades funcionales de la distribución Inicial	
		WHILE @FunctionalUnitRow > 0
		BEGIN
			SELECT TOP 1
				@FunctionalUnitId = tufh.FunctionalUnitId,
				@FunctionalUnitHour = tufh.Hours,
				-----------------------------
				@FunctionalUnitCounter = @FunctionalUnitCounter + 1
			FROM @TableUnitFunctionalHours tufh
			WHERE tufh.FunctionalUnitId > @FunctionalUnitId
			ORDER BY tufh.FunctionalUnitId

			SET @FunctionalUnitRow = @@ROWCOUNT
			IF @FunctionalUnitRow = 0 
			BEGIN
				BREAK
			END

			---------------------------------------------------------------------

			--	-- % a distribuir es igual a hora de la unidad actual / total horas
			SELECT	@FunctionalUnitDebitValue = ROUND(cdi.DebitValue * @FunctionalUnitHour / @UnitFunctionalHoursTotal, 2),
					@FunctionalUnitCreditValue = ROUND(cdi.CreditValue * @FunctionalUnitHour / @UnitFunctionalHoursTotal, 2)
			FROM @CostDistributionsInput cdi
			WHERE cdi.Id = @CostDistributionId

			--	-- Validación para no superar el valor a distribuir (deb o crd)
			SELECT	@FunctionalUnitDebitValue = IIF(@FunctionalUnitDebitValue > @CostDistributionDebitValue, @CostDistributionDebitValue, @FunctionalUnitDebitValue),
					@FunctionalUnitCreditValue = IIF(@FunctionalUnitCreditValue > @CostDistributionCreditValue, @CostDistributionCreditValue, @FunctionalUnitCreditValue)

			--	-- Distribución total para el último ciclo (Por redondeo)
			SELECT	@FunctionalUnitDebitValue = IIF(@FunctionalUnitCounter = @UnitFunctionalHoursRow, @CostDistributionDebitValue, @FunctionalUnitDebitValue),
					@FunctionalUnitCreditValue = IIF(@FunctionalUnitCounter = @UnitFunctionalHoursRow, @CostDistributionCreditValue, @FunctionalUnitCreditValue)

			--	-- Actualización de variable acumulativa
			SELECT	@CostDistributionDebitValue = @CostDistributionDebitValue - @FunctionalUnitDebitValue,
					@CostDistributionCreditValue = @CostDistributionCreditValue - @FunctionalUnitCreditValue

			-- Se insertan datos en la variable de salida mientras exista un valor a distribuir
			IF @FunctionalUnitDebitValue <> 0 OR @FunctionalUnitCreditValue <> 0
			BEGIN
				INSERT INTO @CostDistributionsOutput
				(
					LiquidationId, LiquidationDetailId, 
					JournalVoucherTypeId, MainAccountId, MainAccountNumber, ThirdPartyId, CostCenterId, 
					DebitValue, CreditValue, EmployeeId, 
					NumberHours, Detail, BaseValue, IdRetention
				)
					SELECT	cdi.LiquidationId, cdi.LiquidationDetailId, 
							cdi.JournalVoucherTypeId, cdi.MainAccountId, cdi.MainAccountNumber, cdi.ThirdPartyId, fu.CostCenterId, 
							@FunctionalUnitDebitValue DebitValue, @FunctionalUnitCreditValue CreditValue, cdi.EmployeeId, 
							@FunctionalUnitHour NumberHours, cdi.Detail, cdi.BaseValue, cdi.IdRetention 
					FROM @CostDistributionsInput cdi
					JOIN Payroll.FunctionalUnit fu ON fu.Id = @FunctionalUnitId
					WHERE cdi.Id = @CostDistributionId
			END
		END
	END

	/*===================== RESULTADO ======================*/

	SELECT @CostDistributionsXML = CONVERT(xml, 
		(
			SELECT * FROM @CostDistributionsOutput CostDistributions
			For xml AUTO,TYPE, ELEMENTS
		)
	)
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de nómina que distribuye el costo salarial de un empleado entre las unidades funcionales (centros de costo) según las horas trabajadas por turno en el período de liquidación. Recibe las distribuciones de costo iniciales en formato XML, consulta el período liquidado mediante SP_GetPeriodLiquidatedByLiquidationId y las horas por unidad funcional según el cuadro de turnos mediante SP_GetUnitFunctionalHoursByScheduleOfEmployee, y luego prorratea los valores de débito y crédito de cada concepto de nómina proporcionalmente a las horas que el empleado laboró en cada centro de costo. Las cuentas contables sin centro de costo se pasan directamente sin distribución, mientras que las que sí tienen centro de costo se dividen proporcionalmente por horas, generando los movimientos contables (comprobante de diario) detallados por liquidación, empleado, cuenta principal, tercero y centro de costo.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_DistributionByScheduleByEmployee';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_DistributionByScheduleByEmployee';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Distribuye proporcionalmente los valores débito/crédito de la liquidación de nómina de un empleado entre las unidades funcionales (centros de costo) según las horas de su cuadro de turnos en el periodo.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_DistributionByScheduleByEmployee';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe seguir el esquema /CostDistributions con los nodos esperados (Id, LiquidationId, DebitValue, CreditValue, EmployeeId, etc.).; Debe existir al menos un registro en el XML para poder obtener EmployeeId y LiquidationId (se toma TOP 1).; El LiquidationId debe permitir resolver un periodo válido vía SP_GetPeriodLiquidatedByLiquidationId.; Las unidades funcionales referenciadas deben existir en Payroll.FunctionalUnit para poder obtener su CostCenterId.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_DistributionByScheduleByEmployee';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las cuentas sin centro de costo (CostCenterId nulo o 0) nunca se distribuyen; se conservan idénticas en la salida.; La suma de los valores distribuidos por unidad funcional para cada registro de entrada es igual al débito/crédito original (la última unidad absorbe el residuo de redondeo).; El porcentaje asignado a cada unidad funcional es proporcional a sus horas dentro del total de horas del cuadro de turnos del empleado.; Ninguna asignación parcial puede exceder el saldo pendiente del registro original (truncamiento al tope).; El CostCenterId final de cada fila distribuida proviene de Payroll.FunctionalUnit, no del registro de entrada.; Si no existen horas/unidades funcionales para el empleado en el periodo, no se produce salida alguna.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_DistributionByScheduleByEmployee';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Distribución de costos de nómina; Cuadro de turnos del empleado; Unidad funcional; Centro de costo; Liquidación de nómina; Periodo liquidado; Comprobante contable (débito/crédito)', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_DistributionByScheduleByEmployee';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] @CostDistributionsXML: Devuelve por parámetro OUTPUT un XML con la distribución final (registros sin centro de costo + registros distribuidos por unidad funcional con CostCenterId resuelto desde Payroll.FunctionalUnit).; [RETURN_RESULT] @CostDistributionsXML: Si SP_GetUnitFunctionalHoursByScheduleOfEmployee no retorna filas (@UnitFunctionalHoursRow = 0), el procedimiento retorna sin modificar el XML de salida.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_DistributionByScheduleByEmployee';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si El SP de horas por unidad funcional no devuelve filas (@UnitFunctionalHoursRow = 0) → Se retorna sin generar distribución alguna (RETURN) else Continúa con la distribución por unidad funcional; si ISNULL(CostCenterId,0) = 0 en el registro de entrada → El registro pasa tal cual al resultado de salida sin distribuirse y se elimina del input else El registro entra al ciclo de distribución proporcional por unidad funcional; si Es la última unidad funcional procesada (@FunctionalUnitCounter = @UnitFunctionalHoursRow) → Se asigna a esa unidad el saldo restante completo de débito/crédito para evitar diferencias por redondeo else Se asigna el monto proporcional calculado (valor * horas_unidad / total_horas); si El monto proporcional calculado supera el saldo pendiente del registro original → Se trunca al saldo pendiente (@CostDistributionDebitValue / @CostDistributionCreditValue); si @FunctionalUnitDebitValue <> 0 OR @FunctionalUnitCreditValue <> 0 → Se inserta una fila de distribución en la salida con el CostCenterId tomado de Payroll.FunctionalUnit else No se inserta fila para esa unidad funcional', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_DistributionByScheduleByEmployee';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Payroll.SP_GetPeriodLiquidatedByLiquidationId; Payroll.SP_GetUnitFunctionalHoursByScheduleOfEmployee', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_DistributionByScheduleByEmployee';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.FunctionalUnit', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_DistributionByScheduleByEmployee';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_DistributionByScheduleByEmployee';
-- GO
