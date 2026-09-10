CREATE procedure [Payroll].[SP_ReportShiftBox]
@xmlCriterias As Xml 
As
DECLARE @Period As VARCHAR(20),
		@FunctionalUnitCodeInitial As VARCHAR(20),
		@FunctionalUnitCodeEnding As VARCHAR(20),
		@BranchOfficeCodeInitial As VARCHAR(20),
		@BranchOfficeCodeEnding As VARCHAR(20),
		@EmployeeId As VARCHAR(20),
		@Year AS Varchar(20),
		@Month As Varchar(20),
		@allRecords As bit

SELECT	@Period = t.x.value('Period[1]','VARCHAR(20)'),
		@FunctionalUnitCodeInitial = t.x.value('FunctionalUnitCodeInitial[1]','VARCHAR(20)'),
		@FunctionalUnitCodeEnding = t.x.value('FunctionalUnitCodeEnding[1]','VARCHAR(20)'),
		@BranchOfficeCodeInitial = t.x.value('BranchOfficeCodeInitial[1]','VARCHAR(20)'),
		@BranchOfficeCodeEnding = t.x.value('BranchOfficeCodeEnding[1]','VARCHAR(20)'),
		@EmployeeId = t.x.value('EmployeeId[1]','VARCHAR(20)'),
		@allRecords = t.x.value('allRecords[1]','VARCHAR(20)')
FROM @xmlCriterias.nodes('/Data') t(x)

set @Month = substring(@Period,4,2)
set @Year = substring(@Period,7,4)
set @Period = @Month + '/' + @Year 

