-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-10-08
-- Description:	Procedimiento para el reporte de conciliacion
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ReportReconcileModule]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @Module INT

	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		--Se obtienen los datos de los criterios
		SELECT 
			@Module = t.x.value('Module[1]','int')
		FROM @xmlCriterias.nodes('/Data') t(x)

		/********************************** OBTENCION DE DATOS **********************************/

		IF @Module = 1
		BEGIN
			EXEC [GeneralLedger].[SP_ReportReconcileTreasury] @xmlCriterias
		END
		ELSE IF @Module = 2
		BEGIN
			EXEC [GeneralLedger].[SP_ReportReconcilePortfolio] @xmlCriterias
		END
		ELSE IF @Module = 3
		BEGIN
			EXEC [GeneralLedger].[SP_ReportReconcilePayments] @xmlCriterias
		END
		ELSE IF @Module = 4
		BEGIN
			EXEC [GeneralLedger].[SP_ReportReconcileInventory] @xmlCriterias
		END

	END TRY
	BEGIN CATCH	
		SELECT '999' AS Code, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) AS Message
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Punto de entrada unificado para el reporte de conciliación contable entre el módulo de Contabilidad General (General Ledger) y los demás módulos del ERP. Recibe un criterio XML que indica el módulo a conciliar: Tesorería (1), Cartera/Portafolio (2), Pagos (3) o Inventario (4), y delega la ejecución al procedimiento especializado correspondiente. Existe para centralizar y enrutar en un solo llamado la conciliación contable de cualquier módulo, facilitando la generación de reportes de cuadre entre registros contables y los movimientos financieros u operativos de cada área.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportReconcileModule';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportReconcileModule';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Despacha la generación del reporte de conciliación contable hacia el procedimiento especializado según el módulo indicado (tesorería, cartera, pagos o inventario).', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportReconcileModule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de criterios debe contener un nodo /Data con el elemento Module de tipo entero; El valor de Module debe ser 1, 2, 3 o 4 para que se ejecute alguna rama de conciliación', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportReconcileModule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se invoca un único procedimiento de conciliación por ejecución; Los errores nunca se propagan al llamador: siempre se capturan y se retornan como resultset con código 999; El mismo XML de criterios recibido se reenvía sin transformación al sub-procedimiento elegido', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportReconcileModule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Conciliación contable; Tesorería; Cartera; Pagos; Inventario', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportReconcileModule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando ocurre una excepción en el TRY, se retorna un resultset con Code=''999'' y el mensaje de error junto al número de línea', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportReconcileModule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Module = 1 → Ejecuta el procedimiento de conciliación de tesorería; si Module = 2 → Ejecuta el procedimiento de conciliación de cartera (Portfolio); si Module = 3 → Ejecuta el procedimiento de conciliación de pagos; si Module = 4 → Ejecuta el procedimiento de conciliación de inventario else Si Module no coincide con 1-4, no se ejecuta ningún sub-proceso ni se retorna resultado', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportReconcileModule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'GeneralLedger.SP_ReportReconcileTreasury; GeneralLedger.SP_ReportReconcilePortfolio; GeneralLedger.SP_ReportReconcilePayments; GeneralLedger.SP_ReportReconcileInventory', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportReconcileModule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportReconcileModule';
-- GO
