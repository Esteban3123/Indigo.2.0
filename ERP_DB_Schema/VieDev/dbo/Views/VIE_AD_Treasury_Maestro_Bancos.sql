CREATE VIEW [dbo].[VIE_AD_Treasury_Maestro_Bancos]
AS
SELECT        b.Id, b.Code AS Código, b.Name AS Nombre, T.Nit, T.Name AS Tercero, b.AchCode AS CODACH, b.CenitCode AS [Código CENIT], b.CenitVerification AS [Dígito verificación CENIT], 
                         CASE WHEN b.BankFileCode = '001' THEN 'Bancolombia' WHEN b.BankFileCode = '002' THEN 'Av Villas' WHEN b.BankFileCode = '003' THEN 'Banco Popular' WHEN b.BankFileCode = '004' THEN 'Banco Occidente'
                          WHEN b.BankFileCode = '005' THEN 'Banco BBVA' WHEN b.BankFileCode = '006' THEN 'Banco Davivienda' END AS [Codigo de archivo de banco], b.State AS Estado
FROM            Payroll.Bank AS b WITH (nolock) INNER JOIN
                         Common.ThirdParty AS T WITH (NOLOCK) ON T.Id = b.ThirdPartyId
WHERE        (b.State = '1')