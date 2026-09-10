
-- =============================================
-- Author:       Juan Pablo Daza Medina
-- Create date: 2024-05-22
-- Description:  Ingreso laboral promedio de los últimos seis meses anteriores (numeral 4 art. 206 E.T.)
-- =============================================
CREATE FUNCTION [Payroll].[AverageLaborIncomeSixMonths]
(
    @Year INTEGER,
    @EmployeeId INTEGER
)
RETURNS DECIMAL
AS
BEGIN
    DECLARE @SumD DECIMAL(18,2) -- Suma Devengados
    DECLARE @SumC DECIMAL(18,2) -- Suma de conceptos
    DECLARE @Result DECIMAL(18,2) -- Resultado final
    DECLARE @NumMonths INTEGER -- Número de meses trabajados 

    IF EXISTS (
        -- Buscamos si existen liquidaciones de contratos en el año 
        SELECT 1 
        FROM Payroll.ContractLiquidation cl
        JOIN Payroll.ContractLiquidationDetail cld ON cl.Id = cld.ContractLiquidationId
        WHERE cl.EmployeeId = @EmployeeId 
            AND YEAR(cl.RetirementDate) = @Year
            AND cl.Status = 'C'
    )
    BEGIN
        SET @NumMonths = (
            -- Obtenemos los meses trabajados por el último contrato del empleado
            SELECT Count(*)
            FROM Payroll.ContractLiquidation CL
            LEFT JOIN Payroll.Contract C ON CL.ContractId = C.Id
			LEFT JOIN Payroll.Liquidation L ON C.ID = L.ContractId
            WHERE C.EmployeeId = @EmployeeId AND YEAR(CL.RetirementDate) = @Year
			AND C.Valid = 0 AND L.RegisterStatus = 'C' AND YEAR(L.PayrollDateLiquidated)=@Year
        )
        SET @NumMonths = ISNULL(@NumMonths,1) --SI NO SE ENCUENTRAN LIQUIDACIONES
        DECLARE @Top INT = CASE WHEN  @NumMonths >= 6 THEN 6 ELSE @NumMonths END

        SELECT @SumD = ISNULL(SUM(ld.ConceptTotalValue), 0) 
        FROM Payroll.LiquidationDetail LD
        JOIN Payroll.Concept C ON LD.ConceptId = C.Id
        LEFT JOIN (
            SELECT TOP (@Top) * -- Se toma las últimas 6 liquidaciones confirmadas o las últimas @NumMonths si es menor a 6
            FROM Payroll.Liquidation 
            WHERE EmployeeId = @EmployeeId 
              AND YEAR(PayrollDateLiquidated) = @Year 
              AND RegisterStatus = 'C'  
            ORDER BY PayrollDateLiquidated
        ) AS LastLiquidations ON LD.PayrollId = LastLiquidations.Id
        WHERE LastLiquidations.EmployeeId = @EmployeeId 
          AND C.AffectIBCSeverance = 1  

        SET @Result = @SumD / @Top
    END
    -- Validamos si el contrato sigue vigente para la fecha 2023-12-31 o mayor
    ELSE IF (
        SELECT 1  
        FROM Payroll.Contract 
        WHERE EmployeeId = @EmployeeId  
          AND Valid = 1 
          AND ContractEndingDate >= '2023-12-31' -- Según el PBI 15560 se deja esta fecha porque va para el reporte del 2023
    ) = 1
    BEGIN
        SELECT @SumC = ISNULL(SUM(ld.ConceptTotalValue), 0) 
        FROM Payroll.LiquidationDetail LD
        JOIN Payroll.Concept C ON LD.ConceptId = C.Id
        LEFT JOIN (
            SELECT TOP 6 * 
            FROM Payroll.Liquidation 
            WHERE EmployeeId = @EmployeeId 
              AND YEAR(PayrollDateLiquidated) = @Year 
              AND RegisterStatus = 'C'  
            ORDER BY PayrollDateLiquidated
        ) AS P ON LD.PayrollId = P.Id
        WHERE P.EmployeeId = @EmployeeId 
          AND C.AffectIBCSeverance = 1  
        
        SET @Result = @SumC / 6 --Se divide por las ultimas 6 liquidaciones
    END

    RETURN @Result
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula el ingreso laboral promedio de los últimos seis meses anteriores al año indicado para un empleado específico, según el numeral 4 del artículo 206 del Estatuto Tributario colombiano. Si el empleado fue retirado en el año consultado, toma las liquidaciones de nómina confirmadas del período (hasta un máximo de 6 o los meses efectivamente trabajados) y promedia los valores de los conceptos que afectan la base de cotización para cesantías (IBC cesantías). Si el contrato sigue vigente, siempre divide la sumatoria entre 6 meses. Este cálculo se utiliza en el reporte de retención en la fuente sobre pagos laborales (formulario de exógena o certificado de ingresos), apoyándose en las tablas de liquidaciones de nómina, detalles de conceptos, contratos laborales y liquidaciones de retiro del empleado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'FUNCTION', @level1name = N'AverageLaborIncomeSixMonths';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'FUNCTION', @level1name = N'AverageLaborIncomeSixMonths';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula el ingreso laboral promedio mensual de los últimos seis meses de un empleado en un año dado, base para la exención del numeral 4 art. 206 E.T., considerando si hay liquidación definitiva de contrato o contrato vigente.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'AverageLaborIncomeSixMonths';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir información de liquidaciones de nómina (Payroll.Liquidation) con RegisterStatus=''C'' para el empleado y año.; Los conceptos involucrados deben tener marcado AffectIBCSeverance = 1 en Payroll.Concept para sumar al promedio.; Si se evalúa la rama de contrato vigente, debe existir un contrato con Valid=1 y ContractEndingDate >= ''2023-12-31''.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'AverageLaborIncomeSixMonths';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran liquidaciones con RegisterStatus=''C'' (confirmadas) del año solicitado.; Solo conceptos con AffectIBCSeverance=1 contribuyen al promedio.; El divisor nunca excede 6 meses, alineado al numeral 4 art. 206 E.T.; Si no se encuentran meses trabajados, @NumMonths se asume 1 (ISNULL).; La fecha de corte ''2023-12-31'' está fija por requerimiento PBI 15560 para reporte 2023.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'AverageLaborIncomeSixMonths';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ingreso laboral promedio; Cesantías (IBC Severance); Liquidación de contrato; Liquidación de nómina; Exención numeral 4 art. 206 E.T.; Contrato vigente; Retiro del empleado', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'AverageLaborIncomeSixMonths';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Si existen liquidaciones de contrato (ContractLiquidation con Status=''C'') en el año, retorna SUM(ConceptTotalValue) de las últimas N liquidaciones confirmadas dividido por N (N = min(meses trabajados, 6)).; [RETURN_RESULT] : Si no hay liquidación de contrato pero el contrato sigue vigente (Valid=1 y ContractEndingDate>=''2023-12-31''), retorna SUM(ConceptTotalValue) de las últimas 6 liquidaciones confirmadas dividido por 6.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'AverageLaborIncomeSixMonths';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si EXISTS liquidación de contrato (ContractLiquidation) del empleado en el año con Status=''C'' → Calcula meses trabajados (N) contando liquidaciones del último contrato (Valid=0, RegisterStatus=''C'', YEAR(PayrollDateLiquidated)=@Year); toma TOP min(N,6) liquidaciones y divide la suma de conceptos que afectan IBC de cesantías por ese tope. else Si el contrato está vigente (Valid=1 y ContractEndingDate>=''2023-12-31''), suma los conceptos AffectIBCSeverance=1 de las últimas 6 liquidaciones confirmadas del año y divide entre 6.; si @NumMonths >= 6 → Usa tope de 6 liquidaciones como divisor. else Usa @NumMonths como tope y divisor.; si Concept.AffectIBCSeverance = 1 → El concepto se incluye en la sumatoria del ingreso promedio. else El concepto se excluye del cálculo.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'AverageLaborIncomeSixMonths';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.ContractLiquidation; Payroll.ContractLiquidationDetail; Payroll.Contract; Payroll.Liquidation; Payroll.LiquidationDetail; Payroll.Concept', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'AverageLaborIncomeSixMonths';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'AverageLaborIncomeSixMonths';
GO
