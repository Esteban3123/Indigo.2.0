-- =============================================
-- Author:		Cristhian Mauricio Salazar Narvaez
-- Create date: 27-10-2015
-- Description:	Procedimiento el cual se encarga de realizar el comprobante contable y la acumulacion
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_HomologationJournalVoucher]
	@JournalVoucherXml as Xml,
	@HomologationLegalBookId int
AS
BEGIN
	SET NOCOUNT ON

	/*************************************************** VARIABLES ***************************************************/

	DECLARE @IdJournalVoucher INT, 
			@AccountingMovementId INT, 
			@LegalBookId INT,
			---------------------------------------------------
			@OfficialLegalBookId INT,
			@CurrencyId INT,
			@BookCurrencyId INT,
			@Message Varchar(Max)

	--Tabla para almacenar los detalles del comprobante
	DECLARE @TableDetail TABLE
	(
		RowId Int IDENTITY(1,1),
		---------------------------------------------------
		IdMainAccount INT, 		
		IdThirdParty INT, 
		IdCostCenter INT, 		
		DebitValue DECIMAL(20, 4), 
		CreditValue DECIMAL(20, 4), 
		Detail VARCHAR(MAX), 
		IdRetention INT, 
		RetentionRate DECIMAL(6, 3), 
		BaseValue DECIMAL(18, 2),
		BillingValue DECIMAL(18, 2)		
	)

	--Tabla para el registro de la homologacion
	DECLARE @TableDetailHomologation TABLE
	(	
		TableDetailRowId INT,
		---------------------------------------------------
		MainAccountId INT DEFAULT(0), 
		Number VARCHAR(50),
		NameAccount VARCHAR(500),
		IdThirdParty INT, 
		NameThirdParty VARCHAR(500),
		IdCostCenter INT, 
		NameCostCenter VARCHAR(500),
		DebitValue DECIMAL(20, 4), 
		CreditValue DECIMAL(20, 4), 
		Detail varchar(MAX), 
		IdRetention INT, 
		RetentionRate DECIMAL(6, 3), 
		BaseValue DECIMAL(18, 2),
		BillingValue DECIMAL(18, 2),
		---------------------------------------------------
		CodeMessage VARCHAR(20),
		Message VARCHAR(MAX)
	)

	BEGIN TRY
		--Se obtienen los datos de la cabecera
		SELECT	@IdJournalVoucher = ISNULL(t.x.value('Id[1]','int'),0),
				@AccountingMovementId = ISNULL(t.x.value('AccountingMovementId[1]','int'), 0),
				@LegalBookId = t.x.value('LegalBookId[1]','int'),
				@CurrencyId =  t.x.value('CurrencyId[1]','int')
		FROM @JournalVoucherXml.nodes('/JournalVoucher') t(x)

		--Se consulta el libro oficial
		SELECT @OfficialLegalBookId = Id 
		FROM GeneralLedger.LegalBook 
		WHERE OfficialBook = 1 AND Status = 1

		--Se establece la moneda del Libro 
		SET @BookCurrencyId =(SELECT OfficialCurrencyId from GeneralLedger.LegalBook where Id=@HomologationLegalBookId)

		--Si la Moneda No llega en la cabecera se toma por defecto la moneda oficial del sistema
		IF @CurrencyId IS NULL or @CurrencyId=0
		BEGIN
			SELECT top 1 @CurrencyId = cs.OfficialCurrencyId
			FROM GeneralLedger.CompanySettings cs WITH(NOLOCK)
		END

		--Valido que haya una moneda para la transaccion y que el libro tenga una moneda definida
		IF @CurrencyId IS NULL OR @BookCurrencyId IS NULL
		BEGIN
			INSERT INTO @TableDetailHomologation (CodeMessage, Message)
				SELECT	'999' as CodeMessage,
						CONCAT('El comprobante contable no se puede crear debido a que no hay moneda definida en',IIF(@CurrencyId is null, 'la Transacción','el Libro contable')) as Message
			GOTO Branch_Result
		END

		--Valido que si la moneda de la transaccion es diferente a la del libro tenga registros en la tabla TRM
		IF @CurrencyId <> @BookCurrencyId AND (NOT EXISTS(SELECT 1 FROM Common.TRM WHERE CurrencyId =@CurrencyId) OR NOT EXISTS(SELECT 1 FROM Common.TRM WHERE CurrencyId =@BookCurrencyId))
		BEGIN
			INSERT INTO @TableDetailHomologation (CodeMessage, Message)
				SELECT	'999' as CodeMessage,
						CONCAT('El comprobante contable no se puede crear debido No hay datos del TRM de la moneda',IIF(@CurrencyId is null, ' de la Transacción',' del Libro contable')) as Message
			GOTO Branch_Result
		END

		--Valido que no se este duplicando el registro
		IF EXISTS (SELECT 1 FROM GeneralLedger.JournalVouchers WHERE AccountingMovementId = @AccountingMovementId AND LegalBookId = @HomologationLegalBookId)
		BEGIN
			INSERT INTO @TableDetailHomologation (CodeMessage, Message)
				SELECT	'999' as CodeMessage,
						'No se puede realizar la homologacion ya que ya existe un comprobante contable homologado' as Message

			GOTO Branch_Result
		END

		--Valido que el documento origen sea el libro oficial
		IF ISNULL(@LegalBookId, 0) <> ISNULL(@OfficialLegalBookId, 0)
		BEGIN
			INSERT INTO @TableDetailHomologation (CodeMessage, Message)
				SELECT	'999' as CodeMessage,
						'No se puede realizar la homologacion ya que el libro origen no es el libro oficial' as Message

			GOTO Branch_Result
		END

		--Valido que el comprobante corresponda con el movimiento
		IF NOT EXISTS (SELECT 1 FROM GeneralLedger.JournalVouchers WHERE Id = @IdJournalVoucher AND AccountingMovementId = @AccountingMovementId)
		BEGIN
			INSERT INTO @TableDetailHomologation (CodeMessage, Message)
				SELECT	'999' as CodeMessage,
						'El comprobante contable no corresponde con el movimiento contable' as Message

			GOTO Branch_Result
		END

		--Se obtiene los detalles que vienen en el xml
		INSERT INTO @TableDetail
			SELECT	jvd.IdMainAccount,
					jvd.IdThirdParty,
					jvd.IdCostCenter,
					[Common].[CurrencyConverter](ISNULL(jvd.DebitValue, 0),@CurrencyId,@BookCurrencyId) AS DebitValue,
					[Common].[CurrencyConverter](ISNULL(jvd.CreditValue, 0),@CurrencyId,@BookCurrencyId) AS CreditValue,
					jvd.Detail,
					jvd.IdRetention,
					jvd.RetentionRate,
					[Common].[CurrencyConverter](jvd.BaseValue,@CurrencyId,@BookCurrencyId) AS BaseValue,
					[Common].[CurrencyConverter](jvd.BillingValue,@CurrencyId,@BookCurrencyId) AS BillingValue
			FROM GeneralLedger.JournalVoucherDetails jvd
			WHERE jvd.IdAccounting = @IdJournalVoucher

		-- Valido que existan detalles
		IF NOT EXISTS (SELECT 1 FROM @TableDetail)
		BEGIN
			INSERT INTO @TableDetailHomologation (CodeMessage, Message)
				SELECT	'999' as CodeMessage,
						'El comprobante contable a homologar no se puede crear ya que no tiene detalles.' as Message

			GOTO Branch_Result
		END

		--- Tabla movimiento con las respectivas cuentas de libros
		INSERT INTO @TableDetailHomologation
			SELECT	td.RowId,
					-----------------------------------
					ha.MainAccountId,
					ha.Number, 
					ha.Name NameAccount,
					td.IdThirdParty,
					CONCAT(tp.Nit, ' - ', tp.Name) NameThirdParty,
					td.IdCostCenter,
					CONCAT(cc.Code, ' - ', cc.Name) NameCostCenter,
					td.DebitValue,
					td.CreditValue,
					td.Detail,
					td.IdRetention,
					td.RetentionRate,
					td.BaseValue,
					td.BillingValue,
					----------------------------------
					'0' CodeMessage,
					NULL Message
			FROM @TableDetail td
			JOIN
			(
					SELECT DISTINCT
						ha.OfficialMainAccountId, ha.MainAccountId, mah.Number, mah.Name
					FROM GeneralLedger.HomologationAccount ha
					JOIN GeneralLedger.MainAccounts ma ON ha.OfficialMainAccountId = ma.Id AND @LegalBookId = ma.LegalBookId
					JOIN GeneralLedger.MainAccounts mah ON ha.MainAccountId = mah.Id AND @HomologationLegalBookId = mah.LegalBookId
			) ha ON td.IdMainAccount = ha.OfficialMainAccountId
			LEFT JOIN Common.ThirdParty tp ON td.IdThirdParty = tp.Id
			LEFT JOIN Payroll.CostCenter cc ON td.IdCostCenter = cc.Id

		-- Valido que todos los detalles hayan sido homologados
		IF EXISTS 
		(
			SELECT 1 
			FROM @TableDetail td
			LEFT JOIN @TableDetailHomologation tdh ON td.RowId = tdh.TableDetailRowId
			WHERE tdh.TableDetailRowId IS NULL
		)
		BEGIN
			SELECT @Message = STUFF((
					SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - Cuenta Contable: ', ma.Number, ' - ', ma.Name)
					FROM @TableDetail td
					LEFT JOIN @TableDetailHomologation tdh ON td.RowId = tdh.TableDetailRowId
					LEFT JOIN GeneralLedger.MainAccounts ma ON td.IdMainAccount = ma.Id
					WHERE tdh.TableDetailRowId IS NULL
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			INSERT INTO @TableDetailHomologation (CodeMessage, Message)
				SELECT	'999' as CodeMessage,
						CONCAT('El comprobante contable no se puede crear ya que existen cuentas contable del detalle que no se encuentran homologadas: ', CHAR(13) + CHAR(10), ISNULL(@Message, '')) as Message
			
			GOTO Branch_Result
		END

		-- Valido que todos los detalles hayan sido homologados solo una vez
		IF EXISTS 
		(
			SELECT 1 
			FROM @TableDetail td
			JOIN @TableDetailHomologation tdh ON td.RowId = tdh.TableDetailRowId
			GROUP BY tdh.TableDetailRowId
			HAVING COUNT(1) > 1
		)
		BEGIN
			SELECT @Message = STUFF((
					SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - Cuenta Contable: ', ma.Number, ' - ', ma.Name)
					FROM 
					(
						SELECT tdh.TableDetailRowId
						FROM @TableDetail td
						JOIN @TableDetailHomologation tdh ON td.RowId = tdh.TableDetailRowId
						GROUP BY tdh.TableDetailRowId
						HAVING COUNT(1) > 1
					) tbm
					LEFT JOIN @TableDetail td ON tbm.TableDetailRowId = td.RowId
					LEFT JOIN GeneralLedger.MainAccounts ma ON td.IdMainAccount = ma.Id
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			INSERT INTO @TableDetailHomologation (CodeMessage, Message)
				SELECT	'999' as CodeMessage,
						CONCAT('El comprobante contable no se puede crear ya que existen cuentas contable del detalle con más de un homologo: ', CHAR(13) + CHAR(10), ISNULL(@Message, '')) as Message
			
			GOTO Branch_Result
		END

	END TRY
	BEGIN CATCH
		INSERT INTO @TableDetailHomologation (CodeMessage, Message)
			SELECT	'999' as CodeMessage,
					ERROR_MESSAGE() + ' Linea ' + cast(ERROR_LINE() as varchar(100)) as Message
	END CATCH

	/****************************************************** ******************************************************/

	Branch_Result:

	SELECT	MainAccountId, Number, NameAccount,
			IdThirdParty, NameThirdParty,
			IdCostCenter, NameCostCenter,
			DebitValue, CreditValue, Detail,
			IdRetention, RetentionRate, BaseValue, BillingValue,
			CodeMessage, Message
	FROM @TableDetailHomologation
END