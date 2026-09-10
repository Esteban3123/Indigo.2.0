

-- =============================================
-- Author:		Yohana Rozo
-- Create date: <2021-06-09>
-- Description:	<Funcion que retorna el valor correspondiente a Suministro de Sulfato Ferroso por mimima fecha>
-- ==========================================
CREATE FUNCTION [Report].[FN_Medicamento_Rias_Sulfato_Ferroso_Fecha]
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
WHERE  (ATC.Name LIKE '%SULFATO%FERROSO%TAB%' OR ATC.Name LIKE '%HIERRO%')
AND PDD.Quantity-PDD.ReturnedQuantity>0
AND ING.IPCODPACI=@ipcodpaci
AND PD.ConfirmationDate>=@Fecha_Historia 
--GROUP BY IPCODPACI
) A

 )
	RETURN @variable

END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Función escalar que, dado un paciente y una fecha de referencia, retorna la fecha mínima de confirmación (primera entrega) de un despacho farmacéutico que contenga Sulfato Ferroso o Hierro en tabletas. Consulta la dispensación farmacéutica junto con su detalle, el ingreso del paciente y la clasificación ATC del producto, filtrando por cantidad neta entregada mayor a cero y dispensaciones ocurridas a partir de la fecha indicada.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_Sulfato_Ferroso_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_Sulfato_Ferroso_Fecha';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene la fecha mínima de entrega confirmada de medicamentos clasificados como sulfato ferroso o hierro para un paciente desde una fecha de referencia, usado en reportes RIAS.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_Sulfato_Ferroso_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe existir en DBO.ADINGRESO vinculado por NUMINGRES con dispensaciones farmacéuticas; Debe existir al menos una dispensación con ConfirmationDate >= @Fecha_Historia para que la función retorne valor; de lo contrario retorna NULL', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_Sulfato_Ferroso_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran dispensaciones con cantidad neta entregada positiva: PDD.Quantity - PDD.ReturnedQuantity > 0 (excluye devoluciones totales); El medicamento se identifica por nombre ATC con patrones ''%SULFATO%FERROSO%TAB%'' o ''%HIERRO%'' (clasificación textual, no por código ATC); Solo se consideran dispensaciones cuya ConfirmationDate sea posterior o igual a la fecha de historia clínica de referencia; Retorna únicamente la PRIMERA (mínima) fecha de entrega que cumple los criterios', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_Sulfato_Ferroso_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Dispensación farmacéutica; Sulfato ferroso; Hierro; Clasificación ATC; Ingreso del paciente; RIAS (Rutas Integrales de Atención en Salud); Suministro de medicamento; Devolución de medicamento', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_Sulfato_Ferroso_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Inventory.PharmaceuticalDispensing: Retorna MIN(PD.ConfirmationDate) filtrando por ATC.Name LIKE ''%SULFATO%FERROSO%TAB%'' OR ''%HIERRO%'', cantidad efectivamente entregada (Quantity-ReturnedQuantity>0), paciente igual a @ipcodpaci y fecha de confirmación >= @Fecha_Historia', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_Sulfato_Ferroso_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PharmaceuticalDispensing; Inventory.PharmaceuticalDispensingDetail; DBO.ADINGRESO; Inventory.InventoryProduct; Inventory.ATC', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_Sulfato_Ferroso_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_Sulfato_Ferroso_Fecha';
GO
