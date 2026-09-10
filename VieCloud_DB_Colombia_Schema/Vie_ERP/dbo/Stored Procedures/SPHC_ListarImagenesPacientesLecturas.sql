

/***********************************************************************************************
-- ================================================================================================================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- ================================================================================================================================
-- Modified By: Carlos Pinzon
-- Date: 2019.03.15
-- Description: Realizo modificacion a consulta para traer información de transcripciones de pacientes filtrado por grupo Imagenologia
-- ================================================================================================================================
***********************************************************************************************************************************/
CREATE PROCEDURE [dbo].[SPHC_ListarImagenesPacientesLecturas]
(
@Validado bit,
@Transcripcion bit,
@CentroAtencion Char(500),
@SubGrupo char(10),
@Profesional char(20)
)
AS
BEGIN
SET NOCOUNT ON;

IF @Profesional IS NULL

SELECT AUTO, A.FECORDMED AS FechaSolicitud, RTRIM(A.IPCODPACI) AS CodigoPaciente, 
RTRIM(B.IPNOMCOMP) AS NombrePaciente, B.IPFECNACI AS FechaNacimiento, 
E.UFUACTPAC AS CodigoUnidad, RTRIM(C.UFUDESCRI) AS DescripcionUnidad,RTRIM(C2.UFUDESCRI) AS UnidadSolicitante, A.CODCENATE AS CodigoCentro, A.ESTSERIPS AS Estado,A.ESTALEIMG as EstadoAlerta, A.NUMINGRES AS Ingreso, 
A.NUMEFOLIO AS Folio, RTRIM(D .DESSERIPS) + '. ' + isnull(CD.name,'') AS DescripcionServicio, A.OBSSERIPS AS ObservacionServicio, RTRIM(A.CODSERIPS) AS CodigoServicio, RTRIM(I.NOMDIAGNO) 
AS NombreDiagnostico, G.DESCCAMAS AS Cama,G.CODAISLAM AS TipoAislamiento,
B.IPDIRECCI AS Direccion,b.IPTELEFON AS Telefono,b.IPSEXOPAC  as Sexo,b.IPRHSANGR as Rh, b.IPGRUPSAN as GrupoSanguineo,
B.IPTELMOVI as Movil ,B.CORELEPAC as Correo, H.NOMMEDICO  AS Medico,
A.CONCURRE as Concurrencia,A.SERREAINT as RealizaInterfaz,a.CANSERIPS  as Cantidad, RTRIM(CA.NOMCENATE) as CentroAtencion,
A.FECHASUGE , CASE A.LATERALIDAD WHEN 0 THEN 'No Aplica' WHEN 1 THEN 'Izquierda' WHEN 2 THEN 'Derecha' WHEN 3 THEN 'Ambos' END AS 'LATERALIDAD',
CAST('' As Varchar(100)) As Edad,RTRIM(K.NOMENTIDA) AS DescripcionEntidad, 
CASE A.PRISERIPS WHEN '1' THEN 'Urgente' WHEN '2' THEN 'Rutinario' ELSE 'Otro' END AS PRIORIDAD

FROM  dbo.HCORDIMAG  AS A with(nolock) INNER JOIN
 dbo.INCUPSIPS AS D with(nolock) ON A.CODSERIPS = D .CODSERIPS INNER JOIN
 dbo.INCUPSSUB CS with(nolock) on CS.CODGRUSUB=D.CODGRUSUB INNER JOIN
 dbo.INPACIENT AS B with(nolock) ON A.IPCODPACI = B.IPCODPACI INNER JOIN
 dbo.ADINGRESO AS E with(nolock) ON A.IPCODPACI = E.IPCODPACI AND A.NUMINGRES = E.NUMINGRES INNER JOIN
 dbo.INUNIFUNC AS C with(nolock) ON E.UFUACTPAC = C.UFUCODIGO INNER JOIN
 dbo.INUNIFUNC AS C2 with(nolock) ON A.UFUCODIGO = C2.UFUCODIGO LEFT OUTER JOIN 
 dbo.CHCAMASHO AS G with(nolock) ON E.CODCAMACT = G.CODICAMAS INNER JOIN 
 dbo.INPROFSAL AS H with(nolock) ON A.CODPROSAL =H.CODPROSAL  LEFT OUTER JOIN
 dbo.INDIAGNOS AS I with(nolock) ON A.CODDIAGNO = I.CODDIAGNO INNER JOIN
 dbo.ADCENATEN CA with(nolock) On CA.CODCENATE = A.CODCENATE INNER JOIN
 dbo.INENTIDAD AS K with(nolock) ON K.CODENTIDA=E.CODENTIDA LEFT JOIN 
 contract.CUPSEntityContractDescriptions CDD on CDD.Id = A.IDDESCRIPCIONRELACIONADA LEFT JOIN 
 contract.ContractDescriptions  CD on CD.Id = CDD.ContractDescriptionId
 WHERE A.ESTSERIPS in (3,4) 
 AND A.CODCENATE=@CentroAtencion 
 AND CS.IDRISGRIMAGE=@subgrupo 
 AND A.SERTRANSC=@Transcripcion 
 AND A.SERVALMED=@Validado 
 AND A.ESTTRASER='1' 
 AND A.TIENEGRABACION = 1
  ORDER by A.FECORDMED DESC
