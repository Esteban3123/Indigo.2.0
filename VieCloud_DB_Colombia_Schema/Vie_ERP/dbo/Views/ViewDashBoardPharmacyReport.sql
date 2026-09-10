

CREATE VIEW [dbo].[ViewDashBoardPharmacyReport]
AS
SELECT
	RTRIM(A.NUMINGRES) AS Ingreso
   ,RTRIM(c.Name) AS Entidad
   ,c.Code AS CodigoEntidad
   ,B.CODCONTRA AS CodigoContrato
   ,RTRIM(A.CODPRODUC) AS CodigoProducto
   ,B.CODPANATE AS CodigoPlan
   ,RTRIM(CG.Name)
	AS ContratoPlan
   ,RTRIM(A.CODPRODUC) + ' - ' + RTRIM(D.DESPRODUC) +  iif(hc.DESADMINI is null,'', '( ' + hc.DESADMINI  + ' )' )  AS Producto 
   ,A.TIPOREGIS AS Tipo
   ,H.FECHAORDE AS FechaOrden
   ,RTRIM(IP.IPNOMCOMP) AS NombrePaciente
   ,RTRIM(I.UFUDESCRI)
	AS UnidadFuncional
   ,A.CANPEDPRO AS CantidadSolicitada
   ,CAST('' AS INT) AS CantidadEntregada 
   ,A.CANPENPRO AS CantidadPendiente
   ,CAST(0 AS BIT) AS Unico
   ,A.NOPOSPROD AS NOPOS
   ,A.CODUNIMED AS UnidadMedida
   ,RTRIM(A.CODPROSAL) + ' - ' + RTRIM(E.NOMMEDICO) AS NombreMedico
   ,E.CODIGONIT AS NitMedico
   ,CAST('' AS INT) AS FilaSeleccionada
   ,RTRIM(E.TARJETAPR) AS NumeroTarjeta
   ,A.IDETIPHIS
   ,A.NUMEFOLIO
   ,'0' AS Opcion
   ,'0' AS OpcionAnulado
   ,'' AS Procedimiento
   ,CAST('0' AS BIT) AS MarcarOpcion
   ,RTRIM(A.IPCODPACI) AS CodigoPaciente
   ,A.CODCONCEC AS ConsecutivoFarmacia
   ,A.ID as IdDetalleFarmacia
   ,A.PROESTADO AS Estado
   ,F.CODESPECI + ' - ' + F.DESESPECI AS Especialidad
   ,ISNULL(cama.NUMCAMHOS, '') AS CodigoCama
   ,hc.DESADMINI as NotaAdministracion,
   RTRIM(CASE 
   WHEN hc.FORMAPRESCRIBE IS NOT NULL THEN hc.DESADMINI 
   ELSE CASE 
		WHEN hc.DOSISPRFN IS NULL THEN CASE
			WHEN hcf.ADMMEZLIQ is not null then hcf.ADMMEZLIQ 
			ELSE CASE 
				WHEN  hc.DESADMINI IS NULL THEN 'No aplica.' ELSE hc.DESADMINI
				END 
			END 
	    WHEN hc.DURACIDOS='Dosis Unica' THEN RTRIM(CAST(hc.DOSISPRFN AS CHAR)) + ' ' + RTRIM(CAST(ABRUNIMED AS CHAR)) + ' Dosis Única Via: ' + RTRIM(DESVIAADM) 
			ELSE RTRIM(CAST(hc.DOSISPRFN AS CHAR)) + ' ' + RTRIM(CAST(ABRUNIMED AS CHAR)) + ' Cada ' + RTRIM(CAST(hc.FRECUENCI AS CHAR)) + CASE hc.UNIFRECUE WHEN '1' THEN ' min(s) ' 
			WHEN '2' THEN ' Hora(s) ' WHEN '3' THEN ' Dia(s) ' END + 'Vía: ' + RTRIM(DESVIAADM)
			END
		END
	) AS ADMINISTRACION

