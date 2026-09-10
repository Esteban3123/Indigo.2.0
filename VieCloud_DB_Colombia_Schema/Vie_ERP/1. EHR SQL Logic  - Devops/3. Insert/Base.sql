--script para crear consecutivo de radicacion de QX
insert into INCONSECU(IDCONSECU,CONSECDES ,CONNUMACT  )values('00000030','Consecutivo de Radicación de cirugía',0) 
insert into INCONSECU(IDCONSECU,CONSECDES ,CONNUMACT  )values('00000031','Consecutivo Hoja de Gasto',0) 

--se inserta nueva unidad medida internacional para los esquemas Oncologicos
insert into INUNIMEDI(CODUNIMED, DESUNIMED ,ABRUNIMED ,TIPUNIDAD ,INDAUDFOR ,CODHOMHV  )values('134','mg/M2','mg/M2',1,1,null)
insert into INUNIMEDI(CODUNIMED, DESUNIMED ,ABRUNIMED ,TIPUNIDAD ,INDAUDFOR ,CODHOMHV  )values('135','mg/Kg','mg/Kg',1,1,null)
insert into INUNIMEDI(CODUNIMED, DESUNIMED ,ABRUNIMED ,TIPUNIDAD ,INDAUDFOR ,CODHOMHV  )values('136','UI/M2','ui/M2',1,1,null)

----- 3269 Formulario - aislamiento / visualización de aislamiento
INSERT INTO CHTIPOSAISLAMIENTOS ([Id],[Codigo],[Nombre],[Color],[Estado],[FechaCreacion],[UsuarioCreacion]) VALUES (1,'001','Aerosol','HotTrack',1,CAST(N'2019-06-06T15:48:27.303' AS DateTime),'999')
INSERT INTO CHTIPOSAISLAMIENTOS ([Id],[Codigo],[Nombre],[Color],[Estado],[FechaCreacion],[UsuarioCreacion]) VALUES (2,'002','Contacto','Orange',1,CAST(N'2019-06-06T15:48:27.303' AS DateTime),'999')
INSERT INTO CHTIPOSAISLAMIENTOS ([Id],[Codigo],[Nombre],[Color],[Estado],[FechaCreacion],[UsuarioCreacion]) VALUES (3,'003','Estandar','AppWorkspace',1,CAST(N'2019-06-06T15:48:27.303' AS DateTime),'999')
INSERT INTO CHTIPOSAISLAMIENTOS ([Id],[Codigo],[Nombre],[Color],[Estado],[FechaCreacion],[UsuarioCreacion]) VALUES (4,'004','Gota','SpringGreen',1,CAST(N'2019-06-06T15:48:27.303' AS DateTime),'999')
INSERT INTO CHTIPOSAISLAMIENTOS ([Id],[Codigo],[Nombre],[Color],[Estado],[FechaCreacion],[UsuarioCreacion]) VALUES (5,'005','Protector','Pink',1,CAST(N'2019-06-06T15:48:27.303' AS DateTime),'999')

--Consecutivo medicamentos control
insert into INCONSECU(IDCONSECU,CONSECDES,CONNUMACT)values('00000032','Consecutivo Medicamentos de Control                                             ',0)
insert into INCONSECU(IDCONSECU,CONSECDES,CONNUMACT)values('00000032','Consecutivo Medicamentos de Control                                             ',0)

insert into dbo.INCONSECU(IDCONSECU,CONSECDES,CONNUMACT)values('00000033','Paciente interfaz ALULA','1') 
insert into dbo.INCONSECU(IDCONSECU,CONSECDES,CONNUMACT)VALUES('00000034','Ordenes interfaz ALULA','1') 

------------------------------------------------------------------------------------- Sprint 4 -------------------------------------------------------------------------------------------------------
insert into ADTIPOIDENTIFICA (CODIGO,SIGLA,NOMBRE,ESTADO,USUARIOCREA,FECHACREA) VALUES(1,'CC','CC -  Cédula de Ciudadanía',1,'999',Common.GETDATE())
insert into ADTIPOIDENTIFICA (CODIGO,SIGLA,NOMBRE,ESTADO,USUARIOCREA,FECHACREA) VALUES(2,'CE','CE  -  Cédula de Extranjería',1,'999',Common.GETDATE())
insert into ADTIPOIDENTIFICA (CODIGO,SIGLA,NOMBRE,ESTADO,USUARIOCREA,FECHACREA) VALUES(3,'TI','TI   -  Tarjeta de Identidad',1,'999',Common.GETDATE())
insert into ADTIPOIDENTIFICA (CODIGO,SIGLA,NOMBRE,ESTADO,USUARIOCREA,FECHACREA) VALUES(4,'RC','RC  -  Registro Civil',1,'999',Common.GETDATE())
insert into ADTIPOIDENTIFICA (CODIGO,SIGLA,NOMBRE,ESTADO,USUARIOCREA,FECHACREA) VALUES(5,'PA','PA  -  Pasaporte',1,'999',Common.GETDATE())
insert into ADTIPOIDENTIFICA (CODIGO,SIGLA,NOMBRE,ESTADO,USUARIOCREA,FECHACREA) VALUES(6,'AS','AS  -  Adulto Sin Identificación',1,'999',Common.GETDATE())
insert into ADTIPOIDENTIFICA (CODIGO,SIGLA,NOMBRE,ESTADO,USUARIOCREA,FECHACREA) VALUES(7,'MS','MS  -  Menor Sin Identificación',1,'999',Common.GETDATE())
insert into ADTIPOIDENTIFICA (CODIGO,SIGLA,NOMBRE,ESTADO,USUARIOCREA,FECHACREA) VALUES(8,'NU','NU  -  Número único de identificación personal',1,'999',Common.GETDATE())
insert into ADTIPOIDENTIFICA (CODIGO,SIGLA,NOMBRE,ESTADO,USUARIOCREA,FECHACREA) VALUES(9,'CN','CN  -  Certificado de Nacido Vivo',1,'999',Common.GETDATE())
insert into ADTIPOIDENTIFICA (CODIGO,SIGLA,NOMBRE,ESTADO,USUARIOCREA,FECHACREA) VALUES(10,'CD','CD  -  Carnet Diplomático (Aplica para extranjeros)',1,'999',Common.GETDATE())
insert into ADTIPOIDENTIFICA (CODIGO,SIGLA,NOMBRE,ESTADO,USUARIOCREA,FECHACREA) VALUES(11,'SC','SC  -  Salvoconducto (Aplica para extranjeros)',1,'999',Common.GETDATE())
insert into ADTIPOIDENTIFICA (CODIGO,SIGLA,NOMBRE,ESTADO,USUARIOCREA,FECHACREA) VALUES(12,'PE','PE  -  Permiso especial de Permanencia (Aplica para extranjeros)',1,'999',Common.GETDATE())
insert into ADTIPOIDENTIFICA (CODIGO,SIGLA,NOMBRE,ESTADO,USUARIOCREA,FECHACREA) VALUES(13,'PT ','PT   -  Permiso temporal de permanencia',1,'999',Common.GETDATE())
insert into ADTIPOIDENTIFICA (CODIGO,SIGLA,NOMBRE,ESTADO,USUARIOCREA,FECHACREA) VALUES(14,'DE','DE - Documento extranjero',1,'999',Common.GETDATE())
insert into ADTIPOIDENTIFICA (CODIGO,SIGLA,NOMBRE,ESTADO,USUARIOCREA,FECHACREA) VALUES(15,'SI','SI - Sin identificación',1,'999',Common.GETDATE())

