

CREATE VIEW [dbo].[Diagnosticos_urgenic]
AS
SELECT     TOP (50) PERCENT dbo.ADINGRESO.IFECHAING AS f_ingreso, RTRIM(dbo.INPACIENT.IPPRINOMB) + ' ' + RTRIM(dbo.INPACIENT.IPSEGNOMB) 
                      + ' ' + RTRIM(dbo.INPACIENT.IPPRIAPEL) + ' ' + RTRIM(dbo.INPACIENT.IPSEGAPEL) AS Paciente, dbo.INDIAGNOH.NUMEFOLIO AS folio, 
                      dbo.INDIAGNOH.UFUCODIGO AS area, dbo.INDIAGNOH.NUMINGRES AS ingreso, dbo.INDIAGNOH.IPCODPACI AS id_paciente, dbo.INDIAGNOH.CODDIAGNO AS dx, 
                      dbo.INDIAGNOS.NOMDIAGNO AS nom_dx, dbo.INDIAGNOH.FECDIAGNO AS f_dx
FROM         dbo.INDIAGNOH INNER JOIN
                      dbo.INDIAGNOS ON dbo.INDIAGNOH.CODDIAGNO = dbo.INDIAGNOS.CODDIAGNO INNER JOIN
                      dbo.INPACIENT ON dbo.INDIAGNOH.IPCODPACI = dbo.INPACIENT.IPCODPACI INNER JOIN
                      dbo.ADINGRESO ON dbo.INDIAGNOH.NUMINGRES = dbo.ADINGRESO.NUMINGRES
WHERE     (dbo.INDIAGNOH.CODDIAGNO = 'K350') OR
                      (dbo.INDIAGNOH.CODDIAGNO = 'K351') OR
                      (dbo.INDIAGNOH.CODDIAGNO = 'K359') OR
                      (dbo.INDIAGNOH.CODDIAGNO = 'K36X') OR
                      (dbo.INDIAGNOH.CODDIAGNO = 'K37X') OR
                      (dbo.INDIAGNOH.CODDIAGNO = 'I212') OR
                      (dbo.INDIAGNOH.CODDIAGNO = 'I219') OR
                      (dbo.INDIAGNOH.CODDIAGNO = 'I242')
ORDER BY f_dx DESC
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de consulta/reporte orientada a urgencias que filtra los 50% de registros más recientes con diagnósticos CIE-10 correspondientes a apendicitis aguda (K350, K351, K359, K36X, K37X) e infarto agudo de miocardio (I212, I219, I242). Combina datos del ingreso, paciente, diagnóstico clínico y catálogo CIE-10, presentando nombre completo del paciente, folio, área, número de ingreso y fecha del diagnóstico ordenados de forma descendente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Diagnosticos_urgenic';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Diagnosticos_urgenic';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los ingresos de pacientes con diagnósticos específicos de apendicitis, peritonitis e infarto agudo de miocardio para seguimiento de casos de urgencia.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Diagnosticos_urgenic';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de diagnósticos en INDIAGNOH vinculados a un ingreso (NUMINGRES) válido en ADINGRESO; Códigos de diagnóstico registrados deben existir en el catálogo INDIAGNOS; El paciente del diagnóstico debe existir en INPACIENT', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Diagnosticos_urgenic';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen diagnósticos cuyo código CIE-10 pertenezca al conjunto cerrado de patologías quirúrgicas/cardiovasculares de urgencia (K350, K351, K359, K36X, K37X, I212, I219, I242); El nombre del paciente se construye concatenando primer nombre, segundo nombre, primer apellido y segundo apellido con espacios; Solo se retorna la mitad superior (50%) del conjunto resultante ordenado por fecha de diagnóstico descendente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Diagnosticos_urgenic';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Diagnóstico CIE-10; Paciente; Ingreso/Admisión; Apendicitis; Peritonitis; Infarto agudo de miocardio; Urgencias; Folio de atención; Área/Unidad funcional', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Diagnosticos_urgenic';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve solo el TOP 50 PERCENT de los registros ordenados por fecha de diagnóstico descendente (más recientes primero)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Diagnosticos_urgenic';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CODDIAGNO IN (''K350'',''K351'',''K359'',''K36X'',''K37X'',''I212'',''I219'',''I242'') → Incluye el diagnóstico en el resultado (apendicitis aguda K35x, otras apendicitis K36X, peritonitis K37X, IAM de pared inferior I212, IAM no especificado I219, infarto subsecuente de otros sitios I242) else Excluye el registro', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Diagnosticos_urgenic';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INDIAGNOH; dbo.INDIAGNOS; dbo.INPACIENT; dbo.ADINGRESO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Diagnosticos_urgenic';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Diagnosticos_urgenic';
GO
