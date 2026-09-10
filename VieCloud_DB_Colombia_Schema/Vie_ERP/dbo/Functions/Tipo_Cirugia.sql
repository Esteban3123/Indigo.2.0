-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[Tipo_Cirugia] (
@TIPO_CIRUG as bit

)
RETURNS varchar (100)
AS
BEGIN

declare @TPCIRU varchar(100)
SET @TPCIRU=(CASE 
WHEN @TIPO_CIRUG = 'TRUE' THEN 'URGENCIA' 
WHEN @TIPO_CIRUG = 'FALSE' THEN 'PROGRAMADA' END)

RETURN (@TPCIRU)
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que convierte un valor booleano en la descripción textual del tipo de cirugía: si el valor es verdadero (TRUE) devuelve ''URGENCIA'', y si es falso (FALSE) devuelve ''PROGRAMADA''. Se usa para mostrar en reportes y consultas si una cirugía fue de urgencia o fue una cirugía programada, en lugar de mostrar un valor de bit crudo. Toca la entidad de programación quirúrgica y facilita la lectura humana del tipo de intervención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Tipo_Cirugia';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Tipo_Cirugia';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Traduce un indicador booleano de tipo de cirugía a su etiqueta descriptiva: ''URGENCIA'' o ''PROGRAMADA''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Tipo_Cirugia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El indicador recibido debe ser un valor bit (TRUE/FALSE) o NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Tipo_Cirugia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El resultado solo puede ser ''URGENCIA'', ''PROGRAMADA'' o NULL (si el bit es NULL); Mapea un valor booleano a una etiqueta textual del tipo de cirugía', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Tipo_Cirugia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cirugía; Cirugía de urgencia; Cirugía programada', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Tipo_Cirugia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (scalar return): Cuando el indicador es TRUE retorna ''URGENCIA''; cuando es FALSE retorna ''PROGRAMADA''; en cualquier otro caso retorna NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Tipo_Cirugia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Indicador de tipo de cirugía = TRUE → Devuelve ''URGENCIA'' else Si es FALSE devuelve ''PROGRAMADA''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Tipo_Cirugia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Tipo_Cirugia';
GO
