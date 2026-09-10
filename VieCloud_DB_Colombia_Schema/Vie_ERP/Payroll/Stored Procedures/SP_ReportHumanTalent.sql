-- =============================================
-- Author:		Mariana gonzalez
-- Create date: 2025-12-12
-- Modified: Oscar stiven Astudillo
-- Modified date: 2026-03-23
-- Description:	Lista una fila por cada version de contrato relevante en el mes.
-- =============================================

CREATE PROCEDURE [Payroll].[SP_ReportHumanTalent]
@InitialDate AS DATE,
@FinalDate   AS DATE,
@EmployeeId  AS INT = NULL
AS
BEGIN
SET NOCOUNT ON

BEGIN TRY

;WITH
-- 1. Liquidaciones consolidadas: una por (empleado, contrato, mes)
LiqBase AS (
    SELECT
        EmployeeId,
        ContractId,
        MAX(Id)                    AS Id,
        MAX(PayrollDateLiquidated) AS PayrollDateLiquidated
    FROM Payroll.Liquidation
    WHERE PayrollDateLiquidated BETWEEN @InitialDate AND @FinalDate
      AND (@EmployeeId IS NULL OR EmployeeId = @EmployeeId)
    GROUP BY EmployeeId, ContractId,
             YEAR(PayrollDateLiquidated), MONTH(PayrollDateLiquidated)
),
-- 2. Enriquecer cada liquidacion con los atributos visibles del contrato usado
LiqWithContract AS (
    SELECT
        lb.EmployeeId,
        lb.ContractId,
        lb.PayrollDateLiquidated,
        lc.ModificationDate  AS LiqContractModDate,
        lc.BasicSalary       AS LiqBasicSalary,
        lc.PositionId        AS LiqPositionId,
        lc.FunctionalUnitId  AS LiqFunctionalUnitId
    FROM LiqBase lb
    INNER JOIN Payroll.Contract lc ON lc.Id = lb.ContractId
),
-- 3. Predecesor inmediato por liquidacion (calculado una sola vez, no por fila)
PredecessorMax AS (
    SELECT
        lw.EmployeeId,
        lw.ContractId           AS LiqContractId,
        lw.PayrollDateLiquidated,
        lw.LiqBasicSalary,
        lw.LiqPositionId,
        lw.LiqFunctionalUnitId,
        MAX(c2.Id)              AS PredecessorId
    FROM LiqWithContract lw
    INNER JOIN Payroll.Contract c2
        ON  c2.EmployeeId = lw.EmployeeId
        AND c2.Status     = 4
        AND c2.Id         < lw.ContractId
	AND YEAR(c2.ModificationDate)  = YEAR(lw.PayrollDateLiquidated)
    AND MONTH(c2.ModificationDate) = MONTH(lw.PayrollDateLiquidated)

    -- Solo si el contrato de la liquidacion arranco en el mismo mes reportado
    WHERE YEAR(lw.LiqContractModDate)  = YEAR(lw.PayrollDateLiquidated)
      AND MONTH(lw.LiqContractModDate) = MONTH(lw.PayrollDateLiquidated)
    GROUP BY lw.EmployeeId, lw.ContractId, lw.PayrollDateLiquidated,
             lw.LiqBasicSalary, lw.LiqPositionId, lw.LiqFunctionalUnitId
),

RetiredInPeriod AS (
    SELECT DISTINCT
        c.EmployeeId,
        c.Id AS ContractId,
        CAST(EOMONTH(cl.RetirementDate) AS DATE) AS PayrollDateLiquidated
    FROM Payroll.ContractLiquidation cl
    INNER JOIN Payroll.Contract c ON c.Id = cl.ContractId
    WHERE cl.RetirementDate BETWEEN @InitialDate AND @FinalDate
      AND (@EmployeeId IS NULL OR c.EmployeeId = @EmployeeId)
      AND NOT EXISTS (
          SELECT 1 FROM Payroll.Liquidation l
          WHERE l.EmployeeId = c.EmployeeId
            AND l.ContractId = c.Id
            AND YEAR(l.PayrollDateLiquidated)  = YEAR(cl.RetirementDate)
            AND MONTH(l.PayrollDateLiquidated) = MONTH(cl.RetirementDate)
      )
),

