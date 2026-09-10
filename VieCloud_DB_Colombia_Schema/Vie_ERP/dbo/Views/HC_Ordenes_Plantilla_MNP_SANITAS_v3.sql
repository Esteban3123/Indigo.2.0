
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
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting orientada a la generación de la plantilla de órdenes médicas externas para la aseguradora SANITAS (entidades contractuales 40 y 190), consolidando en un único conjunto cuatro tipos de órdenes —imágenes (IMG), interconsultas (INT), laboratorio (LAB) y patología (PAT)— emitidas desde noviembre de 2023 con manejo externo activo. Aplana datos de paciente, profesional, especialidad, diagnóstico CIE10, código CUPS, EAPB y unidad funcional para alimentar un formato estandarizado de remisión con NIT e IPS fijos de la institución remitente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'HC_Ordenes_Plantilla_MNP_SANITAS_v3';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'HC_Ordenes_Plantilla_MNP_SANITAS_v3';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un único conjunto las órdenes médicas externas (imágenes, interconsultas, laboratorio, patología, procedimientos no quirúrgicos, quirúrgicos y consultas de control) generadas a pacientes de entidades 40 y 190 desde nov/2023, en formato plantilla para SANITAS.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'HC_Ordenes_Plantilla_MNP_SANITAS_v3';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las órdenes deben tener FECORDMED (o FECHISPAC para control) mayor o igual al 01-11-2023.; El ingreso asociado debe estar afiliado a una entidad cuyo GENCONENTITY sea ''40'' o ''190''.; Para las secciones 1-6, la orden debe estar marcada como manejo externo (MANEXTPRO = ''1'').; Existencia opcional de paciente, ingreso, profesional, especialidad, CareGroup, CUPS, unidad funcional e historia clínica (joins LEFT).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'HC_Ordenes_Plantilla_MNP_SANITAS_v3';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'NIT IPS remitente y practicante siempre fijados a ''900559103''.; Código de sucursal remitente y practicante siempre fijados a ''14737''.; Fecha de solicitud y ''Día Actual'' siempre corresponden a la fecha de ejecución (GETDATE).; Solo se exponen órdenes de pacientes cuyas entidades sean ''40'' o ''190'' (presumiblemente Sanitas).; Para órdenes ambulatorias 1-6 solo se incluyen las marcadas como manejo externo (MANEXTPRO=''1'').; El ámbito temporal mínimo está fijado en código a 01-11-2023.; Cantidad y justificación están fijadas para consultas de control (''1'' y ''CONSULTA DE CONTROL'').; Todos los textos de identificadores y descripciones se entregan con RTRIM (sin espacios finales).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'HC_Ordenes_Plantilla_MNP_SANITAS_v3';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Tipo de identificación del afiliado; Ingreso/Atención; Diagnóstico CIE-10; Profesional de salud; Especialidad médica; EAPB (Entidad Administradora de Planes de Beneficios); CareGroup / Grupo de atención; Código CUPS de prestación; Orden de imágenes diagnósticas; Orden de interconsulta; Orden de laboratorio; Orden de patología; Procedimiento no quirúrgico; Procedimiento quirúrgico; Consulta de control externa; Unidad funcional; Nota de evolución / historia clínica; Justificación clínica; IPS remitente / practicante; Manejo externo del procedimiento', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'HC_Ordenes_Plantilla_MNP_SANITAS_v3';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve filas tipificadas con ''IMG'', ''INT'', ''LAB'', ''PAT'', ''PRN'', ''PRQ'' según el tipo de orden, unificadas vía UNION; las consultas de control externo se etiquetan como ''PRQ'' con cantidad fija 1 y justificación ''CONSULTA DE CONTROL''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'HC_Ordenes_Plantilla_MNP_SANITAS_v3';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IPTIPODOC del paciente (1..8,12) → Mapea a códigos estándar CC, CE, TI, RC, PA, AS, MS, NUIP, PE para el tipo de identificación del afiliado. else NULL (no mapeado); si IPTELEFON = '' '' → Usa IPTELMOVI como teléfono de contacto. else Si IPTELEFON no es nulo, usa IPTELEFON.; si DESSERIPS contiene ''CONTRAS'' / ''COMPARA'' / ''BILATER'' → Marca atributo del estudio con T / C / B respectivamente concatenados con ''-''. else Deja vacío el carácter correspondiente.; si Sección 7 (consultas de control externo) → Usa FECHISPAC como fecha de atención y omite el filtro MANEXTPRO; profesional se toma del médico de ingreso (CODPROING). else Secciones 1-6 usan FECORDMED y exigen MANEXTPRO=''1''; profesional desde la orden (CODPROSAL).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'HC_Ordenes_Plantilla_MNP_SANITAS_v3';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDIMAG; dbo.HCORDINTE; dbo.HCORDLABO; dbo.HCORDPATO; dbo.HCORDPRON; dbo.HCORDPROQ; dbo.HCDESCOEX; dbo.INPACIENT; dbo.ADINGRESO; dbo.INPROFSAL; dbo.INESPECIA; Contract.CareGroup; dbo.INCUPSIPS; dbo.INUNIFUNC; dbo.HCHISPACA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'HC_Ordenes_Plantilla_MNP_SANITAS_v3';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'HC_Ordenes_Plantilla_MNP_SANITAS_v3';
GO
