

/***********************************************************************************************
-- ================================================================================================================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- ================================================================================================================================
-- Modified By: Carlos Pinzon
-- Date: 2019.03.15
-- Description: Realizo modificacion a consulta para traer imagenes de pacientes lecturas ambulatorio filtrado por grupo Imagenologia
-- ================================================================================================================================
***********************************************************************************************************************************/
CREATE PROCEDURE [dbo].[SPHC_ListarImagenesPacientesLecturasAmbulatorio]
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

SELECT  AUTO, A.ESTTRASER,A.FECORDMED AS FechaSolicitud, RTRIM(A.IPCODPACI) AS CodigoPaciente, RTRIM(A.IPCODPACI)  as 'Identificación', 
RTRIM(B.IPNOMCOMP) AS NombrePaciente, B.IPFECNACI AS FechaNacimiento, 
E.UFUACTPAC AS CodigoUnidad, C.UFUDESCRI AS DescripcionUnidad,C2.UFUDESCRI AS UnidadSolicitante, A.CODCENATE AS CodigoCentro, A.ESTSERIPS AS Estado,A.ESTALEIMG as EstadoAlerta, A.NUMINGRES AS Ingreso, 
RTRIM(D .DESSERIPS) + '. ' + isnull(CD.name,'') AS DescripcionServicio, RTRIM(A.CODSERIPS) AS CodigoServicio, B.IPDIRECCI AS Direccion,b.IPTELEFON AS Telefono,b.IPSEXOPAC  as Sexo,b.IPRHSANGR as Rh, b.IPGRUPSAN as GrupoSanguineo,
B.IPTELMOVI as Movil ,B.CORELEPAC as Correo, H.NOMMEDICO  AS Medico,
A.CONCURRE as Concurrencia,a.CANSERIPS  as Cantidad,A.OBSERVACI AS ObservacionServicio, RTRIM(CA.NOMCENATE) as CentroAtencion,
CAST('' As Varchar(100)) As Edad,RTRIM(K.NOMENTIDA) AS DescripcionEntidad,
E.CODTIPPAC as 'Grupo Poblacion',Case E.CODTIPPAC when 1 then 'Maternas' when 2 then 'Menor de 5 Años' when 3 then 'Adulto Mayor' when 4 then 'Discapacitado' when 5 then 'Población General' else 'Población General'end As TIPOPOBLACION

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
 WHERE A.ESTSERIPS  in(3,4) 
 AND A.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) 
 AND CS.IDRISGRIMAGE=@subgrupo  
 AND A.SERTRANSC=@Transcripcion 
 AND A.SERVALMED=@Validado 
 AND (ESTTRASER = '1' or ESTTRASER is null)
 AND A.TIENEGRABACION = 1
 ORDER by A.FECORDMED DESC

ELSE

