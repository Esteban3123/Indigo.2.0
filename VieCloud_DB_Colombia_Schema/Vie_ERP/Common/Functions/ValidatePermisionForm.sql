
-- =============================================  
-- Author:  Miguel Angel Fonseca  
-- ALTER date: 2019-05-09  
-- Description: Verifica si un usuario tiene un permiso especifico  
-- =============================================  
CREATE Function [Common].[ValidatePermisionForm]  
(  
 @UserCode VARCHAR(20),  
 @IdForm CHAR(3),  
 @Action VARCHAR(20)  
)  
RETURNS Tinyint  
AS  
BEGIN  
	Declare @valid BIT = 0  
   
	DECLARE @ArchitectureType TINYINT
	DECLARE @ContainerId INT
	DECLARE @RollId INT
	DECLARE @UserId INT
	DECLARE @UserType CHAR(1)
	DECLARE @TenantId AS INT = 0
	DECLARE @IdErpForm VARCHAR(4)
	DECLARE @SQL NVARCHAR(MAX)
	DECLARE @PARAMETROS NVARCHAR(500)
	DECLARE @CAMPO VARCHAR(5)
	DECLARE @DBName NVARCHAR(128)
	DECLARE @ActionId VARCHAR(3)
	SELECT @DBName = (SELECT DB_NAME() AS [Current Database])
	
	SELECT @ArchitectureType = ISNULL(ArchitectureType, 1), @ContainerId = C.Id FROM Security.ContainersInt C WHERE C.TransactionalContainer = @DBName
	if @ContainerId is NULL
	BEGIN
		SELECT @ArchitectureType = ISNULL(ArchitectureType, 1), @ContainerId = C.Id FROM Security.ContainersInt C WHERE C.HISContainer = @DBName
	END
	----1: On Premise
	--IF @ArchitectureType = 1
	--BEGIN
	--	DECLARE @SQL NVARCHAR(MAX)
	--	DECLARE @PARAMETROS NVARCHAR(500);
	--	DECLARE @CAMPO VARCHAR(5)
	--	SET @SQL = 'Select @valid = ' + @Action + ' From dbo.SEGpermiu where codusuari = ' + @UserCode + ' and indidmenu = ' + @IdForm
	--	SET @PARAMETROS = '@valid BIT OUTPUT'
	--	EXEC sys.[sp_executesql] @SQL, @PARAMETROS, @valid OUTPUT;
	--END
	----2: Plataform as service PAAS
	--IF @ArchitectureType = 2
	--BEGIN
		--SELECT @IdErpForm = IdErpForm from [Security].[FormRelationship] where IdHisForm = @IdForm
		set @IdErpForm = @IdForm
		IF @IdErpForm IS NULL
		BEGIN
			SET @SQL = 'Select @valid = ' + @Action + ' From dbo.SEGpermiu where codusuari = ' + @UserCode + ' and indidmenu = ' + @IdForm
			SET @PARAMETROS = '@valid BIT OUTPUT'
			EXEC sys.[sp_executesql] @SQL, @PARAMETROS, @valid OUTPUT;
		END
		ELSE
		BEGIN
			SELECT @UserId = Id,  @RollId = RollCode, @UserType = UserType  FROM Security.[UserInt] WHERE UserCode = @UserCode
			IF @UserId IS NULL
			BEGIN
				SET @SQL = 'Select @valid = ' + @Action + ' From dbo.SEGpermiu where codusuari = ' + @UserCode + ' and indidmenu = ' + @IdForm
				SET @PARAMETROS = '@valid BIT OUTPUT'
				EXEC sys.[sp_executesql] @SQL, @PARAMETROS, @valid OUTPUT;
			END
			ELSE
			BEGIN
				SELECT @ActionId = CASE @Action
										WHEN 'ioptguard' THEN '2'
										WHEN 'ioptconsu' THEN '40'
										WHEN 'ioptactua' THEN  '3'
										WHEN 'ioptelimi' THEN  '1'
										WHEN 'ioptconfi' THEN  '7'
										WHEN 'ioptanula' THEN  '8'
										WHEN 'ioptimpri' THEN  '23'
										WHEN 'ioptdisen' THEN  '130'
										WHEN 'ioptnaveg' THEN  '131'
										WHEN 'igricrear' THEN  '132'
										WHEN 'igrimodif' THEN  '133'
										WHEN 'igrielimi' THEN  '134'
										WHEN 'ioptvisib' THEN '41'
										ELSE '0'
									END
				IF @UserType <> 3
				BEGIN
					SELECT @TenantId = TC.TenantId FROM Security.TenantContainerInt TC
					WHERE TC.ContainerId = @ContainerId

					SELECT @RollId = TU.RollId FROM Security.TenantUsersInt TU
					WHERE TU.UserId = @UserId AND TU.TenantId = @TenantId AND TU.[State] = 1
				END
				SELECT @valid = PR.ActionValue FROM Security.PermissionRollInt PR
				WHERE PR.IdRoll = @RollId AND PR.IdForm = @IdErpForm AND PR.[Action] = @ActionId
				IF (@VALID IS NULL OR @VALID = 0) AND @UserType <> 3
				BEGIN
					SELECT @valid = PU.ActionValue FROM Security.PermissionUserInt PU
					WHERE PU.IdUser = @UserId AND PU.TenantId = @TenantId AND PU.IdForm = @IdErpForm AND PU.[Action] = @ActionId
				END
			END
		END
	--END
	RETURN CAST(ISNULL(@valid,CAST(0 AS BIT))  AS TINYINT)
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar de seguridad que verifica si un usuario tiene permiso para ejecutar una acción específica (guardar, consultar, actualizar, eliminar, imprimir, anular, etc.) sobre un formulario o pantalla del sistema. Recibe el código de usuario, el identificador del formulario y la acción a validar, y devuelve 1 si el permiso está habilitado o 0 si no. La función resuelve el permiso en dos niveles: primero busca el permiso heredado del rol asignado al usuario en el tenant (empresa/organización) correspondiente consultando las tablas de seguridad de contenedores, usuarios, roles y permisos por rol; si no encuentra permiso por rol, verifica si existe un permiso individual asignado directamente al usuario. Es el mecanismo central de control de acceso del ERP/EHR Indigo Vie Cloud, utilizado para habilitar o deshabilitar funcionalidades en la interfaz según los permisos configurados por rol o por usuario.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'FUNCTION', @level1name = N'ValidatePermisionForm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'FUNCTION', @level1name = N'ValidatePermisionForm';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Determina si un usuario tiene permiso para ejecutar una acción específica sobre un formulario, evaluando primero permisos legacy, luego permisos por rol y finalmente permisos individuales en el modelo multi-tenant.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'ValidatePermisionForm';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El nombre de la base de datos actual debe estar registrado en Security.ContainersInt como TransactionalContainer o HISContainer para resolver el ContainerId y ArchitectureType.; El parámetro de acción debe corresponder a un nombre de columna válido en dbo.SEGpermiu cuando se usa la ruta legacy (se concatena dinámicamente en SQL).; Para evaluación por rol/usuario en modelo multi-tenant, el usuario debe existir en Security.UserInt.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'ValidatePermisionForm';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El valor retornado siempre es TINYINT 0 o 1; nunca NULL (se aplica ISNULL al final).; Las acciones se mapean a códigos numéricos fijos: ioptguard=2, ioptconsu=40, ioptactua=3, ioptelimi=1, ioptconfi=7, ioptanula=8, ioptimpri=23, ioptdisen=130, ioptnaveg=131, igricrear=132, igrimodif=133, igrielimi=134, ioptvisib=41; cualquier otra acción se mapea a 0.; Los usuarios con UserType = 3 no usan TenantUsersInt ni el fallback por permiso individual; sólo se evalúan por rol.; Sólo se consideran asignaciones de rol por tenant con State = 1.; El permiso individual de usuario sólo aplica como complemento cuando el permiso por rol no concede acceso.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'ValidatePermisionForm';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Permisos de usuario; Permisos por rol; Formularios/pantallas del sistema; Multi-tenant (inquilinos); Contenedores transaccionales/HIS; Acciones de UI (guardar, consultar, actualizar, eliminar, confirmar, anular, imprimir, diseñar, navegar, visibilidad, grilla crear/modificar/eliminar)', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'ValidatePermisionForm';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Retorna TINYINT 1 si el permiso está concedido, 0 en caso contrario (incluye NULL convertido a 0).; [RETURN_RESULT] dbo.SEGpermiu: Si el formulario no tiene mapeo ERP (IdErpForm IS NULL) o el usuario no existe en Security.UserInt, se consulta dinámicamente la columna correspondiente a la acción en SEGpermiu filtrando por codusuari e indidmenu.; [RETURN_RESULT] Security.PermissionRollInt: Cuando el usuario existe en UserInt, se obtiene ActionValue del rol (IdRoll, IdForm, Action mapeada) como primer criterio de permiso.; [RETURN_RESULT] Security.PermissionUserInt: Si el permiso por rol es NULL o 0 y el UserType <> 3, se consulta el permiso individual del usuario en el tenant como fallback.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'ValidatePermisionForm';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ContainerId no se encuentra por TransactionalContainer → Reintenta búsqueda por HISContainer en Security.ContainersInt; si IdErpForm IS NULL (sin mapeo ERP) → Evalúa permiso vía SQL dinámico sobre dbo.SEGpermiu (ruta legacy) else Evalúa permiso vía esquema multi-tenant (PermissionRollInt/PermissionUserInt); si Usuario no existe en Security.UserInt → Cae a evaluación legacy por dbo.SEGpermiu; si UserType <> 3 → Resuelve TenantId desde TenantContainerInt y sobreescribe RollId con el rol activo del usuario en TenantUsersInt (State=1) else Mantiene RollId proveniente de UserInt sin filtro por tenant; si Permiso por rol es NULL o 0 y UserType <> 3 → Consulta permiso individual en Security.PermissionUserInt como respaldo', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'ValidatePermisionForm';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'sys.sp_executesql', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'ValidatePermisionForm';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Security.ContainersInt; dbo.SEGpermiu; Security.UserInt; Security.TenantContainerInt; Security.TenantUsersInt; Security.PermissionRollInt; Security.PermissionUserInt', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'ValidatePermisionForm';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'FUNCTION', @level1name=N'ValidatePermisionForm';
GO
