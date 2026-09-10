
CREATE FUNCTION [dbo].[DiagnosticoPorFolioConcatenados](@CodigoPaciente as varchar(25), @NumIngreso as char(10), @NumFolio as varchar(10))
RETURNS varchar(max)
AS
BEGIN
    DECLARE @Diagnosticos as varchar(max)

SELECT 
	@Diagnosticos = STRING_AGG(
		concat(RTRIM(B.CODDIAGNO), ' - ', RTRIM(B.NOMDIAGNO)), ', ')
FROM INDIAGNOH A
LEFT OUTER JOIN INDIAGNOS B ON A.CODDIAGNO = B.CODDIAGNO
WHERE NUMINGRES = @NumIngreso AND IPCODPACI = @CodigoPaciente AND NUMEFOLIO = @NumFolio
    
    RETURN @Diagnosticos
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que devuelve, en una sola cadena de texto, todos los diagnósticos CIE-10 registrados en un folio específico de la historia clínica de un paciente. Recibe como parámetros la cédula del paciente, el número de ingreso y el número de folio, consulta los diagnósticos del ingreso en INDIAGNOH y los cruza con el catálogo maestro INDIAGNOS para obtener el código y nombre de cada diagnóstico, y los concatena separados por coma. Se usa para mostrar en un solo campo legible todos los diagnósticos asociados a una atención o nota clínica, facilitando reportes, impresiones de documentos clínicos y visualización en pantalla.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'DiagnosticoPorFolioConcatenados';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'DiagnosticoPorFolioConcatenados';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve, en una sola cadena, los diagnósticos (código y nombre) asociados a un folio de atención de un paciente, concatenados y separados por coma.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DiagnosticoPorFolioConcatenados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir relación entre INDIAGNOH (diagnósticos por hospitalización/folio) e INDIAGNOS (catálogo de diagnósticos) por CODDIAGNO.; Se requiere identificar la atención mediante paciente, número de ingreso y número de folio.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DiagnosticoPorFolioConcatenados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Se aplica RTRIM al código y nombre del diagnóstico antes de concatenar.; El uso de LEFT OUTER JOIN garantiza incluir registros de INDIAGNOH aunque no exista coincidencia en el catálogo INDIAGNOS (en cuyo caso el nombre vendrá vacío/NULL).; El separador entre diagnósticos siempre es '', '' y el separador interno entre código y nombre es '' - ''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DiagnosticoPorFolioConcatenados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Folio de atención; Diagnóstico; Catálogo de diagnósticos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DiagnosticoPorFolioConcatenados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Retorna varchar(max) con la concatenación ''CODDIAGNO - NOMDIAGNO'' separada por '', '' para todos los diagnósticos que cumplan NUMINGRES, IPCODPACI y NUMEFOLIO; si no hay coincidencias retorna NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DiagnosticoPorFolioConcatenados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INDIAGNOH; dbo.INDIAGNOS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DiagnosticoPorFolioConcatenados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DiagnosticoPorFolioConcatenados';
GO
