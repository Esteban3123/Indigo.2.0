
CREATE   PROCEDURE [Billing].[ONCO_SP_Billing]
	@OperatingUnitCode VARCHAR(20),
	@DateStart DATE,
	@DateEnd DATE
AS
BEGIN
	SET NOCOUNT ON

	SET @OperatingUnitCode = IIF(ISNULL(@OperatingUnitCode, '') = '', NULL, @OperatingUnitCode)

	SELECT	uo.UnitName AS Sede, 
            CASE f.DocumentType 
				WHEN '1' THEN 'Factura EAPB con Contrato' 
				WHEN '2' THEN 'Factura EAPB Sin Contrato' 
				WHEN '3' THEN 'Factura Particular' 
				WHEN '4' THEN 'Factura Capita' 
				WHEN '5' THEN 'Control Capitacion' 
				WHEN '6' THEN 'Factura Basica' 
				WHEN '7' THEN 'Factura Venta Productos' 
			END AS [TipoDocumento],
			CASE ing.ICAUSAING 
				WHEN '11' THEN 'Cirugía programada' 
			END AS [CausaIngreso], 
			ing.UFUCODIGO + ' - ' + uf.UFUDESCRI AS [Unidadfuncional], 
            f.InvoiceNumber AS [NroDocumento], 
			f.AdmissionNumber AS Ingreso, 
			ing.IFECHAING AS [FechaIngreso], 
			eh.FECALTPAC AS [Fechaaltamédica], 
			ea.Code + ' - ' + ea.Name AS [EntidadAdministradora], 
			t.Nit, t.DigitVerification AS [Dígitoverificación],  t.Name AS Entidad, 
			f.PatientCode AS Identificación, p.IPNOMCOMP AS NombrePaciente, 
			ga.Code + ' - ' + ga.Name AS [Grupo Atención], 
			f.InvoiceDate AS [Fecha Factura], f.InvoiceExpirationDate AS [Fechavencimiento], 
			f.TotalInvoice AS [VrFactura], 
			f.ThirdPartySalesValue AS [Vr Entidad], 
			f.ThirdPartyDiscountValue AS [Vr.Descuento], 
            CASE f.ResponsibleRecoveryFee 
				WHEN '1' THEN 'Ninguno' 
				WHEN '2' THEN 'Paciente' 
				WHEN '3' THEN 'Tercero' 
			END AS [ResponsableCuotaRecuperacion],
            f.TotalPatientSalesPrice AS [Vrcuotarecuperación], 
			f.PatientDiscount AS [DescuentoCuotarecuperación], 
			f.CashReceiptId AS [ReciboCajaPaciente], 
            f.PatientPaidValue AS [VrpagadoPaciente], 
			f.ThirdPartyAccountReceivableValue AS [VrCxC], 
			f.PatientAccountReceivableValue AS [VrCxCgeneradaaPaciente], 
			CASE f.PatientType 
				WHEN '1' THEN 'Contributivo' 
				WHEN '2' THEN 'Subsidiado' 
				WHEN '3' THEN 'Vinculado' 
				WHEN '4' THEN 'Particular' 
				WHEN '5' THEN 'Otros'
				WHEN '6' THEN 'Desplazado Contributivo' 
				WHEN '7' THEN 'Desplazado Subsidiado' 
				WHEN '8' THEN 'Desplazado no Asegurado' 
			END AS [TipoPaciente], 
			f.Observation AS Observaciones, 
			C.Name AS Categoría, 
			CASE f.Status 
				WHEN '1' THEN 'Facturado' 
				WHEN '2' THEN 'Anulado' 
			END AS Estado_Documento, 
            f.InvoicedUser + ' - ' + per.Fullname AS Usuario, 
			f.InvoicedDate AS Fecha, 
			f.AnnulmentUser + ' - ' + ua.Fullname AS [UsuarioAnula], 
            f.AnnulmentDate AS [Fechaanulación]
	FROM Billing.Invoice AS f WITH (nolock) 
	JOIN Common.OperatingUnit AS uo WITH (nolock) ON uo.Id = f.OperatingUnitId 
	JOIN Common.ThirdParty AS t WITH (nolock) ON t.Id = f.ThirdPartyId
	LEFT JOIN dbo.INPACIENT AS p WITH (nolock) ON p.IPCODPACI = f.PatientCode
	LEFT JOIN dbo.ADINGRESO AS ing WITH (nolock) ON ing.NUMINGRES = f.AdmissionNumber 
	LEFT JOIN Contract.CareGroup AS ga WITH (nolock) ON ga.Id = f.CareGroupId 
	LEFT JOIN Contract.HealthAdministrator AS ea WITH (nolock) ON ea.Id = f.HealthAdministratorId 
	LEFT JOIN Billing.InvoiceCategories AS C WITH (nolock) ON C.Id = f.InvoiceCategoryId 
	LEFT JOIN dbo.INUNIFUNC AS uf WITH (nolock) ON uf.UFUCODIGO = ing.UFUCODIGO 
	LEFT JOIN dbo.HCREGEGRE AS eh WITH (nolock) ON eh.NUMINGRES = f.AdmissionNumber AND eh.IPCODPACI = f.PatientCode
	LEFT JOIN Security.[User] AS u ON u.UserCode = f.InvoicedUser 
	LEFT JOIN Security.Person AS per ON per.Id = u.IdPerson 
	LEFT JOIN Security.[User] AS uc ON uc.UserCode = f.AnnulmentUser 
	LEFT JOIN Security.Person AS ua ON ua.Id = uc.IdPerson
	WHERE uo.UnitCode = ISNULL(@OperatingUnitCode, uo.UnitCode)
		AND CAST(f.InvoiceDate AS DATE) BETWEEN @DateStart AND @DateEnd

