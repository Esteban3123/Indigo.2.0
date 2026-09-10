

CREATE VIEW [Report].[ViewPendientesDeOrdenesParaclinicos]
as
----*******IMAGENES*******-----
WITH CTE_INGRESOS_CON_PENDIENTES
AS
(

SELECT NUMINGRES ,IPCODPACI  FROM 
(
   SELECT DISTINCT NUMINGRES, IPCODPACI FROM dbo.HCORDIMAG A WITH (NOLOCK)  WHERE  A.GENSERVICEORDER IS NULL and ESTSERIPS IN ('2','3','4') AND A.MANEXTPRO = 0
   UNION ALL 
   SELECT DISTINCT NUMINGRES, IPCODPACI FROM dbo.HCORDLABO  A WITH (NOLOCK) WHERE A.GENSERVICEORDER IS NULL AND A.ESTSERIPS IN ('3','4') AND A.MANEXTPRO = 0
   UNION ALL 
   SELECT DISTINCT NUMINGRES, IPCODPACI FROM dbo.HCORDPATO  A WITH (NOLOCK) WHERE A.GENSERVICEORDER IS NULL AND A.ESTSERIPS IN ('3','4') AND A.MANEXTPRO = 0
   ) G GROUP BY G.NUMINGRES,G.IPCODPACI 
),

