

/*******************************************************************************************************************
Nombre: [Report].[ViewOpportunityMedicalAppointmentCE]
Tipo:Vista
Observacion:Oportunidad de consulta externa.
Profesional:
Fecha:
---------------------------------------------------------------------------
Modificaciones
_____________________________________________________________________________
Version 2
Persona que modifico: Nilsson Miguel Galindo Lopez
Fecha: 13-09-2022
Ovservaciones:Se modifica la logica, para cuendo una cita este incumplida no se vea información del ingreso y tampoco del diagnostico
--------------------------------------
Version 3
Persona que modifico:Nilsson Miguel Galindo Lopez
Observacion:SE modifica el campo de tipo de cita sugun lo indicado en el ticquet 9045, ya que no esta mostrando el estado pos operatorio.
Fecha:11-04-2023
--------------------------------------
Version 4
Persona que modifico:Nilsson Miguel Galindo Lopez
Observacion:SE AGREGAN LOS CAMPOS ID DE AGENDAMIENTO Y ID DE PACIENTES DANDO RESPUESTA AL TICKET 9294 DE HOMI
Fecha:24-04-2023
--------------------------------------
Version 5
Persona que modifico: NELLY PATRICIA MORALES CAPERA
Observacion:Se agrega el campo de correo DANDO RESPUESTA AL TICKET 9294 DE HOMI
Fecha:24-04-2023
--------------------------------------
Version 6
Persona que modifico: NILSSON MIGUEL GALINDO LOPEZ
Observacion:Se agrega campo de consultorio de agendamiento segun el ticket 9684
Fecha:11-05-2023
----------------------------------------
Version 7
Persona que modifico: AMIRA GIL MENESES
Observacion:Se modifica el INNER JOIN Por LEFT de la tabla INDEPARTA  Bug 9919
Fecha:26-05-2023
----------------------------------------
Version 8
Persona que modifico: AMIRA GIL MENESES
Observacion:Se ingresa codigo del médico. Solicitud del HSJ
Fecha:12-07-2023
----------------------------------------
Version 9
Persona que modifico: AMIRA GIL MENESES
Observacion:Modificación de Campo Régimen el cual se toma de la tabla dbo.INPACIENT
Fecha:31-07-2023
----------------------------------------
Version 10
Persona que modifico:Nilsson Galindo
Observacion:Se cambia la logica del cte CTE_CONTROL_CE
Fecha:12-09-2023
-------------------------------------------------------------------
Version 11
Persona que modifico:Nilsson Galindo
Observacion:Se cambia la logica de la tabla de ingresos y de diagnosticos 
Fecha:26-10-2023
-------------------------------------------------------------------
Version 12
Persona que modifico:Nilsson Galindo
Observacion:Se cambia la logica del estado de la cita, para que muestre como inculpido el estado antes de que se corra el JOB
Fecha:31-10-2023
--***********************************************************************************************************************************/

CREATE VIEW [Report].[ViewOpportunityMedicalAppointmentCE] AS

WITH 
CTE_CONTROL_CE AS
 (
	SELECT 
	NUMCONCIT,NUMINGRES,IDHCHISPACA
	FROM dbo.ADCONCOEX 
	--WHERE CONESTADO IN (3,7)
--FN V10
)

--IN V11 CTE_DIAGNOSTICOS AS 
--(
--SELECT DIA.IPCODPACI, DIA.NUMINGRES, DIA.CODDIAPRI, MAX(DIA.CODDIAGNO) CODDIAGNO ,DG.NOMDIAGNO
--           FROM INDIAGNOP DIA INNER JOIN DBO.INDIAGNOS DG ON DIA.CODDIAGNO=DG.CODDIAGNO
--		   WHERE CODDIAPRI = 'true'  --AND NUMINGRES='144201'
--		   GROUP BY IPCODPACI, CODDIAPRI, NUMINGRES,DG.NOMDIAGNO
--) FN V11

SELECT DISTINCT
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
CASE A.TIPSOLICITU WHEN 3 THEN 'Tratamientos Especiales'
				   WHEN 2 THEN 'Apoyo Diagnostico'
				   WHEN 1 THEN  'Cita Medica'END [TIPO AGENDAMIENTO],
