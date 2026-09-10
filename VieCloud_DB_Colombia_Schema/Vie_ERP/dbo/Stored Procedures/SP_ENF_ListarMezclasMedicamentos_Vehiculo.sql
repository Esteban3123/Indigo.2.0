
CREATE PROCEDURE [dbo].[SP_ENF_ListarMezclasMedicamentos_Vehiculo]
(
@CodigoPaciente varchar(25),
@Ingreso Char(10),
@IDCabeceraMezcla integer
)
AS
BEGIN
	SET NOCOUNT ON;
             
select  
		A.NUMINGRES,
		A.IPCODPACI,
		CONVERT(bit, 0) as 'Vehiculo',
		A.CODCONCEC,
		rtrim(b.CODPRODUC) AS 'Codigo',
		rtrim(e.DESPRODUC) AS 'Medicamento',
		e.TIPFORMED As 'TipoFormulacion',
		e.PESTOTMED As 'PesoBase',
		e.CODUNIPES As 'CodigoPesoBase',
		e.VOLTOTMED As 'VolumenBase',
		e.CODUNIVOL As 'CodigoVolumenBase',
		e.CODUNIADM As 'CodigoAdminBase',
		b.CONMEDMEZ as 'Concentracion',
		(b.CANPROCAL / b.NUMEROAPLICACIONES) as 'Cantidad',B.CANPROCAL AS 'Cantidad Total',
		A.TIPMEZLIQ,
		case A.TIPMEZLIQ when '1' then 'Mezcla Continua' when '2' then 'Liquido' when '3' then 'Mezcla Frecuencia' when '4' then 'Mezcla Magistral' end as 'Tipo Mezcla',
		a.METAPLMED,case a.METAPLMED when '1' then 'Bolo Mezcla' when '2' then 'Infusion Mezcla Continua' when '3' then 'Bolo Medicamento'  end as 'Solicitud',
		Rtrim(d.MEZLIQPAC)  as 'Productos',
		Rtrim(d.ADMMEZLIQ) as 'Instrucciones',
		Rtrim(d.INDAPLMED) as 'Instrucciones Adicionales',
		d.FECHAINIC as 'Inicio Aplicacion',
		Rtrim(A.CODPROSAL)as 'Codigo Medico Orden',
		c.DURACIINFM,c.VALDURINFM,c.UNIDURINFM,
		NULL AS 'Duracion Liquidos',NULL  AS 'Valor Duracion Fija Liquidos', NULL AS 'Unidad Duracion Fija Liquidos',
		NULL AS 'Frecuencia Aplicacion Liquidos', NULL AS 'Unidad Frecuencia Aplicacion Liquidos', NULL AS 'FechaInicioAplicacionLiquidos',
		NULL AS 'Unidad Medida Apliacion Bolo Liquidos',NULL  AS 'Unidad Medida Infusion Aplicacion Liquidos', NULL AS 'UnidadMedidaInfusionLiquidos',
		NULL  AS 'Dosis Infusion Liquidos', NULL  AS'Dosis Bolo Liquidos',
		case a.METAPLMED when '1' then c.VADUBOMEZ ELSE NULL end as 'DuracionBoloMezcla',
		case a.METAPLMED when '1' then c.UNDUBOMEZ ELSE NULL end as 'MedidaDuracionBoloMezcla',
		case a.METAPLMED when '1' then c.DOBOLOMEZ when '3' THEN c.DOSISBOLO ELSE NULL end as 'DosisBolo',
		case a.METAPLMED when '1' then c.UNMEBOMEZ when '3' THEN c.UNIMEDBOM ELSE NULL end as 'UnidadMedidaBolo',
		D.DOSISUNICA, D.DOSISAPLICACION,D.CODUNIMEDIAPLICACION,D.INICIOAPLICACION,D.DURINFUSION,D.UNIDADINFUSION, D.DURFRECUENCIA, D.UNIDADFRECUENCIA, D.TIPODURACION, D.DURACIONFIJA,D.UNIDADDURFIJA, B.UNIMEDMED AS 'CodUnidadMedida', RTRIM(f.DESUNIMED) AS 'Unidad',
		ESMEZINFU, C.UNIMEINFM as 'UndMedInfusion', C.CANMEINFM as 'CantidadInfusion', C.CANCCHORA as 'HoraInfusion', C.INFTITULA as 'EsTitulable', C.UNIMEINFT as 'UnidadInfusionTitulable', C.CANMEINFT as 'CantidadInfusionTitulable', C.CANCCHORT as 'HoraInfusionTitulable', 
		C.FECINIINFM as 'FechaInicio'
		--DOSISINFU, UNIMEDINF, FRECUEINF, UNIFREINF, FECINIINF, --DURACIINF, VALDURINF, UNIDURINF
	FROM HCINFLIQC As A --Cabecera de Mezclas
		Inner Join HCINFCONC b WITH (NOLOCK) ON a.CODCONCEC = b.CODCONCEC --Medicamentos
		INNER Join HCINFLIQA d WITH (NOLOCK) ON a.CODCONCEC = d.CODCONCEC  or a.CODCONCEC = d.CODCONCEC_ORIGEN --Datos generales administración
		Inner Join IHLISTPRO e WITH (NOLOCK) ON b.CODPRODUC = e.CODPRODUC 
		Left Join HCINFLIQD c WITH (NOLOCK) ON a.CODCONCEC = c.CODCONCEC --Diluyente/Infusión
		Left Join INUNIMEDI f WITH (NOLOCK) ON b.UNIMEDMED = f.CODUNIMED
	WHERE a.CODCONCEC = @IDCabeceraMezcla  AND A.NUMINGRES = @Ingreso AND A.IPCODPACI = @CodigoPaciente
