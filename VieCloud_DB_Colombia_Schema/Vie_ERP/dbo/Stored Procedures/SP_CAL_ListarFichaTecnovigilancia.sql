-- =============================================
-- Author:		<Author,Juan David Patiño Cabrea,Name>
-- Create date: <Create Date,09-07-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas de tecnovigilancia>
-- =============================================
CREATE PROCEDURE [dbo].[SP_CAL_ListarFichaTecnovigilancia]
(
  @IdFicha as Int
)

AS
BEGIN
  SET NOCOUNT ON;

declare @FechaActual as date = [Common].[GETDATE]()
SELECT *, Causa1+'  '+Causa2+'  '+Causa3+'   '+Causa4+'  '+Causa5+'  '+Causa6+'   '+Causa7+'  '+Causa8+'  '+Causa9+'   '+Causa10+'  '+Causa11+'  '+Causa12+'   '+Causa13+'  '+Causa14+'  '+Causa15+'   '+Causa16+'  '+Causa17+'  '+Causa18+'   '+Causa19+'  '+Causa20+'  '+Causa21+'   '+Causa22+'  '+Causa23+'  '+Causa24+'   '+Causa25+'  '+Causa26+'  '+Causa27+'   '+Causa28+'  '+Causa29+'  '+Causa30+'   '+Causa31+'  '+Causa32+'  '+Causa33+'   '+Causa34+'  '+Causa35+'  '+Causa36+'   '+Causa37+'  '+Causa38+'  '+Causa39+'   '+Causa40+'  '+Causa41+'  '+Causa42+'   '+Causa43+'  '+Causa44+'  '+Causa45+'   '+Causa46+'  '+Causa47 AS Concatenacion FROM (
				SELECT Rtrim(A.NOMBREINSTI) AS 'Nombre Institucion',Rtrim(E.nomdepart) +' , '+ Rtrim(P.MUNNOMBRE)  as 'Departamento Municipio',Rtrim(A.NIT) AS 'NIT',CASE NIVELCOMPLEJI When 1 then 'Baja' When 2 then 'Mediana' When 3 then 'Alta' End as 'Nivel Complejidad', Case NATURALEZA When 1 then 'X' end as 'Pública', Case NATURALEZA When 2 then 'X' end as 'Privada', Case NATURALEZA When 3 then 'X' end as 'Mixta',
				CASE C.TIPOIDENTIFICA when 1 then 'Cédula ciudadanía' when 2 then 'Cédula extranjera' when 3 then 'Tarjeta identidad' when 4 then 'Registro civil' when 5 then 'Pasaporte' when 6 then 'Adulto sin ID' when 7 then 'Mednor sin ID' when 8 then 'NU' END AS 'Tipo Identificacion', Case SEXO when 1 then 'X' end as 'Masculino', Case SEXO when 2 then 'X' end as 'Femenino',
				[dbo].[EDAD] (C.FECNACIMIENTO,@FechaActual) As 'Edad',Rtrim(V.CODDIAGNO) +'-'+ Rtrim(V.NOMDIAGNO) AS 'Diagnostico Paciente',
				Rtrim(A.NOMBREGENERICO) AS 'Nombre Generico',Rtrim(A.NOMBRECOMERCAIL) as 'Nombre comercial',Rtrim(A.REGISTROSANITARIO) as 'Registro Sanitario',Rtrim(A.LOTE) as 'Lote',Rtrim(A.MODELO) as 'Modelo',Rtrim(A.REFERENCIA) As 'Referencia',RTRIM(A.SERIAL) As 'Serial',
				RTRIM(A.NOMBREFABRICANTE) As 'Nombre Fabricante',RTRIM(A.NOMBREIPORTADOR) As 'Nombre Razon Social',RTRIM(X.UFUDESCRI) As 'Area Funcionamiento',
				case DISPOSITIVOUTILI when 'True' then 'X' end as 'Si',case DISPOSITIVOUTILI when 'False' then 'X' end as 'No',
				convert(varchar(10),A.FECHAEVENTO,103) as 'Fecha Evento',convert(varchar(10),A.FECHAELABORACION,103) as 'Fecha Elaboracion',case DETENCIONANTES when 1 then 'X' end as 'Antes',case DETENCIONDURANTE when 1 then 'X' end as 'Durante',case DETENCIONDESPUES when 1 then 'X' end as 'despues',
				case CLASIEVENADVESERIO when 1 then 'X' end as 'Evento adverso serio',case CLASIEVENADVNOESERIO when 1 then 'X' end as 'Evento adverso no serio',case CLASIINCIADVESERIO when 1 then 'X' end as 'Incidente adverso serio',case CLASIINCIADVENOSERIO when 1 then 'X' end as 'Incidente adverso no serio',
				RTRIM(DESCRIPEVENTO) As 'Descripcion evento',
				Case DESENLACEEVENTO when 1 then 'Muerte' when 2 then 'Daño de una función o estructura corporal' when 3 then 'Enfermedad o daño que amenace la vida' when 4 then 'Requiere intervención médica o quirúrgica' when 5 then 'Incapacidad permanente parcial' when 6 then 'Hospitalización o prolongación de la misa' when 7 then 'Malformación congénita' when 8 then 'No hubo daño' when 9 then 'Otro' end as 'Desenlace',
				RTRIM(OTRODESENLACE) as 'Otro desenlace', Rtrim(A.ACCICORRECTIVA) as 'Accion Correctiva',
				case REPORFABRICANTE when 1 then 'X' end as 'Reporto Fabricante',case REPORFABRICANTE when 0 then 'X' end as 'No Reporto Fabricante', convert(varchar(10),A.FECHAREPORTE,103) as 'Fecha reporte fabricante', Case DISPOMEDICO when 1 then 'X' end as 'Si Dispositivo Medico',Case DISPOMEDICO when 0 then 'X' end as 'No Dispositivo Medico',
				case ENVIADISPOSITIVO when 1 then 'X' end as 'Si Enviado dispositivos', case ENVIADISPOSITIVO when 0 then 'X' end as 'No Enviado dispositivos', convert(varchar(10),FECHAENVIO,103) as 'Fecha envio',
				Rtrim(A.NOMBRE) as 'Nombre reportante',
				Rtrim(z.desactivi) as 'Profesion reportante',
				RTRIM(Y.UFUDESCRI) As 'Orghanizacion reportante',
				Rtrim(A.DIRECCION) as 'Direccion Reportante',
				Rtrim(A.TELEFONO) as 'Telefono Reportante',
				Rtrim(o.nomdepart) +' , '+ Rtrim(r.MUNNOMBRE)  as 'Departamento Municipio reportante',
				Rtrim(A.CORREO) as 'Correo reportante',
				convert(varchar(10),FECHANOTIFICA,103) as 'Fecha Notificacion',
				Case AUTORIZA when 1 then 'X' end as 'Si Auotiza',Case AUTORIZA when 0 then 'X' end as 'No Auotiza',
				CASE cAUSA500 WHEN 1 THEN '500 Uso anoramal' ELSE '' end AS Causa1, 
				CASE CAUSA510 WHEN 1 THEN '510 Respuesta fisiologica anormal o inexplicable' ELSE '' END AS Causa2,
				CASE CAUSA520 WHEN 1 THEN '520 Falla en la alarma' ELSE '' END AS Causa3, 
				CASE CAUSA530 WHEN 1 THEN '530 Uso de material biológico' ELSE '' END AS Causa4,
				CASE CAUSA540 WHEN 1 THEN '540 Calibración' ELSE '' END AS Causa5,
				CASE CAUSA550 WHEN 1 THEN '550 Hadware de computador' ELSE '' END AS Causa6,
				CASE CAUSA560 WHEN 1 THEN '560 Contaminación durante la producción' ELSE '' END AS Causa7,
				CASE CAUSA570 WHEN 1 THEN '570 Contaminación post-producción' ELSE '' END AS Causa8,
				CASE CAUSA580 WHEN 1 THEN '580 Diseño' ELSE '' END AS Causa9,
				CASE CAUSA590 WHEN 1 THEN '590 Desconexión' ELSE '' END AS Causa10,
				CASE CAUSA600 WHEN 1 THEN '600 Componente eléctrico' ELSE '' END AS Causa11,
				CASE CAUSA610 WHEN 1 THEN '610 Circuito eléctrico' ELSE '' END AS Causa12,
				CASE CAUSA620 WHEN 1 THEN '620 Contacto eléctrico' ELSE '' END AS Causa13,
				CASE CAUSA630 WHEN 1 THEN '630 Interferencia Eletromagnética EIM' ELSE '' END AS Causa14,
				CASE CAUSA640 WHEN 1 THEN '640 Fecha de expiración' ELSE '' END AS Causa15,
				CASE CAUSA650 WHEN 1 THEN '650 Falso negativo' ELSE '' END AS Causa16,
				CASE CAUSA660 WHEN 1 THEN '660 Falso positivo' ELSE '' END AS Causa17,
				CASE CAUSA670 WHEN 1 THEN '670 Resultado falso de la prueba' ELSE '' END AS Causa18,
				CASE CAUSA680 WHEN 1 THEN '680 Falla en el dispositivo implantable' ELSE '' END AS Causa19,
				CASE CAUSA690 WHEN 1 THEN '690 Ambiente inapropiado' ELSE '' END AS Causa20,
				CASE CAUSA700 WHEN 1 THEN '700 Incompatibilidad' ELSE '' END AS Causa21,
				CASE CAUSA710 WHEN 1 THEN '710 Instrucciones para uso y rotulado' ELSE '' END AS Causa22,
				CASE CAUSA720 WHEN 1 THEN '720 Escape/sellado' ELSE '' END AS Causa23,
				CASE CAUSA730 WHEN 1 THEN '730 Mantenimiento' ELSE '' END AS Causa24,
				CASE CAUSA740 WHEN 1 THEN '740 Fabricación' ELSE '' END AS Causa25,
				CASE CAUSA750 WHEN 1 THEN '750 Material' ELSE '' END AS Causa26,
				CASE CAUSA760 WHEN 1 THEN '760 Componentes Mecánicos' ELSE '' END AS Causa27,
				CASE CAUSA770 WHEN 1 THEN '770 Condiciones no higiénicas' ELSE '' END AS Causa28,
				CASE CAUSA780 WHEN 1 THEN '780 No relacionado con el dispositivo' ELSE '' END AS Causa29,
				CASE CAUSA790 WHEN 1 THEN '790 Otros' ELSE '' END AS Causa30,
				CASE CAUSA800 WHEN 1 THEN '800 Empaque' ELSE '' END AS Causa31,
				CASE CAUSA810 WHEN 1 THEN '810 Anatomía/Fisiología del paciente' ELSE '' END AS Causa32,
				CASE CAUSA820 WHEN 1 THEN '820 Condiciones del paciente' ELSE '' END AS Causa33,
				CASE CAUSA830 WHEN 1 THEN '830 Fuente de energía' ELSE '' END AS Causa34,
				CASE CAUSA840 WHEN 1 THEN '840 Medida de protección' ELSE '' END AS Causa35,
				CASE CAUSA850 WHEN 1 THEN '850 Aseguramiento de la calidad en la institución para la atención de salud' ELSE '' END AS Causa36,
				CASE CAUSA860 WHEN 1 THEN '860 Radiación' ELSE '' END AS Causa37,
				CASE CAUSA870 WHEN 1 THEN '870 Software' ELSE '' END AS Causa38,
				CASE CAUSA880 WHEN 1 THEN '880 Esterilización/desinfección/limpieza' ELSE '' END AS Causa39,
				CASE CAUSA890 WHEN 1 THEN '890 Condiciones de almacenamiento' ELSE '' END AS Causa40,
				CASE CAUSA900 WHEN 1 THEN '900 Manipulación, falsificación, sabotaje' ELSE '' END AS Causa41,
				CASE CAUSA910 WHEN 1 THEN '910 Entrenamiento' ELSE '' END AS Causa42,
				CASE CAUSA920 WHEN 1 THEN '920 Trasporte y entrega' ELSE '' END AS Causa43,
				CASE CAUSA930 WHEN 1 THEN '930 Sin definir' ELSE '' END AS Causa44,
				CASE CAUSA940 WHEN 1 THEN '940 Capacidad de uso' ELSE '' END AS Causa45,
				CASE CAUSA950 WHEN 1 THEN '950 Error de uso' ELSE '' END AS Causa46,
				CASE CAUSA960 WHEN 1 THEN '960 Desgaste' ELSE '' END AS Causa47
FROM [dbo].[CALTECNOVIGILANCIA] A
				Inner Join dbo.INUBICACI B ON A.AUUBICACI = B.AUUBICACI
				Inner Join dbo.INMUNICIP P on B.DEPMUNCOD = P.DEPMUNCOD
				Inner Join dbo.INDEPARTA E on P.DEPCODIGO = E.DEPCODIGO 
				Inner Join dbo.CALREPORTE C on A.IDCALREPORTE = C.ID
				Inner Join dbo.INDIAGNOS V on A.CODDIAGNO = V.CODDIAGNO
				leFt Join  dbo.INUNIFUNC X on A.AREAUFUCODIGO = X.UFUCODIGO 
				left join dbo.ADACTIVID z on A.PROFESION = z.codactivi
				leFt Join  dbo.INUNIFUNC y on A.ORGANIZACION = y.UFUCODIGO 
				Left Join dbo.INUBICACI h ON A.UBICACION = h.AUUBICACI
				Left Join dbo.INMUNICIP r on h.DEPMUNCOD = r.DEPMUNCOD
				Left Join dbo.INDEPARTA o on r.DEPCODIGO = o.DEPCODIGO 
where A.IDCALREPORTE = @IdFicha
 
) AS tmp

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recupera el detalle completo de una ficha de tecnovigilancia identificada por su ID, consolidando toda la información necesaria para el reporte oficial de eventos e incidentes adversos relacionados con dispositivos médicos. Compone datos del dispositivo involucrado (nombre genérico, comercial, lote, modelo, serial, registro sanitario, fabricante), datos del paciente (tipo de identificación, sexo, edad calculada, diagnóstico CIE-10), clasificación del evento (adverso serio, adverso no serio, incidente serio, incidente no serio), desenlace, fechas clave (evento, elaboración, reporte, notificación) y acciones correctivas. Cruza con los catálogos de municipios y departamentos para mostrar la ubicación de la institución y del reportante, con unidades funcionales para el área de funcionamiento y la organización reportante, y con el catálogo de actividades para la profesión del reportante. Concatena hasta 47 posibles causas codificadas en una sola cadena de texto para facilitar la presentación en el formulario oficial de tecnovigilancia ante el INVIMA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_CAL_ListarFichaTecnovigilancia';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_CAL_ListarFichaTecnovigilancia';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta y consolida la información completa de una ficha de tecnovigilancia (datos del paciente, dispositivo, evento, reportante y causas) para una ficha específica, generando una concatenación textual de las 47 causas posibles del evento adverso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarFichaTecnovigilancia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en CALTECNOVIGILANCIA cuyo IDCALREPORTE coincida con el identificador recibido; Deben existir las relaciones obligatorias (INNER JOIN) con INUBICACI, INMUNICIP, INDEPARTA, CALREPORTE e INDIAGNOS para la ubicación institucional y diagnóstico del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarFichaTecnovigilancia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna información de una única ficha identificada por IDCALREPORTE; La edad del paciente se calcula con respecto a la fecha actual del sistema mediante [Common].[GETDATE]() y dbo.EDAD; Las fechas (evento, elaboración, reporte, envío, notificación) se formatean como dd/mm/yyyy (formato 103); La ubicación de la institución es obligatoria (INNER JOIN), mientras la ubicación del reportante, área funcional, organización y profesión son opcionales (LEFT JOIN); Las 47 causas de tecnovigilancia se concatenan siempre, aun cuando estén vacías', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarFichaTecnovigilancia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Tecnovigilancia; Dispositivo médico; Evento adverso serio/no serio; Incidente adverso serio/no serio; Desenlace clínico; Diagnóstico (CIE); Paciente; Reportante; Fabricante; Registro sanitario; Lote/Serial/Modelo/Referencia; Nivel de complejidad institucional; Naturaleza jurídica (pública/privada/mixta); Unidad funcional/Área de funcionamiento; Causas de falla en dispositivo médico (códigos 500-960); Acción correctiva', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarFichaTecnovigilancia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.CALTECNOVIGILANCIA: Retorna un único conjunto de resultados con todos los campos de la ficha cuando A.IDCALREPORTE = @IdFicha, incluyendo una columna ''Concatenacion'' que une las 47 marcas de causas (Causa1..Causa47)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarFichaTecnovigilancia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si NIVELCOMPLEJI ∈ {1,2,3} → Traduce a ''Baja'', ''Mediana'' o ''Alta'' respectivamente; si NATURALEZA = 1 / 2 / 3 → Marca con ''X'' la columna Pública / Privada / Mixta según corresponda; si C.TIPOIDENTIFICA ∈ {1..8} → Traduce a tipo documento (Cédula ciudadanía, Cédula extranjera, Tarjeta identidad, Registro civil, Pasaporte, Adulto sin ID, Menor sin ID, NU); si SEXO = 1 / 2 → Marca con ''X'' la columna Masculino o Femenino; si DISPOSITIVOUTILI = ''True''/''False'' → Marca con ''X'' las columnas Si/No de utilización del dispositivo; si DETENCIONANTES/DETENCIONDURANTE/DETENCIONDESPUES = 1 → Marca con ''X'' el momento de detección del evento (Antes, Durante, Después); si CLASIEVENADVESERIO / CLASIEVENADVNOESERIO / CLASIINCIADVESERIO / CLASIINCIADVENOSERIO = 1 → Marca con ''X'' la clasificación correspondiente (evento adverso serio/no serio, incidente adverso serio/no serio); si DESENLACEEVENTO ∈ {1..9} → Traduce a descripción del desenlace (Muerte, Daño de función/estructura, Amenaza vida, Intervención médica/quirúrgica, Incapacidad permanente parcial, Hospitalización/prolongación, Malformación congénita, No hubo daño, Otro); si REPORFABRICANTE = 1 / 0 → Marca con ''X'' columnas ''Reportó Fabricante'' o ''No Reportó Fabricante''; si DISPOMEDICO = 1 / 0 → Marca con ''X'' Si/No es Dispositivo Médico; si ENVIADISPOSITIVO = 1 / 0 → Marca con ''X'' Si/No fue Enviado el dispositivo; si AUTORIZA = 1 / 0 → Marca con ''X'' Si/No autoriza; si CAUSA500..CAUSA960 = 1 → Genera la etiqueta textual de la causa correspondiente (47 causas codificadas según la clasificación de tecnovigilancia); en caso contrario cadena vacía', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarFichaTecnovigilancia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE; dbo.EDAD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarFichaTecnovigilancia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CALTECNOVIGILANCIA; dbo.INUBICACI; dbo.INMUNICIP; dbo.INDEPARTA; dbo.CALREPORTE; dbo.INDIAGNOS; dbo.INUNIFUNC; dbo.ADACTIVID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarFichaTecnovigilancia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarFichaTecnovigilancia';
-- GO