RTRIM(C.CODIPSSEC) AS [CODIGO IPS],
RTRIM(C.NOMCENATE) AS [CENTRO ATENCION], 
RTRIM(ISNULL(UNICE.UFUDESCRI,UNIID.UFUDESCRI)) AS [UNIDAD FUNCIONAL], 
CASE RTRIM(ISNULL(UNICE.UFUTIPUNI, UNIID.UFUTIPUNI))
      WHEN 1 THEN 'URGENCIAS' 
	  WHEN 2 THEN 'HOSPITALIZACION'
	  WHEN 3 THEN 'APOYO DIAGNOSTICO'
	  WHEN 4 THEN 'APOYO TERAPEUTICO'
	  WHEN 5 THEN 'UCI'
	  WHEN 6 THEN 'UCI'
	  WHEN 7 THEN 'UCI'
	  WHEN 8 THEN 'UCI'
	  WHEN 9 THEN 'UCI'
	  WHEN 10 THEN 'UCI'
	  WHEN 11 THEN 'UCI'
	  WHEN 12 THEN 'UNIDAD RENAL'
	  WHEN 13 THEN 'UNIDAD ONCOLOGICA' 
	  WHEN 14 THEN 'UNIDAD MEDICINA NUCLEAR'
	  WHEN 15 THEN 'AMBULATORIO'
	  WHEN 16 THEN 'UNIDAD MENTAL'
	  WHEN 17 THEN 'UNIDAD DE QUEMADOS'
	  WHEN 18 THEN 'UNIDAD DE CUIDADOS PALATIVOS'
	  WHEN 19 THEN 'CIRUGIA'
	  WHEN 20 THEN 'LABORATORIOS'
	  WHEN 21 THEN 'CARDIOLOGIA NO INVASIVA'
	  WHEN 22 THEN 'CARDIOLOGIA INVASIVA'
	  WHEN 23 THEN 'GINECO OBSTETRICIA'
	  WHEN 24 THEN 'CONSULTA EXTERNA GINOCO OBSTETRICIA'
	  WHEN 30 THEN 'OTRAS'
	  WHEN 31 THEN 'CONSULTA PRIORITARIA' ELSE 'CONSULTA EXTERNA' END  [AMBITO],
REPS.[CodigoReps] AS [CODIGO REPS ESPECIALIDAD], 
B.CODESPECI AS [CODIGO ESPECIALIDAD], 
RTRIM(B.DESESPECI) AS ESPECIALIDAD, 
RTRIM(E.CODPROSAL) AS [CODIGO MEDICO], 
RTRIM(E.NOMMEDICO) AS [MEDICO], 
RTRIM(F.DESACTMED) AS 'ACTIVIDAD AGENDAMIENTO',
/*in v6*/CON.DESCRICON AS CONSULTORIO,/*fn v6*/
IIF(F.DURAACTIV <> 0, F.DURAACTIV, (select top(1)FD.DURACSERVI from AGACTMEDD AS FD where a.CODACTMED = FD.CODACTMED AND FD.CODSERIPS = A.CODSERIPS )) + ' ' + 'MINUTOS' 'DURACION ACTIVIDAD',
CASE D.IPTIPODOC WHEN '1'  THEN 'CC'
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
				 WHEN '12' THEN 'PE' 
				 WHEN '13' THEN 'PT'
				 WHEN '14' THEN 'DE'
				 WHEN '15' THEN 'SI'END AS [TIPO IDENTIFICACION],
CASE D.IPTIPODOC WHEN '1'  THEN 'CEDULA DE CIUDADANIA'
				 WHEN '2'  THEN 'CEDULA DE EXTRANJERIA'
				 WHEN '3'  THEN 'TARJETA DE IDENTIDAD'
				 WHEN '4'  THEN 'REGISTRO CIVIL' 
				 WHEN '5'  THEN 'PASAPORTE'
				 WHEN '6'  THEN 'ADULTO SIN IDENTIFICACION'
				 WHEN '7'  THEN 'MENOR SIN IDENTIFICACION'
				 WHEN '8'  THEN 'NUMERO UNICO DE IDENTIFICACIÒN'
				 WHEN '9'  THEN 'CERTIFICADO NACIDO VIVO'
				 WHEN '10' THEN 'CARNET DIPLOMATICO'
				 WHEN '11' THEN 'SALVOCONDUCTO'
				 WHEN '12' THEN 'PERMISO ESPECIAL DE PERMANENCIA' 
				 WHEN '13' THEN 'PERMISO TEMPORAL DE PERMANENCIA'
				 WHEN '14' THEN 'DOCUMENTO EXTRANJERO'
				 WHEN '15' THEN 'SIN IDENTIFICACION'END AS [DESCRIPCION IDENTIFICACION], 
