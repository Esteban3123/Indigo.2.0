
CREATE VIEW [Report].[ViewListadoGeneralUsuarios] as
SELECT 
 CAST(DB_NAME() AS VARCHAR(11)) AS ID_COMPANY,
 vs.TENAN_ID,
 vs.[Persona_Id],
 vs.[Codigo_Usuario],
 vs.[Identification],
 vs.[Nombre_Completo],
 vs.[Email],
 vs.[Tipo Rol],
 vs.[Rol_Codigo],
 vs.[Rol_Nombre],
 vs.[Tipo Usuario],
 vs.[Grupo_Codigo],
 vs.[Grupo_Nombre],
 vs.[Cargo],
 vs.[Estado],
 ips.CODUSUARI [Identificador],
 ips.CODESPEC1 [Nombre_Especif],
 ES.DESESPECI [Especif],
 CASE ips.TIPPROFES WHEN '1' THEN 'Medico General'
                    WHEN '2' THEN 'Medico Especialista'
                    WHEN '3' THEN 'Enfermera'
                    WHEN '4' THEN 'Auxiliar Enfermeria'
                    WHEN '5' THEN 'Odontologo General'
                    WHEN '6' THEN 'Odontologo Especialista'
                    WHEN '7' THEN 'Nutricionista'
                    WHEN '8' THEN 'Higienista'
                    WHEN '9' THEN 'Psicologo'
                    WHEN '10' THEN 'Trabajadora Social'
                    WHEN '11' THEN 'Promotor de Saneamiento'
                    WHEN '12' THEN 'Ingeniero Sanitario'
                    WHEN '13' THEN 'Medico Veterinario'
                    WHEN '14' THEN 'Ingeniero Alimento'
                    WHEN '15' THEN 'Auxiliar Bacteriologo'
                    WHEN '16' THEN 'Terapeuta'
                    WHEN '17' THEN 'Optometra'
                    WHEN '18' THEN 'Quimico Farmaceutico'
                    WHEN '19' THEN 'Radiologo'
                    WHEN '21' THEN 'Instrumentador Qx'
                    WHEN '22' THEN 'Auxiliar Patologia'
                    WHEN '23' THEN 'Otros'
                    WHEN '24' THEN 'Medico Interno'
                    WHEN '25' THEN 'Bacteriologo(a)'
                    WHEN '26' THEN 'Patólogo(a)'
                    WHEN '27' THEN 'Médico residente'
 END [Tipo_Profesional],
 CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM 
 [Report].[ViewListadoUsuarios] vs
 LEFT JOIN dbo.INPROFSAL ips ON ips.CODPROSAL = vs.[Identification]
 LEFT JOIN dbo.INESPECIA ES ON ES.CODESPECI = ips.CODESPEC1