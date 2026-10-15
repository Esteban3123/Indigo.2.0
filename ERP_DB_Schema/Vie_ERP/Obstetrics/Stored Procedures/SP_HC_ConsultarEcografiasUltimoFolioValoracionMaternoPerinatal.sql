
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