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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de medicamentos formulados a pacientes en un rango de fechas. Combina la prescripción farmacéutica (HCFARMEPD) con los datos del paciente, el nombre del medicamento (desde el catálogo de inventario y la clasificación ATC), la cantidad solicitada, el centro de atención, la unidad funcional y el profesional de salud que realizó la formulación. Se utiliza para auditoría asistencial, seguimiento de prescripciones médicas y reportes de consumo farmacológico por sede y médico, filtrando por fecha de inicio de la dosis entre @FechaIni y @FechaFin.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Asistencial_med_formulados';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Asistencial_med_formulados';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los medicamentos formulados a pacientes en un rango de fechas, enriquecidos con datos del paciente, producto, clasificación ATC, centro de atención y profesional prescriptor.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_med_formulados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe proporcionar un rango de fechas (inicio y fin) para filtrar las formulaciones; Las tablas maestras de pacientes, centros de atención y profesionales de salud deben contener los códigos referenciados (INNER JOIN obligatorio)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_med_formulados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan formulaciones cuyo paciente exista en INPACIENT, cuyo centro exista en ADCENATEN y cuyo profesional exista en INPROFSAL (INNER JOIN); El producto y su clasificación ATC son opcionales: si no existen, la formulación igualmente se devuelve (LEFT JOIN); Se aplica conversión de collation SQL_Latin1_General_CP1_CI_AS al cruzar el código de producto contra el catálogo ATC para evitar conflictos de intercalación; El filtro de fechas es inclusivo en ambos extremos (BETWEEN)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_med_formulados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Medicamentos formulados; Paciente; Ingreso; Producto de inventario; Clasificación ATC; Centro de atención; Profesional de salud (médico prescriptor); Unidad funcional', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_med_formulados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCFARMEPD: Cuando FECINIDOS está entre @FechaIni y @FechaFin, retorna las formulaciones de medicamentos con sus datos asociados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_med_formulados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFARMEPD; dbo.INPACIENT; Inventory.InventoryProduct; Inventory.ATC; dbo.ADCENATEN; dbo.INPROFSAL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_med_formulados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_med_formulados';
-- GO
