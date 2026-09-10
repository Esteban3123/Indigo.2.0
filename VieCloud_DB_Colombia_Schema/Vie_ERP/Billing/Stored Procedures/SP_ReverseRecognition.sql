-- =============================================
-- Author:		Diego A. Roldán
-- Create date: 2017-08-01
-- Description:	Genera Reversión de Reconocimiento de Ingresos
-- =============================================
CREATE PROCEDURE [Billing].[SP_ReverseRecognition]
	@RevenueRecognitionId as int,
	@CodeUser as varchar(20)
AS
BEGIN
	SET NOCOUNT ON;

	begin try
		--Tablas y variables necesarias
		declare @CareGroupId int, @OperativeUnitId int, @recognitionDate datetime, @CareGroupTotal as decimal(18, 0)
		declare @ReverseRecognitionJournalVoucherTypeId int, @careGroupCode varchar(20), @careGroupName varchar(100), @MessageReturn varchar(max) = '', @LegalBookId int, @JournalVoucherId int, @JournalVoucherConsecutive bigint, @UserId int
		declare @TableJournalVoucher table(IdJournalVoucher int NOT NULL, LegalBookId int not null, VoucherDate datetime NOT NULL, Imported bit NOT NULL, [Status] tinyint NOT NULL, Detail varchar(500) NULL, EntityCode varchar(20) NULL, EntityId int NULL, EntityName varchar(250) NULL, IsClosedYear bit NOT NULL)
		declare @TableJournalVoucherDetail table(IdMainAccount int NOT NULL, IdThirdParty int NULL, IdCostCenter int NULL,DebitValue decimal(18, 2) NOT NULL,CreditValue decimal(18, 2) NOT NULL,Detail varchar(max) NULL,IdRetention int NULL,RetentionRate decimal(5, 2) NULL,BaseValue decimal(18, 0) NULL,BillingValue decimal(18, 0) NULL)
		
		select @CareGroupId = CareGroupId, @OperativeUnitId = OperativeUnitId, @recognitionDate = DATEADD(DAY, 1, VoucherDate), @CareGroupTotal = TotalCareGroup from Billing.RevenueRecognition where Id = @RevenueRecognitionId
		select @careGroupCode = Code, @careGroupName = [Name] from Contract.CareGroup where Id = @CareGroupId
		select @UserId = Id from [Security].[User] where UserCode = @CodeUser
		
		select @ReverseRecognitionJournalVoucherTypeId = ReverseRecognitionJournalVoucherTypeId from Billing.SettingsBilling bs where bs.IdOperatingUnit = @OperativeUnitId		
		
		-- verificamos que no existan reconocimiento de ingresos ya realizados
		if NOT EXISTS (SELECT 1 FROM Billing.RevenueRecognition WHERE Id = @RevenueRecognitionId AND State = 1)
		begin
			select convert(bit, 0) as StatusResult, CONCAT('No Existe un reconocimiento de ingreso activo para el grupo de atención: ', @careGroupCode, ' - ', @careGroupName) as MessageResult, '' as JournalVoucherConsecutive, '' as JournalVoucherCodeName, 0 as RevenueRecognitionId
			return
		end

		--actualizamos el reconocimiento de ingreso
		UPDATE Billing.RevenueRecognition
			SET State = 2				
		WHERE Id = @revenueRecognitionId

		-- actualizamos el estado a las controles de liquidación
		update rcd
			set [Status] = 1
		FROM [Billing].[RevenueControlDetail] rcd 
		--left JOIN Billing.RevenueRecognitionDetail rrd On rcd.Id = rrd.RevenueControlDetailId
		WHERE rcd.Status=5

		if @CareGroupTotal = 0
		begin
			select convert(bit, 1) as StatusResult, CONCAT('El grupo de atención ', @careGroupCode, ' - ', @careGroupName, ' No tiene un valor a reversar.') as MessageResult, '' as JournalVoucherConsecutive, '' as JournalVoucherCodeName, @revenueRecognitionId as RevenueRecognitionId
			return
		end
		
