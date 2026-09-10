

-- =============================================
-- Author:		Yohana Rozo
-- Create date: <2021-06-09>
-- Description:	<Funcion que retorna el valor correspondiente a Suministro de Acido Folico por mimima fecha>
-- ==========================================
CREATE FUNCTION [Report].[FN_Medicamento_Rias_AcidoFolico_Fecha]
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
WHERE (ATC.NAME LIKE ('%FOLI%') OR ATC.NAME LIKE ('%FOLACINA%') OR ATC.NAME LIKE ('%pteroilmonoglut%') OR ATC.NAME LIKE ('%FOLATO%') OR
   ATC.NAME LIKE ('%VITAMINA%9%'))
AND PDD.Quantity-PDD.ReturnedQuantity>0
AND ING.IPCODPACI=@ipcodpaci
AND PD.ConfirmationDate>=@Fecha_Historia 
--GROUP BY IPCODPACI
) A

 )
	RETURN @variable

END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Función escalar que retorna la fecha mínima de dispensación confirmada de ácido fólico (identificado por nombres ATC como FÓLICO, FOLACINA, FOLATO, pteroilmonoglutámico o VITAMINA B9) para un paciente específico, considerando solo entregas netas positivas (cantidad menos devoluciones) ocurridas a partir de una fecha de historia clínica dada. Se usa en reportes RIAS para evidenciar el primer suministro de este micronutriente por ingreso.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_AcidoFolico_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_AcidoFolico_Fecha';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene la fecha de la primera dispensación confirmada de Ácido Fólico (o equivalentes según ATC) para un paciente a partir de una fecha de historia clínica, para reportes RIAS.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_AcidoFolico_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un código de paciente (IPCODPACI) válido en ADINGRESO asociado a un número de ingreso usado en dispensaciones farmacéuticas.; Debe proveerse una fecha de historia clínica como límite inferior para la búsqueda de la dispensación.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_AcidoFolico_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo considera dispensaciones cuya cantidad neta entregada (Quantity - ReturnedQuantity) sea mayor a 0, descartando devoluciones totales.; El medicamento se identifica por nombre ATC con coincidencias de patrones: ''FOLI'', ''FOLACINA'', ''pteroilmonoglut'', ''FOLATO'' o ''VITAMINA%9'' (Vitamina B9).; Solo se evalúan dispensaciones con ConfirmationDate igual o posterior a la fecha de historia clínica suministrada.; El cruce paciente-dispensación se hace vía ADINGRESO.NUMINGRES = PharmaceuticalDispensing.AdmissionNumber filtrando por IPCODPACI.; Retorna la mínima fecha de confirmación (primera entrega) del medicamento; si no hay dispensaciones que cumplan, retorna NULL.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_AcidoFolico_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Suministro de medicamentos; Ácido Fólico; Dispensación farmacéutica; RIAS (Rutas Integrales de Atención en Salud); Paciente; Ingreso clínico; Clasificación ATC', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_AcidoFolico_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (scalar datetime): Retorna MIN(PD.ConfirmationDate) cuando existen dispensaciones con ATC.NAME que coincida con patrones de Ácido Fólico/Folato/Vitamina B9, cantidad neta > 0, paciente = @ipcodpaci y ConfirmationDate >= @Fecha_Historia; en caso contrario retorna NULL.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_AcidoFolico_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PharmaceuticalDispensing; Inventory.PharmaceuticalDispensingDetail; DBO.ADINGRESO; Inventory.InventoryProduct; Inventory.ATC', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_AcidoFolico_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_AcidoFolico_Fecha';
GO
