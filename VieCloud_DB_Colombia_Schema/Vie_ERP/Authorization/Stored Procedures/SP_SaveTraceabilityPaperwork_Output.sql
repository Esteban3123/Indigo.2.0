-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 02/06/2020
-- Description:	Procedimiento que se encarga de guardar la trazabilidad de tramites
-- =============================================
CREATE PROCEDURE [Authorization].[SP_SaveTraceabilityPaperwork_Output] 
    @Xml AS xml,
	@CodeResult INT OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT,
	@AnnexesConsecutives VARCHAR(20) OUTPUT,
	@AnnexId INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	--Variables para obtener la cabecera del xml
	declare @TraceabilityPaperwork table(RowHeaderId int, Id int, AdmissionNumber varchar(20), Folio varchar(20), ServiceCode varchar(20), Type tinyint, PatientCode varchar(20), 
	CareCenterCode varchar(20), RequestDate datetime, RequestQuantity int, FunctionalUnitCode varchar(20), EntityId int, EntityName varchar(50), PreviousStatus tinyint, Status tinyint, 
	CancellationReasonsId int, CancellationReasonsObservations varchar(max), CancellationUserCode varchar(20), AssignUserCode varchar(20), AuthorizationSourceId int,
	IsManual bit, CareGroupId int, HealthAdministratorId int, ProfessionalCode varchar(20), CareCenterTargetCode varchar(20), FunctionalUnitTargetId int,
	ServiceId int, ContractDescriptionId int, TraceabilityPaperworkPostponementReasonsId int)
	
	--Tabla de eventos
	declare @TraceabilityPaperworkEvents table(RowHeaderId int, RowId int, Id int, TraceabilityPaperworkId int, TraceabilityPaperworkAnnexesId int, HealthAdministratorId int, 
	ReportType tinyint, Instructions varchar(max), Status tinyint, AuthorizationNumber varchar(20), AuthorizedQuantity int, Observations varchar(max), 
	PatientNotificated bit, InformationPatient varchar(max), PhoneNumber varchar(20), Extension varchar(10), InitialTime time(0), EndTime time(0), ContactPerson varchar(50), 
	Charge varchar(50), RadicateNumber varchar(20), SendType tinyint, ReceivedDate datetime, ReceivePerson varchar(50), [URL] varchar(100), RegistrationDate datetime, 
	Email varchar(100), SendDate datetime, CreationUser varchar(20), AuthorizedBy varchar(300), AuthorizationDate datetime, AuthorizationExpiredDate datetime)
	
	--Tabla para los anexos del trámite
	declare @TraceabilityPaperworkAnnexes table(RowHeaderId int, RowId int, Id int, TraceabilityPaperworkId int, HealthAdministratorId int, TypeRequestServices tinyint, 
	PriorityAttention tinyint, Justification varchar(max), Folio varchar(20), CreationUser varchar(20), GenerateConsecutiveWithMultipleService bit, DiagnosticCode varchar(20))

	--Tabla para los documentos asociados al evento
	declare @Attachment table(RowId int, Name varchar(250), Extension varchar(10), Description varchar(max), FileAttached varchar(max), CreationUser varchar(20))

	DECLARE @TraceabilityPaperworkAlert TABLE
	(
		[RowHeaderId] [int] NOT NULL,
		[Id] [int] NULL,
		[TraceabilityPaperworkId] [int] NULL,
		[Comments] [varchar](max) NOT NULL,
		[Status] [bit] NOT NULL,
		[CreationUser] [varchar](20) NOT NULL,
		[ModificationUser] [varchar](20) NULL
	)

	--Tabla para almacenar el postergado de los trámites
	declare @TraceabilityPaperworkPostponementReasons table(RowHeaderId int, Id int, TraceabilityPaperworkId int, PostponementReasonsId int, PostponementDate datetime, 
	PostponementObservations varchar(500), StatusPrevious tinyint, Status bit, CreationUser varchar(20), ModificationUser varchar(20))

	--Mensaje que se retorna
	declare @GenerateConsecutives varchar(max) = ''

	begin try	
	
		--Se obtienen los datos de la cabecera del xml
		insert into @TraceabilityPaperwork
		select 
			t.x.value('RowHeaderId[1]','int') as RowHeaderId,
			t.x.value('Id[1]','int') as Id,
			IIF(t.x.value('AdmissionNumber[1]','varchar(20)') = '', null, t.x.value('AdmissionNumber[1]','varchar(20)')) as AdmissionNumber,
			IIF(t.x.value('Folio[1]','varchar(20)') = '', null, t.x.value('Folio[1]','varchar(20)')) as Folio,
			t.x.value('ServiceCode[1]','varchar(20)') as ServiceCode,
			t.x.value('Type[1]','tinyint') as Type,
			t.x.value('PatientCode[1]','varchar(20)') as PatientCode,
			t.x.value('CareCenterCode[1]','varchar(20)') as CareCenterCode,
			t.x.value('RequestDate[1]','datetime') as RequestDate,
			t.x.value('RequestQuantity[1]','int') as RequestQuantity,			
			t.x.value('FunctionalUnitCode[1]','varchar(20)') as FunctionalUnitCode,
			IIF(t.x.value('EntityId[1]','varchar(20)') = '', null, t.x.value('EntityId[1]','varchar(20)')) as EntityId,
			IIF(t.x.value('EntityName[1]','varchar(50)') = '', null, t.x.value('EntityName[1]','varchar(50)')) as EntityName,
			IIF(t.x.value('PreviousStatus[1]','tinyint') = '', null, t.x.value('PreviousStatus[1]','tinyint')) as PreviousStatus,
			t.x.value('Status[1]','tinyint') as Status,
			IIF(t.x.value('CancellationReasonsId[1]','varchar(20)') = '', null, t.x.value('CancellationReasonsId[1]','varchar(20)')) as CancellationReasonsId,
			IIF(t.x.value('CancellationReasonsObservations[1]','varchar(max)') = '', null, t.x.value('CancellationReasonsObservations[1]','varchar(max)')) as CancellationReasonsObservations,
			IIF(t.x.value('CancellationUserCode[1]','varchar(20)') = '', null, t.x.value('CancellationUserCode[1]','varchar(20)')) as CancellationUserCode,
			IIF(t.x.value('AssignUserCode[1]','varchar(20)') = '', null, t.x.value('AssignUserCode[1]','varchar(20)')) as AssignUserCode,
			IIF(t.x.value('AuthorizationSourceId[1]','varchar(20)') = '', null, t.x.value('AuthorizationSourceId[1]','varchar(20)')) as AuthorizationSourceId,
			t.x.value('IsManual[1]','bit') as IsManual,			
			t.x.value('CareGroupId[1]','int') as CareGroupId,
			IIF(t.x.value('HealthAdministratorId[1]','varchar(20)') = '', null, t.x.value('HealthAdministratorId[1]','varchar(20)')) as HealthAdministratorId,
			t.x.value('ProfessionalCode[1]','varchar(20)') as ProfessionalCode,
			IIF(t.x.value('CareCenterTargetCode[1]','varchar(20)') = '', null, t.x.value('CareCenterTargetCode[1]','varchar(20)')) as CareCenterTargetCode,
			IIF(t.x.value('FunctionalUnitTargetId[1]','varchar(20)') = '', null, t.x.value('FunctionalUnitTargetId[1]','varchar(20)')) as FunctionalUnitTargetId,
			t.x.value('ServiceId[1]','int') as ServiceId,
			t.x.value('ContractDescriptionId[1]','int') as ContractDescriptionId,
			t.x.value('TraceabilityPaperworkPostponementReasonsId[1]','int') as TraceabilityPaperworkPostponementReasonsId
		from @Xml.nodes('/TraceabilityPaperwork') t(x)

		---------------------------------------------------------------------------------------------------------------

		--Si vienen solicitudes duplicadas, y son registros sin tramite, eliminamos y dejamos solo una
		DELETE tp
		FROM @TraceabilityPaperwork tp
		JOIN
		(
			SELECT tp.EntityName, tp.EntityId, tp.ServiceCode, MIN(tp.RowHeaderId) RowHeaderId
			FROM @TraceabilityPaperwork tp
			WHERE ISNULL(tp.Id, 0) = 0
			GROUP BY tp.EntityName, tp.EntityId, tp.ServiceCode
			HAVING COUNT(1) > 1
		) tpd ON tp.EntityName = tpd.EntityName AND tp.EntityId = tpd.EntityId AND tp.ServiceCode = tpd.ServiceCode AND tp.RowHeaderId <> tpd.RowHeaderId
		WHERE ISNULL(tp.Id, 0) = 0

		---------------------------------------------------------------------------------------------------------------
		
		--Se obtienen los datos de los eventos
		insert into @TraceabilityPaperworkEvents
		select 
			t.x.value('RowHeaderId[1]','int') as RowHeaderId,
			t.x.value('RowId[1]','int') as RowId,
			t.x.value('Id[1]','int') as Id,
			t.x.value('TraceabilityPaperworkId[1]','int') as TraceabilityPaperworkId,
			IIF(t.x.value('TraceabilityPaperworkAnnexesId[1]','varchar(20)') = '', null, t.x.value('TraceabilityPaperworkAnnexesId[1]','varchar(20)')) as TraceabilityPaperworkAnnexesId,
			t.x.value('HealthAdministratorId[1]','int') as HealthAdministratorId,
			t.x.value('ReportType[1]','tinyint') as ReportType,
			IIF(t.x.value('Instructions[1]','varchar(max)') = '', null, t.x.value('Instructions[1]','varchar(max)')) as Instructions,
			t.x.value('Status[1]','tinyint') as Status,
			IIF(t.x.value('AuthorizationNumber[1]','varchar(20)') = '', null, t.x.value('AuthorizationNumber[1]','varchar(20)')) as AuthorizationNumber,
			IIF(t.x.value('AuthorizedQuantity[1]','varchar(20)') = '', null, t.x.value('AuthorizedQuantity[1]','varchar(20)')) as AuthorizedQuantity,
			IIF(t.x.value('Observations[1]','varchar(max)') = '', null, t.x.value('Observations[1]','varchar(max)')) as Observations,
			t.x.value('PatientNotificated[1]','bit') as PatientNotificated,
			IIF(t.x.value('InformationPatient[1]','varchar(max)') = '', null, t.x.value('InformationPatient[1]','varchar(max)')) as InformationPatient,
			IIF(t.x.value('PhoneNumber[1]','varchar(20)') = '', null, t.x.value('PhoneNumber[1]','varchar(20)')) as PhoneNumber,
			IIF(t.x.value('Extension[1]','varchar(10)') = '', null, t.x.value('Extension[1]','varchar(10)')) as Extension,
			IIF(t.x.value('InitialTime[1]','varchar(20)') = '', null, t.x.value('InitialTime[1]','varchar(20)')) as InitialTime,
			IIF(t.x.value('EndTime[1]','varchar(20)') = '', null, t.x.value('EndTime[1]','varchar(20)')) as EndTime,
			IIF(t.x.value('ContactPerson[1]','varchar(50)') = '', null, t.x.value('ContactPerson[1]','varchar(50)')) as ContactPerson,
			IIF(t.x.value('Charge[1]','varchar(50)') = '', null, t.x.value('Charge[1]','varchar(50)')) as Charge,
			IIF(t.x.value('RadicateNumber[1]','varchar(20)') = '', null, t.x.value('RadicateNumber[1]','varchar(20)')) as RadicateNumber,
			IIF(t.x.value('SendType[1]','varchar(20)') = '', null, t.x.value('SendType[1]','varchar(20)')) as SendType,
			IIF(t.x.value('ReceivedDate[1]','varchar(20)') = '', null, t.x.value('ReceivedDate[1]','varchar(20)')) as ReceivedDate,
			IIF(t.x.value('ReceivePerson[1]','varchar(50)') = '', null, t.x.value('ReceivePerson[1]','varchar(50)')) as ReceivePerson,
			IIF(t.x.value('URL[1]','varchar(100)') = '', null, t.x.value('URL[1]','varchar(100)')) as URL,
			IIF(t.x.value('RegistrationDate[1]','varchar(20)') = '', null, t.x.value('RegistrationDate[1]','varchar(20)')) as RegistrationDate,
			IIF(t.x.value('Email[1]','varchar(100)') = '', null, t.x.value('Email[1]','varchar(100)')) as Email,
			IIF(t.x.value('SendDate[1]','varchar(20)') = '', null, t.x.value('SendDate[1]','varchar(20)')) as SendDate,
			t.x.value('CreationUser[1]','varchar(20)') as CreationUser,
			IIF(t.x.value('AuthorizedBy[1]','varchar(300)') = '', null, t.x.value('AuthorizedBy[1]','varchar(300)')) as AuthorizedBy,
			IIF(t.x.value('AuthorizationDate[1]','varchar(20)') = '', null, t.x.value('AuthorizationDate[1]','varchar(20)')) as AuthorizationDate,
			IIF(t.x.value('AuthorizationExpiredDate[1]','varchar(20)') = '', null, t.x.value('AuthorizationExpiredDate[1]','varchar(20)')) as AuthorizationExpiredDate
		from @Xml.nodes('/TraceabilityPaperwork/TraceabilityPaperworkEvents') t(x)

		--Se obtiene los datos de los anexos
		insert into @TraceabilityPaperworkAnnexes
		select 
			t.x.value('RowHeaderId[1]','int') as RowHeaderId,
			t.x.value('RowId[1]','int') as RowId,
			t.x.value('Id[1]','int') as Id,
			t.x.value('TraceabilityPaperworkId[1]','int') as TraceabilityPaperworkId,
			t.x.value('HealthAdministratorId[1]','int') as HealthAdministratorId,
			t.x.value('TypeRequestServices[1]','tinyint') as TypeRequestServices,
			t.x.value('PriorityAttention[1]','tinyint') as PriorityAttention,
			IIF(t.x.value('Justification[1]','varchar(max)') = '', null, t.x.value('Justification[1]','varchar(max)')) as Justification,
			t.x.value('Folio[1]','varchar(20)') as Folio,
			t.x.value('CreationUser[1]','varchar(20)') as CreationUser,
			t.x.value('GenerateConsecutiveWithMultipleService[1]','bit') as GenerateConsecutiveWithMultipleService,
			IIF(t.x.value('DiagnosticCode[1]','varchar(20)') = '', null, t.x.value('DiagnosticCode[1]','varchar(20)')) as DiagnosticCode
		from @Xml.nodes('/TraceabilityPaperwork/TraceabilityPaperworkAnnexes') t(x)

		--Se obtiene los datos de los documentos asociados al evento
		insert into @Attachment
		select 
			t.x.value('RowId[1]','int') as RowId,
			t.x.value('Name[1]','varchar(250)') as Name,
			t.x.value('Extension[1]','varchar(10)') as Extension,
			t.x.value('Description[1]','varchar(max)') as Description,
			t.x.value('FileAttached[1]','varchar(max)') as FileAttached,
			t.x.value('CreationUser[1]','varchar(20)') as CreationUser
		from @Xml.nodes('/TraceabilityPaperwork/TraceabilityPaperworkEvents/Attachment') t(x)

		--Se obtiene los datos de los anexos
		insert into @TraceabilityPaperworkAlert
			select 
				t.x.value('RowHeaderId[1]','int') as RowHeaderId,
				t.x.value('Id[1]','int') as Id,
				t.x.value('TraceabilityPaperworkId[1]','int') as TraceabilityPaperworkId,
				t.x.value('Comments[1]','varchar(max)') as Comments,
				t.x.value('Status[1]','bit') as Status,
				t.x.value('CreationUser[1]','varchar(20)') as CreationUser,
				t.x.value('ModificationUser[1]','varchar(20)') as ModificationUser
			from @Xml.nodes('/TraceabilityPaperwork/TraceabilityPaperworkAlert') t(x)

		insert into @TraceabilityPaperworkPostponementReasons
		select 
			t.x.value('RowHeaderId[1]','int') as RowHeaderId,
			t.x.value('Id[1]','int') as Id,
			t.x.value('TraceabilityPaperworkId[1]','int') as TraceabilityPaperworkId,
			t.x.value('PostponementReasonsId[1]','int') as PostponementReasonsId,
			t.x.value('PostponementDate[1]','datetime') as PostponementDate,
			IIF(t.x.value('PostponementObservations[1]','varchar(500)') = '', null, t.x.value('PostponementObservations[1]','varchar(500)')) as PostponementObservations,
			t.x.value('StatusPrevious[1]','tinyint') as StatusPrevious,
			t.x.value('Status[1]','bit') as Status,
			t.x.value('CreationUser[1]','varchar(20)') as CreationUser,
			IIF(t.x.value('ModificationUser[1]','varchar(20)') = '', null, t.x.value('ModificationUser[1]','varchar(20)')) as ModificationUser
		from @Xml.nodes('/TraceabilityPaperwork/TraceabilityPaperworkPostponementReasons') t(x)

		declare  @RowsHeader int = 1, @RowIdHeader int = 0, @Id int, @AdmissionNumber varchar(20), @Folio varchar(20), @ServiceCode varchar(20), @Type tinyint, 
		@PatientCode varchar(20), @CareCenterCode varchar(20), @RequestDate datetime, @RequestQuantity int, @FunctionalUnitCode varchar(20), @EntityId int, @EntityName varchar(50), @Status tinyint, 
		@CancellationReasonsId int, @CancellationReasonsObservations varchar(max), @CancellationUserCode varchar(20), @AssignUserCode varchar(20), @AuthorizationSourceId int,
		@IsManual bit, @CareGroupId int, @HealthAdministratorId int, @ProfessionalCode varchar(20), @CareCenterTargetCode varchar(20), @FunctionalUnitTargetId int,
		@ServiceId int, @ContractDescriptionId int, @TraceabilityPaperworkPostponementReasonsId int, @PreviousStatus tinyint

		while @RowsHeader > 0
		begin
			select top 1 @RowIdHeader = RowHeaderId, @Id = Id, @AdmissionNumber = AdmissionNumber, @Folio = Folio, @ServiceCode = ServiceCode, @Type = Type, 
			@PatientCode = PatientCode, @CareCenterCode = CareCenterCode, @RequestDate = RequestDate, @RequestQuantity = RequestQuantity, 
			@FunctionalUnitCode = FunctionalUnitCode, @EntityId = EntityId, @EntityName = EntityName, @Status = Status, 
			@CancellationReasonsId = CancellationReasonsId, @CancellationReasonsObservations = CancellationReasonsObservations, 
			@CancellationUserCode = CancellationUserCode, @AssignUserCode = AssignUserCode, @AuthorizationSourceId = AuthorizationSourceId,
			@IsManual = IsManual, @CareGroupId = CareGroupId, @HealthAdministratorId = HealthAdministratorId, @ProfessionalCode = ProfessionalCode,
			@CareCenterTargetCode = CareCenterTargetCode, @FunctionalUnitTargetId = FunctionalUnitTargetId,
			@ServiceId = ServiceId, @ContractDescriptionId = ContractDescriptionId, 
			@TraceabilityPaperworkPostponementReasonsId = TraceabilityPaperworkPostponementReasonsId, @PreviousStatus = PreviousStatus
			from @TraceabilityPaperwork
			where RowHeaderId > @RowIdHeader 
			order by RowHeaderId

			set @RowsHeader = @@RowCount
			if @RowsHeader = 0 
				break

			--Si viene valor del id del último registro de postergados es porque se está reingresando ese trámite desde la pestaña de postergados
			if @TraceabilityPaperworkPostponementReasonsId is not null and @TraceabilityPaperworkPostponementReasonsId > 0
			begin
				set @Status = (select StatusPrevious from [Authorization].[TraceabilityPaperworkPostponementReasons] where Id = @TraceabilityPaperworkPostponementReasonsId)
				update [Authorization].[TraceabilityPaperworkPostponementReasons] set Status = 0 where Id = @TraceabilityPaperworkPostponementReasonsId
			end

			--Se inserta la cabecera
			if ISNULL(@Id, 0) = 0
			begin
				--Se valida que no exista un registro ya creado bajo la misma solicitud
				IF EXISTS (select 1 from [Authorization].[TraceabilityPaperwork] where EntityName = @EntityName AND EntityId = @EntityId AND ServiceCode = @ServiceCode)
				begin
					select	@CodeResult = 999, 
							@MessageResult = CONCAT('Ya existe un tramite asociado al paciente ', @PatientCode, ' con ingreso ', @AdmissionNumber, ' y servicio ', @ServiceCode), 
							@AnnexesConsecutives = '', 
							@AnnexId = 0
					return
				end
				

				INSERT INTO [Authorization].[TraceabilityPaperwork]([AdmissionNumber], [Folio], [ServiceCode], [Type], [PatientCode], [CareCenterCode], [RequestDate], [RequestQuantity],
				[DocumentDate], [FunctionalUnitCode], [EntityId], [EntityName], [Status], CancellationReasonsId, CancellationReasonsObservations, CancellationUserCode, 
				CancellationDate, AssignUserCode, AuthorizationSourceId, IsManual, CareGroupId, HealthAdministratorId, ProfessionalCode, CareCenterTargetCode, FunctionalUnitTargetId,
				ServiceId, ContractDescriptionId, PreviousStatus)
				VALUES(@AdmissionNumber, @Folio, @ServiceCode, @Type, @PatientCode, @CareCenterCode, @RequestDate, @RequestQuantity, Common.GETDATE(), @FunctionalUnitCode, @EntityId, @EntityName, 
				@Status, @CancellationReasonsId, @CancellationReasonsObservations, @CancellationUserCode, 
				IIF(@CancellationReasonsId is not null and @CancellationReasonsId > 0, Common.GETDATE(), null), @AssignUserCode, @AuthorizationSourceId, 
				@IsManual, @CareGroupId, @HealthAdministratorId, @ProfessionalCode, @CareCenterTargetCode, @FunctionalUnitTargetId,
				@ServiceId, @ContractDescriptionId, @PreviousStatus)

				set @Id = SCOPE_IDENTITY()
			end
			else begin --Se actualiza la cabecera
				UPDATE [Authorization].[TraceabilityPaperwork] 
					SET [AdmissionNumber] = @AdmissionNumber, [Folio] = @Folio, [ServiceCode] = @ServiceCode, [Type] = @Type, 
						[PatientCode] = @PatientCode, [CareCenterCode] = @CareCenterCode, [RequestDate] = @RequestDate, [RequestQuantity] = @RequestQuantity, 
						[FunctionalUnitCode] = @FunctionalUnitCode, [EntityId] = @EntityId, [EntityName] = @EntityName, [Status] = @Status,
						CancellationReasonsId = @CancellationReasonsId, CancellationReasonsObservations = @CancellationReasonsObservations, 
						AssignUserCode = ISNULL(@AssignUserCode, AssignUserCode) , CancellationUserCode = @CancellationUserCode,
						CancellationDate = IIF(@CancellationReasonsId is not null and @CancellationReasonsId > 0, Common.GETDATE(), null), AuthorizationSourceId = @AuthorizationSourceId,
						IsManual = @IsManual, CareGroupId = @CareGroupId, HealthAdministratorId = @HealthAdministratorId, ProfessionalCode = @ProfessionalCode,
						CareCenterTargetCode = @CareCenterTargetCode, FunctionalUnitTargetId = @FunctionalUnitTargetId, ServiceId = @ServiceId, 
						ContractDescriptionId = @ContractDescriptionId, PreviousStatus = @PreviousStatus
				WHERE Id = @Id
			end

			declare @RowsDetail int = 1, @RowIdDetail int = 0,
			@DetailId int, @HealthAdministratorIdDetail int, @ReportType tinyint, @Instructions varchar(max), @DetailStatus tinyint, 
			@AuthorizationNumber varchar(20), @AuthorizedQuantity int, @Observations varchar(max), @PatientNotificated bit, @InformationPatient varchar(max), @PhoneNumber varchar(20), 
			@Extension varchar(10), @InitialTime time(0), @EndTime time(0), @ContactPerson varchar(50), @Charge varchar(50), @RadicateNumber varchar(20), @SendType tinyint, @ReceivedDate datetime, 
			@ReceivePerson varchar(50), @URL varchar(100), @RegistrationDate datetime, @Email varchar(100), @SendDate datetime, @CreationUserEvent varchar(20),
			@TraceabilityPaperworkAnnexesId int, @AuthorizedBy varchar(300), @AuthorizationDate datetime, @AuthorizationExpiredDate datetime

			while @RowsDetail > 0
			begin
				select top 1 @RowIdDetail = RowId, @DetailId = Id, 
				@HealthAdministratorIdDetail = HealthAdministratorId, @ReportType = ReportType, @Instructions = Instructions, @DetailStatus = Status, 
				@AuthorizationNumber = AuthorizationNumber, @AuthorizedQuantity = AuthorizedQuantity, @Observations = Observations, @PatientNotificated = PatientNotificated, 
				@InformationPatient = InformationPatient, @PhoneNumber = PhoneNumber, @Extension = Extension, @InitialTime = InitialTime, @EndTime = EndTime, 
				@ContactPerson = ContactPerson, @Charge = Charge, @RadicateNumber = RadicateNumber, @SendType = SendType, @ReceivedDate = ReceivedDate, 
				@ReceivePerson = ReceivePerson, @URL = URL, @RegistrationDate = RegistrationDate, @Email = Email, @SendDate = SendDate, @CreationUserEvent = CreationUser,
				@TraceabilityPaperworkAnnexesId = TraceabilityPaperworkAnnexesId, @AuthorizedBy = AuthorizedBy, @AuthorizationDate = AuthorizationDate, @AuthorizationExpiredDate = AuthorizationExpiredDate
				from @TraceabilityPaperworkEvents
				where RowId > @RowIdDetail and RowHeaderId = @RowIdHeader
				order by RowId

				set @RowsDetail = @@RowCount
				if @RowsDetail = 0 
					break
			
				if @DetailId is null or @DetailId = 0 --Se inserta los eventos
				begin
					INSERT INTO [Authorization].[TraceabilityPaperworkEvents]([TraceabilityPaperworkId], [HealthAdministratorId], [ReportType], [Instructions], [Status], [AuthorizationNumber], 
					[AuthorizedQuantity], [Observations], [PatientNotificated], [InformationPatient], [PhoneNumber], [Extension], [InitialTime], [EndTime], [ContactPerson], [Charge], 
					[RadicateNumber], [SendType], [ReceivedDate], [ReceivePerson], [URL], [RegistrationDate], [Email], [SendDate], CreationUser, CreationDate, TraceabilityPaperworkAnnexesId,
					AuthorizedBy, AuthorizationDate, AuthorizationExpiredDate)
					VALUES(@Id, @HealthAdministratorIdDetail, @ReportType, @Instructions, @DetailStatus, @AuthorizationNumber, @AuthorizedQuantity, @Observations, @PatientNotificated, 
					@InformationPatient, @PhoneNumber, @Extension, @InitialTime, @EndTime, @ContactPerson, @Charge, @RadicateNumber, @SendType, @ReceivedDate, @ReceivePerson, @URL,
					@RegistrationDate, @Email, @SendDate, @CreationUserEvent, Common.GETDATE(), @TraceabilityPaperworkAnnexesId, @AuthorizedBy, @AuthorizationDate, @AuthorizationExpiredDate)

					set @DetailId = SCOPE_IDENTITY()
				end
				else begin --Se actualiza el detalle
					UPDATE [Authorization].[TraceabilityPaperworkEvents] SET [HealthAdministratorId] = @HealthAdministratorIdDetail, [ReportType] = @ReportType, [Instructions] = @Instructions,
					[Status] = @DetailStatus, [AuthorizationNumber] = @AuthorizationNumber, [AuthorizedQuantity] = @AuthorizedQuantity, [Observations] = @Observations, 
					[PatientNotificated] = @PatientNotificated, [InformationPatient] = @InformationPatient, [PhoneNumber] = @PhoneNumber, [Extension] = @Extension, 
					[InitialTime] = @InitialTime, [EndTime] = @EndTime, [ContactPerson] = @ContactPerson, [Charge] = @Charge, [RadicateNumber] = @RadicateNumber, [SendType] = @SendType, 
					[ReceivedDate] = @ReceivedDate, [ReceivePerson] = @ReceivePerson, [URL] = @URL, [RegistrationDate] = @RegistrationDate, [Email] = @Email, [SendDate] = @SendDate,
					TraceabilityPaperworkAnnexesId = @TraceabilityPaperworkAnnexesId, AuthorizedBy = @AuthorizedBy, AuthorizationDate = @AuthorizationDate, AuthorizationExpiredDate = @AuthorizationExpiredDate
					WHERE Id = @DetailId
				end

				--Se insertan los documentos asociados al evento
				INSERT INTO Common.Attachment(FormId, EntityName, EntityId, Name, EntityDetailName, EntityDetailId, Extension, Description, FileAttached, CreationUser, CreationDate)
				select	2178, @EntityName, @EntityId, a.Name, 'TraceabilityPaperworkEvents', @DetailId,						
						a.Extension, a.Description, 
						cast(N'' as xml).value('xs:base64Binary(sql:column("a.FileAttached"))', 'varbinary(max)'), a.CreationUser, Common.GETDATE()
				from @Attachment a
				where a.RowId = @RowIdDetail
			end	

			--Se reinician los valores para el ciclo
			set @RowsDetail = 1 
			set @RowIdDetail = 0

			--Variables para el ciclo
			declare @AnnexesId int = 0, @AnnexesHealthAdministratorId int, @TypeRequestServices tinyint, @PriorityAttention tinyint, @Justification varchar(max), 
			@AnnexesFolio varchar(20), @CreationUserAnnex varchar(20), @GenerateConsecutiveWithMultipleService bit, @DiagnosticCode varchar(20)

			while @RowsDetail > 0
			begin
				select top 1 @RowIdDetail = RowId, @AnnexesId = Id, 
				@AnnexesHealthAdministratorId = HealthAdministratorId, @TypeRequestServices = TypeRequestServices, @PriorityAttention = PriorityAttention, 
				@Justification = Justification, @AnnexesFolio = Folio, @CreationUserAnnex = CreationUser, 
				@GenerateConsecutiveWithMultipleService = GenerateConsecutiveWithMultipleService, @DiagnosticCode = DiagnosticCode
				from @TraceabilityPaperworkAnnexes
				where RowId > @RowIdDetail and RowHeaderId = @RowIdHeader
				order by RowId

				set @RowsDetail = @@RowCount
				if @RowsDetail = 0 
					break

				if @AnnexesId is null or @AnnexesId = 0 --Se inserta los anexos
				begin
					--Se valida si existe el código 11 en la tabla Common.Consecutive para obtener el consecutivo para el reporte de anexo
					if not exists(select 1 from Common.Consecutive where Code = 11)
					begin
						select	@CodeResult = 999, 
								@MessageResult = 'No existe el código 11 en la tabla de consecutivos para el reporte de anexo', 
								@AnnexesConsecutives = '', 
								@AnnexId = 0
						return
					end

					--Consecutivo del anexo
					declare @Consecutive decimal(18, 0) = 0

					--Si el consecutivo no es multiple o si es multiple y no se ha asignado consecutivo
					if (@GenerateConsecutiveWithMultipleService = 0) or (@GenerateConsecutiveWithMultipleService = 1 and @GenerateConsecutives = '')
					begin
						--Se obtiene el consecutivo
						set @Consecutive = (select NumberConsecutive from Common.Consecutive where Code = 11)

						--Se suma una unidad
						set @Consecutive += 1

						--Se actualiza el consecutivo
						update Common.Consecutive set NumberConsecutive = @Consecutive where Code = 11
					end
					else begin --Si el consecutivo es multiple y ya ha sido calculado un consecutivo se asigna el mismo
						set @Consecutive = CAST(@GenerateConsecutives as decimal(18, 0))
					end

					INSERT INTO [Authorization].[TraceabilityPaperworkAnnexes]([TraceabilityPaperworkId], [HealthAdministratorId], [TypeRequestServices], [PriorityAttention], 
					[Justification], [Folio], Consecutive, CreationDate, CreationUser, DiagnosticCode)
					VALUES(@Id, @AnnexesHealthAdministratorId, @TypeRequestServices, @PriorityAttention, @Justification, @AnnexesFolio, @Consecutive, Common.GETDATE(), 
					@CreationUserAnnex, @DiagnosticCode)

					--Se obtiene el id que se acaba de generar
					set @AnnexesId = SCOPE_IDENTITY()

					--Se asigna el consecutivo del informe de anexo generado
					set @GenerateConsecutives = @Consecutive
				end
				else begin --Se actualiza el anexo
					UPDATE [Authorization].[TraceabilityPaperworkAnnexes] SET [HealthAdministratorId] = @AnnexesHealthAdministratorId, [TypeRequestServices] = @TypeRequestServices,
					[PriorityAttention] = @PriorityAttention, [Justification] = @Justification, [Folio] = @AnnexesFolio, DiagnosticCode = @DiagnosticCode
					WHERE Id = @AnnexesId
				end
			end

			--Se agregan o actualizan las alertas enviadas
			UPDATE tpa
				SET tpa.Status = ttpa.Status,
					tpa.ModificationUser = ttpa.ModificationUser,
					tpa.ModificationDate = Common.GETDATE()
			FROM @TraceabilityPaperworkAlert ttpa
			JOIN [Authorization].TraceabilityPaperworkAlert tpa ON ttpa.Id = tpa.Id
			WHERE ttpa.RowHeaderId = @RowIdHeader AND tpa.TraceabilityPaperworkId = @Id

			INSERT INTO [Authorization].TraceabilityPaperworkAlert
			(
				TraceabilityPaperworkId, Comments, Status, CreationUser, CreationDate
			)
			SELECT @Id, ttpa.Comments, ttpa.Status, ttpa.CreationUser, Common.GETDATE()
			FROM @TraceabilityPaperworkAlert ttpa
			LEFT JOIN [Authorization].TraceabilityPaperworkAlert tpa ON ttpa.Id = tpa.Id
			WHERE ttpa.RowHeaderId = @RowIdHeader AND tpa.Id IS NULL

			--Se insertan las postergaciones
			INSERT INTO [Authorization].[TraceabilityPaperworkPostponementReasons]([TraceabilityPaperworkId],[PostponementReasonsId],[PostponementDate],[PostponementObservations],
			[StatusPrevious],[Status],[CreationUser],[CreationDate])
			select @Id, t.PostponementReasonsId, t.PostponementDate, t.PostponementObservations, t.StatusPrevious, t.Status, t.CreationUser, Common.GETDATE()
			from @TraceabilityPaperworkPostponementReasons t
			where t.RowHeaderId = @RowIdHeader and ISNULL(t.Id, 0) = 0

			--Se actualizan las postergaciones
			UPDATE t SET t.StatusPrevious = temp.StatusPrevious, t.Status = temp.Status, t.ModificationUser = temp.ModificationUser, t.ModificationDate = Common.GETDATE()
			from @TraceabilityPaperworkPostponementReasons temp
			inner join [Authorization].TraceabilityPaperworkPostponementReasons t on t.Id = temp.Id	
			where temp.RowHeaderId = @RowIdHeader AND t.TraceabilityPaperworkId = @Id

			--Si el estado es cancelado, se inactivan todas las postergaciones asociadas al trámite
			if @Status = 11
			begin
				update [Authorization].[TraceabilityPaperworkPostponementReasons] set Status = 0 where TraceabilityPaperworkId = @Id
			end

			--Se obtiene el id del último evento registrado al trámite
			declare @TraceabilityPaperworkEventsId int = (select MAX(Id) from [Authorization].TraceabilityPaperworkEvents where TraceabilityPaperworkId = @Id)

			--Se actualiza la tabla correspondiente de Crystal
			if @EntityName = 'HCORDIMAG'
			begin
				update .HCORDIMAG set TraceabilityPaperworkEventsId = @TraceabilityPaperworkEventsId, TraceabilityPaperworkId = @Id 
				where AUTO = @EntityId
			end
			else if @EntityName = 'HCORDLABO' begin
				update .HCORDLABO set TraceabilityPaperworkEventsId = @TraceabilityPaperworkEventsId, TraceabilityPaperworkId = @Id 
				where AUTO = @EntityId
			end
			else if @EntityName = 'HCORDPATO' begin
				update .HCORDPATO set TraceabilityPaperworkEventsId = @TraceabilityPaperworkEventsId, TraceabilityPaperworkId = @Id 
				where AUTO = @EntityId
			end
			else if @EntityName = 'HCORDINTE' begin
				update .HCORDINTE set TraceabilityPaperworkEventsId = @TraceabilityPaperworkEventsId, TraceabilityPaperworkId = @Id
				where AUTO = @EntityId
			end
			else if @EntityName = 'HCORDPRON' begin
				update .HCORDPRON set TraceabilityPaperworkEventsId = @TraceabilityPaperworkEventsId, TraceabilityPaperworkId = @Id
				where AUTO = @EntityId
			end
			else if @EntityName = 'HCORDPROQ' begin
				update .HCORDPROQ set TraceabilityPaperworkEventsId = @TraceabilityPaperworkEventsId, TraceabilityPaperworkId = @Id 
				where AUTO = @EntityId
			end
			else if @EntityName = 'HCPRESCRA' begin
				update .HCPRESCRD set TraceabilityPaperworkEventsId = @TraceabilityPaperworkEventsId, TraceabilityPaperworkId = @Id 
				where ID = @EntityId
			end
			else if @EntityName = 'HCORHEMCO' begin
				update .HCORHEMSER set TraceabilityPaperworkEventsId = @TraceabilityPaperworkEventsId, TraceabilityPaperworkId = @Id 
				where HCORHEMCOID = @EntityId and CODSERIPS = @ServiceCode
			end
			else if @EntityName = 'HCDESCOEX' begin
				update .HCDESCOEX set TraceabilityPaperworkEventsId = @TraceabilityPaperworkEventsId, TraceabilityPaperworkId = @Id 
				where AUTO = @EntityId
			end
			else if @EntityName = 'HCORMEDICAMESQUEMA' begin
				update EHR.HCORMEDICAMESQUEMA set TraceabilityPaperworkEventsId = @TraceabilityPaperworkEventsId, TraceabilityPaperworkId = @Id 
				where ID = @EntityId
			end
		end

		--Mensaje a retornar
		declare @MessageReturn varchar(max) = 'Se ha guardado correctamente'

		--Se valida si viene consecutivos de informe de anexos generados para concatenarlos en el mensaje final
		if @GenerateConsecutives <> ''
		begin
			set @MessageReturn = @MessageReturn + CHAR(13) + CHAR(10) + 'Número de informe: ' + @GenerateConsecutives
		end

		select	@CodeResult = 0, 
				@MessageResult = @MessageReturn, 
				@AnnexesConsecutives = @GenerateConsecutives, 
				@AnnexId = @AnnexesId
		return
	end try
	begin catch
		select	@CodeResult = 999, 
				@MessageResult = ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)), 
				@AnnexesConsecutives = '', 
				@AnnexId = 0
		return
	end catch
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que guarda y actualiza la trazabilidad completa de un trámite de autorización ante una EPS o aseguradora, procesando un XML que contiene la cabecera del trámite (paciente, número de ingreso, folio, servicio, centro de atención, unidad funcional, tipo, estado y entidad administradora), junto con sus eventos de gestión (envíos, respuestas, números de autorización, notificaciones al paciente, radicados y documentos adjuntos), anexos con justificación y diagnóstico, alertas y motivos de postergación. Inserta o actualiza registros en la tabla TraceabilityPaperworkEvents y las demás entidades relacionadas según corresponda al estado del trámite, retornando como parámetros de salida un código de resultado, un mensaje, el consecutivo de anexos generado y el identificador del anexo. Centraliza el ciclo de vida del trámite de autorización de servicios de salud, permitiendo rastrear cada acción realizada desde la solicitud hasta la autorización, cancelación o postergación.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'PROCEDURE', @level1name = N'SP_SaveTraceabilityPaperwork_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'PROCEDURE', @level1name = N'SP_SaveTraceabilityPaperwork_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La fecha de creación/modificación de cabecera, eventos, anexos, alertas y postergaciones siempre se asigna con Common.GETDATE() (no se acepta del XML).; El consecutivo de anexos se obtiene exclusivamente de Common.Consecutive con Code=11 y se incrementa en 1 por anexo, salvo que el flag GenerateConsecutiveWithMultipleService permita reutilizarlo dentro de una misma cabecera.; Antes de crear una cabecera nueva, no puede existir otro registro en Authorization.TraceabilityPaperwork con la misma combinación (EntityName, EntityId, ServiceCode).; Si la cabecera nueva no trae Id pero hay duplicados en el lote con misma (EntityName, EntityId, ServiceCode), solo se conserva el RowHeaderId mínimo (deduplicación previa).; Los documentos adjuntos al evento se almacenan siempre con FormId=2178 y EntityDetailName=''TraceabilityPaperworkEvents'', y el contenido se decodifica desde base64 a varbinary(max).; Al cancelar un trámite (Status=11) todas sus postergaciones quedan inactivas (Status=0).; Cuando se reingresa desde postergados, el Status efectivo del trámite proviene del StatusPrevious de la postergación, no del XML.; La sincronización con tablas Crystal/EHR de origen sólo ocurre para EntityName conocidos; para HCORHEMCO la coincidencia exige además CODSERIPS = ServiceCode.; Ante cualquier excepción se retorna CodeResult=999 con ERROR_MESSAGE() y la línea, AnnexesConsecutives='''' y AnnexId=0.; Salida exitosa: CodeResult=0 y AnnexesConsecutives contiene el último consecutivo generado (o vacío si no se generó ninguno).', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveTraceabilityPaperwork_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'trámite de autorización; paciente; ingreso (admisión); servicio; anexo de autorización; consecutivo de informe de anexo; postergación de trámite; cancelación de trámite; alertas de trámite; evento de trazabilidad; administradora de salud (EPS); centro de atención; unidad funcional; diagnóstico; órdenes clínicas (imágenes, laboratorio, patología, interconsulta, pronóstico, procedimientos quirúrgicos, hemoderivados, descongelados, prescripciones, esquemas de medicamentos)', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveTraceabilityPaperwork_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @TraceabilityPaperworkPostponementReasonsId IS NOT NULL AND > 0 → Se interpreta como reingreso desde la pestaña de postergados: el Status del trámite se reemplaza por StatusPrevious del registro de postergación y dicha postergación se desactiva (Status=0).; si ISNULL(@Id,0)=0 (cabecera nueva) → Si ya existe un trámite con misma EntityName+EntityId+ServiceCode, retorna CodeResult=999 con mensaje ''Ya existe un tramite asociado al paciente...''; si no, inserta nueva cabecera en Authorization.TraceabilityPaperwork. else Actualiza la cabecera existente por Id.; si @CancellationReasonsId IS NOT NULL AND > 0 → CancellationDate se establece en Common.GETDATE() al insertar/actualizar la cabecera. else CancellationDate queda en NULL.; si Anexo nuevo (Id null o 0) y NOT EXISTS Common.Consecutive con Code=11 → Retorna CodeResult=999 con mensaje ''No existe el código 11 en la tabla de consecutivos para el reporte de anexo'' y aborta.; si @GenerateConsecutiveWithMultipleService=0 OR (=1 AND aún no se ha generado consecutivo) → Lee NumberConsecutive de Common.Consecutive (Code=11), lo incrementa en 1 y lo persiste; usa ese valor como consecutivo del anexo. else Reutiliza el consecutivo ya generado en la iteración previa (@GenerateConsecutives).; si @Status = 11 (cancelado) → Se inactivan (Status=0) todas las postergaciones asociadas al trámite en Authorization.TraceabilityPaperworkPostponementReasons.; si @EntityName ∈ {HCORDIMAG, HCORDLABO, HCORDPATO, HCORDINTE, HCORDPRON, HCORDPROQ, HCPRESCRA, HCORHEMCO, HCDESCOEX, HCORMEDICAMESQUEMA} → Actualiza la tabla Crystal/EHR correspondiente fijando TraceabilityPaperworkEventsId y TraceabilityPaperworkId, usando AUTO/ID/HCORHEMCOID como llave según el caso (HCORHEMCO también filtra por CODSERIPS=ServiceCode; HCPRESCRA escribe en HCPRESCRD; HCORMEDICAMESQUEMA está en esquema EHR).; si Evento nuevo (DetailId null o 0) → Inserta en TraceabilityPaperworkEvents y luego inserta documentos asociados en Common.Attachment con FormId=2178 y EntityDetailName=''TraceabilityPaperworkEvents''. else Actualiza el evento existente por Id.; si @GenerateConsecutives <> '''' al final → Concatena al mensaje final ''Número de informe: '' + consecutivo generado.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveTraceabilityPaperwork_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Authorization.TraceabilityPaperwork; Authorization.TraceabilityPaperworkPostponementReasons; Common.Consecutive; Authorization.TraceabilityPaperworkEvents; Authorization.TraceabilityPaperworkAlert', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveTraceabilityPaperwork_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveTraceabilityPaperwork_Output';
-- GO
