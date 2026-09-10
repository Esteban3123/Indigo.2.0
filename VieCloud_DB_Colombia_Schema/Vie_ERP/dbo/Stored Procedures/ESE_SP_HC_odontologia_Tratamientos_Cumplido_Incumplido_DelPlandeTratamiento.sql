CREATE PROCEDURE [dbo].[ESE_SP_HC_odontologia_Tratamientos_Cumplido_Incumplido_DelPlandeTratamiento]
AS  
     --- Tratamientos No Realizados  
     SELECT b.IDODONTOPLANTRATAMIENTOPAC,
            CASE B.ESTADO
                WHEN 1
                THEN 'Sin realizar'
                WHEN 2
                THEN 'Realizado'
                WHEN 3
                THEN 'No realizado'
                WHEN 4
                THEN 'Cancelado'
            END AS 'Estado Tratamiento', 
            F.DESCRITRA AS 'Nombre Tratamiento',
            CASE C.IPTIPODOC
                WHEN 1
                THEN 'Céd
ula de Ciudadanía'
                WHEN 2
                THEN 'Cédula de Extranjería'
                WHEN 3
                THEN 'Tarjeta de Identidad'
                WHEN 4
                THEN 'Registro Civil'
                WHEN 5
                THEN 'Pasaporte'
                WHEN 6
                THEN 'Adulto Sin Identificación'
                WHEN 7
                THEN 'Menor Sin Identificación'
                WHEN 8
                THEN 'Número único de iden
tificación personal'
                WHEN 9
                THEN 'Certificado Nacido Vivo'
                WHEN 10
                THEN 'Carnet Diplomático'
                WHEN 11
                THEN 'Salvoconducto'
                WHEN 12
                THEN 'Permiso especial de Permanencia'
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
            CONVERT(DATE, A.FECHACREACION) AS 'Fecha Ordeno Tratamiento', 
            NULL AS 'Profesional Realizo', 
            NULL AS 'Fecha realizo', 
            H.CODPROSAL AS 'Profesional No Realizado', 
            H.FECHAREG AS 'Fecha No Realizado'
     FROM.ODONTOPLANTRATAMIENTOPAC A
         INNER JOIN.ODONTOPLANTRATAMIENTOPACD B WITH(NOLOCK) ON A.ID = B.IDODONTOPLANTRATAMIENTOPAC
                                                                AND B.ESTADO IN(3)
         INNER JOIN.INPACIENT C WITH(NOLOCK) ON A.IPCODPACI = C.IPCODPACI
         INNER JOIN.ADINGRESO D WITH(NOLOCK) ON A.NUMINGRES = D.NUMINGRES
         INNER JOIN Contract.CareGroup E WITH(NOLOCK) ON E.Id = D.GENCAREGROUP
         INNER JOIN.ODOPARTRA F WITH(NOLOCK) ON F.CONSECTRA = B.IDODOPARTRA
         INNER JOIN.ODONTOCONTROL H ON B.IDODONTOCONTROL = H.ID
     WHERE B.ESTADO IN(3)
         AND (A.FECHACREACION BETWEEN '01/04/2019' AND '01/05/2019') --3 - No Cumplido  
     UNION ALL
     SELECT b.IDODONTOPLANTRATAMIENTOPAC,
            CASE B.ESTADO
                WHEN 1
                THEN 'Sin realizar'
                WHEN 2
                THEN 'Realizado'
                WHEN 3
                THEN 'No realizado'
                WHEN 4
                THEN 'Cancelado'
            END AS 'Estado Tratamiento', 
            F.DESCRITRA AS 'Nombre Tratamiento',
            CASE C.IPTIPODOC
                WHEN 1
                THEN 'Cédula
 de Ciudadanía'
                WHEN 2
                THEN 'Cédula de Extranjería'
                WHEN 3
                THEN 'Tarjeta de Identidad'
                WHEN 4
                THEN 'Registro Civil'
                WHEN 5
                THEN 'Pasaporte'
                WHEN 6
                THEN 'Adulto Sin Identificación'
                WHEN 7
                THEN 'Menor Sin Identificación'
                WHEN 8
                THEN 'Número único de identif
icación personal'
                WHEN 9
                THEN 'Certificado Nacido Vivo'
                WHEN 10
                THEN 'Carnet Diplomático'
                WHEN 11
                THEN 'Salvoconducto'
                WHEN 12
                THEN 'Permiso especial de Permanencia'
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
            A.CODUSUARICREACION AS 'Codigo Profesional Ordeno', 
            A.FECHACREACION AS 'Fecha Ordeno Tratamiento', 
            B.PROFESIONALREALIZO AS 'Profesional Realizo', 
            B.FECHAREALIZO AS 'Fecha realizo', 
            NULL AS 'Profesional No Realizado', 
            NULL AS 'Fecha No Realizado'
     FROM.ODONTOPLANTRATAMIENTOPAC A
         INNER JOIN.ODONTOPLANTRATAMIENTOPACD B WITH(NOLOCK) ON A.ID = B.IDODONTOPLANTRATAMIENTOPAC
                                                                AND B.ESTADO IN(1, 2, 4)
         INNER JOIN.INPACIENT C WITH(NOLOCK) ON A.IPCODPACI = C.IPCODPACI
         INNER JOIN.ADINGRESO D WITH(NOLOCK) ON A.NUMINGRES = D.NUMINGRES
         INNER JOIN Contract.CareGroup E WITH(NOLOCK) ON E.Id = D.GENCAREGROUP
         INNER JOIN.ODOPARTRA F WITH(NOLOCK) ON F.CONSECTRA = B.IDODOPARTRA
     WHERE B.ESTADO IN(1, 2, 4)
         AND (A.FECHACREACION BETWEEN '01/05/2019' AND '01/06/2019')  --2 - Plan de tratamiento Realizado     
     ORDER BY B.IDODONTOPLANTRATAMIENTOPAC;  
