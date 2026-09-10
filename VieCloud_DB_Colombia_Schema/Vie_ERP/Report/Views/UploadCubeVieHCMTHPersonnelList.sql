



--CREATE PROCEDURE [dbo].[ODO_TH_Listado_Personal]

CREATE view [Report].[UploadCubeVieHCMTHPersonnelList] AS

	WITH addresses AS
	(
		SELECT 
			ROW_NUMBER() OVER(PARTITION BY per.id ORDER BY per.id ASC) AS [RowNumer],
			[add].id,
			per.id idperson,
			UPPER([add].addresss) AS [DireccionResidencia],
			dp.name AS [DepartamentoResidencia],
			ct.name AS [MunicipioResidencia]
		FROM common.address AS [add] 
		INNER JOIN common.department AS dp ON [add].departmentid = dp.id 
		INNER JOIN common.city AS ct ON [add].cityid = ct.id
		INNER JOIN common.person AS per ON [add].idperson = per.id 
		INNER JOIN common.thirdparty AS thi ON per.id = thi.personid
		INNER JOIN payroll.employee AS emp ON thi.id = emp.thirdpartyid 
	)

	SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		CASE per.identificationtype 
			WHEN 0 THEN 'CC - Cedula de Ciudadania'
			WHEN 1 THEN 'CE - Cedula de Extranjeria'
			WHEN 2 THEN 'TI - Tarjeta de Identidad'
			WHEN 3 THEN 'RC - Registro Civil'
			WHEN 4 THEN 'PA - Pasaporte'
			WHEN 5 THEN 'AS - Adulto Sin Identificacion'
			WHEN 6 THEN 'MS - Menor Sin Identificacion'
			WHEN 7 THEN 'NIT - NIT'
			WHEN 8 THEN 'NU - Numero Unico de Identificacion Personal'
			WHEN 9 THEN 'Certificado de Nacido Vivo'
			WHEN 10 THEN 'CD - Carnet Diplomatico'
			WHEN 11 THEN 'SC - Salvoconducto'
			WHEN 12 THEN 'PE - Permiso Especial de Permanencia'
			WHEN 13 THEN 'PT - Permiso Temporal de Permanencia'
			WHEN 14 THEN 'DE - Documento Extranjero'
			WHEN 15 THEN 'SI - Sin Identificacion' END AS 'TIPO DOCUMENTO',--[TipoDocumento],
		per.identificationnumber AS 'NRO DOCUMENTO',--[NroDocumento],
		citye.name AS 'MUNICIPIO EXPEDICION',--[MunicipioExpedicion],
		per.identificationexpeditiondate AS 'FECHA EXPEDICION',--[FechaExpedicion],
		thi.name AS 'NOMBRE',--[Nombre],
		pos.code AS 'CODIGO CARGO',--[CodigoCargo],
		pos.name AS 'DESCRIPCION CARGO',--[DescripcionCargo],
		offi.name AS 'SEDE',--[Sede],
		uf.name AS 'UNIDAD FUNCIONAL',--[UnidadFuncional],
		CASE uf.unittype 
			WHEN 1 THEN 'Urgencias' 
			WHEN 2 THEN 'Hospitalizacion' 
			WHEN 3 THEN 'Apoyo DX' 
			WHEN 4 THEN 'Apoyo Terapeutico' 
			WHEN 5 THEN 'Unidades de Cuidado Intensivo Adulto' 
			WHEN 6 THEN 'Unidades de Cuidado Intermedio Adulto' 
			WHEN 7 THEN 'Unidades de Cuidado Intensivo Pediatrica' 
			WHEN 8 THEN 'Unidades de Cuidado Intermedio Pediatrica' 
			WHEN 9 THEN 'Unidades de Cuidado Intensivo Neonatal' 
			WHEN 10 THEN 'Unidades de Cuidado Intermedio Neonatal' 
			WHEN 11 THEN 'Unidades de Cuidado Basico Neonatal' 
			WHEN 12 THEN 'Unidad Renal' 
			WHEN 13 THEN 'Unidad Oncologica' 
			WHEN 14 THEN 'Unidad Medicina Nuclear' 
			WHEN 15 THEN 'Consulta Externa' 
			WHEN 16 THEN 'Unidad Mental' 
			WHEN 17 THEN 'Unidad de Quemados' 
			WHEN 18 THEN 'Unidad de Cuidado Paliativo' 
			WHEN 19 THEN 'Cirugia' 
			WHEN 20 THEN 'Laboratorio' 
			WHEN 21 THEN 'Cardiologia No Invasiva'
			WHEN 22 THEN 'Cardiologia Invasiva '
			WHEN 23 THEN 'Gineco-Obstetricia'
			WHEN 24 THEN 'Consulta Externa - Gineco-Obstetricia'
			WHEN 25 THEN 'Otras' END AS 'TIPO UNIDAD FUNCIONAL',--[TipoUnidadFuncional],
		grp.code AS 'CODIGO GRUPO',--[CodigoGrupo],
		grp.name AS 'DESCRIPCION GRUPO',--[DescripcionGrupo],
		empt.name AS 'TIPO COLABORADOR',--[TipoColaborador],
		CASE per.gender WHEN '1' THEN 'Masculino' WHEN '2' THEN 'Femenino' WHEN '3' THEN 'Otro' END AS 'GENERO',--[Genero],
		CAST(per.birthdate AS DATETIME) AS 'FECHA NACIMIENTO',--[FechaNacimiento],
		DATEDIFF(YEAR, per.birthdate, GETDATE()) AS 'EDAD',--[Edad],
		cityn.name AS 'MUNICIPIO NACIMIENTO',--[MunicipioNacimiento],
		(SELECT TOP 1 LOWER(em.email) FROM common.email em WHERE em.idPerson = per.id) AS 'CORREO ELECTRONICO',--[CorreoElectronico],
		(SELECT TOP 1 ph.phone FROM common.phone ph, common.phonetype pt WHERE ph.idperson = per.id AND ph.idphonetype = pt.id AND pt.id = 1) AS 'TELEFONO PRINCIPAL',--[TelefonoPrincipal],
		(SELECT TOP 1 ph.phone FROM common.phone ph, common.phonetype pt WHERE ph.idperson = per.id AND ph.idphonetype = pt.id AND pt.id = 2) AS 'TELEFONO ALTERNATIVO',--[TelefonoAlternativo],
		cont.name AS 'TIPO CONTRATO',--[TipoContrato],
		CAST(con.jobbondingdate AS DATETIME) AS 'FECHA INGRESO',--[FechaIngreso],
		DATEDIFF(MONTH, con.jobbondingdate, GETDATE()) AS 'ANTIGUEDAD MES',--[AntiguedadMes],
		CAST(con.contractendingdate AS DATETIME) AS 'FECHA FIN',--[FechaFin],
		con.basicsalary AS 'SALARIO',--[Salario],
		per.bloodgroup AS 'GRUPO SANGUINEO',--[GrupoSanguineo],
		per.rh AS 'RH',--[RH],
		(SELECT TOP 1 ps.professionalcardnumber FROM common.personstudy ps WHERE ps.personId = per.id) AS 'NRO TARJETA PROFESIONAL',--[NroTarjetaProfesional],
		[add].[DireccionResidencia] 'DIRECCION RESIDENCIA',--,
		[add].[DepartamentoResidencia] 'DEPARTAMENTO RESIDENCIA',--,
		[add].[MunicipioResidencia] 'MUNICIPIO RESIDENCIA',--,
		bank.name AS 'BANCO',--[Banco],
		CASE con.bankaccounttype WHEN 1 THEN 'Ahorros' WHEN 2 THEN 'Corriente' END AS 'TIPO CUENTA',--[TipoCuenta],
		con.bankaccountnumber AS 'NRO CUENTA',--[NroCuenta],

		RTRIM(eth.code) + ' - ' + RTRIM(eth.description) AS 'GRUPO ETNICO',--[GrupoEtnico],
		(SELECT TOP 1 st.studylevel FROM payroll.studytype st, common.personstudy ps WHERE ps.studytypeid = st.id AND per.id = ps.personid) AS 'NIVEL ACADEMICO',--[NivelAcademico],
		(SELECT TOP 1 pp.name FROM payroll.profession pp, common.personprofession cpp WHERE pp.id = cpp.idprofession AND per.id = cpp.personid) AS 'PROFESION',--[Profesion],
		con.id AS 'NRO CONTRATO',--[NroContrato],
		RTRIM(lev.code) + ' - ' + lev.name AS 'NIVEL CARGO',--[NivelCargo],
		(SELECT TOP 1 f.name FROM payroll.fundcontract fc, payroll.fund f WHERE fc.contractid = con.id and fc.fundtype = 1 AND f.id = fc.fundid and fc.state = 1 AND fc.voluntarycontribution = 0) AS 'SALUD',--[Salud],
		(SELECT TOP 1 f.name FROM payroll.fundcontract fc, payroll.fund f WHERE fc.contractid = con.id and fc.fundtype = 1 AND f.id = fc.fundid and fc.state = 1 AND fc.voluntarycontribution = 1) AS 'SALUD VOLUNTARIA',--[SaludVoluntaria],
		(SELECT TOP 1 f.name FROM payroll.fundcontract fc, payroll.fund f WHERE fc.contractid = con.id and fc.fundtype = 2 AND f.id = fc.fundid and fc.state = 1 AND fc.voluntarycontribution = 0) AS 'PENSION',--[Pension],
		(SELECT TOP 1 f.name FROM payroll.fundcontract fc, payroll.fund f WHERE fc.contractid = con.id and fc.fundtype = 2 AND f.id = fc.fundid and fc.state = 1 AND fc.voluntarycontribution = 1) AS 'PENSION VOLUNTARIA',--[PensionVoluntaria],
		(SELECT TOP 1 f.name FROM payroll.fundcontract fc, payroll.fund f WHERE fc.contractid = con.id and fc.fundtype = 5 AND f.id = fc.fundid and fc.state = 1 AND fc.voluntarycontribution = 0) AS 'CAJA COMPENSACION',--[CajaCompensacion],
		(SELECT TOP 1 f.name FROM payroll.fundcontract fc, payroll.fund f WHERE fc.contractid = con.id and fc.fundtype = 3 AND f.id = fc.fundid and fc.state = 1 AND fc.voluntarycontribution = 0) AS 'CESANTIAS',--[Cesantias],
		(SELECT TOP 1 f.name FROM payroll.fundcontract fc, payroll.fund f WHERE fc.contractid = con.id and fc.fundtype = 4 AND f.id = fc.fundid and fc.state = 1 AND fc.voluntarycontribution = 0) AS 'ARL',--[ARL],
		emp.professionalriskpercentage AS 'PORCENTAJE ARL',--[PorcentajeARL],
		kin.name AS 'PARENTESCO',--[Parentesco],
		UPPER(rsh.name) AS 'NOMBRE FAMILIAR',--[NombreFamiliar],
		CAST(rsh.birthdate AS DATETIME) AS 'FEHA NACIMIENTO FAMILIAR',--[FechaNacimientoFamiliar]
		CAST(con.jobbondingdate AS DATE) AS [FECHA BUSQUEDA],
		CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
	FROM common.person AS per 
	INNER JOIN common.thirdparty AS thi ON per.id = thi.personid
	INNER JOIN payroll.employee AS emp ON thi.id = emp.thirdpartyid 
	LEFT JOIN common.city AS citye ON per.identificacioncityid = citye.id
	LEFT JOIN common.city AS cityn ON per.birthcityid = cityn.id
	INNER JOIN payroll.[contract] AS con ON emp.id = con.employeeid AND con.status = 1 AND con.valid = 1
	LEFT JOIN payroll.[group] AS grp ON con.groupid = grp.id
	LEFT JOIN payroll.position AS pos ON con.positionid = pos.id
	LEFT JOIN payroll.functionalunit AS uf ON con.functionalunitid = uf.id
	LEFT JOIN payroll.branchoffice AS offi ON uf.branchofficeid = offi.id
	LEFT JOIN payroll.employeetype AS empt ON emp.employeetypeid = empt.id
	LEFT JOIN addresses AS [add] ON per.id = [add].idperson AND [add].rownumer = 1
	INNER JOIN payroll.contracttype AS cont ON con.contracttypeid = cont.id
	LEFT JOIN payroll.bank as bank ON con.bankid = bank.id
	LEFT JOIN payroll.relationship AS rsh ON emp.id = rsh.employeeid
	LEFT JOIN payroll.kinship AS kin ON rsh.kinshipid = kin.id
	LEFT JOIN payroll.positionlevel AS lev ON pos.positionlevelid = lev.id
	LEFT JOIN payroll.ethnicgroups AS eth ON per.ethnicgroupid = eth.id

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola fila por empleado la información maestra de talento humano (datos personales, contrato vigente, cargo, unidad funcional, fondos de seguridad social, dirección y familiar) para alimentar un cubo de reporte.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieHCMTHPersonnelList';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada empleado debe tener un registro en common.person, common.thirdparty y payroll.employee enlazados.; Debe existir al menos un contrato en payroll.contract con status=1 y valid=1 para que el empleado aparezca en el resultado.; Los catálogos referenciados (city, contracttype) deben existir para los IDs usados en INNER JOIN.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieHCMTHPersonnelList';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen contratos activos y válidos (status=1 AND valid=1).; Para cada persona se selecciona una única dirección de residencia (la primera por orden ascendente de id de persona, RowNumer=1).; Los fondos de seguridad social siempre se filtran por state=1 (vigentes).; Teléfono principal corresponde a phonetype.id=1 y alternativo a phonetype.id=2.; La edad se calcula en años completos desde la fecha de nacimiento hasta la fecha actual.; La antigüedad se expresa en meses desde la fecha de vinculación laboral.; El timestamp de actualización (ULT_ACTUAL) se calcula convirtiendo GETDATE() a la zona horaria ''Pakistan Standard Time''.; El ID de compañía corresponde al nombre de la base de datos truncado a 9 caracteres.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieHCMTHPersonnelList';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Empleado; Contrato laboral; Cargo y nivel de cargo; Unidad funcional clínica (Urgencias, UCI, Consulta Externa, etc.); Sede / Sucursal; Tipo de documento de identidad; Grupo sanguíneo y RH; Tarjeta profesional; Dirección de residencia; Fondos de seguridad social: Salud (EPS), Pensión, Cesantías, ARL, Caja de Compensación; Aportes voluntarios a salud y pensión; Porcentaje de riesgo profesional (ARL); Grupo étnico; Parentesco / familiar del empleado; Tipo de contrato; Cuenta bancaria de nómina', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieHCMTHPersonnelList';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] -: Devuelve un conjunto de resultados con un registro por empleado con contrato activo (status=1 AND valid=1), incluyendo dirección principal (RowNumer=1), datos demográficos, contractuales, fondos de seguridad social y un familiar.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieHCMTHPersonnelList';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si per.identificationtype IN (0..15) → Mapea cada código numérico a su etiqueta de tipo de documento (CC, CE, TI, RC, PA, AS, MS, NIT, NU, Cert. Nacido Vivo, CD, SC, PE, PT, DE, SI).; si uf.unittype IN (1..25) → Traduce el tipo numérico de unidad funcional a su descripción clínica (Urgencias, Hospitalización, UCI Adulto/Pediátrica/Neonatal, Consulta Externa, Cirugía, Laboratorio, etc.).; si per.gender = ''1'' / ''2'' / ''3'' → Etiqueta como Masculino / Femenino / Otro respectivamente.; si con.bankaccounttype = 1 / 2 → Clasifica la cuenta bancaria como Ahorros o Corriente.; si fundcontract.fundtype y voluntarycontribution → Selecciona el fondo según tipo: 1=Salud (obligatoria/voluntaria), 2=Pensión (obligatoria/voluntaria), 3=Cesantías, 4=ARL, 5=Caja de Compensación; siempre con state=1.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieHCMTHPersonnelList';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'common.address; common.department; common.city; common.person; common.thirdparty; payroll.employee; common.email; common.phone; common.phonetype; common.personstudy; payroll.studytype; payroll.profession; common.personprofession; payroll.fundcontract; payroll.fund; payroll.contract; payroll.group; payroll.position; payroll.functionalunit; payroll.branchoffice; payroll.employeetype; payroll.contracttype; payroll.bank; payroll.relationship; payroll.kinship; payroll.positionlevel; payroll.ethnicgroups', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieHCMTHPersonnelList';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieHCMTHPersonnelList';
GO