-- 4. Contratos a mostrar: el de la liquidacion UNION el predecesor elegible
--    Reemplaza el INNER JOIN con OR + subconsultas correlacionadas
ContractsToShow AS (
    -- Siempre: el contrato con el que se liquido
    SELECT EmployeeId, ContractId, PayrollDateLiquidated
    FROM LiqWithContract

    UNION ALL

    -- Adicional: predecesor, solo si algo visible cambio y no tiene liquidacion propia ese mes
    SELECT pm.EmployeeId, pm.PredecessorId, pm.PayrollDateLiquidated
    FROM PredecessorMax pm
    INNER JOIN Payroll.Contract pred ON pred.Id = pm.PredecessorId
    WHERE (
        pred.BasicSalary      <> pm.LiqBasicSalary
        OR pred.PositionId    <> pm.LiqPositionId
        OR pred.FunctionalUnitId <> pm.LiqFunctionalUnitId
    )
    AND NOT EXISTS (
        SELECT 1
        FROM Payroll.Liquidation liq2
        WHERE liq2.EmployeeId = pm.EmployeeId
          AND liq2.ContractId = pm.PredecessorId
          AND YEAR(liq2.PayrollDateLiquidated)  = YEAR(pm.PayrollDateLiquidated)
          AND MONTH(liq2.PayrollDateLiquidated) = MONTH(pm.PayrollDateLiquidated)
    )

	UNION ALL
SELECT EmployeeId, ContractId, PayrollDateLiquidated FROM RetiredInPeriod

),
-- 5. Fecha de retiro efectiva por contrato (evita evaluar COALESCE tres veces en el WHERE)
ContractRetirement AS (
    SELECT
        c.Id AS ContractId,
        COALESCE(
            cl.RetirementDate,
            c.RetirementDate,
            CASE WHEN c.Status = 2 THEN c.ContractEndingDate ELSE NULL END
        ) AS EffectiveRetirementDate
    FROM Payroll.Contract c
    LEFT JOIN Payroll.ContractLiquidation cl ON cl.ContractId = c.Id
    WHERE c.Status <> 3
)

SELECT EmployeeId, PayrollDate, DocumentType, Nit, EmployeeName, ContractStatus,
       FunctionalUnitCode, FunctionalUnitName, PositionCode, PositionName, BasicSalary,
       ContractStartDate, ContractEndDate, BirthDate, Age, Gender, NationalityName,
       MaritalStatus, PaymentMethod, BankName, BankAccountNumber, EmailAddress,
       TotalPendingVacationDays
