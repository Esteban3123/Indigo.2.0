CREATE PROCEDURE [dbo].[ESE_SP_ordenes_medicamentos] @FechaIni DATETIME, 
                                                    @FechaFin DATETIME
AS
     SELECT FM.CODCENATE, 
            CA.NOMCENATE, 
            FM.UFUCODIGO, 
            UF.UFUDESCRI, 
            FM.NUMINGRES, 
            IG.CODENTIDA, 
            ENT.Name,
            CASE ENT.EntityType
                WHEN '1'
                THEN 'Contributivo'
                WHEN '2'
                THEN 'Subsidiado'
                WHEN '3'
                THEN 'Vinculados'
                WHEN '4'
                THEN 'Vicualados'
                WHEN '5'
                THEN 'ARL'
                WHEN '6'
                THEN 'Prepagada'
                WHEN '7'
                THEN 'IPS Privada'
                WHEN '8'
                THEN 'IPS publica'
                WHEN '9'
                THEN 'Especial'
                WHEN '10'
                THEN 'Accidentes de trasito'
                WHEN '11'
                THEN 'Fosyga'
                WHEN '12'
                THEN 'otros'
            END 'Regimen', 
            PAC.GRUPCODIGO, 
            PAC.IPTIPODOC, 
            PAC.IPCODPACI, 
            PAC.IPPRINOMB, 
            PAC.IPSEGNOMB, 
            PAC.IPPRIAPEL, 
            PAC.IPSEGAPEL, 
            PAC.IPSEXO, 
            PAC.IPFECNACI, 
            FM.CODPRODUC, 
            PRO.DESPRODUC, 
            PRO.CONCENMED, 
            PRO.PRESENMED, 
            FM.CODVIAADM, 
            VIA.DESVIAADM, 
            FM.CANPEDPRO, 
            FM.DESADMINI, 
            FM.DOSISPRFN, 
            FM.CODDIAGNO, 
            DIA.NOMDIAGNO, 
            FM.CODPROSAL, 
            ME.NOMMEDICO, 
            EF.PESOPACIE, 
            EF.FECREGITE
     FROM HCPRESCRA FM
          INNER JOIN ADCENATEN CA ON FM.CODCENATE = CA.CODCENATE
          INNER JOIN INUNIFUNC UF ON FM.UFUCODIGO = UF.UFUCODIGO
          INNER JOIN ADINGRESO IG ON FM.NUMINGRES = IG.NUMINGRES
          LEFT JOIN Contract.HealthAdministrator ENT ON IG.CODENTIDA = ENT.Code
          INNER JOIN INPACIENT PAC ON FM.IPCODPACI = PAC.IPCODPACI
          INNER JOIN IHLISTPRO PRO ON FM.CODPRODUC = PRO.CODPRODUC
          INNER JOIN HCVIAADMI VIA ON FM.CODVIAADM = VIA.CODVIAADM
          INNER JOIN INDIAGNOS DIA ON FM.CODDIAGNO = DIA.CODDIAGNO
          INNER JOIN INPROFSAL ME ON FM.CODPROSAL = ME.CODPROSAL
          LEFT JOIN HCEXFISIC EF ON FM.NUMEFOLIO = EF.NUMEFOLIO
                                    AND FM.NUMINGRES = EF.NUMINGRES
                                    AND FM.IPCODPACI = EF.IPCODPACI
     WHERE EF.FECREGITE BETWEEN @FechaIni AND @FechaFin;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de órdenes de medicamentos prescritas en un rango de fechas, orientado a la gestión farmacéutica y clínica de la institución. Consolida información de la prescripción (medicamento, vía de administración, dosis, cantidad pedida y diagnóstico asociado) junto con los datos del paciente (cédula, nombre, sexo, fecha de nacimiento, grupo sanguíneo), el profesional de la salud que ordenó el medicamento, el ingreso o episodio de atención, la sede y unidad funcional donde se prescribió, y la entidad pagadora (EPS, ARL, prepagada, etc.) con su régimen. Complementa cada prescripción con el peso del paciente tomado del examen físico registrado en la misma fecha, permitiendo análisis de dosificación y seguimiento clínico. Se utiliza para auditoría de prescripción médica, control de medicamentos, reportes de farmacia hospitalaria y seguimiento de consumo por sede, unidad, entidad o régimen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_ordenes_medicamentos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_ordenes_medicamentos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las órdenes/prescripciones de medicamentos con datos del paciente, ingreso, administradora de salud, régimen, producto, diagnóstico y profesional, para los exámenes físicos registrados en un rango de fechas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_ordenes_medicamentos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El rango de fechas debe ser válido y corresponder a la fecha de registro del examen físico (HCEXFISIC.FECREGITE).; Deben existir catálogos consistentes para centro de atención, unidad funcional, producto, vía de administración, diagnóstico y profesional de la salud, ya que los joins son obligatorios.; El ingreso debe estar registrado para vincular paciente y administradora de salud.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_ordenes_medicamentos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna prescripciones que tengan un examen físico asociado cuya fecha de registro caiga en el rango solicitado (el LEFT JOIN se vuelve obligatorio por el filtro WHERE sobre EF.FECREGITE).; Las prescripciones deben tener centro de atención, unidad funcional, ingreso, paciente, producto, vía de administración, diagnóstico y profesional de salud válidos (INNER JOIN).; La administradora de salud puede no existir y aun así se devuelve la fila (LEFT JOIN), con Regimen y nombre en NULL.; El régimen se mapea por EntityType de la administradora; tipos fuera de 1..12 producen Regimen NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_ordenes_medicamentos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden/prescripción de medicamentos; Centro de atención; Unidad funcional; Ingreso/admisión del paciente; Administradora de salud (EPS/ARS); Régimen de afiliación (Contributivo, Subsidiado, ARL, Prepagada, Fosyga, etc.); Paciente; Producto/medicamento (concentración, presentación); Vía de administración; Dosis y cantidad prescrita; Diagnóstico; Profesional de la salud (médico prescriptor); Examen físico (peso del paciente, fecha de registro)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_ordenes_medicamentos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCPRESCRA: Cuando HCEXFISIC.FECREGITE está entre @FechaIni y @FechaFin, devuelve el conjunto de prescripciones con datos demográficos, clínicos y de aseguramiento asociados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_ordenes_medicamentos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si EntityType de la administradora de salud (''1''..''12'') → Traduce el código a etiqueta de régimen: 1=Contributivo, 2=Subsidiado, 3=Vinculados, 4=Vicualados, 5=ARL, 6=Prepagada, 7=IPS Privada, 8=IPS publica, 9=Especial, 10=Accidentes de trasito, 11=Fosyga, 12=otros else NULL en columna Regimen', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_ordenes_medicamentos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCPRESCRA; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.ADINGRESO; Contract.HealthAdministrator; dbo.INPACIENT; dbo.IHLISTPRO; dbo.HCVIAADMI; dbo.INDIAGNOS; dbo.INPROFSAL; dbo.HCEXFISIC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_ordenes_medicamentos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_ordenes_medicamentos';
-- GO
