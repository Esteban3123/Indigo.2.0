

CREATE VIEW [dbo].[ViewCamasHSJ]
AS
SELECT CEN.NOMCENATE 'CENTRO DE ATENCION', CAM.UFUCODIGO 'COD.UF',FUN.UFUDESCRI 'UNIDAD FUNCIONAL',CAM.NUMCAMHOS 'COD. CAMA', 
CASE ESTADCAMA WHEN 1 THEN 'Libre'
WHEN 2 THEN 'Asignada'
WHEN 3 THEN 'Inactiva'
WHEN 4 THEN 'En Mantenimiento'
WHEN 5 THEN 'En Aislamiento'
WHEN 6 THEN 'Reservada sin Confirmar'
WHEN 7 THEN 'Reservada Confirmada' END 'ESTADO',

CASE CAM.UFUCODIGO WHEN 1111001   THEN 'Urgencias-Observacion' 
WHEN 1114001    THEN 'Salas de Cirugía'
ELSE 'Hospitalización' END 'TIPO DE CAMA'

FROM DBO.CHCAMASHO CAM

INNER JOIN DBO.INUNIFUNC FUN ON FUN.UFUCODIGO=CAM.UFUCODIGO
LEFT JOIN DBO.ADCENATEN CEN ON CEN.CODCENATE=CAM.CODCENATE
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que muestra el estado actual de todas las camas hospitalarias del HSJ, combinando información del maestro de camas (CHCAMASHO), las unidades funcionales (INUNIFUNC) y los centros de atención (ADCENATEN). Para cada cama entrega: el nombre del centro o sede, el código y nombre de la unidad funcional (sala o servicio), el número de cama, el estado legible (Libre, Asignada, Inactiva, En Mantenimiento, En Aislamiento, Reservada sin Confirmar, Reservada Confirmada) y la clasificación del tipo de cama según la unidad (Urgencias-Observación, Salas de Cirugía u Hospitalización). Se utiliza para el censo de camas, gestión de disponibilidad hospitalaria y consulta operativa de ocupación en tiempo real.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewCamasHSJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewCamasHSJ';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el inventario de camas hospitalarias con su centro de atención, unidad funcional, estado operativo y tipo de cama clasificado según la unidad funcional.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewCamasHSJ';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada cama debe tener una unidad funcional existente (INNER JOIN con INUNIFUNC).; El centro de atención es opcional (LEFT JOIN con ADCENATEN).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewCamasHSJ';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El estado de la cama se restringe a 7 valores codificados (1 a 7).; El tipo de cama se determina exclusivamente por el código de unidad funcional: 1111001=Urgencias-Observación, 1114001=Salas de Cirugía, resto=Hospitalización.; Una cama sin unidad funcional válida no aparece en la vista.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewCamasHSJ';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cama hospitalaria; Centro de atención; Unidad funcional; Estado de cama; Urgencias-Observación; Salas de Cirugía; Hospitalización; Aislamiento; Reserva de cama', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewCamasHSJ';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.CHCAMASHO: Devuelve una fila por cada cama registrada cruzada con su unidad funcional y centro de atención.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewCamasHSJ';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ESTADCAMA = 1 → Estado ''Libre''; si ESTADCAMA = 2 → Estado ''Asignada''; si ESTADCAMA = 3 → Estado ''Inactiva''; si ESTADCAMA = 4 → Estado ''En Mantenimiento''; si ESTADCAMA = 5 → Estado ''En Aislamiento''; si ESTADCAMA = 6 → Estado ''Reservada sin Confirmar''; si ESTADCAMA = 7 → Estado ''Reservada Confirmada''; si UFUCODIGO = 1111001 → Tipo de cama ''Urgencias-Observacion''; si UFUCODIGO = 1114001 → Tipo de cama ''Salas de Cirugía'' else Tipo de cama ''Hospitalización'' para cualquier otra unidad funcional', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewCamasHSJ';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'DBO.CHCAMASHO; DBO.INUNIFUNC; DBO.ADCENATEN', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewCamasHSJ';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewCamasHSJ';
GO
