-- =============================================
-- Author:		<Author,Juan David Patiño Cabrea,Name>
-- Create date: <Create Date,26-10-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas de Hemovigilancia>
-- =============================================
CREATE PROCEDURE [dbo].[SP_CAL_ListarFichaHemovigilancia]
(
  @IdFicha as Int
)

AS
BEGIN
  SET NOCOUNT ON;

  
declare @FechaActual as date = [Common].[GETDATE]()
Select Rtrim(B.INDNOMEMP) AS 'Nombre',Rtrim(B.INDDIREMP) AS 'Direccion',Rtrim(B.INDTE1EMP) AS 'Telefono',RTRIM(REPORTEREALIZADOPOR) AS 'Realizado por',Rtrim(CARGO) as 'Cargo',	convert(varchar(10),C.FECHAREPORTE,103) as 'Fecha Toma reporte',
       Rtrim(NOMBRECOMP) as 'Nombre Paciente', Case SEXO when 1 then 'X' end as 'Masculino', Case SEXO when 2 then 'X' end as 'Femenino',convert(varchar(10),C.FECNACIMIENTO) as'Fecha Nacimiento', [dbo].[EDAD] (C.FECNACIMIENTO,@FechaActual) As 'Edad',
	   IPCODPACI AS 'Identificacion', Rtrim(D.NOMENTIDA) as 'Nombre Entidad', Rtrim(C.DIRECCION) as 'Direccion',Rtrim(C.TELEFONO) as 'Telefonos', X.UFUDESCRI,
	  ---4 
	   Case ANTETRANSFUSIONALES when 1 then 'X' end as 'Ante_Transfusionales_SI',Case ANTETRANSFUSIONALES when 0 then 'X' end as 'Ante_Transfusionales_NO',convert(varchar(10),A.FECHA) as 'Fecha_Antecedente1',
	   Rtrim(E.NOMDIAGNO) '4_Diagnostico',Rtrim(COMPONENTE) as 'Componente',
	   Case ANTEREACCIONESADVERSAS when 1 then 'X' end as 'Ante_Adversas_SI',Case ANTEREACCIONESADVERSAS when 0 then 'X' end as 'Ante_Adversas_NO',convert(varchar(10),A.FECHAANTECEDENTE) as 'Fecha_Ante_Adverso_2',
	   Rtrim(TIPOREACCIONTRANSFUSIONAL) as 'Tipo_Reaccion',Rtrim(ANTEOBSTETRICO) as 'Antecedentes Obstetricos',Rtrim(ANTEPATOLOGICOS) 'Antecedentes Patologicos',Rtrim(COMPROMISOINMUNOLO) as 'Compromiso Inmunologico',
	   Rtrim(F.NOMDIAGNO) 'Diagnostico Principal',Rtrim(G.NOMDIAGNO) 'Otro Diagnostico',Rtrim(GRUPOSANGUINEO) as 'Grupo Sanguineo',
	   Rtrim(MEDICACIONPREVIA) as 'Medicacion previa',Rtrim(MOTIVOREALIZATRANSFUSION) as 'Motivo_realiza_transfision', 
	   --- 5 
	  Case MOMENTOPRESENTACION when 1 then 'X' end as 'Durante Transfusion', Case MOMENTOPRESENTACION when 2 then 'X' end as 'Postransfusion',
	  HORA AS '5_Horas' , DIAS as '5_Dias', MESES as '5_Meses',convert(varchar(10),A.FECHATRANSFUSION) as  'Fecha de la transfusion',
	  HORAINICIOTRANSFUSION AS '5_Horas_transfusion' , Convert(varchar(10),A.FECHAINICIOREACCION) as 'Fecha Inicio reaccion',
	  HORAINICIOREACCION AS '5_Horas_Reaccion',
	  ----6
	  PRETEMPERATURA as 'Pre_Temperatura',POSTEMPERATURA as 'Pos_Temperatura',
	  PREPRESIONARTERIAL as 'Pre_presionArterial',POSPRESIONARTERIAL as 'Pos_presionArterial',
	  PREFRECUENCIACARDIACA as 'Pre_FrecuenciaCardiaca',POSFFRECUENCIACARDIACA as 'Pos_FrecuenciaCardiaca',
	  PREFRECUENCIARESPIRATORIA as 'Pre_FrecuenciaRespiratoria',POSFRECUENCIARESPIRATORIA as 'Pos_FrecuenciaRespiratoria',
	  case FIEBRE when 1 then 'X' end as 'FIEBRE',
	  case ESCALOFRIO when 1 then 'X' end as 'ESCALOFRIO',
	  case HIPOTENSION when 1 then 'X' end as 'HIPOTENSION',
	  case HIPERTENSION when 1 then 'X' end as 'HIPERTENSION',
	  case OLIGUARIA when 1 then 'X' end as 'OLIGUARIA',
	  case CONVULSIONES when 1 then 'X' end as 'CONVULSIONES',
	  case HEMORRAGIA when 1 then 'X' end as 'HEMORRAGIA',
	  case URTICARIA when 1 then 'X' end as 'URTICARIA',
	  case NAUSEAS when 1 then 'X' end as 'NAUSEAS',
	  case ICTERICIA when 1 then 'X' end as 'ICTERICIA',
	  case TAQUICARDIA when 1 then 'X' end as 'TAQUICARDIA',
	  case SOMNOLENCIA when 1 then 'X' end as 'SOMNOLENCIA',
	  case DOLORLUMBAR when 1 then 'X' end as 'DOLORLUMBAR',
	  case DOLORTORACICO when 1 then 'X' end as 'DOLORTORACICO',
	  case DOLORINFUSION when 1 then 'X' end as 'DOLORINFUSION',
	  case CAFALEA when 1 then 'X' end as 'CAFALEA',
	  case PRURITO when 1 then 'X' end as 'PRURITO',
	  case CONFUSION when 1 then 'X' end as 'CONFUSION',
	  case HIPOXEMIA when 1 then 'X' end as 'HIPOXEMIA',
	  case PALIDEZ when 1 then 'X' end as 'PALIDEZ',
	  case DISNEA when 1 then 'X' end as 'DISNEA',
	  case TOS when 1 then 'X' end as 'TOS',
	  case CIANOSIS when 1 then 'X' end as 'CIANOSIS',
	  case ESTUPOR when 1 then 'X' end as 'ESTUPOR',
	  case ARRITMIAS when 1 then 'X' end as 'ARRITMIAS',
	  case PARESTESIAS when 1 then 'X' end as 'PARESTESIAS',
	  case TETANIA when 1 then 'X' end as 'TETANIA',
	  case ERITRODERMIA when 1 then 'X' end as 'ERITRODERMIA',
	  case ORTOPNEA when 1 then 'X' end as 'ORTOPNEA',
	  case ANSIEDAD when 1 then 'X' end as 'ANSIEDAD',
	  case ERITEMA when 1 then 'X' end as 'ERITEMA',
	  case EDEMA when 1 then 'X' end as 'EDEMA',
	  case CHOQUE when 1 then 'X' end as 'CHOQUE',
	  case DIARREA when 1 then 'X' end as 'DIARREA',
	  case PETEQUIAS when 1 then 'X' end as 'PETEQUIAS',
	  case PURPURA when 1 then 'X' end as 'PURPURA',
	 ----- 7
	  case SANGRECOMPLETA when 1 then 'X' end as 'SANGRECOMPLETA',
	  case ERITROCITOS when 1 then 'X' end as 'ERITROCITOS',
	  case PLAQUETAS when 1 then 'X' end as 'PLAQUETAS',
	  case PLASMAFRESCO when 1 then 'X' end as 'PLASMAFRESCO',
	  case PLASMACONGELADO when 1 then 'X' end as 'PLASMACONGELADO',
	  case CRIOPRECIPITADO when 1 then 'X' end as 'CRIOPRECIPITADO',
	  Rtrim(HEMOCOMPONENTE1) 'Hemocomponente_1',Rtrim(HEMOCOMPONENTE2) 'Hemocomponente_2',
	  Rtrim(MODIFICADO1) 'Modificado_1',Rtrim(MODIFICADO2) 'Modificado_2',
	  Rtrim(BANCOSANGRE1) 'Banco_1',Rtrim(BANCOSANGRE2) 'Banco_2',
	  Rtrim(GRUPORH1) 'GrupoRH_1',Rtrim(GRUPORH2) 'GrupoRH_2',
	  Rtrim(IDENTIFICACIONUNIDAD1) 'IdentificacionUndiad_1',Rtrim(IDENTIFICACIONUNIDAD2) 'IdentificacionUndiad_2',
	  convert(varchar(10),A.FECHAVENCIMIENTO1) as 'Fecha Vencimiento 1',convert(varchar(10),A.FECHAVENCIMIENTO2) as 'Fecha Vencimiento 2',
	  Rtrim(MLADMINISTRADO1) 'mlAdministrados_1',Rtrim(MLADMINISTRADO2) 'mlAdministrados_2',
	  Rtrim(DURACIONTRANSFU1) 'DuracionTransfu_1',Rtrim(DURACIONTRANSFU2) 'DuracionTransfu_2',
	  ---- 8
	  case INTERRUPCION when 1 then 'X' end as 'INTERRUPCION',
	  case VASOPRESORES when 1 then 'X' end as 'VASOPRESORES',
	  case ANTIHISTAMINICOS when 1 then 'X' end as 'ANTIHISTAMINICOS',
	  case SUPLENCIADEO2 when 1 then 'X' end as 'SUPLENCIADEO2',
	  case BROCONDILATADORES when 1 then 'X' end as 'BROCONDILATADORES',
	  case LIQUIDOENDOVENOSO when 1 then 'X' end as 'LIQUIDOENDOVENOSO',
	  case ANALGESICOS when 1 then 'X' end as 'ANALGESICOS',
	  case ANTIPIRETICOS when 1 then 'X' end as 'ANTIPIRETICOS',
	  case DIURETICOS when 1 then 'X' end as 'DIURETICOS',
	  case GASESARTERIALES when 1 then 'X' end as 'GASESARTERIALES',
	  case ELECTROLITOS when 1 then 'X' end as 'ELECTROLITOS',
	  case ESTEROIDES when 1 then 'X' end as 'ESTEROIDES',
	  case CUADROHEMATICO when 1 then 'X' end as 'CUADROHEMATICO',
	  case ELECTROCARDIGRAMA when 1 then 'X' end as 'ELECTROCARDIGRAMA',
	  Rtrim(OTROS) as '8_Otros',
	  ---9
	  Rtrim(PREHEMORECEPTOR) as 'Pre_Receptor',Rtrim(POSHEMORECEPTOR) as 'Pos_Receptor',Rtrim(POSHEMORECEPTOR) as 'Identificacion_Receptor',
	  Rtrim(PREHEMOUNIDAD) as 'Pre_unidad',Rtrim(POSHEMOUNIDAD) as 'Pos_Unidad',Rtrim(IDENHEMOUNIDAD) as 'Identificacion_Unidad',
	  Rtrim(PREPRUEBAS) as 'Pre_Cruzadas',Rtrim(POSPRUEBAS) as 'Pos_Cruzadas',Rtrim(IDENPRUEBAS) as 'Identificacion_Cruzadas',
	  Rtrim(COOMBS1) as 'COOMBS1',Rtrim(COOMBS2) as 'COOMBS2', Rtrim(IDENRASTREO1) as 'Rastreo_1',
	  Rtrim(ENZIMA1) as 'ENZIMA1',Rtrim(ENZIMA2) as 'ENZIMA2', Rtrim(IDENRASTREO2) as 'Rastreo_2',
	  Rtrim(PREBUN) as 'PREBUN',Rtrim(POSBUN) as 'POSBUN', 
	  Rtrim(PRECREATININA) as 'PRECREATININA',Rtrim(POSCREATININA) as 'POSCREATININA',
	  Rtrim(PREBILIRRUBINA) as 'PREBILIRRUBINA',Rtrim(POSBILIRRUBINA) as 'POSBILIRRUBINA',
	  Rtrim(PREHEMOGLO) as 'PREHEMOGLO',Rtrim(POSHEMOGLO) as 'POSHEMOGLO',
	  Rtrim(PREPRUEBASHEMOLISIS) as 'pruebas_Hemodialisis',HBLIBRE, HEMOLISIS,
	  Rtrim(PRESENSIBILIZACION) as 'PRESENSIBILIZACION',Rtrim(POSSENSIBILIZACION) as 'POSSENSIBILIZACION',
	  PRERESULTADO,POSRESULTADO,TINCIONGRAM,
	  ---10
	  Case SEVERIDADREACCION when 1 then 'X' end as 'LEVE',
	  Case SEVERIDADREACCION when 2 then 'X' end as 'MODERADA',
	  Case SEVERIDADREACCION when 3 then 'X' end as 'SEVERA',
	  Case SEVERIDADREACCION when 4 then 'X' end as 'MUERTE',
	  Case SEVERIDADREACCION when 5 then 'X' end as 'NO DETERMINADA',
	  Rtrim(MEDICORESPONSABLE) as 'medico responsable',
	  ---11
	  Case IMPUTABILIDAD when 1 then 'X' end as 'GRADO_0',
	  Case IMPUTABILIDAD when 2 then 'X' end as 'GRADO_1',
	  Case IMPUTABILIDAD when 3 then 'X' end as 'GRADO_2',
	  Case IMPUTABILIDAD when 4 then 'X' end as 'GRADO_3',
	  Case IMPUTABILIDAD when 5 then 'X' end as 'NO EVALUABLE',
	  --12 I
	  case CHEKHEMOLISISNOINMUNE when 1 then 'X' end as 'CHEKHEMOLISISNOINMUNE',
	  case CHEKHIPOTENSION when 1 then 'X' end as 'CHEKHIPOTENSION',
	  case CHEKREACCIONHEMOLITICAS when 1 then 'X' end as 'CHEKREACCIONHEMOLITICAS',
	  case CHEKREACCIONALERGICA when 1 then 'X' end as 'CHEKREACCIONALERGICA',
	  case CHEKTRALI when 1 then 'X' end as 'CHEKTRALI',
	  case CHEKHIPERTENSION when 1 then 'X' end as 'CHEKHIPERTENSION',
	  case CHEKSOBRECARGA when 1 then 'X' end as 'CHEKSOBRECARGA',
	  case CHEKHIPOTERMIA when 1 then 'X' end as 'CHEKHIPOTERMIA',
	  case CHEKTOXICIDAD when 1 then 'X' end as 'CHEKTOXICIDAD',
	  case CHEKTRASTORNOS when 1 then 'X' end as 'CHEKTRASTORNOS',
	  case CHEKREACCIONFEBRIL when 1 then 'X' end as 'CHEKREACCIONFEBRIL',
	  --II
	  case CHEKREACCIONHEMOLITICA when 1 then 'X' end as 'CHEKREACCIONHEMOLITICA',
	  case CHEKPURPURA when 1 then 'X' end as 'CHEKPURPURA',
	  case CHEKENFERMEDADINJERTO when 1 then 'X' end as 'CHEKENFERMEDADINJERTO',
	  case CHEKINMUNOMODULACION when 1 then 'X' end as 'CHEKINMUNOMODULACION',
	  case CHEKSOBRECARGAHIERRO when 1 then 'X' end as 'CHEKSOBRECARGAHIERRO',
	 --III
	  case CHEKINFECIONVIRAL when 1 then 'X' end as 'CHEKINFECIONVIRAL',
	  Rtrim(NOMBREINFECIONVIRAL) as 'NOMBREINFECIONVIRAL',
	  
	  case CHEKINFECCIONBACTERIANA when 1 then 'X' end as 'CHEKINFECCIONBACTERIANA',
	  Rtrim(NOMBREINFECCIONBACTERIANA) as 'NOMBREINFECCIONBACTERIANA',

	  case CHEKOTRASINFECCIONES when 1 then 'X' end as 'CHEKOTRASINFECCIONES',
	  Rtrim(NOMBREOTRASINFECCIONES) as 'NOMBREOTRASINFECCIONES',
	  ---13
	case ESTATUSINVESTIGACION when 1 then 'X' end as 'Progreso',
	case ESTATUSINVESTIGACION when 2 then 'X' end as 'Concluida',
	case ESTATUSINVESTIGACION when 3 then 'X' end as 'No pudo ser realizada',
	---14
	case LOCALIZACIONRAT when 1 then 'X' end as 'Seleccion',
	case LOCALIZACIONRAT when 2 then 'X' end as 'Recoleccion',
	case LOCALIZACIONRAT when 3 then 'X' end as 'Check Identificacion',
	case LOCALIZACIONRAT when 4 then 'X' end as 'Procesamiento',
	case LOCALIZACIONRAT when 5 then 'X' end as 'Almacenamiento',
	case LOCALIZACIONRAT when 6 then 'X' end as 'Distribuicion',
	case LOCALIZACIONRAT when 7 then 'X' end as 'Transfusion',
	--15
	Rtrim(PLANMEJORAMIENTO) as 'PLANMEJORAMIENTO'
From CALHEMOVIGILANCIA  A
	  Inner Join INEMPRESU B on A.EMPRESA = B.INDCODEMP
	  Inner Join CALREPORTE C on A.IDCALREPORTE = C.ID
	  Inner Join INENTIDAD  D on D.CODENTIDA  = C.CODENTIDA
	  Inner Join INUNIFUNC  X on X.UFUCODIGO   = C.UFUCODIGO
	  LEFT Join INDIAGNOS  E on E.CODDIAGNO  = A.DIAGNOSTICO 
	  LEFT Join INDIAGNOS  F on E.CODDIAGNO  = A.DIAGNOSTICOPRINCIPAL 
	  LEFT Join INDIAGNOS  G on E.CODDIAGNO  = A.OTRODIAGNOSTICO 
 
END


select * from INUNIFUNC