FROM dbo.HCFARMEPD AS A
INNER JOIN dbo.ADINGRESO AS B
	ON A.NUMINGRES = B.NUMINGRES
INNER JOIN dbo.HCFARMEPC AS H
	ON A.CODCONCEC = H.CODCONCEC
INNER JOIN dbo.INPACIENT AS IP
	ON A.IPCODPACI = IP.IPCODPACI
INNER JOIN dbo.INUNIFUNC AS I
	ON A.UFUCODIGO = I.UFUCODIGO
INNER JOIN Contract.CareGroup AS CG
	ON CG.Id = B.GENCAREGROUP
INNER JOIN Contract.HealthAdministrator AS c
	ON B.GENCONENTITY = c.Id
INNER JOIN dbo.IHLISTPRO AS D
	ON A.CODPRODUC = D.CODPRODUC
INNER JOIN dbo.INPROFSAL AS E
	ON A.CODPROSAL = E.CODPROSAL
INNER JOIN dbo.INESPECIA AS F
	ON E.CODESPEC1 = F.CODESPECI
LEFT OUTER JOIN dbo.HCPRESCRA AS hc
	ON  A.SourceTable='HCPRESCRA' AND A.IdSourceTable= hc.ID
LEFT OUTER JOIN dbo.HCINFLIQA AS hcf
	ON  A.SourceTable='HCINFLIQA' AND A.IdSourceTable= hcf.CONSECUTI
