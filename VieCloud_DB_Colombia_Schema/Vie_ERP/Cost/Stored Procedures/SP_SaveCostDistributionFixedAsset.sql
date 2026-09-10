-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-09-18
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar una distribución de Activos Fijos
-- =============================================
CREATE PROCEDURE [Cost].[SP_SaveCostDistributionFixedAsset]
    @CostDistributionFixedAssetXml AS XML,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @CodeMessage INT,
			@Message VARCHAR(MAX),
			@Id INT,
			@Code VARCHAR(20)

	EXEC [Cost].[SP_SaveCostDistributionFixedAsset_Output] @CostDistributionFixedAssetXml, @CodeUser, @CodeMessage OUTPUT, @Message OUTPUT, @Id OUTPUT, @Code OUTPUT

	SELECT 
		@CodeMessage AS CodeMessage, 
		@Message AS Message, 
		@Id as Id, 
		@Code as Code
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que permite guardar, actualizar o confirmar una distribución de costos de Activos Fijos en el módulo de costos. Recibe la información de distribución en formato XML junto con el código del usuario que ejecuta la operación, y delega el procesamiento real al procedimiento interno SP_SaveCostDistributionFixedAsset_Output, del cual obtiene el resultado de la operación (código de mensaje, mensaje descriptivo, identificador y código generado). Retorna al cliente el resultado de la transacción indicando si la distribución de activos fijos fue procesada exitosamente o si ocurrió algún error.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_SaveCostDistributionFixedAsset';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_SaveCostDistributionFixedAsset';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Wrapper que delega el guardado/actualización/confirmación de una distribución de costos de activos fijos y devuelve el resultado como conjunto de datos.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCostDistributionFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La lógica de negocio se delega íntegramente al procedimiento _Output; este wrapper solo expone sus salidas como result set.; SET NOCOUNT ON evita el envío de mensajes de filas afectadas al cliente.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCostDistributionFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Distribución de costos; Activos fijos', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCostDistributionFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ResultSet: Tras ejecutar el procedimiento interno, retorna en un SELECT los valores de CodeMessage, Message, Id y Code obtenidos como OUTPUT.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCostDistributionFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Cost.SP_SaveCostDistributionFixedAsset_Output', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCostDistributionFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCostDistributionFixedAsset';
-- GO
