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