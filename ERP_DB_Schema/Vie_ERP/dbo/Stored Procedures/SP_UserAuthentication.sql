CREATE procedure [dbo].[SP_UserAuthentication]
@codeUser char(60),
@passUser char(50)
as
begin
select NOMUSUARI, PASSUSUAR from segusuaru where CODUSUARI = @codeUser and PASSUSUAR = @passUser
end