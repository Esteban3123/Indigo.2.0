-- =============================================
-- Author:		Oscar stiven astudillo
-- Create date: 2025-12-15
-- Description:	Sp que obtiene las vacaciones detallada, filtrando por fecha o por empledo
-- =============================================

CREATE  PROCEDURE [Payroll].[SP_SummaryVacationLiquidation]
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
            AND V.VacationValue = L.VacationValueEnjoyed
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
