CREATE PROCEDURE [dbo].[SP_RolePermissionsForm]
(
    -- Add the parameters for the stored procedure here
    @RoleCode varchar(10),  
	@IdForm VARCHAR(5) --es nulo o vacio si se llama desde el formulario de rol en el hist

)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Insert statements for procedure here
	DECLARE @IdErpForm VARCHAR(5)
	DECLARE @Tabla as TABLE
				(
					codigorol varchar(10),
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

	--SELECT @IdErpForm = IdErpForm from [Security].[FormRelationship] where IdHisForm = @IdForm
	SELECT @IdErpForm = @IdForm
	IF @IdErpForm IS NULL
	BEGIN
		SELECT @RoleCode AS codigorol, @IdForm AS indidmenu
				,CAST(0 AS BIT) AS ioptguard
				,CAST(0 AS BIT) AS ioptconsu
				,CAST(0 AS BIT) AS ioptdisen
				,CAST(0 AS BIT) AS ioptactua
				,CAST(0 AS BIT) AS ioptelimi
				,CAST(0 AS BIT) AS ioptnaveg
				,CAST(0 AS BIT) AS ioptconfi
				,CAST(0 AS BIT) AS ioptanula
				,CAST(0 AS BIT) AS ioptimpri
				,CAST(0 AS BIT) AS igricrear
				,CAST(0 AS BIT) AS igrimodif
				,CAST(0 AS BIT) AS igrielimi
				,CAST(0 AS BIT) AS ioptvisib
				,CAST(0 AS BIT) AS customizable
				,CAST(0 AS BIT) AS PreAltaHospitalaria
				,CAST(0 AS BIT) AS SoporteVieliberarCamaDobleEstancia    
				,CAST(0 AS BIT) AS SoporteVieEliminarRegistroEgreso   
				,CAST(0 AS BIT) AS SustituirAnexosPaciente   
				,CAST(0 AS BIT) AS AnulacionAnexosPaciente   
	END
	ELSE
	BEGIN
			--IF @IdErpForm = '828'
			--BEGIN
			--	select @IdErpForm = '2708' --
			--	set @IdForm = @IdErpForm
			--END
			--ELSE
			--BEGIN
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
						if  @IdErpForm IN ('904','965','475','808','2291','2629','1665','828','005','100','101', '109', '113', '114', '123','127', '138', '143', '167', '186', '193', '196', '197', '205','211', '215', '216', '218', '219', '221', '225', '258', '259', '309', '311', '314', '326', '327','401', '402', '403', '404','412', '413', '415', '418'
						, '419', '420', '425', '426', '427', '428', '429', '430', '431', '436', '437', '438', '439', '440', '441', '442', '443', '444', '446', '470','810', '815', '855', '888', '901', '953', '954', '959', '960', '961', '967', '971', '976', '977'
						, '978', '979', '980', '982', '983', '984', '985', '986', '987', '988', '989','990','991', '992', '993', '994', '995', '996', '997', '998','999','840','320'
						,'206','406','407','408','409','410','463','464','465','466','468','330','331','476', '323', '322', '324', '325', '421', '432', '433', '434', '435', '445', '455', '908', '909', '910', '818'
						) OR (SUBSTRING(@IdErpForm,1,1) = '0')
						BEGIN
							select @IdErpForm = IdErpForm from Security.FormRelationshipInt where IdHisForm = @IdErpForm
							print @IdErpForm
							set @IdForm = @IdErpForm
						END
					END
				END
		--	END
			
				;WITH Permission AS
				(
				SELECT
				PR.[Action], PR.ActionValue 
				FROM Security.RollInt R
				INNER JOIN Security.PermissionRollInt PR ON PR.IdRoll = R.Id AND PR.IdForm = @IdErpForm AND PR.[Action] IN ('2','40','130','3','1','131','7','8','23','132','133','134','41','11','146','148','149','153','154')
				WHERE R.RollCode = @RoleCode 
				)
				INSERT INTO @Tabla
				SELECT
					codigorol = @RoleCode,
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
					PreAltaHospitalaria =  MAX(CASE WHEN P.[Action] = '146' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END),
					SoporteVieliberarCamaDobleEstancia =  MAX(CASE WHEN P.[Action] = '148' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END),
					SoporteVieEliminarRegistroEgreso =  MAX(CASE WHEN P.[Action] = '149' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END),
					SustituirAnexosPaciente =  MAX(CASE WHEN P.[Action] = '153' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END),
					AnulacionAnexosPaciente =  MAX(CASE WHEN P.[Action] = '154' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END)
				FROM Permission AS P;

				SELECT 
					codigorol,
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta los permisos asignados a un rol de seguridad sobre un formulario específico del sistema. Recibe el código del rol y el identificador del formulario, resuelve equivalencias entre formularios del módulo de historia clínica y del ERP (usando la tabla de relación FormRelationship), y devuelve una fila con los indicadores de cada acción permitida: guardar, consultar, actualizar, eliminar, navegar, configurar, anular, imprimir, visibilidad, personalización (customizable), opciones de grilla (crear, modificar, eliminar) y permisos especiales como pre-alta hospitalaria, liberar cama de doble estancia y eliminar registro de egreso. También incluye los permisos para sustituir y anular/eliminar anexos de documentos del paciente. Se usa para controlar el acceso y las acciones disponibles para cada rol en cada pantalla o formulario del sistema, tanto en el módulo clínico (HIS) como en el ERP.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_RolePermissionsForm';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_RolePermissionsForm';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve los permisos (acciones) que tiene un rol sobre un formulario, mapeando formularios HIS a sus equivalentes ERP y pivoteando las acciones permitidas en columnas booleanas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RolePermissionsForm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El rol debe existir en Security.RollInt con el código indicado.; Para obtener permisos reales debe proporcionarse un identificador de formulario; si es nulo se devuelven permisos en cero.; Los formularios HIS listados o que comienzan con ''0'' deben tener su equivalente registrado en Security.FormRelationshipInt.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RolePermissionsForm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las banderas de permiso siempre se devuelven como BIT, nunca NULL (se aplica ISNULL → 0).; Cada acción de seguridad se mapea a una columna fija predefinida (guardar, consultar, diseñar, actualizar, eliminar, navegar, configurar, anular, imprimir, crear/modificar/eliminar grilla, visibilidad, customizable, preAltaHospitalaria, liberar cama doble estancia, eliminar registro egreso, sustituir anexos paciente, anulación anexos paciente).; Solo se consideran las 19 acciones específicas listadas; otras acciones del rol se ignoran.; El procedimiento siempre retorna exactamente un conjunto de resultados con una fila por invocación.; El código de rol y el id de formulario en la salida reflejan los valores de entrada (con el id ya transformado al ERP cuando aplica).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RolePermissionsForm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Roles de seguridad; Permisos por formulario; Mapeo de formularios HIS a ERP; Pre alta hospitalaria; Liberación de cama por doble estancia; Eliminación de registro de egreso; Sustitución de anexos del paciente; Anulación de anexos del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RolePermissionsForm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULT: Cuando el identificador de formulario es NULL, retorna una fila con el rol y todas las banderas de permiso en 0.; [RETURN_RESULT] RESULT: Cuando hay formulario, retorna una fila pivoteando las acciones (2,40,130,3,1,131,7,8,23,132,133,134,41,11,146,148,149,153,154) como columnas BIT con el ActionValue máximo encontrado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RolePermissionsForm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Identificador de formulario es NULL → Devuelve fila con todos los permisos en 0 else Aplica mapeo HIS→ERP y consulta permisos del rol; si Identificador de formulario = ''001'' → Sustituye el id por ''1629'' antes de consultar permisos; si Identificador de formulario = ''079'' → Sustituye el id por ''2700'' antes de consultar permisos; si Identificador de formulario está en la lista codificada de IDs HIS o empieza con ''0'' → Resuelve el id ERP equivalente vía Security.FormRelationshipInt (IdHisForm → IdErpForm) else Usa el id de formulario tal cual fue recibido', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RolePermissionsForm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Security.FormRelationshipInt; Security.RollInt; Security.PermissionRollInt', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RolePermissionsForm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RolePermissionsForm';
-- GO
