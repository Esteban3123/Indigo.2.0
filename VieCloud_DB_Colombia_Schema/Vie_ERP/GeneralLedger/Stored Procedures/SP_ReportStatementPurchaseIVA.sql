
CREATE PROCEDURE [GeneralLedger].[SP_ReportStatementPurchaseIVA]
	-- Add the parameters for the stored procedure here
	@Criterios xml,
	@filtros xml
AS
BEGIN
 
	set DATEFORMAT YMD

	Declare @DateStart DATE,
			@DateEnd DATE,
			@LegalBookId INT,
			@ReportCurrency int,

			@GeneralLedgerIVA VARCHAR(max),
			@ThirdPartyStart VARCHAR(50),
			@ThirdPartyEnd VARCHAR(50)

		--Se obtienen los criterios
		SELECT	@DateStart = t.x.value('DateStart[1]','DATE'),
				@DateEnd = t.x.value('DateEnd[1]','DATE'),
				@LegalBookId = t.x.value('Book[1]','int'),
				@ReportCurrency = t.x.value('ReportCurrency[1]','int')
		FROM @Criterios.nodes('/Data') t(x)

		--Se obtienen los filtros
		SELECT	@GeneralLedgerIVA = t.x.value('GeneralLedgerIVA[1]','VARCHAR(max)'),
				@ThirdPartyStart = t.x.value('ThirdPartyStart[1]','VARCHAR(50)'),
				@ThirdPartyEnd = t.x.value('ThirdPartyEnd[1]','VARCHAR(50)')
		FROM @filtros.nodes('/Data') t(x)

		DECLARE @IVAIds TABLE (Id INT)
		DECLARE @Delimiter CHAR(1) = ','
		DECLARE @Value NVARCHAR(100)

		WHILE CHARINDEX(@Delimiter, @GeneralLedgerIVA) > 0
		BEGIN
			SET @Value = LTRIM(RTRIM(LEFT(@GeneralLedgerIVA, CHARINDEX(@Delimiter, @GeneralLedgerIVA) - 1)))
			INSERT INTO @IVAIds (Id) VALUES (CAST(@Value AS INT))
			SET @GeneralLedgerIVA = SUBSTRING(@GeneralLedgerIVA, CHARINDEX(@Delimiter, @GeneralLedgerIVA) + 1, LEN(@GeneralLedgerIVA))
		END

		IF LTRIM(RTRIM(@GeneralLedgerIVA)) <> ''
			INSERT INTO @IVAIds (Id) VALUES (CAST(@GeneralLedgerIVA AS INT))

	DROP TABLE IF EXISTS #data
		create table #data (
			DocumentType tinyint, --1 CxP, 2 - Notas
			DocumentId int,
			DocumentDate date,
			DocumentCode varchar(50),
			JournalVoucher varchar(50),
			ConsecutiveVoucher varchar(10),
			DocumentAffected varchar(20),
			ThirdPartyDNI varchar(20),
			ThirdPartyName varchar(100),
			Detail varchar(500),
			ContributionType varchar(200),
			Currency varchar(30),
			EconomicActivity varchar(MAX),
			TotalValue numeric(20,2),
			Balance numeric(20,2),
			TRM numeric(20,5),
			TotalInCurrencySelected numeric(20,2)
		)

	DROP TABLE IF EXISTS #taxesBases
		create table #taxesBases (
			TaxDocumentType tinyint, -- 1 CxP, 2 Notas
			TaxDocumentId varchar(50),
			BaseTaxName varchar(100),
			BaseValue numeric(20, 2)
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
			NameTax varchar(100))

	declare @CurrencyId int = @ReportCurrency
	declare @CurrencyName varchar(100) = (select iso.CurrencyName from Common.Currency c inner join Common.ISO4217 iso on c.ISO4217Id = iso.Id where c.Id = @CurrencyId)

	declare @OfficialBookId int = (select Id from GeneralLedger.LegalBook where OfficialBook = 1);

	WITH EconomicActivitesVoucherTransaction_Distinct AS (
		SELECT DISTINCT
			vt.Id AS VoucherTransactionId,
			CONCAT(ea.Code,' - ',ea.Name) AS EA
		FROM Treasury.VoucherTransactionDetails vtd
		JOIN Treasury.VoucherTransaction vt ON vt.Id = vtd.IdVoucherTransaction
		LEFT JOIN Common.EconomicActivity ea ON ea.Id = vtd.EconomicActivityId
		WHERE vt.DocumentDate BETWEEN @DateStart and @DateEnd AND ea.Id IS NOT NULL
	), EconomicActivitesVoucherTransaction_Agg AS (
		SELECT VoucherTransactionId, STRING_AGG(EA, ',') AS EconomicActivityCodeName
		FROM EconomicActivitesVoucherTransaction_Distinct
		GROUP BY VoucherTransactionId)

	insert into #data
		--Cuentas por Pagar
		select 1, 
			ap.Id,
			DocumentDate,
			ap.Code, 
			jvt.Name, 
			jv.Consecutive, 
			null,
			tp.Nit,
			tp.Name,
			left(ap.Coments,500),
			(select STRING_AGG(fr.[Name], ',')
			from Common.ThirdPartyFiscalResponsibility tpfr
			inner join Common.FiscalResponsibility fr on tpfr.FiscalResponsibilityId = fr.Id
			where tpfr.ThirdPartyId = tp.Id),
			c.Name as Currency,
			IIF(ea.Id IS NOT NULL, CONCAT(ea.Code,' - ',ea.Name), NULL)	EconomicActivityCodeName
			,ap.Value
			,ap.Balance,
			ISNULL(er.Value, 1),
			[Common].[CurrencyConverterWithDate] (ap.Value, ap.CurrencyId, @CurrencyId, ap.DocumentDate)
		from Payments.AccountPayable ap
		inner join Common.Currency c on c.Id = ap.CurrencyId
		inner join Common.ThirdParty tp on tp.Id = ap.IdThirdParty
		left join Payments.AccountPayableExchangeRate er on er.AccountPayableId = ap.Id and er.CurrencyId = @CurrencyId
		left join GeneralLedger.JournalVouchers jv on jv.LegalBookId = @OfficialBookId and jv.EntityId = ap.Id and jv.EntityName = 'AccountPayable'
		left join GeneralLedger.JournalVoucherTypes jvt on jvt.Id = jv.IdJournalVoucher
		LEFT JOIN Common.EconomicActivity ea ON ea.Id = ap.IdEconomicActivity
		where ap.DocumentDate between @DateStart and @DateEnd and ap.Status = 2 
	union all

	--Notas Débito / Crédito
	select  2,
			pn.Id,
			pn.NoteDate,
			pn.Code,
			jvt.Name,
			jv.Consecutive,
			ap.Code,
			t.Nit,
			t.Name,
			left(pn.Comment,500),
			(select STRING_AGG(fr.[Name], ',')
			from Common.ThirdPartyFiscalResponsibility tpfr
			inner join Common.FiscalResponsibility fr on tpfr.FiscalResponsibilityId = fr.Id
			where tpfr.ThirdPartyId = t.Id),
			c.Name as Currency,
			IIF(ea.Id IS NOT NULL, CONCAT(ea.Code,' - ',ea.Name), NULL)	EconomicActivityCodeName,
			pnapa.AdjusmentValue,
			0 as Balance,
			isnull(er.Value, 1),
			[Common].[CurrencyConverterWithDate] (pnapa.AdjusmentValue, pn.CurrencyId, 1, pn.NoteDate)
	from Payments.PaymentNotes pn
	inner join Common.Currency c on c.Id = pn.CurrencyId
	inner join Common.Supplier s on s.Id = pn.IdSupplier
	inner join Common.ThirdParty t on t.Id = s.IdThirdParty
	inner join Payments.PaymentNotesAccountPayableAdvance pnapa on pn.Id = pnapa.PaymentNoteId
	inner join Payments.AccountPayable ap on ap.Id = pnapa.AccountPayableId
	LEFT JOIN Common.EconomicActivity ea ON ea.Id = ap.IdEconomicActivity
	left join Payments.AccountPayableExchangeRate er on er.AccountPayableId = ap.Id and er.CurrencyId = @CurrencyId
	left join GeneralLedger.JournalVouchers jv on jv.LegalBookId = @OfficialBookId and jv.EntityId = pn.Id and jv.EntityName = 'PaymentNotes'
	left join GeneralLedger.JournalVoucherTypes jvt on jvt.Id = jv.IdJournalVoucher
	where pn.NoteDate between @DateStart and @DateEnd and pn.Status = 2 

	union all -- Notas que aplican Reversión Cuentas por Pagar

	select  2,
			pn.Id,
			pn.NoteDate,
			pn.Code,
			jvt.Name,
			jv.Consecutive,
			ap.Code,
			t.Nit,
			t.Name,
			left(pn.Comment,500),
			(select STRING_AGG(fr.[Name], ',')
			from Common.ThirdPartyFiscalResponsibility tpfr
			inner join Common.FiscalResponsibility fr on tpfr.FiscalResponsibilityId = fr.Id
			where tpfr.ThirdPartyId = t.Id),
			c.Name as Currency,
			IIF(ea.Id IS NOT NULL, CONCAT(ea.Code,' - ',ea.Name), NULL)	EconomicActivityCodeName,
			apdc.[Value],
			0 as Balance,
			isnull(er.Value, 1),
			[Common].[CurrencyConverterWithDate] (apdc.[Value], pn.CurrencyId, 1, pn.NoteDate)
	from Payments.PaymentNotes pn
	inner join Common.Currency c on c.Id = pn.CurrencyId
	inner join Common.Supplier s on s.Id = pn.IdSupplier
	inner join Common.ThirdParty t on t.Id = s.IdThirdParty
	inner join Payments.AccountPayable ap on ap.Id = pn.IdAccountPayable
	inner join Payments.AccountPayableDetailConcept apdc ON apdc.IdAccountPayable = ap.Id
	LEFT JOIN Common.EconomicActivity ea ON ea.Id = ap.IdEconomicActivity
	left join Payments.AccountPayableExchangeRate er on er.AccountPayableId = ap.Id and er.CurrencyId = @CurrencyId
	left join GeneralLedger.JournalVouchers jv on jv.LegalBookId = @OfficialBookId and jv.EntityId = pn.Id and jv.EntityName = 'PaymentNotes'
	left join GeneralLedger.JournalVoucherTypes jvt on jvt.Id = jv.IdJournalVoucher
	where pn.IndicatesBillAdvance = 2 and pn.NoteDate between @DateStart and @DateEnd and pn.Status = 2

	union all 
	--Comprobante de Egreso
	select  3,
			vt.Id,
			vt.DocumentDate,
			vt.Code,
			jvt.Name,
			jv.Consecutive,
			null,
			t.Nit,
			t.Name,
			left(vt.Detail,500),
			(select STRING_AGG(fr.[Name], ',')
			from Common.ThirdPartyFiscalResponsibility tpfr
			inner join Common.FiscalResponsibility fr on tpfr.FiscalResponsibilityId = fr.Id
			where tpfr.ThirdPartyId = t.Id),
			c.Name as Currency,
			eavt.EconomicActivityCodeName,
			sum(vtd.TotalConcept),
			0 as Balance,
			[Common].[CurrencyConverterWithDate]  (1, vt.CurrencyId, @CurrencyId, vt.DocumentDate),
			sum([Common].[CurrencyConverterWithDate] (vtd.TotalConcept, vt.CurrencyId, @CurrencyId, vt.DocumentDate))
	from Treasury.VoucherTransactionDetails vtd
	inner join Treasury.VoucherTransaction vt on vt.Id = vtd.IdVoucherTransaction
	inner join Common.Currency c on c.Id = vt.CurrencyId
	inner join Common.ThirdParty t on t.Id = vt.IdThirdParty
	inner join Treasury.ExpenseConcepts ec on ec.Id = vtd.IdExpenseConcept and ec.Behavior=6
	inner join GeneralLedger.MainAccounts ma on ma.Id = ec.IdMainAccount and ma.RetencionType=0
	left join GeneralLedger.JournalVouchers jv on jv.LegalBookId = @OfficialBookId and jv.EntityId = vt.Id and jv.EntityName = 'VoucherTransaction'
	left join GeneralLedger.JournalVoucherTypes jvt on jvt.Id = jv.IdJournalVoucher
	LEFT JOIN EconomicActivitesVoucherTransaction_Agg eavt ON eavt.VoucherTransactionId = vt.id
	where vt.DocumentDate between @DateStart and @DateEnd and vt.VoucherClass=1 and vt.Status = 2 
		GROUP by vt.Id,
			vt.DocumentDate,
			vt.Code,
			jvt.Name,
			jv.Consecutive,
			t.Nit,
			t.Name,
			vt.Detail,c.Name,t.Id,vt.CurrencyId, eavt.EconomicActivityCodeName
	
	union ALL

	--Reversión de Comprobante de Egreso
	select  4,
			tn.Id,
			tn.NoteDate,
			tn.Code,
			jtn.Name,
			jv.Consecutive,
			vt.Code,
			t.Nit,
			t.Name,
			left(tn.Description,500),
			(select STRING_AGG(fr.[Name], ',')
			from Common.ThirdPartyFiscalResponsibility tpfr
			inner join Common.FiscalResponsibility fr on tpfr.FiscalResponsibilityId = fr.Id
			where tpfr.ThirdPartyId = t.Id),
			c.Name as Currency,
			eavt.EconomicActivityCodeName,
			tn.Value,
			0 as Balance,
			[Common].[CurrencyConverterWithDate]  (1, vt.CurrencyId, @CurrencyId, vt.DocumentDate),
			[Common].[CurrencyConverterWithDate] (tn.Value, vt.CurrencyId, @CurrencyId, vt.DocumentDate)
	from Treasury.TreasuryNote tn 
	inner join Common.Currency c on c.Id = tn.CurrencyId
	inner join Treasury.VoucherTransaction vt on vt.Id = tn.VoucherTransactionId
	inner join Common.ThirdParty t on t.Id = vt.IdThirdParty
	left join GeneralLedger.JournalVouchers jv on jv.LegalBookId = @OfficialBookId and jv.EntityId = tn.Id and jv.EntityName = 'TreasuryNote'
	left join GeneralLedger.JournalVoucherTypes jtn on jtn.Id = jv.IdJournalVoucher
	LEFT JOIN EconomicActivitesVoucherTransaction_Agg eavt ON eavt.VoucherTransactionId = vt.id
	where tn.NoteDate between @DateStart and @DateEnd and tn.Status = 2
 
 --------------------------------------------------------------------------------------------------------------------------------------
	insert into #taxesBases
	(TaxDocumentType, TaxDocumentId, BaseTaxName, BaseValue)
	select 1,
			ap.Id,
		   isnull('Base '+tax.Name,'Base No Gravada') as BaseTaxName,
		   [Common].[CurrencyConverterWithDate] (sum(IIF(apdc.RateIva IS NULL AND apdc.BaseValue <> 0, apdc.Value, isnull(apdc.BaseValue,0))), ap.CurrencyId, @CurrencyId, ap.DocumentDate) as BaseValue
	from Payments.AccountPayable ap
	inner join Common.Currency c on c.Id = ap.CurrencyId
	inner join Payments.AccountPayableDetailConcept apdc on apdc.IdAccountPayable = ap.Id
	inner join Payments.AccountPayableConcepts con on con.Id = apdc.IdConceptAccountPayable
	left join GeneralLedger.GeneralLedgerIVA tax on tax.Id = apdc.RateIva
	where ap.DocumentDate between @DateStart and @DateEnd and ap.Status = 2 and con.HandlesRetention = 0 
	AND (apdc.Detail IS NULL OR apdc.Detail != 'Detalle de cuenta por pagar generada por Comprobante de Entrada ''Costo Producto En Consignación''')
	AND (@GeneralLedgerIVA IS NULL OR EXISTS (SELECT 1 FROM @IVAIds ids WHERE ids.Id = tax.Id))
	group by ap.Id, tax.Id, tax.Name, tax.[Percentage], ap.CurrencyId, ap.DocumentDate

	union all

	select 2,
		   pn.Id,  
		   isnull('Base '+tax.Name,'Base No Gravada') as BaseTaxName, 
		  [Common].[CurrencyConverterWithDate] (sum(isnull(pnd.BaseValue,0)), pn.CurrencyId, @CurrencyId, pn.NoteDate) as BaseValue
	from Payments.PaymentNotes pn
	inner join Common.Currency c on c.Id = pn.CurrencyId
	inner join Common.Supplier s on s.Id = pn.IdSupplier
	inner join Common.ThirdParty t on t.Id = s.IdThirdParty
	inner join Payments.PaymentsNoteDetails pnd on pnd.IdPaymentsNote = pn.Id
	inner join Payments.AccountPayableConceptNotes apcn on apcn.Id = pnd.IdAccountPayableConceptNotes
	left join GeneralLedger.GeneralLedgerIVA tax on tax.Id = pnd.IdGeneralLedgerIVA
	where pn.NoteDate between @DateStart and @DateEnd and pn.Status = 2 and apcn.ManageRetention = 0
	AND (@GeneralLedgerIVA IS NULL OR EXISTS (SELECT 1 FROM @IVAIds ids WHERE ids.Id = tax.Id))
	group by pn.Id, tax.Id, tax.Name, tax.[Percentage], pn.CurrencyId, pn.NoteDate

	union all -- Notas que aplican Reversión CxP

	select 2,
		   pn.Id,  
		   isnull('Base '+tax.Name,'Base No Gravada') as BaseTaxName, 
		  [Common].[CurrencyConverterWithDate] (sum(isnull(apdc.BaseValue,0)), pn.CurrencyId, @CurrencyId, pn.NoteDate) as BaseValue
	from Payments.PaymentNotes pn
	inner join Common.Currency c on c.Id = pn.CurrencyId
	inner join Common.Supplier s on s.Id = pn.IdSupplier
	inner join Common.ThirdParty t on t.Id = s.IdThirdParty
	inner join Payments.AccountPayable ap ON pn.IdAccountPayable = ap.Id
	inner join Payments.AccountPayableDetailConcept apdc ON apdc.IdAccountPayable = ap.Id
	left join GeneralLedger.GeneralLedgerIVA tax on tax.Id = apdc.RateIva
	where pn.NoteDate between @DateStart and @DateEnd and pn.IndicatesBillAdvance = 2 and pn.Status = 2
	AND (@GeneralLedgerIVA IS NULL OR EXISTS (SELECT 1 FROM @IVAIds ids WHERE ids.Id = tax.Id))
	group by pn.Id, tax.Id, tax.Name, tax.[Percentage], pn.CurrencyId, pn.NoteDate 

	union all
		select  3,
		vt.Id,  
		isnull('Base '+tax.Name,'Base No Gravada') as BaseTaxName, 
		[Common].[CurrencyConverterWithDate] (sum(COALESCE(vtd.BaseValue,vtd.value,0)), vt.CurrencyId, @CurrencyId, vt.DocumentDate) as BaseValue
	from Treasury.VoucherTransaction vt
	inner join Common.ThirdParty t on t.Id = vt.IdThirdParty 
	inner join Treasury.VoucherTransactionDetails vtd on vtd.IdVoucherTransaction = vt.Id
	inner join  Treasury.ExpenseConcepts ec on ec.Id = vtd.IdExpenseConcept
	left join GeneralLedger.GeneralLedgerIVA tax on tax.Id = vtd.IdGeneralLedgerIVA
	where vt.DocumentDate between @DateStart and @DateEnd and vt.Status = 2 
	AND (@GeneralLedgerIVA IS NULL OR EXISTS (SELECT 1 FROM @IVAIds ids WHERE ids.Id = tax.Id))
	group by vt.Id, tax.Id, tax.Name, tax.[Percentage], vt.CurrencyId, vt.DocumentDate
	
	union ALL
		select  4,
		tn.Id,  
		isnull('Base '+tax.Name,'Base No Gravada') as BaseTaxName, 
		[Common].[CurrencyConverterWithDate] (sum(COALESCE(vtd.BaseValue,vtd.Value,0)), vt.CurrencyId, @CurrencyId, vt.DocumentDate) as BaseValue
	from Treasury.TreasuryNote tn
	inner join Treasury.VoucherTransaction vt on vt.Id = tn.VoucherTransactionId
	inner join Treasury.VoucherTransactionDetails vtd on vtd.IdVoucherTransaction = vt.Id
	inner join Common.ThirdParty t on t.Id = vt.IdThirdParty 
	inner join  Treasury.ExpenseConcepts ec on ec.Id = vtd.IdExpenseConcept
	left join GeneralLedger.GeneralLedgerIVA tax on tax.Id = vtd.IdGeneralLedgerIVA
	where vt.DocumentDate between @DateStart and @DateEnd  and tn.NoteType=3  and tn.Status = 2
	AND (@GeneralLedgerIVA IS NULL OR EXISTS (SELECT 1 FROM @IVAIds ids WHERE ids.Id = tax.Id))
	group by tn.Id, tax.Id, tax.Name, tax.[Percentage], vt.CurrencyId, vt.DocumentDate
