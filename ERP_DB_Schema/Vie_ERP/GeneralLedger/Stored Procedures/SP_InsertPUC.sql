-- =============================================
-- Author:		Sergio Fernandez
-- Create date: 08/05/2014
-- Description:	sp para insertar las cuentas contables
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_InsertPUC]
	@IdAccountLevel int, 
    @IdAccountClass int,
	@Number varchar(50),
	@Name varchar(100),
	@IdParent int,
	@HandlesThirdParty bit,
	@CloseThirdParty bit,
	@IdThirdParty int,
	@ReconcileAccount bit,
	@Availability tinyint,	
	@HandlesCostCenter bit,
	@RetencionType tinyint,
	@AllowsMovement bit,
	@Status bit	
AS
BEGIN
SET NOCOUNT ON;
	INSERT INTO GeneralLedger.MainAccounts 
	 (IdAccountLevel, IdAccountClass, Number,Name,IdParent,HandlesThirdParty,CloseThirdParty,IdThirdParty,ReconcileAccount,[Availability]
	 ,HandlesCostCenter,RetencionType,[Status],AllowsMovement, CreationUser,CreationDate)
	 VALUES (@IdAccountLevel,@IdAccountClass,@Number,@Name,@IdParent,@HandlesThirdParty,@CloseThirdParty,@IdThirdParty,@ReconcileAccount,@Availability
	 ,@HandlesCostCenter,@RetencionType,@Status,@AllowsMovement, '', [Common].[GETDATE]());

	 select SCOPE_IDENTITY() as id;

END