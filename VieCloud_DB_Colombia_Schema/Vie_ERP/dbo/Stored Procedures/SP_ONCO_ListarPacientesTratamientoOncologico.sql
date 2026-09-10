

CREATE PROCEDURE [dbo].[SP_ONCO_ListarPacientesTratamientoOncologico]
(
@CentroAtencion varchar(500)
)
AS
BEGIN
	SET NOCOUNT ON;

select  cast(0 as bit) as 'Seleccion', A.ID, C.CODTIPPAC,A.FECHAREGISTRO AS 'Fecha Registro',B.IPCODPACI AS 'Identificacion',rtrim(ltrim(B.IPNOMCOMP)) as 'NombrePaciente', rtrim(ltrim(ENT.CODENTIDA)) + ' - ' + rtrim(ltrim(ENT.NOMENTIDA)) as 'Entidad',C.NUMINGRES AS 'Ingreso',[dbo].[EDAD] (B.IPFECNACI,getdate()) As 'Edad',
		 A.ESTADO as 'Estado Registro', CASE A.ESTADO  WHEN  1 then 'Pacientes con diagnósticos nuevos' WHEN 2 then 'Pacientes con datos modificados' WHEN 3 then 'Pacientes con diagnósticos visados' end as 'Nombre Estado',
		 Rtrim(D.NOMCENATE) AS 'Nombre Centro Atencion',Rtrim(A.CODCENATE) AS 'Codigo Centro Atencion',A.NUMEFOLIO AS 'Folio'
 from (
	    select  IPCODPACI,max(ID) as IDUltimo  from HCGRUPOCANCERPACIC A with (nolock)
		where A.ESTADO IN (1,2) AND  A.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) 
		group by IPCODPACI    
	) as TMP 
	inner join HCGRUPOCANCERPACIC  A with (nolock) on A.ID = TMP.IDUltimo
	INNER JOIN INPACIENT B with (nolock) ON A.IPCODPACI = B.IPCODPACI 
	INNER JOIN ADINGRESO C with (nolock) on A.NUMINGRES = C.NUMINGRES
	INNER JOIN INENTIDAD ENT with (nolock) on ENT.CODENTIDA = C.CODENTIDA 
	INNER JOIN ADCENATEN D with (nolock) on A.CODCENATE = D.CODCENATE 
	where A.ESTADO IN (1,2) AND A.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) ORDER BY A.FECHAREGISTRO DESC   

end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes oncológicos activos que se encuentran en tratamiento, mostrando únicamente el registro más reciente por paciente (con estado ''nuevo'' o ''modificado'') para uno o varios centros de atención indicados. Integra el seguimiento oncológico (HCGRUPOCANCERPACIC), los datos personales del paciente como cédula y nombre completo (INPACIENT), el ingreso o episodio de atención asociado (ADINGRESO), la entidad aseguradora o pagador (INENTIDAD) y el centro de atención (ADCENATEN). Se utiliza en el módulo de oncología para que los profesionales de salud identifiquen qué pacientes con diagnóstico de cáncer tienen registros pendientes de visación, incluyendo su edad calculada, número de ingreso, folio, entidad y estado del registro oncológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ONCO_ListarPacientesTratamientoOncologico';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ONCO_ListarPacientesTratamientoOncologico';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista, por uno o varios centros de atención, el último registro vigente de pacientes con tratamiento oncológico cuyos diagnósticos están nuevos o modificados, con datos demográficos y de ingreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesTratamientoOncologico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de centros de atención debe contener uno o más códigos separados procesables por dbo.splitstring.; Deben existir registros en HCGRUPOCANCERPACIC con estado 1 o 2 para los centros indicados.; El paciente, ingreso, entidad y centro referenciados deben existir en INPACIENT, ADINGRESO, INENTIDAD y ADCENATEN respectivamente (joins internos).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesTratamientoOncologico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca retorna pacientes con estado distinto de 1 o 2 (excluye visados=3).; Por cada paciente devuelve un único registro: el de máximo ID dentro del filtro.; Solo considera centros de atención presentes en la lista parametrizada.; Resultados ordenados por FECHAREGISTRO descendente (más recientes primero).; La columna ''Seleccion'' se inicializa siempre en 0 (false) para uso de UI.; La edad se calcula al momento de la consulta usando dbo.EDAD con la fecha actual.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesTratamientoOncologico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente oncológico; Tratamiento oncológico; Diagnóstico de cáncer; Estado de diagnóstico (nuevo, modificado, visado); Centro de atención; Ingreso hospitalario; Entidad (aseguradora/responsable); Tipo de paciente; Folio; Edad del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesTratamientoOncologico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCGRUPOCANCERPACIC: Devuelve solo el registro de mayor ID por paciente (último) cuyo ESTADO IN (1,2) y CODCENATE pertenezca a la lista recibida.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesTratamientoOncologico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ESTADO = 1 → Etiqueta el registro como ''Pacientes con diagnósticos nuevos''.; si ESTADO = 2 → Etiqueta el registro como ''Pacientes con datos modificados''.; si ESTADO = 3 → Etiqueta el registro como ''Pacientes con diagnósticos visados'' (no se incluye en el resultado por filtro ESTADO IN (1,2)).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesTratamientoOncologico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.splitstring; dbo.EDAD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesTratamientoOncologico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCGRUPOCANCERPACIC; dbo.INPACIENT; dbo.ADINGRESO; dbo.INENTIDAD; dbo.ADCENATEN', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesTratamientoOncologico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesTratamientoOncologico';
-- GO
