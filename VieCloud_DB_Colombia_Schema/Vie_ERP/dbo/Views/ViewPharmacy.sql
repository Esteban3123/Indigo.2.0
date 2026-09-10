CREATE VIEW [dbo].[ViewPharmacy]
AS
WITH DetailFlags AS
(
    SELECT
        D.CODCONCEC,
        MAX(CASE WHEN D.Stat = 1 THEN 1 ELSE 0 END) AS HasStat,
        MAX(CASE WHEN D.VIEPROCESSED = 0 THEN 1 ELSE 0 END) AS HasQuantityRemaining,
        MAX(CASE WHEN P.TIPPRODUC = 1 THEN 1 ELSE 0 END) AS HasMedicamento,
        MAX(CASE WHEN P.TIPPRODUC = 2 THEN 1 ELSE 0 END) AS HasInsumo,
        MAX(CASE WHEN P.TIPPRODUC = 3 THEN 1 ELSE 0 END) AS HasMedicamentoInsumo
    FROM dbo.HCFARMEPD D 
    LEFT JOIN dbo.IHLISTPRO P  ON P.CODPRODUC = D.CODPRODUC
    WHERE D.CANPENPRO > 0
    GROUP BY D.CODCONCEC
)

SELECT
    A.CODCONCEC,
    A.CODCONCEP,
    A.CODCONCES,
    A.FECHAORDE,
    A.CODCENATE,
    A.UFUCODIGO,
    A.CODCENCOS,
    A.IPCODPACI,
    A.NUMINGRES,
    A.NUMEFOLIO,
    A.IDETIPHIS,
    CASE
        WHEN A.MedicalOrderType = 2 THEN '3'
        WHEN ISNULL(A.MedicalOrderType, 1) = 1 AND ISNULL(DF.HasStat, 0) = 1 THEN '4'
        WHEN A.ORDTRANUE = 3 THEN '5'
        ELSE A.ORDTRANUE
    END AS ORDTRANUE,
    ISNULL(A.TIPOSOLICITUD, 0) AS TIPOSOLICITUD,
    A.MEDICAMENTOVALIDADO,
    A.CODBODEGA,
    A.CODPROSAL,
    ING.CODCAMACT,
    A.ORDESTADO,
    ISNULL(CG.ExtramuralPharmaceuticalDispensing, 0) AS TIPOSOLICITUD1,
    CG.ExtramuralPharmaceuticalDispensing AS PERMITEEXTRA,
    CASE WHEN ISNULL(DF.HasQuantityRemaining, 0) = 1 THEN 1 ELSE 0 END AS HasQuantityRemaining,
    CAST(0 AS bit) AS PatientDischarge,
    CASE WHEN ISNULL(DF.HasMedicamento, 0) = 1 THEN 1 ELSE NULL END AS MEDICAMENTO,
    CASE WHEN ISNULL(DF.HasInsumo, 0) = 1 THEN 2 ELSE NULL END AS INSUMOS,
    CASE WHEN ISNULL(DF.HasMedicamentoInsumo, 0) = 1 THEN 3 ELSE NULL END AS MEDICAMENTO_INSUMO
FROM dbo.HCFARMEPC AS A 
INNER JOIN dbo.ADINGRESO AS ING  ON A.NUMINGRES = ING.NUMINGRES
LEFT JOIN Contract.CareGroup AS CG  ON ING.GENCAREGROUP = CG.Id
LEFT JOIN DetailFlags AS DF ON DF.CODCONCEC = A.CODCONCEC
WHERE
    A.ORDESTADO = '1'
    AND (A.ORDENQUIMIO = 0 OR A.ORDENQUIMIO IS NULL)
    AND A.IDAGEPROGQX IS NULL
    AND (
        ING.IESTADOIN NOT IN ('F', 'C')
        OR A.TIPOSOLICITUD = 2
        OR ISNULL(ING.TRATAESPECIA, 0) = 3
        OR EXISTS
        (
            SELECT 1
            FROM dbo.HCINGRESORECNAC RN 
            WHERE RN.NUMINGRESHIJO = ING.NUMINGRES
        )
    )

UNION ALL

SELECT
    A.CODCONCEC,
    A.CODCONCEP,
    A.CODCONCES,
    A.FECHAORDE,
    A.CODCENATE,
    A.UFUCODIGO,
    A.CODCENCOS,
    A.IPCODPACI,
    CAST(ADIN.NUMINGRES AS CHAR(10)) AS NUMINGRES,
    A.NUMEFOLIO,
    A.IDETIPHIS,
    CASE
        WHEN A.MedicalOrderType = 2 THEN '3'
        WHEN ISNULL(A.MedicalOrderType, 1) = 1 AND ISNULL(DF.HasStat, 0) = 1 THEN '4'
        WHEN A.ORDTRANUE = 3 THEN '5'
        ELSE A.ORDTRANUE
    END AS ORDTRANUE,
    ISNULL(A.TIPOSOLICITUD, 1) AS TIPOSOLICITUD,
    A.MEDICAMENTOVALIDADO,
    A.CODBODEGA,
    A.CODPROSAL,
    ADIN.CODCAMACT,
    A.ORDESTADO,
    ISNULL(CG.ExtramuralPharmaceuticalDispensing, 0) AS TIPOSOLICITUD1,
    CG.ExtramuralPharmaceuticalDispensing AS PERMITEEXTRA,
    CASE WHEN ISNULL(DF.HasQuantityRemaining, 0) = 1 THEN 1 ELSE 0 END AS HasQuantityRemaining,
    CAST(0 AS bit) AS PatientDischarge,
    CASE WHEN ISNULL(DF.HasMedicamento, 0) = 1 THEN 1 ELSE NULL END AS MEDICAMENTO,
    CASE WHEN ISNULL(DF.HasInsumo, 0) = 1 THEN 2 ELSE NULL END AS INSUMOS,
    CASE WHEN ISNULL(DF.HasMedicamentoInsumo, 0) = 1 THEN 3 ELSE NULL END AS MEDICAMENTO_INSUMO
