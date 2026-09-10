CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila101]
    @IdFicha as Int,
	@NumFicha as Int
AS
BEGIN

	SET NOCOUNT ON;

	SELECT 
		RTRIM(AOAVFECHAACCIDENTE) AS 'FECHAACCIDENTE',
		RTRIM(AOAVDIRECCIONACCIDENTE) AS 'DIRECCIONACCIDENTE',
		'' AS 'ACTIVIDADALMOMENTODELACCIDENTE',
		CASE AOAVACTIVIDADALMOMENTODELACCIDENTE WHEN 1 THEN 'X' END AS 'Recreacion',
		CASE AOAVACTIVIDADALMOMENTODELACCIDENTE WHEN 2 THEN 'X' END AS 'Actividad agricola', 
		CASE AOAVACTIVIDADALMOMENTODELACCIDENTE WHEN 3 THEN 'X' END AS 'Oficios domesticos',
		CASE AOAVACTIVIDADALMOMENTODELACCIDENTE WHEN 4 THEN 'X' END AS 'Recoleccion de desechos',
		CASE AOAVACTIVIDADALMOMENTODELACCIDENTE WHEN 5 THEN 'X' END AS 'Actividad acuatica',
		CASE AOAVACTIVIDADALMOMENTODELACCIDENTE WHEN 6 THEN 'X' END AS 'Caminar por senderos abiertos', 
		CASE AOAVACTIVIDADALMOMENTODELACCIDENTE WHEN 7 THEN 'X' END AS 'Otros',
		RTRIM(AOAVCUALOTROARMA) AS 'CUALOTROARMA',
		'' AS 'TIPOATENCIONINICIAL',
		CASE AOAVTIPOATENCIONINICIAL WHEN 1 THEN 'X' END AS 'Incision',
		CASE AOAVTIPOATENCIONINICIAL WHEN 2 THEN 'X' END AS 'Puncion',
		CASE AOAVTIPOATENCIONINICIAL WHEN 3 THEN 'X' END AS 'Sangria',
		CASE AOAVTIPOATENCIONINICIAL WHEN 4 THEN 'X' END AS 'Torniquete',
		CASE AOAVTIPOATENCIONINICIAL WHEN 5 THEN 'X' END AS 'Inmovilización del enfermo',
		CASE AOAVTIPOATENCIONINICIAL WHEN 6 THEN 'X' END AS 'Inmovilización del miembro',
		CASE AOAVTIPOATENCIONINICIAL WHEN 7 THEN 'X' END AS 'Otro',
		CASE AOAVTIPOATENCIONINICIAL WHEN 8 THEN 'X' END AS 'Succion mecanica',
		RTRIM(AOAVCUALOTROAI) AS 'CUALOTROAI',
		'' AS 'LOCALIZACIONAGRESION',
		CASE AOAVLOCALIZACIONAGRESION WHEN 1 THEN 'X' END AS 'Cabeza (cara)', 
		CASE AOAVLOCALIZACIONAGRESION WHEN 2 THEN 'X' END AS 'Miembros superiores', 
		CASE AOAVLOCALIZACIONAGRESION WHEN 3 THEN 'X' END AS  'Miembros inferiores',
		CASE AOAVLOCALIZACIONAGRESION WHEN 4 THEN 'X' END AS 'Torax anterior',
		CASE AOAVLOCALIZACIONAGRESION WHEN 5 THEN 'X' END AS 'Abdomen',
		CASE AOAVLOCALIZACIONAGRESION WHEN 6 THEN 'X' END AS 'Espalda', 
		CASE AOAVLOCALIZACIONAGRESION WHEN 7 THEN 'X' END AS 'Cuello',
		CASE AOAVLOCALIZACIONAGRESION WHEN 8 THEN 'X' END AS 'Genitales',
		CASE AOAVLOCALIZACIONAGRESION WHEN 9 THEN 'X' END AS 'Gluteos',
		CASE AOAVLOCALIZACIONAGRESION WHEN 10 THEN 'X' END AS 'Dedos de pie',
		CASE AOAVLOCALIZACIONAGRESION WHEN 11 THEN 'X' END AS 'Dedos de mano',
		'' AS 'EVIDENCIAHUELLAAGRESION',
		CASE AOAVEVIDENCIAHUELLAAGRESION WHEN 0 THEN 'X' END AS 'No hay evidencia agresion',
		CASE AOAVEVIDENCIAHUELLAAGRESION WHEN 1 THEN 'X' END AS 'Si hay evidencia agresion',
		'' AS 'PERSONAVIOALANIMALAGRESOR',
		CASE AOAVPERSONAVIOALANIMALAGRESOR WHEN 0 THEN 'X' END AS 'No vio al animal',
		CASE AOAVPERSONAVIOALANIMALAGRESOR WHEN 1 THEN 'X' END AS 'Si vio al animal',
		'' AS 'ANIMALCAPTURADO',
		CASE AOAVANIMALCAPTURADO WHEN 0 THEN 'X' END AS 'No fue capturado',
		CASE AOAVANIMALCAPTURADO WHEN 1 THEN 'X' END AS 'Si fue capturado',
		'' AS 'AGENTEAGRESOR',
		CASE AOAVAGENTEAGRESOR WHEN 1 THEN 'X' END AS 'Escorpion',
		CASE AOAVAGENTEAGRESOR WHEN 2 THEN 'X' END AS 'Arana platanera',
		CASE AOAVAGENTEAGRESOR WHEN 3 THEN 'X' END AS 'Arana violin',
		CASE AOAVAGENTEAGRESOR WHEN 4 THEN 'X' END AS 'Arana viuda',
		CASE AOAVAGENTEAGRESOR WHEN 5 THEN 'X' END AS 'Oruga',
		'' AS 'MANIFESTACIONESLOCALES',
		CASE AOAVMANIFESTACIONESLOCALES WHEN 1 THEN 'X' END AS 'Edema',
		CASE AOAVMANIFESTACIONESLOCALES WHEN 2 THEN 'X' END AS 'Dolor',
		CASE AOAVMANIFESTACIONESLOCALES WHEN 3 THEN 'X' END AS 'Flictenas',
		CASE AOAVMANIFESTACIONESLOCALES WHEN 4 THEN 'X' END AS 'Parestesias',
		CASE AOAVMANIFESTACIONESLOCALES WHEN 5 THEN 'X' END AS 'Equimosis',
		'' AS 'MANIFESTACIONESSISTEMICAS',
		CASE AOAVMANIFESTACIONESSISTEMICAS WHEN 1 THEN 'X' END AS 'Nauseas',
		CASE AOAVMANIFESTACIONESSISTEMICAS WHEN 2 THEN 'X' END AS 'Hemorragias',
		CASE AOAVMANIFESTACIONESSISTEMICAS WHEN 3 THEN 'X' END AS 'Hipotension',
		CASE AOAVMANIFESTACIONESSISTEMICAS WHEN 4 THEN 'X' END AS 'Bradicardia',
		CASE AOAVMANIFESTACIONESSISTEMICAS WHEN 5 THEN 'X' END AS 'Dolor abdominal',
		CASE AOAVMANIFESTACIONESSISTEMICAS WHEN 6 THEN 'X' END AS 'Focalizaciones neurologicas',
		'' AS 'COMPLICACIONESLOCALES',
		CASE AOAVCOMPLICACIONESLOCALES WHEN 1 THEN 'X' END AS 'Celulitis',
		CASE AOAVCOMPLICACIONESLOCALES WHEN 2 THEN 'X' END AS 'Absceso',
		CASE AOAVCOMPLICACIONESLOCALES WHEN 3 THEN 'X' END AS 'Necrosis',
		CASE AOAVCOMPLICACIONESLOCALES WHEN 4 THEN 'X' END AS 'Hipoperfusion',
		CASE AOAVCOMPLICACIONESLOCALES WHEN 5 THEN 'X' END AS 'Sindrome compartimental',
		'' AS 'COMPLICACIONESSISTEMICAS',
		CASE AOAVCOMPLICACIONESSISTEMICAS WHEN 1 THEN 'X' END AS 'Hemorragia masiva',
		CASE AOAVCOMPLICACIONESSISTEMICAS WHEN 2 THEN 'X' END AS 'Shock hipovolemico',
		CASE AOAVCOMPLICACIONESSISTEMICAS WHEN 3 THEN 'X' END AS 'Falla renal aguda',
		CASE AOAVCOMPLICACIONESSISTEMICAS WHEN 4 THEN 'X' END AS 'Falla respiratoria aguda',
		CASE AOAVCOMPLICACIONESSISTEMICAS WHEN 5 THEN 'X' END AS 'Coma',
		CASE AOAVCOMPLICACIONESSISTEMICAS WHEN 6 THEN 'X' END AS 'Coagulacion intravascular diseminada',
		'' AS 'GRAVEDADACCIDENTE ',
		CASE AOAVGRAVEDADACCIDENTE WHEN 1 THEN 'X' END AS 'Leve',
		CASE AOAVGRAVEDADACCIDENTE WHEN 2 THEN 'X' END AS 'Moderado',
		CASE AOAVGRAVEDADACCIDENTE WHEN 3 THEN 'X' END AS 'Grave',
		CASE AOAVGRAVEDADACCIDENTE WHEN 4 THEN 'X' END AS 'No envenenamiento',
		'' AS 'EMPLEOSUERO',
		CASE AOAVEMPLEOSUERO WHEN 0 THEN 'X' END AS 'No empleo suero',
		CASE AOAVEMPLEOSUERO WHEN 1 THEN 'X' END AS 'Si empleo suero',
		'' AS 'TIPOSUERO',
		CASE AOAVTIPOSUERO WHEN 1 THEN 'X' END AS 'Antiescorpionico',
		CASE AOAVTIPOSUERO WHEN 2 THEN 'X' END AS 'Antiaracnidico',
		CASE AOAVTIPOSUERO WHEN 3 THEN 'X' END AS 'Antilonomico',
		RTRIM(AOAVREGISTROINVIMA) AS 'REGISTROINVIMA',
		RTRIM(AOAVTIEMPODELACCIDENTEEINICIOTRATAMIENTO) AS 'TIEMPOENTREACCIDENTEEINICIOTRATAMIENTO',
		LEFT(AOAVTIEMPODELACCIDENTEEINICIOTRATAMIENTO, 2) AS 'TIEMPOENTREACCIDENTEEINICIOTRATAMIENTOHORA',
		RIGHT(AOAVTIEMPODELACCIDENTEEINICIOTRATAMIENTO, 2) AS 'TIEMPOENTREACCIDENTEEINICIOTRATAMIENTOMINUTOS',
		'' AS 'REACCIONESAPLICACIONSUERO',
		CASE AOAVREACCIONESAPLICACIONSUERO WHEN 1 THEN 'X' END AS 'Ninguna',
		CASE AOAVREACCIONESAPLICACIONSUERO WHEN 2 THEN 'X' END AS 'Generalizada',
		CASE AOAVREACCIONESAPLICACIONSUERO WHEN 3 THEN 'X' END AS 'Leve',
		RTRIM(AOAVDOSIS) AS 'DOSIS',
		RTRIM(AOAVTIEMPOADMINISTRACIONANTIVENENO) AS 'TIEMPOADMINISTRACIONANTIVENENO',
		LEFT(RTRIM(AOAVTIEMPOADMINISTRACIONANTIVENENO), 2) AS 'TIEMPOADMINISTRACIONANTIVENENOHORA',
		RIGHT(RTRIM(AOAVTIEMPOADMINISTRACIONANTIVENENO), 2) AS 'TIEMPOADMINISTRACIONANTIVENENOMINUTOS',
		'' AS 'REMITIDOAOTRAINSTITUCION',
		CASE AOAVREMITIDOAOTRAINSTITUCION WHEN 0 THEN 'X' END AS 'No es remitido',
		CASE AOAVREMITIDOAOTRAINSTITUCION WHEN 1 THEN 'X' END AS 'Si es remitido',
		VERSION AS 'VERSION',
		JSON AS 'JSON'
	FROM
		HCFICHA101
	Where 
		IDFICHANOTIFICACION  = @IdFicha
