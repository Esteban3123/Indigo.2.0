
/****** Object:  View [Billing].[ViewListDiagnosticImaging]   ******/
CREATE VIEW [Billing].[ViewListDiagnosticImaging]
as
(
	select distinct cup.Id as ID, cup.Code, cup.Description, hcor.AUTO, hcor.ESTSERIPS, hcor.MEDREALEC, sdet.Id as ServiceOrderDetailId
	from Billing.ServiceOrderDetail sdet
		inner join Contract.CUPSEntity cup on cup.Id = sdet.CUPSEntityId
		inner join HCORDIMAG hcor on hcor.CODSERIPS = cup.Code and hcor.GENSERVICEORDER = sdet.ServiceOrderId

)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista consolidada de imágenes diagnósticas facturadas, que cruza las órdenes de imágenes de la historia clínica (radiografías, ecografías, tomografías, resonancias magnéticas y otros estudios) con el detalle de facturación y el catálogo de servicios CUPS. Para cada imagen solicita muestra el código y descripción del procedimiento CUPS, el estado del servicio, el médico que realizó la lectura o interpretación, y el ítem de la orden de servicio con el que fue facturado. Sirve para conciliar y reportar las imágenes diagnósticas entre la historia clínica y la facturación, facilitando auditorías, glosas y seguimiento de procedimientos de imagenología cobrados al paciente o al pagador.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewListDiagnosticImaging';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewListDiagnosticImaging';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los procedimientos de imágenes diagnósticas asociados a una orden de servicio, cruzando el detalle facturado con el catálogo CUPS y el registro clínico de imágenes (HCORDIMAG) para mostrar autorización, estado IPS y médico que realizó.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListDiagnosticImaging';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada ServiceOrderDetail debe estar enlazado a un CUPSEntity vigente vía CUPSEntityId; Debe existir un registro en HCORDIMAG cuyo CODSERIPS coincida con el código CUPS y cuyo GENSERVICEORDER coincida con el ServiceOrderId del detalle', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListDiagnosticImaging';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan procedimientos cuyo código CUPS coincide exactamente con el CODSERIPS registrado en HCORDIMAG para la misma orden de servicio (hcor.GENSERVICEORDER = sdet.ServiceOrderId); El uso de SELECT DISTINCT garantiza que no se devuelvan filas duplicadas para un mismo CUPS dentro del mismo detalle de orden; Solo aparecen ítems de la orden que estén catalogados como CUPS (INNER JOIN con Contract.CUPSEntity) y que además tengan un registro asociado en HCORDIMAG (imágenes diagnósticas)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListDiagnosticImaging';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de servicio; Detalle de orden de servicio; CUPS (procedimiento en salud); Imágenes diagnósticas; Autorización; Estado IPS del servicio; Médico que realiza', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListDiagnosticImaging';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.ViewListDiagnosticImaging: Devuelve por cada combinación única (CUPS, detalle de orden) los datos: Id y Código/Descripción del CUPS, número de autorización (AUTO), estado del servicio IPS (ESTSERIPS), médico que realiza (MEDREALEC) y el ServiceOrderDetailId, filtrando solo los CUPS que tienen contraparte en HCORDIMAG para la misma orden', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListDiagnosticImaging';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.ServiceOrderDetail; Contract.CUPSEntity; HCORDIMAG', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListDiagnosticImaging';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListDiagnosticImaging';
GO
