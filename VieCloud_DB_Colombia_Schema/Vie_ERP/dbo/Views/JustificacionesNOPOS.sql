

CREATE VIEW [dbo].[JustificacionesNOPOS]
AS
SELECT     ROW_NUMBER() OVER (ORDER BY A.CODCONCEC) AS NUMEROFILA, A.CODCONCEC, A.CODPRODUC, A.IPCODPACI, rtrim(B.IPNOMCOMP) AS IPNOMCOMP, 
A.FECORDMED AS FECORDMED, RTRIM(C.UFUDESCRI) AS UFUDESCRI, D .CANPEDPRO AS CANPEDPRO, D .CANENTPRO AS CANENTPRO, D .CANPENPRO AS CANPENPRO, 
RTRIM(E.DESPRODUC) AS DESPRODUC, A.DOSISDIAS, A.DIASTRATA, A.INDTERAPR, A.RESJUSMED, A.EFEADVMED, A.RAZVENMED, A.RIEINMPAC, A.AGOPOSTER, 
A.MEDUTIPAI, A.MEDEXPERI, A.USOCORINV, A.CANPEDPRO AS CantidadNoPos, A.CODCENATE, A.NUMINGRES, F.CANSERAUT, A.IDETIPHIS, A.UFUCODIGO, A.CODVIAADM, 
A.CODFORMED, A.DOSISPROD, A.CODUNIMED, A.TIPFORMED, A.DURACIDOS, A.VALDURFIJ, A.UNIDURFIJ, A.FRECUENCI, A.UNIFRECUE, A.CODDIAGNO, A.NUMEFOLIO,
A.CODMINSALUD, A.CODPROSAL
FROM         dbo.HCJUNOPOM AS A INNER JOIN
                      --dbo.CHREGESTA G ON A.IPCODPACI = G.IPCODPACI AND A.NUMINGRES = G.NUMINGRES AND REGESTADO = '1' INNER JOIN
                      dbo.INPACIENT AS B ON A.IPCODPACI = B.IPCODPACI INNER JOIN
                      dbo.INUNIFUNC AS C ON A.UFUCODIGO = C.UFUCODIGO INNER JOIN
                      dbo.HCFARMEPD AS D ON A.IDETIPHIS = D .IDETIPHIS AND A.NUMEFOLIO = D .NUMEFOLIO AND A.IPCODPACI = D .IPCODPACI AND 
                      A.NUMINGRES = D .NUMINGRES AND A.CODCENATE = D .CODCENATE AND A.UFUCODIGO = D .UFUCODIGO AND A.CODPRODUC = D .CODPRODUC INNER JOIN
                      dbo.IHLISTPRO AS E ON A.CODPRODUC = E.CODPRODUC LEFT OUTER JOIN
                      dbo.ADAUTSERD F ON A.CODPRODUC = F.CODSERIPS
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consolida las justificaciones de medicamentos y tecnologías NO POS (no incluidos en el plan de beneficios) prescritos a pacientes. Integra la orden médica de no POS (HCJUNOPOM) con los datos del paciente (INPACIENT), la unidad funcional donde fue atendido (INUNIFUNC), el detalle de despacho farmacéutico (HCFARMEPD) con cantidades pedidas, entregadas y pendientes, el catálogo de productos farmacéuticos (IHLISTPRO) y las autorizaciones de servicio (ADAUTSERD). Incluye información clínica de la justificación como diagnóstico CIE-10, dosis, días de tratamiento, indicación terapéutica, efectos adversos, riesgos para el paciente y alternativas terapéuticas evaluadas. Sirve para el reporte y auditoría de recobros, tutelas y solicitudes de medicamentos NO POS ante aseguradoras o entes de control.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'JustificacionesNOPOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'JustificacionesNOPOS';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida las justificaciones de medicamentos NO POS prescritos a pacientes con sus datos clínicos, de pedido de farmacia, producto y autorización del servicio, para soporte de auditoría y entrega de medicamentos no incluidos en el plan de beneficios.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'JustificacionesNOPOS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el paciente referenciado en INPACIENT.; Debe existir la unidad funcional referenciada en INUNIFUNC.; Debe existir el producto referenciado en IHLISTPRO.; Debe existir un pedido de farmacia en HCFARMEPD que coincida exactamente con la justificación en tipo de historia, folio, paciente, ingreso, centro de atención, unidad funcional y producto.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'JustificacionesNOPOS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen justificaciones NO POS que tengan correspondencia con un pedido de farmacia (HCFARMEPD) coincidente en tipo de historia, folio, paciente, ingreso, centro de atención, unidad funcional y producto.; Solo se exponen justificaciones cuyo paciente exista en el maestro INPACIENT, cuya unidad funcional exista en INUNIFUNC y cuyo producto exista en el catálogo IHLISTPRO.; La autorización del servicio (ADAUTSERD) es opcional: si no existe autorización para el producto, la justificación se sigue listando con CANSERAUT en NULL.; Se asigna un número de fila secuencial ordenado por concepto (CODCONCEC).; Los textos descriptivos de paciente, unidad funcional y producto se entregan sin espacios sobrantes a la derecha (RTRIM).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'JustificacionesNOPOS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Justificación NO POS; Medicamento prescrito; Paciente; Unidad funcional; Pedido de farmacia; Producto farmacéutico; Autorización de servicios; Dosis y duración del tratamiento; Reacciones adversas; Riesgo inminente; Uso compasivo / experimental', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'JustificacionesNOPOS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCJUNOPOM: Devuelve una fila por cada justificación NO POS (HCJUNOPOM) que tenga pedido de farmacia coincidente, paciente, unidad funcional y producto válidos; la autorización del servicio se incluye vía LEFT JOIN, por lo que su ausencia no excluye la fila.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'JustificacionesNOPOS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCJUNOPOM; dbo.INPACIENT; dbo.INUNIFUNC; dbo.HCFARMEPD; dbo.IHLISTPRO; dbo.ADAUTSERD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'JustificacionesNOPOS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'JustificacionesNOPOS';
GO