END;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera y formatea los datos completos de la ficha SIVIGILA 101 para accidentes ofídicos y por animales venenosos (AOAV), usada en el reporte epidemiológico obligatorio al sistema nacional de vigilancia en salud pública. Recibe el identificador y número de ficha como parámetros, consulta la tabla HCFICHA101 y desglosa cada campo codificado (actividad al momento del accidente, tipo de atención inicial, localización de la agresión, agente agresor, manifestaciones clínicas locales y sistémicas, complicaciones, gravedad, uso y tipo de suero antiveneno, tiempos de tratamiento, entre otros) en columnas legibles con marca ''X'' para facilitar la impresión o exportación del formulario oficial. Es el procedimiento principal para visualizar, imprimir o transmitir la notificación obligatoria de eventos por mordedura de serpiente, picadura de escorpión, araña u oruga en la historia clínica electrónica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila101';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila101';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve los datos de la ficha de notificación Sivigila 101 (accidente ofídico/animal venenoso) decodificando códigos numéricos a marcas ''X'' por categoría para impresión/visualización.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila101';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El identificador de ficha debe corresponder a un registro existente en la tabla de fichas 101; Los campos de tiempo deben estar almacenados con formato de 4 caracteres (HHMM) para permitir el split en hora/minutos vía LEFT/RIGHT', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila101';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reporta una marca ''X'' por grupo de categorías mutuamente excluyentes (la categoría que iguala el código almacenado); Los tiempos almacenados se descomponen siempre como HH=primeros 2 caracteres y MM=últimos 2 caracteres; El parámetro NumFicha se recibe pero no se utiliza en el filtrado; el filtro efectivo es solo por IdFicha', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila101';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha de notificación Sivigila 101; Accidente ofídico / por animal venenoso; Agente agresor (escorpión, arañas, oruga); Atención inicial prehospitalaria; Manifestaciones locales y sistémicas; Complicaciones locales y sistémicas; Gravedad del envenenamiento; Suero antiveneno (antiescorpiónico, antiaracnídico, antilonómico); Registro INVIMA; Remisión a otra institución', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila101';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCFICHA101: Cuando IDFICHANOTIFICACION coincide con el parámetro de ficha, retorna la fila con campos decodificados (CASE WHEN n THEN ''X'') para actividad, atención inicial, localización, agente agresor, manifestaciones, complicaciones, gravedad, suero y remisión.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila101';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si AOAVACTIVIDADALMOMENTODELACCIDENTE = 1..7 → Marca ''X'' en la categoría correspondiente (Recreación, Agrícola, Domésticos, Recolección desechos, Acuática, Senderos, Otros); si AOAVTIPOATENCIONINICIAL = 1..8 → Marca ''X'' en el tipo de atención inicial (Incisión, Punción, Sangría, Torniquete, Inmovilización enfermo/miembro, Otro, Succión mecánica); si AOAVLOCALIZACIONAGRESION = 1..11 → Marca ''X'' en la zona corporal de la agresión; si AOAVEVIDENCIAHUELLAAGRESION = 0/1 → Marca ''No hay evidencia'' o ''Si hay evidencia'' de agresión; si AOAVPERSONAVIOALANIMALAGRESOR = 0/1 → Marca si la persona vio o no al animal agresor; si AOAVANIMALCAPTURADO = 0/1 → Marca si el animal fue capturado o no; si AOAVAGENTEAGRESOR = 1..5 → Marca el agente agresor (Escorpión, Araña platanera/violín/viuda, Oruga); si AOAVGRAVEDADACCIDENTE = 1..4 → Clasifica gravedad como Leve, Moderado, Grave o No envenenamiento; si AOAVEMPLEOSUERO = 0/1 → Indica si se empleó suero antiveneno; si AOAVTIPOSUERO = 1..3 → Marca tipo de suero aplicado (Antiescorpiónico, Antiaracnídico, Antilonómico); si AOAVREACCIONESAPLICACIONSUERO = 1..3 → Marca tipo de reacción a la aplicación del suero (Ninguna, Generalizada, Leve); si AOAVREMITIDOAOTRAINSTITUCION = 0/1 → Indica si el paciente fue remitido a otra institución', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila101';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA101', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila101';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila101';
-- GO
