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