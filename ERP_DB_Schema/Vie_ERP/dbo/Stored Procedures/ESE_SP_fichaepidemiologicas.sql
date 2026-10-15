-- =============================================  
-- Author:  <Author,,yeny nuñez>  
-- ALTER date: <ALTER Date, 29/07/2019,>  
-- =============================================  
CREATE PROCEDURE [dbo].[ESE_SP_fichaepidemiologicas]
AS
    BEGIN
        SELECT F.CODEVENTO AS 'codigo evento', 
               F.NOMBEVENTO AS 'Nombre Evento', 
               F.FECHANOTIFI AS 'fecha notificacion', 
               F.RAZONSOCIAL AS 'centro salud', 
               P.IPPRIAPEL 'Apellido1', 
               P.IPSEGAPEL 'Apellido2', 
               P.IPPRINOMB 'Nombre1', 
               P.IPSEGNOMB 'Nombre2',
               CASE P.IPTIPODOC
                   WHEN '1'
                   THEN 'CC'
                   WHEN '2'
                   THEN 'CE'
                   WHEN '3'
                   THEN 'TI'
                   WHEN '4'
                   THEN 'RC'
                   WHEN '5'
                   THEN 'PS'
                   WHEN '6'
                   THEN 'AS'
                   WHEN '7'
                   THEN 'MS'
                   WHEN '8'
                   THEN 'NU'
               END 'Tipo Documento', 
               P.IPCODPACI, 
               F.EDAD AS 'Edad', 
               F.UNIDADMED AS 'Unidad Medida', 
               p.IDPAIS AS 'pais', 
               Z.Nationality AS 'Nacionalidad', 
               F.SEXO AS 'sexo', 
               F.CODPAIS AS 'codigo pais', 
               F.CODDEPARTAMENTO AS 'codigo dto', 
               F.AREA AS 'area', 
               F.LOCALIDAD AS 'Localidad', 
               F.BARRIO AS 'barrio', 
               f.VEREDA AS 'vereda', 
               f.DIRRESIDENCIA AS 'direccion', 
               F.OCUPACION, 
               F.TELNOTIFICA AS 'telefono',
               CASE P.IPTIPOPAC
                   WHEN '1'
                   THEN 'contributivo'
                   WHEN '2'
                   THEN 'Subsidiado'
                   WHEN '3'
                   THEN 'Vinculado'
                   WHEN '4'
                   THEN 'Particular'
                   WHEN '5'
                   THEN 'Otro'
                   WHEN '6'
                   THEN 'Despladado contributivo'
                   WHEN '7'
                   THEN 'Desplazado subisidiado'
                   WHEN '8'
                   THEN 'Desplazado no asegurado'
               END 'Regimen', 
               P.CODENTIDA AS 'Codigo Entidad', 
               en.Name AS 'Nombre Entidad', 
               F.ETNIA, 
               GR.DESGRUPET, 
               F.FECHACONSULTA AS 'fechaconsulta', 
               F.FECHAINICOSINTO AS 'fechainiciosintomas',
               CASE CLASIFICACIONCASO
                   WHEN '1'
                   THEN 'sospechoso'
                   WHEN '2'
                   THEN 'Probable'
                   WHEN '3'
                   THEN 'Conf.por laboratorio'
                   WHEN '4'
                   THEN 'Conf. clinica'
                   WHEN '5'
                   THEN 'Conf. nexo epedimiologico'
               END 'CLASIFICACIONCASO', 
               HOSPITALIZADO, 
               FECHAHOSP,
               CASE CONDICIONFINAL
                   WHEN '1'
                   THEN 'Vivo'
                   WHEN '2'
                   THEN 'Muerto'
                   WHEN '3'
                   THEN 'No sabe, no responde'
               END 'CONDICIONFINAL', 
               FECHADIFUNCION, 
               NUMCERTIFICADO, 
               CAUSAMUERTE, 
               TELNOTIFICA, 
               NOMPROFESIONAL, 
               MOTIVODESCAR, 
               OBSERVADESCARTE, 
               USUDESCARTA, 
               FECHADESCARTE, 
               USUREPORTASIVIGILA, 
               FECHAREPORSIVIGILA, 
               USUARIOVALIDO, 
               FECHAVALIDO, 
               ESTRATO, 
               FUENTE, 
               NACIONALIDAD
        FROM HCFICHANOTIFICACION F
             INNER JOIN INPACIENT P ON F.IPCODPACI = P.IPCODPACI
             LEFT JOIN Common.Country Z ON P.IDPAIS = Z.Id
             LEFT JOIN Contract.HealthAdministrator EN ON P.CODENTIDA = EN.Code
             LEFT JOIN ADGRUETNI GR ON F.ETNIA = GR.CODGRUPOE
        ORDER BY F.FECHACREACION;
    END;