D.IPCODPACI AS [NRO IDENTIFICACION],
D.ID AS [ID PACIENTE],
CAST(D.IPFECNACI AS DATE) AS [FECHA NACIMIENTO], 
DATEDIFF(YEAR, D.IPFECNACI, GETDATE()) AS [EDAD],
CASE D.IPSEXOPAC WHEN '1' THEN 'H' WHEN '2' THEN 'M' END AS [CODIGO SEXO],
CASE D.IPSEXOPAC WHEN '1' THEN 'HOMBRE' WHEN '2' THEN 'MUJER' END AS [SEXO], 
RTRIM(D.IPPRIAPEL) AS [PRIMER APELLIDO], 
RTRIM(D.IPSEGAPEL) AS [SEGUNDO APELLIDO], 
RTRIM(D.IPPRINOMB) AS [PRIMER NOMBRE], 
RTRIM(D.IPSEGNOMB) AS [SEGUNDO NOMBRE], 
UPPER(D.CORELEPAC) AS CORREO,
DEP.depcodigo AS [CODIGO DEPARTAMENTO], 
DEP.nomdepart AS [NOMBRE DEPARTAMENTO RESIDENCIA], 
MUN.MUNCODIGO [CODIGO MUNICIPIO RESIDENCIA], 
MUN.MUNNOMBRE AS [NOMBRE MUNICIPIO RESIDENCIA], 
UPPER(D.IPDIRECCI) AS [DIRECCION], 
D.IPTELMOVI AS CELULAR, 
D.IPTELEFON AS [TELEFONO FIJO],
CASE WHEN A.CODTIPSOL = '0' THEN 'PRESENCIAL' WHEN A.CODTIPSOL = '1' THEN 'TELEFONICA' END AS [FORMA DE SOLICITUD], 
IIF(A.CODTIPCIT IS NOT NULL,CASE WHEN A.CODTIPCIT = '0' THEN 'PRIMERA VEZ' 
								 WHEN A.CODTIPCIT = '1' THEN 'CONTROL' 
								 WHEN A.CODTIPCIT = '2' THEN 'POS OPERATORIO' 
								 WHEN A.CODTIPCIT = '3' THEN 'CITA WEB' ELSE 'N/A' END
						   ,CASE WHEN ING1.TIPCITMED = '1' THEN 'PRIMERA VEZ'
							     WHEN ING1.TIPCITMED = '2' THEN 'CONTROL' ELSE 'N/A' END) AS [TIPO DE CITA],--FN V2 
CASE A.MODALIDAD WHEN 0 THEN 'PRESENCIAL' WHEN 1 THEN 'TELECONSULTA' ELSE 'N/A' END AS [MODALIDAD],
CASE WHEN HEA.Id IS NOT NULL THEN RTRIM(HEA.HealthEntityCode) ELSE RTRIM(ENT.CODENTIDA) END AS [CODIGO ENTIDAD],
CASE WHEN HEA.Id IS NOT NULL THEN RTRIM(HEA.Name) ELSE RTRIM(ENT.NOMENTIDA) END AS [ENTIDAD],
CASE D.IPTIPOPAC WHEN 1  THEN 'Contributivo'
					WHEN 2  THEN 'Subsidiado'
					WHEN 3  THEN 'Vinculado'
					WHEN 4  THEN 'Particular'
					WHEN 5  THEN 'Otro'
					WHEN 6  THEN 'Desplazado Reg. Contributivo'
					WHEN 7  THEN 'Desplazado Reg. Subsidiado'
					WHEN 8  THEN 'Desplazado No Asegurado' 	END AS [REGIMEN], 
