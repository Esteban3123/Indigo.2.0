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
			select convert(bit, 0) as StatusResult, CONCAT('Existe un reconocimiento de ingreso activo para el grupo de atención: ', @careGroupCode, ' - ', @careGroupName) as MessageResult, '' as JournalVoucherConsecutive, '' as JournalVoucherCodeName, 0 as RevenueRecognitionId
			return
		end

		if @CareGroupTotal = 0
		begin
			select convert(bit, 1) as StatusResult, CONCAT('El grupo de atención ', @careGroupCode, ' - ', @careGroupName, ' No tiene un valor a reconocer.') as MessageResult, '' as JournalVoucherConsecutive, '' as JournalVoucherCodeName, @revenueRecognitionId as RevenueRecognitionId
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
			SELECT	@revenueRecognitionId, 
			rcd.Id, 
			sod.Id, 
			sod.RecordType,
			sod.CUPSEntityId, 
			sod.IPSServiceId, 
			sod.ProductId, 
			sod.BillingConceptId, 
			sod.PerformsFunctionalUnitId, 
			sod.CostCenterId, 
			[Billing].[fnGetIncomeRecognitionPendingBillingMainAccountId](sod.RecordType, bc.Id, bc.AccountingType, bc.IncomeRecognitionPendingBillingMainAccountId, sod.IncomeMainAccountId, f.UnitType), 
			cas.ServicesPendingBillingMainAccountId,
			sod.InvoicedQuantity, 
			sod.RateManualSalePrice, 
			sodd.GrandTotalSalesPrice - sodd.GrandTotalTaxes,
			iif(@LiquidateMaster = 1, sodd.GrandTotalSalesPrice - sodd.GrandTotalTaxes, sodd.ThirdPartySalesPrice + IIF(cg.CareGroupType = 3, sodd.SubTotalPatientSalesPrice, 0)), 
			sodd.GrandTotalDiscount, 
			IIF(cg.CareGroupType = 3, 0, sodd.SubTotalPatientSalesPrice),
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
			SELECT	@revenueRecognitionId,
			rcd.Id, 
			sod.Id, 
			sod.RecordType,
			sod.CUPSEntityId, 
			sod.IPSServiceId, 
			sod.ProductId, 
			sod.BillingConceptId, 
			sod.PerformsFunctionalUnitId, 
			sod.CostCenterId, 
			pg.IncomeRecognitionMainAccountId, 
			cas.ServicesPendingBillingMainAccountId,
			sod.InvoicedQuantity, sod.RateManualSalePrice, 
			sodd.GrandTotalSalesPrice - sodd.GrandTotalTaxes,
			iif(@LiquidateMaster = 1, sodd.GrandTotalSalesPrice - sodd.GrandTotalTaxes, sodd.ThirdPartySalesPrice + IIF(cg.CareGroupType = 3, sodd.SubTotalPatientSalesPrice, 0)), 
			sodd.GrandTotalDiscount, 
			IIF(cg.CareGroupType = 3, 0, sodd.SubTotalPatientSalesPrice),
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

			DECLARE @MsgBase VARCHAR(MAX) = (SELECT TOP 1 [Message] FROM @TableResultJournal WHERE CodeMessage <> '0')
			DECLARE @MsgDetalle VARCHAR(MAX) = ''

			-- Solo enriquecer el mensaje cuando el error es específicamente de desbalanceo
			IF @MsgBase LIKE '%desbalanceado%'
			BEGIN
				SELECT @MsgDetalle = @MsgDetalle +
					CONCAT(CHAR(13), '  - Folio ', so.Code,
						   ' | Admisión ', RTRIM(rc.AdmissionNumber),
						   ' | Item: ', ISNULL(ip.Name, ISNULL(ips.Name, 'Servicio')),
						   ' | Débito: ', FORMAT(SUM(rrd.ThirdPartySalesPrice + rrd.SubTotalPatientSalesPrice + rrd.GrandTotalDiscount), 'N2'),
						   ' | Crédito: ', FORMAT(SUM(rrd.GrandTotalSalesPrice + rrd.GrandTotalDiscount), 'N2'),
						   ' | Diferencia: ', FORMAT(SUM(rrd.ThirdPartySalesPrice + rrd.SubTotalPatientSalesPrice + rrd.GrandTotalDiscount) - SUM(rrd.GrandTotalSalesPrice + rrd.GrandTotalDiscount), 'N2'))
				FROM Billing.RevenueRecognitionDetail rrd
				JOIN Billing.RevenueControlDetail rcd ON rrd.RevenueControlDetailId = rcd.Id
				JOIN Billing.RevenueControl rc ON rcd.RevenueControlId = rc.Id
				JOIN Billing.ServiceOrderDetail sod ON rrd.ServiceOrderDetailId = sod.Id
				JOIN Billing.ServiceOrder so ON sod.ServiceOrderId = so.Id
				LEFT JOIN Inventory.InventoryProduct ip ON sod.ProductId = ip.Id
				LEFT JOIN Contract.IPSService ips ON sod.IPSServiceId = ips.Id
				WHERE rrd.RevenueRecognitionId = @revenueRecognitionId
				GROUP BY so.Code, rc.AdmissionNumber, ISNULL(ip.Name, ISNULL(ips.Name, 'Servicio'))
				HAVING SUM(rrd.ThirdPartySalesPrice + rrd.SubTotalPatientSalesPrice + rrd.GrandTotalDiscount)
					<> SUM(rrd.GrandTotalSalesPrice + rrd.GrandTotalDiscount)
			END

			select convert(bit, 0) as StatusResult,
				CONCAT('Grupo de Atención ', @careGroupCode, ' - ', @careGroupName, ': ', @MsgBase,
					   IIF(LEN(@MsgDetalle) > 0, CONCAT(CHAR(13), 'Detalle por folio:', @MsgDetalle), '')) as MessageResult,
				'' as JournalVoucherConsecutive, '' as JournalVoucherCodeName,
				0 as RevenueRecognitionId
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

		select @MessageReturn += CONCAT('Se generó el comprobante contable de tipo ', jvt.Code, ' - ', jvt.Name)
		from GeneralLedger.JournalVoucherTypes jvt
		where jvt.Id = @RevenueRecognitionJournalVoucherTypeId
			
		select convert(bit, 1) as StatusResult, @MessageReturn as MessageResult, '' as JournalVoucherConsecutive, '' as JournalVoucherCodeName, @revenueRecognitionId as RevenueRecognitionId
	end try
	begin catch
		select convert(bit, 0) as StatusResult, CONCAT('Grupo de Atención ', @careGroupCode, ' - ', @careGroupName, ': ', 'Se ha producido un error!'+ ERROR_MESSAGE()) as MessageResult, '' as JournalVoucherConsecutive, '' as JournalVoucherCodeName, 0 as RevenueRecognitionId
	end catch	
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reconocimiento contable de ingresos por grupo de atención en el módulo de facturación. Toma los folios de liquidación activos (en estado pendiente o parcial) de las admisiones vigentes, congela su estado para evitar modificaciones durante el proceso, registra el comprobante contable (voucher) en el libro oficial según el tipo de comprobante configurado para la unidad operativa, y construye el detalle del reconocimiento vinculando cada folio con su cuenta contable de ingresos por reconocer y cuentas de servicios pendientes de facturación. Valida que no exista un reconocimiento activo previo para el mismo grupo de atención y que el total a reconocer sea mayor a cero antes de proceder. Integra configuraciones de facturación (SettingsBilling), grupos de atención contractuales (CareGroup), estructura contable del contrato, datos del libro mayor oficial (LegalBook), controles de ingresos por admisión (RevenueControl/RevenueControlDetail) y el registro histórico de estados de folios (RevenueRecognitionFolioStatus) para garantizar trazabilidad completa del ciclo de causación contable.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateRecognition';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateRecognition';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reconocimiento contable de ingresos para un grupo de atención: bloquea sus folios, registra el detalle por orden de servicio y produce el comprobante contable de causación asociado.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateRecognition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'No debe existir un Billing.RevenueRecognition activo (State=1) para el CareGroupId; si existe, se aborta con mensaje de error.; El total a reconocer (@CareGroupTotal) debe ser distinto de 0; si es 0 se retorna sin generar reconocimiento.; Debe existir configuración Billing.SettingsBilling para la unidad operativa con RecognitionJournalVoucherTypeId definido.; Debe existir un libro legal oficial (GeneralLedger.LegalBook.OfficialBook = 1).; Los folios a procesar en RevenueControlDetail deben estar en Status 1 o 3, con IsMasterAccount <> 3, y la admisión asociada (ADINGRESO.IESTADOIN) debe estar en ('' '',''P'',''B'').', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateRecognition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Billing.RevenueRecognitionFolioStatus: Antes de procesar, se respalda el Status original de cada RevenueControlDetail del CareGroup cuyos folios cumplen Status IN (1,3), IsMasterAccount<>3 y la admisión está en estado ('' '',''P'',''B'').; [UPDATE] Billing.RevenueControlDetail: Tras respaldar, se cambia Status=5 a esos mismos folios para bloquearlos durante el reconocimiento de ingresos.; [INSERT] Billing.RevenueRecognition: Se crea la cabecera del reconocimiento con State=1 (activo), tipo de comprobante tomado de SettingsBilling.RecognitionJournalVoucherTypeId, fecha, total y usuario auditor.; [INSERT] Billing.RevenueRecognitionDetail: Se inserta el detalle por cada ServiceOrderDetail vinculado a los folios bloqueados (rcd.Status=5, sod.IsDelete=0, sod.SettlementType<>3, sod.GrandTotalSalesPrice>0); ThirdPartySalesPrice se reemplaza por GrandTotalSalesPrice-Taxes cuando LiquidateMasterAccount=1, y SubTotalPatientSalesPrice se anula cuando CareGroupType=3.; [INSERT] Billing.RevenueRecognitionDetail: Para los ítems con producto de inventario, la cuenta IncomeRecognitionMainAccountId se toma de Inventory.ProductGroup; para el resto, se obtiene vía Billing.fnGetIncomeRecognitionPendingBillingMainAccountId usando el BillingConcept del CUPSEntity.; [UPDATE] Billing.RevenueRecognition: Tras crear el comprobante contable, se actualiza el reconocimiento con JournalVoucherId y JournalVoucherConsecutive obtenidos de GeneralLedger.JournalVouchers (EntityName=''RevenueRecognition'' y libro oficial).; [INSERT] GeneralLedger.JournalVouchers: Vía SP_CreateAndValidateJournalVoucherMovement se genera el comprobante contable de causación con tipo RecognitionJournalVoucherTypeId, libro oficial, fecha @recognitionDate, EntityName=''RevenueRecognition'' y EntityId=@revenueRecognitionId.; [RETURN_RESULT] RESULT: Devuelve un result set con StatusResult (bit), MessageResult, JournalVoucherConsecutive, JournalVoucherCodeName y RevenueRecognitionId; StatusResult=0 si ya existía reconocimiento activo, si SP_CreateAndValidateJournalVoucherMovement reporta errores (CodeMessage<>''0'') o si se captura excepción.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateRecognition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe Billing.RevenueRecognition con CareGroupId=@CareGroupId y State=1 → Retorna StatusResult=0 con mensaje ''Existe un reconocimiento de ingreso activo'' y termina sin generar nada. else Continúa el proceso de reconocimiento.; si @CareGroupTotal = 0 → Retorna StatusResult=1 con mensaje ''No tiene un valor a reconocer'' y termina sin insertar. else Procede a registrar folios y reconocimiento.; si SettingsBilling.LiquidateMasterAccount = 1 → ThirdPartySalesPrice del detalle se calcula como GrandTotalSalesPrice - GrandTotalTaxes (liquidación al maestro). else Se usa sodd.ThirdPartySalesPrice + (SubTotalPatientSalesPrice si CareGroupType=3).; si Contract.CareGroup.CareGroupType = 3 → SubTotalPatientSalesPrice se reconoce como 0 y se suma al ThirdPartySalesPrice (atención particular/paciente asumida por tercero). else SubTotalPatientSalesPrice se conserva separado para el paciente.; si Existen filas en @TableResultJournal con CodeMessage<>''0'' tras invocar SP_CreateAndValidateJournalVoucherMovement → Retorna StatusResult=0 con el mensaje de error contable y termina. else Actualiza RevenueRecognition con JournalVoucherId y consecutivo y retorna éxito.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateRecognition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateRecognition';
-- GO
