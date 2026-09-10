

CREATE VIEW [dbo].[IND_AD_HC_SeguimientoImagenesDiagnosticas]
AS

SELECT        'Intrahospitalario' AS TipoIngreso, IM.NUMINGRES AS INGRESO, IM.IPCODPACI AS DOCUMENTO, P.IPNOMCOMP AS NOMBRE,RTRIM(LTRIM(dbo.Edad(CONVERT(varchar, P.IPFECNACI, 105), CONVERT(varchar, GETDATE(), 105)))) AS Edad, M.TIPMODALI AS Modalidad ,IM.CODSERIPS AS COD_SERVICIO, C.DESSERIPS AS SERVICIO, 
                         im.CANSERIPS as Cantidad ,E.NOMENTIDA AS ENTIDAD, D .NOMDIAGNO AS DIAGNOSTICO, UU.UFUDESCRI AS UnidadFUncional,
                         CASE ESTSERIPS WHEN 1 THEN 'Solicitado' WHEN 2 THEN 'Estudio Realizado' WHEN 3 THEN 'Imagen Procesada' WHEN 4 THEN 'Estudio Interpretado' WHEN 5 THEN 'Remitido' WHEN 6 THEN 'Anulado' WHEN
                          7 THEN 'Extramural' END AS Estado, IM.FECORDMED AS FechaOrden, FECRECEXA AS FechaConfirmacion, U.NOMUSUARI AS UsuarioExamen
FROM            dbo.HCORDIMAG AS IM WITH (NOLOCK) INNER JOIN
                         dbo.ADINGRESO AS I WITH (NOLOCK) ON I.IPCODPACI = IM.IPCODPACI AND IM.NUMINGRES = I.NUMINGRES INNER JOIN
                         dbo.INPACIENT AS P WITH (NOLOCK) ON P.IPCODPACI = I.IPCODPACI INNER JOIN
                         dbo.INCUPSIPS AS C WITH (NOLOCK) ON C.CODSERIPS = IM.CODSERIPS INNER JOIN
                         dbo.INENTIDAD AS E WITH (NOLOCK) ON E.CODENTIDA = I.CODENTIDA INNER JOIN
                         dbo.INDIAGNOS AS D WITH (NOLOCK) ON D .CODDIAGNO = IM.CODDIAGNO INNER JOIN
						 DBO.HCINTESER AS M WITH (NOLOCK) ON M.CODSERIPS=IM.CODSERIPS and M.CODCENATE='001' left outer JOIN
						 DBO.SEGusuaru AS U WITH (NOLOCK) ON U.CODUSUARI=IM.USURECEXA inner join
						 dbo.INUNIFUNC AS UU WITH (NOLOCK) ON UU.UFUCODIGO=IM.UFUCODIGO
WHERE        (IM.FECORDMED >= '01/09/2016') AND IM.CODCENATE = '001' and ESTSERIPS not in ('7','6') /* and m.TIPMODALI='CT'*/
UNION
SELECT        'Ambulatorio' AS TipoIngreso, IM.NUMINGRES AS INGRESO, IM.IPCODPACI AS DOCUMENTO, P.IPNOMCOMP AS NOMBRE,RTRIM(LTRIM(dbo.Edad(CONVERT(varchar, P.IPFECNACI, 105), CONVERT(varchar, GETDATE(), 105)))) AS Edad, M.TIPMODALI AS Modalidad ,IM.CODSERIPS AS COD_SERVICIO, C.DESSERIPS AS SERVICIO, 
                         im.CANSERIPS as Cantidad, E.NOMENTIDA AS ENTIDAD, '' AS DIAGNOSTICO, UU.UFUDESCRI AS UnidadFUncional,
                         CASE ESTSERIPS WHEN 1 THEN 'Solicitado' WHEN 2 THEN 'Estudio Realizado' WHEN 3 THEN 'Imagen Procesada' WHEN 4 THEN 'Estudio Interpretado' WHEN 5 THEN 'Remitido' WHEN 6 THEN 'Anulado' WHEN
                          7 THEN 'Extramural' END AS Estado, IM.FECORDMED AS FechaOrden, FECRECEXA AS FechaConfirmacion, U.NOMUSUARI AS UsuarioExamen
FROM            dbo.AMBORDIMA AS IM INNER JOIN
                         dbo.ADINGRESO AS I WITH (NOLOCK) ON I.IPCODPACI = IM.IPCODPACI AND IM.NUMINGRES = I.NUMINGRES INNER JOIN
                         dbo.INPACIENT AS P WITH (NOLOCK) ON P.IPCODPACI = I.IPCODPACI INNER JOIN
                         dbo.INCUPSIPS AS C WITH (NOLOCK) ON C.CODSERIPS = IM.CODSERIPS INNER JOIN
                         dbo.INENTIDAD AS E WITH (NOLOCK) ON E.CODENTIDA = I.CODENTIDA INNER JOIN
						 DBO.HCINTESER AS M WITH (NOLOCK) ON M.CODSERIPS=IM.CODSERIPS and M.CODCENATE='001' left outer JOIN
						 DBO.SEGusuaru AS U WITH (NOLOCK) ON U.CODUSUARI=IM.USURECEXA inner join
						 dbo.INUNIFUNC AS UU ON UU.UFUCODIGO=IM.UFUCODIGO
