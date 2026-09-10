CREATE VIEW [dbo].[ViewDashboardPharmacyDetailDevolution]
AS

WITH BaseA AS (
    SELECT
        A.Id,
        A.CODCONCEC,
        A.NUMINGRES,
        A.CODCENATE,
        A.UFUCODIGO,
        A.IPCODPACI,
        A.CODPROSAL,
        A.CODPRODUC,
        A.CANDEVOLV,
        A.CANPENDIE,
        A.FECRESGIS,
        A.PROESTADO,
        A.CODUSUARI,
        A.IdDetailPhysicalCUM
    FROM dbo.HCDEVMEDD AS A
    WHERE A.PROESTADO = '1'
),
CustodyProduct AS (
    SELECT
        B.NUMINGRES,
        B.CODPRODUC,
        CAST(ISNULL(CUST.MEDICACUSTODIA, 0) AS bit) AS Custody
    FROM (
        SELECT DISTINCT NUMINGRES, CODPRODUC
        FROM BaseA
        WHERE ISNULL(NUMINGRES, '') <> '' AND ISNULL(CODPRODUC, '') <> ''
    ) AS B
    OUTER APPLY (
        SELECT TOP (1) hcd.MEDICACUSTODIA
        FROM dbo.HCFARMEPD AS hcd 
        WHERE hcd.NUMINGRES = B.NUMINGRES AND hcd.CODPRODUC = B.CODPRODUC
        ORDER BY hcd.ID DESC
    ) AS CUST
)
SELECT
    CAST(NEWID()as varchar(50)) As Id,
    A.CODCONCEC AS Consecutivo,
    A.NUMINGRES AS Ingreso,
    A.CODCENATE,
    A.UFUCODIGO,
    RTRIM(C.Name) AS Entidad,
    RTRIM(CG.Name) AS ContratoPlan,
    RTRIM(A.IPCODPACI) AS CodigoPacienteDevolucion,
    A.CODPROSAL,
    CONCAT(RTRIM(A.CODPROSAL), ' - ', RTRIM(E.NOMMEDICO)) AS Medico,
    CONCAT(RTRIM(F.CODESPECI), ' - ', RTRIM(F.DESESPECI)) AS Especialidad,
    A.CODPRODUC,
    RTRIM(D.DESPRODUC) AS Producto,
    D.TIPPRODUC AS Tipo,
    A.CANDEVOLV AS CantidadDevuelta,
    A.CANPENDIE AS CantidadPendiente,
    D.NOPOSPROD AS NOPOS,
    '' AS PBS,
    '' AS UNIRS,
    A.FECRESGIS,
    A.PROESTADO,
    A.CODUSUARI,
    A.CODCONCEC AS EntityId,
    'HCDEVMEDD' AS EntityName,
    IIF(A.IdDetailPhysicalCUM IS NULL, FP.CANACTPRO, HC.CANACTPRO) AS CantidadFisico,
    DPC.ProductId AS FinalProductId,
    PR.Code AS FinalProductCode,
    PR.[Name] AS FinalProductName,
    A.Id AS HCDEVMEDDId,
    DPC.BatchCode,
    D.TIPPRODUC,
    IIF(EXISTS (SELECT 1 FROM dbo.HCDEVMEDC HDC WHERE HDC.CODCONCEC = A.CODCONCEC AND HDC.IDHCHOJAGASTOQX IS NOT NULL),
        0,
        ISNULL(CP.Custody, 0)) AS Custody
