-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 19/08/2016
-- Description:	Procedimiento que se encarga de guardar la devolución de ingreso de activos
-- =============================================
CREATE PROCEDURE [FixedAsset].[SP_SaveFixedAssetDevolution] 
	@FixedAssetDevolutionXml as Xml,
	@CodeUser as varchar(20)
AS
BEGIN
	
	--Se declaran las variables para la cabecera
	declare @Id int, @OperatingUnitId int, @Code varchar(20), @DocumentDate datetime, @FixedAssetEntryId int, @Description varchar(max),
	@FreightValue numeric(20,0), @FreightIVAPercentage numeric(5,2), @FreightIVAValue numeric(20,0), @Value numeric(20,0), @ValueDiscount numeric(20,0),
	@ValueTax numeric(20,0), @WithholdingTax numeric(20,0), @WithholdingICA numeric(20,0), @RetentionSource numeric(20,0), @RetentionOther numeric(20,0),
	@DeductionOther numeric(20,0), @TotalValue numeric(20,0), @Status tinyint

	--Tabla para almacenar los items del listado que viene en el xml y poder guardar los detalles de la devolución
	declare @TableFixedAssetDevolutionDetail table(Id int, FixedAssetEntryDevolutionId int, FixedAssetEntryItemId int, FixedAssetEntryItemDetailId int,
	UnitValue numeric(20,4), IvaPercentage numeric(5,2), IvaValue numeric(20,4), DiscountPercentage numeric(5,2), DiscountValue numeric(20,4),
	TotalValue numeric(20,4), RTFPercentage numeric(5,2), RTFValue numeric(18,2), CheckOption bit, SubTotalValue numeric(20,4))

	--Tabla para almacenar las obligaciones del listado de xml
	declare @TableFixedAssetEntryDevolutionObligationBudget table(Id int, FixedAssetEntryDevolutionId int, ObligationDetailId int, Value numeric(20,4), IsDelete bit)

	--begin transaction
	begin try

		--Se obtiene la cabecera del xml(FixedAssetDevolution)
		select 
		@Id = t.x.value('Id[1]','int'),
		@OperatingUnitId = t.x.value('OperatingUnitId[1]','int'),
		@Code = t.x.value('Code[1]','varchar(20)'),
		@DocumentDate = convert(datetime, t.x.value('DocumentDate[1]','varchar(20)'), 103),
		@FixedAssetEntryId = t.x.value('FixedAssetEntryId[1]','int'),
		@Description = t.x.value('Description[1]','varchar(max)'),
		@FreightValue = cast(t.x.value('FreightValue[1]','varchar(20)') as numeric(20,0)),
		@FreightIVAPercentage = cast(t.x.value('FreightIVAPercentage[1]','varchar(20)') as numeric(5,2)),
		@FreightIVAValue = cast(t.x.value('FreightIVAValue[1]','varchar(20)') as numeric(20,0)),
		@Value = cast(t.x.value('Value[1]','varchar(20)') as numeric(20,0)),
		@ValueDiscount = cast(t.x.value('ValueDiscount[1]','varchar(20)') as numeric(20,0)),
		@ValueTax = cast(t.x.value('ValueTax[1]','varchar(20)') as numeric(20,0)),
		@WithholdingTax = cast(t.x.value('WithholdingTax[1]','varchar(20)') as numeric(20,0)),
		@WithholdingICA = cast(t.x.value('WithholdingICA[1]','varchar(20)') as numeric(20,0)),
		@RetentionSource = cast(t.x.value('RetentionSource[1]','varchar(20)') as numeric(20,0)),
		@RetentionOther = cast(t.x.value('RetentionOther[1]','varchar(20)') as numeric(20,0)),
		@DeductionOther = cast(t.x.value('DeductionOther[1]','varchar(20)') as numeric(20,0)),
		@TotalValue = cast(t.x.value('TotalValue[1]','varchar(20)') as numeric(20,0)),
		@Status = t.x.value('Status[1]','tinyint')
		from @FixedAssetDevolutionXml.nodes('/FixedAssetDevolution') t(x)

		insert into @TableFixedAssetDevolutionDetail
		select 
		t.x.value('Id[1]','int') as Id,
		t.x.value('FixedAssetEntryDevolutionId[1]','int') as FixedAssetEntryDevolutionId,
		t.x.value('FixedAssetEntryItemId[1]','int') as FixedAssetEntryItemId,
		t.x.value('FixedAssetEntryItemDetailId[1]','int') as FixedAssetEntryItemDetailId,
		cast(t.x.value('UnitValue[1]','varchar(20)') as numeric(20,4)) as UnitValue,
		cast(t.x.value('IvaPercentage[1]','varchar(20)') as numeric(5,2)) as IvaPercentage,
		cast(t.x.value('IvaValue[1]','varchar(20)') as numeric(20,4)) as IvaValue,
		cast(t.x.value('DiscountPercentage[1]','varchar(20)') as numeric(5,2)) as DiscountPercentage,
		cast(t.x.value('DiscountValue[1]','varchar(20)') as numeric(20,4)) as DiscountValue,
		cast(t.x.value('TotalValue[1]','varchar(20)') as numeric(20,4)) as TotalValue,
		cast(t.x.value('RTFPercentage[1]','varchar(20)') as numeric(5,2)) as RTFPercentage,
		cast(t.x.value('RTFValue[1]','varchar(20)') as numeric(18,2)) as RTFValue,
		t.x.value('CheckOption[1]','bit') as CheckOption,
		cast(t.x.value('SubTotalValue[1]','varchar(20)') as numeric(20,4)) as SubTotalValue
		from @FixedAssetDevolutionXml.nodes('/FixedAssetDevolution/FixedAssetDevolutionDetail') t(x)

		--Se insertan las obligaciones
		insert into @TableFixedAssetEntryDevolutionObligationBudget
		select 
		t.x.value('Id[1]','int') as Id,
		t.x.value('FixedAssetEntryDevolutionId[1]','int') as FixedAssetEntryDevolutionId,
		t.x.value('ObligationDetailId[1]','int') as ObligationDetailId,
		REPLACE(t.x.value('Value[1]','varchar(20)'), ',', '.') as Value,
		t.x.value('IsDelete[1]','bit') as IsDelete
		from @FixedAssetDevolutionXml.nodes('/FixedAssetDevolution/FixedAssetEntryDevolutionObligationBudget') t(x)

		--Se eliminan las obligaciones
		delete from FixedAsset.FixedAssetEntryDevolutionObligationBudget where Id in (select Id from @TableFixedAssetEntryDevolutionObligationBudget where Id > 0 and IsDelete = 1)
		delete from @TableFixedAssetEntryDevolutionObligationBudget where IsDelete = 1

		--Se crea el consecutivo siempre y cuando el código este vacío
		if @Code = '' And @Id = 0
		Begin
			--Consultamos si la secuencia es con O o OU
			declare @scope varchar(5) = ''
			declare @idSequenceDetail int
			declare @pattern varchar(300)
			declare @NextS int
			select @scope = Scope from FixedAsset.FixedAssetSequence 
			where IdForm = '1119'

			--Se valida el scope
			if @scope = 'O'
			Begin
				-- Consultamos la secuencia numerica del form de ingreso de activos
				select @pattern = cs.Pattern, @NextS = bsd.[Next] , @idSequenceDetail = bsd.Id  from FixedAsset.FixedAssetSequenceDetail bsd inner join FixedAsset.FixedAssetSequence bs on bs.Id = bsd.IdSequenseFixedAssetC inner join Common.Sequense cs on cs.Id = bsd.IdSequense
				where bs.IdForm = '1119'
			End
			Else
			Begin
				-- Consultamos la secuencia numerica del form de ingreso de activos
				select @pattern = cs.Pattern, @NextS = bsd.[Next] , @idSequenceDetail = bsd.Id  from FixedAsset.FixedAssetSequenceDetail bsd inner join FixedAsset.FixedAssetSequence bs on bs.Id = bsd.IdSequenseFixedAssetC inner join Common.Sequense cs on cs.Id = bsd.IdSequense
				where bs.IdForm = '1119' and bsd.IdOperatingUnit = @OperatingUnitId
			End

			if (@idSequenceDetail is null)
			Begin
				select 999 as CodeMessage, 'Secuencia no encontrada'  as Message, 0 as Id, '' as CodeResult
				return
			End
			select @Code = dbo.GetSequence('',@pattern,@NextS)
			update FixedAsset.FixedAssetSequenceDetail set [Next] += 1 where Id = @idSequenceDetail
		End

		--Se inserta la cabecera
		if @Id = 0 --Si el registro es nuevo guardo
		Begin
			--Inserto la cabecera
			INSERT INTO [FixedAsset].[FixedAssetEntryDevolution] (OperatingUnitId, Code, DocumentDate, FixedAssetEntryId, [Description],
			FreightValue, FreightIVAPercentage, FreightIVAValue, Value, ValueDiscount, ValueTax, WithholdingTax, WithholdingICA, 
			RetentionSource, RetentionOther, DeductionOther, TotalValue, [Status], CreationUser, CreationDate)
			values (@OperatingUnitId, @Code, @DocumentDate, @FixedAssetEntryId, @Description, @FreightValue, @FreightIVAPercentage, @FreightIVAValue,
			@Value, @ValueDiscount, @ValueTax, @WithholdingTax, @WithholdingICA, @RetentionSource, @RetentionOther, @DeductionOther, @TotalValue, @Status,
			@CodeUser, [Common].[GETDATE]())

			--Obtengo el id de la cabcera
			set @Id = SCOPE_IDENTITY()
		End
		Else --Si se esta modificando
		Begin
			declare @AnnulmentUser as varchar(20) = case when @Status <> 3 then null else @CodeUser end
			declare @AnnulmentDate as datetime = case when @Status <> 3 then null else [Common].[GETDATE]() end
			Update [FixedAsset].[FixedAssetEntryDevolution] 
			set OperatingUnitId = @OperatingUnitId, Code = @Code, DocumentDate = @DocumentDate, FixedAssetEntryId = @FixedAssetEntryId, 
			[Description] = @Description, FreightValue = @FreightValue, FreightIVAPercentage = @FreightIVAPercentage, FreightIVAValue = @FreightIVAValue, 
			Value = @Value, ValueDiscount = @ValueDiscount, ValueTax = @ValueTax, WithholdingTax = @WithholdingTax, WithholdingICA = @WithholdingICA, 
			RetentionSource = @RetentionSource, RetentionOther = @RetentionOther, DeductionOther = @DeductionOther, TotalValue = @TotalValue, 
			[Status] = @Status, ModificationUser = @CodeUser, ModificationDate = [Common].[GETDATE](), AnnulmentUser = @AnnulmentUser, AnnulmentDate = @AnnulmentDate
			where Id = @Id
		End

		--Se insertan las obligaciones nuevas
		insert into [FixedAsset].[FixedAssetEntryDevolutionObligationBudget]([FixedAssetEntryDevolutionId], [ObligationDetailId], [Value])
		select @Id, t.ObligationDetailId, t.Value
		from @TableFixedAssetEntryDevolutionObligationBudget t
		where t.Id = 0 and t.IsDelete = 0

		--Se actualizan las obligaciones
		update ob set ob.Value = t.Value
		from @TableFixedAssetEntryDevolutionObligationBudget t
		inner join FixedAsset.FixedAssetEntryDevolutionObligationBudget ob on ob.Id = t.Id
		where t.Id > 0 and t.IsDelete = 0

		--Se valida que al menos haya un detalle seleccionado
		if (select COUNT(*) from @TableFixedAssetDevolutionDetail where CheckOption = 1) = 0
		Begin
			select 999 as CodeMessage, 'No hay detalles seleccionados para poder guardar'  as Message, 0 as Id, '' as CodeResult
			return
		End

		--Se valida que los activos seleccionados no hayan sido devueltos
		IF EXISTS (
			SELECT faeid.Id
			FROM @TableFixedAssetDevolutionDetail fadd
			JOIN FixedAsset.FixedAssetEntryItemDetail faeid ON fadd.FixedAssetEntryItemDetailId = faeid.Id
			WHERE fadd.CheckOption = 1 AND faeid.Refund = 1
		)
		BEGIN
			DECLARE @errorsFixedAssetHasRefund VARCHAR(MAX)
			SELECT @errorsFixedAssetHasRefund = STUFF((SELECT DISTINCT N'; El Activo con Placa ' + faeid.Plate + ' ya fue Devuelto'
				FROM @TableFixedAssetDevolutionDetail fadd
				JOIN FixedAsset.FixedAssetEntryItemDetail faeid ON fadd.FixedAssetEntryItemDetailId = faeid.Id
				WHERE fadd.CheckOption = 1 AND faeid.Refund = 1
				FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
			SELECT 999 AS CodeMessage, @errorsFixedAssetHasRefund AS Message, 0 as Id, '' as CodeResult
			RETURN
		END

		--Se valida que los activos seleccionados no tengan salida
		IF EXISTS (
			SELECT faeid.Id
			FROM @TableFixedAssetDevolutionDetail fadd
			JOIN FixedAsset.FixedAssetEntryItemDetail faeid ON fadd.FixedAssetEntryItemDetailId = faeid.Id
			JOIN FixedAsset.FixedAssetPhysicalAsset fapa ON faeid.Plate = fapa.Plate
			WHERE fadd.CheckOption = 1 AND fapa.Status <> 1
		)
		BEGIN
			DECLARE @errorsFixedAssetHasOutput VARCHAR(MAX)
			SELECT @errorsFixedAssetHasOutput = STUFF((SELECT DISTINCT N'; El Activo con Placa ' + faeid.Plate + ' se encuentra Inactivo'
				FROM @TableFixedAssetDevolutionDetail fadd
				JOIN FixedAsset.FixedAssetEntryItemDetail faeid ON fadd.FixedAssetEntryItemDetailId = faeid.Id
				JOIN FixedAsset.FixedAssetPhysicalAsset fapa ON faeid.Plate = fapa.Plate
				WHERE fadd.CheckOption = 1 AND fapa.Status <> 1
				FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
			SELECT 999 AS CodeMessage, @errorsFixedAssetHasOutput AS Message, 0 as Id, '' as CodeResult
			RETURN
		END

		if @Status <> 3 --Si se esta guardando o confirmando
		Begin
			--Inserto los nuevos detalles
			if (select count(*) from @TableFixedAssetDevolutionDetail where Id = 0 and CheckOption = 1) > 0 begin
				INSERT INTO FixedAsset.FixedAssetEntryDevolutionDetail(FixedAssetEntryDevolutionId, FixedAssetEntryItemId, FixedAssetEntryItemDetailId,
				UnitValue, IvaPercentage, IvaValue, DiscountPercentage, DiscountValue, TotalValue, RTFPercentage, RTFValue, SubTotalValue)
				select @Id, FixedAssetEntryItemId, FixedAssetEntryItemDetailId, UnitValue, IvaPercentage, IvaValue, DiscountPercentage, DiscountValue,
				TotalValue, RTFPercentage, RTFValue, SubTotalValue 
				from @TableFixedAssetDevolutionDetail where Id = 0 and CheckOption = 1
			end

			--Elimino los detalles que deschequearon
			if (select count(*) from @TableFixedAssetDevolutionDetail where Id > 0 and CheckOption = 0) > 0 begin
				delete from FixedAsset.FixedAssetEntryDevolutionDetail where Id in 
				(select Id from @TableFixedAssetDevolutionDetail where Id > 0 and CheckOption = 0)
			end
		End
		
		if @Status = 2 --Si se esta confirmando realizo los cambios en la tabla FixedAssetEntryItemDetail y en FixedAssetPhysicalAsset
		Begin
			--Se actualiza la propiedad que define si ya se realizó la devolución en la tabla FixedAssetEntryItemDetail
			update FixedAsset.FixedAssetEntryItemDetail set Refund = 1
			where Id in (select FixedAssetEntryItemDetailId from FixedAsset.FixedAssetEntryDevolutionDetail where FixedAssetEntryDevolutionId = @Id)

			--Se actualiza las propiedades de devolución en la tabla de FixedAsstePhysicalAsset
			UPDATE fapa 
				set fapa.HasOutput = 1, 
					fapa.OutputDate = @DocumentDate, 
					fapa.OutputRefund = 1,
					fapa.Status = 0
			from FixedAsset.FixedAssetEntryDevolutionDetail faedd
			inner join FixedAsset.FixedAssetEntryItemDetail faeid on faeid.Id = faedd.FixedAssetEntryItemDetailId
			inner join FixedAsset.FixedAssetPhysicalAsset fapa on fapa.Plate = faeid.Plate
			where faedd.FixedAssetEntryDevolutionId = @Id
		End

		--commit transaction
		select 0 as CodeMessage, 'Se guardó correctamente' as Message, @Id as Id, @Code as CodeResult

	end try
	begin catch

		--rollback transaction
		select 999 as CodeMessage, ERROR_MESSAGE() as Message, 0 as Id, '' as CodeResult

	end catch

END