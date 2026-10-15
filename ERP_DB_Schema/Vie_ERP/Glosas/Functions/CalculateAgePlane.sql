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