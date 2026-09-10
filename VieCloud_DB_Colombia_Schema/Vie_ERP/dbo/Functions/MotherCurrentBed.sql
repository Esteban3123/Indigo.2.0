

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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que, dado el código del paciente recién nacido (hijo) y su número de ingreso, retorna el código de la cama actual donde se encuentra hospitalizada la madre. Cruza el registro de nacimientos (HCRECINAC) con los ingresos o admisiones (ADINGRESO) para localizar el episodio de ingreso de la madre vinculado al parto y obtener la cama activa asignada. Es útil en contextos de maternidad y neonatología para identificar la ubicación física de la madre a partir de los datos del recién nacido.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'MotherCurrentBed';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'MotherCurrentBed';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene el código de la cama actualmente asignada a la madre asociada al ingreso de un recién nacido (hijo) hospitalizado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MotherCurrentBed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCRECINAC que vincule al paciente hijo y su número de ingreso con el ingreso de la madre; El ingreso de la madre debe existir en ADINGRESO con cama actual asignada', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MotherCurrentBed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Retorna un único valor (TOP 1) aun si existieran múltiples vínculos madre-hijo; La relación madre-hijo se resuelve mediante HCRECINAC enlazando al hijo con el NUMINGRES (ingreso de la madre)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MotherCurrentBed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Recién nacido; Madre; Ingreso hospitalario; Cama actual; Vínculo madre-hijo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MotherCurrentBed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] -: Devuelve CODCAMACT del ingreso de la madre cuando HCREC.IPCODPACIHIJO y HCREC.NUMINGRESHIJO coinciden con los parámetros; NULL si no hay coincidencia', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MotherCurrentBed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCRECINAC; dbo.ADINGRESO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MotherCurrentBed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MotherCurrentBed';
GO