CTE_IMAGENES
AS
(
SELECT	CONCAT('HCORDIMAG', '-', A.AUTO) ID,
			'IMAGENES' EntityName,iif(A.GENSERVICEORDER IS NULL,'SIN ORDEN','CON ORDEN')  'ORDENE DE SERVICIO' ,CASE A.MANEXTPRO WHEN 0 THEN 'HOSPITALARIO' ELSE 'AMBULATORIO' END 'AMBITO', 
			A.CODSERIPS 'CODIGO CUPS',B.DESSERIPS 'PROCEDIMIENTO CUPS',
			CONCAT(RTRIM(D.UFUCODIGO), ' - ', RTRIM(D.UFUDESCRI)) AS 'UNIDAD FUNCIONAL',
			CONCAT(RTRIM(C.CODIGONIT), ' - ', RTRIM(C.NOMMEDICO)) AS 'MEDICO ORDENAMIENTO',
			A.FECORDMED AS 'FECHA ORDENAMIENTO',A.NUMEFOLIO AS 'FOLIO ORDENAMIENTO',
			A.NUMFOLINT AS 'FOLIO INTERPRETACION',A.CODPROINT AS 'MEDICO INTERPRETO',A.INTERPRET AS 'INTERPRETACION',
			A.CANSERIPS 'CANTIDAD',	A.NUMINGRES 'INGRESO',A.IPCODPACI 'IDENTIFICACION',RTRIM(PAC.IPNOMCOMP) 'PACIENTE' ,
			CASE WHEN SERREASIT='1' THEN 'Estudios Realizados' WHEN  ESTSERIPS IN ('2','3','4') THEN 'Estudios Realizados' when ESTSERIPS = '6' then 'Anulado' ELSE 'Estudios No Realizados' END AS 'ESTADO',
			A.MEDREALEC as 'MEDICO LECTURA',
			A.FECHLECT as 'FECHA LECTURA',
			CASE WHEN A.MEDREALEC IS NULL THEN 'NO' ELSE 'SI' END as 'LECTURA REALIZADA',
			ISNULL(cd.Code + ' - ' + cd.Name, '') 'DESCRIPCION RELACIONADA',A.NOMARCIMG 'ADJUNTO'
	FROM dbo.ADINGRESO ing
	INNER JOIN dbo.HCORDIMAG A with (nolock) ON ing.NUMINGRES = A.NUMINGRES
	INNER JOIN DBO.INPACIENT AS PAC with (nolock) ON PAC.IPCODPACI =A.IPCODPACI 
	INNER JOIN dbo.INCUPSIPS B with (nolock) ON A.CODSERIPS=B.CODSERIPS 
	INNER JOIN dbo.INPROFSAL C with (nolock) ON A.CODPROSAL=C.CODPROSAL 	
	INNER JOIN dbo.INUNIFUNC D with (nolock) ON A.UFUCODIGO=D.UFUCODIGO
	INNER JOIN CTE_INGRESOS_CON_PENDIENTES AS PEN with (nolock) ON PEN.NUMINGRES =ING.NUMINGRES 
	LEFT JOIN dbo.INPROFSAL CR with (nolock) ON COALESCE(A.MEDREALEC, A.CODPROSAL)=CR.CODPROSAL 
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd with (nolock) ON cecd.Id = a.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.ContractDescriptions cd with (nolock) ON cd.Id = cecd.ContractDescriptionId
	WHERE  A.GENSERVICEORDER IS NULL and ESTSERIPS IN ('2','3','4') AND A.MANEXTPRO = 0 --OR ISNULL(ing.TRATAESPECIA, 0) = 3 

UNION ALL

	SELECT	CONCAT('HCORDIMAG', '-', A.AUTO) ID,
			'HCORDIMAG' EntityName,iif(A.GENSERVICEORDER IS NULL,'SIN ORDEN','CON ORDEN')  'ORDENE DE SERVICIO' ,
			CASE A.MANEXTPRO WHEN 0 THEN 'HOSPITALARIO' ELSE 'AMBULATORIO' END 'AMBITO', 
			A.CODSERIPS 'CODIGO CUPS',B.DESSERIPS 'PROCEDIMIENTO CUPS',
			CONCAT(RTRIM(D.UFUCODIGO), ' - ', RTRIM(D.UFUDESCRI)) AS 'UNIDAD FUNCIONAL',
			CONCAT(RTRIM(C.CODIGONIT), ' - ', RTRIM(C.NOMMEDICO)) AS 'MEDICO ORDENAMIENTO',
			A.FECORDMED AS 'FECHA ORDENAMIENTO',A.NUMEFOLIO AS 'FOLIO ORDENAMIENTO',
			A.NUMFOLINT AS 'FOLIO INTERPRETACION',A.CODPROINT AS 'MEDICO INTERPRETO',A.INTERPRET AS 'INTERPRETACION',
			A.CANSERIPS 'CANTIDAD',	A.NUMINGRES 'INGRESO',A.IPCODPACI 'IDENTIFICACION',RTRIM(PAC.IPNOMCOMP) 'PACIENTE' ,
			CASE WHEN SERREASIT='1' THEN 'Estudios Realizados en Sitio' WHEN  ESTSERIPS IN ('2','3','4') THEN 'Estudios Realizados' when ESTSERIPS = '6' then 'Anulado' ELSE 'Estudios No Realizados' END AS 'ESTADO',
			A.MEDREALEC as 'MEDICO LECTURA',
			A.FECHLECT as 'FECHA LECTURA',
			CASE WHEN A.MEDREALEC IS NULL THEN 'NO' ELSE 'SI' END as 'LECTURA REALIZADA',
			ISNULL(cd.Code + ' - ' + cd.Name, '') 'DESCRIPCION RELACIONADA',A.NOMARCIMG 'ADJUNTO'
	FROM dbo.HCORDIMAG A 
	INNER JOIN dbo.INCUPSIPS B with (nolock) ON A.CODSERIPS=B.CODSERIPS 
	INNER JOIN DBO.INPACIENT AS PAC with (nolock) ON PAC.IPCODPACI =A.IPCODPACI
	INNER JOIN dbo.INPROFSAL C with (nolock) ON A.CODPROSAL=C.CODPROSAL 	
	INNER JOIN dbo.INUNIFUNC D with (nolock) ON A.UFUCODIGO=D.UFUCODIGO
	INNER JOIN dbo.HCINGRESORECNAC INGMH  with (nolock) ON a.NUMINGRES = INGMH .NUMINGRESHIJO
	INNER JOIN dbo.HCRECINAC RN  with (nolock) ON INGMH.NUMINGRESHIJO  = rn.NUMINGRESHIJO  
	INNER JOIN dbo.ADINGRESO AS ING  with (nolock) ON ING.NUMINGRES = INGMH.NUMINGRESHIJO  
	INNER JOIN CTE_INGRESOS_CON_PENDIENTES AS PEN with (nolock) ON PEN.NUMINGRES =ING.NUMINGRES
	LEFT JOIN dbo.INPROFSAL CR with (nolock) ON COALESCE(A.MEDREALEC, A.CODPROSAL)=CR.CODPROSAL
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd with (nolock) ON cecd.Id = a.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.ContractDescriptions cd with (nolock) ON cd.Id = cecd.ContractDescriptionId
	WHERE  A.GENSERVICEORDER IS NULL and ESTSERIPS IN ('2','3','4')  AND A.MANEXTPRO = 0 --OR ISNULL(ing.TRATAESPECIA, 0) = 3 
),

