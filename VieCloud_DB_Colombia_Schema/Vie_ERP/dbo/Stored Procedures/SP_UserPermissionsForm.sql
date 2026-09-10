CREATE PROCEDURE [dbo].[SP_UserPermissionsForm]
(
    -- Add the parameters for the stored procedure here
    @UserCode VARCHAR(20),  
	@IdForm VARCHAR(5) --es nulo o vacio si se llama desde el formulario de usuario en el hist

)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Insert statements for procedure here
    DECLARE @ArchitectureType TINYINT
	DECLARE @ContainerId INT
	DECLARE @UserId INT
	DECLARE @UserType CHAR(1)
	DECLARE @TenantId AS INT = 0
	DECLARE @IdErpForm VARCHAR(5)
	DECLARE @DBName NVARCHAR(128)
	DECLARE @Tabla as TABLE
			(
				codusuari VARCHAR(20),
				indidmenu VARCHAR(5),
				ioptguard TINYINT,
				ioptconsu TINYINT,
				ioptdisen TINYINT,
				ioptactua TINYINT,
				ioptelimi TINYINT,
				ioptnaveg TINYINT,
				ioptconfi TINYINT,
				ioptanula TINYINT,
				ioptimpri TINYINT,
				igricrear TINYINT,
				igrimodif TINYINT,
				igrielimi TINYINT,
				ioptvisib TINYINT,
				customizable TINYINT,
				PreAltaHospitalaria  TINYINT,
				SoporteVieliberarCamaDobleEstancia   TINYINT, 
				SoporteVieEliminarRegistroEgreso   TINYINT,
				SustituirAnexosPaciente TINYINT,
				AnulacionAnexosPaciente TINYINT
			)

	SELECT @DBName = (SELECT DB_NAME() AS [Current Database])

	SELECT @ArchitectureType = ISNULL(ArchitectureType, 1), @ContainerId = C.Id FROM Security.ContainersInt C  WHERE C.HISContainer = @DBName
	if @ContainerId is NULL
	BEGIN
		SELECT @ArchitectureType = ISNULL(ArchitectureType, 1), @ContainerId = C.Id FROM Security.ContainersInt C  WHERE C.TransactionalContainer = @DBName
	END

	SELECT @IdErpForm = @IdForm --IdErpForm from [Security].[FormRelationship] where IdHisForm = @IdForm
	IF @IdErpForm IS NULL
	BEGIN
		SELECT @UserCode AS codusuari, @IdForm AS indidmenu, 
		CAST(0 AS BIT) ioptguard, 
		CAST(0 AS BIT) ioptconsu, 
		CAST(0 AS BIT) ioptdisen, 
		CAST(0 AS BIT) ioptactua,
		CAST(0 AS BIT) ioptelimi, 
		CAST(0 AS BIT) ioptnaveg,
		CAST(0 AS BIT) ioptconfi, 
		CAST(0 AS BIT) ioptanula, 
		CAST(0 AS BIT) ioptimpri, 
		CAST(0 AS BIT) igricrear,
		CAST(0 AS BIT) igrimodif, 
		CAST(0 AS BIT) igrielimi, 
		CAST(0 AS BIT) ioptvisib, 
		CAST(0 AS BIT) customizable, 
		CAST(0 AS BIT) PreAltaHospitalaria,
		CAST(0 AS BIT) AS SoporteVieliberarCamaDobleEstancia,  
		CAST(0 AS BIT) AS SoporteVieEliminarRegistroEgreso ,
		CAST(0 AS BIT) AS SustituirAnexosPaciente,
		CAST(0 AS BIT) AS AnulacionAnexosPaciente   
	END
	ELSE
	BEGIN
		SELECT @UserId = Id,  @UserType = UserType  FROM Security.[User] WHERE UserCode = @UserCode

		IF @UserType = '3' --Administrador global
		BEGIN
		
			SELECT @UserCode AS codusuari, '0' AS indidmenu, 
			CAST(0 AS BIT) ioptguard, 
			CAST(0 AS BIT) ioptconsu, 
			CAST(0 AS BIT) ioptdisen, 
			CAST(0 AS BIT) ioptactua,
			CAST(0 AS BIT) ioptelimi, 
			CAST(0 AS BIT) ioptnaveg, 
			CAST(0 AS BIT) ioptconfi, 
			CAST(0 AS BIT) ioptanula, 
			CAST(0 AS BIT) ioptimpri, 
			CAST(0 AS BIT) igricrear,
			CAST(0 AS BIT) igrimodif, 
			CAST(0 AS BIT) igrielimi, 
			CAST(0 AS BIT) ioptvisib, 
			CAST(0 AS BIT) customizable, 
			CAST(0 AS BIT) PreAltaHospitalaria,
			CAST(0 AS BIT) AS SoporteVieliberarCamaDobleEstancia,
			CAST(0 AS BIT) AS SoporteVieEliminarRegistroEgreso ,
			CAST(0 AS BIT) AS SustituirAnexosPaciente,
		    CAST(0 AS BIT) AS AnulacionAnexosPaciente   
			END
		ELSE
		BEGIN
				IF @IdErpForm = '001'
				BEGIN
					select @IdErpForm = '1629' 
					set @IdForm = @IdErpForm
				END
				ELSE
				BEGIN
					IF @IdErpForm = '079'
					BEGIN
						select @IdErpForm = '2700'
						set @IdForm = @IdErpForm
					END
					ELSE
					BEGIN
						if  @IdErpForm IN ('904','965','475','808','2291','2629','1665','828','005','100','101', '109', '113', '114', '123','127', '138', '143', '167', '186', '193', '196', '197', '205','211', '215', '218', '219', '221', '225', '258', '259', '309', '314', '326', '327','401', '402', '403', '404','412', '413', '415', '418'
						, '419', '420', '425', '426', '427', '428', '429', '430', '431', '436', '437', '438', '439', '440', '441', '442', '443', '444', '446', '470','810', '815', '855', '888', '901', '953', '954', '959', '960', '961', '967', '971', '976', '977'
						, '978', '979', '980', '982', '983', '984', '985', '986', '987', '988', '989','990','991', '992', '993', '994', '995', '996', '997', '998','999','840','320'
						,'206','406','407','408','409','410','463','464','465','466','468','330','331','476', '323', '322', '324', '325', '421', '432', '433', '434', '435', '445', '455', '908', '909', '910'
						) OR (SUBSTRING(@IdErpForm,1,1) = '0')
						BEGIN
							select @IdErpForm = IdErpForm from Security.FormRelationshipInt where IdHisForm = @IdErpForm
							set @IdForm = @IdErpForm
						END
					END
				END
			--END
			
			SELECT @TenantId = TC.TenantId FROM Security.TenantContainerInt TC 
			WHERE TC.ContainerId = @ContainerId

			;WITH Permission AS
			(
			SELECT 
			PU.[Action], PU.ActionValue 
			FROM Security.PermissionUserInt PU 
			WHERE PU.IdUser = @UserId AND PU.TenantId = @TenantId AND PU.IdForm = @IdErpForm AND PU.[Action] IN ('2','40','130','3','1','131','7','8','23','132','133','134','41','11','146','148','149','153','154')
			)
			INSERT INTO @Tabla
			SELECT
				codusuari = @UserCode,
				indidmenu = @IdForm,
				ioptguard =      MAX(CASE WHEN P.[Action] = '2' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END),
				ioptconsu =      MAX(CASE WHEN P.[Action] = '40' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END),
				ioptdisen =      MAX(CASE WHEN P.[Action] = '130' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END),
				ioptactua =      MAX(CASE WHEN P.[Action] = '3' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END),
				ioptelimi =      MAX(CASE WHEN P.[Action] = '1' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END),
				ioptnaveg =      MAX(CASE WHEN P.[Action] = '131' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END),
				ioptconfi =      MAX(CASE WHEN P.[Action] = '7' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END),
				ioptanula =      MAX(CASE WHEN P.[Action] = '8' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END),
				ioptimpri =      MAX(CASE WHEN P.[Action] = '23' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END),
				igricrear =      MAX(CASE WHEN P.[Action] = '132' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END),
				igrimodif =      MAX(CASE WHEN P.[Action] = '133' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END),
				igrielimi =      MAX(CASE WHEN P.[Action] = '134' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END),
				ioptvisib =      MAX(CASE WHEN P.[Action] = '41' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END),
				customizable =   MAX(CASE WHEN P.[Action] = '11' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END),
				PreAltaHospitalaria =   MAX(CASE WHEN P.[Action] = '146' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END),
				SoporteVieliberarCamaDobleEstancia =  MAX(CASE WHEN P.[Action] = '148' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END),
				SoporteVieEliminarRegistroEgreso =  MAX(CASE WHEN P.[Action] = '149' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END),
				SustituirAnexosPaciente =  MAX(CASE WHEN P.[Action] = '153' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END),
				AnulacionAnexosPaciente =  MAX(CASE WHEN P.[Action] = '154' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END)
			FROM Permission AS P;

			SELECT 
				codusuari,
				indidmenu,
				ioptguard = CAST(isnull(ioptguard, 0) AS BIT),
				ioptconsu = CAST(isnull(ioptconsu, 0) AS BIT),
				ioptdisen = CAST(isnull(ioptdisen, 0) AS BIT),
				ioptactua = CAST(isnull(ioptactua, 0) AS BIT),
				ioptelimi = CAST(isnull(ioptelimi, 0) AS BIT),
				ioptnaveg = CAST(isnull(ioptnaveg, 0) AS BIT),
				ioptconfi = CAST(isnull(ioptconfi, 0) AS BIT),
				ioptanula = CAST(isnull(ioptanula, 0) AS BIT),
				ioptimpri = CAST(isnull(ioptimpri, 0) AS BIT),
				igricrear = CAST(isnull(igricrear, 0) AS BIT),
				igrimodif = CAST(isnull(igrimodif, 0) AS BIT),
				igrielimi = CAST(isnull(igrielimi, 0) AS BIT),
				ioptvisib = CAST(isnull(ioptvisib, 0) AS BIT),
				customizable = CAST(isnull(customizable, 0) AS BIT),
				PreAltaHospitalaria = CAST(isnull(PreAltaHospitalaria, 0) AS BIT),
				SoporteVieliberarCamaDobleEstancia = CAST(isnull(SoporteVieliberarCamaDobleEstancia, 0) AS BIT),
				SoporteVieEliminarRegistroEgreso = CAST(isnull(SoporteVieEliminarRegistroEgreso, 0) AS BIT),
				SustituirAnexosPaciente = CAST(isnull(SustituirAnexosPaciente, 0) AS BIT),
				AnulacionAnexosPaciente = CAST(isnull(AnulacionAnexosPaciente, 0) AS BIT)
			FROM @Tabla
		END
	END
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta y devuelve los permisos de acceso que tiene un usuario específico sobre un formulario o pantalla del sistema. Recibe el código de usuario y el identificador del formulario, determina el tipo de arquitectura del contenedor (HIS o transaccional), resuelve la equivalencia entre formularios del HIS y del ERP mediante la tabla de relaciones de formularios, y luego extrae desde la tabla de permisos de usuario las acciones habilitadas: guardar, consultar, actualizar, eliminar, imprimir, navegar, anular, diseñar, configurar, operar en grillas, visibilidad, personalización, opciones especiales de hospitalización y los permisos de sustitución y anulación de anexos de documentos del paciente. Es usado por las pantallas del sistema para decidir en tiempo real qué botones y funciones mostrar u ocultar según el perfil del usuario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_UserPermissionsForm';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_UserPermissionsForm';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve los permisos efectivos de un usuario sobre un formulario, mapeando códigos de acción a banderas booleanas, con resolución de equivalencias entre formularios HIS y ERP y cortocircuitos para administradores globales.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_UserPermissionsForm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La base de datos actual debe estar registrada como HISContainer o TransactionalContainer en Security.ContainersInt para resolver el ContainerId.; El usuario debe existir en Security.[User] cuando se requiere evaluar permisos (rama con formulario informado).; Debe existir relación tenant-contenedor en Security.TenantContainerInt para obtener el TenantId aplicable.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_UserPermissionsForm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las banderas de permiso devueltas son siempre BIT con valor 0 cuando no existe registro en PermissionUserInt (ISNULL(...,0)).; Los administradores globales (UserType=''3'') nunca consultan PermissionUserInt; reciben todas las banderas en 0 e indidmenu=''0''.; Solo se consideran las acciones de permiso con códigos 2, 40, 130, 3, 1, 131, 7, 8, 23, 132, 133, 134, 41, 11, 146, 148, 149, 153, 154.; El ContainerId se resuelve siempre contra la base de datos actual (DB_NAME()), priorizando HISContainer sobre TransactionalContainer.; Los permisos se filtran siempre por usuario, tenant y formulario ERP resuelto, garantizando aislamiento multi-tenant.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_UserPermissionsForm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Permisos de usuario; Formularios HIS y ERP; Administrador global; Multi-tenant (TenantId); Contenedor HIS/Transaccional; Pre Alta Hospitalaria; Liberar cama de doble estancia; Eliminar registro de egreso; Sustitución de anexos del paciente; Anulación de anexos del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_UserPermissionsForm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Cuando el formulario solicitado es nulo/vacío, se retorna un registro con todas las banderas de permiso en 0 (sin consultar permisos).; [RETURN_RESULT] RESULTSET: Cuando el usuario tiene UserType = ''3'' (Administrador global), se retorna un registro con indidmenu=''0'' y todas las banderas en 0, omitiendo la consulta de permisos.; [RETURN_RESULT] RESULTSET: Cuando el formulario es no nulo y el usuario no es admin global, se retorna una fila con las banderas de permiso agregadas (MAX por acción) desde Security.PermissionUserInt filtrando por usuario, tenant y formulario ERP resuelto.; [INSERT] @Tabla: Para usuarios no admin se inserta una fila por usuario/formulario consolidando ActionValue por cada Action permitido (2,40,130,3,1,131,7,8,23,132,133,134,41,11,146,148,149) usando MAX(CASE WHEN Action=...), incluyendo ahora las acciones 153 (sustitución de anexos) y 154 (anulación de anexos).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_UserPermissionsForm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ContainerId no encontrado por HISContainer = DB_NAME() → Reintenta resolver ContainerId buscando por TransactionalContainer = DB_NAME().; si @IdErpForm IS NULL (formulario no informado) → Devuelve resultado vacío de permisos (todas las banderas en 0) sin consultar tablas de permisos. else Continúa a evaluar tipo de usuario y resolver formulario ERP.; si UserType = ''3'' (Administrador global) → Devuelve registro con indidmenu=''0'' y banderas en 0. else Procede a mapear el formulario HIS a su equivalente ERP y consultar permisos.; si @IdErpForm = ''001'' → Sustituye el identificador de formulario por ''1629''.; si @IdErpForm = ''079'' → Sustituye el identificador de formulario por ''2700''.; si @IdErpForm está en la lista predefinida de códigos HIS o comienza con ''0'' → Resuelve el IdErpForm consultando Security.FormRelationshipInt por IdHisForm.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_UserPermissionsForm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Security.ContainersInt; Security.User; Security.FormRelationshipInt; Security.TenantContainerInt; Security.PermissionUserInt', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_UserPermissionsForm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_UserPermissionsForm';
-- GO
