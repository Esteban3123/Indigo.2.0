create view [dbo].[I_ADM_PacientesConIngresosAbiertos]
as
SELECT        A.IPCODPACI AS CEDULA, 
                         CASE A.IPTIPODOC WHEN 1 THEN 'CC' WHEN 2 THEN 'CE' WHEN 3 THEN 'TI' WHEN 4 THEN 'RC' WHEN 5 THEN 'PA' WHEN 6 THEN 'AS' WHEN 7 THEN 'MS' WHEN 8 THEN 'NU' END AS TIPODOCUMENTO, 
                         A.CODIGONIT AS Tercero, A.IPNOMCOMP AS PACIENTE, 
                         CASE IPTIPOPAC WHEN 1 THEN 'CONTRIBUTIVO' WHEN 2 THEN 'SUBSIDIADO' WHEN 3 THEN 'VINCULADO' WHEN 4 THEN 'PARTICULAR' WHEN 5 THEN 'OTRO' WHEN 6 THEN 'DESPLAZADO REG. CONTRIBUTIVO' WHEN 7 THEN
                          'DESPLAZADO REG. SUBSIDIADO' WHEN 8 THEN 'DESPLAZADO NO ASEGURADO' END AS TIPOPACIENTE, 
                         CASE A.IPTIPOAFI WHEN 0 THEN 'NO APLICA' WHEN 1 THEN 'COTIZANTE' WHEN 2 THEN 'BENEFICIARIO' WHEN 3 THEN 'ADICIONAL' WHEN 4 THEN 'JUB/RETIRADO' WHEN 5 THEN 'PENSIONADO' END AS TIPOAFILIACION, 
                         CASE A.CAPACIPAG WHEN 0 THEN 'NO APLICA' WHEN 1 THEN 'SI' WHEN 2 THEN 'NO' WHEN 3 THEN 'DESPLAZADO' END AS CAPACIDADPAGO, B.NOMENTIDA AS ENTIDAD, A.IPDIRECCI AS DIRECCION, 
                         A.IPTELEFON AS TELEFONO, A.IPTELMOVI AS MOVIL, 
						 A.IPFECNACI AS FechaNacimiento, 
                         CASE A.IPSEXOPAC WHEN 1 THEN 'MASCULINO' WHEN 2 THEN 'FEMENINO' END AS SEXO, 
                         CASE A.IPESTADOC WHEN 1 THEN 'SOLTERO' WHEN 2 THEN 'CASADO' WHEN 3 THEN 'VIUDO' WHEN 4 THEN 'UNION LIBRE' WHEN 5 THEN 'SEPARADO/DIV' END AS ESTADOCIVIL, 
                         CASE A.TIPCOBSAL WHEN 1 THEN 'CONTRIBUTIVO' WHEN 2 THEN 'SUBTOTAL' WHEN 3 THEN 'SUBPARCIAL' WHEN 4 THEN 'CON SISBEN' WHEN 5 THEN 'SIN SISBEN' WHEN 6 THEN 'DESPLAZADOS' WHEN 7 THEN 'PLAN DE SALUD ADICIONAL'
                          WHEN 8 THEN 'OTROS' END AS COBERTURA, A.CORELEPAC AS CORREO, CASE ESTADOPAC WHEN 1 THEN 'ACTIVO' WHEN 2 THEN 'INACTIVO' END AS ESTADOPACIENTE, A.OBSERVACI AS OBSERVACION, 
                         H1.NOMUSUARI AS EMP_CREA, A.FECREGCRE AS FECHA_CREA, H2.NOMUSUARI AS EMP_MODI, A.FECREGMOD AS FECHA_MODIFICA, I.IFECHAING AS FechaIngreso, 
                         CASE I.TIPOINGRE WHEN 1 THEN 'Ambulatorio' WHEN 2 THEN 'Hospitalario' END AS TipoIngreso, U.UFUDESCRI AS Unidad, 
                         CASE I.IINGREPOR WHEN 1 THEN 'Urgencias' WHEN 2 THEN 'Consulta Externa' WHEN 3 THEN 'Nacido Hospital' WHEN 4 THEN 'Remitido' WHEN 5 THEN 'HospitalizacionURG' END AS IngresoPOR, 
                         CASE I.ICAUSAING WHEN 1 THEN 'Heridos en Combate' WHEN 2 THEN 'Enfermedad Profesional' WHEN 3 THEN 'Emfermedad General Adulto' WHEN 4 THEN 'Enfermedad General Pediatrica' WHEN 1 THEN 'Odontologia' WHEN
                          6 THEN 'Accidente de Transito' WHEN 7 THEN 'Catastrofe' END AS Causa, I.NUMINGRES, CASE I.CODCENATE WHEN '01' THEN 'Bogota' WHEN '03' THEN 'Cali' END AS Sede