------********------
--LABORATORIOS
CTE_LABORATORIOS
AS
(
SELECT	CONCAT('HCORDLABO', '-', A.AUTO) ID,
			'LABORATORIOS' EntityName,iif(A.GENSERVICEORDER IS NULL,'SIN ORDEN','CON ORDEN')  'ORDENE DE SERVICIO' ,CASE A.MANEXTPRO WHEN 0 THEN 'HOSPITALARIO' ELSE 'AMBULATORIO' END 'AMBITO', 
			A.CODSERIPS 'CODIGO CUPS',B.DESSERIPS 'PROCEDIMIENTO CUPS',
			CONCAT(RTRIM(D.UFUCODIGO), ' - ', RTRIM(D.UFUDESCRI)) AS 'UNIDAD FUNCIONAL',
			CONCAT(RTRIM(C.CODIGONIT), ' - ', RTRIM(C.NOMMEDICO)) AS 'MEDICO ORDENAMIENTO',
			A.FECORDMED AS 'FECHA ORDENAMIENTO',A.NUMEFOLIO AS 'FOLIO ORDENAMIENTO',
			A.NUMFOLINT AS 'FOLIO INTERPRETACION',A.CODPROINT AS 'MEDICO INTERPRETO',A.INTERPRET AS 'INTERPRETACION',
			A.CANSERIPS 'CANTIDAD',	A.NUMINGRES 'INGRESO',A.IPCODPACI 'IDENTIFICACION',RTRIM(PAC.IPNOMCOMP) 'PACIENTE' ,
			CASE WHEN SERREASIT='1' THEN 'Estudios Realizados' WHEN A.ESTSERIPS IN ('3','4') THEN 'Estudios Realizados' WHEN A.ESTSERIPS = '6' THEN 'Anulados' 
			WHEN A.ESTSERIPS= '1' THEN 'Solicitados'	ELSE 'No Realizados' END AS 'ESTADO',
			CASE 
				WHEN ISNULL(A.PROFRESULT, '') <> '' THEN RTRIM(LTRIM(A.PROFRESULT))
				WHEN ISNULL(I.CODPROSAL, '') <> '' THEN RTRIM(LTRIM(I.CODPROSAL))
				WHEN ISNULL(E.CODPROSAL, '') <> '' THEN RTRIM(LTRIM(E.CODPROSAL))
				ELSE NULL END  as 'MEDICO LECTURA',
			I.FECREGIST as 'FECHA LECTURA',
            'SI' 'LECTURA REALIZADA',
			ISNULL(cd.Code + ' - ' + cd.Name, '') 'DESCRIPCION RELACIONADA',A.NOMARCLAB 'ADJUNTO'
	FROM dbo.ADINGRESO ing
	INNER JOIN dbo.HCORDLABO A  with (nolock) ON ing.NUMINGRES = a.NUMINGRES
	INNER JOIN DBO.INPACIENT AS PAC with (nolock) ON PAC.IPCODPACI =A.IPCODPACI
	INNER JOIN dbo.INCUPSIPS B with (nolock) ON A.CODSERIPS=B.CODSERIPS 
	INNER JOIN dbo.INPROFSAL C with (nolock) ON A.CODPROSAL=C.CODPROSAL 	
	INNER JOIN dbo.INUNIFUNC D with (nolock) ON A.UFUCODIGO=D.UFUCODIGO
	INNER JOIN CTE_INGRESOS_CON_PENDIENTES AS PEN with (nolock) ON PEN.NUMINGRES =ING.NUMINGRES
	LEFT JOIN
	(
		SELECT I.AUTOLABOR, MIN(I.AUTO) AUTO
		FROM dbo.INTERCTRL I
		WHERE I.NUMUESTRA = 1
		GROUP BY I.AUTOLABOR
	) ID ON A.AUTO = ID.AUTOLABOR
	LEFT JOIN dbo.INTERCTRL I with (nolock) ON ID.AUTO = I.AUTO
	LEFT JOIN dbo.INPROFSAL C2 with (nolock) ON I.CODPROSAL=C2.CODPROSAL
	LEFT JOIN dbo.HCHISPACA E with (nolock) ON A.NUMINGRES = E.NUMINGRES AND A.NUMEFOLIO = E.NUMEFOLIO
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd with (nolock) ON cecd.Id = A.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.ContractDescriptions cd with (nolock) ON cd.Id = cecd.ContractDescriptionId
	WHERE A.GENSERVICEORDER IS NULL AND A.ESTSERIPS IN ('3','4') AND A.MANEXTPRO = 0 
UNION ALL

SELECT	CONCAT('HCORDLABO', '-', A.AUTO) ID,
			'LABORATORIOS' EntityName,iif(A.GENSERVICEORDER IS NULL,'SIN ORDEN','CON ORDEN')  'ORDENE DE SERVICIO' ,CASE A.MANEXTPRO WHEN 0 THEN 'HOSPITALARIO' ELSE 'AMBULATORIO' END 'AMBITO', 
			A.CODSERIPS 'CODIGO CUPS',B.DESSERIPS 'PROCEDIMIENTO CUPS',
			CONCAT(RTRIM(D.UFUCODIGO), ' - ', RTRIM(D.UFUDESCRI)) AS 'UNIDAD FUNCIONAL',
			CONCAT(RTRIM(C.CODIGONIT), ' - ', RTRIM(C.NOMMEDICO)) AS 'MEDICO ORDENAMIENTO',
			A.FECORDMED AS 'FECHA ORDENAMIENTO',A.NUMEFOLIO AS 'FOLIO ORDENAMIENTO',
			A.NUMFOLINT AS 'FOLIO INTERPRETACION',A.CODPROINT AS 'MEDICO INTERPRETO',A.INTERPRET AS 'INTERPRETACION',
			A.CANSERIPS 'CANTIDAD',	A.NUMINGRES 'INGRESO',A.IPCODPACI 'IDENTIFICACION',RTRIM(PAC.IPNOMCOMP) 'PACIENTE' ,
			CASE WHEN SERREASIT='1' THEN 'Estudios Realizados' WHEN A.ESTSERIPS IN ('3','4') THEN 'Estudios Realizados' WHEN A.ESTSERIPS = '6' THEN 'Anulados' 
			WHEN A.ESTSERIPS= '1' THEN 'Solicitados'	ELSE 'No Realizados' END AS 'ESTADO',
			CASE 
				WHEN ISNULL(A.PROFRESULT, '') <> '' THEN RTRIM(LTRIM(A.PROFRESULT))
				WHEN ISNULL(I.CODPROSAL, '') <> '' THEN RTRIM(LTRIM(I.CODPROSAL))
				WHEN ISNULL(E.CODPROSAL, '') <> '' THEN RTRIM(LTRIM(E.CODPROSAL))
				ELSE NULL END  as 'MEDICO LECTURA',
			I.FECREGIST as 'FECHA LECTURA',
            'SI' 'LECTURA REALIZADA',
			ISNULL(cd.Code + ' - ' + cd.Name, '') 'DESCRIPCION RELACIONADA',A.NOMARCLAB 'ADJUNTO'
	FROM dbo.HCORDLABO A 
	INNER JOIN dbo.INCUPSIPS B with (nolock) ON A.CODSERIPS = B.CODSERIPS 
	INNER JOIN DBO.INPACIENT AS PAC with (nolock) ON PAC.IPCODPACI =A.IPCODPACI
	INNER JOIN dbo.INPROFSAL C with (nolock) ON A.CODPROSAL = C.CODPROSAL 
	INNER JOIN dbo.INUNIFUNC D with (nolock) ON A.UFUCODIGO = D.UFUCODIGO
	INNER JOIN CTE_INGRESOS_CON_PENDIENTES AS PEN with (nolock) ON PEN.NUMINGRES =A.NUMINGRES
	INNER JOIN
	(
		SELECT INGMH.NUMINGRESHIJO, MIN(INGMH.ID) ID
		FROM dbo.HCINGRESORECNAC INGMH
		GROUP BY INGMH.NUMINGRESHIJO
	) INGMHD ON A.NUMINGRES = INGMHD.NUMINGRESHIJO
	INNER JOIN dbo.HCINGRESORECNAC INGMH with (nolock) ON INGMHD.ID = INGMH.ID
	INNER JOIN dbo.HCRECINAC RN with (nolock) ON INGMH.NUMINGRESHIJO  = rn.NUMINGRESHIJO  
	INNER JOIN  dbo.ADINGRESO AS ING with (nolock) ON ING.NUMINGRES = INGMH.NUMINGRESHIJO  
	LEFT JOIN
	(
		SELECT I.AUTOLABOR, MIN(I.AUTO) AUTO
		FROM dbo.INTERCTRL I
		WHERE I.NUMUESTRA = 1
		GROUP BY I.AUTOLABOR
	) ID ON A.AUTO = ID.AUTOLABOR
	LEFT JOIN dbo.INTERCTRL I with (nolock) ON ID.AUTO = I.AUTO
	LEFT JOIN dbo.INPROFSAL C2 with (nolock) ON I.CODPROSAL = C2.CODPROSAL 
	LEFT JOIN dbo.HCHISPACA E with (nolock) ON INGMH.NUMINGRES = E.NUMINGRES AND A.NUMEFOLIO = E.NUMEFOLIO
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd with (nolock) ON cecd.Id = A.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.ContractDescriptions cd with (nolock) ON cd.Id = cecd.ContractDescriptionId
	WHERE A.GENSERVICEORDER IS NULL AND A.ESTSERIPS IN ('3','4') AND A.MANEXTPRO = 0 
),

