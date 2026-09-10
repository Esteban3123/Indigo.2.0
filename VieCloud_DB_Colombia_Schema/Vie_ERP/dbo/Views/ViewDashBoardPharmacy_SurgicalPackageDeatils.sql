CREATE VIEW [dbo].[ViewDashBoardPharmacy_SurgicalPackageDeatils]
AS


WITH BaseA AS (
    SELECT
        A.ID,
        A.NUMINGRES,
        A.CODPRODUC,
        A.TIPOREGIS,
        A.CANPEDPRO,
        A.CANPENPRO,
        A.NOPOSPROD,
        A.CODUNIMED,
        A.CODPROSAL,
        A.IDETIPHIS,
        A.NUMEFOLIO,
        A.IPCODPACI,
        A.CODCONCEC,
        A.PROESTADO,
        A.IDAGEPROGQX,
        A.CODSERIPS_QX
    FROM dbo.HCFARMEPD AS A
    WHERE A.CANPENPRO > 0 AND A.IDAGEPROGQX IS NOT NULL
),
BaseKeys AS (
    SELECT DISTINCT
        NUMINGRES,
        CODPRODUC,
        CODCONCEC
    FROM BaseA
),
PackageRows AS (
    SELECT
        NPOD.Id,
        NPO.Id AS PackageId,
        NPO.NUMINGRES,
        NPOD.Quantity,
        PAQ.NOMBRE,
        NPOD.CODPRODUC,
        NPO.IDHCFARMEPC,
        NPO.IDAGPAQUETES,
        PAQ.ParameterizationCostCenter
    FROM MedicalHistory.NursingPackagesOrder AS NPO 
    INNER JOIN MedicalHistory.NursingPackagesOrderDetail AS NPOD  ON NPO.Id = NPOD.IdNursingPackagesOrder
    INNER JOIN BaseKeys AS BK ON BK.NUMINGRES = NPO.NUMINGRES AND BK.CODPRODUC = NPOD.CODPRODUC AND BK.CODCONCEC = NPO.IDHCFARMEPC
    INNER JOIN dbo.AGPAQUETES AS PAQ  ON PAQ.ID = NPO.IDAGPAQUETES
),
RelevantPackageCostCenterKeys AS (
    SELECT DISTINCT
        IDAGPAQUETES,
        CODPRODUC
    FROM PackageRows
    WHERE ParameterizationCostCenter = 1
),
PackageCostCenter AS (
    SELECT
        PAQD.IDAGPAQUETE,
        PAQD.CODPRODUC,
        MIN(PAQD.IdCostCenter) AS IdCostCenter
    FROM dbo.AGPAQUETESD AS PAQD 
    INNER JOIN RelevantPackageCostCenterKeys AS K ON K.IDAGPAQUETES = PAQD.IDAGPAQUETE AND K.CODPRODUC = PAQD.CODPRODUC
    GROUP BY PAQD.IDAGPAQUETE, PAQD.CODPRODUC
),
NursingPackageDetail AS (
    SELECT
        PR.Id,
        PR.PackageId,
        PR.NUMINGRES,
        PR.Quantity,
        PR.NOMBRE,
        PR.CODPRODUC,
        PR.IDHCFARMEPC,
        PCC.IdCostCenter
    FROM PackageRows AS PR
    LEFT JOIN PackageCostCenter AS PCC ON PR.ParameterizationCostCenter = 1 AND PCC.IDAGPAQUETE = PR.IDAGPAQUETES AND PCC.CODPRODUC = PR.CODPRODUC
)
SELECT
    CAST(NEWID()as varchar(50)) as Id,
    RTRIM(A.NUMINGRES) AS Ingreso,
    RTRIM(C.Name) AS Entidad,
    C.Code AS CodigoEntidad,
    B.CODCONTRA AS CodigoContrato,
    B.CODPANATE AS CodigoPlan,
    RTRIM(CG.Name) AS ContratoPlan,
    RTRIM(D.DESPRODUC) AS Producto,
    RTRIM(A.CODPRODUC) AS CodProducto,
    CASE WHEN D.TIPPRODUC = 3 THEN '1' ELSE A.TIPOREGIS END AS Tipo,
    ISNULL(DR.Quantity, A.CANPEDPRO) AS CantidadSolicitada,
    ISNULL(DR.Quantity, 0) AS CantidadPaqueteEnf,
     CAST(0 AS INT) AS CantidadEntregada,
     ISNULL(
         CAST(
             CASE
                 WHEN A.CANPEDPRO = 0 THEN 0
                 WHEN A.CANPEDPRO >= ISNULL(DR.Quantity, A.CANPEDPRO) THEN
                     -- CANPEDPRO = suma de paquetes: distribuir proporcionalmente
                     ROUND(CAST(A.CANPENPRO AS FLOAT) * CAST(DR.Quantity AS FLOAT) / CAST(A.CANPEDPRO AS FLOAT), 0)
                 ELSE
                     -- CANPEDPRO < DR.Quantity: usar CANPENPRO directo, tope en DR.Quantity
                     IIF(A.CANPENPRO > DR.Quantity, DR.Quantity, A.CANPENPRO)
             END
         AS INT),
         A.CANPENPRO
     ) AS CantidadPendiente,
    CAST(0 AS BIT) AS Unico,
    A.NOPOSPROD AS NOPOS,
    A.CODUNIMED AS UnidadMedida,
    CONCAT(RTRIM(A.CODPROSAL), ' - ', RTRIM(E.NOMMEDICO)) AS Medico,
    E.CODIGONIT AS NitMedico,
    CAST(0 AS INT) AS FilaSeleccionada,
    A.IDETIPHIS,
    A.NUMEFOLIO,
    '0' AS Opcion,
    '0' AS OpcionAnulado,
    '' AS Procedimiento,
    CAST(0 AS BIT) AS MarcarOpcion,
    RTRIM(A.IPCODPACI) AS CodigoPaciente,
    A.CODCONCEC AS Consecutivo,
    A.PROESTADO AS Estado,
    CONCAT(F.CODESPECI, ' - ', F.DESESPECI) AS Especialidad,
    CASE
        WHEN DR.Id IS NULL THEN CONCAT('Procedimiento QX: ', RTRIM(Ser.CODSERIPS), ' - ', RTRIM(Ser.DESSERIPS))
        ELSE CONCAT('Paquete de enfermería - ', DR.NOMBRE)
    END AS ServicioIPS,
    A.IDAGEPROGQX,
    ISNULL(DR.NOMBRE, 'Producto adicional') AS PackageCodeName,
    DR.PackageId,
    D.TIPPRODUC,
    DR.IdCostCenter
