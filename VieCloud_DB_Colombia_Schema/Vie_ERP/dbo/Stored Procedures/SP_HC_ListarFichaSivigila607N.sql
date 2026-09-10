
-- =============================================
-- Autor: Hernando Cabrera
-- Fecha Creacion: 13-01-2026
-- Descripcion:  Sp que lista la Información de las Fichas del Sivigila, Ficha 607 (Ébola)
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila607N]
(
    @IdFicha INT

	)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        RTRIM(FamilyMemberName) AS NOM_FAMLIAR,
        RTRIM(ContactPhone1)    AS TEL_FAMILIAR1,
        RTRIM(ContactPhone2)    AS TEL_FAMILIAR2,
        CONVERT(VARCHAR(10), ArrivalDateColombia, 103) AS FECHA_ING_COL,

        CASE WHEN HasTravelHistory = 1 THEN 'X' END AS TIENE_DESPLAZAMIENTO_SI,
        CASE WHEN HasTravelHistory = 0 THEN 'X' END AS TIENE_DESPLAZAMIENTO_NO,

        RTRIM(TravelCity1)    AS CIUDAD_DESP1,
        RTRIM(TravelState1)   AS DEPTO_DESP1,
        RTRIM(TravelCountry1) AS PAIS_DESP1,
        CONVERT(VARCHAR(10), TravelDate1, 103) AS FECHA_DESP1,

        RTRIM(TravelCity2)    AS CIUDAD_DESP2,
        RTRIM(TravelState2)   AS DEPTO_DESP2,
        RTRIM(TravelCountry2) AS PAIS_DESP2,
        CONVERT(VARCHAR(10), TravelDate2, 103) AS FECHA_DESP2,

        RTRIM(TravelCity3)    AS CIUDAD_DESP3,
        RTRIM(TravelState3)   AS DEPTO_DESP3,
        RTRIM(TravelCountry3) AS PAIS_DESP3,
        CONVERT(VARCHAR(10), TravelDate3, 103) AS FECHA_DESP3,

        RTRIM(TravelCity4)    AS CIUDAD_DESP4,
        RTRIM(TravelState4)   AS DEPTO_DESP4,
        RTRIM(TravelCountry4) AS PAIS_DESP4,
        CONVERT(VARCHAR(10), TravelDate4, 103) AS FECHA_DESP4,

        RTRIM(TravelCity5)    AS CIUDAD_DESP5,
        RTRIM(TravelState5)   AS DEPTO_DESP5,
        RTRIM(TravelCountry5) AS PAIS_DESP5,
        CONVERT(VARCHAR(10), TravelDate5, 103) AS FECHA_DESP5,

       CASE TRY_CAST(JSON_VALUE(SelectedSigns,'$.Fiebre') AS BIT) WHEN 1 THEN 'X' END AS [Fiebre],
       CASE TRY_CAST(JSON_VALUE(SelectedSigns,'$.Vomito') AS BIT) WHEN 1 THEN 'X' END AS [Vomito],
       CASE TRY_CAST(JSON_VALUE(SelectedSigns,'$.SangradoCoagulopatias') AS BIT) WHEN 1 THEN 'X' END AS [Sangrado_coagulopatias],
       CASE TRY_CAST(JSON_VALUE(SelectedSigns,'$.Edema') AS BIT) WHEN 1 THEN 'X' END AS [Edema],
       CASE TRY_CAST(JSON_VALUE(SelectedSigns,'$.Cefalea') AS BIT) WHEN 1 THEN 'X' END AS [Cefalea],
       CASE TRY_CAST(JSON_VALUE(SelectedSigns,'$.DolorAbdominal') AS BIT) WHEN 1 THEN 'X' END AS [Dolor_abdominal],
       CASE TRY_CAST(JSON_VALUE(SelectedSigns,'$.AsteniaAdinamia') AS BIT) WHEN 1 THEN 'X' END AS [Astenia_adinamia],
       CASE TRY_CAST(JSON_VALUE(SelectedSigns,'$.Erupcion') AS BIT) WHEN 1 THEN 'X' END AS [Erupcion],
       CASE TRY_CAST(JSON_VALUE(SelectedSigns,'$.DolorMuscular') AS BIT) WHEN 1 THEN 'X' END AS [Dolor_muscular],
       CASE TRY_CAST(JSON_VALUE(SelectedSigns,'$.Anorexia') AS BIT) WHEN 1 THEN 'X' END AS [Anorexia],
       CASE TRY_CAST(JSON_VALUE(SelectedSigns,'$.Diarrea') AS BIT) WHEN 1 THEN 'X' END AS [Diarrea],
       CASE TRY_CAST(JSON_VALUE(SelectedSigns,'$.Conjuntivitis') AS BIT) WHEN 1 THEN 'X' END AS [Conjuntivitis],
       CASE TRY_CAST(JSON_VALUE(SelectedSigns,'$.Otros') AS BIT) WHEN 1 THEN 'X' END AS [Otros],
				    				 
        RTRIM(OtherSigns) AS OTROS_SIGNOS,

        CASE WHEN HasPostSymptomTravel = 1 THEN 'X' END AS TIENE_DESP_POST_SI,
        CASE WHEN HasPostSymptomTravel = 0 THEN 'X' END AS TIENE_DESP_POST_NO,

        RTRIM(PostTravelCity1)          AS CIUDAD_POST1,
        RTRIM(PostTravelState1)         AS DEPTO_POST1,
        RTRIM(PostTravelCountry1)       AS PAIS_POST1,
        RTRIM(PostTravelSpecificPlace1) AS LUGAR_POST1,
        CONVERT(VARCHAR(10), PostTravelDate1, 103) AS FECHA_POST1,

        RTRIM(PostTravelCity2)          AS CIUDAD_POST2,
        RTRIM(PostTravelState2)         AS DEPTO_POST2,
        RTRIM(PostTravelCountry2)       AS PAIS_POST2,
        RTRIM(PostTravelSpecificPlace2) AS LUGAR_POST2,
        CONVERT(VARCHAR(10), PostTravelDate2, 103) AS FECHA_POST2,

        RTRIM(PostTravelCity3)          AS CIUDAD_POST3,
        RTRIM(PostTravelState3)         AS DEPTO_POST3,
        RTRIM(PostTravelCountry3)       AS PAIS_POST3,
        RTRIM(PostTravelSpecificPlace3) AS LUGAR_POST3,
        CONVERT(VARCHAR(10), PostTravelDate3, 103) AS FECHA_POST3,

        RTRIM(PostTravelCity4)          AS CIUDAD_POST4,
        RTRIM(PostTravelState4)         AS DEPTO_POST4,
        RTRIM(PostTravelCountry4)       AS PAIS_POST4,
        RTRIM(PostTravelSpecificPlace4) AS LUGAR_POST4,
        CONVERT(VARCHAR(10), PostTravelDate4, 103) AS FECHA_POST4,

        RTRIM(ContactName1)  AS NOMBRE_CON1,
        RTRIM(ContactPhone11) AS TEL1_CON1,
        RTRIM(ContactPhone12) AS TEL2_CON1,
        CONVERT(VARCHAR(10), ContactDate1, 103) AS FECHA_CON1,

        RTRIM(ContactName2)  AS NOMBRE_CON2,
        RTRIM(ContactPhone21) AS TEL1_CON2,
        RTRIM(ContactPhone22) AS TEL2_CON2,
        CONVERT(VARCHAR(10), ContactDate2, 103) AS FECHA_CON2,

        RTRIM(ContactName3)  AS NOMBRE_CON3,
        RTRIM(ContactPhone31) AS TEL1_CON3,
        RTRIM(ContactPhone32) AS TEL2_CON3,
        CONVERT(VARCHAR(10), ContactDate3, 103) AS FECHA_CON3,

        RTRIM(ContactName4)  AS NOMBRE_CON4,
        RTRIM(ContactPhone41) AS TEL1_CON4,
        RTRIM(ContactPhone42) AS TEL2_CON4,
        CONVERT(VARCHAR(10), ContactDate4, 103) AS FECHA_CON4,

        RTRIM(ContactName5)  AS NOMBRE_CON5,
        RTRIM(ContactPhone51) AS TEL1_CON5,
        RTRIM(ContactPhone52) AS TEL2_CON5,
        CONVERT(VARCHAR(10), ContactDate5, 103) AS FECHA_CON5,

        CONVERT(VARCHAR(10), InterviewDate, 103) AS FECHA_ENTREVISTA,
        RTRIM(InterviewTime) AS HORA_ENTREVISTA,
		RTRIM(dbo.TipoProfesionMedico(InterviewProfessionalType)) AS PROFESION_ENTREVISTADOR,
       

	    
        RTRIM([VERSION]) AS [VERSION],
        RTRIM([JSON])    AS [JSON]
    FROM HCFICHA607N
    WHERE IDFICHANOTIFICACION = @IdFicha;
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recupera el detalle completo de la Ficha SIVIGILA 607 correspondiente a Enfermedad por Virus del Ébola, dado el identificador de la ficha de notificación. Devuelve datos del familiar de contacto (nombre y teléfonos), fecha de llegada a Colombia, historial de desplazamientos previos (hasta 5 viajes con ciudad, departamento, país y fecha), signos y síntomas clínicos registrados en formato JSON (fiebre, vómito, sangrado, cefalea, diarrea, entre otros), desplazamientos posteriores al inicio de síntomas (hasta 4 destinos con lugar específico), listado de personas en contacto estrecho con el caso (hasta 5 contactos con nombre, teléfonos y fecha de contacto), y datos de la entrevista epidemiológica (fecha, hora y profesión del entrevistador). Se utiliza para imprimir o visualizar la ficha epidemiológica oficial de Ébola exigida por el SIVIGILA en la historia clínica del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila607N';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila607N';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera y formatea los datos de una ficha de notificación Sivigila 607 (Ébola) para visualización o impresión, incluyendo antecedentes de viaje, signos clínicos y contactos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila607N';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCFICHA607N cuyo IDFICHANOTIFICACION coincida con el identificador recibido; La columna SelectedSigns debe contener un JSON válido con propiedades booleanas de signos clínicos; Debe existir la función dbo.TipoProfesionMedico para resolver el tipo de profesional entrevistador', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila607N';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las fechas se entregan siempre en formato dd/mm/yyyy (estilo 103); Los campos de texto se entregan sin espacios a la derecha (RTRIM); Los indicadores Sí/No para desplazamiento son mutuamente excluyentes según el bit de origen; Los signos clínicos se exponen como marca ''X'' o NULL, nunca como booleano; La consulta es de solo lectura sobre HCFICHA607N; Soporta hasta 5 desplazamientos previos, 4 desplazamientos post-síntomas y 5 contactos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila607N';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha Sivigila 607; Ébola; Notificación epidemiológica; Antecedentes de viaje; Desplazamientos post-síntomas; Contactos epidemiológicos; Signos y síntomas clínicos; Entrevista epidemiológica; Profesional entrevistador', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila607N';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCFICHA607N: Devuelve una sola fila con los datos de la ficha 607 cuando IDFICHANOTIFICACION coincide con el parámetro recibido', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila607N';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si HasTravelHistory = 1 → Marca ''X'' en TIENE_DESPLAZAMIENTO_SI else Si HasTravelHistory = 0 marca ''X'' en TIENE_DESPLAZAMIENTO_NO; si HasPostSymptomTravel = 1 → Marca ''X'' en TIENE_DESP_POST_SI else Si HasPostSymptomTravel = 0 marca ''X'' en TIENE_DESP_POST_NO; si Cada propiedad booleana del JSON SelectedSigns (Fiebre, Vomito, SangradoCoagulopatias, Edema, Cefalea, DolorAbdominal, AsteniaAdinamia, Erupcion, DolorMuscular, Anorexia, Diarrea, Conjuntivitis, Otros) evalúa a 1 → Marca ''X'' en la columna correspondiente al signo clínico else Deja el valor en NULL si la propiedad no existe o no es 1', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila607N';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.TipoProfesionMedico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila607N';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA607N', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila607N';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila607N';
-- GO
