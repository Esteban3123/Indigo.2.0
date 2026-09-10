CREATE VIEW [GeneralLedger].[ViewReportExogenousFormat]
AS
	SELECT	ISNULL(jvd.ExogenousFormatId, glb.ExogenousFormatId) ExogenousFormatId,
			ISNULL(jvd.MinimumValue, glb.MinimumValue) MinimumValue,
			ISNULL(jvd.Year, glb.Year) Year,
			ISNULL(jvd.IdThirdParty, glb.IdThirdParty) ThirdPartyId,
			ISNULL(jvd.Format, glb.Format) Format,
			ISNULL(jvd.Concept, glb.Concept) Concept,
			ISNULL(jvd.ConceptType, glb.ConceptType) ConceptType,
			ABS(ISNULL(jvd.Value, 0) + ISNULL(glb.Value, 0)) Value
	FROM
	(
		SELECT	ef.Id ExogenousFormatId,
				ef.[Format],
				ef.MinimumValue,
				efd.Concept, 
				efd.ConceptType,
				YEAR(jv.VoucherDate) Year,
				IIF(efd.ThirdPartyBy = 1, jvd.IdThirdParty, ISNULL(
					CASE jv.EntityName
						WHEN 'AccountPayable' THEN ap.IdThirdParty 
						WHEN 'PaymentNotes' THEN pn.IdThirdParty
					END
				, jvd.IdThirdParty)) IdThirdParty,
				SUM
				(
					IIF
					(
						NOT 
						(
							Format = 1001 AND efd.ConceptType = 5
							AND
							(
								efd.Concept = 5007 AND ISNULL(ISNULL(pn.EntityName, ap.EntityName), jv.EntityName) NOT IN ('EntranceVoucher', 'EntranceVoucherDevolution')
								OR
								efd.Concept = 5008 AND ISNULL(ISNULL(pn.EntityName, ap.EntityName), jv.EntityName) NOT IN ('FixedAssetEntry', 'FixedAssetEntryDevolution', 'FixedAssetTransaction')
								--OR
								--efd.Concept = 5016 AND ISNULL(ISNULL(pn.EntityName, ap.EntityName), jv.EntityName) IN ('FixedAssetEntry', 'FixedAssetEntryDevolution', 'FixedAssetTransaction', 'EntranceVoucher', 'EntranceVoucherDevolution')
							)
						),
						CASE efd.Nature
							WHEN 1 THEN ISNULL(jvd.DebitValue, 0)
							WHEN 2 THEN ISNULL(jvd.CreditValue, 0)
							WHEN 3 THEN ISNULL(jvd.DebitValue - jvd.CreditValue, 0) * IIF(mac.Nature = 1, 1, -1)
							WHEN 4 THEN ISNULL(jvd.DebitValue - jvd.CreditValue, 0) * IIF(mac.Nature = 1, 1, -1)
							WHEN 5 THEN ISNULL(jvd.BaseValue, 0)
							WHEN 6 THEN ISNULL(jvd.BillingValue, 0)
							ELSE 0
						END
					, 0)
				) Value
		FROM GeneralLedger.ExogenousFormat ef WITH (NOLOCK)
		JOIN GeneralLedger.ExogenousFormatDetail efd WITH (NOLOCK) ON ef.Id = efd.ExogenousFormatId
		JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON efd.MainAccountId = ma.Id
		JOIN GeneralLedger.MainAccountClasses mac WITH (NOLOCK) ON ma.IdAccountClass = mac.Id
		JOIN GeneralLedger.JournalVoucherDetails jvd WITH (NOLOCK) ON ma.Id = jvd.IdMainAccount
		JOIN GeneralLedger.JournalVouchers jv WITH (NOLOCK) ON jvd.IdAccounting = jv.Id			
		------------------------ PAYMENTS ------------------------
		LEFT JOIN Payments.AccountPayable ap WITH (NOLOCK) ON jv.EntityName = 'AccountPayable' AND jv.EntityId = ap.Id
		LEFT JOIN 
		(
			SELECT pn.Id, s.IdThirdParty, pn.EntityName
			FROM Payments.PaymentNotes pn WITH (NOLOCK) 
			JOIN Common.Supplier s WITH (NOLOCK) ON pn.IdSupplier = s.Id
		) pn ON jv.EntityName = 'PaymentNotes' AND jv.EntityId = pn.Id
		----------------------------------------------------------
		WHERE jv.Status = 2 
			AND jv.IsClosedYear = 0 
		GROUP BY ef.Id, ef.Format, ef.MinimumValue, 
			efd.Concept, efd.ConceptType,
			YEAR(jv.VoucherDate), IIF(efd.ThirdPartyBy = 1, jvd.IdThirdParty, ISNULL(
				CASE jv.EntityName
					WHEN 'AccountPayable' THEN ap.IdThirdParty 
					WHEN 'PaymentNotes' THEN pn.IdThirdParty
				END
			, jvd.IdThirdParty))
	) jvd
	FULL JOIN
	(
		SELECT	ef.Id ExogenousFormatId,
				ef.[Format],
				ef.MinimumValue,
				efd.Concept, 
				efd.ConceptType,
				glb.Year + 1 Year,
				glb.IdThirdParty, 
				SUM
				(
					ISNULL(glb.DebitValue - glb.CreditValue, 0) * IIF(mac.Nature = 1, 1, -1)
				) Value
		FROM GeneralLedger.MainAccountClasses mac WITH (NOLOCK)
		JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON mac.Id = ma.IdAccountClass
		JOIN GeneralLedger.GeneralLedgerBalance glb WITH (NOLOCK) ON ma.Id = glb.IdMainAccount
		JOIN GeneralLedger.ExogenousFormatDetail efd WITH (NOLOCK) ON glb.IdMainAccount = efd.MainAccountId
		JOIN GeneralLedger.ExogenousFormat ef WITH (NOLOCK) ON efd.ExogenousFormatId = ef.Id
		WHERE glb.Month = 14 AND efd.Nature = 4
		GROUP BY ef.Id, ef.Format, ef.MinimumValue, 
			efd.Concept, efd.ConceptType,
			glb.Year, glb.IdThirdParty
	) glb ON jvd.ExogenousFormatId = glb.ExogenousFormatId 
		AND ISNULL(jvd.Concept, 0) = ISNULL(glb.Concept, 0)
		AND ISNULL(jvd.ConceptType, 0) = ISNULL(glb.ConceptType, 0)
		AND ISNULL(jvd.Year, 0) = ISNULL(glb.Year, 0)
		AND ISNULL(jvd.IdThirdParty, 0) = ISNULL(glb.IdThirdParty, 0)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista para el reporte de información exógena tributaria (formatos de la DIAN). Consolida los movimientos contables del año en curso —tomados de comprobantes de diario, cuentas por pagar y notas de pago a proveedores— junto con los saldos acumulados del cierre del año anterior, agrupando los valores por formato exógeno, concepto, tipo de concepto, año y tercero. Combina los detalles de configuración de formatos exógenos (ExogenousFormat / ExogenousFormatDetail) con el libro de comprobantes contables (JournalVouchers / JournalVoucherDetails) y el balance general acumulado (GeneralLedgerBalance), incluyendo la identificación del tercero ya sea desde el movimiento contable directo, la cuenta por pagar (AccountPayable) o la nota de pago al proveedor (PaymentNotes/Supplier). Sirve de base para generar los reportes de información exógena que las organizaciones deben presentar ante la DIAN, permitiendo calcular los valores débito, crédito, neto o base imponible por cuenta contable, concepto tributario y tercero identificado.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'VIEW', @level1name = N'ViewReportExogenousFormat';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'VIEW', @level1name = N'ViewReportExogenousFormat';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida los movimientos contables del año en curso y los saldos iniciales del año anterior para reportar valores agrupados por concepto de formato exógeno (información tributaria DIAN), tercero y año.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'ViewReportExogenousFormat';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los comprobantes contables deben existir en estado contabilizado (Status=2) y no pertenecer a un año cerrado (IsClosedYear=0) para ser incluidos en los movimientos del periodo.; Para los saldos iniciales se requiere que existan registros de balance con Month=14 (cierre de año) y que el detalle del formato exógeno tenga Nature=4.; Las cuentas principales referenciadas deben tener una clase de cuenta (MainAccountClasses) con naturaleza definida para determinar el signo del valor.; Los comprobantes con EntityName=''AccountPayable'' deben tener correspondencia en Payments.AccountPayable; los de EntityName=''PaymentNotes'' deben tener proveedor asociado en Common.Supplier.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'ViewReportExogenousFormat';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los comprobantes no contabilizados (Status≠2) o pertenecientes a años cerrados (IsClosedYear=1) nunca aparecen en el reporte de movimientos del periodo.; Los saldos iniciales solo se obtienen del periodo de cierre anual (Month=14) y se reportan corridos un año hacia adelante (Year+1).; El signo del valor reportado siempre se neutraliza con ABS, garantizando montos no negativos.; En conceptos del Format 1001 con ConceptType=5, los conceptos 5007 y 5008 sólo acumulan valores cuando la entidad origen del comprobante corresponde a entradas/devoluciones de mercancía o activo fijo respectivamente.; La naturaleza (débito/crédito) de la clase de cuenta determina el signo cuando Nature está definida como 3 o 4 en el detalle del formato exógeno.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'ViewReportExogenousFormat';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Formato exógeno (información tributaria DIAN); Comprobante contable / asiento de diario; Plan de cuentas (PUC); Naturaleza débito/crédito de cuentas; Tercero; Cuentas por pagar; Notas de pago; Proveedor; Saldo de libro mayor / cierre anual; Entradas y devoluciones de mercancía; Activos fijos (entrada, devolución, transacción); Valor base / valor de facturación', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'ViewReportExogenousFormat';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve filas combinando movimientos del año (jvd) y saldos del año anterior (glb) mediante FULL JOIN por ExogenousFormatId, Concept, ConceptType, Year y ThirdPartyId; el Value retornado es ABS(SUM(jvd) + SUM(glb)).', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'ViewReportExogenousFormat';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si efd.ThirdPartyBy = 1 → Se usa el tercero del detalle del comprobante (jvd.IdThirdParty) como tercero del reporte. else Se usa el tercero de la cuenta por pagar (ap.IdThirdParty) si EntityName=''AccountPayable'', o el del proveedor de la nota de pago (pn.IdThirdParty) si EntityName=''PaymentNotes''; en su defecto, el tercero del detalle del comprobante.; si Format=1001 AND efd.ConceptType=5 AND efd.Concept=5007 AND EntityName NOT IN (''EntranceVoucher'',''EntranceVoucherDevolution'') → El movimiento se excluye del cálculo (Value = 0) por no corresponder a entrada o devolución de mercancía.; si Format=1001 AND efd.ConceptType=5 AND efd.Concept=5008 AND EntityName NOT IN (''FixedAssetEntry'',''FixedAssetEntryDevolution'',''FixedAssetTransaction'') → El movimiento se excluye del cálculo (Value = 0) por no corresponder a operaciones de activo fijo.; si efd.Nature = 1 → El valor del movimiento toma el DebitValue del detalle del comprobante. else Otros valores de Nature aplican: 2=CreditValue; 3 o 4=(Debit-Credit) ajustado por signo de la naturaleza de la clase de cuenta (mac.Nature=1 →+1, sino -1); 5=BaseValue; 6=BillingValue; cualquier otro valor → 0.; si glb.Month = 14 AND efd.Nature = 4 → Los saldos del balance general se incluyen como movimientos del año siguiente (Year+1), calculados como (Debit-Credit) ajustado por el signo de la naturaleza de la clase de cuenta.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'ViewReportExogenousFormat';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.ExogenousFormat; GeneralLedger.ExogenousFormatDetail; GeneralLedger.MainAccounts; GeneralLedger.MainAccountClasses; GeneralLedger.JournalVoucherDetails; GeneralLedger.JournalVouchers; Payments.AccountPayable; Payments.PaymentNotes; Common.Supplier; GeneralLedger.GeneralLedgerBalance', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'ViewReportExogenousFormat';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'ViewReportExogenousFormat';
GO
