
-- =============================================
-- Author:		Diego A. Roldán
-- ALTER date: 2017-07-25
-- Description:	Genera Reconocimiento de Ingresos
-- =============================================
CREATE PROCEDURE [Billing].[SP_GenerateRecognition]
	@CareGroupId as int,
	@CareGroupTotal as decimal(18, 0),
	@OperativeUnitId int,
	@recognitionDate as datetime,
	@CodeUser as varchar(20)
AS
BEGIN
	SET NOCOUNT ON;
	
	begin try			
		--Tablas y variables necesarias
		declare @RevenueRecognitionJournalVoucherTypeId int, @careGroupCode varchar(20), @careGroupName varchar(100), @MessageReturn varchar(max) = '', @LegalBookId int, @JournalVoucherId int, @JournalVoucherConsecutive bigint, @revenueRecognitionId int, @UserId int, @newBalance decimal(18,0), @contractAccountingStructureId int
		declare @tmpTableDetail as table (MainAccountId int, ThirdPartyId int, CostCenterId int, DebitValue numeric(18,2), CreditValue numeric(18,2), Detail varchar(300))
		declare @TableJournalVoucher table(IdJournalVoucher int NOT NULL, LegalBookId int not null, VoucherDate datetime NOT NULL, Imported bit NOT NULL, [Status] tinyint NOT NULL, Detail varchar(500) NULL, EntityCode varchar(20) NULL, EntityId int NULL, EntityName varchar(250) NULL, IsClosedYear bit NOT NULL)
		declare @TableJournalVoucherDetail table(IdMainAccount int NOT NULL, IdThirdParty int NULL, IdCostCenter int NULL,DebitValue decimal(18, 2) NOT NULL,CreditValue decimal(18, 2) NOT NULL,Detail varchar(max) NULL,IdRetention int NULL,RetentionRate decimal(5, 2) NULL,BaseValue decimal(18, 0) NULL,BillingValue decimal(18, 0) NULL)

		select @careGroupCode = Code, @careGroupName = [Name] from Contract.CareGroup where Id = @CareGroupId
		select @UserId = Id from [Security].[User] where UserCode = @CodeUser

		select @RevenueRecognitionJournalVoucherTypeId = RecognitionJournalVoucherTypeId from Billing.SettingsBilling bs where bs.IdOperatingUnit = @OperativeUnitId		
		select @LegalBookId = Id  from GeneralLedger.LegalBook where OfficialBook = 1

		-- verificamos que no existan reconocimiento de ingresos ya realizados
		if EXISTS(SELECT 1 FROM Billing.RevenueRecognition WHERE CareGroupId = @CareGroupId AND State = 1)
		begin
			select convert(bit, 0) as StatusResult, CONCAT('Existe un reconocimiento de ingreso activo para el grupo de atención: ', @careGroupCode, ' - ', @careGroupName) as MessageResult, '' as JournalVoucherConsecutive, '' as JournalVoucherConsecutive, '' as JournalVoucherCodeName, 0 as RevenueRecognitionId
			return
		end

		if @CareGroupTotal = 0
		begin
			select convert(bit, 1) as StatusResult, CONCAT('El grupo de atención ', @careGroupCode, ' - ', @careGroupName, ' No tiene un valor a reconocer.') as MessageResult, '' as JournalVoucherConsecutive, '' as JournalVoucherConsecutive, '' as JournalVoucherCodeName, @revenueRecognitionId as RevenueRecognitionId
			return
		end

		-- Guardamos los estados del los folios
		INSERT INTO [Billing].[RevenueRecognitionFolioStatus]
           ([RevenueControlDetailId]
           ,[Status])
		select rcd.Id, rcd.[Status]
		FROM [Billing].[RevenueControlDetail] rcd
		inner join Billing.RevenueControl rc on rc.Id = rcd.RevenueControlId
		inner join dbo.ADINGRESO i on i.NUMINGRES = rc.AdmissionNumber
		where rcd.CareGroupId = @CareGroupId And rcd.[Status] in (1, 3) and i.IESTADOIN IN (' ', 'P','B')
		and rcd.IsMasterAccount <> 3

		-- actualizamos el estado a las controles de liquidación a fin de que no sean modificados mientras se realiza el proceso
		update rcd
			set [Status] = 5
		FROM [Billing].[RevenueControlDetail] rcd
		inner join Billing.RevenueControl rc on rc.Id = rcd.RevenueControlId
		inner join dbo.ADINGRESO i on i.NUMINGRES = rc.AdmissionNumber
		where rcd.CareGroupId = @CareGroupId And rcd.[Status] in (1, 3) and i.IESTADOIN IN (' ', 'P','B')
		and rcd.IsMasterAccount <> 3

		--guardamos el reconocimiento de ingreso
		insert into Billing.RevenueRecognition (CareGroupId, JournalVoucherTypeId, OperativeUnitId, TotalCareGroup, VoucherDate, State, CreationUser, CreationDate)
		values(@CareGroupId, @RevenueRecognitionJournalVoucherTypeId, @OperativeUnitId, @CareGroupTotal, @recognitionDate, 1, @UserId, [Common].[GETDATE]())

		set @revenueRecognitionId = SCOPE_IDENTITY()
		declare @LiquidateMaster bit = isnull((select LiquidateMasterAccount from Billing.SettingsBilling where IdOperatingUnit = @OperativeUnitId), 0)
	

		select  @contractAccountingStructureId = ContractAccountingStructureId
		from [Contract].CareGroup where Id = @CareGroupId

		--tabla de detalles de reconocimiento de ingresos donde se indica que folios fueron afectados
		INSERT INTO Billing.RevenueRecognitionDetail 
		(
			[RevenueRecognitionId], [RevenueControlDetailId], [ServiceOrderDetailId], [RecordType],
			[CUPSEntityId], [IPSServiceId], [ProductId], [BillingConceptId], 
			[PerformsFunctionalUnitId], [CostCenterId], 
			[IncomeRecognitionMainAccountId], [ServicesPendingBillingMainAccountId],
			[InvoicedQuantity], [RateManualSalePrice], [GrandTotalSalesPrice],
			[ThirdPartySalesPrice], [GrandTotalDiscount], [SubTotalPatientSalesPrice], [AdmissionNumber]
		)
			SELECT	@revenueRecognitionId, rcd.Id, sod.Id, sod.RecordType,
					sod.CUPSEntityId, sod.IPSServiceId, sod.ProductId, sod.BillingConceptId, 
					sod.PerformsFunctionalUnitId, sod.CostCenterId, 
					[Billing].[fnGetIncomeRecognitionPendingBillingMainAccountId](sod.RecordType, bc.Id, bc.AccountingType, bc.IncomeRecognitionPendingBillingMainAccountId, sod.IncomeMainAccountId, f.UnitType), cas.ServicesPendingBillingMainAccountId,
					sod.InvoicedQuantity, sod.RateManualSalePrice, sodd.GrandTotalSalesPrice,
					iif(@LiquidateMaster = 1, sodd.GrandTotalSalesPrice, sodd.ThirdPartySalesPrice + IIF(cg.CareGroupType = 3, sodd.SubTotalPatientSalesPrice, 0)), sodd.GrandTotalDiscount, IIF(cg.CareGroupType = 3, 0, sodd.SubTotalPatientSalesPrice),
					rc.AdmissionNumber
			FROM Contract.CareGroup cg
			JOIN Billing.RevenueControlDetail rcd ON cg.Id = rcd.CareGroupId
			JOIN Billing.RevenueControl rc on rc.Id = rcd.RevenueControlId
			JOIN [Contract].ContractAccountingStructure cas on cas.Id = @contractAccountingStructureId
			JOIN Billing.ServiceOrderDetailDistribution sodd on sodd.RevenueControlDetailId = rcd.Id
			JOIN Billing.ServiceOrderDetail sod on sodd.ServiceOrderDetailId = sod.Id
			JOIN Payroll.FunctionalUnit f on f.Id = sod.PerformsFunctionalUnitId
			JOIN Contract.CUPSEntity ce on sod.CUPSEntityId = ce.Id
			JOIN Billing.BillingConcept bc on ce.BillingConceptId = bc.Id
			WHERE cg.Id = @CareGroupId And rcd.Status = 5 AND sod.IsDelete = 0 AND sod.SettlementType != 3 AND sod.GrandTotalSalesPrice > 0
		UNION ALL
			SELECT	@revenueRecognitionId, rcd.Id, sod.Id, sod.RecordType,
					sod.CUPSEntityId, sod.IPSServiceId, sod.ProductId, sod.BillingConceptId, 
					sod.PerformsFunctionalUnitId, sod.CostCenterId, 
					pg.IncomeRecognitionMainAccountId, cas.ServicesPendingBillingMainAccountId,
					sod.InvoicedQuantity, sod.RateManualSalePrice, sodd.GrandTotalSalesPrice,
					iif(@LiquidateMaster = 1, sodd.GrandTotalSalesPrice, sodd.ThirdPartySalesPrice + IIF(cg.CareGroupType = 3, sodd.SubTotalPatientSalesPrice, 0)), sodd.GrandTotalDiscount, IIF(cg.CareGroupType = 3, 0, sodd.SubTotalPatientSalesPrice),
					rc.AdmissionNumber
			FROM Contract.CareGroup cg
			JOIN Billing.RevenueControlDetail rcd ON cg.Id = rcd.CareGroupId
			JOIN Billing.RevenueControl rc on rc.Id = rcd.RevenueControlId
			JOIN [Contract].ContractAccountingStructure cas on cas.Id = @contractAccountingStructureId
			JOIN Billing.ServiceOrderDetailDistribution sodd on sodd.RevenueControlDetailId = rcd.Id
			JOIN Billing.ServiceOrderDetail sod on sodd.ServiceOrderDetailId = sod.Id
			JOIN Inventory.InventoryProduct ip on sod.ProductId = ip.Id
			JOIN Inventory.ProductGroup pg ON ip.ProductGroupId = pg.Id
			WHERE cg.Id = @CareGroupId And rcd.Status = 5 AND sod.IsDelete = 0 AND sod.SettlementType != 3 AND sod.GrandTotalSalesPrice > 0
		
