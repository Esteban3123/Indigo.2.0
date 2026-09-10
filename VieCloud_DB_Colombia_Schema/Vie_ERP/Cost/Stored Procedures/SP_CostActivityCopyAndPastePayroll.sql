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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que realiza la operación de copiar y pegar cargos de nómina en los pasos de actividades de costo. Recibe un XML con filas que contienen el identificador del paso de actividad, el código del cargo y las horas asignadas; valida que cada cargo exista en el catálogo de posiciones de nómina (Payroll.Position) y que las horas sean mayores a cero, marcando con error los registros que no cumplan estas condiciones. Los registros válidos se enriquecen con el identificador, código y nombre del cargo de nómina antes de ser devueltos al formulario de Actividades de Costo. Es utilizado en la configuración de costos para asignar masivamente cargos laborales con sus horas a los pasos de una actividad de costo.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CostActivityCopyAndPastePayroll';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CostActivityCopyAndPastePayroll';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida masivamente un listado XML de cargos de nómina con horas para pasos de actividad de costo, marcando errores de cargo inexistente u horas no positivas, y enriqueciendo los válidos con datos del cargo.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostActivityCopyAndPastePayroll';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe recibir un XML con estructura /Data/Row conteniendo CostActivityStepId, PayrollPositionCode y Hours; El catálogo Payroll.Position debe estar poblado con los códigos de cargo a referenciar', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostActivityCopyAndPastePayroll';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelven registros cuyo StatusField sea distinto de 99 (validados, exitosos o erróneos); Un registro válido siempre queda con StatusField=0 e incluye Id, Code y Name del cargo; Los registros con error siempre quedan con StatusField=999 y un mensaje descriptivo; No se realizan escrituras en tablas físicas; toda manipulación ocurre sobre una tabla en memoria; Los errores capturados en el CATCH se devuelven como un registro adicional con StatusField=999 incluyendo número de línea', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostActivityCopyAndPastePayroll';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cargo / Posición de nómina; Horas laborales; Actividad de costo / Paso de actividad; Copy & Paste masivo de cargos', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostActivityCopyAndPastePayroll';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ResultSet: Devuelve el listado de cargos procesados (válidos con StatusField=0 y erróneos con StatusField=999), excluyendo los que permanezcan en estado inicial 99; [RAISERROR] ResultSet: Si ocurre una excepción, devuelve un registro con StatusField=999 y MessageField con ERROR_MESSAGE() y la línea del error', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostActivityCopyAndPastePayroll';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si El código de cargo no existe en Payroll.Position (LEFT JOIN con p.Id IS NULL) → Marca el registro con StatusField=999 y mensaje ''El Cargo con Código X no existe'' else Continúa la validación de horas; si StatusField sigue en 99 y Hours <= 0 → Marca el registro con StatusField=999 y mensaje indicando que las horas no pueden ser iguales o menores a 0; si StatusField sigue en 99 tras las validaciones (cargo existe y horas > 0) → Marca StatusField=0 y enriquece el registro con Id, Code y Name del cargo de nómina', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostActivityCopyAndPastePayroll';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.Position', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostActivityCopyAndPastePayroll';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostActivityCopyAndPastePayroll';
-- GO
