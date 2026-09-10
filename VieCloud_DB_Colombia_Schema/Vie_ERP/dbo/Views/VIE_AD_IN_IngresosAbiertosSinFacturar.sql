

CREATE VIEW [dbo].[VIE_AD_IN_IngresosAbiertosSinFacturar]
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
WHERE (i.CODCENATE = '001')
      AND (i.IESTADOIN <> 'F')
      AND (i.IESTADOIN <> 'A')
      AND (i.IESTADOIN <> 'C');
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Muestra todos los ingresos abiertos (activos, no finalizados, no anulados y no cancelados) del centro de atención 001 que aún no han sido completamente facturados. Integra datos del episodio de ingreso (número de ingreso, fecha, estado, observaciones), información del paciente (identificación, cédula, nombre, lugar de expedición del documento), entidad aseguradora o pagadora, unidad funcional de ingreso y unidad funcional actual de hospitalización, grupo de atención del contrato, valor pendiente por facturar según el control de ingresos de facturación, historia clínica de urgencias con la enfermedad actual y el diagnóstico principal CIE-10, fecha de alta médica, y los usuarios que crearon o modificaron el registro. Es la vista principal para gestión de cuentas por cobrar, seguimiento de pacientes hospitalizados sin factura y cierre de cuentas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VIE_AD_IN_IngresosAbiertosSinFacturar';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VIE_AD_IN_IngresosAbiertosSinFacturar';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los ingresos abiertos (no facturados, no anulados, no cerrados) del centro de atención ''001'' con datos del paciente, entidad, unidad funcional, diagnóstico principal, alta médica y valor pendiente por facturar.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_IN_IngresosAbiertosSinFacturar';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El ingreso debe pertenecer al centro de atención ''001'' (CODCENATE=''001''); El estado del ingreso (IESTADOIN) no puede ser ''F'' (facturado), ''A'' (anulado) ni ''C'' (cerrado)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_IN_IngresosAbiertosSinFacturar';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen ingresos abiertos pendientes de facturación (excluye estados F, A, C); Solo se considera información del centro de atención ''001''; Para el diagnóstico principal se usa el último folio (MAX(NUMEFOLIO)) marcado como CODDIAPRI=''True''; Se asocia únicamente la historia clínica con TIPHISPAC=''i''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_IN_IngresosAbiertosSinFacturar';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ingreso/Admisión; Paciente; Entidad (aseguradora/pagador); Unidad funcional; Grupo de atención; Historia clínica; Diagnóstico principal CIE-10; Alta médica/Egreso; Valor pendiente por facturar; Lugar de expedición del documento', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_IN_IngresosAbiertosSinFacturar';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve únicamente ingresos del centro ''001'' cuyo estado es distinto de ''F'', ''A'' y ''C'', uniendo control de facturación, historia clínica de urgencias, diagnóstico principal y registro de egreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_IN_IngresosAbiertosSinFacturar';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si i.CODCENATE=''001'' AND i.IESTADOIN NOT IN (''F'',''A'',''C'') → Se incluye el ingreso en el resultado else Se excluye del resultado; si INDIAGNOP.CODDIAPRI=''True'' → Se considera el folio como diagnóstico principal (se toma MAX(NUMEFOLIO) por ingreso/paciente); si HCHISPACA.TIPHISPAC=''i'' → Solo se vincula la historia clínica de tipo ''i'' (ingreso/internación)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_IN_IngresosAbiertosSinFacturar';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; Billing.RevenueControl; Billing.RevenueControlDetail; dbo.INENTIDAD; dbo.INUNIFUNC; dbo.SEGusuaru; Contract.CareGroup; dbo.INPACIENT; Common.City; dbo.HCHISPACA; dbo.HCURGING1; dbo.INDIAGNOP; dbo.INDIAGNOS; dbo.HCURGEVO1; dbo.HCREGEGRE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_IN_IngresosAbiertosSinFacturar';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_IN_IngresosAbiertosSinFacturar';
GO
