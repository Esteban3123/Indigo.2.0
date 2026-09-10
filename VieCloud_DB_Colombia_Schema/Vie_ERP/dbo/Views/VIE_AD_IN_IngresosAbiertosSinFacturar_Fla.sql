

CREATE VIEW [dbo].[VIE_AD_IN_IngresosAbiertosSinFacturar_Fla]
AS
SELECT DISTINCT
       i.NUMINGRES AS Ingreso,
       ga.Code AS [Grupo Atención Ingreso],
       ga.Name AS [Grupo Atención],
       i.IPCODPACI AS [Identificación Paciente],
       p.IPNOMCOMP AS [Nombre Paciente],
       p.IPEXPEDIC AS [Lugar Expedición antes Vie],
       CAST(p.GENEXPEDITIONCITY AS VARCHAR(20)) + ' - ' + ISNULL(ci.Name, '') AS [Lugar expedición Vie],
       e.NOMENTIDA AS Entidad,
       i.IFECHAING AS Fecha_Ingreso,
       i.IESTADOIN AS Estado,
       uf.UFUDESCRI AS Unidad_Funcional,
       HCU.ENFACTUAL AS Enfermedad_Actual,
       em.FECALTPAC AS [Fecha alta Médica],
       fd.TotalFolio AS [Vr Pendiente Facturar],
       HC.CODDIAGNO AS CIE_10,
       CIE10.NOMDIAGNO AS Diagnóstico,
       u.NOMUSUARI AS Usuario,
       uu.NOMUSUARI AS UsuarioModifico,
       D.UFUDESCRI AS UnidadActual,
       i.IOBSERVAC AS Observaciones
FROM dbo.ADINGRESO AS i
    LEFT OUTER JOIN Billing.RevenueControl AS c
        ON c.AdmissionNumber = i.NUMINGRES
           AND c.PatientCode = i.IPCODPACI
    LEFT OUTER JOIN Billing.RevenueControlDetail AS fd
        ON fd.RevenueControlId = c.Id
    LEFT OUTER JOIN dbo.INENTIDAD AS e
        ON e.CODENTIDA = i.CODENTIDA
    INNER JOIN dbo.INUNIFUNC AS uf
        ON uf.UFUCODIGO = i.UFUCODIGO
    INNER JOIN dbo.SEGusuaru AS u
        ON u.CODUSUARI = i.CODUSUCRE
    LEFT OUTER JOIN Contract.CareGroup AS ga
        ON ga.Id = i.GENCAREGROUP
    INNER JOIN dbo.INPACIENT AS p
        ON p.IPCODPACI = i.IPCODPACI
    LEFT OUTER JOIN Common.City AS ci
        ON ci.Id = p.GENEXPEDITIONCITY
    LEFT OUTER JOIN dbo.SEGusuaru AS uu
        ON uu.CODUSUARI = i.CODUSUMOD
    LEFT OUTER JOIN dbo.HCHISPACA AS HC
        ON HC.NUMINGRES = i.NUMINGRES
           AND HC.IPCODPACI = HC.IPCODPACI
           AND HC.TIPHISPAC = 'i'
    LEFT OUTER JOIN dbo.HCURGING1 AS HCU
        ON HCU.NUMINGRES = HC.NUMINGRES
           AND HCU.IPCODPACI = HC.IPCODPACI
           AND HCU.NUMEFOLIO = HC.NUMEFOLIO
    LEFT OUTER JOIN
    (
        SELECT IPCODPACI,
               NUMINGRES,
               MAX(NUMEFOLIO) AS Folio
        FROM dbo.INDIAGNOP
        WHERE (CODDIAPRI = 'True')
        GROUP BY NUMINGRES,
                 IPCODPACI
    ) AS DX
        ON DX.IPCODPACI = HCU.IPCODPACI
           AND DX.NUMINGRES = HCU.NUMINGRES
           AND DX.Folio = HC.NUMEFOLIO
    LEFT OUTER JOIN dbo.INDIAGNOS AS CIE10
        ON CIE10.CODDIAGNO = HC.CODDIAGNO
    LEFT OUTER JOIN dbo.HCURGEVO1 AS HCU1
        ON HCU.NUMINGRES = HC.NUMINGRES
           AND HCU1.IPCODPACI = HC.IPCODPACI
           AND HCU1.NUMEFOLIO = HC.NUMEFOLIO
    LEFT OUTER JOIN dbo.ADINGRESO AS I2
        ON I2.NUMINGRES = i.NUMINGRES
    LEFT OUTER JOIN dbo.INUNIFUNC AS D
        ON I2.UFUAACTHOS = D.UFUCODIGO
    LEFT OUTER JOIN dbo.HCREGEGRE AS em
        ON em.IPCODPACI = HC.IPCODPACI
           AND em.NUMINGRES = HC.NUMINGRES
