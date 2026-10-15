-- =============================================
-- Author:		Cristhian Mauricio Salazar
-- Create date: 23-01-2015
-- Description:	Store para crear ordenes de servicio
-- =============================================
CREATE PROCEDURE [Billing].[SP_GenerateServiceOrder_Output]
	@ServiceOrderXml as xml,
	@User varchar(20),
	------------------------------------------------------
	@CodeResult VARCHAR(3) OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT,
	@StatusResult TINYINT OUTPUT,
	@Id INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	SET DATEFORMAT DMY

	Declare @Uno Tinyint = 1, 
			@Cero Tinyint = 0,
			@Dos Tinyint = 2,
			@Tres Tinyint = 3,
			@Cuatro Tinyint = 4,
			@Modified Varchar(8) = 'Modified',
			@Deleted Varchar(7) = 'Deleted',
			@Added Varchar(5) = 'Added',
			@Unchanged Varchar(9) = 'Unchanged',
			@Tag755 Varchar(3) = '755',
			@errors varchar(max),
			------------------------------
			@SubXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX)


	declare @AdmissionDate datetime
	declare @AdmissionStatus char(1)

	declare @RevenueControlId int, @MaxFolio INT
	DECLARE @SettingInventoryId INT,
			@AssociateCostMainAccount TINYINT,
			@AuthorizationNumberControl BIT

	declare @IdServiceOrder int, @CodeServiceOrder varchar(20), @AdmissionNumber varchar(10), @PatientCode varchar(25), @OrderDate datetime, @AffectInventory bit, @EntityCode varchar(20), @EntityId int, @EntityName varchar(250), @OperatingUnitId int, @Status tinyint	
	declare @CodeMessageAnnulment varchar(20), @MessageAnnulment varchar(max) --Variables para la reversión de rias
	declare @generateXmlAnnulment xml --Variable para generar el xml de anulación de rias
	declare @IdRevenueControlDetailPackage int -- Variable usada cuando se empaqueta y se desempaqueta, esta es para dejar los servicios en el mismo folio

	declare @TableDetail table(	RowId Int Identity(1,1) Primary Key,IdRow int, IdDetail int, ServiceOrderId int NOT NULL,
								CareGroupId int NOT NULL, ExcludeIds varchar(200), HealthAdministratorId int NULL, 
								ThirdPartyId int NULL, ServiceType tinyint NOT NULL, RecordType tinyint NOT NULL,
								CUPSEntityId int NULL, IPSServiceId int NULL, HospitalStayId int NULL, HospitalStayDetailId int NULL,
								ControlExternalConsultation tinyint NULL, ControlExternalConsultationCode numeric(20,2) NULL,
								CUPSAssociateService bit NOT NULL, CodeAssociateService varchar(50) NULL, IsPackage bit NOT NULL,
								Packaging bit NOT NULL, PackageServiceOrderDetailId int NULL, LiquidationType tinyint NOT NULL,
								Presentation tinyint NULL,ProductId int NULL,InvoicedQuantity int NOT NULL,SupplyQuantity int NOT NULL,
								DevolutionQuantity int NOT NULL,RateManualSalePrice numeric(20,2) NOT NULL,CostValue numeric(20, 2) NOT NULL,
								ServiceDate datetime NOT NULL,AuthorizationNumber varchar(20) NULL,PerformsFunctionalUnitId int NOT NULL,
								PerformsHealthProfessionalCode char(20) NULL,PerformsProfessionalSpecialty char(3) NULL,
								PerformsHealthProfessionalThirdPartyId int NULL,BillingConceptId int NULL,CostCenterId int NOT NULL,
								SettlementType tinyint NOT NULL,IncludeServiceOrderDetailId int NULL, IncludeServiceOrderDetailIdRow int NULL,
								RecoveryRatio numeric(5, 2) NULL,RateManualId int NULL,RateManualType tinyint NULL,RateManualDetailId int NULL,
								DefinitionRateDetailId int NULL,DefinitionRateDetailConditionId int NULL,SubTotalSalesPrice numeric(20, 2) NOT NULL,
								ThirdPartyDiscount numeric(20,2) NOT NULL,ThirdPartyDiscountPercentage numeric(5, 2) NOT NULL,
								TotalSalesPrice numeric(20, 2) NOT NULL,GrandTotalSalesPrice numeric(20,2) NOT NULL,SurchargeApply bit NOT NULL,
								SurgicalInterventionType tinyint NULL,SurgeryNumber tinyint NOT NULL,IsFirstEvent bit NOT NULL,IsAnnulled bit NOT NULL,
								IsDelete bit NOT NULL,IncomeMainAccountId int NOT NULL,EntityState varchar(50),ApplyRIAS varchar(20) NULL,
								RIASCupsId int NULL,RealizedQuantity int NULL,CUPSEntityContractDescriptionId int null,
								QuotationServiceOrderDetailId int NULL,TraceabilityPaperworkEventsId int null, ContractPackageId int null,
								RevenueControlDetailId INT,CareGroupType TINYINT,ContractEntityId INT, IsServiceOrderDetailControlJustify BIT NOT NULL,
								ServiceOrderDetailControlJustification VARCHAR(500),FinalProductCost DECIMAL(20,2) NOT NULL,
								GrossValue NUMERIC(20,2) NOT NULL,TaxValue NUMERIC(20,2) NOT NULL, IvaId INT NULL, EconomicActivityId INT NULL)
	declare @TableSurgical table(Id int NOT NULL,ServiceOrderDetailIdRow int NOT NULL,ServiceOrderDetailId int NOT NULL,IPSServiceId int NOT NULL,InvoicedQuantity int NOT NULL,LiquidationPercentage numeric(5, 2) NOT NULL,RateManualSalePrice numeric(20,2) NOT NULL,TotalSalesPrice numeric(20,2) NOT NULL,PerformsHealthProfessionalCode char(20) NULL,PerformsHealthProfessionalThirdPartyId int NULL,CostValue numeric(20, 2) NOT NULL,BillingConceptId int NOT NULL,CostCenterId int NOT NULL,RateManualDetailSurgicalId int NULL,SurchargeApply bit NOT NULL,OnlyMedicalFees bit NOT NULL ,IncomeMainAccountId int NOT NULL, EntityState varchar(50),EconomicActivityId INT NULL)--cambios aqui
	declare @TableRevenueControlDetailRefresh table(RowId Int Identity(1,1) Primary Key, Id int) --- Tabla temporal para guardar los Id de los Folios que se van a tocar en la orden para luego recalcularlos
	declare @XmlAnnulmentRias table(CupsCode varchar(20), RIASCupsId int)--Tabla para almacenar la info para la anulación de rias	
	declare @IDSRevenueControlDetail table(Id int primary key, CareGroupId int, ThirdPartyId int)
	begin try
		select	@IdServiceOrder = t.x.value('Id[1]','int'),
				@CodeServiceOrder = t.x.value('Code[1]','varchar(20)'),
				@AdmissionNumber = t.x.value('AdmissionNumber[1]','varchar(10)'),
				@PatientCode = t.x.value('PatientCode[1]','varchar(25)'),
				@OrderDate = t.x.value('OrderDate[1]', 'datetime'),
				@AffectInventory = t.x.value('AffectInventory[1]','bit'),
				@EntityCode = t.x.value('EntityCode[1]','varchar(20)'),
				@EntityId = t.x.value('EntityId[1]','int'),
				@EntityName = t.x.value('EntityName[1]','varchar(250)'),
				@OperatingUnitId = t.x.value('OperatingUnitId[1]','int'),
				@Status = isnull(t.x.value('Status[1]','tinyint'),0)
		from @ServiceOrderXml.nodes('/ServiceOrder') t(x)
		
		insert into @TableDetail
			select coalesce(t.x.value('RowXml[1]','int'), ROW_NUMBER() OVER(ORDER BY t.x.value('Id[1]','int') ASC)),
					t.x.value('Id[1]','int'),
					@IdServiceOrder,
					t.x.value('CareGroupId[1]','int'),
					t.x.value('ExcludeIds[1]','varchar(200)' ),
					t.x.value('HealthAdministratorId[1]','int'),
					t.x.value('ThirdPartyId[1]','int'),
					t.x.value('ServiceType[1]','tinyint'),
					t.x.value('RecordType[1]','tinyint'),
					t.x.value('CUPSEntityId[1]','int'),
					t.x.value('IPSServiceId[1]','int'),
					t.x.value('HospitalStayId[1]','int'),
					t.x.value('HospitalStayDetailId[1]','int'),
					t.x.value('ControlExternalConsultation[1]','tinyint'),
					t.x.value('ControlExternalConsultationCode[1]','numeric(20,2)'),
					t.x.value('CUPSAssociateService[1]','bit'),
					t.x.value('CodeAssociateService[1]','varchar(50)'),
					t.x.value('IsPackage[1]','bit'),
					t.x.value('Packaging[1]','bit'),
					t.x.value('PackageServiceOrderDetailId[1]','int'),
					t.x.value('LiquidationType[1]','tinyint'),
					t.x.value('Presentation[1]','tinyint'),
					t.x.value('ProductId[1]','int'),
					t.x.value('InvoicedQuantity[1]','int'),
					t.x.value('SupplyQuantity[1]','int'),
					t.x.value('DevolutionQuantity[1]','int'),
					t.x.value('RateManualSalePrice[1]','numeric(20,2)'),
					t.x.value('CostValue[1]','numeric(20,2)'),
					t.x.value('ServiceDate[1]', 'datetime'),
					t.x.value('AuthorizationNumber[1]','varchar(20)'),
					t.x.value('PerformsFunctionalUnitId[1]','int'),
					t.x.value('PerformsHealthProfessionalCode[1]','varchar(20)'),
					t.x.value('PerformsProfessionalSpecialty[1]','varchar(3)'),
					t.x.value('PerformsHealthProfessionalThirdPartyId[1]','int'),
					t.x.value('BillingConceptId[1]','int'),
					t.x.value('CostCenterId[1]','int'),
					t.x.value('SettlementType[1]','tinyint'),
					t.x.value('IncludeServiceOrderDetailId[1]','int'),
					t.x.value('IncludeServiceOrderDetailIdRow[1]','int'),
					t.x.value('RecoveryRatio[1]','numeric(5,2)'),
					t.x.value('RateManualId[1]','int'),
					t.x.value('RateManualType[1]','tinyint'),
					t.x.value('RateManualDetailId[1]','int'),
					t.x.value('DefinitionRateDetailId[1]','int'),
					t.x.value('DefinitionRateDetailConditionId[1]','int'),
					t.x.value('SubTotalSalesPrice[1]','numeric(20,2)'),
					t.x.value('ThirdPartyDiscount[1]','numeric(20,2)'),
					t.x.value('ThirdPartyDiscountPercentage[1]','numeric(5,2)'),
					t.x.value('TotalSalesPrice[1]','numeric(20,2)'),
					t.x.value('GrandTotalSalesPrice[1]','numeric(20,2)'),
					t.x.value('SurchargeApply[1]','bit'),
					t.x.value('SurgicalInterventionType[1]','tinyint'),
					t.x.value('SurgeryNumber[1]','tinyint'),
					t.x.value('IsFirstEvent[1]','bit'),
					t.x.value('IsAnnulled[1]','bit'),
					t.x.value('IsDelete[1]','bit'),
					t.x.value('IncomeMainAccountId[1]','int'),
					t.x.value('EntityState[1]','varchar(50)'),
					t.x.value('ApplyRIAS[1]','varchar(20)'),
					t.x.value('RIASCupsId[1]','int'),
					t.x.value('RealizedQuantity[1]','int'),
					t.x.value('CUPSEntityContractDescriptionId[1]','int'),
					t.x.value('QuotationServiceOrderDetailId[1]','int'),
					t.x.value('TraceabilityPaperworkEventsId[1]','int'),
					t.x.value('ContractPackageId[1]','int'),					
					NULL, NULL, NULL,
					ISNULL(t.x.value('IsServiceOrderDetailControlJustify[1]','bit'), 0),
					t.x.value('ServiceOrderDetailControlJustification[1]','varchar(500)'),
					Isnull(t.x.value('FinalProductCost[1]','Decimal(20,2)'),0),
					t.x.value('GrossValue[1]','numeric(20,2)'),
					t.x.value('TaxValue[1]','numeric(20,2)'),
					NULL,
					t.x.value('EconomicActivityId[1]','INT')--recibimos los datos --Cambios aqui
			from @ServiceOrderXml.nodes('/ServiceOrder/ServiceOrderDetail') t(x)
			
		insert into @TableSurgical
			select	t.x.value('Id[1]','int'),
					t.x.value('ServiceOrderDetailIdRow[1]','int'),
					t.x.value('ServiceOrderDetailId[1]','int'),
					t.x.value('IPSServiceId[1]','int'),
					t.x.value('InvoicedQuantity[1]','int'),
					t.x.value('LiquidationPercentage[1]','numeric(5,2)'),
					t.x.value('RateManualSalePrice[1]','numeric(18,0)'),
					t.x.value('TotalSalesPrice[1]','numeric(18,0)'),
					t.x.value('PerformsHealthProfessionalCode[1]','varchar(20)'),
					t.x.value('PerformsHealthProfessionalThirdPartyId[1]','int'),
					t.x.value('CostValue[1]','numeric(18,2)'),
					t.x.value('BillingConceptId[1]','int'),
					t.x.value('CostCenterId[1]','int'),
					t.x.value('RateManualDetailSurgicalId[1]','int'),
					t.x.value('SurchargeApply[1]','bit'),
					t.x.value('OnlyMedicalFees[1]','bit'),
					t.x.value('IncomeMainAccountId[1]','int'),
					t.x.value('EntityState[1]','varchar(50)'),
					NULL
			from @ServiceOrderXml.nodes('/ServiceOrder/ServiceOrderDetail/ServiceOrderDetailSurgical') t(x)
			WHERE t.x.value('Id[1]','int') IS NOT NULL

			DECLARE	@TransacationEconommicActivity BIT = (select top 1 cs.TransactionEconomicActivity from GeneralLedger.CompanySettings cs)
			
			IF @TransacationEconommicActivity = 1 
			BEGIN
				UPDATE TS 
				SET TS.EconomicActivityId = BC.EconomicActivityId 
				FROM  @TableSurgical TS
					JOIN Billing.BillingConcept BC ON BC.ID = TS.BillingConceptId
			END
			

		-- Como el formulario de ordenes de servicio tambien valida que si lo va a eliminar y esta en un factura anulada simplemente cambia el valor de IsDelete a true y yo necesito es que venga marcado como eliminado en el EntityState
		update @TableDetail set EntityState = 'Deleted' where IsDelete = @Uno and EntityState = @Modified

		-- Se actualiza el campo iva segun el parametrizado en TableDetail 
		--(Servicios)
		update  td set	td.IvaId = IIF(iis.TaxedProduct=1 AND td.TaxValue>0,gli1.Id,NULL)
		from @TableDetail td 
		join Contract.IPSService iis WITH (NOLOCK) on  iis.Id = td.IPSServiceId
		join GeneralLedger.GeneralLedgerIVA gli1 WITH (NOLOCK)  on gli1.Id = iis.IVAId

		--(productos) Se establece el iva correspondiente siempre y cuando este gravado y ademas liquide IVA para ventas Salud
		update  td set	td.IvaId = IIF(ip.TaxedProduct=1 and ip.LiquidateSalesTaxes=1, gli2.Id,null),
						td.TaxValue = IIF(ip.TaxedProduct = 1 and ip.LiquidateSalesTaxes =1,td.TaxValue,0),
						td.GrossValue = IIF(ip.TaxedProduct = 1 and ip.LiquidateSalesTaxes =1,td.GrossValue,td.SubTotalSalesPrice)
		from @TableDetail td 
		join Inventory.InventoryProduct ip WITH (NOLOCK)  on ip.Id = td.ProductId
		join GeneralLedger.GeneralLedgerIVA gli2 WITH (NOLOCK) on gli2.Id = ip.IVAId

		/*****__se actualiza la tabla temp detalle para que tome en cuenta el healthAdministrator cuando sea un grupo de atencion diferente a particular__********/
		UPDATE td set td.HealthAdministratorId =IIF(cg.CareGroupType=3,NULL,td.HealthAdministratorId)
		from @TableDetail td
		join Contract.CareGroup cg on td.CareGroupId=cg.Id
		where td.EntityState <> @Deleted

		/*****************************************************   *****************************************************/

		-----------------------------------------validaciones en caso de que el ingreso esté bloqueado---------------------------------------------------------------

		Declare @LockType TINYINT
			select Top 1 @LockType = sb.IncomeLockType 
			From Billing.SettingsBilling sb where sb.IdOperatingUnit = @OperatingUnitId

		Declare @LockStatus char(3)
			select Top 1 @LockStatus = ai.IESTADOIN 
			From dbo.ADINGRESO ai where ai.NUMINGRES= @AdmissionNumber

		IF  @LockStatus ='B' AND @LockType = 1 AND ISNULL(@EntityName,'') = 'PharmaceuticalDispensing' 
		begin
			select	@CodeResult = '999', 
						@MessageResult = 'No se puede realizar la acción debido a que el ingreso tiene un bloqueo de tipo Farmacia', 
						@StatusResult = 3,
						@Id = 0
		    return
		end
		IF  @LockStatus ='B' AND @LockType = 2 AND ISNULL(@EntityName, '') <> 'PharmaceuticalDispensing' 
		begin
			select	@CodeResult = '999', 
						@MessageResult = 'No se puede realizar la acción debido a que el ingreso tiene un bloqueo de tipo Facturación', 
						@StatusResult = 3,
						@Id = 0
		    return
		END

		IF  @LockStatus ='B' AND @LockType = 3 
		begin
			select	@CodeResult = '999', 
						@MessageResult = 'No se puede realizar la acción debido a que el ingreso se encuentra bloqueado en Farmacia y Facturación', 
						@StatusResult = 3,
						@Id = 0
		    return
		END
			
			 
		IF @Status = 3  ---ESTADO ANULADO
		BEGIN

			/*********************************************  VALIDACIONES *********************************************/

			IF @EntityName = 'PharmaceuticalDispensing' AND @Status = 3 BEGIN
				select	@CodeResult = '999', 
						@MessageResult = 'No se puede anular una orden de servicio generada desde dispensación', 
						@StatusResult = 3,
						@Id = 0
				return
			END

			select @AdmissionStatus = IESTADOIN 
			from dbo.ADINGRESO With(Nolock) 
			where NUMINGRES = @AdmissionNumber

			if @AdmissionStatus = 'F' begin
				select	@CodeResult = '999', 
						@MessageResult = CONCAT('No se puede anular la orden de servicio porque el ingreso ', RTRIM(LTRIM(@AdmissionNumber)), ' esta facturado'), 
						@StatusResult = 3,
						@Id = 0
				return
			end	

			if Exists (select 1 from @TableDetail td 
				inner join Billing.ServiceOrderDetailDistribution sodd With(Nolock) on td.IdDetail = sodd.ServiceOrderDetailId 
				inner join Billing.RevenueControlDetail rcd With(Nolock) on rcd.Id = sodd.RevenueControlDetailId 
				where rcd.[Status] > @Uno) begin
				set @errors = (select 'Folio: ' + cast(rcd.FolioOrder as varchar(5)) + ', ' 
					from @TableDetail td 
					inner join Billing.ServiceOrderDetailDistribution sodd With(Nolock) on td.IdDetail = sodd.ServiceOrderDetailId 
					inner join Billing.RevenueControlDetail rcd With(Nolock) on rcd.Id = sodd.RevenueControlDetailId 
					where rcd.[Status] > @Uno for XML PATH(''))
				
				select	@CodeResult = '999', 
						@MessageResult = CONCAT('La orden de servicio no se puede anular debido que tiene items en folios que estan bloqueados o anulados: ', @errors), 
						@StatusResult = 3,
						@Id = 0
				return
			end
			
			if Exists (select 1 from @TableDetail where HospitalStayId is not null) begin
				if Exists (select 1 from @TableDetail td 
					inner join dbo.CHREGESTADET chd With(Nolock) on chd.ID = td.HospitalStayDetailId 
					inner join dbo.CHREGESTADET chdtmp With(Nolock) on chdtmp.IDCHREGESTA = chd.IDCHREGESTA and chdtmp.GENLIQUIDA > chd.GENLIQUIDA) begin

					select	@CodeResult = '999', 
							@MessageResult = 'No se puede anular la orden de servicio porque la estancia tiene otras liquidaciones de estancia con fechas superiores a esta', 
							@StatusResult = 3,
							@Id = 0
					return
				end

				UPDATE Billing.AccountControlStays
				set ServiceOrderDetailId = NULL
					,LiquidationDate = NULL
				WHERE ServiceOrderDetailId IN (SELECT ServiceOrderId from @TableDetail)
				
				delete det
				from dbo.CHREGESTADET det With(Nolock)
				Inner Join @TableDetail On det.ID = HospitalStayDetailId

				Declare @FechUltima Date
				select Top 1 @FechUltima = GENLIQUIDA 
				From dbo.CHREGESTADET Det With(Nolock)
				Inner Join dbo.CHREGESTA ta With(Nolock) On Det.IDCHREGESTA = ta.ID
				Where ta.NUMINGRES = @AdmissionNumber
				order By Det.ID Desc

				--If @FechUltima Is Not Null Begin
				Update dbo.ADINGRESO Set GENULTLIQUI = @FechUltima
				Where NUMINGRES = @AdmissionNumber
				--End

				--- Actializo el estado de la estancias 1 - sin liquidar 2 - Liquidado parcial
				update dbo.CHREGESTA set GENESTLIQ =  data.GENESTLIQ
				from (select ch.ID, case isnull(chd.IDCHREGESTA,0) when 0 then 1 else 2 end as GENESTLIQ from @TableDetail td inner join dbo.CHREGESTA ch With(Nolock) on td.HospitalStayId = ch.ID left join dbo.CHREGESTADET chd With(Nolock) on chd.IDCHREGESTA = ch.ID group by ch.ID, chd.IDCHREGESTA) data
				inner join dbo.CHREGESTA chre With(Nolock) on chre.ID = data.ID
			end

			if Exists (select 1 from @TableDetail where IsPackage = @Uno) begin --- si se esta anulando una orden de servicio que empaqueta, entonces creo los registros de los detalles que el empaqueto
				Set @IdRevenueControlDetailPackage = (select top 1 sodd.RevenueControlDetailId 
					from @TableDetail td 
					inner join Billing.ServiceOrderDetailDistribution sodd With(Nolock) on td.IdDetail = sodd.ServiceOrderDetailId)
					
				INSERT INTO Billing.ServiceOrderDetailDistribution(RevenueControlDetailId,ServiceOrderDetailId,Quantity,GrandTotalSalesPrice,GrandTotalDiscount,DistributionType,ThirdPartySalesPrice,ThirdPartyPercentage,ApplyRecoveryFee,RecoveryFeeType,SubTotalPatientSalesPrice,PatientPercentage,LastCaregroupId)
				select @IdRevenueControlDetailPackage, sod.Id, sod.InvoicedQuantity, sod.GrandTotalSalesPrice, sod.ThirdPartyDiscount * sod.InvoicedQuantity, 1, sod.GrandTotalSalesPrice, 100, 1, 1, 0,0, sod.CareGroupId
				from @TableDetail td inner join Billing.ServiceOrderDetail sod With(Nolock) on td.IdDetail = sod.PackageServiceOrderDetailId
			end

			--==============SE ACTUALIZA VALORES EN FOLIO============================

			DECLARE @RevenueControlDetailIds as TABLE (Id INT)
			DECLARE @revenueControlDetailId INT
			--se guardan los folios a recalcular
			INSERT INTO @RevenueControlDetailIds
			SELECT sodd.RevenueControlDetailId
			from Billing.ServiceOrderDetailDistribution sodd
			Inner Join @TableDetail td On sodd.ServiceOrderDetailId = td.IdDetail
			where td.IdDetail > 0
			GROUP by sodd.RevenueControlDetailId

			---- Actualizo el estado de la orden de servicio
			update Billing.ServiceOrder set Status = @Status, ModificationUser = @User, ModificationDate = [Common].[GETDATE](), AnnulmentUser = @User, AnnulmentDate = [Common].[GETDATE]() where Id = @IdServiceOrder

			-- se eliminan los registros del distribution
			delete sodd
			from Billing.ServiceOrderDetailDistribution sodd
			Inner Join @TableDetail td On sodd.ServiceOrderDetailId = td.IdDetail
			where td.IdDetail > 0

			-- se recalculan los folios con respecto a la nueva info
			DECLARE updateRevenue_cursor CURSOR FOR   
			select distinct Id from @RevenueControlDetailIds 
  
			OPEN updateRevenue_cursor  
			FETCH NEXT FROM updateRevenue_cursor   
			INTO @revenueControlDetailId
  
			WHILE @@FETCH_STATUS = 0  
			BEGIN 
				exec [Billing].[SP_UpdateRevenueControlDetailValues_Output] @revenueControlDetailId, @OperatingUnitId, NULL, NULL

				FETCH NEXT FROM updateRevenue_cursor   
				INTO @revenueControlDetailId
			END
			CLOSE updateRevenue_cursor;  
			DEALLOCATE updateRevenue_cursor;
			--======================================================================

			if @EntityName = 'AccountControl' begin
				--se generó por control de cuentas
				update dbo.HCORDLABO set GENSERVICEORDER = null where GENSERVICEORDER = @IdServiceOrder
				update dbo.HCORDIMAG set GENSERVICEORDER = null, IdServerOrderDetail = null where GENSERVICEORDER = @IdServiceOrder
				update dbo.HCORDPATO set GENSERVICEORDER = null where GENSERVICEORDER = @IdServiceOrder
				update dbo.HCORDPRON set GENSERVICEORDER = null where GENSERVICEORDER = @IdServiceOrder
				update dbo.HCPLAOTRPROCUPS set GENSERVICEORDER = null where GENSERVICEORDER = @IdServiceOrder
				update dbo.HCORDINTE set GENSERVICEORDER = null where GENSERVICEORDER = @IdServiceOrder
				update dbo.HCPROCTER set GENSERVICEORDER = null where GENSERVICEORDER = @IdServiceOrder
				update dbo.HCHOGASIN set GENSERVICEORDER = null where GENSERVICEORDER = @IdServiceOrder
				update dbo.HCCONOXIG set GENSERVICEORDER = null where GENSERVICEORDER = @IdServiceOrder
				update dbo.HCHISPACA set GENSERVICEORDER = null where GENSERVICEORDER = @IdServiceOrder
				update dbo.HCQXREALI set GENSERVICEORDER = null where GENSERVICEORDER = @IdServiceOrder
				--si se anula orden por procedimiento Bilateral
				--update dbo.HCQXREALI set GENSERVICEORDER2 = null where GENSERVICEORDER2 = @IdServiceOrder AND CODVIAABO = '02' 
			end

			--Se elimina lo que haya en esta tabla
			delete from @XmlAnnulmentRias

			--Se inserta en la tabla temporal los registros que manejen rias
			insert into @XmlAnnulmentRias(CupsCode, RIASCupsId)
			select ce.Code, td.RIASCupsId
			from @TableDetail td 
			inner join Contract.CUPSEntity ce With(Nolock) on ce.Id = td.CUPSEntityId
			where td.ApplyRIAS = 'True'

			--Si hay datos para reversar
			if EXISTS(select * from @XmlAnnulmentRias)
			begin
				--Se genera el xml para reversar
				set @generateXmlAnnulment = (select CupsCode, RIASCupsId from @XmlAnnulmentRias as RIASForPatient For Xml Auto, Elements)
						
				--Se ejecuta el proceso de reversión
				exec dbo.SP_RIASAnnulment @generateXmlAnnulment, @PatientCode, @User, @CodeMessageAnnulment output, @MessageAnnulment output

				--Si hay error ejecutando el sp
				if @CodeMessageAnnulment = '999'
				begin
					select	@CodeResult = '999', 
							@MessageResult = ISNULL(@MessageAnnulment, 'Error anulando RIAS'), 
							@StatusResult = 3,
							@Id = 0
					return
				end
			end

			--Se actualiza el estado en la autorización a un estado previo
			update tp 
				set tp.Status = tp.PreviousStatus
			from @TableDetail td
			join [Authorization].TraceabilityPaperworkEvents tpe on tpe.Id = td.TraceabilityPaperworkEventsId
			join [Authorization].TraceabilityPaperwork tp on tp.Id = tpe.TraceabilityPaperworkId

			--Se nulea el campo QuotationServiceOrderDetailId para liberar el importado de la cotización
			update Billing.ServiceOrderDetail set QuotationServiceOrderDetailId = null where ServiceOrderId = @IdServiceOrder

			---Establecemos null a los detalles de las imagenes generadas

			update h set h.IdServerOrderDetail = NULL  , h.GENSERVICEORDER = NULL
			From Billing.ServiceOrderDetail sod
			inner join @TableDetail as td on td.IdDetail = sod.Id
			Inner Join HCORDIMAG h On sod.Id = h.IdServerOrderDetail
			Where h.IdServerOrderDetail = sod.Id

			update a set a.IdServerOrderDetail = NULL , a.GENSERVICEORDER = NULL
			From Billing.ServiceOrderDetail sod
			inner join @TableDetail as td on td.IdDetail = sod.Id
			Inner Join AMBORDIMA a On sod.Id = a.IdServerOrderDetail
			Where a.IdServerOrderDetail = sod.Id 

			---Laboratorio
			update hc set hc.IdServerOrderDetail = NULL , hc.GENSERVICEORDER = NULL
			From Billing.ServiceOrderDetail sod
			inner join @TableDetail as td on td.IdDetail = sod.Id
			Inner Join HCORDLABO hc On sod.Id = hc.IdServerOrderDetail
			Where hc.IdServerOrderDetail = sod.Id 

			update hc set hc.IdServerOrderDetail = NULL , hc.GENSERVICEORDER = NULL
			From Billing.ServiceOrderDetail sod
			inner join @TableDetail as td on td.IdDetail = sod.Id
			Inner Join AMBORDLAB hc On sod.Id = hc.IdServerOrderDetail
			Where hc.IdServerOrderDetail = sod.Id 


		END
		ELSE
		BEGIN
			select	@AdmissionDate = IFECHAING, 
					@AdmissionStatus = IESTADOIN 
			from dbo.ADINGRESO With(Nolock) 
			where NUMINGRES = @AdmissionNumber

			SELECT	@AuthorizationNumberControl = AuthorizationNumberControl
			FROM Billing.SettingsBilling WITH (NOLOCK) 
			WHERE IdOperatingUnit = @OperatingUnitId

			SELECT	@SettingInventoryId = Id,
					@AssociateCostMainAccount = AssociateCostMainAccount
			FROM Inventory.SettingInventory WITH (NOLOCK) 
			WHERE OperatingUnitId = @OperatingUnitId

			select	@RevenueControlId = MIN(rc.Id), 
					@MaxFolio = COUNT(rcd.Id)
			from Billing.RevenueControl rc With(Nolock) 
			LEFT JOIN Billing.RevenueControlDetail rcd WITH(NOLOCK) ON rc.Id = rcd.RevenueControlId
			where AdmissionNumber = @AdmissionNumber

			/*********************************************  VALIDACIONES *********************************************/

			IF EXISTS (SELECT 1 FROM Billing.ServiceOrder so WITH (NOLOCK) WHERE so.Id = @IdServiceOrder AND so.Status <> 1)
			BEGIN
				select	@CodeResult = '999', 
						@MessageResult = 'La orden de servicio se encuentra en estado: ' + IIF(so.Status = 2, 'Confirmado', 'Anulado'), 
						@StatusResult = 3,
						@Id = 0
				FROM Billing.ServiceOrder so WITH (NOLOCK) 
				WHERE so.Id = @IdServiceOrder
				return
			END

			IF EXISTS(SELECT 1 FROM @TableDetail td WHERE td.InvoicedQuantity < 0  )
			BEGIN
				SET @errors= (	select CONCAT(ip.Code,'-',ip.Name) + ', ' from @TableDetail td
								JOIN Inventory.InventoryProduct ip WITH(NOLOCK) ON td.ProductId=ip.Id
								where td.InvoicedQuantity < 0 for XML PATH(''))
				SELECT	@CodeResult='999',
						@MessageResult = CONCAT('Se esta tratanto de Generar una orden de Servicio con Cantidades Negativas, para los siguientes productos: ',@errors), 
						@StatusResult = 3,
						@Id = 0
			END

			if cast(@OrderDate as date) < cast(@AdmissionDate as date) begin
				select	@CodeResult = '999', 
						@MessageResult = 'La fecha de la orden es menor a la fecha de la admisión', 
						@StatusResult = 3,
						@Id = 0
				return
			end

			if @AdmissionStatus = 'F' begin
				select	@CodeResult = '999', 
						@MessageResult = CONCAT('No se puede crear la orden de servicio porque el ingreso ', RTRIM(LTRIM(@AdmissionNumber)), ' esta facturado'), 
						@StatusResult = 3,
						@Id = 0
				return
			end	

			if Exists (
				select 1
				from @TableDetail td 
				inner join [Contract].CareGroup cg With(Nolock) on td.CareGroupId = cg.Id 
				inner join [Contract].[Contract] c With(Nolock) on c.Id = cg.ContractId 
				where c.[Status] = @Dos or c.[Status] = @Tres) 
			begin
				set @errors = (select c.Code + ', ' from @TableDetail td 
					inner join [Contract].CareGroup cg With(Nolock) on td.CareGroupId = cg.Id 
					inner join [Contract].[Contract] c With(Nolock) on c.Id = cg.ContractId 
					where c.[Status] = @Dos or c.[Status] = @Tres for XML PATH(''))
				select	@CodeResult = '999', 
						@MessageResult = CONCAT('Los siguientes Contratos estan suspendidos o terminados: ', @errors), 
						@StatusResult = 3,
						@Id = 0
				return
			end

			if (select count(*) from @TableDetail where ISPackage = @Uno) > 0 and (select count(*) from @TableDetail) > 1 begin
				select	@CodeResult = '999', 
						@MessageResult = 'Las ordenes de servicios que poseen items que empaquetan no pueden contener mas servicios', 
						@StatusResult = 3,
						@Id = 0
				return
			end

			IF NOT EXISTS (
				SELECT 1
				FROM Billing.BillingSequence s WITH (NOLOCK)
				JOIN Billing.BillingSequenceDetail sd WITH (NOLOCK) ON s.Id = sd.IdSequenseBillingC
				JOIN Common.Sequense cs on sd.IdSequense = cs.Id
				WHERE s.IdForm = @Tag755 AND s.IsManual = 0
					AND 
					(
						(s.Scope = 'O')
						OR
						(s.Scope = 'OU' AND sd.IdOperatingUnit = @OperatingUnitId)
					)
			) BEGIN
				select	@CodeResult = '999', 
						@MessageResult = 'No existe una secuencia automatica de ordenes de servicio', 
						@StatusResult = 3,
						@Id = 0
				return
			END

			--- Si hay Items para eliminar entonces los elimino
			if Exists (select 1 from @TableDetail where EntityState = @Deleted) begin
				--- Elimino todos los detalles que se estan con el estado Deleted
				if Exists (select 1 from @TableDetail where EntityState = @Deleted and IsPackage = @Uno) begin
					set @errors = (select ips.Code + ', ' 
						from @TableDetail td 
						inner join [Contract].IPSService ips With(Nolock) on ips.Id = td.IPSServiceId 
						where EntityState = @Deleted and IsPackage = @Uno for XML PATH(''))

					select	@CodeResult = '999', 
							@MessageResult = CONCAT('Los siguientes servicios no se pueden eliminar porque están empaquetados: ', @errors), 
							@StatusResult = 3,
							@Id = 0
					return
				end
				if Exists (select 1 from @TableDetail td 
					inner join Billing.ServiceOrderDetailDistribution sodd With(Nolock) on td.IdDetail = sodd.ServiceOrderDetailId 
					where EntityState = @Deleted and sodd.DistributionType <> @Uno and sodd.DistributionType <> @Cuatro) begin
					set @errors = (select ips.Code + ', ' from @TableDetail td 
						inner join [Contract].IPSService ips With(Nolock) on ips.Id = td.IPSServiceId 
						inner join Billing.ServiceOrderDetailDistribution sodd With(Nolock) on td.IdDetail = sodd.ServiceOrderDetailId 
						where EntityState = @Deleted and sodd.DistributionType <> @Uno and sodd.DistributionType <> @Cuatro for XML PATH(''))
					
					select	@CodeResult = '999', 
							@MessageResult = CONCAT('Los siguientes servicios no se pueden eliminar porque están distribuidos: ', @errors), 
							@StatusResult = 3,
							@Id = 0
					return
				end

				if Exists (
					
					select 1 from @TableDetail td 
					inner join Billing.ServiceOrderDetailDistribution sodd With(Nolock) on td.IdDetail = sodd.ServiceOrderDetailId 
					inner join Billing.RevenueControlDetail rcd With(Nolock) on rcd.Id = sodd.RevenueControlDetailId 
					where EntityState = @Deleted and (rcd.[Status] = @Dos or rcd.[Status] = @Tres))
					begin
					
					set @errors = (
						select  CONCAT('Folio: ' , rcd.FolioOrder , ' Servicio: ' , ips.Code , ' Fecha: ' , td.ServiceDate , CHAR(13) , CHAR(10) )
						from @TableDetail td 
						inner join [Contract].IPSService ips With(Nolock) on ips.Id = td.IPSServiceId 
						inner join Billing.ServiceOrderDetailDistribution sodd With(Nolock) on td.IdDetail = sodd.ServiceOrderDetailId 
						inner join Billing.RevenueControlDetail rcd With(Nolock) on rcd.Id = sodd.RevenueControlDetailId 
						where EntityState = @Deleted and (rcd.[Status] = @Dos or rcd.[Status] = @Tres) for XML PATH(''))
			
					select	@CodeResult = '999', 
							@MessageResult = CONCAT('Los siguientes servicios no se pueden eliminar porque sus folios están facturados o bloqueados: ', CHAR(13) + CHAR(10) + @errors), 
							@StatusResult = 3,
							@Id = 0
					return
				end
			end
			-- Valido que todos los productos tengan una cuenta de venta asociada
			IF EXISTS 
			(
				SELECT 1
				FROM @TableDetail pdd
				JOIN Inventory.InventoryProduct ip With(Nolock) ON ip.Id = pdd.ProductId
				LEFT JOIN Inventory.SettingInventoryFunctionalUnit sifu WITH (NOLOCK) ON @AssociateCostMainAccount = 1 AND sifu.SettingInventoryId = @SettingInventoryId AND pdd.PerformsFunctionalUnitId = sifu.FunctionalUnitId
				LEFT JOIN Inventory.ProductGroupFunctionalUnit pgfu WITH (NOLOCK) ON @AssociateCostMainAccount = 2 AND ip.ProductGroupId = pgfu.ProductGroupId AND pdd.PerformsFunctionalUnitId = pgfu.FunctionalUnitId
				WHERE ISNULL(sifu.Id, pgfu.Id) IS NULL AND ISNULL(pdd.EntityState, '') <> @Deleted
			) BEGIN
				select @errors = stuff((
					SELECT DISTINCT N'; La unidad funcional ' + fu.Code	+ ' - ' + fu.Name + ' no esta parametrizada en ' + 
						IIF(@AssociateCostMainAccount = 1, 'los parametros de inventarios', CONCAT('el grupo de producto ', pg.Code, ' - ', pg.Name))
					FROM @TableDetail pdd
					JOIN Payroll.FunctionalUnit fu WITH (NOLOCK) ON pdd.PerformsFunctionalUnitId = fu.Id
					JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON ip.Id = pdd.ProductId
					JOIN Inventory.ProductGroup pg WITH (NOLOCK) ON ip.ProductGroupId = pg.Id
					LEFT JOIN Inventory.SettingInventoryFunctionalUnit sifu WITH (NOLOCK) ON @AssociateCostMainAccount = 1 AND sifu.SettingInventoryId = @SettingInventoryId AND pdd.PerformsFunctionalUnitId = sifu.FunctionalUnitId
					LEFT JOIN Inventory.ProductGroupFunctionalUnit pgfu WITH (NOLOCK) ON @AssociateCostMainAccount = 2 AND ip.ProductGroupId = pgfu.ProductGroupId AND pdd.PerformsFunctionalUnitId = pgfu.FunctionalUnitId
					WHERE ISNULL(sifu.Id, pgfu.Id) IS NULL AND ISNULL(pdd.EntityState, '') <> @Deleted
				for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
							
				select	@CodeResult = '999', 
						@MessageResult = ISNULL(@errors, 'Unidades funcionales no parametrizadas'), 
						@StatusResult = 3,
						@Id = 0
				return
			end

			--- Valido que todos los detalles tengan asociado un Concepto de facturacion
			if Exists 
			(
				select 1 
				from @TableDetail td 
				left join [Contract].CUPSEntity ce with(nolock) on ce.Id = td.CUPSEntityId 
				left join Billing.BillingConcept bc with(nolock) on bc.Id = IIF(ISNULL(td.ApplyRIAS, '') = 'True', ce.RIASBillingConceptId, ce.BillingConceptId)
				where bc.Id is null AND td.CUPSEntityId IS NOT NULL AND td.CUPSEntityId <> 0 AND EntityState <> @Deleted
			) 
			begin
				set @errors = (select ce.Code + ', ' 
					from @TableDetail td 
					left join [Contract].CUPSEntity ce with(nolock) on ce.Id = td.CUPSEntityId 
					left join Billing.BillingConcept bc with(nolock) on bc.Id = IIF(ISNULL(td.ApplyRIAS, '') = 'True', ce.RIASBillingConceptId, ce.BillingConceptId)
					where bc.Id is null AND EntityState <> @Deleted for XML PATH(''))
				
				select	@CodeResult = '999', 
						@MessageResult = CONCAT('Los siguientes CUPS no tienen asociado un concepto de facturacion: ', @errors), 
						@StatusResult = 3,
						@Id = 0
				return
			end

			--- Valido que no haya un concepto de tipo facturacion basica
			if Exists (select 1 from @TableDetail td 
				inner join [Contract].CUPSEntity ce with(nolock) on ce.Id = td.CUPSEntityId 
				inner join Billing.BillingConcept bc with(nolock) on bc.Id = IIF(ISNULL(td.ApplyRIAS, '') = 'True', ce.RIASBillingConceptId, ce.BillingConceptId)
				where bc.ConceptType = @Uno AND td.CUPSEntityId IS NOT NULL AND td.CUPSEntityId <> 0 AND EntityState <> @Deleted) begin
				set @errors = (select ce.Code + ', ' 
					from @TableDetail td 
					inner join [Contract].CUPSEntity ce with(nolock) on ce.Id = td.CUPSEntityId 
					inner join Billing.BillingConcept bc with(nolock) on bc.Id = IIF(ISNULL(td.ApplyRIAS, '') = 'True', ce.RIASBillingConceptId, ce.BillingConceptId)
					where bc.ConceptType = @Uno AND EntityState <> @Deleted for XML PATH(''))
				
				select	@CodeResult = '999', 
						@MessageResult = CONCAT('Los siguientes CUPS tienen un concepto de facturacion tipo básica: ', @errors), 
						@StatusResult = 3,
						@Id = 0
				return
			end

			--- Valido que todos los detalles de los servicios Qx tengan asociado un Concepto de facturacion
			if Exists (select 1 from @TableSurgical ts 
				inner join [Contract].IPSService ips with(nolock) on ts.IPSServiceId = ips.Id 
				where ips.ServiceClass <> @Uno and ips.BillingConceptId is null) begin
				set @errors = (select ips.Code + ', ' from @TableSurgical ts 
					inner join Contract.IPSService ips with(nolock) on ts.IPSServiceId = ips.Id 
					where ips.BillingConceptId is null for XML PATH(''))
				
				select	@CodeResult = '999', 
						@MessageResult = CONCAT('Los siguientes Servicios IPS no tienen asociado un concepto de facturacion: ', @errors), 
						@StatusResult = 3,
						@Id = 0
				return
			end

			if Exists (select 1 from @TableDetail where IsPackage = @Uno and EntityState <> @Added and EntityState <> @Unchanged AND EntityState <> @Deleted) begin
				set @errors = (select ips.Code + ', ' 
					from @TableDetail td 
					inner join Contract.IPSService ips With(Nolock) on td.IPSServiceId = ips.Id 
					where td.IsPackage = @Uno and td.EntityState <> @Added and td.EntityState <> @Unchanged AND EntityState <> @Deleted for XML PATH('')
				)
				
				select	@CodeResult = '999', 
						@MessageResult = CONCAT('Los siguientes Servicios IPS no se pueden modificar ya que estan empaquetados: ', @errors), 
						@StatusResult = 3,
						@Id = 0
				return
			end
			if Exists (select 1 from @TableDetail td 
				inner join Billing.ServiceOrderDetailDistribution sodd With(Nolock) on td.IdDetail = sodd.ServiceOrderDetailId 
				where sodd.DistributionType <> @Uno and sodd.DistributionType <> @Cuatro and td.EntityState <> @Unchanged AND EntityState <> @Deleted) begin
				set @errors = (select ips.Code + ', ' 
					from @TableDetail td 
					inner join Billing.ServiceOrderDetailDistribution sodd With(Nolock) on td.IdDetail = sodd.ServiceOrderDetailId 
					inner join Contract.IPSService ips With(Nolock) on ips.Id = td.IPSServiceId 
					where sodd.DistributionType <> @Uno and sodd.DistributionType <> @Cuatro and td.EntityState <> @Unchanged AND EntityState <> @Deleted for XML PATH(''))

				select	@CodeResult = '999', 
						@MessageResult = CONCAT('Los siguientes Servicios IPS no se pueden modificar ya que estan distribuidos: ', @errors), 
						@StatusResult = 3,
						@Id = 0
				return
			end
			if Exists (select 1 from @TableDetail td 
				inner join Billing.ServiceOrderDetailDistribution sodd With(Nolock) on td.IdDetail = sodd.ServiceOrderDetailId 
				inner join Billing.RevenueControlDetail rcd With(Nolock) on rcd.Id = sodd.RevenueControlDetailId 
				where td.EntityState <> @Unchanged and rcd.[Status] > @Uno AND EntityState <> @Deleted) begin
				set @errors = (select 'Servicio IPS: ' + ips.Code + ' - Folio: ' + rcd.FolioOrder + CHAR(13) + CHAR(10) 
					from @TableDetail td 
					inner join Contract.IPSService ips With(Nolock) on ips.Id = td.IPSServiceId 
					inner join Billing.ServiceOrderDetailDistribution sodd With(Nolock) on td.IdDetail = sodd.ServiceOrderDetailId 
					inner join Billing.RevenueControlDetail rcd With(Nolock) on rcd.Id = sodd.RevenueControlDetailId 
					where td.EntityState <> @Unchanged and rcd.[Status] > @Uno AND EntityState <> @Deleted for XML PATH(''))
				
				select	@CodeResult = '999', 
						@MessageResult = CONCAT('Los siguientes Servicios IPS no se pueden modificar ya sus folios estan facturados o bloqueados: ', CHAR(13) + CHAR(10) + @errors), 
						@StatusResult = 3,
						@Id = 0
				return
			END
			--Validamos que no exista algun reconocimiento de ingreso sin reversar enlazado a algun folio vacio
			IF EXISTS 
			(
				SELECT 1
				from Billing.RevenueControlDetail  rcd With(Nolock)
				JOIN Billing.RevenueRecognitionDetail rrd With(Nolock) ON rcd.Id = rrd.RevenueControlDetailId	
				JOIN Billing.RevenueRecognition rr With(Nolock) ON rrd.RevenueRecognitionId = rr.Id
				LEFT JOIN Billing.ServiceOrderDetailDistribution sodd WITH (NOLOCK) ON rcd.Id = sodd.RevenueControlDetailId
				WHERE rcd.RevenueControlId = @RevenueControlId
					AND rr.State <> 2
					AND sodd.Id IS NULL
			)
			BEGIN
				select	@CodeResult = '999', 
						@MessageResult = 'Existen reconocimientos de Ingresos sin reversar', 
						@StatusResult = 3,
						@Id = 0
				RETURN
			END

			/**************************************** CONTROL DE AUTORIZACION ****************************************/
			
			SELECT @SubXml = CONVERT
			(
				XML, 
				(
					SELECT *
					FROM
					(
						SELECT	@OperatingUnitId OperatingUnitId,
								@PatientCode PatientCode,
								@AdmissionNumber AdmissionNumber,
								IdDetail Id,
								CareGroupId,
								ExcludeIds,
								HealthAdministratorId,
								IPSServiceId,
								AuthorizationNumber,
								IsDelete,
								CUPSEntityContractDescriptionId,
								IsServiceOrderDetailControlJustify,
								ServiceOrderDetailControlJustification
						FROM @TableDetail
						WHERE EntityState <> @Deleted AND IPSServiceId IS NOT NULL
					) ServiceOrderDetail
					For XML AUTO,TYPE, ELEMENTS
				)
			)

			EXEC [Billing].[SP_ValidateServiceOrderDetail_Output] @SubXml, @User, @Code_Output OUT, @Message_Output OUT

			IF @Code_Output <> 0
			BEGIN
				select	@CodeResult = '999', 
						@MessageResult = ISNULL(@Message_Output, 'Ocurrió un error al realizar la validación del control de autorización'), 
						@StatusResult = 3,
						@Id = 0
				RETURN
			END

			/***************************************************   ***************************************************/
			
			if Exists (select 1 from @TableDetail where EntityState = @Deleted) begin
				update sod set IsDelete = 1 
				From Billing.ServiceOrderDetail sod
				Inner Join @TableDetail td On sod.Id = td.IdDetail
				Where EntityState = @Deleted 

				delete sodd from Billing.ServiceOrderDetailDistribution sodd Inner Join @TableDetail td On sodd.ServiceOrderDetailId = td.IdDetail Where EntityState = @Deleted				
										
				DELETE sodc
				FROM Billing.ServiceOrderDetailControl sodc
				JOIN @TableDetail td On sodc.ServiceOrderDetailId = td.IdDetail
				WHERE EntityState = @Deleted 
					And Not Exists (
						select 1
						from Billing.InvoiceDetail idsub with(nolock) 
						inner join Billing.Invoice isub with(nolock) on idsub.InvoiceId = isub.Id and isub.[Status] = @Dos And isub.AdmissionNumber = @AdmissionNumber
						Where td.IdDetail = idsub.ServiceOrderDetailId
					)
					And Not Exists (
						select 1 from Billing.ServiceOrderDetail with(nolock) 
						where IncludeServiceOrderDetailId is not null And td.IdDetail = IncludeServiceOrderDetailId
					)
			
				--Se elimina lo que haya en esta tabla
				delete from @XmlAnnulmentRias

				--Se inserta en la tabla temporal los registros que sean para eliminar y que manejen rias
				insert into @XmlAnnulmentRias(CupsCode, RIASCupsId)
				select ce.Code, td.RIASCupsId
				from @TableDetail td 
				inner join Contract.CUPSEntity ce With(Nolock) on ce.Id = td.CUPSEntityId
				where td.EntityState = @Deleted and td.ApplyRIAS = 'True'
			
				--Si hay datos para reversar
				if EXISTS(select * from @XmlAnnulmentRias)
				begin
					--Se genera el xml para reversar
					set @generateXmlAnnulment = (select CupsCode, RIASCupsId from @XmlAnnulmentRias as RIASForPatient For Xml Auto, Elements)
						
					--Se ejecuta el proceso de reversión
					exec dbo.SP_RIASAnnulment @generateXmlAnnulment, @PatientCode, @User, @CodeMessageAnnulment output, @MessageAnnulment output

					--Si hay error ejecutando el sp
					if @CodeMessageAnnulment = '999'
					begin
						select	@CodeResult = '999', 
								@MessageResult = ISNULL(@MessageAnnulment, 'Error anulando RIAS'), 
								@StatusResult = 3,
								@Id = 0
						return
					end
				end


				---*****Cups de tipo IMAGENOLOGIA*****
				-- Eliminamos la relacion de los servicios detalles(ServiceOrderDetail) con las tablas relacionadas con el EHR
				-- Estableciendo el campo  IdServerOrderDetail en NULL
				
				---Tabla almacena las imagenes generadas
				update h set h.IdServerOrderDetail = NULL  , h.GENSERVICEORDER = NULL
				From Billing.ServiceOrderDetail sod
				inner join @TableDetail as td on td.IdDetail = sod.Id
				Inner Join HCORDIMAG h On sod.Id = h.IdServerOrderDetail
				Where h.IdServerOrderDetail = sod.Id  and td.EntityState = @Deleted 

				---Tabla almacena los servicios de las imagenes generadas
				update a set a.IdServerOrderDetail = NULL , a.GENSERVICEORDER = NULL
				From Billing.ServiceOrderDetail sod
				inner join @TableDetail as td on td.IdDetail = sod.Id
				Inner Join AMBORDIMA a On sod.Id = a.IdServerOrderDetail
				Where a.IdServerOrderDetail = sod.Id and td.EntityState = @Deleted

				---Tabla almacena laboratiorios
				update hc set hc.IdServerOrderDetail = NULL , hc.GENSERVICEORDER = NULL
				From Billing.ServiceOrderDetail sod
				inner join @TableDetail as td on td.IdDetail = sod.Id
				Inner Join HCORDLABO hc On sod.Id = hc.IdServerOrderDetail
				Where hc.IdServerOrderDetail = sod.Id and td.EntityState = @Deleted

				update am set am.IdServerOrderDetail = NULL , am.GENSERVICEORDER = NULL
				From Billing.ServiceOrderDetail sod
				inner join @TableDetail as td on td.IdDetail = sod.Id
				Inner Join AMBORDLAB am On sod.Id = am.IdServerOrderDetail
				Where am.IdServerOrderDetail = sod.Id and td.EntityState = @Deleted


				delete from @TableDetail where EntityState = @Deleted

				--- Eliminamos los reconocimientos de ingresos relacionados con los folios vacios
				DELETE rrd
				from Billing.RevenueControlDetail  rcd
				JOIN Billing.RevenueRecognitionDetail rrd ON rcd.Id = rrd.RevenueControlDetailId
				LEFT JOIN Billing.ServiceOrderDetailDistribution sodd ON rcd.Id = sodd.RevenueControlDetailId
				WHERE rcd.RevenueControlId = @RevenueControlId AND sodd.Id IS NULL
			
				--- Validar que folios quedaron vacios para eliminarlos
				delete rcd 
				from Billing.RevenueControlDetail rcd
				LEFT JOIN Billing.ServiceOrderDetailDistribution sodd ON rcd.Id = sodd.RevenueControlDetailId
				where Status = 1 AND RevenueControlId = @RevenueControlId AND sodd.Id IS NULL
			end

			--- Actualizo la cuenta contable de ingreso
			update td
				Set IncomeMainAccountId = Billing.fnGetIncomeMainAccount(td.RecordType, cg.CareGroupType, bc.Id, bc.AccountingType, bc.EntityIncomeAccountId, bc.IndividualIncomeAccountId, ISNULL(sifu.SalesAccountId, pgfu.SalesAccountId), f.UnitType),
					BillingConceptId = IIF(ISNULL(td.ApplyRIAS, '') = 'True', ce.RIASBillingConceptId, iif(isnull(cecd.BillingConceptId, 0) > 0, cecd.BillingConceptId, td.BillingConceptId)),
					CareGroupType = cg.CareGroupType,
					LiquidationType = cg.LiquidationType,
					ContractEntityId = c.ContractEntityId
			from @TableDetail td 
			inner join [Contract].CareGroup cg with(nolock) on td.CareGroupId = cg.Id
			inner join Payroll.FunctionalUnit f with(nolock) on f.Id = td.PerformsFunctionalUnitId
			left join Contract.[Contract] c With(Nolock) on c.Id = cg.ContractId 
			left join [Contract].CUPSEntity ce with(nolock) on ce.Id = td.CUPSEntityId
			left join Contract.CUPSEntityContractDescriptions cecd with(nolock) on cecd.Id = td.CUPSEntityContractDescriptionId
			left join Billing.BillingConcept bc with(nolock) on bc.Id = IIF(ISNULL(td.ApplyRIAS, '') = 'True', ce.RIASBillingConceptId, iif(isnull(cecd.BillingConceptId, 0) > 0, cecd.BillingConceptId, ce.BillingConceptId))
			left join Inventory.InventoryProduct ipr with(nolock) on ipr.Id = td.ProductId
			LEFT JOIN Inventory.SettingInventoryFunctionalUnit sifu WITH (NOLOCK) ON @AssociateCostMainAccount = 1 AND sifu.SettingInventoryId = @SettingInventoryId AND td.PerformsFunctionalUnitId = sifu.FunctionalUnitId
			LEFT JOIN Inventory.ProductGroupFunctionalUnit pgfu WITH (NOLOCK) ON @AssociateCostMainAccount = 2 AND ipr.ProductGroupId = pgfu.ProductGroupId AND td.PerformsFunctionalUnitId = pgfu.FunctionalUnitId			

			declare @conceptCode varchar(500) = ''
			select @conceptCode = concat(@conceptCode, bc.Code, ', ')
			from @TableSurgical ts
			inner join @TableDetail td on td.IdRow = ts.ServiceOrderDetailIdRow
			inner join [Contract].CareGroup cg with(nolock) on td.CareGroupId = cg.Id
			inner join Payroll.FunctionalUnit f with(nolock) on f.Id = td.PerformsFunctionalUnitId
			inner join [Contract].IPSService ips with(nolock) on ips.Id = ts.IPSServiceId
			inner join Billing.BillingConcept bc with(nolock) on bc.Id = ips.BillingConceptId
			where Billing.fnGetIncomeMainAccount(td.RecordType, cg.CareGroupType, bc.Id, bc.AccountingType, bc.EntityIncomeAccountId, bc.IndividualIncomeAccountId, null, f.UnitType) is null

			if isnull(@conceptCode, '') <> ''  BEGIN
				select	@CodeResult = '999', 
					@MessageResult = 'Falta configurar las cuentas de ingreso entidad para los conceptos de facturación: ' + @conceptCode, 
					@StatusResult = 3,
					@Id = 0
				return
			END

			update ts set IncomeMainAccountId = Billing.fnGetIncomeMainAccount(td.RecordType, cg.CareGroupType, bc.Id, bc.AccountingType, bc.EntityIncomeAccountId, bc.IndividualIncomeAccountId, null, f.UnitType)
			from @TableSurgical ts
			inner join @TableDetail td on td.IdRow = ts.ServiceOrderDetailIdRow
			inner join [Contract].CareGroup cg with(nolock) on td.CareGroupId = cg.Id
			inner join Payroll.FunctionalUnit f with(nolock) on f.Id = td.PerformsFunctionalUnitId
			inner join [Contract].IPSService ips with(nolock) on ips.Id = ts.IPSServiceId
			inner join Billing.BillingConcept bc with(nolock) on bc.Id = ips.BillingConceptId

			if @IdServiceOrder = 0 begin
				--genero la secuencia para la orden de servicio
				declare @idSequenceDetail int
				declare @pattern varchar(300)
				declare @NextS bigint
				declare @Scope varchar(5)
				declare @IdSequence int
				select @IdSequence = Id, @Scope = Scope from Billing.BillingSequence With(Nolock) where IdForm = @Tag755
				if @Scope = 'OU' begin
					select @pattern = cs.Pattern, @idSequenceDetail = psd.Id  
					from Billing.BillingSequenceDetail psd  With(Nolock)
					inner join Common.Sequense cs With(Nolock) on cs.Id = psd.IdSequense 
					where psd.IdSequenseBillingC = @IdSequence and IdOperatingUnit = @OperatingUnitId
				end
				else begin
					select @pattern = cs.Pattern, @idSequenceDetail = psd.Id  
					from Billing.BillingSequenceDetail psd With(Nolock) 
					inner join Common.Sequense cs With(Nolock) on cs.Id = psd.IdSequense 
					where psd.IdSequenseBillingC = @IdSequence
				end
				update Billing.BillingSequenceDetail set @NextS = [Next] += 1 where Id = @idSequenceDetail
				select @CodeServiceOrder = dbo.GetSequence('',@pattern,(@NextS - 1))

				-- Le agrego el Entity Name si es creado desde el mismo form de ordenes de servicio
				if isnull(@EntityName,'') = '' begin
					set @EntityName = 'ServiceOrder'
					set @EntityCode = @CodeServiceOrder
				end

				-- ******************* Inserto la cabecera de la orden *************
				
				INSERT INTO [Billing].[ServiceOrder] ([Code],[AdmissionNumber],[PatientCode],[OrderDate],[AffectInventory],[EntityCode],[EntityId],[EntityName],[OperatingUnitId],[Status],[CreationUser],[CreationDate])
				values (@CodeServiceOrder, @AdmissionNumber, @PatientCode, @OrderDate, @AffectInventory, @EntityCode, @EntityId, @EntityName, @OperatingUnitId, @Status, @User, [Common].[GETDATE]())
				set @IdServiceOrder = SCOPE_IDENTITY()
				update @TableDetail set ServiceOrderId = @IdServiceOrder
			end
			else begin
				---- Actualizo el estado de la orden de servicio
				update Billing.ServiceOrder set Status = @Status, ModificationUser = @User, ModificationDate = [Common].[GETDATE]() where Id = @IdServiceOrder
			end
			--Tabla para almacenar los errores de actividad economica
			DECLARE @ErrorsTable TABLE ( --Hubo cambios aqui
				CodeResult VARCHAR(10),
				MessageResult VARCHAR(MAX),
				StatusResult INT,
				Id INT
			);

			DECLARE @EconomicActivityId int 
			IF (@EntityName = 'PharmaceuticalDispensing' And @TransacationEconommicActivity = 1 )
			BEGIN 


			-- Declaración del cursor
				DECLARE cursor_TableDetail CURSOR
				FOR 
				-- Consulta para definir el conjunto de datos
				SELECT ProductId, EconomicActivityId
				FROM @TableDetail
				

				-- Declaración de variables para almacenar los valores del cursor
				DECLARE @ProductIdC Int, @EconomimicActivityIdC Int;

				-- Abrir el cursor
				OPEN cursor_TableDetail;

				-- Obtener el primer registro del cursor
				FETCH NEXT FROM cursor_TableDetail INTO @ProductIdC, @EconomimicActivityIdC;

				-- Bucle para procesar cada registro
				WHILE @@FETCH_STATUS = 0
				BEGIN
					IF ( @EconomimicActivityIdC IS NULL ) --validamos que exista actividad economica 
						BEGIN
							
							INSERT INTO @ErrorsTable (CodeResult, MessageResult, StatusResult, Id)
							SELECT 
								'999', 
								CONCAT('No se tiene definida una Actividad Económica Generadora de Ingreso en el Grupo ', pg.Name, ' asociado al Producto ', p.name),
								3, 
								0
								FROM @TableDetail td
								JOIN Inventory.InventoryProduct p on p.Id = td.ProductId
								JOIN Inventory.ProductGroup pg on pg.Id = p.ProductGroupId
								WHERE p.id = @ProductIdC;								

					END

					-- Obtener el siguiente registro
					FETCH NEXT FROM cursor_TableDetail INTO @ProductIdC, @EconomimicActivityIdC;
				END;

				-- Cerrar el cursor
				CLOSE cursor_TableDetail;

				-- Liberar el cursor de la memoria
				DEALLOCATE cursor_TableDetail;
			END;
			
			IF EXISTS (SELECT 1 FROM @ErrorsTable)
			BEGIN
				
				DECLARE @AllErrors NVARCHAR(MAX) = '';
				SELECT @AllErrors = STRING_AGG(MessageResult, CHAR(10)) 
				FROM @ErrorsTable;

				SELECT	@CodeResult = '999', 
						@MessageResult = @AllErrors, 
						@StatusResult = 3,
						@Id = 0

				RETURN;
			END
	
			--Actualizamos el campo de la actividad economica en los detalles --hubo cambios
			IF((@EntityName = 'ServiceOrder' Or @EntityName = 'ControlOutPatientServices' Or @EntityName = 'AccountControl')  AND @TransacationEconommicActivity = 1)-- cambios aqui 
			BEGIN 
			DECLARE @ProductIdCS Int, @EconomimicActivityIdCS Int, @BillingConceptIdCS INT;

			DECLARE cursor_TableDetailS CURSOR
				FOR 
				-- Consulta para definir el conjunto de datos
				SELECT td.ProductId, bc.EconomicActivityId,td.BillingConceptId
				FROM @TableDetail td 
				join Billing.BillingConcept BC on BC.Id = td.BillingConceptId
				

				OPEN cursor_TableDetailS;


				FETCH NEXT FROM cursor_TableDetailS INTO @ProductIdCS, @EconomimicActivityIdCS,@BillingConceptIdCS;

				WHILE @@FETCH_STATUS = 0
				BEGIN
				
					IF @ProductIdCS IS NULL
					BEGIN
					UPDATE @TableDetail SET EconomicActivityId = @EconomimicActivityIdCS where BillingConceptId = @BillingConceptIdCS
					
					
					END

					FETCH NEXT FROM cursor_TableDetailS INTO @ProductIdCS, @EconomimicActivityIdCS,@BillingConceptIdCS;
				END;


				CLOSE cursor_TableDetailS;

				DEALLOCATE cursor_TableDetailS;
			END--hasta aqui
			--********************** CRUD los detalles ****************************
			-- No elimino ya que se hace en la parte de arriba
			UPDATE Billing.ServiceOrderDetail 
			   SET	ServiceOrderId = td.ServiceOrderId,
					CareGroupId = td.CareGroupId,
					HealthAdministratorId = td.HealthAdministratorId,
					ThirdPartyId = td.ThirdPartyId,
					ServiceType = td.ServiceType,
					RecordType = td.RecordType,
					CUPSEntityId = td.CUPSEntityId,
					IPSServiceId = td.IPSServiceId,
					HospitalStayId = td.HospitalStayId,
					HospitalStayDetailId = td.HospitalStayDetailId,
					ControlExternalConsultation = td.ControlExternalConsultation,
					ControlExternalConsultationCode = td.ControlExternalConsultationCode,
					CUPSAssociateService = td.CUPSAssociateService,
					CodeAssociateService = td.CodeAssociateService,
					IsPackage = td.IsPackage,
					Packaging = td.Packaging,
					PackageServiceOrderDetailId = td.PackageServiceOrderDetailId,
					LiquidationType = td.LiquidationType,
					Presentation = td.Presentation,
					ProductId = td.ProductId,
					InvoicedQuantity = td.InvoicedQuantity,
					SupplyQuantity = td.SupplyQuantity,
					DevolutionQuantity = td.DevolutionQuantity,
					RateManualSalePrice = td.RateManualSalePrice,
					CostValue = td.CostValue,
					ServiceDate = td.ServiceDate,
					AuthorizationNumber = td.AuthorizationNumber,
					PerformsFunctionalUnitId = td.PerformsFunctionalUnitId,
					PerformsHealthProfessionalCode = td.PerformsHealthProfessionalCode,
					PerformsProfessionalSpecialty = td.PerformsProfessionalSpecialty,
					PerformsHealthProfessionalThirdPartyId = td.PerformsHealthProfessionalThirdPartyId,
					BillingConceptId = td.BillingConceptId,
					CostCenterId = td.CostCenterId,
					SettlementType = td.SettlementType,
					IncludeServiceOrderDetailId = td.IncludeServiceOrderDetailId,
					RecoveryRatio = td.RecoveryRatio,
					RateManualId = td.RateManualId,
					RateManualType = td.RateManualType,
					RateManualDetailId = td.RateManualDetailId,
					DefinitionRateDetailId = td.DefinitionRateDetailId,
					DefinitionRateDetailConditionId = td.DefinitionRateDetailConditionId,
					SubTotalSalesPrice = td.SubTotalSalesPrice,
					ThirdPartyDiscount = td.ThirdPartyDiscount,
					ThirdPartyDiscountPercentage = td.ThirdPartyDiscountPercentage,
					TotalSalesPrice = td.TotalSalesPrice,
					GrandTotalSalesPrice = td.GrandTotalSalesPrice,
					SurchargeApply = td.SurchargeApply,
					SurgicalInterventionType = td.SurgicalInterventionType,
					SurgeryNumber = td.SurgeryNumber,
					IsFirstEvent = td.IsFirstEvent,
					IsAnnulled = td.IsAnnulled,
					IsDelete = td.IsDelete,
					IncomeMainAccountId = td.IncomeMainAccountId,
					ApplyRIAS = case when td.ApplyRIAS = '-' then null else cast(td.ApplyRIAS as bit) end,
					RIASCupsId = case when td.RIASCupsId = 0 then null else td.RIASCupsId end,
					CUPSEntityContractDescriptionId = case when td.CUPSEntityContractDescriptionId = 0 then null else td.CUPSEntityContractDescriptionId end,
					QuotationServiceOrderDetailId = case when td.QuotationServiceOrderDetailId = 0 then null else td.QuotationServiceOrderDetailId end,
					TraceabilityPaperworkEventsId = case when td.TraceabilityPaperworkEventsId = 0 then null else td.TraceabilityPaperworkEventsId end,
					FinalProductCost = td.FinalProductCost,
					ContractPackageId = td.ContractPackageId,
					GrossValue = td.GrossValue,
					TaxValue =td.TaxValue,
					EconomicActivityId = td.EconomicActivityId
			from @TableDetail td 
			inner join Billing.ServiceOrderDetail sod with(nolock) on sod.Id = td.IdDetail and td.EntityState = @Modified
			
			
			
			--Se actualiza el estado a Ejecutado(9) de la autorización
			update tp 
				set tp.PreviousStatus = tp.Status, 
					tp.Status = 9
			from @TableDetail td
			inner join [Authorization].TraceabilityPaperworkEvents tpe on tpe.Id = td.TraceabilityPaperworkEventsId
			inner join [Authorization].TraceabilityPaperwork tp on tp.Id = tpe.TraceabilityPaperworkId

			---- inserto los detail
			declare @IdRowTmp int

			Declare @Rows Int, @RowId Int
			Set @Rows = 1
			Set @RowId = 1

			While @Rows > 0
			Begin
			   
				Select Top 1 @RowId = RowId, @IdRowTmp = IdRow From @TableDetail Where RowId >= @RowId And IdDetail = 0 Order By RowId
				Set @Rows = @@ROWCOUNT
				If @Rows = 0 
					Break

				INSERT INTO Billing.ServiceOrderDetail(ServiceOrderId,CareGroupId,HealthAdministratorId,ThirdPartyId,ServiceType,RecordType,CUPSEntityId,IPSServiceId,HospitalStayId,HospitalStayDetailId,ControlExternalConsultation,ControlExternalConsultationCode,CUPSAssociateService,CodeAssociateService,IsPackage,Packaging,PackageServiceOrderDetailId,LiquidationType,Presentation,ProductId,InvoicedQuantity,SupplyQuantity,DevolutionQuantity,RateManualSalePrice,CostValue,ServiceDate,AuthorizationNumber,PerformsFunctionalUnitId,PerformsHealthProfessionalCode,PerformsProfessionalSpecialty,PerformsHealthProfessionalThirdPartyId,BillingConceptId,CostCenterId,SettlementType,IncludeServiceOrderDetailId,RecoveryRatio,RateManualId,RateManualType,RateManualDetailId,DefinitionRateDetailId,DefinitionRateDetailConditionId,SubTotalSalesPrice,ThirdPartyDiscount,ThirdPartyDiscountPercentage,TotalSalesPrice,GrandTotalSalesPrice,SurchargeApply,SurgicalInterventionType,SurgeryNumber,IsFirstEvent,IsAnnulled,IsDelete,IncomeMainAccountId, ApplyRIAS, RIASCupsId, CUPSEntityContractDescriptionId, QuotationServiceOrderDetailId, TraceabilityPaperworkEventsId, FinalProductCost, ContractPackageId,GrossValue,TaxValue, IvaId,EconomicActivityId)--,EconomicActivityId
				select ServiceOrderId,CareGroupId,HealthAdministratorId,ThirdPartyId,ServiceType,RecordType,CUPSEntityId,IPSServiceId,HospitalStayId,HospitalStayDetailId,ControlExternalConsultation,ControlExternalConsultationCode,CUPSAssociateService,CodeAssociateService,IsPackage,Packaging,PackageServiceOrderDetailId,LiquidationType,Presentation,ProductId,InvoicedQuantity,SupplyQuantity,DevolutionQuantity,RateManualSalePrice,CostValue,ServiceDate,AuthorizationNumber,PerformsFunctionalUnitId,PerformsHealthProfessionalCode,PerformsProfessionalSpecialty,PerformsHealthProfessionalThirdPartyId,BillingConceptId,CostCenterId,SettlementType,IncludeServiceOrderDetailId,RecoveryRatio,RateManualId,RateManualType,RateManualDetailId,DefinitionRateDetailId,DefinitionRateDetailConditionId,SubTotalSalesPrice,ThirdPartyDiscount,ThirdPartyDiscountPercentage,TotalSalesPrice,GrandTotalSalesPrice,SurchargeApply,SurgicalInterventionType,SurgeryNumber,IsFirstEvent,IsAnnulled,IsDelete,IncomeMainAccountId, case when ApplyRIAS = '-' then null else cast(ApplyRIAS as bit) end, case when RIASCupsId = 0 then null else RIASCupsId end, case when CUPSEntityContractDescriptionId = 0 then null else CUPSEntityContractDescriptionId end, case when QuotationServiceOrderDetailId = 0 then null else QuotationServiceOrderDetailId end, case when TraceabilityPaperworkEventsId = 0 then null else TraceabilityPaperworkEventsId end ,FinalProductCost,ContractPackageId,GrossValue,TaxValue, IvaId,EconomicActivityId
				from @TableDetail 
				where IdRow = @IdRowTmp

				update @TableDetail set IdDetail = SCOPE_IDENTITY() where IdRow = @IdRowTmp

				Set @RowId += 1
			END
	
			--Tabla para almacenar la info para el proceso RIAS
			declare @XmlProcessRias table(CupsCode varchar(20), RIASCupsId int, Quantity int, ServiceOrderDetailId int, ValueCups decimal(20,2), HealthProfessionalCode varchar(20), 
			RealizationDate datetime, RealizedQuantity int)		
		
			--Se insertan los datos a la tabla de xml para el proceso de rias
			insert into @XmlProcessRias(CupsCode, RIASCupsId, Quantity, ServiceOrderDetailId, ValueCups, HealthProfessionalCode, RealizationDate, RealizedQuantity)
			select ce.Code, td.RIASCupsId, td.InvoicedQuantity, td.IdDetail, td.GrandTotalSalesPrice, td.PerformsHealthProfessionalCode, td.ServiceDate, RealizedQuantity
			from @TableDetail td 
			inner join Contract.CUPSEntity ce with (nolock) on ce.Id = td.CUPSEntityId
			where td.EntityState = @Added and td.ApplyRIAS = 'True'
		
			--Si hay datos en la tabla xml se consume el sp del proceso rias
			if EXISTS(select * from @XmlProcessRias)
			begin

				--Variable xml para enviar al sp
				declare @GenerateXml xml = (select CupsCode, RIASCupsId, Quantity, ServiceOrderDetailId, ValueCups, HealthProfessionalCode, RealizationDate, RealizedQuantity 
											from @XmlProcessRias as RIASForPatient For Xml Auto, Elements)

				--Variables en donde se almacena el resultado del proceso RIAS
				declare @CodeMessageProcessRias varchar(20), @MessageProcessRias varchar(max)

				--Se ejecuta el sp de inscripción
				exec dbo.SP_RIASProcess @GenerateXml, @PatientCode, @AdmissionNumber, @User, @CodeMessageProcessRias output, @MessageProcessRias output

				--Si hay error ejecutando el sp
				if @CodeMessageProcessRias = '999'
				begin
					select	@CodeResult = '999', 
							@MessageResult = ISNULL(@MessageProcessRias, 'Error generando RIAS'), 
							@StatusResult = 3,
							@Id = 0
					return
				end
			end

			update sod set IncludeServiceOrderDetailId = (select IdDetail from @TableDetail where IdRow = td.IncludeServiceOrderDetailIdRow)
			from @TableDetail td 
			inner join Billing.ServiceOrderDetail sod on td.IdDetail = sod.Id 
			where td.IncludeServiceOrderDetailIdRow is not null

			--****************** CRUD los surgical *******************
			update ts set ServiceOrderDetailId = td.IdDetail
			from @TableDetail td 
			inner join @TableSurgical ts on td.IdRow = ts.ServiceOrderDetailIdRow

			delete sods
			from [Billing].[ServiceOrderDetailSurgical] sods
			Inner Join @TableSurgical ts On sods.Id = ts.Id
			where ts.EntityState = @Deleted

			--Eliminamos los surgical que no se envían en @TableSurgical porque esto está duplicado los registros
			DELETE sods
			FROM Billing.ServiceOrderDetailSurgical sods
			JOIN @TableSurgical ts ON sods.ServiceOrderDetailId = ts.ServiceOrderDetailId
			WHERE sods.IPSServiceId NOT IN (SELECT IPSServiceId FROM @TableSurgical WHERE Id > 0)

			delete from @TableSurgical where EntityState = @Deleted

			
			/**************** CENTRO DE COSTO POR DETALLE QX ***************************/
			DECLARE @AccountingForSurgical TINYINT

			SET @AccountingForSurgical =(SELECT TOP 1 sb.AccountingForSurgical FROM Billing.SettingsBilling sb WITH(NOLOCK) where sb.IdOperatingUnit = @OperatingUnitId)

			IF @AccountingForSurgical =2 BEGIN

					DECLARE @CenterAttentionCode CHAR(10)					
					SELECT TOP 1 @CenterAttentionCode = ing.CODCENATE
						FROM dbo.ADINGRESO ing  WITH(NOLOCK)
						WHERE ing.NUMINGRES = @AdmissionNumber
				
					UPDATE	 ts SET
						TS.CostCenterId = (CASE bc.ObtainCostCenter
											WHEN 1 THEN (SELECT TOP 1 fu.CostCenterId FROM Payroll.FunctionalUnit fu WHERE fu.Id=sod.PerformsFunctionalUnitId)
											WHEN 2 THEN bc.CostCenterId
											WHEN 3 THEN (SELECT top 1 bccc.CostCenterId
															FROM Billing.BillingConceptCostCenter bccc WITH(NOLOCK)
															JOIN Payroll.BranchOffice bo  WITH(NOLOCK) ON bccc.BranchOfficeId = bo.Id
															WHERE bccc.BillingConceptId = bc.Id
																AND bo.Code = ISNULL(@CenterAttentionCode, '') AND bccc.FunctionalUnitId = ISNULL(sod.PerformsFunctionalUnitId, 0) )
											ELSE sod.CostCenterId
											END)
					FROM @TableSurgical ts
					JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) ON ts.ServiceOrderDetailId= sod.Id
					JOIN [Contract].IPSService i WITH(NOLOCK) ON ts.IPSServiceId = i.Id
					JOIN Billing.BillingConcept bc WITH(NOLOCK) ON bc.Id = i.BillingConceptId
				
			END	
			ELSE BEGIN

					UPDATE TS SET TS.CostCenterId = sod.CostCenterId
					FROM @TableSurgical ts
					JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) ON ts.ServiceOrderDetailId= sod.Id		

			END
			
		/****************************************************************************************************/
		IF ((@EntityName = 'ServiceOrder' Or @EntityName = 'ControlOutPatientServices') AND @TransacationEconommicActivity = 1 )--hubo cambios
			BEGIN 			

			DECLARE cursor_TableDetailSurgical CURSOR
				FOR 
				-- Consulta para definir el conjunto de datos
				SELECT BillingConceptId, EconomicActivityId
				FROM @TableSurgical

				-- Declaración de variables para almacenar los valores del cursor
				DECLARE @BillingConceptIdC Int, @EconomimicActivityIdCSu Int;

				-- Abrir el cursor
				OPEN cursor_TableDetailSurgical;

				-- Obtener el primer registro del cursor
				FETCH NEXT FROM cursor_TableDetailSurgical INTO @ProductIdC, @EconomimicActivityIdCSu;

				-- Bucle para procesar cada registro
				WHILE @@FETCH_STATUS = 0
				BEGIN
				IF ( @EconomimicActivityIdCSu IS NULL ) --validamos que exista actividad economica 
				BEGIN

				INSERT INTO @ErrorsTable (CodeResult, MessageResult, StatusResult, Id)
				SELECT 
					'999', 
					CONCAT('No se tiene definida una Actividad Económica Generadora de Ingreso en el Concepto de Facturación ', BC.Name, ' asociado al servicio ', IPSS.Name),
					3, 
					0
				FROM @TableSurgical TS
				JOIN Billing.BillingConcept BC ON BC.ID = TS.BillingConceptId
				JOIN Contract.IPSService IPSS ON IPSS.Id = TS.IPSServiceId
				WHERE TS.BillingConceptId = IPSS.BillingConceptId;
				
				END
					-- Obtener el siguiente registro
					FETCH NEXT FROM cursor_TableDetailSurgical  INTO @BillingConceptIdC, @EconomimicActivityIdCSu;
				END;
				-- Cerrar el cursor
				CLOSE cursor_TableDetailSurgical;
				-- Liberar el cursor de la memoria
				DEALLOCATE cursor_TableDetailSurgical;

		END	
		
		
			IF EXISTS (SELECT 1 FROM @ErrorsTable)
			BEGIN
				
				DECLARE @AllErrorsS NVARCHAR(MAX) = '';
				SELECT @AllErrorsS = STRING_AGG(MessageResult, CHAR(10)) 
				FROM @ErrorsTable;

				SELECT	@CodeResult = '999', 
						@MessageResult = @AllErrors, 
						@StatusResult = 3,
						@Id = 0

				RETURN;
			END--hasta aqui
						

			UPDATE Billing.ServiceOrderDetailSurgical
			   SET [ServiceOrderDetailId] = ts.ServiceOrderDetailId,
				  [IPSServiceId] = ts.IPSServiceId,
				  [InvoicedQuantity] = ts.InvoicedQuantity,
				  [LiquidationPercentage] = ts.LiquidationPercentage,
				  [RateManualSalePrice] = ts.RateManualSalePrice,
				  [TotalSalesPrice] = ts.TotalSalesPrice,
				  [PerformsHealthProfessionalCode] = ts.PerformsHealthProfessionalCode,
				  [PerformsHealthProfessionalThirdPartyId] = ts.PerformsHealthProfessionalThirdPartyId,
				  [CostValue] = ts.CostValue,
				  [BillingConceptId] = ts.BillingConceptId,
				  [CostCenterId] = ts.CostCenterId,
				  [RateManualDetailSurgicalId] = ts.RateManualDetailSurgicalId,
				  [SurchargeApply] = ts.SurchargeApply,
				  [OnlyMedicalFees] = ts.OnlyMedicalFees,
				  [IncomeMainAccountId] = ts.IncomeMainAccountId,
				  [EconomicActivityId] = ts.EconomicActivityId
				 				  
			 from @TableSurgical ts 
			 inner join Billing.ServiceOrderDetailSurgical sods 
			 on ts.ServiceOrderDetailId = sods.ServiceOrderDetailId and ts.Id=sods.Id

			INSERT INTO [Billing].[ServiceOrderDetailSurgical](ServiceOrderDetailId,IPSServiceId,InvoicedQuantity,LiquidationPercentage,RateManualSalePrice,TotalSalesPrice,PerformsHealthProfessionalCode,PerformsHealthProfessionalThirdPartyId,CostValue,BillingConceptId,CostCenterId,RateManualDetailSurgicalId,SurchargeApply,OnlyMedicalFees,IncomeMainAccountId,EconomicActivityId)
			select ServiceOrderDetailId,IPSServiceId,InvoicedQuantity,LiquidationPercentage,RateManualSalePrice,TotalSalesPrice,PerformsHealthProfessionalCode,PerformsHealthProfessionalThirdPartyId,CostValue,BillingConceptId,CostCenterId,RateManualDetailSurgicalId,SurchargeApply,OnlyMedicalFees,IncomeMainAccountId,EconomicActivityId 
			from @TableSurgical where Id = 0


						
			/****** Verifico si se esta empaquetando para leer los detalles  *******/
			if Exists (select 1 from @TableDetail where IsPackage = @Uno and EntityState = @Added) begin
				--Obtengo el folio en donde estan los items que se van a empaquetar, esto para dejar el paquete en el mismo folio
				set @IdRevenueControlDetailPackage = (
					select top 1 sodd.RevenueControlDetailId 
					from Billing.ServiceOrderDetail sod With(Nolock)
					inner join Billing.ServiceOrderDetailDistribution sodd With(Nolock) on sod.Id = sodd.ServiceOrderDetailId 
					where sod.Id in (select t.x.value('ItemPackageId[1]','int') from @ServiceOrderXml.nodes('/ServiceOrder/ServiceOrderDetail/ServiceOrderDetailPackage') t(x))
				)
				declare @IdServiceOrderPackage int = (select IdDetail from @TableDetail where IsPackage = 1)
				update Billing.ServiceOrderDetail set Packaging = 1, PackageServiceOrderDetailId = @IdServiceOrderPackage
				where Id in (select t.x.value('ItemPackageId[1]','int') from @ServiceOrderXml.nodes('/ServiceOrder/ServiceOrderDetail/ServiceOrderDetailPackage') t(x))
				delete from Billing.ServiceOrderDetailDistribution where ServiceOrderDetailId in (select t.x.value('ItemPackageId[1]','int') from @ServiceOrderXml.nodes('/ServiceOrder/ServiceOrderDetail/ServiceOrderDetailPackage') t(x))
			END

			--Tabla control de autorizaciones
			IF @AuthorizationNumberControl = 1
			BEGIN
				INSERT INTO [Billing].[ServiceOrderDetailControl]
				(
					ServiceOrderDetailId, Justification, CreationUser, CreationDate
				)
				SELECT td.IdDetail, IIF(td.IsServiceOrderDetailControlJustify = 1, td.ServiceOrderDetailControlJustification, NULL),  @User, [Common].[GETDATE]()
				FROM @TableDetail td
				LEFT JOIN [Billing].[ServiceOrderDetailControl] sodc WITH (NOLOCK) ON td.IdDetail = sodc.ServiceOrderDetailId
				WHERE td.IPSServiceId IS NOT NULL AND sodc.Id IS NULL

				UPDATE sodc
					SET sodc.Justification = td.ServiceOrderDetailControlJustification
				FROM @TableDetail td
				JOIN [Billing].[ServiceOrderDetailControl] sodc WITH (NOLOCK) ON td.IdDetail = sodc.ServiceOrderDetailId
				WHERE td.IsServiceOrderDetailControlJustify = 1
			END

			/*** SI el estado es registrado ***/
			if @Status = 1 begin
				/******** INSERTO Y ACTUALIZO EN EL DISTRIBUTION ******/				
				if @RevenueControlId IS NULL begin
					INSERT INTO [Billing].[RevenueControl]
					([AdmissionNumber]
					,[PatientCode]
					,[FolioQuantity]
					,[TopEventFeeModerator]
					,[TopEventCopay]
					,[TopEventFeeRecovery])
					values(@AdmissionNumber, @PatientCode, 1, 0, 0, 0)
					set @RevenueControlId = SCOPE_IDENTITY()
					set @MaxFolio = 0
				end
			end
			
			---- Se actualiza el distribution
			update sodd 
				set GrandTotalSalesPrice = td.GrandTotalSalesPrice, 
					GrandTotalDiscount = td.ThirdPartyDiscount * td.InvoicedQuantity, 
					ThirdPartySalesPrice = td.GrandTotalSalesPrice, 
					SubTotalPatientSalesPrice = 0, 
					PatientPercentage = 0, 
					Quantity = td.InvoicedQuantity, 
					ThirdPartyPercentage = 100, 
					LastCaregroupId = td.CareGroupId
			from @TableDetail td 
			inner join Billing.ServiceOrderDetailDistribution sodd With(Nolock) on td.IdDetail = sodd.ServiceOrderDetailId 
			where td.EntityState <> @Unchanged
			
			INSERT INTO @IDSRevenueControlDetail(Id, CareGroupId, ThirdPartyId)
				SELECT rcd.Id, rcd.CareGroupId, rcd.ThirdPartyId
				FROM Billing.RevenueControlDetail rcd With(Nolock) 
				WHERE rcd.RevenueControlId = @RevenueControlId and rcd.[Status] = @Uno and rcd.IsMasterAccount <> 2 
			-- se adiciona el filtro para que no tome los folio de tipo aseguradora para agrega mas items

			UPDATE td
				SET td.RevenueControlDetailId = rcd.Id
			FROM @TableDetail td
			join @IDSRevenueControlDetail rcd ON rcd.CareGroupId = td.CareGroupId and rcd.ThirdPartyId = td.ThirdPartyId
			where td.EntityState = @Added 

			---- Inserto los folios que tengo que crear
			INSERT INTO Billing.RevenueControlDetail(RevenueControlId,BillingAuthorizationId,FolioOrder,FolioType,LiquidationType,ContractEntityId,HealthAdministratorId,ThirdPartyId,CareGroupId,TotalFolio,Status,CreationUser,CreationDate)
			output inserted.Id, inserted.CareGroupId, inserted.ThirdPartyId into @IDSRevenueControlDetail(Id, CareGroupId, ThirdPartyId)
			select	DISTINCT
					@RevenueControlId, null, 
					@MaxFolio + 1,
					td.CareGroupType, td.LiquidationType, td.ContractEntityId, td.HealthAdministratorId, td.ThirdPartyId, td.CareGroupId, 0, 1, @User, [Common].[GETDATE]() 
			from @TableDetail td 
			where td.EntityState = @Added AND td.RevenueControlDetailId is NULL

			UPDATE td
				SET td.RevenueControlDetailId = rcd.Id
			FROM @TableDetail td
			join @IDSRevenueControlDetail rcd ON rcd.CareGroupId = td.CareGroupId and rcd.ThirdPartyId = td.ThirdPartyId
			where td.EntityState = @Added AND td.RevenueControlDetailId is NULL

			---- Se inserta los nuevos distribution
			if Exists (select 1 from @TableDetail where IsPackage = @Uno) begin --- Si estan empaquetando
				INSERT INTO Billing.ServiceOrderDetailDistribution(RevenueControlDetailId,ServiceOrderDetailId,Quantity,GrandTotalSalesPrice,GrandTotalDiscount,DistributionType,ThirdPartySalesPrice,ThirdPartyPercentage,ApplyRecoveryFee,RecoveryFeeType,SubTotalPatientSalesPrice,PatientPercentage,LastCaregroupId)
				select @IdRevenueControlDetailPackage, td.IdDetail, td.InvoicedQuantity, td.GrandTotalSalesPrice, td.ThirdPartyDiscount * td.InvoicedQuantity, 1, td.GrandTotalSalesPrice, 100, 1, 1, 0,0, td.CareGroupId from @TableDetail td where td.EntityState = @Added
				
				declare @ifg int = SCOPE_IDENTITY()
				print CONCAT( @ifg,' Id')
			end
			else begin
				INSERT INTO Billing.ServiceOrderDetailDistribution(RevenueControlDetailId,ServiceOrderDetailId,Quantity,GrandTotalSalesPrice,GrandTotalDiscount,DistributionType,
				ThirdPartySalesPrice,ThirdPartyPercentage,ApplyRecoveryFee,RecoveryFeeType,SubTotalPatientSalesPrice,PatientPercentage,LastCaregroupId)
				select td.RevenueControlDetailId, td.IdDetail, td.InvoicedQuantity, td.GrandTotalSalesPrice, td.ThirdPartyDiscount * td.InvoicedQuantity, 1, td.GrandTotalSalesPrice, 100, 1, 1, 0,0, td.CareGroupId 
				from @TableDetail td 
				where td.EntityState = @Added		
				
			END

			SELECT @MaxFolio = ISNULL((SELECT COUNT(1) FROM @IDSRevenueControlDetail), 0)

			---- Actualizo la cantidad de folios que tiene el ingreso
			update Billing.RevenueControl set FolioQuantity = @MaxFolio where Id = @RevenueControlId

			insert into @TableRevenueControlDetailRefresh
			select distinct sodd.RevenueControlDetailId 
			from Billing.ServiceOrder so With(Nolock) 
			inner join Billing.ServiceOrderDetail sod With(Nolock) on sod.ServiceOrderId = so.Id 
			inner join Billing.ServiceOrderDetailDistribution sodd With(Nolock) on sodd.ServiceOrderDetailId = sod.Id
			where so.Id = @IdServiceOrder

			---Recalculo los folios que estan activos
			declare @IdRevenueControlDetailTmp int

			Set @Rows = 1
			Set @RowId = 0

			While @Rows > 0
			Begin
			
				select distinct Top 1 @RowId = RowId, @IdRevenueControlDetailTmp = Id 
				from @TableRevenueControlDetailRefresh 
				where RowId > @RowId And Id is not null 
				Order By RowId

				Set @Rows = @@ROWCOUNT
				If @Rows = 0 
					Break

				--exec [Billing].[SP_UpdateRevenueControlDetailValuesNoSelect] @IdRevenueControlDetailTmp, @OperatingUnitId
				EXEC [Billing].[SP_UpdateRevenueControlDetailValues_Output] @IdRevenueControlDetailTmp, @OperatingUnitId, NULL, NULL
			End
		END
		
		--------------------------------------
		select	@CodeResult = '0', 
				@MessageResult = CONCAT('Se guardo correctamente la orden de servicio ',  @CodeServiceOrder), 
				@StatusResult = 1,
				@Id = @IdServiceOrder
	end try
	begin catch
		IF CURSOR_STATUS('global','updateRevenue_cursor') >= -1  BEGIN
		  IF CURSOR_STATUS('global','updateRevenue_cursor') > -1 BEGIN
			CLOSE updateRevenue_cursor
		  END
		 DEALLOCATE updateRevenue_cursor
		END
		select	@CodeResult = '999', 
				@MessageResult = CONCAT('Error Generando la Orden de Servicio: ', ERROR_MESSAGE(), ' - Linea: ', ERROR_LINE()),
				@StatusResult = 3,
				@Id = 0
	end catch
END