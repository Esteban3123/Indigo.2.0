

CREATE VIEW [Payroll].[ViewElectronicPayrollInformation]
AS 
	SELECT	CONCAT(v.EntityName, '-', v.EntityId, '-', v.EntityDetailId) Id,
			ep.Id ElectronicPayrollId,
			ep.Year, ep.Month,
			tp.Nit EmployeePartyNit,
			tp.Name EmployeePartyName,
			v.Nature,
			CASE v.Nature
						WHEN 1 THEN 'Debito'
						WHEN 2 THEN 'Credito'
						ELSE 'Debito'
			END AS NatureDescription,
			--IIF(v.Nature = 1, 'Debito', 'Credito') NatureDescription,
			v.Detail,
			v.DateStart,
			v.DateEnd,
			v.Quantity,
			v.Percentage,
			v.Value,
			ep.CUNE
	FROM Common.ThirdParty tp WITH (NOLOCK)
	JOIN Payroll.ElectronicPayroll ep WITH (NOLOCK) ON tp.Id = ep.EmployeePartyId
	JOIN Payroll.ElectronicPayrollPaymentSupportDetail eppsd WITH (NOLOCK) ON ep.EntityId = eppsd.ElectronicPayrollPaymentSupportId AND ep.EntityName = 'ElectronicPayrollPaymentSupport'
	JOIN [Payroll].[ViewElectronicPayrollPaymentSupportDetail] v WITH (NOLOCK) ON eppsd.EntityId = v.EntityId AND eppsd.EntityName = v.EntityName
	UNION ALL
	SELECT	CONCAT(v.EntityName, '-', v.EntityId, '-', v.EntityDetailId) Id,
			ep.Id ElectronicPayrollId,
			ep.Year, ep.Month,
			tp.Nit EmployeePartyNit,
			tp.Name EmployeePartyName,
			v.Nature,
			CASE v.Nature
						WHEN 1 THEN 'Debito'
						WHEN 2 THEN 'Credito'
						ELSE 'Debito'
			END AS NatureDescription,
			--IIF(v.Nature = 1, 'Debito', 'Credito') NatureDescription, --
			v.Detail,
			v.DateStart,
			v.DateEnd,
			v.Quantity,
			v.Percentage,
			v.Value,
			ep.CUNE
	FROM Common.ThirdParty tp WITH (NOLOCK)
	JOIN Payroll.ElectronicPayroll ep WITH (NOLOCK) ON tp.Id = ep.EmployeePartyId
	JOIN Payroll.ElectronicPayroll EP1 WITH (NOLOCK) ON EP1.Id = ep.EntityId
	JOIN Payroll.ElectronicPayrollPaymentSupport EPPS WITH(NOLOCK) ON EPPS.Id = ep1.EntityId AND EP1.EntityName = 'ElectronicPayrollPaymentSupport'
	JOIN Payroll.ElectronicPayrollPaymentSupportDetail eppsd WITH (NOLOCK) ON eppsd.ElectronicPayrollPaymentSupportId = EPPS.Id
	JOIN [Payroll].[ViewElectronicPayrollPaymentSupportDetail] v WITH (NOLOCK) ON eppsd.EntityId = v.EntityId AND eppsd.EntityName = v.EntityName
	where ep.DocumentType = 2 and ep.EntityName = 'ElectronicPayroll'
