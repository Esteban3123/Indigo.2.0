
CREATE VIEW [dbo].[ATENDIDOS PEDIATRIA CHICONGUÑA]
AS
SELECT        dbo.HCHISPACA.CODESPTRA, dbo.HCHISPACA.CODDIAGNO, dbo.HCHISPACA.IPCODPACI, dbo.INPACIENT.IPPRINOMB, dbo.INPACIENT.IPSEGNOMB, 
                         dbo.INPACIENT.IPNOMCOMP, dbo.INPACIENT.CODEMPRES, dbo.HCHISPACA.FECHISPAC
FROM            dbo.HCHISPACA INNER JOIN
                         dbo.INPACIENT ON dbo.HCHISPACA.IPCODPACI = dbo.INPACIENT.IPCODPACI
WHERE        (dbo.HCHISPACA.CODDIAGNO = 'R509') AND (dbo.HCHISPACA.FECHISPAC >= CONVERT(DATETIME, '2014-12-01 00:00:00', 102))
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting que identifica pacientes pediátricos atendidos con diagnóstico CIE-10 R509 (fiebre no especificada, utilizada para vigilancia de Chikungunya) desde el 1 de diciembre de 2014. Cruza las historias clínicas con el maestro de pacientes para exponer datos de identificación del paciente, código de empresa y fecha de atención, sirviendo como herramienta de seguimiento epidemiológico del brote de Chikungunya en el servicio de Pediatría.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ATENDIDOS PEDIATRIA CHICONGUÑA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ATENDIDOS PEDIATRIA CHICONGUÑA';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las atenciones de pacientes con diagnóstico R509 (fiebre, usado como proxy de chikunguña) registradas a partir del 1 de diciembre de 2014, incluyendo datos identificatorios del paciente y su empresa/aseguradora.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ATENDIDOS PEDIATRIA CHICONGUÑA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir relación válida entre la historia clínica y el maestro de pacientes por el código de paciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ATENDIDOS PEDIATRIA CHICONGUÑA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo expone atenciones cuyo diagnóstico registrado sea exactamente ''R509''.; Solo incluye atenciones con fecha igual o posterior al 1 de diciembre de 2014.; Cada fila corresponde a una atención de un paciente existente en el maestro de pacientes (INNER JOIN obliga correspondencia).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ATENDIDOS PEDIATRIA CHICONGUÑA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; historia clínica; diagnóstico; atención pediátrica; fiebre (R509); chikunguña', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ATENDIDOS PEDIATRIA CHICONGUÑA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCHISPACA: Devuelve únicamente registros donde CODDIAGNO=''R509'' y FECHISPAC >= ''2014-12-01'', cruzados con el maestro de pacientes.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ATENDIDOS PEDIATRIA CHICONGUÑA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA; dbo.INPACIENT', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ATENDIDOS PEDIATRIA CHICONGUÑA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ATENDIDOS PEDIATRIA CHICONGUÑA';
GO
