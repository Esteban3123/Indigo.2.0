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