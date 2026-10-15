

CREATE FUNCTION [dbo].[MotherCurrentBed] (@PatientCode as Varchar(25), @AdmissionNumber as Char(10))
RETURNS int 
AS
BEGIN


declare @BedCode int
	
 SET @BedCode  = (SELECT TOP 1 ADINGR.CODCAMACT FROM HCRECINAC HCREC INNER JOIN ADINGRESO ADINGR ON HCREC.NUMINGRES = ADINGR.NUMINGRES
						   WHERE HCREC.IPCODPACIHIJO = @PatientCode AND HCREC.NUMINGRESHIJO = @AdmissionNumber)
/*
Función que me va a retornar el código de la cama de la madre de un hijo
*/

RETURN @BedCode

END