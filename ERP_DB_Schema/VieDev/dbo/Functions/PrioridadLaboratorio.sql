CREATE FUNCTION [dbo].[PrioridadLaboratorio]
(
@CodigoPrioridad As char 
)
RETURNS nvarchar (15)
AS
BEGIN

    DECLARE @Prioridad as nvarchar(15)

    SELECT @Prioridad = CASE @CodigoPrioridad WHEN '1' THEN 'Urgente' WHEN '2' THEN 'Rutinario' END 

    RETURN @Prioridad
END