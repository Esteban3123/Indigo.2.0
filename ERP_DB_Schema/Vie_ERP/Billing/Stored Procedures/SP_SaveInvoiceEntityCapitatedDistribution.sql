-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-03-16
-- Description:	Procedimiento que se encarga de guardar, actualizar la factura basica
-- =============================================
CREATE PROCEDURE [Billing].[SP_SaveInvoiceEntityCapitatedDistribution] 
    @InvoiceEntityCapitatedDistributionXml AS XML,
	@InvoiceEntityCapitatedDistributionDetailXml AS XML,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON

	--Se declaran las variables para obtener la cabecera
	DECLARE @Id INT, 
			@Code VARCHAR(20),
			@OperatingUnitId INT,
			@DocumentDate DATE,
			@InvoiceEntityCapitatedId INT,
			@Observation VARCHAR(MAX),
			@Status TINYINT,
			------------------------------
			@errors VARCHAR(MAX)

	--Tabla temporal de BasicBillingDetail
	DECLARE @Invoices TABLE
	(
		[InvoiceId] [int]
	)

	BEGIN TRY
		--Se obtienen los datos de la cabecera
		SELECT 
			@Id = t.x.value('Id[1]','int'),			
			@Code = t.x.value('Code[1]','varchar(20)'),
			@OperatingUnitId = t.x.value('OperatingUnitId[1]','int'),
			@DocumentDate = t.x.value('DocumentDate[1]','datetime'),
			@InvoiceEntityCapitatedId = t.x.value('InvoiceEntityCapitatedId[1]','int'),
			@Observation = t.x.value('Observation[1]','varchar(max)'),
			@Status = t.x.value('Status[1]','tinyint')
		FROM @InvoiceEntityCapitatedDistributionXml.nodes('/InvoiceEntityCapitatedDistribution') t(x)

		IF EXISTS (SELECT 1 FROM Billing.InvoiceEntityCapitatedDistribution iec WHERE iec.Id = @Id AND iec.Status <> 1)
		BEGIN
			SELECT 999 as CodeMessage, 'El registro se encuentra en estado: ' + IIF(iec.Status = 2, 'Confirmado', 'Anulado') as Message, '' as Code, 0 as Id
			FROM Billing.InvoiceEntityCapitatedDistribution iec
			WHERE iec.Id = @Id
			RETURN
		END

		--Si no se esta anulando
		IF @Status <> 3
		BEGIN
			--Se obtiene BasicBillingDetail
			INSERT INTO @Invoices
				SELECT DISTINCT
					t.x.value('Id[1]','int') as Id
				FROM @InvoiceEntityCapitatedDistributionDetailXml.nodes('/Invoice') t(x)

			/*************************************VALIDACIONES************************************/

			--- Valido que la factura monto fijo se encuentre confirmada
			IF NOT EXISTS ( SELECT 1 FROM [Billing].[InvoiceEntityCapitated] iec WHERE iec.Id = @InvoiceEntityCapitatedId AND iec.Status = 2 )
			BEGIN
				SELECT @errors = STUFF((
						SELECT iec.Code
						FROM [Billing].[InvoiceEntityCapitated] iec 
						WHERE iec.Id = @InvoiceEntityCapitatedId AND iec.Status <> 2
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT 999 AS CodeMessage, 'Los Factura Monto Fijo ' + ISNULL(@errors, '') + ' no existe o no se encuentra confirmada' AS Message, '' AS Code, 0 AS Id
				RETURN
			END

			--- Valido que la factura monto fijo no haya sido distribuida
			IF EXISTS ( SELECT 1 FROM [Billing].[InvoiceEntityCapitatedDistribution] iecd WHERE iecd.Id <> @Id AND iecd.InvoiceEntityCapitatedId = @InvoiceEntityCapitatedId AND iecd.Status NOT IN (3, 4) )
			BEGIN
				SELECT @errors = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + iecd.Code
						FROM [Billing].[InvoiceEntityCapitatedDistribution] iecd 
						WHERE iecd.Id <> @Id 
							AND iecd.InvoiceEntityCapitatedId = @InvoiceEntityCapitatedId 
							AND iecd.Status NOT IN (3, 4)
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT 999 AS CodeMessage, 'La factura Monto Fijo ya se encuentra en otra distribución ' + CHAR(13) + CHAR(10) + @errors AS Message, '' AS Code, 0 AS Id
				RETURN
			END

			--- Valido que todas las facturas sean un control de servicio
			IF EXISTS (SELECT 11 FROM @Invoices t LEFT JOIN [Billing].[Invoice] i ON t.InvoiceId = i.Id WHERE ISNULL(i.DocumentType, 0) <> 5)
			BEGIN
				SELECT @errors = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + ISNULL(i.InvoiceNumber, 'N/A')
						FROM @Invoices t 
						LEFT JOIN [Billing].[Invoice] i ON t.InvoiceId = i.Id 
						WHERE ISNULL(i.DocumentType, 0) <> 5
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT 999 AS CodeMessage, 'Los siguientes documentos no son controles de servicios: ' + CHAR(13) + CHAR(10) + @errors AS Message, '' AS Code, 0 AS Id
				RETURN
			END

			--- Valido que los controles de servicios no se encuentren anulados
			IF EXISTS 
			(
				SELECT 1 
				FROM @Invoices t 
				JOIN Billing.Invoice i ON t.InvoiceId = i.Id
				WHERE i.Status <> 1
			)
			BEGIN
				SELECT @errors = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + i.InvoiceNumber + ' (Estado: ' + IIF(i.Status = 2, 'ANULADO', 'N/A') + ')'
						FROM @Invoices t 
						JOIN Billing.Invoice i ON t.InvoiceId = i.Id
						WHERE i.Status <> 1
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT 999 AS CodeMessage, 'Los siguientes controles de servicios no se encuentran en estado facturado ' + CHAR(13) + CHAR(10) + @errors AS Message, '' AS Code, 0 AS Id
				RETURN
			END

			--- Valido que los controles de servicios no se encuentren ya distribuidas
			IF EXISTS 
			(
				SELECT 1 
				FROM [Billing].[InvoiceEntityCapitatedDistribution] iecd
				JOIN [Billing].[InvoiceEntityCapitatedDistributionDetail] iecdd ON iecd.Id = iecdd.InvoiceEntityCapitatedDistributionId
				JOIN @Invoices i ON iecdd.InvoiceId = i.InvoiceId
				WHERE iecd.Id <> @Id AND iecd.Status NOT IN (3, 4)
			)
			BEGIN
				SELECT @errors = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + i.InvoiceNumber + ' (Distribución: ' + iecd.Code + ')'
						FROM [Billing].[InvoiceEntityCapitatedDistribution] iecd
						JOIN [Billing].[InvoiceEntityCapitatedDistributionDetail] iecdd ON iecd.Id = iecdd.InvoiceEntityCapitatedDistributionId
						JOIN @Invoices t ON iecdd.InvoiceId = t.InvoiceId
						JOIN Billing.Invoice i ON t.InvoiceId = i.Id
						WHERE iecd.Id <> @Id AND iecd.Status NOT IN (3, 4)
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT 999 AS CodeMessage, 'Los siguientes controles de servicios ya se encuentran en otra distribución ' + CHAR(13) + CHAR(10) + @errors AS Message, '' AS Code, 0 AS Id
				RETURN
			END

			/*************************************************************************************/

			--Si se esta insertando por primera vez se consulta la secuencia numerica
			IF @Id = 0
			BEGIN
				IF @Code = '' 
				BEGIN
					--Consultamos si la secuencia es con O o OU
					DECLARE @IdForm VARCHAR(5) = '2057',							
							@pattern VARCHAR(300),
							@NextS INT,
							@idSequenceDetail INT
				
					-- Consultamos la secuencia numerica del formulario
					SELECT @pattern = cs.Pattern, 
						@NextS = bsd.[Next], 
						@idSequenceDetail = bsd.Id  
					FROM Billing.BillingSequenceDetail bsd 
					JOIN Billing.BillingSequence bs ON bs.Id = bsd.IdSequenseBillingC
					JOIN Common.Sequense cs on cs.Id = bsd.IdSequense
					WHERE bs.IdForm = @IdForm 
						AND 
						(
							(bs.Scope = 'O')
							OR
							(bs.Scope <> 'O' AND bsd.IdOperatingUnit = @OperatingUnitId)
						)

					IF (@idSequenceDetail IS NULL)
					BEGIN
						SELECT 999 as CodeMessage, 'Secuencia de Facturación Básica no encontrada' as Message, '' as Code, 0 as Id
						RETURN
					END

					SELECT @Code = dbo.GetSequence('', @pattern, @NextS)
					UPDATE Billing.BillingSequenceDetail SET [Next] += 1 WHERE Id = @idSequenceDetail

					--Se inserta la cabecera
					INSERT INTO [Billing].[InvoiceEntityCapitatedDistribution]
					(
						[Code],[OperatingUnitId],[DocumentDate],[InvoiceEntityCapitatedId],[InvoiceValue],[TotalControlValue],[Observation],[Status],[CreationUser],[CreationDate]
					)
					SELECT @Code,@OperatingUnitId,@DocumentDate,@InvoiceEntityCapitatedId,0 AS InvoiceValue,0 AS TotalControlValue,@Observation,@Status,@CodeUser,Common.GETDATE()

					--Obtengo el id de la cabcera
					SET @Id = SCOPE_IDENTITY()
				END
			END
			ELSE --Si se esta actualizando
			BEGIN
				UPDATE [Billing].[InvoiceEntityCapitatedDistribution]
					SET [Code] = @Code,
						[OperatingUnitId] = @OperatingUnitId,
						[DocumentDate] = @DocumentDate,
						[InvoiceEntityCapitatedId] = @InvoiceEntityCapitatedId,
						[Observation] = @Observation,
						[Status] = @Status,
						[ModificationUser] = @CodeUser,
						[ModificationDate] = Common.GETDATE()
				WHERE Id = @Id
			END

			-- Elimino los detalles que no se encuentren en las facturas enviadas
			DELETE iecdd
			FROM Billing.InvoiceEntityCapitatedDistributionDetail iecdd
			LEFT JOIN @Invoices i ON iecdd.InvoiceId = i.InvoiceId
			WHERE iecdd.InvoiceEntityCapitatedDistributionId = @Id
				AND i.InvoiceId IS NULL

			-- Inserto los nuevo detalles
			INSERT INTO [Billing].[InvoiceEntityCapitatedDistributionDetail]
            (
				[InvoiceEntityCapitatedDistributionId]
			   ,[InvoiceId]
			   ,[HealthAdministratorId]
			   ,[InvoiceCategoryId]
			   ,[InvoiceNumber]
			   ,[InvoiceDate]
			   ,[InvoiceValue]
			)
			SELECT
				@Id,
				i.Id,
				i.HealthAdministratorId,
				i.InvoiceCategoryId,
				i.InvoiceNumber,
				i.InvoiceDate,
				i.ThirdPartySalesValue
			FROM @Invoices t
			JOIN Billing.Invoice i ON t.InvoiceId = i.Id
			JOIN Billing.RevenueControlDetail rcd ON i.RevenueControlDetailId = rcd.Id
			LEFT JOIN [Billing].[InvoiceEntityCapitatedDistributionDetail] iecdd ON i.Id = iecdd.InvoiceId AND iecdd.InvoiceEntityCapitatedDistributionId = @Id
			WHERE iecdd.Id IS NULL

			-- Actualizo de acuerdo al valor de la factura monto fijo
			UPDATE iecd
				SET iecd.InvoiceValue = iec.TotalValue
			FROM Billing.InvoiceEntityCapitatedDistribution iecd
			JOIN Billing.InvoiceEntityCapitated iec ON iecd.InvoiceEntityCapitatedId = iec.Id
			WHERE iecd.Id = @Id

			-- Actualizo de acuerdo al valor de los detalles de la distribucion
			UPDATE iecd
				SET iecd.TotalControlValue = iecdd.TotalValue
			FROM Billing.InvoiceEntityCapitatedDistribution iecd
			JOIN 
			(
				SELECT iecdd.InvoiceEntityCapitatedDistributionId, SUM(iecdd.InvoiceValue) TotalValue
				FROM Billing.InvoiceEntityCapitatedDistributionDetail iecdd
				WHERE iecdd.InvoiceEntityCapitatedDistributionId = @Id
				GROUP BY iecdd.InvoiceEntityCapitatedDistributionId
			) iecdd ON iecd.Id = iecdd.InvoiceEntityCapitatedDistributionId
			WHERE iecd.Id = @Id

		END
		ELSE
		BEGIN
			DECLARE @AnnulmentUser VARCHAR(20) = CASE WHEN @Status <> 3 THEN null ELSE @CodeUser END
			DECLARE @AnnulmentDate DATETIME = CASE WHEN @Status <> 3 THEN null ELSE Common.GETDATE() END

			UPDATE [Billing].[InvoiceEntityCapitatedDistribution]
				SET [Status] = @Status,
					[AnnulmentUser] = @AnnulmentUser,
					[AnnulmentDate] = @AnnulmentDate
			WHERE Id = @Id
		END

		SELECT 0 AS CodeMessage, 'Se guardó correctamente' AS Message, @Code as Code, @Id as Id		
	END TRY
	BEGIN CATCH
		SELECT 999 AS CodeMessage, ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)) AS Message, '' AS Code, 0 AS Id
	END CATCH
END