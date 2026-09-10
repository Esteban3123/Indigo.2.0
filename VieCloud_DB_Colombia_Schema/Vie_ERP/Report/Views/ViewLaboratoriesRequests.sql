

/*******************************************************************************************************************
Nombre: [Report].[ViewLaboratoriesRequests]
Tipo:Vista
Observacion:
Profesional: 
Fecha:
---------------------------------------------------------------------------
Modificaciones
_____________________________________________________________________________
Vercion 1
Persona que modifico: Nilsson Miguel Galindo Lopez
Fecha:02-11-2022
Ovservaciones: Azure DevOps 6279
--------------------------------------
Vercion 2
Persona que modifico: Amira Gil Meneses
Fecha: 22-03-2023
Observaciones: se ingresa el campo fecha de resultado FECHARESULT.
***********************************************************************************************************************************/
CREATE VIEW [Report].[ViewLaboratoriesRequests] AS

--*****LABORATORIOS***---
WITH 
CTE_ORDEN_LABORATORIO AS
(
SELECT 
IPCODPACI,NUMINGRES,NUMEFOLIO,UFUCODIGO,CODCENATE,CODPROSAL,CODSERIPS,GENSERVICEORDER,IDDESCRIPCIONRELACIONADA,
FECORDMED,CANSERIPS,FECRECMUE,ESTSERIPS,MANEXTPRO,TraceabilityPaperworkId,FECHARESULT
FROM DBO.HCORDLABO
--UNION ALL
--SELECT 
--IPCODPACI,NUMINGRES,'' AS NUMEFOLIO,UFUCODIGO,CODCENATE,CODPROSAL,CODSERIPS,''AS GENSERVICEORDER,IDDESCRIPCIONRELACIONADA,
--FECORDMED,CANSERIPS,FECRECMUE,ESTSERIPS,'' AS MANEXTPRO,'' AS TraceabilityPaperworkId
--FROM
--DBO.AMBORDLAB
),
CTE_GRUPO AS
(
SELECT 
CG.CODE AS CODGRUPO, 
CE.CODE AS CUPS, 
CASE PC.CONTRACTED WHEN 1 THEN 'SI' ELSE 'NO' END AS CONTRATADO,
CASE PC.QUOTED WHEN 1 THEN 'SI' ELSE 'NO' END AS COTIZADO, 
CECD.ID AS IDRELACION
FROM 
CONTRACT.CAREGROUP AS CG INNER JOIN 
CONTRACT.PROCEDURETEMPLATE AS PT ON CG.PROCEDURETEMPLATEID = PT.ID INNER JOIN 
CONTRACT.PROCEDURECUPS AS PC ON PT.ID = PC.PROCEDURESTEMPLATEID INNER JOIN 
CONTRACT.CUPSENTITY AS CE  ON PC.CUPSID = CE.ID LEFT JOIN 
CONTRACT.CUPSENTITYCONTRACTDESCRIPTIONS AS CECD ON PC.CUPSENTITYCONTRACTDESCRIPTIONID = CECD.ID
),

