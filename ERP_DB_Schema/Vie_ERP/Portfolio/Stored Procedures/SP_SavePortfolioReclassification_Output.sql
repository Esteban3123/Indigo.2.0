-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-04-07
-- Description:	Procedimiento que se encarga de guardar y confirmar una Reclasificación de Documentos de Cartera
-- =============================================
CREATE PROCEDURE [Portfolio].[SP_SavePortfolioReclassification_Output]
    @PortfolioReclassificationXml AS XML,
	@CodeUser AS VARCHAR(20),
	------------------------------------------------------
	@CodeResult Int OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT,
	------------------------------------------------------
	@Code VARCHAR(20) OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	--Se declaran las variables para obtener la cabecera
	DECLARE @Id INT,
			@OperatingUnitId INT,
			@DocumentDate DATETIME,
			@DocumentType TINYINT,
			@Status TINYINT,
			@EntityId INT,
			@EntityCode VARCHAR(20),
			@EntityName VARCHAR(250),
			------------------------------
			@IdForm INT = 1529,
			@LegalBookId INT,
			@JournalVoucherTypeId INT,
			@Message VARCHAR(MAX),
			------------------------------
			@SubXml XML,
			@Code_Output INT,			
			@Message_Output VARCHAR(MAX),
			@JournalVoucherCodeResult VARCHAR(20),
			@CurrencyId INT

	-------------------------------------------------------------------------------------------------------------------
				
	--Tabla temporal de los detalles
	DECLARE @Details TABLE
	(
		Id INT,
		AccountReceivableId INT,
		SourceAccountId INT,
		TargetAccountId INT,
		Value NUMERIC(18, 2)
	)
	
	--Resultado de la generación del comprobante contable
	DECLARE @resultJournalVoucher TABLE 
	(
		code VARCHAR(20),
		MessageResult VARCHAR(MAX),
		IdJournalVoucher INT
	)

	-------------------------------------------------------------------------------------------------------------------

	BEGIN TRY
		--Se obtienen los datos de la cabecera
		SELECT	@OperatingUnitId = t.x.value('OperatingUnitId[1]','int'),
				@Code = t.x.value('Code[1]','varchar(20)'),
				@DocumentDate = t.x.value('DocumentDate[1]','datetime'),
				@DocumentType = t.x.value('DocumentType[1]','tinyint'),
				@Message = t.x.value('Detail[1]','varchar(max)'),
				@Status = t.x.value('Status[1]','tinyint'),
				@EntityId = t.x.value('EntityId[1]','int'),
				@EntityCode = t.x.value('EntityCode[1]','varchar(20)'),
				@EntityName = t.x.value('EntityName[1]','varchar(250)'),
				@CurrencyId = t.x.value('CurrencyId[1]','int')
		FROM @PortfolioReclassificationXml.nodes('/PortfolioReclassification') t(x)

		IF EXISTS (SELECT 1 FROM Portfolio.PortfolioReclassification WHERE Code = @Code AND Status <> 1)
		BEGIN
			SELECT @CodeResult = 999, 
				   @MessageResult = 'La Reclasificación de Documentos de Cartera se encuentra en estado: ' + IIF(Status = 2, 'Confirmado', 'Anulado')
			FROM Portfolio.PortfolioReclassification
			WHERE Code = @Code
			RETURN
		END

		IF @CurrencyId is null or @CurrencyId = 0 BEGIN
			SELECT @CurrencyId = cs.OfficialCurrencyId
			FROM GeneralLedger.CompanySettings cs WITH(NOLOCK)
		END
		
		IF @Status = 3
		BEGIN
			UPDATE [Portfolio].[PortfolioReclassification]
				SET [Status] = @Status,
					[ModificationUser] = @CodeUser,
					[ModificationDate] = [Common].[GETDATE](),
					[AnnulmentUser] = @CodeUser,
					[AnnulmentDate] = [Common].[GETDATE]()
			WHERE Code = @Code
		END
		ELSE
		BEGIN
			--Se obtiene los detalles que vienen en el xml
			INSERT INTO @Details
				SELECT	t.x.value('Id[1]','int'),
						t.x.value('AccountReceivableId[1]','int'),
						t.x.value('SourceAccountId[1]','int'),
						t.x.value('TargetAccountId[1]','int'),
						t.x.value('Value[1]','decimal(18,2)')
				FROM @PortfolioReclassificationXml.nodes('/PortfolioReclassification/PortfolioReclassificationDetail') t(x)

			-- Se obtiene el libro contable oficial
			SELECT @LegalBookId = Id 
			FROM GeneralLedger.LegalBook 
			WHERE OfficialBook = 1

			-- Se obtiene el tipo de comprobante contable
			SELECT @JournalVoucherTypeId =	CASE @DocumentType
												WHEN 1 THEN JournalVoucherTypeFilingAccountId
												WHEN 6 THEN JournalVoucherTypeHardCollectionId
											END
			FROM [Portfolio].[SettingPortfolio]
			WHERE OperatingUnitId = @OperatingUnitId 

			/*************************************VALIDACIONES************************************/

			--- Valido que existan detalles
			IF NOT EXISTS (SELECT 1 FROM @Details)
			BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = 'La Reclasificación de Documentos de Cartera no tiene detalles.'
				RETURN
			END

			--- Valido que existan detalles
			IF EXISTS (SELECT 1 FROM @Details WHERE Value <= 0)
			BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = 'Existen detalles de la Reclasificación de Documentos de Cartera con valor 0 o negativo.'
				RETURN
			END

			----- Valido que la cuenta origen no sea la misma cuenta destino
			--IF EXISTS 
			--(
			--	SELECT 1 
			--	FROM @Details d 			
			--	WHERE d.SourceAccountId = d.TargetAccountId
			--)
			--BEGIN
			--	SELECT @Message = STUFF((
			--			SELECT DISTINCT CHAR(13) + CHAR(10) + ' - Factura ' + ar.InvoiceNumber
			--			FROM @Details d
			--			JOIN Portfolio.AccountReceivable ar ON d.AccountReceivableId = ar.Id
			--			WHERE d.SourceAccountId = d.TargetAccountId
			--			FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			--	SELECT	@CodeResult = 999, 
			--			@MessageResult = 'La Reclasificación de Documentos de Cartera no es posible debido a que las siguientes facturas tienen la misma cuenta contable (origen y destino): ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
			--	RETURN
			--END

			--- Valido que la cuenta origen no sea la misma de acuerdo al estado de la factura
			IF EXISTS 
			(
				SELECT 1 
				FROM @Details d
				LEFT JOIN
				(
					SELECT ara.AccountReceivableId, MAX(ara.Id) Id
					FROM Portfolio.AccountReceivableAccounting ara
					WHERE ara.Balance <> 0
					GROUP BY ara.AccountReceivableId
				) aram ON d.AccountReceivableId = aram.AccountReceivableId
				LEFT JOIN Portfolio.AccountReceivableAccounting ara ON aram.Id = ara.Id
				WHERE d.SourceAccountId <> ISNULL(ara.MainAccountId, 0)
			)
			BEGIN
				SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - Factura ' + ar.InvoiceNumber
						FROM @Details d
						LEFT JOIN Portfolio.AccountReceivable ar ON d.AccountReceivableId = ar.Id
						LEFT JOIN
						(
							SELECT ara.AccountReceivableId, MAX(ara.Id) Id
							FROM Portfolio.AccountReceivableAccounting ara
							WHERE ara.Balance <> 0
							GROUP BY ara.AccountReceivableId
						) aram ON d.AccountReceivableId = aram.AccountReceivableId
						LEFT JOIN Portfolio.AccountReceivableAccounting ara ON aram.Id = ara.Id
						WHERE d.SourceAccountId <> ISNULL(ara.MainAccountId, 0)
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT	@CodeResult = 999, 
						@MessageResult = 'La Reclasificación de Documentos de Cartera no es posible debido a que las siguientes facturas tienen una cuenta contable origen distinta a la actual de la factura: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
				RETURN
			END

			--- El saldo de la factura no coincide con el saldo de la cuenta origen
			IF EXISTS 
			(
				SELECT 1 
				FROM @Details d
				LEFT JOIN
				(
					SELECT ara.AccountReceivableId, MAX(ara.Id) Id
					FROM Portfolio.AccountReceivableAccounting ara
					WHERE ara.Balance <> 0
					GROUP BY ara.AccountReceivableId
				) aram ON d.AccountReceivableId = aram.AccountReceivableId
				LEFT JOIN Portfolio.AccountReceivableAccounting ara ON aram.Id = ara.Id
				WHERE d.Value <> ISNULL(ara.Balance, 0)
			)
			BEGIN
				SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - Factura ' + ar.InvoiceNumber
						FROM @Details d
						LEFT JOIN Portfolio.AccountReceivable ar ON d.AccountReceivableId = ar.Id
						LEFT JOIN
						(
							SELECT ara.AccountReceivableId, MAX(ara.Id) Id
							FROM Portfolio.AccountReceivableAccounting ara
							WHERE ara.Balance <> 0
							GROUP BY ara.AccountReceivableId
						) aram ON d.AccountReceivableId = aram.AccountReceivableId
						LEFT JOIN Portfolio.AccountReceivableAccounting ara ON aram.Id = ara.Id
						WHERE d.Value <> ISNULL(ara.Balance, 0)
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT	@CodeResult = 999, 
						@MessageResult = 'La Reclasificación de Documentos de Cartera no es posible debido a que el saldo de las siguientes facturas no es el mismo de la cuenta contable actual de la factura: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
				RETURN
			END

			IF @LegalBookId IS NULL
			BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = 'No se encontro libro oficial en contabilidad'
				RETURN 
			END
			
			IF @JournalVoucherTypeId IS NULL
			BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = 'No se encuentra parametrizado el tipo de comprobante para la Reclasificación de Documentos de Cartera'
				RETURN
			END

			/*************************************************************************************/

			DECLARE @ConfirmationUser VARCHAR(20) = CASE WHEN @Status = 2 THEN @CodeUser ELSE NULL END
			DECLARE @ConfirmationDate DATETIME = CASE WHEN @Status = 2 THEN [Common].[GETDATE]() ELSE NULL END

			IF ISNULL(@Code, '') = ''
			BEGIN
				--Si se esta insertando por primera vez se consulta la secuencia numerica
				DECLARE @IsManual BIT
				
				EXEC Common.SP_GetSequence 160, @IdForm, @OperatingUnitId, NULL, NULL, @IsManual OUT, @Code OUT, @Code_Output OUT, @Message_Output OUT

				IF @Code_Output <> 0
				BEGIN
					SELECT	@CodeResult = 999, 
							@MessageResult = REPLACE(@Message_Output, '{0}', 'Reclasificación de Documentos de Cartera')
					RETURN
				END

				--Validar que no exista ya un documento con el mismo codigo
				IF EXISTS (SELECT 1 FROM Portfolio.PortfolioReclassification WHERE Code = @Code)
				BEGIN
					SELECT @CodeResult = 999, 
						   @MessageResult = 'Ya existe un documento de Reclasificación de Documentos de Cartera con codigo: ' + @Code
					RETURN
				END

				--Se inserta la cabecera
				INSERT INTO [Portfolio].[PortfolioReclassification]
				(
					[Code],[DocumentType],[DocumentDate],[AccountReceivableId],[SourceAccountId],[TargetAccountId],[Value],
					[Status],[CreationUser],[CreationDate],[ModificationUser],[ModificationDate],[ConfirmationUser],[ConfirmationDate],
					[EntityId],[EntityCode],[EntityName]
				)
				SELECT @Code,@DocumentType,@DocumentDate,d.AccountReceivableId,d.SourceAccountId,d.TargetAccountId,d.Value,
					@Status,@CodeUser,[Common].[GETDATE](),@ConfirmationUser,@ConfirmationDate,@ConfirmationUser,@ConfirmationDate,
					@EntityId,@EntityCode,@EntityName
				FROM @Details d

				--Obtengo el id de la cabcera
				SET @Id = SCOPE_IDENTITY()
			END
			ELSE --Si se esta actualizando
			BEGIN
				UPDATE pr
					SET [DocumentType] = @DocumentType,
						[DocumentDate] = @DocumentDate,
						[AccountReceivableId] = d.AccountReceivableId,
						[SourceAccountId] = d.SourceAccountId,
						[TargetAccountId] = d.TargetAccountId,
						[Value] = d.Value,
						[Status] = @Status,
						[ModificationUser] = @CodeUser,
						[ModificationDate] = [Common].[GETDATE](),
						[ConfirmationUser] = @ConfirmationUser,
						[ConfirmationDate] = @ConfirmationDate
				FROM [Portfolio].[PortfolioReclassification] pr
				JOIN @Details d On pr.Id = d.Id
				WHERE pr.Code = @Code

				--Obtengo el id de la cabcera
				SELECT @Id = pr.Id
				FROM [Portfolio].[PortfolioReclassification] pr
				WHERE pr.Code = @Code
			END

			/*************************************************************************************/

			IF @Status = 2
			BEGIN
				SELECT @SubXml = CONVERT
				(
					XML, 
					(
						SELECT *
						FROM 
						(
							SELECT	0 Id,
									0 Consecutive,
									@LegalBookId LegalBookId, 
									@JournalVoucherTypeId IdJournalVoucher, 									
									@DocumentDate VoucherDate, 
									'False' Imported,
									2 Status,
									@Message Detail, 
									'PortfolioReclassification' EntityName,
									@Code EntityCode,
									@Id EntityId,
									0 IsClosedYear,
									@CurrencyId as CurrencyId
						) JournalVoucher
						JOIN
						( 
							SELECT	0 Id,
									0 IdAccounting,
									ma.Id IdMainAccount,
									IIF(ma.HandlesThirdParty = 1, ar.ThirdPartyId, NULL) IdThirdParty, 
									IIF(ma.HandlesCostCenter = 1, ar.CostCenterId, NULL) IdCostCenter, 									
									SUM( prd.Value) DebitValue, 
									0 CreditValue,
									NULL Detail,
									NULL IdRetention, 
									NULL RetentionRate,
									NULL BaseValue,
									NULL BillingValue
							FROM Portfolio.AccountReceivable ar
							JOIN Portfolio.PortfolioReclassification prd ON ar.Id = prd.AccountReceivableId
							JOIN GeneralLedger.MainAccounts ma ON prd.TargetAccountId = ma.Id
							WHERE prd.Code = @Code
							GROUP BY ma.Id, IIF(ma.HandlesThirdParty = 1, ar.ThirdPartyId, NULL), IIF(ma.HandlesCostCenter = 1, ar.CostCenterId, NULL)

							UNION ALL

							SELECT	0 Id,
									0 IdAccounting,
									ma.Id IdMainAccount,
									IIF(ma.HandlesThirdParty = 1, ar.ThirdPartyId, NULL) IdThirdParty, 
									IIF(ma.HandlesCostCenter = 1, ar.CostCenterId, NULL) IdCostCenter, 									
									0 DebitValue, 
									SUM( prd.Value) CreditValue,
									NULL Detail,
									NULL IdRetention, 
									NULL RetentionRate,
									NULL BaseValue,
									NULL BillingValue
							FROM Portfolio.AccountReceivable ar
							JOIN Portfolio.PortfolioReclassification prd ON ar.Id = prd.AccountReceivableId
							JOIN GeneralLedger.MainAccounts ma ON  prd.SourceAccountId = ma.Id
							WHERE prd.Code = @Code
							GROUP BY ma.Id, IIF(ma.HandlesThirdParty = 1, ar.ThirdPartyId, NULL), IIF(ma.HandlesCostCenter = 1, ar.CostCenterId, NULL)

						) JournalVoucherDetail ON JournalVoucher.Id = JournalVoucherDetail.IdAccounting
						For xml AUTO,TYPE, ELEMENTS
					)
				)

				INSERT @resultJournalVoucher 
					EXEC GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @SubXml, @CodeUser

				SELECT	@JournalVoucherCodeResult = r.Code,				
						@Message_Output = r.MessageResult,
						----------------------------------------------
						@Message = 'Se guardó y confirmó el comprobante contable ' + CAST(jv.Consecutive AS VARCHAR(20)) + ' de tipo ' + jvt.Code + ' - ' + jvt.Name
				FROM @resultJournalVoucher r
				LEFT JOIN GeneralLedger.JournalVouchers jv ON r.IdJournalVoucher = jv.Id
				LEFT JOIN GeneralLedger.JournalVoucherTypes jvt ON jv.IdJournalVoucher = jvt.Id

				IF ISNULL(@JournalVoucherCodeResult, '999') <> '0' 
				BEGIN
					SELECT	@CodeResult = 999, 
							@MessageResult = ISNULL(@Message_Output, 'Comprobante contable no generado')
					RETURN 
				END

				/*************************************************************************************/

				UPDATE Portfolio.AccountReceivableAccounting 
					SET Balance = 0 
				FROM Portfolio.PortfolioReclassification prd
				JOIN Portfolio.AccountReceivable ar ON prd.AccountReceivableId = ar.Id
				JOIN Portfolio.AccountReceivableAccounting ara ON ar.Id = ara.AccountReceivableId AND prd.SourceAccountId = ara.MainAccountId
				WHERE prd.Code = @Code
		
				UPDATE ara
					SET ara.Balance = prd.Value,
						ara.[Value] = IIF(ar.Value <> ar.Balance,prd.[Value],ara.[Value])
				FROM Portfolio.PortfolioReclassification prd
				JOIN Portfolio.AccountReceivable ar ON prd.AccountReceivableId = ar.Id
				JOIN Portfolio.AccountReceivableAccounting ara ON ar.Id = ara.AccountReceivableId AND prd.TargetAccountId = ara.MainAccountId
				WHERE prd.Code = @Code

				INSERT INTO Portfolio.AccountReceivableAccounting
				(
					AccountReceivableId, MainAccountId, ThirdPartyId, CostCenterId, Value, Balance
				)
				SELECT ar.Id, prd.TargetAccountId, ar.ThirdPartyId, ar.CostCenterId, prd.Value, prd.Value
				FROM Portfolio.PortfolioReclassification prd
				JOIN Portfolio.AccountReceivable ar ON prd.AccountReceivableId = ar.Id
				LEFT JOIN Portfolio.AccountReceivableAccounting ara ON ar.Id = ara.AccountReceivableId AND prd.TargetAccountId = ara.MainAccountId
				WHERE prd.Code = @Code AND ara.Id IS NULL
			END
		END

		SELECT @CodeResult = 0, 
			   @MessageResult = CASE @Status
				   WHEN 2 THEN CONCAT('Se guardó y confirmó la Reclasificación de Documentos de Cartera con código ', @Code)
				   WHEN 3 THEN CONCAT('Se anuló la Reclasificación de Documentos de Cartera con código ', @Code)
				   ELSE CONCAT('Se guardó la Reclasificación de Documentos de Cartera con código ', @Code)
			   END + IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10) + ISNULL(@Message, ''))
	END TRY
	BEGIN CATCH
		SELECT @CodeResult = 999, 
			   @MessageResult = ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10))
	END CATCH
END