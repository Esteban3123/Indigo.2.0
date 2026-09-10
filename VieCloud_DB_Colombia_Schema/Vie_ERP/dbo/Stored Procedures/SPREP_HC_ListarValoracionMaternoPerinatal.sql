
CREATE PROCEDURE [dbo].[SPREP_HC_ListarValoracionMaternoPerinatal]
    @CodigoPaciente VARCHAR(25),
    @NumeroIngreso NCHAR(10),
    @NumeroFolio CHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    -- 1. Cabecera (PerinatalMaternalAssessmentC)
    -- Asumimos que esta tabla tiene los campos de condición directamente
    SELECT Id 
    FROM Obstetrics.PerinatalMaternalAssessmentC 
    WHERE IPCODPACI = @CodigoPaciente AND NUMINGRES = @NumeroIngreso AND NUMEFOLIO = @NumeroFolio;

    -- 2. PerinatalMaternalAssessment
    SELECT A.Id, C.ContinueWithTheHug, 
           CASE C.ContinueWithTheHug WHEN 1 THEN 'Sí' WHEN 0 THEN 'No' ELSE 'Sin Dato' END AS ContinueWithTheHugAns, 
           ISNULL(FORMAT(C.DatelastMenstruation, 'dd/MM/yyyy'), 'Sin dato') AS DatelastMenstruation,
           C.ReliableMenstruationDate,
           CASE C.ReliableMenstruationDate WHEN 0 THEN 'No' WHEN 1 THEN 'Sí' ELSE 'Sin Dato' END AS ReliableMenstruationDateAns, 
           ISNULL(FORMAT(C.DateFirstUltrasound, 'dd/MM/yyyy'), 'Sin dato') AS DateFirstUltrasound,
           C.WeeksAccordingToUltrasound, C.UterineHeight, C.GestationalAge, C.GestationalAgeResult,
           ISNULL(FORMAT(C.ProbableDeliveryDate, 'dd'' de ''MMMM'' de ''yyyy', 'es-ES'), 'Sin dato') AS ProbableDeliveryDates,
           C.GestationNumber, C.CauseOfIVE,
           CASE C.CauseOfIVE WHEN 1 THEN 'Riesgo para la vida o salud de la mujer' WHEN 2 THEN 'Grave malformación del feto' WHEN 3 THEN 'Violencia sexual, incesto o inseminación artificial no consentida' ELSE 'Gestación menor de 24 semanas' END AS CauseOfIVEAns,
           C.ObstetricRisk, CASE C.ObstetricRisk WHEN 1 THEN 'Alto' WHEN 2 THEN 'Bajo' ELSE 'Sin dato' END AS ObstetricRiskAns,
           C.ThromboembolicRisk, CASE C.ThromboembolicRisk WHEN 1 THEN 'Muy alto' WHEN 2 THEN 'Alto' WHEN 3 THEN 'Moderado' WHEN 4 THEN 'Bajo' ELSE 'Sin dato' END AS ThromboembolicRiskAns, 
           C.PsychosocialRisk, CASE C.PsychosocialRisk WHEN 1 THEN 'Alto' WHEN 2 THEN 'Bajo' ELSE 'Sin dato' END AS PsychosocialRiskAns,
           C.NotRememberMaternalRH,
           CONCAT(CASE WHEN MaternalBloodGroup = 1 THEN 'A' WHEN MaternalBloodGroup = 2 THEN 'B' WHEN MaternalBloodGroup = 3 THEN 'AB' WHEN MaternalBloodGroup = 4 THEN 'O' ELSE 'No recuerda' END, ' ', CASE WHEN MaternalRH = 1 THEN '+' WHEN MaternalRH = 2 THEN '-' ELSE '' END) AS MaternalBlood,
           CASE PaternalRH WHEN 1 THEN '+' WHEN 2 THEN '-' ELSE '' END AS PaternalRHAns,
           CONCAT(CASE WHEN PaternalBloodGroup = 1 THEN 'A' WHEN PaternalBloodGroup = 2 THEN 'B' WHEN PaternalBloodGroup = 3 THEN 'AB' WHEN PaternalBloodGroup = 4 THEN 'O' ELSE 'No recuerda' END, ' ', CASE WHEN PaternalRH = 1 THEN '+' WHEN PaternalRH = 2 THEN '-' ELSE '' END) AS PaternalBlood,
           C.NotRememberPaternalRH, C.RiskOfIsoimmunization,
           CASE C.RiskOfIsoimmunization WHEN 1 THEN 'Sí' WHEN 2 THEN 'No' ELSE 'Sin dato' END AS RiskOfIsoimmunizationAns,
           C.Sensitized, CASE C.Sensitized WHEN 1 THEN 'Sin dato' WHEN 2 THEN 'Sí' WHEN 3 THEN 'No' WHEN 4 THEN 'Riesgo no evaluado' ELSE 'Sin dato' END AS SensitizedAns, 
           C.Coombs, CASE C.Coombs WHEN 1 THEN 'Sin dato' WHEN 2 THEN 'Positivo' WHEN 3 THEN 'Negativo' WHEN 4 THEN 'Riesgo no evaluado' ELSE 'Sin dato' END AS CoombsAns
    FROM Obstetrics.PerinatalMaternalAssessment C 
    INNER JOIN Obstetrics.PerinatalMaternalAssessmentC A ON A.Id = C.IdPerinatalMaternalAssessmentC 
    WHERE A.IPCODPACI = @CodigoPaciente AND A.NUMINGRES = @NumeroIngreso AND A.NUMEFOLIO = @NumeroFolio;

    -- 3. ContraceptiveAdvice
    SELECT A.Id, 
           CASE C.ContraceptiveAdvice WHEN 1 THEN 'Sí' WHEN 0 THEN 'No' ELSE 'Sin dato' END AS ContraceptiveAdviceAns,
           CASE C.PostObstetricEventContraceptive WHEN 1 THEN 'Sí' WHEN 0 THEN 'No' ELSE 'No aplica' END AS PostObstetricEventContraceptiveAns,
           CASE C.ContraceptiveMethod WHEN 1 THEN 'Anillo vaginal' WHEN 2 THEN 'Coito interrumpido' WHEN 3 THEN 'Condón' WHEN 5 THEN 'DIU' WHEN 8 THEN 'Implante subdémico' WHEN 10 THEN 'Anticonceptivo oral' ELSE 'No aplica' END AS ContraceptiveMethodAns,
           ISNULL(C.Observations, 'No registra') AS Observations
    FROM Obstetrics.ContraceptiveAdvice C
    INNER JOIN Obstetrics.PerinatalMaternalAssessmentC A ON A.Id = C.IdPerinatalMaternalAssessmentC
    WHERE A.IPCODPACI = @CodigoPaciente AND A.NUMINGRES = @NumeroIngreso AND A.NUMEFOLIO = @NumeroFolio;

    -- 4. FirstTrimesterEvaluationParaclinical
    SELECT A.Id, ISNULL(UrineCulture, 'Sin dato') AS UrineCulture, ISNULL(FORMAT(UrineCultureDate, 'dd/MM/yyyy'), 'Sin dato') AS UrineCultureDate,
           CASE HIV1 WHEN 1 THEN 'Positivo' WHEN 2 THEN 'Negativo' ELSE 'Sin dato' END AS HIV1Ans,
           CASE CervicovaginalCytology WHEN 0 THEN 'Normal' WHEN 1 THEN 'Anormal' ELSE 'Sin dato' END AS CervicovaginalCytologyAns,
           ISNULL(Observations, 'Sin dato') AS Observations
    FROM Obstetrics.FirstTrimesterEvaluationParaclinical C
    INNER JOIN Obstetrics.PerinatalMaternalAssessmentC A ON A.Id = C.IdPerinatalMaternalAssessmentC
    WHERE A.IPCODPACI = @CodigoPaciente AND A.NUMINGRES = @NumeroIngreso AND A.NUMEFOLIO = @NumeroFolio;

    -- 5. Ultrasound
    SELECT A.Id, ISNULL(FORMAT(UltrasoundDate, 'dd/MM/yyyy'), 'Sin dato') AS UltrasoundDate,
           ISNULL(CONVERT(VARCHAR, WeeksAccordingToUltrasound, 23), 'Sin dato') AS WeeksAccordingToUltrasound,
           ISNULL(CONVERT(VARCHAR, Weight, 23), 'Sin dato') AS Weight,
           ISNULL(CONVERT(VARCHAR, Observations, 23), 'Sin dato') AS Observations
    FROM Obstetrics.Ultrasound U
    INNER JOIN Obstetrics.PerinatalMaternalAssessmentC A ON A.Id = U.IdPerinatalMaternalAssessmentC 
    WHERE A.IPCODPACI = @CodigoPaciente AND A.NUMINGRES = @NumeroIngreso AND A.NUMEFOLIO = @NumeroFolio;

    -- 6. SecondTrimesterEvaluationParaclinical
    SELECT A.Id, CASE HIV1 WHEN 1 THEN 'Positivo' WHEN 2 THEN 'Negativo' ELSE 'Sin dato' END AS HIV1Ans,
           ISNULL(BloodCount, 'Sin dato') AS BloodCount,
           ISNULL(Observations, 'Sin dato') AS Observations
    FROM Obstetrics.SecondTrimesterEvaluationParaclinical S
    INNER JOIN Obstetrics.PerinatalMaternalAssessmentC A ON A.Id = S.IdPerinatalMaternalAssessmentC 
    WHERE A.IPCODPACI = @CodigoPaciente AND A.NUMINGRES = @NumeroIngreso AND A.NUMEFOLIO = @NumeroFolio;

    -- 7. ThirdTrimesterEvaluationParaclinical
    SELECT A.Id, CASE VDRL WHEN 1 THEN 'Positivo' WHEN 2 THEN 'Negativo' ELSE 'Sin dato' END AS VDRLAns,
           ISNULL(RetrovaginalCulture, 'Sin dato') AS RetrovaginalCulture,
           ISNULL(Observations, 'Sin dato') AS Observations
    FROM Obstetrics.ThirdTrimesterEvaluationParaclinical C
    INNER JOIN Obstetrics.PerinatalMaternalAssessmentC A ON A.Id = C.IdPerinatalMaternalAssessmentC 
    WHERE A.IPCODPACI = @CodigoPaciente AND A.NUMINGRES = @NumeroIngreso AND A.NUMEFOLIO = @NumeroFolio;

END
GO


