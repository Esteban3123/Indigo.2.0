
-- =============================================
-- Author:		Andres Alarcon
-- Create date: 2023-05-24
-- Description:	Procedimiento que se encarga de guardar, actualizar la tarifa de productos y servicios
-- =============================================

CREATE PROCEDURE [Billing].[SP_SaveProductAndServiceFee] 
    @EntityXml AS XML,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON

	--Se declaran las variables para obtener la cabecera
	DECLARE @Id INT, 
			@Code VARCHAR(20),
			@Name VARCHAR(MAX),
			@OperatingUnitId int,
			@Status TINYINT

	--Tabla temporal de BasicBillingDetail
	DECLARE @ProductFeeDetail TABLE
	(
		[Id] [int],
		[TempId] [int],
		[ProductAndServiceFeeId] [int],
		[ProductId] [int],
		[RateType] [bit],
		[PercentageType] [bit],
		[Observations] [varchar](max),
		[InitialDate] [DateTime],
		[FinalDate] [DateTime],
		[SalePrice] [numeric](18,2),
		[Percentage] [numeric](5,2),
		IsDelete [bit]
	)

	DECLARE @ServiceFeeDetail TABLE
	(
		[Id] [int],
		[TempId] [int],
		[ProductAndServiceFeeId] [int],
		[ServiceId] [int],
		[Observations] [varchar](max),
		[InitialDate] [DateTime],
		[FinalDate] [DateTime],
		[SalePrice] [numeric](18,2),
		IsDelete [bit]
	)

		DECLARE @ProductAndServiceFeeUserDetail TABLE
	(
		[Id] [int],
		[TempId] [int],
		[ProductAndServiceFeeId] [int],
		[UserId] [int],
		[UserCode] [varchar](20),
		IsDelete [bit]
	)
	
	BEGIN TRY
		--Se obtienen los datos de la cabecera
		SELECT 
			@Id = t.x.value('Id[1]','int'),
			@Code = t.x.value('Code[1]','varchar(20)'),
			@Name = t.x.value('Name[1]','varchar(max)'),
			@OperatingUnitId = t.x.value('OperatingUnitId[1]','int'),
			@Status = t.x.value('Status[1]','tinyint')
		FROM @EntityXml.nodes('/ProductAndServiceFee') t(x)
		
			--Se obtiene el detalle de los productos
			INSERT INTO @ProductFeeDetail
				SELECT 
					t.x.value('Id[1]','int') as Id,
					t.x.value('TempId[1]','int') as TempId,
					t.x.value('ProductAndServiceFeeId[1]','int') as ProductAndServiceFeeId,
					t.x.value('ProductId[1]','int') as ProductId,
					t.x.value('RateType[1]','bit') as RateType,
					t.x.value('PercentageType[1]','bit') as PercentageType,
					t.x.value('Observations[1]','varchar(max)') as Observations,
		TRY_PARSE(
    REPLACE(
        REPLACE(
            LOWER(
                REPLACE(
                    t.x.value('InitialDate[1]','varchar(50)'),
                    CHAR(160),
                    ' '
                )
            ),
            'a. m.',
            'AM'
        ),
        'p. m.',
        'PM'
    )
    AS datetime
    USING 'es-CO'
) as InitialDate,

TRY_PARSE(
    REPLACE(
        REPLACE(
            LOWER(
                REPLACE(
                    t.x.value('FinalDate[1]','varchar(50)'),
                    CHAR(160),
                    ' '
                )
            ),
            'a. m.',
            'AM'
        ),
        'p. m.',
        'PM'
    )
    AS datetime
    USING 'es-CO'
) as FinalDate,
					IIF(t.x.value('SalePrice[1]','varchar(20)') = '', null, REPLACE(t.x.value('SalePrice[1]','varchar(20)'), ',', '.')) as SalePrice,
					IIF(t.x.value('Percentage[1]','varchar(20)') = '', null, REPLACE(t.x.value('Percentage[1]','varchar(20)'), ',', '.')) as Percentage,
					t.x.value('IsDelete[1]','bit') as IsDelete
				FROM @EntityXml.nodes('/ProductAndServiceFee/ProductFeeDetail') t(x)
			
			INSERT INTO @ServiceFeeDetail
				SELECT 
					t.x.value('Id[1]','int') as Id,
					t.x.value('TempId[1]','int') as TempId,
					t.x.value('ProductAndServiceFeeId[1]','int') as ProductAndServiceFeeId,
					t.x.value('ServiceId[1]','int') as ServiceId,
					t.x.value('Observations[1]','varchar(max)') as Observations,
			TRY_PARSE(
    REPLACE(
        REPLACE(
            LOWER(
                REPLACE(
                    t.x.value('InitialDate[1]','varchar(50)'),
                    CHAR(160),
                    ' '
                )
            ),
            'a. m.',
            'AM'
        ),
        'p. m.',
        'PM'
    )
    AS datetime
    USING 'es-CO'
) as InitialDate,

