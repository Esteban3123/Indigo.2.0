-- ==========================================================
-- Author:		Hector Rodriguez Rubiano
-- Create date: 03/04/2020
-- Description:	Store que se encarga de guardar medicamentos con resistencia bacteriana
-- ==========================================================
CREATE PROCEDURE [Inventory].[SP_SaveBacterialResistanceMedication]
	@Xml xml,
	@UserCode varchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	--Variables para asignar los valores desde el xml para poder guardar
	--declare @Id int, @AtcId int, @StartDate datetime, @EndDate datetime, @Observation varchar(500), @StateBRM tinyint

	declare @BacterialResistanceMedication table(Id int, AtcId int, StartDate datetime, EndDate datetime, Observation varchar(500), StateBRM tinyint)

	insert into @BacterialResistanceMedication
	select 
	t.x.value('Id[1]','int')
	,t.x.value('AtcId[1]','int')
	,t.x.value('StartDate[1]','datetime')
	,t.x.value('EndDate[1]','datetime')
	,t.x.value('Observation[1]','varchar(500)')
	,t.x.value('StateBRM[1]','tinyint')
	from @Xml.nodes('/Data/BRM') t(x)
	
	begin try
        --Error
		--select 999 as CodeResult, 'La unidad de medida para peso del ATC no tiene una clase seleccionada' as MessageResult, '' as Code, 0 as Id
		--return 

		--Nuevos
		insert into Inventory.BacterialResistanceMedication(AtcId, StartDate, EndDate, Observation, StateBRM, CreationUser, CreationDate)
		SELECT AtcId, StartDate, EndDate, Observation, StateBRM, @UserCode, getdate() FROM @BacterialResistanceMedication WHERE StateBRM = 1
	
		--Eliminar
		DELETE Inventory.BacterialResistanceMedication
		WHERE Id IN (SELECT Id from @BacterialResistanceMedication where StateBRM = 0)
		and StartDate > Getdate()
		
		--Inactivos
		update Inventory.BacterialResistanceMedication set StateBRM = 3, InactivateUser = @UserCode, InactivateDate = [Common].[GETDATE]() 
		WHERE Id IN (SELECT Id from @BacterialResistanceMedication where StateBRM = 0) 
		and StateBRM <> 3 and StartDate <= Getdate()

		--Modificados
		update BRM set AtcId = t.AtcId, StartDate = t.StartDate, EndDate = t.EndDate, Observation = t.Observation,
		ModificationUser = @UserCode, ModificationDate = [Common].[GETDATE]()
		from @BacterialResistanceMedication t
		inner join Inventory.BacterialResistanceMedication BRM
		ON t.StateBRM = 2
		and BRM.Id = t.Id

		--Vigentes
		update Inventory.BacterialResistanceMedication set StateBRM = 2, ModificationUser = @UserCode, ModificationDate = [Common].[GETDATE]()
		WHERE StateBRM = 1 AND [Common].[GETDATE]() BETWEEN StartDate and EndDate

		--Inactivos
		update Inventory.BacterialResistanceMedication set StateBRM = 3, InactivateUser = @UserCode, InactivateDate = [Common].[GETDATE]() 
		WHERE StateBRM <> 3 and [Common].[GETDATE]() > EndDate

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