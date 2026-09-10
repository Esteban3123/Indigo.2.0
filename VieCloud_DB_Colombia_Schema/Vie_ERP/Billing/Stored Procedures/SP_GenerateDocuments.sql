
-- =============================================
-- Author:		Diego A. Roldán
-- Create date: 2018-01-13
-- Description:	SP que genera los documentos de control servicios ambulatorios
-- =============================================
CREATE PROCEDURE [Billing].[SP_GenerateDocuments]
	@controlOutPatientServicesXml xml,
	@User varchar(10),
	@SchedulingOwnerReferencesJson nvarchar(max) = NULL OUTPUT,
	@SchedulingOwnerFeedbackEnabled bit = 0
AS
BEGIN
	SET NOCOUNT ON;
	SET @SchedulingOwnerReferencesJson = NULL;
	DECLARE @PendingOwnerRows TABLE (RowNumber int, HealthcareServiceCode varchar(20));
	INSERT INTO @PendingOwnerRows
	SELECT item.value('(RowNumber/text())[1]', 'int'), item.value('(HealthcareServiceCode/text())[1]', 'varchar(20)')
	FROM @controlOutPatientServicesXml.nodes('/ControlOutPatientServices/SchedulingOwnerFeedback') AS data(item);
	DECLARE @SchedulingOwnerReferences TABLE
	(
		RowNumber int NOT NULL,
		HealthcareServiceCode varchar(20) NOT NULL,
		ExternalConsultationId numeric(18,0) NOT NULL,
		ExpectedAppointmentId int NULL
	);
	
	begin try	

		declare @UnitTypeFunctionalUnit int, @OperatingUnitId int, 
			@careGroupId int, 			
			@IngresoExistente varchar(20)			

		declare @tmpIngreso table(NUMINGRES varchar(10) primary key,
			IPCODPACI varchar(15),
			TIPOINGRE int,
			IINGREPOR int,
			CODENTIDA varchar(9),
			ITIPORIES int,
			ICAUSAING int,
			IFECHAING datetime,
			ILIQUIDAC int,
			ICONTROLI varchar(15),
			CODCENATE varchar(10),
			UFUCODIGO varchar(10),
			IAUTORIZA varchar(15),
			IESTADOIN varchar(1),
			IINGRESOA varchar(10),
			ISOATVALO numeric(18,2) null,
			ISALCODIG varchar(3),
			INUMERORE varchar(15),
			IFECHAREM datetime null,
			IAUTORREM varchar(15),
			DEPMUNCOD varchar(5),
			AIPSREMIS varchar(100),
			IOBSERVAC varchar(2000),
			IJUSTIFIC varchar(254),
			IREINGRES int,
			UFUINGMED varchar(10),
			CODPROING varchar(20),
			UFUEGRMED varchar(10),
			CODPROEGR varchar(20),
			UFUINGHOS varchar(10),
			UFUEGRHOS varchar(10),
			CODESPTRA varchar(3),
			TIPOPROFE varchar(2),
			UFUAACTMED varchar(10),
			UFUAACTHOS varchar(10),
			CODCAMACT int null,
			CODDIAING varchar(4),
			CODDIAEGR varchar(4),
			UFUACTPAC varchar(10),
			CODUSUCRE varchar(20),
			FECREGCRE datetime null,
			CODUSUMOD varchar(20),
			FECREGMOD datetime null,
			CODUSUANU varchar(20),
			FECREGANU datetime null,
			NUMINGREI varchar(10),
			CODICAMHO varchar(10),
			FECHOSPIT datetime null,
			INDAUDFOR numeric(18,0),
			IPRNOMBRE varchar(80),
			IPCODACTR varchar(15),
			IPEXPEDIC varchar(40),
			FECACTRAN datetime null,
			HORACIDEN varchar(5),
			IPTELEFON varchar(15),
			OBSERACIT varchar(250),
			OBSERAREM varchar(250),
			INGRECEXT bit null,
			PACATENDI bit null,
			ESCADOWNT char(1),
			ESCABIERI char(1),
			ESCARASS char(1),
			ESCNORPAC char(1),
			SERSUSCEP bit null,
			ESCVASPAC char(1),
			ESCAPAPAC char(1),
			VIVESOLO bit null,
			GENCAREGROUP int null,
			GENULTLIQUI date null,
			GENCONENTITY int null,
			NUMTRIAGEI char(20),
			SOLRESHEMO BIT,
			TRATAESPECIA int null,
			IdAdmissionType int null,
			IdEntryRoutesHealthServices int null,
			IdHealthPurposes int null,
			IdAdmissionModalities int null)
			
		declare @tmpServiceOrderDetail table(
			RowXml int,
			Id int,
			ServiceOrderId int,
			CareGroupId int,
			HealthAdministratorId int null,
			ThirdPartyId int null,
			ServiceType tinyint,
			RecordType tinyint,
			CUPSEntityId int null,
			IPSServiceId int null,
			HospitalStayId int null,
			HospitalStayDetailId int null,
			ControlExternalConsultation tinyint null,
			ControlExternalConsultationCode numeric(18,0) null,
			CUPSAssociateService bit,
			CodeAssociateService varchar(50),
			IsPackage bit,
			Packaging bit,
			PackageServiceOrderDetailId int null,
			LiquidationType tinyint,
			Presentation tinyint null,
			ProductId int null,
			InvoicedQuantity int,
			SupplyQuantity int,
			DevolutionQuantity int,
			RateManualSalePrice numeric(18,2),
			CostValue numeric(18,2),
			ServiceDate datetime,
			AuthorizationNumber varchar(20),
			PerformsFunctionalUnitId int,
			PerformsHealthProfessionalCode char(20),
			PerformsProfessionalSpecialty char(3),
			PerformsHealthProfessionalThirdPartyId int null,
			BillingConceptId int null,
			CostCenterId int,
			SettlementType tinyint,
			IncludeServiceOrderDetailId int null,
			RecoveryRatio numeric(5,2) null,
			RateManualId int null,
			RateManualType tinyint null,
			RateManualDetailId int null,
			DefinitionRateDetailId int null,
			DefinitionRateDetailConditionId int null,
			SubTotalSalesPrice numeric(18,2),
			ThirdPartyDiscount numeric(18,2),
			ThirdPartyDiscountPercentage numeric(5,2),
			TotalSalesPrice numeric(18,2),
			GrandTotalSalesPrice numeric(18,2),
			SurchargeApply bit,
			SurgicalInterventionType tinyint null,
			SurgeryNumber tinyint,
			IsFirstEvent bit,
			IsAnnulled bit,
			IsDelete bit,
			IncomeMainAccountId int,
			IdCita varchar(10), 
			CODSERIPS varchar(20),
			ApplyRIAS varchar(20) NULL,
			RIASCupsId int NULL,
			RealizedQuantity int NULL
			,HemocomponentId INT NULL,
			CUPSEntityContractDescriptionId int NULL,
			QuotationServiceOrderDetailId int NULL,
			TraceabilityPaperworkEventsId int NULL, 
			IsServiceOrderDetailControlJustify BIT NOT NULL, 
			ServiceOrderDetailControlJustification VARCHAR(500),
			FinalProductCost DECIMAL(20,2) NOT NULL, 
			GrossValue NUMERIC(20,2), 
			TaxValue NUMERIC(20,2)
		)

		declare @tmpServiceOrderDetailSurgical table(
			Id int,
			ServiceOrderDetailId int,
			ServiceOrderDetailIdRow int,
			IPSServiceId int,
			InvoicedQuantity int,
			LiquidationPercentage numeric(5, 2),
			RateManualSalePrice numeric(18, 0),
			TotalSalesPrice numeric(18, 0),
			PerformsHealthProfessionalCode char(20),
			PerformsHealthProfessionalThirdPartyId int,
			CostValue numeric(18, 2),
			BillingConceptId int,
			CostCenterId int,
			RateManualDetailSurgicalId int,
			SurchargeApply bit,
			OnlyMedicalFees bit,
			IncomeMainAccountId int, 
			EntityState varchar(50))
			
		declare @tmpCitasMedicas table(
			TempId INT NOT NULL IDENTITY(1,1),						 
			OrigenCirugia int,
			Codigo varchar(20),
			CodeRelated varchar(20),			   
			FechaCita datetime,
			CodigoProfesional varchar(20),
			Profesional varchar(50),
			NitMedico varchar(20),
			Consultorio varchar(20),
			TipoCita int null,
			ActividadMedica varchar(20),
			CodigoServicio varchar(20),
			Especialidad varchar(20),
			Servicio varchar(20),
			CantidadServicio int,
			CodigoEspecialidad varchar(20),
			AreaServicio varchar(20),
			CentroCosto varchar(20),
			TipoSolicitud int null,
			RequiresConfirmAppointment bit,
			InvoiceId int null,
			InvoiceNumber varchar(20),
			CodigoPaciente varchar(20),
			CodigoCentroAtencion varchar(20),
			CodigoUnidadFuncional varchar(20),
			AdmissionNumberInvoice varchar(20),
			IsFalseId bit,
			GeneratedId numeric(18,0),
			HealthAdministratorIdInvoice int null,
			CareGroupIdInvoice int null,
			NombreCompletoPaciente varchar(100),
			CUPSEntityContractDescriptionId int null,
			ActivityType int,
			IdSala int null,
			CODACTMED char(3),
			TypeOfScheduleActivity int)

		declare @messageNotificationContract varchar(max) = '', 
			@stringResult varchar(max) = '',
			@numingresSO varchar(10), 
			@patientCodeSO varchar(15), 
			@careGroupIdSO int, 
			@healthAdministratorIdSO int

		declare @careGroupType tinyint, @contractStatus tinyint,
				@contractTerminationControl tinyint, @contractNotificationValueType tinyint,
				@contractExecuteValue numeric(18, 0), @contractValue numeric(18,0),
				@contractPercentageNotification numeric(5,2),
				@contractNotificationValue numeric(18,0),
				@contractCodeName varchar(200), @notificationTimeType tinyint,
				@contractEndDate date, @notificationDays int

		--HEMOCOMPONENT
		declare @Hemocomponent table(
			IdAuto int
			,Id int
			,Code varchar(20)
			,Description varchar(200)
			,ProfessionalId VARCHAR(20)
			,Quantity int
			,SpecialityCode varchar(3)
			,IdAGASICITA int
			,VolumenComponent decimal(4,1)
		)

		declare @HemocomponentDetail table(
			 IdAuto int identity(1,1)
			,Id int
			,HemocomponentId int
			,TypeServiceIPS tinyint
			,kindLoad tinyint
			,CodeServiceIPS varchar(20)
			,DescriptionServiceIPS varchar(300)
			,SERRASANTI bit
			,IdAutoC int
		)
		declare @HCORHEMCO_HCCOMSAN table(
		HCORHEMCOID int
		,HCCOMSANID int
		)

		declare @HCORHEMBOL_HEMODETAIL table(
		HCORHEMBOLID int
		,HCORHEMCOID INT
		,HDIdAuto int
		)
		DECLARE @SERRASANTI VARCHAR(20)
		DECLARE @CODCENATE VARCHAR(10)
		DECLARE @UFUCODIGO VARCHAR(10)

		DECLARE @IdsAMBORDIMA table(
		Id int
		)

		/*INSERCION TABLA DE LOGS PARA CONTROL SERVICIOS AMBULATORIO*/
		INSERT INTO [Billing].[LogsControlOutpatientServices](ControlOutPatientServicesXml,CreationUser,CreationDate)
		SELECT @controlOutPatientServicesXml,@User,Common.GETDATE()
		/*--------------------------------------------------------*/
		--
		
		select 
			@UnitTypeFunctionalUnit = t.x.value('UnitTypeFunctionalUnit[1]','int'),
			@OperatingUnitId = t.x.value('OperatingUnitId[1]','int'),
			@CareGroupId = t.x.value('CareGroupId[1]','int'),
			@IngresoExistente = t.x.value('IngresoExistente[1]','varchar(20)')		
			from @controlOutPatientServicesXml.nodes('/ControlOutPatientServices') t(x)

		insert into @tmpServiceOrderDetail
		select
			t.x.value('RowXml[1]','int'),
			t.x.value('Id[1]','int'),
			t.x.value('ServiceOrderId[1]','int'),
			t.x.value('CareGroupId[1]','int'),
			t.x.value('HealthAdministratorId[1]','int'),
			t.x.value('ThirdPartyId[1]','int'),
			t.x.value('ServiceType[1]','tinyint'),
			t.x.value('RecordType[1]','tinyint'),
			t.x.value('CUPSEntityId[1]','int'),
			t.x.value('IPSServiceId[1]','int'),
			t.x.value('HospitalStayId[1]','int'),
			t.x.value('HospitalStayDetailId[1]','int'),
			t.x.value('ControlExternalConsultation[1]','tinyint'),
			--t.x.value('ControlExternalConsultationCode[1]','numeric(18,0)'),
			convert(numeric(18,0), replace(t.x.value('ControlExternalConsultationCode[1]','varchar(25)'), ',', '.')),			
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
			--t.x.value('RateManualSalePrice[1]','numeric(18,0)'),
			convert(numeric(18,2), replace(t.x.value('RateManualSalePrice[1]','varchar(25)'), ',', '.')),
			--t.x.value('CostValue[1]','numeric(18,2)'),
			convert(numeric(18,2), replace(t.x.value('CostValue[1]','varchar(25)'), ',', '.')),			
			convert(datetime, t.x.value('ServiceDate[1]', 'nvarchar(19)'), 103),			
			t.x.value('AuthorizationNumber[1]','varchar(20)'),
			t.x.value('PerformsFunctionalUnitId[1]','int'),
			t.x.value('PerformsHealthProfessionalCode[1]','varchar(20)'),
			t.x.value('PerformsProfessionalSpecialty[1]','varchar(3)'),
			t.x.value('PerformsHealthProfessionalThirdPartyId[1]','int'),
			t.x.value('BillingConceptId[1]','int'),
			t.x.value('CostCenterId[1]','int'),
			t.x.value('SettlementType[1]','tinyint'),
			t.x.value('IncludeServiceOrderDetailId[1]','int'),
			--t.x.value('RecoveryRatio[1]','numeric(5,2)'),
			convert(numeric(5,2), replace(t.x.value('RecoveryRatio[1]','varchar(8)'), ',', '.')),
			t.x.value('RateManualId[1]','int'),
			t.x.value('RateManualType[1]','tinyint'),
			t.x.value('RateManualDetailId[1]','int'),
			t.x.value('DefinitionRateDetailId[1]','int'),
			t.x.value('DefinitionRateDetailConditionId[1]','int'),
			--t.x.value('SubTotalSalesPrice[1]','numeric(18,2)'),
			convert(numeric(18,2), replace(t.x.value('SubTotalSalesPrice[1]','varchar(25)'), ',', '.')),
			--t.x.value('ThirdPartyDiscount[1]','numeric(18,0)'),
			convert(numeric(18,2), replace(t.x.value('ThirdPartyDiscount[1]','varchar(25)'), ',', '.')),
			--t.x.value('ThirdPartyDiscountPercentage[1]','numeric(5,2)'),
			convert(numeric(5,2), replace(t.x.value('ThirdPartyDiscountPercentage[1]','varchar(8)'), ',', '.')),
			--t.x.value('TotalSalesPrice[1]','numeric(18,2)'),
			convert(numeric(18,2), replace(t.x.value('TotalSalesPrice[1]','varchar(25)'), ',', '.')),
			--t.x.value('GrandTotalSalesPrice[1]','numeric(18,0)'),
			convert(numeric(18,2), replace(t.x.value('GrandTotalSalesPrice[1]','varchar(25)'), ',', '.')),
			t.x.value('SurchargeApply[1]','bit'),
			t.x.value('SurgicalInterventionType[1]','tinyint'),
			t.x.value('SurgeryNumber[1]','tinyint'),
			t.x.value('IsFirstEvent[1]','bit'),
			t.x.value('IsAnnulled[1]','bit'),
			t.x.value('IsDelete[1]','bit'),
			t.x.value('IncomeMainAccountId[1]','int'),
			t.x.value('IdCita[1]','varchar(10)'),
			t.x.value('CODSERIPS[1]','varchar(20)'),
			t.x.value('ApplyRIAS[1]','varchar(20)'),
			t.x.value('RIASCupsId[1]','int'),
			t.x.value('RealizedQuantity[1]','int')
			,t.x.value('HemocomponentId[1]','int'),
			t.x.value('CUPSEntityContractDescriptionId[1]','int'),
			t.x.value('QuotationServiceOrderDetailId[1]','int'),
			t.x.value('TraceabilityPaperworkEventsId[1]','int'),
			ISNULL(t.x.value('IsServiceOrderDetailControlJustify[1]','bit'), 0),
			t.x.value('ServiceOrderDetailControlJustification[1]','varchar(500)'),
			convert(numeric(18,2), replace(t.x.value('FinalProductCost[1]','varchar(20)'), ',', '.')),
			convert(numeric(18,2), replace(t.x.value('GrossValue[1]','varchar(20)'), ',', '.')),
			convert(numeric(18,2), replace(t.x.value('TaxValue[1]','varchar(20)'), ',', '.'))
		  from @controlOutPatientServicesXml.nodes('/ControlOutPatientServices/ServiceOrderDetail') t(x)
		  
		  insert into @tmpServiceOrderDetailSurgical
			select
				t.x.value('Id[1]','int'),
				t.x.value('ServiceOrderDetailId[1]','int'),
				t.x.value('ServiceOrderDetailIdRow[1]','int'),
				t.x.value('IPSServiceId[1]','int'),
				t.x.value('InvoicedQuantity[1]','int'),
				convert(NUMERIC(5,2),REPLACE(t.x.value('LiquidationPercentage[1]','varchar(7)'),',','.')),
				convert(NUMERIC(18,2),REPLACE(t.x.value('RateManualSalePrice[1]','varchar(18)'),',','.')),
				convert(NUMERIC(18,2),REPLACE(t.x.value('TotalSalesPrice[1]','varchar(18)'),',','.')),
				t.x.value('PerformsHealthProfessionalCode[1]','varchar(20)'),
				t.x.value('PerformsHealthProfessionalThirdPartyId[1]','int'),
				convert(NUMERIC(18,2),REPLACE(t.x.value('CostValue[1]','varchar(20)'),',','.')),
				t.x.value('BillingConceptId[1]','int'),
				t.x.value('CostCenterId[1]','int'),
				t.x.value('RateManualDetailSurgicalId[1]','int'),
				t.x.value('SurchargeApply[1]','bit'),
				t.x.value('OnlyMedicalFees[1]','bit'),
				t.x.value('IncomeMainAccountId[1]','int'),
				t.x.value('EntityState[1]','varchar(50)')
			from @controlOutPatientServicesXml.nodes('/ControlOutPatientServices/ServiceOrderDetail/ServiceOrderDetailSurgical') t(x)

		insert into @tmpCitasMedicas
		select
			t.x.value('OrigenCirugia[1]','int'),
			t.x.value('Codigo[1]','varchar(20)'),
			t.x.value('CodeRelated[1]','varchar(20)'),								 
			convert(datetime, t.x.value('FechaCita[1]', 'nvarchar(19)'), 103),
			t.x.value('CodigoProfesional[1]','varchar(20)'),
			t.x.value('Profesional[1]','varchar(50)'),
			t.x.value('NitMedico[1]','varchar(20)'),
			t.x.value('Consultorio[1]','varchar(20)'),
			t.x.value('TipoCita[1]','int'),
			t.x.value('ActividadMedica[1]','varchar(20)'),			
			t.x.value('CodigoServicio[1]','varchar(20)'),
			t.x.value('Especialidad[1]','varchar(20)'),
			t.x.value('Servicio[1]','varchar(20)'),
			t.x.value('CantidadServicio[1]','int'),
			t.x.value('CodigoEspecialidad[1]','varchar(20)'),
			t.x.value('AreaServicio[1]','varchar(20)'),
			t.x.value('CentroCosto[1]','varchar(20)'),
			t.x.value('TipoSolicitud[1]','int'),
			t.x.value('RequiresConfirmAppointment[1]','bit'),
			t.x.value('InvoiceId[1]','int'),
			t.x.value('InvoiceNumber[1]','varchar(20)'),
			t.x.value('CodigoPaciente[1]','varchar(20)'),
			t.x.value('CodigoCentroAtencion[1]','varchar(20)'),
			t.x.value('CodigoUnidadFuncional[1]','varchar(20)'),
			t.x.value('AdmissionNumberInvoice[1]','varchar(20)'),
			t.x.value('IsFalseId[1]','bit'),
			--t.x.value('GeneratedId[1]','numeric(18,0)'),
			convert(numeric(18,0), replace(t.x.value('GeneratedId[1]','varchar(25)'), ',', '.')),
			t.x.value('HealthAdministratorIdInvoice[1]','int'),
			t.x.value('CareGroupIdInvoice[1]','int'),
			t.x.value('NombreCompletoPaciente[1]','varchar(100)'),
			IIF(t.x.value('CUPSEntityContractDescriptionId[1]','varchar(20)') = '', null, t.x.value('CUPSEntityContractDescriptionId[1]','varchar(20)')),
			t.x.value('ActivityType[1]','int'),
			t.x.value('IdSala[1]','int'),
			t.x.value('CODACTMED[1]','Char(3)'),
			t.x.value('TypeOfScheduleActivity[1]','int')
		  from @controlOutPatientServicesXml.nodes('/ControlOutPatientServices/CitaMedica') t(x)

		  		  
		insert into @tmpIngreso
		select
			t.x.value('NUMINGRES[1]','varchar(10)'),
			t.x.value('IPCODPACI[1]','varchar(15)'),
			t.x.value('TIPOINGRE[1]','int'),
			t.x.value('IINGREPOR[1]','int'),
			t.x.value('CODENTIDA[1]','varchar(9)'),
			t.x.value('ITIPORIES[1]','int'),
			t.x.value('ICAUSAING[1]','int'),
			[Common].[GETDATE](),
			t.x.value('ILIQUIDAC[1]','int'),
			t.x.value('ICONTROLI[1]','varchar(15)'),
			t.x.value('CODCENATE[1]','varchar(10)'),
			t.x.value('UFUCODIGO[1]','varchar(10)'),
			t.x.value('IAUTORIZA[1]','varchar(15)'),
			t.x.value('IESTADOIN[1]','varchar(1)'),
			t.x.value('IINGRESOA[1]','varchar(10)'),
			t.x.value('ISOATVALO[1]','numeric(18,2)'),
			t.x.value('ISALCODIG[1]','varchar(3)'),
			t.x.value('INUMERORE[1]','varchar(15)'),
			convert(datetime, t.x.value('IFECHAREM[1]', 'nvarchar(19)'), 103),
			t.x.value('IAUTORREM[1]','varchar(15)'),
			t.x.value('DEPMUNCOD[1]','varchar(5)'),
			t.x.value('AIPSREMIS[1]','varchar(100)'),
			t.x.value('IOBSERVAC[1]','varchar(2000)'),
			t.x.value('IJUSTIFIC[1]','varchar(254)'),
			t.x.value('IREINGRES[1]','int'),
			t.x.value('UFUINGMED[1]','varchar(10)'),
			t.x.value('CODPROING[1]','varchar(20)'),
			t.x.value('UFUEGRMED[1]','varchar(10)'),
			t.x.value('CODPROEGR[1]','varchar(20)'),
			t.x.value('UFUINGHOS[1]','varchar(10)'),
			t.x.value('UFUEGRHOS[1]','varchar(10)'),
			t.x.value('CODESPTRA[1]','varchar(3)'),
			t.x.value('TIPOPROFE[1]','varchar(2)'),
			t.x.value('UFUAACTMED[1]','varchar(10)'),
			t.x.value('UFUAACTHOS[1]','varchar(10)'),
			t.x.value('CODCAMACT[1]','int'),
			t.x.value('CODDIAING[1]','varchar(4)'),
			t.x.value('CODDIAEGR[1]','varchar(4)'),
			t.x.value('UFUACTPAC[1]','varchar(10)'),
			t.x.value('CODUSUCRE[1]','varchar(20)'),
			convert(datetime, t.x.value('FECREGCRE[1]', 'nvarchar(19)'), 103),
			t.x.value('CODUSUMOD[1]','varchar(20)'),
			convert(datetime, t.x.value('FECREGMOD[1]', 'nvarchar(19)'), 103),
			t.x.value('CODUSUANU[1]','varchar(20)'),
			convert(datetime, t.x.value('FECREGANU[1]', 'nvarchar(19)'), 103),
			t.x.value('NUMINGREI[1]','varchar(10)'),
			t.x.value('CODICAMHO[1]','varchar(10)'),
			convert(datetime, t.x.value('FECHOSPIT[1]', 'nvarchar(19)'), 103),
			t.x.value('INDAUDFOR[1]','numeric(18,0)'),
			t.x.value('IPRNOMBRE[1]','varchar(80)'),
			t.x.value('IPCODACTR[1]','varchar(15)'),
			t.x.value('IPEXPEDIC[1]','varchar(40)'),
			convert(datetime, t.x.value('FECACTRAN[1]', 'nvarchar(19)'), 103),
			t.x.value('HORACIDEN[1]','varchar(5)'),
			t.x.value('IPTELEFON[1]','varchar(15)'),
			t.x.value('OBSERACIT[1]','varchar(250)'),
			t.x.value('OBSERAREM[1]','varchar(250)'),
			t.x.value('INGRECEXT[1]','bit'),
			t.x.value('PACATENDI[1]','bit'),
			t.x.value('ESCADOWNT[1]','char(1)'),
			t.x.value('ESCABIERI[1]','char(1)'),
			t.x.value('ESCARASS[1]','char(1)'),
			t.x.value('ESCNORPAC[1]','char(1)'),
			t.x.value('SERSUSCEP[1]','bit'),
			t.x.value('ESCVASPAC[1]','char(1)'),
			t.x.value('ESCAPAPAC[1]','char(1)'),
			t.x.value('VIVESOLO[1]','bit'),
			t.x.value('GENCAREGROUP[1]','int'),
			convert(date, t.x.value('GENULTLIQUI[1]', 'nvarchar(19)'), 103),
			t.x.value('GENCONENTITY[1]','int'),
			t.x.value('NUMTRIAGEI[1]','char(20)'),
			t.x.value('SOLRESHEMO[1]','bit'),
			t.x.value('TRATAESPECIA[1]','int'),
			t.x.value('IdAdmissionType[1]','int'),
			t.x.value('IdEntryRoutesHealthServices[1]','int'),
			t.x.value('IdHealthPurposes[1]','int'),
			t.x.value('IdAdmissionModalities[1]','int')
		  from @controlOutPatientServicesXml.nodes('/ControlOutPatientServices/IngresoOrdenServicio') t(x)

		--HEMOCOMPONENT
		insert into @Hemocomponent
		select
			 t.x.value('IdAuto[1]','int')
			,t.x.value('Id[1]','int')
			,t.x.value('Code[1]','varchar(20)')
			,t.x.value('Description[1]','varchar(200)')
			,t.x.value('ProfessionalId[1]','varchar(20)')
			,t.x.value('Quantity[1]','int')
			,t.x.value('SpecialityCode[1]','varchar(3)')
			,iif(t.x.value('IdAGASICITA[1]','int') = 0,NULL, t.x.value('IdAGASICITA[1]','int'))
			,CONVERT(decimal(4,1), REPLACE(t.x.value('VolumenComponent[1]','Varchar(5)'),',','.'))
			from @controlOutPatientServicesXml.nodes('/ControlOutPatientServices/Hemocomponent') t(x)

		insert into @HemocomponentDetail
		select
			t.x.value('Id[1]','int')
			,t.x.value('HemocomponentId[1]','int')
			,t.x.value('TypeServiceIPS[1]','tinyint')
			,t.x.value('kindLoad[1]','tinyint')
			,t.x.value('CodeServiceIPS[1]','varchar(20)')
			,t.x.value('DescriptionServiceIPS[1]','varchar(300)')
			,t.x.value('SERRASANTI[1]','bit')
			,t.x.value('IdAutoC[1]','int')
			from @controlOutPatientServicesXml.nodes('/ControlOutPatientServices/Hemocomponent/HemocomponentDetail') t(x)
		--
		
		--validamos el contrato
		if @careGroupId > 0 begin
			
			select @careGroupType = cg.CareGroupType,
				@contractStatus = c.[Status],
				@contractTerminationControl = cd.TerminationControl,
				@contractNotificationValueType = cd.NotificationValueType,
				@contractExecuteValue = c.ExecuteValue,
				@contractValue = c.ContractValue,
				@contractPercentageNotification = cd.PercentageNotification,
				@contractNotificationValue = cd.NotificationValue,
				@contractCodeName = concat(c.Code, ' - ', cd.[ContractName]),
				@notificationTimeType = cd.NotificationTimeType,
				@contractEndDate = cd.BillingEndDate,
				@notificationDays = cd.NotificationDays
			from [Contract].[CareGroup] cg 
			left join [Contract].[Contract] c  on cg.ContractId = c.Id
			left join Contract.ContractDetail cd  on cd.ContractId = c.Id and cd.ValidRecord = 1
			where cg.Id = @careGroupId
						

			if @careGroupType = 1 begin

				--Se valida que el contrato asociado al grupo de atención tenga detalles y además tenga uno válido
				if not exists(select 1
				from Contract.CareGroup cg  
				inner join Contract.Contract c  on c.Id = cg.ContractId
				inner join Contract.ContractDetail cd  on cd.ContractId = c.Id and cd.ValidRecord = 1
				where cg.Id = @careGroupId)
				begin
					select '999' as CodeResult, 'El contrato asociado al grupo de atención no tiene detalles de contrato o no tiene un detalle válido' as MessageResult, '' as NotificationContract, '' as NumIngres
					return
				end

				if @contractStatus = 2 begin
					select '999' as CodeResult, 'El contrato se encuentra Suspendido.' as MessageResult, '' as NotificationContract, '' as NumIngres
					return
				end
				else if @contractStatus = 3 begin
					select '999' as CodeResult, 'El contrato se encuentra Terminado.' as MessageResult, '' as NotificationContract, '' as NumIngres
					return
				end
				--validaciones del contrato
				if @contractTerminationControl = 3 begin				
					--Terminación del contrato por valor del contrato
					if @contractNotificationValueType = 2 And @contractExecuteValue > (@contractValue * @contractPercentageNotification / 100) begin
						--% del contrato o valor del contrato
						set @messageNotificationContract = @messageNotificationContract + 'Atención: Queda un saldo restante de ' + convert(varchar, cast((@contractValue - @contractExecuteValue) AS money), 1)
							+ ' para la terminación del contrato ' + @contractCodeName
					end
					else if @contractNotificationValueType = 3 And @contractExecuteValue > @contractNotificationValue begin
						--Valor del contrato
						set @messageNotificationContract = @messageNotificationContract + 'Atención: Queda un saldo restante de ' + convert(varchar, cast((@contractValue - @contractExecuteValue) AS money), 1)
							+ ' para la terminación del contrato ' + @contractCodeName
					end
				end
				else if @contractTerminationControl = 2 begin
					if @notificationTimeType = 2 And (select dateadd(day, @notificationDays, [Common].[GETDATE]())) >= @contractEndDate begin
						set @messageNotificationContract = @messageNotificationContract + 'Atención: La fecha del contrato vencera en ' + 
							cast((select day(@contractEndDate) - day(dateadd(day, @notificationDays, [Common].[GETDATE]()))) as varchar) + ' dias'
					end
				end
				else if @contractTerminationControl = 4 begin
					if @contractNotificationValueType = 2 And @contractExecuteValue > (@contractValue * @contractPercentageNotification / 100) begin
						--% del contrato o valor del contrato
						set @messageNotificationContract = @messageNotificationContract + 'Atención: Queda un saldo restante de ' + convert(varchar, cast((@contractValue - @contractExecuteValue) AS money), 1)
							+ ' para la terminación del contrato ' + @contractCodeName
					end
					else if @contractNotificationValueType = 3 And @contractExecuteValue > @contractNotificationValue begin
						--Valor del contrato
						set @messageNotificationContract = @messageNotificationContract + 'Atención: Queda un saldo restante de ' + convert(varchar, cast((@contractValue - @contractExecuteValue) AS money), 1)
							+ ' para la terminación del contrato ' + @contractCodeName
					end
					if @notificationTimeType = 2 And (select dateadd(day, @notificationDays, [Common].[GETDATE]())) >= @contractEndDate begin
						set @messageNotificationContract = @messageNotificationContract + 'Atención: La fecha del contrato vencera en ' + 
							cast((select day(@contractEndDate) - day(dateadd(day, @notificationDays, [Common].[GETDATE]()))) as varchar) + ' dias'
					end
				end
			end
		end
		
		declare @codentida varchar(20) = ''

		if (select count(*) from @tmpServiceOrderDetail) > 0 Or (select count(*) from @tmpCitasMedicas where InvoiceId is null) > 0 OR EXISTS(SELECT 1 from @Hemocomponent) begin
			
			if @IngresoExistente is null Or @IngresoExistente = '' begin
				declare @connumact VARCHAR(10) = CONVERT(varchar(10), right(newid(),10))
				
				if @careGroupType = 3 begin
					select @codentida = CODENTIDA from .INENTIDAD  where CODIGONIT = '000000000000999'
					if @codentida is null Or @codentida = '' begin
						select '999' as CodeResult, 'La entidad administradora 999 no se encontró en INDIGO Crystal.' as MessageResult, '' as NotificationContract, '' as NumIngres
						return
					end
				end
				else begin
				
					declare @IdHealthAdministrator int, 
						@CodeHealthAdministrator VARCHAR(20),
						@thirdPartyIdHealthAdm int,
						@thirdPartyNit varchar(20)

					select 
						@IdHealthAdministrator = ha.Id, 
						@CodeHealthAdministrator = ha.Code,
						@thirdPartyIdHealthAdm = tp.Id,
						@thirdPartyNit = tp.Nit
					from [Contract].[HealthAdministrator] ha  
					inner join Common.ThirdParty tp  on ha.ThirdPartyId = tp.Id
					where ha.Id = (select top 1 CODENTIDA from @tmpIngreso)

					if @thirdPartyIdHealthAdm = 0 begin
						select '999' as CodeResult, 'No se encontró tercero relacionado a la administradora.' as MessageResult, 
							'' as NotificationContract, '' as NumIngres
						return
					end
					
					select @codentida = CODENTIDA from .INENTIDAD  WHERE CODENTIDA = @CodeHealthAdministrator
					if @codentida is null Or @codentida = '' begin
						select @codentida = CODENTIDA from .INENTIDAD  where CODIGONIT = REPLACE(STR(@thirdPartyNit, 15), SPACE(1), '0')
						if @codentida is null Or @codentida = '' begin
							select '999' as CodeResult, 'La entidad administradora para el tercero (' + @thirdPartyNit + ') no se encotró en INDIGO Crystal.' as MessageResult, 
								'' as NotificationContract, '' as NumIngres
							return
						end
					end
					
				end
				
				if @careGroupType = 3 begin
					declare @genconentity int
					select @genconentity = Id from [Contract].[HealthAdministrator] ha  where Code = '999'
					if @genconentity = 0 begin
						select '999' as CodeResult, 'No se puede crear el ingreso debido a que no se encontró la entidad administradora (999).' as MessageResult, 
							'' as NotificationContract, '' as NumIngres
						return
					end
					update @tmpIngreso set GENCONENTITY = @genconentity
				end
				
				if (select count(*) from @tmpCitasMedicas) > 0 And 
					(select count(*) from @tmpCitasMedicas where TipoCita is not null And TipoCita = 3 And InvoiceId is null) = 
					(select count(*) from @tmpCitasMedicas where InvoiceId is null) begin
					update @tmpIngreso set IESTADOIN = 'C'
				end
				
				insert into .ADINGRESO (NUMINGRES,IPCODPACI,CODENTIDA,TIPOINGRE,IINGREPOR,ITIPORIES,ICAUSAING,IFECHAING,ILIQUIDAC,
					ICONTROLI,CODCENATE,UFUCODIGO,IAUTORIZA,IESTADOIN,IINGRESOA,ISOATVALO,ISALCODIG,INUMERORE,IFECHAREM,IAUTORREM,DEPMUNCOD,
					AIPSREMIS,IOBSERVAC,IREINGRES,TIPOPROFE,UFUACTPAC,CODUSUCRE,FECREGCRE,INDAUDFOR,INGRECEXT,PACATENDI,GENCAREGROUP,
					GENCONENTITY, SOLRESHEMO,IdAdmissionType,TRATAESPECIA,IdEntryRoutesHealthServices,IdHealthPurposes,IdAdmissionModalities)
				select @connumact,IPCODPACI,@codentida,TIPOINGRE,IINGREPOR,ITIPORIES,ICAUSAING,[Common].[GETDATE](),ILIQUIDAC,
					ICONTROLI,CODCENATE,UFUCODIGO,IAUTORIZA,IESTADOIN,IINGRESOA,ISOATVALO,ISALCODIG,INUMERORE,IFECHAREM,IAUTORREM,DEPMUNCOD,
					AIPSREMIS,IOBSERVAC,IREINGRES,TIPOPROFE,UFUACTPAC,CODUSUCRE,[Common].[GETDATE](),INDAUDFOR,INGRECEXT,PACATENDI,GENCAREGROUP,
					GENCONENTITY, SOLRESHEMO, IdAdmissionType, iif(atp.IsSpecialTreatment = 0,1,atp.TreatmentType),IdEntryRoutesHealthServices,
					IdHealthPurposes,IdAdmissionModalities
					--(select top 1 Id from EntryRoutesHealthServices where code = '1')  as IdEntryRoutesHealthServices
				from @tmpIngreso temp
				JOIN Admissions.AdmissionType atp  on temp.IdAdmissionType = atp.Id
				
				set @numingresSO = @connumact
				set @patientCodeSO = (select top 1 IPCODPACI from @tmpIngreso)
				set @careGroupIdSO = (select top 1 GENCAREGROUP from @tmpIngreso)
				set @healthAdministratorIdSO = (select top 1 GENCONENTITY from @tmpIngreso)
				set @stringResult = @stringResult + 'Ingreso con consecutivo ' + @numingresSO + CHAR(13) + CHAR(10)
				
			end
			else begin
				set @numingresSO = @IngresoExistente
				set @patientCodeSO = (select top 1 IPCODPACI from @tmpIngreso)
				set @careGroupIdSO = (select top 1 GENCAREGROUP from @tmpIngreso)
				set @healthAdministratorIdSO = (select top 1 GENCONENTITY from @tmpIngreso)
				
				set @codentida = (select top 1 CODENTIDA from .ADINGRESO where NUMINGRES = @IngresoExistente)

				if (select count(*) from @tmpCitasMedicas) > 0 
					And (select count(*) from @tmpCitasMedicas where TipoCita is not null And TipoCita = 3 And InvoiceId is null) =
					(select count(*) from @tmpCitasMedicas where InvoiceId is null) begin
					
					update .ADINGRESO set IESTADOIN = 'C' where NUMINGRES = @numingresSO
					set @stringResult = @stringResult + 'Se cerró el Ingreso con consecutivo ' + @numingresSO + CHAR(13)+CHAR(10)
				end
				update .ADINGRESO set SOLRESHEMO = t.SOLRESHEMO
				from @tmpIngreso t
				where t.SOLRESHEMO = 1 AND ADINGRESO.NUMINGRES = t.NUMINGRES

			end
		end
		else if (select count(*) from @tmpCitasMedicas where InvoiceId is not null) > 0 begin

			delete from @tmpIngreso
			insert into @tmpIngreso (CODENTIDA) values ('')

			set @numingresSO = (select top 1 AdmissionNumberInvoice from @tmpCitasMedicas)
			set @patientCodeSO = (select top 1 CodigoPaciente from @tmpCitasMedicas)
			set @careGroupIdSO = @careGroupId
			if (select top 1 HealthAdministratorIdInvoice from @tmpCitasMedicas) is null begin
				set @healthAdministratorIdSO = 0
			end
			else begin
				set @healthAdministratorIdSO = (select top 1 HealthAdministratorIdInvoice from @tmpCitasMedicas)
			end
		end
		----------------------------------------------------------------	
		-----------------------------------------------------------------------------------------------------
		--Relaciona la cita, el servicio (porque si es cita de apoyo dx entonces varios servicios pueden tener la misma cita) y el dashboard a afectar
		declare @tmp_cita_servicio_dashboard table(Codigo int, CodSerIPS varchar(20), SERIPSDASH int null)
		declare @errors varchar(max) = ''
		
		declare @OrigenCirugia int,
			@Codigo varchar(20),
			@oFechaCita datetime,
			@FechaCita datetime,
			@CodigoProfesional varchar(20),
			@Profesional varchar(50),
			@NitMedico varchar(20),
			@Consultorio varchar(20),
			@TipoCita int,
			@ActividadMedica varchar(20),
			@CodigoServicio varchar(20),
			@Especialidad varchar(20),
			@Servicio varchar(20),
			@CantidadServicio int,
			@CodigoEspecialidad varchar(20),
			@AreaServicio varchar(20),
			@CentroCosto varchar(20),
			@TipoSolicitud int,
			@RequiresConfirmAppointment bit,
			@InvoiceId int,
			@InvoiceNumber varchar(20),
			@CodigoPaciente varchar(20),
			@CodigoCentroAtencion varchar(20),
			@CodigoUnidadFuncional varchar(20),
			@AdmissionNumberInvoice varchar(20),
			@IsFalseId bit = 0,
			@GeneratedId numeric(18,0),
			@HealthAdministratorIdInvoice int,
			@CareGroupIdInvoice int,
			@NombreCompletoPaciente varchar(100),
			@CUPSEntityContractDescriptionId int,
			@ActivityType int,
			@IdSala int,
			@CODACTMED char(3),
			@TypeOfScheduleActivity int,
			@TempId int   

		---------------------------------------------------------------------------------------------------------------------------------------
		-- Modificación para que se inserte el detalle de Laboratorios en la tabla de Indigo Crystal 

				Update t1 set Codserips = t2.code
				From
				@tmpServiceOrderDetail As t1 
				Inner Join Contract.CUPSEntity As t2 On t2.Id = t1.CUPSEntityId
				Where t1.codserips is null or t1.codserips = ''

				Declare @ActionResult Table(Id Int IDENTITY(1,1), StatusResult Varchar(3), MessageResult Varchar(Max), GenerateId Int)

				Declare @i As Int = 1, @lenghtTable As Int = (Select Max(RowXml) From @tmpServiceOrderDetail)	
				
				Update @tmpServiceOrderDetail Set IdCita = t.Indicador From 
				(Select @lenghtTable + ROW_NUMBER() over(order by RowXml) As Indicador, RowXml From @tmpServiceOrderDetail Where idCita is null or IdCita = '') As t
				Where [@tmpServiceOrderDetail].RowXml = t.RowXml

				Declare       @RowCita As Int
							, @DashBoard As Int
							, @UnidadFuncionalCita As Varchar(20)
							, @CodePatient As Varchar(20)
							, @CompletNamePatient As Varchar(100)
							, @CodeCenter As Varchar(20)
							, @CodeServices As Varchar(20)
							, @CodeProfessional As Varchar(20)
							, @CodeSpecialty As Varchar(20)
							, @InvoiceQuantity As Int

				IF EXISTS (SELECT 1 FROM @tmpCitasMedicas)
				BEGIN
					Select Top 1  @CodePatient = CodigoPaciente
								, @CompletNamePatient = NombreCompletoPaciente
								, @CodeCenter = CodigoCentroAtencion
								, @UnidadFuncionalCita = CodigoUnidadFuncional
								, @IsFalseId = IsFalseId
					From		  @tmpCitasMedicas
					Order by	  FechaCita Desc
				END
				ELSE
				BEGIN
					SET @CodePatient = (SELECT IPCODPACI FROM @tmpIngreso)
					SET @CompletNamePatient = (SELECT IPNOMCOMP FROM dbo.INPACIENT WHERE IPCODPACI = @CodePatient)
					SET @CodeCenter = (SELECT CODCENATE FROM @tmpIngreso)
					SET @UnidadFuncionalCita = (SELECT UFUCODIGO FROM @tmpIngreso)
				END

				--Se crea tabla variable, para guardar la informacion de los CUPS que se recorreran para evitar estar consultando en cada ciclo
				DECLARE @CupsTempInfo TABLE
				(
					Id INT NULL,
					Code VARCHAR(20) NOT NULL PRIMARY KEY,
					HandlesDescription BIT NOT NULL
				)

				--se inserta la info de los cups a facturar para consultar posteriormente
				INSERT INTO @CupsTempInfo (Id, Code, HandlesDescription)
				SELECT cups.Id,
					   cups.Code,
					   CONVERT(BIT, CASE WHEN descriptions.CUPSEntityId IS NULL THEN 0 ELSE 1 END)
				FROM Contract.CUPSEntity cups 
				JOIN
				(
					SELECT DISTINCT CODSERIPS
					FROM @tmpServiceOrderDetail
				) detail
					ON cups.Code = detail.CODSERIPS
				   AND cups.IsPanel = 0
				LEFT JOIN
				(
					SELECT descriptionsData.CUPSEntityId
					FROM Contract.CUPSEntityContractDescriptions descriptionsData 
					GROUP BY descriptionsData.CUPSEntityId
				) descriptions
					ON descriptions.CUPSEntityId = cups.Id
				
				
				DECLARE @HandlesDescription BIT

				While @i <= @lenghtTable
				Begin
					SELECT @TypeOfScheduleActivity = NULL,
						   @DashBoard = NULL,
						   @HandlesDescription = NULL,
						   @CUPSEntityContractDescriptionId = NULL

					SELECT @CodeServices = detail.CODSERIPS,
						   @CodeProfessional = detail.PerformsHealthProfessionalCode,
						   @CodeSpecialty = detail.PerformsProfessionalSpecialty,
						   @InvoiceQuantity = detail.InvoicedQuantity,
						   @RowCita = detail.IdCita,
						   @TypeOfScheduleActivity = appointment.TypeOfScheduleActivity,
						   @CUPSEntityContractDescriptionId = CASE WHEN cups.HandlesDescription = 1 THEN appointment.CUPSEntityContractDescriptionId END,
						   @HandlesDescription = cups.HandlesDescription,
						   @DashBoard = ISNULL(configuration.SERIPSDASHAMBU, configuration.SERIPSDASH)
					FROM @tmpServiceOrderDetail detail
					LEFT JOIN @CupsTempInfo cups
						ON cups.Code = detail.CODSERIPS
					LEFT JOIN dbo.INCUPSIPS configuration
						ON configuration.CODSERIPS = detail.CODSERIPS
					OUTER APPLY
					(
						SELECT TOP (1)
							   appointmentData.TypeOfScheduleActivity,
							   appointmentData.CUPSEntityContractDescriptionId
						FROM @tmpCitasMedicas appointmentData
						WHERE appointmentData.CodigoServicio = detail.CODSERIPS
						  AND appointmentData.CodeRelated = detail.IdCita
					) appointment
					WHERE detail.RowXml = @i

					IF @DashBoard = 1 AND @HandlesDescription IS NOT NULL BEGIN
						
						---- Solo se envían laboratorios cargados desde cita, de resto no debe tener interfaz con el EHR
						if EXISTS (SELECT 1 FROM @tmpCitasMedicas WHERE CodeRelated = @RowCita)
						Begin
							DECLARE @CurrentDate DATETIME = [Common].[GETDATE]()

							Delete @ActionResult
							Insert Into @ActionResult
							exec [Billing].[SP_GenerateItemDocument] 
							null
							,''
							, 1
							, @numingresSO
							, @careGroupIdSO
							, @healthAdministratorIdSO
							, @CODENTIDA
							, @CodePatient
							, @CodeServices
							, @CurrentDate
							, @CompletNamePatient
							, @CodeCenter
							, @UnidadFuncionalCita
							, NULL
							, @CodeProfessional
							, @IsFalseId
							, @RowCita
							, @CodeSpecialty
							, @careGroupIdSO,@healthAdministratorIdSO
							, ''
							, @InvoiceQuantity
							, @CUPSEntityContractDescriptionId
							, @TypeOfScheduleActivity
						End
					End
					if (select top 1 StatusResult from @ActionResult) = '999' begin					
						set @errors = @errors + (select top 1 MessageResult from @ActionResult) + CHAR(13) + CHAR(10)	 
					end	
					ELSE IF EXISTS(SELECT 1 FROM @ActionResult)  BEGIN 
						
						--select top 1 GenerateId,@RowCita from @ActionResult order by Id desc

						update @tmpCitasMedicas set GeneratedId = (select top 1 GenerateId from @ActionResult order by Id desc)
						WHERE CodeRelated = @RowCita
						
						--Se limpia la tabla variable para evaluar la siguiente iteracion
						DELETE FROM @ActionResult
					END			
					
						SET  @TypeOfScheduleActivity = NULL
					Set @i = @i + 1
				End
		--@Juan Pablo Castillo
		---------------------------------------------------------------------------------------------------------------------------------------

		declare @tmpActionResult table(Id int IDENTITY(1,1), StatusResult varchar(3), MessageResult varchar(max), GenerateId int)
		DECLARE @OwnerExternalConsultationId numeric(18,0);

		declare cursor_citas cursor local fast_forward for
		select	OrigenCirugia,
				Codigo,
				CodigoServicio,
				IsFalseId,
				FechaCita,
				InvoiceId,
				AdmissionNumberInvoice,
				CodigoPaciente,
				NombreCompletoPaciente,
				CodigoCentroAtencion,
				CodigoUnidadFuncional,
				TipoCita,
				CodigoProfesional,
				CodigoEspecialidad,
				CareGroupIdInvoice,
				HealthAdministratorIdInvoice,
				InvoiceNumber,
				CantidadServicio,
				CUPSEntityContractDescriptionId,
				ActivityType,
				IdSala,
				CODACTMED,
				Consultorio,
				TypeOfScheduleActivity,
				TempId		  
		from @tmpCitasMedicas
		open cursor_citas

		fetch next from cursor_citas into	@OrigenCirugia,
											@Codigo,
											@CodigoServicio, 
											@IsFalseId, 
											@oFechaCita,
											@InvoiceId,
											@AdmissionNumberInvoice,
											@CodigoPaciente,
											@NombreCompletoPaciente,
											@CodigoCentroAtencion,
											@CodigoUnidadFuncional,
											@TipoCita,
											@CodigoProfesional,
											@CodigoEspecialidad,
											@CareGroupIdInvoice,
											@HealthAdministratorIdInvoice,
											@InvoiceNumber,
											@CantidadServicio, 
											@CUPSEntityContractDescriptionId,
											@ActivityType,
											@IdSala,
											@CODACTMED,
											@Consultorio,
											@TypeOfScheduleActivity,
											@TempId				  
		while @@fetch_status = 0
		begin

		IF @ActivityType <> 0
		BEGIN
			/*
				SchedulingData ya no pertenece a la base owner. La cita debe
				estar creada/validada/actualizada por ERP_Services_Core contra
				Scheduling API antes de entrar a este procedimiento.
			*/
			IF @IsFalseId = 0 AND TRY_CONVERT(int, NULLIF(LTRIM(RTRIM(CONVERT(varchar(30), @Codigo))), '')) IS NULL
			BEGIN
				select '999' as CodeResult,
						CONCAT('El codigo de la cita debe estar resuelto por Scheduling API antes de generar documentos. Codigo recibido: ', ISNULL(CONVERT(varchar(30), @Codigo), '')) as MessageResult,
						'' as NotificationContract,
						'' as NumIngres
				return
			END

			update @tmpCitasMedicas set Codigo = @Codigo where TempId = @TempId

			UPDATE @tmpServiceOrderDetail SET IdCita = @Codigo
			FROM @tmpServiceOrderDetail sod
			JOIN @tmpCitasMedicas cm ON sod.IdCita = cm.CodeRelated
			WHERE cm.TempId = @TempId
		END
			

			declare @sERIPSDASH int, @codSerIpsCita varchar(20), @FlagPanel BIT =0

			

			DECLARE Panel_Cursor CURSOR LOCAL FAST_FORWARD FOR
					select ISNULL(SERIPSDASHAMBU,SERIPSDASH) , --Nuevo campo SERIPSDASHAMBU 
							CODSERIPS,
							0
					from .INCUPSIPS inc 
					JOIN Contract.CUPSEntity cups  on inc.CODSERIPS = cups.Code and cups.IsPanel=0
					where CODSERIPS = ltrim(rtrim(@CodigoServicio))

					UNION all 

					select  ISNULL(inc2.SERIPSDASHAMBU,inc2.SERIPSDASH),  --Nuevo campo SERIPSDASHAMBU 
							inc2.CODSERIPS,
							1
					from .INCUPSIPS inc 
					join Contract.CUPSEntity cups  on inc.CODSERIPS = cups.Code and cups.IsPanel=1
					join Contract.CUPSEntityPanelDetail cpd  ON cups.Id = cpd.CUPSEntityPanelId
					JOIN Contract.CUPSEntity cups2  ON cups2.Id =cpd.CUPSEntityId
					join .INCUPSIPS inc2  on inc2.CODSERIPS = cups2.Code
					where inc.CODSERIPS = ltrim(rtrim(@CodigoServicio))
  

			OPEN Panel_Cursor  
  
				FETCH NEXT FROM Panel_Cursor   
				INTO @sERIPSDASH,@codSerIpsCita,@FlagPanel	
  
			WHILE @@FETCH_STATUS = 0  
			BEGIN  
				/*****desde aqui*********/
				
				if @sERIPSDASH is null Or @sERIPSDASH = 0 begin
					set @errors = @errors + 'No se encontró que DASHBOARD afectar para el servicio ' + @codSerIpsCita + CHAR(13) + CHAR(10)
				end
				else begin

					insert into @tmp_cita_servicio_dashboard values (@Codigo, @codSerIpsCita, @sERIPSDASH)	

					if @IsFalseId = 1 begin
						set @FechaCita = [Common].[GETDATE]()
					end
					else begin
						set @FechaCita = @oFechaCita
					end
				
					DELETE FROM @tmpActionResult
					SET @OwnerExternalConsultationId = NULL;
				
					if @sERIPSDASH = 1 AND @FlagPanel =1 begin
						--Laboratorios
						insert into @tmpActionResult
						exec [Billing].[SP_GenerateItemDocument] @InvoiceId,@AdmissionNumberInvoice,1,@numingresSO,@careGroupIdSO,@healthAdministratorIdSO,
							@CODENTIDA,@CodigoPaciente,@codSerIpsCita,@FechaCita,@NombreCompletoPaciente,@CodigoCentroAtencion,@CodigoUnidadFuncional,
							@TipoCita,@CodigoProfesional,@IsFalseId,@Codigo,@CodigoEspecialidad,@CareGroupIdInvoice,@HealthAdministratorIdInvoice,
							@InvoiceNumber,@CantidadServicio,@CUPSEntityContractDescriptionId, @TypeOfScheduleActivity, @OwnerExternalConsultationId OUTPUT
					end
					If @sERIPSDASH = 2 begin
						--Patologias
						insert into @tmpActionResult
						exec [Billing].[SP_GenerateItemDocument] @InvoiceId,@AdmissionNumberInvoice,2,@numingresSO,@careGroupIdSO,@healthAdministratorIdSO,
							@CODENTIDA,@CodigoPaciente,@codSerIpsCita,@FechaCita,@NombreCompletoPaciente,@CodigoCentroAtencion,@CodigoUnidadFuncional,
							@TipoCita,@CodigoProfesional,@IsFalseId,@Codigo,@CodigoEspecialidad,@CareGroupIdInvoice,@HealthAdministratorIdInvoice,
							@InvoiceNumber,@CantidadServicio, @CUPSEntityContractDescriptionId, @TypeOfScheduleActivity, @OwnerExternalConsultationId OUTPUT
					end
					else if @sERIPSDASH = 3 begin
						--Imagenes
						insert into @tmpActionResult
						exec [Billing].[SP_GenerateItemDocument] @InvoiceId,@AdmissionNumberInvoice,3,@numingresSO,@careGroupIdSO,@healthAdministratorIdSO,
							@CODENTIDA,@CodigoPaciente,@codSerIpsCita,@FechaCita,@NombreCompletoPaciente,@CodigoCentroAtencion,@CodigoUnidadFuncional,
							@TipoCita,@CodigoProfesional,@IsFalseId,@Codigo,@CodigoEspecialidad,@CareGroupIdInvoice,@HealthAdministratorIdInvoice,
							@InvoiceNumber,@CantidadServicio, @CUPSEntityContractDescriptionId, @TypeOfScheduleActivity, @OwnerExternalConsultationId OUTPUT
						-- se guardan los Ids para hacer la posterior actualizacion del Campo GENSERVICEORDER de AMBORDIMA
						INSERT INTO @IdsAMBORDIMA
						SELECT GenerateId
						FROM @tmpActionResult
					end
					else if @sERIPSDASH = 4 begin
						--Consulta Externa
						-- Opt-in only for callers producing a derived Scheduling creation event.
						DECLARE @PendingPanelAppointment bit = CASE
							WHEN @SchedulingOwnerFeedbackEnabled = 1 AND @FlagPanel = 1
							AND EXISTS (SELECT 1 FROM @PendingOwnerRows WHERE RowNumber = @TempId AND HealthcareServiceCode = @codSerIpsCita)
							THEN 1 ELSE @IsFalseId END;
						insert into @tmpActionResult
						exec [Billing].[SP_GenerateItemDocument] @InvoiceId,@AdmissionNumberInvoice,4,@numingresSO,@careGroupIdSO,@healthAdministratorIdSO,
							@CODENTIDA,@CodigoPaciente,@codSerIpsCita,@FechaCita,@NombreCompletoPaciente,@CodigoCentroAtencion,@CodigoUnidadFuncional,
							@TipoCita,@CodigoProfesional,@PendingPanelAppointment,@Codigo,@CodigoEspecialidad,@CareGroupIdInvoice,@HealthAdministratorIdInvoice,
							@InvoiceNumber,@CantidadServicio, @CUPSEntityContractDescriptionId, @TypeOfScheduleActivity, @OwnerExternalConsultationId OUTPUT
					end
					else if @sERIPSDASH in (5, 6, 7, 8, 9, 10, 11, 12) begin
						--5. Quimioterapias, 6. Radioterapias, 7. Diálisis, 8. Ninguno, 9. Procedimiento no Qx, 10. Procedimiento Qx, 11. Interconsultas, 12. Otros Procedimientos
						insert into @tmpActionResult
						exec [Billing].[SP_GenerateItemDocument] @InvoiceId,@AdmissionNumberInvoice,@sERIPSDASH,@numingresSO,@careGroupIdSO,@healthAdministratorIdSO,
							@CODENTIDA,@CodigoPaciente,@codSerIpsCita,@FechaCita,@NombreCompletoPaciente,@CodigoCentroAtencion,@CodigoUnidadFuncional,
							@TipoCita,@CodigoProfesional,@IsFalseId,@Codigo,@CodigoEspecialidad,@CareGroupIdInvoice,@HealthAdministratorIdInvoice,
							@InvoiceNumber,@CantidadServicio, @CUPSEntityContractDescriptionId, @TypeOfScheduleActivity, @OwnerExternalConsultationId OUTPUT
					end
					else begin
						insert into @tmpActionResult values('0', '', 0)
					end

					if (select count(*) from @tmpActionResult) = 0 Or (select top 1 StatusResult from @tmpActionResult) = '999' begin					
						set @errors = @errors + ISNULL((select top 1 MessageResult + CHAR(13) + CHAR(10) from @tmpActionResult), '')
					end
					else begin
						IF @OwnerExternalConsultationId > 0
						BEGIN
							INSERT INTO @SchedulingOwnerReferences
							VALUES (@TempId, @codSerIpsCita, @OwnerExternalConsultationId,
								CASE WHEN @IsFalseId = 0 THEN TRY_CONVERT(int, @Codigo) END);
						END
						IF @sERIPSDASH = 12 AND @TypeOfScheduleActivity <> 5
						BEGIN
							IF EXISTS
							(
								SELECT 1
								FROM dbo.HCCUPSXCA
								WHERE CODCENATE = @CodigoCentroAtencion AND CODSERIPS = @codSerIpsCita
							)
							BEGIN
								declare @idCitaAmbor int = TRY_CONVERT(int, NULLIF(LTRIM(RTRIM(CONVERT(varchar(30), @Codigo))), ''))
								declare @generateId int = (select top 1 GenerateId from @tmpActionResult order by Id desc)

							
								INSERT INTO dbo.AMBORDOTROSPRO
								(
									IDCITA, ESTADO, NUMINGRES, CODCENATE, UFUCODIGO, FECHAREG, CODSERIPS, CANTIDAD, IDDESCRIPCIONRELACIONADA, GENCAREGROUP, GENCONENTITY,
									GENINVOICE, GENINVOICEID, GENSERVICEORDER, CODPROSAL, CODESPECI, IPCODPACI
								)
								values (@idCitaAmbor, 1, @numingresSO, @CodigoCentroAtencion, @CodigoUnidadFuncional, [Common].[GETDATE](), @CodigoServicio, IIF(@CantidadServicio = 0, 1, @CantidadServicio), @CUPSEntityContractDescriptionId, @careGroupIdSO, @healthAdministratorIdSO,
									@InvoiceNumber, @InvoiceId, @generateId, IIF(@CodigoProfesional = '', NULL, @CodigoProfesional), IIF(@CodigoEspecialidad = '', NULL, @CodigoEspecialidad), @patientCodeSO)
							
							END
						END

						--Si el item es de tipo cirugia
						if @OrigenCirugia = 1 begin
							--Se actualiza el ingreso
							update .ADINGRESO set PACIENTESITIOQX = 1 where NUMINGRES = @numingresSO

							-- Scheduling API actualiza el estado de la cirugia principal e hijas.
						end
						else begin --Si es de otro tipo

							--Actualizar el generateId
							update @tmpCitasMedicas set GeneratedId = (select top 1 GenerateId from @tmpActionResult order by Id desc)
							where TempId = @TempId AND COALESCE( GeneratedId ,0) =0
																														  
							-- Scheduling API marca GeneratedServiceOrder para citas reales.
						end
					end
				end
				/*****Hasta aqui********/					
				FETCH NEXT FROM Panel_Cursor   
				INTO @sERIPSDASH,@codSerIpsCita,@FlagPanel
			END   
			CLOSE Panel_Cursor;  
			DEALLOCATE Panel_Cursor;

			fetch next from cursor_citas into @OrigenCirugia, @Codigo, @CodigoServicio, @IsFalseId, @oFechaCita,@InvoiceId,@AdmissionNumberInvoice,
			@CodigoPaciente,@NombreCompletoPaciente,@CodigoCentroAtencion,@CodigoUnidadFuncional,@TipoCita,@CodigoProfesional,
			@CodigoEspecialidad,@CareGroupIdInvoice,@HealthAdministratorIdInvoice,@InvoiceNumber,@CantidadServicio, @CUPSEntityContractDescriptionId,@ActivityType,
			@IdSala,@CODACTMED,@Consultorio,@TypeOfScheduleActivity,@TempId
		end
		close cursor_citas
		deallocate cursor_citas

		if @errors <> '' And ltrim(rtrim(@errors)) <> '' begin
			select '999' as CodeResult, @errors as MessageResult, '' as NotificationContract, '' as NumIngres
			return
		end

		if (select count(*) from @tmpServiceOrderDetail) > 0 begin			
			declare @TableServiceOrder table(Id int not NULL,
				Code varchar(20) NOT NULL,
				AdmissionNumber varchar(20), 
				PatientCode varchar(15) NOT NULL,
				OrderDate datetime NOT NULL,
				AffectInventory bit,
				EntityCode varchar(20) NULL, 
				EntityId int NULL, 
				EntityName varchar(250) NULL,
				OperatingUnitId int NOT NULL,
				[Status] tinyint NOT NULL)
			
			declare @TableServiceOrderDetail table(ServiceOrderId int,
				RowXml int not null, 
				CareGroupId int NOT NULL,
				HealthAdministratorId int NULL,
				ThirdPartyId int NULL,
				ServiceType tinyint NOT NULL,
				RecordType tinyint NOT NULL,
				CUPSEntityId int NULL,
				IPSServiceId int NULL,
				HospitalStayId int NULL,
				HospitalStayDetailId int NULL,
				ControlExternalConsultation tinyint NULL,
				ControlExternalConsultationCode numeric(18, 0) NULL,
				CUPSAssociateService bit NOT NULL,
				CodeAssociateService varchar(50) NULL,
				IsPackage bit NOT NULL,
				Packaging bit NOT NULL,
				PackageServiceOrderDetailId int NULL,
				LiquidationType tinyint NOT NULL,
				Presentation tinyint NULL,
				ProductId int NULL,
				InvoicedQuantity int NOT NULL,
				SupplyQuantity int NOT NULL,
				DevolutionQuantity int NOT NULL,
				RateManualSalePrice numeric(18, 0) NOT NULL,
				CostValue numeric(18, 2) NOT NULL,
				ServiceDate datetime NOT NULL,
				AuthorizationNumber varchar(20) NULL,
				PerformsFunctionalUnitId int NOT NULL,
				PerformsHealthProfessionalCode char(20) NULL,
				PerformsProfessionalSpecialty char(3) NULL,
				PerformsHealthProfessionalThirdPartyId int NULL,
				BillingConceptId int NULL,
				CostCenterId int NOT NULL,
				SettlementType tinyint NOT NULL,
				IncludeServiceOrderDetailId int NULL,
				RecoveryRatio numeric(5, 2) NULL,
				RateManualId int NULL,
				RateManualType tinyint NULL,
				RateManualDetailId int NULL,
				DefinitionRateDetailId int NULL,
				DefinitionRateDetailConditionId int NULL,
				SubTotalSalesPrice numeric(18, 2) NOT NULL,
				ThirdPartyDiscount numeric(18, 2) NOT NULL,
				ThirdPartyDiscountPercentage numeric(5, 2) NOT NULL,
				TotalSalesPrice numeric(18, 2) NOT NULL,
				GrandTotalSalesPrice numeric(18, 2) NOT NULL,
				SurchargeApply bit NOT NULL,
				SurgicalInterventionType tinyint NULL,
				SurgeryNumber tinyint NOT NULL,
				IsFirstEvent bit NOT NULL,
				IsAnnulled bit NOT NULL,
				IsDelete bit NOT NULL,
				IncomeMainAccountId int NOT NULL,
				ApplyRIAS varchar(20) NULL,
				RIASCupsId int NULL,
				RealizedQuantity int NULL,
				CUPSEntityContractDescriptionId int NULL,
				QuotationServiceOrderDetailId int null,
				TraceabilityPaperworkEventsId int null, 
				IsServiceOrderDetailControlJustify BIT NOT NULL, 
				ServiceOrderDetailControlJustification VARCHAR(500),
				FinalProductCost DECIMAL(20,2) NOT NULL, 
				GrossValue NUMERIC(20,2), 
				TaxValue NUMERIC(20,2)
			)

			--- inserto la cabecera de la orden de servicio tmp
			insert into @TableServiceOrder values (0, '', @numingresSO, @patientCodeSO, [Common].[GETDATE](), 0,
				null, null, 'ControlOutPatientServices', @OperatingUnitId, 1)

			UPDATE t1 SET t1.ControlExternalConsultation = (
				SELECT TOP 1
					CASE
						WHEN c.TypeOfScheduleActivity = 5 THEN 8
						ELSE SERIPSDASH
					END
				FROM @tmp_cita_servicio_dashboard WHERE Codigo = c.Codigo AND CodSerIPS = t1.CODSERIPS
				), 
				t1.ControlExternalConsultationCode = c.GeneratedId
			FROM @tmpServiceOrderDetail AS t1
			INNER JOIN @tmpCitasMedicas c ON c.Codigo = t1.IdCita AND c.CodigoServicio = t1.CODSERIPS
	  

			--- Ahora inserto los detalles de la orden de servicio
			insert into @TableServiceOrderDetail (ServiceOrderId, RowXml, CareGroupId,HealthAdministratorId,ThirdPartyId,ServiceType,RecordType,CUPSEntityId,IPSServiceId,HospitalStayId ,HospitalStayDetailId
			,ControlExternalConsultation,ControlExternalConsultationCode,CUPSAssociateService,CodeAssociateService,IsPackage,Packaging,PackageServiceOrderDetailId
			,LiquidationType,Presentation,ProductId,InvoicedQuantity,SupplyQuantity,DevolutionQuantity,RateManualSalePrice,CostValue
			,ServiceDate,AuthorizationNumber,PerformsFunctionalUnitId,PerformsHealthProfessionalCode,PerformsProfessionalSpecialty
			,PerformsHealthProfessionalThirdPartyId,BillingConceptId,CostCenterId
			,SettlementType,IncludeServiceOrderDetailId,RecoveryRatio,RateManualId,RateManualType,RateManualDetailId,DefinitionRateDetailId,DefinitionRateDetailConditionId
			,SubTotalSalesPrice,ThirdPartyDiscount,ThirdPartyDiscountPercentage,TotalSalesPrice,GrandTotalSalesPrice,SurchargeApply,SurgicalInterventionType,SurgeryNumber,IsFirstEvent,IsAnnulled,IsDelete,IncomeMainAccountId,
			ApplyRIAS, RIASCupsId, RealizedQuantity, CUPSEntityContractDescriptionId, QuotationServiceOrderDetailId, TraceabilityPaperworkEventsId, IsServiceOrderDetailControlJustify, ServiceOrderDetailControlJustification, FinalProductCost, GrossValue, TaxValue)
			select 0, RowXml, CareGroupId,HealthAdministratorId,ThirdPartyId,ServiceType,RecordType,CUPSEntityId,IPSServiceId,HospitalStayId ,HospitalStayDetailId
			,ControlExternalConsultation,ControlExternalConsultationCode,CUPSAssociateService,CodeAssociateService,IsPackage,Packaging,PackageServiceOrderDetailId
			,LiquidationType,Presentation,ProductId,InvoicedQuantity,SupplyQuantity,DevolutionQuantity,RateManualSalePrice,CostValue
			,ServiceDate,AuthorizationNumber,PerformsFunctionalUnitId,PerformsHealthProfessionalCode,PerformsProfessionalSpecialty
			,PerformsHealthProfessionalThirdPartyId,BillingConceptId,CostCenterId
			,SettlementType,IncludeServiceOrderDetailId,RecoveryRatio,RateManualId,RateManualType,RateManualDetailId,DefinitionRateDetailId,DefinitionRateDetailConditionId
			,SubTotalSalesPrice,ThirdPartyDiscount,ThirdPartyDiscountPercentage,TotalSalesPrice,GrandTotalSalesPrice,SurchargeApply,SurgicalInterventionType,SurgeryNumber,IsFirstEvent,IsAnnulled,IsDelete,IncomeMainAccountId,
			ApplyRIAS, RIASCupsId, RealizedQuantity, CUPSEntityContractDescriptionId, QuotationServiceOrderDetailId, TraceabilityPaperworkEventsId, IsServiceOrderDetailControlJustify, ServiceOrderDetailControlJustification,
			FinalProductCost, GrossValue, TaxValue
			from @tmpServiceOrderDetail

			--Xml para ordenes de servicio
			declare @OrderXml xml
			
			--- Realizo la interfaz con Ordenes de servicios -- Facturacion
			if(select count(*) from @tmpServiceOrderDetailSurgical) > 0 --Se valida si viene con detalles qx
			begin
				set @OrderXml = (
				select  ServiceOrder.Id, ServiceOrder.Code, ServiceOrder.AdmissionNumber, ServiceOrder.PatientCode, ServiceOrder.OrderDate, ServiceOrder.AffectInventory, ServiceOrder.EntityCode, ServiceOrder.EntityId, ServiceOrder.EntityName, ServiceOrder.OperatingUnitId, ServiceOrder.Status
				,ServiceOrderDetail.RowXml, 0 as Id, ServiceOrderDetail.CareGroupId, ServiceOrderDetail.HealthAdministratorId, ServiceOrderDetail.ThirdPartyId, ServiceOrderDetail.ServiceType, ServiceOrderDetail.RecordType, ServiceOrderDetail.CUPSEntityId, ServiceOrderDetail.IPSServiceId, ServiceOrderDetail.HospitalStayId, ServiceOrderDetail.HospitalStayDetailId
				,ServiceOrderDetail.ControlExternalConsultation, ServiceOrderDetail.ControlExternalConsultationCode, ServiceOrderDetail.CUPSAssociateService, ServiceOrderDetail.CodeAssociateService, ServiceOrderDetail.IsPackage, ServiceOrderDetail.Packaging, ServiceOrderDetail.PackageServiceOrderDetailId, ServiceOrderDetail.LiquidationType
				,ServiceOrderDetail.Presentation, ServiceOrderDetail.ProductId, ServiceOrderDetail.InvoicedQuantity, ServiceOrderDetail.SupplyQuantity, ServiceOrderDetail.DevolutionQuantity, ServiceOrderDetail.RateManualSalePrice, ServiceOrderDetail.CostValue, ServiceOrderDetail.ServiceDate, ServiceOrderDetail.AuthorizationNumber, ServiceOrderDetail.PerformsFunctionalUnitId
				,ServiceOrderDetail.PerformsHealthProfessionalCode, ServiceOrderDetail.PerformsProfessionalSpecialty, ServiceOrderDetail.PerformsHealthProfessionalThirdPartyId, ServiceOrderDetail.BillingConceptId, ServiceOrderDetail.CostCenterId, ServiceOrderDetail.SettlementType, ServiceOrderDetail.IncludeServiceOrderDetailId, ServiceOrderDetail.RecoveryRatio
				,ServiceOrderDetail.RateManualId, ServiceOrderDetail.RateManualType, ServiceOrderDetail.RateManualDetailId, ServiceOrderDetail.DefinitionRateDetailId, ServiceOrderDetail.DefinitionRateDetailConditionId, ServiceOrderDetail.SubTotalSalesPrice, ServiceOrderDetail.ThirdPartyDiscount, ServiceOrderDetail.ThirdPartyDiscountPercentage
				,ServiceOrderDetail.TotalSalesPrice, ServiceOrderDetail.GrandTotalSalesPrice, ServiceOrderDetail.SurchargeApply, ServiceOrderDetail.SurgicalInterventionType, ServiceOrderDetail.SurgeryNumber, ServiceOrderDetail.IsFirstEvent, ServiceOrderDetail.IsAnnulled, ServiceOrderDetail.IsDelete, ServiceOrderDetail.IncomeMainAccountId, 'Added' as EntityState, 
				ServiceOrderDetail.ApplyRIAS, ServiceOrderDetail.RIASCupsId, ServiceOrderDetail.RealizedQuantity, ServiceOrderDetail.CUPSEntityContractDescriptionId, ServiceOrderDetail.QuotationServiceOrderDetailId, ServiceOrderDetail.TraceabilityPaperworkEventsId
				,ServiceOrderDetailSurgical.Id, ServiceOrderDetailSurgical.ServiceOrderDetailIdRow, ServiceOrderDetailSurgical.ServiceOrderDetailId, ServiceOrderDetailSurgical.IPSServiceId, ServiceOrderDetailSurgical.InvoicedQuantity, ServiceOrderDetailSurgical.LiquidationPercentage, ServiceOrderDetailSurgical.RateManualSalePrice, ServiceOrderDetailSurgical.TotalSalesPrice
				,ServiceOrderDetailSurgical.PerformsHealthProfessionalCode, ServiceOrderDetailSurgical.PerformsHealthProfessionalThirdPartyId, ServiceOrderDetailSurgical.CostValue, ServiceOrderDetailSurgical.BillingConceptId, ServiceOrderDetailSurgical.CostCenterId, ServiceOrderDetailSurgical.RateManualDetailSurgicalId, ServiceOrderDetailSurgical.SurchargeApply
				,ServiceOrderDetailSurgical.OnlyMedicalFees, ServiceOrderDetailSurgical.IncomeMainAccountId, ServiceOrderDetailSurgical.EntityState,ServiceOrderDetail.FinalProductCost,ServiceOrderDetail.GrossValue,ServiceOrderDetail.TaxValue
				from @TableServiceOrder as ServiceOrder
				inner join @TableServiceOrderDetail as ServiceOrderDetail on ServiceOrderDetail.ServiceOrderId = ServiceOrder.Id
				left join @tmpServiceOrderDetailSurgical as ServiceOrderDetailSurgical on ServiceOrderDetailSurgical.ServiceOrderDetailIdRow = ServiceOrderDetail.RowXml
				for xml auto, elements)
			end
			else begin --Si no viene con detalles qx
				set @OrderXml = (
				select  ServiceOrder.Id, ServiceOrder.Code, ServiceOrder.AdmissionNumber, ServiceOrder.PatientCode, ServiceOrder.OrderDate, ServiceOrder.AffectInventory, ServiceOrder.EntityCode, ServiceOrder.EntityId, ServiceOrder.EntityName, ServiceOrder.OperatingUnitId, ServiceOrder.Status
				,ServiceOrderDetail.RowXml, 0 as Id, ServiceOrderDetail.CareGroupId, ServiceOrderDetail.HealthAdministratorId, ServiceOrderDetail.ThirdPartyId, ServiceOrderDetail.ServiceType, ServiceOrderDetail.RecordType, ServiceOrderDetail.CUPSEntityId, ServiceOrderDetail.IPSServiceId, ServiceOrderDetail.HospitalStayId, ServiceOrderDetail.HospitalStayDetailId
				,ServiceOrderDetail.ControlExternalConsultation, ServiceOrderDetail.ControlExternalConsultationCode, ServiceOrderDetail.CUPSAssociateService, ServiceOrderDetail.CodeAssociateService, ServiceOrderDetail.IsPackage, ServiceOrderDetail.Packaging, ServiceOrderDetail.PackageServiceOrderDetailId, ServiceOrderDetail.LiquidationType
				,ServiceOrderDetail.Presentation, ServiceOrderDetail.ProductId, ServiceOrderDetail.InvoicedQuantity, ServiceOrderDetail.SupplyQuantity, ServiceOrderDetail.DevolutionQuantity, ServiceOrderDetail.RateManualSalePrice, ServiceOrderDetail.CostValue, ServiceOrderDetail.ServiceDate, ServiceOrderDetail.AuthorizationNumber, ServiceOrderDetail.PerformsFunctionalUnitId
				,ServiceOrderDetail.PerformsHealthProfessionalCode, ServiceOrderDetail.PerformsProfessionalSpecialty, ServiceOrderDetail.PerformsHealthProfessionalThirdPartyId, ServiceOrderDetail.BillingConceptId, ServiceOrderDetail.CostCenterId, ServiceOrderDetail.SettlementType, ServiceOrderDetail.IncludeServiceOrderDetailId, ServiceOrderDetail.RecoveryRatio
				,ServiceOrderDetail.RateManualId, ServiceOrderDetail.RateManualType, ServiceOrderDetail.RateManualDetailId, ServiceOrderDetail.DefinitionRateDetailId, ServiceOrderDetail.DefinitionRateDetailConditionId, ServiceOrderDetail.SubTotalSalesPrice, ServiceOrderDetail.ThirdPartyDiscount, ServiceOrderDetail.ThirdPartyDiscountPercentage
				,ServiceOrderDetail.TotalSalesPrice, ServiceOrderDetail.GrandTotalSalesPrice, ServiceOrderDetail.SurchargeApply, ServiceOrderDetail.SurgicalInterventionType, ServiceOrderDetail.SurgeryNumber, ServiceOrderDetail.IsFirstEvent, ServiceOrderDetail.IsAnnulled, ServiceOrderDetail.IsDelete, ServiceOrderDetail.IncomeMainAccountId, 'Added' as EntityState,
				ServiceOrderDetail.ApplyRIAS, ServiceOrderDetail.RIASCupsId, ServiceOrderDetail.RealizedQuantity, ServiceOrderDetail.CUPSEntityContractDescriptionId, ServiceOrderDetail.QuotationServiceOrderDetailId, ServiceOrderDetail.TraceabilityPaperworkEventsId, ServiceOrderDetail.IsServiceOrderDetailControlJustify, ServiceOrderDetail.ServiceOrderDetailControlJustification,
				ServiceOrderDetail.FinalProductCost,ServiceOrderDetail.GrossValue,ServiceOrderDetail.TaxValue
				from @TableServiceOrder as ServiceOrder
				inner join @TableServiceOrderDetail as ServiceOrderDetail on ServiceOrderDetail.ServiceOrderId = ServiceOrder.Id
				for xml auto, elements)
			end

			declare @TableResultOrder table(CodeMessage varchar(20), [Message] varchar(max), ServiceOrderId int, [Status] tinyint)
			
			
			
				

			insert into @TableResultOrder
			exec [Billing].[SP_GenerateServiceOrder] @OrderXml, @User
				
			if(select count(*) from @TableResultOrder where Status = 3) > 0 begin
				select CodeMessage as CodeResult, Message as MessageResult, '' as NotificationContract, '' as NumIngres from @TableResultOrder
				return
			end
			set @stringResult = @stringResult + (select [Message] from @TableResultOrder where Status = 1) + CHAR(13)+CHAR(10)
		end

		declare @ServiceOrderId as int
		SELECT top 1 @ServiceOrderId = ServiceOrderId from @TableResultOrder

		/*Se actualiza la TABLA AMBORDIMA si existen Ids en la tabla temp IdsAMBORDIMA para asignarles el id de la orden de servicio*/
		if EXISTS(SELECT 1 FROM @IdsAMBORDIMA) BEGIN
			
			UPDATE a
			SET a.GENSERVICEORDER = @ServiceOrderId,
				a.IdServerOrderDetail = sod.id
			FROM AMBORDIMA a
			JOIN @IdsAMBORDIMA i on a.AUTO = i.Id
			left join Contract.CUPSEntity ce on a.CODSERIPS = ce.Code
			left join Billing.ServiceOrderDetail sod on sod.CUPSEntityId = ce.Id
			where sod.ServiceOrderId = @ServiceOrderId
		END

		/* Actualiza el ID del detalle de la orden de servicio según el tipo de servicio:
		1: Laboratorios, 2: Patologías, 3: Imágenes, 4: Proc. no Qx, 5: Proc. Qx,
		6: Interconsultas, 7: Ninguno, 8: Consulta Externa */
		IF EXISTS (
				SELECT 1
				FROM @tmpCitasMedicas AS tcm
				INNER JOIN Contract.CUPSEntity ce ON tcm.CodigoServicio = ce.Code
				WHERE ce.ServiceType = 1
			) 
			BEGIN
				DECLARE @admissionnumber varchar(50) = IIF(@InvoiceId > 0, @AdmissionNumberInvoice, @numingresSO);

				UPDATE a
				SET a.GENSERVICEORDER = @ServiceOrderId,
					a.IdServerOrderDetail = sod.id
				FROM AMBORDLAB a
				INNER JOIN Contract.CUPSEntity ce ON a.CODSERIPS = ce.Code
				INNER JOIN Billing.ServiceOrderDetail sod ON sod.CUPSEntityId = ce.Id
				WHERE sod.ServiceOrderId = @ServiceOrderId
				AND a.NUMINGRES = @admissionnumber
				AND  ce.ServiceType = 1
			END

		IF (SELECT COUNT(1) FROM @Hemocomponent)>0 BEGIN
			SELECT @CODCENATE = CODCENATE, @UFUCODIGO = UFUCODIGO FROM @tmpIngreso
			
			--alter table HCORHEMCO add HCCOMSANID int null
			INSERT INTO dbo.HCORHEMCO(AUTOENVP,PRIOTRAN,RESERCIR,TRANPREV,REACTRAN, AOEMBPRE, AOERIFE,IDETIPHIS,NUMEFOLIO,IPCODPACI,NUMINGRES,CODCENATE,FECORDMED,MANEXTPRO,SOLEXTRAMU,HCCOMSANID,idAGASICITA)
			OUTPUT INSERTED.ID, INSERTED.HCCOMSANID INTO @HCORHEMCO_HCCOMSAN
			select 0, 4, 0,0,0, 0, 0,'','',@patientCodeSO,@numingresSO,@CODCENATE,[Common].[GETDATE](),0,1,H.Id,H.IdAGASICITA
			from @Hemocomponent H
			GROUP by H.Id,H.IdAGASICITA

			--alter table HCORHEMBOL add TEMPID int null
			INSERT INTO dbo.HCORHEMBOL(HCORHEMCOID, COMSAMID, ESTADO, TIPOSOLICI, PENCONRES, AUTENTREGA, ESDEVOLUTIVO, FECSOLRES, FECSOLTRA, PROFSOLRES, PROFSOLTRA, UFUSOLRES, UFUSOLTRA, PRUEBACRUZADA, TEMPID,VolumeTransfuse)
			OUTPUT INSERTED.ID, INSERTED.HCORHEMCOID, INSERTED.TEMPID INTO @HCORHEMBOL_HEMODETAIL
			select HH.HCORHEMCOID, H.Id, 1, 1, 0, 0, 0, [Common].[GETDATE](), [Common].[GETDATE](), H.ProfessionalId, H.ProfessionalId, @UFUCODIGO, @UFUCODIGO, 1, H.IdAuto,H.VolumenComponent
			from @Hemocomponent H
			INNER JOIN @HCORHEMCO_HCCOMSAN HH ON HH.HCCOMSANID = H.Id

			INSERT INTO dbo.HCORHEMSER(HCORHEMBOLID,HCORHEMCOID,CODSERIPS,ESTADO,TIPOSERVICIO,GENORDSER)
			SELECT HHD.HCORHEMBOLID, HHD.HCORHEMCOID, HD.CodeServiceIPS, 1, 2, 0
			FROM  @HCORHEMCO_HCCOMSAN HH
			INNER JOIN @HCORHEMBOL_HEMODETAIL HHD ON HHD.HCORHEMCOID = HH.HCORHEMCOID
			INNER JOIN @Hemocomponent H ON H.IdAuto =HHD.HDIdAuto
			INNER JOIN @HemocomponentDetail HD	ON HD.IdAutoC = H.IdAuto
			WHERE HD.SERRASANTI=0

			INSERT INTO dbo.HCORHEMSER(HCORHEMCOID,CODSERIPS,ESTADO,TIPOSERVICIO,GENORDSER)
			SELECT HH.HCORHEMCOID, HD.CodeServiceIPS, 1, 1, 0
			FROM  @HCORHEMCO_HCCOMSAN HH
			INNER JOIN @HemocomponentDetail HD
			ON HD.HemocomponentId = HH.HCCOMSANID
			AND HD.SERRASANTI = 1

			INSERT INTO  HCORHEMCUPS(HCORHEMCOID,HCORHEMBOLID,IDHCCOMSAN,CODSERIPS)
			SELECT	HHD.HCORHEMCOID,
					HHD.HCORHEMBOLID,
					HH.HCCOMSANID,
					HD.CodeServiceIPS
			FROM  @HCORHEMCO_HCCOMSAN HH
			INNER JOIN @HCORHEMBOL_HEMODETAIL HHD ON HHD.HCORHEMCOID = HH.HCORHEMCOID
			INNER JOIN @Hemocomponent H ON H.IdAuto =HHD.HDIdAuto
			INNER JOIN @HemocomponentDetail HD	ON HD.IdAutoC = H.IdAuto	
			
			/* Scheduling API marca GeneratedServiceOrder para las citas de hemocomponentes. */

			INSERT INTO dbo.ADCONCOEX (	IPCODPACI,--1
										IPFECHACO,--2
										IPFECHCIT,--3
										CONESTADO,--4
										IPNOMCOMP,--5
										CODCENATE,--6
										UFUCODIGO,--7
										NUMINGRES,--8
										INDAUDFOR,--9
										LIQUIDAR,--10
										NUMCONCIT)--11
			SELECT	@patientCodeSO,		--1
					[Common].[GETDATE](),--2
					[Common].[GETDATE](),--3
					1,					--4
					@CompletNamePatient,--5
					@CODCENATE,			--6
					@UFUCODIGO,			--7
					@numingresSO,		--8
					1,					--9
					1,					--10
					h.IdAGASICITA		--11
			FROM  (SELECT h.IdAGASICITA
					FROM @Hemocomponent h
					where h.IdAGASICITA >0
					GROUP by h.IdAGASICITA) h
			 

		END
		
		SET @SchedulingOwnerReferencesJson = (SELECT * FROM @SchedulingOwnerReferences FOR JSON PATH);
		select '0' as CodeResult, @stringResult as MessageResult, @messageNotificationContract as NotificationContract, @numingresSO as NumIngres
	end try
	begin catch
		IF CURSOR_STATUS('local','Panel_Cursor') >= -1
		BEGIN
			IF CURSOR_STATUS('local','Panel_Cursor') > -1
				CLOSE Panel_Cursor
			DEALLOCATE Panel_Cursor
		END

		IF CURSOR_STATUS('local','cursor_citas') >= -1
		BEGIN
			IF CURSOR_STATUS('local','cursor_citas') > -1
				CLOSE cursor_citas
			DEALLOCATE cursor_citas
		END
		select '999' as CodeResult, error_message() as MessageResult, '' as NotificationContract, '' as NumIngres
	end catch

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento owner/Billing que genera documentos de control para servicios ambulatorios en facturación. Recibe las referencias de cita ya resueltas por el servicio ERP mediante Scheduling API y persiste documentos, órdenes, ingresos y trazabilidad local sin consultar ni mutar tablas SchedulingData.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateDocuments';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateDocuments';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Procesa un XML de control de servicios ambulatorios para crear/actualizar el ingreso del paciente, generar ítems clínicos por dashboard, la orden de servicio de facturación y, si aplica, la solicitud de hemocomponentes. La creación/actualización de citas queda fuera del SP y se realiza por Scheduling API desde el servicio ERP.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateDocuments';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Si @careGroupId>0, debe existir el CareGroup con un Contract y un ContractDetail con ValidRecord=1; en caso contrario se retorna código ''999''.; El contrato asociado no debe estar en estado Suspendido (=2) ni Terminado (=3).; Cuando se crea un nuevo ingreso y CareGroupType=3, debe existir en INENTIDAD una entidad con CODIGONIT=''000000000000999'' y un HealthAdministrator con Code=''999''.; Para CareGroupType distinto de 3 al crear ingreso, debe existir un ThirdParty asociado al HealthAdministrator (ThirdPartyId<>0) y la entidad debe encontrarse en INENTIDAD por CODENTIDA o por CODIGONIT del NIT.; Si @ActivityType<>0 y no es IsFalseId, @Codigo debe venir resuelto por Scheduling API como AppointmentId numerico.; Cada CODSERIPS debe tener configurado SERIPSDASH/SERIPSDASHAMBU en INCUPSIPS para identificar el dashboard a afectar.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateDocuments';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateDocuments';
-- GO
