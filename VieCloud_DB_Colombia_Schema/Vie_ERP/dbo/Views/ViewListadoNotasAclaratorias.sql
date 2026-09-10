CREATE VIEW [rda].[ViewListadoNotasAclaratorias]
AS

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
        WHERE A.HasNote = 1 AND A.Send = 1
    )

    SELECT
        B.id AS 'Consecutivo',
        B.rda_id AS 'IdRDA',
        C.CODCENATE AS 'CentroAtencion', 
        F.TipoRda AS 'TipoRDA', 
        CE.CompositionId AS 'CodigoUnico',
        RTRIM(D.IPCODPACI) AS 'Identificacion',
        CONCAT(RTRIM(D.IPCODPACI), ' - ', RTRIM(D.IPNOMCOMP)) AS 'Paciente',
        RTRIM(C.NUMINGRES) AS 'Ingreso',
        CASE A.Send WHEN 1 THEN 'Enviado' WHEN 0 THEN 'Error' END AS 'Estado', 
        CONVERT(DATETIME, B.created_at) AS 'FechaDocumento', 
        CONVERT(DATETIME, A.SubmissionDate) AS 'FechaEnvio',
        A.Payload, RTRIM(E.NUMEFOLIO) AS 'NumeroFolio'
    FROM (
        SELECT *,
            ROW_NUMBER() OVER (
                PARTITION BY IdHispaca, TipoRda
                ORDER BY SubmissionId DESC
            ) AS rn
        FROM rda.Submission
    ) A
    INNER JOIN rda.rda_clarification_notes B ON A.IdNote = B.id
    INNER JOIN ADINGRESO C ON B.numingres = C.NUMINGRES
    INNER JOIN INPACIENT D ON C.IPCODPACI = D.IPCODPACI
    INNER JOIN HCHISPACA E ON A.IdHispaca = E.ID
    INNER JOIN rda.rda_enviados F ON B.rda_id = F.Id
    LEFT JOIN CompositionExtract CE ON CE.SubmissionId = A.SubmissionId
    WHERE A.HasNote = 1 AND A.rn = 1;

GO
