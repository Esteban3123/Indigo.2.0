
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda o actualiza la configuración de tiempos permitidos para servicios ambulatorios susceptibles de autorización, recibiendo los datos desde un XML. Crea o modifica los plazos máximos (en distintas unidades de tiempo) para asignación, solicitud, radicación y entrega de un servicio CUPS o producto de inventario del portafolio de autorización. Además, administra las excepciones por grupo de atención, permitiendo definir reglas particulares por grupo (como si el servicio es susceptible de autorización y sus tiempos específicos), insertando, actualizando o eliminando dichas excepciones según las instrucciones del XML. Es el procedimiento central para parametrizar los tiempos del proceso de autorización ambulatoria en la configuración del portafolio.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'PROCEDURE', @level1name = N'SP_SaveConfigurationServicesAmbulatory';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'PROCEDURE', @level1name = N'SP_SaveConfigurationServicesAmbulatory';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Persiste (inserta o actualiza) la configuración de tiempos para servicios ambulatorios susceptibles de autorización junto con sus excepciones por grupo de atención, a partir de un XML de entrada.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveConfigurationServicesAmbulatory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe respetar la estructura /ConfigurationServicesAmbulatory con nodos hijos ConfigurationServicesAmbulatoryExceptions y TableIds.; Para insertar nuevas configuraciones, el nodo Id de la cabecera debe ser nulo o 0 y debe existir al menos un registro en TableIds.; Para edición, el Id de la cabecera debe corresponder a un registro existente en Authorization.ConfigurationServicesAmbulatory.; Cada excepción a eliminar debe traer Id > 0 e IsDelete = 1; las nuevas excepciones deben venir con Id = 0; las excepciones a actualizar deben venir con Id > 0 e IsDelete = 0.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveConfigurationServicesAmbulatory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada nueva configuración insertada queda asociada con la misma copia de excepciones provistas en el XML.; Las excepciones marcadas IsDelete=1 nunca se reinsertan ni actualizan: se eliminan antes del flujo de inserción/actualización.; El procedimiento siempre devuelve un resultset con CodeResult y MessageResult, tanto en éxito (0) como en error (999).; Los identificadores de portafolio CUPS o de producto de inventario almacenados son mutuamente alternativos: lo que llega vacío se persiste como NULL.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveConfigurationServicesAmbulatory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Autorización de servicios ambulatorios; Configuración de tiempos (Asignación, Solicitud, Radicación, Entrega del servicio); Excepciones por grupo de atención (CareGroup); Susceptibilidad de autorización; Portafolio CUPS; Producto de inventario', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveConfigurationServicesAmbulatory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[DELETE] Authorization.ConfigurationServicesAmbulatoryExceptions: Cuando una excepción del XML trae Id > 0 e IsDelete = 1, se elimina el registro correspondiente por Id.; [INSERT] Authorization.ConfigurationServicesAmbulatory: Cuando @Id es nulo o 0 y existen filas en TableIds, se inserta una configuración por cada fila de TableIds usando AuthorizationPortfolioCUPSEntityId/AuthorizationPortfolioInventoryProductId (vacíos se convierten a NULL) y los tiempos/unidades de la cabecera.; [INSERT] Authorization.ConfigurationServicesAmbulatoryExceptions: Tras insertar cada nueva configuración, se replican TODAS las excepciones del XML asociándolas al SCOPE_IDENTITY() de la configuración recién creada.; [UPDATE] Authorization.ConfigurationServicesAmbulatory: En modo edición (existe @Id > 0), se actualizan Assignment, AssignmentUnit, Request, RequestUnit, Radicated, RadicatedUnit, DeliveryService y DeliveryServiceUnit del registro con Id = @Id.; [INSERT] Authorization.ConfigurationServicesAmbulatoryExceptions: En modo edición, las excepciones del XML con Id = 0 se insertan como nuevas asociadas a la configuración @Id.; [UPDATE] Authorization.ConfigurationServicesAmbulatoryExceptions: En modo edición, las excepciones del XML con Id > 0 (no marcadas para borrar) actualizan CareGroupId, SusceptibleAuthorization y los tiempos/unidades del registro existente con el mismo Id.; [RETURN_RESULT] (resultset): Al finalizar con éxito retorna CodeResult=0 y MessageResult=''Se guardó correctamente''; ante excepción retorna CodeResult=999 con ERROR_MESSAGE() y número de línea.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveConfigurationServicesAmbulatory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si EXISTS filas en @TableIds AND (@Id IS NULL OR @Id = 0) → Modo inserción: se itera TableIds creando una configuración por cada RowId y replicando todas las excepciones del XML para cada nueva configuración. else Modo edición: se actualiza la configuración con Id=@Id, se insertan las excepciones nuevas (Id=0) y se actualizan las existentes (Id>0).; si Excepción del XML con Id > 0 AND IsDelete = 1 → Se elimina la excepción de la tabla física y de la tabla temporal antes de proceder con inserciones/actualizaciones.; si Valor de AuthorizationPortfolioCUPSEntityId o AuthorizationPortfolioInventoryProductId viene como cadena vacía en el XML → Se convierte a NULL antes de insertar en ConfigurationServicesAmbulatory. else Se conserva el valor convertido a int.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveConfigurationServicesAmbulatory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Authorization.ConfigurationServicesAmbulatoryExceptions', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveConfigurationServicesAmbulatory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveConfigurationServicesAmbulatory';
-- GO
