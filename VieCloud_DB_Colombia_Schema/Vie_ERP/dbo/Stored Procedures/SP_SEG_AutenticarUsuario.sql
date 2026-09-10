

CREATE PROCEDURE [dbo].[SP_SEG_AutenticarUsuario]
(
@PKeyUser Char(20)
)
AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @CODUSUARI CHAR(20) = NULL
	SELECT TOP 1 @CODUSUARI = CODUSUARI FROM dbo.SEGusuaru WHERE CODUSUARI = @PKeyUser
	
	IF @CODUSUARI iS NULL
	BEGIN
		DECLARE @ArchitectureType TINYINT
		DECLARE @ContainerId INT
		DECLARE @RollCode CHAR(3)
		DECLARE @RollDescri CHAR(60)
		DECLARE @GroupCode VARCHAR(3)
		DECLARE @TenantId AS INT = 0
		DECLARE @Administrator as bit
		DECLARE @DBName NVARCHAR(128)
		SELECT @DBName = (SELECT DB_NAME() AS [Current Database])

		SELECT @ArchitectureType = ISNULL(ArchitectureType, 1), @ContainerId = C.Id FROM Security.ContainersInt C WHERE C.HISContainer = @DBName
		if @ContainerId is NULL
		BEGIN
			SELECT @ArchitectureType = ISNULL(ArchitectureType, 1), @ContainerId = C.Id FROM Security.ContainersInt C WHERE C.TransactionalContainer = @DBName
		END

		SELECT @TenantId = TC.TenantId FROM Security.TenantContainerInt TC WHERE TC.ContainerId = @ContainerId

		SELECT @RollCode = R.RollCode, @RollDescri = R.Description, @GroupCode = G.Code,
			@Administrator = CASE WHEN U.UserType = '3' THEN CAST(1 AS BIT)
								WHEN U.UserType = '2' THEN CAST(1 AS BIT)
								WHEN U.UserType = '1' AND TU.ManageCompany = 1 THEN CAST(1 AS BIT)
								ELSE CAST(0 AS BIT) END
		FROM Security.[UserInt] U
		LEFT OUTER JOIN Security.TenantUsersInt TU ON TU.UserId = U.Id AND TU.TenantId = @TenantId
		LEFT OUTER JOIN SECURITY.RollInt R on R.ID = CASE WHEN U.UserType = '3' THEN U.RollCode ELSE TU.RollId END
		LEFT OUTER JOIN SECURITY.[Group] G on G.ID = CASE WHEN U.UserType = '3' THEN U.GroupCode ELSE TU.GroupId END
		WHERE UserCode = @PKeyUser

		If NOT EXISTS (SELECT 1 FROM SEGrolesu WHERE codigorol = @RollCode) Begin
			Insert Into SEGrolesu (codigorol, descrirol)
			VALUES (@RollCode, @RollDescri)
		End

		INSERT INTO dbo.SEGusuaru (CODUSUARI,NOMUSUARI,PASSUSUAR,CODUSUDGH,USUACTIVO,CODIGOROL,CODGRUPOU,USUEMAILE,DESCARUSU,TIPPERUSU,SOLCAMCON,DIACAMCON,FECULTCAM,USUADMINI)
		SELECT U.UserCode, P.Fullname, U.Password,'', U.State, @RollCode, @GroupCode, U.EMAIL, U.Position, U.ProfileType, CAST(0 AS BIT),CAST(0 AS BIT), [Common].[GETDATE](), @Administrator
		FROM Security.[UserInt] U
		INNER JOIN Security.PersonInt P
		ON P.Id = U.IdPerson
		WHERE U.UserCode = @PKeyUser
	END
	SELECT
		U.SOLCAMCON
	   ,CODUSUARI AS CODIGO
	   ,NOMUSUARI AS NOMBREUSUARIO
	   ,PASSUSUAR AS PASSWORD
	   ,CODUSUDGH AS CODIGOINTERFAZ
	   ,USUACTIVO AS ESTADO
	   ,R.CODIGOROL AS ROL
	   ,G.CODGRUPOU AS GRUPO
	   ,USUEMAILC AS SIP
	   ,USUEMAILE AS EMAIL
	   ,DESCARUSU AS CARGO
	   ,TIPPERUSU AS TIPO
	   ,CASE
			WHEN DIACAMCON = 0 THEN 0
			WHEN DATEADD(D, DIACAMCON, FECULTCAM) > [Common].[GETDATE]() THEN 0
			ELSE 1
		END
		AS CAMBIARPASSW
	   ,DATEDIFF(D, FECADCUE, [Common].[GETDATE]()) AS DIASCADUCACUENTA
	   ,PERASISTE AS DASHBOARDDEFAULT
	   ,CODCENATE AS CENTROATENCION
	   ,UFUCODIGO AS UNIDADFUNCIONAL
	   ,USUADMINI AS ADMINISTRADOR
	   ,R.descrirol AS ROLDESC
	   ,G.descrigru AS GRUDESC

	FROM dbo.SEGusuaru AS U
	INNER JOIN dbo.SEGrolesu AS R
		ON U.CODIGOROL = R.codigorol
	INNER JOIN dbo.SEGgruusu AS G
		ON U.CODGRUPOU = G.codgrupou
	WHERE CODUSUARI = @PKeyUser
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autentica un usuario en Indigo Vie Cloud dado su código de usuario. Si el usuario no existe aún en la tabla local de seguridad (SEGusuaru), lo busca en el modelo centralizado de seguridad multiempresa (Security.User, Security.TenantUsers) para obtener su rol, grupo y tipo de perfil, y lo registra automáticamente en la base de datos local junto con su rol si tampoco existía. Finalmente devuelve los datos completos del usuario autenticado: nombre, contraseña, estado, rol, grupo, email, cargo, tipo de perfil, si debe cambiar contraseña, días de caducidad de cuenta, centro de atención, unidad funcional y si es administrador.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_SEG_AutenticarUsuario';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_SEG_AutenticarUsuario';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Autentica/sincroniza un usuario: si no existe en la tabla local de seguridad lo aprovisiona desde el esquema multi-tenant Security y devuelve sus datos de sesión, rol, grupo y estado de contraseña.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SEG_AutenticarUsuario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El usuario debe existir en Security.UserInt (con su Persona en Security.PersonInt) si no está aún en dbo.SEGusuaru, para poder aprovisionarlo.; La base de datos actual debe estar registrada en Security.ContainersInt como HISContainer o TransactionalContainer para resolver el ContainerId y TenantId.; Debe existir el grupo referenciado en dbo.SEGgruusu, ya que el SELECT final hace INNER JOIN sobre CODGRUPOU.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SEG_AutenticarUsuario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Un usuario aprovisionado siempre queda con un rol existente en SEGrolesu (se crea antes si falta).; El aprovisionamiento solo ocurre la primera vez que un usuario se autentica localmente; ejecuciones posteriores no reinsertan ni actualizan SEGusuaru.; La condición de administrador se deriva de UserType y, para usuarios tipo 1, de la marca ManageCompany del tenant correspondiente.; El TenantId utilizado siempre corresponde al contenedor que coincide con la BD actual (HIS o Transaccional).; CAMBIARPASSW nunca se solicita si DIACAMCON = 0 (política de no caducidad).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SEG_AutenticarUsuario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Autenticación de usuario; Aprovisionamiento de usuarios desde plataforma multi-tenant; Roles y grupos de seguridad; Tenant/Contenedor (HIS y Transaccional); Caducidad y cambio de contraseña; Usuario administrador; Centro de atención y unidad funcional', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SEG_AutenticarUsuario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] dbo.SEGrolesu: Cuando el usuario no existe localmente y el rol resuelto desde Security.RollInt no está en SEGrolesu, se inserta el rol con su código y descripción.; [INSERT] dbo.SEGusuaru: Cuando el usuario no existe en SEGusuaru, se aprovisiona desde Security.UserInt + Security.PersonInt, marcando USUADMINI=1 si UserType=3, UserType=2, o (UserType=1 y TenantUsersInt.ManageCompany=1); en otro caso 0. Se inicializan SOLCAMCON=0, DIACAMCON=0 y FECULTCAM con la fecha actual.; [RETURN_RESULT] dbo.SEGusuaru: Siempre se devuelve el registro del usuario con su rol y grupo, calculando CAMBIARPASSW=1 cuando DIACAMCON>0 y DATEADD(D,DIACAMCON,FECULTCAM) ya venció; 0 en otro caso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SEG_AutenticarUsuario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si El usuario solicitado no se encuentra en dbo.SEGusuaru → Resuelve contenedor/tenant, obtiene rol/grupo desde el esquema Security y aprovisiona el rol (si falta) y el usuario en las tablas locales SEGrolesu/SEGusuaru. else Omite el aprovisionamiento y solo retorna los datos existentes.; si La BD actual no coincide con HISContainer en Security.ContainersInt → Reintenta la búsqueda del contenedor usando TransactionalContainer para obtener ArchitectureType y ContainerId.; si U.UserType = ''3'' → Marca el usuario como administrador y toma RollCode/GroupCode directamente de Security.UserInt. else Para UserType ''2'' marca administrador; para ''1'' depende de TenantUsersInt.ManageCompany; y el rol/grupo se toma de TenantUsersInt.; si DIACAMCON = 0 o la fecha de cambio aún no venció → Devuelve CAMBIARPASSW = 0 (no requiere cambio de contraseña). else Devuelve CAMBIARPASSW = 1 indicando que debe cambiar contraseña.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SEG_AutenticarUsuario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.SEGusuaru; Security.ContainersInt; Security.TenantContainerInt; Security.UserInt; Security.TenantUsersInt; SECURITY.RollInt; SECURITY.Group; Security.PersonInt; dbo.SEGrolesu; dbo.SEGgruusu', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SEG_AutenticarUsuario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SEG_AutenticarUsuario';
-- GO
