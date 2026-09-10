
CREATE VIEW [dbo].[Auditoria_InterpretacionParaclinicos]
AS
SELECT     dbo.HCORDLABO.AUTO, dbo.HCORDLABO.IDETIPHIS, dbo.HCORDLABO.NUMEFOLIO, dbo.HCORDLABO.IPCODPACI, dbo.HCORDLABO.NUMINGRES, 
                      dbo.HCORDLABO.CODCENATE, dbo.HCORDLABO.UFUCODIGO, dbo.HCORDLABO.CODPROSAL, dbo.INPROFSAL.NOMMEDICO, dbo.HCORDLABO.FECORDMED, 
                      dbo.HCORDLABO.CODSERIPS, dbo.HCORDLABO.CANSERIPS, dbo.HCORDLABO.OBSSERIPS, dbo.HCORDLABO.PRISERIPS, dbo.HCORDLABO.ESTSERIPS, 
                      dbo.HCORDLABO.MANEXTPRO, dbo.HCORDLABO.CODDIAGNO, dbo.HCORDLABO.INTERPRET, dbo.HCORDLABO.CODPROINT, dbo.HCORDLABO.NUMFOLINT, 
                      dbo.HCORDLABO.SERREAINT, dbo.HCORDLABO.INDAUDFOR, dbo.HCORDLABO.ESTALELAB, dbo.HCORDLABO.FECRECMUE, dbo.HCORDLABO.NOMARCLAB, 
                      dbo.HCORDLABO.CONCURRE, dbo.HCORDLABO.USURECMUE
FROM         dbo.HCORDLABO INNER JOIN
                      dbo.INPROFSAL ON dbo.HCORDLABO.CODPROSAL = dbo.HCORDLABO.CODPROSAL
WHERE     (LEN(dbo.HCORDLABO.INTERPRET) < 2)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de auditoría que identifica órdenes de laboratorio clínico (paraclínicos) que carecen de interpretación médica registrada o la tienen incompleta (menos de 2 caracteres). Combina las órdenes de laboratorio de la historia clínica (HCORDLABO) con el maestro de profesionales de la salud (INPROFSAL) para mostrar, junto a cada examen solicitado, el nombre del médico ordenador, el paciente, el ingreso, el diagnóstico asociado, el estado del examen y datos de la muestra. Sirve para controlar y auditar el cumplimiento del proceso de interpretación de resultados paraclínicos por parte de los profesionales de salud, permitiendo detectar exámenes con resultado sin interpretación formal documentada en la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Auditoria_InterpretacionParaclinicos';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Auditoria_InterpretacionParaclinicos';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las órdenes de laboratorio clínico cuya interpretación por el profesional aún no ha sido registrada o es insuficiente, para efectos de auditoría.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Auditoria_InterpretacionParaclinicos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las órdenes de laboratorio deben existir en HCORDLABO con un código de profesional asociado; Debe existir el profesional de la salud en INPROFSAL para obtener su nombre', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Auditoria_InterpretacionParaclinicos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen órdenes de laboratorio sin interpretación clínica suficiente (campo INTERPRET con menos de 2 caracteres); Cada orden retornada incluye los datos del profesional solicitante mediante cruce con el maestro de profesionales', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Auditoria_InterpretacionParaclinicos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Auditoría clínica; Órdenes de laboratorio; Interpretación de paraclínicos; Profesional de la salud; Historia clínica; Diagnóstico; Folio de orden; Centro de atención', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Auditoria_InterpretacionParaclinicos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCORDLABO: Devuelve solo órdenes de laboratorio donde LEN(INTERPRET) < 2, es decir, sin interpretación registrada o con texto trivial', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Auditoria_InterpretacionParaclinicos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si LEN(HCORDLABO.INTERPRET) < 2 → La orden de laboratorio se incluye en el resultado de auditoría como pendiente de interpretación else La orden no aparece en la vista (se considera interpretada)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Auditoria_InterpretacionParaclinicos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDLABO; dbo.INPROFSAL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Auditoria_InterpretacionParaclinicos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Auditoria_InterpretacionParaclinicos';
GO
