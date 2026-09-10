-- ===============================================================================================================================
-- Author:		Andrea Coqueco
-- Create date: 04/08/2025
-- Description:	Procedimiento que se encarga de adicionar los detalles no ordenados del paquete
-- ===============================================================================================================================
CREATE PROCEDURE [MixingStation].[SP_AddManualPharmaDose]
	@Xml XML,
	------------------------------------------------------
	@CodeResult INT OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT
AS
BEGIN
	--Tabla de para obtener el xml
	DECLARE @XmlTable TABLE(
		ConfirmationUnitDoseId INT
	)
	
	BEGIN TRY
		INSERT INTO @XmlTable
			SELECT
				t.x.value('Id[1]','int') as Id
			FROM @Xml.nodes('/ConfirmationUnitDose') t(x)
		;

		---------------------------------

		WITH pharmaDoses AS (
			SELECT
				cud.Id ConfirmationUnitDoseId,
				pd.IDHCFARMEPC,
				pd.CodeSusceptibleMixingStation,
				pd.ProductCode,
				pd.Dose,
				pd.MeasurementUnitCode,
				pd.UnitDoseTypeId,
				pd.GroupingCodeDose,
				pd.DeliveryStatus,
				pd.QuantityReceivable,
				pd.AppliedDose,
				pd.AppliedDateDose,
				pd.MixingStationId,
				pd.IsDispensed,
				pd.IsTransformedProductStandardDispensation
			FROM MixingStation.ConfirmationUnitDose cud
			JOIN MedicalHistory.PharmaDose pd
				ON cud.GroupingCodeDose = pd.GroupingCodeDose
		)
		, packageDetails AS (
			SELECT
				cud.Id ConfirmationUnitDoseId,
				atc.Code ProductCode,
				COALESCE(pd.Quantity, pd.Volume) Dose,
				imu.Code MeasurementUnitCode
			FROM MixingStation.ConfirmationUnitDose cud
			JOIN MixingStation.Package p ON cud.PackageId = p.Id
			JOIN MixingStation.PackageDetail pd ON p.Id = pd.PackageId
			JOIN Inventory.ATC atc ON pd.AtcId = atc.Id
			JOIN Inventory.InventoryMeasurementUnit imu ON COALESCE(pd.MeasurementUnitId, pd.VolumeMeasureUnit) = imu.Id
			WHERE cud.PersonalizedMasterPreparation = 0
		UNION
			SELECT
				cud.Id ConfirmationUnitDoseId,
				atc.Code ProductCode,
				COALESCE(pd.Quantity, pd.Volume) Dose,
				imu.Code MeasurementUnitCode
			FROM MixingStation.ConfirmationUnitDose cud
			JOIN MixingStation.PackagePersonalized p ON cud.PersonalizedMasterPreparationPackageId = p.Id
			JOIN MixingStation.PackagePersonalizedDetail pd ON p.Id = pd.PackagePersonalizedId
			JOIN Inventory.ATC atc ON pd.AtcId = atc.Id
			JOIN Inventory.InventoryMeasurementUnit imu ON COALESCE(pd.MeasurementUnitId, pd.VolumeMeasureUnit) = imu.Id
			WHERE cud.PersonalizedMasterPreparation = 1
		)
		, pharmaDosesComplete AS (
			SELECT DISTINCT
				p.ConfirmationUnitDoseId,
				p.IDHCFARMEPC,
				p.CodeSusceptibleMixingStation,
				pd.ProductCode,
				pd.Dose,
				pd.MeasurementUnitCode,
				p.UnitDoseTypeId,
				p.GroupingCodeDose,
				p.DeliveryStatus,
				p.QuantityReceivable,
				p.AppliedDose,
				p.AppliedDateDose,
				p.MixingStationId,
				p.IsDispensed,
				p.IsTransformedProductStandardDispensation,
				CAST(1 AS BIT) IsManualAddition
			FROM pharmaDoses p
			JOIN packageDetails pd ON p.ConfirmationUnitDoseId = pd.ConfirmationUnitDoseId
		)

		-----------------------------------------

		INSERT INTO MedicalHistory.PharmaDose (
			[IDHCFARMEPC]
           ,[CodeSusceptibleMixingStation]
           ,[ProductCode]
           ,[MeasurementUnitCode]
           ,[UnitDoseTypeId]
           ,[Dose]
           ,[GroupingCodeDose]
           ,[DeliveryStatus]
           ,[QuantityReceivable]
           ,[AppliedDose]
           ,[AppliedDateDose]
           ,[MixingStationId]
           ,[IsDispensed]
           ,[IsTransformedProductStandardDispensation]
           ,[IsManualAddition]
		)
		SELECT 
			pd.[IDHCFARMEPC]
           ,pd.[CodeSusceptibleMixingStation]
           ,pd.[ProductCode]
           ,pd.[MeasurementUnitCode]
           ,pd.[UnitDoseTypeId]
           ,pd.[Dose]
           ,pd.[GroupingCodeDose]
           ,pd.[DeliveryStatus]
           ,pd.[QuantityReceivable]
           ,pd.[AppliedDose]
           ,pd.[AppliedDateDose]
           ,pd.[MixingStationId]
           ,pd.[IsDispensed]
           ,pd.[IsTransformedProductStandardDispensation]
           ,pd.[IsManualAddition]
		FROM @XmlTable t
		JOIN pharmaDosesComplete pd ON t.ConfirmationUnitDoseId = pd.ConfirmationUnitDoseId
		LEFT JOIN pharmaDoses p ON pd.ConfirmationUnitDoseId = p.ConfirmationUnitDoseId
			AND pd.ProductCode = p.ProductCode
			--AND pd.Dose = p.Dose
			--AND pd.MeasurementUnitCode = p.MeasurementUnitCode
		WHERE p.ConfirmationUnitDoseId IS NULL

		-----------------------------------------

		SELECT @CodeResult = 0, 
			   @MessageResult = 'Detalles faltantes agregados correctamente'
	END TRY
	BEGIN CATCH
		SELECT @CodeResult = 999, 
			   @MessageResult = CONCAT(ERROR_MESSAGE(), ' - Linea: ', ERROR_LINE())
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de la estación de mezclas que agrega manualmente los productos (componentes) faltantes a las dosis farmacéuticas de un paquete ya confirmado para dispensación en dosis unitaria. Recibe por XML los identificadores de las confirmaciones de dosis unitaria que requieren adición manual, luego cruza la información de las confirmaciones (MixingStation.ConfirmationUnitDose) con las dosis ya registradas en historia clínica (MedicalHistory.PharmaDose) y con los detalles de los paquetes de preparación (estándar o personalizada), obteniendo el código del producto ATC, la dosis y la unidad de medida del inventario. Finalmente inserta en MedicalHistory.PharmaDose únicamente los productos del paquete que aún no tienen dosis registrada para esa confirmación, marcándolos con la bandera de adición manual (IsManualAddition), evitando duplicar ítems ya existentes. Se utiliza cuando la preparación en la estación de mezclas requiere incluir insumos o medicamentos no contemplados originalmente en la orden médica.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'PROCEDURE', @level1name = N'SP_AddManualPharmaDose';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'PROCEDURE', @level1name = N'SP_AddManualPharmaDose';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Inserta en la historia clínica las dosis farmacéuticas correspondientes a los componentes del paquete (estándar o personalizado) que aún no fueron registrados previamente, marcándolas como adición manual.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_AddManualPharmaDose';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe contener nodos /ConfirmationUnitDose con un Id entero válido; Cada ConfirmationUnitDose referenciado debe existir en MixingStation.ConfirmationUnitDose y tener un GroupingCodeDose con dosis previas en MedicalHistory.PharmaDose; El ConfirmationUnitDose debe estar asociado a un PackageId (si PersonalizedMasterPreparation=0) o a un PersonalizedMasterPreparationPackageId (si =1) válido con sus detalles; Los AtcId y MeasurementUnitId/VolumeMeasureUnit referenciados en los detalles deben existir en Inventory.ATC e Inventory.InventoryMeasurementUnit', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_AddManualPharmaDose';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las dosis insertadas se marcan siempre con IsManualAddition = 1 (CAST(1 AS BIT)), identificándolas como adiciones manuales no ordenadas; Los componentes del paquete a insertar se cruzan por GroupingCodeDose entre ConfirmationUnitDose y PharmaDose para heredar los atributos clínicos (IDHCFARMEPC, CodeSusceptibleMixingStation, UnitDoseTypeId, DeliveryStatus, etc.); ProductCode, Dose y MeasurementUnitCode insertados provienen del detalle del paquete (estándar o personalizado), no de PharmaDose original; Dose se calcula como COALESCE(Quantity, Volume) y la unidad como COALESCE(MeasurementUnitId, VolumeMeasureUnit), priorizando cantidad sobre volumen; Solo se procesan los ConfirmationUnitDoseId provistos en el XML de entrada; Cualquier excepción retorna CodeResult=999 con el mensaje y línea del error; éxito retorna CodeResult=0', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_AddManualPharmaDose';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'dosis farmacéutica; estación de mezclas; confirmación de dosis unitaria; paquete de preparación; preparación magistral personalizada; clasificación ATC; unidad de medida de inventario; dispensación; adición manual de dosis', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_AddManualPharmaDose';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] MedicalHistory.PharmaDose: Por cada ConfirmationUnitDoseId del XML, inserta una fila por cada ProductCode del paquete (estándar si PersonalizedMasterPreparation=0 o personalizado si =1) que no exista aún en PharmaDose para ese GroupingCodeDose, con IsManualAddition=1; [RETURN_RESULT] @CodeResult/@MessageResult: Retorna 0 + ''Detalles faltantes agregados correctamente'' al éxito; en CATCH retorna 999 con ERROR_MESSAGE() y ERROR_LINE()', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_AddManualPharmaDose';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si cud.PersonalizedMasterPreparation = 0 → Los detalles del paquete se obtienen desde MixingStation.Package y MixingStation.PackageDetail (paquete estándar) else Si PersonalizedMasterPreparation = 1, los detalles se obtienen desde MixingStation.PackagePersonalized y MixingStation.PackagePersonalizedDetail (paquete personalizado/magistral); si LEFT JOIN pharmaDoses p ON pd.ConfirmationUnitDoseId = p.ConfirmationUnitDoseId AND pd.ProductCode = p.ProductCode WHERE p.ConfirmationUnitDoseId IS NULL → Solo se insertan en PharmaDose los productos del paquete que aún no existen como PharmaDose para esa confirmación de dosis (mismo GroupingCodeDose y mismo ProductCode) else Si ya existe un PharmaDose con ese ProductCode para la confirmación, se omite la inserción', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_AddManualPharmaDose';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.ConfirmationUnitDose; MedicalHistory.PharmaDose; MixingStation.Package; MixingStation.PackageDetail; Inventory.ATC; Inventory.InventoryMeasurementUnit; MixingStation.PackagePersonalized; MixingStation.PackagePersonalizedDetail', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_AddManualPharmaDose';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_AddManualPharmaDose';
-- GO
