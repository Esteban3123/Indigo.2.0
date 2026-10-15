









-- ===============================================================================================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 15/03/2021
-- Description:	Procedimiento que se encarga de guardar los cronogramas
-- ===============================================================================================================================
CREATE PROCEDURE [MixingStation].[SP_SaveProductionSchedule]
	@Xml xml,
	@UserCode varchar(20)
AS
BEGIN
	
	--Variables que se obtienen de generar la cabecera
	declare @Id int, @Code varchar(20), @CMConfigurationId int, @ProductionLineId int, @OperatingUnitId int

	--Tabla de detalles
	declare @ProductionScheduleDetail table(CampaignDetailId int)
	
	Begin try
		
		--Se obtienen los datos para la cabecera
		select 
			@CMConfigurationId = t.x.value('CMConfigurationId[1]','int'),
			@ProductionLineId = t.x.value('ProductionLineId[1]','int'),
			@OperatingUnitId = t.x.value('OperatingUnitId[1]','int')
		from @Xml.nodes('/Data') t(x)

		--Se obtienen los detalles del xml
		insert into @ProductionScheduleDetail
		select 
			t.x.value('CampaignDetailId[1]','int') as CampaignDetailId
		from @Xml.nodes('/Data/Details') t(x)
		
		--Consultamos si la secuencia es con O o OU
		declare @scope varchar(5) = ''
		declare @idSequenceDetail int
		declare @pattern varchar(300)
		declare @NextS int
		select @scope = Scope from MixingStation.MixingStationSequence
		where IdForm = '2227'

		if @scope = 'O' --Si el ambito es por organización
		begin
			select top 1 @pattern = cs.Pattern, @NextS = bsd.[Next] , @idSequenceDetail = bsd.Id  
			from MixingStation.MixingStationSequenceDetail bsd 
			inner join MixingStation.MixingStationSequence bs on bs.Id = bsd.IdSequenseMixingStationC
			inner join Common.Sequense cs on cs.Id = bsd.IdSequense
			where bs.IdForm = '2227'
			order by bsd.Next desc
		end
		else begin --Si el ambito es por unidad operativa
			select top 1 @pattern = cs.Pattern, @NextS = bsd.[Next] , @idSequenceDetail = bsd.Id  
			from MixingStation.MixingStationSequenceDetail bsd 
			inner join MixingStation.MixingStationSequence bs on bs.Id = bsd.IdSequenseMixingStationC
			inner join Common.Sequense cs on cs.Id = bsd.IdSequense
			where bs.IdForm = '2227' and bsd.IdOperatingUnit = @OperatingUnitId
			order by bsd.Next desc
		end
					
		if (@idSequenceDetail is null)
		Begin
			select 999 as CodeMessage, 'Secuencia no encontrada para generar el cronograma' as Message, 0 Id, '' Code
			return
		End

		select @Code = dbo.GetSequence('',@pattern,@NextS)
		update MixingStation.MixingStationSequenceDetail set [Next] += 1 where Id = @idSequenceDetail

		INSERT INTO [MixingStation].[ProductionSchedule]([Code],[CMConfigurationId],[ProductionLineId],[Status],[CreationUser],[CreationDate])
		VALUES(@Code, @CMConfigurationId, @ProductionLineId, 1, @UserCode, Common.GETDATE())

		set @Id = SCOPE_IDENTITY()
		
		--Se insertan los detalles de campañas
		INSERT INTO [MixingStation].[ProductionScheduleDetail]([ProductionScheduleId],[CampaignDetailId])
		select @Id, CampaignDetailId
		from @ProductionScheduleDetail
		
		--Se retorna el ok
		select 0 as CodeMessage, 'Se generó la orden de producción con código ' + @Code as Message, @Id Id, @Code Code
		return

	End try
	Begin Catch

		--Se retorna el error
		select 999 as CodeMessage, ERROR_MESSAGE() as Message, 0 Id, '' Code
		return

	End Catch

END