CG.Name AS [GRUPO ATENCION], 
A.CODAUTONU AS [ID AGENDA],
CONVERT(VARCHAR, CAST(A.FECITADES AS DATE), 23) AS [FECHA DESEADA DE CITA], 
CONVERT(VARCHAR, CAST(A.FECHAOFERTADA AS DATE), 23) AS [MEJOR CITA DISPONIBLE], 
CONVERT(VARCHAR, CAST(FECHORAIN AS DATE), 23) AS [FECHA ASIGNACION DE CITA], 
CONVERT(VARCHAR, CAST(FECHORAIN AS DATETIME), 20) AS [FECHA HORA ASIGNACION DE CITA], 
CONVERT(VARCHAR, CAST(FECHORAFI AS DATE), 23) AS [FECHA FINAL ASIGNACION DE CITA], 
CONVERT(VARCHAR, CAST(FECHORAFI AS DATETIME), 20) AS [FECHA HORA FINAL ASIGNACION DE CITA], 
CONVERT(VARCHAR, CAST(A.FECREGSIS AS DATE), 23) AS [FECHA SOLICITUD DE CITA], 
IIF(A.CODESTCIT = '4' OR A.CODESTCIT = '5',CONVERT(VARCHAR, CAST(A.FECHCANCELA AS DATETIME), 20),'') AS [FECHA CANCELACION DE CITA], 
CONVERT(VARCHAR, CAST(A.FECREGSIS AS DATETIME), 23) AS [FECHA REGISTRO EN BASE DE DATOS],          
DATEDIFF(DAY, A.FECREGSIS, FECHORAIN) AS [FECHA ASIGNACION vs FECHA SOLICITUD],
DATEDIFF(DAY, A.FECITADES, FECHORAIN) AS [FECHA ASIGNACION vs FECHA DESEADA], 
DATEDIFF(DAY, A.FECREGSIS, A.FECHAOFERTADA) AS [DISPONIBILIDAD DE AGENDA],
CASE WHEN A.CITAEXTRA = 1 THEN 'Si' ELSE 'No' END AS [CITA EXTRA], 
ISNULL(RTRIM(M.CODSERIPS), ISNULL(RTRIM(CE.Code), ISNULL(RTRIM(O.CODSERIPS), RTRIM(IPS3.CODSERIPS)))) AS [CODIGO CUPS], 
ISNULL(RTRIM(M.DESSERIPS), ISNULL(RTRIM(CD.Name), ISNULL(RTRIM(IPS2.DESSERIPS), RTRIM(IPS3.DESSERIPS)))) AS [DESCRIPCION CUPS], 
RTRIM(G.NOMUSUARI) AS [USUARIO QUE REGISTRO LA CITA], 
'ASIGNADA' AS [ESTADO INICIAL],
IIF(ING.NUMINGRES IS NOT NULL OR A.CODESTCIT=0,IIF(A.CODESTCIT=0 AND CAST(A.FECHORAIN AS DATE)=CAST(GETDATE() AS DATE) AND CAST(A.FECHORAIN AS TIME)<=CAST(DATEADD(HOUR,-5,getdate()) AS TIME),'INCUMPLIDA'
																		   ,CASE WHEN A.CODESTCIT='0' THEN 'ASIGNADA'
																				 WHEN A.CODESTCIT='1' AND ING.NUMINGRES IS NOT NULL THEN 'CUMPLIDA'
																				 WHEN A.CODESTCIT='1' AND ING.NUMINGRES IS NULL THEN 'INCUMPLIDA'
																				 WHEN A.CODESTCIT='2' AND ING.NUMINGRES IS NOT NULL THEN 'CUMPLIDA'
																				 WHEN A.CODESTCIT='2' AND ING.NUMINGRES IS NULL THEN 'INCUMPLIDA'
																				 WHEN A.CODESTCIT='3' THEN 'PREASIGNADA'
																				 WHEN A.CODESTCIT='4' AND A.FECHCANCELA IS NOT NULL THEN 'CANCELADA'
																				 WHEN A.CODESTCIT='4' AND ING.NUMINGRES IS NOT NULL THEN 'CUMPLIDA'
																				 WHEN A.CODESTCIT='4' AND ING.NUMINGRES IS NULL THEN 'CANCELADA'
																				 WHEN A.CODESTCIT='5' THEN 'INATENCION' 
																				 WHEN A.CODESTCIT='7' THEN '' END)
						 ,CASE A.CODESTCIT WHEN 1 THEN 'INCUMPLIDA'
										   WHEN 2 THEN 'INCUMPLIDA'
										   WHEN 3 THEN 'PREASIGNADA'
										   WHEN 4 THEN 'CANCELADA' END) AS [ESTADO ACTUAL],A.CODESTCIT,
