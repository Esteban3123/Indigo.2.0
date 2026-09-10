

CREATE VIEW [Inventory].[ViewReportPharmaceuticalDispensingNeckBandPatient]
AS 

SELECT distinct
	pdd.Id Id,
	pd.Id PharmaceuticalDispensingId,
	pd.Code AS 'CodePharmaceuticalDispensing',
	pac.IPNOMCOMP AS 'PacientName',
	pac.IPCODPACI AS 'PacientDocumentNumber',
	CONVERT(VARCHAR, pac.IPFECNACI, 107) 'PacientBirthDate',
	ing.NUMINGRES AS 'AdmissionNumber',
	lp.CODPRODUC AS 'ProductCode',
	RTRIM(isnull(concat(lp.DESPRODUC,CHAR(13)+CHAR(10), CHAR(13)+CHAR(10),RTRIM(mescla.MEZLIQPAC)), lp.DESPRODUC)) AS 'ProductName',
	sfd.CANPEDPRO AS QuantityRequest,
	pdd.Quantity AS QuantityDispensing,
	REPLACE(ISNULL(pres.DESADMINI, mescla.ADMMEZLIQ),'continuamente', '') AS 'Prescription', 
	REPLACE(isnull(pres.INDAPLMED, mescla.INDAPLMED), 'continuamente', '') AS 'ApplyInstructions',
	pdn.Note AS 'PharmaceuticalNotes',
	IIF
	(
		(CHARINDEX('vía', REPLACE(ISNULL(pres.DESADMINI, mescla.ADMMEZLIQ),'continuamente', ''))) > 0,
		SUBSTRING(REPLACE(ISNULL(pres.DESADMINI, mescla.ADMMEZLIQ),'continuamente', ''), CHARINDEX('vía',  REPLACE(ISNULL(pres.DESADMINI, mescla.ADMMEZLIQ),'continuamente', '')) + LEN('Via') + 1, LEN( REPLACE(ISNULL(pres.DESADMINI, mescla.ADMMEZLIQ),'continuamente', ''))),
		ISNULL(viaMed.DESVIAADM, via.DESVIAADM)
	) AS 'Via'
	--ISNULL(viaMed.DESVIAADM, via.DESVIAADM) AS 'Via'
