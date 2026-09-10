

-- =============================================
-- Author:		Yohana Rozo
-- Create date: <2021-06-09>
-- Description:	<Funcion que retorna el valor correspondiente a Suministro de Carbonato de Calcio por mimima fecha>
-- ==========================================
CREATE FUNCTION [Report].[FN_Medicamento_Rias_CarbonatoCalcio_Fecha]
(
	-- Add the parameters for the function here
	@Fecha_Historia datetime,
	@ipcodpaci varchar(25)
)
 returns datetime
 as
 begin
   declare @variable datetime=
			 (SELECT A.[FECHA ENTREGA] from 
			 (SELECT  MIN(PD.ConfirmationDate) 'FECHA ENTREGA'--, ING.IPCODPACI
FROM Inventory.PharmaceuticalDispensing AS PD WITH(NOLOCK)
INNER JOIN Inventory.PharmaceuticalDispensingDetail AS PDD WITH(NOLOCK) ON PD.Id =PDD.PharmaceuticalDispensingId 
INNER JOIN DBO.ADINGRESO ING WITH(NOLOCK) ON PD.AdmissionNumber=ING.NUMINGRES
INNER JOIN Inventory.InventoryProduct AS PRO WITH(NOLOCK) ON PDD.ProductId =PRO.Id 
INNER JOIN Inventory.ATC ATC WITH(NOLOCK) ON ATC.ID = PRO.ATCId 
WHERE  (ATC.Name LIKE '%CALCIO%%CARBO%' OR  ATC.Name LIKE '%CARBONAT%CALCIO%' OR  ATC.Name LIKE '%CALCIO%')
AND PDD.Quantity-PDD.ReturnedQuantity>0
AND ING.IPCODPACI=@ipcodpaci
AND PD.ConfirmationDate>=@Fecha_Historia 
--GROUP BY IPCODPACI
) A

 )
	RETURN @variable

END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Función escalar que, dado un código de paciente y una fecha de historia clínica, retorna la fecha mínima de confirmación (primera entrega) en que se dispensó Carbonato de Calcio —identificado por coincidencia de nombre en la clasificación ATC— con cantidad neta positiva. Considera únicamente dispensaciones confirmadas a partir de la fecha indicada, vinculando el ingreso hospitalario del paciente con el detalle de dispensación farmacéutica.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_CarbonatoCalcio_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_CarbonatoCalcio_Fecha';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene la primera fecha de confirmación de entrega de Carbonato de Calcio (u otros medicamentos con calcio) dispensada a un paciente a partir de una fecha de historia clínica dada, para reportes RIAS.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_CarbonatoCalcio_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe existir en DBO.ADINGRESO con el código suministrado (IPCODPACI).; Debe existir al menos una dispensación confirmada con fecha igual o posterior a la fecha de historia para que el resultado no sea NULL.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_CarbonatoCalcio_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo considera dispensaciones cuya cantidad neta entregada (Quantity - ReturnedQuantity) sea mayor a 0, descartando entregas totalmente devueltas.; Solo considera medicamentos cuyo nombre ATC coincida con patrones de calcio/carbonato de calcio (''%CALCIO%%CARBO%'', ''%CARBONAT%CALCIO%'' o ''%CALCIO%'').; La fecha de entrega considerada es la mínima ConfirmationDate posterior o igual a la fecha de historia suministrada.; El cruce paciente-dispensación se realiza vía ADINGRESO.NUMINGRES = PharmaceuticalDispensing.AdmissionNumber filtrando por IPCODPACI.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_CarbonatoCalcio_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Dispensación farmacéutica; Suministro de medicamento; Carbonato de Calcio; Clasificación ATC; Ingreso/Admisión del paciente; RIAS (Rutas Integrales de Atención en Salud)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_CarbonatoCalcio_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Inventory.PharmaceuticalDispensing: Retorna MIN(PD.ConfirmationDate) cuando existe dispensación de medicamento con ATC tipo calcio, cantidad neta > 0, para el paciente y con ConfirmationDate >= @Fecha_Historia; en caso contrario retorna NULL.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_CarbonatoCalcio_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PharmaceuticalDispensing; Inventory.PharmaceuticalDispensingDetail; DBO.ADINGRESO; Inventory.InventoryProduct; Inventory.ATC', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_CarbonatoCalcio_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_CarbonatoCalcio_Fecha';
GO
