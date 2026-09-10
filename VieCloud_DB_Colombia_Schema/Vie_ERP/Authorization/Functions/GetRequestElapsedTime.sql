
-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-07-19
-- Description:	Devuelve el tiempo transcurrido
-- =============================================
CREATE FUNCTION [Authorization].[GetRequestElapsedTime]
(
	@RequestDate DATETIME,				-- Estado de la solicitud
	@RequestUnitTime INT,				-- Unidad de tiempo del proceso
	@Now DATETIME						-- "Ahora" calculado por el caller (evita GETDATE() interno: bloqueaba scalar UDF inlining y forzaba plan serial)
)
RETURNS int
AS
BEGIN
	RETURN	ISNULL(	CASE @RequestUnitTime
						WHEN 1 THEN DATEDIFF(MINUTE, @requestDate, @Now) --Minutos
						WHEN 2 THEN DATEDIFF(HOUR, @requestDate, @Now) --Horas
						WHEN 3 THEN DATEDIFF(DAY, @requestDate, @Now) --Días
					END, 0)
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula el tiempo transcurrido desde una fecha de solicitud de autorización hasta el momento actual, expresado en la unidad de tiempo indicada: minutos (1), horas (2) o días (3). Se usa para medir cuánto tiempo lleva pendiente o en proceso una solicitud de autorización de servicios de salud. Retorna cero si el resultado es nulo, garantizando siempre un valor numérico válido. Es útil para controlar vencimientos, alertas de demora y cumplimiento de tiempos de respuesta en el proceso de autorización.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'FUNCTION', @level1name = N'GetRequestElapsedTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'FUNCTION', @level1name = N'GetRequestElapsedTime';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula el tiempo transcurrido desde una fecha de solicitud hasta el momento actual, expresado en minutos, horas o días según la unidad de tiempo indicada.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'GetRequestElapsedTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La fecha de referencia debe ser interpretable como DATETIME válido; La unidad de tiempo debe ser 1 (minutos), 2 (horas) o 3 (días) para obtener un cálculo significativo', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'GetRequestElapsedTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El resultado nunca es NULL: si el CASE no cubre la unidad, se retorna 0; El cálculo siempre se hace contra GETDATE() (hora actual del servidor); Solo se reconocen tres unidades de tiempo: minutos, horas y días', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'GetRequestElapsedTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Solicitud de autorización; Tiempo transcurrido de la solicitud; Unidad de tiempo del proceso', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'GetRequestElapsedTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (scalar return): Devuelve DATEDIFF en MINUTE/HOUR/DAY según unidad 1/2/3; si la unidad no coincide o el resultado es NULL, devuelve 0 (ISNULL(...,0))', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'GetRequestElapsedTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @RequestUnitTime = 1 → Retorna diferencia en minutos entre la fecha de solicitud y GETDATE(); si @RequestUnitTime = 2 → Retorna diferencia en horas entre la fecha de solicitud y GETDATE(); si @RequestUnitTime = 3 → Retorna diferencia en días entre la fecha de solicitud y GETDATE(); si @RequestUnitTime no está en (1,2,3) → El CASE retorna NULL y por ISNULL se devuelve 0', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'GetRequestElapsedTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'GetRequestElapsedTime';
GO
