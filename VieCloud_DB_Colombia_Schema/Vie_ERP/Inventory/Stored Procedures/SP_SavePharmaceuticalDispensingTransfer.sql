-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-08-18
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar un traslado de dispensación por ingreso
-- =============================================
CREATE PROCEDURE [Inventory].[SP_SavePharmaceuticalDispensingTransfer]
    @PharmaceuticalDispensingTransferXml AS XML,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @CodeResult INT,
			@MessageResult VARCHAR(MAX),
			@Id INT,
			@Code VARCHAR(20)

	EXEC [Inventory].[SP_SavePharmaceuticalDispensingTransfer_Output] 
		@PharmaceuticalDispensingTransferXml, 
		@CodeUser, 
		--------------------------------------------
		@CodeResult OUTPUT, 
		@MessageResult OUTPUT, 
		--------------------------------------------
		@Id OUTPUT, 
		@Code OUTPUT

	SELECT	@CodeResult AS CodeResult, 
			@MessageResult AS MessageResult, 
			@Id as Id, 
			@Code as Code
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que permite guardar, actualizar o confirmar un traslado de dispensación farmacéutica asociado a un ingreso de paciente. Recibe los datos del traslado en formato XML junto con el código del usuario que realiza la operación, y delega el procesamiento real al procedimiento interno SP_SavePharmaceuticalDispensingTransfer_Output. Retorna el resultado de la operación con un código de estado, un mensaje descriptivo, el identificador interno generado y el código del traslado registrado. Se utiliza en el módulo de inventario y farmacia para gestionar el movimiento de medicamentos entre unidades o servicios durante la dispensación por ingreso hospitalario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SavePharmaceuticalDispensingTransfer';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SavePharmaceuticalDispensingTransfer';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'"Wrapper que invoca la lógica de guardar/actualizar/confirmar un traslado de dispensación farmacéutica por ingreso y expone el resultado (código, mensaje, Id y Código generado) como un resultset."', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePharmaceuticalDispensingTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe recibir el XML con la información del traslado de dispensación farmacéutica y el código del usuario que ejecuta la acción.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePharmaceuticalDispensingTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Delega íntegramente la lógica de guardado/actualización/confirmación al procedimiento _Output, actuando solo como wrapper que materializa los OUTPUT como resultset.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePharmaceuticalDispensingTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'traslado de dispensación; dispensación farmacéutica; ingreso', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePharmaceuticalDispensingTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Tras ejecutar SP_SavePharmaceuticalDispensingTransfer_Output, retorna un SELECT con CodeResult, MessageResult, Id y Code provenientes de los parámetros OUTPUT.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePharmaceuticalDispensingTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Inventory.SP_SavePharmaceuticalDispensingTransfer_Output', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePharmaceuticalDispensingTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePharmaceuticalDispensingTransfer';
-- GO
