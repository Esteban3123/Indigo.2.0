-- =============================================
-- Autor: Juan Tovar
-- Fecha Creacion: 23-04-2026
-- Descripcion:  Sp que lista la Información de las Fichas del Sivigila, Ficha 114 (Riesgo de desnutricion)
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila114]
(
    @IdFicha INT

	)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT	    				 
        RTRIM(BirthWeightGrams) AS PESO_NACER,
        RTRIM(BirthHeightCm) AS TALLA_NACER,
        RTRIM(CurrentWeightKg) AS PESO_ACTUAL,
        RTRIM(CurrentHeightCm) AS TALLA_ACTUAL,
        RTRIM(MidUpperArmCircumferenceCm) AS CIRCUNFERENCIA_MEDIA_BRAZO,

        CASE WHEN InsufficientWeightGain = 1 THEN 'X' END AS GANAC_PESO_INSUF_SI,
        CASE WHEN InsufficientWeightGain = 0 THEN 'X' END AS GANAC_PESO_INSUF_NO,

        CASE WHEN GrowthCurveFlatteningDescent = 1 THEN 'X' END AS APLANAM_CURVA_CRECIM_SI,
        CASE WHEN GrowthCurveFlatteningDescent = 0 THEN 'X' END AS APLANAM_CURVA_CRECIM_NO,

        CASE WHEN NoWeightRecoveryThirdWeek = 1 THEN 'X' END AS NO_RECUP_NACER_SI,
        CASE WHEN NoWeightRecoveryThirdWeek = 0 THEN 'X' END AS NO_RECUP_NACER_NO,

        CASE WHEN Anemia = 1 THEN 'X' END AS ANEMIA_SI,
        CASE WHEN Anemia = 0 THEN 'X' END AS ANEMIA_NO,

        CASE WHEN AcuteMalnutritionHistory = 1 THEN 'X' END AS ANTEC_DESNUTR_AGUDA_SI,
        CASE WHEN AcuteMalnutritionHistory = 0 THEN 'X' END AS ANTEC_DESNUTR_AGUDA_NO,

        CASE WHEN RecentIraEdaEpisodes = 1 THEN 'X' END AS EPIS_IRA_EDA_SI,
        CASE WHEN RecentIraEdaEpisodes = 0 THEN 'X' END AS EPIS_IRA_EDA_NO,

        CASE WHEN FeedingDifficulties = 1 THEN 'X' END AS DIFICULT_ALIMENT_SI,
        CASE WHEN FeedingDifficulties = 0 THEN 'X' END AS DIFICULT_ALIMENT_NO,

        CASE WHEN InadequateFeedingPractices = 1 THEN 'X' END AS PRACT_ALIMNET_INADE_SI,
        CASE WHEN InadequateFeedingPractices = 0 THEN 'X' END AS PRACT_ALIMNET_INADE_NO,

        CASE WHEN BrachialPerimeterBelow = 1 THEN 'X' END AS PERIMETR_BRANQUEAL_SI,
        CASE WHEN BrachialPerimeterBelow = 0 THEN 'X' END AS PERIMETR_BRANQUEAL_NO,

        CASE WHEN RecurrentPersistentInfections = 1 THEN 'X' END AS INFECCION_RECURRENT_SI,
        CASE WHEN RecurrentPersistentInfections = 0 THEN 'X' END AS INFECCION_RECURRENT_NO,

        CASE WHEN MotherAbsence = 1 THEN 'X' END AS AUSENCIA_PERM_MADRE_SI,
        CASE WHEN MotherAbsence = 0 THEN 'X' END AS AUSENCIA_PERM_MADRE_NO,

        CASE WHEN CaregiverHealthProblems = 1 THEN 'X' END AS PROBLEM_SALUD_CUIDADOR_SI,
        CASE WHEN CaregiverHealthProblems = 0 THEN 'X' END AS PROBLEM_SALUD_CUIDADOR_NO,

        CASE WHEN TeenMotherNoSupport = 1 THEN 'X' END AS MADRE_ADOLES_SIN_APOYO_SI,
        CASE WHEN TeenMotherNoSupport = 0 THEN 'X' END AS MADRE_ADOLES_SIN_APOYO_NO,

        CASE WHEN SocioeconomicVulnerability = 1 THEN 'X' END AS VULNERAB_SOCIOECONOM_SI,
        CASE WHEN SocioeconomicVulnerability = 0 THEN 'X' END AS VULNERAB_SOCIOECONOM_NO,

        CASE WHEN HealthServiceAccessDifficulty = 1 THEN 'X' END AS DIFILC_ACCES_SALUD_SI,
        CASE WHEN HealthServiceAccessDifficulty = 0 THEN 'X' END AS DIFILC_ACCES_SALUD_NO,

        CASE WHEN TwoOrMoreChildrenFoodInsecurity = 1 THEN 'X' END AS HOGAR_VULNERABLE_SI,
        CASE WHEN TwoOrMoreChildrenFoodInsecurity = 0 THEN 'X' END AS HOGAR_VULNERABLE_NO,

        CASE WHEN ResidenceInNutritionalRiskZone = 1 THEN 'X' END AS RESIDENT_ZONA_RIESGO_NUTRI_SI,
        CASE WHEN ResidenceInNutritionalRiskZone = 0 THEN 'X' END AS RESIDENT_ZONA_RIESGO_NUTRI_NO,

        CASE WHEN PrematurityHistory = 1 THEN 'X' END AS ANTECEDENT_PREMATUREZ_SI,
        CASE WHEN PrematurityHistory = 0 THEN 'X' END AS ANTECEDENT_PREMATUREZ_NO,

        CASE WHEN LowBirthWeightHistory = 1 THEN 'X' END AS ANTC_BAJO_PESO_NACER_SI,
        CASE WHEN LowBirthWeightHistory = 0 THEN 'X' END AS ANTC_BAJO_PESO_NACER_NO,

        CASE WHEN SmallForGestationalAge = 1 THEN 'X' END AS PEQUE_EDAD_GESTACIONAL_SI,
        CASE WHEN SmallForGestationalAge = 0 THEN 'X' END AS PEQUE_EDAD_GESTACIONAL_NO,

        RTRIM([VERSION]) AS [VERSION],
        RTRIM([JSON])    AS [JSON]
    FROM HCFICHA114
    WHERE IDFICHANOTIFICACION = @IdFicha;
