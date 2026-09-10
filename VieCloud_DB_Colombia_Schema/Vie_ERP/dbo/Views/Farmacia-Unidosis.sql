

CREATE VIEW [dbo].[Farmacia-Unidosis]
AS
SELECT        PR.NUMEFOLIO AS NUMERO_FOLIO, PR.IPCODPACI AS 'CODIGO PACIENTE', PA.IPNOMCOMP AS 'NOMBRE PACIENTE', 
                         PR.NUMINGRES AS 'NUMERO DE INGRESO', PR.UFUCODIGO AS 'UNIDAD FUNCIONAL', U.UFUDESCRI, M.CODPROSAL, M.NOMMEDICO, PR.CODPRODUC, 
                         P.DESPRODUC, PR.CODVIAADM, PR.CODFORMED, PR.FRECUENCI, PR.UNIFRECUE, PR.FECINIDOS, PR.TIPFORMED, PR.DURACIDOS, PR.PREFECANT, 
                         PR.PREESTADO, PR.CODDIAGNO, PR.INDAPLMED, PR.CANPEDPRO, PR.TOTPROUNI, PR.DESADMINI, PR.DOSISPRFN, PR.CODUNIMFN
FROM            dbo.HCPRESCRA AS PR INNER JOIN
                         dbo.INPACIENT AS PA ON PR.IPCODPACI = PA.IPCODPACI INNER JOIN
                         dbo.INUNIFUNC AS U ON PR.UFUCODIGO = U.UFUCODIGO INNER JOIN
                         dbo.INPROFSAL AS M ON PR.CODPROSAL = M.CODPROSAL INNER JOIN
                         dbo.IHLISTPRO AS P ON PR.CODPRODUC = P.CODPRODUC
WHERE        (PR.PREESTADO IN (1, 6))
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida las prescripciones de medicamentos en modalidad unidosis activas o en dispensación (estados 1 y 6), integrando datos del paciente (cédula y nombre completo), el número de ingreso u hospitalización, la unidad funcional o sala donde está internado, el médico prescriptor, el medicamento con su descripción, la vía de administración, forma farmacéutica, frecuencia, duración del tratamiento, dosis, cantidad pedida y fecha de inicio. Combina las tablas de prescripciones (HCPRESCRA), pacientes (INPACIENT), unidades funcionales (INUNIFUNC), profesionales de la salud (INPROFSAL) y catálogo de productos farmacéuticos (IHLISTPRO). Está diseñada para alimentar el proceso de dispensación unidosis en farmacia hospitalaria, permitiendo al farmacéutico identificar qué medicamentos deben prepararse y entregarse por paciente, sala y turno.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Farmacia-Unidosis';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Farmacia-Unidosis';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una vista las prescripciones médicas activas o vigentes para dispensación en modalidad unidosis, enriquecidas con datos del paciente, unidad funcional, profesional prescriptor y producto farmacéutico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Farmacia-Unidosis';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las prescripciones deben tener referencias válidas e íntegras a paciente, unidad funcional, profesional de salud y producto en sus maestros correspondientes.; El estado de la prescripción (PREESTADO) debe ser 1 o 6 para ser visible.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Farmacia-Unidosis';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo expone prescripciones cuyo estado (PREESTADO) sea 1 o 6, asociadas al esquema de unidosis.; Cada prescripción listada debe tener paciente, unidad funcional, profesional y producto válidos en sus respectivos maestros (INNER JOIN obligatorio).; Excluye prescripciones en estados distintos a 1 y 6 (no aptas para dispensación en unidosis).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Farmacia-Unidosis';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; prescripción médica; unidad funcional; profesional de la salud (médico); producto farmacéutico; dosis unitaria (unidosis); vía de administración; forma medicamentosa; frecuencia de dosificación; diagnóstico; ingreso hospitalario', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Farmacia-Unidosis';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando PREESTADO IN (1,6) y existen registros relacionados en INPACIENT, INUNIFUNC, INPROFSAL e IHLISTPRO, se retorna la prescripción enriquecida con datos de paciente, unidad funcional, médico y producto.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Farmacia-Unidosis';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCPRESCRA; dbo.INPACIENT; dbo.INUNIFUNC; dbo.INPROFSAL; dbo.IHLISTPRO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Farmacia-Unidosis';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Farmacia-Unidosis';
GO
