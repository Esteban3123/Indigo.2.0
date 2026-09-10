

CREATE VIEW [Inventory].[ViewRealizedDispensingByManualDispensing]
AS

select ROW_NUMBER() OVER (ORDER BY mfpd.Id) AS Id,
mfpd.MedicalFormulaId, 
mfpd.PharmaceuticalDispensingId, 
mfpd.PharmaceuticalDispensingCode, 
pd.AdmissionNumber, 
i.Id InvoiceId, 
i.InvoiceNumber
from Inventory.MedicalFormulaPharmaceuticalDispensing mfpd WITH(NOLOCK)
inner join Inventory.PharmaceuticalDispensing pd WITH(NOLOCK) on pd.Id = mfpd.PharmaceuticalDispensingId
inner join Billing.Invoice i WITH(NOLOCK) on i.AdmissionNumber = pd.AdmissionNumber
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona las dispensaciones farmacéuticas realizadas de forma manual con sus fórmulas médicas y las facturas de cobro correspondientes. Para cada despacho de medicamentos o insumos generado manualmente en farmacia, muestra el identificador de la fórmula médica prescrita, el código del documento de dispensación, el número de ingreso del paciente y el número de factura asociado al ingreso. Sirve como punto de consulta para rastrear qué dispensaciones manuales de farmacia ya fueron ejecutadas y a qué factura de venta quedaron vinculadas, facilitando la conciliación entre el proceso asistencial de medicamentos y el proceso de facturación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewRealizedDispensingByManualDispensing';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewRealizedDispensingByManualDispensing';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone las dispensaciones farmacéuticas realizadas de forma manual asociadas a una fórmula médica, junto con la admisión y la factura correspondiente del paciente.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewRealizedDispensingByManualDispensing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros en Inventory.MedicalFormulaPharmaceuticalDispensing vinculados a Inventory.PharmaceuticalDispensing.; La admisión asociada a la dispensación debe existir también en Billing.Invoice mediante AdmissionNumber para que el registro aparezca.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewRealizedDispensingByManualDispensing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen dispensaciones cuya admisión tiene al menos una factura asociada (INNER JOIN con Billing.Invoice por AdmissionNumber).; Solo se exponen dispensaciones farmacéuticas que estén ligadas a una fórmula médica vía MedicalFormulaPharmaceuticalDispensing.; Si una admisión tiene varias facturas, la dispensación se replica una vez por cada factura (producto cartesiano controlado por AdmissionNumber).; El identificador Id es generado dinámicamente con ROW_NUMBER y no es estable entre ejecuciones.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewRealizedDispensingByManualDispensing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Fórmula médica; Dispensación farmacéutica; Admisión; Factura', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewRealizedDispensingByManualDispensing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Inventory.MedicalFormulaPharmaceuticalDispensing: Devuelve el cruce entre fórmulas médicas, sus dispensaciones farmacéuticas y las facturas de la admisión del paciente, omitiendo dispensaciones cuya admisión no tenga factura emitida.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewRealizedDispensingByManualDispensing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.MedicalFormulaPharmaceuticalDispensing; Inventory.PharmaceuticalDispensing; Billing.Invoice', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewRealizedDispensingByManualDispensing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewRealizedDispensingByManualDispensing';
GO
