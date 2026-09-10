
-- =============================================
-- Author:        Cristhian Mauricio Salazar Narvaez
-- Create date: 09/11/2016
-- Description:    Procedimiento almacenado para estimar los costos
-- =============================================
CREATE PROCEDURE [InteropCost].[SP_EstimateCost]
    -- Add the parameters for the stored procedure here
    @distributionType tinyint, --1 - primaria, 2 - Secundaria, 3 - Final
    @ContainerNameDGEmpres varchar(50),
    @ContainPayroll bit, --1 - Contiene el módulo de nómina, 0 - sin módulo nómina
    @OnlySimulate bit, -- 1 - Solo simular; 2 - Ejecutar
    @DataXml as xml, --Los datos anteriormente simulados para comparar con los nuevos y así saber si cambiaron o no, si cambiaron se realimentan los nuevos valores como si fuera solo simulacion
    @UserCode as varchar(20)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    begin try
    
        declare @year as int
        declare @month as int
        declare @costEstimateLabor bit
        declare @errors as varchar(max) = ''
		declare @SettingsId int
        select top 1 @year=Year,@month=Month,@costEstimateLabor=CostEstimateLabor, @SettingsId = Id from InteropCost.InteropCostSetting

        declare @tmpPreviusData as table (
            ProductionCenterId int,
            DirectCostDistribution numeric(20,2),
            AutoCostDistribution numeric(20,2),
            ManPowerDistributionDirect numeric(20,2),
            ManPowerDistributionInDirect numeric(20,2),
            FixedAssetDistribution numeric(20,2),
            DispensingDistribution numeric(20,2),
            TransferDistribution numeric(20,2),
            InitialDistribution numeric(20,2),
            IntermediateDistribution numeric(20,2),
            SecondaryDistribution numeric(20,2)
        )
        declare @tmpCost as table (
            ProductionCenterId int,
            ProductionCenterName varchar(500),
            DirectCostDistribution numeric(20,2) default 0,
            AutoCostDistribution numeric(20,2) default 0,
            CostAccountingAdjustment numeric(20,4) default 0,
            ManPowerDistributionDirect numeric(20,2) default 0,
            ManPowerDistributionIndirect numeric(20,2) default 0,
            ManPowerDistributionTotal numeric(20,2) default 0, -- Suma del valor Directo e Indirecto
            ManPowerAccountingAdjustment numeric(20,4) default 0,
            FixedAssetDistribution numeric(20,2) default 0,
            FixedAssetAccountingAdjustment numeric(20,4) default 0,
            DispensingDistribution numeric(20,2) default 0,
            TransferDistribution numeric(20,2) default 0,
            InitialDistribution numeric(20,2) default 0,
            IntermediateDistribution numeric(20,2) default 0,
            SecondaryDirectCostDistribution numeric(20,2) default 0,
            SecondaryAutoCostDistribution numeric(20,2) default 0,
            SecondaryManPowerDistributionDirect numeric(20,2) default 0,
            SecondaryManPowerDistributionIndirect numeric(20,2) default 0,
            SecondaryFixedAssetDistribution numeric(20,2) default 0,
            SecondaryDispensingDistribution numeric(20,2) default 0,
            SecondaryTransferDistribution numeric(20,2) default 0,
            SecondaryDistribution numeric(20,2) default 0
        )
        declare @tmpGeneralLedgerBalance as table (
            ProductionCenterId int,
            Balance numeric(20,4)
        )

		--Se realiza ésta porción de código para la entrega en el hospital pero debe de cambiarse para cuando se realicen los cálculos
		if @distributionType = 3 --Cuando la distribución sea final
		Begin
			
			--Se inserta un valor cualquiera para poder devolver algo
			insert into @tmpCost(ProductionCenterName) values('Test')

			--Se valida que hayan hecho la estimación primaria
			if (select count(*) from InteropCost.CostEstimation where [Year] = @year and [Month] = @month) = 0 
			Begin
                select *, '999' as CodeResult, 'La Distribución Primaria no ha sido confirmada' as MessageResult
                from @tmpCost
                return
            End

			--Se valida que hayan hecho la estimación secundaria
			if (select count(*) from InteropCost.CostEstimation where [Year] = @year and [Month] = @month and SecondaryDistribution > 0) = 0 
			Begin
                select *, '999' as CodeResult, 'La Distribución Secundaria no ha sido confirmada' as MessageResult
                from @tmpCost
                return
            End

			--Se actualiza el mes y el año de parámetros
			if @month = 12 --Si mes llega a doce se reinicia y se pasa al siguiente año
			Begin
				set @month = 1
				set @year += 1
			End
			Else --Sino se aumenta en uno el mes
			Begin
				set @month += 1 
			End

			--Se actulizan los campos de parámetros
			update InteropCost.InteropCostSetting set [Month] = @month, [Year] = @year where Id = @SettingsId

			select *, '000' as CodeResult, 'Ok' as MessageResult
			from @tmpCost
			order by ProductionCenterName

			return
		End

        insert into @tmpPreviusData
        select
            t.x.value('ProductionCenterId[1]','int'),
            t.x.value('DirectCostDistribution[1]','numeric(20,2)'),
            t.x.value('AutoCostDistribution[1]','numeric(20,2)'),
            t.x.value('ManPowerDistributionDirect[1]','numeric(20,2)'),
            t.x.value('ManPowerDistributionInDirect[1]','numeric(20,2)'),
            t.x.value('FixedAssetDistribution[1]','numeric(20,2)'),
            t.x.value('DispensingDistribution[1]','numeric(20,2)'),
            t.x.value('TransferDistribution[1]','numeric(20,2)'),
            t.x.value('InitialDistribution[1]','numeric(20,2)'),
            t.x.value('IntermediateDistribution[1]','numeric(20,2)'),
            t.x.value('SecondaryDistribution[1]','numeric(20,2)')
          from @DataXml.nodes('/header/Detail') t(x)

        --CodeResult: 999 - Error de validación; 000 - Correcto; 111 - Los Datos Cambiaron

        /***Validaciones generales***/
        /*1. Validar que el periodo contable este cerrado*/
        declare @QuantityClose int
        declare @sqlCloseMonth nvarchar(max) = 'select @QuantityClose = count(*) from '+ @ContainerNameDGEmpres +'.dbo.CTNCIEMEN where CTCMESTADO <> 1 and CTCMAÑO = '+ cast(@year as varchar(10)) + ' And CTCMMES = ' + cast(@month as varchar(10))
        print @sqlCloseMonth
        exec sp_executesql @sqlCloseMonth, N'@QuantityClose int output',@QuantityClose output
        if @QuantityClose > 0 begin
            select *, '999' as CodeResult, 'El periodo (' + cast(@year as varchar) + '/' + cast(@month as varchar) + ') debe estar cerrado en contabilidad' as MessageResult
            from @tmpCost
            return;
        end

        /***DISTRIBUCION INICIAL***/
        if @distributionType = 1 begin
        insert into @tmpCost
        select Id,Code+ ' - '+ Name,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 from InteropCost.ProductionCenter where Status = 1
        --Se realizan las distribuciones : Gastos generales, Mano de Obra, Activos fijos, Suministros y Consumos
        
        /*****************************DISTRIBUCIÓN DE GASTOS GENERALES*****************************/
            /*
                DistributionType: 1 - Directa, 2 - Calculada, 3 - Buscada
                MeasurementUnit: 1 - Proporcion, 2 - Valor
            */

            /**************************DISTRIBUCIÓN DE GASTOS DIRECTOS************************/

            ---- Valido que todas las distribuciones esten confirmadas
            if (select count(*) from InteropCost.DistributionDirectCost where [Year] = @year and [Month] = @month and [Status] = 1) > 0 begin
                declare @messajeConfirmDocument varchar(max) = ''
                select @messajeConfirmDocument=stuff((select N';  ' + Code
                from InteropCost.DistributionDirectCost where [Year] = @year and [Month] = @month and [Status] = 1
                for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

                select *, '999' as CodeResult, 'Actualmente Existen Distribuciones de Elementos del Costo sin Confirmar Codigos: ' + @messajeConfirmDocument as MessageResult
                from @tmpCost
                return;
            end    
            ---- Calculamos los gastos directos por mano de obra directa, indirecta y materiales directos e indirectos
            update @tmpCost set DirectCostDistribution += x1.Directvalue
            , ManPowerDistributionDirect += x1.ManPowerDirectvalue
            , ManPowerDistributionIndirect += x1.ManPowerInDirectvalue
            , DispensingDistribution += x1.MaterialDirectvalue
            , TransferDistribution += x1.MaterialInDirectvalue
            from (
                select dcd.ProductionCenterId as pCenterId
                ,case ge.ElementCostType when 1 then sum(dc.Value * dcd.Percentage / 100) else 0 end as ManPowerDirectvalue
                ,case ge.ElementCostType when 2 then sum(dc.Value * dcd.Percentage / 100) else 0 end as ManPowerInDirectvalue
                ,case ge.ElementCostType when 3 then sum(dc.Value * dcd.Percentage / 100) else 0 end as MaterialDirectvalue
                ,case ge.ElementCostType when 4 then sum(dc.Value * dcd.Percentage / 100) else 0 end as MaterialInDirectvalue
                ,case ge.ElementCostType when 5 then sum(dc.Value * dcd.Percentage / 100) else 0 end as Directvalue
                from InteropCost.DistributionDirectCost dc
                inner join InteropCost.GeneralExpense ge on ge.Id = dc.GeneralExpenseId
                inner join InteropCost.DistributionDirectCostDetail dcd on dcd.DistributionDirectCostId = dc.Id
                where dc.Year = @year And dc.Month = @month And ge.Status = 1
                group by dcd.ProductionCenterId, ge.ElementCostType
            ) as x1
            inner join @tmpCost c on x1.pCenterId = c.ProductionCenterId

            ------ Ahora actualizo los valores que fueron calculos de forma Buscada o calculada
            update @tmpCost set AutoCostDistribution += x1.value
            from (
                select dcd.ProductionCenterId as pCenterId
                , sum(dc.Value * dcd.Percentage / 100) as Value
                from InteropCost.DistributionDirectCost dc
                inner join InteropCost.GeneralExpense ge on ge.Id = dc.GeneralExpenseId
                inner join InteropCost.DistributionDirectCostDetail dcd on dcd.DistributionDirectCostId = dc.Id
                where dc.Year = @year And dc.Month = @month And ge.Status = 1
                and ge.Id in (
                    select distinct GeneralExpenseId from InteropCost.DistributionBase where DistributionType in (2,3)
                )
                group by dcd.ProductionCenterId, ge.ElementCostType
            ) as x1
            inner join @tmpCost c on x1.pCenterId = c.ProductionCenterId
            /**************************FIN DISTRIBUCIÓN DE GASTOS DIRECTOS************************/
            ---- Ahora valido que los gastos generales sean iguales a los de contabilidad
            declare @sqlBalanceCost nvarchar(max) = '
            select pc.Id,isnull(Diference,0)
            from InteropCost.ProductionCenter pc
            left join (
            select ph.ProductionCenterId,isnull(case cla.CLANATURA when 1 then sum(isnull(s.CSCDEBITO,0)) - sum(isnull(s.CSCCREDITO,0)) else sum(isnull(s.CSCCREDITO,0)) - sum(isnull(s.CSCDEBITO,0)) end,0) as Diference
            from InteropCost.ProductionCenterHomologation ph
            inner join ' + @ContainerNameDGEmpres + '.dbo.CTNCUENTA c on ph.[AccountOriginId] = c.OID
            inner join ' + @ContainerNameDGEmpres + '.dbo.CTNCLASE cla on cla.OID = c.CTNCLASE
            inner join ' + @ContainerNameDGEmpres + '.dbo.CTNSAL'+ cast(@Year as varchar(10)) +' s on s.CTNCUENTA = c.OID and s.CSCMES = '+ cast(@month as varchar) +'
            inner join InteropCost.ProductionCenterCostCenter pccc on pccc.CostCenterId = s.CTNCENCOS and pccc.ProductionCenterId = ph.ProductionCenterId
            where ph.[HomologationType] = 4
            group by cla.CLANATURA, ph.ProductionCenterId) as data on data.ProductionCenterId = pc.Id
            '
            delete from @tmpGeneralLedgerBalance
            insert into @tmpGeneralLedgerBalance
            exec sp_sqlexec @sqlBalanceCost

            update @tmpCost set CostAccountingAdjustment = b.Balance - (t.DirectCostDistribution + t.AutoCostDistribution), AutoCostDistribution += b.Balance - (t.DirectCostDistribution + t.AutoCostDistribution)
            from @tmpGeneralLedgerBalance b
            inner join @tmpCost t on t.ProductionCenterId = b.ProductionCenterId
            where b.Balance <> (t.DirectCostDistribution + t.AutoCostDistribution)

        
            /********************************************************** MANO DE OBRA **********************************************************/
            /***********Validaciones***********/
            /*
                1. Todos los Gastos Directos con valores tengan distribucion
                Recorrer todos los gastos generales que tengan base de distribucion directa y verificar si la cuenta contable tiene             
                saldo diferente de cero y/o que su saldo sea de la misma naturaleza que el de la cuenta y que tenga distribucion.
            */
        
            
            /*Buscamos el valor a distribuir de cada una de las cuentas para el periodo en cuestion*/
            declare @sqlDGEmpres nvarchar(max) = ''
            declare @mainAccountJoin varchar(max)
            declare @tmpMainAccountValue as table (MainAccountId int, Value decimal(18,0))
            declare @countMainAccountError int
            if @ContainPayroll = 1 begin
                /*****************************DISTRIBUCION DE MANO DE OBRA*****************************/
                /*
                    1. Verificar que el modulo de Nomina este en produccion  (Si no puede continuar sin problema / No hace nada)
                    2. Verificar que el periodo de Nomina que corresponde al mismo periodo de costos este confirmado en todos los grupos.
                */
                declare @TotalGroups int
                declare @TotalGroupsLiquidated int
                select @TotalGroups=coalesce(count(*),0) from Payroll.[Group] where month(LastDateLiquidation) = @month and year(LastDateLiquidation) = @year
                select @TotalGroupsLiquidated=count(*) from (
                    select Code, Liquidation, day(LastDateLiquidation) as DayOfMonth from Payroll.[Group]
                    where month(LastDateLiquidation) = @month and year(LastDateLiquidation) = @year
                    and day(LastDateLiquidation) = (case when Liquidation = 1 then 1 else 16 end)
                ) as x
                if @TotalGroups <> @TotalGroupsLiquidated begin
                    select @errors+= ', ' + Code from Payroll.[Group]
                    where month(LastDateLiquidation) = @month and year(LastDateLiquidation) = @year
                    and day(LastDateLiquidation) != (case when Liquidation = 1 then 1 else 16 end)
                    select *, '999' as CodeResult, 'Faltan por liquidar los grupos de nomina (' + substring(@errors, 3, datalength(@errors)) + ')' as MessageResult
                    from @tmpCost
                    return;
                end
            
                update @tmpCost set ManPowerDistributionDirect += x1.ValueDirect, ManPowerDistributionIndirect += x1.ValueInDirect
                from (
                    select x0.ProductionCenterId as pCenter, sum(cast(ProductionCenterValueDirect as numeric(20,2))) as ValueDirect, sum(cast(ProductionCenterValueInDirect as numeric(20,2))) as ValueInDirect from (
                        select
                        md.ProductionCenterId, (
                            case pc.CenterType when 1 then --- Operativo
                                case @costEstimateLabor
                                when 1 then convert(decimal(5,4), cast(md.HoursQuantity as decimal)/cast(m.HoursWorked as decimal)) * sum(m.TotalAccrued)
                                else
                                convert(decimal(5,4), cast(md.HoursQuantity as decimal)/cast(m.HoursWorked as decimal)) * (sum(m.TotalAccrued) + sum(m.TotalEmployerContribution) + sum(m.TotalParafiscal) + sum(m.TotalProvision))
                                end
                            else
                            0
                            end
                        )
                        as ProductionCenterValueDirect
                        , (
                            case pc.CenterType when 2 then --- Administrativo
                                case @costEstimateLabor
                                when 1 then convert(decimal(5,4), cast(md.HoursQuantity as decimal)/cast(m.HoursWorked as decimal)) * sum(m.TotalAccrued)
                                else
                                convert(decimal(5,4), cast(md.HoursQuantity as decimal)/cast(m.HoursWorked as decimal)) * (sum(m.TotalAccrued) + sum(m.TotalEmployerContribution) + sum(m.TotalParafiscal) + sum(m.TotalProvision))
                                end
                            else
                            0
                            end
                        )
                        as ProductionCenterValueInDirect
                        from InteropCost.DistributionManpower m
                        inner join InteropCost.DistributionManpowerDetail md on md.DistributionManpowerId = m.Id
                        inner join InteropCost.ProductionCenter pc on pc.Id = md.ProductionCenterId
                        where m.Year = @year and m.Month = @month and m.HoursWorked > 0
                        group by md.ProductionCenterId,md.HoursQuantity,m.HoursWorked,m.TotalAccrued,m.TotalEmployerContribution,m.TotalParafiscal,m.TotalProvision, pc.CenterType
                    ) as x0 group by x0.ProductionCenterId
                ) as x1 where ProductionCenterId = x1.pCenter
            end
            /**************************** FIN DISTRIBUCION DE MANO DE OBRA **************************/
            ----- Ahora comparo contra los saldos en contabilidad para realizar los ajustes por mano de obra
            declare @sqlBalanceManPower nvarchar(max) = '
            select pc.Id,isnull(Diference,0)
            from InteropCost.ProductionCenter pc
            left join (
            select ph.ProductionCenterId,isnull(case cla.CLANATURA when 1 then sum(isnull(s.CSCDEBITO,0)) - sum(isnull(s.CSCCREDITO,0)) else sum(isnull(s.CSCCREDITO,0)) - sum(isnull(s.CSCDEBITO,0)) end,0) as Diference
            from InteropCost.ProductionCenterHomologation ph
            inner join ' + @ContainerNameDGEmpres + '.dbo.CTNCUENTA c on ph.[AccountOriginId] = c.OID
            inner join ' + @ContainerNameDGEmpres + '.dbo.CTNCLASE cla on cla.OID = c.CTNCLASE
            inner join ' + @ContainerNameDGEmpres + '.dbo.CTNSAL'+ cast(@Year as varchar(10)) +' s on s.CTNCUENTA = c.OID and s.CSCMES = '+ cast(@month as varchar) +'
            inner join InteropCost.ProductionCenterCostCenter pccc on pccc.CostCenterId = s.CTNCENCOS and pccc.ProductionCenterId = ph.ProductionCenterId
            where ph.[HomologationType] = 1
            group by cla.CLANATURA, ph.ProductionCenterId) as data on data.ProductionCenterId = pc.Id
            '
            delete from @tmpGeneralLedgerBalance
            insert into @tmpGeneralLedgerBalance
            exec sp_sqlexec @sqlBalanceManPower

            update @tmpCost set ManPowerAccountingAdjustment = b.Balance - (t.ManPowerDistributionIndirect + t.ManPowerDistributionDirect), ManPowerDistributionIndirect += case when b.Balance > 0 then b.Balance - (t.ManPowerDistributionIndirect + t.ManPowerDistributionDirect) else t.ManPowerDistributionIndirect end
            from @tmpGeneralLedgerBalance b
            inner join @tmpCost t on t.ProductionCenterId = b.ProductionCenterId
            where b.Balance <> (t.ManPowerDistributionIndirect + t.ManPowerDistributionDirect)

            /*****************************DISTRIBUCION DE SUMINISTROS *******************************/

            declare @TableDispensing table(ProductionCenterId int, Value numeric(18,2))
            declare @SqlDispensing nvarchar(max) = '
            select pc.Id,isnull(Diference,0)
            from InteropCost.ProductionCenter pc
            left join (
            select ph.ProductionCenterId,isnull(case cla.CLANATURA when 1 then sum(isnull(s.CSCDEBITO,0)) - sum(isnull(s.CSCCREDITO,0)) else sum(isnull(s.CSCCREDITO,0)) - sum(isnull(s.CSCDEBITO,0)) end,0) as Diference
            from InteropCost.ProductionCenterHomologation ph
            inner join ' + @ContainerNameDGEmpres + '.dbo.CTNCUENTA c on ph.[AccountOriginId] = c.OID
            inner join ' + @ContainerNameDGEmpres + '.dbo.CTNCLASE cla on cla.OID = c.CTNCLASE
            inner join ' + @ContainerNameDGEmpres + '.dbo.CTNSAL'+ cast(@Year as varchar(10)) +' s on s.CTNCUENTA = c.OID and s.CSCMES = '+ cast(@month as varchar) +'
            inner join InteropCost.ProductionCenterCostCenter pccc on pccc.CostCenterId = s.CTNCENCOS and pccc.ProductionCenterId = ph.ProductionCenterId
            where ph.[HomologationType] = 2
            group by cla.CLANATURA, ph.ProductionCenterId) as data on data.ProductionCenterId = pc.Id
            '
            insert into @TableDispensing
            exec sp_sqlexec @SqlDispensing

            update @tmpCost set [DispensingDistribution] = d.Value
            from @tmpCost tmp
            inner join @TableDispensing d on d.ProductionCenterId = tmp.ProductionCenterId

            /*****************************FIN DISTRIBUCION DE SUMINISTROS *******************************/

            /*****************************DISTRIBUCION DE CONSUMO *******************************/

            declare @TableTransfer table(ProductionCenterId int, Value numeric(18,2))
            declare @SqlTransfer nvarchar(max) = '
            select pc.Id,isnull(Diference,0)
            from InteropCost.ProductionCenter pc
            left join (
            select ph.ProductionCenterId,isnull(case cla.CLANATURA when 1 then sum(isnull(s.CSCDEBITO,0)) - sum(isnull(s.CSCCREDITO,0)) else sum(isnull(s.CSCCREDITO,0)) - sum(isnull(s.CSCDEBITO,0)) end,0) as Diference
            from InteropCost.ProductionCenterHomologation ph
            inner join ' + @ContainerNameDGEmpres + '.dbo.CTNCUENTA c on ph.[AccountOriginId] = c.OID
            inner join ' + @ContainerNameDGEmpres + '.dbo.CTNCLASE cla on cla.OID = c.CTNCLASE
            inner join ' + @ContainerNameDGEmpres + '.dbo.CTNSAL'+ cast(@Year as varchar(10)) +' s on s.CTNCUENTA = c.OID and s.CSCMES = '+ cast(@month as varchar) +'
            inner join InteropCost.ProductionCenterCostCenter pccc on pccc.CostCenterId = s.CTNCENCOS and pccc.ProductionCenterId = ph.ProductionCenterId
            where ph.[HomologationType] = 3
            group by cla.CLANATURA, ph.ProductionCenterId) as data on data.ProductionCenterId = pc.Id
            '
            insert into @TableTransfer
            exec sp_sqlexec @SqlTransfer

            update @tmpCost set TransferDistribution = d.Value
            from @tmpCost tmp
            inner join @TableTransfer d on d.ProductionCenterId = tmp.ProductionCenterId

            /*****************************FIN DISTRIBUCION DE CONSUMO *******************************/

            /*****************************DISTRIBUCION DE ACTIVOS FIJOS*****************************/
            update @tmpCost set FixedAssetDistribution = x1.DeprecationValue
            from (
                select x0.ProductionCenterId as pCenterId, sum(x0.DeprecationValue) as DeprecationValue from (
                    select fd.ProductionCenterId,
                    (f.DepreciationValue * fd.Proportion / 100) as DeprecationValue
                    from InteropCost.DistributionFixedAsset f
                    inner join InteropCost.DistributionFixedAssetDetail fd on fd.DistributionFixedAssetId = f.Id
                    where f.Year = @year and f.Month = @month
                    group by fd.ProductionCenterId,fd.Proportion,f.DepreciationValue
                ) as x0 group by x0.ProductionCenterId
            ) as x1 where ProductionCenterId = x1.pCenterId
            /*****************************FIN DISTRIBUCION DE ACTIVOS FIJOS ***************************/
            ----- Ahora comparo contra los saldos en contabilidad para realizar los ajustes por activos fijos
            declare @sqlBalanceFixed nvarchar(max) = '
            select pc.Id,isnull(Diference,0)
            from InteropCost.ProductionCenter pc
            left join (
            select ph.ProductionCenterId,isnull(case cla.CLANATURA when 1 then sum(isnull(s.CSCDEBITO,0)) - sum(isnull(s.CSCCREDITO,0)) else sum(isnull(s.CSCCREDITO,0)) - sum(isnull(s.CSCDEBITO,0)) end,0) as Diference
            from InteropCost.ProductionCenterHomologation ph
            inner join ' + @ContainerNameDGEmpres + '.dbo.CTNCUENTA c on ph.[AccountOriginId] = c.OID
            inner join ' + @ContainerNameDGEmpres + '.dbo.CTNCLASE cla on cla.OID = c.CTNCLASE
            inner join ' + @ContainerNameDGEmpres + '.dbo.CTNSAL'+ cast(@Year as varchar(10)) +' s on s.CTNCUENTA = c.OID and s.CSCMES = '+ cast(@month as varchar) +'
            inner join InteropCost.ProductionCenterCostCenter pccc on pccc.CostCenterId = s.CTNCENCOS and pccc.ProductionCenterId = ph.ProductionCenterId
            where ph.[HomologationType] = 5
            group by cla.CLANATURA, ph.ProductionCenterId) as data on data.ProductionCenterId = pc.Id'
            delete from @tmpGeneralLedgerBalance
            insert into @tmpGeneralLedgerBalance
            exec sp_sqlexec @sqlBalanceFixed
            
            update @tmpCost set FixedAssetAccountingAdjustment = b.Balance - t.FixedAssetDistribution, FixedAssetDistribution += b.Balance - t.FixedAssetDistribution
            from @tmpGeneralLedgerBalance b
            inner join @tmpCost t on t.ProductionCenterId = b.ProductionCenterId
            where b.Balance <> t.FixedAssetDistribution

            update @tmpCost set InitialDistribution = isnull([@tmpCost].DirectCostDistribution,0) + isnull([@tmpCost].AutoCostDistribution,0) + isnull([@tmpCost].ManPowerDistributionDirect,0) + isnull([@tmpCost].ManPowerDistributionInDirect,0) + isnull([@tmpCost].FixedAssetDistribution,0) + isnull([@tmpCost].DispensingDistribution,0) + isnull([@tmpCost].TransferDistribution,0)
            if @OnlySimulate = 1 begin
                --Comparamos los nuevos datos con los datos previos
                declare @countDiferentRows int
                select @countDiferentRows=coalesce(count(*),0)
                from @tmpCost c
                inner join @tmpPreviusData p on c.ProductionCenterId = p.ProductionCenterId
                where c.DirectCostDistribution <> p.DirectCostDistribution or c.AutoCostDistribution <> p.AutoCostDistribution or c.ManPowerDistributionDirect <> p.ManPowerDistributionDirect or c.ManPowerDistributionIndirect <> p.ManPowerDistributionInDirect
                or c.FixedAssetDistribution <> p.FixedAssetDistribution or c.DispensingDistribution <> p.DispensingDistribution or c.TransferDistribution <> p.TransferDistribution

                if @countDiferentRows > 0 begin
                    select *, '111' as CodeResult, 'Los registros han cambiado, vuelva a verificar los datos antes de confirmar' as MessageResult
                    from @tmpCost
                    return
                end
            end
            else begin

                if(select count(*) from InteropCost.CostEstimation where [Year] = @year and [Month] = @month) > 0 begin
                    select *, '111' as CodeResult, 'La Distribucion Primaria ya fue confirmada y no puede ser reemplazada' as MessageResult
                    from @tmpCost
                    return
                end
                update InteropCost.CostEstimation set
                DirectCostDistribution = [@tmpCost].DirectCostDistribution,
                AutoCostDistribution = [@tmpCost].AutoCostDistribution,
                ManPowerDistributionDirect = [@tmpCost].ManPowerDistributionDirect,
                ManPowerDistributionInDirect = [@tmpCost].ManPowerDistributionInDirect,
                FixedAssetDistribution = [@tmpCost].FixedAssetDistribution,
                DispensingDistribution = [@tmpCost].DispensingDistribution,
                TransferDistribution = [@tmpCost].TransferDistribution,
                ModificationUser = @UserCode,
                ModificationDate = [Common].[GETDATE]()
                from @tmpCost
                where InteropCost.CostEstimation.ProductionCenterId = [@tmpCost].ProductionCenterId And InteropCost.CostEstimation.Year = @year And InteropCost.CostEstimation.Month = @month

                insert into InteropCost.CostEstimation (Year,Month,ProductionCenterId,DirectCostDistribution,AutoCostDistribution,ManPowerDistributionDirect, ManPowerDistributionInDirect,FixedAssetDistribution,DispensingDistribution,TransferDistribution,InitialDistribution,IntermediateDistribution,SecondaryDistribution,CreationUser,CreationDate)
                select @year, @month, ProductionCenterId, DirectCostDistribution, AutoCostDistribution, ManPowerDistributionDirect, ManPowerDistributionIndirect, FixedAssetDistribution, DispensingDistribution, TransferDistribution, InitialDistribution
                ,IntermediateDistribution, SecondaryDistribution, @UserCode, [Common].[GETDATE]()
                from @tmpCost where ProductionCenterId not in (
                    select ProductionCenterId from InteropCost.CostEstimation where Year = @year And Month = @month
                ) --And InitialDistribution <> 0

            end
        end /*** FIN DISTRIBUCION INICIAL***/
        else if @distributionType = 2 begin /*** INICIO DISTRIBUCION SECUNDARIA ***/
            
            if (select count(*) from InteropCost.ProductionCenter where CenterType <> 1 and Status = 1 and Id not in (select ProductionCenterId from InteropCost.DistributionSecondary where Status = 1)) > 0 begin
                insert into @tmpCost (ProductionCenterId, ProductionCenterName)
                values(1,'')
                declare @errorsProductionCenter varchar(MAX)
                select @errorsProductionCenter=stuff((select N'    '+Code    + ' - ' + Name + + ', '
                from InteropCost.ProductionCenter where CenterType <> 1 and Status = 1 and Id not in (select ProductionCenterId from InteropCost.DistributionSecondary where Status = 1)
                for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

                select *, '111' as CodeResult, 'Los siguientes centros de produccion no estan creados como Elementos de Distribucion Secundaria: ' + @errorsProductionCenter as MessageResult
                from @tmpCost
                return
            end

            if(select count(*) from InteropCost.DistributionSecondary ds inner join InteropCost.DistributionSecondaryBase dsb on dsb.DistributionSecondaryId = ds.Id where ds.Status = 1 and dsb.DistributionType = 1 and ds.Id not in (select DistributionSecondaryId from InteropCost.DirectDistributionSecondary where Year = @year and Month = @month)) > 0 begin
                insert into @tmpCost (ProductionCenterId, ProductionCenterName)
                values(1,'')            
                declare @errorsSecondaryDistribution varchar(MAX)
                select @errorsSecondaryDistribution=stuff((select N'    '+ Code + ', '
                from InteropCost.DistributionSecondary ds inner join InteropCost.DistributionSecondaryBase dsb on dsb.DistributionSecondaryId = ds.Id where ds.Status = 1 and dsb.DistributionType = 1 and ds.Id not in (select DistributionSecondaryId from InteropCost.DirectDistributionSecondary where Year = @year and Month = @month)
                for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

                select *, '111' as CodeResult, 'Los siguientes Elementos de Distribucion Secundaria no tienen una Distribucion Secundaria en el mes '+ cast(@month as varchar(10)) +': ' + @errorsSecondaryDistribution as MessageResult
                from @tmpCost
                return
            end

            if (select count(*) from InteropCost.CostEstimation where Year = @year and Month = @month) = 0 begin
                insert into @tmpCost (ProductionCenterId, ProductionCenterName)
                values(1,'')
                select *, '111' as CodeResult, 'No existe una distribucion inicial' as MessageResult
                from @tmpCost
                return
            end
            --- Esta tabla se crea para poder validar de que de haya distribuido el 100% del valor, ya que aveces quedaban unos malditos centavos sin distribuir :P
            declare @TableTmpValueDistribution table(Id int identity(1,1),ProductionCenterId int, DirectCostDistribution numeric(18,2), AutoCostDistribution numeric(18,2), ManPowerDistributionDirect numeric(18,2), ManPowerDistributionInDirect numeric(18,2),
                        FixedAssetDistribution numeric(18,2), DispensingDistribution numeric(18,2), TransferDistribution numeric(18,2))
            
            delete from InteropCost.CostEstimationProductionCenterSecondary where [Year] = @year and [Month] = @month

            insert into @tmpCost(ProductionCenterId, ProductionCenterName, DirectCostDistribution, AutoCostDistribution, ManPowerDistributionDirect, ManPowerDistributionIndirect , FixedAssetDistribution , DispensingDistribution ,TransferDistribution ,InitialDistribution , IntermediateDistribution)
            select ProductionCenterId, pc.Code + ' - ' + pc.Name, DirectCostDistribution, AutoCostDistribution, ManPowerDistributionDirect, ManPowerDistributionIndirect , FixedAssetDistribution , DispensingDistribution ,TransferDistribution ,InitialDistribution , IntermediateDistribution
            from InteropCost.CostEstimation ce
            inner join InteropCost.ProductionCenter pc on pc.Id = ce.ProductionCenterId
            where ce.Year = @year and ce.Month = @month

            declare @IdDirectSecundary int
            DECLARE directSecundary_cursor CURSOR FOR   
            select Id from [InteropCost].[DirectDistributionSecondary] where [Year] = @year and [Month] = @month
 
            OPEN directSecundary_cursor  
 
            FETCH NEXT FROM directSecundary_cursor   
            INTO @IdDirectSecundary
 
            WHILE @@FETCH_STATUS = 0  
            BEGIN  
                declare @IdProductionCenterTmpCursor int = (
                select s.ProductionCenterId from [InteropCost].[DirectDistributionSecondary] ds
                inner join InteropCost.DistributionSecondary s on s.Id = ds.DistributionSecondaryId
                where ds.Id = @IdDirectSecundary
                )
                delete from @TableTmpValueDistribution
                insert into @TableTmpValueDistribution
                 select
                 dat.TargetProductionCenterId
                , sum(ce.DirectCostDistribution * dat.Percentage / 100 ) as DirectCost
                , sum(ce.AutoCostDistribution * dat.Percentage / 100) as AutoCostDistribution
                , sum(ce.ManPowerDistributionDirect * dat.Percentage / 100) as ManPowerDistributionDirect
                , sum(ce.ManPowerDistributionInDirect * dat.Percentage / 100) as ManPowerDistributionInDirect
                , sum(ce.FixedAssetDistribution * dat.Percentage / 100) as FixedAssetDistribution
                , sum(ce.DispensingDistribution * dat.Percentage / 100) as DispensingDistribution
                , sum(ce.TransferDistribution * dat.Percentage / 100) as TransferDistribution
                from (
				select s.ProductionCenterId as SourceProductionCenterId, dsd.ProductionCenterId as TargetProductionCenterId, (dsd.Percentage * 100 / sum(dsd.Percentage) over()) as Percentage
				from [InteropCost].[DirectDistributionSecondary] ds
                inner join InteropCost.DistributionSecondary s on s.Id = ds.DistributionSecondaryId
                inner join [InteropCost].[DirectDistributionSecondaryDetail] dsd on ds.Id = dsd.[DirectDistributionSecondaryId]
				inner join InteropCost.ProductionCenter pc on pc.Id = dsd.ProductionCenterId
                where ds.Id = @IdDirectSecundary and ds.[Year] = @year and ds.[Month] = @month and pc.CenterType = 1
				) as dat
				inner join InteropCost.CostEstimation ce on ce.ProductionCenterId = dat.SourceProductionCenterId
                where ce.[Year] = @year and ce.[Month] = @month
                group by dat.TargetProductionCenterId

                --- Valido y actualizo la distribucion de los valores al 100%
                update @TableTmpValueDistribution
                set DirectCostDistribution += (select sum(DirectCostDistribution) from InteropCost.CostEstimation where ProductionCenterId = @IdProductionCenterTmpCursor and [Year] = @year and [Month] = @month) - (select sum(DirectCostDistribution) from @TableTmpValueDistribution)
                , AutoCostDistribution += (select sum(AutoCostDistribution) from InteropCost.CostEstimation where ProductionCenterId = @IdProductionCenterTmpCursor  and [Year] = @year and [Month] = @month) - (select sum(AutoCostDistribution) from @TableTmpValueDistribution)
                , ManPowerDistributionDirect += (select sum(ManPowerDistributionDirect) from InteropCost.CostEstimation where ProductionCenterId = @IdProductionCenterTmpCursor and [Year] = @year and [Month] = @month) - (select sum(ManPowerDistributionDirect) from @TableTmpValueDistribution)
                , ManPowerDistributionInDirect += (select sum(ManPowerDistributionInDirect) from InteropCost.CostEstimation where ProductionCenterId = @IdProductionCenterTmpCursor and [Year] = @year and [Month] = @month) - (select sum(ManPowerDistributionInDirect) from @TableTmpValueDistribution)
                , FixedAssetDistribution += (select sum(FixedAssetDistribution) from InteropCost.CostEstimation where ProductionCenterId = @IdProductionCenterTmpCursor and [Year] = @year and [Month] = @month) - (select sum(FixedAssetDistribution) from @TableTmpValueDistribution)
                , DispensingDistribution += (select sum(DispensingDistribution) from InteropCost.CostEstimation where ProductionCenterId = @IdProductionCenterTmpCursor and [Year] = @year and [Month] = @month) - (select sum(DispensingDistribution) from @TableTmpValueDistribution)
                , TransferDistribution += (select sum(TransferDistribution) from InteropCost.CostEstimation where ProductionCenterId = @IdProductionCenterTmpCursor and [Year] = @year and [Month] = @month) - (select sum(TransferDistribution) from @TableTmpValueDistribution)
                where Id = (select max(Id) from @TableTmpValueDistribution)

                --- Inserto la distribucion por centro de costos secundario
                INSERT INTO [InteropCost].[CostEstimationProductionCenterSecondary]
               ([Year]
               ,[Month]
               ,[SourceProductionCenterId]
               ,[DistributionType]
               ,[DirectCost]
               ,[AutoCostDistribution]
               ,[ManPowerDistributionDirect]
               ,[ManPowerDistributionInDirect]
               ,[FixedAssetDistribution]
               ,[DispensingDistribution]
               ,[TransferDistribution]
               ,[TargetProductionCenterId])
               select @year
                ,@month
                ,@IdProductionCenterTmpCursor
                , 1
                , DirectCostDistribution
                , AutoCostDistribution
                , ManPowerDistributionDirect
                , ManPowerDistributionInDirect
                , FixedAssetDistribution
                , DispensingDistribution
                , TransferDistribution
                , ProductionCenterId
                from @TableTmpValueDistribution
            
                --- realizo la distribucion secundaria para los directos
                update @tmpCost set SecondaryDirectCostDistribution += data.DirectCostDistribution
                , SecondaryAutoCostDistribution += data.AutoCostDistribution
                , SecondaryManPowerDistributionDirect += data.ManPowerDistributionDirect
                , SecondaryManPowerDistributionInDirect += data.ManPowerDistributionInDirect
                , SecondaryFixedAssetDistribution += data.FixedAssetDistribution
                , SecondaryDispensingDistribution += data.DispensingDistribution
                , SecondaryTransferDistribution += data.TransferDistribution
                from @TableTmpValueDistribution as data
                inner join @tmpCost ce on ce.ProductionCenterId = data.ProductionCenterId

                FETCH NEXT FROM directSecundary_cursor   
                INTO @IdDirectSecundary
            END   
            CLOSE directSecundary_cursor;  
            DEALLOCATE directSecundary_cursor;

            --- realizo la distribucion secundaria para las buscadas y calculadas
            declare @TableProductionCenterDistribution table(Id int identity(1,1), ProductionCenterId int, Percentage decimal(5,2))
            declare @DistributionSecondaryId int
            declare @ProductionCenterId int
            
            DECLARE secondary_cursor CURSOR FOR  
            select Id, ProductionCenterId
            from [InteropCost].[DistributionSecondary]
            where Id not in (
                select [DistributionSecondaryId] from [InteropCost].[DistributionSecondaryBase] dsb
                inner join [InteropCost].[DistributionSecondary] d on d.Id = dsb.DistributionSecondaryId
                where dsb.DistributionType = 1
            ) and Status = 1

            OPEN secondary_cursor  
 
            FETCH NEXT FROM secondary_cursor   
            INTO @DistributionSecondaryId, @ProductionCenterId
 
            WHILE @@FETCH_STATUS = 0  
            BEGIN  
                print ';)'
                ---Obtengo las distribuciones que tiene ese centro de produccion
                delete from @TableProductionCenterDistribution
                declare @DistributionTypeTmp tinyint, @DistributionSecondaryBaseIdTmp int
                declare @AreaTmp bit, @OfficialHoursTmp bit,@SupplyValueTmp bit, @WorkmanshipValueTmp bit, @AssetValueTmp bit, @CousinValueTmp bit, @InvoiceValueTmp bit
                DECLARE secondary_base_cursor CURSOR FOR  
                select Id,DistributionType, [Area],[OfficialHours],[SupplyValue],[WorkmanshipValue],[AssetValue], [CousinValue], [InvoiceValue]
                from [InteropCost].[DistributionSecondaryBase]
                where [DistributionSecondaryId] = @DistributionSecondaryId
                order by MultipleBase
                print ';);)'
                OPEN secondary_base_cursor
                FETCH NEXT FROM secondary_base_cursor   
                INTO @DistributionSecondaryBaseIdTmp, @DistributionTypeTmp, @AreaTmp, @OfficialHoursTmp, @SupplyValueTmp, @WorkmanshipValueTmp, @AssetValueTmp, @CousinValueTmp, @InvoiceValueTmp
 
                WHILE @@FETCH_STATUS = 0  
                BEGIN
                    print ':P'
                    print @DistributionTypeTmp
                    print @DistributionSecondaryBaseIdTmp
                    if @DistributionTypeTmp = 2 begin ---Calculada
                        declare @TotalQuantity numeric(18,2) = (select sum(Quantity) from [InteropCost].[DistributionSecondaryBaseDetail] where [DistributionSecondaryBaseId] = @DistributionSecondaryBaseIdTmp)
                        --- Inserto los porcentajes
                        insert into @TableProductionCenterDistribution
                        select [ProductionCenterId], ([Quantity] * 100 / @TotalQuantity)
                        from [InteropCost].[DistributionSecondaryBaseDetail]
                        where [DistributionSecondaryBaseId] = @DistributionSecondaryBaseIdTmp
                    end
                    else if @DistributionTypeTmp = 3 begin ---Buscada
                        declare @TableSearchAVG table(Id int identity(1,1), ProductionCenterId int, Percentage decimal(5,2))
                        delete from @TableSearchAVG
                        if @AreaTmp = 1 begin -- Buscada por Area
                            declare @TotalArea numeric(18,2) = (select sum(pc.Area) from [InteropCost].[DistributionSecondaryBaseDetail] dsbd inner join InteropCost.ProductionCenter pc on pc.Id = dsbd.ProductionCenterId where [DistributionSecondaryBaseId] = @DistributionSecondaryBaseIdTmp)
                            insert into @TableSearchAVG
                            select ProductionCenterId, (pc.Area * 100 / @TotalArea)
                            from [InteropCost].[DistributionSecondaryBaseDetail] dsbd
                            inner join InteropCost.ProductionCenter pc on pc.Id = dsbd.ProductionCenterId
                            where [DistributionSecondaryBaseId] = @DistributionSecondaryBaseIdTmp
                        end
                        if @OfficialHoursTmp = 1 begin --Si es por horas trabajadas
                            declare @TotalHoursWorked int = (
                            select sum(dmd.HoursQuantity)
                            from InteropCost.DistributionManpower dm
                            inner join InteropCost.DistributionManpowerDetail dmd on dmd.DistributionManpowerId = dm.Id
                            inner join InteropCost.DistributionSecondaryBaseDetail sd on sd.ProductionCenterId = dmd.ProductionCenterId
                            where dm.Status = 1 And dm.Year = @year And dm.Month = @month and sd.DistributionSecondaryBaseId = @DistributionSecondaryBaseIdTmp
                            )
                            insert into @TableSearchAVG
                            select dmd.ProductionCenterId, (dmd.HoursQuantity * 100 / @TotalHoursWorked) as HoursWorked
                            from InteropCost.DistributionManpower dm
                            inner join InteropCost.DistributionManpowerDetail dmd on dmd.DistributionManpowerId = dm.Id
                            inner join InteropCost.DistributionSecondaryBaseDetail sd on sd.ProductionCenterId = dmd.ProductionCenterId
                            where dm.Status = 1 And dm.Year = @year And dm.Month = @month and sd.DistributionSecondaryBaseId = @DistributionSecondaryBaseIdTmp
                        end
                        if @SupplyValueTmp = 1 begin -- Si es por valor del suministro
                            declare @tmpMainAccountValuesSupply table (ProductionCenterId int , Value decimal(18,0))
                            delete from @tmpMainAccountValuesSupply
                            declare @SelectSupply nvarchar(max) =  'select pc.Id, ABS(COALEsCE(SUM([CSCDEBITO]) - sum([CSCCREDITO]),0))
                            from [InteropCost].[DistributionSecondaryBaseDetail] sbd
                            inner join InteropCost.ProductionCenter pc on sbd.ProductionCenterId = pc.Id
                            inner join InteropCost.ProductionCenterHomologation pch on pch.ProductionCenterId = pc.Id
                            inner join ' + @ContainerNameDGEmpres +'.dbo.CTNSAL'+ cast(@Year as varchar(20)) +' s on s.CTNCUENTA = pch.AccountOriginId
                            inner join InteropCost.ProductionCenterCostCenter pccc on pccc.CostCenterId = s.CTNCENCOS and pccc.ProductionCenterId = pch.ProductionCenterId
                            where sbd.DistributionSecondaryBaseId = '+cast(@DistributionSecondaryBaseIdTmp as varchar(20))+' And pch.HomologationType = 2 And pc.Status = 1 and s.CSCMES = '+cast(@month as varchar(10))+'
                            group by pc.Id'
                            insert into @tmpMainAccountValuesSupply
                            exec sp_executesql @SelectSupply
                            insert into @TableSearchAVG
                            select ProductionCenterId, (Value * 100 / (select SUM(Value) from @tmpMainAccountValuesSupply))
                            from @tmpMainAccountValuesSupply
                        end
                        if @WorkmanshipValueTmp = 1 begin
                            declare @tmpMainAccountValuesWorkMan table (ProductionCenterId int , Value decimal(18,0))
                            delete from @tmpMainAccountValuesWorkMan
                            declare @SelectWorkMan nvarchar(max) =  'select pc.Id, ABS(COALEsCE(SUM([CSCDEBITO]) - sum([CSCCREDITO]),0))
                            from [InteropCost].[DistributionSecondaryBaseDetail] sbd
                            inner join InteropCost.ProductionCenter pc on sbd.ProductionCenterId = pc.Id
                            inner join InteropCost.ProductionCenterHomologation pch on pch.ProductionCenterId = pc.Id
                            inner join ' + @ContainerNameDGEmpres +'.dbo.CTNSAL'+ cast(@Year as varchar(20)) +' s on s.CTNCUENTA = pch.AccountOriginId
                            inner join InteropCost.ProductionCenterCostCenter pccc on pccc.CostCenterId = s.CTNCENCOS and pccc.ProductionCenterId = pch.ProductionCenterId
                            where sbd.DistributionSecondaryBaseId = ' + cast(@DistributionSecondaryBaseIdTmp as varchar(20)) + ' And pch.HomologationType = 1 And pc.Status = 1 and s.CSCMES = '+cast(@month as varchar(10))+'
                            group by pc.Id'
                            if @DistributionSecondaryBaseIdTmp = 8 begin
                                print @SelectWorkMan
                            end
                            insert into @tmpMainAccountValuesWorkMan
                            exec sp_executesql @SelectWorkMan
                            insert into @TableSearchAVG
                            select ProductionCenterId, (Value * 100 / (select SUM(Value) from @tmpMainAccountValuesWorkMan))
                            from @tmpMainAccountValuesWorkMan
                        end
                        if @AssetValueTmp = 1 begin
                            declare @tmpMainAccountValuesAsset table (ProductionCenterId int , Value decimal(18,0))
                            delete from @tmpMainAccountValuesAsset
                            declare @SelectAsset nvarchar(max) =  'select pc.Id, ABS(COALEsCE(SUM([CSCDEBITO]) - sum([CSCCREDITO]),0))
                            from [InteropCost].[DistributionSecondaryBaseDetail] sbd
                            inner join InteropCost.ProductionCenter pc on sbd.ProductionCenterId = pc.Id
                            inner join InteropCost.ProductionCenterHomologation pch on pch.ProductionCenterId = pc.Id
                            inner join ' + @ContainerNameDGEmpres +'.dbo.CTNSAL'+ cast(@Year as varchar(20)) +' s on s.CTNCUENTA = pch.AccountOriginId
                            inner join InteropCost.ProductionCenterCostCenter pccc on pccc.CostCenterId = s.CTNCENCOS and pccc.ProductionCenterId = pch.ProductionCenterId
                            where sbd.DistributionSecondaryBaseId = ' + cast(@DistributionSecondaryBaseIdTmp as varchar(20)) + ' And pch.HomologationType = 5 And pc.Status = 1 and s.CSCMES = '+cast(@month as varchar(10))+'
                            group by pc.Id'
                            insert into @tmpMainAccountValuesAsset
                            exec sp_executesql @SelectAsset
                            insert into @TableSearchAVG
                            select ProductionCenterId, (Value * 100 / (select SUM(Value) from @tmpMainAccountValuesAsset))
                            from @tmpMainAccountValuesAsset
                        end
                        if @InvoiceValueTmp = 1 begin
                            declare @tmpMainAccountValuesInvoice table (ProductionCenterId int , Value decimal(18,0))
                            delete from @tmpMainAccountValuesInvoice
                            declare @SelectInvoice nvarchar(max) =  'select pc.Id, ABS(COALEsCE(SUM([CSCDEBITO]) - sum([CSCCREDITO]),0))
                            from [InteropCost].[DistributionSecondaryBaseDetail] sbd
                            inner join InteropCost.ProductionCenter pc on sbd.ProductionCenterId = pc.Id
                            inner join InteropCost.ProductionCenterHomologation pch on pch.ProductionCenterId = pc.Id
                            inner join ' + @ContainerNameDGEmpres + '.dbo.CTNSAL'+ cast(@Year as varchar(20)) +' s on s.CTNCUENTA = pch.AccountOriginId
                            inner join InteropCost.ProductionCenterCostCenter pccc on pccc.CostCenterId = s.CTNCENCOS and pccc.ProductionCenterId = pch.ProductionCenterId
                            where sbd.DistributionSecondaryBaseId = ' + cast(@DistributionSecondaryBaseIdTmp as varchar(20)) + ' And pch.HomologationType = 6 And pc.Status = 1 and s.CSCMES = '+cast(@month as varchar(10))+'
                            group by pc.Id'
                            insert into @tmpMainAccountValuesInvoice
                            exec sp_executesql @SelectInvoice
                            insert into @TableSearchAVG
                            select ProductionCenterId, (Value * 100 / (select SUM(Value) from @tmpMainAccountValuesInvoice))
                            from @tmpMainAccountValuesInvoice
                        end
                        
                        if (select count(*) from @TableSearchAVG) = 0 begin
                            insert into @TableProductionCenterDistribution
                            select ProductionCenterId
                            ,1.0/(select count(*) from [InteropCost].[DistributionSecondaryBaseDetail] where DistributionSecondaryBaseId = @DistributionSecondaryBaseIdTmp)
                            from [InteropCost].[DistributionSecondaryBaseDetail] where DistributionSecondaryBaseId = @DistributionSecondaryBaseIdTmp
                            if (select sum(Percentage) from @TableProductionCenterDistribution) < 100 begin
                                update @TableProductionCenterDistribution set Percentage += 100.0 - (select sum(Percentage) from @TableProductionCenterDistribution) where Id = (select top 1 Id from @TableProductionCenterDistribution)
                            end
                        end
                        else begin
                            -- Tabla Temporal para guardar los consolidados por centro de produccion y validar que no supere ni este por debajo del 100%
                            declare @TablePercentageFinal table(Id int identity(1,1),ProductionCenterId int, Percentage numeric(5,2))
                            delete from @TablePercentageFinal
                            insert into @TablePercentageFinal
                            select ProductionCenterId, AVG(Percentage) from @TableSearchAVG group by ProductionCenterId

                            if (select sum(Percentage) from @TablePercentageFinal) < 100 begin
                                update @TablePercentageFinal set Percentage += 100.00 - (select sum(Percentage) from @TablePercentageFinal) where Id = (select max(Id) from @TablePercentageFinal)
                            end
                            else if (select sum(Percentage) from @TablePercentageFinal) > 100 begin
                                update @TablePercentageFinal set Percentage -= (select sum(Percentage) from @TablePercentageFinal) - 100.00 where Id = (select max(Id) from @TablePercentageFinal)
                            end
                            insert into @TableProductionCenterDistribution
                            select ProductionCenterId, Percentage from @TablePercentageFinal
                        end
                    end --- Fin Distribucion buscada
                    print ':P:P'
                    FETCH NEXT FROM secondary_base_cursor   
                    INTO @DistributionSecondaryBaseIdTmp, @DistributionTypeTmp, @AreaTmp, @OfficialHoursTmp, @SupplyValueTmp, @WorkmanshipValueTmp, @AssetValueTmp, @CousinValueTmp, @InvoiceValueTmp
                END   
                CLOSE secondary_base_cursor;  
                DEALLOCATE secondary_base_cursor;
                print ':O'
                
                delete from @TableTmpValueDistribution
                insert into @TableTmpValueDistribution
                select
                tmp.ProductionCenterId as ProductionCenterId
                , sum(ce.DirectCostDistribution * tmp.Percentage / 100.00) as DirectCost
                , sum(ce.AutoCostDistribution * tmp.Percentage / 100.00) as AutoCostDistribution
                , sum(ce.ManPowerDistributionDirect * tmp.Percentage / 100.00) as ManPowerDistributionDirect
                , sum(ce.ManPowerDistributionInDirect * tmp.Percentage / 100.00) as ManPowerDistributionInDirect
                , sum(ce.FixedAssetDistribution * tmp.Percentage / 100.00) as FixedAssetDistribution
                , sum(ce.DispensingDistribution * tmp.Percentage / 100.00) as DispensingDistribution
                , sum(ce.TransferDistribution * tmp.Percentage / 100.00) as TransferDistribution
                from @TableProductionCenterDistribution tmp
                inner join InteropCost.CostEstimation ce on ce.ProductionCenterId = @ProductionCenterId
                where  ce.[Year] = @year and ce.[Month] = @month
                group by tmp.ProductionCenterId

                --- Valido y actualizo la distribucion de los valores al 100%
                update @TableTmpValueDistribution
                set DirectCostDistribution += (select sum(DirectCostDistribution) from InteropCost.CostEstimation where ProductionCenterId = @ProductionCenterId and [Year] = @year and [Month] = @month) - (select sum(DirectCostDistribution) from @TableTmpValueDistribution)
                , AutoCostDistribution += (select sum(AutoCostDistribution) from InteropCost.CostEstimation where ProductionCenterId = @ProductionCenterId and [Year] = @year and [Month] = @month) - (select sum(AutoCostDistribution) from @TableTmpValueDistribution)
                , ManPowerDistributionDirect += (select sum(ManPowerDistributionDirect) from InteropCost.CostEstimation where ProductionCenterId = @ProductionCenterId and [Year] = @year and [Month] = @month) - (select sum(ManPowerDistributionDirect) from @TableTmpValueDistribution)
                , ManPowerDistributionInDirect += (select sum(ManPowerDistributionInDirect) from InteropCost.CostEstimation where ProductionCenterId = @ProductionCenterId and [Year] = @year and [Month] = @month) - (select sum(ManPowerDistributionInDirect) from @TableTmpValueDistribution)
                , FixedAssetDistribution += (select sum(FixedAssetDistribution) from InteropCost.CostEstimation where ProductionCenterId = @ProductionCenterId and [Year] = @year and [Month] = @month) - (select sum(FixedAssetDistribution) from @TableTmpValueDistribution)
                , DispensingDistribution += (select sum(DispensingDistribution) from InteropCost.CostEstimation where ProductionCenterId = @ProductionCenterId and [Year] = @year and [Month] = @month) - (select sum(DispensingDistribution) from @TableTmpValueDistribution)
                , TransferDistribution += (select sum(TransferDistribution) from InteropCost.CostEstimation where ProductionCenterId = @ProductionCenterId and [Year] = @year and [Month] = @month) - (select sum(TransferDistribution) from @TableTmpValueDistribution)
                where Id = (select max(Id) from @TableTmpValueDistribution)
                

                INSERT INTO [InteropCost].[CostEstimationProductionCenterSecondary]
               ([SourceProductionCenterId]
               ,[DistributionType]
               ,[DirectCost]
               ,[AutoCostDistribution]
               ,[ManPowerDistributionDirect]
               ,[ManPowerDistributionInDirect]
               ,[FixedAssetDistribution]
               ,[DispensingDistribution]
               ,[TransferDistribution]
               ,[TargetProductionCenterId]
			   ,[Year]
			   ,[Month])
               select @ProductionCenterId
                , @DistributionTypeTmp
                , DirectCostDistribution
                , AutoCostDistribution
                , ManPowerDistributionDirect
                , ManPowerDistributionInDirect
                , FixedAssetDistribution
                , DispensingDistribution
                , TransferDistribution
                , ProductionCenterId
				, @year
				, @month
                from @TableTmpValueDistribution

                --- Distribuyo el valor de la estimacion inicial
                update @tmpCost set SecondaryDirectCostDistribution += data.DirectCostDistribution
                , SecondaryAutoCostDistribution += data.AutoCostDistribution
                , SecondaryManPowerDistributionDirect += data.ManPowerDistributionDirect
                , SecondaryManPowerDistributionInDirect += data.ManPowerDistributionInDirect
                , SecondaryFixedAssetDistribution += data.FixedAssetDistribution
                , SecondaryDispensingDistribution += data.DispensingDistribution
                , SecondaryTransferDistribution += data.TransferDistribution
                from @TableTmpValueDistribution data
                inner join @tmpCost ce on ce.ProductionCenterId = data.ProductionCenterId
                print ':O:O'
                FETCH NEXT FROM secondary_cursor   
                INTO @DistributionSecondaryId, @ProductionCenterId
            END   
            CLOSE secondary_cursor;  
            DEALLOCATE secondary_cursor;

            update [@tmpCost] set SecondaryDistribution = [@tmpCost].SecondaryDirectCostDistribution + [@tmpCost].SecondaryAutoCostDistribution + [@tmpCost].SecondaryManPowerDistributionDirect + [@tmpCost].SecondaryManPowerDistributionInDirect + [@tmpCost].SecondaryFixedAssetDistribution + [@tmpCost].SecondaryDispensingDistribution + [@tmpCost].SecondaryTransferDistribution + [@tmpCost].InitialDistribution
            from @tmpCost
            where [@tmpCost].ProductionCenterId in (select Id from InteropCost.ProductionCenter WHERE CenterType = 1)

            update InteropCost.CostEstimation set
                SecondaryDirectCostDistribution = [@tmpCost].SecondaryDirectCostDistribution,
                SecondaryAutoCostDistribution = [@tmpCost].SecondaryAutoCostDistribution,
                SecondaryManPowerDistributionDirect = [@tmpCost].SecondaryManPowerDistributionDirect,
                SecondaryManPowerDistributionInDirect = [@tmpCost].SecondaryManPowerDistributionInDirect,
                SecondaryFixedAssetDistribution = [@tmpCost].SecondaryFixedAssetDistribution,
                SecondaryDispensingDistribution = [@tmpCost].SecondaryDispensingDistribution,
                SecondaryTransferDistribution = [@tmpCost].SecondaryTransferDistribution,
                SecondaryDistribution = [@tmpCost].SecondaryDistribution,
                ModificationUser = @UserCode,
                ModificationDate = [Common].[GETDATE]()
                from @tmpCost
                inner join InteropCost.CostEstimation ce on ce.ProductionCenterId = [@tmpCost].ProductionCenterId
                where ce.[Year] = @year And ce.[Month] = @month and [@tmpCost].ProductionCenterId in (select Id from InteropCost.ProductionCenter WHERE CenterType = 1)

            --- Elimino los centros de costos que no son operativos
            delete from @tmpCost where [@tmpCost].ProductionCenterId in (select Id from InteropCost.ProductionCenter WHERE CenterType <> 1)
        end    /*** FIN DISTRIBUCION SECUNDARIA ***/
        
        --- Actualizo el campo de total de Mano de Obra
        update @tmpCost set ManPowerDistributionTotal = ManPowerDistributionDirect + ManPowerDistributionIndirect

        select *, '000' as CodeResult, 'Ok' as MessageResult
        from @tmpCost
        order by ProductionCenterName

    end try
    begin catch
        --Close InfoValidation
        --Deallocate InfoValidation
        select *, '999' as CodeResult, error_message() + ' Linea: ' + cast(ERROR_LINE() as varchar(20)) as MessageResult
        from @tmpCost
        order by ProductionCenterName
    end catch
    
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento almacenado que ejecuta la estimación y confirmación de costos por período (mes y año) en el módulo de interoperabilidad contable. Según el tipo de distribución solicitado (primaria, secundaria o final), calcula y acumula los costos de cada centro de producción considerando costos directos, mano de obra (horas trabajadas, nómina, provisiones y aportes patronales desde DistributionManpower y DistributionManpowerDetail), distribución secundaria (bases e importes desde DistributionSecondary y DistributionSecondaryBaseDetail) y ajustes contables del mayor general. Permite tanto simular el resultado antes de confirmar como ejecutar el cierre definitivo, comparando los datos nuevos contra una simulación previa enviada como XML para detectar si hubo cambios; en la distribución final, valida que las etapas anteriores estén confirmadas y avanza el período contable en la configuración (InteropCostSetting). Construye parte de su lógica mediante SQL dinámico sobre la empresa configurada (@ContainerNameDGEmpres), por lo que toca tablas del balance contable en tiempo de ejecución.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'PROCEDURE', @level1name = N'SP_EstimateCost';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'PROCEDURE', @level1name = N'SP_EstimateCost';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula y opcionalmente persiste las distribuciones de costos por centro de producción (primaria, secundaria o final) para el periodo configurado, conciliando los valores con los saldos contables de la empresa y avanzando el periodo al confirmar la etapa final.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_EstimateCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un registro en InteropCost.InteropCostSetting (provee Year, Month, CostEstimateLabor, Id).; El parámetro @distributionType indica la etapa: 1=Primaria, 2=Secundaria, 3=Final.; El parámetro @ContainerNameDGEmpres debe corresponder a una BD de empresa con tablas CTNCIEMEN, CTNCUENTA, CTNCLASE, CTNSAL{Year} y CSCMES accesibles.; Para etapa 3 (final), debe existir al menos un registro en CostEstimation para (Year, Month) y otro con SecondaryDistribution > 0.; El periodo (Year, Month) debe estar cerrado en CTNCIEMEN (CTCMESTADO = 1) de la empresa configurada.; Para etapa 1 (primaria), no debe haber DistributionDirectCost del periodo con Status = 1 sin confirmar.; Si @ContainPayroll = 1, todos los Payroll.Group cuya LastDateLiquidation cae en el periodo deben estar liquidados según su periodicidad (día 1 si Liquidation=1, día 16 en otro caso).; Para etapa 2 (secundaria), todos los ProductionCenter con CenterType<>1 y Status=1 deben existir como DistributionSecondary con Status=1; las DistributionSecondary directas deben tener su DirectDistributionSecondary del periodo; y debe haber estimación primaria previa para (Year, Month).', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_EstimateCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los cálculos siempre se hacen para el (Year, Month) configurados en InteropCost.InteropCostSetting (TOP 1).; El periodo contable a costear debe estar cerrado (CTNCIEMEN.CTCMESTADO = 1) en la empresa @ContainerNameDGEmpres.; La distribución secundaria solo procede si previamente existe una distribución primaria en CostEstimation para (Year, Month).; La distribución final solo procede si existen registros en CostEstimation para el periodo y al menos uno con SecondaryDistribution > 0.; La estimación primaria, una vez confirmada (existe fila en CostEstimation para Year/Month), no puede reemplazarse en modo ejecución.; Los ajustes contables (CostAccountingAdjustment, ManPowerAccountingAdjustment, FixedAssetAccountingAdjustment) cuadran las distribuciones calculadas contra el saldo contable obtenido de CTNSAL{Year} según la naturaleza de la cuenta (CLANATURA).; En la distribución secundaria, el último registro de @TableTmpValueDistribution se ajusta para que la suma redistribuida iguale el total del centro origen al 100% (corrección de centavos).; Cuando una base buscada no produce filas, se reparte equitativamente (1/N) entre los centros del detalle, ajustando el último para completar 100%.; Cuando la suma de porcentajes de la base buscada difiere de 100, se ajusta el último registro hacia arriba o hacia abajo para totalizar exactamente 100%.; La distribución secundaria solo afecta CostEstimation y @tmpCost para centros de producción con CenterType = 1 (operativos); los no operativos se eliminan del resultado.; Mano de obra solo se calcula si @ContainPayroll = 1 y todos los grupos de nómina del periodo están liquidados según su periodicidad (Liquidation=1 → día 1, otro → día 16).; El cálculo de mano de obra incluye aportes patronales, parafiscales y provisiones solo si @costEstimateLabor ≠ 1.; El valor de mano de obra se prorratea por HoursQuantity/HoursWorked y se asigna a Directo si CenterType=1 (operativo) o Indirecto si CenterType=2 (administrativo).; InitialDistribution = suma de DirectCost + AutoCost + ManPowerDirect + ManPowerIndirect + FixedAsset + Dispensing + Transfer.; SecondaryDistribution = suma de los componentes Secondary* + InitialDistribution, solo para centros operativos.; ManPowerDistributionTotal siempre se calcula como ManPowerDistributionDirect + ManPowerDistributionIndirect antes de retornar.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_EstimateCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Distribución primaria de costos; Distribución secundaria de costos; Distribución final; Centro de producción operativo/administrativo; Mano de obra directa e indirecta; Activos fijos / depreciación; Gastos generales (directos, calculados, buscados); Suministros y consumos (dispensación y traslado); Homologación contable de centros de producción; Cierre de periodo contable; Liquidación de nómina por grupos; Bases de distribución (área, horas, valor de suministro, mano de obra, activos, facturación)', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_EstimateCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_EstimateCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'InteropCost.InteropCostSetting; InteropCost.CostEstimation; InteropCost.ProductionCenter; InteropCost.DistributionDirectCost; InteropCost.DistributionDirectCostDetail; InteropCost.GeneralExpense; InteropCost.DistributionBase; InteropCost.ProductionCenterHomologation; InteropCost.ProductionCenterCostCenter; Payroll.Group; InteropCost.DistributionManpower; InteropCost.DistributionManpowerDetail; InteropCost.DistributionFixedAsset; InteropCost.DistributionFixedAssetDetail; InteropCost.DistributionSecondary; InteropCost.DistributionSecondaryBase; InteropCost.DistributionSecondaryBaseDetail; InteropCost.DirectDistributionSecondary; InteropCost.DirectDistributionSecondaryDetail; InteropCost.CostEstimationProductionCenterSecondary', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_EstimateCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_EstimateCost';
-- GO
