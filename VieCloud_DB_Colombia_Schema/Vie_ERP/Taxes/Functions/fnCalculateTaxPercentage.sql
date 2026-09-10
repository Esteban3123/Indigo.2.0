
create FUNCTION [Taxes].[fnCalculateTaxPercentage] (@Appraisal as decimal(18,0), @SMLV as decimal(18,0))
RETURNS decimal(5,2)
AS
BEGIN

--Porcentaje impuesto
declare @TaxPercentage decimal(5,2)

--Se calcula la comparación
declare @Compare decimal(18,0) = ROUND((@Appraisal / @SMLV), 0)

--Se realizan las comparaciones para establecer el porcentaje del impuesto y el valor del impuesto
set @TaxPercentage = 0

--Avaluo mayor a 10 y hasta 50 salarios minimos
if @Compare > 10 and @Compare <= 50
Begin
	set @TaxPercentage = 4.2
End

--Avaluo mayores a 50 y hasta 90 salarios minimos
if @Compare > 50 and @Compare <= 90
Begin
	set @TaxPercentage = 5.2
End

--Avaluo mayores a 90 y hasta 130 salarios minimos
if @Compare > 90 and @Compare <= 130
Begin
	set @TaxPercentage = 6.2
End

--Avaluo mayores a 130 y hasta 170 salarios minimos
if @Compare > 130 and @Compare <= 170
Begin
	set @TaxPercentage = 7.5
End

--Avaluo mayores a 170 y hasta 250 salarios minimos
if @Compare > 170 and @Compare <= 250
Begin
	set @TaxPercentage = 8.5
End

--Avaluo mayores a 250 y hasta 350 salarios minimos
if @Compare > 250 and @Compare <= 350
Begin
	set @TaxPercentage = 9.5
End

--Avaluo mayores a 350 y hasta 450 salarios minimos
if @Compare > 350 and @Compare <= 450
Begin
	set @TaxPercentage = 10.5
End

--Avaluo mayores a 450
if @Compare > 450
Begin
	set @TaxPercentage = 12
End

Return @TaxPercentage

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula el porcentaje de impuesto aplicable según el avalúo de un bien, expresado en múltiplos del Salario Mínimo Legal Vigente (SMLV). Recibe el valor del avalúo y el valor del SMLV, divide el primero entre el segundo y aplica una tabla de rangos progresivos: desde 0% (hasta 10 SMLV) hasta 12% (más de 450 SMLV), con tasas intermedias de 4.2%, 5.2%, 6.2%, 7.5%, 8.5%, 9.5% y 10.5%. Es utilizada en el módulo de impuestos (Taxes) para determinar la tarifa impositiva correspondiente a avalúos de bienes, siguiendo una escala progresiva definida por la normativa fiscal.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'FUNCTION', @level1name = N'fnCalculateTaxPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'FUNCTION', @level1name = N'fnCalculateTaxPercentage';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Determina el porcentaje de impuesto aplicable a un avalúo según el número de salarios mínimos legales vigentes (SMLV) que representa, usando una escala progresiva por tramos.', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'FUNCTION', @level1name=N'fnCalculateTaxPercentage';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El divisor SMLV debe ser distinto de cero para evitar división por cero.; El avalúo y el SMLV deben expresarse en la misma unidad monetaria para que la comparación sea válida.', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'FUNCTION', @level1name=N'fnCalculateTaxPercentage';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El porcentaje retornado siempre pertenece al conjunto discreto {0, 4.2, 5.2, 6.2, 7.5, 8.5, 9.5, 10.5, 12}.; La escala de porcentajes es estrictamente creciente con respecto al número de SMLV del avalúo.; Avalúos de hasta 10 SMLV están exentos (porcentaje 0).; La comparación se hace sobre el cociente Appraisal/SMLV redondeado a entero, no sobre el cociente exacto.; Los tramos son excluyentes: el límite inferior es exclusivo y el superior inclusivo.', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'FUNCTION', @level1name=N'fnCalculateTaxPercentage';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Avalúo; Salario Mínimo Legal Vigente (SMLV); Impuesto; Porcentaje de impuesto; Escala tributaria progresiva por tramos', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'FUNCTION', @level1name=N'fnCalculateTaxPercentage';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (scalar return): Retorna 0 cuando el avalúo equivale a 10 o menos SMLV (no entra en ningún tramo).; [RETURN_RESULT] (scalar return): Cuando ROUND(Appraisal/SMLV) está en (10, 50] retorna 4.2%.; [RETURN_RESULT] (scalar return): Cuando ROUND(Appraisal/SMLV) está en (50, 90] retorna 5.2%.; [RETURN_RESULT] (scalar return): Cuando ROUND(Appraisal/SMLV) está en (90, 130] retorna 6.2%.; [RETURN_RESULT] (scalar return): Cuando ROUND(Appraisal/SMLV) está en (130, 170] retorna 7.5%.; [RETURN_RESULT] (scalar return): Cuando ROUND(Appraisal/SMLV) está en (170, 250] retorna 8.5%.; [RETURN_RESULT] (scalar return): Cuando ROUND(Appraisal/SMLV) está en (250, 350] retorna 9.5%.; [RETURN_RESULT] (scalar return): Cuando ROUND(Appraisal/SMLV) está en (350, 450] retorna 10.5%.; [RETURN_RESULT] (scalar return): Cuando ROUND(Appraisal/SMLV) es mayor a 450 retorna 12%.', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'FUNCTION', @level1name=N'fnCalculateTaxPercentage';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ROUND(Appraisal/SMLV) > 10 AND <= 50 → Porcentaje = 4.2; si ROUND(Appraisal/SMLV) > 50 AND <= 90 → Porcentaje = 5.2; si ROUND(Appraisal/SMLV) > 90 AND <= 130 → Porcentaje = 6.2; si ROUND(Appraisal/SMLV) > 130 AND <= 170 → Porcentaje = 7.5; si ROUND(Appraisal/SMLV) > 170 AND <= 250 → Porcentaje = 8.5; si ROUND(Appraisal/SMLV) > 250 AND <= 350 → Porcentaje = 9.5; si ROUND(Appraisal/SMLV) > 350 AND <= 450 → Porcentaje = 10.5; si ROUND(Appraisal/SMLV) > 450 → Porcentaje = 12; si ROUND(Appraisal/SMLV) <= 10 → Porcentaje = 0 (valor por defecto)', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'FUNCTION', @level1name=N'fnCalculateTaxPercentage';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'FUNCTION', @level1name=N'fnCalculateTaxPercentage';
GO