SELECT  AUTO, A.ESTTRASER,A.FECORDMED AS FechaSolicitud, RTRIM(A.IPCODPACI) AS CodigoPaciente, RTRIM(A.IPCODPACI)  as 'Identificación', 
RTRIM(B.IPNOMCOMP) AS NombrePaciente, B.IPFECNACI AS FechaNacimiento, 
E.UFUACTPAC AS CodigoUnidad, C.UFUDESCRI AS DescripcionUnidad,C2.UFUDESCRI AS UnidadSolicitante, A.CODCENATE AS CodigoCentro, A.ESTSERIPS AS Estado,A.ESTALEIMG as EstadoAlerta, A.NUMINGRES AS Ingreso, 
RTRIM(D .DESSERIPS) + '. ' + isnull(CD.name,'') AS DescripcionServicio, A.CODSERIPS AS CodigoServicio,
B.IPDIRECCI AS Direccion,b.IPTELEFON AS Telefono,b.IPSEXOPAC  as Sexo,b.IPRHSANGR as Rh, b.IPGRUPSAN as GrupoSanguineo,
B.IPTELMOVI as Movil ,B.CORELEPAC as Correo, H.NOMMEDICO  AS Medico,
A.CONCURRE as Concurrencia,A.OBSERVACI AS ObservacionServicio, RTRIM(CA.NOMCENATE) as CentroAtencion,
CAST('' As Varchar(100)) As Edad,RTRIM(K.NOMENTIDA) AS DescripcionEntidad,
E.CODTIPPAC as 'Grupo Poblacion',Case E.CODTIPPAC when 1 then 'Maternas' when 2 then 'Menor de 5 Años' when 3 then 'Adulto Mayor' when 4 then 'Discapacitado' when 5 then 'Población General' else 'Población General'end As TIPOPOBLACION

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

 WHERE A.ESTSERIPS in(3,4) 
 AND  A.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) 
 AND CS.IDRISGRIMAGE=@subgrupo  
 AND A.SERTRANSC=@Transcripcion 
 AND A.SERVALMED=@Validado 
 AND A.CODPROVAL=@Profesional  
 AND (ESTTRASER = '1' or ESTTRASER is null)
 AND A.TIENEGRABACION = 1
 ORDER by A.FECORDMED DESC

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las órdenes de imágenes diagnósticas ambulatorias pendientes de lectura o validación médica, filtrando por centro de atención, subgrupo de imagenología (por ejemplo radiología, ecografía, tomografía), estado de transcripción, estado de validación y opcionalmente por profesional lector. Combina datos del paciente (identificación, nombre, fecha de nacimiento, datos de contacto, grupo sanguíneo), del ingreso ambulatorio (unidad funcional, entidad aseguradora, tipo de población), del servicio CUPS solicitado y del médico asignado. Se utiliza en el módulo de imagenología para que el radiólogo o especialista visualice la lista de estudios grabados que requieren lectura o dictamen, diferenciando si se filtra por un profesional específico o se muestran todos los pendientes del centro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarImagenesPacientesLecturasAmbulatorio';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarImagenesPacientesLecturasAmbulatorio';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las órdenes de imágenes diagnósticas ambulatorias con grabación disponible, filtradas por centros de atención, subgrupo de imagenología y estados de transcripción/validación, opcionalmente acotadas a un profesional validador.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesLecturasAmbulatorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@CentroAtencion debe ser una cadena parseable por dbo.splitstring con códigos de centro de atención válidos.; @SubGrupo debe corresponder a un IDRISGRIMAGE existente en INCUPSSUB.; Los flags @Validado y @Transcripcion deben coincidir con los valores almacenados en SERVALMED y SERTRANSC respectivamente.; Si @Profesional no es NULL, debe corresponder al CODPROVAL del registro a filtrar.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesLecturasAmbulatorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan órdenes con ESTSERIPS en (3,4).; Solo se retornan órdenes con grabación asociada (TIENEGRABACION=1).; Se excluyen órdenes con ESTTRASER distinto de ''1'' y no nulo (trasladadas).; El filtro de centros de atención siempre se aplica vía splitstring sobre @CentroAtencion.; El subgrupo de imagenología (IDRISGRIMAGE) siempre se aplica como filtro obligatorio.; La población se clasifica en: 1=Maternas, 2=Menor de 5 Años, 3=Adulto Mayor, 4=Discapacitado, 5/otros=Población General.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesLecturasAmbulatorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Órdenes de imágenes diagnósticas ambulatorias; Imagenología (subgrupo RIS); Transcripción de servicio; Validación médica; Centro de atención; Unidad funcional; Profesional de salud (médico validador); Ingreso/admisión; Entidad responsable de pago; Grupo poblacional (Maternas, Menor de 5 Años, Adulto Mayor, Discapacitado, Población General); Estado de traslado del servicio; Grabación de imagen; Concurrencia', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesLecturasAmbulatorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.AMBORDIMA: Devuelve órdenes de imagen donde ESTSERIPS IN (3,4), TIENEGRABACION=1, (ESTTRASER=''1'' o NULL), filtradas por centro, subgrupo de imagenología, transcripción y validación; ordenado por FECORDMED DESC.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesLecturasAmbulatorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Profesional IS NULL → Ejecuta consulta sin filtro por CODPROVAL, devolviendo además la columna Cantidad (CANSERIPS). else Aplica filtro adicional A.CODPROVAL=@Profesional y omite la columna Cantidad en el resultado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesLecturasAmbulatorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.splitstring', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesLecturasAmbulatorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AMBORDIMA; dbo.INPACIENT; dbo.INCUPSIPS; dbo.ADINGRESO; dbo.INUNIFUNC; dbo.INPROFSAL; dbo.INCUPSSUB; dbo.ADCENATEN; dbo.INENTIDAD; contract.CUPSEntityContractDescriptions; contract.ContractDescriptions', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesLecturasAmbulatorio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesLecturasAmbulatorio';
-- GO
