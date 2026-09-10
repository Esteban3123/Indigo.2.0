CREATE PROCEDURE [dbo].[SP_ONCO_Circular022_AtencionTratamiento]
(
    @FechaInicial DATE,
    @FechaFinal DATE,
    @IDEntidadVIE VARCHAR(20),
    @Empresa VARCHAR(20)
)
AS
BEGIN
    SET NOCOUNT ON;

	WITH DATOS AS (
	
		SELECT
		    TipoRegistro=4, TipoID=CASE P.IPTIPODOC WHEN 1 THEN 'CC' WHEN 2 THEN 'CE' WHEN 3 THEN 'TI' WHEN 4 THEN 'RC' WHEN 6 THEN 'AS' WHEN 7 THEN 'MS' WHEN 9 THEN 'CN' WHEN 13 THEN 'PT' ELSE '' END,
		    Identificacion=RTRIM(P.IPCODPACI), CIE10= RTRIM(H.CODDIAGNO), CIE11='-', CUPSSolicitado = RTRIM(O.CODSERIPS), FechaSolicitud= O.FECORDMED, FechaRealizacion= Iif(O.FECHARESULT IS NULL, '-', FORMAT(O.FECHARESULT, 'yyy/MM/dd')),
		    EstadoVital= CASE H.[127] WHEN 1 THEN 'Vivo' WHEN 2 THEN 'Fallecido' ELSE 'Desconocido' END, H.FECHACREACION
		FROM HCONCOPREG H
		INNER JOIN INPACIENT P ON RTRIM(P.IPCODPACI)=RTRIM(H.[6])
		INNER JOIN HCORDLABO O ON P.IPCODPACI = O.IPCODPACI AND O.CODDIAGNO = H.CODDIAGNO
		INNER JOIN INCUPSIPS I ON I.CODSERIPS = O.CODSERIPS
		WHERE H.IDEntidadVIE=@IDEntidadVIE AND H.FECHACREACION>=@FechaInicial AND H.FECHACREACION<DATEADD(DAY,1,@FechaFinal)

		UNION ALL
		
		SELECT
		    TipoRegistro=4, TipoID=CASE P.IPTIPODOC WHEN 1 THEN 'CC' WHEN 2 THEN 'CE' WHEN 3 THEN 'TI' WHEN 4 THEN 'RC' WHEN 6 THEN 'AS' WHEN 7 THEN 'MS' WHEN 9 THEN 'CN' WHEN 13 THEN 'PT' ELSE '' END,
		    Identificacion=RTRIM(P.IPCODPACI), CIE10= RTRIM(H.CODDIAGNO), CIE11='-', CUPSSolicitado = RTRIM(O.CODSERIPS), FechaSolicitud= O.FECORDMED, FechaRealizacion= Iif(O.FECHARESULT IS NULL, '-', FORMAT(O.FECHARESULT, 'yyy/MM/dd')),
		    EstadoVital= CASE H.[127] WHEN 1 THEN 'Vivo' WHEN 2 THEN 'Fallecido' ELSE 'Desconocido' END, H.FECHACREACION
		FROM HCONCOPREG H
		INNER JOIN INPACIENT P ON RTRIM(P.IPCODPACI)=RTRIM(H.[6])
		INNER JOIN HCORDPATO O ON P.IPCODPACI = O.IPCODPACI AND O.CODDIAGNO = H.CODDIAGNO
		INNER JOIN INCUPSIPS I ON I.CODSERIPS = O.CODSERIPS
		WHERE H.IDEntidadVIE=@IDEntidadVIE AND H.FECHACREACION>=@FechaInicial AND H.FECHACREACION<DATEADD(DAY,1,@FechaFinal) AND O.ESTSERIPS NOT IN (5,6) --7

		UNION ALL
		
		SELECT
		    TipoRegistro=4, TipoID=CASE P.IPTIPODOC WHEN 1 THEN 'CC' WHEN 2 THEN 'CE' WHEN 3 THEN 'TI' WHEN 4 THEN 'RC' WHEN 6 THEN 'AS' WHEN 7 THEN 'MS' WHEN 9 THEN 'CN' WHEN 13 THEN 'PT' ELSE '' END,
		    Identificacion=RTRIM(P.IPCODPACI), CIE10= RTRIM(H.CODDIAGNO), CIE11='-', CUPSSolicitado = RTRIM(O.CODSERIPS), FechaSolicitud= O.FECORDMED, FechaRealizacion= IIF(O.FECHLECT   IS NULL, '-', FORMAT(O.FECHLECT, 'yyyy/MM/dd')),
		    EstadoVital= CASE H.[127] WHEN 1 THEN 'Vivo' WHEN 2 THEN 'Fallecido' ELSE 'Desconocido' END, H.FECHACREACION
		FROM HCONCOPREG H
		INNER JOIN INPACIENT P ON RTRIM(P.IPCODPACI)=RTRIM(H.[6])
		INNER JOIN HCORDIMAG O ON P.IPCODPACI = O.IPCODPACI AND O.CODDIAGNO = H.CODDIAGNO
		INNER JOIN INCUPSIPS I ON I.CODSERIPS = O.CODSERIPS
		WHERE H.IDEntidadVIE=@IDEntidadVIE AND H.FECHACREACION>=@FechaInicial AND H.FECHACREACION<DATEADD(DAY,1,@FechaFinal) AND O.ESTSERIPS IN (1, 2, 3, 4)

		UNION ALL
		
		SELECT
		    TipoRegistro=4, TipoID=CASE P.IPTIPODOC WHEN 1 THEN 'CC' WHEN 2 THEN 'CE' WHEN 3 THEN 'TI' WHEN 4 THEN 'RC' WHEN 6 THEN 'AS' WHEN 7 THEN 'MS' WHEN 9 THEN 'CN' WHEN 13 THEN 'PT' ELSE '' END,
		    Identificacion=RTRIM(P.IPCODPACI), CIE10= RTRIM(H.CODDIAGNO), CIE11='-', CUPSSolicitado = RTRIM(O.CODSERIPS), FechaSolicitud= O.FECORDMED, FechaRealizacion= IIF(O.FECHREALI IS NULL, '-', FORMAT(O.FECHREALI, 'yyyy/MM/dd')),
		    EstadoVital= CASE H.[127] WHEN 1 THEN 'Vivo' WHEN 2 THEN 'Fallecido' ELSE 'Desconocido' END, H.FECHACREACION
		FROM HCONCOPREG H
		INNER JOIN INPACIENT P ON RTRIM(P.IPCODPACI)=RTRIM(H.[6])
		INNER JOIN HCORDPRON O ON P.IPCODPACI = O.IPCODPACI AND O.CODDIAGNO = H.CODDIAGNO
		INNER JOIN INCUPSIPS I ON I.CODSERIPS = O.CODSERIPS
		WHERE H.IDEntidadVIE=@IDEntidadVIE AND H.FECHACREACION>=@FechaInicial AND H.FECHACREACION<DATEADD(DAY,1,@FechaFinal) AND O.ESTSERIPS NOT IN (4,5)

		UNION ALL
		
		SELECT
		    TipoRegistro=4, TipoID=CASE P.IPTIPODOC WHEN 1 THEN 'CC' WHEN 2 THEN 'CE' WHEN 3 THEN 'TI' WHEN 4 THEN 'RC' WHEN 6 THEN 'AS' WHEN 7 THEN 'MS' WHEN 9 THEN 'CN' WHEN 13 THEN 'PT' ELSE '' END,
		    Identificacion=RTRIM(P.IPCODPACI), CIE10= RTRIM(H.CODDIAGNO), CIE11='-', CUPSSolicitado = RTRIM(O.CODSERIPS), FechaSolicitud= O.FECORDMED, FechaRealizacion=IIF(O.FECHAPRO IS NULL, '-', FORMAT(O.FECHAPRO, 'yyyy/MM/dd')),
		    EstadoVital= CASE H.[127] WHEN 1 THEN 'Vivo' WHEN 2 THEN 'Fallecido' ELSE 'Desconocido' END, H.FECHACREACION
		FROM HCONCOPREG H
		INNER JOIN INPACIENT P ON RTRIM(P.IPCODPACI)=RTRIM(H.[6])
		INNER JOIN HCORDPROQ O ON P.IPCODPACI = O.IPCODPACI AND O.CODDIAGNO = H.CODDIAGNO
		INNER JOIN INCUPSIPS I ON I.CODSERIPS = O.CODSERIPS
		WHERE H.IDEntidadVIE=@IDEntidadVIE AND H.FECHACREACION>=@FechaInicial AND H.FECHACREACION<DATEADD(DAY,1,@FechaFinal) AND O.ESTSERIPS NOT IN (3,5,6)

		UNION ALL
		
		SELECT
		    TipoRegistro=4, TipoID=CASE P.IPTIPODOC WHEN 1 THEN 'CC' WHEN 2 THEN 'CE' WHEN 3 THEN 'TI' WHEN 4 THEN 'RC' WHEN 6 THEN 'AS' WHEN 7 THEN 'MS' WHEN 9 THEN 'CN' WHEN 13 THEN 'PT' ELSE '' END,
		    Identificacion=RTRIM(P.IPCODPACI), CIE10= RTRIM(H.CODDIAGNO), CIE11='-', CUPSSolicitado = RTRIM(O.CODSERIPS), FechaSolicitud= O.FECORDMED, FechaRealizacion= IIF(O.FECHAINT IS NULL, '-', FORMAT(O.FECHAINT, 'yyyy/MM/dd')),
		    EstadoVital= CASE H.[127] WHEN 1 THEN 'Vivo' WHEN 2 THEN 'Fallecido' ELSE 'Desconocido' END, H.FECHACREACION
		FROM HCONCOPREG H
		INNER JOIN INPACIENT P ON RTRIM(P.IPCODPACI)=RTRIM(H.[6])
		INNER JOIN HCORDINTE O ON P.IPCODPACI = O.IPCODPACI AND O.CODDIAGNO = H.CODDIAGNO
		INNER JOIN INCUPSIPS I ON I.CODSERIPS = O.CODSERIPS
		WHERE H.IDEntidadVIE=@IDEntidadVIE AND H.FECHACREACION>=@FechaInicial AND H.FECHACREACION<DATEADD(DAY,1,@FechaFinal) AND O.ESTSERIPS <> 5
	
	)

    SELECT Consecutivo=ROW_NUMBER()OVER(ORDER BY FECHACREACION), * FROM DATOS
