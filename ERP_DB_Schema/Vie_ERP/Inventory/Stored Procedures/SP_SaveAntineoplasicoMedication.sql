-- ==========================================================
-- Author:		Hector Rodriguez Rubiano
-- Create date: 04/08/2020
-- Description:	Store que se encarga de guardar medicamentos antineoplásico CAC
-- ==========================================================
CREATE PROCEDURE [Inventory].[SP_SaveAntineoplasicoMedication]
	@Xml xml,
	@UserCode varchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	--Tabla para asignar los valores desde el xml para poder guardar
	declare @AntineoplasicoMedication table(Id int, AtcId int, ClassificationId tinyint, StateAPM tinyint)

	insert into @AntineoplasicoMedication
	select 
	t.x.value('Id[1]','int')
	,t.x.value('AtcId[1]','int')
	,t.x.value('ClassificationId[1]','tinyint')
	,t.x.value('StateAPM[1]','tinyint')
	from @Xml.nodes('/Data/APM') t(x)
	
	begin try
        --Error
		--select 999 as CodeResult, 'La unidad de medida para peso del ATC no tiene una clase seleccionada' as MessageResult, '' as Code, 0 as Id
		--return 

		--Nuevos
		insert into Inventory.AntineoplasicoMedication(AtcId, ClassificationId, StateAPM, CreationUser, CreationDate)
		SELECT AtcId, ClassificationId, StateAPM, @UserCode, getdate() FROM @AntineoplasicoMedication WHERE StateAPM = 1
	
		--Inactivos
		update Inventory.AntineoplasicoMedication set StateAPM = 2, InactivateUser = @UserCode, InactivateDate = [Common].[GETDATE]() 
		WHERE Id IN (SELECT Id from @AntineoplasicoMedication where StateAPM = 0) 
		and StateAPM <> 2

		--Retorna el ok
		select 0 as CodeResult, 'Se guardó correctamente' as MessageResult, '' as Code, 0 as Id
		return

	end try
	begin catch

		--Retorna el error
		select 999 as CodeResult, ERROR_MESSAGE() as MessageResult, '' as Code, 0 as Id
		return

	end catch

END