----------------------------------------------------------------------------------------------------------------------------
	insert into #taxes
	(TaxDocumentType, TaxDocumentId, TaxName, TaxValue)
	select 1,
			ap.Id,
		   isnull(tax.Name,'') as TaxName, 
		   [Common].[CurrencyConverterWithDate] (sum(isnull(apdc.IvaValue,0)), ap.CurrencyId, @CurrencyId, ap.DocumentDate) as IvaValue
	from Payments.AccountPayable ap
	inner join Common.Currency c on c.Id = ap.CurrencyId
	inner join Payments.AccountPayableDetailConcept apdc on apdc.IdAccountPayable = ap.Id
	inner join Payments.AccountPayableConcepts con on con.Id = apdc.IdConceptAccountPayable
	left join GeneralLedger.GeneralLedgerIVA tax on tax.Id = apdc.RateIva
	where ap.DocumentDate between @DateStart and @DateEnd and ap.Status = 2 and con.HandlesRetention = 0  
	AND (@GeneralLedgerIVA IS NULL OR EXISTS (SELECT 1 FROM @IVAIds ids WHERE ids.Id = tax.Id))
	group by ap.Id, tax.Id, tax.Name, tax.[Percentage], ap.CurrencyId, ap.DocumentDate

	union all

	select 2,
		   pn.Id, 
		   isnull(tax.Name,'') as TaxName, 
		  [Common].[CurrencyConverterWithDate] (sum(isnull(pnd.IvaValue,0)), pn.CurrencyId, @CurrencyId, pn.NoteDate) as IvaValue
	from Payments.PaymentNotes pn
	inner join Common.Currency c on c.Id = pn.CurrencyId
	inner join Common.Supplier s on s.Id = pn.IdSupplier
	inner join Common.ThirdParty t on t.Id = s.IdThirdParty
	inner join Payments.PaymentsNoteDetails pnd on pnd.IdPaymentsNote = pn.Id
	inner join Payments.AccountPayableConceptNotes apcn on apcn.Id = pnd.IdAccountPayableConceptNotes
	left join GeneralLedger.GeneralLedgerIVA tax on tax.Id = pnd.IdGeneralLedgerIVA
	where pn.NoteDate between @DateStart and @DateEnd and pn.Status = 2 and apcn.ManageRetention = 0
	AND (@GeneralLedgerIVA IS NULL OR EXISTS (SELECT 1 FROM @IVAIds ids WHERE ids.Id = tax.Id))
	group by pn.Id, tax.Id, tax.Name, tax.[Percentage], pn.CurrencyId, pn.NoteDate

	union all  -- Notas que aplican Reversión CxP
	select 2,
		   pn.Id, 
		   isnull(tax.Name,'') as TaxName, 
		  [Common].[CurrencyConverterWithDate] (sum(isnull(apdc.IvaValue,0)), pn.CurrencyId, @CurrencyId, pn.NoteDate) as IvaValue
	from Payments.PaymentNotes pn
	inner join Common.Currency c on c.Id = pn.CurrencyId
	inner join Common.Supplier s on s.Id = pn.IdSupplier
	inner join Common.ThirdParty t on t.Id = s.IdThirdParty
	inner join Payments.AccountPayable ap ON pn.IdAccountPayable = ap.Id
	inner join Payments.AccountPayableDetailConcept apdc ON apdc.IdAccountPayable = ap.Id
	left join GeneralLedger.GeneralLedgerIVA tax on tax.Id = apdc.RateIva
	where pn.NoteDate between @DateStart and @DateEnd and pn.IndicatesBillAdvance = 2 and pn.Status = 2 
	AND (@GeneralLedgerIVA IS NULL OR EXISTS (SELECT 1 FROM @IVAIds ids WHERE ids.Id = tax.Id))
	group by pn.Id, tax.Id, tax.Name, tax.[Percentage], pn.CurrencyId, pn.NoteDate

	union all
	select 3,
		   vt.Id, 
		   isnull(tax.Name,'') as TaxName, 
		  [Common].[CurrencyConverterWithDate] (sum(isnull(vtd.ValueIVA,0)), vt.CurrencyId, @CurrencyId, vt.DocumentDate) as IvaValue
	from Treasury.VoucherTransaction vt
	inner join Common.Currency c on c.Id = vt.CurrencyId
	inner join Common.ThirdParty t on t.Id = vt.IdThirdParty
	inner join Treasury.VoucherTransactionDetails vtd on vtd.IdVoucherTransaction = vt.Id
	inner join  Treasury.ExpenseConcepts ec on ec.Id = vtd.IdExpenseConcept
	left join GeneralLedger.GeneralLedgerIVA tax on tax.Id = vtd.IdGeneralLedgerIVA
	where vt.DocumentDate between @DateStart and @DateEnd and vt.Status = 2  
	AND (@GeneralLedgerIVA IS NULL OR EXISTS (SELECT 1 FROM @IVAIds ids WHERE ids.Id = tax.Id))
	group by vt.Id, tax.Id, tax.Name, tax.[Percentage], vt.CurrencyId, vt.DocumentDate

	union all
	select 4,
		   tn.Id, 
		   isnull(tax.Name,'') as TaxName, 
		  [Common].[CurrencyConverterWithDate] (sum(isnull(vtd.ValueIVA,0)), vt.CurrencyId, @CurrencyId, vt.DocumentDate) as IvaValue
	from Treasury.TreasuryNote tn 
	inner join Treasury.VoucherTransaction vt on vt.Id = tn.VoucherTransactionId
	inner join Treasury.VoucherTransactionDetails vtd on vtd.IdVoucherTransaction = vt.Id
	inner join Common.ThirdParty t on t.Id = vt.IdThirdParty
	inner join Common.Currency c on c.Id = vt.CurrencyId
	inner join  Treasury.ExpenseConcepts ec on ec.Id = vtd.IdExpenseConcept
	left join GeneralLedger.GeneralLedgerIVA tax on tax.Id = vtd.IdGeneralLedgerIVA
	where vt.DocumentDate between @DateStart and @DateEnd and tn.NoteType=3  and tn.Status = 2  
	AND (@GeneralLedgerIVA IS NULL OR EXISTS (SELECT 1 FROM @IVAIds ids WHERE ids.Id = tax.Id))
	group by tn.Id, tax.Id, tax.Name, tax.[Percentage], vt.CurrencyId, vt.DocumentDate
