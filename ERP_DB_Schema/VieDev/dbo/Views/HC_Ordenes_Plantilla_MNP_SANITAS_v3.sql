
CREATE VIEW [dbo].[HC_Ordenes_Plantilla_MNP_SANITAS_v3]
AS
/* 1 Ordenes de Imagenes */ SELECT CONVERT(VARCHAR(10), GETDATE(), 103) AS [Fecha_Solicitud], '900559103' AS [Nit Ips Remitente], '14737' AS [Código_Sucursal_Remitente], 
                         CASE p1.IPTIPODOC WHEN '1' THEN 'CC' WHEN '2' THEN 'CE' WHEN '3' THEN 'TI' WHEN '4' THEN 'RC' WHEN '5' THEN 'PA' WHEN '6' THEN 'AS' WHEN '7' THEN 'MS' WHEN '8' THEN 'NUIP' WHEN '12' THEN 'PE' END AS [Tipo_Identificación_del_Afiliado],
                          RTRIM(o1.IPCODPACI) AS [Número_de_Identificación_del_Afiliado], RTRIM(p1.IPNOMCOMP) AS [Nombre_Paciente], CASE WHEN p1.IPTELEFON = ' ' THEN RTRIM(p1.IPTELMOVI) WHEN p1.IPTELEFON IS NOT NULL 
                         THEN RTRIM(p1.IPTELEFON) END AS [Telefono_Celular_1], CONVERT(VARCHAR(10), o1.FECORDMED, 103) AS [Fecha_Atencion], RTRIM(i1.CODDIAING) AS [CIE10], RTRIM(m1.NOMMEDICO) AS [Nombre_Medico], 
                         RTRIM(e1.DESESPECI) AS [Especialidad_Remitente], '900559103' AS [Nit Ips Practica], '14737' AS [Código_Sucursal_Practicante], RTRIM(o1.CODSERIPS) AS [Código_CUPS_Prestacion], RTRIM(s1.DESSERIPS) 
                         AS [Descripcion_Prestacion], 
                         CASE WHEN s1.DESSERIPS LIKE '%CONTRAS%' THEN 'T' ELSE '' END + '-' + CASE WHEN s1.DESSERIPS LIKE '%COMPARA%' THEN 'C' ELSE '' END + '-' + CASE WHEN s1.DESSERIPS LIKE '%BILATER%' THEN 'B' ELSE '' END AS
                          [Atributo], RTRIM(o1.CANSERIPS) AS [Cantidad], RTRIM(o1.OBSSERIPS) AS [Justificacion_Clinica], '' AS [AUTORIZACION ANULAR], '' AS [OBSERVACION 1], '' AS [IMPRIMIR 1 Visible], '' AS [OBSERVACION 2], 
                         '' AS [IMPRIMIR 2 Visible], '' AS [OBSERVACION 3], '' AS [IMPRIMIR 3 No Visible], ' ' AS [ ], CONVERT(VARCHAR(10), GETDATE(), 110) AS [Día Actual], RTRIM(h1.DATOBJETI) AS [Nota Evolución], RTRIM(u1.UFUDESCRI) 
                         AS [Unidad Funcional], RTRIM(i1.GENCAREGROUP) AS [Codigo EAPB], RTRIM(g1.Name) AS [Entidad EAPB], RTRIM(o1.NUMINGRES) AS [No Ingreso], 'IMG' AS [Tipo Orden]
FROM            dbo.HCORDIMAG AS o1 LEFT OUTER JOIN
                         dbo.INPACIENT AS p1 WITH (NOLOCK) ON o1.IPCODPACI = p1.IPCODPACI LEFT OUTER JOIN
                         dbo.ADINGRESO AS i1 WITH (NOLOCK) ON o1.NUMINGRES = i1.NUMINGRES LEFT OUTER JOIN
                         dbo.INPROFSAL AS m1 WITH (NOLOCK) ON o1.CODPROSAL = m1.CODPROSAL LEFT OUTER JOIN
                         dbo.INESPECIA AS e1 WITH (NOLOCK) ON m1.CODESPEC1 = e1.CODESPECI LEFT OUTER JOIN
                         Contract.CareGroup AS g1 WITH (NOLOCK) ON i1.GENCAREGROUP = g1.Id LEFT OUTER JOIN
                         dbo.INCUPSIPS AS s1 WITH (NOLOCK) ON o1.CODSERIPS = s1.CODSERIPS LEFT OUTER JOIN
                         dbo.INUNIFUNC AS u1 WITH (NOLOCK) ON o1.UFUCODIGO = u1.UFUCODIGO LEFT OUTER JOIN
                         dbo.HCHISPACA AS h1 WITH (NOLOCK) ON o1.NUMINGRES = h1.NUMINGRES AND o1.NUMEFOLIO = h1.NUMEFOLIO
