

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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda y actualiza solicitudes de cotización de servicios en el dashboard de facturación. Recibe un XML con uno o varios registros de cotización (nuevos o existentes) para un paciente, con información del ingreso, servicio, profesional, entidad pagadora y estado de la cotización. Si el registro es nuevo (Id = 0) lo inserta en la tabla DashboardQuoted; si ya existe (Id > 0) lo actualiza, registrando además los usuarios y fechas de confirmación o marcación como no cotizado según el estado enviado. Existe para centralizar la gestión del flujo de cotizaciones de servicios de salud en el proceso de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_SaveDashboardQuoted';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_SaveDashboardQuoted';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Persiste (inserta o actualiza) los registros del dashboard de cotizaciones de servicios recibidos en un XML, marcando trazabilidad de creación, confirmación o no-cotización según el estado.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDashboardQuoted';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe seguir la estructura /DashboardQuoted con los nodos esperados (Id, AdmissionNumber, ServiceCode, Type, etc.); Para actualizar, el Id del XML debe existir previamente en Billing.DashboardQuoted; El UserCode recibido se utiliza como autor de creación, modificación, confirmación o marcación de no cotizado según corresponda', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDashboardQuoted';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La inserción solo aplica a filas del XML con Id = 0 y la actualización solo a filas con Id > 0; QuotationId nunca se almacena con valor 0 o menor: se normaliza a NULL; Los campos EntityId, EntityName, HealthAdministratorId y QuotationId provenientes del XML como cadena vacía se normalizan a NULL; Toda inserción registra CreationUser y CreationDate; toda actualización registra ModificationUser y ModificationDate con el usuario invocador y la hora del servidor (Common.GETDATE); Los campos NotQuotedUser/NotQuotedDate solo se llenan cuando Status = 3; ConfirmationUser/ConfirmationDate solo cuando Status = 2 (en actualización); Cualquier error es capturado y devuelto como CodeResult=999 con el mensaje y línea de error, sin propagar la excepción', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDashboardQuoted';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'cotización de servicios; facturación; paciente; centro de atención; unidad funcional; administrador de salud; grupo de cuidado; profesional tratante; admisión', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDashboardQuoted';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Billing.DashboardQuoted: Para cada fila del XML con Id = 0 se inserta un nuevo registro con CreationUser=@UserCode y CreationDate=Common.GETDATE(); si Status=3 también se setean NotQuotedUser y NotQuotedDate, en otro caso quedan NULL; [UPDATE] Billing.DashboardQuoted: Para cada fila del XML con Id > 0 se actualizan todos los campos del registro coincidente por Id, fijando ModificationUser=@UserCode y ModificationDate=Common.GETDATE(); ConfirmationUser/Date se llenan solo si Status=2 y NotQuotedUser/Date solo si Status=3; [RETURN_RESULT] (resultset): Devuelve CodeResult=0 y ''Se guardó correctamente'' al finalizar con éxito; en caso de excepción devuelve CodeResult=999 con ERROR_MESSAGE() y la línea del error', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDashboardQuoted';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Id = 0 en el registro del XML → Se inserta un nuevo registro en Billing.DashboardQuoted else Si Id > 0 se actualiza el registro existente con el mismo Id; si Status = 3 al insertar o actualizar → Se establece NotQuotedUser y NotQuotedDate con el usuario y la fecha actuales else NotQuotedUser y NotQuotedDate quedan en NULL; si Status = 2 en actualización → Se establece ConfirmationUser y ConfirmationDate con el usuario y la fecha actuales else ConfirmationUser y ConfirmationDate quedan en NULL; si QuotationId > 0 → Se persiste el valor de QuotationId tal cual else Se persiste NULL en QuotationId', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDashboardQuoted';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDashboardQuoted';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDashboardQuoted';
-- GO
