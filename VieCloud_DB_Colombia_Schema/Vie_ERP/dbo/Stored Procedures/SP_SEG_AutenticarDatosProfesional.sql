

CREATE PROCEDURE [dbo].[SP_SEG_AutenticarDatosProfesional]
(
@PKeyUser Char(20)
)
AS
BEGIN
	SET NOCOUNT ON;
SELECT CODPROSAL AS CODIGO, NOMMEDICO AS NOMBREPROFESIONAL, CODESPEC1 AS ESPECIALIDAD1, CODESPEC2 AS ESPECIALIDAD2, CODESPEC3 AS ESPECIALIDAD3,  
                        TARJETAPR AS TARJETAPROFESIONAL, ESTADOMED AS ESTADO,   TIPPROFES AS TIPOPROFESIONAL,    MEDPERCIR AS PERFILCIRUGIA

FROM dbo.INPROFSAL
WHERE CODUSUARI = @PKeyUser
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autentica y recupera los datos de un profesional de la salud a partir de su usuario de sistema. Dado un código de usuario (@PKeyUser), consulta el maestro de profesionales (INPROFSAL) y retorna su código, nombre completo, hasta tres especialidades, número de tarjeta profesional, estado (activo/inactivo), tipo de profesional y perfil de cirugía. Se usa en los flujos de login o validación de identidad para confirmar que el usuario que inicia sesión corresponde a un profesional habilitado y obtener su información clínica y administrativa en un solo paso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_SEG_AutenticarDatosProfesional';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_SEG_AutenticarDatosProfesional';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera los datos profesionales (código, nombre, especialidades, tarjeta, estado, tipo y perfil de cirugía) asociados a un usuario autenticado del sistema.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SEG_AutenticarDatosProfesional';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en el maestro de profesionales asociado al código de usuario suministrado; de lo contrario el resultado será vacío.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SEG_AutenticarDatosProfesional';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La búsqueda del profesional se realiza exclusivamente por el código de usuario (CODUSUARI), estableciendo una relación entre la identidad del usuario autenticado y su ficha profesional.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SEG_AutenticarDatosProfesional';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Profesional de la salud; Especialidades médicas; Tarjeta profesional; Perfil de cirugía; Tipo de profesional; Estado del profesional', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SEG_AutenticarDatosProfesional';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.INPROFSAL: Cuando CODUSUARI coincide con el código de usuario recibido, retorna los datos identificatorios y profesionales (código, nombre, hasta tres especialidades, tarjeta profesional, estado, tipo y perfil de cirugía).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SEG_AutenticarDatosProfesional';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INPROFSAL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SEG_AutenticarDatosProfesional';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_SEG_AutenticarDatosProfesional';
-- GO
