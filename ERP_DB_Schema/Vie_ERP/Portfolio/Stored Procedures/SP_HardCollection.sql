-- =============================================
-- Author:		Carlos Ernesto Cordoba
-- Create Date: 14-07-2016
-- Description:	procedimiento para generar cuentas de dificil recaudo
-- =============================================
CREATE PROCEDURE [Portfolio].[SP_HardCollection]
	@HardCollectionXml AS XML,
	@User VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	
	--Se declaran las variables para obtener la cabecera
	DECLARE @HardCollectionId INT,
			@HardCollectionCode VARCHAR(20),
			@DocumentDate DATE,
			@OperatingUnitId INT,
			@Status TINYINT,
			@ChangeTracker VARCHAR(30),
			------------------------------
			@IdForm INT = 1816,
			------------------------------
			@Message VARCHAR(MAX),
			------------------------------
			@SubXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX)
    
	--Tabla temporal de los detalles
	DECLARE @HardCollectionDetail TABLE
	(
		[Id] [INT] NOT NULL,
		[HardCollectionId] [INT] NOT NULL,
		[AccountReceivableId] [INT] NOT NULL,
		[Balance] [NUMERIC](18, 2) NOT NULL,
		[ChangeTracker] VARCHAR(30)
	)
	
	BEGIN TRY
		--Se obtienen los datos de la cabecera
		SELECT	@HardCollectionId = t.x.value('Id[1]', 'INT'),
				@HardCollectionCode = t.x.value('Code[1]', 'VARCHAR(20)'),
				@DocumentDate = CONVERT( DATETIME, t.x.value('DocumentDate[1]', 'VARCHAR(20)'), 103),	
				@OperatingUnitId = t.x.value('OperatingUnitId[1]', 'INT'),	
				@Status = t.x.value('Status[1]', 'TINYINT'),
				@ChangeTracker = t.x.value('ChangeTracker[1]', 'VARCHAR(30)')
		FROM @HardCollectionXml.nodes('/HardCollection') t(x)

		IF EXISTS (SELECT 1 FROM Portfolio.HardCollection WHERE Id = @HardCollectionId AND Status <> 1)
		BEGIN
			SELECT	'999' AS CodeMessage,
					'La Cuenta de Dificil Recaudo se encuentra en estado: ' + IIF(Status = 2, 'Confirmado', 'Anulado') Message,
					0 AS HardCollectionId,
					CAST(3 AS TINYINT) AS [Status]
			FROM Portfolio.HardCollection
			WHERE Id = @HardCollectionId
			RETURN
		END

		IF @Status = 3
		BEGIN
			UPDATE Portfolio.HardCollection
				SET [Status] = @Status,
					[ModificationUser] = @User,
					[ModificationDate] = [Common].[GETDATE](),
					[AnnulmentUser] = @User,
					[AnnulmentDate] = [Common].[GETDATE]()
			WHERE Id = @HardCollectionId
		END
		ELSE
		BEGIN
			--Se obtiene los detalles que vienen en el xml
			INSERT INTo @HardCollectionDetail
				SELECT	DISTINCT
						t.x.value('Id[1]', 'INT') as Id,
						t.x.value('HardCollectionId[1]', 'INT') as HardCollectionId,
						t.x.value('AccountReceivableId[1]', 'INT') as AccountReceivableId,		
						t.x.value('Balance[1]', 'decimal(18, 2)') AS Balance,
						t.x.value('ChangeTracker[1]', 'VARCHAR(30)') AS ChangeTracker
				FROM @HardCollectionXml.nodes('/HardCollection/HardCollectionDetail') t(x)

			--se eliminan los detalles
			DELETE hcd
			FROM @HardCollectionDetail d 
			JOIN Portfolio.HardCollectionDetail hcd on d.Id = hcd.Id
			WHERE d.ChangeTracker = 'Deleted' AND hcd.HardCollectionId = @HardCollectionId

			--Eliminamos registros que vengan duplicados
			DELETE d
			FROM @HardCollectionDetail d
			JOIN Portfolio.HardCollectionDetail hcd ON (@HardCollectionId = hcd.HardCollectionId AND d.AccountReceivableId = hcd.AccountReceivableId) AND ISNULL(d.Id, 0) = 0

			--Se obtiene los detalles que ya estan y no han sido actualizados
			INSERT INTo @HardCollectionDetail
				SELECT	hcd.Id,
						hcd.HardCollectionId,
						hcd.AccountReceivableId,
						hcd.Balance,
						'' ChangeTracker
				FROM Portfolio.HardCollectionDetail hcd
				LEFT JOIN @HardCollectionDetail d ON hcd.Id = d.Id
				WHERE hcd.HardCollectionId = @HardCollectionId AND d.Id IS NULL
		
			/*************************************VALIDACIONES************************************/

			-- Validar duplicados
			IF EXISTS 
			( 
				SELECT 1
				FROM @HardCollectionDetail d
				GROUP BY d.AccountReceivableId
				HAVING COUNT(*) > 1
			)
			BEGIN			
				SELECT @Message = STUFF((
							SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + ar.InvoiceNumber
							FROM @HardCollectionDetail d
							JOIN Portfolio.AccountReceivable ar ON d.AccountReceivableId = ar.Id
							GROUP BY d.AccountReceivableId, ar.InvoiceNumber
							FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
				
				SELECT	'999' CodeMessage, 
						'La Cuenta de Dificil Recaudo tiene la siguientes facturas duplicadas: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '') Message,
						0 AS HardCollectionId,
						CAST(3 AS TINYINT) AS [Status]
				RETURN
			END

			-- Validar que no se vuelva a cargar una factura que ya este en otro cuenta de dificil recaudo
			IF EXISTS 
			( 
				SELECT 1
				FROM Portfolio.HardCollection hc
				JOIN Portfolio.HardCollectionDetail hcd ON hc.Id = hcd.HardCollectionId
				JOIN @HardCollectionDetail d ON hcd.AccountReceivableId = d.AccountReceivableId
				WHERE hc.Id <> @HardCollectionId AND hc.Status IN (1, 2)
			)
			BEGIN			
				SELECT @Message = STUFF((
							SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + ar.InvoiceNumber + ' (Cuenta de Dificil Recaudo: ' + hc.Code + ')'
							FROM Portfolio.HardCollection hc
							JOIN Portfolio.HardCollectionDetail hcd ON hc.Id = hcd.HardCollectionId
							JOIN @HardCollectionDetail d ON hcd.AccountReceivableId = d.AccountReceivableId
							JOIN Portfolio.AccountReceivable ar ON d.AccountReceivableId = ar.Id
							WHERE hc.Id <> @HardCollectionId AND hc.Status IN (1, 2)
							FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
				
				SELECT	'999' CodeMessage, 
						'Existen facturas que se encuentran en otra Cuenta de Dificil Recaudo: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')  Message,
						0 AS HardCollectionId,
						CAST(3 AS TINYINT) AS [Status]
				RETURN
			END

			-- Validar Portfolio estado
			IF EXISTS 
			( 
				SELECT 1
				FROM @HardCollectionDetail d
				JOIN Portfolio.AccountReceivable ar ON d.AccountReceivableId = ar.Id
				WHERE ar.PortfolioStatus IN (1, 2, 15)
			)
			BEGIN			
				SELECT @Message = STUFF((
							SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + ar.InvoiceNumber + '(' +
								CASE ar.PortfolioStatus
									WHEN 1 THEN 'Sin Radicar'
									WHEN 2 THEN 'Radicada Sin Confirmar'
									WHEN 15 THEN 'Cuenta de Dificil Recaudo'
								END + ')'
							FROM @HardCollectionDetail d
							JOIN Portfolio.AccountReceivable ar ON d.AccountReceivableId = ar.Id
							WHERE ar.PortfolioStatus IN (1, 2, 15)
							FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
				
				SELECT	'999' CodeMessage, 
						'Las siguientes facturas se encuentran en estados no validos: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')  Message,
						0 AS HardCollectionId,
						CAST(3 AS TINYINT) AS [Status]
				RETURN
			END

			-- Validar que tenga saldo
			IF EXISTS 
			( 
				SELECT 1
				FROM @HardCollectionDetail d
				JOIN Portfolio.AccountReceivable ar ON d.AccountReceivableId = ar.Id
				WHERE ar.Balance = 0
			)
			BEGIN			
				SELECT @Message = STUFF((
							SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + ar.InvoiceNumber
							FROM @HardCollectionDetail d
							JOIN Portfolio.AccountReceivable ar ON d.AccountReceivableId = ar.Id
							WHERE ar.Balance = 0
							FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
				
				SELECT	'999' CodeMessage, 
						'Las siguientes facturas no tienen saldo: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')  Message,
						0 AS HardCollectionId,
						CAST(3 AS TINYINT) AS [Status]
				RETURN
			END

			/*************************************************************************************/

			DECLARE @ConfirmationUser VARCHAR(20) = CASE WHEN @Status = 2 THEN @User ELSE NULL END
			DECLARE @ConfirmationDate DATETIME = CASE WHEN @Status = 2 THEN [Common].[GETDATE]() ELSE NULL END

			IF ISNULL(@HardCollectionId, 0) = 0
			BEGIN
				--Si se esta insertando por primera vez se consulta la secuencia numerica
				IF @HardCollectionCode = '' 
				BEGIN
					--Si se esta insertando por primera vez se consulta la secuencia numerica
					DECLARE @IsManual BIT
				
					EXEC Common.SP_GetSequence 160, @IdForm, @OperatingUnitId, NULL, NULL, @IsManual OUT, @HardCollectionCode OUT, @Code_Output OUT, @Message_Output OUT

					IF @Code_Output <> 0
					BEGIN
						SELECT	'999' AS CodeMessage,
								REPLACE(@Message_Output, '{0}', 'Cuenta de Dificil Recaudo') Message,
								0 AS HardCollectionId,
								CAST(3 AS TINYINT) AS [Status]
						RETURN
					END

					--Se inserta la cabecera
					INSERT INTO [Portfolio].[HardCollection]
					(
						[Code],[DocumentDate],[OperatingUnitId],
						[Status],[CreationUser],[CreationDate],[ModificationUser],[ModificationDate],[ConfirmationUser],[ConfirmationDate]
					)
					SELECT @HardCollectionCode,@DocumentDate,@OperatingUnitId,
						@Status,@User,[Common].[GETDATE](),@ConfirmationUser,@ConfirmationDate,@ConfirmationUser,@ConfirmationDate

					--Obtengo el id de la cabcera
					SET @HardCollectionId = SCOPE_IDENTITY()
				END
			END
			ELSE --Si se esta actualizando
			BEGIN
				UPDATE [Portfolio].[HardCollection]
					SET [Code] = @HardCollectionCode,
						[DocumentDate] = @DocumentDate,
						[OperatingUnitId] = @OperatingUnitId,
						[Status] = @Status,
						[ModificationUser] = @User,
						[ModificationDate] = [Common].[GETDATE](),
						[ConfirmationUser] = @ConfirmationUser,
						[ConfirmationDate] = @ConfirmationDate
				WHERE Id = @HardCollectionId
			END

			/*************************************************************************************/

			--agrego los nuevos detalles
			INSERT INTO [Portfolio].[HardCollectionDetail]
			(
				HardCollectionId, AccountReceivableId, Balance
			)
			SELECT @HardCollectionId, d.AccountReceivableId, d.Balance
			FROM @HardCollectionDetail d 
			WHERE d.ChangeTracker = 'Added'

			/*************************************************************************************/

			--confirmacion de las cuentas de dificil recaudo
			IF @Status = 2
			BEGIN
				-- Se actualiza el saldo en el detalle de la radicación por el saldo actual de la factura
				UPDATE hcd 
					SET hcd.Balance = ar.Balance
				FROM Portfolio.HardCollectionDetail hcd 
				JOIN Portfolio.AccountReceivable ar ON hcd.AccountReceivableId = ar.Id
				WHERE hcd.HardCollectionId = @HardCollectionId

				SELECT @Message = STUFF((
							SELECT ', ' + ar.InvoiceNumber
							FROM Portfolio.HardCollectionDetail hcd 
							JOIN Portfolio.AccountReceivable ar ON hcd.AccountReceivableId = ar.Id
							WHERE hcd.HardCollectionId = @HardCollectionId
							FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				/***************************************** RECLASIFICAR LAS FACTURAS *****************************************/
				SELECT @SubXml = CONVERT
				(
					XML, 
					(
						SELECT *
						FROM 
						(
							SELECT	0 Id,
									@OperatingUnitId OperatingUnitId,
									'' Code,
									@DocumentDate DocumentDate,
									6 DocumentType,
									'Cuenta de Dificil Recaudo No. ' + @HardCollectionCode + ' Facturas: ' + @Message Detail,
									2 Status,
									hc.Id EntityId,
									hc.Code EntityCode,
									'HardCollection' EntityName
							FROM Portfolio.HardCollection hc
							WHERE hc.Id = @HardCollectionId
						) PortfolioReclassification
						JOIN
						(
							SELECT	0 PortfolioReclassificationId,
									0 Id,
									ar.Id AccountReceivableId,
									ara.MainAccountId SourceAccountId,
									ar.AccountHardCollectionId TargetAccountId,
									hcd.Balance Value
							FROM Portfolio.HardCollectionDetail hcd
							JOIN Portfolio.AccountReceivable ar ON hcd.AccountReceivableId = ar.Id
							LEFT JOIN
							(
								SELECT ara.AccountReceivableId, MAX(ara.Id) Id
								FROM Portfolio.AccountReceivableAccounting ara
								WHERE ara.Balance <> 0
								GROUP BY ara.AccountReceivableId
							) aram ON ar.Id = aram.AccountReceivableId
							LEFT JOIN Portfolio.AccountReceivableAccounting ara ON aram.Id = ara.Id
							WHERE hcd.HardCollectionId = @HardCollectionId AND hcd.Balance <> 0
						) PortfolioReclassificationDetail ON PortfolioReclassification.Id = PortfolioReclassificationDetail.PortfolioReclassificationId
						For xml AUTO,TYPE, ELEMENTS
					)
				)

				EXEC [Portfolio].[SP_SavePortfolioReclassification_Output] @SubXml, @User, @Code_Output OUT, @Message_Output OUT, NULL

				IF @Code_Output <> 0
				BEGIN
					SELECT	'999' CodeMessage, 
							ISNULL(@Message_Output, 'No se pudo generar la Reclasificación de Documentos de Cartera')  Message,
							0 AS HardCollectionId,
							CAST(3 AS TINYINT) AS [Status]
					RETURN
				END

				SET @Message = CHAR(13) + CHAR(10) + ISNULL(@Message_Output, '')

				/**************************************** ACTUALIZACION DE REGISTROS *****************************************/
		
				UPDATE ar 
					SET PortfolioStatus = 15
				FROM Portfolio.HardCollectionDetail hcd 
				JOIN Portfolio.AccountReceivable ar ON hcd.AccountReceivableId = ar.Id
				WHERE hcd.HardCollectionId = @HardCollectionId
			END	
		END

		SELECT	'0' AS CodeMessage,
				CASE @Status
				   WHEN 2 THEN CONCAT('Se guardó y confirmó la Cuenta de Dificil Recaudo con código ', @HardCollectionCode)
				   WHEN 3 THEN CONCAT('Se anuló la Cuenta de Dificil Recaudo con código ', @HardCollectionCode)
				   ELSE CONCAT('Se guardó la Cuenta de Dificil Recaudo con código ', @HardCollectionCode)
			   END + ISNULL(@Message, '') AS Message,
				@HardCollectionId as HardCollectionId,
				cast(1 as TINYINT) as [Status]
	END TRY
    BEGIN CATCH
        SELECT	'999' AS CodeMessage,
				ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)) Message,
				0 AS HardCollectionId,
				CAST(3 AS TINYINT) AS [Status];
    END CATCH	
END