ELSE

SELECT AUTO,A.FECORDMED AS FechaSolicitud, RTRIM(A.IPCODPACI) AS CodigoPaciente, 
RTRIM(B.IPNOMCOMP) AS NombrePaciente, B.IPFECNACI AS FechaNacimiento, 
E.UFUACTPAC AS CodigoUnidad, RTRIM(C.UFUDESCRI) AS DescripcionUnidad,RTRIM(C2.UFUDESCRI) AS UnidadSolicitante, A.CODCENATE AS CodigoCentro, A.ESTSERIPS AS Estado,A.ESTALEIMG as EstadoAlerta, A.NUMINGRES AS Ingreso, 
A.NUMEFOLIO AS Folio, RTRIM(D .DESSERIPS) + '. ' + isnull(CD.name,'') AS DescripcionServicio, A.OBSSERIPS AS ObservacionServicio, RTRIM(A.CODSERIPS) AS CodigoServicio, RTRIM(I.NOMDIAGNO) 
AS NombreDiagnostico, G.DESCCAMAS AS Cama,G.CODAISLAM AS TipoAislamiento,
B.IPDIRECCI AS Direccion,b.IPTELEFON AS Telefono,b.IPSEXOPAC  as Sexo,b.IPRHSANGR as Rh, b.IPGRUPSAN as GrupoSanguineo,
B.IPTELMOVI as Movil ,B.CORELEPAC as Correo, H.NOMMEDICO  AS Medico,
A.CONCURRE as Concurrencia,A.SERREAINT as RealizaInterfaz, RTRIM(CA.NOMCENATE) as CentroAtencion, 
A.FECHASUGE , CASE A.LATERALIDAD WHEN 0 THEN 'No Aplica' WHEN 1 THEN 'Izquierda' WHEN 2 THEN 'Derecha' WHEN 3 THEN 'Ambos' END AS 'LATERALIDAD',
CAST('' As Varchar(100)) As Edad,RTRIM(K.NOMENTIDA) AS DescripcionEntidad, 
CASE A.PRISERIPS WHEN '1' THEN 'Urgente' WHEN '2' THEN 'Rutinario' ELSE 'Otro' END AS PRIORIDAD

