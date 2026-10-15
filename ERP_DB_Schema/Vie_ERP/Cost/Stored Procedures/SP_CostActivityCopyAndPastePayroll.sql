-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-04-03
-- Description:	Procedimiento que se encarga de el Copy & Paste de los cargos desde el formulario Actividades de Costo
-- =============================================
CREATE PROCEDURE [Cost].[SP_CostActivityCopyAndPastePayroll] 
	@XmlObject as Xml
AS
BEGIN
	SET NOCOUNT ON

	/************************************* VARIABLES ************************************/
	
	--Tabla para almacenar los items del listado que viene en el xml y los resultados a devolver
	DECLARE @TableXmlObject TABLE
	(
		Id INT IDENTITY PRIMARY KEY, 
		CostActivityStepId INT,
		PayrollPositionId INT,
		PayrollPositionCode VARCHAR(500),
		PayrollPositionName VARCHAR(500),
		Hours DECIMAL(24,6),
		--------------------------------
		StatusField INT DEFAULT(99), 
		MessageField VARCHAR(MAX)
	)

	BEGIN TRY

		INSERT INTO @TableXmlObject
			(CostActivityStepId, PayrollPositionCode, Hours)
			SELECT 
				t.x.value('CostActivityStepId[1]','int') as CostActivityStepId,
				t.x.value('PayrollPositionCode[1]','varchar(500)') as PayrollPositionCode,
				t.x.value('Hours[1]','decimal(24,6)') as Hours
			FROM @XmlObject.nodes('/Data/Row') t(x)

		UPDATE tXml
			SET tXml.StatusField = 999,
				tXml.MessageField = 'El Cargo con Código ' + tXml.PayrollPositionCode + ' no existe'
		FROM @TableXmlObject tXml
		LEFT JOIN Payroll.Position p ON tXml.PayrollPositionCode = p.Code
		WHERE p.Id IS NULL

		UPDATE tXml
			SET tXml.StatusField = 999,
				tXml.MessageField = 'Las Horas del Cargo con Código ' + tXml.PayrollPositionCode + ' no pueden ser igual o menor a 0'
		FROM @TableXmlObject tXml
		WHERE tXml.StatusField = 99 AND tXml.Hours <= 0

		UPDATE tXml
			SET tXml.StatusField = 0,
				tXml.PayrollPositionId = p.Id,
				tXml.PayrollPositionCode = p.Code,
				txml.PayrollPositionName = p.Name
		FROM @TableXmlObject tXml
		JOIN Payroll.Position p ON tXml.PayrollPositionCode = p.Code
		WHERE tXml.StatusField = 99

	END TRY
	BEGIN CATCH
		INSERT INTO @TableXmlObject(StatusField, MessageField)
		VALUES (999, ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(5)))
	END CATCH

	SELECT DISTINCT
		CostActivityStepId,
		PayrollPositionId,
		PayrollPositionCode,
		PayrollPositionName,
		Hours,
		--------------------
		StatusField,
		MessageField
	FROM @TableXmlObject
	WHERE StatusField <> 99
END