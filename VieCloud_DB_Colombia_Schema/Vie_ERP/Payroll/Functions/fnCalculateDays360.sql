CREATE FUNCTION [Payroll].[fnCalculateDays360] (@InitialDate Date, @EndDate Date)
RETURNS INT
AS
BEGIN

	declare @TotalDays integer
	declare @AuxiliarDays integer
 
	If Day(@EndDate) > 30 BEGIN
		SET @AuxiliarDays = 0
	END ELSE IF Day(@EndDate) = 28 And Month(@EndDate) = 2 BEGIN
		SET @AuxiliarDays = 2
	END ELSE IF Day(@EndDate) = 29 And Month(@EndDate) = 2 BEGIN
		SET @AuxiliarDays = 1
	END ELSE BEGIN
		SET @AuxiliarDays = 1
	END

	If Month(@EndDate) = 2 And Day(@EndDate) = 28 BEGIN
		SET @AuxiliarDays = 3
	END

	If Month(@EndDate) = 2 And Day(@EndDate) = 29 BEGIN
		SET @AuxiliarDays = 2
	END

	If Month(@InitialDate) = 2 And Day(@InitialDate) = 1 And Month(@EndDate) = 2 And Day(@EndDate) = 28 BEGIN
		SET @AuxiliarDays = 3
	END

	If Month(@InitialDate) = 2 And Day(@InitialDate) = 1 And Month(@EndDate) = 2 And Day(@EndDate) = 29 BEGIN
		SET @AuxiliarDays = 2
	END

	If Abs(Year(@InitialDate) - Year(@EndDate)) = 0 BEGIN
		SET @TotalDays = 30 - Day(@InitialDate) + (Abs(Month(@InitialDate) - Month(@EndDate)) - 1) * 30 + Day(@EndDate) + @AuxiliarDays
	END ELSE BEGIN
		SET @TotalDays = (Abs(Year(@EndDate) - Year(@InitialDate) - 1) * 360) + (360 - Month(@InitialDate) * 30) + ((Month(@EndDate) - 1) * 30 + Day(@EndDate)) + (30 - Day(@InitialDate)) + @AuxiliarDays
	END

	Return @TotalDays

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula la diferencia en días entre dos fechas usando la convención de año comercial de 360 días (cada mes equivale a 30 días), método estándar utilizado en liquidación de nómina y prestaciones sociales. Recibe una fecha inicial y una fecha final, y aplica ajustes especiales para los meses de febrero (28 y 29 días) y para días que superan el día 30, garantizando que el cálculo sea consistente con las reglas laborales colombianas. Se usa principalmente en el módulo de Nómina (Payroll) para calcular períodos de trabajo, cesantías, vacaciones, intereses y otras liquidaciones que requieren el conteo de días en base 360.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'FUNCTION', @level1name = N'fnCalculateDays360';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'FUNCTION', @level1name = N'fnCalculateDays360';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula la cantidad de días entre dos fechas usando la convención financiera 360 (meses de 30 días, año de 360), con ajustes especiales para fines de mes y febrero.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'fnCalculateDays360';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Ambas fechas deben ser válidas (tipo Date no nulo) para que las funciones Day/Month/Year operen sin error.; Se asume que @InitialDate es anterior o igual a @EndDate; en caso contrario el resultado puede ser inconsistente aunque se usa Abs sobre la diferencia de años.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'fnCalculateDays360';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Todo mes se considera de 30 días y todo año de 360 días (convención 30/360).; Cuando el día final supera 30 no se aplica corrección (@AuxiliarDays=0); en los demás casos se suma un ajuste de 1, 2 o 3 días según el tratamiento de febrero.; El cálculo siempre devuelve un entero (INT) representando días equivalentes en la base 360.; Las reglas de febrero-28 y febrero-29 sobrescriben las asignaciones previas de @AuxiliarDays, por lo que prevalecen sobre el bloque inicial IF/ELSE.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'fnCalculateDays360';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Nómina (schema Payroll); Cálculo de días base 360 (convención financiera/actuarial 30/360); Tratamiento especial de febrero (28/29 días)', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'fnCalculateDays360';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (scalar return): Si Year(@InitialDate)=Year(@EndDate): retorna 30 - Day(@InitialDate) + (|Month(@InitialDate)-Month(@EndDate)|-1)*30 + Day(@EndDate) + @AuxiliarDays.; [RETURN_RESULT] (scalar return): Si los años difieren: retorna |Year(@EndDate)-Year(@InitialDate)-1|*360 + (360 - Month(@InitialDate)*30) + ((Month(@EndDate)-1)*30 + Day(@EndDate)) + (30 - Day(@InitialDate)) + @AuxiliarDays.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'fnCalculateDays360';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Day(@EndDate) > 30 → @AuxiliarDays = 0 (no se aplica ajuste porque el día excede el tope de 30 de la convención).; si Day(@EndDate) = 28 AND Month(@EndDate) = 2 → Ajuste inicial @AuxiliarDays = 2, luego sobrescrito a 3 por la regla específica de febrero-28. else Para los demás casos no especiales, @AuxiliarDays = 1.; si Day(@EndDate) = 29 AND Month(@EndDate) = 2 → Ajuste inicial @AuxiliarDays = 1, luego sobrescrito a 2 por la regla específica de febrero-29.; si Month(@InitialDate)=2 AND Day(@InitialDate)=1 AND Month(@EndDate)=2 AND Day(@EndDate)=28 → @AuxiliarDays = 3 (ajuste especial cuando el rango cubre todo febrero no bisiesto).; si Month(@InitialDate)=2 AND Day(@InitialDate)=1 AND Month(@EndDate)=2 AND Day(@EndDate)=29 → @AuxiliarDays = 2 (ajuste especial cuando el rango cubre todo febrero bisiesto).; si Abs(Year(@InitialDate) - Year(@EndDate)) = 0 → Aplica fórmula de mismo año. else Aplica fórmula multi-año basada en 360 días por año.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'fnCalculateDays360';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'fnCalculateDays360';
GO
