CREATE VIEW [dbo].[ViewPharmacyMixingStationWithOncology]
AS
WITH MainData AS
(
    SELECT
        HC.CODCONCEC,
        HC.CODCONCEP,
        HC.CODCONCES,
        HC.FECHAORDE,
        HC.CODCENATE,
        HC.UFUCODIGO,
        HC.CODCENCOS,
        HC.IPCODPACI,
        HC.NUMINGRES,
        HC.NUMEFOLIO,
        HC.IDETIPHIS,
        HC.ORDTRANUE,
        ISNULL(HC.TIPOSOLICITUD, 0) AS TIPOSOLICITUD,
        HC.MEDICAMENTOVALIDADO,
        HC.CODBODEGA,
        HC.CODPROSAL,
        HC.ORDESTADO,
        ISNULL(HC.ORDENQUIMIO, 0) AS ORDENQUIMIO
    FROM dbo.HCFARMEPC AS HC 
    WHERE HC.ORDESTADO = '1' AND HC.IDAGEPROGQX IS NULL
),
MixingStationDetail AS
(
    SELECT
        HD.CODCONCEC,
        HD.CodeSusceptibleMixingStation,
        PSMS.ApplicationsNumber AS RequestedQuantity,
        COUNT
        (
            DISTINCT CASE
                WHEN PD.IsDispensed = 1 THEN PD.GroupingCodeDose
            END
        ) AS DispensedQuantity
    FROM dbo.HCFARMEPD AS HD 
    INNER JOIN MedicalHistory.ProductSusceptibleMixingStation AS PSMS  ON PSMS.CodeSusceptibleMixingStation = HD.CodeSusceptibleMixingStation
    INNER JOIN MedicalHistory.PharmaDose AS PD  ON PD.CodeSusceptibleMixingStation = PSMS.CodeSusceptibleMixingStation
    WHERE HD.SENDTO IN (0, 2)
    GROUP BY HD.CODCONCEC, HD.CodeSusceptibleMixingStation, PSMS.ApplicationsNumber
    HAVING PSMS.ApplicationsNumber >
            COUNT
            (
                DISTINCT CASE
                    WHEN PD.IsDispensed = 1 THEN PD.GroupingCodeDose
                END
            )
),
PendingMixingStation AS
(
    SELECT MSD.CODCONCEC
    FROM MixingStationDetail AS MSD
    GROUP BY MSD.CODCONCEC
    HAVING SUM(MSD.RequestedQuantity) > SUM(MSD.DispensedQuantity)
)
SELECT
    HC.CODCONCEC,
    HC.CODCONCEP,
    HC.CODCONCES,
    HC.FECHAORDE,
    HC.CODCENATE,
    HC.UFUCODIGO,
    HC.CODCENCOS,
    HC.IPCODPACI,
    HC.NUMINGRES,
    HC.NUMEFOLIO,
    HC.IDETIPHIS,
    HC.ORDTRANUE,
    HC.TIPOSOLICITUD,
    HC.MEDICAMENTOVALIDADO,
    HC.CODBODEGA,
    HC.CODPROSAL,
    ING.CODCAMACT,
    HC.ORDESTADO,
    ISNULL(CG.ExtramuralPharmaceuticalDispensing, 0) AS TIPOSOLICITUD1,
    CG.ExtramuralPharmaceuticalDispensing AS PERMITEEXTRA,
    1 AS RoutingMP
