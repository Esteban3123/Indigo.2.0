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
            pho_movil2.Phone AS MobilePhone,
            pho_fijo2.Phone AS LandlinePhone,
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
            part1.GrossSalary
            
        FROM Payroll.Employee emp WITH (NOLOCK)
        INNER JOIN Common.ThirdParty ter WITH (NOLOCK) ON ter.Id = emp.ThirdPartyId
        INNER JOIN Common.Person per WITH (NOLOCK) ON per.Id = ter.PersonId
        INNER JOIN Payroll.[Contract] cct WITH (NOLOCK) ON cct.EmployeeId = emp.Id
        INNER JOIN Payroll.Position car WITH (NOLOCK) ON car.Id = cct.PositionId
        INNER JOIN Payroll.FunctionalUnit uf WITH (NOLOCK) ON uf.Id = cct.FunctionalUnitId
        INNER JOIN Payroll.CostCenter cc WITH (NOLOCK) ON cc.Id = emp.CostCenterId
        INNER JOIN Payroll.Liquidation liq WITH (NOLOCK) ON liq.ContractId = cct.Id
        INNER JOIN Payroll.[Group] gru WITH (NOLOCK) ON gru.Id = liq.GroupId
         LEFT JOIN (
			SELECT
				Id AS LiquidationId,
				ISNULL(PayrollDays, 0) AS DiasNomina,
				ISNULL(DaysWorked, 0) AS DiasTrabaj,
				ISNULL(VacationDays, 0) AS DiasVacac,
				ISNULL(DisabilityDays, 0) AS DiasIncap,
				ISNULL(UnpaidLicenseDays, 0) AS DiasPermisoSinGoce,
				ISNULL(LicenseDays, 0) AS DiasPermisoConGoce,
				TotalAccrued,
				TotalDeducted,
				HealthJCB AS GrossSalary,
				YEAR(PayrollDateLiquidated) AS Ano,
                MONTH(PayrollDateLiquidated) AS Mes,
                DAY(PayrollDateLiquidated) AS Dia
			FROM Payroll.Liquidation WITH (NOLOCK)
			WHERE PayrollDateLiquidated >= @InitialDate
			  AND PayrollDateLiquidated < DATEADD(DAY, 1, @EndDate)
			  AND (@EmployeeId IS NULL OR EmployeeId = @EmployeeId)
			  AND (@RegisterStatus IS NULL OR @RegisterStatus = ''T'' OR RegisterStatus = @RegisterStatus)
		) part1 ON part1.LiquidationId = liq.Id
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
        LEFT JOIN (
			SELECT
				MAX(Id) AS Item,
				IdPerson
			FROM Common.Phone WITH (NOLOCK)
			WHERE State = 1 AND IdPhoneType = 1  -- Móvil activo
			GROUP BY IdPerson
		) pho_movil ON pho_movil.IdPerson = per.Id
		LEFT JOIN Common.Phone pho_movil2 WITH (NOLOCK) ON pho_movil2.Id = pho_movil.Item
		LEFT JOIN (
			SELECT
				MAX(Id) AS Item,
				IdPerson
			FROM Common.Phone WITH (NOLOCK)
			WHERE State = 1 AND IdPhoneType = 2  -- Fijo activo
			GROUP BY IdPerson
		) pho_fijo ON pho_fijo.IdPerson = per.Id
		LEFT JOIN Common.Phone pho_fijo2 WITH (NOLOCK) ON pho_fijo2.Id = pho_fijo.Item
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
        INNER JOIN Payroll.[Group] gru WITH (NOLOCK) ON gru.Id = liq.GroupId
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
        base.MobilePhone,
        base.LandlinePhone,
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
		base.GrossSalary,
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
        base.TotalAccrued, base.TotalDeducted, base.NetPay, base.GrossSalary, base.LiquidationId, base.MobilePhone, base.LandlinePhone
    ORDER BY base.DocumentNumber, base.LiquidationDate';

    -- Ejecutar la consulta
    EXEC sp_executesql @SQL, N'@InitialDate DATE, @EndDate DATE, @EmployeeId INT, @GroupInitial INT, @GroupFinal INT, @BranchOfficeInitial INT, @BranchOfficeFinal INT, @RegisterStatus CHAR(1)', 
        @InitialDate, @EndDate, @EmployeeId, @GroupInitial, @GroupFinal, @BranchOfficeInitial, @BranchOfficeFinal, @RegisterStatus;

