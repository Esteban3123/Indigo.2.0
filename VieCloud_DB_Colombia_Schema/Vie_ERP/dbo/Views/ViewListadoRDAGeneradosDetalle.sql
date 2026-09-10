CREATE VIEW [rda].[ViewListadoRDAGeneradosDetalle] AS

    SELECT
        A.SubmissionId AS 'Consecutivo',
        CASE A.Send WHEN 1 THEN A.Payload WHEN 0 THEN A.Response END AS 'Mensaje', 
        CASE A.Send WHEN 1 THEN 'Enviado' WHEN 0 THEN 'Error' END AS 'Estado', 
        CONVERT(DATETIME, A.SubmissionDate) AS 'FechaEnvio',
        CONVERT(DATETIME, A.Created_At) AS 'FechaValidacion',
        B.IPCODPACI AS 'Paciente', B.NUMINGRES AS 'Ingreso', A.TipoRda AS 'TipoRDA' 
    FROM rda.Submission A
        INNER JOIN HCHISPACA B ON A.IdHispaca = B.ID
        INNER JOIN INPACIENT C ON B.IPCODPACI = C.IPCODPACI
    WHERE 
        A.HasNote = 0

GO
