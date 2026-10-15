-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-04-29
-- Description:	Procedimiento que se encarga de actualizar el campo de import en la tabla CostLogisticsProductionCenterRecordDetail
-- =============================================
CREATE PROCEDURE [Cost].[SP_UpdateFieldImportCost_Output]
	@ObjectXml AS XML,
	------------------------------------------------------
	@CodeMessageResult Int OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	--Tabla para almacenar los items del listado que viene en el xml y poder actualizar el campo en la tabla
	DECLARE @TableIds TABLE (Id INT)

	BEGIN TRY
		--Se obtiene los detalles que vienen en el xml
		INSERT INTO @TableIds
			SELECT	t.x.value('Id[1]','int') as Id
			FROM @ObjectXml.nodes('/Data') t(x)

		--Actualizo los registros si hay datos
		IF EXISTS (SELECT 1 from @TableIds)
		BEGIN
			UPDATE lpcrd 
				SET lpcrd.Import = 1
			FROM Cost.CostLogisticsProductionCenterRecordDetail lpcrd 
			JOIN @TableIds ti ON ti.Id = lpcrd.Id
		END

		SELECT @CodeMessageResult = 0, 
			   @MessageResult = 'Se actualizó correctamente'
	END TRY
	BEGIN CATCH
		SELECT @CodeMessageResult = 999, 
			   @MessageResult = ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10))
	END CATCH
END