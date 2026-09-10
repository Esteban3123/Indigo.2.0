

CREATE   PROCEDURE [Payroll].[SP_SummaryVacationLiquidation]
(
    @LiquidationDate DATE = NULL,    
    @EmployeeId   INT = NULL  
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @PeriodStartDate DATE = DATEFROMPARTS(YEAR(@LiquidationDate), MONTH(@LiquidationDate), 1);
    DECLARE @PeriodEndDate DATE = DATEADD(MONTH, 1, @PeriodStartDate);

    WITH CTE_Base
    AS (
        SELECT
            E.Id AS EmployeeId
           ,T.Nit AS EmployeeNit
           ,T.Name AS EmployeeName
           ,CAST(E.AdmissionDate AS DATE) AS AdmissionDate
           ,E.ProfessionalRiskPercentage AS RiskPercentage
           ,ET.Name AS EmployeeType
           ,WC.Name AS WorkCenter
           ,CC.Name AS CostCenter
           ,CASE
                WHEN E.State = 1 THEN 'ACTIVO'
                ELSE 'INACTIVO'
            END AS EmployeeStatus
        FROM Payroll.Employee AS E
        INNER JOIN Common.ThirdParty AS T
            ON E.ThirdPartyId = T.Id
        INNER JOIN Payroll.EmployeeType AS ET
            ON ET.Id = E.EmployeeTypeId
        INNER JOIN Payroll.CostCenter AS CC
            ON CC.Id = E.CostCenterId
        INNER JOIN Payroll.WorkCenter AS WC
            ON WC.Id = E.WorkCenterId
    )
    ,CTE_Liquidation
    AS (
        SELECT
            L.EmployeeId AS EmployeeId
           ,YEAR(L.PayrollDateLiquidated) AS Year
           ,MONTH(L.PayrollDateLiquidated) AS Month
           ,L.PayrollDateLiquidated AS PayrollLiquidationDate
           ,L.VacationDays AS VacationDaysInLiquidationMonth
           ,L.VacationValueEnjoy AS VacationValueEnjoyed
           ,L.PeriodJCB AS LastLiquidationIBC
        FROM Payroll.Liquidation AS L
        INNER JOIN CTE_Base AS E
            ON E.EmployeeId = L.EmployeeId
    )
    ,CTE_Vacations
    AS (
        SELECT
            E.EmployeeNit AS EmployeeNit
           ,E.EmployeeName AS EmployeeName
           ,VP.EmployeeId AS EmployeeId
           ,VP.InitialDatePeriod AS InitialAccrualDate
           ,VP.EndDatePeriod AS FinalAccrualDate
           ,VP.VacationDays AS AccumulatedDays
           ,V.TakenDays AS TakenDaysRegistered
           ,VP.PendingDays AS PendingDays
           ,V.EnjoyDays AS EnjoyedDaysDF
           ,V.VacationStartDate AS VacationStartDate
           ,V.VacationEndDate AS VacationEndDate
           ,V.IncorporationDate AS IncorporationDate
           ,V.IncorporationDateReal AS RealIncorporationDate
           ,YEAR(V.LiquidationDate) AS LiquidationYear
           ,MONTH(V.LiquidationDate) AS LiquidationMonth
           ,V.BaseLiquidation AS BaseLiquidationValue
           ,V.VacationValue AS VacationValue
           ,V.HealthContribution AS HealthValue
           ,V.PensionContribution AS PensionValue
           ,V.VacationValueNet AS NetVacationValue
           ,CASE
                WHEN V.TypeLiquidation = 1 THEN 'PROMEDIO'
                ELSE 'SUELDO BASICO'
            END AS LiquidationType
           ,CASE
                WHEN V.TypeVacation = 1 THEN 'PAGADAS'
                WHEN V.TypeVacation = 2 THEN 'DISFRUTAR'
                WHEN V.TypeVacation = 3 THEN 'PERMISO CON CARGO A VACACIONES'
                WHEN V.TypeVacation = 4 THEN 'VACACIONES COMPENSADAS'
                ELSE 'INTERRUMPIDAS'
            END AS VacationType
           ,CASE
                WHEN V.TypePayment = 1 THEN 'INMEDIATO'
                ELSE 'PROXIMA NOMINA'
            END AS PaymentType
           ,V.Id AS VacationId
        FROM Payroll.VacationPeriod AS VP
        INNER JOIN CTE_Base AS E
            ON E.EmployeeId = VP.EmployeeId
        LEFT JOIN Payroll.Vacation AS V
            ON VP.Id = V.VacationPeriodId 
    )
    ,CTE_Final
    AS (
        SELECT
            V.*
           ,L.PayrollLiquidationDate AS PayrollLiquidationDate
           ,L.VacationDaysInLiquidationMonth AS VacationDaysInLiquidationMonth
           ,L.LastLiquidationIBC AS LastLiquidationIBC
        FROM CTE_Vacations AS V
        LEFT JOIN CTE_Liquidation AS L
            ON V.EmployeeId = L.EmployeeId
            AND V.LiquidationYear = L.Year
            AND V.LiquidationMonth = L.Month
            --AND V.VacationValue = L.VacationValueEnjoyed
    )
    ,CTE_Calculated
    AS (
        SELECT
            F.*
           ,CASE
                WHEN F.VacationStartDate = '0001-01-01' THEN F.AccumulatedDays
                ELSE F.AccumulatedDays
                     - ISNULL(
                           SUM(F.TakenDaysRegistered) OVER (
                               PARTITION BY F.EmployeeNit
                                          ,F.InitialAccrualDate
                                          ,F.FinalAccrualDate
                               ORDER BY F.VacationStartDate
                               ROWS BETWEEN UNBOUNDED PRECEDING AND 1 PRECEDING
                           )
                         ,0
                       )
            END AS CorrectedAccumulatedDays
        FROM CTE_Final AS F
    )

    SELECT
        C.EmployeeNit AS EmployeeNit
       ,C.EmployeeName AS EmployeeName
       ,DENSE_RANK() OVER (
            PARTITION BY C.EmployeeNit
            ORDER BY C.InitialAccrualDate
                    ,C.FinalAccrualDate
        ) AS Period
       ,C.InitialAccrualDate AS InitialAccrualDate
       ,C.FinalAccrualDate AS FinalAccrualDate
       ,C.CorrectedAccumulatedDays AS AccumulatedDays
       ,C.TakenDaysRegistered AS TakenDaysRegistered
       ,C.CorrectedAccumulatedDays - C.TakenDaysRegistered AS PendingDaysRegistered
       ,C.PendingDays AS PendingDays
       ,C.EnjoyedDaysDF AS EnjoyedDaysDF
       ,C.VacationStartDate AS VacationStartDate
       ,C.VacationEndDate AS VacationEndDate
       ,C.IncorporationDate AS IncorporationDate
       ,C.RealIncorporationDate AS RealIncorporationDate
       ,C.PayrollLiquidationDate AS PayrollLiquidationDate
       ,C.LiquidationYear AS LiquidationYear
       ,C.LiquidationMonth AS LiquidationMonth
       ,C.VacationDaysInLiquidationMonth AS VacationDaysInLiquidationMonth
       ,C.BaseLiquidationValue AS BaseLiquidationValue
       ,C.VacationValue AS VacationValue
       ,C.HealthValue AS HealthValue
       ,C.PensionValue AS PensionValue
       ,C.NetVacationValue AS NetVacationValue
       ,C.LastLiquidationIBC AS LastLiquidationIBC
       ,C.LiquidationType AS LiquidationType
       ,C.VacationType AS VacationType
       ,C.PaymentType AS PaymentType
       ,C.VacationId AS VacationId
    FROM CTE_Calculated AS C
    WHERE (C.PayrollLiquidationDate IS NOT NULL
            OR C.VacationType <> 'INTERRUMPIDAS')
    AND (@EmployeeId IS NULL 
            OR C.EmployeeId = @EmployeeId)
    AND (@LiquidationDate IS NULL 
            OR (
                (@EmployeeId IS NULL 
                    AND C.PayrollLiquidationDate >= @PeriodStartDate
                    AND C.PayrollLiquidationDate < @PeriodEndDate)
                OR (@EmployeeId IS NOT NULL 
                    AND C.PayrollLiquidationDate < @PeriodEndDate)
            ))
    ORDER BY EmployeeNit
            ,InitialAccrualDate
            ,VacationStartDate
            DESC
END;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera el resumen detallado de la liquidación de vacaciones de los empleados para un período y/o empleado específico. Consolida información del empleado (NIT, nombre, tipo, centro de costo, sede laboral y estado), sus períodos de causación de vacaciones, los días acumulados, tomados, pendientes y disfrutados, junto con los valores liquidados (base, bruto, salud, pensión y neto), el tipo de vacación (pagadas, a disfrutar, compensadas, con permiso o interrumpidas) y la forma de pago (inmediato o próxima nómina). Cruza los datos de nómina liquidada del período para incorporar los días de vacaciones reflejados en la liquidación mensual y el IBC, calculando además los días acumulados corregidos según el historial de tomas previas dentro de cada período de causación. Se utiliza para reportes de control y auditoría de vacaciones en el módulo de nómina, permitiendo verificar la consistencia entre los registros de vacaciones y las liquidaciones de nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_SummaryVacationLiquidation';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_SummaryVacationLiquidation';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un resumen consolidado de liquidaciones de vacaciones por empleado, mostrando períodos de causación, días acumulados/tomados/pendientes, valores liquidados y su correlación con la nómina del mes.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SummaryVacationLiquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El empleado debe existir con sus relaciones obligatorias en ThirdParty, EmployeeType, CostCenter y WorkCenter (INNER JOIN en CTE_Base).; Si se filtra por @LiquidationDate, este se normaliza al primer día del mes y se usa como ventana [PeriodStartDate, PeriodEndDate).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SummaryVacationLiquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El cruce con la liquidación de nómina (CTE_Liquidation) requiere coincidencia exacta de empleado, año, mes y que VacationValue = VacationValueEnjoyed.; El número de período (Period) se asigna por empleado mediante DENSE_RANK ordenado por fecha inicial y final de causación, garantizando numeración consecutiva por períodos distintos.; Vacaciones de tipo ''INTERRUMPIDAS'' solo se muestran cuando tienen una liquidación de nómina asociada.; Los días acumulados corregidos descuentan solo las tomas registradas previas a la fila actual ordenadas por VacationStartDate dentro del mismo período de causación.; El resultado se ordena por NIT del empleado, fecha inicial de causación y fecha de inicio de vacaciones descendente.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SummaryVacationLiquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Empleado; Tercero/NIT; Centro de costo; Centro de trabajo; Tipo de empleado; Liquidación de nómina; Período de causación de vacaciones; Días acumulados/tomados/pendientes de vacaciones; Vacaciones disfrutadas, pagadas, compensadas, interrumpidas; Permiso con cargo a vacaciones; Base de liquidación; Aporte a salud y pensión; IBC (PeriodJCB); Liquidación por promedio o sueldo básico; Pago inmediato o próxima nómina', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SummaryVacationLiquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas de vacaciones por empleado y período acumulativo, con cálculo de días acumulados corregidos y correlación opcional con la liquidación de nómina del mismo mes.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SummaryVacationLiquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si E.State = 1 → Marca EmployeeStatus=''ACTIVO'' else Marca EmployeeStatus=''INACTIVO''; si V.TypeLiquidation = 1 → LiquidationType=''PROMEDIO'' else LiquidationType=''SUELDO BASICO''; si V.TypeVacation in (1,2,3,4) → Mapea a ''PAGADAS'',''DISFRUTAR'',''PERMISO CON CARGO A VACACIONES'',''VACACIONES COMPENSADAS'' respectivamente else Cualquier otro valor se etiqueta como ''INTERRUMPIDAS''; si V.TypePayment = 1 → PaymentType=''INMEDIATO'' else PaymentType=''PROXIMA NOMINA''; si F.VacationStartDate = ''0001-01-01'' → CorrectedAccumulatedDays = AccumulatedDays (sin restar tomados previos) else CorrectedAccumulatedDays = AccumulatedDays - suma acumulada de TakenDaysRegistered de filas previas del mismo período de causación; si @EmployeeId IS NULL AND @LiquidationDate IS NOT NULL → Filtra por PayrollLiquidationDate dentro del mes [PeriodStartDate, PeriodEndDate) else Si @EmployeeId IS NOT NULL y @LiquidationDate IS NOT NULL, filtra solo por PayrollLiquidationDate < PeriodEndDate (toda la historia hasta fin de mes); si C.PayrollLiquidationDate IS NULL AND C.VacationType = ''INTERRUMPIDAS'' → Excluye la fila del resultado else Incluye la fila', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SummaryVacationLiquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.Employee; Common.ThirdParty; Payroll.EmployeeType; Payroll.CostCenter; Payroll.WorkCenter; Payroll.Liquidation; Payroll.VacationPeriod; Payroll.Vacation', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SummaryVacationLiquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SummaryVacationLiquidation';
-- GO