CTE_ESTADIO AS
(
SELECT DISTINCT
IPCODPACI,
CASE ESTADIO WHEN 0 THEN 'ESTADIO CLÍNICO (EC) 0 (TUMOR IN SITU)'
			 WHEN 1 THEN 'EC I O 1'
			 WHEN 2 THEN 'EC IA O 1A'
			 WHEN 3 THEN 'EC IA1'
			 WHEN 4 THEN 'EC IA2'
			 WHEN 5 THEN 'EC IB O 1B'
			 WHEN 6 THEN 'EC IB1'
			 WHEN 7 THEN 'EC IB2'
			 WHEN 8 THEN 'EC IC O 1C'
			 WHEN 9 THEN 'EC IS O 1S'
			 WHEN 10 THEN 'EC II O 2'
			 WHEN 11 THEN 'EC IIA O 2A'
			 WHEN 12 THEN 'EC IIA1'
			 WHEN 13 THEN 'EC IIA2'
			 WHEN 14 THEN 'EC IIB O 2B'
			 WHEN 15 THEN 'EC IIC O 2C'
			 WHEN 16 THEN 'EC III O 3'
			 WHEN 17 THEN 'EC IIIA O 3A'
			 WHEN 18 THEN 'EC IIIB O 3B'
			 WHEN 19 THEN 'EC IIIC O 3C'
			 WHEN 20 THEN 'EC IV O 4'
			 WHEN 21 THEN 'EC IVA O 4A'
			 WHEN 22 THEN 'EC IVB O 4B'
			 WHEN 23 THEN 'EC IVC O 4C'
			 WHEN 24 THEN 'EC 4S (PARA NEUROBLASTOMA)'
			 WHEN 25 THEN 'EC  V O 5'
			 WHEN 26 THEN 'EC ESTADIO IAB'
			 WHEN 55 THEN 'PERSONA CON ASEGURAMIENTO (RÉGIMEN SUBSIDIADO O CONTRIBUTIVO Y QUE NO SON PPNA) QUE RECIBIÓ SERVICIOS DE SALUD POR PARTE DEL ENTE TERRITORIALDURANTE EL PERIODO DE REPORTE' END ESTADIO,
NUMINGRES
FROM
INDIAGNOH EST WHERE ESTADIO IS NOT NULL AND ESTADIO NOT IN(93, 98, 99)
									    AND EST.NUMINGRES=(SELECT MIN(FE.NUMINGRES) FROM DBO.INDIAGNOH FE WHERE EST.IPCODPACI=FE.IPCODPACI)
										--AND EST.IPCODPACI='1092947812'
--SELECT * FROM INDIAGNOH  WHERE IPCODPACI='1092947812' AND NUMINGRES='26378'
),
CTE_INGRESO AS
(
SELECT 
ING.NUMINGRES,GA.CODE,
EA.CODE + ' - ' + EA.NAME AS [ENTIDAD ADMINISTRADORA], 
GA.CODE + ' - ' + GA.NAME [GRUPO ATENCION],
GA.LIQUIDATIONTYPE
FROM DBO.ADINGRESO ING INNER JOIN
CONTRACT.CAREGROUP AS GA ON GA.ID = ING.GENCAREGROUP INNER JOIN
CONTRACT.HEALTHADMINISTRATOR AS EA ON EA.ID = ING.GENCONENTITY 
),
CTE_FACTURA AS
(
SELECT DISTINCT
FAC.InvoiceNumber,
ORDD.ServiceOrderId,
ORDD.CUPSEntityId
FROM
Billing.ServiceOrderDetail ORDD INNER JOIN
Billing.InvoiceDetail FACD ON ORDD.ID=FACD.ServiceOrderDetailId AND ORDD.CUPSEntityId IS NOT NULL INNER JOIN
Billing.Invoice FAC ON FACD.InvoiceId=FAC.Id AND FAC.Status!=2
),

CTE_PACIENTE AS (
SELECT 
I.IPCODPACI,/*IDN.SIGLA,*/I.IPNOMCOMP,I.IPTELEFON,I.IPTELMOVI, 
CASE I.IPTIPODOC WHEN '1'  THEN 'CC'
				 WHEN '2'  THEN 'CE'
				 WHEN '3'  THEN 'TI'
				 WHEN '4'  THEN 'RC'
				 WHEN '5'  THEN 'PA'
				 WHEN '6'  THEN 'AS'
				 WHEN '7'  THEN 'MS'
				 WHEN '8'  THEN 'NU'
				 WHEN '9'  THEN 'NV'
				 WHEN '10' THEN 'CD'
				 WHEN '11' THEN 'SC'
				 WHEN '12' THEN 'PE' END AS SIGLA
FROM 
DBO.INPACIENT I 
--INNER JOIN DBO.ADTIPOIDENTIFICA IDN ON I.IPTIPODOC=IDN.CODIGO
),

CTE_CANCELACION AS
(
SELECT
CAN.ID,
CAN.CancellationDate,
USU.NOMUSUARI,
CAN.CancellationReasonsObservations
FROM
[Authorization].TraceabilityPaperwork CAN INNER JOIN
dbo.SEGusuaru USU ON CAN.CancellationUserCode=USU.CODUSUARI
)

