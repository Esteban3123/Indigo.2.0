

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