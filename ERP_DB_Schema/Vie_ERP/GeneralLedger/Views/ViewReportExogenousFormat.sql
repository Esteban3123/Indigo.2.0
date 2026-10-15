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