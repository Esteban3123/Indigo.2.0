-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 07/01/2021
-- Description:	Procedimiento que se encarga de importar los medicamentos para producción
-- =============================================
CREATE PROCEDURE [MixingStation].[SP_ImportMedicinesProduction] 
	@XmlObject as Xml,
	@CMConfigurationId as int
AS
BEGIN
	
	--Tabla para almacenar los items del listado que viene en el xml y poder guardar los registros
	DECLARE @TableXmlObject TABLE (
		Id INT IDENTITY PRIMARY KEY, 
		CountFields INT, 
		StatusField INT, 
		MessageField VARCHAR(MAX), 
		ATCCode VARCHAR(20), 
		UnitDoseTypeCode VARCHAR(20), 
		CodeCenterAttention VARCHAR(10),
		AllowsRemnant VARCHAR(10),
		---------------------------
		IsExternal BIT,
		ExternalCareCenterId INT
	)

	BEGIN TRY
	
		INSERT INTO @TableXmlObject
			(CountFields, StatusField, MessageField, ATCCode, UnitDoseTypeCode, CodeCenterAttention, AllowsRemnant)
			SELECT 
				t.x.value('CountFields[1]','int') AS CountFields,
				1 AS StatusField,
				t.x.value('MessageField[1]','varchar(100)') AS MessageField,
				t.x.value('ATCCode[1]','varchar(20)') AS ATCCode,
				t.x.value('UnitDoseTypeCode[1]','varchar(20)') AS UnitDoseTypeCode,
				t.x.value('CodeCenterAttention[1]','varchar(10)') AS CodeCenterAttention,
				t.x.value('AllowsRemnant[1]','varchar(10)') AS AllowsRemnant
			FROM @XmlObject.nodes('/Data/Row') t(x)

		--------------------------------- VALIDATIONS ---------------------------------

		UPDATE txo
			SET txo.StatusField = 0,
				txo.MessageField = CONCAT('El registro ', txo.Id, ' no tiene la estructura requerida')
		FROM @TableXmlObject txo
		WHERE txo.StatusField = 1
			AND txo.CountFields <> 4

		UPDATE txo
			SET txo.StatusField = 0,
				txo.MessageField = CONCAT('El registro ', txo.Id, ' se encuentra duplicado')
		FROM @TableXmlObject txo
		JOIN @TableXmlObject txod ON txo.ATCCode = txod.ATCCode AND txo.UnitDoseTypeCode = txod.UnitDoseTypeCode AND txo.CodeCenterAttention = txod.CodeCenterAttention AND txo.Id <> txod.Id
		WHERE txo.StatusField = 1

		UPDATE txo
			SET txo.StatusField = 0,
				txo.MessageField = CONCAT('El código del medicamento ', txo.ATCCode, ' del registro ', txo.Id, ' no existe')
		FROM @TableXmlObject txo
		LEFT JOIN Inventory.ATC atc ON txo.ATCCode = atc.Code
		WHERE txo.StatusField = 1
			AND atc.Id IS NULL

		UPDATE txo
			SET txo.StatusField = 0,
				txo.MessageField = CONCAT('El código del tipo de dosis unitaria ', txo.UnitDoseTypeCode, ' del registro ', txo.Id, ' no existe')
		FROM @TableXmlObject txo
		LEFT JOIN MixingStation.UnitDoseType udt ON txo.UnitDoseTypeCode = udt.Code
		WHERE txo.StatusField = 1
			AND udt.Id IS NULL

		UPDATE txo
			SET txo.StatusField = IIF(cc.CODCENATE IS NULL AND ecc.Id IS NULL, 0, txo.StatusField),
				txo.MessageField = IIF(cc.CODCENATE IS NULL AND ecc.Id IS NULL,CONCAT('El código del centro de atención ', txo.CodeCenterAttention, ' del registro ', txo.Id, ' no existe'), txo.MessageField),
				txo.IsExternal = IIF(ecc.Id IS NOT NULL, 1, 0),
				txo.ExternalCareCenterId = ecc.Id
		FROM @TableXmlObject txo
		LEFT JOIN dbo.ADCENATEN cc ON txo.CodeCenterAttention = cc.CODCENATE
		LEFT JOIN MixingStation.ExternalCareCenter ecc ON txo.CodeCenterAttention = ecc.Code
		WHERE txo.StatusField = 1

		UPDATE txo
			SET txo.StatusField = 0,
				txo.MessageField = CONCAT('El código del centro de atención ', txo.CodeCenterAttention, ' del registro ', txo.Id, ' no se encuentra asociado a la central de mezcla')
		FROM @TableXmlObject txo
		LEFT JOIN MixingStation.CMCenterAttention cmp ON txo.IsExternal = 0 AND txo.CodeCenterAttention = cmp.CodeCenterAttention AND cmp.IdMixingStation = @CMConfigurationId
		LEFT JOIN MixingStation.CMExternalCareCenter cme ON txo.IsExternal = 1 AND txo.ExternalCareCenterId = cme.ExternalCareCenterId AND cme.CMConfigurationId = @CMConfigurationId 
		WHERE txo.StatusField = 1
			AND cmp.Id IS NULL AND cme.Id IS NULL

		UPDATE txo
			SET txo.StatusField = 0,
				txo.MessageField = CONCAT('Ya existe un registro con el medicamento ', txo.ATCCode, ', el tipo de dosis unitaria ', txo.UnitDoseTypeCode, ' y el centro de atención ', txo.CodeCenterAttention, ' para la central de mezclas seleccionada')
		FROM @TableXmlObject txo
		JOIN Inventory.ATC atc ON txo.ATCCode = atc.Code
		JOIN MixingStation.UnitDoseType udt ON txo.UnitDoseTypeCode = udt.Code
		JOIN MixingStation.MedicinesProduction mp ON atc.Id = mp.ATCId AND udt.Id = mp.UnitDoseTypeId AND txo.CodeCenterAttention = mp.CenterAttentionId AND mp.CMConfigurationId = @CMConfigurationId
		WHERE txo.StatusField = 1

		------------------------------------------------------------------------------------------------------

		INSERT INTO MixingStation.MedicinesProduction(CMConfigurationId, ATCId, UnitDoseTypeId, CenterAttentionId, AllowsRemnant, Status)
			SELECT	@CMConfigurationId, atc.Id, udt.Id, txo.CodeCenterAttention, 
					CASE txo.AllowsRemnant
						WHEN 'SI' THEN 1
						WHEN '1' THEN 1
						ELSE 0
					END, 
					1
			FROM @TableXmlObject txo
			JOIN Inventory.ATC atc ON txo.ATCCode = atc.Code
			JOIN MixingStation.UnitDoseType udt ON txo.UnitDoseTypeCode = udt.Code
			WHERE txo.StatusField = 1

		UPDATE txo
			SET txo.MessageField = CONCAT('Se guardó correctamente el registro ', txo.Id)
		FROM @TableXmlObject txo
		WHERE txo.StatusField = 1		
	END TRY
	BEGIN CATCH

		DELETE FROM @TableXmlObject
		INSERT INTO @TableXmlObject(CountFields, StatusField, MessageField, ATCCode, UnitDoseTypeCode, CodeCenterAttention) values(0, 2, ERROR_MESSAGE(), '', '', '')

	END CATCH

	--Se retorna la tabla
	SELECT	Id,
			CountFields,
			StatusField,
			MessageField,
			ATCCode,
			UnitDoseTypeCode,
			CodeCenterAttention
	FROM @TableXmlObject
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Importa desde un archivo XML una lista de medicamentos habilitados para producción en una central de mezclas específica. Para cada registro del XML valida: que tenga la estructura correcta (4 campos), que no esté duplicado dentro del mismo lote, que el código del medicamento exista en el catálogo ATC, que el tipo de dosis unitaria sea válido, que el centro de atención exista (ya sea interno o externo a la central de mezclas) y que ese centro esté asociado a la central de mezclas indicada, y que la combinación medicamento/dosis/centro no haya sido registrada previamente. Los registros que superan todas las validaciones se insertan en la tabla de medicamentos para producción (MedicinesProduction); los que fallan quedan marcados con el mensaje de error correspondiente. Retorna una tabla con el resultado de cada registro procesado indicando si fue guardado exitosamente o el motivo del rechazo.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'PROCEDURE', @level1name = N'SP_ImportMedicinesProduction';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'PROCEDURE', @level1name = N'SP_ImportMedicinesProduction';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Procesa un lote XML de medicamentos para producción en una central de mezclas, valida cada fila contra catálogos y configuraciones, e inserta los registros válidos en MedicinesProduction devolviendo el resultado por fila.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ImportMedicinesProduction';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe tener nodos /Data/Row con los campos CountFields, MessageField, ATCCode, UnitDoseTypeCode, CodeCenterAttention y AllowsRemnant.; Cada fila del XML debe traer CountFields = 4 para considerarse estructuralmente válida.; Debe existir una configuración de central de mezclas identificada por @CMConfigurationId y los centros de atención deben estar previamente asociados a ella en CMCenterAttention o CMExternalCareCenter.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ImportMedicinesProduction';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo los registros con StatusField=1 (sin errores acumulados) avanzan a las siguientes validaciones y al INSERT final.; Un registro sólo se inserta en MedicinesProduction si supera TODAS las validaciones: estructura (CountFields=4), no duplicado en el lote, ATC existente, UnitDoseType existente, centro de atención existente (interno o externo), centro asociado a la central de mezclas y no preexistente en MedicinesProduction.; AllowsRemnant se normaliza: solo ''SI'' o ''1'' se traducen a 1; cualquier otro valor queda como 0.; Status del registro insertado en MedicinesProduction siempre es 1 (activo).; Los duplicados se detectan por la combinación (ATCCode, UnitDoseTypeCode, CodeCenterAttention) dentro del mismo XML.; La unicidad en destino se valida por (ATCId, UnitDoseTypeId, CenterAttentionId, CMConfigurationId).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ImportMedicinesProduction';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Medicamento (ATC); Tipo de dosis unitaria; Centro de atención; Centro de atención externo; Estación/Central de mezclas; Producción de medicamentos; Remanente', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ImportMedicinesProduction';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] @TableXmlObject: Cuando CountFields <> 4, se marca StatusField=0 con mensaje ''no tiene la estructura requerida''.; [UPDATE] @TableXmlObject: Cuando existen dos filas con misma (ATCCode, UnitDoseTypeCode, CodeCenterAttention), se marcan como duplicadas (StatusField=0).; [UPDATE] @TableXmlObject: Si ATCCode no existe en Inventory.ATC, se marca StatusField=0 con mensaje ''el código del medicamento ... no existe''.; [UPDATE] @TableXmlObject: Si UnitDoseTypeCode no existe en MixingStation.UnitDoseType, se marca StatusField=0 con mensaje ''el código del tipo de dosis unitaria ... no existe''.; [UPDATE] @TableXmlObject: Si CodeCenterAttention no existe ni en dbo.ADCENATEN ni en MixingStation.ExternalCareCenter, se marca StatusField=0; si está en ExternalCareCenter se setea IsExternal=1 y ExternalCareCenterId.; [UPDATE] @TableXmlObject: Si el centro (interno o externo) no está asociado a la central de mezclas @CMConfigurationId vía CMCenterAttention/CMExternalCareCenter, se marca StatusField=0 con mensaje ''no se encuentra asociado a la central de mezcla''.; [UPDATE] @TableXmlObject: Si ya existe en MedicinesProduction un registro con (ATCId, UnitDoseTypeId, CenterAttentionId, CMConfigurationId), se marca StatusField=0 con mensaje ''Ya existe un registro ...''.; [INSERT] MixingStation.MedicinesProduction: Para cada fila con StatusField=1 tras las validaciones, se inserta (CMConfigurationId, ATCId, UnitDoseTypeId, CenterAttentionId=CodeCenterAttention, AllowsRemnant normalizado, Status=1).; [UPDATE] @TableXmlObject: Tras el INSERT, las filas con StatusField=1 reciben el mensaje ''Se guardó correctamente el registro ...''.; [DELETE] @TableXmlObject: En el bloque CATCH se eliminan todos los registros de la tabla temporal antes de devolver el error.; [INSERT] @TableXmlObject: En CATCH se inserta una fila única con StatusField=2 y MessageField=ERROR_MESSAGE() para reportar la excepción.; [RETURN_RESULT] @TableXmlObject: Al final se retorna SELECT de Id, CountFields, StatusField, MessageField, ATCCode, UnitDoseTypeCode y CodeCenterAttention de la tabla temporal.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ImportMedicinesProduction';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si txo.AllowsRemnant = ''SI'' o ''1'' → Se inserta AllowsRemnant=1 en MedicinesProduction else Se inserta AllowsRemnant=0; si ecc.Id IS NOT NULL (existe en MixingStation.ExternalCareCenter) → Se marca el registro como IsExternal=1 y se asigna ExternalCareCenterId else IsExternal=0 (centro de atención interno vía dbo.ADCENATEN); si Ocurre excepción dentro del TRY → En CATCH se vacía la tabla temporal y se inserta una única fila con StatusField=2 y MessageField=ERROR_MESSAGE()', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ImportMedicinesProduction';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.ATC; MixingStation.UnitDoseType; dbo.ADCENATEN; MixingStation.ExternalCareCenter; MixingStation.CMCenterAttention; MixingStation.CMExternalCareCenter; MixingStation.MedicinesProduction', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ImportMedicinesProduction';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ImportMedicinesProduction';
-- GO
