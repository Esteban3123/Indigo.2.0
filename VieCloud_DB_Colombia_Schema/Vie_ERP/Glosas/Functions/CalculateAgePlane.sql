CREATE FUNCTION [Glosas].[CalculateAgePlane] (
    @fechaNacimiento DATE,
    @fechaCorte DATE
)
RETURNS @PatienAges TABLE
(
	Age INT,
	UnitMeasureAge INT
)
AS
BEGIN
    DECLARE @years INT;
    DECLARE @months INT;
    DECLARE @days INT;

    SET @years = DATEDIFF(YEAR, @fechaNacimiento, @fechaCorte);
    SET @fechaNacimiento = DATEADD(YEAR, @years, @fechaNacimiento);
 
    IF (@fechaNacimiento > @fechaCorte)
    BEGIN
        SET @years = @years - 1;
        SET @fechaNacimiento = DATEADD(YEAR, -1, @fechaNacimiento);
    END

    SET @months = DATEDIFF(MONTH, @fechaNacimiento, @fechaCorte);
    SET @fechaNacimiento = DATEADD(MONTH, @months, @fechaNacimiento);

    IF (@fechaNacimiento > @fechaCorte)
    BEGIN
        SET @months = @months - 1;
        SET @fechaNacimiento = DATEADD(MONTH, -1, @fechaNacimiento);
    END

    SET @days = DATEDIFF(DAY, @fechaNacimiento, @fechaCorte);

	IF (@years > 0)
		INSERT INTO @PatienAges VALUES(@years, 1)
    ELSE IF (@months > 0)
        INSERT INTO @PatienAges VALUES(@months, 2)
    ELSE
        INSERT INTO @PatienAges VALUES(@days, 3)

    RETURN
END;
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que calcula la edad de un paciente a partir de su fecha de nacimiento y una fecha de corte, expresando el resultado en la unidad de medida más apropiada según la etapa de vida: años (unidad 1) si el paciente tiene al menos un año de vida, meses (unidad 2) si tiene al menos un mes pero menos de un año, o días (unidad 3) si es menor de un mes. Se utiliza en el módulo de Glosas para determinar la edad del paciente al momento de la atención, dato relevante en la validación y auditoría de cuentas médicas donde la edad condiciona tarifas, diagnósticos o reglas de facturación. Devuelve una tabla con el valor numérico de la edad y su unidad de medida correspondiente.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'FUNCTION', @level1name = N'CalculateAgePlane';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'FUNCTION', @level1name = N'CalculateAgePlane';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula la edad de un paciente entre fecha de nacimiento y fecha de corte, expresándola en la unidad mayor disponible (años, meses o días).', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'FUNCTION', @level1name=N'CalculateAgePlane';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se requieren fecha de nacimiento y fecha de corte válidas; Se asume que la fecha de corte es posterior o igual a la fecha de nacimiento', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'FUNCTION', @level1name=N'CalculateAgePlane';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Siempre devuelve exactamente una fila con la edad; La unidad de medida representa la mayor unidad temporal con valor positivo: 1=años, 2=meses, 3=días; Los años y meses incompletos no se cuentan (se ajusta restando 1 cuando la suma excede la fecha de corte)', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'FUNCTION', @level1name=N'CalculateAgePlane';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Edad del paciente; Unidad de medida de edad; Fecha de corte', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'FUNCTION', @level1name=N'CalculateAgePlane';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @PatienAges: Cuando @years > 0 inserta el valor en años con UnitMeasureAge=1; [INSERT] @PatienAges: Cuando @years = 0 y @months > 0 inserta el valor en meses con UnitMeasureAge=2; [INSERT] @PatienAges: Cuando @years = 0 y @months = 0 inserta el valor en días con UnitMeasureAge=3', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'FUNCTION', @level1name=N'CalculateAgePlane';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Tras sumar @years a la fecha de nacimiento, ésta supera la fecha de corte → Se decrementa @years en 1 y se retrocede la fecha un año para no contar el año incompleto; si Tras sumar @months, la fecha de nacimiento supera la fecha de corte → Se decrementa @months en 1 y se retrocede un mes para no contar el mes incompleto; si @years > 0 → Edad expresada en años (unidad=1) else Si @months > 0 se expresa en meses (unidad=2); de lo contrario en días (unidad=3)', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'FUNCTION', @level1name=N'CalculateAgePlane';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'FUNCTION', @level1name=N'CalculateAgePlane';
GO
