


--CREATE PROCEDURE [dbo].[ODO_Nomina_Listado_Personal] 
--AS
--BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;

	
CREATE view [Report].[UploadCubeVieHCMPersonnelList] AS

	WITH addresses AS
	(
		SELECT 
			ROW_NUMBER() OVER(PARTITION BY per.id ORDER BY per.id ASC) AS [RowNumer],
			per.id idperson,
			UPPER([add].addresss) AS [Direccion de Residencia],
			dp.name AS [Departamento de Residencia],
			ct.name AS [Municipio de Residencia]
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
			WHEN 15 THEN 'SI - Sin Identificacion' END AS 'TIPO DOCUMENTO',-- [TipoDocumento],
		per.identificationnumber AS 'NRO DOCUMENTO',--[NroDocumento],
		citye.name AS 'MUNICIPIO EXPEDICION',--[MunicipioExpedicion],
		per.identificationexpeditiondate AS 'FECHA EXPEDICION',--[FechaExpedicion],
		thi.name AS 'NOOMBRE',--[Nombre],
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
		[add].[Direccion de Residencia] AS 'DIRECCION RESIDENCIA',--[DireccionResidencia],
		[add].[Departamento de Residencia] AS 'DEPARTAMNTO RESIDENCIA',--[DepartamentoResidencia],
		[add].[Municipio de Residencia] AS 'MUNICIPIO RESIDENCIA',--[MunicipioResidencia],
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
		emp.professionalriskpercentage AS 'PORCENTAJE ARL',--[PorcentajeARL]
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
	LEFT JOIN payroll.positionlevel AS lev ON pos.positionlevelid = lev.id
	LEFT JOIN payroll.ethnicgroups AS eth ON per.ethnicgroupid = eth.id