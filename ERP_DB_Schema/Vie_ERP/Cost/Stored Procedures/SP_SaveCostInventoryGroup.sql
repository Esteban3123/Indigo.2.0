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