IIF(ING.NUMINGRES IS NULL AND A.CODESTCIT = '4' OR A.CODESTCIT = '5',ISNULL(A.CODCAUCAN, '00'),'') AS [CODIGO CANCELACION], 
IIF(ING.NUMINGRES IS NULL AND A.CODESTCIT = '4' OR A.CODESTCIT = '5',ISNULL(RTRIM(CAN.DESCAUCAN), ''),'')AS [DESCRIPCION CANCELACION], 
A.NUMAUTORI AS [NUMERO AUTORIZACION],
IIF(DIA.CODDIAGNO IS NULL AND A.CODESTCIT in (2,4),NULL,ISNULL(GC.NUMINGRES, A.NUMINGRES)) AS [INGRESO DE ATENCION],
DIA.CODDIAGNO AS [CODIGO DEL DIAGNOSTICO],
DIA.NOMDIAGNO AS [DIAGNOSTICO],
1 as 'CANTIDAD',
CAST(FECHORAIN AS date) AS 'FECHA BUSQUEDA',
YEAR(FECHORAIN) AS 'AÑO BUSQUEDA',
MONTH(FECHORAIN) AS 'MES BUSQUEDA',
CONCAT(FORMAT(MONTH(FECHORAIN), '00') ,' - ', 
	   CASE MONTH(FECHORAIN) 
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
	    WHEN 12 THEN 'DICIEMBRE' END) 'MES NOMBRE BUSQUEDA',
CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM 
dbo.AGASICITA A 
INNER JOIN dbo.ADCENATEN AS C ON A.CODCENATE = C.CODCENATE
INNER JOIN dbo.INPACIENT AS D ON A.IPCODPACI = D.IPCODPACI
/*AGENDA DE CONSULTORIOS Y UNIDAD FUNCIONAL DE CONSULTA EXTERNA*/
LEFT JOIN dbo.AGCONSULT AS AGC ON A.CODIGOCON=AGC.CODIGOCON AND C.CODCENATE=AGC.CODCENATE
LEFT JOIN dbo.INUNIFUNC AS UNICE ON AGC.UFUCODIGO=UNICE.UFUCODIGO
/*SALAS Y UNIDADES FUNCIONALES DE APOYO DX Y PROCEDIMIENTOS*/
LEFT JOIN dbo.AGENSALAACT AS AGAC ON A.IDSALA = AGAC.CODCONCEC AND A.CODACTMED=AGAC.CODACTMED
LEFT JOIN dbo.AGENSALAC AS AGS ON AGAC.CODCONCEC = AGS.CODCONCEC
LEFT JOIN dbo.INUNIFUNC AS UNIID ON AGS.UFUCODIGO=UNIID.UFUCODIGO

