-- =============================================
-- Author:		Cristhian Mauricio Salazar Narvaez
-- Create date: 26/01/2016
-- Description:	Procedimiento almacena para guardar o confirmar las devoluciones
-- =============================================
CREATE PROCEDURE [Inventory].[SP_GeneratePharmaceuticalDevolution]
	@DevolutionXml xml,
	@AnnulateXml xml,
	@User varchar(20)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @CodeMessage VARCHAR(10),
			@Message VARCHAR(MAX),
			@DevolutionId INT,
			@Status TINYINT

	EXEC [Inventory].[SP_GeneratePharmaceuticalDevolution_Output] 
		@DevolutionXml, 
		@AnnulateXml, 
		@User, 
		--------------------------------------------
		@CodeMessage OUTPUT, 
		@Message OUTPUT, 
		--------------------------------------------
		@DevolutionId OUTPUT, 
		@Status OUTPUT

	SELECT	@CodeMessage AS CodeMessage, 
			@Message AS Message, 
			@DevolutionId as DevolutionId, 
			@Status as Status
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que registra o confirma devoluciones farmacéuticas en el módulo de inventario. Recibe como parámetros un XML con los datos de la devolución, un XML con los ítems a anular y el usuario que ejecuta la operación. Delega el procesamiento real al procedimiento interno SP_GeneratePharmaceuticalDevolution_Output y retorna el resultado con un código de mensaje, descripción, el identificador de la devolución generada y su estado. Se utiliza para gestionar el flujo de devolución de medicamentos o dispositivos farmacéuticos, ya sea creando una nueva devolución o anulando movimientos de inventario existentes.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_GeneratePharmaceuticalDevolution';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_GeneratePharmaceuticalDevolution';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Wrapper que invoca el procesamiento de devoluciones farmacéuticas (guardado/confirmación) y devuelve como result set el código de mensaje, mensaje, identificador de devolución y estado resultantes.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePharmaceuticalDevolution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe recibir los XML de devolución y de anulación, junto con el usuario que ejecuta la operación, para que el procedimiento subyacente pueda procesarlos.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePharmaceuticalDevolution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Actúa como wrapper: delega la lógica de guardado/confirmación de devoluciones al procedimiento _Output y retorna sus salidas como un único result set.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePharmaceuticalDevolution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'devolución farmacéutica; anulación', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePharmaceuticalDevolution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (result set): Tras ejecutar SP_GeneratePharmaceuticalDevolution_Output, se retorna un SELECT con CodeMessage, Message, DevolutionId y Status obtenidos como parámetros OUTPUT.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePharmaceuticalDevolution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Inventory.SP_GeneratePharmaceuticalDevolution_Output', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePharmaceuticalDevolution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePharmaceuticalDevolution';
-- GO
