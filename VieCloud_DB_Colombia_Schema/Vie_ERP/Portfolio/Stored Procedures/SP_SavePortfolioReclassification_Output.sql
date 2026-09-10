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
    WHERE CAST(d.Value AS NUMERIC(18,2)) <> ROUND(ISNULL(ara.Balance, 0), 2)  -- ✅ CORREGIDO
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
            WHERE CAST(d.Value AS NUMERIC(18,2)) <> ROUND(ISNULL(ara.Balance, 0), 2)  -- ✅ CORREGIDO
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que guarda y confirma (o anula) una reclasificación de documentos de cartera: traslada el saldo de cuentas por cobrar desde una cuenta contable de origen hacia una cuenta contable destino, generando el comprobante contable correspondiente según el tipo de documento (radicación de cuentas o cobro jurídico). Recibe los datos de cabecera y detalles en formato XML, valida que la reclasificación no esté ya confirmada o anulada, que los detalles tengan valores positivos y que las cuentas origen coincidan con el estado contable actual de cada cuenta por cobrar (consultando Portfolio.AccountReceivableAccounting). Utiliza la configuración de cartera (Portfolio.SettingPortfolio) para determinar el tipo de comprobante contable, el libro contable oficial (GeneralLedger.LegalBook) y la moneda oficial de la empresa (GeneralLedger.CompanySettings); si la operación es una anulación, actualiza el estado, el usuario y la fecha de anulación en Portfolio.PortfolioReclassification.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_SavePortfolioReclassification_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_SavePortfolioReclassification_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Guarda, confirma o anula una Reclasificación de Documentos de Cartera, validando saldos y cuentas, generando el comprobante contable y actualizando los saldos contables de las cuentas por cobrar.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SavePortfolioReclassification_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe contener el nodo /PortfolioReclassification con la cabecera y al menos un nodo PortfolioReclassificationDetail.; Si el documento ya existe (Code), su Status debe ser distinto de 2 (Confirmado) y 3 (Anulado); es decir, sólo se permite operar sobre documentos en estado 1 (no confirmado).; Debe existir un libro contable oficial en GeneralLedger.LegalBook (OfficialBook = 1).; Debe existir parametrización en Portfolio.SettingPortfolio para el OperatingUnitId, con JournalVoucherTypeFilingAccountId si DocumentType=1 o JournalVoucherTypeHardCollectionId si DocumentType=6.; Cada detalle debe tener Value > 0.; La SourceAccountId de cada detalle debe coincidir con la MainAccountId vigente (último AccountReceivableAccounting con Balance<>0) de la AccountReceivable.; El Value de cada detalle debe ser igual al Balance vigente de la cuenta contable origen de la factura.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SavePortfolioReclassification_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Portfolio.PortfolioReclassification: Cuando @Status = 3 (anulación) se actualiza Status, ModificationUser/Date y AnnulmentUser/Date para el documento con el Code recibido.; [INSERT] Portfolio.PortfolioReclassification: Cuando el Code llega vacío y no es anulación, se obtiene el consecutivo vía Common.SP_GetSequence (form 1529, módulo 160) y se inserta una fila por cada detalle del XML con la cabecera replicada; si Status=2 también se llenan ConfirmationUser y ConfirmationDate.; [UPDATE] Portfolio.PortfolioReclassification: Cuando ya existe Code, se actualizan DocumentType, DocumentDate, AccountReceivableId, SourceAccountId, TargetAccountId, Value, Status, ModificationUser/Date y, si Status=2, ConfirmationUser/Date, emparejando por Id de detalle.; [EXECUTE] GeneralLedger.JournalVouchers: Cuando @Status = 2 se construye un XML de comprobante (débito en TargetAccountId, crédito en SourceAccountId, agrupando por cuenta/tercero/centro de costo según HandlesThirdParty/HandlesCostCenter) y se invoca GeneralLedger.SP_CreateAndValidateJournalVoucherMovement; si su resultado no es ''0'' se retorna error 999.; [UPDATE] Portfolio.AccountReceivableAccounting: Tras confirmar (Status=2), se pone Balance=0 en los registros cuya MainAccountId coincide con SourceAccountId de la reclasificación.; [UPDATE] Portfolio.AccountReceivableAccounting: Tras confirmar (Status=2), para los registros cuya MainAccountId coincide con TargetAccountId, se asigna Balance = Value reclasificado y Value = Value reclasificado sólo si la factura tenía Value <> Balance.; [INSERT] Portfolio.AccountReceivableAccounting: Tras confirmar (Status=2), si no existe registro contable previo para la AccountReceivable con MainAccountId = TargetAccountId, se inserta uno con el tercero/centro de costo de la cuenta por cobrar y Value = Balance = Value reclasificado.; [RETURN_RESULT] @CodeResult/@MessageResult: Devuelve CodeResult=0 con mensaje según Status (guardado, guardado y confirmado, o anulado) y concatena el detalle del comprobante generado; ante cualquier validación fallida o excepción retorna CodeResult=999 con el mensaje correspondiente.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SavePortfolioReclassification_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe PortfolioReclassification con el Code y Status <> 1 → Retorna 999 indicando que está Confirmado (Status=2) o Anulado (Status=3).; si @CurrencyId es null o 0 → Toma OfficialCurrencyId de GeneralLedger.CompanySettings.; si @Status = 3 → Sólo actualiza la cabecera marcándola como anulada (Status, AnnulmentUser, AnnulmentDate). else Procesa detalles, valida y, si Status=2, genera comprobante contable y reasigna saldos contables.; si @DocumentType = 1 → Usa JournalVoucherTypeFilingAccountId de Portfolio.SettingPortfolio. else Si DocumentType = 6 usa JournalVoucherTypeHardCollectionId.; si ISNULL(@Code,'''') = '''' → Genera consecutivo con Common.SP_GetSequence e inserta cabecera nueva. else Actualiza la cabecera existente uniéndose por Id de detalle.; si @Status = 2 (Confirmar) → Construye XML del comprobante, ejecuta GeneralLedger.SP_CreateAndValidateJournalVoucherMovement y reajusta Portfolio.AccountReceivableAccounting (cierre de cuenta origen y apertura/actualización de cuenta destino).; si Resultado del comprobante contable distinto de ''0'' → Retorna 999 con el mensaje del SP de comprobantes o ''Comprobante contable no generado''.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SavePortfolioReclassification_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SavePortfolioReclassification_Output';
-- GO