CTE_PATOLOGIAS
AS
(
SELECT	CONCAT('HCORDPATO', '-', A.AUTO) Id,
			'PATOLOGIAS' EntityName,iif(A.GENSERVICEORDER IS NULL,'SIN ORDEN','CON ORDEN')  'ORDENE DE SERVICIO' ,
			CASE A.MANEXTPRO WHEN 0 THEN 'HOSPITALARIO' ELSE 'AMBULATORIO' END 'AMBITO', 
			A.CODSERIPS 'CODIGO CUPS',B.DESSERIPS 'PROCEDIMIENTO CUPS',
			CONCAT(RTRIM(D.UFUCODIGO), ' - ', RTRIM(D.UFUDESCRI)) AS 'UNIDAD FUNCIONAL',
			CONCAT(RTRIM(C.CODIGONIT), ' - ', RTRIM(C.NOMMEDICO)) AS 'MEDICO ORDENAMIENTO',
			A.FECORDMED AS 'FECHA ORDENAMIENTO',A.NUMEFOLIO AS 'FOLIO ORDENAMIENTO',
			A.NUMFOLINT AS 'FOLIO INTERPRETACION',A.CODPROINT AS 'MEDICO INTERPRETO',A.INTERPRET AS 'INTERPRETACION',
			A.CANSERIPS 'CANTIDAD',	A.NUMINGRES 'INGRESO',A.IPCODPACI 'IDENTIFICACION',RTRIM(PAC.IPNOMCOMP) 'PACIENTE' ,
			CASE 
				WHEN ESTSERIPS IN ('3','4') THEN 'Estudios Realizados' when ESTSERIPS = '6' then 'Anulados'	when ESTSERIPS = '1' then 'Solicitados'
				ELSE 'No Realizados' END AS 'ESTADO',
			CASE WHEN A.PROFRESULTADO IS NULL THEN A.CODPROSAL ELSE A.PROFRESULTADO END as 'MEDICO LECTURA',

			CASE WHEN A.PROFRESULTADO IS NULL THEN A.FECORDMED ELSE A.FECHARESULT END as 'FECHA LECTURA',
			IIF(CASE WHEN A.PROFRESULTADO IS NULL THEN A.FECORDMED ELSE A.FECHARESULT END IS NULL,'NO','SI') 'LECTURA REALIZADA',
			ISNULL(cd.Code + ' - ' + cd.Name, '') 'DESCRIPCION RELACIONADA',A.NOMARCPAT 'ADJUNTO'
	FROM dbo.ADINGRESO ing with (nolock)
	INNER JOIN dbo.HCORDPATO A with (nolock) ON ing.NUMINGRES = A.NUMINGRES
	INNER JOIN DBO.INPACIENT AS PAC with (nolock) ON PAC.IPCODPACI =A.IPCODPACI
	INNER JOIN dbo.INCUPSIPS B with (nolock) ON A.CODSERIPS=B.CODSERIPS 
	INNER JOIN dbo.INPROFSAL C with (nolock) ON A.CODPROSAL=C.CODPROSAL 
	INNER JOIN dbo.INUNIFUNC D with (nolock) ON A.UFUCODIGO=D.UFUCODIGO
	INNER JOIN dbo.INPACIENT i with (nolock) ON a.IPCODPACI = i.IPCODPACI 
	INNER JOIN CTE_INGRESOS_CON_PENDIENTES AS PEN with (nolock) ON PEN.NUMINGRES =ING.NUMINGRES
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd with (nolock) ON cecd.Id = a.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.ContractDescriptions cd with (nolock) ON cd.Id = cecd.ContractDescriptionId
	WHERE A.GENSERVICEORDER IS NULL AND A.ESTSERIPS IN ('3','4') AND A.MANEXTPRO = 0

UNION ALL

SELECT	CONCAT('HCORDPATO', '-', A.AUTO) Id,
			'PATOLOGIAS' EntityName,iif(A.GENSERVICEORDER IS NULL,'SIN ORDEN','CON ORDEN')  'ORDENE DE SERVICIO' ,
			CASE A.MANEXTPRO WHEN 0 THEN 'HOSPITALARIO' ELSE 'AMBULATORIO' END 'AMBITO', 
			A.CODSERIPS 'CODIGO CUPS',B.DESSERIPS 'PROCEDIMIENTO CUPS',
			CONCAT(RTRIM(D.UFUCODIGO), ' - ', RTRIM(D.UFUDESCRI)) AS 'UNIDAD FUNCIONAL',
			CONCAT(RTRIM(C.CODIGONIT), ' - ', RTRIM(C.NOMMEDICO)) AS 'MEDICO ORDENAMIENTO',
			A.FECORDMED AS 'FECHA ORDENAMIENTO',A.NUMEFOLIO AS 'FOLIO ORDENAMIENTO',
			A.NUMFOLINT AS 'FOLIO INTERPRETACION',A.CODPROINT AS 'MEDICO INTERPRETO',A.INTERPRET AS 'INTERPRETACION',
			A.CANSERIPS 'CANTIDAD',	A.NUMINGRES 'INGRESO',A.IPCODPACI 'IDENTIFICACION',RTRIM(PAC.IPNOMCOMP) 'PACIENTE' ,
			CASE 
				WHEN ESTSERIPS IN ('3','4') THEN 'Estudios Realizados' when ESTSERIPS = '6' then 'Anulados'	when ESTSERIPS = '1' then 'Solicitados'
				ELSE 'No Realizados' END AS 'ESTADO',
			CASE WHEN A.PROFRESULTADO IS NULL THEN A.CODPROSAL ELSE A.PROFRESULTADO END as 'MEDICO LECTURA',
			CASE WHEN A.PROFRESULTADO IS NULL THEN A.FECORDMED ELSE A.FECHARESULT END as 'FECHA LECTURA',
			IIF(CASE WHEN A.PROFRESULTADO IS NULL THEN A.FECORDMED ELSE A.FECHARESULT END IS NULL,'NO','SI') 'LECTURA REALIZADA',
			ISNULL(cd.Code + ' - ' + cd.Name, '') 'DESCRIPCION RELACIONADA',A.NOMARCPAT 'ADJUNTO'
	
	FROM dbo.HCORDPATO A  with (nolock) 
	INNER JOIN dbo.INCUPSIPS B with (nolock) ON A.CODSERIPS=B.CODSERIPS 
	INNER JOIN dbo.INPROFSAL C with (nolock) ON A.CODPROSAL=C.CODPROSAL 
	INNER JOIN dbo.INUNIFUNC D with (nolock) ON A.UFUCODIGO=D.UFUCODIGO
	INNER JOIN dbo.HCINGRESORECNAC INGMH  with (nolock) ON a.NUMINGRES = INGMH .NUMINGRESHIJO
	INNER JOIN dbo.HCRECINAC RN  with (nolock) ON INGMH.NUMINGRESHIJO  = rn.NUMINGRESHIJO  
	INNER JOIN dbo.ADINGRESO AS ING  with (nolock) ON ING.NUMINGRES = INGMH.NUMINGRESHIJO 
	INNER JOIN DBO.INPACIENT AS PAC with (nolock) ON PAC.IPCODPACI =A.IPCODPACI
	INNER JOIN dbo.INPACIENT i with (nolock) ON a.IPCODPACI = i.IPCODPACI 
	INNER JOIN CTE_INGRESOS_CON_PENDIENTES AS PEN with (nolock) ON PEN.NUMINGRES =ING.NUMINGRES
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd with (nolock) ON cecd.Id = a.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.ContractDescriptions cd with (nolock) ON cd.Id = cecd.ContractDescriptionId
	WHERE A.GENSERVICEORDER IS NULL AND A.ESTSERIPS IN ('3','4') AND A.MANEXTPRO = 0
),

