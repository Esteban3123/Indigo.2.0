--Campo para indicar si se usa el servicio de prueba de MIPRES
--PBI9392
--HECTOR RODRIGUEZ
--11/05/2020
update INEMPRESU set MIPRESTEST = 0 

--Modificar los permisos para Homologacion Mipres
--BUG8396 TAREA9165
--HECTOR RODRIGUEZ
--21/04/2020
UPDATE SEGPERMIF SET IGRICREAR = 0, IGRIMODIF = 0, IGRIELIMI = 1 WHERE INDIDMENU=859 
UPDATE SEGPERMIU SET IGRICREAR = 0, IGRIMODIF = 0 WHERE INDIDMENU = 859

--Actualiza el rango de edad para las patologias de los medicamentos, productos
--PBI7585 TAREA7586
--HECTOR RODRIGUEZ
--24/01/2020
update INPRODPAT set MinimumAge = 0, MaximumAge = 120, AgeMeasure = 1 WHERE AgeMeasure IS NULL

------1958 - HSAP #8639 - Registro de Partograma a nombre del mismo medico
update HCPARTGRA set IDENTIFICADOR = 1 WHERE FECREGIST IS NOT NULL AND  FECREGIST > '01-01-2019' and IDENTIFICADOR IS NULL
update HCPARTGRA set IDENTIFICADOR = 0 WHERE FECREGIST IS NULL AND  FECINIREG  > '01-01-2019' and IDENTIFICADOR IS NULL

------------- 2145 -Campo No Facturable en Nota Administrativa (Agregar el formulario en el modulo de Procesos de HC)
update SEGmenusu set indopcion = 2 where indidmenu = '416'

--Se modifican los datos para completar 4 caracteres
UPDATE [HCMOANULB] SET CODMOTANU='00'+CODMOTANU
GO
UPDATE [HCLABANUL] SET CODMOTANU='00'+CODMOTANU
GO
UPDATE [HCIMGANUL] SET CODMOTANU='00'+CODMOTANU
GO
UPDATE [HCPATANUL] SET CODMOTANU='00'+CODMOTANU
GO
UPDATE [HCALEANUIMG] SET CODMOTANU='00'+CODMOTANU
GO
UPDATE [HCINTEANUL] SET CODMOTANU='00'+CODMOTANU
GO
UPDATE [HCALEANUPAT] SET CODMOTANU='00'+CODMOTANU
GO

--se modifica la descripcion del menu
update SEGpermif set indmendes ='MOTIVOS Y CAUSAS GENERALES' 
WHERE  indidmenu='131'

---- PBI 3922 : Creacion Filtros Cumplimientos de Citas
--- Se actualiza el nombre del Formulario
UPDATE SEGpermif SET indmendes = 'CONFIRMACION DE CITAS' WHERE indidmenu = 232

----PBI _nuevo
--PBI 4062 -- Parametro Dashboard Limpieza/Desinfeccion
update CHPARAMET set DASHLIMPIEZA = 0 WHERE DASHLIMPIEZA IS NULL -- Pasar todos los registros de la tabla a 0 si estan en NULL.

---- PBI 5067 - 5. Tipo "Cancelar solicitud traslados internos"
update SEGpermif set indmendes = 'MOTIVOS Y CAUSAS GENERALES' where indidmenu = '131'

--- actualizando el valor de datos previos en la columna CANCELARWEB --> 0
UPDATE AGCACANQX SET CANCELARWEB = 0 WHERE CANCELARWEB IS NULL

----PBI - 5148 - Agregar Tipos de Consulta WEB a Parámetros
UPDATE AGPARAMET SET WEBMEDGENERAL = 0
UPDATE AGPARAMET SET WEBMEDESPECIALIZADA = 0
UPDATE AGPARAMET SET WEBODONTOLOGIA = 0
UPDATE AGPARAMET SET WEBLABORATORIO = 0
UPDATE AGPARAMET SET WEBIMAGENESDX = 0
UPDATE AGPARAMET SET WEBPROMOPREVEN = 0

----PBI - 5120 - Parámetro mostrar especialidad en agendamiento WEB
UPDATE INESPECIA SET MOSTRARWEB = 0

----PBI - 5284 - Opciones si-no mostrar agenda en agendamiento WEB
UPDATE AGAGEMEDC SET MOSTRARWEB = 0

----PBI - 5292 - Opciones si-no asignar profesional a agendamiento WEB
UPDATE INPROFSAL SET MOSTRARWEB = 0

----PBI - 5747 - Parámetro usar en cancelación de citas WEB
UPDATE AGCAUCANC SET MOSTRARWEB = 0

----PBI - 5905 - Parámetro Agregar a Tipo de consulta Laboratorios y imagenes Dx en citas WEB
UPDATE AGENSALAC SET MOSTRARWEB = 0

----PBI - 5987 - Parámetro mostrar agenda en citas web - apoyo diagnóstico
UPDATE AGDISPONSALA SET MOSTRARWEB = 0

--nueva columna de ordenes de hemocomponetes para determinar o no si se ha enviado a la interfaz tharsis
update HCORHEMCO set INTERFAZ = 0

----PBI - 6437 - Opciones SI/NO del campo obligar agregar CUPS - citas de apoyo Dx-laboratorios
--nueva columna que permite guardar las citas en apoyo Dx - laboratorios con CUPS o sin CUPS
UPDATE AGPARAMET SET OBLICUPSAPODX = 0

---- PBI 6343 : Cambio de Label formulario Dashboard gestión QX por Dashboard procedimientos invasivos
--- Se actualiza el nombre del Formulario
UPDATE SEGpermif SET indmendes = 'DASHBOARD PROCEDIMIENTOS INVASIVOS' WHERE indidmenu = '816'

---- PBI 6192 : ONC - 4. Opción Agregar paquete por Riesgo identificado
--- Nueva columna que permite guardar si desea agregar paquete
update PRHCEXPRES set AGRPAQUETE = 0 where AGRPAQUETE is null

-- BUG_6545: error al guardar favoritos insumos - por rol 
-- Actualizando ROL por valor 0, no nulo y valor 0 por defecto
UPDATE HCFAVORTI SET ROL = 0 WHERE ROL IS NULL

-- PBI 7467: Modificación Imágenes Radioterapia
-- se cambia la descripcion del menú de Imágenes radioterapia a Plantilla Imágenes
UPDATE SEGpermif SET indmendes = 'PLANTILLA IMAGENES' WHERE indidmenu = 943
update HCIMARADIO SET TIPO = 1

  --18/02/2020
  -- BUG 7849
--se modifica la descripcion del menu
update SEGpermif set indmendes ='PAQUETE DE ÓRDENES' 
WHERE  indidmenu='253'

update HCIMARADIO SET TIPO = 1

--24 Abril 2020
--quitar permiso botones  del frm salas
update SEGpermif set ioptdisen =0, ioptnaveg =0, ioptconfi =0,ioptanula = 0,ioptimpri = 0,igricrear = 0,igrielimi = 0,igrimodif = 0 where indidmenu = 310
--quitar permiso botones  del frm salas
update SEGpermir set ioptdisen =0, ioptnaveg =0, ioptconfi =0,ioptanula = 0,ioptimpri = 0,igricrear = 0,igrielimi = 0,igrimodif = 0 where indidmenu = 310
update SEGpermiu set ioptdisen =0, ioptnaveg =0, ioptconfi =0,ioptanula = 0,ioptimpri = 0,igricrear = 0,igrielimi = 0,igrimodif = 0 where indidmenu = 310

--quitar permiso botones  del frm recursos
update SEGpermif set ioptdisen =0, ioptnaveg =0, ioptconfi =0,ioptanula = 0,ioptimpri = 0,igricrear = 0,igrielimi = 0,igrimodif = 0 where indidmenu = 317
--quitar permiso botones  del frm recursos
update SEGpermir set ioptdisen =0, ioptnaveg =0, ioptconfi =0,ioptanula = 0,ioptimpri = 0,igricrear = 0,igrielimi = 0,igrimodif = 0 where indidmenu = 317
update SEGpermiu SET ioptdisen =0, ioptnaveg =0, ioptconfi =0,ioptanula = 0,ioptimpri = 0,igricrear = 0,igrielimi = 0,igrimodif = 0 WHERE indidmenu = 317

--para todos los clientes diferentes a oncologos: definir boegas en 0 y mostrar insumos wen cantidades cero = 1 activo
update [dbo].[HCUNITHIS] set DEFINIRBODEGAS = 0 , MOSTRARINSUCERO = 1 WHERE CODTIPHIS = 'ENF'

--- 15 Septiembre  2020 
--- PBI 10502: ONC  - Identificador HC junta médica - dashboard paciente - consultar historias
--- Script para  actualizar historias antiguas con valor de Junta Medica cuando se cumpla condicion 
-- tabla HCHISPACA  --
--UPDATE HCHISPACA SET  TIPHISPAC ='JM' WHERE JUNTAMEDICA = 1 AND TIPHISPAC ='N'

------------------------------------------------------------------------------------- Sprint 128 -------------------------------------------------------------------------------------------------------
update EHR.SchemesDrugs set DescriptionDays = Days where  DescriptionDays is  null 
update EHR.HCORMEDICAMESQUEMA set DESCRIPCIONDIA = DIA where DESCRIPCIONDIA is null 

------------------------------------------------------------------------------------- Sprint 129 -------------------------------------------------------------------------------------------------------
UPDATE HCRADORDEN SET CODCENATEULTIMO = CODCENATE WHERE CODCENATEULTIMO IS NULL 

