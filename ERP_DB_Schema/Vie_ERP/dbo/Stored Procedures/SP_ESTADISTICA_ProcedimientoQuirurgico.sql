
-- =============================================  
-- Author:  JRodriguez  
-- ALTER date:   
-- Description:   
-- =============================================  
CREATE PROCEDURE [dbo].[SP_ESTADISTICA_ProcedimientoQuirurgico]  
-- Add the parameters for the stored procedure here  
@FechaI DATE, 
@FechaF DATE
AS
    BEGIN  
        -- SET NOCOUNT ON added to prevent extra result sets from  
        -- interfering with SELECT statements.  
        SET NOCOUNT ON;

        -- Insert statements for procedure here  
        SELECT 'TIPO DE IDENTIFICACION' = CASE Pa.IPTIPODOC
                                              WHEN '1'
                                              THEN 'Cédula de Ciudadanía'
                                              WHEN '2'
                                              THEN 'Cédula de Extranjería'
                                              WHEN '3'
                                              THEN 'Tarjeta de Identidad'
                                              WHEN '4'
                                              THEN 'Registro Civil'
                                              WHEN '5'
                                              THEN 'Pasporte'
                                              WHEN '6'
                                              THEN 'Adulto Sin Identificación'
                                              ELSE 'Menor Sin Identificación'
                                          END, 
               Q.IPCODPACI AS 'NUMERO DE IDENTIFICACION', 
               CAST(Pa.IPFECNACI AS DATE) AS 'FECHA DE NACIMINETO', 
               'SEXO PACIENTE' = CASE Pa.IPSEXOPAC
                                     WHEN '1'
                                     THEN 'HOMBRE'
                                     WHEN '2'
                                     THEN 'MUJER'
                                     ELSE 'INDEFINIDO'
                                 END, 
               Pa.IPPRIAPEL AS 'PRIMER APELLIDO', 
               Pa.IPSEGAPEL AS 'SEGUNDO APELLIDO', 
               Pa.IPPRINOMB AS 'PRIMER NOMBRE', 
               Pa.IPSEGNOMB AS 'SEGUNDO NOMBRE', 
               Q.CODSERIPS, 
               GrA.Name AS GrupoAtencion, 
               EntA.Name AS Entidad, 
               INUBICACI.DEPMUNCOD AS Municipio, 
               Q2.CODSERIPS AS 'CODIGO DE PROCEDIMIENTO', 
               CAST(Q.FECHORINI AS DATE) AS 'FECHA DE PROCEDIMIENTO'
        FROM HCQXINFOR AS Q
             INNER JOIN INPROFSAL AS P ON P.CODPROSAL = Q.CODPROSAL
             INNER JOIN INDIAGNOS AS DX2 ON Q.CODDIAPRE = DX2.CODDIAGNO
             INNER JOIN INDIAGNOS AS DX ON Q.CODDIAPRE = DX.CODDIAGNO
             INNER JOIN INESPECIA AS E ON P.CODESPEC1 = E.CODESPECI
             INNER JOIN INCUPSIPS AS C ON C.CODSERIPS = Q.CODSERIPS
             INNER JOIN HCQXREALI AS Q2 ON Q.NUMEFOLIO = Q2.NUMEFOLIO
                                           AND Q.IPCODPACI = Q2.IPCODPACI
                                           AND Q.NUMINGRES = Q2.NUMINGRES
             INNER JOIN INCUPSIPS AS C2 ON C2.CODSERIPS = Q2.CODSERIPS
             INNER JOIN INPACIENT AS Pa ON Q.IPCODPACI = Pa.IPCODPACI
             INNER JOIN INUBICACI ON Pa.AUUBICACI = INUBICACI.AUUBICACI
             INNER JOIN ADINGRESO AS Ing ON Q.IPCODPACI = Ing.IPCODPACI
                                            AND Q.NUMINGRES = Ing.NUMINGRES
             INNER JOIN Contract.CareGroup AS GrA ON Ing.GENCAREGROUP = GrA.Id
             INNER JOIN Contract.HealthAdministrator AS EntA ON Ing.GENCONENTITY = EntA.Id
        WHERE(Q.FECHORINI BETWEEN @FechaI AND @FechaF)
        ORDER BY Q.FECHORINI;
    END;