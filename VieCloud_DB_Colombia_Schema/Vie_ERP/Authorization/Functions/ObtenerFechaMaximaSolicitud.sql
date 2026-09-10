-- =============================================
-- Author:      DIEGO A. ROLDÁN
-- Create Date: 2022-03-18
-- Description: OBTIENE LA FECHA MÁXIMA PARA VALIDACION DE AUTORIZACIONES
-- =============================================
CREATE FUNCTION [Authorization].[ObtenerFechaMaximaSolicitud]
(
    @NUMINGRES CHAR(10)
)
RETURNS DateTime
AS
BEGIN
	DECLARE @DATE DATETIME

	SELECT @DATE = MAX(X.REGDATE) FROM (
		SELECT MAX(FECREGIST) AS REGDATE
		FROM ADAUTOSER A WITH(NOLOCK)
		WHERE A.NUMINGRES = @NUMINGRES
		UNION ALL
		SELECT MAX(FECINIEST) AS REGDATE
		FROM CHREGESTA WITH(NOLOCK)
		WHERE NUMINGRES = @NUMINGRES
		UNION ALL
		SELECT MAX(C.FECINFORM) AS REGDATE
		FROM ADAUTSERD D WITH(NOLOCK)
		JOIN ADAUTSERC C ON D.CODCONCEC = C.CODCONCEC AND D.NUMINGRES = C.NUMINGRES
		WHERE C.NUMINGRES = @NUMINGRES
	) AS X
	
	RETURN COALESCE(@DATE, CAST(-53690 AS DATETIME))
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula la fecha máxima de actividad registrada para un ingreso hospitalario específico, consultando tres fuentes distintas: la última fecha de registro de solicitudes de autorización de servicios (ADAUTOSER), la última fecha de inicio de estancia del paciente (CHREGESTA) y la última fecha de informes de autorización de servicios ante aseguradoras (ADAUTSERC/ADAUTSERD). Retorna el máximo global entre esas tres fechas, y si no existe ningún registro, devuelve una fecha por defecto muy antigua. Se utiliza como referencia temporal para validar si una autorización de servicio ante una EPS/aseguradora está vigente o fue solicitada dentro del período esperado para un número de ingreso dado.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'FUNCTION', @level1name = N'ObtenerFechaMaximaSolicitud';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'FUNCTION', @level1name = N'ObtenerFechaMaximaSolicitud';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Determina la fecha máxima de actividad relacionada a una autorización (registro, cambio de estado o informe de concepto) para un ingreso, usada como referencia temporal en validaciones de autorizaciones.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'ObtenerFechaMaximaSolicitud';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe proveerse el número de ingreso a evaluar.; Las tablas ADAUTOSER, CHREGESTA, ADAUTSERC y ADAUTSERD deben contener registros asociados al ingreso para obtener una fecha real; en caso contrario se devuelve un valor centinela.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'ObtenerFechaMaximaSolicitud';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La fecha retornada nunca es NULL: siempre se aplica COALESCE con un valor centinela.; El cálculo considera tres orígenes temporales unificados vía UNION ALL y se queda con el máximo global.; El cruce entre ADAUTSERD y ADAUTSERC se realiza siempre por la pareja (CODCONCEC, NUMINGRES), no solo por el concepto.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'ObtenerFechaMaximaSolicitud';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'autorización de servicios; ingreso (admisión hospitalaria); cambio de estado clínico/administrativo; informe de concepto autorizado; validación temporal de autorizaciones', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'ObtenerFechaMaximaSolicitud';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (retorno escalar): Devuelve el MAX entre: MAX(FECREGIST) de ADAUTOSER, MAX(FECINIEST) de CHREGESTA y MAX(FECINFORM) de ADAUTSERC (unido a ADAUTSERD por CODCONCEC y NUMINGRES), todos filtrados por NUMINGRES = @NUMINGRES.; [RETURN_RESULT] (retorno escalar): Si no existen registros en ninguna de las tres fuentes (resultado NULL), retorna CAST(-53690 AS DATETIME) como fecha centinela (equivale a 1753-01-01).', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'ObtenerFechaMaximaSolicitud';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @DATE IS NULL (ningún registro de autorización, estado o informe para el ingreso) → Retorna fecha centinela CAST(-53690 AS DATETIME). else Retorna la fecha máxima encontrada entre las tres fuentes.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'ObtenerFechaMaximaSolicitud';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'ADAUTOSER; CHREGESTA; ADAUTSERD; ADAUTSERC', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'ObtenerFechaMaximaSolicitud';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'FUNCTION', @level1name=N'ObtenerFechaMaximaSolicitud';
GO
