CREATE PROCEDURE [Security].[SP_FillSecurityTables]
AS
BEGIN
	SET NOCOUNT ON; -- Se recomienda utilizar SET NOCOUNT ON para evitar el envío de recuentos de filas como mensajes desde las operaciones DML.
	
	-- Eliminar tablas existentes si existen
	IF OBJECT_ID(N'[Security].FormRelationshipInt', N'U') IS NOT NULL  
	   DROP TABLE [Security].FormRelationshipInt;  
	
	-- Insertar datos en [Security].FormRelationshipInt desde Security.FormRelationship
	SELECT * INTO [Security].FormRelationshipInt FROM Security.FormRelationship;
	
	IF OBJECT_ID(N'[Security].RollInt', N'U') IS NOT NULL 
	   DROP TABLE [Security].RollInt;  
	
	SELECT * INTO [Security].RollInt FROM Security.Roll;
	
	IF OBJECT_ID(N'[Security].PermissionRollInt', N'U') IS NOT NULL  
	   DROP TABLE [Security].PermissionRollInt;  
	
	SELECT * INTO [Security].PermissionRollInt FROM Security.PermissionRoll;
	
	IF OBJECT_ID(N'[Security].TenantContainerInt', N'U') IS NOT NULL  
	   DROP TABLE [Security].TenantContainerInt;  
	
	SELECT * INTO [Security].TenantContainerInt FROM Security.TenantContainer;
	
	IF OBJECT_ID(N'[Security].PermissionUserInt', N'U') IS NOT NULL  
	   DROP TABLE [Security].PermissionUserInt;  
	
	SELECT * INTO [Security].PermissionUserInt FROM Security.PermissionUser;

	IF OBJECT_ID(N'[Security].UserInt', N'U') IS NOT NULL  
	   DROP TABLE [Security].UserInt;  
	
	SELECT * INTO [Security].UserInt FROM [Security].[User];

	IF OBJECT_ID(N'[Security].PersonInt', N'U') IS NOT NULL  
	   DROP TABLE [Security].PersonInt;  
	
	SELECT * INTO [Security].PersonInt FROM [Security].[Person];

	IF OBJECT_ID(N'[Security].PermissionCompanyInt', N'U') IS NOT NULL   
	   DROP TABLE [Security].PermissionCompanyInt;  
	
	SELECT * INTO [Security].PermissionCompanyInt FROM [Security].PermissionCompany;

	IF OBJECT_ID(N'[Security].TenantUsersInt', N'U') IS NOT NULL  
	   DROP TABLE [Security].TenantUsersInt;  
	
	SELECT * INTO [Security].TenantUsersInt FROM [Security].TenantUsers;
	
	IF OBJECT_ID(N'[Security].ContainersInt', N'U') IS NOT NULL 
	   DROP TABLE [Security].ContainersInt;  
	
	SELECT * INTO [Security].ContainersInt FROM [Security].Containers;
		
	-- Crear los índices después de haber insertado los datos
	CREATE NONCLUSTERED INDEX [IX_PermissionRollInt_Action_ActionValue_IdRoll_IdForm] ON [Security].[PermissionRollInt] ([Action], [ActionValue], [IdRoll], [IdForm]);
	CREATE NONCLUSTERED INDEX [IX_PermissionRollInt_ActionValue_IdForm_IdRoll_Action_TimeStamp] ON [Security].[PermissionRollInt] ([ActionValue], [IdForm], [IdRoll], [Action], [TimeStamp]);
	CREATE NONCLUSTERED INDEX [IX_PermissionRollInt_ActionValue_IdRoll_Action] ON [Security].[PermissionRollInt] ([ActionValue], [IdRoll], [Action], [IdForm]);
	CREATE NONCLUSTERED INDEX [ix_PermissionRollInt_idform_Action] ON [Security].[PermissionRollInt] ([IdForm], [Action], [ActionValue], [IdRoll]);
	CREATE NONCLUSTERED INDEX [IX_PermissionRollInt_IdForm_Action_IdRoll_ActionValue] ON [Security].[PermissionRollInt] ([IdForm], [Action], [IdRoll], [ActionValue]);
	CREATE NONCLUSTERED INDEX [IX_PermissionRollInt_IdRoll_IdForm_Action_ActionValue] ON [Security].[PermissionRollInt] ([IdRoll], [IdForm], [Action], [ActionValue]);
	CREATE NONCLUSTERED INDEX [IX_PermissionUserInt_Action_ActionValue_IdUser_TenantId] ON [Security].[PermissionUserInt] ([Action], [ActionValue], [IdUser], [TenantId], [IdForm]);
	CREATE UNIQUE NONCLUSTERED INDEX [IX_PermissionUserInt] ON [Security].[PermissionUserInt] ([IdUser], [IdForm], [Action]);
