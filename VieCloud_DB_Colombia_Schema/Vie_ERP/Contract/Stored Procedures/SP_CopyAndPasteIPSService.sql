

-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 03/12/2015
-- Description:	Procedimiento que se encarga de validar el copyPaste de la rejilla de Cruce Anticipo vs CxC
-- =============================================
CREATE PROCEDURE [Contract].[SP_CopyAndPasteIPSService] 
	@XmlObject as Xml,
	@ServiceManual as int
AS
BEGIN
	
	--Tabla para almacenar los items del listado que viene en el xml y poder guardar las homologaciones de cuenta
	declare @TableXmlObject table(Id int IDENTITY PRIMARY KEY,CountFields int, StatusField int, 
								MessageField varchar(max), IPSServiceCode varchar(20), IPSServiceDescription varchar(max), IPSServiceId int,
								Class tinyint, DefaultService bit, ServiceAmount varchar(18))

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
		t.x.value('Class[1]','tinyint') as Class,
		t.x.value('DefaultService[1]','bit') as DefaultService,
		t.x.value('ServiceAmount[1]','varchar(18)') as ServiceAmount
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
		Declare @Class as tinyint
		Declare @DefaultService as bit
		Declare @ServiceAmount as varchar(18)
		Declare InfoItem Cursor For Select [CountFields], [StatusField], [MessageField], 
										   [Id], [IPSServiceCode], [IPSServiceDescription], [IPSServiceId], 
										   [Class], [DefaultService], [ServiceAmount] From @TableXmlObject

		Open InfoItem

		Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, @IPSServiceCode, @IPSServiceDescription, @IPSServiceId,
									  @Class, @DefaultService, @ServiceAmount

		While @@fetch_status = 0
		Begin
		
			--Se incrementa la posicion
			set @Position = @Position + 1
			
			--Se asigna el defaultService
			if @Position = 1
			Begin
				set @DefaultService = 1
			End
			Else
			Begin
				set @DefaultService = 0
			End

			--Se valida que cada registro tenga la estructura requerida
			if @CountFields <> 2
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El registro ' + convert(varchar(3),@Position) + ' no tiene la estructura requerida'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, @IPSServiceCode, @IPSServiceDescription, @IPSServiceId,
									  @Class, @DefaultService, @ServiceAmount
				continue
			End		

			--Se valida que el código del servicio ips exista
			if (select count(*) 
			from Contract.IPSService 
			where Status = 1 and Presentation = 1 and ServiceManual = @ServiceManual and 
			ServiceClass <> 1 and ServiceClass <> 6 and Code = @IPSServiceCode) = 0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El código del servicio ips del registro ' + convert(varchar(3),@Position) + ' no existe'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, @IPSServiceCode, @IPSServiceDescription, @IPSServiceId,
									  @Class, @DefaultService, @ServiceAmount
				continue
			End
			
			--Se valida que la cantidad(ServiceAmount) no este vacia
			if LEN(@ServiceAmount) = 0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'La cantidad del registro ' + convert(varchar(3),@Position) + ' no tiene valor'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, @IPSServiceCode, @IPSServiceDescription, @IPSServiceId,
									  @Class, @DefaultService, @ServiceAmount
				continue
			End

			--Se valida que la cantidad(ServiceAmount) sea numérico
			if ISNUMERIC(@ServiceAmount) <> 1
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'La cantidad del registro ' + convert(varchar(3),@Position) + ' no es un valor numérico'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, @IPSServiceCode, @IPSServiceDescription, @IPSServiceId,
									  @Class, @DefaultService, @ServiceAmount
				continue
			End

			--Se consulta la tabla IPSService para obtener los datos necesarios
			select @IPSServiceId = Id, @IPSServiceDescription = CONCAT(Code, ' - ', Name), @Class = ServiceClass
			from Contract.IPSService 
			where Status = 1 and Presentation = 1 and ServiceManual = @ServiceManual and 
			ServiceClass <> 1 and ServiceClass <> 6 and Code = @IPSServiceCode

			 --Se actualiza los campos con que se necesitan para armar el objeto en el formulario
			update @TableXmlObject set StatusField = 1, IPSServiceDescription = @IPSServiceDescription, 
			IPSServiceId = @IPSServiceId, Class = @Class, DefaultService = @DefaultService
			where Id = @Id	
			
			--Se pasa a la siguiente posicion del cursor
			Fetch Next From InfoItem Into @CountFields, @StatusField, @MessageField, @Id, @IPSServiceCode, @IPSServiceDescription, @IPSServiceId,
									  @Class, @DefaultService, @ServiceAmount
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que valida y procesa un listado de servicios de la IPS enviado en formato XML para permitir la operación de copiar y pegar (copy-paste) en la grilla de servicios contratados. Recibe cada fila del XML con el código del servicio (CUPS/IPS), la cantidad y la clase, y verifica que el código exista en el catálogo maestro de servicios (Contract.IPSService) con estado activo, presentación habilitada y manual de tarifas indicado, excluyendo clases de servicio restringidas. Además valida que la cantidad sea un valor numérico no vacío y que cada registro tenga la estructura requerida (número de campos correcto), marcando cada fila con un estado de éxito o error y su mensaje descriptivo. Devuelve el listado enriquecido con el identificador del servicio, la descripción combinada (código + nombre), la clase y la bandera de servicio predeterminado (primer registro de la lista), listo para construir el objeto en el formulario de contratación.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'PROCEDURE', @level1name = N'SP_CopyAndPasteIPSService';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'PROCEDURE', @level1name = N'SP_CopyAndPasteIPSService';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida y homologa, fila por fila, una lista de servicios IPS recibida en XML (típicamente pegada en una grilla) verificando estructura, existencia en el catálogo y cantidad, y devuelve la lista enriquecida con descripción, identificador y clase del servicio.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteIPSService';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe tener nodos /Data/Row con los campos esperados (CountFields, StatusField, MessageField, IPSServiceCode, IPSServiceDescription, IPSServiceId, Class, DefaultService, ServiceAmount); Debe proporcionarse un identificador de manual de servicios válido para filtrar el catálogo de servicios IPS', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteIPSService';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran válidos los servicios IPS con Status=1, Presentation=1, del manual indicado y cuya ServiceClass no sea 1 ni 6; El primer registro de la lista siempre queda marcado como servicio por defecto y los demás como no-default; Cada registro debe contar con exactamente 2 campos para considerarse estructuralmente válido; La cantidad del servicio debe ser no vacía y numérica; La descripción del servicio se construye como ''Code - Name''; Ante una excepción, igualmente se devuelve el contenido procesado (sin propagar el error)', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteIPSService';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Servicio IPS; Manual de servicios; Clase de servicio; Servicio por defecto; Cruce Anticipo vs CxC', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteIPSService';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TableXmlObject: Por cada nodo /Data/Row del XML se inserta una fila con los campos parseados; [UPDATE] @TableXmlObject: Cuando CountFields<>2 → StatusField=0 con mensaje de estructura inválida; [UPDATE] @TableXmlObject: Cuando no existe IPSService activo/presentación con ServiceManual dado, ServiceClass<>1 y <>6 y Code coincidente → StatusField=0 con mensaje ''no existe''; [UPDATE] @TableXmlObject: Cuando LEN(ServiceAmount)=0 → StatusField=0 con mensaje ''no tiene valor''; [UPDATE] @TableXmlObject: Cuando ISNUMERIC(ServiceAmount)<>1 → StatusField=0 con mensaje ''no es un valor numérico''; [UPDATE] @TableXmlObject: Cuando el registro es válido → StatusField=1, se asigna IPSServiceId, IPSServiceDescription=CONCAT(Code,'' - '',Name), Class=ServiceClass y DefaultService según posición; [RETURN_RESULT] @TableXmlObject: Al finalizar (o ante catch) se devuelve el contenido completo de la tabla temporal con el resultado de cada registro', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteIPSService';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Es la primera fila procesada (posición = 1) → Marca el registro como servicio por defecto (DefaultService = 1) else Marca el registro con DefaultService = 0; si El conteo de campos del registro no es exactamente 2 → Marca el registro con StatusField=0 y mensaje ''no tiene la estructura requerida'' y omite validaciones posteriores; si No existe en el catálogo un IPSService activo, de presentación, con el ServiceManual indicado, con ServiceClass distinto de 1 y 6, y Code igual al ingresado → Marca el registro con StatusField=0 y mensaje ''el código del servicio ips ... no existe''; si La cantidad (ServiceAmount) viene vacía → Marca el registro con StatusField=0 y mensaje ''la cantidad ... no tiene valor''; si La cantidad (ServiceAmount) no es numérica → Marca el registro con StatusField=0 y mensaje ''la cantidad ... no es un valor numérico''; si El registro pasa todas las validaciones → Completa el registro con Id, descripción (Code - Name), ServiceClass y DefaultService, y marca StatusField=1', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteIPSService';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Contract.IPSService', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteIPSService';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteIPSService';
-- GO