if @allRecords = 1 begin
	with CTEHoursDetailByEmployee As
	(
			select 
				sd.EmployeeId
				,SUM(Case When sdh.Event = 0 Then convert(numeric(5,0), sdh.TotalNumberHours) Else 0 End) NormalHours
				,SUM(Case When sdh.Event = 1 And sdh.Approved = 1 Then convert(numeric(5,0), sdh.TotalNumberHours) Else 0 End) EventHoursApproved 
			from Payroll.ScheduleDetail sd
			left join Payroll.ScheduleDetailHour sdh on sd.id = sdh.ScheduleDetailId
			left join Payroll.FunctionalUnit fu on sd.FunctionalUnitId = fu.Id
			left join Payroll.BranchOffice bo ON fu.BranchOfficeId = bo.Id
			WHERE sd.DateDetail BETWEEN @Year+'-'+@Month+'-01' And @Year+'-'+@Month+'-31'
			group by sd.EmployeeId
	)
	Select 
		s.EmployeeId 
		,tp.Name EmployeeName
		,s.Period 
		,sd1.Letter D01
		,sd2.Letter D02
		,sd3.Letter D03
		,sd4.Letter D04
		,sd5.Letter D05
		,sd6.Letter D06
		,sd7.Letter D07
		,sd8.Letter D08
		,sd9.Letter D09
		,sd10.Letter D10
		,sd11.Letter D11
		,sd12.Letter D12
		,sd13.Letter D13
		,sd14.Letter D14
		,sd15.Letter D15
		,sd16.Letter D16
		,sd17.Letter D17
		,sd18.Letter D18
		,sd19.Letter D19
		,sd20.Letter D20
		,sd21.Letter D21
		,sd22.Letter D22
		,sd23.Letter D23
		,sd24.Letter D24
		,sd25.Letter D25
		,sd26.Letter D26
		,sd27.Letter D27
		,sd28.Letter D28
		,sd29.Letter D29
		,sd30.Letter D30
		,sd31.Letter D31
		,fu.Id FunctionalUnitId
		,fu.Code FunctionalUnitCode
		,fu.Name FunctionalUnitName
		,cte.NormalHours
		,cte.EventHoursApproved
	From Payroll.Schedule As s
	Left Join Payroll.ScheduleDetail As sd1 On s.D01 = sd1.Id
	Left Join Payroll.ScheduleDetail As sd2 On s.D02 = sd2.Id
	Left Join Payroll.ScheduleDetail As sd3 On s.D03 = sd3.Id
	Left Join Payroll.ScheduleDetail As sd4 On s.D04 = sd4.Id
	Left Join Payroll.ScheduleDetail As sd5 On s.D05 = sd5.Id
	Left Join Payroll.ScheduleDetail As sd6 On s.D06 = sd6.Id
	Left Join Payroll.ScheduleDetail As sd7 On s.D07 = sd7.Id
	Left Join Payroll.ScheduleDetail As sd8 On s.D08 = sd8.Id
	Left Join Payroll.ScheduleDetail As sd9 On s.D08 = sd9.Id
	Left Join Payroll.ScheduleDetail As sd10 On s.D10 = sd10.Id
	Left Join Payroll.ScheduleDetail As sd11 On s.D11 = sd11.Id
	Left Join Payroll.ScheduleDetail As sd12 On s.D12 = sd12.Id
	Left Join Payroll.ScheduleDetail As sd13 On s.D13 = sd13.Id
	Left Join Payroll.ScheduleDetail As sd14 On s.D14 = sd14.Id
	Left Join Payroll.ScheduleDetail As sd15 On s.D15 = sd15.Id
	Left Join Payroll.ScheduleDetail As sd16 On s.D16 = sd16.Id
	Left Join Payroll.ScheduleDetail As sd17 On s.D17 = sd17.Id
	Left Join Payroll.ScheduleDetail As sd18 On s.D18 = sd18.Id
	Left Join Payroll.ScheduleDetail As sd19 On s.D19 = sd19.Id
	Left Join Payroll.ScheduleDetail As sd20 On s.D20 = sd20.Id
	Left Join Payroll.ScheduleDetail As sd21 On s.D21 = sd21.Id
	Left Join Payroll.ScheduleDetail As sd22 On s.D22 = sd22.Id
	Left Join Payroll.ScheduleDetail As sd23 On s.D23 = sd23.Id
	Left Join Payroll.ScheduleDetail As sd24 On s.D24 = sd24.Id
	Left Join Payroll.ScheduleDetail As sd25 On s.D25 = sd25.Id
	Left Join Payroll.ScheduleDetail As sd26 On s.D26 = sd26.Id
	Left Join Payroll.ScheduleDetail As sd27 On s.D27 = sd27.Id
	Left Join Payroll.ScheduleDetail As sd28 On s.D28 = sd28.Id
	Left Join Payroll.ScheduleDetail As sd29 On s.D29 = sd29.Id
	Left Join Payroll.ScheduleDetail As sd30 On s.D30 = sd30.Id
	Left Join Payroll.ScheduleDetail As sd31 On s.D31 = sd31.Id
	Left Join Payroll.FunctionalUnit As fu On s.FunctionalUnitId = fu.Id
	left join Payroll.BranchOffice bo ON fu.BranchOfficeId = bo.Id
	Left Join Payroll.Employee As e On s.EmployeeId = e.Id
	Left Join Common.ThirdParty As tp On e.ThirdPartyId = tp.Id
	Left Join CTEHoursDetailByEmployee As cte On s.EmployeeId = cte.EmployeeId
	WHERE Period = @Period
end
else begin
	;with CTEHoursDetailByEmployee As
	(
			select 
				sd.EmployeeId
				,SUM(Case When sdh.Event = 0 Then convert(numeric(5,0), sdh.TotalNumberHours) Else 0 End) NormalHours
				,SUM(Case When sdh.Event = 1 And sdh.Approved = 1 Then convert(numeric(5,0), sdh.TotalNumberHours) Else 0 End) EventHoursApproved 
			from Payroll.ScheduleDetail sd
			left join Payroll.ScheduleDetailHour sdh on sd.id = sdh.ScheduleDetailId
			left join Payroll.FunctionalUnit fu on sd.FunctionalUnitId = fu.Id
			left join Payroll.BranchOffice bo ON fu.BranchOfficeId = bo.Id
			WHERE sd.DateDetail BETWEEN @Year+'-'+@Month+'-01' And @Year+'-'+@Month+'-31'
				AND fu.Code BETWEEN @FunctionalUnitCodeInitial AND @FunctionalUnitCodeEnding
				OR bo.Code BETWEEN @BranchOfficeCodeInitial AND @BranchOfficeCodeEnding
				or sd.EmployeeId = @EmployeeId
			group by sd.EmployeeId
	)
