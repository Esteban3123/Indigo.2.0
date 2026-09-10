-- =============================================
-- Author:		Johan Carranza
-- Create date: 2019-01-30
-- Description:	Valida la información del archivo Excel para cargue masivo de entidades externas.
-- =============================================
CREATE PROCEDURE [Payroll].[SP_ValidateMassiveNovelties] 
	@XMLObj XML

AS
BEGIN

Declare @Action varchar(15)
select @Action = ISNULL(TRY_CONVERT(Varchar(15),t.x.value('Action[1]', 'VARCHAR(100)')),'Validar')
FROM @XMLObj.nodes('/Data') t(x)

Declare @Usuario Int
select @Usuario = ISNULL(TRY_CONVERT(INT,t.x.value('User[1]', 'VARCHAR(100)')),0)
FROM @XMLObj.nodes('/Data') t(x)

If @Action <> 'Confirmar'
BEGIN

;WITH cteXML AS(
		SELECT 
			ROW_NUMBER() OVER(ORDER BY (SELECT 0)) AS Linea,
			t.x.value('Nit[1]', 'VARCHAR(100)') AS Nit,
			t.x.value('TypeNovelty[1]', 'VARCHAR(100)') AS TypeNovelty,
			t.x.value('Extension[1]', 'VARCHAR(100)') AS Extension,
			t.x.value('InabilityClass[1]', 'VARCHAR(100)') AS InabilityClass,
			t.x.value('RiskType[1]', 'VARCHAR(100)') AS RiskType,
			t.x.value('RealDate[1]', 'VARCHAR(100)') AS RealDate,
			t.x.value('Days[1]', 'VARCHAR(100)') AS Days,
			CASE WHEN (t.x.value('Reason[1]', 'VARCHAR(100)')) = '' THEN '0' ELSE t.x.value('Reason[1]', 'VARCHAR(100)') END AS Reason,
			t.x.value('TipoLiquidar[1]', 'VARCHAR(100)') AS TipoLiquidar,
			t.x.value('PostularValor[1]', 'VARCHAR(100)') AS PostularValor
		FROM @XMLObj.nodes('/Data/Row') t(x)
	), cteType AS(
		SELECT
			Linea,
			'Caracteres inválidos/formato incorrecto/Campo Faltante' AS Mensaje,
			Columna, 
			Valor,
			CASE
				WHEN Columna = 'Nit' AND TRY_CONVERT(INT, Valor) IS NOT NULL AND Valor<>'' THEN 1
				WHEN Columna = 'TypeNovelty' AND TRY_CONVERT(TINYINT, Valor) IS NOT NULL THEN 1
				WHEN Columna = 'Extension' AND TRY_CONVERT(TINYINT, Valor) IS NOT NULL THEN 1
				WHEN Columna = 'InabilityClass' AND TRY_CONVERT(TINYINT, Valor, 103) IS NOT NULL THEN 1
				WHEN Columna = 'RiskType' AND TRY_CONVERT(TINYINT, Valor) IS NOT NULL THEN 1
				WHEN Columna = 'RealDate' AND TRY_CONVERT(DATE, Valor, 103) IS NOT NULL OR Valor <> '' THEN 1
				WHEN Columna = 'Days' AND TRY_CONVERT(TINYINT, Valor) IS NOT NULL THEN 1
				WHEN Columna = 'TipoLiquidar' AND TRY_CONVERT(TINYINT, Valor) IS NOT NULL THEN 1
				WHEN Columna = 'PostularValor' AND TRY_CONVERT(TINYINT, Valor) IS NOT NULL THEN 1
				ELSE 0
			END AS TC
		FROM cteXML
		UNPIVOT(
			Valor FOR Columna IN(
				Nit,
				TypeNovelty,
				Extension,
				InabilityClass,
				RiskType,
				RealDate,
				Days,
				Reason,
				TipoLiquidar,
				PostularValor
			)
		) u
	), cte1Employee AS(
		SELECT
			cteXML.Linea,
			'El empleado no existe' AS Mensaje,
			'Número Identificación' AS Columna,
			cteXML.Nit AS Valor
		FROM cteXML
		LEFT JOIN Common.ThirdParty tPrty WITH(NOLOCK) ON tPrty.Nit = cteXML.Nit
		LEFT JOIN Payroll.Employee emply WITH(NOLOCK) ON emply.ThirdPartyId = tPrty.Id
		WHERE emply.ThirdpartyId IS NULL
	), cte2Contract AS(
		SELECT
			cteXML.Linea,
			CASE WHEN cntrc.EmployeeId IS NULL AND DATEADD(day,CAST(cteXML.Days AS TINYINT) , CteXMl.RealDate) > cntrc.ContractEndingDate AND cteXML.RealDate < cntrc.ContractInitialDate
			THEN 'El empleado no tiene un contrato activo/La fecha inicial de la novedad (' + CAST(cteXML.RealDate AS varchar(30)) + ') no puede ser mayor a la fecha final de contrato ('+
			CAST(cntrc.ContractEndingDate AS varchar(30)) + ') / Fecha inicio novedad ('+ CAST(cteXML.RealDate AS varchar(30)) +') debe ser mayor o igual a la fecha de inicio del contrato (' + CAST(cntrc.ContractInitialDate AS varchar(30)) + ').'
			WHEN cntrc.EmployeeId IS NULL THEN 'El empleado no tiene un contrato activo'
			WHEN DATEADD(day,CAST(cteXML.Days AS TINYINT),CteXMl.RealDate) > cntrc.ContractEndingDate THEN 'la fecha inicial de la novedad (' + CAST(cteXML.RealDate AS varchar(30)) + ') no puede ser mayor a la fecha final de contrato ('+
			CAST(cntrc.ContractEndingDate AS varchar(30)) + ')'
			WHEN cteXML.RealDate < cntrc.ContractInitialDate THEN 'Fecha inicio novedad ('+ CAST(cteXML.RealDate AS varchar(30)) +') debe ser mayor o igual a la fecha de inicio del contrato (' + CAST(cntrc.ContractInitialDate AS varchar(30)) + ').'
			ELSE
			'El empleado no tiene un contrato activo'
			END AS Mensaje,
			'Número Identificación' AS Columna,
			cteXML.Nit AS Valor
		FROM cteXML
		INNER JOIN Common.ThirdParty tPrty WITH(NOLOCK) ON tPrty.Nit = cteXML.Nit
		INNER JOIN Payroll.Employee emply WITH(NOLOCK) ON emply.ThirdPartyId = tPrty.Id
		LEFT JOIN Payroll.[Contract] cntrc WITH(NOLOCK) ON cntrc.EmployeeId = emply.Id AND cntrc.Valid = 1 AND cntrc.[Status] = 1
		WHERE cntrc.EmployeeId IS NULL OR DATEADD(day,CAST(cteXML.Days AS TINYINT),CteXMl.RealDate) > cntrc.ContractEndingDate OR cteXML.RealDate < cntrc.ContractInitialDate
	), ctePrevNovelties AS(
		SELECT ROW_NUMBER() OVER(Partition By N.EmployeeId ORDER BY N.EndDate Desc) as Novedad, N.EmployeeId, N.Reason, N.EndDate, N.[days] , N.TypeNovelty--, N.Consecutive
		FROM cteXML
		INNER JOIN Common.ThirdParty tPrty WITH(NOLOCK) ON tPrty.Nit = cteXML.Nit
		INNER JOIN Payroll.Employee emply WITH(NOLOCK) ON emply.ThirdPartyId = tPrty.Id
		INNER JOIN Payroll.Novelty N WITH(NOLOCK) ON N.EmployeeId = emply.Id AND N.Reason = cteXML.Reason AND N.TypeNovelty = cteXML.TypeNovelty
	), cteValidExtension AS(
		SELECT
			cteXML.Linea,
			CASE WHEN CPN.EndDate IS NULL THEN 'El empleado no tiene una incapacidad previa con ese mismo diagnóstico ('+ CteXML.Reason +') o es mayor a 30 dias.'
			WHEN DATEDIFF(day,cteXML.RealDate, CPN.EndDate)>30 AND cteXMl.Extension = 1
			THEN 'La novedad no es una prorroga debido a que al fecha inicial de la novedad ('+ CAST(cteXML.RealDate AS varchar(30)) +') es mayor 30 dias a la fecha final de la incapacidad previa ('+ CAST(CPN.EndDate AS varchar(30)) +').' 
			END AS Mensaje,
			'Número Identificación' AS Columna,
			cteXML.Nit AS Valor
		FROM cteXML
		INNER JOIN Common.ThirdParty tPrty WITH(NOLOCK) ON tPrty.Nit = cteXML.Nit
		INNER JOIN Payroll.Employee emply WITH(NOLOCK) ON emply.ThirdPartyId = tPrty.Id
		INNER JOIN Payroll.[Contract] cntrc WITH(NOLOCK) ON cntrc.EmployeeId = emply.Id AND cntrc.Valid = 1 AND cntrc.[Status] = 1
		LEFT OUTER JOIN ctePrevNovelties CPN ON CPN.EmployeeId=emply.Id AND CPN.Reason = cteXML.Reason AND CPN.TypeNovelty = cteXML.TypeNovelty AND Novedad=1
		WHERE (CPN.EndDate IS NULL OR DATEDIFF(day,cteXML.RealDate, CPN.EndDate)>30) AND cteXMl.Extension = 1
	), cteTotalDaysNovelty AS(
		SELECT CteXML.Linea,tPrty.Nit, emply.Id As EmployeeId , cntrc.BasicSalary ,ISNULL(Sum(CPN.[Days]),0) as PastDays , Sum(CAST(cteXML.[Days] AS TINYINT)) As [Days], Sum(CAST(cteXML.[Days] AS TINYINT))+ISNULL(Sum(CPN.[Days]),0) as TotalDays, CteXMl.PostularValor,PP.LegalSalaryMinimum, CteXML.TipoLiquidar, cteXML.TypeNovelty

		FROM cteXML
		INNER JOIN Common.ThirdParty tPrty WITH(NOLOCK) ON tPrty.Nit = cteXML.Nit
		INNER JOIN Payroll.Employee emply WITH(NOLOCK) ON emply.ThirdPartyId = tPrty.Id
		INNER JOIN Payroll.[Contract] cntrc WITH(NOLOCK) ON cntrc.EmployeeId = emply.Id AND cntrc.Valid = 1 AND cntrc.[Status] = 1
		LEFT OUTER JOIN ctePrevNovelties CPN ON CPN.EmployeeId=emply.Id AND CPN.Reason = cteXML.Reason AND CPN.TypeNovelty = cteXML.TypeNovelty
		LEFT OUTER JOIN Payroll.[Group] G ON G.Id = cntrc.GroupId
		LEFT OUTER JOIN Payroll.PayrollParameter  PP ON PP.Id = G.PayrollParameterId
		WHERE CteXMl.TypeNovelty = 1 OR CteXMl.TypeNovelty = 2 OR CteXMl.TypeNovelty = 3
		Group By CPN.EmployeeId, tPrty.Nit, CteXMl.PostularValor,emply.Id, cntrc.BasicSalary, PP.LegalSalaryMinimum, cteXML.Linea, cteXML.TipoLiquidar, cteXML.TypeNovelty
	), ctePastDays AS(
		SELECT Dp1=CASE WHEN TDN.PastDays <= 2 THEN TDN.PastDays ELSE 2 END, Dp2=CASE WHEN TDN.PastDays > 2 AND TDN.PastDays<=90 THEN TDN.PastDays ELSE CASE WHEN TDN.PastDays>90 THEN 90 ELSE 
		CASE WHEN TDN.PastDays<=2 THEN 2 ELSE 0 END END END, 
		Dp3 = CASE WHEN TDN.PastDays > 90 AND TDN.PastDays<=180 THEN TDN.PastDays ELSE CASE WHEN TDN.PastDays>180 THEN 90 ELSE CASE WHEN TDN.PastDays<=90 THEN 90 ELSE 0 END END END, DT=TDN.PastDays+TDN.Days,
		PostularValor, TDN.EmployeeId,  LegalSalaryMinimum, Linea , 
		CASE WHEN TipoLiquidar=1 THEN (CASE WHEN TDN.BasicSalary<LegalSalaryMinimum THEN LegalSalaryMinimum ELSE TDN.BasicSalary END) 
		WHEN TipoLiquidar=2 THEN (CASE WHEN ISNULL(LD.BasicSalary,TDN.BasicSalary)<LegalSalaryMinimum THEN LegalSalaryMinimum ELSE ISNULL(LD.BasicSalary,TDN.BasicSalary) END) END As BasicSalary, TDN.TypeNovelty
		FROM cteTotalDaysNovelty TDN
		INNER JOIN Payroll.[Contract] cntrc WITH(NOLOCK) ON cntrc.EmployeeId = TDN.EmployeeId AND cntrc.Valid = 1 AND cntrc.[Status] = 1
		INNER JOIN Payroll.[Group] G WITH(NOLOCK) ON G.Id = cntrc.GroupId
		LEFT OUTER JOIN Payroll.Liquidation L WITH(NOLOCK) ON MONTH(L.PayrollDateLiquidated)=MONTH(G.LastDateLiquidation) AND YEAR(L.PayrollDateLiquidated)=YEAR(G.LastDateLiquidation) AND L.EmployeeId = TDN.EmployeeId AND L.ContractId = cntrc.Id
		LEFT OUTER JOIN (Select LD.PayrollId, SUM(LD.AccruedValue)-SUM(LD.DeductedValue) as BasicSalary from Payroll.LiquidationDetail LD
					INNER JOIN Payroll.Concept C ON C.Id = LD.ConceptId where C.AffectIBC=1
					GROUP BY LD.PayrollId) LD ON LD.PayrollId = L.Id
	), cteInabilityValue AS(
		SELECT 
		CASE WHEN DT <= 2 THEN DT-Dp1 ELSE 2-Dp1 END As X1,

		CASE WHEN DT <= 2 THEN (DT-Dp1)*(CASE WHEN PostularValor=1 OR PostularValor=3 THEN (CASE WHEN (BasicSalary/30)*1<(LegalSalaryMinimum/30) THEN (LegalSalaryMinimum/30) ELSE (BasicSalary/30)*1 END) 
		WHEN PostularValor=2 THEN (CASE WHEN (BasicSalary/30)*0.6667<(LegalSalaryMinimum/30) THEN (LegalSalaryMinimum/30) ELSE (BasicSalary/30)*0.6667 END) 
		ELSE (CASE WHEN (BasicSalary/30)*1<(LegalSalaryMinimum/30) THEN (LegalSalaryMinimum/30) ELSE (BasicSalary/30)*1 END) END)
		ELSE (2-Dp1)*(CASE WHEN PostularValor=1 OR PostularValor=3 THEN (CASE WHEN (BasicSalary/30)*1<(LegalSalaryMinimum/30) THEN (LegalSalaryMinimum/30) ELSE (BasicSalary/30)*1 END) 
		WHEN PostularValor=2 THEN (CASE WHEN (BasicSalary/30)*0.6667<(LegalSalaryMinimum/30) THEN (LegalSalaryMinimum/30) ELSE (BasicSalary/30)*0.6667 END) END) END As ValueX1, --(100%,66&,100%)

		CASE WHEN DT <= 2 THEN (CASE WHEN PostularValor=1 OR PostularValor=3 THEN  ROUND((BasicSalary/30)*1,0)
		WHEN PostularValor=2 THEN ROUND((BasicSalary/30)*0.6667,0) ELSE ROUND((BasicSalary/30)*1,0) END) 
		ELSE (CASE WHEN PostularValor=1 OR PostularValor=3 THEN ROUND((BasicSalary/30)*1,0) 
		WHEN PostularValor=2 THEN ROUND((BasicSalary/30)*0.6667,0)  END) END As DailySalaryX1, --(100%,66&,100%)

		CASE WHEN DT<=2 THEN 0 ELSE CASE WHEN DT > 2 AND DT<=90 THEN DT-Dp2 ELSE 90-Dp2 END END As X2,

		CASE WHEN DT<=2 THEN 0 ELSE CASE WHEN DT > 2 AND DT<=90 THEN (DT-Dp2)*(CASE WHEN PostularValor=1 THEN (CASE WHEN (BasicSalary/30)*1<(LegalSalaryMinimum/30) THEN (LegalSalaryMinimum/30) ELSE (BasicSalary/30)*1 END)
		WHEN PostularValor=2 OR PostularValor=3 THEN (CASE WHEN (BasicSalary/30)*0.6667<(LegalSalaryMinimum/30) THEN (LegalSalaryMinimum/30) ELSE (BasicSalary/30)*0.6667 END)
		ELSE (CASE WHEN (BasicSalary/30)*1<(LegalSalaryMinimum/30) THEN (LegalSalaryMinimum/30) ELSE (BasicSalary/30)*1 END) END) 
		ELSE (90-Dp2)*(CASE WHEN PostularValor=1 THEN (CASE WHEN (BasicSalary/30)*1<(LegalSalaryMinimum/30) THEN (LegalSalaryMinimum/30) ELSE (BasicSalary/30)*1 END) WHEN PostularValor=2 OR PostularValor=3 THEN (CASE WHEN (BasicSalary/30)*0.6667<(LegalSalaryMinimum/30) THEN (LegalSalaryMinimum/30) ELSE (BasicSalary/30)*0.6667 END)
		ELSE (CASE WHEN (BasicSalary/30)*1<(LegalSalaryMinimum/30) THEN (LegalSalaryMinimum/30) ELSE (BasicSalary/30)*1 END)  END) END END As ValueX2, --(100%,66&,66%)

		CASE WHEN DT<=2 THEN 0 ELSE CASE WHEN DT > 2 AND DT<=90 THEN (CASE WHEN PostularValor=1 THEN ROUND((BasicSalary/30)*1,0) WHEN PostularValor=2 OR PostularValor=3 THEN ROUND((BasicSalary/30)*0.6667,0) 
		ELSE ROUND((BasicSalary/30)*1,0) END) 
		ELSE (CASE WHEN PostularValor=1 THEN ROUND((BasicSalary/30)*1,0) WHEN PostularValor=2 OR PostularValor=3 THEN ROUND((BasicSalary/30)*0.6667,0) ELSE ROUND((BasicSalary/30)*1,0) END) END END As DailySalaryX2, --(100%,66&,66%)

		CASE WHEN DT<=90 THEN 0 ELSE CASE WHEN DT > 90 AND DT<=180 THEN DT-Dp3 ELSE 180-Dp3 END END As X3,

		CASE WHEN DT<=90 THEN 0 ELSE CASE WHEN DT > 90 AND DT<=180 THEN (DT-Dp3)*(CASE WHEN PostularValor=1 THEN (CASE WHEN (BasicSalary/30)*1<(LegalSalaryMinimum/30) THEN (LegalSalaryMinimum/30) ELSE (BasicSalary/30)*1 END)
		WHEN PostularValor=2 OR PostularValor=3 THEN (CASE WHEN (BasicSalary/30)*0.5<(LegalSalaryMinimum/30) THEN (LegalSalaryMinimum/30) ELSE (BasicSalary/30)*0.5 END)
		ELSE (CASE WHEN (BasicSalary/30)*1<(LegalSalaryMinimum/30) THEN (LegalSalaryMinimum/30) ELSE (BasicSalary/30)*1 END) END)
		ELSE (180-Dp3)*(CASE WHEN PostularValor=1 THEN (CASE WHEN (BasicSalary/30)*1<(LegalSalaryMinimum/30) THEN (LegalSalaryMinimum/30) ELSE (BasicSalary/30)*1 END)
		WHEN PostularValor=2 OR PostularValor=3 THEN (CASE WHEN (BasicSalary/30)*0.5<(LegalSalaryMinimum/30) THEN (LegalSalaryMinimum/30) ELSE (BasicSalary/30)*0.5 END)
		ELSE (CASE WHEN (BasicSalary/30)*1<(LegalSalaryMinimum/30) THEN (LegalSalaryMinimum/30) ELSE (BasicSalary/30)*1 END) END) END END As ValueX3, --(100%,50&,50%)

		CASE WHEN DT<=90 THEN 0 ELSE CASE WHEN DT > 90 AND DT<=180 THEN (CASE WHEN PostularValor=1 THEN ROUND((BasicSalary/30)*1,0) WHEN PostularValor=2 OR PostularValor=3 THEN ROUND((BasicSalary/30)*0.5,0) 
		ELSE ROUND((BasicSalary/30)*1,0) END)
		ELSE (CASE WHEN PostularValor=1 THEN ROUND((BasicSalary/30)*1,0) WHEN PostularValor=2 OR PostularValor=3 THEN ROUND((BasicSalary/30)*0.5,0) ELSE ROUND((BasicSalary/30)*1,0) END) END END As DailySalaryX3, --(100%,50&,50%)

		EmployeeId,LegalSalaryMinimum, Linea, BasicSalary

		FROM ctePastDays PD
	), cteLessThanMinimumSalary AS(
		SELECT cteXML.Linea,
			CASE WHEN IV.DailySalaryX1<LegalSalaryMinimum/30 AND IV.DailySalaryX1 > 0 THEN 'El salario diario de los dias de contribucion del empleado ('+ CAST(CAST(IV.DailySalaryX1 AS NUMERIC(18,0)) AS VARCHAR (30)) +') es menor al salario minimo ('+ CAST(CAST(LegalSalaryMinimum/30 AS NUMERIC(18,0)) AS VARCHAR (30)) 
			+')(Se tomara el salario minimo legal).'
			WHEN IV.DailySalaryX2<LegalSalaryMinimum/30 AND IV.DailySalaryX2 > 0 THEN 'El salario diario de los dias de contribucion de la EPS (dia 2-90) ('+ CAST(CAST(IV.DailySalaryX2 AS NUMERIC(18,0)) AS VARCHAR (30)) +') es menor al salario minimo ('+ CAST(CAST(LegalSalaryMinimum/30 AS NUMERIC(18,0)) AS VARCHAR (30))
			+')(Se tomara el salario minimo legal).'
			WHEN IV.DailySalaryX3<LegalSalaryMinimum/30 AND IV.DailySalaryX3 > 0 THEN 'El salario diario de los dias de contribucion de la EPS (dia 90-180) ('+ CAST(CAST(IV.DailySalaryX3 AS NUMERIC(18,0)) AS VARCHAR (30)) +') es menor al salario minimo ('+ CAST(CAST(LegalSalaryMinimum/30 AS NUMERIC(18,0)) AS VARCHAR (30))
			+')(Se tomara el salario minimo legal).'
			END AS Mensaje,
			'Número Identificación' AS Columna,
			cteXML.Nit AS Valor
		FROM cteXML
		INNER JOIN Common.ThirdParty tPrty WITH(NOLOCK) ON tPrty.Nit = cteXML.Nit
		INNER JOIN Payroll.Employee emply WITH(NOLOCK) ON emply.ThirdPartyId = tPrty.Id
		INNER JOIN cteInabilityValue IV ON cteXML.Linea = IV.Linea
		WHERE CteXML.TypeNovelty<>2 AND ((IV.DailySalaryX1<LegalSalaryMinimum/30 AND IV.DailySalaryX1 > 0) OR (IV.DailySalaryX2<LegalSalaryMinimum/30 AND IV.DailySalaryX2 > 0) OR (IV.DailySalaryX3<LegalSalaryMinimum/30 AND IV.DailySalaryX3 > 0))
	), cteOutput AS(
		SELECT T.*, tPrty.Name as Employee, (DATEADD(day,CAST(T.Days AS TINYINT),T.RealDate)) As EndDate,
		-------------<----------------->--------- Vacaciones
		---------<---1---><-2-><------3----->---- ActionVacation
		---------<------------4------------->---- Rango de Novedad mayor al Rango de vacaciones.
		--<---5---->---------------------<--5-->- Novedad por fuera de las vacaciones
		CASE WHEN (T.RealDate<V.VacationStartDate) AND DATEADD(day,CAST(T.Days AS TINYINT),T.RealDate)<=V.VacationEndDate THEN 1
		WHEN (T.RealDate>=v.VacationStartDate) AND DATEADD(day,CAST(T.Days AS TINYINT),T.RealDate)<=V.VacationEndDate THEN 2
		WHEN (T.RealDate>=v.VacationStartDate) AND DATEADD(day,CAST(T.Days AS TINYINT),T.RealDate)>V.VacationEndDate THEN 3
		WHEN (T.RealDate<=v.VacationStartDate) AND DATEADD(day,CAST(T.Days AS TINYINT),T.RealDate)>=V.VacationEndDate THEN 4
		ELSE 5
		END as ActionVacation,
		-------------<-----Nomina---->----------- Periodo a liquidar nomina
		---<---1--->----------------------------- ActionSchedule - No se cambia el cuadro de turnos.
		---<----2---->--2-->--------------------- ActionSchedule - Solo se eliminan los dias que se encuentren despues del inicio de si guiente periodo de liquidacion.
		----------------<----3---->-------------- ActionSchedule - Se eliminan todos los registros.
		CASE WHEN (DATEADD(day,CAST(T.Days AS TINYINT),T.RealDate)) < G.NextDateLiquidation  THEN 1
		WHEN (T.RealDate < G.NextDateLiquidation) AND (DATEADD(day,CAST(T.Days AS TINYINT),T.RealDate)) >= G.NextDateLiquidation THEN 2
		WHEN (T.RealDate >= G.NextDateLiquidation)  THEN 3
		ELSE 4
		END as ActionSchedule,
		IV.ValueX1 AS PaidEmployervalue, (IV.ValueX2+IV.ValueX3) As EPSRecognizeValue, (IV.ValueX1+IV.ValueX2+IV.ValueX3) As [Value], IV.X1 AS EmployerDays, (IV.X2+IV.X3) As EPSDays, IV.BasicSalary
		FROM cteXML T
		INNER JOIN Common.ThirdParty tPrty WITH(NOLOCK) ON tPrty.Nit = T.Nit
		INNER JOIN Payroll.Employee emply WITH(NOLOCK) ON emply.ThirdPartyId = tPrty.Id
		INNER JOIN Payroll.[Contract] cntrc WITH(NOLOCK) ON cntrc.EmployeeId = emply.Id AND cntrc.Valid = 1 AND cntrc.[Status] = 1
		INNER JOIN Payroll.[Group] G WITH(NOLOCK) ON G.Id = cntrc.GroupId
		LEFT OUTER JOIN Payroll.VacationPeriod VP WITH(NOLOCK) ON VP.ContractId = cntrc.Id AND VP.TakenDays>0 AND VP.EmployeeId = emply.Id AND VP.PendingDays<>0
		LEFT OUTER JOIN Payroll.Vacation V WITH(NOLOCK) ON V.VacationPeriodId = VP.Id
		LEFT OUTER JOIN cteInabilityValue IV ON IV.Linea = T.Linea
	), cte AS(
		SELECT Linea, Employee=NULL, TypeNovelty= NULL, InabilityClass=NULL, RealDate=NULL, EndDate=NULL, [Days]=NULL, Reason=NULL, Nit=NULL, Extension=NULL, RiskType=NULL, TipoLiquidar=NULL, PostularValor=NULL, Tipo='Error',
		PaidEmployervalue=NULL, EPSRecognizeValue= NULL, [Value]=NULL, ActionVacation=NULL, ActionSchedule=NULL, Mensaje, Columna, Valor, EmployerDays=NULL, EPSDays=NULL, BasicSalary=NULL
		FROM cteType WHERE TC = 0
		UNION ALL
		SELECT Linea, Employee=NULL, TypeNovelty= NULL, InabilityClass=NULL, RealDate=NULL, EndDate=NULL, [Days]=NULL, Reason=NULL, Nit=NULL, Extension=NULL, RiskType=NULL, TipoLiquidar=NULL, PostularValor=NULL, Tipo='Error',
		PaidEmployervalue=NULL, EPSRecognizeValue= NULL, [Value]=NULL, ActionVacation=NULL, ActionSchedule=NULL, Mensaje, Columna, Valor ,EmployerDays=NULL, EPSDays=NULL, BasicSalary=NULL
		FROM cte1Employee
		UNION ALL
		SELECT Linea, Employee=NULL, TypeNovelty= NULL, InabilityClass=NULL, RealDate=NULL, EndDate=NULL, [Days]=NULL, Reason=NULL, Nit=NULL, Extension=NULL, RiskType=NULL, TipoLiquidar=NULL, PostularValor=NULL, Tipo='Error',
		PaidEmployervalue=NULL, EPSRecognizeValue= NULL, [Value]=NULL, ActionVacation=NULL, ActionSchedule=NULL, Mensaje, Columna, Valor ,EmployerDays=NULL, EPSDays=NULL, BasicSalary=NULL
		FROM cte2Contract
		UNION ALL
		SELECT Linea, Employee=NULL, TypeNovelty= NULL, InabilityClass=NULL, RealDate=NULL, EndDate=NULL, [Days]=NULL, Reason=NULL, Nit=NULL, Extension=NULL, RiskType=NULL, TipoLiquidar=NULL, PostularValor=NULL, Tipo='Error',
		PaidEmployervalue=NULL, EPSRecognizeValue= NULL, [Value]=NULL, ActionVacation=NULL, ActionSchedule=NULL, Mensaje, Columna, Valor ,EmployerDays=NULL, EPSDays=NULL, BasicSalary=NULL
		FROM cteValidExtension
		UNION ALL
		SELECT Linea, Employee=NULL, TypeNovelty= NULL, InabilityClass=NULL, RealDate=NULL, EndDate=NULL, [Days]=NULL, Reason=NULL, Nit=NULL, Extension=NULL, RiskType=NULL, TipoLiquidar=NULL, PostularValor=NULL, Tipo='Info',
		PaidEmployervalue=NULL, EPSRecognizeValue= NULL, [Value]=NULL, ActionVacation=NULL, ActionSchedule=NULL, Mensaje, Columna, Valor ,EmployerDays=NULL, EPSDays=NULL, BasicSalary=NULL
		FROM cteLessThanMinimumSalary
		UNION ALL
		SELECT Linea,Employee, TypeNovelty, InabilityClass, RealDate, CAST(EndDate AS VARCHAR(30)), [Days], Reason, CAST(Nit AS BIGINT), Extension, RiskType,  TipoLiquidar, PostularValor, Tipo='Datos', PaidEmployervalue, EPSRecognizeValue, [Value], ActionVacation
		,ActionSchedule, Mensaje=NULL, Columna= NULL, Valor=NULL, EmployerDays, EPSDays, BasicSalary FROM cteOutput
	) 

	SELECT  Linea,Employee, TypeNovelty, InabilityClass, RealDate, EndDate, [Days], Reason, Nit, Extension, RiskType,  TipoLiquidar, PostularValor, Tipo, PaidEmployervalue, EPSRecognizeValue, [Value], ActionVacation ,ActionSchedule, Mensaje, Columna, Valor, EmployerDays, EPSDays, ISNULL(CAST(BasicSalary AS NUMERIC(18,0)),0.0) AS BasicSalary  --Faltan los 3 valores calculados, Action Scheduled y ActionVacation 
	FROM cte
	ORDER BY Linea
