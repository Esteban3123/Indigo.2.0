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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle completo de los paquetes quirúrgicos configurados en el sistema de agendamiento. Muestra, para cada paquete activo de tipo cirugía o intervención (donde la actividad médica está marcada como activa con ícono), las actividades médicas asociadas con su código y descripción, los procedimientos o servicios CUPS/IPS que las componen, y los productos farmacéuticos o insumos incluidos en el paquete con su cantidad definida. Integra la definición del paquete (AGPAQUETES), sus ítems de producto (AGPAQUETESD), el catálogo de medicamentos e insumos (IHLISTPRO), las actividades médicas agendables (AGACTIMED), los servicios CUPS asociados a cada actividad dentro del paquete (AGACTPAQUETES) y el catálogo de servicios IPS/CUPS (INCUPSIPS). Sirve para consultar la composición completa de paquetes quirúrgicos: qué procedimientos, actividades médicas, medicamentos e insumos incluye cada paquete, útil para cotización, planeación quirúrgica y gestión de paquetes de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Paquetes_Cirugía';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Paquetes_Cirugía';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone la composición de paquetes quirúrgicos relacionando actividades médicas activas con sus procedimientos CUPS y los productos/insumos asignados con su cantidad.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Paquetes_Cirugía';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las actividades médicas, paquetes, productos, servicios CUPS y relaciones actividad-paquete deben existir y estar correctamente vinculados por sus claves para aparecer en el resultado.; El paquete debe tener al menos un detalle de producto y al menos una actividad asociada con su correspondiente CUPS.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Paquetes_Cirugía';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen paquetes cuya actividad médica esté marcada como activa.; Cada fila representa un producto perteneciente a un paquete que está asociado a una actividad médica con su procedimiento CUPS.; Se eliminan duplicados mediante DISTINCT, garantizando combinaciones únicas de actividad-procedimiento-paquete-producto-cantidad.; Solo se muestran productos, paquetes, actividades y CUPS que existen y están correctamente referenciados (INNER JOIN).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Paquetes_Cirugía';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paquete quirúrgico; Actividad médica; Procedimiento CUPS; Producto/insumo médico; Cantidad de producto en paquete', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Paquetes_Cirugía';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve filas distintas con actividad, procedimiento (CUPS), paquete, producto y cantidad únicamente cuando la actividad médica está activa (ACTIVICON = 1).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Paquetes_Cirugía';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ACT.ACTIVICON = 1 (actividad médica activa) → Se incluye la combinación actividad-paquete-producto en el resultado else Se excluye del resultado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Paquetes_Cirugía';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGPAQUETESD; dbo.AGPAQUETES; dbo.IHLISTPRO; dbo.AGACTPAQUETES; dbo.AGACTIMED; dbo.INCUPSIPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Paquetes_Cirugía';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Paquetes_Cirugía';
GO
