

CREATE PROCEDURE [dbo].[SP_ONCO_ListarPacientesDXVisados]
(
@CentroAtencion Char(500)
)
AS
BEGIN
	SET NOCOUNT ON;

 select  cast('' as bit) as 'sel', A.ID, C.CODTIPPAC,A.FECHAREGISTRO AS 'Fecha Registro',B.IPCODPACI AS 'Identificacion',rtrim(ltrim(B.IPNOMCOMP)) as 'NombrePaciente', rtrim(ltrim(ENT.CODENTIDA)) + ' - ' + rtrim(ltrim(ENT.NOMENTIDA)) as 'Entidad',C.NUMINGRES AS 'Ingreso',[dbo].[EDAD] (B.IPFECNACI,getdate()) As 'Edad',
		 A.ESTADO as 'Estado Registro', CASE A.ESTADO  WHEN  1 then 'Pacientes con diagnósticos nuevos' WHEN 2 then 'Pacientes con datos modificados' WHEN 3 then 'Pacientes con diagnósticos visados' end as 'Nombre Estado',
		 Rtrim(D.NOMCENATE) AS 'Nombre Centro Atencion',Rtrim(A.CODCENATE) AS 'Codigo Centro Atencion',A.NUMEFOLIO AS 'Folio'
 from (
	 select distinct tmpX.IDUltimo from (	Select 	 (select top 1 ID from HCGRUPOCANCERPACIC t where t.IPCODPACI = A.IPCODPACI ORDER BY t.FECHAREGISTRO DESC   ) as IDUltimo
		FROM   HCGRUPOCANCERPACIC  A where A.ESTADO IN (3) AND  A.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion))  
		) as tmpX
	) as TMP 
	inner join HCGRUPOCANCERPACIC  A on A.ID = TMP.IDUltimo
	INNER JOIN INPACIENT B ON A.IPCODPACI = B.IPCODPACI 
	INNER JOIN ADINGRESO C on A.NUMINGRES = C.NUMINGRES
	INNER JOIN INENTIDAD ENT on ENT.CODENTIDA = C.CODENTIDA 
	INNER JOIN ADCENATEN D on A.CODCENATE = D.CODCENATE 
	where A.ESTADO IN (3) AND A.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) ORDER BY A.FECHAREGISTRO DESC   

end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes oncológicos cuyos diagnósticos de cáncer han sido visados (estado 3), filtrando por uno o más centros de atención recibidos como parámetro. Para cada paciente retorna el registro más reciente del grupo de cáncer (última fecha de registro), junto con su identificación, nombre completo, edad calculada, entidad aseguradora o pagadora, número de ingreso, tipo de paciente, folio y nombre del centro de atención. Combina los datos del seguimiento oncológico (HCGRUPOCANCERPACIC), la información maestra del paciente (INPACIENT), el episodio de ingreso (ADINGRESO), la entidad pagadora (INENTIDAD) y la sede de atención (ADCENATEN). Se usa principalmente en los módulos de oncología para visualizar y gestionar el listado de pacientes con diagnóstico de cáncer ya visado por el profesional responsable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ONCO_ListarPacientesDXVisados';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ONCO_ListarPacientesDXVisados';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los pacientes oncológicos cuyo último registro de grupo de cáncer se encuentra en estado "visado", filtrados por uno o varios centros de atención.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesDXVisados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de centros de atención debe contener uno o más códigos separados, parseables por dbo.splitstring.; Deben existir registros en HCGRUPOCANCERPACIC con ESTADO=3 para los centros indicados.; Los pacientes deben tener correspondencia en INPACIENT, ADINGRESO, INENTIDAD y ADCENATEN.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesDXVisados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran registros en estado visado (ESTADO=3).; Por cada paciente se retorna únicamente su registro más reciente según FECHAREGISTRO.; El filtrado por centro de atención es obligatorio y se aplica tanto en la subconsulta como en la consulta externa.; El resultado se ordena de más reciente a más antiguo por fecha de registro.; La columna ''sel'' siempre se inicializa en 0/false para selección en UI.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesDXVisados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente oncológico; Grupo de cáncer; Diagnóstico visado; Diagnóstico nuevo; Datos modificados; Centro de atención; Ingreso; Entidad (aseguradora); Folio; Edad del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesDXVisados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve solo el último registro por paciente (top 1 por FECHAREGISTRO DESC) cuyo ESTADO=3 y cuyo CODCENATE pertenece a la lista de centros recibida.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesDXVisados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ESTADO = 1 → Etiqueta como ''Pacientes con diagnósticos nuevos''; si ESTADO = 2 → Etiqueta como ''Pacientes con datos modificados''; si ESTADO = 3 → Etiqueta como ''Pacientes con diagnósticos visados'' else Sin etiqueta (NULL)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesDXVisados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.splitstring; dbo.EDAD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesDXVisados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCGRUPOCANCERPACIC; dbo.INPACIENT; dbo.ADINGRESO; dbo.INENTIDAD; dbo.ADCENATEN', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesDXVisados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesDXVisados';
-- GO
