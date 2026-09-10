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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que realiza la homologación de un comprobante contable desde el libro oficial hacia otro libro contable (por ejemplo, en moneda extranjera). Recibe el comprobante en formato XML junto con el identificador del libro destino, valida que la moneda de la transacción y del libro estén definidas y con tasas TRM disponibles, y convierte los valores de débito, crédito y base utilizando el convertidor de monedas. Genera un nuevo comprobante contable (JournalVoucher) con sus detalles en el libro de homologación, garantizando que no existan duplicados y que el origen sea siempre el libro oficial. Si ocurre algún error de validación, retorna un mensaje de error con código descriptivo en lugar de insertar el comprobante.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_HomologationJournalVoucher';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_HomologationJournalVoucher';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Homologa un comprobante contable del libro oficial a otro libro contable, convirtiendo valores a la moneda destino y mapeando las cuentas mediante la tabla de homologación, devolviendo el detalle homologado o mensajes de error.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_HomologationJournalVoucher';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un libro oficial activo (OfficialBook=1, Status=1) en GeneralLedger.LegalBook.; El libro destino de homologación debe tener una moneda oficial definida (OfficialCurrencyId).; Si la moneda de la transacción difiere de la del libro destino, deben existir registros en Common.TRM para ambas monedas.; No debe existir previamente un comprobante homologado para el mismo AccountingMovementId y libro destino.; El libro origen indicado en el XML debe ser el libro oficial.; El comprobante debe corresponder con el movimiento contable (Id y AccountingMovementId coincidentes en JournalVouchers).; El comprobante origen debe tener líneas de detalle.; Todas las cuentas del detalle deben estar homologadas exactamente una vez en GeneralLedger.HomologationAccount entre el libro oficial y el libro destino.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_HomologationJournalVoucher';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se permite homologar desde el libro oficial activo (OfficialBook=1, Status=1) hacia otro libro.; Los valores monetarios (DebitValue, CreditValue, BaseValue, BillingValue) siempre se convierten desde la moneda de la transacción a la moneda oficial del libro destino vía Common.CurrencyConverter.; Cada cuenta contable del detalle debe tener exactamente una homologación (no cero, no múltiple) para poder homologar el comprobante.; Nunca se persisten cambios en tablas físicas: el procedimiento solo lee y devuelve un resultset (los inserts son sobre variables tabla).; No se permite homologar dos veces el mismo movimiento contable hacia el mismo libro destino.; Los errores se devuelven como filas con CodeMessage=''999'' en lugar de lanzar excepciones (incluso los del CATCH).', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_HomologationJournalVoucher';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] @TableDetailHomologation: Siempre retorna el contenido de la tabla de homologación, ya sea con los detalles homologados (CodeMessage=''0'') o con mensajes de error (CodeMessage=''999'').; [INSERT] @TableDetailHomologation: Cuando no hay moneda en la transacción o en el libro contable, inserta mensaje de error ''999'' indicando ausencia de moneda.; [INSERT] @TableDetailHomologation: Cuando la moneda de la transacción difiere de la del libro y falta TRM para alguna, inserta mensaje ''999'' por falta de TRM.; [INSERT] @TableDetailHomologation: Cuando ya existe un comprobante homologado (mismo AccountingMovementId y LegalBookId destino), inserta mensaje ''999'' indicando duplicado.; [INSERT] @TableDetailHomologation: Cuando el libro origen del XML no coincide con el libro oficial, inserta mensaje ''999'' rechazando la homologación.; [INSERT] @TableDetailHomologation: Cuando el comprobante (Id) no corresponde al movimiento contable, inserta mensaje ''999''.; [INSERT] @TableDetailHomologation: Cuando el comprobante no tiene detalles, inserta mensaje ''999'' indicando ausencia de detalles.; [INSERT] @TableDetailHomologation: Cuando hay cuentas del detalle sin homólogo en HomologationAccount, inserta mensaje ''999'' listando cada cuenta no homologada.; [INSERT] @TableDetailHomologation: Cuando alguna cuenta del detalle tiene más de un homólogo (COUNT>1), inserta mensaje ''999'' listando las cuentas con homologación múltiple.; [INSERT] @TableDetailHomologation: En el flujo exitoso, inserta una fila por cada detalle con la cuenta homologada, valores convertidos a la moneda del libro destino mediante Common.CurrencyConverter, datos de tercero y centro de costo, con CodeMessage=''0''.; [INSERT] @TableDetailHomologation: Cuando ocurre una excepción en el TRY, inserta mensaje ''999'' con ERROR_MESSAGE() y línea del error.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_HomologationJournalVoucher';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @CurrencyId IS NULL o 0 → Toma por defecto la moneda oficial del sistema desde CompanySettings.; si @CurrencyId IS NULL o @BookCurrencyId IS NULL → Aborta con error ''999'' por moneda no definida. else Continúa validaciones.; si @CurrencyId <> @BookCurrencyId y falta TRM para alguna → Aborta con error ''999'' por falta de TRM.; si Ya existe comprobante homologado para el mismo movimiento y libro destino → Aborta con error ''999'' por duplicado.; si @LegalBookId del XML <> @OfficialLegalBookId → Aborta con error ''999'' porque el origen no es el libro oficial.; si No existe el comprobante con ese Id y AccountingMovementId → Aborta con error ''999'' por inconsistencia comprobante/movimiento.; si @TableDetail está vacía → Aborta con error ''999'' por falta de detalles.; si Existen detalles sin homologación de cuenta → Aborta con error ''999'' listando cuentas no homologadas.; si Existen detalles con más de una homologación de cuenta → Aborta con error ''999'' listando cuentas con homólogos múltiples.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_HomologationJournalVoucher';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.CurrencyConverter', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_HomologationJournalVoucher';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.LegalBook; GeneralLedger.CompanySettings; Common.TRM; GeneralLedger.JournalVouchers; GeneralLedger.JournalVoucherDetails; GeneralLedger.HomologationAccount; GeneralLedger.MainAccounts; Common.ThirdParty; Payroll.CostCenter', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_HomologationJournalVoucher';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_HomologationJournalVoucher';
-- GO