UNION all
SELECT  
		A.NUMINGRES,
		A.IPCODPACI,
		case A.TIPMEZLIQ WHEN '2' THEN CONVERT(bit, 0) else CONVERT(bit, 1) END AS 'Vehiculo',
		A.CODCONCEC,
		rtrim(b.CODPRODUC) AS 'Codigo',
		rtrim(e.DESPRODUC) AS 'Medicamento',
		e.TIPFORMED As 'TipoFormulacion',
		e.PESTOTMED As 'PesoBase',
		e.CODUNIPES As 'CodigoPesoBase',
		e.VOLTOTMED As 'VolumenBase',
		e.CODUNIVOL As 'CodigoVolumenBase',
		e.CODUNIADM As 'CodigoAdminBase',
		CASE A.TIPMEZLIQ WHEN '1' THEN (CASE A.METAPLMED WHEN '1' THEN B.DOBOLOMEZ ELSE B.CANPASDIL END) WHEN '2' THEN (CASE A.METAPLMED WHEN '4' THEN b.DOSISINFU WHEN '5' THEN b.DOSISBOLO END)   ELSE B.CANPASDIL END AS 'Concentracion',
		(B.CANPROCAL / b.NUMEROAPLICACIONES) as 'Cantidad',B.CANPROCAL AS 'Cantidad Total',
		A.TIPMEZLIQ,
		case A.TIPMEZLIQ when '1' then 'Mezcla Continua' when '2' then 'Liquido' when '3' then 'Mezcla Frecuencia' when '4' then 'Mezcla Magistral' end as 'Tipo Mezcla',
		a.METAPLMED,case a.METAPLMED when '1' then 'Bolo Mezcla' when '2' then 'Infusion Mezcla Continua' when '3' then 'Bolo Medicamento' WHEN '4' THEN 'Infusión Líquidos' WHEN '5' THEN 'Bolo Medicamento Líquido'  end as 'Solicitud',
		Rtrim(d.MEZLIQPAC)  as 'Productos',
		Rtrim(d.ADMMEZLIQ) as 'Instrucciones',
		Rtrim(d.INDAPLMED) as 'Instrucciones Adicionales',
		d.FECHAINIC as 'Inicio Aplicacion',
		Rtrim(A.CODPROSAL)as 'Codigo Medico Orden',
		b.DURACIINFM,b.VALDURINFM,b.UNIDURINFM,
		b.DURACIINF AS 'Duracion Liquidos',B.VALDURINF AS 'Valor Duracion Fija Liquidos',b.UNIDURINF AS 'Unidad Duracion Fija Liquidos',
		FRECUEINF AS 'Frecuencia Aplicacion Liquidos', UNIFREINF AS 'Unidad Frecuencia Aplicacion Liquidos', FECINIINF AS 'FechaInicioAplicacionLiquidos',
		UNIMEDBOL AS 'Unidad Medida Apliacion Bolo Liquidos',b.UNIMEDINF AS 'Unidad Medida Infusion Aplicacion Liquidos', RTRIM(f.DESUNIMED) AS 'UnidadMedidaInfusionLiquidos',
		DOSISINFU AS 'Dosis Infusion Liquidos', DOSISBOLO AS'Dosis Bolo Liquidos',
		case a.METAPLMED when '1' then b.VADUBOMEZ ELSE NULL end as 'DuracionBoloMezcla',
		case a.METAPLMED when '1' then b.UNDUBOMEZ ELSE NULL end as 'MedidaDuracionBoloMezcla',
		case a.METAPLMED when '1' then b.DOBOLOMEZ when '3' THEN b.DOSISBOLO ELSE NULL end as 'DosisBolo',
		case a.METAPLMED when '1' then b.UNMEBOMEZ when '3' THEN b.UNIMEDBOM ELSE NULL end as 'UnidadMedidaBolo',
		D.DOSISUNICA, D.DOSISAPLICACION,D.CODUNIMEDIAPLICACION,D.INICIOAPLICACION,D.DURINFUSION,D.UNIDADINFUSION, D.DURFRECUENCIA, D.UNIDADFRECUENCIA, D.TIPODURACION, D.DURACIONFIJA,D.UNIDADDURFIJA, isnull(B.UNIMEDDIL,B.UNIMEDBOM) AS 'CodUnidadMedida', RTRIM(f.DESUNIMED) AS 'Unidad',
		B.ESMEZINFU, B.UNIMEINFM as 'UndMedInfusion', B.CANMEINFM as 'CantidadInfusion', B.CANCCHORA as 'HoraInfusion', B.INFTITULA as 'EsTitulable', B.UNIMEINFT as 'UnidadInfusionTitulable', B.CANMEINFT as 'CantidadInfusionTitulable', B.CANCCHORT as 'HoraInfusionTitulable', 
		B.FECINIINFM as 'FechaInicio'
	FROM HCINFLIQC As A --Cabecera de Mezclas
		Inner Join HCINFLIQD b WITH (NOLOCK) ON a.CODCONCEC = b.CODCONCEC --Diluyente/Infusión
		Inner Join HCINFLIQA d WITH (NOLOCK) ON a.CODCONCEC = d.CODCONCEC or a.CODCONCEC = d.CODCONCEC_ORIGEN--Datos generales administración
		Inner Join IHLISTPRO e WITH (NOLOCK) ON b.CODPRODUC = e.CODPRODUC 
		Left Join INUNIMEDI f WITH (NOLOCK) ON b.UNIMEDDIL = f.CODUNIMED  OR b.UNIMEDBOM = f.CODUNIMED OR b.UNIMEDINF = f.CODUNIMED
	WHERE a.CODCONCEC = @IDCabeceraMezcla  AND A.NUMINGRES = @Ingreso AND A.IPCODPACI = @CodigoPaciente   AND
		  NOT EXISTS(SELECT xp.CODCONCEC FROM HCINFCONC xp WHERE xp.CODCONCEC  = @IDCabeceraMezcla AND xp.NUMINGRES = @Ingreso AND xp.IPCODPACI = @CodigoPaciente and xp.CODPRODUC = b.CODPRODUC )