UPDATE HCRADESQUEMAS
SET HCRADESQUEMAS.CODCENATEPLANEACION = HCRADORDEN.CODCENATEULTIMO
FROM HCRADESQUEMAS 
INNER JOIN HCRADORDEN
ON HCRADESQUEMAS.IDHCRADORDEN = HCRADORDEN.ID
WHERE HCRADESQUEMAS.CODCENATEPLANEACION IS NULL 

--10841 - ONC - Ajustar registro de administración de quimio (05-10-2020)
UPDATE HCCUMPLITRATAESPECIAL
SET HCCUMPLITRATAESPECIAL.TIPOTRATA = CASE WHEN INCUPSIPS.SERIPSDASH = 5 THEN 1  WHEN INCUPSIPS.SERIPSDASH = 13 THEN 3  WHEN INCUPSIPS.SERIPSDASH = 6 THEN 2 END 
FROM HCCUMPLITRATAESPECIAL
	INNER JOIN HCORDPRON ON HCCUMPLITRATAESPECIAL.IDHCORDPRON = HCORDPRON.AUTO 
	INNER JOIN INCUPSIPS ON HCORDPRON.CODSERIPS = INCUPSIPS.CODSERIPS
WHERE HCCUMPLITRATAESPECIAL.TIPOTRATA IS NULL 

UPDATE EHR.HCORDCICLOSD SET HCORDCICLOSD.FECHAPROGRAMACION = (SELECT TOP 1 HCHOJAMED.FECPROAPL FROM HCHOJAMED WHERE HCHOJAMED.IDHCORDCICLOSD = HCORDCICLOSD.ID  ORDER BY HCHOJAMED.FECAPLMED ASC) WHERE HCORDCICLOSD.FECHAPROGRAMACION IS NULL 
UPDATE EHR.HCORDCICLOSD SET HCORDCICLOSD.FECHAADMINISTRACION = (SELECT TOP 1 HCHOJAMED.FECAPLMED FROM HCHOJAMED WHERE HCHOJAMED.IDHCORDCICLOSD = HCORDCICLOSD.ID  ORDER BY HCHOJAMED.FECAPLMED ASC) WHERE HCORDCICLOSD.FECHAADMINISTRACION IS NULL 
UPDATE EHR.HCORDCICLOSD SET HCORDCICLOSD.USUARIOADMINISTRACION = (SELECT TOP 1 HCHOJAMED.CODPROAPL FROM HCHOJAMED WHERE HCHOJAMED.IDHCORDCICLOSD = HCORDCICLOSD.ID  ORDER BY HCHOJAMED.FECAPLMED ASC) WHERE HCORDCICLOSD.USUARIOADMINISTRACION IS NULL 

--10843 - ONC - Record de anestesia que permita guardar temporalmente, crear folio y finalizar y confirmar
UPDATE HCREGANES SET FECHAREGISTRO = HORINIANES WHERE FECHAREGISTRO IS NULL 
UPDATE HCREGANES SET TIPOREGISTRO = 3  WHERE TIPOREGISTRO IS NULL 
UPDATE  HCANECATE SET CODCATEGO=RIGHT('0' + LTRIM(RTRIM(CODCATEGO)),2)
UPDATE  HCANESUBC Set CODCATEGO=RIGHT('0' + Ltrim(Rtrim(CODCATEGO)),2), CODSUBCAT=RIGHT('0' + Ltrim(Rtrim(CODSUBCAT)),2)

------------------------------------------------------------------------------------- Sprint 130 -------------------------------------------------------------------------------------------------------
--10907 - 1. ONC - Parámetro "Precargar diagnóstico de cáncer en HC"
update HCPARACA SET PRECARGARDIAGNOSTICOSCANCERHC = 2 WHERE PRECARGARDIAGNOSTICOSCANCERHC IS NULL 

--- Corrigiendo error en Fichas de Notificación, para Tipo de documento PE :Permiso especial de Permanencia (Aplica para extranjeros)
UPDATE HCFICHANOTIFICACION  SET TIPODOCUMENTO = 8 WHERE TIPODOCUMENTO IS NULL

------------------------------------------------------------------------------------- Sprint 135 -------------------------------------------------------------------------------------------------------
--- 30 Diciembre 2020
--- Bug 11490: #12809-ODO- No permite egresar paciente con orden de procedimiento Quirúrgico
--- Corrigiendo estado de Cancelacion o Anulacion en Procedimientos QX
 UPDATE HCORDPROQ SET ESTSERIPS = '3' WHERE ESTSERIPS = '5' 

------------------------------------------------------------------------------------- Sprint 141 -------------------------------------------------------------------------------------------------------
--- Actualizando campo de MANEXTRPRO para HCPRODUCTOS CONTROL cuando es Extramural
 UPDATE HCPRODUCTOSCONTROL SET MANEXTPRO = 1 
 WHERE ID IN ( SELECT  E.ID  FROM HCPRESCRD A WITH (NOLOCK)
INNER JOIN HCPRODUCTOSCONTROL E WITH (NOLOCK) 
ON E.CODPRODUC = A.CODPRODUC AND E.NUMEFOLIO = A.NUMEFOLIO AND E.NUMINGRES = A.NUMINGRES AND E.IPCODPACI = A.IPCODPACI AND E.CANTIDAD = A.CANPEDPRO AND A.DURACIDOS = E.DURACIDOS
WHERE  (A.MANEXTPRO <> E.MANEXTPRO ) AND A.MANEXTPRO = 1 )

------------------------------------------------------------------------------------- Sprint 142 -------------------------------------------------------------------------------------------------------
UPDATE HCHOJMEZC SET FECPROGRAMACIONAPL = FECAPLMED WHERE FECPROGRAMACIONAPL IS NULL

------------------------------------------------------------------------------------- Sprint 143 -------------------------------------------------------------------------------------------------------
UPDATE HCESCDOWN SET TIPOESCALA = 47 WHERE TIPOESCALA IS NULL

------------------------------------------------------------------------------------- Sprint 148 -------------------------------------------------------------------------------------------------------
----- Actualizando ubicaciones del ODONTOGRAMA ---------
---- 1-) Actualizando los campos de la zona derecha del reporte
-- NOTA : Para poder intercambiar tipo = 5 y tipo = 8 , se usa el tipo = 11 como variable temporal
---  Primer y Segundo Script , donde pasamos de tipo 8 a 11  (11=es variable temporal)
UPDATE ODONTODIENTEDIAG SET TIPO = 11 WHERE ID IN 
(
	SELECT C.ID
	FROM ODONTOCONTROL AS A
	INNER JOIN ODONTODIENTE AS B ON A.ID = B.IDODONTOCONTROL
	INNER JOIN ODONTODIENTEDIAG AS C ON B.ID = C.IDODONTODIENTE
	WHERE C.TIPO = 8 AND B.DIENTE IN (21,22,23,24,25,26,27,28, 61,62,63,64,65, 71,72,73,74,75, 31,32,33,34,35,36,37,38)
)
UPDATE ODONTODIENTETRATA SET TIPO = 11 WHERE ID IN 
(
	SELECT C.ID
	FROM ODONTOCONTROL AS A
	INNER JOIN ODONTODIENTE AS B ON A.ID = B.IDODONTOCONTROL
	INNER JOIN ODONTODIENTETRATA AS C ON B.ID = C.IDODONTODIENTE
	WHERE C.TIPO = 8 AND B.DIENTE  IN (21,22,23,24,25,26,27,28, 61,62,63,64,65, 71,72,73,74,75, 31,32,33,34,35,36,37,38)
)
--- Tercer y Cuarto Script, pasamos de tipo 5 a 8
 UPDATE ODONTODIENTEDIAG SET TIPO = 8 WHERE ID IN 
(
	SELECT C.ID
	FROM ODONTOCONTROL AS A
	INNER JOIN ODONTODIENTE AS B ON A.ID = B.IDODONTOCONTROL
	INNER JOIN ODONTODIENTEDIAG AS C ON B.ID = C.IDODONTODIENTE
	WHERE C.TIPO = 5 AND B.DIENTE  IN (21,22,23,24,25,26,27,28, 61,62,63,64,65, 71,72,73,74,75, 31,32,33,34,35,36,37,38)
)
UPDATE ODONTODIENTETRATA SET TIPO = 8 WHERE ID IN 
(
	SELECT C.ID
	FROM ODONTOCONTROL AS A
	INNER JOIN ODONTODIENTE AS B ON A.ID = B.IDODONTOCONTROL
	INNER JOIN ODONTODIENTETRATA AS C ON B.ID = C.IDODONTODIENTE
	WHERE C.TIPO = 5 AND B.DIENTE  IN (21,22,23,24,25,26,27,28, 61,62,63,64,65, 71,72,73,74,75, 31,32,33,34,35,36,37,38)
)
--- Quinto y Sexto Script, pasamos de variable temporal 11 a 5 
UPDATE ODONTODIENTEDIAG SET TIPO = 5 WHERE ID IN 
(
	SELECT C.ID
	FROM ODONTOCONTROL AS A
	INNER JOIN ODONTODIENTE AS B ON A.ID = B.IDODONTOCONTROL
	INNER JOIN ODONTODIENTEDIAG AS C ON B.ID = C.IDODONTODIENTE
	WHERE C.TIPO = 11 AND B.DIENTE IN (21,22,23,24,25,26,27,28, 61,62,63,64,65, 71,72,73,74,75, 31,32,33,34,35,36,37,38)
)
 UPDATE ODONTODIENTETRATA SET TIPO = 5 WHERE ID IN 