SELECT 
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
RTRIM(E.NOMCENATE) AS [CENTRO ATENCION], 
I.SIGLA AS [TIPO IDENTIFICACION], 
A.IPCODPACI AS [IDENTIFICACION], 
I.IPNOMCOMP AS [PACIENTE], 
A.UFUCODIGO + ' - ' + RTRIM(D.UFUDESCRI) AS [UNIDAD FUNCIONAL], 
CG.CODE + '-' + CG.NAME [GRUPO], 
CSG.CODE + '-' + CSG.NAME [SUBGRUPO], 
A.CODSERIPS [CODIGO CUPS], 
RTRIM(CUPS.Description) AS [DESCRIPCION SERVICIO], 
/*ISNULL(CD.CODE + ' - ' + CD.NAME, '')*/ '' [DESCRIPCION RELACIONADA], 
A.FECORDMED AS [FECHA SOLICITUD],
A.FECRECMUE AS [FECHA RECOLECIÓN DE MUESTRA],
A.FECHARESULT AS [FECHA DE RESULTADO],
I.IPTELEFON AS TELEFONO_FIJO, 
I.IPTELMOVI AS TELEFONO_MOVIL, 
A.NUMINGRES AS [INGRESO],
A.NUMEFOLIO AS FOLIO, 
CASE A.ESTSERIPS WHEN 1 THEN 'Solicitado'
				 WHEN 2 THEN 'Muestra Recolectada'
				 WHEN 3 THEN 'Resultado Entregado' 
				 WHEN 4 THEN 'Examen Interpretado' 
				 WHEN 5 THEN 'Remitido'
				 WHEN 6 THEN 'Anulado' 
				 WHEN 7 THEN 'Extramural' 
				 WHEN 8 THEN 'Muestra Recolectada Parcialmente' END AS ESTADO, 
A.CANSERIPS AS CANTIDAD, 
PRO.CODPROSAL [ID PROFESIONAL], 
PRO.NOMMEDICO AS PROFESIONAL, 
ESPMED.DESESPECI ESPECIALIDAD, 
DIAG.CODDIAGNO CIE10, 
DIAG.NOMDIAGNO DIAGNOSTICO,
EST.ESTADIO,
CASE WHEN MANEXTPRO = 0 THEN 'HOSPITALARIO' ELSE 'AMBULATORIO' END AS [TIPO SOLICITUD], 
ING.[ENTIDAD ADMINISTRADORA], 
ING.[GRUPO ATENCION],
CASE ING.LIQUIDATIONTYPE WHEN 1 THEN 'PAGO POR SERVICIOS'
						WHEN 2 THEN 'PGP'
						WHEN 3 THEN 'FACTURA GLOBAL'
						WHEN 4 THEN 'CAPITACION GLOBAL'
						WHEN 5 THEN 'CONTROL' END [TIPO CONTRATO], 
'LABORATORIOS' [TIPO ORDEN], 
IIF(G.CODGRUPO IS NULL, 'NO', 'SI') CUBIERTO, 
ISNULL(G.CONTRATADO, 'NO') CONTRATADO, 
ISNULL(G.COTIZADO, 'NO') COTIZADO, 
ING.CODE [CODIGO GRUPO], 
A.IDDESCRIPCIONRELACIONADA, 
FAC.InvoiceNumber AS FACTURA,
CAST(CAN.CancellationDate AS DATE) AS [FECHA ANULACION],
CAST(CAN.CancellationDate as time) AS [HORA ANULACION],
CAN.NOMUSUARI AS [USUARIO ANULACION],
CAN.CancellationReasonsObservations AS [OBSERVACION DE LA CANCELACIÓN],
CAST(A.FECORDMED AS DATE) [FECHA BUSQUEDA],
YEAR(A.FECORDMED) AS 'AÑO FECHA BUSQUEDA',
MONTH(A.FECORDMED) AS 'MES AÑO FECHA BUSQUEDA', 
CASE MONTH(A.FECORDMED) WHEN 1 THEN 'ENERO'
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
							 WHEN 12 THEN 'DICIEMBRE' END AS 'MES NOMBRE FECHA BUSQUEDA',
