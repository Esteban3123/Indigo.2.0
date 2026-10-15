/****** Object:  View [Payroll].[ViewReportPayStub]    Script Date: 22/05/2025 9:42:30 a.�m. ******/
CREATE VIEW [Payroll].[ViewReportPayStub]
AS

WITH
VacationDaysInCash AS ( ---Vacaciones en dinero
    SELECT EmployeeId, EnjoyDays
    FROM (
        SELECT 
            vp.EmployeeId,
            v.EnjoyDays,
            ROW_NUMBER() OVER (PARTITION BY vp.EmployeeId ORDER BY v.VacationStartDate DESC) AS rn
        FROM Payroll.VacationPeriod vp
        JOIN Payroll.Vacation v ON v.VacationPeriodId = vp.Id
        WHERE v.TypeVacation = 1 
          AND v.State = 1
    ) x
    WHERE rn = 1
),
VacationsByConceptClass AS ( --Todos los conceptos con la clase vacaciones
	SELECT *
	FROM (
		SELECT
			l.Id AS PayrollId,
			v.VacationStartDate,
			v.VacationEndDate,
			v.TakenDays,
			v.EnjoyDays,
			c.ConceptClass,
			ROW_NUMBER() OVER (PARTITION BY l.Id ORDER BY v.VacationStartDate DESC) AS rowN
		FROM Payroll.Liquidation l
		JOIN Payroll.LiquidationDetail ld ON l.id = ld.PayrollId
		JOIN Payroll.Concept c ON ld.ConceptId = c.Id
		JOIN Payroll.VacationPeriod vp ON vp.EmployeeId = l.EmployeeId
		JOIN Payroll.Vacation v ON v.VacationPeriodId = vp.Id
		WHERE c.ConceptClass = '030'
	) AS VacData
	WHERE rowN = 1
),
PayrollBase AS (           ---Base liquidacion
    SELECT
        pld.Id as PayrollDId,
        pl.Id as PayrollCId,
        ISNULL(pld.AgreementsId, 0) AS AgreementsId,
        ct.Nit,
        ct.[Name], 
        pco.Code as Concepto, 
        pc.NumberShares,
        pl.PayrollDateLiquidated,
        pg.id as GroupId,
        pl.BasicSalary,
        pl.PeriodJCB,
        pld.ConceptDetail,
        pld.ConceptId,
        ISNULL(pld.TotalNumberHours, mc.QuoteValue) as TotalNumberHours,
        pld.ConceptType,
        pld.ConceptCode,
        pld.ConceptClass,
        pld.ConceptTotalValue,
        pld.EmployeerDays,
        pld.ErpDays,
        pl.TotalPaid,
        pl.DaysWorked,
        pl.LicenseDays,
        pl.DisabilityDays,
        pl.OccupationalRisksDays,
        pl.UnpaidLicenseDays,
        pl.SanctionDays,
        pl.PermissionDays,
        pl.QuoteHealthDays,
        pl.PensionEnrollmentDays,
        pl.VacationDays,
        pp.Code as CodigoPosition,
        pp.[Name] as NamePosition,
        pe.Id as EmployeeId,
        pl.RegisterStatus,
        CONCAT(pg.Code, ' ', pg.Name) AS Grupo,
        pct.FunctionalUnitId,
        fUnit.BranchOfficeId,
        pc.CurrentBalance,
        pc.Id as AgreementsCId,
        pc.Consecutive as AgreementConsecutive,
        pa.DatePayment as AgreementDatePayment,
        pl.ContractId,
		pld.AccruedValue,
		pld.DeductedValue,
		CASE 
		WHEN pg.liquidation = 1  ---Mensual
			 AND pl.PayrollDateLiquidated = EOMONTH(pl.PayrollDateLiquidated) 
			THEN DATEFROMPARTS(YEAR(pl.PayrollDateLiquidated), MONTH(pl.PayrollDateLiquidated), 1)
		WHEN pg.liquidation = 2  -- Quincenal
			 AND DAY(pl.PayrollDateLiquidated) = 15 
			THEN DATEFROMPARTS(YEAR(pl.PayrollDateLiquidated), MONTH(pl.PayrollDateLiquidated), 1)
		WHEN pg.liquidation = 2 
			 AND pl.PayrollDateLiquidated = EOMONTH(pl.PayrollDateLiquidated) 
			THEN DATEFROMPARTS(YEAR(pl.PayrollDateLiquidated), MONTH(pl.PayrollDateLiquidated), 16)
		ELSE DATEFROMPARTS(YEAR(pl.PayrollDateLiquidated), MONTH(pl.PayrollDateLiquidated), 1)
	END AS PayrollStarDate,
	pl.PayrollDateLiquidated AS PayrollEndDate,
	pl.MaternityLeaveDays,
	pet.Code AS EmployeeTypeCode

    FROM Payroll.LiquidationDetail pld WITH (NOLOCK)
    JOIN Payroll.Liquidation pl WITH (NOLOCK) ON pl.id = pld.PayrollId
    JOIN Payroll.[Contract] pct WITH (NOLOCK) ON pct.id = pl.ContractId
    JOIN Payroll.Position pp WITH (NOLOCK) ON pp.id = pct.PositionId
    JOIN Payroll.[Group] pg WITH (NOLOCK) ON pg.id = pl.GroupId
    JOIN Payroll.Concept pco WITH (NOLOCK) ON pco.Id = pld.ConceptId
    JOIN Payroll.Employee pe WITH (NOLOCK) ON pe.id = pl.EmployeeId
    JOIN Common.ThirdParty ct WITH (NOLOCK) ON ct.id = pe.ThirdPartyId
    LEFT JOIN Payroll.EmployeeType pet WITH (NOLOCK) ON pet.Id = pe.EmployeeTypeId
    LEFT JOIN Payroll.AgreementsC pc ON pc.id = pld.AgreementsId
    LEFT JOIN Payroll.AgreementsD pa ON pa.id = pld.AgreementsDId
    LEFT JOIN Payroll.FunctionalUnit fUnit WITH (NOLOCK) ON fUnit.Id = pct.FunctionalUnitId
	LEFT JOIN payroll.ManualConcepts mc ON mc.EmployeeId = pe.Id AND mc.ContractId = pct.Id AND mc.ConceptId = pco.Id
    WHERE pld.ConceptType <> 3
)

