

-- =============================================
-- Author:		Yohana Rozo
-- Create date: <2021-06-09>
-- Description:	<Funcion que retorna el valor correspondiente a Suministro de Acido Folico por mimima fecha>
-- ==========================================
CREATE FUNCTION [Report].[FN_Medicamento_Rias_MEDICA_LEPRA_Fecha]
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
WHERE (ATC.NAME LIKE ('%DAPSONA%') OR ATC.NAME LIKE ('%RIFAMPICINA%') OR ATC.NAME LIKE ('%CLOFAZ%') OR ATC.NAME LIKE ('%OFLOXA%') )
AND PDD.Quantity-PDD.ReturnedQuantity>0
AND ING.IPCODPACI=@ipcodpaci
AND PD.ConfirmationDate>=@Fecha_Historia 
--GROUP BY IPCODPACI
) A

 )
	RETURN @variable

END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Función escalar que retorna la fecha mínima de confirmación de dispensación de medicamentos antileprosos (Dapsona, Rifampicina, Clofazimina u Ofloxacina) para un paciente específico, considerando solo entregas posteriores a una fecha de historia clínica dada y con cantidad neta dispensada mayor a cero. Se utiliza en reportes de RIAS para registrar el primer suministro del esquema farmacológico de tratamiento de lepra.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_MEDICA_LEPRA_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_MEDICA_LEPRA_Fecha';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve la fecha mínima de confirmación de dispensación de medicamentos antileprosos (poliquimioterapia) entregados a un paciente desde una fecha de referencia, para reportes RIAS de lepra.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_MEDICA_LEPRA_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos una dispensación farmacéutica confirmada del paciente cuyo medicamento ATC contenga DAPSONA, RIFAMPICINA, CLOFAZ u OFLOXA.; La cantidad neta entregada (Quantity - ReturnedQuantity) debe ser mayor a 0.; La fecha de confirmación de la dispensación debe ser mayor o igual a la fecha de historia indicada.; El ingreso (ADINGRESO.NUMINGRES) debe estar relacionado con el paciente (IPCODPACI) suministrado.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_MEDICA_LEPRA_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se evalúan dispensaciones cuya ConfirmationDate sea ≥ @Fecha_Historia.; Se identifica al paciente vía ADINGRESO.IPCODPACI a través del NUMINGRES de la dispensación.; El criterio de medicamento antileproso se basa exclusivamente en coincidencia textual del nombre ATC, no en código.; Solo se cuentan unidades efectivamente entregadas (cantidad menos devolución > 0).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_MEDICA_LEPRA_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Dispensación farmacéutica; Poliquimioterapia para lepra (Dapsona, Rifampicina, Clofazimina, Ofloxacino); Clasificación ATC de medicamentos; Ingreso del paciente; RIAS (Rutas Integrales de Atención en Salud); Devolución de medicamentos', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_MEDICA_LEPRA_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Inventory.PharmaceuticalDispensing: Retorna MIN(PD.ConfirmationDate) cuando el ATC del producto coincide con DAPSONA/RIFAMPICINA/CLOFAZ/OFLOXA, la cantidad neta es >0 y la dispensación es posterior o igual a @Fecha_Historia para el paciente; si no hay coincidencias retorna NULL.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_MEDICA_LEPRA_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ATC.NAME LIKE ''%DAPSONA%'' OR ''%RIFAMPICINA%'' OR ''%CLOFAZ%'' OR ''%OFLOXA%'' → El medicamento se considera parte del esquema antileproso y la dispensación entra al cálculo de fecha mínima. else La dispensación se ignora.; si PDD.Quantity - PDD.ReturnedQuantity > 0 → Se considera que hubo entrega efectiva y participa en el MIN(ConfirmationDate). else Se excluye la fila al considerarse devolución total.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_MEDICA_LEPRA_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PharmaceuticalDispensing; Inventory.PharmaceuticalDispensingDetail; DBO.ADINGRESO; Inventory.InventoryProduct; Inventory.ATC', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_MEDICA_LEPRA_Fecha';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'FN_Medicamento_Rias_MEDICA_LEPRA_Fecha';
GO
