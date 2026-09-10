
/*******************************************************************************************************************
Nombre: [Report].[ViewCitasAgendadas]
Tipo:Vista
Observacion:Vista que trae toda la información de las citas que tengan un estado de agendado
Profesional:
Fecha:
---------------------------------------------------------------------------
Modificaciones
_____________________________________________________________________________

Version 1
Persona que modifico: AMIRA GIL MENESES
Observacion:Se ingresa Códido del Médico. Solicitud del HSJ
Fecha:13-07-2023
-----------------------------------------------------------------------------------------
Version 2
Persona que modifico: Nilsson Miguel Galindo Lopez
Fecha: 20-09-2023
Ovservaciones:Se agrega el campo de la unidad funcional y se comentarea la validación de tipo de cita
-----------------------------------------------------------------------------------------
****************************************************************************************/

CREATE VIEW [Report].[ViewCitasAgendadas] as
select 
 CAST(DB_NAME() AS VARCHAR(11)) AS ID_COMPANY,
CASE A.TIPSOLICITU WHEN 1 THEN 'CITA MEDICA'
				   WHEN 2 THEN 'CITA APOYO DIAGNOSTICO'
				   WHEN 3 THEN 'CITA TRATAMIENTO ESPECIALES' ELSE '' END AS 'TIPO AGENDAMIENTO',
RTRIM(C.CODIPSSEC) AS 'CODIGO IPS', 
/*IN V3*/RTRIM(C.NOMCENATE) AS 'CENTRO ATENCION',RTRIM(UNI.UFUDESCRI) AS 'UNIDAD FUNCIONAL'/*FN V3*/
,B.CODESPECI AS [CODIGO ESPECIALIDAD],RTRIM(B.DESESPECI) AS 'ESPECIALIDAD',
/*IN V2*/RTRIM(E.CODPROSAL) + ' - ' + RTRIM(E.NOMMEDICO) AS 'MEDICO',--FN V2
RTRIM(F.DESACTMED) AS 'ACTIVIDAD AGENDAMIENTO' 
,ISNULL(RTRIM(M.CODSERIPS),ISNULL(RTRIM(CE.Code),ISNULL(RTRIM(O.CODSERIPS),RTRIM(IPS3.CODSERIPS)))) AS [CODIGO CUPS]
,ISNULL(RTRIM(M.DESSERIPS),ISNULL(RTRIM(CD.Name),ISNULL(RTRIM(IPS2.DESSERIPS),RTRIM(IPS3.DESSERIPS)))) AS 'DESCRIPCION CUPS'
,F.DURAACTIV + ' ' + 'MINUTOS' 'DURACION ACTIVIDAD',CASE D.IPTIPODOC WHEN '1' THEN 'CC' WHEN '2' THEN 'CE' WHEN '3' THEN 'TI' WHEN '4' THEN 'RC' WHEN '5' THEN 'PA' WHEN '6' THEN 'AS' WHEN '7' THEN 'MS' WHEN '8' THEN 'NU' WHEN '9' THEN 'NV' WHEN '10' THEN 'CD'
WHEN '11' THEN 'SC' WHEN '12' THEN 'PE' END AS [TIPO IDENTIFICACION],CASE D.IPTIPODOC WHEN '1' THEN 'CEDULA DE CIUDADANIA' WHEN '2' THEN 'CEDULA DE EXTRANJERIA' WHEN '3' THEN 'TARJETA DE IDENTIDAD' WHEN '4' THEN 'REGISTRO CIVIL' WHEN '5' THEN 'PASAPORTE'
WHEN '6' THEN 'ADULTO SIN IDENTIFICACION' WHEN '7' THEN 'MENOR SIN IDENTIFICACION' WHEN '8' THEN 'NUMERO UNICO DE IDENTIFICACIÒN' WHEN '9' THEN 'CERTIFICADO NACIDO VIVO' WHEN '10' THEN 'CARNET DIPLOMATICO'
WHEN '11' THEN 'SALVOCONDUCTO' WHEN '12' THEN 'PERMISO ESPECIAL DE PERMANENCIA' END AS [DESCRIPCION IDENTIFICACION]
,D.IPCODPACI AS [NRO IDENTIFICACION], CAST(D.IPFECNACI AS DATE) AS [FECHA NACIMIENTO],DATEDIFF(YEAR, D.IPFECNACI, GETDATE()) AS [EDAD],CASE D.IPSEXOPAC WHEN '1' THEN 'H' WHEN '2' THEN 'M' END AS [CODIGO SEXO]
,CASE D.IPSEXOPAC WHEN '1' THEN 'HOMBRE' WHEN '2' THEN 'MUJER' END AS [SEXO],RTRIM(D.IPPRIAPEL) AS [PRIMER APELLIDO],RTRIM(D.IPSEGAPEL) AS [SEGUNDO APELLIDO], RTRIM(D.IPPRINOMB) AS [PRIMER NOMBRE] , RTRIM(D.IPSEGNOMB) AS [SEGUNDO NOMBRE]
,DEP.depcodigo AS [CODIGO DEPARTAMENTO],DEP.nomdepart AS [NOMBRE DEPARTAMENTO RESIDENCIA], MUN.MUNCODIGO [CODIGO MUNICIPIO RESIDENCIA], MUN.MUNNOMBRE AS [NOMBRE MUNICIPIO RESIDENCIA]
,UPPER(D.IPDIRECCI) AS [DIRECCION], D.IPTELMOVI AS CELULAR, D.IPTELEFON AS [TELEFONO FIJO]
,CASE WHEN A.CODTIPSOL = '0' THEN 'PRESENCIAL' WHEN A.CODTIPSOL = '1' THEN 'TELEFONICA' END AS [FORMA DE SOLICITUD]
,CASE WHEN A.CODTIPCIT = '0' THEN 'PRIMERA VEZ' WHEN A.CODTIPCIT = '1' THEN 'CONTROL' WHEN A.CODTIPCIT = '2' THEN 'POS OPERATORIO' ELSE 'N/A' END [TIPO DE CITA]
,CASE A.MODALIDAD WHEN 0 THEN 'PRESENCIAL' WHEN 1 THEN 'TELECONSULTA' ELSE 'N/A' END AS [MODALIDAD]
,RTRIM(HEA.HealthEntityCode ) AS [CODIGO ENTIDAD]
,RTRIM(HEA.Name) AS [ENTIDAD]
,CASE HEA.EntityType
           WHEN 1 THEN 'EPS Contributivo'
           WHEN 2 THEN 'EPS Subsidiado'
           WHEN 3 THEN 'ET Vinculados Municipios'
           WHEN 4 THEN 'ET Vinculados Departamentos'
           WHEN 5 THEN 'ARL Riesgos Laborales'
           WHEN 6 THEN 'MP Medicina Prepagada'
           WHEN 7 THEN 'IPS Privada'
           WHEN 8 THEN 'IPS Publica'
           WHEN 9 THEN 'Regimen Especial'
           WHEN 10 THEN 'Accidentes de transito'
           WHEN 11 THEN 'Fosyga'
           WHEN 12 THEN 'Otros'
       END AS [REGIMEN]
