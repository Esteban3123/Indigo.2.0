-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 01/06/2020
-- Description:	Devuelve el color con que se pinta
-- =============================================
CREATE FUNCTION [Authorization].[fnGetColor]
(
	@RequestTime INT,			-- Tiempo parametrizado para el proceso
	@RequestElapsedTime INT	-- Tiempo Trasncurrido
)
RETURNS INT
AS
BEGIN
	-- Reescrita como expresión única (sin DECLARE/SET/IF de múltiples statements) para que
	-- califique a scalar UDF inlining — la versión anterior forzaba plan no paralelo en TODA
	-- la consulta que la llamara, sin importar el compat level. Misma semántica exacta,
	-- incluyendo la división entera @RequestTime/2 (trunca igual con ambos operandos INT).
	-- Color: 1-Verde, 2-Amarillo, 3-Rojo
	RETURN CASE
		WHEN @RequestElapsedTime > @RequestTime THEN 3
		WHEN @RequestElapsedTime > @RequestTime / 2 THEN 2
		ELSE 1
	END
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula el color semafórico de alerta para una solicitud de autorización según el tiempo transcurrido frente al tiempo máximo permitido. Retorna 1 (verde) si el tiempo transcurrido es menor a la mitad del tiempo parametrizado, 2 (amarillo) si supera la mitad pero no excede el límite, y 3 (rojo) si ya se desbordó el tiempo asignado. Se usa para indicar visualmente el estado de cumplimiento del tiempo de respuesta en los procesos de autorización de servicios de salud.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'FUNCTION', @level1name = N'fnGetColor';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'FUNCTION', @level1name = N'fnGetColor';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Determina un indicador de semáforo (verde/amarillo/rojo) según qué tanto del tiempo parametrizado ha transcurrido en un proceso de autorización.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'fnGetColor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se requieren ambos tiempos (parametrizado y transcurrido) expresados en la misma unidad para que la comparación sea válida.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'fnGetColor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El valor retornado siempre pertenece al conjunto {1,2,3} correspondiente a verde/amarillo/rojo.; El umbral de amarillo se calcula como división entera de @RequestTime entre 2 (puede truncar para valores impares).; Rojo prevalece sobre amarillo cuando se excede el tiempo total.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'fnGetColor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Autorización; Tiempo de respuesta / SLA; Semáforo de estado (verde, amarillo, rojo)', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'fnGetColor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (retorno escalar): Si tiempo transcurrido > tiempo parametrizado → retorna 3 (rojo); si transcurrido > mitad del parametrizado → retorna 2 (amarillo); en otro caso retorna 1 (verde).', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'fnGetColor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @RequestElapsedTime > @RequestTime → Asigna color = 3 (rojo: tiempo desfasado) else Evalúa si el transcurrido supera la mitad del parametrizado; si @RequestElapsedTime > (@RequestTime / 2) (cuando no está desfasado) → Asigna color = 2 (amarillo) else Asigna color = 1 (verde)', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'fnGetColor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'fnGetColor';
GO