Select 
	s.EmployeeId 
	,tp.Name EmployeeName
	,s.Period 
	,sd1.Letter D01
	,sd2.Letter D02
	,sd3.Letter D03
	,sd4.Letter D04
	,sd5.Letter D05
	,sd6.Letter D06
	,sd7.Letter D07
	,sd8.Letter D08
	,sd9.Letter D09
	,sd10.Letter D10
	,sd11.Letter D11
	,sd12.Letter D12
	,sd13.Letter D13
	,sd14.Letter D14
	,sd15.Letter D15
	,sd16.Letter D16
	,sd17.Letter D17
	,sd18.Letter D18
	,sd19.Letter D19
	,sd20.Letter D20
	,sd21.Letter D21
	,sd22.Letter D22
	,sd23.Letter D23
	,sd24.Letter D24
	,sd25.Letter D25
	,sd26.Letter D26
	,sd27.Letter D27
	,sd28.Letter D28
	,sd29.Letter D29
	,sd30.Letter D30
	,sd31.Letter D31
	,fu.Id FunctionalUnitId
	,fu.Code FunctionalUnitCode
	,fu.Name FunctionalUnitName
	,cte.NormalHours
	,cte.EventHoursApproved
From Payroll.Schedule As s
Left Join Payroll.ScheduleDetail As sd1 On s.D01 = sd1.Id
Left Join Payroll.ScheduleDetail As sd2 On s.D02 = sd2.Id
Left Join Payroll.ScheduleDetail As sd3 On s.D03 = sd3.Id
Left Join Payroll.ScheduleDetail As sd4 On s.D04 = sd4.Id
Left Join Payroll.ScheduleDetail As sd5 On s.D05 = sd5.Id
Left Join Payroll.ScheduleDetail As sd6 On s.D06 = sd6.Id
Left Join Payroll.ScheduleDetail As sd7 On s.D07 = sd7.Id
Left Join Payroll.ScheduleDetail As sd8 On s.D08 = sd8.Id
Left Join Payroll.ScheduleDetail As sd9 On s.D08 = sd9.Id
Left Join Payroll.ScheduleDetail As sd10 On s.D10 = sd10.Id
Left Join Payroll.ScheduleDetail As sd11 On s.D11 = sd11.Id
Left Join Payroll.ScheduleDetail As sd12 On s.D12 = sd12.Id
Left Join Payroll.ScheduleDetail As sd13 On s.D13 = sd13.Id
Left Join Payroll.ScheduleDetail As sd14 On s.D14 = sd14.Id
Left Join Payroll.ScheduleDetail As sd15 On s.D15 = sd15.Id
Left Join Payroll.ScheduleDetail As sd16 On s.D16 = sd16.Id
Left Join Payroll.ScheduleDetail As sd17 On s.D17 = sd17.Id
Left Join Payroll.ScheduleDetail As sd18 On s.D18 = sd18.Id
Left Join Payroll.ScheduleDetail As sd19 On s.D19 = sd19.Id
Left Join Payroll.ScheduleDetail As sd20 On s.D20 = sd20.Id
Left Join Payroll.ScheduleDetail As sd21 On s.D21 = sd21.Id
Left Join Payroll.ScheduleDetail As sd22 On s.D22 = sd22.Id
Left Join Payroll.ScheduleDetail As sd23 On s.D23 = sd23.Id
Left Join Payroll.ScheduleDetail As sd24 On s.D24 = sd24.Id
Left Join Payroll.ScheduleDetail As sd25 On s.D25 = sd25.Id
Left Join Payroll.ScheduleDetail As sd26 On s.D26 = sd26.Id
Left Join Payroll.ScheduleDetail As sd27 On s.D27 = sd27.Id
Left Join Payroll.ScheduleDetail As sd28 On s.D28 = sd28.Id
Left Join Payroll.ScheduleDetail As sd29 On s.D29 = sd29.Id
Left Join Payroll.ScheduleDetail As sd30 On s.D30 = sd30.Id
Left Join Payroll.ScheduleDetail As sd31 On s.D31 = sd31.Id
Left Join Payroll.FunctionalUnit As fu On s.FunctionalUnitId = fu.Id
left join Payroll.BranchOffice bo ON fu.BranchOfficeId = bo.Id
Left Join Payroll.Employee As e On s.EmployeeId = e.Id
Left Join Common.ThirdParty As tp On e.ThirdPartyId = tp.Id
Left Join CTEHoursDetailByEmployee As cte On s.EmployeeId = cte.EmployeeId
WHERE Period = @Period
	AND fu.Code BETWEEN @FunctionalUnitCodeInitial AND @FunctionalUnitCodeEnding
	OR bo.Code BETWEEN @BranchOfficeCodeInitial AND @BranchOfficeCodeEnding
	or e.Id = @EmployeeId
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de cuadro de turnos mensual por empleado para el módulo de nómina. Genera una grilla donde cada columna representa un día del mes (D01 a D31) y muestra la letra o código del turno asignado a cada empleado en ese día, tomando los datos de la programación horaria (Schedule y ScheduleDetail). Además, consolida las horas normales trabajadas y las horas de eventos especiales aprobados (recargos, festivos, etc.) por empleado en el período seleccionado, filtrando opcionalmente por rango de unidad funcional, sede o sucursal, y número de empleado. Sirve para que el área de nómina y talento humano visualice y audite la programación de turnos de toda la plantilla o de grupos específicos en un mes determinado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_ReportShiftBox';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_ReportShiftBox';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte mensual de planilla de turnos (shift box) por empleado, mostrando la letra de turno asignada para cada uno de los 31 días del período junto con el total de horas normales y horas de evento aprobadas.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ReportShiftBox';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@xmlCriterias debe contener un nodo /Data con los elementos Period, FunctionalUnitCodeInitial, FunctionalUnitCodeEnding, BranchOfficeCodeInitial, BranchOfficeCodeEnding, EmployeeId y allRecords.; Period debe tener formato dd/MM/yyyy (se extrae mes en posición 4-5 y año en 7-10).; Si @allRecords <> 1, deben proveerse rangos de código de unidad funcional y/o sucursal y/o un EmployeeId para filtrar.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ReportShiftBox';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El período se reconstruye como MM/YYYY tomando posiciones 4-5 (mes) y 7-10 (año) del parámetro Period recibido por XML.; El rango de fechas para el cálculo de horas se arma como ''@Year-@Month-01'' a ''@Year-@Month-31'' (rango fijo a día 31, sin ajuste por mes).; NormalHours suma TotalNumberHours solo cuando Event = 0; EventHoursApproved suma solo cuando Event = 1 AND Approved = 1.; El reporte expone una columna por cada uno de los 31 días del mes (D01..D31) con la letra del turno (ScheduleDetail.Letter).; El JOIN del día 9 (sd9) se hace incorrectamente sobre s.D08 en lugar de s.D09 en ambas ramas (bug latente que duplica el día 8 en la columna D09).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ReportShiftBox';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Turno/Programación laboral mensual; Unidad funcional; Sucursal; Empleado; Horas normales; Horas de evento aprobadas; Período (mes/año)', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ReportShiftBox';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.Schedule: Cuando @allRecords = 1, retorna todas las filas de Schedule cuyo Period = MM/YYYY derivado del parámetro, con D01..D31 (Letter), unidad funcional, nombre del empleado y totales NormalHours/EventHoursApproved.; [RETURN_RESULT] Payroll.Schedule: Cuando @allRecords <> 1, retorna las filas de Schedule donde Period = MM/YYYY AND fu.Code entre rango de unidad funcional, OR bo.Code entre rango de sucursal, OR e.Id = @EmployeeId (precedencia natural de OR sobre AND).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ReportShiftBox';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @allRecords = 1 → Devuelve la programación de turnos de TODOS los empleados del período sin filtrar por unidad funcional, sucursal ni empleado. else Aplica filtros por rango de código de unidad funcional, rango de código de sucursal y/o un EmployeeId específico (combinados con OR).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ReportShiftBox';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.Schedule; Payroll.ScheduleDetail; Payroll.ScheduleDetailHour; Payroll.FunctionalUnit; Payroll.BranchOffice; Payroll.Employee; Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ReportShiftBox';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ReportShiftBox';
-- GO
