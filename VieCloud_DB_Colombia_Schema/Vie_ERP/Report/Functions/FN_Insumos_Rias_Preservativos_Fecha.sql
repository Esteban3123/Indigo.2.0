

-- =============================================
-- Author:		Yohana Rozo
-- Create date: <2021-06-09>
-- Description:	<Funcion que retorna el valor correspondiente a Suministro de Acido Folico por mimima fecha>
-- ==========================================
CREATE FUNCTION [Report].[FN_Insumos_Rias_Preservativos_Fecha]
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
WHERE (PRO.NAME LIKE ('%preserv%') OR PRO.NAME LIKE ('%condon%'))
AND PDD.Quantity-PDD.ReturnedQuantity>0
AND ING.IPCODPACI=@ipcodpaci
AND PD.ConfirmationDate>=@Fecha_Historia 
--GROUP BY IPCODPACI
) A

 )
	RETURN @variable

END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Función escalar que retorna la fecha mínima de confirmación de una dispensación farmacéutica de preservativos o condones para un paciente específico, considerando solo registros posteriores o iguales a una fecha de historia clínica dada. Filtra productos cuyo nombre contenga "preserv" o "condon", con cantidad neta entregada mayor a cero. Forma parte del reporte de insumos de RIAS (Rutas Integrales de Atención en Salud).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Insumos_Rias_Preservativos_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Insumos_Rias_Preservativos_Fecha';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve la fecha más temprana de dispensación confirmada de preservativos/condones entregados a un paciente desde una fecha de referencia.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Insumos_Rias_Preservativos_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente identificado debe existir en DBO.ADINGRESO con ingresos asociados a dispensaciones farmacéuticas.; Debe existir al menos un producto en Inventory.InventoryProduct cuyo NAME contenga ''preserv'' o ''condon'' para poder retornar fecha; en caso contrario retorna NULL.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Insumos_Rias_Preservativos_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo considera dispensaciones con entrega efectiva neta positiva: PDD.Quantity - PDD.ReturnedQuantity > 0 (excluye devoluciones totales).; Identifica preservativos/condones únicamente por coincidencia textual en el nombre del producto (no por código ni clasificación).; Filtra dispensaciones cuya ConfirmationDate sea posterior o igual a la fecha de historia, descartando entregas previas a esa fecha.; Vincula la dispensación al paciente vía ADINGRESO.NUMINGRES = PharmaceuticalDispensing.AdmissionNumber.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Insumos_Rias_Preservativos_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Dispensación farmacéutica; Preservativos/condones; Insumos RIAS; Ingreso del paciente; Devolución de medicamentos; Fecha de confirmación de entrega', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Insumos_Rias_Preservativos_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Retorna MIN(PD.ConfirmationDate) de dispensaciones donde el producto se identifica por nombre LIKE ''%preserv%'' o ''%condon%'', la cantidad neta entregada (Quantity-ReturnedQuantity) es mayor a cero, el ingreso pertenece al paciente indicado y la fecha de confirmación es ≥ a la fecha de historia recibida.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Insumos_Rias_Preservativos_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PharmaceuticalDispensing; Inventory.PharmaceuticalDispensingDetail; DBO.ADINGRESO; Inventory.InventoryProduct', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Insumos_Rias_Preservativos_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Insumos_Rias_Preservativos_Fecha';
GO