FROM            dbo.INPACIENT AS A INNER JOIN
                         dbo.ADINGRESO AS I ON A.IPCODPACI = I.IPCODPACI AND I.IESTADOIN = '' INNER JOIN
                         dbo.INENTIDAD AS B ON I.CODENTIDA = B.CODENTIDA INNER JOIN
                         dbo.SEGusuaru AS H1 ON H1.CODUSUARI = A.CODUSUCRE LEFT OUTER JOIN
                         dbo.SEGusuaru AS H2 ON H2.CODUSUARI = A.CODUSUMOD INNER JOIN
                         dbo.INUNIFUNC AS U ON I.UFUACTPAC = U.UFUCODIGO
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todos los pacientes que actualmente tienen ingresos abiertos (sin fecha de egreso o cierre) en la institución, combinando datos demográficos del paciente (cédula, nombre, fecha de nacimiento, sexo, estado civil, contacto) con información del ingreso activo (fecha de ingreso, tipo de ingreso, motivo, causa, número de ingreso, sede y unidad funcional donde se encuentra) y la entidad aseguradora o pagadora asociada. Integra las tablas de pacientes (INPACIENT), ingresos (ADINGRESO filtrando solo los abiertos), entidades (INENTIDAD), unidades funcionales (INUNIFUNC) y usuarios del sistema (SEGusuaru) para mostrar quién creó y modificó el registro. Sirve para monitoreo de censo hospitalario en tiempo real, control de camas ocupadas, seguimiento de pacientes activos por sede y generación de reportes operativos de admisiones vigentes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'I_ADM_PacientesConIngresosAbiertos';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'I_ADM_PacientesConIngresosAbiertos';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Exponer el listado de pacientes con ingresos actualmente abiertos, enriquecido con datos demográficos, de afiliación, entidad responsable, unidad funcional, sede y trazabilidad de creación/modificación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'I_ADM_PacientesConIngresosAbiertos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de ingresos en ADINGRESO con IESTADOIN igual a cadena vacía ('''') para considerarse abiertos.; Coincidencia de IPCODPACI entre paciente e ingreso, y existencia de la entidad (CODENTIDA), unidad funcional (UFUACTPAC) y usuario creador (CODUSUCRE) referenciados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'I_ADM_PacientesConIngresosAbiertos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen pacientes cuyo ingreso esté abierto, identificado por IESTADOIN = '''' (cadena vacía).; Cada fila representa la combinación paciente-ingreso abierto; un paciente con varios ingresos abiertos aparece varias veces.; Se exige que el ingreso tenga entidad (CODENTIDA), unidad funcional (UFUACTPAC) y usuario creador (CODUSUCRE) válidos por ser INNER JOIN; el usuario modificador es opcional (LEFT JOIN).; Los códigos numéricos de catálogos internos (tipo documento, tipo paciente, afiliación, capacidad de pago, sexo, estado civil, cobertura, estado, tipo ingreso, ingreso por, causa, sede) se traducen a etiquetas legibles; valores fuera de los rangos definidos se exponen como NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'I_ADM_PacientesConIngresosAbiertos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso/Admisión; Tipo de documento; Tipo de paciente (régimen); Tipo de afiliación; Capacidad de pago; Entidad/Aseguradora; Cobertura en salud; Estado civil; Sexo; Tipo de ingreso (Ambulatorio/Hospitalario); Unidad funcional; Causa de ingreso; Sede (Bogotá/Cali); Usuario que crea/modifica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'I_ADM_PacientesConIngresosAbiertos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.ADINGRESO: Devuelve únicamente registros de ADINGRESO cuyo IESTADOIN = '''' (ingreso abierto/no cerrado), unidos a INPACIENT, INENTIDAD, INUNIFUNC y SEGusuaru.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'I_ADM_PacientesConIngresosAbiertos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INPACIENT; dbo.ADINGRESO; dbo.INENTIDAD; dbo.SEGusuaru; dbo.INUNIFUNC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'I_ADM_PacientesConIngresosAbiertos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'I_ADM_PacientesConIngresosAbiertos';
GO
