


-- ====================================================================================================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 24/01/2020
-- Description:	Procedimiento que se encarga de validar que la descripción que se vaya a eliminar no este en  procesos de crystal
-- ====================================================================================================================================
CREATE PROCEDURE [Contract].[SP_ValidateDescriptionsInCrystal] 
	@CUPSEntityContractDescriptionId as int
AS
BEGIN
	
	--Tabla en donde se almacenan los errores de las validaciones
	declare @TableErrors table(MessageError varchar(max))

	--Mensaje que retorna las validaciones del sp
	declare @MessageReturn varchar(max) = ''

	--Código que retorna las validaciones del sp
	declare @CodeReturn int = 000
	
	Begin try
	
		insert into @TableErrors(MessageError)
		select 'La actividad de agendamiento ' + temp.ActivityCode + ' - ' + temp.ActivityDescription + ' utiliza esta descripción relacionada, primero reconfigure las actividades de agendamiento'
		from (
			select IDDESCRIPCIONRELACIONADA_CITA CUPSEntityContractDescriptionId, CODACTMED ActivityCode, DESACTMED ActivityDescription
			from .AGACTIMED

			union all

			select IDDESCRIPCIONRELACIONADA_CONTROL CUPSEntityContractDescriptionId, CODACTMED ActivityCode, DESACTMED ActivityDescription
			from .AGACTIMED

			union all

			select IDDESCRIPCIONRELACIONADA_DIALISIS CUPSEntityContractDescriptionId, CODACTMED ActivityCode, DESACTMED ActivityDescription
			from .AGACTIMED

			union all

			select ad.IDDESCRIPCIONRELACIONADA CUPSEntityContractDescriptionId, A.CODACTMED ActivityCode, A.DESACTMED ActivityDescription
			from .AGACTMDDD AD
			inner join .AGACTIMED A ON A.CODACTMED = AD.CODACTMED

			union all

			select ad.IDDESCRIPCIONRELACIONADA CUPSEntityContractDescriptionId, A.CODACTMED ActivityCode, A.DESACTMED ActivityDescription
			from .AGACTMEDD AD
			inner join .AGACTIMED A ON A.CODACTMED = AD.CODACTMED

			union all

			select ad.IDDESCRIPCIONRELACIONADA CUPSEntityContractDescriptionId, A.CODACTMED ActivityCode, A.DESACTMED ActivityDescription
			from .AGACTPAQUETES AD
			inner join .AGACTIMED A ON A.CODACTMED = AD.CODACTMED
		) temp
		where temp.CUPSEntityContractDescriptionId = @CUPSEntityContractDescriptionId
		group by temp.ActivityCode, temp.ActivityDescription
	
		insert into @TableErrors(MessageError)
		select 'El paquete de ordenes ' + p.CODIGO + ' - ' + p.NOMBRE + ' en el módulo de historias clínicas utiliza esta descripción relacionada, primero reconfigure los paquetes de órdenes'
		from .HCPAQORDENESD PD
		inner join .HCPAQORDENESC P ON P.ID = PD.IDHCPAQORDENESC
		where PD.IDDESCRIPCIONRELACIONADA = @CUPSEntityContractDescriptionId
		group by p.CODIGO, p.NOMBRE

		insert into @TableErrors(MessageError)
		select 'La área de otros procedimientos ' + OPC.CODAREA + ' - ' + OPC.DESAREA + ' utiliza esta descripción relacionada, primero reconfigure las áreas otros procedimientos'
		from .HCAREASD OPD
		inner join .HCAREASC OPC ON OPC.ID = OPD.IDAREASC
		where OPD.IDDESCRIPCIONRELACIONADA = @CUPSEntityContractDescriptionId
		group by OPC.CODAREA, OPC.DESAREA

		insert into @TableErrors(MessageError)
		select 'El tipo de oxigeno ' + CODVIAADM + ' - ' + DESVIAADM + ' utiliza esta descripción relacionada, primero reconfigure los tipos de oxigeno'
		from .HCPARCONO
		where IDDESCRIPCIONRELACIONADA = @CUPSEntityContractDescriptionId
		group by CODVIAADM, DESVIAADM
		
		insert into @TableErrors(MessageError)
		select 'La cama ' + temp.DescriptionBed + '(' + temp.FunctionalUnitName + ') en tarifa de camas utiliza esta descripción relacionada, primero reconfigure las tarifas de camas'
		from (
			select IDDESCRIPCIONRELACIONADA_CUPS CUPSEntityContractDescriptionId, C.DESCCAMAS DescriptionBed, UF.UFUDESCRI FunctionalUnitName
			from .CHGENTARI TC
			inner join .CHCAMASHO C ON C.CODICAMAS = TC.CODICAMAS
			inner join .INUNIFUNC UF ON UF.UFUCODIGO = TC.UFUCODIGO

			union all

			select IDDESCRIPCIONRELACIONADA_CUPS2 CUPSEntityContractDescriptionId, C.DESCCAMAS DescriptionBed, UF.UFUDESCRI FunctionalUnitName
			from .CHGENTARI TC
			inner join .CHCAMASHO C ON C.CODICAMAS = TC.CODICAMAS
			inner join .INUNIFUNC UF ON UF.UFUCODIGO = TC.UFUCODIGO
		) temp
		where temp.CUPSEntityContractDescriptionId = @CUPSEntityContractDescriptionId
		group by temp.DescriptionBed, temp.FunctionalUnitName

		insert into @TableErrors(MessageError)
		select 'La lista de chequeo ' + cast(LC.CODCONSEC as varchar(20)) + ' - ' + LC.NOMENCENF + ' utiliza esta descripción relacionada, primero reconfigure las listas de chequeo'
		from .HCLISTAPLI LD
		inner join .HCLISTACC LC ON LC.CODCONSEC = LD.IDLISTACHEQUEO
		where LD.IDDESCRIPCIONRELACIONADA = @CUPSEntityContractDescriptionId
		group by lc.CODCONSEC, LC.NOMENCENF

		insert into @TableErrors(MessageError)
		select 'La especialidad ' + temp.EspecialtyName + '(' + temp.CareCenterCode + ' - ' + temp.CareCenterName + ') en el formulario de parámetros de historia utiliza esta descripción relacionada, primero reconfigure las especialidades'
		from (
			select R.IDDESCRIPCIONRELACIONADA_CONS CUPSEntityContractDescriptionId, E.DESESPECI EspecialtyName, CA.CODCENATE CareCenterCode, CA.NOMCENATE CareCenterName
			from .HCESPSERU R
			inner join .INESPECIA E ON E.CODESPECI = R.CODESPECI
			inner join .HCTURNREC P ON P.CODESPECI = R.CODESPECI
			inner join .ADCENATEN CA ON CA.CODCENATE = P.CODCENATE

			union all

			select R.IDDESCRIPCIONRELACIONADA_CONT CUPSEntityContractDescriptionId, E.DESESPECI EspecialtyName, CA.CODCENATE CareCenterCode, CA.NOMCENATE CareCenterName
			from .HCESPSERU R
			inner join .INESPECIA E ON E.CODESPECI = R.CODESPECI
			inner join .HCTURNREC P ON P.CODESPECI = R.CODESPECI
			inner join .ADCENATEN CA ON CA.CODCENATE = P.CODCENATE

			union all

			select R.IDDESCRIPCIONRELACIONADA_INTER CUPSEntityContractDescriptionId, E.DESESPECI EspecialtyName, CA.CODCENATE CareCenterCode, CA.NOMCENATE CareCenterName
			from .HCESPSERU R
			inner join .INESPECIA E ON E.CODESPECI = R.CODESPECI
			inner join .HCTURNREC P ON P.CODESPECI = R.CODESPECI
			inner join .ADCENATEN CA ON CA.CODCENATE = P.CODCENATE

			union all

			select R.IDDESCRIPCIONRELACIONADA_INTRA CUPSEntityContractDescriptionId, E.DESESPECI EspecialtyName, CA.CODCENATE CareCenterCode, CA.NOMCENATE CareCenterName
			from .HCESPSERU R
			inner join .INESPECIA E ON E.CODESPECI = R.CODESPECI
			inner join .HCTURNREC P ON P.CODESPECI = R.CODESPECI
			inner join .ADCENATEN CA ON CA.CODCENATE = P.CODCENATE
		) temp
		where temp.CUPSEntityContractDescriptionId = @CUPSEntityContractDescriptionId
		group by temp.CareCenterCode, temp.CareCenterName, temp.EspecialtyName

		insert into @TableErrors(MessageError)
		select 'El hemocomponente ' + c.CODCOMSAM + ' - ' + c.DESCOMSAM + ' utiliza esta descripción relacionada, primero reconfigure los hemocomponentes'
		from .HCCOMSAND CD
		inner join .HCCOMSAN C ON C.ID = CD.COMSAMID
		where CD.IDDESCRIPCIONRELACIONADA = @CUPSEntityContractDescriptionId
		group by c.CODCOMSAM, c.DESCOMSAM

		insert into @TableErrors(MessageError)
		select 'El centro de atención ' + ca.CODCENATE + ' - ' + ca.NOMCENATE + ' en el formulario de parámetros de consulta externa utiliza esta descripción relacionada, primero reconfigure los centros de atención'
		from .ADFAVLIQU F
		inner join .ADCENATEN CA ON CA.CODCENATE = F.CODCENATE
		where f.IDDESCRIPCIONRELACIONADA = @CUPSEntityContractDescriptionId
		group by ca.CODCENATE, ca.NOMCENATE

		insert into @TableErrors(MessageError)
		select 'El centro de atención ' + CA.CODCENATE + ' - ' + ca.NOMCENATE + ' en parámetros de historia utiliza esta descripción relacionada, primero reconfigure los centros de atención'
		from .HCPARACA P
		inner join .ADCENATEN CA ON CA.CODCENATE = P.CODCENATE
		where p.IDDESCRIPCIONRELACIONADA_INTER = @CUPSEntityContractDescriptionId
		group by ca.CODCENATE, ca.NOMCENATE

		insert into @TableErrors(MessageError)
		select 'Esta descripción relacionada está configurada en el formulario Configurar Favoritos, primero reconfigurelos'
		from .HCFAVORTI
		where IDDESCRIPCIONRELACIONADA = @CUPSEntityContractDescriptionId
		group by IDDESCRIPCIONRELACIONADA

		insert into @TableErrors(MessageError)
		select 'El grupo de procedimiento invasivo ' + PIC.DESCRIPCION + ' utiliza esta descripción relacionada'
		from .HCGRUPINVD PID
		inner join .HCGRUPINVC PIC ON PIC.ID = PID.IDHCGRUPINVC
		where PID.IDDESCRIPCIONRELACIONADA = @CUPSEntityContractDescriptionId
		group by PIC.DESCRIPCION

		insert into @TableErrors(MessageError)
		select 'El centro de atención ' + CA.CODCENATE + ' - ' + ca.NOMCENATE + ' en parámetros de historia(PACS) utilizan esta descripción relacionada, primero reconfigure los centros de atención'
		from .HCINTESER s
		inner join .ADCENATEN CA ON CA.CODCENATE = S.CODCENATE
		where IDDESCRIPCIONRELACIONADA = @CUPSEntityContractDescriptionId
		group by ca.CODCENATE, ca.NOMCENATE

		insert into @TableErrors(MessageError)
		select 'La sala de agendamiento ' + SC.CODIGSALA + ' - ' + SC.DESCRIPSAL + ' utiliza esta descripción relacionada, primero reconfigure las salas'
		from .AGENSALAD SD
		inner join .AGENSALAC SC ON SC.CODCONCEC = SD.CODCONCEC
		where SD.IDDESCRIPCIONRELACIONADA = @CUPSEntityContractDescriptionId
		group by SC.CODIGSALA, sc.DESCRIPSAL

		insert into @TableErrors(MessageError)
		select 'Esta descripción relacionada es utilizada en en el formulario Servicios Susceptiebles de Autorización Intrahospitalario, primero reconfigurelo'
		from .ADCONFSER
		where IDDESCRIPCIONRELACIONADA = @CUPSEntityContractDescriptionId
		group by IDDESCRIPCIONRELACIONADA

		--Se obtiene el id de la descripción y el cups
		declare @ContractDescriptionId int, @CUPSEntityId int
		select @ContractDescriptionId = ContractDescriptionId, @CUPSEntityId = CUPSEntityId from Contract.CUPSEntityContractDescriptions where Id = @CUPSEntityContractDescriptionId

		insert into @TableErrors(MessageError)
		select 'La descripción ' + cd.Code + ' - ' + cd.Name + ' es utilizada por la definición de tarifas ' + dr.Code + ' con el cups ' + ce.Code + ' - ' + ce.Description
		from (
			select drdc.ContractDescriptionId, drd.CUPSEntityId, drd.DefinitionRateId
			from Contract.DefinitionRateDetailCondition drdc
			inner join Contract.DefinitionRateDetail drd on drd.Id = drdc.DefinitionRateDetailId
			where drdc.ContractDescriptionId = @ContractDescriptionId and drd.CUPSEntityId = @CUPSEntityId

			union all

			select drdc.ContractDescriptionId2 ContractDescriptionId, drd.CUPSEntityId, drd.DefinitionRateId
			from Contract.DefinitionRateDetailCondition drdc
			inner join Contract.DefinitionRateDetail drd on drd.Id = drdc.DefinitionRateDetailId
			where drdc.ContractDescriptionId2 = @ContractDescriptionId and drd.CUPSEntityId = @CUPSEntityId
		) temp
		inner join Contract.DefinitionRate dr on dr.Id = temp.DefinitionRateId
		inner join Contract.ContractDescriptions cd on cd.Id = temp.ContractDescriptionId
		inner join Contract.CUPSEntity ce on ce.Id = temp.CUPSEntityId
		group by dr.Code, cd.Code, cd.Name, ce.Code, ce.Description

		insert into @TableErrors(MessageError)
		select 'La descripción ' + cd.Code + ' - ' + cd.Name + ' es utilizada por el cubrimiento de procedimientos ' + pt.Code + ' con el cups ' + ce.Code + ' - ' + ce.Description
		from Contract.ProcedureCups pc
		inner join Contract.ProcedureTemplate pt on pt.Id = pc.ProceduresTemplateId
		inner join Contract.ContractDescriptions cd on cd.Id = pc.ContractDescriptionId
		inner join Contract.CUPSEntity ce on ce.Id = pc.CupsId
		where pc.CUPSEntityContractDescriptionId = @CUPSEntityContractDescriptionId
		group by pt.Code, cd.Code, cd.Name, ce.Code, ce.Description

		insert into @TableErrors(MessageError)
		select 'La descripción ' + cd.Code + ' - ' + cd.Name + ' es utilizada por el portafolio de autorizaciones ' + ap.Code + ' con el cups ' + ce.Code + ' - ' + ce.Description
		from [Authorization].AuthorizationPortfolioCUPSEntity apce
		inner join [Authorization].AuthorizationPortfolio ap on ap.Id = apce.AuthorizationPortfolioId
		inner join Contract.ContractDescriptions cd on cd.Id = apce.ContractDescriptionId
		inner join Contract.CUPSEntity ce on ce.Id = apce.CUPSEntityId
		where apce.ContractDescriptionId = @ContractDescriptionId and apce.CUPSEntityId = @CUPSEntityId
		group by ap.Code, cd.Code, cd.Name, ce.Code, ce.Description
		
		--Se valida si hay errores
		if exists(select 1 from @TableErrors)
		begin 
			set @CodeReturn = 888

			select @MessageReturn = STUFF((
			select CHAR(13) + CHAR(10) + MessageError
			from @TableErrors
			FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
		end

		--Se retorna el ok
		select @CodeReturn as CodeValidation, @MessageReturn as MessageValidation
		
	end try
	begin catch

		--Se retorna el error
		select 999 as CodeValidation, ERROR_MESSAGE() as MessageValidation

	end catch

END