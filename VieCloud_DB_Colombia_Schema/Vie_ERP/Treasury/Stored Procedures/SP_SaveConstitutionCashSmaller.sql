-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 18/01/2017
-- Description:	Procedimiento que se encarga de guardar, actualizar o confirmar un fondo de caja menor
-- =============================================
CREATE PROCEDURE [Treasury].[SP_SaveConstitutionCashSmaller] 
	@XmlObject as Xml,
	@CodeUser as varchar(20)
AS
BEGIN
	
	--Se declaran las variables para la cabecera de la provision
	declare @Id int, @Code varchar(20), @DocumentDate date, @DocumentType tinyint, @CashRegisterSmallerId int, @SourceType tinyint, @CashRegisterId int, @EntityBankAccountId int,
	@Value numeric(18,0), @OperatingUnitId int, @Status tinyint

	--Mensaje que se devuelve al usuario
	declare @MessageReturn varchar(max)

	--Tipo de comprobante contable para el form de fondos de caja menor
	declare @JournalVoucherTypeId int

	--Se declara una tabla con los datos para la cabecera del comprobante contable 
	declare @JournalVourcherTmp table (Id integer ,Consecutive bigint,LegalBookId integer,IdJournalVoucher integer,VoucherDate varchar(30),
	Imported varchar(5),[Status] tinyint,Detail varchar(500),EntityCode  varchar(20),EntityId integer,EntityName varchar(250),
	IsClosedYear TINYINT, CurrencyId INT)

	--Se declara una tabla temporal para los detalles del comprobante
	declare @JournalVourcherDetailTmp table (Id integer,IdAccounting integer,IdMainAccount integer,IdThirdParty integer,IdCostCenter integer,
	DebitValue decimal(18,2),CreditValue decimal(18,2),Detail varchar(500),IdRetention integer,RetentionRate decimal(5,2),
	BaseValue decimal(18,0),BillingValue decimal(18,0) )

	--Libro oficial
	declare @LegalBookId INTEGER
	--CurrencyTransaction
	declare @CurrencyTransactionId INTEGER

	--Tabla temporal para guardar el resultado del save del comprobante contable
	declare @resultJournalVoucher table (code varchar(20),MessageResult varchar(max),IdJournalVoucher integer)

	--Variable para obtener el xml
	declare @JournalVoucherXML as XML

	--Variable para saber los saldos
	declare @Balance decimal(18,0)

	Begin try
		
		--Se obtienen los datos del xml(ConstitutionCashSmaller)
		select	@Id = t.x.value('Id[1]','int'),
				@Code = t.x.value('Code[1]','varchar(20)'),
				@DocumentDate = convert(date, t.x.value('DocumentDate[1]','varchar(20)'), 103),
				@DocumentType = t.x.value('DocumentType[1]','tinyint'),
				@CashRegisterSmallerId = t.x.value('CashRegisterSmallerId[1]','int'),
				@SourceType = t.x.value('SourceType[1]','tinyint'),
				@CashRegisterId = case t.x.value('CashRegisterId[1]','int') when 0 then null else t.x.value('CashRegisterId[1]','int') end,
				@EntityBankAccountId = case t.x.value('EntityBankAccountId[1]','int') when 0 then null else t.x.value('EntityBankAccountId[1]','int') end,
				@Value = t.x.value('Value[1]','numeric(18,0)'),
				@OperatingUnitId = t.x.value('OperatingUnitId[1]','int'),
				@Status = t.x.value('Status[1]','tinyint')
		from @XmlObject.nodes('/ConstitutionCashSmaller') t(x)

		if @Status = 2 --Se validan si se está confirmando
		Begin
			--Se valida que hayan parámetros para la unidad operativa escogida
			if (select COUNT(*) from Treasury.SettingsTreasury where IdOperatingUnit = @OperatingUnitId) = 0
			Begin
				select 999 as CodeMessage, 'No existe parámetros para la unidad operativa escogida' as Message, '' as Code, 0 as Id
				return
			End

			--Se valida que el tipo de comprobante contable este diligenciado para el form
			select @JournalVoucherTypeId = JournalVoucherTypeConstitutionCashId from Treasury.SettingsTreasury where IdOperatingUnit = @OperatingUnitId
			if @JournalVoucherTypeId is null or @JournalVoucherTypeId = 0
			Begin
				select 999 as CodeMessage, 'No está parametrizado el tipo de comprobante contable fondo de caja menor en parámetros' as Message, '' as Code, 0 as Id
				return
			End
		End

		--Se crea el consecutivo siempre y cuando el código este vacío
		if @Code = '' And @Id = 0
		Begin
			-- Consultamos la secuencia numerica del form
			declare @idSequenceDetail int
			declare @pattern varchar(300)
			declare @NextS int
			select @pattern = cs.Pattern, @NextS = bsd.[Next] , @idSequenceDetail = bsd.Id  
			from Treasury.TreasurySequenceDetail bsd 
			inner join Treasury.TreasurySequence bs on bs.Id = bsd.IdSequenseTreasuryC 
			inner join Common.Sequense cs on cs.Id = bsd.IdSequense
			where bs.IdForm = '1947'
			if (@idSequenceDetail is null)
			Begin
			 select 999 as CodeMessage, 'No se encontró secuencia numérica para el formulario' as Message, '' as Code, 0 as Id
			 return
			End
			select @Code = dbo.GetSequence('',@pattern,@NextS)
			update Treasury.TreasurySequenceDetail set [Next] += 1 where Id = @idSequenceDetail
		End
		
		--Se empieza el registro del objeto
		declare @ConfirmationUser as varchar(20) = case when @Status <> 2 then null else @CodeUser end
		declare @ConfirmationDate as datetime = case when @Status <> 2 then null else [Common].[GETDATE]() end

		if @Id = 0 --Si el registro es nuevo guardo
		Begin
			--Inserto
			INSERT INTO Treasury.ConstitutionCashSmaller(Code, DocumentDate, DocumentType, CashRegisterSmallerId, SourceType, CashRegisterId, EntityBankAccountId, Value, [Status], OperatingUnitId,
			CreationUser, CreationDate, ConfirmationUser, ConfirmationDate)
			values (@Code, @DocumentDate, @DocumentType, @CashRegisterSmallerId, @SourceType, @CashRegisterId, @EntityBankAccountId, @Value, @Status, @OperatingUnitId,
			@CodeUser, [Common].[GETDATE](), @ConfirmationUser, @ConfirmationDate)
			
			--Obtengo el id generado
			set @Id = SCOPE_IDENTITY()

			if @Status = 1 --Se llena la variable mensaje, con el texto de guardar
			Begin
				set @MessageReturn = 'Se guardó correctamente con código ' + @Code
			End
		End
		Else --Si se esta modificando
		Begin
			--Actualizo
			declare @AnnulmentUser as varchar(20) = case when @Status <> 3 then null else @CodeUser end
			declare @AnnulmentDate as datetime = case when @Status <> 3 then null else [Common].[GETDATE]() end

			Update Treasury.ConstitutionCashSmaller
			set Code = @Code, DocumentDate = @DocumentDate, DocumentType = @DocumentType, CashRegisterSmallerId = @CashRegisterSmallerId, SourceType = @SourceType, 
			CashRegisterId = @CashRegisterId, EntityBankAccountId = @EntityBankAccountId, Value = @Value, [Status] = @Status, OperatingUnitId = @OperatingUnitId,
			ModificationUser = @CodeUser, ModificationDate = [Common].[GETDATE](),
			ConfirmationUser = @ConfirmationUser, ConfirmationDate = @ConfirmationDate, AnnulmentUser = @AnnulmentUser, AnnulmentDate = @AnnulmentDate
			where Id = @Id

			if @Status = 1 --Se llena la variable mensaje, con el texto de actualizar
			Begin
				set @MessageReturn = 'Se actualizó correctamente'
			End
			Else if @Status = 3 --Se llena la variable mensaje, con el texto de anular
			Begin
				set @MessageReturn = 'Se anuló correctamente'
			End
		End

		--Se realiza el update a la caja menor siempre y cuando se esté confirmando
		if @Status = 2 --Confirmando
		Begin
			INSERT INTO Treasury.TreasuryBalance
			(
				DocumentNumber, DocumentDate, DocumentType, Nature, CashRegisterId, EntityBankAccountId,
				PreviousBalance, ValueMovement, CreationDate
			)
			SELECT ccs.*
			FROM
			(
					SELECT	ccs.Code DocumentNumber, 
							ccs.DocumentDate, 
							5 DocumentType,
							IIF(ccs.DocumentType = 1, 1, 2) Nature,
							ccs.CashRegisterSmallerId CashRegisterId,
							NULL EntityBankAccountId,
							cr.CurrentBalance PreviousBalance,
							ccs.Value ValueMovement,
							ccs.ConfirmationDate CreationDate
					FROM Treasury.ConstitutionCashSmaller ccs
					LEFT JOIN Treasury.CashRegisters cr ON ccs.CashRegisterSmallerId = cr.Id
					WHERE ccs.Id = @Id AND ccs.Status = 2
				UNION ALL
					SELECT	ccs.Code DocumentNumber, 
							ccs.DocumentDate, 
							5 DocumentType,
							IIF(ccs.DocumentType = 1, 2, 1) Nature,
							ccs.CashRegisterId,
							ccs.EntityBankAccountId,
							ISNULL(IIF(ccs.SourceType = 1, cr.CurrentBalance, eba.CurrentBalance), 0) PreviousBalance,
							ccs.Value ValueMovement,
							ccs.ConfirmationDate CreationDate
					FROM Treasury.ConstitutionCashSmaller ccs
					LEFT JOIN Treasury.CashRegisters cr ON ccs.CashRegisterId = cr.Id
					LEFT JOIN Treasury.EntityBankAccounts eba ON ccs.EntityBankAccountId = eba.Id
					WHERE ccs.Id = @Id AND ccs.Status = 2
			) ccs
			LEFT JOIN Treasury.TreasuryBalance tb 
				ON tb.DocumentType = 5 
					AND ccs.DocumentNumber = tb.DocumentNumber 
					AND 
					(
						ISNULL(ccs.CashRegisterId, 0) = ISNULL(tb.CashRegisterId, 0)
						OR
						ISNULL(ccs.EntityBankAccountId, 0) = ISNULL(tb.EntityBankAccountId, 0)
					)
			WHERE tb.Id IS NULL

			--Obtengo el libro oficial
			select @LegalBookId = Id  from GeneralLedger.LegalBook where OfficialBook = 1
			SET @CurrencyTransactionId = (SELECT cr.CurrencyId FROM Treasury.CashRegisters cr WHERE cr.Id = @CashRegisterSmallerId)

			--Inserto la cabecera del comprobante contable
			insert into @JournalVourcherTmp(Id,Consecutive,LegalBookId,IdJournalVoucher,VoucherDate,Imported,[Status],Detail,EntityCode,
			EntityId,EntityName,IsClosedYear, CurrencyId)
			values(0,0,@LegalBookId,@JournalVoucherTypeId,@DocumentDate,'False',2,
			'Comprobante contable generado desde Fondo de Caja Menor',@Code,@Id,'ConstitutionCashSmaller',0, @CurrencyTransactionId)

			if @DocumentType = 1 --Aumento caja menor
			Begin

				--Debito caja menor
				insert into  @JournalVourcherDetailTmp
				select 0,0,
				ma.Id,
				case ma.HandlesThirdParty when 1 then crs.ThirdPartyId else NULL end as ThirdPartyId ,
				case ma.HandlesCostCenter when 1 then crs.IdCostCenter else NULL end as CostCenterId,
				@Value,
				0,
				'',NULL,0,0,0 
				from Treasury.CashRegisters crs
				inner join GeneralLedger.MainAccounts ma on ma.Id = crs.IdMainAccount
				where crs.Id = @CashRegisterSmallerId	

				--Se aumenta el saldo en la caja menor
				update Treasury.CashRegisters set CurrentBalance += @Value where Id = @CashRegisterSmallerId 

				--Cuando se aumenta la caja menor, la caja mayor o banco disminuye
				if @CashRegisterId <> null or @CashRegisterId > 0
				Begin

					--Credito caja mayor
					insert into  @JournalVourcherDetailTmp
					select 0,0,
					ma.Id,
					case ma.HandlesThirdParty when 1 then crs.ThirdPartyId else NULL end as ThirdPartyId ,
					case ma.HandlesCostCenter when 1 then crs.IdCostCenter else NULL end as CostCenterId,
					0,
					@Value,
					'',NULL,0,0,0 
					from Treasury.CashRegisters crs
					inner join GeneralLedger.MainAccounts ma on ma.Id = crs.IdMainAccount
					where crs.Id = @CashRegisterId

					--Se obtiene el saldo de la caja mayor para validar
					select @Balance = CurrentBalance from Treasury.CashRegisters where Id = @CashRegisterId

					--Se valida que el valor digitado no sea mayor al saldo de la caja mayor
					if @Value > @Balance
					Begin
						select 999 as CodeMessage, 'El valor digitado ' + CAST(@Value as varchar(20)) + ' es mayor al saldo de la caja mayor ' + CAST(@Balance as varchar(20)) as Message, '' as Code, 0 as Id
						return
					End

					update Treasury.CashRegisters set CurrentBalance -= @Value where Id = @CashRegisterId
				End
				Else if @EntityBankAccountId <> null or @EntityBankAccountId > 0
				Begin

					--Credito cuenta bancaria entidades
					insert into  @JournalVourcherDetailTmp
					select 0,0,
					ma.Id,
					case ma.HandlesThirdParty when 1 then eba.ThirdPartyId else NULL end as ThirdPartyId ,
					case ma.HandlesCostCenter when 1 then eba.IdCostCenter else NULL end as CostCenterId,
					0,
					@Value,
					'',NULL,0,0,0 
					from Treasury.EntityBankAccounts eba
					inner join GeneralLedger.MainAccounts ma on ma.Id = eba.IdMainAccount
					where eba.Id = @EntityBankAccountId

					--Se obtiene el saldo de la cuenta bancaria entidades para validar
					select @Balance = CurrentBalance from Treasury.EntityBankAccounts where Id = @EntityBankAccountId

					--Se valida que el valor digitado no sea mayor al saldo de la cuenta bancaria entidades
					if @Value > @Balance
					Begin
						select 999 as CodeMessage, 'El valor digitado ' + CAST(@Value as varchar(20)) + ' es mayor al saldo de la cuenta bancaria ' + CAST(@Balance as varchar(20)) as Message, '' as Code, 0 as Id
						return
					End

					update Treasury.EntityBankAccounts set CurrentBalance -= @Value where Id = @EntityBankAccountId
				End
			End
			Else
			Begin --Disminución caja menor

				--Se valida que cuando el DocumentType sea de disminución el valor digitado no sea mayor al saldo de la caja
				--Se obtiene el saldo de la caja
				select @Balance = CurrentBalance from Treasury.CashRegisters where Id = @CashRegisterSmallerId

				--Se valida que el valor digitado no sea mayor al saldo
				if @Value > @Balance
				Begin
					select 999 as CodeMessage, 'El valor digitado ' + CAST(@Value as varchar(20)) + ' es mayor al saldo de la caja menor ' + CAST(@Balance as varchar(20)) as Message, '' as Code, 0 as Id
					return
				End

				--Credito caja menor
				insert into  @JournalVourcherDetailTmp
				select 0,0,
				ma.Id,
				case ma.HandlesThirdParty when 1 then crs.ThirdPartyId else NULL end as ThirdPartyId ,
				case ma.HandlesCostCenter when 1 then crs.IdCostCenter else NULL end as CostCenterId,
				0,
				@Value,
				'',NULL,0,0,0 
				from Treasury.CashRegisters crs
				inner join GeneralLedger.MainAccounts ma on ma.Id = crs.IdMainAccount
				where crs.Id = @CashRegisterSmallerId

				--Se disminuye el saldo en la caja menor
				update Treasury.CashRegisters set CurrentBalance -= @Value where Id = @CashRegisterSmallerId
				
				--Cuando se disminuye la caja menor, la caja mayor o banco aumenta
				if @CashRegisterId <> null or @CashRegisterId > 0
				Begin
				
					--Debito caja mayor
					insert into  @JournalVourcherDetailTmp
					select 0,0,
					ma.Id,
					case ma.HandlesThirdParty when 1 then crs.ThirdPartyId else NULL end as ThirdPartyId ,
					case ma.HandlesCostCenter when 1 then crs.IdCostCenter else NULL end as CostCenterId,
					@Value,
					0,
					'',NULL,0,0,0 
					from Treasury.CashRegisters crs
					inner join GeneralLedger.MainAccounts ma on ma.Id = crs.IdMainAccount
					where crs.Id = @CashRegisterId

					update Treasury.CashRegisters set CurrentBalance += @Value where Id = @CashRegisterId
				End
				Else if @EntityBankAccountId <> null or @EntityBankAccountId > 0
				Begin
				
					--Debito cuenta bancaria entidades
					insert into  @JournalVourcherDetailTmp
					select 0,0,
					ma.Id,
					case ma.HandlesThirdParty when 1 then eba.ThirdPartyId else NULL end as ThirdPartyId ,
					case ma.HandlesCostCenter when 1 then eba.IdCostCenter else NULL end as CostCenterId,
					@Value,
					0,
					'',NULL,0,0,0 
					from Treasury.EntityBankAccounts eba
					inner join GeneralLedger.MainAccounts ma on ma.Id = eba.IdMainAccount
					where eba.Id = @EntityBankAccountId

					update Treasury.EntityBankAccounts set CurrentBalance += @Value where Id = @EntityBankAccountId
				End
			End
			
			--Obtengo el xml para poder consumir el sp que guarda el comprobante contable
			select @JournalVoucherXML =  convert(xml, (select * from @JournalVourcherTmp JournalVoucher 
			inner join @JournalVourcherDetailTmp JournalVoucherDetail on JournalVoucher.Id = JournalVoucherDetail.IdAccounting For xml AUTO,TYPE, ELEMENTS))

			--Se consume el sp que guarda el comprobante contable
			insert @resultJournalVoucher exec GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @JournalVoucherXML, @CodeUser 

			--Se valida que no hayan errores en el guardado del comprobante contable
			if (select code  from @resultJournalVoucher) = '999' 
			Begin			
				declare @errorJV varchar(max)
				select @errorJV = MessageResult  from @resultJournalVoucher 
				select 999 as CodeMessage, @errorJV as Message, '' as Code, 0 as Id
				return
			End  

			--Se genera el mensaje a devolver
			set @MessageReturn = 'Se guardó y se confirmó correctamente con código ' + @Code
			select @MessageReturn = @MessageReturn + CHAR(13) + CHAR(10) + 'Se generó comprobante contable de tipo ' + Code + ' - ' + Name
			from GeneralLedger.JournalVoucherTypes where Id = @JournalVoucherTypeId

		End

		--Se retorna el ok
		select 0 as CodeMessage, @MessageReturn as Message, @Code as Code, @Id as Id
		
	end try
	begin catch

		--Se retorna el error
		select 999 as CodeMessage, ERROR_MESSAGE() + ' linea: ' + cast(ERROR_LINE() as varchar(20) )  as Message, '' as Code, 0 as Id

	end catch

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que gestiona el ciclo de vida completo de una constitución de caja menor: permite crear, actualizar, confirmar y anular registros de apertura o dotación de fondos a cajas menores. Al crear un registro nuevo genera automáticamente el consecutivo consultando las secuencias configuradas en Tesorería (TreasurySequenceDetail y TreasurySequence). Al confirmar, valida que existan parámetros de tesorería para la unidad operativa (SettingsTreasury) y que esté configurado el tipo de comprobante contable correspondiente, para luego generar el comprobante contable en el libro mayor (JournalVouchers). Registra el usuario y fecha de cada acción (creación, confirmación o anulación) y retorna mensajes de resultado al usuario sobre el estado de la operación.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_SaveConstitutionCashSmaller';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_SaveConstitutionCashSmaller';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El consecutivo del documento solo se genera cuando Code está vacío e Id=0, y siempre incrementa TreasurySequenceDetail.[Next] en 1.; Solo en confirmación (@Status=2) se afectan saldos de cajas/cuentas, se inserta en TreasuryBalance y se genera comprobante contable.; ConfirmationUser/ConfirmationDate solo se llenan cuando @Status=2; AnnulmentUser/AnnulmentDate solo cuando @Status=3.; TreasuryBalance se inserta con DocumentType=5 y solo si no existe previamente un movimiento con el mismo DocumentNumber y misma caja o cuenta bancaria.; La naturaleza (Nature) del movimiento se invierte entre la caja menor y su contraparte: si DocumentType=1 caja menor es 1 y contraparte 2; si no, se invierte.; El valor a mover nunca puede exceder el saldo actual de la fuente (caja menor en disminución, caja mayor o cuenta bancaria en aumento).; El comprobante contable se construye con LegalBookId del libro oficial (OfficialBook=1) y el JournalVoucherTypeConstitutionCashId parametrizado en SettingsTreasury.; La moneda del comprobante (CurrencyId) se toma de la caja menor (CashRegisters.CurrencyId).; Los errores en el SP de comprobantes abortan el flujo y retornan CodeMessage=999 con el mensaje propagado.; Cualquier excepción se captura y devuelve CodeMessage=999 con ERROR_MESSAGE() y línea.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveConstitutionCashSmaller';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Caja menor; Caja mayor; Cuenta bancaria de entidad; Saldo de tesorería; Comprobante contable; Secuencia / consecutivo de formulario; Libro oficial contable; Plan de cuentas (cuenta principal); Tercero y centro de costo contable; Unidad operativa; Constitución / aumento / disminución de caja menor; Anulación de documento', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveConstitutionCashSmaller';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Status = 2 (confirmación) → Valida parámetros de tesorería y tipo de comprobante; inserta en TreasuryBalance, arma comprobante contable, ajusta saldos de cajas/cuentas y llama a GeneralLedger.SP_CreateAndValidateJournalVoucherMovement else Solo guarda/actualiza/anula la cabecera en ConstitutionCashSmaller sin afectar saldos ni contabilidad; si @Code = '''' AND @Id = 0 → Obtiene secuencia del formulario IdForm=1947 vía dbo.GetSequence e incrementa Treasury.TreasurySequenceDetail.[Next]; si @Id = 0 → INSERT en Treasury.ConstitutionCashSmaller con CreationUser/CreationDate else UPDATE de la cabecera con ModificationUser/ModificationDate y, si @Status=3, AnnulmentUser/AnnulmentDate; si @DocumentType = 1 (aumento de caja menor) → Débito a la cuenta de caja menor por @Value y suma a CurrentBalance de la caja menor; crédito a caja mayor o cuenta bancaria origen y resta @Value a su CurrentBalance else Disminución de caja menor: crédito a caja menor y resta de su saldo; débito y suma de saldo a caja mayor o cuenta bancaria destino; si @DocumentType=1 con @CashRegisterId>0 y @Value > CurrentBalance de la caja mayor → Retorna CodeMessage 999 informando que el valor digitado supera el saldo de la caja mayor; si @DocumentType=1 con @EntityBankAccountId>0 y @Value > CurrentBalance de la cuenta bancaria → Retorna CodeMessage 999 informando que el valor digitado supera el saldo de la cuenta bancaria; si @DocumentType<>1 (disminución) y @Value > CurrentBalance de la caja menor → Retorna CodeMessage 999 informando que el valor digitado supera el saldo de la caja menor; si Resultado de SP_CreateAndValidateJournalVoucherMovement con code = ''999'' → Retorna CodeMessage 999 propagando el mensaje de error del comprobante contable y aborta else Concatena al mensaje el tipo y consecutivo del comprobante contable generado; si ma.HandlesThirdParty = 1 / ma.HandlesCostCenter = 1 → Asigna ThirdPartyId / IdCostCenter de la caja o cuenta bancaria al detalle del comprobante; en caso contrario NULL', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveConstitutionCashSmaller';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'GeneralLedger.SP_CreateAndValidateJournalVoucherMovement; Common.GETDATE; dbo.GetSequence', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveConstitutionCashSmaller';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Treasury.SettingsTreasury; Treasury.TreasurySequenceDetail; Treasury.TreasurySequence; Common.Sequense; Treasury.ConstitutionCashSmaller; Treasury.CashRegisters; Treasury.EntityBankAccounts; Treasury.TreasuryBalance; GeneralLedger.LegalBook; GeneralLedger.MainAccounts; GeneralLedger.JournalVouchers; GeneralLedger.JournalVoucherTypes', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveConstitutionCashSmaller';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveConstitutionCashSmaller';
-- GO
