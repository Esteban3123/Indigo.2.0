

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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el kardex anual de nómina de un empleado específico, mostrando los valores de cada concepto de nómina (devengados, deducciones o aportes según el tipo solicitado) distribuidos por mes en formato de tabla pivote con columnas de Enero a Diciembre. Consolida en un único reporte todas las fuentes de pago del año: liquidaciones ordinarias de nómina, ajustes retroactivos, primas de servicios y navidad, retención en la fuente sobre incentivos y liquidaciones por retiro o desvinculación del empleado. Es el reporte de historia salarial mensual por concepto que permite auditar lo pagado o deducido a un trabajador durante un año determinado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_ReportKardex';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_ReportKardex';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte tipo kárdex anual de un empleado pivotando por mes los valores de conceptos de nómina (liquidaciones, retroactivos, primas, retenciones y liquidaciones de retiro), filtrado por tipo de concepto.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ReportKardex';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El empleado debe existir y tener movimientos en alguna de las fuentes consultadas para el año indicado; Los conceptos referenciados deben existir en Payroll.Concept (join por Id); Las liquidaciones consideradas deben tener RegisterStatus distinto de cadena vacía; Para primas de servicios se requiere IncentivePayment.Period = 1; para prima de navidad Period = 2; Para incluir el detalle adicional de prima se requiere que el concepto tenga Code = ''332''', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ReportKardex';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El mes se determina siempre por MONTH() sobre la fecha característica de cada fuente (PayrollDateLiquidated, NextPayrollDate, PeriodEndDate o RetirementDate); Solo se consideran movimientos cuyo año coincide con @Year; Las primas (servicios y navidad) y la retención en la fuente se etiquetan con nombres y ConceptType fijos, independientes de Payroll.Concept; Los retiros suman Accrued + Deducted como valor del concepto; Las liquidaciones se restringen a aquellas con RegisterStatus distinto de vacío', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ReportKardex';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Nómina; Liquidación de nómina; Retroactivo de nómina; Prima de servicios; Prima de navidad; Retención en la fuente; Liquidación de contrato (retiro); Concepto de nómina; Devengado y deducido; Kárdex anual del empleado', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ReportKardex';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve un único resultset con NombreConcepto, TipoConcepto y 12 columnas (Enero..Diciembre) con la suma de ValorConcepto por mes; los meses sin valor se reemplazan por 0 vía COALESCE; [RETURN_RESULT] Resultset: Filtra el pivote final por ConceptType = @ConceptType, devolviendo solo conceptos del tipo solicitado', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ReportKardex';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IncentivePayment.Period = 1 → Se reporta el TotalAccrued como concepto sintético ''PRIMA DE SERVICIOS'' con ConceptType=1; si IncentivePayment.Period = 2 → Se reporta el TotalAccrued como concepto sintético ''PRIMA DE NAVIDAD'' con ConceptType=1; si En IncentivePaymentDetail, IPD.AccruedValue = 0 → Se toma IPD.DeductedValue como ValorConcepto else Se toma IPD.AccruedValue como ValorConcepto; si Concepto con Code = ''332'' en IncentivePaymentDetail → Se incluye su detalle dentro del kárdex (regla provisional documentada en el ticket 7568); si Para cualquier IncentivePayment del empleado en el año → Se reporta IP.RetentionValue como concepto sintético ''RETENCION EN LA FUENTE'' con ConceptType=2', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ReportKardex';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.LiquidationDetail; Payroll.Liquidation; Payroll.Concept; Payroll.RetroactiveC; Payroll.RetroactiveD; Payroll.IncentivePayment; Payroll.Contract; Payroll.IncentivePaymentDetail; Payroll.ContractLiquidation; Payroll.ContractLiquidationDetail', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ReportKardex';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ReportKardex';
-- GO