SELECT
    DISTINCT
	pb.PayrollStarDate,
	pb.PayrollEndDate,
    pb.PayrollDId,
    pb.PayrollCId,
    pb.AgreementsId,
    pb.Nit,
    pb.[Name], 
    pb.Concepto, 
    pb.NumberShares,
    pb.PayrollDateLiquidated,
    pb.GroupId,
    pb.BasicSalary,
    pb.PeriodJCB,

    --convenios
    CASE 
        WHEN pb.ConceptId = ISNULL(pb.AgreementsCId, 0) THEN 
            CONCAT(
                LEFT(pb.ConceptDetail, 
                    CASE 
                        WHEN CHARINDEX('contrato', pb.ConceptDetail) > 0 
                        THEN CHARINDEX('contrato', pb.ConceptDetail) - 1 
                        ELSE LEN(pb.ConceptDetail) 
                    END
                ), 
                ' - Convenio #', pb.AgreementConsecutive, 
                ' Cuota ', 
                (SELECT COUNT(agds.Id)
                 FROM Payroll.AgreementsD agds 
                 WHERE agds.AgreementsCId = pb.AgreementsCId
                ), 
                ' / ', 
                pb.NumberShares
            )
        ELSE 
            pb.ConceptDetail
    END AS ConceptDetail,
  0 as TotalNumberHours,
  pb.ConceptType,
  pb.ConceptCode,
    -- Cantidades de los conceptos
    CASE
		-- Horas extras y recargos (incluye mes actual + eventos del mes anterior, con cálculo inverso como fallback)
		WHEN pb.ConceptClass IN ('001', '012', '013', '042', '043', '050', '051', '052') THEN 
			CASE 
				-- Primero intentar usar TotalNumberHours si existe
				WHEN pb.TotalNumberHours IS NOT NULL AND pb.TotalNumberHours > 0 THEN pb.TotalNumberHours
				-- Luego intentar usar horas del cuadro de turnos
				WHEN ISNULL(scheduleHours.HoursNumber, 0) + ISNULL(lastMonthEventHours.HoursNumber, 0) > 0 
					THEN ISNULL(scheduleHours.HoursNumber, 0) + ISNULL(lastMonthEventHours.HoursNumber, 0)
				-- Finalmente, calcular inversamente desde el valor del concepto
				WHEN pb.ConceptTotalValue > 0 AND pb.BasicSalary > 0 THEN
					ROUND(
						pb.ConceptTotalValue / (
							(pb.BasicSalary / 220.0) *  -- Factor fijo 220 segun formulas del cliente
							CASE pb.ConceptClass
								WHEN '001' THEN 1.25   -- Hora Extra Diurna
								WHEN '012' THEN 1.75   -- Hora Extra Nocturna
								WHEN '013' THEN 2.0    -- Hora Extra Dominical/Festiva Diurna
								WHEN '050' THEN 2.5    -- Hora Extra Dominical/Festiva Nocturna
								WHEN '042' THEN 1.35   -- Recargo Nocturno (hora base + 35%)
								WHEN '043' THEN 2.15   -- Recargo Nocturno Festivo (corregido segun formula)
								WHEN '051' THEN 1.80   -- Recargo Dominical/Festivo Diurno (hora base + 80%)
								WHEN '052' THEN 2.10   -- Recargo Dominical Nocturno (hora base + 110%)
								ELSE 1.0
							END
						), 2
					)
				ELSE 0
			END
        WHEN pb.ConceptClass IN ('005', '006', '066') THEN pb.DaysWorked - ISNULL(pb.LicenseDays, 0)
        WHEN pb.ConceptClass IN ('021', '022') THEN ISNULL(pb.DisabilityDays, 60)
        WHEN pb.ConceptClass IN ('027') THEN ISNULL(pb.OccupationalRisksDays, 0)
		WHEN pb.ConceptClass IN ('023') THEN ISNULL(NULLIF(pb.MaternityLeaveDays, 0), ml.LeaveDays) --Maternidad
        WHEN pb.ConceptClass IN ('023', '024') THEN  -- Licencias
			CASE 
				WHEN ISNULL(pb.LicenseDays, 0) = 0 THEN 
					CASE 
						WHEN pb.Concepto = '013' THEN pl.LeaveDays -- Paternidad
						WHEN pb.Concepto = '011' THEN prlOld.LeaveDays -- riesgo profesional 
					END
				ELSE pb.LicenseDays
			END
        WHEN pb.ConceptClass = '025' THEN ISNULL(pb.UnpaidLicenseDays, 0)
        WHEN pb.ConceptClass = '026' THEN ISNULL(pb.SanctionDays, 0)
        WHEN pb.ConceptClass = '028' THEN pb.PermissionDays
        WHEN pb.ConceptClass = '017' THEN ISNULL(pb.QuoteHealthDays, 0)
        WHEN pb.ConceptClass = '014' THEN ISNULL(pb.PensionEnrollmentDays, 0)
        WHEN pb.ConceptClass = '030' THEN CASE 
             WHEN ISNULL(pb.VacationDays, 0) = 0 
                  THEN ISNULL(vac.TakenDays, 0) 
             ELSE pb.VacationDays 
         END
		-- Incapacidad Ambulatoria Patrono con fallback a tabla Novelty
        WHEN pb.ConceptClass = '067' THEN ISNULL(NULLIF(pb.EmployeerDays, 0), ambulatoryDays.AmbulatoryEmployerDays)
		-- Incapacidad Hospitalaria Patrono
        WHEN pb.ConceptClass = '069' THEN ISNULL(pb.EmployeerDays, 0)
		-- Incapacidad Ambulatoria/Hospitalaria ERP
        WHEN pb.ConceptClass IN ('068', '070') THEN ISNULL(pb.ErpDays, 0)
		WHEN pb.ConceptClass = '071' THEN lc.LeaveDays --Calamidades
        WHEN pb.ConceptClass = '072' THEN lr.LeaveDays --Licencias Remuneradas
		-- Vacaciones en Dinero con fallback y calculo inverso
        WHEN pb.ConceptClass = '073' THEN 
			COALESCE(
				NULLIF(vdc.EnjoyDays, 0), 
				NULLIF(vacCash.VacationDays, 0),
				-- Calculo inverso: Dias = Valor * 30 / Salario
				CASE WHEN pb.ConceptTotalValue > 0 AND pb.BasicSalary > 0 
					 THEN ROUND(pb.ConceptTotalValue * 30.0 / pb.BasicSalary, 2)
					 ELSE 0
				END
			)
		WHEN pb.ConceptClass = '074' THEN ld.LeaveDays --Luto
		WHEN pb.ConceptClass = '075' THEN prl.LeaveDays --Riesgo profesional patrono
		WHEN pb.ConceptClass = '076' THEN pArl.LeaveDays --Riesgo profesional erp
        ELSE 0
   END as Quantity,
    pb.AccruedValue,
	pb.DeductedValue,
    pb.ConceptTotalValue,
    pb.TotalPaid,
    pb.DaysWorked,
    pb.CodigoPosition,
    pb.NamePosition,
    pb.EmployeeId,
    pb.RegisterStatus,
    pb.Grupo,
    COALESCE(
        (SELECT COUNT(agds.Id)
         FROM Payroll.AgreementsD agds 
         JOIN Payroll.AgreementsC agcs ON agcs.Id = agds.AgreementsCId
         WHERE agcs.Id = pb.AgreementsCId 
           AND agds.DatePayment <= pb.AgreementDatePayment 
           AND agcs.NumberShares = 0),
        (SELECT 
            COUNT(CASE WHEN (agcs.AgreementValue / agcs.NumberShares) = agds.ShareValuePaid THEN 1 END) +
            COUNT(CASE WHEN ((agcs.AgreementValue / agcs.NumberShares) * 2) = agds.ShareValuePaid THEN 2 END)
         FROM Payroll.AgreementsD agds 
         JOIN Payroll.AgreementsC agcs ON agcs.Id = agds.AgreementsCId
         WHERE agcs.Id = pb.AgreementsCId 
           AND agds.DatePayment <= pb.AgreementDatePayment 
           AND agcs.NumberShares > 0),
        0
    ) AS CountAgree,

    pb.FunctionalUnitId,
    pb.BranchOfficeId,
    pb.CurrentBalance,
	vac.VacationStartDate,
	vac.VacationEndDate,
	vac.TakenDays

