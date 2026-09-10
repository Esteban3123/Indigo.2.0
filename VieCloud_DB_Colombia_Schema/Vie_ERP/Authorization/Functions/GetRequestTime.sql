-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-07-19
-- Description:	Devuelve el tiempo de la solicitud
-- =============================================
CREATE FUNCTION [Authorization].[GetRequestTime]
(
	@Status TINYINT,		-- Estado de la solicitud
	@Request INT,			-- Tiempo solicitud
	@Radicated INT,			-- Tiempo radicación
	@DeliveryService INT	-- Tiempo autorización
)
RETURNS int
AS
BEGIN
	RETURN	ISNULL(	CASE @Status
						WHEN 1 THEN @Request
						WHEN 2 THEN @Radicated
						WHEN 3 THEN @Radicated
						WHEN 5 THEN @DeliveryService
						WHEN 12 THEN @Request
						WHEN 13 THEN @Radicated
					END, 0)
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que calcula y devuelve el tiempo relevante de una solicitud de autorización médica según su estado actual. Dependiendo del estado de la solicitud (en trámite, radicada, con autorización entregada, entre otros), selecciona el tiempo correspondiente: tiempo de solicitud, tiempo de radicación o tiempo de entrega del servicio autorizado. Retorna cero si no aplica ningún estado reconocido. Se usa para medir tiempos de gestión y respuesta en el proceso de autorización de servicios de salud.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'FUNCTION', @level1name = N'GetRequestTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'FUNCTION', @level1name = N'GetRequestTime';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Selecciona el indicador de tiempo aplicable a una solicitud de autorización según su estado, devolviendo 0 cuando el estado no está contemplado.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'GetRequestTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El estado de la solicitud debe ser uno de los valores reconocidos (1, 2, 3, 5, 12, 13) para obtener un tiempo distinto de 0.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'GetRequestTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El resultado nunca es NULL: el ISNULL garantiza un piso de 0.; Los estados 2, 3 y 13 comparten la misma métrica (tiempo de radicación).; Los estados 1 y 12 comparten la misma métrica (tiempo de solicitud).', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'GetRequestTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Solicitud de autorización; Estado de la solicitud; Tiempo de radicación; Tiempo de autorización; Entrega de servicio', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'GetRequestTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (scalar return): Devuelve el tiempo de solicitud cuando el estado es 1 o 12; el tiempo de radicación cuando el estado es 2, 3 o 13; el tiempo de autorización (entrega de servicio) cuando el estado es 5; en cualquier otro caso devuelve 0.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'GetRequestTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Status = 1 OR @Status = 12 → retorna @Request (tiempo de solicitud); si @Status = 2 OR @Status = 3 OR @Status = 13 → retorna @Radicated (tiempo de radicación); si @Status = 5 → retorna @DeliveryService (tiempo de autorización/entrega); si @Status no coincide con 1,2,3,5,12,13 o el CASE retorna NULL → retorna 0 por efecto del ISNULL', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'GetRequestTime';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'GetRequestTime';
GO