(
	SELECT C.ID
	FROM ODONTOCONTROL AS A
	INNER JOIN ODONTODIENTE AS B ON A.ID = B.IDODONTOCONTROL
	INNER JOIN ODONTODIENTETRATA AS C ON B.ID = C.IDODONTODIENTE
	WHERE C.TIPO = 11 AND B.DIENTE  IN (21,22,23,24,25,26,27,28, 61,62,63,64,65, 71,72,73,74,75, 31,32,33,34,35,36,37,38)
)
---- 2-) Actualizando los campos de los dientes inferiores
--- Septimo y Octavo Script, pasando de tipo 2 a 10 --> Palatino Arriba del diente inferior
 UPDATE ODONTODIENTEDIAG SET TIPO = 10 WHERE ID IN 
(
	SELECT C.ID
	FROM ODONTOCONTROL AS A
	INNER JOIN ODONTODIENTE AS B ON A.ID = B.IDODONTOCONTROL
	INNER JOIN ODONTODIENTEDIAG AS C ON B.ID = C.IDODONTODIENTE
	WHERE C.TIPO = 2 AND B.DIENTE IN (81,82,83,84,85, 71,72,73,74,75, 41,42,43,44,45,46,47,48, 31,32,33,34,35,36,37,38)
)
 UPDATE ODONTODIENTETRATA SET TIPO = 10 WHERE ID IN 
(
	SELECT C.ID
	FROM ODONTOCONTROL AS A
	INNER JOIN ODONTODIENTE AS B ON A.ID = B.IDODONTOCONTROL
	INNER JOIN ODONTODIENTETRATA AS C ON B.ID = C.IDODONTODIENTE
	WHERE C.TIPO = 2 AND B.DIENTE IN (81,82,83,84,85, 71,72,73,74,75, 41,42,43,44,45,46,47,48, 31,32,33,34,35,36,37,38)
)
--- Noveno y Decimo Script, pasando de tipo 4 a 9 --> Vestibular Abajo  del diente inferior
 UPDATE ODONTODIENTEDIAG SET TIPO = 9 WHERE ID IN 
(
	SELECT C.ID
	FROM ODONTOCONTROL AS A
	INNER JOIN ODONTODIENTE AS B ON A.ID = B.IDODONTOCONTROL
	INNER JOIN ODONTODIENTEDIAG AS C ON B.ID = C.IDODONTODIENTE
	WHERE C.TIPO = 4 AND B.DIENTE IN (81,82,83,84,85, 71,72,73,74,75, 41,42,43,44,45,46,47,48, 31,32,33,34,35,36,37,38)
)
 UPDATE ODONTODIENTETRATA SET TIPO = 9 WHERE ID IN 
(
	SELECT C.ID
	FROM ODONTOCONTROL AS A
	INNER JOIN ODONTODIENTE AS B ON A.ID = B.IDODONTOCONTROL
	INNER JOIN ODONTODIENTETRATA AS C ON B.ID = C.IDODONTODIENTE
	WHERE C.TIPO = 4 AND B.DIENTE IN (81,82,83,84,85, 71,72,73,74,75, 41,42,43,44,45,46,47,48, 31,32,33,34,35,36,37,38)
)
--UPDATE HCMEDRIES
--SET TIPOREGISTRO = 2
--WHERE ALERGICO = 0
--UPDATE HCMEDRIES
--SET TIPOREGISTRO = 1
--WHERE ALERGICO = 1

--------------------------------------------------------------------------------------- Sprint 149 -------------------------------------------------------------------------------------------------------
----- PBI 12991: Refactoring antecedentes farmacológicos
-------- Actualizo todos los Medicamentos Farmacologicos como no alergicos a Activo -----
--UPDATE HCMEDRIES
--SET ALERGICO = 1
--WHERE ALERGICO = 0 AND TIPOREGISTRO = 2;

--- Bug 13053: COH - HLI - Reporte de odontograma no concuerda con el registro realizado
--- Tarea 13242 - Tercera Revisión
----- Actualizando ubicaciones del ODONTOGRAMA ---------
---- 3-) Actualizando los campos de los dientes inferiores
--- Se realizara intercambio de palatinos
--- DIENTES INFERIORES ---
--- Primer y Segundo Script, pasando de tipo 4 a 12 --> de 4 a variable temporal 12
 UPDATE ODONTODIENTEDIAG SET TIPO = 12 WHERE ID IN 
(
	SELECT C.ID
	FROM ODONTOCONTROL AS A
	INNER JOIN ODONTODIENTE AS B ON A.ID = B.IDODONTOCONTROL
	INNER JOIN ODONTODIENTEDIAG AS C ON B.ID = C.IDODONTODIENTE
	WHERE C.TIPO = 4 AND B.DIENTE IN (81,82,83,84,85, 71,72,73,74,75, 41,42,43,44,45,46,47,48, 31,32,33,34,35,36,37,38)
)
 UPDATE ODONTODIENTETRATA SET TIPO = 12 WHERE ID IN 
(
	SELECT C.ID
	FROM ODONTOCONTROL AS A
	INNER JOIN ODONTODIENTE AS B ON A.ID = B.IDODONTOCONTROL
	INNER JOIN ODONTODIENTETRATA AS C ON B.ID = C.IDODONTODIENTE
	WHERE C.TIPO = 4 AND B.DIENTE IN (81,82,83,84,85, 71,72,73,74,75, 41,42,43,44,45,46,47,48, 31,32,33,34,35,36,37,38)
)
--- Tercer y Cuarto Script, pasando de tipo 10 a 4 --> Intercambio de Palatinos directo
 UPDATE ODONTODIENTEDIAG SET TIPO = 4 WHERE ID IN 
(
	SELECT C.ID
	FROM ODONTOCONTROL AS A
	INNER JOIN ODONTODIENTE AS B ON A.ID = B.IDODONTOCONTROL
	INNER JOIN ODONTODIENTEDIAG AS C ON B.ID = C.IDODONTODIENTE
	WHERE C.TIPO = 10 AND B.DIENTE IN (81,82,83,84,85, 71,72,73,74,75, 41,42,43,44,45,46,47,48, 31,32,33,34,35,36,37,38)
)
 UPDATE ODONTODIENTETRATA SET TIPO = 4 WHERE ID IN 
(
	SELECT C.ID
	FROM ODONTOCONTROL AS A
	INNER JOIN ODONTODIENTE AS B ON A.ID = B.IDODONTOCONTROL
	INNER JOIN ODONTODIENTETRATA AS C ON B.ID = C.IDODONTODIENTE
	WHERE C.TIPO = 10 AND B.DIENTE IN (81,82,83,84,85, 71,72,73,74,75, 41,42,43,44,45,46,47,48, 31,32,33,34,35,36,37,38)
)
-- Quinto y Sexto cript, pasando de tipo 12 a 10 --> Intercambio de Palatinos con variable temporal
 UPDATE ODONTODIENTEDIAG SET TIPO = 10 WHERE ID IN 
(
	SELECT C.ID
	FROM ODONTOCONTROL AS A
	INNER JOIN ODONTODIENTE AS B ON A.ID = B.IDODONTOCONTROL
	INNER JOIN ODONTODIENTEDIAG AS C ON B.ID = C.IDODONTODIENTE
	WHERE C.TIPO = 12 AND B.DIENTE IN (81,82,83,84,85, 71,72,73,74,75, 41,42,43,44,45,46,47,48, 31,32,33,34,35,36,37,38)
)
 UPDATE ODONTODIENTETRATA SET TIPO = 10 WHERE ID IN 
(
	SELECT C.ID
	FROM ODONTOCONTROL AS A
	INNER JOIN ODONTODIENTE AS B ON A.ID = B.IDODONTOCONTROL
	INNER JOIN ODONTODIENTETRATA AS C ON B.ID = C.IDODONTODIENTE
	WHERE C.TIPO = 12 AND B.DIENTE IN (81,82,83,84,85, 71,72,73,74,75, 41,42,43,44,45,46,47,48, 31,32,33,34,35,36,37,38)
)
---  DIENTES SUPERIORES ---
--- Septimo y Octavo Script, pasando de tipo 4 a 13 --> de 4 a variable temporal 13
 UPDATE ODONTODIENTEDIAG SET TIPO = 13 WHERE ID IN 
(
	SELECT C.ID
	FROM ODONTOCONTROL AS A
	INNER JOIN ODONTODIENTE AS B ON A.ID = B.IDODONTOCONTROL
	INNER JOIN ODONTODIENTEDIAG AS C ON B.ID = C.IDODONTODIENTE
	WHERE C.TIPO = 4 AND B.DIENTE IN (11,12,13,14,15,16,17,18,  51,52,53,54,55,  21,22,23,24,25,26,27,28, 61,62,63,64,65 )
)
 UPDATE ODONTODIENTETRATA SET TIPO = 13 WHERE ID IN 
(
	SELECT C.ID
	FROM ODONTOCONTROL AS A
	INNER JOIN ODONTODIENTE AS B ON A.ID = B.IDODONTOCONTROL
	INNER JOIN ODONTODIENTETRATA AS C ON B.ID = C.IDODONTODIENTE
	WHERE C.TIPO = 4 AND B.DIENTE IN (11,12,13,14,15,16,17,18,  51,52,53,54,55,  21,22,23,24,25,26,27,28, 61,62,63,64,65 )
)
--- Decimo y Undecimo Script, pasando de tipo 10 a 4 --> Intercambio de Palatinos directo
 UPDATE ODONTODIENTEDIAG SET TIPO = 4 WHERE ID IN 
