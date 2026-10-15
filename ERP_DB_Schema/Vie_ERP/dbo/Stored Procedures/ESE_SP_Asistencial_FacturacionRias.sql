CREATE PROCEDURE [dbo].[ESE_SP_Asistencial_FacturacionRias] @FechaIni DATETIME, 
                                                           @FechaFin DATETIME
AS
     SELECT E.NOMENTIDA AS Entidad, 
            CC.NOMCENATE AS CentroAtencion, 
            I.IFECHAING AS FechaIngreso, 
            HC.FECHISPAC AS FechaHistoria, 
            I.NUMINGRES AS Ingreso,
            CASE P.IPTIPODOC
                WHEN 1
                THEN 'CC'
                WHEN 2
                THEN 'CE'
                WHEN 3
                THEN 'TI'
                WHEN 4
                THEN 'RC '
                WHEN 5
                THEN 'PA'
                WHEN 6
                THEN 'AS'
                WHEN 7
                THEN 'MS'
                WHEN 8
                THEN 'NUIP'
            END AS 'TipoIdentificacion', 
            RTRIM(I.IPCODPACI) AS 'Identificacion', 
            P.IPNOMCOMP AS Paciente, 
            CAST(P.IPFECNACI AS DATE) AS 'FechaNacimiento', 
            YEAR([Common].[GETDATE]()) - YEAR(P.IPFECNACI) AS Edad,
            CASE P.IPSEXOPAC
                WHEN 1
                THEN 'M'
                WHEN 2
                THEN 'F'
            END AS 'Sexo', 
            P.IPTELMOVI AS TelCelular, 
            P.IPTELEFON AS TelFijo, 
            P.IPDIRECCI AS Direccion, 
            IIF(C.IDRIASCUPS IS NOT NULL, 'SI', '') AS AplicaRIAS, 
            IIF(c.CODTIPCIT = 0, 'Primera Vez', 'Control') AS TipoConsulta, 
            UF.UFUDESCRI AS UnidadFuncional, 
            I.CODDIAEGR AS 'CodCIE-10', 
            Diag.NOMDIAGNO AS Diagnostico, 
            ag.DESACTMED AS Actividad, 
            rias.NOMBRE AS Rias, 
            ag.APLICARIAS AS AplicaRiasActividad, 
            Cup2.Code AS Cups, 
            cup2.Description AS DescripcionCups, 
            dos.TotalSalesPrice AS Valor
     FROM AGASICITA C WITH(NOLOCK)
          INNER JOIN ADCONCOEX F WITH(NOLOCK) ON F.NUMCONCIT = C.CODAUTONU
          INNER JOIN ADINGRESO I WITH(NOLOCK) ON I.NUMINGRES = F.NUMINGRES
          INNER JOIN.HCHISPACA AS HC WITH(NOLOCK) ON I.NUMINGRES = HC.NUMINGRES
                                                     AND I.IPCODPACI = HC.IPCODPACI
          INNER JOIN INENTIDAD E WITH(NOLOCK) ON E.CODENTIDA = I.CODENTIDA
          INNER JOIN ADCENATEN CC WITH(NOLOCK) ON CC.CODCENATE = I.CODCENATE
          INNER JOIN INUNIFUNC AS UF WITH(NOLOCK) ON UF.UFUCODIGO = F.UFUCODIGO
          INNER JOIN INPACIENT P WITH(NOLOCK) ON P.IPCODPACI = C.IPCODPACI
          INNER JOIN AGACTIMED AS ag WITH(NOLOCK) ON c.CODACTMED = ag.CODACTMED
          LEFT OUTER JOIN Contract.CUPSEntity AS Cup WITH(NOLOCK) ON Cup.code = ag.CODSERIPS
          LEFT OUTER JOIN INDIAGNOS AS Diag WITH(NOLOCK) ON I.CODDIAEGR = Diag.CODDIAGNO
          LEFT OUTER JOIN RIASCUPS AS riasc WITH(NOLOCK) ON riasc.id = ag.IDRIASCUPS
          LEFT OUTER JOIN RIAS AS rias WITH(NOLOCK) ON riasc.IDRIAS = rias.ID
          LEFT OUTER JOIN Billing.Invoice AS FI WITH(NOLOCK) ON I.NUMINGRES = FI.AdmissionNumber
                                                                AND FI.STATUS = 1
          INNER JOIN Billing.InvoiceDetail AS DF WITH(NOLOCK) ON DF.InvoiceId = FI.Id
          INNER JOIN Billing.RevenueControlDetail rcd WITH(NOLOCK) ON rcd.Id = FI.RevenueControlDetailId
          INNER JOIN Billing.ServiceOrderDetail AS dos WITH(NOLOCK) ON dos.Id = DF.ServiceOrderDetailId
          INNER JOIN Contract.CUPSEntity AS Cup2 WITH(NOLOCK) ON Cup2.Id = dos.CUPSEntityId
                                                                 AND Cup2.Code = ag.CODSERIPS
     WHERE HC.TIPHISPAC = 'I'
           AND HC.INDICAPAC <> 13
           AND dos.RecordType = 1
           AND CAST(HC.FECHISPAC AS DATE) BETWEEN @FechaIni AND @FechaFin
     UNION ALL
     SELECT E.NOMENTIDA AS Entidad, 
            CC.NOMCENATE AS CentroAtencion, 
            I.IFECHAING AS FechaIngreso, 
            H.FECHISPAC AS FechaHistoria, 
            I.NUMINGRES AS Ingreso,
            CASE P.IPTIPODOC
                WHEN 1
                THEN 'CC'
                WHEN 2
                THEN 'CE'
                WHEN 3
                THEN 'TI'
                WHEN 4
                THEN 'RC '
                WHEN 5
                THEN 'PA'
                WHEN 6
                THEN 'AS'
                WHEN 7
                THEN 'MS'
                WHEN 8
                THEN 'NUIP'
            END AS 'TipoIdentificacion', 
            RTRIM(I.IPCODPACI) AS 'Identificacion', 
            P.IPNOMCOMP AS Paciente, 
            CAST(P.IPFECNACI AS DATE) AS 'FechaNacimiento', 
            YEAR([Common].[GETDATE]()) - YEAR(P.IPFECNACI) AS Edad,
            CASE P.IPSEXOPAC
                WHEN 1
                THEN 'M'
                WHEN 2
                THEN 'F'
            END AS 'Sexo', 
            P.IPTELMOVI AS TelCelular, 
            P.IPTELEFON AS TelFijo, 
            P.IPDIRECCI AS Direccion, 
            'SinDato' + '-' + PH.DESCRIPCION AS AplicaRIAS, 
            'SinDato' AS TipoConsulta, 
            UF.UFUDESCRI AS UnidadFuncional, 
            I.CODDIAEGR AS 'CodCIE-10', 
            Diag.NOMDIAGNO AS Diagnostico, 
            'SinActividad' AS Actividad, 
            'SinRias' AS Rias, 
            0 AS AplicaRiasActividad, 
            Cup2.Code AS Cups, 
            cup2.Description AS DescripcionCups, 
            dos.TotalSalesPrice AS Valor
     FROM HCHISPACA H
          INNER JOIN PRMODELOHC PH WITH(NOLOCK) ON H.IDMODELOHC = PH.ID
          INNER JOIN ADINGRESO I WITH(NOLOCK) ON I.NUMINGRES = H.NUMINGRES
          INNER JOIN INENTIDAD E WITH(NOLOCK) ON E.CODENTIDA = I.CODENTIDA
          INNER JOIN ADCENATEN CC WITH(NOLOCK) ON CC.CODCENATE = I.CODCENATE
          INNER JOIN INPACIENT P WITH(NOLOCK) ON P.IPCODPACI = I.IPCODPACI
          INNER JOIN INUNIFUNC AS UF WITH(NOLOCK) ON UF.UFUCODIGO = I.UFUCODIGO
          INNER JOIN INDIAGNOS AS Diag WITH(NOLOCK) ON I.CODDIAEGR = Diag.CODDIAGNO
          LEFT OUTER JOIN Billing.Invoice AS FI WITH(NOLOCK) ON I.NUMINGRES = FI.AdmissionNumber
                                                                AND FI.STATUS = 1
          INNER JOIN Billing.InvoiceDetail AS DF WITH(NOLOCK) ON DF.InvoiceId = FI.Id
          INNER JOIN Billing.RevenueControlDetail rcd WITH(NOLOCK) ON rcd.Id = FI.RevenueControlDetailId
          INNER JOIN Billing.ServiceOrderDetail AS dos WITH(NOLOCK) ON dos.Id = DF.ServiceOrderDetailId
          INNER JOIN Contract.CUPSEntity AS Cup2 WITH(NOLOCK) ON Cup2.Id = dos.CUPSEntityId
     WHERE H.TIPHISPAC = 'I'
           AND H.INDICAPAC <> 13
           AND dos.RecordType = 1
           AND CAST(H.FECHISPAC AS DATE) BETWEEN @FechaIni AND @FechaFin
           AND IDMODELOHC IS NOT NULL
           AND H.NUMINGRES NOT IN
     (
         SELECT I.NUMINGRES
         FROM AGASICITA C WITH(NOLOCK)
              INNER JOIN ADCONCOEX F WITH(NOLOCK) ON F.NUMCONCIT = C.CODAUTONU
              INNER JOIN ADINGRESO I WITH(NOLOCK) ON I.NUMINGRES = F.NUMINGRES
     );