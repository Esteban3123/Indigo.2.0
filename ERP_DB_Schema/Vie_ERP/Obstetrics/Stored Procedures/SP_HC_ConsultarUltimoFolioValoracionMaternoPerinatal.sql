
-- =============================================
-- Author:      Angie Lorena Galindo
-- Create Date: 28/02/2025
-- Description: Obtiene los datos registrados en el último folio de una paciente,  
-- en el page Valoracion Materno Perinatal
-- =============================================
CREATE PROCEDURE [Obstetrics].[SP_HC_ConsultarUltimoFolioValoracionMaternoPerinatal]
(
    @IPCODPACI INT
)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        pma.ContinueWithTheHug,
        pma.DateLastMenstruation,
        pma.ReliableMenstruationDate,
        pma.DateFirstUltrasound,
        pma.WeeksAccordingToUltrasound,
        pma.GestationNumber,
        pma.CauseOfIVE,
        pma.ObstetricRisk,
        pma.ThromboembolicRisk,
        pma.PsychosocialRisk,
        pma.MaternalBloodGroup,
        pma.MaternalRH,
        pma.NotRememberMaternalRH,
        pma.PaternalBloodGroup,
        pma.PaternalRH,
        pma.NotRememberPaternalRH,
        pma.RiskOfIsoimmunization,
        pma.Sensitized,
        pma.Coombs,
        ftep.UrineCulture,
        ftep.UrineCultureDate,
        ftep.BloodCount,
        ftep.BloodCountDate,
        ftep.BasalGlycemia,
        ftep.BasalGlycemiaDate,
        ftep.HIV1,
        ftep.HIV1Date,
        ftep.HIV2,
        ftep.HIV2Date,
        ftep.RapidSyphilisTest,
        ftep.RapidSyphilisTestDate,
        ftep.VDRL,
        ftep.VDRLDate,
        ftep.Dilutions,
        ftep.HepatitisBAntibodies,
        ftep.HepatitisBAntibodiesDate,
        ftep.TSH,
        ftep.TSHDate,
        ftep.IgGMeasles,
        ftep.IgGMeaslesDate,
        ftep.IgMMeasles,
        ftep.IgMMeaslesDate,
        ftep.IgGToxoplasmosis,
        ftep.IgGToxoplasmosisDate,
        ftep.IgMToxoplasmosis,
        ftep.IgMToxoplasmosisDate,
        ftep.CervicovaginalCytology,
        ftep.CervicovaginalCytologyDate,
        ftep.CytologyResult,
        ftep.Chagas,
        ftep.ChagasDate,
        ftep.Observations,
        step.HIV1,
        step.HIV1Date,
        step.HIV2,
        step.HIV2Date,
        step.BloodCount,
        step.BloodCountDate,
        step.PTOG,
        step.PTOGDate,
        step.VDRL,
        step.VDRLDate,
        step.Dilutions,
        step.IgMToxoplasmosis,
        step.IgMToxoplasmosisDate,
        step.Observations,
        ttep.HIV1, 
        ttep.HIV1Date, 
        ttep.HIV2, 
        ttep.HIV2Date, 
        ttep.VDRL, 
        ttep.VDRLDate, 
        ttep.Dilutions, 
        ttep.IgMToxoplasmosis, 
        ttep.IgMToxoplasmosisDate, 
        ttep.RetrovaginalCulture, 
        ttep.RetrovaginalCultureDate, 
        ttep.Observations, 
        ca.ContraceptiveAdvice,
        ca.PostObstetricEventContraceptive,
        ca.ContraceptiveMethod,
        ca.Observations
    FROM Obstetrics.PerinatalMaternalAssessmentC p
    LEFT JOIN Obstetrics.ContraceptiveAdvice ca
        ON p.Id = ca.IdPerinatalMaternalAssessmentC
    LEFT JOIN Obstetrics.FirstTrimesterEvaluationParaclinical ftep
        ON p.Id = ftep.IdPerinatalMaternalAssessmentC
    LEFT JOIN Obstetrics.PerinatalMaternalAssessment pma
        ON p.Id = pma.IdPerinatalMaternalAssessmentC
    LEFT JOIN Obstetrics.SecondTrimesterEvaluationParaclinical step
        ON p.Id = step.IdPerinatalMaternalAssessmentC
    LEFT JOIN Obstetrics.ThirdTrimesterEvaluationParaclinical ttep 
        ON p.Id = ttep.IdPerinatalMaternalAssessmentC
    WHERE p.IPCODPACI = @IPCODPACI
    AND p.NUMEFOLIO = (
        SELECT MAX(CAST(NUMEFOLIO AS INT)) 
        FROM Obstetrics.PerinatalMaternalAssessmentC 
        WHERE IPCODPACI = @IPCODPACI
    );
END