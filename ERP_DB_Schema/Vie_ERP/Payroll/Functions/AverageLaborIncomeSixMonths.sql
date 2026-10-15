
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