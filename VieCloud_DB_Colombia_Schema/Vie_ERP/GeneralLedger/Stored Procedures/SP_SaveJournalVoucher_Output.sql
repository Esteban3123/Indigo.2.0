CREATE PROCEDURE [GeneralLedger].[SP_SaveJournalVoucher_Output]
	@JournalVoucherXml as Xml,
	@CodeUser as varchar(20),
	------------------------------------------------------
	@CodeMessage Int Output,
	@Message Varchar(Max) Output,
	@IdJournalVoucherResult Int Output
AS
BEGIN
	--SET NOCOUNT ON

	--/*************************************************** VARIABLES ***************************************************/
	
	--DECLARE @IsHomologation BIT = 0,
	--		@OfficialLegalBookId INT,
	--		------------- Variables para la contabilizacion
	--		@Rows INT, 
	--		@RowId INT,
	--		@MovementLegalBookId INT,
	--		@MovementJournalVoucherId INT,
	--		@MonthMovement INT,
	--		@Message_Output VARCHAR(MAX)

	--DECLARE @IdJournalVoucher INT, 
	--		@AccountingMovementId INT, 
	--		@Consecutive BIGINT, 
	--		@LegalBookId INT, 
	--		@IdJournalVoucherType INT, 
	--		@VoucherDate DATETIME, 
	--		@Imported BIT, 
	--		@Status TINYINT, 
	--		@Detail VARCHAR(MAX), 
	--		@EntityCode VARCHAR(20), 
	--		@EntityId INT, 
	--		@EntityName VARCHAR(250), 
	--		@OriginEntityName VARCHAR(250),
	--		@IsClosedYear TINYINT,
	--		@OperatingUnitId INT,
	--		@ConfirmationUser VARCHAR(20), 
	--		@ConfirmationDate DATETIME,
	--		@CurrencyId INT,
	--		@BookCurrencyId INT,
	--		@DateTRM DATE,
	--		@EntityNameConversion VARCHAR(250)

	----Tabla para almacenar los detalles del comprobante
	--DECLARE @TableDetail TABLE
	--(
	--	RowId Int IDENTITY(1,1),
	--	Id INT, 
	--	IdMainAccount INT, 
	--	IdThirdParty INT, 
	--	IdCostCenter INT, 
	--	DebitValue DECIMAL(21, 5), 
	--	CreditValue DECIMAL(21, 5), 
	--	Detail VARCHAR(MAX), 
	--	IdRetention INT, 
	--	RetentionRate DECIMAL(6, 3), 
	--	BaseValue DECIMAL(18, 2),
	--	BillingValue DECIMAL(18, 2), 
	--	IsDelete BIT,
	--	OriginalDebitValue DECIMAL(21, 5), 
	--	OriginalCreditValue DECIMAL(21, 5),
	--	OriginalBaseValue DECIMAL(18, 2),
	--	OriginalBillingValue DECIMAL(18, 2)
	--)

	----Tabla para saber que libros tengo que afectar
	--DECLARE @TableBookMovement TABLE
	--(
	--	RowId Int IDENTITY(1,1),
	--	LegalBookId INT, 
	--	JournalVoucherId INT
	--)
	
	----Tabla para el registro de la homologacion
	--CREATE TABLE #TableDetailHomologation
	--(	
	--	LegalBookId INT,
	--	TableDetailRowId INT,
	--	---------------------------------------------------
	--	Id INT, 
	--	IdMainAccount INT, 
	--	IdThirdParty INT, 
	--	IdCostCenter INT, 
	--	DebitValue DECIMAL(21, 5), 
	--	CreditValue DECIMAL(21, 5), 
	--	Detail varchar(MAX), 
	--	IdRetention INT, 
	--	RetentionRate DECIMAL(6, 3), 
	--	BaseValue DECIMAL(18, 2),
	--	BillingValue DECIMAL(18, 2)
	--)

	---------------------------------------------------------------------------------------------------------------------

	--BEGIN TRY
	--	--Se obtienen los datos de la cabecera
	--	SELECT	@IdJournalVoucher = ISNULL(t.x.value('Id[1]','int'),0),
	--			@AccountingMovementId = ISNULL(t.x.value('AccountingMovementId[1]','int'), 0),
	--			@Consecutive = t.x.value('Consecutive[1]','bigint'),
	--			@LegalBookId = t.x.value('LegalBookId[1]','int'),				
	--			@IdJournalVoucherType = t.x.value('IdJournalVoucher[1]','int'),
	--			@VoucherDate = t.x.value('VoucherDate[1]','datetime'),		
	--			@Imported = t.x.value('Imported[1]','bit'),
	--			@Status = t.x.value('Status[1]','tinyint'),
	--			@Detail = t.x.value('Detail[1]','varchar(MAX)'),
	--			@EntityCode = t.x.value('EntityCode[1]','varchar(20)'),
	--			@EntityId = t.x.value('EntityId[1]','int'),
	--			@EntityName = ISNULL(t.x.value('EntityName[1]','varchar(250)'), 'JournalVouchers'),
	--			@OriginEntityName = IIF(ISNULL(t.x.value('OriginEntityName[1]', 'varchar(250)'), '') = '', NULL, t.x.value('OriginEntityName[1]', 'varchar(250)')),
	--			@IsClosedYear = t.x.value('IsClosedYear[1]','tinyint'),
	--			@OperatingUnitId = t.x.value('OperatingUnitId[1]','int'),
	--			@CurrencyId =  t.x.value('CurrencyId[1]','INT'),
	--			@DateTRM = t.x.value('DateTRM[1]','DATE')
	--	FROM @JournalVoucherXml.nodes('/JournalVoucher') t(x)
		
	--	-- se establece el entityName para la conversion
	--	set @EntityNameConversion = (CASE 
	--									WHEN @EntityName in ( 'AutomaticPortfolioTransfer','AutomaticPortfolioNote' )then 'Invoice'
	--									WHEN SUBSTRING(@EntityName,0,10) = 'Automatic' THEN 'Invoice'
	--									ELSE @EntityName
	--									END)
	--	set @EntityName =(CASE 
	--						WHEN @EntityName = 'AutomaticPortfolioTransfer' then 'PortfolioTransfer'
	--						WHEN @EntityName = 'AutomaticPortfolioNote' then 'PortfolioNote'
	--						WHEN SUBSTRING(@EntityName,0,10) = 'Automatic' then SUBSTRING(@EntityName,10,241)
	--						ELSE @EntityName
	--						END)

	--print @EntityNameConversion
	--	--Se consulta el libro oficial
	--	SELECT @OfficialLegalBookId = Id 
	--	FROM GeneralLedger.LegalBook WITH (NOLOCK)
	--	WHERE OfficialBook = 1 AND Status = 1

	--	--Si el registro no cuenta con libro, se asigna el libro oficial
	--	IF @LegalBookId IS NULL BEGIN
	--		SELECT @LegalBookId = @OfficialLegalBookId
	--	END
		
	--	/************************************************************************************************************/
	--		--------------------------------- DESACOPLAMIENTO CONTABILIDAD -----------------------------------------------
	--		--IF (@IdJournalVoucher = 0 and @EntityName NOT in ('JournalVouchers', 'PayrollLiquidation','RevenueRecognition','ReverseRevenueRecognition')) BEGIN
				
	--		--	INSERT INTO @TableDetail (	Id,
	--		--								IdMainAccount,
	--		--								IdThirdParty,
	--		--								IdCostCenter,
	--		--								DebitValue,
	--		--								CreditValue,
	--		--								IdRetention,
	--		--								RetentionRate,
	--		--								BaseValue,
	--		--								BillingValue,
	--		--								IsDelete)
	--		--	SELECT	ISNULL(t.x.value('(Id/text())[1]','int'),0) AS Id,
	--		--			t.x.value('(IdMainAccount/text())[1]','int') AS IdMainAccount,
	--		--			IIF(t.x.value('(IdThirdParty/text())[1]','int') = 0, null, t.x.value('IdThirdParty[1]','int')) AS IdThirdParty,
	--		--			IIF(t.x.value('(IdCostCenter/text())[1]','int') = 0, null, t.x.value('IdCostCenter[1]','int')) AS IdCostCenter,
	--		--			ISNULL(t.x.value('(DebitValue/text())[1]','decimal(21, 5)'), 0) AS DebitValue,
	--		--			ISNULL(t.x.value('(CreditValue/text())[1]','decimal(21, 5)'), 0) AS CreditValue,
	--		--			t.x.value('(IdRetention/text())[1]','int') AS IdRetention,
	--		--			t.x.value('(RetentionRate/text())[1]','decimal(6, 3)') AS RetentionRate,
	--		--			t.x.value('(BaseValue/text())[1]','decimal(18, 2)') AS BaseValue,
	--		--			t.x.value('(BillingValue/text())[1]','decimal(18, 2)') AS BillingValue,
	--		--			ISNULL(t.x.value('(IsDelete/text())[1]','bit'),0) AS IsDelete
	--		--	FROM @JournalVoucherXml.nodes('/JournalVoucher/JournalVoucherDetail') t(x)
	--		--	WHERE @Status <> 4

	--		--	/*****periodo contable****/
				
	--		--	--Valido que el año este abierto
	--		--	IF EXISTS 
	--		--	(
	--		--		SELECT *
	--		--		FROM GeneralLedger.LegalBook lb WITH (NOLOCK)
	--		--		WHERE lb.OfficialBook=1 AND YEAR(@VoucherDate) <= LastYearClose
	--		--	) 
	--		--	BEGIN
	--		--		SELECT	@CodeMessage = 999,
	--		--				@Message = CONCAT('El comprobante contable no se puede crear ya que el año ', YEAR(@VoucherDate), ' se encuentra cerrado'),
	--		--				@IdJournalVoucherResult = 0
	--		--		RETURN
	--		--	END

	--		--	--Valido que el mes este abierto
	--		--	IF @IsClosedYear = 0 AND NOT EXISTS (SELECT 1 FROM GeneralLedger.ClosedMonth WITH (NOLOCK) WHERE Year = YEAR(@VoucherDate) AND Month = MONTH(@VoucherDate) AND Status = 1)
	--		--	BEGIN
	--		--		SELECT	@CodeMessage = 999,
	--		--				@Message = CONCAT('El comprobante contable no se puede crear ya que el periodo ', YEAR(@VoucherDate), '-', RIGHT(CONCAT('00', MONTH(@VoucherDate)), 2), ' no se encuentra abierto'),
	--		--				@IdJournalVoucherResult = 0
	--		--		RETURN
	--		--	END
	--		--	/*****periodo contable FIN****/

	--		--	--Valido que no se pueda hacer un documento con fecha mayor al sistema asi el mes este abierto
	--		--	IF CAST(@VoucherDate AS DATE) > CAST([Common].[GETDATE]() AS DATE) 
	--		--	BEGIN
	--		--		SELECT	@CodeMessage = 999,
	--		--				@Message = 'El comprobante contable no se puede crear con fechas superiores a la del sistema',
	--		--				@IdJournalVoucherResult = 0
	--		--		RETURN
	--		--	END
				
	--		--	--valido que existan detalles(eliminando los que tienen valores en cero)
	--		--	DELETE d 
	--		--	FROM @TableDetail d 
	--		--	WHERE
	--		--	(
	--		--		d.IsDelete = 1
	--		--		OR
	--		--		(d.DebitValue = 0 AND d.CreditValue = 0)
	--		--	)

	--		--	IF @Status = 2
				
	--		--	IF NOT EXISTS (SELECT 1 FROM @TableDetail )
	--		--		BEGIN
	--		--			SELECT	@CodeMessage = 999, 
	--		--					@Message = 'El comprobante contable no se puede crear ya que no tiene detalles.',
	--		--					@IdJournalVoucherResult = 0
	--		--			RETURN
	--		--	END

	--		--	--Valido que no este desbalanceado
	--		--	declare @TmpDebit decimal(21,5), @TmpCredit decimal(21,5)
	--		--	SELECT @TmpDebit = SUM(DebitValue),
	--		--		   @TmpCredit = SUM(CreditValue)
	--		--	FROM @TableDetail
	--		--	WHERE @Status <> 4

	--		--	IF @TmpDebit <> @TmpCredit 
	--		--	BEGIN				
	--		--		SELECT	@CodeMessage = 999,
	--		--				@Message = CONCAT('El comprobante contable no se puede crear ya que se encuentra desbalanceado. Debitos: ', FORMAT(@TmpDebit, 'C5', 'es-CO'), ' - Creditos:', FORMAT(@TmpCredit, 'C5', 'es-CO')),
	--		--				@IdJournalVoucherResult = 0
	--		--		RETURN
	--		--	END

	--		--	--VALIDO QUE LOS DETALLES TENGAN CUENTAS CONTABLES
	--		--	IF EXISTS(SELECT 1 FROM @TableDetail WHERE IdMainAccount IS NULL)BEGIN
	--		--		SELECT	@CodeMessage = 999,
	--		--				@Message = 'El comprobante contable no se puede crear ya que existen detalles del comprobante sin cuenta contable',
	--		--				@IdJournalVoucherResult = 0
	--		--		RETURN
	--		--	END

	--		-- --Valido que los centros de costos del detalle esten activos
	--		--	IF EXISTS 
	--		--	(
	--		--		SELECT 1 
	--		--		FROM @TableDetail td 
	--		--		JOIN Payroll.CostCenter cc WITH (NOLOCK) ON td.IdCostCenter = cc.Id 
	--		--		WHERE cc.State = 0
	--		--	)
	--		--	BEGIN
	--		--		SELECT @Message = STUFF((
	--		--				SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - ', cc.Code, ' - ', cc.Name)
	--		--				FROM @TableDetail td 
	--		--				JOIN Payroll.CostCenter cc WITH (NOLOCK) ON td.IdCostCenter = cc.Id 
	--		--				WHERE cc.State = 0
	--		--				FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

	--		--		SELECT	@CodeMessage = 999, 
	--		--				@Message = CONCAT('El comprobante contable no se puede crear ya que existen centros de costos del detalle en estado inactivo: ', ISNULL(@Message, '')),
	--		--				@IdJournalVoucherResult = 0
	--		--		RETURN
	--		--	END

	--		-- --Valido que las cuentas esten activas
	--		--	IF EXISTS 
	--		--	(
	--		--		SELECT 1 
	--		--		FROM @TableDetail td 
	--		--		JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON td.IdMainAccount = ma.Id 
	--		--		WHERE ma.Status = 0
	--		--	)
	--		--	BEGIN
	--		--		SELECT @Message = STUFF((
	--		--				SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - ', ma.Number, ' - ', ma.Name)
	--		--				FROM @TableDetail td 
	--		--				JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON td.IdMainAccount = ma.Id 
	--		--				WHERE ma.Status = 0
	--		--				FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

	--		--		SELECT	@CodeMessage = 999, 
	--		--				@Message = CONCAT('El comprobante contable no se puede crear ya que existen cuentas contables del detalle que estan inactivas: ', ISNULL(@Message, '')),
	--		--				@IdJournalVoucherResult = 0
	--		--		RETURN
	--		--	END

	--		--	-- Valido que las cuentas que manejen centro de costo tengan centro de costo
	--		--	IF EXISTS 
	--		--	(
	--		--		SELECT 1 
	--		--		FROM @TableDetail td 
	--		--		JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON td.IdMainAccount = ma.Id 
	--		--		WHERE ma.HandlesCostCenter = 1 AND td.IdCostCenter IS NULL
	--		--	)
	--		--	BEGIN
	--		--		SELECT @Message = STUFF((
	--		--				SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - ', ma.Number, ' - ', ma.Name)
	--		--				FROM @TableDetail td 
	--		--				JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON td.IdMainAccount = ma.Id 
	--		--				WHERE ma.HandlesCostCenter = 1 AND td.IdCostCenter IS NULL
	--		--				FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

	--		--		SELECT	@CodeMessage = 999, 
	--		--				@Message = CONCAT('El comprobante contable no se puede crear ya que existen cuentas contables del detalle que manejan centro de costo pero el centro de costo esta vacio: ', ISNULL(@Message, '')),
	--		--				@IdJournalVoucherResult = 0
	--		--		RETURN
	--		--	END

	--		--	-- Valido que las cuentas que manejen tercero tengan tercero
	--		--	IF EXISTS (SELECT 1 from inventory.SettingInventory WHERE @OperatingUnitId = OperatingUnitId AND AssociateCostMainAccount = 2) BEGIN
	--		--		IF EXISTS 
	--		--		(
	--		--			SELECT 1 
	--		--			FROM @TableDetail td 
	--		--			JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON td.IdMainAccount = ma.Id 
	--		--			WHERE ma.HandlesThirdParty = 1 AND td.IdThirdParty IS NULL
	--		--		)
	--		--		BEGIN
	--		--			SELECT @Message = STUFF((
	--		--					SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - ', ma.Number, ' - ', ma.Name)
	--		--					FROM @TableDetail td 
	--		--					JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON td.IdMainAccount = ma.Id 
	--		--					WHERE ma.HandlesThirdParty = 1 AND td.IdThirdParty IS NULL
	--		--					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

	--		--			SELECT	@CodeMessage = 999, 
	--		--					@Message = CONCAT('El comprobante contable no se puede crear ya que existen cuentas contables del detalle que manejan tercero pero el tercero esta vacio: ', ISNULL(@Message, '')),
	--		--					@IdJournalVoucherResult = 0
	--		--			RETURN
	--		--		END
	--		--	END

	--		--	/***************** RETENCION VALIDACION ************************/
	--		--	IF @IsClosedYear = 0 and EXISTS 
	--		--	(
	--		--		SELECT 1 
	--		--		FROM @TableDetail td 
	--		--		JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON td.IdMainAccount = ma.Id 
	--		--		WHERE ma.RetencionType > 0 
	--		--			AND (td.IdRetention IS NULL OR td.RetentionRate IS NULL OR td.BaseValue IS NULL) 
	--		--			AND @EntityName NOT IN ('VoucherTransaction', 'TreasuryNote', 'PortfolioTransfer', 'PayrollLiquidation')
	--		--	)
	--		--	BEGIN
	--		--		SELECT @Message = STUFF((
	--		--				SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - ', ma.Number, ' - ', ma.Name)
	--		--				FROM @TableDetail td 
	--		--				JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON td.IdMainAccount = ma.Id 
	--		--				WHERE ma.RetencionType > 0 
	--		--					AND (td.IdRetention IS NULL OR td.RetentionRate IS NULL OR td.BaseValue IS NULL)
	--		--					AND @EntityName NOT IN ('VoucherTransaction', 'TreasuryNote', 'PortfolioTransfer', 'PayrollLiquidation')
	--		--				FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

	--		--		SELECT	@CodeMessage = 999, 
	--		--				@Message = CONCAT('El comprobante contable no se puede crear ya que existen cuentas contables del detalle que manejan retención y no tienen información de la retención: ', ISNULL(@Message, '')),
	--		--				@IdJournalVoucherResult = 0
	--		--		RETURN
	--		--	END
	--		--	/*********************************************************************/

	--		--	/**************Validacion de restriccion************/
	--		--	-- Valido si existe cuentas con restricciones de terceros o centro de costos
	--		--		if (select count(*) 
	--		--			from @TableDetail d 
	--		--			inner join GeneralLedger.MainAccounts ma with(nolock) on ma.Id = d.IdMainAccount
	--		--			where ma.HandlesCostCenterRestriction = 1 or ma.HandlesThirdPartyRestriction = 1
	--		--			) > 0 begin
				
	--		--			DECLARE @IdMainAccount_Sync int,
	--		--					@HandlesCostCenterRestriction_Sync int,
	--		--					@HandlesThirdPartyRestriction_Sync int,
	--		--					@IdCostCenter_Sync int,
	--		--					@IdThirdParty_Sync int
	--		--			declare @NumberMainAccount_Sync varchar(25),
	--		--					@CostCenterName_Sync varchar(25),
	--		--					@ThirdPartyName_Sync varchar(25)
	--		--			declare @RestrictionType_Sync int
	--		--			declare @MessageRestriction_Sync varchar(2000)= ''
				
	--		--			DECLARE accountInfo_Sync CURSOR FOR 
	--		--				select d.IdMainAccount, ma.Number, ma.HandlesCostCenterRestriction, ma.HandlesThirdPartyRestriction, d.IdCostCenter, c.[Name], d.IdThirdParty, t.[Name]
	--		--				from @TableDetail d 
	--		--				inner join GeneralLedger.MainAccounts ma with(nolock) on ma.Id = d.IdMainAccount
	--		--				left join Payroll.CostCenter c on c.Id = d.IdCostCenter
	--		--				left join Common.ThirdParty t on t.Id = d.IdThirdParty
	--		--				where ma.HandlesCostCenterRestriction = 1 or ma.HandlesThirdPartyRestriction = 1
	--		--			OPEN accountInfo_Sync
				
	--		--			FETCH NEXT FROM accountInfo_Sync
	--		--			INTO @IdMainAccount_Sync, @NumberMainAccount_Sync, @HandlesCostCenterRestriction_Sync, @HandlesThirdPartyRestriction_Sync, @IdCostCenter_Sync, @CostCenterName_Sync, @IdThirdParty_Sync, @ThirdPartyName_Sync
				
	--		--			WHILE @@fetch_status = 0
	--		--			BEGIN
	--		--				if @HandlesCostCenterRestriction_Sync = 1 begin
	--		--					-- Primero valido cual es la opcion generica, es decir por defualt como estan todos los items, H/D
	--		--					set @RestrictionType_Sync = (select top 1 isnull(RestrictionType,2) from GeneralLedger.MainAccountRestrictions where MainAccountId = @IdMainAccount_Sync and ItemType = 1 and AllItems = 1)
	--		--					if (@RestrictionType_Sync = 2) begin --Si todos estan restringidos
	--		--						if (select count(*) from GeneralLedger.MainAccountRestrictions 
	--		--							where MainAccountId = @IdMainAccount_Sync and ItemType = 1 and CostCenterId = @IdCostCenter_Sync and RestrictionType = 1) = 0 begin
	--		--							set @MessageRestriction_Sync = concat(@MessageRestriction_Sync, 'No se puede realizar el movimiento de la cuenta ', @NumberMainAccount_Sync, ' con el centro de costo ', @CostCenterName_Sync, ', ')
	--		--						end
	--		--					end
	--		--					else if (@RestrictionType_Sync = 1) begin --Si todos estan habilitados
	--		--						if (select count(*) from GeneralLedger.MainAccountRestrictions 
	--		--							where MainAccountId = @IdMainAccount_Sync and ItemType = 1 and CostCenterId = @IdCostCenter_Sync and RestrictionType = 2) > 0 begin
	--		--							set @MessageRestriction_Sync = concat(@MessageRestriction_Sync, 'No se puede realizar el movimiento de la cuenta ', @NumberMainAccount_Sync, ' con el centro de costo ', @CostCenterName_Sync, ', ')
	--		--						end
	--		--					end
	--		--				end
					
	--		--				if @HandlesThirdPartyRestriction_Sync = 1 begin
	--		--					-- Primero valido cual es la opcion generica, es decir por defualt como estan todos los items, H/D
	--		--					set @RestrictionType_Sync = (select top 1 isnull(RestrictionType,2) from GeneralLedger.MainAccountRestrictions where MainAccountId = @IdMainAccount_Sync and ItemType = 2 and AllItems = 1)
	--		--					if (@RestrictionType_Sync = 2) begin --Si todos estan restringidos
	--		--						if (select count(*) from GeneralLedger.MainAccountRestrictions 
	--		--							where MainAccountId = @IdMainAccount_Sync and ItemType = 2 and ThirdPartyId = @IdThirdParty_Sync and RestrictionType = 1) = 0 begin
	--		--							set @MessageRestriction_Sync = concat(@MessageRestriction_Sync, 'No se puede realizar el movimiento de la cuenta ', @NumberMainAccount_Sync, ' con el tercero ', @ThirdPartyName_Sync, ', ')
	--		--						end
	--		--					end
	--		--					else if (@RestrictionType_Sync = 1) begin --Si todos estan habilitados
	--		--						if (select count(*) from GeneralLedger.MainAccountRestrictions 
	--		--							where MainAccountId = @IdMainAccount_Sync and ItemType = 2 and ThirdPartyId = @IdThirdParty_Sync and RestrictionType = 2) > 0 begin
	--		--							set @MessageRestriction_Sync = concat(@MessageRestriction_Sync, 'No se puede realizar el movimiento de la cuenta ', @NumberMainAccount_Sync, ' con el tercero ', @ThirdPartyName_Sync, ', ')
	--		--						end
	--		--					end
	--		--				end

	--		--				FETCH NEXT FROM accountInfo_Sync 
	--		--				INTO @IdMainAccount_Sync, @NumberMainAccount_Sync, @HandlesCostCenterRestriction_Sync, @HandlesThirdPartyRestriction_Sync, @IdCostCenter_Sync, @CostCenterName_Sync, @IdThirdParty_Sync, @ThirdPartyName_Sync
	--		--			END
	--		--			CLOSE accountInfo_Sync
	--		--			DEALLOCATE accountInfo_Sync

	--		--		end

	--		--	if (@MessageRestriction_Sync <> '') begin
	--		--		SELECT	@CodeMessage = 999, 
	--		--				@Message = @MessageRestriction_Sync,
	--		--				@IdJournalVoucherResult = 0
	--		--		RETURN
	--		--	end
	--		--/*************************************************************************************************/

	--		--	INSERT INTO GeneralLedger.AccountingMovement
	--		--	(
	--		--		LegalBookId, JournalVoucherTypeId, VoucherDate, Detail, EntityCode, EntityId, EntityName, JournalVoucherXml, CreationUser, CreationDate
	--		--	)
	--		--	SELECT @LegalBookId, @IdJournalVoucherType, @VoucherDate, SUBSTRING(@Detail,1,500), @EntityCode, @EntityId, @EntityName, @JournalVoucherXml, @CodeUser, [Common].[GETDATE]()

	--		--	DECLARE @MovementId int = SCOPE_IDENTITY()
	--		--	INSERT INTO [GeneralLedger].[AccountingMovementPending](
	--		--	[AccountingMovementId],
	--		--	[JournalVoucherTypeId],
	--		--	[VoucherDate],
	--		--	[Detail],
	--		--	[EntityCode],
	--		--	[EntityId],
	--		--	[EntityName],
	--		--	[CreationUser],
	--		--	[CreationDate])
	--		--	SELECT @MovementId, @IdJournalVoucherType, @VoucherDate, SUBSTRING(@Detail,1,500), @EntityCode, @EntityId, @EntityName, @CodeUser, [Common].[GETDATE]()
			
	--		--	SELECT	@CodeMessage = 0, 
	--		--				@Message = 'Se guardó el Comprobante Contable',
	--		--				@IdJournalVoucherResult = 0	
	--		--	RETURN
	--		--END
			
	--	/*********************************************** FIN **********************************************************/

	--	--Se establece la moneda del Libro 
	--	SET @BookCurrencyId =(SELECT OfficialCurrencyId from GeneralLedger.LegalBook where Id=@LegalBookId)
	--	SELECT @CurrencyId = IIF(ISNULL(@CurrencyId, 0) = 0, @BookCurrencyId, @CurrencyId)
	--	print concat('moneda:' ,@CurrencyId)
	--	print concat('moneda del libro:' ,@BookCurrencyId)

	--	--Si la Moneda No llega en la cabecera se toma por defecto la moneda oficial del sistema
	--	IF @CurrencyId IS NULL OR @CurrencyId=0 BEGIN
	--		SELECT top 1 @CurrencyId = cs.OfficialCurrencyId
	--		FROM GeneralLedger.CompanySettings cs WITH(NOLOCK)
	--	END

	--	--Identificar si es una homologación
	--	SELECT	@IsHomologation = 1 
	--	FROM GeneralLedger.JournalVouchers WITH (NOLOCK)
	--	WHERE @IdJournalVoucher = 0 AND AccountingMovementId = @AccountingMovementId
	--	/******************************************  VALIDACIONES GENERALES ******************************************/
		
	--	--Valido que no se este duplicando el registro
	--	IF @IdJournalVoucher = 0 AND EXISTS (SELECT 1 FROM GeneralLedger.JournalVouchers WITH (NOLOCK) WHERE AccountingMovementId = @AccountingMovementId AND LegalBookId = @LegalBookId)
	--	BEGIN
	--		SELECT	@CodeMessage = 999, 
	--				@Message = CONCAT('Ya existe un comprobante contable homologado en el libro ', Code, ' - ', Name),
	--				@IdJournalVoucherResult = 0
	--		FROM GeneralLedger.LegalBook WITH (NOLOCK)
	--		WHERE Id = @LegalBookId
	--		RETURN
	--	END

	--	--Valido que si el documento se encuentra homologado, solo se puede editar el documento que pertenece al libro oficial
	--	IF @LegalBookId <> @OfficialLegalBookId AND EXISTS (SELECT 1 FROM GeneralLedger.JournalVouchers WITH (NOLOCK) WHERE AccountingMovementId = @AccountingMovementId GROUP BY AccountingMovementId HAVING COUNT(1) > 1)
	--	BEGIN
	--		SELECT	@CodeMessage = 999, 
	--				@Message = 'El comprobante contable no se puede editar debido a que es un documento homologo y solo se puede editar el registro correspondiente al libro oficial',
	--				@IdJournalVoucherResult = 0
	--		RETURN
	--	END

	--	--Valido que el comprobante corresponda con el movimiento
	--	IF @IdJournalVoucher > 0 AND NOT EXISTS (SELECT 1 FROM GeneralLedger.JournalVouchers WITH (NOLOCK) WHERE Id = @IdJournalVoucher AND AccountingMovementId = @AccountingMovementId)
	--	BEGIN
	--		SELECT	@CodeMessage = 999, 
	--				@Message = 'El comprobante contable no corresponde con el movimiento contable',
	--				@IdJournalVoucherResult = 0
	--		RETURN
	--	END

	--	--Valido que no exista un documento homologo en un estado diferente
	--	IF EXISTS 
	--	(
	--		SELECT 1 
	--		FROM GeneralLedger.JournalVouchers jvo WITH (NOLOCK)
	--		JOIN GeneralLedger.JournalVouchers jvd WITH (NOLOCK) ON jvo.AccountingMovementId = jvd.AccountingMovementId AND jvo.Id > jvd.Id
	--		WHERE jvo.AccountingMovementId = @AccountingMovementId AND jvo.Status <> jvd.Status
	--	)
	--	BEGIN
	--		SELECT	@CodeMessage = 999, 
	--				@Message = 'El comprobante contable tiene documentos homologos en diferente estado',
	--				@IdJournalVoucherResult = 0
	--		RETURN
	--	END
		
	--	--Valido que el estado corresponda con la accion a realizar
	--	IF @Status = 4
	--	BEGIN
	--		--Si estoy desconfirmando el comprobante contable debe estar confirmado
	--		IF EXISTS (SELECT 1 FROM GeneralLedger.JournalVouchers WITH (NOLOCK) WHERE AccountingMovementId = @AccountingMovementId AND Status <> 2)
	--		BEGIN
	--			SELECT	@CodeMessage = 999,
	--					@Message = CONCAT('El comprobante contable del Libro ', lb.Code, ' - ', lb.Name, ' Consecutivo ', jv.Consecutive,' y Tipo de Comprobante ', jvt.Code, ' - ', jvt.Name ,' se encuentra en estado: ', CASE jv.Status 
	--																																																							WHEN 1 THEN 'Registrado'
	--																																																							WHEN 3 THEN 'Anulado'
	--																																																							ELSE 'N/A'
	--																																																						END),
	--					@IdJournalVoucherResult = 0
	--			FROM GeneralLedger.JournalVouchers jv WITH (NOLOCK)
	--			JOIN GeneralLedger.LegalBook lb WITH (NOLOCK) ON jv.LegalBookId = lb.Id
	--			JOIN GeneralLedger.JournalVoucherTypes jvt WITH (NOLOCK) ON jv.IdJournalVoucher = jvt.Id
	--			WHERE jv.AccountingMovementId = @AccountingMovementId AND jv.Status <> 2
	--			RETURN
	--		END

	--		--No se puede desconfirmar un comprobante contable interfazado, se deben ajustar por notas contables
	--		IF EXISTS (SELECT 1 FROM GeneralLedger.JournalVouchers WITH (NOLOCK) WHERE AccountingMovementId = @AccountingMovementId AND ISNULL(EntityName, '') NOT IN ('', 'JournalVouchers'))
	--		BEGIN
	--			SELECT	@CodeMessage = 999, 
	--					@Message = 'El comprobante contable no se puede desconfirmar ya que fue un documento interfazado',
	--					@IdJournalVoucherResult = 0
	--			RETURN
	--		END
	--	END
	--	ELSE IF @IsHomologation = 0 AND EXISTS (SELECT 1 FROM GeneralLedger.JournalVouchers WITH (NOLOCK) WHERE AccountingMovementId = @AccountingMovementId AND Status <> 1)
	--	BEGIN
	--		--Si no es una homologación el comprobante contable debe estar en estado registrado
	--		SELECT	@CodeMessage = 999,
	--				@Message = CONCAT('El comprobante contable del Libro ', lb.Code, ' - ', lb.Name, ' Consecutivo ', jv.Consecutive,' y Tipo de Comprobante ', jvt.Code, ' - ', jvt.Name ,' se encuentra en estado: ', CASE jv.Status 
	--																																																						WHEN 2 THEN 'Confirmado'
	--																																																						WHEN 3 THEN 'Anulado'
	--																																																						ELSE 'N/A'
	--																																																					END),
	--				@IdJournalVoucherResult = 0
	--		FROM GeneralLedger.JournalVouchers jv WITH (NOLOCK)
	--		JOIN GeneralLedger.LegalBook lb WITH (NOLOCK) ON jv.LegalBookId = lb.Id
	--		JOIN GeneralLedger.JournalVoucherTypes jvt WITH (NOLOCK) ON jv.IdJournalVoucher = jvt.Id
	--		WHERE jv.AccountingMovementId = @AccountingMovementId AND jv.Status <> 1
	--		RETURN
	--	END
	
	--	/****************************************************** ******************************************************/
	--	PRINT CONCAT('@IsHomologation:',@IsHomologation)
	--	PRINT CONCAT('@STATUS:',@Status)
	--	PRINT CONCAT('@EntityName:',@EntityName)
		
	--	IF @Status = 3
	--	BEGIN
	--		UPDATE GeneralLedger.JournalVouchers
	--			SET Status = @Status,
	--				ModificationUser = @CodeUser,
	--				ModificationDate = [Common].[GETDATE](),
	--				AnnulmentUser = @CodeUser,
	--				AnnulmentDate = [Common].[GETDATE]()
	--		WHERE AccountingMovementId = @AccountingMovementId
	--	END
	--	ELSE
	--	BEGIN
	--		--Libros a contabilizar de acuerdo a la configuración de VIEBOT			
	--		IF @Status = 4
	--		BEGIN	
	--			INSERT INTO @TableBookMovement
	--				SELECT j.LegalBookId, j.Id JournalVoucherId
	--				FROM GeneralLedger.JournalVouchers j WITH (NOLOCK)
	--				WHERE j.AccountingMovementId = @AccountingMovementId  
	--		END
	--		ELSE IF @IsHomologation = 1
	--		BEGIN
	--			INSERT INTO @TableBookMovement VALUES (@LegalBookId, @IdJournalVoucher)
	--		END
	--		ELSE IF @EntityName = 'JournalVouchers' OR @EntityName = '' 
	--		BEGIN
	--			IF EXISTS (SELECT 1 FROM GeneralLedger.JournalVouchers WITH (NOLOCK) WHERE AccountingMovementId = @AccountingMovementId)
	--			BEGIN
	--				INSERT INTO @TableBookMovement
	--					SELECT j.LegalBookId, j.Id JournalVoucherId
	--					FROM GeneralLedger.JournalVouchers j WITH (NOLOCK)
	--					WHERE j.AccountingMovementId = @AccountingMovementId 
						
	--			END
	--			ELSE
	--			BEGIN
	--				INSERT INTO @TableBookMovement VALUES (@LegalBookId, @IdJournalVoucher)
					
	--			END
	--		END
	--		ELSE IF @EntityName = 'DistributionCostElements'
	--		BEGIN
	--			INSERT INTO @TableBookMovement
	--				SELECT l.Id, @IdJournalVoucher
	--				FROM GeneralLedger.LegalBook l WITH(NOLOCK)
	--				WHERE l.OfficialBook = 1 and l.Status = 1

					
	--		END
	--		ELSE
	--		BEGIN--here
	--			INSERT INTO @TableBookMovement
	--				SELECT vb.LegalBookId, 0 JournalVoucherId
	--				FROM [GeneralLedger].[VieBot] vb WITH (NOLOCK)
	--				--WHERE vb.Form = IIF((ISNULL(@OriginEntityName, @EntityName)='MedicalFeesLiquidation' or ISNULL(@OriginEntityName, @EntityName)='CostDistributionDirectCost'),'AccountPayable',ISNULL(@OriginEntityName, @EntityName)) AND vb.Allow = 1
	--				WHERE vb.Form = IIF(ISNULL(@OriginEntityName, @EntityName)='MedicalFeesLiquidation','AccountPayable',ISNULL(@OriginEntityName, @EntityName)) AND vb.Allow = 1
	--					AND
	--					(
	--						vb.HandlesHomologation = 1
	--						OR
	--						(vb.LegalBookId = @LegalBookId)
	--					)
						
			
	--			UPDATE tbm
	--				SET tbm.JournalVoucherId = j.Id
	--			FROM @TableBookMovement tbm
	--			JOIN GeneralLedger.JournalVouchers j WITH (NOLOCK) ON tbm.LegalBookId = j.LegalBookId and j.AccountingMovementId = @AccountingMovementId 
				
	--			INSERT INTO @TableBookMovement
	--				SELECT j.LegalBookId, j.Id JournalVoucherId
	--				FROM GeneralLedger.JournalVouchers j WITH (NOLOCK)
	--				LEFT JOIN @TableBookMovement tbm ON j.LegalBookId = tbm.LegalBookId
	--				WHERE j.AccountingMovementId = @AccountingMovementId AND tbm.RowId IS NULL
					
	--		END
	--		/***************************************** VALIDACIONES CABECERA *****************************************/
			
	--		--Valido que exista un Libro Oficial
	--		IF @OfficialLegalBookId IS NULL 
	--		BEGIN				
	--			SELECT	@CodeMessage = 999,
	--					@Message = 'El comprobante contable no se puede crear ya que no existe un libro oficial',
	--					@IdJournalVoucherResult = 0
	--			RETURN
	--		END
			 
	--		--Valido que exista al menos un libro al cual afectar
	--		IF NOT EXISTS (SELECT 1 FROM @TableBookMovement)
	--		BEGIN
	--			SELECT	@CodeMessage = 999,
	--					@Message = 'no se generó ningún comprobante contable ya que asi esta configurado en VieBot',
	--					@IdJournalVoucherResult = 0
	--			RETURN
	--		END

	--		--Valido que el año este abierto
	--		IF EXISTS 
	--		(
	--			SELECT 1 
	--			FROM GeneralLedger.LegalBook lb WITH (NOLOCK)
	--			JOIN @TableBookMovement tbm ON lb.Id = tbm.LegalBookId
	--			WHERE YEAR(@VoucherDate) <= LastYearClose
	--		) 
	--		BEGIN
	--			SELECT	@CodeMessage = 999,
	--					@Message = CONCAT('El comprobante contable no se puede crear ya que el año ', YEAR(@VoucherDate), ' se encuentra cerrado'),
	--					@IdJournalVoucherResult = 0
	--			RETURN
	--		END

	--		--Valido que el mes este abierto
	--		IF @IsClosedYear = 0 AND NOT EXISTS (SELECT 1 FROM GeneralLedger.ClosedMonth WITH (NOLOCK) WHERE Year = YEAR(@VoucherDate) AND Month = MONTH(@VoucherDate) AND Status = 1)
	--		BEGIN
	--			SELECT	@CodeMessage = 999,
	--					@Message = CONCAT('El comprobante contable no se puede crear ya que el periodo ', YEAR(@VoucherDate), '-', RIGHT(CONCAT('00', MONTH(@VoucherDate)), 2), ' no se encuentra abierto'),
	--					@IdJournalVoucherResult = 0
	--			RETURN
	--		END
			
	--		--Valido que no se haya realizado estimaciones de costos
	--		IF EXISTS (SELECT 1 FROM Cost.CostEstimationNative WITH (NOLOCK) WHERE Year = YEAR(@VoucherDate) AND Month = MONTH(@VoucherDate))
	--		BEGIN
	--			SELECT	@CodeMessage = 999,
	--					@Message = CONCAT('El comprobante contable no se puede crear ya que existe una estimación de costos en el periodo ', YEAR(@VoucherDate), '-', RIGHT(CONCAT('00', MONTH(@VoucherDate)), 2)),
	--					@IdJournalVoucherResult = 0
	--			RETURN
	--		END
			
	--		--Valido que no se pueda hacer un documento con fecha mayor al sistema asi el mes este abierto
	--		IF CAST(@VoucherDate AS DATE) > CAST([Common].[GETDATE]() AS DATE) 
	--		BEGIN
	--			SELECT	@CodeMessage = 999,
	--					@Message = 'El comprobante contable no se puede crear con fechas superiores a la del sistema',
	--					@IdJournalVoucherResult = 0
	--			RETURN
	--		END
			
			

		
	--		--Valido que el tipo de comprobante tenga una secuencia numerica para la vigencia y el libro que esta recorriendo
	--		--Parametrizacion tipos conprobantes contables
	--		IF EXISTS
	--		(
	--			SELECT 1 
	--			FROM @TableBookMovement tbm
	--			LEFT JOIN GeneralLedger.JournalVoucherTypeConsecutive jvtc WITH (NOLOCK) ON tbm.LegalBookId = jvtc.LegalBookId AND @IdJournalVoucherType = jvtc.JournalVoucherTypeId AND jvtc.Year = Year(@VoucherDate)
	--			WHERE jvtc.Id IS NULL
	--		) 
	--		BEGIN
	--			SELECT	@CodeMessage = 999, 
	--					@Message = CONCAT('El comprobante contable no se puede crear ya que no existe una secuencia numerica con el libro ', lb.Code,  ' - ', lb.Name, ' para el tipo de comprobante ', jvt.Code, ' - ', jvt.Name, ' en la vigencia ', YEAR(@VoucherDate)),
	--					@IdJournalVoucherResult = 0
	--			FROM @TableBookMovement tbm
	--			LEFT JOIN GeneralLedger.LegalBook lb WITH (NOLOCK) ON tbm.LegalBookId = lb.Id
	--			LEFT JOIN GeneralLedger.JournalVoucherTypes jvt WITH (NOLOCK) ON @IdJournalVoucherType = jvt.Id
	--			LEFT JOIN GeneralLedger.JournalVoucherTypeConsecutive jvtc WITH (NOLOCK) ON tbm.LegalBookId = jvtc.LegalBookId AND @IdJournalVoucherType = jvtc.JournalVoucherTypeId AND jvtc.Year = Year(@VoucherDate)
	--			WHERE jvtc.Id IS NULL
	--			RETURN
	--		END

	--		--Valido que haya una moneda para la transaccion y que el libro tenga una moneda definida
	--		IF @CurrencyId IS NULL OR @BookCurrencyId IS NULL
	--		BEGIN
	--			SELECT	@CodeMessage = 999,
	--					@Message = CONCAT('El comprobante contable no se puede crear debido a que no hay moneda definida en',IIF(@CurrencyId is null, 'la Transacción','el Libro contable')),
	--					@IdJournalVoucherResult = 0
	--			RETURN
	--		END

	--		/************************************DATE TRM (Fecha del TRM)************************************************/
				
	--			PRINT @DateTRM
	--			--Si el Date trm no viene en el xml de la cabecera se toma la fecha del comprobante
	--			IF @DateTRM IS NULL OR @DateTRM ='' or @DateTRM = '0001-01-01' BEGIN
	--				SET @DateTRM =cast( @VoucherDate as DATE)
	--			END

	--		/************************************************************************************************************/

	--		--Valido que si la moneda de la transaccion es diferente a la del libro tenga registros en la tabla TRM
	--		IF EXISTS(	SELECT 1 
	--					FROM @TableBookMovement tb
	--					JOIN GeneralLedger.LegalBook lb WITH(NOLOCK) ON tb.LegalBookId=lb.Id
	--					WHERE lb.OfficialCurrencyId <> @CurrencyId AND [Common].[CurrencyConverterByModule](1,@CurrencyId,lb.OfficialCurrencyId, NULL,@EntityNameConversion,@DateTRM) is NULL) BEGIN

	--					SELECT @CodeMessage = 999,
	--							@Message = CONCAT('El comprobante contable no se puede crear debido No hay datos del TRM de la moneda de la Transacción para el libro : ',lb.Code,' - ',lb.Name, @DateTRM),
	--							@IdJournalVoucherResult = 0
	--					FROM @TableBookMovement tb
	--					JOIN GeneralLedger.LegalBook lb WITH(NOLOCK) ON tb.LegalBookId=lb.Id
	--					WHERE lb.OfficialCurrencyId <> @CurrencyId AND [Common].[CurrencyConverterByModule](1,@CurrencyId,lb.OfficialCurrencyId,NULL,@EntityNameConversion,@DateTRM) is NULL

	--					RETURN

	--		END
			
	--		/**************************************************************************************************************/
	--		/*********************************************** EN LINEA *****************************************************/
	--		--Se obtiene los detalles que vienen en el xml
	--		INSERT INTO @TableDetail
	--			SELECT	ISNULL(t.x.value('(Id/text())[1]','int'),0) AS Id,
	--					t.x.value('(IdMainAccount/text())[1]','int') AS IdMainAccount,
	--					IIF(t.x.value('(IdThirdParty/text())[1]','int') = 0, null, t.x.value('IdThirdParty[1]','int')) AS IdThirdParty,
	--					IIF(t.x.value('(IdCostCenter/text())[1]','int') = 0, null, t.x.value('IdCostCenter[1]','int')) AS IdCostCenter,
	--					[Common].[CurrencyConverterByModule](ISNULL(t.x.value('(DebitValue/text())[1]','decimal(21, 5)'), 0),@CurrencyId,@BookCurrencyId,NULL,@EntityNameConversion,@DateTRM) AS DebitValue,
	--					[Common].[CurrencyConverterByModule](ISNULL(t.x.value('(CreditValue/text())[1]','decimal(21, 5)'), 0),@CurrencyId,@BookCurrencyId,NULL,@EntityNameConversion,@DateTRM) AS CreditValue,
	--					t.x.value('(Detail/text())[1]','varchar(MAX)') AS Detail,
	--					t.x.value('(IdRetention/text())[1]','int') AS IdRetention,
	--					t.x.value('(RetentionRate/text())[1]','decimal(6, 3)') AS RetentionRate,
	--					[Common].[CurrencyConverterByModule](t.x.value('(BaseValue/text())[1]','decimal(18, 2)'),@CurrencyId,@BookCurrencyId,NULL,@EntityNameConversion,@DateTRM) AS BaseValue,
	--					[Common].[CurrencyConverterByModule](t.x.value('(BillingValue/text())[1]','decimal(18, 2)'),@CurrencyId,@BookCurrencyId,NULL,@EntityNameConversion,@DateTRM) AS BillingValue,
	--					ISNULL(t.x.value('(IsDelete/text())[1]','bit'),0) AS IsDelete,
	--					ISNULL(t.x.value('(DebitValue/text())[1]','decimal(21, 5)'), 0) AS OriginalDebitValue,
	--					ISNULL(t.x.value('(CreditValue/text())[1]','decimal(21, 5)'), 0)  AS OriginalCreditValue,
	--					t.x.value('(BaseValue/text())[1]','decimal(18, 2)') AS OriginalBaseValue,
	--					t.x.value('(BillingValue/text())[1]','decimal(18, 2)') AS OriginalBillingValue
	--			FROM @JournalVoucherXml.nodes('/JournalVoucher/JournalVoucherDetail') t(x)
	--			WHERE @Status <> 4

	--		--	select * from @TableDetail
	
	--		--se eliminan los detalles marcados para su eliminación
	--		DELETE jvd
	--		FROM GeneralLedger.JournalVoucherDetails jvd
	--		JOIN @TableDetail d ON jvd.Id = d.Id
	--		WHERE @IdJournalVoucher = jvd.IdAccounting 
	--			AND 
	--			(
	--				d.IsDelete = 1
	--				OR
	--				(d.DebitValue = 0 AND d.CreditValue = 0)
	--			)

	--		--Se eliminan los registros de la tabla temporal
	--		DELETE d 
	--		FROM @TableDetail d 
	--		WHERE
	--		(
	--			d.IsDelete = 1
	--			OR
	--			(d.DebitValue = 0 AND d.CreditValue = 0)
	--		)

	--		IF @Status = 2

	--		BEGIN -- Solo se cargan todos los detalles previamente guardados al confirmar			
	--			--Se obtiene los detalles previamente insertados que no han sido modificados
	--			INSERT INTO @TableDetail
	--				SELECT	jvd.Id,
	--						jvd.IdMainAccount,
	--						jvd.IdThirdParty,
	--						jvd.IdCostCenter,
	--						jvd.DebitValue,
	--						jvd.CreditValue,
	--						jvd.Detail,
	--						jvd.IdRetention,
	--						jvd.RetentionRate,
	--						jvd.BaseValue,
	--						jvd.BillingValue,
	--						0,
	--						jvd.DebitValue,
	--						jvd.CreditValue,
	--						jvd.BaseValue,
	--						jvd.BillingValue
	--				FROM GeneralLedger.JournalVoucherDetails jvd WITH (NOLOCK)
	--				LEFT JOIN @TableDetail d ON jvd.Id = d.Id
	--				WHERE jvd.IdAccounting = @IdJournalVoucher AND ISNULL(d.Id, 0) = 0
	--				--select * from @TableDetail
	--			-- Valido que existan detalles
	--			IF NOT EXISTS (SELECT 1 FROM @TableDetail)
	--			BEGIN
	--				SELECT	@CodeMessage = 999, 
	--						@Message = 'El comprobante contable no se puede crear ya que no tiene detalles.',
	--						@IdJournalVoucherResult = 0
	--				RETURN
	--			END
	--		END		

	--		-- Si la cuenta no maneja tercero entonces se quitan los terceros o
	--		-- Si la cuenta no maneja centro de costo entonces se quitan
	--		UPDATE td
	--			SET td.IdThirdParty = IIF(ma.HandlesThirdParty = 0, NULL, td.IdThirdParty),
	--				td.IdCostCenter = IIF(ma.HandlesCostCenter = 0, NULL, td.IdCostCenter)
	--		FROM @TableDetail td 
	--		JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON td.IdMainAccount = ma.Id 
	--		WHERE ma.HandlesThirdParty = 0 OR ma.HandlesCostCenter = 0
			
	--		/***************************************** VALIDACIONES DETALLES *****************************************/
	--		--Valido que no vengan movimientos en negativo
	--		IF EXISTS (SELECT 1 FROM @TableDetail WHERE DebitValue < 0 OR CreditValue < 0)
	--		BEGIN
	--			SELECT	@CodeMessage = 999, 
	--					@Message = 'El comprobante contable no se puede crear ya que existen movimientos con valores negativos.',
	--					@IdJournalVoucherResult = 0
	--			RETURN
	--		END

	--		--Se valida que los detalles tengan diligenciado sólo uno de los valores debito o credito (no puede existir detalles debito y credito a la vez)
	--		IF EXISTS (SELECT 1 FROM @TableDetail WHERE DebitValue > 0 AND CreditValue > 0)
	--		BEGIN
	--			SELECT	@CodeMessage = 999, 
	--					@Message = 'El comprobante contable no se puede crear ya que existen cuentas contables del detalle que son débito y crédito a la vez.',
	--					@IdJournalVoucherResult = 0
	--			RETURN
	--		END
			
	--		-- Valido que las cuentas permitan movimientos
	--		IF EXISTS 
	--		(
	--			SELECT 1 
	--			FROM @TableDetail td 
	--			JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON td.IdMainAccount = ma.Id 
	--			WHERE ma.AllowsMovement = 0
	--		)
	--		BEGIN
	--			SELECT @Message = STUFF((
	--					SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - ', ma.Number, ' - ', ma.Name)
	--					FROM @TableDetail td 
	--					JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON td.IdMainAccount = ma.Id 
	--					WHERE ma.AllowsMovement = 0
	--					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

	--			SELECT	@CodeMessage = 999, 
	--					@Message = CONCAT('El comprobante contable no se puede crear ya que existen cuentas contables del detalle que no permiten manejar movimientos: ', ISNULL(@Message, '')),
	--					@IdJournalVoucherResult = 0
	--			RETURN
	--		END

	--		-- Valido que las cuentas esten activas
	--		IF EXISTS 
	--		(
	--			SELECT 1 
	--			FROM @TableDetail td 
	--			JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON td.IdMainAccount = ma.Id 
	--			WHERE ma.Status = 0
	--		)
	--		BEGIN
	--			SELECT @Message = STUFF((
	--					SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - ', ma.Number, ' - ', ma.Name)
	--					FROM @TableDetail td 
	--					JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON td.IdMainAccount = ma.Id 
	--					WHERE ma.Status = 0
	--					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

	--			SELECT	@CodeMessage = 999, 
	--					@Message = CONCAT('El comprobante contable no se puede crear ya que existen cuentas contables del detalle que estan inactivas: ', ISNULL(@Message, '')),
	--					@IdJournalVoucherResult = 0
	--			RETURN
	--		END
						
	--		-- Valido que las cuentas que manejan retención tengan un concepto de retención
	--		--IF @IsClosedYear = 0 and EXISTS 
	--		--(
	--		--	SELECT 1 
	--		--	FROM @TableDetail td 
	--		--	JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON td.IdMainAccount = ma.Id 
	--		--	WHERE ma.RetencionType > 0 
	--		--		AND (td.IdRetention IS NULL OR td.RetentionRate IS NULL OR td.BaseValue IS NULL) 
	--		--		AND @EntityName NOT IN ('VoucherTransaction', 'TreasuryNote', 'PortfolioTransfer', 'PayrollLiquidation')
	--		--)
	--		--BEGIN
	--		--	SELECT @Message = STUFF((
	--		--			SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - ', ma.Number, ' - ', ma.Name)
	--		--			FROM @TableDetail td 
	--		--			JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON td.IdMainAccount = ma.Id 
	--		--			WHERE ma.RetencionType > 0 
	--		--				AND (td.IdRetention IS NULL OR td.RetentionRate IS NULL OR td.BaseValue IS NULL)
	--		--				AND @EntityName NOT IN ('VoucherTransaction', 'TreasuryNote', 'PortfolioTransfer', 'PayrollLiquidation')
	--		--			FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

	--		--	SELECT	@CodeMessage = 999, 
	--		--			@Message = CONCAT('El comprobante contable no se puede crear ya que existen cuentas contables del detalle que manejan retención y no tienen información de la retención: ', ISNULL(@Message, '')),
	--		--			@IdJournalVoucherResult = 0
	--		--	RETURN
	--		--END

	--		-- Valido que las cuentas que manejen tercero tengan tercero
	--		IF EXISTS (SELECT 1 from inventory.SettingInventory WHERE @OperatingUnitId = OperatingUnitId AND AssociateCostMainAccount = 2) BEGIN
	--			IF EXISTS 
	--			(
	--				SELECT 1 
	--				FROM @TableDetail td 
	--				JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON td.IdMainAccount = ma.Id 
	--				WHERE ma.HandlesThirdParty = 1 AND td.IdThirdParty IS NULL
	--			)
	--			BEGIN
	--				SELECT @Message = STUFF((
	--						SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - ', ma.Number, ' - ', ma.Name)
	--						FROM @TableDetail td 
	--						JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON td.IdMainAccount = ma.Id 
	--						WHERE ma.HandlesThirdParty = 1 AND td.IdThirdParty IS NULL
	--						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

	--				SELECT	@CodeMessage = 999, 
	--						@Message = CONCAT('El comprobante contable no se puede crear ya que existen cuentas contables del detalle que manejan tercero pero el tercero esta vacio: ', ISNULL(@Message, '')),
	--						@IdJournalVoucherResult = 0
	--				RETURN
	--			END
	--		END

	--		-- Valido si existe cuentas con restricciones de terceros o centro de costos
	--		if (select count(*) 
	--			from @TableDetail d 
	--			inner join GeneralLedger.MainAccounts ma with(nolock) on ma.Id = d.IdMainAccount
	--			where ma.HandlesCostCenterRestriction = 1 or ma.HandlesThirdPartyRestriction = 1
	--			) > 0 begin
				
	--			DECLARE @IdMainAccount int, @HandlesCostCenterRestriction int, @HandlesThirdPartyRestriction int, @IdCostCenter int, @IdThirdParty int
	--			declare @NumberMainAccount varchar(25), @CostCenterName varchar(25), @ThirdPartyName varchar(25)
	--			declare @RestrictionType int
	--			declare @MessageRestriction varchar(2000)= ''
				
	--			DECLARE accountInfo CURSOR FOR 
	--				select d.IdMainAccount, ma.Number, ma.HandlesCostCenterRestriction, ma.HandlesThirdPartyRestriction, d.IdCostCenter, c.[Name], d.IdThirdParty, t.[Name]
	--				from @TableDetail d 
	--				inner join GeneralLedger.MainAccounts ma with(nolock) on ma.Id = d.IdMainAccount
	--				left join Payroll.CostCenter c on c.Id = d.IdCostCenter
	--				left join Common.ThirdParty t on t.Id = d.IdThirdParty
	--				where ma.HandlesCostCenterRestriction = 1 or ma.HandlesThirdPartyRestriction = 1
	--			OPEN accountInfo
				
	--			FETCH NEXT FROM accountInfo 
	--			INTO @IdMainAccount, @NumberMainAccount, @HandlesCostCenterRestriction, @HandlesThirdPartyRestriction, @IdCostCenter, @CostCenterName, @IdThirdParty, @ThirdPartyName
				
	--			WHILE @@fetch_status = 0
	--			BEGIN
	--				if @HandlesCostCenterRestriction = 1 begin
	--					-- Primero valido cual es la opcion generica, es decir por defualt como estan todos los items, H/D
	--					set @RestrictionType = (select top 1 isnull(RestrictionType,2) from GeneralLedger.MainAccountRestrictions where MainAccountId = @IdMainAccount and ItemType = 1 and AllItems = 1)
	--					if (@RestrictionType = 2) begin --Si todos estan restringidos
	--						if (select count(*) from GeneralLedger.MainAccountRestrictions 
	--							where MainAccountId = @IdMainAccount and ItemType = 1 and CostCenterId = @IdCostCenter and RestrictionType = 1) = 0 begin
	--							set @MessageRestriction = concat(@MessageRestriction, 'No se puede realizar el movimiento de la cuenta ', @NumberMainAccount, ' con el centro de costo ', @CostCenterName, ', ')
	--						end
	--					end
	--					else if (@RestrictionType = 1) begin --Si todos estan habilitados
	--						if (select count(*) from GeneralLedger.MainAccountRestrictions 
	--							where MainAccountId = @IdMainAccount and ItemType = 1 and CostCenterId = @IdCostCenter and RestrictionType = 2) > 0 begin
	--							set @MessageRestriction = concat(@MessageRestriction, 'No se puede realizar el movimiento de la cuenta ', @NumberMainAccount, ' con el centro de costo ', @CostCenterName, ', ')
	--						end
	--					end
	--				end
					
	--				if @HandlesThirdPartyRestriction = 1 begin
	--					-- Primero valido cual es la opcion generica, es decir por defualt como estan todos los items, H/D
	--					set @RestrictionType = (select top 1 isnull(RestrictionType,2) from GeneralLedger.MainAccountRestrictions where MainAccountId = @IdMainAccount and ItemType = 2 and AllItems = 1)
	--					if (@RestrictionType = 2) begin --Si todos estan restringidos
	--						if (select count(*) from GeneralLedger.MainAccountRestrictions 
	--							where MainAccountId = @IdMainAccount and ItemType = 2 and ThirdPartyId = @IdThirdParty and RestrictionType = 1) = 0 begin
	--							set @MessageRestriction = concat(@MessageRestriction, 'No se puede realizar el movimiento de la cuenta ', @NumberMainAccount, ' con el tercero ', @ThirdPartyName, ', ')
	--						end
	--					end
	--					else if (@RestrictionType = 1) begin --Si todos estan habilitados
	--						if (select count(*) from GeneralLedger.MainAccountRestrictions 
	--							where MainAccountId = @IdMainAccount and ItemType = 2 and ThirdPartyId = @IdThirdParty and RestrictionType = 2) > 0 begin
	--							set @MessageRestriction = concat(@MessageRestriction, 'No se puede realizar el movimiento de la cuenta ', @NumberMainAccount, ' con el tercero ', @ThirdPartyName, ', ')
	--						end
	--					end
	--				end

	--				FETCH NEXT FROM accountInfo 
	--				INTO @IdMainAccount, @NumberMainAccount, @HandlesCostCenterRestriction, @HandlesThirdPartyRestriction, @IdCostCenter, @CostCenterName, @IdThirdParty, @ThirdPartyName
	--			END
	--			CLOSE accountInfo
	--			DEALLOCATE accountInfo

	--		end
			
	--		if (@MessageRestriction <> '') begin
	--			SELECT	@CodeMessage = 999, 
	--					@Message = @MessageRestriction,
	--					@IdJournalVoucherResult = 0
	--			RETURN
	--		end
			
	--		-- Valido que los tercero del detalle esten activos
	--		IF EXISTS 
	--		(
	--			SELECT 1 
	--			FROM @TableDetail td 
	--			JOIN Common.ThirdParty tp WITH (NOLOCK) ON td.IdThirdParty = tp.Id 
	--			WHERE tp.State = 0
	--		)
	--		BEGIN
	--			SELECT @Message = STUFF((
	--					SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - ', tp.Nit, ' - ', tp.Name)
	--					FROM @TableDetail td 
	--					JOIN Common.ThirdParty tp WITH (NOLOCK) ON td.IdThirdParty = tp.Id 
	--					WHERE tp.State = 0
	--					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

	--			SELECT	@CodeMessage = 999, 
	--					@Message = CONCAT('El comprobante contable no se puede crear ya que existen terceros del detalle en estado inactivo: ', ISNULL(@Message, '')),
	--					@IdJournalVoucherResult = 0
	--			RETURN
	--		END

	--		 --Valido que las cuentas que manejen centro de costo tengan centro de costo
	--		IF EXISTS 
	--		(
	--			SELECT 1 
	--			FROM @TableDetail td 
	--			JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON td.IdMainAccount = ma.Id 
	--			WHERE ma.HandlesCostCenter = 1 AND td.IdCostCenter IS NULL
	--		)
	--		BEGIN
	--			SELECT @Message = STUFF((
	--					SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - ', ma.Number, ' - ', ma.Name)
	--					FROM @TableDetail td 
	--					JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON td.IdMainAccount = ma.Id 
	--					WHERE ma.HandlesCostCenter = 1 AND td.IdCostCenter IS NULL
	--					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

	--			SELECT	@CodeMessage = 999, 
	--					@Message = CONCAT('El comprobante contable no se puede crear ya que existen cuentas contables del detalle que manejan centro de costo pero el centro de costo esta vacio: ', ISNULL(@Message, '')),
	--					@IdJournalVoucherResult = 0
	--			RETURN
	--		END

	--		-- Valido que los centros de costos del detalle esten activos
	--		IF EXISTS 
	--		(
	--			SELECT 1 
	--			FROM @TableDetail td 
	--			JOIN Payroll.CostCenter cc WITH (NOLOCK) ON td.IdCostCenter = cc.Id 
	--			WHERE cc.State = 0
	--		)
	--		BEGIN
	--			SELECT @Message = STUFF((
	--					SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - ', cc.Code, ' - ', cc.Name)
	--					FROM @TableDetail td 
	--					JOIN Payroll.CostCenter cc WITH (NOLOCK) ON td.IdCostCenter = cc.Id 
	--					WHERE cc.State = 0
	--					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

	--			SELECT	@CodeMessage = 999, 
	--					@Message = CONCAT('El comprobante contable no se puede crear ya que existen centros de costos del detalle en estado inactivo: ', ISNULL(@Message, '')),
	--					@IdJournalVoucherResult = 0
	--			RETURN
	--		END
	--		print @CurrencyId
	--		/****************************************************** ******************************************************/
	--		-- Tabla movimiento con las respectivas cuentas de libros
	--		INSERT INTO #TableDetailHomologation
	--			SELECT	ISNULL(ha.LegalBookId, @LegalBookId) LegalBookId,
	--					td.RowId,
	--					-----------------------------------
	--					IIF(ISNULL(ha.LegalBookId, @LegalBookId) = @LegalBookId, td.Id, 0) Id,
	--					ISNULL(ha.MainAccountId, td.IdMainAccount) IdMainAccount,
	--					td.IdThirdParty,
	--					td.IdCostCenter,
	--					IIF(@CurrencyId = ISNULL(ha.OfficialCurrencyId,@BookCurrencyId),
	--						td.OriginalDebitValue,
	--						[Common].[CurrencyConverterByModule](td.DebitValue,@BookCurrencyId,ISNULL(ha.OfficialCurrencyId,@BookCurrencyId),NULL,@EntityNameConversion,@DateTRM)
	--					),
	--					IIF(@CurrencyId = ISNULL(ha.OfficialCurrencyId,@BookCurrencyId),
	--						td.OriginalCreditValue,
	--						[Common].[CurrencyConverterByModule](td.CreditValue,@BookCurrencyId,ISNULL(ha.OfficialCurrencyId,@BookCurrencyId),NULL,@EntityNameConversion,@DateTRM)
	--					),
	--					td.Detail,
	--					td.IdRetention,
	--					td.RetentionRate,
	--					IIF(@CurrencyId = ISNULL(ha.OfficialCurrencyId,@BookCurrencyId),
	--						td.OriginalBaseValue,
	--						[Common].[CurrencyConverterByModule](td.BaseValue,@BookCurrencyId,ISNULL(ha.OfficialCurrencyId,@BookCurrencyId),NULL,@EntityNameConversion,@DateTRM)
	--					),
	--					IIF(@CurrencyId = ISNULL(ha.OfficialCurrencyId,@BookCurrencyId),
	--						td.OriginalBillingValue,
	--						[Common].[CurrencyConverterByModule](td.BillingValue,@BookCurrencyId,ISNULL(ha.OfficialCurrencyId,@BookCurrencyId),NULL,@EntityNameConversion,@DateTRM)
	--					)						
	--			FROM @TableDetail td
	--			LEFT JOIN
	--			(
	--					--Cuentas Oficiales
	--					SELECT DISTINCT tm.LegalBookId, ma.Id OfficialMainAccountId, ma.Id MainAccountId, lb.OfficialCurrencyId
	--					FROM @TableDetail td
	--					JOIN @TableBookMovement tm ON @OfficialLegalBookId = tm.LegalBookId
	--					JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON td.IdMainAccount = ma.Id AND tm.LegalBookId = ma.LegalBookId
	--					JOIN GeneralLedger.LegalBook lb WITH(NOLOCK) ON tm.LegalBookId=lb.Id
	--				UNION ALL
	--					--Cuentas homologas
	--					SELECT DISTINCT
	--						tm.LegalBookId, ha.OfficialMainAccountId, ha.MainAccountId,lb.OfficialCurrencyId
	--					FROM @TableDetail td
	--					JOIN @TableBookMovement tm ON @OfficialLegalBookId <> tm.LegalBookId
	--					JOIN GeneralLedger.HomologationAccount ha WITH (NOLOCK) ON td.IdMainAccount = ha.OfficialMainAccountId
	--					JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ha.OfficialMainAccountId = ma.Id AND @OfficialLegalBookId = ma.LegalBookId
	--					JOIN GeneralLedger.MainAccounts mah WITH (NOLOCK) ON tm.LegalBookId = mah.LegalBookId AND ha.MainAccountId = mah.Id
	--					JOIN GeneralLedger.LegalBook lb WITH(NOLOCK) ON lb.Id=tm.LegalBookId
	--			) ha ON @IsHomologation = 0 AND td.IdMainAccount = ha.OfficialMainAccountId
	--		/*********************************** VALIDACIONES DETALLES DEFINITIVOS ***********************************/
	--		-- Valido que todos los detalles pertenezcan al comprobante contable
	--		--select sum(CreditValue) Creditos, sum(DebitValue) Debitos from #TableDetailHomologation where  LegalBookId =3
	--		--select * from #TableDetailHomologation 

	--		IF EXISTS 
	--		(
	--			SELECT 1 
	--			FROM @TableBookMovement tbm
	--			JOIN #TableDetailHomologation tdh ON tbm.LegalBookId = tdh.LegalBookId
	--			LEFT JOIN GeneralLedger.JournalVoucherDetails jvd WITH (NOLOCK) ON tdh.Id = jvd.Id
	--			WHERE tdh.Id <> 0 AND tbm.JournalVoucherId <> ISNULL(jvd.IdAccounting, 0)
	--		)
	--		BEGIN
	--			SELECT	@CodeMessage = 999, 
	--					@Message = 'El comprobante contable no se puede crear ya que existen detalles que no pertenecen al comprobante contable',
	--					@IdJournalVoucherResult = 0
	--			RETURN
	--		END
	--		-- Valido que todos los detalles pertenezcan al correspondiente libro contable			
	--		IF EXISTS 
	--		(
	--			SELECT 1 
	--			FROM #TableDetailHomologation tdh
	--			LEFT JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON tdh.IdMainAccount = ma.Id
	--			WHERE tdh.LegalBookId <> ma.LegalBookId
	--		)
	--		BEGIN
	--			SELECT @Message = STUFF((
	--					SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - Libro: ', lb.Code, ' - ', lb.Name, ' - Cuenta Contable: ', ma.Number, ' - ', ma.Name)
	--					FROM #TableDetailHomologation tdh
	--					LEFT JOIN GeneralLedger.LegalBook lb WITH (NOLOCK) ON tdh.LegalBookId = lb.Id
	--					LEFT JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON tdh.IdMainAccount = ma.Id
	--					WHERE tdh.LegalBookId <> ISNULL(ma.LegalBookId, 0)
	--					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

	--			SELECT	@CodeMessage = 999, 
	--					@Message = CONCAT('El comprobante contable no se puede crear ya que existen cuentas contables que no pertenecen al libro contable: ', ISNULL(@Message, '')),
	--					@IdJournalVoucherResult = 0
	--			RETURN
	--		END
			
	--		--SELECT *
	--		--	FROM 
	--		--	(
	--		--		SELECT tbm.LegalBookId, td.RowId
	--		--		FROM @TableBookMovement tbm, @TableDetail td
	--		--	) tbm
	--		--	LEFT JOIN #TableDetailHomologation tdh ON tbm.LegalBookId = tdh.LegalBookId AND tbm.RowId = tdh.TableDetailRowId
	--		--	WHERE tdh.TableDetailRowId IS NULL
	--		-- Valido que todos los detalles hayan sido homologados
	--		IF EXISTS 
	--		(
	--			SELECT 1 
	--			FROM 
	--			(
	--				SELECT tbm.LegalBookId, td.RowId
	--				FROM @TableBookMovement tbm, @TableDetail td
	--			) tbm
	--			LEFT JOIN #TableDetailHomologation tdh ON tbm.LegalBookId = tdh.LegalBookId AND tbm.RowId = tdh.TableDetailRowId
	--			WHERE tdh.TableDetailRowId IS NULL
	--		)
	--		BEGIN
	--			SELECT @Message = STUFF((
	--					SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - Libro: ', lb.Code, ' - ', lb.Name, ' - Cuenta Contable: ', ma.Number, ' - ', ma.Name)
	--					FROM 
	--					(
	--						SELECT tbm.LegalBookId, td.RowId, td.IdMainAccount
	--						FROM @TableBookMovement tbm, @TableDetail td
	--					) tbm
	--					LEFT JOIN #TableDetailHomologation tdh ON tbm.LegalBookId = tdh.LegalBookId AND tbm.RowId = tdh.TableDetailRowId
	--					LEFT JOIN GeneralLedger.LegalBook lb WITH (NOLOCK) ON tbm.LegalBookId = lb.Id
	--					LEFT JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON tbm.IdMainAccount = ma.Id
	--					WHERE tdh.TableDetailRowId IS NULL
	--					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

	--			SELECT	@CodeMessage = 999, 
	--					@Message = CONCAT('El comprobante contable no se puede crear ya que existen cuentas contable del detalle que no han sido homologadas: ', ISNULL(@Message, '')),
	--					@IdJournalVoucherResult = 0
	--			RETURN
	--		END

	--		-- Valido que todos los detalles hayan sido homologados solo una vez
	--		IF EXISTS 
	--		(
	--			SELECT 1 
	--			FROM #TableDetailHomologation tdh
	--			GROUP BY tdh.LegalBookId, tdh.TableDetailRowId
	--			HAVING COUNT(1) > 1
	--		)
	--		BEGIN
	--			SELECT @Message = STUFF((
	--					SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - Libro: ', lb.Code, ' - ', lb.Name, ' - Cuenta Contable: ', ma.Number, ' - ', ma.Name)
	--					FROM 
	--					(
	--						SELECT tdh.LegalBookId, tdh.TableDetailRowId
	--						FROM #TableDetailHomologation tdh
	--						GROUP BY tdh.LegalBookId, tdh.TableDetailRowId
	--						HAVING COUNT(1) > 1
	--					) tbm
	--					LEFT JOIN @TableDetail td ON tbm.TableDetailRowId = td.RowId
	--					LEFT JOIN GeneralLedger.LegalBook lb WITH (NOLOCK) ON tbm.LegalBookId = lb.Id
	--					LEFT JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON td.IdMainAccount = ma.Id
	--					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

	--			SELECT	@CodeMessage = 999, 
	--					@Message = CONCAT('El comprobante contable no se puede crear ya que existen cuentas contable del detalle con más de un homologo: ', ISNULL(@Message, '')),
	--					@IdJournalVoucherResult = 0
	--			RETURN
	--		END

	--		--Validaciones al confirmar
	--		IF @Status = 2
	--		BEGIN
			
	--			 --Valido que los debitos sean igual que los creditos en los detalles
	--			IF EXISTS 
	--			(
	--				SELECT 1 
	--				FROM 
	--				(
	--					SELECT SUM(DebitValue) DebitValue, SUM(CreditValue) CreditValue 
	--					FROM @TableDetail
	--				) d
	--				WHERE ROUND(d.DebitValue,-1) <> ROUND( d.CreditValue,-1)
	--			)
	--			BEGIN
	--				SELECT	@CodeMessage = 999, 
	--						@Message = CONCAT('El comprobante contable no se puede crear ya que se encuentra desbalanceado. Debitos: ', FORMAT(SUM(DebitValue), 'C5', 'es-CO'), ' - Creditos:', FORMAT(SUM(CreditValue), 'C5', 'es-CO')),
	--						@IdJournalVoucherResult = 0
	--				FROM @TableDetail
	--				RETURN
	--			END

	--			-- Valido que los debitos sean igual que los creditos en los detalles homologados
				
	--			IF EXISTS 
	--			(
	--				SELECT 1 
	--				FROM #TableDetailHomologation
	--				GROUP BY LegalBookId
	--				HAVING ROUND( SUM(DebitValue),-1) <> ROUND( SUM(CreditValue),-1)
	--			)
	--			BEGIN
	--				SELECT	@CodeMessage = 999, 
	--						@Message = CONCAT('El comprobante contable no se puede crear ya que los detalles luego de homologarse el libro ', lb.Code, ' - ', lb.Name, ' se encuentra desbalanceado. Debitos: ', FORMAT(SUM(DebitValue), 'C5', 'es-CO'), ' - Creditos:', FORMAT(SUM(CreditValue), 'C5', 'es-CO')),
	--						@IdJournalVoucherResult = 0
	--				FROM #TableDetailHomologation tdh
	--				LEFT JOIN GeneralLedger.LegalBook lb WITH (NOLOCK) ON tdh.LegalBookId = lb.Id
	--				GROUP BY LegalBookId, lb.Code, lb.Name
	--				HAVING SUM(DebitValue) <> SUM(CreditValue)
	--				RETURN
	--			END
	--		END

	--		/************************************************ PROCESO ************************************************/
			
	--		--Si apenas se esta creando el comprobante se inserta el registro del movimiento
	--		IF @AccountingMovementId = 0 
	--		BEGIN
	--			INSERT INTO GeneralLedger.AccountingMovement
	--			(
	--				LegalBookId, JournalVoucherTypeId, VoucherDate, Detail, EntityCode, EntityId, EntityName, JournalVoucherXml, CreationUser, CreationDate
	--			)
	--			SELECT @LegalBookId, @IdJournalVoucherType, @VoucherDate, SUBSTRING(@Detail,1,500), @EntityCode, @EntityId, @EntityName, @JournalVoucherXml, @CodeUser, [Common].[GETDATE]()
				
	--			SET @AccountingMovementId = SCOPE_IDENTITY()
	--		END

	--		/******************************************** CONTABILIZACION ********************************************/
	--		SET @Rows = 1
	--		SET @RowId = 0

	--		WHILE @Rows > 0 --while
	--		BEGIN		
			
	--			SELECT TOP 1 
	--				@RowId = RowId, 
	--				@MovementLegalBookId = LegalBookId, 
	--				@MovementJournalVoucherId = JournalVoucherId 
	--			FROM @TableBookMovement 
	--			WHERE RowId > @RowId 
	--			ORDER BY RowId

	--			SET @Rows = @@ROWCOUNT

	--			IF @Rows = 0 
	--				BREAK
	--			-------------------------------------------------------------------------------------------------------			
	--			--Si apenas se esta guardando, y no es el libro enviado en la cabecera, no realizamos nada
	--			--Esto a fin de no eliminar los detalles de los documentos homologos, dado que, como se envia por lotes puede generar inconvenientes
				
	--			--El formulario ConsignmentCostList se excluye de esta condidicón ya que se esta pasando con estado 1, pero igual se necesita realizar la homologación 
	--				IF @Status = 1 AND @MovementLegalBookId <> @LegalBookId AND @EntityName != 'ConsignmentCostList'
	--			BEGIN
	--				CONTINUE
	--			END				

	--			--Si estan confirmando el comprobante
	--			IF @Status = 2 
	--			BEGIN
	--				SET @ConfirmationUser = @CodeUser
	--				SET @ConfirmationDate = [Common].[GETDATE]()
	--			END				

	--			IF @MovementJournalVoucherId = 0 
	--			BEGIN -- Si el comprobante contable es nuevo
	--				UPDATE GeneralLedger.JournalVoucherTypeConsecutive 
	--					SET @Consecutive = Consecutive = Consecutive + 1
	--				WHERE JournalVoucherTypeId = @IdJournalVoucherType AND LegalBookId = @MovementLegalBookId AND [Year] = Year(@VoucherDate)
				
	--				-- Valido que no exista ya un comprobante con el consecutivo para el tipo de comprobante, libro y año
	--				IF EXISTS 
	--				(
	--					SELECT 1 
	--					FROM GeneralLedger.JournalVouchers jv WITH (NOLOCK)
	--					WHERE jv.Consecutive = (@Consecutive - 1) 
	--						AND jv.LegalBookId = @MovementLegalBookId 
	--						AND jv.IdJournalVoucher = @IdJournalVoucherType
	--						AND jv.YearMovement = YEAR(@VoucherDate)
	--				)
	--				BEGIN
	--					SELECT	@CodeMessage = 999, 
	--							@Message = CONCAT('Ya existe un comprobante contable con el consecutivo ', (@Consecutive - 1), 
	--								' del tipo de comprobante ', (SELECT CONCAT(Code, ' - ', Name) FROM GeneralLedger.JournalVoucherTypes WITH (NOLOCK) WHERE Id = @IdJournalVoucherType), 
	--								' del año ', YEAR(@VoucherDate),
	--								' para el libro ',  (SELECT CONCAT(Code, ' - ', Name) FROM GeneralLedger.LegalBook WITH (NOLOCK) WHERE Id = @MovementLegalBookId)),
	--							@IdJournalVoucherResult = 0
	--					RETURN
	--				END

	--				INSERT INTO [GeneralLedger].[JournalVouchers] 
	--				(
	--					[Consecutive],[AccountingMovementId],[LegalBookId],[IdJournalVoucher],[VoucherDate],[YearMovement],[Status],[Imported],
	--					[Detail],[EntityCode],[EntityId],[EntityName],[IsClosedYear],[CreationUser],[CreationDate],[ConfirmationUser],[ConfirmationDate]
	--				)
	--				SELECT	@Consecutive - 1, @AccountingMovementId, @MovementLegalBookId, @IdJournalVoucherType, @VoucherDate, YEAR(@VoucherDate), @Status, @Imported, 
	--						@Detail, @EntityCode, @EntityId, @EntityName, @IsClosedYear, @CodeUser, [Common].[GETDATE](), @ConfirmationUser, @ConfirmationDate

	--				SET @MovementJournalVoucherId = SCOPE_IDENTITY()				
	--				UPDATE @TableBookMovement 
	--					SET JournalVoucherId = @MovementJournalVoucherId 
	--				 WHERE LegalBookId = @MovementLegalBookId
	--			END				
	--			ELSE
	--			BEGIN -- Si se esta modificando
	--				UPDATE [GeneralLedger].[JournalVouchers] 
	--					SET VoucherDate = @VoucherDate, 
	--						Detail = @Detail, 
	--						Status = IIF(@Status = 4, 1, @Status),
	--						ModificationUser = @CodeUser, 
	--						ModificationDate = [Common].[GETDATE](),
	--						ConfirmationUser = @ConfirmationUser,
	--						ConfirmationDate = @ConfirmationDate
	--				WHERE Id = @MovementJournalVoucherId
	--			END
	--			--select * from [GeneralLedger].[JournalVouchers] WHERE Id = @MovementJournalVoucherId
	--			IF @Status <> 4 
	--			BEGIN
	--				IF (@MovementLegalBookId <> @OfficialLegalBookId AND EXISTS (SELECT 1 FROM GeneralLedger.JournalVouchers WITH (NOLOCK) WHERE AccountingMovementId = @AccountingMovementId GROUP BY AccountingMovementId HAVING COUNT(1) > 1))
	--				BEGIN
	--					DELETE GeneralLedger.JournalVoucherDetails WHERE IdAccounting = @MovementJournalVoucherId
	--				END
					
	--				--Se actualiza los detalles del comprobante
	--				UPDATE jvd
	--					SET jvd.IdMainAccount = tdh.IdMainAccount,
	--						jvd.IdThirdParty = tdh.IdThirdParty,
	--						jvd.IdCostCenter = tdh.IdCostCenter,
	--						jvd.DebitValue = tdh.DebitValue,
	--						jvd.CreditValue = tdh.CreditValue,
	--						jvd.Detail = tdh.Detail,
	--						jvd.IdRetention = tdh.IdRetention,
	--						jvd.RetentionRate = tdh.RetentionRate,
	--						jvd.BaseValue = tdh.BaseValue,
	--						jvd.BillingValue = tdh.BillingValue
	--				FROM #TableDetailHomologation tdh
	--				JOIN GeneralLedger.JournalVoucherDetails jvd ON tdh.Id = jvd.Id
	--				WHERE tdh.LegalBookId = @MovementLegalBookId

	--				--Se inserta los nuevos detalles del comprobante
	--				INSERT INTO GeneralLedger.JournalVoucherDetails
	--				(
	--					IdAccounting, IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail, IdRetention, RetentionRate, BaseValue, BillingValue
	--				)
	--				SELECT @MovementJournalVoucherId, IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue, Detail, IdRetention, RetentionRate, BaseValue, BillingValue
	--				FROM #TableDetailHomologation tdh
	--				WHERE tdh.LegalBookId = @MovementLegalBookId AND tdh.Id = 0  and tdh.DebitValue is not null and tdh.CreditValue is not null
	--				--SELECT CONCAT(ma.Number, ' - ', ma.Name) MainAccount,* 
	--				--FROM GeneralLedger.JournalVoucherDetails jvd
	--				--JOIN GeneralLedger.MainAccounts ma ON jvd.IdMainAccount = ma.Id
	--				--WHERE jvd.IdAccounting = @MovementJournalVoucherId
					
	--			END
			
	--			-------------------------------------------------------------------------------------------------------
							
	--			--Si se esta confirmando o reversando
	--			IF @Status IN (2, 4)
	--			BEGIN
	--				IF @Status = 2
	--				BEGIN
	--					-- Valido que los debitos sean igual que los creditos en los detalles
	--					IF EXISTS 
	--					(
	--						SELECT 1 
	--						FROM GeneralLedger.JournalVoucherDetails jvd WITH (NOLOCK)
	--						WHERE jvd.IdAccounting = @MovementJournalVoucherId
	--						GROUP BY jvd.IdAccounting
	--						HAVING ROUND(SUM(DebitValue),-1) <> ROUND(SUM(CreditValue),-1))
	--					BEGIN
	--						SELECT	@CodeMessage = 999, 
	--								@Message = CONCAT('El comprobante contable no se puede crear ya que se encuentra desbalanceado. Debitos: ', FORMAT(SUM(DebitValue), 'C5', 'es-CO'), ' - Creditos:', FORMAT(SUM(CreditValue), 'C5', 'es-CO')),
	--								@IdJournalVoucherResult = 0
	--						FROM GeneralLedger.JournalVoucherDetails jvd WITH (NOLOCK)
	--						WHERE jvd.IdAccounting = @MovementJournalVoucherId
	--						GROUP BY jvd.IdAccounting
	--						RETURN
	--					END
	--				END

	--				--Periodo del movimiento
	--				IF @IsClosedYear = 1
	--				BEGIN
	--					SET @MonthMovement  = 14
	--				END
	--				ELSE IF @IsClosedYear = 2
	--				BEGIN
	--					SET @MonthMovement  = 13
	--				END
	--				ELSE
	--				BEGIN
	--					SET @MonthMovement = MONTH(@VoucherDate)	
	--				END

	--				UPDATE glb
	--					SET glb.DebitValue = glb.DebitValue + jvd.DebitValue,
	--						glb.CreditValue = glb.CreditValue + jvd.CreditValue
	--				FROM 
	--				(
	--					SELECT	YEAR(@VoucherDate) Year, @MonthMovement Month,
	--							jvd.IdMainAccount, jvd.IdThirdParty, jvd.IdCostCenter,
	--							SUM(jvd.DebitValue * IIF(@Status = 2, 1, -1)) DebitValue, 
	--							SUM(jvd.CreditValue * IIF(@Status = 2, 1, -1)) CreditValue
	--					FROM GeneralLedger.JournalVoucherDetails jvd
	--					WHERE jvd.IdAccounting = @MovementJournalVoucherId
	--					GROUP BY jvd.IdMainAccount, jvd.IdThirdParty, jvd.IdCostCenter
	--				) jvd
	--				JOIN GeneralLedger.GeneralLedgerBalance glb ON jvd.Year = glb.Year AND jvd.Month = glb.Month
	--					AND jvd.IdMainAccount = glb.IdMainAccount AND ISNULL(jvd.IdThirdParty, 0) =ISNULL(glb.IdThirdParty, 0) AND ISNULL(jvd.IdCostCenter, 0) = ISNULL(glb.IdCostCenter, 0)

	--				INSERT INTO GeneralLedger.GeneralLedgerBalance
	--				(
	--					Year, Month, IdMainAccount, IdThirdParty, IdCostCenter, DebitValue, CreditValue
	--				)
	--				SELECT	jvd.Year, jvd.Month, jvd.IdMainAccount, jvd.IdThirdParty, jvd.IdCostCenter, jvd.DebitValue, jvd.CreditValue
	--				FROM 
	--				(
	--					SELECT	YEAR(@VoucherDate) Year, @MonthMovement Month,
	--							jvd.IdMainAccount, jvd.IdThirdParty, jvd.IdCostCenter,
	--							SUM(jvd.DebitValue * IIF(@Status = 2, 1, -1)) DebitValue, 
	--							SUM(jvd.CreditValue * IIF(@Status = 2, 1, -1)) CreditValue
	--					FROM GeneralLedger.JournalVoucherDetails jvd
	--					WHERE jvd.IdAccounting = @MovementJournalVoucherId
	--					GROUP BY jvd.IdMainAccount, jvd.IdThirdParty, jvd.IdCostCenter
	--				) jvd
	--				LEFT JOIN GeneralLedger.GeneralLedgerBalance glb ON jvd.Year = glb.Year AND jvd.Month = glb.Month
	--					AND jvd.IdMainAccount = glb.IdMainAccount AND ISNULL(jvd.IdThirdParty, 0) =ISNULL(glb.IdThirdParty, 0) AND ISNULL(jvd.IdCostCenter, 0) = ISNULL(glb.IdCostCenter, 0)
	--				WHERE glb.Id IS NULL
	--			END
	--			-------------------------------------------------------------------------------------------------------

	--			SELECT @Message_Output = CONCAT(ISNULL(@Message_Output + CHAR(13) + CHAR(10), ''), CASE @Status
	--						WHEN 2 THEN 'Se guardó y confirmó el Comprobante Contable '
	--						WHEN 3 THEN 'Se anuló el Comprobante Contable '
	--						WHEN 4 THEN 'Se desconfirmó el Comprobante Contable '
	--						ELSE 'Se guardó el Comprobante Contable '
	--					END, jv.Consecutive, ' de tipo ', jvt.Name, ' del libro contable ', lb.Name)
	--			FROM GeneralLedger.LegalBook lb WITH (NOLOCK)
	--			JOIN GeneralLedger.JournalVouchers jv WITH (NOLOCK) ON lb.Id = jv.LegalBookId
	--			JOIN GeneralLedger.JournalVoucherTypes jvt WITH (NOLOCK) ON jv.IdJournalVoucher = jvt.Id
	--			WHERE jv.Id = @MovementJournalVoucherId

	--		END
	--	END
		/****************************************************** ******************************************************/
		--SELECT	@CodeMessage = 0, 
		--		@Message = ISNULL(@Message_Output, CASE @Status
		--			WHEN 2 THEN 'Se guardó y confirmó el Comprobante Contable'
		--			WHEN 3 THEN 'Se anuló el Comprobante Contable'
		--			WHEN 4 THEN 'Se desconfirmó el Comprobante Contable'
		--			ELSE 'Se guardó el Comprobante Contable '
		--		END),
		--		@IdJournalVoucherResult = ISNULL((SELECT TOP 1 JournalVoucherId FROM @TableBookMovement ORDER BY IIF(LegalBookId = @LegalBookId, 0, LegalBookId)), @IdJournalVoucher)

		SELECT	@CodeMessage = 999, 
				@Message = 'Procedimiento incorrecto',
				@IdJournalVoucherResult = 0
	--END TRY
	--BEGIN CATCH
	--	SELECT	@CodeMessage = 999, 
	--			@Message = CONCAT('Se presentó un error al crear el comprobante contable: ', ERROR_MESSAGE(), ' - Linea: ', ERROR_LINE()),
	--			@IdJournalVoucherResult = 0
	--END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento del módulo de Contabilidad General (GeneralLedger) encargado de guardar o registrar un comprobante contable (journal voucher) a partir de un XML con la información de cabecera y detalle del comprobante. Recibe el XML del comprobante, el usuario que ejecuta la operación y devuelve como parámetros de salida el código de resultado, un mensaje descriptivo del proceso y el identificador del comprobante contable generado o actualizado. Gestiona la afectación de libros contables (oficiales y auxiliares), movimientos de débito y crédito por cuenta, tercero y centro de costo, soportando además procesos de homologación entre libros. Es el punto de entrada principal para la contabilización de documentos financieros como facturas, traslados de cartera, notas, liquidaciones de nómina y reconocimiento de ingresos en el libro mayor.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_SaveJournalVoucher_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_SaveJournalVoucher_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Procedimiento cuyo cuerpo funcional está completamente comentado; en su forma activa solo retorna un mensaje de error genérico ("Procedimiento incorrecto") sin ejecutar lógica de guardado de comprobantes contables.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_SaveJournalVoucher_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se deben proveer los parámetros de entrada (XML del comprobante y usuario), aunque no son utilizados por la lógica activa.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_SaveJournalVoucher_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El procedimiento nunca modifica datos en su estado actual: toda la lógica de validación, inserción y contabilización está comentada.; La salida es determinística: código 999 y mensaje ''Procedimiento incorrecto''.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_SaveJournalVoucher_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'comprobante contable (referenciado en el mensaje de salida y nombre del objeto)', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_SaveJournalVoucher_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (parámetros de salida): Siempre retorna @CodeMessage=999, @Message=''Procedimiento incorrecto'' y @IdJournalVoucherResult=0, sin importar la entrada.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_SaveJournalVoucher_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_SaveJournalVoucher_Output';
-- GO
