
CREATE PROCEDURE [dbo].[SPREP_HC_Generales_DietasPaciente]
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
          
SELECT A.CODTIPDIE AS 'CODIGO TIPO DIETA',RTRIM(B.DESTIPDIE) AS 'DESCRIPCION TIPO DE DIETA', A.OBSERVACI AS OBSERVACIONES, B.BreastMilk 

FROM HCREGDIET A With(Nolock)
LEFT JOIN CHTIPDIET B With(Nolock) ON A.CODTIPDIE=B.CODTIPDIE 
WHERE A.IPCODPACI=@CodigoPaciente AND A.NUMINGRES=@NumeroIngreso AND A.NUMEFOLIO=@NumeroFolio 

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta las dietas prescritas a un paciente hospitalizado durante un ingreso específico. Dado el código del paciente (cédula), el número de ingreso y el número de folio de historia clínica, retorna el tipo de dieta asignada, su descripción y observaciones, incluyendo si aplica leche materna. Combina el registro de dietas del paciente (HCREGDIET) con el catálogo de tipos de dieta (CHTIPDIET) para mostrar información completa sobre el régimen alimenticio o nutricional prescrito. Se usa en la historia clínica general para visualizar la alimentación indicada al paciente durante su hospitalización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_DietasPaciente';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_DietasPaciente';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta las dietas registradas para un paciente en un ingreso y folio específicos, devolviendo tipo de dieta, descripción, observaciones e indicador de leche materna.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_DietasPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros de dietas asociados al paciente, ingreso y folio indicados para obtener resultados.; El catálogo de tipos de dieta debe estar disponible para resolver descripción e indicador de leche materna.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_DietasPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna dietas asociadas a la combinación exacta paciente-ingreso-folio.; El tipo de dieta se resuelve por catálogo; si no existe en el catálogo, igualmente se devuelve la fila por el LEFT JOIN con descripción nula.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_DietasPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Dieta; Tipo de dieta; Ingreso hospitalario; Folio de historia clínica; Leche materna (BreastMilk)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_DietasPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCREGDIET: Devuelve el listado de dietas del paciente filtrando por paciente, número de ingreso y número de folio, enriquecido con la descripción del tipo de dieta y la marca BreastMilk del catálogo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_DietasPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCREGDIET; dbo.CHTIPDIET', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_DietasPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_DietasPaciente';
-- GO
