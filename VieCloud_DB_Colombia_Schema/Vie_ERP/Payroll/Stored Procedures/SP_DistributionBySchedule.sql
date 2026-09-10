-- =============================================
-- Author:		Andrea Pahola Coqueco
-- Create date: 2023-08-17
-- Description:	Procedimiento que se encarga de realizar la distribución del costo por cuadro de turnos
-- =============================================

CREATE PROCEDURE [Payroll].[SP_DistributionBySchedule]
(
	@CostDistributionsXML AS XML OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON

	/*===================== DECLARACIÓN DE VARIABLES ======================*/

	DECLARE @EmployeeId AS INT,
			@EmployeeRow AS INT

	DECLARE @CostDistributionsInput TABLE
	(
		[Id] [int] NOT NULL,	
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

	/*===================== PROCESAMIENTO POR EMPLEADO ======================*/

	SET @EmployeeRow = 1
	SET @EmployeeId = 0

	WHILE @EmployeeRow > 0
	BEGIN
		SELECT TOP 1
			@EmployeeId = cdi.EmployeeId
		FROM @CostDistributionsInput cdi
		WHERE cdi.EmployeeId > @EmployeeId
		ORDER BY cdi.EmployeeId

		SET @EmployeeRow = @@ROWCOUNT
		IF @EmployeeRow = 0 
		BEGIN
			BREAK
		END

		-------------------------------------------------------------------------

		SELECT @CostDistributionsXML = CONVERT(xml, 
			(
				SELECT * 
				FROM @CostDistributionsInput CostDistributions
				WHERE CostDistributions.EmployeeId = @EmployeeId
				For xml AUTO,TYPE, ELEMENTS
			)
		)

		EXEC [Payroll].[SP_DistributionByScheduleByEmployee] @CostDistributionsXML OUTPUT

		INSERT INTO @CostDistributionsOutput
		(
			LiquidationId, LiquidationDetailId, 
			JournalVoucherTypeId, MainAccountId, MainAccountNumber, ThirdPartyId, CostCenterId, 
			DebitValue, CreditValue, EmployeeId, NumberHours, Detail, BaseValue, IdRetention
		)
			SELECT	t.x.value('LiquidationId[1]','int') AS LiquidationId,
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de nómina que realiza la distribución del costo de la liquidación de empleados según el cuadro de turnos trabajados. Recibe como entrada un XML con las distribuciones de costo pendientes de repartir (cada registro incluye la liquidación, el detalle, la cuenta contable, el centro de costo, el tercero y los valores débito/crédito), y para cada empleado involucrado invoca el subprocedimiento SP_DistributionByScheduleByEmployee que ejecuta la distribución individual por turnos. Consolida los resultados de todos los empleados procesados y devuelve el XML final con las distribuciones de costo calculadas, listas para ser contabilizadas en el comprobante de diario (asiento contable) de la nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_DistributionBySchedule';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_DistributionBySchedule';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Orquesta la distribución contable del costo de nómina recorriendo, empleado por empleado, las filas recibidas en XML y delegando el cálculo individual al procedimiento de distribución por empleado, para luego consolidar el resultado en un único XML de salida.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_DistributionBySchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro XML debe seguir la estructura /CostDistributions con los nodos hijos esperados (Id, LiquidationId, LiquidationDetailId, JournalVoucherTypeId, MainAccountId/Number, ThirdPartyId, CostCenterId, DebitValue, CreditValue, EmployeeId, NumberHours, Detail, BaseValue, IdRetention); Cada fila del XML debe tener un EmployeeId válido (>0) para ser tomada por el cursor que filtra cdi.EmployeeId > @EmployeeId', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_DistributionBySchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El XML de entrada se itera por EmployeeId de forma ascendente y única, garantizando que cada empleado se procese una sola vez; Solo se acumulan en la salida las filas devueltas por SP_DistributionByScheduleByEmployee, descartando las originales no procesadas; La salida final reemplaza el contenido del parámetro XML de entrada/salida con el conjunto consolidado; El Id de la salida se regenera mediante IDENTITY, no se conserva el Id original de entrada', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_DistributionBySchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Distribución de costos de nómina; Cuadro de turnos; Liquidación; Empleado; Centro de costo; Tercero; Cuenta contable; Retención', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_DistributionBySchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] @CostDistributionsXML: Al finalizar, sobrescribe el parámetro OUTPUT con el XML resultante de @CostDistributionsOutput (unión de las distribuciones procesadas por cada empleado); [RETURN_RESULT] @CostDistributionsXML: Dentro del bucle, reasigna el XML con el subconjunto de filas del empleado actual antes de invocar SP_DistributionByScheduleByEmployee', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_DistributionBySchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si No existe un siguiente EmployeeId mayor al ya procesado (@@ROWCOUNT = 0) → Termina el bucle WHILE y consolida el resultado final else Procesa la distribución de costos para ese empleado invocando SP_DistributionByScheduleByEmployee', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_DistributionBySchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Payroll.SP_DistributionByScheduleByEmployee', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_DistributionBySchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_DistributionBySchedule';
-- GO
