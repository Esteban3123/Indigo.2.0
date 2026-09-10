
-- =============================================
-- Author:      Angie Lorena Galindo
-- Create Date: 27/02/2025
-- Description: Obtiene las ecografias registradas en el último folio de una paciente,  
-- en el page Valoracion Materno Perinatal
-- =============================================
CREATE PROCEDURE [Obstetrics].[SP_HC_ConsultarEcografiasUltimoFolioValoracionMaternoPerinatal]
(
    @IPCODPACI INT
)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        u.UltrasoundDate AS [Fecha ecografía],
        u.WeeksAccordingToUltrasound AS [Semanas],
        GETDATE() AS [Fecha actual],  -- Fecha de hoy
        DATEDIFF(WEEK, u.UltrasoundDate, GETDATE()) + u.WeeksAccordingToUltrasound AS [Semanas a hoy],
        u.EstimatedDateOfDeliveryByUltrasound AS [F.P.P. por ecografía],
        u.Percentile AS [Percentil],
        u.Weight AS [Peso (gramos)],
        u.Size AS [Talla],
        u.AmnioticFluidIndex AS [Índice de líquido amniótico (ILA) en cms],
        u.MaximumVerticalPocket AS [Bolsillo vertical máximo (BVM) en cms],
        u.Observations AS [Observaciones]
    FROM Obstetrics.PerinatalMaternalAssessmentC p
    LEFT JOIN Obstetrics.Ultrasound u
        ON p.Id = u.IdPerinatalMaternalAssessmentC
    WHERE p.IPCODPACI = @IPCODPACI
    AND p.NUMEFOLIO = (
        SELECT MAX(CAST(NUMEFOLIO AS INT)) 
        FROM Obstetrics.PerinatalMaternalAssessmentC 
        WHERE IPCODPACI = @IPCODPACI
    );
END
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Recupera las ecografías obstétricas registradas en el folio más reciente de la valoración materno-perinatal de una paciente identificada por su código. Para cada ecografía devuelve biometría fetal, líquido amniótico, semanas de gestación calculadas a la fecha actual y fecha probable de parto por ecografía. Solo consulta registros del folio con el número máximo, garantizando que se muestren datos de la última evaluación activa.', @level0type=N'SCHEMA', @level0name=N'Obstetrics', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ConsultarEcografiasUltimoFolioValoracionMaternoPerinatal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Obstetrics', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ConsultarEcografiasUltimoFolioValoracionMaternoPerinatal';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera los datos de las ecografías obstétricas registradas en el folio más reciente de la valoración materno-perinatal de una paciente.', @level0type=N'SCHEMA', @level0name=N'Obstetrics', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ConsultarEcografiasUltimoFolioValoracionMaternoPerinatal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La paciente debe existir y tener al menos un registro en PerinatalMaternalAssessmentC; NUMEFOLIO debe ser convertible a INT para poder calcular el máximo', @level0type=N'SCHEMA', @level0name=N'Obstetrics', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ConsultarEcografiasUltimoFolioValoracionMaternoPerinatal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran los registros del último folio numérico (mayor NUMEFOLIO) de la paciente; Las semanas a hoy se calculan como diferencia en semanas entre la fecha de la ecografía y la fecha actual, sumadas a las semanas reportadas en la ecografía; El LEFT JOIN garantiza que se retorne la fila de la valoración aunque no existan ecografías asociadas', @level0type=N'SCHEMA', @level0name=N'Obstetrics', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ConsultarEcografiasUltimoFolioValoracionMaternoPerinatal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ecografía obstétrica; Valoración materno-perinatal; Edad gestacional (semanas por ecografía); Fecha probable de parto (FPP); Percentil fetal; Peso fetal; Talla fetal; Índice de líquido amniótico (ILA); Bolsillo vertical máximo (BVM); Folio de historia clínica', @level0type=N'SCHEMA', @level0name=N'Obstetrics', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ConsultarEcografiasUltimoFolioValoracionMaternoPerinatal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Obstetrics.PerinatalMaternalAssessmentC: Devuelve las ecografías asociadas únicamente al folio con MAX(CAST(NUMEFOLIO AS INT)) de la paciente filtrada por IPCODPACI', @level0type=N'SCHEMA', @level0name=N'Obstetrics', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ConsultarEcografiasUltimoFolioValoracionMaternoPerinatal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Obstetrics.PerinatalMaternalAssessmentC; Obstetrics.Ultrasound', @level0type=N'SCHEMA', @level0name=N'Obstetrics', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ConsultarEcografiasUltimoFolioValoracionMaternoPerinatal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Obstetrics', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ConsultarEcografiasUltimoFolioValoracionMaternoPerinatal';
-- GO
