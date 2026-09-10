CREATE PROCEDURE [dbo].[SPREP_HC_Generales_CabeceraReportes]
(
@CodigoPaciente Varchar(25),
@NumeroIngreso Varchar(Max)
)
WITH RECOMPILE  
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Insert statements for procedure here
          
IF @NumeroIngreso = '' or @NumeroIngreso = 'NOT NULL'

	SELECT TOP 1
		A.IPCODPACI AS 'CODIGO PACIENTE', dbo.TipoDocumento(A.IPTIPODOC) AS 'TIPO DOCUMENTO', RTRIM(A.IPPRIAPEL) + ' ' + RTRIM(A.IPSEGAPEL) AS 'APELLIDOS',
		RTRIM(A.IPPRINOMB) + ' ' + RTRIM(A.IPSEGNOMB) AS 'NOMBRES', A.IPFECNACI AS 'EDAD', '' AS 'FECHA DE NACIMIENTO',
		IIF(H.Address IS NULL,(RTRIM(IPDIRECCI) + ' - ' + RTRIM(E.UBINOMBRE) + ' - ' + RTRIM(F.MUNNOMBRE) + ' - ' + RTRIM(z.nomdepart)),(dbo.Patient_Address(@CodigoPaciente))) AS 'DIRECCION',
		CASE IPSEXOPAC WHEN 1 THEN 'MASCULINO' ELSE 'FEMENINO' END AS 'SEXO', [dbo].[TypeGenderIdentity](A.IdGenderIdentity) AS 'IDENTIDAD GENERO',
		A.IPSEXOPAC as 'GENERO', -- Esta línea es obsoleta, pero se deja para no reventar algún reporte que utilice este parámetro.
		RTRIM(A.IPTELEFON) + ' - ' + RTRIM(A.IPTELMOVI) AS 'TELEFONO', IPGRUPSAN AS 'GRUPO SANGUINEO', CASE IPRHSANGR WHEN '+' THEN 'Positivo' WHEN '-' THEN 'Negativo' ELSE '!!' END AS 'RH SANGRE', 
		RTRIM(C.NOMENTIDA) AS 'NOMBRE ENTIDAD', dbo.TipoAfiliado(A.IPTIPOAFI) AS 'TIPO AFILIADO', dbo.TipoPaciente(A.IPTIPOPAC) AS 'TIPO PACIENTE',
		dbo.RangoAfiliacion(A.NIVCODIGO) AS 'NIVEL AFILIACION', dbo.EstadoCivilPaciente(A.IPESTADOC,A.IPSEXOPAC) AS 'ESTADO CIVIL',
		D.DESACTIVI AS PROFESION, G.DESGRUPET AS 'GRUPO ETNICO', [Common].[GETDATE]() AS 'FECHA DE IMPRESION', dbo.DireccionEmpresa() AS 'DIRECCION EMPRESA', 
		dbo.TelefonoEmpresa()  AS 'TELEFONO EMPRESA', A.NUMCARPET AS 'NUMERO HISTORIA', EV.Name As 'EntidadVie', '' AS 'CodigoCama', '' As 'INGRESOS',
		'' AS 'NOMBRE DE LA EMPRESA', '' AS 'CODIGO HABILITACION', '' AS 'DIRECCION CENTRO ATENCION', '' AS 'NUMERO TELEFONO CENTRO ATENCION', 
		'' AS 'NUMERO CELULAR CENTRO ATENCION' ,'' AS 'INGRESO', '' AS 'FECHA DE INGRESO', '' AS 'SERVICIO-INGRESO', '' AS 'SERVICIO-EGRESO'

		FROM INPACIENT A With(Nolock) 
		LEFT JOIN ADINGRESO B With(Nolock) ON A.IPCODPACI=B.IPCODPACI
		LEFT JOIN dbo.INEntidad C With(Nolock) ON A.CODENTIDA=C.CODENTIDA
		LEFT JOIN dbo.ADACTIVID D With(Nolock) ON A.CODACTIVI=D.CODACTIVI
		LEFT JOIN dbo.INUBICACI E With(Nolock) ON A.AUUBICACI=E.AUUBICACI
		LEFT JOIN dbo.INMUNICIP F With(Nolock) ON E.DEPMUNCOD=F.DEPMUNCOD
		LEFT JOIN dbo.INDEPARTA z With(Nolock) ON z.depcodigo = F.DEPCODIGO 
		LEFT JOIN dbo.ADGRUETNI G With(Nolock) ON A.CODGRUPOE=G.CODGRUPOE 
		LEFT JOIN Admissions.PatientAddress H With(Nolock) ON A.IPCODPACI = H.IPCODPACI
		LEFT OUTER JOIN CONTRACT.healthadministrator EV With(Nolock) ON EV.Id = A.GENCONENTITY 
		WHERE A.IPCODPACI=@CodigoPaciente ORDER BY b.IFECHAING DESC

