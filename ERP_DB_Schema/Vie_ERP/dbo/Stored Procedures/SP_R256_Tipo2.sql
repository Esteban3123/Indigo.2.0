
CREATE PROCEDURE [dbo].[SP_R256_Tipo2]
(@FechaI DATETIME, 
 @FechaF DATETIME
)
AS
    BEGIN
        SET NOCOUNT ON;
        SELECT '2' AS [0. Tipo Registro], 
               ROW_NUMBER() OVER(
               ORDER BY [N° CITA] DESC) AS [1. Consecutivo de Registro], 
               *
        FROM
        (
            SELECT DISTINCT 
                   [dbo].[TipDocR256](PA.IPTIPODOC) AS [2. Tipo de Identificación del Usuario], 
                   C.IPCODPACI AS [3. Número de Identificación del Usuario], 
                   CONVERT(VARCHAR, PA.IPFECNACI, 111) AS [4. Fecha de Nacimiento del usuario], 
                   [dbo].[SexoR256](PA.IPSEXOPAC) AS [5. Sexo del Usuario], 
                   PA.IPPRIAPEL AS [6. Primer apellido del usuario], 
                   PA.IPSEGAPEL AS [7. Segundo apellido del usuario], 
                   PA.IPPRINOMB AS [8. Primer nombre del usuario], 
                   PA.IPSEGNOMB AS [9. Segundo nombre del usuario], 
                   EP.CODADMPAG AS [10. Código de la EAPB del usuario], 
                   '1' AS [11. Identificación del tipo de cita o procedimiento no quirúrgico], 
                   CONVERT(VARCHAR, C.FECREGSIS, 111) AS [12. Fecha de la solicitud de la cita], 
                   '1' AS [13. La cita fue asignada], 
                   CONVERT(VARCHAR, C.FECHORAIN, 111) AS [14. Fecha de la asignación de la cita], 
                   CONVERT(VARCHAR, C.FECITADES, 111) AS [15. Fecha para la cual el usuario solicitó que le fuera asignada la cita], 
                   C.CODAUTONU AS [N° CITA]
            FROM dbo.AGASICITA AS C
                 INNER JOIN dbo.INESPECIA AS E ON E.CODESPECI = C.CODESPECI
                 INNER JOIN dbo.INPACIENT AS PA ON PA.IPCODPACI = C.IPCODPACI
                 INNER JOIN dbo.INENTIDAD AS EP ON EP.CODENTIDA = PA.CODENTIDA
                 INNER JOIN dbo.INPROFSAL AS P ON P.CODPROSAL = C.CODPROSAL
                 INNER JOIN dbo.AGCONSULT AS CO ON CO.CODIGOCON = C.CODIGOCON
                 INNER JOIN dbo.AGACTIMED AS A ON A.CODACTMED = C.CODACTMED
            WHERE(C.FECHORAIN BETWEEN @FechaI AND @FechaF)
                 AND C.CODTIPCIT = 0
                 AND C.CODESPECI = 002
            UNION ALL
            SELECT DISTINCT 
                   [dbo].[TipDocR256](PA.IPTIPODOC) AS [2. Tipo de Identificación del Usuario], 
                   C.IPCODPACI AS [3. Número de Identificación del Usuario], 
                   CONVERT(VARCHAR, PA.IPFECNACI, 111) AS [4. Fecha de Nacimiento del usuario], 
                   [dbo].[SexoR256](PA.IPSEXOPAC) AS [5. Sexo del Usuario], 
                   PA.IPPRIAPEL AS [6. Primer apellido del usuario], 
                   PA.IPSEGAPEL AS [7. Segundo apellido del usuario], 
                   PA.IPPRINOMB AS [8. Primer nombre del usuario], 
                   PA.IPSEGNOMB AS [9. Segundo nombre del usuario], 
                   EP.CODADMPAG AS [10. Código de la EAPB del usuario], 
                   '2' AS [11. Identificación del tipo de cita o procedimiento no quirúrgico], 
                   CONVERT(VARCHAR, C.FECREGSIS, 111) AS [12. Fecha de la solicitud de la cita], 
                   '1' AS [13. La cita fue asignada], 
                   CONVERT(VARCHAR, C.FECHORAIN, 111) AS [14. Fecha de la asignación de la cita], 
                   CONVERT(VARCHAR, C.FECITADES, 111) AS [15. Fecha para la cual el usuario solicitó que le fuera asignada la cita], 
                   C.CODAUTONU AS [N° CITA]
            FROM dbo.AGASICITA AS C
                 INNER JOIN dbo.INESPECIA AS E ON E.CODESPECI = C.CODESPECI
                 INNER JOIN dbo.INPACIENT AS PA ON PA.IPCODPACI = C.IPCODPACI
                 INNER JOIN dbo.INENTIDAD AS EP ON EP.CODENTIDA = PA.CODENTIDA
                 INNER JOIN dbo.INPROFSAL AS P ON P.CODPROSAL = C.CODPROSAL
                 INNER JOIN dbo.AGCONSULT AS CO ON CO.CODIGOCON = C.CODIGOCON
                 INNER JOIN dbo.AGACTIMED AS A ON A.CODACTMED = C.CODACTMED
            WHERE(C.FECHORAIN BETWEEN @FechaI AND @FechaF)
                 AND C.CODTIPCIT = 0
                 AND C.CODESPECI = 208
            UNION ALL
            SELECT DISTINCT 
                   [dbo].[TipDocR256](PA.IPTIPODOC) AS [2. Tipo de Identificación del Usuario], 
                   C.IPCODPACI AS [3. Número de Identificación del Usuario], 
                   CONVERT(VARCHAR, PA.IPFECNACI, 111) AS [4. Fecha de Nacimiento del usuario], 
                   [dbo].[SexoR256](PA.IPSEXOPAC) AS [5. Sexo del Usuario], 
                   PA.IPPRIAPEL AS [6. Primer apellido del usuario], 
                   PA.IPSEGAPEL AS [7. Segundo apellido del usuario], 
                   PA.IPPRINOMB AS [8. Primer nombre del usuario], 
                   PA.IPSEGNOMB AS [9. Segundo nombre del usuario], 
                   EP.CODADMPAG AS [10. Código de la EAPB del usuario], 
                   '3' AS [11. Identificación del tipo de cita o procedimiento no quirúrgico], 
                   CONVERT(VARCHAR, C.FECREGSIS, 111) AS [12. Fecha de la solicitud de la cita], 
                   '1' AS [13. La cita fue asignada], 
                   CONVERT(VARCHAR, C.FECHORAIN, 111) AS [14. Fecha de la asignación de la cita], 
                   CONVERT(VARCHAR, C.FECITADES, 111) AS [15. Fecha para la cual el usuario solicitó que le fuera asignada la cita], 
                   C.CODAUTONU AS [N° CITA]
            FROM dbo.AGASICITA AS C
                 INNER JOIN dbo.INESPECIA AS E ON E.CODESPECI = C.CODESPECI
                 INNER JOIN dbo.INPACIENT AS PA ON PA.IPCODPACI = C.IPCODPACI
                 INNER JOIN dbo.INENTIDAD AS EP ON EP.CODENTIDA = PA.CODENTIDA
                 INNER JOIN dbo.INPROFSAL AS P ON P.CODPROSAL = C.CODPROSAL
                 INNER JOIN dbo.AGCONSULT AS CO ON CO.CODIGOCON = C.CODIGOCON
                 INNER JOIN dbo.AGACTIMED AS A ON A.CODACTMED = C.CODACTMED
            WHERE(C.FECHORAIN BETWEEN @FechaI AND @FechaF)
                 AND C.CODTIPCIT = 0
                 AND C.CODESPECI = 101
            UNION ALL
            SELECT DISTINCT 
                   [dbo].[TipDocR256](PA.IPTIPODOC) AS [2. Tipo de Identificación del Usuario], 
                   C.IPCODPACI AS [3. Número de Identificación del Usuario], 
                   CONVERT(VARCHAR, PA.IPFECNACI, 111) AS [4. Fecha de Nacimiento del usuario], 
                   [dbo].[SexoR256](PA.IPSEXOPAC) AS [5. Sexo del Usuario], 
                   PA.IPPRIAPEL AS [6. Primer apellido del usuario], 
                   PA.IPSEGAPEL AS [7. Segundo apellido del usuario], 
                   PA.IPPRINOMB AS [8. Primer nombre del usuario], 
                   PA.IPSEGNOMB AS [9. Segundo nombre del usuario], 
                   EP.CODADMPAG AS [10. Código de la EAPB del usuario], 
                   '4' AS [11. Identificación del tipo de cita o procedimiento no quirúrgico], 
                   CONVERT(VARCHAR, C.FECREGSIS, 111) AS [12. Fecha de la solicitud de la cita], 
                   '1' AS [13. La cita fue asignada], 
                   CONVERT(VARCHAR, C.FECHORAIN, 111) AS [14. Fecha de la asignación de la cita], 
                   CONVERT(VARCHAR, C.FECITADES, 111) AS [15. Fecha para la cual el usuario solicitó que le fuera asignada la cita], 
                   C.CODAUTONU AS [N° CITA]
            FROM dbo.AGASICITA AS C
                 INNER JOIN dbo.INESPECIA AS E ON E.CODESPECI = C.CODESPECI
                 INNER JOIN dbo.INPACIENT AS PA ON PA.IPCODPACI = C.IPCODPACI
                 INNER JOIN dbo.INENTIDAD AS EP ON EP.CODENTIDA = PA.CODENTIDA
                 INNER JOIN dbo.INPROFSAL AS P ON P.CODPROSAL = C.CODPROSAL
                 INNER JOIN dbo.AGCONSULT AS CO ON CO.CODIGOCON = C.CODIGOCON
                 INNER JOIN dbo.AGACTIMED AS A ON A.CODACTMED = C.CODACTMED
            WHERE(C.FECHORAIN BETWEEN @FechaI AND @FechaF)
                 AND C.CODTIPCIT = 0
                 AND C.CODESPECI = 142
            UNION ALL
            SELECT DISTINCT 
                   [dbo].[TipDocR256](PA.IPTIPODOC) AS [2. Tipo de Identificación del Usuario], 
                   C.IPCODPACI AS [3. Número de Identificación del Usuario], 
                   CONVERT(VARCHAR, PA.IPFECNACI, 111) AS [4. Fecha de Nacimiento del usuario], 
                   [dbo].[SexoR256](PA.IPSEXOPAC) AS [5. Sexo del Usuario], 
                   PA.IPPRIAPEL AS [6. Primer apellido del usuario], 
                   PA.IPSEGAPEL AS [7. Segundo apellido del usuario], 
                   PA.IPPRINOMB AS [8. Primer nombre del usuario], 
                   PA.IPSEGNOMB AS [9. Segundo nombre del usuario], 
                   EP.CODADMPAG AS [10. Código de la EAPB del usuario], 
                   '5' AS [11. Identificación del tipo de cita o procedimiento no quirúrgico], 
                   CONVERT(VARCHAR, C.FECREGSIS, 111) AS [12. Fecha de la solicitud de la cita], 
                   '1' AS [13. La cita fue asignada], 
                   CONVERT(VARCHAR, C.FECHORAIN, 111) AS [14. Fecha de la asignación de la cita], 
                   CONVERT(VARCHAR, C.FECITADES, 111) AS [15. Fecha para la cual el usuario solicitó que le fuera asignada la cita], 
                   C.CODAUTONU AS [N° CITA]
            FROM dbo.AGASICITA AS C
                 INNER JOIN dbo.INESPECIA AS E ON E.CODESPECI = C.CODESPECI
                 INNER JOIN dbo.INPACIENT AS PA ON PA.IPCODPACI = C.IPCODPACI
                 INNER JOIN dbo.INENTIDAD AS EP ON EP.CODENTIDA = PA.CODENTIDA
                 INNER JOIN dbo.INPROFSAL AS P ON P.CODPROSAL = C.CODPROSAL
                 INNER JOIN dbo.AGCONSULT AS CO ON CO.CODIGOCON = C.CODIGOCON
                 INNER JOIN dbo.AGACTIMED AS A ON A.CODACTMED = C.CODACTMED
            WHERE(C.FECHORAIN BETWEEN @FechaI AND @FechaF)
                 AND C.CODTIPCIT = 0
                 AND C.CODESPECI = 085
            UNION ALL
            SELECT DISTINCT 
                   [dbo].[TipDocR256](PA.IPTIPODOC) AS [2. Tipo de Identificación del Usuario], 
                   C.IPCODPACI AS [3. Número de Identificación del Usuario], 
                   CONVERT(VARCHAR, PA.IPFECNACI, 111) AS [4. Fecha de Nacimiento del usuario], 
                   [dbo].[SexoR256](PA.IPSEXOPAC) AS [5. Sexo del Usuario], 
                   PA.IPPRIAPEL AS [6. Primer apellido del usuario], 
                   PA.IPSEGAPEL AS [7. Segundo apellido del usuario], 
                   PA.IPPRINOMB AS [8. Primer nombre del usuario], 
                   PA.IPSEGNOMB AS [9. Segundo nombre del usuario], 
                   EP.CODADMPAG AS [10. Código de la EAPB del usuario], 
                   '6' AS [11. Identificación del tipo de cita o procedimiento no quirúrgico], 
                   CONVERT(VARCHAR, C.FECREGSIS, 111) AS [12. Fecha de la solicitud de la cita], 
                   '1' AS [13. La cita fue asignada], 
                   CONVERT(VARCHAR, C.FECHORAIN, 111) AS [14. Fecha de la asignación de la cita], 
                   CONVERT(VARCHAR, C.FECITADES, 111) AS [15. Fecha para la cual el usuario solicitó que le fuera asignada la cita], 
                   C.CODAUTONU AS [N° CITA]
            FROM dbo.AGASICITA AS C
                 INNER JOIN dbo.INESPECIA AS E ON E.CODESPECI = C.CODESPECI
                 INNER JOIN dbo.INPACIENT AS PA ON PA.IPCODPACI = C.IPCODPACI
                 INNER JOIN dbo.INENTIDAD AS EP ON EP.CODENTIDA = PA.CODENTIDA
                 INNER JOIN dbo.INPROFSAL AS P ON P.CODPROSAL = C.CODPROSAL
                 INNER JOIN dbo.AGCONSULT AS CO ON CO.CODIGOCON = C.CODIGOCON
                 INNER JOIN dbo.AGACTIMED AS A ON A.CODACTMED = C.CODACTMED
            WHERE(C.FECHORAIN BETWEEN @FechaI AND @FechaF)
                 AND C.CODTIPCIT = 0
                 AND A.CODACTMED = 004
            UNION ALL
            SELECT DISTINCT 
                   [dbo].[TipDocR256](PA.IPTIPODOC) AS [2. Tipo de Identificación del Usuario], 
                   C.IPCODPACI AS [3. Número de Identificación del Usuario], 
                   CONVERT(VARCHAR, PA.IPFECNACI, 111) AS [4. Fecha de Nacimiento del usuario], 
                   [dbo].[SexoR256](PA.IPSEXOPAC) AS [5. Sexo del Usuario], 
                   PA.IPPRIAPEL AS [6. Primer apellido del usuario], 
                   PA.IPSEGAPEL AS [7. Segundo apellido del usuario], 
                   PA.IPPRINOMB AS [8. Primer nombre del usuario], 
                   PA.IPSEGNOMB AS [9. Segundo nombre del usuario], 
                   EP.CODADMPAG AS [10. Código de la EAPB del usuario], 
                   '7' AS [11. Identificación del tipo de cita o procedimiento no quirúrgico], 
                   CONVERT(VARCHAR, C.FECREGSIS, 111) AS [12. Fecha de la solicitud de la cita], 
                   '1' AS [13. La cita fue asignada], 
                   CONVERT(VARCHAR, C.FECHORAIN, 111) AS [14. Fecha de la asignación de la cita], 
                   CONVERT(VARCHAR, C.FECITADES, 111) AS [15. Fecha para la cual el usuario solicitó que le fuera asignada la cita], 
                   C.CODAUTONU AS [N° CITA]
            FROM dbo.AGASICITA AS C
                 INNER JOIN dbo.INESPECIA AS E ON E.CODESPECI = C.CODESPECI
                 INNER JOIN dbo.INPACIENT AS PA ON PA.IPCODPACI = C.IPCODPACI
                 INNER JOIN dbo.INENTIDAD AS EP ON EP.CODENTIDA = PA.CODENTIDA
                 INNER JOIN dbo.INPROFSAL AS P ON P.CODPROSAL = C.CODPROSAL
                 INNER JOIN dbo.AGCONSULT AS CO ON CO.CODIGOCON = C.CODIGOCON
                 INNER JOIN dbo.AGACTIMED AS A ON A.CODACTMED = C.CODACTMED
            WHERE(C.FECHORAIN BETWEEN @FechaI AND @FechaF)
                 AND C.CODTIPCIT = 0
                 AND C.CODESPECI = 044
            UNION ALL
            SELECT [dbo].[TipDocR256](PA.IPTIPODOC) AS [2. Tipo de Identificación del Usuario], 
                   C.IPCODPACI AS [3. Número de Identificación del Usuario], 
                   CONVERT(VARCHAR, PA.IPFECNACI, 111) AS [4. Fecha de Nacimiento del usuario], 
                   [dbo].[SexoR256](PA.IPSEXOPAC) AS [5. Sexo del Usuario], 
                   PA.IPPRIAPEL AS [6. Primer apellido del usuario], 
                   PA.IPSEGAPEL AS [7. Segundo apellido del usuario], 
                   PA.IPPRINOMB AS [8. Primer nombre del usuario], 
                   PA.IPSEGNOMB AS [9. Segundo nombre del usuario], 
                   EP.CODADMPAG AS [10. Código de la EAPB del usuario], 
                   '8' AS [11. Identificación del tipo de cita o procedimiento no quirúrgico], 
                   CONVERT(VARCHAR, C.FECREGSIS, 111) AS [12. Fecha de la solicitud de la cita], 
                   '1' AS [13. La cita fue asignada], 
                   CONVERT(VARCHAR, C.FECHORAIN, 111) AS [14. Fecha de la asignación de la cita], 
                   CONVERT(VARCHAR, C.FECITADES, 111) AS [15. Fecha para la cual el usuario solicitó que le fuera asignada la cita], 
                   C.CODAUTONU AS [N° CITA]
            FROM dbo.AGASICITA AS C
                 INNER JOIN dbo.INESPECIA AS E ON E.CODESPECI = C.CODESPECI
                 INNER JOIN dbo.INPACIENT AS PA ON PA.IPCODPACI = C.IPCODPACI
                 INNER JOIN dbo.INENTIDAD AS EP ON EP.CODENTIDA = PA.CODENTIDA
                 INNER JOIN dbo.INPROFSAL AS P ON P.CODPROSAL = C.CODPROSAL
                 INNER JOIN dbo.AGCONSULT AS CO ON CO.CODIGOCON = C.CODIGOCON
                 INNER JOIN dbo.AGACTIMED AS A ON A.CODACTMED = C.CODACTMED
            WHERE(C.FECHORAIN BETWEEN @FechaI AND @FechaF)
                 AND A.CODACTMED IN(027, 070, 071, 072, 073, 074)
        ) SUB;
    END;