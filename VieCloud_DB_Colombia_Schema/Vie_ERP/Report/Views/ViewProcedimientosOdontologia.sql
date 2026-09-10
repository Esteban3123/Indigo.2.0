

    /*******************************************************************************************************************
Nombre: Report.ProcedimientosOdontologia
Tipo:Vista
Observacion:Vista de los procedimientos odontologicos
Profesional: Nilsson Miguel Galindo Lopez
Fecha:28-07-2022
---------------------------------------------------------------------------
Modificaciones
_____________________________________________________________________________
Vercion 1
Persona que modifico: 
Fecha:
Ovservaciones: 
--------------------------------------
Vercion 2
Persona que modifico:
Fecha:
***********************************************************************************************************************************/

--select * from dbo.ODONTOCONTROL
--select * from dbo.ODONTOCONTROLVALO
--select * from dbo.ODONTODIENTE --Tabla de control de odontograma por diente, y por tipo control (odontograma valoracion - Odontograma Tratamiento )
--select * from dbo.ODONTODIENTEDIAG --Tabla de Diagnosticos por diente
--select * from dbo.ODONTODIENTETRATA --Tabla de Tratamientos por Diente
--select * from dbo.ODONTOOTROSTRA --Tabla en la que se guarda los Otros tratamientos del odontograma.
--select * from dbo.ODONTOPLANTRATAMIENTOPAC --Tabla donde vamos a guardar el Plan de tratamiento del paciente
--select * from dbo.ODONTOPLANTRATAMIENTOPACD --Tabla en la cual me almacena los tratamientos que le envió el medico al paciente
--select * from dbo.ODONTOPLANTRATAMIENTOPACH --Tabla en la cual vamos a manejar los historicos de los tratamientos de los planes que ha tenido el paciente en el Odontograma
--select * from dbo.ODOPARDIA --Tabla que contiene los parametros de diagnosticos de odontologia
--select * from dbo.ODOPARTRA --Tabla que contiene los parametros de los tratamientos de odontologia
--select * from dbo.ODOPARTRACUPSD --Tabla en la cual vamos a almacenar los CUPS que tiene relacionados el tratamiento odontologico
--3.315
CREATE VIEW [Report].[ViewProcedimientosOdontologia]
as

WITH
CTE_CONSULTA AS 
(
SELECT TIPCITMED,NUMINGRES,NUMEFOLIO,FECINIATE,FECHFINH FROM dbo.HCURGING1 
),
CTE_DATOSPERSONALES AS
(
SELECT
CEN.NOMCENATE,PAC.IPTIPODOC,PAC.IPCODPACI,PAC.IPNOMCOMP,PAC.IPPRINOMB,PAC.IPSEGNOMB,PAC.IPPRIAPEL,PAC.IPSEGAPEL,PAC.IPSEXOPAC,
PAC.IPFECNACI,ING.IFECHAING,DEP.NOMDEPART,MUN.MUNNOMBRE,UB.UBINOMBRE,PAC.IPTELEFON,PAC.IPTELMOVI,EAPB.CODENTIDA,EAPB.NOMENTIDA,
PAC.IPTIPOPAC,FUN.UFUDESCRI,ODC.NUMINGRES,ODC.NUMEFOLIO,ODC.CODPROSAL,PRO.NOMMEDICO,CON.TIPCITMED,CON.FECINIATE,
CON.FECHFINH
FROM 
dbo.ODONTOCONTROL ODC INNER JOIN
DBO.ADINGRESO ING ON ODC.NUMINGRES=ING.NUMINGRES INNER JOIN
DBO.INPACIENT PAC ON ODC.IPCODPACI=PAC.IPCODPACI INNER JOIN
DBO.ADCENATEN CEN ON ODC.CODCENATE=CEN.CODCENATE INNER JOIN
DBO.INUBICACI UB ON PAC.AUUBICACI=UB.AUUBICACI INNER JOIN
DBO.INMUNICIP MUN ON UB.DEPMUNCOD = MUN.DEPMUNCOD INNER JOIN
DBO.INDEPARTA DEP ON MUN.DEPCODIGO = DEP.DEPCODIGO AND PAC.AUUBICACI = UB.AUUBICACI INNER JOIN
DBO.INENTIDAD AS EAPB ON PAC.CODENTIDA=EAPB.CODENTIDA INNER JOIN
DBO.INUNIFUNC AS FUN ON ODC.UFUCODIGO=FUN.UFUCODIGO INNER JOIN
DBO.INPROFSAL AS PRO ON ODC.CODPROSAL=PRO.CODPROSAL INNER JOIN
CTE_CONSULTA CON ON ODC.NUMINGRES=CON.NUMINGRES AND ODC.NUMEFOLIO=CON.NUMEFOLIO 
),

