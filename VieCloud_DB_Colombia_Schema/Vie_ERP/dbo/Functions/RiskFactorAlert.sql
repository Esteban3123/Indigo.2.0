
CREATE FUNCTION [dbo].[RiskFactorAlert] (@PatientCode as varchar(25), @AdmissionNumber as char(10), @Origin as int)
RETURNS bit
AS
BEGIN
--Origin -> 1 = Factores de riesgo 
--Origin -> 2 = Escalas

DECLARE @RiskFactorAlert BIT = 0;

    IF @Origin = 1
    BEGIN
        IF EXISTS (
            SELECT 1 
            FROM dbo.ReportRiskFactors 
            WHERE IPCODPACI = @PatientCode 
              AND Status = 1
        )
            SET @RiskFactorAlert = 1;
    END
    ELSE IF @Origin = 2
    BEGIN
        IF EXISTS (
            SELECT 1 
            FROM dbo.HCESCALAS 
            WHERE IPCODPACI = @PatientCode 
              AND NUMINGRES = @AdmissionNumber
        )
            SET @RiskFactorAlert = 1;
    END

/*
Función que me va a definir si un paciente tiene Factor de riesgo o Alerta
*/

    RETURN @RiskFactorAlert

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que determina si un paciente tiene alertas activas según dos orígenes posibles: factores de riesgo clínico o escalas de valoración aplicadas. Cuando el origen es 1 (factores de riesgo), consulta la tabla ReportRiskFactors verificando si el paciente tiene al menos un factor de riesgo activo (estado 1); cuando el origen es 2 (escalas), consulta HCESCALAS para verificar si existen escalas clínicas registradas para el paciente en un ingreso específico. Retorna un valor verdadero o falso (bit) que indica la presencia o ausencia de la alerta, útil para mostrar indicadores visuales de riesgo en la historia clínica o en pantallas de atención del paciente. Se usa típicamente para señalizar en tiempo real si un paciente —identificado por su cédula y número de ingreso— requiere atención especial por riesgo clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'RiskFactorAlert';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'RiskFactorAlert';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Determina si un paciente presenta una alerta clínica activa, ya sea por factores de riesgo reportados o por escalas de valoración registradas en un ingreso específico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'RiskFactorAlert';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe estar identificado por su código; Para origen=2 (escalas) se requiere un número de ingreso válido; El parámetro de origen debe ser 1 (factores de riesgo) o 2 (escalas)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'RiskFactorAlert';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El resultado siempre es 0 (falso) por defecto si no se cumple ninguna condición de existencia; Solo se consideran factores de riesgo con Status = 1 (activos); Para escalas, la coincidencia exige paciente e ingreso específicos; para factores de riesgo solo se exige el paciente; Si el origen no es 1 ni 2, la función retorna 0', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'RiskFactorAlert';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso/Admisión; Factor de riesgo; Escalas clínicas; Alerta clínica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'RiskFactorAlert';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Cuando origen=1 y existe registro en ReportRiskFactors con Status=1 para el paciente, retorna 1; en caso contrario, 0; [RETURN_RESULT] : Cuando origen=2 y existe registro en HCESCALAS para el paciente y el número de ingreso, retorna 1; en caso contrario, 0', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'RiskFactorAlert';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Origen = 1 (Factores de riesgo) → Verifica existencia de factores de riesgo activos (Status = 1) del paciente en ReportRiskFactors else Si Origen = 2 (Escalas), verifica existencia de escalas clínicas del paciente para el ingreso indicado en HCESCALAS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'RiskFactorAlert';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ReportRiskFactors; dbo.HCESCALAS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'RiskFactorAlert';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'RiskFactorAlert';
GO
