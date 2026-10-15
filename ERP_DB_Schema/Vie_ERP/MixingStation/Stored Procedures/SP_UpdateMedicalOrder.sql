-- ===============================================================================================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 24/01/2021
-- Description:	Procedimiento que se encarga de actualizar el campo de observacion en la tabla de ordenes medicas
-- ===============================================================================================================================
CREATE PROCEDURE [MixingStation].[SP_UpdateMedicalOrder]
	@Xml xml,
	@UserCode varchar(20)
AS
BEGIN
	
	--Id de la central de mezcla
	declare @CMConfigurationId int
	--Tabla de para obtener el xml
	declare @XmlTable table(Id int, Observations varchar(max), SendTo tinyint, SourceType tinyint, ConfirmationUnitDoseId int, PatientCode varchar(25), Bed varchar(max), CodeSusceptibleMixingStation varchar(36), EntityId int, EntityName varchar(50))	
	--Tabla para obtener los registros principales
	declare @XmlTablePrincipal table(SendTo tinyint, SourceType tinyint, CodeSusceptibleMixingStation varchar(max), PatientCode varchar(25), ConfirmationUnitDoseId int, Bed varchar(max), EntityId int, EntityName varchar(50))	
	--Código e identificador de la solicitud
	declare @Code varchar(20) = '', @Id int
	--Variables para llamar a otros SP
	DECLARE @SubXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX)

	Begin try
			
		--Se obtienen los datos para la cabecera
		select 
			@CMConfigurationId = t.x.value('CMConfigurationId[1]','int')
		from @Xml.nodes('/Header') t(x)

		--Se obtienen los detalles del xml
		insert into @XmlTable
		select 
			t.x.value('Id[1]','int') as Id,
			IIF(t.x.value('Observations[1]','varchar(max)') = '', null, t.x.value('Observations[1]','varchar(max)')) as Observations,
			IIF(t.x.value('SendTo[1]','tinyint') = '', null, t.x.value('SendTo[1]','tinyint')) as SendTo,
			t.x.value('SourceType[1]','tinyint') as SourceType,
			t.x.value('CodeSusceptibleMixingStation[1]','varchar(36)') as CodeSusceptibleMixingStation,
			IIF(t.x.value('ConfirmationUnitDoseId[1]','varchar(20)') = '', null, t.x.value('ConfirmationUnitDoseId[1]','varchar(20)')) as ConfirmationUnitDoseId,
			t.x.value('PatientCode[1]','varchar(25)') as PatientCode,
		    IIF(t.x.value('Bed[1]','varchar(max)') = '', null, t.x.value('Bed[1]','varchar(max)')) as Bed,
			IIF(t.x.value('EntityId[1]','varchar(20)') = '', null, t.x.value('EntityId[1]','varchar(20)')) as EntityId,
			IIF(t.x.value('EntityName[1]','varchar(max)') = '', null, t.x.value('EntityName[1]','varchar(max)')) as EntityName
		from @Xml.nodes('/SendOneToOne') t(x)
		
		if not exists(select 1 from @XmlTable) --Si no llegaron datos es porque se enviaron los registros principales
		begin
			insert into @XmlTablePrincipal
			select 
				t.x.value('SendTo[1]','tinyint') as SendTo,
				t.x.value('SourceType[1]','tinyint') as SourceType,
				t.x.value('CodeSusceptibleMixingStation[1]','varchar(max)') as CodeSusceptibleMixingStation,
				t.x.value('PatientCode[1]', 'varchar(25)'),
				IIF(t.x.value('ConfirmationUnitDoseId[1]','varchar(20)') = '', null, t.x.value('ConfirmationUnitDoseId[1]','varchar(20)')) as ConfirmationUnitDoseId,
				IIF(t.x.value('Bed[1]','varchar(max)') = '', null, t.x.value('Bed[1]','varchar(max)')) as Bed,
				IIF(t.x.value('EntityId[1]','varchar(20)') = '', null, t.x.value('EntityId[1]','varchar(20)')) as EntityId,
				IIF(t.x.value('EntityName[1]','varchar(max)') = '', null, t.x.value('EntityName[1]','varchar(max)')) as EntityName
			from @Xml.nodes('/SendComplete') t(x)
			
			insert into @XmlTable
			select ps.Id, ps.FullProductName, x.SendTo, x.SourceType, x.ConfirmationUnitDoseId, x.PatientCode, x.Bed, x.CodeSusceptibleMixingStation, NULL, NULL
			from @XmlTablePrincipal x
			join MedicalHistory.ProductSusceptibleMixingStation ps (NOLOCK) on ps.CodeSusceptibleMixingStation = x.CodeSusceptibleMixingStation
			where x.SourceType = 1

			INSERT INTO @XmlTable
			SELECT NULL, NULL, x.SendTo, x.SourceType, x.ConfirmationUnitDoseId, '', '' Bed, NULL, x.EntityId, x.EntityName
			FROM @XmlTablePrincipal x
			WHERE x.SourceType = 2
		END

		IF EXISTS(SELECT 1
					FROM dbo.HCFARMEPD d
					INNER JOIN @XmlTable x on x.CodeSusceptibleMixingStation = d.CodeSusceptibleMixingStation
					WHERE x.SourceType = 1 and x.SendTo = 1 AND d.VIEPROCESSED = 1)
			BEGIN
				SELECT 999 AS CodeMessage, 'No se puede enviar a farmacia debido a que una o más dosis ya han sido procesadas por central de mezclas' AS Message
				RETURN
		END

		--Se actualiza la tabla de ordenes medicas.
		--Permite reasignar el enrutamiento entre farmacia (1) y central de mezclas (2).
		--Al enrutar a farmacia (SendTo = 1) se limpia CodeSusceptibleMixingStation.
		UPDATE d SET
			--d.OBSERVATIONSCM = x.Observations,
			d.VIEPROCESSED = iif(x.SendTo = 1, 0, 1),
			d.SENDTO = x.SendTo,
			d.CodeSusceptibleMixingStation = IIF(x.SendTo = 1, NULL, d.CodeSusceptibleMixingStation)
		from dbo.HCFARMEPD d
		inner join @XmlTable x on x.CodeSusceptibleMixingStation = d.CodeSusceptibleMixingStation
		where x.SourceType = 1

		--Se realiza la solicitud de central de mezclas
		if exists (select 1 from @XmlTable where SendTo = 2)
		begin
			
			--Consultamos si la secuencia es con O o OU
			declare @scope varchar(5) = ''
			declare @idSequenceDetail int
			declare @pattern varchar(300)
			declare @NextS int
			select @scope = Scope from MixingStation.MixingStationSequence
			where IdForm = '2224'

			select top 1 @pattern = cs.Pattern, @NextS = bsd.[Next] , @idSequenceDetail = bsd.Id  
			from MixingStation.MixingStationSequenceDetail bsd 
			inner join MixingStation.MixingStationSequence bs on bs.Id = bsd.IdSequenseMixingStationC
			inner join Common.Sequense cs on cs.Id = bsd.IdSequense
			where bs.IdForm = '2224'
			order by bsd.Next desc
					
			if (@idSequenceDetail is null)
			Begin
				select 999 as CodeMessage, 'Secuencia no encontrada para generar la solicitud a central de mezclas' as Message
				return
			End

			select @Code = dbo.GetSequence('',@pattern,@NextS)
			update MixingStation.MixingStationSequenceDetail set [Next] += 1 where Id = @idSequenceDetail

			--Se realiza la cabecera de la solicitud
			INSERT INTO [MixingStation].[RequestMixingStation]([Code],[Status],[RequestUser],[RequestDate], [CMConfigurationId])
			VALUES(@Code, 2, @UserCode, Common.GETDATE(), @CMConfigurationId)

			--Se obtiene el id generado de la cabecera
			set @Id = SCOPE_IDENTITY()

			--Tabla en donde se almacenan los ids
			declare @IDSDetail table(RequestMixingStationDetailId int, ConfirmationUnitDoseId int)

			--Se crean los detalles de la solicitud
			INSERT INTO [MixingStation].[RequestMixingStationDetail]([RequestMixingStationId],[ATCId],[PackageId],[UnitDoseTypeId],[Quantity],[Status],[EntityId],[EntityName], 
			[CareCenterCode], [Source],[ProductionLineId], [PackagePersonalizedId]) output inserted.Id, inserted.EntityId into @IDSDetail(RequestMixingStationDetailId, ConfirmationUnitDoseId)
			select @Id, null, cud.PackageId, p.UnitDoseTypeId, null, null, x.ConfirmationUnitDoseId, 'ConfirmationUnitDose', cud.CareCenterCode, 
			cud.Source, cud.ProductionLineId, cud.PersonalizedMasterPreparationPackageId
			from @XmlTable x
			inner join MixingStation.ConfirmationUnitDose cud on cud.Id = x.ConfirmationUnitDoseId
			inner join MixingStation.Package p on p.Id = cud.PackageId
			where x.SendTo = 2
			group by x.ConfirmationUnitDoseId, cud.PackageId, p.UnitDoseTypeId, cud.CareCenterCode, cud.Source, cud.PersonalizedMasterPreparationPackageId,cud.ProductionLineId
			
			--Se guardan los pacientes del detalle
			INSERT INTO [MixingStation].[RequestMixingStationDetailPatients]([RequestMixingStationDetailId],[PatientCode],[Bed],[Quantity],[EntityId],[EntityName], 
			[Status], [FunctionalUnitCode], [AdministrationRouteId])
			select data.RequestMixingStationDetailId, data.PatientCode, data.Bed, data.Quantity, data.EntityId, data.EntityName, 1, data.FunctionalUnitCode, data.AdministrationRouteId
			from (
				select ids.RequestMixingStationDetailId, RTRIM(LTRIM(x.PatientCode)) PatientCode, x.Bed Bed, 1 Quantity, 
					ps.Id EntityId, 'ProductSusceptibleMixingStation' EntityName, RTRIM(LTRIM(ps.FunctionalUnitCode)) FunctionalUnitCode,
					isnull(ar.Id, ar2.Id) AdministrationRouteId
				from @IDSDetail ids
				inner join @XmlTable x on x.ConfirmationUnitDoseId = ids.ConfirmationUnitDoseId
				join MedicalHistory.ProductSusceptibleMixingStation ps on x.Id = ps.Id
				LEFT JOIN HCPRESCRA pc (nolock) on ps.IdOrigin = pc.ID and ps.Origin = 'HCPRESCRA'
				LEFT JOIN Inventory.AdministrationRoute ar (nolock) on ltrim(rtrim(pc.CODVIAADM)) = ar.Code
				LEFT JOIN HCINFLIQA lq (nolock) on ps.IdOrigin = lq.CONSECUTI and ps.Origin = 'HCINFLIQA'
				LEFT JOIN HCINFLIQD ld (nolock) on lq.CODCONCEC = ld.CODCONCEC
				LEFT JOIN Inventory.AdministrationRoute ar2 (nolock) on ltrim(rtrim(ld.VIAADMDIL)) = ar2.Code
				WHERE x.SendTo = 2

				UNION ALL

				SELECT ids.RequestMixingStationDetailId, pecc.IdentificationNumber PatientCode, p.Bed, 1 Quantity, 
				epp.Id EntityId, 'ExternalPatientPreparation' EntityName, p.ExternalFunctionalUnitCode FunctionalUnitCode,
				epp.AdministrationRouteId
				FROM @IDSDetail ids
				INNER JOIN @XmlTable x on x.ConfirmationUnitDoseId = ids.ConfirmationUnitDoseId
				INNER JOIN MixingStation.ExternalPatientPreparation epp ON epp.Id = x.EntityId AND x.EntityName = 'ExternalPatientPreparation'
				INNER JOIN MixingStation.RequestUnitDoseExternalCareCenterPatient p ON p.Id = epp.RequestUnitDoseExternalCareCenterPatientId
				INNER JOIN MixingStation.PatientExternalCareCenter pecc ON pecc.Id = p.PatientExternalCareCenterId
				WHERE x.SendTo = 2 AND x.SourceType = 2
				
			) data

			-- Actualizamos las cantidades
			update dt set dt.Quantity = (
				select sum(Quantity) from [MixingStation].[RequestMixingStationDetailPatients] pt where pt.RequestMixingStationDetailId = tb.RequestMixingStationDetailId
			), dt.Status = 1 
			from [MixingStation].[RequestMixingStationDetail] dt
			inner join @IDSDetail tb on dt.Id = tb.RequestMixingStationDetailId

			--Se actualiza el campo en donde se identifica que esa confirmación ya esta en un proceso de solicitud central mezclas
			update c set c.RequestMixingStationDetailId = x.RequestMixingStationDetailId
			from @IDSDetail x
			inner join MixingStation.ConfirmationUnitDose c on c.Id = x.ConfirmationUnitDoseId

			--------------------------------------------------------------------

			SELECT @SubXml = CONVERT
			(
				XML, 
				(
					SELECT *
					FROM (
						SELECT 
							ConfirmationUnitDoseId Id
						FROM @XmlTable
					) ConfirmationUnitDose
					For xml AUTO,TYPE, ELEMENTS
				)
			)

			EXEC [MixingStation].[SP_AddManualPharmaDose] @SubXml, @Code_Output OUT, @Message_Output OUT

			IF @Code_Output <> 0
			BEGIN
				SELECT @Code_Output as CodeMessage, @Message_Output as Message
				RETURN
			END

			--------------------------------------------------------------------

		end

		declare @MessageReturn varchar(max) = 'Solicitudes enviadas correctamente' + IIF(@Code <> '',  CHAR(13) + CHAR(10) + 'Se generó solicitud central de mezclas con código ' + @Code, '')
		
		--Se retorna el ok
		select 0 as CodeMessage, @MessageReturn as Message
		return

	End try
	Begin Catch

		--Se retorna el error
		select 999 as CodeMessage, ERROR_MESSAGE() as Message
		return

	End Catch

END