CREATE PROCEDURE [dbo].[SPMOV_AutentificacionUsuarios]
(
@Usuario Char(15),
@Password nvarchar(50)
)
AS
BEGIN
	SET NOCOUNT ON;

	Select  EstadoValidacion = 1,
		RTRIM(A.CODUSUARI) as Codigo,
		RTRIM(A.NOMUSUARI) as Nombre,
		RTRIM(A.USUEMAILE) as UserEmail,
		RTRIM(A.DESCARUSU) as Cargo,
	    case 
			when RTRIM(C.CODCENATE) is null then ''
			else RTRIM(C.CODCENATE)
		end as CodigoCentro,
		case 
			when RTRIM(C.NOMCENATE) is null then ''
			else RTRIM(C.NOMCENATE)
		end as NombreCentro,
		case 
			when RTRIM(B.UFUCODIGO) is null then ''
			else RTRIM(B.UFUCODIGO)
		end as CodigoUnidad,
		case 
			when RTRIM(B.UFUDESCRI) is null then ''
			else RTRIM(B.UFUDESCRI)
		end as NombreUnidad
	from SEGusuaru as A
	left join dbo.INUNIFUNC B on B.UFUCODIGO = A.UFUCODIGO
	left join dbo.ADCENATEN C on C.CODCENATE = A.CODCENATE
	WHERE A.CODUSUARI = @Usuario AND A.PASSUSUAR = @Password
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autentica a un usuario del sistema verificando su nombre de usuario y contraseña contra el registro de usuarios de seguridad. Si las credenciales son correctas, retorna los datos del usuario: código, nombre, correo electrónico y cargo, junto con el centro de atención y la unidad funcional (servicio o área) a los que está asociado. Este procedimiento es el punto de entrada al sistema para el control de acceso, y combina información del catálogo de sedes y del catálogo de unidades funcionales para armar el perfil completo de sesión del usuario autenticado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPMOV_AutentificacionUsuarios';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPMOV_AutentificacionUsuarios';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida las credenciales de un usuario y, si son correctas, devuelve sus datos básicos junto con su centro de atención y unidad funcional asociados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_AutentificacionUsuarios';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El usuario debe existir en la tabla maestra de usuarios con el código y la contraseña indicados.; La contraseña debe coincidir exactamente con la almacenada (comparación directa, sensible al collation).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_AutentificacionUsuarios';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retorna información del usuario cuando coinciden exactamente código de usuario y contraseña.; Los códigos y nombres de centro y unidad nunca se devuelven NULL: si no existe relación, se retorna cadena vacía.; EstadoValidacion siempre se devuelve con valor fijo 1 cuando hay coincidencia (no se modela explícitamente el caso de fallo).; La contraseña se compara en texto plano contra la columna almacenada, sin hashing ni transformación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_AutentificacionUsuarios';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Usuario del sistema; Autenticación; Centro de atención; Unidad funcional; Cargo del usuario', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_AutentificacionUsuarios';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.SEGusuaru: Cuando CODUSUARI y PASSUSUAR coinciden con los parámetros, se devuelve un resultset con EstadoValidacion=1 y los datos del usuario, centro y unidad; si no coinciden, el resultset sale vacío.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_AutentificacionUsuarios';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.SEGusuaru; dbo.INUNIFUNC; dbo.ADCENATEN', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_AutentificacionUsuarios';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_AutentificacionUsuarios';
-- GO
