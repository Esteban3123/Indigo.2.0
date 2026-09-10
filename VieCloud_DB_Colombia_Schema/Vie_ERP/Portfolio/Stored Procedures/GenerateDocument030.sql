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
				(   
					SELECT MAX(CONVERT(DATE, dc.DocumentDate, 20))
					FROM Glosas.GlosaDevolutionsReceptionD dd
					JOIN Glosas.GlosaDevolutionsReceptionC dc ON dc.Id = dd.GlosaDevolutionsReceptionCId
					WHERE dd.InvoiceNumber = ar.InvoiceNumber
				) DevolutionDate,
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera y procesa los datos requeridos por la Circular 030 de cartera para un trimestre y año específicos, cumpliendo con la normativa de reporte a superintendencias o entes de control. Calcula las fechas de inicio y fin del trimestre seleccionado, valida que el período no sea igual o posterior al actual, y obtiene el NIT de la empresa desde los parámetros generales de contabilidad (GeneralLedgerSettings y ThirdParty). Crea o actualiza el encabezado del período en Circular030 y regenera el detalle de facturas en Circular030Detail, consultando la antigüedad de cartera mediante GetAccountReceivableByAge_030 para registrar valores de factura, pagos, glosas y saldos por cobrar a EPS/aseguradoras. Se usa para producir el archivo (TXT y Excel) exigido por la normativa de reporte trimestral de cartera institucional.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'GenerateDocument030';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'GenerateDocument030';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte trimestral de cartera exigido por la Circular 030, consolidando facturas por deudor con sus pagos, glosas y saldos a partir de la cartera por edades del trimestre indicado.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'GenerateDocument030';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El trimestre solicitado debe ser estrictamente anterior al actual: @EndDate < @CurrentDate (de lo contrario lanza error 50001 ''El trimestre seleccionado es igual o mayor al actual.''); Debe existir al menos un registro en GeneralLedger.GeneralLedgerSettings (sino lanza 50001 ''No existe parámetros generales de contabilidad.''); El trimestre se interpreta con fechas fijas: T1=1/1-31/3, T2=1/4-30/6, T3=1/7-30/9, T4=1/10-31/12; Solo se reprocesa/genera el detalle si NO existen cabeceras Circular030 de períodos posteriores (mismo año con trimestre mayor o años posteriores)', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'GenerateDocument030';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'PaymentType siempre se reporta como ''F'' (factura); IdentificationTypeIPS_EPSS siempre es ''NI'' y IdentificationNumberIPS_EPSS es el NIT del tercero DIAN configurado en GeneralLedgerSettings; RegisterType de salida siempre es 2; InvoiceNumber se normaliza removiendo espacios, guiones y ceros a la izquierda; InvoicePrefix se obtiene como la parte alfabética anterior al primer dígito; TotalValuePayments = TransferValue+CashReceiptValue+CrossingValue + saldos de pagos del OpeningBalance (cuando aplica); ObjectionValue = (CreditValue-DebitValue) + glosas del OpeningBalance (cuando aplica); ObjectionWithAnswer se inserta siempre en 0; DevolutionDate se inserta siempre como NULL; RadicateDate usa RadicatedDate y, si es nula, AccountReceivableDate; Solo se reportan facturas con régimen perteneciente a los regímenes habilitados de Circular 030; El reproceso de un período es idempotente: borra el detalle previo antes de re-insertar; No se reprocesa un trimestre si ya existe información de trimestres posteriores', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'GenerateDocument030';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Portfolio.Circular030: Si no existen períodos posteriores y no hay cabecera para (@Year,@Trimester), inserta una nueva cabecera con CreationUser=@User y CreationDate=Common.GETDATE(); el Id queda en @Circular030Id vía SCOPE_IDENTITY(); [DELETE] Portfolio.Circular030Detail: Antes de regenerar, borra todos los detalles existentes del período actual: DELETE WHERE Circular030Id=@Circular030Id; [INSERT] Portfolio.Circular030Detail: Inserta un detalle por cada factura obtenida de Portfolio.GetAccountReceivableByAge_030(@InitialDate,@EndDate) que cumpla: (AccountReceivableType=2 AND PortfolioStatus>=3) OR (PortfolioStatus>=1 AND existe en Glosas.GlosaDevolutionsReceptionD); RegimenCalculated en {Contributivo, EPS Subsidiado, Vinculados Municipios, Vinculados - Departamentos, Atencion con Cargo Sub a la oferta}; y (Balance<>0 OR InPeriod=1); [UPDATE] Portfolio.Circular030Detail: Tras la inserción, pone InvoiceBalance=0 en facturas del período actual cuya factura equivalente (mismo AccountReceivableId) en el período anterior tenía InvoiceBalance>0 y en el actual ya quedó en 0; [RETURN_RESULT] Portfolio.Circular030Detail: Devuelve el reporte final uniendo cabecera y detalle del @Circular030Id, con RegisterType=2 y consecutivo ROW_NUMBER() ordenado por D.Id, ordenado por NameERP; [RAISERROR] Portfolio.Circular030Detail: THROW 50001 si el trimestre solicitado es igual o mayor al actual, o si no existen parámetros generales de contabilidad', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'GenerateDocument030';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Trimester=1..4 (CASE) → Calcula @InitialDate y @EndDate como los límites del trimestre calendario correspondiente; si (@Trimester-1) < 1 → El trimestre anterior se ajusta a 4 y @PreviousYear se decrementa en 1 else @PreviousTrimester=@Trimester-1, @PreviousYear=@Year; si @EndDate >= @CurrentDate → Lanza THROW 50001 y aborta; si NOT EXISTS en GeneralLedger.GeneralLedgerSettings → Lanza THROW 50001 y aborta; si NOT EXISTS Circular030 con (Year=@Year AND Trimester>@Trimester) OR Year>@Year → Procede a (re)generar el detalle del trimestre else No regenera; solo ejecuta el UPDATE de saldos y la consulta final; si ISNULL(@Circular030Id,0)=0 → Crea la cabecera Circular030 del período; si tp.StateEnterpriseType IN (0,1,2,3) → Mapea IdentificationTypeERP a ''NI'',''MU'',''DE'',''DI'' respectivamente; cualquier otro valor se considera ''NI''; si StateEnterpriseType=0 → IdentificationNumberERP = tp.Nit else IdentificationNumberERP = ISNULL(tp.CodeDivipola, tp.Nit); si Existe detalle previo del mismo AccountReceivableId en @PreviousCircular030Id → UpdateIndicator=''A'' (actualización) else UpdateIndicator=''I'' (inserción); si ar.PortfolioStatus = 16 → InvoiceJudicialRecovery=1 y JudicialRecoveryStatus=1 (en cobro jurídico) else Ambos en 0', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'GenerateDocument030';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'GenerateDocument030';
-- GO
