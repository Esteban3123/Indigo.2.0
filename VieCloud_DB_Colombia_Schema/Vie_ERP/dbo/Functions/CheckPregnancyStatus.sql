
CREATE FUNCTION [dbo].[CheckPregnancyStatus](@IPCODPACI as varchar(25), @Ingreso as varchar(10) )

RETURNS bit
AS
BEGIN

declare @retorno as bit

IF EXISTS (SELECT TOP 1 Id FROM HCRIESGOSP WHERE IPCODPACI = @IPCODPACI and NUMINGRCES = @Ingreso and GESTACION = 1) or EXISTS (SELECT top 1 ID FROM ADTRIAGEU WHERE IPCODPACI = @IPCODPACI and NUMINGRES = @Ingreso and PregnancyStatus = 1 ORDER BY TRIAFECHA DESC)
	set @retorno  = 1 
else
	set @retorno  = 0

return @retorno  

end
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Verifica si una paciente está embarazada en un ingreso específico, consultando dos fuentes clínicas distintas: los factores de riesgo en salud pública (tabla HCRIESGOSP, campo GESTACION) y la valoración inicial de triage de urgencias (tabla ADTRIAGEU, campo PregnancyStatus). Recibe la cédula del paciente y el número de ingreso, y retorna verdadero (1) si en cualquiera de las dos fuentes se registró gestación activa para ese ingreso. Se usa para identificar pacientes gestantes en procesos clínicos, alertas de riesgo y reportes que requieren distinguir si la paciente está en estado de embarazo durante su atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'CheckPregnancyStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'CheckPregnancyStatus';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Determina si un paciente, dentro de un ingreso específico, está marcado como en estado de gestación según los registros clínicos de riesgos obstétricos o el triage.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CheckPregnancyStatus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe proveerse identificación del paciente y número de ingreso para correlacionar registros en HCRIESGOSP y ADTRIAGEU', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CheckPregnancyStatus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El resultado siempre es booleano (0 o 1), nunca NULL; Basta con que UNA de las dos fuentes (riesgos o triage) marque embarazo para considerar gestante; La evaluación se hace siempre en el contexto de un ingreso específico del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CheckPregnancyStatus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; ingreso; gestación/embarazo; triage; riesgos obstétricos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CheckPregnancyStatus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe registro de riesgo obstétrico con GESTACION=1 para el paciente e ingreso, o existe triage con PregnancyStatus=1 para el paciente e ingreso → Retorna 1 (paciente en estado de gestación) else Retorna 0 (no gestante)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CheckPregnancyStatus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCRIESGOSP; dbo.ADTRIAGEU', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CheckPregnancyStatus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CheckPregnancyStatus';
GO
