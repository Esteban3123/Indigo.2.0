

-- =============================================
-- Author:		Yohana Rozo
-- Create date: <2021-06-09>
-- Description:	<Funcion que retorna el valor correspondiente a Suministro de Acido Folico por mimima fecha>
-- ==========================================
CREATE FUNCTION [Report].[FN_Medicamento_Rias_LEVONORGESTREL_Fecha]
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
WHERE (ATC.NAME LIKE ('%LEVONORGESTREL 0.75%'))
AND PDD.Quantity-PDD.ReturnedQuantity>0
AND ING.IPCODPACI=@ipcodpaci
AND PD.ConfirmationDate>=@Fecha_Historia 
--GROUP BY IPCODPACI
) A

 )
	RETURN @variable

END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Función escalar que retorna la fecha mínima de confirmación de dispensación del medicamento LEVONORGESTREL 0.75 mg para un paciente específico, considerando únicamente registros posteriores o iguales a una fecha de historia clínica dada y con cantidad neta entregada mayor a cero. Se usa en reportes de RIAS (Rutas Integrales de Atención en Salud) para identificar el primer suministro registrado de este anticonceptivo de emergencia en el período evaluado.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_LEVONORGESTREL_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_LEVONORGESTREL_Fecha';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Retorna la fecha mínima de confirmación de dispensación de Levonorgestrel 0.75 para un paciente, posterior o igual a una fecha de historia clínica dada, para reportes RIAS.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_LEVONORGESTREL_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un código de paciente (IPCODPACI) válido en ADINGRESO con ingresos asociados.; La fecha de historia debe ser una referencia válida a partir de la cual buscar la dispensación.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_LEVONORGESTREL_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo considera dispensaciones cuya cantidad neta entregada (Quantity - ReturnedQuantity) sea mayor a cero, excluyendo entregas totalmente devueltas.; Filtra medicamentos cuyo nombre ATC contiene ''LEVONORGESTREL 0.75'', acotando a anticoncepción de emergencia.; Solo se consideran dispensaciones con ConfirmationDate igual o posterior a la fecha de historia clínica suministrada.; El cruce paciente-dispensación se hace por número de ingreso (ADINGRESO.NUMINGRES = PharmaceuticalDispensing.AdmissionNumber) e IPCODPACI del paciente.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_LEVONORGESTREL_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Dispensación farmacéutica; Levonorgestrel 0.75 (anticoncepción de emergencia); RIAS (Rutas Integrales de Atención en Salud); Ingreso del paciente; Clasificación ATC de medicamentos', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_LEVONORGESTREL_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Inventory.PharmaceuticalDispensing: Devuelve MIN(PD.ConfirmationDate) de las dispensaciones farmacéuticas del paciente cuyo medicamento ATC contiene ''LEVONORGESTREL 0.75'', con cantidad neta entregada > 0 y ConfirmationDate >= @Fecha_Historia; NULL si no existe dispensación.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_LEVONORGESTREL_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PharmaceuticalDispensing; Inventory.PharmaceuticalDispensingDetail; DBO.ADINGRESO; Inventory.InventoryProduct; Inventory.ATC', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_LEVONORGESTREL_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_LEVONORGESTREL_Fecha';
GO
