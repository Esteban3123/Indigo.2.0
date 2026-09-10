

-- =============================================
-- Author:		Yohana Rozo
-- Create date: <2021-06-09>
-- Description:	<Funcion que retorna el valor correspondiente a Suministro de Acido Folico por mimima fecha>
-- ==========================================
CREATE FUNCTION [Report].[FN_Medicamento_Rias_VitaminaA_Fecha]
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
WHERE (ATC.NAME LIKE ('%VITAMINA%A%'))
AND PDD.Quantity-PDD.ReturnedQuantity>0
AND ING.IPCODPACI=@ipcodpaci
AND PD.ConfirmationDate>=@Fecha_Historia 
--GROUP BY IPCODPACI
) A

 )
	RETURN @variable

END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Función escalar que retorna la fecha mínima de confirmación de dispensación de Vitamina A para un paciente específico, considerando solo despachos posteriores o iguales a una fecha de historia clínica dada. Filtra medicamentos cuyo nombre ATC contenga "VITAMINA A", excluye devoluciones completas (cantidad neta > 0) y se utiliza en reportes de seguimiento RIAS para suministro de este micronutriente.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_VitaminaA_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_VitaminaA_Fecha';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene la fecha mínima de entrega (dispensación confirmada) de un medicamento clasificado como Vitamina A para un paciente, a partir de una fecha de historia dada.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_VitaminaA_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente identificado debe tener al menos un ingreso (ADINGRESO) asociado a una dispensación farmacéutica.; La fecha de confirmación de la dispensación debe ser igual o posterior a la fecha de historia recibida.; El producto debe estar clasificado en ATC con nombre que coincida con el patrón ''%VITAMINA%A%''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_VitaminaA_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo considera dispensaciones efectivamente entregadas (Quantity - ReturnedQuantity > 0), descartando las totalmente devueltas.; Filtra medicamentos cuyo nombre ATC contenga la cadena ''VITAMINA'' seguida de ''A'' (clasificación ATC tipo Vitamina A).; Solo agrega dispensaciones con fecha de confirmación posterior o igual a la fecha de historia clínica de referencia.; Devuelve únicamente la fecha más temprana (MIN) que cumpla las condiciones; NULL si no existe dispensación que cumpla.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_VitaminaA_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Dispensación farmacéutica; Vitamina A; Clasificación ATC; Ingreso del paciente; RIAS (Rutas Integrales de Atención en Salud); Cantidad entregada vs devuelta; Fecha de historia clínica', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_VitaminaA_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Retorna la fecha mínima de ConfirmationDate de las dispensaciones farmacéuticas de Vitamina A para el paciente, cuya cantidad neta entregada (Quantity - ReturnedQuantity) sea mayor a 0 y cuya fecha de confirmación sea >= @Fecha_Historia.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_VitaminaA_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PharmaceuticalDispensing; Inventory.PharmaceuticalDispensingDetail; DBO.ADINGRESO; Inventory.InventoryProduct; Inventory.ATC', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_VitaminaA_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_VitaminaA_Fecha';
GO