TRY_PARSE(
    REPLACE(
        REPLACE(
            LOWER(
                REPLACE(
                    t.x.value('FinalDate[1]','varchar(50)'),
                    CHAR(160),
                    ' '
                )
            ),
            'a. m.',
            'AM'
        ),
        'p. m.',
        'PM'
    )
    AS datetime
    USING 'es-CO'
) as FinalDate,
					IIF(t.x.value('SalePrice[1]','varchar(20)') = '', null, REPLACE(t.x.value('SalePrice[1]','varchar(20)'), ',', '.')) as SalePrice,
					t.x.value('IsDelete[1]','bit') as IsDelete
				FROM @EntityXml.nodes('/ProductAndServiceFee/ServiceFeeDetail') t(x)

						
			INSERT INTO @ProductAndServiceFeeUserDetail
				SELECT 
					t.x.value('Id[1]','int') as Id,
					t.x.value('TempId[1]','int') as TempId,
					t.x.value('ProductAndServiceFeeId[1]','int') as ProductAndServiceFeeId,
					t.x.value('UserId[1]','int') as UserId,
					t.x.value('UserCode[1]','varchar(20)') as UserCode,
					t.x.value('IsDelete[1]','bit') as IsDelete
				FROM @EntityXml.nodes('/ProductAndServiceFee/ProductAndServiceFeeUser') t(x)
		
			Delete pfd
			from Billing.ProductFeeDetail pfd
			JOIN @ProductFeeDetail pfdd ON pfd.Id = pfdd.Id
			where pfdd.IsDelete = 1

			DELETE sfd
			FROM Billing.ServiceFeeDetail sfd
			JOIN @ServiceFeeDetail sfdd ON sfd.Id = sfdd.Id
			WHERE sfdd.IsDelete = 1

			DELETE psf
			FROM Billing.ProductAndServiceFeeUser psf
			JOIN @ProductAndServiceFeeUserDetail psfd ON psfd.Id = psf.Id
			WHERE psfd.IsDelete = 1

			DELETE FROM @ProductFeeDetail WHERE IsDelete = 1
			DELETE FROM @ServiceFeeDetail WHERE IsDelete = 1
			DELETE FROM @ProductAndServiceFeeUserDetail WHERE IsDelete = 1
	
			--Si se esta insertando por primera vez se consulta la secuencia numerica
			IF @Code = '' or @code is null
			BEGIN
				--Consultamos si la secuencia es con O o OU
				DECLARE @scope varchar(5) = '',
						@idSequenceDetail int,
						@pattern varchar(300),
						@NextS int,
						@IdForm varchar(5) = '2827'
				
				SELECT @scope = Scope 
				FROM Billing.BillingSequence
				WHERE IdForm = @IdForm

				--Se valida el scope
				IF @scope = 'O'
				BEGIN
					-- Si el ambito es por organización
					SELECT @pattern = cs.Pattern, 
						@NextS = bsd.[Next] , 
						@idSequenceDetail = bsd.Id  
					FROM Billing.BillingSequenceDetail bsd 
					JOIN Billing.BillingSequence bs ON bs.Id = bsd.IdSequenseBillingC
					JOIN Common.Sequense cs on cs.Id = bsd.IdSequense
					WHERE bs.IdForm = @IdForm
				END
				ELSE BEGIN
					-- Si el ambito es por unidad operativa
					SELECT @pattern = cs.Pattern, 
						@NextS = bsd.[Next], 
						@idSequenceDetail = bsd.Id  
					FROM Billing.BillingSequenceDetail bsd 
					JOIN Billing.BillingSequence bs ON bs.Id = bsd.IdSequenseBillingC
					JOIN Common.Sequense cs on cs.Id = bsd.IdSequense
					WHERE bs.IdForm = @IdForm AND bsd.IdOperatingUnit = @OperatingUnitId
				END

				IF (@idSequenceDetail IS NULL)
				BEGIN
					SELECT 999 as CodeMessage, 'Secuencia de Facturación Básica no encontrada' as Message, '' as Code, 0 as Id
					RETURN
				END

				SELECT @Code = dbo.GetSequence('', @pattern,@NextS)
				UPDATE Billing.BillingSequenceDetail SET [Next] += 1 WHERE Id = @idSequenceDetail

			END
							
			IF @Id = 0 or @Id is NULL --Se valida si es un nuevo registro o se actualiza uno existente
			BEGIN 
				INSERT INTO [Billing].[ProductAndServiceFee]([Code],[Name],[Status],[CreationUser],[CreationDate])
				VALUES (@Code,@Name,@Status,@CodeUser,[Common].[GETDATE]())

				SET @Id = SCOPE_IDENTITY()
			END
			ELSE BEGIN
				UPDATE [Billing].[ProductAndServiceFee] SET [Code] = @Code, [Name] = @Name,[Status] = @Status,
				[ModificationUser] = @CodeUser,[ModificationDate] = [Common].[GETDATE]()
				WHERE Id = @Id
			END
			
			
				--Insertamos los detalles tipo Producto
			DECLARE @TempProductId INT,
					@ProductFeeDetailId INT

			DECLARE InfoProductItem CURSOR FOR 
				SELECT TempId FROM @ProductFeeDetail WHERE Id = 0

			OPEN InfoProductItem 
			
			FETCH NEXT FROM InfoProductItem INTO @TempProductId

			WHILE @@fetch_status = 0
			BEGIN
				INSERT INTO [Billing].[ProductFeeDetail]([ProductAndServiceFeeId],[ProductId],[RateType],[PercentageType]
				,[Observations],[InitialDate],[FinalDate],[SalePrice],[Percentage])
				SELECT @Id,[ProductId],[RateType],IIF([RateType] = 1, [PercentageType], NULL), 
				[Observations],[InitialDate],[FinalDate],IIF([RateType] = 0, [SalePrice], NULL),IIF([RateType] = 1, [Percentage], NULL)
				FROM @ProductFeeDetail
				WHERE TempId = @TempProductId

				--Obtengo el id del detalle
				SET @ProductFeeDetailId = SCOPE_IDENTITY()

				FETCH NEXT FROM InfoProductItem INTO @TempProductId
			END
		
			CLOSE InfoProductItem
			DEALLOCATE InfoProductItem
			
			--Insertamos los detalles tipo Servicios

			DECLARE @TempServiceId INT,
					@ServiceFeeDetailId INT

			DECLARE InfoServiceItem CURSOR FOR 
				SELECT TempId FROM @ServiceFeeDetail WHERE Id = 0

			OPEN InfoServiceItem 
			
			FETCH NEXT FROM InfoServiceItem INTO @TempServiceId
			

			WHILE @@fetch_status = 0
			BEGIN
				INSERT INTO [Billing].[ServiceFeeDetail]([ProductAndServiceFeeId],[ServiceId],[Observations],
				[InitialDate],[FinalDate],[SalePrice])
				SELECT @Id,[ServiceId],[Observations],[InitialDate],[FinalDate],[SalePrice]
				FROM @ServiceFeeDetail
				WHERE TempId = @TempServiceId

				--Obtengo el id del detalle
				SET @ServiceFeeDetailId = SCOPE_IDENTITY()

				FETCH NEXT FROM InfoServiceItem INTO @TempServiceId
			END
			
			CLOSE InfoServiceItem
			DEALLOCATE InfoServiceItem
		
			--Insertamos los detalles tipo usuarios autorizados
			DECLARE @TempUserId INT,
					@UserDetailId INT

			DECLARE InfoUserItem CURSOR FOR 
				SELECT TempId FROM @ProductAndServiceFeeUserDetail WHERE Id = 0

			OPEN InfoUserItem 
		
			FETCH NEXT FROM InfoUserItem INTO @TempUserId

			WHILE @@fetch_status = 0
			BEGIN
				INSERT INTO [Billing].[ProductAndServiceFeeUser]([ProductAndServiceFeeId],[UserId],[UserCode])
				SELECT @Id,[UserId],[UserCode]
				FROM @ProductAndServiceFeeUserDetail
				WHERE TempId = @TempUserId

				--Obtengo el id del detalle
				SET @UserDetailId = SCOPE_IDENTITY()

				FETCH NEXT FROM InfoUserItem INTO @TempUserId
			END

			CLOSE InfoUserItem
			DEALLOCATE InfoUserItem

		---------------------------------------------------Actualizar--------------------------------------------------------------
			
			--Se Actualizan los detalles de productos, servicios y usuarios autorizados
			
				UPDATE pfd SET pfd.[ProductId] = pfdd.ProductId ,pfd.[RateType] = pfdd.RateType, 
				pfd.[PercentageType] = IIF(pfdd.RateType = 1, pfdd.PercentageType, NULL),pfd.[Observations] = pfdd.Observations, 
				pfd.[InitialDate] = pfdd.InitialDate, pfd.[FinalDate] = pfdd.FinalDate, 
				pfd.[SalePrice] = IIF(pfdd.RateType = 0, pfdd.SalePrice, NULL), pfd.Percentage = IIF(pfdd.RateType = 1, pfdd.Percentage, NULL)
				FROM [Billing].[ProductFeeDetail] pfd
				JOIN @ProductFeeDetail pfdd ON pfdd.Id = pfd.Id

				UPDATE sfd SET sfd.[ServiceId] = sfdd.ServiceId ,sfd.[Observations] = sfdd.Observations, sfd.[InitialDate] = sfdd.InitialDate,
				sfd.[FinalDate] = sfdd.FinalDate, sfd.[SalePrice] = sfdd.SalePrice
				FROM [Billing].[ServiceFeeDetail] sfd
				JOIN @ServiceFeeDetail sfdd ON sfdd.Id = sfd.Id

				UPDATE psf SET psf.[UserId] = psfd.UserId, psf.[UserCode] = psfd.UserCode
				FROM [Billing].[ProductAndServiceFeeUser] psf
				JOIN @ProductAndServiceFeeUserDetail psfd ON psfd.Id = psf.Id

			---------------------------------------------------------------------------------------------------------------

		SELECT 0 AS CodeMessage, 'Se guardó correctamente' AS Message, @Code as Code, @Id as Id		
	END TRY
	BEGIN CATCH
		SELECT 999 AS CodeMessage, ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)) AS Message, '' AS Code, 0 AS Id
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda o actualiza una tarifa de productos y servicios (portafolio de precios) en el módulo de facturación, incluyendo el detalle de precios por producto, por servicio y los usuarios habilitados para aplicar cada tarifa. Recibe toda la información en formato XML, procesa inserciones, actualizaciones y eliminaciones lógicas en las tablas de detalle de tarifas (ProductFeeDetail, ServiceFeeDetail y ProductAndServiceFeeUser), y si es un registro nuevo genera automáticamente el código consecutivo consultando la secuencia de numeración configurada en BillingSequence y BillingSequenceDetail según el ámbito (organización o unidad operativa). Permite administrar tarifarios y listas de precios aplicables en la facturación de productos y servicios, controlando vigencias y accesos por usuario.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_SaveProductAndServiceFee';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_SaveProductAndServiceFee';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Persiste (inserta/actualiza) la cabecera de tarifa de productos y servicios junto con sus detalles de productos, servicios y usuarios autorizados, generando el código consecutivo cuando es nuevo.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SaveProductAndServiceFee';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe contener el nodo /ProductAndServiceFee con Id, Code, Name, OperatingUnitId y Status.; Para nuevos registros (Code vacío o NULL) debe existir una configuración en Billing.BillingSequence para IdForm=''2827''.; Si el ámbito de la secuencia es por unidad operativa (Scope<>''O''), debe existir un BillingSequenceDetail asociado al OperatingUnitId recibido.; Los detalles enviados con Id>0 deben corresponder a registros existentes en sus tablas (ProductFeeDetail, ServiceFeeDetail, ProductAndServiceFeeUser).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SaveProductAndServiceFee';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Un detalle (producto/servicio/usuario) marcado con IsDelete=1 nunca se inserta ni se actualiza: solo se elimina.; En ProductFeeDetail, SalePrice y Percentage son mutuamente excluyentes según RateType (0=precio, 1=porcentaje).; Los nuevos detalles se identifican por Id=0 (entran al cursor de inserción); los demás van a la rutina de UPDATE.; La generación de Code solo ocurre cuando la cabecera no trae Code, evitando duplicar consecutivos en actualizaciones.; Toda fecha de creación/modificación se toma de Common.GETDATE() y no del cliente.; El IdForm de la secuencia para esta entidad está fijado en ''2827''.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SaveProductAndServiceFee';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[DELETE] Billing.ProductFeeDetail: Cuando un detalle de producto llega con IsDelete=1, se elimina por coincidencia de Id.; [DELETE] Billing.ServiceFeeDetail: Cuando un detalle de servicio llega con IsDelete=1, se elimina por coincidencia de Id.; [DELETE] Billing.ProductAndServiceFeeUser: Cuando un usuario autorizado llega con IsDelete=1, se elimina por coincidencia de Id.; [INSERT] Billing.ProductAndServiceFee: Si @Id=0 o NULL, inserta nueva cabecera con Code generado/recibido, registrando CreationUser=@CodeUser y CreationDate=Common.GETDATE().; [UPDATE] Billing.ProductAndServiceFee: Si @Id>0, actualiza Code, Name y Status, fijando ModificationUser=@CodeUser y ModificationDate=Common.GETDATE().; [UPDATE] Billing.BillingSequenceDetail: Tras generar un Code nuevo vía dbo.GetSequence, incrementa [Next] en 1 para el detalle de secuencia usado.; [INSERT] Billing.ProductFeeDetail: Para cada detalle de producto con Id=0, inserta asociado al @Id de cabecera; si RateType=1 guarda PercentageType y Percentage y deja SalePrice NULL; si RateType=0 guarda SalePrice y deja PercentageType y Percentage NULL.; [INSERT] Billing.ServiceFeeDetail: Para cada detalle de servicio con Id=0, inserta vinculado al @Id de cabecera con ServiceId, fechas, observaciones y SalePrice.; [INSERT] Billing.ProductAndServiceFeeUser: Para cada usuario autorizado con Id=0, inserta la asociación (ProductAndServiceFeeId, UserId, UserCode).; [UPDATE] Billing.ProductFeeDetail: Para detalles de producto existentes (match por Id), actualiza datos; PercentageType y Percentage solo se persisten si RateType=1, SalePrice solo si RateType=0 (en caso contrario quedan NULL).; [UPDATE] Billing.ServiceFeeDetail: Para detalles de servicio existentes (match por Id), actualiza ServiceId, Observations, InitialDate, FinalDate y SalePrice.; [UPDATE] Billing.ProductAndServiceFeeUser: Para usuarios autorizados existentes (match por Id), actualiza UserId y UserCode.; [RETURN_RESULT] resultset: Devuelve CodeMessage=999 con mensaje ''Secuencia de Facturación Básica no encontrada'' cuando no se encuentra el detalle de secuencia para IdForm=''2827''.; [RETURN_RESULT] resultset: Al finalizar exitosamente devuelve CodeMessage=0, Message=''Se guardó correctamente'', Code y Id de la cabecera.; [RETURN_RESULT] resultset: En el bloque CATCH devuelve CodeMessage=999 con ERROR_MESSAGE() concatenado con el número de línea.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SaveProductAndServiceFee';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Code = '''' o @Code IS NULL → Consulta Billing.BillingSequence para IdForm=''2827'' y genera el Code mediante dbo.GetSequence, incrementando el [Next] de la secuencia. else Usa el Code recibido sin generar consecutivo.; si @scope = ''O'' (ámbito por organización) → Obtiene patrón y consecutivo desde BillingSequenceDetail sin filtrar por unidad operativa. else Filtra BillingSequenceDetail por IdOperatingUnit = @OperatingUnitId (ámbito por unidad operativa).; si @idSequenceDetail IS NULL → Retorna error 999 ''Secuencia de Facturación Básica no encontrada'' y termina el procedimiento.; si @Id = 0 o NULL → INSERT en Billing.ProductAndServiceFee y obtiene el Id por SCOPE_IDENTITY(). else UPDATE de la cabecera existente por Id.; si RateType = 1 en el detalle de producto → Persiste PercentageType y Percentage, y deja SalePrice en NULL. else Persiste SalePrice y deja PercentageType y Percentage en NULL.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SaveProductAndServiceFee';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SaveProductAndServiceFee';
-- GO
