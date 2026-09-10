CREATE PROCEDURE [dbo].[SPMOV_ListarLaboratoriosPacientes]
(
@Paciente Varchar(25),
@Ingreso Char(10)
)
AS
BEGIN
	SET NOCOUNT ON;
		SELECT  RTRIM(e.CODSERIPS) as Codigo,
					RTRIM(E.DESSERIPS) AS Servicio
						--RTRIM(N.DESESPECI) AS Especialidad
			FROM HCORDLABO A
					INNER JOIN INCUPSIPS E ON A.CODSERIPS=E.CODSERIPS 
			--INNER JOIN HCHISPACA AS J ON A.NUMEFOLIO=J.NUMEFOLIO AND A.IPCODPACI=J.IPCODPACI 
			--LEFT OUTER JOIN INESPECIA N ON J.CODESPTRA=N.CODESPECI
		WHERE A.IPCODPACI= @Paciente AND A.NUMINGRES = @Ingreso
		group by e.CODSERIPS,e.DESSERIPS
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los exámenes de laboratorio clínico solicitados para un paciente en un ingreso específico. Recibe como parámetros la cédula del paciente y el número de ingreso, consulta las órdenes de laboratorio de la historia clínica (HCORDLABO) y cruza con el catálogo de servicios CUPS/IPS (INCUPSIPS) para obtener el código y la descripción de cada examen. Devuelve un listado único (sin duplicados) de los servicios de laboratorio ordenados, útil para visualizar qué pruebas diagnósticas fueron solicitadas durante una atención o internación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPMOV_ListarLaboratoriosPacientes';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPMOV_ListarLaboratoriosPacientes';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los servicios/exámenes de laboratorio únicos asociados a un paciente en un ingreso específico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarLaboratoriosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el paciente y el ingreso indicados con órdenes de laboratorio registradas en HCORDLABO; Los códigos de servicio en HCORDLABO deben existir en el catálogo INCUPSIPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarLaboratoriosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El resultado nunca contiene servicios duplicados al agrupar por código y descripción; Solo se retornan servicios que tengan correspondencia en el catálogo INCUPSIPS (INNER JOIN); El resultado se restringe siempre a un único paciente y un único ingreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarLaboratoriosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Órdenes de laboratorio; Servicios CUPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarLaboratoriosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCORDLABO: Cuando IPCODPACI = paciente y NUMINGRES = ingreso, retorna códigos y descripciones de servicios de laboratorio agrupados (sin duplicados)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarLaboratoriosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDLABO; dbo.INCUPSIPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarLaboratoriosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarLaboratoriosPacientes';
-- GO
