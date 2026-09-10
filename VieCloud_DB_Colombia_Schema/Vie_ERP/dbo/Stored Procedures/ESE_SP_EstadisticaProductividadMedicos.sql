
-- =============================================  
-- Author:  Rafael Eduardo Patiño Cabrera  
-- ALTER date: 07/11/2019  
-- Description: sp estadistico de medicos  
-- =============================================  
CREATE PROCEDURE [dbo].[ESE_SP_EstadisticaProductividadMedicos] @CentroAtencion AS VARCHAR(200), 
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
        --  select @columnasACTiviaddes_ISNULL  

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
        where CODCENATE = ''', @CentroAtencion, ''' AND C.CODACTMED in (select CODACTMED from .AGACTIMED where ACTIVICON  = 0 AND ESTADOACT =1) AND cast(FECREGSIS as date) between ''', @FechaI, ''' and ''', @FechaF, ''' AND CODESTCIT = 1  
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

        --select @query  
        EXEC (@query);
    END;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento estadístico que genera un reporte de productividad de médicos para un centro de atención y un rango de fechas determinado. Consulta las citas agendadas (AGASICITA) cruzadas con los profesionales de la salud (INPROFSAL) y las actividades médicas configuradas (AGACTIMED), contando cuántas citas atendidas tiene cada médico por tipo de actividad. Construye dinámicamente una tabla pivotada donde cada columna representa una actividad médica activa (no de iconos) con su nombre, código CUPS y duración en minutos, y cada fila corresponde a un médico con sus totales por actividad. Se usa para medir y comparar la productividad asistencial de los profesionales de la salud por sede, periodo y tipo de consulta o procedimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_EstadisticaProductividadMedicos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_EstadisticaProductividadMedicos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte dinámico tipo pivote con la productividad de los médicos, mostrando por cada médico la cantidad de citas atendidas agrupadas por tipo de actividad médica en un centro de atención y rango de fechas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_EstadisticaProductividadMedicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos una actividad médica activa (ESTADOACT = 1) y no de consulta (ACTIVICON = 0) en AGACTIMED para poder construir las columnas dinámicas.; El centro de atención y el rango de fechas deben ser válidos.; Las citas deben estar registradas en AGASICITA con relación a INPROFSAL y AGACTIMED.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_EstadisticaProductividadMedicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan actividades médicas activas (ESTADOACT=1) y que no sean de consulta (ACTIVICON=0).; Solo se cuentan citas con estado CODESTCIT = 1.; El conteo se filtra siempre por centro de atención y por fecha de registro (FECREGSIS) dentro del rango.; Los nulos en el pivote se reemplazan por 0 (ISNULL).; Los encabezados de columnas incluyen descripción de actividad, código CUPS (CODSERIPS) y duración en minutos (DURAACTIV).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_EstadisticaProductividadMedicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Productividad médica; Actividad médica; Cita médica; Centro de atención; Profesional de la salud; Código CUPS; Duración de actividad', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_EstadisticaProductividadMedicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A (resultset dinámico): Devuelve un resultset pivoteado con NOMMEDICO y una columna por cada actividad médica activa no-consulta, contando citas con CODESTCIT = 1 (estado cita) en el centro y rango de fechas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_EstadisticaProductividadMedicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ACTIVICON = 0 AND ESTADOACT = 1 en AGACTIMED → La actividad se incluye como columna del pivote y como filtro de las citas consideradas else La actividad se excluye del reporte; si CODESTCIT = 1 en la cita → La cita se cuenta en la productividad else La cita se ignora; si CAST(FECREGSIS AS DATE) BETWEEN @FechaInicial AND @FechaFinal y CODCENATE = @CentroAtencion → La cita entra al conteo agrupado por médico y actividad else Se descarta', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_EstadisticaProductividadMedicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'AGACTIMED; AGASICITA; INPROFSAL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_EstadisticaProductividadMedicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_EstadisticaProductividadMedicos';
-- GO