/************************* CONFIGURACIÓN DE VIEBOT DE IGUAL MANERA QUE EL RECONOCIMIENTO *************************/
		-- se insertan los libros que hagan falta
		IF EXISTS 
		(
			SELECT vb.Id
			FROM [GeneralLedger].[VieBot] vb 
			LEFT JOIN [GeneralLedger].[VieBot] vb2 ON vb2.Form = 'ReverseRevenueRecognition' AND vb.HandlesHomologation = vb2.HandlesHomologation AND vb.LegalBookId = vb2.LegalBookId AND vb.Allow = vb2.Allow
			WHERE vb.Form = 'RevenueRecognition' AND vb2.Id IS NULL
		)
		BEGIN
			INSERT INTO [GeneralLedger].[VieBot] (Form, HandlesHomologation, LegalBookId, Allow, CreationUser, CreationDate, ModificationUser, ModificationDate)
				SELECT 'ReverseRevenueRecognition', vb.HandlesHomologation, vb.LegalBookId, vb.Allow, vb.CreationUser, vb.CreationDate, vb.ModificationUser, vb.ModificationDate
				FROM [GeneralLedger].[VieBot] vb 
				LEFT JOIN [GeneralLedger].[VieBot] vb2 ON vb2.Form = 'ReverseRevenueRecognition' AND vb.HandlesHomologation = vb2.HandlesHomologation AND vb.LegalBookId = vb2.LegalBookId AND vb.Allow = vb2.Allow
				WHERE vb.Form = 'RevenueRecognition' AND vb2.Id IS NULL
		END

		-- se eliminan los libros que tenga configurado de mas
		IF EXISTS 
		(
			SELECT vb.Id
			FROM [GeneralLedger].[VieBot] vb 
			LEFT JOIN [GeneralLedger].[VieBot] vb2 ON vb2.Form = 'RevenueRecognition' AND vb.HandlesHomologation = vb2.HandlesHomologation AND vb.LegalBookId = vb2.LegalBookId AND vb.Allow = vb2.Allow
			WHERE vb.Form = 'ReverseRevenueRecognition' AND vb2.Id IS NULL
		)
		BEGIN
			DELETE vb 
			FROM [GeneralLedger].[VieBot] vb 
			LEFT JOIN [GeneralLedger].[VieBot] vb2 ON vb2.Form = 'RevenueRecognition' AND vb.HandlesHomologation = vb2.HandlesHomologation AND vb.LegalBookId = vb2.LegalBookId AND vb.Allow = vb2.Allow
			WHERE vb.Form = 'ReverseRevenueRecognition' AND vb2.Id IS NULL
		END

/*****************************************************************************************************************/

		set @LegalBookId = (select Id from GeneralLedger.LegalBook where OfficialBook = 1)

		select	@JournalVoucherId = Id
		FROM GeneralLedger.JournalVouchers jv
		WHERE jv.EntityId = @RevenueRecognitionId AND jv.EntityName = 'RevenueRecognition' and jv.LegalBookId = @LegalBookId
		ORDER BY Id

		---Inserto la cabecera del comprobante
		insert into @TableJournalVoucher
			values (@ReverseRecognitionJournalVoucherTypeId, @LegalBookId, @recognitionDate, 0, 2, concat('Reversión de Reconocimiento Ingreso Grupo de Atención ', @careGroupCode), @careGroupCode, @RevenueRecognitionId, 'ReverseRevenueRecognition', 0)
		
		---- Inserto los detalles que se van acreditar
		insert into @TableJournalVoucherDetail
			SELECT	jvd.IdMainAccount, jvd.IdThirdParty, jvd.IdCostCenter, jvd.CreditValue, jvd.DebitValue, jvd.Detail, null, null, null, null
			FROM GeneralLedger.JournalVoucherDetails jvd
			WHERE jvd.IdAccounting = @JournalVoucherId
		
		--- Realizo la interfaz con contabilidad
		declare @JournalXml xml = (select *
		from @TableJournalVoucher as JournalVoucher
		CROSS APPLY @TableJournalVoucherDetail as JournalVoucherDetail
		for xml auto, elements)

		declare @TableResultJournal table(CodeMessage varchar(20), [Message] varchar(max), IdJournalVoucher int)

		-- llamo el procedimiento encargado de la generación de los documentos contables
		insert into @TableResultJournal
			exec [GeneralLedger].[SP_CreateAndValidateJournalVoucherMovement] @JournalXml, @CodeUser
					
		if(select count(*) from @TableResultJournal where CodeMessage <> '0') > 0 begin
			select convert(bit, 0) as StatusResult, CONCAT('Grupo de Atención ', @careGroupCode, ' - ', @careGroupName, ': ', [Message]) as MessageResult, '' as JournalVoucherConsecutive, '' as JournalVoucherCodeName, 0 as RevenueRecognitionId from @TableResultJournal
			return
		end

