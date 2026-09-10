-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-10-10
-- Description:	Procedimiento que se encarga de guardar y confirmar el cruce de anticipo vs cxc
-- =============================================
CREATE PROCEDURE [Portfolio].[SP_SavePortfolioTransfer]
	@PortfolioTransferXml XML,
	@CodeUser VARCHAR(20),
	------------------------------------------------------
	@CompanyType TINYINT
AS
BEGIN
    SET NOCOUNT ON
	
	DECLARE @CodeResult INT,
			@MessageResult VARCHAR(MAX),
			@Id INT,
			@Code VARCHAR(20)

	EXEC [Portfolio].[SP_SavePortfolioTransfer_Output] @PortfolioTransferXml, @CodeUser, @CompanyType, @CodeResult OUTPUT, @MessageResult OUTPUT, @Id OUTPUT, @Code OUTPUT

	SELECT 
		@CodeResult AS CodeResult, 
		@MessageResult AS MessageResult, 
		@Id as Id, 
		@Code as Code    
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que guarda y confirma el cruce (aplicación) de anticipos contra cuentas por cobrar en la cartera de la institución. Recibe los datos del traslado de cartera en formato XML junto con el usuario que ejecuta la operación y el tipo de empresa, luego delega el procesamiento real al procedimiento interno SP_SavePortfolioTransfer_Output. Retorna un código de resultado, un mensaje de estado, el identificador y el código del registro generado, permitiendo al sistema informar al usuario si la operación de cruce fue exitosa o si ocurrió algún error.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_SavePortfolioTransfer';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_SavePortfolioTransfer';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'"Wrapper que ejecuta el guardado y confirmación del cruce de anticipo contra cuentas por cobrar delegando en SP_SavePortfolioTransfer_Output y retorna el resultado como conjunto de datos."', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SavePortfolioTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se requiere un XML con la información del cruce a guardar.; Se requiere identificar al usuario que ejecuta la operación y el tipo de compañía para que el procedimiento subyacente pueda procesar el cruce.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SavePortfolioTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La lógica de persistencia se delega íntegramente al procedimiento Portfolio.SP_SavePortfolioTransfer_Output; este wrapper no modifica datos por sí mismo.; Siempre devuelve un único result set con las columnas CodeResult, MessageResult, Id y Code provenientes de los OUTPUT del procedimiento invocado.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SavePortfolioTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'anticipo; cuentas por cobrar (CxC); cruce de anticipo vs CxC; transferencia de cartera', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SavePortfolioTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (result set): Tras ejecutar SP_SavePortfolioTransfer_Output, se retorna un SELECT con CodeResult, MessageResult, Id y Code obtenidos de los parámetros OUTPUT.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SavePortfolioTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Portfolio.SP_SavePortfolioTransfer_Output', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SavePortfolioTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SavePortfolioTransfer';
-- GO
