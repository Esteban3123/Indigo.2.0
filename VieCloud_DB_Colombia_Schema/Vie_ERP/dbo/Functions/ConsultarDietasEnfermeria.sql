CREATE FUNCTION [dbo].[ConsultarDietasEnfermeria]
(
 @PatientId VARCHAR(25),
 @AdmissionNumber VARCHAR(20),
 @DietId INT
)
RETURNS VARCHAR(2000)
AS
BEGIN
	DECLARE @value VARCHAR(2000)

	-- Se trae descripcion concatenada de dietas en tabla de histórico de dietas
		BEGIN 
		SET @value = (SELECT STUFF((SELECT ', ' + RTRIM(x.DESTIPDIE)
		FROM MedicalDiet.DietPatientDetail AS Diet
		INNER JOIN MedicalDiet.DietControlNursing DCN ON Diet.DietControlNursingId = DCN.Id
		INNER JOIN CHTIPDIET X ON Diet.DietTypeCode = X.CODTIPDIE
		WHERE Diet.DietControlNursingId = @DietId AND DCN.PatientCode = @PatientId AND DCN.AdmissionNumber = @AdmissionNumber AND (X.BreastMilk IS NULL OR X.BreastMilk !=1)
			FOR XML PATH('')),1,1,'') as dietName)
		END
RETURN @value
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que retorna, en un único texto concatenado, los nombres de las dietas asignadas a un paciente hospitalizado para un control de enfermería específico. Recibe como parámetros el código o cédula del paciente, el número de ingreso o admisión y el identificador del registro de control de dieta; con ellos cruza el detalle de dietas del paciente (DietPatientDetail), el control de enfermería (DietControlNursing) y el catálogo de tipos de dieta (CHTIPDIET), excluyendo las dietas de leche materna. Se utiliza para mostrar en pantalla o en reportes de enfermería la lista legible de dietas prescritas a un paciente durante su hospitalización, como por ejemplo ''Dieta blanda, Sin sal, Hipocalórica''.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'ConsultarDietasEnfermeria';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'ConsultarDietasEnfermeria';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve, en una sola cadena separada por comas, las descripciones de las dietas prescritas a un paciente durante una admisión, excluyendo dietas de leche materna, para visualización en enfermería.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ConsultarDietasEnfermeria';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un control de dietas de enfermería identificado que corresponda al paciente y a la admisión indicados; El detalle de dieta debe estar enlazado a un tipo de dieta presente en el catálogo CHTIPDIET', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ConsultarDietasEnfermeria';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las dietas marcadas como leche materna (BreastMilk = 1) nunca aparecen en el resultado; El resultado solo contiene dietas pertenecientes al control de enfermería, paciente y admisión indicados; El primer separador '', '' inicial es removido por STUFF, garantizando una lista limpia', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ConsultarDietasEnfermeria';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Dieta de paciente; Control de dietas por enfermería; Tipo de dieta; Leche materna; Admisión hospitalaria', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ConsultarDietasEnfermeria';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Retorna VARCHAR(2000) con las descripciones de tipo de dieta concatenadas con '', '' usando STUFF + FOR XML PATH; si no hay coincidencias retorna NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ConsultarDietasEnfermeria';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si X.BreastMilk IS NULL OR X.BreastMilk != 1 → Se incluye la descripción del tipo de dieta en el resultado concatenado else Se excluye la dieta (caso leche materna)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ConsultarDietasEnfermeria';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MedicalDiet.DietPatientDetail; MedicalDiet.DietControlNursing; dbo.CHTIPDIET', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ConsultarDietasEnfermeria';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ConsultarDietasEnfermeria';
GO