(
	SELECT C.ID
	FROM ODONTOCONTROL AS A
	INNER JOIN ODONTODIENTE AS B ON A.ID = B.IDODONTOCONTROL
	INNER JOIN ODONTODIENTEDIAG AS C ON B.ID = C.IDODONTODIENTE
	WHERE C.TIPO = 10 AND B.DIENTE IN (11,12,13,14,15,16,17,18,  51,52,53,54,55,  21,22,23,24,25,26,27,28, 61,62,63,64,65 )
)
 UPDATE ODONTODIENTETRATA SET TIPO = 4 WHERE ID IN 
(
	SELECT C.ID
	FROM ODONTOCONTROL AS A
	INNER JOIN ODONTODIENTE AS B ON A.ID = B.IDODONTOCONTROL
	INNER JOIN ODONTODIENTETRATA AS C ON B.ID = C.IDODONTODIENTE
	WHERE C.TIPO = 10 AND B.DIENTE IN (11,12,13,14,15,16,17,18,  51,52,53,54,55,  21,22,23,24,25,26,27,28, 61,62,63,64,65 )
)
-- Doceavo y Treceavo script, pasando de tipo 13 a 10 --> Intercambio de Palatinos con variable temporal
 UPDATE ODONTODIENTEDIAG SET TIPO = 10 WHERE ID IN 
(
	SELECT C.ID
	FROM ODONTOCONTROL AS A
	INNER JOIN ODONTODIENTE AS B ON A.ID = B.IDODONTOCONTROL
	INNER JOIN ODONTODIENTEDIAG AS C ON B.ID = C.IDODONTODIENTE
	WHERE C.TIPO = 13 AND B.DIENTE IN (11,12,13,14,15,16,17,18,  51,52,53,54,55,  21,22,23,24,25,26,27,28, 61,62,63,64,65 )
)
 UPDATE ODONTODIENTETRATA SET TIPO = 10 WHERE ID IN 
(
	SELECT C.ID
	FROM ODONTOCONTROL AS A
	INNER JOIN ODONTODIENTE AS B ON A.ID = B.IDODONTOCONTROL
	INNER JOIN ODONTODIENTETRATA AS C ON B.ID = C.IDODONTODIENTE
	WHERE C.TIPO = 13 AND B.DIENTE IN (11,12,13,14,15,16,17,18,  51,52,53,54,55,  21,22,23,24,25,26,27,28, 61,62,63,64,65 )
)

------------------------------------------------------------------------------------- Sprint 16 -------------------------------------------------------------------------------------------------------
--- Actualizando descripción del Evento
UPDATE HCFICHANOTIFICACION 
SET NOMBEVENTO = 'Enfermedades Huérfanas - Raras'
WHERE CODEVENTO = '342'
--- Actualizando descripción del Evento
UPDATE HCFICHANOTIFICACION 
SET NOMBEVENTO = 'Infección Asociada a Dispositivos en Unidades de Cuidados Intensivos - IAD en UCI'
WHERE CODEVENTO = '357'

------------------------------------------------------------------------------------- Sprint 1 -------------------------------------------------------------------------------------------------------
--- Se parametrizan horas por defecto para la solicitud de dietas
UPDATE CHPARAMET SET BreakfastStartTime='00:00:00' WHERE BreakfastStartTime is NULL
UPDATE CHPARAMET SET BreakfastEndTime='23:59:00' WHERE BreakfastEndTime is NULL
UPDATE CHPARAMET SET LunchStartTime='00:00:00' WHERE LunchStartTime is NULL
UPDATE CHPARAMET SET LunchEndTime='23:59:00' WHERE LunchEndTime is NULL
UPDATE CHPARAMET SET DinnerStartTime='00:00:00' WHERE DinnerStartTime is NULL
UPDATE CHPARAMET SET DinnerEndTime='23:59:00' WHERE DinnerEndTime is NULL
UPDATE CHPARAMET SET MorningSupplementStartTime='00:00:00' WHERE MorningSupplementStartTime is NULL
UPDATE CHPARAMET SET MorningSupplementEndTime='23:59:00' WHERE MorningSupplementEndTime is NULL
UPDATE CHPARAMET SET AfternoonSupplementStartTime='00:00:00' WHERE AfternoonSupplementStartTime is NULL
UPDATE CHPARAMET SET AfternoonSupplementEndTime='23:59:00' WHERE AfternoonSupplementEndTime is NULL
UPDATE CHPARAMET SET OtherMealStartTime='00:00:00' WHERE OtherMealStartTime is NULL
UPDATE CHPARAMET SET OtherMealEndTime='23:59:00' WHERE OtherMealEndTime is NULL

------------------------------------------------------------------------------------- Sprint 3 -------------------------------------------------------------------------------------------------------
--- 23 de Marzo de 2020
--Product Backlog Item 3246: 1. Maestro "Tipos gases para inhalación"
UPDATE SEGpermif SET indmendes = 'TIPOS GASES PARA INHALACIÓN' WHERE indidmenu = 210

------------------------------------------------------------------------------------- Sprint 4 -------------------------------------------------------------------------------------------------------
--- 08 de Abril de 2022
--Product Backlog Item 3664: 4. Modificación al parámetro de validación de tratamiento por médico cada 24 horas
UPDATE HCPARACA SET PharmacologicalTreatment = '2' WHERE PharmacologicalTreatment is NULL

------------------------------------------------------------------------------------- Sprint 7 -------------------------------------------------------------------------------------------------------
-- 13 de mayo de 2022
--- PBI HOMI: Reformulación de medicamento
UPDATE HCPARACA SET AllowDrugReformulation = '0' WHERE AllowDrugReformulation IS NULL

------------------------------------------------------------------------------------- Sprint 24 -------------------------------------------------------------------------------------------------------


------------------------------------------------------------------------------------- Sprint Week 6'7 2023 -------------------------------------------------------------------------------------------------------
--PBI 7392: 3. Refactoring formulario "Unidades funcionales" - campo "Grupo"
update INUNIFUNC set FunctionalUnitGroup = 1 where UFUTIPUNI = 15
update INUNIFUNC set FunctionalUnitGroup = 1 where UFUTIPUNI = 24
update INUNIFUNC set FunctionalUnitGroup = 1 where UFUTIPUNI = 31
update INUNIFUNC set FunctionalUnitGroup = 1 where UFUTIPUNI = 32
update INUNIFUNC set FunctionalUnitGroup = 2 where UFUTIPUNI = 3
update INUNIFUNC set FunctionalUnitGroup = 2 where UFUTIPUNI = 4
update INUNIFUNC set FunctionalUnitGroup = 2 where UFUTIPUNI = 12
update INUNIFUNC set FunctionalUnitGroup = 2 where UFUTIPUNI = 13
update INUNIFUNC set FunctionalUnitGroup = 2 where UFUTIPUNI = 14
update INUNIFUNC set FunctionalUnitGroup = 2 where UFUTIPUNI = 20
update INUNIFUNC set FunctionalUnitGroup = 2 where UFUTIPUNI = 33
update INUNIFUNC set FunctionalUnitGroup = 2 where UFUTIPUNI = 34
update INUNIFUNC set FunctionalUnitGroup = 2 where UFUTIPUNI = 35
update INUNIFUNC set FunctionalUnitGroup = 3 where UFUTIPUNI = 2
update INUNIFUNC set FunctionalUnitGroup = 3 where UFUTIPUNI = 5
update INUNIFUNC set FunctionalUnitGroup = 3 where UFUTIPUNI = 6
update INUNIFUNC set FunctionalUnitGroup = 3 where UFUTIPUNI = 7
update INUNIFUNC set FunctionalUnitGroup = 3 where UFUTIPUNI = 8
update INUNIFUNC set FunctionalUnitGroup = 3 where UFUTIPUNI = 9
update INUNIFUNC set FunctionalUnitGroup = 3 where UFUTIPUNI = 10
update INUNIFUNC set FunctionalUnitGroup = 3 where UFUTIPUNI = 11
update INUNIFUNC set FunctionalUnitGroup = 3 where UFUTIPUNI = 16
update INUNIFUNC set FunctionalUnitGroup = 3 where UFUTIPUNI = 17
update INUNIFUNC set FunctionalUnitGroup = 3 where UFUTIPUNI = 18
update INUNIFUNC set FunctionalUnitGroup = 3 where UFUTIPUNI = 21
update INUNIFUNC set FunctionalUnitGroup = 4 where UFUTIPUNI = 19
update INUNIFUNC set FunctionalUnitGroup = 4 where UFUTIPUNI = 22
update INUNIFUNC set FunctionalUnitGroup = 4 where UFUTIPUNI = 23
update INUNIFUNC set FunctionalUnitGroup = 5 where UFUTIPUNI = 1

------------------------------------------------------------------------------------- Sprint Week 12'13 2023 -------------------------------------------------------------------------------------------------------
UPDATE ParamEducationFormatsC SET Type = 1 --PBI 8433  1. Refactoring maestro Creación encuestas 

------------------------------------------------------------------------------------- Sprint Week 18'19 2023 -------------------------------------------------------------------------------------------------------
update PatientEducationFormatsC SET FormatType = 1 WHERE FormatType  IS NULL --BUG-9768 Error en consultar imprimir en formato educativo encuesta.		

------------------------------------------------------------------------------------- Sprint Week 20 - 21 (2023) -------------------------------------------------------------------------------------------------------
-- PBI 9128 - San José-EHR-. Ajuste al formulario Paquetes quirúrgicos
UPDATE AGPAQUETES
SET ParameterizationCostCenter = 0
WHERE ParameterizationCostCenter IS NULL

--PBI 9987 1. Modificaciones al formulario profesionales de la salud para permitir asistencia de interconsultas
UPDATE INPROFSAL set PerformAssistedInterconsultation = 0 where PerformAssistedInterconsultation is null

