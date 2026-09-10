-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [HumanTalent].[InsertAnnualVacationSchedule]
	-- Add the parameters for the stored procedure here
	@RequestDays INT,
	@EmployeeId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	
	SET NOCOUNT ON;
	DECLARE @SumPendingDays INT, @VacationPeriodId INT,@VacationPeriodPendingDays INT, @RowIndexCount INT, @DaysToken INT;
    -- Insert statements for procedure here
	
	SET @SumPendingDays = 0;
	SET @RowIndexCount = 1;
	--Obtener en una tabla temporal los Id's de los Vacation Periods y los index ordenada de manera descendente por fecha inicial
	SELECT Id, PendingDays INTO #TempVacationPeriod FROM Payroll.VacationPeriod WHERE (EmployeeId = @EmployeeId AND PendingDays > 0) ORDER BY InitialDatePeriod DESC;
	create table #Temp
	( 
		id INT IDENTITY NOT NULL PRIMARY KEY,
		VacationPeriodId INT,
		PendingDays INT,
		RequestDays INT,
		DaysTaken INT,
		SumPendingDays INT
		
	)
	WHILE @SumPendingDays < @RequestDays
	BEGIN
		WITH OrderedOrders AS  
		(  
		   SELECT ROW_NUMBER() OVER(ORDER BY Id ASC) AS RowIndex, Id, PendingDays  FROM #TempVacationPeriod  
		)   
		SELECT @VacationPeriodId=Id, @VacationPeriodPendingDays=OrderedOrders.PendingDays
		FROM OrderedOrders  WHERE RowIndex = @RowIndexCount;  
		 
		 
		
		SET @SumPendingDays = @SumPendingDays + @VacationPeriodPendingDays;
		
		--SET @DaysToken =  @VacationPeriodPendingDays -@SumPendingDays;
		--SELECT Id, PendingDays INTO #Temp FROM #TempVacationPeriod WHERE Id = @VacationPeriodId;
		INSERT INTO #Temp
		(
		    VacationPeriodId,
			PendingDays,
			RequestDays,
			SumPendingDays
		)
		VALUES
		(   
			@VacationPeriodId, -- VacationPeriodId - int		
			@VacationPeriodPendingDays,
			@DaysToken,
			@SumPendingDays
		)
		SET @RowIndexCount = @RowIndexCount + 1;
	END;
END
SELECT * FROM #Temp;
--INSERT INTO Payroll.VacationPeriod
--(
--    EmployeeId,
--    ContractId,
--    InitialDatePeriod,
--    EndDatePeriod,
--    VacationDays,
--    PendingDays,
--    TakenDays,
--    CreationUser,
--    CreationDate,
--    ModificationUser,
--    ModificationDate
--)
--VALUES
--(   @EmployeeId,         -- EmployeeId - int
--    (SELECT ContractId FROM Payroll.Contract WHERE @EmployeeId),         -- ContractId - int
--    [Common].[GETDATE](), -- InitialDatePeriod - date
--    [Common].[GETDATE](), -- EndDatePeriod - date
--    0,         -- VacationDays - tinyint
--    0,         -- PendingDays - tinyint
--    0,         -- TakenDays - tinyint
--    '',        -- CreationUser - varchar(20)
--    [Common].[GETDATE](), -- CreationDate - datetime
--    '',        -- ModificationUser - varchar(20)
--    [Common].[GETDATE]()  -- ModificationDate - datetime
--    )
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula y distribuye los días de vacaciones solicitados por un empleado entre sus períodos vacacionales pendientes. Recibe como parámetros el identificador del empleado y la cantidad de días solicitados, luego consulta los períodos de vacaciones con días pendientes en Payroll.VacationPeriod (ordenados desde el más reciente) y los va acumulando período por período hasta cubrir el total de días pedidos. Retorna un detalle temporal que muestra qué períodos vacacionales se afectan, cuántos días pendientes tiene cada uno y el acumulado de días cubiertos, sirviendo como base para programar el cronograma anual de vacaciones del empleado en el módulo de Talento Humano y Nómina.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'PROCEDURE', @level1name = N'InsertAnnualVacationSchedule';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'PROCEDURE', @level1name = N'InsertAnnualVacationSchedule';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Distribuye los días de vacaciones solicitados por un empleado entre sus períodos vacacionales con días pendientes, devolviendo el desglose tentativo sin persistir cambios.', @level0type=N'SCHEMA', @level0name=N'HumanTalent', @level1type=N'PROCEDURE', @level1name=N'InsertAnnualVacationSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El empleado debe tener al menos un registro en Payroll.VacationPeriod con PendingDays > 0; de lo contrario el bucle WHILE no dispone de filas y puede iterar indefinidamente.; Los días solicitados deben ser representables como INT positivo.', @level0type=N'SCHEMA', @level0name=N'HumanTalent', @level1type=N'PROCEDURE', @level1name=N'InsertAnnualVacationSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran períodos vacacionales del empleado con PendingDays > 0.; Los períodos se cargan inicialmente ordenados de forma descendente por InitialDatePeriod (más recientes primero) en la temporal de origen, pero se consumen iterativamente por Id ascendente vía ROW_NUMBER.; El procedimiento no modifica Payroll.VacationPeriod ni inserta registros nuevos: la lógica de persistencia está comentada.; El campo RequestDays/DaysTaken insertado en #Temp queda con @DaysToken sin asignar (siempre NULL), por lo que no representa días realmente tomados.', @level0type=N'SCHEMA', @level0name=N'HumanTalent', @level1type=N'PROCEDURE', @level1name=N'InsertAnnualVacationSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Vacaciones anuales; Período vacacional; Días pendientes de vacaciones; Días solicitados; Empleado', @level0type=N'SCHEMA', @level0name=N'HumanTalent', @level1type=N'PROCEDURE', @level1name=N'InsertAnnualVacationSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] #Temp: Por cada período vacacional del empleado con PendingDays > 0, mientras la suma acumulada de pendientes no alcance los días solicitados, se inserta una fila con VacationPeriodId, PendingDays del período y la suma acumulada.; [RETURN_RESULT] #Temp: Al finalizar, retorna SELECT * FROM #Temp con el desglose de períodos consumidos para cubrir la solicitud.', @level0type=N'SCHEMA', @level0name=N'HumanTalent', @level1type=N'PROCEDURE', @level1name=N'InsertAnnualVacationSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si WHILE @SumPendingDays < @RequestDays → Toma el siguiente período vacacional ordenado por Id ascendente dentro de los pendientes y acumula sus PendingDays hasta cubrir la solicitud.', @level0type=N'SCHEMA', @level0name=N'HumanTalent', @level1type=N'PROCEDURE', @level1name=N'InsertAnnualVacationSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.VacationPeriod', @level0type=N'SCHEMA', @level0name=N'HumanTalent', @level1type=N'PROCEDURE', @level1name=N'InsertAnnualVacationSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'HumanTalent', @level1type=N'PROCEDURE', @level1name=N'InsertAnnualVacationSchedule';
-- GO
