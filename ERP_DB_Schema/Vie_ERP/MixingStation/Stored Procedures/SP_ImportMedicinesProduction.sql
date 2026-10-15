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