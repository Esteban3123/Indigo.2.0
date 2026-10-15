
CREATE VIEW [dbo].[IND_AD_HC_PacientesInsuficienciaRenal]
AS
SELECT DISTINCT 
                         RTRIM(NUMINGRES) + RTRIM(IPCODPACI) + RTRIM(IPCODPACI) AS Llave, NUMINGRES AS Ingreso, IPCODPACI AS Identificación, 
                         UFUCODIGO AS UnidadFuncional
FROM            dbo.INDIAGNOH AS ddx WITH (NOLOCK)
WHERE        (CODDIAGNO IN ('N170', 'N171', 'N172', 'N179', 'N180', 'N189', 'N19X', 'N990', 'O084', 'O904', 'P960')) AND (CODPROSAL <> 'd84') AND 
                         (FECDIAGNO >= '01/01/2016 00:00:00')
GROUP BY NUMINGRES, IPCODPACI, UFUCODIGO