END
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Recrea tablas intermedias (sufijo `Int`) en el esquema `Security` eliminando y repoblando cada una como copia exacta de sus tablas fuente de seguridad (roles, permisos, usuarios, personas, contenedores, tenants y formularios). Tras la carga masiva, crea índices no agrupados optimizados sobre `PermissionRollInt` y `PermissionUserInt` para acelerar consultas de autorización por rol, formulario, acción y usuario. Funciona como proceso de materialización/caché de las tablas de seguridad, desacoplando las consultas de permisos de las tablas transaccionales originales.', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_FillSecurityTables';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_FillSecurityTables';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reconstruye un conjunto de tablas espejo (sufijo Int) del esquema de seguridad copiando los datos actuales y recreando índices de soporte para consultas de permisos.', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_FillSecurityTables';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las tablas fuente del esquema Security (FormRelationship, Roll, PermissionRoll, TenantContainer, PermissionUser, User, Person, PermissionCompany, TenantUsers, Containers) deben existir y ser accesibles.; El ejecutor debe tener permisos DDL (DROP/CREATE) y DML (SELECT INTO) sobre el esquema Security.', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_FillSecurityTables';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Tras la ejecución, cada tabla espejo (*Int) contiene una copia exacta de su tabla fuente al momento de la corrida.; La tabla PermissionUserInt siempre queda con un índice único sobre la combinación (IdUser, IdForm, Action), garantizando unicidad de permiso por usuario/formulario/acción.; Los índices de soporte sobre PermissionRollInt y PermissionUserInt se recrean siempre tras repoblar las tablas.; El proceso es destructivo: cualquier dato previo en las tablas *Int se pierde al recrearlas.', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_FillSecurityTables';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Usuarios; Personas; Roles; Permisos por rol; Permisos por usuario; Permisos por compañía; Formularios; Tenants (multi-tenant); Contenedores; Acciones de seguridad', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_FillSecurityTables';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[DELETE] Security.FormRelationshipInt: Si la tabla existe (OBJECT_ID IS NOT NULL) se elimina con DROP TABLE antes de recrearla.; [INSERT] Security.FormRelationshipInt: Se recrea mediante SELECT * INTO copiando todo el contenido de Security.FormRelationship.; [DELETE] Security.RollInt: Si existe se elimina con DROP TABLE.; [INSERT] Security.RollInt: Se recrea con SELECT * INTO desde Security.Roll.; [DELETE] Security.PermissionRollInt: Si existe se elimina con DROP TABLE.; [INSERT] Security.PermissionRollInt: Se recrea con SELECT * INTO desde Security.PermissionRoll y luego se le crean 6 índices no clusterizados sobre combinaciones de Action, ActionValue, IdRoll e IdForm.; [DELETE] Security.TenantContainerInt: Si existe se elimina con DROP TABLE.; [INSERT] Security.TenantContainerInt: Se recrea con SELECT * INTO desde Security.TenantContainer.; [DELETE] Security.PermissionUserInt: Si existe se elimina con DROP TABLE.; [INSERT] Security.PermissionUserInt: Se recrea con SELECT * INTO desde Security.PermissionUser y se crean un índice no clusterizado por Action/ActionValue/IdUser/TenantId/IdForm y un índice único por IdUser+IdForm+Action.; [DELETE] Security.UserInt: Si existe se elimina con DROP TABLE.; [INSERT] Security.UserInt: Se recrea con SELECT * INTO desde Security.User.; [DELETE] Security.PersonInt: Si existe se elimina con DROP TABLE.; [INSERT] Security.PersonInt: Se recrea con SELECT * INTO desde Security.Person.; [DELETE] Security.PermissionCompanyInt: Si existe se elimina con DROP TABLE.; [INSERT] Security.PermissionCompanyInt: Se recrea con SELECT * INTO desde Security.PermissionCompany.; [DELETE] Security.TenantUsersInt: Si existe se elimina con DROP TABLE.; [INSERT] Security.TenantUsersInt: Se recrea con SELECT * INTO desde Security.TenantUsers.; [DELETE] Security.ContainersInt: Si existe se elimina con DROP TABLE.; [INSERT] Security.ContainersInt: Se recrea con SELECT * INTO desde Security.Containers.', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_FillSecurityTables';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si OBJECT_ID de cada tabla espejo (sufijo Int) IS NOT NULL → Se ejecuta DROP TABLE sobre la tabla espejo antes de recrearla. else Se omite el DROP y se procede directamente al SELECT * INTO.', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_FillSecurityTables';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Security.FormRelationship; Security.Roll; Security.PermissionRoll; Security.TenantContainer; Security.PermissionUser; Security.User; Security.Person; Security.PermissionCompany; Security.TenantUsers; Security.Containers', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_FillSecurityTables';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_FillSecurityTables';
-- GO