END;
GO
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte detallado de liquidación de nómina por empleado, mostrando en columnas dinámicas todos los conceptos devengados (salarios, horas extras, bonificaciones), deducciones (salud, pensión, embargos) y aportes patronales vigentes, tomados del catálogo Payroll.Concept según su tipo. Para cada empleado liquidado en el rango de fechas indicado muestra información personal (documento, nombre, género, correo, fecha de nacimiento), datos laborales (cargo, unidad funcional, centro de costo, grupo, fecha de contratación, salario básico), afiliaciones a salud y pensión, cuenta bancaria, días trabajados, días de vacaciones, incapacidades y permisos, y los totales de devengado, deducido y neto a pagar. Permite filtrar por empleado, rango de grupos, rango de sucursales y estado del registro; construye el SQL de manera dinámica (pivot de conceptos) usando sp_executesql, por lo que las columnas del resultado varían según los conceptos activos en el catálogo. Sirve como insumo para auditoría de nómina, control de pagos y conciliación de seguridad social.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_LiquidationDetailReport';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_LiquidationDetailReport';


-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte tabular detallado de liquidaciones de nómina por período, pivotando dinámicamente como columnas los conceptos activos (devengados, deducidos, patronales y horas) del catálogo.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_LiquidationDetailReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@InitialDate y @EndDate definen el rango cerrado-abierto [InitialDate, EndDate+1) sobre Liquidation.PayrollDateLiquidated; El catálogo Payroll.Concept debe tener registros con State = 1 para que se generen columnas dinámicas; de lo contrario las secciones pivote quedan vacías; Para filtrar por estado de registro se espera @RegisterStatus = ''T'' (todos) o un valor que coincida con Liquidation.RegisterStatus; NULL equivale a no filtrar', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_LiquidationDetailReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se pivotean conceptos con State = 1 en Payroll.Concept; Solo se consideran filas de LiquidationDetail con ConceptTotalValue <> 0 al construir el CTE de conceptos; El rango de fechas se aplica como semiabierto: PayrollDateLiquidated >= @InitialDate AND < DATEADD(DAY,1,@EndDate); NetPay siempre se calcula como TotalAccrued - TotalDeducted de la liquidación; Los días de nómina/trabajados/vacaciones/incapacidad/permisos se devuelven como 0 cuando son NULL (ISNULL); Los teléfonos se filtran por State=1 y se toma el de mayor Id por persona: IdPhoneType=1 para móvil y IdPhoneType=2 para fijo; Para email se toma el registro con MAX(Id) por persona, sin filtro de estado; Los rangos de grupo y sucursal se aplican inclusivos en ambos extremos cuando los parámetros no son NULL', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_LiquidationDetailReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Liquidación de nómina; Conceptos devengados; Conceptos deducidos; Conceptos patronales; Provisiones laborales; Horas extras y recargos; IBC de salud; Fondo de salud (EPS); Fondo de pensión (AFP); Riesgo profesional (ARL); Centro de costo; Unidad funcional; Grupo de nómina; Sucursal; Días trabajados / vacaciones / incapacidad / permisos con y sin goce; Salario básico; Cuenta bancaria de pago; Neto a pagar', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_LiquidationDetailReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve un único conjunto de resultados con columnas fijas de empleado/contrato/liquidación más columnas dinámicas por cada concepto activo, ordenado por DocumentNumber y LiquidationDate', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_LiquidationDetailReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Payroll.Concept.ConceptType = 1 AND ConceptClass NOT IN (''001'',''012'',''013'',''042'',''043'',''050'',''051'',''052'') → El concepto se incluye como columna de DEVENGADO sumando ConceptTotalValue; si Payroll.Concept.ConceptType = 1 AND ConceptClass IN (''001'',''012'',''013'',''042'',''043'',''050'',''051'',''052'') → El concepto se incluye como par de columnas de HORAS: una con ConceptQuantity (CANTIDAD) y otra con ConceptTotalValue; si Payroll.Concept.ConceptType = 2 → El concepto se incluye como columna de DEDUCIDO sumando ConceptTotalValue; si Payroll.Concept.ConceptType = 3 → El concepto se incluye como columna PATRONAL; los de ConceptClass IN (''008'',''034'',''033'',''031'') se ordenan al final por considerarse provisiones; si per.Gender = 1 / 2 / 3 → Se traduce a ''M'', ''F'' u ''Otro'' respectivamente; cualquier otro valor se devuelve como cadena vacía; si lde.Quantity = 0 o NULL → Se usa lde.TotalNumberHours como cantidad del concepto (COALESCE(NULLIF(Quantity,0), TotalNumberHours, 0)) else Se usa lde.Quantity; si @RegisterStatus IS NULL OR @RegisterStatus = ''T'' → No se filtra por RegisterStatus (incluye todas las liquidaciones del rango) else Solo se incluyen liquidaciones cuyo RegisterStatus coincida con el parámetro', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_LiquidationDetailReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'sys.sp_executesql', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_LiquidationDetailReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.Concept; Payroll.Employee; Common.ThirdParty; Common.Person; Payroll.Contract; Payroll.Position; Payroll.FunctionalUnit; Payroll.CostCenter; Payroll.Liquidation; Payroll.Group; Payroll.Fund; Payroll.Bank; Common.Email; Common.Phone; Payroll.LiquidationDetail', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_LiquidationDetailReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_LiquidationDetailReport';
-- GO
