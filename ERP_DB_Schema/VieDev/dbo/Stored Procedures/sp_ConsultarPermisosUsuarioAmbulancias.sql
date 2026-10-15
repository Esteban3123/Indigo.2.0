CREATE proc [dbo].[sp_ConsultarPermisosUsuarioAmbulancias]
@userCode char(20)
as
select * from SEGpermiu where codusuari = '999' and indidmenu = '255'