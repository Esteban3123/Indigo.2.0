

CREATE FUNCTION [dbo].[DiferenciaColumna] (  @Cama as nvarchar(10), @Cuenta as nvarchar(10))
RETURNS datetime 
AS
BEGIN

declare @Dias datetime

select  @Dias= b.FECINIEST
    FROM .CHREGESTA AS A INNER JOIN
	  (SELECT CODICAMAS,NUMINGRES,FECINIEST,  ROW_NUMBER() OVER(PARTITION BY CODICAMAS ORDER BY FECREGSIS ASC)-1 AS Cuenta 
	       from .CHREGESTA) AS B ON  @Cama = a.CODICAMAS   
WHERE @Cuenta= b.cuenta 
RETURN @Dias

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que, dado un código de cama y un número de posición (índice ordinal), retorna la fecha de inicio de estancia correspondiente a ese turno de ocupación de la cama en el historial hospitalario. Consulta el registro de estados del paciente (CHREGESTA) dos veces: una como referencia de la cama buscada y otra con numeración secuencial de ocupaciones ordenadas por fecha de registro del sistema, permitiendo identificar cuándo comenzó cada período de uso de una cama específica. Se utiliza para calcular diferencias o trazabilidad en la rotación de camas entre ingresos o estancias, apoyando el control de ocupación y gestión de camas hospitalarias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'DiferenciaColumna';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'DiferenciaColumna';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve la fecha de inicio de estancia correspondiente a la n-ésima ocupación (según orden cronológico de registro) de una cama hospitalaria específica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DiferenciaColumna';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un registro en CHREGESTA para la cama indicada; El índice de cuenta solicitado debe existir dentro de las ocupaciones de esa cama (numeración base 0)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DiferenciaColumna';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La numeración de ocupaciones por cama empieza en 0 (ROW_NUMBER()-1) y se ordena ascendentemente por FECREGSIS; El particionamiento se hace por CODICAMAS, por lo que cada cama tiene su propia secuencia independiente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DiferenciaColumna';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'cama hospitalaria; estancia; ingreso; ocupación de cama', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DiferenciaColumna';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'CHREGESTA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DiferenciaColumna';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DiferenciaColumna';
GO
