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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de órdenes de imágenes diagnósticas (radiografías, ecografías, tomografías y similares) registradas en un rango de fechas. Para cada orden muestra el estado del servicio (Solicitado, Tomado, Interpretado, Anulado, entre otros), la fecha de la orden y de la toma, el código y descripción del procedimiento CUPS, el grupo de atención y la entidad pagadora (EPS/aseguradora) del ingreso, el usuario que tomó la imagen, el profesional que realizó la lectura o interpretación, y los datos completos del paciente (tipo y número de documento, nombres y apellidos). Consolida órdenes provenientes de dos fuentes distintas —órdenes ambulatorias (AMBORDIMA) y órdenes de historia clínica— uniéndolas con los datos de ingreso (ADINGRESO), el catálogo de servicios CUPS, los grupos de atención contractuales (CareGroup), la entidad pagadora (INENTIDAD), el maestro de pacientes (INPACIENT), el maestro de profesionales (INPROFSAL) y los usuarios del sistema (SEGusuaru). Se usa principalmente para seguimiento operativo y gerencial de la productividad del servicio de imágenes diagnósticas, control de lecturas pendientes y auditoría de tiempos de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Lecturas_Imagenes';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Lecturas_Imagenes';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reporta las órdenes de imágenes diagnósticas (ambulatorias y de historia clínica) en un rango de fechas, con su estado, paciente, entidad, profesional que lee y si la lectura fue realizada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Lecturas_Imagenes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El rango de fechas (@FechaIni, @FechaFin) debe estar definido y aplicarse sobre FECORDMED.; Las órdenes deben tener ingreso válido en ADINGRESO, entidad en INENTIDAD, paciente en INPACIENT, CUPS en Contract.CUPSEntity y grupo de atención en Contract.CareGroup; de lo contrario se excluyen del resultado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Lecturas_Imagenes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El estado ''2'' tiene semántica distinta según origen: ''Muestra Recolectada'' para órdenes ambulatorias y ''Procesando'' para órdenes de historia clínica.; Solo se incluyen registros cuya fecha de orden médica (FECORDMED) caiga dentro del rango solicitado.; El profesional que lee y el usuario que toma la imagen son opcionales (LEFT JOIN); su ausencia no excluye la orden.; La unión es UNION ALL, por lo que una misma orden presente en ambas fuentes aparecería duplicada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Lecturas_Imagenes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Órdenes de imágenes diagnósticas; Estado del servicio (CUPS); Lectura/interpretación de imágenes; Paciente y tipo de documento; Entidad responsable de pago; Grupo de atención; Ingreso hospitalario; Profesional de salud que lee', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Lecturas_Imagenes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando AMBORDIMA.FECORDMED está entre @FechaIni y @FechaFin, se retorna la orden de imagen ambulatoria con sus datos decodificados (estado, tipo de documento, lectura realizada).; [RETURN_RESULT] resultset: Cuando HCORDIMAG.FECORDMED está entre @FechaIni y @FechaFin, se retorna la orden de imagen de historia clínica unida (UNION ALL) al resultado anterior.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Lecturas_Imagenes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ESTSERIPS de la orden → Se traduce a etiqueta de estado: 1=Solicitado, 2=Muestra Recolectada/Procesando, 3=Tomado, 4=Interpretado, 5=Remitido, 6=Anulado else Cualquier otro valor se rotula como ''Extramural''; si IPTIPODOC del paciente → Se mapea al tipo de documento colombiano (CC, CE, TI, RC, PA, AS, MS, NU, CN, CD, SC) else Cualquier otro valor se interpreta como ''PE: Permiso especial de Permanencia''; si SERTRANSC = 0 → Lectura_realizada = ''NO'' else Lectura_realizada = ''SI''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Lecturas_Imagenes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AMBORDIMA; dbo.ADINGRESO; dbo.INENTIDAD; Contract.CUPSEntity; Contract.CareGroup; dbo.INPACIENT; dbo.INPROFSAL; dbo.SEGusuaru; dbo.HCORDIMAG', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Lecturas_Imagenes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Lecturas_Imagenes';
-- GO