END
GO
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Recupera los datos de la Ficha 114 del sistema SIVIGILA (riesgo de desnutrición) almacenados en la tabla `HCFICHA114`, filtrando por el identificador de la ficha de notificación. Retorna medidas antropométricas (peso y talla al nacer y actuales, circunferencia braquial) junto con indicadores dicotómicos (Sí/No) de factores de riesgo nutricional, clínico y socioeconómico, formateados para impresión del formulario oficial.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila114';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila114';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'"Recupera los datos antropométricos y los factores de riesgo de desnutrición registrados en la ficha Sivigila 114 para una notificación específica, presentándolos en formato apto para reporte (marcas SI/NO)."', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila114';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en la tabla de fichas 114 cuyo identificador de notificación coincida con el parámetro recibido; de lo contrario el resultado es vacío.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila114';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada factor de riesgo clínico/social se expone como dos columnas mutuamente excluyentes (SI/NO) marcadas con ''X'' según el valor 1/0; si el campo es NULL ambas quedan vacías.; Los valores de texto/medidas antropométricas se entregan sin espacios sobrantes a la derecha (RTRIM).; El resultado se filtra exclusivamente por el identificador de la ficha de notificación, retornando a lo más un registro asociado a esa ficha.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila114';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Sivigila; Ficha 114; Riesgo de desnutrición; Antropometría (peso, talla, perímetro braquial); Anemia; Antecedentes de desnutrición aguda; Prematurez; Bajo peso al nacer; Pequeño para edad gestacional; Episodios IRA/EDA; Prácticas de alimentación; Vulnerabilidad socioeconómica; Inseguridad alimentaria; Madre adolescente sin apoyo; Cuidador con problemas de salud', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila114';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCFICHA114: Cuando IDFICHANOTIFICACION coincide con el identificador recibido, se retorna una fila con medidas antropométricas, marcadores ''X'' por cada factor de riesgo (1=SI, 0=NO) y los campos VERSION y JSON de la ficha.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila114';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA114', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila114';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila114';
-- GO
