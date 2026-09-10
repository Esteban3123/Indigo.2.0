CREATE PROCEDURE [GeneralLedger].[SP_ReportStatementSaleIVA]
	-- Add the parameters for the stored procedure here
	@xmlCriterias as xml,
	@xmlFilters as xml
AS
BEGIN

	Declare @DateStart DATETIME,
			@DateEnd DATETIME,
			@LegalBookId INT,
			@ReportCurrency int,

			@GeneralLedgerIVA VARCHAR(max),
			@ThirdPartyStart VARCHAR(50),
			@ThirdPartyEnd VARCHAR(50),
			@OfficialCurrencyId integer

	BEGIN TRY
		
		set DATEFORMAT YMD

		--Se obtienen los criterios
		SELECT	@DateStart = t.x.value('DateStart[1]','datetime'),
				@DateEnd = t.x.value('DateEnd[1]','datetime'),
				@LegalBookId = t.x.value('Book[1]','int'),
				@ReportCurrency = t.x.value('ReportCurrency[1]','int')
		FROM @xmlCriterias.nodes('/Data') t(x)

		--Se obtienen los filtros
		SELECT	@GeneralLedgerIVA = t.x.value('GeneralLedgerIVA[1]','VARCHAR(max)'),
				@ThirdPartyStart = t.x.value('ThirdPartyStart[1]','VARCHAR(50)'),
				@ThirdPartyEnd = t.x.value('ThirdPartyEnd[1]','VARCHAR(50)')
		FROM @xmlFilters.nodes('/Data') t(x)

		set @OfficialCurrencyId = (select OfficialCurrencyId from GeneralLedger.CompanySettings)
		declare @CurrencyName varchar(100) = (select iso.CurrencyName from Common.Currency c inner join Common.ISO4217 iso on c.ISO4217Id = iso.Id where c.Id = @ReportCurrency)
		
		DROP TABLE IF EXISTS #data
		create table #data (
			DocumentType tinyint, --1 Invoice, 2 - Notas CxC
			DocumentId int,
			DocumentDate DATETIME,
			DocumentCode varchar(50),
			JournalVoucher varchar(100),
			Nature varchar(100),
			ConsecutiveVoucher varchar(100),
			DocumentAffected nvarchar(MAX),
			ThirdPartyDNI varchar(20),
			ThirdPartyName varchar(100),
			Detail varchar(MAX),
			ContributionType varchar(200),
			DocumentState varchar(50),
			Currency varchar(30),
			EconomicActivity varchar(max),
			Subtotal numeric(20,2),
			DiscountValue numeric(20,2),
			NetValue numeric(20,2),
			TotalIVAValue numeric(20,2),
			Total numeric(20,2)
		)

		DROP TABLE IF EXISTS #taxesBase
		create table #taxesBase (
			TaxDocumentType tinyint, -- 1 CxP, 2 Notas
			TaxDocumentId varchar(50),			
			BaseTaxName varchar(100),
			BaseValue numeric(20, 2),
		)

		DROP TABLE IF EXISTS #taxes
		create table #taxes (
			TaxDocumentType tinyint, -- 1 CxP, 2 Notas
			TaxDocumentId varchar(50),
			TaxName varchar(100),
			TaxValue numeric(20, 2)
		)

		declare @basesTaxes table (
		NameBase varchar(100),
		NameTax varchar(100)
		);

		-- CTE que se utilizarán para la consulta de las Notas CxC
		WITH Consecutivos AS (
			SELECT 
				pn.Id,
				CONCAT(jv.Consecutive, ' - ', jv.EntityName, ' - ', jv.EntityCode) AS JournalVoucherName,
				STRING_AGG(jv.Consecutive, ', ') WITHIN GROUP (ORDER BY jv.Consecutive) AS ConsecutivoComprobante
			FROM Portfolio.PortfolioNote pn
			JOIN GeneralLedger.JournalVouchers jv ON pn.Id = jv.EntityId 
			WHERE pn.NoteDate BETWEEN @DateStart AND @DateEnd
			  AND jv.EntityCode = pn.Code AND jv.EntityName = 'PortfolioNote'
			GROUP BY pn.Id, jv.Consecutive, jv.EntityName, jv.EntityCode
		), Invoice As(
			SELECT 
				pn.Id,
				ar.InvoiceNumber
			FROM Portfolio.PortfolioNoteAccountReceivableAdvance pnara
			JOIN Portfolio.PortfolioNote pn ON pnara.PortfolioNoteId = pn.Id
			JOIN Portfolio.AccountReceivable ar ON ar.Id = pnara.AccountReceivableId
			WHERE pn.NoteDate BETWEEN @DateStart AND @DateEnd 
			  AND pn.NoteType IN (1, 6) 
			  AND pn.Status = 2 -- Notas Confirmadas Tipo: Factura Total y Factura Detallada 
			GROUP BY 
				pn.Id, ar.InvoiceNumber
		), EconomicActivitesInvoice_Distinct AS (
		SELECT DISTINCT
			i.Id AS InvoiceId,
			CONCAT(ea.Code,' - ',ea.Name) AS EA
		FROM Billing.Invoice i
		JOIN Billing.InvoiceDetail id              ON id.InvoiceId = i.Id
		LEFT JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON sod.Id = id.ServiceOrderDetailId
		LEFT JOIN Common.EconomicActivity ea ON ea.Id = sod.EconomicActivityId
		WHERE i.InvoiceDate BETWEEN @DateStart AND @DateEnd AND ea.Id IS NOT NULL

		UNION ALL

		SELECT DISTINCT
			i.Id AS InvoiceId,
			CONCAT(ea.Code,' - ',ea.Name) AS EA
		FROM Billing.Invoice i
		LEFT JOIN Billing.BasicBilling bb ON bb.InvoiceId = i.Id
		LEFT JOIN Billing.BasicBillingDetail bbd ON bbd.BasicBillingId = bb.Id
		LEFT JOIN Common.EconomicActivity ea ON ea.Id = bbd.EconomicActivityId
		WHERE i.InvoiceDate BETWEEN @DateStart AND @DateEnd AND ea.Id IS NOT NULL
	), EconomicActivitesInvoice_Agg AS (
		SELECT InvoiceId, STRING_AGG(EA, ',') AS EconomicActivityCodeName
		FROM EconomicActivitesInvoice_Distinct
		GROUP BY InvoiceId

	), EconomicActivitesNotes_Agg AS (
		SELECT
			ar.InvoiceId,
			eai.EconomicActivityCodeName,
			pnara.PortfolioNoteId
		FROM Portfolio.PortfolioNoteAccountReceivableAdvance pnara
		JOIN Portfolio.AccountReceivable ar ON ar.Id = pnara.AccountReceivableId
		JOIN Billing.Invoice i ON i.Id = ar.InvoiceId
		JOIN EconomicActivitesInvoice_Agg eai ON eai.InvoiceId = i.Id
		WHERE i.InvoiceDate BETWEEN @DateStart AND @DateEnd
	)

		/******************************************************/
		-- INICIO DEL PROCESO DE OBTENCIÓN E INSERCIÓN DE DATOS
		insert into #data
		select 
			1,
			i.Id,
			i.InvoiceDate,
			i.InvoiceNumber,
			jv.Name, 
			Null As Nature,
			jv.Consecutive, Null,
			t.Nit,
			t.Name,
			i.Observation,
			(select STRING_AGG(fr.[Name], ',')
				from Common.ThirdPartyFiscalResponsibility tpfr
				inner join Common.FiscalResponsibility fr on tpfr.FiscalResponsibilityId = fr.Id
				where tpfr.ThirdPartyId = t.Id),
			'Activa' as DocumentStatus,
			c.Name as Currency,
			det.EconomicActivityCodeName,
			(NetWorth + GrandTotalDiscount) as Subtotal,
			GrandTotalDiscount as Valordescuento,
			NetWorth as Valorneto,
			GrandTotalTaxes as ValorTotalIVA,
			GrandTotalSalesPrice as Total
		from Billing.Invoice i
		inner join Common.ThirdParty t on t.Id = i.ThirdPartyId
		inner join Common.Currency c on c.Id = i.CurrencyId
		inner join 
			(
				SELECT
					i.Id AS InvoiceId,
					SUM(CASE WHEN ISNULL(rcd.IsMasterAccount, 0) = 0
							 THEN [Common].[CurrencyConverterByModule](sod.GrossValue * id.InvoicedQuantity,@OfficialCurrencyId,ISNULL(i.CurrencyId, @OfficialCurrencyId),NULL,'Invoice',i.InvoiceDate)
							 ELSE id.NetWorth
						END) AS NetWorth,
					SUM(id.GrandTotalDiscount) AS GrandTotalDiscount,
					SUM(id.GrandTotalTaxes) AS GrandTotalTaxes,
					SUM(id.GrandTotalSalesPrice) AS GrandTotalSalesPrice,
					eaiagg.EconomicActivityCodeName
				FROM Billing.Invoice i
				JOIN Portfolio.AccountReceivable ar ON ar.InvoiceId = i.Id
				JOIN Billing.InvoiceDetail id ON id.InvoiceId = i.Id
				JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON sod.Id = id.ServiceOrderDetailId
				LEFT JOIN Billing.RevenueControlDetail rcd WITH (NOLOCK) ON rcd.Id = i.RevenueControlDetailId
				LEFT JOIN EconomicActivitesInvoice_Agg eaiagg ON eaiagg.InvoiceId = i.Id
				WHERE i.InvoiceDate BETWEEN @DateStart AND @DateEnd
				GROUP BY i.Id, eaiagg.EconomicActivityCodeName

				UNION ALL

				SELECT 
					bb.InvoiceId, 
					SUM(bbd.[Value]) AS NetWorth, 
					SUM(bbd.ValueDiscount) AS GrandTotalDiscount, 
					SUM(round((bbd.[Value] * bbd.PercentageIVA / 100.00),2)) AS GrandTotalTaxes,  
					SUM(bbd.[Value] + round((bbd.[Value] * bbd.PercentageIVA / 100.00),2)) AS GrandTotalSalesPrice,
					eaiagg.EconomicActivityCodeName
				FROM Billing.BasicBillingDetail bbd
					INNER JOIN Billing.BasicBilling bb ON bb.Id = bbd.BasicBillingId
					LEFT JOIN EconomicActivitesInvoice_Agg eaiagg ON eaiagg.InvoiceId = bb.InvoiceId
				WHERE 
					bb.DocumentDate BETWEEN @DateStart AND @DateEnd
				GROUP BY bb.InvoiceId, eaiagg.EconomicActivityCodeName
			) as det on det.InvoiceId = i.Id
		LEFT JOIN 
			(
				SELECT jv.EntityName, jv.EntityId, STRING_AGG(jvt.Name, ',') AS [Name],  STRING_AGG(jv.Consecutive, ',') AS Consecutive
				FROM GeneralLedger.JournalVouchers jv 
					JOIN GeneralLedger.JournalVoucherTypes jvt on jvt.Id = jv.IdJournalVoucher 
					AND jv.LegalBookId = 1 and jv.EntityName = 'Invoice'
				GROUP BY jv.EntityName, jv.EntityId
			) jv ON jv.EntityId = i.Id
		WHERE 
			i.InvoiceDate BETWEEN @DateStart AND @DateEnd -- Filtro las facturas desde un inicio para optimizar la consulta

		UNION ALL -- DATOS DE LAS NOTAS DE CXC
		
		SELECT  -- NOTAS TIPO FACTURA TOTAL
			2 AS DocumentType,
			pn.Id AS DocumentId,
			pn.NoteDate AS DocumentDate,
			pn.Code AS DocumentCode, 
			cn.JournalVoucherName AS JournalVoucher,
			CASE
				WHEN pn.Nature = 1 THEN 'Debito'
				WHEN pn.Nature = 2 THEN 'Crédito'
			END AS Nature,
			cn.ConsecutivoComprobante AS ConsecutiveVoucher,
			STRING_AGG(CAST(i.InvoiceNumber AS NVARCHAR(MAX)), ', ') WITHIN GROUP (ORDER BY i.InvoiceNumber) AS DocumentAffected,
			tp.Nit AS ThirdPartyDNI,
			tp.Name AS ThirdPartyName,
			pn.Observations AS Detail,
			CASE
				WHEN tp.ContributionType = 0 THEN 'No Responsable de Iva' 
				WHEN tp.ContributionType = 1 THEN 'Responsable de Iva'  
				WHEN tp.ContributionType = 2 THEN 'Empresa estatal'
				WHEN tp.ContributionType = 3 THEN 'Gran Contribuyente'
				WHEN tp.ContributionType = 4 THEN 'Regimen Simple'
				WHEN tp.ContributionType = 5 THEN 'Exento'
			END AS ContributionType, 
			'Confirmado' AS DocumentState,
			c.Name AS Currency,
			ean.EconomicActivityCodeName,
			SUM(pnd.Value) AS Subtotal,
			0 AS Valordescuento,
			SUM(pnd.Value) AS Valorneto,
			SUM(pnd.IvaRate) AS ValorTotalIVA,
			SUM((pnd.Value + ISNULL(pnd.IvaRate, 0))) AS Total
		FROM Portfolio.PortfolioNote pn
		JOIN Portfolio.PortfolioNoteDetail pnd ON pn.Id = pnd.PortfolioNoteId
		JOIN Invoice i ON i.Id = pn.Id
		JOIN Consecutivos cn ON pn.Id = cn.Id
		JOIN Common.Customer cus ON cus.Id = pn.CustomerId
		JOIN Common.ThirdParty tp ON tp.Id = cus.ThirdPartyId
		JOIN Common.Currency c ON pn.CurrencyId = c.Id
		LEFT JOIN EconomicActivitesNotes_Agg ean ON ean.PortfolioNoteId = pn.Id
		WHERE pn.NoteDate BETWEEN @DateStart AND @DateEnd 
		  AND pn.NoteType = 1 
		  AND pn.Status = 2 -- Notas Confirmadas 
		GROUP BY
			pn.Id, pn.Code, cn.JournalVoucherName, pn.Nature, pn.NoteDate, 
			cn.ConsecutivoComprobante, tp.Nit, tp.Name, pn.Observations, 
			tp.ContributionType, c.Name, ean.EconomicActivityCodeName

		UNION ALL
		
		SELECT  -- NOTAS TIPO FACTURA DETALLADA
			2 AS DocumentType,
			pn.Id AS DocumentId,
			pn.NoteDate AS DocumentDate,
			pn.Code AS DocumentCode, 
			cn.JournalVoucherName AS JournalVoucher,
			CASE
				WHEN pn.Nature = 1 THEN 'Debito'
				WHEN pn.Nature = 2 THEN 'Crédito'
			END AS Nature,
			cn.ConsecutivoComprobante AS ConsecutiveVoucher,
			STRING_AGG(CAST(i.InvoiceNumber AS NVARCHAR(MAX)), ', ') WITHIN GROUP (ORDER BY i.InvoiceNumber) AS DocumentAffected,
			tp.Nit AS ThirdPartyDNI,
			tp.Name AS ThirdPartyName,
			pn.Observations AS Detail,
			CASE
				WHEN tp.ContributionType = 0 THEN 'No Responsable de Iva' 
				WHEN tp.ContributionType = 1 THEN 'Responsable de Iva'  
				WHEN tp.ContributionType = 2 THEN 'Empresa estatal'
				WHEN tp.ContributionType = 3 THEN 'Gran Contribuyente'
				WHEN tp.ContributionType = 4 THEN 'Regimen Simple'
				WHEN tp.ContributionType = 5 THEN 'Exento'
			END AS ContributionType, 
			'Confirmado' AS DocumentState,
			c.Name AS Currency,
			ean.EconomicActivityCodeName,
			SUM(ard.BaseValue) AS Subtotal,
			0 AS Valordescuento,
			SUM(ard.BaseValue) AS Valorneto,
			SUM(ard.TaxValue) AS ValorTotalIVA,
			SUM(ard.Value) AS Total
		FROM Portfolio.PortfolioNote pn
		JOIN Portfolio.PortfolioNoteAccountReceivableAdvance pnara ON pn.Id = pnara.PortfolioNoteId
		JOIN Portfolio.PortfolioNoteAccountReceivableDetail ard ON ard.PortfolioNoteAccountReceivableId = pnara.Id
		JOIN Invoice i ON i.Id = pn.Id
		JOIN Consecutivos cn ON pn.Id = cn.Id
		JOIN Common.Customer cus ON cus.Id = pn.CustomerId
		JOIN Common.ThirdParty tp ON tp.Id = cus.ThirdPartyId
		JOIN Common.Currency c ON pn.CurrencyId = c.Id
		LEFT JOIN EconomicActivitesNotes_Agg ean ON ean.PortfolioNoteId = pn.Id
		WHERE pn.NoteDate BETWEEN @DateStart AND @DateEnd 
		  AND pn.NoteType = 6 
		  AND pn.Status = 2 -- Notas Confirmadas 
		GROUP BY
			pn.Id, pn.Code, cn.JournalVoucherName, pn.Nature, pn.NoteDate, 
			cn.ConsecutivoComprobante, tp.Nit, tp.Name, pn.Observations, 
			tp.ContributionType, c.Name, ean.EconomicActivityCodeName

		--------------------------------------------------------------------------------------------------------------------------
		
		insert into #taxesBase (TaxDocumentType, TaxDocumentId, BaseTaxName, BaseValue)
		select 1, i.Id, isnull('Base '+tax.Name,'Base No Gravada') as BaseTaxName,

				[Common].[CurrencyConverterByModule](sum(isnull(id.NetWorth,0)),i.CurrencyId,@ReportCurrency,NULL,'Invoice',i.InvoiceDate) as BaseValue
		from Billing.Invoice i
		join Portfolio.AccountReceivable ar on ar.InvoiceId = i.Id
		inner join Billing.InvoiceDetail id on i.Id = id.InvoiceId
		left join GeneralLedger.GeneralLedgerIVA tax on tax.Id = id.TaxId
		where 
		i.InvoiceDate between @DateStart and @DateEnd
		group by i.Id, tax.Id, tax.Name, tax.[Percentage], i.CurrencyId, i.InvoiceDate, ar.Id
		UNION ALL
		SELECT 1, bb.InvoiceId, isnull('Base '+ iv.Name,'Base No Gravada') as BaseTaxName, Common.CurrencyConverterWithDate(SUM(bbd.Value), bb.CurrencyId, @ReportCurrency,bb.DocumentDate) as BaseValue
		FROM Billing.BasicBillingDetail bbd
		INNER JOIN Billing.BasicBilling bb ON bb.Id = bbd.BasicBillingId
		left join Billing.BillingConcept bc on bc.Id = bbd.BillingConceptId
		left join Inventory.InventoryProduct ip on ip.id = bbd.ProductId
		left join GeneralLedger.GeneralLedgerIVA iv on iv.Id  = COALESCE(bc.IVAId, ip.IVAId)
		WHERE bb.DocumentDate between @DateStart and @DateEnd
		GROUP BY bb.InvoiceId, iv.Name, bb.CurrencyId, bb.DocumentDate

		UNION ALL -- NOTAS DE CXC
		
		SELECT -- NOTA TIPO FACTURA TOTAL
			2 As TaxDocumentType,
			pn.Id As TaxDocumentId,
			isnull('Base '+tax.Name,'Base No Gravada') as BaseTaxName,
			[Common].[CurrencyConverterByModule](sum(isnull(pnd.Value,0)),pn.CurrencyId,@ReportCurrency,NULL,NULL,pn.NoteDate) as BaseValue
		FROM Portfolio.PortfolioNoteAccountReceivableAdvance pnara
		JOIN Portfolio.PortfolioNote pn ON pnara.PortfolioNoteId = pn.Id
		JOIN Portfolio.AccountReceivable ar ON ar.Id = pnara.AccountReceivableId
		JOIN Portfolio.PortfolioNoteDetail pnd ON pn.Id = pnd.PortfolioNoteId
		LEFT JOIN GeneralLedger.GeneralLedgerIVA tax ON tax.Id = pnd.IdGeneralLedgerIVA
		WHERE pn.NoteType = 1 AND pn.Status = 2
			AND pn.NoteDate BETWEEN @DateStart AND @DateEnd
		GROUP BY pn.Id, tax.Name, tax.Id, tax.[Percentage], pn.CurrencyId, pn.NoteDate, ar.Id

		UNION ALL

		SELECT -- NOTA TIPO FACTURA DETALLADA
			2 As TaxDocumentType,
			pn.Id As TaxDocumentId,
			isnull('Base '+tax.Name,'Base No Gravada') as BaseTaxName,
			[Common].[CurrencyConverterByModule](sum(isnull(ard.BaseValue,0)),pn.CurrencyId,@ReportCurrency,NULL,NULL,pn.NoteDate) as BaseValue
		FROM Portfolio.PortfolioNoteAccountReceivableAdvance pnara
		JOIN Portfolio.PortfolioNote pn ON pnara.PortfolioNoteId = pn.Id
		JOIN Portfolio.AccountReceivable ar ON ar.Id = pnara.AccountReceivableId
		JOIN Portfolio.PortfolioNoteAccountReceivableDetail ard ON ard.PortfolioNoteAccountReceivableId = pnara.Id
		LEFT JOIN GeneralLedger.GeneralLedgerIVA tax ON tax.Id = ard.TaxId
		WHERE pn.NoteType = 6 AND pn.Status = 2
			AND pn.NoteDate BETWEEN @DateStart AND @DateEnd
		GROUP BY pn.Id, tax.Name, tax.Id, tax.[Percentage], pn.CurrencyId, pn.NoteDate, ar.Id

		--------------------------------------------------------------------------------------------------------------------------------

		insert into #taxes (TaxDocumentType, TaxDocumentId, TaxName, TaxValue)
		select 1, i.Id, isnull(tax.Name,'') as TaxName, 
				[Common].[CurrencyConverterByModule](sum(isnull(id.GrandTotalTaxes,0)),i.CurrencyId,@ReportCurrency,NULL,'Invoice',i.InvoiceDate) IvaValue
		from Billing.Invoice i
		join Portfolio.AccountReceivable ar on ar.InvoiceId = i.Id
		inner join Billing.InvoiceDetail id on i.Id = id.InvoiceId
		left join GeneralLedger.GeneralLedgerIVA tax on tax.Id = id.TaxId
		where 
		i.InvoiceDate between @DateStart and @DateEnd
		group by i.Id, tax.Id, tax.Name, tax.[Percentage], i.CurrencyId, i.InvoiceDate, ar.Id
		UNION ALL
		SELECT 1, bb.InvoiceId, isnull(iv.Name,'') as TaxName, Common.CurrencyConverterWithDate(SUM(round((bbd.[Value] * bbd.PercentageIVA / 100.00),2)), bb.CurrencyId, @ReportCurrency,bb.DocumentDate) AS Taxes
		FROM Billing.BasicBillingDetail bbd
		INNER JOIN Billing.BasicBilling bb ON bb.Id = bbd.BasicBillingId
		left join Billing.BillingConcept bc on bc.Id = bbd.BillingConceptId
		left join Inventory.InventoryProduct ip on ip.id = bbd.ProductId
		left join GeneralLedger.GeneralLedgerIVA iv on iv.Id  = COALESCE(bc.IVAId, ip.IVAId)
		WHERE bb.DocumentDate between @DateStart and @DateEnd
		GROUP BY bb.InvoiceId, iv.Name, bb.CurrencyId, bb.DocumentDate

		UNION ALL -- NOTAS DE CXC
		
		SELECT -- NOTA TIPO FACTURA TOTAL
			2 As TaxDocumentType,
			pn.Id As TaxDocumentId,
			isnull(tax.Name,'') as TaxName,
			[Common].[CurrencyConverterByModule](sum(isnull(pnd.IvaRate,0)),pn.CurrencyId,@ReportCurrency,NULL,NULL,pn.NoteDate) IvaValue
		FROM Portfolio.PortfolioNoteAccountReceivableAdvance pnara
		JOIN Portfolio.PortfolioNote pn ON pnara.PortfolioNoteId = pn.Id
		JOIN Portfolio.AccountReceivable ar ON ar.Id = pnara.AccountReceivableId
		JOIN Portfolio.PortfolioNoteDetail pnd ON pn.Id = pnd.PortfolioNoteId
		LEFT JOIN GeneralLedger.GeneralLedgerIVA tax ON tax.Id = pnd.IdGeneralLedgerIVA
		WHERE pn.NoteType = 1 AND pn.Status = 2
			AND pn.NoteDate BETWEEN @DateStart AND @DateEnd
		GROUP BY pn.Id, tax.Name, tax.Id, tax.[Percentage], pn.CurrencyId, pn.NoteDate, ar.Id

		UNION ALL

		SELECT -- NOTA TIPO FACTURA DETALLADA
			2 As TaxDocumentType,
			pn.Id As TaxDocumentId,
			isnull(tax.Name,'') as TaxName,
			[Common].[CurrencyConverterByModule](sum(isnull(ard.TaxValue,0)),pn.CurrencyId,@ReportCurrency,NULL,NULL,pn.NoteDate) IvaValue
		FROM Portfolio.PortfolioNoteAccountReceivableAdvance pnara
		JOIN Portfolio.PortfolioNote pn ON pnara.PortfolioNoteId = pn.Id
		JOIN Portfolio.AccountReceivable ar ON ar.Id = pnara.AccountReceivableId
		JOIN Portfolio.PortfolioNoteAccountReceivableDetail ard ON ard.PortfolioNoteAccountReceivableId = pnara.Id
		LEFT JOIN GeneralLedger.GeneralLedgerIVA tax ON tax.Id = ard.TaxId
		WHERE pn.NoteType = 6 AND pn.Status = 2
			AND pn.NoteDate BETWEEN @DateStart AND @DateEnd
		GROUP BY pn.Id, tax.Name, tax.Id, tax.[Percentage], pn.CurrencyId, pn.NoteDate, ar.Id
				
		
	-------------------------------------------------------------------------------------------------------------------------------			
		insert into @basesTaxes
		values ('Base No Gravada', '')

		insert into @basesTaxes
		select isnull('Base '+ Name,'Base No Gravada') as BaseTaxName, isnull( Name,'') as TaxName from GeneralLedger.GeneralLedgerIVA where Status = 1 order by [Percentage] asc

		declare @BaseFields varchar(1000) = (select STRING_AGG(dat.TaxName, ',') from (select concat('[', NameBase,']') as TaxName from @basesTaxes) as dat)
		declare @TaxFields varchar(1000) = (select STRING_AGG(dat.TaxName, ',') from (select concat('[', NameTax,']') as TaxName from @basesTaxes where NameTax <> '') as dat)

		exec ('
			select
			dat.DocumentDate as FechaDocumento,
			case dat.DocumentType 
				when 1 then ''Factura'' 
				when 2 then CONCAT(''Nota '', dat.Nature)
			end as TipoDocumento,
			dat.DocumentCode as Documento,
			dat.ConsecutiveVoucher as ConsecutivoComprobante,
			dat.DocumentAffected as DocumentoAjustado,
			dat.ThirdPartyDNI as IdentificacionTercero,
			dat.ThirdPartyName as NombreTercero,
			dat.Detail as Observacion,
			dat.ContributionType as CategoríaTributaria,
			dat.Currency as Moneda,
			dat.EconomicActivity ActividadEconomica,
			dat.Subtotal,
			dat.DiscountValue,
			dat.NetValue,
			dat.TotalIVAValue,
			dat.Total,
			 '+ @BaseFields +', '+ @TaxFields +'
			from #data as dat
			inner join (
				select *
				from #taxesBase
				pivot (sum(BaseValue) for BaseTaxName in ('+ @BaseFields +')) as pivottable
			) as tmpBase on tmpBase.TaxDocumentType = dat.DocumentType and tmpBase.TaxDocumentId = dat.DocumentId
			inner join (
				select *
				from #taxes
				pivot (sum(TaxValue) for TaxName in ('+ @TaxFields +')) as pivottable2
			) as tmp on tmp.TaxDocumentType = dat.DocumentType and tmp.TaxDocumentId = dat.DocumentId
			')

	END TRY
	BEGIN CATCH	
		SELECT '999' AS Code, ERROR_MESSAGE() AS Message, ERROR_LINE() AS Line
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento almacenado que genera el estado de ventas con desglose de IVA (Impuesto al Valor Agregado) para el módulo de contabilidad general. Consolida información de facturas de venta (Billing.Invoice), notas de cuentas por cobrar de cartera (Portfolio.PortfolioNote, Portfolio.AccountReceivable) y sus respectivos anticipos, calculando subtotales, descuentos, valor neto, valor de IVA por base tributaria y total por documento. Aplica filtros de rango de fechas, libro contable, moneda de reporte y rango de terceros; construye SQL dinámico en tiempo de ejecución para pivotar las bases y tarifas de impuestos, por lo que el grafo de dependencias puede ser incompleto. El resultado es un informe contable-tributario utilizado para declaración y conciliación del IVA en ventas, incluyendo actividades económicas, responsabilidades fiscales del tercero y el comprobante de diario asociado a cada documento.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportStatementSaleIVA';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportStatementSaleIVA';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte contable-tributario de IVA en ventas pivotando bases y tarifas por documento, consolidando facturas y notas de cartera (tipo factura total y detallada) confirmadas en un rango de fechas.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportStatementSaleIVA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los XML de criterios y filtros deben contener DateStart, DateEnd, Book y ReportCurrency válidos.; Debe existir un registro en GeneralLedger.CompanySettings con OfficialCurrencyId.; La moneda de reporte debe existir en Common.Currency con su ISO4217 asociado.; Las notas de cartera deben tener Status = 2 (confirmadas) para ser incluidas.; Las notas consideradas son únicamente NoteType 1 (factura total) y 6 (factura detallada).', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportStatementSaleIVA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las facturas se restringen siempre por InvoiceDate entre @DateStart y @DateEnd.; Las notas de cartera incluidas siempre tienen Status = 2 (confirmadas).; Solo se consideran JournalVouchers con LegalBookId = 1 y EntityName = ''Invoice'' para enlazar comprobantes a facturas.; El estado del documento factura siempre se reporta como ''Activa'' y el de las notas como ''Confirmado''.; Los valores monetarios se convierten siempre a la moneda de reporte usando Common.CurrencyConverterByModule o Common.CurrencyConverterWithDate.; Solo participan en el pivote tarifas de IVA con Status = 1, ordenadas ascendentemente por porcentaje.; Cualquier error en el TRY no aborta: se devuelve un resultset con Code ''999''.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportStatementSaleIVA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] #data: Inserta facturas (DocumentType=1) cuyo InvoiceDate esté entre @DateStart y @DateEnd, calculando NetWorth, descuentos, impuestos y total con conversión de moneda según corresponda.; [INSERT] #data: Inserta notas de cartera (DocumentType=2) con NoteType=1 y Status=2 entre fechas, sumando Value y IvaRate de PortfolioNoteDetail; mapea Nature 1->''Debito'', 2->''Crédito'' y ContributionType 0..5 a etiquetas tributarias.; [INSERT] #data: Inserta notas de cartera tipo factura detallada (NoteType=6, Status=2) entre fechas, sumando BaseValue, TaxValue y Value desde PortfolioNoteAccountReceivableDetail.; [INSERT] #taxesBase: Para cada factura/nota dentro del rango de fechas, inserta la base por tarifa de IVA con etiqueta ''Base ''+tax.Name (o ''Base No Gravada'' cuando no hay tarifa), convirtiendo a la moneda de reporte.; [INSERT] #taxes: Para cada factura/nota dentro del rango de fechas, inserta el valor del IVA por tarifa, convertido a la moneda de reporte.; [INSERT] @basesTaxes: Inserta una fila fija (''Base No Gravada'','''') y una fila por cada GeneralLedgerIVA con Status=1 ordenada por Percentage ascendente, para construir las columnas del pivote.; [RETURN_RESULT] N/A: Ejecuta SQL dinámico que pivota #taxesBase y #taxes por documento y retorna el dataset final con columnas dinámicas por cada base y cada tarifa de IVA activa.; [RETURN_RESULT] N/A: En el bloque CATCH retorna un único registro con Code=''999'', Message=ERROR_MESSAGE() y Line=ERROR_LINE() en vez de lanzar la excepción.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportStatementSaleIVA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si DocumentType = 1 (Factura) → Se etiqueta como ''Factura'' y se toman datos de Billing.Invoice/InvoiceDetail o BasicBilling/BasicBillingDetail.; si DocumentType = 2 (Nota CxC) → Se etiqueta como ''Nota ''+Nature (''Debito'' o ''Crédito'') según pn.Nature 1 o 2.; si ISNULL(rcd.IsMasterAccount,0) = 0 en RevenueControlDetail → NetWorth se calcula como sod.GrossValue * id.InvoicedQuantity convertido a la moneda oficial. else NetWorth toma directamente id.NetWorth sin recalcular.; si tp.ContributionType IN (0..5) → Se traduce a categoría tributaria: ''No Responsable de Iva'', ''Responsable de Iva'', ''Empresa estatal'', ''Gran Contribuyente'', ''Regimen Simple'' o ''Exento''.; si pn.NoteType = 1 y Status = 2 → Se procesa como nota tipo factura total usando PortfolioNoteDetail.; si pn.NoteType = 6 y Status = 2 → Se procesa como nota tipo factura detallada usando PortfolioNoteAccountReceivableDetail.; si tax.Name no nulo → Se etiqueta la base como ''Base ''+tax.Name. else Se etiqueta como ''Base No Gravada''.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportStatementSaleIVA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.CurrencyConverterByModule; Common.CurrencyConverterWithDate', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportStatementSaleIVA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportStatementSaleIVA';
-- GO
