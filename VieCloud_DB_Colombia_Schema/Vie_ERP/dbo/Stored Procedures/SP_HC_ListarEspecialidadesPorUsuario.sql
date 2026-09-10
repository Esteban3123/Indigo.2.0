CREATE proc [dbo].[SP_HC_ListarEspecialidadesPorUsuario]
@userCode char(20)
as
select p.CODESPEC1 code, ltrim(rtrim(s.DESESPECI)) name from INPROFSAL as p 
inner join INESPECIA as s on p.CODESPEC1 = s.CODESPECI
where p.CODPROSAL = @userCode
union
select p.CODESPEC2 code, ltrim(rtrim(s.DESESPECI)) name from INPROFSAL as p 
inner join INESPECIA as s on p.CODESPEC2 = s.CODESPECI
where p.CODPROSAL = @userCode
union
select p.CODESPEC3 code, ltrim(rtrim(s.DESESPECI)) name from INPROFSAL as p 
inner join INESPECIA as s on p.CODESPEC3 = s.CODESPECI
where p.CODPROSAL = @userCode
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Retorna todas las especialidades médicas asignadas a un profesional de la salud específico, dado su código de usuario. Consulta hasta tres especialidades registradas en el perfil del profesional (especialidad 1, 2 y 3) y las une en un único listado con el código y nombre descriptivo de cada especialidad. Combina el maestro de profesionales de la salud (INPROFSAL) con el catálogo de especialidades (INESPECIA) para devolver el nombre legible de cada especialidad. Se utiliza para poblar selectores o filtros en la historia clínica donde se necesita saber en qué especialidades puede atender un profesional determinado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarEspecialidadesPorUsuario';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarEspecialidadesPorUsuario';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las especialidades médicas asociadas a un profesional de salud, consolidando hasta tres especialidades registradas en su perfil.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarEspecialidadesPorUsuario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El profesional debe existir en INPROFSAL identificado por su código.; Los códigos de especialidad referenciados deben existir en INESPECIA para ser retornados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarEspecialidadesPorUsuario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna especialidades cuyo código existe efectivamente en el catálogo INESPECIA (INNER JOIN).; Elimina especialidades duplicadas entre los tres campos por uso de UNION.; El nombre de la especialidad se entrega sin espacios al inicio ni al final.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarEspecialidadesPorUsuario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Profesional de salud; Especialidad médica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarEspecialidadesPorUsuario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve código y nombre (sin espacios) de especialidades unidas desde CODESPEC1, CODESPEC2 y CODESPEC3 del profesional, eliminando duplicados vía UNION.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarEspecialidadesPorUsuario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INPROFSAL; dbo.INESPECIA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarEspecialidadesPorUsuario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarEspecialidadesPorUsuario';
-- GO
