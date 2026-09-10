CREATE PROCEDURE dbo.SP_ONCO_Circular022_CasosIncidentes
(
    @FechaInicial   DATE,
    @FechaFinal     DATE,
    @IDEntidadVIE   VARCHAR(20),
    @Empresa        VARCHAR(20)
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        TipoRegistro = 2,
        Consecutivo = ROW_NUMBER() OVER ( ORDER BY H.FECHACREACION, H.[6] ),
        TipoID = CASE P.IPTIPODOC
                    WHEN 1  THEN 'CC'
                    WHEN 2  THEN 'CE'
                    WHEN 3  THEN 'TI'
                    WHEN 4  THEN 'RC'
                    WHEN 6  THEN 'AS'
                    WHEN 7  THEN 'MS'
                    WHEN 9  THEN 'CN'
                    WHEN 13 THEN 'PT'
                    ELSE ''
                 END,
        Identificacion      = RTRIM(P.IPCODPACI),
        PrimerNombre        = RTRIM(P.IPPRINOMB),
        SegundoNombre       = RTRIM(P.IPSEGNOMB),
        PrimerApellido      = RTRIM(P.IPPRIAPEL),
        SegundoApellido     = RTRIM(P.IPSEGAPEL),
        FechaNacimiento     = CONVERT(CHAR(10), P.IPFECNACI, 23),
        Sexo = CASE P.IPSEXOPAC
                    WHEN 1 THEN 'M'
                    WHEN 2 THEN 'F'
                    ELSE ''
               END,
        MunicipioResidencia = dbo.Patient_Address(RTRIM(P.IPCODPACI))
    FROM HCONCOPREG H
    INNER JOIN INPACIENT P ON RTRIM(P.IPCODPACI) = RTRIM(H.[6])
    WHERE H.IDEntidadVIE   = @IDEntidadVIE
      AND H.FECHACREACION >= @FechaInicial
      AND H.FECHACREACION <  DATEADD(DAY,1,@FechaFinal);
END;

GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el listado de casos incidentes oncológicos (TipoRegistro=2) con datos demográficos del paciente, para reporte regulatorio Circular 022 dentro de un rango de fechas y entidad.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_Circular022_CasosIncidentes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir registro en HCONCOPREG con IDEntidadVIE igual al parámetro y FECHACREACION dentro del rango [FechaInicial, FechaFinal]; Debe existir paciente en INPACIENT cuyo IPCODPACI coincida (con RTRIM) con la columna [6] de HCONCOPREG; La función dbo.Patient_Address debe estar disponible para resolver el municipio de residencia', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_Circular022_CasosIncidentes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'TipoRegistro siempre es 2 (incidentes); El rango de fechas se evalúa de forma semiabierta usando DATEADD(DAY,1,@FechaFinal) para incluir el día final completo; Solo se reportan casos asociados a la entidad VIE indicada; La fecha de nacimiento se formatea siempre como CHAR(10) en formato ISO (yyyy-mm-dd); Solo se incluyen pacientes existentes en INPACIENT (INNER JOIN)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_Circular022_CasosIncidentes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; casos incidentes oncológicos; Circular 022; tipo de documento de identificación; entidad VIE; municipio de residencia; registro oncológico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_Circular022_CasosIncidentes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas con TipoRegistro fijo=2 (casos incidentes), consecutivo ordenado por FECHACREACION y columna [6], junto con identificación y datos demográficos del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_Circular022_CasosIncidentes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si P.IPTIPODOC IN (1,2,3,4,6,7,9,13) → Mapea a códigos de tipo de documento CC, CE, TI, RC, AS, MS, CN, PT respectivamente else Devuelve cadena vacía como tipo de identificación; si P.IPSEXOPAC = 1 / = 2 → Traduce sexo a ''M'' o ''F'' else Devuelve cadena vacía', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_Circular022_CasosIncidentes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Patient_Address', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_Circular022_CasosIncidentes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCONCOPREG; dbo.INPACIENT', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_Circular022_CasosIncidentes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_Circular022_CasosIncidentes';
-- GO
