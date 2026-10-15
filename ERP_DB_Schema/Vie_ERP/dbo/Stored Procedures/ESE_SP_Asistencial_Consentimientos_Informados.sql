-- =============================================  
-- Author:  <Author,,William Suaza>  
-- ALTER date: <ALTER Date, 13/05/2019,>  
-- Description: <Description, Consulta los registros de los consentimientos informados adjunto,>  
-- =============================================  
CREATE PROCEDURE [dbo].[ESE_SP_Asistencial_Consentimientos_Informados]
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
                   THEN 'RC'
                   WHEN '9'
                   THEN 'CN'
                   WHEN '10'
                   THEN 'CD'
                   WHEN '11'
                   THEN 'SC'
                   ELSE 'PE'
               END AS Tipo_Documento, 
               A.IPCODPACI 'Doc Paciente', 
               B.IPPRINOMB 'Primer Nombre', 
               B.IPSEGNOMB 'Segundo nombre', 
               B.IPPRIAPEL 'Primer Apellido', 
               B.IPSEGAPEL 'Segundo apellido', 
               A.NUMINGRES 'Ingreso', 
               A.FECPROCES 'F. Adjunto', 
               A.CODCENATE 'Cod. Centro Atención', 
               C.NOMCENATE 'Centro Atención', 
               A.UFUCODIGO 'Cod. Unidad Funcional', 
               D.UFUDESCRI 'Unidad funcional', 
               A.CODUSUARI 'Cod Usuario adjunta', 
               E.NOMUSUARI
        FROM.HCDOCUMAD A
            JOIN.INPACIENT B WITH(NOLOCK) ON B.IPCODPACI = A.IPCODPACI
            JOIN.ADCENATEN C WITH(NOLOCK) ON C.CODCENATE = A.CODCENATE
            JOIN.INUNIFUNC D WITH(NOLOCK) ON D.UFUCODIGO = A.UFUCODIGO
            JOIN.SEGusuaru E WITH(NOLOCK) ON E.CODUSUARI = A.CODUSUARI
        WHERE A.FECPROCES BETWEEN '01/05/2019' AND '30/05/2019'
              AND A.TIPODOCUM = 7;
    END;