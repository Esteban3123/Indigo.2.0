
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte RIPS Tipo 2 (registro de consultas externas y citas médicas asignadas) para un rango de fechas dado, filtrando citas de tipo estándar (CODTIPCIT=0) por especialidades específicas. Consolida en un único resultado la información demográfica del paciente (tipo y número de documento, fecha de nacimiento, sexo, nombre completo), la EAPB/EPS a la que pertenece, y las fechas clave de la cita (fecha de solicitud, fecha de asignación y fecha solicitada por el usuario), clasificando cada registro según el tipo de consulta o procedimiento no quirúrgico correspondiente. Integra los maestros de pacientes (INPACIENT), entidades/aseguradoras (INENTIDAD), citas agendadas (AGASICITA), especialidades médicas (INESPECIA), profesionales de la salud (INPROFSAL), tipos de consulta (AGCONSULT) y actividades médicas (AGACTIMED), y aplica las funciones TipDocR256 y SexoR256 para traducir códigos internos a los valores exigidos por el estándar RIPS de la Resolución 256.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_R256_Tipo2';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_R256_Tipo2';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el archivo Tipo 2 del Resolución 256 (indicadores de calidad en salud), listando las citas asignadas en un rango de fechas clasificadas por tipo de cita/procedimiento no quirúrgico según la especialidad o actividad médica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_R256_Tipo2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@FechaI y @FechaF deben acotar el rango de FECHORAIN (fecha/hora de asignación de cita); Las funciones escalares dbo.TipDocR256 y dbo.SexoR256 deben existir para mapear tipo de documento y sexo al estándar de la Resolución 256; Los catálogos INESPECIA, INPACIENT, INENTIDAD, INPROFSAL, AGCONSULT y AGACTIMED deben tener registros consistentes con las citas en AGASICITA (los INNER JOIN excluyen citas con maestros faltantes)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_R256_Tipo2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan citas cuya FECHORAIN cae dentro de [@FechaI, @FechaF]; Los tipos 1 a 7 exigen CODTIPCIT = 0 (cita estándar/no excluida); el tipo 8 no aplica este filtro; Todas las citas reportadas se marcan con valor fijo ''1'' en el campo ''La cita fue asignada''; Las fechas se exponen en formato ISO yyyy/mm/dd (estilo CONVERT 111); Una misma cita puede aparecer en varias categorías si cumple condiciones de más de un bloque (UNION ALL no deduplica entre bloques); Dentro de cada bloque de los tipos 1-7 se aplica DISTINCT, evitando duplicados intra-categoría; el tipo 8 no usa DISTINCT', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_R256_Tipo2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Resolución 256 (indicadores de calidad en salud); Cita médica asignada; Tipo de cita o procedimiento no quirúrgico; Especialidad médica; Actividad médica; EAPB (Entidad Administradora de Planes de Beneficios); Tipo de identificación del usuario; Paciente; Profesional de la salud', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_R256_Tipo2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve un resultset con literal ''2'' como tipo de registro y un consecutivo ROW_NUMBER ordenado por CODAUTONU descendente, junto con datos demográficos del paciente y fechas de la cita', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_R256_Tipo2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si C.CODTIPCIT = 0 AND C.CODESPECI = 002 → Clasifica la cita como tipo ''1'' (Identificación del tipo de cita o procedimiento no quirúrgico); si C.CODTIPCIT = 0 AND C.CODESPECI = 208 → Clasifica la cita como tipo ''2''; si C.CODTIPCIT = 0 AND C.CODESPECI = 101 → Clasifica la cita como tipo ''3''; si C.CODTIPCIT = 0 AND C.CODESPECI = 142 → Clasifica la cita como tipo ''4''; si C.CODTIPCIT = 0 AND C.CODESPECI = 085 → Clasifica la cita como tipo ''5''; si C.CODTIPCIT = 0 AND A.CODACTMED = 004 → Clasifica la cita como tipo ''6''; si C.CODTIPCIT = 0 AND C.CODESPECI = 044 → Clasifica la cita como tipo ''7''; si A.CODACTMED IN (027, 070, 071, 072, 073, 074) → Clasifica la cita como tipo ''8'' (sin restricción de CODTIPCIT)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_R256_Tipo2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.TipDocR256; dbo.SexoR256', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_R256_Tipo2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGASICITA; dbo.INESPECIA; dbo.INPACIENT; dbo.INENTIDAD; dbo.INPROFSAL; dbo.AGCONSULT; dbo.AGACTIMED', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_R256_Tipo2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_R256_Tipo2';
-- GO
