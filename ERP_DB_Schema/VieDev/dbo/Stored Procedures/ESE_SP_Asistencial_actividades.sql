CREATE PROCEDURE [dbo].[ESE_SP_Asistencial_actividades] @FechaIni DATETIME, 
                                                       @FechaFin DATETIME
AS
     SELECT ACT.IPCODPACI, 
            PAC.IPPRIAPEL, 
            PAC.IPSEGAPEL, 
            PAC.IPPRINOMB, 
            PAC.IPSEGNOMB, 
            ACT.NUMINGRES, 
            ACT.CODCENATE, 
            CA.NOMCENATE, 
            ACT.UFUCODIGO, 
            UNI.UFUDESCRI, 
            ACT.CODPROSAL, 
            PRO.NOMMEDICO, 
            ACT.CODPRODUC, 
            INV.Name, 
            ACT.CANUTIPRO, 
            ACT.TIPORIGEN, 
            ACT.OBSERVACI, 
            ACT.CODACTENF, 
            AE.DESACTENF, 
            ACT.FECHAUTIL,
            CASE
                WHEN ACT.TIPORIGEN = 1
                THEN 'Medicamentos'
                WHEN ACT.TIPORIGEN = 2
                THEN 'Mezclas y liquidad '
                WHEN ACT.TIPORIGEN = 3
                THEN 'enfermeria'
            END AS 'origen'
     FROM HCHOGASIN ACT
          INNER JOIN ADCENATEN CA ON ACT.CODCENATE = CA.CODCENATE
          INNER JOIN INPACIENT PAC ON ACT.IPCODPACI = PAC.IPCODPACI
          INNER JOIN INUNIFUNC UNI ON ACT.UFUCODIGO = UNI.UFUCODIGO
          INNER JOIN INPROFSAL PRO ON ACT.CODPROSAL = PRO.CODPROSAL
          LEFT JOIN HCACTENFE AE ON ACT.CODACTENF = AE.CODACTENF
          LEFT JOIN Inventory.InventoryProduct INV ON ACT.CODPRODUC = INV.Code
     WHERE ACT.FECHAUTIL BETWEEN @FechaIni AND @FechaFin;