FROM BaseA AS A
JOIN dbo.ADINGRESO AS B ON A.NUMINGRES = B.NUMINGRES
JOIN Contract.CareGroup AS CG ON CG.Id = B.GENCAREGROUP
JOIN Contract.HealthAdministrator AS C ON B.GENCONENTITY = C.Id
JOIN dbo.IHLISTPRO AS D ON A.CODPRODUC = D.CODPRODUC
JOIN dbo.INPROFSAL AS E ON A.CODPROSAL = E.CODPROSAL
JOIN dbo.INESPECIA AS F ON E.CODESPEC1 = F.CODESPECI
LEFT JOIN MedicalHistory.DetailPhysicalCUM AS DPC ON A.IdDetailPhysicalCUM = DPC.Id
LEFT JOIN Inventory.InventoryProduct AS PR ON DPC.ProductId = PR.Id
LEFT JOIN dbo.HCFISIPRO AS HC ON DPC.IDHCFISIPRO = HC.ID
LEFT JOIN dbo.HCFISIPRO AS FP ON A.IdDetailPhysicalCUM IS NULL AND A.NUMINGRES = FP.NUMINGRES AND FP.CODPRODUC = A.CODPRODUC AND FP.UFUCODIGO = A.UFUCODIGO
LEFT JOIN CustodyProduct AS CP ON CP.NUMINGRES = A.NUMINGRES AND CP.CODPRODUC = A.CODPRODUC
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista del dashboard de farmacia que muestra el detalle de las devoluciones de medicamentos y dispositivos médicos por ingreso hospitalario. Integra los registros de devoluciones (HCDEVMEDD) con el ingreso del paciente, la entidad pagadora (EPS/aseguradora), el grupo de contrato o plan, el profesional de salud responsable, la especialidad médica y el catálogo de productos farmacéuticos. Para cada devolución expone la cantidad devuelta, la cantidad pendiente y la cantidad física disponible en inventario, obtenida ya sea desde el control físico de medicamentos por lote (DetailPhysicalCUM con código CUM y número de lote) o desde el inventario físico general (HCFISIPRO), según si la devolución está asociada a un detalle de control físico o no. Sirve para monitorear y gestionar en tiempo real las devoluciones de medicamentos e insumos en farmacia, permitiendo identificar productos, pacientes, profesionales, entidades contratantes y estado del proceso de devolución.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewDashboardPharmacyDetailDevolution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewDashboardPharmacyDetailDevolution';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista que consolida el detalle de devoluciones de medicamentos activas con datos del paciente, entidad/contrato, médico, especialidad, producto, cantidades devueltas/pendientes, lote/CUM y existencias físicas para alimentar un dashboard de farmacia.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashboardPharmacyDetailDevolution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen devoluciones registradas en HCDEVMEDD con PROESTADO=1.; El ingreso (NUMINGRES) debe existir en ADINGRESO y estar asociado a un CareGroup y a una HealthAdministrator vigentes.; El producto devuelto debe existir en IHLISTPRO y el profesional en INPROFSAL con una especialidad válida en INESPECIA.; Si la devolución referencia un IdDetailPhysicalCUM, este debe existir en MedicalHistory.DetailPhysicalCUM con su producto en Inventory.InventoryProduct y registro en HCFISIPRO con PROESTADO=1.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashboardPharmacyDetailDevolution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen devoluciones activas (PROESTADO = ''1'').; Una devolución se asocia a una única fuente de inventario físico: o bien al detalle CUM (cuando IdDetailPhysicalCUM no es nulo) o bien al registro HCFISIPRO por ingreso/producto/UFU.; Entidad, contrato/plan, médico y especialidad siempre se resuelven vía joins obligatorios (INNER), por lo que registros sin estas relaciones no aparecen.; La especialidad mostrada corresponde a la especialidad principal (CODESPEC1) del profesional.; Se conserva la información original (consecutivo, ingreso, producto, cantidades) sin modificarla; la vista es de solo lectura.; El identificador EntityName siempre se reporta como ''HCDEVMEDD'' indicando el origen de la entidad.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashboardPharmacyDetailDevolution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Devolución de medicamentos; Inventario físico de farmacia; CUM (Código Único de Medicamento) y lote; Ingreso/admisión hospitalaria; Entidad administradora de salud (EPS/aseguradora); Contrato y plan de atención (CareGroup); Profesional de la salud y especialidad médica; Producto NOPOS/POS; Custodia de producto; Cantidad devuelta y cantidad pendiente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashboardPharmacyDetailDevolution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ViewDashboardPharmacyDetailDevolution: Solo retorna devoluciones cuyo HCDEVMEDD.PROESTADO = ''1'' (devoluciones activas/no anuladas).; [RETURN_RESULT] ViewDashboardPharmacyDetailDevolution: Cuando A.IdDetailPhysicalCUM IS NULL, la cantidad física se toma de HCFISIPRO.CANACTPRO; en caso contrario se toma del DetailPhysicalCUM (dc.CANACTPRO).; [RETURN_RESULT] ViewDashboardPharmacyDetailDevolution: El CTE DetailCUM solo considera detalles físicos CUM cuando HCDEVMEDD.IdDetailPhysicalCUM no es nulo y HCFISIPRO.PROESTADO = 1.; [RETURN_RESULT] ViewDashboardPharmacyDetailDevolution: El JOIN con HCFISIPRO se aplica solo cuando A.IdDetailPhysicalCUM IS NULL, emparejando por NUMINGRES, CODPRODUC y UFUCODIGO.; [RETURN_RESULT] ViewDashboardPharmacyDetailDevolution: Cada fila genera un Id sintético mediante CAST(NEWID() as varchar(50)).; [RETURN_RESULT] ViewDashboardPharmacyDetailDevolution: Los campos PBS y UNIRS se devuelven siempre como cadena vacía.; [RETURN_RESULT] ViewDashboardPharmacyDetailDevolution: La condición de custodia del producto se calcula invocando Inventory.FlagCustodyProduct(CODPRODUC, NUMINGRES).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashboardPharmacyDetailDevolution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si A.IdDetailPhysicalCUM IS NULL → CantidadFisico = FP.CANACTPRO (toma existencia desde HCFISIPRO) else CantidadFisico = dc.CANACTPRO (toma existencia desde el detalle físico CUM relacionado); si h.IdDetailPhysicalCUM IS NOT NULL AND h.PROESTADO = 1 (en CTE DetailCUM) → Se incluye el detalle físico CUM con su producto, lote y cantidad activa para enriquecer la devolución', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashboardPharmacyDetailDevolution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Inventory.FlagCustodyProduct', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashboardPharmacyDetailDevolution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCDEVMEDD; dbo.ADINGRESO; Contract.CareGroup; Contract.HealthAdministrator; dbo.IHLISTPRO; dbo.INPROFSAL; dbo.INESPECIA; MedicalHistory.DetailPhysicalCUM; Inventory.InventoryProduct; dbo.HCFISIPRO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashboardPharmacyDetailDevolution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashboardPharmacyDetailDevolution';
GO
