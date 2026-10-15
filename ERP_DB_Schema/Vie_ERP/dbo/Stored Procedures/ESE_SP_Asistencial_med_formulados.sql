-- =============================================  
-- Author:  <Author,,yeny nunez>  
-- ALTER date: <ALTER Date, 13/05/2019,>  
-- Description: <Description, Medicamentos formulados >  
-- =============================================  
CREATE PROCEDURE [dbo].[ESE_SP_Asistencial_med_formulados] @FechaIni DATETIME, 
                                                          @FechaFin DATETIME
AS
    BEGIN
        SELECT MED.IPCODPACI, 
               PA.IPNOMCOMP, 
               MED.NUMINGRES, 
               MED.FECINIDOS, 
               MED.CODPRODUC, 
               PRO.Name, 
               ATC.Name, 
               med.CANPEDPRO, 
               med.CODCENATE, 
               CA.NOMCENATE, 
               MED.UFUCODIGO, 
               MED.CODPROSAL, 
               MS.NOMMEDICO
        FROM HCFARMEPD MED
             INNER JOIN INPACIENT PA ON MED.IPCODPACI = PA.IPCODPACI
             LEFT JOIN Inventory.InventoryProduct PRO ON MED.CODPRODUC = PRO.Code
             LEFT JOIN Inventory.ATC ATC ON MED.CODPRODUC COLLATE SQL_Latin1_General_CP1_CI_AS = ATC.Code COLLATE SQL_Latin1_General_CP1_CI_AS
             INNER JOIN ADCENATEN CA ON MED.CODCENATE = CA.CODCENATE
             INNER JOIN INPROFSAL MS ON MED.CODPROSAL = MS.CODPROSAL
        WHERE MED.FECINIDOS BETWEEN @FechaIni AND @FechaFin;
    END;