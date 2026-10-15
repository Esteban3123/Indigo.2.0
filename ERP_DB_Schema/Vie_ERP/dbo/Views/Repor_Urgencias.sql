CREATE VIEW [dbo].[Repor_Urgencias]
AS
SELECT        TOP (100) 
                         PERCENT CASE WHEN U.IPTIPODOC = '1' THEN 'CC' WHEN U.IPTIPODOC = '2' THEN 'CE' WHEN U.IPTIPODOC = '3' THEN 'TI' WHEN U.IPTIPODOC = '4' THEN 'RC' WHEN U.IPTIPODOC = '5' THEN 'PA' WHEN U.IPTIPODOC = '6'
                          THEN ' AS' WHEN U.IPTIPODOC = '7' THEN 'MS' END AS [Tipo Identificación], C.IPCODPACI AS Identificación, C.IPNOMCOMP AS Paciente, 
                         CASE WHEN U.IPSEXOPAC = '1' THEN 'Masculino' WHEN U.IPSEXOPAC = '2' THEN 'Femenino' END AS Sexo, YEAR(GETDATE()) - YEAR(U.IPFECNACI) AS Edad, U.IPTELEFON AS Telefono, U.IPTELMOVI AS Movil, 
                         CASE C.CODTIPPAC WHEN 1 THEN 'Maternas' WHEN 2 THEN 'Menores de 5 Años' WHEN 3 THEN 'Adultos Mayores' WHEN 4 THEN 'Prepagadas o Particulares' WHEN 5 THEN 'Poblacion General' END AS TipoPaciente, 
                         C.IPFECLLEGA AS [Hora de ingreso a Admisiones], 
                         CASE WHEN C.CONESTADO = '1' THEN 'Sin Atender' WHEN C.CONESTADO = '2' THEN 'Ausente en Clasificacion TRIAGE' WHEN C.CONESTADO = '3' THEN 'Clasificado sin Ingreso' WHEN C.CONESTADO = '4' THEN 'Clasificado con Ingreso'
                          WHEN C.CONESTADO = '5' THEN 'Atendido' WHEN C.CONESTADO = '6' THEN 'Ausente en Atencion Inicial Urgencias' WHEN C.CONESTADO = '7' THEN 'Anulado por error de Parametrizacion' WHEN C.CONESTADO = '8' THEN 'No atendido por Clasificacion sin Autorizacion'
                          END AS Estado, ds.TRIANOMCA AS Causa_de_ingreso, C.OBVAUSENT AS Observacion_Ausencia_Triage, DATEDIFF(minute, C.IPFECLLEGA, A.TRIAFECHA) AS Minutos_Espera_Admisiones_a_Triage, 
                         A.TRIAFECHA AS FechaHoraTriage, A.TRIAGECLA AS ClasificacionTriage, P.NOMMEDICO AS Profesional_Realiza_Triage, ES.DESESPECI AS Especialidad, F.NOMENTIDA AS Entidad_del_Paciente, 
                         CASE e.EntityType WHEN '1' THEN 'EPS Contributivo' WHEN '2' THEN 'EPS Subsidiado' WHEN '3' THEN 'ET Vinculados Municipios' WHEN '4' THEN 'ET Vinculados Departamentos' WHEN '5' THEN 'ARL' WHEN '6' THEN 'Prepagada'
                          WHEN '7' THEN 'IPS' WHEN '8' THEN 'IPS' WHEN '9' THEN 'Regimen Especial' WHEN '10' THEN 'Accidentes Transito' WHEN '11' THEN 'Fosyga' WHEN '12' THEN 'Otros' WHEN '99' THEN 'Particulares' END AS Regimen, 
                         DATEDIFF(minute, A.TRIAFECHA, k.FECHINIHI) AS Minutos_Espera_Triage_A_Consulta, k.FECHINIHI AS [Fecha Hora Inicial consulta], PAIU.NOMMEDICO AS Profesional_que_atiende_consulta, A.NUMINGRES AS Ingreso, 
                         CASE I.IESTADOIN WHEN ' ' THEN 'Abierto' WHEN 'F' THEN 'Facturado' WHEN 'C' THEN 'Cerrado' WHEN 'A' THEN 'Anulado' END AS Estado_Ingreso, DX.NOMDIAGNO AS Diagnostico_Ingreso, 
                         DXA.NOMDIAGNO AS Diagnostico_Egreso, k.FECHFINH AS [Fecha Hora Final consulta], DATEDIFF(minute, k.FECHINIHI, k.FECHFINH) AS Minutos_de_la_Consulta
FROM            dbo.ADCONTURG AS C WITH (nolock) LEFT OUTER JOIN
                         dbo.INPACIENT AS U WITH (nolock) ON U.IPCODPACI = C.IPCODPACI LEFT OUTER JOIN
                         dbo.INENTIDAD AS F WITH (nolock) ON F.CODENTIDA = U.CODENTIDA LEFT OUTER JOIN
                         Contract.CareGroup AS E WITH (nolock) ON E.Id = U.GENCAREGROUP LEFT OUTER JOIN
                         dbo.ADTRIAGEU AS A WITH (nolock) ON A.TRIANUMER = C.CODCONCEC LEFT OUTER JOIN
                         dbo.INPROFSAL AS P WITH (nolock) ON P.CODPROSAL = A.CODPROSAL LEFT OUTER JOIN
                         dbo.INPROFSAL AS Pa WITH (nolock) ON Pa.CODPROSAL = C.PROAUSENT LEFT OUTER JOIN
                         dbo.INESPECIA AS ES WITH (nolock) ON ES.CODESPECI = P.CODESPEC1 LEFT OUTER JOIN
                         dbo.ADCATTRIU AS ds WITH (nolock) ON ds.TRIACATEG = A.TRIACATEG LEFT OUTER JOIN
                         dbo.HCURGING1 AS k WITH (nolock) ON k.IPCODPACI = A.IPCODPACI AND k.NUMINGRES = A.NUMINGRES AND k.UFUCODIGO = C.UFUCODIGO LEFT OUTER JOIN
                         dbo.INPROFSAL AS PAIU WITH (NOLOCK) ON PAIU.CODPROSAL = k.CODPROSAL LEFT OUTER JOIN
                         dbo.ADINGRESO AS I WITH (NOLOCK) ON I.NUMINGRES = A.NUMINGRES LEFT OUTER JOIN
                         dbo.INDIAGNOS AS DX WITH (NOLOCK) ON DX.CODDIAGNO = I.CODDIAING LEFT OUTER JOIN
                         dbo.INDIAGNOS AS DXA WITH (NOLOCK) ON DXA.CODDIAGNO = I.CODDIAEGR
WHERE        (C.IPFECLLEGA >= '01/09/2020 00:00:00')