FROM dbo.HCFARMEPC AS A 
INNER JOIN dbo.ADINGRESO AS ADIN  ON ADIN.IPCODPACI = A.IPCODPACI AND ADIN.TRATAESPECIA = 3 AND ADIN.IESTADOIN <> 'C'
LEFT JOIN Contract.CareGroup AS CG  ON ADIN.GENCAREGROUP = CG.Id
LEFT JOIN DetailFlags AS DF ON DF.CODCONCEC = A.CODCONCEC
WHERE A.ORDESTADO = '1'
    AND (A.ORDENQUIMIO = 0 OR A.ORDENQUIMIO IS NULL)
    AND A.IDAGEPROGQX IS NULL
    AND A.TIPOSOLICITUD = 2;
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida las órdenes médicas de farmacia pendientes de despacho, combinando el encabezado de la orden (HCFARMEPC) con el detalle de productos (HCFARMEPD), el ingreso del paciente (ADINGRESO) y las reglas del grupo de atención del contrato (CareGroup). Determina para cada orden si tiene cantidades pendientes de procesar, clasifica los productos en medicamentos, insumos o ambos según el catálogo maestro (IHLISTPRO), e indica el tipo de solicitud incluyendo despacho extramural. Es usada por el módulo de farmacia para presentar en cola las órdenes activas que requieren preparación o entrega, distinguiendo órdenes inmediatas, PRN (según necesidad), ambulatorias y de tratamiento especial, excluyendo quimioterapias y cirugías programadas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewPharmacy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewPharmacy';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone órdenes farmacéuticas activas con cantidades pendientes, clasificándolas por tipo de producto (medicamento/insumo) y prioridad de despacho, tanto para pacientes hospitalizados como ambulatorios/extramurales.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewPharmacy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La orden farmacéutica debe estar en estado activo (ORDESTADO=''1'').; La orden no debe corresponder a quimioterapia (ORDENQUIMIO=0 o NULL).; La orden no debe estar asociada a una programación quirúrgica (IDAGEPROGQX IS NULL).; Para el primer bloque, el ingreso no debe estar finalizado o cancelado (IESTADOIN NOT IN (''F'',''C'')), salvo que sea solicitud tipo 2.; Para el segundo bloque, el ingreso debe ser de tratamiento especial tipo 3 y no estar cancelado (IESTADOIN<>''C'').', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewPharmacy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen órdenes activas (ORDESTADO=''1'') no quimioterápicas y no quirúrgicas programadas.; La clasificación MEDICAMENTO/INSUMO/MEDICAMENTO_INSUMO depende de TIPPRODUC (1, 2 o 3) en IHLISTPRO y solo aplica a productos con cantidad pendiente (CANPENPRO>0).; PatientDischarge siempre se devuelve como 0 en ambos bloques del UNION.; ORDTRANUE prioriza PRN (3) sobre inmediato (4), y este sobre la reclasificación de valor 3 a 5.; TIPOSOLICITUD nunca es NULL en la salida (default 0 en bloque 1, 1 en bloque 2).; El segundo bloque solo aplica a solicitudes extramurales/ambulatorias (TIPOSOLICITUD=2).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewPharmacy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'orden médica farmacéutica; medicamento; insumo; orden PRN (según necesidad); orden inmediata; dispensación farmacéutica extramural; ingreso de paciente; tratamiento especial; egreso del paciente; grupo de atención (CareGroup); quimioterapia; programación quirúrgica; cantidad pendiente por despachar; validación de medicamento; bodega farmacéutica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewPharmacy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ViewPharmacy: Devuelve filas de HCFARMEPC unidas con ADINGRESO y CareGroup, enriquecidas con clasificación de tipo de producto (medicamento, insumo, mixto) tomada de IHLISTPRO según HCFARMEPD con CANPENPRO>0.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewPharmacy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si MedicalOrderType = 2 → ORDTRANUE se fija en ''3'' (orden PRN - según necesidad).; si MedicalOrderType = 1 (o NULL en el primer bloque) y existe detalle HCFARMEPD con Stat=1 y CANPENPRO>0 → ORDTRANUE se fija en ''4'' (orden inmediata). else Se evalúa si ORDTRANUE original = 3.; si ORDTRANUE original = 3 → ORDTRANUE se reasigna a ''5''. else Se conserva el valor original de ORDTRANUE.; si Existe al menos un HCFARMEPD con CANPENPRO>0 y VIEPROCESSED=0 para la orden → HasQuantityRemaining = 1. else HasQuantityRemaining = 0.; si TIPOSOLICITUD = 2 (segundo bloque del UNION) → Se incluyen órdenes ambulatorias/extramurales agrupadas por paciente con ingresos de TRATAESPECIA=3 no cancelados, independiente del estado del ingreso original.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewPharmacy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFARMEPD; dbo.HCFARMEPC; dbo.ADINGRESO; dbo.IHLISTPRO; Contract.CareGroup', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewPharmacy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewPharmacy';
GO
