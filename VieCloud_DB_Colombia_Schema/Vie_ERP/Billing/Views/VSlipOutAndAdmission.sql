

CREATE VIEW [Billing].[VSlipOutAndAdmission]
AS

SELECT so.Id as idSlipOut ,so.Code as codeSlipOut, so.DocumentDate as dateSlipOut, so.AdmissionNumber as AdmissionNumber, p.IPCODPACI as documentPacient, p.IPNOMCOMP as namePacient
FROM Billing.SlipOut so INNER JOIN dbo.ADINGRESO i on i.NUMINGRES = so.AdmissionNumber 
INNER JOIN dbo.INPACIENT p on p.IPCODPACI = i.IPCODPACI
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Comprobantes de salida (slip out) de facturación cruzados con los datos del ingreso y del paciente. Combina cada documento de egreso o remisión con el número de ingreso hospitalario al que pertenece y los datos de identificación y nombre completo del paciente. Sirve para consultas y reportes de facturación donde se necesita saber a qué paciente y a qué episodio de atención (urgencia, hospitalización, etc.) corresponde cada comprobante de salida, sin tener que cruzar manualmente las tablas de admisiones y pacientes.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'VSlipOutAndAdmission';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'VSlipOutAndAdmission';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los comprobantes de salida (slip out) cruzados con el ingreso del paciente y sus datos básicos de identificación, para consulta unificada de despachos por paciente.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VSlipOutAndAdmission';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada SlipOut debe tener un AdmissionNumber existente en dbo.ADINGRESO (INNER JOIN con NUMINGRES).; Cada ingreso debe estar asociado a un paciente existente en dbo.INPACIENT (INNER JOIN por IPCODPACI).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VSlipOutAndAdmission';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan slip outs cuyo AdmissionNumber empareja con un ingreso vigente en ADINGRESO (INNER JOIN excluye huérfanos).; Solo se retornan registros cuyo ingreso tiene paciente registrado en INPACIENT (INNER JOIN).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VSlipOutAndAdmission';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'comprobante de salida (slip out); ingreso/admisión del paciente; paciente; documento de identificación del paciente', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VSlipOutAndAdmission';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.VSlipOutAndAdmission: Devuelve una fila por cada SlipOut que tenga ingreso y paciente válidos, exponiendo id, código y fecha del slip, número de admisión y documento/nombre del paciente.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VSlipOutAndAdmission';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.SlipOut; dbo.ADINGRESO; dbo.INPACIENT', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VSlipOutAndAdmission';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VSlipOutAndAdmission';
GO
