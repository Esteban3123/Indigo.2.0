
CREATE VIEW [dbo].[TRAUMA BOTACHE]
AS
SELECT     dbo.HCHISPACA.FECHISPAC, dbo.HCHISPACA.UFUCODIGO AS Expr1, dbo.HCHISPACA.CODDIAGNO, dbo.INPACIENT.IPCODPACI, dbo.INPACIENT.IPTIPODOC, 
                      dbo.INPACIENT.IPNOMCOMP, dbo.INDIAGNOS.NOMDIAGNO, dbo.INPACIENT.IPDIRECCI, dbo.INPACIENT.IPTELEFON, dbo.INPACIENT.NUMCARPET
FROM         dbo.HCHISPACA INNER JOIN
                      dbo.INPACIENT ON dbo.HCHISPACA.IPCODPACI = dbo.INPACIENT.IPCODPACI INNER JOIN
                      dbo.INDIAGNOS ON dbo.HCHISPACA.CODDIAGNO = dbo.INDIAGNOS.CODDIAGNO
WHERE     (dbo.HCHISPACA.UFUCODIGO = '043') AND (dbo.HCHISPACA.FECHISPAC >= '2014-6-1 00:00:00') AND (dbo.HCHISPACA.FECHISPAC <= CONVERT(DATETIME, 
                      '2014-12-31 00:00:00', 102))
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de consulta puntual que cruza historias clínicas, datos demográficos de pacientes y catálogo de diagnósticos CIE-10, filtrando exclusivamente la unidad funcional ''043'' y el período comprendido entre el 1 de junio y el 31 de diciembre de 2014. Está orientada a reporting o seguimiento de casos de trauma atendidos en dicha unidad, exponiendo información de contacto y dirección del paciente junto con el diagnóstico registrado en cada nota clínica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'TRAUMA BOTACHE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'TRAUMA BOTACHE';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las atenciones clínicas de la unidad funcional ''043'' (trauma) ocurridas entre el 1 de junio y el 31 de diciembre de 2014, junto con datos del paciente y el diagnóstico asociado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'TRAUMA BOTACHE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las historias clínicas deben tener un paciente existente en el maestro de pacientes (INNER JOIN por IPCODPACI); Las historias clínicas deben tener un diagnóstico existente en el catálogo de diagnósticos (INNER JOIN por CODDIAGNO)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'TRAUMA BOTACHE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo expone atenciones de la unidad funcional con código ''043''; Solo expone atenciones registradas en el segundo semestre de 2014 (desde 1-jun-2014 hasta 31-dic-2014); Excluye historias sin paciente o sin diagnóstico catalogado por uso de INNER JOIN', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'TRAUMA BOTACHE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; historia clínica; diagnóstico; unidad funcional; atención de trauma', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'TRAUMA BOTACHE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve filas solo cuando UFUCODIGO=''043'' y FECHISPAC entre 2014-06-01 y 2014-12-31', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'TRAUMA BOTACHE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA; dbo.INPACIENT; dbo.INDIAGNOS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'TRAUMA BOTACHE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'TRAUMA BOTACHE';
GO
