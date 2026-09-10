CREATE VIEW [rda].[ViewListadoNotasAclaratoriasDetalle] AS

    SELECT
        A.SubmissionId AS 'Consecutivo',
        NULL AS 'Codigo',
        B.rda_id AS 'IdRDA',
        CASE A.Send WHEN 1 THEN B.note WHEN 0 THEN A.Response END AS 'Mensaje', 
        CASE A.Send WHEN 1 THEN 'Enviado' WHEN 0 THEN 'Error' END AS 'Estado', 
        CONVERT(DATETIME, C.FechaEnvio) AS 'FechaEnvio',
        CONVERT(DATETIME, B.created_at) AS 'FechaValidacion'
    FROM rda.Submission A
        INNER JOIN rda.rda_clarification_notes B ON A.IdNote = B.id
        INNER JOIN rda.rda_enviados C ON B.rda_id = C.id
    WHERE
        A.HasNote = 1

GO
