
/****** Object:  View [Billing].[ViewListDiagnosticImagingAmbulatory]   ******/
CREATE VIEW [Billing].[ViewListDiagnosticImagingAmbulatory]
as
(
	select distinct cup.Id as ID, cup.Code, cup.Description, ambor.AUTO, ambor.ESTSERIPS, ambor.MEDREALEC, sdet.Id as ServiceOrderDetailId
	from Billing.ServiceOrderDetail sdet
		inner join Contract.CUPSEntity cup on cup.Id = sdet.CUPSEntityId
		inner join AMBORDIMA ambor on ambor.CODSERIPS = cup.Code and ambor.GENSERVICEORDER = sdet.ServiceOrderId

)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los exámenes de imágenes diagnósticas (radiografías, ecografías, tomografías, resonancias, etc.) ordenados en el contexto ambulatorio. Cruza el detalle de la orden de servicio facturada con el catálogo de procedimientos CUPS y con la tabla de órdenes de imágenes ambulatorias (AMBORDIMA), devolviendo el código y descripción del procedimiento, el estado del servicio, el médico que realizó la lectura y el identificador del ítem de la orden. Sirve para reportería de facturación y seguimiento de imágenes diagnósticas en pacientes ambulatorios, permitiendo conocer qué estudios fueron ordenados, su estado de ejecución y quién los interpretó.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewListDiagnosticImagingAmbulatory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewListDiagnosticImagingAmbulatory';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Listar los procedimientos de imágenes diagnósticas ambulatorias, cruzando los detalles de orden de servicio con su CUPS y la información de autorización/estado/médico provista por la tabla externa AMBORDIMA.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListDiagnosticImagingAmbulatory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de registros en AMBORDIMA con CODSERIPS y GENSERVICEORDER que coincidan con el código CUPS y el ServiceOrderId del detalle; El detalle de orden debe estar enlazado a una entidad CUPS vigente', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListDiagnosticImagingAmbulatory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan ítems cuyo código CUPS y orden de servicio tengan correspondencia exacta en AMBORDIMA (CODSERIPS y GENSERVICEORDER); Se eliminan duplicados mediante SELECT DISTINCT; El cruce exige que el detalle de orden esté asociado a una entidad CUPS válida en Contract.CUPSEntity', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListDiagnosticImagingAmbulatory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Imágenes diagnósticas ambulatorias; Orden de servicio; CUPS; Autorización; Estado del servicio IPS; Médico que realiza', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListDiagnosticImagingAmbulatory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.ViewListDiagnosticImagingAmbulatory: Devuelve un conjunto distinto de procedimientos (Id, Code, Description del CUPS) junto con AUTO, ESTSERIPS, MEDREALEC de AMBORDIMA y el ServiceOrderDetailId, solo cuando AMBORDIMA.CODSERIPS = CUPSEntity.Code y AMBORDIMA.GENSERVICEORDER = ServiceOrderDetail.ServiceOrderId', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListDiagnosticImagingAmbulatory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.ServiceOrderDetail; Contract.CUPSEntity; AMBORDIMA', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListDiagnosticImagingAmbulatory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListDiagnosticImagingAmbulatory';
GO