ELSE

	SELECT 
		A.IPCODPACI AS 'CODIGO PACIENTE', dbo.TipoDocumento(A.IPTIPODOC) AS 'TIPO DOCUMENTO', RTRIM(A.IPPRIAPEL) + ' ' + RTRIM(A.IPSEGAPEL) AS 'APELLIDOS', 
		RTRIM(A.IPPRINOMB) + ' ' + RTRIM(A.IPSEGNOMB) AS 'NOMBRES', A.IPFECNACI AS 'EDAD', '' AS 'FECHA DE NACIMIENTO', 
		IIF(H.Address IS NULL,(RTRIM(IPDIRECCI) + ' - ' + RTRIM(E.UBINOMBRE) + ' - ' + RTRIM(F.MUNNOMBRE) + ' - ' + RTRIM(z.nomdepart)),(dbo.Patient_Address(@CodigoPaciente))) AS 'DIRECCION',
		CASE IPSEXOPAC WHEN 1 THEN 'MASCULINO' ELSE 'FEMENINO' END AS 'SEXO',  [dbo].[TypeGenderIdentity](A.IdGenderIdentity) AS 'IDENTIDAD GENERO', 
		A.IPSEXOPAC as 'GENERO', -- Esta línea es obsoleta, pero se deja para no reventar algún reporte que utilice este parámetro.
		RTRIM(A.IPTELEFON) + ' - ' + RTRIM(A.IPTELMOVI) AS 'TELEFONO', IPGRUPSAN AS 'GRUPO SANGUINEO', CASE IPRHSANGR WHEN '+' THEN 'Positivo' WHEN '-' THEN 'Negativo' ELSE '!!' END AS 'RH SANGRE', 
		RTRIM(EV.Name) AS 'NOMBRE ENTIDAD', dbo.TipoAfiliado(A.IPTIPOAFI) AS 'TIPO AFILIADO', dbo.TipoPaciente(A.IPTIPOPAC) AS 'TIPO PACIENTE',
		dbo.RangoAfiliacion(A.NIVCODIGO) AS 'NIVEL AFILIACION', dbo.EstadoCivilPaciente(A.IPESTADOC,A.IPSEXOPAC) AS 'ESTADO CIVIL',
		D.DESACTIVI AS PROFESION, G.DESGRUPET AS 'GRUPO ETNICO', [Common].[GETDATE]() AS 'FECHA DE IMPRESION', dbo.DireccionEmpresa() AS 'DIRECCION EMPRESA',
		dbo.TelefonoEmpresa()  AS 'TELEFONO EMPRESA', A.NUMCARPET AS 'NUMERO HISTORIA',  EV.Name AS 'EntidadVie', ISNULL(cama.NUMCAMHOS, '') AS 'CodigoCama',
		B.NUMINGRES AS INGRESO, B.IFECHAING AS 'FECHA DE INGRESO', isnull(EV.Name,RTRIM(K.NOMENTIDA))  AS 'NOMBRE ENTIDAD PACIENTE', I.NOMCENATE AS 'NOMBRE DE LA EMPRESA', 
		I.CODIPSSEC AS 'CODIGO HABILITACION', I.DIRCENATE AS 'DIRECCION CENTRO ATENCION', I.NUMTELCEN AS 'NUMERO TELEFONO CENTRO ATENCION', I.NUMCELCEN AS 'NUMERO CELULAR CENTRO ATENCION',
		(SELECT MUNNOMBRE FROM INMUNICIP WHERE DEPMUNCOD = I.DEPMUNCOD) AS 'NOMBRE MUNICIPIO'
		--[dbo].[GetInunifuncByFolio](@CodigoPaciente, @NumeroIngreso, @NumeroFolio, 1) AS 'SERVICIO-INGRESO',
		--[dbo].[GetInunifuncByFolio](@CodigoPaciente, @NumeroIngreso, @NumeroFolio, 2) AS 'SERVICIO-EGRESO'
		FROM dbo.INPACIENT A With(Nolock)
		INNER JOIN dbo.ADINGRESO B With(Nolock) ON A.IPCODPACI=B.IPCODPACI
		LEFT JOIN dbo.INEntidad C With(Nolock) ON B.CODENTIDA=C.CODENTIDA
		LEFT JOIN dbo.ADACTIVID D With(Nolock) ON A.CODACTIVI=D.CODACTIVI
		LEFT JOIN dbo.INUBICACI E With(Nolock) ON A.AUUBICACI=E.AUUBICACI
		LEFT JOIN dbo.INMUNICIP F With(Nolock) ON E.DEPMUNCOD=F.DEPMUNCOD
		LEFT JOIN dbo.INDEPARTA z With(Nolock) ON z.depcodigo = F.DEPCODIGO 
		LEFT JOIN dbo.INEntidad K With(Nolock) ON A.CODENTIDA=K.CODENTIDA
		INNER JOIN dbo.ADCENATEN I With(Nolock) ON B.CODCENATE=I.CODCENATE
		LEFT JOIN dbo.ADGRUETNI G With(Nolock) ON A.CODGRUPOE=G.CODGRUPOE 
		LEFT OUTER JOIN Contract.healthadministrator EV With(Nolock)  ON EV.Id = B.GENCONENTITY 
		LEFT JOIN  dbo.CHCAMASHO cama With(Nolock) ON B.CODCAMACT = cama.CODICAMAS
		LEFT JOIN Admissions.PatientAddress H With(Nolock) ON A.IPCODPACI = H.IPCODPACI and IsMain = 1 
		WHERE A.IPCODPACI=@CodigoPaciente AND B.NUMINGRES IN (SELECT Value FROM dbo.splitstring(@NumeroIngreso))
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera la cabecera o encabezado de los reportes e historias clínicas de un paciente, combinando datos demográficos, de contacto y administrativos del paciente (cédula, nombre, apellidos, fecha de nacimiento, sexo, identidad de género, grupo sanguíneo, RH, estado civil, grupo étnico, profesión, dirección y teléfono) con información del episodio de ingreso (número de ingreso, fecha de ingreso, cama asignada, centro de atención, código de habilitación, dirección y teléfonos del centro). Recibe como parámetros el código o cédula del paciente y el número de ingreso: si no se proporciona número de ingreso devuelve únicamente los datos generales del paciente tomando el ingreso más reciente, y si se especifica un número de ingreso devuelve además los datos completos del episodio de atención (urgencia, hospitalización, consulta externa u otra modalidad). Integra información de las tablas maestras de pacientes (INPACIENT), ingresos (ADINGRESO), entidades pagadoras o EPS (INENTIDAD, HealthAdministrator), actividades de admisión/profesión (ADACTIVID), ubicación geográfica del paciente (INUBICACI, INMUNICIP, INDEPARTA), grupos étnicos (ADGRUETNI), dirección registrada del paciente (PatientAddress) y centro de atención (ADCENATEN). Se utiliza como base de datos para imprimir encabezados en reportes clínicos, historias clínicas y documentos oficiales del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_CabeceraReportes';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_CabeceraReportes';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve la cabecera consolidada de datos demográficos, de afiliación y del centro de atención del paciente para imprimir reportes clínicos, opcionalmente filtrada por uno o varios ingresos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CabeceraReportes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe existir en INPACIENT con el código suministrado.; Si se especifican ingresos, deben existir en ADINGRESO asociados al paciente y con un centro de atención válido en ADCENATEN.; El parámetro de ingresos, cuando aplica, debe ser una cadena delimitada parseable por dbo.splitstring.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CabeceraReportes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El sexo siempre se reporta como MASCULINO o FEMENINO; cualquier valor distinto de 1 se considera femenino.; Cuando no hay dirección estructurada en Admissions.PatientAddress se reconstruye con la ubicación, municipio y departamento del paciente.; La dirección y teléfono de la empresa se obtienen siempre desde funciones de configuración (DireccionEmpresa, TelefonoEmpresa).; Solo en el flujo con ingresos se considera la dirección principal (IsMain = 1) del paciente.; El flujo sin ingresos siempre devuelve cadenas vacías para datos del centro de atención y del ingreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CabeceraReportes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso/Admisión; Entidad aseguradora (EPS/administradora); Tipo de afiliado; Nivel de afiliación; Estado civil; Identidad de género; Grupo étnico; Grupo sanguíneo y RH; Centro de atención; Código de habilitación; Cama hospitalaria; Historia clínica (número de carpeta); Ubicación geográfica (departamento/municipio)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CabeceraReportes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] INPACIENT: Cuando el número de ingreso viene vacío o con literal ''NOT NULL'', se retorna TOP 1 con los datos del paciente ordenados por fecha de ingreso descendente, sin información de ingreso ni centro de atención.; [RETURN_RESULT] ADINGRESO: Cuando se proveen ingresos, se retorna una fila por cada NUMINGRES contenido en la lista delimitada, incluyendo datos del ingreso, cama, centro de atención y entidad.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CabeceraReportes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @NumeroIngreso = '''' OR @NumeroIngreso = ''NOT NULL'' → Ejecuta consulta resumida (TOP 1) priorizando el ingreso más reciente y omitiendo datos de ingreso/centro de atención (campos en blanco). else Ejecuta consulta extendida con JOIN obligatorio a ADINGRESO y ADCENATEN, filtrando por los ingresos contenidos en la lista delimitada.; si H.Address IS NULL en Admissions.PatientAddress → Construye la dirección concatenando IPDIRECCI con ubicación, municipio y departamento. else Usa la dirección formateada por dbo.Patient_Address.; si IPSEXOPAC = 1 → Reporta sexo ''MASCULINO''. else Reporta sexo ''FEMENINO''.; si IPRHSANGR = ''+'' / ''-'' → Traduce a ''Positivo'' o ''Negativo'' respectivamente. else Devuelve ''!!'' como indicador de RH desconocido.; si EV.Name (healthadministrator) IS NULL en flujo con ingresos → Usa RTRIM(K.NOMENTIDA) de INEntidad como nombre de entidad del paciente. else Usa EV.Name como nombre de entidad.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CabeceraReportes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.TipoDocumento; dbo.TipoAfiliado; dbo.TipoPaciente; dbo.RangoAfiliacion; dbo.EstadoCivilPaciente; dbo.TypeGenderIdentity; dbo.Patient_Address; dbo.DireccionEmpresa; dbo.TelefonoEmpresa; Common.GETDATE; dbo.splitstring', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CabeceraReportes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INPACIENT; dbo.ADINGRESO; dbo.INEntidad; dbo.ADACTIVID; dbo.INUBICACI; dbo.INMUNICIP; dbo.INDEPARTA; dbo.ADGRUETNI; Admissions.PatientAddress; CONTRACT.healthadministrator; dbo.ADCENATEN; dbo.CHCAMASHO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CabeceraReportes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CabeceraReportes';
-- GO