------------------------------------------------------------------------------------- Sprint Week 38'39 -------------------------------------------------------------------------------------------------------
--PBI 5759 2. Creación Maestro "Causas de atención"
INSERT into Causesofattention values ('1','Heridos en combate',0,'999',common.getdate(),null,null)
insert into Causesofattention values ('2','Enfermedad profesional',0,'999',common.getdate(),null,null)
insert into Causesofattention values ('3','Enfermedad general adulto',0,'999',common.getdate(),null,null)
insert into Causesofattention values ('4','Enfermedad general pediatría',0,'999',common.getdate(),null,null)
insert into Causesofattention values ('5','Odontología',0,'999',common.getdate(),null,null)
insert into Causesofattention values ('6','Accidente de transito',0,'999',common.getdate(),null,null)
insert into Causesofattention values ('7','Evento catastrófico',0,'999',common.getdate(),null,null)
insert into Causesofattention values ('8','Quemados',0,'999',common.getdate(),null,null)
insert into Causesofattention values ('9','Maternidad',0,'999',common.getdate(),null,null)
insert into Causesofattention values ('10','Accidente laboral',0,'999',common.getdate(),null,null)
insert into Causesofattention values ('11','Cirugía programada',0,'999',common.getdate(),null,null)
insert into Causesofattention values ('12','Accidente de trabajo',1,'999',common.getdate(),null,null)
insert into Causesofattention values ('13','Accidente en el hogar',1,'999',common.getdate(),null,null)
insert into Causesofattention values ('14','Accidente de transito de origen común',1,'999',common.getdate(),null,null)
insert into Causesofattention values ('15','Accidente de transito de origen laboral',1,'999',common.getdate(),null,null)
insert into Causesofattention values ('16','Accidente en el entorno educativo',1,'999',common.getdate(),null,null)
insert into Causesofattention values ('17','Otro tipo de accidente',1,'999',common.getdate(),null,null)
insert into Causesofattention values ('18','Evento catastrófico de origen natural',1,'999',common.getdate(),null,null)
insert into Causesofattention values ('19','Lesion por agresión',1,'999',common.getdate(),null,null)
insert into Causesofattention values ('20','Lesion auto infligida',1,'999',common.getdate(),null,null)
insert into Causesofattention values ('21','Sospecha de violencia física',1,'999',common.getdate(),null,null)
insert into Causesofattention values ('22','Sospecha de violencia psicológica',1,'999',common.getdate(),null,null)
insert into Causesofattention values ('23','Sospecha de violencia sexual',1,'999',common.getdate(),null,null)
insert into Causesofattention values ('24','Sospecha de negligencia y abandono',1,'999',common.getdate(),null,null)
insert into Causesofattention values ('25','IVE relacionada con peligro a la salud o vida de la mujer',1,'999',common.getdate(),null,null)
insert into Causesofattention values ('26','IVE por malformacion congenita incompatible con la vida',1,'999',common.getdate(),null,null)
insert into Causesofattention values ('27','IVE por violencia sexual, incesto o por inseminacion artificial o transferencia de ovulo fecundado no consentida',1,'999',common.getdate(),null,null)
insert into Causesofattention values ('28','Evento adverso en salud',1,'999',common.getdate(),null,null)
insert into Causesofattention values ('29','Enfermedad general',1,'999',common.getdate(),null,null)
insert into Causesofattention values ('30','Enfermedad laboral',1,'999',common.getdate(),null,null)
insert into Causesofattention values ('31','Promoción y mantenimiento de la salud - Intervenciones individuales',1,'999',common.getdate(),null,null)
insert into Causesofattention values ('32','Intervención colectiva',1,'999',common.getdate(),null,null)
insert into Causesofattention values ('33','Atención de población materno perinatal',1,'999',common.getdate(),null,null)
insert into Causesofattention values ('34','Seguridad y Salud en el trabajo',1,'999',common.getdate(),null,null)
insert into Causesofattention values ('35','Otros eventos catastróficos',1,'999',common.getdate(),null,null)
insert into Causesofattention values ('36','Accidente en mina anti- personal - MAP',1,'999',common.getdate(),null,null)
insert into Causesofattention values ('37','Accidente de Artefacto Explosivo Improvisado - AEI',1,'999',common.getdate(),null,null)
insert into Causesofattention values ('38','Accidente de Munición Sin Explotar - MUSE',1,'999',common.getdate(),null,null)
insert into Causesofattention values ('39','Otra victima de conflicto armado colombiano',1,'999',common.getdate(),null,null)
insert into Causesofattention values ('40','Riesgo ambiental',1,'999',common.getdate(),null,null)

------------------------------------------------------------------------------------- Sprint Week 40'41 -------------------------------------------------------------------------------------------------------
--PBI 5939 3.1 Actualización lógica Formulario "Historia clínica" ambulatoria - campo "Finalidad" 

