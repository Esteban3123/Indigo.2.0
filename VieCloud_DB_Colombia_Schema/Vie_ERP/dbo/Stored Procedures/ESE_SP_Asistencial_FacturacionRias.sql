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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de facturación y reporte RIAS (Rutas Integrales de Atención en Salud) para el área asistencial. Consolida en un único resultado la información de atenciones facturadas dentro de un rango de fechas, combinando datos de citas agendadas, ingresos/admisiones, historias clínicas, pacientes, entidades aseguradoras, centros de atención, unidades funcionales, actividades médicas, diagnósticos CIE-10, códigos CUPS y valores facturados (precio de venta). Sirve para generar el reporte de producción asistencial con corte financiero, identificando qué servicios se prestaron, a qué paciente (cédula, nombre, edad, sexo, contacto), bajo qué contrato o entidad pagadora, si la atención aplica a alguna RIAS y cuál fue el valor cobrado. Une dos fuentes: atenciones originadas desde citas programadas (con vínculo a la orden de facturación) y atenciones registradas directamente desde la historia clínica sin cita previa, útil para auditoría, control de glosas y análisis de cumplimiento de rutas de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Asistencial_FacturacionRias';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Asistencial_FacturacionRias';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera reporte de facturación de servicios asistenciales asociados a Rutas Integrales de Atención en Salud (RIAS) por rango de fechas de historia clínica, combinando atenciones con cita y atenciones sin cita.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_FacturacionRias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@FechaIni y @FechaFin deben acotar la fecha de la historia clínica (FECHISPAC).; Deben existir facturas en Billing.Invoice con STATUS=1 y detalles relacionados en InvoiceDetail, RevenueControlDetail y ServiceOrderDetail.; El detalle de orden de servicio debe tener RecordType=1 (procedimientos/CUPS).; La historia clínica debe ser de tipo ''I'' (TIPHISPAC=''I'') y con INDICAPAC distinto de 13.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_FacturacionRias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran facturas activas (Billing.Invoice.STATUS=1).; Solo se consideran ítems de orden de servicio con RecordType=1.; Solo historias clínicas de hospitalización/internación (TIPHISPAC=''I'') excluyendo INDICAPAC=13.; El segundo bloque del UNION nunca incluye ingresos que ya tengan cita registrada en AGASICITA/ADCONCOEX, evitando duplicados.; En el primer bloque, el CUPS reportado debe coincidir entre el catálogo (Cup2.Code) y el código IPS de la actividad médica (ag.CODSERIPS).; La edad se calcula como diferencia de años entre la fecha actual y la fecha de nacimiento (no ajustada por mes/día).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_FacturacionRias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'RIAS (Rutas Integrales de Atención en Salud); CUPS; Paciente; Ingreso/Admisión; Historia clínica; Cita médica; Diagnóstico CIE-10; Entidad responsable de pago; Centro de atención; Unidad funcional; Actividad médica; Factura de venta; Tipo de consulta (Primera vez/Control); Tipo de identificación (CC, CE, TI, RC, PA, AS, MS, NUIP)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_FacturacionRias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve un resultset unificado (UNION ALL) con datos de entidad, paciente, ingreso, diagnóstico, actividad RIAS, CUPS y valor facturado para historias en el rango de fechas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_FacturacionRias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IDRIASCUPS NOT NULL en la actividad médica → Marca AplicaRIAS=''SI'' else AplicaRIAS queda en cadena vacía; si CODTIPCIT = 0 en la cita → TipoConsulta=''Primera Vez'' else TipoConsulta=''Control''; si Historia con IDMODELOHC no nulo y sin cita asociada (NUMINGRES no presente en AGASICITA/ADCONCOEX) → Se incluye en el segundo bloque del UNION con AplicaRIAS=''SinDato-''+modelo, TipoConsulta=''SinDato'', Actividad=''SinActividad'', Rias=''SinRias'' else Se reporta vía primer bloque (con cita y actividad médica); si IPTIPODOC del paciente (1..8) → Mapea a etiquetas CC, CE, TI, RC, PA, AS, MS, NUIP; si IPSEXOPAC = 1 / 2 → Mapea a ''M'' / ''F''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_FacturacionRias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_FacturacionRias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGASICITA; dbo.ADCONCOEX; dbo.ADINGRESO; dbo.HCHISPACA; dbo.INENTIDAD; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.INPACIENT; dbo.AGACTIMED; Contract.CUPSEntity; dbo.INDIAGNOS; dbo.RIASCUPS; dbo.RIAS; Billing.Invoice; Billing.InvoiceDetail; Billing.RevenueControlDetail; Billing.ServiceOrderDetail; dbo.PRMODELOHC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_FacturacionRias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_FacturacionRias';
-- GO
