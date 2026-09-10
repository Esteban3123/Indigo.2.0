
CREATE FUNCTION [dbo].[TipoPaciente] (@CodigoTipoPaciente as int)
RETURNS nvarchar (60)
AS
BEGIN

declare @TipoPaciente nvarchar (60)

SELECT @TipoPaciente = CASE @CodigoTipoPaciente WHEN 1 THEN 'CONTRIBUTIVO' WHEN 2 THEN 'SUBSIDIADO' WHEN 3 THEN 'No afiliado' WHEN 4 THEN 'PARTICULAR' WHEN 5 THEN 'OTRO' WHEN 6 THEN 'DESPLAZADO REG. CONTRIBUTIVO' WHEN 7 THEN 'DESPLAZADO REG. SUBSIDIADO' WHEN 8 THEN 'DESPLAZADO NO ASEGURADO'
WHEN 9 THEN 'Especial o Excepción' WHEN 10 THEN 'Personas privadas de la libertad a cargo del Fondo Nacional de Salud' WHEN 11 THEN 'Tomador/ Amparado ARL' WHEN 12 THEN 'Tomador/ Amparado SOAT' WHEN 13 THEN 'Tomador/ Amparado Planes voluntarios de salud' END

RETURN @TipoPaciente

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que traduce el código numérico del tipo de paciente a su descripción en texto, según el régimen de afiliación al sistema de salud colombiano. Cubre categorías como Contributivo, Subsidiado, Particular, Desplazados, Privados de la libertad, ARL, SOAT y Planes voluntarios. Se usa para mostrar en reportes y consultas el régimen o tipo de cobertura del paciente en lenguaje legible, evitando manejar el código numérico directamente en las interfaces o informes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'TipoPaciente';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'TipoPaciente';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Traducir un código numérico de tipo de paciente a su descripción textual según el catálogo de regímenes y coberturas de afiliación en salud.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoPaciente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe suministrar un código entero que represente el tipo de paciente; valores fuera del rango 1-13 no tienen descripción asociada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoPaciente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El catálogo de tipos de paciente está cableado en código (valores del 1 al 13), no se consulta una tabla maestra.; Códigos fuera del rango 1-13 producen NULL como descripción.; La descripción retornada nunca supera 60 caracteres (tipo de retorno nvarchar(60)).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoPaciente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Tipo de paciente; Régimen contributivo; Régimen subsidiado; No afiliado; Particular; Desplazado; Régimen especial o de excepción; Personas privadas de la libertad (PPL) a cargo del Fondo Nacional de Salud; ARL; SOAT; Planes voluntarios de salud', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoPaciente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Código = 1 → Retorna ''CONTRIBUTIVO''; si Código = 2 → Retorna ''SUBSIDIADO''; si Código = 3 → Retorna ''No afiliado''; si Código = 4 → Retorna ''PARTICULAR''; si Código = 5 → Retorna ''OTRO''; si Código = 6 → Retorna ''DESPLAZADO REG. CONTRIBUTIVO''; si Código = 7 → Retorna ''DESPLAZADO REG. SUBSIDIADO''; si Código = 8 → Retorna ''DESPLAZADO NO ASEGURADO''; si Código = 9 → Retorna ''Especial o Excepción''; si Código = 10 → Retorna ''Personas privadas de la libertad a cargo del Fondo Nacional de Salud''; si Código = 11 → Retorna ''Tomador/ Amparado ARL''; si Código = 12 → Retorna ''Tomador/ Amparado SOAT''; si Código = 13 → Retorna ''Tomador/ Amparado Planes voluntarios de salud'' else Retorna NULL para cualquier otro código no contemplado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoPaciente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoPaciente';
GO