/*****************************************************************************************************************/

		--tomamos el Id del comprobante contable generado
		select	@JournalVoucherId = Id,
				@JournalVoucherConsecutive = Consecutive
		from [GeneralLedger].[JournalVouchers] 
		where EntityId = @revenueRecognitionId AND EntityName = 'ReverseRevenueRecognition'
		ORDER BY Id

		UPDATE Billing.RevenueRecognition
			SET JournalVoucherReverseId = @JournalVoucherId,
				JournalVoucherTypeReverseId = @ReverseRecognitionJournalVoucherTypeId,
				JournalVoucherReverseConsecutive = @JournalVoucherConsecutive,
				VoucherDateReverse = @recognitionDate
		WHERE Id = @revenueRecognitionId

		select @MessageReturn += CONCAT('Se generó el comprobante contable de tipo ', jvt.Code, ' - ', jvt.Name)
		from GeneralLedger.JournalVoucherTypes jvt
		where jvt.Id = @ReverseRecognitionJournalVoucherTypeId

		select convert(bit, 1) as StatusResult, @MessageReturn as MessageResult, '' as JournalVoucherConsecutive, '' as JournalVoucherCodeName, @RevenueRecognitionId as RevenueRecognitionId
	end try
	begin catch
		select convert(bit, 0) as StatusResult, CONCAT('Grupo de Atención ', @careGroupCode, ' - ', @careGroupName, ': ', 'Se ha producido un error!'+ ERROR_MESSAGE()) as MessageResult, '' as JournalVoucherConsecutive, '' as JournalVoucherCodeName, 0 as RevenueRecognitionId
	end catch	
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera la reversión contable de un reconocimiento de ingresos previamente causado en facturación. Recibe el identificador del reconocimiento de ingresos y el código del usuario que ejecuta la operación; verifica que el reconocimiento esté en estado activo, lo marca como revertido, restablece el estado de los controles de liquidación asociados (RevenueControlDetail), sincroniza la configuración de libros contables del VieBot para el formulario de reversión (espejando los libros del reconocimiento original), y genera el comprobante contable de reversión invirtiendo débitos y créditos del comprobante original. Se usa en el cierre o anulación del proceso de causación de ingresos por grupo de atención dentro del módulo de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_ReverseRecognition';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_ReverseRecognition';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reversa contablemente un reconocimiento de ingresos por grupo de atención, invirtiendo débitos y créditos del comprobante original y liberando los controles de liquidación.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseRecognition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en Billing.RevenueRecognition con el Id recibido y State = 1 (activo); de lo contrario se aborta con mensaje de error.; Debe existir configuración en Billing.SettingsBilling para la unidad operativa del reconocimiento que defina ReverseRecognitionJournalVoucherTypeId.; Debe existir un libro contable oficial (GeneralLedger.LegalBook con OfficialBook = 1).; Debe existir el comprobante contable original asociado (EntityName=''RevenueRecognition'') del cual se tomarán los detalles a invertir.; El usuario identificado por @CodeUser debe existir en Security.User.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseRecognition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La fecha del comprobante de reversión es siempre el día siguiente a la fecha del comprobante de reconocimiento original (DATEADD(DAY,1,VoucherDate)).; El comprobante de reversión invierte exactamente débitos y créditos del comprobante original (CreditValue→DebitValue, DebitValue→CreditValue).; La configuración de VieBot para ''ReverseRevenueRecognition'' se mantiene siempre sincronizada (espejo) con la de ''RevenueRecognition'' por LegalBookId, HandlesHomologation y Allow.; La reversión solo opera sobre reconocimientos en State=1 y los deja en State=2.; Solo se trabaja con el libro contable oficial (LegalBook.OfficialBook = 1).; Cualquier error es capturado por TRY/CATCH y devuelto como StatusResult=0 sin propagar excepción.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseRecognition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Billing.RevenueRecognition: Cuando el reconocimiento existe y está activo (State=1), se marca como revertido fijando State = 2.; [UPDATE] Billing.RevenueControlDetail: Todos los detalles de control de ingresos con Status = 5 se devuelven a Status = 1 (liberados de la causación).; [INSERT] GeneralLedger.VieBot: Para cada configuración del Form ''RevenueRecognition'' que no tenga su contraparte en Form ''ReverseRevenueRecognition'' (con igual HandlesHomologation, LegalBookId y Allow), se inserta una fila espejo con Form=''ReverseRevenueRecognition''.; [DELETE] GeneralLedger.VieBot: Se eliminan las configuraciones de Form ''ReverseRevenueRecognition'' que no tengan equivalente en Form ''RevenueRecognition'' con mismos HandlesHomologation, LegalBookId y Allow (sincronización inversa).; [INSERT] GeneralLedger.JournalVouchers: Vía EXEC GeneralLedger.SP_CreateAndValidateJournalVoucherMovement se genera un comprobante de tipo ReverseRecognitionJournalVoucherTypeId con fecha = VoucherDate original + 1 día, EntityName=''ReverseRevenueRecognition'' y EntityId = Id del reconocimiento, invirtiendo débitos y créditos del comprobante original (CreditValue del original pasa a DebitValue y viceversa).; [UPDATE] Billing.RevenueRecognition: Tras generar el comprobante de reversión exitosamente, se actualizan JournalVoucherReverseId, JournalVoucherTypeReverseId, JournalVoucherReverseConsecutive y VoucherDateReverse con los datos del nuevo comprobante.; [RETURN_RESULT] RESULT: Devuelve resultset con StatusResult (bit), MessageResult, JournalVoucherConsecutive, JournalVoucherCodeName y RevenueRecognitionId; StatusResult=0 en errores (no activo, error en SP de contabilidad o excepción capturada) y StatusResult=1 cuando finaliza correctamente o cuando el total a reversar es 0.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseRecognition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si NOT EXISTS reconocimiento con Id = @RevenueRecognitionId AND State = 1 → Retorna StatusResult=0 con mensaje ''No Existe un reconocimiento de ingreso activo'' y termina sin más cambios. else Continúa con la reversión: actualiza estados y genera comprobante.; si @CareGroupTotal = 0 (RevenueRecognition.TotalCareGroup = 0) → Retorna StatusResult=1 con mensaje ''No tiene un valor a reversar'' y termina sin generar comprobante contable, pero después de haber actualizado State y RevenueControlDetail. else Procede a generar el comprobante contable de reversión.; si Existen filas en VieBot con Form=''RevenueRecognition'' sin contraparte ''ReverseRevenueRecognition'' → INSERT espejo en VieBot con Form=''ReverseRevenueRecognition''.; si Existen filas en VieBot con Form=''ReverseRevenueRecognition'' sin contraparte ''RevenueRecognition'' → DELETE de esas filas en VieBot.; si El SP de contabilidad devolvió algún CodeMessage <> ''0'' → Retorna StatusResult=0 con el mensaje de error del SP contable y termina. else Continúa actualizando RevenueRecognition con los datos del comprobante generado.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseRecognition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'GeneralLedger.SP_CreateAndValidateJournalVoucherMovement', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseRecognition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.RevenueRecognition; Contract.CareGroup; Security.User; Billing.SettingsBilling; Billing.RevenueControlDetail; GeneralLedger.VieBot; GeneralLedger.LegalBook; GeneralLedger.JournalVouchers; GeneralLedger.JournalVoucherDetails; GeneralLedger.JournalVoucherTypes', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseRecognition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseRecognition';
-- GO
