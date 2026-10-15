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
	declare @Id int, @OperatingUnitId int, @Code varchar(20), @Name varchar(100), @Status bit

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
			@Status = t.x.value('Status[1]','bit')
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
		where cc.CareCenterCode in (select CareCenterCode from @AuthorizationPortfolioCareCenter where IsDelete = 0) and cc.AuthorizationPortfolioId <> @Id)
		begin
			declare @Message varchar(max) = ''
			select @Message = STUFF((
								select distinct CHAR(13) + CHAR(10) + 'El centro de atención ' + RTRIM(ltrim(cc.CareCenterCode)) + ' ya existe en el portafolio ' + ap.Code
								from [Authorization].AuthorizationPortfolioCareCenter cc
								inner join [Authorization].AuthorizationPortfolio ap on ap.Id = cc.AuthorizationPortfolioId
								where cc.CareCenterCode in (select CareCenterCode from @AuthorizationPortfolioCareCenter where IsDelete = 0) and cc.AuthorizationPortfolioId <> @Id
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
			insert into [Authorization].[AuthorizationPortfolio]([OperatingUnitId], [Code], [Name], [Status], [CreationUser], [CreationDate])
			values(@OperatingUnitId, @Code, @Name, @Status, @UserCode, [Common].[GETDATE]())

			set @Id = SCOPE_IDENTITY()
		end
		else begin --Se actualiza la cabecera
			update [Authorization].[AuthorizationPortfolio]
			set [OperatingUnitId] = @OperatingUnitId, [Code] = @Code, [Name] = @Name, [Status] = @Status,
			[ModificationUser] = @UserCode, [ModificationDate] = [Common].[GETDATE]()
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