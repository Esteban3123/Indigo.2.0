-- =============================================
-- Author:		Oscar stiven astudillo
-- Create date: 2025-12-05
-- Description:	Reporte de detalle de liquidación con conceptos dinamicas como columnas
-- =============================================
CREATE PROCEDURE [Payroll].[SP_LiquidationDetailReport]
(
    @InitialDate DATE,
    @EndDate DATE,
    @EmployeeId INT = NULL,
    @GroupInitial INT = NULL,
    @GroupFinal INT = NULL,
    @BranchOfficeInitial INT = NULL,
    @BranchOfficeFinal INT = NULL,
    @RegisterStatus CHAR(1) = NULL
)
AS
BEGIN 
    SET NOCOUNT ON;
    DECLARE @SQL NVARCHAR(MAX);
    DECLARE @ColsAccrued NVARCHAR(MAX) = '';
    DECLARE @ColsDeducted NVARCHAR(MAX) = '';
    DECLARE @ColsEmployer NVARCHAR(MAX) = '';
    DECLARE @ColsHoursQuantity NVARCHAR(MAX) = '';

    -- Obtener columnas dinamicas para CONCEPTOS DEVENGADOS (ConceptType = '1')
    -- Excluye los conceptos de horas/recargos que se muestran en @ColsHoursQuantity
    SELECT @ColsAccrued = STUFF((
		SELECT ', ' + CONCAT('SUM(CASE WHEN conceptos.ConceptCode = ''', Code, ''' THEN conceptos.ConceptTotalValue ELSE 0 END) AS [', 
					   Code, ' - ', REPLACE(REPLACE(Name, '''', ''), '  ', ' '), ']')
		FROM Payroll.Concept WITH (NOLOCK)
		WHERE State = 1
		  AND ConceptType = 1
		  AND ConceptClass NOT IN ('001', '012', '013', '042', '043', '050', '051', '052')  -- Excluir horas/recargos
		ORDER BY Code
        FOR XML PATH(''), TYPE
    ).value('.', 'NVARCHAR(MAX)'), 1, 2, '');

    -- Obtener columnas dinamicas para CONCEPTOS DEDUCIDOS (ConceptType = '2')
    SELECT @ColsDeducted = STUFF((
        SELECT ', ' + CONCAT('SUM(CASE WHEN conceptos.ConceptCode = ''', Code, ''' THEN conceptos.ConceptTotalValue ELSE 0 END) AS [', 
               Code, ' - ', REPLACE(REPLACE(Name, '''', ''), '  ', ' '), ']')
		FROM Payroll.Concept WITH (NOLOCK)
		WHERE State = 1
		  AND ConceptType = 2
		ORDER BY Code
        FOR XML PATH(''), TYPE
    ).value('.', 'NVARCHAR(MAX)'), 1, 2, '');

    -- Obtener columnas dinamicas para CONCEPTOS PATRONALES (ConceptType = '3')
    SELECT @ColsEmployer = STUFF((
      SELECT ', ' + CONCAT('SUM(CASE WHEN conceptos.ConceptCode = ''', Code, ''' THEN conceptos.ConceptTotalValue ELSE 0 END) AS [', 
               Code, ' - ', REPLACE(REPLACE(Name, '''', ''), '  ', ' '), ']')
		FROM Payroll.Concept WITH (NOLOCK)
		WHERE State = 1
		  AND ConceptType = 3
		ORDER BY 
		CASE 
				WHEN ConceptClass IN ('008', '034', '033', '031') THEN 1 --CONCEPTOS DE PROVISIONES DE ULTIMO
				ELSE 0
			END,Code 
        FOR XML PATH(''), TYPE
    ).value('.', 'NVARCHAR(MAX)'), 1, 2, '');

    -- Obtener columnas dinamicas para HORAS
    -- ConceptClass de horas: 001, 012, 013, 042, 043, 050, 051, 052
    SELECT @ColsHoursQuantity = STUFF((
        SELECT ', ' + CONCAT(
               'SUM(CASE WHEN conceptos.ConceptCode = ''', Code, ''' THEN conceptos.ConceptQuantity ELSE 0 END) AS [', 
               Code, ' - ', REPLACE(REPLACE(Name, '''', ''), '  ', ' '), ' CANTIDAD], ',
               'SUM(CASE WHEN conceptos.ConceptCode = ''', Code, ''' THEN conceptos.ConceptTotalValue ELSE 0 END) AS [', 
               Code, ' - ', REPLACE(REPLACE(Name, '''', ''), '  ', ' '), ']')
        FROM Payroll.Concept WITH (NOLOCK)
        WHERE State = 1
          AND ConceptType = 1  -- Solo conceptos devengados
          AND ConceptClass IN ('001', '012', '013', '042', '043', '050', '051', '052')  -- Clases de horas/recargos
        ORDER BY Code
        FOR XML PATH(''), TYPE
    ).value('.', 'NVARCHAR(MAX)'), 1, 2, '');

    -- Construir la consulta SQL dinamicas
    SET @SQL = N'
    WITH CTE_Base AS
    (
        SELECT
            -- 1. EMPLOYEE INFORMATION
			liq.Id AS LiquidationId,
			emp.Id AS EmployeeId,
            emp.InternalCode AS InternalCode,
            ter.Nit AS DocumentNumber,
            CONCAT(per.FirstLastName, '' '', per.SecondLastName, '' '', per.FirstName, '' '', per.SecondName) AS EmployeeName,
			CASE
				WHEN per.Gender = 1 THEN ''M''
				WHEN per.Gender = 2 THEN ''F''
				WHEN per.Gender = 3 THEN ''Otro''
				ELSE ''''  
			END AS Gender,
            ema2.Email AS Email,
            per.BirthDate AS BirthDate,
            
            -- Work data
            car.Code AS PositionCode,
            car.Name AS PositionName,
            uf.Code AS FunctionalUnitCode,
            uf.Name AS FunctionalUnitName,
            cc.Code AS CostCenterCode,
            cc.Name AS CostCenterName,
            gru.Code AS GroupCode,
            gru.Name AS GroupName,
            cct.JobBondingDate AS HiringDate,
            cct.BasicSalary AS BasicSalary,
            
            -- Social security data
            sal.Code AS HealthFundCode,
            sal.Name AS HealthFundName,
            pen.Code AS PensionFundCode,
            pen.Name AS PensionFundName,
            
            -- Bank data
            bco.Code AS BankCode,
            bco.Name AS BankName,
            cct.BankAccountNumber AS BankAccountNumber,
            emp.ProfessionalRiskPercentage AS ProfessionalRiskPercentage,
            
            -- Liquidation dates
            liq.PayrollDateLiquidated AS LiquidationDate,
            YEAR(liq.PayrollDateLiquidated) AS Year,
            MONTH(liq.PayrollDateLiquidated) AS Month,
            DAY(liq.PayrollDateLiquidated) AS Day,
            
            -- 2. WORKED DAYS
            part1.DiasNomina AS PayrollDays,
            part1.DiasTrabaj AS WorkedDays,
            part1.DiasVacac AS VacationDays,
            part1.DiasIncap AS DisabilityDays,
            part1.DiasPermisoSinGoce AS UnpaidLeaveDays,
            part1.DiasPermisoConGoce AS PaidLeaveDays,
            
            -- Totals
            part1.TotalAccrued AS TotalAccrued,
            part1.TotalDeducted AS TotalDeducted,
            (part1.TotalAccrued - part1.TotalDeducted) AS NetPay,
            part1.IBCHealth
            
        FROM Payroll.Employee emp WITH (NOLOCK)
        INNER JOIN Common.ThirdParty ter WITH (NOLOCK) ON ter.Id = emp.ThirdPartyId
        INNER JOIN Common.Person per WITH (NOLOCK) ON per.Id = ter.PersonId
        INNER JOIN Payroll.[Contract] cct WITH (NOLOCK) ON cct.EmployeeId = emp.Id
        INNER JOIN Payroll.Position car WITH (NOLOCK) ON car.Id = cct.PositionId
        INNER JOIN Payroll.FunctionalUnit uf WITH (NOLOCK) ON uf.Id = cct.FunctionalUnitId
        INNER JOIN Payroll.CostCenter cc WITH (NOLOCK) ON cc.Id = emp.CostCenterId
        INNER JOIN Payroll.[Group] gru WITH (NOLOCK) ON gru.Id = cct.GroupId
        INNER JOIN Payroll.Liquidation liq WITH (NOLOCK) ON liq.ContractId = cct.Id
            AND liq.EmployeeId = cct.EmployeeId
            AND liq.GroupId = cct.GroupId
        LEFT JOIN (
            SELECT
                ISNULL(SUM(PayrollDays), 0) AS DiasNomina,
                ISNULL(SUM(DaysWorked), 0) AS DiasTrabaj,
                ISNULL(SUM(VacationDays), 0) AS DiasVacac,
                ISNULL(SUM(DisabilityDays), 0) AS DiasIncap,
                ISNULL(SUM(UnpaidLicenseDays), 0) AS DiasPermisoSinGoce,
                ISNULL(SUM(LicenseDays), 0) AS DiasPermisoConGoce,
                SUM(TotalAccrued) AS TotalAccrued,
                SUM(TotalDeducted) AS TotalDeducted,
                SUM(HealthJCB) AS IBCHealth,
                EmployeeId AS Item,
                YEAR(PayrollDateLiquidated) AS Ano,
                MONTH(PayrollDateLiquidated) AS Mes,
                DAY(PayrollDateLiquidated) AS Dia
            FROM Payroll.Liquidation WITH (NOLOCK)
            GROUP BY EmployeeId,
                     YEAR(PayrollDateLiquidated),
                     MONTH(PayrollDateLiquidated),
                     DAY(PayrollDateLiquidated)
        ) part1 ON part1.Item = liq.EmployeeId
            AND part1.Ano = YEAR(liq.PayrollDateLiquidated)
            AND part1.Mes = MONTH(liq.PayrollDateLiquidated)
            AND part1.Dia = DAY(liq.PayrollDateLiquidated)
        LEFT JOIN Payroll.Fund sal WITH (NOLOCK) ON sal.Id = liq.HealthFundId
        LEFT JOIN Payroll.Fund pen WITH (NOLOCK) ON pen.Id = liq.PensionFundId
        LEFT JOIN Payroll.Bank bco WITH (NOLOCK) ON bco.Id = cct.BankId
        LEFT JOIN (
            SELECT
                MAX(Id) AS Item,
                IdPerson
            FROM Common.Email WITH (NOLOCK)
            GROUP BY IdPerson
        ) ema ON ema.IdPerson = per.Id
        LEFT JOIN Common.Email ema2 WITH (NOLOCK) ON ema2.Id = ema.Item
        WHERE liq.PayrollDateLiquidated >= @InitialDate
          AND liq.PayrollDateLiquidated < DATEADD(DAY, 1, @EndDate)
          AND (@EmployeeId IS NULL OR liq.EmployeeId = @EmployeeId)
          AND (@GroupInitial IS NULL OR gru.Id >= @GroupInitial)
          AND (@GroupFinal IS NULL OR gru.Id <= @GroupFinal)
          AND (@BranchOfficeInitial IS NULL OR uf.BranchOfficeId >= @BranchOfficeInitial)
          AND (@BranchOfficeFinal IS NULL OR uf.BranchOfficeId <= @BranchOfficeFinal)
          AND (@RegisterStatus IS NULL OR @RegisterStatus = ''T'' OR liq.RegisterStatus = @RegisterStatus)
    ),

    CTE_Conceptos AS
    (
        SELECT
            liq.Id AS LiquidationId,
            lde.ConceptCode,
            lde.ConceptType,
            SUM(lde.ConceptTotalValue) AS ConceptTotalValue,
            SUM(COALESCE(NULLIF(lde.Quantity, 0), lde.TotalNumberHours, 0)) AS ConceptQuantity,
            con.Name AS ConceptName
        FROM Payroll.Liquidation liq WITH (NOLOCK)
        INNER JOIN Payroll.LiquidationDetail lde WITH (NOLOCK) ON lde.PayrollId = liq.Id
        INNER JOIN Payroll.Concept con WITH (NOLOCK) ON con.Id = lde.ConceptId AND con.State = 1
        INNER JOIN Payroll.[Contract] cct WITH (NOLOCK) ON cct.Id = liq.ContractId
        INNER JOIN Payroll.FunctionalUnit uf WITH (NOLOCK) ON uf.Id = cct.FunctionalUnitId
        INNER JOIN Payroll.[Group] gru WITH (NOLOCK) ON gru.Id = cct.GroupId
        WHERE liq.PayrollDateLiquidated >= @InitialDate
          AND liq.PayrollDateLiquidated < DATEADD(DAY, 1, @EndDate)
          AND (@EmployeeId IS NULL OR liq.EmployeeId = @EmployeeId)
          AND (@GroupInitial IS NULL OR gru.Id >= @GroupInitial)
          AND (@GroupFinal IS NULL OR gru.Id <= @GroupFinal)
          AND (@BranchOfficeInitial IS NULL OR uf.BranchOfficeId >= @BranchOfficeInitial)
          AND (@BranchOfficeFinal IS NULL OR uf.BranchOfficeId <= @BranchOfficeFinal)
          AND (@RegisterStatus IS NULL OR @RegisterStatus = ''T'' OR liq.RegisterStatus = @RegisterStatus)
          AND lde.ConceptTotalValue <> 0
        GROUP BY liq.Id, lde.ConceptCode, lde.ConceptType, con.Name
    )

    SELECT
		base.LiquidationId,
		base.EmployeeId,
        base.InternalCode,
        base.DocumentNumber,
        base.EmployeeName,
        base.Gender,
        base.Email,
        base.BirthDate,
        base.PositionCode,
        base.PositionName,
        base.FunctionalUnitCode,
        base.FunctionalUnitName,
        base.CostCenterCode,
        base.CostCenterName,
        base.GroupCode,
        base.GroupName,
        base.HiringDate,
        base.BasicSalary,
        base.HealthFundCode,
        base.HealthFundName,
        base.PensionFundCode,
        base.PensionFundName,
        base.BankCode,
        base.BankName,
        base.BankAccountNumber,
        base.ProfessionalRiskPercentage,
        base.LiquidationDate,
        base.Year,
        base.Month,
        base.Day,
        base.PayrollDays,
        base.WorkedDays,
        base.VacationDays,
        base.DisabilityDays,
        base.UnpaidLeaveDays,
        base.PaidLeaveDays' + 
        CASE WHEN @ColsHoursQuantity IS NOT NULL AND @ColsHoursQuantity <> '' THEN ', ' + @ColsHoursQuantity ELSE '' END + 
        CASE WHEN @ColsAccrued IS NOT NULL AND @ColsAccrued <> '' THEN ', ' + @ColsAccrued ELSE '' END + ',
        base.TotalAccrued' + 
        CASE WHEN @ColsDeducted IS NOT NULL AND @ColsDeducted <> '' THEN ', ' + @ColsDeducted ELSE '' END + ',
        base.TotalDeducted,
		base.IBCHealth,
        base.NetPay' + 
        CASE WHEN @ColsEmployer IS NOT NULL AND @ColsEmployer <> '' THEN ', ' + @ColsEmployer ELSE '' END + '
    FROM CTE_Base base
    LEFT JOIN CTE_Conceptos conceptos ON conceptos.LiquidationId = base.LiquidationId
    GROUP BY
        base.EmployeeId, base.InternalCode, base.DocumentNumber, base.EmployeeName, base.Gender, base.Email, base.BirthDate,
        base.PositionCode, base.PositionName, base.FunctionalUnitCode, base.FunctionalUnitName, base.CostCenterCode, base.CostCenterName,
        base.GroupCode, base.GroupName, base.HiringDate, base.BasicSalary,
        base.HealthFundCode, base.HealthFundName, base.PensionFundCode, base.PensionFundName, base.BankCode, base.BankName,
        base.BankAccountNumber, base.ProfessionalRiskPercentage, base.LiquidationDate, base.Year, base.Month, base.Day,
        base.PayrollDays, base.WorkedDays, base.VacationDays, base.DisabilityDays, base.UnpaidLeaveDays, base.PaidLeaveDays,
        base.TotalAccrued, base.TotalDeducted, base.NetPay, base.IBCHealth, base.LiquidationId
    ORDER BY base.DocumentNumber, base.LiquidationDate';

    -- Ejecutar la consulta
    EXEC sp_executesql @SQL, N'@InitialDate DATE, @EndDate DATE, @EmployeeId INT, @GroupInitial INT, @GroupFinal INT, @BranchOfficeInitial INT, @BranchOfficeFinal INT, @RegisterStatus CHAR(1)', 
        @InitialDate, @EndDate, @EmployeeId, @GroupInitial, @GroupFinal, @BranchOfficeInitial, @BranchOfficeFinal, @RegisterStatus;

END;