LEFT JOIN INESPECIA AS B ON A.CODESPECI = B.CODESPECI
LEFT JOIN AGACTIMED AS F  ON A.CODACTMED = F.CODACTMED
LEFT JOIN INPROFSAL AS E  ON A.CODPROSAL = E.CODPROSAL 
LEFT JOIN INUBICACI AS UBI  ON D.AUUBICACI = UBI.AUUBICACI
LEFT JOIN INMUNICIP AS MUN  ON UBI.DEPMUNCOD = MUN.DEPMUNCOD
LEFT JOIN INDEPARTA AS DEP  ON DEP.depcodigo = MUN.DEPCODIGO
LEFT JOIN INENTIDAD AS ENT  ON D.CODENTIDA = ENT.CODENTIDA
LEFT JOIN Contract.HealthAdministrator AS HEA  ON A.GENCONENTITY = HEA.Id
LEFT JOIN SEGusuaru AS G ON A.CODUSUASI = G.CODUSUARI
LEFT JOIN INCUPSIPS AS M  ON A.CODSERIPS = M.CODSERIPS
LEFT JOIN Contract.CareGroup AS CG ON A.GENCAREGROUP=CG.Id
LEFT JOIN Contract.CUPSEntityContractDescriptions AS CECD  ON CECD.ID = A.IDDESCRIPCIONRELACIONADA
LEFT JOIN Contract.CUPSEntity AS CE  ON CE.Id = CECD.CUPSEntityId
LEFT JOIN Contract.ContractDescriptions AS CD  ON CD.Id = CECD.ContractDescriptionId
LEFT JOIN AGCAUCANC AS CAN  ON A.CODCAUCAN = CAN.CODCAUCAN
LEFT JOIN CTE_CONTROL_CE AS GC ON A.CODAUTONU=GC.NUMCONCIT
LEFT JOIN RIASCUPS AS O  ON A.IDRIASCUPS = O.ID
LEFT JOIN RIAS AS Q  ON O.IDRIAS = Q.ID
LEFT JOIN INCUPSIPS AS IPS2  ON O.CODSERIPS = IPS2.CODSERIPS
LEFT JOIN INCUPSIPS AS IPS3  ON F.CODSERIPS = IPS3.CODSERIPS
LEFT JOIN ADINGRESO AS ING ON ISNULL(GC.NUMINGRES,A.NUMINGRES)=ING.NUMINGRES
LEFT JOIN HCHISPACA AS HIS ON /*ING.NUMINGRES = HIS.NUMINGRES AND HIS.GENCONEXT = 1 AND */  HIS.TIPHISPAC = 'I' AND GC.IDHCHISPACA = HIS.ID
--LEFT JOIN CTE_DIAGNOSTICOS AS DG ON HIS.NUMINGRES=DG.NUMINGRES
--LEFT JOIN CTE_DIAGNOSTICOS AS DIA ON GC.NUMINGRES=DIA.NUMINGRES
LEFT JOIN INDIAGNOS AS DIA  ON ISNULL(ING.CODDIAING,HIS.CODDIAGNO)=DIA.CODDIAGNO
LEFT JOIN HCURGING1 AS ING1  ON HIS.NUMINGRES = ING1.NUMINGRES AND HIS.NUMEFOLIO = ING1.NUMEFOLIO LEFT JOIN
/*in v6*/dbo.AGCONSULT CON ON A.CODIGOCON=CON.CODIGOCON AND A.CODCENATE=CON.CODCENATE/*fn v6*/
LEFT JOIN Report.TablaEspecialidadesReps AS REPS ON B.CODESPECI = REPS.CodigoEspecialidad
WHERE
A.IPCODPACI not in ('000000000000000','1234567','12345678') --AND A.IPCODPACI IN ('32251893') --and CODAUTONU='119834'
--and cast(a.FECHORAIN as date) between '2023-02-13' and '2023-02-26'
--and a.IPCODPACI='1043124821' 
--and ing.NUMINGRES='B578D10003'

