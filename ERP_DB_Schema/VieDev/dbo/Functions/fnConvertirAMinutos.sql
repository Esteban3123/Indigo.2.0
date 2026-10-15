
CREATE FUNCTION [dbo].[fnConvertirAMinutos] (@Cantidad Integer, @Unidad as Char(1))
RETURNS Integer
AS
BEGIN

	Declare @Multiplicador Integer = 0

/*
1: Minutos
2: Horas
3: Dias 
4:Semanas 
5:meses 
6:Años
*/

	If @Unidad = '1' 
		Set @Multiplicador = 1
	Else If @Unidad = '2' 
		Set @Multiplicador = 60
	Else If @Unidad = '3' 
		Set @Multiplicador = 1440
	Else If @Unidad = '4' 
		Set @Multiplicador = 10080
	Else If @Unidad = '5' 
		Set @Multiplicador = 43200
	Else If @Unidad = '6' 
		Set @Multiplicador = 525600

	RETURN @Cantidad * @Multiplicador

END