WHERE        FECORDMED >= '01-11-2023' AND i1.GENCONENTITY IN ('40', '190') AND o1.MANEXTPRO = '1'
UNION
/* 2 Ordenes de Interconsultas */ SELECT CONVERT(VARCHAR(10), GETDATE(), 103) AS [Fecha_Solicitud], '900559103' AS [Nit Ips Remitente], '14737' AS [Código_Sucursal_Remitente], 
                         CASE p1.IPTIPODOC WHEN '1' THEN 'CC' WHEN '2' THEN 'CE' WHEN '3' THEN 'TI' WHEN '4' THEN 'RC' WHEN '5' THEN 'PA' WHEN '6' THEN 'AS' WHEN '7' THEN 'MS' WHEN '8' THEN 'NUIP' WHEN '12' THEN 'PE' END AS [Tipo_Identificación_del_Afiliado],
                          RTRIM(o1.IPCODPACI) AS [Número_de_Identificación_del_Afiliado], RTRIM(p1.IPNOMCOMP) AS [Nombre_Paciente], CASE WHEN p1.IPTELEFON = ' ' THEN RTRIM(p1.IPTELMOVI) WHEN p1.IPTELEFON IS NOT NULL 
                         THEN RTRIM(p1.IPTELEFON) END AS [Telefono_Celular_1], CONVERT(VARCHAR(10), o1.FECORDMED, 103) AS [Fecha_Atencion], RTRIM(i1.CODDIAING) AS [CIE10], RTRIM(m1.NOMMEDICO) AS [Nombre_Medico], 
                         RTRIM(e1.DESESPECI) AS [Especialidad_Remitente], '900559103' AS [Nit Ips Practica], '14737' AS [Código_Sucursal_Practicante], RTRIM(o1.CODSERIPS) AS [Código_CUPS_Prestacion], RTRIM(s1.DESSERIPS) 
                         AS [Descripcion_Prestacion], 
                         CASE WHEN s1.DESSERIPS LIKE '%CONTRAS%' THEN 'T' ELSE '' END + '-' + CASE WHEN s1.DESSERIPS LIKE '%COMPARA%' THEN 'C' ELSE '' END + '-' + CASE WHEN s1.DESSERIPS LIKE '%BILATER%' THEN 'B' ELSE '' END AS
                          [Atributo], RTRIM(o1.CANSERIPS) AS [Cantidad], RTRIM(o1.OBSSERIPS) AS [Justificacion_Clinica], '' AS [AUTORIZACION ANULAR], '' AS [OBSERVACION 1], '' AS [IMPRIMIR 1 Visible], '' AS [OBSERVACION 2], 
                         '' AS [IMPRIMIR 2 Visible], '' AS [OBSERVACION 3], '' AS [IMPRIMIR 3 No Visible], ' ' AS [ ], CONVERT(VARCHAR(10), GETDATE(), 110) AS [Día Actual], RTRIM(h1.DATOBJETI) AS [Nota Evolución], RTRIM(u1.UFUDESCRI) 
                         AS [Unidad Funcional], RTRIM(i1.GENCAREGROUP) AS [Codigo EAPB], RTRIM(g1.Name) AS [Entidad EAPB], RTRIM(o1.NUMINGRES) AS [No Ingreso], 'INT' AS [Tipo Orden]
