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