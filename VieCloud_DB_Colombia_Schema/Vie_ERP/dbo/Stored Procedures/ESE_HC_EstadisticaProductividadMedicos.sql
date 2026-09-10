
-- =============================================  
-- Author:  Rafael Eduardo Patiño Cabrera  
-- ALTER date: 07/11/2019  
-- Description: sp estadistico de medicos  
-- =============================================  
CREATE PROCEDURE [dbo].[ESE_HC_EstadisticaProductividadMedicos] @CentroAtencion AS VARCHAR(200), 
                                                               @FechaInicial AS   DATE, 
                                                               @FechaFinal AS     DATE
AS
    BEGIN
        DECLARE @query AS NVARCHAR(MAX);
        DECLARE @FechaI AS VARCHAR(20)= format(@FechaInicial, 'dd/MM/yyyy');
        DECLARE @FechaF AS VARCHAR(20)= format(@FechaFinal, 'dd/MM/yyyy');
        DECLARE @columnasACTiviaddes_ISNULL NVARCHAR(MAX)= '';
        SELECT @columnasACTiviaddes_ISNULL = COALESCE(@columnasACTiviaddes_ISNULL + 'ISNULL(' + QUOTENAME(CAST(RTRIM(DESACTMED) AS VARCHAR(100))) + ',0) as [' + RTRIM(DESACTMED) + ']' + ',', '')
        FROM
        (
            SELECT DISTINCT 
                   DESACTMED
            FROM.AGACTIMED
            WHERE ACTIVICON = 0
                  AND ESTADOACT = 1
        ) AS DTM;
        SET @columnasACTiviaddes_ISNULL = LEFT(@columnasACTiviaddes_ISNULL, LEN(@columnasACTiviaddes_ISNULL) - 1);
        SELECT @columnasACTiviaddes_ISNULL;
        DECLARE @columnasACTiviaddes NVARCHAR(MAX)= '';
        SELECT @columnasACTiviaddes = COALESCE(@columnasACTiviaddes + QUOTENAME(CAST(RTRIM(DESACTMED) AS VARCHAR(100))) + ',', '')
        FROM
        (
            SELECT DISTINCT 
                   DESACTMED
            FROM.AGACTIMED
            WHERE ACTIVICON = 0
                  AND ESTADOACT = 1
        ) AS DTM;
        SET @columnasACTiviaddes = LEFT(@columnasACTiviaddes, LEN(@columnasACTiviaddes) - 1);
        DECLARE @columnasFuncionAgregado_Agrupar NVARCHAR(MAX)= '';
        SELECT @columnasFuncionAgregado_Agrupar = COALESCE(@columnasFuncionAgregado_Agrupar + 'MAX(' + QUOTENAME(CAST(RTRIM(DESACTMED) AS VARCHAR(100))) + ') as [ ' + RTRIM(DESACTMED) + ' - ' + 'CUPS: ' + RTRIM(CODSERIPS) + ' - Minutos: ' + ISNULL(DURAACTIV, 0) + ']' + ',', '')
        FROM
        (
            SELECT DISTINCT 
                   DESACTMED, 
                   CODSERIPS, 
                   DURAACTIV
            FROM.AGACTIMED
            WHERE ACTIVICON = 0
                  AND ESTADOACT = 1
        ) AS DTM;
        SET @columnasFuncionAgregado_Agrupar = LEFT(@columnasFuncionAgregado_Agrupar, LEN(@columnasFuncionAgregado_Agrupar) - 1);
        SELECT @query = CONCAT('  
     WITH Pivoted  
     AS  
     (  
       SELECT NOMMEDICO, ', @columnasACTiviaddes_ISNULL, '  
       from  
       (  
        select ACT.CODACTMED,ACT.DESACTMED,P.NOMMEDICO, count(P.CODPROSAL) as CANTIDAD   
        from .AGASICITA C INNER JOIN .INPROFSAL P on P.CODPROSAL = C.CODPROSAL INNER JOIN .AGACTIMED ACT on ACT.CODACTMED =C.CODACTMED  
        where CODCENATE = ''', @CentroAtencion, ''' AND C.CODACTMED in (select CODACTMED from .AGACTIMED where ACTIVICON  = 0 AND ESTADOACT =1) AND FORMAT(FECHORAIN,''dd/MM/yyyy'') between ''', @FechaI, ''' and ''', @FechaF, ''' AND CODESTCIT = 1  
        group by ACT.CODACTMED,ACT.DESACTMED,P.NOMMEDICO  
       ) as st  
       pivot  
       (  
        MAX(CANTIDAD)  
        FOR DESACTMED in (' + @columnasACTiviaddes + ')  
       ) as p  
     )   
     SELECT NOMMEDICO, ', @columnasFuncionAgregado_Agrupar, ' FROM Pivoted GROUP BY NOMMEDICO;   
    ');
        SELECT @query;
        EXEC (@query);
    END;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte estadístico de productividad médica por rango de fechas y centro de atención. Genera dinámicamente una tabla pivote donde cada columna representa un tipo de actividad médica activa (consultas, procedimientos) obtenida del catálogo AGACTIMED, y cada fila muestra el nombre del médico junto con la cantidad de citas atendidas (estado completado) por actividad. Cruza las citas agendadas (AGASICITA) con los profesionales de salud (INPROFSAL) y las actividades médicas (AGACTIMED), incluyendo el código CUPS y la duración en minutos de cada actividad en el encabezado de columna. Se usa para medir el desempeño y carga de trabajo de los médicos dentro de un período y sede específicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_HC_EstadisticaProductividadMedicos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_HC_EstadisticaProductividadMedicos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera dinámicamente un reporte pivotado de productividad de médicos, contando citas atendidas por cada actividad médica activa en un centro de atención y rango de fechas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_HC_EstadisticaProductividadMedicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos una actividad médica con ACTIVICON=0 y ESTADOACT=1 en AGACTIMED (de lo contrario el LEFT con LEN-1 falla).; El centro de atención debe ser válido para el filtro CODCENATE.; Las fechas deben ser convertibles a formato dd/MM/yyyy.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_HC_EstadisticaProductividadMedicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran actividades médicas no convenio (ACTIVICON=0) y activas (ESTADOACT=1).; Solo se cuentan citas con estado=1 (atendidas/efectivas).; El conteo se realiza sobre CODPROSAL agrupado por médico y actividad.; Las columnas dinámicas se etiquetan con descripción de actividad, código CUPS y duración en minutos.; La comparación de fechas se hace por string en formato dd/MM/yyyy (no por tipo DATE).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_HC_EstadisticaProductividadMedicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'productividad médica; actividad médica; cita médica; centro de atención; profesional de salud; código CUPS; duración de actividad; estado de cita', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_HC_EstadisticaProductividadMedicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dynamic_resultset: Devuelve un conjunto pivotado por NOMMEDICO con columnas dinámicas por cada DESACTMED, mostrando la cantidad (COUNT de CODPROSAL) de citas con CODESTCIT=1 dentro del rango de fechas y centro de atención.; [RETURN_RESULT] dynamic_resultset: Adicionalmente expone los strings @columnasACTiviaddes_ISNULL y @query como resultados intermedios (SELECT directos).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_HC_EstadisticaProductividadMedicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si AGACTIMED.ACTIVICON = 0 AND ESTADOACT = 1 → La actividad médica se incluye como columna dinámica en el pivot y en el filtro IN de las citas consideradas. else La actividad se excluye del reporte.; si AGASICITA.CODESTCIT = 1 → La cita se cuenta en la productividad del médico. else La cita es ignorada.; si FORMAT(FECHORAIN,''dd/MM/yyyy'') BETWEEN @FechaI AND @FechaF → Cita incluida en el conteo. else Cita excluida.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_HC_EstadisticaProductividadMedicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGACTIMED; dbo.AGASICITA; dbo.INPROFSAL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_HC_EstadisticaProductividadMedicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_HC_EstadisticaProductividadMedicos';
-- GO
