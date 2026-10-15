




CREATE VIEW [Payroll].[ViewElectronicPayroll]
AS
	SELECT	ep.Id,
			ep.DocumentType,
			ep.Year,
			ep.Month,
			CONCAT(ep.Year, '-', RIGHT(CONCAT('00', ep.Month), 2)) Period,
			ep.EmployeePartyId,
			CONCAT(tp.Nit, ' - ', tp.Name) EmployeePartyNitName,
			ep.Prefix,
			ep.DocumentNumber,
			ep.CUNE,
			ep.CreationDate,
			ep.ShippingDate,
			ep.ValidationDate,
			ep.Status,
			STUFF
			(
				(
					SELECT DISTINCT CONCAT(', ', 'Se ha confirmado ', 
							CASE v.EntityName
								WHEN 'Liquidation' THEN 'una liquidacion de nomina'
								WHEN 'IncentivePayment' THEN 'una prima'
								WHEN 'ContractLiquidation' THEN 'una liquidacion de contrato'
							END,
							' posterior a la fecha de envio del documento electronico')
					FROM Payroll.ElectronicPayrollPaymentSupportDetail eppsd WITH (NOLOCK)
					JOIN [Payroll].[ViewElectronicPayrollPaymentSupportDetail] v WITH (NOLOCK) ON eppsd.EntityId = v.EntityId AND eppsd.EntityName = v.EntityName
					LEFT JOIN
					(
						SELECT epn.EntityId, MAX(ISNULL(epn.ShippingDate, epn.CreationDate)) ShippingDate
						FROM Payroll.ElectronicPayroll epn
						WHERE epn.DocumentType = 2
						GROUP BY epn.EntityId
					) epn ON ep.Id = epn.EntityId
					WHERE ep.EntityId = eppsd.ElectronicPayrollPaymentSupportId AND ep.EntityName = 'ElectronicPayrollPaymentSupport'
						AND v.ConfirmationDate > COALESCE(epn.ShippingDate, ep.ShippingDate, ep.CreationDate)
						FOR XML PATH('')
				), 1, 1, ''
			) Message
	FROM Payroll.ElectronicPayroll ep WITH (NOLOCK)
	JOIN Common.ThirdParty tp WITH (NOLOCK) ON ep.EmployeePartyId = tp.Id
	--WHERE ep.Status <> 3