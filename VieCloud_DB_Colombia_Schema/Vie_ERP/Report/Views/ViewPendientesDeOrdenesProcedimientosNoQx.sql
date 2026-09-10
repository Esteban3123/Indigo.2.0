CREATE VIEW [Report].[ViewPendientesDeOrdenesProcedimientosNoQx]
as
WITH CTE_PROCEDIMIENTOS_NO_QX
AS
(
SELECT	CONCAT('HCORDPRON', '-', A.AUTO) Id,'HCORDPRON' EntityName,	A.[AUTO] as Row,A.CODSERIPS,IIF(A.GENSERVICEORDER IS NULL, 0, 1) AS Seleccione,	CONCAT(RTRIM(D.UFUCODIGO), ' - ', RTRIM(D.UFUDESCRI)) AS UnidadFuncional,
			CONCAT(RTRIM(C.CODIGONIT), ' - ', RTRIM(C.NOMMEDICO)) AS Medico,A.OBSSERIPS AS Observacion,	A.FECORDMED AS Fecha,A.NUMEFOLIO AS Folio,A.CODPROSAL,CODESPEC1,CANSERIPS,A.NUMINGRES,
			A.IPCODPACI,D.UFUCODIGO,C.CODIGONIT as NitMedico,A.GENSERVICEORDER,
			case when A.ESTSERIPS = '1' then 'No Realizados' when A.ESTSERIPS = '5' then 'Anulados' else 'Realizados' end as Tipo,
			COALESCE((SELECT TOP 1 CODPROSAL FROM dbo.HCINFPROM where IPCODPACI = A.IPCODPACI AND CODSERIPS = A.CODSERIPS AND NUMINGRES = A.NUMINGRES ORDER BY FECREAPRO ASC), A.CODPROSAL) as MedicoRealizo,
			COALESCE((SELECT TOP 1 FECREAPRO FROM dbo.HCINFPROM where IPCODPACI = A.IPCODPACI AND CODSERIPS = A.CODSERIPS AND NUMINGRES = A.NUMINGRES ORDER BY FECREAPRO ASC), A.FECORDMED) as FechaRealizacion,
			CASE WHEN (SELECT TOP 1 CODPROSAL FROM dbo.HCINFPROM where IPCODPACI = A.IPCODPACI AND CODSERIPS = A.CODSERIPS AND NUMINGRES = A.NUMINGRES ORDER BY FECREAPRO ASC) IS NULL THEN 0 ELSE 1 END as Realizo,
			RTRIM(B.DESSERIPS) AS DESSERIPS,ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId,ISNULL(cd.Id, 0) ContractDescriptionId,ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName
	FROM dbo.ADINGRESO ing
	JOIN dbo.HCORDPRON A ON ing.NUMINGRES = a.NUMINGRES
	JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
	JOIN dbo.INPROFSAL C ON A.CODPROSAL=C.CODPROSAL 
	JOIN dbo.INUNIFUNC D ON A.UFUCODIGO=D.UFUCODIGO
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd on cecd.Id = a.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
	WHERE A.MANEXTPRO = 0 OR ISNULL(ing.TRATAESPECIA, 0) = 3 AND A.GENSERVICEORDER IS NULL
UNION ALL
	SELECT CONCAT('HCORDPRON', '-', A.AUTO) Id,	'HCORDPRON' EntityName,	A.[AUTO] as Row,A.CODSERIPS, IIF(A.GENSERVICEORDER IS NULL, 0, 1) AS Seleccione,
			CONCAT(RTRIM(D.UFUCODIGO), ' - ', RTRIM(D.UFUDESCRI)) AS UnidadFuncional,CONCAT(RTRIM(C.CODIGONIT), ' - ', RTRIM(C.NOMMEDICO)) AS Medico,
			A.OBSSERIPS AS Observacion,	A.FECORDMED AS Fecha,A.NUMEFOLIO AS Folio,A.CODPROSAL,CODESPEC1,CANSERIPS,INGMH.NUMINGRES,a.IPCODPACI,D.UFUCODIGO,
			C.CODIGONIT as NitMedico,A.GENSERVICEORDER, 
			case when A.ESTSERIPS = '1' then 'No Realizados' when A.ESTSERIPS = '5' then 'Anulados' else 'Realizados' end as Tipo,
			COALESCE((SELECT TOP 1 CODPROSAL FROM dbo.HCINFPROM where IPCODPACI = A.IPCODPACI AND CODSERIPS = A.CODSERIPS AND NUMINGRES = A.NUMINGRES ORDER BY FECREAPRO ASC), A.CODPROSAL) as MedicoRealizo,
			COALESCE((SELECT TOP 1 FECREAPRO FROM dbo.HCINFPROM where IPCODPACI = A.IPCODPACI AND CODSERIPS = A.CODSERIPS AND NUMINGRES = A.NUMINGRES ORDER BY FECREAPRO ASC), A.FECORDMED) as FechaRealizacion,
			CASE WHEN (SELECT TOP 1 CODPROSAL FROM dbo.HCINFPROM where IPCODPACI = A.IPCODPACI AND CODSERIPS = A.CODSERIPS AND NUMINGRES = A.NUMINGRES ORDER BY FECREAPRO ASC) IS NULL THEN 0 ELSE 1 END as Realizo,
			RTRIM(B.DESSERIPS) AS DESSERIPS,ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId,	ISNULL(cd.Id, 0) ContractDescriptionId,ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName
	FROM dbo.HCORDPRON A 
	JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
	JOIN dbo.INPROFSAL C ON A.CODPROSAL=C.CODPROSAL 
	JOIN dbo.INUNIFUNC D ON A.UFUCODIGO=D.UFUCODIGO 
	JOIN dbo.HCINGRESORECNAC INGMH  on a.NUMINGRES = INGMH .NUMINGRESHIJO
	JOIN dbo.HCRECINAC RN  on INGMH.NUMINGRESHIJO  = rn.NUMINGRESHIJO  
	JOIN dbo.ADINGRESO AS ING  on ING.NUMINGRES = INGMH.NUMINGRESHIJO  
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd on cecd.Id = a.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
	WHERE A.MANEXTPRO = 0 OR ISNULL(ing.TRATAESPECIA, 0) = 3 AND A.GENSERVICEORDER IS NULL
),

