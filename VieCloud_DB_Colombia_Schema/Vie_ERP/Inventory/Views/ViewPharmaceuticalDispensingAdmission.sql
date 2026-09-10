

CREATE VIEW [Inventory].[ViewPharmaceuticalDispensingAdmission]
AS
SELECT  CONCAT(pd.Id, ' - ', ing.NUMINGRES) Id,
		pd.Id PharmaceuticalDispensingId,
		CONCAT(RTRIM(pac.IPCODPACI), ' - ', pac.IPNOMCOMP) PatientName,
		ISNULL(bed.NUMCAMHOS, '') AS Bed
FROM Inventory.PharmaceuticalDispensing pd
JOIN dbo.ADINGRESO ing ON pd.AdmissionNumber = ing.NUMINGRES
JOIN dbo.INPACIENT pac ON ing.IPCODPACI = pac.IPCODPACI
LEFT JOIN dbo.CHCAMASHO bed ON ISNULL(CASE WHEN ing.CODCAMACT = 0 THEN '' ELSE CAST(ing.CODCAMACT AS VARCHAR(15)) END, '') = bed.CODICAMAS
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que combina los documentos de dispensación farmacéutica con los datos del ingreso hospitalario, el paciente y la cama asignada. Para cada despacho de medicamentos muestra un identificador compuesto (dispensación + número de ingreso), la cédula y nombre completo del paciente, y el número de cama donde se encuentra hospitalizado. Sirve para que el personal de farmacia y enfermería consulte rápidamente a qué paciente ingresado y en qué cama corresponde cada dispensación de medicamentos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewPharmaceuticalDispensingAdmission';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewPharmaceuticalDispensingAdmission';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone las dispensaciones farmacéuticas asociadas a una admisión, mostrando el paciente y la cama actual para facilitar su identificación operativa.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPharmaceuticalDispensingAdmission';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada dispensación farmacéutica debe estar asociada a un número de admisión existente en ADINGRESO; Cada admisión debe referenciar un paciente existente en INPACIENT', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPharmaceuticalDispensingAdmission';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El Id expuesto siempre concatena el Id de la dispensación con el número de ingreso separado por '' - ''; El nombre del paciente siempre se presenta como ''código - nombre completo''; La cama se presenta como cadena vacía cuando no hay cama activa o el código es 0; Solo se incluyen dispensaciones cuya admisión y paciente existan (INNER JOIN); la cama es opcional (LEFT JOIN)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPharmaceuticalDispensingAdmission';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'dispensación farmacéutica; admisión/ingreso; paciente; cama hospitalaria', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPharmaceuticalDispensingAdmission';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Inventory.PharmaceuticalDispensing: Devuelve un identificador compuesto por el Id de la dispensación y el número de ingreso, junto con datos del paciente y cama', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPharmaceuticalDispensingAdmission';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ing.CODCAMACT = 0 → Se considera cama vacía ('''') y no se enlaza con CHCAMASHO else Se convierte el código de cama a varchar y se busca en CHCAMASHO', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPharmaceuticalDispensingAdmission';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PharmaceuticalDispensing; dbo.ADINGRESO; dbo.INPACIENT; dbo.CHCAMASHO', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPharmaceuticalDispensingAdmission';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPharmaceuticalDispensingAdmission';
GO