CTE_HISTORICO AS
(
SELECT
IDODONTOCONTROL,IDODOPARTRA,PROFESIONALORDENO,ESTADO,PROFESIONALREALIZO,FECHAREALIZO
FROM
dbo.ODONTOPLANTRATAMIENTOPACH 
)

select 
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
DAT.NOMCENATE AS [CENTRO DE ATENCION],
CASE DAT.IPTIPODOC WHEN 1 THEN 'CC - CEDULA DE CIUDADANIA' 
				   WHEN 2 THEN 'CE - CEDULA DE EXTRANJERIA' 
				   WHEN 3 THEN 'TI - TARJETA DE IDENTIDAD' 
				   WHEN 4 THEN 'RC - REGISTRO CIVIL' 
				   WHEN 5 THEN 'PA - PASAPORTE' 
				   WHEN 6 THEN 'AS - ADULTO SIN IDENTIFICACION' 
				   WHEN 7 THEN 'MS - MENOR SIN IDENTIFICACION' 
				   WHEN 8 THEN 'NU - NUMERO UNICO DE IDENTIFICACIÒN' 
				   WHEN 9 THEN 'NV - CERTIFICADO NACIDO VIVO' 
				   WHEN 10 THEN 'CD - CARNET DIPLOMATICO' 
				   WHEN 11 THEN 'SC - SALVOCONDUCTO' 
				   WHEN 12 THEN 'PE - PERMISO ESPECIAL DE PERMANENCIA' ELSE 'OTRO' END [TIPO IDENTIFICACION],
DAT.IPCODPACI AS IDENTIFICACION,
DAT.IPNOMCOMP AS [NOMBRE COMPLETO],
DAT.IPPRINOMB AS [PRIMER NOMBRE],
DAT.IPSEGNOMB AS [SEGUNDO NOMBRE], 
DAT.IPPRIAPEL AS [PRIMER APELLIDO],
DAT.IPSEGAPEL AS [SEGUNDO APELLIDO],
CASE DAT.IPSEXOPAC WHEN 1 THEN 'MASCULINO' ELSE 'FEMENINO' END [SEXO],
CAST(DAT.IPFECNACI AS DATE ) AS [FECHA_NACIMIENTO],
FLOOR((CAST(CONVERT(VARCHAR(8), DAT.IFECHAING , 112) AS INT) - CAST(CONVERT(VARCHAR(8), DAT.IPFECNACI, 112) AS INT)) / 10000) AS [EDAD],
DAT.NOMDEPART AS DEPARTAMENTO, 
DAT.MUNNOMBRE AS MUNICIPIO,
DAT.UBINOMBRE AS UBICACION,
DAT.IPTELEFON AS [TELEFONO],
DAT.IPTELMOVI AS [CELULAR],
DAT.CODENTIDA AS [CODIGO EAPB],
DAT.NOMENTIDA AS [NOMBRE EAPB],
CASE DAT.IPTIPOPAC WHEN 1 THEN 'Contributivo'
				   WHEN 2 THEN 'Subsidiado'
				   WHEN 3 THEN 'Vinculado'
				   WHEN 4 THEN 'Particular'
				   WHEN 5 THEN 'Otro' 
				   WHEN 6 THEN 'Desplazado Reg. Contributivo'
				   WHEN 7 THEN 'Desplazado Reg. Subsidiado'
				   WHEN 8 THEN 'Desplazado No Asegurado' END AS [REGIMEN],