FROM  dbo.HCORDIMAG  AS A with(nolock) INNER JOIN
 dbo.INCUPSIPS AS D with(nolock) ON A.CODSERIPS = D .CODSERIPS INNER JOIN
 dbo.INPACIENT AS B with(nolock) ON A.IPCODPACI = B.IPCODPACI INNER JOIN
 dbo.INCUPSSUB CS with(nolock) on CS.CODGRUSUB=D.CODGRUSUB INNER JOIN
 dbo.ADINGRESO AS E with(nolock) ON A.IPCODPACI = E.IPCODPACI AND A.NUMINGRES = E.NUMINGRES INNER JOIN
 dbo.INUNIFUNC AS C with(nolock) ON E.UFUACTPAC = C.UFUCODIGO INNER JOIN
 dbo.INUNIFUNC AS C2 with(nolock) ON A.UFUCODIGO = C2.UFUCODIGO LEFT OUTER JOIN 
 dbo.CHCAMASHO AS G with(nolock) ON E.CODCAMACT = G.CODICAMAS INNER JOIN 
 dbo.INPROFSAL AS H with(nolock) ON A.CODPROSAL =H.CODPROSAL  LEFT OUTER JOIN
 dbo.INDIAGNOS AS I with(nolock) ON A.CODDIAGNO = I.CODDIAGNO INNER JOIN
 dbo.ADCENATEN CA with(nolock) On CA.CODCENATE = A.CODCENATE INNER JOIN
 dbo.INENTIDAD AS K with(nolock) ON K.CODENTIDA=E.CODENTIDA LEFT JOIN 
 contract.CUPSEntityContractDescriptions CDD on CDD.Id = A.IDDESCRIPCIONRELACIONADA LEFT JOIN 
 contract.ContractDescriptions  CD on CD.Id = CDD.ContractDescriptionId 
 WHERE A.ESTSERIPS in(3,4) 
 AND  A.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) 
 AND CS.IDRISGRIMAGE=@subgrupo 
 AND A.SERTRANSC=@Transcripcion 
 AND A.SERVALMED=@Validado 
 AND A.CODPROVAL=@Profesional 
 AND A.ESTTRASER='1' 
 AND A.TIENEGRABACION = 1
  ORDER by A.FECORDMED DESC
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las órdenes de imágenes diagnósticas (radiologías, ecografías, tomografías, resonancias, entre otras) que tienen grabación registrada, filtradas por centro de atención, subgrupo de imagenología, estado de transcripción y validación médica. Para cada orden devuelve datos completos del paciente (cédula, nombre, fecha de nacimiento, sexo, grupo sanguíneo, contacto), del ingreso hospitalario (número de ingreso, unidad funcional actual y solicitante, cama asignada con tipo de aislamiento), del servicio CUPS solicitado (código, descripción, prioridad, lateralidad, folio, concurrencia), del profesional médico que la ordenó y del diagnóstico CIE-10 asociado. Permite filtrar opcionalmente por un profesional específico; si no se indica profesional, trae todas las órdenes del centro y subgrupo sin ese filtro. Es el procedimiento central del módulo de lectura e interpretación de imágenes diagnósticas, usado por radiólogos y coordinadores para gestionar la cola de estudios pendientes de transcripción o validación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarImagenesPacientesLecturas';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarImagenesPacientesLecturas';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las órdenes de imágenes diagnósticas con grabación disponible para los flujos de transcripción/validación radiológica, filtrando por centro, subgrupo de imagenología y opcionalmente por profesional validador.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesLecturas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El subgrupo proporcionado debe corresponder a un grupo de imagenología (IDRISGRIMAGE en INCUPSSUB).; Cuando se especifica profesional, el centro de atención puede ser una lista delimitada parseable por dbo.splitstring; cuando no, se espera un único código de centro.; Las órdenes deben tener grabación asociada (TIENEGRABACION=1) para ser visibles en la cola de lectura.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesLecturas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan órdenes en estado de servicio 3 o 4 (estados habilitados para lectura/validación).; Solo se incluyen órdenes con estado de traslado de servicio =''1''.; Solo se incluyen órdenes con grabación (TIENEGRABACION=1).; El subgrupo del CUPS debe pertenecer al grupo de imagenología configurado (IDRISGRIMAGE).; Los resultados se ordenan por fecha de orden médica descendente (más recientes primero).; Las banderas de transcripción (SERTRANSC) y validación médica (SERVALMED) deben coincidir con los parámetros, separando colas pendientes vs. realizadas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesLecturas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Órdenes de imágenes diagnósticas; Transcripción radiológica; Validación médica; Lectura/interpretación de imágenes; Subgrupo de imagenología; Centro de atención; Unidad funcional solicitante y actual; Paciente; Ingreso/admisión; Cama y aislamiento; Diagnóstico (CIE); Profesional de la salud; Entidad/aseguradora; Lateralidad; Prioridad (Urgente/Rutinario); Estado de alerta de imagen; Grabación de estudio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesLecturas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCORDIMAG: Devuelve órdenes con ESTSERIPS IN (3,4), ESTTRASER=''1'', TIENEGRABACION=1, filtradas por centro, subgrupo de imagenología, banderas de transcripción y validación; si hay profesional, además filtra por CODPROVAL=@Profesional.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesLecturas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Profesional IS NULL → Ejecuta consulta filtrando por un único centro (A.CODCENATE=@CentroAtencion) e incluye la columna Cantidad (CANSERIPS) en el resultado. else Ejecuta consulta permitiendo múltiples centros mediante dbo.splitstring(@CentroAtencion), agrega filtro A.CODPROVAL=@Profesional y omite la columna Cantidad.; si A.LATERALIDAD (0/1/2/3) → Traduce a ''No Aplica''/''Izquierda''/''Derecha''/''Ambos'' respectivamente.; si A.PRISERIPS (''1''/''2''/otro) → Traduce prioridad a ''Urgente''/''Rutinario''/''Otro''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesLecturas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.splitstring', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesLecturas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDIMAG; dbo.INCUPSIPS; dbo.INCUPSSUB; dbo.INPACIENT; dbo.ADINGRESO; dbo.INUNIFUNC; dbo.CHCAMASHO; dbo.INPROFSAL; dbo.INDIAGNOS; dbo.ADCENATEN; dbo.INENTIDAD; contract.CUPSEntityContractDescriptions; contract.ContractDescriptions', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesLecturas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesLecturas';
-- GO
