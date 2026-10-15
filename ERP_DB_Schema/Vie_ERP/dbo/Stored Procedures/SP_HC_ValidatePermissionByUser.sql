CREATE proc [dbo].[SP_HC_ValidatePermissionByUser]
@userCode char(20)
as
select indidmenu as code from SEGpermiu where codusuari = @userCode and indidmenu = '255' union
select indidmenu as code from SEGpermiu where codusuari = @userCode and indidmenu = '256'