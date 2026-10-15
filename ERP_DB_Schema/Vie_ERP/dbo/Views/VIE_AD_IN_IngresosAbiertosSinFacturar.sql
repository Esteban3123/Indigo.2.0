


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