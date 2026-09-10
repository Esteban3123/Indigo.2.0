-- Stored Procedure

-- =============================================
-- Author:		<Author,Julian Cardozo>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SPHC_ListarServiciosBancosSangre]
(
@CentroAtencion Char(10),
@Estado int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT ROW_NUMBER() OVER (ORDER BY A.FECORDMED DESC) AS NumeroFila, A.FECORDMED AS FechaSolicitud, RTRIM(A.IPCODPACI) AS CodigoPaciente, 
RTRIM(A.IPCODPACI) + ' - ' + RTRIM(B.IPNOMCOMP) AS NombrePaciente, B.IPFECNACI AS FechaNacimiento, 
A.UFUCODIGO AS CodigoUnidad, C.UFUDESCRI AS DescripcionUnidad, A.CODCENATE AS CodigoCentro, A.ESTSERIPS AS Estado,A.ESTALEHEM  as EstadoAlerta, A.NUMINGRES AS Ingreso, 
A.NUMEFOLIO AS Folio, RTRIM(D.DESPRODUC) AS DescripcionServicio, A.OBSSERIPS AS ObservacionServicio, A.CODPRODUC  AS CodigoServicio, RTRIM(I.NOMDIAGNO) 
AS NombreDiagnostico, G.DESCCAMAS AS Cama, g.CODAISLAM AS TipoAislamiento,
b.IPDIRECCI AS Direccion,b.IPTELEFON AS Telefono,b.IPSEXOPAC  as Sexo,b.IPRHSANGR as Rh, b.IPGRUPSAN as GrupoSanguineo,
b.IPTELMOVI as Movil ,b.CORELEPAC as Correo, H.NOMMEDICO  AS Medico, 0 as Minutos,CAST('' as char) as Barra,CAST('' as bit) as Marcar,
a.CONCURRE as Concurrencia

FROM  dbo.HCORDHEMO  AS A with(nolock) INNER JOIN
 dbo.INPACIENT AS B with(nolock) ON A.IPCODPACI = B.IPCODPACI INNER JOIN
 dbo.INUNIFUNC AS C with(nolock) ON A.UFUCODIGO = C.UFUCODIGO INNER JOIN
 dbo.HCHEMPROD  AS D with(nolock) ON A.CODPRODUC = D.CODPRODUC  INNER JOIN
 dbo.ADINGRESO AS E with(nolock) ON A.IPCODPACI = E.IPCODPACI AND A.NUMINGRES = E.NUMINGRES LEFT OUTER JOIN
 dbo.CHCAMASHO AS G with(nolock) ON E.CODCAMACT = G.CODICAMAS INNER JOIN 
 dbo.INPROFSAL AS H with(nolock) ON A.CODPROSAL =H.CODPROSAL  LEFT OUTER JOIN
 dbo.INDIAGNOS AS I with(nolock) ON A.CODDIAGNO = I.CODDIAGNO 
 
 WHERE A.ESTSERIPS=@Estado and A.CODCENATE=@CentroAtencion
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las órdenes de hemoterapia (solicitudes de banco de sangre) registradas para un centro de atención y estado específicos. Combina la orden médica de hemoterapia con los datos del paciente (nombre, fecha de nacimiento, sexo, grupo sanguíneo, RH, contacto), la unidad funcional donde se generó la solicitud, el producto hemoterápico solicitado, el ingreso hospitalario, la cama asignada con su tipo de aislamiento, el médico tratante y el diagnóstico CIE-10 asociado. Se utiliza en el módulo de banco de sangre para que el personal asistencial y de laboratorio consulte y gestione las solicitudes de productos sanguíneos pendientes, en proceso o finalizadas según su estado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarServiciosBancosSangre';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarServiciosBancosSangre';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las órdenes de hemoderivados (banco de sangre) de un centro de atención filtradas por estado, enriqueciendo con datos del paciente, unidad funcional, producto, cama, médico y diagnóstico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarServiciosBancosSangre';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El centro de atención y el estado de la orden deben suministrarse como filtros obligatorios; Cada orden de hemoderivado debe tener paciente, unidad funcional, producto, ingreso y profesional asociados (joins INNER); El ingreso del paciente debe existir y coincidir por código de paciente y número de ingreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarServiciosBancosSangre';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen órdenes del centro de atención y estado solicitados; El resultado se ordena por fecha de solicitud de manera descendente y se numera con ROW_NUMBER; La cama y el diagnóstico son opcionales (LEFT JOIN); los demás vínculos son obligatorios; El nombre del paciente se entrega concatenado como ''código - nombre completo''; Se entregan campos auxiliares fijos: Minutos=0, Barra vacío, Marcar=false, para uso por la capa cliente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarServiciosBancosSangre';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Banco de sangre; Órdenes de hemoderivados; Paciente; Ingreso hospitalario; Unidad funcional; Cama hospitalaria; Tipo de aislamiento; Diagnóstico; Profesional de salud (médico); Estado de alerta hemoterapia; Grupo sanguíneo y Rh; Centro de atención; Concurrencia', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarServiciosBancosSangre';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCORDHEMO: Cuando ESTSERIPS = @Estado y CODCENATE = @CentroAtencion, devuelve el listado de órdenes de hemoderivados con datos asociados, numerado por fecha de solicitud descendente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarServiciosBancosSangre';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDHEMO; dbo.INPACIENT; dbo.INUNIFUNC; dbo.HCHEMPROD; dbo.ADINGRESO; dbo.CHCAMASHO; dbo.INPROFSAL; dbo.INDIAGNOS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarServiciosBancosSangre';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarServiciosBancosSangre';
-- GO
