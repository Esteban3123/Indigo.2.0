CREATE FUNCTION [Payroll].[fnCalculateSolidarityFundSubsistence] (@LegalMinimunSalary as decimal(18,0), @BasicSalary as decimal(18,0), @IBCPension decimal (18,0))
RETURNS decimal(18,0)
AS
BEGIN

--Porcentaje impuesto
declare @SolidarityFundSubsitence decimal(18,0)

SET @SolidarityFundSubsitence = 0

IF @IBCPension >= (@LegalMinimunSalary * 16) AND @IBCPension <= (@LegalMinimunSalary * 17) BEGIN
	SET @SolidarityFundSubsitence = @IBCPension * (0.012 - 0.005)
END

IF @IBCPension > (@LegalMinimunSalary * 17) AND @IBCPension <= (@LegalMinimunSalary * 18) BEGIN
	SET @SolidarityFundSubsitence = @IBCPension * (0.014 - 0.005)
END

IF @IBCPension > (@LegalMinimunSalary * 18) AND @IBCPension <= (@LegalMinimunSalary * 19) BEGIN
	SET @SolidarityFundSubsitence = @IBCPension * (0.016 - 0.005)
END

IF @IBCPension > (@LegalMinimunSalary * 19) AND @IBCPension <= (@LegalMinimunSalary * 20) BEGIN
	SET @SolidarityFundSubsitence = @IBCPension * (0.020 - 0.005)
END

IF @IBCPension > (@LegalMinimunSalary * 20) BEGIN
	SET @SolidarityFundSubsitence = @IBCPension * (0.01 - 0.005)
END

If @BasicSalary > (@LegalMinimunSalary * 25)  BEGIN
	SET @SolidarityFundSubsitence = (@LegalMinimunSalary * 25) * 0.015
END

Return @SolidarityFundSubsitence

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula el aporte al Fondo de Solidaridad Pensional en su subcuenta de Subsistencia para un empleado en nómina, de acuerdo con las tarifas progresivas establecidas por la ley colombiana. Recibe el Ingreso Base de Cotización (IBC) para pensión, el salario básico del trabajador y el salario mínimo legal vigente (SMLV), y aplica el porcentaje de cotización adicional según el rango de salarios mínimos que corresponda (entre 16 y más de 20 SMLV). Si el salario básico supera 25 SMLV, aplica una tarifa fija del 1.5% sobre ese tope. El resultado es el valor monetario que debe descontarse al empleado como contribución solidaria a la subcuenta de Subsistencia del Fondo de Solidaridad Pensional.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'FUNCTION', @level1name = N'fnCalculateSolidarityFundSubsistence';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'FUNCTION', @level1name = N'fnCalculateSolidarityFundSubsistence';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula el aporte al Fondo de Solidaridad Pensional - Subcuenta de Subsistencia, aplicando tarifas progresivas según el IBC pensional medido en múltiplos del salario mínimo legal.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'fnCalculateSolidarityFundSubsistence';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El salario mínimo legal vigente debe ser mayor que cero para que las comparaciones por múltiplos sean significativas.; El IBC de pensión debe estar expresado en la misma unidad monetaria que el salario mínimo legal.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'fnCalculateSolidarityFundSubsistence';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El aporte por defecto es 0 cuando el IBC pensional es inferior a 16 SMLMV y el salario básico no excede 25 SMLMV.; La tasa efectiva siempre se obtiene restando 0.005 a la tarifa nominal del rango (descuento fijo de 0.5 puntos porcentuales).; El tope por salario básico mayor a 25 SMLMV prevalece sobre cualquier cálculo previo basado en IBC, ya que se evalúa al final.; Los rangos por IBC son mutuamente excluyentes y cubren a partir de 16 SMLMV.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'fnCalculateSolidarityFundSubsistence';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Fondo de Solidaridad Pensional; Subcuenta de Subsistencia; IBC de pensión; Salario mínimo legal mensual vigente (SMLMV); Salario básico; Aporte parafiscal pensional', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'fnCalculateSolidarityFundSubsistence';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (retorno escalar): Si IBCPension está entre 16 y 17 SMLMV (inclusive), retorna IBCPension * (0.012 - 0.005).; [RETURN_RESULT] (retorno escalar): Si IBCPension está entre >17 y 18 SMLMV, retorna IBCPension * (0.014 - 0.005).; [RETURN_RESULT] (retorno escalar): Si IBCPension está entre >18 y 19 SMLMV, retorna IBCPension * (0.016 - 0.005).; [RETURN_RESULT] (retorno escalar): Si IBCPension está entre >19 y 20 SMLMV, retorna IBCPension * (0.020 - 0.005).; [RETURN_RESULT] (retorno escalar): Si IBCPension supera 20 SMLMV, retorna IBCPension * (0.01 - 0.005).; [RETURN_RESULT] (retorno escalar): Si BasicSalary > 25 SMLMV, sobrescribe el resultado y retorna (LegalMinimunSalary * 25) * 0.015, independientemente del rango de IBC.; [RETURN_RESULT] (retorno escalar): Si IBCPension < 16 SMLMV y BasicSalary <= 25 SMLMV, retorna 0.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'fnCalculateSolidarityFundSubsistence';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @IBCPension >= 16*SMLMV AND <= 17*SMLMV → Aporte = IBC * 0.7%; si @IBCPension > 17*SMLMV AND <= 18*SMLMV → Aporte = IBC * 0.9%; si @IBCPension > 18*SMLMV AND <= 19*SMLMV → Aporte = IBC * 1.1%; si @IBCPension > 19*SMLMV AND <= 20*SMLMV → Aporte = IBC * 1.5%; si @IBCPension > 20*SMLMV → Aporte = IBC * 0.5%; si @BasicSalary > 25*SMLMV → El aporte se topa y se recalcula como 25*SMLMV * 1.5%, sustituyendo cualquier cálculo previo basado en IBC.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'fnCalculateSolidarityFundSubsistence';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'fnCalculateSolidarityFundSubsistence';
GO