FROM            dbo.HCORDINTE AS o1 LEFT OUTER JOIN
                         dbo.INPACIENT AS p1 WITH (NOLOCK) ON o1.IPCODPACI = p1.IPCODPACI LEFT OUTER JOIN
                         dbo.ADINGRESO AS i1 WITH (NOLOCK) ON o1.NUMINGRES = i1.NUMINGRES LEFT OUTER JOIN
                         dbo.INPROFSAL AS m1 WITH (NOLOCK) ON o1.CODPROSAL = m1.CODPROSAL LEFT OUTER JOIN
                         dbo.INESPECIA AS e1 WITH (NOLOCK) ON m1.CODESPEC1 = e1.CODESPECI LEFT OUTER JOIN
                         Contract.CareGroup AS g1 WITH (NOLOCK) ON i1.GENCAREGROUP = g1.Id LEFT OUTER JOIN
                         dbo.INCUPSIPS AS s1 WITH (NOLOCK) ON o1.CODSERIPS = s1.CODSERIPS LEFT OUTER JOIN
                         dbo.INUNIFUNC AS u1 WITH (NOLOCK) ON o1.UFUCODIGO = u1.UFUCODIGO LEFT OUTER JOIN
                         dbo.HCHISPACA AS h1 WITH (NOLOCK) ON o1.NUMINGRES = h1.NUMINGRES AND o1.NUMEFOLIO = h1.NUMEFOLIO
WHERE        FECORDMED >= '01-11-2023' AND i1.GENCONENTITY IN ('40', '190') AND o1.MANEXTPRO = '1'
UNION
/* 3 Ordenes de Laboratorio */ SELECT CONVERT(VARCHAR(10), GETDATE(), 103) AS [Fecha_Solicitud], '900559103' AS [Nit Ips Remitente], '14737' AS [Código_Sucursal_Remitente], 
                         CASE p1.IPTIPODOC WHEN '1' THEN 'CC' WHEN '2' THEN 'CE' WHEN '3' THEN 'TI' WHEN '4' THEN 'RC' WHEN '5' THEN 'PA' WHEN '6' THEN 'AS' WHEN '7' THEN 'MS' WHEN '8' THEN 'NUIP' WHEN '12' THEN 'PE' END AS [Tipo_Identificación_del_Afiliado],
                          RTRIM(o1.IPCODPACI) AS [Número_de_Identificación_del_Afiliado], RTRIM(p1.IPNOMCOMP) AS [Nombre_Paciente], CASE WHEN p1.IPTELEFON = ' ' THEN RTRIM(p1.IPTELMOVI) WHEN p1.IPTELEFON IS NOT NULL 
                         THEN RTRIM(p1.IPTELEFON) END AS [Telefono_Celular_1], CONVERT(VARCHAR(10), o1.FECORDMED, 103) AS [Fecha_Atencion], RTRIM(i1.CODDIAING) AS [CIE10], RTRIM(m1.NOMMEDICO) AS [Nombre_Medico], 
                         RTRIM(e1.DESESPECI) AS [Especialidad_Remitente], '900559103' AS [Nit Ips Practica], '14737' AS [Código_Sucursal_Practicante], RTRIM(o1.CODSERIPS) AS [Código_CUPS_Prestacion], RTRIM(s1.DESSERIPS) 
                         AS [Descripcion_Prestacion], 
                         CASE WHEN s1.DESSERIPS LIKE '%CONTRAS%' THEN 'T' ELSE '' END + '-' + CASE WHEN s1.DESSERIPS LIKE '%COMPARA%' THEN 'C' ELSE '' END + '-' + CASE WHEN s1.DESSERIPS LIKE '%BILATER%' THEN 'B' ELSE '' END AS
                          [Atributo], RTRIM(o1.CANSERIPS) AS [Cantidad], RTRIM(o1.OBSSERIPS) AS [Justificacion_Clinica], '' AS [AUTORIZACION ANULAR], '' AS [OBSERVACION 1], '' AS [IMPRIMIR 1 Visible], '' AS [OBSERVACION 2], 
                         '' AS [IMPRIMIR 2 Visible], '' AS [OBSERVACION 3], '' AS [IMPRIMIR 3 No Visible], ' ' AS [ ], CONVERT(VARCHAR(10), GETDATE(), 110) AS [Día Actual], RTRIM(h1.DATOBJETI) AS [Nota Evolución], RTRIM(u1.UFUDESCRI) 
                         AS [Unidad Funcional], RTRIM(i1.GENCAREGROUP) AS [Codigo EAPB], RTRIM(g1.Name) AS [Entidad EAPB], RTRIM(o1.NUMINGRES) AS [No Ingreso], 'LAB' AS [Tipo Orden]
