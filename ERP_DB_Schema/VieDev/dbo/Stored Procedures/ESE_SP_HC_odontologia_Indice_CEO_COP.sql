CREATE PROCEDURE [dbo].[ESE_SP_HC_odontologia_Indice_CEO_COP]
AS
     SELECT D.NUMINGRES,
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
                THEN 'Pasaporte'
                WHEN 6
                THEN 'Adulto Sin Identificación'
                WHEN 7
                THEN 'Menor 
Sin Identificación'
                WHEN 8
                THEN 'Número único de identificación personal'
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
            C.IPPRIAPEL AS 'Apellido 1', 
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
            F.CODENTIDA AS 'Código Entidad', 
            F.NOMENTIDA AS 'Nombre entidad',
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
                THEN 'IPS Priva
da'
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
            G.CODPROSAL AS 'Código del profesional', 
            G.NOMMEDICO AS 'Nombre del profesional', 
            D.NUMINGRES AS 'ingreso', 
            (H.CEO_C + H.CPO_C) AS 'Total dientes cariados', 
            (H.CEO_E + H.CPO_P) AS 'Total dientes perdidos-Extraidos', 
            (H.CEO_O + H.CPO_O) AS 'Total dientes Obturados', 
            (CEO_C + CEO_E + CEO_O) AS 'Suma de CEO', 
            (CPO_C + CPO_P + CPO_O) AS 'Suma de CPO', 
            H.CEO_C, 
            H.CEO_E, 
            H.CEO_O, 
            H.CPO_C, 
            H.CPO_O, 
            H.CPO_P, 
            B.IDMODELOHC, 
            B.FECHISPAC AS 'Fecha del folio', 
            odo.[Tipo de Atencion]
     FROM.ODONTOCONTROL A
         INNER JOIN.HCHISPACA B WITH(NOLOCK) ON A.NUMEFOLIO = B.NUMEFOLIO
                                                AND A.IPCODPACI = B.IPCODPACI
                                                AND B.IDMODELOHC = 28
         INNER JOIN.INPACIENT C WITH(NOLOCK) ON A.IPCODPACI = C.IPCODPACI
         INNER JOIN.ADINGRESO D WITH(NOLOCK) ON A.NUMINGRES = D.NUMINGRES
         INNER JOIN Contract.CareGroup E WITH(NOLOCK) ON E.Id = D.GENCAREGROUP
         INNER JOIN.INENTIDAD F WITH(NOLOCK) ON F.CODENTIDA = D.CODENTIDA
         INNER JOIN.INPROFSAL G WITH(NOLOCK) ON G.CODPROSAL = A.CODPROSAL
         INNER JOIN.ODONTOCONTROLVALO H WITH(NOLOCK) ON H.IDODONTOCONTROL = A.ID
         INNER JOIN.ESE_HC_HISTORICO_ODONTOLOGIA ODO ON A.NUMINGRES = ODO.Ingreso
     WHERE B.FECHISPAC BETWEEN '01/08/2019' AND '01/09/2019';   ---Fecha Inicial del Folio Formato: (DD-MM-YYYY), Fecha Final del Folio Formato:(DD-MM-YYYY)  