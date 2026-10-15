-- =============================================  
-- Author:  <Author,,William Suaza>  
-- ALTER date: <ALTER Date, 13/05/2019,>  
-- Description: <Description, Registro de control paciente, triage y atención inicial de urgencias con todos los tiempos en cada proceso,>  
-- =============================================  
CREATE PROCEDURE [dbo].[ESE_SP_Asistencial_Triage] @FechaIni DATETIME, 
                                                  @FechaFin DATETIME
AS
    BEGIN
        SELECT CASE B.IPTIPODOC
                   WHEN '1'
                   THEN 'CC'
                   WHEN '2'
                   THEN 'CE'
                   WHEN '3'
                   THEN 'TI'
                   WHEN '4'
                   THEN 'RC'
                   WHEN '5'
                   THEN 'PA'
                   WHEN '6'
                   THEN 'AS'
                   WHEN '7'
                   THEN 'MS'
                   WHEN '8'
                   THEN 'NU'
                   WHEN '9'
                   THEN 'CN'
                   WHEN '10'
                   THEN 'CD'
                   WHEN '11'
                   THEN 'SC'
                   ELSE 'PE'
               END 'Tipo Doc Paciente', 
               A.IPCODPACI 'Paciente', 
               B.IPPRINOMB 'Primer Nombre', 
               B.IPSEGNOMB 'Segundo Nombre', 
               B.IPPRIAPEL 'Primer apellido', 
               B.IPSEGAPEL 'Segundo Apellido', 
               B.IPFECNACI 'F Nacimiento', 
               DATEDIFF(YEAR, B.IPFECNACI, A.TRIAFECHA) 'Edad', 
               C.CODCONCEC 'Concecutivo CP', 
               C.CODUSUARI 'Cod Usu CP', 
               D.NOMUSUARI 'Usu CP', 
               C.IPFECLLEGA 'Fec CP', 
               A.FECHINITR 'F. Inicia Triage', 
               A.TRIAFECHA 'F. Fin Triage', 
               A.CODPROSAL 'Cod Prof Triage', 
               E.NOMUSUARI 'Prof Triage',
               CASE A.TRIAGECLA
                   WHEN 1
                   THEN '1.Reanimación'
                   WHEN 2
                   THEN '2.Emergencia'
                   WHEN 3
                   THEN '3.Urgencia Medica'
                   WHEN 4
                   THEN '4.Urgencia Diferida'
                   WHEN 5
                   THEN '5.No Urgente'
               END 'Triage', 
               A.TRIANUMER 'Consecutivo Triage', 
               A.NUMINGRES 'Ingreso', 
               F.FECHINIHI 'F Inicia Atención', 
               F.FECHFINH 'F Finaliza atención', 
               F.CODPROSAL 'Cod Prof Consultorio', 
               H.NOMUSUARI 'Prof Consultorio', 
               F.CODCENATE 'Cod Centro Atención', 
               I.NOMCENATE 'Centro Atención', 
               F.UFUCODIGO 'Cod Unidad Funcional', 
               J.UFUDESCRI 'Unidad Funcional', 
               A.NUMINGRES 'Ingreso', 
               L.Code 'Cod Entidad', 
               L.Name 'Entidad'
        FROM.ADTRIAGEU A
            JOIN.INPACIENT B WITH(NOLOCK) ON B.IPCODPACI = A.IPCODPACI
            LEFT JOIN.ADCONTURG C WITH(NOLOCK) ON C.CODCONCEC = A.CODCONCEC
            JOIN.SEGusuaru D WITH(NOLOCK) ON D.CODUSUARI = C.CODUSUARI
            JOIN.SEGusuaru E WITH(NOLOCK) ON E.CODUSUARI = A.CODPROSAL
            LEFT JOIN.HCURGING1 F WITH(NOLOCK) ON F.NUMINGRES = A.NUMINGRES
            LEFT JOIN.INUNIFUNC G WITH(NOLOCK) ON G.UFUCODIGO = F.UFUCODIGO
            LEFT JOIN.SEGusuaru H WITH(NOLOCK) ON H.CODUSUARI = F.CODPROSAL
            LEFT JOIN.ADCENATEN I WITH(NOLOCK) ON I.CODCENATE = F.CODCENATE
            LEFT JOIN.INUNIFUNC J WITH(NOLOCK) ON J.UFUCODIGO = F.UFUCODIGO
            LEFT JOIN.ADINGRESO K WITH(NOLOCK) ON K.NUMINGRES = A.NUMINGRES
            LEFT JOIN Contract.HealthAdministrator L WITH(NOLOCK) ON L.Id = K.GENCONENTITY
        WHERE C.IPFECLLEGA BETWEEN @FechaIni AND @FechaFin
              AND G.UFUTIPUNI IN(1);
    END;