-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-09-16
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar una distribución de mano de obra
-- =============================================
CREATE PROCEDURE [Cost].[SP_SaveCostDistributionManpower]
    @CostDistributionManpowerXml AS XML,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @CodeMessage INT,
			@Message VARCHAR(MAX),
			@Id INT,
			@Code VARCHAR(20)

	EXEC [Cost].[SP_SaveCostDistributionManpower_Output] @CostDistributionManpowerXml, @CodeUser, @CodeMessage OUTPUT, @Message OUTPUT, @Id OUTPUT, @Code OUTPUT

	SELECT 
		@CodeMessage AS CodeMessage, 
		@Message AS Message, 
		@Id as Id, 
		@Code as Code
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que permite guardar, actualizar o confirmar una distribución de mano de obra en el módulo de costos. Recibe los datos de la distribución en formato XML junto con el código del usuario que realiza la operación, y delega el procesamiento real al procedimiento interno SP_SaveCostDistributionManpower_Output. Retorna un código de mensaje, una descripción del resultado, el identificador generado y un código asociado, permitiendo al sistema informar si la operación fue exitosa o si ocurrió algún error. Es el punto de entrada principal para gestionar la asignación y distribución del costo de recurso humano entre las unidades o centros de costo de la organización.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_SaveCostDistributionManpower';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_SaveCostDistributionManpower';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Wrapper que delega en un procedimiento interno la persistencia (guardar/actualizar/confirmar) de una distribución de mano de obra y devuelve el resultado al cliente.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCostDistributionManpower';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe proveer un XML con la información de la distribución de mano de obra; Se debe proveer el código del usuario que ejecuta la operación', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCostDistributionManpower';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La lógica de guardado/actualización/confirmación se delega íntegramente al procedimiento _Output; Siempre devuelve un único resultset con los cuatro campos de salida', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCostDistributionManpower';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Distribución de costos; Mano de obra', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCostDistributionManpower';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Tras invocar el procedimiento interno, retorna un resultset con CodeMessage, Message, Id y Code obtenidos como salida', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCostDistributionManpower';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Cost.SP_SaveCostDistributionManpower_Output', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCostDistributionManpower';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCostDistributionManpower';
-- GO
