CREATE PROCEDURE [dbo].[SP_REP_CertificadosAsistencialDatos]
(
  @Id as Int
)

AS
BEGIN
  SET NOCOUNT ON;

   select  A.TEXTOCERTI AS Text,A.NUMINGRES AS NumeroIngreso,A.FECHAREGISTRO As FechaRegistro, B.UFUCODIGO AS CodigoUnidad, B.UFUDESCRI AS UnidadDescripcion, C.CODCENATE AS CodigoAtencion,
		 C.NOMCENATE AS NombreAtencion, A.CODESPECIALIDAD AS CodigoEspecialidad, RTRIM(D.DESESPECI) AS NombreEspecialidad, A.IDCERTIFICADO AS IDCertificado, ISNULL(E.DESCRIPLA,'No selecciono plantilla') AS NombrePlantilla, 
		 A.USUARIOREGISTO AS CodigoUsuario, F.DESCARUSU AS NombreProfesion, A.IPCODEPACI AS IdPaciente, G.IPNOMCOMP AS NombrePaciente, G.IPFECNACI as DOB,
		 H.NOMMEDICO As NombreMedico, H.TARJETAPR As TerjetaProfesional,H.MEDIFIRMA AS FirmaProfesional

FROM HCCERTIASISTENCIAL A with (nolock) 
		INNER JOIN INUNIFUNC B with (nolock) ON A.UFUCODIGO = B.UFUCODIGO 
		INNER JOIN ADCENATEN C with (nolock) ON A.CODCENATE = C.CODCENATE 
		INNER JOIN INESPECIA D with (nolock) ON A.CODESPECIALIDAD = D.CODESPECI 
		LEFT JOIN  HCPLANDOC E with (nolock) ON A.IDCERTIFICADO = E.CODCONSEC --SE DEJA LEFT PORQUE ES POSIBLE QUE EL USUARIO DILIGENCIA CERTIFICADIOS ASISTENCIALES Y NO SELECCIONE UNA PLANTILLA COMO TAL
		INNER JOIN SEGusuaru F with (nolock) ON A.USUARIOREGISTO = F.CODUSUARI 
		INNER JOIN INPACIENT G with (nolock) ON A.IPCODEPACI = G.IPCODPACI 
		INNER JOIN INPROFSAL H with (nolock) ON A.USUARIOREGISTO = H.CODUSUARI
WHERE A.ID=@Id

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recupera el detalle completo de un certificado de asistencia médica a partir de su identificador interno. Combina el texto del certificado, el número de ingreso y la fecha de registro con información del paciente (nombre, documento, fecha de nacimiento), el profesional de la salud que lo emitió (nombre, tarjeta profesional, firma), la unidad funcional, el centro de atención y la especialidad médica involucrada. También incluye el nombre de la plantilla de documento clínico utilizada, o indica que no se seleccionó ninguna. Se usa para imprimir o visualizar certificados asistenciales en la historia clínica del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_REP_CertificadosAsistencialDatos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_REP_CertificadosAsistencialDatos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtener los datos consolidados de un certificado asistencial (texto, paciente, médico, especialidad, unidad, centro de atención y plantilla) para alimentar su reporte impreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REP_CertificadosAsistencialDatos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe proporcionarse un identificador de certificado asistencial existente en HCCERTIASISTENCIAL.; Deben existir registros relacionados en las tablas maestras de unidad funcional, centro de atención, especialidad, usuario, paciente y profesional de la salud para el certificado consultado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REP_CertificadosAsistencialDatos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retorna información de un certificado asistencial cuyo identificador coincida exactamente con el parámetro recibido.; El certificado debe estar asociado a una unidad funcional, centro de atención y especialidad existentes; de lo contrario no se retorna registro.; El usuario que registró el certificado debe existir simultáneamente como usuario del sistema y como profesional de la salud para que se retorne información.; El paciente referenciado por el certificado debe existir en el maestro de pacientes.; Si no se asoció una plantilla al certificado, se retorna la leyenda ''No selecciono plantilla'' en lugar de un valor nulo.; Las consultas se realizan con NOLOCK, asumiendo tolerancia a lecturas sucias para fines de reporte.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REP_CertificadosAsistencialDatos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Certificado asistencial; Paciente; Profesional de la salud; Especialidad; Unidad funcional; Centro de atención; Plantilla de documento clínico; Firma profesional; Tarjeta profesional', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REP_CertificadosAsistencialDatos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCCERTIASISTENCIAL: Cuando el certificado asistencial existe y todas sus referencias maestras están presentes, se retorna un único conjunto de resultados con los datos del certificado, paciente, médico y plantilla.; [RETURN_RESULT] dbo.HCPLANDOC: Cuando el certificado no tiene plantilla asociada (HCPLANDOC sin coincidencia por LEFT JOIN), el campo de nombre de plantilla se retorna como ''No selecciono plantilla''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REP_CertificadosAsistencialDatos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCCERTIASISTENCIAL; dbo.INUNIFUNC; dbo.ADCENATEN; dbo.INESPECIA; dbo.HCPLANDOC; dbo.SEGusuaru; dbo.INPACIENT; dbo.INPROFSAL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REP_CertificadosAsistencialDatos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REP_CertificadosAsistencialDatos';
-- GO
