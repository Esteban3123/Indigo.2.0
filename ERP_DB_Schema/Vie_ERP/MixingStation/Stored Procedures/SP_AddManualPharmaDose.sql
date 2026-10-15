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