DAT.UFUDESCRI AS [UNIDAD FUNCIONAL],
ODC.NUMINGRES AS INGRESO,
ODC.NUMEFOLIO AS FOLIO,
ODC.CODPROSAL AS [IDENTIFICACION PROFESIONAL],
DAT.NOMMEDICO AS [NOMBRE PROFESIONAL],
ODC.FECHAREG AS [FECHA REGISTRO],
DAT.FECINIATE AS [FECHA INICIAL ATENCION],
DAT.FECHFINH AS [FECHA FINAL ATENCION],
DIE.DIENTE AS [# DIENTE],
CASE OD.TIPO WHEN 1 THEN 'Nivel Diente' 
			 WHEN 2 THEN 'Vestibular' 
			 WHEN 3 THEN 'Oclusal' 
			 WHEN 4 THEN 'Palatina' 
			 WHEN 5 THEN 'Distal Izquierda' 
			 WHEN 6 THEN 'Distal Derecha' 
			 WHEN 7 THEN 'Mesial Derecha' 
			 WHEN 8 THEN 'Mesial Izquierda' 
			 WHEN 9 THEN 'Vestibular' 
			 WHEN 10 THEN 'Lingual' END UBICACIONES,	
rtrim(TRATA.CODIGOTRA) + ' - '+  RTRIM(TRATA.DESCRITRA) AS [DESCRIPCION],
RTRIM(TRATA.CODSERIPS)+' - '+CUP.DESSERIPS AS CUPS,
CASE TRATA.INDICECPO WHEN 'N' THEN 'No Aplica' 
					 WHEN 'C' THEN 'Cariado' 
					 WHEN 'P' THEN 'Perdido'
					 WHEN 'O' THEN 'Obturado' END AS [INDICE CPO],
CASE TRATA.INDICECEO WHEN 'N' THEN 'No Aplica' 
					 WHEN 'C' THEN 'Cariado' 
					 WHEN 'E' THEN 'Extraido'
					 WHEN 'O' THEN 'Obturado' END AS [INDICE CEO],
TRATA.OBSERVTRA AS OBSERVACION,
CASE DAT.TIPCITMED WHEN 1 THEN 'PRIMERA VEZ'
				   WHEN 0 THEN 'CONTROL' END AS [TIPO DE CITA],
CASE HIS.estado WHEN 1 THEN 'NO REALIZADO'
				WHEN 2 THEN 'REALIZADO' END as ESTADO,
CASE DIE.TIPOODONTOGRAMA WHEN 'V' THEN 'VALORACION'
						 WHEN 'T' THEN 'TRATAMIENTO' END AS TIPO,
 1 'CANTIDAD',
 CAST(ODC.FECHAREG AS date) AS 'FECHA BUSQUEDA',
 YEAR(ODC.FECHAREG) AS 'AÑO FECHA BUSQUEDA',
 MONTH(ODC.FECHAREG) AS 'MES AÑO FECHA BUSQUEDA',
 CASE MONTH(ODC.FECHAREG) WHEN 1 THEN 'ENERO'
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
FORMAT(DAY(ODC.FECHAREG), '00') AS 'DIA FECHA BUSQUEDA',
CONCAT(FORMAT(MONTH(ODC.FECHAREG), '00') ,' - ', 
CASE MONTH(ODC.FECHAREG) WHEN 1 THEN 'ENERO'
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
							  WHEN 12 THEN 'DICIEMBRE' END) MES_LABEL_INGRESO,
CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
from 
dbo.ODONTOCONTROL ODC INNER JOIN
CTE_DATOSPERSONALES DAT ON ODC.NUMINGRES=DAT.NUMINGRES INNER JOIN
dbo.ODONTODIENTE DIE ON ODC.ID=DIE.IDODONTOCONTROL INNER JOIN
dbo.ODONTODIENTETRATA OD on DIE.ID = OD.IDODONTODIENTE INNER JOIN
dbo.ODOPARTRA TRATA ON OD.CONSECTRA = TRATA.CONSECTRA LEFT JOIN
DBO.INCUPSIPS CUP ON TRATA.CODSERIPS=CUP.CODSERIPS LEFT JOIN
CTE_HISTORICO HIS ON ODC.ID=HIS.IDODONTOCONTROL AND TRATA.CONSECTRA=HIS.IDODOPARTRA

UNION ALL

select 
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
DAT.NOMCENATE AS [CENTRO DE ATENCION],
CASE DAT.IPTIPODOC WHEN 1 THEN 'CC - CEDULA DE CIUDADANIA' 
				   WHEN 2 THEN 'CE - CEDULA DE EXTRANJERIA' 
				   WHEN 3 THEN 'TI - TARJETA DE IDENTIDAD' 
				   WHEN 4 THEN 'RC - REGISTRO CIVIL' 
				   WHEN 5 THEN 'PA - PASAPORTE' 
				   WHEN 6 THEN 'AS - ADULTO SIN IDENTIFICACION' 
				   WHEN 7 THEN 'MS - MENOR SIN IDENTIFICACION' 
				   WHEN 8 THEN 'NU - NUMERO UNICO DE IDENTIFICACIÒN' 
				   WHEN 9 THEN 'NV - CERTIFICADO NACIDO VIVO' 
				   WHEN 10 THEN 'CD - CARNET DIPLOMATICO' 
				   WHEN 11 THEN 'SC - SALVOCONDUCTO' 
				   WHEN 12 THEN 'PE - PERMISO ESPECIAL DE PERMANENCIA' ELSE 'OTRO' END [TIPO IDENTIFICACION],
DAT.IPCODPACI AS IDENTIFICACION,
DAT.IPNOMCOMP AS [NOMBRE COMPLETO],
DAT.IPPRINOMB AS [PRIMER NOMBRE],
DAT.IPSEGNOMB AS [SEGUNDO NOMBRE], 
DAT.IPPRIAPEL AS [PRIMER APELLIDO],
DAT.IPSEGAPEL AS [SEGUNDO APELLIDO],
CASE DAT.IPSEXOPAC WHEN 1 THEN 'MASCULINO' ELSE 'FEMENINO' END [SEXO],
CAST(DAT.IPFECNACI AS DATE ) AS [FECHA_NACIMIENTO],
FLOOR((CAST(CONVERT(VARCHAR(8), DAT.IFECHAING , 112) AS INT) - CAST(CONVERT(VARCHAR(8), DAT.IPFECNACI, 112) AS INT)) / 10000) AS [EDAD],
DAT.NOMDEPART AS DEPARTAMENTO, 
DAT.MUNNOMBRE AS MUNICIPIO,
DAT.UBINOMBRE AS UBICACION,
DAT.IPTELEFON AS [TELEFONO],
DAT.IPTELMOVI AS [CELULAR],
DAT.CODENTIDA AS [CODIGO EAPB],
DAT.NOMENTIDA AS [NOMBRE EAPB],
CASE DAT.IPTIPOPAC WHEN 1 THEN 'Contributivo'
				   WHEN 2 THEN 'Subsidiado'
				   WHEN 3 THEN 'Vinculado'
				   WHEN 4 THEN 'Particular'
				   WHEN 5 THEN 'Otro' 
				   WHEN 6 THEN 'Desplazado Reg. Contributivo'
				   WHEN 7 THEN 'Desplazado Reg. Subsidiado'
				   WHEN 8 THEN 'Desplazado No Asegurado' END AS [REGIMEN],
DAT.UFUDESCRI AS [UNIDAD FUNCIONAL],
ODC.NUMINGRES AS INGRESO,
ODC.NUMEFOLIO AS FOLIO,
ODC.CODPROSAL AS [IDENTIFICACION PROFESIONAL],
DAT.NOMMEDICO AS [NOMBRE PROFESIONAL],
ODC.FECHAREG AS [FECHA REGISTRO],
DAT.FECINIATE AS [FECHA INICIAL ATENCION],
DAT.FECHFINH AS [FECHA FINAL ATENCION],
'' AS [# DIENTE],
'' AS UBICACIONES,	
rtrim(TRA.CODIGOTRA) + ' - '+  RTRIM(TRA.DESCRITRA) AS [DESCRIPCION],
RTRIM(TRA.CODSERIPS)+' - '+CUP.DESSERIPS AS CUPS,
CASE TRA.INDICECPO WHEN 'N' THEN 'No Aplica' 
					 WHEN 'C' THEN 'Cariado' 
					 WHEN 'P' THEN 'Perdido'
					 WHEN 'O' THEN 'Obturado' END AS [INDICE CPO],
CASE TRA.INDICECEO WHEN 'N' THEN 'No Aplica' 
					 WHEN 'C' THEN 'Cariado' 
					 WHEN 'E' THEN 'Extraido'
					 WHEN 'O' THEN 'Obturado' END AS [INDICE CEO],
TRA.OBSERVTRA AS OBSERVACION,
CASE DAT.TIPCITMED WHEN 1 THEN 'PRIMERA VEZ'
					 WHEN 0 THEN 'CONTROL' END AS [TIPO DE CITA],
CASE HIS.estado WHEN 1 THEN 'NO REALIZADO'
				WHEN 2 THEN 'REALIZADO' END as ESTADO,
'' AS TIPO, 1 'CANTIDAD',
 CAST(ODC.FECHAREG AS date) AS 'FECHA BUSQUEDA',
 YEAR(ODC.FECHAREG) AS 'AÑO FECHA BUSQUEDA',
 MONTH(ODC.FECHAREG) AS 'MES AÑO FECHA BUSQUEDA',
 CASE MONTH(ODC.FECHAREG) WHEN 1 THEN 'ENERO'
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
FORMAT(DAY(ODC.FECHAREG), '00') AS 'DIA FECHA BUSQUEDA',
CONCAT(FORMAT(MONTH(ODC.FECHAREG), '00') ,' - ', 
CASE MONTH(ODC.FECHAREG) WHEN 1 THEN 'ENERO'
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
							  WHEN 12 THEN 'DICIEMBRE' END) MES_LABEL_INGRESO,
CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
from 
dbo.ODONTOCONTROL ODC INNER JOIN
CTE_DATOSPERSONALES DAT ON ODC.NUMINGRES=DAT.NUMINGRES INNER JOIN
dbo.ODONTOOTROSTRA OTR ON ODC.ID=OTR.IDODONTOCONTROL INNER JOIN
dbo.ODOPARTRA TRA ON OTR.CONSECTRA=TRA.CONSECTRA LEFT JOIN
DBO.INCUPSIPS CUP ON TRA.CODSERIPS=CUP.CODSERIPS LEFT JOIN
CTE_HISTORICO HIS ON ODC.ID=HIS.IDODONTOCONTROL AND TRA.CONSECTRA=HIS.IDODOPARTRA
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting orientada a consumo en herramientas de inteligencia de negocio. Consolida, mediante UNION ALL, los procedimientos odontológicos realizados por diente (con número y ubicación de cara dental) y los tratamientos adicionales sin diente específico, aplanando datos demográficos del paciente, régimen de aseguramiento (EAPB), profesional tratante, códigos CUPS, índices CPO/CEO, estado de ejecución del plan de tratamiento y dimensiones de fecha (año, mes, día) para facilitar filtros y agrupaciones en reportes de producción odontológica.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewProcedimientosOdontologia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewProcedimientosOdontologia';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un solo conjunto de filas los procedimientos odontológicos realizados a pacientes (tanto los aplicados a un diente específico como los "otros tratamientos") con los datos demográficos, administrativos y clínicos asociados, para fines de reporte.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewProcedimientosOdontologia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El control odontológico (ODONTOCONTROL) debe estar enlazado a un ingreso válido en ADINGRESO, a un paciente en INPACIENT, a un centro de atención (ADCENATEN), unidad funcional (INUNIFUNC), profesional (INPROFSAL) y entidad/EAPB (INENTIDAD); de lo contrario la fila se descarta por ser INNER JOIN.; El paciente debe tener ubicación, municipio y departamento registrados (INUBICACI/INMUNICIP/INDEPARTA), pues se enlazan por INNER JOIN.; Para la primera rama, el control debe tener al menos un diente registrado (ODONTODIENTE) y un tratamiento por diente (ODONTODIENTETRATA) con su parámetro de tratamiento (ODOPARTRA).; Para la segunda rama, el control debe tener registros en ODONTOOTROSTRA con su parámetro de tratamiento en ODOPARTRA.; El ingreso/folio del control debe existir en HCURGING1 (CTE_CONSULTA) por INNER JOIN, lo que limita el universo a atenciones con registro en historia clínica de urgencias.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewProcedimientosOdontologia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada fila representa exactamente un procedimiento contado como CANTIDAD = 1 (constante).; ID_COMPANY se obtiene siempre del nombre de la base de datos actual (DB_NAME()) truncado a 9 caracteres.; ULT_ACTUAL se calcula convirtiendo GETDATE() a la zona horaria ''Pakistan Standard Time''.; La EDAD se calcula en años completos como FLOOR((AAAAMMDD_ingreso - AAAAMMDD_nacimiento)/10000), usando la fecha de ingreso como referencia, no la fecha actual.; El campo CUPS se forma siempre como ''CODSERIPS - DESSERIPS''; si no existe en INCUPSIPS (LEFT JOIN) la descripción será NULL y la concatenación retornará NULL.; El historial de ejecución (estado realizado/no realizado) se vincula por (IDODONTOCONTROL, IDODOPARTRA = CONSECTRA); si no hay historial, ESTADO queda en NULL (LEFT JOIN).; Solo se incluyen atenciones cuyo (NUMINGRES, NUMEFOLIO) tenga correspondencia en HCURGING1, lo que restringe el reporte a atenciones con nota de urgencias.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewProcedimientosOdontologia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewProcedimientosOdontologia: Devuelve la unión (UNION ALL) de dos conjuntos: tratamientos por diente (ODONTODIENTE + ODONTODIENTETRATA) y otros tratamientos (ODONTOOTROSTRA), ambos enlazados a ODOPARTRA y opcionalmente a INCUPSIPS y al historial ODONTOPLANTRATAMIENTOPACH.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewProcedimientosOdontologia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Origen del tratamiento: por diente vs. otros tratamientos → Primera rama del UNION: trae # DIENTE, UBICACIONES y TIPO (V=VALORACION, T=TRATAMIENTO) desde ODONTODIENTE/ODONTODIENTETRATA. else Segunda rama del UNION: # DIENTE, UBICACIONES y TIPO se devuelven como cadena vacía y los datos del tratamiento provienen de ODONTOOTROSTRA.; si IPTIPODOC del paciente (1..12) → Mapea a etiquetas de tipo de identificación colombianas (CC, CE, TI, RC, PA, AS, MS, NU, NV, CD, SC, PE); cualquier otro valor se rotula como ''OTRO''.; si IPTIPOPAC (1..8) → Mapea al régimen de afiliación: Contributivo, Subsidiado, Vinculado, Particular, Otro, Desplazado Reg. Contributivo, Desplazado Reg. Subsidiado, Desplazado No Asegurado.; si IPSEXOPAC = 1 → Sexo = ''MASCULINO'' else Sexo = ''FEMENINO'' (cualquier otro valor); si TIPCITMED = 1 vs 0 (HCURGING1) → 1 → ''PRIMERA VEZ''; 0 → ''CONTROL''.; si HIS.estado del historial ODONTOPLANTRATAMIENTOPACH → 1 → ''NO REALIZADO''; 2 → ''REALIZADO''; cualquier otro valor o ausencia (LEFT JOIN) → NULL.; si TRATA.INDICECPO ∈ {N,C,P,O} → N→''No Aplica'', C→''Cariado'', P→''Perdido'', O→''Obturado'' (índice CPO de dentición permanente).; si TRATA.INDICECEO ∈ {N,C,E,O} → N→''No Aplica'', C→''Cariado'', E→''Extraido'', O→''Obturado'' (índice CEO de dentición temporal).; si OD.TIPO (1..10) en ODONTODIENTETRATA → Traduce a la cara/superficie del diente: Nivel Diente, Vestibular, Oclusal, Palatina, Distal Izq/Der, Mesial Der/Izq, Lingual.; si DIE.TIPOODONTOGRAMA = ''V'' vs ''T'' → V → ''VALORACION''; T → ''TRATAMIENTO''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewProcedimientosOdontologia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewProcedimientosOdontologia';
GO
