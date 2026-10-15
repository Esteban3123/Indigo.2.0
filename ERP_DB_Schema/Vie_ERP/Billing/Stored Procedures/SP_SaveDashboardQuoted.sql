

-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 07/07/2020
-- Description:	Procedimiento que se encarga de guardar los datos de la dashboard de cotizaciones
-- =============================================
CREATE PROCEDURE [Billing].[SP_SaveDashboardQuoted] 
    @Xml as xml,
	@UserCode as varchar(20)
AS
BEGIN
	SET NOCOUNT ON

	--Tabla para obtener los datos del xml
	declare @DashboardQuoted table(Id int, AdmissionNumber varchar(20), ServiceCode varchar(20), Type tinyint, PatientCode varchar(20), 
	CareCenterCode varchar(20), RequestDate datetime, RequestQuantity int, FunctionalUnitCode varchar(20), EntityId int, EntityName varchar(50), 
	CareGroupId int, HealthAdministratorId int, ProfessionalCode varchar(20), QuotationId int, Status tinyint)
	
	begin try	
	
		--Se obtienen los datos del xml
		insert into @DashboardQuoted
		select 
			t.x.value('Id[1]','int') as Id,
			t.x.value('AdmissionNumber[1]','varchar(20)') as AdmissionNumber,
			t.x.value('ServiceCode[1]','varchar(20)') as ServiceCode,
			t.x.value('Type[1]','tinyint') as Type,
			t.x.value('PatientCode[1]','varchar(20)') as PatientCode,
			t.x.value('CareCenterCode[1]','varchar(20)') as CareCenterCode,
			t.x.value('RequestDate[1]','varchar(20)') as RequestDate,
			t.x.value('RequestQuantity[1]','int') as RequestQuantity,			
			t.x.value('FunctionalUnitCode[1]','varchar(20)') as FunctionalUnitCode,
			IIF(t.x.value('EntityId[1]','varchar(20)') = '', null, t.x.value('EntityId[1]','varchar(20)')) as EntityId,
			IIF(t.x.value('EntityName[1]','varchar(50)') = '', null, t.x.value('EntityName[1]','varchar(50)')) as EntityName,
			t.x.value('CareGroupId[1]','int') as CareGroupId,
			IIF(t.x.value('HealthAdministratorId[1]','varchar(20)') = '', null, t.x.value('HealthAdministratorId[1]','varchar(20)')) as HealthAdministratorId,
			t.x.value('ProfessionalCode[1]','varchar(20)') as ProfessionalCode,
			IIF(t.x.value('QuotationId[1]','varchar(20)') = '', null, t.x.value('QuotationId[1]','varchar(20)')) as QuotationId,
			t.x.value('Status[1]','tinyint') as Status
		from @Xml.nodes('/DashboardQuoted') t(x)
		
		--Se guardan los registros
		INSERT INTO [Billing].[DashboardQuoted]([AdmissionNumber], [ServiceCode], [Type], [PatientCode], [CareCenterCode], [RequestDate], [RequestQuantity], [FunctionalUnitCode], 
		[EntityId], [EntityName], [CareGroupId], [HealthAdministratorId], [ProfessionalCode], [QuotationId], [Status], [CreationUser], [CreationDate], [NotQuotedUser], [NotQuotedDate])
		select AdmissionNumber, ServiceCode, Type, PatientCode, CareCenterCode, RequestDate, RequestQuantity, FunctionalUnitCode, EntityId, EntityName, CareGroupId, HealthAdministratorId, 
		ProfessionalCode, IIF(QuotationId > 0, QuotationId, null), Status, @UserCode, [Common].[GETDATE](), IIF(Status = 3, @UserCode, null), IIF(Status = 3, [Common].[GETDATE](), null)
		from @DashboardQuoted 
		where Id = 0

		--Se actualizan los registros
		UPDATE d set d.AdmissionNumber = t.AdmissionNumber, d.ServiceCode = t.ServiceCode, d.Type = t.Type, d.PatientCode = t.PatientCode, d.CareCenterCode = t.CareCenterCode, 
		d.RequestDate = t.RequestDate, d.RequestQuantity = t.RequestQuantity, d.FunctionalUnitCode = t.FunctionalUnitCode, d.EntityId = t.EntityId, d.EntityName = t.EntityName, 
		d.CareGroupId = t.CareGroupId, d.HealthAdministratorId = t.HealthAdministratorId, d.ProfessionalCode = t.ProfessionalCode, d.QuotationId = IIF(t.QuotationId > 0, t.QuotationId, null), d.Status = t.Status,
		d.ModificationUser = @UserCode, d.ModificationDate = [Common].[GETDATE](), d.ConfirmationUser = IIF(t.Status = 2, @UserCode, null), d.ConfirmationDate = IIF(t.Status = 2, [Common].[GETDATE](), null),
		d.NotQuotedUser = IIF(t.Status = 3, @UserCode, null), d.NotQuotedDate = IIF(t.Status = 3, [Common].[GETDATE](), null)
		from @DashboardQuoted t
		inner join [Billing].[DashboardQuoted] d on d.Id = t.Id
		where t.Id > 0

		select 0 AS CodeResult, 'Se guardó correctamente' AS MessageResult
		return
	end try
	begin catch
		select 999 AS CodeResult, ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)) AS MessageResult
		return
	end catch
END