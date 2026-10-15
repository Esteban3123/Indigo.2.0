-- =============================================  
-- Author:  <Author,,William Suaza>  
-- ALTER date: <ALTER Date, 09/05/2019,>  
-- Description: <Description, HC del modelo de riesgo Cardiovascular de la ESE sin variables parametrizadas,>  
-- =============================================  
CREATE PROCEDURE [dbo].[ESE_SP_HC_Riesgo_Cardiovascular]
AS
    BEGIN
        SELECT HC.IPCODPACI AS Doc_Paciente, 
               I.IPNOMCOMP AS Nombre_Paciente, 
               HC.NUMINGRES AS Ingreso, 
               HC.NUMEFOLIO AS Folio, 
               HC.CODCENATE AS Cod_Centro_Atencion, 
               CA.NOMCENATE AS Centro_Atencion, 
               HC.CODPROSAL AS Cod_Profesional, 
               PR.NOMMEDICO AS Profesional, 
               HC.CODESPTRA AS Cod_Especialidad, 
               ES.DESESPECI AS Especialidad, 
               HC.FECHISPAC AS Fec_HC, 
               HC.CODDIAGNO AS Diagnostico_Principal
        FROM.HCHISPACA AS HC
            JOIN.INPACIENT AS I WITH(NOLOCK) ON I.IPCODPACI = HC.IPCODPACI
            JOIN.ADCENATEN AS CA WITH(NOLOCK) ON CA.CODCENATE = HC.CODCENATE
            JOIN.INPROFSAL AS PR WITH(NOLOCK) ON PR.CODPROSAL = HC.CODPROSAL
            JOIN.INESPECIA AS ES WITH(NOLOCK) ON ES.CODESPECI = HC.CODESPTRA
        WHERE IDMODELOHC = 14
              AND FECHISPAC BETWEEN '01/04/2019' AND '30/04/2019';
    END;