

CREATE VIEW [Authorization].[ViewAcceptanceAuthorization]
AS

	select CONCAT(t.Id, '') Id, t.PatientCode, p.IPNOMCOMP PatientName, RTRIM(LTRIM(t.PatientCode)) + ' - ' + RTRIM(LTRIM(p.IPNOMCOMP)) PatientCodeName, t.AdmissionNumber,
	t.ServiceCode, temp.CodeName ServiceCodeName,
	fu.Id FunctionalUnitId, fu.Code FunctionalUnitCode, fu.Name FunctionalUnitName, fu.Code + ' - ' + fu.Name FunctionalUnitCodeName,
	fut.Id FunctionalUnitTargetId, fut.Code FunctionalUnitTargetCode, fut.Name FunctionalUnitTargetName, fut.Code + ' - ' + fut.Name FunctionalUnitTargetCodeName,
	t.Id TraceabilityPaperworkId, t.CareCenterCode, t.CareCenterTargetCode
	from [Authorization].TraceabilityPaperwork t
	inner join Payroll.FunctionalUnit fu on fu.Code = t.FunctionalUnitCode
	inner join Payroll.FunctionalUnit fut on fut.Id = t.FunctionalUnitTargetId
	inner join .INPACIENT p on p.IPCODPACI = t.PatientCode
	inner join (
		select ce.Code, 1 Type, ce.Code + ' - ' + ce.Description CodeName
		from Contract.CUPSEntity ce

		union all

		select p.Code, 2 Type, p.Code + ' - ' + p.Name CodeName
		from Inventory.InventoryProduct p
	) temp on temp.Code = t.ServiceCode and temp.Type = t.Type
	where t.AcceptanceStatus is null or (t.Status = 6 and t.AcceptanceStatus is not null)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida las solicitudes de autorización de servicios de salud pendientes de aceptación o en estado de aceptación completada (estado 6). Integra la trazabilidad de trámites de autorización con datos del paciente (cédula y nombre completo), el servicio o procedimiento solicitado (CUPS o medicamento/insumo de inventario), la unidad funcional solicitante y la unidad funcional destino. Sirve para que gestores de autorizaciones consulten en un solo lugar qué trámites requieren aceptación, a qué paciente e ingreso corresponden, y entre qué áreas o servicios se tramita la autorización.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'ViewAcceptanceAuthorization';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'ViewAcceptanceAuthorization';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los trámites de autorización pendientes de aceptación o ya aceptados en estado finalizado, enriquecidos con datos del paciente, unidades funcionales origen/destino y descripción del servicio (CUPS o producto de inventario).', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewAcceptanceAuthorization';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada trámite debe tener un FunctionalUnitCode existente en Payroll.FunctionalUnit (INNER JOIN); Cada trámite debe tener un FunctionalUnitTargetId existente en Payroll.FunctionalUnit (INNER JOIN); El PatientCode debe existir en INPACIENT (INNER JOIN); El ServiceCode debe existir en Contract.CUPSEntity (si Type=1) o en Inventory.InventoryProduct (si Type=2)', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewAcceptanceAuthorization';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El catálogo de servicios se discrimina por Type: 1 = CUPS, 2 = producto de inventario; Los trámites con AcceptanceStatus no nulo solo se exponen cuando su Status es exactamente 6; Los códigos compuestos (CodeName) se construyen siempre como ''Code - Descripción/Nombre'' con espacios y guión', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewAcceptanceAuthorization';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'autorización de servicios; trámite/trazabilidad; aceptación de autorización; paciente; unidad funcional origen y destino; centro de atención; servicio CUPS; producto de inventario', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewAcceptanceAuthorization';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Authorization.ViewAcceptanceAuthorization: Solo retorna trámites donde AcceptanceStatus IS NULL (pendientes) o donde Status = 6 y AcceptanceStatus IS NOT NULL (finalizados con aceptación registrada)', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewAcceptanceAuthorization';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si temp.Type = 1 (servicio CUPS) → Resuelve el nombre del servicio desde Contract.CUPSEntity como Code + '' - '' + Description else Si Type = 2, resuelve el nombre del servicio desde Inventory.InventoryProduct como Code + '' - '' + Name; si t.AcceptanceStatus IS NULL → Incluye el trámite (pendiente de aceptación) else Solo lo incluye si t.Status = 6 y AcceptanceStatus IS NOT NULL', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewAcceptanceAuthorization';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Authorization.TraceabilityPaperwork; Payroll.FunctionalUnit; INPACIENT; Contract.CUPSEntity; Inventory.InventoryProduct', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewAcceptanceAuthorization';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewAcceptanceAuthorization';
GO
