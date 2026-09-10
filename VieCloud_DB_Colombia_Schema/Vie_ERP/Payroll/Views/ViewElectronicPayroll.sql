

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
	WHERE (
	-- Las Notas de Ajuste (EntityName='ElectronicPayroll') siempre se muestran
	ep.EntityName <> 'ElectronicPayrollPaymentSupport'
	OR EXISTS
	(
		-- parametrizado diferente a No Aplica (InternalCode <> 0)
		SELECT 1
		FROM Payroll.ElectronicPayrollPaymentSupportDetail eppsd WITH (NOLOCK)
		JOIN Payroll.ViewElectronicPayrollPaymentSupportDetail v WITH (NOLOCK)
			ON eppsd.EntityId = v.EntityId AND eppsd.EntityName = v.EntityName
		WHERE eppsd.ElectronicPayrollPaymentSupportId = ep.EntityId
			AND v.Type <> 0   -- InternalCode = 0 = No Aplica
			AND v.Value > 0
	)
)

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de nómina electrónica que consolida la información de los documentos de nómina electrónica transmitidos o pendientes de transmisión a la DIAN, combinando los datos del documento (tipo, año, mes, período, prefijo, número, CUNE) con la identificación del empleado (NIT y nombre obtenidos de la tabla de terceros). Adicionalmente, genera un mensaje de alerta cuando alguno de los conceptos asociados al soporte de pago (liquidación de nómina, prima o liquidación de contrato) fue confirmado con una fecha posterior al envío del documento electrónico, advirtiendo así posibles inconsistencias entre el documento ya enviado y novedades registradas después. Sirve como fuente principal para la consulta, seguimiento y auditoría de comprobantes de nómina electrónica por empleado y período, incluyendo su estado de envío y validación ante la DIAN.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewElectronicPayroll';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewElectronicPayroll';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los documentos de nómina electrónica enriquecidos con datos del empleado, período formateado y un mensaje de alerta cuando existen liquidaciones, primas o liquidaciones de contrato confirmadas después del envío del documento.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewElectronicPayroll';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada ElectronicPayroll debe tener un EmployeePartyId que exista en Common.ThirdParty (JOIN no LEFT).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewElectronicPayroll';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El campo Period siempre se formatea como ''YYYY-MM'' con el mes rellenado a 2 dígitos.; EmployeePartyNitName siempre se construye como ''Nit - Name'' del tercero asociado.; El mensaje de alerta solo aplica a documentos cuyo EntityName sea ''ElectronicPayrollPaymentSupport''.; La comparación de ConfirmationDate usa primero ShippingDate de la nota electrónica (DocumentType=2); si no existe, usa ShippingDate del documento; si tampoco existe, usa CreationDate.; Se usa NOLOCK en todas las lecturas, permitiendo lecturas sucias.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewElectronicPayroll';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Nómina electrónica; DIAN/CUNE; Liquidación de nómina; Prima (IncentivePayment); Liquidación de contrato; Soporte de pago de nómina electrónica; Nota de ajuste de nómina electrónica (DocumentType=2); Tercero/empleado (NIT); Período año-mes', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewElectronicPayroll';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.ElectronicPayroll: Devuelve todos los registros de ElectronicPayroll unidos con ThirdParty; el filtro ''WHERE ep.Status <> 3'' está comentado, por lo que se incluyen también los documentos en estado 3.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewElectronicPayroll';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si v.EntityName = ''Liquidation'' → El mensaje describe la confirmación como ''una liquidacion de nomina''.; si v.EntityName = ''IncentivePayment'' → El mensaje describe la confirmación como ''una prima''.; si v.EntityName = ''ContractLiquidation'' → El mensaje describe la confirmación como ''una liquidacion de contrato''.; si ep.EntityName = ''ElectronicPayrollPaymentSupport'' y v.ConfirmationDate > COALESCE(epn.ShippingDate, ep.ShippingDate, ep.CreationDate) → Se concatena un mensaje de alerta indicando que se confirmó un evento posterior al envío del documento electrónico. else El campo Message queda vacío/NULL.; si epn.DocumentType = 2 (nota electrónica) agrupado por EntityId → Se toma como referencia temporal el MAX(ISNULL(ShippingDate, CreationDate)) de las notas asociadas para comparar contra ConfirmationDate.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewElectronicPayroll';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.ElectronicPayroll; Common.ThirdParty; Payroll.ElectronicPayrollPaymentSupportDetail; Payroll.ViewElectronicPayrollPaymentSupportDetail', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewElectronicPayroll';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewElectronicPayroll';
GO