--PBI 11069 Crear sección de búsqueda en formulario FURIPS PARTE 2
UPDATE ADFURIPSU SET NUMCONREC = 'Sin registrar' WHERE NUMCONREC = ''

------------------------------------------------------------------------------------- Sprint Week 34 - 35 2023 -------------------------------------------------------------------------------------------------------
--PBI 11716 5. Homologación código de reporte "Causas de atención" con "Código RIPS"
----------------Se actualiza campo RIpsCode para las causas de atención creadas y activas actualmente
----------------(Basado en Tabla SISPRO R03 RIPS causa externa versión 2 (Documento anexo al pbi))
UPDATE causesofattention
SET
    RIPSCode = CASE
        WHEN Code = 12 THEN '21'
        WHEN Code = 13 THEN '22'
		WHEN Code = 14 THEN '23'
        WHEN Code = 15 THEN '24'
		WHEN Code = 16 THEN '25'
        WHEN Code = 17 THEN '26'
		WHEN Code = 18 THEN '27'
        WHEN Code = 19 THEN '28'
		WHEN Code = 20 THEN '29'
		WHEN Code = 21 THEN '30'
		WHEN Code = 22 THEN '31'
        WHEN Code = 23 THEN '32'
		WHEN Code = 24 THEN '33'
        WHEN Code = 25 THEN '34'
		WHEN Code = 26 THEN '35'
        WHEN Code = 27 THEN '36'
		WHEN Code = 28 THEN '37'
        WHEN Code = 29 THEN '38'
		WHEN Code = 30 THEN '39'
        WHEN Code = 31 THEN '40'
        WHEN Code = 32 THEN '41'
        WHEN Code = 33 THEN '42'
		WHEN Code = 34 THEN '43'
		WHEN Code = 35 THEN '44'
		WHEN Code = 36 THEN '45'
		WHEN Code = 37 THEN '46'
		WHEN Code = 38 THEN '47'
		WHEN Code = 39 THEN '48'
		WHEN Code = 40 THEN '49'
    END
WHERE Code IN (12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40)

---https://dev.azure.com/IndigoVie/EHR/_workitems/edit/10650/																								  
UPDATE INUNIFUNC set ApplyOtherProcedures = 1 where UFUTIPUNI = '3' 

---https://dev.azure.com/IndigoVie/EHR/_workitems/edit/10652
UPDATE RISGRIMAGE SET DefineRoomIntrahospitalario = 0 WHERE DefineRoomIntrahospitalario IS NULL 

--PBI 11647  4.1. Refactoring "Ingresos" - campo "Modalidades de atención"
UPDATE ADINGRESO SET IdAdmissionModalities = 1 where IdAdmissionModalities IS NULL

----PBI 10990 HOMI- 5.  Crear ajustes al reporte FURIPS 
----- SE ACTUALIZA CAMPOS VACIO EN NULOS EN EL TIPO DE IDENTIFICACION 
UPDATE ADFURIPSU SET TIPDOCCON=NULL WHERE TIPDOCCON=' '

--Actualizar columnas nuevas informe quirúrgico e incapacidades.
update HCINCAPAC set DisabilityClass = 2 where DisabilityClass IS NULL 
update HCQXINFOR set PerioperativeBleeding = 2 where PerioperativeBleeding IS NULL 
update HCQXINFOR set MaterialCount = 2 where MaterialCount IS NULL 
update HCQXINFOR set Compresses = 2 where Compresses IS NULL 
update HCQXINFOR set Gauze  = 2 where Gauze  IS NULL 
update HCQXINFOR set PREPATOLO  = 2 where PREPATOLO  IS NULL 

------------------------------------------------------------------------------------- Sprint Week 36'37 2023 -------------------------------------------------------------------------------------------------------
--PBI-11795 1. Modificaciones e inclusión de campos faltantes formulario de incapacidades
update HCINCAPAC set RetroactiveDisability = 0 where RetroactiveDisability is null

--BUG 12843 - HSJ - No se está mostrando la posología (administración del medicamento de control en el reporte Prescripción medicamentos de control)
--01/10/2023
update HCPRODUCTOSCONTROL set ORIGENPRODUCTO = 1 where ORIGENPRODUCTO IS NULL

------------------------------------------------------------------------------------- Sprint Week 46'47 2023 -------------------------------------------------------------------------------------------------------
--13686	--PBI	--BANDERA PARA MEDICAMENTOS ONCOLOGICOS EN CENTRAL DE MEZCLAS																					  
--15/11/2023
update HCPARACA set AllowConfirmOncologialSchemesCentralMixes = 0 where AllowConfirmOncologialSchemesCentralMixes is NULL

------------------------------------------------------------------------------------- Sprint Week 48'49 2023 -------------------------------------------------------------------------------------------------------
--PBI 13802 Interno- 2. Crear función al campo especialidad en reportes de HC
--05/12/2023
--Se cargan los datos de la primera especialidad de los profesionales de la salud en la nueva columna CODESPECI de las siguientes tablas:
UPDATE HCCTRNOTE SET CODESPECI = B.CODESPEC1 FROM HCCTRNOTE A INNER JOIN INPROFSAL B ON A.CODPROSAL = B.CODPROSAL where A.CODESPECI is null;
UPDATE HCCTRNOTT SET CODESPECI = B.CODESPEC1 FROM HCCTRNOTT A INNER JOIN INPROFSAL B ON A.CODPROSAL = B.CODPROSAL where A.CODESPECI is null;
UPDATE HCPROCTER SET CODESPECI = B.CODESPEC1 FROM HCPROCTER A INNER JOIN INPROFSAL B ON A.CODPROSAL = B.CODPROSAL where A.CODESPECI is null;
UPDATE HCQXINFOR SET CODESPECI = B.CODESPEC1 FROM HCQXINFOR A INNER JOIN INPROFSAL B ON A.CODPROSAL = B.CODPROSAL where A.CODESPECI is null;
UPDATE HCATINPAR SET CODESPECI = B.CODESPEC1 FROM HCATINPAR A INNER JOIN INPROFSAL B ON A.CODPROSAL = B.CODPROSAL where A.CODESPECI is null;
UPDATE HCRECINAC SET CODESPECI = B.CODESPEC1 FROM HCRECINAC A INNER JOIN INPROFSAL B ON A.CODPROSAL = B.CODPROSAL where A.CODESPECI is null;

------------------------------------------------------------------------------------- Sprint Week 50 51 2023 -------------------------------------------------------------------------------------------------------
UPDATE HCESCALAS
SET HCESCALAS.CONSECTRIAGEU = I.TRIANUMER
FROM HCESCALAS A
INNER JOIN ADTRIAGEU i ON A.IPCODPACI = I.IPCODPACI AND A.NUMINGRES = I.NUMINGRES 
INNER JOIN INUNIFUNC Z ON Z.UFUCODIGO = A.UFUCODIGO
WHERE  (NUMEFOLIO IS NULL OR NUMEFOLIO = '') and HCCTRNOTEID is null and CONSECTRIAGEU is null  and Z.UFUTIPUNI IN ('1') AND A.TIPOESCALA IN ('88','89','92','93')

UPDATE HCESCVASC 
SET HCESCVASC.CONSECTRIAGEU = I.TRIANUMer
FROM HCESCVASC A
INNER JOIN ADTRIAGEU i ON A.IPCODPACI = I.IPCODPACI AND A.NUMINGRES = I.NUMINGRES 
INNER JOIN INUNIFUNC Z ON Z.UFUCODIGO = A.UFUCODIGO
WHERE  CONSECTRIAGEU is null  and Z.UFUTIPUNI IN ('1') AND (NUMEFOLIO IS NULL OR NUMEFOLIO = '')

update HCPLADIAG set TipoFicha = 0 where TipoFicha is null 

--Bug-14265  En el "Dashboard atención domiciliaria" los pacientes se visualizan duplicados por cada solicitud.
UPDATE PADCONTROL SET IDREGISTROPADRE = 0 WHERE IDREGISTROPADRE IS NULL

---------------------------------------------------------------------------------- Sprint Week 08 - 09 (2024) ---------------------------------------------------------------------------------------------------
--PBI 15188	Refactoring "Parametrización de escalas" - "Escala Downton" y "Escala Downton adaptada"																					  
--18/02/2024
--Se renombra la Escala Downton a Escala Downton Adaptada
UPDATE [SEGpermif] SET [indmendes] = 'ESCALA DOWNTON ADAPTADA' WHERE [indidmenu] = 323

---------------------------------------------------------------------------------- Sprint Week 12 - 13 (2024) ---------------------------------------------------------------------------------------------------
UPDATE [INPROFSAL]
SET
    [INPROFSAL].IDADTIPOIDENTIFICA = P.IdentificationTypeId
FROM [INPROFSAL]
    inner join Common.Person p on [INPROFSAL].CODIGONIT = p.IdentificationNumber
where p.IdentificationTypeId is not null AND INPROFSAL.IDADTIPOIDENTIFICA IS NULL

update INCUPSIPS set RequiresLaterality = 0 where RequiresLaterality is null
update INCUPSIPS set ISPANEL = 0 where ISPANEL is null
update Contract.CUPSEntity SET RequiresLaterality = 0 where RequiresLaterality is NULL
update Contract.CUPSEntity SET IsPanel = 0 where IsPanel is NULL
update INCUPSIPS set SERIPSDASHAMBU = SERIPSDASH where SERIPSDASHAMBU is null and SERIPSDASH is NOT NULL

