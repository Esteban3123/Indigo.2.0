-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 27/04/2020
-- Description:	Procedimiento que se encarga de guardar, actualizar un portafolio de autorizaciones
-- =============================================
CREATE PROCEDURE [Authorization].[SP_SaveAuthorizationPortfolio] 
    @AuthorizationPortfolioXml AS xml,
	@UserCode AS varchar(20)
AS
BEGIN
	SET NOCOUNT ON

	--Variables para obtener la cabecera del xml
	declare @Id int, @OperatingUnitId int, @Code varchar(20), @Name varchar(100), @Status bit, @TypePortfolio int

	--Tabla para obtener los cups del xml
	declare @AuthorizationPortfolioCUPSEntity table(Id int, AuthorizationPortfolioId int, AuthorizationGroupId int, CUPSEntityId int, ContractDescriptionId int, IsDelete bit)

	--Tabla para obtener los productos del xml
	declare @AuthorizationPortfolioInventoryProduct table(Id int, AuthorizationPortfolioId int, AuthorizationGroupId int, InventoryProductId int, IsDelete bit)

	--Tabla para obtener los centros de atención del xml
	declare @AuthorizationPortfolioCareCenter table(Id int, AuthorizationPortfolioId int, CareCenterCode varchar(20), IsDelete bit)
	
	begin try	

		--Se obtienen los datos de la cabecera del xml
		select 
			@Id = t.x.value('Id[1]','int'),
			@OperatingUnitId = t.x.value('OperatingUnitId[1]','int'),
			@Code = t.x.value('Code[1]','varchar(20)'),
			@Name = t.x.value('Name[1]','varchar(100)'),
			@Status = t.x.value('Status[1]','bit'),
			@TypePortfolio = t.x.value('TypePortfolio[1]','int')
		from @AuthorizationPortfolioXml.nodes('/AuthorizationPortfolio') t(x)

		--Se obtienen los datos de los cups del xml
		insert into @AuthorizationPortfolioCUPSEntity
		select 
			t.x.value('Id[1]','int') as Id,
			t.x.value('AuthorizationPortfolioId[1]','int') as AuthorizationPortfolioId,
			t.x.value('AuthorizationGroupId[1]','int') as AuthorizationGroupId,
			t.x.value('CUPSEntityId[1]','int') as CUPSEntityId,
			t.x.value('ContractDescriptionId[1]','int') as ContractDescriptionId,
			t.x.value('IsDelete[1]','bit') as IsDelete
		from @AuthorizationPortfolioXml.nodes('/AuthorizationPortfolio/AuthorizationPortfolioCUPSEntity') t(x)

		--Se obtienen los datos de los productos del xml
		insert into @AuthorizationPortfolioInventoryProduct
		select 
			t.x.value('Id[1]','int') as Id,
			t.x.value('AuthorizationPortfolioId[1]','int') as AuthorizationPortfolioId,
			t.x.value('AuthorizationGroupId[1]','int') as AuthorizationGroupId,
			t.x.value('InventoryProductId[1]','int') as InventoryProductId,
			t.x.value('IsDelete[1]','bit') as IsDelete
		from @AuthorizationPortfolioXml.nodes('/AuthorizationPortfolio/AuthorizationPortfolioInventoryProduct') t(x)

		--Se obtienen los datos de los centros de atención del xml
		insert into @AuthorizationPortfolioCareCenter
		select 
			t.x.value('Id[1]','int') as Id,
			t.x.value('AuthorizationPortfolioId[1]','int') as AuthorizationPortfolioId,
			t.x.value('CareCenterCode[1]','varchar(20)') as CareCenterCode,
			t.x.value('IsDelete[1]','bit') as IsDelete
		from @AuthorizationPortfolioXml.nodes('/AuthorizationPortfolio/AuthorizationPortfolioCareCenter') t(x)

		--Se valida que los centros de atención no existan en otro registro
		if exists(select *
		from [Authorization].AuthorizationPortfolioCareCenter cc
		inner join [Authorization].AuthorizationPortfolio ap on ap.Id = cc.AuthorizationPortfolioId
		where cc.CareCenterCode in (select CareCenterCode from @AuthorizationPortfolioCareCenter where IsDelete = 0) and cc.AuthorizationPortfolioId <> @Id and ap.TypePortfolio = @TypePortfolio)
		begin
			declare @Message varchar(max) = ''
			select @Message = STUFF((
								select distinct CHAR(13) + CHAR(10) + 'El centro de atención ' + RTRIM(ltrim(cc.CareCenterCode)) + ' ya existe en el portafolio ' + ap.Code
								from [Authorization].AuthorizationPortfolioCareCenter cc
								inner join [Authorization].AuthorizationPortfolio ap on ap.Id = cc.AuthorizationPortfolioId
								where cc.CareCenterCode in (select CareCenterCode from @AuthorizationPortfolioCareCenter where IsDelete = 0) and cc.AuthorizationPortfolioId <> @Id and ap.TypePortfolio = @TypePortfolio
								FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			select 999 AS CodeResult, @Message AS MessageResult, 0 AS Id, '' AS Code
			return
		end

		--Se eliminan los detalles
		delete from [Authorization].AuthorizationPortfolioCUPSEntity where Id in (select Id from @AuthorizationPortfolioCUPSEntity where Id > 0 and IsDelete = 1)
		delete from @AuthorizationPortfolioCUPSEntity where Id > 0 and IsDelete = 1

		delete from [Authorization].AuthorizationPortfolioInventoryProduct where Id in (select Id from @AuthorizationPortfolioInventoryProduct where Id > 0 and IsDelete = 1)
		delete from @AuthorizationPortfolioInventoryProduct where Id > 0 and IsDelete = 1

		delete from [Authorization].AuthorizationPortfolioCareCenter where Id in (select Id from @AuthorizationPortfolioCareCenter where Id > 0 and IsDelete = 1)
		delete from @AuthorizationPortfolioCareCenter where Id > 0 and IsDelete = 1

		--Si no viene el código se genera
		if @Code = '' or @Code is null
		begin
			--Consultamos si la secuencia es con O o OU
			declare @scope varchar(5) = ''
			declare @idSequenceDetail int
			declare @pattern varchar(300)
			declare @NextS int
			select @scope = Scope from [Authorization].AuthorizationSequence
			where IdForm = '2168'

			if @scope = 'O' --Si el ambito es por organización
			begin
				select top 1 @pattern = cs.Pattern, @NextS = bsd.[Next] , @idSequenceDetail = bsd.Id  
				from [Authorization].AuthorizationSequenceDetail bsd 
				inner join [Authorization].AuthorizationSequence bs on bs.Id = bsd.IdSequenseAuthorizationC
				inner join Common.Sequense cs on cs.Id = bsd.IdSequense
				where bs.IdForm = '2168'
				order by bsd.Next desc
			end
			else begin --Si el ambito es por unidad operativa
				select top 1 @pattern = cs.Pattern, @NextS = bsd.[Next] , @idSequenceDetail = bsd.Id  
				from [Authorization].AuthorizationSequenceDetail bsd 
				inner join [Authorization].AuthorizationSequence bs on bs.Id = bsd.IdSequenseAuthorizationC
				inner join Common.Sequense cs on cs.Id = bsd.IdSequense
				where bs.IdForm = '2168' and bsd.IdOperatingUnit = @OperatingUnitId
				order by bsd.Next desc
			end
					
			if (@idSequenceDetail is null)
			Begin
				select 999 AS CodeResult, 'Secuencia no encontrada para generar la cotización' AS MessageResult, 0 AS Id, '' AS Code
				return
			End

			select @Code = dbo.GetSequence('',@pattern,@NextS)
			update [Authorization].AuthorizationSequenceDetail set [Next] += 1 where Id = @idSequenceDetail
		end

		if @Id = 0 or @Id is null --Se guarda la cabecera
		begin
			insert into [Authorization].[AuthorizationPortfolio]([OperatingUnitId], [Code], [Name], [Status], [CreationUser], [CreationDate],[TypePortfolio])
			values(@OperatingUnitId, @Code, @Name, @Status, @UserCode, [Common].[GETDATE](), @TypePortfolio)

			set @Id = SCOPE_IDENTITY()
		end
		else begin --Se actualiza la cabecera
			update [Authorization].[AuthorizationPortfolio]
			set [OperatingUnitId] = @OperatingUnitId, [Code] = @Code, [Name] = @Name, [Status] = @Status,
			[ModificationUser] = @UserCode, [ModificationDate] = [Common].[GETDATE](), [TypePortfolio] = @TypePortfolio
			where Id = @Id
		end

		--Si insertan los cups nuevos
		insert into [Authorization].[AuthorizationPortfolioCUPSEntity]([AuthorizationPortfolioId], [AuthorizationGroupId], [CUPSEntityId], [ContractDescriptionId])
		select @Id, AuthorizationGroupId, CUPSEntityId, ContractDescriptionId
		from @AuthorizationPortfolioCUPSEntity
		where Id = 0

		--Se insertan los productos nuevos
		insert into [Authorization].[AuthorizationPortfolioInventoryProduct]([AuthorizationPortfolioId], [AuthorizationGroupId], [InventoryProductId])
		select @Id, AuthorizationGroupId, InventoryProductId
		from @AuthorizationPortfolioInventoryProduct
		where Id = 0

		--Se insertan los centros de atención nuevos
		insert into [Authorization].[AuthorizationPortfolioCareCenter]([AuthorizationPortfolioId], [CareCenterCode])
		select @Id, CareCenterCode
		from @AuthorizationPortfolioCareCenter
		where Id = 0

		select 0 AS CodeResult, 'Se guardó correctamente' AS MessageResult, @Id as Id, @Code as Code
		return
	end try
	begin catch
		select 999 AS CodeResult, ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)) AS MessageResult, 0 AS Id, '' AS Code
		return
	end catch
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que crea o actualiza un portafolio de autorizaciones, incluyendo su información de cabecera (unidad operativa, código, nombre y estado) y sus tres tipos de detalle: los servicios CUPS autorizados por entidad y grupo de autorización, los productos de inventario (medicamentos e insumos) habilitados en el portafolio, y los centros de atención donde aplica el portafolio. Recibe toda la información empaquetada en un XML, valida que un mismo centro de atención no esté asignado a dos portafolios distintos, genera automáticamente el código del portafolio si no se proporciona (usando la secuencia configurada por organización o unidad operativa), y ejecuta las operaciones de inserción, actualización o eliminación lógica sobre las tablas AuthorizationPortfolio, AuthorizationPortfolioCUPSEntity, AuthorizationPortfolioInventoryProduct y AuthorizationPortfolioCareCenter, retornando el resultado de la operación con su código y mensaje.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'PROCEDURE', @level1name = N'SP_SaveAuthorizationPortfolio';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'PROCEDURE', @level1name = N'SP_SaveAuthorizationPortfolio';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Persiste (crea o actualiza) un portafolio de autorización junto con sus detalles (CUPS, productos de inventario y centros de atención), validando exclusividad de centros de atención y generando consecutivo cuando no se proporciona código.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAuthorizationPortfolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe seguir la estructura /AuthorizationPortfolio con nodos hijos AuthorizationPortfolioCUPSEntity, AuthorizationPortfolioInventoryProduct y AuthorizationPortfolioCareCenter; Debe existir configuración en Authorization.AuthorizationSequence para IdForm=''2168'' cuando se requiera generar código automático; Cuando el ámbito es por unidad operativa, debe existir un AuthorizationSequenceDetail asociado al @OperatingUnitId; Los Id de detalles a eliminar deben corresponder a registros existentes en las tablas de detalle', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAuthorizationPortfolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Un CareCenterCode no puede pertenecer simultáneamente a más de un portafolio de autorización (validación previa antes de cualquier escritura); Las inserciones de detalles (CUPS, productos, centros) solo aplican a filas del XML con Id=0 (nuevas); las filas existentes no se actualizan en sus atributos; Las eliminaciones de detalles solo aplican a filas con Id>0 y IsDelete=1; La generación de código solo ocurre cuando @Code está vacío/NULL; si se generó, se incrementa en 1 el contador Next del detalle de secuencia usado; El alcance de la secuencia (Scope=''O'' u otro) determina si la numeración es global por organización o segmentada por unidad operativa; Toda la operación se ejecuta dentro de TRY/CATCH retornando estructura uniforme {CodeResult, MessageResult, Id, Code}; éxito=0, error=999', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAuthorizationPortfolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'portafolio de autorización; centro de atención; CUPS; grupo de autorización; producto de inventario; unidad operativa; secuencia/consecutivo de autorización; ámbito por organización vs unidad operativa', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAuthorizationPortfolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe algún CareCenterCode (con IsDelete=0) ya asociado a otro portafolio distinto del actual (AuthorizationPortfolioId <> @Id) → Retorna CodeResult=999 con mensaje listando los centros y portafolios en conflicto, y aborta sin persistir cambios else Continúa con la eliminación de detalles marcados y la generación/persistencia; si @Code viene vacío o NULL → Genera el código consultando la secuencia del formulario IdForm=''2168'' en AuthorizationSequence; si Scope=''O'' usa la secuencia a nivel organización, en caso contrario filtra por IdOperatingUnit=@OperatingUnitId else Usa el @Code recibido sin generar consecutivo; si No se encuentra detalle de secuencia (@idSequenceDetail IS NULL) al intentar generar el código → Retorna CodeResult=999 con mensaje ''Secuencia no encontrada para generar la cotización'' y aborta; si @Id = 0 o NULL (registro nuevo) → INSERT en Authorization.AuthorizationPortfolio y asigna @Id = SCOPE_IDENTITY() else UPDATE de la cabecera existente en Authorization.AuthorizationPortfolio para Id=@Id', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAuthorizationPortfolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.GetSequence; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAuthorizationPortfolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Authorization.AuthorizationPortfolioCareCenter; Authorization.AuthorizationPortfolio; Authorization.AuthorizationSequence; Authorization.AuthorizationSequenceDetail; Common.Sequense', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAuthorizationPortfolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAuthorizationPortfolio';
-- GO
