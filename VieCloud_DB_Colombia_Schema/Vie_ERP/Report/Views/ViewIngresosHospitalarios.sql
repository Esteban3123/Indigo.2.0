

/****************************************************************************************
-----------------------------------------------------------------------------------------
Version 1
Persona que modifico: AMIRA GIL MENESES
Observacion:Se ingresa dirección, Telefono y Móvil del Paciente. Solicitud del HSJ
Fecha:12-07-2023
-----------------------------------------------------------------------------------------
****************************************************************************************/

CREATE VIEW [Report].[ViewIngresosHospitalarios] as

/*
INDIGO001
INDIGO006
INDIGO007
INDIGO008
INDIGO009
INDIGO010
INDIGO012
INDIGO013
INDIGO014
INDIGO015
INDIGO030
*/

WITH ESTANCIA_UNICA
          AS (SELECT EST.IPCODPACI, 
                     EST.NUMINGRES, 
                     MIN(ID) ID
              FROM CHREGESTA AS EST
              GROUP BY EST.IPCODPACI, 
                       EST.NUMINGRES),
          CTE_INGRESOS_HOSPITALARIOS
          AS (SELECT CEN.NOMCENATE 'CENTRO ATENCION',
                     CASE UNI.UFUTIPUNI
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
                         WHEN 15 THEN 'CONSULTA EXTERNA'
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
                         WHEN 31 THEN 'CONSULTA PRIORITARIA' END 'TIPO UNIDAD', 
                     UNI.UFUDESCRI 'UNIDAD FUNCIONAL',
					 CASE EST.CODTIPEST WHEN '001' THEN 'GENERAL' 
									    WHEN '002' THEN 'CUIDADO BASICO' 
									    WHEN '003' THEN 'CUIDADO INTERMEDIO' 
									    ELSE 'CUIDADO INTENSIVO'END AS 'TIPO ESTANCIA',
                     HEA.Code 'CODIGO ENTIDAD', 
                     HEA.Name 'ENTIDAD', 
                     CGR.Name 'GRUPO DE ATENCION',
					 CASE PAC.IPTIPOPAC WHEN 1 THEN 'CONTRIBUTIVO' 
										WHEN 2 THEN 'SUBSIDIADO' 
										WHEN 3 THEN 'VINCULADO' 
										WHEN 4 THEN 'PARTICULAR' 
										WHEN 5 THEN 'OTRO' 
										WHEN 6 THEN 'DESPLAZADO REG. CONTRIBUTIVO' 
										WHEN 7 THEN 'DESPLAZADO REG. SUBSIDIADO' 
										ELSE 'DESPLAZADO NO ASEGURADO' 
										END AS 'RÉGIMEN',
                     CAM.NUMCAMHOS 'CAMA', 
                     CAM.DESCCAMAS 'DESCRIPCION CAMA',
                     CASE PAC.IPTIPODOC
                         WHEN '1' THEN 'CC'
                         WHEN '2' THEN 'CE'
                         WHEN '3' THEN 'TI'
                         WHEN '4' THEN 'RC'
                         WHEN '5' THEN 'PA'
                         WHEN '6' THEN 'AS'
                         WHEN '7' THEN 'MS'
                         WHEN '8' THEN 'NU'
                         WHEN '9' THEN 'NV'
                         WHEN '10' THEN 'CD'
                         WHEN '11' THEN 'SC'
                         WHEN '12' THEN 'PE'
                     END AS [TIPO IDENTIFICACION],
                     CASE PAC.IPTIPODOC
                         WHEN '1' THEN 'CEDULA DE CIUDADANIA'
                         WHEN '2' THEN 'CEDULA DE EXTRANJERIA'
                         WHEN '3' THEN 'TARJETA DE IDENTIDAD'
                         WHEN '4' THEN 'REGISTRO CIVIL'
                         WHEN '5' THEN 'PASAPORTE'
                         WHEN '6' THEN 'ADULTO SIN IDENTIFICACION'
                         WHEN '7' THEN 'MENOR SIN IDENTIFICACION'
                         WHEN '8' THEN 'NUMERO UNICO DE IDENTIFICACIÒN'
                         WHEN '9' THEN 'CERTIFICADO NACIDO VIVO'
                         WHEN '10' THEN 'CARNET DIPLOMATICO'
                         WHEN '11' THEN 'SALVOCONDUCTO'
                         WHEN '12' THEN 'PERMISO ESPECIAL DE PERMANENCIA'  END AS 'DESCRIPCION IDENTIFICACION', 
                     EST.IPCODPACI AS 'IDENTIFICACION', 
                     PAC.IPPRINOMB 'PRIMER NOMBRE', 
                     PAC.IPSEGNOMB 'SEGUNDO NOMBRE', 
                     PAC.IPPRIAPEL 'PRIMER APELLIDO', 
                     PAC.IPSEGAPEL 'SEGUNDO APELLIDO', 
                     PAC.IPNOMCOMP 'NOMBRE COMPLETO PACIENTE', 
                     PAC.IPFECNACI 'FECHA NACIMIENTO', 
                     FLOOR((CAST(CONVERT(VARCHAR(8), EST.FECINIEST, 112) AS INT) - CAST(CONVERT(VARCHAR(8), PAC.IPFECNACI, 112) AS INT)) / 10000) AS EDAD,
					 CASE WHEN PAC.IPSEXOPAC = '1' THEN 'M' ELSE 'F' END 'SEXO',
					 PAC.IPTELMOVI 'MOVIL',
					 PAC.IPTELEFON 'TELEFONO',
					 PAC.IPDIRECCI 'DIRECCION',
                     EST.NUMINGRES 'INGRESO', 
					 CASE WHEN DIS.DISCDESCRI IS NULL THEN 'NINGUNO' ELSE DIS.DISCDESCRI END AS DISCAPACIDAD, 
                     CAST(EST.FECINIEST AS DATE) AS 'FECHA INICIAL ESTANCIA', 
                     PRO.NOMMEDICO 'PROFESIONAL DE LA SALUD', 
                     ESP.DESESPECI 'ESPECIALIDAD', 
                     ING.CODDIAING 'CIE10 INGRESO', 
                     DIA.NOMDIAGNO 'DIAGNOSTICO DE INGRESO', 
                     ING.CODDIAEGR 'CIE10 EGRESO', 
                     DIAE.NOMDIAGNO 'DIAGNOSTICO DE EGRESO', 
                     1 AS 'CANTIDAD', 
					 CAST(EST.FECINIEST AS date) AS 'FECHA BUSQUEDA',
					 YEAR(EST.FECINIEST) AS 'AÑO BUSQUEDA',
					 MONTH(EST.FECINIEST) AS 'MES BUSQUEDA',
					 CONCAT(FORMAT(MONTH(EST.FECINIEST), '00') ,' - ', 
					 CASE MONTH(EST.FECINIEST) 
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
              FROM CHREGESTA AS EST WITH(NOLOCK)
                   JOIN ESTANCIA_UNICA AS EU WITH(NOLOCK) ON EU.ID = EST.ID
                   JOIN INPACIENT AS PAC WITH(NOLOCK) ON PAC.IPCODPACI = EST.IPCODPACI
/*IN V1*/		   LEFT JOIN ADDISCAPACI DIS ON DIS.DISCCODIGO=PAC.DISCCODIGO   /*FN V1*/
                   JOIN ADINGRESO AS ING WITH(NOLOCK) ON ING.IPCODPACI = EST.IPCODPACI
                                                         AND ING.NUMINGRES = EST.NUMINGRES
                   JOIN CHCAMASHO CAM ON EST.CODICAMAS = CAM.CODICAMAS
                   JOIN ADCENATEN AS CEN WITH(NOLOCK) ON CAM.CODCENATE = CEN.CODCENATE
                   JOIN INUNIFUNC UNI WITH(NOLOCK) ON CAM.UFUCODIGO = UNI.UFUCODIGO
                   JOIN Contract.HealthAdministrator HEA WITH(NOLOCK) ON ING.GENCONENTITY = HEA.ID
                   JOIN Contract.CareGroup AS CGR WITH(NOLOCK) ON ING.GENCAREGROUP = CGR.Id
                   LEFT JOIN..INPROFSAL PRO WITH(NOLOCK) ON EST.CODPROSAL = PRO.CODPROSAL
                   LEFT JOIN..INESPECIA AS ESP WITH(NOLOCK) ON ESP.CODESPECI = EST.CODESPECI
                   LEFT JOIN..INDIAGNOS AS DIA ON ING.CODDIAING = DIA.CODDIAGNO
                   LEFT JOIN..INDIAGNOS AS DIAE ON ING.CODDIAEGR = DIAE.CODDIAGNO 
              --WHERE CAST(EST.FECINIEST AS DATE) BETWEEN @FECINI AND @FECFIN
              )
          SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY, 
                 *
          FROM CTE_INGRESOS_HOSPITALARIOS;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting para el análisis de ingresos hospitalarios, consolidando por paciente y número de ingreso la primera estancia registrada. Integra datos demográficos del paciente (identificación, nombre, sexo, edad calculada, contacto, dirección y discapacidad), información de cama, unidad funcional y centro de atención, régimen de afiliación, entidad pagadora, grupo de atención, profesional, especialidad, y diagnósticos CIE10 de ingreso y egreso. Expone campos temporales (año, mes, nombre de mes) para filtrado en herramientas de BI/reporting, e incluye el nombre de la base de datos como identificador de empresa.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewIngresosHospitalarios';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewIngresosHospitalarios';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista consolidada de ingresos hospitalarios que combina datos de estancia, paciente, ingreso administrativo, cama, unidad funcional, entidad pagadora y diagnósticos para reportería.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewIngresosHospitalarios';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada par (IPCODPACI, NUMINGRES) debe existir en CHREGESTA para considerar la estancia.; El ingreso administrativo (ADINGRESO) debe coexistir para la misma combinación paciente/ingreso.; La cama (CHCAMASHO), centro de atención (ADCENATEN), unidad funcional (INUNIFUNC), entidad (HealthAdministrator) y grupo de atención (CareGroup) referenciados deben existir (JOIN obligatorio).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewIngresosHospitalarios';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La edad se calcula como diferencia entera de años entre la fecha de inicio de estancia y la fecha de nacimiento usando aritmética AAAAMMDD/10000.; Cada fila representa una unidad de ingreso (CANTIDAD = 1 fija) para conteos agregados.; ULT_ACTUAL se calcula con la hora actual convertida a ''Pakistan Standard Time''.; ID_COMPANY se obtiene de DB_NAME() truncado a VARCHAR(9), identificando la base/instancia donde se ejecuta la vista.; Solo se incluyen ingresos con cama, unidad funcional, centro de atención, entidad y grupo de atención asignados (JOINs INNER); profesional, especialidad y diagnósticos son opcionales (LEFT JOIN).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewIngresosHospitalarios';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ingreso hospitalario; Estancia; Paciente; Cama; Unidad funcional; Centro de atención; Tipo de estancia (cuidado básico/intermedio/intensivo); Régimen de afiliación; Entidad pagadora (EPS/aseguradora); Grupo de atención; Discapacidad; Diagnóstico CIE-10 de ingreso y egreso; Profesional de la salud; Especialidad; Tipo de identificación', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewIngresosHospitalarios';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewIngresosHospitalarios: Devuelve una sola fila por estancia mínima de cada par (IPCODPACI, NUMINGRES) usando MIN(ID) en el CTE ESTANCIA_UNICA, evitando duplicar el ingreso por múltiples movimientos de estancia.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewIngresosHospitalarios';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si UNI.UFUTIPUNI → Mapea el código numérico a una etiqueta de tipo de unidad (URGENCIAS, HOSPITALIZACION, UCI cuando es 5–11, UNIDAD RENAL, ONCOLOGICA, etc.); valores 5 a 11 se consolidan todos como ''UCI''.; si EST.CODTIPEST IN (''001'',''002'',''003'') → Traduce a ''GENERAL'', ''CUIDADO BASICO'' o ''CUIDADO INTERMEDIO'' respectivamente. else Cualquier otro valor se etiqueta como ''CUIDADO INTENSIVO''.; si PAC.IPTIPOPAC entre 1 y 7 → Asigna régimen (CONTRIBUTIVO, SUBSIDIADO, VINCULADO, PARTICULAR, OTRO, DESPLAZADO REG. CONTRIBUTIVO, DESPLAZADO REG. SUBSIDIADO). else Cualquier otro valor se etiqueta como ''DESPLAZADO NO ASEGURADO''.; si PAC.IPSEXOPAC = ''1'' → Sexo = ''M''. else Sexo = ''F'' (cualquier otro valor incluido NULL).; si DIS.DISCDESCRI IS NULL → Discapacidad se reporta como ''NINGUNO''. else Se usa la descripción de ADDISCAPACI.; si PAC.IPTIPODOC entre ''1'' y ''12'' → Mapea a abreviatura (CC, CE, TI, RC, PA, AS, MS, NU, NV, CD, SC, PE) y a su descripción extendida (CEDULA DE CIUDADANIA, etc.).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewIngresosHospitalarios';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CHREGESTA; dbo.INPACIENT; dbo.ADDISCAPACI; dbo.ADINGRESO; dbo.CHCAMASHO; dbo.ADCENATEN; dbo.INUNIFUNC; Contract.HealthAdministrator; Contract.CareGroup; dbo.INPROFSAL; dbo.INESPECIA; dbo.INDIAGNOS', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewIngresosHospitalarios';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewIngresosHospitalarios';
GO
