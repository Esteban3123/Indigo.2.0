
-- =============================================  
-- Author:  <Author,,Name>  
-- ALTER date: <ALTER Date,,>  
-- Description: <Description,,>  
-- =============================================  
CREATE PROCEDURE [dbo].[SP_Tecnico_Cientifica_ProceQuirurgicos]   
-- Add the parameters for the stored procedure here  
@FECHA_INICIAL DATETIME, 
@FEHCA_FINAL   DATETIME
AS
    BEGIN  
        -- SET NOCOUNT ON added to prevent extra result sets from  
        -- interfering with SELECT statements.  
        SET NOCOUNT ON;

        -- Insert statements for procedure here  
        SELECT Gra.Name AS 'GRUPO ATENCION', 
               EntA.Name AS ENTIDAD, 
               HCQ.NUMEFOLIO, 
               HCQ.IPCODPACI 'IDENTIFICACION', 
               INP.IPNOMCOMP AS 'NOMBRE PACIENTE', 
               HCQ.NUMINGRES, 
               HCQ.CODSERIPS, 
               INC.DESSERIPS, 
               HCQ.CODPROSAL AS 'COD MEDICO ESP', 
               INPR.NOMMEDICO AS 'MEDICO ESP', 
               dbo.MedicosEsp(HCQ.CODPROSAL) AS 'CODIGO ESP', 
               INES.DESESPECI, 
               dbo.Interno_Cod(HCQ.NUMEFOLIO, HCQ.IPCODPACI) AS 'COD MEDICO INTERNO', 
               DX.NOMDIAGNO AS DxPRE, 
               HCQ.FECHORINI AS 'FECHA INICIAL', 
               HCQ.FECHORFIN AS 'FEHCA FINAL', 
               dbo.Tiempo_transcurrido(HCQ.FECHORFIN, HCQ.FECHORINI) AS 'TIEMPO TRANSCURRIDO', 
               dbo.Tipo_Anestesia(HCQ.TIPOANEST) AS 'TIPO ANESTESIA', 
               dbo.Tipo_Cirugia(HCQ.URGECIRUG) AS 'TIPO CIRUGIA', 
               Q2.CODSERIPS AS Código,
               CASE
                   WHEN Q2.QXPRINCIP = 'TRUE'
                   THEN 'SI'
                   WHEN Q2.QXPRINCIP = 'FALSE'
                   THEN 'NO'
               END AS PRINCIPAL, 
               C2.DESSERIPS AS Descripción, 
               DATEDIFF(YY, INP.IPFECNACI, HCQ.FECHORINI) AS Expr1, 
               RTRIM(LTRIM(dbo.Edad2(CONVERT(VARCHAR, INP.IPFECNACI, 105), CONVERT(VARCHAR, HCQ.FECHORINI, 105)))) AS EDAD, 
               INUB.UBINOMBRE AS Barrio, 
               INUB.DEPMUNCOD AS Municipio, 
               CAST(INP.IPFECNACI AS DATE) AS Fnacimiento, 
               dbo.SexoR256(INP.IPSEXOPAC) AS sexo, 
               dbo.Etarios(INP.IPFECNACI, HCQ.FECHORINI) AS 'GRUPO ETARIO 1', 
               INP.IPTELEFON AS Tel1, 
               INP.IPTELMOVI AS Tel2, 
               HCQ.SALACIRUG AS Sala, 
               HCQ.MATERADIC AS Materiales
        FROM dbo.HCQXINFOR AS HCQ
             INNER JOIN dbo.INPACIENT AS INP ON INP.IPCODPACI = HCQ.IPCODPACI
             INNER JOIN dbo.INPROFSAL AS INPR ON INPR.CODPROSAL = HCQ.CODPROSAL
             INNER JOIN dbo.ADINGRESO AS Ing ON HCQ.IPCODPACI = Ing.IPCODPACI
                                                AND HCQ.NUMINGRES = Ing.NUMINGRES
             INNER JOIN Contract.CareGroup AS GrA ON Ing.GENCAREGROUP = GrA.Id
             INNER JOIN Contract.HealthAdministrator AS EntA ON Ing.GENCONENTITY = EntA.Id
             INNER JOIN dbo.INCUPSIPS AS INC ON INC.CODSERIPS = HCQ.CODSERIPS
             INNER JOIN dbo.INESPECIA AS INES ON INPR.CODESPEC1 = INES.CODESPECI
             INNER JOIN dbo.INDIAGNOS AS DX ON HCQ.CODDIAPRE = DX.CODDIAGNO
             INNER JOIN dbo.HCQXREALI AS Q2 ON(HCQ.NUMEFOLIO = Q2.NUMEFOLIO)
                                              AND (HCQ.IPCODPACI = Q2.IPCODPACI)
                                              AND (HCQ.NUMINGRES = Q2.NUMINGRES)
             INNER JOIN dbo.INCUPSIPS AS C2 ON C2.CODSERIPS = Q2.CODSERIPS
             INNER JOIN dbo.INUBICACI AS INUB ON INP.AUUBICACI = INUB.AUUBICACI
        WHERE(HCQ.FECHORINI BETWEEN @FECHA_INICIAL AND @FEHCA_FINAL)
        ORDER BY HCQ.IPCODPACI;
    END;