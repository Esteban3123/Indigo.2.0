-- =============================================
-- Author:      Duván Mejia Cortes
-- Create Date: 2022-03-01
-- Description: Lista los Diagnosticos & Procedimientos QX - NoQX - Cirugias 
-- =============================================
CREATE PROCEDURE [dbo].[SP_ANE_ListarDiagnosticosPaciente]
(
	@PatientCode varchar(25)
)
AS
BEGIN
	SET NOCOUNT ON

		SELECT TOP(1) AG.FECHORAIN currentDay,IPS.CODSERIPS ServiceCode,
				IPS.DESSERIPS ServiceDescription,
				DG.CODDIAGNO DiagnosisCode,
				DG.NOMDIAGNO DiagnosisName	 
		  FROM 
		       AGEPROGQX AG with(nolock) INNER JOIN 			  
		       INCUPSIPS IPS with(nolock) ON IPS.CODSERIPS = AG.CODSERIPS INNER JOIN	
		       INDIAGNOS DG  with(nolock) ON DG.CODDIAGNO = AG.DiagnosisCode
		 WHERE AG.IPCODPACI = @PatientCode AND AG.CODESTPQX <> '6' AND Cast(AG.FECHORAIN As Date) = Cast(Common.Getdate() As DATE)		   
         ORDER BY Cast(AG.FECHORAIN  as DATE) DESC
END