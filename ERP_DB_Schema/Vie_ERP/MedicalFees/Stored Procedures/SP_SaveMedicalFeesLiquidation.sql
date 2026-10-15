





-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 16/02/2016
-- Description:	Procedimiento que se encarga de guardar, actualizar la liquidación de honorarios médicos
-- =============================================
CREATE PROCEDURE [MedicalFees].[SP_SaveMedicalFeesLiquidation] 
	@MedicalFeesLiquidationXml as Xml,
	@CodeUser as varchar(20)
AS
BEGIN

	--Se declaran las variables para obtener la cabecera
	declare @Id int, @Code varchar(20), @OperatingUnitId int, @LiquidationType tinyint, @MedicalFeesContractId int,
	 @HealthProfessionalCode char(20), @SupplierId int, @SuppliersDistributionLineId int, @CostCenterId int, @InitialDate datetime, 
	 @EndDate datetime, @BillNumber varchar(20), @DocumentDate datetime, @FilingUnitId int, @SupplierTypeId int, @AccountPayableId int, @Status tinyint
	
	--Tabla temporal para obtener los detalles de MedicalFeesLiquidation
	declare @MedicalFeesLiquidationDetail table(Id int, LiquidationType tinyint, MedicalFeesLiquidacionId int, MedicalFeesCausationId int, ChangeTracker int, ChangeStatusMedicalFeesCausation bit)

	Begin try
	
		--Se obtiene la cabecera del xml(MedicalFeesLiquidation)
		select 
		@Id = t.x.value('Id[1]','int'),
		@Code = t.x.value('Code[1]','varchar(20)'),
		@OperatingUnitId = t.x.value('OperatingUnitId[1]','int'),
		@LiquidationType = t.x.value('LiquidationType[1]','tinyint'),
		@MedicalFeesContractId = case t.x.value('MedicalFeesContractId[1]','int') when 0 then null else t.x.value('MedicalFeesContractId[1]','int') end,
		@HealthProfessionalCode = case t.x.value('HealthProfessionalCode[1]','char(20)') when '0' then null else t.x.value('HealthProfessionalCode[1]','char(20)') end,
		@SupplierId = t.x.value('SupplierId[1]','int'),
		@SuppliersDistributionLineId = t.x.value('SuppliersDistributionLineId[1]','int'),
		@CostCenterId = case t.x.value('CostCenterId[1]','int') when 0 then null else t.x.value('CostCenterId[1]','int') end,
		@InitialDate = convert(datetime, t.x.value('InitialDate[1]','varchar(20)'), 103),
		@EndDate = convert(datetime, t.x.value('EndDate[1]','varchar(20)'), 103),
		@BillNumber = t.x.value('BillNumber[1]','varchar(20)'),		
		@DocumentDate = convert(datetime, t.x.value('DocumentDate[1]','varchar(20)'), 103),
		@FilingUnitId = t.x.value('FilingUnitId[1]','int'),
		@SupplierTypeId = t.x.value('SupplierTypeId[1]','int'),
		@AccountPayableId = case t.x.value('AccountPayableId[1]','int') when 0 then null else t.x.value('AccountPayableId[1]','int') end,
		@Status = t.x.value('Status[1]','tinyint')
		from @MedicalFeesLiquidationXml.nodes('/MedicalFeesLiquidation') t(x)
		
		--Se obtiene los detalles del xml(MedicalFeesLiquidationDeatil)
		insert into @MedicalFeesLiquidationDetail
		select 
		t.x.value('Id[1]','int') as Id,
		case when t.x.value('LiquidationType[1]','tinyint') = 0 then null else t.x.value('LiquidationType[1]','tinyint') end as LiquidationType,
		t.x.value('MedicalFeesLiquidacionId[1]','int') as MedicalFeesLiquidacionId,
		t.x.value('MedicalFeesCausationId[1]','int') as MedicalFeesCausationId,
		t.x.value('ChangeTracker[1]','int') as ChangeTracker,
		t.x.value('ChangeStatusMedicalFeesCausation[1]','bit') as ChangeStatusMedicalFeesCausation
		from @MedicalFeesLiquidationXml.nodes('/MedicalFeesLiquidation/MedicalFeesLiquidationDetail') t(x)

		--Se crea el consecutivo siempre y cuando el código este vacío
		if @Code = '' And @Id = 0
		Begin
			-- Consultamos la secuencia numerica del form
			declare @idSequenceDetail int
			declare @pattern varchar(300)
			declare @NextS int
			declare @Scope varchar(5)
			select @Scope = bs.Scope  
			from MedicalFees.MedicalFeesSecuenceDetail bsd 
			inner join MedicalFees.MedicalFeesSecuence bs on bs.Id = bsd.SequenseMedicalFeesId 
			inner join Common.Sequense cs on cs.Id = bsd.IdSequense
			where bs.IdForm = '1303' --and bsd.IdOperatingUnit = @OperatingUnitId
			
			if @Scope = 'O' begin --- Secuencia por Prefijo
				select @idSequenceDetail = bsd.Id, @pattern = cs.Pattern, @NextS = bsd.[Next]
				from MedicalFees.MedicalFeesSecuenceDetail bsd 
				inner join MedicalFees.MedicalFeesSecuence bs on bs.Id = bsd.SequenseMedicalFeesId 
				inner join Common.Sequense cs on cs.Id = bsd.IdSequense
				where bs.IdForm = '1303'
			end
			else begin -- Secuencia por Unidad operativa
				select @idSequenceDetail = bsd.Id, @pattern = cs.Pattern, @NextS = bsd.[Next]
				from MedicalFees.MedicalFeesSecuenceDetail bsd 
				inner join MedicalFees.MedicalFeesSecuence bs on bs.Id = bsd.SequenseMedicalFeesId 
				inner join Common.Sequense cs on cs.Id = bsd.IdSequense
				where bs.IdForm = '1303' and bsd.IdOperatingUnit = @OperatingUnitId
			end

			if (@idSequenceDetail is null)
			Begin
			 select 999 as CodeMessage, 'Secuencia no encontrada'  as Message, 0 Id, '' as CodeMedicalFeesLiquidation
			 return
			End
			select @Code = dbo.GetSequence('',@pattern,@NextS)
			update MedicalFees.MedicalFeesSecuenceDetail set [Next] += 1 where Id = @idSequenceDetail
		End
		
		--Actualizo las variables correspondientes en la tabla de medicalFeesCausation
		if (@Id = 0 and @Status = 1) or @Status = 2
		Begin
			--Actualizo MedicalFeesCausation siempre y cuando el item del detalle no este eliminado
			update [MedicalFees].MedicalFeesCausation
			set ReassessmentForReversal = case when @Status = 2 and Status = 3 and InvoiceReversal = 1 then 1 else 0 end, 
			ReassessmentForObjection = case when @Status = 2 and Status = 3 and ObjectionAccepted = 1 then 1 else 0 end, 
			Status = case when Status <> 3 then case when @Status = 1 then  2 else  3 end else Status end
			where Id in (select MedicalFeesCausationId from @MedicalFeesLiquidationDetail where ChangeTracker <> 3)

			--Actualizo MedicalFeesCausation siempre y cuando el item del detalle este eliminado
			update [MedicalFees].MedicalFeesCausation
			set Status = 4
			where Id in (select MedicalFeesCausationId from @MedicalFeesLiquidationDetail where ChangeTracker = 3)
		End

		--Se consulta el contrato o el medico para cambiarle la fecha de ultima liquidacion y el temporal de la fecha de ultima liquidacion
		if @Id = 0 or @Status = 2 or @Status = 3
		Begin
			
			if @HealthProfessionalCode is not null --Si viene el código del médico lleno se actualiza la tabla de crystal
			Begin
				update dbo.INPROFSAL 
				set FECULTLIQTMP = case when @Status = 1 then @EndDate when @Status = 2 then null when @Status = 3 then null end, 
				FECULTLIQ = case when @Status = 2 then @EndDate else FECULTLIQ end
				where CODPROSAL = @HealthProfessionalCode
			End
			Else
			Begin
				update [MedicalFees].MedicalFeesContract
				set LastLiquidationDateTmp = case when @Status = 1 then @EndDate when @Status = 2 then null when @Status = 3 then null end,
				LastLiquidationDate = case when @Status = 2 then @EndDate else LastLiquidationDate end
				where Id = @MedicalFeesContractId
			End
			
		End
		
		--Se empieza el registro del objeto
		declare @ConfirmationUser as varchar(20) = case when @Status <> 2 then null else @CodeUser end
		declare @ConfirmationDate as datetime = case when @Status <> 2 then null else [Common].[GETDATE]() end
		if @Id = 0 --Si el registro es nuevo guardo
		Begin
			--Inserto la cabecera
			INSERT INTO [MedicalFees].[MedicalFeesLiquidation] ([Code],[OperatingUnitId],[LiquidationType],[MedicalFeesContractId],[HealthProfessionalCode],[SupplierId],[SuppliersDistributionLineId],[CostCenterId],[InitialDate],[EndDate],[BillNumber],[DocumentDate],[FilingUnitId],[SupplierTypeId],[AccountPayableId],[Status],[CreationUser],[CreationDate],[ConfirmationUser], [ConfirmationDate])
			values (@Code,@OperatingUnitId,@LiquidationType,@MedicalFeesContractId,@HealthProfessionalCode,@SupplierId,@SuppliersDistributionLineId,@CostCenterId,@InitialDate,@EndDate,@BillNumber,@DocumentDate,@FilingUnitId,@SupplierTypeId,@AccountPayableId,@Status,@CodeUser,[Common].[GETDATE](),@ConfirmationUser,@ConfirmationDate)

			--Obtengo el id de la cabcera
			set @Id = SCOPE_IDENTITY()
		End
		Else --Si se esta modificando
		Begin
			--Actualizo la cabecera
			declare @AnnulmentUser as varchar(20) = case when @Status <> 3 then null else @CodeUser end
			declare @AnnulmentDate as datetime = case when @Status <> 3 then null else [Common].[GETDATE]() end
			Update [MedicalFees].[MedicalFeesLiquidation] 
			set Code = @Code, OperatingUnitId = @OperatingUnitId, LiquidationType = @LiquidationType, MedicalFeesContractId = @MedicalFeesContractId,
			HealthProfessionalCode = @HealthProfessionalCode, SupplierId = @SupplierId, SuppliersDistributionLineId = @SuppliersDistributionLineId,
			CostCenterId = @CostCenterId, InitialDate = @InitialDate, EndDate = @EndDate, BillNumber = @BillNumber, DocumentDate = @DocumentDate,
			FilingUnitId = @FilingUnitId, SupplierTypeId = @SupplierTypeId, AccountPayableId = @AccountPayableId, Status = @Status, ModificationUser = @CodeUser,
			ModificationDate = [Common].[GETDATE](), ConfirmationUser = @ConfirmationUser, ConfirmationDate = @ConfirmationDate, AnnulmentUser = @AnnulmentUser,
			AnnulmentDate = @AnnulmentDate
			where Id = @Id
		End

		--Inserto los detalles(MedicalFeesLiquidationDetail) nuevos si hay
		if (select count(*) from @MedicalFeesLiquidationDetail where Id = 0) > 0 
		Begin
			INSERT INTO [MedicalFees].[MedicalFeesLiquidationDetail] ([LiquidationType],[MedicalFeesLiquidacionId],[MedicalFeesCausationId])
			select LiquidationType, @Id, MedicalFeesCausationId from @MedicalFeesLiquidationDetail where Id = 0
		End
		--Actualizo los detalles(MedicalFeesLiquidationDetail) si hay
		if (select count(*) from @MedicalFeesLiquidationDetail where Id > 0) > 0 
		Begin
			UPDATE mfld set mfld.LiquidationType = mfldtemp.LiquidationType, mfld.MedicalFeesLiquidacionId = mfldtemp.MedicalFeesLiquidacionId, mfld.MedicalFeesCausationId = mfldtemp.MedicalFeesCausationId
			from [MedicalFees].[MedicalFeesLiquidationDetail] mfld
			inner join @MedicalFeesLiquidationDetail mfldtemp on mfld.Id = mfldtemp.Id
		End
		--Elimino los detalles(MedicalFeesLiquidationDetail) si hay
		if (select count(*) from @MedicalFeesLiquidationDetail where Id > 0 and ChangeTracker = 3) > 0 
		Begin
			
			--Actualizo el estado de la causación siempre y cuando se haya eliminado el item y este con el ChangeStatusMedicalFeesCausation en True
			if (select count(*) from @MedicalFeesLiquidationDetail where Id > 0 and ChangeTracker = 3 and ChangeStatusMedicalFeesCausation = 1) > 0 
			Begin
				--Actualizo MedicalFeesCausation
				update [MedicalFees].MedicalFeesCausation
				set Status = 1
				where Id in (select MedicalFeesCausationId from @MedicalFeesLiquidationDetail where ChangeTracker = 3 and ChangeStatusMedicalFeesCausation = 1)
			End

			delete [MedicalFees].[MedicalFeesLiquidationDetail] where Id in (select Id from @MedicalFeesLiquidationDetail where Id > 0 and ChangeTracker = 3)
		End

		
		select 0 as CodeMessage, 'Se guardó correctamente' as Message, @Id as Id, @Code as CodeMedicalFeesLiquidation
		
	end try
	begin catch
		select 999 as CodeMessage, ERROR_MESSAGE() as Message, 0 Id, '' as CodeMedicalFeesLiquidation
	end catch

END