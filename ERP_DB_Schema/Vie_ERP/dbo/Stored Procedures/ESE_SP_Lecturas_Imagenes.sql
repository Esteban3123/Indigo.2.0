CREATE PROCEDURE [dbo].[ESE_SP_Lecturas_Imagenes] 
-- Add the parameters for the stored procedure here
@FechaIni DATETIME, 
@FechaFin DATETIME
AS
    BEGIN
        -- SET NOCOUNT ON added to prevent extra result sets from
        -- interfering with SELECT statements.
        SET NOCOUNT ON;

        -- Insert statements for procedure here
        SELECT CASE AM.ESTSERIPS
                   WHEN '1'
                   THEN 'Solicitado'
                   WHEN '2'
                   THEN 'Muestra Recolectada'
                   WHEN '3'
                   THEN 'Tomado'
                   WHEN '4'
                   THEN 'Interpretado'
                   WHEN '5'
                   THEN 'Remitido'
                   WHEN '6'
                   THEN 'Anulado'
                   ELSE 'Extramural'
               END AS Estado, 
               AM.FECORDMED AS Fecha_orden, 
               AM.CODSERIPS AS Cod_Servicio, 
               CUPS.Description AS CUPS, 
               CG.Code AS Cod_Grupo_Atencion, 
               CG.Name AS Grupo_Atencion, 
               I.CODENTIDA AS Cod_Entidad, 
               EN.NOMENTIDA AS Entidad, 
               AM.USURECEXA AS Cod_Usu_Toma_Imagen, 
               U.NOMUSUARI AS Usu_Toma_Imagen, 
               AM.FECRECEXA AS Fec_Toma_Imagen, 
               I.NUMINGRES AS Ingreso,
               CASE P.IPTIPODOC
                   WHEN '1'
                   THEN 'CC: Cédula de Ciudadanía'
                   WHEN '2'
                   THEN 'CE: Cédula de Extranjería'
                   WHEN '3'
                   THEN 'TI: Tarjeta de Identidad'
                   WHEN '4'
                   THEN 'RC: Registro Civil'
                   WHEN '5'
                   THEN 'PA: Pasaporte'
                   WHEN '6'
                   THEN 'AS: Adulto Sin Identificación'
                   WHEN '7'
                   THEN 'MS: Menor Sin Identificación'
                   WHEN '8'
                   THEN 'NU: Número único de identificación personal'
                   WHEN '9'
                   THEN 'CN: Certificado Nacido Vivo'
                   WHEN '10'
                   THEN 'CD: Carnet Diplomático'
                   WHEN '11'
                   THEN 'SC: Salvoconducto'
                   ELSE 'PE: Permiso especial de Permanencia'
               END AS Tipo_Documento, 
               P.IPCODPACI AS Documento, 
               P.IPPRIAPEL AS Primer_Apellido, 
               P.IPSEGAPEL AS Segundo_Apellido, 
               P.IPPRINOMB AS Primer_Nombre, 
               P.IPSEGNOMB AS Segundo_Nombre, 
               AM.FECTRASER AS Fec_lectura, 
               AM.CODPROVAL AS Cod_Prof_Lee, 
               PROF.NOMMEDICO AS Prof_lee,
               CASE AM.SERTRANSC
                   WHEN 0
                   THEN 'NO'
                   ELSE 'SI'
               END AS Lectura_realizada
        FROM.AMBORDIMA AS AM
            INNER JOIN.ADINGRESO AS I WITH(NOLOCK) ON i.NUMINGRES = AM.NUMINGRES
            INNER JOIN.INENTIDAD AS EN WITH(NOLOCK) ON EN.CODENTIDA = I.CODENTIDA
            INNER JOIN Contract.CUPSEntity AS CUPS WITH(NOLOCK) ON CUPS.Code = AM.CODSERIPS
            INNER JOIN Contract.CareGroup AS CG WITH(NOLOCK) ON CG.Id = I.GENCAREGROUP
            INNER JOIN.INPACIENT AS P WITH(NOLOCK) ON P.IPCODPACI = AM.IPCODPACI
            LEFT OUTER JOIN.INPROFSAL AS PROF WITH(NOLOCK) ON PROF.CODPROSAL = AM.CODPROVAL
            LEFT OUTER JOIN.SEGusuaru AS U WITH(NOLOCK) ON AM.USURECEXA = U.CODUSUARI
        WHERE AM.FECORDMED BETWEEN @FechaIni AND @FechaFin
        UNION ALL
        SELECT CASE HC.ESTSERIPS
                   WHEN '1'
                   THEN 'Solicitado'
                   WHEN '2'
                   THEN 'Procesando'
                   WHEN '3'
                   THEN 'Tomado'
                   WHEN '4'
                   THEN 'Interpretado'
                   WHEN '5'
                   THEN 'Remitido'
                   WHEN '6'
                   THEN 'Anulado'
                   ELSE 'Extramural'
               END AS Estado, 
               HC.FECORDMED AS Fecha_orden, 
               HC.CODSERIPS AS Cod_Servicio, 
               CUPS.Description AS CUPS, 
               CG.Code AS Cod_Grupo_Atencion, 
               CG.Name AS Grupo_Atencion, 
               I.CODENTIDA AS Cod_Entidad, 
               EN.NOMENTIDA AS Entidad, 
               HC.USURECEXA AS Cod_Usu_Toma_Imagen, 
               U.NOMUSUARI AS Usu_Toma_Imagen, 
               HC.FECRECEXA AS Fec_Toma_Imagen, 
               I.NUMINGRES AS Ingreso,
               CASE P.IPTIPODOC
                   WHEN '1'
                   THEN 'CC: Cédula de Ciudadanía'
                   WHEN '2'
                   THEN 'CE: Cédula de Extranjería'
                   WHEN '3'
                   THEN 'TI: Tarjeta de Identidad'
                   WHEN '4'
                   THEN 'RC: Registro Civil'
                   WHEN '5'
                   THEN 'PA: Pasaporte'
                   WHEN '6'
                   THEN 'AS: Adulto Sin Identificación'
                   WHEN '7'
                   THEN 'MS: Menor Sin Identificación'
                   WHEN '8'
                   THEN 'NU: Número único de identificación personal'
                   WHEN '9'
                   THEN 'CN: Certificado Nacido Vivo'
                   WHEN '10'
                   THEN 'CD: Carnet Diplomático'
                   WHEN '11'
                   THEN 'SC: Salvoconducto'
                   ELSE 'PE: Permiso especial de Permanencia'
               END AS Tipo_Documento, 
               P.IPCODPACI AS Documento, 
               P.IPPRIAPEL AS Primer_Apellido, 
               P.IPSEGAPEL AS Segundo_Apellido, 
               P.IPPRINOMB AS Primer_Nombre, 
               P.IPSEGNOMB AS Segundo_Nombre, 
               HC.FECTRASER AS Fec_lectura, 
               HC.CODPROVAL AS Cod_Prof_Lee, 
               PROF.NOMMEDICO AS Prof_lee,
               CASE HC.SERTRANSC
                   WHEN 0
                   THEN 'NO'
                   ELSE 'SI'
               END AS Lectura_realizada
        FROM.HCORDIMAG AS HC
            INNER JOIN.ADINGRESO AS I WITH(NOLOCK) ON i.NUMINGRES = HC.NUMINGRES
            INNER JOIN.INENTIDAD AS EN WITH(NOLOCK) ON EN.CODENTIDA = I.CODENTIDA
            INNER JOIN Contract.CUPSEntity AS CUPS WITH(NOLOCK) ON CUPS.Code = HC.CODSERIPS
            INNER JOIN Contract.CareGroup AS CG WITH(NOLOCK) ON CG.Id = I.GENCAREGROUP
            INNER JOIN.INPACIENT AS P WITH(NOLOCK) ON P.IPCODPACI = HC.IPCODPACI
            LEFT OUTER JOIN.INPROFSAL AS PROF WITH(NOLOCK) ON PROF.CODPROSAL = HC.CODPROVAL
            LEFT OUTER JOIN.SEGusuaru AS U WITH(NOLOCK) ON HC.USURECEXA = U.CODUSUARI
        WHERE HC.FECORDMED BETWEEN @FechaIni AND @FechaFin; --@FechaIni AND @FechaFin
    END;