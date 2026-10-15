
create FUNCTION [Taxes].[fnGetPropertyTypeByCode] (@Code as varchar(50))
RETURNS int
AS
BEGIN

--Variable que retorna el tipo de predio @PropertyType = 1.Urbano(01), 2.Rural(00)
declare @PropertyType int

--Para sacar el tipo de predio se saca la posición 6 y 7 del código del predio(las posiciones se cuentan apartir de 1 y no de 0)

if SUBSTRING(@Code, 1, 2) = '01' --Es urbano
Begin
	set @PropertyType = 1
End
Else --Sino es rural
Begin
	set @PropertyType = 2
End

Return @PropertyType

END