FORMAT(DAY(A.FECORDMED), '00') AS 'DIA FECHA BUSQUEDA',
CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM 
CTE_ORDEN_LABORATORIO A INNER JOIN 
CTE_INGRESO ING ON A.NUMINGRES=ING.NUMINGRES INNER JOIN
CTE_PACIENTE I ON A.IPCODPACI = I.IPCODPACI INNER JOIN 
DBO.INUNIFUNC D WITH(NOLOCK) ON A.UFUCODIGO = D.UFUCODIGO INNER JOIN
DBO.ADCENATEN E ON A.CODCENATE = E.CODCENATE INNER JOIN
DBO.INPROFSAL PRO WITH(NOLOCK) ON A.CODPROSAL = PRO.CODPROSAL INNER JOIN 
CONTRACT.CUPSENTITY AS CUPS ON CUPS.CODE = A.CODSERIPS INNER JOIN 
CONTRACT.CUPSSUBGROUP AS CSG WITH(NOLOCK) ON CSG.ID = CUPS.CUPSSUBGROUPID INNER JOIN 
CONTRACT.CUPSGROUP AS CG WITH(NOLOCK) ON CG.ID = CSG.CUPSGROUPID INNER JOIN 
DBO.HCHISPACA HIS WITH(NOLOCK) ON A.NUMINGRES = HIS.NUMINGRES AND A.NUMEFOLIO = HIS.NUMEFOLIO INNER JOIN
DBO.INESPECIA AS ESPMED WITH(NOLOCK) ON ESPMED.CODESPECI = HIS.CODESPTRA LEFT JOIN
DBO.INDIAGNOS AS DIAG WITH(NOLOCK) ON DIAG.CODDIAGNO = HIS.CODDIAGNO LEFT JOIN
CTE_FACTURA FAC ON A.GENSERVICEORDER=FAC.ServiceOrderId AND CUPS.Id=FAC.CUPSEntityId LEFT JOIN
CTE_GRUPO AS G ON G.CODGRUPO = ING.CODE AND A.CODSERIPS=G.CUPS
									    AND A.IDDESCRIPCIONRELACIONADA = G.IDRELACION LEFT JOIN
CTE_ESTADIO EST ON A.IPCODPACI=EST.IPCODPACI LEFT JOIN
CTE_CANCELACION CAN ON A.TraceabilityPaperworkId=CAN.Id

--WHERE A.IPCODPACI='1092947812' AND A.CODSERIPS='903856' AND A.NUMINGRES='26378'--AND A.GENSERVICEORDER='640139'
--completo 1:12
--CTE PACIENTES 1:05
--SE AGREGO CTE_CANCELACION

