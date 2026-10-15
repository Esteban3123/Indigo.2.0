
-- =============================================
-- Author:		Carlos Jhefersson Muñoz Ramirez
-- Create date: 20/03/2017
-- Description:	Procedimiento para eliminar el Tercero
-- =============================================
CREATE PROCEDURE [Common].[SP_DeleteThirdParty]

	@NitThirdParty as varchar(20) 
	
AS
BEGIN
	--declare @NitThirdParty as varchar(20) 
	--set @NitThirdParty = '899999327'
	declare @PersonId as Integer = (select PersonId from Common.ThirdParty where Nit = @NitThirdParty)
	
	Begin try
		delete Common.Phone where IdPerson = @PersonId
		delete Common.[Address] where IdPerson = @PersonId
		delete Common.Email where IdPerson = @PersonId
		delete Common.ThirdParty where nit = @NitThirdParty 
		delete Common.Person where id = @PersonId

		--commit transaction
		select 0 as CodeMessage, 'Se Elimino correctamente' as Message
	End try
	Begin Catch

		--rollback transaction
		select 999 as CodeMessage, ERROR_MESSAGE() as Message
		--select 999 as CodeMessage, ERROR_NUMBER() as Message

	End Catch
END