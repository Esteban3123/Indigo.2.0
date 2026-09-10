

-- sp para listar las solicitudes autorizadas del usuario administrador logueado

CREATE PROCEDURE [dbo].[SP_Sol_ListarSolAdminAceptadas] 
@CodUser as char (20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
SELECT A.FECHAUTORI,A.CODUSUARI,A.COMAUTO ,
B.NOMUSUARI ,
C.CODUSUARI ,C.COMFECHA ,
D.NOMUSUARI AS USERSOLI
FROM SOL_AUTORI AS A 
INNER JOIN SEGusuaru  AS B ON A.CODUSUARI = B.CODUSUARI 
INNER JOIN SOLCOMPRA  AS C ON A.COMAUTO = C.COMAUTON 
INNER JOIN SEGusuaru  AS D ON C.CODUSUARI = D.CODUSUARI 
WHERE A.CODUSUARI = @CodUser 
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todas las solicitudes de compra que han sido autorizadas (aceptadas) por un administrador específico, identificado por su código de usuario. Cruza las autorizaciones registradas en SOL_AUTORI con los datos del usuario autorizador (SEGusuaru), los detalles de la solicitud de compra (SOLCOMPRA) y el nombre del usuario que originalmente generó la solicitud. Se usa para que cada administrador pueda consultar el historial de solicitudes administrativas que él mismo ha aprobado, mostrando la fecha de autorización, la fecha de la solicitud, el número de la compra y los nombres del autorizador y del solicitante.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_Sol_ListarSolAdminAceptadas';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_Sol_ListarSolAdminAceptadas';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las solicitudes de compra autorizadas por un usuario administrador, mostrando datos de la autorización junto con información del autorizador y del solicitante.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Sol_ListarSolAdminAceptadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El usuario administrador debe existir en SEGusuaru y haber autorizado solicitudes en SOL_AUTORI.; Cada autorización debe tener una solicitud de compra correspondiente en SOLCOMPRA vinculada por COMAUTO/COMAUTON.; El usuario solicitante debe existir en SEGusuaru.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Sol_ListarSolAdminAceptadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen autorizaciones que tienen una solicitud de compra existente (INNER JOIN con SOLCOMPRA).; Solo se incluyen registros donde tanto el autorizador como el solicitante existen en el catálogo de usuarios.; El resultado se restringe exclusivamente a autorizaciones realizadas por el usuario administrador indicado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Sol_ListarSolAdminAceptadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Solicitud de compra; Autorización de solicitud; Usuario administrador; Usuario solicitante', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Sol_ListarSolAdminAceptadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] SOL_AUTORI: Devuelve fecha de autorización, código de autorizador, código de solicitud autorizada, nombre del autorizador, fecha de compra y nombre del solicitante, filtrando por CODUSUARI igual al usuario administrador recibido.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Sol_ListarSolAdminAceptadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.SOL_AUTORI; dbo.SEGusuaru; dbo.SOLCOMPRA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Sol_ListarSolAdminAceptadas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Sol_ListarSolAdminAceptadas';
-- GO
