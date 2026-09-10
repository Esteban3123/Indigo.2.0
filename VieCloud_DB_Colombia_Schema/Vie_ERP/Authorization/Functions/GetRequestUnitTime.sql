-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-07-19
-- Description:	Devuelve la unidad de tiempo de la solicitud
-- =============================================
CREATE FUNCTION [Authorization].[GetRequestUnitTime]
(
	@Status TINYINT,				-- Estado de la solicitud
	@RequestUnit TINYINT,			-- Tiempo solicitud
	@RadicatedUnit TINYINT,			-- Tiempo radicación
	@DeliveryServiceUnit TINYINT	-- Tiempo autorización
)
RETURNS int
AS
BEGIN
	RETURN	ISNULL(	CASE @Status
						WHEN 1 THEN @RequestUnit
						WHEN 2 THEN @RadicatedUnit
						WHEN 3 THEN @RadicatedUnit
						WHEN 5 THEN @DeliveryServiceUnit
						WHEN 12 THEN @RequestUnit
						WHEN 13 THEN @RadicatedUnit
					END, 0)
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que determina la unidad de tiempo aplicable a una solicitud de autorización médica según su estado actual. Recibe cuatro parámetros: el estado de la solicitud y tres posibles unidades de tiempo (tiempo de solicitud, tiempo de radicación y tiempo de entrega/autorización del servicio). Según el estado, devuelve la unidad correspondiente: para estado 1 o 12 retorna el tiempo de solicitud, para estados 2, 3 o 13 retorna el tiempo de radicación, y para estado 5 retorna el tiempo de autorización del servicio; si no aplica ningún estado conocido, retorna 0. Se usa en el proceso de autorización de servicios de salud para calcular los tiempos de respuesta exigidos según la etapa en que se encuentre la solicitud.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'FUNCTION', @level1name = N'GetRequestUnitTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'FUNCTION', @level1name = N'GetRequestUnitTime';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Selecciona, según el estado de la solicitud de autorización, cuál unidad de tiempo aplica (de solicitud, radicación o entrega de servicio) para calcular plazos de respuesta.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'GetRequestUnitTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El estado debe corresponder a uno de los códigos manejados (1, 2, 3, 5, 12, 13) para obtener una unidad distinta de 0.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'GetRequestUnitTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca retorna NULL: si el estado no coincide con los casos definidos o la unidad correspondiente es NULL, se retorna 0 por el ISNULL.; Los estados 2 y 3 comparten la misma unidad de tiempo (radicación), al igual que 1 y 12 (solicitud) y 3 y 13 (radicación).', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'GetRequestUnitTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'solicitud de autorización; estado de la solicitud; radicación; entrega de servicio; tiempos de respuesta en autorización de servicios de salud', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'GetRequestUnitTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (scalar return): Mapea el estado a una unidad de tiempo: 1/12→RequestUnit, 2/3/13→RadicatedUnit, 5→DeliveryServiceUnit; cualquier otro estado o valor NULL retorna 0.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'GetRequestUnitTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Estado = 1 o 12 → Devuelve la unidad de tiempo de la solicitud; si Estado = 2, 3 o 13 → Devuelve la unidad de tiempo de la radicación; si Estado = 5 → Devuelve la unidad de tiempo de la entrega/autorización del servicio; si Estado no contemplado (distinto de 1,2,3,5,12,13) o el valor mapeado es NULL → Devuelve 0 por efecto del ISNULL', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'GetRequestUnitTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'GetRequestUnitTime';
GO
