


/****** Object:  View [Payroll].[ViewReportPayStub]    Script Date: 22/05/2025 9:42:30 a.�m. ******/
CREATE VIEW [Payroll].[ViewReportPayStub]
AS

WITH NoveltyCalamity AS (   -- Calamidades
    SELECT 
        Days,
        EmployeeId
    FROM Payroll.Novelty
    WHERE TypeNovelty = 3 
      AND LicenseClass = 5
),
PaidLeave AS (              --Licencia remunerada
    SELECT 
        Days,
        EmployeeId
    FROM Payroll.Novelty
    WHERE TypeNovelty = 3 
      AND LicenseClass = 1
),
VacationDaysInCash AS (     ---Vacaciones en dinero
    SELECT 
        v.EnjoyDays,
        vp.EmployeeId
    FROM Payroll.VacationPeriod vp
    JOIN Payroll.Vacation v ON v.VacationPeriodId = vp.Id
    WHERE v.TypeVacation = 1
),
VacationsByConceptClass AS ( --Todos los conceptos con la clase vacaciones
	SELECT *
	FROM (
		SELECT
			l.Id AS PayrollId,
			v.VacationStartDate,
			v.VacationEndDate,
			v.TakenDays,
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
        pld.TotalNumberHours,
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
		pld.DeductedValue
    FROM Payroll.LiquidationDetail pld WITH (NOLOCK)
    JOIN Payroll.Liquidation pl WITH (NOLOCK) ON pl.id = pld.PayrollId
    JOIN Payroll.[Contract] pct WITH (NOLOCK) ON pct.id = pl.ContractId
    JOIN Payroll.Position pp WITH (NOLOCK) ON pp.id = pct.PositionId
    JOIN Payroll.[Group] pg WITH (NOLOCK) ON pg.id = pl.GroupId
    JOIN Payroll.Concept pco WITH (NOLOCK) ON pco.Id = pld.ConceptId
    JOIN Payroll.Employee pe WITH (NOLOCK) ON pe.id = pl.EmployeeId
    JOIN Common.ThirdParty ct WITH (NOLOCK) ON ct.id = pe.ThirdPartyId
    LEFT JOIN Payroll.AgreementsC pc ON pc.id = pld.AgreementsId
    LEFT JOIN Payroll.AgreementsD pa ON pa.id = pld.AgreementsDId
    LEFT JOIN Payroll.FunctionalUnit fUnit WITH (NOLOCK) ON fUnit.Id = pct.FunctionalUnitId
    WHERE pld.ConceptType <> 3
)

SELECT
    DISTINCT
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
    
    pb.TotalNumberHours,
    pb.ConceptType,
    pb.ConceptCode,
    
    -- Cantidades de los conceptos
    CASE
        WHEN scheduleHours.HoursNumber IS NOT NULL THEN scheduleHours.HoursNumber
        WHEN pb.ConceptClass IN ('005', '006') THEN pb.DaysWorked - ISNULL(pb.LicenseDays, 0)
        WHEN pb.ConceptClass IN ('021', '022') THEN ISNULL(pb.DisabilityDays, 0)
        WHEN pb.ConceptClass IN ('027') THEN ISNULL(pb.OccupationalRisksDays, 0)
        WHEN pb.ConceptClass IN ('023', '024') THEN ISNULL(pb.LicenseDays, 0)
        WHEN pb.ConceptClass = '025' THEN ISNULL(pb.UnpaidLicenseDays, 0)
        WHEN pb.ConceptClass = '026' THEN ISNULL(pb.SanctionDays, 0)
        WHEN pb.ConceptClass = '028' THEN pb.PermissionDays
        WHEN pb.ConceptClass = '017' THEN ISNULL(pb.QuoteHealthDays, 0)
        WHEN pb.ConceptClass = '014' THEN ISNULL(pb.PensionEnrollmentDays, 0)
        WHEN pb.ConceptClass = '030' THEN ISNULL(pb.VacationDays, 0)
        WHEN pb.ConceptClass IN ('067', '069') THEN ISNULL(pb.EmployeerDays, 0)
        WHEN pb.ConceptClass IN ('068', '070') THEN ISNULL(pb.ErpDays, 0)
        WHEN pb.ConceptClass IN ('071', '072') THEN pal.Days
        WHEN pb.ConceptClass = '073' THEN vdc.EnjoyDays
        ELSE 0
    END AS Quantity,
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
LEFT JOIN NoveltyCalamity nc ON nc.EmployeeId = pb.EmployeeId
LEFT JOIN PaidLeave pal ON pal.EmployeeId = pb.EmployeeId
LEFT JOIN VacationDaysInCash vdc ON vdc.EmployeeId = pb.EmployeeId
LEFT JOIN VacationsByConceptClass vac ON vac.PayrollId = pb.PayrollCId AND pb.ConceptClass = '030'