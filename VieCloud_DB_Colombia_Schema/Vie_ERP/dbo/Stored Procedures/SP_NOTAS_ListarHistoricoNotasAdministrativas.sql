
CREATE PROCEDURE [dbo].[SP_NOTAS_ListarHistoricoNotasAdministrativas]
(
@INDPaciente Varchar(25)

)
AS
BEGIN
	SET NOCOUNT ON;
	
	Select 
		'Notas administrativas' AS Tipo,
		Rtrim(D.CODIGO) AS 'CODIGO', 
		A.ID, 
		IPCODPACI, 
		FECHACREACION, 
		Rtrim(C.NOMCENATE) as 'Centro Atencion', 
		Rtrim(b.NOMUSUARI) as 'Usuario', 
		NUMINGRES, 
		A.IDNOTAADMINISTRATIVA, 
		Rtrim(D.CODIGO) + ' - ' + RTRIM(D.NOMBRE) AS FORMATO,
		Rtrim(D.NOMBRE) AS 'NOMBRE'
	From 
		NTNOTASADMINISTRATIVASC A 
		INNER JOIN SEGusuaru B ON A.CODUSUARI = B.CODUSUARI 
		INNER JOIN ADCENATEN C ON A.CODCENATE = C.CODCENATE 
		INNER JOIN NTADMINISTRATIVAS D ON A.IDNOTAADMINISTRATIVA = D.ID 
	Where 
		A.IPCODPACI = @INDPaciente 
UNION ALL 

		Select 	
	   'Formatos educativos/encuestas' AS Tipo,
		Rtrim(D.Code) AS 'CODIGO', 
		A.ID, 
		A.IPCODPACI, 
		A.DateCreation, 
		Rtrim(C.NOMCENATE) as 'Centro Atencion', 
		Rtrim(b.NOMMEDICO) as 'Usuario', 
		NUMINGRES, 
		A.IdParamEducationFormatsC, 
		Rtrim(D.Code) + ' - ' + RTRIM(D.Description) AS FORMATO,
		Rtrim(D.Description) AS 'NOMBRE' 
	From 
		PatientEducationFormatsC A 
		INNER JOIN INPROFSAL B ON A.CODPROSAL = B.CODPROSAL
		INNER JOIN ADCENATEN C ON A.CODCENATE = C.CODCENATE 
		INNER JOIN ParamEducationFormatsC D ON A.IdParamEducationFormatsC = D.ID 
	Where 
		A.IPCODPACI = @INDPaciente 

end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista el historial completo de documentación administrativa y educativa asociada a un paciente, identificado por su cédula o código. Combina en un único resultado dos tipos de registros: las notas administrativas (como alertas, observaciones o gestiones administrativas) registradas en los ingresos del paciente, y los formatos educativos o encuestas de educación al paciente aplicados por profesionales de la salud. Para cada registro muestra el tipo de documento, su código y nombre, el centro de atención donde se generó, el usuario o profesional que lo creó, la fecha de creación y el número de ingreso. Se utiliza en los módulos de historia clínica y gestión administrativa para consultar todo el historial de notas y formatos educativos de un paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_NOTAS_ListarHistoricoNotasAdministrativas';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_NOTAS_ListarHistoricoNotasAdministrativas';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista el histórico unificado de notas administrativas y formatos educativos/encuestas asociados a un paciente, mostrando metadatos de creación, centro de atención y usuario responsable.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_NOTAS_ListarHistoricoNotasAdministrativas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe existir y tener notas administrativas o formatos educativos registrados con su identificador.; Las notas y formatos deben tener relaciones íntegras con usuario/profesional, centro de atención y catálogo de formato.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_NOTAS_ListarHistoricoNotasAdministrativas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelven registros del paciente recibido como parámetro (filtro IPCODPACI en ambas consultas).; Se usan INNER JOIN, por lo que se excluyen notas/formatos sin usuario, centro de atención o catálogo asociado.; El campo ''Tipo'' diferencia el origen: ''Notas administrativas'' vs ''Formatos educativos/encuestas''.; El campo FORMATO siempre se entrega como concatenación ''CODIGO - NOMBRE/DESCRIPTION'' sin espacios sobrantes (RTRIM).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_NOTAS_ListarHistoricoNotasAdministrativas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Notas administrativas; Formatos educativos; Encuestas; Centro de atención; Usuario; Profesional de la salud; Ingreso (NUMINGRES)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_NOTAS_ListarHistoricoNotasAdministrativas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve un único conjunto de resultados con UNION ALL: filas tipificadas como ''Notas administrativas'' provenientes de NTNOTASADMINISTRATIVASC y filas tipificadas como ''Formatos educativos/encuestas'' provenientes de PatientEducationFormatsC, filtradas ambas por IPCODPACI = @INDPaciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_NOTAS_ListarHistoricoNotasAdministrativas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.NTNOTASADMINISTRATIVASC; dbo.SEGusuaru; dbo.ADCENATEN; dbo.NTADMINISTRATIVAS; dbo.PatientEducationFormatsC; dbo.INPROFSAL; dbo.ParamEducationFormatsC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_NOTAS_ListarHistoricoNotasAdministrativas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_NOTAS_ListarHistoricoNotasAdministrativas';
-- GO
