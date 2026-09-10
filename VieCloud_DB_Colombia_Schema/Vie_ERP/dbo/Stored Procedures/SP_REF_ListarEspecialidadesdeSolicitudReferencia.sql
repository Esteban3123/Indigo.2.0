-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,09-04-2019,>
-- Description:	<Description,Sp que me lista las especialidades de Solicitud de referencia ,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_REF_ListarEspecialidadesdeSolicitudReferencia] 
(
@AUTO as varchar(200)
)

AS
BEGIN
	
	SET NOCOUNT ON;

	Declare @valores nvarchar(MAX)
	SELECT @valores= COALESCE(Rtrim(@valores)+ ' - ', '') + Rtrim(DESESPECI) FROM HCREFESPECIALIDADES a
	 Inner join INESPECIA B ON A.CODESPECI = B.CODESPECI WHERE  IDHCREFCONT = @AUTO
	select @valores as 'Especialidades'

	--exec sp_executesql @valores 

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todas las especialidades médicas asociadas a una solicitud de referencia o remisión clínica específica, identificada por su número de radicado o identificador único (@AUTO). Consulta las especialidades registradas en la remisión (HCREFESPECIALIDADES) y obtiene su nombre descriptivo desde el catálogo maestro de especialidades (INESPECIA), concatenando todos los nombres en una sola cadena separada por guiones. Devuelve un único campo llamado ''Especialidades'' con el listado textual de todas las especialidades a las que fue referido el paciente (por ejemplo: ''Cardiología - Ortopedia - Neurología''). Se utiliza para visualizar de forma resumida las especialidades involucradas en una solicitud de referencia o remisión dentro de la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_REF_ListarEspecialidadesdeSolicitudReferencia';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_REF_ListarEspecialidadesdeSolicitudReferencia';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve, en una sola cadena concatenada, las descripciones de las especialidades asociadas a una solicitud de referencia identificada por su autorización.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_ListarEspecialidadesdeSolicitudReferencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el identificador de la solicitud de referencia (contexto/autorización) recibido como entrada.; El catálogo de especialidades debe tener correspondencia con los códigos registrados en las especialidades de la referencia.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_ListarEspecialidadesdeSolicitudReferencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las especialidades retornadas se concatenan en una sola cadena separadas por '' - ''.; Solo se incluyen especialidades cuyo código exista en el catálogo de especialidades (INESPECIA).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_ListarEspecialidadesdeSolicitudReferencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Solicitud de referencia; Especialidad médica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_ListarEspecialidadesdeSolicitudReferencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando existen especialidades vinculadas a la solicitud de referencia (IDHCREFCONT = parámetro), se retorna un resultset de una columna ''Especialidades'' con las descripciones concatenadas separadas por '' - ''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_ListarEspecialidadesdeSolicitudReferencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCREFESPECIALIDADES; dbo.INESPECIA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_ListarEspecialidadesdeSolicitudReferencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_ListarEspecialidadesdeSolicitudReferencia';
-- GO