UNION

SELECT	uo.UnitName AS Sede, 
            CASE f.DocumentType 
				WHEN '1' THEN 'Factura EAPB con Contrato' 
				WHEN '2' THEN 'Factura EAPB Sin Contrato' 
				WHEN '3' THEN 'Factura Particular' 
				WHEN '4' THEN 'Factura Capita' 
				WHEN '5' THEN 'Control Capitacion' 
				WHEN '6' THEN 'Factura Basica' 
				WHEN '7' THEN 'Factura Venta Productos' 
			END AS [TipoDocumento],
			CASE ing.ICAUSAING 
				WHEN '11' THEN 'Cirugía programada' 
			END AS [CausaIngreso], 
			ing.UFUCODIGO + ' - ' + uf.UFUDESCRI AS [Unidadfuncional], 
            f.InvoiceNumber AS [NroDocumento], 
			f.AdmissionNumber AS Ingreso, 
			ing.IFECHAING AS [FechaIngreso], 
			eh.FECALTPAC AS [Fechaaltamédica], 
			ea.Code + ' - ' + ea.Name AS [EntidadAdministradora], 
			t.Nit, t.DigitVerification AS [Dígitoverificación],  t.Name AS Entidad, 
			f.PatientCode AS Identificación, p.IPNOMCOMP AS NombrePaciente, 
			ga.Code + ' - ' + ga.Name AS [Grupo Atención], 
			f.AnnulmentDate AS [Fecha Factura], f.InvoiceExpirationDate AS [Fechavencimiento], 
			-f.TotalInvoice AS [VrFactura], 
			-f.ThirdPartySalesValue AS [Vr Entidad], 
			-f.ThirdPartyDiscountValue AS [Vr.Descuento], 
            CASE f.ResponsibleRecoveryFee 
				WHEN '1' THEN 'Ninguno' 
				WHEN '2' THEN 'Paciente' 
				WHEN '3' THEN 'Tercero' 
			END AS [ResponsableCuotaRecuperacion],
            -f.TotalPatientSalesPrice AS [Vrcuotarecuperación], 
			-f.PatientDiscount AS [DescuentoCuotarecuperación], 
			-f.CashReceiptId AS [ReciboCajaPaciente], 
            -f.PatientPaidValue AS [VrpagadoPaciente], 
			-f.ThirdPartyAccountReceivableValue AS [VrCxC], 
			-f.PatientAccountReceivableValue AS [VrCxCgeneradaaPaciente], 
			CASE f.PatientType 
				WHEN '1' THEN 'Contributivo' 
				WHEN '2' THEN 'Subsidiado' 
				WHEN '3' THEN 'Vinculado' 
				WHEN '4' THEN 'Particular' 
				WHEN '5' THEN 'Otros'
				WHEN '6' THEN 'Desplazado Contributivo' 
				WHEN '7' THEN 'Desplazado Subsidiado' 
				WHEN '8' THEN 'Desplazado no Asegurado' 
			END AS [TipoPaciente], 
			f.Observation AS Observaciones, 
			C.Name AS Categoría, 
			CASE f.Status 
				WHEN '1' THEN 'Facturado' 
				WHEN '2' THEN 'Anulado' 
			END AS Estado_Documento, 
            f.InvoicedUser + ' - ' + per.Fullname AS Usuario, 
			f.InvoicedDate AS Fecha, 
			f.AnnulmentUser + ' - ' + ua.Fullname AS [UsuarioAnula], 
            f.AnnulmentDate AS [Fechaanulación]
	FROM Billing.Invoice AS f WITH (nolock) 
	JOIN Common.OperatingUnit AS uo WITH (nolock) ON uo.Id = f.OperatingUnitId 
	JOIN Common.ThirdParty AS t WITH (nolock) ON t.Id = f.ThirdPartyId
	LEFT JOIN dbo.INPACIENT AS p WITH (nolock) ON p.IPCODPACI = f.PatientCode
	LEFT JOIN dbo.ADINGRESO AS ing WITH (nolock) ON ing.NUMINGRES = f.AdmissionNumber 
	LEFT JOIN Contract.CareGroup AS ga WITH (nolock) ON ga.Id = f.CareGroupId 
	LEFT JOIN Contract.HealthAdministrator AS ea WITH (nolock) ON ea.Id = f.HealthAdministratorId 
	LEFT JOIN Billing.InvoiceCategories AS C WITH (nolock) ON C.Id = f.InvoiceCategoryId 
	LEFT JOIN dbo.INUNIFUNC AS uf WITH (nolock) ON uf.UFUCODIGO = ing.UFUCODIGO 
	LEFT JOIN dbo.HCREGEGRE AS eh WITH (nolock) ON eh.NUMINGRES = f.AdmissionNumber AND eh.IPCODPACI = f.PatientCode
	LEFT JOIN Security.[User] AS u ON u.UserCode = f.InvoicedUser 
	LEFT JOIN Security.Person AS per ON per.Id = u.IdPerson 
	LEFT JOIN Security.[User] AS uc ON uc.UserCode = f.AnnulmentUser 
	LEFT JOIN Security.Person AS ua ON ua.Id = uc.IdPerson
	WHERE uo.UnitCode = ISNULL(@OperatingUnitCode, uo.UnitCode)
		AND CAST(f.AnnulmentDate AS DATE) BETWEEN @DateStart AND @DateEnd AND f.Status ='2'
END