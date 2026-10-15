-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2021-03-18
-- Description:	Obtener la secuencia numerica
-- =============================================
CREATE PROCEDURE [Common].[SP_CalculateFormule]
	@Formulate VARCHAR(MAX),
	@XmlVariables XML,
	---------------------------------------------------------------------------
	@CodeResult INT OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT,
	@Result DECIMAL(18, 2) OUTPUT
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @TheSQL NVARCHAR(MAX)

	DECLARE @Variables AS TABLE
	(
		Pattern VARCHAR(500),
		Value DECIMAL(18,2)
	)

	BEGIN TRY

		INSERT INTO @Variables
			SELECT	t.x.value('Pattern[1]','varchar(500)'),
					t.x.value('Value[1]','decimal(18,2)')
			FROM @XmlVariables.nodes('/Data') t(x)

		/************************************************* VARIABLES *************************************************/

		SELECT @Formulate = REPLACE(@Formulate, Pattern, Value)
		FROM @Variables

		SET @TheSQL = CONCAT('SELECT @Result = ', @Formulate)

		EXEC sp_executesql @TheSQL, N'@Result DECIMAL(18,2) OUTPUT', @Result = @Result OUTPUT

		IF @@ROWCOUNT > 1
		BEGIN
			SELECT @CodeResult = 999, 
					@MessageResult = CONCAT('La Formula: ''',@Formulate, ''' no es valida')
			RETURN
		END

		/************************************************* RESULTADO *************************************************/

		SELECT @CodeResult = 0,
			   @MessageResult = ''
	END TRY
	BEGIN CATCH
		SELECT @CodeResult = 999, 
			   @MessageResult = CONCAT('SP_CalculateFormule: ', ERROR_MESSAGE(), ' - Linea: ', ERROR_LINE())
	END CATCH
END