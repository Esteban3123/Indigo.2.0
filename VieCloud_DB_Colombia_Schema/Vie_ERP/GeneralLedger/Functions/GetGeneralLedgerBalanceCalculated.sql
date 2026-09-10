-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-02-17
-- Description:	Obtiene el balance hasta un periodo, con los campos requeridos
-- =============================================
CREATE FUNCTION [GeneralLedger].[GetGeneralLedgerBalanceCalculated]
(	
	@LegalBookId INT,
	@Year INT,
	@InitialMonth INT,
	@FinalMonth INT,
	@IsTestBalance BIT,
	@CalculateResultExercise BIT,
	@MainAccountClassNature TINYINT,
	@MainAccountMemorandum BIT,
	@MainAccountInitial VARCHAR(MAX),
	@MainAccountFinal VARCHAR(MAX),
	@ThirdPartyInitial VARCHAR(MAX),
	@ThirdPartyFinal VARCHAR(MAX),
	@CostCenterInitial VARCHAR(MAX),
	@CostCenterFinal VARCHAR(MAX)
)
RETURNS @GeneralLedgerBalance TABLE 
(
	LegalBookName VARCHAR(200) NOT NULL,
	MainAccountId INT NOT NULL, 
	MainAccountCode VARCHAR(20) NOT NULL, 
	MainAccountName VARCHAR(100) NOT NULL, 
	MainAccountNature TINYINT NOT NULL, 
	MainAccountAllowsMovement BIT NOT NULL, 
	MainAccountAvailability TINYINT NOT NULL, 
	MainAccountClassType TINYINT NOT NULL, 
	MainAccountClassCode VARCHAR(20) NOT NULL, 
	MainAccountClassName VARCHAR(100) NOT NULL, 
	MainAccountLevel INT NOT NULL, 
	-----------------------------------------------------------------------------------------------------------------
	ThirdPartyId INT, 
	ThirdPartyNit VARCHAR(20), 
	ThirdPartyName VARCHAR(300), 
	CostCenterId INT, 
	CostCenterCode VARCHAR(20), 
	CostCenterName VARCHAR(200), 
	-----------------------------------------------------------------------------------------------------------------
	ValueDebitInitial DECIMAL(18,2) NOT NULL DEFAULT(0), 
	ValueCreditInitial DECIMAL(18,2) NOT NULL DEFAULT(0), 
	PreviousBalance DECIMAL(18,2) NOT NULL DEFAULT(0), 
	-----------------------------------------------------------------------------------------------------------------
	ValueDebitMovement DECIMAL(18,2) NOT NULL DEFAULT(0), 
	ValueCreditMovement DECIMAL(18,2) NOT NULL DEFAULT(0),
	NewBalance DECIMAL(18,2) NOT NULL DEFAULT(0),
	-----------------------------------------------------------------------------------------------------------------
	ResultExercise DECIMAL(18,2) NOT NULL DEFAULT(0)
)
AS
BEGIN
	/*****************************  VERIFICAMOS EL TIPO DE LIBRO *****************************/

	--Identificar si ya se cerro el año anterior al seleccionado
	DECLARE @LegalBookType TINYINT,
			@LastYearClose INT,
			@LegalBookName VARCHAR(200)

	SELECT	@LegalBookType = lb.TypeBook,
			@LegalBookName = lb.Name,
			@LastYearClose = lb.LastYearClose
	FROM GeneralLedger.LegalBook lb WITH (NOLOCK)
	WHERE lb.Id = @LegalBookId

	--Si es un libro de estados financieros consultamos el libro oficial
	IF @LegalBookType = 3
	BEGIN
		SELECT	@LastYearClose = lb.LastYearClose
		FROM GeneralLedger.LegalBook lb WITH (NOLOCK)
		WHERE lb.OfficialBook = 1 AND lb.Status = 1
	END

	/*********************** OBTENEMOS LOS REGISTROS CONTABLES DEL PERIODO ***********************/

	--Insertamos los movimientos del periodo
	INSERT INTO @GeneralLedgerBalance
		(
			LegalBookName,
			MainAccountId, MainAccountCode, MainAccountName, MainAccountNature, MainAccountAllowsMovement, MainAccountAvailability,
			MainAccountClassType, MainAccountClassCode, MainAccountClassName, MainAccountLevel,
			ThirdPartyId, ThirdPartyNit, ThirdPartyName,
			CostCenterId, CostCenterCode, CostCenterName,
			ValueDebitInitial, ValueCreditInitial, PreviousBalance,
			ValueDebitMovement, ValueCreditMovement, NewBalance
		)
		SELECT 	@LegalBookName,
				glb.MainAccountId, glb.MainAccountCode, glb.MainAccountName, glb.MainAccountNature, glb.MainAccountAllowsMovement, glb.MainAccountAvailability,
				glb.MainAccountClassType, glb.MainAccountClassCode, glb.MainAccountClassName, glb.MainAccountLevel, 
				-----------------------------------------------------------------------------------------------------------------
				tp.Id AS ThirdPartyId, tp.Nit AS ThirdPartyNit, tp.Name AS ThirdPartyName, 
				-----------------------------------------------------------------------------------------------------------------
				cc.Id AS CostCenterId, cc.Code AS CostCenterCode, cc.Name AS CostCenterName, 
				-----------------------------------------------------------------------------------------------------------------
				glb.ValueDebitInitial, glb.ValueCreditInitial, 
				((glb.ValueDebitInitial - glb.ValueCreditInitial) * IIF(glb.MainAccountNature = 1, 1, -1)) PreviousBalance, 
				-----------------------------------------------------------------------------------------------------------------
				glb.ValueDebitMovement, glb.ValueCreditMovement, 
				((glb.ValueDebitInitial - glb.ValueCreditInitial + glb.ValueDebitMovement - glb.ValueCreditMovement) * IIF(glb.MainAccountNature = 1, 1, -1)) NewBalance
		FROM [GeneralLedger].[GetGeneralLedgerBalance](@LegalBookType, @LegalBookId, @LastYearClose, @Year, @InitialMonth, @FinalMonth, 1) glb
		LEFT JOIN Common.ThirdParty AS tp WITH (NOLOCK) ON glb.ThirdPartyId  = tp.Id
		LEFT JOIN Payroll.CostCenter AS cc WITH (NOLOCK) ON glb.CostCenterId = cc.Id
		WHERE	(
					@IsTestBalance = 1
					OR
					(
						glb.MainAccountClassType = 1									-- Cuentas del Balance
						OR 
						(@MainAccountMemorandum = 1 AND glb.MainAccountClassType = 3)	-- Cuentas de Orden
					)
				)
				AND ISNULL(@MainAccountClassNature, glb.MainAccountClassNature) = glb.MainAccountClassNature
				AND glb.MainAccountCode BETWEEN ISNULL(@MainAccountInitial, '0') AND ISNULL(@MainAccountFinal, 'ZZZZZZZZZZZZZZZZZZZZ')
				AND ISNULL(tp.Nit, '0') BETWEEN ISNULL(@ThirdPartyInitial, '0') AND ISNULL(@ThirdPartyFinal, 'ZZZZZZZZZZZZZZZZZZZZ')
				AND ISNULL(cc.Code, '0') BETWEEN ISNULL(@CostCenterInitial, '0') AND ISNULL(@CostCenterFinal, 'ZZZZZZZZZZZZZZZZZZZZ')

	/***************************  CALCULAMOS LA UTILIDAD O PERDIDA ***************************/
    
	IF @CalculateResultExercise = 1 AND ISNULL(@MainAccountClassNature, 2) = 2
	BEGIN
		DECLARE @MainAccountDeficitId INT,
				@MainAccountSuperAvitId INT,
				@ResultExercise DECIMAL(18,2) = 0
		
		SELECT	@MainAccountDeficitId = IdDeficitAccount, 
				@MainAccountSuperAvitId = IdSuperavitAccount 
		FROM GeneralLedger.GeneralLedgerSettings WITH (NOLOCK)

		IF @MainAccountClassNature IS NULL
		BEGIN
			SELECT	@ResultExercise = SUM(glb.ValueDebitInitial - glb.ValueCreditInitial + glb.ValueDebitMovement - glb.ValueCreditMovement)
			FROM @GeneralLedgerBalance glb
			WHERE glb.MainAccountClassType = 1
		END
		ELSE
		BEGIN
			SELECT	@ResultExercise = SUM(glb.ValueDebitInitial - glb.ValueCreditInitial + glb.ValueDebitMovement - glb.ValueCreditMovement)
			FROM [GeneralLedger].[GetGeneralLedgerBalance](@LegalBookType, @LegalBookId, @LastYearClose, @Year, @InitialMonth, @FinalMonth, 1) glb
			WHERE glb.MainAccountClassType = 1
		END

		SET @ResultExercise = ISNULL(@ResultExercise, 0)

		IF EXISTS (SELECT 1 FROM @GeneralLedgerBalance WHERE MainAccountId = IIF(@ResultExercise > 0, @MainAccountSuperAvitId, @MainAccountDeficitId) )
		BEGIN
			UPDATE TOP(1) @GeneralLedgerBalance
				SET ValueDebitMovement += IIF((@ResultExercise > 0), 0, ABS(@ResultExercise)),
					ValueCreditMovement += IIF((@ResultExercise > 0), ABS(@ResultExercise), 0), 
					newBalance += @ResultExercise 
			WHERE MainAccountId = IIF(@ResultExercise > 0, @MainAccountSuperAvitId, @MainAccountDeficitId)
		END
		ELSE BEGIN
			INSERT INTO @GeneralLedgerBalance
				(
					LegalBookName,
					MainAccountId, MainAccountCode, MainAccountName, MainAccountNature, MainAccountAllowsMovement, MainAccountAvailability,
					MainAccountClassType, MainAccountClassCode, MainAccountClassName, MainAccountLevel,
					ValueDebitMovement, ValueCreditMovement, NewBalance
				)
				SELECT	@LegalBookName,
						ma.Id, ma.Number, ma.Name, ma.Nature, ma.AllowsMovement, ma.Availability,
						mac.Type, mac.Code, mac.Name, mal.Level,
						IIF((@ResultExercise > 0), 0, @ResultExercise), IIF((@ResultExercise > 0), @ResultExercise, 0), @ResultExercise
				FROM GeneralLedger.MainAccounts ma WITH (NOLOCK)
				JOIN GeneralLedger.MainAccountClasses mac WITH (NOLOCK) on mac.Id = ma.IdAccountClass 
				JOIN GeneralLedger.MainAccountLevels mal WITH (NOLOCK) on mal.Id = ma.IdAccountLevel
				WHERE ma.Id = IIF((@ResultExercise > 0), @MainAccountSuperAvitId, @MainAccountDeficitId)
					AND ma.LegalBookId = @LegalBookId
		END

		----Actualizamos el resultado del ejercicio
		UPDATE @GeneralLedgerBalance
			SET ResultExercise = @ResultExercise
	END

	/******************************* MAYORIZAMOS LOS REGISTROS *******************************/

	-- Mayorizamos los saldos
	UPDATE ste 
		SET ste.valueDebitInitial = te.valueDebitInitial,
			ste.valueCreditInitial = te.valueCreditInitial,
			ste.PreviousBalance = (te.ValueDebitInitial - te.ValueCreditInitial) * IIF(ste.MainAccountNature = 1, 1, -1),
			ste.valueDebitMovement = ISNULL(te.valueDebitMovement, 0),
			ste.valueCreditMovement = ISNULL(te.valueCreditMovement, 0),
			ste.NewBalance = (te.ValueDebitInitial - te.ValueCreditInitial + te.valueDebitMovement - te.valueCreditMovement) * IIF(ste.MainAccountNature = 1, 1, -1)
	FROM @GeneralLedgerBalance ste
	JOIN
	(
		SELECT
			ste.mainAccountId,
			SUM(te.valueDebitInitial) valueDebitInitial,
			SUM(te.valueCreditInitial) valueCreditInitial,
			SUM(te.valueDebitMovement) valueDebitMovement,
			SUM(te.valueCreditMovement) valueCreditMovement
		FROM @GeneralLedgerBalance ste
		JOIN
		(
			SELECT
				te.mainAccountCode,
				SUM(ISNULL(te.valueDebitInitial,0)) valueDebitInitial,
				SUM(ISNULL(te.valueCreditInitial,0)) valueCreditInitial,
				SUM(ISNULL(te.valueDebitMovement,0)) valueDebitMovement,
				SUM(ISNULL(te.valueCreditMovement,0)) valueCreditMovement
			FROM @GeneralLedgerBalance AS te
			WHERE te.MainAccountAllowsMovement = 1
			GROUP BY te.mainAccountCode
		) te ON te.mainAccountCode LIKE (ste.mainAccountCode + '%')
		WHERE ste.MainAccountAllowsMovement = 0
		GROUP BY ste.mainAccountId
	) te ON ste.mainAccountId = te.mainAccountId

	RETURN
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de tabla que calcula el balance del libro mayor contable para un libro legal (oficial, NIIF u otro) en un rango de meses y año determinados. Consolida los saldos iniciales de débito y crédito, los movimientos del período y el nuevo saldo resultante, aplicando la naturaleza de cada cuenta (débito o crédito) para presentar el balance con signo correcto. Permite filtrar por rango de cuentas contables, terceros (NIT) y centros de costo, y opcionalmente calcula el resultado del ejercicio (utilidad o pérdida). Se utiliza para generar balances de prueba, estados de situación financiera y reportes contables del módulo de Contabilidad General.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'FUNCTION', @level1name = N'GetGeneralLedgerBalanceCalculated';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'FUNCTION', @level1name = N'GetGeneralLedgerBalanceCalculated';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula el balance contable mayorizado de un libro hasta un periodo dado, incluyendo saldos iniciales, movimientos, saldo nuevo y opcionalmente la utilidad/pérdida del ejercicio.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'FUNCTION', @level1name=N'GetGeneralLedgerBalanceCalculated';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El libro contable debe existir en GeneralLedger.LegalBook.; Si se calcula resultado del ejercicio, deben estar configuradas las cuentas de déficit y superávit en GeneralLedger.GeneralLedgerSettings.; Las cuentas de déficit/superávit configuradas deben pertenecer al libro contable indicado.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'FUNCTION', @level1name=N'GetGeneralLedgerBalanceCalculated';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El saldo (PreviousBalance/NewBalance) se calcula multiplicando (débito - crédito) por +1 si la naturaleza de la cuenta es 1 (débito) o por -1 en caso contrario.; Las cuentas que no permiten movimiento (AllowsMovement=0) son saldos mayorizados a partir de sus cuentas hijas que sí permiten movimiento.; La mayorización agrupa por prefijo de código (jerarquía del PUC).; La utilidad/pérdida solo se calcula sobre cuentas con MainAccountClassType=1 (cuentas de balance).; Los rangos nulos de filtro se interpretan como ''0'' (mínimo) y ''ZZZZ...'' (máximo).; Solo se actualiza una sola fila (TOP 1) al ajustar el resultado del ejercicio en una cuenta existente.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'FUNCTION', @level1name=N'GetGeneralLedgerBalanceCalculated';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Libro contable (legal book); Balance de prueba; Cuentas de balance; Cuentas de orden / memorando; Plan de cuentas (PUC); Naturaleza débito/crédito; Tercero (NIT); Centro de costo; Mayorización contable; Utilidad/pérdida del ejercicio; Superávit / déficit; Cierre de año contable; Estados financieros', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'FUNCTION', @level1name=N'GetGeneralLedgerBalanceCalculated';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @GeneralLedgerBalance: Inserta movimientos del periodo filtrando: si no es balance de prueba, solo cuentas de balance (ClassType=1) o de orden (ClassType=3 cuando se incluyen memorando); además aplica filtros por naturaleza de clase, rango de cuenta, rango de NIT y rango de centro de costo.; [INSERT] @GeneralLedgerBalance: Cuando se calcula resultado del ejercicio y la cuenta de superávit/déficit correspondiente NO existe en el balance, inserta una nueva fila con la cuenta de superávit (si resultado > 0) o de déficit (si resultado <= 0) con el valor del ejercicio en débito o crédito según signo.; [UPDATE] @GeneralLedgerBalance: Cuando se calcula resultado del ejercicio y la cuenta de superávit/déficit YA existe en el balance, actualiza TOP(1) sumando el resultado: si es positivo va al crédito (superávit), si es negativo va al débito (déficit) en valor absoluto, y suma el resultado al NewBalance.; [UPDATE] @GeneralLedgerBalance: Asigna el valor del resultado del ejercicio (ResultExercise) a todas las filas cuando @CalculateResultExercise = 1 y la naturaleza de clase es nula o 2.; [UPDATE] @GeneralLedgerBalance: Mayoriza saldos: las cuentas que no permiten movimiento (MainAccountAllowsMovement=0) reciben la suma de débitos/créditos iniciales y de movimiento de las cuentas hijas (cuyo código empieza por el código de la cuenta padre) que sí permiten movimiento, recalculando PreviousBalance y NewBalance multiplicando por +1 o -1 según naturaleza débito/crédito.; [RETURN_RESULT] @GeneralLedgerBalance: Retorna la tabla con balance mayorizado: saldo previo, movimientos del periodo, saldo nuevo y resultado del ejercicio por cuenta/tercero/centro de costo.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'FUNCTION', @level1name=N'GetGeneralLedgerBalanceCalculated';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Tipo de libro = 3 (estados financieros) → Se toma el LastYearClose del libro oficial activo (OfficialBook=1, Status=1) en lugar del libro indicado. else Se usa el LastYearClose del libro consultado.; si @IsTestBalance = 1 → Incluye todas las cuentas sin restricción de tipo de clase. else Solo incluye cuentas de balance (ClassType=1) y, si @MainAccountMemorandum=1, también cuentas de orden (ClassType=3).; si @CalculateResultExercise = 1 y (@MainAccountClassNature es NULL o = 2) → Calcula utilidad/pérdida del ejercicio y la registra en la cuenta de superávit o déficit. else No se calcula resultado del ejercicio.; si @MainAccountClassNature IS NULL al calcular resultado → Suma los valores desde la tabla temporal ya filtrada (ClassType=1). else Recalcula desde GetGeneralLedgerBalance original (sin el filtro de naturaleza aplicado a la tabla temporal) para obtener el resultado total.; si Resultado del ejercicio > 0 → Se registra como superávit (crédito) en la cuenta @MainAccountSuperAvitId. else Se registra como déficit (débito) en la cuenta @MainAccountDeficitId.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'FUNCTION', @level1name=N'GetGeneralLedgerBalanceCalculated';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'GeneralLedger.GetGeneralLedgerBalance', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'FUNCTION', @level1name=N'GetGeneralLedgerBalanceCalculated';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.LegalBook; GeneralLedger.GetGeneralLedgerBalance; Common.ThirdParty; Payroll.CostCenter; GeneralLedger.GeneralLedgerSettings; GeneralLedger.MainAccounts; GeneralLedger.MainAccountClasses; GeneralLedger.MainAccountLevels', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'FUNCTION', @level1name=N'GetGeneralLedgerBalanceCalculated';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'FUNCTION', @level1name=N'GetGeneralLedgerBalanceCalculated';
GO
