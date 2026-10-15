
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