FROM            dbo.HCORDLABO AS o1 LEFT OUTER JOIN
                         dbo.INPACIENT AS p1 WITH (NOLOCK) ON o1.IPCODPACI = p1.IPCODPACI LEFT OUTER JOIN
                         dbo.ADINGRESO AS i1 WITH (NOLOCK) ON o1.NUMINGRES = i1.NUMINGRES LEFT OUTER JOIN
                         dbo.INPROFSAL AS m1 WITH (NOLOCK) ON o1.CODPROSAL = m1.CODPROSAL LEFT OUTER JOIN
                         dbo.INESPECIA AS e1 WITH (NOLOCK) ON m1.CODESPEC1 = e1.CODESPECI LEFT OUTER JOIN
                         Contract.CareGroup AS g1 WITH (NOLOCK) ON i1.GENCAREGROUP = g1.Id LEFT OUTER JOIN
                         dbo.INCUPSIPS AS s1 WITH (NOLOCK) ON o1.CODSERIPS = s1.CODSERIPS LEFT OUTER JOIN
                         dbo.INUNIFUNC AS u1 WITH (NOLOCK) ON o1.UFUCODIGO = u1.UFUCODIGO LEFT OUTER JOIN
                         dbo.HCHISPACA AS h1 WITH (NOLOCK) ON o1.NUMINGRES = h1.NUMINGRES AND o1.NUMEFOLIO = h1.NUMEFOLIO
WHERE        FECORDMED >= '01-11-2023' AND i1.GENCONENTITY IN ('40', '190') AND o1.MANEXTPRO = '1'
UNION
/* 4 Ordenes de Patologias */ SELECT CONVERT(VARCHAR(10), GETDATE(), 103) AS [Fecha_Solicitud], '900559103' AS [Nit Ips Remitente], '14737' AS [Código_Sucursal_Remitente], 
                         CASE p1.IPTIPODOC WHEN '1' THEN 'CC' WHEN '2' THEN 'CE' WHEN '3' THEN 'TI' WHEN '4' THEN 'RC' WHEN '5' THEN 'PA' WHEN '6' THEN 'AS' WHEN '7' THEN 'MS' WHEN '8' THEN 'NUIP' WHEN '12' THEN 'PE' END AS [Tipo_Identificación_del_Afiliado],
                          RTRIM(o1.IPCODPACI) AS [Número_de_Identificación_del_Afiliado], RTRIM(p1.IPNOMCOMP) AS [Nombre_Paciente], CASE WHEN p1.IPTELEFON = ' ' THEN RTRIM(p1.IPTELMOVI) WHEN p1.IPTELEFON IS NOT NULL 
                         THEN RTRIM(p1.IPTELEFON) END AS [Telefono_Celular_1], CONVERT(VARCHAR(10), o1.FECORDMED, 103) AS [Fecha_Atencion], RTRIM(i1.CODDIAING) AS [CIE10], RTRIM(m1.NOMMEDICO) AS [Nombre_Medico], 
                         RTRIM(e1.DESESPECI) AS [Especialidad_Remitente], '900559103' AS [Nit Ips Practica], '14737' AS [Código_Sucursal_Practicante], RTRIM(o1.CODSERIPS) AS [Código_CUPS_Prestacion], RTRIM(s1.DESSERIPS) 
                         AS [Descripcion_Prestacion], 
                         CASE WHEN s1.DESSERIPS LIKE '%CONTRAS%' THEN 'T' ELSE '' END + '-' + CASE WHEN s1.DESSERIPS LIKE '%COMPARA%' THEN 'C' ELSE '' END + '-' + CASE WHEN s1.DESSERIPS LIKE '%BILATER%' THEN 'B' ELSE '' END AS
                          [Atributo], RTRIM(o1.CANSERIPS) AS [Cantidad], RTRIM(o1.OBSSERIPS) AS [Justificacion_Clinica], '' AS [AUTORIZACION ANULAR], '' AS [OBSERVACION 1], '' AS [IMPRIMIR 1 Visible], '' AS [OBSERVACION 2], 
                         '' AS [IMPRIMIR 2 Visible], '' AS [OBSERVACION 3], '' AS [IMPRIMIR 3 No Visible], ' ' AS [ ], CONVERT(VARCHAR(10), GETDATE(), 110) AS [Día Actual], RTRIM(h1.DATOBJETI) AS [Nota Evolución], RTRIM(u1.UFUDESCRI) 
                         AS [Unidad Funcional], RTRIM(i1.GENCAREGROUP) AS [Codigo EAPB], RTRIM(g1.Name) AS [Entidad EAPB], RTRIM(o1.NUMINGRES) AS [No Ingreso], 'PAT' AS [Tipo Orden]
