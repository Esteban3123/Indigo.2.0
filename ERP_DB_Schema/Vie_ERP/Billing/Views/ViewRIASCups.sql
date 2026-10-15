



CREATE VIEW [Billing].[ViewRIASCups]
AS

	select r.ID RiasId, rc.ID RiasCupsId, r.CODPRO RiasCode, r.NOMBRE RiasName, r.CODPRO + ' - ' + r.NOMBRE RiasCodeName, r.ESTADO RiasStatus, rc.CODSERIPS CupsCode
	from .RIASCUPS rc
	inner join .RIAS r on r.ID = rc.IDRIAS