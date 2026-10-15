
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