FROM            dbo.HCORDPATO AS o1 LEFT OUTER JOIN
                         dbo.INPACIENT AS p1 WITH (NOLOCK) ON o1.IPCODPACI = p1.IPCODPACI LEFT OUTER JOIN
                         dbo.ADINGRESO AS i1 WITH (NOLOCK) ON o1.NUMINGRES = i1.NUMINGRES LEFT OUTER JOIN
                         dbo.INPROFSAL AS m1 WITH (NOLOCK) ON o1.CODPROSAL = m1.CODPROSAL LEFT OUTER JOIN
                         dbo.INESPECIA AS e1 WITH (NOLOCK) ON m1.CODESPEC1 = e1.CODESPECI LEFT OUTER JOIN
                         Contract.CareGroup AS g1 WITH (NOLOCK) ON i1.GENCAREGROUP = g1.Id LEFT OUTER JOIN
                         dbo.INCUPSIPS AS s1 WITH (NOLOCK) ON o1.CODSERIPS = s1.CODSERIPS LEFT OUTER JOIN
                         dbo.INUNIFUNC AS u1 WITH (NOLOCK) ON o1.UFUCODIGO = u1.UFUCODIGO LEFT OUTER JOIN
                         dbo.HCHISPACA AS h1 WITH (NOLOCK) ON o1.NUMINGRES = h1.NUMINGRES AND o1.NUMEFOLIO = h1.NUMEFOLIO
WHERE        FECORDMED >= '01-11-2023' AND i1.GENCONENTITY IN ('40', '190') AND o1.MANEXTPRO = '1'
UNION
/* 5 Ordenes Procedimientos NO QX */ SELECT CONVERT(VARCHAR(10), GETDATE(), 103) AS [Fecha_Solicitud], '900559103' AS [Nit Ips Remitente], '14737' AS [Código_Sucursal_Remitente], 
                         CASE p1.IPTIPODOC WHEN '1' THEN 'CC' WHEN '2' THEN 'CE' WHEN '3' THEN 'TI' WHEN '4' THEN 'RC' WHEN '5' THEN 'PA' WHEN '6' THEN 'AS' WHEN '7' THEN 'MS' WHEN '8' THEN 'NUIP' WHEN '12' THEN 'PE' END AS [Tipo_Identificación_del_Afiliado],
                          RTRIM(o1.IPCODPACI) AS [Número_de_Identificación_del_Afiliado], RTRIM(p1.IPNOMCOMP) AS [Nombre_Paciente], CASE WHEN p1.IPTELEFON = ' ' THEN RTRIM(p1.IPTELMOVI) WHEN p1.IPTELEFON IS NOT NULL 
                         THEN RTRIM(p1.IPTELEFON) END AS [Telefono_Celular_1], CONVERT(VARCHAR(10), o1.FECORDMED, 103) AS [Fecha_Atencion], RTRIM(i1.CODDIAING) AS [CIE10], RTRIM(m1.NOMMEDICO) AS [Nombre_Medico], 
                         RTRIM(e1.DESESPECI) AS [Especialidad_Remitente], '900559103' AS [Nit Ips Practica], '14737' AS [Código_Sucursal_Practicante], RTRIM(o1.CODSERIPS) AS [Código_CUPS_Prestacion], RTRIM(s1.DESSERIPS) 
                         AS [Descripcion_Prestacion], 
                         CASE WHEN s1.DESSERIPS LIKE '%CONTRAS%' THEN 'T' ELSE '' END + '-' + CASE WHEN s1.DESSERIPS LIKE '%COMPARA%' THEN 'C' ELSE '' END + '-' + CASE WHEN s1.DESSERIPS LIKE '%BILATER%' THEN 'B' ELSE '' END AS
                          [Atributo], RTRIM(o1.CANSERIPS) AS [Cantidad], RTRIM(o1.OBSSERIPS) AS [Justificacion_Clinica], '' AS [AUTORIZACION ANULAR], '' AS [OBSERVACION 1], '' AS [IMPRIMIR 1 Visible], '' AS [OBSERVACION 2], 
                         '' AS [IMPRIMIR 2 Visible], '' AS [OBSERVACION 3], '' AS [IMPRIMIR 3 No Visible], ' ' AS [ ], CONVERT(VARCHAR(10), GETDATE(), 110) AS [Día Actual], RTRIM(h1.DATOBJETI) AS [Nota Evolución], RTRIM(u1.UFUDESCRI) 
                         AS [Unidad Funcional], RTRIM(i1.GENCAREGROUP) AS [Codigo EAPB], RTRIM(g1.Name) AS [Entidad EAPB], RTRIM(o1.NUMINGRES) AS [No Ingreso], 'PRN' AS [Tipo Orden]
