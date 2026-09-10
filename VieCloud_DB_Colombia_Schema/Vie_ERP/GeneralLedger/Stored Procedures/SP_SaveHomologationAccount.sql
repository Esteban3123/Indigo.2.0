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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la homologación de cuentas contables en el libro mayor general, recibiendo como parámetro un XML con una lista de relaciones entre cuentas principales oficiales y cuentas principales del sistema. Si el registro tiene Id igual a cero lo inserta como nuevo en la tabla HomologationAccount; si tiene Id mayor a cero, actualiza la relación de cuentas existente. Se utiliza para mantener la equivalencia entre el plan de cuentas oficial (externo o normativo) y el plan de cuentas interno de la organización dentro del módulo de contabilidad.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_SaveHomologationAccount';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_SaveHomologationAccount';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Persiste un lote de homologaciones entre el plan de cuentas oficial y el plan interno, insertando las nuevas y actualizando las existentes a partir de un XML de entrada.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_SaveHomologationAccount';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe seguir la estructura /HomologationAccounts/HomologationAccount con los nodos Id, OfficialMainAccountId, MainAccountId, CreationUser y CreationDate.; Id = 0 indica un registro nuevo a insertar; Id > 0 indica un registro existente a actualizar.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_SaveHomologationAccount';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las actualizaciones nunca modifican CreationUser ni CreationDate, sólo OfficialMainAccountId y MainAccountId.; Los registros nuevos siempre se identifican con Id = 0 en el XML.; La operación procesa de forma diferenciada altas y modificaciones en una misma ejecución.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_SaveHomologationAccount';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Homologación de cuentas contables; Plan de cuentas oficial; Plan de cuentas interno (MainAccount); Contabilidad / General Ledger', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_SaveHomologationAccount';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] GeneralLedger.HomologationAccount: Cuando el ítem del XML tiene Id = 0 se inserta una nueva homologación con OfficialMainAccountId, MainAccountId, CreationUser y CreationDate.; [UPDATE] GeneralLedger.HomologationAccount: Cuando el ítem del XML tiene Id > 0 se actualizan OfficialMainAccountId y MainAccountId del registro cuyo Id coincide.; [RETURN_RESULT] (resultset): Si la operación termina sin excepción retorna CodeMessage=0 con mensaje de éxito; si ocurre una excepción retorna CodeMessage=999 con ERROR_MESSAGE().', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_SaveHomologationAccount';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existen ítems con Id = 0 en el lote → Inserta esos ítems como nuevas homologaciones de cuenta else No se ejecuta el INSERT; si Existen ítems con Id > 0 en el lote → Actualiza las homologaciones existentes haciendo join por Id else No se ejecuta el UPDATE; si Se produce un error en el bloque TRY → Retorna CodeMessage=999 con el mensaje de error capturado else Retorna CodeMessage=0 con mensaje de éxito', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_SaveHomologationAccount';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.HomologationAccount', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_SaveHomologationAccount';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_SaveHomologationAccount';
-- GO
