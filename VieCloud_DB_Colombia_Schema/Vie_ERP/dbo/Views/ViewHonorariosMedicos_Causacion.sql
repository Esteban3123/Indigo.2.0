    /*******************************************************************************************************************
Nombre: ViewHonorariosMedicos_Causacion
Tipo:Vista
Observacion:Vista de todas las causaciones de honorarios medicos
Profesional: Nilsson Miguel Galindo Lopez
Fecha:26-04-2022
---------------------------------------------------------------------------
Modificaciones
_____________________________________________________________________________
Vercion 1
Persona que modifico: 
Fecha:
Ovservaciones: 
--------------------------------------
Vercion 2
Persona que modifico:
Fecha:
***********************************************************************************************************************************/
	
	

CREATE VIEW [dbo].[ViewHonorariosMedicos_Causacion] AS

SELECT        
MCA.CausationDate AS [Fecha Causacion], 
MCA.AdmissionNumber AS Ingreso, 
F.InvoiceNumber AS Factura, 
F.InvoiceDate AS [Fecha Factura], 
MCA.PatientCode AS DocPaciente, 
PROF.CODPROSAL AS Medico, 
CASE PROF.TIPPROFES WHEN 1 THEN 'Medico General' 
					WHEN 2 THEN 'Medico Especialista' 
					WHEN 3 THEN 'Enfermera' 
					WHEN 4 THEN 'Auxiliar Enfermeria' 
					WHEN 5 THEN 'Odontologo General' 
					WHEN 6 THEN 'Odontologo Especialista'
                    WHEN 7 THEN 'Nutricionista' 
					WHEN 8 THEN 'Higienista' 
					WHEN 9 THEN 'Psicologo' 
					WHEN 10 THEN 'Trabajadora Social' 
					WHEN 11 THEN 'PromotorSaneamiento' 
					WHEN 12 THEN 'IngSanitario' 
					WHEN 13 THEN 'Medico Veterinario'
                    WHEN 14 THEN 'Ing Alimento' 
					WHEN 15 THEN 'Auxiliar Bacteriologo' 
					WHEN 16 THEN 'Terapeuta' 
					WHEN 17 THEN 'Optometra' 
					WHEN 18 THEN 'Quimico Farmaceutico' 
					WHEN 19 THEN 'Radiologo' 
					WHEN 20 THEN 'Tecnologo Radiologo'
                    WHEN 21 THEN 'Instrumentador' 
					WHEN 22 THEN 'Auxiliar Patologia' 
					WHEN 25 THEN 'Bacteriologo(a)' 
					WHEN 26 THEN 'Patólogo(a)' END AS Profesion, 
TER.Nit AS [Nit Medico], 
TER.Name AS Tercero, 
P.Code AS [Nit Entidad], 
MCO.ContractName AS [Entidad Contrato], 
SI.Code AS [Codigo del Servicio], 
SI.Description AS Servicio, 
MCA.AmountPayable AS [Valor Causado], 
CASE MCA.Status WHEN 1 THEN 'Causado' 
				WHEN 2 THEN 'Liquidado' 
				WHEN 3 THEN 'Confirmado' 
				WHEN 4 THEN 'Anulado' END AS Estado
FROM            
MedicalFees.MedicalFeesCausation AS MCA INNER JOIN
MedicalFees.MedicalFeesContract AS MCO ON MCA.MedicalFeesContractId = MCO.Id AND MCA.Status <> 4 INNER JOIN
dbo.INPROFSAL AS PROF ON MCA.HealthProfessionalCode = PROF.CODPROSAL INNER JOIN
Common.ThirdParty AS TER ON TER.Id = MCA.ThirdPartyId LEFT OUTER JOIN
Common.Supplier AS P ON MCO.SupplierId = P.Id INNER JOIN
Billing.InvoiceDetail AS DF ON MCA.InvoiceDetailId = DF.Id INNER JOIN
Billing.Invoice AS F ON DF.InvoiceId = F.Id INNER JOIN
Billing.ServiceOrderDetail AS DS ON MCA.ServiceOrderDetailId = DS.Id INNER JOIN
Contract.CUPSEntity AS SI ON DS.CUPSEntityId = SI.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consolidado de causaciones de honorarios médicos activas (excluye anuladas): integra cada registro de causación con el contrato de honorarios correspondiente, el profesional de la salud (médico, enfermero, odontólogo, terapeuta, etc.), el tercero o proveedor beneficiario del pago, la factura de venta asociada y el servicio/procedimiento CUPS facturado. Permite consultar, por ingreso o factura, cuánto se causó a cada profesional por cada servicio prestado, en qué estado se encuentra el pago (causado, liquidado, confirmado o anulado) y a qué entidad contratante pertenece el contrato de honorarios. Sirve como base para reportes de liquidación, auditoría de pagos a médicos y conciliación de honorarios contra facturación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewHonorariosMedicos_Causacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewHonorariosMedicos_Causacion';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone las causaciones de honorarios médicos vigentes (no anuladas) con su factura, profesional, tercero, entidad contratante, servicio CUPS y estado, para consulta y reportes.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewHonorariosMedicos_Causacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La causación debe estar asociada a un contrato de honorarios médicos vigente.; El profesional referenciado debe existir en el maestro de profesionales de la salud.; La causación debe estar ligada a un tercero, a un detalle de factura con factura cabecera y a un detalle de orden de servicio con su CUPS.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewHonorariosMedicos_Causacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca se muestran causaciones anuladas (Status=4).; Toda causación expuesta tiene contrato, profesional, tercero, factura y servicio CUPS asociados (joins INNER).; El proveedor/entidad puede ser nulo (LEFT JOIN sobre Supplier).; La profesión y el estado siempre se entregan como descripciones legibles, no como códigos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewHonorariosMedicos_Causacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Causación de honorarios médicos; Contrato de honorarios médicos; Profesional de la salud; Profesión (tipo de profesional); Tercero; Proveedor / Entidad contratante; Factura; Detalle de factura; Orden de servicio; Servicio CUPS; Ingreso/Admisión del paciente; Estado de causación (Causado, Liquidado, Confirmado, Anulado); Valor causado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewHonorariosMedicos_Causacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MedicalFees.MedicalFeesCausation: Solo retorna causaciones cuyo Status <> 4 (excluye anuladas) y que tengan vínculo válido con contrato, profesional, tercero, factura y orden de servicio.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewHonorariosMedicos_Causacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si MCA.Status <> 4 (no anulada) → Se incluye la causación en el resultado else Se excluye la causación (anulada); si PROF.TIPPROFES (1..26) → Traduce el código a la profesión correspondiente (Medico General, Especialista, Enfermera, Odontólogo, Nutricionista, Psicólogo, Radiólogo, Bacteriólogo, Patólogo, etc.); si MCA.Status (1..4) → Traduce el estado a Causado (1), Liquidado (2), Confirmado (3) o Anulado (4)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewHonorariosMedicos_Causacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MedicalFees.MedicalFeesCausation; MedicalFees.MedicalFeesContract; dbo.INPROFSAL; Common.ThirdParty; Common.Supplier; Billing.InvoiceDetail; Billing.Invoice; Billing.ServiceOrderDetail; Contract.CUPSEntity', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewHonorariosMedicos_Causacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewHonorariosMedicos_Causacion';
GO
