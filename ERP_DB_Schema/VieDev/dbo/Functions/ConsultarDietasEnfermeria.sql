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