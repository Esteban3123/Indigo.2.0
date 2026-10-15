


-- ===============================================================================================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 17/03/2021
-- Description:	Procedimiento que se encarga de generar las campañas
-- ===============================================================================================================================
CREATE PROCEDURE [MixingStation].[SP_SaveCampaign]
	@Xml xml,
	@UserCode varchar(20)
AS
BEGIN
	
	--Variables para realizar la cabecera de la campaña
	declare @Id int, @CMConfigurationId int

	--Variables para realizar el detalle de la campaña
	declare @ProductionLineId int, @UnitDoseTypeId int, @CampaignDetailId int

	--Tabla de detalles para asociar el id de la campaña realizada
	declare @XmlTable table(SourceType tinyint, StringIds varchar(max), RequestMixingStationDetailId varchar(max))
	declare @IsCamapaignNew bit
	declare @CampaignNumber int
	Begin try
		
		--Se obtienen los datos
		select 
			@Id = t.x.value('Id[1]','int'),
			@CMConfigurationId = t.x.value('CMConfigurationId[1]','int'),
			@ProductionLineId = t.x.value('ProductionLineId[1]','int'),
			@UnitDoseTypeId = t.x.value('UnitDoseTypeId[1]','int'),
			@CampaignDetailId = t.x.value('CampaignDetailId[1]','int')
		from @Xml.nodes('/Data') t(x)

		--Se obtienen los detalles del xml
		insert into @XmlTable
		select 
			t.x.value('SourceType[1]','tinyint') as SourceType,
			t.x.value('StringIds[1]','varchar(max)') as StringIds,
			t.x.value('RequestMixingStationDetailId[1]', 'varchar(max)') as RequestMixingStationDetailId
		from @Xml.nodes('/Data/Details') t(x)
		

		if @CampaignDetailId = 0
			set @IsCamapaignNew = 1
		else
			set @IsCamapaignNew = 0

		--Si no se ha generado una cabecera
		if @Id = 0 or @Id is null
		begin
			INSERT INTO [MixingStation].[Campaign]([CMConfigurationId],[CampaignQuantity])
			VALUES(@CMConfigurationId, 0)

			set @Id = SCOPE_IDENTITY()
		end
			
		--Se inserta el detalle de la campaña siempre y cuando no venga el id, si viene el id es porque va a insertar mas solicitudes a una campaña ya existente
		if @CampaignDetailId = 0
		begin
			--Se obtiene el último registro de campañas para obtener en que no. van
			set @CampaignNumber= (select ISNULL(MAX(CampaignNumber), 0) from MixingStation.CampaignDetail where CampaignId = @Id)
			INSERT INTO [MixingStation].[CampaignDetail]([CampaignId],[ProductionLineId],[UnitDoseTypeId],[Observations],[Status], [CampaignNumber],
			[CreationUser],[CreationDate])
			VALUES(@Id, @ProductionLineId, @UnitDoseTypeId, null, 1, @CampaignNumber + 1, @UserCode, Common.GETDATE())

			set @CampaignDetailId = SCOPE_IDENTITY()
		end

		declare @NewCampaignNumber int
		set @NewCampaignNumber = (@CampaignNumber + 1)

		--Se asigna el id de la campaña al detalle de la solicitud cuando son con pacientes
		update d set d.CampaignDetailId = @CampaignDetailId, d.Status = 1
		--select d.Id, d.CampaignDetailId, @CampaignDetailId, d.Status, 1 , 'Primer update'
		from @XmlTable x
		outer apply dbo.Split(x.StringIds, ',') s
		inner join MixingStation.RequestMixingStationDetailPatients d on d.Id = s.Data
		where x.SourceType in (1, 2)

		--Se asigna el id de la campaña al detalle de la solicitud cuando no tienen pacientes
		update d set d.CampaignDetailId = @CampaignDetailId
		--select d.CampaignDetailId, @CampaignDetailId, 'Segundo update'
		from @XmlTable tmp
		inner join MixingStation.RequestMixingStationDetail d on d.Id = tmp.RequestMixingStationDetailId
		--where x.SourceType in (3, 4)

		--update d set d.CampaignDetailId = @CampaignDetailId
		--from @XmlTable x
		--outer apply dbo.Split(x.StringIds, ',') s
		--inner join MixingStation.RequestMixingStationDetailPatients p on p.Id = s.Data
		--inner join MixingStation.RequestMixingStationDetail d on d.Id = p.RequestMixingStationDetailId
		----where x.SourceType in (3, 4)

		--Se actualiza la cantidad de campañas
		update c set c.CampaignQuantity = temp.CampaignQuantity
		from MixingStation.Campaign c
		inner join (
			select cd.CampaignId, COUNT(cd.CampaignId) CampaignQuantity
			from MixingStation.CampaignDetail cd
			group by cd.CampaignId
		) temp on temp.CampaignId = c.Id
		where c.Id = @Id

		--Se retorna el ok
		if @IsCamapaignNew = 1
			select 0 as CodeMessage, 'Se generó la campaña No. ' + cast(@NewCampaignNumber AS VARCHAR) + ' correctamente.'   as Message, @Id Id, @CampaignDetailId DetailId
		else
			SET @CampaignNumber = (select CampaignNumber from MixingStation.CampaignDetail where Id = @CampaignDetailId)
			select 0 as CodeMessage, 'Se agregaron las solicitudes correctamente a la campaña No. ' + cast(@CampaignNumber as varchar(11))   as Message, @Id Id, @CampaignDetailId DetailId
		return

	End try
	Begin Catch

		--Se retorna el error
		select 999 as CodeMessage, ERROR_MESSAGE() as Message, 0 Id, 0 DetailId
		return

	End Catch

END