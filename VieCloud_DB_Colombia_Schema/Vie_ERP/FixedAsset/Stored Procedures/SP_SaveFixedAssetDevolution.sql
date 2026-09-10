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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que registra y guarda la devolución de un ingreso de activos fijos en el sistema. Procesa un documento XML con la cabecera de la devolución (unidad operativa, fecha, valores de flete, IVA, descuentos, retenciones, deducciones y estado), los ítems de detalle devueltos y las obligaciones presupuestales asociadas. Genera automáticamente el consecutivo o código del documento de devolución consultando la secuencia configurada (por organización u unidad operativa) a través de las tablas FixedAssetSequence y FixedAssetSequenceDetail, y usando la función GetSequence. Gestiona también la eliminación lógica de obligaciones presupuestales marcadas para borrar y persiste el registro principal en FixedAssetEntryDevolution, siendo el punto central de control para formalizar la reversión de una entrada de activos fijos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'PROCEDURE', @level1name = N'SP_SaveFixedAssetDevolution';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'PROCEDURE', @level1name = N'SP_SaveFixedAssetDevolution';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Guarda (crea/actualiza) la devolución de un ingreso de activos fijos con su cabecera, detalles seleccionados y obligaciones presupuestales asociadas, generando consecutivo y aplicando efectos al confirmarla.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SaveFixedAssetDevolution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir configuración de secuencia en FixedAssetSequence con IdForm=''1119'' y su detalle en FixedAssetSequenceDetail (acorde al Scope ''O'' o por OperatingUnit).; Al menos un detalle del XML debe tener CheckOption=1 para poder guardar.; Los activos seleccionados (FixedAssetEntryItemDetail) no deben tener Refund=1 (ya devueltos).; Los activos físicos (FixedAssetPhysicalAsset) asociados por Plate deben estar activos (Status=1).', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SaveFixedAssetDevolution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La numeración consecutiva sólo se genera para registros nuevos sin código, y siempre incrementa el contador de secuencia.; No se permite guardar una devolución sin al menos un detalle con CheckOption=1.; No se permite devolver un activo que ya fue devuelto previamente (Refund=1).; No se permite devolver activos físicos que no estén activos (Status<>1).; La confirmación (Status=2) es la única que altera el estado físico del activo y marca Refund.; La anulación (Status=3) registra usuario y fecha de anulación y no toca detalles.; El parámetro Value de obligaciones admite coma decimal y se normaliza a punto antes de guardar.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SaveFixedAssetDevolution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[DELETE] FixedAsset.FixedAssetEntryDevolutionObligationBudget: Cuando una obligación del XML viene con Id>0 y IsDelete=1, se elimina el registro correspondiente.; [UPDATE] FixedAsset.FixedAssetSequenceDetail: Cuando se genera consecutivo nuevo (Code vacío e Id=0), se incrementa [Next] en 1 del detalle de secuencia usado.; [INSERT] FixedAsset.FixedAssetEntryDevolution: Cuando Id=0, se inserta la cabecera con CreationUser=usuario y CreationDate=GETDATE.; [UPDATE] FixedAsset.FixedAssetEntryDevolution: Cuando Id<>0, se actualiza la cabecera; si Status=3 se setean AnnulmentUser/AnnulmentDate con el usuario y fecha actuales, en otro caso quedan en NULL.; [INSERT] FixedAsset.FixedAssetEntryDevolutionObligationBudget: Para cada obligación del XML con Id=0 e IsDelete=0 se inserta vinculada a la devolución guardada.; [UPDATE] FixedAsset.FixedAssetEntryDevolutionObligationBudget: Para cada obligación del XML con Id>0 e IsDelete=0 se actualiza su Value.; [INSERT] FixedAsset.FixedAssetEntryDevolutionDetail: Cuando Status<>3, los detalles del XML con Id=0 y CheckOption=1 se insertan como nuevos detalles de la devolución.; [DELETE] FixedAsset.FixedAssetEntryDevolutionDetail: Cuando Status<>3, los detalles existentes con Id>0 y CheckOption=0 (deschequeados) se eliminan.; [UPDATE] FixedAsset.FixedAssetEntryItemDetail: Cuando Status=2 (confirmación), se marca Refund=1 en los ítems incluidos en los detalles de la devolución.; [UPDATE] FixedAsset.FixedAssetPhysicalAsset: Cuando Status=2 (confirmación), los activos físicos asociados por Plate se marcan con HasOutput=1, OutputDate=DocumentDate, OutputRefund=1 y Status=0.; [RETURN_RESULT] (resultset): Devuelve CodeMessage=999 y mensaje específico cuando: no se encuentra secuencia, no hay detalles seleccionados, hay activos ya devueltos, o hay activos inactivos; en éxito devuelve CodeMessage=0 con Id y Code.; [RETURN_RESULT] (resultset): En caso de excepción captura el error y retorna CodeMessage=999 con ERROR_MESSAGE().', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SaveFixedAssetDevolution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Code vacío y Id=0 → Se calcula consecutivo según Scope (''O'' global vs por OperatingUnit) y se actualiza el siguiente número. else Se conserva el Code recibido.; si Scope de la secuencia = ''O'' → Consulta la secuencia sin filtrar por OperatingUnit. else Filtra el detalle de secuencia por IdOperatingUnit=OperatingUnitId.; si Id=0 → INSERT de cabecera nueva. else UPDATE de cabecera existente con manejo de Annulment según Status.; si Status=3 al actualizar → Asigna AnnulmentUser y AnnulmentDate. else Deja AnnulmentUser y AnnulmentDate en NULL.; si Status<>3 → Sincroniza detalles: inserta nuevos chequeados y elimina los deschequeados. else No modifica los detalles.; si Status=2 (confirmación) → Marca Refund=1 en FixedAssetEntryItemDetail y actualiza HasOutput/OutputDate/OutputRefund/Status=0 en FixedAssetPhysicalAsset. else No aplica cambios sobre ítems ni activos físicos.; si Existen detalles con CheckOption=1 cuyo activo ya tiene Refund=1 → Aborta y retorna mensaje listando placas ya devueltas. else Continúa el flujo.; si Existen activos físicos asociados con Status<>1 → Aborta y retorna mensaje listando placas inactivas. else Continúa el flujo.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SaveFixedAssetDevolution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE; dbo.GetSequence', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SaveFixedAssetDevolution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SaveFixedAssetDevolution';
-- GO
