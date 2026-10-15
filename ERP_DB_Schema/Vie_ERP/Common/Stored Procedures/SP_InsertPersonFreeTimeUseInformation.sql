-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Common].[SP_InsertPersonFreeTimeUseInformation] 
	-- Add the parameters for the stored procedure here
	@IdentificationNumber Varchar(20),
	@FreeTimeUseId INT
	
	

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	
	declare @Count INT
	declare @Result INT 
	SET @Count = (SELECT COUNT(*)
	FROM [Common].[PersonFreeTimeUse]
	WHERE ([PersonFreeTimeUse].[PersonId] = (Select [Person].[Id] from [Common].[Person] WHERE [Common].[Person].[IdentificationNumber] = @IdentificationNumber) AND [PersonFreeTimeUse].FreeTimeUseId = @FreeTimeUseId))
	
	IF @Count = 0
	BEGIN
		INSERT INTO [Common].[PersonFreeTimeUse] (PersonId, FreeTimeUseId) VALUES ((Select [Person].[Id] from [Common].[Person] WHERE [Common].[Person].[IdentificationNumber] = @IdentificationNumber) ,@FreeTimeUseId);
		/*
		SET @Result = 1
		RETURN(1)*/
		SELECT '001' AS CodeMessage, 'Se insertó correctamente' as Mensaje
	END
	ELSE
	BEGIN 
	/*
		SET @Result = 0	
		RETURN(0)*/
		SELECT '999' AS CodeMessage, 'No se insertó la Actividad porque ya existe' as Mensaje
	END

END