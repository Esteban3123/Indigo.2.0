
CREATE PROCEDURE [dbo].[SPCH_ListarPacientesAuditorExterno]
(
@CodigoUsuario Varchar(25),
@INDEntidad Char(3)
)
AS
BEGIN

		SELECT 
				TOP 1
				A.IPCODPACI 'CODIGO PACIENTE',
				B.IPNOMCOMP 'NOMBRE PACIENTE',
				A.NUMINGRES 'NUMERO INGRESO',
				A.FECHISPAC 'FECHA HISTORIA',
				C.NOMDIAGNO 'DIAGNOSTICO', 
				A.NUMEFOLIO 'NUMERO FOLIO'
			FROM 
				HCHISPACA A
				INNER JOIN 
				INPACIENT B
				ON A.IPCODPACI = B.IPCODPACI AND A.NUMEFOLIO !='' AND A.NUMINGRES !=''
				INNER JOIN 
				INDIAGNOS C
				ON A.CODDIAGNO =  C.CODDIAGNO 
				WHERE B.CODENTIDA ='00013    '

end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que retorna un ejemplo de paciente (primer registro disponible) para un auditor externo, mostrando su cédula, nombre completo, número de ingreso, fecha de la historia clínica, diagnóstico principal y número de folio. Combina las historias clínicas (HCHISPACA), el maestro de pacientes (INPACIENT) y el catálogo de diagnósticos CIE-10 (INDIAGNOS), filtrando únicamente los pacientes pertenecientes a la entidad con código ''00013''. Se utiliza para que un auditor externo pueda verificar o previsualizar datos clínicos de un paciente con historia clínica y diagnóstico registrados, recibiendo como parámetros el usuario y la entidad auditora.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_ListarPacientesAuditorExterno';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_ListarPacientesAuditorExterno';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve un único registro con los datos básicos de un paciente (identificación, ingreso, historia, diagnóstico y folio) asociado a una entidad específica para revisión por auditor externo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesAuditorExterno';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros en HCHISPACA con NUMEFOLIO y NUMINGRES no vacíos; El paciente en INPACIENT debe pertenecer a la entidad con código ''00013''; El diagnóstico referenciado debe existir en INDIAGNOS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesAuditorExterno';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran historias clínicas con NUMEFOLIO distinto de vacío y NUMINGRES distinto de vacío; Filtra exclusivamente pacientes de la entidad ''00013'' (valor hardcodeado, ignorando el parámetro de entidad recibido); Devuelve a lo sumo una fila (TOP 1) sin ORDER BY, por lo que el resultado es no determinista; Los parámetros de entrada no se utilizan dentro de la consulta', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesAuditorExterno';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Historia clínica; Ingreso; Diagnóstico; Folio; Entidad (aseguradora/convenio); Auditor externo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesAuditorExterno';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCHISPACA: Retorna TOP 1 fila con datos del paciente, ingreso, historia, diagnóstico y folio cuando el paciente pertenece a la entidad ''00013'' y existen NUMEFOLIO y NUMINGRES no vacíos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesAuditorExterno';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA; dbo.INPACIENT; dbo.INDIAGNOS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesAuditorExterno';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesAuditorExterno';
-- GO
