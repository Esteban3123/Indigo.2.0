-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 03/12/2015
-- Description:	Procedimiento que se encarga de validar el copyPaste de la rejilla de Cruce Anticipo vs CxC
-- =============================================
CREATE PROCEDURE [Contract].[SP_CopyAndPasteRateManual] 
	@XmlObject as Xml,
	@ServiceManual as int,
	@GridOption as int
AS
BEGIN
	
	--@GridOption = 0(Rejilla sin grupo quirúrgico), 1(Rejilla con grupo quirúrgico)

	--Tabla para almacenar los items del listado que viene en el xml y poder guardar las homologaciones de cuenta
	declare @TableXmlObject table(Id int IDENTITY PRIMARY KEY,CountFields int, StatusField int, 
								MessageField varchar(max), IPSServiceCode varchar(20), IPSServiceDescription varchar(max), IPSServiceId int,
								SalesValue varchar(18), SalesValueWithSurcharge varchar(18), 
								SurgicalGroupDescription varchar(max), SurgicalGroupId int, SurgicalGroupCode varchar(20),
								UVRRangeDescription varchar(max), UVRRangeId int, UVRRangeCode varchar(20))

	--begin transaction
	Begin try
	
		insert into @TableXmlObject
		select 
		t.x.value('CountFields[1]','int') as CountFields,
		t.x.value('StatusField[1]','int') as StatusField,
		t.x.value('MessageField[1]','varchar(100)') as MessageField,
		t.x.value('IPSServiceCode[1]','varchar(20)') as IPSServiceCode,
		t.x.value('IPSServiceDescription[1]','varchar(max)') as IPSServiceDescription,
		t.x.value('IPSServiceId[1]','int') as IPSServiceId,
		t.x.value('SalesValue[1]','varchar(18)') as SalesValue,
		t.x.value('SalesValueWithSurcharge[1]','varchar(18)') as SalesValueWithSurcharge,
		t.x.value('SurgicalGroupDescription[1]','varchar(max)') as SurgicalGroupDescription,
		t.x.value('SurgicalGroupId[1]','int') as SurgicalGroupId,
		t.x.value('SurgicalGroupCode[1]','varchar(20)') as SurgicalGroupCode,
		t.x.value('UVRRangeDescription[1]','varchar(max)') as UVRRangeDescription,
		t.x.value('UVRRangeId[1]','int') as UVRRangeId,
		t.x.value('UVRRangeCode[1]','varchar(20)') as UVRRangeCode
		from @XmlObject.nodes('/Data/Row') t(x)

		--Se declara el contador de posiciones para enviar en los mensajes de error
		Declare @Position as int = 0
		--Se declara la variable para poder realizar las validaciones
		Declare @Count as int

		--Se declara un cursor y las variables que lleva el cursor
		Declare @CountFields as int
		Declare @StatusField as int
		Declare @MessageField as varchar(100)
		Declare @Id as int
		Declare @IPSServiceCode as varchar(20)
		Declare @IPSServiceDescription as varchar(max)
		Declare @IPSServiceId as int
		Declare @SalesValue as varchar(18)
		Declare @SalesValueWithSurcharge as varchar(18)
		Declare @SurgicalGroupDescription as varchar(max)
		Declare @SurgicalGroupId as int
		Declare @SurgicalGroupCode as varchar(20)
		Declare @UVRRangeDescription as varchar(max)
		Declare @UVRRangeId as int
		Declare @UVRRangeCode as varchar(20)
		Declare InfoItem Cursor For Select [CountFields], [StatusField], [MessageField], 
										   [Id], [IPSServiceCode], [IPSServiceDescription], [IPSServiceId], 
										   [SalesValue], [SalesValueWithSurcharge], 
										   [SurgicalGroupDescription], [SurgicalGroupId], [SurgicalGroupCode],
										   [UVRRangeDescription], [UVRRangeId], [UVRRangeCode] From @TableXmlObject

		Open InfoItem

		Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, @IPSServiceCode, @IPSServiceDescription, @IPSServiceId,
									  @SalesValue, @SalesValueWithSurcharge, @SurgicalGroupDescription, @SurgicalGroupId, @SurgicalGroupCode,
									  @UVRRangeDescription, @UVRRangeId, @UVRRangeCode

		While @@fetch_status = 0
		Begin
		
			--Se incrementa la posicion
			set @Position = @Position + 1
			
			--Se valida que cada registro tenga la estructura requerida dependiendo de la rejilla sin grupo = 3, con grupo = 4
			if @GridOption = 0 --Rejilla sin grupo
			Begin			
				if @CountFields <> 3
				Begin
					--Se actualiza los campos con el estado en false y el mensaje de error
					update @TableXmlObject set StatusField = 0, MessageField = 'El registro ' + convert(varchar(3),@Position) + ' no tiene la estructura requerida'
					where Id = @Id			
					--Se pasa a la siguiente posicion del cursor
					Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, @IPSServiceCode, @IPSServiceDescription, @IPSServiceId,
										  @SalesValue, @SalesValueWithSurcharge, @SurgicalGroupDescription, @SurgicalGroupId, @SurgicalGroupCode,
										  @UVRRangeDescription, @UVRRangeId, @UVRRangeCode
					continue
				End		
			End
			Else --Rejilla con grupo
			Begin			
				if (@ServiceManual = 4 and @CountFields <> 3) Or (@ServiceManual <> 4 And @CountFields <> 4)
				Begin
				select @CountFields
					--Se actualiza los campos con el estado en false y el mensaje de error
					update @TableXmlObject set StatusField = 0, MessageField = 'El registro ' + convert(varchar(3),@Position) + ' no tiene la estructura requerida'
					where Id = @Id			
					--Se pasa a la siguiente posicion del cursor
					Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, @IPSServiceCode, @IPSServiceDescription, @IPSServiceId,
										  @SalesValue, @SalesValueWithSurcharge, @SurgicalGroupDescription, @SurgicalGroupId, @SurgicalGroupCode,
										  @UVRRangeDescription, @UVRRangeId, @UVRRangeCode
					continue
				End		
			End	

			--Se valida que el código del servicio ips exista dependiendo de la rejilla
			if @GridOption = 0 --Rejilla sin grupo
			Begin
				if (
					select count(1) 
					from [Contract].IPSService 
					where [Status] = 1 
						And (
							@ServiceManual = 4 Or (ServiceClass = 1 or ServiceClass is null)
						) 
						And Presentation <> 2 
						And ServiceManual = @ServiceManual 
						And Code = @IPSServiceCode
					) = 0
				Begin
					--Se actualiza los campos con el estado en false y el mensaje de error
					update @TableXmlObject set StatusField = 0, MessageField = 'El código del servicio ips del registro ' + convert(varchar(3),@Position) + ' no existe'
					where Id = @Id			
					--Se pasa a la siguiente posicion del cursor
					Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, @IPSServiceCode, @IPSServiceDescription, @IPSServiceId,
										  @SalesValue, @SalesValueWithSurcharge, @SurgicalGroupDescription, @SurgicalGroupId, @SurgicalGroupCode,
										  @UVRRangeDescription, @UVRRangeId, @UVRRangeCode
					continue
				End
			End
			Else --Rejilla con grupo
			Begin
				--Depende del tipo de manual
				if @ServiceManual = 1 or @ServiceManual = 2
				Begin
					if (
						select count(1) 
						from [Contract].IPSService 
						where [Status] = 1 
							and ServiceClass > 1 
							and Presentation = 1 
							and ServiceManual = @ServiceManual 
							and ServiceClass <> 2 and ServiceClass <> 3 and ServiceClass <> 4 and Code = @IPSServiceCode
					) = 0
					Begin
						--Se actualiza los campos con el estado en false y el mensaje de error
						update @TableXmlObject set StatusField = 0, MessageField = 'El código del servicio ips del registro ' + convert(varchar(3),@Position) + ' no existe'
						where Id = @Id			
						--Se pasa a la siguiente posicion del cursor
						Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, @IPSServiceCode, @IPSServiceDescription, @IPSServiceId,
											  @SalesValue, @SalesValueWithSurcharge, @SurgicalGroupDescription, @SurgicalGroupId, @SurgicalGroupCode,
											  @UVRRangeDescription, @UVRRangeId, @UVRRangeCode
						continue
					End
				End
				Else
				Begin
					if (
						select count(*) 
						from [Contract].IPSService 
						where [Status] = 1 
							and ServiceClass > 1 
							and Presentation = 1 
							and ServiceManual = @ServiceManual 
							and Code = @IPSServiceCode
					) = 0
					Begin
						--Se actualiza los campos con el estado en false y el mensaje de error
						update @TableXmlObject set StatusField = 0, MessageField = 'El código del servicio ips del registro ' + convert(varchar(3),@Position) + ' no existe'
						where Id = @Id			
						--Se pasa a la siguiente posicion del cursor
						Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, @IPSServiceCode, @IPSServiceDescription, @IPSServiceId,
											  @SalesValue, @SalesValueWithSurcharge, @SurgicalGroupDescription, @SurgicalGroupId, @SurgicalGroupCode,
											  @UVRRangeDescription, @UVRRangeId, @UVRRangeCode
						continue
					End
				End
			End		
			
			--Valido si el grupo existe siempre y cuando sea la rejilla con grupo
			if @GridOption = 1 --Rejilla con grupo
			Begin
				--Se valida si el tipo de manual es ISS2001 o ISS204 se pida el uvr, si es SOAT se pide el grupo
				if @ServiceManual = 1 or @ServiceManual = 2 --Si es 1 o 2 se valida el uvr
				Begin
					
					if (select COUNT(*) from Contract.UVRRange where Code = @UVRRangeCode) = 0
					Begin
						--Se actualiza los campos con el estado en false y el mensaje de error
						update @TableXmlObject set StatusField = 0, MessageField = 'El código del rango UVR del registro ' + convert(varchar(3),@Position) + ' no existe'
						where Id = @Id			
						--Se pasa a la siguiente posicion del cursor
						Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, @IPSServiceCode, @IPSServiceDescription, @IPSServiceId,
												@SalesValue, @SalesValueWithSurcharge, @SurgicalGroupDescription, @SurgicalGroupId, @SurgicalGroupCode,
												@UVRRangeDescription, @UVRRangeId, @UVRRangeCode
						continue
					End
					
					--Se actualizan los campos del rango uvr
					select @UVRRangeId = Id, @UVRRangeDescription = CONCAT(Code, ' - ', Name) 
					from Contract.UVRRange
					where Code = @UVRRangeCode
					
				End
				Else If @ServiceManual <> 4 --Si es 3 se valida el grupo
				Begin
					if (select count(*) from Contract.SurgicalGroup where Code = @SurgicalGroupCode) = 0
					Begin
						--Se actualiza los campos con el estado en false y el mensaje de error
						update @TableXmlObject set StatusField = 0, MessageField = 'El código del grupo quirúrgico del registro ' + convert(varchar(3),@Position) + ' no existe'
						where Id = @Id			
						--Se pasa a la siguiente posicion del cursor
						Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, @IPSServiceCode, @IPSServiceDescription, @IPSServiceId,
												@SalesValue, @SalesValueWithSurcharge, @SurgicalGroupDescription, @SurgicalGroupId, @SurgicalGroupCode,
												@UVRRangeDescription, @UVRRangeId, @UVRRangeCode
						continue
					End

					--Se actualizan los campos del grupo
					select @SurgicalGroupId = Id, @SurgicalGroupDescription = CONCAT(Code, ' - ', Name) 
					from Contract.SurgicalGroup 
					where Code = @SurgicalGroupCode
				End

			End

			--Se valida que el valor servicio no este vacia
			if LEN(@SalesValue) = 0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El valor del servicio del registro ' + convert(varchar(3),@Position) + ' no tiene valor'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, @IPSServiceCode, @IPSServiceDescription, @IPSServiceId,
									  @SalesValue, @SalesValueWithSurcharge, @SurgicalGroupDescription, @SurgicalGroupId, @SurgicalGroupCode,
									  @UVRRangeDescription, @UVRRangeId, @UVRRangeCode
				continue
			End

			--Se valida que el valor servicio sea numérico
			if ISNUMERIC(@SalesValue) <> 1
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El valor del servicio del registro ' + convert(varchar(3),@Position) + ' no es un valor numérico'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, @IPSServiceCode, @IPSServiceDescription, @IPSServiceId,
									  @SalesValue, @SalesValueWithSurcharge, @SurgicalGroupDescription, @SurgicalGroupId, @SurgicalGroupCode,
									  @UVRRangeDescription, @UVRRangeId, @UVRRangeCode
				continue
			End

			--Se valida que el valor recargo no este vacia
			if LEN(@SalesValueWithSurcharge) = 0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El valor del recargo del registro ' + convert(varchar(3),@Position) + ' no tiene valor'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, @IPSServiceCode, @IPSServiceDescription, @IPSServiceId,
									  @SalesValue, @SalesValueWithSurcharge, @SurgicalGroupDescription, @SurgicalGroupId, @SurgicalGroupCode,
									  @UVRRangeDescription, @UVRRangeId, @UVRRangeCode
				continue
			End

			--Se valida que el valor servicio sea numérico
			if ISNUMERIC(@SalesValueWithSurcharge) <> 1
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El valor del recargo del registro ' + convert(varchar(3),@Position) + ' no es un valor numérico'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, @IPSServiceCode, @IPSServiceDescription, @IPSServiceId,
									  @SalesValue, @SalesValueWithSurcharge, @SurgicalGroupDescription, @SurgicalGroupId, @SurgicalGroupCode,
									  @UVRRangeDescription, @UVRRangeId, @UVRRangeCode
				continue
			End

			--Se consulta la tabla IPSService para obtener los datos necesarios
			select @IPSServiceId = Id, @IPSServiceDescription = CONCAT(Code, ' - ', Name)
			from Contract.IPSService 
			where Code = @IPSServiceCode

			--Se actualiza los campos con que se necesitan para armar el objeto en el formulario
			update @TableXmlObject set StatusField = 1, IPSServiceDescription = @IPSServiceDescription, 
			IPSServiceId = @IPSServiceId, SurgicalGroupDescription = @SurgicalGroupDescription, SurgicalGroupId = @SurgicalGroupId,
			UVRRangeDescription = @UVRRangeDescription, UVRRangeId = @UVRRangeId
			where Id = @Id	
			
			--Se pasa a la siguiente posicion del cursor
			Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, @IPSServiceCode, @IPSServiceDescription, @IPSServiceId,
									  @SalesValue, @SalesValueWithSurcharge, @SurgicalGroupDescription, @SurgicalGroupId, @SurgicalGroupCode,
									  @UVRRangeDescription, @UVRRangeId, @UVRRangeCode
			continue

		End

		Close InfoItem
		Deallocate InfoItem

		
		--Se retorna la tabla
		select * from @TableXmlObject
		
	end try
	begin catch

		--rollback transaction
		select * from @TableXmlObject

	end catch

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que valida y procesa el pegado masivo (copy-paste) de tarifas manuales en la rejilla de contratación de la IPS. Recibe un lote de filas en formato XML, cada una con código de servicio (CUPS/IPS), valor de venta, recargo, grupo quirúrgico y rango UVR, y verifica fila a fila que la estructura sea correcta según el tipo de rejilla (con o sin grupo quirúrgico) y el manual tarifario seleccionado. Valida que los códigos de servicio existan y estén activos en el catálogo de servicios IPS, que los grupos quirúrgicos pertenezcan al contrato y que los rangos UVR sean válidos, marcando cada registro con un estado de éxito o error y un mensaje descriptivo de la falla. Se usa en el módulo de contratos para agilizar la carga masiva de tarifas manuales sin tener que ingresar servicio por servicio.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'PROCEDURE', @level1name = N'SP_CopyAndPasteRateManual';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'PROCEDURE', @level1name = N'SP_CopyAndPasteRateManual';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida fila por fila un pegado masivo (copy/paste) de tarifas manuales contra el catálogo de servicios IPS, grupos quirúrgicos y rangos UVR según el tipo de manual y rejilla, y devuelve el conjunto marcado como válido o con su mensaje de error.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteRateManual';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe respetar la estructura /Data/Row con los nodos esperados (CountFields, StatusField, MessageField, IPSServiceCode, SalesValue, SalesValueWithSurcharge, SurgicalGroupCode, UVRRangeCode, etc.).; GridOption debe ser 0 (rejilla sin grupo quirúrgico) o 1 (rejilla con grupo quirúrgico).; ServiceManual debe corresponder a un tipo de manual válido (1=ISS2001, 2=ISS204, 3=SOAT, 4=otro) coherente con los servicios cargados en Contract.IPSService.; Los catálogos Contract.IPSService, Contract.UVRRange y Contract.SurgicalGroup deben existir y estar poblados para resolver las validaciones.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteRateManual';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada registro del XML queda con StatusField=1 solo si supera todas las validaciones (estructura, existencia del servicio IPS, existencia del grupo/UVR según manual, y valores numéricos no vacíos para servicio y recargo).; Los registros inválidos siempre conservan un MessageField que identifica la posición del registro y la causa del rechazo.; El procedimiento nunca modifica las tablas Contract.IPSService, Contract.UVRRange ni Contract.SurgicalGroup; solo las consulta.; La estructura esperada de columnas depende combinadamente de GridOption y ServiceManual: 3 campos para rejilla sin grupo o para manual=4, 4 campos para rejilla con grupo en otros manuales.; Cuando ServiceManual IN (1,2) y la rejilla tiene grupo, la tarifa se asocia a un rango UVR; cuando ServiceManual NOT IN (1,2,4) se asocia a un grupo quirúrgico.; Si ocurre cualquier excepción se devuelve la tabla en su estado actual sin propagar el error (catch silencioso).', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteRateManual';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Servicio IPS; Manual tarifario (ISS2001, ISS204, SOAT); Grupo quirúrgico; Rango UVR; Valor de venta del servicio; Valor con recargo; Clase de servicio (ServiceClass); Presentación del servicio', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteRateManual';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si GridOption = 0 (rejilla sin grupo quirúrgico) → Se exige estructura con 3 campos y se valida existencia del servicio IPS exigiendo Status=1, Presentation<>2, ServiceClass=1 o NULL (excepto cuando ServiceManual=4 que omite el filtro de ServiceClass) y ServiceManual coincidente. else GridOption = 1: rejilla con grupo quirúrgico, se aplican validaciones de estructura y de grupo/UVR según el tipo de manual.; si GridOption = 1 y ServiceManual = 4 → Se exige estructura con 3 campos. else GridOption = 1 y ServiceManual <> 4: se exige estructura con 4 campos.; si GridOption = 1 y ServiceManual IN (1,2) (manuales ISS2001/ISS204) → Se valida el servicio IPS con Status=1, ServiceClass>1, Presentation=1, ServiceManual coincidente y excluyendo ServiceClass IN (2,3,4); además se exige que exista el código de rango UVR en Contract.UVRRange y se cargan Id y descripción del UVR. else Para otros valores de ServiceManual la validación del servicio IPS no excluye ServiceClass 2/3/4.; si GridOption = 1 y ServiceManual NOT IN (1,2,4) → Se valida la existencia del código de grupo quirúrgico en Contract.SurgicalGroup y se cargan Id y descripción del grupo.; si Por cada registro del XML, si CountFields no coincide con la estructura esperada → Marca StatusField=0 con mensaje ''no tiene la estructura requerida'' y omite las demás validaciones de ese registro. else Continúa con validaciones de servicio IPS, grupo/UVR y valores.; si LEN(SalesValue)=0, ISNUMERIC(SalesValue)<>1, LEN(SalesValueWithSurcharge)=0 o ISNUMERIC(SalesValueWithSurcharge)<>1 → Marca StatusField=0 con el mensaje correspondiente (''no tiene valor'' o ''no es un valor numérico'') y pasa al siguiente registro. else El registro se considera válido y se actualiza StatusField=1 con la descripción e Id del servicio IPS y, si aplica, del grupo quirúrgico o rango UVR.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteRateManual';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Contract.IPSService; Contract.UVRRange; Contract.SurgicalGroup', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteRateManual';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteRateManual';
-- GO
