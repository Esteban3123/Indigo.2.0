
-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 19/05/2020
-- Description:	Procedimiento que se encarga de guardar, actualizar una configuración de servicios susceptibles a autorización
-- =============================================
CREATE PROCEDURE [Authorization].[SP_SaveConfigurationServicesAmbulatory] 
    @Xml AS xml,
	@UserCode AS varchar(20)
AS
BEGIN
	SET NOCOUNT ON

	--Variables para obtener ConfigurationServicesAmbulatory del xml
	declare @Id int, @Assignment int, @AssignmentUnit tinyint, @Request int, @RequestUnit tinyint, @Radicated int, @RadicatedUnit tinyint, @DeliveryService int, @DeliveryServiceUnit tinyint
	
	--Tabla de excepciones
	declare @TableConfigurationServicesAmbulatoryExceptions table(Id int, CareGroupId int, SusceptibleAuthorization bit, Assignment int, AssignmentUnit tinyint, Request int, 
	RequestUnit tinyint, Radicated int, RadicatedUnit tinyint, DeliveryService int, DeliveryServiceUnit tinyint, IsDelete bit)

	--Tabla para almacenar los id del detalle del portafolio cups
	declare @TableIds table(RowId int, AuthorizationPortfolioCUPSEntityId int null, AuthorizationPortfolioInventoryProductId int null)
		
	begin try	

		--Se obtienen los datos de la cabecera del xml
		select 
			@Id = t.x.value('Id[1]','int'),
			@Assignment = t.x.value('Assignment[1]','int'),
			@AssignmentUnit = t.x.value('AssignmentUnit[1]','tinyint'),			
			@Request = t.x.value('Request[1]','int'),
			@RequestUnit = t.x.value('RequestUnit[1]','tinyint'),
			@Radicated = t.x.value('Radicated[1]','int'),
			@RadicatedUnit = t.x.value('RadicatedUnit[1]','tinyint'),
			@DeliveryService = t.x.value('DeliveryService[1]','int'),
			@DeliveryServiceUnit = t.x.value('DeliveryServiceUnit[1]','tinyint')
		from @Xml.nodes('/ConfigurationServicesAmbulatory') t(x)

		--Se obtienen los datos de las excepciones del xml
		insert into @TableConfigurationServicesAmbulatoryExceptions
		select 
			t.x.value('Id[1]','int') as Id,
			t.x.value('CareGroupId[1]','int') as CareGroupId,
			t.x.value('SusceptibleAuthorization[1]','bit') as SusceptibleAuthorization,
			t.x.value('Assignment[1]','int') as Assignment,
			t.x.value('AssignmentUnit[1]','tinyint') as AssignmentUnit,			
			t.x.value('Request[1]','int') as Request,
			t.x.value('RequestUnit[1]','tinyint') as RequestUnit,
			t.x.value('Radicated[1]','int') as Radicated,
			t.x.value('RadicatedUnit[1]','tinyint') as RadicatedUnit,
			t.x.value('DeliveryService[1]','int') as DeliveryService,
			t.x.value('DeliveryServiceUnit[1]','tinyint') as DeliveryServiceUnit,
			t.x.value('IsDelete[1]','bit') as IsDelete
		from @Xml.nodes('/ConfigurationServicesAmbulatory/ConfigurationServicesAmbulatoryExceptions') t(x)

		--Se obtienen los ids para generar las configuraciones
		insert into @TableIds
		select 
			t.x.value('RowId[1]','int') as RowId,
			IIF(t.x.value('AuthorizationPortfolioCUPSEntityId[1]','varchar(20)') = '', null, t.x.value('AuthorizationPortfolioCUPSEntityId[1]','varchar(20)')) as AuthorizationPortfolioCUPSEntityId,
			IIF(t.x.value('AuthorizationPortfolioInventoryProductId[1]','varchar(20)') = '', null, t.x.value('AuthorizationPortfolioInventoryProductId[1]','varchar(20)')) as AuthorizationPortfolioInventoryProductId
		from @Xml.nodes('/ConfigurationServicesAmbulatory/TableIds') t(x)

		--Se eliminan las excepciones
		delete from [Authorization].ConfigurationServicesAmbulatoryExceptions where Id in (select Id from @TableConfigurationServicesAmbulatoryExceptions where Id > 0 and IsDelete = 1)
		delete from @TableConfigurationServicesAmbulatoryExceptions where Id > 0 and IsDelete = 1

		if exists(select 1 from @TableIds) and (@Id is null or @Id = 0) --Si se esta insertando registros
		begin
			--Se declaran las variables para el while
			declare @Rows int = 1, @RowId int = 0, @AuthorizationPortfolioCUPSEntityId int, @AuthorizationPortfolioInventoryProductId int

			--Se recorre la tabla de ids para generar igual numero de registros
			while @Rows > 0
			begin
				select top 1 @RowId = RowId,
				@AuthorizationPortfolioCUPSEntityId = AuthorizationPortfolioCUPSEntityId,
				@AuthorizationPortfolioInventoryProductId = AuthorizationPortfolioInventoryProductId
				from @TableIds
				where RowId > @RowId 
				order by RowId

				set @Rows = @@RowCount
				if @Rows = 0 
					break

				--Se inserta la confirguración
				INSERT INTO [Authorization].[ConfigurationServicesAmbulatory]([AuthorizationPortfolioCUPSEntityId], [AuthorizationPortfolioInventoryProductId], [Assignment], [AssignmentUnit], 
				[Request], [RequestUnit], [Radicated], [RadicatedUnit], [DeliveryService], [DeliveryServiceUnit])
				VALUES(@AuthorizationPortfolioCUPSEntityId, @AuthorizationPortfolioInventoryProductId, @Assignment, @AssignmentUnit, @Request, @RequestUnit, @Radicated, @RadicatedUnit, 
				@DeliveryService, @DeliveryServiceUnit)

				set @Id = SCOPE_IDENTITY()

				--Se insertan las excepciones a la configuración
				INSERT INTO [Authorization].[ConfigurationServicesAmbulatoryExceptions]([ConfigurationServicesAmbulatoryId], [CareGroupId], [SusceptibleAuthorization], [Assignment], 
				[AssignmentUnit], [Request], [RequestUnit], [Radicated], [RadicatedUnit], [DeliveryService], [DeliveryServiceUnit])
				select @Id, CareGroupId, SusceptibleAuthorization, Assignment, AssignmentUnit, Request, RequestUnit, Radicated, RadicatedUnit, DeliveryService, DeliveryServiceUnit
				from @TableConfigurationServicesAmbulatoryExceptions
			end
		end
		else begin --Si se esta editando
			--Se actualiza la configuración
			UPDATE [Authorization].[ConfigurationServicesAmbulatory] SET [Assignment] = @Assignment, [AssignmentUnit] = @AssignmentUnit, [Request] = @Request, 
			[RequestUnit] = @RequestUnit, [Radicated] = @Radicated, [RadicatedUnit] = @RadicatedUnit, [DeliveryService] = @DeliveryService, [DeliveryServiceUnit] = @DeliveryServiceUnit
			WHERE Id = @Id

			--Se insertan las nuevas excepciones
			INSERT INTO [Authorization].[ConfigurationServicesAmbulatoryExceptions]([ConfigurationServicesAmbulatoryId], [CareGroupId], [SusceptibleAuthorization], [Assignment], 
			[AssignmentUnit], [Request], [RequestUnit], [Radicated], [RadicatedUnit], [DeliveryService], [DeliveryServiceUnit])
			select @Id, CareGroupId, SusceptibleAuthorization, Assignment, AssignmentUnit, Request, RequestUnit, Radicated, RadicatedUnit, DeliveryService, DeliveryServiceUnit
			from @TableConfigurationServicesAmbulatoryExceptions
			where Id = 0

			--Se actualizan las excepciones
			UPDATE e SET e.CareGroupId = te.CareGroupId, e.SusceptibleAuthorization = te.SusceptibleAuthorization,
			e.Assignment = te.Assignment, e.AssignmentUnit = te.AssignmentUnit, e.Request = te.Request, e.RequestUnit = te.RequestUnit, e.Radicated = te.Radicated, 
			e.RadicatedUnit = te.RadicatedUnit, e.DeliveryService = te.DeliveryService, e.DeliveryServiceUnit = te.DeliveryServiceUnit
			from @TableConfigurationServicesAmbulatoryExceptions te
			inner join [Authorization].ConfigurationServicesAmbulatoryExceptions e on e.Id = te.Id
			where te.Id > 0
		end

		select 0 AS CodeResult, 'Se guardó correctamente' AS MessageResult
		return
	end try
	begin catch
		select 999 AS CodeResult, ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)) AS MessageResult
		return
	end catch
END