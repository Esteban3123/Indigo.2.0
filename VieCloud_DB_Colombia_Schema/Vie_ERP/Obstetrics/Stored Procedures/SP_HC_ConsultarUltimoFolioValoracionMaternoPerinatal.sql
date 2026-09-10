
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
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Recupera el registro más reciente (último folio) de la valoración materno-perinatal de una paciente identificada por su código, consolidando en una sola consulta los datos clínicos y de riesgo obstétrico, los resultados paraclínicos de los tres trimestres de gestación, y la asesoría anticonceptiva postevento. Utiliza el folio máximo registrado para la paciente como criterio de selección del control prenatal más actual.', @level0type=N'SCHEMA', @level0name=N'Obstetrics', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ConsultarUltimoFolioValoracionMaternoPerinatal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Obstetrics', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ConsultarUltimoFolioValoracionMaternoPerinatal';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera, para una paciente, la información consolidada del último folio de valoración materno-perinatal junto con sus paraclínicos por trimestre y la asesoría anticonceptiva asociada.', @level0type=N'SCHEMA', @level0name=N'Obstetrics', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ConsultarUltimoFolioValoracionMaternoPerinatal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un registro de valoración materno-perinatal para la paciente indicada; El número de folio debe ser convertible a entero (CAST NUMEFOLIO AS INT)', @level0type=N'SCHEMA', @level0name=N'Obstetrics', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ConsultarUltimoFolioValoracionMaternoPerinatal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retorna el folio más reciente (MAX(NUMEFOLIO)) por paciente; La cabecera PerinatalMaternalAssessmentC siempre se conserva mediante LEFT JOIN aunque no existan paraclínicos de algún trimestre o asesoría anticonceptiva; El criterio de ''último folio'' se basa en el valor numérico del folio, no en fecha', @level0type=N'SCHEMA', @level0name=N'Obstetrics', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ConsultarUltimoFolioValoracionMaternoPerinatal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Valoración materno-perinatal; Folio de historia clínica; Edad gestacional por ecografía y FUM; Riesgo obstétrico; Riesgo tromboembólico; Riesgo psicosocial; Grupo sanguíneo y RH materno/paterno; Riesgo de isoinmunización; Coombs; Paraclínicos primer/segundo/tercer trimestre (urocultivo, hemograma, glicemia basal, VIH, sífilis, VDRL, Hepatitis B, TSH, sarampión, toxoplasmosis, citología cervicovaginal, Chagas, PTOG, cultivo retrovaginal); Asesoría anticonceptiva post evento obstétrico; IVE (Interrupción Voluntaria del Embarazo)', @level0type=N'SCHEMA', @level0name=N'Obstetrics', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ConsultarUltimoFolioValoracionMaternoPerinatal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Obstetrics.PerinatalMaternalAssessmentC: Devuelve el conjunto de datos del folio cuyo NUMEFOLIO es el máximo (como entero) para la paciente filtrada por IPCODPACI', @level0type=N'SCHEMA', @level0name=N'Obstetrics', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ConsultarUltimoFolioValoracionMaternoPerinatal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Obstetrics.PerinatalMaternalAssessmentC; Obstetrics.ContraceptiveAdvice; Obstetrics.FirstTrimesterEvaluationParaclinical; Obstetrics.PerinatalMaternalAssessment; Obstetrics.SecondTrimesterEvaluationParaclinical; Obstetrics.ThirdTrimesterEvaluationParaclinical', @level0type=N'SCHEMA', @level0name=N'Obstetrics', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ConsultarUltimoFolioValoracionMaternoPerinatal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Obstetrics', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ConsultarUltimoFolioValoracionMaternoPerinatal';
-- GO
