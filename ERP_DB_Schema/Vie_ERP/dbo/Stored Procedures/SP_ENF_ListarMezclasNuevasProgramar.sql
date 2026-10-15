
CREATE PROCEDURE [dbo].[SP_ENF_ListarMezclasNuevasProgramar]
(
@CodigoPaciente varchar(25),
@Ingreso Char(10)
)
AS
BEGIN
	SET NOCOUNT ON;
             
	select  
		A.CONSECUTI as 'IDHCINFLIQA', A.CODCONCEC, A.CODCONCEC_ORIGEN,
		case a.METAPLMED when '1' then 'Bolo Mezcla' when '2' then 'Infusion Mezcla Continua' when '3' then 'Bolo Medicamento'  end as 'Solicitud',A.METAPLMED,
		'Mezcla Infusión Continua' As 'Tipo Mezcla',A.TIPMEZLIQ,
		Rtrim(a.MEZLIQPAC)  as 'Productos',
		Rtrim(a.ADMMEZLIQ) as 'Dosis',
		Rtrim(a.INDAPLMED) as 'Instrucciones Adicionales',
		a.FECHAINIC as 'Inicio Aplicacion',
		case a.METAPLMED when '1' then 'Dosis única' when '2' then rtrim(c.DURACIINFM) when '3' then 'Dosis única' end as 'Duracion',
		rtrim(b.NOMMEDICO)  As 'Prescribe'
	from HCINFLIQA As a 
		Inner Join INPROFSAL b WITH (NOLOCK) ON a.CODPROSAL = b.CODPROSAL 
		Inner Join HCINFLIQD c WITH (NOLOCK) ON a.CODCONCEC = c.CODCONCEC
	where a.TIPMEZLIQ = 1 AND a.IPCODPACI = @CodigoPaciente AND a.NUMINGRES = @Ingreso AND (a.MEDPROGRAMADO = 0 OR a.MEDPROGRAMADO = NULL) AND PREESTADO <> 4    --1.Mezcla Continua
union all
	select  
		A.CONSECUTI As 'IDHCINFLIQA', A.CODCONCEC, A.CODCONCEC_ORIGEN,
		case A.TIPMEZLIQ when 3 then 'Infusion Mezcla Frecuencia' when 4 then 'Mezcla Magistral' end  As 'Solicitud',A.METAPLMED,
		case a.TIPMEZLIQ when 3 then 'Mezcla Infusión Frecuencia' when 4 Then 'Mezcla Magistral' end as 'Tipo Mezcla',A.TIPMEZLIQ,
		Rtrim(a.MEZLIQPAC)  as 'Productos',
		Rtrim(a.ADMMEZLIQ) as 'Dosis',
		Rtrim(INDAPLMED) as 'Instrucciones Adicionales',
		a.INICIOAPLICACION as 'Inicio Aplicacion',
		a.TIPODURACION as 'Duracion',
		rtrim(b.NOMMEDICO)  As 'Prescribe'
	from HCINFLIQA As a 
		Inner Join INPROFSAL b WITH (NOLOCK) ON a.CODPROSAL = b.CODPROSAL 
	where A.TIPMEZLIQ in (3,4)  AND a.IPCODPACI = @CodigoPaciente AND a.NUMINGRES = @Ingreso and (a.MEDPROGRAMADO = 0 OR a.MEDPROGRAMADO = NULL) AND PREESTADO <> 4 --3.Mezcla Frecuencia y 4.Mezcla Magistral
UNION ALL
	select  
		A.CONSECUTI as 'IDHCINFLIQA', A.CODCONCEC, A.CODCONCEC_ORIGEN,
		case a.METAPLMED when '4' then 'Infusion Líquidos' when '5' then 'Bolo Medicamento Líquidos' END as 'Solicitud',A.METAPLMED,
		'Líquidos' As 'Tipo Mezcla',A.TIPMEZLIQ,
		Rtrim(a.MEZLIQPAC)  as 'Productos',
		Rtrim(a.ADMMEZLIQ) as 'Dosis',
		Rtrim(a.INDAPLMED) as 'Instrucciones Adicionales',
		a.FECHAINIC as 'Inicio Aplicacion',
		case a.METAPLMED when '5' then 'Dosis única' when '4' then rtrim(c.DURACIINF)  end as 'Duracion',
		rtrim(b.NOMMEDICO)  As 'Prescribe'
	from HCINFLIQA As a 
			Inner Join INPROFSAL b WITH (NOLOCK) ON a.CODPROSAL = b.CODPROSAL 
			Inner Join HCINFLIQD c WITH (NOLOCK) ON a.CODCONCEC = c.CODCONCEC
	where a.TIPMEZLIQ = 2 AND a.IPCODPACI = @CodigoPaciente AND a.NUMINGRES = @Ingreso AND (a.MEDPROGRAMADO = 0 OR a.MEDPROGRAMADO = NULL) AND PREESTADO <> 4   --2.Liquidos
UNION ALL

	 -- Nutrición Parenteral 

	select  
	A.ID AS 'IDHCINFLIQA', 
	A.ID AS CODCONCEC, 
	NULL AS CODCONCEC_ORIGEN,
	'Nutrición Parenteral' AS 'Solicitud',
	'6' AS 'METAPLMED',
	NULL AS 'Tipo Mezcla',
	'5' AS TIPMEZLIQ,
        Rtrim(c.FullProductName) as 'Productos',
        Rtrim(a.INDICACIONADM) as 'Dosis',
        Rtrim(a.INDICACIONADI) as 'Instrucciones Adicionales',
		a.FECHAORDEN as 'Inicio Aplicacion',
		case a.TIPOINFUSION when '1' then 'Continua' when '2' then 'Ciclada' end as 'Duracion',
        rtrim(b.NOMMEDICO) As 'Prescribe'
		
    from HCNUTPAREC As a 
        Inner Join INPROFSAL b WITH (NOLOCK) ON a.CODPROSAL = b.CODPROSAL
		Inner join [MedicalHistory].[ProductSusceptibleMixingStation] c WITH (NOLOCK) ON a.ID = c.IdOrigin AND c.Origin = 'HCNUTPAREC'
		--inner join [MedicalHistory].[PharmaDose]  D ON D.CodeSusceptibleMixingStation = C.CodeSusceptibleMixingStation --AND D.IsDispensed = 1
		where A.IPCODPACI = @CodigoPaciente AND a.NUMINGRES = @Ingreso  AND A.STATUS = 1
end