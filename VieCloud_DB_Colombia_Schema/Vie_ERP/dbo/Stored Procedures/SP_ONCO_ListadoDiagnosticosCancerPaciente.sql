CREATE PROCEDURE [dbo].[SP_ONCO_ListadoDiagnosticosCancerPaciente]
(
@INDpaciente Varchar(25)
)
AS
BEGIN
	SET NOCOUNT ON;

----------------------------------	
	select  A.ID,a.IPCODPACI as 'Identificacion', A.NUMINGRES as 'Ingreso' ,  A.FECHAREGISTRO,RTRIM(B.CODDIAGNO)AS 'Codigo Diagnostico',RTRIM(B.NOMDIAGNO)AS 'Nombre Diagnostico', CASE TIPO WHEN 1 THEN 'Primario' WHEN 2 THEN 'Otro Primario' WHEN 3 THEN 'Primario Desconocido' END AS 'Tipo',
			Rtrim(C.NOMMEDICO) as 'Nombre Medico', A.NUMEFOLIO AS 'Folio', 
			(Select top 1 RTRIM(X.DESESPECI) from HCHISPACA Z INNER JOIN INESPECIA X ON Z.CODESPTRA = X.CODESPECI where Z.IPCODPACI = @INDpaciente AND A.NUMEFOLIO = Z.NUMEFOLIO) AS 'Especialidad Tratante',
			Rtrim(D.NOMCENATE) as 'Centro Atencion', D.CODCENATE as CODCENATE, Rtrim(E.UFUDESCRI) AS 'Unidad Funcional',G.CODIGOGRUPO AS 'Codigo Grupo' 
	from (
		select distinct tmpX.IDUltimo from ( Select (select top 1 ID from HCGRUPOCANCERPACIC t where t.CODDIAGNO = A.CODDIAGNO AND T.IPCODPACI = @INDpaciente ORDER BY t.FECHAREGISTRO ASC ) as IDUltimo
		FROM   HCGRUPOCANCERPACIC  A where  a.IPCODPACI = @INDpaciente 
		) as tmpX
	) as TMP 
			inner join HCGRUPOCANCERPACIC  A with(nolock) on A.ID = TMP.IDUltimo
			Inner Join INDIAGNOS B with(nolock) on A.CODDIAGNO = B.CODDIAGNO 
			Inner Join INPROFSAL C with(nolock) on A.CODPROSAL = C.CODPROSAL  
			Inner Join ADCENATEN D with(nolock) on A.CODCENATE = D.CODCENATE  
			Inner Join INUNIFUNC E with(nolock) on A.UFUCODIGO = E.UFUCODIGO
			left JOIN ADGRUPOCANCERDIAGND F with(nolock) ON A.CODDIAGNO = F.CODDIAGNO 
			left JOIN ADGRUPOCANCERC G with(nolock) ON F.IDADGRUPOCANCERC = G.ID
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todos los diagnósticos oncológicos registrados para un paciente específico (identificado por su cédula o código de paciente), mostrando únicamente el primer registro histórico de cada diagnóstico de cáncer. Para cada diagnóstico retorna el código y nombre CIE-10, el tipo de tumor (primario, otro primario o primario desconocido), el médico tratante, la especialidad tratante, el folio de la historia clínica, el centro de atención, la unidad funcional y el código del grupo oncológico al que pertenece. Combina información del seguimiento oncológico del paciente (HCGRUPOCANCERPACIC) con los catálogos maestros de diagnósticos CIE-10, profesionales de la salud, centros de atención, unidades funcionales y clasificación de grupos de cáncer. Se usa en el módulo de oncología para consultar el historial de diagnósticos de cáncer de un paciente, apoyando la gestión clínica oncológica y la trazabilidad de la enfermedad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ONCO_ListadoDiagnosticosCancerPaciente';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ONCO_ListadoDiagnosticosCancerPaciente';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve el listado consolidado de diagnósticos de cáncer registrados para un paciente, mostrando el primer registro histórico por cada código de diagnóstico junto con datos clínicos y administrativos asociados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListadoDiagnosticosCancerPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe existir y tener registros en HCGRUPOCANCERPACIC; Los diagnósticos referenciados deben existir en INDIAGNOS, INPROFSAL, ADCENATEN e INUNIFUNC para aparecer en el resultado (joins internos)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListadoDiagnosticosCancerPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retorna un registro por cada CODDIAGNO del paciente (el más antiguo por FECHAREGISTRO); La especialidad tratante se obtiene del primer HCHISPACA del paciente que coincida con NUMEFOLIO; Diagnósticos sin grupo de cáncer asociado igualmente se listan (LEFT JOIN sobre ADGRUPOCANCERDIAGND/ADGRUPOCANCERC); Solo se incluyen diagnósticos cuyo médico, centro de atención y unidad funcional existen en sus catálogos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListadoDiagnosticosCancerPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; diagnóstico de cáncer; tipo de diagnóstico (primario / otro primario / primario desconocido); médico tratante; especialidad tratante; centro de atención; unidad funcional; grupo de cáncer; ingreso; folio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListadoDiagnosticosCancerPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Por cada CODDIAGNO del paciente en HCGRUPOCANCERPACIC retorna únicamente el registro con FECHAREGISTRO más antigua (TOP 1 ORDER BY FECHAREGISTRO ASC), enriquecido con diagnóstico, médico, centro de atención, unidad funcional, especialidad tratante y grupo de cáncer.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListadoDiagnosticosCancerPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TIPO = 1 → Etiqueta el diagnóstico como ''Primario''; si TIPO = 2 → Etiqueta el diagnóstico como ''Otro Primario''; si TIPO = 3 → Etiqueta el diagnóstico como ''Primario Desconocido'' else NULL para otros valores de TIPO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListadoDiagnosticosCancerPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCGRUPOCANCERPACIC; dbo.INDIAGNOS; dbo.INPROFSAL; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.ADGRUPOCANCERDIAGND; dbo.ADGRUPOCANCERC; dbo.HCHISPACA; dbo.INESPECIA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListadoDiagnosticosCancerPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListadoDiagnosticosCancerPaciente';
-- GO