INSERT into [Admissions].[HealthPurposes] values ('1', 'Atención del parto (Puerperio)', 0, '999',common.getdate(),null,null)
insert into [Admissions].[HealthPurposes] values ('2', 'Atención del recién nacido', 0, '999',common.getdate(),null,null)
insert into [Admissions].[HealthPurposes] values ('3', 'Atención en planificación familiar', 0, '999',common.getdate(),null,null)
insert into [Admissions].[HealthPurposes] values ('4', 'Detección de alteraciones del crecimiento y desarrollo del menor de 10 años', 0, '999',common.getdate(),null,null)
insert into [Admissions].[HealthPurposes] values ('5', 'Detección de alteraciones del desarrollo del joven', 0, '999',common.getdate(),null,null)
insert into [Admissions].[HealthPurposes] values ('6', 'Detección de alteraciones del embarazo', 0, '999',common.getdate(),null,null)
insert into [Admissions].[HealthPurposes] values ('7', 'Detección de alteraciones del adulto', 0, '999',common.getdate(),null,null)
insert into [Admissions].[HealthPurposes] values ('8', 'Detección de alteraciones de la agudeza visual', 0, '999',common.getdate(),null,null)
insert into [Admissions].[HealthPurposes] values ('9', 'Detección de enfermedad profesional', 0, '999',common.getdate(),null,null)
insert into [Admissions].[HealthPurposes] values ('10', 'No aplica', 0, '999',common.getdate(),null,null)
insert into [Admissions].[HealthPurposes] values ('11', 'Valoración integral para la promoción y mantenimiento', 1, '999',common.getdate(),null,null)
insert into [Admissions].[HealthPurposes] values ('12', 'Detección temprana de enfermedad general', 1, '999',common.getdate(),null,null)
insert into [Admissions].[HealthPurposes] values ('13', 'Detección temprana de enfermedad laboral', 1, '999',common.getdate(),null,null)
insert into [Admissions].[HealthPurposes] values ('14', 'Protección específica', 1, '999',common.getdate(),null,null)
insert into [Admissions].[HealthPurposes] values ('15', 'Diagnóstico', 1, '999',common.getdate(),null,null)
insert into [Admissions].[HealthPurposes] values ('16', 'Tratamiento', 1, '999',common.getdate(),null,null)
insert into [Admissions].[HealthPurposes] values ('17', 'Rehabilitación', 1, '999',common.getdate(),null,null)
insert into [Admissions].[HealthPurposes] values ('18', 'Paliación', 1, '999',common.getdate(),null,null)
insert into [Admissions].[HealthPurposes] values ('19', 'Planificación familiar y anticoncepción', 1, '999',common.getdate(),null,null)
insert into [Admissions].[HealthPurposes] values ('20', 'Promoción y apoyo a la lactancia materna', 1, '999',common.getdate(),null,null)
insert into [Admissions].[HealthPurposes] values ('21', 'Atención básica de orientación familiar', 1, '999',common.getdate(),null,null)
insert into [Admissions].[HealthPurposes] values ('22', 'Atención para el cuidado preconcepcional', 1, '999',common.getdate(),null,null)
insert into [Admissions].[HealthPurposes] values ('23', 'Atención para el cuidado prenatal', 1, '999',common.getdate(),null,null)
insert into [Admissions].[HealthPurposes] values ('24', 'Interrupción Voluntaria del Embarazo', 1, '999',common.getdate(),null,null)
insert into [Admissions].[HealthPurposes] values ('25', 'Atención del parto y puerperio', 1, '999',common.getdate(),null,null)
insert into [Admissions].[HealthPurposes] values ('26', 'Atención para el cuidado del recién nacido', 1, '999',common.getdate(),null,null)
insert into [Admissions].[HealthPurposes] values ('27', 'Atención para el seguimiento del recién nacido', 1, '999',common.getdate(),null,null)
insert into [Admissions].[HealthPurposes] values ('28', 'Preparación para la maternidad y paternidad', 1, '999',common.getdate(),null,null)
insert into [Admissions].[HealthPurposes] values ('29', 'Promoción de actividad física', 1, '999',common.getdate(),null,null)
insert into [Admissions].[HealthPurposes] values ('30', 'Promoción de la cesación del tabaquismo', 1, '999',common.getdate(),null,null)
insert into [Admissions].[HealthPurposes] values ('31', 'Prevención del consumo de sustancias psicoactivas', 1, '999',common.getdate(),null,null)
insert into [Admissions].[HealthPurposes] values ('32', 'Promoción de la alimentación saludable', 1, '999',common.getdate(),null,null)
insert into [Admissions].[HealthPurposes] values ('33', 'Promoción para el ejercicio de los derechos sexuales y derechos reproductivos', 1, '999',common.getdate(),null,null)
insert into [Admissions].[HealthPurposes] values ('34', 'Promoción para el desarrollo de habilidades para la vida', 1, '999',common.getdate(),null,null)
insert into [Admissions].[HealthPurposes] values ('35', 'Promoción para la construcción de estrategias de afrontamiento frente a sucesos vitales', 1, '999',common.getdate(),null,null)
insert into [Admissions].[HealthPurposes] values ('36', 'Promoción de la sana convivencia y el tejido social', 1, '999',common.getdate(),null,null)
insert into [Admissions].[HealthPurposes] values ('37', 'Promoción de un ambiente seguro y de cuidado y protección del ambiente', 1, '999',common.getdate(),null,null)
insert into [Admissions].[HealthPurposes] values ('38', 'Promoción del empoderamiento para el ejercicio del derecho de salud', 1, '999',common.getdate(),null,null)
insert into [Admissions].[HealthPurposes] values ('39', 'Promoción para la adopción de prácticas de crianza y cuidado de la salud', 1, '999',common.getdate(),null,null)
insert into [Admissions].[HealthPurposes] values ('40', 'Promoción de la capacidad de agencia y cuidado de la salud', 1, '999',common.getdate(),null,null)
insert into [Admissions].[HealthPurposes] values ('41', 'Desarrollo de habilidades cognitivas', 1, '999',common.getdate(),null,null)
insert into [Admissions].[HealthPurposes] values ('42', 'Intervención colectiva', 1, '999',common.getdate(),null,null)
insert into [Admissions].[HealthPurposes] values ('43', 'Modificación de la estética corporal (fines estéticos)', 1, '999',common.getdate(),null,null)
insert into [Admissions].[HealthPurposes] values ('44', 'Otra', 1, '999',common.getdate(),null,null)

------------------------------------------------------------------------------------- Sprint Week 6'7 2023 -------------------------------------------------------------------------------------------------------
--PBI 7354  creaccion maestro" - Vías ingreso servicios salud
INSERT INTO EntryRoutesHealthServices values (01,'Demanda espontánea consulta externa', '01', 2, 1, '999',common.getdate(), null,null)
INSERT INTO EntryRoutesHealthServices values (02,'Demanda espontánea urgencias','01', 1, 1 , '999',common.getdate(), null,null)
INSERT INTO EntryRoutesHealthServices values (03,'Derivado de consulta externa','02', 2, 1 , '999',common.getdate(), null,null)
INSERT INTO EntryRoutesHealthServices values (04,'Derivado de urgencias','03', 1, 1 , '999',common.getdate(), null,null)
INSERT INTO EntryRoutesHealthServices values (05,'Derivado de hospitalización','04', 5, 1 , '999',common.getdate(), null,null)
INSERT INTO EntryRoutesHealthServices values (06,'Derivado de sala de cirugía','05', 1, 1 , '999',common.getdate(), null,null)
INSERT INTO EntryRoutesHealthServices values (07,'Derivado de sala de partos','06', 3, 1 , '999',common.getdate(), null,null)
INSERT INTO EntryRoutesHealthServices values (08,'Recién nacido en la institución','07', 3, 1 , '999',common.getdate(), null,null)
INSERT INTO EntryRoutesHealthServices values (09,'Recién nacido en otra institución','08', 3, 1 , '999',common.getdate(), null,null)
INSERT INTO EntryRoutesHealthServices values (10,'Derivado o referido de hospitalización domiciliaria','09', 5, 1 , '999',common.getdate(), null,null)
INSERT INTO EntryRoutesHealthServices values (11,'Derivado de atención domiciliaria','10', 2, 1 , '999',common.getdate(), null,null)
INSERT INTO EntryRoutesHealthServices values (12,'Derivado de telemedicina','11', 2, 1 , '999',common.getdate(), null,null)
INSERT INTO EntryRoutesHealthServices values (13,'Derivado de jornada de salud','12', 2, 1 , '999',common.getdate(), null,null)
INSERT INTO EntryRoutesHealthServices values (14,'Referido de otra institución','13', 4, 1 , '999',common.getdate(), null,null)
INSERT INTO EntryRoutesHealthServices values (15,'Contrarreferido de otra institución','14', 4, 1 , '999',common.getdate(), null,null)

------------------------------------------------------------------------------------- Sprint Week 32 - 33 2023 -------------------------------------------------------------------------------------------------------
 -- 18 Agosto 2023
