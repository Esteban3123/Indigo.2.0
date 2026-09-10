/****** Object:  StoredProcedure [dbo].[SPHC_ListarRipsUrgenciasUsuarios]    Script Date: 22/5/2026 23:50:24 ******/

CREATE    PROCEDURE [dbo].[SPHC_ListarRipsUrgenciasUsuarios]
    @FechaInicial DATE,
    @FechaFinal   DATE,
    @CentroAtencion VARCHAR(25)

AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @FechaFinalMas1 DATE = DATEADD(DAY, 1, @FechaFinal);

    ;WITH CTE_DestinoFinal AS (
        -- Evalúa el INDICAPAC del último registro de cualquier tipo para determinar
        -- si el ingreso tiene un destino válido. Sin filtro TIPHISPAC ni UFUTIPUNI para
        -- que la decisión se tome del verdadero último registro del ingreso, incluso si
        -- el paciente fue trasladado a otra unidad (p. ej. Cirugía, Hospitalización) y su
        -- disposición final quedó registrada allí y no en la unidad de Urgencias. Filtrar
        -- por U.UFUTIPUNI=1 aquí excluía esos ingresos por completo del archivo plano
        -- (el paciente sí tuvo consulta de Urgencias, ver CTE_TieneConsultaI, pero su nota
        -- final no vive en una unidad tipo Urgencias).
        -- Toma el registro más reciente que YA tenga un INDICAPAC válido (filtra por
        -- INDICAPAC antes de rankear, no después): el ingreso puede tener notas de
        -- continuación (INDICAPAC 13/14/18/22, p.ej. "Pre-alta hospitalaria") registradas
        -- después de la disposición final real, y estas no deben anularla. No basta con
        -- priorizar TIPHISPAC='E' porque el registro de Egreso no siempre lleva el
        -- INDICAPAC correcto (a veces queda una nota 'N' posterior con la disposición
        -- real) — se probó y causaba exclusiones incorrectas en la dirección opuesta.
        -- Sin tope superior de fecha (solo @FechaInicial como piso): un ingreso admitido
        -- dentro del período puede resolverse (egreso real) días o semanas después, fuera
        -- de la ventana de consulta. Acotar también por @FechaFinalMas1 aquí hacía que esa
        -- disposición final, ya registrada pero posterior al cierre del período, nunca se
        -- viera, y el ingreso quedaba excluido aunque sí tuviera un destino válido.
        SELECT IPCODPACI, NUMINGRES
        FROM (
            SELECT H.IPCODPACI, H.NUMINGRES, H.INDICAPAC,
                ROW_NUMBER() OVER (
                    PARTITION BY H.IPCODPACI, H.NUMINGRES
                    ORDER BY CAST(H.NUMEFOLIO AS INT) DESC
                ) AS RN
            FROM HCHISPACA H
            WHERE H.CODCENATE = @CentroAtencion
              AND H.FECHISPAC >= @FechaInicial
              AND H.INDICAPAC IN (8, 3, 4, 5, 6, 19, 20, 21, 12, 9, 10, 11, 15, 16)
        ) X
        WHERE RN = 1
    )
    , CTE_TieneConsultaI AS (
        -- Garantiza que el ingreso tuvo al menos una consulta TIPHISPAC='I' en el período,
        -- reemplazando el WHERE HC.TIPHISPAC='I' que tenía la consulta principal original.
        -- El centro de atención se valida contra ADINGRESO (el ingreso), no contra
        -- HCHISPACA.CODCENATE (el centro donde se registró la nota clínica), ya que en
        -- clientes multi-sede (p. ej. HOMI) ambos valores pueden diferir para el mismo
        -- ingreso y filtrar por H.CODCENATE excluía indebidamente esos registros.
        SELECT DISTINCT H.IPCODPACI, H.NUMINGRES
        FROM HCHISPACA H
        INNER JOIN INUNIFUNC U ON U.UFUCODIGO = H.UFUCODIGO AND U.UFUTIPUNI = 1
        INNER JOIN dbo.ADINGRESO AI ON AI.IPCODPACI = H.IPCODPACI AND AI.NUMINGRES = H.NUMINGRES
        WHERE AI.CODCENATE = @CentroAtencion
          AND H.TIPHISPAC  = 'I'
          AND H.FECHISPAC >= @FechaInicial
          AND H.FECHISPAC  < @FechaFinalMas1
    )
    SELECT
        ROW_NUMBER() OVER (ORDER BY A.IFECHAING, A.IPCODPACI) AS Consecutivo,
        TD.SIGLA AS TipoIdentificacion,
        RTRIM(A.IPCODPACI) AS NumeroIdentificacion,
        RTRIM(PAC.IPNOMCOMP) AS NombreView,
        CASE
            WHEN HT.EntityType = '10' THEN '1'
            WHEN HT.EntityType = '5'  THEN '3'
            ELSE '2'
        END AS ResponsablePago,
        CASE
            WHEN HT.EntityType = '10' THEN 'SOAT'
            WHEN HT.EntityType = '5'  THEN 'ARL'
            ELSE 'SGSS'
        END AS ResponsablePagoView,
        CASE PAC.IPTIPOPAC
            WHEN '1'  THEN '1'  WHEN '14' THEN '2'  WHEN '3'  THEN '3'
            WHEN '4'  THEN '4'  WHEN '10' THEN '4'  WHEN '11' THEN '4'
            WHEN '12' THEN '4'  WHEN '13' THEN '4'  WHEN '9'  THEN '5'
            WHEN '2'  THEN '6'  ELSE '0'
        END AS RegimenAfiliacion,
        CASE PAC.IPTIPOPAC
            WHEN '1'  THEN 'Contributivo'    WHEN '14' THEN 'Excepción'
            WHEN '3'  THEN 'No asegurado'    WHEN '4'  THEN 'Otros regímenes'
            WHEN '10' THEN 'Otros regímenes' WHEN '11' THEN 'Otros regímenes'
            WHEN '12' THEN 'Otros regímenes' WHEN '13' THEN 'Otros regímenes'
            WHEN '9'  THEN 'Especial'        WHEN '2'  THEN 'Subsidiado'
            ELSE ''
        END AS RegimenAfiliacionView,
        CASE
            WHEN PAC.IPTIPOPAC = '3'                                   THEN ''
            WHEN UPPER(ISNULL(RTRIM(HT.HealthEntityCode), '')) = 'N/A' THEN ''
            WHEN UPPER(ISNULL(RTRIM(HT.HealthEntityCode), '')) = 'NA'  THEN ''
            ELSE ISNULL(RTRIM(HT.HealthEntityCode), '')
        END AS CodigoAseguradoraEAPB,
        CASE
            WHEN PAC.IPTIPOPAC = '3' THEN ''
            ELSE ISNULL(RTRIM(HT.Name), '')
        END AS CodigoAseguradoraEAPBView,
        CASE
            WHEN ED.EdadAnios >= 1 THEN ED.EdadAnios
            WHEN ED.EdadMeses >= 1 THEN ED.EdadMeses
            ELSE ED.EdadDias
        END AS Edad,
        CASE
            WHEN ED.EdadAnios >= 1 THEN CAST(ED.EdadAnios AS VARCHAR(3)) + CASE WHEN ED.EdadAnios = 1 THEN ' año' ELSE ' años' END
            WHEN ED.EdadMeses >= 1 THEN CAST(ED.EdadMeses AS VARCHAR(3)) + CASE WHEN ED.EdadMeses = 1 THEN ' mes' ELSE ' meses' END
            ELSE CAST(ED.EdadDias AS VARCHAR(5)) + CASE WHEN ED.EdadDias = 1 THEN ' día' ELSE ' días' END
        END AS EdadView,
        CASE
            WHEN ED.EdadAnios >= 1 THEN '1'
            WHEN ED.EdadMeses >= 1 THEN '2'
            ELSE '3'
        END AS UnidadMedidaEdad,
        CASE RTRIM(PAC.IPSEXOPAC) WHEN '1' THEN 'H' WHEN '2' THEN 'M' ELSE '' END AS Sexo,
        CASE RTRIM(PAC.IPSEXOPAC)
            WHEN '1' THEN 'Hombre' WHEN '2' THEN 'Mujer'
            ELSE 'I - Indeterminado o intersexual'
        END AS SexoView,
        '2' AS TipoRegistro
    FROM dbo.ADINGRESO A
    INNER JOIN CTE_DestinoFinal F
        ON F.IPCODPACI = A.IPCODPACI AND F.NUMINGRES = A.NUMINGRES
    INNER JOIN CTE_TieneConsultaI TI
        ON TI.IPCODPACI = A.IPCODPACI AND TI.NUMINGRES = A.NUMINGRES
    INNER JOIN dbo.INPACIENT PAC
        ON PAC.IPCODPACI = A.IPCODPACI
    INNER JOIN ADTIPOIDENTIFICA TD
        ON TD.CODIGO = PAC.IPTIPODOC
    LEFT JOIN Contract.HealthAdministrator HT
        ON RTRIM(A.GENCONENTITY) = RTRIM(HT.Id)
    CROSS APPLY (
        SELECT
            DATEDIFF(YEAR, PAC.IPFECNACI, GETDATE())
            - CASE WHEN DATEADD(YEAR, DATEDIFF(YEAR, PAC.IPFECNACI, GETDATE()), PAC.IPFECNACI) > GETDATE() THEN 1 ELSE 0 END AS EdadAnios,
            DATEDIFF(MONTH, PAC.IPFECNACI, GETDATE())
            - CASE WHEN DATEADD(MONTH, DATEDIFF(MONTH, PAC.IPFECNACI, GETDATE()), PAC.IPFECNACI) > GETDATE() THEN 1 ELSE 0 END AS EdadMeses,
            DATEDIFF(DAY, PAC.IPFECNACI, GETDATE()) AS EdadDias
    ) ED
    WHERE A.CODCENATE = @CentroAtencion
    OPTION (RECOMPILE);
END