,CG.Name AS [GRUPO ATENCION]
,CAST(A.FECITADES AS date) AS [FECHA DESEADA DE CITA] 
,CAST(A.FECHAOFERTADA AS date) AS [MEJOR CITA DISPONIBLE]
,CAST(FECHORAIN AS date) AS [FECHA ASIGNACION DE CITA]
,CONVERT(char(5), FECHORAIN, 108) AS [HORA ASIGNACION DE CITA]
,YEAR (A.FECHORAIN) AS [AÑO ASIGNACION DE CITA]
,MONTH (A.FECHORAIN) AS [MES ASIGNACION DE CITA]
,CASE MONTH(A.FECHORAIN) 
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
  END AS [MES NOMBRE ASIGNACION DE CITA]
,DAY (A.FECHORAIN) AS [DIA ASIGNACION DE CITA]
,CAST(FECHORAFI AS date) AS [FECHA FINAL ASIGNACION DE CITA]
,CONVERT(char(5), FECHORAFI, 108) AS [HORA FINAL ASIGNACION DE CITA]
,CAST(A.FECREGSIS AS date) AS [FECHA SOLICITUD DE CITA]
,CAST(A.FECREGSIS AS datetime) AS [FECHA REGISTRO EN BASE DE DATOS]
,DATEDIFF(DAY,A.FECREGSIS,FECHORAIN) AS 'FECHA ASIGNACION vs FECHA SOLICITUD'
,DATEDIFF(DAY,A.FECITADES,FECHORAIN) AS 'FECHA ASIGNACION vs FECHA DESEADA'
,DATEDIFF(DAY,A.FECREGSIS,A.FECHAOFERTADA) AS 'DISPONIBILIDAD DE AGENDA'
,CASE WHEN A.CITAEXTRA = 1 THEN 'Si' ELSE 'No' END AS [CITA EXTRA] 
,RTRIM(G.NOMUSUARI) AS [USUARIO QUE REGISTRO LA CITA],
  1 as 'CANTIDAD',
  CAST(A.FECHORAIN AS date) AS 'FECHA BUSQUEDA',
  YEAR(A.FECHORAIN) AS 'AÑO BUSQUEDA',
  MONTH(A.FECHORAIN) AS 'MES BUSQUEDA',
  CONCAT(FORMAT(MONTH(A.FECHORAIN), '00') ,' - ', 
	   CASE MONTH(A.FECHORAIN) 
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
INNER JOIN ..AGACTIMED AS F  ON A.CODACTMED = F.CODACTMED
INNER JOIN ..INPACIENT AS D  ON A.IPCODPACI = D.IPCODPACI
INNER JOIN ..INUBICACI AS UBI  ON D.AUUBICACI =UBI.AUUBICACI
INNER JOIN ..INMUNICIP as MUN  ON UBI.DEPMUNCOD =MUN.DEPMUNCOD
INNER JOIN ..INDEPARTA AS DEP  ON DEP.depcodigo =MUN.DEPCODIGO
INNER JOIN ..SEGusuaru AS G  ON A.CODUSUASI = G.CODUSUARI
LEFT JOIN ..ADCENATEN AS C ON A.CODCENATE = C.CODCENATE
LEFT JOIN ..AGCONSULT AS AGC  ON AGC.CODIGOCON =A.CODIGOCON AND AGC.CODCENATE =C.CODCENATE
LEFT JOIN ..INPROFSAL AS E  ON A.CODPROSAL = E.CODPROSAL
LEFT JOIN ..INESPECIA AS B  ON A.CODESPECI = B.CODESPECI
/*IN V3*/LEFT JOIN dbo.INUNIFUNC AS UNI ON AGC.UFUCODIGO=UNI.UFUCODIGO /*FN V3*/
LEFT JOIN .Contract.HealthAdministrator AS HEA  ON A.GENCONENTITY = HEA.Id
LEFT JOIN .Contract.CareGroup AS CG  ON CG.Id =D.GENCAREGROUP
LEFT JOIN .Contract.CUPSEntityContractDescriptions AS CECD  ON CECD.ID=A.IDDESCRIPCIONRELACIONADA
LEFT JOIN .Contract.CUPSEntity AS CE  ON CE.Id =CECD.CUPSEntityId
LEFT JOIN .Contract.ContractDescriptions AS CD  ON CD.Id =CECD.ContractDescriptionId
LEFT JOIN ..INCUPSIPS AS M  ON A.CODSERIPS = M.CODSERIPS
LEFT JOIN ..RIASCUPS AS O  ON A.IDRIASCUPS = O.ID
LEFT JOIN ..RIAS AS Q  ON O.IDRIAS = Q.ID
LEFT JOIN ..INCUPSIPS AS IPS2  ON O.CODSERIPS =IPS2.CODSERIPS
LEFT JOIN ..INCUPSIPS AS IPS3  ON F.CODSERIPS =IPS3.CODSERIPS
WHERE  /*IN V3 A.TIPSOLICITU = 1 and FN V3*/ A.CODESTCIT ='0' --and RTRIM(G.NOMUSUARI)='LADY DIANA CABRERA'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting orientada a consumo por herramientas de inteligencia de negocio. Consolida todas las citas con estado **agendado** (`CODESTCIT=''0''`), aplanando datos del paciente (identificación, demografía, residencia, contacto), del profesional de salud, especialidad, actividad médica con código CUPS, centro de atención, unidad funcional y entidad aseguradora con su régimen. Calcula métricas de oportunidad como diferencia entre fecha de solicitud, fecha deseada y fecha asignada, e incluye descomposición temporal (año, mes, día) para facilitar análisis de demanda y tiempos de espera en agendamiento.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewCitasAgendadas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewCitasAgendadas';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone para reportería las citas médicas en estado ''agendado'' con datos de paciente, profesional, especialidad, entidad, CUPS, tiempos de asignación y ubicación geográfica.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewCitasAgendadas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de registros en AGASICITA con CODESTCIT=''0'' (estado agendado); Cada cita debe tener actividad médica (AGACTIMED), paciente (INPACIENT), ubicación (INUBICACI), municipio (INMUNICIP), departamento (INDEPARTA) y usuario asignador (SEGusuaru) válidos por ser INNER JOIN; El nombre de la BD actual (DB_NAME) debe caber en VARCHAR(11)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewCitasAgendadas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen citas con estado agendado (CODESTCIT=''0''); El identificador de compañía corresponde al nombre de la base de datos en ejecución, truncado a 11 caracteres; La edad se calcula como diferencia en años entre IPFECNACI y la fecha actual; Los días entre solicitud, fecha deseada y asignación se calculan como DATEDIFF en días; La marca temporal ULT_ACTUAL siempre se entrega convertida a la zona horaria ''Pakistan Standard Time''; Cada fila aporta CANTIDAD = 1 (apta para conteos en BI); El código del médico se concatena con su nombre en el campo MEDICO (formato ''CODIGO - NOMBRE'')', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewCitasAgendadas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cita médica; Agendamiento; Especialidad; Profesional de salud; Paciente; Tipo de identificación; CUPS; RIAS; Centro de atención; Unidad funcional; Entidad de salud / régimen (EPS, ARL, IPS, Medicina Prepagada, Fosyga); Grupo de atención; Modalidad (presencial / teleconsulta); Tipo de cita (primera vez / control / posoperatorio); Cita extra; Disponibilidad de agenda; Oportunidad de la cita (fecha deseada vs asignada)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewCitasAgendadas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewCitasAgendadas: Devuelve únicamente las citas cuyo CODESTCIT = ''0'' (agendadas), enriquecidas con catálogos de centro, especialidad, profesional, CUPS, entidad, grupo de atención y unidad funcional', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewCitasAgendadas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si A.TIPSOLICITU IN (1,2,3) → Etiqueta TIPO AGENDAMIENTO como ''CITA MEDICA'', ''CITA APOYO DIAGNOSTICO'' o ''CITA TRATAMIENTO ESPECIALES'' else Cadena vacía; si Resolución del CUPS de la cita → Toma INCUPSIPS (M) por A.CODSERIPS; si es null, CUPSEntity (CE) vía CUPSEntityContractDescriptions; si es null, RIASCUPS (O); en última instancia el CUPS de la actividad médica (AGACTIMED.CODSERIPS); si A.CODTIPSOL = ''0'' / ''1'' → FORMA DE SOLICITUD = ''PRESENCIAL'' / ''TELEFONICA''; si A.CODTIPCIT IN (''0'',''1'',''2'') → TIPO DE CITA = ''PRIMERA VEZ'' / ''CONTROL'' / ''POS OPERATORIO'' else ''N/A''; si A.MODALIDAD = 0 / 1 → MODALIDAD = ''PRESENCIAL'' / ''TELECONSULTA'' else ''N/A''; si A.CITAEXTRA = 1 → CITA EXTRA = ''Si'' else ''No''; si HEA.EntityType (1..12) → Mapea a régimen (EPS Contributivo, EPS Subsidiado, ARL, Medicina Prepagada, IPS Pública/Privada, Régimen Especial, Fosyga, Otros, etc.); si D.IPTIPODOC entre ''1'' y ''12'' → Asigna abreviatura (CC, CE, TI, RC, PA, AS, MS, NU, NV, CD, SC, PE) y descripción del tipo de identificación', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewCitasAgendadas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGASICITA; dbo.AGACTIMED; dbo.INPACIENT; dbo.INUBICACI; dbo.INMUNICIP; dbo.INDEPARTA; dbo.SEGusuaru; dbo.ADCENATEN; dbo.AGCONSULT; dbo.INPROFSAL; dbo.INESPECIA; dbo.INUNIFUNC; Contract.HealthAdministrator; Contract.CareGroup; Contract.CUPSEntityContractDescriptions; Contract.CUPSEntity; Contract.ContractDescriptions; dbo.INCUPSIPS; dbo.RIASCUPS; dbo.RIAS', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewCitasAgendadas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewCitasAgendadas';
GO
