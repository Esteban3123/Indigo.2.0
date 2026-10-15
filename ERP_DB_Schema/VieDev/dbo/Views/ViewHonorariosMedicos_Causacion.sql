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