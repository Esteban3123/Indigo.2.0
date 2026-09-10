	-- =============================================
	-- Author:		<Juan Montealegre>
	-- Create date: <27-07-2014>
	-- Description:	<Listar el cuadro de turno>
	-- =============================================
	CREATE PROCEDURE [Payroll].[SP_GetSchedule]
	@UnidadFuncional int,
	@Periodo varchar(7)
	AS
	BEGIN
		-- SET NOCOUNT ON added to prevent extra result sets from
		-- interfering with SELECT statements.
		SET NOCOUNT ON;

	SELECT 

	a.EmployeeId ,
	c.Name 'NombreEmpleado' ,
		
			----Letra turno asignado
			dbo.[GetLetter](D01) 'D1',
			dbo.[GetLetter](D02) 'D2',
			dbo.[GetLetter](D03) 'D3',
			dbo.[GetLetter](D04) 'D4',
			dbo.[GetLetter](D05) 'D5',
			dbo.[GetLetter](D06) 'D6',
			dbo.[GetLetter](D07) 'D7',
			dbo.[GetLetter](D08) 'D8',
			dbo.[GetLetter](D09) 'D9',
			dbo.[GetLetter](D10) 'D10',
			dbo.[GetLetter](D11) 'D11',
			dbo.[GetLetter](D12) 'D12',
			dbo.[GetLetter](D13) 'D13',
			dbo.[GetLetter](D14) 'D14',
			dbo.[GetLetter](D15) 'D15',
			dbo.[GetLetter](D16) 'D16',
			dbo.[GetLetter](D17) 'D17',
			dbo.[GetLetter](D18) 'D18',
			dbo.[GetLetter](D19) 'D19',
			dbo.[GetLetter](D20) 'D20',
			dbo.[GetLetter](D21) 'D21',
			dbo.[GetLetter](D22) 'D22',
			dbo.[GetLetter](D23) 'D23',
			dbo.[GetLetter](D24) 'D24',
			dbo.[GetLetter](D25) 'D25',
			dbo.[GetLetter](D26) 'D26',
			dbo.[GetLetter](D27) 'D27',
			dbo.[GetLetter](D28) 'D28',
			dbo.[GetLetter](D29) 'D29',
			dbo.[GetLetter](D30) 'D30',
			dbo.[GetLetter](D30) 'D31',
			A.TotalHour 'TotalHoras'

			 FROM 
			 payroll.SCHEDULE A 
			 INNER JOIN
			 Payroll.Employee B ON A.EmployeeId = B.Id 
			 INNER JOIN
			 Common.ThirdParty C ON B.ThirdPartyId = C.Id 

		WHERE
			A.FunctionalUnitId =@unidadfuncional and A.Period = @periodo

			-- select * from [Payroll].[VistaLetra2]

	END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta y devuelve el cuadro de turnos mensual de los empleados asignados a una unidad funcional y período (mes/año) específicos. Para cada empleado muestra su nombre completo (obtenido desde el registro de terceros) y la letra o código del turno correspondiente a cada día del mes (del día 1 al 31), calculada mediante la función GetLetter a partir de la programación registrada en la tabla de horarios. También incluye el total de horas programadas en el período. Se utiliza en nómina y gestión de talento humano para visualizar y verificar la programación laboral mensual por unidad funcional.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_GetSchedule';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_GetSchedule';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista el cuadro de turnos mensual por empleado de una unidad funcional, mostrando la letra del turno asignado para cada uno de los 31 días del periodo y el total de horas.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en payroll.SCHEDULE que coincida con la unidad funcional y el periodo solicitados.; Cada empleado en SCHEDULE debe tener su correspondiente registro en Payroll.Employee y a su vez en Common.ThirdParty (los INNER JOIN excluyen huérfanos).; El periodo se entrega como cadena de 7 caracteres (formato tipo ''YYYY-MM'').', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La columna ''D31'' del resultado se calcula a partir de D30 (no de un campo D31), por lo que D30 y D31 siempre muestran la misma letra de turno.; Solo se incluyen empleados que tengan a la vez registro de Schedule, Employee y ThirdParty (INNER JOIN en cascada).; Cada celda diaria se traduce de un código interno a una letra de turno mediante dbo.GetLetter.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuadro de turnos; Empleado de nómina; Unidad funcional; Periodo de nómina; Letra/código de turno; Total de horas trabajadas; Tercero', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve una fila por empleado con EmployeeId, nombre, las letras de turno de D1..D31 (vía dbo.GetLetter) y TotalHoras, filtrado por FunctionalUnitId=@UnidadFuncional y Period=@Periodo.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.GetLetter', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'payroll.SCHEDULE; Payroll.Employee; Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetSchedule';
-- GO
