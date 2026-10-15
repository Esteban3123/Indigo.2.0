-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-09-05
-- Description:	Tipos de Conceptos
-- Si tipo es 1: Devengos
-- Si tipo es 2: Provision
-- Si tipo es 3: Parafiscales
-- Si tipo es 4: Seguridad Social
-- Si tipo es 5: Incapacidades
-- =============================================
CREATE FUNCTION [Payroll].[GetTypeByConceptClass]
(	
	@ConceptClass VARCHAR(3)
)
RETURNS TINYINT
AS
BEGIN

	IF EXISTS (SELECT 1 FROM Payroll.GetConceptClassByType(1) WHERE ConceptClass = @ConceptClass)
	BEGIN
		RETURN 1
	END
	
	IF EXISTS (SELECT 1 FROM Payroll.GetConceptClassByType(2) WHERE ConceptClass = @ConceptClass)
	BEGIN
		RETURN 2
	END

	IF EXISTS (SELECT 1 FROM Payroll.GetConceptClassByType(3) WHERE ConceptClass = @ConceptClass)
	BEGIN
		RETURN 3
	END

	IF EXISTS (SELECT 1 FROM Payroll.GetConceptClassByType(4) WHERE ConceptClass = @ConceptClass)
	BEGIN
		RETURN 4
	END

	IF EXISTS (SELECT 1 FROM Payroll.GetConceptClassByType(5) WHERE ConceptClass = @ConceptClass)
	BEGIN
		RETURN 5
	END

	RETURN 0
END