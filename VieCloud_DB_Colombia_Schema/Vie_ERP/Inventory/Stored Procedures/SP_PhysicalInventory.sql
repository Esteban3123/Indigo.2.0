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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento central de inventario físico que actúa como enrutador (dispatcher) para confirmar distintos tipos de documentos de inventario. Según el tipo de documento recibido, deriva la ejecución a uno de tres subprocesos: confirmación de entrada por remisión (SP_ConfirmRemissionEntrance), confirmación de remisión de inventario en consignación (SP_ConfirmConsignmentInventoryRemission) o confirmación de comprobante de entrada (SP_ConfirmEntranceVoucher). Recibe el identificador del documento, el usuario que confirma, el nombre del reporte Crystal asociado y un indicador de control de costos, garantizando que cada movimiento de inventario quede correctamente procesado y validado según su naturaleza.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_PhysicalInventory';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_PhysicalInventory';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Enrutador de confirmación de comprobantes de entrada de inventario que delega al procedimiento específico según el tipo de documento, devolviendo error controlado si no es soportado.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_PhysicalInventory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El tipo de documento debe ser uno de los soportados (''RemissionEntrance'', ''ConsignmentInventoryRemission'' o ''EntranceVoucher''); Debe existir el registro identificado para el tipo de documento indicado, requerido por los SP de confirmación delegados', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_PhysicalInventory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se procesan tres tipos de documento: RemissionEntrance, ConsignmentInventoryRemission y EntranceVoucher; cualquier otro retorna error controlado; Cualquier excepción se captura y se devuelve como resultado con StatusResult=0, evitando propagar el error; El parámetro de contenedor de Crystal solo se propaga al confirmar EntranceVoucher', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_PhysicalInventory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Comprobante de entrada de inventario; Remisión de entrada; Remisión de inventario en consignación; Control de costos', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_PhysicalInventory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Inventory.SP_ConfirmRemissionEntrance: Cuando @Document=''RemissionEntrance'' se delega la confirmación al SP correspondiente y se retorna; [RETURN_RESULT] Inventory.SP_ConfirmConsignmentInventoryRemission: Cuando @Document=''ConsignmentInventoryRemission'' se delega la confirmación al SP correspondiente y se retorna; [RETURN_RESULT] Inventory.SP_ConfirmEntranceVoucher: Cuando @Document=''EntranceVoucher'' se delega la confirmación al SP correspondiente pasando el contenedor Crystal y se retorna; [RETURN_RESULT] (resultset): Si el documento no coincide con ninguno de los tipos controlados, retorna StatusResult=0 y mensaje ''Error: Documento no controlado''; [RETURN_RESULT] (resultset): Ante cualquier excepción en TRY, retorna StatusResult=0 y ''Error ! '' concatenado con ERROR_MESSAGE()', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_PhysicalInventory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Documento = ''RemissionEntrance'' → Ejecuta SP_ConfirmRemissionEntrance y termina; si Documento = ''ConsignmentInventoryRemission'' → Ejecuta SP_ConfirmConsignmentInventoryRemission y termina; si Documento = ''EntranceVoucher'' → Ejecuta SP_ConfirmEntranceVoucher (incluye contenedor Crystal) y termina else Retorna StatusResult=0 con mensaje ''Error: Documento no controlado''', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_PhysicalInventory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Inventory.SP_ConfirmRemissionEntrance; Inventory.SP_ConfirmConsignmentInventoryRemission; Inventory.SP_ConfirmEntranceVoucher', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_PhysicalInventory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_PhysicalInventory';
-- GO
