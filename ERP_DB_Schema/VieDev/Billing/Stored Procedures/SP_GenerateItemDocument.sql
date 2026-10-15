
-- =============================================
-- Author:		Diego A. Roldán
-- Create date: 2018-01-13
-- Description:	SP que devuelve los datos ingreso
-- =============================================

CREATE PROCEDURE [Billing].[SP_GenerateItemDocument]
	@InvoiceId int,
	@AdmissionNumberInvoice varchar(20),
	@itemType tinyint, --1: laboratorios, 2: patologias, 3: Imagenes, 4: Consulta Externa
	@numIngres varchar(20),
	@careGroupId int,
	@healthAdministratorId int,
	@codentida varchar(20),
	@CodigoPaciente varchar(20),
	@CodigoServicio varchar(20),
	@FechaCita datetime,
	@NombreCompletoPaciente varchar(100),
	@CodigoCentroAtencion varchar(20),
	@CodigoUnidadFuncional varchar(20),
	@TipoCita int,
	@CodigoProfesional varchar(20),
	@IsFalseId bit,
	@Codigo varchar(20),
	@CodigoEspecialidad varchar(20),
	@CareGroupIdInvoice int,
	@HealthAdministratorIdInvoice int,
	@InvoiceNumber varchar(20),
	@CantidadServicio int,
	@CUPSEntityContractDescriptionId int,
	@TypeOfScheduleActivity int --Valida el tipo de actividad de agendamiento, si es mixta(5) solo inserta en ADCONCOEX
