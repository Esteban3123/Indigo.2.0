CREATE VIEW [dbo].[IND_AS_HC_IndiceReintervecionesQuirurgicas_Nva_2016]
AS
     SELECT Q.IPCODPACI AS Identificacion, 
            P.IPNOMCOMP AS Nombre, 
            Q.NUMINGRES AS Ingreso, 
            Q.CODDIAPRE AS CodDiagnosticoPre, 
            D.NOMDIAGNO AS DiagnosticoPre, 
            Q.CODDIAPOS AS CodDiagnosticoPos, 
            D1.NOMDIAGNO AS DiagnosticoPos, 
            Q.CODSERIPS AS CódProcedimiento, 
            Pr.DESSERIPS AS Procedimiento, 
            Q.FECHORINI AS FechaCirugia, 
            Q.DESHALLOP AS Hallazgo, 
            Q.DESPROCED,
            CASE q.URGECIRUG
                WHEN 'True'
                THEN 'Si'
                WHEN 'False'
                THEN 'No'
            END AS CirugíaUrgente, 
            M.NOMMEDICO AS Cirujano, 
            ga.Name AS Entidad,
            CASE ga.EntityType
                WHEN '1'
                THEN 'EPS'
                WHEN '2'
                THEN 'EPS-S'
                WHEN '3'
                THEN 'Vinculados'
                WHEN '4'
                THEN 'ARL'
                WHEN '5'
                THEN 'ARL'
                WHEN '6'
                THEN 'IPSPrivada'
                WHEN '7'
                THEN 'IPSPublica'
                WHEN '8'
                THEN 'Fosyga'
                WHEN '9'
                THEN 'RegimenEspecial'
                WHEN '10'
                THEN 'SOAT'
                WHEN '11'
                THEN 'Otros'
                WHEN '12'
                THEN 'Otros'
            END AS TipoEntidad
     FROM dbo.HCQXINFOR AS Q
          INNER JOIN
     (
         SELECT IPCODPACI AS Identificacion, 
                COUNT(IPCODPACI) AS Cantidad
         FROM dbo.HCQXINFOR
         WHERE(FECHORINI BETWEEN '01/01/2015 00:00:00' AND '31/12/2015 23:59:59')
         GROUP BY IPCODPACI
         HAVING(COUNT(IPCODPACI) > 1)
     ) AS G ON G.Identificacion = Q.IPCODPACI
          INNER JOIN dbo.INPROFSAL AS M ON M.CODPROSAL = Q.CODPROSAL
          INNER JOIN dbo.INPACIENT AS P ON P.IPCODPACI = Q.IPCODPACI
          INNER JOIN dbo.INDIAGNOS AS D ON D.CODDIAGNO = Q.CODDIAPRE
          INNER JOIN dbo.ADINGRESO AS i ON i.NUMINGRES = Q.NUMINGRES
                                           AND i.IPCODPACI = Q.IPCODPACI
          LEFT OUTER JOIN dbo.INDIAGNOS AS D1 ON D1.CODDIAGNO = Q.CODDIAPOS
          LEFT OUTER JOIN dbo.INCUPSIPS AS Pr ON Pr.CODSERIPS = Q.CODSERIPS
          LEFT OUTER JOIN dbo.INENTIDAD AS e ON e.CODENTIDA = i.CODENTIDA
          LEFT OUTER JOIN Contract.CareGroup AS ga ON ga.Id = i.GENCAREGROUP
     WHERE(Q.CODCENATE = '001')
          AND (Q.FECHORINI >= '01/01/2016 00:00:00');
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting para el indicador de reintervenciones quirúrgicas del año 2016 en el centro de atención ''001''. Identifica pacientes con más de una intervención registrada en 2015 que cuenten con cirugías desde enero de 2016, exponiendo diagnósticos pre y posoperatorios (CIE-10), procedimiento CUPS, cirujano responsable, carácter urgente de la cirugía y la entidad pagadora con su tipo (EPS, ARL, SOAT, etc.). Consolida información de historia clínica quirúrgica, admisiones, maestros clínicos y contratos para alimentar indicadores de calidad asistencial.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HC_IndiceReintervecionesQuirurgicas_Nva_2016';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HC_IndiceReintervecionesQuirurgicas_Nva_2016';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las cirugías realizadas desde 2016 sobre pacientes que tuvieron más de una intervención quirúrgica durante 2015 en el centro de atención ''001'', para construir el indicador de reintervenciones quirúrgicas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HC_IndiceReintervecionesQuirurgicas_Nva_2016';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de registros quirúrgicos en HCQXINFOR con FECHORINI entre 01/01/2015 y 31/12/2015 para identificar pacientes reintervenidos.; Los registros quirúrgicos deben pertenecer al centro de atención ''001''.; Los registros del flujo principal deben tener FECHORINI >= 01/01/2016.; Cada cirugía debe tener profesional, paciente, diagnóstico pre e ingreso válidos (INNER JOIN obligatorios).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HC_IndiceReintervecionesQuirurgicas_Nva_2016';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan pacientes con más de una cirugía registrada en el año 2015.; Solo se incluyen cirugías del centro de atención ''001''.; Solo se reportan cirugías con fecha de inicio igual o posterior al 01/01/2016.; Los códigos 4 y 5 de EntityType se consolidan ambos como ''ARL''; los códigos 11 y 12 se consolidan como ''Otros''.; El diagnóstico postoperatorio, procedimiento CUPS, entidad y grupo de atención son opcionales (LEFT JOIN).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HC_IndiceReintervecionesQuirurgicas_Nva_2016';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Reintervención quirúrgica; Cirugía urgente; Diagnóstico preoperatorio; Diagnóstico postoperatorio; Procedimiento CUPS; Cirujano; Ingreso/Admisión; Entidad responsable de pago (EPS, EPS-S, ARL, SOAT, Fosyga, Régimen Especial, IPS pública/privada, Vinculados); Centro de atención; Hallazgos quirúrgicos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HC_IndiceReintervecionesQuirurgicas_Nva_2016';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve cirugías de 2016 (Q.FECHORINI >= ''01/01/2016'') del centro ''001'' solo si el paciente tuvo más de una cirugía en 2015 (HAVING COUNT(IPCODPACI) > 1).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HC_IndiceReintervecionesQuirurgicas_Nva_2016';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si URGECIRUG = ''True'' → Marca la cirugía como urgente (''Si'') else Si ''False'' marca como ''No''; si EntityType de CareGroup en (1..12) → Traduce el código numérico a tipo de entidad (EPS, EPS-S, Vinculados, ARL, IPSPrivada, IPSPublica, Fosyga, RegimenEspecial, SOAT, Otros)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HC_IndiceReintervecionesQuirurgicas_Nva_2016';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCQXINFOR; dbo.INPROFSAL; dbo.INPACIENT; dbo.INDIAGNOS; dbo.ADINGRESO; dbo.INCUPSIPS; dbo.INENTIDAD; Contract.CareGroup', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HC_IndiceReintervecionesQuirurgicas_Nva_2016';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AS_HC_IndiceReintervecionesQuirurgicas_Nva_2016';
GO
