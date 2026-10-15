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