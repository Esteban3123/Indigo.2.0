-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 01/12/2015
-- Description:	Procedimiento que se encarga de guardar la homologación de cuentas
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_SaveHomologationAccount] 
	@HomologationAccountXml as Xml
AS
BEGIN
	
	--Tabla para almacenar los items del listado que viene en el xml y poder guardar las homologaciones de cuenta
	declare @TableHomologationAccount table(Id int, OfficialMainAccountId int, MainAccountId int, CreationUser varchar(20), CreationDate datetime)

	--begin transaction
	begin try

		insert into @TableHomologationAccount
		select 
		t.x.value('Id[1]','int') as Id,
		t.x.value('OfficialMainAccountId[1]','int') as OfficialMainAccountId,
		t.x.value('MainAccountId[1]','int') as MainAccountId,
		t.x.value('CreationUser[1]','varchar(20)') as CreationUser,
		t.x.value('CreationDate[1]','datetime') as CreationDate
		from @HomologationAccountXml.nodes('/HomologationAccounts/HomologationAccount') t(x)

		--Inserto los nuevos objetos
		if (select count(*) from @TableHomologationAccount where Id = 0) > 0 begin
			INSERT INTO GeneralLedger.HomologationAccount(OfficialMainAccountId,MainAccountId,CreationUser,CreationDate)
			select OfficialMainAccountId,MainAccountId, CreationUser, CreationDate from @TableHomologationAccount where Id = 0
		end

		--Actualizo los registros si hay
		if (select count(*) from @TableHomologationAccount where Id > 0) > 0 begin
			UPDATE ha set ha.OfficialMainAccountId = td.OfficialMainAccountId, ha.MainAccountId = td.MainAccountId
			from GeneralLedger.HomologationAccount ha 
			inner join @TableHomologationAccount td on ha.Id = td.Id
		end

		--commit transaction
		select 0 as CodeMessage, 'Se guardó correctamente las homologaciones de cuentas.' as Message

	end try
	begin catch

		--rollback transaction
		select 999 as CodeMessage, ERROR_MESSAGE() as Message

	end catch

END