CTE_ALTA_MEDICA
AS
(
  SELECT EGR.NUMINGRES ,EGR.IPCODPACI ,MAX(FECALTPAC) 'FECHA ALTA' FROM DBO.HCREGEGRE EGR with (nolock) 
  INNER JOIN CTE_INGRESOS_CON_PENDIENTES AS PEN with (nolock) ON PEN.NUMINGRES =EGR.NUMINGRES 
  GROUP BY EGR.NUMINGRES ,EGR.IPCODPACI
),

CTE_ESTANCIA_MAYOR
AS
(
  SELECT C.NUMINGRES ,C.IPCODPACI,C.CODICAMAS  ,C.FECINIEST 'FECHA ESTANCIA',C.REGESTADO,C.CODTIPEST,C.ID    FROM dbo.CHREGESTA C with (nolock)
  INNER JOIN CTE_INGRESOS_CON_PENDIENTES AS PEN ON PEN.NUMINGRES =C.NUMINGRES 
  INNER JOIN(SELECT C.NUMINGRES ,C.IPCODPACI ,MAX(C.ID ) IDD FROM dbo.CHREGESTA C with (nolock) GROUP BY C.NUMINGRES ,C.IPCODPACI) AS G ON G.IDD =C.ID 
),

CTE_CAMAS
AS
(
     SELECT FUN.UFUDESCRI 'UNIDAD FUNCIONAL' ,C.IPCODPACI 'IDENTIFICACION' ,C.NUMINGRES 'INGRESO',A.NUMCAMHOS 'CAMA',G.DESTIPEST 'TIPO ESTANCIA'  ,C.ID  'ID_ESTANCIA', 
	 A.CODICAMAS 'ID_CAMAS', CEN.NOMCENATE 'CENTRO DE ATENCION',A.CODCLAHAB ,A.CODCLACAM 
	 FROM dbo.CHCAMASHO A with (nolock)
	 INNER JOIN DBO.INUNIFUNC AS FUN with (nolock) ON A.UFUCODIGO =FUN.UFUCODIGO 
	 INNER JOIN CTE_ESTANCIA_MAYOR C with (nolock) ON A.CODICAMAS=C.CODICAMAS AND C.REGESTADO = 1 
     INNER JOIN dbo.CHTIPESTA G with (nolock) ON G.CODTIPEST=C.CODTIPEST 
	 INNER JOIN DBO.ADCENATEN AS CEN with (nolock) ON CEN.CODCENATE =A.CODCENATE 
),