--select * from dbo.AGASICITA where IPCODPACI='1030541206'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting orientada a medir la oportunidad en la asignación de citas de consulta externa. Consolida datos de agendamiento, paciente, médico, especialidad, entidad pagadora, régimen, diagnóstico e ingreso de atención, calculando diferencias de días entre fecha de solicitud, fecha deseada y fecha asignada. Determina el estado final de cada cita (cumplida, incumplida, cancelada, preasignada) cruzando el estado del agendamiento con la existencia de un ingreso real. Está diseñada para consumo en reportes de indicadores de oportunidad y seguimiento de citas ambulatorias.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOpportunityMedicalAppointmentCE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOpportunityMedicalAppointmentCE';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte que consolida la oportunidad y trazabilidad de citas de consulta externa (asignación, cumplimiento, cancelación) con datos demográficos del paciente, especialidad, médico, entidad pagadora, ingreso de atención y diagnóstico asociado.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOpportunityMedicalAppointmentCE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente no debe tener identificadores ficticios: A.IPCODPACI no in (''000000000000000'',''1234567'',''12345678'').; Debe existir cita en AGASICITA con centro de atención (ADCENATEN) y paciente (INPACIENT) válidos (INNER JOIN).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOpportunityMedicalAppointmentCE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'ID_COMPANY se reporta como DB_NAME() truncado a 9 caracteres.; ESTADO INICIAL siempre se reporta como ''ASIGNADA''.; CANTIDAD siempre es 1 (una fila = una cita).; Las fechas en formato corto se entregan como VARCHAR estilo 23 (YYYY-MM-DD); las datetime con estilo 20 (YYYY-MM-DD HH:MI:SS).; ULT_ACTUAL se calcula con la zona horaria ''Pakistan Standard Time''.; Tipos de identificación y sexo se traducen mediante catálogos fijos embebidos (CC, CE, TI, RC, PA, etc.; 1=HOMBRE, 2=MUJER).; REGIMEN se determina por D.IPTIPOPAC desde INPACIENT (Versión 9), no por la entidad.; El diagnóstico mostrado proviene de ISNULL(ING.CODDIAING, HIS.CODDIAGNO) buscado en INDIAGNOS (Versión 11).; Para citas incumplidas/canceladas (estado 2 o 4 sin diagnóstico) no se exponen datos de ingreso ni diagnóstico (Versión 2).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOpportunityMedicalAppointmentCE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewOpportunityMedicalAppointmentCE: Devuelve un set DISTINCT de citas de consulta externa con métricas de oportunidad (DATEDIFF entre fecha solicitud, deseada, ofertada y asignación) y estado actual derivado del cumplimiento del ingreso.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOpportunityMedicalAppointmentCE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si A.TIPSOLICITU = 1/2/3 → Clasifica TIPO AGENDAMIENTO como ''Cita Medica'' / ''Apoyo Diagnostico'' / ''Tratamientos Especiales''.; si ISNULL(UNICE.UFUTIPUNI, UNIID.UFUTIPUNI) según código (1..31) → Asigna ÁMBITO (URGENCIAS, HOSPITALIZACION, APOYO DIAGNOSTICO, UCI, AMBULATORIO, CONSULTA PRIORITARIA, etc.); por defecto ''CONSULTA EXTERNA''.; si A.CODTIPCIT IS NOT NULL → TIPO DE CITA se toma de A.CODTIPCIT (0=PRIMERA VEZ, 1=CONTROL, 2=POS OPERATORIO, 3=CITA WEB). else Se toma de ING1.TIPCITMED (1=PRIMERA VEZ, 2=CONTROL) o N/A.; si HEA.Id IS NOT NULL (existe HealthAdministrator vinculada por A.GENCONENTITY) → CODIGO/ENTIDAD se toma de Contract.HealthAdministrator. else Se toma de INENTIDAD (ENT) ligada por D.CODENTIDA.; si ING.NUMINGRES IS NOT NULL OR A.CODESTCIT=0 → Calcula ESTADO ACTUAL combinando CODESTCIT con existencia de ingreso: 0=ASIGNADA; 1/2 con ingreso=CUMPLIDA, sin ingreso=INCUMPLIDA; 3=PREASIGNADA; 4 con FECHCANCELA=CANCELADA o con ingreso=CUMPLIDA; 5=INATENCION; 7=''''. else Sin ingreso → 1/2=INCUMPLIDA, 3=PREASIGNADA, 4=CANCELADA.; si A.CODESTCIT=0 AND CAST(A.FECHORAIN AS DATE)=hoy AND hora ≤ GETDATE()-5h → Marca la cita como ''INCUMPLIDA'' antes de que el JOB la procese (Versión 12).; si A.CODESTCIT IN (4,5) → Expone CODIGO y DESCRIPCION de cancelación y FECHA CANCELACION; en otro caso queda vacío.; si DIA.CODDIAGNO IS NULL AND A.CODESTCIT IN (2,4) → INGRESO DE ATENCION se devuelve NULL (no se muestra ingreso para citas incumplidas/canceladas sin diagnóstico, Versión 2). else Se devuelve ISNULL(GC.NUMINGRES, A.NUMINGRES).; si F.DURAACTIV <> 0 → DURACION ACTIVIDAD usa F.DURAACTIV. else Toma TOP 1 FD.DURACSERVI desde AGACTMEDD para CODACTMED y CODSERIPS.; si Prioridad CODIGO/DESCRIPCION CUPS → Se resuelve vía ISNULL en orden: INCUPSIPS(M) → Contract.CUPSEntity(CE)/ContractDescriptions(CD) → INCUPSIPS(IPS2) por RIASCUPS → INCUPSIPS(IPS3) por F.CODSERIPS.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOpportunityMedicalAppointmentCE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOpportunityMedicalAppointmentCE';
GO
