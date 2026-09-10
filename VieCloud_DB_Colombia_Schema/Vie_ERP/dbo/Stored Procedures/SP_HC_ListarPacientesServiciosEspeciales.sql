

CREATE PROCEDURE [dbo].[SP_HC_ListarPacientesServiciosEspeciales]
(
@CentroAtencion Char(10),
@UnidadFuncional Char(10)
)
AS
BEGIN
	SET NOCOUNT ON;

select distinct P.IPCODPACI as Identificacion, rtrim(ltrim(P.IPNOMCOMP)) as NombrePaciente, rtrim(ltrim(ENT.CODENTIDA)) + ' - ' + rtrim(ltrim(ENT.NOMENTIDA)) as Entidad,
f.CODCENATE,f.UFUCODIGO,
(select top 1 FECHINGRES from ..HCONCOFICH  where IPCODPACI = F.IPCODPACI order by FECHINGRES asc ) as FechaIngresoPrograma
from ..INPACIENT p inner join  ..HCONCOFICH  F on P.IPCODPACI = F.IPCODPACI inner join 
..INENTIDAD ENT on ENT.CODENTIDA = P.CODENTIDA 
 where f.CODCENATE = @CentroAtencion and UFUCODIGO = @UnidadFuncional
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes inscritos en servicios especiales (como el programa oncológico) para un centro de atención y unidad funcional específicos. Combina los datos de identificación y nombre completo del paciente (INPACIENT), la entidad aseguradora o pagadora a la que pertenece (INENTIDAD) y la ficha oncológica (HCONCOFICH) para mostrar también la fecha de ingreso más antigua al programa. Se utiliza para consultar el censo de pacientes activos en programas especiales, por ejemplo oncología, filtrando por sede y unidad funcional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarPacientesServiciosEspeciales';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarPacientesServiciosEspeciales';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los pacientes con su entidad y fecha de ingreso al programa, filtrados por centro de atención y unidad funcional, para gestión de servicios especiales en historia clínica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesServiciosEspeciales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El centro de atención y unidad funcional deben existir y tener fichas asociadas en HCONCOFICH; Cada paciente debe tener entidad registrada que exista en INENTIDAD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesServiciosEspeciales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen pacientes con al menos una ficha de historia clínica en el centro y unidad funcional solicitados; La fecha de ingreso al programa corresponde a la más antigua (MIN FECHINGRES) registrada para el paciente en HCONCOFICH, sin filtrar por centro/unidad; Resultado sin duplicados (DISTINCT) por paciente; Solo se incluyen pacientes cuya entidad esté registrada en INENTIDAD (INNER JOIN); Nombre de paciente y código/nombre de entidad se devuelven sin espacios en blanco al inicio o final', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesServiciosEspeciales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Entidad (aseguradora/responsable); Centro de atención; Unidad funcional; Historia clínica; Fecha de ingreso a programa; Servicios especiales', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesServiciosEspeciales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando una ficha (HCONCOFICH) coincide con el centro de atención y unidad funcional indicados, retorna identificación, nombre, entidad y la fecha de ingreso más antigua del paciente al programa', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesServiciosEspeciales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'INPACIENT; HCONCOFICH; INENTIDAD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesServiciosEspeciales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesServiciosEspeciales';
-- GO