FROM (
    SELECT DISTINCT
        e.Id AS EmployeeId,
        YEAR(cts.PayrollDateLiquidated)  AS _SortYear,
        MONTH(cts.PayrollDateLiquidated) AS _SortMonth,
        c.BasicSalary                    AS _SortSalary,
        CASE MONTH(cts.PayrollDateLiquidated)
            WHEN 1  THEN 'Enero'
            WHEN 2  THEN 'Febrero'
            WHEN 3  THEN 'Marzo'
            WHEN 4  THEN 'Abril'
            WHEN 5  THEN 'Mayo'
            WHEN 6  THEN 'Junio'
            WHEN 7  THEN 'Julio'
            WHEN 8  THEN 'Agosto'
            WHEN 9  THEN 'Septiembre'
            WHEN 10 THEN 'Octubre'
            WHEN 11 THEN 'Noviembre'
            WHEN 12 THEN 'Diciembre'
        END + ' - ' + CAST(YEAR(cts.PayrollDateLiquidated) AS VARCHAR(4)) AS PayrollDate,
        CASE
            WHEN adt.NOMBRE IS NOT NULL AND adt.NOMBRE <> '' AND adt.NOMBRE <> '0'
                THEN adt.NOMBRE
            WHEN p.IdentificationType = 0 THEN 'CC - Cedula de Ciudadania'
        END AS DocumentType,
        tp.Nit,
        tp.Name AS EmployeeName,
        -- ContractStatus CONTEXTUAL: segun el periodo consultado, no el estado actual
        CASE
            WHEN c.Status = 2 AND cr.EffectiveRetirementDate <= EOMONTH(cts.PayrollDateLiquidated) THEN 'ContratoLiquidado'
            WHEN c.Status IN (1, 2, 4) AND (cr.EffectiveRetirementDate IS NULL OR cr.EffectiveRetirementDate > EOMONTH(cts.PayrollDateLiquidated)) THEN 'Activo'
WHEN c.Status = 5 THEN 'LiquidadoParcialmente'
        END AS ContractStatus,
        fu.Code AS FunctionalUnitCode,
        fu.Name AS FunctionalUnitName,
        pos.Code AS PositionCode,
        pos.Name AS PositionName,
        c.BasicSalary,
        c.JobBondingDate AS ContractStartDate,
        ISNULL(
            CASE
                WHEN YEAR(cr.EffectiveRetirementDate) = 1900
                    THEN CONVERT(DATE, '9999-12-31')
                ELSE cr.EffectiveRetirementDate
            END,
            CONVERT(DATE, '9999-12-31')
        ) AS ContractEndDate,
        p.BirthDate,
		DATEDIFF(YEAR, p.BirthDate, @FinalDate)
		-CASE
			WHEN MONTH(p.BirthDate) > MONTH(@FinalDate)
			  OR (MONTH(p.BirthDate) = MONTH(@FinalDate) AND DAY(p.BirthDate) > DAY(@FinalDate))
			THEN 1 ELSE 0
		  END AS Age,
        CASE WHEN p.Gender = 1 THEN 'M' ELSE 'F' END AS Gender,
        ISNULL(coun.Name, '') AS NationalityName,
        CASE p.MaritalStatus
            WHEN 0 THEN 'Soltero'
            WHEN 1 THEN 'Casado'
            WHEN 2 THEN 'Divorciado'
            WHEN 3 THEN 'Viudo'
            WHEN 4 THEN 'UnionLibre'
            WHEN 5 THEN 'Separado'
        END AS MaritalStatus,
        CASE c.PaymentType
            WHEN 1 THEN 'Cheque'
            WHEN 2 THEN 'Efectivo'
            WHEN 3 THEN 'Trasferencia'
            WHEN 4 THEN 'Desprendible'
        END AS PaymentMethod,
        b.Name AS BankName,
        c.BankAccountNumber,
        em.Email AS EmailAddress,
        0 AS TotalPendingVacationDays

    FROM ContractsToShow cts
    INNER JOIN Payroll.Contract c         ON c.Id  = cts.ContractId
    INNER JOIN Payroll.Employee e         ON e.Id  = cts.EmployeeId
    INNER JOIN Common.ThirdParty tp       ON tp.Id = e.ThirdPartyId
    INNER JOIN Common.Person p            ON p.Id  = tp.PersonId
    INNER JOIN Payroll.FunctionalUnit fu  ON fu.Id  = c.FunctionalUnitId
    INNER JOIN Payroll.Position pos       ON pos.Id = c.PositionId
    INNER JOIN ContractRetirement cr      ON cr.ContractId = c.Id
    LEFT JOIN (
        SELECT IdPerson, MAX(Id) AS LatestEmailId
        FROM Common.Email
        GROUP BY IdPerson
    ) LatestEmail ON LatestEmail.IdPerson = p.Id
    LEFT JOIN Common.Email em             ON em.Id = LatestEmail.LatestEmailId
    LEFT JOIN Common.PersonNationality pn ON pn.PersonId = p.Id
    LEFT JOIN Common.Country coun         ON coun.Id = pn.CountryId
    LEFT JOIN dbo.ADTIPOIDENTIFICA adt ON adt.ID = p.IdentificationTypeId
    LEFT JOIN Payroll.Bank b              ON b.Id = c.BankId

    WHERE c.Status <> 3
      AND c.JobBondingDate <= @FinalDate
	    AND (cr.EffectiveRetirementDate IS NULL OR cr.EffectiveRetirementDate >= @InitialDate)
) AS q
ORDER BY EmployeeId, _SortYear, _SortMonth, _SortSalary

END TRY
BEGIN CATCH
    SELECT 999 AS CodeMessage,
           ERROR_MESSAGE() + CHAR(13) + CHAR(10) + ' Line: ' + CAST(ERROR_LINE() AS VARCHAR(10)) AS Message
