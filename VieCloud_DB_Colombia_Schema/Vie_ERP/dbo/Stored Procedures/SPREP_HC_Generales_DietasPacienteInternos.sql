
CREATE PROCEDURE [dbo].[SPREP_HC_Generales_DietasPacienteInternos]
(
@CodigoPaciente Varchar(25),
@NumeroFolio Char(10),
@NumeroIngreso Char(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
          
SELECT A.CODTIPDIE AS 'CODIGO TIPO DIETA',RTRIM(B.DESTIPDIE) AS 'DESCRIPCION TIPO DE DIETA', A.OBSERVACI AS OBSERVACIONES

FROM HCREGDIEI A WITH(NOLOCK)
INNER JOIN CHTIPDIET B WITH(NOLOCK) ON A.CODTIPDIE=B.CODTIPDIE 
WHERE A.IPCODPACI=@CodigoPaciente AND A.NUMINGRES=@NumeroIngreso AND A.NUMEFOLIO=@NumeroFolio

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta las dietas prescritas a un paciente internado, retornando el tipo de dieta y sus observaciones para un ingreso y folio específicos de la historia clínica. Combina el registro de dietas del paciente (HCREGDIEI) con el catálogo de tipos de dieta hospitalaria (CHTIPDIET) para mostrar tanto el código como la descripción legible del régimen alimenticio indicado. Se utiliza en la visualización del componente de dietas dentro de la historia clínica general del paciente hospitalizado, identificado por su cédula, número de ingreso y número de folio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_DietasPacienteInternos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_DietasPacienteInternos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta el listado de dietas asignadas a un paciente internado, identificadas por su ingreso y folio, mostrando el código y descripción del tipo de dieta junto con observaciones.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_DietasPacienteInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un registro de dieta para la combinación paciente/ingreso/folio recibida para retornar filas; El tipo de dieta registrado debe existir en el catálogo de tipos de dieta', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_DietasPacienteInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelven dietas asociadas a la combinación exacta de paciente, ingreso y folio; Cada dieta registrada debe tener un tipo de dieta válido en el catálogo (INNER JOIN), si no existe correspondencia no se reporta', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_DietasPacienteInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente internado; dieta; tipo de dieta; ingreso hospitalario; folio de historia clínica; observaciones de dieta', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_DietasPacienteInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCREGDIEI: Cuando IPCODPACI, NUMINGRES y NUMEFOLIO coinciden con los parámetros, retorna código de tipo de dieta, descripción (sin espacios a la derecha) y observaciones', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_DietasPacienteInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCREGDIEI; dbo.CHTIPDIET', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_DietasPacienteInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_DietasPacienteInternos';
-- GO
