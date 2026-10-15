-- =============================================
-- Author:		Juan Carlos Bermudez Gutierrez
-- Create date: 21-10-2015
-- Description:	sp que se ejecuta al confirmar un comprobante de entrada
-- =============================================
CREATE PROCEDURE [Inventory].[SP_PhysicalInventory]
@Id As int,
@Document as varchar(250),
@User as varchar(20),
@ContainerNameCrystal as varchar(20),
@ControlCost as bit = 0

AS
BEGIN
	SET NOCOUNT ON;

	DECLARE	@return_value int

	BEGIN TRY
			
		if @Document = 'RemissionEntrance' 
		BEGIN
			EXEC [Inventory].[SP_ConfirmRemissionEntrance] 
			@Id, 
			@User,
			@ControlCost
			return
		END

		if @Document = 'ConsignmentInventoryRemission' 
		BEGIN
			EXEC [Inventory].[SP_ConfirmConsignmentInventoryRemission] 
			@Id, 
			@User,
			@ControlCost
			return
		END

		if @Document = 'EntranceVoucher' 
		BEGIN
			EXEC [Inventory].[SP_ConfirmEntranceVoucher] 
			@Id, 
			@User,
			@ContainerNameCrystal,
			@ControlCost
			return
		END

		select convert(bit, 0) as StatusResult, 'Error: Documento no controlado ' as MessageResult

	END TRY
	BEGIN CATCH
		select convert(bit, 0) as StatusResult, 'Error ! '+ ERROR_MESSAGE() as MessageResult
	END CATCH

	END