--SELECT * FROM DBO.HCORDLABO WHERE IPCODPACI='1092947812' AND CODSERIPS='903856' AND NUMINGRES='26378' --AND GENSERVICEORDER='640139'
----SELECT * FROM Billing.ServiceOrder WHERE ID='640139'
----SELECT * FROM CONTRACT.CUPSENTITY WHERE Code='902210'
--SELECT * FROM Billing.ServiceOrderDetail WHERE ServiceOrderId='640139' AND CUPSEntityId='7611'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte que consolida todas las órdenes de laboratorio clínico registradas en `HCORDLABO`, aplanando en una sola fila por orden los datos del paciente, unidad funcional, centro de atención, profesional solicitante, diagnóstico, estadio oncológico, estado del servicio (Solicitado → Resultado Entregado → Anulado), cobertura contractual CUPS, número de factura asociada y datos de cancelación. Está orientada a consumo por herramientas de reporting/BI, incluyendo descomposición temporal (año, mes, día) de la fecha de solicitud.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewLaboratoriesRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewLaboratoriesRequests';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un único reporte las solicitudes de laboratorio de pacientes, enriquecidas con datos clínicos, administrativos, contractuales (CUPS/grupos), de facturación, de cancelación de autorización y estadio clínico, para análisis y reportería.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewLaboratoriesRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La orden de laboratorio debe existir en HCORDLABO con paciente, ingreso, unidad funcional, centro de atención, profesional y CUPS válidos (todos los joins son INNER).; El CUPS de la orden (CODSERIPS) debe existir en CONTRACT.CUPSENTITY y estar asociado a un CUPSSUBGROUP y CUPSGROUP.; Debe existir historia clínica (HCHISPACA) con la combinación NUMINGRES+NUMEFOLIO de la orden y una especialidad tratante en INESPECIA.; El ingreso (ADINGRESO) debe tener grupo de atención (CAREGROUP) y entidad administradora (HEALTHADMINISTRATOR) registrados.; El tipo de identificación del paciente debe corresponder a uno de los códigos mapeados (''1'' a ''12''); de lo contrario SIGLA queda NULL.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewLaboratoriesRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El ID_COMPANY siempre corresponde al nombre de la base de datos actual (DB_NAME()), truncado a 9 caracteres.; TIPO ORDEN siempre se etiqueta como ''LABORATORIOS''.; ULT_ACTUAL se calcula con la hora actual convertida a ''Pakistan Standard Time''.; El estadio clínico reportado siempre corresponde al MIN(NUMINGRES) del paciente en INDIAGNOH (primer ingreso con diagnóstico).; Solo se consideran facturas con Status distinto de 2.; Los códigos de estadio 93, 98 y 99 nunca se incluyen.; La descripción relacionada siempre se devuelve como cadena vacía (campo neutralizado en la versión actual).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewLaboratoriesRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de laboratorio; Paciente; Ingreso hospitalario; Centro de atención; Unidad funcional; Profesional de salud / especialidad; Diagnóstico CIE-10; Estadio clínico oncológico; CUPS / grupo / subgrupo de procedimientos; Contrato (contratado/cotizado/cubierto); Entidad administradora (EPS); Grupo de atención y tipo de liquidación (PGP, capitación, etc.); Factura de venta; Trámite de autorización y cancelación; Tipo de solicitud hospitalaria/ambulatoria; Folio de historia clínica', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewLaboratoriesRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewLaboratoriesRequests: Devuelve un row por orden de laboratorio (HCORDLABO) cruzada con su ingreso, paciente, profesional, CUPS, grupo/subgrupo, factura (si existe), cancelación de trámite (si existe) y estadio clínico del primer ingreso del paciente.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewLaboratoriesRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ESTSERIPS de la orden → Se traduce a estado textual: 1=Solicitado, 2=Muestra Recolectada, 3=Resultado Entregado, 4=Examen Interpretado, 5=Remitido, 6=Anulado, 7=Extramural, 8=Muestra Recolectada Parcialmente.; si MANEXTPRO = 0 → TIPO SOLICITUD = ''HOSPITALARIO'' else TIPO SOLICITUD = ''AMBULATORIO''; si LIQUIDATIONTYPE del grupo de atención del ingreso → Se traduce a TIPO CONTRATO: 1=PAGO POR SERVICIOS, 2=PGP, 3=FACTURA GLOBAL, 4=CAPITACION GLOBAL, 5=CONTROL.; si Existe coincidencia en CTE_GRUPO por (CODGRUPO=ING.CODE, CUPS=CODSERIPS, IDRELACION=IDDESCRIPCIONRELACIONADA) → CUBIERTO=''SI'' y se toman los flags CONTRATADO/COTIZADO del contrato else CUBIERTO=''NO'' y CONTRATADO/COTIZADO se devuelven ''NO'' por ISNULL; si PC.CONTRACTED = 1 / PC.QUOTED = 1 → Se marca CONTRATADO=''SI'' / COTIZADO=''SI'' else ''NO''; si ESTADIO en INDIAGNOH IS NOT NULL y NOT IN (93,98,99) y NUMINGRES = MIN(NUMINGRES) del paciente → Se incluye el estadio clínico oncológico del primer ingreso del paciente, traducido a su descripción textual (EC 0..IV, 4S, IAB, código 55 aseguramiento).; si FAC.Status != 2 en Billing.Invoice → Solo se asocia número de factura cuando la factura no está en estado 2 (excluye facturas anuladas/canceladas).; si Existe TraceabilityPaperworkId asociado a la orden → Se enriquece con fecha, hora, usuario y observación de cancelación del trámite de autorización.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewLaboratoriesRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'DBO.HCORDLABO; CONTRACT.CAREGROUP; CONTRACT.PROCEDURETEMPLATE; CONTRACT.PROCEDURECUPS; CONTRACT.CUPSENTITY; CONTRACT.CUPSENTITYCONTRACTDESCRIPTIONS; DBO.INDIAGNOH; DBO.ADINGRESO; CONTRACT.HEALTHADMINISTRATOR; Billing.ServiceOrderDetail; Billing.InvoiceDetail; Billing.Invoice; DBO.INPACIENT; Authorization.TraceabilityPaperwork; dbo.SEGusuaru; DBO.INUNIFUNC; DBO.ADCENATEN; DBO.INPROFSAL; CONTRACT.CUPSSUBGROUP; CONTRACT.CUPSGROUP; DBO.HCHISPACA; DBO.INESPECIA; DBO.INDIAGNOS', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewLaboratoriesRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewLaboratoriesRequests';
GO
