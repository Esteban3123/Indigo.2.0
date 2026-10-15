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