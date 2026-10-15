-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-09-05
-- Description:	Tipos de Conceptos
-- Si tipo es 1: Devengos
-- Si tipo es 2: Deducciones
-- Si tipo es 3: Seguridad Social Empleador
-- Si tipo es 4: Parafiscales
-- Si tipo es 5: Provisiones
-- Si tipo es 6: Incapacidades
-- =============================================
CREATE FUNCTION [Payroll].[GetConceptClassByType]
(	
	@Type TINYINT
)
RETURNS @ListConceptClass TABLE 
(
	ConceptClass VARCHAR(3)
)
AS
BEGIN

	IF @Type = 1		-- Devengos
	BEGIN
		INSERT INTO @ListConceptClass VALUES
			('001'),('002'),('003'),('004'),('005'),('006'),('007'),('010'),('011'),('012'),('013'),('021'),('022'),('023'),('024'),('025'),('027'),('029'),('030'),('042'),('043'),('046'),('047'),('049'),('050'),('051'),('052'),('055')
	END
	ELSE IF @Type = 2	-- Deducciones
	BEGIN
		INSERT INTO @ListConceptClass VALUES
			('006'),('011'),('014'),('017'),('020'),('030'),('038'),('041'),('044')
	END	
	ELSE IF @Type = 3	-- Seguridad Social Empleador
	BEGIN
		INSERT INTO @ListConceptClass VALUES
			('009'),('015'),('018')
	END
	ELSE IF @Type = 4	-- Parafiscales
	BEGIN
		INSERT INTO @ListConceptClass VALUES
			('035'),('036'),('037')
	END
	ELSE IF @Type = 5	-- Provisiones
	BEGIN
		INSERT INTO @ListConceptClass VALUES
			('008'),('031'),('033'),('034'),('056'),('058'),('057'),('060'),('059')
	END
	ELSE IF @Type = 6	-- Incapacidades
	BEGIN
		INSERT INTO @ListConceptClass VALUES
			('021'),('022'),('023'),('024'),('068'),('027'),('070')
	END

	RETURN
END