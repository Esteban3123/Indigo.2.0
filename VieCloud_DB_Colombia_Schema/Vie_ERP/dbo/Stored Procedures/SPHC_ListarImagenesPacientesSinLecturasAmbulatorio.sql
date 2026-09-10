
/***********************************************************************************************
Modified By: Carlos Pinzon
Date: 2019.03.15
Description: Realizo modificacion a consulta para traer información de lista imagenes de pacientes sin lectura filtrado por grupo Imagenologia
************************************************************************************************/
CREATE PROCEDURE [dbo].[SPHC_ListarImagenesPacientesSinLecturasAmbulatorio]
(
@CentroAtencion Char(10),
@SubGrupo char(10)
)
AS
BEGIN
SET NOCOUNT ON;

SELECT  AUTO, A.ESTTRASER,A.FECORDMED AS FechaSolicitud, RTRIM(A.IPCODPACI) AS CodigoPaciente, 
RTRIM(B.IPNOMCOMP) AS NombrePaciente, B.IPFECNACI AS FechaNacimiento, 
E.UFUACTPAC AS CodigoUnidad, C.UFUDESCRI AS DescripcionUnidad,C2.UFUDESCRI AS UnidadSolicitante, A.CODCENATE AS CodigoCentro, A.ESTSERIPS AS Estado,A.ESTALEIMG as EstadoAlerta, A.NUMINGRES AS Ingreso, 
RTRIM(D .DESSERIPS) + '. ' + isnull(CD.name,'') AS DescripcionServicio, RTRIM(A.CODSERIPS) AS CodigoServicio, B.IPDIRECCI AS Direccion,b.IPTELEFON AS Telefono,b.IPSEXOPAC  as Sexo,b.IPRHSANGR as Rh, b.IPGRUPSAN as GrupoSanguineo,
B.IPTELMOVI as Movil ,B.CORELEPAC as Correo, H.NOMMEDICO  AS Medico,
A.CONCURRE as Concurrencia,a.CANSERIPS  as Cantidad,A.OBSERVACI AS ObservacionServicio, RTRIM(CA.NOMCENATE) as CentroAtencion,
CAST('' As Varchar(100)) As Edad,RTRIM(K.NOMENTIDA) AS DescripcionEntidad,
E.CODTIPPAC as 'Grupo Poblacion',Case E.CODTIPPAC when 1 then 'Maternas' when 2 then 'Menor de 5 Años' when 3 then 'Adulto Mayor' when 4 then 'Discapacitado' when 5 then 'Población General' else 'Población General'end As TIPOPOBLACION,
dbo.[RiskFactorAlert](A.IPCODPACI, A.NUMINGRES, 1) AS 'AlertaFactoresRiesgo', 
dbo.[RiskFactorAlert](A.IPCODPACI, A.NUMINGRES, 2) AS 'AlertaEscalas'
FROM  dbo.AMBORDIMA  AS A with(nolock) INNER JOIN
 dbo.INPACIENT AS B with(nolock) ON A.IPCODPACI = B.IPCODPACI INNER JOIN
 dbo.INCUPSIPS AS D with(nolock) ON A.CODSERIPS = D .CODSERIPS INNER JOIN
 dbo.ADINGRESO AS E with(nolock) ON A.IPCODPACI = E.IPCODPACI AND A.NUMINGRES = E.NUMINGRES INNER JOIN
 dbo.INUNIFUNC AS C with(nolock) ON E.UFUACTPAC = C.UFUCODIGO INNER JOIN
 dbo.INUNIFUNC AS C2 with(nolock) ON e.UFUACTPAC = C2.UFUCODIGO LEFT OUTER JOIN 
 dbo.INPROFSAL AS H with(nolock) ON A.CODPROSAL =H.CODPROSAL INNER JOIN
 dbo.INCUPSSUB CS with(nolock) on CS.CODGRUSUB=D.CODGRUSUB INNER JOIN
 dbo.ADCENATEN CA with(nolock) On CA.CODCENATE = A.CODCENATE INNER JOIN
 dbo.INENTIDAD AS K with(nolock) ON K.CODENTIDA=E.CODENTIDA LEFT JOIN 
 contract.CUPSEntityContractDescriptions CDD on CDD.Id = A.IDDESCRIPCIONRELACIONADA LEFT JOIN 
 contract.ContractDescriptions  CD on CD.Id = CDD.ContractDescriptionId
 WHERE A.ESTSERIPS in(3,4)  AND  A.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion))  
  AND CS.IDRISGRIMAGE=@subgrupo 
 and ESTTRASER='1' 
 AND A.TIENEGRABACION = 0
 ORDER by A.FECORDMED DESC

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las órdenes de imágenes diagnósticas ambulatorias que aún no tienen lectura registrada (sin grabación de resultado), filtrando por centro de atención y subgrupo de imagenología (por ejemplo, rayos X, ecografía, tomografía). Combina datos del paciente (cédula, nombre, fecha de nacimiento, contacto, grupo sanguíneo, sexo), el ingreso o episodio de atención, la unidad funcional, el profesional solicitante, el servicio CUPS con su descripción de contrato, la entidad aseguradora y alertas de factores de riesgo y escalas clínicas. Se utiliza en los módulos de imagenología ambulatoria para que el personal de radiología identifique estudios pendientes de interpretar y gestione la cola de lectura por sede y tipo de imagen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarImagenesPacientesSinLecturasAmbulatorio';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarImagenesPacientesSinLecturasAmbulatorio';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las órdenes de imágenes diagnósticas ambulatorias pendientes de lectura para un conjunto de centros de atención y un subgrupo de imagenología, enriquecidas con datos del paciente, ingreso, profesional y alertas clínicas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesSinLecturasAmbulatorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir relación válida del paciente en INPACIENT y un ingreso vigente en ADINGRESO con la misma combinación paciente/ingreso de la orden; El servicio CUPS de la orden debe estar clasificado en un subgrupo (INCUPSSUB) cuyo IDRISGRIMAGE coincida con el subgrupo solicitado; El parámetro de centros de atención debe poder dividirse mediante dbo.splitstring en uno o más códigos válidos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesSinLecturasAmbulatorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan órdenes ambulatorias cuyo estado de servicio IPS sea 3 o 4 (pendientes/sin lectura); Se excluyen órdenes que ya tienen grabación asociada (TIENEGRABACION = 0); Se excluyen órdenes que no tengan ESTTRASER=''1'' (traslado/estado de servicio confirmado); El filtro por subgrupo se aplica vía la clasificación de imagenología (IDRISGRIMAGE) del subgrupo CUPS, no por el grupo del servicio directamente; Toda fila devuelta pertenece a uno de los centros de atención recibidos en la lista de entrada; Cada paciente se etiqueta con un único grupo poblacional según CODTIPPAC del ingreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesSinLecturasAmbulatorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Órdenes de imágenes diagnósticas ambulatorias; Lectura de imágenes (pendientes); Subgrupo de imagenología; Centro de atención; Paciente y datos demográficos; Ingreso/admisión; Unidad funcional actual y solicitante; Profesional de la salud (médico solicitante); Entidad/aseguradora; Grupo poblacional (Maternas, Menor de 5 Años, Adulto Mayor, Discapacitado, Población General); Alertas de factores de riesgo y escalas; Concurrencia; Contrato/descripción CUPS por entidad', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesSinLecturasAmbulatorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.AMBORDIMA: Devuelve solo órdenes con ESTSERIPS IN (3,4), ESTTRASER=''1'' y TIENEGRABACION=0, filtradas por centro de atención y subgrupo de imagenología, ordenadas por FECORDMED descendente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesSinLecturasAmbulatorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si E.CODTIPPAC = 1 → Clasifica al paciente como ''Maternas''; si E.CODTIPPAC = 2 → Clasifica al paciente como ''Menor de 5 Años''; si E.CODTIPPAC = 3 → Clasifica al paciente como ''Adulto Mayor''; si E.CODTIPPAC = 4 → Clasifica al paciente como ''Discapacitado''; si E.CODTIPPAC = 5 o cualquier otro valor (incluido NULL) → Clasifica al paciente como ''Población General''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesSinLecturasAmbulatorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.splitstring; dbo.RiskFactorAlert', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesSinLecturasAmbulatorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AMBORDIMA; dbo.INPACIENT; dbo.INCUPSIPS; dbo.ADINGRESO; dbo.INUNIFUNC; dbo.INPROFSAL; dbo.INCUPSSUB; dbo.ADCENATEN; dbo.INENTIDAD; contract.CUPSEntityContractDescriptions; contract.ContractDescriptions', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesSinLecturasAmbulatorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesSinLecturasAmbulatorio';
-- GO
