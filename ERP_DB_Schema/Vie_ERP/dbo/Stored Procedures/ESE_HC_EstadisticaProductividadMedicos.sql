
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