WHERE        (IM.FECORDMED >= '01/09/2016') AND IM.CODCENATE = '001' and ESTSERIPS not in ('7','6')
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Seguimiento de órdenes de imágenes diagnósticas (radiografías, ecografías, tomografías, resonancias y otros estudios) tanto de pacientes intrahospitalarios como ambulatorios, para el centro de atención ''001''. Consolida en una sola consulta las órdenes de imágenes de historia clínica hospitalaria (HCORDIMAG) y las ambulatorias (AMBORDIMA), enriqueciendo cada orden con datos del paciente (cédula, nombre, edad), la entidad aseguradora o pagador (EPS/ARS), el servicio CUPS solicitado, la modalidad de imagen (CT, RX, ECO, etc.), la unidad funcional donde se generó la orden, el diagnóstico CIE-10 asociado y el usuario que confirmó el examen. Excluye estudios anulados y extramurales, y muestra el estado del proceso diagnóstico (Solicitado, Estudio Realizado, Imagen Procesada, Estudio Interpretado, Remitido). Sirve como base para reportes de gestión, monitoreo de tiempos de respuesta en imagenología y control operativo del servicio de imágenes diagnósticas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'IND_AD_HC_SeguimientoImagenesDiagnosticas';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'IND_AD_HC_SeguimientoImagenesDiagnosticas';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un único listado el seguimiento de órdenes de imágenes diagnósticas tanto intrahospitalarias como ambulatorias, mostrando estado, paciente, entidad, diagnóstico, servicio y usuario que recepciona el examen.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_HC_SeguimientoImagenesDiagnosticas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existir registros en HCORDIMAG (intrahospitalario) o AMBORDIMA (ambulatorio) con FECORDMED >= ''01/09/2016''; Las órdenes deben pertenecer al centro de atención CODCENATE=''001''; Debe existir el ingreso asociado en ADINGRESO con coincidencia de paciente y número de ingreso; Deben existir el servicio (INCUPSIPS), entidad (INENTIDAD), unidad funcional (INUNIFUNC) y configuración del servicio en HCINTESER para CODCENATE=''001''; Para el bloque intrahospitalario debe existir el diagnóstico en INDIAGNOS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_HC_SeguimientoImagenesDiagnosticas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen órdenes del centro de atención ''001''; Nunca se muestran órdenes en estado Anulado (6) ni Extramural (7); Solo órdenes con fecha de orden médica desde el 01/09/2016 en adelante; Las órdenes ambulatorias siempre se devuelven sin diagnóstico; La edad del paciente se calcula a la fecha actual mediante dbo.Edad; El usuario que recepciona el examen es opcional (LEFT JOIN sobre SEGusuaru)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_HC_SeguimientoImagenesDiagnosticas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Imágenes diagnósticas; Orden médica; Ingreso intrahospitalario; Atención ambulatoria; Modalidad de imagen; Diagnóstico CIE; Entidad/aseguradora; Unidad funcional; Estado de orden (Solicitado, Realizado, Procesado, Interpretado, Remitido, Anulado, Extramural); Centro de atención', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_HC_SeguimientoImagenesDiagnosticas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Se retorna conjunto unificado (UNION) de órdenes de imagen intrahospitalarias y ambulatorias filtrando FECORDMED >= ''01/09/2016'', CODCENATE=''001'' y excluyendo estados 6 (Anulado) y 7 (Extramural)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_HC_SeguimientoImagenesDiagnosticas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Origen de la orden es HCORDIMAG → Se etiqueta TipoIngreso=''Intrahospitalario'' e incluye el diagnóstico desde INDIAGNOS; si Origen de la orden es AMBORDIMA → Se etiqueta TipoIngreso=''Ambulatorio'' y el diagnóstico se devuelve vacío; si ESTSERIPS = 1/2/3/4/5/6/7 → Se traduce a estado textual: Solicitado / Estudio Realizado / Imagen Procesada / Estudio Interpretado / Remitido / Anulado / Extramural else NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_HC_SeguimientoImagenesDiagnosticas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Edad', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_HC_SeguimientoImagenesDiagnosticas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDIMAG; dbo.ADINGRESO; dbo.INPACIENT; dbo.INCUPSIPS; dbo.INENTIDAD; dbo.INDIAGNOS; dbo.HCINTESER; dbo.SEGusuaru; dbo.INUNIFUNC; dbo.AMBORDIMA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_HC_SeguimientoImagenesDiagnosticas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_HC_SeguimientoImagenesDiagnosticas';
GO