CTE_ALTA_MEDICA
AS
(
  SELECT EGR.NUMINGRES ,EGR.IPCODPACI ,MAX(FECALTPAC) 'FECHA ALTA' FROM DBO.HCREGEGRE EGR with (nolock) 
  INNER JOIN CTE_PROCEDIMIENTOS_NO_QX AS PEN with (nolock) ON PEN.NUMINGRES =EGR.NUMINGRES 
  GROUP BY EGR.NUMINGRES ,EGR.IPCODPACI
),

CTE_ESTANCIA_MAYOR
AS
(
  SELECT C.NUMINGRES ,C.IPCODPACI,C.CODICAMAS  ,C.FECINIEST 'FECHA ESTANCIA',C.REGESTADO,C.CODTIPEST,C.ID    FROM dbo.CHREGESTA C with (nolock)
  INNER JOIN CTE_PROCEDIMIENTOS_NO_QX AS PEN ON PEN.NUMINGRES =C.NUMINGRES 
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
  INNER JOIN CTE_PROCEDIMIENTOS_NO_QX AS PEN with (nolock) ON PEN.NUMINGRES =HIS.NUMINGRES 
  WHERE HIS.GENCONEXT =1
  GROUP BY HIS.NUMINGRES ,HIS.IPCODPACI
)

