CREATE PROCEDURE [dbo].[SPHC_ListarLaboratoriosPacientesNotificacionesInterfaz] 
(
@CentroAtencion Char(10),
@UnidadFuncional Char(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
--SELECT C.AUTO AS AutoCabecera,B.AUTOLABOR AS AUTOLABO,D.NUMEFOLIO AS FOLIO,A.IPCODPACI AS Paciente,A.NUMINGRES AS INGRESO,B.CODSERIPS AS 'CODIGO SERVICIO',
--RTRIM(F.DESSERIPS) AS 'DESCRIPCION SERVICIO',D.CANSERIPS AS CANTIDAD,G.NUMUESTRA AS MUESTRA,D.FECORDMED AS 'FECHA SERVICIO',C.ORDEN_INDIGO,G.FECREGIST AS FechaRecepcion,G.CODPROSAL AS Profesional,
--RTRIM(I.IPNOMCOMP) AS NOMBREPACIENTE, ISNULL(ILD.CriticalResult, 0) AS AlertaLAB, ISNULL(ILD.ValuesOutLimits,0),iif(ISNULL(CriticalResult,0) = 1 OR ISNULL(ValuesOutLimits,0) = 1,1,0) AS 'ValorC'
--FROM INTERCABE AS A 
--INNER JOIN INTERDETA AS B ON A.AUTO=B.CODCONCEC 
--INNER JOIN INTERLABC AS C ON B.CODCONCEC=C.ORDEN_INDIGO 
--INNER JOIN HCORDLABO AS D ON B.AUTOLABOR= D.AUTO 
--INNER JOIN INCUPSIPS AS F ON B.CODSERIPS=F.CODSERIPS 
--INNER JOIN INTERCTRL AS G ON D.AUTO=G.AUTOLABOR
--INNER JOIN ADINGRESO AS H ON D.NUMINGRES=H.NUMINGRES
--INNER JOIN INPACIENT AS I ON A.IPCODPACI=I.IPCODPACI
--OUTER APPLY (SELECT TOP 1 CODCONCEC, CriticalResult,ValuesOutLimits FROM dbo.INTERLABD AS ILD WHERE C.AUTO = ILD.CODCONCEC ORDER BY CriticalResult DESC) ILD --16-09-2022cambio realizado por maria paula
--WHERE G.ESTADOINT=0 AND H.UFUAACTMED=@UnidadFuncional AND D.CODCENATE=@CentroAtencion ORDER BY G.FECREGIST DESC

SELECT DISTINCT C.AUTO AS AutoCabecera,B.AUTOLABOR AS AUTOLABO,D.NUMEFOLIO AS FOLIO,A.IPCODPACI AS Paciente,A.NUMINGRES AS INGRESO,B.CODSERIPS AS 'CODIGO SERVICIO',
RTRIM(F.DESSERIPS) AS 'DESCRIPCION SERVICIO',D.CANSERIPS AS CANTIDAD,G.NUMUESTRA AS MUESTRA,D.FECORDMED AS 'FECHA SERVICIO',C.ORDEN_INDIGO,G.FECREGIST AS FechaRecepcion,G.CODPROSAL AS Profesional,
RTRIM(I.IPNOMCOMP) AS NOMBREPACIENTE, ISNULL(ILD.CriticalResult, 0) AS AlertaLAB, 
(SELECT CASE 
            WHEN EXISTS (
                SELECT 1 
                FROM INTERLABD LABD 
                WHERE C.AUTO = LABD.CODCONCEC 
                  AND G.AUTOLABOR = LABD.AUTOLABOR 
                  AND ISNULL(LABD.CriticalResult, 0) = 1
            ) 
            THEN 1 ELSE 0 
        END
) AS 'ValorC',

-- para fuera de rango
(SELECT CASE 
            WHEN EXISTS (
                SELECT 1 
                FROM INTERLABD LABD 
                WHERE C.AUTO = LABD.CODCONCEC 
                  AND G.AUTOLABOR = LABD.AUTOLABOR 
                  AND ISNULL(LABD.ValuesOutLimits, 0) = 1
            ) 
            THEN 1 ELSE 0 
        END
) AS 'ValorAlertaFueraRango'
FROM INTERCABE AS A 
INNER JOIN INTERDETA AS B ON A.AUTO=B.CODCONCEC 
INNER JOIN INTERLABC AS C ON B.CODCONCEC=C.ORDEN_INDIGO 
INNER JOIN HCORDLABO AS D ON B.AUTOLABOR= D.AUTO 
INNER JOIN INCUPSIPS AS F ON B.CODSERIPS=F.CODSERIPS 
INNER JOIN INTERCTRL AS G ON D.AUTO=G.AUTOLABOR
INNER JOIN ADINGRESO AS H ON D.NUMINGRES=H.NUMINGRES AND H.IESTADOIN IN ('', 'P', 'B') 
INNER JOIN INPACIENT AS I ON A.IPCODPACI=I.IPCODPACI
LEFT JOIN INTERLABD AS ILD ON C.AUTO = ILD.CODCONCEC --Se agrega el left y se comenta el outer apply por lentitud a la hora de cargar los datos en el dashboard (29-02-2024)
--OUTER APPLY (SELECT TOP 1 CODCONCEC, CriticalResult,ValuesOutLimits FROM dbo.INTERLABD AS ILD WHERE C.AUTO = ILD.CODCONCEC ORDER BY CriticalResult DESC) ILD --16-09-2022cambio realizado por maria paula
WHERE G.ESTADOINT=0 AND H.UFUAACTMED=@UnidadFuncional AND D.CODCENATE=@CentroAtencion ORDER BY G.FECREGIST DESC
	 
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los exámenes de laboratorio pendientes de notificación en la interfaz de integración con el laboratorio externo, filtrando por centro de atención y unidad funcional. Consulta las órdenes de laboratorio de la historia clínica (HCORDLABO) vinculadas a las cabeceras de interconsulta (INTERCABE) y al control de muestras (INTERCTRL), cruzando con el ingreso activo del paciente (ADINGRESO, estados vacío/P/B) y sus datos de identificación (INPACIENT). Para cada orden muestra el folio, el paciente (código y nombre completo), el número de ingreso, el servicio CUPS solicitado, la cantidad, el número de muestra, la fecha de la orden y la fecha de recepción, e incluye indicadores de alerta si algún analito del resultado tiene valor crítico (AlertaLAB / ValorC) o fuera de rango (ValorAlertaFueraRango), obtenidos desde el detalle de resultados de laboratorio (INTERLABD). Se usa en el dashboard de notificaciones de laboratorio para que el personal clínico identifique rápidamente resultados sin notificar que requieren atención, especialmente valores críticos o anormales de pacientes hospitalizados o en urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarLaboratoriosPacientesNotificacionesInterfaz';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarLaboratoriosPacientesNotificacionesInterfaz';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Listar las órdenes/muestras de laboratorio pendientes de notificación por la interfaz para un centro de atención y unidad funcional dados, indicando si presentan resultados críticos o valores fuera de rango.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarLaboratoriosPacientesNotificacionesInterfaz';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros relacionados entre cabecera de interfaz, detalle, orden de laboratorio (HCORDLABO), control de interfaz (INTERCTRL), ingreso (ADINGRESO) y paciente (INPACIENT); El ingreso asociado debe tener estado vacío, ''P'' o ''B''; El control de interfaz de la muestra debe estar en estado 0 (no procesado/pendiente)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarLaboratoriosPacientesNotificacionesInterfaz';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan registros con estado de interfaz pendiente (ESTADOINT=0); Solo se incluyen ingresos cuyo estado sea vacío, ''P'' o ''B''; El listado se filtra por la unidad funcional de atención médica del ingreso y por el centro de atención de la orden de laboratorio; Los resultados se ordenan de forma descendente por la fecha de registro de la muestra (más recientes primero); Las alertas (crítico/fuera de rango) se reportan como 1 únicamente si existe al menos un detalle marcado; en ausencia de detalles se reporta 0; Se utiliza SELECT DISTINCT para evitar duplicados generados por el LEFT JOIN con el detalle de laboratorio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarLaboratoriosPacientesNotificacionesInterfaz';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Laboratorio clínico; Pacientes; Órdenes de laboratorio; Muestras; Resultados críticos; Valores fuera de rango; Ingreso/Admisión hospitalaria; Centro de atención; Unidad funcional; Profesional de salud; Folio de orden; Interfaz de laboratorio (Indigo)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarLaboratoriosPacientesNotificacionesInterfaz';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando G.ESTADOINT=0 y H.UFUAACTMED=@UnidadFuncional y D.CODCENATE=@CentroAtencion y H.IESTADOIN IN ('''',''P'',''B'') → devuelve filas con datos de orden, paciente, muestra, profesional y banderas AlertaLAB, ValorC y ValorAlertaFueraRango', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarLaboratoriosPacientesNotificacionesInterfaz';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe al menos un detalle en INTERLABD para la orden (C.AUTO=LABD.CODCONCEC y G.AUTOLABOR=LABD.AUTOLABOR) con CriticalResult=1 → ValorC = 1 (alerta de resultado crítico) else ValorC = 0; si Existe al menos un detalle en INTERLABD para la orden con ValuesOutLimits=1 → ValorAlertaFueraRango = 1 (alerta de valor fuera de rango) else ValorAlertaFueraRango = 0', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarLaboratoriosPacientesNotificacionesInterfaz';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INTERCABE; dbo.INTERDETA; dbo.INTERLABC; dbo.HCORDLABO; dbo.INCUPSIPS; dbo.INTERCTRL; dbo.ADINGRESO; dbo.INPACIENT; dbo.INTERLABD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarLaboratoriosPacientesNotificacionesInterfaz';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarLaboratoriosPacientesNotificacionesInterfaz';
-- GO
