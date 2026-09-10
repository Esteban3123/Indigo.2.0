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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de nómina que, dado un código de clase de concepto de liquidación, determina a qué tipo de concepto pertenece: devengos (1), provisiones (2), parafiscales (3), seguridad social (4) o incapacidades (5). Internamente consulta la función GetConceptClassByType para cada tipo posible y retorna el número correspondiente; si la clase no pertenece a ningún tipo, retorna 0. Se usa en el proceso de nómina para clasificar automáticamente los conceptos salariales y prestacionales según su naturaleza contable y legal.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'FUNCTION', @level1name = N'GetTypeByConceptClass';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'FUNCTION', @level1name = N'GetTypeByConceptClass';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Determina a qué tipo contable/legal de nómina (Devengos, Provisión, Parafiscales, Seguridad Social o Incapacidades) pertenece una clase de concepto, o 0 si no está clasificada.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'GetTypeByConceptClass';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La función Payroll.GetConceptClassByType debe existir y retornar las clases asociadas a cada tipo (1..5); El código de clase de concepto debe caber en VARCHAR(3)', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'GetTypeByConceptClass';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El valor retornado siempre es un entero entre 0 y 5; La evaluación se hace en orden 1→5 y retorna el primer tipo coincidente, por lo que si una clase estuviera en varios tipos prevalecería el de menor número; Si la clase no pertenece a ningún tipo, retorna 0; La correspondencia tipo→significado es: 1=Devengos, 2=Provisión, 3=Parafiscales, 4=Seguridad Social, 5=Incapacidades', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'GetTypeByConceptClass';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Concepto de nómina; Devengos; Provisión; Parafiscales; Seguridad Social; Incapacidades; Clasificación contable y legal de nómina', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'GetTypeByConceptClass';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (scalar return): Retorna 1..5 según el primer GetConceptClassByType(n) en el que exista la clase; si no existe en ninguno, retorna 0', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'GetTypeByConceptClass';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si La clase de concepto existe en GetConceptClassByType(1) → Retorna 1 (Devengos) else Evalúa el siguiente tipo; si La clase de concepto existe en GetConceptClassByType(2) → Retorna 2 (Provisión) else Evalúa el siguiente tipo; si La clase de concepto existe en GetConceptClassByType(3) → Retorna 3 (Parafiscales) else Evalúa el siguiente tipo; si La clase de concepto existe en GetConceptClassByType(4) → Retorna 4 (Seguridad Social) else Evalúa el siguiente tipo; si La clase de concepto existe en GetConceptClassByType(5) → Retorna 5 (Incapacidades) else Retorna 0 (no clasificada)', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'GetTypeByConceptClass';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Payroll.GetConceptClassByType', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'GetTypeByConceptClass';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.GetConceptClassByType', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'GetTypeByConceptClass';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'GetTypeByConceptClass';
GO
