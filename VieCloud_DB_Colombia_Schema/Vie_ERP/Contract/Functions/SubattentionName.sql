

CREATE Function [Contract].[SubattentionName]
(
	@SubattentionCode tinyint
)
Returns varchar(50)
As
Begin 
	RETURN  CASE
		WHEN @SubattentionCode = 1 THEN 'NINGUNO'
		WHEN @SubattentionCode = 2 THEN 'ESTANCIA INDIVIDUAL'
		WHEN @SubattentionCode = 3 THEN 'HABITACIÓN COMPARTIDA'
		WHEN @SubattentionCode = 4 THEN 'UCI ADULTOS'
		WHEN @SubattentionCode = 5 THEN 'UCI NEONATAL'
		WHEN @SubattentionCode = 6 THEN 'UCI CUIDADOS MEDIANOS'
		WHEN @SubattentionCode = 7 THEN 'INCUBADORA'
		WHEN @SubattentionCode = 8 THEN 'CONSULTA MÉDICA GENERAL'
		WHEN @SubattentionCode = 9 THEN 'CONSULTA ESPECIALISTA'
		WHEN @SubattentionCode = 10 THEN 'INTERCONSULTA'
		WHEN @SubattentionCode = 11 THEN 'VISITAS HOSPITALARIAS'
		WHEN @SubattentionCode = 12 THEN 'HONORARIOS CIRUJANOS'
		WHEN @SubattentionCode = 13 THEN 'HONORARIOS ANESTESIA'
		WHEN @SubattentionCode = 14 THEN 'HONORARIOS AYUDANTÍA'
		WHEN @SubattentionCode = 15 THEN 'HONORARIOS INSTRUMENTACIÓN'
		WHEN @SubattentionCode = 16 THEN 'DERECHOS DE SALA'
		WHEN @SubattentionCode = 17 THEN 'DERECHO ANESTESIA'
		WHEN @SubattentionCode = 18 THEN 'DERECHO EQUIPO'
		WHEN @SubattentionCode = 19 THEN 'INSUMOS HOSPITALARIOS'
		WHEN @SubattentionCode = 20 THEN 'MATERIALES'
		WHEN @SubattentionCode = 21 THEN 'MEDICAMENTOS'
		WHEN @SubattentionCode = 22 THEN 'OXÍGENO'
		WHEN @SubattentionCode = 23 THEN 'LABORATORIO'
		WHEN @SubattentionCode = 24 THEN 'RADIOLOGÍA'
		WHEN @SubattentionCode = 25 THEN 'TOMOGRAFÍAS'
		WHEN @SubattentionCode = 26 THEN 'MEDICINA NUCLEAR'
		WHEN @SubattentionCode = 27 THEN 'RESONANCIA MAGNÉTICA'
		WHEN @SubattentionCode = 28 THEN 'EXÁMENES COMPLEMENTARIOS'
		WHEN @SubattentionCode = 29 THEN 'EXÁMENES VASCULARES'
		WHEN @SubattentionCode = 30 THEN 'HEMODINAMIA'
		WHEN @SubattentionCode = 31 THEN 'BANCO SANGRE'
		WHEN @SubattentionCode = 32 THEN 'TERAPIAS'
		WHEN @SubattentionCode = 33 THEN 'AMBULANCIA'
		WHEN @SubattentionCode = 34 THEN 'FACTURA INTEGRAL'
	 END
End
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que convierte un código numérico de sub-atención (tipo de servicio o concepto de facturación) en su nombre descriptivo en español. Actúa como tabla de referencia para los 34 tipos de sub-atención definidos en contratos, cubriendo categorías como estancias hospitalarias, UCI, honorarios médicos, insumos, medicamentos, exámenes de laboratorio e imagen, terapias y factura integral. Se usa en reportes de contratos y facturación para mostrar el nombre legible del tipo de servicio a partir de su código, evitando joins a tablas maestras. Útil cuando se necesita describir conceptos de cobro como ''habitación compartida'', ''honorarios cirujanos'', ''laboratorio'', ''radiología'', ''UCI neonatal'', entre otros.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'SubattentionName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'SubattentionName';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Traduce un código numérico de subtipo de atención a su nombre descriptivo en español dentro del catálogo de servicios hospitalarios.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'SubattentionName';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El código recibido debe estar en el rango 1..34; cualquier otro valor produce NULL', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'SubattentionName';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Mapeo 1:1 fijo entre código y nombre de subtipo de atención; El catálogo soporta exactamente 34 subtipos de atención; La función es determinística y no accede a tablas; Códigos no contemplados devuelven NULL en lugar de error', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'SubattentionName';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Subtipo de atención; Hospitalización; UCI (adultos, neonatal, cuidados medianos); Consulta médica; Honorarios profesionales (cirujano, anestesia, ayudantía, instrumentación); Derechos quirúrgicos (sala, anestesia, equipo); Insumos y medicamentos hospitalarios; Apoyo diagnóstico (laboratorio, imágenes, hemodinamia); Banco de sangre; Ambulancia; Factura integral', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'SubattentionName';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (scalar return): Cuando el código coincide con uno de los valores 1..34, retorna la etiqueta correspondiente (p.ej. 4=''UCI ADULTOS'', 21=''MEDICAMENTOS'', 34=''FACTURA INTEGRAL''); en cualquier otro caso retorna NULL', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'SubattentionName';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Código = 1 → Devuelve ''NINGUNO''; si Código entre 2 y 7 → Devuelve categorías de hospitalización/UCI (estancia individual, habitación compartida, UCI adultos/neonatal/cuidados medianos, incubadora); si Código entre 8 y 11 → Devuelve categorías de consultas (general, especialista, interconsulta, visitas hospitalarias); si Código entre 12 y 15 → Devuelve honorarios profesionales (cirujanos, anestesia, ayudantía, instrumentación); si Código entre 16 y 18 → Devuelve derechos quirúrgicos (sala, anestesia, equipo); si Código entre 19 y 22 → Devuelve suministros (insumos hospitalarios, materiales, medicamentos, oxígeno); si Código entre 23 y 30 → Devuelve servicios de apoyo diagnóstico (laboratorio, radiología, tomografías, medicina nuclear, resonancia, exámenes complementarios/vasculares, hemodinamia); si Código entre 31 y 34 → Devuelve servicios complementarios (banco sangre, terapias, ambulancia, factura integral); si Código fuera de 1..34 → Retorna NULL (CASE sin ELSE)', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'SubattentionName';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'SubattentionName';
GO
