
CREATE FUNCTION [dbo].[FactorRiesgoSivigila560] (@IdFichaSivigila as int,@CodigoAntecedentes as int)
RETURNS varchar (1)
AS
BEGIN

declare @TipoAntecedentenMarcada varchar (1)

IF (Select COUNT(*) from HCFICHARIESGOS where IDFICHANOTIFICACION = @IdFichaSivigila AND CODRIESGOCOMPLI = @CodigoAntecedentes) > 0 BEGIN
	set @TipoAntecedentenMarcada  = 'X'
END 

RETURN @TipoAntecedentenMarcada  

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si un factor de riesgo o antecedente específico está marcado en una ficha de notificación Sivigila (ficha 560). Recibe el identificador de la ficha Sivigila y el código del riesgo o complicación, y retorna ''X'' si ese antecedente fue registrado para esa ficha, o nulo si no aplica. Consulta la tabla de riesgos y complicaciones de la historia clínica (HCFICHARIESGOS) para verificar la existencia del registro. Se usa típicamente para generar el reporte SIVIGILA, marcando casillas de factores de riesgo presentes en la notificación epidemiológica del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'FactorRiesgoSivigila560';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'FactorRiesgoSivigila560';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Determina si una ficha de notificación Sivigila tiene marcado un determinado factor de riesgo/antecedente, devolviendo ''X'' cuando está presente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'FactorRiesgoSivigila560';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir la ficha de notificación referenciada en HCFICHARIESGOS para que la marca aplique; El código de antecedente debe corresponder a un valor válido de CODRIESGOCOMPLI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'FactorRiesgoSivigila560';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El valor de retorno solo puede ser ''X'' o NULL; No realiza modificaciones de datos; es función de solo lectura', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'FactorRiesgoSivigila560';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha de notificación Sivigila; Factor de riesgo; Antecedentes clínicos; Riesgos y complicaciones', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'FactorRiesgoSivigila560';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Cuando COUNT(*) en HCFICHARIESGOS para la ficha y el código de riesgo es > 0, retorna ''X''; en caso contrario retorna NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'FactorRiesgoSivigila560';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe al menos un registro en HCFICHARIESGOS para la ficha de notificación y el código de riesgo/antecedente indicados → Retorna ''X'' indicando que el antecedente está marcado else Retorna NULL (variable sin asignar)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'FactorRiesgoSivigila560';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHARIESGOS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'FactorRiesgoSivigila560';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'FactorRiesgoSivigila560';
GO
