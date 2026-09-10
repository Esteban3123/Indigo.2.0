

CREATE VIEW [Report].[ViewTherapiesRequests] AS

     ---*****TERAPIAS******
     WITH CTE_ORDENAMIENTO_TERAPIAS
          AS (SELECT RTRIM(E.NOMCENATE) [CENTRO ATENCION], 
                     A.UFUCODIGO + ' - ' + RTRIM(D.UFUDESCRI) AS [UNIDAD FUNCIONAL], 
					 A.NUMEFOLIO AS FOLIO, 
                     CG.CODE + '-' + CG.NAME [GRUPO], 
                     CSG.CODE + '-' + CSG.NAME [SUBGRUPO], 
                     A.CODSERIPS [CODIGO CUPS], 
                     RTRIM(B.DESSERIPS) AS [DESCRIPCION SERVICIO], 
                     ISNULL(CD.CODE + ' - ' + CD.NAME, '') [DESCRIPCION RELACIONADA], 
                     cast (A.FECORDMED as date) [FECHA ORDENAMIENTO],
                     rtrim(convert(char(5), A.FECORDMED, 108)) [HORA ORDENAMIENTO],
					 CASE I.IPTIPODOC
                         WHEN '1'
                         THEN 'CC'
                         WHEN '2'
                         THEN 'CE'
                         WHEN '3'
                         THEN 'TI'
                         WHEN '4'
                         THEN 'RC'
                         WHEN '5'
                         THEN 'PA'
                         WHEN '6'
                         THEN 'AS'
                         WHEN '7'
                         THEN 'MS'
                         WHEN '8'
                         THEN 'NU'
                         WHEN '9'
                         THEN 'NV'
                         WHEN '10'
                         THEN 'CD'
                         WHEN '11'
                         THEN 'SC'
                         WHEN '12'
                         THEN 'PE'
                     END AS [TIPO IDENTIFICACION], 
                     A.IPCODPACI AS [IDENTIFICACION], 
                     I.IPTELEFON AS [TELEFONO FIJO], 
                     I.IPTELMOVI AS [TELEFONO MOVIL], 
                     I.IPNOMCOMP AS [PACIENTE], 
                     A.NUMINGRES AS [INGRESO],
                     CASE
                         WHEN SERREASIT = '1'
                         THEN 'ESTUDIOS REALIZADOS EN SITIO'
                         WHEN ESTSERIPS IN('2', '3', '4')
                         THEN 'ESTUDIOS REALIZADOS'
                         WHEN ESTSERIPS = '6'
                         THEN 'ANULADO'
                         ELSE 'ESTUDIOS NO REALIZADOS'
                     END AS ESTADO, 
                     SUM(A.CANSERIPS) AS CANTIDAD, 
                     PRO.CODPROSAL [ID PROFESIONAL], 
                     PRO.NOMMEDICO AS PROFESIONAL, 
                     ESPMED.DESESPECI ESPECIALIDAD, 
                     DIAG.CODDIAGNO CIE10, 
                     DIAG.NOMDIAGNO DIAGNOSTICO,
                     CASE ESTA.ESTADIO
                         WHEN 0
                         THEN 'ESTADIO CLÍNICO (EC) 0 (TUMOR IN SITU)'
                         WHEN 1
                         THEN 'EC I O 1'
                         WHEN 2
                         THEN 'EC IA O 1A'
                         WHEN 3
                         THEN 'EC IA1'
                         WHEN 4
                         THEN 'EC IA2'
                         WHEN 5
                         THEN 'EC IB O 1B'
                         WHEN 6
                         THEN 'EC IB1'
                         WHEN 7
                         THEN 'EC IB2'
                         WHEN 8
                         THEN 'EC IC O 1C'
                         WHEN 9
                         THEN 'EC IS O 1S'
                         WHEN 10
                         THEN 'EC II O 2'
                         WHEN 11
                         THEN 'EC IIA O 2A'
                         WHEN 12
                         THEN 'EC IIA1'
                         WHEN 13
                         THEN 'EC IIA2'
                         WHEN 14
                         THEN 'EC IIB O 2B'
                         WHEN 15
                         THEN 'EC IIC O 2C'
                         WHEN 16
                         THEN 'EC III O 3'
                         WHEN 17
                         THEN 'EC IIIA O 3A'
                         WHEN 18
                         THEN 'EC IIIB O 3B'
                         WHEN 19
                         THEN 'EC IIIC O 3C'
                         WHEN 20
                         THEN 'EC IV O 4'
                         WHEN 21
                         THEN 'EC IVA O 4A'
                         WHEN 22
                         THEN 'EC IVB O 4B'
                         WHEN 23
                         THEN 'EC IVC O 4C'
                         WHEN 24
                         THEN 'EC 4S (PARA NEUROBLASTOMA)'
                         WHEN 25
                         THEN 'EC  V O 5'
                         WHEN 26
                         THEN 'EC ESTADIO IAB'
                         WHEN 55
                         THEN 'PERSONA CON ASEGURAMIENTO (RÉGIMEN SUBSIDIADO O CONTRIBUTIVO Y QUE NO SON PPNA) QUE RECIBIÓ SERVICIOS DE SALUD POR PARTE DEL ENTE TERRITORIALDURANTE EL PERIODO DE REPORTE'
                         WHEN 93
                         THEN 'SIN INFORMACIÓN DE ESTADIFICACIÓN EN HISTORIA CLÍNICA'
                         WHEN 98
                         THEN 'NO APLICA (ES CÁNCER DE PIEL BASOCELULAR, ES CÁNCER HEMATOLÓGICO O ES CÁNCER EN SNC, EXCEPTO NEUROBLASTOMA)'
                         WHEN 99
                         THEN 'DESCONOCIDO, EL DATO DE ESTA VARIABLE NO SE ENCUENTRA DESCRITO EN LOS SOPORTES CLÍNICOS'
                         ELSE ''
                     END ESTADIO,
                     CASE
                         WHEN MANEXTPRO = 0
                         THEN 'HOSPITALARIO'
                         ELSE 'AMBULATORIO'
                     END AS [TIPO SOLICITUD], 
                     EA.CODE + ' - ' + EA.NAME AS [ENTIDAD ADMINISTRADORA], 
                     GA.CODE + ' - ' + GA.NAME [GRUPO ATENCION],
                     CASE GA.LIQUIDATIONTYPE
                         WHEN 1
                         THEN 'PAGO POR SERVICIOS'
                         WHEN 2
                         THEN 'PGP'
                         WHEN 3
                         THEN 'FACTURA GLOBAL'
                         WHEN 4
                         THEN 'CAPITACION GLOBAL'
                         WHEN 5
                         THEN 'CONTROL'
                     END [TIPO CONTRATO], 
                     'TERAPIAS' [TIPO ORDEN], 
					 cast (HIS.FECHISPAC as date) [FECHA ATENCION],
                     rtrim(convert(char(5), HIS.FECHISPAC, 108)) [HORA ATENCION],
					 IIF(G.CODGRUPO IS NULL, 'NO', 'SI') CUBIERTO, 
                     ISNULL(G.CONTRATADO, 'NO') CONTRATADO, 
                     ISNULL(G.COTIZADO, 'NO') COTIZADO, 
                     GA.CODE [CODIGO GRUPO], 
                     A.IDDESCRIPCIONRELACIONADA, 
                     CAST(A.FECORDMED AS DATE) [FECHA BUSQUEDA]
              FROM DBO.ADINGRESO ING WITH(NOLOCK)
                   JOIN DBO.HCORDPRON A WITH(NOLOCK) ON ING.NUMINGRES = A.NUMINGRES
                   JOIN DBO.INCUPSIPS B WITH(NOLOCK) ON A.CODSERIPS = B.CODSERIPS
                   JOIN DBO.INPACIENT I WITH(NOLOCK) ON A.IPCODPACI = I.IPCODPACI
                   JOIN DBO.INUNIFUNC D WITH(NOLOCK) ON A.UFUCODIGO = D.UFUCODIGO
                   JOIN DBO.ADCENATEN E WITH(NOLOCK) ON A.CODCENATE = E.CODCENATE
                   JOIN DBO.INPROFSAL PRO WITH(NOLOCK) ON A.CODPROSAL = PRO.CODPROSAL
                   JOIN CONTRACT.CUPSENTITY AS CUPS WITH(NOLOCK) ON CUPS.CODE = B.CODSERIPS
                   JOIN CONTRACT.CUPSSUBGROUP AS CSG WITH(NOLOCK) ON CSG.ID = CUPS.CUPSSUBGROUPID
                   JOIN CONTRACT.CUPSGROUP AS CG WITH(NOLOCK) ON CG.ID = CSG.CUPSGROUPID
                   JOIN DBO.HCHISPACA HIS WITH(NOLOCK) ON A.NUMINGRES = HIS.NUMINGRES
                                                          AND A.NUMEFOLIO = HIS.NUMEFOLIO
                   JOIN DBO.INESPECIA AS ESPMED WITH(NOLOCK) ON ESPMED.CODESPECI = HIS.CODESPTRA
                   JOIN DBO.INDIAGNOS AS DIAG WITH(NOLOCK) ON DIAG.CODDIAGNO = HIS.CODDIAGNO
                   JOIN CONTRACT.CAREGROUP AS GA WITH(NOLOCK) ON GA.ID = ING.GENCAREGROUP
                   JOIN CONTRACT.HEALTHADMINISTRATOR AS EA WITH(NOLOCK) ON EA.ID = ING.GENCONENTITY
                   LEFT JOIN CONTRACT.CUPSENTITYCONTRACTDESCRIPTIONS CECD WITH(NOLOCK) ON CECD.ID = A.IDDESCRIPCIONRELACIONADA
                   LEFT JOIN CONTRACT.CONTRACTDESCRIPTIONS CD WITH(NOLOCK) ON CD.ID = CECD.CONTRACTDESCRIPTIONID
                   LEFT JOIN DBO.INDIAGNOP AS EST WITH(NOLOCK) ON A.IPCODPACI = EST.IPCODPACI
                                                                  AND A.NUMINGRES = EST.NUMINGRES
                                                                  AND EST.NUMEFOLIO = A.NUMEFOLIO
                                                                  AND EST.ESTADIO IS NOT NULL
                   LEFT JOIN
              (
                  SELECT CG.CODE AS CODGRUPO, 
                         CG.NAME AS GRUPOATENCION, 
                         CON.CODE AS CODCONTRATO, 
                         CON.CONTRACTNAME AS NOMBRECONTRATO, 
                         PT.CODE AS CODPLANTIPROCE, 
                         PT.NAME AS PLANTILLAPROCEDIMIENTO, 
                         CGR.CODE AS CODIGOGRUPO, 
                         CGR.NAME AS GRUPO, 
                         CSG.CODE AS CODSUBGRUPO, 
                         CSG.NAME AS SUBGRUPO, 
                         CE.CODE AS CUPS, 
                         CE.DESCRIPTION AS DESCRIPCIONCUPS, 
                         CD.CODE AS CODRELACION, 
                         CD.NAME AS DESCRIPCIONRELACIONADA,
                         CASE PC.CONTRACTED
                             WHEN 1
                             THEN 'SI'
                             ELSE 'NO'
                         END AS CONTRATADO,
                         CASE PC.QUOTED
                             WHEN 1
                             THEN 'SI'
                             ELSE 'NO'
                         END AS COTIZADO, 
                         CECD.ID AS IDRELACION
                  FROM CONTRACT.CAREGROUP AS CG WITH(NOLOCK)
                       INNER JOIN CONTRACT.CONTRACT AS CON WITH(NOLOCK) ON CG.CONTRACTID = CON.ID
                       INNER JOIN CONTRACT.PROCEDURETEMPLATE AS PT WITH(NOLOCK) ON CG.PROCEDURETEMPLATEID = PT.ID
                       INNER JOIN CONTRACT.PROCEDURECUPS AS PC WITH(NOLOCK) ON PT.ID = PC.PROCEDURESTEMPLATEID
                       INNER JOIN CONTRACT.CUPSENTITY AS CE WITH(NOLOCK) ON PC.CUPSID = CE.ID
                       LEFT JOIN CONTRACT.CUPSENTITYCONTRACTDESCRIPTIONS AS CECD WITH(NOLOCK) ON PC.CUPSENTITYCONTRACTDESCRIPTIONID = CECD.ID
                       LEFT JOIN CONTRACT.CONTRACTDESCRIPTIONS AS CD WITH(NOLOCK) ON CECD.CONTRACTDESCRIPTIONID = CD.ID
                       LEFT JOIN CONTRACT.CUPSSUBGROUP CSG ON CE.CUPSSUBGROUPID = CSG.ID
                       LEFT JOIN CONTRACT.CUPSGROUP CGR ON CSG.CUPSGROUPID = CGR.ID
              ) AS G ON G.CODGRUPO = GA.CODE
                        AND G.CUPS = A.CODSERIPS
                        AND A.IDDESCRIPCIONRELACIONADA = G.IDRELACION
                   LEFT JOIN
              (
                  SELECT EST.NUMEFOLIO, 
                         EST.IPCODPACI PACIENTE, 
                         ESTADIO
                  FROM DBO.INDIAGNOH AS EST WITH(NOLOCK)
                       INNER JOIN
                  (
                      SELECT MIN(NUMEFOLIO) NUMEFOLIO, 
                             IPCODPACI
                      FROM DBO.INDIAGNOH WITH(NOLOCK)
                      WHERE ESTADIO IS NOT NULL
                            AND ESTADIO NOT IN(93, 98, 99)
                      GROUP BY IPCODPACI
                  ) FE ON EST.NUMEFOLIO = FE.NUMEFOLIO
                          AND EST.IPCODPACI = FE.IPCODPACI --CONSULTA DE PRIMER ESTADIO 20201123
                  WHERE ESTADIO IS NOT NULL
              ) ESTA ON ING.IPCODPACI = ESTA.PACIENTE
              WHERE B.TIPSERTER = 1
              GROUP BY A.CODSERIPS, 
                       B.DESSERIPS, 
                       SERREASIT, 
                       ESTSERIPS, 
                       A.IPCODPACI, 
                       A.NUMINGRES, 
                       I.IPNOMCOMP, 
                       CECD.ID, 
                       CD.ID, 
                       CD.CODE, 
                       CD.NAME, 
                       E.NOMCENATE, 
                       A.UFUCODIGO, 
                       D.UFUDESCRI, 
                       A.NUMEFOLIO, 
                       PRO.CODPROSAL, 
                       PRO.NOMMEDICO, 
                       MANEXTPRO, 
                       A.FECORDMED, 
                       CG.CODE + '-' + CG.NAME, 
                       CSG.CODE + '-' + CSG.NAME, 
                       ESPMED.DESESPECI, 
                       DIAG.CODDIAGNO, 
                       DIAG.NOMDIAGNO, 
                       EA.CODE + ' - ' + EA.NAME, 
                       GA.CODE + ' - ' + GA.NAME, 
                       GA.LIQUIDATIONTYPE, 
                       EST.ESTADIO, 
                       GA.CODE, 
                       A.IDDESCRIPCIONRELACIONADA, 
                       G.CONTRATADO, 
                       G.COTIZADO, 
                       IIF(G.CODGRUPO IS NULL, 'NO', 'SI'), 
                       ESTA.ESTADIO, 
                       I.IPTIPODOC, 
                       I.IPTELEFON, 
                       I.IPTELMOVI,
					   HIS.FECHISPAC
              UNION ALL
              SELECT RTRIM(E.NOMCENATE) [CENTRO ATENCION], 
                     A.UFUCODIGO + ' - ' + RTRIM(D.UFUDESCRI) AS [UNIDAD FUNCIONAL], 
                     A.NUMEFOLIO AS FOLIO, 
                     CG.CODE + '-' + CG.NAME [GRUPO], 
                     CSG.CODE + '-' + CSG.NAME [SUBGRUPO], 
                     A.CODSERIPS [CODIGO CUPS], 
                     RTRIM(B.DESSERIPS) AS [DESCRIPCION SERVICIO], 
                     ISNULL(CD.CODE + ' - ' + CD.NAME, '') [DESCRIPCION RELACIONADA], 
                     cast (A.FECORDMED as date) [FECHA ORDENAMIENTO],
                     rtrim(convert(char(5), A.FECORDMED, 108)) [HORA ORDENAMIENTO],
					 CASE I.IPTIPODOC
                         WHEN '1'
                         THEN 'CC'
                         WHEN '2'
                         THEN 'CE'
                         WHEN '3'
                         THEN 'TI'
                         WHEN '4'
                         THEN 'RC'
                         WHEN '5'
                         THEN 'PA'
                         WHEN '6'
                         THEN 'AS'
                         WHEN '7'
                         THEN 'MS'
                         WHEN '8'
                         THEN 'NU'
                         WHEN '9'
                         THEN 'NV'
                         WHEN '10'
                         THEN 'CD'
                         WHEN '11'
                         THEN 'SC'
                         WHEN '12'
                         THEN 'PE'
                     END AS [TIPO IDENTIFICACION], 
                     A.IPCODPACI AS [IDENTIFICACION], 
                     I.IPTELEFON AS [TELEFONO FIJO], 
                     I.IPTELMOVI AS [TELEFONO MOVIL], 
                     'HIJO ' + CAST(RN.NUMHIJREG AS VARCHAR(20)) AS [PACIENTE], 
                     INGMH.NUMINGRES AS [INGRESO],
                     CASE
                         WHEN SERREASIT = '1'
                         THEN 'ESTUDIOS REALIZADOS EN SITIO'
                         WHEN ESTSERIPS IN('2', '3', '4')
                         THEN 'ESTUDIOS REALIZADOS'
                         WHEN ESTSERIPS = '6'
                         THEN 'ANULADO'
                         ELSE 'ESTUDIOS NO REALIZADOS'
                     END AS ESTADO, 
                     SUM(A.CANSERIPS) AS CANTIDAD, 
                     PRO.CODPROSAL [ID PROFESIONAL], 
                     PRO.NOMMEDICO AS PROFESIONAL, 
                     ESPMED.DESESPECI ESPECIALIDAD, 
                     DIAG.CODDIAGNO CIE10, 
                     DIAG.NOMDIAGNO DIAGNOSTICO,
                     CASE ESTA.ESTADIO
                         WHEN 0
                         THEN 'ESTADIO CLÍNICO (EC) 0 (TUMOR IN SITU)'
                         WHEN 1
                         THEN 'EC I O 1'
                         WHEN 2
                         THEN 'EC IA O 1A'
                         WHEN 3
                         THEN 'EC IA1'
                         WHEN 4
                         THEN 'EC IA2'
                         WHEN 5
                         THEN 'EC IB O 1B'
                         WHEN 6
                         THEN 'EC IB1'
                         WHEN 7
                         THEN 'EC IB2'
                         WHEN 8
                         THEN 'EC IC O 1C'
                         WHEN 9
                         THEN 'EC IS O 1S'
                         WHEN 10
                         THEN 'EC II O 2'
                         WHEN 11
                         THEN 'EC IIA O 2A'
                         WHEN 12
                         THEN 'EC IIA1'
                         WHEN 13
                         THEN 'EC IIA2'
                         WHEN 14
                         THEN 'EC IIB O 2B'
                         WHEN 15
                         THEN 'EC IIC O 2C'
                         WHEN 16
                         THEN 'EC III O 3'
                         WHEN 17
                         THEN 'EC IIIA O 3A'
                         WHEN 18
                         THEN 'EC IIIB O 3B'
                         WHEN 19
                         THEN 'EC IIIC O 3C'
                         WHEN 20
                         THEN 'EC IV O 4'
                         WHEN 21
                         THEN 'EC IVA O 4A'
                         WHEN 22
                         THEN 'EC IVB O 4B'
                         WHEN 23
                         THEN 'EC IVC O 4C'
                         WHEN 24
                         THEN 'EC 4S (PARA NEUROBLASTOMA)'
                         WHEN 25
                         THEN 'EC  V O 5'
                         WHEN 26
                         THEN 'EC ESTADIO IAB'
                         WHEN 55
                         THEN 'PERSONA CON ASEGURAMIENTO (RÉGIMEN SUBSIDIADO O CONTRIBUTIVO Y QUE NO SON PPNA) QUE RECIBIÓ SERVICIOS DE SALUD POR PARTE DEL ENTE TERRITORIALDURANTE EL PERIODO DE REPORTE'
                         WHEN 93
                         THEN 'SIN INFORMACIÓN DE ESTADIFICACIÓN EN HISTORIA CLÍNICA'
                         WHEN 98
                         THEN 'NO APLICA (ES CÁNCER DE PIEL BASOCELULAR, ES CÁNCER HEMATOLÓGICO O ES CÁNCER EN SNC, EXCEPTO NEUROBLASTOMA)'
                         WHEN 99
                         THEN 'DESCONOCIDO, EL DATO DE ESTA VARIABLE NO SE ENCUENTRA DESCRITO EN LOS SOPORTES CLÍNICOS'
                         ELSE ''
                     END ESTADIO,
                     CASE
                         WHEN MANEXTPRO = 0
                         THEN 'HOSPITALARIO'
                         ELSE 'AMBULATORIO'
                     END AS [TIPO SOLICITUD], 
                     EA.CODE + ' - ' + EA.NAME AS [ENTIDAD ADMINISTRADORA], 
                     GA.CODE + ' - ' + GA.NAME [GRUPO ATENCION],
                     CASE GA.LIQUIDATIONTYPE
                         WHEN 1
                         THEN 'PAGO POR SERVICIOS'
                         WHEN 2
                         THEN 'PGP'
                         WHEN 3
                         THEN 'FACTURA GLOBAL'
                         WHEN 4
                         THEN 'CAPITACION GLOBAL'
                         WHEN 5
                         THEN 'CONTROL'
                     END [TIPO CONTRATO], 
                     'TERAPIAS' [TIPO ORDEN], 
                     cast (HIS.FECHISPAC as date) [FECHA ATENCION],
                     rtrim(convert(char(5), HIS.FECHISPAC, 108)) [HORA ATENCION],
					 IIF(G.CODGRUPO IS NULL, 'NO', 'SI') CUBIERTO, 
                     ISNULL(G.CONTRATADO, 'NO') CONTRATADO, 
                     ISNULL(G.COTIZADO, 'NO') COTIZADO, 
                     GA.CODE [CODIGO GRUPO], 
                     A.IDDESCRIPCIONRELACIONADA, 
                     CAST(A.FECORDMED AS DATE) [FECHA BUSQUEDA]
              FROM DBO.HCORDPRON A WITH(NOLOCK)
                   JOIN DBO.INCUPSIPS B WITH(NOLOCK) ON A.CODSERIPS = B.CODSERIPS
                   JOIN DBO.HCINGRESORECNAC INGMH WITH(NOLOCK) ON A.NUMINGRES = INGMH.NUMINGRESHIJO
                   JOIN DBO.HCRECINAC RN WITH(NOLOCK) ON INGMH.NUMINGRESHIJO = RN.NUMINGRESHIJO
                   JOIN DBO.ADINGRESO AS ING ON ING.NUMINGRES = INGMH.NUMINGRESHIJO
                   JOIN DBO.INUNIFUNC D WITH(NOLOCK) ON A.UFUCODIGO = D.UFUCODIGO
                   JOIN DBO.INPACIENT I WITH(NOLOCK) ON A.IPCODPACI = I.IPCODPACI
                   JOIN DBO.ADCENATEN E WITH(NOLOCK) ON A.CODCENATE = E.CODCENATE
                   JOIN DBO.INPROFSAL PRO WITH(NOLOCK) ON A.CODPROSAL = PRO.CODPROSAL
                   JOIN CONTRACT.CUPSENTITY AS CUPS WITH(NOLOCK) ON CUPS.CODE = B.CODSERIPS
                   JOIN CONTRACT.CUPSSUBGROUP AS CSG WITH(NOLOCK) ON CSG.ID = CUPS.CUPSSUBGROUPID
                   JOIN CONTRACT.CUPSGROUP AS CG WITH(NOLOCK) ON CG.ID = CSG.CUPSGROUPID
                   JOIN DBO.HCHISPACA HIS WITH(NOLOCK) ON A.NUMINGRES = HIS.NUMINGRES
                                                          AND A.NUMEFOLIO = HIS.NUMEFOLIO
                   JOIN DBO.INESPECIA AS ESPMED WITH(NOLOCK) ON ESPMED.CODESPECI = HIS.CODESPTRA
                   JOIN DBO.INDIAGNOS AS DIAG WITH(NOLOCK) ON DIAG.CODDIAGNO = HIS.CODDIAGNO
                   JOIN CONTRACT.CAREGROUP AS GA WITH(NOLOCK) ON GA.ID = ING.GENCAREGROUP
                   JOIN CONTRACT.HEALTHADMINISTRATOR AS EA WITH(NOLOCK) ON EA.ID = ING.GENCONENTITY
                   LEFT JOIN CONTRACT.CUPSENTITYCONTRACTDESCRIPTIONS CECD WITH(NOLOCK) ON CECD.ID = A.IDDESCRIPCIONRELACIONADA
                   LEFT JOIN CONTRACT.CONTRACTDESCRIPTIONS CD WITH(NOLOCK) ON CD.ID = CECD.CONTRACTDESCRIPTIONID
                   LEFT JOIN DBO.INDIAGNOP AS EST WITH(NOLOCK) ON A.IPCODPACI = EST.IPCODPACI
                                                                  AND A.NUMINGRES = EST.NUMINGRES
                                                                  AND EST.NUMEFOLIO = A.NUMEFOLIO
                                                                  AND EST.ESTADIO IS NOT NULL
                   LEFT JOIN
              (
                  SELECT CG.CODE AS CODGRUPO, 
                         CG.NAME AS GRUPOATENCION, 
                         CON.CODE AS CODCONTRATO, 
                         CON.CONTRACTNAME AS NOMBRECONTRATO, 
                         PT.CODE AS CODPLANTIPROCE, 
                         PT.NAME AS PLANTILLAPROCEDIMIENTO, 
                         CGR.CODE AS CODIGOGRUPO, 
                         CGR.NAME AS GRUPO, 
                         CSG.CODE AS CODSUBGRUPO, 
                         CSG.NAME AS SUBGRUPO, 
                         CE.CODE AS CUPS, 
                         CE.DESCRIPTION AS DESCRIPCIONCUPS, 
                         CD.CODE AS CODRELACION, 
                         CD.NAME AS DESCRIPCIONRELACIONADA,
                         CASE PC.CONTRACTED
                             WHEN 1
                             THEN 'SI'
                             ELSE 'NO'
                         END AS CONTRATADO,
                         CASE PC.QUOTED
                             WHEN 1
                             THEN 'SI'
                             ELSE 'NO'
                         END AS COTIZADO, 
                         CECD.ID AS IDRELACION
                  FROM CONTRACT.CAREGROUP AS CG WITH(NOLOCK)
                       INNER JOIN CONTRACT.CONTRACT AS CON WITH(NOLOCK) ON CG.CONTRACTID = CON.ID
                       INNER JOIN CONTRACT.PROCEDURETEMPLATE AS PT WITH(NOLOCK) ON CG.PROCEDURETEMPLATEID = PT.ID
                       INNER JOIN CONTRACT.PROCEDURECUPS AS PC WITH(NOLOCK) ON PT.ID = PC.PROCEDURESTEMPLATEID
                       INNER JOIN CONTRACT.CUPSENTITY AS CE WITH(NOLOCK) ON PC.CUPSID = CE.ID
                       LEFT JOIN CONTRACT.CUPSENTITYCONTRACTDESCRIPTIONS AS CECD WITH(NOLOCK) ON PC.CUPSENTITYCONTRACTDESCRIPTIONID = CECD.ID
                       LEFT JOIN CONTRACT.CONTRACTDESCRIPTIONS AS CD WITH(NOLOCK) ON CECD.CONTRACTDESCRIPTIONID = CD.ID
                       LEFT JOIN CONTRACT.CUPSSUBGROUP CSG WITH(NOLOCK) ON CE.CUPSSUBGROUPID = CSG.ID
                       LEFT JOIN CONTRACT.CUPSGROUP CGR WITH(NOLOCK) ON CSG.CUPSGROUPID = CGR.ID
              ) AS G ON G.CODGRUPO = GA.CODE
                        AND G.CUPS = A.CODSERIPS
                        AND A.IDDESCRIPCIONRELACIONADA = G.IDRELACION
                   LEFT JOIN
              (
                  SELECT EST.NUMEFOLIO, 
                         EST.IPCODPACI PACIENTE, 
                         ESTADIO
                  FROM DBO.INDIAGNOH AS EST WITH(NOLOCK)
                       INNER JOIN
                  (
                      SELECT MIN(NUMEFOLIO) NUMEFOLIO, 
                             IPCODPACI
                      FROM DBO.INDIAGNOH
                      WHERE ESTADIO IS NOT NULL
                            AND ESTADIO NOT IN(93, 98, 99)
                      GROUP BY IPCODPACI
                  ) FE ON EST.NUMEFOLIO = FE.NUMEFOLIO
                          AND EST.IPCODPACI = FE.IPCODPACI --CONSULTA DE PRIMER ESTADIO 20201123
                  WHERE ESTADIO IS NOT NULL
              ) ESTA ON ING.IPCODPACI = ESTA.PACIENTE
              WHERE B.TIPSERTER = 1
              GROUP BY A.CODSERIPS, 
                       B.DESSERIPS, 
                       SERREASIT, 
                       ESTSERIPS, 
                       A.IPCODPACI, 
                       INGMH.NUMINGRES, 
                       RN.NUMHIJREG, 
                       CECD.ID, 
                       CD.ID, 
                       CD.CODE, 
                       CD.NAME, 
                       E.NOMCENATE, 
                       A.UFUCODIGO, 
                       D.UFUDESCRI, 
                       A.NUMEFOLIO, 
                       PRO.CODPROSAL, 
                       PRO.NOMMEDICO, 
                       MANEXTPRO, 
                       A.FECORDMED, 
                       CG.CODE + '-' + CG.NAME, 
                       CSG.CODE + '-' + CSG.NAME, 
                       ESPMED.DESESPECI, 
                       DIAG.CODDIAGNO, 
                       DIAG.NOMDIAGNO, 
                       EA.CODE + ' - ' + EA.NAME, 
                       GA.CODE + ' - ' + GA.NAME, 
                       GA.LIQUIDATIONTYPE, 
                       EST.ESTADIO, 
                       GA.CODE, 
                       A.IDDESCRIPCIONRELACIONADA, 
                       G.CONTRATADO, 
                       G.COTIZADO, 
                       IIF(G.CODGRUPO IS NULL, 'NO', 'SI'), 
                       ESTA.ESTADIO, 
                       I.IPTIPODOC, 
                       I.IPTELEFON, 
                       I.IPTELMOVI,
					   HIS.FECHISPAC)
          SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY, 
                 *, 
				 YEAR([FECHA BUSQUEDA]) AS 'AÑO FECHA BUSQUEDA',
				 MONTH([FECHA BUSQUEDA]) AS 'MES AÑO FECHA BUSQUEDA',
				 CASE MONTH([FECHA BUSQUEDA]) 
				      WHEN 1 THEN 'ENERO'
					  WHEN 2 THEN 'FEBRERO'
					  WHEN 3 THEN 'MARZO'
					  WHEN 4 THEN 'ABRIL'
					  WHEN 5 THEN 'MAYO'
					  WHEN 6 THEN 'JUNIO'
					  WHEN 7 THEN 'JULIO'
					  WHEN 8 THEN 'AGOSTO'
					  WHEN 9 THEN 'SEPTIEMBRE'
					  WHEN 10 THEN 'OCTUBRE'
					  WHEN 11 THEN 'NOVIEMBRE'
					  WHEN 12 THEN 'DICIEMBRE'
		         END AS 'MES NOMBRE FECHA BUSQUEDA', 
				 FORMAT(DAY([FECHA BUSQUEDA]), '00') AS 'DIA FECHA BUSQUEDA',
				 CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
          FROM CTE_ORDENAMIENTO_TERAPIAS;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting que consolida las órdenes de terapias médicas (código CUPS, grupo/subgrupo, cantidad) por ingreso, aplanando datos del paciente, profesional ordenador, especialidad, diagnóstico CIE-10 y estadio oncológico clínico. Incluye fecha/hora de ordenamiento y de atención, estado de ejecución del servicio, tipo de solicitud (hospitalario/ambulatorio), entidad administradora, grupo de atención y tipo de contrato. Adicionalmente verifica si el procedimiento está cubierto, contratado y cotizado según las plantillas contractuales vigentes.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewTherapiesRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewTherapiesRequests';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolidar las órdenes de terapias (CUPS con TIPSERTER=1) por paciente —incluyendo recién nacidos— enriquecidas con datos administrativos, clínicos, de contrato y cobertura, para reportes analíticos.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewTherapiesRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'DBO.INCUPSIPS debe tener clasificado el servicio con TIPSERTER = 1 para considerarse terapia.; Cada orden (HCORDPRON) debe tener ingreso (ADINGRESO o HCINGRESORECNAC), paciente (INPACIENT), unidad funcional (INUNIFUNC), centro de atención (ADCENATEN), profesional (INPROFSAL) y CUPS (CUPSENTITY) válidos por los INNER JOIN.; La historia clínica HCHISPACA debe existir para el par (NUMINGRES, NUMEFOLIO) de la orden.; El ingreso debe tener asignados grupo de atención (GENCAREGROUP) y entidad administradora (GENCONENTITY).; Para el segundo bloque (recién nacidos) el ingreso debe existir en HCINGRESORECNAC y HCRECINAC vinculado por NUMINGRESHIJO.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewTherapiesRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan órdenes cuyo CUPS tenga TIPSERTER = 1 (terapias).; El tipo de orden siempre se etiqueta como ''TERAPIAS''.; ID_COMPANY se obtiene de DB_NAME() truncado a VARCHAR(9), por lo que el reporte es por base de datos actual.; ULT_ACTUAL se calcula con GETDATE() convertido a ''Pakistan Standard Time''.; El estadio mostrado proviene del primer folio del paciente con estadio válido (excluye 93, 98, 99).; Cuando no hay coincidencia en la plantilla del grupo de atención, CONTRATADO y COTIZADO se reportan como ''NO'' por ISNULL.; La cantidad reportada es la suma de CANSERIPS agrupada por servicio/paciente/folio/profesional/etc.; La fecha de búsqueda se usa para derivar año, mes numérico, mes nombre (en español) y día con formato ''00''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewTherapiesRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Terapias; Órdenes médicas / ordenamiento; CUPS (códigos de procedimientos); Grupo y subgrupo CUPS; Centro de atención; Unidad funcional; Ingreso hospitalario; Folio clínico; Paciente y tipos de identificación (CC, CE, TI, RC, PA, AS, MS, NU, NV, CD, SC, PE); Profesional de la salud y especialidad; Diagnóstico CIE-10; Estadio clínico oncológico (EC 0..V, 4S, IAB, 55, 93, 98, 99); Tipo de solicitud hospitalario/ambulatorio; Entidad administradora (EPS); Grupo de atención y contrato; Tipo de liquidación (pago por servicios, PGP, factura global, capitación global, control); Plantilla de procedimientos contratados/cotizados; Cobertura del servicio en contrato; Recién nacido / ingreso del hijo; Estado del servicio (realizado, en sitio, anulado, no realizado)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewTherapiesRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si B.TIPSERTER = 1 → Solo se incluyen servicios CUPS clasificados como tipo terapia; los demás tipos quedan excluidos del reporte.; si SERREASIT = ''1'' → Estado se reporta como ''ESTUDIOS REALIZADOS EN SITIO'' else Si ESTSERIPS IN (2,3,4) → ''ESTUDIOS REALIZADOS''; si ESTSERIPS = ''6'' → ''ANULADO''; en otro caso → ''ESTUDIOS NO REALIZADOS''; si MANEXTPRO = 0 → Tipo solicitud = ''HOSPITALARIO'' else Tipo solicitud = ''AMBULATORIO''; si G.CODGRUPO IS NULL (no existe match en plantilla de procedimientos del grupo de atención) → CUBIERTO = ''NO'' else CUBIERTO = ''SI''; si Segundo bloque del UNION ALL: ingreso del paciente está vinculado a un recién nacido (HCINGRESORECNAC/HCRECINAC) → Se reemplaza el nombre del paciente por ''HIJO '' + número de hijo del registro y se usa el ingreso del hijo (NUMINGRESHIJO); si GA.LIQUIDATIONTYPE → Se traduce a etiqueta de tipo de contrato: 1=PAGO POR SERVICIOS, 2=PGP, 3=FACTURA GLOBAL, 4=CAPITACION GLOBAL, 5=CONTROL; si INDIAGNOH.ESTADIO IS NOT NULL AND ESTADIO NOT IN (93,98,99) → Se selecciona el primer folio (MIN(NUMEFOLIO)) por paciente para determinar el estadio clínico inicial reportado', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewTherapiesRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'DBO.ADINGRESO; DBO.HCORDPRON; DBO.INCUPSIPS; DBO.INPACIENT; DBO.INUNIFUNC; DBO.ADCENATEN; DBO.INPROFSAL; CONTRACT.CUPSENTITY; CONTRACT.CUPSSUBGROUP; CONTRACT.CUPSGROUP; DBO.HCHISPACA; DBO.INESPECIA; DBO.INDIAGNOS; CONTRACT.CAREGROUP; CONTRACT.HEALTHADMINISTRATOR; CONTRACT.CUPSENTITYCONTRACTDESCRIPTIONS; CONTRACT.CONTRACTDESCRIPTIONS; DBO.INDIAGNOP; CONTRACT.CONTRACT; CONTRACT.PROCEDURETEMPLATE; CONTRACT.PROCEDURECUPS; DBO.INDIAGNOH; DBO.HCINGRESORECNAC; DBO.HCRECINAC', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewTherapiesRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewTherapiesRequests';
GO
