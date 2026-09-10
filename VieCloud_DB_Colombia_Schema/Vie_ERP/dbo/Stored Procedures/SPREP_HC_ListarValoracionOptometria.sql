
CREATE OR ALTER PROCEDURE [dbo].[SPREP_HC_ListarValoracionOptometria]
    @CodigoPaciente VARCHAR(25),
    @NumeroIngreso NCHAR(10),
    @NumeroFolio CHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    -- ─────────────────────────────────────────────────────────────────────────
    -- Result set 0: Optometry Cabecera
    -- ─────────────────────────────────────────────────────────────────────────
    SELECT A.Id
    FROM [Glasses].[OptometryClinicalEvaluationC] AS A WITH(NOLOCK)
    WHERE A.IPCODPACI = @CodigoPaciente
      AND A.NUMINGRES = @NumeroIngreso
      AND A.NUMEFOLIO = @NumeroFolio;

    -- ─────────────────────────────────────────────────────────────────────────
    -- Result set 1: Tab1 Anamnesis Antecedentes
    -- ─────────────────────────────────────────────────────────────────────────
    SELECT A.Id,
        -- Tab 1.1: Anamnesis
        (SELECT STRING_AGG((CASE C.Code
            WHEN 1  THEN 'Ardor'
            WHEN 2  THEN 'Astenopia'
            WHEN 3  THEN 'Cefalea'
            WHEN 4  THEN 'Dolor ocular'
            WHEN 5  THEN 'Fotofobia'
            WHEN 6  THEN 'Lagrimeo'
            WHEN 7  THEN 'Diplopia'
            WHEN 8  THEN 'Hiperemia'
            WHEN 9  THEN 'Secreciones'
            WHEN 10 THEN 'Prurito'
            WHEN 11 THEN 'Miodesopsias'
            WHEN 12 THEN 'Confusión al leer'
            WHEN 13 THEN 'Mareo'
            WHEN 14 THEN CONCAT('Otros: ', RTRIM(C.OtherDescription))
            WHEN 15 THEN 'No refiere'
            ELSE NULL END), ', ') WITHIN GROUP (ORDER BY CAST(C.CODE AS INT) ASC)
         FROM [Glasses].[OptometryClinicalEvaluationC] AS OCE WITH(NOLOCK)
         INNER JOIN [Glasses].[OptometryAnamnesisDetail] AS C WITH(NOLOCK)
             ON OCE.Id = C.IdOptometryClinicalEvaluationC
         WHERE OCE.Id = A.Id
        ) AS 'AnamnesisSymptoms',
        RTRIM(B.OphthalmologicalAnamnesisObservations) AS 'OphthalmologicalAnamnesisObservations',
        (CASE WHEN RTRIM(B.OphthalmologicalAnamnesisObservations) IS NULL  THEN CAST(0 AS BIT)
              WHEN RTRIM(B.OphthalmologicalAnamnesisObservations) = ''     THEN CAST(0 AS BIT)
              ELSE CAST(1 AS BIT) END) AS 'EnableTab1.1.OphthalmologicalAnamnesisObservationsSB',

        -- Tab 1.2: Antecedentes
        (CASE D.PersonalHistory    WHEN 1 THEN 'Sí' WHEN 2 THEN 'No' ELSE NULL END) AS 'PersonalHistory',
        (CASE D.PersonalHistory    WHEN 1 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END) AS 'EnableTab1.2.WhichPersonal',
        RTRIM(D.WhichPersonal) AS 'WhichPersonal',
        (CASE D.FamilyHistory      WHEN 1 THEN 'Sí' WHEN 2 THEN 'No' ELSE NULL END) AS 'FamilyHistory',
        (CASE D.FamilyHistory      WHEN 1 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END) AS 'EnableTab1.2.WhichFamily',
        RTRIM(D.WhichFamily) AS 'WhichFamily',
        (CASE D.SurgicalHistory    WHEN 1 THEN 'Sí' WHEN 2 THEN 'No' ELSE NULL END) AS 'SurgicalHistory',
        (CASE D.SurgicalHistory    WHEN 1 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END) AS 'EnableTab1.2.WhichSurgical',
        RTRIM(D.WhichSurgical) AS 'WhichSurgical',
        (CASE D.PharmacologicalHistory WHEN 1 THEN 'Sí' WHEN 2 THEN 'No' ELSE NULL END) AS 'PharmacologicalHistory',
        (CASE D.PharmacologicalHistory WHEN 1 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END) AS 'EnableTab1.2.WhichPharmacological',
        RTRIM(D.WhichPharmacological) AS 'WhichPharmacological',
        (CASE D.UseCorrelation    WHEN 1 THEN 'Sí' WHEN 2 THEN 'No' ELSE NULL END) AS 'UseCorrelation',
        (CASE D.UseCorrelation    WHEN 1 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END) AS 'EnableTab1.2.Correlation',
        (CASE D.CorrelationType   WHEN 1 THEN 'Lentes de contacto' WHEN 2 THEN 'Gafas' ELSE NULL END) AS 'CorrelationType',
        (CASE D.FormUseCorrelation WHEN 1 THEN 'Permanente' WHEN 2 THEN 'Ocupacional' WHEN 3 THEN 'Ocasional' ELSE NULL END) AS 'FormUseCorrelation',
        (CASE WHEN RTRIM(B.OphthalmologicalHistoryObservations) IS NULL THEN CAST(0 AS BIT)
              WHEN RTRIM(B.OphthalmologicalHistoryObservations) = ''    THEN CAST(0 AS BIT)
              ELSE CAST(1 AS BIT) END) AS 'EnableTab1.2.OphthalmologicalHistoryObservations',
        RTRIM(B.OphthalmologicalHistoryObservations) AS 'OphthalmologicalHistoryObservations'
    FROM [Glasses].[OptometryClinicalEvaluationC] AS A WITH(NOLOCK)
    INNER JOIN [Glasses].[OptometryClinicalEvaluationGeneralInformation] AS B WITH(NOLOCK)
        ON A.Id = B.idOptometryClinicalEvaluationC
    LEFT JOIN [Glasses].[OptometryAnamnesisHistoryDetail] AS D WITH(NOLOCK)
        ON A.Id = D.IdOptometryClinicalEvaluationC
    WHERE A.IPCODPACI = @CodigoPaciente
      AND A.NUMINGRES = @NumeroIngreso
      AND A.NUMEFOLIO = @NumeroFolio
    ORDER BY A.Id ASC;

    -- ─────────────────────────────────────────────────────────────────────────
    -- Result set 2: Tab2 Valoracion Externa
    -- ─────────────────────────────────────────────────────────────────────────
    SELECT A.Id,
        -- Tab 2.1: Examen externo P1
        (CASE E.HasAnophthalmia WHEN 1 THEN 'Sí' WHEN 2 THEN 'No' ELSE NULL END) AS 'HasAnophthalmia',
        (CASE E.HasAnophthalmia WHEN 1 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END) AS 'EnableTab2.1.AnophthalmiaLatObs',
        (CASE E.AffectedLateralityAnophthalmia WHEN 1 THEN 'Derecho' WHEN 2 THEN 'Izquierdo' WHEN 3 THEN 'Ambos' ELSE NULL END) AS 'AffectedLateralityAnophthalmia',
        RTRIM(E.AnophthalmiaObservations) AS 'AnophthalmiaObservations',
        (CASE E.HasBlindness WHEN 1 THEN 'Sí' WHEN 2 THEN 'No' ELSE NULL END) AS 'HasBlindness',
        (CASE WHEN E.HasAnophthalmia = 1 AND E.AffectedLateralityAnophthalmia = 3 THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END) AS 'EnableTab2.1.BlindnessSB',
        (CASE E.AffectedLateralityBlindness WHEN 1 THEN 'Derecho' WHEN 2 THEN 'Izquierdo' WHEN 3 THEN 'Ambos' ELSE NULL END) AS 'AffectedLateralityBlindness',
        (CASE E.HasAnophthalmia
            WHEN 2 THEN CASE E.HasBlindness WHEN 1 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END
            ELSE CAST(0 AS BIT) END) AS 'EnableTab2.1.BlindnessLaterality',
        RTRIM(E.BlindnessObservations) AS 'BlindnessObservations',
        (CASE E.HasAnophthalmia
            WHEN 1 THEN CASE WHEN E.AffectedLateralityAnophthalmia BETWEEN 1 AND 2
                             THEN CASE WHEN E.HasBlindness = 1 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END
                             ELSE CAST(0 AS BIT) END
            WHEN 2 THEN CASE WHEN E.HasBlindness = 1 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END
            ELSE CAST(0 AS BIT) END) AS 'EnableTab2.1.BlindnessObsSB',
        (CASE E.DominantEye WHEN 1 THEN 'Derecho' WHEN 2 THEN 'Izquierdo' ELSE NULL END) AS 'DominantEye',
        (CASE E.HasHeterochromia WHEN 1 THEN 'Sí' WHEN 2 THEN 'No' ELSE NULL END) AS 'HasHeterochromia',
        (CASE E.HasAnophthalmia WHEN 1 THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END) AS 'EnableTab2.1.HeterochromiaSB',
        RTRIM(E.HeterochromiaObservations) AS 'HeterochromiaObservations',
        (CASE E.HasAnophthalmia
            WHEN 1 THEN CAST(0 AS BIT)
            WHEN 2 THEN CASE E.HasHeterochromia WHEN 1 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END
            ELSE CAST(0 AS BIT) END) AS 'EnableTab2.1.HeterochromiaObsSB',
        (CASE E.HasMotility WHEN 1 THEN 'Normal' WHEN 2 THEN 'Anormal' ELSE NULL END) AS 'HasMotility',
        (CASE WHEN E.HasAnophthalmia = 1 AND E.AffectedLateralityAnophthalmia = 3 THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END) AS 'EnableTab2.1.MotilitySB',
        (CASE E.AffectedLateralityMotility WHEN 1 THEN 'Derecho' WHEN 2 THEN 'Izquierdo' WHEN 3 THEN 'Ambos' ELSE NULL END) AS 'AffectedLateralityMotility',
        (CASE E.HasAnophthalmia
            WHEN 2 THEN CASE E.HasMotility WHEN 2 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END
            ELSE CAST(0 AS BIT) END) AS 'EnableTab2.1.MotilityLaterality',
        RTRIM(E.MotilityObservations) AS 'MotilityObservations',
        (CASE E.HasAnophthalmia
            WHEN 1 THEN CASE WHEN E.AffectedLateralityAnophthalmia BETWEEN 1 AND 2
                             THEN CASE WHEN E.HasMotility = 2 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END
                             ELSE CAST(0 AS BIT) END
            WHEN 2 THEN CASE WHEN E.HasMotility = 2 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END
            ELSE CAST(0 AS BIT) END) AS 'EnableTab2.1.MotilityObsSB',

        -- Tab 2.1: Cover Test
        (CASE E.HasCoverTest WHEN 1 THEN 'Sí' WHEN 2 THEN 'No' ELSE NULL END) AS 'HasCoverTest',
        (CASE WHEN E.HasAnophthalmia = 1 AND E.AffectedLateralityAnophthalmia = 3 THEN CAST(0 AS BIT)
              ELSE CAST(1 AS BIT) END) AS 'EnableTab2.1.CoverTestSB',
        (CASE E.CoverTestResult WHEN 1 THEN 'Ortofórico' WHEN 2 THEN 'Anormal' ELSE NULL END) AS 'CoverTestResult',
        (CASE WHEN E.HasCoverTest = 1 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END) AS 'EnableTab2.1.CoverTestResult',
        (CASE E.CoverTestTypeOfAbnormality WHEN 1 THEN 'Forias' WHEN 2 THEN 'Tropías' WHEN 3 THEN 'Ambos' ELSE NULL END) AS 'CoverTestTypeOfAbnormality',
        (CASE WHEN E.CoverTestResult = 2 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END) AS 'EnableTab2.1.CoverTestAbnormality',
        (CASE WHEN (E.HasAnophthalmia = 1 AND E.AffectedLateralityAnophthalmia = 3) THEN CAST(0 AS BIT)
              WHEN EXISTS(SELECT TOP 1 Id FROM [Glasses].[OptometryExternalReviewDetailCoverTest] AS F WITH(NOLOCK)
                          WHERE CoverTestTableNumber = 1 AND F.IdOptometryClinicalEvaluationC = A.Id) THEN CAST(1 AS BIT)
              ELSE CAST(0 AS BIT) END) AS 'EnableTab2.1.CoverTestVisionLCSB',
        (CASE WHEN (E.HasAnophthalmia = 1 AND E.AffectedLateralityAnophthalmia = 3) THEN CAST(0 AS BIT)
              WHEN EXISTS(SELECT TOP 1 Id FROM [Glasses].[OptometryExternalReviewDetailCoverTest] AS F WITH(NOLOCK)
                          WHERE CoverTestTableNumber = 1 AND F.IdOptometryClinicalEvaluationC = A.Id)
               AND EXISTS(SELECT TOP 1 Id FROM [Glasses].[OptometryExternalReviewDetailCoverTest] AS F WITH(NOLOCK)
                          WHERE CoverTestTableNumber = 2 AND F.IdOptometryClinicalEvaluationC = A.Id) THEN CAST(1 AS BIT)
              ELSE CAST(0 AS BIT) END) AS 'EnableTab2.1.CoverTestVisionCSB',
        (CASE WHEN (E.HasAnophthalmia = 1 AND E.AffectedLateralityAnophthalmia = 3) THEN CAST(0 AS BIT)
              WHEN EXISTS(SELECT TOP 1 Id FROM [Glasses].[OptometryExternalReviewDetailCoverTest] AS F WITH(NOLOCK)
                          WHERE CoverTestTableNumber = 2 AND F.IdOptometryClinicalEvaluationC = A.Id) THEN CAST(1 AS BIT)
              ELSE CAST(0 AS BIT) END) AS 'EnableTab2.1.CoverTestVisionC2',

        -- Tab 2.1: Examen externo P2
        E.MaximumPointConvergence,
        E.PupillaryDistance,
        (CASE E.HasAnophthalmia WHEN 1 THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END) AS 'EnableTab2.1.ConvergenceSB',
        (CASE E.HasStereopsis WHEN 1 THEN 'Sí' WHEN 2 THEN 'No' ELSE NULL END) AS 'HasStereopsis',
        (CASE WHEN E.HasAnophthalmia = 1 AND E.AffectedLateralityAnophthalmia = 3 THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END) AS 'EnableTab2.1.SteriopsisSB',
        (CASE E.TechniqueUsedInStereopsis WHEN 1 THEN 'Titmus' WHEN 2 THEN 'Random' WHEN 3 THEN 'Frisby' WHEN 4 THEN 'Lang' ELSE NULL END) AS 'TechniqueUsedInStereopsis',
        (CASE E.StereopsisArcSeconds
            WHEN 1 THEN '800 seconds' WHEN 2 THEN '400 seconds' WHEN 3 THEN '200 seconds'
            WHEN 4 THEN '140 seconds' WHEN 5 THEN '100 seconds' WHEN 6 THEN '80 seconds'
            WHEN 7 THEN '60 seconds'  WHEN 8 THEN '50 seconds'  WHEN 9 THEN '40 seconds'
            ELSE NULL END) AS 'StereopsisArcSeconds',
        (CASE E.HasStereopsis WHEN 1 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END) AS 'EnableTab2.1.SteriopsisTechniqueArcSec',
        RTRIM(B.ExternalReviewObservations) AS 'ExternalReviewObservations',
        RTRIM(E.ExternalExamination)        AS 'ExternalExamination',
        RTRIM(E.RightEye)                   AS 'RightEye',
        RTRIM(E.LeftEye)                    AS 'LeftEye',
        RTRIM(E.ProximityPointConvergence)  AS 'ProximityPointConvergence',
        RTRIM(E.CoverTestObservations)      AS 'CoverTestObservations'
    FROM [Glasses].[OptometryClinicalEvaluationC] AS A WITH(NOLOCK)
    INNER JOIN [Glasses].[OptometryClinicalEvaluationGeneralInformation] AS B WITH(NOLOCK)
        ON A.Id = B.idOptometryClinicalEvaluationC
    INNER JOIN [Glasses].[OptometryExternalReviewDetail] AS E WITH(NOLOCK)
        ON A.Id = E.IdOptometryClinicalEvaluationC
    WHERE A.IPCODPACI = @CodigoPaciente
      AND A.NUMINGRES = @NumeroIngreso
      AND A.NUMEFOLIO = @NumeroFolio
    ORDER BY A.Id ASC;

    -- ─────────────────────────────────────────────────────────────────────────
    -- Result set 3: Tab2 Table1And2 Cover Test Cuadro
    -- ─────────────────────────────────────────────────────────────────────────
    SELECT DISTINCT A.Id,
        -- Tabla 1 Cover Test
        (SELECT DISTINCT RTRIM(OERDCT.CoverTestDescription) FROM [Glasses].[OptometryExternalReviewDetailCoverTest] AS OERDCT WITH(NOLOCK) WHERE OERDCT.CoverTestTableNumber=1 AND OERDCT.CoverTestRow=0 AND OERDCT.CoverTestColumn=0 AND A.Id=OERDCT.IdOptometryClinicalEvaluationC) AS 'Tab21Table1Row0Col0',
        (SELECT DISTINCT RTRIM(OERDCT.CoverTestDescription) FROM [Glasses].[OptometryExternalReviewDetailCoverTest] AS OERDCT WITH(NOLOCK) WHERE OERDCT.CoverTestTableNumber=1 AND OERDCT.CoverTestRow=0 AND OERDCT.CoverTestColumn=1 AND A.Id=OERDCT.IdOptometryClinicalEvaluationC) AS 'Tab21Table1Row0Col1',
        (SELECT DISTINCT RTRIM(OERDCT.CoverTestDescription) FROM [Glasses].[OptometryExternalReviewDetailCoverTest] AS OERDCT WITH(NOLOCK) WHERE OERDCT.CoverTestTableNumber=1 AND OERDCT.CoverTestRow=0 AND OERDCT.CoverTestColumn=2 AND A.Id=OERDCT.IdOptometryClinicalEvaluationC) AS 'Tab21Table1Row0Col2',
        (SELECT DISTINCT RTRIM(OERDCT.CoverTestDescription) FROM [Glasses].[OptometryExternalReviewDetailCoverTest] AS OERDCT WITH(NOLOCK) WHERE OERDCT.CoverTestTableNumber=1 AND OERDCT.CoverTestRow=1 AND OERDCT.CoverTestColumn=0 AND A.Id=OERDCT.IdOptometryClinicalEvaluationC) AS 'Tab21Table1Row1Col0',
        (SELECT DISTINCT RTRIM(OERDCT.CoverTestDescription) FROM [Glasses].[OptometryExternalReviewDetailCoverTest] AS OERDCT WITH(NOLOCK) WHERE OERDCT.CoverTestTableNumber=1 AND OERDCT.CoverTestRow=1 AND OERDCT.CoverTestColumn=1 AND A.Id=OERDCT.IdOptometryClinicalEvaluationC) AS 'Tab21Table1Row1Col1',
        (SELECT DISTINCT RTRIM(OERDCT.CoverTestDescription) FROM [Glasses].[OptometryExternalReviewDetailCoverTest] AS OERDCT WITH(NOLOCK) WHERE OERDCT.CoverTestTableNumber=1 AND OERDCT.CoverTestRow=1 AND OERDCT.CoverTestColumn=2 AND A.Id=OERDCT.IdOptometryClinicalEvaluationC) AS 'Tab21Table1Row1Col2',
        (SELECT DISTINCT RTRIM(OERDCT.CoverTestDescription) FROM [Glasses].[OptometryExternalReviewDetailCoverTest] AS OERDCT WITH(NOLOCK) WHERE OERDCT.CoverTestTableNumber=1 AND OERDCT.CoverTestRow=2 AND OERDCT.CoverTestColumn=0 AND A.Id=OERDCT.IdOptometryClinicalEvaluationC) AS 'Tab21Table1Row2Col0',
        (SELECT DISTINCT RTRIM(OERDCT.CoverTestDescription) FROM [Glasses].[OptometryExternalReviewDetailCoverTest] AS OERDCT WITH(NOLOCK) WHERE OERDCT.CoverTestTableNumber=1 AND OERDCT.CoverTestRow=2 AND OERDCT.CoverTestColumn=1 AND A.Id=OERDCT.IdOptometryClinicalEvaluationC) AS 'Tab21Table1Row2Col1',
        (SELECT DISTINCT RTRIM(OERDCT.CoverTestDescription) FROM [Glasses].[OptometryExternalReviewDetailCoverTest] AS OERDCT WITH(NOLOCK) WHERE OERDCT.CoverTestTableNumber=1 AND OERDCT.CoverTestRow=2 AND OERDCT.CoverTestColumn=2 AND A.Id=OERDCT.IdOptometryClinicalEvaluationC) AS 'Tab21Table1Row2Col2',
        -- Tabla 2 Cover Test
        (SELECT DISTINCT RTRIM(OERDCT.CoverTestDescription) FROM [Glasses].[OptometryExternalReviewDetailCoverTest] AS OERDCT WITH(NOLOCK) WHERE OERDCT.CoverTestTableNumber=2 AND OERDCT.CoverTestRow=0 AND OERDCT.CoverTestColumn=0 AND A.Id=OERDCT.IdOptometryClinicalEvaluationC) AS 'Tab21Table2Row0Col0',
        (SELECT DISTINCT RTRIM(OERDCT.CoverTestDescription) FROM [Glasses].[OptometryExternalReviewDetailCoverTest] AS OERDCT WITH(NOLOCK) WHERE OERDCT.CoverTestTableNumber=2 AND OERDCT.CoverTestRow=0 AND OERDCT.CoverTestColumn=1 AND A.Id=OERDCT.IdOptometryClinicalEvaluationC) AS 'Tab21Table2Row0Col1',
        (SELECT DISTINCT RTRIM(OERDCT.CoverTestDescription) FROM [Glasses].[OptometryExternalReviewDetailCoverTest] AS OERDCT WITH(NOLOCK) WHERE OERDCT.CoverTestTableNumber=2 AND OERDCT.CoverTestRow=0 AND OERDCT.CoverTestColumn=2 AND A.Id=OERDCT.IdOptometryClinicalEvaluationC) AS 'Tab21Table2Row0Col2',
        (SELECT DISTINCT RTRIM(OERDCT.CoverTestDescription) FROM [Glasses].[OptometryExternalReviewDetailCoverTest] AS OERDCT WITH(NOLOCK) WHERE OERDCT.CoverTestTableNumber=2 AND OERDCT.CoverTestRow=1 AND OERDCT.CoverTestColumn=0 AND A.Id=OERDCT.IdOptometryClinicalEvaluationC) AS 'Tab21Table2Row1Col0',
        (SELECT DISTINCT RTRIM(OERDCT.CoverTestDescription) FROM [Glasses].[OptometryExternalReviewDetailCoverTest] AS OERDCT WITH(NOLOCK) WHERE OERDCT.CoverTestTableNumber=2 AND OERDCT.CoverTestRow=1 AND OERDCT.CoverTestColumn=1 AND A.Id=OERDCT.IdOptometryClinicalEvaluationC) AS 'Tab21Table2Row1Col1',
        (SELECT DISTINCT RTRIM(OERDCT.CoverTestDescription) FROM [Glasses].[OptometryExternalReviewDetailCoverTest] AS OERDCT WITH(NOLOCK) WHERE OERDCT.CoverTestTableNumber=2 AND OERDCT.CoverTestRow=1 AND OERDCT.CoverTestColumn=2 AND A.Id=OERDCT.IdOptometryClinicalEvaluationC) AS 'Tab21Table2Row1Col2',
        (SELECT DISTINCT RTRIM(OERDCT.CoverTestDescription) FROM [Glasses].[OptometryExternalReviewDetailCoverTest] AS OERDCT WITH(NOLOCK) WHERE OERDCT.CoverTestTableNumber=2 AND OERDCT.CoverTestRow=2 AND OERDCT.CoverTestColumn=0 AND A.Id=OERDCT.IdOptometryClinicalEvaluationC) AS 'Tab21Table2Row2Col0',
        (SELECT DISTINCT RTRIM(OERDCT.CoverTestDescription) FROM [Glasses].[OptometryExternalReviewDetailCoverTest] AS OERDCT WITH(NOLOCK) WHERE OERDCT.CoverTestTableNumber=2 AND OERDCT.CoverTestRow=2 AND OERDCT.CoverTestColumn=1 AND A.Id=OERDCT.IdOptometryClinicalEvaluationC) AS 'Tab21Table2Row2Col1',
        (SELECT DISTINCT RTRIM(OERDCT.CoverTestDescription) FROM [Glasses].[OptometryExternalReviewDetailCoverTest] AS OERDCT WITH(NOLOCK) WHERE OERDCT.CoverTestTableNumber=2 AND OERDCT.CoverTestRow=2 AND OERDCT.CoverTestColumn=2 AND A.Id=OERDCT.IdOptometryClinicalEvaluationC) AS 'Tab21Table2Row2Col2'
    FROM [Glasses].[OptometryClinicalEvaluationC] AS A WITH(NOLOCK)
    INNER JOIN [Glasses].[OptometryExternalReviewDetailCoverTest] AS F WITH(NOLOCK)
        ON A.Id = F.idOptometryClinicalEvaluationC
    WHERE A.IPCODPACI = @CodigoPaciente
      AND A.NUMINGRES = @NumeroIngreso
      AND A.NUMEFOLIO = @NumeroFolio
    ORDER BY A.Id ASC;

    -- ─────────────────────────────────────────────────────────────────────────
    -- Result set 4: Tab3 Agudeza Visual
    -- ─────────────────────────────────────────────────────────────────────────
    SELECT A.Id,
        (CASE WHEN EXISTS(SELECT TOP 1 OVAD.Id FROM [Glasses].[OptometryVisualAcuityDetail] AS OVAD WHERE OVAD.idOptometryClinicalEvaluationC = A.Id)
              THEN 'Valorado'
              WHEN E.HasAnophthalmia = 1 AND E.AffectedLateralityAnophthalmia = 3 THEN 'No aplica'
              ELSE 'No valorado' END) AS 'VisualAcuityValorated',
        (CASE E.HasAnophthalmia
            WHEN 1 THEN CASE WHEN E.AffectedLateralityAnophthalmia BETWEEN 1 AND 2
                             THEN CASE WHEN EXISTS(SELECT TOP 1 OVAD.Id FROM [Glasses].[OptometryVisualAcuityDetail] AS OVAD WHERE OVAD.idOptometryClinicalEvaluationC = A.Id) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END
                             ELSE CAST(0 AS BIT) END
            WHEN 2 THEN CASE WHEN EXISTS(SELECT TOP 1 OVAD.Id FROM [Glasses].[OptometryVisualAcuityDetail] AS OVAD WHERE OVAD.idOptometryClinicalEvaluationC = A.Id) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END
            ELSE CAST(0 AS BIT) END) AS 'EnableTab3.1.SB_EnableTab3.2.CorrectionSB',
        RTRIM(B.VisualAcuityObservations) AS 'VisualAcuityObservations',
        (CASE B.VisualAcuityCorrection WHEN 1 THEN 'Si' WHEN 2 THEN 'No' WHEN 99 THEN 'No aplica' ELSE NULL END) AS 'VisualAcuityCorrection',
        (CASE E.HasAnophthalmia
            WHEN 1 THEN CASE WHEN E.AffectedLateralityAnophthalmia BETWEEN 1 AND 2
                             THEN CASE WHEN B.VisualAcuityCorrection = 1 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END
                             ELSE CAST(0 AS BIT) END
            WHEN 2 THEN CASE WHEN B.VisualAcuityCorrection = 1 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END
            ELSE CAST(0 AS BIT) END) AS 'EnableTab3.2.CorrectionValorationObsSB',
        RTRIM(B.VisualAcuityCorrectionObservations) AS 'VisualAcuityCorrectionObservations',
        RTRIM(B.Dominance) AS 'Dominance'
    FROM [Glasses].[OptometryClinicalEvaluationC] AS A WITH(NOLOCK)
    LEFT JOIN [Glasses].[OptometryClinicalEvaluationGeneralInformation] AS B WITH(NOLOCK)
        ON A.Id = B.IdOptometryClinicalEvaluationC
    LEFT JOIN [Glasses].[OptometryExternalReviewDetail] AS E WITH(NOLOCK)
        ON A.Id = E.IdOptometryClinicalEvaluationC
    WHERE A.IPCODPACI = @CodigoPaciente
      AND A.NUMINGRES = @NumeroIngreso
      AND A.NUMEFOLIO = @NumeroFolio
    ORDER BY A.Id ASC;

    -- ─────────────────────────────────────────────────────────────────────────
    -- Result set 5: Tab31 Table1 Visual Acuity
    -- ─────────────────────────────────────────────────────────────────────────
    -- Helper CTE for visual acuity code → label conversion
    ;WITH VALabel AS (
        SELECT v.Code, v.Label FROM (VALUES
            (1,'20/10'),(2,'20/15'),(3,'20/20'),(4,'20/25'),(5,'20/30'),
            (6,'20/40'),(7,'20/50'),(8,'20/60'),(9,'20/70'),(10,'20/80'),
            (11,'20/100'),(12,'20/150'),(13,'20/200'),(14,'20/400'),(15,'20/800'),
            (16,'Cuenta dedos a 5 m'),(17,'Cuenta dedos a 4 m'),(18,'Cuenta dedos a 3 m'),
            (19,'Cuenta dedos a 2 m'),(20,'Cuenta dedos a 1 m'),(21,'Cuenta dedos a 50 cm'),
            (22,'Movimiento de manos'),(23,'Percibe luz'),(24,'No percibe luz'),
            (25,'Centra - Sigue - Mantiene'),(26,'Centra - Sigue - No mantiene'),
            (27,'Centra - No sigue - No mantiene'),(28,'No centra - No sigue - No mantiene'),
            (29,'Rechaza luz'),(99,'')
        ) AS v(Code, Label)
    ),
    ProxLabel AS (
        SELECT p.Code, p.Label FROM (VALUES
            (1,'J1'),(2,'J2'),(3,'J3'),(4,'J4'),(5,'J5'),(6,'J6'),(7,'J7'),(8,'<J7'),
            (9,'0.5 M'),(10,'0.75'),(11,'1 M'),(12,'1.25 M'),(13,'1.5 M'),(14,'1.75 M'),
            (15,'2 M'),(16,'2.25 M'),(17,'2.5 M'),
            (18,'20/10'),(19,'20/15'),(20,'20/20'),(21,'20/25'),(22,'20/30'),(23,'20/40'),
            (24,'20/50'),(25,'20/60'),(26,'20/70'),(27,'20/80'),(28,'20/10'),
            (29,'20/200'),(30,'20/400'),(31,'20/800'),(99,'')
        ) AS p(Code, Label)
    )
    SELECT A.Id,
        ISNULL(VALDerFar.Label,      'No aplica') AS 'Tab31Table1LejanaSinCorreccionDer',
        ISNULL(VALDerFarC.Label,     'No aplica') AS 'Tab31Table1LejanaConCorreccionDer',
        ISNULL(VALDerPin.Label,      'No aplica') AS 'Tab31Table1EstenopeicaDer',
        ISNULL(ProxDerSin.Label,     'No aplica') AS 'Tab31Table1CercanaSinCorreccionDer',
        ISNULL(ProxDerCon.Label,     'No aplica') AS 'Tab31Table1CercanaConCorrecionDer',
        ISNULL(VALIzqFar.Label,      'No aplica') AS 'Tab31Table1LejanaSinCorreccionIzq',
        ISNULL(VALIzqFarC.Label,     'No aplica') AS 'Tab31Table1LejanaConCorreccionIzq',
        ISNULL(VALIzqPin.Label,      'No aplica') AS 'Tab31Table1EstenopeicaIzq',
        ISNULL(ProxIzqSin.Label,     'No aplica') AS 'Tab31Table1CercanaSinCorreccionIzq',
        ISNULL(ProxIzqCon.Label,     'No aplica') AS 'Tab31Table1CercanaConCorreccionIzq'
    FROM [Glasses].[OptometryClinicalEvaluationC] AS A WITH(NOLOCK)
    INNER JOIN [Glasses].[OptometryExternalReviewDetail] AS E WITH(NOLOCK)
        ON A.Id = E.IdOptometryClinicalEvaluationC
    LEFT JOIN [Glasses].[OptometryVisualAcuityDetail] AS OVADDer WITH(NOLOCK)
        ON A.Id = OVADDer.idOptometryClinicalEvaluationC AND OVADDer.Eye = 1
    LEFT JOIN [Glasses].[OptometryVisualAcuityDetail] AS OVADIzq WITH(NOLOCK)
        ON A.Id = OVADIzq.idOptometryClinicalEvaluationC AND OVADIzq.Eye = 2
    LEFT JOIN VALabel   AS VALDerFar  ON OVADDer.FarAwayWithoutCorrection = VALDerFar.Code
    LEFT JOIN VALabel   AS VALDerFarC ON OVADDer.FarAwayWithCorrection    = VALDerFarC.Code
    LEFT JOIN VALabel   AS VALDerPin  ON OVADDer.Pinhole                  = VALDerPin.Code
    LEFT JOIN ProxLabel AS ProxDerSin ON OVADDer.ProximalWithOutCorrection = ProxDerSin.Code
    LEFT JOIN ProxLabel AS ProxDerCon ON OVADDer.ProximalWithCorrection    = ProxDerCon.Code
    LEFT JOIN VALabel   AS VALIzqFar  ON OVADIzq.FarAwayWithoutCorrection = VALIzqFar.Code
    LEFT JOIN VALabel   AS VALIzqFarC ON OVADIzq.FarAwayWithCorrection    = VALIzqFarC.Code
    LEFT JOIN VALabel   AS VALIzqPin  ON OVADIzq.Pinhole                  = VALIzqPin.Code
    LEFT JOIN ProxLabel AS ProxIzqSin ON OVADIzq.ProximalWithOutCorrection = ProxIzqSin.Code
    LEFT JOIN ProxLabel AS ProxIzqCon ON OVADIzq.ProximalWithCorrection    = ProxIzqCon.Code
    WHERE A.IPCODPACI = @CodigoPaciente
      AND A.NUMINGRES = @NumeroIngreso
      AND A.NUMEFOLIO = @NumeroFolio
    ORDER BY A.Id ASC;

    -- ─────────────────────────────────────────────────────────────────────────
    -- Result set 6: Tab32 Table1 Visual Acuity Correction
    -- ─────────────────────────────────────────────────────────────────────────
    ;WITH VALabel2 AS (
        SELECT v.Code, v.Label FROM (VALUES
            (1,'20/10'),(2,'20/15'),(3,'20/20'),(4,'20/25'),(5,'20/30'),
            (6,'20/40'),(7,'20/50'),(8,'20/60'),(9,'20/70'),(10,'20/80'),
            (11,'20/100'),(12,'20/150'),(13,'20/200'),(14,'20/400'),(15,'20/800'),
            (16,'Cuenta dedos a 5 m'),(17,'Cuenta dedos a 4 m'),(18,'Cuenta dedos a 3 m'),
            (19,'Cuenta dedos a 2 m'),(20,'Cuenta dedos a 1 m'),(21,'Cuenta dedos a 50 cm'),
            (22,'Movimiento de manos'),(23,'Percibe luz'),(24,'No percibe luz'),
            (25,'Centra - Sigue - Mantiene'),(26,'Centra - Sigue - No mantiene'),
            (27,'Centra - No sigue - No mantiene'),(28,'No centra - No sigue - No mantiene'),
            (29,'Rechaza luz'),(99,'')
        ) AS v(Code, Label)
    ),
    ProxLabel2 AS (
        SELECT p.Code, p.Label FROM (VALUES
            (1,'J1'),(2,'J2'),(3,'J3'),(4,'J4'),(5,'J5'),(6,'J6'),(7,'J7'),(8,'<J7'),
            (9,'0.5 M'),(10,'0.75'),(11,'1 M'),(12,'1.25 M'),(13,'1.5 M'),(14,'1.75 M'),
            (15,'2 M'),(16,'2.25 M'),(17,'2.5 M'),
            (18,'20/10'),(19,'20/15'),(20,'20/20'),(21,'20/25'),(22,'20/30'),(23,'20/40'),
            (24,'20/50'),(25,'20/60'),(26,'20/70'),(27,'20/80'),(28,'20/10'),
            (29,'20/200'),(30,'20/400'),(31,'20/800'),(99,'')
        ) AS p(Code, Label)
    )
    SELECT A.Id,
        IIF(RTRIM(OVACDDer.Sphere)   <> 'NoRegistra', RTRIM(OVACDDer.Sphere),   '') AS 'Tab32Table1EsferaDer',
        IIF(RTRIM(OVACDDer.Cylinder) <> 'NoRegistra', RTRIM(OVACDDer.Cylinder), '') AS 'Tab32Table1CilindroDer',
        IIF(RTRIM(OVACDDer.Axis)     <> 'NoRegistra', RTRIM(OVACDDer.Axis),     '') AS 'Tab32Table1EjeDer',
        ISNULL(VALDerAg.Label, 'No aplica')                                          AS 'Tab32Table1AgudezaDer',
        IIF(RTRIM(OVACDDer.Adition)  <> 'NoRegistra', RTRIM(OVACDDer.Adition),  '') AS 'Tab32Table1AdicionDer',
        IIF(RTRIM(OVACDDer.Prism)    <> 'NoRegistra', RTRIM(OVACDDer.Prism),    '') AS 'Tab32Table1PrismasDer',
        ISNULL(ProxDerCerca.Label, 'No aplica')                                      AS 'Tab32Table1CercanaDer',
        IIF(RTRIM(OVACDIzq.Sphere)   <> 'NoRegistra', RTRIM(OVACDIzq.Sphere),   '') AS 'Tab32Table1EsferaIzq',
        IIF(RTRIM(OVACDIzq.Cylinder) <> 'NoRegistra', RTRIM(OVACDIzq.Cylinder), '') AS 'Tab32Table1CilindroIzq',
        IIF(RTRIM(OVACDIzq.Axis)     <> 'NoRegistra', RTRIM(OVACDIzq.Axis),     '') AS 'Tab32Table1EjeIzq',
        ISNULL(VALIzqAg.Label, 'No aplica')                                          AS 'Tab32Table1AgudezaIzq',
        IIF(RTRIM(OVACDIzq.Adition)  <> 'NoRegistra', RTRIM(OVACDIzq.Adition),  '') AS 'Tab32Table1AdicionIzq',
        IIF(RTRIM(OVACDIzq.Prism)    <> 'NoRegistra', RTRIM(OVACDIzq.Prism),    '') AS 'Tab32Table1PrismasIzq',
        ISNULL(ProxIzqCerca.Label, 'No aplica')                                      AS 'Tab32Table1CercanaIzq'
    FROM [Glasses].[OptometryClinicalEvaluationC] AS A WITH(NOLOCK)
    LEFT JOIN [Glasses].[OptometryVisualAcuityCorrectionDetail] AS OVACDDer WITH(NOLOCK)
        ON A.Id = OVACDDer.idOptometryClinicalEvaluationC AND OVACDDer.Eye = 1
    LEFT JOIN [Glasses].[OptometryVisualAcuityCorrectionDetail] AS OVACDIzq WITH(NOLOCK)
        ON A.Id = OVACDIzq.idOptometryClinicalEvaluationC AND OVACDIzq.Eye = 2
    LEFT JOIN VALabel2   AS VALDerAg    ON OVACDDer.VisualAcuity   = VALDerAg.Code
    LEFT JOIN VALabel2   AS VALIzqAg    ON OVACDIzq.VisualAcuity   = VALIzqAg.Code
    LEFT JOIN ProxLabel2 AS ProxDerCerca ON OVACDDer.ProximalVision = ProxDerCerca.Code
    LEFT JOIN ProxLabel2 AS ProxIzqCerca ON OVACDIzq.ProximalVision = ProxIzqCerca.Code
    WHERE A.IPCODPACI = @CodigoPaciente
      AND A.NUMINGRES = @NumeroIngreso
      AND A.NUMEFOLIO = @NumeroFolio
    ORDER BY A.Id ASC;

    -- ─────────────────────────────────────────────────────────────────────────
    -- Result set 7: Tab4 Refraccion
    -- ─────────────────────────────────────────────────────────────────────────
    SELECT A.Id,
        (CASE WHEN EXISTS(SELECT TOP 1 OROD.Id FROM [Glasses].[OptometryRefractionObjectiveDetail] AS OROD WHERE OROD.idOptometryClinicalEvaluationC = A.Id AND OROD.TypeRefraction = 1) THEN 'Valorado' WHEN E.HasAnophthalmia=1 AND E.AffectedLateralityAnophthalmia=3 THEN 'No aplica' ELSE 'No valorado' END) AS 'RefractionObjectiveWithValorated',
        (CASE WHEN EXISTS(SELECT TOP 1 OROD.Id FROM [Glasses].[OptometryRefractionObjectiveDetail] AS OROD WHERE OROD.idOptometryClinicalEvaluationC = A.Id AND OROD.TypeRefraction = 2) THEN 'Valorado' WHEN E.HasAnophthalmia=1 AND E.AffectedLateralityAnophthalmia=3 THEN 'No aplica' ELSE 'No valorado' END) AS 'RefractionObjectiveWithoutValorated',
        (CASE E.HasAnophthalmia
            WHEN 1 THEN CASE WHEN E.AffectedLateralityAnophthalmia BETWEEN 1 AND 2 THEN CASE WHEN EXISTS(SELECT TOP 1 OROD.Id FROM [Glasses].[OptometryRefractionObjectiveDetail] AS OROD WHERE OROD.idOptometryClinicalEvaluationC=A.Id AND OROD.TypeRefraction=1) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END ELSE CAST(0 AS BIT) END
            WHEN 2 THEN CASE WHEN EXISTS(SELECT TOP 1 OROD.Id FROM [Glasses].[OptometryRefractionObjectiveDetail] AS OROD WHERE OROD.idOptometryClinicalEvaluationC=A.Id AND OROD.TypeRefraction=1) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END
            ELSE CAST(0 AS BIT) END) AS 'EnableTab4.1.ObjetiveRefractionWithSB',
        (CASE E.HasAnophthalmia
            WHEN 1 THEN CASE WHEN E.AffectedLateralityAnophthalmia BETWEEN 1 AND 2 THEN CASE WHEN EXISTS(SELECT TOP 1 OROD.Id FROM [Glasses].[OptometryRefractionObjectiveDetail] AS OROD WHERE OROD.idOptometryClinicalEvaluationC=A.Id AND OROD.TypeRefraction=2) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END ELSE CAST(0 AS BIT) END
            WHEN 2 THEN CASE WHEN EXISTS(SELECT TOP 1 OROD.Id FROM [Glasses].[OptometryRefractionObjectiveDetail] AS OROD WHERE OROD.idOptometryClinicalEvaluationC=A.Id AND OROD.TypeRefraction=2) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END
            ELSE CAST(0 AS BIT) END) AS 'EnableTab4.1.ObjetiveRefractionWithoutSB',
        RTRIM(B.ObjectiveRefractionObservations) AS 'ObjectiveRefractionObservations',
        (CASE WHEN (EXISTS(SELECT TOP 1 ORSWTCD.Id FROM [Glasses].[OptometryRefractionSubjectiveWithCyclopejiaDetail] AS ORSWTCD WHERE ORSWTCD.idOptometryClinicalEvaluationC=A.Id) AND EXISTS(SELECT TOP 1 ORSWCD.Id FROM [Glasses].[OptometryRefractionSubjectiveWithoutCyclopejiaDetail] AS ORSWCD WHERE ORSWCD.idOptometryClinicalEvaluationC=A.Id)) THEN 'Valorado' WHEN E.HasAnophthalmia=1 AND E.AffectedLateralityAnophthalmia=3 THEN 'No aplica' ELSE 'No valorado' END) AS 'RefractionSubjectiveWithValorated',
        (CASE WHEN (EXISTS(SELECT TOP 1 ORSWTCD.Id FROM [Glasses].[OptometryRefractionSubjectiveWithoutCyclopejiaDetail] AS ORSWTCD WHERE ORSWTCD.idOptometryClinicalEvaluationC=A.Id) AND EXISTS(SELECT TOP 1 ORSWCD.Id FROM [Glasses].[OptometryRefractionSubjectiveWithoutCyclopejiaDetail] AS ORSWCD WHERE ORSWCD.idOptometryClinicalEvaluationC=A.Id)) THEN 'Valorado' WHEN E.HasAnophthalmia=1 AND E.AffectedLateralityAnophthalmia=3 THEN 'No aplica' ELSE 'No valorado' END) AS 'RefractionSubjectiveWithoutValorated',
        (CASE E.HasAnophthalmia
            WHEN 1 THEN CASE WHEN E.AffectedLateralityAnophthalmia BETWEEN 1 AND 2 THEN CASE WHEN EXISTS(SELECT TOP 1 ORSWTCD.Id FROM [Glasses].[OptometryRefractionSubjectiveWithoutCyclopejiaDetail] AS ORSWTCD WHERE ORSWTCD.idOptometryClinicalEvaluationC=A.Id) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END ELSE CAST(0 AS BIT) END
            WHEN 2 THEN CASE WHEN EXISTS(SELECT TOP 1 ORSWTCD.Id FROM [Glasses].[OptometryRefractionSubjectiveWithoutCyclopejiaDetail] AS ORSWTCD WHERE ORSWTCD.idOptometryClinicalEvaluationC=A.Id) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END
            ELSE CAST(0 AS BIT) END) AS 'EnableTab4.2.SubjetiveRefractionWithoutSB',
        (CASE E.HasAnophthalmia
            WHEN 1 THEN CASE WHEN E.AffectedLateralityAnophthalmia BETWEEN 1 AND 2 THEN CASE WHEN EXISTS(SELECT TOP 1 ORSWCD.Id FROM [Glasses].[OptometryRefractionSubjectiveWithCyclopejiaDetail] AS ORSWCD WHERE ORSWCD.idOptometryClinicalEvaluationC=A.Id) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END ELSE CAST(0 AS BIT) END
            WHEN 2 THEN CASE WHEN EXISTS(SELECT TOP 1 ORSWCD.Id FROM [Glasses].[OptometryRefractionSubjectiveWithCyclopejiaDetail] AS ORSWCD WHERE ORSWCD.idOptometryClinicalEvaluationC=A.Id) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END
            ELSE CAST(0 AS BIT) END) AS 'EnableTab4.2.SubjetiveRefractionWithSB',
        RTRIM(B.SubjectiveRefractionObservations)  AS 'SubjectiveRefractionObservations',
        RTRIM(B.PupillaryDistance)                 AS 'PupillaryDistance',
        (CASE WHEN EXISTS(SELECT TOP 1 ORKD.Id FROM [Glasses].[OptometryRefractionKeratometryDetail] AS ORKD WHERE ORKD.idOptometryClinicalEvaluationC=A.Id) THEN 'Valorado' WHEN E.HasAnophthalmia=1 AND E.AffectedLateralityAnophthalmia=3 THEN 'No aplica' ELSE 'No valorado' END) AS 'RefractionKeratometryValorated',
        (CASE E.HasAnophthalmia
            WHEN 1 THEN CASE WHEN E.AffectedLateralityAnophthalmia BETWEEN 1 AND 2 THEN CASE WHEN EXISTS(SELECT TOP 1 ORKD.Id FROM [Glasses].[OptometryRefractionKeratometryDetail] AS ORKD WHERE ORKD.idOptometryClinicalEvaluationC=A.Id) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END ELSE CAST(0 AS BIT) END
            WHEN 2 THEN CASE WHEN EXISTS(SELECT TOP 1 ORKD.Id FROM [Glasses].[OptometryRefractionKeratometryDetail] AS ORKD WHERE ORKD.idOptometryClinicalEvaluationC=A.Id) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END
            ELSE CAST(0 AS BIT) END) AS 'EnableTab4.3.KeratometrySB',
        RTRIM(B.KeratometryRefractionObservations) AS 'KeratometryRefractionObservations',
        (CASE WHEN EXISTS(SELECT TOP 1 ORAD.Id FROM [Glasses].[OptometryRefractionAutokeratometryDetail] AS ORAD WHERE ORAD.idOptometryClinicalEvaluationC=A.Id) THEN 'Valorado' WHEN E.HasAnophthalmia=1 AND E.AffectedLateralityAnophthalmia=3 THEN 'No aplica' ELSE 'No valorado' END) AS 'RefractionAutokeratometryValorated',
        (CASE E.HasAnophthalmia
            WHEN 1 THEN CASE WHEN E.AffectedLateralityAnophthalmia BETWEEN 1 AND 2 THEN CASE WHEN EXISTS(SELECT TOP 1 ORAD.Id FROM [Glasses].[OptometryRefractionAutokeratometryDetail] AS ORAD WHERE ORAD.idOptometryClinicalEvaluationC=A.Id) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END ELSE CAST(0 AS BIT) END
            WHEN 2 THEN CASE WHEN EXISTS(SELECT TOP 1 ORAD.Id FROM [Glasses].[OptometryRefractionAutokeratometryDetail] AS ORAD WHERE ORAD.idOptometryClinicalEvaluationC=A.Id) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END
            ELSE CAST(0 AS BIT) END) AS 'EnableTab4.4.AutokeratometrySB',
        RTRIM(B.AutokeratometryRefractionObservations) AS 'AutokeratometryRefractionObservations'
    FROM [Glasses].[OptometryClinicalEvaluationC] AS A WITH(NOLOCK)
    LEFT JOIN [Glasses].[OptometryClinicalEvaluationGeneralInformation] AS B WITH(NOLOCK)
        ON A.Id = B.IdOptometryClinicalEvaluationC
    LEFT JOIN [Glasses].[OptometryExternalReviewDetail] AS E WITH(NOLOCK)
        ON A.Id = E.IdOptometryClinicalEvaluationC
    WHERE A.IPCODPACI = @CodigoPaciente
      AND A.NUMINGRES = @NumeroIngreso
      AND A.NUMEFOLIO = @NumeroFolio
    ORDER BY A.Id ASC;

    -- ─────────────────────────────────────────────────────────────────────────
    -- Result set 8: Tab41 Table1 Objective Refraction Without (sin ciclopejia)
    -- ─────────────────────────────────────────────────────────────────────────
    SELECT A.Id,
        IIF(RTRIM(ORODDerSin.Sphere)   <> 'NoRegistra', RTRIM(ORODDerSin.Sphere),   '') AS 'Tab41Table1EsferaDerSin',
        IIF(RTRIM(ORODDerSin.Cylinder) <> 'NoRegistra', RTRIM(ORODDerSin.Cylinder), '') AS 'Tab41Table1CilindroDerSin',
        IIF(RTRIM(ORODDerSin.Axis)     <> 'NoRegistra', RTRIM(ORODDerSin.Axis),     '') AS 'Tab41Table1EjeDerSin',
        IIF(RTRIM(ORODIzqSin.Sphere)   <> 'NoRegistra', RTRIM(ORODIzqSin.Sphere),   '') AS 'Tab41Table1EsferaIzqSin',
        IIF(RTRIM(ORODIzqSin.Cylinder) <> 'NoRegistra', RTRIM(ORODIzqSin.Cylinder), '') AS 'Tab41Table1CilindroIzqSin',
        IIF(RTRIM(ORODIzqSin.Axis)     <> 'NoRegistra', RTRIM(ORODIzqSin.Axis),     '') AS 'Tab41Table1EjeIzqSin'
    FROM [Glasses].[OptometryClinicalEvaluationC] AS A WITH(NOLOCK)
    LEFT JOIN [Glasses].[OptometryRefractionObjectiveDetail] AS ORODDerSin WITH(NOLOCK)
        ON A.Id = ORODDerSin.IdOptometryClinicalEvaluationC AND ORODDerSin.Eye = 1 AND ORODDerSin.TypeRefraction = 2
    LEFT JOIN [Glasses].[OptometryRefractionObjectiveDetail] AS ORODIzqSin WITH(NOLOCK)
        ON A.Id = ORODIzqSin.IdOptometryClinicalEvaluationC AND ORODIzqSin.Eye = 2 AND ORODIzqSin.TypeRefraction = 2
    WHERE A.IPCODPACI = @CodigoPaciente
      AND A.NUMINGRES = @NumeroIngreso
      AND A.NUMEFOLIO = @NumeroFolio
    ORDER BY A.Id ASC;

    -- ─────────────────────────────────────────────────────────────────────────
    -- Result set 9: Tab41 Table1 Objective Refraction With (con ciclopejia)
    -- ─────────────────────────────────────────────────────────────────────────
    SELECT A.Id,
        IIF(RTRIM(ORODDerCon.Sphere)   <> 'NoRegistra', RTRIM(ORODDerCon.Sphere),   '') AS 'Tab41Table1EsferaDerCon',
        IIF(RTRIM(ORODDerCon.Cylinder) <> 'NoRegistra', RTRIM(ORODDerCon.Cylinder), '') AS 'Tab41Table1CilindroDerCon',
        IIF(RTRIM(ORODDerCon.Axis)     <> 'NoRegistra', RTRIM(ORODDerCon.Axis),     '') AS 'Tab41Table1EjeDerCon',
        IIF(RTRIM(ORODIzqCon.Sphere)   <> 'NoRegistra', RTRIM(ORODIzqCon.Sphere),   '') AS 'Tab41Table1EsferaIzqCon',
        IIF(RTRIM(ORODIzqCon.Cylinder) <> 'NoRegistra', RTRIM(ORODIzqCon.Cylinder), '') AS 'Tab41Table1CilindroIzqCon',
        IIF(RTRIM(ORODIzqCon.Axis)     <> 'NoRegistra', RTRIM(ORODIzqCon.Axis),     '') AS 'Tab41Table1EjeIzqCon'
    FROM [Glasses].[OptometryClinicalEvaluationC] AS A WITH(NOLOCK)
    LEFT JOIN [Glasses].[OptometryRefractionObjectiveDetail] AS ORODDerCon WITH(NOLOCK)
        ON A.Id = ORODDerCon.IdOptometryClinicalEvaluationC AND ORODDerCon.Eye = 1 AND ORODDerCon.TypeRefraction = 1
    LEFT JOIN [Glasses].[OptometryRefractionObjectiveDetail] AS ORODIzqCon WITH(NOLOCK)
        ON A.Id = ORODIzqCon.IdOptometryClinicalEvaluationC AND ORODIzqCon.Eye = 2 AND ORODIzqCon.TypeRefraction = 1
    WHERE A.IPCODPACI = @CodigoPaciente
      AND A.NUMINGRES = @NumeroIngreso
      AND A.NUMEFOLIO = @NumeroFolio
    ORDER BY A.Id ASC;

    -- ─────────────────────────────────────────────────────────────────────────
    -- Result set 10: Tab42 Table1 Subjective Without Cyclopejia Refraction
    -- ─────────────────────────────────────────────────────────────────────────
    SELECT A.Id,
        IIF(RTRIM(ORSWTCDer.Sphere)   <> 'NoRegistra', RTRIM(ORSWTCDer.Sphere),   '') AS 'Tab42Table1EsferaDer',
        IIF(RTRIM(ORSWTCDer.Cylinder) <> 'NoRegistra', RTRIM(ORSWTCDer.Cylinder), '') AS 'Tab42Table1CilindroDer',
        IIF(RTRIM(ORSWTCDer.Axis)     <> 'NoRegistra', RTRIM(ORSWTCDer.Axis),     '') AS 'Tab42Table1EjeDer',
        IIF(RTRIM(ORSWTCDer.Adition)  <> 'NoRegistra', RTRIM(ORSWTCDer.Adition),  '') AS 'Tab42Table1AdicionDer',
        [dbo].[GetDescriptionVisualAcuity](ORSWTCDer.FarVisualAcuity)                  AS 'Tab42Table1AgudezaLejanaDer',
        [dbo].[GetDescriptionProximalVisualAcuity](ORSWTCDer.ProximalVisualAcuity)     AS 'Tab42Table1AgudezaCercanaDer',
        [dbo].[GetDescriptionPinhole](ORSWTCDer.Pinhole)                               AS 'Tab42Table1PinholeDer',
        IIF(RTRIM(ORSWTCIzq.Sphere)   <> 'NoRegistra', RTRIM(ORSWTCIzq.Sphere),   '') AS 'Tab42Table1EsferaIzq',
        IIF(RTRIM(ORSWTCIzq.Cylinder) <> 'NoRegistra', RTRIM(ORSWTCIzq.Cylinder), '') AS 'Tab42Table1CilindroIzq',
        IIF(RTRIM(ORSWTCIzq.Axis)     <> 'NoRegistra', RTRIM(ORSWTCIzq.Axis),     '') AS 'Tab42Table1EjeIzq',
        IIF(RTRIM(ORSWTCIzq.Adition)  <> 'NoRegistra', RTRIM(ORSWTCIzq.Adition),  '') AS 'Tab42Table1AdicionIzq',
        [dbo].[GetDescriptionVisualAcuity](ORSWTCIzq.FarVisualAcuity)                  AS 'Tab42Table1AgudezaLejanaIzq',
        [dbo].[GetDescriptionProximalVisualAcuity](ORSWTCIzq.ProximalVisualAcuity)     AS 'Tab42Table1AgudezaCercanaIzq',
        [dbo].[GetDescriptionPinhole](ORSWTCIzq.Pinhole)                               AS 'Tab42Table1PinholeIzq'
    FROM [Glasses].[OptometryClinicalEvaluationC] AS A WITH(NOLOCK)
    LEFT JOIN [Glasses].[OptometryRefractionSubjectiveWithoutCyclopejiaDetail] AS ORSWTCDer WITH(NOLOCK)
        ON A.Id = ORSWTCDer.IdOptometryClinicalEvaluationC AND ORSWTCDer.Eye = 1
    LEFT JOIN [Glasses].[OptometryRefractionSubjectiveWithoutCyclopejiaDetail] AS ORSWTCIzq WITH(NOLOCK)
        ON A.Id = ORSWTCIzq.IdOptometryClinicalEvaluationC AND ORSWTCIzq.Eye = 2
    WHERE A.IPCODPACI = @CodigoPaciente
      AND A.NUMINGRES = @NumeroIngreso
      AND A.NUMEFOLIO = @NumeroFolio
    ORDER BY A.Id ASC;

    -- ─────────────────────────────────────────────────────────────────────────
    -- Result set 11: Tab42 Table2 Subjective With Cyclopejia Refraction
    -- ─────────────────────────────────────────────────────────────────────────
    SELECT A.Id,
        IIF(RTRIM(ORSWCDer.Sphere)   <> 'NoRegistra', RTRIM(ORSWCDer.Sphere),   '') AS 'Tab42Table2EsferaDer',
        IIF(RTRIM(ORSWCDer.Cylinder) <> 'NoRegistra', RTRIM(ORSWCDer.Cylinder), '') AS 'Tab42Table2CilindroDer',
        IIF(RTRIM(ORSWCDer.Axis)     <> 'NoRegistra', RTRIM(ORSWCDer.Axis),     '') AS 'Tab42Table2EjeDer',
        IIF(RTRIM(ORSWCDer.Adition)  <> 'NoRegistra', RTRIM(ORSWCDer.Adition),  '') AS 'Tab42Table2AdicionDer',
        [dbo].[GetDescriptionVisualAcuity](ORSWCDer.VisualAcuity)                     AS 'Tab42Table2AgudezaLejanaDer',
        [dbo].[GetDescriptionPinhole](ORSWCDer.Pinhole)                               AS 'Tab42Table2PinholeDer',
        IIF(RTRIM(ORSWCIzq.Sphere)   <> 'NoRegistra', RTRIM(ORSWCIzq.Sphere),   '') AS 'Tab42Table2EsferaIzq',
        IIF(RTRIM(ORSWCIzq.Cylinder) <> 'NoRegistra', RTRIM(ORSWCIzq.Cylinder), '') AS 'Tab42Table2CilindroIzq',
        IIF(RTRIM(ORSWCIzq.Axis)     <> 'NoRegistra', RTRIM(ORSWCIzq.Axis),     '') AS 'Tab42Table2EjeIzq',
        IIF(RTRIM(ORSWCIzq.Adition)  <> 'NoRegistra', RTRIM(ORSWCIzq.Adition),  '') AS 'Tab42Table2AdicionIzq',
        [dbo].[GetDescriptionVisualAcuity](ORSWCIzq.VisualAcuity)                     AS 'Tab42Table2AgudezaLejanaIzq',
        [dbo].[GetDescriptionPinhole](ORSWCIzq.Pinhole)                               AS 'Tab42Table2PinholeIzq'
    FROM [Glasses].[OptometryClinicalEvaluationC] AS A WITH(NOLOCK)
    LEFT JOIN [Glasses].[OptometryRefractionSubjectiveWithCyclopejiaDetail] AS ORSWCDer WITH(NOLOCK)
        ON A.Id = ORSWCDer.IdOptometryClinicalEvaluationC AND ORSWCDer.Eye = 1
    LEFT JOIN [Glasses].[OptometryRefractionSubjectiveWithCyclopejiaDetail] AS ORSWCIzq WITH(NOLOCK)
        ON A.Id = ORSWCIzq.IdOptometryClinicalEvaluationC AND ORSWCIzq.Eye = 2
    WHERE A.IPCODPACI = @CodigoPaciente
      AND A.NUMINGRES = @NumeroIngreso
      AND A.NUMEFOLIO = @NumeroFolio
    ORDER BY A.Id ASC;

    -- ─────────────────────────────────────────────────────────────────────────
    -- Result set 12: Tab43 Table1 Keratometry Refraction
    -- ─────────────────────────────────────────────────────────────────────────
    SELECT A.Id,
        IIF(RTRIM(ORKDDer.KeratometryOne)   <> 'NoRegistra', RTRIM(ORKDDer.KeratometryOne),   '') AS 'Tab43Table1Kera1Der',
        IIF(RTRIM(ORKDDer.KeratometryTwo)   <> 'NoRegistra', RTRIM(ORKDDer.KeratometryTwo),   '') AS 'Tab43Table1Kera2Der',
        IIF(RTRIM(ORKDDer.KeratometryThree) <> 'NoRegistra', RTRIM(ORKDDer.KeratometryThree), '') AS 'Tab43Table1Kera3Der',
        IIF(RTRIM(ORKDIzq.KeratometryOne)   <> 'NoRegistra', RTRIM(ORKDIzq.KeratometryOne),   '') AS 'Tab43Table1Kera1Izq',
        IIF(RTRIM(ORKDIzq.KeratometryTwo)   <> 'NoRegistra', RTRIM(ORKDIzq.KeratometryTwo),   '') AS 'Tab43Table1Kera2Izq',
        IIF(RTRIM(ORKDIzq.KeratometryThree) <> 'NoRegistra', RTRIM(ORKDIzq.KeratometryThree), '') AS 'Tab43Table1Kera3Izq'
    FROM [Glasses].[OptometryClinicalEvaluationC] AS A WITH(NOLOCK)
    LEFT JOIN [Glasses].[OptometryRefractionKeratometryDetail] AS ORKDDer WITH(NOLOCK)
        ON A.Id = ORKDDer.IdOptometryClinicalEvaluationC AND ORKDDer.Eye = 1
    LEFT JOIN [Glasses].[OptometryRefractionKeratometryDetail] AS ORKDIzq WITH(NOLOCK)
        ON A.Id = ORKDIzq.IdOptometryClinicalEvaluationC AND ORKDIzq.Eye = 2
    WHERE A.IPCODPACI = @CodigoPaciente
      AND A.NUMINGRES = @NumeroIngreso
      AND A.NUMEFOLIO = @NumeroFolio
    ORDER BY A.Id ASC;

    -- ─────────────────────────────────────────────────────────────────────────
    -- Result set 13: Tab44 Table1 Autokeratometry Refraction
    -- ─────────────────────────────────────────────────────────────────────────
    SELECT A.Id,
        IIF(RTRIM(ORAKDDer.AutokeratometryOne)   <> 'NoRegistra', RTRIM(ORAKDDer.AutokeratometryOne),   '') AS 'Tab44Table1AutoKera1Der',
        IIF(RTRIM(ORAKDDer.AutokeratometryTwo)   <> 'NoRegistra', RTRIM(ORAKDDer.AutokeratometryTwo),   '') AS 'Tab44Table1AutoKera2Der',
        IIF(RTRIM(ORAKDDer.AutokeratometryThree) <> 'NoRegistra', RTRIM(ORAKDDer.AutokeratometryThree), '') AS 'Tab44Table1AutoKera3Der',
        IIF(RTRIM(ORAKDIzq.AutokeratometryOne)   <> 'NoRegistra', RTRIM(ORAKDIzq.AutokeratometryOne),   '') AS 'Tab44Table1AutoKera1Izq',
        IIF(RTRIM(ORAKDIzq.AutokeratometryTwo)   <> 'NoRegistra', RTRIM(ORAKDIzq.AutokeratometryTwo),   '') AS 'Tab44Table1AutoKera2Izq',
        IIF(RTRIM(ORAKDIzq.AutokeratometryThree) <> 'NoRegistra', RTRIM(ORAKDIzq.AutokeratometryThree), '') AS 'Tab44Table1AutoKera3Izq'
    FROM [Glasses].[OptometryClinicalEvaluationC] AS A WITH(NOLOCK)
    LEFT JOIN [Glasses].[OptometryRefractionAutokeratometryDetail] AS ORAKDDer WITH(NOLOCK)
        ON A.Id = ORAKDDer.IdOptometryClinicalEvaluationC AND ORAKDDer.Eye = 1
    LEFT JOIN [Glasses].[OptometryRefractionAutokeratometryDetail] AS ORAKDIzq WITH(NOLOCK)
        ON A.Id = ORAKDIzq.IdOptometryClinicalEvaluationC AND ORAKDIzq.Eye = 2
    WHERE A.IPCODPACI = @CodigoPaciente
      AND A.NUMINGRES = @NumeroIngreso
      AND A.NUMEFOLIO = @NumeroFolio
    ORDER BY A.Id ASC;

    -- ─────────────────────────────────────────────────────────────────────────
    -- Result set 14: Tab5 Valoracion Complementaria
    -- ─────────────────────────────────────────────────────────────────────────
    SELECT A.Id,
        (CASE WHEN EXISTS(SELECT TOP 1 OFEPE.Id FROM [Glasses].[OptometryFurtherEvaluationPupillaryExam] AS OFEPE WHERE OFEPE.IdOptometryClinicalEvaluationC=A.Id) THEN 'Valorado' WHEN E.HasAnophthalmia=1 AND E.AffectedLateralityAnophthalmia=3 THEN 'No aplica' ELSE 'No valorado' END) AS 'PupilarValorated',
        (CASE E.HasAnophthalmia WHEN 1 THEN CASE WHEN E.AffectedLateralityAnophthalmia BETWEEN 1 AND 2 THEN CASE WHEN EXISTS(SELECT TOP 1 OFEPE.Id FROM [Glasses].[OptometryFurtherEvaluationPupillaryExam] AS OFEPE WHERE OFEPE.IdOptometryClinicalEvaluationC=A.Id) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END ELSE CAST(0 AS BIT) END WHEN 2 THEN CASE WHEN EXISTS(SELECT TOP 1 OFEPE.Id FROM [Glasses].[OptometryFurtherEvaluationPupillaryExam] AS OFEPE WHERE OFEPE.IdOptometryClinicalEvaluationC=A.Id) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END ELSE CAST(0 AS BIT) END) AS 'EnableTab5.1.PupilarityExamSB',
        RTRIM(B.FurtherEvaluationPupilarExamObservations)    AS 'FurtherEvaluationPupilarExamObservations',
        (CASE WHEN EXISTS(SELECT TOP 1 OFEB.Id FROM [Glasses].[OptometryFurtherEvaluationBiomicroscopy] AS OFEB WHERE OFEB.IdOptometryClinicalEvaluationC=A.Id) THEN 'Valorado' WHEN E.HasAnophthalmia=1 AND E.AffectedLateralityAnophthalmia=3 THEN 'No aplica' ELSE 'No valorado' END) AS 'BiomicroscopyValorated',
        (CASE E.HasAnophthalmia WHEN 1 THEN CASE WHEN E.AffectedLateralityAnophthalmia BETWEEN 1 AND 2 THEN CASE WHEN EXISTS(SELECT TOP 1 OFEB.Id FROM [Glasses].[OptometryFurtherEvaluationBiomicroscopy] AS OFEB WHERE OFEB.IdOptometryClinicalEvaluationC=A.Id) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END ELSE CAST(0 AS BIT) END WHEN 2 THEN CASE WHEN EXISTS(SELECT TOP 1 OFEB.Id FROM [Glasses].[OptometryFurtherEvaluationBiomicroscopy] AS OFEB WHERE OFEB.IdOptometryClinicalEvaluationC=A.Id) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END ELSE CAST(0 AS BIT) END) AS 'EnableTab5.2.BiomicroscopyExamSB',
        RTRIM(B.FurtherEvaluationBiomicroscopyObservations)  AS 'FurtherEvaluationBiomicroscopyObservations',
        (CASE WHEN EXISTS(SELECT TOP 1 OFET.Id FROM [Glasses].[OptometryFurtherEvaluationTonometry] AS OFET WHERE OFET.IdOptometryClinicalEvaluationC=A.Id) THEN 'Valorado' WHEN E.HasAnophthalmia=1 AND E.AffectedLateralityAnophthalmia=3 THEN 'No aplica' ELSE 'No valorado' END) AS 'TonometryValorated',
        (CASE E.HasAnophthalmia WHEN 1 THEN CASE WHEN E.AffectedLateralityAnophthalmia BETWEEN 1 AND 2 THEN CASE WHEN EXISTS(SELECT TOP 1 OFET.Id FROM [Glasses].[OptometryFurtherEvaluationTonometry] AS OFET WHERE OFET.IdOptometryClinicalEvaluationC=A.Id) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END ELSE CAST(0 AS BIT) END WHEN 2 THEN CASE WHEN EXISTS(SELECT TOP 1 OFET.Id FROM [Glasses].[OptometryFurtherEvaluationTonometry] AS OFET WHERE OFET.IdOptometryClinicalEvaluationC=A.Id) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END ELSE CAST(0 AS BIT) END) AS 'EnableTab5.3.TonometrySB',
        RTRIM(B.FurtherEvaluationTonometryObservations)      AS 'FurtherEvaluationTonometryObservations',
        (CASE WHEN EXISTS(SELECT TOP 1 OFEF.Id FROM [Glasses].[OptometryFurtherEvaluationFundus] AS OFEF WHERE OFEF.IdOptometryClinicalEvaluationC=A.Id) THEN 'Valorado' WHEN E.HasAnophthalmia=1 AND E.AffectedLateralityAnophthalmia=3 THEN 'No aplica' ELSE 'No valorado' END) AS 'FundusValorated',
        (CASE E.HasAnophthalmia WHEN 1 THEN CASE WHEN E.AffectedLateralityAnophthalmia BETWEEN 1 AND 2 THEN CASE WHEN EXISTS(SELECT TOP 1 OFEF.Id FROM [Glasses].[OptometryFurtherEvaluationFundus] AS OFEF WHERE OFEF.IdOptometryClinicalEvaluationC=A.Id) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END ELSE CAST(0 AS BIT) END WHEN 2 THEN CASE WHEN EXISTS(SELECT TOP 1 OFEF.Id FROM [Glasses].[OptometryFurtherEvaluationFundus] AS OFEF WHERE OFEF.IdOptometryClinicalEvaluationC=A.Id) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END ELSE CAST(0 AS BIT) END) AS 'EnableTab5.4.FundusSB',
        RTRIM(B.FurtherEvaluationFundusObservations)         AS 'FurtherEvaluationFundusObservations',
        (CASE WHEN EXISTS(SELECT TOP 1 OFEG.Id FROM [Glasses].[OptometryFurtherEvaluationGonioscopy] AS OFEG WHERE OFEG.IdOptometryClinicalEvaluationC=A.Id) THEN 'Valorado' WHEN E.HasAnophthalmia=1 AND E.AffectedLateralityAnophthalmia=3 THEN 'No aplica' ELSE 'No valorado' END) AS 'GonioscopyValorated',
        (CASE E.HasAnophthalmia WHEN 1 THEN CASE WHEN E.AffectedLateralityAnophthalmia BETWEEN 1 AND 2 THEN CASE WHEN EXISTS(SELECT TOP 1 OFEG.Id FROM [Glasses].[OptometryFurtherEvaluationGonioscopy] AS OFEG WHERE OFEG.IdOptometryClinicalEvaluationC=A.Id) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END ELSE CAST(0 AS BIT) END WHEN 2 THEN CASE WHEN EXISTS(SELECT TOP 1 OFEG.Id FROM [Glasses].[OptometryFurtherEvaluationGonioscopy] AS OFEG WHERE OFEG.IdOptometryClinicalEvaluationC=A.Id) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END ELSE CAST(0 AS BIT) END) AS 'EnableTab5.5.GonioscopySB',
        RTRIM(B.FurtherEvaluationGonioscopyObservations)     AS 'FurtherEvaluationGonioscopyObservations',
        (CASE WHEN EXISTS(SELECT TOP 1 OFECV.Id FROM [Glasses].[OptometryFurtherEvaluationColorVision] AS OFECV WHERE OFECV.IdOptometryClinicalEvaluationC=A.Id) THEN 'Valorado' WHEN E.HasAnophthalmia=1 AND E.AffectedLateralityAnophthalmia=3 THEN 'No aplica' ELSE 'No valorado' END) AS 'ColorVisionValorated',
        (CASE E.HasAnophthalmia WHEN 1 THEN CASE WHEN E.AffectedLateralityAnophthalmia BETWEEN 1 AND 2 THEN CASE WHEN EXISTS(SELECT TOP 1 OFECV.Id FROM [Glasses].[OptometryFurtherEvaluationColorVision] AS OFECV WHERE OFECV.IdOptometryClinicalEvaluationC=A.Id) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END ELSE CAST(0 AS BIT) END WHEN 2 THEN CASE WHEN EXISTS(SELECT TOP 1 OFECV.Id FROM [Glasses].[OptometryFurtherEvaluationColorVision] AS OFECV WHERE OFECV.IdOptometryClinicalEvaluationC=A.Id) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END ELSE CAST(0 AS BIT) END) AS 'EnableTab5.6.ColorVisionSB',
        RTRIM(B.FurtherEvaluationColorVisionObservations)    AS 'FurtherEvaluationColorVisionObservations',
        (CASE B.FurtherEvaluationHasStereopsis WHEN 1 THEN 'Sí' WHEN 2 THEN 'No' ELSE NULL END) AS 'FurtherEvaluationHasStereopsis',
        (CASE B.FurtherEvaluationHasStereopsis WHEN 1 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END) AS 'EnableStereopsisFields',
        (CASE B.FurtherEvaluationStereopsisTechnique WHEN 1 THEN 'Titmus' WHEN 2 THEN 'Random' WHEN 3 THEN 'Frisby' WHEN 4 THEN 'Lang' ELSE NULL END) AS 'FurtherEvaluationStereopsisTechnique',
        RTRIM(B.FurtherEvaluationStereopsisArcSeconds)       AS 'FurtherEvaluationStereopsisArcSeconds',
        (CASE WHEN (RTRIM(B.FurtherEvaluationContactologyObservations) IS NOT NULL AND RTRIM(B.FurtherEvaluationContactologyObservations) <> '') THEN 'Valorado' WHEN E.HasAnophthalmia=1 AND E.AffectedLateralityAnophthalmia=3 THEN 'No aplica' ELSE 'No valorado' END) AS 'ContactologyValorated',
        (CASE WHEN (RTRIM(B.FurtherEvaluationContactologyObservations) IS NOT NULL AND RTRIM(B.FurtherEvaluationContactologyObservations) <> '') THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END) AS 'EnableTab5.7.ContactologySB',
        RTRIM(B.FurtherEvaluationContactologyObservations)   AS 'FurtherEvaluationContactologyObservations'
    FROM [Glasses].[OptometryClinicalEvaluationC] AS A WITH(NOLOCK)
    LEFT JOIN [Glasses].[OptometryClinicalEvaluationGeneralInformation] AS B WITH(NOLOCK)
        ON A.Id = B.IdOptometryClinicalEvaluationC
    LEFT JOIN [Glasses].[OptometryExternalReviewDetail] AS E WITH(NOLOCK)
        ON A.Id = E.IdOptometryClinicalEvaluationC
    WHERE A.IPCODPACI = @CodigoPaciente
      AND A.NUMINGRES = @NumeroIngreso
      AND A.NUMEFOLIO = @NumeroFolio
    ORDER BY A.Id ASC;

    -- ─────────────────────────────────────────────────────────────────────────
    -- Result set 15: Tab51 Table1 Pupilar
    -- ─────────────────────────────────────────────────────────────────────────
    SELECT A.Id,
        IIF(RTRIM(OFEPEDer.Size)          <> 'NoRegistra', RTRIM(OFEPEDer.Size),          '') AS 'Tab51Table1SizeDer',
        IIF(RTRIM(OFEPEDer.Direct)        <> 'NoRegistra', RTRIM(OFEPEDer.Direct),        '') AS 'Tab51Table1DirectoDer',
        IIF(RTRIM(OFEPEDer.Consensual)    <> 'NoRegistra', RTRIM(OFEPEDer.Consensual),    '') AS 'Tab51Table1ConsensualDer',
        IIF(RTRIM(OFEPEDer.Accommodation) <> 'NoRegistra', RTRIM(OFEPEDer.Accommodation), '') AS 'Tab51Table1AcomodacionDer',
        IIF(RTRIM(OFEPEDer.DPAR)          <> 'NoRegistra', RTRIM(OFEPEDer.DPAR),          '') AS 'Tab51Table1DPARDer',
        IIF(RTRIM(OFEPEIzq.Size)          <> 'NoRegistra', RTRIM(OFEPEIzq.Size),          '') AS 'Tab51Table1SizeIzq',
        IIF(RTRIM(OFEPEIzq.Direct)        <> 'NoRegistra', RTRIM(OFEPEIzq.Direct),        '') AS 'Tab51Table1DirectoIzq',
        IIF(RTRIM(OFEPEIzq.Consensual)    <> 'NoRegistra', RTRIM(OFEPEIzq.Consensual),    '') AS 'Tab51Table1ConsensualIzq',
        IIF(RTRIM(OFEPEIzq.Accommodation) <> 'NoRegistra', RTRIM(OFEPEIzq.Accommodation), '') AS 'Tab51Table1AcomodacionIzq',
        IIF(RTRIM(OFEPEIzq.DPAR)          <> 'NoRegistra', RTRIM(OFEPEIzq.DPAR),          '') AS 'Tab51Table1DPARIzq'
    FROM [Glasses].[OptometryClinicalEvaluationC] AS A WITH(NOLOCK)
    LEFT JOIN [Glasses].[OptometryFurtherEvaluationPupillaryExam] AS OFEPEDer WITH(NOLOCK)
        ON A.Id = OFEPEDer.IdOptometryClinicalEvaluationC AND OFEPEDer.Eye = 1
    LEFT JOIN [Glasses].[OptometryFurtherEvaluationPupillaryExam] AS OFEPEIzq WITH(NOLOCK)
        ON A.Id = OFEPEIzq.IdOptometryClinicalEvaluationC AND OFEPEIzq.Eye = 2
    WHERE A.IPCODPACI = @CodigoPaciente
      AND A.NUMINGRES = @NumeroIngreso
      AND A.NUMEFOLIO = @NumeroFolio
    ORDER BY A.Id ASC;

    -- ─────────────────────────────────────────────────────────────────────────
    -- Result set 16: Tab52 Table1 Biomicroscopy
    -- ─────────────────────────────────────────────────────────────────────────
    SELECT A.Id,
        IIF(RTRIM(OFEBDer.Biomicroscopy) <> 'NoRegistra', RTRIM(OFEBDer.Biomicroscopy), '') AS 'Tab52Table1BiomicroscopiaDer',
        IIF(RTRIM(OFEBIzq.Biomicroscopy) <> 'NoRegistra', RTRIM(OFEBIzq.Biomicroscopy), '') AS 'Tab52Table1BiomicroscopiaIzq'
    FROM [Glasses].[OptometryClinicalEvaluationC] AS A WITH(NOLOCK)
    LEFT JOIN [Glasses].[OptometryFurtherEvaluationBiomicroscopy] AS OFEBDer WITH(NOLOCK)
        ON A.Id = OFEBDer.IdOptometryClinicalEvaluationC AND OFEBDer.Eye = 1
    LEFT JOIN [Glasses].[OptometryFurtherEvaluationBiomicroscopy] AS OFEBIzq WITH(NOLOCK)
        ON A.Id = OFEBIzq.IdOptometryClinicalEvaluationC AND OFEBIzq.Eye = 2
    WHERE A.IPCODPACI = @CodigoPaciente
      AND A.NUMINGRES = @NumeroIngreso
      AND A.NUMEFOLIO = @NumeroFolio
    ORDER BY A.Id ASC;

    -- ─────────────────────────────────────────────────────────────────────────
    -- Result set 17: Tab53 Table1 Tonometry
    -- ─────────────────────────────────────────────────────────────────────────
    SELECT A.Id,
        IIF(RTRIM(OFETDer.Flattening) <> 'NoRegistra', RTRIM(OFETDer.Flattening), '') AS 'Tab53Table1AplanacionDer',
        IIF(RTRIM(OFETDer.Digital)    <> 'NoRegistra', RTRIM(OFETDer.Digital),    '') AS 'Tab53Table1DigitalDer',
        IIF(RTRIM(OFETDer.Another)    <> 'NoRegistra', RTRIM(OFETDer.Another),    '') AS 'Tab53Table1OtroDer',
        IIF(RTRIM(OFETIzq.Flattening) <> 'NoRegistra', RTRIM(OFETIzq.Flattening), '') AS 'Tab53Table1AplanacionIzq',
        IIF(RTRIM(OFETIzq.Digital)    <> 'NoRegistra', RTRIM(OFETIzq.Digital),    '') AS 'Tab53Table1DigitalIzq',
        IIF(RTRIM(OFETIzq.Another)    <> 'NoRegistra', RTRIM(OFETIzq.Another),    '') AS 'Tab53Table1OtroIzq'
    FROM [Glasses].[OptometryClinicalEvaluationC] AS A WITH(NOLOCK)
    LEFT JOIN [Glasses].[OptometryFurtherEvaluationTonometry] AS OFETDer WITH(NOLOCK)
        ON A.Id = OFETDer.IdOptometryClinicalEvaluationC AND OFETDer.Eye = 1
    LEFT JOIN [Glasses].[OptometryFurtherEvaluationTonometry] AS OFETIzq WITH(NOLOCK)
        ON A.Id = OFETIzq.IdOptometryClinicalEvaluationC AND OFETIzq.Eye = 2
    WHERE A.IPCODPACI = @CodigoPaciente
      AND A.NUMINGRES = @NumeroIngreso
      AND A.NUMEFOLIO = @NumeroFolio
    ORDER BY A.Id ASC;

    -- ─────────────────────────────────────────────────────────────────────────
    -- Result set 18: Tab54 Table1 Fundus
    -- ─────────────────────────────────────────────────────────────────────────
    SELECT A.Id,
        CASE OFEFDer.[Type] WHEN 1 THEN 'Directo' WHEN 2 THEN 'Indirecto' ELSE NULL END AS 'FondoDeOjoTipo',
        IIF(RTRIM(OFEFDer.Fundus) <> 'NoRegistra', RTRIM(OFEFDer.Fundus), '') AS 'Tab54Table1FondoDer',
        IIF(RTRIM(OFEFIzq.Fundus) <> 'NoRegistra', RTRIM(OFEFIzq.Fundus), '') AS 'Tab54Table1FondoIzq'
    FROM [Glasses].[OptometryClinicalEvaluationC] AS A WITH(NOLOCK)
    LEFT JOIN [Glasses].[OptometryFurtherEvaluationFundus] AS OFEFDer WITH(NOLOCK)
        ON A.Id = OFEFDer.IdOptometryClinicalEvaluationC AND OFEFDer.Eye = 1
    LEFT JOIN [Glasses].[OptometryFurtherEvaluationFundus] AS OFEFIzq WITH(NOLOCK)
        ON A.Id = OFEFIzq.IdOptometryClinicalEvaluationC AND OFEFIzq.Eye = 2
    WHERE A.IPCODPACI = @CodigoPaciente
      AND A.NUMINGRES = @NumeroIngreso
      AND A.NUMEFOLIO = @NumeroFolio
    ORDER BY A.Id ASC;

    -- ─────────────────────────────────────────────────────────────────────────
    -- Result set 19: Tab55 Table1 Gonioscopy
    -- ─────────────────────────────────────────────────────────────────────────
    SELECT A.Id,
        IIF(RTRIM(OFEGDer.Gonioscopy) <> 'NoRegistra', RTRIM(OFEGDer.Gonioscopy), '') AS 'Tab55Table1GonioscopiaDer',
        IIF(RTRIM(OFEGIzq.Gonioscopy) <> 'NoRegistra', RTRIM(OFEGIzq.Gonioscopy), '') AS 'Tab55Table1GonioscopiaIzq'
    FROM [Glasses].[OptometryClinicalEvaluationC] AS A WITH(NOLOCK)
    LEFT JOIN [Glasses].[OptometryFurtherEvaluationGonioscopy] AS OFEGDer WITH(NOLOCK)
        ON A.Id = OFEGDer.IdOptometryClinicalEvaluationC AND OFEGDer.Eye = 1
    LEFT JOIN [Glasses].[OptometryFurtherEvaluationGonioscopy] AS OFEGIzq WITH(NOLOCK)
        ON A.Id = OFEGIzq.IdOptometryClinicalEvaluationC AND OFEGIzq.Eye = 2
    WHERE A.IPCODPACI = @CodigoPaciente
      AND A.NUMINGRES = @NumeroIngreso
      AND A.NUMEFOLIO = @NumeroFolio
    ORDER BY A.Id ASC;

    -- ─────────────────────────────────────────────────────────────────────────
    -- Result set 20: Tab56 Table1 Color Vision
    -- ─────────────────────────────────────────────────────────────────────────
    SELECT A.Id,
        CASE
            WHEN (OFECV.Ihsihara=1 AND OFECV.Farnsworth=1 AND OFECV.Lanthony=1) THEN 'Ishihara, Farnsworth, Lanthony'
            WHEN (OFECV.Ihsihara=2 AND OFECV.Farnsworth=1 AND OFECV.Lanthony=1) THEN 'Farnsworth, Lanthony'
            WHEN (OFECV.Ihsihara=1 AND OFECV.Farnsworth=2 AND OFECV.Lanthony=1) THEN 'Ishihara, Lanthony'
            WHEN (OFECV.Ihsihara=2 AND OFECV.Farnsworth=2 AND OFECV.Lanthony=1) THEN 'Lanthony'
            WHEN (OFECV.Ihsihara=1 AND OFECV.Farnsworth=2 AND OFECV.Lanthony=2) THEN 'Ishihara'
            WHEN (OFECV.Ihsihara=2 AND OFECV.Farnsworth=1 AND OFECV.Lanthony=2) THEN 'Farnsworth'
            ELSE NULL
        END AS 'TipoColorTest',
        CASE OFECV.Result WHEN 1 THEN 'Normal' WHEN 2 THEN 'Anormal' ELSE NULL END AS 'ResultadoColorTest'
    FROM [Glasses].[OptometryClinicalEvaluationC] AS A WITH(NOLOCK)
    LEFT JOIN [Glasses].[OptometryFurtherEvaluationColorVision] AS OFECV WITH(NOLOCK)
        ON A.Id = OFECV.IdOptometryClinicalEvaluationC
    WHERE A.IPCODPACI = @CodigoPaciente
      AND A.NUMINGRES = @NumeroIngreso
      AND A.NUMEFOLIO = @NumeroFolio
    ORDER BY A.Id ASC;

    -- ─────────────────────────────────────────────────────────────────────────
    -- Result set 21: Page Adicionales
    -- ─────────────────────────────────────────────────────────────────────────
    SELECT A.Id,
        RTRIM(B.OphthalmologicalDiagnoses) AS 'OphthalmologicalDiagnoses',
        RTRIM(B.Analysis)                  AS 'Analysis',
        RTRIM(B.ManagementPlan)            AS 'ManagementPlan',
        RTRIM(B.WorkTeam)                  AS 'WorkTeam'
    FROM [Glasses].[OptometryClinicalEvaluationC] AS A WITH(NOLOCK)
    LEFT JOIN [Glasses].[OptometryClinicalEvaluationGeneralInformation] AS B WITH(NOLOCK)
        ON A.Id = B.IdOptometryClinicalEvaluationC
    WHERE A.IPCODPACI = @CodigoPaciente
      AND A.NUMINGRES = @NumeroIngreso
      AND A.NUMEFOLIO = @NumeroFolio
    ORDER BY A.Id ASC;

END
GO
