
CREATE VIEW [MixingStation].[ViewCustomersWithExternalCareCenters]
AS
SELECT	DISTINCT 
		c.Id ViewKey,
		c.Id IdCustomer,
		c.Nit NitCustomer,
		c.Name NameCustomer
FROM Common.Customer c
JOIN MixingStation.ExternalCareCenter ecc ON c.Id = ecc.CustomerId
WHERE ecc.Status = 1