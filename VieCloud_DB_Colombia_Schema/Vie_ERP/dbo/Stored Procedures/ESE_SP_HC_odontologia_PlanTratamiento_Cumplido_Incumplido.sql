CREATE PROCEDURE [dbo].[ESE_SP_HC_odontologia_PlanTratamiento_Cumplido_Incumplido]
AS  
     ---Plan de Tratamientos que el medico paso a No se realizaron  
     SELECT DISTINCT 
            'Plan de Tratamientos No Cumplidos' AS 'Estado Tratamiento', 
            a.ID,
            CASE C.IPTIPODOC
                WHEN 1
                THEN 'Cédula de Ciudadanía'
                WHEN 2
                THEN 'Cédula de Extranjería'
                WHEN 3
                THEN 'Tarjeta de Identidad'
                WHEN 4
                THEN 'Registro Civil'
                WHEN 5
                THEN 'Pasap
orte'
                WHEN 6
                THEN 'Adulto Sin Identificación'
                WHEN 7
                THEN 'Menor Sin Identificación'
                WHEN 8
                THEN 'Número único de identificación personal'
                WHEN 9
                THEN 'Certificado Nacido Vivo'
                WHEN 10
                THEN 'Carnet Diplomático'
                WHEN 11
                THEN 'Salvoconducto'
                WHEN 12
                THEN 'P
ermiso especial de Permanencia'
            END AS 'Tipo de ID del paciente', 
            C.IPCODPACI AS 'Numero de id del paciente', 
            A.NUMINGRES AS 'Ingreso', 
            C.IPPRIAPEL AS 'Apellido 1', 
            C.IPSEGAPEL AS 'Apellido 2', 
            C.IPPRINOMB AS 'Nombre 1', 
            C.IPSEGNOMB AS 'Nombre 2', 
            C.IPFECNACI AS 'Fecha de nacieminto', 
            (CAST(DATEDIFF(dd, IPFECNACI, [Common
].[GETDATE]()) / 365.25 AS INT)) AS 'Edad en años',
            CASE IPSEXOPAC
                WHEN 1
                THEN 'Masculino'
                WHEN 2
                THEN 'Femenino'
            END AS 'Sexo', 
            E.Code AS 'Código Grupo de atención', 
            E.Name AS 'Nombre Grupo de atención',
            CASE EntityType
                WHEN 1
                THEN 'EPS Contributivo'
                WHEN 2
                THEN 'EPS Subsidiado'
                WHEN 3
                THEN 'ET Vinculados Municipios'
                WHEN 4
                THEN 'ET Vinculados Departamentos'
                WHEN 5
                THEN 'ARL Riesgos Laborales'
                WHEN 6
                THEN 'MP Medicina Prepagada'
                WHEN 7
                THEN 'IPS Pri
vada'
                WHEN 8
                THEN 'IPS Publica'
                WHEN 9
                THEN 'Regimen Especial'
                WHEN 10
                THEN 'Accidentes de transito'
                WHEN 11
                THEN 'Fosyga'
                WHEN 12
                THEN 'Otros'
                WHEN 13
                THEN 'Aseguradoras'
                WHEN 99
                THEN 'Particulares'
            END AS 'Tipo de entidad', 
            CODUSUARICREACION AS 'Codigo Profesional Ordeno', 
            SEG.NOMUSUARI, 
            RIES.GESTACION, 
            CONVERT(DATE, A.FECHACREACION) AS 'Fecha Ordeno Tratamiento'
     FROM.ODONTOPLANTRATAMIENTOPAC A
         INNER JOIN.ODONTOPLANTRATAMIENTOPACD B WITH(NOLOCK) ON A.ID = B.IDODONTOPLANTRATAMIENTOPAC
                                                                AND B.ESTADO = 3 -- 3 -Plan de tratamiento No realizado  
         INNER JOIN.INPACIENT C WITH(NOLOCK) ON A.IPCODPACI = C.IPCODPACI
         INNER JOIN.ADINGRESO D WITH(NOLOCK) ON A.NUMINGRES = D.NUMINGRES
         INNER JOIN Contract.CareGroup E WITH(NOLOCK) ON E.Id = D.GENCAREGROUP
         INNER JOIN.ODONTOCONTROL H ON B.IDODONTOCONTROL = H.ID
         INNER JOIN.SEGusuaru SEG ON A.CODUSUARICREACION = SEG.CODUSUARI
         LEFT JOIN.HCRIESGOSP RIES ON A.IPCODPACI = RIES.IPCODPACI
     WHERE A.ESTADO = 3
           AND (H.FECHAREG BETWEEN '01/06/2019' AND '01/07/2019')  --3 - No Cumplido  
     UNION ALL  
     ---Plan de Tratamientos que el medico paso a Realizado  
     SELECT DISTINCT 
            'Plan de Tratamientos Cumplido' AS 'Estado Tratamiento', 
            a.ID,
            CASE C.IPTIPODOC
                WHEN 1
                THEN 'Cédula de Ciudadanía'
                WHEN 2
                THEN 'Cédula de Extranjería'
                WHEN 3
                THEN 'Tarjeta de Identidad'
                WHEN 4
                THEN 'Registro Civil'
                WHEN 5
                THEN 'Pasaporte
