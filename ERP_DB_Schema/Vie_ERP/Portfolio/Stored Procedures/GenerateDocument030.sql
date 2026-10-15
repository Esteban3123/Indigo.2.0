-- =============================================
-- Author:		Juan Fernando Tamayo
-- Create DATE: 2016-12-26
-- Description:	Procesa y genera los datos requeridos por la circular 030 para un trimestre dado
-- =============================================
CREATE PROCEDURE [Portfolio].[GenerateDocument030]
	@Year INT, 
	@Trimester TINYINT, 
	@CurrentDate DATE, 
	@User VARCHAR(20)
AS
BEGIN	
	SET NOCOUNT ON

	--Se declaran las variables necesarias para el proceso
	DECLARE @Circular030Id INT, --Id de la cabecera
			@InitialDate DATE, --Fecha inicial del trimestre
			@EndDate DATE, --Fecha final del trimestre
			------------------------------
			@PreviousCircular030Id INT, --Id de la cabecera del trimestre anterior
			@PreviousYear INT, --Año del trimestre anterior
			@PreviousTrimester TINYINT, --Trimestre anterior al seleccionado			
			------------------------------			
			@CompanyThirdPartyNit VARCHAR(20) = '', --Nit del tercero de la empresa con la cual se loguea
			@RegisterType INT = 2 --Se declara esta variable para devolver y que sea visualizado tanto en el txt como en el excel

	--se declara una tabla temporal para guardar los datos de la funcion GetAccountReceivableByAge para optimizar la consulta
	CREATE TABLE #AccountReceivableByAgeTemp (
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Circular030Id] [int] NOT NULL,
		[AccountReceivableId] [int] NULL,
		[IdentificationTypeERP] [varchar](2) NOT NULL,
		[IdentificationNumberERP] [varchar](25) NOT NULL,
		[NameERP] [varchar](250) NOT NULL,
		[IdentificationTypeIPS_EPSS] [varchar](2) NOT NULL,
		[IdentificationNumberIPS_EPSS] [varchar](12) NOT NULL,
		[PaymentType] [varchar](1) NOT NULL,
		[InvoicePrefix] [varchar](6) NULL,
		[InvoiceNumber] [varchar](20) NOT NULL,
		[UpdateIndicator] [varchar](1) NOT NULL,
		[InvoiceValue] [decimal](18, 2) NOT NULL,
		[InvoiceDate] [date] NOT NULL,
		[RadicateDate] [date] NOT NULL,
		[DevolutionDate] [date] NULL,
		[TotalValuePayments] [decimal](18, 2) NOT NULL,
		[ObjectionValue] [decimal](18, 2) NOT NULL,
		[ObjectionWithAnswer] [bit] NOT NULL,
		[InvoiceBalance] [decimal](18, 2) NOT NULL,
		[InvoiceJudicialRecovery] [bit] NOT NULL,
		[JudicialRecoveryStatus] [tinyint] NOT NULL
	)

	CREATE TABLE #AccountReceivableByAgeFunction (
    Id INT PRIMARY KEY,
    OperatingUnitId INT,
    InvoiceCategoryId INT,
    CareGroupId INT,
    ContractId INT,
    ThirdPartyId INT,
    InvoiceNumber NVARCHAR(50),
    AdmissionNumber NVARCHAR(50),
    AccountReceivableDate DATE,
    AccountReceivableType INT,
    NumberShares INT,
    Term INT,
    OpeningBalance DECIMAL(18,2),
    PortfolioStatus INT,
    PortfolioStatusName NVARCHAR(50),
    RadicatedConsecutive NVARCHAR(50),
    RadicatedUser NVARCHAR(100),
    RadicatedDate DATE,
    RadicatedState NVARCHAR(50),
    RegimenCalculated NVARCHAR(100),
    AccountWithoutRadicateNumber NVARCHAR(50),
    AccountWithoutRadicateId INT,
    AccountRadicateId INT,
    AccountHardCollectionId INT,
    MainAccountId INT,
    DocumentValue DECIMAL(18,2),
    RetentionValue DECIMAL(18,2),
    InitialValue DECIMAL(18,2),
    DebitValue DECIMAL(18,2),
    CreditValue DECIMAL(18,2),
    TransferValue DECIMAL(18,2),
    CashReceiptValue DECIMAL(18,2),
    CrossingValue DECIMAL(18,2),
    Balance DECIMAL(18,2),
    CurrentBalance DECIMAL(18,2),
    InPeriod BIT,
    CurrencyId INT,
    CurrencyName NVARCHAR(50)
);

	/*************************************************  ASIGNACIONES *************************************************/

	--Obtenemos la fecha final de consulta según el trimestre y año enviados
	SELECT	@InitialDate =	CASE @Trimester
							WHEN 1 THEN DATEFROMPARTS(@Year, 1, 1)
							WHEN 2 THEN DATEFROMPARTS(@Year, 4, 1)
							WHEN 3 THEN DATEFROMPARTS(@Year, 7, 1)
							WHEN 4 THEN DATEFROMPARTS(@Year, 10, 1)
						END,
			@EndDate =	CASE @Trimester
							WHEN 1 THEN DATEFROMPARTS(@Year, 3, 31)
							WHEN 2 THEN DATEFROMPARTS(@Year, 6, 30)
							WHEN 3 THEN DATEFROMPARTS(@Year, 9, 30)
							WHEN 4 THEN DATEFROMPARTS(@Year, 12, 31)
						END

	--Obtenemos el trimestre anterior al seleccionado
	SET @PreviousYear = @Year
	SET @PreviousTrimester = (@Trimester - 1)
	IF @PreviousTrimester < 1 
	BEGIN 
		SET @PreviousTrimester = 4
		SET @PreviousYear -= 1
	END

	--Se obtiene el id y nit del tercero
	SELECT	@CompanyThirdPartyNit = t.Nit
	FROM GeneralLedger.GeneralLedgerSettings gls
	JOIN Common.ThirdParty t ON t.Id = gls.IdDian

	--Se obtiene el Id de la cabecera del periodo actual
	SELECT @Circular030Id = Id 
	FROM Portfolio.Circular030
	WHERE Year = @Year AND Trimester = @Trimester

	--Se obtiene el Id de la cabecera del periodo actual
	SELECT @PreviousCircular030Id = Id 
	FROM Portfolio.Circular030 H 
	WHERE Year = @PreviousYear AND Trimester = @PreviousTrimester

	/*************************************************  VALIDACIONES *************************************************/

	--Validamos que el trimestre seleccionado no sea mayor o igual al actual
	IF @EndDate >= @CurrentDate 
	BEGIN
		;THROW 50001, 'El trimestre seleccionado es igual o mayor al actual.', 1
	END

	--Se valida que haya registro en los parámetros generales de contabilidad
	IF NOT EXISTS (SELECT 1 FROM GeneralLedger.GeneralLedgerSettings)
	BEGIN
		;THROW 50001, 'No existe parámetros generales de contabilidad.', 1
	END

	/**************************************************** PROCESO ****************************************************/

	--Si no existen datos en los proximos trimestres generamos la información del trimestre
	IF NOT EXISTS (SELECT 1 FROM Portfolio.Circular030 WHERE ((Year = @Year AND Trimester > @Trimester) OR (Year > @Year)))
	BEGIN
		IF ISNULL(@Circular030Id, 0) = 0 
		BEGIN
			--Se generan e insertan los datos iniciales
			INSERT INTO Portfolio.Circular030 (Year, Trimester, CreationUser, CreationDate) 
			VALUES (@Year, @Trimester, @User, [Common].[GETDATE]())
			
			SET @Circular030Id = SCOPE_IDENTITY()
		END

		--Borramos lo generado
		DELETE FROM Portfolio.Circular030Detail WHERE Circular030Id = @Circular030Id	
		--Incertamos en la tabla temporal los valores de la funcion GetAccountReceivableByAge
		INSERT INTO #AccountReceivableByAgeFunction
		SELECT * FROM [Portfolio].[GetAccountReceivableByAge_030](@InitialDate, @EndDate)
		
		--Insertamos los detalles a la tabla temporal
		INSERT INTO #AccountReceivableByAgeTemp
		(
			Circular030Id, IdentificationTypeERP, IdentificationNumberERP, NameERP, IdentificationTypeIPS_EPSS, IdentificationNumberIPS_EPSS, PaymentType, 
			AccountReceivableId, InvoicePrefix, InvoiceNumber, UpdateIndicator, InvoiceValue, InvoiceDate, RadicateDate, DevolutionDate, 
			TotalValuePayments, ObjectionValue, ObjectionWithAnswer, InvoiceBalance, InvoiceJudicialRecovery, JudicialRecoveryStatus
		) 
		SELECT @Circular030Id Circular030Id,				
				CASE tp.StateEnterpriseType 
					WHEN 0 THEN 'NI' 
					WHEN 1 THEN 'MU' 
					WHEN 2 THEN 'DE' 
					WHEN 3 THEN 'DI' 
					ELSE 'NI' 
				END IdentificationTypeERP,
				CASE tp.StateEnterpriseType 
					WHEN 0 THEN tp.Nit 
					ELSE ISNULL(tp.CodeDivipola, tp.Nit) 
				END IdentificationNumberERP,
				tp.Name NameERP,
				'NI' IdentificationTypeIPS_EPSS,
				@CompanyThirdPartyNit IdentificationNumberIPS_EPSS,
				'F' PaymentType,
				ar.Id AccountReceivableId,
				SUBSTRING(ar.InvoiceNumber, 1, (PATINDEX('%[0-9]%', ar.InvoiceNumber) - 1)) InvoicePrefix,
				REPLACE(LTRIM(REPLACE(dbo.udf_GetNumeric(REPLACE(REPLACE(ar.InvoiceNumber, ' ', ''), '-', '')),'0',' ')),' ','0') InvoiceNumber,
				IIF
				(
					cd.Id IS NULL,
					'I',
					'A'
				) UpdateIndicator,
				ar.DocumentValue InvoiceValue,
				CONVERT(DATE, ar.AccountReceivableDate,20) InvoiceDate,
				CONVERT(DATE, ISNULL(ar.RadicatedDate,ar.AccountReceivableDate), 20) RadicateDate, 
				NULL DevolutionDate,
				(ar.TransferValue + ar.CashReceiptValue + ar.CrossingValue) + isnull(ob.TotalValuePayments,0) TotalValuePayments,
				(ar.CreditValue - ar.DebitValue) + isnull(ob.TotalGlosaValue,0) ObjectionValue,
				0 ObjectionWithAnswer,
				ar.Balance InvoiceBalance,
				CASE ar.PortfolioStatus WHEN 16 then 1 else 0 END InvoiceJudicialRecovery,
				CASE ar.PortfolioStatus WHEN 16 then 1 else 0 END JudicialRecoveryStatus
		FROM #AccountReceivableByAgeFunction AS ar
		JOIN Common.ThirdParty tp WITH (NOLOCK) ON ar.ThirdPartyId = tp.Id
		LEFT JOIN Portfolio.Circular030Detail cd WITH (NOLOCK) ON cd.Circular030Id = @PreviousCircular030Id AND ar.Id = cd.AccountReceivableId
		LEFT JOIN Portfolio.OpeningBalanceCircularZeroThirty ob with(nolock) on ar.InvoiceNumber = ob.InvoiceNumber and ar.OpeningBalance =1
		LEFT JOIN Glosas.GlosaDevolutionsReceptionD d ON d.InvoiceNumber = ar.InvoiceNumber
		WHERE
			(
				(ar.AccountReceivableType = 2 AND ar.PortfolioStatus >= 3)
				OR
				(ar.PortfolioStatus >= 1 AND d.Id IS NOT NULL)
			)
			AND ISNULL(ar.RegimenCalculated, '') IN 
			(
				'Contributivo', 
				'EPS Subsidiado', 
				'Vinculados Municipios', 
				'Vinculados - Departamentos', 
				'Atencion con Cargo Sub a la oferta'
			)
			AND
			(
				ar.Balance <> 0 OR ar.InPeriod = 1
			)			
	END	

	--Insertamos los detalles a la tabla temporal
	INSERT into Portfolio.Circular030Detail (
		Circular030Id, IdentificationTypeERP, IdentificationNumberERP, NameERP, IdentificationTypeIPS_EPSS, IdentificationNumberIPS_EPSS, PaymentType, 
		AccountReceivableId, InvoicePrefix, InvoiceNumber, UpdateIndicator, InvoiceValue, InvoiceDate, RadicateDate, DevolutionDate, 
		TotalValuePayments, ObjectionValue, ObjectionWithAnswer, InvoiceBalance, InvoiceJudicialRecovery, JudicialRecoveryStatus
	) 
	select 
		Circular030Id, IdentificationTypeERP, IdentificationNumberERP, NameERP, IdentificationTypeIPS_EPSS, IdentificationNumberIPS_EPSS, PaymentType, 
		AccountReceivableId, InvoicePrefix, InvoiceNumber, UpdateIndicator, InvoiceValue, InvoiceDate, RadicateDate, DevolutionDate, 
		TotalValuePayments, ObjectionValue, ObjectionWithAnswer, InvoiceBalance, InvoiceJudicialRecovery, JudicialRecoveryStatus
	from #AccountReceivableByAgeTemp GROUP BY Circular030Id, IdentificationTypeERP, IdentificationNumberERP, NameERP, IdentificationTypeIPS_EPSS, IdentificationNumberIPS_EPSS, PaymentType, 
		AccountReceivableId, InvoicePrefix, InvoiceNumber, UpdateIndicator, InvoiceValue, InvoiceDate, RadicateDate, DevolutionDate, 
		TotalValuePayments, ObjectionValue, ObjectionWithAnswer, InvoiceBalance, InvoiceJudicialRecovery, JudicialRecoveryStatus

	DROP TABLE #AccountReceivableByAgeFunction
	DROP TABLE #AccountReceivableByAgeTemp

	-- Actualizamos las facturas que en el trimestre anterior tenían saldo y en el trimestre actual el saldo es 0
	UPDATE D
	SET D.InvoiceBalance = 0 
	FROM Portfolio.Circular030Detail D
	JOIN Portfolio.Circular030 H ON H.Id = D.Circular030Id
	JOIN Portfolio.Circular030Detail PrevD ON PrevD.Circular030Id = @PreviousCircular030Id
	    AND D.AccountReceivableId = PrevD.AccountReceivableId
	WHERE PrevD.InvoiceBalance > 0 -- Facturas con saldo en el trimestre anterior
	AND D.InvoiceBalance = 0 -- Facturas que en este trimestre tienen saldo 0
	AND H.Year = @Year
	AND H.Trimester = @Trimester;

	/*************************************************** RESULTADO ***************************************************/

	SELECT @RegisterType RegisterType, 
			ROW_NUMBER() OVER(ORDER BY D.Id) Consecutive, 
			H.Year, H.Trimester, H.CreationUser, H.CreationDate, H.ModificationUser, H.ModificationDate,
			D.IdentificationTypeERP, D.IdentificationNumberERP, D.NameERP, D.IdentificationTypeIPS_EPSS, D.IdentificationNumberIPS_EPSS, 
			D.PaymentType, D.InvoicePrefix, D.InvoiceNumber, D.UpdateIndicator, D.InvoiceValue, D.InvoiceDate, D.RadicateDate, D.DevolutionDate, 
			D.TotalValuePayments, D.ObjectionValue, D.ObjectionWithAnswer, D.InvoiceBalance, D.InvoiceJudicialRecovery, D.JudicialRecoveryStatus
	FROM Portfolio.Circular030 H 
	JOIN Portfolio.Circular030Detail D ON h.Id=d.Circular030Id
	WHERE H.Id = @Circular030Id 
	ORDER BY d.NameERP	
END