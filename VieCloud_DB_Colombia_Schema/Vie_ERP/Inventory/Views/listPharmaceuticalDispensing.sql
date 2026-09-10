

/***************************************************************************************************
  CAMBIO: Optimización de performance sin cambio funcional
  Objeto:    <Inventory.listPharmaceuticalDispensing>
  Fecha:     2026-03-03
  Autor:     Miguel Angel Ruiz Vega - DBA

  CONTEXTO / PROBLEMA:
  - La versión anterior construía un CTE (cte_WareHouse) que agregaba (STRING_AGG) los almacenes
    leyendo y agrupando TODA la tabla Inventory.PharmaceuticalDispensingDetail antes de filtrar
    por paciente.
  - Esto provocaba alto consumo (CPU/IO) por Hash Aggregate/Scans y potencial spill a tempdb
    en escenarios con alto volumen.

  CAMBIO REALIZADO:
  - Se reemplazó el CTE global por un OUTER APPLY correlacionado al registro de dispensación (ph.Id),
    de modo que el STRING_AGG se calcule únicamente para las dispensaciones resultantes del filtro.
  - Se reordenó el flujo lógico para aplicar primero el filtro del paciente (pac.IPCODPACI),
    reduciendo cardinalidad antes de la agregación.

  IMPACTO / BENEFICIO:
  - Menor lectura de datos (IO) y menor uso de CPU.
  - Evita agregaciones masivas innecesarias y mejora la estabilidad del plan.
  - Resultado funcional: SIN CAMBIOS (mismas columnas y valores esperados).
***************************************************************************************************/

CREATE VIEW [Inventory].[listPharmaceuticalDispensing]  
AS  
  
SELECT
    ph.Id,
    ph.Code,
    ph.AdmissionNumber,
    RTRIM(pac.IPCODPACI) AS CodePatient,
    pac.IPNOMCOMP        AS Patient,
    ph.DocumentDate,
    ph.Status,
    wh.Warehouse,
    CONCAT(ph.CreationUser, ' - ', ps.Fullname) AS CreationUser
FROM dbo.INPACIENT AS pac WITH (NOLOCK)
JOIN dbo.ADINGRESO AS ad WITH (NOLOCK)
    ON ad.IPCODPACI = pac.IPCODPACI
JOIN Inventory.PharmaceuticalDispensing AS ph WITH (NOLOCK)
    ON ph.AdmissionNumber = ad.NUMINGRES
OUTER APPLY  --2026-03-03
(
    SELECT STRING_AGG(CONCAT(w.Code, ' - ', w.Name), ', ') AS Warehouse
    FROM
    (
SELECT DISTINCT phd.WarehouseId
FROM Inventory.PharmaceuticalDispensingDetail AS phd WITH (NOLOCK)
WHERE phd.PharmaceuticalDispensingId = ph.Id
    ) AS d
    JOIN Inventory.Warehouse AS w WITH (NOLOCK)
        ON w.Id = d.WarehouseId
) AS wh
LEFT JOIN Security.[UserInt] AS us
    ON us.UserCode = ph.CreationUser
LEFT JOIN Security.PersonInt AS ps
    ON ps.Id = us.IdPerson
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista consolidada de documentos de dispensación farmacéutica emitidos en el sistema. Combina el encabezado de cada despacho de medicamentos con los datos del paciente (cédula, nombre completo) obtenidos a través del ingreso hospitalario, las bodegas o almacenes desde donde se surtieron los ítems (agrupadas por documento), y el usuario que creó el registro. Sirve para consultar y hacer seguimiento al ciclo de vida de las dispensaciones: código del documento, número de ingreso del paciente, fecha, estado y bodega(s) involucradas. Es la vista principal de búsqueda y reporte del módulo de farmacia para gestión de entregas de medicamentos a pacientes hospitalizados o en atención.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'listPharmaceuticalDispensing';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'listPharmaceuticalDispensing';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista que lista las dispensaciones farmacéuticas por paciente ingresado, mostrando datos del documento, paciente, almacenes involucrados y usuario creador con su nombre completo.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'listPharmaceuticalDispensing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe existir en el maestro de pacientes y tener al menos un ingreso asociado para que aparezca su dispensación; La dispensación farmacéutica debe estar vinculada a un número de admisión existente', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'listPharmaceuticalDispensing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen dispensaciones cuyo AdmissionNumber corresponde a un ingreso (ADINGRESO) de un paciente registrado (INPACIENT) — el JOIN es INNER; El listado de almacenes (Warehouse) se concatena como ''Code - Name'' separado por coma, eliminando duplicados por WarehouseId dentro de la misma dispensación; Si la dispensación no tiene detalles, el campo Warehouse queda NULL (OUTER APPLY); El usuario creador se muestra como ''CreationUser - Fullname''; si no existe el usuario o persona asociada, Fullname es NULL (LEFT JOIN); El código de paciente se entrega sin espacios a la derecha (RTRIM); Se usa NOLOCK en lecturas, asumiendo tolerancia a lecturas sucias', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'listPharmaceuticalDispensing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso/Admisión; Dispensación farmacéutica; Detalle de dispensación; Almacén/Bodega; Usuario creador', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'listPharmaceuticalDispensing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Inventory.listPharmaceuticalDispensing: Devuelve una fila por dispensación farmacéutica cuyo número de admisión coincide con un ingreso de un paciente existente', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'listPharmaceuticalDispensing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INPACIENT; dbo.ADINGRESO; Inventory.PharmaceuticalDispensing; Inventory.PharmaceuticalDispensingDetail; Inventory.Warehouse; Security.UserInt; Security.PersonInt', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'listPharmaceuticalDispensing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'listPharmaceuticalDispensing';
GO
