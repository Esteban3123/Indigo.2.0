CREATE FUNCTION [dbo].[SplitString] ( @stringToSplit VARCHAR(MAX) )
RETURNS
@returnList TABLE ([Value] [nvarchar] (1000))
AS
BEGIN

 DECLARE @name NVARCHAR(500)
 DECLARE @pos INT

 WHILE CHARINDEX(',', @stringToSplit) > 0
 BEGIN
  SELECT @pos  = CHARINDEX(',', @stringToSplit)  
  SELECT @name = SUBSTRING(@stringToSplit, 1, @pos-1)

  INSERT INTO @returnList 
  SELECT @name

  SELECT @stringToSplit = SUBSTRING(@stringToSplit, @pos+1, LEN(@stringToSplit)-@pos)
 END

 INSERT INTO @returnList
 SELECT @stringToSplit

 RETURN
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función utilitaria que divide una cadena de texto separada por comas y devuelve cada elemento como una fila individual en una tabla. Se usa internamente para procesar listas de valores múltiples (por ejemplo, listas de códigos de pacientes, servicios, diagnósticos u otros identificadores) que llegan como un único texto concatenado. Permite que otros procedimientos o consultas puedan operar sobre cada valor por separado, como si fueran registros independientes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'SplitString';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'SplitString';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Divide una cadena delimitada por comas en una tabla con un registro por cada fragmento.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'SplitString';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Siempre devuelve al menos una fila, incluso si la entrada no contiene comas; Cada valor insertado se trunca al tipo nvarchar(1000); El delimitador utilizado siempre es la coma', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'SplitString';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @returnList: Mientras CHARINDEX('','', cadena) > 0, inserta el fragmento previo a la coma; al finalizar el bucle inserta el resto de la cadena.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'SplitString';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CHARINDEX('','', @stringToSplit) > 0 → Extrae el segmento anterior a la coma y lo inserta, luego recorta la cadena else Inserta la cadena restante como último elemento', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'SplitString';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'SplitString';
GO
