CREATE FUNCTION [dbo].[Split]
(
	@RowData nvarchar(max),
	@SplitOn nvarchar(5)
)  
RETURNS @RtnValue table 
(
	Id int identity(1,1),
	Data nvarchar(250)
) 
AS  
BEGIN 
	Declare @Cnt int
	Set @Cnt = 1

	While (Charindex(@SplitOn,@RowData)>0)
	Begin
		Insert Into @RtnValue (data)
		Select 
			Data = ltrim(rtrim(Substring(@RowData,1,Charindex(@SplitOn,@RowData)-1)))

		Set @RowData = Substring(@RowData,Charindex(@SplitOn,@RowData)+1,len(@RowData))
		Set @Cnt = @Cnt + 1
	End
	
	Insert Into @RtnValue (data)
	Select Data = ltrim(rtrim(@RowData))

	Return
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función utilitaria que divide una cadena de texto en partes separadas por un delimitador definido, retornando cada fragmento como una fila en una tabla. Recibe el texto original y el carácter o secuencia separadora, y devuelve un listado numerado de valores individuales. Se usa internamente para procesar listas de códigos, identificadores o valores concatenados (por ejemplo, lista de cédulas, códigos de servicios o diagnósticos separados por coma o punto y coma) que llegan como un único texto y necesitan tratarse como registros independientes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Split';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Split';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Divide una cadena en múltiples filas usando un delimitador, devolviendo cada fragmento con un identificador secuencial.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Split';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La cadena de entrada y el delimitador no deben ser nulos para que CHARINDEX opere correctamente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Split';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada fragmento insertado se almacena recortado de espacios en blanco (LTRIM/RTRIM); Siempre se inserta al menos una fila (el resto final), incluso si no se encuentra el delimitador; El Id se asigna secuencialmente preservando el orden de aparición en la cadena original; La columna Data trunca a 250 caracteres por definición de la tabla', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Split';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @RtnValue: Mientras CHARINDEX(@SplitOn,@RowData)>0, inserta el segmento previo al delimitador con LTRIM/RTRIM aplicado; [INSERT] @RtnValue: Al finalizar el bucle, inserta el último fragmento restante de la cadena con LTRIM/RTRIM; [RETURN_RESULT] @RtnValue: Devuelve la tabla con Id identity(1,1) y los fragmentos en columna Data', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Split';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CHARINDEX(@SplitOn,@RowData) > 0 → Extrae el substring previo al delimitador, lo inserta y recorta @RowData else Sale del bucle e inserta el resto de la cadena como último registro', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Split';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Split';
GO
