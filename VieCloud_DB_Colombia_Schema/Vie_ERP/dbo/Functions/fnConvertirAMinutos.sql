
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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Convierte una duración expresada en distintas unidades de tiempo a su equivalente en minutos. Recibe una cantidad numérica y un código de unidad (1=Minutos, 2=Horas, 3=Días, 4=Semanas, 5=Meses, 6=Años) y retorna el total de minutos correspondiente. Se usa en procesos clínicos y de agendamiento donde los intervalos de tiempo (como frecuencias de medicamentos, duraciones de tratamientos o ventanas de atención) necesitan normalizarse a una única unidad para su comparación o cálculo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'fnConvertirAMinutos';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'fnConvertirAMinutos';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Convierte una cantidad expresada en una unidad de tiempo (minutos, horas, días, semanas, meses o años) a su equivalente en minutos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnConvertirAMinutos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La unidad debe ser un carácter entre ''1'' y ''6''; cualquier otro valor produce multiplicador 0 y retorno 0.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnConvertirAMinutos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Un mes se considera fijo de 30 días (43200 minutos).; Un año se considera fijo de 365 días (525600 minutos), sin contemplar años bisiestos.; Si la unidad no está en el rango ''1''-''6'', el resultado siempre es 0.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnConvertirAMinutos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'unidad de tiempo; conversión a minutos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnConvertirAMinutos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (scalar return): Retorna Cantidad * Multiplicador, donde el multiplicador depende de la unidad: ''1''→1, ''2''→60, ''3''→1440, ''4''→10080, ''5''→43200, ''6''→525600.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnConvertirAMinutos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Unidad = ''1'' → Multiplicador = 1 (minutos); si Unidad = ''2'' → Multiplicador = 60 (horas a minutos); si Unidad = ''3'' → Multiplicador = 1440 (días a minutos); si Unidad = ''4'' → Multiplicador = 10080 (semanas a minutos); si Unidad = ''5'' → Multiplicador = 43200 (meses a minutos, asumiendo 30 días); si Unidad = ''6'' → Multiplicador = 525600 (años a minutos, asumiendo 365 días) else Multiplicador permanece en 0', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnConvertirAMinutos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnConvertirAMinutos';
GO
