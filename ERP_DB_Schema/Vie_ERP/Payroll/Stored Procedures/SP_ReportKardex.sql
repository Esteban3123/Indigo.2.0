

-- =============================================
-- Author:		Daniel Eduardo Arévalo Bonilla
-- Create date: 28-07-2016
-- Description:	Actualiza las Provisiones de Nómina por Tipo de Provision para Nóminas CONFIRMADAS
-- =============================================
CREATE PROCEDURE [Payroll].[SP_ReportKardex]
	@IdEmployee int, -- Id Grupo
	@Year int, -- Fecha de Nomina
	@ConceptType int -- Tipo de Concepto de Nomina
AS 
BEGIN
	SET NOCOUNT ON;

	SELECT Name as NombreConcepto, ConceptType as TipoConcepto,
	coalesce([1], 0) as Enero, coalesce([2], 0) as Febrero, coalesce([3], 0) as Marzo, coalesce([4], 0) as Abril, coalesce([5], 0) as Mayo, coalesce([6], 0) as Junio, coalesce([7], 0) as Julio, coalesce([8], 0) as Agosto, coalesce([9], 0) as Septiembre, coalesce([10], 0) as Octubre, coalesce([11], 0) as Noviembre, coalesce([12], 0) as Diciembre 
	FROM
	(
		SELECT Mes, Name, ConceptType, SUM(ValorConcepto) AS ValorConcepto FROM  
		(
			SELECT MONTH(L.PayrollDateLiquidated) as Mes, C.Name, C.ConceptType, SUM(LD.ConceptTotalValue) AS ValorConcepto
			FROM Payroll.LiquidationDetail LD, Payroll.Liquidation L, Payroll.Concept C 
			WHERE LD.PayrollId = L.Id and C.Id = LD.ConceptId and year(L.PayrollDateLiquidated) = @Year and L.EmployeeId = @IdEmployee and L.RegisterStatus <> ''
			GROUP BY l.PayrollDateLiquidated, C.Code, C.Name, C.ConceptType

			UNION ALL

			SELECT MONTH(RC.NextPayrollDate) as Mes, C.Name, C.ConceptType, RD.ValueConceptWithRetroactive FROM Payroll.RetroactiveC RC, Payroll.RetroactiveD RD, Payroll.Concept c
			WHERE RD.IdRetroactiveC = RC.Id and RC.IdEmployee = @IdEmployee and C.Id = RD.IdConcept and YEAR(RC.NextPayrollDate) = @Year
	
			UNION ALL
	
			SELECT MONTH(IP.PeriodEndDate) as Mes, 'PRIMA DE SERVICIOS' AS Name, 1 as ConceptType, IP.TotalAccrued FROM Payroll.IncentivePayment IP, Payroll.Contract c
			where IP.ContractId = c.Id AND c.EmployeeId = @IdEmployee AND YEAR(IP.PeriodEndDate) = @Year and ip.[Period] = 1

			UNION ALL
	
			SELECT MONTH(IP.PeriodEndDate) as Mes, 'PRIMA DE NAVIDAD' AS Name, 1 as ConceptType, IP.TotalAccrued FROM Payroll.IncentivePayment IP, Payroll.Contract c
			where IP.ContractId = c.Id AND c.EmployeeId = @IdEmployee AND YEAR(IP.PeriodEndDate) = @Year and ip.[Period] = 2

			UNION ALL
			--En la prima tambien se pueden pagar conceptos diferentes para los detalles
			--Solución Provisional, Hay que preguntarle a Daniel en que caso las primas no genreran detalles, ya que unas simplementen sacan el valor de la cabecera.
			--Ticket 7568
			SELECT MONTH(IP.PeriodEndDate) as Mes, PC.Name, PC.ConceptType, case IPD.AccruedValue when 0 then IPD.DeductedValue else IPD.AccruedValue end as ValorConcepto
			FROM Payroll.IncentivePaymentDetail IPD, Payroll.IncentivePayment IP, Payroll.[Contract] C, Payroll.Concept PC
			where IPD.IncentivePaymentId = IP.Id AND IPD.ConceptId = PC.Id AND IP.ContractId = c.Id AND c.EmployeeId = @IdEmployee AND YEAR(IP.PeriodEndDate) = @Year
			AND PC.Code = '332'

			UNION ALL
	
			SELECT MONTH(IP.PeriodEndDate) as Mes, 'RETENCION EN LA FUENTE' AS Name, 2 as ConceptType, IP.RetentionValue FROM Payroll.IncentivePayment IP, Payroll.Contract c
			where IP.ContractId = c.Id AND c.EmployeeId = @IdEmployee AND YEAR(IP.PeriodEndDate) = @Year

			UNION ALL
			-- RETIROS
			SELECT MONTH(CL.RetirementDate) as Mes, C.Name, C.ConceptType, SUM(CLD.Accrued) + SUM(CLD.Deducted) as ValorConcepto 
			FROM Payroll.ContractLiquidation CL, Payroll.ContractLiquidationDetail CLD, Payroll.Concept C
			where YEAR(RetirementDate) = @Year AND EmployeeID = @IdEmployee AND CLD.ContractLiquidationId = CL.ID AND C.Id = CLD.IdConcept
			group by CL.RetirementDate, C.Name, C.ConceptType

		) AS OTHER GROUP BY Mes, Name, ConceptType
	) as TablaConsulta
	PIVOT
	(
		SUM(ValorConcepto)
		FOR Mes in 
		( [1], [2], [3], [4], [5], [6], [7], [8], [9], [10], [11], [12] )
	) as TablaPivote where ConceptType = @ConceptType
END