END CATCH
END
GO
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de talento humano por período: genera un listado consolidado de empleados activos, en proceso de retiro o retirados dentro de un rango de fechas, mostrando una fila por cada versión de contrato relevante en el mes (cargo, salario básico, unidad funcional, estado contractual, datos personales, información bancaria y días de vacaciones pendientes). Cruza las liquidaciones de nómina del período (Payroll.Liquidation) con los contratos laborales (Payroll.Contract) y las liquidaciones de retiro (Payroll.ContractLiquidation) para identificar cambios salariales o de cargo ocurridos en el mes, incluyendo contratos predecesores cuando hubo modificación visible. Se usa para informes de gestión de personal, auditoría de nómina y seguimiento de novedades contractuales (ingresos, modificaciones y retiros de empleados).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_ReportHumanTalent';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_ReportHumanTalent';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte consolidado de talento humano por período, mostrando datos personales, contractuales, de pago y vacaciones pendientes de los empleados con liquidaciones de nómina vigentes en el rango de fechas.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ReportHumanTalent';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@InitialDate y @FinalDate deben ser fechas válidas que delimitan el periodo del reporte.; Deben existir liquidaciones en Payroll.Liquidation con sus respectivos Employee y Contract relacionados; sin esos vínculos no se devuelven filas.; El catálogo ADTIPOIDENTIFICA debe estar disponible para resolver el tipo de documento (legacy).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ReportHumanTalent';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan liquidaciones cuya PayrollDateLiquidated cae dentro del rango [@InitialDate, @FinalDate].; Se excluyen contratos con Status = 3 (estado de contrato considerado no elegible para el reporte).; Solo se incluyen contratos cuyo JobBondingDate sea menor o igual a @FinalDate (vinculación efectiva al cierre del rango).; Se excluyen filas cuya fecha efectiva de retiro sea anterior a @InitialDate o anterior/igual al fin de mes de la liquidación (la liquidación debe corresponder a un período en que el contrato seguía vigente).; Para el correo se selecciona únicamente el último Email registrado por persona (MAX(Id) por IdPerson).; El género se reporta como ''M'' cuando p.Gender = 1 y ''F'' en cualquier otro caso.; Si no existe nacionalidad asociada a la persona, NationalityName se devuelve como cadena vacía.; Cuando ocurre un error, se devuelve un único registro con CodeMessage = 999 y el mensaje de error con la línea, en lugar del resultado del reporte.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ReportHumanTalent';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Liquidación de nómina; Contrato laboral; Tipo de identificación; Estado de contrato; Unidad funcional; Cargo; Liquidación de contrato; Nacionalidad; Estado civil; Método de pago de nómina; Cuenta bancaria; Vacaciones causadas y tomadas; Días pendientes de vacaciones; Antigüedad laboral', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ReportHumanTalent';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.Liquidation: Devuelve un resultset con datos de empleado, contrato, cargo, unidad funcional, banco, email, nacionalidad y días pendientes de vacaciones para cada liquidación dentro del rango [@InitialDate,@FinalDate] cuyo contrato no esté en Status=3 y siga vigente al cierre de mes de la liquidación.; [RETURN_RESULT] (error): Si ocurre cualquier excepción, retorna un resultset alterno con CodeMessage=999 y Message = ERROR_MESSAGE() + '' Line: '' + ERROR_LINE().', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ReportHumanTalent';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si adt.NOMBRE no es nulo, vacío ni ''0'' → Usa adt.NOMBRE como tipo de documento else Si p.IdentificationType = 0 retorna ''CC - Cédula de Ciudadanía''; en otro caso NULL; si c.Status según valor (1,2,4,5) → Mapea a ''Activo'', ''ContratoLiquidado'', ''Reemplazado'' o ''LiquidadoParcialmente'' respectivamente; si YEAR(COALESCE(cl.RetirementDate, c.RetirementDate)) = 1900 o ambos NULL → Fecha fin de contrato se reporta como 9999-12-31 else Se reporta la fecha de retiro vigente (cl.RetirementDate prevalece sobre c.RetirementDate); si DATEDIFF(MONTH, VacationLastDateLiquidation, GETDATE()) por tramos de 12 meses (<12, 12-23, … >=120) → Días de vacaciones causadas se incrementan en escalones de +0, +2, +4, +6, +8, +10, +12, +14, +16, +18 y +20 conforme aumenta la antigüedad desde la última liquidación de vacaciones; si @EmployeeId IS NULL → No filtra por empleado (incluye todos) else Filtra resultados al empleado indicado; si c.Status = 2 (ContratoLiquidado) y no hay RetirementDate → Para validar el rango se usa c.ContractEndingDate como fecha de fin efectiva', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ReportHumanTalent';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.Liquidation; Payroll.Employee; Payroll.Contract; Common.ThirdParty; Common.Person; Common.Email; Common.Phone; Payroll.FunctionalUnit; Payroll.Position; Payroll.ContractLiquidation; Common.PersonNationality; Common.Country; ADTIPOIDENTIFICA; Payroll.Bank; Payroll.VacationPeriod', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ReportHumanTalent';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ReportHumanTalent';
-- GO
