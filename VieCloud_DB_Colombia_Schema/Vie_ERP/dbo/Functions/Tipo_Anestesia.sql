-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	Tipo de Anestecia que se aplicara al paciente
-- =============================================
CREATE FUNCTION [dbo].[Tipo_Anestesia] (
@TIPO_ANEST as int

)
RETURNS varchar (100)
AS
BEGIN

declare @TPANEST varchar(100)
SET @TPANEST=(CASE 
WHEN @TIPO_ANEST = 1 THEN 'LOCAL' 
WHEN @TIPO_ANEST = 2 THEN 'REGIONAL' 
WHEN @TIPO_ANEST = 3 THEN 'GENERAL' 
WHEN @TIPO_ANEST = 4 THEN 'COMBINADA'
END)

RETURN (@TPANEST)
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que convierte un código numérico de tipo de anestesia en su descripción textual para el paciente quirúrgico. Recibe un número entero (1 al 4) y devuelve el nombre correspondiente: LOCAL, REGIONAL, GENERAL o COMBINADA. Se usa para mostrar en reportes, historias clínicas y documentos quirúrgicos el tipo de anestesia aplicada o planificada para el paciente, en lugar del código interno.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Tipo_Anestesia';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Tipo_Anestesia';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Traduce un código numérico de tipo de anestesia a su descripción textual para mostrar al usuario.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Tipo_Anestesia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El código recibido debe estar en el rango 1-4; otros valores devuelven NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Tipo_Anestesia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo existen 4 tipos válidos de anestesia codificados (1..4); El resultado siempre es una cadena de hasta 100 caracteres o NULL; No realiza acceso a tablas; es una función pura de mapeo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Tipo_Anestesia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Tipo de anestesia; Paciente; Anestesia local; Anestesia regional; Anestesia general; Anestesia combinada', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Tipo_Anestesia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (retorno escalar): Devuelve ''LOCAL'' si código=1, ''REGIONAL'' si código=2, ''GENERAL'' si código=3, ''COMBINADA'' si código=4; en cualquier otro caso devuelve NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Tipo_Anestesia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si código = 1 → Retorna ''LOCAL''; si código = 2 → Retorna ''REGIONAL''; si código = 3 → Retorna ''GENERAL''; si código = 4 → Retorna ''COMBINADA'' else Retorna NULL (CASE sin ELSE)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Tipo_Anestesia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Tipo_Anestesia';
GO
