CREATE FUNCTION [Billing].[DuplicateRows]
(
	@Id int,
	@Value int
)  
RETURNS @RtnValue table 
(
	Id int
) 
AS  
BEGIN 
	Declare @Cnt int
	Set @Cnt = 0

	While @Cnt < @Value
	Begin
		Insert Into @RtnValue (Id) values (@Id)
		Set @Cnt = @Cnt + 1
	End
	
	Return
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función utilitaria de facturación que recibe un identificador (Id) y un número de repeticiones (Value), y devuelve una tabla con el Id repetido tantas veces como indique Value. Se usa para duplicar o expandir filas de facturación, por ejemplo al necesitar multiplicar un registro de cargo o concepto de cobro una cantidad determinada de veces dentro del proceso de liquidación o generación de líneas de factura.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'FUNCTION', @level1name = N'DuplicateRows';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'FUNCTION', @level1name = N'DuplicateRows';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera una tabla con un identificador repetido N veces, útil para expandir filas mediante un join.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'DuplicateRows';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El segundo parámetro debe ser mayor que cero para producir filas; si es 0 o negativo, retorna tabla vacía.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'DuplicateRows';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El número de filas devueltas es exactamente igual al valor recibido (cuando es > 0).; Todas las filas contienen el mismo identificador, sin variación.; Si el valor es ≤ 0, no se inserta ninguna fila.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'DuplicateRows';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Facturación (Billing)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'DuplicateRows';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @RtnValue: Mientras el contador sea menor que el valor recibido, inserta una fila con el Id en la tabla de retorno e incrementa el contador en 1.; [RETURN_RESULT] @RtnValue: Devuelve la tabla con tantas filas como indique el valor, todas con el mismo Id.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'DuplicateRows';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Cnt < @Value → Inserta una fila con @Id en @RtnValue y suma 1 al contador. else Termina el bucle y retorna la tabla acumulada.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'DuplicateRows';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'DuplicateRows';
GO