-- Author: Andres David Losada Vladerrama
-- PBI 11636 4. Creación maestro "Modalidades de atención"
--Formulario Modalidades de atención / visualización de las modalidades de atención
insert into Admissions.AdmissionModalities ([id], [Code], [Name], [CodeRIPS], [Status], [UserCreation], [DateCreation]) values (1 , '1', 'Intramural', '01', 1, '999', GETDATE())
insert into Admissions.AdmissionModalities ([id], [Code], [Name], [CodeRIPS], [Status], [UserCreation], [DateCreation]) values (2 , '2', 'Extramural unidad móvil', '02', 1, '999', GETDATE())
insert into Admissions.AdmissionModalities ([id], [Code], [Name], [CodeRIPS], [Status], [UserCreation], [DateCreation]) values (3 , '3', 'Extramural domiciliaria', '03', 1, '999', GETDATE())
insert into Admissions.AdmissionModalities ([id], [Code], [Name], [CodeRIPS], [Status], [UserCreation], [DateCreation]) values (4 , '4', 'Extramural jornada de salud', '04', 1, '999', GETDATE())
insert into Admissions.AdmissionModalities ([id], [Code], [Name], [CodeRIPS], [Status], [UserCreation], [DateCreation]) values (6 , '6', 'Telemedicina interactiva', '06', 1, '999', GETDATE())
insert into Admissions.AdmissionModalities ([id], [Code], [Name], [CodeRIPS], [Status], [UserCreation], [DateCreation]) values (7 , '7', 'Telemedicina no interactiva', '07', 1, '999', GETDATE())
insert into Admissions.AdmissionModalities ([id], [Code], [Name], [CodeRIPS], [Status], [UserCreation], [DateCreation]) values (8 , '8', 'Telemedicina telexperticia', '08', 1, '999', GETDATE())
insert into Admissions.AdmissionModalities ([id], [Code], [Name], [CodeRIPS], [Status], [UserCreation], [DateCreation]) values (9 , '9', 'Telemedicina telemonitoreo', '09', 1, '999', GETDATE())

--PBI 11098 HOMI- 4. Crear formulario Parámetros FURIPS
----------------Tipos de identificación de la rejilla víctima.
insert into [Admissions].[FuripsParameters](Code,IdentificationType,UserCreation,DateCreation)
values(1,1,999,'2023-08-17 09:47:40.677')
insert into [Admissions].[FuripsParameters](Code,IdentificationType,UserCreation,DateCreation)
values(2,1,999,'2023-08-17 09:47:40.677')
insert into [Admissions].[FuripsParameters](Code,IdentificationType,UserCreation,DateCreation)
values(9,1,999,'2023-08-17 09:47:40.677')
insert into [Admissions].[FuripsParameters](Code,IdentificationType,UserCreation,DateCreation)
values(5,1,999,'2023-08-17 09:47:40.677')
insert into [Admissions].[FuripsParameters](Code,IdentificationType,UserCreation,DateCreation)
values(3,1,999,'2023-08-17 09:47:40.677')
insert into [Admissions].[FuripsParameters](Code,IdentificationType,UserCreation,DateCreation)
values(4,1,999,'2023-08-17 09:47:40.677')
insert into [Admissions].[FuripsParameters](Code,IdentificationType,UserCreation,DateCreation)
values(6,1,999,'2023-08-17 09:47:40.677')
insert into [Admissions].[FuripsParameters](Code,IdentificationType,UserCreation,DateCreation)
values(7,1,999,'2023-08-17 09:47:40.677')
insert into [Admissions].[FuripsParameters](Code,IdentificationType,UserCreation,DateCreation)
values(10,1,999,'2023-08-17 09:47:40.677')
insert into [Admissions].[FuripsParameters](Code,IdentificationType,UserCreation,DateCreation)
values(11,1,999,'2023-08-17 09:47:40.677')
insert into [Admissions].[FuripsParameters](Code,IdentificationType,UserCreation,DateCreation)
values(12,1,999,'2023-08-17 09:47:40.677')
insert into [Admissions].[FuripsParameters](Code,IdentificationType,UserCreation,DateCreation)
values(13,1,999,'2023-08-17 09:47:40.677')
insert into [Admissions].[FuripsParameters](Code,IdentificationType,UserCreation,DateCreation)
values(14,1,999,'2023-08-17 09:47:40.677')
----------------Tipos de identificación de la rejilla propietario.
insert into [Admissions].[FuripsParameters](Code,IdentificationType,UserCreation,DateCreation)
values(1,2,999,'2023-08-17 09:47:40.677')
insert into [Admissions].[FuripsParameters](Code,IdentificationType,UserCreation,DateCreation)
values(2,2,999,'2023-08-17 09:47:40.677')
insert into [Admissions].[FuripsParameters](Code,IdentificationType,UserCreation,DateCreation)
values(10,2,999,'2023-08-17 09:47:40.677')
insert into [Admissions].[FuripsParameters](Code,IdentificationType,UserCreation,DateCreation)
values(14,2,999,'2023-08-17 09:47:40.677')
insert into [Admissions].[FuripsParameters](Code,IdentificationType,UserCreation,DateCreation)
values(11,2,999,'2023-08-17 09:47:40.677')
insert into [Admissions].[FuripsParameters](Code,IdentificationType,UserCreation,DateCreation)
values(12,2,999,'2023-08-17 09:47:40.677')
insert into [Admissions].[FuripsParameters](Code,IdentificationType,UserCreation,DateCreation)
values(13,2,999,'2023-08-17 09:47:40.677')
---El NIT = Número de identificación tributaria no está creado en ADTIPOIDENTIFICA
--insert into [Admissions].[FuripsParameters](Code,IdentificationType,UserCreation,DateCreation)
--values(16,2,999,'2023-08-17 09:47:40.677')
----------------Tipos de identificación de la rejilla conductor.
insert into [Admissions].[FuripsParameters](Code,IdentificationType,UserCreation,DateCreation)
values(1,3,999,'2023-08-17 09:47:40.677')
insert into [Admissions].[FuripsParameters](Code,IdentificationType,UserCreation,DateCreation)
values(2,3,999,'2023-08-17 09:47:40.677')
insert into [Admissions].[FuripsParameters](Code,IdentificationType,UserCreation,DateCreation)
values(5,3,999,'2023-08-17 09:47:40.677')
insert into [Admissions].[FuripsParameters](Code,IdentificationType,UserCreation,DateCreation)
values(4,3,999,'2023-08-17 09:47:40.677')
insert into [Admissions].[FuripsParameters](Code,IdentificationType,UserCreation,DateCreation)
values(3,3,999,'2023-08-17 09:47:40.677')
insert into [Admissions].[FuripsParameters](Code,IdentificationType,UserCreation,DateCreation)
values(10,3,999,'2023-08-17 09:47:40.677')
insert into [Admissions].[FuripsParameters](Code,IdentificationType,UserCreation,DateCreation)
values(11,3,999,'2023-08-17 09:47:40.677')
insert into [Admissions].[FuripsParameters](Code,IdentificationType,UserCreation,DateCreation)
values(14,3,999,'2023-08-17 09:47:40.677')
insert into [Admissions].[FuripsParameters](Code,IdentificationType,UserCreation,DateCreation)
values(12,3,999,'2023-08-17 09:47:40.677')
insert into [Admissions].[FuripsParameters](Code,IdentificationType,UserCreation,DateCreation)
values(13,3,999,'2023-08-17 09:47:40.677')
----------------Tipos de identificación de la rejilla profesional.
insert into [Admissions].[FuripsParameters](Code,IdentificationType,UserCreation,DateCreation)
values(1,4,999,'2023-08-17 09:47:40.677')
insert into [Admissions].[FuripsParameters](Code,IdentificationType,UserCreation,DateCreation)
values(2,4,999,'2023-08-17 09:47:40.677')
insert into [Admissions].[FuripsParameters](Code,IdentificationType,UserCreation,DateCreation)
values(5,4,999,'2023-08-17 09:47:40.677')
insert into [Admissions].[FuripsParameters](Code,IdentificationType,UserCreation,DateCreation)
values(12,4,999,'2023-08-17 09:47:40.677')
insert into [Admissions].[FuripsParameters](Code,IdentificationType,UserCreation,DateCreation)
values(13,4,999,'2023-08-17 09:47:40.677')							  

