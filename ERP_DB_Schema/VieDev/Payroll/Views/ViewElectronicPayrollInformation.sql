


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