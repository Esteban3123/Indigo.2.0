
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
	@TypeOfScheduleActivity int, --Valida el tipo de actividad de agendamiento, si es mixta(5) solo inserta en ADCONCOEX
	@ExternalConsultationId numeric(18,0) = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	SET @ExternalConsultationId = NULL;
	
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
			/*
				El clon de cita panel ya no se realiza en SQL owner. Si aplica,
					ERP emite su creacion por outbox. El llamador puede dejar NUMCONCIT
					pendiente y correlacionar la confirmacion mediante ExternalConsultationId.
			*/

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
					-- Existing callers keep the original detail relation.
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

		SET @ExternalConsultationId = CASE WHEN @itemType = 4 OR @itemType BETWEEN 5 AND 12
			THEN @generatedId ELSE @codconcec END;
		select '0' as CodeResult, '' as MessageResult, @generatedId as GenerateId
	end try
	begin catch
		select '999' as CodeResult, error_message() as MessageResult, 0 as GenerateId
	end catch

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de facturación que genera los ítems de un documento de cobro (factura o cuenta de cobro) según el tipo de servicio prestado: laboratorios (1), patologías (2), imágenes (3) o consulta externa (4). Para cada tipo, registra la cita o consulta en la tabla de citas externas (ADCONCOEX) y luego inserta la orden del servicio correspondiente (órdenes de laboratorio, patología, imágenes o consulta externa), vinculando el ítem al paciente, ingreso, profesional de salud, centro de atención, unidad funcional, entidad contratante y número de factura. Controla casos especiales como actividades de agendamiento de tipo mixto (TypeOfScheduleActivity = 5), consultas pendientes previas del paciente y servicios con identificadores ficticios, garantizando la trazabilidad entre la orden clínica y la factura generada.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateItemDocument';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateItemDocument';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera los registros de orden/cita asociados a un ítem facturable (laboratorio, patología, imagen, consulta externa u otros procedimientos) creando la consulta en ADCONCOEX y la orden específica según el tipo de servicio.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateItemDocument';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Para itemType=4 (consulta externa), si el flujo requiere cita panel/derivada, @Codigo debe venir resuelto previamente por ERP_Services_Core mediante Scheduling API.; El AdmissionNumber efectivo se determina como AdmissionNumberInvoice si InvoiceId>0; en caso contrario se usa numIngres.; Para itemType=3, se consulta HCPARPACS por centro de atención para determinar la integración PACS (INTPACSAC).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateItemDocument';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Billing.ADCONCOEX: Inserta una consulta externa con CONESTADO=1 cuando @flagInsertADCONCOEX=1 (no hay otra ADCONCOEX activa para el paciente) o cuando el TypeOfScheduleActivity=5 (mixta); LIQUIDAR=0 si InvoiceId>0, sino 1; CareGroup/HealthAdministrator se toman de los parámetros *Invoice cuando InvoiceId>0.; [INSERT] Billing.ADCONCOED: Cuando @IsFalseId=0 y se insertó previamente en ADCONCOEX, se inserta el vínculo (CODCONCEC=scope_identity, NUMCONCIT=@Codigo).; [INSERT] Billing.AMBORDLAB: Cuando @itemType=1 y @TypeOfScheduleActivity<>5 inserta orden de laboratorio con ESTSERIPS=1, CANSERIPS=@CantidadServicio (mínimo 1) y NUMCONCIT=@Codigo si no es FalseId.; [INSERT] Billing.AMBORDPAT: Cuando @itemType=2 y @TypeOfScheduleActivity<>5 inserta orden de patología con ESTSERIPS=1 y CANSERIPS=@CantidadServicio (mínimo 1).; [INSERT] Billing.AMBORDIMA: Cuando @itemType=3 y @TypeOfScheduleActivity<>5 inserta orden de imagen; SERREAINT=1 sólo si HCPARPACS.INTPACSAC=1 y existe el servicio en HCINTESER para ese centro, en caso contrario 0.; [RETURN_RESULT] RESULTSET: Devuelve CodeResult=''0'', MessageResult='''' y GenerateId=@generatedId en éxito; en CATCH devuelve CodeResult=''999'', MessageResult=error_message() y GenerateId=0.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateItemDocument';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si EXISTS ADCONCOEX activa (CONESTADO=1) para @CodigoPaciente y @TypeOfScheduleActivity<>5 → Se baja @flagInsertADCONCOEX=0 para evitar crear otra consulta externa duplicada (excepto para itemType=11 donde se fuerza la inserción). else @flagInsertADCONCOEX=1 y se permitirá insertar en ADCONCOEX.; si @itemType = 1 / 2 / 3 → Inserta en ADCONCOEX (si flag) y en la tabla de órdenes correspondiente (AMBORDLAB/AMBORDPAT/AMBORDIMA) cuando TypeOfScheduleActivity<>5.; si @itemType = 3 y HCPARPACS.INTPACSAC = 1 → Valida en HCINTESER si el servicio está habilitado para integración PACS y marca SERREAINT=1 en AMBORDIMA. else SERREAINT=0.; si @itemType = 4 → Inserta ADCONCOEX/ADCONCOED usando @Codigo como AppointmentId final resuelto por Scheduling API.; si @itemType IN (5,6,7,8,9,10,11,12) → Inserta en ADCONCOEX si @flagInsertADCONCOEX=1 o si @itemType=11 (radioterapia siempre se inserta). else @generatedId=NULL (no se inserta nada).; si @IsFalseId = 0 → Se persiste el vínculo en ADCONCOED entre la nueva consulta y el código externo @Codigo. else No se inserta en ADCONCOED y NUMCONCIT queda en NULL.; si @InvoiceId > 0 → Se usan CareGroupIdInvoice/HealthAdministratorIdInvoice y LIQUIDAR=0 (la consulta queda ya facturada). else Se usan careGroupId/healthAdministratorId y LIQUIDAR=1 (pendiente de liquidar).; si @CantidadServicio > 0 → Se usa la cantidad indicada en CANSERIPS. else Se asume CANSERIPS=1.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateItemDocument';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateItemDocument';
-- GO