--PBI12432	--parametrización de paginas, Formatos de documentación clínica intrahospitalarios'																					  
--25/9/2023
INSERT INTO [ClinicalParameters].[HistoryPages] (Id,Code,[Name],[Status],CreationDate,CreationUser) VALUES (1,'01', 'Información medica general', 1, Common.GETDATE(), '999')
INSERT INTO [ClinicalParameters].[HistoryPages] (Id,Code,[Name],[Status],CreationDate,CreationUser) VALUES (2,'02', 'Motivo de consulta / Enfermedad actual', 1, Common.GETDATE(), '999')
INSERT INTO [ClinicalParameters].[HistoryPages] (Id,Code,[Name],[Status],CreationDate,CreationUser) VALUES (3,'03', 'Antecedentes', 1, Common.GETDATE(), '999')
INSERT INTO [ClinicalParameters].[HistoryPages] (Id,Code,[Name],[Status],CreationDate,CreationUser) VALUES (4,'04', 'Revisión por sistemas', 1, Common.GETDATE(), '999')
INSERT INTO [ClinicalParameters].[HistoryPages] (Id,Code,[Name],[Status],CreationDate,CreationUser) VALUES (5,'05', 'Examen físico', 1, Common.GETDATE(), '999')
INSERT INTO [ClinicalParameters].[HistoryPages] (Id,Code,[Name],[Status],CreationDate,CreationUser) VALUES (6,'06', 'Análisis', 1, Common.GETDATE(), '999')
INSERT INTO [ClinicalParameters].[HistoryPages] (Id,Code,[Name],[Status],CreationDate,CreationUser) VALUES (7,'07', 'Impresión diagnóstica', 1, Common.GETDATE(), '999')
INSERT INTO [ClinicalParameters].[HistoryPages] (Id,Code,[Name],[Status],CreationDate,CreationUser) VALUES (8,'08', 'Plan de manejo', 1, Common.GETDATE(), '999')
INSERT INTO [ClinicalParameters].[HistoryPages] (Id,Code,[Name],[Status],CreationDate,CreationUser) VALUES (9,'09', 'Escalas', 1, Common.GETDATE(), '999')
INSERT INTO [ClinicalParameters].[HistoryPages] (Id,Code,[Name],[Status],CreationDate,CreationUser) VALUES (10,'10', 'Destino del paciente', 1, Common.GETDATE(), '999')

------------------------------------------------------------------------------------- Sprint Week 40'41 2023 -------------------------------------------------------------------------------------------------------
		--12869	--PBI	--parametrización de Tipos de admisiones																					  
--04/10/2023
INSERT [Admissions].[AdmissionType] ([Id], [Code], [Name], [IsSpecialTreatment], [TreatmentType], [Color], [CreationDate], [CreationUser], [ModificationDate], [ModificationUser]) VALUES (1, N'001', N'Normal', 0, 1, N'#CCD1D1', CAST(N'2023-01-01T00:00:00.000' AS DateTime), N'999 ', NULL, NULL)
GO
INSERT [Admissions].[AdmissionType] ([Id], [Code], [Name], [IsSpecialTreatment], [TreatmentType], [Color], [CreationDate], [CreationUser], [ModificationDate], [ModificationUser]) VALUES (2, N'002', N'Renal ', 1, 2, N'#F53926', CAST(N'2023-01-01T00:00:00.000' AS DateTime), N'999 ', NULL, NULL)
GO
INSERT [Admissions].[AdmissionType] ([Id], [Code], [Name], [IsSpecialTreatment], [TreatmentType], [Color], [CreationDate], [CreationUser], [ModificationDate], [ModificationUser]) VALUES (7, N'003', N'Oncológico - Quimioterapia ', 1, 3, N'#45F526', CAST(N'2023-01-01T00:00:00.000' AS DateTime), N'999 ', NULL, NULL)
GO
INSERT [Admissions].[AdmissionType] ([Id], [Code], [Name], [IsSpecialTreatment], [TreatmentType], [Color], [CreationDate], [CreationUser], [ModificationDate], [ModificationUser]) VALUES (8, N'004', N'Oncológico - Radioterapia ', 1, 4, N'#264CF5', CAST(N'2023-01-01T00:00:00.000' AS DateTime), N'999 ', NULL, NULL)
GO
INSERT [Admissions].[AdmissionType] ([Id], [Code], [Name], [IsSpecialTreatment], [TreatmentType], [Color], [CreationDate], [CreationUser], [ModificationDate], [ModificationUser]) VALUES (9, N'005', N'Oncológico - Braquiterapia', 1, 5, N'#F526C9', CAST(N'2023-01-01T00:00:00.000' AS DateTime), N'999 ', NULL, NULL)
GO

------------------------------------------------------------------------------------- Sprint Week XX'XX 2023 -------------------------------------------------------------------------------------------------------
--PBI 12653 Master- 4. Crear formulario Parámetros FURTRAN
----------------Tipos de identificación de la rejilla víctima.
insert into [Admissions].[FurtranParameters](Code,IdentificationType,UserCreation,DateCreation)
values(1,1,999,'2023-08-17 09:47:40.677')
insert into [Admissions].[FurtranParameters](Code,IdentificationType,UserCreation,DateCreation)
values(2,1,999,'2023-08-17 09:47:40.677')
insert into [Admissions].[FurtranParameters](Code,IdentificationType,UserCreation,DateCreation)
values(9,1,999,'2023-08-17 09:47:40.677')
insert into [Admissions].[FurtranParameters](Code,IdentificationType,UserCreation,DateCreation)
values(5,1,999,'2023-08-17 09:47:40.677')
insert into [Admissions].[FurtranParameters](Code,IdentificationType,UserCreation,DateCreation)
values(3,1,999,'2023-08-17 09:47:40.677')
insert into [Admissions].[FurtranParameters](Code,IdentificationType,UserCreation,DateCreation)
values(4,1,999,'2023-08-17 09:47:40.677')
insert into [Admissions].[FurtranParameters](Code,IdentificationType,UserCreation,DateCreation)
values(6,1,999,'2023-08-17 09:47:40.677')
insert into [Admissions].[FurtranParameters](Code,IdentificationType,UserCreation,DateCreation)
values(7,1,999,'2023-08-17 09:47:40.677')
insert into [Admissions].[FurtranParameters](Code,IdentificationType,UserCreation,DateCreation)
values(10,1,999,'2023-08-17 09:47:40.677')
insert into [Admissions].[FurtranParameters](Code,IdentificationType,UserCreation,DateCreation)
values(11,1,999,'2023-08-17 09:47:40.677')
insert into [Admissions].[FurtranParameters](Code,IdentificationType,UserCreation,DateCreation)
values(12,1,999,'2023-08-17 09:47:40.677')
insert into [Admissions].[FurtranParameters](Code,IdentificationType,UserCreation,DateCreation)
values(13,1,999,'2023-08-17 09:47:40.677')
insert into [Admissions].[FurtranParameters](Code,IdentificationType,UserCreation,DateCreation)
values(14,1,999,'2023-08-17 09:47:40.677')
----------------Tipos de identificación de la rejilla conductor.
insert into [Admissions].[FurtranParameters](Code,IdentificationType,UserCreation,DateCreation)
values(1,2,999,'2023-08-17 09:47:40.677')
insert into [Admissions].[FurtranParameters](Code,IdentificationType,UserCreation,DateCreation)
values(2,2,999,'2023-08-17 09:47:40.677')
insert into [Admissions].[FurtranParameters](Code,IdentificationType,UserCreation,DateCreation)
values(5,2,999,'2023-08-17 09:47:40.677')
insert into [Admissions].[FurtranParameters](Code,IdentificationType,UserCreation,DateCreation)
values(10,2,999,'2023-08-17 09:47:40.677')
insert into [Admissions].[FurtranParameters](Code,IdentificationType,UserCreation,DateCreation)
values(12,2,999,'2023-08-17 09:47:40.677')
insert into [Admissions].[FurtranParameters](Code,IdentificationType,UserCreation,DateCreation)
values(13,2,999,'2023-08-17 09:47:40.677')