'
                WHEN 6
                THEN 'Adulto Sin Identificación'
                WHEN 7
                THEN 'Menor Sin Identificación'
                WHEN 8
                THEN 'Número único de identificación personal'
                WHEN 9
                THEN 'Certificado Nacido Vivo'
                WHEN 10
                THEN 'Carnet Diplomático'
                WHEN 11
                THEN 'Salvoconducto'
                WHEN 12
                THEN 'Permi
so especial de Permanencia'
            END AS 'Tipo de ID del paciente', 
            C.IPCODPACI AS 'Numero de id del paciente', 
            A.NUMINGRES AS 'Ingreso', 
            IPPRIAPEL AS 'Apellido 1', 
            C.IPSEGAPEL AS 'Apellido 2', 
            C.IPPRINOMB AS 'Nombre 1', 
            C.IPSEGNOMB AS 'Nombre 2', 
            C.IPFECNACI AS 'Fecha de nacieminto', 
            (CAST(DATEDIFF(dd, IPFECNACI, [Common].[GETDATE]()) / 365.25 AS INT)) AS 'Edad en años',
            CASE IPSEXOPAC
                WHEN 1
                THEN 'Masculino'
                WHEN 2
                THEN 'Femenino'
            END AS 'Sexo', 
            E.Code AS 'Código Grupo de atención', 
            E.Name AS 'Nombre Grupo de atención',
            CASE EntityType
                WHEN 1
                THEN 'EPS Contributivo'
                WHEN 2
                THEN 'EPS Subsidiado'
                WHEN 3
                THEN 'ET Vinculados Municipios'
                WHEN 4
                THEN 'ET Vinculados Departamentos'
                WHEN 5
                THEN 'ARL Riesgos Laborales'
                WHEN 6
                THEN 'MP Medicina Prepagada'
                WHEN 7
                THEN 'IPS Pri
vada'
                WHEN 8
                THEN 'IPS Publica'
                WHEN 9
                THEN 'Regimen Especial'
                WHEN 10
                THEN 'Accidentes de transito'
                WHEN 11
                THEN 'Fosyga'
                WHEN 12
                THEN 'Otros'
                WHEN 13
                THEN 'Aseguradoras'
                WHEN 99
                THEN 'Particulares'
            END AS 'Tipo de entidad', 
            CODUSUARICREACION AS 'Codigo Profesional Ordeno', 
            SEG.NOMUSUARI, 
            RIES.GESTACION, 
            CONVERT(DATE, A.FECHACREACION) AS 'Fecha Ordeno Tratamiento'
     FROM.ODONTOPLANTRATAMIENTOPAC A
         INNER JOIN.ODONTOPLANTRATAMIENTOPACD B WITH(NOLOCK) ON A.ID = B.IDODONTOPLANTRATAMIENTOPAC
                                                                AND B.ESTADO = 2 -- 2 - Realizado  
         INNER JOIN.INPACIENT C WITH(NOLOCK) ON A.IPCODPACI = C.IPCODPACI
         INNER JOIN.ADINGRESO D WITH(NOLOCK) ON A.NUMINGRES = D.NUMINGRES
         INNER JOIN Contract.CareGroup E WITH(NOLOCK) ON E.Id = D.GENCAREGROUP
         INNER JOIN.SEGusuaru SEG ON A.CODUSUARICREACION = SEG.CODUSUARI
         LEFT JOIN.HCRIESGOSP RIES ON A.IPCODPACI = RIES.IPCODPACI
     WHERE A.ESTADO = 2
           AND (B.FECHAREALIZO BETWEEN '01/07/2019' AND '01/08/2019');  --2 - Plan de tratamiento Realizado     
