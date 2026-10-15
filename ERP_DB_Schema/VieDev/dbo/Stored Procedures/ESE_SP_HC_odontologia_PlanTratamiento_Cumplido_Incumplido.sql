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