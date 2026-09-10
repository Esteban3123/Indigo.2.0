

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

		IF @CampaignDetailId <> 0 AND EXISTS (
			SELECT 1
			FROM MixingStation.CampaignDetail cd WITH(NOLOCK)
			WHERE cd.Id = @CampaignDetailId
				AND cd.CampaignStatus IN (5, 6)
		)
		BEGIN
			select 99 as CodeMessage, 'No se pueden agregar solicitudes a una campaña procesada o terminada.' as Message, 0 Id, 0 DetailId
			return
		END

		IF EXISTS (
			SELECT 1
			FROM @XmlTable x
			OUTER APPLY dbo.Split(x.StringIds, ',') s
			INNER JOIN MixingStation.RequestMixingStationDetailPatients rmsdp WITH(NOLOCK) ON rmsdp.Id = s.Data
			INNER JOIN MixingStation.RequestMixingStationDetail rmsd WITH(NOLOCK) ON rmsd.Id = rmsdp.RequestMixingStationDetailId
			LEFT JOIN MixingStation.RequestPackageDetailStatus rpds WITH(NOLOCK) ON rpds.RequestMixingStationDetailId = rmsd.Id
			WHERE x.SourceType IN (1, 2)
				AND (
					rmsdp.Status = 3
					OR rmsd.Status = 3
					OR rmsdp.CampaignDetailId IS NOT NULL
					OR rmsd.CampaignDetailId IS NOT NULL
					OR ISNULL(rpds.Status, 1) <> 1
					OR ISNULL(rpds.QualityStatus, 0) <> 0
					OR ISNULL(rpds.SendTo, 0) <> 0
					OR rpds.PhysicalInventoryId IS NOT NULL
					OR NULLIF(LTRIM(RTRIM(ISNULL(rpds.BatchCode, ''))), '') IS NOT NULL
					OR rpds.VerificationTagUser IS NOT NULL
					OR rpds.VerificationTagDate IS NOT NULL
					OR rpds.PreparationTime IS NOT NULL
				)
		)
		BEGIN
			select 99 as CodeMessage, 'No se pueden asociar a campaña solicitudes anuladas, procesadas o ya asociadas.' as Message, 0 Id, 0 DetailId
			return
		END

		IF EXISTS (
			SELECT 1
			FROM @XmlTable x
			INNER JOIN MixingStation.RequestMixingStationDetail rmsd WITH(NOLOCK) ON rmsd.Id = TRY_CONVERT(int, x.RequestMixingStationDetailId)
			LEFT JOIN MixingStation.RequestPackageDetailStatus rpds WITH(NOLOCK) ON rpds.RequestMixingStationDetailId = rmsd.Id
			WHERE x.SourceType IN (3, 4)
				AND (
					rmsd.Status = 3
					OR rmsd.CampaignDetailId IS NOT NULL
					OR ISNULL(rpds.Status, 1) <> 1
					OR ISNULL(rpds.QualityStatus, 0) <> 0
					OR ISNULL(rpds.SendTo, 0) <> 0
					OR rpds.PhysicalInventoryId IS NOT NULL
					OR NULLIF(LTRIM(RTRIM(ISNULL(rpds.BatchCode, ''))), '') IS NOT NULL
					OR rpds.VerificationTagUser IS NOT NULL
					OR rpds.VerificationTagDate IS NOT NULL
					OR rpds.PreparationTime IS NOT NULL
				)
		)
		BEGIN
			select 99 as CodeMessage, 'No se pueden asociar a campaña solicitudes anuladas, procesadas o ya asociadas.' as Message, 0 Id, 0 DetailId
			return
		END

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
		begin
			SET @CampaignNumber = (select CampaignNumber from MixingStation.CampaignDetail where Id = @CampaignDetailId)
			select 0 as CodeMessage, 'Se agregaron las solicitudes correctamente a la campaña No. ' + cast(@CampaignNumber as varchar(11))   as Message, @Id Id, @CampaignDetailId DetailId
		end
		return

	End try
	Begin Catch

		--Se retorna el error
		select 999 as CodeMessage, ERROR_MESSAGE() as Message, 0 Id, 0 DetailId
		return

	End Catch

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera y guarda campañas de preparación farmacéutica en la estación de mezclas. Recibe un XML con la configuración de la campaña (línea de producción, tipo de dosis unitaria, solicitudes a incluir) y crea o reutiliza una cabecera de campaña en MixingStation.Campaign y su detalle en MixingStation.CampaignDetail, asignando un número de campaña correlativo. Luego vincula las solicitudes de mezcla con pacientes (RequestMixingStationDetailPatients) o sin pacientes (RequestMixingStationDetail) al detalle de campaña recién creado, y actualiza el conteo total de lotes en la cabecera. Devuelve el identificador de la campaña y el número de lote generado, o un mensaje de error si algo falla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'PROCEDURE', @level1name = N'SP_SaveCampaign';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'PROCEDURE', @level1name = N'SP_SaveCampaign';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Crea o amplía una campaña de mezclas: genera la cabecera y/o un nuevo detalle de campaña, vincula las solicitudes (con o sin pacientes) al detalle y recalcula la cantidad de detalles asociados a la cabecera.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCampaign';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe contener el nodo /Data con CMConfigurationId, ProductionLineId, UnitDoseTypeId y CampaignDetailId, y opcionalmente nodos /Data/Details con SourceType, StringIds y RequestMixingStationDetailId.; Los Ids contenidos en StringIds deben corresponder a registros existentes en MixingStation.RequestMixingStationDetailPatients cuando SourceType ∈ (1,2).; RequestMixingStationDetailId debe corresponder a un registro existente en MixingStation.RequestMixingStationDetail cuando aplica el segundo update.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCampaign';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'CampaignNumber dentro de una misma CampaignId es estrictamente creciente: cada nuevo detalle toma MAX(CampaignNumber)+1.; CampaignQuantity en MixingStation.Campaign refleja siempre el conteo real de detalles asociados tras cada ejecución exitosa.; Un nuevo CampaignDetail siempre se crea con Status=1 y sin observaciones.; Las solicitudes con pacientes asociadas reciben Status=1 al ser vinculadas al detalle de campaña.; Cualquier error es atrapado y devuelto como CodeMessage=999 sin propagar excepción.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCampaign';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Campaña de mezclas; Detalle de campaña; Línea de producción; Dosis unitaria; Solicitud de mezcla; Paciente asociado a solicitud; Configuración de estación de mezclas', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCampaign';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] MixingStation.Campaign: Cuando @Id es 0 o NULL (no existe cabecera previa) se inserta una nueva campaña con la configuración recibida y CampaignQuantity inicial = 0.; [INSERT] MixingStation.CampaignDetail: Cuando @CampaignDetailId = 0 se inserta un detalle nuevo con Status=1, Observations=NULL, CreationUser=@UserCode, CreationDate=Common.GETDATE() y CampaignNumber = MAX(CampaignNumber) existente para esa CampaignId + 1 (1 si no hay previos).; [UPDATE] MixingStation.RequestMixingStationDetailPatients: Para cada Id presente en StringIds (split por coma) cuyo SourceType ∈ (1,2), se asigna CampaignDetailId = nuevo/actual detalle y Status = 1.; [UPDATE] MixingStation.RequestMixingStationDetail: Para cada fila de @XmlTable se asigna CampaignDetailId al RequestMixingStationDetail cuyo Id = RequestMixingStationDetailId (sin filtro por SourceType).; [UPDATE] MixingStation.Campaign: Tras vincular las solicitudes, CampaignQuantity de la cabecera @Id se recalcula como COUNT de filas en MixingStation.CampaignDetail con esa CampaignId.; [RETURN_RESULT] : Si la campaña era nueva (@CampaignDetailId inicial = 0) retorna CodeMessage=0 con mensaje ''Se generó la campaña No. <n>...''; en caso contrario retorna ''Se agregaron las solicitudes correctamente a la campaña No. <n>''; ante excepción retorna CodeMessage=999 con ERROR_MESSAGE().', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCampaign';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @CampaignDetailId = 0 → Marca @IsCamapaignNew=1, calcula siguiente CampaignNumber e inserta nuevo CampaignDetail. else Marca @IsCamapaignNew=0 y reutiliza el CampaignDetailId recibido (agrega solicitudes a campaña existente).; si @Id = 0 OR @Id IS NULL → Inserta una nueva cabecera en MixingStation.Campaign y toma SCOPE_IDENTITY() como @Id. else Reutiliza la cabecera existente identificada por @Id.; si x.SourceType IN (1,2) → Actualiza RequestMixingStationDetailPatients (vincula pacientes al CampaignDetail y pone Status=1).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCampaign';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Split; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCampaign';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.CampaignDetail; MixingStation.Campaign; MixingStation.RequestMixingStationDetailPatients; MixingStation.RequestMixingStationDetail; dbo.Split', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCampaign';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCampaign';
-- GO
