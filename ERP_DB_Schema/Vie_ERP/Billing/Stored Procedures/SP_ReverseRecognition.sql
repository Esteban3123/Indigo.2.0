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
			select convert(bit, 0) as StatusResult, CONCAT('No Existe un reconocimiento de ingreso activo para el grupo de atención: ', @careGroupCode, ' - ', @careGroupName) as MessageResult, '' as JournalVoucherConsecutive, '' as JournalVoucherConsecutive, '' as JournalVoucherCodeName, 0 as RevenueRecognitionId
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
			select convert(bit, 1) as StatusResult, CONCAT('El grupo de atención ', @careGroupCode, ' - ', @careGroupName, ' No tiene un valor a reversar.') as MessageResult, '' as JournalVoucherConsecutive, '' as JournalVoucherConsecutive, '' as JournalVoucherCodeName, @revenueRecognitionId as RevenueRecognitionId
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
			select convert(bit, 0) as StatusResult, CONCAT('Grupo de Atención ', @careGroupCode, ' - ', @careGroupName, ': ', [Message]) as MessageResult, '' as JournalVoucherConsecutive, '' as JournalVoucherConsecutive, '' as JournalVoucherCodeName, 0 as RevenueRecognitionId from @TableResultJournal
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

		select @MessageReturn += CONCAT(lb.Code, ' - ', lb.Name, ': Se generó el comprobante contable ', jv.Consecutive, ' de tipo ', jvt.Code, ' - ', jvt.Name)
		from [GeneralLedger].[JournalVouchers] jv
		JOIN GeneralLedger.LegalBook lb ON jv.LegalBookId = lb.Id
		JOIN GeneralLedger.JournalVoucherTypes jvt ON jv.IdJournalVoucher = jvt.Id
		where jv.EntityId = @revenueRecognitionId AND jv.EntityName = 'ReverseRevenueRecognition'

		select convert(bit, 1) as StatusResult, @MessageReturn as MessageResult, '' as JournalVoucherConsecutive, '' as JournalVoucherCodeName, @RevenueRecognitionId as RevenueRecognitionId
	end try
	begin catch
		select convert(bit, 0) as StatusResult, CONCAT('Grupo de Atención ', @careGroupCode, ' - ', @careGroupName, ': ', 'Se ha producido un error!'+ ERROR_MESSAGE()) as MessageResult, '' as JournalVoucherConsecutive, '' as JournalVoucherCodeName, 0 as RevenueRecognitionId
	end catch	
END