FROM PayrollBase pb
-- Calculacion horas de cuadro de turno
OUTER APPLY (
    SELECT 
        SUM(sdh.TotalNumberHours) AS HoursNumber
    FROM Payroll.ScheduleDetail sd
    JOIN Payroll.ScheduleDetailHour sdh ON sdh.ScheduleDetailId = sd.Id
    JOIN Payroll.ScheduleDetailConcept sdc ON sdc.ScheduleDetailHourId = sdh.Id
    JOIN Payroll.Concept c ON c.Id = sdc.ConceptId
    WHERE 
        c.ConceptClass <> '005'
        AND sd.EmployeeId = pb.EmployeeId
        AND sdc.ConceptId = pb.ConceptId
        AND sd.ContractId = pb.ContractId
        AND sd.DateDetail BETWEEN 
            DATEFROMPARTS(YEAR(pb.PayrollDateLiquidated), MONTH(pb.PayrollDateLiquidated), 1) 
            AND EOMONTH(pb.PayrollDateLiquidated)
        AND (
            (sdh.AppliedLiquidationConcept = 1 AND sdc.ConceptType = 1)
            OR
            (sdh.AppliedLiquidationConcept = 0 AND sdc.ConceptType = 0)
        )
) scheduleHours 
-- Horas de eventos del mes anterior (para conceptos 040, 026 - horas extras festivas pendientes)
OUTER APPLY (
    SELECT 
        SUM(sdh.TotalNumberHours) AS HoursNumber
    FROM Payroll.ScheduleDetail sd
    JOIN Payroll.ScheduleDetailHour sdh ON sdh.ScheduleDetailId = sd.Id
    JOIN Payroll.ScheduleDetailConcept sdc ON sdc.ScheduleDetailHourId = sdh.Id
    WHERE sd.EmployeeId = pb.EmployeeId
      AND sd.ContractId = pb.ContractId
      AND sdc.ConceptId = pb.ConceptId
      AND sdh.EventLastMonth = 1
      AND sd.DateDetail BETWEEN 
          DATEADD(MONTH, -1, DATEFROMPARTS(YEAR(pb.PayrollDateLiquidated), MONTH(pb.PayrollDateLiquidated), 1))
          AND EOMONTH(DATEADD(MONTH, -1, pb.PayrollDateLiquidated))
) lastMonthEventHours
LEFT JOIN VacationDaysInCash vdc ON vdc.EmployeeId = pb.EmployeeId
LEFT JOIN VacationsByConceptClass vac ON vac.PayrollId = pb.PayrollCId AND pb.ConceptClass = '030'
CROSS APPLY (  --- LICENCIAS REMUNERADA
    SELECT SUM(n.Days) AS LeaveDays
    FROM Payroll.Novelty n
    WHERE n.EmployeeId = pb.EmployeeId
      AND n.TypeNovelty = 3
      AND n.LicenseClass = 1
      AND (
            (n.RealDate >= pb.PayrollStarDate AND n.EndDate <= pb.PayrollEndDate) -- inician y terminan en el periodo
         OR (n.RealDate < pb.PayrollStarDate AND n.EndDate <= pb.PayrollEndDate ) -- inician antes, terminan en el periodo y son nuevas
      )
) lr
CROSS APPLY ( --CALAMIDAD
    SELECT 
        SUM(x.DaysEffective) AS LeaveDays
    FROM (
        SELECT 
            -- Calcula los días efectivos dentro del período de liquidación
            DATEDIFF(
                DAY,
                CASE 
                    WHEN n.RealDate < pb.PayrollStarDate THEN pb.PayrollStarDate 
                    ELSE n.RealDate 
                END,
                CASE 
                    WHEN n.EndDate > pb.PayrollEndDate THEN pb.PayrollEndDate 
                    ELSE n.EndDate 
                END
            ) + 1 AS DaysEffective
        FROM Payroll.Novelty n
        WHERE n.EmployeeId = pb.EmployeeId
          AND n.TypeNovelty = 3
          AND n.LicenseClass = 5
          -- Solo novedades que se intersectan con el período de liquidación
          AND n.RealDate <= pb.PayrollEndDate
          AND n.EndDate >= pb.PayrollStarDate
    ) x
    WHERE x.DaysEffective > 0  -- Solo días efectivos positivos
) lc

