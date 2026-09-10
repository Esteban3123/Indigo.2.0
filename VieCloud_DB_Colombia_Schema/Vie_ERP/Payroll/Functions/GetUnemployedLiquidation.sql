

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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula el valor total de cesantías e intereses de cesantías correspondientes a un empleado en un año específico, combinando dos fuentes: las cesantías incluidas en liquidaciones de contrato cerradas (conceptos 29 y 30 de ContractLiquidationDetail cuando el contrato tiene estado ''C'' y la fecha de retiro cae en el año indicado) y las cesantías registradas en la tabla de liquidaciones periódicas de cesantías (UnemployedLiquidation, estado activo). Suma ambos montos y retorna el total como un valor decimal, lo que permite conocer el monto acumulado de cesantías e intereses que la empresa debe certificar o reportar por un trabajador en un año dado, ya sea por retiro o por liquidación anual ordinaria.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'FUNCTION', @level1name = N'GetUnemployedLiquidation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'FUNCTION', @level1name = N'GetUnemployedLiquidation';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula el valor total de cesantías de un empleado en un año, sumando las cesantías liquidadas del periodo y, si aplica, las cesantías e intereses incluidos en la liquidación definitiva del contrato.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'GetUnemployedLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El año y el identificador de empleado deben corresponder a registros existentes para obtener un valor distinto de cero.; Las liquidaciones de contrato consideradas deben estar en estado ''C'' (cerradas/confirmadas).; Las liquidaciones de cesantías consideradas deben tener Status = 1 (activas/vigentes).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'GetUnemployedLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los conceptos 29 y 30 representan cesantías e intereses de cesantías dentro del detalle de liquidación de contrato.; Solo se consideran liquidaciones de contrato con Status=''C'' (las demás se ignoran).; Solo se consideran liquidaciones de cesantías con Status=1.; Los valores nulos en las sumas se tratan como 0 mediante ISNULL.; El total nunca incluye cesantías de contratos cuyo año de retiro no coincide con @Year.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'GetUnemployedLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cesantías; Intereses de cesantías; Liquidación de contrato; Liquidación de cesantías; Empleado; Retiro laboral; Devengado', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'GetUnemployedLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Retorna la suma de TotalUnemployed + UnemployedInterestTotal de Payroll.UnemployedLiquidation (Status=1, año y empleado dados) más la suma de Accrued de Payroll.ContractLiquidationDetail con IdConcept IN (29,30) cuando existe liquidación de contrato cerrada (Status=''C'') con YEAR(RetirementDate)=@Year.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'GetUnemployedLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe al menos una Payroll.ContractLiquidation con Status=''C'', YEAR(RetirementDate)=@Year, y detalle con IdConcept IN (29,30) para el empleado → Asigna a @SumULC la suma de Accrued de esos detalles (cesantías e intereses de la liquidación de contrato) else Asigna @SumULC = 0 (no se incorporan cesantías de liquidación de contrato)', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'GetUnemployedLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.ContractLiquidation; Payroll.ContractLiquidationDetail; Payroll.UnemployedLiquidation', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'GetUnemployedLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'GetUnemployedLiquidation';
GO
