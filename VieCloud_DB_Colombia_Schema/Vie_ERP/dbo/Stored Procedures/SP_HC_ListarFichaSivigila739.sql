
CREATE PROCEDURE   [dbo].[SP_HC_ListarFichaSivigila739]
(
  @IdFicha as Int
)

AS
BEGIN
    SET NOCOUNT ON

   SELECT
    H739.Id,
    H739.IDFICHANOTIFICACION,
    H739.CODDIAGNO,
   

     CASE TRY_CAST(JSON_VALUE( H739.InitialSymptoms,'$.Fiebre') AS BIT ) WHEN 1 THEN 'X' else '' END AS [Fiebre] ,
     CASE TRY_CAST(JSON_VALUE( H739.InitialSymptoms,'$.Diarrea') AS BIT ) WHEN 1 THEN 'X' else '' END AS [Diarrea] ,
     CASE TRY_CAST(JSON_VALUE( H739.InitialSymptoms,'$.Dolor_abdominal') AS BIT ) WHEN 1 THEN 'X' else '' END AS [Dolor Abdominal] ,
     CASE TRY_CAST(JSON_VALUE( H739.InitialSymptoms,'$.Nauseas_Vomito') AS BIT ) WHEN 1 THEN 'X' else '' END AS [NauseasVomito] ,
     CASE TRY_CAST(JSON_VALUE( H739.InitialSymptoms,'$.Hipotension') AS BIT ) WHEN 1 THEN 'X' else '' END AS [Hipotension] ,
     CASE TRY_CAST(JSON_VALUE( H739.InitialSymptoms,'$.OTROS') AS BIT ) WHEN 1 THEN 'X' else '' END AS [Otros] ,
     H739.OtherSymptoms AS [Otros sintomas],

    -- Campos clínicos
    CONVERT(VARCHAR(10), H739.FeverStartDate, 103) AS [Fecha inicio fiebre],

        H739.RtPcrPositive AS [RTPCR],
        H739.AcIgmIggPositive AS [IgMIgc],
        H739.EpidemiologicalLinkToCovidCase AS [NexoEpidemiologicopositivo],

        
        CASE H739.HasFibrinogenoAlteration WHEN 1 THEN 1 ELSE 0 END AS [FibrinogenoSi],
        CASE H739.HasFibrinogenoAlteration WHEN 0 THEN 1 ELSE 0 END AS [FibrinogenoNo],
        COALESCE(NULLIF(H739.FibrinogenoValue, ''), '0') AS [Valor Fibrinogeno],
       

        CASE H739.HasCReactiveProteinAlteration WHEN 1 THEN 1 ELSE 0 END AS [CReactiveProteinSi],
        CASE H739.HasCReactiveProteinAlteration WHEN 0 THEN 1 ELSE 0 END AS [CReactiveProteinNo],
        COALESCE(NULLIF(H739.CReactiveProteinValue, ''), '0') AS [Valor ProteinaC],
       

        CASE H739.HasFerritinaAlteration WHEN 1 THEN 1 ELSE 0 END AS [FerritinaSi],
        CASE H739.HasFerritinaAlteration WHEN 0 THEN 1 ELSE 0 END AS [FerritinaNo],
        COALESCE(NULLIF(H739.FerritinaValue, ''), '0') AS [Valor Ferritina],
     

        CASE H739.HasDDimeroAlteration WHEN 1 THEN 1 ELSE 0 END AS [DDimeroSi],
        CASE H739.HasDDimeroAlteration WHEN 0 THEN 1 ELSE 0 END AS [DDimeroNo],
        COALESCE(NULLIF(H739.DDimeroValue, ''), '0') AS [Valor DDimero],
       

        CASE H739.HasLinfopenia WHEN 1 THEN 1 ELSE 0 END AS [LinfopeniaSi],
        CASE H739.HasLinfopenia WHEN 0 THEN 1 ELSE 0 END AS [LinfopeniaNo],
        COALESCE(NULLIF(H739.LinfopeniaValue, ''), '0') AS [Valor Linfopenia],
   

        CASE H739.HasTroponinaElevation WHEN 1 THEN 1 ELSE 0 END AS [TroponinaSi],
        CASE H739.HasTroponinaElevation WHEN 0 THEN 1 ELSE 0 END AS [TroponinaNo],
        COALESCE(NULLIF(H739.TroponinaValue, ''), '0') AS [Valor Troponina],
    

    H739.VERSION
FROM .dbo.HCFICHA739 AS H739
WHERE H739.IDFICHANOTIFICACION = @IdFicha 
ORDER BY H739.Id DESC;
   
   
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera el detalle completo de una ficha epidemiológica SIVIGILA 739 para notificación de casos sospechosos o confirmados de Síndrome Inflamatorio Multisistémico asociado a COVID-19. Dado el identificador interno de la ficha de notificación, consulta la tabla HCFICHA739 y devuelve los síntomas iniciales (fiebre, diarrea, dolor abdominal, náuseas, hipotensión y otros), la fecha de inicio de fiebre, los resultados de pruebas diagnósticas (RT-PCR, IgM/IgG) y el nexo epidemiológico con caso de COVID-19. También expone los marcadores inflamatorios del paciente —fibrinógeno, proteína C reactiva, ferritina, dímero D, linfopenia y troponina— indicando si cada marcador presenta alteración o elevación y su valor numérico registrado. Se utiliza para visualizar e imprimir la ficha de vigilancia epidemiológica 739 dentro de la historia clínica, apoyando el reporte oficial a las autoridades de salud pública.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila739';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila739';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consultar y formatear los datos clínicos y de síntomas de una ficha de notificación Sivigila (código 739) asociada a COVID-19, transformando flags y JSON a representaciones legibles para reporte.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila739';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe proporcionarse el identificador de la ficha de notificación para filtrar los registros.; La tabla de fichas debe contener la columna InitialSymptoms con un JSON válido que incluya las claves de síntomas esperadas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila739';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los síntomas almacenados en JSON se exponen como ''X'' cuando el valor es 1 y vacío en cualquier otro caso (incluyendo nulos o no convertibles a BIT).; Cada marcador clínico (fibrinógeno, PCR, ferritina, dímero D, linfopenia, troponina) se desdobla en dos banderas mutuamente excluyentes Sí/No según el valor de alteración (1/0); si el flag es NULL ambas quedan en 0.; Los valores numéricos de marcadores clínicos vacíos o nulos se normalizan a ''0''.; Las fechas de inicio de fiebre se entregan en formato dd/mm/aaaa (estilo 103).; Los resultados se ordenan por Id descendente, retornando primero el registro más reciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila739';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha de notificación Sivigila; Síntomas iniciales (fiebre, diarrea, dolor abdominal, náuseas/vómito, hipotensión); Diagnóstico COVID-19 (RT-PCR, IgM/IgG, nexo epidemiológico); Marcadores clínicos (fibrinógeno, proteína C reactiva, ferritina, dímero D, linfopenia, troponina)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila739';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCFICHA739: Cuando IDFICHANOTIFICACION coincide con el parámetro recibido, se devuelve el conjunto de filas con síntomas, marcadores y banderas formateadas, ordenadas por Id DESC.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila739';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA739', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila739';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila739';
-- GO
