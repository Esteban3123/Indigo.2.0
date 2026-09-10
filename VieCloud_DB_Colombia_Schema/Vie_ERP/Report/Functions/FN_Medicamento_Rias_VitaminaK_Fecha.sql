

-- =============================================
-- Author:		Yohana Rozo
-- Create date: <2021-06-09>
-- Description:	<Funcion que retorna el valor correspondiente a Suministro de Acido Folico por mimima fecha>
-- ==========================================
CREATE FUNCTION [Report].[FN_Medicamento_Rias_VitaminaK_Fecha]
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
WHERE (ATC.NAME LIKE ('%VITAMINA%K%') OR ATC.NAME LIKE ('%Fitomenadiona%'))
AND PDD.Quantity-PDD.ReturnedQuantity>0
AND ING.IPCODPACI=@ipcodpaci
AND PD.ConfirmationDate>=@Fecha_Historia 
--GROUP BY IPCODPACI
) A

 )
	RETURN @variable

END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Función escalar que retorna la fecha mínima de confirmación de dispensación de Vitamina K (Fitomenadiona) para un paciente específico, a partir de una fecha de historia clínica dada. Consulta los registros de dispensación farmacéutica filtrando por clasificación ATC que contenga "VITAMINA K" o "Fitomenadiona", considerando únicamente ítems con cantidad neta entregada mayor a cero.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_VitaminaK_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_VitaminaK_Fecha';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve la fecha más temprana de entrega/dispensación de Vitamina K (Fitomenadiona) a un paciente a partir de una fecha de historia clínica dada, para reportes RIAS.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_VitaminaK_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe existir en DBO.ADINGRESO con el código IPCODPACI suministrado.; Debe haber dispensaciones farmacéuticas confirmadas con fecha mayor o igual a la fecha de historia.; El producto dispensado debe estar clasificado en ATC con nombre que contenga ''VITAMINA K'' o ''Fitomenadiona''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_VitaminaK_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran dispensaciones con saldo efectivo entregado (Quantity - ReturnedQuantity > 0), excluyendo devoluciones totales.; Solo se considera la primera (mínima) fecha de confirmación de dispensación posterior o igual a la fecha de historia.; La identificación del medicamento Vitamina K se hace por coincidencia textual en el nombre del ATC, no por código.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_VitaminaK_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Dispensación farmacéutica; Vitamina K / Fitomenadiona; Clasificación ATC; Ingreso del paciente; RIAS (Rutas Integrales de Atención en Salud); Devolución de medicamentos', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_VitaminaK_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Retorna MIN(PD.ConfirmationDate) cuando ATC.NAME LIKE ''%VITAMINA%K%'' o ''%Fitomenadiona%'', cantidad neta (Quantity-ReturnedQuantity) > 0, ingreso del paciente coincide y la fecha de confirmación es >= @Fecha_Historia; en caso contrario retorna NULL.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_VitaminaK_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PharmaceuticalDispensing; Inventory.PharmaceuticalDispensingDetail; DBO.ADINGRESO; Inventory.InventoryProduct; Inventory.ATC', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_VitaminaK_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_VitaminaK_Fecha';
GO