AS
BEGIN
	SET NOCOUNT ON;
	
	begin try
		
		declare @generatedId int = 0
		declare @admissionnumber varchar(20) = iif(@InvoiceId > 0, @AdmissionNumberInvoice, @numIngres)
		declare @auto int
		declare @codconcec numeric(18,0)

		declare @fechaRegistro datetime = [Common].[GETDATE]()
		declare @flagInsertADCONCOEX bit =1

		/*Bandera que determina si se debe insertar en ADCONCOEX, ya que solo lo debe hacer siempre y cuando no tenga pendiente
		de consultas (solo queda para Imagenes y laboratorios), cuando el tipo de actividad es mixta, se omitirá esta validación*/
		if EXISTS(	SELECT 1 
					FROM .ADCONCOEX
					WHERE IPCODPACI =@CodigoPaciente and CONESTADO=1) AND @TypeOfScheduleActivity <> 5 BEGIN
			
				SET	@flagInsertADCONCOEX =0
		END

		if @itemType = 1 begin

			IF @flagInsertADCONCOEX = 1 
			BEGIN
				insert into .ADCONCOEX(IPCODPACI,IPFECHACO,IPFECHCIT,CONESTADO,IPNOMCOMP,CODENTIDA,CODCENATE,UFUCODIGO,
				CODTIPCON,CODPROSAL,NUMINGRES,NUMCONCIT,INDAUDFOR,AUTESTADO,CODESPECI,LIQUIDAR,PRIMERLLA,SEGUNDLLA,TERCERLLA,
				GENCAREGROUP,GENCONENTITY,GENINVOICE,GENINVOICEID, IDDESCRIPCIONRELACIONADA, FECREGSIS )
				values(@CodigoPaciente,@FechaCita,@FechaCita,1,@NombreCompletoPaciente,ltrim(rtrim(@codentida)),@CodigoCentroAtencion,
					@CodigoUnidadFuncional,@TipoCita,@CodigoProfesional,@admissionnumber,iif(@IsFalseId = 0, @Codigo, null),0,
					null,@CodigoEspecialidad,iif(@InvoiceId > 0, 0, 1),0,0,0,iif(@InvoiceId > 0,@CareGroupIdInvoice,@careGroupId),
					iif(@InvoiceId > 0,@HealthAdministratorIdInvoice,@healthAdministratorId),@InvoiceNumber,@InvoiceId, @CUPSEntityContractDescriptionId , 
					@fechaRegistro )

				set @codconcec = scope_identity()

				if @IsFalseId = 0 
				begin
					insert into .ADCONCOED (CODCONCEC,NUMCONCIT)
					values(@codconcec,iif(@IsFalseId = 0, @Codigo, null))
				end
			END
			IF @TypeOfScheduleActivity <> 5 BEGIN
			
				insert into .AMBORDLAB (IPCODPACI,NUMINGRES,CODCENATE,UFUCODIGO,CODPROSAL,FECORDMED,
					CODSERIPS,CANSERIPS,ESTSERIPS,INDAUDFOR,ESTALELAB,IPFECHACO,NUMCONCIT,GENCAREGROUP,GENCONENTITY,
					GENINVOICE,GENINVOICEID, IDDESCRIPCIONRELACIONADA)
				values (@CodigoPaciente,@admissionnumber,@CodigoCentroAtencion,@CodigoUnidadFuncional,IIF(@CodigoProfesional = '', NULL, @CodigoProfesional),
					@FechaCita,@CodigoServicio,iif(@CantidadServicio > 0, @CantidadServicio, 1),1,0,0,@FechaCita,
					iif(@IsFalseId = 0,@Codigo,null),iif(@InvoiceId > 0,@CareGroupIdInvoice,@careGroupId),
					iif(@InvoiceId > 0,@HealthAdministratorIdInvoice,@healthAdministratorId),@InvoiceNumber,@InvoiceId, @CUPSEntityContractDescriptionId)
		END
			set @generatedId = scope_identity()
		end
		else if @itemType = 2 begin

			select @auto = [AUTO] from .AMBORDPAT with(nolock)
			where NUMINGRES = @admissionnumber And IPCODPACI = @CodigoPaciente And CODSERIPS = @CodigoServicio

			if @auto > 0 begin
				select '999' as CodeResult, 'Ya existe un registro de patologías para el ingreso (' + 
					@admissionnumber + '), paciente (' + @CodigoPaciente + ') y servicio (' + @CodigoServicio + ')' as MessageResult, 0 as GenerateId
				return
			end

			IF @flagInsertADCONCOEX = 1 BEGIN
				insert into .ADCONCOEX(IPCODPACI,IPFECHACO,IPFECHCIT,CONESTADO,IPNOMCOMP,CODENTIDA,CODCENATE,UFUCODIGO,
				CODTIPCON,CODPROSAL,NUMINGRES,NUMCONCIT,INDAUDFOR,AUTESTADO,CODESPECI,LIQUIDAR,PRIMERLLA,SEGUNDLLA,TERCERLLA,
				GENCAREGROUP,GENCONENTITY,GENINVOICE,GENINVOICEID, IDDESCRIPCIONRELACIONADA, FECREGSIS )
				values(@CodigoPaciente,@FechaCita,@FechaCita,1,@NombreCompletoPaciente,ltrim(rtrim(@codentida)),@CodigoCentroAtencion,
					@CodigoUnidadFuncional,@TipoCita,@CodigoProfesional,@admissionnumber,iif(@IsFalseId = 0, @Codigo, null),0,
					null,@CodigoEspecialidad,iif(@InvoiceId > 0, 0, 1),0,0,0,iif(@InvoiceId > 0,@CareGroupIdInvoice,@careGroupId),
					iif(@InvoiceId > 0,@HealthAdministratorIdInvoice,@healthAdministratorId),@InvoiceNumber,@InvoiceId, @CUPSEntityContractDescriptionId , 
					@fechaRegistro )

				set @codconcec = scope_identity()

				if @IsFalseId = 0 begin
					insert into .ADCONCOED (CODCONCEC,NUMCONCIT)
					values(@codconcec,iif(@IsFalseId = 0, @Codigo, null))
				end
			END

			IF @TypeOfScheduleActivity <> 5 BEGIN
			insert into .AMBORDPAT (IPCODPACI,NUMINGRES,CODCENATE,UFUCODIGO,CODPROSAL,FECORDMED,CODSERIPS,
				CANSERIPS,ESTSERIPS,INDAUDFOR,ESTALEPAT,IPFECHACO,NUMCONCIT,GENCAREGROUP,GENCONENTITY,GENINVOICE,GENINVOICEID, IDDESCRIPCIONRELACIONADA)
			values (@CodigoPaciente,@admissionnumber,@CodigoCentroAtencion,@CodigoUnidadFuncional,@CodigoProfesional,
				@FechaCita,@CodigoServicio,iif(@CantidadServicio > 0, @CantidadServicio, 1),1,0,0,@FechaCita,
				iif(@IsFalseId = 0,@Codigo,null),iif(@InvoiceId > 0,@CareGroupIdInvoice,@careGroupId),
				iif(@InvoiceId > 0,@HealthAdministratorIdInvoice,@healthAdministratorId),@InvoiceNumber,@InvoiceId, @CUPSEntityContractDescriptionId)
				END
			set @generatedId = scope_identity()
		end
		else if @itemType = 3 begin
			declare @INTPACSAC bit, @CODSERIPSInteser varchar(20) = ''

			select @INTPACSAC = INTPACSAC from .HCPARPACS where CODCENATE = @CodigoCentroAtencion
			if @INTPACSAC = 1 begin
				select @CODSERIPSInteser = CODSERIPS from .HCINTESER where CODSERIPS = @CodigoServicio And CODCENATE = @CodigoCentroAtencion
			end
			
			IF @flagInsertADCONCOEX =1 BEGIN
				insert into .ADCONCOEX(IPCODPACI,IPFECHACO,IPFECHCIT,CONESTADO,IPNOMCOMP,CODENTIDA,CODCENATE,UFUCODIGO,
				CODTIPCON,CODPROSAL,NUMINGRES,NUMCONCIT,INDAUDFOR,AUTESTADO,CODESPECI,LIQUIDAR,PRIMERLLA,SEGUNDLLA,TERCERLLA,
				GENCAREGROUP,GENCONENTITY,GENINVOICE,GENINVOICEID, IDDESCRIPCIONRELACIONADA, FECREGSIS )
				values(@CodigoPaciente,@FechaCita,@FechaCita,1,@NombreCompletoPaciente,ltrim(rtrim(@codentida)),@CodigoCentroAtencion,
					@CodigoUnidadFuncional,@TipoCita,@CodigoProfesional,@admissionnumber,iif(@IsFalseId = 0, @Codigo, null),0,
					null,@CodigoEspecialidad,iif(@InvoiceId > 0, 0, 1),0,0,0,iif(@InvoiceId > 0,@CareGroupIdInvoice,@careGroupId),
					iif(@InvoiceId > 0,@HealthAdministratorIdInvoice,@healthAdministratorId),@InvoiceNumber,@InvoiceId, @CUPSEntityContractDescriptionId, 
					@fechaRegistro )

				set @codconcec = scope_identity()

				if @IsFalseId = 0 begin
					insert into .ADCONCOED (CODCONCEC,NUMCONCIT)
					values(@codconcec,iif(@IsFalseId = 0, @Codigo, null))
				end
			END
		
		IF @TypeOfScheduleActivity <> 5 BEGIN
			insert into .AMBORDIMA (IPCODPACI,NUMINGRES,CODCENATE,UFUCODIGO,CODPROSAL,FECORDMED,CODSERIPS,CANSERIPS,
				ESTSERIPS,INDAUDFOR,SERREAINT,SERTRANSC,SERVALMED,ESTALEIMG,REALINOTIF,IPFECHACO,NUMCONCIT,GENCAREGROUP,
				GENCONENTITY,GENINVOICE,GENINVOICEID, TIENEGRABACION, IDDESCRIPCIONRELACIONADA)
			values (@CodigoPaciente,@admissionnumber,@CodigoCentroAtencion,@CodigoUnidadFuncional,@CodigoProfesional,
				@FechaCita,@CodigoServicio,iif(@CantidadServicio > 0, @CantidadServicio, 1),1,0,iif(@INTPACSAC = 1 And @CODSERIPSInteser <> '', 1, 0),
				0,0,0,0,@FechaCita,iif(@IsFalseId = 0,@Codigo,null),iif(@InvoiceId > 0,@CareGroupIdInvoice,@careGroupId),
				iif(@InvoiceId > 0,@HealthAdministratorIdInvoice,@healthAdministratorId),@InvoiceNumber,@InvoiceId, 0, @CUPSEntityContractDescriptionId)
				END
			set @generatedId = scope_identity()
		end
		else if @itemType = 4 begin
			--select @codconcec = CODCONCEC from .ADCONCOEX where NUMINGRES = @admissionnumber And IPCODPACI = @CodigoPaciente And CONESTADO = 1
			--if @codconcec > 0 begin
			--	select '999' as CodeResult, 'Ya existe un registro de consulta externa para el ingreso (' + 
			--		@admissionnumber + ') y paciente (' + @CodigoPaciente + ')' as MessageResult, 0 as GenerateId
			--	return
			--end
			if exists( SELECT 1 
						from AGASICITA A WITH(NOLOCK)
						JOIN AGACTIMED D with(nolock) ON A.CODACTMED=D.CODACTMED	
						JOIN INCUPSIPS F with(nolock) ON (CASE isnull(A.CODTIPCIT,'9') WHEN '0' THEN ISNULL(A.CODSERIPS,D.CODSERIPS) WHEN '1' THEN ISNULL(A.CODSERIPS, iif(D.CODSERIPSCONTROL is null or D.CODSERIPSCONTROL ='0',ISNULL(D.CODSERIPS, A.CODSERIPS),D.CODSERIPSCONTROL)) WHEN '9' THEN A.CODSERIPS ELSE D.CODSERIPS END =F.CODSERIPS) and  f.ISPANEL =1
						JOIN Contract.CUPSEntity cups with(NOLOCK) on f.CODSERIPS =cups.Code and cups.IsPanel=1
						where A.CODAUTONU = @Codigo and A.IPCODPACI = @CodigoPaciente) BEGIN

				INSERT INTO AGASICITA (	CODESPECI --1
									,CODCENATE --2
									,IPCODPACI --3
									,CODPROSAL --4
									,FECHORAIN --5
									,FECHORAFI --6
									,CODACTMED --7
									,CODTIPSOL --8
									,CODESTCIT --9
									,CITAEXTRA --10
									,OBSERVACI --11
									,ESTENVSMS --12
									,CODUSUASI  --13
									,FECREGSIS --14
									,FECITADES --15
									,TIPSOLICITU --16
									,IDSALA --17
									,GENGENERATESO  --18
									,CODSERIPS --19
									,RELCITAINGRE  --20
									,NUMINGRES --21
									,CODDIAGNO  --22
									,CONFASIST --23
									,GENCAREGROUP --24
									,GENCONENTITY  --25
									,IDDESCRIPCIONRELACIONADA --26
									,MODALIDAD --27
									,CODIGOCON --28
									) 
				SELECT
						A.CODESPECI, --1
						A.CODCENATE, --2
						A.IPCODPACI, --3
						A.CODPROSAL,  --4
						A.FECHORAIN, --5
						A.FECHORAFI, --6
						A.CODACTMED, --7
						A.CODTIPSOL, --8 (presencial)
						A.CODESTCIT, --9
						A.CITAEXTRA,  --10
						A.OBSERVACI, --11
						A.ESTENVSMS, --12
						A.CODUSUASI, --13
						A.FECREGSIS, --14
						A.FECITADES, --15
						A.TIPSOLICITU, --16
						A.IDSALA, --17
						A.GENGENERATESO, --18
						@CodigoServicio,  --19
						A.RELCITAINGRE, --20
						A.NUMINGRES, --21
						A.CODDIAGNO, --22
						A.CONFASIST, --23
						A.GENCAREGROUP, --24
						A.GENCONENTITY, --25
						@CUPSEntityContractDescriptionId, --26
						A.MODALIDAD, --27
						A.CODIGOCON
					FROM AGASICITA A WITH(NOLOCK)
					where A.CODAUTONU = @Codigo and A.IPCODPACI = @CodigoPaciente

					SET @Codigo = SCOPE_IDENTITY()
				END

				insert into .ADCONCOEX(IPCODPACI,IPFECHACO,IPFECHCIT,CONESTADO,IPNOMCOMP,CODENTIDA,CODCENATE,UFUCODIGO,
				CODTIPCON,CODPROSAL,NUMINGRES,NUMCONCIT,INDAUDFOR,AUTESTADO,CODESPECI,LIQUIDAR,PRIMERLLA,SEGUNDLLA,TERCERLLA,
				GENCAREGROUP,GENCONENTITY,GENINVOICE,GENINVOICEID, IDDESCRIPCIONRELACIONADA, FECREGSIS )
				values(@CodigoPaciente,@FechaCita,@FechaCita,1,@NombreCompletoPaciente,ltrim(rtrim(@codentida)),@CodigoCentroAtencion,
					@CodigoUnidadFuncional,@TipoCita,@CodigoProfesional,@admissionnumber,iif(@IsFalseId = 0, @Codigo, null),0,
					null,@CodigoEspecialidad,iif(@InvoiceId > 0, 0, 1),0,0,0,iif(@InvoiceId > 0,@CareGroupIdInvoice,@careGroupId),
					iif(@InvoiceId > 0,@HealthAdministratorIdInvoice,@healthAdministratorId),@InvoiceNumber,@InvoiceId, @CUPSEntityContractDescriptionId, 
					@fechaRegistro )

				set @generatedId = scope_identity()

				if @IsFalseId = 0 begin
					insert into .ADCONCOED (CODCONCEC,NUMCONCIT)
					values(@generatedId,iif(@IsFalseId = 0, @Codigo, null))
				end
			
		end
		else if @itemType in (5, 6, 7, 8, 9, 10,11, 12) begin
			--5. Quimioterapias, 6. Radioterapias, 7. Diálisis, 8. Ninguno, 9. Procedimiento no Qx, 10. Procedimiento Qx, , 12. Otros Procedimientos
			--select @codconcec = CODCONCEC from .ADCONCOEX where NUMINGRES = @admissionnumber And IPCODPACI = @CodigoPaciente And CONESTADO = 1
			--if @codconcec > 0 begin
			--	select '999' as CodeResult, 'Ya existe un registro en ADCONCOEX para el ingreso (' + 
			--		@admissionnumber + ') y paciente (' + @CodigoPaciente + ')' as MessageResult, 0 as GenerateId
			--	return
			--end
			if @flagInsertADCONCOEX = 1 OR @itemType = 11 BEGIN
				
				insert into .ADCONCOEX(IPCODPACI,IPFECHACO,IPFECHCIT,CONESTADO,IPNOMCOMP,CODENTIDA,CODCENATE,UFUCODIGO,
				CODTIPCON,CODPROSAL,NUMINGRES,NUMCONCIT,INDAUDFOR,AUTESTADO,CODESPECI,LIQUIDAR,PRIMERLLA,SEGUNDLLA,TERCERLLA,
				GENCAREGROUP,GENCONENTITY,GENINVOICE,GENINVOICEID, IDDESCRIPCIONRELACIONADA, FECREGSIS )
				values(@CodigoPaciente,@FechaCita,@FechaCita,1,@NombreCompletoPaciente,ltrim(rtrim(@codentida)),@CodigoCentroAtencion,
					@CodigoUnidadFuncional,@TipoCita,@CodigoProfesional,@admissionnumber,iif(@IsFalseId = 0, @Codigo, null),0,
					null,@CodigoEspecialidad,iif(@InvoiceId > 0, 0, 1),0,0,0,iif(@InvoiceId > 0,@CareGroupIdInvoice,@careGroupId),
					iif(@InvoiceId > 0,@HealthAdministratorIdInvoice,@healthAdministratorId),@InvoiceNumber,@InvoiceId, @CUPSEntityContractDescriptionId,
					@fechaRegistro )

				set @generatedId = scope_identity()
			END
			ELSE BEGIN 
				set @generatedId =NULL
			END	
		
		end

		select '0' as CodeResult, '' as MessageResult, @generatedId as GenerateId
	end try
	begin catch
		select '999' as CodeResult, error_message() as MessageResult, 0 as GenerateId
	end catch

END