---------------------------------------------------------------------------------- Sprint Week 14 - 15 (2024) ---------------------------------------------------------------------------------------------------
--PBI 16547	Modificaciones generales en formulario "Actividades de agendamiento"																					  
--10/04/2024
--Se actualizan los datos relacionados a los campos "Tipo consulta 4505" y "Tipo res 256" para guardar "No aplica" y "Null" respectivamente
UPDATE AGACTIMED SET R4505CONADPRV = NULL, R4505CONCREDES = NULL, R4505CONJOPRV = NULL, R4505CONNUT = NULL, R4505CONOFT = NULL, R4505CONPSI = NULL, R4505COPRNPRV = NULL, R4505COPRPRV = NULL, R4505NOAPL = 1, TIPRES256 = NULL
update INCUPSIPS set SERIPSDASHAMBU = SERIPSDASH where SERIPSDASHAMBU is null and SERIPSDASH is NOT NULL


---------------------------------------------------------------------------- Sprint Week 14 - 15 (2024) -----------------------------------------------------------------------------------
--PBI POST ACTUALIZACIÖN ODO (Actualizar campo Permitir Descontar mezclas y liquidos sin existencias en Parametros Historias para que tenga el mismo valor por defecto que Permitir Descontar medicamentos sin existencias)
update HCUNITHIS SET AllowDiscountingNonStockMixtures = REGMEDSIN WHERE CODTIPHIS = 'ENF' AND  AllowDiscountingNonStockMixtures IS NULL

---------------------------------------------------------------------------------- Sprint Week 18 - 19 (2024) ---------------------------------------------------------------------------------------------------
--BUG-17199 ODO - PAQUETES QUIRÚRGICOS NO SE PUEDEN INACTIVAR 
--Actualizar el TIPO en  los paquetes 
update AGPAQUETES set TIPO = 1 where TIPO is null 
--BUG-17557 ODO - CUANDOS E INTENTA IMPRIMIR LAS ORDENES AMBULATORIAS Y HOSPITALARIAS SE VISUALIZA EL SIGUIENTE MENSAJE
--Actualizar el campo MOSEPICRI en los registros cuyo campo es NULL y son registros de anestesia
update HCHISPACA SET MOSEPICRI = 1 WHERE MOSEPICRI IS NULL AND TRAINTUNI = 0 AND RECORDANESTESIA = 1

---------------------------------------------------------------------------------- Sprint Week 20 - 21 (2024) ---------------------------------------------------------------------------------------------------
UPDATE HCPARACA SET RequiresInformedConsent = IIF(RequireInformedConsentQx = 1,'[{''Seleccion'':true,''Nombre'':''Procedimientos no quirúrgicos''},{''Seleccion'':true,''Nombre'':''Procedimientos quirúrgicos''},{''Seleccion'':true,''Nombre'':''Laboratorios''},{''Seleccion'':true,''Nombre'':''Imágenes diagnósticas''},{''Seleccion'':true,''Nombre'':''Patologías''},{''Seleccion'':true,''Nombre'':''Hemocomponentes''}]','[{''Seleccion'':false,''Nombre'':''Procedimientos no quirúrgicos''},{''Seleccion'':false,''Nombre'':''Procedimientos quirúrgicos''},{''Seleccion'':false,''Nombre'':''Laboratorios''},{''Seleccion'':false,''Nombre'':''Imágenes diagnósticas''},{''Seleccion'':false,''Nombre'':''Patologías''},{''Seleccion'':false,''Nombre'':''Hemocomponentes''}]') Where RequiresInformedConsent IS NULL

--PBI 16980 4. Refactoring formulario "Criterios de hospitalización" 
--Scrips para actualizar el nombre del formulario "Criterios de hospitalización" a "Criterios de estancia y egreso"
Update SEGpermif Set indmendes = 'Criterios de estancia y egreso' where indidmenu = '081'

---------------------------------------------------------------------------------- Sprint Week 24 - 25 (2024) ---------------------------------------------------------------------------------------------------
--PBI 18446 Cambiar el nombre del Dashboard de Especialistas por Dashboard de interconsultas
--Scrips para actualizar el nombre del formulario "Dashboard Especialistas" a "Dashboard Interconsultas"
Update SEGpermif set indmendes = 'Dashboard Interconsultas' where indidmenu = '076'

---------------------------------------------------------------------------------- Sprint Week 26 - 27 (2024) ---------------------------------------------------------------------------------------------------
--18719 Crear nueva columna en la tabla INCUPSIPS del EHR
-- Scrips para actualizar todos los null del campo llamado ImageGuidanceProcedure  
UPDATE INCUPSIPS SET ImageGuidanceProcedure = 0 WHERE ImageGuidanceProcedure IS NULL;

 

---------------------------------------------------------------------------------- Sprint Week 34 - 35 (2024) ---------------------------------------------------------------------------------------------------
--BUG 20131 Casanare - No permite consultar las "Escalas"
--Actualizar la especialidad de las tablas de escalas para poder imprimir los reportes.
UPDATE  HCESCNTON SET CODESPECI = B.CODESPEC1 FROM HCESCNTON A INNER JOIN INPROFSAL B ON A.CODPROSAL = B.CODPROSAL WHERE A.CODESPECI IS NULL
UPDATE  HCESCALAS SET CODESPECI = B.CODESPEC1 FROM HCESCALAS A INNER JOIN INPROFSAL B ON A.CODPROSAL = B.CODPROSAL where A.CODESPECI is NULL

--poder actualizar el campo StoryType esto para poder hacer regenreacion de reportes a .pdf
UPDATE HCHISPACA SET StoryType = '22' where  IDMODELOHC  IS NOT NULL AND StoryType IS NULL -- Cuando son HC especializadas
UPDATE HCHISPACA SET StoryType = '5'  where  IDMODELOHC  IS NULL AND IDETIPHIS = 'HCURGING1' AND TIPHISPAC = 'I' AND StoryType IS NULL --Cuando son Historias de ingreso
UPDATE HCHISPACA SET StoryType = '6'  where  IDMODELOHC  IS NULL AND IDETIPHIS = 'HCNOTEVO1' AND TIPHISPAC = 'N' AND StoryType IS NULL --Cuando son nota de evolución
UPDATE HCHISPACA SET StoryType = '6'  where  IDMODELOHC  IS NULL AND IDETIPHIS = 'HCNOTEVO1' AND TIPHISPAC = 'I' AND StoryType IS NULL --Cuando son ingreso pero quedan marcadas como nota evolucion
UPDATE HCHISPACA SET StoryType = '5'  where  IDMODELOHC  IS NULL AND  IDETIPHIS = 'HCURGING1' AND TIPHISPAC = 'T' AND StoryType IS NULL --Cuando son historias de control


--BUG-20336 VERSIÓN QA 24.18.0.18 NO SE GENERAN FURIPS
--Se actualiza el campo IdInvoice de la tabla ADFURIPSU para poderlo relacionar con la tabla Invoice y poder generar el archivo plano de RIPS.
UPDATE ADFURIPSU
set IdInvoice = i.Id
FROM ADFURIPSU af
JOIN Billing.Invoice i ON i.InvoiceNumber = REPLACE(af.NUMFAC, ' ','')
WHERE IdInvoice is NULL 

--------------------------------------------------------------------
UPDATE AGAGEMEDD SET CODESPECI = B.CODESPECI 
FROM AGAGEMEDD A 
INNER JOIN AGAGEMEDC B ON A.CODAUTONU = B.CODAUTONU 
WHERE B.CODESPECI IS NOT NULL AND A.CODESPECI IS NULL

--------------------------------------------------------------------- Sprint Week 40- 41 (2024) -------------------------------------------------------------------------------------------------------------------
-- PBI 21136 1. Ajustes a la parametrización del formulario Paquete ordenes
-- Script para actualizar los registros de la columna OrderQuantity de la tabla HCPAQORDENESD a 1 cuando estos sean NULL
Update HCPAQORDENESD SET OrderQuantity = 1 where OrderQuantity IS NULL AND TIPOSERVICIO IN (1,2,4,5,8)

--------------------------------------------------------------------- Sprint Week 42- 43 (2024) -------------------------------------------------------------------------------------------------------------------
-- PBI 21666 Crear Script para garantizar opción en el campo "Finalidad" de ingresos históricos
-- Scrips para actualizar la finalidad en los ingreso historicos
UPDATE ADINGRESO SET IdHealthPurposes = CASE WHEN IINGREPOR IN(1, 4, 5) THEN 44 WHEN IINGREPOR = 3 THEN 27 END WHERE IdHealthPurposes IS NULL
UPDATE ADINGRESO SET IdHealthPurposes = B.FINALIDAD
FROM ADINGRESO A
INNER JOIN (SELECT NUMINGRES, FINALIDAD, MAX(FECHISPAC) AS max_fech	FROM HCHISPACA B GROUP BY NUMINGRES, FINALIDAD) B ON A.NUMINGRES = B.NUMINGRES
WHERE A.IdHealthPurposes IS NULL AND A.IINGREPOR = '2' AND B.FINALIDAD IS NOT NULL;
UPDATE ADINGRESO SET IdHealthPurposes = 44 WHERE IdHealthPurposes  is null and IINGREPOR = '2'

--PBI-21742 1. Migrar funcionalidades de la nota de servicio farmacéutico del EHR Internacional al EHR Colombia
---Llenar la nueva columna PharmaceuticalCare de la tabla HCPARAESCALAS
UPDATE HCPARAESCALAS SET PharmaceuticalCare = 0 where PharmaceuticalCare is null

--------------------------------------------------------------------- Sprint Week 2- 3 (2025) -------------------------------------------------------------------------------------------------------------------
-- PBI 23017
update PRMODELOHC set Gender = 3 where Gender is null