---------------------------------------------------------------------------------------------------------------------------------

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
		when 1 then ''Cuenta por pagar'' 
		when 2 then ''Notas'' 
		when 3 then ''Comprobante de Egreso''
		when 4 then ''Reversión Comprobante de Egreso''
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
	dat.TotalValue as Monto,
	dat.Balance as Saldo,
	dat.TRM as TipoCambio,
	dat.TotalInCurrencySelected as [Monto '+ @CurrencyName +'], 
	'+ @BaseFields +', '+ @TaxFields +'
	from #data as dat
	join (
		select *
		from #taxesBases
		pivot (sum(BaseValue) for BaseTaxName in ('+ @BaseFields +')) as pivottable
	) as tmpBase on tmpBase.TaxDocumentType = dat.DocumentType and tmpBase.TaxDocumentId = dat.DocumentId
	join (
		select *
		from #taxes
		pivot (sum(TaxValue) for TaxName in ('+ @TaxFields +')) as pivottable2
	) as tmpTaxes on tmpTaxes.TaxDocumentType = dat.DocumentType and tmpTaxes.TaxDocumentId = dat.DocumentId
	')

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento almacenado que genera el reporte de declaración de IVA en compras para un período contable determinado. Consolida información de cuentas por pagar a proveedores (facturas confirmadas) y notas débito/crédito, extrayendo para cada documento los impuestos (IVA), bases gravables, actividades económicas, responsabilidades fiscales del tercero/proveedor y valores en la moneda de reporte seleccionada (con conversión de tasa de cambio). Recibe como parámetros un rango de fechas, el libro contable oficial, la moneda de reporte y un listado de cuentas contables de IVA (filtradas dinámicamente en tiempo de ejecución mediante SQL dinámico), además de un rango de terceros para acotar el resultado. El reporte sirve para la declaración tributaria de IVA en compras, la conciliación contable y el cumplimiento fiscal ante la DIAN, integrando datos del módulo de tesorería (comprobantes de egreso), cuentas por pagar, libro mayor (GeneralLedger) y catálogos de monedas ISO 4217.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportStatementPurchaseIVA';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportStatementPurchaseIVA';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte de relación de compras con IVA discriminado, consolidando cuentas por pagar, notas, comprobantes de egreso y sus reversiones, con bases e IVA pivotados por tasa y convertidos a la moneda de reporte.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportStatementPurchaseIVA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los XML @Criterios y @filtros deben tener estructura /Data con nodos esperados (DateStart, DateEnd, Book, ReportCurrency, GeneralLedgerIVA, ThirdPartyStart, ThirdPartyEnd); @GeneralLedgerIVA debe ser una lista de Ids enteros separados por coma o vacío/NULL; Debe existir exactamente un LegalBook con OfficialBook = 1; La moneda indicada en ReportCurrency debe existir en Common.Currency con su ISO4217 asociado; La función Common.CurrencyConverterWithDate debe estar disponible y soportar conversión entre las monedas involucradas en las fechas usadas', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportStatementPurchaseIVA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran documentos con Status = 2 (aprobados/contabilizados) en CxP, Notas, VoucherTransaction y TreasuryNote; Los comprobantes contables se enlazan únicamente al libro oficial (LegalBook.OfficialBook = 1), no al libro recibido por parámetro; Los documentos se filtran por rango de fechas [@DateStart, @DateEnd]; Los conceptos con manejo de retención se excluyen del cálculo de bases e IVA; Las cuentas con RetencionType distinto de 0 se excluyen del flujo de comprobantes de egreso; Todos los valores monetarios se convierten a la moneda de reporte mediante CurrencyConverterWithDate usando la fecha del documento; El balance reportado solo aplica a CxP; en notas y comprobantes de egreso se fuerza a 0; El resultado final pivota dinámicamente columnas por cada tasa de IVA activa (Status=1) ordenada ascendentemente por porcentaje; Siempre se incluye una columna ''Base No Gravada'' aunque no exista IVA configurado', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportStatementPurchaseIVA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuentas por pagar; Notas débito/crédito; Comprobante de egreso; Reversión de comprobante de egreso; IVA (Impuesto al Valor Agregado); Base gravada / Base no gravada; Retenciones; Tercero / Proveedor; Responsabilidad fiscal; Actividad económica; Libro oficial contable; Comprobante contable (Journal Voucher); TRM / conversión de moneda; Anticipo a factura', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportStatementPurchaseIVA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Cadena de IVAs separada por comas en filtro → Se parsean los Ids de IVA y se cargan en tabla temporal para filtrar tasas de IVA; si @GeneralLedgerIVA es NULL o vacío → No se filtra por tasas de IVA específicas (se incluyen todas) else Solo se incluyen registros cuya tasa de IVA esté en la lista provista; si DocumentType en (1=CxP, 2=Notas, 3=Comprobante de Egreso, 4=Reversión Comprobante de Egreso) → Se mapea el TipoDocumento legible en el resultado final; si pn.IndicatesBillAdvance = 2 → La nota se trata como Reversión de Cuenta por Pagar y toma valores desde AccountPayableDetailConcept; si ec.Behavior=6 y ma.RetencionType=0 → Solo se incluyen detalles de comprobante de egreso cuyo concepto de gasto y cuenta contable no correspondan a retenciones; si tn.NoteType=3 → Las TreasuryNote solo se consideran como reversiones de comprobante de egreso para el cálculo de bases e IVA; si con.HandlesRetention = 0 / apcn.ManageRetention = 0 → Solo se incluyen conceptos que NO manejan retención al calcular bases e IVA de CxP y Notas; si apdc.RateIva IS NULL AND apdc.BaseValue <> 0 → Se usa apdc.Value como base; en otro caso se usa apdc.BaseValue; si apdc.Detail = ''Detalle de cuenta por pagar generada por Comprobante de Entrada Costo Producto En Consignación'' → Se excluye ese detalle del cálculo de bases else Se incluye en el cálculo; si tax.Name IS NULL → Se etiqueta como ''Base No Gravada'' else Se etiqueta como ''Base '' + nombre del IVA', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportStatementPurchaseIVA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.CurrencyConverterWithDate', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportStatementPurchaseIVA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.Currency; Common.ISO4217; GeneralLedger.LegalBook; Treasury.VoucherTransactionDetails; Treasury.VoucherTransaction; Common.EconomicActivity; Common.ThirdPartyFiscalResponsibility; Common.FiscalResponsibility; Payments.AccountPayable; Common.ThirdParty; Payments.AccountPayableExchangeRate; GeneralLedger.JournalVouchers; GeneralLedger.JournalVoucherTypes; Payments.PaymentNotes; Common.Supplier; Payments.PaymentNotesAccountPayableAdvance; Payments.AccountPayableDetailConcept; Treasury.ExpenseConcepts; GeneralLedger.MainAccounts; Treasury.TreasuryNote; Payments.AccountPayableConcepts; GeneralLedger.GeneralLedgerIVA; Payments.PaymentsNoteDetails; Payments.AccountPayableConceptNotes', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportStatementPurchaseIVA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportStatementPurchaseIVA';
-- GO
