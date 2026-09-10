

-- ==============================================================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 07/05/2018
-- Description:	Procedimiento que se encarga de guardar en las tablas de integración con HEON
-- ==============================================================================================
CREATE PROCEDURE [Inventory].[SP_SaveIntegration] 
	@Xml as xml,
	@CodeUser as varchar(20)
AS
BEGIN
	
	--Se declaran las variables para obtener los datos de la cabecera
	declare @LogisticOperator int, @OfficeType int, @CareCenterCode varchar(20), @JsonSolicitud varchar(max), @JsonEntrega varchar(max), @JsonRespuestaHEON varchar(max),
	@EntityId int, @EntityCode varchar(20), @EntityName varchar(250), @Status tinyint

	--Tabla temporal para los detalles de la integración
	declare @TempControlIntegrationHeonDetail table(Id int IDENTITY PRIMARY KEY, MedicalOrderRecipe varchar(20), ProductCodeHeon varchar(20), TotalQuantity int, [Status] tinyint, [Message] varchar(max))

	--Tabla temporal que se devuelve y sirve para poder validar
	declare @TableMessage table(CodeMessage int, MessageReturn varchar(max))

	--Id de la cabecera
	declare @HeaderId int

	--begin transaction
	begin try
	
		--Se obtiene los datos de la cabecera
		select 
		@LogisticOperator = t.x.value('LogisticOperator[1]','int'),
		@OfficeType = t.x.value('OfficeType[1]','int'),
		@CareCenterCode = t.x.value('CareCenterCode[1]','varchar(20)'),
		@JsonSolicitud = t.x.value('JsonSolicitud[1]','varchar(max)'),
		@JsonEntrega = t.x.value('JsonEntrega[1]','varchar(max)'),
		@JsonRespuestaHEON = t.x.value('JsonRespuestaHeon[1]','varchar(max)'),
		@EntityId = t.x.value('EntityId[1]','int'),
		@EntityCode = t.x.value('EntityCode[1]','varchar(20)'),
		@EntityName = t.x.value('EntityName[1]','varchar(250)'),
		@Status = t.x.value('Status[1]','tinyint')
		from @Xml.nodes('/Header') t(x)
		
		print '@JsonRespuestaHEON: ' + @JsonRespuestaHEON

		--Se obtiene los datos de los detalles
		insert into @TempControlIntegrationHeonDetail
		select 
		t.x.value('MedicalOrderRecipe[1]','varchar(20)') as MedicalOrderRecipe,
		t.x.value('ProductCodeHeon[1]','varchar(20)') as ProductCodeHeon,
		t.x.value('TotalQuantity[1]','int') as TotalQuantity,
		t.x.value('Status[1]','tinyint') as Status,
		t.x.value('Message[1]','varchar(max)') as Message
		from @Xml.nodes('/Header/Details') t(x)
		
		--Se obtiene el id de la dispensación farmaceutica
		set @EntityId = (select Id from Inventory.PharmaceuticalDispensing where Code = @EntityCode)

		if @JsonRespuestaHEON = ''
		begin

			--Se guarda la cabecera de la tabla de control de integración
			insert into [Inventory].[ControlIntegrationHeon]
			([LogisticOperator], [OfficeType], [CareCenterCode], [JsonSolicitud], [JsonEntrega], [JsonRespuestaHEON], [EntityId], [EntityCode], [EntityName],
			[Status], [ConfirmationDate], [ConfirmationUser])
			values
			(@LogisticOperator, @OfficeType, @CareCenterCode, @JsonSolicitud, @JsonEntrega, @JsonRespuestaHEON, @EntityId, @EntityCode, @EntityName,
			@Status, [Common].[GETDATE](), @CodeUser)

			--Se obtiene el id generado
			set @HeaderId = SCOPE_IDENTITY()

			--Se insertan los detalles
			insert into [Inventory].[ControlIntegrationHeonDetail]
			([ControlIntegrationHeonId], [MedicalOrderRecipe], [ProductCodeHeon], [TotalQuantity], [Status], [Message])
			select @HeaderId, MedicalOrderRecipe, ProductCodeHeon, TotalQuantity, [Status], [Message]
			from @TempControlIntegrationHeonDetail

		end
		else begin

			--Se obtiene el id de la cabecera de la tabla de control
			set @HeaderId = (select Id from Inventory.ControlIntegrationHeon where EntityCode = @EntityCode)

			--Se actualiza el estado y la respuesta de HEON en la tabla de control
			update [Inventory].[ControlIntegrationHeon] set Status = @Status, JsonRespuestaHEON = @JsonRespuestaHEON where Id = @HeaderId

			--Se actualizan los detalles con la respuesta de HEON
			update cihd set cihd.Status = temp.Status, cihd.Message = temp.Message
			from Inventory.ControlIntegrationHeonDetail cihd
			inner join @TempControlIntegrationHeonDetail temp on temp.MedicalOrderRecipe = cihd.MedicalOrderRecipe
			where cihd.ControlIntegrationHeonId = @HeaderId

		end
		
		--Se inserta ok en la tabla return
		insert into @TableMessage(CodeMessage, MessageReturn) values(0, 'Se ha guardado correctamente en las tablas de control de integración')
		
		--Se devuelve la info
		select * from @TableMessage

	end try
	begin catch

		--Se limpia la tabla de mensajes
		delete from @TableMessage

		--Se inserta en la tabla de mensajes el error
		insert into @TableMessage(CodeMessage, MessageReturn) values(999, ERROR_MESSAGE())

		--Se devuelve la info
		select * from @TableMessage

	end catch

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda y actualiza el registro de integración con el sistema logístico externo HEON para operaciones de inventario farmacéutico. Recibe un XML con cabecera y detalle de la transacción: si aún no existe respuesta de HEON, inserta un nuevo registro en la tabla de control (ControlIntegrationHeon) junto con sus ítems detallados (ControlIntegrationHeonDetail) vinculados a la dispensación farmacéutica correspondiente (PharmaceuticalDispensing); si ya hay respuesta, actualiza el estado y los mensajes de HEON tanto en la cabecera como en cada ítem (medicamento o producto por orden médica/receta). Se usa para sincronizar el ciclo de vida de una dispensación de medicamentos entre Indigo y el operador logístico HEON, registrando solicitudes, entregas y confirmaciones.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SaveIntegration';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SaveIntegration';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Persiste y sincroniza la integración con el sistema HEON: crea el registro de control y sus detalles cuando aún no hay respuesta, o actualiza estado y respuesta de cabecera y detalles cuando HEON ya respondió.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveIntegration';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe contener un nodo /Header con los campos de cabecera y, opcionalmente, nodos /Header/Details con los ítems.; Debe existir un registro en Inventory.PharmaceuticalDispensing cuyo Code coincida con EntityCode del XML (de lo contrario @EntityId queda NULL).; Para el flujo de actualización (cuando JsonRespuestaHeon viene con valor) debe existir previamente una cabecera en Inventory.ControlIntegrationHeon con el mismo EntityCode.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveIntegration';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El EntityId usado para persistir siempre se sobrescribe con el Id de Inventory.PharmaceuticalDispensing que coincide con EntityCode, ignorando el EntityId que llegue en el XML.; En el flujo de inserción la fecha de confirmación se fija con Common.GETDATE() y el usuario de confirmación con @CodeUser.; La actualización de detalles solo afecta filas cuyo ControlIntegrationHeonId corresponde a la cabecera localizada por EntityCode, conservando aislamiento por integración.; Siempre se devuelve un result set con CodeMessage y MessageReturn (0 éxito, 999 error).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveIntegration';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Dispensación farmacéutica; Integración HEON; Operador logístico; Orden médica / receta; Centro de atención; Medicamento/producto', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveIntegration';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Inventory.ControlIntegrationHeon: Cuando @JsonRespuestaHEON = '''' se inserta la cabecera de integración con los datos del XML, ConfirmationDate=Common.GETDATE() y ConfirmationUser=@CodeUser.; [INSERT] Inventory.ControlIntegrationHeonDetail: Tras insertar la cabecera (rama sin respuesta HEON), se insertan todos los detalles del XML asociados al Id recién generado vía SCOPE_IDENTITY().; [UPDATE] Inventory.ControlIntegrationHeon: Cuando ya existe respuesta de HEON, se actualizan Status y JsonRespuestaHEON en la fila cuyo Id corresponde al EntityCode recibido.; [UPDATE] Inventory.ControlIntegrationHeonDetail: En la rama de respuesta HEON, se actualizan Status y Message de cada detalle haciendo join por MedicalOrderRecipe contra el XML, restringido a ControlIntegrationHeonId = cabecera localizada por EntityCode.; [RETURN_RESULT] @TableMessage: Devuelve (0, ''Se ha guardado correctamente...'') al finalizar con éxito, o (999, ERROR_MESSAGE()) en el bloque CATCH.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveIntegration';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @JsonRespuestaHEON = '''' (no hay respuesta aún de HEON) → Inserta una nueva cabecera en ControlIntegrationHeon con fecha y usuario de confirmación, recupera el Id con SCOPE_IDENTITY() e inserta los detalles asociados en ControlIntegrationHeonDetail. else Localiza la cabecera existente por EntityCode y actualiza Status y JsonRespuestaHEON, además de actualizar Status y Message en cada detalle haciendo match por MedicalOrderRecipe.; si Ocurre cualquier error durante el TRY (catch) → Limpia la tabla de mensajes y devuelve un único registro con CodeMessage=999 y el texto de ERROR_MESSAGE(). else Devuelve CodeMessage=0 con mensaje de éxito de guardado.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveIntegration';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveIntegration';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PharmaceuticalDispensing; Inventory.ControlIntegrationHeon; Inventory.ControlIntegrationHeonDetail', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveIntegration';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveIntegration';
-- GO
