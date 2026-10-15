
-- =============================================
-- Author:		Cristhian Mauricio Salazar
-- Create date: 04/11/2016
-- Description:	Store para el reporte de resultado de operaciones
-- =============================================
-- =============================================
-- Author:		Carlos Jhefersson Muñoz Ramirez
-- Create Modified: 10/04/2017
-- Description:	Stored para el reporte de Structure Organizacional Mayorizado
-- =============================================

CREATE PROCEDURE [InteropCost].[SP_ReportOperatingResult]
	@InitialMonth int,
	@EndMonth int,
	@Year int,
	@Container varchar(20),
	@CodePCenterIni varchar(50),
    @CodePCenterFin varchar(50),
    @StructureOfCostId int
AS
BEGIN

	if @CodePCenterFin = ''
		Begin
			set @CodePCenterFin = 'z'
		End
	
	declare @StringSelect nvarchar(max)
	declare @StringSelect12 nvarchar(max)
	declare @StringSelect12AND nvarchar(max)
	declare @TableBillingValue table(ProductionCenterId int, CostCenterId int, Value numeric(18,0))

	set @StringSelect = 'select hm.ProductionCenterId,pccc.CostCenterId,abs(sum(CSCCREDITO - CSCDEBITO)) from '+ @Container +'.dbo.CTNSAL'+ cast(@Year as varchar(20)) +' sal
	inner join '+ @Container +'.dbo.CTNCUENTA cue on cue.OID = sal.CTNCUENTA
	inner join InteropCost.ProductionCenterHomologation hm on hm.AccountOrigin = cue.CUECODIGO and hm.HomologationType = 6
	inner join InteropCost.ProductionCenterCostCenter pccc on pccc.ProductionCenterId = hm.ProductionCenterId and pccc.CostCenterId = sal.CTNCENCOS
	where CSCMES >= '+ cast(@InitialMonth as varchar(20)) +' and CSCMES <= '+ CAST(@EndMonth as varchar(20)) +'
	group by hm.ProductionCenterId, pccc.CostCenterId'


	set @StringSelect12 = 'select hm.ProductionCenterId,pccc.CostCenterId,sum(MOV.CMMVALCRE -MOV.CMMVALDEB) from '+ @Container +'.dbo.CTNCOMD'+ cast(@Year as varchar(20)) +' as Mov 
	inner join '+ @Container +'.dbo.CTNCOM'+ cast(@Year as varchar(20)) +' as CMov on Cmov.OID = Mov.CtNCOMCONC
	inner join '+ @Container +'.dbo.CTNCUENTA as MA on MA.OID = Mov.CTNCUENTA
	inner join InteropCost.ProductionCenterHomologation hm on hm.AccountOrigin = MA.CUECODIGO and hm.HomologationType = 6
	inner join InteropCost.ProductionCenterCostCenter pccc on pccc.ProductionCenterId = hm.ProductionCenterId and pccc.CostCenterId = MOV.CTNCENCOS
	where CMov.CTNTIPCOM <> 67 and Month(COMFECCOM) = 12 and MA.CTNCLASE = 5
	group by hm.ProductionCenterId, pccc.CostCenterId'

	set @StringSelect12AND = 'select hm.ProductionCenterId,pccc.CostCenterId,abs(sum(CSCDEBITO - CSCCREDITO)) from '+ @Container +'.dbo.CTNSAL'+ cast(@Year as varchar(20)) +' sal
	inner join '+ @Container +'.dbo.CTNCUENTA cue on cue.OID = sal.CTNCUENTA
	inner join InteropCost.ProductionCenterHomologation hm on hm.AccountOrigin = cue.CUECODIGO and hm.HomologationType = 6
	inner join InteropCost.ProductionCenterCostCenter pccc on pccc.ProductionCenterId = hm.ProductionCenterId and pccc.CostCenterId = sal.CTNCENCOS
	where CSCMES >= '+ cast(@InitialMonth as varchar(20)) +' and CSCMES <= 11
	group by hm.ProductionCenterId, pccc.CostCenterId
	UNION ALL
	select hm.ProductionCenterId,pccc.CostCenterId,sum(MOV.CMMVALCRE -MOV.CMMVALDEB) from '+ @Container +'.dbo.CTNCOMD'+ cast(@Year as varchar(20)) +' as Mov 
	inner join '+ @Container +'.dbo.CTNCOM'+ cast(@Year as varchar(20)) +' as CMov on Cmov.OID = Mov.CtNCOMCONC
	inner join '+ @Container +'.dbo.CTNCUENTA as MA on MA.OID = Mov.CTNCUENTA
	inner join InteropCost.ProductionCenterHomologation hm on hm.AccountOrigin = MA.CUECODIGO and hm.HomologationType = 6
	inner join InteropCost.ProductionCenterCostCenter pccc on pccc.ProductionCenterId = hm.ProductionCenterId and pccc.CostCenterId = MOV.CTNCENCOS
	where CMov.CTNTIPCOM <> 67 and Month(COMFECCOM) = 12 and MA.CTNCLASE = 5
	group by hm.ProductionCenterId, pccc.CostCenterId'
		

	if @EndMonth = 12 and @InitialMonth = 12 begin
		insert into @TableBillingValue
		exec sp_executesql @StringSelect12
	end
	else if @EndMonth = 12 and @InitialMonth <= 11 begin
		insert into @TableBillingValue
		exec sp_executesql @StringSelect12AND
	--Sobraria solo puede ir el else...	
	end 
	else if @EndMonth <> 12 and @InitialMonth <= 11 begin
		insert into @TableBillingValue
		exec sp_executesql @StringSelect
	end


	-- Creamos la tabla temporal que devolveremos con la OrganizationalStructure organizada
    declare @TableOrganizationalStructure table(Id Integer,OrganizationalStructureCode varchar(20) ,OrganizationalStructureName varchar(100),ParentId Integer,TotalCost decimal(20,2),BillingValue decimal(20,2),Diference decimal(20,2),Margin decimal(20,2),Utility decimal(20,2),[Level] tinyint)





	declare @IdStructure int
	
	DECLARE structure_cursor CURSOR FOR   
	select Id from InteropCost.OrganizationalStructureOfCosts where ParentId is null
  
	OPEN structure_cursor  
  
	FETCH NEXT FROM structure_cursor   
	INTO @IdStructure
  
	WHILE @@FETCH_STATUS = 0  
	BEGIN  
		
		-- Llenamos la Tabla Temporal a Retornar pasamos el id del padre(1) and inciamos desde el nivel (0)
		insert into @TableOrganizationalStructure
		select Id,Code,[Name],ParentId,0,0,0,0,0,cast(NumberLevel as tinyint) from InteropCost.[fnRecursiveStructureLevel](@IdStructure,0)

		FETCH NEXT FROM structure_cursor   
		INTO @IdStructure
	END   
	CLOSE structure_cursor;  
	DEALLOCATE structure_cursor;













	

	-- Creamos la tabla temporal que devolveremos con los datos Mayorizados
    declare @TableExecution table(Id Integer,OrganizationalStructureCode varchar(20) ,OrganizationalStructureName varchar(100),ParentId Integer,TotalCost decimal(20,2),BillingValue decimal(20,2),Diference decimal(20,2),Margin decimal(20,2),Utility decimal(20,2),[Level] tinyint)

	--Insertamos los hijos a la tabla temporal en un nivel 3
	insert into @TableExecution
	select 
	os.Id, 
	os.Code,
	os.[Name],
	os.ParentId,
	sum(cast(ce.SecondaryDistribution  as decimal (20,2) )) as TotalCost,
	sum(cast(isnull(bv.Value,0) as decimal (20,2) )) as BillingValue,
	sum(cast(isnull(bv.Value,0) - ce.SecondaryDistribution  as decimal (20,2) )) as Diference,
	sum(cast((isnull(bv.Value,0) - ce.SecondaryDistribution) * 100 / case isnull(ce.SecondaryDistribution,0) when 0 then 1 else isnull(ce.SecondaryDistribution,0) end as decimal (20,2) )) as Margin,
	case isnull(bv.Value,0) when 0 then 0 else sum(cast((isnull(bv.Value,0) - ce.SecondaryDistribution) * 100 / case isnull(bv.Value,0) when 0 then 1 else isnull(bv.Value,0) end as decimal (20,2) )) end as Utility,
	cast(3 as tinyint) as [level]
	from InteropCost.ProductionCenter pc
	inner join (select ProductionCenterId, sum(SecondaryDistribution) as SecondaryDistribution from InteropCost.CostEstimation where [Month] >= @InitialMonth and [Month] <= @EndMonth and [Year] = @Year group by ProductionCenterId) ce on ce.ProductionCenterId = pc.Id
	--inner join InteropCost.CostEstimation ce on ce.ProductionCenterId = pc.Id
	LEFT JOIN InteropCost.OrganizationalStructureOfCosts as os on os.Id = pc.OrganizationalStructureOfCostId
	left join (select ProductionCenterId, sum(Value) as Value from @TableBillingValue group by ProductionCenterId) bv on bv.ProductionCenterId = pc.Id
	where pc.CenterType = 1 and pc.[Status] = 1 and pc.Code >= @CodePCenterIni and pc.Code <= @CodePCenterFin	
	--and pc.OrganizationalStructureOfCostId In (select id from [InteropCost].[OrganizationalStructureOfCosts] as oec where @StructureOfCostId = case @StructureOfCostId when 0 then @StructureOfCostId else oec.Id end or @StructureOfCostId = case @StructureOfCostId when 0 then @StructureOfCostId else oec.[ParentId] end)
	--and pc.OrganizationalStructureOfCostId in (select * from InteropCost.fnRecursiveStructure(case @StructureOfCostId when 0 then 1 else @StructureOfCostId end))
	group by os.Id,os.Code,os.[Name],os.ParentId,ce.ProductionCenterId, pc.Id, pc.Code, pc.[Name], bv.[Value] --, ce.SecondaryDistribution
	order by case isnull(bv.Value,0) when 0 then 0 else sum(cast((isnull(bv.Value,0) - ce.SecondaryDistribution) * 100 / case isnull(bv.Value,0) when 0 then 1 else isnull(bv.Value,0) end as decimal (20,2) )) end desc

	--Recorremos los Hijos para Obtener los Padres
	-- mayorizamos los saldos a nivel de ParentId
	declare @TotalCostMayor decimal(20,2)
	declare @BillingValueMayor decimal(20,2)
	declare @DiferenceMayor decimal(20,2)
	declare @MarginMayor decimal(20,2)
	declare @UtilityMayor decimal(20,2)
	declare @ParentIdSon Integer

		declare detail_cursor cursor for
		select sum(TotalCost),sum(BillingValue), sum(Diference), sum(Margin),sum(Utility),ParentId from @TableExecution group by ParentId

		open detail_cursor
				FETCH NEXT FROM detail_cursor
				INTO @TotalCostMayor,@BillingValueMayor,@DiferenceMayor,@MarginMayor,@UtilityMayor,@ParentIdSon

				WHILE @@FETCH_STATUS = 0
				BEGIN

				--Valores Sumatorias Externas
				declare @IdStructureFather integer
				declare @CodeStructureFather varchar(20)
				declare @NameStructureFather varchar(100)
				declare @ParentIdStructureFather integer

				--Select OrganizationalStructureOfCosts los padres de los hijos que ya hemos obtenido.
				select @IdStructureFather = Id,@CodeStructureFather = Code,@NameStructureFather = [Name],@ParentIdStructureFather = ParentId 
				from InteropCost.OrganizationalStructureOfCosts
				where Id = @ParentIdSon

				--Insertamos los padres a la tabla temporal en un nivel 2
				insert into @TableExecution (Id,OrganizationalStructureCode,OrganizationalStructureName,ParentId,TotalCost,BillingValue,Diference,Margin,Utility,[level])
				values (@IdStructureFather,@CodeStructureFather,@NameStructureFather,@ParentIdStructureFather,@TotalCostMayor,@BillingValueMayor,@DiferenceMayor,@MarginMayor,@UtilityMayor,cast(2 as tinyint))
						
				FETCH NEXT FROM detail_cursor
				INTO @TotalCostMayor,@BillingValueMayor,@DiferenceMayor,@MarginMayor,@UtilityMayor,@ParentIdSon
				END
		close detail_cursor
		deallocate detail_cursor

		--Recorremos los Padres para Obtener los Abuelos
		-- mayorizamos los saldos a nivel de ParentId
		declare @TotalCostMayorFather decimal(20,2)
		declare @BillingValueMayorFather decimal(20,2)
		declare @DiferenceMayorFather decimal(20,2)
		declare @MarginMayorFather decimal(20,2)
		declare @UtilityMayorFather decimal(20,2)
		declare @ParentIdFather Integer

		declare detail_cursorStructure cursor for
		select sum(TotalCost),sum(BillingValue), sum(Diference), sum(Margin),sum(Utility),ParentId from @TableExecution where [Level] = cast(2 as tinyint) group by ParentId

		open detail_cursorStructure
				FETCH NEXT FROM detail_cursorStructure
				INTO @TotalCostMayorFather,@BillingValueMayorFather,@DiferenceMayorFather,@MarginMayorFather,@UtilityMayorFather,@ParentIdFather

				WHILE @@FETCH_STATUS = 0
				BEGIN 
								
					--Valores Sumatorias Externas
					declare @IdStructureGrandFather integer
					declare @CodeStructureGrandFather varchar(20)
					declare @NameStructureGrandFather varchar(100)
					declare @ParentIdStructureGrandFather integer
						
					--Select OrganizationalStructureOfCosts los padres de los hijos que ya hemos obtenido.
					select @IdStructureGrandFather = Id,@CodeStructureGrandFather = Code,@NameStructureGrandFather = [Name],@ParentIdStructureGrandFather = ParentId 
					from InteropCost.OrganizationalStructureOfCosts
					where Id = @ParentIdFather
					----------
					--Valores Sumatorias Externas
					declare @IdStructureGrandFather2 integer
					declare @CodeStructureGrandFather2 varchar(20)
					declare @NameStructureGrandFather2 varchar(100)
					declare @ParentIdStructureGrandFather2 integer

					--Para encontrar de nivel 1
					select @IdStructureGrandFather2 = Id,@CodeStructureGrandFather2 = Code,@NameStructureGrandFather = [Name],@ParentIdStructureGrandFather2 = ParentId 
					from InteropCost.OrganizationalStructureOfCosts
					where ParentId = @ParentIdFather 
					
					--Insertamos los padres a la tabla temporal en un nivel 1
					insert into @TableExecution (Id,OrganizationalStructureCode,OrganizationalStructureName,ParentId,TotalCost,BillingValue,Diference,Margin,Utility,[level])
					values (@IdStructureGrandFather,@CodeStructureGrandFather,@NameStructureGrandFather,@ParentIdStructureGrandFather,@TotalCostMayorFather,@BillingValueMayorFather,@DiferenceMayorFather,@MarginMayorFather,@UtilityMayorFather,cast(1 as tinyint))
				FETCH NEXT FROM detail_cursorStructure
				INTO @TotalCostMayorFather,@BillingValueMayorFather,@DiferenceMayorFather,@MarginMayorFather,@UtilityMayorFather,@ParentIdFather
				END
		close detail_cursorStructure
		deallocate detail_cursorStructure

		--Recorremos los Abuelos para Obtener la Raiz
		-- mayorizamos los saldos a nivel de ParentId
		declare @TotalCostMayorGrandFather decimal(20,2)
		declare @BillingValueMayorGrandFather decimal(20,2)
		declare @DiferenceMayorGrandFather decimal(20,2)
		declare @MarginMayorGrandFather decimal(20,2)
		declare @UtilityMayorGrandFather decimal(20,2)
		declare @ParentIdGrandFather Integer

		declare detail_cursorStructureGrandFather cursor for
		select sum(TotalCost),sum(BillingValue), sum(Diference), sum(Margin),sum(Utility),ParentId from @TableExecution where [Level] = cast(1 as tinyint) group by ParentId

		open detail_cursorStructureGrandFather
				FETCH NEXT FROM detail_cursorStructureGrandFather
				INTO @TotalCostMayorGrandFather,@BillingValueMayorGrandFather,@DiferenceMayorGrandFather,@MarginMayorGrandFather,@UtilityMayorGrandFather,@ParentIdGrandFather

				WHILE @@FETCH_STATUS = 0
				BEGIN 
								
					--Valores Sumatorias Externas
					declare @IdStructureRoot integer
					declare @CodeStructureRoot varchar(20)
					declare @NameStructureRoot varchar(100)
					declare @ParentIdStructureRoot integer
						
					--Select OrganizationalStructureOfCosts los Abuelos de la Raiz que ya hemos obtenido.
					select @IdStructureRoot =Id,@CodeStructureRoot = Code,@NameStructureRoot = [Name],@ParentIdStructureRoot = ParentId 
					from InteropCost.OrganizationalStructureOfCosts
					where Id = @ParentIdGrandFather

					--Insertamos los padres a la tabla temporal en un nivel 0
					insert into @TableExecution (Id,OrganizationalStructureCode,OrganizationalStructureName,ParentId,TotalCost,BillingValue,Diference,Margin,Utility,[level])
					values (@IdStructureRoot,@CodeStructureRoot,@NameStructureRoot,@ParentIdStructureRoot,@TotalCostMayorGrandFather,@BillingValueMayorGrandFather,@DiferenceMayorGrandFather,@MarginMayorGrandFather,@UtilityMayorGrandFather,cast(0 as tinyint))
				FETCH NEXT FROM detail_cursorStructureGrandFather
				INTO @TotalCostMayorGrandFather,@BillingValueMayorGrandFather,@DiferenceMayorGrandFather,@MarginMayorGrandFather,@UtilityMayorGrandFather,@ParentIdGrandFather
				END
		close detail_cursorStructureGrandFather
		deallocate detail_cursorStructureGrandFather
	--Return
	--select Id,OrganizationalStructureCode,OrganizationalStructureName,ParentId,TotalCost,BillingValue,Diference,Margin,Utility,[Level] from @TableExecution
	--union ALL
	--select Id,Code,[Name],ParentId,0,0,0,0,0,cast(NumberLevel as tinyint) from InteropCost.[fnRecursiveStructureLevel](1,0) where Id not in (select Id from @TableExecution)

	--Actualizo a la OrganizationalStructure los datos Consultados
	UPDATE @TableOrganizationalStructure  SET TotalCost = te.TotalCost, BillingValue = te.BillingValue, Diference = te.Diference, Margin = te.Margin, Utility = te.Utility
	FROM (select Id,TotalCost,BillingValue,Diference,Margin,Utility from @TableExecution) as te inner join @TableOrganizationalStructure as t on t.Id = te.Id
	
	--Return
	select Id,OrganizationalStructureCode,OrganizationalStructureName,ParentId,TotalCost,BillingValue,Diference,Margin,Utility,[Level] from @TableOrganizationalStructure
END