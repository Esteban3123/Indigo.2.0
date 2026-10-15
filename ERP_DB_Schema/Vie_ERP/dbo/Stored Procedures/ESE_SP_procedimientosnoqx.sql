-- =============================================
-- Author:		<Author,,William Suaza>
-- Create date: <Create Date, 10/05/2019,>
-- Description:	<Description, Todas las HC de control perinatal ID = 11,>
-- =============================================
CREATE PROCEDURE [dbo].[ESE_SP_procedimientosnoqx] @FechaIni DATETIME, 
                                                  @FechaFin DATETIME
AS
    BEGIN
        SELECT QX.IPCODPACI, 
               PAC.IPPRIAPEL, 
               PAC.IPSEGAPEL, 
               PAC.IPPRINOMB, 
               PAC.IPSEGNOMB, 
               PAC.IPFECNACI, 
               QX.NUMINGRES, 
               QX.CODCENATE, 
               QX.UFUCODIGO, 
               QX.CODPROSAL, 
               MED.NOMMEDICO, 
               QX.FECORDMED, 
               QX.CODSERIPS
        FROM HCORDPRON qx
             INNER JOIN INPACIENT PAC ON qx.IPCODPACI = PAC.IPCODPACI
             INNER JOIN INPROFSAL MED ON QX.CODPROSAL = MED.CODPROSAL
        WHERE qx.FECORDMED BETWEEN @FechaIni AND @FechaFin;
    END;