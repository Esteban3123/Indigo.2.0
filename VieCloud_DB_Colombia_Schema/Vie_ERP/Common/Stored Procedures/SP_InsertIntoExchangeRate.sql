
-- =============================================
-- Author:      Giovanny Plazas
-- Create Date: 27/12/2022
-- Description: Store Procedure que se encarga de guardar en la tabla de tasa de cambio, de la respectiva tabla
-- =============================================
CREATE PROCEDURE [Common].[SP_InsertIntoExchangeRate]

    @EntityName as varchar(50),
	@EntityId as int,
	@EntityCurrencyId as int,
	@StateResult INT = NULL OUTPUT,
	@MessageOutput VARCHAR(100) = NULL OUTPUT
------------------------------------------------------
AS
SET
  ANSI_NULLS,
  QUOTED_IDENTIFIER,
  CONCAT_NULL_YIELDS_NULL,
  ANSI_WARNINGS,
  ANSI_PADDING
ON;
BEGIN
	
	EXEC [Common].[SP_InsertIntoExchangeRateWithDate]  @EntityName,@EntityId,@EntityCurrencyId,NULL,@StateResult=@StateResult OUTPUT, @MessageOutput = @MessageOutput OUTPUT
	 
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra la tasa de cambio de moneda para una entidad de negocio específica (identificada por nombre e ID) sin requerir una fecha explícita, usando la fecha actual por defecto. Actúa como un atajo o versión simplificada del procedimiento SP_InsertIntoExchangeRateWithDate, al que delega la operación pasando NULL como fecha para que ese procedimiento tome la fecha del sistema. Retorna un código de estado y un mensaje de resultado indicando si el registro fue exitoso o si ocurrió algún error. Se usa en procesos contables y financieros donde se necesita registrar el tipo de cambio de una divisa asociada a una entidad (por ejemplo, una empresa, sede o contrato) en el momento actual.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'SP_InsertIntoExchangeRate';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'SP_InsertIntoExchangeRate';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Wrapper que registra la tasa de cambio para una entidad y moneda delegando en el procedimiento que acepta fecha, invocándolo con fecha NULL.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_InsertIntoExchangeRate';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el procedimiento Common.SP_InsertIntoExchangeRateWithDate accesible con la firma esperada.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_InsertIntoExchangeRate';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Siempre delega el registro de la tasa de cambio al procedimiento con fecha, pasando NULL como fecha (lo que implica usar la fecha por defecto definida en el procedimiento delegado).', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_InsertIntoExchangeRate';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Tasa de cambio; Entidad; Moneda', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_InsertIntoExchangeRate';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Common.SP_InsertIntoExchangeRateWithDate: Siempre invoca al SP delegado pasando fecha NULL y retorna sus parámetros de salida (estado y mensaje).', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_InsertIntoExchangeRate';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.SP_InsertIntoExchangeRateWithDate', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_InsertIntoExchangeRate';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_InsertIntoExchangeRate';
-- GO
