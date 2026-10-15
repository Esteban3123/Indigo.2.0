-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-03-26
-- Description:	Procedimiento que se encarga de guardar, actualizar una actividad
-- =============================================
CREATE PROCEDURE [Cost].[SP_SaveCostActivity] 
    @CostActivityXml AS XML,
	@ListCostProductionCenterXml AS XML,
	@ListCostActivityStepXml AS XML,
	@ListCostActivityStepFixedAssetXml AS XML,
	@ListCostActivityStepPayrollXml AS XML,
	@ListCostActivityStepInventoryXml AS XML,
	@ListCostActivityStepAddictionalCostXml AS XML,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON

	/************************************* VARIABLES ************************************/

	DECLARE @Id INT, 
			@Code VARCHAR(20),
			@Name VARCHAR(100),
			@OperatingUnitId INT,
			@CUPSEntityId INT,
			@InitialDate DATE,
			@EndDate DATE,
			@Description VARCHAR(500),
			------------------------------
			@errors VARCHAR(MAX),
			------------------------------
			@maxOrder INT

	DECLARE @CostActivityProductionCenter TABLE
	(
		[Id] [int],
		[CostProductionCenterId] [int]
	)

	DECLARE @CostActivityStep TABLE
	(
		[Id] [int],
		[UUID] [varchar](500),
		[Order] [int],
		[Description] [varchar](500)
	)

	DECLARE @CostActivityStepFixedAsset TABLE
	(
		[id] [int],
		[ParentUUID] [varchar](500),
		[CostActivityStepId] [int],
		[FixedAssetItemId] [int],
		[Hours] [decimal](24,6)
	)

	DECLARE @CostActivityStepPayroll TABLE
	(
		[id] [int],
		[ParentUUID] [varchar](500),
		[CostActivityStepId] [int],
		[PayrollPositionId] [int],
		[Hours] [decimal](24,6)
	)

	DECLARE @CostActivityStepInventory TABLE
	(
		[id] [int],
		[ParentUUID] [varchar](500),
		[CostActivityStepId] [int],
		[CostInventoryGroupId] [int],
		[Quantity] [decimal](24,6)
	)

	DECLARE @CostActivityStepAddictionalCost TABLE
	(
		[id] [int],
		[ParentUUID] [varchar](500),
		[CostActivityStepId] [int],
		[Description] [varchar](500),
		[Value] [decimal](18,2)
	)

	/************************************* --------- ************************************/

	BEGIN TRY
		
		SELECT 
			@Id = t.x.value('Id[1]','int'),
			@Code = t.x.value('Code[1]','varchar(20)'),
			@Name = t.x.value('Name[1]','varchar(100)'),
			@OperatingUnitId = t.x.value('OperatingUnitId[1]','int'),
			@CUPSEntityId = t.x.value('CUPSEntityId[1]','int'),
			@InitialDate = t.x.value('InitialDate[1]','date'),
			@EndDate = t.x.value('EndDate[1]','date'),			
			@Description = t.x.value('Description[1]','varchar(500)')
		FROM @CostActivityXml.nodes('/CostActivity') t(x)
		
		INSERT INTO @CostActivityProductionCenter
			SELECT DISTINCT
				t.x.value('Id[1]','int') as Id,
				t.x.value('CostProductionCenterId[1]','int') as CostProductionCenterId
			FROM @ListCostProductionCenterXml.nodes('/ListCostActivityProductionCenter/CostActivityProductionCenter') t(x)

		INSERT INTO @CostActivityStep
			SELECT DISTINCT
				t.x.value('Id[1]','int') as Id,
				t.x.value('UUID[1]','varchar(500)') as UUID,
				t.x.value('Order[1]','int') as [Order],
				t.x.value('Description[1]','varchar(500)') as [Description]
			FROM @ListCostActivityStepXml.nodes('/ListCostActivityStep/CostActivityStep') t(x)

		INSERT INTO @CostActivityStepFixedAsset
			SELECT DISTINCT
				t.x.value('Id[1]','int') as Id,
				t.x.value('ParentUUID[1]','varchar(500)') as ParentUUID,
				t.x.value('CostActivityStepId[1]','int') as [CostActivityStepId],
				t.x.value('FixedAssetItemId[1]','int') as [FixedAssetItemId],
				t.x.value('Hours[1]','decimal(24,6)') as [Hours]
			FROM @ListCostActivityStepFixedAssetXml.nodes('/ListCostActivityStepFixedAsset/CostActivityStepFixedAsset') t(x)

		INSERT INTO @CostActivityStepPayroll
			SELECT DISTINCT
				t.x.value('Id[1]','int') as Id,
				t.x.value('ParentUUID[1]','varchar(500)') as ParentUUID,
				t.x.value('CostActivityStepId[1]','int') as [CostActivityStepId],
				t.x.value('PayrollPositionId[1]','int') as [PayrollPositionId],
				t.x.value('Hours[1]','decimal(24,6)') as [Hours]
			FROM @ListCostActivityStepPayrollXml.nodes('/ListCostActivityStepPayroll/CostActivityStepPayroll') t(x)

		INSERT INTO @CostActivityStepInventory
			SELECT DISTINCT
				t.x.value('Id[1]','int') as Id,
				t.x.value('ParentUUID[1]','varchar(500)') as ParentUUID,
				t.x.value('CostActivityStepId[1]','int') as [CostActivityStepId],
				t.x.value('CostInventoryGroupId[1]','int') as [CostInventoryGroupId],
				t.x.value('Quantity[1]','decimal(24,6)') as [Quantity]
			FROM @ListCostActivityStepInventoryXml.nodes('/ListCostActivityStepInventory/CostActivityStepInventory') t(x)

		INSERT INTO @CostActivityStepAddictionalCost
			SELECT DISTINCT
				t.x.value('Id[1]','int') as Id,
				t.x.value('ParentUUID[1]','varchar(500)') as ParentUUID,
				t.x.value('CostActivityStepId[1]','int') as [CostActivityStepId],
				t.x.value('Description[1]','varchar(500)') as [Description],
				t.x.value('Value[1]','decimal(18,2)') as [Value]
			FROM @ListCostActivityStepAddictionalCostXml.nodes('/ListCostActivityStepAddictionalCost/CostActivityStepAddictionalCost') t(x)

		/************************************* VALIDACIONES ************************************/

		IF EXISTS (SELECT 1 FROM Cost.CostActivity ca WHERE ca.Id = @Id AND ca.Status <> 1)
		BEGIN
			SELECT 999 as CodeMessage, 'El registro se encuentra en estado: Inactivo' as Message, '' as Code, 0 as Id
			RETURN
		END

		--IF EXISTS (SELECT 1 FROM Cost.CostActivity ca WHERE ca.Id = @Id AND ca.Status <> 1)
		--BEGIN
		--	SELECT 999 as CodeMessage, 'No se puede editar porque la actividad ya se uso en una estimación ' as Message, '' as Code, 0 as Id
		--	RETURN
		--END

		IF NOT EXISTS ( SELECT 1 FROM @CostActivityProductionCenter capc JOIN Cost.CostProductionCenter cpc ON capc.CostProductionCenterId = cpc.Id )
		BEGIN
			SELECT 999 AS CodeMessage, 'Debe seleccionar al menos un centro de producción ' AS Message, '' AS Code, 0 AS Id
			RETURN
		END		

		IF EXISTS 
		( 
			SELECT 1 
			FROM Cost.CostActivity ca
			JOIN Cost.CostActivityProductionCenter capc ON ca.Id = capc.CostActivityId
			JOIN @CostActivityProductionCenter capca ON capc.CostProductionCenterId = capca.CostProductionCenterId
			WHERE ca.Id <> @Id AND ca.Status = 1 AND ca.CUPSEntityId = @CUPSEntityId
		)
		BEGIN
			SELECT @errors = STUFF((
					SELECT CHAR(13) + CHAR(10) + ' - ' + CONCAT(cpc.Code, ' - ', cpc.Name, ' (Actividad: ', ca.Code, ' - ', ca.Name, ')')
					FROM Cost.CostActivity ca
					JOIN Cost.CostActivityProductionCenter capc ON ca.Id = capc.CostActivityId
					JOIN Cost.CostProductionCenter cpc ON capc.CostProductionCenterId = cpc.Id
					JOIN @CostActivityProductionCenter capca ON capc.CostProductionCenterId = capca.CostProductionCenterId
					WHERE ca.Id <> @Id AND ca.Status = 1 AND ca.CUPSEntityId = @CUPSEntityId
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeMessage, 'Los siguientes centros de producción ya se encuentran en otra actividad activa asignados al mismo servicio: ' + ISNULL(@errors, '') AS Message, '' AS Code, 0 AS Id
			RETURN
		END

		IF NOT EXISTS ( SELECT 1 FROM @CostActivityStep caps )
		BEGIN
			SELECT 999 AS CodeMessage, 'Debe agregar al menos un paso ' AS Message, '' AS Code, 0 AS Id
			RETURN
		END

		SELECT @maxOrder = MAX(cas.[Order])
		FROM @CostActivityStep cas

		IF EXISTS
		(
			SELECT 1
			FROM @CostActivityStep casi
			LEFT JOIN @CostActivityStep casf ON casi.[Order] + 1 = casf.[Order]
			WHERE casf.Id IS NULL AND casi.[Order] < @maxOrder
		)
		BEGIN
			SELECT 999 AS CodeMessage, 'Los pasos no siguen una secuencia ordenada ' AS Message, '' AS Code, 0 AS Id
			RETURN
		END

		IF EXISTS
		(
			SELECT 1
			FROM @CostActivityStep cas
			GROUP BY cas.[Order]
			HAVING COUNT(*) > 1
		)
		BEGIN
			SELECT 999 AS CodeMessage, 'Existen pasos con orden duplicado ' AS Message, '' AS Code, 0 AS Id
			RETURN
		END

		IF EXISTS
		(
			SELECT 1
			FROM @CostActivityStep cas
			LEFT JOIN @CostActivityStepFixedAsset casfa ON cas.Id = casfa.CostActivityStepId AND cas.UUID = casfa.ParentUUID
			LEFT JOIN @CostActivityStepPayroll casp ON cas.Id = casp.CostActivityStepId AND cas.UUID = casp.ParentUUID
			LEFT JOIN @CostActivityStepInventory casi ON cas.Id = casi.CostActivityStepId AND cas.UUID = casi.ParentUUID
			LEFT JOIN @CostActivityStepAddictionalCost casac ON cas.Id = casac.CostActivityStepId AND cas.UUID = casac.ParentUUID
			WHERE casfa.id IS NULL AND casp.id IS NULL AND casi.id IS NULL AND casac.id IS NULL
		)
		BEGIN
			SELECT @errors = STUFF((
					SELECT CHAR(13) + CHAR(10) + ' - ' + CONCAT(cas.[Order], ' - ', cas.Description)
					FROM @CostActivityStep cas
			LEFT JOIN @CostActivityStepFixedAsset casfa ON cas.Id = casfa.CostActivityStepId AND cas.UUID = casfa.ParentUUID
			LEFT JOIN @CostActivityStepPayroll casp ON cas.Id = casp.CostActivityStepId AND cas.UUID = casp.ParentUUID
			LEFT JOIN @CostActivityStepInventory casi ON cas.Id = casi.CostActivityStepId AND cas.UUID = casi.ParentUUID
			LEFT JOIN @CostActivityStepAddictionalCost casac ON cas.Id = casac.CostActivityStepId AND cas.UUID = casac.ParentUUID
			WHERE casfa.id IS NULL AND casp.id IS NULL AND casi.id IS NULL AND casac.id IS NULL
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeMessage, 'Los siguientes pasos no tienen detalles relacionados: ' + ISNULL(@errors, '') AS Message, '' AS Code, 0 AS Id
			RETURN
		END

		/************************************* CABECERA ************************************/
		print @code
		print @id

		DECLARE @prueba AS BIT; --Declarar mejor 

		SET @prueba = CASE 
                 WHEN EXISTS (SELECT 1 FROM [Cost].[CostActivity] WHERE Code = @code) 
                 THEN 1 
                 ELSE 0 
              END;
		print @prueba
		print ':o'
		--Si se esta insertando por primera vez se consulta la secuencia numerica
		IF @Id = 0
		BEGIN
			IF @Code = '' OR @prueba <> 1
			BEGIN
				--Consultamos si la secuencia es con O o OU
				DECLARE @IdForm VARCHAR(5) = '2058',							
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
					SELECT 999 as CodeMessage, 'Secuencia de Actividades no encontrada' as Message, '' as Code, 0 as Id
					RETURN
				END
				if @Code =''
					Begin
					SELECT @Code = dbo.GetSequence('', @pattern, @NextS);

				end 
				UPDATE Cost.CostSecuenceDetail SET [Next] += 1 WHERE Id = @idSequenceDetail

				--Se inserta la cabecera
				INSERT INTO [Cost].[CostActivity]
				(
					[Code],[Name],[OperatingUnitId],[CUPSEntityId],[InitialDate],[EndDate],[Description],[Status],[CreationUser],[CreationDate]
				)
				SELECT @Code,@Name,@OperatingUnitId,@CUPSEntityId,@InitialDate,@EndDate,@Description,1,@CodeUser,[Common].[GETDATE]()

				--Obtengo el id de la cabcera
				SET @Id = SCOPE_IDENTITY()
			END
		END
		ELSE --Si se esta actualizando
		BEGIN
			UPDATE [Cost].[CostActivity]
				SET [Code] = @Code,
					[Name] = @Name,
					[OperatingUnitId] = @OperatingUnitId,
					[CUPSEntityId] = @CUPSEntityId,
					[InitialDate] = @InitialDate,
					[EndDate] = @EndDate,
					[Description] = @Description,
					[Status] = 1,
					[ModificationUser] = @CodeUser,
					[ModificationDate] = [Common].[GETDATE]()
			WHERE Id = @Id

		END

		/************************************* CENTROS DE PRODUCCION ************************************/

		DELETE capc
		FROM Cost.CostActivityProductionCenter capc
		LEFT JOIN @CostActivityProductionCenter tcapc ON capc.Id = tcapc.Id
		WHERE capc.CostActivityId = @Id AND tcapc.Id IS NULL
		
		UPDATE capc
			SET capc.CostProductionCenterId = tcapc.CostProductionCenterId
		FROM Cost.CostActivityProductionCenter capc
		JOIN @CostActivityProductionCenter tcapc ON capc.Id = tcapc.Id
			AND capc.CostActivityId = @Id

		--SELECT @Id, tcapc.CostProductionCenterId
		--FROM @CostActivityProductionCenter tcapc

		INSERT INTO Cost.CostActivityProductionCenter
		(
			CostActivityId, CostProductionCenterId
		)
		SELECT @Id, tcapc.CostProductionCenterId
		FROM @CostActivityProductionCenter tcapc
		LEFT JOIN Cost.CostActivityProductionCenter capc ON capc.Id = tcapc.Id
		WHERE capc.Id IS NULL

		/************************************* PASOS ************************************/

			/************************************* SE ELIMINA LOS ELEMENTOS RELACIONADOS ANTES DE ELIMINAR EL PASO ************************************/

			DELETE casfa
			FROM Cost.CostActivityStepFixedAsset casfa
			JOIN Cost.CostActivityStep cas ON casfa.CostActivityStepId = cas.Id AND cas.CostActivityId = @Id
			LEFT JOIN @CostActivityStepFixedAsset tcasfa ON casfa.Id = tcasfa.Id
			WHERE tcasfa.Id IS NULL

			DELETE casp
			FROM Cost.CostActivityStepPayroll casp
			JOIN Cost.CostActivityStep cas ON casp.CostActivityStepId = cas.Id AND cas.CostActivityId = @Id
			LEFT JOIN @CostActivityStepPayroll tcasp ON casp.Id = tcasp.Id
			WHERE tcasp.Id IS NULL

			DELETE casi
			FROM Cost.CostActivityStepInventory casi
			JOIN Cost.CostActivityStep cas ON casi.CostActivityStepId = cas.Id AND cas.CostActivityId = @Id
			LEFT JOIN @CostActivityStepInventory tcasi ON casi.Id = tcasi.Id
			WHERE tcasi.Id IS NULL

			DELETE casac
			FROM Cost.CostActivityStepAddictionalCost casac
			JOIN Cost.CostActivityStep cas ON casac.CostActivityStepId = cas.Id AND cas.CostActivityId = @Id
			LEFT JOIN @CostActivityStepAddictionalCost tcasac ON casac.Id = tcasac.Id
			WHERE tcasac.Id IS NULL

			/************************************* SE ACTUALIZA LOS ELEMENTOS RELACIONADOS ANTES DE ELIMINAR EL PASO ************************************/

			UPDATE casfa
				SET casfa.CostActivityStepId = tcasfa.CostActivityStepId,
					casfa.FixedAssetItemId = tcasfa.FixedAssetItemId,
					casfa.Hours = tcasfa.Hours
			FROM Cost.CostActivityStepFixedAsset casfa
			JOIN Cost.CostActivityStep cas ON casfa.CostActivityStepId = cas.Id AND cas.CostActivityId = @Id
			JOIN @CostActivityStepFixedAsset tcasfa ON casfa.Id = tcasfa.Id

			UPDATE casp
				SET casp.CostActivityStepId = tcasp.CostActivityStepId,
					casp.PayrollPositionId = tcasp.PayrollPositionId,
					casp.Hours = tcasp.Hours
			FROM Cost.CostActivityStepPayroll casp
			JOIN Cost.CostActivityStep cas ON casp.CostActivityStepId = cas.Id AND cas.CostActivityId = @Id
			JOIN @CostActivityStepPayroll tcasp ON casp.Id = tcasp.Id

			UPDATE casi
				SET casi.CostActivityStepId = tcasi.CostActivityStepId,
					casi.CostInventoryGroupId = tcasi.CostInventoryGroupId,
					casi.Quantity = tcasi.Quantity
			FROM Cost.CostActivityStepInventory casi
			JOIN Cost.CostActivityStep cas ON casi.CostActivityStepId = cas.Id AND cas.CostActivityId = @Id
			JOIN @CostActivityStepInventory tcasi ON casi.Id = tcasi.Id

			UPDATE casac
				SET casac.CostActivityStepId = tcasac.CostActivityStepId,
					casac.Description = tcasac.Description,
					casac.Value = tcasac.Value
			FROM Cost.CostActivityStepAddictionalCost casac
			JOIN Cost.CostActivityStep cas ON casac.CostActivityStepId = cas.Id AND cas.CostActivityId = @Id
			JOIN @CostActivityStepAddictionalCost tcasac ON casac.Id = tcasac.Id

		DELETE cas
		FROM Cost.CostActivityStep cas
		LEFT JOIN @CostActivityStep tcas ON cas.Id = tcas.Id
		WHERE cas.CostActivityId = @Id AND tcas.Id IS NULL

		UPDATE cas
			SET cas.[Order] = tcas.[Order],
				cas.Description = tcas.Description
		FROM Cost.CostActivityStep cas
		JOIN @CostActivityStep tcas ON cas.Id = tcas.Id
			AND cas.CostActivityId = @Id

		INSERT INTO Cost.CostActivityStep
		(
			CostActivityId, [Order], Description
		)
		SELECT @Id, tcas.[Order], tcas.Description
		FROM @CostActivityStep tcas
		LEFT JOIN Cost.CostActivityStep cas ON cas.Id = tcas.Id
		WHERE cas.Id IS NULL

		/************************************* SE ACTUALIZA EL ID DEL PASO EN LOS ELEMENTOS RELACIONADOS ************************************/

		UPDATE tcasfa
			SET tcasfa.CostActivityStepId = cas.Id
		FROM @CostActivityStep tcas
		JOIN @CostActivityStepFixedAsset tcasfa ON tcas.UUID = tcasfa.ParentUUID AND tcas.Id = tcasfa.CostActivityStepId
		JOIN Cost.CostActivityStep cas ON cas.CostActivityId = @Id AND cas.[Order] = tcas.[Order]

		UPDATE tcasp
			SET tcasp.CostActivityStepId = cas.Id
		FROM @CostActivityStep tcas
		JOIN @CostActivityStepPayroll tcasp ON tcas.UUID = tcasp.ParentUUID AND tcas.Id = tcasp.CostActivityStepId
		JOIN Cost.CostActivityStep cas ON cas.CostActivityId = @Id AND cas.[Order] = tcas.[Order]

		UPDATE tcasi
			SET tcasi.CostActivityStepId = cas.Id
		FROM @CostActivityStep tcas
		JOIN @CostActivityStepInventory tcasi ON tcas.UUID = tcasi.ParentUUID AND tcas.Id = tcasi.CostActivityStepId
		JOIN Cost.CostActivityStep cas ON cas.CostActivityId = @Id AND cas.[Order] = tcas.[Order]

		UPDATE tcasac
			SET tcasac.CostActivityStepId = cas.Id
		FROM @CostActivityStep tcas
		JOIN @CostActivityStepAddictionalCost tcasac ON tcas.UUID = tcasac.ParentUUID AND tcas.Id = tcasac.CostActivityStepId
		JOIN Cost.CostActivityStep cas ON cas.CostActivityId = @Id AND cas.[Order] = tcas.[Order]

		/************************************* SE INSERTA LOS ELEMENTOS RELACIONADOS  ************************************/
		
		INSERT INTO Cost.CostActivityStepFixedAsset
		(
			CostActivityStepId, FixedAssetItemId, Hours
		)
		SELECT tcasfa.CostActivityStepId, tcasfa.FixedAssetItemId, tcasfa.Hours
		FROM @CostActivityStepFixedAsset tcasfa
		LEFT JOIN Cost.CostActivityStepFixedAsset casfa ON casfa.Id = tcasfa.Id
		WHERE casfa.Id IS NULL

		INSERT INTO Cost.CostActivityStepPayroll
		(
			CostActivityStepId, PayrollPositionId, Hours
		)
		SELECT tcasp.CostActivityStepId, tcasp.PayrollPositionId, tcasp.Hours
		FROM @CostActivityStepPayroll tcasp
		LEFT JOIN Cost.CostActivityStepPayroll casp ON casp.Id = tcasp.Id
		WHERE casp.Id IS NULL
		
		INSERT INTO Cost.CostActivityStepInventory
		(
			CostActivityStepId, CostInventoryGroupId, Quantity
		)
		SELECT tcasi.CostActivityStepId, tcasi.CostInventoryGroupId, tcasi.Quantity
		FROM @CostActivityStepInventory tcasi
		LEFT JOIN Cost.CostActivityStepInventory casi ON casi.Id = tcasi.Id
		WHERE casi.Id IS NULL
		
		INSERT INTO Cost.CostActivityStepAddictionalCost
		(
			CostActivityStepId, Description, Value
		)
		SELECT tcasac.CostActivityStepId, tcasac.Description, tcasac.Value
		FROM @CostActivityStepAddictionalCost tcasac
		LEFT JOIN Cost.CostActivityStepAddictionalCost casac ON casac.Id = tcasac.Id
		WHERE casac.Id IS NULL

		SELECT 0 AS CodeMessage, 'Se guardó correctamente' AS Message, @Code as Code, @Id as Id		
	END TRY
	BEGIN CATCH
		SELECT 999 AS CodeMessage, ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)) AS Message, '' AS Code, 0 AS Id
	END CATCH
END