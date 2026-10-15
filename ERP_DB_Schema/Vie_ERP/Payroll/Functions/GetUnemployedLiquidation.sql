

-- =============================================
-- Author:		Juan Pablo Daza Medina
-- Create date: 2022-03-08
-- Description:	Valor de Cesantias 

-- =============================================
CREATE FUNCTION [Payroll].[GetUnemployedLiquidation]
(	
	@Year Integer,
	@EmployeeId Integer
)
RETURNS DECIMAL
AS
BEGIN
DECLARE @TotalUnemplpoyed DECIMAL(18,2)
DECLARE @SumUL DECIMAL(18,2) --Suma de cesantias
DECLARE @SumULC DECIMAL(18,2) --Suma de cesantias e intereses en la liquidacion de contrato

	 IF EXISTS (--Buscamos si existe liquidaciones de contratos en el año 
        SELECT 1 
        FROM Payroll.ContractLiquidation cl
        JOIN Payroll.ContractLiquidationDetail cld ON cl.id = cld.ContractLiquidationId
        WHERE cl.EmployeeId = @EmployeeId 
            AND cld.IdConcept IN (29, 30) -- Cesantías e intereses
            AND YEAR(cl.RetirementDate) = @Year
            AND cl.Status = 'C'
    )
    BEGIN
        -- Si existe, calcular la suma
        SELECT @SumULC = ISNULL(SUM(cld.Accrued), 0)
        FROM Payroll.ContractLiquidation cl
        JOIN Payroll.ContractLiquidationDetail cld ON cl.id = cld.ContractLiquidationId
        WHERE cl.EmployeeId = @EmployeeId 
            AND cld.IdConcept IN (29, 30) 
            AND YEAR(cl.RetirementDate) = @Year
            AND cl.Status = 'C';
    END
    ELSE
    BEGIN
        -- Si no existe, establecer la suma como 0
        SET @SumULC = 0;
    END
	--Se busca
    SELECT @SumUL= isnull(SUM(ul.TotalUnemployed + ul.UnemployedInterestTotal),0)
			FROM Payroll.UnemployedLiquidation ul with(nolock)	
			WHERE ul.Year = @Year
				AND ul.EmployeeId = @EmployeeId
				AND ul.Status = 1

	SELECT @TotalUnemplpoyed = @SumUL + @SumULC

    RETURN @TotalUnemplpoyed;

END