--------------------------------------------------------------------- Sprint Week 4 - 5 (2025) -------------------------------------------------------------------------------------------------------------------
-- PBI 24029 - Creación de nuevo parámetro para asignación de citas con frecuencia
update AGPARAMET SET AppointmentAssignmentFrequently = 1 WHERE AppointmentAssignmentFrequently IS NULL

--PBI 23769 - Ajuste al formulario de parametrización del módulo de "Agendamiento"
UPDATE AGPARAMET
SET GenerateAgendaOnSaturday = 
    CASE 
        WHEN GENAGESAB = 1 THEN '{"CitasMedicas":true,"ApoyoDx":true,"TratamientosEspeciales":true,"Cirugia":true}'
        WHEN GENAGESAB = 0 THEN NULL
    END
WHERE GenerateAgendaOnSaturday IS NULL
UPDATE AGPARAMET
SET GenerateAgendaOnSunday = 
    CASE 
        WHEN GENAGEDOM = 1 THEN '{"CitasMedicas":true,"ApoyoDx":true,"TratamientosEspeciales":true,"Cirugia":true}'
        WHEN GENAGEDOM = 0 THEN NULL
    END
WHERE GenerateAgendaOnSunday IS NULL
UPDATE AGPARAMET
SET GenerateAgendaOnHolidays = 
    CASE 
        WHEN GENAGEFES = 1 THEN '{"CitasMedicas":true,"ApoyoDx":true,"TratamientosEspeciales":true,"Cirugia":true}'
        WHEN GENAGEFES = 0 THEN NULL
    END
WHERE GenerateAgendaOnHolidays IS NULL

---------------------------------------------------------------------------------- Sprint Week 10 - 11 (2025) ---------------------------------------------------------------------------------------------------
--PBI: 24754 
update ADINGRESO  set ADINGRESO.IDADTIPOIDENTIFICA = (
												select ID from ADTIPOIDENTIFICA where SIGLA IN ('CC','cc')
											) 
where ADINGRESO.IDADTIPOIDENTIFICA is null 

---------------------------------------------------------------------------------- Sprint Week 12 - 13 (2025) ---------------------------------------------------------------------------------------------------
-- PBI - 25029 Ajuste de sección de "Parámetros de Oncología" en Parámetros de historias
-- Se actualizan los registros Nulos a 0 por defecto para los dos nuevos paremetros de centro de actencion
Update HCPARACA set AllowAuthorizedCyclesModification = 0 where AllowAuthorizedCyclesModification IS NULL
Update HCPARACA set AllowAuthorizingMTOCicleHC = 0 where AllowAuthorizingMTOCicleHC IS NULL

---------------------------------------------------------------------------------- Sprint Week 18 - 19 (2025) ---------------------------------------------------------------------------------------------------
--pbi: 25866 Scrip para actualizar o  postular por default el campo en "No permitir" que significa opcion 3
UPDATE HCPARACA
SET AllowAuthorizingMTOCicleHC = '3'
WHERE AllowAuthorizingMTOCicleHC IS NULL OR AllowAuthorizingMTOCicleHC = '0';

---------------------------------------------------------------------------------- Sprint Week 20 - 21 (2025) ---------------------------------------------------------------------------------------------------
--PBI: 27055 2. Modificación al formulario "Grupos Imagenología": crear campo para definir enrutamiento a interfaz
UPDATE RISGRIMAGE
SET DefineRoutingToTheInterface = 2
WHERE DefineRoutingToTheInterface IS NULL

--PBI: 27029 2.1. Crear formulario para gestión de imágenes diagnósticas
UPDATE HCORDIMAG
SET 
    SendToInterface = 1,
    SendToInterfaceProfessional = CODPROSAL,
    SendToInterfaceDate = GETDATE()
WHERE 
    SendToInterface IS NULL AND
    SendToInterfaceProfessional IS NULL AND
    SendToInterfaceDate IS NULL;

-- Se actualiza la columna Neonate de la tabla HCCRIUNID a 0 donde Neonate sea NULL
Update HCCRIUNID SET Neonate = 0 where Neonate is null

---------------------------------------------------------------------------------- Sprint Week 22 - 23 (2025) ---------------------------------------------------------------------------------------------------
update HCORDIMAG set EXMREASIT = 0 where EXMREASIT is null

--BUG 27477 - HOMI - NO SE VISULIZA CITAS DE APOYO DIAGNOSTICO ASIGNADAS.
UPDATE AGASICITA SET CODPROSAL = ASA.CODPROSAL, CODESPECI = ASA.CODESPECI from AGASICITA AC INNER JOIN AGENSALAC ASA ON AC.IDSALA = ASA.CODCONCEC WHERE AC.IDSALA IS NOT NULL AND AC.CODPROSAL IS NULL AND AC.CODESPECI IS NULL AND AC.TIPSOLICITU = 2


---------------------------------------------------------------------------------- Sprint Week 24 - 25 (2025) ---------------------------------------------------------------------------------------------------
--PBI 25462 3. Crear opción Permitir registros de terapia sin orden médica en formulario Parámetros de historias
UPDATE HCPARACA
SET AllowTherapyRecordsWithoutDoctorOrder = 1
WHERE AllowTherapyRecordsWithoutDoctorOrder IS NULL;

-- PBI 27760 - Crear función a la opción Anular en formularios de registro de factores de riesgo en la historia clínica
-- Se actualizan registros historicos con estado inactivo = 0 al valor nuevo inactivo = 2
update ReportRiskFactors SET Status = 2 WHERE Status = 0

--PBI 28017 Modificaciones formulario parametrizar nutriciones parenterales.
UPDATE SEGpermif SET indmendes = 'Plantillas NPT' WHERE indidmenu = 888

---------------------------------------------------------------------------------- Sprint Week 36 - 37 (2025) ---------------------------------------------------------------------------------------------------
--PBI-30045 Crear parámetro para activación del proceso lactario en "Parámetros de historias"
update hcparaca set LactationProcessActive = 0 WHERE LactationProcessActive IS NULL 

--PBI-23370 1. Modificación al formulario "Seguimiento asistencial puerperio"
UPDATE PostpartumCareFollowUp SET MomentOfAssessment = 1 WHERE MomentOfAssessment IS NULL

---------------------------------------------------------------------------------- Sprint Week 38 - 39 (2025) ---------------------------------------------------------------------------------------------------
--PBI-29901 1. Crear parámetro En historia clínica exige prescripción de dieta, en el maestro Parámetros de historias
update HCPARACA set DietOrderRequired = 0 WHERE DietOrderRequired IS NULL

---------------------------------------------------------------------------------- Sprint Week 40 - 41 (2025) ---------------------------------------------------------------------------------------------------

---PBI  30892 - 6. Identificación segmentada Hodgkin - No Hodgkin   ( Variable 38 CAC ) 


--🔹 Hodgkin
UPDATE D
SET D.LymphomaType = 1
FROM ADGRUPOCANCERDIAGND D
INNER JOIN ADGRUPOCANCERC C ON C.ID = D.IDADGRUPOCANCERC
WHERE C.CODIGOGRUPO = 10
  AND D.LymphomaType IS NULL
  AND D.CODDIAGNO IN ('C810','C811','C812','C813','C814','C817','C819');

--🔹 No Hodgkin
UPDATE D
SET D.LymphomaType = 2
FROM ADGRUPOCANCERDIAGND D
INNER JOIN ADGRUPOCANCERC C ON C.ID = D.IDADGRUPOCANCERC
WHERE C.CODIGOGRUPO = 10
  AND D.LymphomaType IS NULL
  AND D.CODDIAGNO IN (
    'C820','C821','C822','C827','C829','C830','C831','C832','C833','C834','C835','C837','C839',
    'C840','C841','C842','C844','C845','C846','C847','C849','C850','C851','C852','C857','C859',
    'C967','C969','C823','C824','C825','C826','C838','C848','C860','C861','C862','C863','C864',
    'C865','C866','C836','C960','C961','C962','C963','C884','D761','C843'
  );



--PBI-31100 9. Parámetro para visualización de variables de procedimientos Qx
UPDATE HCPARACA SET SurgicalProcedureCACVariables = 1 WHERE SurgicalProcedureCACVariables IS NULL

--PBI-30993 1. Ajustar funcionalidad campo Fetocardía (latidos/min) en Formulario Triage
UPDATE ADTRIAGEU SET FetocardiaTriage = CONCAT('{"Fetocardia N°1":"', FetocardiaTriage,'"}') WHERE FetocardiaTriage IS NOT NULL AND ISJSON(FetocardiaTriage) = 0 AND TRY_CAST(FetocardiaTriage AS INT) IS NOT NULL AND FetocardiaTriage <> '0'
UPDATE HCEXFISIC SET FETOCARDIA = CONCAT('{"Fetocardia N°1":"', FETOCARDIA,'"}') WHERE FETOCARDIA IS NOT NULL AND ISJSON(FETOCARDIA) = 0 AND TRY_CAST(FETOCARDIA AS INT) IS NOT NULL AND FETOCARDIA <> '0'

---------------------------------------------------------------------------------- Sprint Week 42 - 43 (2025) ---------------------------------------------------------------------------------------------------
--Actualización en la db correspondiente del estado de  la cirugía programada 
UPDATE dbo.AGEPROGQX set CODESTPQX = 6 where CODESTPQX = 2 

--PBI: 31393 - Crear función bloqueo de agenda parcial en formulario Disponibilidad sala apoyo diagnóstico
UPDATE AGBLOQUEOPARCIAL SET ScheduleType = 1 WHERE ScheduleType IS NULL OR ScheduleType = 0
---------------------------------------------------------------------------------- Sprint Week 44 - 45 (2025) ---------------------------------------------------------------------------------------------------
--PBI 32035 - 7.1 Ajuste al guardado de enrrutamiento imagenologia Historia Clinica - Flujo funcional desde la orden médica  ( PBI NO FUNCIONAL )

