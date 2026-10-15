
-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 2/12/2016
-- Description:	Procedimiento que se encarga de actualizar el campo de import en la tabla LogisticsProductionCenterRecordDetail
-- =============================================
CREATE PROCEDURE [InteropCost].[SP_UpdateFieldImport] 
	@ObjectXml as Xml,
	@Status as int
AS
BEGIN
	
	--Tabla para almacenar los items del listado que viene en el xml y poder actualizar el campo en la tabla
	declare @TableIds table(Id int)

	--begin transaction
	begin try

		insert into @TableIds
		select 
		t.x.value('Id[1]','int') as Id
		from @ObjectXml.nodes('/Data') t(x)

		--Actualizo los registros si hay datos y sea diferente de anular
		if (select count(*) from @TableIds) > 0 and @Status <> 3 begin
			update lpcrd set lpcrd.Import = 1
			from InteropCost.LogisticsProductionCenterRecordDetail lpcrd 
			inner join @TableIds ti on ti.Id = lpcrd.Id
		end

		--Actualizo los registros si hay datos y sea igual a anular
		if (select count(*) from @TableIds) > 0 and @Status = 3 begin
			update lpcrd set lpcrd.Import = 0
			from InteropCost.LogisticsProductionCenterRecordDetail lpcrd 
			inner join @TableIds ti on ti.Id = lpcrd.Id
		end

		--commit transaction
		select 0 as CodeMessage, 'Se actualizó correctamente' as Message

	end try
	begin catch

		--rollback transaction
		select 999 as CodeMessage, ERROR_MESSAGE() as Message

	end catch

END