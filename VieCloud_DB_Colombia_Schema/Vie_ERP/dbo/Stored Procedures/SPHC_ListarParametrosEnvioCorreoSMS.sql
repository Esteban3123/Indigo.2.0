
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los parámetros configurados para el envío de alertas por correo electrónico y SMS, filtrando por centro de atención y tipo de servicio. Consulta la tabla de configuración de notificaciones (HCPARALERNOT) para obtener los destinatarios, sus datos de contacto (correo, celular), el nombre con el que aparecen en el envío y el mensaje adicional a incluir. Complementa la información con los datos de los usuarios del sistema que crearon o modificaron cada registro, mostrando código y nombre del responsable. Se utiliza para administrar y consultar a quiénes se les envían notificaciones automáticas de alertas clínicas según el centro de atención y el tipo de servicio configurado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarParametrosEnvioCorreoSMS';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarParametrosEnvioCorreoSMS';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta los parámetros configurados de envío de notificaciones (correo y SMS) para un centro de atención y tipo de servicio específicos, incluyendo datos de auditoría con nombres de usuario.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarParametrosEnvioCorreoSMS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros en HCPARALERNOT que coincidan con el centro de atención y tipo de servicio recibidos.; Los códigos de usuario de creación/modificación, si existen, se resuelven contra SEGusuaru.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarParametrosEnvioCorreoSMS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna parámetros del centro de atención y tipo de servicio indicados.; El indicador de envío de correo se expone como bit (booleano).; Los datos de usuario de creación y modificación se devuelven concatenados como ''código - nombre''.; El cruce con usuarios es opcional (LEFT JOIN): se listan los parámetros aunque no exista usuario asociado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarParametrosEnvioCorreoSMS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Centro de atención; Tipo de servicio/alerta; Notificaciones por correo; Notificaciones por SMS; Usuarios destinatarios de alertas; Auditoría de usuario creación/modificación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarParametrosEnvioCorreoSMS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCPARALERNOT: Cuando CODCENATE = @CentroAtencion y TIPSECALER = @TipoServicio, retorna los parámetros de envío de correo/SMS junto con la información de los usuarios de creación y modificación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarParametrosEnvioCorreoSMS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCPARALERNOT; dbo.SEGusuaru', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarParametrosEnvioCorreoSMS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarParametrosEnvioCorreoSMS';
-- GO
