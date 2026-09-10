-- =============================================
-- Author:      Oscar Stiven Astudillo
-- Create date: 2025-02-08
-- Description: Reporte de liquidación de contratos con conceptos dinámicos
-- Parámetros:  Fecha inicio, fecha fin, y empleado opcional
-- =============================================
CREATE PROCEDURE [Payroll].[SP_ContractLiquidationDetailReport]
(
    @InitialDate DATE,
    @EndDate DATE,
    @EmployeeId INT = NULL
)
AS
BEGIN 
    SET NOCOUNT ON;
    DECLARE @SQL NVARCHAR(MAX);
    DECLARE @ColsAccrued NVARCHAR(MAX) = '';
    DECLARE @ColsDeducted NVARCHAR(MAX) = '';

    -- Obtener columnas dinámicas para CONCEPTOS DEVENGADOS (ConceptType = 1)
    -- Usa Code y Name de la tabla Concept
    SELECT @ColsAccrued = STUFF((
        SELECT ', ' + CONCAT(
            'MAX(CASE WHEN conceptos.ConceptCode = ''', Code, ''' AND conceptos.Accrued > 0 THEN conceptos.Accrued ELSE NULL END) AS [', 
            Code, ' - ', REPLACE(REPLACE(Name, '''', ''''''), '  ', ' '), ']')
        FROM Payroll.Concept WITH (NOLOCK)
        WHERE State = 1
          AND ConceptType = 1
          AND EXISTS (
              SELECT 1 
              FROM Payroll.ContractLiquidationDetail cld WITH (NOLOCK)
              INNER JOIN Payroll.ContractLiquidation cl WITH (NOLOCK) ON cl.Id = cld.ContractLiquidationId
              WHERE cld.IdConcept = Concept.Id
                AND cld.Accrued > 0
                AND cl.RetirementDate >= @InitialDate
                AND cl.RetirementDate < DATEADD(DAY, 1, @EndDate)
                AND (@EmployeeId IS NULL OR cl.EmployeeId = @EmployeeId)
          )
        ORDER BY Code
        FOR XML PATH(''), TYPE
    ).value('.', 'NVARCHAR(MAX)'), 1, 2, '');

    -- Obtener columnas dinámicas para CONCEPTOS DEDUCIDOS (ConceptType = 2)
    SELECT @ColsDeducted = STUFF((
        SELECT ', ' + CONCAT(
            'MAX(CASE WHEN conceptos.ConceptCode = ''', Code, ''' AND conceptos.Deducted > 0 THEN conceptos.Deducted ELSE NULL END) AS [', 
            Code, ' - ', REPLACE(REPLACE(Name, '''', ''''''), '  ', ' '), ']')
        FROM Payroll.Concept WITH (NOLOCK)
        WHERE State = 1
          AND ConceptType = 2
          AND EXISTS (
              SELECT 1 
              FROM Payroll.ContractLiquidationDetail cld WITH (NOLOCK)
              INNER JOIN Payroll.ContractLiquidation cl WITH (NOLOCK) ON cl.Id = cld.ContractLiquidationId
              WHERE cld.IdConcept = Concept.Id
                AND cld.Deducted > 0
                AND cl.RetirementDate >= @InitialDate
                AND cl.RetirementDate < DATEADD(DAY, 1, @EndDate)
                AND (@EmployeeId IS NULL OR cl.EmployeeId = @EmployeeId)
          )
        ORDER BY Code
        FOR XML PATH(''), TYPE
    ).value('.', 'NVARCHAR(MAX)'), 1, 2, '');

    -- Construir la consulta SQL dinámica
    SET @SQL = N'
    WITH CTE_Base AS
    (
        SELECT
            -- 1. IDs
            cl.Id AS ContractLiquidationId,
            emp.Id AS EmployeeId,
            
			g.Code as CodeGroup,
			g.Name AS NameGroup,
            -- 2. Información del Empleado
            ter.Nit AS DocumentNumber,
            ter.Name AS EmployeeName,
       
            -- 3. Información Laboral
            car.Code AS PositionCode,
            car.Name AS PositionName,
            uf.Code AS FunctionalUnitCode,
            uf.Name AS FunctionalUnitName,
            cct.BasicSalary,

            
            -- 4. Información de Retiro
			cct.JobBondingDate,
            cl.RetirementDate,
            rr.Name AS RetirementReason,
            DATEDIFF(DAY, cct.JobBondingDate, cl.RetirementDate) AS TotalDaysWorked,
        
            
            -- 7. Totales de Liquidación
            cl.TotalAccrued,
            cl.TotalDeducted,
            cl.TotalPaid AS NetPay,
            
            -- 8. Estado y Resolución
            CASE WHEN cl.Status = ''C'' THEN ''Confirmado'' ELSE ''Sin Confirmar'' END AS StatusDescription,
            cl.Status,
            cl.ResolutionNumber,
            cl.ResolutionDate
            


        FROM Payroll.ContractLiquidation cl WITH (NOLOCK)
        INNER JOIN Payroll.Employee emp WITH (NOLOCK) ON emp.Id = cl.EmployeeId
        INNER JOIN Common.ThirdParty ter WITH (NOLOCK) ON ter.Id = emp.ThirdPartyId
        INNER JOIN Payroll.[Contract] cct WITH (NOLOCK) ON cct.Id = cl.ContractId
		INNER JOIN Payroll.[Group] g WITH (NOLOCK) ON g.Id = cct.GroupId
        INNER JOIN Payroll.Position car WITH (NOLOCK) ON car.Id = cct.PositionId
        INNER JOIN Payroll.FunctionalUnit uf WITH (NOLOCK) ON uf.Id = cct.FunctionalUnitId
        INNER JOIN Payroll.CostCenter cc WITH (NOLOCK) ON cc.Id = emp.CostCenterId
        INNER JOIN Payroll.RetirementReason rr WITH (NOLOCK) ON rr.Id = cl.RetirementReasonId
        LEFT JOIN Payroll.Bank bco WITH (NOLOCK) ON bco.Id = cct.BankId
       
        WHERE cl.RetirementDate >= @InitialDate
          AND cl.RetirementDate < DATEADD(DAY, 1, @EndDate)
          AND (@EmployeeId IS NULL OR cl.EmployeeId = @EmployeeId)
    ),
    
    CTE_Conceptos AS
    (
        SELECT
            cld.ContractLiquidationId,
            con.Code AS ConceptCode,
            con.Name AS ConceptName,
            con.ConceptType,
            cld.Accrued,
            cld.Deducted
        FROM Payroll.ContractLiquidationDetail cld WITH (NOLOCK)
        INNER JOIN Payroll.Concept con WITH (NOLOCK) ON con.Id = cld.IdConcept AND con.State = 1
        WHERE EXISTS (
            SELECT 1 FROM CTE_Base b WHERE b.ContractLiquidationId = cld.ContractLiquidationId
        )
    )
    
    SELECT
        base.ContractLiquidationId,
        base.EmployeeId,
		base.CodeGroup,
		base.NameGroup,
        base.DocumentNumber,
        base.EmployeeName,
        base.PositionName,
        base.FunctionalUnitName,
        base.BasicSalary,
		base.StatusDescription,
		base.JobBondingDate,
        base.RetirementDate,
        base.RetirementReason,
        base.TotalDaysWorked' + 
        CASE WHEN @ColsAccrued IS NOT NULL AND @ColsAccrued <> '' THEN ', ' + @ColsAccrued ELSE '' END + ',
        base.TotalAccrued' + 
        CASE WHEN @ColsDeducted IS NOT NULL AND @ColsDeducted <> '' THEN ', ' + @ColsDeducted ELSE '' END + ',
        base.TotalDeducted,
        base.NetPay
    FROM CTE_Base base
    LEFT JOIN CTE_Conceptos conceptos ON conceptos.ContractLiquidationId = base.ContractLiquidationId
    GROUP BY
        base.ContractLiquidationId, base.EmployeeId, base.DocumentNumber, CodeGroup,NameGroup,
        base.EmployeeName, base.PositionName, base.FunctionalUnitName,base.BasicSalary, 
        base.JobBondingDate,base.RetirementDate, base.RetirementReason, 
        base.TotalDaysWorked, base.TotalAccrued, base.TotalDeducted, base.NetPay, base.StatusDescription
    ORDER BY base.DocumentNumber, base.RetirementDate';

    -- Ejecutar la consulta dinámica
    EXEC sp_executesql @SQL, 
        N'@InitialDate DATE, @EndDate DATE, @EmployeeId INT', 
        @InitialDate, @EndDate, @EmployeeId;

END;