------------------------------------------------------------------------------------- Sprint Week 51 - 01  2023 -------------------------------------------------------------------------------------------------------
--PBI13970	--parametrización Tipos de género'																					  
--28/12/2023
INSERT INTO [Admissions].[GenderTypes] (Code,[Name],[Status],UserCreation,DateCreation) VALUES ('001', 'Masculino', 1, '999',Common.GETDATE())
INSERT INTO [Admissions].[GenderTypes] (Code,[Name],[Status],UserCreation,DateCreation) VALUES ('002', 'Femenino', 1, '999',Common.GETDATE())
INSERT INTO [Admissions].[GenderTypes] (Code,[Name],[Status],UserCreation,DateCreation) VALUES ('003', 'No binario', 1, '999',Common.GETDATE())
INSERT INTO [Admissions].[GenderTypes] (Code,[Name],[Status],UserCreation,DateCreation) VALUES ('004', 'Transgénero', 1, '999',Common.GETDATE())
INSERT INTO [Admissions].[GenderTypes] (Code,[Name],[Status],UserCreation,DateCreation) VALUES ('005', 'Neutro', 1, '999',Common.GETDATE())

--BUG 17772 COHAN - En nueva versión 24.18.0.7 dificultades en visualización en históricos en revisión por sistemas y examen físico de las HC parametrizables y consu
--Se actualizan las tablas estandar de Examen fisico, antecedentes y revision por sistemas ambulatorias insertando los registros que se guardaron en las tablas que se creaban con anterioridad
--(EXAVALORES+AnioMes, OTVALORES+AnioMes, RSVALORES+AnioMes, ANTVALORES+AnioMes) ((PARA EL BUG MENCIONADO LA TABLA OTVALORES NO PRESENTABA EL ESCENARIO, PERO SE DEJAN LOS SCRIPTS POR SI SE PRESENTA EL CASO))
--ESTOS SCRIPTS SE DEBEN EJECUTAR POR PARTE DE DESARROLLO, YA QUE SE DEBEN CONSULTAR LAS TABLAS DE LAS CUALES SE VA A CARGAR LA INFORMACIÓN A LAS NUEVAS TABLAS GENERALES
--Tabla de Examen físico
insert into EXAVALORES (IDHCHISPACA, IDEXAGRUPO, IDEXAVARIABLE, VALOR, IDITEMLISTA, VALOROPCION) ( 
select IDHCHISPACA, IDEXAGRUPO, IDEXAVARIABLE, VALOR, IDITEMLISTA, VALOROPCION from EXAVALORES202404 A inner join HCHISPACA B ON A.IDHCHISPACa = B. ID 
union
select IDHCHISPACA, IDEXAGRUPO, IDEXAVARIABLE, VALOR, IDITEMLISTA, VALOROPCION from EXAVALORES202403 A inner join HCHISPACA B ON A.IDHCHISPACa = B. ID 
union
select IDHCHISPACA, IDEXAGRUPO, IDEXAVARIABLE, VALOR, IDITEMLISTA, VALOROPCION from EXAVALORES202402 A inner join HCHISPACA B ON A.IDHCHISPACa = B. ID
WHERE B.FECHISPAC > DATEADD(DAY,-90,Convert(DATE, GETDATE())))

--Tabla de Revision por sistemas
insert into RSVALORES (IDHCHISPACA, IDGRUPO, IDRSVARIABLE, VALOR, IDITEMLISTA) ( 
select IDHCHISPACA, IDGRUPO, IDRSVARIABLE, VALOR, IDITEMLISTA from RSVALORES202404 A inner join HCHISPACA B ON A.IDHCHISPACa = B. ID 
union
select IDHCHISPACA, IDGRUPO, IDRSVARIABLE, VALOR, IDITEMLISTA from RSVALORES202403 A inner join HCHISPACA B ON A.IDHCHISPACa = B. ID 
union
select IDHCHISPACA, IDGRUPO, IDRSVARIABLE, VALOR, IDITEMLISTA from RSVALORES202402 A inner join HCHISPACA B ON A.IDHCHISPACa = B. ID
WHERE B.FECHISPAC > DATEADD(DAY,-90,Convert(DATE, GETDATE())))

--Tabla de antecedentes
insert into ANTVALORES (IDHCHISPACA, CODANTECEDENTE, IDANTVARIABLE, VALOR, IDITEMLISTA) ( 
select IDHCHISPACA, CODANTECEDENTE, IDANTVARIABLE, VALOR, IDITEMLISTA from ANTVALORES202404 A inner join HCHISPACA B ON A.IDHCHISPACa = B. ID 
union
select IDHCHISPACA, CODANTECEDente, IDANTVARIABLE, VALOR, IDITEMLISTA from ANTVALORES202403 A inner join HCHISPACA B ON A.IDHCHISPACa = B. ID 
union
select IDHCHISPACA, CODANTECEDENTE, IDANTVARIABLE, VALOR, IDITEMLISTA from ANTVALORES202402 A inner join HCHISPACA B ON A.IDHCHISPACa = B. ID
WHERE B.FECHISPAC > DATEADD(DAY,-90,Convert(DATE, GETDATE())))

--Tabla de Otros
insert into OTVALORES (IDHCHISPACA, IDGRUPO, IDOTVARIABLE, VALOR, IDITEMLISTA) ( 
select IDHCHISPACA, IDGRUPO, IDOTVARIABLE, VALOR, IDITEMLISTA from OTVALORES202404 A inner join HCHISPACA B ON A.IDHCHISPACa = B. ID 
union
select IDHCHISPACA, IDGRUPO, IDOTVARIABLE, VALOR, IDITEMLISTA from OTVALORES202403 A inner join HCHISPACA B ON A.IDHCHISPACa = B. ID 
union
select IDHCHISPACA, IDGRUPO, IDOTVARIABLE, VALOR, IDITEMLISTA from OTVALORES202402 A inner join HCHISPACA B ON A.IDHCHISPACa = B. ID
WHERE B.FECHISPAC > DATEADD(DAY,-90,Convert(DATE, GETDATE())))

