

CREATE VIEW [Portfolio].[ViewListPortfolioTransferDetail]
AS

	select 
			ptd.Id as PortfolioTransferDetailId, 
			pt.Id as PortfolioTransferId, 
			ar.Id as AccountReceivableId, 
			ma.Id as MainAccountId, 
			ptd.Value, 
			ara.Value as ValueBill, 
			ara.Balance,
			ar.InvoiceNumber, 
			CONCAT(ma.Number,' - ',ma.Name) as CodeNameMainAccount,
			ptd.CostCenterId,
			CASE ma.Id
				WHEN  ar.AccountWithoutRadicateId THEN '1-Sin Radicar'
				WHEN ar.AccountRadicateId THEN '3-Radicada Entidad'
				WHEN ar.AccountObjectionRemediedId THEN '4-Glosada sin Conciliar'
				WHEN ar.AccountConciliationId THEN '12-Glosada Conciliada'
				WHEN ar.AccountHardCollectionId THEN '15-Cuenta de Dificil Recaudo'
				WHEN ar.AccountLegalCollectionId THEN '16-Cobro Juridico'
				ELSE 'N/A'
			END PortfolioStatusName,
			c.Id CurrencyInvoiceId,
			c.Abbreviation CurrencyAbbreviation,
			CONCAT(trim(i.PatientCode),' - ',pa.IPNOMCOMP) Patient
	from	[Portfolio].PortfolioTransferDetail ptd with (nolock)
			inner join [Portfolio].PortfolioTransfer pt with (nolock) on pt.Id = ptd.PortfolioTrasferId
			inner join [Portfolio].AccountReceivable ar  with (nolock) on ar.Id = ptd.AccountReceivableId
			left join Billing.Invoice i  with (nolock) on ar.InvoiceId = i.Id
			left join INPACIENT pa with (nolock) on i.PatientCode=pa.IPCODPACI
			inner join [GeneralLedger].MainAccounts ma with (nolock) on ma.Id = ptd.MainAccountId
			inner join GeneralLedger.LegalBook lb on lb.Id = ma.LegalBookId and lb.OfficialBook = 1
			inner join [Portfolio].AccountReceivableAccounting ara with (nolock) on ara.AccountReceivableId = ar.Id and ara.MainAccountId = ma.Id
			JOIN Common.Currency c WITH(NOLOCK) on isnull(ar.CurrencyId,(SELECT top 1 OfficialCurrencyId from GeneralLedger.CompanySettings))=c.Id

			UNION ALL

			select 
					ptd.Id as PortfolioTransferDetailId, 
					pt.Id as PortfolioTransferId, 
					ar.Id as AccountReceivableId, 
					ma.Id as MainAccountId, 
					ptd.Value, 
					iar.Value as ValueBill, 
					iar.Balance,
					ar.InvoiceNumber, 
					CONCAT(ma.Number,' - ',ma.Name) as CodeNameMainAccount,
					ptd.CostCenterId,
					CASE ma.Id
						WHEN  ar.AccountWithoutRadicateId THEN '1-Sin Radicar'
						WHEN ar.AccountRadicateId THEN '3-Radicada Entidad'
						WHEN ar.AccountObjectionRemediedId THEN '4-Glosada sin Conciliar'
						WHEN ar.AccountConciliationId THEN '12-Glosada Conciliada'
						WHEN ar.AccountHardCollectionId THEN '15-Cuenta de Dificil Recaudo'
						WHEN ar.AccountLegalCollectionId THEN '16-Cobro Juridico'
						ELSE 'N/A'
					END PortfolioStatusName,
					c.Id CurrencyInvoiceId,
					c.Abbreviation CurrencyAbbreviation,
					CONCAT(trim(i.PatientCode),' - ',pa.IPNOMCOMP) Patient
			from [Portfolio].PortfolioTransferDetail ptd with (nolock)
			join [Portfolio].PortfolioTransfer pt with (nolock) on pt.Id = ptd.PortfolioTrasferId
			join [Portfolio].AccountReceivable ar  with (nolock) on ar.Id = ptd.AccountReceivableId
			left JOIN Billing.Invoice i  with (nolock) on ar.InvoiceId = i.Id
			left join INPACIENT pa with (nolock) on i.PatientCode=pa.IPCODPACI
			join [GeneralLedger].MainAccounts ma with (nolock) on ma.Id = ptd.MainAccountId
			join GeneralLedger.LegalBook lb on lb.Id = ma.LegalBookId and lb.OfficialBook = 1
			left join [Portfolio].AccountReceivableAccounting ara with (nolock) on ara.AccountReceivableId = ar.Id and ara.MainAccountId = ma.Id
			JOIN Portfolio.PortfolioInitialBalanceAccountReceivable iar on iar.InvoiceNumber  = ar.InvoiceNumber
			join Portfolio.PortfolioInitialBalanceAccountReceivableAccounting iara on iar.Id = iara.PortfolioInitialBalanceAccountReceivableId
			JOIN Common.Currency c WITH(NOLOCK) on isnull(ar.CurrencyId,(SELECT top 1 OfficialCurrencyId from GeneralLedger.CompanySettings))=c.Id
			where ara.id is null
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que muestra el detalle completo de las transferencias de cartera, combinando tanto las cuentas por cobrar con registro contable activo como las que provienen de saldos iniciales (balance de apertura). Para cada línea de transferencia expone el identificador del traslado, la cuenta por cobrar involucrada, el número de factura, el valor transferido, el valor original de la factura, el saldo pendiente, la cuenta contable PUC (código y nombre), el centro de costos, el estado de la cartera (sin radicar, radicada, glosada, difícil recaudo, cobro jurídico, entre otros), la moneda de la factura y el nombre completo del paciente. Sirve como fuente de reportería para auditar y controlar los movimientos de traspaso de saldos entre terceros o unidades operativas, permitiendo rastrear cada cuenta por cobrar dentro de un proceso de transferencia de cartera.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'ViewListPortfolioTransferDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'ViewListPortfolioTransferDetail';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista el detalle de transferencias de cartera enriquecido con datos de la cuenta por cobrar, factura, paciente, cuenta contable, moneda y estado del portafolio, combinando registros con contabilización vigente y aquellos provenientes del saldo inicial cuando no existe contabilización en AccountReceivableAccounting.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewListPortfolioTransferDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La cuenta contable (MainAccount) debe pertenecer a un LegalBook marcado como OfficialBook = 1.; Para el primer bloque debe existir un registro en Portfolio.AccountReceivableAccounting con la misma AccountReceivableId y MainAccountId.; Para el segundo bloque (UNION ALL) la factura debe tener registro en Portfolio.PortfolioInitialBalanceAccountReceivable por InvoiceNumber y NO existir contabilización en Portfolio.AccountReceivableAccounting (ara.id is null).; Si la cuenta por cobrar no tiene CurrencyId, se asume la OfficialCurrencyId definida en GeneralLedger.CompanySettings.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewListPortfolioTransferDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen movimientos cuya cuenta contable principal pertenezca a un libro contable oficial (LegalBook.OfficialBook = 1).; El estado de la cartera (PortfolioStatusName) se deriva exclusivamente de comparar el MainAccountId del detalle contra las cuentas de estado configuradas en la AccountReceivable.; Cada fila siempre tiene una moneda asociada: la de la AR o, en su defecto, la oficial de la compañía.; El campo Patient concatena el código del paciente de la factura con su nombre completo (IPNOMCOMP) cuando existen factura y paciente; de lo contrario queda con separador y nulos.; Las dos ramas del UNION ALL son mutuamente excluyentes por el filtro ''ara.id is null'' en la segunda.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewListPortfolioTransferDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Transferencia de cartera; Cuenta por cobrar; Factura; Paciente; Cuenta contable (PUC); Libro contable oficial; Centro de costos; Moneda / divisa; Saldo inicial de cartera; Estado de cartera (Sin Radicar, Radicada, Glosada, Conciliada, Difícil Recaudo, Cobro Jurídico); Glosa; Cobro jurídico', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewListPortfolioTransferDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Portfolio.ViewListPortfolioTransferDetail: Devuelve una fila por cada PortfolioTransferDetail combinado con su contabilización vigente o, alternativamente, con su saldo inicial de cartera cuando no existe AccountReceivableAccounting.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewListPortfolioTransferDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ma.Id = ar.AccountWithoutRadicateId → PortfolioStatusName = ''1-Sin Radicar''; si ma.Id = ar.AccountRadicateId → PortfolioStatusName = ''3-Radicada Entidad''; si ma.Id = ar.AccountObjectionRemediedId → PortfolioStatusName = ''4-Glosada sin Conciliar''; si ma.Id = ar.AccountConciliationId → PortfolioStatusName = ''12-Glosada Conciliada''; si ma.Id = ar.AccountHardCollectionId → PortfolioStatusName = ''15-Cuenta de Dificil Recaudo''; si ma.Id = ar.AccountLegalCollectionId → PortfolioStatusName = ''16-Cobro Juridico''; si ma.Id no coincide con ninguna cuenta de estado de cartera de la AR → PortfolioStatusName = ''N/A''; si ar.CurrencyId IS NULL → Se usa la moneda oficial de GeneralLedger.CompanySettings (TOP 1 OfficialCurrencyId) else Se usa ar.CurrencyId; si Existe contabilización en Portfolio.AccountReceivableAccounting para la AR y MainAccount → Se toman Value y Balance desde ara (primer SELECT) else Se toman Value y Balance desde Portfolio.PortfolioInitialBalanceAccountReceivable (segundo SELECT del UNION ALL, filtrando ara.id is null)', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewListPortfolioTransferDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.PortfolioTransferDetail; Portfolio.PortfolioTransfer; Portfolio.AccountReceivable; Billing.Invoice; INPACIENT; GeneralLedger.MainAccounts; GeneralLedger.LegalBook; Portfolio.AccountReceivableAccounting; Common.Currency; GeneralLedger.CompanySettings; Portfolio.PortfolioInitialBalanceAccountReceivable; Portfolio.PortfolioInitialBalanceAccountReceivableAccounting', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewListPortfolioTransferDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewListPortfolioTransferDetail';
GO
