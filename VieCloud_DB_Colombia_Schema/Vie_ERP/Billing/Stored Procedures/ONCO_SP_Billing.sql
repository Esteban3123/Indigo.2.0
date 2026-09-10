
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de facturación oncológica que genera un reporte detallado de facturas emitidas en un rango de fechas, filtrado opcionalmente por sede (unidad operativa). Consolida en un solo resultado los documentos facturados y sus contrapartidas de anulación (valores negativos), combinando datos de la factura con información del paciente, el ingreso hospitalario, la unidad funcional, la entidad administradora de salud (EPS/ARS), el grupo de atención contractual, la categoría de factura y los usuarios que facturaron o anularon. Se usa para cuadres de cartera, auditoría de facturación y reportes gerenciales de ingresos por sede, tipo de documento, tipo de paciente y régimen (contributivo, subsidiado, particular, entre otros).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'ONCO_SP_Billing';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'ONCO_SP_Billing';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte de facturación oncológica por sede y rango de fechas, mostrando facturas emitidas y, en negativo, las anuladas dentro del periodo, con datos del paciente, ingreso, entidad y usuarios responsables.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'ONCO_SP_Billing';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@DateStart y @DateEnd deben estar definidos para acotar el rango de InvoiceDate y AnnulmentDate; Si @OperatingUnitCode llega vacío o NULL se normaliza a NULL para no filtrar por sede', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'ONCO_SP_Billing';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Una factura anulada dentro del rango aparece dos veces si también fue emitida en el mismo rango: una en positivo (por InvoiceDate) y otra en negativo (por AnnulmentDate), produciendo el efecto neto de reverso; Los valores monetarios de la rama anulada siempre se devuelven con signo invertido (-TotalInvoice, -ThirdPartySalesValue, -PatientPaidValue, etc.); El filtro de sede es opcional: si no se envía código se reportan todas las sedes; Solo se traduce ICAUSAING=''11''; cualquier otra causa de ingreso se muestra como NULL en CausaIngreso; Las consultas usan WITH(NOLOCK), por lo que pueden leer datos no confirmados', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'ONCO_SP_Billing';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Facturación oncológica; EAPB / EPS (entidad administradora de salud); Contributivo/Subsidiado/Vinculado/Particular (tipo de paciente); Cuota de recuperación; Cuentas por cobrar a tercero y a paciente; Anulación de factura; Capitación; Causa de ingreso (cirugía programada); Unidad funcional; Grupo de atención; Egreso hospitalario / fecha de alta médica; Sede / Unidad operativa; Recibo de caja del paciente', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'ONCO_SP_Billing';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.Invoice: Devuelve facturas con CAST(InvoiceDate AS DATE) BETWEEN @DateStart AND @DateEnd con valores monetarios en positivo (movimiento de facturación); [RETURN_RESULT] Billing.Invoice: Devuelve, unidas por UNION, facturas anuladas (Status=''2'') con CAST(AnnulmentDate AS DATE) BETWEEN @DateStart AND @DateEnd, invirtiendo el signo de los valores monetarios y usando AnnulmentDate como [Fecha Factura]', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'ONCO_SP_Billing';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ISNULL(@OperatingUnitCode,'''') = '''' → Se asigna NULL y el WHERE uo.UnitCode = ISNULL(@OperatingUnitCode, uo.UnitCode) deja pasar todas las sedes else Filtra solo la sede cuyo UnitCode coincide con @OperatingUnitCode; si Segundo SELECT del UNION con f.Status = ''2'' → Solo incluye facturas anuladas dentro del rango y reporta sus valores monetarios negados (reverso contable); si DocumentType IN (''1''..''7'') → Traduce el código a etiqueta: 1=Factura EAPB con Contrato, 2=Factura EAPB Sin Contrato, 3=Factura Particular, 4=Factura Capita, 5=Control Capitacion, 6=Factura Basica, 7=Factura Venta Productos; si PatientType IN (''1''..''8'') → Mapea régimen del paciente: Contributivo, Subsidiado, Vinculado, Particular, Otros, Desplazado Contributivo, Desplazado Subsidiado, Desplazado no Asegurado; si ResponsibleRecoveryFee IN (''1'',''2'',''3'') → Traduce responsable de cuota de recuperación: Ninguno, Paciente o Tercero; si Status IN (''1'',''2'') → Etiqueta documento como ''Facturado'' o ''Anulado''; si ICAUSAING = ''11'' → Etiqueta causa de ingreso como ''Cirugía programada'' (otros códigos quedan en NULL)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'ONCO_SP_Billing';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Common.OperatingUnit; Common.ThirdParty; dbo.INPACIENT; dbo.ADINGRESO; Contract.CareGroup; Contract.HealthAdministrator; Billing.InvoiceCategories; dbo.INUNIFUNC; dbo.HCREGEGRE; Security.User; Security.Person', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'ONCO_SP_Billing';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'ONCO_SP_Billing';
-- GO