LEFT JOIN dbo.CHCAMASHO cama ON ISNULL(CASE WHEN B.CODCAMACT = 0 THEN '' ELSE CAST(B.CODCAMACT AS VARCHAR(15)) END, '') = cama.CODICAMAS
LEFT OUTER JOIN dbo.INUNIMEDI IND On hc.CODUNIMFN=IND.CODUNIMED 
LEFT OUTER JOIN dbo.HCVIAADMI VIA On D.CODVIAADM=VIA.CODVIAADM
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Panel de control (dashboard) de despacho farmacéutico que consolida, por cada ítem de medicamento solicitado en una orden de farmacia, los datos del paciente (nombre, cédula, código), el ingreso hospitalario o de urgencias al que pertenece, la entidad pagadora (EPS/aseguradora) y el plan o grupo de contrato, el producto farmacéutico con su descripción y unidad de medida, las cantidades solicitadas y pendientes de entrega, el médico prescriptor con su especialidad y tarjeta profesional, la unidad funcional (sala o servicio) y la cama asignada, la fecha de la orden, el estado del ítem, y las instrucciones de administración del medicamento (dosis, frecuencia, vía, mezclas o dosis única). Integra los detalles de órdenes de farmacia (HCFARMEPD y HCFARMEPC), el ingreso del paciente (ADINGRESO), el catálogo de pacientes (INPACIENT), productos farmacéuticos (IHLISTPRO), profesionales de la salud (INPROFSAL), especialidades (INESPECIA), unidades funcionales (INUNIFUNC), camas (CHCAMASHO), prescripciones e infusiones/mezclas líquidas (HCPRESCRA, HCINFLIQA), y los contratos con entidades pagadoras (CareGroup, HealthAdministrator). Esta vista es la fuente principal de reportería y visualización en tiempo real del estado de dispensación de medicamentos para el área de farmacia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewDashBoardPharmacyReport';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewDashBoardPharmacyReport';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola vista las órdenes farmacéuticas pendientes/despachadas con datos del paciente, ingreso, contrato, plan, médico, unidad funcional, cama y la instrucción de administración del medicamento, para alimentar reportes/tableros de farmacia.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacyReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada detalle de farmacia debe tener un ingreso (ADINGRESO), encabezado de orden (HCFARMEPC), paciente, unidad funcional, grupo de cuidado del contrato, administradora de salud, producto, profesional prescriptor y especialidad asociada (todos vía INNER JOIN).; El detalle de farmacia debe referenciar como SourceTable ''HCPRESCRA'' o ''HCINFLIQA'' para poder enlazar la prescripción y derivar la administración.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacyReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada fila representa un detalle de pedido de farmacia (HCFARMEPD) y siempre lleva asociado un ingreso, paciente, profesional, especialidad, producto, contrato y plan.; La columna Producto siempre incluye código y descripción del producto, y opcionalmente la nota de administración entre paréntesis.; ADMINISTRACION nunca queda nula: si no hay datos suficientes se devuelve ''No aplica.''.; Se devuelven constantes auxiliares para uso de UI: CantidadEntregada, FilaSeleccionada, Unico=0, Opcion=''0'', OpcionAnulado=''0'', Procedimiento='''', MarcarOpcion=0.; Solo se traen órdenes cuya entidad pagadora y grupo de cuidado existan en los catálogos de Contract.; La especialidad mostrada corresponde a la especialidad principal (CODESPEC1) del profesional prescriptor.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacyReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de farmacia; Prescripción médica; Paciente; Ingreso/Admisión; Contrato y plan de salud; Administradora de salud (EPS); Unidad funcional; Cama hospitalaria; Profesional de salud / médico prescriptor; Especialidad médica; Producto farmacéutico / medicamento; Dosis, frecuencia y vía de administración; Mezcla de líquidos (infusión); NOPOS; Cantidad solicitada / pendiente / entregada', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacyReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ViewDashBoardPharmacyReport: Devuelve una fila por cada registro de HCFARMEPD enriquecido con datos de ingreso, paciente, profesional, producto, contrato/plan, cama y prescripción.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacyReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si hc.DESADMINI IS NULL en la prescripción HCPRESCRA → Se concatena al nombre del producto el sufijo ''( DESADMINI )'' else No se agrega el sufijo de administración al producto; si hc.FORMAPRESCRIBE IS NOT NULL → La columna ADMINISTRACION toma el texto libre hc.DESADMINI else Se evalúa la siguiente regla según DOSISPRFN; si hc.DOSISPRFN IS NULL (no hay dosis estructurada) → Si existe hcf.ADMMEZLIQ (mezcla líquida) se usa esa instrucción; si no, se usa hc.DESADMINI o el literal ''No aplica.'' cuando también es nulo else Se construye la administración a partir de dosis, unidad de medida, frecuencia y vía; si hc.DURACIDOS = ''Dosis Unica'' → ADMINISTRACION = dosis + unidad + ''Dosis Única Via: '' + descripción de vía else ADMINISTRACION = dosis + unidad + ''Cada '' + frecuencia + unidad de frecuencia + ''Vía: '' + descripción de vía; si hc.UNIFRECUE = ''1'' / ''2'' / ''3'' → La unidad de frecuencia se traduce a ''min(s)'', ''Hora(s)'' o ''Dia(s)'' respectivamente else No se agrega texto de unidad de frecuencia; si B.CODCAMACT = 0 o NULL → No se intenta enlazar cama (se usa cadena vacía); CodigoCama queda en '''' else Se enlaza con CHCAMASHO por el código de cama actual del ingreso; si A.SourceTable = ''HCPRESCRA'' → Se enlaza la prescripción desde HCPRESCRA por IdSourceTable = hc.ID else hc queda nulo; si A.SourceTable = ''HCINFLIQA'' → Se enlaza la información de mezcla líquida desde HCINFLIQA por IdSourceTable = hcf.CONSECUTI else hcf queda nulo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacyReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFARMEPD; dbo.ADINGRESO; dbo.HCFARMEPC; dbo.INPACIENT; dbo.INUNIFUNC; Contract.CareGroup; Contract.HealthAdministrator; dbo.IHLISTPRO; dbo.INPROFSAL; dbo.INESPECIA; dbo.HCPRESCRA; dbo.HCINFLIQA; dbo.CHCAMASHO; dbo.INUNIMEDI; dbo.HCVIAADMI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacyReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacyReport';
GO
