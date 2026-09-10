
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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determina el tipo de predio (urbano o rural) a partir del código catastral del predio. Evalúa los dos primeros caracteres del código: si comienzan con ''01'' el predio se clasifica como urbano (tipo 1), de lo contrario se clasifica como rural (tipo 2). Se utiliza en el módulo de impuestos para categorizar predios y aplicar las reglas tributarias correspondientes según su naturaleza.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'FUNCTION', @level1name = N'fnGetPropertyTypeByCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'FUNCTION', @level1name = N'fnGetPropertyTypeByCode';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Determina si un predio es urbano o rural a partir del prefijo de su código catastral.', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'FUNCTION', @level1name=N'fnGetPropertyTypeByCode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El código recibido debe tener al menos 2 caracteres iniciales que representen el tipo de predio.', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'FUNCTION', @level1name=N'fnGetPropertyTypeByCode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El resultado siempre es 1 (Urbano) o 2 (Rural); no existen otros valores posibles.; La clasificación depende exclusivamente de los dos primeros caracteres del código, ignorando el resto.; Un código nulo o con menos de 2 caracteres que no inicie en ''01'' se clasifica como Rural.', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'FUNCTION', @level1name=N'fnGetPropertyTypeByCode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Predio urbano; Predio rural; Código catastral del predio', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'FUNCTION', @level1name=N'fnGetPropertyTypeByCode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (retorno escalar): Cuando los dos primeros caracteres del código son ''01'' retorna 1 (Urbano); en cualquier otro caso retorna 2 (Rural).', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'FUNCTION', @level1name=N'fnGetPropertyTypeByCode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si SUBSTRING(@Code,1,2) = ''01'' → Clasifica el predio como Urbano (1) else Clasifica el predio como Rural (2)', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'FUNCTION', @level1name=N'fnGetPropertyTypeByCode';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'FUNCTION', @level1name=N'fnGetPropertyTypeByCode';
GO