FROM            dbo.HCORDPRON AS o1 LEFT OUTER JOIN
                         dbo.INPACIENT AS p1 WITH (NOLOCK) ON o1.IPCODPACI = p1.IPCODPACI LEFT OUTER JOIN
                         dbo.ADINGRESO AS i1 WITH (NOLOCK) ON o1.NUMINGRES = i1.NUMINGRES LEFT OUTER JOIN
                         dbo.INPROFSAL AS m1 WITH (NOLOCK) ON o1.CODPROSAL = m1.CODPROSAL LEFT OUTER JOIN
                         dbo.INESPECIA AS e1 WITH (NOLOCK) ON m1.CODESPEC1 = e1.CODESPECI LEFT OUTER JOIN
                         Contract.CareGroup AS g1 WITH (NOLOCK) ON i1.GENCAREGROUP = g1.Id LEFT OUTER JOIN
                         dbo.INCUPSIPS AS s1 WITH (NOLOCK) ON o1.CODSERIPS = s1.CODSERIPS LEFT OUTER JOIN
                         dbo.INUNIFUNC AS u1 WITH (NOLOCK) ON o1.UFUCODIGO = u1.UFUCODIGO LEFT OUTER JOIN
                         dbo.HCHISPACA AS h1 WITH (NOLOCK) ON o1.NUMINGRES = h1.NUMINGRES AND o1.NUMEFOLIO = h1.NUMEFOLIO
