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