-- =============================================
-- Author:      Oscar Stiven Astudillo
-- Create date: 2025-02-08
-- Description: Reporte de liquidación de contratos con conceptos dinámicos
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

    -- Construir la consulta SQL
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

    EXEC sp_executesql @SQL, 
        N'@InitialDate DATE, @EndDate DATE, @EmployeeId INT', 
        @InitialDate, @EndDate, @EmployeeId;

END;
GO
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte detallado de liquidaciones de contratos laborales con conceptos dinámicos de nómina. Para un rango de fechas de retiro y opcionalmente un empleado específico, consolida la información del empleado (documento, nombre, cargo, unidad funcional, salario base, grupo), los datos de retiro (fecha, motivo, días trabajados, número y fecha de resolución) y los totales de la liquidación (devengado, deducido, neto a pagar). La columna de conceptos de nómina es dinámica: construye en tiempo de ejecución una columna pivote por cada concepto devengado (ConceptType=1) y otra por cada concepto deducido (ConceptType=2) que haya tenido movimiento en el período, tomando código y nombre desde el catálogo Payroll.Concept. Consume Payroll.ContractLiquidation, Payroll.ContractLiquidationDetail, Payroll.Concept, Payroll.Employee, Payroll.Contract, Payroll.Position, Payroll.FunctionalUnit y Payroll.RetirementReason. Se usa para reportería de recursos humanos y nómina sobre la liquidación definitiva de empleados al momento del retiro.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_ContractLiquidationDetailReport';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_ContractLiquidationDetailReport';

Existencia de conceptos activos (State=1) en Payroll.Concept para que aparezcan como columnas dinámicas.; Las liquidaciones deben tener empleado, contrato, grupo, cargo, unidad funcional, centro de costo, motivo de retiro y tercero asociados (joins INNER).  |  Invariantes: El rango de fechas se evalúa como [@InitialDate, @EndDate+1) mediante DATEADD(DAY,1,@EndDate), incluyendo todo el día @EndDate.; Solo se consideran conceptos con State = 1 (activos) tanto para columnas dinámicas como para el detalle.; Solo se generan columnas dinámicas para conceptos que efectivamente tengan movimientos (Accrued>0 o Deducted>0) en el rango filtrado.; TotalDaysWorked se calcula como DATEDIFF(DAY, cct.JobBondingDate, cl.RetirementDate).; NetPay corresponde a cl.TotalPaid.; El reporte se ordena por DocumentNumber y RetirementDate.; Los nombres de concepto sanitizan comillas simples y dobles espacios al construir alias de columna.  |  Conceptos de dominio: Liquidación de contrato; Conceptos devengados; Conceptos deducidos; Motivo de retiro; Salario básico; Fecha de vinculación laboral; Fecha de retiro; Días trabajados; Resolución de liquidación; Centro de costo; Unidad funcional; Cargo; Grupo de nómina; Neto a pagar  |  Side effects: [RETURN_RESULT] N/A: Devuelve un resultset con una fila por liquidación de contrato cuya RetirementDate esté en [@InitialDate, @EndDate], filtrada opcionalmente por @EmployeeId, con columnas dinámicas por cada concepto devengado y deducido.  |  Decisiones: si @EmployeeId IS NULL → No se filtra por empleado; se incluyen todas las liquidaciones del rango. else Se filtran solo liquidaciones donde cl.EmployeeId = @EmployeeId.; si Concept.ConceptType = 1 con detalles cuyo Accrued > 0 en el rango → Se genera una columna dinámica de devengado [Code - Name] con MAX(Accrued).; si Concept.ConceptType = 2 con detalles cuyo Deducted > 0 en el rango → Se genera una columna dinámica de deducido [Code - Name] con MAX(Deducted).; si cl.Status = ''C'' → StatusDescription = ''Confirmado''. else StatusDescription = ''Sin Confirmar''.; si @ColsAccrued vacío o NULL → No se concatenan columnas dinámicas de devengados al SELECT final.; si @ColsDeducted vacío o NULL → No se concatenan columnas dinámicas de deducidos al SELECT final.  |  Llama a: sys.sp_executesql  |  Consulta: Payroll.Concept, Payroll.ContractLiquidation, Payroll.ContractLiquidationDetail, Payroll.Employee, Common.ThirdParty, Payroll.Contract, Payroll.Group, Payroll.Position, Payroll.FunctionalUnit, Payroll.CostCenter, Payroll.RetirementReason, Payroll.Bank', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ContractLiquidationDetailReport';

