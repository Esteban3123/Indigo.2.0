CREATE VIEW [dbo].[HCVistaEspecialidadesInterconsultasOrdenes]
AS
SELECT DISTINCT 
                         B.ESTADO, D.CODCENATE, RTRIM(A.CODESPECI) AS Codigo, RTRIM(B.DESESPECI) AS Especialidad, RTRIM(A.CODSERINT) AS CodigoServicio, RTRIM(C.DESSERIPS) AS NombreServicio, RTRIM(A.CODESPECI) 
                         + ' - ' + RTRIM(B.DESESPECI) AS CodigoDescripcion, C.SERIPSDASH, C.TIPSERIPS, A.IDDESCRIPCIONRELACIONADA_INTER
FROM            dbo.HCESPSERU AS A INNER JOIN
                         dbo.INESPECIA AS B ON A.CODESPECI = B.CODESPECI INNER JOIN
                         dbo.INCUPSIPS AS C ON A.CODSERINT = C.CODSERIPS LEFT OUTER JOIN
                         dbo.HCTURNREC AS D ON D.CODESPECI = A.CODESPECI