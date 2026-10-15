CREATE VIEW [dbo].[Paquetes_Cirugía]
AS
     SELECT DISTINCT 
            ACT.CODACTMED AS Código_Actividad, 
            ACT.DESACTMED AS Descripción_ACtividad, 
            PA.CODSERIPS AS Código_Procedimiento, 
            CUP.DESSERIPS AS Descripción_Procedimiento, 
            ADP.[IDAGPAQUETE] AS Código_Paquete, 
            AP.NOMBRE AS Nombre_Paquete, 
            ADP.[CODPRODUC] AS Código_Producto, 
            PRO.DESPRODUC AS Nombre_Producto, 
            [CANTIDAD] AS Cantidad_Producto
     FROM [dbo].[AGPAQUETESD] ADP
          INNER JOIN [dbo].[AGPAQUETES] AP ON ADP.IDAGPAQUETE = AP.ID
          INNER JOIN [dbo].[IHLISTPRO] PRO ON ADP.CODPRODUC = PRO.CODPRODUC
          INNER JOIN [dbo].[AGACTPAQUETES] PA ON PA.IDAGPAQUETE = AP.ID
          INNER JOIN [dbo].[AGACTIMED] ACT ON ACT.CODACTMED = PA.CODACTMED
          INNER JOIN [dbo].[INCUPSIPS] CUP ON CUP.CODSERIPS = PA.CODSERIPS
     WHERE ACT.ACTIVICON = 1;