CREATE VIEW [Authorization].[ViewWithdrawal]
AS
SELECT	CONCAT('ManagementMedicalOrder-', mmo.Id) Id,
		mmo.EntityName, mmo.EntityId,
		ou.UnitName OperatingUnit,
		ha.Name HealthAdministrator,
		CONCAT(RTRIM(fu.UFUCODIGO), ' - ', fu.UFUDESCRI) FunctionalUnit,
		mmo.PatientCode,
		p.IPNOMCOMP PatientName,
		mmo.AdmissionNumber,
		mmo.Folio,
		ing.CODCAMACT Bed,
		ing.IFECHAING AdmissionDate,
		ing.FECHEGRESO EgressDate,
		mmo.ProfessionalCode Professional,
		mmo.RequestDate,
		mmo.Type,
		mmo.ItemCode,
		IIF(mmo.Type = 1, ce.Description, ip.Name) ItemName,
		mmo.RequestQuantity Quantity,
		mmo.Status
FROM Common.OperatingUnit ou
JOIN [Authorization].ManagementMedicalOrder mmo ON ou.Id = mmo.OperatingUnitId
JOIN dbo.INPACIENT p ON mmo.PatientCode = p.IPCODPACI
JOIN dbo.ADINGRESO ing ON mmo.AdmissionNumber = ing.NUMINGRES
JOIN .INUNIFUNC fu ON mmo.FunctionalUnitCode = fu.UFUCODIGO
JOIN Contract.HealthAdministrator ha ON ing.GENCONENTITY = ha.Id
LEFT JOIN Contract.CUPSEntity ce ON mmo.Type = 1 AND mmo.ItemCode = ce.Code 
LEFT JOIN Inventory.InventoryProduct ip ON mmo.Type = 2 AND mmo.ItemCode = ip.Code
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida las solicitudes de retiro de ítems clínicos (procedimientos CUPS y medicamentos/insumos) gestionadas bajo el módulo de autorizaciones. Integra datos del paciente (nombre, cédula), el ingreso hospitalario (número de ingreso, cama, fechas de admisión y egreso), la unidad funcional, la sede operativa y la administradora de salud (EPS/pagador) responsable. Dependiendo del tipo de ítem solicitado, muestra la descripción del procedimiento CUPS o el nombre del producto de inventario, junto con el profesional solicitante, la cantidad pedida y el estado de la solicitud. Sirve para reportería y seguimiento de retiros autorizados de servicios y medicamentos por ingreso de paciente.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'ViewWithdrawal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'ViewWithdrawal';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista consolidada de órdenes médicas gestionadas en el módulo de autorizaciones, enriquecida con datos del paciente, ingreso, unidad funcional, administradora de salud y descripción del ítem (CUPS o producto de inventario).', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewWithdrawal';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La orden médica debe tener una unidad operativa, paciente, ingreso (admisión), unidad funcional y administradora de salud asociados para aparecer en la vista (JOINs internos).', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewWithdrawal';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El campo FunctionalUnit siempre se presenta con el formato ''CÓDIGO - DESCRIPCIÓN'' (RTRIM del código + '' - '' + descripción).; La administradora de salud mostrada corresponde a la asociada al ingreso (ADINGRESO.GENCONENTITY), no directamente a la orden médica.; Solo se incluyen órdenes cuyo ítem es resoluble como CUPS (Type=1) o producto de inventario (Type=2); otros tipos quedan con ItemName en NULL por los LEFT JOINs condicionados.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewWithdrawal';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'orden médica; autorización; paciente; ingreso/admisión hospitalaria; egreso; cama; unidad funcional; unidad operativa/sede; administradora de salud (EPS/pagador); CUPS (procedimiento); producto de inventario; profesional tratante; folio', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewWithdrawal';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Authorization.ViewWithdrawal: Devuelve un Id sintético con el prefijo ''ManagementMedicalOrder-'' concatenado al Id de la orden médica.; [RETURN_RESULT] Authorization.ViewWithdrawal: Cuando Type = 1 se toma la descripción del ítem desde Contract.CUPSEntity; cuando Type = 2 se toma el nombre desde Inventory.InventoryProduct (IIF sobre mmo.Type).', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewWithdrawal';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si mmo.Type = 1 → ItemName proviene de Contract.CUPSEntity.Description (procedimiento CUPS) else Si mmo.Type = 2, ItemName proviene de Inventory.InventoryProduct.Name (producto de inventario)', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewWithdrawal';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.OperatingUnit; Authorization.ManagementMedicalOrder; dbo.INPACIENT; dbo.ADINGRESO; dbo.INUNIFUNC; Contract.HealthAdministrator; Contract.CUPSEntity; Inventory.InventoryProduct', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewWithdrawal';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewWithdrawal';
GO
