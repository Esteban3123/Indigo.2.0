
-- =============================================  
-- Author:  <Author,,Name>  
-- ALTER date: <ALTER Date,,>  
-- Description: <Description,,>  
-- =============================================  
CREATE PROCEDURE [dbo].[SP_Tecnico_Cientifica_ProceQuirurgicos]   
-- Add the parameters for the stored procedure here  
@FECHA_INICIAL DATETIME, 
@FEHCA_FINAL   DATETIME
AS
    BEGIN  
        -- SET NOCOUNT ON added to prevent extra result sets from  
        -- interfering with SELECT statements.  
        SET NOCOUNT ON;

        -- Insert statements for procedure here  
        SELECT Gra.Name AS 'GRUPO ATENCION', 
               EntA.Name AS ENTIDAD, 
               HCQ.NUMEFOLIO, 
               HCQ.IPCODPACI 'IDENTIFICACION', 
               INP.IPNOMCOMP AS 'NOMBRE PACIENTE', 
               HCQ.NUMINGRES, 
               HCQ.CODSERIPS, 
               INC.DESSERIPS, 
               HCQ.CODPROSAL AS 'COD MEDICO ESP', 
               INPR.NOMMEDICO AS 'MEDICO ESP', 
               dbo.MedicosEsp(HCQ.CODPROSAL) AS 'CODIGO ESP', 
               INES.DESESPECI, 
               dbo.Interno_Cod(HCQ.NUMEFOLIO, HCQ.IPCODPACI) AS 'COD MEDICO INTERNO', 
               DX.NOMDIAGNO AS DxPRE, 
               HCQ.FECHORINI AS 'FECHA INICIAL', 
               HCQ.FECHORFIN AS 'FEHCA FINAL', 
               dbo.Tiempo_transcurrido(HCQ.FECHORFIN, HCQ.FECHORINI) AS 'TIEMPO TRANSCURRIDO', 
               dbo.Tipo_Anestesia(HCQ.TIPOANEST) AS 'TIPO ANESTESIA', 
               dbo.Tipo_Cirugia(HCQ.URGECIRUG) AS 'TIPO CIRUGIA', 
               Q2.CODSERIPS AS Código,
               CASE
                   WHEN Q2.QXPRINCIP = 'TRUE'
                   THEN 'SI'
                   WHEN Q2.QXPRINCIP = 'FALSE'
                   THEN 'NO'
               END AS PRINCIPAL, 
               C2.DESSERIPS AS Descripción, 
               DATEDIFF(YY, INP.IPFECNACI, HCQ.FECHORINI) AS Expr1, 
               RTRIM(LTRIM(dbo.Edad2(CONVERT(VARCHAR, INP.IPFECNACI, 105), CONVERT(VARCHAR, HCQ.FECHORINI, 105)))) AS EDAD, 
               INUB.UBINOMBRE AS Barrio, 
               INUB.DEPMUNCOD AS Municipio, 
               CAST(INP.IPFECNACI AS DATE) AS Fnacimiento, 
               dbo.SexoR256(INP.IPSEXOPAC) AS sexo, 
               dbo.Etarios(INP.IPFECNACI, HCQ.FECHORINI) AS 'GRUPO ETARIO 1', 
               INP.IPTELEFON AS Tel1, 
               INP.IPTELMOVI AS Tel2, 
               HCQ.SALACIRUG AS Sala, 
               HCQ.MATERADIC AS Materiales
        FROM dbo.HCQXINFOR AS HCQ
             INNER JOIN dbo.INPACIENT AS INP ON INP.IPCODPACI = HCQ.IPCODPACI
             INNER JOIN dbo.INPROFSAL AS INPR ON INPR.CODPROSAL = HCQ.CODPROSAL
             INNER JOIN dbo.ADINGRESO AS Ing ON HCQ.IPCODPACI = Ing.IPCODPACI
                                                AND HCQ.NUMINGRES = Ing.NUMINGRES
             INNER JOIN Contract.CareGroup AS GrA ON Ing.GENCAREGROUP = GrA.Id
             INNER JOIN Contract.HealthAdministrator AS EntA ON Ing.GENCONENTITY = EntA.Id
             INNER JOIN dbo.INCUPSIPS AS INC ON INC.CODSERIPS = HCQ.CODSERIPS
             INNER JOIN dbo.INESPECIA AS INES ON INPR.CODESPEC1 = INES.CODESPECI
             INNER JOIN dbo.INDIAGNOS AS DX ON HCQ.CODDIAPRE = DX.CODDIAGNO
             INNER JOIN dbo.HCQXREALI AS Q2 ON(HCQ.NUMEFOLIO = Q2.NUMEFOLIO)
                                              AND (HCQ.IPCODPACI = Q2.IPCODPACI)
                                              AND (HCQ.NUMINGRES = Q2.NUMINGRES)
             INNER JOIN dbo.INCUPSIPS AS C2 ON C2.CODSERIPS = Q2.CODSERIPS
             INNER JOIN dbo.INUBICACI AS INUB ON INP.AUUBICACI = INUB.AUUBICACI
        WHERE(HCQ.FECHORINI BETWEEN @FECHA_INICIAL AND @FEHCA_FINAL)
        ORDER BY HCQ.IPCODPACI;
    END;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el informe técnico-científico de procedimientos quirúrgicos realizados en un rango de fechas. Para cada cirugía, consolida datos del paciente (cédula, nombre, edad, sexo, barrio, municipio, teléfonos), del ingreso hospitalario (número de ingreso, grupo de atención, entidad pagadora/EPS), del informe quirúrgico (sala, fecha y hora de inicio y fin, duración, tipo de anestesia, tipo de cirugía, materiales, diagnóstico preoperatorio) y del médico especialista (código, nombre, especialidad). Además desglosa todos los procedimientos CUPS ejecutados durante cada intervención, indicando cuál es el principal y cuáles son secundarios. Este reporte es utilizado por el área técnico-científica o de calidad para auditar y analizar la actividad quirúrgica institucional por período, entidad pagadora y grupo de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_Tecnico_Cientifica_ProceQuirurgicos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_Tecnico_Cientifica_ProceQuirurgicos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte técnico-científico de procedimientos quirúrgicos realizados en un rango de fechas, consolidando datos del paciente, ingreso, médico, especialidad, diagnóstico prequirúrgico y procedimientos CUPS asociados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Tecnico_Cientifica_ProceQuirurgicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las fechas de inicio y fin del rango deben estar definidas para filtrar HCQ.FECHORINI; Cada informe quirúrgico debe tener paciente, profesional, ingreso, grupo de atención, entidad, servicio CUPS, especialidad y diagnóstico prequirúrgico válidos (INNER JOIN obligatorio); Debe existir al menos un procedimiento realizado en HCQXREALI asociado al folio/paciente/ingreso; El paciente debe tener ubicación registrada (AUUBICACI en INUBICACI)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Tecnico_Cientifica_ProceQuirurgicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen cirugías cuya fecha de inicio cae dentro del rango parametrizado; Cada fila representa un procedimiento realizado (HCQXREALI) vinculado a un informe quirúrgico (HCQXINFOR) por folio, paciente e ingreso; Se reportan únicamente cirugías con paciente, profesional, ingreso, grupo de atención, entidad administradora, especialidad y diagnóstico prequirúrgico existentes; La edad y grupo etario se calculan respecto a la fecha de inicio de la cirugía, no respecto a la fecha actual; El tiempo transcurrido se calcula entre la fecha/hora final e inicial del acto quirúrgico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Tecnico_Cientifica_ProceQuirurgicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Procedimiento quirúrgico; Informe quirúrgico; Paciente; Ingreso/admisión; Grupo de atención; Entidad administradora de salud; Profesional de la salud / médico especialista; Médico interno; Especialidad médica; Diagnóstico prequirúrgico; Tipo de anestesia; Tipo de cirugía (urgencia/electiva); CUPS (servicios); Procedimiento principal; Sala de cirugía; Materiales adicionales; Edad y grupo etario; Sexo; Ubicación (barrio/municipio)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Tecnico_Cientifica_ProceQuirurgicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando HCQ.FECHORINI está entre @FECHA_INICIAL y @FEHCA_FINAL, devuelve el listado de procedimientos quirúrgicos ordenado por identificación del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Tecnico_Cientifica_ProceQuirurgicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Q2.QXPRINCIP = ''TRUE'' → Marca el procedimiento como PRINCIPAL = ''SI'' else Si Q2.QXPRINCIP = ''FALSE'' marca PRINCIPAL = ''NO''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Tecnico_Cientifica_ProceQuirurgicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.MedicosEsp; dbo.Interno_Cod; dbo.Tiempo_transcurrido; dbo.Tipo_Anestesia; dbo.Tipo_Cirugia; dbo.Edad2; dbo.SexoR256; dbo.Etarios', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Tecnico_Cientifica_ProceQuirurgicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCQXINFOR; dbo.INPACIENT; dbo.INPROFSAL; dbo.ADINGRESO; Contract.CareGroup; Contract.HealthAdministrator; dbo.INCUPSIPS; dbo.INESPECIA; dbo.INDIAGNOS; dbo.HCQXREALI; dbo.INUBICACI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Tecnico_Cientifica_ProceQuirurgicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Tecnico_Cientifica_ProceQuirurgicos';
-- GO
