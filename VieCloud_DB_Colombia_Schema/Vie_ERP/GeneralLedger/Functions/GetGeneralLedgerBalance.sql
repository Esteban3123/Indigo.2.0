-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-02-17
-- Description:	Obtiene el balance hasta un periodo
-- =============================================
CREATE FUNCTION [GeneralLedger].[GetGeneralLedgerBalance]
(
	@LegalBookType INT,
	@LegalBookId INT,
	@LastYearClose INT,
	@Year INT,
	@InitialMonth INT,
	@FinalMonth INT,
	@IncludeParents BIT
)
RETURNS @GeneralLedgerBalance TABLE 
(
	MainAccountId INT NOT NULL, 
	MainAccountCode VARCHAR(20), 
	MainAccountName VARCHAR(100), 
	MainAccountNature TINYINT, 
	MainAccountAllowsMovement BIT, 
	MainAccountAvailability TINYINT, 
	MainAccountClassType TINYINT, 
	MainAccountClassNature TINYINT, 
	MainAccountClassCode VARCHAR(20), 
	MainAccountClassName VARCHAR(100), 
	MainAccountLevel INT, 
	-----------------------------------------------------------------------------------------------------------------
	ThirdPartyId INT, 
	CostCenterId INT, 
	-----------------------------------------------------------------------------------------------------------------
	ValueDebitInitial DECIMAL(18,2) NOT NULL, 
	ValueCreditInitial DECIMAL(18,2) NOT NULL, 
	-----------------------------------------------------------------------------------------------------------------
	ValueDebitMovement DECIMAL(18,2) NOT NULL, 
	ValueCreditMovement DECIMAL(18,2) NOT NULL
)
AS
BEGIN
	DECLARE @GeneralLedgerBalanceLegalBookId INT = @LegalBookId,
			@IsClosedLastYear BIT = IIf(@LastYearClose >= (@Year - 1), 1, 0)

	IF @LegalBookType = 3
	BEGIN
		SELECT @GeneralLedgerBalanceLegalBookId = Id
		FROM GeneralLedger.LegalBook WITH (NOLOCK)
		WHERE OfficialBook = 1 AND Status = 1
	END

	-----------------------------------------------------------------------------------------------------------------

	INSERT INTO @GeneralLedgerBalance
	SELECT	ma.Id, ma.Number, ma.Name, ma.Nature, ma.AllowsMovement, ma.Availability,
			mac.Type, mac.Nature, mac.Code, mac.Name, mal.Level,
			glb.IdThirdParty, glb.IdCostCenter,
			SUM(IIF(([Year] = @Year and [Month] >= @InitialMonth), 0, DebitValue)) AS InitialDebitValue,
			SUM(IIF(([Year] = @Year and [Month] >= @InitialMonth), 0, CreditValue)) AS InitialCreditValue,
			SUM(IIF(([Year] = @Year and [Month] >= @InitialMonth), DebitValue, 0)) AS DebitValue,
			SUM(IIF(([Year] = @Year and [Month] >= @InitialMonth), CreditValue, 0)) AS CreditValue
	FROM GeneralLedger.MainAccounts AS ma WITH (NOLOCK)
	JOIN GeneralLedger.MainAccountClasses AS mac WITH (NOLOCK) ON ma.IdAccountClass = mac.Id
	JOIN GeneralLedger.MainAccountLevels AS mal WITH (NOLOCK) ON ma.IdAccountLevel = mal.Id
	JOIN GeneralLedger.GeneralLedgerBalance glb WITH (NOLOCK) ON ma.Id = glb.IdMainAccount
	WHERE @GeneralLedgerBalanceLegalBookId = ma.LegalBookId AND
	(
		(
			(@IsClosedLastYear = 0 AND ((glb.[Year] = (@Year - 1) AND glb.[Month] < 13) OR (glb.[Year] = (@Year - 2) AND glb.[Month] = 14))) 
			OR 
			(@IsClosedLastYear = 1 AND  (glb.[Year] = (@Year - 1) AND glb.[Month] = 14))
		) 
		OR 
		(glb.[Year] = @Year AND glb.[Month] <= @FinalMonth)
	)
	GROUP BY ma.Id, ma.Number, ma.Name, ma.Nature, ma.AllowsMovement, ma.Availability,
		mac.Type, mac.Nature, mac.Code, mac.Name, mal.Level,
		glb.IdThirdParty, glb.IdCostCenter

	-----------------------------------------------------------------------------------------------------------------

	IF @LegalBookType = 3
	BEGIN
		UPDATE glb
			SET glb.MainAccountId = ma.Id, 
				glb.MainAccountCode = ma.Number, 
				glb.MainAccountName = ma.Name, 
				glb.MainAccountNature = ma.Nature, 
				glb.MainAccountAllowsMovement = ma.AllowsMovement, 
				glb.MainAccountAvailability = ma.Availability,
				glb.MainAccountClassType = mac.Type, 
				glb.MainAccountClassNature = mac.Nature, 
				glb.MainAccountClassCode = mac.Code, 
				glb.MainAccountClassName = mac.Name, 
				glb.MainAccountLevel = mal.Level
		FROM @GeneralLedgerBalance AS glb
		JOIN GeneralLedger.HomologationAccount ha WITH (NOLOCK) ON glb.MainAccountId = ha.OfficialMainAccountId
		JOIN GeneralLedger.MainAccounts AS ma WITH (NOLOCK) ON ha.MainAccountId = ma.Id
		JOIN GeneralLedger.MainAccountClasses AS mac WITH (NOLOCK) ON ma.IdAccountClass = mac.Id
		JOIN GeneralLedger.MainAccountLevels AS mal WITH (NOLOCK) ON ma.IdAccountLevel = mal.Id
		WHERE ma.LegalBookId = @LegalBookId
	END

	-------------------------------------------------------------------------------------------------------------------

	IF @IncludeParents = 1
	BEGIN
		INSERT INTO @GeneralLedgerBalance 
			SELECT	ma.Id, ma.Number, ma.Name, ma.Nature, ma.AllowsMovement, ma.Availability,
					mac.Type, mac.Nature, mac.Code, mac.Name, mal.Level,
					NULL, NULL,
					0, 0,
					0, 0
			FROM GeneralLedger.MainAccounts AS ma WITH (NOLOCK)
			JOIN GeneralLedger.MainAccountClasses AS mac WITH (NOLOCK) ON mac.Id = ma.IdAccountClass 
			JOIN GeneralLedger.MainAccountLevels AS mal WITH (NOLOCK) ON mal.Id = ma.IdAccountLevel 
			LEFT JOIN @GeneralLedgerBalance glb ON ma.Id = glb.MainAccountId
			WHERE ma.LegalBookId = @LegalBookId AND glb.MainAccountId IS NULL
	END

	RETURN
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de tabla que calcula el balance del libro mayor contable para un rango de meses y año específico, devolviendo los saldos iniciales y movimientos (débitos y créditos) por cuenta contable, tercero y centro de costo. Recibe como parámetros el tipo y identificador del libro legal, el año del último cierre, el año y rango de meses a consultar, e indicador de si se deben incluir cuentas padre sin movimiento. Integra las cuentas contables (MainAccounts) con su clasificación (MainAccountClasses) y nivel jerárquico (MainAccountLevels) contra los saldos registrados en GeneralLedger.GeneralLedgerBalance, aplicando lógica de cierre anual para determinar el saldo inicial acumulado. Cuando el tipo de libro es consolidado o de homologación (tipo 3), reasigna las cuentas oficiales a sus equivalentes homologadas, permitiendo la generación de balances de prueba, estados financieros y reportes contables oficiales.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'FUNCTION', @level1name = N'GetGeneralLedgerBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'FUNCTION', @level1name = N'GetGeneralLedgerBalance';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula el balance contable del libro mayor (saldos iniciales y movimientos del periodo) por cuenta, tercero y centro de costo hasta un mes/año dado, considerando cierre anual, homologación de libro oficial y opcionalmente cuentas padre sin movimiento.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'FUNCTION', @level1name=N'GetGeneralLedgerBalance';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un libro legal activo y oficial (OfficialBook=1, Status=1) cuando el tipo de libro es 3; Las cuentas contables deben estar asociadas a una clase y a un nivel del plan de cuentas; Para homologación, debe existir correspondencia en HomologationAccount entre la cuenta oficial y la cuenta del libro solicitado; Los saldos en GeneralLedgerBalance deben estar registrados con Year/Month coherentes (mes 1-12 movimientos, mes 14 cierre anual)', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'FUNCTION', @level1name=N'GetGeneralLedgerBalance';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El periodo de cierre anual se representa con Month=14 (saldo de cierre), distinto de los meses 1-12 de movimiento; Cuando el año anterior está cerrado, el saldo inicial proviene exclusivamente del registro de cierre (Month=14) del año anterior; Cuando el año anterior NO está cerrado, el saldo inicial se construye con los movimientos mensuales del año anterior más el cierre del año penúltimo; Los movimientos del periodo se filtran hasta FinalMonth inclusive del año solicitado; Para libros tipo 3 el cálculo se hace sobre el libro oficial activo y luego se homologan las cuentas hacia el libro solicitado; Los saldos se agrupan por cuenta, tercero y centro de costo; Cuando se incluyen padres, las cuentas sin movimiento se devuelven con saldos en cero (no se calculan acumulados jerárquicos)', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'FUNCTION', @level1name=N'GetGeneralLedgerBalance';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Libro mayor contable; Plan único de cuentas (PUC); Saldos iniciales y movimientos contables; Cierre contable anual (mes 14); Homologación de cuentas oficiales; Naturaleza débito/crédito; Tercero y centro de costo; Libros legales (oficial / auxiliar)', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'FUNCTION', @level1name=N'GetGeneralLedgerBalance';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] @GeneralLedgerBalance: Devuelve por cuenta/tercero/centro de costo: saldo inicial (debe/haber) acumulado de periodos anteriores al rango y movimientos (debe/haber) del rango [InitialMonth, FinalMonth] del año solicitado; [UPDATE] @GeneralLedgerBalance: Cuando LegalBookType=3, reemplaza los datos de cuenta del resultado por los de la cuenta homologada en el libro solicitado (HomologationAccount.OfficialMainAccountId → MainAccountId del libro destino); [INSERT] @GeneralLedgerBalance: Cuando IncludeParents=1, agrega al resultado todas las cuentas del libro (LegalBookId) que no aparecieron con saldo, con valores en cero', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'FUNCTION', @level1name=N'GetGeneralLedgerBalance';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Tipo de libro legal = 3 (libro oficial homologado) → Se sustituye el libro por el libro oficial activo (OfficialBook=1 AND Status=1) y posteriormente se homologan las cuentas resultantes contra el plan de cuentas del libro solicitado mediante HomologationAccount else Se usa directamente el libro indicado sin homologación; si Cierre del año anterior (LastYearClose >= Year-1) → Los saldos iniciales se toman del periodo de cierre del año anterior (Year-1, Month=14) else Los saldos iniciales se toman de los movimientos del año anterior (Month<13) más el cierre del año penúltimo (Year-2, Month=14); si Periodo (Year, Month) >= (Year solicitado, InitialMonth) → Los valores se acumulan como movimientos del periodo (DebitValue/CreditValue del movimiento) else Los valores se acumulan como saldo inicial (InitialDebitValue/InitialCreditValue); si IncludeParents = 1 → Se agregan al resultado las cuentas del plan de cuentas del libro que no tienen saldo, con valores en cero, para representar las cuentas padre/sin movimiento', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'FUNCTION', @level1name=N'GetGeneralLedgerBalance';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.LegalBook; GeneralLedger.MainAccounts; GeneralLedger.MainAccountClasses; GeneralLedger.MainAccountLevels; GeneralLedger.GeneralLedgerBalance; GeneralLedger.HomologationAccount', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'FUNCTION', @level1name=N'GetGeneralLedgerBalance';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'FUNCTION', @level1name=N'GetGeneralLedgerBalance';
GO
