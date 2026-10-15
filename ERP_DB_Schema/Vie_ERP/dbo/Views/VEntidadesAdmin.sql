CREATE VIEW [dbo].[VEntidadesAdmin]
AS
SELECT        CODENTADM, NOMENTADM, CODIGONIT, TIPENTADM, REGIMEN, CODSUPERINTEN
FROM            dbo.INENTADM
WHERE        (CODENTADM NOT IN
                             (SELECT        Code
                               FROM            Contract.HealthAdministrator))