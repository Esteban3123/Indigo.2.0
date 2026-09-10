-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-03-04
-- Description:	Procedimiento para el reporte de cuenta fiscal
-- =============================================
CREATE PROCEDURE [Inventory].[SP_ReportFiscalAccount]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @TypeReport INT

	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		--Se obtienen los datos de los criterios
		SELECT 
			@TypeReport = t.x.value('TypeReport[1]','int')
		FROM @xmlCriterias.nodes('/Data') t(x)

		/********************************** OBTENCION DE DATOS **********************************/

		IF @TypeReport = 1
		BEGIN
			EXEC [Inventory].[SP_ReportFiscalAccountSummary] @xmlCriterias
		END
		ELSE IF @TypeReport = 2
		BEGIN
			EXEC [Inventory].[SP_ReportFiscalAccountDetailed] @xmlCriterias
		END

	END TRY
	BEGIN CATCH	
		SELECT '999' AS Code, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) AS Message
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento principal para generar el reporte de Cuenta Fiscal del módulo de Inventario. Recibe criterios de búsqueda en formato XML y, según el tipo de reporte solicitado, delega la ejecución al procedimiento de resumen (SP_ReportFiscalAccountSummary, tipo 1) o al procedimiento de detalle (SP_ReportFiscalAccountDetailed, tipo 2). Sirve como punto de entrada unificado para la consulta fiscal de inventario, permitiendo obtener tanto una vista consolidada como el detalle de las cuentas fiscales registradas.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ReportFiscalAccount';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ReportFiscalAccount';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Despachador del reporte de cuenta fiscal que enruta la ejecución hacia la versión resumida o detallada según el tipo de reporte indicado en el XML de criterios.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFiscalAccount';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de criterios debe contener el nodo /Data con el elemento TypeReport convertible a int.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFiscalAccount';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se invoca uno de los dos procedimientos hijos por ejecución (mutuamente excluyentes).; Errores nunca se propagan al llamador: siempre se capturan y devuelven como resultset con código ''999''.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFiscalAccount';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuenta fiscal; Reporte resumido; Reporte detallado', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFiscalAccount';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Cuando ocurre cualquier excepción, devuelve un resultset con Code=''999'' y Message conformado por ERROR_MESSAGE() concatenado con el número de línea.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFiscalAccount';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @TypeReport = 1 → Ejecuta Inventory.SP_ReportFiscalAccountSummary pasando el XML de criterios. else Si @TypeReport = 2, ejecuta Inventory.SP_ReportFiscalAccountDetailed; cualquier otro valor no produce ejecución.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFiscalAccount';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Inventory.SP_ReportFiscalAccountSummary; Inventory.SP_ReportFiscalAccountDetailed', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFiscalAccount';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFiscalAccount';
-- GO
