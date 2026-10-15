


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
	  AND IIF(epc.ConceptType = 1, vd.Accrued, vd.Deducted) > 0