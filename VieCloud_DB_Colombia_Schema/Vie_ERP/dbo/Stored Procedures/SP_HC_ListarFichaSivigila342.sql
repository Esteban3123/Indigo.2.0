-- Stored Procedure
-- =============================================
-- Autor:		Jean Carlos Roldan Lozano
-- Fecha Creación: 02-11-2018
-- Descripción:	Sp que me lista la Información de las Fichas del Sivigila, Ficha 342
-- Modificó:    Yezid Garcia Medina
-- Fecha Modificación : 26-01-2022
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila342]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON;
 
			Select
				CASE NIVELEDUCA  WHEN '1' THEN 'X' END AS 'Preescolar',CASE NIVELEDUCA WHEN '2' THEN 'X' END AS 'Primaria', CASE NIVELEDUCA WHEN '3' THEN 'X' END AS 'Secundaria',
				CASE NIVELEDUCA  WHEN '4' THEN 'X' END AS 'Academica',CASE NIVELEDUCA WHEN '5' THEN 'X' END AS 'Tecnica', CASE NIVELEDUCA WHEN '6' THEN 'X' END AS 'Normalista',
				CASE NIVELEDUCA  WHEN '7' THEN 'X' END AS 'TecniProfesional',CASE NIVELEDUCA WHEN '8' THEN 'X' END AS 'Tecnologica', CASE NIVELEDUCA WHEN '9' THEN 'X' END AS 'Profesional',
				CASE NIVELEDUCA  WHEN '10' THEN 'X' END AS 'Especializacion',CASE NIVELEDUCA WHEN '11' THEN 'X' END AS 'Maestria', CASE NIVELEDUCA WHEN '12' THEN 'X' END AS 'Doctorado',
				CASE NIVELEDUCA  WHEN '13' THEN 'X' END AS 'Ninguno', CASE TRABURBAN WHEN '1' THEN 'X' END AS 'TRABURBAN', CASE TRABRURAL WHEN '1' THEN 'X' END AS 'TRABRURAL',
				CASE JOVENVULNE WHEN '1' THEN 'X' END AS 'JOVENVULNE', CASE JOVENVULURB WHEN '1' THEN 'X' END AS 'JOVENVULURB', CASE DISSISNERV WHEN '1' THEN 'X' END AS 'DISSISNERV',
				CASE DISOJOS WHEN '1' THEN 'X' END AS 'DISOJOS', CASE DISOIDOS WHEN '1' THEN 'X' END AS 'DISOIDOS', CASE DISDEMAS WHEN '1' THEN 'X' END AS 'DISDEMAS',
				CASE DISVOZ WHEN '1' THEN 'X' END AS 'DISVOZ', CASE DISSISTEM WHEN '1' THEN 'X' END AS 'DISSISTEM', CASE DISDIGEST WHEN '1' THEN 'X' END AS 'DISDIGEST',
				CASE DISSISTGENI WHEN '1' THEN 'X' END AS 'DISSISTGENI', CASE DISMOVI WHEN '1' THEN 'X' END AS 'DISMOVI', CASE DISPIEL WHEN '1' THEN 'X' END AS 'DISPIEL',
				CASE DISOTRO WHEN '1' THEN 'X' END AS 'DISOTRO', CASE NODEFIN WHEN '1' THEN 'X' END AS 'NODEFIN', convert(varchar(10),FECHADIAG,103) As 'Fecha Diagno', 
				Rtrim(PRUEBLAB) As 'PRUEBLAB',  Rtrim(NOMBENFER) As 'NOMBENFER', 
				VERSION AS 'VERSION', 
				JSON AS 'JSON' 
			From 
				HCFICHA342 
			Where 
				IDFICHANOTIFICACION  = @IdFicha			
		
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera el detalle completo de una ficha de notificación epidemiológica SIVIGILA código 342, utilizada para reportar eventos de salud pública como discapacidades y enfermedades de interés epidemiológico. A partir del identificador interno de la ficha (@IdFicha) y el número de ficha (@NumFicha), consulta la tabla HCFICHA342 y devuelve la información estructurada para imprimir o visualizar el formulario oficial: nivel educativo del paciente (preescolar, primaria, secundaria, técnica, profesional, maestría, doctorado, etc.), tipo de trabajador (urbano o rural), condición de joven vulnerable, sistemas del cuerpo afectados por discapacidad (sistema nervioso, ojos, oídos, voz, sistema digestivo, sistema genitourinario, movilidad, piel, entre otros), fecha de diagnóstico, resultado de prueba de laboratorio, nombre de la enfermedad notificada y la versión y representación JSON de la ficha. Se usa principalmente en los módulos de vigilancia epidemiológica e historia clínica para generar el reporte oficial SIVIGILA que debe remitirse a las autoridades sanitarias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila342';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila342';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera y formatea para visualización los datos clínicos y sociodemográficos de una ficha de notificación epidemiológica tipo 342 del Sivigila.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila342';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCFICHA342 con IDFICHANOTIFICACION igual al identificador suministrado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila342';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La fecha de diagnóstico se entrega siempre en formato dd/mm/yyyy (estilo 103).; Los campos booleanos se exponen como ''X'' o NULL, nunca como su valor numérico original.; Solo retorna fichas que coincidan exactamente con el identificador de notificación suministrado.; El parámetro NumFicha se recibe pero no participa en el filtrado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila342';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Sivigila; Ficha de notificación epidemiológica 342; Nivel educativo; Discapacidad; Población vulnerable (joven urbano/rural); Trabajador urbano/rural; Diagnóstico; Prueba de laboratorio; Enfermedad notificada', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila342';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCFICHA342: Cuando IDFICHANOTIFICACION coincide con el identificador, retorna una fila con los datos de la ficha 342 con campos de nivel educativo, discapacidades y vulnerabilidad mapeados a ''X'' según código.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila342';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si NIVELEDUCA entre ''1'' y ''13'' → Marca con ''X'' la columna correspondiente al nivel educativo (Preescolar, Primaria, Secundaria, Academica, Tecnica, Normalista, TecniProfesional, Tecnologica, Profesional, Especializacion, Maestria, Doctorado, Ninguno). else La columna queda en NULL.; si Banderas TRABURBAN, TRABRURAL, JOVENVULNE, JOVENVULURB iguales a ''1'' → Se marcan con ''X'' indicando condición laboral o de vulnerabilidad del joven. else NULL; si Banderas de discapacidad (DISSISNERV, DISOJOS, DISOIDOS, DISDEMAS, DISVOZ, DISSISTEM, DISDIGEST, DISSISTGENI, DISMOVI, DISPIEL, DISOTRO, NODEFIN) iguales a ''1'' → Se marca ''X'' indicando el tipo de discapacidad presente. else NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila342';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA342', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila342';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila342';
-- GO