END;
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento de reporte oncológico que consolida, para un rango de fechas y entidad aseguradora, las órdenes médicas de pacientes con registro en historia clínica oncológica (HCONCOPREG), cruzando seis tipos de órdenes: laboratorio, patología, imágenes diagnósticas, pronóstico, procedimientos quirúrgicos e intervenciones. Genera el tipo de registro 4 exigido por la Circular 022, incluyendo tipo y número de documento del paciente, diagnóstico CIE-10, código CUPS solicitado, fechas de solicitud y realización, y estado vital. Aplica filtros de estado para excluir órdenes canceladas o anuladas según cada modalidad.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_Circular022_AtencionTratamiento';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_Circular022_AtencionTratamiento';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte tipo 4 (Atención y Tratamiento) de la Circular 022 oncológica, consolidando órdenes de laboratorio, patología, imágenes, pronóstico, procedimientos quirúrgicos e intervenciones asociadas a diagnósticos oncológicos en un rango de fechas y entidad VIE.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_Circular022_AtencionTratamiento';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe existir en el maestro de pacientes con coincidencia por código (IPCODPACI = HCONCOPREG.[6]).; El servicio CUPS de cada orden debe existir en el catálogo de CUPS-IPS.; Las órdenes deben compartir el mismo código de diagnóstico (CODDIAGNO) que el registro oncológico.; El registro oncológico debe pertenecer a la entidad VIE indicada y haberse creado dentro del rango [FechaInicial, FechaFinal].', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_Circular022_AtencionTratamiento';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Todos los registros emitidos llevan TipoRegistro=4.; CIE11 siempre se reporta como ''-'' (no se calcula).; Cada orden incluida debe estar vinculada al mismo diagnóstico (CODDIAGNO) que el registro oncológico del paciente.; Solo se consideran registros oncológicos cuya FECHACREACION cae estrictamente antes de FechaFinal+1 día (rango inclusivo por día).; La numeración consecutiva se asigna ordenando por FECHACREACION del registro oncológico.; Los códigos CUPS se reportan sin espacios a la derecha (RTRIM).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_Circular022_AtencionTratamiento';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente oncológico; Diagnóstico CIE-10; CUPS; Orden de laboratorio; Orden de patología; Orden de imágenes diagnósticas; Pronóstico; Procedimiento quirúrgico; Intervención; Estado vital; Tipo de documento de identidad; Circular 022 (reporte VIE oncología)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_Circular022_AtencionTratamiento';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve un único conjunto unificado con TipoRegistro=4 que contiene órdenes de laboratorio, patología (excluye ESTSERIPS 5 y 6), imágenes (solo ESTSERIPS 1,2,3,4), pronóstico (excluye ESTSERIPS 4 y 5), procedimientos quirúrgicos (excluye ESTSERIPS 3,5,6) e intervenciones (ESTSERIPS distinto de 5), numerado por FECHACREACION.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_Circular022_AtencionTratamiento';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IPTIPODOC del paciente (1..13) → Mapea a código de tipo de documento: 1→CC, 2→CE, 3→TI, 4→RC, 6→AS, 7→MS, 9→CN, 13→PT else Cadena vacía; si HCONCOPREG.[127] (estado vital) → 1→''Vivo'', 2→''Fallecido'' else ''Desconocido''; si Fecha de realización/lectura/procedimiento NULL → Se devuelve ''-'' como FechaRealizacion else Se formatea la fecha (yyyy/MM/dd); si Origen de la orden = patología → Solo incluye si ESTSERIPS NOT IN (5,6); si Origen de la orden = imágenes → Solo incluye si ESTSERIPS IN (1,2,3,4); si Origen de la orden = pronóstico → Solo incluye si ESTSERIPS NOT IN (4,5); si Origen de la orden = procedimientos quirúrgicos → Solo incluye si ESTSERIPS NOT IN (3,5,6); si Origen de la orden = intervenciones → Solo incluye si ESTSERIPS <> 5', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_Circular022_AtencionTratamiento';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCONCOPREG; dbo.INPACIENT; dbo.HCORDLABO; dbo.HCORDPATO; dbo.HCORDIMAG; dbo.HCORDPRON; dbo.HCORDPROQ; dbo.HCORDINTE; dbo.INCUPSIPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_Circular022_AtencionTratamiento';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_Circular022_AtencionTratamiento';
-- GO