WHERE (i.CODCENATE = '002')
      AND (i.IESTADOIN <> 'F')
      AND (i.IESTADOIN <> 'A')
      AND (i.IESTADOIN <> 'C');
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting orientada a la gestión de facturación hospitalaria. Consolida los ingresos activos del centro de atención ''002'' que no han sido facturados ni cerrados (excluyendo estados F, A y C), mostrando datos del paciente, entidad aseguradora, grupo de atención contractual, unidad funcional actual, diagnóstico principal CIE-10, enfermedad actual de urgencias, fecha de alta médica y valor pendiente por facturar, para apoyar el seguimiento y control de cuentas abiertas sin liquidar.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_IN_IngresosAbiertosSinFacturar_Fla';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_IN_IngresosAbiertosSinFacturar_Fla';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los ingresos abiertos (no finalizados, anulados ni cerrados) del centro de atención ''002'' con su saldo pendiente de facturación, datos clínicos, diagnóstico principal, entidad responsable y unidad funcional actual.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_IN_IngresosAbiertosSinFacturar_Fla';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El ingreso debe pertenecer al centro de atención CODCENATE = ''002''; El estado del ingreso (IESTADOIN) no puede ser ''F'' (Facturado/Finalizado), ''A'' (Anulado) ni ''C'' (Cerrado); Para obtener diagnóstico se requiere historia clínica con TIPHISPAC = ''i'' (ingreso/internación); El diagnóstico principal se identifica con CODDIAPRI = ''True'' en INDIAGNOP', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_IN_IngresosAbiertosSinFacturar_Fla';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan ingresos del centro de atención ''002''; Nunca se devuelven ingresos en estado ''F'', ''A'' o ''C''; El diagnóstico mostrado corresponde al diagnóstico principal (CODDIAPRI=''True'') del último folio (MAX NUMEFOLIO); La historia clínica considerada es exclusivamente del tipo internación (''i''); El valor pendiente por facturar proviene del detalle de control de ingresos (RevenueControlDetail.TotalFolio)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_IN_IngresosAbiertosSinFacturar_Fla';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ingreso/Admisión; Paciente; Entidad responsable; Grupo de atención; Unidad funcional; Historia clínica de internación; Diagnóstico principal CIE-10; Enfermedad actual; Alta médica; Valor pendiente por facturar; Control de ingresos (Revenue Control); Centro de atención; Estado del ingreso; Lugar de expedición del documento', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_IN_IngresosAbiertosSinFacturar_Fla';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] VIE_AD_IN_IngresosAbiertosSinFacturar_Fla: Devuelve filas DISTINCT de ingresos cuyo CODCENATE=''002'' e IESTADOIN no esté en (''F'',''A'',''C''), enriquecidas con paciente, entidad, unidad funcional, diagnóstico CIE-10 principal y valor pendiente por facturar.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_IN_IngresosAbiertosSinFacturar_Fla';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si i.CODCENATE = ''002'' AND i.IESTADOIN NOT IN (''F'',''A'',''C'') → Se incluye el ingreso en el resultado else Se excluye del resultado; si INDIAGNOP.CODDIAPRI = ''True'' → Se considera el folio como diagnóstico principal del ingreso (MAX(NUMEFOLIO)); si HCHISPACA.TIPHISPAC = ''i'' → Se toma la historia clínica como correspondiente al ingreso/internación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_IN_IngresosAbiertosSinFacturar_Fla';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; Billing.RevenueControl; Billing.RevenueControlDetail; dbo.INENTIDAD; dbo.INUNIFUNC; dbo.SEGusuaru; Contract.CareGroup; dbo.INPACIENT; Common.City; dbo.HCHISPACA; dbo.HCURGING1; dbo.INDIAGNOP; dbo.INDIAGNOS; dbo.HCURGEVO1; dbo.HCREGEGRE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_IN_IngresosAbiertosSinFacturar_Fla';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_IN_IngresosAbiertosSinFacturar_Fla';
GO