/*****************************************************************************************************************/
		--return
		---Inserto la cabecera del comprobante
		insert into @TableJournalVoucher 
			values (@RevenueRecognitionJournalVoucherTypeId, @LegalBookId, @recognitionDate, 0, 2, concat('Reconocimiento Ingreso Grupo de Atención ', @careGroupCode), @careGroupCode, @revenueRecognitionId, 'RevenueRecognition', 0)

		---- Inserto los detalles que se van acreditar
		insert into @tmpTableDetail
			exec [Billing].[SP_GenerateJournalVoucherDetailsRecognition] @revenueRecognitionId, @OperativeUnitId

		---- Inserto los detalles que se van acreditar
		insert into @TableJournalVoucherDetail
			select tdt.MainAccountId, tdt.ThirdPartyId, tdt.CostCenterId, tdt.DebitValue, tdt.CreditValue, tdt.Detail, null, null, null, null
			from @tmpTableDetail tdt

		----- Elimino los registros que no tienen valor
		delete from @TableJournalVoucherDetail where DebitValue = 0 and CreditValue = 0
		
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
		select	@JournalVoucherId = jv.Id,
				@JournalVoucherConsecutive = Consecutive
		from [GeneralLedger].[JournalVouchers] jv
		INNER JOIN GeneralLedger.LegalBook lb ON lb.Id = jv.LegalBookId
		where jv.EntityId = @revenueRecognitionId AND jv.EntityName = 'RevenueRecognition' AND lb.OfficialBook = 1

		UPDATE Billing.RevenueRecognition
			SET JournalVoucherId = @JournalVoucherId,
				JournalVoucherConsecutive = @JournalVoucherConsecutive
		WHERE Id = @revenueRecognitionId		

		select @MessageReturn += CONCAT(lb.Code, ' - ', lb.Name, ': Se generó el comprobante contable ', jv.Consecutive, ' de tipo ', jvt.Code, ' - ', jvt.Name)
		from [GeneralLedger].[JournalVouchers] jv
		JOIN GeneralLedger.LegalBook lb ON jv.LegalBookId = lb.Id
		JOIN GeneralLedger.JournalVoucherTypes jvt ON jv.IdJournalVoucher = jvt.Id
		where jv.EntityId = @revenueRecognitionId AND jv.EntityName = 'RevenueRecognition'
			
		select convert(bit, 1) as StatusResult, @MessageReturn as MessageResult, '' as JournalVoucherConsecutive, '' as JournalVoucherCodeName, @revenueRecognitionId as RevenueRecognitionId
	end try
	begin catch
		select convert(bit, 0) as StatusResult, CONCAT('Grupo de Atención ', @careGroupCode, ' - ', @careGroupName, ': ', 'Se ha producido un error!'+ ERROR_MESSAGE()) as MessageResult, '' as JournalVoucherConsecutive, '' as JournalVoucherCodeName, 0 as RevenueRecognitionId
	end catch	
END