-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte detallado de liquidaciones de contratos laborales en un rango de fechas de retiro, pivotando dinámicamente los conceptos devengados y deducidos como columnas individuales.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ContractLiquidationDetailReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@InitialDate y @EndDate deben estar definidos para acotar el rango de RetirementDate.; Existencia de conceptos activos (State=1) en Payroll.Concept para que aparezcan como columnas dinámicas.; Las liquidaciones deben tener empleado, contrato, grupo, cargo, unidad funcional, centro de costo, motivo de retiro y tercero asociados (joins INNER).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ContractLiquidationDetailReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El rango de fechas se evalúa como [@InitialDate, @EndDate+1) mediante DATEADD(DAY,1,@EndDate), incluyendo todo el día @EndDate.; Solo se consideran conceptos con State = 1 (activos) tanto para columnas dinámicas como para el detalle.; Solo se generan columnas dinámicas para conceptos que efectivamente tengan movimientos (Accrued>0 o Deducted>0) en el rango filtrado.; TotalDaysWorked se calcula como DATEDIFF(DAY, cct.JobBondingDate, cl.RetirementDate).; NetPay corresponde a cl.TotalPaid.; El reporte se ordena por DocumentNumber y RetirementDate.; Los nombres de concepto sanitizan comillas simples y dobles espacios al construir alias de columna.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ContractLiquidationDetailReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Liquidación de contrato; Conceptos devengados; Conceptos deducidos; Motivo de retiro; Salario básico; Fecha de vinculación laboral; Fecha de retiro; Días trabajados; Resolución de liquidación; Centro de costo; Unidad funcional; Cargo; Grupo de nómina; Neto a pagar', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ContractLiquidationDetailReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve un resultset con una fila por liquidación de contrato cuya RetirementDate esté en [@InitialDate, @EndDate], filtrada opcionalmente por @EmployeeId, con columnas dinámicas por cada concepto devengado y deducido.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ContractLiquidationDetailReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @EmployeeId IS NULL → No se filtra por empleado; se incluyen todas las liquidaciones del rango. else Se filtran solo liquidaciones donde cl.EmployeeId = @EmployeeId.; si Concept.ConceptType = 1 con detalles cuyo Accrued > 0 en el rango → Se genera una columna dinámica de devengado [Code - Name] con MAX(Accrued).; si Concept.ConceptType = 2 con detalles cuyo Deducted > 0 en el rango → Se genera una columna dinámica de deducido [Code - Name] con MAX(Deducted).; si cl.Status = ''C'' → StatusDescription = ''Confirmado''. else StatusDescription = ''Sin Confirmar''.; si @ColsAccrued vacío o NULL → No se concatenan columnas dinámicas de devengados al SELECT final.; si @ColsDeducted vacío o NULL → No se concatenan columnas dinámicas de deducidos al SELECT final.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ContractLiquidationDetailReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'sys.sp_executesql', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ContractLiquidationDetailReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.Concept; Payroll.ContractLiquidation; Payroll.ContractLiquidationDetail; Payroll.Employee; Common.ThirdParty; Payroll.Contract; Payroll.Group; Payroll.Position; Payroll.FunctionalUnit; Payroll.CostCenter; Payroll.RetirementReason; Payroll.Bank', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ContractLiquidationDetailReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ContractLiquidationDetailReport';
-- GO
