CREATE PROCEDURE [dbo].[SP_eHC_ListarReporteInterconsulta]
(
	@Centro varchar(20),
	@FechaInicio Datetime ,
	@FechaFin Datetime ,
	@TipoSolicitud Char(1)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Insert statements for procedure here	
		SELECT 
			A.FECORDMED AS 'Fecha Solicitud' ,
			RTRIM(B.CODCENATE) AS 'Codigo Centro de Atencion' ,
			RTRIM(B.NOMCENATE) AS 'Centro de Atencion' ,
			CASE A.ESTSERIPS
				WHEN 1 THEN '1.Solicitado'
				WHEN 2 THEN '2.Solicitud Enviada'
				WHEN 3 THEN '3.Interconsulta Realizada'
				WHEN 4 THEN '4.Extramural'
				WHEN 5 THEN '5.Anulado'
				when 6 THEN '6.Pendiente de verificación por parte del especialista'
			END AS 'Estado',
			RTRIM(C.UFUCODIGO) AS 'Codigo Unidad Funcional' ,
			RTRIM(C.UFUDESCRI) AS 'Unidad Funcional' ,
			dbo.TipoDocumentoNombreCompleto(d.IPTIPODOC) as 'Tipo de Identificacion',
			RTRIM(D.IPCODPACI) AS 'Numero de Identificacion' ,
			RTRIM(D.IPNOMCOMP) AS 'Nombres y Apellidos' ,
			RTRIM(D.IPPRINOMB) AS 'Primer Nombre' , 
			RTRIM(D.IPSEGNOMB) AS 'Segundo Nombre' , 
			RTRIM(D.IPPRIAPEL)AS 'Primer Apellido' , 
			RTRIM(D.IPSEGAPEL) AS 'Segundo Apellido' ,
			D.IPFECNACI AS 'Fecha Nacimiento' ,
			cast ( datediff ( dd, D.IPFECNACI ,GETDATE( ) ) / 365.25 as int ) AS 'Edad en Años' ,
			dbo.EDAD( D.IPFECNACI, [Common].[GETDATE]() ) AS 'Edad Completa' ,
			RTRIM(D.IPDIRECCI) AS 'Direccion' ,
			RTRIM(D.IPTELMOVI) AS 'Celular' ,
			RTRIM(D.IPTELEFON) AS 'Telefono Fijo' , 
			CASE D.IPSEXOPAC
				WHEN 1 THEN 'Masculino'
				WHEN 2 THEN 'Femenino'
			END AS 'Sexo' ,
			RTRIM(J.Code) AS 'Codigo Grupo de Atencion' ,
			RTRIM(J.Name) AS 'Grupo de Atencion' ,
			RTRIM(K.Code) AS 'Codigo Entidad' ,
			RTRIM(K.Name) AS 'Entidad' ,
			RTRIM(F.CODSERIPS) AS 'Codigo de Servicio' ,
			RTRIM(F.DESSERIPS) AS 'Servicio' ,			
			CASE 
				WHEN M.IDMODELOHC IS NULL THEN 'N/A'
				WHEN M.IDMODELOHC IS NOT NULL THEN N.CODIGO
			END AS 'Codigo Modelo HC' ,
			CASE 
				WHEN M.IDMODELOHC IS NULL THEN 
					(CASE M.TIPHISPAC
						WHEN 'I' THEN 'Historia Clinica Ingreso'
						WHEN 'N' THEN 'Nota Evolución'
						WHEN 'E' THEN 'Evolución'
						WHEN 'O' THEN 'Otros modelos de apoyo'
						WHEN 'PT' THEN 'Partograma'
						WHEN 'NF' THEN 'Nota Farmaceutica'
						WHEN 'V' THEN 'Valoración de Seguimiento'
						WHEN 'F' THEN 'Consulta Preanestesia'	
						WHEN 'T' THEN 'Historia clinica de Control'
						WHEN 'S' THEN 'Servicio de apoyo'
						WHEN 'P' THEN 'Atención partos'
						WHEN 'B' THEN 'Recien Nacido'
						WHEN 'JM' THEN 'Junta Médica - Nota Evolución'
						ELSE 'Otro'
					END)
				WHEN M.IDMODELOHC IS NOT NULL THEN N.DESCRIPCION
			END AS 'Modelo HC' ,
			RTRIM(G.CODPROSAL) AS 'Codigo Profesional' ,
			RTRIM(G.NOMMEDICO) AS 'Profesional' ,
			RTRIM(H.CODDIAGNO) AS 'Codigo Dx' , 
			RTRIM(H.NOMDIAGNO) AS 'Diagnostico' , 
			RTRIM(I.CODESPECI) AS 'Codigo Especialidad' ,
			RTRIM(I.DESESPECI) AS 'Especialidad' ,
			CASE 
				WHEN A.CODPROINT IS NULL THEN 'N/A'
				WHEN A.CODPROINT IS NOT NULL THEN RTRIM(L.CODPROSAL)
			END AS 'Codigo Medico' ,			
			CASE 
				WHEN A.CODPROINT IS NULL THEN 'N/A'
				WHEN A.CODPROINT IS NOT NULL THEN RTRIM(L.NOMMEDICO)
			END AS 'Medico' , 
			CASE A.PRISERIPS
				WHEN '1' THEN 'Urgente'
				WHEN '2' THEN 'Rutina'
			END AS 'Prioridad' ,
			CASE A.MANEXTPRO
				WHEN 0 THEN 'Intrahospitalario'
				WHEN 1 THEN 'Extramural'
			END AS 'Tipo de Solicitud' ,
			CASE
				WHEN A.OBSSERIPS IS NULL THEN ''
				WHEN A.OBSSERIPS IS NOT NULL THEN RTRIM(A.OBSSERIPS)
			END AS 'Recomendaciones'	
		FROM
			HCORDINTE AS A with (nolock) 
			INNER JOIN ADCENATEN AS B with (nolock) ON A.CODCENATE = B.CODCENATE
			INNER JOIN INUNIFUNC AS C with (nolock) ON A.UFUCODIGO = C.UFUCODIGO
			INNER JOIN INPACIENT AS D with (nolock) ON A.IPCODPACI = D.IPCODPACI
			INNER JOIN ADINGRESO AS E with (nolock) ON A.NUMINGRES = E.NUMINGRES
			INNER JOIN INCUPSIPS AS F with (nolock) ON A.CODSERIPS = F.CODSERIPS
			INNER JOIN INPROFSAL AS G with (nolock) ON A.CODPROSAL = G.CODPROSAL
			INNER JOIN INDIAGNOS AS H with (nolock) ON A.CODDIAGNO = H.CODDIAGNO
			INNER JOIN INESPECIA AS I with (nolock) ON A.CODESPECI = I.CODESPECI
			INNER JOIN Contract.CareGroup AS J with (nolock) ON E.GENCAREGROUP = J.Id
			INNER JOIN Contract.HealthAdministrator AS K with (nolock) ON E.GENCONENTITY = K.Id
			LEFT JOIN INPROFSAL AS L with (nolock) ON A.CODPROINT = L.CODPROSAL
			INNER JOIN HCHISPACA AS M with (nolock) ON A.IPCODPACI = M.IPCODPACI AND A.NUMINGRES = M.NUMINGRES AND A.NUMEFOLIO = M.NUMEFOLIO AND A.IDETIPHIS = M.IDETIPHIS
			LEFT JOIN PRMODELOHC AS N with (nolock) ON M.IDMODELOHC = N.ID
		 WHERE	
			A.CODCENATE IN (SELECT Value FROM dbo.SplitString(@Centro))
			AND A.FECORDMED BETWEEN  @FechaInicio  AND @FechaFin  AND
				CASE @TipoSolicitud
					WHEN '0' THEN A.MANEXTPRO
					WHEN '1' THEN A.MANEXTPRO
					ELSE CAST (1 AS BIT)
				END
				=
				CASE @TipoSolicitud
					WHEN '0' THEN 0
					WHEN '1' THEN 1
					ELSE CAST (1 AS BIT)
				END
		
-- fin SP
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de interconsultas médicas solicitadas durante un período de fechas, para uno o varios centros de atención, con opción de filtrar por tipo de solicitud (intrahospitalaria o extramural). Consolida en una sola consulta los datos del paciente (cédula, nombre, edad, sexo, dirección, contacto), el ingreso hospitalario, la unidad funcional, el profesional que solicitó la interconsulta, el médico interconsultante, el diagnóstico CIE-10, la especialidad requerida, el servicio CUPS/IPS, la entidad pagadora (EPS/aseguradora), el grupo de atención contractual y el modelo de historia clínica asociado. Está diseñado para reportería operativa y de gestión clínica que permite auditar el estado de cada interconsulta (solicitado, enviado, realizado, extramural, anulado, pendiente de verificación) y la prioridad de atención (urgente o de rutina).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_eHC_ListarReporteInterconsulta';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_eHC_ListarReporteInterconsulta';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte detallado de solicitudes de interconsulta médica filtrado por centros de atención, rango de fechas y tipo de solicitud (intrahospitalaria/extramural).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_eHC_ListarReporteInterconsulta';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de centros debe ser una cadena parseable por dbo.SplitString para producir códigos válidos de CODCENATE.; El rango de fechas debe estar definido (FechaInicio y FechaFin no nulos) para filtrar FECORDMED.; Las órdenes de interconsulta deben tener correspondencia obligatoria en centro, unidad funcional, paciente, ingreso, servicio CUPS, profesional solicitante, diagnóstico, especialidad, grupo de atención del ingreso, entidad administradora e historia clínica (HCHISPACA).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_eHC_ListarReporteInterconsulta';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen interconsultas con ingreso, paciente, servicio, profesional solicitante, diagnóstico, especialidad, grupo de atención, entidad e historia clínica vinculados (INNER JOIN obligatorios).; El médico interconsultante y el modelo de HC son opcionales y se reportan como ''N/A'' cuando no existen.; Las consultas se realizan con NOLOCK, por lo que pueden incluirse lecturas sucias.; La edad se calcula tanto en años enteros (datediff/365.25) como en formato completo vía función EDAD.; El tipo de solicitud Extramural corresponde a MANEXTPRO=1 e Intrahospitalario a MANEXTPRO=0.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_eHC_ListarReporteInterconsulta';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Interconsulta; Solicitud médica; Centro de atención; Unidad funcional; Paciente; Ingreso hospitalario; Diagnóstico; Especialidad; Profesional de salud; Modelo de historia clínica; Grupo de atención; Entidad administradora de salud; Prioridad (Urgente/Rutina); Atención intrahospitalaria vs extramural; Estado de la interconsulta', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_eHC_ListarReporteInterconsulta';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCORDINTE: Devuelve filas de órdenes de interconsulta cuyo CODCENATE pertenece a la lista de centros y FECORDMED está entre FechaInicio y FechaFin, filtrando además por tipo de solicitud (intrahospitalario/extramural) o todas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_eHC_ListarReporteInterconsulta';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TipoSolicitud = ''0'' → Filtra solo órdenes con MANEXTPRO=0 (Intrahospitalario).; si TipoSolicitud = ''1'' → Filtra solo órdenes con MANEXTPRO=1 (Extramural).; si TipoSolicitud distinto de ''0'' y ''1'' → No aplica filtro por tipo de solicitud (incluye todas).; si ESTSERIPS de la orden → Mapea a etiquetas: 1 Solicitado, 2 Solicitud Enviada, 3 Interconsulta Realizada, 4 Extramural, 5 Anulado, 6 Pendiente verificación especialista.; si IDMODELOHC de la historia clínica es NULL → Reporta ''N/A'' como código y mapea TIPHISPAC a descripción del tipo de historia (Ingreso, Evolución, Partograma, Junta Médica, etc.). else Toma código y descripción desde PRMODELOHC.; si CODPROINT de la orden es NULL → Reporta ''N/A'' en código y nombre del médico interconsultante. else Reporta los datos del profesional resuelto vía LEFT JOIN a INPROFSAL.; si PRISERIPS → Mapea prioridad: ''1'' Urgente, ''2'' Rutina.; si IPSEXOPAC → Mapea sexo: 1 Masculino, 2 Femenino.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_eHC_ListarReporteInterconsulta';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.SplitString; dbo.TipoDocumentoNombreCompleto; dbo.EDAD; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_eHC_ListarReporteInterconsulta';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDINTE; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.INPACIENT; dbo.ADINGRESO; dbo.INCUPSIPS; dbo.INPROFSAL; dbo.INDIAGNOS; dbo.INESPECIA; Contract.CareGroup; Contract.HealthAdministrator; dbo.HCHISPACA; dbo.PRMODELOHC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_eHC_ListarReporteInterconsulta';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_eHC_ListarReporteInterconsulta';
-- GO
