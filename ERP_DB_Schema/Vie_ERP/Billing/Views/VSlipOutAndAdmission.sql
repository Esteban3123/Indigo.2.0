









CREATE VIEW [Billing].[VSlipOutAndAdmission]
AS


SELECT so.Id as idSlipOut ,so.Code as codeSlipOut, so.DocumentDate as dateSlipOut, so.AdmissionNumber as AdmissionNumber, p.IPCODPACI as documentPacient, p.IPNOMCOMP as namePacient
FROM Billing.SlipOut so INNER JOIN dbo.ADINGRESO i on i.NUMINGRES = so.AdmissionNumber 
INNER JOIN dbo.INPACIENT p on p.IPCODPACI = i.IPCODPACI