CROSS APPLY (  --- LUTO
    SELECT SUM(n.Days) AS LeaveDays
    FROM Payroll.Novelty n
    WHERE n.EmployeeId = pb.EmployeeId
      AND n.TypeNovelty = 3
      AND n.LicenseClass = 6
      AND (
            (n.RealDate >= pb.PayrollStarDate AND n.EndDate <= pb.PayrollEndDate and status =  IIF(pb.RegisterStatus <> '', 1, 0)) -- inician y terminan en el periodo
          OR (n.RealDate < pb.PayrollStarDate AND n.EndDate <= pb.PayrollEndDate and status =  IIF(pb.RegisterStatus <> '', 1, 0) ) -- inician antes, terminan en el periodo y son nuevas
		  --OR (n.RealDate >= pb.PayrollStarDate AND n.EndDate > pb.PayrollEndDate and status =  IIF(pb.RegisterStatus <> '', 1, 0)) -- inician en este, terminan en el siguiente
	  )
) ld


CROSS APPLY (
    SELECT SUM(x.DaysEffective) AS LeaveDays
    FROM (
        SELECT 
            DATEDIFF(
                DAY,
                CASE WHEN n.RealDate < pb.PayrollStarDate THEN pb.PayrollStarDate ELSE n.RealDate END,
                CASE WHEN n.EndDate > pb.PayrollEndDate THEN pb.PayrollEndDate ELSE n.EndDate END
            ) AS DaysEffective
        FROM Payroll.Novelty n
        WHERE n.EmployeeId = pb.EmployeeId
          AND n.TypeNovelty = 1
          AND n.InabilityClass = 3
          AND (
                (n.RealDate >= pb.PayrollStarDate AND n.EndDate <= pb.PayrollEndDate) -- inician y terminan en el periodo
             OR (n.RealDate <  pb.PayrollStarDate AND n.EndDate <= pb.PayrollEndDate) -- inician antes, terminan en el periodo
             OR (n.RealDate >= pb.PayrollStarDate AND n.EndDate > pb.PayrollEndDate) -- inician en este, terminan en el siguiente
          )
    ) x
) ml

