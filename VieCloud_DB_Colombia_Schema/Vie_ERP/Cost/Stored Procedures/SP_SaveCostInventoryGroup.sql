-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-04-23
-- Description:	Procedimiento que se encarga de guardar, actualizar un grupo de productos
-- =============================================
CREATE PROCEDURE [Cost].[SP_SaveCostInventoryGroup] 
    @CostInventoryGroupXml AS XML,
	@ListCostInventoryGroupDetailXml AS XML,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON

	/************************************* VARIABLES ************************************/

	DECLARE @Id INT, 
			@Code VARCHAR(20),
			@Name VARCHAR(100),
			@OperatingUnitId INT,
			@InventoryMeasurementUnitId INT,
			@Description VARCHAR(500),
			------------------------------
			@errors VARCHAR(MAX)

	DECLARE @CostInventoryGroupDetail TABLE
	(
		[Id] [int],
		[InventoryProductId] [int],
		[Quantity] [decimal](24,6)
	)

	/************************************* --------- ************************************/

	BEGIN TRY

		SELECT 
			@Id = t.x.value('Id[1]','int'),
			@Code = t.x.value('Code[1]','varchar(20)'),
			@Name = t.x.value('Name[1]','varchar(100)'),
			@OperatingUnitId = t.x.value('OperatingUnitId[1]','int'),
			@InventoryMeasurementUnitId = t.x.value('InventoryMeasurementUnitId[1]','int'),
			@Description = t.x.value('Description[1]','varchar(500)')
		FROM @CostInventoryGroupXml.nodes('/CostInventoryGroup') t(x)
		
		INSERT INTO @CostInventoryGroupDetail
			SELECT DISTINCT
				t.x.value('Id[1]','int') as Id,
				t.x.value('InventoryProductId[1]','int') as [InventoryProductId],
				t.x.value('Quantity[1]','decimal(24,6)') as [Quantity]
			FROM @ListCostInventoryGroupDetailXml.nodes('/ListCostInventoryGroupDetail/CostInventoryGroupDetail') t(x)

		/************************************* VALIDACIONES ************************************/

		IF EXISTS (SELECT 1 FROM Cost.CostInventoryGroup cig WHERE cig.Id = @Id AND cig.Status <> 1)
		BEGIN
			SELECT 999 as CodeMessage, 'El registro se encuentra en estado: Inactivo' as Message, '' as Code, 0 as Id
			RETURN
		END

		IF NOT EXISTS ( SELECT 1 FROM @CostInventoryGroupDetail cigd JOIN Inventory.InventoryProduct ip ON cigd.InventoryProductId = ip.Id )
		BEGIN
			SELECT 999 AS CodeMessage, 'Debe seleccionar al menos un detalle ' AS Message, '' AS Code, 0 AS Id
			RETURN
		END

		IF EXISTS
		(
			SELECT 1
			FROM @CostInventoryGroupDetail cigd
			JOIN Cost.CostInventoryGroupDetail cigdduplicate ON cigd.InventoryProductId = cigdduplicate.InventoryProductId
			JOIN Cost.CostInventoryGroup cigduplicate ON cigdduplicate.CostInventoryGroupId = cigduplicate.Id			
			WHERE cigduplicate.Id <> @Id AND cigduplicate.Status = 1
		)
		BEGIN
			SELECT @errors = STUFF((
					SELECT CHAR(13) + CHAR(10) + ' - ' + CONCAT(ip.Code, ' - ', ip.Name, ' (Grupo de Producto: ', cigduplicate.Code, ' - ', cigduplicate.Name, ')')
					FROM @CostInventoryGroupDetail cigd
					JOIN Cost.CostInventoryGroupDetail cigdduplicate ON cigd.InventoryProductId = cigdduplicate.InventoryProductId
					JOIN Cost.CostInventoryGroup cigduplicate ON cigdduplicate.CostInventoryGroupId = cigduplicate.Id
					JOIN Inventory.InventoryProduct ip ON cigd.InventoryProductId = ip.Id
					WHERE cigduplicate.Id <> @Id AND cigduplicate.Status = 1
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeMessage, 'Los siguientes productos ya fueron asociados: ' + CHAR(13) + CHAR(10) +  ISNULL(@errors, '') AS Message, '' AS Code, 0 AS Id
			RETURN
		END

		/************************************* CABECERA ************************************/

		--Si se esta insertando por primera vez se consulta la secuencia numerica
		IF @Id = 0
		BEGIN
			IF @Code = '' 
			BEGIN
				--Consultamos si la secuencia es con O o OU
				DECLARE @IdForm VARCHAR(5) = '2062',							
						@pattern VARCHAR(300),
						@NextS INT,
						@idSequenceDetail INT
				
				-- Consultamos la secuencia numerica del formulario
				SELECT @pattern = s.Pattern, 
					@NextS = csd.[Next], 
					@idSequenceDetail = csd.Id  
				FROM Cost.CostSecuenceDetail csd 
				JOIN Cost.CostSecuence cs ON cs.Id = csd.SequenseInteropCostId
				JOIN Common.Sequense s on csd.IdSequense = s.Id
				WHERE cs.IdForm = @IdForm 
					AND 
					(
						(cs.Scope = 'O')
						OR
						(cs.Scope <> 'O' AND csd.IdOperatingUnit = @OperatingUnitId)
					)

				IF (@idSequenceDetail IS NULL)
				BEGIN
					SELECT 999 as CodeMessage, 'Secuencia de Grupo de Productos no encontrada' as Message, '' as Code, 0 as Id
					RETURN
				END

				SELECT @Code = dbo.GetSequence('', @pattern, @NextS)
				UPDATE Cost.CostSecuenceDetail SET [Next] += 1 WHERE Id = @idSequenceDetail
			END
				--Se inserta la cabecera
				INSERT INTO [Cost].[CostInventoryGroup]
				(
					[Code],[Name],[OperatingUnitId],[InventoryMeasurementUnitId],[Description],[Status],[CreationUser],[CreationDate]
				)
				SELECT @Code,@Name,@OperatingUnitId,@InventoryMeasurementUnitId,@Description,1,@CodeUser,[Common].[GETDATE]()

				--Obtengo el id de la cabcera
				SET @Id = SCOPE_IDENTITY()
		END
		ELSE --Si se esta actualizando
		BEGIN
			UPDATE [Cost].[CostInventoryGroup]
				SET [Code] = @Code,
					[Name] = @Name,
					[OperatingUnitId] = @OperatingUnitId,
					[InventoryMeasurementUnitId] = @InventoryMeasurementUnitId,
					[Description] = @Description,
					[Status] = 1,
					[ModificationUser] = @CodeUser,
					[ModificationDate] = [Common].[GETDATE]()
			WHERE Id = @Id
		END

		/************************************* DETALLES ************************************/

		DELETE cigd
		FROM Cost.CostInventoryGroupDetail cigd
		LEFT JOIN @CostInventoryGroupDetail tcigd ON cigd.Id = tcigd.Id
		WHERE cigd.CostInventoryGroupId = @Id AND tcigd.Id IS NULL

		UPDATE cigd
			SET cigd.InventoryProductId = tcigd.InventoryProductId,
				cigd.Quantity = tcigd.Quantity
		FROM Cost.CostInventoryGroupDetail cigd
		JOIN @CostInventoryGroupDetail tcigd ON cigd.Id = tcigd.Id
		WHERE cigd.CostInventoryGroupId = @Id

		INSERT INTO Cost.CostInventoryGroupDetail
		(
			CostInventoryGroupId, InventoryProductId, Quantity
		)
		SELECT @Id, tcigd.InventoryProductId, tcigd.Quantity
		FROM @CostInventoryGroupDetail tcigd
		LEFT JOIN Cost.CostInventoryGroupDetail cigd ON cigd.Id = tcigd.Id
		WHERE cigd.Id IS NULL

		SELECT 0 AS CodeMessage, 'Se guardó correctamente' AS Message, @Code as Code, @Id as Id		
	END TRY
	BEGIN CATCH
		SELECT 999 AS CodeMessage, ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)) AS Message, '' AS Code, 0 AS Id
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Crea o actualiza un grupo de productos de inventario utilizado en el módulo de costos, junto con su detalle de ítems (insumos, medicamentos, materiales) y las cantidades asociadas a cada uno. Recibe los datos del encabezado del grupo y su lista de productos en formato XML, valida que el grupo esté activo, que tenga al menos un producto asociado y que ningún producto ya pertenezca a otro grupo de costos activo. Si es un registro nuevo y no se indica código, genera el código automáticamente consultando la secuencia numérica configurada en CostSecuence y CostSecuenceDetail; en caso de actualización, modifica los datos del grupo y sincroniza el detalle de productos insertando los nuevos, actualizando los existentes y eliminando los que ya no apliquen.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_SaveCostInventoryGroup';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_SaveCostInventoryGroup';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Guarda o actualiza un grupo de productos de inventario para costeo y sincroniza su detalle (insertar/actualizar/eliminar), validando estado activo, existencia de productos y no duplicidad entre grupos.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCostInventoryGroup';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'XML de cabecera con un único nodo /CostInventoryGroup parseable; XML de detalle con nodos /ListCostInventoryGroupDetail/CostInventoryGroupDetail; Los InventoryProductId del detalle deben existir en Inventory.InventoryProduct; Si Id > 0, el grupo debe existir y estar en Status = 1; Para generar código automático debe existir secuencia configurada para el formulario 2062 acorde al Scope/OperatingUnitId', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCostInventoryGroup';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Un producto de inventario no puede estar asociado simultáneamente a más de un grupo activo (Status=1); Los grupos creados o actualizados quedan siempre con Status=1 (activo); No se permite modificar grupos cuyo Status sea distinto de 1; El detalle del grupo se sincroniza completamente con la lista recibida: lo no enviado se elimina, lo coincidente se actualiza y lo nuevo se inserta; En alta sin código provisto, el código se genera a partir del patrón configurado y la secuencia se incrementa en 1; La selección de secuencia respeta el alcance: ''O'' aplica global, distinto de ''O'' aplica por unidad operativa; Toda alta registra CreationUser/CreationDate y toda actualización registra ModificationUser/ModificationDate usando Common.GETDATE()', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCostInventoryGroup';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Grupo de productos de inventario; Detalle de grupo de productos; Producto de inventario; Unidad operativa; Unidad de medida de inventario; Secuencia/consecutivo por formulario; Alcance de secuencia (Organización vs Unidad Operativa)', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCostInventoryGroup';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Cost.CostInventoryGroup: Cuando @Id = 0, inserta la cabecera con Status=1, CreationUser=@CodeUser y CreationDate=Common.GETDATE(); si @Code venía vacío usa el código generado por dbo.GetSequence; [UPDATE] Cost.CostSecuenceDetail: Cuando se generó código nuevo desde la secuencia, incrementa [Next] en 1 para el idSequenceDetail consultado; [UPDATE] Cost.CostInventoryGroup: Cuando @Id <> 0, actualiza Code/Name/OperatingUnitId/InventoryMeasurementUnitId/Description, fija Status=1 y registra ModificationUser/ModificationDate; [DELETE] Cost.CostInventoryGroupDetail: Elimina los detalles del grupo (@Id) cuyos Id no estén presentes en la lista recibida; [UPDATE] Cost.CostInventoryGroupDetail: Para detalles cuyos Id coinciden con la tabla en memoria, actualiza InventoryProductId y Quantity; [INSERT] Cost.CostInventoryGroupDetail: Inserta los detalles recibidos cuyo Id no existe aún, asociándolos al grupo @Id; [RETURN_RESULT] (resultset): Retorna CodeMessage=0 con Code e Id al guardar correctamente; CodeMessage=999 con mensaje específico ante validación fallida o error capturado en CATCH (incluyendo línea)', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCostInventoryGroup';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe un grupo con el Id recibido cuyo Status <> 1 (inactivo) → Retorna mensaje 999 ''El registro se encuentra en estado: Inactivo'' y termina else Continúa el flujo; si Ningún detalle recibido coincide con un producto existente en Inventory.InventoryProduct → Retorna mensaje 999 ''Debe seleccionar al menos un detalle'' y termina; si Algún InventoryProductId del detalle ya está asociado a otro grupo activo (Status=1) distinto al actual → Construye listado de productos duplicados y retorna mensaje 999 ''Los siguientes productos ya fueron asociados...''; si Id = 0 (alta nueva) → Si Code está vacío, consulta la secuencia del formulario 2062 y genera el código; luego INSERT en CostInventoryGroup else UPDATE de la cabecera CostInventoryGroup por Id; si Code vacío en alta y no se encuentra secuencia para el formulario 2062 según Scope (''O'' global u ''OU'' por OperatingUnitId) → Retorna mensaje 999 ''Secuencia de Grupo de Productos no encontrada'' y termina', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCostInventoryGroup';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.GetSequence; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCostInventoryGroup';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostInventoryGroup; Cost.CostInventoryGroupDetail; Inventory.InventoryProduct; Cost.CostSecuenceDetail; Cost.CostSecuence; Common.Sequense', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCostInventoryGroup';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCostInventoryGroup';
-- GO
