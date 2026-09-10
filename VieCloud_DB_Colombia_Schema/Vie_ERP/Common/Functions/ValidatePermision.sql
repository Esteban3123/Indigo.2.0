-- =============================================  
-- Author:  Miguel Angel Fonseca  
-- ALTER date: 2019-05-09  
-- Description: Verifica si un usuario tiene un permiso especifico  
-- =============================================  
CREATE Function [Common].[ValidatePermision]
(  
 @UserCode VARCHAR(20),  
 @IdForm VARCHAR(5),  
 @Action VARCHAR(5)  
)  
RETURNS BIT  
AS  
BEGIN  
	Declare @valid BIT = 0  
	DECLARE @ArchitectureType TINYINT
	DECLARE @ContainerId INT
	DECLARE @RollId INT
	DECLARE @UserId INT
	DECLARE @UserType CHAR(1)
	DECLARE @TenantId AS INT = 0
	DECLARE @DBName NVARCHAR(128)

	SELECT @DBName = (SELECT DB_NAME() AS [Current Database])
	SELECT @UserId = Id,  @RollId = RollCode, @UserType = UserType  FROM Security.[User] WHERE UserCode = @UserCode
			
	IF @UserType <> 3
	BEGIN
		SELECT @ArchitectureType = ISNULL(ArchitectureType, 1), @ContainerId = C.Id FROM Security.Containers C WHERE C.TransactionalContainer = @DBName
		if @ContainerId is NULL
		BEGIN
			SELECT @ArchitectureType = ISNULL(ArchitectureType, 1), @ContainerId = C.Id FROM Security.Containers C WHERE C.HISContainer = @DBName
		END

		SELECT @TenantId = TC.TenantId FROM Security.TenantContainer TC
		WHERE TC.ContainerId = @ContainerId

		SELECT @RollId = TU.RollId FROM Security.TenantUsers TU
		WHERE TU.UserId = @UserId AND TU.TenantId = @TenantId AND TU.[State] = 1
	END
	SELECT @valid = PR.ActionValue FROM Security.PermissionRoll PR
	WHERE PR.IdRoll = @RollId AND PR.IdForm = @IdForm AND PR.[Action] = @Action
	IF (@VALID IS NULL OR @VALID = 0) AND @UserType <> 3
	BEGIN
		SELECT @valid = PU.ActionValue FROM Security.PermissionUser PU
		WHERE PU.IdUser = @UserId AND PU.TenantId = @TenantId AND PU.IdForm = @IdForm AND PU.[Action] = @Action
	END
   
	 RETURN ISNULL(@valid, CAST(0 AS BIT))  
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de seguridad que verifica si un usuario tiene permiso para ejecutar una acción específica sobre un formulario o pantalla del sistema. Recibe el código de usuario, el identificador del formulario y la acción a validar (por ejemplo, leer, crear, editar o eliminar), y retorna verdadero o falso. Primero resuelve el rol del usuario dentro del tenant (empresa u organización) consultando la arquitectura de contenedores y la asignación de usuarios por tenant; luego consulta los permisos asociados al rol y, si no encuentra habilitación, revisa si el usuario tiene un permiso individual que lo sobrescriba. Es utilizada en todo el sistema para controlar el acceso a funcionalidades según el perfil y la organización del usuario autenticado.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'FUNCTION', @level1name = N'ValidatePermision';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'FUNCTION', @level1name = N'ValidatePermision';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Determina si un usuario tiene habilitada una acción específica sobre un formulario, evaluando permisos por rol en el tenant correspondiente y, en su defecto, permisos individuales.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'ValidatePermision';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El usuario debe existir en Security.User para obtener su Id, rol y tipo; La base de datos actual debe estar registrada como TransactionalContainer o HISContainer en Security.Containers para usuarios no tipo 3; Debe existir una relación en Security.TenantContainer para resolver el TenantId del contenedor', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'ValidatePermision';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El permiso por rol tiene precedencia: solo se evalúa permiso individual si el rol no concede acceso; Los usuarios con UserType=3 no se evalúan contra tenant ni contra permisos individuales; su autorización depende exclusivamente de su rol base; El permiso individual solo aplica dentro del tenant resuelto a partir del contenedor (BD actual); Nunca retorna NULL: si no hay coincidencias devuelve 0 (false); El rol efectivo para validación multi-tenant es el de TenantUsers, no el RollCode de Security.User', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'ValidatePermision';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Usuario; Rol; Permiso por rol; Permiso por usuario; Tenant; Contenedor transaccional; Contenedor HIS; Formulario; Acción; Multi-tenant', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'ValidatePermision';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (retorno BIT): Devuelve 1 si PermissionRoll.ActionValue=1 para el rol/form/action; si es nulo o 0 y UserType<>3, devuelve el valor de PermissionUser.ActionValue; en cualquier otro caso devuelve 0', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'ValidatePermision';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @UserType <> 3 → Resuelve ContainerId desde TransactionalContainer o HISContainer, obtiene TenantId y sobrescribe el RollId con el rol asignado al usuario en ese tenant (TenantUsers.State=1) else Conserva el RollCode original del usuario sin resolver tenant ni rol por tenant (usuario tipo 3 se trata como global/superusuario respecto a multi-tenant); si ContainerId es NULL al buscar por TransactionalContainer → Reintenta la búsqueda en Security.Containers usando HISContainer = nombre de BD actual; si (@valid IS NULL OR @valid = 0) AND @UserType <> 3 → Consulta permiso individual en Security.PermissionUser filtrando por usuario, tenant, formulario y acción else Mantiene el valor obtenido del permiso por rol', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'ValidatePermision';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Security.User; Security.Containers; Security.TenantContainer; Security.TenantUsers; Security.PermissionRoll; Security.PermissionUser', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'ValidatePermision';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'ValidatePermision';
GO
