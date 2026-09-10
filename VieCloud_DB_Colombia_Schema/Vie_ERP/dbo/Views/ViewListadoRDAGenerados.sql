CREATE VIEW [rda].[ViewListadoRDAGenerados] AS

WITH CompositionExtract AS (
    SELECT 
        A.SubmissionId,
        e.CompositionId
    FROM rda.Submission A
    OUTER APPLY (
        SELECT TOP 1 e2.CompositionId
        FROM OPENJSON(
                CASE WHEN ISJSON(A.Response) = 1 THEN A.Response END,
                '$.entry'
             )
            WITH (
                resourceType   VARCHAR(50)  '$.resource.resourceType',
                CompositionId  VARCHAR(100) '$.resource.id'
            ) e2
        WHERE e2.resourceType = 'Composition'
    ) e
    WHERE A.HasNote = 0 AND A.Send = 1
)
SELECT
    A.SubmissionId AS 'Consecutivo',
    B.CODCENATE AS 'CentroAtencion',
    A.TipoRda AS 'TipoRDA',
    RTRIM(C.IPCODPACI) AS 'Identificacion',
    CONCAT(RTRIM(C.IPCODPACI), ' - ', RTRIM(C.IPNOMCOMP)) AS 'Paciente',
    RTRIM(B.NUMINGRES) AS 'Ingreso',
    CE.CompositionId AS 'CodigoUnico',
    A.Payload,
    CONVERT(DATETIME, A.SubmissionDate) AS 'FechaEnvio',
    CASE A.Send WHEN 1 THEN 'Enviado' WHEN 0 THEN 'Error' END AS 'Estado',
    CONVERT(DATETIME, A.Created_At) AS 'FechaValidacion',
    A.DataHash, A.IdHispaca, RTRIM(B.NUMEFOLIO) AS 'NumeroFolio', A.Response
FROM (
    SELECT *,
        ROW_NUMBER() OVER (
            PARTITION BY IdHispaca, TipoRda
            ORDER BY SubmissionId DESC
        ) AS rn
    FROM rda.Submission
) A
INNER JOIN HCHISPACA B ON A.IdHispaca = B.ID
INNER JOIN INPACIENT C ON B.IPCODPACI = C.IPCODPACI
LEFT JOIN CompositionExtract CE ON CE.SubmissionId = A.SubmissionId
WHERE A.HasNote = 0 AND A.rn = 1;

GO