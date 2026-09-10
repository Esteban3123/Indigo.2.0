

/****** Object:  View [Payroll].[ViewReportPayStub]    Script Date: 22/05/2025 9:42:30 a.m. ******/
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
	pld.Quantity
    FROM Payroll.LiquidationDetail pld
    JOIN Payroll.Liquidation pl ON pl.id = pld.PayrollId
    JOIN Payroll.[Contract] pct ON pct.id = pl.ContractId
    JOIN Payroll.Position pp ON pp.id = pct.PositionId
    JOIN Payroll.[Group] pg ON pg.id = pl.GroupId
    JOIN Payroll.Concept pco ON pco.Id = pld.ConceptId
    JOIN Payroll.Employee pe ON pe.id = pl.EmployeeId
    JOIN Common.ThirdParty ct ON ct.id = pe.ThirdPartyId
    LEFT JOIN Payroll.EmployeeType pet ON pet.Id = pe.EmployeeTypeId
    LEFT JOIN Payroll.AgreementsC pc ON pc.id = pld.AgreementsId
    LEFT JOIN Payroll.AgreementsD pa ON pa.id = pld.AgreementsDId
    LEFT JOIN Payroll.FunctionalUnit fUnit ON fUnit.Id = pct.FunctionalUnitId
    OUTER APPLY (
        SELECT TOP (1)
            manualConcept.QuoteValue
        FROM Payroll.ManualConcepts manualConcept
        WHERE manualConcept.EmployeeId = pe.Id
          AND manualConcept.ContractId = pct.Id
          AND manualConcept.ConceptId = pco.Id
          AND pl.PayrollDateLiquidated BETWEEN manualConcept.PayrollInitialDate
                                           AND manualConcept.PayrollEndingDate
        ORDER BY manualConcept.Id DESC
    ) mc
    WHERE pld.ConceptType <> 3
)

