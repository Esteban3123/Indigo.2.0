
CREATE PROCEDURE [dbo].[SPCH_ListarOportunidadDeAgendaMedica]
(
	  @CentroAtencion as char(10),
	  @FechoraInicial as date ,
	  @FechoraFianal as date

	 /*
		 @CentroAtencion as char(10) = '11011',
		 @FechoraInicial as date = '01/06/2024 00:00:00',
		 @FechoraFianal as date = '30/06/2024 23:59:59',
	 */

)
AS
BEGIN
	SET NOCOUNT ON;

WITH Tmp_ConsultaEspecialidad AS (
		
		select distinct agagemedc.CODAUTONU, agagemedc.CODPROSAL, agagemedc.CODESPECI, agagemedc.FECHORAIN,agagemedc.FECHORAFI, codcenate, codigocon,TIPAGEMED
			,MinutosCitasAsignadadas  = (
												Select isnull(SUM(DATEDIFF(MINUTE,FECHORAIN, FECHORAFI )),0) 
												FROM dbo.AGASICITA  With (nolock) 
												WHERE IDAGENDA  = agagemedc.CODAUTONU AND CODESTCIT NOT IN(4,5) AND CODCENATE=@CentroAtencion  
									   ) 
			,MinutosAgenda = (
									DATEDIFF(MINUTE,agagemedc.FECHORAIN, agagemedc.FECHORAFI)
							 )
			,ROW_NUMBER() OVER (PARTITION BY CODESPECI ORDER BY FECHORAIN asc) AS rn
			from dbo.AGAGEMEDC as agagemedc  WITH (nolock)
			WHERE   agagemedc.TIPAGEMED = 0 
				AND agagemedc.CODCENATE = @CentroAtencion 
				AND agagemedc.FECHORAIN  >= @FechoraInicial  
				AND agagemedc.FECHORAIN <= @FechoraFianal 
				AND (Select isnull(SUM(DATEDIFF(MINUTE,FECHORAIN, FECHORAFI )),0) FROM dbo.AGASICITA  With (nolock) 
												WHERE IDAGENDA  = agagemedc.CODAUTONU AND CODESTCIT NOT IN(4,5) AND CODCENATE=@CentroAtencion)  < (DATEDIFF(MINUTE,agagemedc.FECHORAIN, agagemedc.FECHORAFI))
)
--select CODAUTONU,CODPROSAL,CODESPECI, FECHORAIN, FECHORAFI,MinutosCitasAsignadadas,MinutosAgenda from Tmp_ConsultaEspecialidad  where rn = 1  
--select * from Tmp_ConsultaEspecialidad

		SELECT cte.codautonu AS codautonuAgenda,
			   concat(cte.CODESPECI ,' - ',ESPECIALIDAD.DESESPECI) as 'Especialidad Agenda',
			   fechorain as 'Fecha Hora inicial Agenda',
			   fechorafi as 'Fecha Hora Final Agenda',
			   Rtrim(cte.codprosal) AS 'Profesional Agenda',
			   Rtrim(PROFESIONALSALUD.nommedico)     AS 'Nombre Profesional',
			   cte.codcenate as 'Centro Atención',
			   concat(cte.codigocon , ' - ', UBICACION.DESCRICON) AS Consultorio,
			   cte.MinutosCitasAsignadadas, 
			   cte.MinutosAgenda,
			   CitasAgenda = (
				SELECT   CONCAT('Cita el: ', format(FECHORAIN,'dd/MM/yyyy HH:mm'), ' hasta las: ', format(FECHORAFI,'dd/MM/yyyy HH:mm'), ' / Paciente: ', IPCODPACI, '  ******  ' ) 
				FROM AGASICITA
				WHERE IDAGENDA = cte.CODAUTONU  AND CODESTCIT NOT IN(4,5) AND CODCENATE=@CentroAtencion  
				FOR XML PATH ('')   )					  		
		FROM  Tmp_ConsultaEspecialidad cte   WITH (nolock)
			   INNER JOIN dbo.adcenaten CENTROATENCION WITH (nolock)		 ON cte.codcenate = CENTROATENCION.codcenate
			   LEFT OUTER JOIN dbo.agconsult UBICACION WITH (nolock)		 ON cte.codigocon = UBICACION.codigocon AND UBICACION.codcenate = @CentroAtencion
			   LEFT OUTER JOIN dbo.inprofsal PROFESIONALSALUD WITH (nolock)	 ON cte.codprosal = PROFESIONALSALUD.codprosal
			   LEFT OUTER JOIN dbo.inespecia ESPECIALIDAD WITH (nolock)		 ON cte.codespeci = ESPECIALIDAD.codespeci
		WHERE   
		--		rn = 1  
				cte.TIPAGEMED = 0 
				AND cte.codcenate = @CentroAtencion 
				AND fechorain >= @FechoraInicial  
				AND fechorain <= @FechoraFianal 
	  order by cte.CODESPECI ASC, cte.FECHORAIN asc, cte.FECHORAFI asc
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los bloques de agenda médica con disponibilidad (cupos libres) para un centro de atención y un rango de fechas determinados. Compara los minutos totales de cada bloque de agenda (AGAGEMEDC) contra los minutos ya ocupados por citas activas asignadas (AGASICITA), mostrando únicamente los bloques donde aún queda tiempo disponible. Para cada bloque retorna la especialidad, el profesional de salud, el consultorio, el horario y el detalle de las citas ya agendadas con su paciente. Es utilizado para medir y reportar la oportunidad de agendamiento médico por especialidad, apoyando la gestión de citas, auditoría de capacidad instalada y seguimiento de indicadores de acceso a servicios de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_ListarOportunidadDeAgendaMedica';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_ListarOportunidadDeAgendaMedica';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las agendas médicas de un centro de atención dentro de un rango de fechas que aún tienen disponibilidad (minutos no copados por citas vigentes), mostrando especialidad, profesional, consultorio y detalle de citas asignadas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarOportunidadDeAgendaMedica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El centro de atención debe existir en adcenaten; Las fechas inicial y final deben estar definidas y acotar el rango de búsqueda sobre FECHORAIN; Solo se consideran agendas con TIPAGEMED = 0 (consulta/agenda estándar)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarOportunidadDeAgendaMedica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se evalúan agendas tipo TIPAGEMED = 0; Las citas con estado 4 o 5 nunca se consideran ocupadas ni se muestran en el detalle de citas; El cálculo de ocupación se restringe al mismo centro de atención del parámetro; Los minutos ocupados se acotan en cero cuando no hay citas (ISNULL sobre SUM); El resultado se ordena por especialidad y luego por fecha/hora de inicio y fin de la agenda', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarOportunidadDeAgendaMedica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Agenda médica; Centro de atención; Especialidad; Profesional de salud; Consultorio; Cita médica; Paciente; Estado de cita; Disponibilidad/oportunidad de agenda', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarOportunidadDeAgendaMedica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve agendas (TIPAGEMED=0) del centro y rango indicado cuya suma de minutos de citas no canceladas (CODESTCIT NOT IN (4,5)) sea menor al total de minutos del bloque de agenda, junto con el detalle concatenado de citas vigentes vía FOR XML PATH', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarOportunidadDeAgendaMedica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Suma de minutos de citas con CODESTCIT NOT IN (4,5) en AGASICITA para la agenda < DATEDIFF en minutos entre FECHORAIN y FECHORAFI de la agenda → La agenda se incluye como oportunidad disponible else La agenda se excluye del resultado por estar copada; si CODESTCIT IN (4,5) en AGASICITA → La cita se considera no vigente (cancelada/anulada) y no suma minutos ocupados ni se lista en el detalle else La cita se cuenta como ocupación efectiva del bloque', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarOportunidadDeAgendaMedica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGAGEMEDC; dbo.AGASICITA; dbo.adcenaten; dbo.agconsult; dbo.inprofsal; dbo.inespecia', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarOportunidadDeAgendaMedica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarOportunidadDeAgendaMedica';
-- GO