---------------------------------------------------------------------------------- Sprint Week 36 - 37 (2024) ---------------------------------------------------------------------------------------------------
--PBI 16336 Creación del Reporte de Inconsistencias (Actualización de datos de Contacto)
--Se añade el reporte para que pueda ser personalizado
Insert into MedicalHistory.ReportListCustomize (ReportClass, ReportName, ReportDescription, ModuleName) values ('rptADInconsistenciasRes_2335_2023', 'Anexo técnico n°1', 'Informe de actualización de datos de contacto', 'Admisiones')
--Se añade el script del reporte "Atencion Incial Urgencias Nuevo" debido a que este no estaba añadido a los scripts 
Insert into MedicalHistory.ReportListCustomize (ReportClass, ReportName, ReportDescription, ModuleName) values ('rptADAtencionIncialUrgenciasRes_2335_2023', 'Anexo técnico n°1', 'Informe de atención de urgencias', 'Admisiones')
--Se añade el script del reporte "Atencion Incial Urgencias Nuevo" debido a que este no estaba añadido a los scripts 
Insert into MedicalHistory.ReportListCustomize (ReportClass, ReportName, ReportDescription, ModuleName) values ('rptADSolicitudAutorizacionServiciosTecnologiasSaludRes_2335_2023', 'Anexo técnico n°1', 'Informe de solicitud de autorización de servicios y tecnologías en salud', 'Admisiones')

---------------------------------------------------------------------------------- Sprint Week 38 - 39 (2024) ---------------------------------------------------------------------------------------------------
Insert into MedicalHistory.ReportListCustomize (ReportClass, ReportName, ReportDescription, ModuleName) values ('rptADFurtran', 'Reporte de FURTRAN', 'Formulario único de reclamación de gastos de transporte y movilización de victimas', 'Admisiones')

---------------------------------------------------------------------------------- Sprint Week 6 - 7 (2025) ---------------------------------------------------------------------------------------------------
-- PBI 22892 - 1. Habilitación y modificaciones en el parametrizador de formatos clínicos intrahospitalarios EHR Colombia
-- Se adicionan páginas de salud visual y de valoracion materno perinatal a la tabla de las páginas habilitadas para parametrización de formatos intrahospitalarios
INSERT INTO [ClinicalParameters].[HistoryPages] (Id,Code,[Name],[Status],CreationDate,CreationUser) VALUES (12,'11', 'Valoración clínica de optometría', 1, '2025-02-12 17:18:00.000', '551')
INSERT INTO [ClinicalParameters].[HistoryPages] (Id,Code,[Name],[Status],CreationDate,CreationUser) VALUES (13,'12', 'Valoración maternoperinatal', 1, '2025-02-12 17:18:00.000', '551')

---------------------------------------------------------------------------------- Sprint Week 10 - 11 (2025) ---------------------------------------------------------------------------------------------------
--PBI:24866
Insert into MedicalHistory.ReportListCustomize (ReportClass, ReportName, ReportDescription, ModuleName) values ('rptAGECitasPeriodicas', 'Planificación de citas periódicas', 'Este reporte proporciona al usuario la información sobre las citas asignadas en frecuencia', 'Agendamiento')

---------------------------------------------------------------------------------- Sprint Week 14 - 15 (2025) ---------------------------------------------------------------------------------------------------
--PBI: 24638 INT - Listar reporte "Citas de apoyo diagnóstico y tratamientos especiales" en Formulario de Modificación de Reportes
Insert into MedicalHistory.ReportListCustomize (ReportClass, ReportName, ReportDescription, ModuleName) values ('rptAGEListadoCitasApoyoDiagnostico', 'Reporte de Citas de Apoyo Diagnóstico', 'Este reporte muestra el listado de citas de apoyo diagnóstico y citas especiales', 'Agendamiento')

---------------------------------------------------------------------------------- Sprint Week 32 - 33 (2025) ---------------------------------------------------------------------------------------------------
-- PBI 29344 4.1. Incluir reporte "Sticker de leche de fórmula" en el formulario "Modificación de reportes"
IF NOT EXISTS (
    SELECT 1
    FROM MedicalHistory.ReportListCustomize
    WHERE ReportClass = 'rptCHStickerLactario'
)
BEGIN
    INSERT INTO MedicalHistory.ReportListCustomize
        (ReportClass, ReportName, ReportDescription, ModuleName)
    VALUES
        (
            'rptCHStickerLactario',
            'Sticker de leche de fórmula (Lactario)',
            'Este reporte muestra los stickers de la leche de fórmula preparada en el formulario Dashboard Lactario (Gestión fórmulas lácteas)',
            'Hospitalización'
        );
END
GO
---------------------------------------------------------------------------------- Sprint Week 34 - 35 (2025) ---------------------------------------------------------------------------------------------------
IF NOT EXISTS (
    SELECT 1
    FROM MedicalHistory.ReportListCustomize
    WHERE ReportClass = 'rptCHStickerLecheMaterna'
)
BEGIN
    INSERT INTO MedicalHistory.ReportListCustomize
        (ReportClass, ReportName, ReportDescription, ModuleName)
    VALUES
        (
            'rptCHStickerLecheMaterna',
            'Sticker de leche materna (Lactario)',
            'Este reporte muestra los stickers de la leche materna gestionada en el formulario Dashboard Lactario (Gestión leche materna)',
            'Hospitalización'
        );
END
GO

---------------------------------------------------------------------------------- Sprint Week 44 - 45 (2025) ---------------------------------------------------------------------------------------------------


---------------------------------------------------------------------------------- Sprint Week 46 - 47 (2025) ---------------------------------------------------------------------------------------------------


---------------------------------------------------------------------------------- Sprint Week 48 - 49 (2025) ---------------------------------------------------------------------------------------------------

-- PBI-32350  1. Crear Maestro Tipo de riesgos

INSERT INTO Admissions.RisksType
(
    Code, Name, Status, UserCreation, DateCreation, UserModification, DateModification
)
VALUES
('001', 'Químicos',       1, '551', Common.GETDATE(), NULL, NULL),
('002', 'Físicos',        1, '551', Common.GETDATE(), NULL, NULL),
('003', 'Biomecánicos',   1, '551', Common.GETDATE(), NULL, NULL),
('004', 'Psicosociales',  1, '551', Common.GETDATE(), NULL, NULL),
('005', 'Biológicos',     1, '551', Common.GETDATE(), NULL, NULL),
('006', 'Otro',           1, '551', Common.GETDATE(), NULL, NULL);

-- PBI-32355 1. Crear Maestro Tipos de alergia

