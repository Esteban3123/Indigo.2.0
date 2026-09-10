
create FUNCTION [dbo].[EstadoCivilPaciente] (@EstadoCivil as int, @Sexopaciente as int)
RETURNS nvarchar (60)
AS
BEGIN

declare @EstadoCivilPaciente nvarchar (60)

SELECT @EstadoCivilPaciente = CASE WHEN (@EstadoCivil = 1 AND @Sexopaciente = 1) THEN 'SOLTERO' WHEN (@EstadoCivil = 1 AND @Sexopaciente = 2) THEN 'SOLTERA' WHEN (@EstadoCivil = 2 AND @Sexopaciente = 1) THEN 'CASADO' WHEN (@EstadoCivil = 2 AND @Sexopaciente = 2) THEN 'CASADA' WHEN (@EstadoCivil = 3 AND @Sexopaciente = 1) THEN 'VIUDO' WHEN (@EstadoCivil = 3 AND @Sexopaciente = 2) THEN 'VIUDA' WHEN (@EstadoCivil = 4 AND @Sexopaciente = 1) THEN 'UNION LIBRE' WHEN (@EstadoCivil = 4 AND @Sexopaciente = 2) THEN 'UNION LIBRE' WHEN (@EstadoCivil = 5 AND @Sexopaciente = 1) THEN 'SEPARADO' WHEN (@EstadoCivil = 5 AND @Sexopaciente = 2) THEN 'SEPARADA' END  

RETURN @EstadoCivilPaciente

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que convierte los códigos numéricos de estado civil y sexo del paciente en su descripción textual con concordancia de género (por ejemplo: SOLTERO/SOLTERA, CASADO/CASADA, VIUDO/VIUDA, SEPARADO/SEPARADA, UNION LIBRE). Recibe el código de estado civil (1=soltero, 2=casado, 3=viudo, 4=unión libre, 5=separado) y el código de sexo del paciente (1=masculino, 2=femenino), y retorna la etiqueta correspondiente en español con género correcto. Se utiliza para mostrar el estado civil del paciente en documentos, reportes clínicos, historia clínica e informes de RIPS con la forma gramaticalmente adecuada según el sexo registrado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'EstadoCivilPaciente';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'EstadoCivilPaciente';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Traduce los códigos numéricos de estado civil y sexo del paciente a su descripción textual con concordancia de género.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EstadoCivilPaciente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los códigos de estado civil deben pertenecer al dominio {1,2,3,4,5}.; Los códigos de sexo deben pertenecer al dominio {1=masculino, 2=femenino}.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EstadoCivilPaciente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El texto del estado civil se concuerda en género según el sexo del paciente (masculino=1, femenino=2), excepto ''UNION LIBRE'' que es invariante al sexo.; Solo se reconocen 5 códigos de estado civil (1=Soltero, 2=Casado, 3=Viudo, 4=Unión libre, 5=Separado); cualquier otro valor produce NULL.; Solo se reconocen 2 códigos de sexo (1 y 2); otros valores producen NULL (salvo estado civil 4).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EstadoCivilPaciente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; estado civil; sexo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EstadoCivilPaciente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve la descripción del estado civil concordada en género; si la combinación de códigos no coincide con ningún CASE, retorna NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EstadoCivilPaciente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si EstadoCivil=1 y Sexo=1 → Devuelve ''SOLTERO''; si EstadoCivil=1 y Sexo=2 → Devuelve ''SOLTERA''; si EstadoCivil=2 y Sexo=1 → Devuelve ''CASADO''; si EstadoCivil=2 y Sexo=2 → Devuelve ''CASADA''; si EstadoCivil=3 y Sexo=1 → Devuelve ''VIUDO''; si EstadoCivil=3 y Sexo=2 → Devuelve ''VIUDA''; si EstadoCivil=4 (cualquier sexo) → Devuelve ''UNION LIBRE''; si EstadoCivil=5 y Sexo=1 → Devuelve ''SEPARADO''; si EstadoCivil=5 y Sexo=2 → Devuelve ''SEPARADA''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EstadoCivilPaciente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'EstadoCivilPaciente';
GO
