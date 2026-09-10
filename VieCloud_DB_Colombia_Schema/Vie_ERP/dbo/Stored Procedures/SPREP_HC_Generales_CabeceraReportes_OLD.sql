CREATE PROCEDURE [dbo].[SPREP_HC_Generales_CabeceraReportes_OLD]
(
@CodigoPaciente Varchar(25),
@NumeroIngreso Char(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
          
IF @NumeroIngreso = '' or @NumeroIngreso = 'NOT NULL'

SELECT A.IPCODPACI AS 'CODIGO PACIENTE', dbo.TipoDocumento(A.IPTIPODOC) AS 'TIPO DOCUMENTO', RTRIM(A.IPPRIAPEL) + ' ' + RTRIM(A.IPSEGAPEL) AS APELLIDOS, 
RTRIM(A.IPPRINOMB) + ' ' + RTRIM(A.IPSEGNOMB) AS NOMBRES, A.IPFECNACI AS 'EDAD', '' AS 'FECHA DE NACIMIENTO', RTRIM(IPDIRECCI) + ' - ' + RTRIM(E.UBINOMBRE) + ' - ' + RTRIM(F.MUNNOMBRE) AS DIRECCION, CASE IPSEXOPAC WHEN 1 THEN 'MASCULINO' ELSE 'FEMENINO' END AS SEXO, 
RTRIM(A.IPTELEFON) + ' - ' + RTRIM(A.IPTELMOVI) AS TELEFONO, IPGRUPSAN AS 'GRUPO SANGUINEO', IPRHSANGR AS 'RH SANGRE', RTRIM(C.NOMENTIDA) AS 'NOMBRE ENTIDAD', dbo.TipoAfiliado(A.IPTIPOAFI) AS 'TIPO AFILIADO',
dbo.TipoPaciente(A.IPTIPOPAC) AS 'TIPO PACIENTE', dbo.EstadoCivilPaciente(A.IPESTADOC,A.IPSEXOPAC) AS 'ESTADO CIVIL' , D.DESACTIVI AS PROFESION, G.DESGRUPET AS 'GRUPO ETNICO', [Common].[GETDATE]() AS 'FECHA DE IMPRESION'
--B.NUMINGRES AS INGRESO, B.IFECHAING AS 'FECHA DE INGRESO'
FROM dbo.INPACIENT A  With(Nolock)
--INNER JOIN dbo.ADINGRESO B ON A.IPCODPACI=B.IPCODPACI
INNER JOIN dbo.INEntidad C With(Nolock) ON A.CODENTIDA=C.CODENTIDA
INNER JOIN dbo.ADACTIVID D With(Nolock) ON A.CODACTIVI=D.CODACTIVI
INNER JOIN dbo.INUBICACI E With(Nolock) ON A.AUUBICACI=E.AUUBICACI
INNER JOIN dbo.INMUNICIP F With(Nolock) ON E.DEPMUNCOD=F.DEPMUNCOD
LEFT OUTER JOIN dbo.ADGRUETNI G With(Nolock) ON A.CODGRUPOE=G.CODGRUPOE 
WHERE A.IPCODPACI=@CodigoPaciente

ELSE

SELECT A.IPCODPACI AS 'CODIGO PACIENTE', dbo.TipoDocumento(A.IPTIPODOC) AS 'TIPO DOCUMENTO', RTRIM(A.IPPRIAPEL) + ' ' + RTRIM(A.IPSEGAPEL) AS APELLIDOS, 
RTRIM(A.IPPRINOMB) + ' ' + RTRIM(A.IPSEGNOMB) AS NOMBRES, A.IPFECNACI AS 'EDAD', '' AS 'FECHA DE NACIMIENTO', RTRIM(IPDIRECCI) + ' - ' + RTRIM(E.UBINOMBRE) + ' - ' + RTRIM(F.MUNNOMBRE) AS DIRECCION, CASE IPSEXOPAC WHEN 1 THEN 'MASCULINO' ELSE 'FEMENINO' END AS SEXO, 
RTRIM(A.IPTELEFON) + ' - ' + RTRIM(A.IPTELMOVI) AS TELEFONO, IPGRUPSAN AS 'GRUPO SANGUINEO', IPRHSANGR AS 'RH SANGRE', RTRIM(C.NOMENTIDA) AS 'NOMBRE ENTIDAD', dbo.TipoAfiliado(A.IPTIPOAFI) AS 'TIPO AFILIADO',
dbo.TipoPaciente(A.IPTIPOPAC) AS 'TIPO PACIENTE', dbo.EstadoCivilPaciente(A.IPESTADOC,A.IPSEXOPAC) AS 'ESTADO CIVIL' , D.DESACTIVI AS PROFESION, G.DESGRUPET AS 'GRUPO ETNICO',
B.NUMINGRES AS INGRESO, B.IFECHAING AS 'FECHA DE INGRESO', [Common].[GETDATE]() AS 'FECHA DE IMPRESION'
FROM dbo.INPACIENT A 
INNER JOIN dbo.ADINGRESO B With(Nolock) ON A.IPCODPACI=B.IPCODPACI
INNER JOIN dbo.INEntidad C With(Nolock) ON B.CODENTIDA=C.CODENTIDA
INNER JOIN dbo.ADACTIVID D With(Nolock) ON A.CODACTIVI=D.CODACTIVI
INNER JOIN dbo.INUBICACI E With(Nolock) ON A.AUUBICACI=E.AUUBICACI
INNER JOIN dbo.INMUNICIP F With(Nolock) ON E.DEPMUNCOD=F.DEPMUNCOD
LEFT OUTER JOIN dbo.ADGRUETNI G With(Nolock) ON A.CODGRUPOE=G.CODGRUPOE 
WHERE A.IPCODPACI=@CodigoPaciente AND B.NUMINGRES=@NumeroIngreso

END
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento orientado a la generación del encabezado de reportes de historia clínica. Retorna datos demográficos, de contacto, aseguradora, grupo étnico y profesión del paciente. Cuando se suministra un número de ingreso válido, incluye además el episodio de admisión correspondiente (número y fecha de ingreso) obtenido de la tabla de admisiones; de lo contrario, consulta solo el maestro de pacientes. La sufijo `_OLD` sugiere que es una versión obsoleta o reemplazada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CabeceraReportes_OLD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CabeceraReportes_OLD';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera la cabecera demográfica del paciente para reportes de historia clínica, opcionalmente asociada a un ingreso específico cuando se proporciona.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CabeceraReportes_OLD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe existir en el maestro de pacientes con el código suministrado.; Si se especifica un ingreso (distinto de '''' o ''NOT NULL''), debe existir un registro de ingreso para ese paciente con ese número.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CabeceraReportes_OLD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El sexo se traduce: 1 = MASCULINO, cualquier otro = FEMENINO.; Tipo de documento, tipo de afiliado, tipo de paciente y estado civil se resuelven mediante funciones escalares de dominio.; La dirección se compone concatenando dirección, ubicación y municipio.; El teléfono se compone concatenando teléfono fijo y móvil.; El grupo étnico es opcional (LEFT JOIN), no obligatorio para el paciente.; Siempre incluye la fecha de impresión obtenida desde Common.GETDATE().', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CabeceraReportes_OLD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; ingreso/admisión; entidad aseguradora; tipo de afiliado; tipo de paciente; estado civil; grupo sanguíneo; RH; grupo étnico; ubicación/municipio; actividad/profesión', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CabeceraReportes_OLD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando el número de ingreso es vacío o ''NOT NULL'', retorna datos demográficos del paciente sin información de ingreso (entidad tomada del maestro de pacientes).; [RETURN_RESULT] resultset: Cuando se proporciona un número de ingreso válido, retorna datos demográficos junto con número y fecha de ingreso, tomando la entidad desde el registro de ingreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CabeceraReportes_OLD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Número de ingreso es '''' o ''NOT NULL'' → Consulta sólo el maestro de pacientes y catálogos relacionados; entidad proviene de INPACIENT. else Consulta el maestro de pacientes junto con ADINGRESO filtrando por el número de ingreso; entidad proviene de ADINGRESO e incluye número y fecha de ingreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CabeceraReportes_OLD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.TipoDocumento; dbo.TipoAfiliado; dbo.TipoPaciente; dbo.EstadoCivilPaciente; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CabeceraReportes_OLD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INPACIENT; dbo.ADINGRESO; dbo.INEntidad; dbo.ADACTIVID; dbo.INUBICACI; dbo.INMUNICIP; dbo.ADGRUETNI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CabeceraReportes_OLD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_CabeceraReportes_OLD';
-- GO
