CREATE PROCEDURE [dbo].[SP_ONCO_Circular022_DiagnosticoEstadificacion]
(
    @FechaInicial DATE,
    @FechaFinal DATE,
    @IDEntidadVIE VARCHAR(20),
    @Empresa VARCHAR(20)
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        TipoRegistro=3,
        Consecutivo=ROW_NUMBER()OVER(ORDER BY H.FECHACREACION,H.[6]),
        TipoID=CASE P.IPTIPODOC WHEN 1 THEN 'CC' WHEN 2 THEN 'CE' WHEN 3 THEN 'TI' WHEN 4 THEN 'RC' WHEN 6 THEN 'AS' WHEN 7 THEN 'MS' WHEN 9 THEN 'CN' WHEN 13 THEN 'PT' ELSE '' END,
        Identificacion=RTRIM(P.IPCODPACI),
        CIE10= RTRIM(H.CODDIAGNO),
        CIE11='-',
        FechaDiagnostico=H.FECHACREACION,
        FechaNotaRemision= ISNULL((SELECT TOP 1 FECDIAGNO FROM INDIAGNOH WHERE IPCODPACI = P.IPCODPACI AND TIPDIAGNO = 'I' AND CODDIAGNO = H.CODDIAGNO order by FECDIAGNO desc), (SELECT TOP 1 FECDIAGNO FROM INDIAGNOH WHERE IPCODPACI = P.IPCODPACI AND TIPDIAGNO <> 'I' AND CODDIAGNO = H.CODDIAGNO order by FECDIAGNO ASC)),
        FechaAtencion=H.FECHACREACION,
        TipoEstudio= CASE H.[21] 
					 WHEN 1 THEN 'Mielograma o aspirado de médula ósea' 
					 WHEN 2 THEN 'Biopsia de médula ósea' 
					 WHEN 3 THEN 'Biopsia de ganglios'
					 WHEN 4 THEN 'Biopsia de masa' 
					 WHEN 5 THEN 'Inmunohistoquímica' 
					 WHEN 6 THEN 'Citometría de flujo' 
					 WHEN 7  THEN 'Clínica exclusivamente (incluye estudios imagenológicos y/o de laboratorio en aquellos casos clínicamente justificados en donde fue imposible tomar muestra de estudio histopatológico)'
					 WHEN 8  THEN 'Otro'
					 WHEN 9  THEN 'Genética'
					 WHEN 10 THEN 'Patología básica'
					 WHEN 99 THEN 'Desconocido, el dato de esta variable no se encuentra descrito en los soportes clínicos'
					 ELSE '-' END,
        RecoleccionMuestra= FORMAT(H.[23], 'dd/MM/yyy'),
        FechaHistopatologia= FORMAT(H.[24], 'dd/MM/yyy'),
        FechaPrimeraConsulta= FORMAT(H.[26], 'dd/MM/yyy'),
        Estadificacion= dbo.EstadificacionPaciente(H.[29]),
        FechaEstadificacion= FORMAT(H.[30], 'dd/MM/yyy'),
        PrimerHER2=H.[32],
        UltimoHER2=H.[32],
        EstLinfomas= dbo.EstadificacionLinfomas(H.[36]),
        Gleason= CASE H.[37] 
				 WHEN 11 THEN 'Gleason ≤ 6: 3 + 3'
				 WHEN 12 THEN 'Gleason 7: 3+4'
				 WHEN 13 THEN 'Gleason 7: 4+3'
				 WHEN 14 THEN 'Gleason 8: 4+4 o 3+5 o 5+3'
				 WHEN 15 THEN 'Gleason 9 o 10: 4+5 o 5+4 o 5+5'				 
				 WHEN 97 THEN 'Es cáncer de próstata, pero no hay información acerca de esta estadificación porque el diagnóstico fue clínico'
				 WHEN 98 THEN 'No es cáncer de próstata'
				 WHEN 99 THEN 'Es cáncer de próstata, pero no hay información en la historia clínica acerca de esta clasificación' END,
        ClasifRiesgo= CASE H.[38] 
					  WHEN 1  THEN 'Estándar, bajo, o favorable'
					  WHEN 2  THEN 'Bajo intermedio'
					  WHEN 3  THEN 'Intermedio'
					  WHEN 4  THEN 'Alto intermedio'
					  WHEN 5  THEN 'Alto o desfavorable'
					  WHEN 98 THEN 'No Aplica (no es leucemia, ni linfoma)'
					  WHEN 99 THEN 'Desconocido, el dato de esta variable no se encuentra descrito en los soportes clínicos' END,
        EstadoVital= CASE H.[127] WHEN 1 THEN 'Vivo' WHEN 2 THEN 'Fallecido' ELSE 'Desconocido' END
    FROM HCONCOPREG H
		INNER JOIN INPACIENT P ON RTRIM(P.IPCODPACI)=RTRIM(H.[6])
    WHERE H.IDEntidadVIE=@IDEntidadVIE
      AND H.FECHACREACION>=@FechaInicial
      AND H.FECHACREACION<DATEADD(DAY,1,@FechaFinal);
END;
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento de reporte oncológico que extrae datos de diagnóstico y estadificación de pacientes con cáncer, filtrando por rango de fechas y entidad VIE. Consolida información clínica como código CIE-10, tipo de estudio histopatológico, fechas de diagnóstico y atención, estadificación tumoral (incluyendo escala de Gleason, linfomas y clasificación de riesgo), y estado vital, para generar el Tipo de Registro 3 exigido por la Circular 022 de reporte oncológico obligatorio en el sistema de salud colombiano.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_Circular022_DiagnosticoEstadificacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_Circular022_DiagnosticoEstadificacion';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte regulatorio de Diagnóstico y Estadificación oncológica (TipoRegistro=3, Circular 022) para una entidad VIE y rango de fechas, traduciendo códigos clínicos a descripciones legibles.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_Circular022_DiagnosticoEstadificacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El rango de fechas (@FechaInicial, @FechaFinal) debe ser válido y coherente; Debe existir la entidad VIE indicada en HCONCOPREG; Las funciones escalares dbo.EstadificacionPaciente y dbo.EstadificacionLinfomas deben existir; Los pacientes referenciados en HCONCOPREG.[6] deben existir en INPACIENT', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_Circular022_DiagnosticoEstadificacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan registros de HCONCOPREG asociados a la entidad VIE indicada y al rango de fechas de creación dado (inclusive en inicio, exclusivo del día siguiente al final); TipoRegistro siempre es 3 (identificador del bloque de la Circular 022 - Diagnóstico/Estadificación); El consecutivo se asigna por ROW_NUMBER ordenado por fecha de creación y código de paciente; CIE11 siempre se reporta como ''-'' (no se calcula); PrimerHER2 y UltimoHER2 toman el mismo valor (columna [32]); no se diferencian primero/último; Las fechas de recolección, histopatología, primera consulta y estadificación se formatean como dd/MM/yyy; Solo se incluyen pacientes que existan en INPACIENT (INNER JOIN) con coincidencia de IPCODPACI tras RTRIM', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_Circular022_DiagnosticoEstadificacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Oncología; Diagnóstico oncológico; Estadificación; Linfomas; Cáncer de próstata (Gleason); HER2; CIE-10; CIE-11; Clasificación de riesgo (leucemia/linfoma); Tipo de estudio histopatológico; Estado vital del paciente; Circular 022 (reporte regulatorio); Entidad VIE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_Circular022_DiagnosticoEstadificacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCONCOPREG: Cuando H.IDEntidadVIE=@IDEntidadVIE y H.FECHACREACION está entre @FechaInicial y @FechaFinal (inclusive), se devuelve un resultset con el detalle de diagnóstico y estadificación oncológica por paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_Circular022_DiagnosticoEstadificacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe en INDIAGNOH un registro del paciente con TIPDIAGNO=''I'' y mismo CODDIAGNO → FechaNotaRemision toma el FECDIAGNO más reciente de ese conjunto (TOP 1 ORDER BY FECDIAGNO DESC) else Toma el FECDIAGNO más antiguo de INDIAGNOH con TIPDIAGNO<>''I'' y mismo CODDIAGNO (TOP 1 ORDER BY FECDIAGNO ASC); si Mapeo de IPTIPODOC a código de tipo de identificación → 1→CC, 2→CE, 3→TI, 4→RC, 6→AS, 7→MS, 9→CN, 13→PT else Cadena vacía; si Mapeo de tipo de estudio diagnóstico (columna [21]) → Valores 1-10, 99 traducen a descripciones clínicas (mielograma, biopsias, inmunohistoquímica, citometría, clínica, genética, patología básica, desconocido) else ''-'' cuando no coincide; si Clasificación de Gleason según columna [37] → 11..15 mapean a categorías Gleason ≤6 hasta 9/10; 97/98/99 indican excepciones de información o no aplicación; si Clasificación de riesgo según columna [38] → 1..5 escalan de estándar/bajo a alto/desfavorable; 98 No Aplica; 99 Desconocido; si Estado vital según columna [127] → 1→Vivo, 2→Fallecido else Desconocido', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_Circular022_DiagnosticoEstadificacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.EstadificacionPaciente; dbo.EstadificacionLinfomas', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_Circular022_DiagnosticoEstadificacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCONCOPREG; dbo.INPACIENT; dbo.INDIAGNOH', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_Circular022_DiagnosticoEstadificacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_Circular022_DiagnosticoEstadificacion';
-- GO