---Fecha Formato: (DD-MM-YYYY), Fecha Final del Folio Formato:(DD-MM-YYYY)
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de seguimiento al cumplimiento de planes de tratamiento odontológico por paciente. Combina los planes de tratamiento (cabecera y detalle de procedimientos) con los datos demográficos del paciente, el ingreso hospitalario, el grupo de atención contractual y los factores de riesgo en salud pública, para identificar qué tratamientos odontológicos fueron efectivamente realizados y cuáles quedaron sin cumplir. Devuelve dos bloques unidos: planes marcados como ''No Cumplidos'' (estado 3 en el detalle) y planes marcados como ''Cumplidos'' (estado 2 en el detalle), incluyendo el profesional que ordenó el tratamiento, la entidad pagadora y si el paciente tenía gestación registrada como factor de riesgo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_HC_odontologia_PlanTratamiento_Cumplido_Incumplido';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_HC_odontologia_PlanTratamiento_Cumplido_Incumplido';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reporta los planes de tratamiento odontológico clasificados como cumplidos (realizados) o no cumplidos (no realizados) en ventanas de fechas específicas, con datos demográficos del paciente, entidad y profesional.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_odontologia_PlanTratamiento_Cumplido_Incumplido';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de planes de tratamiento odontológico con sus detalles (cabecera y detalle relacionados por IDODONTOPLANTRATAMIENTOPAC).; Pacientes registrados en INPACIENT y con ingreso en ADINGRESO vinculado al grupo de atención (Contract.CareGroup).; Usuario creador del plan registrado en SEGusuaru.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_odontologia_PlanTratamiento_Cumplido_Incumplido';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Un plan se considera ''Cumplido'' solo si tanto la cabecera como el detalle tienen ESTADO=2.; Un plan se considera ''No Cumplido'' solo si tanto la cabecera como el detalle tienen ESTADO=3.; La edad se calcula como DATEDIFF(días)/365.25 truncado a entero.; El criterio de fecha para ''No Cumplidos'' usa la fecha de registro del control odontológico (H.FECHAREG); para ''Cumplidos'' usa la fecha de realización del detalle (B.FECHAREALIZO).; Los rangos de fechas están hardcodeados (junio-julio 2019 y julio-agosto 2019).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_odontologia_PlanTratamiento_Cumplido_Incumplido';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Plan de tratamiento odontológico; Cumplimiento/incumplimiento de tratamiento; Paciente; Tipo de documento de identidad; Ingreso asistencial; Grupo de atención (CareGroup); Tipo de entidad (EPS, ARL, IPS, Medicina Prepagada, Fosyga, Particulares); Profesional ordenador; Gestación/riesgo; Control odontológico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_odontologia_PlanTratamiento_Cumplido_Incumplido';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Cuando A.ESTADO=3 y B.ESTADO=3 y H.FECHAREG está entre 01/06/2019 y 01/07/2019, se retornan filas etiquetadas ''Plan de Tratamientos No Cumplidos''.; [RETURN_RESULT] N/A: Cuando A.ESTADO=2 y B.ESTADO=2 y B.FECHAREALIZO está entre 01/07/2019 y 01/08/2019, se retornan filas etiquetadas ''Plan de Tratamientos Cumplido''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_odontologia_PlanTratamiento_Cumplido_Incumplido';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ESTADO del plan y detalle = 3 (No realizado) y FECHAREG del control en rango 01/06/2019–01/07/2019 → Incluir registro como ''Plan de Tratamientos No Cumplidos''; si ESTADO del plan y detalle = 2 (Realizado) y FECHAREALIZO en rango 01/07/2019–01/08/2019 → Incluir registro como ''Plan de Tratamientos Cumplido''; si Mapeo IPTIPODOC 1..12 → Traduce a etiqueta de tipo de documento (CC, CE, TI, RC, Pasaporte, etc.); si Mapeo IPSEXOPAC → 1=Masculino, 2=Femenino; si Mapeo EntityType 1..13,99 → Traduce a tipo de entidad (EPS Contributivo/Subsidiado, ARL, IPS, Particulares, etc.)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_odontologia_PlanTratamiento_Cumplido_Incumplido';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_odontologia_PlanTratamiento_Cumplido_Incumplido';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ODONTOPLANTRATAMIENTOPAC; dbo.ODONTOPLANTRATAMIENTOPACD; dbo.INPACIENT; dbo.ADINGRESO; Contract.CareGroup; dbo.ODONTOCONTROL; dbo.SEGusuaru; dbo.HCRIESGOSP', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_odontologia_PlanTratamiento_Cumplido_Incumplido';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_odontologia_PlanTratamiento_Cumplido_Incumplido';
-- GO
