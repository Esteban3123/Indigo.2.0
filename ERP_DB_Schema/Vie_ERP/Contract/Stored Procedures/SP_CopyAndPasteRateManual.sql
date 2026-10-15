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