WHERE        FECORDMED >= '01-11-2023' AND i1.GENCONENTITY IN ('40', '190') AND o1.MANEXTPRO = '1'
UNION
/* 6 Ordenes Procedimientos QX */ SELECT CONVERT(VARCHAR(10), GETDATE(), 103) AS [Fecha_Solicitud], '900559103' AS [Nit Ips Remitente], '14737' AS [Código_Sucursal_Remitente], 
                         CASE p1.IPTIPODOC WHEN '1' THEN 'CC' WHEN '2' THEN 'CE' WHEN '3' THEN 'TI' WHEN '4' THEN 'RC' WHEN '5' THEN 'PA' WHEN '6' THEN 'AS' WHEN '7' THEN 'MS' WHEN '8' THEN 'NUIP' WHEN '12' THEN 'PE' END AS [Tipo_Identificación_del_Afiliado],
                          RTRIM(o1.IPCODPACI) AS [Número_de_Identificación_del_Afiliado], RTRIM(p1.IPNOMCOMP) AS [Nombre_Paciente], CASE WHEN p1.IPTELEFON = ' ' THEN RTRIM(p1.IPTELMOVI) WHEN p1.IPTELEFON IS NOT NULL 
                         THEN RTRIM(p1.IPTELEFON) END AS [Telefono_Celular_1], CONVERT(VARCHAR(10), o1.FECORDMED, 103) AS [Fecha_Atencion], RTRIM(i1.CODDIAING) AS [CIE10], RTRIM(m1.NOMMEDICO) AS [Nombre_Medico], 
                         RTRIM(e1.DESESPECI) AS [Especialidad_Remitente], '900559103' AS [Nit Ips Practica], '14737' AS [Código_Sucursal_Practicante], RTRIM(o1.CODSERIPS) AS [Código_CUPS_Prestacion], RTRIM(s1.DESSERIPS) 
                         AS [Descripcion_Prestacion], 
                         CASE WHEN s1.DESSERIPS LIKE '%CONTRAS%' THEN 'T' ELSE '' END + '-' + CASE WHEN s1.DESSERIPS LIKE '%COMPARA%' THEN 'C' ELSE '' END + '-' + CASE WHEN s1.DESSERIPS LIKE '%BILATER%' THEN 'B' ELSE '' END AS
                          [Atributo], RTRIM(o1.CANSERIPS) AS [Cantidad], RTRIM(o1.OBSSERIPS) AS [Justificacion_Clinica], '' AS [AUTORIZACION ANULAR], '' AS [OBSERVACION 1], '' AS [IMPRIMIR 1 Visible], '' AS [OBSERVACION 2], 
                         '' AS [IMPRIMIR 2 Visible], '' AS [OBSERVACION 3], '' AS [IMPRIMIR 3 No Visible], ' ' AS [ ], CONVERT(VARCHAR(10), GETDATE(), 110) AS [Día Actual], RTRIM(h1.DATOBJETI) AS [Nota Evolución], RTRIM(u1.UFUDESCRI) 
                         AS [Unidad Funcional], RTRIM(i1.GENCAREGROUP) AS [Codigo EAPB], RTRIM(g1.Name) AS [Entidad EAPB], RTRIM(o1.NUMINGRES) AS [No Ingreso], 'PRQ' AS [Tipo Orden]
FROM            dbo.HCORDPROQ AS o1 LEFT OUTER JOIN
                         dbo.INPACIENT AS p1 WITH (NOLOCK) ON o1.IPCODPACI = p1.IPCODPACI LEFT OUTER JOIN
                         dbo.ADINGRESO AS i1 WITH (NOLOCK) ON o1.NUMINGRES = i1.NUMINGRES LEFT OUTER JOIN
                         dbo.INPROFSAL AS m1 WITH (NOLOCK) ON o1.CODPROSAL = m1.CODPROSAL LEFT OUTER JOIN
                         dbo.INESPECIA AS e1 WITH (NOLOCK) ON m1.CODESPEC1 = e1.CODESPECI LEFT OUTER JOIN
                         Contract.CareGroup AS g1 WITH (NOLOCK) ON i1.GENCAREGROUP = g1.Id LEFT OUTER JOIN
                         dbo.INCUPSIPS AS s1 WITH (NOLOCK) ON o1.CODSERIPS = s1.CODSERIPS LEFT OUTER JOIN
                         dbo.INUNIFUNC AS u1 WITH (NOLOCK) ON o1.UFUCODIGO = u1.UFUCODIGO LEFT OUTER JOIN
                         dbo.HCHISPACA AS h1 WITH (NOLOCK) ON o1.NUMINGRES = h1.NUMINGRES AND o1.NUMEFOLIO = h1.NUMEFOLIO
