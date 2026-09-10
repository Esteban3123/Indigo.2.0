

-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 16/02/2016
-- Description:	Procedimiento que se encarga de guardar, actualizar la liquidación de honorarios médicos
-- =============================================
CREATE PROCEDURE [FixedAsset].[SP_SaveFixedAssetInitialBalance] 
	@FixedAssetInitialBalanceItemPartsDetailBookXml as Xml,
	@FixedAssetInitialBalanceItemPartsXml as Xml,
	@FixedAssetInitialBalanceItemDetailBookXml as Xml,
	@FixedAssetInitialBalanceItemXml as Xml,
	@CodeUser as varchar(20)
AS
BEGIN

	--Tabla temporal para obtener el listado de ids de eliminados de FixedAssetInitialBalanceItemPartsDetailBook
	declare @FixedAssetInitialBalanceItemPartsDetailBookTemp table(Id int)
	--Tabla temporal para obtener el listado de ids de eliminados de FixedAssetInitialBalanceItemParts
	declare @FixedAssetInitialBalanceItemPartsTemp table(Id int)
	--Tabla temporal para obtener el listado de ids de eliminados de FixedAssetInitialBalanceItemDetailBook
	declare @FixedAssetInitialBalanceItemDetailBookTemp table(Id int)
	--Tabla temporal para obtener el listado de ids de eliminados de FixedAssetInitialBalanceItem
	declare @FixedAssetInitialBalanceItemTemp table(Id int)

	Begin try
	
		--Se obtienen los ids de los xml

		--Se obtiene los detalles del xml(FixedAssetInitialBalanceItemPartsDetailBook)
		insert into @FixedAssetInitialBalanceItemPartsDetailBookTemp
		select 
		t.x.value('Id[1]','int') as Id
		from @FixedAssetInitialBalanceItemPartsDetailBookXml.nodes('/ListDeleteFixedAssetInitialBalanceItemPartsDetailBook') t(x)

		--Se obtiene los detalles del xml(FixedAssetInitialBalanceItemParts)
		insert into @FixedAssetInitialBalanceItemPartsTemp
		select 
		t.x.value('Id[1]','int') as Id
		from @FixedAssetInitialBalanceItemPartsXml.nodes('/ListDeleteFixedAssetInitialBalanceItemParts') t(x)

		--Se obtiene los detalles del xml(FixedAssetInitialBalanceItemDetailBook)
		insert into @FixedAssetInitialBalanceItemDetailBookTemp
		select 
		t.x.value('Id[1]','int') as Id
		from @FixedAssetInitialBalanceItemDetailBookXml.nodes('/ListDeleteFixedAssetInitialBalanceItemDetailBook') t(x)

		--Se obtiene los detalles del xml(FixedAssetInitialBalanceItem)
		insert into @FixedAssetInitialBalanceItemTemp
		select 
		t.x.value('Id[1]','int') as Id
		from @FixedAssetInitialBalanceItemXml.nodes('/ListDeleteFixedAssetInitialBalanceItem') t(x)

		
		--Se eliminan los registros en cascada

		--Eliminación FixedAssetInitialBalanceItemPartsDetailBook
		delete [FixedAsset].[FixedAssetInitialBalanceItemPartsDetailBook] where Id in (select Id from @FixedAssetInitialBalanceItemPartsDetailBookTemp where Id > 0)

		--Eliminación FixedAssetInitialBalanceItemParts
		delete [FixedAsset].[FixedAssetInitialBalanceItemParts] where Id in (select Id from @FixedAssetInitialBalanceItemPartsTemp where Id > 0)

		--Eliminación FixedAssetInitialBalanceItemDetailBook
		delete [FixedAsset].[FixedAssetInitialBalanceItemDetailBook] where Id in (select Id from @FixedAssetInitialBalanceItemDetailBookTemp where Id > 0)

		--Eliminación FixedAssetInitialBalanceItem
		delete [FixedAsset].[FixedAssetInitialBalanceItem] where Id in (select Id from @FixedAssetInitialBalanceItemTemp where Id > 0)

		select 0 as CodeMessage, 'Se eliminó correctamente' as Message
		
	end try
	begin catch
		select 999 as CodeMessage, ERROR_MESSAGE() as Message
	end catch

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que gestiona la eliminación en cascada del saldo inicial de activos fijos. Recibe por XML los identificadores de los registros a eliminar en cuatro niveles jerárquicos: ítems del saldo inicial (FixedAssetInitialBalanceItem), su detalle contable por libro legal (FixedAssetInitialBalanceItemDetailBook), los componentes o partes del activo (FixedAssetInitialBalanceItemParts) y el detalle contable por libro legal de cada parte (FixedAssetInitialBalanceItemPartsDetailBook). Elimina primero los niveles más detallados para respetar la integridad referencial y luego los niveles superiores, garantizando la consistencia del saldo inicial de activos fijos en el módulo contable.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'PROCEDURE', @level1name = N'SP_SaveFixedAssetInitialBalance';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'PROCEDURE', @level1name = N'SP_SaveFixedAssetInitialBalance';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Elimina en cascada registros de saldos iniciales de activos fijos (ítems, partes y libros de detalle) a partir de listas de Ids recibidas en estructuras XML.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SaveFixedAssetInitialBalance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los XML de entrada deben contener nodos raíz ListDeleteFixedAssetInitialBalanceItemPartsDetailBook, ListDeleteFixedAssetInitialBalanceItemParts, ListDeleteFixedAssetInitialBalanceItemDetailBook y ListDeleteFixedAssetInitialBalanceItem con elementos Id enteros.; Los Ids a eliminar deben ser mayores que 0 para ser considerados.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SaveFixedAssetInitialBalance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se procesan eliminaciones; no realiza inserciones ni actualizaciones pese a la descripción del encabezado.; Nunca elimina registros con Id <= 0.; Las eliminaciones se realizan en orden de dependencia: primero detalle de partes, luego partes, luego detalle del ítem y finalmente el ítem.; Los errores no se propagan; siempre se retorna un resultset con CodeMessage y Message.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SaveFixedAssetInitialBalance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Activo fijo; Saldo inicial de activo fijo; Ítem de saldo inicial; Partes del activo fijo; Libro de detalle contable', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SaveFixedAssetInitialBalance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[DELETE] FixedAsset.FixedAssetInitialBalanceItemPartsDetailBook: Elimina filas cuyo Id esté en la lista extraída del XML de partes-detalle libro y sea > 0.; [DELETE] FixedAsset.FixedAssetInitialBalanceItemParts: Elimina filas cuyo Id esté en la lista extraída del XML de partes y sea > 0.; [DELETE] FixedAsset.FixedAssetInitialBalanceItemDetailBook: Elimina filas cuyo Id esté en la lista extraída del XML de detalle libro y sea > 0.; [DELETE] FixedAsset.FixedAssetInitialBalanceItem: Elimina filas cuyo Id esté en la lista extraída del XML de ítems y sea > 0.; [RETURN_RESULT] -: Si la transacción es exitosa retorna CodeMessage=0 con mensaje ''Se eliminó correctamente''; ante excepción retorna CodeMessage=999 con ERROR_MESSAGE().', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SaveFixedAssetInitialBalance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Id > 0 en cada tabla temporal → Se incluye el Id en el DELETE correspondiente else Se ignora el Id (no se elimina); si Ocurre excepción dentro del TRY → Se devuelve CodeMessage=999 con el mensaje de error else Se devuelve CodeMessage=0 con mensaje de éxito', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SaveFixedAssetInitialBalance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SaveFixedAssetInitialBalance';
-- GO
