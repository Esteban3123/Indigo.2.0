
CREATE PROC [dbo].[SP_HC_ConsultarDatosUsuario]
@userCode char(15)
AS
SELECT		rtrim(U.CODUSUARI) AS Code,
			rtrim(U.NOMUSUARI) as Name,
			U.USUADMINI AS IsAdministrator,
			p.TIPPROFES AS Profession
FROM		SEGusuaru AS U 
INNER JOIN	INPROFSAL AS P ON U.CODUSUARI = P.CODPROSAL
WHERE		u.CODUSUARI = @userCode
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta los datos básicos de un usuario del sistema identificado por su código de acceso, combinando su información de seguridad (nombre, rol de administrador) con su perfil como profesional de la salud (tipo de profesión). Une el registro de usuario con el maestro de profesionales para devolver en una sola consulta el código, nombre, indicador de administrador y tipo de profesión. Se usa para identificar y caracterizar al usuario que opera el sistema, especialmente en módulos de historia clínica donde es necesario conocer el tipo de profesional que registra atenciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ConsultarDatosUsuario';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ConsultarDatosUsuario';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtener los datos básicos de un usuario del sistema junto con su profesión y rol administrativo, restringido a quienes están registrados como profesionales de la salud.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ConsultarDatosUsuario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en SEGusuaru con el código solicitado.; El mismo código debe estar registrado en INPROFSAL como CODPROSAL para que el INNER JOIN devuelva resultados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ConsultarDatosUsuario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna usuarios que además existen como profesional de la salud (INNER JOIN entre SEGusuaru e INPROFSAL por código de usuario = código profesional).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ConsultarDatosUsuario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Usuario; Profesional de la salud; Profesión; Administrador del sistema', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ConsultarDatosUsuario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.SEGusuaru: Cuando el código coincide en SEGusuaru y existe correspondencia en INPROFSAL, se retorna código, nombre, indicador de administrador y tipo de profesión.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ConsultarDatosUsuario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.SEGusuaru; dbo.INPROFSAL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ConsultarDatosUsuario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ConsultarDatosUsuario';
-- GO
