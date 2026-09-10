CREATE VIEW [dbo].[I_HC_OrdenesLaboratorioCovid]
AS
SELECT O.FECORDMED AS FechaOrden, O.IPCODPACI AS Identificación, O.NUMINGRES AS Ingreso, P.IPNOMCOMP AS Paciente, O.CODSERIPS AS CodigoServicio, 
             CASE O.ESTSERIPS WHEN 1 THEN 'Solicitado' WHEN 2 THEN 'MuestraRecolectada' WHEN 3 THEN 'ResultadoEntregado' WHEN 4 THEN 'ExamenInterpretado' WHEN 5 THEN 'Remitido' WHEN 6 THEN 'Anulado' WHEN 7 THEN 'Extramural' WHEN 8 THEN 'MuestraParcial' END AS Estado, 
             U.UFUDESCRI AS Unidad, GA.Name AS GrupoAtención, M.NOMMEDICO AS MedicoSolicita
FROM   dbo.HCORDLABO AS O INNER JOIN
             dbo.INPACIENT AS P ON O.IPCODPACI = P.IPCODPACI AND CONVERT(nvarchar(10), O.FECORDMED, 20) > CONVERT(nvarchar(10), GETDATE() - 182, 20) RIGHT OUTER JOIN
             dbo.INUNIFUNC AS U ON O.UFUCODIGO = U.UFUCODIGO INNER JOIN
             dbo.INPROFSAL AS M ON O.CODPROSAL = M.CODPROSAL INNER JOIN
             dbo.ADINGRESO AS H ON O.NUMINGRES = H.NUMINGRES INNER JOIN
             Contract.CareGroup AS GA ON H.GENCAREGROUP = GA.Id
WHERE (O.CODSERIPS = '908856')
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Órdenes de laboratorio de COVID-19 (código de servicio CUPS 908856) solicitadas en los últimos 6 meses, que consolida en una sola consulta los datos de la orden clínica (fecha, estado del examen, código de servicio), la identificación y nombre completo del paciente, el número de ingreso o episodio de atención, la unidad funcional o servicio donde se generó la orden, el grupo de atención del contrato vigente y el médico que la solicitó. Integra las órdenes de laboratorio (HCORDLABO) con el maestro de pacientes (INPACIENT), el catálogo de unidades funcionales (INUNIFUNC), el maestro de profesionales de salud (INPROFSAL), los ingresos o admisiones (ADINGRESO) y los grupos de atención contractuales (CareGroup). El estado de cada orden se presenta en texto legible: Solicitado, MuestraRecolectada, ResultadoEntregado, ExamenInterpretado, Remitido, Anulado, Extramural o MuestraParcial. Sirve para seguimiento, control epidemiológico y reportería operativa de pruebas COVID en la institución.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'I_HC_OrdenesLaboratorioCovid';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'I_HC_OrdenesLaboratorioCovid';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las órdenes de laboratorio del examen COVID (código IPS 908856) registradas en los últimos ~182 días, junto con datos del paciente, unidad funcional, médico solicitante y grupo de atención del ingreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'I_HC_OrdenesLaboratorioCovid';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La orden debe estar asociada a un paciente existente en INPACIENT; La orden debe tener un profesional solicitante válido en INPROFSAL; La orden debe estar vinculada a un ingreso existente en ADINGRESO; El ingreso debe tener un grupo de atención (CareGroup) válido en Contract.CareGroup', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'I_HC_OrdenesLaboratorioCovid';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen órdenes cuyo código de servicio IPS sea ''908856'' (prueba COVID); Solo se incluyen órdenes con fecha posterior a hoy menos 182 días (~6 meses); La unidad funcional se vincula vía RIGHT OUTER JOIN, por lo que pueden aparecer unidades sin órdenes asociadas; El estado de la orden siempre se entrega como texto descriptivo, no como código numérico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'I_HC_OrdenesLaboratorioCovid';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de laboratorio; Paciente; Ingreso/Admisión; Unidad funcional; Médico solicitante; Grupo de atención; Código de servicio IPS; Estado de orden de laboratorio; Prueba COVID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'I_HC_OrdenesLaboratorioCovid';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCORDLABO: Cuando CODSERIPS=''908856'' y FECORDMED > GETDATE()-182, se retorna la orden con su estado traducido (1..8) a etiquetas textuales (Solicitado, MuestraRecolectada, ResultadoEntregado, ExamenInterpretado, Remitido, Anulado, Extramural, MuestraParcial)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'I_HC_OrdenesLaboratorioCovid';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ESTSERIPS = 1 → Estado = ''Solicitado''; si ESTSERIPS = 2 → Estado = ''MuestraRecolectada''; si ESTSERIPS = 3 → Estado = ''ResultadoEntregado''; si ESTSERIPS = 4 → Estado = ''ExamenInterpretado''; si ESTSERIPS = 5 → Estado = ''Remitido''; si ESTSERIPS = 6 → Estado = ''Anulado''; si ESTSERIPS = 7 → Estado = ''Extramural''; si ESTSERIPS = 8 → Estado = ''MuestraParcial''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'I_HC_OrdenesLaboratorioCovid';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDLABO; dbo.INPACIENT; dbo.INUNIFUNC; dbo.INPROFSAL; dbo.ADINGRESO; Contract.CareGroup', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'I_HC_OrdenesLaboratorioCovid';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'I_HC_OrdenesLaboratorioCovid';
GO