END
ELSE
BEGIN

DECLARE @dataFile TABLE(Linea BIGINT,  Nit INT, TypeNovelty TINYINT, Extension TINYINT, InabilityClass TINYINT, RiskType TINYINT, RealDate DATE, EndDate DATE, Days TINYINT, Reason VARCHAR(50), TipoLiquidar TINYINT, PostularValor TINYINT, 
PaidEmployervalue NUMERIC(18,0), EPSRecognizeValue NUMERIC(18,0), Value NUMERIC(18,0), ActionVacation TINYINT, ActionSchedule TINYINT, EmployerDays TINYINT, EPSDays TINYINT, Consecutive INT, BasicSalary NUMERIC(18,0), 
IncorporationDate DATE, DaysOffset INT, DaysOffsetSchedule INT, InitialDateSchedule DATE, EndDateSchedule DATE)

DECLARE @dataFileWhile TABLE(Linea BIGINT,  GroupId INT, EmployeeId INT, ContractId INT, CompanyId INT, BranchOfficeId INT, FuncionalUnitId INT, CenterCostId INT, ScheduleFuncionalUnitId INT, PayrollLiquidationNumber INT,
Letter VARCHAR(10), DateDetail DATE, TotalNumberHours INT, ScheduleTemplateId INT, Status INT, State INT, DaysOffsetSchedule INT)