END

/****** Object:  StoredProcedure [dbo].[SP_ENF_ListarMezclasNuevasProgramar]    Script Date: 6/2/2022 6:33:46 PM ******/
SET ANSI_NULLS ON
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista el detalle completo de las mezclas y preparaciones de medicamentos (mezclas continuas, líquidos, mezclas de frecuencia y mezclas magistrales) asociadas a un paciente, ingreso y cabecera de mezcla específicos. Combina la cabecera de la mezcla (HCINFLIQC), los medicamentos y concentraciones que la componen (HCINFCONC), los datos generales de administración (HCINFLIQA), la información de diluyentes e infusión (HCINFLIQD), el catálogo de productos farmacéuticos (IHLISTPRO) y las unidades de medida (INUNIMEDI) para devolver en una sola consulta tanto el medicamento principal (marcado como no vehículo) como el vehículo o diluyente (marcado como vehículo), con todos sus parámetros de dosificación: concentración, cantidad, tipo de formulación, pesos y volúmenes base, instrucciones de administración, duración, frecuencia, dosis de bolo e infusión. Es utilizado por el módulo de enfermería para visualizar y gestionar la preparación y administración de mezclas intravenosas en la historia clínica del paciente hospitalizado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ENF_ListarMezclasMedicamentos_Vehiculo';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ENF_ListarMezclasMedicamentos_Vehiculo';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los componentes (medicamentos y vehículo/diluyente) de una mezcla de infusión de líquidos para un paciente e ingreso, unificando la información de medicamentos registrados y del diluyente cuando éste no está como medicamento.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarMezclasMedicamentos_Vehiculo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente y número de ingreso deben existir en HCINFLIQC junto con la cabecera de mezcla solicitada; La cabecera de mezcla (CODCONCEC) debe corresponder al paciente e ingreso indicados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarMezclasMedicamentos_Vehiculo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La cantidad por aplicación se calcula siempre como CANPROCAL / NUMEROAPLICACIONES; El filtrado siempre exige coincidencia simultánea de CODCONCEC, NUMINGRES e IPCODPACI; El diluyente nunca se duplica como vehículo si ya existe como medicamento (HCINFCONC) con el mismo CODPRODUC; Las lecturas se hacen con NOLOCK (lecturas sucias permitidas); Los campos específicos de líquidos solo se pueblan en la rama del diluyente; en la rama de medicamentos van como NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarMezclasMedicamentos_Vehiculo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Mezcla de medicamentos; Infusión de líquidos; Diluyente / Vehículo; Bolo; Dosis; Unidad de medida; Frecuencia de aplicación; Médico que ordena; Mezcla Continua; Mezcla Frecuencia; Mezcla Magistral; Titulación de infusión', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarMezclasMedicamentos_Vehiculo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCINFLIQC: Devuelve filas combinando medicamentos (HCINFCONC) y vehículo/diluyente (HCINFLIQD) de la mezcla filtrada por CODCONCEC, NUMINGRES e IPCODPACI; [RETURN_RESULT] HCINFLIQD: Cuando NOT EXISTS un registro en HCINFCONC con el mismo CODPRODUC del diluyente, se incluye la fila del diluyente como vehículo (Vehiculo=1) salvo cuando TIPMEZLIQ=''2'' (Líquido) en cuyo caso Vehiculo=0', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarMezclasMedicamentos_Vehiculo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si A.TIPMEZLIQ = ''2'' (Líquido) en la rama del diluyente → Marca el registro con Vehiculo = 0 else Marca el registro con Vehiculo = 1 (es vehículo/diluyente); si TIPMEZLIQ = ''1'' → Etiqueta como ''Mezcla Continua'' y calcula Concentración según METAPLMED (''1''→DOBOLOMEZ, otro→CANPASDIL); si TIPMEZLIQ = ''2'' → Etiqueta como ''Liquido'' y calcula Concentración según METAPLMED (''4''→DOSISINFU, ''5''→DOSISBOLO); si TIPMEZLIQ = ''3'' → Etiqueta como ''Mezcla Frecuencia'' y usa CANPASDIL como Concentración; si TIPMEZLIQ = ''4'' → Etiqueta como ''Mezcla Magistral''; si METAPLMED = ''1'' (Bolo Mezcla) → Expone duración, unidad, dosis y unidad medida del bolo de mezcla (VADUBOMEZ, UNDUBOMEZ, DOBOLOMEZ, UNMEBOMEZ); si METAPLMED = ''3'' (Bolo Medicamento) → Expone DOSISBOLO y UNIMEDBOM como dosis y unidad de bolo else Devuelve NULL en duración del bolo de mezcla; si METAPLMED in (''4'',''5'') solo aplica en rama de líquidos → Solicitud se rotula como ''Infusión Líquidos'' o ''Bolo Medicamento Líquido'' respectivamente; si NOT EXISTS medicamento en HCINFCONC con el mismo CODPRODUC del diluyente → Se agrega el diluyente al resultado como fila adicional', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarMezclasMedicamentos_Vehiculo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCINFLIQC; dbo.HCINFCONC; dbo.HCINFLIQA; dbo.HCINFLIQD; dbo.IHLISTPRO; dbo.INUNIMEDI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarMezclasMedicamentos_Vehiculo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarMezclasMedicamentos_Vehiculo';
-- GO
