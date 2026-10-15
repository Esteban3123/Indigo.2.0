
CREATE PROCEDURE [dbo].[SPHC_ListarParametrosEnvioCorreoSMS]
(
@CentroAtencion Char(10),
@TipoServicio Char(1)
)
AS
BEGIN

	SET NOCOUNT ON;

	SELECT 
		A.CODCONSEC as Consecutivo, A.CODCENATE as Centro, A.TIPSECALER as TipoServicios, cast(A.ENVMAILUS as bit) as EnviarCorreo, A.ENVSMSUSU  as EnviarSms,
		A.NOMCOMUSU as NombreUsuario, A.NOMCORSMS as NombreEnvioSms, A.NUMTELMOV as MovilSMS, A.CORELEUSU as Correo, A.MENADICOR as MensajeAdicional, 
		A.CODCONSEC As 'ID', A.USUARIOCREACION, A.FECHACREACION, A.USUARIOMODIFICACION, A.FECHAMODIFICACION, 
		Rtrim(U1.CODUSUARI) + ' - ' + Rtrim(U1.NOMUSUARI) AS 'Usuario_Creacion', Rtrim(U2.CODUSUARI) + ' - ' + Rtrim(U2.NOMUSUARI) AS 'Usuario_Modificacion' 
		
    FROM 
		dbo.HCPARALERNOT As A 
		Left Outer Join dbo.SEGusuaru As U1 On A.USUARIOCREACION = U1.CODUSUARI 
		Left Outer Join.dbo.SEGusuaru As U2 On A.USUARIOMODIFICACION = U2.CODUSUARI 
		
    WHERE 
		A.CODCENATE = @CentroAtencion  
		AND A.TIPSECALER = @TipoServicio 

   
END