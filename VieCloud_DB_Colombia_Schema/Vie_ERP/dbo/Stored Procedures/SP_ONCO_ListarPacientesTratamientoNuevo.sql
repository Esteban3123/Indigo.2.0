

CREATE PROCEDURE [dbo].[SP_ONCO_ListarPacientesTratamientoNuevo]
(
@CentroAtencion varchar(500)
)
AS
BEGIN
	SET NOCOUNT ON;

Select TOP 1  A.ID,C.CODTIPPAC,A.FECHAREGISTRO AS 'Fecha Registro',B.IPCODPACI AS 'Identificacion',rtrim(ltrim(B.IPNOMCOMP)) as 'NombrePaciente', rtrim(ltrim(ENT.CODENTIDA)) + ' - ' + rtrim(ltrim(ENT.NOMENTIDA)) as 'Entidad',C.NUMINGRES AS 'Ingreso',[dbo].[EDAD] (B.IPFECNACI,getdate()) As 'Edad',
		 A.ESTADO as 'Estado Registro', CASE A.ESTADO  WHEN  1 then 'Pacientes con diagnósticos nuevos' WHEN 2 then 'Pacientes con datos modificados' WHEN 3 then 'Pacientes con diagnósticos visados' end as 'Nombre Estado',
		 Rtrim(D.NOMCENATE) AS 'Centro Atencion',Rtrim(A.CODCENATE) AS 'Codigo Centro Atencion'
FROM  HCGRUPOCANCERPACIC  A 
		INNER JOIN INPACIENT B ON A.IPCODPACI = B.IPCODPACI 
		INNER JOIN ADINGRESO C on A.NUMINGRES = C.NUMINGRES
		INNER JOIN INENTIDAD ENT on ENT.CODENTIDA = C.CODENTIDA 
		INNER JOIN ADCENATEN D on A.CODCENATE = D.CODCENATE 
	where A.ESTADO IN (1,2) AND A.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) ORDER BY A.FECHAREGISTRO DESC   

end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes oncológicos con tratamiento nuevo o datos modificados pendientes de visado, consultando el registro de seguimiento oncológico (HCGRUPOCANCERPACIC) junto con los datos del paciente, su ingreso hospitalario, la entidad aseguradora o pagadora y el centro de atención. Filtra por uno o varios centros de atención (recibidos como parámetro separado por comas) y devuelve el registro más reciente por fecha, incluyendo cédula, nombre, edad calculada, número de ingreso, entidad y el estado del diagnóstico oncológico (nuevo, modificado o visado). Se usa en el módulo de oncología para que los profesionales identifiquen qué pacientes tienen diagnósticos de cáncer pendientes de revisión o aprobación en sus sedes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ONCO_ListarPacientesTratamientoNuevo';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ONCO_ListarPacientesTratamientoNuevo';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista el registro oncológico más reciente de pacientes con diagnósticos nuevos o modificados (no visados) en uno o varios centros de atención dados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesTratamientoNuevo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe proveer una cadena con uno o más códigos de centro de atención separados, parseable por dbo.splitstring; Deben existir registros relacionados en INPACIENT, ADINGRESO, INENTIDAD y ADCENATEN para que el INNER JOIN retorne filas', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesTratamientoNuevo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna pacientes oncológicos cuyo registro está en estado 1 (nuevo) o 2 (modificado), excluyendo los visados (3); Restringe los resultados a uno o varios centros de atención recibidos como lista delimitada; Devuelve únicamente el registro más reciente por fecha de registro (TOP 1 ORDER BY FECHAREGISTRO DESC); Calcula la edad del paciente respecto a la fecha actual mediante la función dbo.EDAD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesTratamientoNuevo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente oncológico; grupo de cáncer; ingreso; entidad (asegurador/pagador); centro de atención; edad del paciente; diagnóstico nuevo; diagnóstico visado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesTratamientoNuevo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCGRUPOCANCERPACIC: Cuando A.ESTADO IN (1,2) y A.CODCENATE pertenece a la lista parseada de @CentroAtencion, retorna el registro TOP 1 ordenado por FECHAREGISTRO DESC con datos del paciente, ingreso, entidad, edad, estado y centro de atención', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesTratamientoNuevo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ESTADO = 1 → Se etiqueta como ''Pacientes con diagnósticos nuevos''; si ESTADO = 2 → Se etiqueta como ''Pacientes con datos modificados''; si ESTADO = 3 → Se etiqueta como ''Pacientes con diagnósticos visados'' (no se incluye en el filtro principal)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesTratamientoNuevo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.splitstring; dbo.EDAD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesTratamientoNuevo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCGRUPOCANCERPACIC; dbo.INPACIENT; dbo.ADINGRESO; dbo.INENTIDAD; dbo.ADCENATEN', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesTratamientoNuevo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesTratamientoNuevo';
-- GO