FROM MainData AS HC
INNER JOIN PendingMixingStation AS PMS ON PMS.CODCONCEC = HC.CODCONCEC
INNER JOIN dbo.ADINGRESO AS ING  ON ING.NUMINGRES = HC.NUMINGRES
LEFT JOIN Contract.CareGroup AS CG  ON CG.Id = ING.GENCAREGROUP
WHERE
    (
        HC.ORDENQUIMIO = 0
        AND
        (
            ING.IESTADOIN NOT IN ('F', 'C')
            OR
            (
                HC.TIPOSOLICITUD = 2
                AND ISNULL(ING.TRATAESPECIA, 0) <> 3
            )
        )
    )
    OR
    (
        HC.ORDENQUIMIO = 1
        AND ING.IESTADOIN NOT IN ('F', 'C')
    );
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que muestra las órdenes de farmacia activas (estado abierto, sin cirugía programada asociada) cuyos productos susceptibles de preparación en estación de mezclas tienen cantidades solicitadas pendientes de dispensación completa. Integra el encabezado de órdenes farmacéuticas (HCFARMEPC), el detalle de productos para mezcla (HCFARMEPD y ProductSusceptibleMixingStation) y las dosis registradas (PharmaDose) para identificar brechas entre lo solicitado y lo dispensado. Cubre tres escenarios clínicos mediante UNION: (1) órdenes generales con ingreso activo o solicitudes ambulatorias especiales, (2) ingresos ambulatorios oncológicos sin quimioterapia, y (3) procesos oncológicos de quimioterapia. Para cada orden expone la cédula del paciente, número de ingreso, profesional de salud, centro de atención, unidad funcional, bodega, estado de la orden, tipo de solicitud y si el grupo de contrato permite dispensación extramural, siendo utilizada principalmente por el módulo de estación de mezclas y farmacia oncológica para enrutar y gestionar la preparación pendiente de medicamentos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewPharmacyMixingStationWithOncology';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewPharmacyMixingStationWithOncology';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone las órdenes farmacéuticas activas pendientes de dispensación que deben prepararse en estación de mezclas, distinguiendo casos no oncológicos, ambulatorios oncológicos no quimioterapia y procesos de quimioterapia.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewPharmacyMixingStationWithOncology';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las órdenes deben estar en estado activo (ORDESTADO=''1'') y no estar asociadas a una agenda de programación quirúrgica (IDAGEPROGQX IS NULL).; Debe existir correspondencia entre la orden y un ingreso vigente en ADINGRESO.; Los detalles de la orden deben tener SENDTO en (0,2) para ser considerados en el control de dispensación.; Para el bloque de quimioterapia, el ingreso debe estar asociado obligatoriamente a un CareGroup.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewPharmacyMixingStationWithOncology';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca incluye órdenes con ORDESTADO distinto de ''1''.; Nunca incluye órdenes ligadas a agenda de programación quirúrgica (IDAGEPROGQX IS NULL obligatorio).; Solo expone órdenes con saldo pendiente: cantidad solicitada > cantidad dispensada.; Excluye ingresos finalizados (''F'') o cancelados (''C''), salvo la excepción de solicitudes ambulatorias TIPOSOLICITUD=2 con TRATAESPECIA<>3.; El campo RoutingMP siempre se entrega con valor 1.; TIPOSOLICITUD nulo se normaliza a 0 y ExtramuralPharmaceuticalDispensing nulo se normaliza a 0.; La dispensación se cuenta a nivel de GroupingCodeDose distintos para evitar doble conteo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewPharmacyMixingStationWithOncology';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Estación de mezclas farmacéuticas; Orden médica farmacéutica; Quimioterapia; Oncología ambulatoria; Dosis farmacéutica; Dispensación de medicamentos; Producto susceptible de mezcla; Ingreso/admisión del paciente; Grupo de atención (CareGroup); Dispensación farmacéutica extramural; Tratamiento especial', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewPharmacyMixingStationWithOncology';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ViewPharmacyMixingStationWithOncology: Cuando ORDENQUIMIO=0 y el ingreso no está finalizado/cancelado (IESTADOIN NOT IN (''F'',''C'')), o bien la solicitud es ambulatoria (TIPOSOLICITUD=2) con TRATAESPECIA<>3, se retorna la orden como caso no quimioterapia.; [RETURN_RESULT] ViewPharmacyMixingStationWithOncology: Cuando ORDENQUIMIO=0, TIPOSOLICITUD=2, TRATAESPECIA=3 y el ingreso no está en estado ''F'' o ''C'', se retorna la orden como ambulatorio oncológico no quimioterapia.; [RETURN_RESULT] ViewPharmacyMixingStationWithOncology: Cuando ORDENQUIMIO=1 y el ingreso no está en estado ''F'' o ''C'', se retorna la orden como proceso oncológico de quimioterapia.; [RETURN_RESULT] ViewPharmacyMixingStationWithOncology: Solo se retornan órdenes cuya cantidad solicitada (ApplicationsNumber) supere a la cantidad efectivamente dispensada (GroupingCodeDose distintos con IsDispensed=1), garantizando que existan dosis pendientes.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewPharmacyMixingStationWithOncology';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ISNULL(ORDENQUIMIO,0)=0 y IESTADOIN NOT IN (''F'',''C'') → Incluye la orden en el bloque general de mezclas no oncológicas. else Se evalúan las ramas oncológicas.; si ORDENQUIMIO=0, TIPOSOLICITUD=2 y TRATAESPECIA=3 → Incluye la orden como ambulatorio oncológico que no es quimioterapia.; si ORDENQUIMIO=1 → Incluye la orden como proceso oncológico de quimioterapia.; si PharmaDose.IsDispensed=1 → Cuenta la dosis (GroupingCodeDose distinto) como dispensada para comparar contra ApplicationsNumber. else No se considera dispensada.; si TIPOSOLICITUD=2 y TRATAESPECIA<>3 (en bloque general) → Permite incluir la orden incluso si IESTADOIN está en ''F'' o ''C''. else Aplica la exclusión normal de ingresos finalizados/cancelados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewPharmacyMixingStationWithOncology';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFARMEPC; dbo.HCFARMEPD; MedicalHistory.ProductSusceptibleMixingStation; MedicalHistory.PharmaDose; dbo.ADINGRESO; Contract.CareGroup', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewPharmacyMixingStationWithOncology';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewPharmacyMixingStationWithOncology';
GO
