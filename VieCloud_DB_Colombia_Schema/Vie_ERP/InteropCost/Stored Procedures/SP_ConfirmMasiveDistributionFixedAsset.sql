-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [InteropCost].[SP_ConfirmMasiveDistributionFixedAsset]
	@Container varchar(20),
	@Year int,
	@Month int,
	@User varchar(20)
AS
BEGIN
	
	begin try

		--Se valida que exista secuencia numérica para el form correspondiente
		declare @idSequenceDetail int
		select @idSequenceDetail = bsd.Id  
		from InteropCost.InteropCostSecuenceDetail bsd 
		inner join InteropCost.InteropCostSecuence bs on bs.Id = bsd.SequenseInteropCostId 
		inner join Common.Sequense cs on cs.Id = bsd.IdSequense
		where bs.IdForm = '1206' --and bsd.IdOperatingUnit = @OperatingUnitId
		if (@idSequenceDetail is null) --Si no existe la secuencia
		Begin
			select 999 as CodeMessage, 'El formulario no tiene parametrizada la secuencia numérica'  as Message
			return
		End

		--Se valida que la secuencia numérica sea secuencial y no manual
		declare @IsManual bit
		declare @Sequential bit
		select @IsManual = bs.IsManual, @Sequential = bs.Sequential 
		from InteropCost.InteropCostSecuenceDetail bsd 
		inner join InteropCost.InteropCostSecuence bs on bs.Id = bsd.SequenseInteropCostId 
		inner join Common.Sequense cs on cs.Id = bsd.IdSequense
		where bs.IdForm = '1206' --and bsd.IdOperatingUnit = @OperatingUnitId
		if @IsManual = 1 or @Sequential = 0
		Begin
			select 999 as CodeMessage, 'La secuencia numérica para la confirmación masiva no puede ser manual y tiene que ser secuencial'  as Message
			return
		End

		declare @TableTmp table(FixedAssetId int, FixedAssetCode varchar(20), FixesAssetName varchar(300), Value numeric(18,0), CostCenterCode varchar(20), CostCenterName varchar(300), ProductionCenterId int)
		declare @Sql nvarchar(max) = '
		select act.OID ,act.AACCODACT, pro.APRNOMBRE, cal.ACADEPMEN, cen.CCCODIGO, cen.CCNOMBRE, pc.Id
		from '+ @Container +'.dbo.AFNCALDEP cal
		inner join '+ @Container +'.dbo.AFNACTIVO act on act.OID = cal.AFNACTIVO
		inner join '+ @Container +'.dbo.AFNPRODUC pro on pro.OID = act.AFNPRODUC
		inner join '+ @Container +'.dbo.CTNCENCOS cen on cen.OID = cal.CTNCENCOS
		inner join '+ @Container +'.dbo.AFNDEPRECI dep on dep.OID = cal.AFNDEPRECI
		left join InteropCost.ProductionCenterCostCenter pcc on pcc.[CostCenterId] = cal.CTNCENCOS
		left join InteropCost.ProductionCenter pc on pc.Id = pcc.[ProductionCenterId]
		where cal.ACADEPMEN > 0 and year(dep.ACAFECCHCI) = '+ cast(@Year as varchar(20)) +' and month(dep.ACAFECCHCI) = ' + cast(@Month as varchar(20)) +
		' and act.OID not in (select FixedAssetId from [InteropCost].[DistributionFixedAsset] where [Year] = '+ cast(@Year as varchar(20)) +' and [Month] = '+ cast(@Month as varchar(20)) +' and [Status] = 1)'

		insert into @TableTmp
		exec sp_sqlexec @Sql

		if (select count(*) from @TableTmp where ProductionCenterId is null) > 0 begin
			declare @errors varchar(MAX)
			select @errors=stuff((select N'; El centro de costo ' + CostCenterCode	+ ' - ' + CostCenterName + ' no esta asociado a un Centro de Produccion'
			from @TableTmp where ProductionCenterId is null
			for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
			select 999 as CodeMessage, @errors as [Message]
			return
		end

		declare @FixedAssetId int, @FixedAssetCode varchar(20), @FixesAssetName varchar(300), @Value numeric(18,0), @ProductionCenterId int
		declare @MessageResult varchar(max) = ' Se guardo correctamente y se crearon los siguientes codigos:'
		declare @Code varchar(20)
		declare @IdDistribution int
		DECLARE fixed_cursor CURSOR FOR   
		select FixedAssetId, FixedAssetCode, FixesAssetName, Value, ProductionCenterId
		from @TableTmp
  
		OPEN fixed_cursor  
  
		FETCH NEXT FROM fixed_cursor   
		INTO @FixedAssetId, @FixedAssetCode, @FixesAssetName, @Value, @ProductionCenterId

		WHILE @@FETCH_STATUS = 0  
		BEGIN  
			
			--Se genera la secuencia numérica para la distribución de mano de obra
			declare @pattern varchar(300)
			declare @NextS int
			select @pattern = cs.Pattern, @NextS = bsd.[Next] , @idSequenceDetail = bsd.Id  
			from InteropCost.InteropCostSecuenceDetail bsd 
			inner join InteropCost.InteropCostSecuence bs on bs.Id = bsd.SequenseInteropCostId
			inner join Common.Sequense cs on cs.Id = bsd.IdSequense
			where bs.IdForm = '1206'
			if (@idSequenceDetail is null)
			Begin
				Close InfoItem
				Deallocate InfoItem
				select 999 as CodeMessage, 'Secuencia no encontrada para generar la distribución de mano de obra' as Message
				return
			End
			select @Code = dbo.GetSequence('',@pattern,@NextS)
			update InteropCost.InteropCostSecuenceDetail set [Next] += 1 where Id = @idSequenceDetail

			INSERT INTO [InteropCost].[DistributionFixedAsset]
           ([Code]
           ,[FixedAssetId]
           ,[FixedAssetCode]
           ,[DepreciationValue]
           ,[Description]
           ,[Year]
           ,[Month]
           ,[Status]
           ,[CreationUser]
           ,[CreationDate])
		   values (@Code, @FixedAssetId, @FixedAssetCode, @Value, @FixedAssetCode + ' - ' +@FixesAssetName, @Year, @Month, 1, @User, [Common].[GETDATE]())
			
			set @IdDistribution = SCOPE_IDENTITY()

			INSERT INTO [InteropCost].[DistributionFixedAssetDetail]
           ([DistributionFixedAssetId]
           ,[ProductionCenterId]
           ,[Proportion]
           ,[DepreciationValue])
		   values (@IdDistribution, @ProductionCenterId, 100, @Value)

		   set @MessageResult = @MessageResult + ' ' + @Code

			FETCH NEXT FROM fixed_cursor   
			INTO @FixedAssetId, @FixedAssetCode, @FixesAssetName, @Value, @ProductionCenterId
		END   
		CLOSE fixed_cursor;  
		DEALLOCATE fixed_cursor;  

		select 1 as CodeMessage, @MessageResult as [Message]

	end try
	begin catch
		select 999 as CodeMessage,ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() as varchar(20)) as [Message]
	end catch

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Confirma de forma masiva la distribución de costos de activos fijos para un período específico (año y mes) dentro de un contenedor (base de datos ERP). Para cada activo fijo con depreciación mensual registrada en el sistema contable origen, valida que su centro de costo esté asociado a un centro de producción; si alguno no lo está, rechaza el proceso con un mensaje detallado de error. Por cada activo válido, genera un código secuencial automático usando la secuencia configurada para el formulario 1206 (consultando InteropCostSecuence e InteropCostSecuenceDetail), y crea los registros de distribución de depreciación en DistributionFixedAsset y su detalle en DistributionFixedAssetDetail, asignando el 100% del valor al centro de producción correspondiente. Este procedimiento es el punto de cierre del proceso de costeo de activos fijos, evitando duplicar distribuciones ya confirmadas en períodos anteriores.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmMasiveDistributionFixedAsset';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmMasiveDistributionFixedAsset';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Confirma de forma masiva la distribución de depreciación de activos fijos del período (mes/año) generando códigos secuenciales y asignando el 100% del valor depreciado al centro de producción asociado a cada centro de costo.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmMasiveDistributionFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una secuencia numérica configurada en InteropCostSecuence/InteropCostSecuenceDetail para IdForm=''1206''; La secuencia configurada para el formulario 1206 debe ser automática (IsManual=0) y secuencial (Sequential=1); El parámetro @Container debe corresponder a una base de datos válida que contenga las tablas AFNCALDEP, AFNACTIVO, AFNPRODUC, CTNCENCOS y AFNDEPRECI; Todos los centros de costo (CTNCENCOS) de los activos a confirmar deben estar asociados a un ProductionCenter en InteropCost.ProductionCenterCostCenter; Los activos a confirmar no deben existir previamente en DistributionFixedAsset con Status=1 para el mismo Year/Month', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmMasiveDistributionFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se procesan activos con depreciación mensual ACADEPMEN > 0 del año/mes indicado; No se reprocesan activos ya confirmados (Status=1) en DistributionFixedAsset para el mismo Year/Month; Cada activo confirmado se asocia 100% (Proportion=100) a un único centro de producción en DistributionFixedAssetDetail; Toda distribución creada queda con Status=1 (confirmada) y registra CreationUser y CreationDate=Common.GETDATE(); Si algún centro de costo carece de centro de producción asociado, NO se inserta ninguna distribución (validación previa al cursor); El correlativo Code se genera vía dbo.GetSequence con el patrón y Next de la secuencia, incrementando Next en 1 por cada activo procesado', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmMasiveDistributionFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Activo fijo; Depreciación mensual; Centro de costo; Centro de producción; Distribución de costos de activos fijos; Secuencia numérica por formulario (1206); Confirmación masiva de distribución', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmMasiveDistributionFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] InteropCost.DistributionFixedAsset: Por cada activo válido del período se inserta una fila con Status=1, Code generado por dbo.GetSequence, DepreciationValue=ACADEPMEN, Description=código+'' - ''+nombre del producto, CreationUser=@User y CreationDate=Common.GETDATE(); [INSERT] InteropCost.DistributionFixedAssetDetail: Por cada cabecera insertada se crea un detalle con ProductionCenterId del centro asociado al CostCenter, Proportion=100 y DepreciationValue igual al de la cabecera; [UPDATE] InteropCost.InteropCostSecuenceDetail: Por cada activo procesado se incrementa [Next] en 1 (Next += 1) en el detalle de secuencia del formulario 1206; [RETURN_RESULT] (resultset): Devuelve CodeMessage=1 con mensaje concatenando los códigos creados al finalizar; o CodeMessage=999 con mensaje de error en cualquier validación o excepción', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmMasiveDistributionFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si No existe registro en InteropCostSecuenceDetail/InteropCostSecuence para IdForm=''1206'' → Devuelve CodeMessage=999 con mensaje ''El formulario no tiene parametrizada la secuencia numérica'' y termina; si La secuencia configurada tiene IsManual=1 o Sequential=0 → Devuelve CodeMessage=999 indicando que la secuencia no puede ser manual y debe ser secuencial, y termina; si Existe al menos un activo cuyo CostCenter no está asociado a un ProductionCenter (ProductionCenterId IS NULL en el join con ProductionCenterCostCenter) → Devuelve CodeMessage=999 concatenando por XML PATH cada centro de costo no asociado y termina sin insertar; si Durante el cursor, al recargar la secuencia no se encuentra registro para IdForm=''1206'' → Cierra/desasigna el cursor (referencia ''InfoItem'') y devuelve CodeMessage=999 ''Secuencia no encontrada para generar la distribución de mano de obra''; si ERROR capturado en TRY/CATCH → Devuelve CodeMessage=999 con ERROR_MESSAGE() y la línea del error', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmMasiveDistributionFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.GetSequence; Common.GETDATE; sp_sqlexec', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmMasiveDistributionFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'InteropCost.InteropCostSecuenceDetail; InteropCost.InteropCostSecuence; Common.Sequense; InteropCost.ProductionCenterCostCenter; InteropCost.ProductionCenter; InteropCost.DistributionFixedAsset; AFNCALDEP; AFNACTIVO; AFNPRODUC; CTNCENCOS; AFNDEPRECI', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmMasiveDistributionFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmMasiveDistributionFixedAsset';
-- GO
