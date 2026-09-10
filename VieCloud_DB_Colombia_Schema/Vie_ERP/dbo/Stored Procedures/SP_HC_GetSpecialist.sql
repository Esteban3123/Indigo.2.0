CREATE PROC [dbo].[SP_HC_GetSpecialist]
@IDNumber char(15)
AS
SELECT		U.CODUSUARI AS Code,
			P.CODIGONIT AS IDNumber,
			U.NOMUSUARI as Name,
			U.USUADMINI AS IsAdministrator,
			p.CODESPEC1 AS FirstSpecialty,
			ISNULL(P.CODESPEC2, '0') AS SecondSpecialty,
			ISNULL(P.CODESPEC3, '0')AS ThirdSpecialty
FROM		SEGusuaru AS U 
INNER JOIN	INPROFSAL AS P ON U.CODUSUARI = P.CODPROSAL
WHERE		P.CODIGONIT = @IDNumber
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta los datos de un profesional de la salud (médico, especialista u otro prestador) a partir de su número de identificación (NIT o cédula). Cruza el maestro de profesionales de la salud con el registro de usuarios del sistema para devolver el código de usuario, nombre, indicador de administrador y hasta tres especialidades asociadas. Se usa para identificar y validar un especialista dentro de la historia clínica o flujos de atención médica, por ejemplo al asignar un profesional a una orden, consulta o episodio de cuidado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_GetSpecialist';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_GetSpecialist';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene los datos básicos y especialidades de un profesional de la salud a partir de su número de identificación, junto con su rol de usuario.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_GetSpecialist';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un profesional en INPROFSAL cuyo CODIGONIT coincida con el identificador recibido.; El profesional debe tener un usuario asociado en SEGusuaru mediante CODPROSAL = CODUSUARI.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_GetSpecialist';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Si la segunda o tercera especialidad son NULL, se sustituyen por ''0''.; Solo se retornan profesionales que también existan como usuarios del sistema (INNER JOIN).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_GetSpecialist';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Profesional de la salud; Especialidad médica; Usuario del sistema; Administrador', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_GetSpecialist';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] SEGusuaru: Retorna código, nombre y bandera de administrador del usuario unido al profesional.; [RETURN_RESULT] INPROFSAL: Retorna identificación y hasta 3 especialidades del profesional filtrando por CODIGONIT = parámetro.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_GetSpecialist';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.SEGusuaru; dbo.INPROFSAL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_GetSpecialist';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_GetSpecialist';
-- GO