/*Vista procedimiento no qx;  seleccione(1- con orden de servicio; 0- sin orden de servicio) tipo Realizados */
SELECT 'PROCEDIMIENTOS NO QX' EntityName ,'SIN ORDENES' 'ORDENE DE SERVICIO',noqx.NUMINGRES 'INGRESO',noqx.IPCODPACI 'IDENTIFICACION',PAC.IPNOMCOMP 'PACIENTE',
noqx.UnidadFuncional 'UNIDAD FUNCIONAL',noqx.Medico 'PROFESIONAL',noqx.FechaRealizacion 'FECHA REALIZACION', noqx.CODSERIPS 'CUPS PROCEDIMIENTO',
noqx.DESSERIPS 'PROCEDIMIENTO',noqx.CANSERIPS 'CANTIDAD' ,ing.IESTADOIN 'ESTADO' ,f.InvoiceNumber 'NRO FACTURA' ,C.RadicatedConsecutive 'NRO RADICADO',alt.[FECHA ALTA]
from CTE_PROCEDIMIENTOS_NO_QX noqx 
INNER JOIN DBO.INPACIENT AS PAC ON PAC.IPCODPACI =noqx.IPCODPACI
inner join dbo.ADINGRESO as ing on noqx.NUMINGRES  =ing.NUMINGRES 
left join Billing .Invoice as f on f.AdmissionNumber =ing.NUMINGRES and f.Status =1
LEFT JOIN Portfolio.RadicateInvoiceD AS RAD ON RAD.InvoiceNumber =F.InvoiceNumber 
LEFT JOIN Portfolio .RadicateInvoiceC AS C ON C.Id =RAD.RadicateInvoiceCId 
LEFT JOIN CTE_ALTA_MEDICA AS ALT with (nolock) ON ALT.NUMINGRES =noqx.NUMINGRES
where noqx.Seleccione =0 and noqx.Tipo ='Realizados' and  noqx.CODSERIPS not in ('S55201','S55202','S55203','S55204','S55205','S55206','S55207','S55208','S55209','939403','931001',
'937000','937101','937201','937202','937203','937300','937400','938303','938302','933501','893801')-- AND NOQX.NUMINGRES ='322 '
--AND noqx.FechaRealizacion BETWEEN '2022-05-01' AND '2022-05-31' and ing.IESTADOIN ='F'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte que consolida órdenes de procedimientos no quirúrgicos realizados pero sin orden de servicio generada, excluyendo un conjunto de códigos CUPS específicos. Une admisiones, historia clínica, catálogo CUPS, profesionales y unidades funcionales; incluye además recién nacidos vinculando el ingreso del neonato al de la madre. Enriquece cada registro con la cama y estancia actual del paciente, la fecha de alta médica, el estado del ingreso, el número de factura activa y el radicado ante la entidad pagadora, para uso de seguimiento de cartera y facturación.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPendientesDeOrdenesProcedimientosNoQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPendientesDeOrdenesProcedimientosNoQx';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los procedimientos no quirúrgicos realizados que aún no tienen orden de servicio generada, enriqueciendo cada uno con datos del paciente, ingreso, médico, alta, factura y radicado, para seguimiento de cartera y facturación.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPendientesDeOrdenesProcedimientosNoQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las órdenes en HCORDPRON deben estar marcadas como no manejadas externamente (MANEXTPRO=0) o el ingreso debe ser de tratamiento especial tipo 3 (TRATAESPECIA=3) y sin orden de servicio generada (GENSERVICEORDER IS NULL).; Para incluir órdenes de recién nacido, el ingreso del neonato debe estar vinculado al de la madre vía HCINGRESORECNAC y existir el registro de nacimiento en HCRECINAC.; El registro debe corresponder a un procedimiento clasificado como ''Realizados'' (ESTSERIPS distinto de ''1'' y ''5'').', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPendientesDeOrdenesProcedimientosNoQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Excluye un conjunto fijo de códigos CUPS considerados no aplicables al reporte (estancias, consultas y otros: S55201..S55209, 939403, 931001, 937000, 937101..937203, 937300, 937400, 938303, 938302, 933501, 893801).; Solo considera procedimientos efectivamente realizados (Tipo=''Realizados'').; Solo considera órdenes que aún no tienen orden de servicio generada (Seleccione=0).; Une dos universos de órdenes: ingresos directos (ADINGRESO) y órdenes asociadas a ingresos de recién nacidos vinculados a la madre vía HCINGRESORECNAC.; La factura mostrada siempre corresponde al estado activo (Status=1); si no existe factura activa, los campos de factura/radicado quedan nulos.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPendientesDeOrdenesProcedimientosNoQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Procedimientos no quirúrgicos; Órdenes médicas; Orden de servicio; Historia clínica; Recién nacido vinculado a la madre; Estancia hospitalaria; Cama hospitalaria; Alta médica; Unidad funcional; Profesional de la salud; CUPS; Contrato (descripciones de contrato); Factura; Radicación de factura ante entidad pagadora; Cartera; Tratamiento especial', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPendientesDeOrdenesProcedimientosNoQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewPendientesDeOrdenesProcedimientosNoQx: Devuelve únicamente filas con Seleccione=0 (sin orden de servicio: GENSERVICEORDER IS NULL), Tipo=''Realizados'' y CODSERIPS NO incluido en la lista de exclusión (''S55201''..''S55209'',''939403'',''931001'',''937000'',''937101'',''937201'',''937202'',''937203'',''937300'',''937400'',''938303'',''938302'',''933501'',''893801'').; [RETURN_RESULT] Report.ViewPendientesDeOrdenesProcedimientosNoQx: Clasifica el procedimiento como ''No Realizados'' si ESTSERIPS=''1'', ''Anulados'' si ESTSERIPS=''5'' y ''Realizados'' en cualquier otro caso.; [RETURN_RESULT] Report.ViewPendientesDeOrdenesProcedimientosNoQx: MedicoRealizo y FechaRealizacion se toman del primer registro de HCINFPROM (ordenado por FECREAPRO ascendente) que coincida en IPCODPACI, CODSERIPS y NUMINGRES; si no existe, se usan CODPROSAL y FECORDMED de la orden.; [RETURN_RESULT] Report.ViewPendientesDeOrdenesProcedimientosNoQx: El indicador Realizo=1 cuando existe un registro asociado en HCINFPROM, y 0 en caso contrario.; [RETURN_RESULT] Report.ViewPendientesDeOrdenesProcedimientosNoQx: La factura se asocia mediante Billing.Invoice por AdmissionNumber=NUMINGRES filtrando solo facturas activas (Status=1).; [RETURN_RESULT] Report.ViewPendientesDeOrdenesProcedimientosNoQx: La FECHA ALTA se calcula como el MAX(FECALTPAC) en HCREGEGRE para el ingreso del paciente.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPendientesDeOrdenesProcedimientosNoQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ESTSERIPS = ''1'' → Tipo = ''No Realizados'' else Si ESTSERIPS=''5'' Tipo=''Anulados'', en otro caso Tipo=''Realizados''; si GENSERVICEORDER IS NULL → Seleccione=0 (sin orden de servicio, candidato del reporte) else Seleccione=1 (con orden de servicio, queda excluido por el filtro final Seleccione=0); si Existe TOP 1 en HCINFPROM para (IPCODPACI, CODSERIPS, NUMINGRES) → Realizo=1 y se toman MedicoRealizo y FechaRealizacion del registro más antiguo por FECREAPRO else Realizo=0 y se usan CODPROSAL y FECORDMED de la orden original; si A.MANEXTPRO = 0 OR ISNULL(ing.TRATAESPECIA,0) = 3 AND A.GENSERVICEORDER IS NULL → La orden se incluye en el CTE de procedimientos no Qx else Se descarta', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPendientesDeOrdenesProcedimientosNoQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDPRON; dbo.ADINGRESO; dbo.INCUPSIPS; dbo.INPROFSAL; dbo.INUNIFUNC; dbo.HCINFPROM; dbo.HCINGRESORECNAC; dbo.HCRECINAC; Contract.CUPSEntityContractDescriptions; Contract.ContractDescriptions; dbo.HCREGEGRE; dbo.CHREGESTA; dbo.CHCAMASHO; dbo.CHTIPESTA; dbo.ADCENATEN; dbo.HCHISPACA; dbo.INPACIENT; Billing.Invoice; Portfolio.RadicateInvoiceD; Portfolio.RadicateInvoiceC', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPendientesDeOrdenesProcedimientosNoQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewPendientesDeOrdenesProcedimientosNoQx';
GO