SELECT
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

  case WHEN pb.quantity = 0   THEN 
  -- Cantidades de los conceptos
    CASE
		WHEN pb.ConceptClass IN ('001', '012', '013','051','042','043','050','043') THEN ISNULL(pb.TotalNumberHours,scheduleHours.HoursNumber)
        WHEN pb.ConceptClass IN ('005', '006') THEN pb.DaysWorked - ISNULL(pb.LicenseDays, 0)
        WHEN pb.ConceptClass IN ('021', '022') THEN ISNULL(pb.DisabilityDays, 60)
        WHEN pb.ConceptClass IN ('027') THEN ISNULL(pb.OccupationalRisksDays, 0)
		WHEN pb.ConceptClass IN ('023') THEN  ISNULL(NULLIF(pb.MaternityLeaveDays, 0), ml.LeaveDays) --Maternidad
        WHEN pb.ConceptClass IN ('023', '024') THEN  -- Licencias
			CASE 
				WHEN ISNULL(pb.LicenseDays,0) = 0 THEN 
					CASE 
						WHEN pb.Concepto = '013' -- Paternidad
							THEN pl.LeaveDays
						WHEN pb.Concepto = '011' -- riesgo profesional 
							THEN prlOld.LeaveDays
					END
				ELSE pb.LicenseDays
			END
        WHEN pb.ConceptClass = '025' THEN ISNULL(pb.UnpaidLicenseDays, 0)
        WHEN pb.ConceptClass = '026' THEN ISNULL(pb.SanctionDays, 0)
        WHEN pb.ConceptClass = '028' THEN pb.PermissionDays
        WHEN pb.ConceptClass = '017' THEN ISNULL(pb.QuoteHealthDays, 0)
        WHEN pb.ConceptClass = '014' THEN ISNULL(pb.PensionEnrollmentDays, 0)
        WHEN pb.ConceptClass = '030' THEN  CASE 
             WHEN ISNULL(pb.VacationDays, 0) = 0 
                  THEN ISNULL(vac.TakenDays, 0) 
             ELSE pb.VacationDays 
         END
        WHEN pb.ConceptClass IN ('067', '069') THEN ISNULL(pb.EmployeerDays, 0)
        WHEN pb.ConceptClass IN ('068', '070') THEN ISNULL(pb.ErpDays, 0)
		WHEN pb.ConceptClass ='071' THEN lc.LeaveDays --Calamidades
        WHEN pb.ConceptClass IN  ('072') THEN lr.LeaveDays --Licencias
        WHEN pb.ConceptClass = '073' THEN vdc.EnjoyDays--Vacaciones
		WHEN pb.ConceptClass = '074' THEN ld.LeaveDays --Luto
		WHEN pb.ConceptClass IN ('075')  THEN prl.LeaveDays --riesgo profesional  patrono
		WHEN pb.ConceptClass IN ('076')  THEN pArl.LeaveDays --riesgo profesional  erp
        ELSE 0
		end 
  else  pb.Quantity 
  end as Quantity,
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
    CASE
        WHEN ISNULL(pb.AgreementsCId, 0) = 0 THEN 0
        ELSE (
            SELECT COUNT(agds.Id)
            FROM Payroll.AgreementsD agds
            WHERE agds.AgreementsCId = pb.AgreementsCId
              AND agds.DatePayment <= pb.PayrollDateLiquidated
        )
    END AS CountAgree,

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
LEFT JOIN VacationDaysInCash vdc ON vdc.EmployeeId = pb.EmployeeId
OUTER APPLY (
    SELECT TOP (1)
        vacation.VacationStartDate,
        vacation.VacationEndDate,
        vacation.TakenDays,
        vacation.EnjoyDays
    FROM Payroll.VacationPeriod vacationPeriod
    JOIN Payroll.Vacation vacation ON vacation.VacationPeriodId = vacationPeriod.Id
    WHERE vacationPeriod.EmployeeId = pb.EmployeeId
      AND pb.ConceptClass = '030'
    ORDER BY vacation.VacationStartDate DESC, vacation.Id DESC
) vac
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
CROSS APPLY (  --- CALAMIDADES
    SELECT SUM(n.Days) AS LeaveDays
    FROM Payroll.Novelty n
    WHERE n.EmployeeId = pb.EmployeeId
      AND n.TypeNovelty = 3
      AND n.LicenseClass = 5
      AND (
            (n.RealDate >= pb.PayrollStarDate AND n.EndDate <= pb.PayrollEndDate) -- inician y terminan en el periodo
         OR (n.RealDate < pb.PayrollStarDate AND n.EndDate <= pb.PayrollEndDate ) -- inician antes, terminan en el periodo y son nuevas
      )
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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de reporte de colilla de pago (desprendible de nómina) por empleado y período de liquidación. Integra el detalle de conceptos liquidados (devengados y deducciones), información del contrato, cargo y grupo de nómina, así como los días de vacaciones disfrutados y en dinero más reciente del empleado. Calcula automáticamente la fecha de inicio del período según si la nómina es mensual o quincenal, y enriquece los conceptos de convenios de libranza con el número de cuota y saldo. Sirve para generar el desprendible o colilla de pago que se entrega al empleado mostrando todo lo que ganó y le descontaron en el período, así como para consultas de auditoría y reportes de nómina por empresa (NIT), grupo, cargo y unidad funcional.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportPayStub';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportPayStub';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Construye el dataset para el reporte de colilla/desprendible de pago por empleado, consolidando conceptos liquidados, cantidades por clase de concepto (horas, días de licencias, incapacidades, vacaciones), información de convenios y rango del período liquidado.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportPayStub';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros en Payroll.LiquidationDetail con ConceptType distinto de 3 (se excluyen aportes/conceptos tipo 3); Cada liquidación tiene contrato, cargo, grupo, concepto, empleado y tercero asociados (joins obligatorios); Para vacaciones en dinero, debe existir Payroll.Vacation con TypeVacation=1 y State=1 vinculada al empleado vía VacationPeriod; El grupo (Payroll.Group) tiene parametrizado el tipo de liquidación (1=Mensual, 2=Quincenal) para derivar la fecha de inicio del período', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportPayStub';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.ViewReportPayStub: Devuelve una fila distinta por detalle de liquidación (PayrollDId) con conceptos, cantidades calculadas y datos del convenio asociado, excluyendo conceptos con ConceptType = 3', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportPayStub';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si pg.liquidation = 1 (mensual) y PayrollDateLiquidated = fin de mes → PayrollStarDate = primer día del mes de la fecha liquidada else Se evalúan reglas quincenales o se asume primer día del mes; si pg.liquidation = 2 (quincenal) y DAY(PayrollDateLiquidated)=15 → PayrollStarDate = día 1 del mes (primera quincena); si pg.liquidation = 2 y PayrollDateLiquidated = fin de mes → PayrollStarDate = día 16 del mes (segunda quincena); si pb.ConceptId = ISNULL(pb.AgreementsCId,0) (el concepto corresponde a un convenio/libranza) → ConceptDetail se reescribe concatenando ''- Convenio #<consecutivo> Cuota <n> / <NumberShares>'', truncando el detalle original antes de la palabra ''contrato'' else Se conserva el ConceptDetail original; si pb.Quantity = 0 → Se calcula la cantidad según la ConceptClass (horas de cuadro de turno, días trabajados, incapacidades, licencias, vacaciones, sanciones, permisos, maternidad, paternidad, riesgo profesional, etc.) else Se conserva pb.Quantity tal cual; si ConceptClass IN (''001'',''012'',''013'',''051'',''042'',''043'',''050'') → Cantidad = TotalNumberHours; si es nulo, suma de horas del cuadro de turno (ScheduleDetailHour); si ConceptClass IN (''005'',''006'') → Cantidad = DaysWorked - LicenseDays; si ConceptClass IN (''021'',''022'') → Cantidad = DisabilityDays (default 60 si nulo); si ConceptClass = ''023'' (Maternidad) → Cantidad = MaternityLeaveDays; si es 0/nulo, días efectivos de novedad TypeNovelty=1, InabilityClass=3 dentro del período; si ConceptClass IN (''023'',''024'') con LicenseDays=0 y Concepto=''013'' → Cantidad = días de paternidad (Novelty TypeNovelty=1, LicenseClass=5); si ConceptClass IN (''023'',''024'') con LicenseDays=0 y Concepto=''011'' → Cantidad = días de riesgo profesional (Novelty TypeNovelty=1, InabilityClass=4) calculados con prlOld; si ConceptClass = ''030'' y VacationDays=0 → Cantidad = TakenDays del período de vacaciones más reciente con ConceptClass=''030'' (rowN=1) else Cantidad = pb.VacationDays; si ConceptClass = ''071'' (Calamidades) → Cantidad = suma de días de Novelty TypeNovelty=3, LicenseClass=5 dentro del período; si ConceptClass = ''072'' (Licencias remuneradas) → Cantidad = suma de días de Novelty TypeNovelty=3, LicenseClass=1 dentro del período; si ConceptClass = ''073'' (Vacaciones en dinero) → Cantidad = EnjoyDays de la vacación más reciente con TypeVacation=1 y State=1; si ConceptClass = ''074'' (Luto) → Cantidad = suma de días de Novelty TypeNovelty=3, LicenseClass=6 dentro del período; si ConceptClass = ''075'' (Riesgo profesional patrono) → Cantidad = suma de EmployerDays de Novelty TypeNovelty=1, InabilityClass=4; si ConceptClass = ''076'' (Riesgo profesional ERP/EPS) → Cantidad = EPSDays o días recortados al período de Novelty TypeNovelty=1, InabilityClass=4 con Status=2 cuando inicia antes del período; si el detalle pertenece a un convenio → CountAgree = número de registros de pago (AgreementsD) del convenio cuya DatePayment no supera PayrollDateLiquidated; si no pertenece a un convenio → CountAgree = 0', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportPayStub';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportPayStub';
GO