--Se actualiza valor de enrutamiento según valores establecidos en el nuevo radio group del frm Grupos imagenologia (EJECUTAR EN EL ORDEN EN EL CUAL ESTÁN EN EL ARCHIVO)
--Se actualiza a 2 (valor nuevo establecido para la opción Procesamiento por VieCloud)
update RISGRIMAGE SET DefineRoutingToTheInterface = 2 WHERE DefineRoutingToTheInterface = 0
--Se actualiza a 2 (valor nuevo establecido para la opción Requiere enrutamiento)
update RISGRIMAGE SET DefineRoutingToTheInterface = 0 WHERE DefineRoutingToTheInterface = 1

--PBI 31928 -1. Incluir funcionalidad al campo fetocardia en formulario Triage

--En el campo FetocardiaTriage se actualizan todos los registros tipo json que no sean NULL donde la calve pasa de Fetocardia N° a Feto N°
UPDATE ADTRIAGEU 
SET FetocardiaTriage = (
    SELECT '{' + STRING_AGG(
        '"' + 
        CASE 
            WHEN [key] LIKE 'Fetocardia N°%' 
                THEN ('Feto N°' + SUBSTRING([key], CHARINDEX('°', [key]) + 1, LEN([key])))
            ELSE [key]
        END COLLATE SQL_Latin1_General_CP1_CI_AS + 
        '":"' + [value] + '"', 
        ','
    ) + '}'
    FROM OPENJSON(ADTRIAGEU.FetocardiaTriage)
) WHERE FetocardiaTriage IS NOT NULL AND FetocardiaTriage LIKE '%Fetocardia N°%';


--PBI 31929 -2. Incluir funcionalidad al campo fetocardia en formulario Signos vitales

--En el campo FETOCARDIA se actualizan todos los registros tipo json que no sean NULL donde la calve pasa de Fetocardia N° a Feto N°
UPDATE HCEXFISIC 
SET FETOCARDIA = (
    SELECT '{' + STRING_AGG(
        '"' + 
        CASE 
            WHEN [key] LIKE 'Fetocardia N°%' 
                THEN ('Feto N°' + SUBSTRING([key], CHARINDEX('°', [key]) + 1, LEN([key])))
            ELSE [key]
        END COLLATE SQL_Latin1_General_CP1_CI_AS + 
        '":"' + [value] + '"', 
        ','
    ) + '}'
    FROM OPENJSON(HCEXFISIC.FETOCARDIA)
) WHERE FETOCARDIA IS NOT NULL AND FETOCARDIA LIKE '%Fetocardia N°%';

--BUG 32160 - HOMI - HSJ - ERROR EN AGRUPADOR EN GESTION DE CAMAS.
--Se actualizan estados de reservas de camas antiguas a la fecha 30-09-2025 con el fin de anular las reservas en estado solicitadas y establecer como completadas las reservas confirmadas
update CHRESERVA SET ESTADORES = 3 WHERE ESTADORES IN (1) AND FECINIEST < '2025-09-30 23:59:59.000'
update CHRESERVA SET ESTADORES = 4 WHERE ESTADORES IN (2) AND FECINIEST < '2025-09-30 23:59:59.000'
---------------------------------------------------------------------------------- Sprint Week 46 - 47 (2025) ---------------------------------------------------------------------------------------------------

--PBI 32394. Crear opción de  actualizar la columna FrozenFreshPlasma de la tabla HCCOMSAN a 2 donde FrozenFreshPlasma sea NULL, aclarando que la columna FrozenFreshPlasma es nueva.
 UPDATE HCCOMSAN
 SET FrozenFreshPlasma = 2
 WHERE FrozenFreshPlasma IS NULL;

---------------------------------------------------------------------------------- Sprint Week 48 - 49 (2025) ---------------------------------------------------------------------------------------------------
--- Arreglo de columna FECHAREGISTRO tabla  HCONCOPREG 
 
UPDATE H
SET H.FECHACREACION = X.FECDIAGNO_MIN
FROM HCONCOPREG H
CROSS APPLY (
    SELECT MIN(D.FECDIAGNO) AS FECDIAGNO_MIN
    FROM INDIAGNOH D
    WHERE D.IPCODPACI = H.[6]          -- Identificación paciente
      AND D.CODDIAGNO = H.CODDIAGNO    -- Diagnóstico
      AND D.TIPDIAGNO IN ('C','R')     -- Solo C y R
) X
WHERE H.FECHACREACION IS NULL;

--PBI 32827 -1.Vie verify - Adicionar parámetro "Firma Electrónica" en el módulo de Parametrización de Profesionales de la Salud
UPDATE INPROFSAL SET RequiresElectronicSignature = 0 WHERE RequiresElectronicSignature IS NULL

---------------------------------------------------------------------------------- Sprint Week 50 - 51 (2025) ---------------------------------------------------------------------------------------------------
UPDATE H
SET H.SendToInterface = CASE RIS.DefineRoutingToTheInterface
                            WHEN 0 THEN 0
                            WHEN 1 THEN 1
                            WHEN 2 THEN 2
                        END
FROM HCORDIMAG H
INNER JOIN INCUPSIPS I    ON I.CODSERIPS = H.CODSERIPS
INNER JOIN INCUPSSUB CS  WITH (NOLOCK) ON CS.CODGRUSUB = I.CODGRUSUB
INNER JOIN RISGRIMAGE RIS          ON CS.IDRISGRIMAGE = RIS.ID
WHERE RIS.DefineRoutingToTheInterface IN (0,1,2) AND ESTSERIPS IN (1,2);

--BUG-33024 HSJ -  VALIDACION QUE IMPIDE VISULIZAR REPORTE POR PAGE: Consultar / Imprimir - Seleccionar ordenes medicas
--Se actualiza IHLISTPRO con el DCI creado para medicamentos adicionales

UPDATE IHLISTPRO SET CODDCIMED = 'MED-ADICIONAL' WHERE CODDCIMED IS NULL AND TIPPRODUC = 1 AND CODPRODUC LIKE 'M%'

---------------------------------------------------------------------------------- Sprint Week 2 - 3 (2026) ---------------------------------------------------------------------------------------------------

--PBI 19883: 1 - Creación de parámetro para vigencia de diagnósticos presuntivos de cáncer - este script hace que los valores que fueran nulos los cambie por valor 0 para evitar tener valores nulos en la base de datos.

UPDATE HCPARACA
SET CkbxDxPresumptiveCancer = 0
WHERE CkbxDxPresumptiveCancer IS NULL;

UPDATE HCPARACA
SET DxPresumptiveCancer = 0
WHERE DxPresumptiveCancer IS NULL;

--------------------------------------------------------------------------------- Sprint Week 6 - 7 (2026) ----------------------------------------------------------------------------------------------------
--Product Backlog Item 32340: 1. Ajuste al campo Alérgicos de la pestaña Antecedentes  en el formulario Triage
UPDATE HCMEDRIES SET IdAllergyType = 1 WHERE TIPOREGISTRO = 1

UPDATE R
SET R.Allergen = CONCAT(RTRIM(P.CODPRODUC), '-', RTRIM(P.DESPRODUC))
FROM HCMEDRIES AS R
INNER JOIN IHLISTPRO AS P ON R.CODPRODUC = P.CODPRODUC
WHERE (R.Allergen IS NULL OR LTRIM(R.Allergen) = '')
  AND R.CODPRODUC IS NOT NULL

---------------------------------------------------------------------------------- Sprint Week 19 - 20 (2026) ---------------------------------------------------------------------------------------------------

---- PBI 36415 FECHA 14/05/2026: Ajuste el label formulario Parámetros FURIPS
--- Se actualiza el nombre del Formulario

 update SEGpermif set indmendes = 'Parámetros formularios de reclamaciones' where indidmenu = '88027'  --EHR PROYECTO

 ---------------------------------------------------------------------------------- Sprint Week 24 - 25 (2026) ---------------------------------------------------------------------------------------------------

 -- PBI 37216 Finalidades Tecnología de Salud, agregar campo nuevo para identificar si se debe liquidar pago moderador o no

 UPDATE Admissions.HealthPurposes SET PayModeratorFee = 1 WHERE  PayModeratorFee IS NULL;

 UPDATE Admissions.HealthPurposes SET PayModeratorFee = 0 WHERE  Code IN (
    '11',   -- Valoración integral para la promoción y mantenimiento
    '12',   -- Detección temprana de enfermedad general
    '14',   -- Protección específica
    '19',   -- Planificación familiar y anticoncepción
    '20',   -- Promoción y apoyo a la lactancia materna
    '21',   -- Atención básica de orientación familiar
    '22',   -- Atención para el cuidado preconcepcional
    '23',   -- Atención para el cuidado prenatal
    '24',   -- Interrupción voluntaria del embarazo
    '25',   -- Atención del parto y puerperio
    '27'    -- Atención para el seguimiento del recién nacido
);

 ---------------------------------------------------------------------------------- Sprint Week 30 - 31 (2026) ---------------------------------------------------------------------------------------------------
 ---- BUG 39025 FECHA 22/07/2026: ESE - PRUEBAS - Formulario Pacientes no carga la dirección
--- Se actualiza el valor de IDPAIS en tabla INDEPARTA para cumplir JOIN al cargar las direcciones
update INDEPARTA SET IDPAIS = 1 WHERE IDPAIS IS NULL