FROM Inventory.PharmaceuticalDispensing pd WITH (NOLOCK)
JOIN Inventory.PharmaceuticalDispensingDetail pdd WITH (NOLOCK) ON pd.Id = pdd.PharmaceuticalDispensingId
JOIN dbo.ADINGRESO ing ON pd.AdmissionNumber = ing.NUMINGRES
JOIN dbo.INPACIENT pac WITH (NOLOCK) ON ing.IPCODPACI = pac.IPCODPACI
JOIN dbo.HCFARMEPD sfd WITH (NOLOCK) ON sfd.ID = pdd.EntityId AND 'HCFARMEPD' = pdd.EntityName
JOIN dbo.IHLISTPRO lp WITH (NOLOCK) ON lp.CODPRODUC = sfd.CODPRODUC
LEFT JOIN Inventory.PharmaceuticalDispensingNotes pdn WITH (NOLOCK) ON pdn.PharmaceuticalRequestDetailId = sfd.ID
--------- preescripciòn -----------
LEFT JOIN dbo.HCPRESCRA pres WITH (NOLOCK) ON pres.ID = sfd.IdSourceTable AND 'HCPRESCRA' = isnull(sfd.SourceTable, 'HCPRESCRA')
LEFT JOIN dbo.HCINFLIQA mescla WITH (NOLOCK) ON mescla.CONSECUTI = sfd.IdSourceTable AND 'HCINFLIQA' = sfd.SourceTable
--------- via ---------------
LEFT JOIN dbo.HCVIAADMI viaMed WITH (NOLOCK) ON viaMed.CODVIAADM = pres.CODVIAADM
LEFT JOIN dbo.HCVIAADMI via WITH (NOLOCK) ON via.CODVIAADM = lp.CODVIAADM
WHERE lp.TIPPRODUC IN (1, 3)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida la información necesaria para imprimir o generar la colilla (rotulado o ''neck band'') de dispensación farmacéutica por paciente. Integra el documento de despacho de medicamentos con el detalle de cada ítem dispensado, los datos del paciente (nombre, cédula, fecha de nacimiento) y su número de ingreso u hospitalización. Complementa la información con el catálogo de productos farmacéuticos (medicamentos e insumos de tipo 1 y 3), la prescripción o fórmula médica original (ya sea una prescripción estándar o una mezcla/infusión líquida), las instrucciones de administración, la vía de administración del medicamento y las notas u observaciones del proceso de dispensación. Es utilizada para reportería operativa de farmacia, permitiendo identificar qué medicamento fue despachado, en qué cantidad, cómo debe administrarse y a qué paciente corresponde cada despacho.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewReportPharmaceuticalDispensingNeckBandPatient';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewReportPharmaceuticalDispensingNeckBandPatient';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida la información necesaria para imprimir el manilla/rotulo de dispensación farmacéutica por paciente, mostrando datos del paciente, del producto, cantidades, prescripción, indicaciones, vía y notas farmacéuticas.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportPharmaceuticalDispensingNeckBandPatient';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El detalle de dispensación debe estar vinculado a un pedido farmacéutico HCFARMEPD (EntityName=''HCFARMEPD''); El ingreso del paciente debe existir en ADINGRESO y referenciar un paciente válido en INPACIENT; El producto del pedido debe existir en el catálogo IHLISTPRO y ser de tipo 1 o 3', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportPharmaceuticalDispensingNeckBandPatient';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen ítems cuyo producto en IHLISTPRO sea de tipo 1 o 3 (TIPPRODUC IN (1,3)); La palabra ''continuamente'' se elimina siempre de las cadenas de prescripción e indicaciones de aplicación mostradas; La fecha de nacimiento del paciente se presenta en formato MMM dd, yyyy (estilo 107); Cada fila combina obligatoriamente dispensación, detalle, ingreso, paciente, pedido farmacéutico y producto (joins internos); las notas, prescripción, mezcla y vías son opcionales; La vía de administración del producto se usa como fallback cuando no hay vía definida en la prescripción', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportPharmaceuticalDispensingNeckBandPatient';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Dispensación farmacéutica; Paciente; Ingreso/Admisión hospitalaria; Prescripción médica; Mezcla de líquidos para infusión; Vía de administración; Producto farmacéutico; Notas farmacéuticas; Manilla/rótulo de paciente', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportPharmaceuticalDispensingNeckBandPatient';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Inventory.PharmaceuticalDispensingDetail: Cuando el producto asociado es de tipo 1 o 3 (TIPPRODUC IN (1,3)), se retorna una fila por detalle de dispensación con datos consolidados de paciente, ingreso, producto, prescripción y vía', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportPharmaceuticalDispensingNeckBandPatient';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si El detalle dispensado proviene de HCPRESCRA (prescripción) o HCINFLIQA (mezcla de líquidos), determinado por SourceTable → Se toma la descripción de administración, indicaciones y vía desde la fuente correspondiente, priorizando HCPRESCRA cuando SourceTable es nulo; si La cadena de prescripción contiene la palabra ''vía'' → Se extrae la vía de administración como el texto que sigue a ''vía'' dentro de la prescripción else Se usa la vía del catálogo HCVIAADMI vinculada a la prescripción y, en su defecto, la vía asociada al producto; si Existe descripción de mezcla (MEZLIQPAC) para el ítem → Se concatena al nombre del producto separada por saltos de línea else Se muestra solo la descripción del producto', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportPharmaceuticalDispensingNeckBandPatient';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PharmaceuticalDispensing; Inventory.PharmaceuticalDispensingDetail; dbo.ADINGRESO; dbo.INPACIENT; dbo.HCFARMEPD; dbo.IHLISTPRO; Inventory.PharmaceuticalDispensingNotes; dbo.HCPRESCRA; dbo.HCINFLIQA; dbo.HCVIAADMI', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportPharmaceuticalDispensingNeckBandPatient';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportPharmaceuticalDispensingNeckBandPatient';
GO