DECLARE @InsertedScheduleDetail  TABLE(Id INT,Linea BIGINT,  GroupId INT, EmployeeId INT, ContractId INT, CompanyId INT, BranchOfficeId INT, FuncionalUnitId INT, CenterCostId INT, ScheduleFuncionalUnitId INT, PayrollLiquidationNumber INT,
Letter VARCHAR(10), DateDetail DATE, TotalNumberHours INT, ScheduleTemplateId INT, Status INT, State INT, DaysOffsetSchedule INT, [YEAR] INT, [MONTH] INT, [DAY] INT)

DECLARE @Table4CrossJoin TABLE(Linea BIGINT,DaysOffsetSchedule INT, EmployeeId INT, DateDetail DATE)

DECLARE @MessageTable TABLE(Linea BIGINT, Mensaje VARCHAR(500),Columna VARCHAR(50), Nit VARCHAR(50), RealDate DATE, Days TINYINT, Extension TINYINT)

BEGIN TRAN [tran1]
BEGIN TRY

;WITH cteXML AS(
		SELECT 
			t.x.value('Linea[1]', 'VARCHAR(100)') AS Linea,
			t.x.value('Nit[1]', 'VARCHAR(100)') AS Nit,
			t.x.value('TypeNovelty[1]', 'VARCHAR(100)') AS TypeNovelty,
			t.x.value('Extension[1]', 'VARCHAR(100)') AS Extension,
			t.x.value('InabilityClass[1]', 'VARCHAR(100)') AS InabilityClass,
			t.x.value('RiskType[1]', 'VARCHAR(100)') AS RiskType,
			t.x.value('RealDate[1]', 'VARCHAR(100)') AS RealDate,
			t.x.value('EndDate[1]', 'VARCHAR(100)') AS EndDate,
			t.x.value('Days[1]', 'VARCHAR(100)') AS Days,
			CASE WHEN (t.x.value('Reason[1]', 'VARCHAR(100)')) = '' THEN '0' ELSE t.x.value('Reason[1]', 'VARCHAR(100)') END AS Reason,
			t.x.value('TipoLiquidar[1]', 'VARCHAR(100)') AS TipoLiquidar,
			t.x.value('PostularValor[1]', 'VARCHAR(100)') AS PostularValor,
			t.x.value('PaidEmployervalue[1]', 'VARCHAR(100)') AS PaidEmployervalue,
			t.x.value('EPSRecognizeValue[1]', 'VARCHAR(100)') AS EPSRecognizeValue,
			t.x.value('Value[1]', 'VARCHAR(100)') AS [Value],
			t.x.value('ActionVacation[1]', 'VARCHAR(100)') AS ActionVacation,
			t.x.value('ActionSchedule[1]', 'VARCHAR(100)') AS ActionSchedule,
			t.x.value('EmployerDays[1]', 'VARCHAR(100)') AS EmployerDays,
			t.x.value('EPSDays[1]', 'VARCHAR(100)') AS EPSDays,
			t.x.value('BasicSalary[1]', 'VARCHAR(100)') AS BasicSalary
		FROM @XMLObj.nodes('/Data/Row') t(x)
		), cteXML2 AS(
			SELECT
			CASE WHEN TRY_CONVERT(BIGINT, cteXML.Linea) IS NULL THEN 0 ELSE Linea END AS Linea,
			CASE WHEN TRY_CONVERT(INT, cteXML.Nit) IS NULL THEN 0 ELSE Nit END AS Nit,
			CASE WHEN TRY_CONVERT(TINYINT, cteXML.TypeNovelty) IS NULL THEN 0 ELSE cteXML.TypeNovelty END as TypeNovelty,
			CASE WHEN TRY_CONVERT(TINYINT, cteXMl.Extension) IS NULL THEN 0 ELSE Extension END as Extension,
			CASE WHEN TRY_CONVERT(TINYINT, cteXMl.InabilityClass) IS NULL THEN 0 ELSE InabilityClass END as InabilityClass,
			CASE WHEN TRY_CONVERT(TINYINT, RiskType) IS NULL THEN 0 ELSE RiskType END as RiskType,
			CASE WHEN TRY_CONVERT(DATE, RealDate, 103) IS NULL THEN [Common].[GETDATE]() ELSE RealDate END as RealDate,
			CASE WHEN TRY_CONVERT(DATE, EndDate, 103) IS NULL THEN [Common].[GETDATE]() ELSE EndDate END as EndDate,
			CASE WHEN TRY_CONVERT(TINYINT, Days) IS NULL THEN 0 ELSE Days END as Days,
			Reason,
			CASE WHEN TRY_CONVERT(TINYINT, TipoLiquidar) IS NULL THEN 0 ELSE TipoLiquidar END as TipoLiquidar,
			CASE WHEN TRY_CONVERT(TINYINT, PostularValor) IS NULL THEN 0 ELSE PostularValor END as PostularValor,
			CASE WHEN TRY_CONVERT(NUMERIC(18,0), REPLACE(PaidEmployervalue,',','.')) IS NULL THEN 0.0 ELSE CAST(REPLACE(PaidEmployervalue,',','.') AS NUMERIC(18,0)) END as PaidEmployervalue,
			CASE WHEN TRY_CONVERT(NUMERIC(18,0), REPLACE(EPSRecognizeValue,',','.')) IS NULL THEN 0.0 ELSE CAST(REPLACE(EPSRecognizeValue,',','.') AS NUMERIC(18,0)) END as EPSRecognizeValue,
			CASE WHEN TRY_CONVERT(NUMERIC(18,0), REPLACE(Value,',','.')) IS NULL THEN 0.0 ELSE CAST(REPLACE(Value,',','.') AS NUMERIC(18,0)) END as Value,
			CASE WHEN TRY_CONVERT(TINYINT, ActionVacation) IS NULL THEN 0 ELSE ActionVacation END as ActionVacation,
			CASE WHEN TRY_CONVERT(TINYINT, ActionSchedule) IS NULL THEN 0 ELSE ActionSchedule END as ActionSchedule,
			CASE WHEN TRY_CONVERT(TINYINT, EmployerDays) IS NULL THEN 0 ELSE EmployerDays END as EmployerDays,
			CASE WHEN TRY_CONVERT(TINYINT, EPSDays) IS NULL THEN 0 ELSE EPSDays END as EPSDays,
			CASE WHEN TRY_CONVERT(NUMERIC(18,0),  BasicSalary) IS NULL THEN 0.0 ELSE CAST(BasicSalary AS NUMERIC(18,0)) END as BasicSalary
			FROM cteXML

			)

			INSERT INTO @dataFile(Linea, Nit, TypeNovelty , Extension , InabilityClass , RiskType , RealDate , EndDate, Days, Reason, TipoLiquidar, PostularValor, PaidEmployervalue, EPSRecognizeValue, Value, ActionVacation, ActionSchedule, EmployerDays, EPSDays, BasicSalary)
			SELECT * from cteXML2

		;WITH cteInsertTable AS(
		--Tipo de Novedad 1- Incapacidad 2- Sancion 3 - Licencia
		--Insertar la novedad para incapacidades.
		SELECT /*T.Linea,*/Consecutive= N.Consecutive, 
		GroupId=G.Id, EmployeeId=emply.Id, T.TypeNovelty, T.Extension, IBC=CASE WHEN T.TipoLiquidar=2 THEN T.BasicSalary ELSE 0.0 END, cntrc.BasicSalary, T.RealDate, T.Days ,T.EndDate, VacationalInitialDateNovelty=NULL, VacationalEndDateNovelty=NULL,LiquidatedIBCorSalary=T.TipoLiquidar,
		T.Reason, LiquidationBase= T.BasicSalary, T.EPSDays, T.EmployerDays, LicenseClass =NULL, T.InabilityClass, T.RiskType, NoveltyLiquidate=1, CaculationType = NULL, T.EPSRecognizeValue,
		PaidPayrollValue=T.Value, T.PaidEmployervalue, T.Value, AutorizationNumber=NULL, ResolutionNumber=NULL, ResolutionDate =NULL, Status=0, CreationUser = @Usuario, CreationDate=CAST([Common].[GETDATE]() AS date), ModificationUser = @Usuario, ModificationDate = CAST([Common].[GETDATE]() AS date)
		FROM @dataFile T
		INNER JOIN Common.ThirdParty tPrty WITH(NOLOCK) ON tPrty.Nit = T.Nit
		INNER JOIN Payroll.Employee emply WITH(NOLOCK) ON emply.ThirdPartyId = tPrty.Id
		INNER JOIN Payroll.[Contract] cntrc WITH(NOLOCK) ON cntrc.EmployeeId = emply.Id AND cntrc.Valid = 1 AND cntrc.[Status] = 1
		INNER JOIN Payroll.[Group] G WITH(NOLOCK) ON G.Id = cntrc.GroupId
		INNER JOIN Common.Consecutive CON WITH(NOLOCK) ON CON.Code=5 OR CON.[Description]='INCAPACIDADES'
		LEFT OUTER JOIN Payroll.Novelty N WITH(NOLOCK) ON N.EmployeeId=emply.Id AND N.Reason = T.Reason AND N.TypeNovelty = T.TypeNovelty AND DATEDIFF(day,N.EndDate,T.RealDate)<30
		WHERE T.TypeNovelty=1 AND T.Extension=1 --Incapacidades Con extension

		UNION ALL

		SELECT /*T.Linea,*/(CON.NumberConsecutive + ROW_NUMBER() OVER(ORDER BY T.Linea)) As Consecutive , 
		GroupId=G.Id, EmployeeId=emply.Id, T.TypeNovelty, T.Extension, IBC=CASE WHEN T.TipoLiquidar=2 THEN T.BasicSalary ELSE 0.0 END, cntrc.BasicSalary, T.RealDate, T.Days, T.EndDate, VacationalInitialDateNovelty=NULL, VacationalEndDateNovelty=NULL,LiquidatedIBCorSalary=T.TipoLiquidar,
		T.Reason, LiquidationBase= T.BasicSalary, T.EPSDays, T.EmployerDays, LicenseClass =NULL, T.InabilityClass, T.RiskType, NoveltyLiquidate=1, CaculationType = NULL, T.EPSRecognizeValue,
		PaidPayrollValue=T.Value, T.PaidEmployervalue, T.Value, AutorizationNumber=NULL, ResolutionNumber=NULL, ResolutionDate =NULL, Status=0, CreationUser = @Usuario, CreationDate=CAST([Common].[GETDATE]() AS date), ModificationUser = @Usuario, ModificationDate = CAST([Common].[GETDATE]() AS date)
		FROM @dataFile T
		INNER JOIN Common.ThirdParty tPrty WITH(NOLOCK) ON tPrty.Nit = T.Nit
		INNER JOIN Payroll.Employee emply WITH(NOLOCK) ON emply.ThirdPartyId = tPrty.Id
		INNER JOIN Payroll.[Contract] cntrc WITH(NOLOCK) ON cntrc.EmployeeId = emply.Id AND cntrc.Valid = 1 AND cntrc.[Status] = 1
		INNER JOIN Payroll.[Group] G WITH(NOLOCK) ON G.Id = cntrc.GroupId
		INNER JOIN Common.Consecutive CON WITH(NOLOCK) ON CON.Code=5 OR CON.[Description]='INCAPACIDADES'
		WHERE T.TypeNovelty=1 AND T.Extension=0 --Incapacidades

		UNION ALL

		SELECT /*T.Linea,*/Consecutive=CON.NumberConsecutive + (SELECT Count(1) FROM @dataFile WHERE TypeNovelty=1) + ROW_NUMBER() OVER(ORDER BY T.Linea) , 
		GroupId=G.Id, EmployeeId = emply.Id, T.TypeNovelty, T.Extension, IBC=CASE WHEN T.TipoLiquidar=2 THEN T.BasicSalary ELSE 0.0 END, cntrc.BasicSalary, T.RealDate, T.Days, T.EndDate, VacationalInitialDateNovelty=NULL, VacationalEndDateNovelty=NULL,LiquidatedIBCorSalary=T.TipoLiquidar,
		T.Reason, LiquidationBase= T.BasicSalary, 
		CASE WHEN T.TypeNovelty=3 THEN (CASE WHEN T.InabilityClass = 2 THEN T.[Days] ELSE 0 END) ELSE 0 END AS EPSDays, 
		CASE WHEN T.TypeNovelty=3 THEN (CASE WHEN T.InabilityClass = 2 OR T.InabilityClass = 8 THEN 0 ELSE T.[Days] END) ELSE 0 END AS EmployerDays, 
		LicenseClass = CASE WHEN T.TypeNovelty=3 THEN T.InabilityClass END,
		T.InabilityClass, T.RiskType, NoveltyLiquidate=1, CaculationType = NULL, 
		EPSRecognizeValue = CASE WHEN T.TypeNovelty=3 THEN (CASE WHEN T.InabilityClass = 2 THEN T.[Days]*T.BasicSalary/30 ELSE 0 END) ELSE 0 END,
		PaidPayrollValue = CASE WHEN T.TypeNovelty=3 THEN (CASE WHEN T.InabilityClass = 8 THEN 0 ELSE T.[Days]*T.BasicSalary/30 END) ELSE 0 END,
		PaidEmployervalue = CASE WHEN T.TypeNovelty=3 THEN (CASE WHEN T.InabilityClass = 2 OR T.InabilityClass = 8 THEN 0 ELSE T.[Days]*T.BasicSalary END) ELSE 0 END, 
		[Value] = CASE WHEN T.TypeNovelty=3 THEN (CASE WHEN T.InabilityClass = 8 THEN 0 ELSE T.[Days]*T.BasicSalary END) ELSE 0 END, 
		AutorizationNumber=NULL, ResolutionNumber=NULL, ResolutionDate =NULL, Status=0, CreationUser = @Usuario, CreationDate=CAST([Common].[GETDATE]() AS date), ModificationUser = @Usuario, ModificationDate = CAST([Common].[GETDATE]() AS date)
		FROM @dataFile T
		INNER JOIN Common.ThirdParty tPrty WITH(NOLOCK) ON tPrty.Nit = T.Nit
		INNER JOIN Payroll.Employee emply WITH(NOLOCK) ON emply.ThirdPartyId = tPrty.Id
		INNER JOIN Payroll.[Contract] cntrc WITH(NOLOCK) ON cntrc.EmployeeId = emply.Id AND cntrc.Valid = 1 AND cntrc.[Status] = 1
		INNER JOIN Payroll.[Group] G WITH(NOLOCK) ON G.Id = cntrc.GroupId
		INNER JOIN Common.Consecutive CON WITH(NOLOCK) ON CON.Code=5 OR CON.[Description]='INCAPACIDADES'
		WHERE T.TypeNovelty=2 OR T.TypeNovelty=3 --Sanciones y Licencias.
		)

		INSERT INTO Payroll.Novelty (Consecutive,GroupId,EmployeeId,TypeNovelty,Extension,IBC,EmployeeBaseSalary,RealDate,Days,EndDate, VacationInitialDateNovelty, VacationEndDateNovelty, LiquidatedIBCorSalary,Reason,LiquidationBase,EPSDays,EmployerDays,LicenseClass,
		InabilityClass,RiskType,NoveltyLiquidate,CalculationType,EPSRecognizeValue,PaidPayrollValue,PaidEmployerValue,Value,AutorizationNumber,ResolutionNumber,ResolutionDate,Status,CreationUser,CreationDate,ModificationUser,ModificationDate)
		OUTPUT 'Se inserto en la tabla novedades (Id='+CAST(INSERTED.Id AS varchar(10))+') ', 'Empleado: ', INSERTED.EmployeeId, inserted.RealDate, inserted.Days, inserted.Extension INTO @MessageTable (Mensaje,Columna,Nit,RealDate, Days, Extension)
		Select * from cteInsertTable T ORDER By Consecutive

		UPDATE @MessageTable SET Linea = T.Linea, Nit=T.Nit
		FROM @MessageTable MT
		INNER JOIN Payroll.Employee E ON E.Id = MT.Nit
		INNER JOIN Common.ThirdParty TP ON TP.Id = E.ThirdPartyId
		INNER JOIN @dataFile T ON T.Nit = TP.Nit AND MT.RealDate = T.RealDate AND MT.Days = T.Days AND T.Extension = MT.Extension
		
		---Actualizacion Consecutivo de la tabla Common.Consecutive.
		UPDATE Common.Consecutive SET NumberConsecutive = (SELECT Max(Consecutive) FROM Payroll.Novelty) WHERE Code=5 OR [Description]='INCAPACIDADES'
		INSERT INTO @MessageTable (Linea,Mensaje,Columna,Nit)
		SELECT Linea=1, 'Se actualizo ultimo consecutivo. ', 'Consecutivo: ', CAST((SELECT Max(Consecutive) FROM Payroll.Novelty) AS VARCHAR(50))

		----Calculo de dias a correr en las vacaciones.
		---------------<----------------->--------- Vacaciones
		-----------<---1---><-2-><------3----->---- ActionVacation
		-----------<------------4------------->---- Rango de Novedad mayor al Rango de vacaciones.
		----<---5---->---------------------<--5-->- Novedad por fuera de las vacaciones
		UPDATE @dataFile 
		SET DaysOffset =  CASE WHEN T.ActionVacation=1 THEN DATEDIFF(DAY,V.VacationStartDate,T.EndDate) 
		WHEN T.ActionVacation=2 THEN DATEDIFF(DAY,T.RealDate,T.EndDate) 
		WHEN T.ActionVacation=3 THEN DATEDIFF(DAY,T.RealDate,V.VacationEndDate)
		WHEN T.ActionVacation=4 THEN DATEDIFF(DAY,V.VacationStartDate,V.VacationEndDate)
		ELSE 0
		END
		FROM @dataFile T
		INNER JOIN Common.ThirdParty tPrty WITH(NOLOCK) ON tPrty.Nit = T.Nit
		INNER JOIN Payroll.Employee emply WITH(NOLOCK) ON emply.ThirdPartyId = tPrty.Id
		INNER JOIN Payroll.[Contract] cntrc WITH(NOLOCK) ON cntrc.EmployeeId = emply.Id AND cntrc.Valid = 1 AND cntrc.[Status] = 1
		INNER JOIN Payroll.[Group] G WITH(NOLOCK) ON G.Id = cntrc.GroupId
		INNER JOIN Payroll.VacationPeriod VP WITH(NOLOCK) ON VP.ContractId = cntrc.Id AND VP.TakenDays>0 AND VP.EmployeeId = emply.Id AND VP.PendingDays<>0
		INNER JOIN Payroll.Vacation V WITH(NOLOCK) ON V.VacationPeriodId = VP.Id
		--WHERE ActionVacation<>5

		------------Verificar que se este cargando bien el ActionSchedule.
		------------Select T.*, G.NextDateLiquidation from @dataFile T
		------------INNER JOIN Common.ThirdParty tPrty WITH(NOLOCK) ON tPrty.Nit = T.Nit
		------------INNER JOIN Payroll.Employee emply WITH(NOLOCK) ON emply.ThirdPartyId = tPrty.Id
		------------INNER JOIN Payroll.[Contract] cntrc WITH(NOLOCK) ON cntrc.EmployeeId = emply.Id AND cntrc.Valid = 1 AND cntrc.[Status] = 1
		------------INNER JOIN Payroll.[Group] G WITH(NOLOCK) ON G.Id = cntrc.GroupId
		------------ORDER BY T.Linea

		
		----ACTTUALIZACION A LAS VACACIONES.
		UPDATE Payroll.Vacation SET IncorporationDate = DATEADD(DAY,T.DaysOffset,ISNULL(V.IncorporationDate,''))
		--SELECT /*T.*,  DATEADD(DAY,T.DaysOffset,ISNULL(V.IncorporationDate,'')) , V.IncorporationDate ,*/ V.*, DATEADD(DAY,T.DaysOffset,ISNULL(V.IncorporationDate,''))
		FROM @dataFile T
		INNER JOIN Common.ThirdParty tPrty WITH(NOLOCK) ON tPrty.Nit = T.Nit
		INNER JOIN Payroll.Employee emply WITH(NOLOCK) ON emply.ThirdPartyId = tPrty.Id
		INNER JOIN Payroll.[Contract] cntrc WITH(NOLOCK) ON cntrc.EmployeeId = emply.Id AND cntrc.Valid = 1 AND cntrc.[Status] = 1
		INNER JOIN Payroll.[Group] G WITH(NOLOCK) ON G.Id = cntrc.GroupId
		INNER JOIN Payroll.VacationPeriod VP WITH(NOLOCK) ON VP.ContractId = cntrc.Id AND VP.TakenDays>0 AND VP.EmployeeId = emply.Id AND VP.PendingDays<>0
		INNER JOIN Payroll.Vacation V WITH(NOLOCK) ON V.VacationPeriodId = VP.Id
		WHERE T.TypeNovelty = 1

		---- GUARDADO EN EL MENSAJE DE SALIDA*--

		INSERT INTO @MessageTable(Linea,Mensaje,Columna,Nit)
		SELECT Linea=T.Linea ,Mensaje='Se actualizo fecha de incorporacion ('+  CAST(DATEADD(DAY,T.DaysOffset,ISNULL(V.IncorporationDate,'')) AS varchar(50)) +') en la tabla de vacaiciones. ', Columna='Empleado: ', T.Nit
		FROM @dataFile T
		INNER JOIN Common.ThirdParty tPrty WITH(NOLOCK) ON tPrty.Nit = T.Nit
		INNER JOIN Payroll.Employee emply WITH(NOLOCK) ON emply.ThirdPartyId = tPrty.Id
		INNER JOIN Payroll.[Contract] cntrc WITH(NOLOCK) ON cntrc.EmployeeId = emply.Id AND cntrc.Valid = 1 AND cntrc.[Status] = 1
		INNER JOIN Payroll.[Group] G WITH(NOLOCK) ON G.Id = cntrc.GroupId
		INNER JOIN Payroll.VacationPeriod VP WITH(NOLOCK) ON VP.ContractId = cntrc.Id AND VP.TakenDays>0 AND VP.EmployeeId = emply.Id AND VP.PendingDays<>0
		INNER JOIN Payroll.Vacation V WITH(NOLOCK) ON V.VacationPeriodId = VP.Id
		WHERE T.TypeNovelty = 1

		------ACTUALIZACION periodo de causación.
		UPDATE Payroll.VacationPeriod  SET InitialDatePeriod = DATEADD(DAY,T.[days],VP.InitialDatePeriod) , EndDatePeriod = DATEADD(DAY,T.[days],VP.EndDatePeriod)
		--SELECT InitialDatePeriod = DATEADD(DAY,T.[days],VP.InitialDatePeriod) , EndDatePeriod = DATEADD(DAY,T.[days],VP.EndDatePeriod), T.Linea, T.realDate,T.EndDate, VP.InitialDatePeriod , VP.EndDatePEriod, T.Nit, T.Days
		FROM @dataFile T
		INNER JOIN Common.ThirdParty tPrty WITH(NOLOCK) ON tPrty.Nit = T.Nit
		INNER JOIN Payroll.Employee emply WITH(NOLOCK) ON emply.ThirdPartyId = tPrty.Id
		INNER JOIN Payroll.[Contract] cntrc WITH(NOLOCK) ON cntrc.EmployeeId = emply.Id AND cntrc.Valid = 1 AND cntrc.[Status] = 1
		INNER JOIN (Select ROW_NUMBER() OVER(PARTITION BY EMployeeId ORDER BY InitialDatePeriod Desc) as Row1,EmployeeId,InitialDatePeriod,EndDatePeriod  from Payroll.VacationPeriod WITH(NOLOCK)) VP 
		ON VP.EmployeeId = emply.Id AND ((T.RealDate BETWEEN VP.InitialDatePeriod AND VP.EndDatePEriod) OR (T.EndDate BETWEEN VP.InitialDatePeriod AND VP.EndDatePEriod)) AND Row1=1
		WHERE T.TypeNovelty=2 and Payroll.VacationPeriod.EmployeeId = emply.Id

		----------- GUARDADO EN EL MENSAJE DE SALIDA*--

		INSERT INTO @MessageTable(Linea,Mensaje,Columna,Nit)
		SELECT Linea= T.Linea ,'Se actualizó periodo de causación; Inicial(' + CAST(DATEADD(DAY,T.[days],VP.InitialDatePeriod) AS VARCHAR(50)) + ') / Final ('+CAST(DATEADD(DAY,T.[days],VP.EndDatePeriod) AS VARCHAR(50))+') en la tabla de periodo de vacaciones. ', 'Empleado: ', T.Nit
		FROM @dataFile T
		INNER JOIN Common.ThirdParty tPrty WITH(NOLOCK) ON tPrty.Nit = T.Nit
		INNER JOIN Payroll.Employee emply WITH(NOLOCK) ON emply.ThirdPartyId = tPrty.Id
		INNER JOIN Payroll.[Contract] cntrc WITH(NOLOCK) ON cntrc.EmployeeId = emply.Id AND cntrc.Valid = 1 AND cntrc.[Status] = 1
		INNER JOIN (Select ROW_NUMBER() OVER(PARTITION BY EMployeeId ORDER BY InitialDatePeriod Desc) as Row1,EmployeeId,InitialDatePeriod,EndDatePeriod  from Payroll.VacationPeriod WITH(NOLOCK)) VP 
		ON VP.EmployeeId = emply.Id AND ((T.RealDate BETWEEN VP.InitialDatePeriod AND VP.EndDatePEriod) OR (T.EndDate BETWEEN VP.InitialDatePeriod AND VP.EndDatePEriod)) AND Row1=1
		WHERE T.TypeNovelty=2 

		----ACTUALIZACION CUADRO DE TURNOS.
		---------------<-----Nomina---->----------- Periodo a liquidar nomina
		-----<---1--->----------------------------- ActionSchedule - No se cambia el cuadro de turnos.
		-----<----2---->--2-->--------------------- ActionSchedule - Se actualizan los dias que se encontraton dentro del periodo a liquidar y se eliminan los registros de ScheduleHour y ScheduleConcept.
		------------------<----3---->-------------- ActionSchedule - Se eliminan todos los registros.
		UPDATE @dataFile
		SET DaysOffsetSchedule =  CASE WHEN T.ActionSchedule=2 THEN DATEDIFF(DAY,G.NextDateLiquidation,T.EndDate)+1 WHEN T.ActionSchedule=3 THEN DATEDIFF(DAY,T.RealDate,T.EndDate) ELSE 0 END,
		InitialDateSchedule = CASE WHEN T.ActionSchedule=2 THEN G.NextDateLiquidation WHEN T.ActionSchedule=3 THEN T.RealDate  END ,
		EndDateSchedule = CASE WHEN T.ActionSchedule=2 THEN DATEADD(DAY,DATEDIFF(DAY,G.NextDateLiquidation,T.EndDate),G.NextDateLiquidation) WHEN T.ActionSchedule=3 THEN T.EndDate ELSE '' END
		FROM @dataFile T
		INNER JOIN Common.ThirdParty tPrty WITH(NOLOCK) ON tPrty.Nit = T.Nit
		INNER JOIN Payroll.Employee emply WITH(NOLOCK) ON emply.ThirdPartyId = tPrty.Id
		INNER JOIN Payroll.[Contract] cntrc WITH(NOLOCK) ON cntrc.EmployeeId = emply.Id AND cntrc.Valid = 1 AND cntrc.[Status] = 1
		INNER JOIN Payroll.[Group] G WITH(NOLOCK) ON G.Id = cntrc.GroupId
		WHERE T.ActionSchedule = 2 OR T.ActionSchedule = 3

		------SIEMPRE Y CUANDO EXISTAN TURNOS YA PROGRAMADOS PARA ESE EMPLEADO.
		------ACTUALIZAR SCHEDULE DETAIL.

		UPDATE Payroll.ScheduleDetail SET Letter=CASE WHEN T.TypeNovelty=1 THEN 'I' WHEN T.TypeNovelty=2 THEN 'S' WHEN T.TypeNovelty=3 THEN 'L' END
		--SELECT T.*,Letter=CASE WHEN T.TypeNovelty=1 THEN 'I' WHEN T.TypeNovelty=2 THEN 'S' WHEN T.TypeNovelty=3 THEN 'L' END, emply.Id, SD.Id
		FROM @dataFile T 
		INNER JOIN Common.ThirdParty tPrty WITH(NOLOCK) ON tPrty.Nit = T.Nit
		INNER JOIN Payroll.Employee emply WITH(NOLOCK) ON emply.ThirdPartyId = tPrty.Id
		INNER JOIN Payroll.[Contract] cntrc WITH(NOLOCK) ON cntrc.EmployeeId = emply.Id AND cntrc.Valid = 1 AND cntrc.[Status] = 1
		INNER JOIN Payroll.Schedule S WITH(NOLOCK) ON S.EmployeeId = emply.Id AND ((YEAR (T.RealDate) = YEAR(CONVERT(DATE,'01/'+[Period],103)) AND MONTH(T.RealDate) = MONTH(CONVERT(DATE,'01/'+[Period],103)) ) OR
		(YEAR (T.EndDate) = YEAR(CONVERT(DATE,'01/'+[Period],103)) AND MONTH(T.EndDate) = MONTH(CONVERT(DATE,'01/'+[Period],103))))
		LEFT OUTER JOIN Payroll.ScheduleDetail SD WITH(NOLOCK) ON SD.Id IN (S.D01,S.D02,S.D03,S.D04,S.D05,S.D06,S.D07,S.D08,S.D09,S.D10,S.D11,S.D12,S.D13,S.D14,S.D15,S.D16,S.D17,S.D18,S.D19,S.D20,S.D21,S.D22,S.D23,S.D24,S.D25,S.D26,S.D27,S.D28,S.D29,S.D30,S.D31)
		WHERE T.ActionSchedule = 2 OR T.ActionSchedule=3

		---- GUARDADO EN EL MENSAJE DE SALIDA*--

		INSERT INTO @MessageTable(Linea,Mensaje,Columna,Nit)
		SELECT Linea=T.Linea , 'Se actualizó turno existente en la tabla de cuadro de turnos (detalle) ; Id = '+ CAST(SD.Id AS varchar(10)) +' . ', 'Empleado: ', T.Nit
		FROM @dataFile T 
		INNER JOIN Common.ThirdParty tPrty WITH(NOLOCK) ON tPrty.Nit = T.Nit
		INNER JOIN Payroll.Employee emply WITH(NOLOCK) ON emply.ThirdPartyId = tPrty.Id
		INNER JOIN Payroll.[Contract] cntrc WITH(NOLOCK) ON cntrc.EmployeeId = emply.Id AND cntrc.Valid = 1 AND cntrc.[Status] = 1
		INNER JOIN Payroll.Schedule S WITH(NOLOCK) ON S.EmployeeId = emply.Id AND ((YEAR (T.RealDate) = YEAR(CONVERT(DATE,'01/'+[Period],103)) AND MONTH(T.RealDate) = MONTH(CONVERT(DATE,'01/'+[Period],103)) ) OR
		(YEAR (T.EndDate) = YEAR(CONVERT(DATE,'01/'+[Period],103)) AND MONTH(T.EndDate) = MONTH(CONVERT(DATE,'01/'+[Period],103))))
		LEFT OUTER JOIN Payroll.ScheduleDetail SD WITH(NOLOCK) ON SD.Id IN (S.D01,S.D02,S.D03,S.D04,S.D05,S.D06,S.D07,S.D08,S.D09,S.D10,S.D11,S.D12,S.D13,S.D14,S.D15,S.D16,S.D17,S.D18,S.D19,S.D20,S.D21,S.D22,S.D23,S.D24,S.D25,S.D26,S.D27,S.D28,S.D29,S.D30,S.D31)
		WHERE T.ActionSchedule = 2 OR T.ActionSchedule=3

			---------- GUARDADO EN EL MENSAJE DE SALIDA ((SE GUARDA EL MENSAJE ANTES DE BORRAR PRECISAMENTE)--

		INSERT INTO @MessageTable(Linea,Mensaje,Columna,Nit)
		SELECT Linea=T.Linea, 'Se eliminó turno existente en la tabla de cuadro de turnos (detalle-hora-concepto). Id='+ CAST(SDC.Id AS varchar(10)) +' ', 'Empleado: ', T.Nit
		FROM @dataFile T 
		INNER JOIN Common.ThirdParty tPrty WITH(NOLOCK) ON tPrty.Nit = T.Nit
		INNER JOIN Payroll.Employee emply WITH(NOLOCK) ON emply.ThirdPartyId = tPrty.Id
		INNER JOIN Payroll.[Contract] cntrc WITH(NOLOCK) ON cntrc.EmployeeId = emply.Id AND cntrc.Valid = 1 AND cntrc.[Status] = 1
		INNER JOIN Payroll.Schedule S WITH(NOLOCK) ON S.EmployeeId = emply.Id AND ((YEAR (T.RealDate) = YEAR(CONVERT(DATE,'01/'+[Period],103)) AND MONTH(T.RealDate) = MONTH(CONVERT(DATE,'01/'+[Period],103)) ) OR
		(YEAR (T.EndDate) = YEAR(CONVERT(DATE,'01/'+[Period],103)) AND MONTH(T.EndDate) = MONTH(CONVERT(DATE,'01/'+[Period],103))))
		LEFT OUTER JOIN Payroll.ScheduleDetail SD WITH(NOLOCK) ON SD.Id IN (S.D01,S.D02,S.D03,S.D04,S.D05,S.D06,S.D07,S.D08,S.D09,S.D10,S.D11,S.D12,S.D13,S.D14,S.D15,S.D16,S.D17,S.D18,S.D19,S.D20,S.D21,S.D22,S.D23,S.D24,S.D25,S.D26,S.D27,S.D28,S.D29,S.D30,S.D31)
		LEFT OUTER JOIN Payroll.ScheduleDetailHour SDH WITH(NOLOCK) ON SDH.ScheduleDetailId = SD.ID
		LEFT OUTER JOIN Payroll.ScheduleDetailConcept SDC WITH(NOLOCK) ON SDC.ScheduleDetailHourId = SDH.ID
		WHERE T.ActionSchedule = 2 OR T.ActionSchedule=3

		----ELIMINAR SCHEDULE HOUR Y CONCEPT.
		DELETE Payroll.ScheduleDetailConcept
		--SELECT T.*, SD.ID as SD_Id,SDH.ID 
		FROM @dataFile T 
		INNER JOIN Common.ThirdParty tPrty WITH(NOLOCK) ON tPrty.Nit = T.Nit
		INNER JOIN Payroll.Employee emply WITH(NOLOCK) ON emply.ThirdPartyId = tPrty.Id
		INNER JOIN Payroll.[Contract] cntrc WITH(NOLOCK) ON cntrc.EmployeeId = emply.Id AND cntrc.Valid = 1 AND cntrc.[Status] = 1
		INNER JOIN Payroll.Schedule S WITH(NOLOCK) ON S.EmployeeId = emply.Id AND ((YEAR (T.RealDate) = YEAR(CONVERT(DATE,'01/'+[Period],103)) AND MONTH(T.RealDate) = MONTH(CONVERT(DATE,'01/'+[Period],103)) ) OR
		(YEAR (T.EndDate) = YEAR(CONVERT(DATE,'01/'+[Period],103)) AND MONTH(T.EndDate) = MONTH(CONVERT(DATE,'01/'+[Period],103))))
		LEFT OUTER JOIN Payroll.ScheduleDetail SD WITH(NOLOCK) ON SD.Id IN (S.D01,S.D02,S.D03,S.D04,S.D05,S.D06,S.D07,S.D08,S.D09,S.D10,S.D11,S.D12,S.D13,S.D14,S.D15,S.D16,S.D17,S.D18,S.D19,S.D20,S.D21,S.D22,S.D23,S.D24,S.D25,S.D26,S.D27,S.D28,S.D29,S.D30,S.D31)
		LEFT OUTER JOIN Payroll.ScheduleDetailHour SDH WITH(NOLOCK) ON SDH.ScheduleDetailId = SD.ID
		WHERE T.ActionSchedule = 2 OR T.ActionSchedule=3 --AND Payroll.ScheduleDetailConcept.ScheduleDetailHourId = SDH.ID

		---------- GUARDADO EN EL MENSAJE DE SALIDA ((SE GUARDA EL MENSAJE ANTES DE BORRAR PRECISAMENTE)--

		INSERT INTO @MessageTable(Linea,Mensaje,Columna,Nit)
		SELECT Linea=T.Linea, 'Se eliminó turno existente en la tabla de cuadro de turnos (detalle-hora). Id = '+ CAST(SDH.Id AS VARCHAR(10)) +' ', 'Empleado: ', T.Nit
		FROM @dataFile T 
		INNER JOIN Common.ThirdParty tPrty WITH(NOLOCK) ON tPrty.Nit = T.Nit
		INNER JOIN Payroll.Employee emply WITH(NOLOCK) ON emply.ThirdPartyId = tPrty.Id
		INNER JOIN Payroll.[Contract] cntrc WITH(NOLOCK) ON cntrc.EmployeeId = emply.Id AND cntrc.Valid = 1 AND cntrc.[Status] = 1
		INNER JOIN Payroll.Schedule S WITH(NOLOCK) ON S.EmployeeId = emply.Id AND ((YEAR (T.RealDate) = YEAR(CONVERT(DATE,'01/'+[Period],103)) AND MONTH(T.RealDate) = MONTH(CONVERT(DATE,'01/'+[Period],103)) ) OR
		(YEAR (T.EndDate) = YEAR(CONVERT(DATE,'01/'+[Period],103)) AND MONTH(T.EndDate) = MONTH(CONVERT(DATE,'01/'+[Period],103))))
		LEFT OUTER JOIN Payroll.ScheduleDetail SD WITH(NOLOCK) ON SD.Id IN (S.D01,S.D02,S.D03,S.D04,S.D05,S.D06,S.D07,S.D08,S.D09,S.D10,S.D11,S.D12,S.D13,S.D14,S.D15,S.D16,S.D17,S.D18,S.D19,S.D20,S.D21,S.D22,S.D23,S.D24,S.D25,S.D26,S.D27,S.D28,S.D29,S.D30,S.D31)
		LEFT OUTER JOIN Payroll.ScheduleDetailHour SDH WITH(NOLOCK) ON SDH.ScheduleDetailId = SD.Id
		WHERE T.ActionSchedule = 2 OR T.ActionSchedule=3

		---------ELIMINAR DE SCHEDULE DETAIL HOUR
		DELETE Payroll.ScheduleDetailHour 
		FROM @dataFile T 
		INNER JOIN Common.ThirdParty tPrty WITH(NOLOCK) ON tPrty.Nit = T.Nit
		INNER JOIN Payroll.Employee emply WITH(NOLOCK) ON emply.ThirdPartyId = tPrty.Id
		INNER JOIN Payroll.[Contract] cntrc WITH(NOLOCK) ON cntrc.EmployeeId = emply.Id AND cntrc.Valid = 1 AND cntrc.[Status] = 1
		INNER JOIN Payroll.Schedule S WITH(NOLOCK) ON S.EmployeeId = emply.Id AND ((YEAR (T.RealDate) = YEAR(CONVERT(DATE,'01/'+[Period],103)) AND MONTH(T.RealDate) = MONTH(CONVERT(DATE,'01/'+[Period],103)) ) OR
		(YEAR (T.EndDate) = YEAR(CONVERT(DATE,'01/'+[Period],103)) AND MONTH(T.EndDate) = MONTH(CONVERT(DATE,'01/'+[Period],103))))
		LEFT OUTER JOIN Payroll.ScheduleDetail SD WITH(NOLOCK) ON SD.Id IN (S.D01,S.D02,S.D03,S.D04,S.D05,S.D06,S.D07,S.D08,S.D09,S.D10,S.D11,S.D12,S.D13,S.D14,S.D15,S.D16,S.D17,S.D18,S.D19,S.D20,S.D21,S.D22,S.D23,S.D24,S.D25,S.D26,S.D27,S.D28,S.D29,S.D30,S.D31)
		WHERE T.ActionSchedule = 2 OR T.ActionSchedule=3 AND Payroll.ScheduleDetailHour.ScheduleDetailId = SD.ID

		

		----EN CASO DE QUE NO EXISTAN TURNOS PARA ESE EMPLEADO, SE DEBE INSERTAR EN SCHEDULE Y SCHEDULEDETAIL.
		
		--Select * FROM @dataFile

		DECLARE @MaxLinea INT
		DECLARE @CurrentLinea INT = 0
		DECLARE @CurrentDay INT = 0
		DECLARE @Count INT = 0

		
		INSERT @dataFileWhile
		SELECT * FROM (SELECT T.Linea,GroupId=G.Id, EmployeeId=emply.Id, ContractId=cntrc.Id, CompanyId= C.Id, BranchOfficeId=BO.Id, FuncionalUnitId= FU.Id, CenterCostId=CC.Id ,ScheduleFuncionalUnitId= FU.Id,
		PayrollLiquidationNumber=NULL, Letter = CASE WHEN T.TypeNovelty=1 THEN 'I' WHEN T.TypeNovelty=2 THEN 'S' WHEN T.TypeNovelty=3 THEN 'L' END,
		DateDetail= T.InitialDateSchedule, TotalNumberHours = 0, ScheduleTemplateId = NULL, Status = 0, State = 0, T.DaysOffsetSchedule
		FROM @dataFile T
		INNER JOIN Common.ThirdParty tPrty WITH(NOLOCK) ON tPrty.Nit = T.Nit
		INNER JOIN Payroll.Employee emply WITH(NOLOCK) ON emply.ThirdPartyId = tPrty.Id
		INNER JOIN Payroll.[Contract] cntrc WITH(NOLOCK) ON cntrc.EmployeeId = emply.Id AND cntrc.Valid = 1 AND cntrc.[Status] = 1
		INNER JOIN Payroll.[Group] G WITH(NOLOCK) ON G.Id = cntrc.GroupId
		INNER JOIN Payroll.CostCenter CC WITH(NOLOCK) ON CC.Id = emply.CostCenterId
		INNER JOIN Payroll.FunctionalUnit FU WITH(NOLOCK) ON FU.CostCenterId= CC.Id
		INNER JOIN Payroll.BranchOffice BO WITH(NOLOCK) ON BO.Id = FU.BranchOfficeId
		INNER JOIN Payroll.Company C WITH(NOLOCK) ON C.Id = BO.CompanyId
		LEFT OUTER JOIN Payroll.Schedule S WITH(NOLOCK) ON S.EmployeeId = emply.Id AND ((YEAR (T.RealDate) = YEAR(CONVERT(DATE,'01/'+[Period],103)) AND MONTH(T.RealDate) = MONTH(CONVERT(DATE,'01/'+[Period],103)) ) OR
		(YEAR (T.EndDate) = YEAR(CONVERT(DATE,'01/'+[Period],103)) AND MONTH(T.EndDate) = MONTH(CONVERT(DATE,'01/'+[Period],103))))
		LEFT OUTER JOIN Payroll.ScheduleDetail SD WITH(NOLOCK) ON SD.Id IN (S.D01,S.D02,S.D03,S.D04,S.D05,S.D06,S.D07,S.D08,S.D09,S.D10,S.D11,S.D12,S.D13,S.D14,S.D15,S.D16,S.D17,S.D18,S.D19,S.D20,S.D21,S.D22,S.D23,S.D24,S.D25,S.D26,S.D27,S.D28,S.D29,S.D30,S.D31)
		WHERE SD.Id IS NULL AND T.DaysOffsetSchedule IS NOT NULL AND T.DaysOffsetSchedule>0 ) T ORDER BY T.Linea
		
		SELECT @MaxLinea=Max(Linea)
		FROM @dataFileWhile

		--SELECT * FROM @dataFileWhile ORDER BY Linea
		--SELECT @MaxLinea --8
		
		
		----CICLO PARA REPLICAR REGISTROS N VECES:
		SET @CurrentLinea = 1
		WHILE @CurrentLinea <= @MaxLinea
		BEGIN
			SELECT @CurrentDay=DaysOffsetSchedule FROM @dataFileWhile DF WHERE DF.Linea = @CurrentLinea
			IF @@ROWCOUNT > 0
			BEGIN
				SET @Count = 0
				WHILE(@Count<@CurrentDay)
				BEGIN
					INSERT @Table4CrossJoin
					SELECT TCJ.Linea, TCJ.DaysOffsetSchedule, TCJ.EmployeeId, DateDetail=DATEADD(DAY,@Count,TCJ.DateDetail) FROM @dataFileWhile AS TCJ WHERE TCJ.Linea = @CurrentLinea
					SET @Count=@Count+1
				END
			END
			SET @CurrentLinea = @CurrentLinea+1
		END

		----- INSERTAR EN Payroll.SCHEDULE-DETAIL
		INSERT Payroll.ScheduleDetail
			(GroupId, EmployeeId, ContractId, CompanyId, BranchOfficeId, FunctionalUnitId,
			 CenterCostId, ScheduleFunctionalUnitId, PayrollLiquidationNumber, Letter,
			 DateDetail, TotalNumberHours, ScheduleTemplateId, Status, State)
		OUTPUT INSERTED.Id, YEAR(INSERTED.DateDetail) AS [YEAR] , MONTH(INSERTED.DateDetail) AS [MONTH],  INSERTED.EmployeeId, DAY(INSERTED.DateDetail) AS [DAY], INSERTED.DateDetail, INSERTED.FunctionalUnitId, INSERTED.Letter INTO @InsertedScheduleDetail (Id,[YEAR],[MONTH],EmployeeId,[DAY],DateDetail, FuncionalUnitId, Letter)
		SELECT DFW.GroupId,DFW.EmployeeId,DFW.ContractId,DFW.CompanyId, DFW.BranchOfficeId, DFW.FuncionalUnitId, DFW.CenterCostId, DFW.ScheduleFuncionalUnitId, DFW.PayrollLiquidationNumber, DFW.Letter, TCJ.DateDetail, DFW.TotalNumberHours, DFW.ScheduleTemplateId,DFW.Status, DFW.State
		FROM @dataFileWhile DFW INNER JOIN @Table4CrossJoin TCJ ON DFW.EmployeeId = TCJ.EmployeeId AND DFW.Linea = TCJ.Linea ORDER BY TCJ.Linea

		---- GUARDADO EN EL MENSAJE DE SALIDA*--

		INSERT INTO @MessageTable (Linea,Mensaje,Columna,Nit)
		SELECT Linea=T.Linea, 'Se insertó registro en la tabla de cuadro de turnos (detalle). Id = '+ CAST(ID.Id AS VARCHAR (10)) +' ', 'Empleado: ', TP.Nit
		FROM @dataFileWhile DFW
		INNER JOIN @Table4CrossJoin TCJ ON DFW.EmployeeId = TCJ.EmployeeId AND DFW.Linea = TCJ.Linea 
		INNER JOIN Payroll.Employee E WITH(NOLOCK) ON E.Id = DFW.EmployeeId
		INNER JOIN Common.ThirdParty TP WITH(NOLOCK) ON TP.Id = E.ThirdPartyId
		INNER JOIN @dataFile T ON T.Nit = TP.Nit
		INNER JOIN @InsertedScheduleDetail ID ON ID.EmployeeId = E.Id
		GROUP BY TP.Nit, ID.Id, T.Linea
		ORDER BY T.Linea

		--SELECT * FROM @InsertedScheduleDetail

		-------INSERTAR EN SCHEDULE CABECERAAAAA
			INSERT Payroll.Schedule ([EmployeeId],[Period],[FunctionalUnitId],[D01],[D02],[D03],[D04],[D05],[D06],[D07],[D08],[D09],[D10],[D11] ,[D12],[D13],[D14],[D15],[D16],[D17],[D18],[D19],[D20],[D21],[D22],[D23],[D24],[D25],[D26],[D27],[D28],[D29],[D30],[D31],[TotalHour],[State])
			SELECT EmployeeId, [Period]=CAST(MES AS VARCHAR(10))+'/'+CAST(AÑO AS VARCHAR(10)),FuncionalUnitId, min(d1),min(d2),min(d3),min(d4),min(d5),min(d6),min(d7),min(d8),min(d9),min(d10),min(d11),min(d12),min(d13),min(d14),min(d15),min(d16),min(d17),min(d18),min(d19),min(d20),
			min(d21),min(d22),min(d23),min(d24),min(d25),min(d26),min(d27),min(d28),min(d29),min(d30),min(d31), TotalHour = 0, State = 0
			FROM
			(
			select ISD.[YEAR] as AÑO, ISD.[MONTH] MES, ISD.EmployeeId EmployeeId, ISD.FuncionalUnitId,
			d1 = case when ISD.[DAY] = 1 then ISD.id else null end,
			d2 = case when ISD.[DAY] = 2 then ISD.id else null end, 
			d3 = case when ISD.[DAY] = 3 then ISD.id else null end,
			d4 = case when ISD.[DAY] = 4 then ISD.id else null end,
			d5 = case when ISD.[DAY] = 5 then ISD.id else null end,
			d6 = case when ISD.[DAY] = 6 then ISD.id else null end,
			d7 = case when ISD.[DAY] = 7 then ISD.id else null end,
			d8 = case when ISD.[DAY] = 8 then ISD.id else null end,
			d9 = case when ISD.[DAY] = 9 then ISD.id else null end,
			d10 = case when ISD.[DAY] = 10 then ISD.id else null end,
			d11 = case when ISD.[DAY] = 11 then ISD.id else null end,
			d12 = case when ISD.[DAY] = 12 then ISD.id else null end,
			d13 = case when ISD.[DAY] = 13 then ISD.id else null end,
			d14 = case when ISD.[DAY] = 14 then ISD.id else null end,
			d15 = case when ISD.[DAY] = 15 then ISD.id else null end,
			d16 = case when ISD.[DAY] = 16 then ISD.id else null end,
			d17 = case when ISD.[DAY] = 17 then ISD.id else null end,
			d18 = case when ISD.[DAY] = 18 then ISD.id else null end,
			d19 = case when ISD.[DAY] = 19 then ISD.id else null end,
			d20 = case when ISD.[DAY] = 20 then ISD.id else null end,
			d21 = case when ISD.[DAY] = 21 then ISD.id else null end,
			d22 = case when ISD.[DAY] = 22 then ISD.id else null end,
			d23 = case when ISD.[DAY] = 23 then ISD.id else null end,
			d24 = case when ISD.[DAY] = 24 then ISD.id else null end,
			d25 = case when ISD.[DAY] = 25 then ISD.id else null end,
			d26 = case when ISD.[DAY] = 26 then ISD.id else null end,
			d27 = case when ISD.[DAY] = 27 then ISD.id else null end,
			d28 = case when ISD.[DAY] = 28 then ISD.id else null end,
			d29 = case when ISD.[DAY] = 29 then ISD.id else null end,
			d30 = case when ISD.[DAY] = 30 then ISD.id else null end,
			d31 = case when ISD.[DAY] = 31 then ISD.id else null end
			FROM
			@InsertedScheduleDetail ISD
			) t
			group by AÑo, MES, EmployeeId, FuncionalUnitId

			-- GUARDADO EN EL MENSAJE DE SALIDA*--

			INSERT INTO @MessageTable(Linea,Mensaje,Columna,Nit)
			SELECT Linea= DT.Linea , 'Se insertó registro en la tabla de cuadro de turnos. Periodo = '+ CAST(MES AS VARCHAR(10))+'/'+CAST(AÑO AS VARCHAR(10)) +' ', 'Empleado: ', TP.Nit
			FROM
			(
			select ISD.[YEAR] as AÑO, ISD.[MONTH] MES, ISD.EmployeeId EmployeeId, ISD.FuncionalUnitId, ISD.Id,
			d1 = case when ISD.[DAY] = 1 then ISD.id else null end,
			d2 = case when ISD.[DAY] = 2 then ISD.id else null end, 
			d3 = case when ISD.[DAY] = 3 then ISD.id else null end,
			d4 = case when ISD.[DAY] = 4 then ISD.id else null end,
			d5 = case when ISD.[DAY] = 5 then ISD.id else null end,
			d6 = case when ISD.[DAY] = 6 then ISD.id else null end,
			d7 = case when ISD.[DAY] = 7 then ISD.id else null end,
			d8 = case when ISD.[DAY] = 8 then ISD.id else null end,
			d9 = case when ISD.[DAY] = 9 then ISD.id else null end,
			d10 = case when ISD.[DAY] = 10 then ISD.id else null end,
			d11 = case when ISD.[DAY] = 11 then ISD.id else null end,
			d12 = case when ISD.[DAY] = 12 then ISD.id else null end,
			d13 = case when ISD.[DAY] = 13 then ISD.id else null end,
			d14 = case when ISD.[DAY] = 14 then ISD.id else null end,
			d15 = case when ISD.[DAY] = 15 then ISD.id else null end,
			d16 = case when ISD.[DAY] = 16 then ISD.id else null end,
			d17 = case when ISD.[DAY] = 17 then ISD.id else null end,
			d18 = case when ISD.[DAY] = 18 then ISD.id else null end,
			d19 = case when ISD.[DAY] = 19 then ISD.id else null end,
			d20 = case when ISD.[DAY] = 20 then ISD.id else null end,
			d21 = case when ISD.[DAY] = 21 then ISD.id else null end,
			d22 = case when ISD.[DAY] = 22 then ISD.id else null end,
			d23 = case when ISD.[DAY] = 23 then ISD.id else null end,
			d24 = case when ISD.[DAY] = 24 then ISD.id else null end,
			d25 = case when ISD.[DAY] = 25 then ISD.id else null end,
			d26 = case when ISD.[DAY] = 26 then ISD.id else null end,
			d27 = case when ISD.[DAY] = 27 then ISD.id else null end,
			d28 = case when ISD.[DAY] = 28 then ISD.id else null end,
			d29 = case when ISD.[DAY] = 29 then ISD.id else null end,
			d30 = case when ISD.[DAY] = 30 then ISD.id else null end,
			d31 = case when ISD.[DAY] = 31 then ISD.id else null end
			FROM
			@InsertedScheduleDetail ISD
			) t
			INNER JOIN Payroll.Employee E ON E.Id = t.EmployeeId
			INNER JOIN Common.ThirdParty TP ON TP.Id = E.ThirdPartyId
			INNER JOIN @dataFile DT ON TP.Nit = DT.Nit AND AÑO = YEAR(DT.RealDate) AND MES = MONTH(DT.RealDate) 
			group by AÑo, MES, EmployeeId, FuncionalUnitId, DT.Linea, TP.Nit

		--	--****************************************************************************************************--

			SELECT  Linea, '' AS Employee, TypeNovelty=NULL, InabilityClass=NULL, RealDate=NULL, EndDate=NULL, [Days]=NULL, Reason=NULL, Nit=NULL, Extension=NULL, RiskType=NULL,  TipoLiquidar=NULL, PostularValor=NULL, Tipo=NULL, PaidEmployervalue=NULL, 
			EPSRecognizeValue=NULL, [Value]=NULL, ActionVacation =NULL,ActionSchedule=NULL, Mensaje, Columna, Valor=Nit, EmployerDays=NULL, EPSDays=NULL, BasicSalary =NULL
			FROM @MessageTable  WHERE Mensaje IS NOT NULL
			ORDER BY Linea

			COMMIT TRAN [tran1]
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN [tran1]
	END CATCH

END
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de validación y carga masiva de novedades de nómina a partir de un archivo XML (típicamente importado desde Excel). Recibe un lote de novedades externas (incapacidades, prórrogas, licencias y otros eventos) y ejecuta múltiples validaciones de negocio antes de confirmar su ingreso: verifica que el NIT/cédula del empleado exista en el maestro de terceros y empleados, que tenga un contrato laboral activo y vigente para el período de la novedad, que las fechas de inicio y fin no excedan los límites del contrato, y que las prórrogas de incapacidad tengan una novedad previa del mismo diagnóstico dentro del rango de 30 días permitido. Dependiendo del parámetro de acción recibido (''Validar'' o ''Confirmar''), retorna los errores encontrados por línea y columna para que el usuario pueda corregirlos, o bien confirma el cargue definitivo de las novedades al período de nómina correspondiente.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_ValidateMassiveNovelties';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_ValidateMassiveNovelties';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Novedades de nómina; Incapacidades; Prórroga de incapacidad; Sanciones; Licencias (maternidad/paternidad/no remunerada); Diagnóstico (Reason); IBC / salario base de liquidación; Salario mínimo legal; Días pagados por empleador vs EPS (tramos 1-2, 2-90, 90-180); Vacaciones y período de causación; Cuadro de turnos / Schedule; Consecutivo de incapacidades; ARL / tipo de riesgo; EPS', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveNovelties';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Action <> ''Confirmar'' → Ejecuta el flujo de validación: arma errores de tipo de dato, existencia de empleado, contrato vigente, prórroga válida, salario diario menor al mínimo y devuelve filas tipo ''Error''/''Info''/''Datos'' con los valores calculados (PaidEmployervalue, EPSRecognizeValue, ActionVacation, ActionSchedule). else Cuando @Action = ''Confirmar'', dentro de transacción tran1: inserta novedades en Payroll.Novelty, actualiza el consecutivo, ajusta vacaciones/períodos vacacionales y reorganiza los cuadros de turnos (ScheduleDetail/Hour/Concept y Schedule).; si TypeNovelty = 1 AND Extension = 1 (incapacidad con prórroga) → Inserta la novedad reutilizando el Consecutive de la novedad previa (mismo EmployeeId, Reason, TypeNovelty con DATEDIFF(EndDate,RealDate) < 30). else Si TypeNovelty=1 AND Extension=0 (incapacidad nueva) asigna Consecutive = NumberConsecutive + ROW_NUMBER(); si TypeNovelty=2 ó 3 (sanción/licencia) suma además el conteo de incapacidades del lote.; si TypeNovelty = 3 (licencia) y InabilityClass = 2 → Los días se cargan como EPSDays y EPSRecognizeValue = Days*BasicSalary/30; PaidEmployerValue = 0. else Si InabilityClass = 8 → no se paga nada (Value=0, PaidPayrollValue=0); en otros casos los días se cargan como EmployerDays y se paga Days*BasicSalary al empleador.; si ActionVacation IN (1,2,3,4) → Calcula DaysOffset según el solapamiento entre la novedad y el rango de vacaciones y, si TypeNovelty=1, actualiza Payroll.Vacation.IncorporationDate sumando DaysOffset. else ActionVacation=5: novedad fuera de vacaciones, DaysOffset=0, no se ajusta IncorporationDate.; si ActionSchedule = 2 → Solo se afectan los días posteriores a Group.NextDateLiquidation: se recalcula Letter en ScheduleDetail y se eliminan ScheduleDetailHour y ScheduleDetailConcept asociados. else ActionSchedule = 3: se eliminan/actualizan todos los días del rango de la novedad; ActionSchedule = 1: no se modifica el cuadro de turnos.; si No existe ScheduleDetail previo para el empleado en el período (SD.Id IS NULL) y DaysOffsetSchedule > 0 → Mediante ciclo WHILE genera un registro por día en @Table4CrossJoin, inserta filas en Payroll.ScheduleDetail con Letter=''I''/''S''/''L'' según TypeNovelty y luego inserta la cabecera en Payroll.Schedule pivoteando los Ids por día (D01..D31).; si TipoLiquidar = 1 → BasicSalary de cálculo = MAX(BasicSalary del contrato, LegalSalaryMinimum). else TipoLiquidar = 2: usa el IBC efectivo derivado de la última Liquidation/LiquidationDetail (suma de AccruedValue - DeductedValue de conceptos con Concept.AffectIBC=1), también acotado al salario mínimo.; si PostularValor = 1 ó 3 → Reconoce el 100% del salario diario en los tramos correspondientes (días 1-2, 2-90, 90-180). else PostularValor = 2: aplica 66.67% en tramos 1-90 y 50% en tramo 90-180 (con piso de salario mínimo diario).; si Extension = 1 y (no existe novedad previa con mismo Reason+TypeNovelty o DATEDIFF(RealDate, EndDate previa) > 30) → Genera mensaje de error ''no es prórroga / no existe incapacidad previa con ese diagnóstico o es mayor a 30 días''.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveNovelties';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveNovelties';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.ThirdParty; Payroll.Employee; Payroll.Contract; Payroll.Novelty; Payroll.Group; Payroll.PayrollParameter; Payroll.Liquidation; Payroll.LiquidationDetail; Payroll.Concept; Payroll.VacationPeriod; Payroll.Vacation; Common.Consecutive; Payroll.Schedule; Payroll.ScheduleDetail; Payroll.ScheduleDetailHour; Payroll.ScheduleDetailConcept; Payroll.CostCenter; Payroll.FunctionalUnit; Payroll.BranchOffice; Payroll.Company', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveNovelties';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveNovelties';
-- GO
