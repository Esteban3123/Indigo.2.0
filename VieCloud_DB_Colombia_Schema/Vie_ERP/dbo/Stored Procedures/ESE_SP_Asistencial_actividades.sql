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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de actividades asistenciales de enfermería registradas en un rango de fechas. Consolida los insumos, medicamentos, mezclas y actividades de enfermería consumidos durante la atención hospitalaria de cada paciente, cruzando el número de ingreso y la cédula del paciente con su nombre completo, el centro de atención, la unidad funcional (servicio o sala), el profesional de salud responsable, el producto del inventario utilizado y la descripción de la actividad de enfermería. Clasifica cada registro según su origen: medicamentos, mezclas y líquidos, o enfermería. Se usa para supervisión asistencial, seguimiento del consumo de insumos por paciente y soporte a auditorías clínicas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Asistencial_actividades';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Asistencial_actividades';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Listar las actividades asistenciales (gastos/insumos y actividades de enfermería) registradas en un rango de fechas, enriquecidas con datos del paciente, centro de atención, unidad funcional, profesional, producto y clasificación de origen.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_actividades';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe proveer un rango de fechas (inicio y fin) para filtrar las actividades por fecha de utilización.; Deben existir los catálogos referenciados (centros de atención, pacientes, unidades funcionales, profesionales de salud) con las claves usadas en las actividades.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_actividades';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan actividades cuya fecha de utilización esté dentro del rango solicitado.; El reporte exige que la actividad tenga centro de atención, paciente, unidad funcional y profesional de salud válidos (INNER JOIN).; La actividad de enfermería y el producto de inventario son opcionales en la actividad (LEFT JOIN).; El origen textual solo cubre los valores 1, 2 y 3; cualquier otro valor produce origen NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_actividades';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Centro de atención; Unidad funcional; Profesional de la salud; Producto/Inventario (medicamentos, mezclas); Actividad de enfermería; Ingreso hospitalario; Hoja de gasto/insumos asistenciales', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_actividades';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCHOGASIN: Devuelve un conjunto de resultados con las actividades asistenciales cuya FECHAUTIL está entre el rango de fechas proporcionado, incluyendo la traducción del tipo de origen (1=Medicamentos, 2=Mezclas y liquidad, 3=enfermeria).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_actividades';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Tipo de origen de la actividad asistencial = 1 → Se etiqueta el origen como ''Medicamentos''; si Tipo de origen de la actividad asistencial = 2 → Se etiqueta el origen como ''Mezclas y liquidad''; si Tipo de origen de la actividad asistencial = 3 → Se etiqueta el origen como ''enfermeria''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_actividades';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHOGASIN; dbo.ADCENATEN; dbo.INPACIENT; dbo.INUNIFUNC; dbo.INPROFSAL; dbo.HCACTENFE; Inventory.InventoryProduct', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_actividades';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_actividades';
-- GO