WHERE        FECORDMED >= '01-11-2023' AND i1.GENCONENTITY IN ('40', '190') AND o1.MANEXTPRO = '1'
UNION
/* 7 Ordenes Control Consulta EXT */ SELECT CONVERT(VARCHAR(10), GETDATE(), 103) AS [Fecha_Solicitud], '900559103' AS [Nit Ips Remitente], '14737' AS [Código_Sucursal_Remitente], 
                         CASE p1.IPTIPODOC WHEN '1' THEN 'CC' WHEN '2' THEN 'CE' WHEN '3' THEN 'TI' WHEN '4' THEN 'RC' WHEN '5' THEN 'PA' WHEN '6' THEN 'AS' WHEN '7' THEN 'MS' WHEN '8' THEN 'NUIP' WHEN '12' THEN 'PE' END AS [Tipo_Identificación_del_Afiliado],
                          RTRIM(o1.IPCODPACI) AS [Número_de_Identificación_del_Afiliado], RTRIM(p1.IPNOMCOMP) AS [Nombre_Paciente], CASE WHEN p1.IPTELEFON = ' ' THEN RTRIM(p1.IPTELMOVI) WHEN p1.IPTELEFON IS NOT NULL 
                         THEN RTRIM(p1.IPTELEFON) END AS [Telefono_Celular_1], CONVERT(VARCHAR(10), h1.FECHISPAC, 103) AS [Fecha_Atencion], RTRIM(i1.CODDIAING) AS [CIE10], RTRIM(m1.NOMMEDICO) AS [Nombre_Medico], 
                         RTRIM(e1.DESESPECI) AS [Especialidad_Remitente], '900559103' AS [Nit Ips Practica], '14737' AS [Código_Sucursal_Practicante], RTRIM(o1.CODSERIPS) AS [Código_CUPS_Prestacion], RTRIM(s1.DESSERIPS) 
                         AS [Descripcion_Prestacion], 
                         CASE WHEN s1.DESSERIPS LIKE '%CONTRAS%' THEN 'T' ELSE '' END + '-' + CASE WHEN s1.DESSERIPS LIKE '%COMPARA%' THEN 'C' ELSE '' END + '-' + CASE WHEN s1.DESSERIPS LIKE '%BILATER%' THEN 'B' ELSE '' END AS
                          [Atributo], '1' AS [Cantidad], 'CONSULTA DE CONTROL' AS [Justificacion_Clinica], '' AS [AUTORIZACION ANULAR], '' AS [OBSERVACION 1], '' AS [IMPRIMIR 1 Visible], '' AS [OBSERVACION 2], '' AS [IMPRIMIR 2 Visible], 
                         '' AS [OBSERVACION 3], '' AS [IMPRIMIR 3 No Visible], ' ' AS [ ], CONVERT(VARCHAR(10), GETDATE(), 110) AS [Día Actual], RTRIM(h1.DATOBJETI) AS [Nota Evolución], RTRIM(u1.UFUDESCRI) AS [Unidad Funcional], 
                         RTRIM(i1.GENCAREGROUP) AS [Codigo EAPB], RTRIM(g1.Name) AS [Entidad EAPB], RTRIM(o1.NUMINGRES) AS [No Ingreso], 'PRQ' AS [Tipo Orden]
FROM            dbo.HCDESCOEX AS o1 LEFT OUTER JOIN
                         dbo.INPACIENT AS p1 WITH (NOLOCK) ON o1.IPCODPACI = p1.IPCODPACI LEFT OUTER JOIN
                         dbo.ADINGRESO AS i1 WITH (NOLOCK) ON o1.NUMINGRES = i1.NUMINGRES LEFT OUTER JOIN
                         dbo.INPROFSAL AS m1 WITH (NOLOCK) ON i1.CODPROING = m1.CODPROSAL LEFT OUTER JOIN
                         dbo.INESPECIA AS e1 WITH (NOLOCK) ON m1.CODESPEC1 = e1.CODESPECI LEFT OUTER JOIN
                         Contract.CareGroup AS g1 WITH (NOLOCK) ON i1.GENCAREGROUP = g1.Id LEFT OUTER JOIN
                         dbo.INCUPSIPS AS s1 WITH (NOLOCK) ON o1.CODSERIPS = s1.CODSERIPS LEFT OUTER JOIN
                         dbo.INUNIFUNC AS u1 WITH (NOLOCK) ON o1.UFUCODIGO = u1.UFUCODIGO LEFT OUTER JOIN
                         dbo.HCHISPACA AS h1 WITH (NOLOCK) ON o1.NUMINGRES = h1.NUMINGRES AND o1.NUMEFOLIO = h1.NUMEFOLIO
WHERE        FECHISPAC >= '01-11-2023' AND i1.GENCONENTITY IN ('40', '190')