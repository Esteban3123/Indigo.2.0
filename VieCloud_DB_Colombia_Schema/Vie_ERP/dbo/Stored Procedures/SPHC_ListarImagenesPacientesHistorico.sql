

/***********************************************************************************************
-- ================================================================================================================================
-- Author:		rafael Patiño
-- Create date: 17/10/2019
-- Description:	Listar historico de imagenes
***********************************************************************************************************************************/
CREATE PROCEDURE [dbo].[SPHC_ListarImagenesPacientesHistorico]
(
@CentroAtencion Char(10),
@Paciente varchar(25)
)
AS
BEGIN
SET NOCOUNT ON;

SELECT A.AUTO,A.FECORDMED AS FechaSolicitud, RTRIM(A.IPCODPACI) AS CodigoPaciente, 
RTRIM(B.IPNOMCOMP) AS NombrePaciente, B.IPFECNACI AS FechaNacimiento, 
E.UFUACTPAC AS CodigoUnidad, RTRIM(C.UFUDESCRI) AS DescripcionUnidad,RTRIM(C2.UFUDESCRI) AS UnidadSolicitante, A.CODCENATE AS CodigoCentro, A.ESTSERIPS AS Estado,A.ESTALEIMG as EstadoAlerta, A.NUMINGRES AS Ingreso, 
A.NUMEFOLIO AS Folio, RTRIM(D .DESSERIPS) AS DescripcionServicio, A.OBSSERIPS AS ObservacionServicio, RTRIM(A.CODSERIPS) AS CodigoServicio, RTRIM(I.NOMDIAGNO) 
AS NombreDiagnostico, G.DESCCAMAS AS Cama,G.CODAISLAM AS TipoAislamiento,
B.IPDIRECCI AS Direccion,b.IPTELEFON AS Telefono,b.IPSEXOPAC  as Sexo,b.IPRHSANGR as Rh, b.IPGRUPSAN as GrupoSanguineo,
B.IPTELMOVI as Movil ,B.CORELEPAC as Correo, H.NOMMEDICO  AS Medico,
A.CONCURRE as Concurrencia,A.SERREAINT as RealizaInterfaz, RTRIM(CA.NOMCENATE) as CentroAtencion, 
A.FECHASUGE , CASE A.LATERALIDAD WHEN 0 THEN 'No Aplica' WHEN 1 THEN 'Izquierda' WHEN 2 THEN 'Derecha' WHEN 3 THEN 'Ambos' END AS 'LATERALIDAD',
CAST('' As Varchar(100)) As Edad,RTRIM(K.NOMENTIDA) AS DescripcionEntidad, 
CASE A.PRISERIPS WHEN '1' THEN 'Urgente' WHEN '2' THEN 'Rutinario' ELSE 'Otro' END AS PRIORIDAD,
CASE A.ESTSERIPS when 1 then 'Solicitado ' when 2 then 'Estudio Realizado ' when 3 then 'Imagen Procesada' when 4 then 'Estudio Interpretado' when 5 then 'Remitido' when 6 then 'Anulado' when 7 then 'Extramural' END as ESTADO
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
 dbo.INENTIDAD AS K with(nolock) ON K.CODENTIDA=E.CODENTIDA 
 WHERE A.ESTSERIPS in(3,4) 
 AND A.IPCODPACI=@Paciente 
 AND A.ESTTRASER='1' 
 AND A.TIENEGRABACION = 1
 order by A.FECORDMED desc

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista el historial completo de imágenes diagnósticas (radiologías, ecografías, tomografías, resonancias, etc.) de un paciente específico en un centro de atención, mostrando únicamente los estudios que ya fueron procesados o interpretados (estados 3 y 4) y que tienen grabación registrada. Consolida información del paciente (cédula, nombre, fecha de nacimiento, sexo, grupo sanguíneo, contacto), del ingreso o admisión (número de ingreso, unidad funcional actual y solicitante, cama asignada, tipo de aislamiento), de la orden de imagen (fecha de solicitud, servicio CUPS, diagnóstico CIE-10, prioridad urgente o rutinaria, lateralidad, estado descriptivo, concurrencia e interfaz), del médico solicitante, del centro de atención y de la entidad o aseguradora. Sirve para que el módulo de historia clínica y los portales de consulta muestren el historial de imágenes ya disponibles de un paciente, integrando las tablas de órdenes de imágenes, catálogo de servicios CUPS, maestro de pacientes, ingresos, unidades funcionales, camas, profesionales de la salud, diagnósticos CIE-10, centros de atención y entidades aseguradoras.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarImagenesPacientesHistorico';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarImagenesPacientesHistorico';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista el histórico de estudios de imágenes diagnósticas de un paciente que ya fueron procesados o interpretados y cuentan con grabación, incluyendo datos clínicos, administrativos y demográficos asociados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesHistorico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe existir en INPACIENT y tener órdenes en HCORDIMAG; Las órdenes deben tener servicio CUPS/IPS, ingreso, unidad funcional, profesional, centro de atención y entidad válidos asociados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesHistorico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen estudios de imagen en estado 3 (Imagen Procesada) o 4 (Estudio Interpretado); Solo se exponen órdenes con grabación disponible (TIENEGRABACION=1); Solo se exponen órdenes con estado de traslado de servicio igual a ''1''; El resultado siempre incluye unidad actual del paciente y unidad solicitante de la orden como entidades distintas; El listado siempre se entrega en orden cronológico descendente por fecha de la orden médica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesHistorico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Orden de imagen diagnóstica; Historia clínica; Ingreso/admisión; Unidad funcional; Cama hospitalaria; Tipo de aislamiento; Diagnóstico (CIE); Profesional tratante; Centro de atención; Entidad responsable de pago; Lateralidad del estudio; Prioridad clínica (urgente/rutinario); Estado del servicio de imagen; Grabación de estudio; Concurrencia; Interfaz de servicio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesHistorico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCORDIMAG: Devuelve solo órdenes con ESTSERIPS IN (3,4) (Imagen Procesada o Estudio Interpretado), ESTTRASER=''1'' y TIENEGRABACION=1, filtradas por el paciente recibido, ordenadas por FECORDMED descendente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesHistorico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si A.LATERALIDAD = 0/1/2/3 → Traduce a ''No Aplica''/''Izquierda''/''Derecha''/''Ambos'' respectivamente; si A.PRISERIPS = ''1'' o ''2'' → Mapea prioridad a ''Urgente'' o ''Rutinario'' else Cualquier otro valor se etiqueta como ''Otro''; si A.ESTSERIPS de 1 a 7 → Traduce a descripción textual del estado: Solicitado, Estudio Realizado, Imagen Procesada, Estudio Interpretado, Remitido, Anulado o Extramural', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesHistorico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDIMAG; dbo.INCUPSIPS; dbo.INPACIENT; dbo.INCUPSSUB; dbo.ADINGRESO; dbo.INUNIFUNC; dbo.CHCAMASHO; dbo.INPROFSAL; dbo.INDIAGNOS; dbo.ADCENATEN; dbo.INENTIDAD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesHistorico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesHistorico';
-- GO
