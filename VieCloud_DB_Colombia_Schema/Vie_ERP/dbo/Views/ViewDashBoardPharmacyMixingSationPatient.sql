

CREATE VIEW [dbo].[ViewDashBoardPharmacyMixingSationPatient]
AS
	SELECT  CONCAT(trim(A.IPCODPACI), ' - ', A.ORDTRANUE,'-',A.NUMINGRES ) Id,
			0 AS SelectOption, 
			A.IPCODPACI AS CodigoPaciente,			
            A.CODCENATE AS CodigoCentroAtencion,
			RTRIM(B.IPNOMCOMP) AS NombrePaciente, 
			A.NUMINGRES AS Ingreso, 
			CASE 
               WHEN ORDTRANUE = 1 THEN 'Tratamiento nuevo'
               WHEN ORDTRANUE = 2 THEN 'Tratamiento antiguo'
               WHEN ORDTRANUE = 3 THEN 'Código azul' end AS Tipo, 
			CAST('' AS bit) AS Impresion, 
			--Case When A.MEDICAMENTOVALIDADO Is Null Or A.MEDICAMENTOVALIDADO = 0 Then Cast(0 As Bit) Else Cast(1 As Bit) End As Validado,			
			B.IPFECNACI BirthDay
	FROM [dbo].[ViewPharmacyMixingStationWithOncology] AS A  
	JOIN dbo.INPACIENT AS B  ON A.IPCODPACI = B.IPCODPACI	
	WHERE A.RoutingMP = 1 
	GROUP BY A.NUMINGRES , A.IPCODPACI, B.IPNOMCOMP, A.ORDTRANUE, B.IPFECNACI, A.CODCENATE
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Panel de control (dashboard) para la estación de mezclas de farmacia con oncología, que lista los pacientes con órdenes activas de preparación de mezclas. Combina los datos de órdenes de mezcla oncológica (ViewPharmacyMixingStationWithOncology) con la información demográfica del paciente como nombre completo y fecha de nacimiento (INPACIENT), filtrando únicamente las órdenes enrutadas a la estación de mezclas (RoutingMP=1). Para cada paciente muestra su cédula o código de identificación, nombre, número de ingreso, centro de atención, fecha de nacimiento y el tipo de tratamiento (Tratamiento nuevo, Tratamiento antiguo o Código azul). Es utilizada por el módulo de farmacia para visualizar en tiempo real qué pacientes tienen preparaciones de mezclas pendientes o en curso, distinguiendo si es un tratamiento nuevo, continuación o emergencia (código azul).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewDashBoardPharmacyMixingSationPatient';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewDashBoardPharmacyMixingSationPatient';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone la lista de pacientes con preparaciones de mezclas farmacéuticas pendientes de ruteo en la central de mezclas, enriquecida con datos demográficos, para alimentar el dashboard de la estación de mezclas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacyMixingSationPatient';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros en ViewPharmacyMixingStationWithOncology con RoutingMP = 1.; Cada paciente referenciado debe existir en INPACIENT (JOIN interno por IPCODPACI).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacyMixingSationPatient';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen órdenes con marca de ruteo a central de mezclas (RoutingMP = 1).; El identificador de la fila combina código de paciente, tipo de orden e ingreso, garantizando unicidad por esos tres atributos.; El campo Impresion siempre se devuelve como bit vacío/falso (no refleja estado real de impresión).; Solo se exponen pacientes con correspondencia en el maestro INPACIENT (JOIN interno).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacyMixingSationPatient';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Central de mezclas farmacéuticas; Oncología; Tratamiento nuevo/antiguo; Código azul; Centro de atención', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacyMixingSationPatient';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.ViewDashBoardPharmacyMixingSationPatient: Devuelve únicamente filas donde RoutingMP = 1, agrupadas por ingreso, paciente, tipo de orden, fecha de nacimiento y centro de atención.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacyMixingSationPatient';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ORDTRANUE = 1 → Clasifica el registro como ''Tratamiento nuevo''.; si ORDTRANUE = 2 → Clasifica el registro como ''Tratamiento antiguo''.; si ORDTRANUE = 3 → Clasifica el registro como ''Código azul''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacyMixingSationPatient';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ViewPharmacyMixingStationWithOncology; dbo.INPACIENT', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacyMixingSationPatient';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacyMixingSationPatient';
GO