CTE_HISTORIAS_AMBULATORIAS
AS
(
  SELECT HIS.NUMINGRES ,HIS.IPCODPACI ,MAX(FECHISPAC) AS 'FECHA ALTA'    FROM  DBO.HCHISPACA HIS with (nolock)
  INNER JOIN CTE_INGRESOS_CON_PENDIENTES AS PEN with (nolock) ON PEN.NUMINGRES =HIS.NUMINGRES 
  WHERE HIS.GENCONEXT =1
  GROUP BY HIS.NUMINGRES ,HIS.IPCODPACI
),

CTE_PARACLINICOS as (
SELECT img.*,ing.IESTADOIN 'ESTADO INGRESO' ,f.InvoiceNumber 'NRO FACTURA' ,C.RadicatedConsecutive 'NRO RADICADO',ALT.[FECHA ALTA] ,CAM.CAMA 
FROM CTE_IMAGENES  as img with (nolock) 
inner join dbo.ADINGRESO as ing with (nolock) ON img.INGRESO =ing.NUMINGRES 
left join Billing .Invoice as f with (nolock) ON f.AdmissionNumber =ing.NUMINGRES and f.Status =1
LEFT JOIN Portfolio.RadicateInvoiceD AS RAD with (nolock) ON RAD.InvoiceNumber =F.InvoiceNumber 
LEFT JOIN Portfolio .RadicateInvoiceC AS C with (nolock) ON C.Id =RAD.RadicateInvoiceCId 
LEFT JOIN CTE_ALTA_MEDICA AS ALT with (nolock) ON ALT.NUMINGRES =IMG.INGRESO 
LEFT JOIN CTE_CAMAS AS CAM with (nolock) ON  CAM.INGRESO =IMG.INGRESO
UNION ALL
SELECT img.*,ing.IESTADOIN 'ESTADO INGRESO' ,f.InvoiceNumber 'NRO FACTURA' ,C.RadicatedConsecutive 'NRO RADICADO',ALT.[FECHA ALTA] ,CAM.CAMA
FROM CTE_LABORATORIOS as img with (nolock)
inner join dbo.ADINGRESO as ing with (nolock) ON img.INGRESO =ing.NUMINGRES 
left join Billing .Invoice as f with (nolock) ON f.AdmissionNumber =ing.NUMINGRES and f.Status =1
LEFT JOIN Portfolio.RadicateInvoiceD AS RAD with (nolock) ON RAD.InvoiceNumber =F.InvoiceNumber 
LEFT JOIN Portfolio .RadicateInvoiceC AS C with (nolock) ON C.Id =RAD.RadicateInvoiceCId 
LEFT JOIN CTE_ALTA_MEDICA AS ALT with (nolock) ON ALT.NUMINGRES =IMG.INGRESO
LEFT JOIN CTE_CAMAS AS CAM with (nolock) ON  CAM.INGRESO =IMG.INGRESO
UNION ALL
SELECT img.*,ing.IESTADOIN 'ESTADO INGRESO' ,f.InvoiceNumber 'NRO FACTURA' ,C.RadicatedConsecutive 'NRO RADICADO',ALT.[FECHA ALTA]  ,CAM.CAMA
FROM CTE_PATOLOGIAS as img
inner join dbo.ADINGRESO as ing with (nolock) ON img.INGRESO =ing.NUMINGRES 
left join Billing .Invoice as f with (nolock) ON f.AdmissionNumber =ing.NUMINGRES and f.Status =1
LEFT JOIN Portfolio.RadicateInvoiceD AS RAD with (nolock) ON RAD.InvoiceNumber =F.InvoiceNumber 
LEFT JOIN Portfolio .RadicateInvoiceC AS C with (nolock) ON C.Id =RAD.RadicateInvoiceCId 
LEFT JOIN CTE_ALTA_MEDICA AS ALT with (nolock) ON ALT.NUMINGRES =IMG.INGRESO
LEFT JOIN CTE_CAMAS AS CAM with (nolock) ON  CAM.INGRESO =IMG.INGRESO
)

