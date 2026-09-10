-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-04-03
-- Description:	Procedimiento que se encarga de el Copy & Paste de los centros de producción desde el formulario Actividades de Costo
-- =============================================
CREATE PROCEDURE [Cost].[SP_CostActivityCopyAndPasteProductionCenter] 
	@XmlObject as Xml
AS
BEGIN
	SET NOCOUNT ON

	/************************************* VARIABLES ************************************/
	
	--Tabla para almacenar los items del listado que viene en el xml y los resultados a devolver
	DECLARE @TableXmlObject TABLE
	(
		Id INT IDENTITY PRIMARY KEY, 
		CostProductionCenterId INT,
		CostProductionCenterCode VARCHAR(500),
		CostProductionCenterName VARCHAR(500),
		--------------------------------
		StatusField INT DEFAULT(99), 
		MessageField VARCHAR(MAX)
	)

	BEGIN TRY

		INSERT INTO @TableXmlObject
			(CostProductionCenterCode)
			SELECT 
				t.x.value('CostProductionCenterCode[1]','varchar(500)') as CostProductionCenterCode
			FROM @XmlObject.nodes('/Data/Row') t(x)

		UPDATE tXml
			SET tXml.StatusField = 999,
				tXml.MessageField = 'El Centro de Producción con Código ' + tXml.CostProductionCenterCode + ' no existe'
		FROM @TableXmlObject tXml
		LEFT JOIN Cost.CostProductionCenter cpc ON tXml.CostProductionCenterCode = cpc.Code
		WHERE cpc.Id IS NULL

		UPDATE tXml
			SET tXml.StatusField = 999,
				tXml.MessageField = 'El Centro de Producción con Código ' + tXml.CostProductionCenterCode + ' es de tipo ' + 
					CASE cpc.CenterType
						WHEN 2 THEN 'Administrativo'
						WHEN 3 THEN 'Logístico'
						ELSE 'N/A'
					END
		FROM @TableXmlObject tXml
		JOIN Cost.CostProductionCenter cpc ON tXml.CostProductionCenterCode = cpc.Code
		WHERE tXml.StatusField = 99 AND cpc.CenterType <> 1

		UPDATE tXml
			SET tXml.StatusField = 0,
				tXml.CostProductionCenterId = cpc.Id,
				tXml.CostProductionCenterCode = cpc.Code,
				txml.CostProductionCenterName = cpc.Name
		FROM @TableXmlObject tXml
		JOIN Cost.CostProductionCenter cpc ON tXml.CostProductionCenterCode = cpc.Code
		WHERE tXml.StatusField = 99

	END TRY
	BEGIN CATCH
		INSERT INTO @TableXmlObject(StatusField, MessageField)
		VALUES (999, ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(5)))
	END CATCH

	SELECT DISTINCT
		CostProductionCenterId,
		CostProductionCenterCode,
		CostProductionCenterName,
		--------------------
		StatusField,
		MessageField
	FROM @TableXmlObject
	WHERE StatusField <> 99
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que valida y resuelve una operación de copiar y pegar centros de producción de costos desde el formulario de Actividades de Costo. Recibe un XML con códigos de centros de producción, verifica que cada código exista en la tabla maestra de centros de producción y que sea de tipo Asistencial (tipo 1); si el centro no existe o es de tipo Administrativo o Logístico, devuelve un mensaje de error por fila. Retorna el resultado de cada centro procesado con su identificador, código, nombre y el estado de la validación, permitiendo al formulario informar al usuario qué centros se copiaron correctamente y cuáles presentaron inconsistencias.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CostActivityCopyAndPasteProductionCenter';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CostActivityCopyAndPasteProductionCenter';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida una lista de centros de producción recibida por XML para una operación de copiar/pegar en el formulario de Actividades de Costo, devolviendo por fila si el centro es válido o el motivo del rechazo.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostActivityCopyAndPasteProductionCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe seguir la estructura /Data/Row con nodo CostProductionCenterCode; Los códigos enviados deben corresponder a registros existentes en Cost.CostProductionCenter para ser válidos; Los centros deben ser de tipo asistencial (CenterType = 1) para considerarse válidos en la operación', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostActivityCopyAndPasteProductionCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se aceptan como válidos los centros de producción con CenterType = 1 (asistenciales); Cada fila procesada termina con StatusField = 0 (válido) o 999 (error); las que quedan en 99 se omiten del resultado; Los códigos inexistentes se reportan como error sin abortar el proceso completo; Errores no controlados se capturan y devuelven como una fila con StatusField=999 incluyendo número de línea', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostActivityCopyAndPasteProductionCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Centro de producción; Centro de costo; Tipo de centro (Asistencial/Administrativo/Logístico); Actividades de Costo; Copy & Paste de centros', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostActivityCopyAndPasteProductionCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultado tabular: Devuelve un conjunto distinto con Id, Código, Nombre, StatusField y MessageField por cada centro cuyo StatusField <> 99 (válidos con 0 o errados con 999)', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostActivityCopyAndPasteProductionCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si El código del centro de producción no existe en Cost.CostProductionCenter (LEFT JOIN con cpc.Id IS NULL) → Marca StatusField=999 con mensaje ''El Centro de Producción con Código X no existe'' else Continúa con la siguiente validación; si El centro existe pero CenterType <> 1 (no es asistencial) → Marca StatusField=999 indicando que es de tipo Administrativo (2), Logístico (3) o N/A else Marca StatusField=0 y completa Id, Code y Name del centro', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostActivityCopyAndPasteProductionCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostProductionCenter', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostActivityCopyAndPasteProductionCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CostActivityCopyAndPasteProductionCenter';
-- GO
