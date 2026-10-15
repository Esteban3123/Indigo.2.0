


-- ====================================================================================================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 24/01/2020
-- Description:	Procedimiento que se encarga de validar que el cups no este parametrizado en las tablas de Crystal sin descripción relacionada
-- ====================================================================================================================================
CREATE PROCEDURE [Contract].[SP_ValidateCUPSInCrystal] 
	@CUPSEntityCode as varchar(20)
AS
BEGIN
	
	--Tabla en donde se almacenan los errores de las validaciones
	declare @TableErrors table(MessageError varchar(max))
	
	Begin try
	
		insert into @TableErrors(MessageError)
		select 'El CUPS ' + @CUPSEntityCode + ' en la actividad de agendamiento ' + temp.ActivityCode + ' - ' + temp.ActivityDescription + ' está sin descripción relacionada'
		from (
			select IDDESCRIPCIONRELACIONADA_CITA CUPSEntityContractDescriptionId, CODACTMED ActivityCode, DESACTMED ActivityDescription, CODSERIPS CUPSEntityCode
			from .AGACTIMED

			union all

			select IDDESCRIPCIONRELACIONADA_CONTROL CUPSEntityContractDescriptionId, CODACTMED ActivityCode, DESACTMED ActivityDescription, CODSERIPS CUPSEntityCode
			from .AGACTIMED

			union all

			select IDDESCRIPCIONRELACIONADA_DIALISIS CUPSEntityContractDescriptionId, CODACTMED ActivityCode, DESACTMED ActivityDescription, CODSERIPS CUPSEntityCode
			from .AGACTIMED

			union all

			select ad.IDDESCRIPCIONRELACIONADA CUPSEntityContractDescriptionId, A.CODACTMED ActivityCode, A.DESACTMED ActivityDescription, A.CODSERIPS CUPSEntityCode
			from .AGACTMDDD AD
			inner join .AGACTIMED A ON A.CODACTMED = AD.CODACTMED

			union all

			select ad.IDDESCRIPCIONRELACIONADA CUPSEntityContractDescriptionId, A.CODACTMED ActivityCode, A.DESACTMED ActivityDescription, A.CODSERIPS CUPSEntityCode
			from .AGACTMEDD AD
			inner join .AGACTIMED A ON A.CODACTMED = AD.CODACTMED

			union all

			select ad.IDDESCRIPCIONRELACIONADA CUPSEntityContractDescriptionId, A.CODACTMED ActivityCode, A.DESACTMED ActivityDescription, A.CODSERIPS CUPSEntityCode
			from .AGACTPAQUETES AD
			inner join .AGACTIMED A ON A.CODACTMED = AD.CODACTMED
		) temp
		where temp.CUPSEntityCode = @CUPSEntityCode and temp.CUPSEntityContractDescriptionId is null
		group by temp.ActivityCode, temp.ActivityDescription
	
		insert into @TableErrors(MessageError)
		select 'El CUPS ' + @CUPSEntityCode + ' en el paquete de ordenes ' + p.CODIGO + ' - ' + p.NOMBRE + ' en el módulo de historias clínicas está sin descripción relacionada'
		from .HCPAQORDENESD PD
		inner join .HCPAQORDENESC P ON P.ID = PD.IDHCPAQORDENESC
		where PD.CODIGOSERVICIO = @CUPSEntityCode and PD.IDDESCRIPCIONRELACIONADA is null
		group by p.CODIGO, p.NOMBRE

		insert into @TableErrors(MessageError)
		select 'El CUPS ' + @CUPSEntityCode + ' en el área de otros procedimientos ' + OPC.CODAREA + ' - ' + OPC.DESAREA + ' está sin descripción relacionada'
		from .HCAREASD OPD
		inner join .HCAREASC OPC ON OPC.ID = OPD.IDAREASC
		where OPD.CODSERIPS = @CUPSEntityCode and OPD.IDDESCRIPCIONRELACIONADA is null
		group by OPC.CODAREA, OPC.DESAREA

		insert into @TableErrors(MessageError)
		select 'El CUPS ' + @CUPSEntityCode + ' en el tipo de oxigeno ' + CODVIAADM + ' - ' + DESVIAADM + ' está sin descripción relacionada'
		from .HCPARCONO
		where CODSERIPS = @CUPSEntityCode and IDDESCRIPCIONRELACIONADA is null
		group by CODVIAADM, DESVIAADM
		
		insert into @TableErrors(MessageError)
		select 'El CUPS ' + @CUPSEntityCode + ' en la cama ' + temp.DescriptionBed + '(' + temp.FunctionalUnitName + ') en tarifa de camas está sin descripción relacionada'
		from (
			select IDDESCRIPCIONRELACIONADA_CUPS CUPSEntityContractDescriptionId, C.DESCCAMAS DescriptionBed, UF.UFUDESCRI FunctionalUnitName, TC.GENCUPS CUPSEntityCode
			from .CHGENTARI TC
			inner join .CHCAMASHO C ON C.CODICAMAS = TC.CODICAMAS
			inner join .INUNIFUNC UF ON UF.UFUCODIGO = TC.UFUCODIGO

			union all

			select IDDESCRIPCIONRELACIONADA_CUPS2 CUPSEntityContractDescriptionId, C.DESCCAMAS DescriptionBed, UF.UFUDESCRI FunctionalUnitName, TC.GENCUPS2 CUPSEntityCode
			from .CHGENTARI TC
			inner join .CHCAMASHO C ON C.CODICAMAS = TC.CODICAMAS
			inner join .INUNIFUNC UF ON UF.UFUCODIGO = TC.UFUCODIGO
		) temp
		where cast(temp.CUPSEntityCode as varchar(20)) = @CUPSEntityCode and temp.CUPSEntityContractDescriptionId is null
		group by temp.DescriptionBed, temp.FunctionalUnitName

		insert into @TableErrors(MessageError)
		select 'El CUPS ' + @CUPSEntityCode + ' en la lista de chequeo ' + cast(LC.CODCONSEC as varchar(20)) + ' - ' + LC.NOMENCENF + ' está sin descripción relacionada'
		from .HCLISTAPLI LD
		inner join .HCLISTACC LC ON LC.CODCONSEC = LD.IDLISTACHEQUEO
		where LD.CODSERIPS = @CUPSEntityCode and LD.IDDESCRIPCIONRELACIONADA is null
		group by lc.CODCONSEC, LC.NOMENCENF

		insert into @TableErrors(MessageError)
		select 'El CUPS ' + @CUPSEntityCode + ' en la especialidad ' + temp.EspecialtyName + '(' + temp.CareCenterCode + ' - ' + temp.CareCenterName + ') en el formulario de parámetros de historia está sin descripción relacionada'
		from (
			select R.IDDESCRIPCIONRELACIONADA_CONS CUPSEntityContractDescriptionId, E.DESESPECI EspecialtyName, CA.CODCENATE CareCenterCode, CA.NOMCENATE CareCenterName, r.CODSERCEX CUPSEntityCode
			from .HCESPSERU R
			inner join .INESPECIA E ON E.CODESPECI = R.CODESPECI
			inner join .HCTURNREC P ON P.CODESPECI = R.CODESPECI
			inner join .ADCENATEN CA ON CA.CODCENATE = P.CODCENATE

			union all

			select R.IDDESCRIPCIONRELACIONADA_CONT CUPSEntityContractDescriptionId, E.DESESPECI EspecialtyName, CA.CODCENATE CareCenterCode, CA.NOMCENATE CareCenterName, r.CODSERCEC CUPSEntityCode
			from .HCESPSERU R
			inner join .INESPECIA E ON E.CODESPECI = R.CODESPECI
			inner join .HCTURNREC P ON P.CODESPECI = R.CODESPECI
			inner join .ADCENATEN CA ON CA.CODCENATE = P.CODCENATE

			union all

			select R.IDDESCRIPCIONRELACIONADA_INTER CUPSEntityContractDescriptionId, E.DESESPECI EspecialtyName, CA.CODCENATE CareCenterCode, CA.NOMCENATE CareCenterName, CODSERINT CUPSEntityCode
			from .HCESPSERU R
			inner join .INESPECIA E ON E.CODESPECI = R.CODESPECI
			inner join .HCTURNREC P ON P.CODESPECI = R.CODESPECI
			inner join .ADCENATEN CA ON CA.CODCENATE = P.CODCENATE

			union all

			select R.IDDESCRIPCIONRELACIONADA_INTRA CUPSEntityContractDescriptionId, E.DESESPECI EspecialtyName, CA.CODCENATE CareCenterCode, CA.NOMCENATE CareCenterName, r.CODSERIPSINTRA CUPSEntityCode
			from .HCESPSERU R
			inner join .INESPECIA E ON E.CODESPECI = R.CODESPECI
			inner join .HCTURNREC P ON P.CODESPECI = R.CODESPECI
			inner join .ADCENATEN CA ON CA.CODCENATE = P.CODCENATE
		) temp
		where temp.CUPSEntityCode = @CUPSEntityCode and temp.CUPSEntityContractDescriptionId is null
		group by temp.CareCenterCode, temp.CareCenterName, temp.EspecialtyName

		insert into @TableErrors(MessageError)
		select 'El CUPS ' + @CUPSEntityCode + ' en el hemocomponente ' + c.CODCOMSAM + ' - ' + c.DESCOMSAM + ' está sin descripción relacionada'
		from .HCCOMSAND CD
		inner join .HCCOMSAN C ON C.ID = CD.COMSAMID
		where CD.CODSERIPS = @CUPSEntityCode and CD.IDDESCRIPCIONRELACIONADA is null
		group by c.CODCOMSAM, c.DESCOMSAM

		insert into @TableErrors(MessageError)
		select 'El CUPS ' + @CUPSEntityCode + ' en el centro de atención ' + ca.CODCENATE + ' - ' + ca.NOMCENATE + ' en el formulario de parámetros de consulta externa está sin descripción relacionada'
		from .ADFAVLIQU F
		inner join .ADCENATEN CA ON CA.CODCENATE = F.CODCENATE
		where f.CODSERIPS = @CUPSEntityCode and f.IDDESCRIPCIONRELACIONADA is null
		group by ca.CODCENATE, ca.NOMCENATE

		insert into @TableErrors(MessageError)
		select 'El CUPS ' + @CUPSEntityCode + ' en el centro de atención ' + CA.CODCENATE + ' - ' + ca.NOMCENATE + ' en parámetros de historia está sin descripción relacionada'
		from .HCPARACA P
		inner join .ADCENATEN CA ON CA.CODCENATE = P.CODCENATE
		where p.CODSERINT = @CUPSEntityCode and p.IDDESCRIPCIONRELACIONADA_INTER is null
		group by ca.CODCENATE, ca.NOMCENATE

		insert into @TableErrors(MessageError)
		select 'El CUPS ' + @CUPSEntityCode + ' en el formulario Configurar Favoritos está sin descripción relacionada'
		from .HCFAVORTI
		where CODIGITEM = @CUPSEntityCode and IDDESCRIPCIONRELACIONADA is null
		group by CODIGITEM

		insert into @TableErrors(MessageError)
		select 'El CUPS ' + @CUPSEntityCode + ' en el grupo de procedimiento invasivo ' + PIC.DESCRIPCION + ' está sin descripción relacionada'
		from .HCGRUPINVD PID
		inner join .HCGRUPINVC PIC ON PIC.ID = PID.IDHCGRUPINVC
		where PID.CODSERIPS = @CUPSEntityCode and PID.IDDESCRIPCIONRELACIONADA is null
		group by PIC.DESCRIPCION

		insert into @TableErrors(MessageError)
		select 'El CUPS ' + @CUPSEntityCode + ' en el centro de atención ' + CA.CODCENATE + ' - ' + ca.NOMCENATE + ' en parámetros de historia(PACS) está sin descripción relacionada'
		from .HCINTESER s
		inner join .ADCENATEN CA ON CA.CODCENATE = S.CODCENATE
		where CODSERIPS = @CUPSEntityCode and IDDESCRIPCIONRELACIONADA is null
		group by ca.CODCENATE, ca.NOMCENATE

		insert into @TableErrors(MessageError)
		select 'El CUPS ' + @CUPSEntityCode + ' en la sala de agendamiento ' + SC.CODIGSALA + ' - ' + SC.DESCRIPSAL + ' está sin descripción relacionada'
		from .AGENSALAD SD
		inner join .AGENSALAC SC ON SC.CODCONCEC = SD.CODCONCEC
		where SD.CODSERIPS = @CUPSEntityCode and SD.IDDESCRIPCIONRELACIONADA is null
		group by SC.CODIGSALA, sc.DESCRIPSAL

		insert into @TableErrors(MessageError)
		select 'El CUPS ' + @CUPSEntityCode + ' en el formulario Servicios Susceptiebles de Autorización Intrahospitalario está sin descripción relacionada'
		from .ADCONFSER
		where CODSERIPS = @CUPSEntityCode and IDDESCRIPCIONRELACIONADA is null
		group by CODSERIPS

		--Se valida si hay errores
		if exists(select 1 from @TableErrors)
		begin 
			declare @MessageReturn varchar(max) = ''

			select @MessageReturn = STUFF((
			select CHAR(13) + CHAR(10) + MessageError
			from @TableErrors
			FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			select 999 as CodeValidation, @MessageReturn as MessageValidation
			return
		end

		--Se retorna el ok
		select 000 as CodeValidation, 'Validación Exitosa' as MessageValidation
		return

	end try
	begin catch

		--Se retorna el error
		select 999 as CodeValidation, ERROR_MESSAGE() as MessageValidation
		return

	end catch

END