-- =====================================================================
	-- UNION 3: Vacaciones pago inmediato (TypePayment = 1, State = 2)
	-- =====================================================================
	UNION ALL
	SELECT	CONCAT('Vacation-', v.Id, '-', vd.Id) Id,
			ep.Id ElectronicPayrollId,
			ep.Year, ep.Month,
			tp.Nit EmployeePartyNit,
			tp.Name EmployeePartyName,
			epc.ConceptType Nature,
			CASE epc.ConceptType
						WHEN 1 THEN 'Debito'
						WHEN 2 THEN 'Credito'
						ELSE 'Debito'
			END AS NatureDescription,
			c.Name Detail,
			IIF(c.ConceptClass = '030', v.VacationStartDate, NULL) DateStart,
			IIF(c.ConceptClass = '030', v.VacationEndDate,   NULL) DateEnd,
			IIF(c.ConceptClass = '030', v.EnjoyDays, 0) Quantity,
			0 Percentage,
			IIF(epc.ConceptType = 1, vd.Accrued, vd.Deducted) Value,
			ep.CUNE
	FROM Common.ThirdParty tp WITH (NOLOCK)
	JOIN Payroll.ElectronicPayroll ep                               WITH (NOLOCK) ON tp.Id = ep.EmployeePartyId
																				 AND ep.EntityName = 'ElectronicPayrollPaymentSupport'
	JOIN Payroll.ElectronicPayrollPaymentSupport epps               WITH (NOLOCK) ON epps.Id = ep.EntityId
	JOIN Payroll.Vacation v                                         WITH (NOLOCK) ON YEAR(v.VacationStartDate)  = ep.Year
																				 AND MONTH(v.VacationStartDate) = ep.Month
	JOIN Payroll.VacationPeriod vp                                  WITH (NOLOCK) ON vp.Id = v.VacationPeriodId
	JOIN Payroll.Employee e                                         WITH (NOLOCK) ON e.Id  = vp.EmployeeId
																				 AND e.ThirdPartyId = epps.EmployeePartyId
	JOIN Payroll.VacationDetail vd                                  WITH (NOLOCK) ON vd.IdVacation = v.Id
	JOIN Payroll.Concept c                                          WITH (NOLOCK) ON c.Id  = vd.IdConcept
	INNER JOIN Payroll.ElectronicPayrollConcepts epc                WITH (NOLOCK) ON epc.Id = c.IdElectronicPayrollConcepts
	LEFT  JOIN Payroll.ElectronicPayrollConceptSubtype epcs         WITH (NOLOCK) ON epcs.Id = c.IdElectronicPayrollConceptSubtype
	WHERE v.TypePayment             = 1		-- Solo pago inmediato
	  AND v.State                   = 2		-- Solo confirmadas/pagadas
	  AND vd.IdConcept              IS NOT NULL
	  AND epc.ConceptType           IN (1, 2)
	  AND epc.State                 = 1
	  -- BUG-39883: el concepto de vacaciones (clase '030') gobierna todo el VacationDetail.
	  -- Si no esta clasificado para nomina electronica, no viaja ningun concepto de la vacacion.
	  AND EXISTS
	  (
		SELECT 1
		FROM Payroll.VacationDetail vdr                       WITH (NOLOCK)
		JOIN Payroll.Concept cr                               WITH (NOLOCK) ON cr.Id   = vdr.IdConcept
		JOIN Payroll.ElectronicPayrollConcepts epcr           WITH (NOLOCK) ON epcr.Id = cr.IdElectronicPayrollConcepts
		WHERE vdr.IdVacation   = v.Id
		  AND cr.ConceptClass  = '030'
		  AND epcr.ConceptType IN (1, 2)
		  AND epcr.State       = 1
	  )
	  AND IIF(epc.ConceptType = 1, vd.Accrued, vd.Deducted) > 0
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consolida toda la información de nómina electrónica transmitida a la DIAN por empleado y período (año/mes), integrando los conceptos devengados y deducidos (débitos y créditos) de cada comprobante de pago. Combina tres fuentes mediante UNION: los soportes de pago estándar, las nóminas de ajuste o notas de nómina electrónica (documento tipo 2), y las vacaciones de pago inmediato confirmadas. Para cada línea expone el NIT y nombre del empleado, el concepto liquidado con su naturaleza (Débito/Crédito), fechas, cantidades, porcentajes, valores y el código CUNE asignado por la DIAN. Sirve como fuente principal para reportes de nómina electrónica, auditoría de conceptos pagados y verificación del cumplimiento ante la DIAN.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewElectronicPayrollInformation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewElectronicPayrollInformation';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola vista la información de nómina electrónica (original y notas de ajuste) con sus conceptos de detalle, datos del empleado y naturaleza débito/crédito, para reporte y trazabilidad ante la DIAN.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewElectronicPayrollInformation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros en Payroll.ElectronicPayroll vinculados a Common.ThirdParty mediante EmployeePartyId.; Para el primer bloque, los registros de ElectronicPayroll deben referenciar a un ElectronicPayrollPaymentSupport (EntityName=''ElectronicPayrollPaymentSupport'').; Para el segundo bloque, deben existir notas de ajuste (DocumentType=2, EntityName=''ElectronicPayroll'') encadenadas a otra ElectronicPayroll que a su vez apunte a un ElectronicPayrollPaymentSupport.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewElectronicPayrollInformation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Todas las filas se asocian siempre a un tercero (empleado) existente en Common.ThirdParty mediante INNER JOIN por EmployeePartyId.; NatureDescription nunca es NULL: por defecto se asigna ''Debito'' si Nature no es 1 ni 2.; El UNION ALL puede producir duplicados intencionales (no DISTINCT) entre el documento original y sus ajustes.; El detalle siempre proviene de Payroll.ViewElectronicPayrollPaymentSupportDetail enlazado por (EntityId, EntityName).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewElectronicPayrollInformation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Nómina electrónica; CUNE; Soporte de pago de nómina; Empleado (tercero); Naturaleza débito/crédito; Devengados y deducciones; Notas de ajuste de nómina electrónica (DocumentType=2); DIAN', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewElectronicPayrollInformation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve un Id sintético construido como CONCAT(EntityName,''-'',EntityId,''-'',EntityDetailId) por cada línea de detalle de soporte de pago.; [RETURN_RESULT] N/A: Traduce v.Nature: 1→''Debito'', 2→''Credito'', cualquier otro valor (incluido NULL) → ''Debito'' por defecto.; [RETURN_RESULT] N/A: El segundo SELECT del UNION ALL solo expone documentos donde ep.DocumentType = 2 y ep.EntityName = ''ElectronicPayroll'' (notas de ajuste sobre nómina electrónica).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewElectronicPayrollInformation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si v.Nature = 1 → NatureDescription = ''Debito'' else Si v.Nature = 2 → ''Credito''; cualquier otro valor → ''Debito''; si ep.DocumentType = 2 AND ep.EntityName = ''ElectronicPayroll'' (segundo bloque del UNION ALL) → Se incluyen documentos de ajuste que referencian otra ElectronicPayroll y a través de ella un ElectronicPayrollPaymentSupport else El primer bloque cubre nómina electrónica que apunta directamente a ElectronicPayrollPaymentSupport', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewElectronicPayrollInformation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.ThirdParty; Payroll.ElectronicPayroll; Payroll.ElectronicPayrollPaymentSupport; Payroll.ElectronicPayrollPaymentSupportDetail; Payroll.ViewElectronicPayrollPaymentSupportDetail', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewElectronicPayrollInformation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewElectronicPayrollInformation';
GO
