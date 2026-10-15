CREATE proc [dbo].[sp_AddToken]
@user char(20),
@token char(100),
@expiration datetime
as
update segusuaru set token = @token, expiration = @expiration where codusuari = @user