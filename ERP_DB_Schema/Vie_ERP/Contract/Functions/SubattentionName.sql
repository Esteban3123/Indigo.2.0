


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