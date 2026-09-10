-- ================================================
/******* EJEMPLO DE LA ESTRUCTURA QUE DEBEN ENVIAR ***********/
--<Kardex>
--  <ThirdPartyId>654</ThirdPartyId>
--  <ProductId>94009</ProductId>
--  <MovementType>1</MovementType>
--  <WarehouseId>4</WarehouseId>
--  <Quantity>10</Quantity>
--  <Value>2500</Value>
--  <AffectAverageCost>1</AffectAverageCost>
--</Kardex>
--<Kardex>
--  <ThirdPartyId>798</ThirdPartyId>
--  <ProductId>389407</ProductId>
--  <MovementType>1</MovementType>
--  <WarehouseId>4</WarehouseId>
--  <BatchSerialId>2</BatchSerialId>
--  <Quantity>10</Quantity>
--  <Value>2500</Value>
--  <AffectAverageCost>1</AffectAverageCost>
--</Kardex>
-- =============================================
-- Author:		Cristhian Salazar
-- Create date: 27/01/2016
-- Description:	Store para afectar el inventario fisico y registrar en el kardex, es decir que este store lo deben consumir todos los procesos que hagan movimientos de productos
-- =============================================
CREATE PROCEDURE [Inventory].[SP_SavePhysicalInventoryKardex]
	@Kardex xml,
	@EntityId int,
	@EntityCode varchar(20),
	@EntityName as varchar(100),
	@User varchar(20),
	@ControlCost as bit = 1
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @CodeResult INT,
			@MessageResult VARCHAR(MAX)

	EXEC [Inventory].[SP_SavePhysicalInventoryKardex_Output] 
			@Kardex, 
			@EntityId, 
			@EntityCode, 
			@EntityName, 
			@User, 
			@ControlCost, 
			-----------------------------------------------
			@CodeResult OUTPUT, 
			@MessageResult OUTPUT

	SELECT	CAST(@CodeResult AS VARCHAR(20)) AS CodeMessage, 
			@MessageResult AS Message, 
			CAST(IIF(@CodeResult <> 0, 3, 1) AS TINYINT) Status
		
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra movimientos de inventario físico en el kardex de productos, afectando las existencias reales en bodega. Recibe un bloque XML con los detalles del movimiento (producto, bodega, tercero, cantidad, valor, tipo de movimiento y si afecta el costo promedio) y los datos de la entidad que genera el movimiento. Delega el procesamiento real al procedimiento SP_SavePhysicalInventoryKardex_Output y retorna un código de estado y mensaje de resultado indicando éxito o error. Es el punto central que deben invocar todos los procesos que generen entradas, salidas o ajustes de productos en inventario (compras, dispensación, traslados, ajustes de inventario físico).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SavePhysicalInventoryKardex';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SavePhysicalInventoryKardex';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Wrapper que delega la afectación del inventario físico y registro en kardex a un procedimiento interno y devuelve el resultado normalizado con código, mensaje y estado.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePhysicalInventoryKardex';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de movimientos debe respetar la estructura esperada (ThirdPartyId, ProductId, MovementType, WarehouseId, Quantity, Value, AffectAverageCost y opcionalmente BatchSerialId).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePhysicalInventoryKardex';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Toda afectación de inventario físico y kardex se centraliza en el procedimiento interno invocado, garantizando un único punto de entrada para movimientos de productos.; El resultado siempre se mapea a un Status binario de éxito/error a partir del CodeResult retornado.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePhysicalInventoryKardex';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Kardex; Inventario físico; Movimiento de productos; Bodega/Almacén; Lote/Serial; Costo promedio; Tercero', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePhysicalInventoryKardex';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve siempre un resultset con CodeMessage, Message y Status; Status=1 cuando CodeResult=0 (éxito) y Status=3 cuando CodeResult<>0 (error).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePhysicalInventoryKardex';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @CodeResult <> 0 → Status devuelto = 3 (error) else Status devuelto = 1 (éxito)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePhysicalInventoryKardex';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Inventory.SP_SavePhysicalInventoryKardex_Output', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePhysicalInventoryKardex';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePhysicalInventoryKardex';
-- GO
