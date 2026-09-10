-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2021-04-12
-- Description:	Procedimiento para el reporte de saldos por tercero
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ReportThirdPartyBalance]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @Year INT,
			@Month INT,
			@LegalBookId INT,
			@AccountsZero BIT,
			@ThirdPartyInitial VARCHAR(MAX),
			@ThirdPartyFinal VARCHAR(MAX),
			@MainAccountInitial VARCHAR(MAX),
			@MainAccountFinal VARCHAR(MAX),
			-------------------------------------------------------------------
			@LegalBookType TINYINT,
			@LastYearClose INT,
			@LegalBookName VARCHAR(200)

	BEGIN TRY
		
		/******************************************** CRITERIOS Y FILTROS ********************************************/

		--Se obtienen los datos de los criterios
		SELECT	@Year = t.x.value('Year[1]','int'),
				@Month = t.x.value('Month[1]','int'),
				@LegalBookId = t.x.value('LegalBookId[1]','int'),
				@AccountsZero = t.x.value('AccountsZero[1]','bit'),
				@ThirdPartyInitial = t.x.value('ThirdPartyInitial[1]','varchar(max)'),
				@ThirdPartyFinal = t.x.value('ThirdPartyFinal[1]','varchar(max)'),
				@MainAccountInitial = t.x.value('MainAccountInitial[1]','varchar(max)'),
				@MainAccountFinal = t.x.value('MainAccountFinal[1]','varchar(max)')
		FROM @xmlCriterias.nodes('/Data') t(x)

		SELECT	@ThirdPartyInitial = IIF(ISNULL(@ThirdPartyInitial, '') = '', '0', @ThirdPartyInitial),
				@ThirdPartyFinal = IIF(ISNULL(@ThirdPartyFinal, '') = '', 'ZZZZZZZZZZZZZZZZZZZZ', @ThirdPartyFinal),
				@MainAccountInitial = IIF(ISNULL(@MainAccountInitial, '') = '', '0', @MainAccountInitial),
				@MainAccountFinal = IIF(ISNULL(@MainAccountFinal, '') = '', 'ZZZZZZZZZZZZZZZZZZZZ', @MainAccountFinal)

		---------------------------------------------------------------------------------------------------------------

		SELECT	@LegalBookType = lb.TypeBook,
				@LegalBookName = lb.Name,
				@LastYearClose = lb.LastYearClose
		FROM GeneralLedger.LegalBook lb WITH (NOLOCK)
		WHERE lb.Id = @LegalBookId

		/*****************************************  OBTENEMOS LA INFORMACION *****************************************/

		--Insertamos los movimientos del periodo inicial
		SELECT 	tp.Id AS ThirdPartyId, tp.Nit AS ThirdPartyNit, tp.Name AS ThirdPartyName, 
				-----------------------------------------------------------------------------------------------------------------
				@LegalBookName LegalBookName,
				glb.MainAccountId, glb.MainAccountCode, glb.MainAccountName,
				-----------------------------------------------------------------------------------------------------------------
				glb.ValueDebitInitial, glb.ValueCreditInitial,
				((glb.ValueDebitInitial - glb.ValueCreditInitial) * IIF(glb.MainAccountNature = 1, 1, -1)) PreviousBalance,
				-----------------------------------------------------------------------------------------------------------------
				glb.ValueDebitMovement, glb.ValueCreditMovement,
				-----------------------------------------------------------------------------------------------------------------
				(glb.ValueDebitInitial + glb.ValueDebitMovement) ValueDebitFinal, (glb.ValueCreditInitial + glb.ValueCreditMovement) ValueCreditFinal,
				((glb.ValueDebitInitial - glb.ValueCreditInitial + glb.ValueDebitMovement - glb.ValueCreditMovement) * IIF(glb.MainAccountNature = 1, 1, -1)) NewBalance
		INTO #SP_ReportThirdPartyBalance
		FROM [GeneralLedger].[GetGeneralLedgerBalance](@LegalBookType, @LegalBookId, @LastYearClose, @Year, @Month, @Month, 0) glb
		JOIN Common.ThirdParty AS tp WITH (NOLOCK) ON glb.ThirdPartyId  = tp.Id
		WHERE	glb.MainAccountCode BETWEEN ISNULL(@MainAccountInitial, '0') AND ISNULL(@MainAccountFinal, 'ZZZZZZZZZZZZZZZZZZZZ')
				AND ISNULL(tp.Nit, '0') BETWEEN ISNULL(@ThirdPartyInitial, '0') AND ISNULL(@ThirdPartyFinal, 'ZZZZZZZZZZZZZZZZZZZZ')

		/******************************************* APLICAMOS LOS FILTROS *******************************************/

		--Filtros del reporte
		IF @AccountsZero = 0 
		BEGIN
			DELETE e 
			FROM #SP_ReportThirdPartyBalance e 
			JOIN 
			(
				SELECT MainAccountId, ThirdPartyId
				FROM #SP_ReportThirdPartyBalance 
				GROUP BY MainAccountId, ThirdPartyId 
				HAVING SUM(newBalance) = 0
			) AS data ON data.MainAccountId = e.MainAccountId AND ISNULL(data.ThirdPartyId, 0) = ISNULL(e.ThirdPartyId, 0)
		END		
    
		/*****************************************  MOSTRAMOS LOS RESULTADOS *****************************************/

		--Mostramos los resultados
		SELECT	tpb.LegalBookName, 
				tpb.ThirdPartyId, 
				tpb.ThirdPartyNit, 
				tpb.ThirdPartyName,
				tpb.MainAccountId, 
				tpb.MainAccountCode, 
				tpb.MainAccountName,
				tpb.ValueDebitInitial,
				tpb.ValueCreditInitial,
				tpb.PreviousBalance,
				tpb.ValueDebitMovement, 
				tpb.ValueCreditMovement,
				tpb.ValueDebitFinal,
				tpb.ValueCreditFinal,
				tpb.NewBalance
		FROM #SP_ReportThirdPartyBalance tpb
		ORDER BY 3, 6
	END TRY
	BEGIN CATCH
		PRINT CONCAT('Error: ', ERROR_MESSAGE(), ' - Linea: ', ERROR_LINE())
	END CATCH

	-- Eliminamos Las tablas temporales
	IF OBJECT_ID('tempdb..#SP_ReportThirdPartyBalance') IS NOT NULL DROP TABLE #SP_ReportThirdPartyBalance
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de saldos contables por tercero (proveedor, contratista, aseguradora u otra entidad externa) para un libro contable legal y un período (año y mes) específicos. Combina los saldos iniciales, movimientos débito/crédito del período y el saldo final de cada cuenta mayor, filtrando por rangos de NIT del tercero y código de cuenta contable. Utiliza la función GetGeneralLedgerBalance para obtener los movimientos del libro mayor y los cruza con la tabla de terceros (Common.ThirdParty) para mostrar el NIT y nombre de cada tercero. Opcionalmente excluye del resultado las cuentas cuyo saldo neto sea cero, y ordena el informe por NIT y código de cuenta para facilitar la conciliación contable y tributaria con terceros.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportThirdPartyBalance';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportThirdPartyBalance';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte de saldos contables por tercero y cuenta principal para un mes y libro contable, mostrando saldo anterior, movimientos débito/crédito y nuevo saldo, con opción de excluir cuentas con saldo cero.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportThirdPartyBalance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de criterios debe contener el nodo /Data con Year, Month, LegalBookId y AccountsZero; Debe existir un LegalBook con el Id indicado para obtener TypeBook, Name y LastYearClose; La función tabular GetGeneralLedgerBalance debe estar disponible y aceptar (TypeBook, LegalBookId, LastYearClose, Year, Month, Month, 0); Los terceros referenciados por los movimientos deben existir en Common.ThirdParty (JOIN, no LEFT JOIN)', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportThirdPartyBalance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Si los rangos de tercero o cuenta llegan vacíos/nulos, se sustituyen por límites por defecto (''0'' a ''ZZZZZZZZZZZZZZZZZZZZ'') para no excluir registros; El saldo previo y el saldo nuevo siempre se ajustan según la naturaleza de la cuenta principal (débito = +1, otra = -1); El reporte se acota al periodo de un único mes (mismo mes inicial y final pasado a la función de saldos); Los terceros sin NIT se tratan como ''0'' para efectos del filtro de rango; La tabla temporal de trabajo siempre se elimina al final aun si hay error (limpieza fuera del TRY/CATCH); Los errores no se relanzan: se imprimen con PRINT, por lo que el procedimiento no falla visiblemente al consumidor', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportThirdPartyBalance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Saldo por tercero; Libro contable (LegalBook); Tercero (NIT); Cuenta principal (MainAccount); Naturaleza de cuenta (débito/crédito); Saldo anterior y saldo nuevo; Movimientos débito/crédito del periodo; Cierre de año anterior (LastYearClose)', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportThirdPartyBalance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] #SP_ReportThirdPartyBalance: Se carga el resultado de GetGeneralLedgerBalance unido a Common.ThirdParty filtrando por rango de MainAccountCode y rango de Nit del tercero; [DELETE] #SP_ReportThirdPartyBalance: Cuando @AccountsZero = 0, se eliminan filas cuyas combinaciones (MainAccountId, ThirdPartyId) tengan SUM(NewBalance) = 0; [RETURN_RESULT] #SP_ReportThirdPartyBalance: Devuelve el set final ordenado por ThirdPartyNit y MainAccountCode con saldos inicial, movimientos y saldo nuevo', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportThirdPartyBalance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @AccountsZero = 0 → Elimina del resultado los pares (MainAccountId, ThirdPartyId) cuyo SUM(NewBalance) sea 0, ocultando cuentas/terceros sin saldo neto else Conserva todas las filas, incluidas las de saldo cero; si MainAccountNature = 1 (naturaleza débito) → El saldo se calcula como (Débito - Crédito) else Para naturaleza distinta de 1, el saldo se invierte multiplicando por -1 (Crédito - Débito)', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportThirdPartyBalance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.LegalBook; GeneralLedger.GetGeneralLedgerBalance; Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportThirdPartyBalance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportThirdPartyBalance';
-- GO