SELECT 
 CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY, 
 *,
 CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM CTE_PARACLINICOS
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte que consolida, para consumo de reporting, las órdenes paraclínicas hospitalarias (imágenes diagnósticas, laboratorios y patologías) que aún no tienen orden de servicio generada (`GENSERVICEORDER IS NULL`) y pertenecen al ámbito hospitalario (`MANEXTPRO = 0`). Identifica primero los ingresos con pendientes mediante un CTE base y luego aplana, por tipo de examen, la información del paciente, profesional ordenante, unidad funcional, código CUPS, estado del estudio, médico lector, fecha de lectura y descripción contractual asociada. Incluye un ramal adicional para órdenes vinculadas a ingresos de recién nacidos.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPendientesDeOrdenesParaclinicos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPendientesDeOrdenesParaclinicos';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un único listado las órdenes paraclínicas (imágenes, laboratorios y patologías) que están pendientes de generar orden de servicio en ingresos hospitalarios, con datos del paciente, médico, ingreso, alta, cama, factura y radicado, para su seguimiento operativo y administrativo.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPendientesDeOrdenesParaclinicos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El ingreso debe tener al menos una orden de imagen, laboratorio o patología con GENSERVICEORDER nulo, MANEXTPRO=0 (ámbito hospitalario) y estado en los rangos definidos (ESTSERIPS 2/3/4 para imágenes, 3/4 para laboratorios y patologías).; Las órdenes deben estar asociadas a maestros válidos: paciente (INPACIENT), CUPS (INCUPSIPS), profesional de salud (INPROFSAL) y unidad funcional (INUNIFUNC).; Para incluir órdenes asociadas a recién nacidos, debe existir el vínculo madre-hijo en HCINGRESORECNAC y registro en HCRECINAC.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPendientesDeOrdenesParaclinicos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Sólo se reportan órdenes pendientes de generar orden de servicio (GENSERVICEORDER IS NULL).; Sólo se incluyen órdenes de ámbito hospitalario (MANEXTPRO = 0); se excluyen ambulatorias.; Para laboratorios la ''LECTURA REALIZADA'' siempre se reporta como ''SI'' (constante).; El estado del ingreso, número de factura y radicado se vinculan vía LEFT JOIN sólo si existe factura activa (Invoice.Status = 1).; La cama asignada corresponde al último registro de estancia del ingreso (MAX(ID) en CHREGESTA) y sólo si REGESTADO = 1.; La fecha de alta corresponde al máximo FECALTPAC en HCREGEGRE para el ingreso.; Para laboratorios, la lectura se toma del primer registro de muestra (INTERCTRL.NUMUESTRA = 1, MIN(AUTO)).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPendientesDeOrdenesParaclinicos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Órdenes paraclínicas pendientes; Imágenes diagnósticas; Laboratorio clínico; Patología; Orden de servicio; Ingreso hospitalario; Recién nacido (vínculo madre-hijo); Estancia y cama hospitalaria; Alta médica (egreso); Factura y radicación ante pagador; CUPS; Unidad funcional; Centro de atención; Interpretación/lectura de estudios', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPendientesDeOrdenesParaclinicos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewPendientesDeOrdenesParaclinicos: Devuelve un conjunto unificado por UNION ALL de imágenes, laboratorios y patologías filtrando GENSERVICEORDER IS NULL y MANEXTPRO=0, agregando ID_COMPANY=DB_NAME() y ULT_ACTUAL=GETDATE() convertido a zona horaria ''Pakistan Standard Time''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPendientesDeOrdenesParaclinicos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si A.GENSERVICEORDER IS NULL → Marca la fila como ''SIN ORDEN''; en caso contrario ''CON ORDEN'' (aunque el WHERE sólo permite SIN ORDEN). else CON ORDEN; si A.MANEXTPRO = 0 → Clasifica el ámbito como ''HOSPITALARIO''. else AMBULATORIO (no incluido por el WHERE MANEXTPRO=0); si Imágenes: SERREASIT=''1'' o ESTSERIPS IN (''2'',''3'',''4'') → Estado = ''Estudios Realizados''. else ESTSERIPS=''6'' → ''Anulado''; otros → ''Estudios No Realizados''.; si Laboratorios/Patologías: ESTSERIPS IN (''3'',''4'') → Estado = ''Estudios Realizados''. else ''6'' → ''Anulados''; ''1'' → ''Solicitados''; otros → ''No Realizados''.; si Imágenes: A.MEDREALEC IS NULL → LECTURA REALIZADA = ''NO''. else ''SI''; si Patologías: A.PROFRESULTADO IS NULL → Médico lectura = CODPROSAL y fecha lectura = FECORDMED. else Médico = PROFRESULTADO y fecha = FECHARESULT.; si Laboratorios: PROFRESULT no vacío → ese profesional; si no, INTERCTRL.CODPROSAL; si no, HCHISPACA.CODPROSAL → Selecciona médico de lectura según el primer origen disponible. else NULL; si Existe ingreso recién nacido (HCINGRESORECNAC + HCRECINAC) → Incluye también órdenes asociadas al ingreso del hijo en la rama UNION ALL específica.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPendientesDeOrdenesParaclinicos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDIMAG; dbo.HCORDLABO; dbo.HCORDPATO; dbo.ADINGRESO; dbo.INPACIENT; dbo.INCUPSIPS; dbo.INPROFSAL; dbo.INUNIFUNC; Contract.CUPSEntityContractDescriptions; Contract.ContractDescriptions; dbo.HCINGRESORECNAC; dbo.HCRECINAC; dbo.INTERCTRL; dbo.HCHISPACA; dbo.HCREGEGRE; dbo.CHREGESTA; dbo.CHCAMASHO; dbo.CHTIPESTA; dbo.ADCENATEN; Billing.Invoice; Portfolio.RadicateInvoiceD; Portfolio.RadicateInvoiceC', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPendientesDeOrdenesParaclinicos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPendientesDeOrdenesParaclinicos';
GO