CROSS APPLY ( --PATERNIDAD
    SELECT SUM(n.Days) AS LeaveDays
    FROM Payroll.Novelty n
    WHERE n.EmployeeId = pb.EmployeeId
      AND n.TypeNovelty = 1
      AND n.LicenseClass = 5
      AND (
            (n.RealDate >= pb.PayrollStarDate AND n.EndDate <= pb.PayrollEndDate) -- inician y terminan en el periodo
         OR (n.RealDate < pb.PayrollStarDate AND n.EndDate <= pb.PayrollEndDate ) -- inician antes, terminan en el periodo y son nuevas
      )
) pl

CROSS APPLY ( --PROFESIONAL OLD
    SELECT SUM(prl.DaysInPeriod) AS LeaveDays
    FROM (
        SELECT 
            DaysInPeriod = 
                DATEDIFF(
                    DAY,
                    CASE 
                        WHEN n.RealDate < pb.PayrollStarDate THEN CAST(pb.PayrollStarDate AS DATE)
                        ELSE n.RealDate
                    END,
                    CASE 
                        WHEN n.EndDate > pb.PayrollEndDate THEN CAST(pb.PayrollEndDate AS DATE)
                        ELSE n.EndDate
                    END
                ) + 1
        FROM Payroll.Novelty n
        WHERE n.EmployeeId     = pb.EmployeeId
          AND n.TypeNovelty    = 1
          AND n.InabilityClass = 4
          AND n.RealDate <= pb.PayrollEndDate
          AND n.EndDate >= pb.PayrollStarDate
    ) prl
) prlOld

