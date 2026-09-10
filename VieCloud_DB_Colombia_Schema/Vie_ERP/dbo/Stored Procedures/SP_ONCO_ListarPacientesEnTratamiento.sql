

CREATE PROCEDURE [dbo].[SP_ONCO_ListarPacientesEnTratamiento]
(
@CentroAtencion Char(10),
@UnidadFuncional Char(10)
)
AS
BEGIN
	SET NOCOUNT ON;

Select   A.CODTIPPAC,C.FECORDMED AS 'Fecha Orden Medica',P.IPCODPACI AS 'Identificacion',rtrim(ltrim(P.IPNOMCOMP)) as 'NombrePaciente', rtrim(ltrim(ENT.CODENTIDA)) + ' - ' + rtrim(ltrim(ENT.NOMENTIDA)) as 'Entidad',C.NUMINGRES AS 'Ingreso',[dbo].[EDAD] (P.IPFECNACI,getdate()) As 'Edad',
		 Rtrim(X.UFUDESCRI) AS 'UFOrdeno'
FROM  INPACIENT P 
	INNER JOIN HCORDPRON C ON P.IPCODPACI = C.IPCODPACI 
	INNER JOIN ADINGRESO A on A.NUMINGRES = C.NUMINGRES
	INNER JOIN INENTIDAD ENT on ENT.CODENTIDA = A.CODENTIDA 
	INNER JOIN INCUPSIPS  I on I.CODSERIPS  = C.CODSERIPS 
	INNER JOIN INUNIFUNC  X on X.UFUCODIGO   = C.UFUCODIGO
	where I.SERIPSDASH  = 6 AND C.ESTSERIPS = 6 AND  C.CODCENATE = @CentroAtencion 

end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes oncológicos que actualmente se encuentran en tratamiento activo en un centro de atención determinado. Combina datos de identificación y nombre del paciente (INPACIENT), el ingreso o episodio de atención (ADINGRESO), la entidad aseguradora o pagadora (INENTIDAD), las órdenes médicas de procedimientos vigentes con estado activo (HCORDPRON), el servicio CUPS/IPS clasificado como oncológico (INCUPSIPS con SERIPSDASH=6), y la unidad funcional que generó la orden (INUNIFUNC). Calcula además la edad actual del paciente mediante la función Edad. Se utiliza principalmente en módulos de oncología para monitorear y hacer seguimiento de los pacientes que tienen órdenes de tratamiento oncológico pendientes o en curso, filtrando por centro de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ONCO_ListarPacientesEnTratamiento';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ONCO_ListarPacientesEnTratamiento';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Listar los pacientes con órdenes médicas oncológicas activas (en tratamiento) para un centro de atención determinado, junto con su identificación, entidad, ingreso, edad y unidad funcional que ordenó.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesEnTratamiento';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un código de centro de atención válido para filtrar las órdenes médicas.; Las órdenes médicas deben estar relacionadas a ingresos, entidades, servicios CUPS y unidades funcionales existentes (todos los joins son INNER).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesEnTratamiento';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan órdenes cuyo servicio CUPS está clasificado en el dashboard como oncología (SERIPSDASH = 6).; Solo se listan órdenes cuyo estado del servicio corresponde a ''en tratamiento'' (ESTSERIPS = 6).; Solo retorna pacientes asociados al centro de atención solicitado.; Cada fila representa una orden médica vigente vinculada a un paciente, ingreso y entidad responsable.; La edad del paciente se calcula al momento de la consulta usando la función dbo.EDAD sobre la fecha de nacimiento y la fecha actual.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesEnTratamiento';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente oncológico; Orden médica; Tratamiento oncológico; Centro de atención; Unidad funcional; Entidad responsable de pago; Ingreso hospitalario', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesEnTratamiento';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando SERIPSDASH = 6 y ESTSERIPS = 6 y CODCENATE = parámetro de centro, se retorna el conjunto de pacientes en tratamiento con sus datos de orden, ingreso, entidad y unidad funcional.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesEnTratamiento';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INPACIENT; dbo.HCORDPRON; dbo.ADINGRESO; dbo.INENTIDAD; dbo.INCUPSIPS; dbo.INUNIFUNC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesEnTratamiento';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesEnTratamiento';
-- GO
