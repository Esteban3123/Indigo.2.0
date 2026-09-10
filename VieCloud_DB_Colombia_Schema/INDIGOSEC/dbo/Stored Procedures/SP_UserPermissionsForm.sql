-- =============================================
-- Author:  Hector Rodriguez Rubiano
-- ALTER date: 2021-05-14  
-- Description: Consulta los permisos de usuario en un formulario  
-- =============================================
CREATE PROCEDURE DBO.SP_UserPermissionsForm
(
    -- Add the parameters for the stored procedure here
    @UserCode VARCHAR(20),  
	@IdForm CHAR(3) --es nulo o vacio si se llama desde el formulario de usuario en el hist

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
	DECLARE @IdErpForm VARCHAR(4)
	DECLARE @DBName NVARCHAR(128)
	SELECT @DBName = (SELECT DB_NAME() AS [Current Database])

	SELECT @ArchitectureType = ISNULL(ArchitectureType, 1), @ContainerId = C.Id FROM Security.Containers C WHERE C.HISContainer = @DBName
	if @ContainerId is NULL
	BEGIN
		SELECT @ArchitectureType = ISNULL(ArchitectureType, 1), @ContainerId = C.Id FROM Security.Containers C WHERE C.TransactionalContainer = @DBName
	END

	SELECT @IdErpForm = IdErpForm from [Security].[FormRelationship] where IdHisForm = @IdForm
	IF @IdErpForm IS NULL
	BEGIN
		SELECT @UserCode AS codusuari, @IdForm AS indidmenu, CAST(0 AS BIT) ioptguard, CAST(0 AS BIT) ioptconsu, CAST(0 AS BIT) ioptdisen, CAST(0 AS BIT) ioptactua
		, CAST(0 AS BIT) ioptelimi, CAST(0 AS BIT) ioptnaveg, CAST(0 AS BIT) ioptconfi, CAST(0 AS BIT) ioptanula, CAST(0 AS BIT) ioptimpri, CAST(0 AS BIT) igricrear
		, CAST(0 AS BIT) igrimodif, CAST(0 AS BIT) igrielimi, CAST(0 AS BIT) ioptvisib
	END
	ELSE
	BEGIN
		SELECT @UserId = Id,  @UserType = UserType  FROM Security.[User] WHERE UserCode = @UserCode

		IF @UserType = '3'
		BEGIN
			SELECT @UserCode AS codusuari, @IdForm AS indidmenu, 
			F.ioptguard, F.ioptconsu, F.ioptdisen, F.ioptactua
			, F.ioptelimi, F.ioptnaveg, F.ioptconfi, F.ioptanula, F.ioptimpri, F.igricrear
			, F.igrimodif, F.igrielimi, CAST(1 AS BIT) ioptvisib
			FROM dbo.SEGPERMIF F WHERE F.indidmenu = @IdForm
		END
		ELSE
		BEGIN
			SELECT @TenantId = TC.TenantId FROM Security.TenantContainer TC
			WHERE TC.ContainerId = @ContainerId

			;WITH Permission AS
			(
			SELECT @IdForm AS IdHisForm, PU.[Action], PU.ActionValue 
			FROM Security.PermissionUser PU 
			WHERE PU.IdUser = @UserId AND PU.TenantId = @TenantId AND PU.IdForm = @IdErpForm AND PU.[Action] IN ('2','40','127','3','1','128','7','8','23','129','130','131','41')
			)
			SELECT
				codusuari = @UserCode,
				indidmenu = P.IdHisForm,
				ioptguard =      MAX(CASE WHEN P.[Action] = '2' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END),
				ioptconsu =      MAX(CASE WHEN P.[Action] = '40' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END),
				ioptdisen =      MAX(CASE WHEN P.[Action] = '127' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END),
				ioptactua =      MAX(CASE WHEN P.[Action] = '3' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END),
				ioptelimi =      MAX(CASE WHEN P.[Action] = '1' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END),
				ioptnaveg =      MAX(CASE WHEN P.[Action] = '128' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END),
				ioptconfi =      MAX(CASE WHEN P.[Action] = '7' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END),
				ioptanula =      MAX(CASE WHEN P.[Action] = '8' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END),
				ioptimpri =      MAX(CASE WHEN P.[Action] = '23' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END),
				igricrear =      MAX(CASE WHEN P.[Action] = '129' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END),
				igrimodif =      MAX(CASE WHEN P.[Action] = '130' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END),
				igrielimi =      MAX(CASE WHEN P.[Action] = '131' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END),
				ioptvisib =      MAX(CASE WHEN P.[Action] = '41' THEN CAST(P.ActionValue AS TINYINT) ELSE CAST(0 AS TINYINT) END)
			FROM Permission AS P
			GROUP BY
				P.IdHisForm;
		END
	END
END
