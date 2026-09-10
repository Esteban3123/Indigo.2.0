-- =============================================
-- Autor:		Juan David Patiño Cabrera
-- Fecha Creacion: 04-05-2018
-- Descripcion:	Sp que me lista la cabecera para todas las fichas del Sivigila que existen en Indigo
-- Modifico:    Yezid Garcia Medina
-- Fecha Modificación : 30-12-2021
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarCabeceraFichasSivigila]
(
  @IdFicha as Int,
  @Paciente as varchar,
  @Ingreso as Varchar,
  @VersionERP int  
)
AS
BEGIN
  SET NOCOUNT ON;

  declare @VersionCabecera As Varchar(20)
  declare @EsSivigila int  --1 = Sivigila 2 = Distrital

  set @VersionCabecera =  RTRIM( (Select Top 1 VERSION From dbo.HCFICHANOTIFICACION where ID = @IdFicha) )
  set @EsSivigila =  iif(RTRIM( (Select Top 1 CODEVENTO From dbo.HCFICHANOTIFICACION where ID = @IdFicha) ) = '875_D' OR RTRIM( (Select Top 1 CODEVENTO From dbo.HCFICHANOTIFICACION where ID = @IdFicha) ) = '356_D' OR RTRIM( (Select Top 1 CODEVENTO From dbo.HCFICHANOTIFICACION where ID = @IdFicha) ) = '903_D',0,1)
  If @EsSivigila = 1 --fICHAS SIVIGILA
  	IF @VersionCabecera = 'V10_2021-05-23' OR @VersionCabecera = 'V11_2022-06-08' OR @VersionCabecera = 'V12_2024-03-01'  ---  version cabecera = FOR-R02.0000-001 V:10 2021-05-23.
		BEGIN
			IF @VersionERP = 0 --Vie
				Select C.Id,UPGD As 'Codigo UPGD',Rtrim(RAZONSOCIAL) As 'Razon Social',Rtrim(NOMBEVENTO) As 'Nombre Evento', Rtrim(CODEVENTO) As 'Codigo Evento',convert(varchar(10),FECHANOTIFI,103) As 'Fecha Notificacion',Rtrim(C.NACIONALIDAD) As 'Nacionalidad',
					 'Estrato:' +' '+ CONVERT(VARCHAR,ESTRATO) AS 'ESTRATO',
					 CASE FUENTE WHEN 1 THEN 'Notificación rutinaria' WHEN 2 THEN 'Búsqueda activa Inst' WHEN 3 THEN 'Vigilancia Intensificada' WHEN 4 THEN 'Búsqueda activa com.' WHEN 5 THEN 'Investigaciones' END AS 'FUENTE', 
					 CASE TIPODOCUMENTO WHEN '1' THEN 'X' END AS 'Registro Civil', CASE TIPODOCUMENTO WHEN '2' THEN 'X' END AS 'Tarjeta Identidad', CASE TIPODOCUMENTO WHEN '3' THEN 'X' END AS 'Cedula', CASE TIPODOCUMENTO WHEN '4' THEN 'X' END AS 'Cedula Extranjera', 
					 CASE TIPODOCUMENTO WHEN '5' THEN 'X' END AS 'Pasaporte', CASE TIPODOCUMENTO WHEN '6' THEN 'X' END AS 'Menor sin Identificacion', CASE TIPODOCUMENTO WHEN '7' THEN 'X' END AS 'Adulto sin Identificacion', CASE TIPODOCUMENTO WHEN '8' THEN 'X' END AS 'Permiso Especial', 
					 CASE TIPODOCUMENTO WHEN '9' THEN 'X' END AS 'Certificado Nacido Vivo', CASE TIPODOCUMENTO WHEN '10' THEN 'X' END AS 'CarnetDiplomatico', CASE TIPODOCUMENTO WHEN '11' THEN 'X' END AS 'SalvoConducto', CASE TIPODOCUMENTO WHEN '13' THEN 'X' END AS 'DocumentoExtranjero',
					 CASE TIPODOCUMENTO  
						WHEN '1' THEN 'RC' WHEN '2' THEN 'TI' WHEN '3' THEN 'CC' WHEN '4' THEN 'CE' 
						WHEN '5' THEN 'PA' WHEN '6' THEN 'MS' WHEN '7' THEN 'AS' WHEN '8' THEN 'PE' 
						WHEN '9' THEN 'CN' WHEN '10' THEN 'CD' WHEN '11' THEN 'SC' WHEN '13' THEN 'DE' 
					 END AS 'TipoIdentificacion',
					  TI.NOMBRE AS TIPODOC,
					 --CASE TIPODOCUMENTO WHEN 1 THEN 'RC - Registro Civil' WHEN 2 THEN 'TI - Tarjeta de Identidad' WHEN 3 THEN 'CC - Cédula de Ciudadanía' WHEN 4 THEN 'CE - Cédula de Extranjería' WHEN 5 THEN 'PA - Pasaporte' WHEN 6 THEN 'MS - Menor Sin Identificación' WHEN 7 THEN 'AS - Adulto Sin Identificación' WHEN 8 THEN 'PE - Permiso Especial de Permanencia' WHEN 9 THEN 'CN - Certificado de Nacido Vivo' WHEN 10 THEN 'CD - Carnet Diplomático' WHEN 11 THEN 'SC - Salvoconducto' WHEN 12 THEN 'NU - Número único de identificación personal' WHEN 13 THEN 'DE - Documento Extranjero' WHEN 14 THEN 'PT - Permiso Temporal de Permanencia' WHEN 15 THEN 'SI - Sin Identificación' END AS TIPODOC,
					 Rtrim(IPNOMCOMP) AS 'Nombre paciente', Rtrim(I.IPCODPACI) As 'Identificacion',Rtrim(TELEFONO) As 'Telefono Paciente',IPFECNACI AS 'Fecha Nacimiento',EDAD As 'Edad',
					 CASE UNIDADMED  WHEN '1' THEN 'X' END AS 'Años',CASE UNIDADMED  WHEN '2' THEN 'X' END AS 'Meses',CASE UNIDADMED  WHEN '3' THEN 'X' END AS 'Dias',CASE UNIDADMED  WHEN '4' THEN 'X' END AS 'Horas',CASE UNIDADMED  WHEN '5' THEN 'X' END AS 'Minutos',CASE UNIDADMED  WHEN '6' THEN 'X' END AS 'No Aplica',
					 CASE SEXO  WHEN '1' THEN 'X' END AS 'Masculino',CASE SEXO  WHEN '2' THEN 'X' END AS 'Femenino',CASE SEXO  WHEN '3' THEN 'X' END AS 'Indeterminado',CODPAIS As 'Pais Ocurrencia',
					 CONCAT( RTRIM(E.depcodigo), '-', RTRIM(E.nomdepart), ' / ', RTRIM(P.MUNCODIGO), '-', RTRIM(P.MUNNOMBRE) ) as 'Departamento Municipio',
					 CASE AREA  WHEN '1' THEN 'X' END AS 'Cabecera municipal',CASE AREA  WHEN '2' THEN 'X' END AS 'Centro poblado',CASE AREA  WHEN '3' THEN 'X' END AS 'Rural disperso',
					 Rtrim(LOCALIDAD) As 'Localidad',Rtrim(BARRIO) As 'Barrio',Rtrim(CENTRORURAL) As 'Centro municipal', Rtrim(VEREDA) As 'Vereda Zona',Rtrim(A.codactivi) +'-'+ Rtrim(A.desactivi) As 'Ocupacion',
					 CASE TIPOREGIMEN WHEN '1' THEN 'X' END AS 'Exepción',CASE TIPOREGIMEN WHEN '2' THEN 'X' END AS 'Especial',CASE TIPOREGIMEN WHEN '3' THEN 'X' END AS 'Contributivo',CASE TIPOREGIMEN WHEN '4' THEN 'X' END AS 'Subsidiado',CASE TIPOREGIMEN WHEN '5' THEN 'X' END AS 'No asegurado',CASE TIPOREGIMEN WHEN '6' THEN 'X' END AS 'Indeterminado/pendiente',
					 CASE ETNIA WHEN '1' THEN 'X' END AS 'Indigena',CASE ETNIA WHEN '2' THEN 'X' END AS 'Rom gitano',CASE ETNIA WHEN '3' THEN 'X' END AS 'Raizal',CASE ETNIA WHEN '4' THEN 'X' END AS 'Palenquero',CASE ETNIA WHEN '5' THEN 'X' END AS 'Negro',CASE ETNIA WHEN '6' THEN 'X' END AS 'Otro',
					 Rtrim(heal.Code) +'-'+ Rtrim(heal.Name) As 'Nombre Entidad beneficios', 
					 CONCAT( Q.Code, '-', RTRIM(Q.StandardCode), '-', RTRIM(Q.Name), ' / ', RTRIM(O.depcodigo), '-', RTRIM(O.nomdepart), ' / ', RTRIM(R.MUNCODIGO), '-', RTRIM(R.MUNNOMBRE) ) as 'Residencia', 
					 Rtrim(DIRRESIDENCIA) As 'Direccion Residencia',convert(varchar(10),FECHACONSULTA,103) As 'Fecha Consulta',convert(varchar(10),FECHAINICOSINTO,103) As 'Fecha Inicio Sintomas',
					 CASE CLASIFICACIONCASO WHEN '1' THEN 'X' END AS 'Sospechoso',CASE CLASIFICACIONCASO WHEN '2' THEN 'X' END AS 'Probable',CASE CLASIFICACIONCASO WHEN '3' THEN 'X' END AS 'Conf laboratorio',CASE CLASIFICACIONCASO WHEN '4' THEN 'X' END AS 'Conf clinica',CASE CLASIFICACIONCASO WHEN '5' THEN 'X' END AS 'Conf epidemiológico',
					 CASE Rtrim(HOSPITALIZADO) WHEN '1' THEN 'X' END AS 'Si',CASE Rtrim(HOSPITALIZADO) WHEN '0' THEN 'X' END AS 'No',convert(varchar(10),FECHAHOSP,103) As 'Fecha Hospitalizacion', CASE CONDICIONFINAL WHEN '1' THEN 'X' END AS 'Vivo',CASE CONDICIONFINAL WHEN '2' THEN 'X' END AS 'Muerto',CASE CONDICIONFINAL WHEN '3' THEN 'X' END AS 'No Sabe', convert(varchar(10),FECHADIFUNCION,103) As 'Fecha Difuncion',Rtrim(NUMCERTIFICADO) As 'Numero Certificado',Rtrim(CAUSAMUERTE) As 'Causa Muerte',Rtrim(NOMPROFESIONAL) As 'Nombre Profesional', Rtrim(TELNOTIFICA) As 'Telefono Profesional',
					 [dbo].[TipoPoblacionSivigila] (@IdFicha,1) As 'Discapacitados',[dbo].[TipoPoblacionSivigila] (@IdFicha,2) As 'Desplazados',[dbo].[TipoPoblacionSivigila] (@IdFicha,3) As 'Migrantes',[dbo].[TipoPoblacionSivigila] (@IdFicha,4) As 'Carcelarios',[dbo].[TipoPoblacionSivigila] (@IdFicha,5) As 'Gestantes',[dbo].[TipoPoblacionSivigila] (@IdFicha,6) As 'Indigentes',[dbo].[TipoPoblacionSivigila] (@IdFicha,7) As 'Poblacion infaltil',[dbo].[TipoPoblacionSivigila] (@IdFicha,8) As 'Madres comunitarias',[dbo].[TipoPoblacionSivigila] (@IdFicha,9) As 'Desmovilizados',[dbo].[TipoPoblacionSivigila] (@IdFicha,10) As 'Centros',[dbo].[TipoPoblacionSivigila] (@IdFicha,11) As 'Victimas',[dbo].[TipoPoblacionSivigila] (@IdFicha,12) As 'Otros',
					 SEMANASGESTACION as 'Semanas Gestacion', C.VERSION AS 'VERSION', C.JSON AS 'JSON', 
					 RTRIM( JSON_VALUE(JSON,'$.CUALETNIA') ) As 'CUALETNIA', JSON_VALUE(JSON,'$.IdentificacionGenero') As 'IdentificacionGenero', RTRIM(JSON_VALUE(JSON,'$.CualGenero')) As 'CualGenero', JSON_VALUE(JSON,'$.OrientacionSexual') As 'OrientacionSexual', RTRIM(JSON_VALUE(JSON,'$.CualOrientacionSex')) As 'CualOrientacionSex',
					   CONCAT(
                       QN.StandardCodeNumeric, ' - ', QN.Name
                       ) AS NombreNacionalidad,
					  CONCAT(
                      QB.UBICODIGO, ' - ', QB.UBINOMBRE
                      ) AS NombreBarrio, CONCAT(
                      QV.UBICODIGO, ' - ', QV.UBINOMBRE
                      ) AS NombreVereda
				From dbo.HCFICHANOTIFICACION C
					Inner Join dbo.INPACIENT I on C.IPCODPACI = I.IPCODPACI 
					INNER JOIN dbo.ADTIPOIDENTIFICA TI ON TI.CODIGO=C.TIPODOCUMENTO
					Inner join dbo.INUbicaci U on C.CODDEPARTAMENTO = U.AUUBICACI
					Inner Join dbo.INMUNICIP P on U.DEPMUNCOD = P.DEPMUNCOD
					Inner Join dbo.INDEPARTA E on P.DEPCODIGO = E.DEPCODIGO 
					Left Join dbo.ADACTIVID A on C.OCUPACION = A.codactivi
					inner join Contract.HealthAdministrator heal on heal.Id = C.ADMINPLANBENE
					Inner join dbo.INUbicaci M on C.RESIDENCIA = M.AUUBICACI
					Inner Join dbo.INMUNICIP R on M.DEPMUNCOD = R.DEPMUNCOD
					Inner Join dbo.INDEPARTA O on R.DEPCODIGO = O.DEPCODIGO 
					LEFT JOIN Common.Country Q ON O.IDPAIS = Q.Id 
					LEFT JOIN Common.Country QN ON QN.Id = TRY_CAST(JSON_VALUE(C.JSON,'$.Nationality') AS INT)
					LEFT JOIN dbo.INUbicaci QB ON RTRIM(QB.UBICODIGO) = NULLIF(LTRIM(RTRIM(JSON_VALUE(C.JSON,'$.Neighborhood'))), '') AND C.CODDEPARTAMENTO = QB.AUUBICACI
					LEFT JOIN dbo.INUbicaci QV ON RTRIM(QV.UBICODIGO) = NULLIF(LTRIM(RTRIM(JSON_VALUE(C.JSON,'$.Sidewalk'))), '') AND C.CODDEPARTAMENTO = QV.AUUBICACI
					Where C.ID = @IdFicha
			ELSE  --Otro ERP
			  Select C.Id,UPGD As 'Codigo UPGD',Rtrim(RAZONSOCIAL) As 'Razon Social',Rtrim(NOMBEVENTO) As 'Nombre Evento', Rtrim(CODEVENTO) As 'Codigo Evento',convert(varchar(10),FECHANOTIFI,103) As 'Fecha Notificacion',Rtrim(C.NACIONALIDAD) As 'Nacionalidad',
					'Estrato:' +' '+ CONVERT(VARCHAR,ESTRATO) AS 'ESTRATO',
					 CASE FUENTE WHEN 1 THEN 'Notificación rutinaria' WHEN 2 THEN 'Búsqueda activa Inst' WHEN 3 THEN 'Vigilancia Intensificada' WHEN 4 THEN 'Búsqueda activa com.' WHEN 5 THEN 'Investigaciones' END AS 'FUENTE',
					 CASE TIPODOCUMENTO WHEN '1' THEN 'X' END AS 'Registro Civil', CASE TIPODOCUMENTO WHEN '2' THEN 'X' END AS 'Tarjeta Identidad', CASE TIPODOCUMENTO WHEN '3' THEN 'X' END AS 'Cedula', CASE TIPODOCUMENTO WHEN '4' THEN 'X' END AS 'Cedula Extranjera', 
					 CASE TIPODOCUMENTO WHEN '5' THEN 'X' END AS 'Pasaporte', CASE TIPODOCUMENTO WHEN '6' THEN 'X' END AS 'Menor sin Identificacion', CASE TIPODOCUMENTO WHEN '7' THEN 'X' END AS 'Adulto sin Identificacion', CASE TIPODOCUMENTO WHEN '8' THEN 'X' END AS 'Permiso Especial', 
					 CASE TIPODOCUMENTO WHEN '9' THEN 'X' END AS 'Certificado Nacido Vivo', CASE TIPODOCUMENTO WHEN '10' THEN 'X' END AS 'CarnetDiplomatico', CASE TIPODOCUMENTO WHEN '11' THEN 'X' END AS 'SalvoConducto', CASE TIPODOCUMENTO WHEN '13' THEN 'X' END AS 'DocumentoExtranjero',
					 CASE TIPODOCUMENTO  
						WHEN '1' THEN 'RC' WHEN '2' THEN 'TI' WHEN '3' THEN 'CC' WHEN '4' THEN 'CE' 
						WHEN '5' THEN 'PA' WHEN '6' THEN 'MS' WHEN '7' THEN 'AS' WHEN '8' THEN 'PE' 
						WHEN '9' THEN 'CN' WHEN '10' THEN 'CD' WHEN '11' THEN 'SC' WHEN '13' THEN 'DE' 
					 END AS 'TipoIdentificacion',
					  TI.NOMBRE AS TIPODOC,
					 ----CASE TIPODOCUMENTO WHEN 1 THEN 'RC - Registro Civil' WHEN 2 THEN 'TI - Tarjeta de Identidad' WHEN 3 THEN 'CC - Cédula de Ciudadanía' WHEN 4 THEN 'CE - Cédula de Extranjería' WHEN 5 THEN 'PA - Pasaporte' WHEN 6 THEN 'MS - Menor Sin Identificación' WHEN 7 THEN 'AS - Adulto Sin Identificación' WHEN 8 THEN 'PE - Permiso Especial de Permanencia' WHEN 9 THEN 'CN - Certificado de Nacido Vivo' WHEN 10 THEN 'CD - Carnet Diplomático' WHEN 11 THEN 'SC - Salvoconducto' WHEN 12 THEN 'NU - Número único de identificación personal' WHEN 13 THEN 'DE - Documento Extranjero' WHEN 14 THEN 'PT - Permiso Temporal de Permanencia' WHEN 15 THEN 'SI - Sin Identificación' END AS TIPODOC,
					 Rtrim(IPNOMCOMP) AS 'Nombre paciente', Rtrim(I.IPCODPACI) As 'Identificacion',Rtrim(TELEFONO) As 'Telefono Paciente',convert(varchar(10),IPFECNACI) AS 'Fecha Nacimiento',EDAD As 'Edad',
					 CASE UNIDADMED  WHEN '1' THEN 'X' END AS 'Años',CASE UNIDADMED  WHEN '2' THEN 'X' END AS 'Dias',CASE UNIDADMED  WHEN '3' THEN 'X' END AS 'Minutos',CASE UNIDADMED  WHEN '4' THEN 'X' END AS 'Meses',CASE UNIDADMED  WHEN '5' THEN 'X' END AS 'Horas',CASE UNIDADMED  WHEN '6' THEN 'X' END AS 'No Aplica',
					 CASE SEXO  WHEN '1' THEN 'X' END AS 'Masculino',CASE SEXO  WHEN '2' THEN 'X' END AS 'Femenino',CASE SEXO  WHEN '3' THEN 'X' END AS 'Indeterminado',CODPAIS As 'Pais Ocurrencia',
					 CONCAT( RTRIM(E.depcodigo), '-', RTRIM(E.nomdepart), ' / ', RTRIM(P.MUNCODIGO), '-', RTRIM(P.MUNNOMBRE) ) as 'Departamento Municipio',
					 CASE AREA  WHEN '1' THEN 'X' END AS 'Cabecera municipal',CASE AREA  WHEN '2' THEN 'X' END AS 'Centro poblado',CASE AREA  WHEN '3' THEN 'X' END AS 'Rural disperso',
					 Rtrim(LOCALIDAD) As 'Localidad',Rtrim(BARRIO) As 'Barrio',Rtrim(CENTRORURAL) As 'Centro municipal', Rtrim(VEREDA) As 'Vereda Zona',Rtrim(A.codactivi) +'-'+ Rtrim(A.desactivi) As 'Ouapcion',
					 CASE TIPOREGIMEN WHEN '1' THEN 'X' END AS 'Exepción',CASE TIPOREGIMEN WHEN '2' THEN 'X' END AS 'Especial',CASE TIPOREGIMEN WHEN '3' THEN 'X' END AS 'Contributivo',CASE TIPOREGIMEN WHEN '4' THEN 'X' END AS 'Subsidiado',CASE TIPOREGIMEN WHEN '5' THEN 'X' END AS 'No asegurado',CASE TIPOREGIMEN WHEN '6' THEN 'X' END AS 'Indeterminado/pendiente',
					 CASE ETNIA WHEN '1' THEN 'X' END AS 'Indígena',CASE ETNIA WHEN '2' THEN 'X' END AS 'Rom gitano',CASE ETNIA WHEN '3' THEN 'X' END AS 'Raizal',CASE ETNIA WHEN '4' THEN 'X' END AS 'Palenquero',CASE ETNIA WHEN '5' THEN 'X' END AS 'Negro',CASE ETNIA WHEN '6' THEN 'X' END AS 'Otro',
					 Rtrim(CODENTADM) +'-'+ Rtrim(NOMENTADM) As 'Nombre Entidad beneficios', 
					 CONCAT( '170', '-', 'COLOMBIA', ' / ', RTRIM(O.depcodigo), '-', RTRIM(O.nomdepart), ' / ', RTRIM(R.MUNCODIGO), '-', RTRIM(R.MUNNOMBRE) ) as 'Residencia', 
					 Rtrim(DIRRESIDENCIA) As 'Direccion Residencia',convert(varchar(10),FECHACONSULTA,103) As 'Fecha Consulta',convert(varchar(10),FECHAINICOSINTO,103) As 'Fecha Inicio Sintomas',
					 CASE CLASIFICACIONCASO WHEN '1' THEN 'X' END AS 'Sospechoso',CASE CLASIFICACIONCASO WHEN '2' THEN 'X' END AS 'Probable',CASE CLASIFICACIONCASO WHEN '3' THEN 'X' END AS 'Conf laboratorio',CASE CLASIFICACIONCASO WHEN '4' THEN 'X' END AS 'Conf clinica',CASE CLASIFICACIONCASO WHEN '5' THEN 'X' END AS 'Conf epidemiológico',
					 CASE Rtrim(HOSPITALIZADO) WHEN '1' THEN 'X' END AS 'Si',CASE Rtrim(HOSPITALIZADO) WHEN '2' THEN 'X' END AS 'No',convert(varchar(10),FECHAHOSP,103) As 'Fecha Hospitalizacion', CASE CONDICIONFINAL WHEN '1' THEN 'X' END AS 'Vivo',CASE CONDICIONFINAL WHEN '2' THEN 'X' END AS 'Muerto',CASE CONDICIONFINAL WHEN '3' THEN 'X' END AS 'No Sabe', convert(varchar(10),FECHADIFUNCION,103) As 'Fecha Difuncion',Rtrim(NUMCERTIFICADO) As 'Numero Certificado',Rtrim(CAUSAMUERTE) As 'Causa Muerte',Rtrim(NOMPROFESIONAL) As 'Nombre Profesional', Rtrim(TELNOTIFICA) As 'Telefono Profesional',
					 [dbo].[TipoPoblacionSivigila] (@IdFicha,1) As 'Discapacitados',[dbo].[TipoPoblacionSivigila] (@IdFicha,2) As 'Desplazados',[dbo].[TipoPoblacionSivigila] (@IdFicha,3) As 'Migrantes',[dbo].[TipoPoblacionSivigila] (@IdFicha,4) As 'Carcelarios',[dbo].[TipoPoblacionSivigila] (@IdFicha,5) As 'Gestantes',[dbo].[TipoPoblacionSivigila] (@IdFicha,6) As 'Indigentes',[dbo].[TipoPoblacionSivigila] (@IdFicha,7) As 'Poblacion infaltil',[dbo].[TipoPoblacionSivigila] (@IdFicha,8) As 'Madres comunitarias',[dbo].[TipoPoblacionSivigila] (@IdFicha,9) As 'Desmovilizados',[dbo].[TipoPoblacionSivigila] (@IdFicha,10) As 'Centros',[dbo].[TipoPoblacionSivigila] (@IdFicha,11) As 'Victimas',[dbo].[TipoPoblacionSivigila] (@IdFicha,12) As 'Otros',
					 SEMANASGESTACION as 'Semanas Gestacion', C.VERSION AS 'VERSION', C.JSON AS 'JSON', 
					 RTRIM( JSON_VALUE(JSON,'$.CUALETNIA') ) As 'CUALETNIA', JSON_VALUE(JSON,'$.IdentificacionGenero') As 'IdentificacionGenero', RTRIM(JSON_VALUE(JSON,'$.CualGenero')) As 'CualGenero', JSON_VALUE(JSON,'$.OrientacionSexual') As 'OrientacionSexual', RTRIM(JSON_VALUE(JSON,'$.CualOrientacionSex')) As 'CualOrientacionSex',
					   CONCAT(
                       QN.StandardCodeNumeric, ' - ', QN.Name
                       ) AS NombreNacionalidad, CONCAT(
                      QB.UBICODIGO, ' - ', QB.UBINOMBRE
                      ) AS NombreBarrio, CONCAT(
                      QV.UBICODIGO, ' - ', QV.UBINOMBRE
                      ) AS NombreVereda
				From dbo.HCFICHANOTIFICACION C 
					Inner Join dbo.INPACIENT I on C.IPCODPACI = I.IPCODPACI 
					INNER JOIN dbo.ADTIPOIDENTIFICA TI ON TI.CODIGO=C.TIPODOCUMENTO
					Inner join dbo.INUbicaci U on C.CODDEPARTAMENTO = U.AUUBICACI
					Inner Join dbo.INMUNICIP P on U.DEPMUNCOD = P.DEPMUNCOD
					Inner Join dbo.INDEPARTA E on P.DEPCODIGO = E.DEPCODIGO 
					Left Join dbo.ADACTIVID A on C.OCUPACION = A.codactivi
					INNER join dbo.INENTADM	X on X.CODENTADM = C.CODENTIDA
					Inner join dbo.INUbicaci M on C.RESIDENCIA = M.AUUBICACI
					Inner Join dbo.INMUNICIP R on M.DEPMUNCOD = R.DEPMUNCOD
					Inner Join dbo.INDEPARTA O on R.DEPCODIGO = O.DEPCODIGO
					LEFT JOIN Common.Country QN ON QN.Id = TRY_CAST(JSON_VALUE(C.JSON,'$.Nationality') AS INT)
					LEFT JOIN dbo.INUbicaci QB ON RTRIM(QB.UBICODIGO) = NULLIF(LTRIM(RTRIM(JSON_VALUE(C.JSON,'$.Neighborhood'))), '') AND C.CODDEPARTAMENTO = QB.AUUBICACI
					LEFT JOIN dbo.INUbicaci QV ON RTRIM(QV.UBICODIGO) = NULLIF(LTRIM(RTRIM(JSON_VALUE(C.JSON,'$.Sidewalk'))), '') AND C.CODDEPARTAMENTO = QV.AUUBICACI
		END
	ELSE 
		BEGIN 
			IF @VersionCabecera = 'V09_2020-03-06'  ---  version cabecera = FOR-R02.0000-001 V:09 2020-03-06.
				BEGIN
					IF @VersionERP = 0 --Vie

						Select C.Id,UPGD As 'Codigo UPGD',Rtrim(RAZONSOCIAL) As 'Razon Social',Rtrim(NOMBEVENTO) As 'Nombre Evento', Rtrim(CODEVENTO) As 'Codigo Evento',convert(varchar(10),FECHANOTIFI,103) As 'Fecha Notificacion',Rtrim(C.NACIONALIDAD) As 'Nacionalidad',
							 'Estrato:' +' '+ CONVERT(VARCHAR,ESTRATO) AS 'ESTRATO',
							CASE FUENTE WHEN 1 THEN 'Notificación rutinaria' WHEN 2 THEN 'Búsqueda activa Inst' WHEN 3 THEN 'Vigilancia Intensificada' WHEN 4 THEN 'Búsqueda activa com.' WHEN 5 THEN 'Investigaciones' END AS 'FUENTE',
							 CASE TIPODOCUMENTO  WHEN '1' THEN 'X' END AS 'Registro Civil',CASE TIPODOCUMENTO WHEN '2' THEN 'X' END AS 'Tarjeta Identidad',CASE TIPODOCUMENTO WHEN '3' THEN 'X' END AS 'Cedula',CASE TIPODOCUMENTO WHEN '4' THEN 'X' END AS 'Cedula Extranjera',CASE TIPODOCUMENTO WHEN '5' THEN 'X' END AS 'Pasaporte',CASE TIPODOCUMENTO WHEN '6' THEN 'X' END AS 'Menor sin Identificacion',CASE TIPODOCUMENTO WHEN '7' THEN 'X' END AS 'Adulto sin Identificacion', CASE TIPODOCUMENTO WHEN '8' THEN 'X' END AS 'Permiso Especial', CASE TIPODOCUMENTO WHEN '9' THEN 'X' END AS 'Certificado Nacido Vivo',
							 CASE TIPODOCUMENTO  WHEN '1' THEN 'RC' WHEN '2' THEN 'TI' WHEN '3' THEN 'CC' WHEN '4' THEN 'CE' WHEN '5' THEN 'PA' WHEN '6' THEN 'MS' WHEN '7' THEN 'AS' WHEN '8' THEN 'PE' WHEN '9' THEN 'CN' END AS 'TipoIdentificacion',
							 TI.NOMBRE AS TIPODOC,							 
							 ---CASE TIPODOCUMENTO WHEN 1 THEN 'RC - Registro Civil' WHEN 2 THEN 'TI - Tarjeta de Identidad' WHEN 3 THEN 'CC - Cédula de Ciudadanía' WHEN 4 THEN 'CE - Cédula de Extranjería' WHEN 5 THEN 'PA - Pasaporte' WHEN 6 THEN 'MS - Menor Sin Identificación' WHEN 7 THEN 'AS - Adulto Sin Identificación' WHEN 8 THEN 'PE - Permiso Especial de Permanencia' WHEN 9 THEN 'CN - Certificado de Nacido Vivo' WHEN 10 THEN 'CD - Carnet Diplomático' WHEN 11 THEN 'SC - Salvoconducto' WHEN 12 THEN 'NU - Número único de identificación personal' WHEN 13 THEN 'DE - Documento Extranjero' WHEN 14 THEN 'PT - Permiso Temporal de Permanencia' WHEN 15 THEN 'SI - Sin Identificación' END AS TIPODOC,
							 Rtrim(IPNOMCOMP) AS 'Nombre paciente', Rtrim(I.IPCODPACI) As 'Identificacion',Rtrim(TELEFONO) As 'Telefono Paciente',IPFECNACI AS 'Fecha Nacimiento',EDAD As 'Edad',
							 CASE UNIDADMED  WHEN '1' THEN 'X' END AS 'Años',CASE UNIDADMED  WHEN '2' THEN 'X' END AS 'Meses',CASE UNIDADMED  WHEN '3' THEN 'X' END AS 'Dias',CASE UNIDADMED  WHEN '4' THEN 'X' END AS 'Horas',CASE UNIDADMED  WHEN '5' THEN 'X' END AS 'Minutos',CASE UNIDADMED  WHEN '6' THEN 'X' END AS 'No Aplica',
							 CASE SEXO  WHEN '1' THEN 'X' END AS 'Masculino',CASE SEXO  WHEN '2' THEN 'X' END AS 'Femenino',CASE SEXO  WHEN '3' THEN 'X' END AS 'Indeterminado',CODPAIS As 'Pais Ocurrencia',
							 CONCAT( RTRIM(E.depcodigo), '-', RTRIM(E.nomdepart), ' / ', RTRIM(P.MUNCODIGO), '-', RTRIM(P.MUNNOMBRE) ) as 'Departamento Municipio',
							 CASE AREA  WHEN '1' THEN 'X' END AS 'Cabecera municipal',CASE AREA  WHEN '2' THEN 'X' END AS 'Centro poblado',CASE AREA  WHEN '3' THEN 'X' END AS 'Rural disperso',
							 Rtrim(LOCALIDAD) As 'Localidad',Rtrim(BARRIO) As 'Barrio',Rtrim(CENTRORURAL) As 'Centro municipal', Rtrim(VEREDA) As 'Vereda Zona',Rtrim(A.codactivi) +'-'+ Rtrim(A.desactivi) As 'Ocupacion',
							 CASE TIPOREGIMEN WHEN '1' THEN 'X' END AS 'Exepción',CASE TIPOREGIMEN WHEN '2' THEN 'X' END AS 'Especial',CASE TIPOREGIMEN WHEN '3' THEN 'X' END AS 'Contributivo',CASE TIPOREGIMEN WHEN '4' THEN 'X' END AS 'Subsidiado',CASE TIPOREGIMEN WHEN '5' THEN 'X' END AS 'No asegurado',CASE TIPOREGIMEN WHEN '6' THEN 'X' END AS 'Indeterminado/pendiente',
							 CASE ETNIA WHEN '1' THEN 'X' END AS 'Indigena',CASE ETNIA WHEN '2' THEN 'X' END AS 'Rom gitano',CASE ETNIA WHEN '3' THEN 'X' END AS 'Raizal',CASE ETNIA WHEN '4' THEN 'X' END AS 'Palenquero',CASE ETNIA WHEN '5' THEN 'X' END AS 'Negro',CASE ETNIA WHEN '6' THEN 'X' END AS 'Otro',
							 Rtrim(heal.Code) +'-'+ Rtrim(heal.Name) As 'Nombre Entidad beneficios', 
							 CONCAT( RTRIM(Q.StandardCode), '-', RTRIM(Q.Name), ' / ', RTRIM(O.depcodigo), '-', RTRIM(O.nomdepart), ' / ', RTRIM(R.MUNCODIGO), '-', RTRIM(R.MUNNOMBRE) ) as 'Residencia', 
							 Rtrim(DIRRESIDENCIA) As 'Direccion Residencia',convert(varchar(10),FECHACONSULTA,103) As 'Fecha Consulta',convert(varchar(10),FECHAINICOSINTO,103) As 'Fecha Inicio Sintomas',
							 CASE CLASIFICACIONCASO WHEN '1' THEN 'X' END AS 'Sospechoso',CASE CLASIFICACIONCASO WHEN '2' THEN 'X' END AS 'Probable',CASE CLASIFICACIONCASO WHEN '3' THEN 'X' END AS 'Conf laboratorio',CASE CLASIFICACIONCASO WHEN '4' THEN 'X' END AS 'Conf clinica',CASE CLASIFICACIONCASO WHEN '5' THEN 'X' END AS 'Conf epidemiológico',
							 CASE Rtrim(HOSPITALIZADO) WHEN '1' THEN 'X' END AS 'Si',CASE Rtrim(HOSPITALIZADO) WHEN '0' THEN 'X' END AS 'No',convert(varchar(10),FECHAHOSP,103) As 'Fecha Hospitalizacion', CASE CONDICIONFINAL WHEN '1' THEN 'X' END AS 'Vivo',CASE CONDICIONFINAL WHEN '2' THEN 'X' END AS 'Muerto',CASE CONDICIONFINAL WHEN '3' THEN 'X' END AS 'No Sabe', convert(varchar(10),FECHADIFUNCION,103) As 'Fecha Difuncion',Rtrim(NUMCERTIFICADO) As 'Numero Certificado',Rtrim(CAUSAMUERTE) As 'Causa Muerte',Rtrim(NOMPROFESIONAL) As 'Nombre Profesional', Rtrim(TELNOTIFICA) As 'Telefono Profesional',
							 [dbo].[TipoPoblacionSivigila] (@IdFicha,1) As 'Discapacitados',[dbo].[TipoPoblacionSivigila] (@IdFicha,2) As 'Desplazados',[dbo].[TipoPoblacionSivigila] (@IdFicha,3) As 'Migrantes',[dbo].[TipoPoblacionSivigila] (@IdFicha,4) As 'Carcelarios',[dbo].[TipoPoblacionSivigila] (@IdFicha,5) As 'Gestantes',[dbo].[TipoPoblacionSivigila] (@IdFicha,6) As 'Indigentes',[dbo].[TipoPoblacionSivigila] (@IdFicha,7) As 'Poblacion infaltil',[dbo].[TipoPoblacionSivigila] (@IdFicha,8) As 'Madres comunitarias',[dbo].[TipoPoblacionSivigila] (@IdFicha,9) As 'Desmovilizados',[dbo].[TipoPoblacionSivigila] (@IdFicha,10) As 'Centros',[dbo].[TipoPoblacionSivigila] (@IdFicha,11) As 'Victimas',[dbo].[TipoPoblacionSivigila] (@IdFicha,12) As 'Otros',
							 SEMANASGESTACION as 'Semanas Gestacion', C.VERSION AS 'VERSION' , C.JSON AS 'JSON'
						From dbo.HCFICHANOTIFICACION C
							Inner Join dbo.INPACIENT I on C.IPCODPACI = I.IPCODPACI 
							INNER JOIN dbo.ADTIPOIDENTIFICA TI ON TI.CODIGO=C.TIPODOCUMENTO
							Inner join dbo.INUbicaci U on C.CODDEPARTAMENTO = U.AUUBICACI
							Inner Join dbo.INMUNICIP P on U.DEPMUNCOD = P.DEPMUNCOD
							Inner Join dbo.INDEPARTA E on P.DEPCODIGO = E.DEPCODIGO 
							Left Join dbo.ADACTIVID A on C.OCUPACION = A.codactivi
							inner join Contract.HealthAdministrator heal on heal.Id = C.ADMINPLANBENE
							Inner join dbo.INUbicaci M on C.RESIDENCIA = M.AUUBICACI
							Inner Join dbo.INMUNICIP R on M.DEPMUNCOD = R.DEPMUNCOD
							Inner Join dbo.INDEPARTA O on R.DEPCODIGO = O.DEPCODIGO 
							LEFT JOIN Common.Country Q ON O.IDPAIS = Q.Id 
							Where C.ID = @IdFicha

					ELSE  --Otro ERP
						Select C.Id,UPGD As 'Codigo UPGD',Rtrim(RAZONSOCIAL) As 'Razon Social',Rtrim(NOMBEVENTO) As 'Nombre Evento', Rtrim(CODEVENTO) As 'Codigo Evento',convert(varchar(10),FECHANOTIFI,103) As 'Fecha Notificacion',Rtrim(C.NACIONALIDAD) As 'Nacionalidad',
							'Estrato:' +' '+ CONVERT(VARCHAR,ESTRATO) AS 'ESTRATO',
							 CASE FUENTE WHEN 1 THEN 'Notificación rutinaria' WHEN 2 THEN 'Búsqueda activa Inst' WHEN 3 THEN 'Vigilancia Intensificada' WHEN 4 THEN 'Búsqueda activa com.' WHEN 5 THEN 'Investigaciones' END AS 'FUENTE',
							 CASE TIPODOCUMENTO  WHEN '1' THEN 'X' END AS 'Registro Civil',CASE TIPODOCUMENTO WHEN '2' THEN 'X' END AS 'Tarjeta Identidad',CASE TIPODOCUMENTO WHEN '3' THEN 'X' END AS 'Cedula',CASE TIPODOCUMENTO WHEN '4' THEN 'X' END AS 'Cedula Extranjera',CASE TIPODOCUMENTO WHEN '5' THEN 'X' END AS 'Pasaporte',CASE TIPODOCUMENTO WHEN '6' THEN 'X' END AS 'Menor sin Identificacion',CASE TIPODOCUMENTO WHEN '7' THEN 'X' END AS 'Adulto sin Identificacion', CASE TIPODOCUMENTO WHEN '8' THEN 'X' END AS 'Permiso Especial', CASE TIPODOCUMENTO WHEN '9' THEN 'X' END AS 'Certificado Nacido Vivo',
							 CASE TIPODOCUMENTO  WHEN '1' THEN 'RC' WHEN '2' THEN 'TI' WHEN '3' THEN 'CC' WHEN '4' THEN 'CE' WHEN '5' THEN 'PA' WHEN '6' THEN 'MS' WHEN '7' THEN 'AS' WHEN '8' THEN 'PE' WHEN '9' THEN 'CN' END AS 'TipoIdentificacion',
							  TI.NOMBRE AS TIPODOC,						 
							 ---CASE TIPODOCUMENTO WHEN 1 THEN 'RC - Registro Civil' WHEN 2 THEN 'TI - Tarjeta de Identidad' WHEN 3 THEN 'CC - Cédula de Ciudadanía' WHEN 4 THEN 'CE - Cédula de Extranjería' WHEN 5 THEN 'PA - Pasaporte' WHEN 6 THEN 'MS - Menor Sin Identificación' WHEN 7 THEN 'AS - Adulto Sin Identificación' WHEN 8 THEN 'PE - Permiso Especial de Permanencia' WHEN 9 THEN 'CN - Certificado de Nacido Vivo' WHEN 10 THEN 'CD - Carnet Diplomático' WHEN 11 THEN 'SC - Salvoconducto' WHEN 12 THEN 'NU - Número único de identificación personal' WHEN 13 THEN 'DE - Documento Extranjero' WHEN 14 THEN 'PT - Permiso Temporal de Permanencia' WHEN 15 THEN 'SI - Sin Identificación' END AS TIPODOC,
							 Rtrim(IPNOMCOMP) AS 'Nombre paciente', Rtrim(I.IPCODPACI) As 'Identificacion',Rtrim(TELEFONO) As 'Telefono Paciente',convert(varchar(10),IPFECNACI) AS 'Fecha Nacimiento',EDAD As 'Edad',
							 CASE UNIDADMED  WHEN '1' THEN 'X' END AS 'Años',CASE UNIDADMED  WHEN '2' THEN 'X' END AS 'Dias',CASE UNIDADMED  WHEN '3' THEN 'X' END AS 'Minutos',CASE UNIDADMED  WHEN '4' THEN 'X' END AS 'Meses',CASE UNIDADMED  WHEN '5' THEN 'X' END AS 'Horas',CASE UNIDADMED  WHEN '6' THEN 'X' END AS 'No Aplica',
							 CASE SEXO  WHEN '1' THEN 'X' END AS 'Masculino',CASE SEXO  WHEN '2' THEN 'X' END AS 'Femenino',CASE SEXO  WHEN '3' THEN 'X' END AS 'Indeterminado',CODPAIS As 'Pais Ocurrencia',
							 CONCAT( RTRIM(E.depcodigo), '-', RTRIM(E.nomdepart), ' / ', RTRIM(P.MUNCODIGO), '-', RTRIM(P.MUNNOMBRE) ) as 'Departamento Municipio',
							 CASE AREA  WHEN '1' THEN 'X' END AS 'Cabecera municipal',CASE AREA  WHEN '2' THEN 'X' END AS 'Centro poblado',CASE AREA  WHEN '3' THEN 'X' END AS 'Rural disperso',
							 Rtrim(LOCALIDAD) As 'Localidad',Rtrim(BARRIO) As 'Barrio',Rtrim(CENTRORURAL) As 'Centro municipal', Rtrim(VEREDA) As 'Vereda Zona',Rtrim(A.codactivi) +'-'+ Rtrim(A.desactivi) As 'Ouapcion',
							 CASE TIPOREGIMEN WHEN '1' THEN 'X' END AS 'Exepción',CASE TIPOREGIMEN WHEN '2' THEN 'X' END AS 'Especial',CASE TIPOREGIMEN WHEN '3' THEN 'X' END AS 'Contributivo',CASE TIPOREGIMEN WHEN '4' THEN 'X' END AS 'Subsidiado',CASE TIPOREGIMEN WHEN '5' THEN 'X' END AS 'No asegurado',CASE TIPOREGIMEN WHEN '6' THEN 'X' END AS 'Indeterminado/pendiente',
							 CASE ETNIA WHEN '1' THEN 'X' END AS 'Indígena',CASE ETNIA WHEN '2' THEN 'X' END AS 'Rom gitano',CASE ETNIA WHEN '3' THEN 'X' END AS 'Raizal',CASE ETNIA WHEN '4' THEN 'X' END AS 'Palenquero',CASE ETNIA WHEN '5' THEN 'X' END AS 'Negro',CASE ETNIA WHEN '6' THEN 'X' END AS 'Otro',
							 Rtrim(CODENTADM) +'-'+ Rtrim(NOMENTADM) As 'Nombre Entidad beneficios', 
							 CONCAT( '170', '-', 'COLOMBIA', ' / ', RTRIM(O.depcodigo), '-', RTRIM(O.nomdepart), ' / ', RTRIM(R.MUNCODIGO), '-', RTRIM(R.MUNNOMBRE) ) as 'Residencia', 
							 Rtrim(DIRRESIDENCIA) As 'Direccion Residencia',convert(varchar(10),FECHACONSULTA,103) As 'Fecha Consulta',convert(varchar(10),FECHAINICOSINTO,103) As 'Fecha Inicio Sintomas',
							 CASE CLASIFICACIONCASO WHEN '1' THEN 'X' END AS 'Sospechoso',CASE CLASIFICACIONCASO WHEN '2' THEN 'X' END AS 'Probable',CASE CLASIFICACIONCASO WHEN '3' THEN 'X' END AS 'Conf laboratorio',CASE CLASIFICACIONCASO WHEN '4' THEN 'X' END AS 'Conf clinica',CASE CLASIFICACIONCASO WHEN '5' THEN 'X' END AS 'Conf epidemiológico',
							 CASE Rtrim(HOSPITALIZADO) WHEN '1' THEN 'X' END AS 'Si',CASE Rtrim(HOSPITALIZADO) WHEN '2' THEN 'X' END AS 'No',convert(varchar(10),FECHAHOSP,103) As 'Fecha Hospitalizacion', CASE CONDICIONFINAL WHEN '1' THEN 'X' END AS 'Vivo',CASE CONDICIONFINAL WHEN '2' THEN 'X' END AS 'Muerto',CASE CONDICIONFINAL WHEN '3' THEN 'X' END AS 'No Sabe', convert(varchar(10),FECHADIFUNCION,103) As 'Fecha Difuncion',Rtrim(NUMCERTIFICADO) As 'Numero Certificado',Rtrim(CAUSAMUERTE) As 'Causa Muerte',Rtrim(NOMPROFESIONAL) As 'Nombre Profesional', Rtrim(TELNOTIFICA) As 'Telefono Profesional',
							 [dbo].[TipoPoblacionSivigila] (@IdFicha,1) As 'Discapacitados',[dbo].[TipoPoblacionSivigila] (@IdFicha,2) As 'Desplazados',[dbo].[TipoPoblacionSivigila] (@IdFicha,3) As 'Migrantes',[dbo].[TipoPoblacionSivigila] (@IdFicha,4) As 'Carcelarios',[dbo].[TipoPoblacionSivigila] (@IdFicha,5) As 'Gestantes',[dbo].[TipoPoblacionSivigila] (@IdFicha,6) As 'Indigentes',[dbo].[TipoPoblacionSivigila] (@IdFicha,7) As 'Poblacion infaltil',[dbo].[TipoPoblacionSivigila] (@IdFicha,8) As 'Madres comunitarias',[dbo].[TipoPoblacionSivigila] (@IdFicha,9) As 'Desmovilizados',[dbo].[TipoPoblacionSivigila] (@IdFicha,10) As 'Centros',[dbo].[TipoPoblacionSivigila] (@IdFicha,11) As 'Victimas',[dbo].[TipoPoblacionSivigila] (@IdFicha,12) As 'Otros',
							 SEMANASGESTACION as 'Semanas Gestacion', C.VERSION AS 'VERSION' , C.JSON AS 'JSON'
						From dbo.HCFICHANOTIFICACION C
							Inner Join dbo.INPACIENT I on C.IPCODPACI = I.IPCODPACI 
							INNER JOIN dbo.ADTIPOIDENTIFICA TI ON TI.CODIGO=C.TIPODOCUMENTO
							Inner join dbo.INUbicaci U on C.CODDEPARTAMENTO = U.AUUBICACI
							Inner Join dbo.INMUNICIP P on U.DEPMUNCOD = P.DEPMUNCOD
							Inner Join dbo.INDEPARTA E on P.DEPCODIGO = E.DEPCODIGO 
							Left Join dbo.ADACTIVID A on C.OCUPACION = A.codactivi
							INNER join dbo.INENTADM	X on X.CODENTADM = C.CODENTIDA
							Inner join dbo.INUbicaci M on C.RESIDENCIA = M.AUUBICACI
							Inner Join dbo.INMUNICIP R on M.DEPMUNCOD = R.DEPMUNCOD
							Inner Join dbo.INDEPARTA O on R.DEPCODIGO = O.DEPCODIGO 
				END
			 ELSE  ---  version cabecera =  nula: versiones previas a 'V09_2020-03-06'
				BEGIN
					IF @VersionERP = 0 --Vie
						Select C.Id,UPGD As 'Codigo UPGD',Rtrim(RAZONSOCIAL) As 'Razon Social',Rtrim(NOMBEVENTO) As 'Nombre Evento', Rtrim(CODEVENTO) As 'Codigo Evento',convert(varchar(10),FECHANOTIFI,103) As 'Fecha Notificacion',Rtrim(C.NACIONALIDAD) As 'Nacionalidad',
							 'Estrato:' +' '+ CONVERT(VARCHAR,ESTRATO) AS 'ESTRATO',
							 CASE FUENTE WHEN 1 THEN 'Notificación rutinaria' WHEN 2 THEN 'Búsqueda activa Inst' WHEN 3 THEN 'Vigilancia Intensificada' WHEN 4 THEN 'Búsqueda activa com.' WHEN 5 THEN 'Investigaciones' END AS 'FUENTE',
							 CASE TIPODOCUMENTO  WHEN '1' THEN 'X' END AS 'Registro Civil',CASE TIPODOCUMENTO WHEN '2' THEN 'X' END AS 'Tarjeta Identidad',CASE TIPODOCUMENTO WHEN '3' THEN 'X' END AS 'Cedula',CASE TIPODOCUMENTO WHEN '4' THEN 'X' END AS 'Cedula Extranjera',CASE TIPODOCUMENTO WHEN '5' THEN 'X' END AS 'Pasaporte',CASE TIPODOCUMENTO WHEN '6' THEN 'X' END AS 'Menor sin Identificacion',CASE TIPODOCUMENTO WHEN '7' THEN 'X' END AS 'Adulto sin Identificacion', CASE TIPODOCUMENTO WHEN '8' THEN 'X' END AS 'Permiso Especial',
							 CASE TIPODOCUMENTO  WHEN '1' THEN 'RC' WHEN '2' THEN 'TI' WHEN '3' THEN 'CC' WHEN '4' THEN 'CE' WHEN '5' THEN 'PA' WHEN '6' THEN 'MS' WHEN '7' THEN 'AS' WHEN '8' THEN 'PE' END AS 'TipoIdentificacion',
							  TI.NOMBRE AS TIPODOC,							 
							 --CASE TIPODOCUMENTO WHEN 1 THEN 'RC - Registro Civil' WHEN 2 THEN 'TI - Tarjeta de Identidad' WHEN 3 THEN 'CC - Cédula de Ciudadanía' WHEN 4 THEN 'CE - Cédula de Extranjería' WHEN 5 THEN 'PA - Pasaporte' WHEN 6 THEN 'MS - Menor Sin Identificación' WHEN 7 THEN 'AS - Adulto Sin Identificación' WHEN 8 THEN 'PE - Permiso Especial de Permanencia' WHEN 9 THEN 'CN - Certificado de Nacido Vivo' WHEN 10 THEN 'CD - Carnet Diplomático' WHEN 11 THEN 'SC - Salvoconducto' WHEN 12 THEN 'NU - Número único de identificación personal' WHEN 13 THEN 'DE - Documento Extranjero' WHEN 14 THEN 'PT - Permiso Temporal de Permanencia' WHEN 15 THEN 'SI - Sin Identificación' END AS TIPODOC,
							 Rtrim(IPNOMCOMP) AS 'Nombre paciente', Rtrim(I.IPCODPACI) As 'Identificacion',Rtrim(TELEFONO) As 'Telefono Paciente',IPFECNACI AS 'Fecha Nacimiento',EDAD As 'Edad',
							 CASE UNIDADMED  WHEN '1' THEN 'X' END AS 'Años',CASE UNIDADMED  WHEN '2' THEN 'X' END AS 'Meses',CASE UNIDADMED  WHEN '3' THEN 'X' END AS 'Dias',CASE UNIDADMED  WHEN '4' THEN 'X' END AS 'Horas',CASE UNIDADMED  WHEN '5' THEN 'X' END AS 'Minutos',CASE UNIDADMED  WHEN '6' THEN 'X' END AS 'No Aplica',
							 CASE SEXO  WHEN '1' THEN 'X' END AS 'Masculino',CASE SEXO  WHEN '2' THEN 'X' END AS 'Femenino',CASE SEXO  WHEN '3' THEN 'X' END AS 'Indeterminado',CODPAIS As 'Pais Ocurrencia',
							 Rtrim(E.depcodigo) + '-' + Rtrim(E.nomdepart) +' ; '+ Rtrim(P.MUNCODIGO) +'-'+ Rtrim(P.MUNNOMBRE)  as 'Departamento Municipio',  CASE AREA  WHEN '1' THEN 'X' END AS 'Cabecera municipal',CASE AREA  WHEN '2' THEN 'X' END AS 'Centro poblado',CASE AREA  WHEN '3' THEN 'X' END AS 'Rural disperso',
							 Rtrim(LOCALIDAD) As 'Localidad',Rtrim(BARRIO) As 'Barrio',Rtrim(CENTRORURAL) As 'Centro municipal', Rtrim(VEREDA) As 'Vereda Zona',Rtrim(A.codactivi) +'-'+ Rtrim(A.desactivi) As 'Ocupacion',
							 CASE TIPOREGIMEN WHEN '1' THEN 'X' END AS 'Exepción',CASE TIPOREGIMEN WHEN '2' THEN 'X' END AS 'Especial',CASE TIPOREGIMEN WHEN '3' THEN 'X' END AS 'Contributivo',CASE TIPOREGIMEN WHEN '4' THEN 'X' END AS 'Subsidiado',CASE TIPOREGIMEN WHEN '5' THEN 'X' END AS 'No asegurado',CASE TIPOREGIMEN WHEN '6' THEN 'X' END AS 'Indeterminado/pendiente',
							 CASE ETNIA WHEN '1' THEN 'X' END AS 'Indigena',CASE ETNIA WHEN '2' THEN 'X' END AS 'Rom gitano',CASE ETNIA WHEN '3' THEN 'X' END AS 'Raizal',CASE ETNIA WHEN '4' THEN 'X' END AS 'Palenquero',CASE ETNIA WHEN '5' THEN 'X' END AS 'Negro',CASE ETNIA WHEN '6' THEN 'X' END AS 'Otro',
							 Rtrim(Code) +'-'+ Rtrim(Name) As 'Nombre Entidad beneficios',Rtrim(O.depcodigo) + '-' + Rtrim(O.nomdepart) +' ; '+ Rtrim(R.MUNCODIGO) +'-'+ Rtrim(R.MUNNOMBRE)  as 'Residencia',Rtrim(DIRRESIDENCIA) As 'Direccion Residencia',convert(varchar(10),FECHACONSULTA,103) As 'Fecha Consulta',convert(varchar(10),FECHAINICOSINTO,103) As 'Fecha Inicio Sintomas',
							 CASE CLASIFICACIONCASO WHEN '1' THEN 'X' END AS 'Sospechoso',CASE CLASIFICACIONCASO WHEN '2' THEN 'X' END AS 'Probable',CASE CLASIFICACIONCASO WHEN '3' THEN 'X' END AS 'Conf laboratorio',CASE CLASIFICACIONCASO WHEN '4' THEN 'X' END AS 'Conf clinica',CASE CLASIFICACIONCASO WHEN '5' THEN 'X' END AS 'Conf epidemiológico',
							 CASE Rtrim(HOSPITALIZADO) WHEN '1' THEN 'X' END AS 'Si',CASE Rtrim(HOSPITALIZADO) WHEN '0' THEN 'X' END AS 'No',convert(varchar(10),FECHAHOSP,103) As 'Fecha Hospitalizacion', CASE CONDICIONFINAL WHEN '1' THEN 'X' END AS 'Vivo',CASE CONDICIONFINAL WHEN '2' THEN 'X' END AS 'Muerto',CASE CONDICIONFINAL WHEN '3' THEN 'X' END AS 'No Sabe', convert(varchar(10),FECHADIFUNCION,103) As 'Fecha Difuncion',Rtrim(NUMCERTIFICADO) As 'Numero Certificado',Rtrim(CAUSAMUERTE) As 'Causa Muerte',Rtrim(NOMPROFESIONAL) As 'Nombre Profesional', Rtrim(TELNOTIFICA) As 'Telefono Profesional',
							 [dbo].[TipoPoblacionSivigila] (@IdFicha,1) As 'Discapacitados',[dbo].[TipoPoblacionSivigila] (@IdFicha,2) As 'Desplazados',[dbo].[TipoPoblacionSivigila] (@IdFicha,3) As 'Migrantes',[dbo].[TipoPoblacionSivigila] (@IdFicha,4) As 'Carcelarios',[dbo].[TipoPoblacionSivigila] (@IdFicha,5) As 'Gestantes',[dbo].[TipoPoblacionSivigila] (@IdFicha,6) As 'Indigentes',[dbo].[TipoPoblacionSivigila] (@IdFicha,7) As 'Poblacion infaltil',[dbo].[TipoPoblacionSivigila] (@IdFicha,8) As 'Madres comunitarias',[dbo].[TipoPoblacionSivigila] (@IdFicha,9) As 'Desmovilizados',[dbo].[TipoPoblacionSivigila] (@IdFicha,10) As 'Centros',[dbo].[TipoPoblacionSivigila] (@IdFicha,11) As 'Victimas',[dbo].[TipoPoblacionSivigila] (@IdFicha,12) As 'Otros',
							 SEMANASGESTACION as 'Semanas Gestacion' , C.VERSION AS 'VERSION' , C.JSON AS 'JSON'
						From dbo.HCFICHANOTIFICACION C
							Inner Join dbo.INPACIENT I on C.IPCODPACI = I.IPCODPACI 
							INNER JOIN dbo.ADTIPOIDENTIFICA TI ON TI.CODIGO=C.TIPODOCUMENTO
							Inner join dbo.INUbicaci U on C.CODDEPARTAMENTO = U.AUUBICACI
							Inner Join dbo.INMUNICIP P on U.DEPMUNCOD = P.DEPMUNCOD
							Inner Join dbo.INDEPARTA E on P.DEPCODIGO = E.DEPCODIGO 
							Left Join dbo.ADACTIVID A on C.OCUPACION = A.codactivi
							inner join Contract.HealthAdministrator heal on heal.Id = C.ADMINPLANBENE
							Inner join dbo.INUbicaci M on C.RESIDENCIA = M.AUUBICACI
							Inner Join dbo.INMUNICIP R on M.DEPMUNCOD = R.DEPMUNCOD
							Inner Join dbo.INDEPARTA O on R.DEPCODIGO = O.DEPCODIGO 
							Where C.ID = @IdFicha
					ELSE  --Otro ERP
						 Select C.Id,UPGD As 'Codigo UPGD',Rtrim(RAZONSOCIAL) As 'Razon Social',Rtrim(NOMBEVENTO) As 'Nombre Evento', Rtrim(CODEVENTO) As 'Codigo Evento',convert(varchar(10),FECHANOTIFI,103) As 'Fecha Notificacion',Rtrim(C.NACIONALIDAD) As 'Nacionalidad',
							'Estrato:' +' '+ CONVERT(VARCHAR,ESTRATO) AS 'ESTRATO',
							 CASE FUENTE WHEN 1 THEN 'Notificación rutinaria' WHEN 2 THEN 'Búsqueda activa Inst' WHEN 3 THEN 'Vigilancia Intensificada' WHEN 4 THEN 'Búsqueda activa com.' WHEN 5 THEN 'Investigaciones' END AS 'FUENTE',
							 CASE TIPODOCUMENTO  WHEN '1' THEN 'X' END AS 'Registro Civil',CASE TIPODOCUMENTO WHEN '2' THEN 'X' END AS 'Tarjeta Identidad',CASE TIPODOCUMENTO WHEN '3' THEN 'X' END AS 'Cedula',CASE TIPODOCUMENTO WHEN '4' THEN 'X' END AS 'Cedula Extranjera',CASE TIPODOCUMENTO WHEN '5' THEN 'X' END AS 'Pasaporte',CASE TIPODOCUMENTO WHEN '6' THEN 'X' END AS 'Menor sin Identificacion',CASE TIPODOCUMENTO WHEN '7' THEN 'X' END AS 'Adulto sin Identificacion', CASE TIPODOCUMENTO WHEN '8' THEN 'X' END AS 'Permiso Especial',
							 CASE TIPODOCUMENTO  WHEN '1' THEN 'RC' WHEN '2' THEN 'TI' WHEN '3' THEN 'CC' WHEN '4' THEN 'CE' WHEN '5' THEN 'PA' WHEN '6' THEN 'MS' WHEN '7' THEN 'AS' WHEN '8' THEN 'PE' END AS 'TipoIdentificacion',
							 TI.NOMBRE AS TIPODOC,
							---CASE TIPODOCUMENTO WHEN 1 THEN 'RC - Registro Civil' WHEN 2 THEN 'TI - Tarjeta de Identidad' WHEN 3 THEN 'CC - Cédula de Ciudadanía' WHEN 4 THEN 'CE - Cédula de Extranjería' WHEN 5 THEN 'PA - Pasaporte' WHEN 6 THEN 'MS - Menor Sin Identificación' WHEN 7 THEN 'AS - Adulto Sin Identificación' WHEN 8 THEN 'PE - Permiso Especial de Permanencia' WHEN 9 THEN 'CN - Certificado de Nacido Vivo' WHEN 10 THEN 'CD - Carnet Diplomático' WHEN 11 THEN 'SC - Salvoconducto' WHEN 12 THEN 'NU - Número único de identificación personal' WHEN 13 THEN 'DE - Documento Extranjero' WHEN 14 THEN 'PT - Permiso Temporal de Permanencia' WHEN 15 THEN 'SI - Sin Identificación' END AS TIPODOC,
							 Rtrim(IPNOMCOMP) AS 'Nombre paciente', Rtrim(I.IPCODPACI) As 'Identificacion',Rtrim(TELEFONO) As 'Telefono Paciente',convert(varchar(10),IPFECNACI) AS 'Fecha Nacimiento',EDAD As 'Edad',
							 CASE UNIDADMED  WHEN '1' THEN 'X' END AS 'Años',CASE UNIDADMED  WHEN '2' THEN 'X' END AS 'Dias',CASE UNIDADMED  WHEN '3' THEN 'X' END AS 'Minutos',CASE UNIDADMED  WHEN '4' THEN 'X' END AS 'Meses',CASE UNIDADMED  WHEN '5' THEN 'X' END AS 'Horas',CASE UNIDADMED  WHEN '6' THEN 'X' END AS 'No Aplica',
							 CASE SEXO  WHEN '1' THEN 'X' END AS 'Masculino',CASE SEXO  WHEN '2' THEN 'X' END AS 'Femenino',CASE SEXO  WHEN '3' THEN 'X' END AS 'Indeterminado',CODPAIS As 'Pais Ocurrencia',
							 Rtrim(E.depcodigo) + '-' + Rtrim(E.nomdepart) +' ; '+ Rtrim(P.MUNCODIGO) +'-'+ Rtrim(P.MUNNOMBRE)  as 'Departamento Municipio',  CASE AREA  WHEN '1' THEN 'X' END AS 'Cabecera municipal',CASE AREA  WHEN '2' THEN 'X' END AS 'Centro poblado',CASE AREA  WHEN '3' THEN 'X' END AS 'Rural disperso',
							 Rtrim(LOCALIDAD) As 'Localidad',Rtrim(BARRIO) As 'Barrio',Rtrim(CENTRORURAL) As 'Centro municipal', Rtrim(VEREDA) As 'Vereda Zona',Rtrim(A.codactivi) +'-'+ Rtrim(A.desactivi) As 'Ouapcion',
							 CASE TIPOREGIMEN WHEN '1' THEN 'X' END AS 'Exepción',CASE TIPOREGIMEN WHEN '2' THEN 'X' END AS 'Especial',CASE TIPOREGIMEN WHEN '3' THEN 'X' END AS 'Contributivo',CASE TIPOREGIMEN WHEN '4' THEN 'X' END AS 'Subsidiado',CASE TIPOREGIMEN WHEN '5' THEN 'X' END AS 'No asegurado',CASE TIPOREGIMEN WHEN '6' THEN 'X' END AS 'Indeterminado/pendiente',
							 CASE ETNIA WHEN '1' THEN 'X' END AS 'Indígena',CASE ETNIA WHEN '2' THEN 'X' END AS 'Rom gitano',CASE ETNIA WHEN '3' THEN 'X' END AS 'Raizal',CASE ETNIA WHEN '4' THEN 'X' END AS 'Palenquero',CASE ETNIA WHEN '5' THEN 'X' END AS 'Negro',CASE ETNIA WHEN '6' THEN 'X' END AS 'Otro',
							 Rtrim(CODENTADM) +'-'+ Rtrim(NOMENTADM) As 'Nombre Entidad beneficios',Rtrim(O.depcodigo) + '-' + Rtrim(O.nomdepart) +' ; '+ Rtrim(R.MUNCODIGO) +'-'+ Rtrim(R.MUNNOMBRE)  as 'Residencia',Rtrim(DIRRESIDENCIA) As 'Direccion Residencia',convert(varchar(10),FECHACONSULTA,103) As 'Fecha Consulta',convert(varchar(10),FECHAINICOSINTO,103) As 'Fecha Inicio Sintomas',
							 CASE CLASIFICACIONCASO WHEN '1' THEN 'X' END AS 'Sospechoso',CASE CLASIFICACIONCASO WHEN '2' THEN 'X' END AS 'Probable',CASE CLASIFICACIONCASO WHEN '3' THEN 'X' END AS 'Conf laboratorio',CASE CLASIFICACIONCASO WHEN '4' THEN 'X' END AS 'Conf clinica',CASE CLASIFICACIONCASO WHEN '5' THEN 'X' END AS 'Conf epidemiológico',
							 CASE Rtrim(HOSPITALIZADO) WHEN '1' THEN 'X' END AS 'Si',CASE Rtrim(HOSPITALIZADO) WHEN '2' THEN 'X' END AS 'No',convert(varchar(10),FECHAHOSP,103) As 'Fecha Hospitalizacion', CASE CONDICIONFINAL WHEN '1' THEN 'X' END AS 'Vivo',CASE CONDICIONFINAL WHEN '2' THEN 'X' END AS 'Muerto',CASE CONDICIONFINAL WHEN '3' THEN 'X' END AS 'No Sabe', convert(varchar(10),FECHADIFUNCION,103) As 'Fecha Difuncion',Rtrim(NUMCERTIFICADO) As 'Numero Certificado',Rtrim(CAUSAMUERTE) As 'Causa Muerte',Rtrim(NOMPROFESIONAL) As 'Nombre Profesional', Rtrim(TELNOTIFICA) As 'Telefono Profesional',
							 [dbo].[TipoPoblacionSivigila] (@IdFicha,1) As 'Discapacitados',[dbo].[TipoPoblacionSivigila] (@IdFicha,2) As 'Desplazados',[dbo].[TipoPoblacionSivigila] (@IdFicha,3) As 'Migrantes',[dbo].[TipoPoblacionSivigila] (@IdFicha,4) As 'Carcelarios',[dbo].[TipoPoblacionSivigila] (@IdFicha,5) As 'Gestantes',[dbo].[TipoPoblacionSivigila] (@IdFicha,6) As 'Indigentes',[dbo].[TipoPoblacionSivigila] (@IdFicha,7) As 'Poblacion infaltil',[dbo].[TipoPoblacionSivigila] (@IdFicha,8) As 'Madres comunitarias',[dbo].[TipoPoblacionSivigila] (@IdFicha,9) As 'Desmovilizados',[dbo].[TipoPoblacionSivigila] (@IdFicha,10) As 'Centros',[dbo].[TipoPoblacionSivigila] (@IdFicha,11) As 'Victimas',[dbo].[TipoPoblacionSivigila] (@IdFicha,12) As 'Otros',
							 SEMANASGESTACION as 'Semanas Gestacion' , C.VERSION AS 'VERSION' , C.JSON AS 'JSON'
						From dbo.HCFICHANOTIFICACION C
							Inner Join dbo.INPACIENT I on C.IPCODPACI = I.IPCODPACI 
							INNER JOIN dbo.ADTIPOIDENTIFICA TI ON TI.CODIGO=C.TIPODOCUMENTO
							Inner join dbo.INUbicaci U on C.CODDEPARTAMENTO = U.AUUBICACI
							Inner Join dbo.INMUNICIP P on U.DEPMUNCOD = P.DEPMUNCOD
							Inner Join dbo.INDEPARTA E on P.DEPCODIGO = E.DEPCODIGO 
							Left Join dbo.ADACTIVID A on C.OCUPACION = A.codactivi
							INNER join dbo.INENTADM	X on X.CODENTADM = C.CODENTIDA
							Inner join dbo.INUbicaci M on C.RESIDENCIA = M.AUUBICACI
							Inner Join dbo.INMUNICIP R on M.DEPMUNCOD = R.DEPMUNCOD
							Inner Join dbo.INDEPARTA O on R.DEPCODIGO = O.DEPCODIGO 
				END
		END
	ELSE
	Select C.Id,UPGD As 'Codigo UPGD',Rtrim(RAZONSOCIAL) As 'Razon Social',Rtrim(NOMBEVENTO) As 'Nombre Evento', Rtrim(CODEVENTO) As 'Codigo Evento',convert(varchar(10),FECHANOTIFI,103) As 'Fecha Notificacion',Rtrim(C.NACIONALIDAD) As 'Nacionalidad',
					 CONVERT(VARCHAR,ESTRATO) AS 'ESTRATO',
					 CASE FUENTE WHEN 1 THEN 1 ELSE 0 END AS 'Notificación rutinaria',CASE FUENTE WHEN 2 THEN 1 ELSE 0 END AS 'Busqueda activa',CASE FUENTE WHEN 3 THEN 1 ELSE 0 END AS 'Vigilancia intensificada',CASE FUENTE WHEN 4 THEN 1 ELSE 0 END AS 'Busqueda activa com',CASE FUENTE WHEN 5 THEN 1 ELSE 0 END AS 'Invstigaciones', 
					 TI.NOMBRE AS TIPODOC,
					 Rtrim(IPNOMCOMP) AS 'Nombre paciente', Rtrim(I.IPCODPACI) As 'Identificacion',Rtrim(TELEFONO) As 'Telefono Paciente',convert(varchar(10),IPFECNACI ,103) AS 'Fecha Nacimiento',EDAD As 'Edad',
					 CASE UNIDADMED  WHEN '1' THEN 'X' END AS 'Años',CASE UNIDADMED  WHEN '2' THEN 'X' END AS 'Meses',CASE UNIDADMED  WHEN '3' THEN 'X' END AS 'Dias',CASE UNIDADMED  WHEN '4' THEN 'X' END AS 'Horas',CASE UNIDADMED  WHEN '5' THEN 'X' END AS 'Minutos',CASE UNIDADMED  WHEN '6' THEN 'X' END AS 'No Aplica',
					 CASE SEXO  WHEN '1' THEN 1 ELSE 0 END AS 'Masculino',CASE SEXO  WHEN '2' THEN 1 ELSE 0 END AS 'Femenino',CASE SEXO  WHEN '3' THEN 1 ELSE 0 END AS 'Indeterminado',CODPAIS As 'Pais Ocurrencia',
					 CONCAT( RTRIM(E.depcodigo), '-', RTRIM(E.nomdepart), ' / ', RTRIM(P.MUNCODIGO), '-', RTRIM(P.MUNNOMBRE) ) as 'Departamento Municipio', RTRIM(E.nomdepart) AS 'DepartamentoResidencia' ,
					 CASE AREA  WHEN '1' THEN 'X' END AS 'Cabecera municipal',CASE AREA  WHEN '2' THEN 'X' END AS 'Centro poblado',CASE AREA  WHEN '3' THEN 'X' END AS 'Rural disperso',
					 Rtrim(LOCALIDAD) As 'Localidad',Rtrim(BARRIO) As 'Barrio',Rtrim(CENTRORURAL) As 'Centro municipal', Rtrim(VEREDA) As 'Vereda Zona',Rtrim(A.codactivi) +'-'+ Rtrim(A.desactivi) As 'Ocupacion',
					 Rtrim(heal.Code) +'-'+ Rtrim(heal.Name) As 'Nombre Entidad beneficios', 
					 CONCAT( RTRIM(Q.StandardCode), '-', RTRIM(Q.Name), ' / ', RTRIM(O.depcodigo), '-', RTRIM(O.nomdepart), ' / ', RTRIM(R.MUNCODIGO), '-', RTRIM(R.MUNNOMBRE) ) as 'Residencia', 
					 Rtrim(DIRRESIDENCIA) As 'Direccion Residencia',convert(varchar(10),FECHACONSULTA,103) As 'Fecha Consulta',convert(varchar(10),FECHAINICOSINTO,103) As 'Fecha Inicio Sintomas',
					 CASE CLASIFICACIONCASO WHEN '1' THEN 1 ELSE 0 END AS 'Sospechoso', CASE CLASIFICACIONCASO WHEN '2' THEN 1 ELSE 0 END AS 'Probable', CASE CLASIFICACIONCASO WHEN '3' THEN 1 ELSE 0 END AS 'Conf por laboratorio', CASE CLASIFICACIONCASO WHEN '4' THEN 1 ELSE 0 END AS 'Conf por clinica',
					 CASE Rtrim(HOSPITALIZADO) WHEN '1' THEN 1 ELSE 0 END AS 'Si',CASE Rtrim(HOSPITALIZADO) WHEN '0' THEN 1 ELSE 0 END AS 'No',convert(varchar(10),FECHAHOSP,103) As 'Fecha Hospitalizacion', CASE CONDICIONFINAL WHEN '1' THEN 1 ELSE 0 END AS 'Vivo',CASE CONDICIONFINAL WHEN '2' THEN 1 ELSE 0 END AS 'Muerto',CASE CONDICIONFINAL WHEN '3' THEN 1 ELSE 0 END AS 'No Sabe', convert(varchar(10),FECHADIFUNCION,103) As 'Fecha Difuncion',Rtrim(NUMCERTIFICADO) As 'Numero Certificado',Rtrim(CAUSAMUERTE) As 'Causa Muerte',Rtrim(NOMPROFESIONAL) As 'Nombre Profesional', Rtrim(TELNOTIFICA) As 'Telefono Profesional',
					 [dbo].[TipoPoblacionSivigila] (@IdFicha,1) As 'Discapacitados',[dbo].[TipoPoblacionSivigila] (@IdFicha,2) As 'Desplazados',[dbo].[TipoPoblacionSivigila] (@IdFicha,3) As 'Migrantes',[dbo].[TipoPoblacionSivigila] (@IdFicha,4) As 'Carcelarios',[dbo].[TipoPoblacionSivigila] (@IdFicha,5) As 'Gestantes',[dbo].[TipoPoblacionSivigila] (@IdFicha,6) As 'Indigentes',[dbo].[TipoPoblacionSivigila] (@IdFicha,7) As 'Poblacion infaltil',[dbo].[TipoPoblacionSivigila] (@IdFicha,8) As 'Madres comunitarias',[dbo].[TipoPoblacionSivigila] (@IdFicha,9) As 'Desmovilizados',[dbo].[TipoPoblacionSivigila] (@IdFicha,10) As 'Centros',[dbo].[TipoPoblacionSivigila] (@IdFicha,11) As 'Victimas',[dbo].[TipoPoblacionSivigila] (@IdFicha,12) As 'Otros', [dbo].[TipoPoblacionSivigila] (@IdFicha,13) As 'Indigenas',
					 SEMANASGESTACION as 'Semanas Gestacion', C.VERSION AS 'VERSION', C.JSON AS 'JSON', 
					 RTRIM( JSON_VALUE(JSON,'$.CUALETNIA') ) As 'CUALETNIA', RTRIM( JSON_VALUE(JSON,'$.CUALBARRIO') ) As 'CUALBARRIO', JSON_VALUE(JSON,'$.IdentificacionGenero') As 'IdentificacionGenero', RTRIM(JSON_VALUE(JSON,'$.CualGenero')) As 'CualGenero', JSON_VALUE(JSON,'$.OrientacionSexual') As 'OrientacionSexual', RTRIM(JSON_VALUE(JSON,'$.CualOrientacionSex')) As 'CualOrientacionSex',  
					 TIPOREGIMEN as 'Tipo Regimen Distritales', ETNIA as 'Etnia Distritales', IPPRINOMB as 'Primer Nombre', IPSEGNOMB as 'Segundo Nombre', IPPRIAPEL as 'Primer Apellido', IPSEGAPEL as 'Segundo Apellido',
					 CASE TIPODOCUMENTO WHEN '9' THEN 1 ELSE 0 END AS 'CNV', CASE TIPODOCUMENTO WHEN '4' THEN 1 ELSE 0 END AS 'RC', CASE TIPODOCUMENTO WHEN '3' THEN 1 ELSE 0 END AS 'TI', CASE TIPODOCUMENTO WHEN '1' THEN 1 ELSE 0 END AS 'CC', CASE TIPODOCUMENTO WHEN '12' THEN 1 ELSE 0 END AS 'PEP', CASE TIPODOCUMENTO WHEN '2' THEN 1 ELSE 0 END AS 'CE',
					 CASE TIPODOCUMENTO WHEN '5' THEN 1 ELSE 0 END AS 'PA', CASE TIPODOCUMENTO WHEN '7' THEN 1 ELSE 0 END AS 'MSI', CASE TIPODOCUMENTO WHEN '6' THEN 1 ELSE 0 END AS 'ASI', CASE TIPODOCUMENTO WHEN '13' THEN 1 ELSE 0 END AS 'PPT', CASE TIPODOCUMENTO WHEN '11' THEN 1 ELSE 0 END AS 'SC'
				From dbo.HCFICHANOTIFICACION C
					Inner Join dbo.INPACIENT I on C.IPCODPACI = I.IPCODPACI 
					INNER JOIN dbo.ADTIPOIDENTIFICA TI ON TI.CODIGO=C.TIPODOCUMENTO
					Inner join dbo.INUbicaci U on C.CODDEPARTAMENTO = U.AUUBICACI
					Inner Join dbo.INMUNICIP P on U.DEPMUNCOD = P.DEPMUNCOD
					Inner Join dbo.INDEPARTA E on P.DEPCODIGO = E.DEPCODIGO 
					Left Join dbo.ADACTIVID A on C.OCUPACION = A.codactivi
					inner join Contract.HealthAdministrator heal on heal.Id = C.ADMINPLANBENE
					Left join dbo.INUbicaci M on C.RESIDENCIA = M.AUUBICACI
					Left Join dbo.INMUNICIP R on M.DEPMUNCOD = R.DEPMUNCOD
					Left Join dbo.INDEPARTA O on R.DEPCODIGO = O.DEPCODIGO 
					LEFT JOIN Common.Country Q ON O.IDPAIS = Q.Id 
				    Where C.ID = @IdFicha

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recupera la cabecera completa de las fichas de notificación obligatoria SIVIGILA para un paciente y un ingreso específicos, adaptando el resultado según la versión del formulario (V10, V11, V12) y la plataforma (Vie Cloud o versión anterior). Consolida información clínica, demográfica y epidemiológica del evento notificable consultando la ficha de notificación (HCFICHANOTIFICACION), los datos del paciente (INPACIENT), el tipo de documento de identificación (ADTIPOIDENTIFICA), la ubicación geográfica del caso y la residencia (INUBICACI, INMUNICIP, INDEPARTA), la ocupación (ADACTIVID) y la administradora de salud o EPS (HealthAdministrator). Distingue entre fichas SIVIGILA nacionales y fichas distritales (eventos con código terminado en ''_D'') para aplicar la plantilla de cabecera correcta. Se usa para imprimir o visualizar el encabezado oficial de la ficha epidemiológica antes de enviarla al sistema de vigilancia en salud pública.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarCabeceraFichasSivigila';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarCabeceraFichasSivigila';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve la cabecera de una ficha de notificación SIVIGILA o Distrital, seleccionando el formato de salida según el código del evento, la versión de la ficha y el ERP de origen.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarCabeceraFichasSivigila';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCFICHANOTIFICACION con ID = parámetro de ficha; en caso contrario las variables internas (VERSION, CODEVENTO) quedan nulas y se cae al ramo de versión legacy; El paciente referenciado por la ficha debe existir en INPACIENT (INNER JOIN); El TIPODOCUMENTO de la ficha debe existir en ADTIPOIDENTIFICA (INNER JOIN); Las ubicaciones de departamento (CODDEPARTAMENTO) y residencia deben existir en INUbicaci, INMUNICIP e INDEPARTA (INNER JOIN, salvo en el ramo distrital que usa LEFT JOIN para residencia); Para ERP=0 la administradora de planes de beneficios (ADMINPLANBENE) debe existir en Contract.HealthAdministrator; para otros ERP, CODENTIDA debe existir en INENTADM', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarCabeceraFichasSivigila';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La ficha siempre se identifica por su Id (C.ID = @IdFicha); todas las consultas devuelven a lo más una fila por ficha; Los eventos con código ''875_D'', ''356_D'' y ''903_D'' se clasifican como fichas Distritales y no como Sivigila estándar; El tipo de documento se traduce a un código corto (RC, TI, CC, CE, PA, MS, AS, PE, CN, CD, SC, DE) según el catálogo numérico de TIPODOCUMENTO; La fuente de notificación se mapea a un texto fijo: 1=Notificación rutinaria, 2=Búsqueda activa Inst, 3=Vigilancia Intensificada, 4=Búsqueda activa com., 5=Investigaciones; Las categorías de sexo, área, régimen, etnia, clasificación del caso, condición final y hospitalización se exponen como columnas tipo bandera (''X'' o 1/0) por cada valor posible; Las 12 categorías de población especial (1..12, y 13 sólo en distritales) se obtienen invocando la función dbo.TipoPoblacionSivigila con el IdFicha; Para ERP distinto de 0 (''Otro ERP'') la nacionalidad/país de residencia se asume Colombia (código 170); Para fichas distritales se exponen tanto banderas binarias (1/0) como datos crudos de TIPOREGIMEN y ETNIA, además de descomponer el nombre del paciente en sus 4 partes', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarCabeceraFichasSivigila';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CODEVENTO de la ficha es ''875_D'', ''356_D'' o ''903_D'' → Se trata como ficha Distrital (EsSivigila=0) y se ejecuta el bloque ELSE final con columnas y catálogos específicos del distrito (incluye campos JSON adicionales como CUALBARRIO, banderas binarias 1/0 y campos distritales TIPOREGIMEN, ETNIA, nombres y apellidos separados) else Se trata como ficha Sivigila estándar (EsSivigila=1) y se enruta según VERSION de la cabecera; si EsSivigila=1 y VERSION ∈ {''V10_2021-05-23'',''V11_2022-06-08'',''V12_2024-03-01''} → Se devuelve la cabecera con el formato FOR-R02.0000-001 V:10/V:11/V:12, incluyendo campos JSON Nationality, Neighborhood, Sidewalk, género y orientación sexual, y tipos de documento ampliados (RC,TI,CC,CE,PA,MS,AS,PE,CN,CD,SC,DE) else Se evalúa si la versión es V09 o anterior; si EsSivigila=1 y VERSION = ''V09_2020-03-06'' → Se devuelve la cabecera con el formato FOR-R02.0000-001 V:09 (sin campos de género/orientación sexual ni barrio/vereda desde JSON, tipos de documento RC..CN) else Se asume versión previa a V09 (versión nula u otra); si VERSION nula o anterior a V09 → Se devuelve la cabecera legacy con concatenación de departamento/municipio con separador '';'' y tipos de documento solo hasta PE; si @VersionERP = 0 (ERP ''Vie'') → Se usan joins a Contract.HealthAdministrator (heal) y Common.Country para entidad de beneficios y país de residencia, y se filtra Where C.ID = @IdFicha else Se usa dbo.INENTADM y se concatena país fijo ''170-COLOMBIA'' para la residencia, tomando entidad desde CODENTADM/NOMENTADM de la ficha', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarCabeceraFichasSivigila';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.TipoPoblacionSivigila', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarCabeceraFichasSivigila';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHANOTIFICACION; dbo.INPACIENT; dbo.ADTIPOIDENTIFICA; dbo.INUbicaci; dbo.INMUNICIP; dbo.INDEPARTA; dbo.ADACTIVID; Contract.HealthAdministrator; Common.Country; dbo.INENTADM', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarCabeceraFichasSivigila';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarCabeceraFichasSivigila';
-- GO
