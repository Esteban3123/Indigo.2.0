
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