INSERT INTO Admissions.AllergyType
(
    Code, Name, Status, UserCreation, DateCreation, UserModification, DateModification
)
VALUES
('001', 'Medicamento',                          1, '551', Common.GETDATE(), NULL, NULL),
('002', 'Alimento',                             1, '551', Common.GETDATE(), NULL, NULL),
('003', 'Sustancia del ambiente',               1, '551', Common.GETDATE(), NULL, NULL),
('004', 'Sustancia que entran en contacto con la piel', 1, '551', Common.GETDATE(), NULL, NULL),
('005', 'Picadura de insectos',                 1, '551', Common.GETDATE(), NULL, NULL),
('006', 'Otra',                                 1, '551', Common.GETDATE(), NULL, NULL);

---------------------------------------------------------------------------------- Sprint Week 50 - 51 (2025) ---------------------------------------------------------------------------------------------------

--BUG-33024 HSJ -  VALIDACION QUE IMPIDE VISULIZAR REPORTE POR PAGE: Consultar / Imprimir - Seleccionar ordenes medicas
--Creacion de DCI para medicamentos adicionales

IF NOT EXISTS (SELECT 1 FROM IHDCIMEDI WHERE CODDCIMED = 'MED-ADICIONAL')
BEGIN
    INSERT INTO IHDCIMEDI (CODDCIMED, DESDCIMED)
    VALUES ('MED-ADICIONAL', 'DCI MEDICAMENTOS ADIONALES');
END;

IF NOT EXISTS (SELECT 1 FROM Inventory.DCI WHERE Code = 'MED-ADICIONAL')
BEGIN
    INSERT INTO Inventory.DCI (Code, Name, DCICrystal, Status, CreationUser, CreationDate, Combined, TypeWarning)
    VALUES ('MED-ADICIONAL','DCI MEDICAMENTOS ADIONALES', 'MED-ADICIONAL', 1, 551, Common.GETDATE(), 0, 0);
END;

---------------------------------------------------------------  Product Backlog Item Sprint Week 4 - 5 (2026) ---------------------------------------------------------------  

-- PBI 34104. Integrar la Estratificacion Socioeconómica parametrizable en el formulario Pacientes
-- Creación de los 7 tipos de Estratos

INSERT INTO Admissions.Adstratification
(
    Code, Description, Status, CreationDate, CreationUser, ModificationDate, ModificationUSER
)
VALUES
(1, 'Estrato 1', 1, Common.GETDATE(), '551', NULL, NULL),
(2, 'Estrato 2', 1, Common.GETDATE(), '551', NULL, NULL),
(3, 'Estrato 3', 1, Common.GETDATE(), '551', NULL, NULL),
(4, 'Estrato 4', 1, Common.GETDATE(), '551', NULL, NULL),
(5, 'Estrato 5', 1, Common.GETDATE(), '551', NULL, NULL),
(6, 'Estrato 6', 1, Common.GETDATE(), '551', NULL, NULL),
(7, 'Estrato 7', 1, Common.GETDATE(), '551', NULL, NULL);

---------------------------------------------------------------------------------- Sprint Week 24 - 25 (2026) ---------------------------------------------------------------------------------------------------
-- PBI 32756 Creacion del formulario Tipo de usuario

INSERT INTO Admissions.UsersType
    (UserTypeCode, [Name],                                                        ReportCode, PatientTypeCode, AffiliateTypeCode, EntityTypeId, IsActive)
VALUES
    ('01',  'Contributivo Cotizante',                                              '01',          1,               1,                 NULL,         1),
    ('02',  'Contributivo Beneficiario',                                           '02',          1,               2,                 NULL,         1),
    ('03',  'Contributivo Adicional',                                              '03',          1,               3,                 NULL,         1),
    ('04',  'Subsidiado',                                                          '04',          2,               NULL,              NULL,         1),
    ('05',  'No afiliado',                                                         '05',          3,               NULL,              NULL,         1),
    ('06',  'Particular',                                                          '06',          4,               NULL,              NULL,         1),
    ('07',  'Especial o Excepción Cotizante',                                      '07',          9,               1,                 NULL,         1),
    ('08',  'Especial o Excepción Beneficiario',                                   '08',          9,               2,                 NULL,         1),
    ('09',  'Especial o Excepción Adicional',                                      '09',          9,               3,                 NULL,         1),
    ('10', 'Personas privadas de la libertad a cargo del Fondo Nacional de Salud', '10',         10,              NULL,              NULL,         1),
    ('11', 'Tomador / Amparado ARL',                                               '11',         11,              NULL,              NULL,         1),
    ('12', 'Tomador / Amparado SOAT',                                              '12',         12,              NULL,              NULL,         1),
    ('13', 'Tomador / Amparado planes voluntarios de salud',                       '13',         13,              NULL,              NULL,         1),
    ('14', 'Especial o Excepción no cotizante Ley 352 de 1997',                    '14',         14,              NULL,              NULL,         1)



    ---------------------Product Backlog Item 35173: 1. Crear formulario Trazabilidad validación IHCE-RDA-------------------------------------
    Insert into SEGmenusu 
    values (89046, 20,1)

    Insert into SEGpermif 
    values (89046, 'Trazabilidad validación IHCE- RDA', 0, 1,1,0,0,0,0,0,0,0,0,0,1,0)

    IF NOT EXISTS (
        SELECT 1 
        FROM Security.Form 
        WHERE Id = 89046
    )
    BEGIN
        INSERT INTO Security.Form (
            Id, Name, PrintEvents, HasSequence, IsNativeForm, 
            HasForm, ClassName, AssemblyName, HandlesMassiveConfirm, 
            SequenceModule, State
        )
        VALUES (
            89046, 
            'Trazabilidad validación IHCE- RDA', 
            'SUCA', 
            0, 
            0, 
            1, 
            'FrmValidationTraceability', 
            'Indigo-Calidad.dll', 
            0, 
            NULL, 
            1
        );
    END


    -- Acción 3
    IF NOT EXISTS (
        SELECT 1 FROM Security.FormAction 
        WHERE IdForm = 89046 AND IdAction = 3
    )
    BEGIN
        INSERT INTO Security.FormAction (IdForm, IdAction)
        VALUES (89046, 3);
    END

    -- Acción 40
    IF NOT EXISTS (
        SELECT 1 FROM Security.FormAction 
        WHERE IdForm = 89046 AND IdAction = 40
    )
    BEGIN
        INSERT INTO Security.FormAction (IdForm, IdAction)
        VALUES (89046, 40);
    END

    -- Acción 41
    IF NOT EXISTS (
        SELECT 1 FROM Security.FormAction 
        WHERE IdForm = 89046 AND IdAction = 41
    )
    BEGIN
        INSERT INTO Security.FormAction (IdForm, IdAction)
        VALUES (89046, 41);
    END


    IF NOT EXISTS (
        SELECT 1 FROM Security.ModuleForm 
        WHERE IdForm = 89046 
    )
    BEGIN
        INSERT INTO Security.ModuleForm (IdModule, IdTitle, IdForm, FormOrder)
        VALUES (721, 4, 89046, 4);
    END

    ---------------------------------------------------------------------------------- Sprint Week 36 - 37 (2026) ---------------------------------------------------------------------------------------------------
    -- PBI 39601 Habilitar opción Imprimir y funcionalidad en el formulario FUR
    INSERT INTO MedicalHistory.ReportListCustomize (ReportClass, ReportName, ReportDescription, ModuleName) VALUES ( 'rptADFur', 'Reporte FUR', 'Formulario único de reclamaciones', 'Admisiones')