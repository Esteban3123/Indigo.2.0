CREATE PROCEDURE [Billing].[SP_ReversePortfolioByBasicBillingIdAsList]
	@OperatingUnitId INT,
	@BasicBillingId INT,
	@AnnulmentDate DATETIME,
	@ReversalReasonDescription VARCHAR(300),
	@CodeUser VARCHAR(20),
	@CompanyType TINYINT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @CodeResult VARCHAR(20), @MessageResult VARCHAR(MAX)

	EXEC [Billing].[SP_ReversePortfolioByBasicBillingId_Output] @OperatingUnitId, @BasicBillingId, @AnnulmentDate, @ReversalReasonDescription, @CodeUser, @CompanyType, @CodeResult OUTPUT, @MessageResult OUTPUT

	SELECT @CodeResult AS Code, @MessageResult AS Message
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que realiza la reversión o anulación de una cartera de facturación a partir de un identificador de factura base (BasicBillingId), para una unidad operativa y tipo de empresa específicos. Registra la fecha de anulación, el motivo de la reversión y el usuario que ejecuta la operación, tocando directamente la entidad de cartera y facturación. Internamente delega toda la lógica de reversión al procedimiento SP_ReversePortfolioByBasicBillingId_Output, capturando su resultado (código y mensaje de respuesta) y devolviéndolo como resultado de la consulta. Sirve como punto de entrada para anular facturas o cuentas de cobro en el módulo de cartera, retornando el estado de la operación para su uso en listas o procesos masivos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_ReversePortfolioByBasicBillingIdAsList';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_ReversePortfolioByBasicBillingIdAsList';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Wrapper que ejecuta la reversión de cartera de una facturación básica delegando en el SP _Output y devuelve su código y mensaje de resultado como conjunto de resultados tabular.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReversePortfolioByBasicBillingIdAsList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El resultado de la operación se expone siempre como un único conjunto de filas con columnas Code y Message, independientemente del éxito o fallo del procedimiento subyacente.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReversePortfolioByBasicBillingIdAsList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Reversión de cartera; Facturación básica; Unidad operativa; Anulación; Motivo de reversión', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReversePortfolioByBasicBillingIdAsList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Tras invocar SP_ReversePortfolioByBasicBillingId_Output, retorna un SELECT con las columnas Code y Message provenientes de los parámetros OUTPUT @CodeResult y @MessageResult.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReversePortfolioByBasicBillingIdAsList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Billing.SP_ReversePortfolioByBasicBillingId_Output', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReversePortfolioByBasicBillingIdAsList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReversePortfolioByBasicBillingIdAsList';
-- GO