CROSS APPLY ( ----RIESGO PROFESIONAL PATRONO 
    SELECT SUM(n.EmployerDays) AS LeaveDays
    FROM Payroll.Novelty n
    WHERE n.EmployeeId = pb.EmployeeId
      AND n.TypeNovelty = 1
      AND n.InabilityClass = 4
      AND (
            (n.RealDate >= pb.PayrollStarDate AND n.EndDate <= pb.PayrollEndDate) -- inician y terminan en el periodo
         OR (n.RealDate < pb.PayrollStarDate AND n.EndDate <= pb.PayrollEndDate and n.Status = 0 ) -- inician antes, terminan en el periodo y son nuevas
      )
) prl


CROSS APPLY ( ----RIESGO PROFESIONAL ERP 
    SELECT 
        SUM(x.LeaveDays) AS LeaveDays
    FROM (
        SELECT 
            CASE 
                WHEN n.RealDate < pb.PayrollStarDate
                     AND n.EndDate >= pb.PayrollStarDate
                THEN DATEDIFF(DAY, pb.PayrollStarDate, n.EndDate) + 1
                ELSE n.EPSDays
            END AS LeaveDays
        FROM Payroll.Novelty n
        WHERE n.EmployeeId = pb.EmployeeId
          AND n.TypeNovelty = 1
          AND n.InabilityClass = 4
          AND (
                (n.RealDate >= pb.PayrollStarDate AND n.EndDate <= pb.PayrollEndDate)-- inician y terminan en el periodo
             OR (n.RealDate < pb.PayrollStarDate AND n.EndDate >= pb.PayrollStarDate AND n.Status = 2) -- inician antes, terminan en el periodo y estan parcialmente liquidadas
          )
    ) x
) pArl

CROSS APPLY ( -- INCAPACIDAD AMBULATORIA PATRONO (concepto 067)
    SELECT ISNULL(SUM(n.EmployerDays), 0) AS AmbulatoryEmployerDays
    FROM Payroll.Novelty n
    WHERE n.EmployeeId = pb.EmployeeId
      AND n.TypeNovelty = 1
      AND n.InabilityClass = 1
      AND n.RealDate <= pb.PayrollEndDate
      AND n.EndDate >= pb.PayrollStarDate
) ambulatoryDays

OUTER APPLY ( -- VACACIONES EN DINERO (concepto 035)
    SELECT TOP 1 v.EnjoyDays AS VacationDays
    FROM Payroll.VacationPeriod vp
    JOIN Payroll.Vacation v ON v.VacationPeriodId = vp.Id
    WHERE vp.EmployeeId = pb.EmployeeId
      AND v.TypeVacation = 1
      AND v.State = 1
      AND v.VacationStartDate <= pb.PayrollEndDate
    ORDER BY v.VacationStartDate DESC
) vacCash