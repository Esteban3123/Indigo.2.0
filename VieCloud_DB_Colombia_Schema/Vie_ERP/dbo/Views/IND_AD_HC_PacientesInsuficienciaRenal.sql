
CREATE VIEW [dbo].[IND_AD_HC_PacientesInsuficienciaRenal]
AS
SELECT DISTINCT 
                         RTRIM(NUMINGRES) + RTRIM(IPCODPACI) + RTRIM(IPCODPACI) AS Llave, NUMINGRES AS Ingreso, IPCODPACI AS Identificación, 
                         UFUCODIGO AS UnidadFuncional
FROM            dbo.INDIAGNOH AS ddx WITH (NOLOCK)
WHERE        (CODDIAGNO IN ('N170', 'N171', 'N172', 'N179', 'N180', 'N189', 'N19X', 'N990', 'O084', 'O904', 'P960')) AND (CODPROSAL <> 'd84') AND 
                         (FECDIAGNO >= '01/01/2016 00:00:00')
GROUP BY NUMINGRES, IPCODPACI, UFUCODIGO
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Listado de pacientes con diagnóstico de insuficiencia renal (aguda, crónica u otras patologías renales relacionadas), identificados por los códigos CIE-10 N170, N171, N172, N179, N180, N189, N19X, N990, O084, O904 y P960. Consulta los diagnósticos registrados en la historia clínica (INDIAGNOH) filtrando únicamente los registros a partir del 1 de enero de 2016 y excluyendo un profesional específico, con el fin de obtener de forma única el número de ingreso, la cédula del paciente y la unidad funcional donde fue atendido. Sirve como insumo para indicadores clínicos, seguimiento epidemiológico y reportería de pacientes renales hospitalizados o en atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'IND_AD_HC_PacientesInsuficienciaRenal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'IND_AD_HC_PacientesInsuficienciaRenal';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Identificar de manera única los pacientes con diagnóstico de insuficiencia renal (códigos CIE-10 específicos) registrados desde 2016, excluyendo cierto origen, para uso en indicadores asistenciales.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_HC_PacientesInsuficienciaRenal';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La tabla de diagnósticos debe contar con códigos CIE-10 registrados y fecha de diagnóstico válida.; Los códigos de profesional/origen (CODPROSAL) deben estar definidos para poder excluir el valor ''d84''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_HC_PacientesInsuficienciaRenal';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran diagnósticos cuyo código CIE-10 pertenezca al conjunto de insuficiencia renal: N170, N171, N172, N179, N180, N189, N19X, N990, O084, O904, P960.; Se excluyen los diagnósticos cuyo profesional/origen (CODPROSAL) sea ''d84''.; Solo se incluyen diagnósticos registrados a partir del 01/01/2016.; Los resultados son únicos por combinación de ingreso, identificación del paciente y unidad funcional (DISTINCT + GROUP BY).; Se construye una llave concatenando ingreso e identificación del paciente (duplicada) con espacios derechos recortados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_HC_PacientesInsuficienciaRenal';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Insuficiencia renal; Diagnóstico CIE-10; Ingreso hospitalario; Paciente; Unidad funcional; Historia clínica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_HC_PacientesInsuficienciaRenal';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.INDIAGNOH: Cuando CODDIAGNO está en la lista de códigos de insuficiencia renal y CODPROSAL <> ''d84'' y FECDIAGNO >= ''2016-01-01'', retorna el ingreso, identificación, unidad funcional y una llave concatenada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_HC_PacientesInsuficienciaRenal';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INDIAGNOH', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_HC_PacientesInsuficienciaRenal';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_HC_PacientesInsuficienciaRenal';
GO