---Fecha Formato: (DD-MM-YYYY), Fecha Final del Folio Formato:(DD-MM-YYYY)
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de cumplimiento e incumplimiento de planes de tratamiento odontológico por paciente. Consolida, mediante una unión de consultas, los procedimientos dentales en estado ''No realizado'' y ''Realizado'' registrados en el detalle del plan de tratamiento, cruzando con los datos demográficos del paciente (nombre, cédula, tipo de documento, edad, sexo, fecha de nacimiento), el número de ingreso, el grupo de atención (entidad pagadora y tipo: EPS, ARL, particular, etc.) y la descripción del tratamiento del catálogo odontológico. Para cada ítem del plan informa quién ordenó el tratamiento y cuándo, y en caso de tratamientos no realizados, también el profesional y la fecha en que se registró el incumplimiento. Sirve para auditoría clínica y seguimiento de la ejecución de planes de salud oral, permitiendo identificar qué tratamientos fueron completados o quedaron pendientes dentro de un período de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_HC_odontologia_Tratamientos_Cumplido_Incumplido_DelPlandeTratamiento';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_HC_odontologia_Tratamientos_Cumplido_Incumplido_DelPlandeTratamiento';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reporte que lista los tratamientos del plan odontológico de pacientes, separando los no realizados de los realizados/pendientes/cancelados, con datos demográficos y de la entidad responsable.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_odontologia_Tratamientos_Cumplido_Incumplido_DelPlandeTratamiento';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de planes de tratamiento odontológico (ODONTOPLANTRATAMIENTOPAC) y su detalle (ODONTOPLANTRATAMIENTOPACD) con estados válidos (1-4).; Para tratamientos no realizados (estado=3) debe existir un registro asociado en ODONTOCONTROL referenciado por IDODONTOCONTROL.; El paciente debe estar registrado en INPACIENT y el ingreso en ADINGRESO con un grupo de atención (GENCAREGROUP) existente en Contract.CareGroup.; El tratamiento debe estar parametrizado en ODOPARTRA (catálogo de tratamientos).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_odontologia_Tratamientos_Cumplido_Incumplido_DelPlandeTratamiento';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los rangos de fechas de filtrado están fijos en el código (hardcoded): abril 2019 para no realizados y mayo 2019 para realizados/pendientes/cancelados.; Solo se consideran tratamientos cuyo estado pertenezca al conjunto {1,2,3,4}.; Los tratamientos con estado 3 siempre se cruzan con ODONTOCONTROL; los demás estados nunca usan ODONTOCONTROL.; La edad se calcula como DATEDIFF(día, fecha_nacimiento, hoy)/365.25 truncada a entero.; El procedimiento es de solo lectura: no realiza INSERT/UPDATE/DELETE.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_odontologia_Tratamientos_Cumplido_Incumplido_DelPlandeTratamiento';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Plan de tratamiento odontológico; Tratamiento cumplido / incumplido; Paciente; Ingreso asistencial; Grupo de atención (CareGroup); Tipo de entidad responsable (EPS, ARL, Medicina Prepagada, Fosyga, Particulares); Tipo de documento de identificación; Profesional que ordena / realiza / no realiza el tratamiento; Control odontológico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_odontologia_Tratamientos_Cumplido_Incumplido_DelPlandeTratamiento';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve un único result set unido (UNION ALL): primer bloque con tratamientos en estado 3 (No realizado) creados entre 01/04/2019 y 01/05/2019; segundo bloque con tratamientos en estados 1, 2 o 4 creados entre 01/05/2019 y 01/06/2019, ordenado por IDODONTOPLANTRATAMIENTOPAC.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_odontologia_Tratamientos_Cumplido_Incumplido_DelPlandeTratamiento';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Detalle del plan con ESTADO = 3 (No realizado) → Se incluye en el primer bloque con JOIN a ODONTOCONTROL para obtener profesional y fecha del no-realizado; los campos ''Profesional Realizo'' y ''Fecha realizo'' se devuelven NULL. else Si ESTADO IN (1,2,4) se incluye en el segundo bloque con datos de PROFESIONALREALIZO y FECHAREALIZO; los campos ''Profesional No Realizado'' y ''Fecha No Realizado'' se devuelven NULL.; si Mapeo de B.ESTADO a etiqueta → 1→''Sin realizar'', 2→''Realizado'', 3→''No realizado'', 4→''Cancelado''.; si Mapeo de IPTIPODOC del paciente → Se traduce a etiqueta del tipo de documento (CC, CE, TI, RC, Pasaporte, etc., 1..12).; si Mapeo de IPSEXOPAC → 1→''Masculino'', 2→''Femenino''.; si Mapeo de EntityType del grupo de atención → Se traduce a tipo de entidad responsable (EPS Contributivo, Subsidiado, ARL, Medicina Prepagada, Particulares, etc.).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_odontologia_Tratamientos_Cumplido_Incumplido_DelPlandeTratamiento';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_odontologia_Tratamientos_Cumplido_Incumplido_DelPlandeTratamiento';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ODONTOPLANTRATAMIENTOPAC; dbo.ODONTOPLANTRATAMIENTOPACD; dbo.INPACIENT; dbo.ADINGRESO; Contract.CareGroup; dbo.ODOPARTRA; dbo.ODONTOCONTROL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_odontologia_Tratamientos_Cumplido_Incumplido_DelPlandeTratamiento';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_odontologia_Tratamientos_Cumplido_Incumplido_DelPlandeTratamiento';
-- GO
