
-- =============================================  
-- Author:  JRodriguez  
-- ALTER date:   
-- Description:   
-- =============================================  
CREATE PROCEDURE [dbo].[SP_ESTADISTICA_ProcedimientoQuirurgico]  
-- Add the parameters for the stored procedure here  
@FechaI DATE, 
@FechaF DATE
AS
    BEGIN  
        -- SET NOCOUNT ON added to prevent extra result sets from  
        -- interfering with SELECT statements.  
        SET NOCOUNT ON;

        -- Insert statements for procedure here  
        SELECT 'TIPO DE IDENTIFICACION' = CASE Pa.IPTIPODOC
                                              WHEN '1'
                                              THEN 'Cédula de Ciudadanía'
                                              WHEN '2'
                                              THEN 'Cédula de Extranjería'
                                              WHEN '3'
                                              THEN 'Tarjeta de Identidad'
                                              WHEN '4'
                                              THEN 'Registro Civil'
                                              WHEN '5'
                                              THEN 'Pasporte'
                                              WHEN '6'
                                              THEN 'Adulto Sin Identificación'
                                              ELSE 'Menor Sin Identificación'
                                          END, 
               Q.IPCODPACI AS 'NUMERO DE IDENTIFICACION', 
               CAST(Pa.IPFECNACI AS DATE) AS 'FECHA DE NACIMINETO', 
               'SEXO PACIENTE' = CASE Pa.IPSEXOPAC
                                     WHEN '1'
                                     THEN 'HOMBRE'
                                     WHEN '2'
                                     THEN 'MUJER'
                                     ELSE 'INDEFINIDO'
                                 END, 
               Pa.IPPRIAPEL AS 'PRIMER APELLIDO', 
               Pa.IPSEGAPEL AS 'SEGUNDO APELLIDO', 
               Pa.IPPRINOMB AS 'PRIMER NOMBRE', 
               Pa.IPSEGNOMB AS 'SEGUNDO NOMBRE', 
               Q.CODSERIPS, 
               GrA.Name AS GrupoAtencion, 
               EntA.Name AS Entidad, 
               INUBICACI.DEPMUNCOD AS Municipio, 
               Q2.CODSERIPS AS 'CODIGO DE PROCEDIMIENTO', 
               CAST(Q.FECHORINI AS DATE) AS 'FECHA DE PROCEDIMIENTO'
        FROM HCQXINFOR AS Q
             INNER JOIN INPROFSAL AS P ON P.CODPROSAL = Q.CODPROSAL
             INNER JOIN INDIAGNOS AS DX2 ON Q.CODDIAPRE = DX2.CODDIAGNO
             INNER JOIN INDIAGNOS AS DX ON Q.CODDIAPRE = DX.CODDIAGNO
             INNER JOIN INESPECIA AS E ON P.CODESPEC1 = E.CODESPECI
             INNER JOIN INCUPSIPS AS C ON C.CODSERIPS = Q.CODSERIPS
             INNER JOIN HCQXREALI AS Q2 ON Q.NUMEFOLIO = Q2.NUMEFOLIO
                                           AND Q.IPCODPACI = Q2.IPCODPACI
                                           AND Q.NUMINGRES = Q2.NUMINGRES
             INNER JOIN INCUPSIPS AS C2 ON C2.CODSERIPS = Q2.CODSERIPS
             INNER JOIN INPACIENT AS Pa ON Q.IPCODPACI = Pa.IPCODPACI
             INNER JOIN INUBICACI ON Pa.AUUBICACI = INUBICACI.AUUBICACI
             INNER JOIN ADINGRESO AS Ing ON Q.IPCODPACI = Ing.IPCODPACI
                                            AND Q.NUMINGRES = Ing.NUMINGRES
             INNER JOIN Contract.CareGroup AS GrA ON Ing.GENCAREGROUP = GrA.Id
             INNER JOIN Contract.HealthAdministrator AS EntA ON Ing.GENCONENTITY = EntA.Id
        WHERE(Q.FECHORINI BETWEEN @FechaI AND @FechaF)
        ORDER BY Q.FECHORINI;
    END;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte estadístico de procedimientos quirúrgicos realizados en un rango de fechas definido por el usuario. Integra el informe quirúrgico (HCQXINFOR) con los procedimientos ejecutados (HCQXREALI, códigos CUPS/INCUPSIPS), los datos demográficos del paciente (INPACIENT, INUBICACI), el ingreso hospitalario (ADINGRESO) y el cirujano responsable con su especialidad (INPROFSAL, INESPECIA), incluyendo el diagnóstico preoperatorio en código CIE-10 (INDIAGNOS). Por cada acto quirúrgico devuelve: tipo y número de identificación del paciente, fecha de nacimiento, sexo, nombre completo, municipio de residencia, grupo de atención y entidad contratante (EPS/aseguradora), código del procedimiento principal y del procedimiento realizado, y fecha de la intervención. Se usa para estadísticas quirúrgicas, auditoría clínica y reportería operativa de salas de cirugía.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ESTADISTICA_ProcedimientoQuirurgico';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ESTADISTICA_ProcedimientoQuirurgico';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte estadístico de procedimientos quirúrgicos realizados en un rango de fechas, listando datos demográficos del paciente, entidad responsable, grupo de atención y códigos CUPS de los procedimientos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ESTADISTICA_ProcedimientoQuirurgico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las fechas de inicio y fin deben definir un rango válido para FECHORINI; Cada procedimiento (HCQXINFOR) debe tener correspondencia en HCQXREALI por folio, paciente e ingreso; El paciente debe tener ubicación geográfica registrada (INUBICACI); El ingreso debe tener asociado un grupo de atención (CareGroup) y una entidad administradora de salud (HealthAdministrator); El procedimiento debe tener diagnóstico previo, profesional de salud, especialidad y código CUPS válidos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ESTADISTICA_ProcedimientoQuirurgico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan procedimientos cuya fecha de inicio (FECHORINI) caiga dentro del rango solicitado; Únicamente se incluyen procedimientos con relación completa entre HCQXINFOR y HCQXREALI (mismo folio, paciente e ingreso); Se excluyen registros sin paciente, sin ubicación, sin ingreso, sin grupo de atención o sin entidad administradora (uso de INNER JOIN); Los tipos de documento y sexo se entregan siempre traducidos a etiquetas legibles, nunca como código crudo; Las fechas de nacimiento e inicio del procedimiento se devuelven sin componente de hora (CAST a DATE)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ESTADISTICA_ProcedimientoQuirurgico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Procedimiento quirúrgico; Diagnóstico; Código CUPS / IPS; Profesional de salud; Especialidad médica; Ingreso hospitalario; Grupo de atención; Entidad administradora de salud (EPS/ARS); Tipo de identificación; Ubicación geográfica (municipio)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ESTADISTICA_ProcedimientoQuirurgico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCQXINFOR: Cuando FECHORINI está entre las fechas parametrizadas, devuelve el detalle del procedimiento quirúrgico con datos del paciente, entidad y CUPS, ordenado por fecha de procedimiento', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ESTADISTICA_ProcedimientoQuirurgico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IPTIPODOC del paciente → Mapea el código numérico a literal: 1=Cédula de Ciudadanía, 2=Cédula de Extranjería, 3=Tarjeta de Identidad, 4=Registro Civil, 5=Pasaporte, 6=Adulto Sin Identificación else Cualquier otro valor se reporta como ''Menor Sin Identificación''; si IPSEXOPAC del paciente → 1=HOMBRE, 2=MUJER else Cualquier otro valor se reporta como INDEFINIDO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ESTADISTICA_ProcedimientoQuirurgico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCQXINFOR; dbo.INPROFSAL; dbo.INDIAGNOS; dbo.INESPECIA; dbo.INCUPSIPS; dbo.HCQXREALI; dbo.INPACIENT; dbo.INUBICACI; dbo.ADINGRESO; Contract.CareGroup; Contract.HealthAdministrator', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ESTADISTICA_ProcedimientoQuirurgico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ESTADISTICA_ProcedimientoQuirurgico';
-- GO