FROM BaseA AS A
INNER JOIN dbo.ADINGRESO AS B  ON A.NUMINGRES = B.NUMINGRES
INNER JOIN Contract.CareGroup AS CG  ON CG.Id = B.GENCAREGROUP
INNER JOIN Contract.HealthAdministrator AS C  ON B.GENCONENTITY = C.Id
INNER JOIN dbo.IHLISTPRO AS D  ON A.CODPRODUC = D.CODPRODUC
INNER JOIN dbo.INPROFSAL AS E  ON A.CODPROSAL = E.CODPROSAL
INNER JOIN dbo.INESPECIA AS F  ON E.CODESPEC1 = F.CODESPECI
INNER JOIN dbo.INCUPSIPS AS Ser  ON Ser.CODSERIPS = A.CODSERIPS_QX
LEFT JOIN NursingPackageDetail AS DR ON DR.NUMINGRES = A.NUMINGRES AND DR.CODPRODUC = A.CODPRODUC AND DR.IDHCFARMEPC = A.CODCONCEC
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista del dashboard de farmacia que muestra el detalle de productos farmacéuticos e insumos con cantidades pendientes de entrega asociados a procedimientos quirúrgicos programados. Integra el registro de dispensación de farmacia (HCFARMEPD) con el ingreso del paciente, el contrato y la entidad pagadora (EPS/ARS), el catálogo de productos, el profesional médico con su especialidad, y la programación quirúrgica (cirugía agendada con su código CUPS). Adicionalmente, vincula los paquetes de enfermería ordenados y su detalle de insumos para distinguir si un producto pertenece a un paquete de enfermería o es un producto adicional del procedimiento. Sirve para que el equipo de farmacia controle qué medicamentos e insumos quirúrgicos están pendientes de entregar por ingreso, cirugía, entidad pagadora, plan de contrato y médico tratante.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewDashBoardPharmacy_SurgicalPackageDeatils';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewDashBoardPharmacy_SurgicalPackageDeatils';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone, para el dashboard de farmacia, los productos pendientes de entrega asociados a cirugías programadas, distinguiendo si corresponden a un paquete de enfermería o son productos adicionales del procedimiento quirúrgico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacy_SurgicalPackageDeatils';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de relación entre el pedido farmacéutico y un ingreso, contrato, grupo de cuidado, entidad administradora, producto, profesional de salud, especialidad y procedimiento quirúrgico programado.; El procedimiento quirúrgico programado debe existir en la programación de cirugías y tener un servicio CUPS asociado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacy_SurgicalPackageDeatils';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Toda fila retornada corresponde a un pedido farmacéutico con cantidad pendiente positiva y vinculado a una cirugía programada.; Cada fila genera un identificador único nuevo (NEWID) en cada consulta, por lo que el Id no es estable entre ejecuciones.; Los campos CantidadEntregada y FilaSeleccionada siempre se exponen vacíos/nulos como placeholders de UI.; MarcarOpcion y Unico siempre se inicializan en 0 (falso).; Un pedido puede asociarse a un paquete de enfermería solo si comparten ingreso, producto y consecutivo de historia farmacéutica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacy_SurgicalPackageDeatils';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Pedido farmacéutico; Ingreso del paciente; Entidad administradora de salud; Contrato y plan; Grupo de cuidado (CareGroup); Producto farmacéutico / dispositivo; Profesional de salud / médico; Especialidad médica; Programación quirúrgica (cirugía); Procedimiento CUPS / Servicio IPS; Paquete de enfermería; Centro de costo; Cantidad solicitada / pendiente / entregada; Indicador NO POS; Unidad de medida', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacy_SurgicalPackageDeatils';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Solo retorna pedidos con CANPENPRO > 0 (cantidad pendiente por entregar) y con IDAGEPROGQX no nulo (vinculados a una cirugía programada).; [RETURN_RESULT] resultset: Cuando el pedido no cruza con un detalle de paquete de enfermería (dr.Id IS NULL) se etiqueta como ''Procedimiento QX'' con código y descripción del servicio CUPS; en caso contrario se etiqueta como ''Paquete de enfermería'' con el nombre del paquete.; [RETURN_RESULT] resultset: Cuando el tipo de producto (TIPPRODUC) es 3 el campo Tipo se fuerza a ''1''; en cualquier otro caso conserva el TIPOREGIS original del pedido.; [RETURN_RESULT] resultset: Si no hay coincidencia con detalle de paquete de enfermería, la cantidad de paquete de enfermería se reporta como 0 y el nombre del paquete como ''Producto adicional''.; [RETURN_RESULT] resultset: El centro de costo del paquete solo se asigna cuando el paquete tiene ParameterizationCostCenter = 1 y coincide producto y paquete en su detalle.; [RETURN_RESULT] resultset: El cruce con paquete de enfermería exige coincidencia simultánea de ingreso, código de producto y consecutivo de historia farmacéutica (CODCONCEC = IDHCFARMEPC).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacy_SurgicalPackageDeatils';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si D.TIPPRODUC = 3 → El tipo se reporta como ''1'' (homologación de tipo para productos especiales). else Se conserva el TIPOREGIS original del pedido.; si dr.Id IS NULL (no existe detalle de paquete de enfermería para el pedido) → ServicioIPS se compone con el código y descripción del procedimiento quirúrgico (CUPS). else ServicioIPS se compone como ''Paquete de enfermería - <nombre del paquete>''.; si AGPAQUETES.ParameterizationCostCenter = 1 → Se obtiene el centro de costo desde el detalle del paquete (AGPAQUETESD) para el producto correspondiente. else No se asigna centro de costo desde el paquete.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacy_SurgicalPackageDeatils';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFARMEPD; dbo.ADINGRESO; Contract.CareGroup; Contract.HealthAdministrator; dbo.IHLISTPRO; dbo.INPROFSAL; dbo.INESPECIA; dbo.AGEPROGQX; dbo.INCUPSIPS; MedicalHistory.NursingPackagesOrder; MedicalHistory.NursingPackagesOrderDetail; dbo.AGPAQUETES; dbo.HCFARMEPC; dbo.AGPAQUETESD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacy_SurgicalPackageDeatils';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacy_SurgicalPackageDeatils';
GO
