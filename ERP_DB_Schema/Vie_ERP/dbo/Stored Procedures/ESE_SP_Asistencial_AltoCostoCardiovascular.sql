CREATE PROCEDURE [dbo].[ESE_SP_Asistencial_AltoCostoCardiovascular] @FechaIni DATETIME,   
                                                                   @FechaFin DATETIME  
AS  
     SELECT ING.NUMINGRES AS Ingreso,   
            ING.CODCENATE AS CodCentro,   
            RTRIM(cen.NOMCENATE) AS CentroAtencion,   
            RTRIM(PAC.IPPRINOMB) AS '1.PrimerNombre',   
            RTRIM(PAC.IPSEGNOMB) AS '2.SegundoNombre',   
            RTRIM(PAC.IPPRIAPEL) AS '3.PrimerApellido',   
            RTRIM(PAC.IPSEGAPEL) AS '4.Seg  
undoApellido',  
            CASE PAC.IPTIPODOC  
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
            END AS '5.TipoIdentificacion',   
            RTRIM(ING.IPCODPACI) AS '6.Identificacion',   
            CAST(PAC.IPFECNACI AS DATE) AS '7.FechaNacimiento',   
            YEAR([Common].[GETDATE]()) - YEAR(PAC.IPFECNACI) AS Edad,  
            CASE  
                WHEN(YEAR([Common].[GETDATE]()) - YEAR(PAC.IPFECNACI)) < 60  
                THEN '1'  
                ELSE '2'  
            END AS EdadAgrupada,  
            CASE PAC.IPSEXOPAC  
                WHEN 1  
                THEN 'M'  
                WHEN 2  
                THEN 'F'  
            END AS '8.Sexo',  
            CASE PAC.IPSEXOPAC  
                WHEN 1  
                THEN 1  
                ELSE 0  
            END AS ConstanteSexo,  
            CASE PAC.IPSEXOPAC  
                WHEN 1  
                THEN '1'  
                ELSE '0,742'  
            END AS ConstanteTFG,  
            CASE  
                WHEN CGR.EntityType = 1  
                THEN 'C'  
                WHEN CGR.EntityType = 2  
                THEN 'S'  
                WHEN CGR.EntityType = 3  
                THEN 'ET Vinculados Municipios - N'  
                WHEN CGR.EntityType = 4  
                THEN 'ET Vinculados Departamentos - N'  
                WHEN CGR.EntityType = 5  
                THEN 'ARL Riesgos Laborales - P'  
                WHEN CGR.EntityType = 6  
                THEN 'MP Medicina Prepagada - P'  
                WHEN CGR.EntityType = 7  
                THEN 'IPS Privada - P'  
                WHEN CGR.EntityType = 8  
                THEN 'IPS Publica - P'  
                WHEN CGR.EntityType = 9  
                THEN 'E'  
                WHEN CGR.EntityType = 10  
                THEN 'Accidentes de transito -P'  
                WHEN CGR.EntityType = 11  
                THEN 'Fosyga - P'  
                WHEN CGR.EntityType = 12  
                THEN 'Otros - N'  
                WHEN CGR.EntityType = 13  
                THEN 'Aseguradoras - P'  
                WHEN CGR.EntityType = 99  
                THEN 'Particulares - P'  
            END AS '9.Regimen',   
            ING.CODENTIDA AS '10.CodigoEPSEntidadTerritorial',   
            ENT.NOMENTIDA AS 'Entidad',  
            CASE  
                WHEN GRE.TIPOET IS NULL  
                THEN '6'  
                ELSE GRE.TIPOET  
            END AS '11.GrupoEtnico',  
            CASE  
                WHEN PE.TIPOPOBESP IS NULL  
                THEN '99'  
                ELSE PE.TIPOPOBESP  
            END AS '12.PoblacionEspecial',   
            SUBSTRING(PAC.AUUBICACI, 1, 5) AS '13.MunicipioResidencia',  
            CASE  
                WHEN PAC.IPTELMOVI IS NULL  
                THEN PAC.IPTELEFON  
                ELSE PAC.IPTELMOVI  
            END AS '14.Telefono',   
            '' AS '15.FechaAfiliacion',   
            '410010045101' AS '16.CodigoIPS',   
            CAST(HC.FECHISPAC AS DATE) AS '17.FechaIngresoNefroprotección',   
            ING.CODDIAEGR AS 'CIE-10',   
            DIAG.NOMDIAGNO AS Diagnostico,   
            DIAC.ABREVIATURA AS DxMedico,  
            CASE  
                WHEN DIAC.ABREVIATURA = 'HTA'  
                THEN '1'  
                ELSE '2'  
            END AS '18.HTA',  
            CASE  
                WHEN DIAC.ABREVIATURA = 'HTA'  
                THEN CAST(DIAP.FECDIAGNO AS DATE)  
                ELSE '1845-01-01'  
            END AS '19.FechaDiagnosticoHTA',   
            '' AS '19.1 CostoHTA',  
            CASE  
                WHEN DIAC.ABREVIATURA = 'DM'  
                THEN '1'  
                ELSE '2'  
            END AS '20.DM',  
            CASE  
                WHEN DIAC.ABREVIATURA = 'DM'  
                THEN CAST(DIAP.FECDIAGNO AS DATE)  
                ELSE '1845-01-01'  
            END AS '21.FechaDiagnósticoDM',   
            '' AS '21.1 CostoDM',  
            CASE  
                WHEN G.Tipo = 'ins'  
                THEN G.PRODUCTO  
                ELSE ''  
            END AS 'NombreInsulina',   
            '98' AS '22.EtiologaERC',   
            (EFIS.PESOPACIE / 1000) AS '23.Peso',   
            EFIS.TALLAPACI AS '24.Talla',   
            ROUND(((EFIS.PESOPACIE / 1000) / (EFIS.TALLAPACI * EFIS.TALLAPACI) * 1000), 2, 0) AS 'IMC',   
            EFIS.TENARTSIS AS '25.TAS',   
            EFIS.TENARTDIA AS '26.TAD',  
            CASE  
                WHEN G3.Tipo = 'Creat'  
                THEN G3.Procedimiento  
                ELSE ''  
            END AS 'ProcidimientoCreatinina',   
            '' AS '27.Creatinina',   
            '' AS '27,1.FechaUCreatinina',  
            CASE  
                WHEN G3.Tipo = 'HeGli'  
                THEN G3.Procedimiento  
                ELSE ''  
            END AS 'ProcidimientoHemoglobina',   
            '' AS '28.Hemoglobina',   
            '' AS '28,1.FechaUHemoglobina',  
            CASE  
                WHEN G3.Tipo = 'Album'  
                THEN G3.Procedimiento  
                ELSE ''  
            END AS 'ProcidimientoAlbumina',   
            '' AS '29.albumina',   
            '' AS '29,1.FechaUAlbumina',   
            '98' AS '30.Creatinuria',   
            '1845-01-01' AS '30,1.FechaUCreatinuria',  
            CASE  
                WHEN G3.Tipo = 'Coles'  
                THEN G3.Procedimiento  
                ELSE ''  
            END AS 'ProcidimientoColesterolTotal',   
            '' AS '30.Colesterol',   
            '' AS '30,1.FechaUColesterol',  
            CASE  
                WHEN G3.Tipo = 'HDL'  
                THEN G3.Procedimiento  
                ELSE ''  
            END AS 'ProcidimientoHDL',   
            '' AS '31.HDL',   
            '' AS '31,1.FechaUHDL',  
            CASE  
                WHEN G3.Tipo = 'LDL'  
                THEN G3.Procedimiento  
                ELSE ''  
            END AS 'ProcidimientoLDL',   
            '' AS '32.LDL',   
            '' AS '32,1.FechaULDL',   
            '98' AS '33.PTH',   
            '1845-01-01' AS '33,1.FechaUPTH',  
            CASE  
                WHEN G.Tipo = 'IECAS'  
                THEN G.PRODUCTO  
                ELSE ''  
            END AS 'ProductoIECA',  
            CASE  
                WHEN G.Tipo = 'IECAS'  
                THEN '1'  
                ELSE '2'  
            END AS '36.IECA',  
            CASE  
                WHEN G.Tipo = 'ARA'  
                THEN G.PRODUCTO  
                ELSE ''  
            END AS 'ProductoARA',  
            CASE  
                WHEN G.Tipo = 'ARA'  
                THEN '1'  
                ELSE '2'  
            END AS '37.ARA',   
            S.NOMMEDICO AS 'medico',   
            CAST(esc.RESULTADO AS CHAR) AS 'EscalaFramingham',  
            CASE  
                WHEN esc.RESULTADO < 10  
                THEN 'Riesgo bajo.'  
                WHEN esc.RESULTADO >= 10  
                     AND esc.RESULTADO <= 20  
                THEN 'Riesgo moderado.'  
                WHEN esc.RESULTADO > 20  
                THEN 'Riesgo alto.'  
            END AS 'ResultadoFramingham',   
            CAST(esc2.RESULTADO AS CHAR) AS 'EscalaMorisky',  
   CASE  
                WHEN esc2.RESULTADO < 4  
                THEN 'No ADHERENTE'  
                WHEN esc2.RESULTADO = 4  
                THEN 'ADHERENTE'  
            END AS 'ResultadoMorisky',   
            CAST(EFIS.NEOPERABD AS CHAR) AS 'PerimetroAbdominal'  
     FROM.ADINGRESO AS ING  
         INNER JOIN.HCHISPACA AS HC WITH(NOLOCK) ON ING.NUMINGRES = HC.NUMINGRES  
                                                    AND ING.IPCODPACI = HC.IPCODPACI  
         INNER JOIN.INDIAGNOS AS DIAG WITH(NOLOCK) ON ING.CODDIAEGR = DIAG.CODDIAGNO  
         INNER JOIN.INPACIENT AS PAC WITH(NOLOCK) ON ING.IPCODPACI = PAC.IPCODPACI  
         INNER JOIN.INENTIDAD AS ENT WITH(NOLOCK) ON ING.CODENTIDA = ENT.CODENTIDA  
         INNER JOIN.ADCENATEN AS Cen ON Cen.CODCENATE = ING.CODCENATE  
         INNER JOIN.INPROFSAL AS S ON S.CODPROSAL = hc.CODPROSAL  
         INNER JOIN Contract.CareGroup AS CGR WITH(NOLOCK) ON ING.GENCAREGROUP = CGR.Id  
         LEFT OUTER JOIN.ADGRUETNI AS GRE WITH(NOLOCK) ON PAC.CODGRUPOE = GRE.CODGRUPOE  
         LEFT OUTER JOIN  
     (  
         SELECT PA.ID,   
                PA.IPCODPACI,   
                PA.IDADPOBESPE  
         FROM.ADPOBESPEPAC AS PA  
             INNER JOIN  
         (  
             SELECT MIN(ID) AS ID,   
                    IPCODPACI  
             FROM.ADPOBESPEPAC  
             GROUP BY IPCODPACI  
         ) AS G ON G.ID = PA.ID  
     ) AS G2 ON PAC.IPCODPACI = G2.IPCODPACI  
         LEFT OUTER JOIN.ADPOBESPE AS PE ON PE.ID = G2.IDADPOBESPE  
         LEFT OUTER JOIN .INDIAGNOSAltoCosto AS DIAC ON ING.CODDIAEGR = DIAC.CODDIAGNO  
         INNER JOIN.INDIAGNOP AS DIAP ON DIAP.CODDIAGNO = ING.CODDIAEGR  
                                         AND ING.NUMINGRES = DIAP.NUMINGRES  
         LEFT OUTER JOIN  
     (  
         SELECT PD.IPCODPACI,   
                PD.NUMINGRES,   
                PD.NUMEFOLIO,   
                PD.CODPRODUC,   
                RTRIM(P.DESPRODUC) AS PRODUCTO,   
                AL.Tipo AS Tipo  
         FROM.HCPRESCRC AS PC  
             INNER JOIN.HCPRESCRD AS PD ON PC.CODCONCEC = PD.CODCONCEC  
             INNER JOIN .IHLISTPROMediAltoCosto AS AL ON PD.CODPRODUC = AL.CODPRODUC  
             INNER JOIN.IHLISTPRO AS P ON PD.CODPRODUC = P.CODPRODUC  
     ) AS G ON G.IPCODPACI = ING.IPCODPACI  
               AND G.NUMINGRES = ING.NUMINGRES  
               AND G.NUMEFOLIO = HC.NUMEFOLIO  
         INNER JOIN.HCEXFISIC AS EFIS ON ING.NUMINGRES = EFIS.NUMINGRES  
                                         AND EFIS.IPCODPACI = ING.IPCODPACI  
                                         AND HC.NUMEFOLIO = EFIS.NUMEFOLIO  
         LEFT OUTER JOIN  
     (  
         SELECT lab.numefolio AS folio,   
                lab.ipcodpaci AS paciente,   
                lab.numingres AS Ingreso,   
                RTRIM(p.DESSERIPS) AS Procedimiento,   
                pro.tipo AS Tipo  
         FROM.HCORDLABO AS lab  
             INNER JOIN .INCUPSIPSAltoCosto AS pro ON lab.codserips = pro.CODSERIPS  
             INNER JOIN.INCUPSIPS AS p ON lab.codserips = p.codserips  
         WHERE pro.tipo IS NOT NULL  
     ) AS G3 ON G3.paciente = ing.IPCODPACI  
                AND G3.Ingreso = ING.NUMINGRES  
                AND G3.folio = HC.NUMEFOLIO  
         LEFT OUTER JOIN.HCESCALAS AS esc ON ing.NUMINGRES = esc.NUMINGRES  
                                             AND ing.IPCODPACI = esc.IPCODPACI  
                                             AND esc.TIPOESCALA = 6  
                                             AND esc.NUMEFOLIO = hc.NUMEFOLIO  
         LEFT OUTER JOIN.HCESCALAS AS esc2 ON ing.NUMINGRES = esc2.NUMINGRES  
                                              AND ing.IPCODPACI = esc2.IPCODPACI  
                                              AND esc2.TIPOESCALA = 7  
                                              AND esc2.NUMEFOLIO = hc.NUMEFOLIO  
     WHERE HC.TIPHISPAC = 'I'  
           AND HC.IDMODELOHC = 14  
           AND CAST(HC.FECHISPAC AS DATE) BETWEEN @FechaIni AND @FechaFin  
     ORDER BY CAST(HC.FECHISPAC AS DATE);