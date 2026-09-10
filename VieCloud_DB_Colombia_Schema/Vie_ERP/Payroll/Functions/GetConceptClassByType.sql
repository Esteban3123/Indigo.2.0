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
			('021'),('022'),('023'),('027')
	END

	RETURN
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de nómina que recibe un tipo numérico y devuelve los códigos de clase de concepto de nómina correspondientes a esa categoría. Según el tipo ingresado, retorna las clases asociadas a: Devengos (1), Deducciones (2), Seguridad Social a cargo del Empleador (3), Parafiscales (4), Provisiones (5) o Incapacidades (6). Se utiliza para filtrar y clasificar los conceptos de liquidación de nómina según su naturaleza contable y laboral, facilitando el cálculo de la nómina, los informes de seguridad social y los reportes de parafiscales y provisiones laborales.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'FUNCTION', @level1name = N'GetConceptClassByType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'FUNCTION', @level1name = N'GetConceptClassByType';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve el catálogo de clases de concepto de nómina asociadas a un tipo (devengos, deducciones, seguridad social empleador, parafiscales, provisiones o incapacidades).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'GetConceptClassByType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de tipo debe estar entre 1 y 6; cualquier otro valor produce un resultado vacío sin error.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'GetConceptClassByType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las ramas son mutuamente excluyentes: para un mismo tipo solo se carga un único conjunto de clases.; Algunas clases se repiten entre tipos distintos (p.ej. 006, 011, 030 aparecen tanto en Devengos como en Deducciones; 021, 022, 023, 027 aparecen en Devengos e Incapacidades).; El catálogo de clases por tipo está hardcodeado en la función; no consulta ninguna tabla.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'GetConceptClassByType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Nómina; Concepto de nómina; Devengos; Deducciones; Seguridad Social del Empleador; Parafiscales; Provisiones; Incapacidades', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'GetConceptClassByType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @ListConceptClass: Si tipo=1 (Devengos), inserta 28 clases: 001-007, 010-013, 021-025, 027, 029, 030, 042, 043, 046, 047, 049-052, 055.; [INSERT] @ListConceptClass: Si tipo=2 (Deducciones), inserta 9 clases: 006, 011, 014, 017, 020, 030, 038, 041, 044.; [INSERT] @ListConceptClass: Si tipo=3 (Seguridad Social Empleador), inserta 3 clases: 009, 015, 018.; [INSERT] @ListConceptClass: Si tipo=4 (Parafiscales), inserta 3 clases: 035, 036, 037.; [INSERT] @ListConceptClass: Si tipo=5 (Provisiones), inserta 9 clases: 008, 031, 033, 034, 056, 057, 058, 059, 060.; [INSERT] @ListConceptClass: Si tipo=6 (Incapacidades), inserta 4 clases: 021, 022, 023, 027.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'GetConceptClassByType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Type = 1 → Carga clases de Devengos else Evalúa siguiente rama; si @Type = 2 → Carga clases de Deducciones; si @Type = 3 → Carga clases de Seguridad Social Empleador; si @Type = 4 → Carga clases de Parafiscales; si @Type = 5 → Carga clases de Provisiones; si @Type = 6 → Carga clases de Incapacidades else Retorna tabla vacía', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'GetConceptClassByType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'GetConceptClassByType';
GO
