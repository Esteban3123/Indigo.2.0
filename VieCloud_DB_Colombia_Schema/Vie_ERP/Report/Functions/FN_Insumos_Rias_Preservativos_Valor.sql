

-- =============================================
-- Author:		Yohana Rozo
-- Create date: <2021-06-09>
-- Description:	<Funcion que retorna el valor correspondiente a Suministro de Acido Folico por mimima fecha>
-- ==========================================
create FUNCTION [Report].[FN_Insumos_Rias_Preservativos_Valor]
(
	-- Add the parameters for the function here
	@Fecha_Historia datetime,
	@ipcodpaci varchar(25)
)
 returns datetime
 as
 begin
   declare @variable datetime=
			 (SELECT A.CANTIDAD  from 
			 (SELECT  MIN(PD.ConfirmationDate) 'FECHA ENTREGA',SUM(PDD.Quantity) 'CANTIDAD'  --, ING.IPCODPACI
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'La función retorna la cantidad total (sumatoria) de preservativos o condones dispensados a un paciente específico a partir de una fecha determinada, consultando registros de dispensación farmacéutica. Filtra productos cuyo nombre contenga "preserv" o "condon", excluye ítems con cantidad neta cero (descontando devoluciones), y agrupa por el mínimo de fecha de confirmación. A pesar de que el tipo de retorno declarado es `datetime`, la columna recuperada es `CANTIDAD` (numérica), lo que sugiere una inconsistencia en la definición.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Insumos_Rias_Preservativos_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Insumos_Rias_Preservativos_Valor';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Retorna la cantidad total dispensada de preservativos/condones a un paciente desde una fecha de historia clínica dada, para reportes de insumos RIAS.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Insumos_Rias_Preservativos_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente identificado debe existir en DBO.ADINGRESO con el código de paciente recibido.; Deben existir dispensaciones farmacéuticas confirmadas en o después de la fecha de historia indicada.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Insumos_Rias_Preservativos_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se contabilizan ítems con cantidad neta entregada positiva (Quantity - ReturnedQuantity > 0), excluyendo dispensaciones totalmente devueltas.; Solo se consideran dispensaciones cuya ConfirmationDate sea igual o posterior a la fecha de historia clínica indicada.; La identificación de preservativos/condones se hace exclusivamente por coincidencia textual en el nombre del producto (no por código ni clasificación).; A pesar de declararse como returns datetime, la función retorna realmente la cantidad sumada (valor numérico) tomada del alias ''CANTIDAD''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Insumos_Rias_Preservativos_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'dispensación farmacéutica; preservativos/condones; paciente (ingreso); insumos RIAS; fecha de historia clínica; cantidad devuelta', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Insumos_Rias_Preservativos_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Inventory.PharmaceuticalDispensing: Devuelve SUM(PDD.Quantity) de productos cuyo nombre contiene ''preserv'' o ''condon'', filtrando dispensaciones del paciente (ING.IPCODPACI=@ipcodpaci) con ConfirmationDate >= @Fecha_Historia y donde Quantity-ReturnedQuantity > 0.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Insumos_Rias_Preservativos_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PRO.NAME LIKE ''%preserv%'' OR PRO.NAME LIKE ''%condon%'' → Incluye el ítem dispensado en la suma de cantidades retornada.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Insumos_Rias_Preservativos_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PharmaceuticalDispensing; Inventory.PharmaceuticalDispensingDetail; DBO.ADINGRESO; Inventory.InventoryProduct', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Insumos_Rias_Preservativos_Valor';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Insumos_Rias_Preservativos_Valor';
GO
