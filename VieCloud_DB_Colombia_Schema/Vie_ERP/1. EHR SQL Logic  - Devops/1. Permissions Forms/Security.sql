-- Actualiza el nombre de la clase para el formulario de Parámetros de Urgencias.
Update Security.Form set className = 'Parámetros Urgencias' where Id = '1654' and ClassName = 'frmADParametros'

-- Define el formulario 'Tipos de Catéteres', lo asocia a un módulo y le asigna acciones básicas (CRUD).
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88007)
INSERT INTO Security.Form VALUES (88007,'Tipos de Catéteres','SUCA',0,0,1,'FrmHCTipoCateteres','Indigo.eHistorias.dll',0,'',1) 
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 30 AND IdTitle = 92 AND IdForm = 88007)
INSERT INTO Security.ModuleForm values (30,92,88007,14)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88007 AND IdAction = 2)
INSERT INTO Security.FormAction values (88007,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88007 AND IdAction = 3)
INSERT INTO Security.FormAction values (88007,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88007 AND IdAction = 40)
INSERT INTO Security.FormAction values (88007,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88007 AND IdAction = 41)
INSERT INTO Security.FormAction values (88007,41)

-- Define el formulario maestro 'Factores de riesgo', lo asocia a un módulo y le asigna acciones básicas.
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88008)
INSERT INTO Security.Form VALUES (88008,'Factores de riesgo','SUCA',0,0,1,'FrmHCFactoresRiesgo','Indigo-Calidad.dll',0,'',1) 
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 30 AND IdTitle = 96 AND IdForm = 88008)
INSERT INTO Security.ModuleForm values (30,96,88008,7)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88008 AND IdAction = 2)
INSERT INTO Security.FormAction values (88008,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88008 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88008,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88008 AND IdAction = 40)
INSERT INTO Security.FormAction values (88008,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88008 AND IdAction = 41)
INSERT INTO Security.FormAction VALUES (88008,41)

-- Define el formulario para el 'Reporte de Factores de Riesgo', lo asocia a múltiples módulos y asigna acciones.
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88009)
INSERT INTO Security.Form VALUES (88009,'Reporte de factores de riesgo','SUCA',0,0,1,'FrmCALRptFactoresRiesgo','Indigo-Calidad.dll',0,'',1) 

-- Define el formulario para el 'Archivo Atencion Urgencias', lo asocia a múltiples módulos y asigna acciones.
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 89040)
INSERT INTO Security.Form VALUES (89040, 'Archivo Plano de Atención Urgencias', 'SUCA', 0, 0, 1, 'frmAtencionUrgencias', 'Indigo.eHistorias.dll', 0, '', 1);
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 89040 AND IdAction = 2)
INSERT INTO Security.FormAction VALUES (89040, 2);   -- Guardar
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 89040 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (89040, 3);   -- Actualizar
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 89040 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (89040, 40);  -- Consultar
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 89040 AND IdAction = 41)
INSERT INTO Security.FormAction VALUES (89040, 41);  -- Visible

IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 12 AND IdTitle = 4 AND IdForm = 89040)
INSERT INTO Security.ModuleForm VALUES (12, 4, 89040, 9); 
-- modulo
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 2 AND IdTitle = 30 AND IdForm = 88009)
INSERT INTO Security.ModuleForm VALUES (2,30,88009,14)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 4 AND IdTitle = 30 AND IdForm = 88009)
INSERT INTO Security.ModuleForm VALUES (4,30,88009,14)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 5 AND IdTitle = 30 AND IdForm = 88009)
INSERT INTO Security.ModuleForm values (5,30,88009,14)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 10 AND IdTitle = 30 AND IdForm = 88009)
INSERT INTO Security.ModuleForm values (10,30,88009,14)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 11 AND IdTitle = 80 AND IdForm = 88009)
INSERT INTO Security.ModuleForm values (11,80,88009,14)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 12 AND IdTitle = 30 AND IdForm = 88009)
INSERT INTO Security.ModuleForm VALUES (12,30,88009,14)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 13 AND IdTitle = 30 AND IdForm = 88009)
INSERT INTO Security.ModuleForm VALUES (13,30,88009,14)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 14 AND IdTitle = 30 AND IdForm = 88009)
INSERT INTO Security.ModuleForm VALUES (14,30,88009,14)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 16 AND IdTitle = 30 AND IdForm = 88009)
INSERT INTO Security.ModuleForm VALUES (16,30,88009,14)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 17 AND IdTitle = 30 AND IdForm = 88009)
INSERT INTO Security.ModuleForm VALUES (17,30,88009,14)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 18 AND IdTitle = 30 AND IdForm = 88009)
INSERT INTO Security.ModuleForm VALUES (18,30,88009,14)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 19 AND IdTitle = 30 AND IdForm = 88009)
INSERT INTO Security.ModuleForm VALUES (19,30,88009,14)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 20 AND IdTitle = 30 AND IdForm = 88009)
INSERT INTO Security.ModuleForm VALUES (20,30,88009,14)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 21 AND IdTitle = 30 AND IdForm = 88009)
INSERT INTO Security.ModuleForm VALUES (21,30,88009,14)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 22 AND IdTitle = 30 AND IdForm = 88009)
INSERT INTO Security.ModuleForm VALUES (22,30,88009,14)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 23 AND IdTitle = 30 AND IdForm = 88009)
INSERT INTO Security.ModuleForm VALUES (23,30,88009,14)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 24 AND IdTitle = 30 AND IdForm = 88009)
INSERT INTO Security.ModuleForm VALUES (24,30,88009,14)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 25 AND IdTitle = 30 AND IdForm = 88009)
INSERT INTO Security.ModuleForm VALUES (25,30,88009,14)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 26 AND IdTitle = 30 AND IdForm = 88009)
INSERT INTO Security.ModuleForm VALUES (26,30,88009,14)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 29 AND IdTitle = 30 AND IdForm = 88009)
INSERT INTO Security.ModuleForm VALUES (29,30,88009,14)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 58 AND IdTitle = 30 AND IdForm = 88009)
INSERT INTO Security.ModuleForm VALUES (58,30,88009,14)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 721 AND IdTitle = 30 AND IdForm = 88009)
INSERT INTO Security.ModuleForm VALUES (721,30,88009,14)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 726 AND IdTitle = 30 AND IdForm = 88009)
INSERT INTO Security.ModuleForm VALUES (726,30,88009,14)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 727 AND IdTitle = 30 AND IdForm = 88009)
INSERT INTO Security.ModuleForm VALUES (727,30,88009,14)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88009 AND IdAction = 2)
INSERT INTO Security.FormAction values (88009,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88009 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88009,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88009 AND IdAction = 41)
INSERT INTO Security.FormAction VALUES (88009,41)

-- Define el formulario maestro 'Tipos de Bombas de Infusión' y sus configuraciones de seguridad.
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88014)
insert into Security.Form  values (88014,'Tipos de Bombas de Infusión','SUCA',0,0,1,'frmTiposBombasInfusion','Indigo.eHistorias.dll',0,'',1)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 30 AND IdTitle = 92 AND IdForm = 88014)
insert into Security.ModuleForm  values (30,92,88014,15)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88014 AND IdAction = 2)
insert into Security.FormAction  values (88014,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88014 AND IdAction = 3)
insert into Security.FormAction  values (88014,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88014 AND IdAction = 40)
INSERT INTO Security.FormAction  VALUES (88014,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88014 AND IdAction = 41)
insert into Security.FormAction  values (88014,41)

-- Define el 'Dashboard del Instrumentador Quirúrgico' y sus permisos.
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88010)
INSERT INTO Security.Form VALUES (88010,'Dashboard instrumentador quirúrgico','SUCA',0,0,1,'frmHCDashboardInstrumentadorQuirurgico','Indigo.eHistorias.dll',0,'',1) 
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 16 AND IdTitle = 30 AND IdForm = 88010)
INSERT INTO Security.ModuleForm values (16,30,88010,14)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88010 AND IdAction = 2)
INSERT INTO Security.FormAction values (88010,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88010 AND IdAction = 3)
INSERT INTO Security.FormAction values (88010,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88010 AND IdAction = 41)
INSERT INTO Security.FormAction values (88010,41)

-- Define el formulario maestro 'Causas de atención' y sus permisos.
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88011)
INSERT INTO Security.Form VALUES (88011,'Causas de atención','SUCA',0,0,1,'FrmADCausasdeatencion','Indigo.Admisiones.dll',0,'',1) 
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 58 AND IdTitle = 29 AND IdForm = 88011)
INSERT INTO Security.ModuleForm values (58,29,88011,20)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88011 AND IdAction = 2)
INSERT INTO Security.FormAction values (88011,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88011 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88011,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88011 AND IdAction = 40)
INSERT INTO Security.FormAction values (88011,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88011 AND IdAction = 41)
INSERT INTO Security.FormAction VALUES (88011,41)

-- Define el formulario maestro 'Finalidades tecnología de salud' y sus permisos.
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88012)
INSERT INTO Security.Form VALUES (88012,'Finalidades tecnología de salud','SUCA',0,0,1,'FrmAdFinalidadesTecnologiaSalud','Indigo.Admisiones.dll',0,'',1) 
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 58 AND IdTitle = 29 AND IdForm = 88012)
INSERT INTO Security.ModuleForm values (58,29,88012,21)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88012 AND IdAction = 2)
INSERT INTO Security.FormAction values (88012,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88012 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88012,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88012 AND IdAction = 40)
INSERT INTO Security.FormAction values (88012,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88012 AND IdAction = 41)
INSERT INTO Security.FormAction VALUES (88012,41)

-- Define el formulario 'Reporte de Triage' y sus permisos.
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88013)
INSERT INTO Security.Form VALUES (88013,'Reporte triage','SUCA',0,0,1,'frmReportetriage','Indigo.Admisiones.dll',0,'',1) 
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 12 AND IdTitle = 4 AND IdForm = 88013)
INSERT INTO Security.ModuleForm values (12,4,88013,7)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88013 AND IdAction = 2)
INSERT INTO Security.FormAction values (88013,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88013 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88013,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88013 AND IdAction = 41)
INSERT INTO Security.FormAction VALUES (88013,41)

-- Define el formulario para 'Parametrizar Monitoreo Hemodinámico'.
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88830)
INSERT INTO Security.Form VALUES (88830,'Parametrizar monitoreo hemodinámico','SUCA',0,0,1,'FrmHCParametrizacionMonitoreoHemodinamico','Indigo.eHistorias.dll',0,'',1) 
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 30 AND IdTitle = 40 AND IdForm = 88830)
INSERT INTO Security.ModuleForm values (30,40,88830,20)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88830 AND IdAction = 2)
INSERT INTO Security.FormAction values (88830,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88830 AND IdAction = 3)
INSERT INTO Security.FormAction values (88830,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88830 AND IdAction = 40)
INSERT INTO Security.FormAction values (88830,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88830 AND IdAction = 41)
INSERT INTO Security.FormAction values (88830,41)

-- Define el formulario maestro 'Vías de Ingreso a Servicios de Salud'.
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88827)
INSERT INTO Security.Form VALUES (88827,'Vías ingreso servicios salud','SUCA',0,0,1,'FrmADViasIngresoServiciosSalud','Indigo.Admisiones.dll',0,'',1) 
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 58 AND IdTitle = 29 AND IdForm = 88827)
INSERT INTO Security.ModuleForm values (58,29,88827,22)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88827 AND IdAction = 2)
INSERT INTO Security.FormAction values (88827,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88827 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88827,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88827 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88827,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88827 AND IdAction = 41)
INSERT INTO Security.FormAction VALUES (88827,41)

-- Define el formulario para 'Formatos de Documentación Clínica Intrahospitalarios'.
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88032)
INSERT INTO Security.Form VALUES (88032,'Formatos de documentación clínica intrahospitalarios','SUCA',0,0,1,'FrmHCParametrizarFormatosHosptitalarios','Indigo.eHistorias.dll',0,'',1) 
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 30 AND IdTitle = 40 AND IdForm = 88032)
INSERT INTO Security.ModuleForm values (30,40,88032,6)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88032 AND IdAction = 2)
INSERT INTO Security.FormAction values (88032,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88032 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88032,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88032 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88032,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88032 AND IdAction = 41)
INSERT INTO Security.FormAction VALUES (88032,41)

-- Define y actualiza el formulario para 'Parametrizar Formatos de Educación y Encuestas'.
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88005)
INSERT INTO Security.Form VALUES (88005,'Parametrizar formatos y encuestas','SUCA',0,0,1,'FrmHCFormatosdeeducacionalpaciente','Indigo.eHistorias.dll',0,'',1) 
update Security.Form set Name = 'Parametrizar formatos de educación y encuestas' where id = 88005
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 30 AND IdTitle = 40 AND IdForm = 88005)
INSERT INTO Security.ModuleForm values (30,40,88005,21)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 7 AND IdTitle = 78 AND IdForm = 88005)
INSERT INTO Security.ModuleForm values (7,78,88005,12)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88005 AND IdAction = 2)
INSERT INTO Security.FormAction values (88005,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88005 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88005,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88005 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88005,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88005 AND IdAction = 41)
INSERT INTO Security.FormAction VALUES (88005,41)

-- Define el formulario para la 'Escala CAM' (Confusion Assessment Method).
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88044)
INSERT INTO Security.Form VALUES (88044,'Escala CAM','SUCA',0,0,1,'FrmHCEscala_CAM','Indigo.Emergentes.dll',0,'',1) 
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88044 AND IdAction = 2)
INSERT INTO Security.FormAction values (88044,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88044 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88044,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88044 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88044,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88044 AND IdAction = 23)
INSERT INTO Security.FormAction VALUES (88044,23)

-- Define el formulario para la 'Escala de Gijón' (Valoración sociofamiliar).
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88016)
INSERT INTO Security.Form VALUES (88016,'Escala Gijón','SUCA',0,0,1,'FmrHCEscala_GIJON','Indigo.Emergentes.dll',0,'',1) 
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88016 AND IdAction = 2)
INSERT INTO Security.FormAction values (88016,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88016 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88016,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88016 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88016,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88016 AND IdAction = 23)
INSERT INTO Security.FormAction VALUES (88016,23)

-- Define el formulario para la 'Escala de Valoración del Riesgo Farmacológico'.
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88019)
INSERT INTO Security.Form VALUES (88019,'Escala de valoración del riesgo farmacológico','SUCA',0,0,1,'FrmHCEscalaRiesgoFarmacologico','Indigo.Emergentes.dll',0,'',1) 
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88019 AND IdAction = 2)
INSERT INTO Security.FormAction values (88019,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88019 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88019,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88019 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88019,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88019 AND IdAction = 23)
INSERT INTO Security.FormAction VALUES (88019,23)

-- Define el formulario para la 'Escala de Valoración del Riesgo de Infección'.
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88017)
INSERT INTO Security.Form VALUES (88017,'Escala Valoración del riesgo de infección','SUCA',0,0,1,'FrmHCEscalaValoracionRiesgoInfeccion','Indigo.Emergentes.dll',0,'',1) 
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88017 AND IdAction = 2)
INSERT INTO Security.FormAction values (88017,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88017 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88017,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88017 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88017,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88017 AND IdAction = 23)
INSERT INTO Security.FormAction VALUES (88017,23)

-- Define el formulario para la 'Escala Numérica del Dolor (NRS)'.
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88018)
INSERT INTO Security.Form VALUES (88018,'Escala numérica del dolor (NRS)','SUCA',0,0,1,'FrmHCEscalaNRS','Indigo.Emergentes.dll',0,'',1) 
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88018 AND IdAction = 2)
INSERT INTO Security.FormAction values (88018,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88018 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88018,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88018 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88018,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88018 AND IdAction = 23)
INSERT INTO Security.FormAction VALUES (88018,23)

-- Define el formulario para la 'Escala de Gijón (Versión Original)'.
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88020)
INSERT INTO Security.Form VALUES (88020,'Escala Gijón original','SUCA',0,0,1,'FrmHCEscala_GIJONOriginal','Indigo.Emergentes.dll',0,'',1) 
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88020 AND IdAction = 2)
INSERT INTO Security.FormAction values (88020,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88020 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88020,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88020 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88020,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88020 AND IdAction = 23)
INSERT INTO Security.FormAction VALUES (88020,23)

-- Define el formulario para la 'Escala de Valoración del Riesgo Psicosocial (Conducta Suicida)'.
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88024)
INSERT INTO Security.Form VALUES (88024,'Escala de valoración riesgo psicosocial (conducta suicida)','SUCA',0,0,1,'FmrHCEscala_Valoracion_riesgo_psicosocial','Indigo.Emergentes.dll',0,'',1) 
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88024 AND IdAction = 2)
INSERT INTO Security.FormAction values (88024,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88024 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88024,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88024 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88024,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88024 AND IdAction = 23)
INSERT INTO Security.FormAction VALUES (88024,23)	

-- Define el formulario para la 'Escala Obstétrica de Alerta Temprana'.
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88021)
INSERT INTO Security.Form VALUES (88021,'Escala obstétrica de alerta temprana','SUCA',0,0,1,'frmHCEscalaObstétricaAlertaTemprana','Indigo.Emergentes.dll',0,'',1)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88021 AND IdAction = 2)
INSERT INTO Security.FormAction values (88021,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88021 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88021,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88021 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88021,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88021 AND IdAction = 23)
INSERT INTO Security.FormAction VALUES (88021,23)

-- Define el formulario para la 'Escala MPEWS' (Sistema de Alerta Temprana Pediátrica Modificado).
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88022)
INSERT INTO Security.Form VALUES (88022,'Escala MPEWS','SUCA',0,0,1,'frmHCEscalaMPEWS','Indigo.Emergentes.dll',0,'',1) 
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88022 AND IdAction = 2)
INSERT INTO Security.FormAction values (88022,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88022 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88022,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88022 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88022,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88022 AND IdAction = 23)
INSERT INTO Security.FormAction VALUES (88022,23)

-- Define el formulario para la 'Escala BPEWS' (Sistema de Alerta Temprana Pediátrica al Lado de la Cama).
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88022)
INSERT INTO Security.Form VALUES (88022,'Escala BPEWS','SUCA',0,0,1,'frmHCEscala_BPEWS','Indigo.Emergentes.dll',0,'',1) 
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88023 AND IdAction = 2)
INSERT INTO Security.FormAction values (88023,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88023 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88023,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88023 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88023,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88023 AND IdAction = 23)
INSERT INTO Security.FormAction VALUES (88023,23)

-- Define el formulario para la 'Escala de Eventos Tromboembólicos Venosos'.
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88025)
INSERT INTO Security.Form VALUES (88025,'Escala eventos tromboembólicos venosos','SUCA',0,0,1,'FrmHCEscalaEventosTromboembolicosVenosos','Indigo.Emergentes.dll',0,'',1) 
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88025 AND IdAction = 2)
INSERT INTO Security.FormAction values (88025,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88025 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88025,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88025 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88025,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88025 AND IdAction = 23)
INSERT INTO Security.FormAction VALUES (88025,23)

-- Define el formulario para administrar 'Grupos de Listas de Chequeo'.
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88824)
INSERT INTO Security.Form VALUES('88824','Grupo Lista Chequeo','SUCA','0','0','1','FrmHCGruposCH','Indigo.eHistorias.dll','0','',1)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88824 AND IdAction = 2)
INSERT INTO Security.FormAction VALUES('88824',2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88824 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES('88824',3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88824 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES('88824',40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88824 AND IdAction = 41)
INSERT INTO Security.FormAction VALUES('88824',41)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 100 AND IdTitle = 5 AND IdForm = 88824)
INSERT INTO Security.ModuleForm VALUES('100',5,'88824',1)

-- Define el formulario para administrar 'Variables de Listas de Chequeo'.
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88825)
INSERT INTO Security.Form VALUES('88825','Variables Lista de Chequeo','SUCA','0','0','1','FrmHCVariablesCH','Indigo.eHistorias.dll','0','',1)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88825 AND IdAction = 1)
INSERT INTO Security.FormAction VALUES('88825',1)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88825 AND IdAction = 2)
INSERT INTO Security.FormAction VALUES('88825',2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88825 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES('88825',3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88825 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES('88825',40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88825 AND IdAction = 41)
INSERT INTO Security.FormAction VALUES('88825',41)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 100 AND IdTitle = 5 AND IdForm = 88825)
INSERT INTO Security.ModuleForm VALUES('100',5,'88825',1)

-- Define el formulario para 'Bitácoras de Seguimiento'.
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88829)
INSERT INTO Security.Form VALUES (88829,'Bitácoras de seguimiento','SUCA',0,0,1,'FrmRCBitacoraSeguimiento','Indigo.eHistorias.dll',0,'',1)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88829 AND IdAction = 2)
INSERT INTO Security.FormAction values (88829,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88829 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88829,40)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 12 AND IdTitle = 4 AND IdForm = 88829)
INSERT INTO Security.ModuleForm values (12,4,88829,7)

-- Define el formulario para la 'Escala de Bishop' (Inducción del parto).
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88026)
INSERT INTO Security.Form VALUES (88026,'Escala de Bishop','SUCA',0,0,1,'FrmHCEscalaBishop','Indigo.Emergentes.dll',0,'',1)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88026 AND IdAction = 2)
INSERT INTO Security.FormAction values (88026,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88026 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88026,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88026 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88026,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88026 AND IdAction = 23)
INSERT INTO Security.FormAction VALUES (88026,23)    
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 100 AND IdTitle = 5 AND IdForm = 88026)
insert into security.ModuleForm values (100,5,88026,1)

-- Define un permiso especial para agregar medicamentos adicionales.
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88836)
INSERT INTO Security.Form VALUES (88836,'Permiso medicamentos adicionales','SUCA',0,0,1,null,null,0,'',1)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88836 AND IdAction = 2)
INSERT INTO Security.FormAction values (88836,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88836 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88836,3)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 100 AND IdTitle = 5 AND IdForm = 88836)
INSERT INTO Security.ModuleForm values (100,5,88836,1)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 7 AND IdTitle = 73 AND IdForm = 88836)
INSERT INTO Security.ModuleForm values (7,73,88836,1)

-- Define el formulario de 'Parámetros FURIPS' (Formulario Único de Reclamación).
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88027)
INSERT INTO Security.Form VALUES (88027,'Parámetros FURIPS','SUCA',0,0,1,'frmADparametrosFURIPS','Indigo.Admisiones.dll',0,'',1)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88027 AND IdAction = 2)
INSERT INTO Security.FormAction values (88027,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88027 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88027,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88027 AND IdAction = 41)
INSERT INTO Security.FormAction VALUES (88027,41)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88027 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88027,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88027 AND IdAction = 19)
INSERT INTO Security.FormAction VALUES (88027,19)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 58 AND IdTitle = 40 AND IdForm = 88027)
INSERT INTO Security.ModuleForm values (58,40,88027,4)

-- Define el formulario maestro 'Modalidades de Atención'.
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88028)
INSERT INTO Security.Form VALUES (88028,'Modalidades de atención','SUCA',0,0,1,'frmADModalidadesAtencion','Indigo.Admisiones.dll',0,'',1) 
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88028 AND IdAction = 2)
INSERT INTO Security.FormAction values (88028,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88028 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88028,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88028 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88028,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88028 AND IdAction = 41)
INSERT INTO Security.FormAction VALUES (88028,41)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 58 AND IdTitle = 29 AND IdForm = 88028)
INSERT INTO Security.ModuleForm values (58,29,88028,23)

-- Define el formulario para la 'Escala OFRAS' (Riesgo de caídas en obstetricia).
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88046)
INSERT INTO Security.Form VALUES (88046,'Escala OFRAS','SUCA',0,0,1,'FrmHCEscalaOfras','Indigo.Emergentes.dll',0,'',1) 
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88046 AND IdAction = 2)
INSERT INTO Security.FormAction values (88046,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88046 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88046,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88046 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88046,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88046 AND IdAction = 23)
INSERT INTO Security.FormAction VALUES (88046,23)

-- Define un permiso para desconfirmar un FURIPS.
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88029)
INSERT INTO Security.Form VALUES (88029,'Permiso desconfirmar FURIPS','SUCA',0,0,1,'frmADFurips','Indigo.Admisiones.dll',0,'',1) 
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88029 AND IdAction = 2)
INSERT INTO Security.FormAction values (88029,2)	
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 100 AND IdTitle = 5 AND IdForm = 88029)
INSERT INTO Security.ModuleForm values (100,5,88029,1)

-- Define el formulario para la 'Escala Finnegan' (Síndrome de abstinencia neonatal).
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88837)
INSERT INTO Security.Form VALUES (88837,'Escala Finnegan','SUCA',0,0,1,'FrmHCEscalaFinnegan','Indigo.Emergentes.dll',0,'',1)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88837 AND IdAction = 2)
INSERT INTO Security.FormAction values (88837,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88837 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88837,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88837 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88837,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88837 AND IdAction = 23)
INSERT INTO Security.FormAction VALUES (88837,23) 
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 100 AND IdTitle = 5 AND IdForm = 88837)
insert into security.ModuleForm values (100,5,88837,1)

-- Define el formulario para la 'Escala de Clasificación de Choque y Evaluación de la Respuesta'.
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88838)
INSERT INTO Security.Form VALUES (88838,'Escala de clasificación de choque','SUCA',0,0,1,'frmHCEscaladeClasificaciondeChoqueyEvaluaciondelaRespuesta','Indigo.Emergentes.dll',0,'',1)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88838 AND IdAction = 2)
INSERT INTO Security.FormAction values (88838,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88838 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88838,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88838 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88838,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88838 AND IdAction = 23)
INSERT INTO Security.FormAction VALUES (88838,23)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 100 AND IdTitle = 5 AND IdForm = 88838)
insert into security.ModuleForm values (100,5,88838,1)

-- Define el formulario para el 'Cuestionario de Búsqueda de Casos Sospechosos de EPOC'.
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88047)
INSERT INTO Security.Form VALUES (88047,'Escala Cuestionario EPOC','SUCA',0,0,1,'FrmHCEscalaCuestionario_EPOC','Indigo.Emergentes.dll',0,'',1) 
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88047 AND IdAction = 2)
INSERT INTO Security.FormAction VALUES (88047,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88047 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88047,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88047 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88047,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88047 AND IdAction = 23)
INSERT INTO Security.FormAction VALUES (88047,23)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 100 AND IdTitle = 5 AND IdForm = 88047)
INSERT INTO Security.ModuleForm VALUES (100,5,88047,1)

-- Define el formulario para la 'Escala GAD-2' (Trastorno de Ansiedad Generalizada).
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88031)
INSERT INTO Security.Form VALUES (88031,'Escala GAD-2','SUCA',0,0,1,'FrmHCEscalaGAD2','Indigo.Emergentes.dll',0,'',1) 
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88031 AND IdAction = 2)
INSERT INTO Security.FormAction VALUES (88031,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88031 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88031,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88031 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88031,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88031 AND IdAction = 23)
INSERT INTO Security.FormAction VALUES (88031,23)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 100 AND IdTitle = 5 AND IdForm = 88031)
INSERT INTO Security.ModuleForm values (100,5,88031,1)

-- Define el formulario para la 'Escala de Estratificación de Riesgo Cardiovascular de la OMS'.
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88030)
INSERT INTO Security.Form VALUES (88030,'Escala de estratificación riesgo cardiovascular de la Organización Mundial de la Salud (OMS)','SUCA',0,0,1,'FrmHCEscalaEstratificacionRCV','Indigo.Emergentes.dll',0,'',1) 
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88030 AND IdAction = 2)
INSERT INTO Security.FormAction values (88030,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88030 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88030,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88030 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88030,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88030 AND IdAction = 23)
INSERT INTO Security.FormAction VALUES (88030,23)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 100 AND IdTitle = 5 AND IdForm = 88030)
INSERT INTO security.ModuleForm values (100,5,88030,1)

-- Define el formulario maestro 'Tipos de Admisiones'.
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88839)
INSERT INTO Security.Form VALUES (88839,'Tipos de admisiones','SUCA',0,0,1,'frmADTiposdeAdmisiones','Indigo.Admisiones.dll',0,'',1) 
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 58 AND IdTitle = 29 AND IdForm = 88839)
INSERT INTO Security.ModuleForm values (58,29,88839,24)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88839 AND IdAction = 2)
INSERT INTO Security.FormAction values (88839,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88839 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88839,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88839 AND IdAction = 41)
INSERT INTO Security.FormAction VALUES (88839,41)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88839 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88839,40)

-- Define el formulario para la 'Escala de Medición del Grado de Tabaquismo (Índice Paquetes Año)'.
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88048)
INSERT INTO Security.Form VALUES (88048,'Escala medicion grado de tabaquismo','SUCA',0,0,1,'FrmHCEscalaMedicionGradoTabaquismo','Indigo.Emergentes.dll',0,'',1) 
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88048 AND IdAction = 2)
INSERT INTO Security.FormAction VALUES (88048,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88048 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88048,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88048 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88048,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88048 AND IdAction = 23)
INSERT INTO Security.FormAction VALUES (88048,23)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 100 AND IdTitle = 5 AND IdForm = 88048)
INSERT INTO Security.ModuleForm VALUES (100,5,88048,1)

-- Define el formulario de 'Parámetros FURTRAN' (Formulario para Transporte).
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88033)
INSERT INTO Security.Form VALUES (88033,'Parámetros FURTRAN','SUCA',0,0,1,'frmADParametroFURTRAN','Indigo.Admisiones.dll',0,'',1)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88033 AND IdAction = 2)
INSERT INTO Security.FormAction values (88033,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88033 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88033,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88033 AND IdAction = 41)
INSERT INTO Security.FormAction VALUES (88033,41)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88033 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88033,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88033 AND IdAction = 19)
INSERT INTO Security.FormAction VALUES (88033,19)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 58 AND IdTitle = 40 AND IdForm = 88033)
INSERT INTO Security.ModuleForm values (58,40,88033,5)

-- Define el formulario maestro 'Recomendaciones'.
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88035)
INSERT INTO Security.Form VALUES (88035,'Recomendaciones','SUCA',0,0,1,'FrmHCRecomendacion','Indigo.eHistorias.dll',0,'',1) 
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88035 AND IdAction = 2)
INSERT INTO Security.FormAction VALUES (88035,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88035 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88035,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88035 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88035,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88035 AND IdAction = 23)
INSERT INTO Security.FormAction VALUES (88035,23)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 30 AND IdTitle = 92 AND IdForm = 88035)
INSERT INTO Security.ModuleForm VALUES (30,92,88035,18)

-- Define el formulario maestro 'Tipo de Prioridad'.
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88034)
INSERT INTO Security.Form VALUES (88034,'TIPO PRIORIDAD','SUCA',0,0,1,'frmCHTipoPrioridad','Indigo.Hospitalizacion.dll',0,'',1)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88034 AND IdAction = 2)
INSERT INTO Security.FormAction values (88034,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88034 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88034,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88034 AND IdAction = 41)
INSERT INTO Security.FormAction VALUES (88034,41)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88034 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88034,40)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 30 AND IdTitle = 92 AND IdForm = 88034)
INSERT INTO Security.ModuleForm values (30,92,88034,17)

-- Define los formularios y acciones para la funcionalidad 'Recomendar Paciente'.
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88036)
INSERT INTO Security.Form VALUES (88036,'Recomendar paciente','SUCA',0,0,1,'FrmRecomendarPaciente','Indigo.Controls.dll',0,'',1)
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88037)
INSERT INTO Security.Form VALUES (88037,'Agregar recomendación','SUCA',0,0,1,'FrmRecomendarPacienteDetalle','Indigo.Controls.dll',0,'',1)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88036 AND IdAction = 41)
INSERT INTO Security.FormAction VALUES (88036,41)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88036 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88036,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88036 AND IdAction = 19)
INSERT INTO Security.FormAction VALUES (88036,19)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88037 AND IdAction = 2)
INSERT INTO Security.FormAction values (88037,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88037 AND IdAction = 41)
INSERT INTO Security.FormAction VALUES (88037,41)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88037 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88037,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88037 AND IdAction = 19)
INSERT INTO Security.FormAction VALUES (88037,19)

-- Define el formulario maestro 'Tipos de Género'.
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88038)
INSERT INTO Security.Form VALUES (88038,'Tipos de género','SUCA',0,0,1,'frmADTiposdeGenero','Indigo.Admisiones.dll',0,'',1) 
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88038 AND IdAction = 1)
INSERT INTO Security.FormAction values (88038,1)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88038 AND IdAction = 2)
INSERT INTO Security.FormAction values (88038,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88038 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88038,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88038 AND IdAction = 41)
INSERT INTO Security.FormAction VALUES (88038,41)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88038 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88038,40)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 58 AND IdTitle = 29 AND IdForm = 88038)
INSERT INTO Security.ModuleForm values (58,29,88038,25)

-- Define el formulario para la 'Escala de Silverman-Anderson' (Dificultad respiratoria neonatal).
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88840)
INSERT INTO Security.Form VALUES (88840,'Escala de Silverman - Anderson','SUCA',0,0,1,'FrmHCEscalaSilverman_Anderson','Indigo.Emergentes.dll',0,'',1)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88840 AND IdAction = 2)
INSERT INTO Security.FormAction values (88840,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88840 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88840,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88840 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88840,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88840 AND IdAction = 23)
INSERT INTO Security.FormAction VALUES (88840,23) 
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 100 AND IdTitle = 5 AND IdForm = 88840)
insert into security.ModuleForm values (100,5,88840,1)

-- Define el formulario para la 'Bitácora de Auditoría de Historia Clínica'.
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88039)
INSERT INTO Security.Form VALUES (88039,'Bitácora de auditoría historia clínica','SUCA',0,0,1,'FrmHCBitacoraAuditoria','Indigo.eHistorias.dll',0,'',1)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88039 AND IdAction = 23)
INSERT INTO Security.FormAction values (88039,23)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88039 AND IdAction = 40)
INSERT INTO Security.FormAction values (88039,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88039 AND IdAction = 41)
INSERT INTO Security.FormAction values (88039,41)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 14 AND IdTitle = 4 AND IdForm = 88039)
insert into security.ModuleForm values (14,4,88039,6)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 721 AND IdTitle = 4 AND IdForm = 88039)
insert into security.ModuleForm values (721,4,88039,1)

-- Define el formulario para la 'Escala Downton' (Riesgo de caídas).
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88049)
INSERT INTO Security.Form VALUES (88049,'Escala Downton','SUCA',0,0,1,'FrmHCEscala_Downton','Indigo.Emergentes.dll',0,'',1) 
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88049 AND IdAction = 2)
INSERT INTO Security.FormAction VALUES (88049,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88049 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88049,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88049 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88049,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88049 AND IdAction = 23)
INSERT INTO Security.FormAction VALUES (88049,23)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 100 AND IdTitle = 5 AND IdForm = 88049)
INSERT INTO Security.ModuleForm VALUES (100,5,88049,1)

-- Define un permiso para acceder a la 'Hoja de Gasto Quirúrgica'.
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88050)
insert into security.form values ('88050','Hoja de gasto quirúrgica','SUCA','0','0','0',NULL,NULL,'0','','1')
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88050 AND IdAction = 41)
INSERT INTO Security.FormAction VALUES (88050,41)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88050 AND IdAction = 2)
INSERT INTO Security.FormAction VALUES (88050,2)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 100 AND IdTitle = 5 AND IdForm = 88050)
INSERT INTO Security.ModuleForm VALUES (100,5,88050,1)

-- Define el formulario maestro 'Definición de Muestras' para laboratorio.
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88040)
INSERT INTO Security.Form VALUES (88040,'Definición de muestras','SUCA',0,0,1,'FrmDefinicionMuestras','Indigo.Laboratorio.dll',0,'',1)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88040 AND IdAction = 1)
INSERT INTO Security.FormAction VALUES (88040,1)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88040 AND IdAction = 2)
INSERT INTO Security.FormAction VALUES (88040,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88040 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88040,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88040 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88040,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88040 AND IdAction = 41)
INSERT INTO Security.FormAction VALUES (88040,41)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 30 AND IdTitle = 94 AND IdForm = 88040)
INSERT INTO Security.ModuleForm VALUES (30,94,88040,4)

-- Define el formulario para la funcionalidad 'Regenerar Folio a PDF'.
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88041)
INSERT INTO Security.Form VALUES (88041,'Regenerar folio a PDF','SUCA',0,0,1,'FrmADRegenerarFolioaPDF','Indigo.Admisiones.dll',0,'',1) 
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88041 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88041,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88041 AND IdAction = 41)
INSERT INTO Security.FormAction VALUES (88041,41)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 98 AND IdTitle = 105 AND IdForm = 88041)
INSERT INTO Security.ModuleForm values (98,105,88041,3)

-- Habilita un permiso para visualizar el censo histórico general.
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88070)
insert into security.form values ('88070','Habilitar censo historico general','SUCA','0','0','0',NULL,NULL,'0','','1')
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88070 AND IdAction = 41)
INSERT INTO Security.FormAction VALUES (88070,41)

-- Define la acción 'Solicitud de Medicamentos en Pre-alta Hospitalaria'.
IF NOT EXISTS (SELECT 1 FROM Security.Action WHERE Id = 146)
INSERT INTO   Security.Action (id,Name,State ) VALUES ('146','Solicitud de medicamentos en Pre-alta hospitalaria','1')
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 2326 AND IdAction = 146)
INSERT INTO Security.FormAction (IdForm, IdAction)    values ('2326','146')

-- Define el formulario para la 'Escala ABCD2' (Riesgo de ACV).
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88843)
INSERT INTO Security.Form VALUES (88843,'Escala ABCD2 (Riesgo de ACV despues de un ataque isquémico transitorio)','SUCA',0,0,1,'FrmHCEscalaABCD','Indigo.Emergentes.dll',0,'',1)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88843 AND IdAction = 2)
INSERT INTO Security.FormAction values (88843,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88843 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88843,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88843 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88843,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88843 AND IdAction = 23)
INSERT INTO Security.FormAction VALUES (88843,23) 
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 100 AND IdTitle = 5 AND IdForm = 88843)
insert into security.ModuleForm values (100,5,88843,1)

-- Define el formulario para la 'Escala de Alvarado' (Diagnóstico de apendicitis).
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88844)
INSERT INTO Security.Form VALUES (88844,'Escala Alvarado','SUCA',0,0,1,'FrmHCEscalaAlvarado','Indigo.Emergentes.dll',0,'',1)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88844 AND IdAction = 2)
INSERT INTO Security.FormAction values (88844,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88844 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88844,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88844 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88844,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88844 AND IdAction = 23)
INSERT INTO Security.FormAction VALUES (88844,23) 
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 100 AND IdTitle = 5 AND IdForm = 88844)
insert into security.ModuleForm values (100,5,88844,1)

-- Define el formulario para la 'Escala EsGravE' (Equipo de Respuesta Rápida Pediátrica).
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88845)
INSERT INTO Security.Form VALUES (88845,'Escala EsGravE (Equipo de Respuesta Rápida pediatría)','SUCA',0,0,1,'FrmHCEscalaEsGravE','Indigo.Emergentes.dll',0,'',1)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88845 AND IdAction = 2)
INSERT INTO Security.FormAction values (88845,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88845 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88845,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88845 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88845,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88845 AND IdAction = 23)
INSERT INTO Security.FormAction VALUES (88845,23) 
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 100 AND IdTitle = 5 AND IdForm = 88845)
insert into security.ModuleForm values (100,5,88845,1)

-- Define el formulario para la 'Escala Shock Index'.
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88846)
INSERT INTO Security.Form VALUES (88846,'Escala Shock Index','SUCA',0,0,1,'FrmHCEscalaShockIndex','Indigo.Emergentes.dll',0,'',1)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88846 AND IdAction = 2)
INSERT INTO Security.FormAction values (88846,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88846 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88846,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88846 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88846,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88846 AND IdAction = 23)
INSERT INTO Security.FormAction VALUES (88846,23) 
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 100 AND IdTitle = 5 AND IdForm = 88846)
insert into security.ModuleForm values (100,5,88846,1)

-- Define acciones de soporte: 'Liberar Cama Doble Estancia' y 'Eliminar Registro de Egreso'.
IF NOT EXISTS (SELECT 1 FROM Security.Action WHERE Id = 148)
INSERT INTO Security.[action] (Id,Name,State) VALUES (148,'Soporte Vie: liberar cama doble estancia',1)
IF NOT EXISTS (SELECT 1 FROM Security.Action WHERE Id = 149)
INSERT INTO Security.[action] (Id,Name,State) VALUES (149,'Soporte Vie: eliminar registro egreso',1)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 1611 AND IdAction = 148)
insert into Security.FormAction (idform,IdAction) values (1611,148)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 1611 AND IdAction = 149)
insert into Security.FormAction (idform,IdAction) values (1611,149)

-- Actualiza el nombre del formulario de consulta de referencias y contrarreferencias.
update Security.form set Name = 'Consultar referencia / contrarreferencia' where id = '1918' and Name = 'Consultar Referencias'
update Security.form set Name = 'Consultar referencia / contrarreferencia' where id = '2545' and Name = 'Consultar Referencias' 

-- Define el formulario para la 'Escala de Evaluación del Riesgo de Preeclampsia'.
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88847)
INSERT INTO Security.Form VALUES (88847,'Escala de evaluación del riesgo de preeclampsia','SUCA',0,0,1,'FrmHCEscaladeEvaluaciondelRiesgodePreeclampsiavb','Indigo.Emergentes.dll',0,'',1)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88847 AND IdAction = 2)
INSERT INTO Security.FormAction values (88847,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88847 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88847,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88847 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88847,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88847 AND IdAction = 23)
INSERT INTO Security.FormAction VALUES (88847,23) 
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 100 AND IdTitle = 5 AND IdForm = 88847)
insert into security.ModuleForm values (100,5,88847,1)

-- Define el formulario para la 'Escala APGAR Familiar para uso en niños'.
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88051)
INSERT INTO Security.Form VALUES (88051,'Escala APGAR Familiar para uso en niños','SUCA',0,0,1,'FrmHCEscalaApgarUsoNinos','Indigo.Emergentes.dll',0,'',1)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88051 AND IdAction = 2)
INSERT INTO Security.FormAction values (88051,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88051 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88051,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88051 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88051,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88051 AND IdAction = 23)
INSERT INTO Security.FormAction VALUES (88051,23) 
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 100 AND IdTitle = 5 AND IdForm = 88051)
insert into security.ModuleForm values (100,5,88051,1)

-- Define el permiso para 'Modificar Ciclos Vigentes de Quimioterapia'.
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88852)
INSERT INTO Security.Form VALUES (88852,'Modificar ciclos vigentes de quimioterapia','SUCA',0,0,1,'frmHCAutorizacionEsquemas','Indigo.Emergentes.dll',0,'',1) 
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88852 AND IdAction = 40)
INSERT INTO Security.FormAction values (88852,40)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 100 AND IdTitle = 5 AND IdForm = 88852)
INSERT INTO Security.ModuleForm VALUES(100, 5, 88852, 1)

-- Define el formulario para el 'Enrutador de Imágenes Intrahospitalarias'.
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88853)
INSERT INTO Security.Form VALUES (88853,'Enrutador Imágenes Intrahospitalarias','SUCA',0,0,1,'frmRISDashboardRedireccionamiento1','Indigo.RIS.dll',0,'',1) 
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88853 AND IdAction = 2)
INSERT INTO Security.FormAction VALUES (88853,2)	
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88853 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88853,3)	
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88853 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88853,40)	
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88853 AND IdAction = 41)
INSERT INTO Security.FormAction VALUES (88853,41)	 
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 18 AND IdTitle = 30 AND IdForm = 88853)
INSERT INTO Security.ModuleForm values (18,30,88853,3)

-- Define el formulario de 'Parámetros del Lactario'.
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88854)
INSERT INTO Security.Form VALUES (88854,'Parámetros Lactario','SUCA',0,0,1,'FrmHCParametrizacionLactario','Indigo.eHistorias.dll',0,'',1) 
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 30 AND IdTitle = 108 AND IdForm = 88854)
INSERT INTO Security.ModuleForm values (30,108,88854,1)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88854 AND IdAction = 2)
INSERT INTO Security.FormAction values (88854,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88854 AND IdAction = 3)
INSERT INTO Security.FormAction values (88854,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88854 AND IdAction = 40)
INSERT INTO Security.FormAction values (88854,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88854 AND IdAction = 41)
INSERT INTO Security.FormAction values (88854,41)

-- Define el 'Dashboard del Lactario'.
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88855)
INSERT INTO Security.Form VALUES (88855,'Dashboard lactario','SUCA',0,0,1,'frmHCDashboarLactario','Indigo.eHistorias.dll',0,'',1)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88855 AND IdAction = 2)
INSERT INTO Security.FormAction VALUES (88855,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88855 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88855,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88855 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88855,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88855 AND IdAction = 41)
INSERT INTO Security.FormAction VALUES (88855,41)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 21 AND IdTitle = 30 AND IdForm = 88855)
INSERT INTO Security.ModuleForm values (21,30,88855,1)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 20 AND IdTitle = 30 AND IdForm = 88855)
INSERT INTO Security.ModuleForm values (20,30,88855,1)


---------------------------------------------------------------  Sprint Week 42 - 43 (2025)  ---------------------------------------------------------------  
update Security.Form set Name = 'Dashboard Terapias Reemplazo Renal', ClassName = 'frmHCDashboarRenal'  where id = 1900


---  Dashboard de Gestión de Desinfección de Equipos de Dialización (31615)
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88857)
INSERT INTO Security.Form VALUES (88857,'Desinfección equipos de tratamiento renal','SUCA',0,0,1,'frmHCDesinfeccionEquiposDializacion','Indigo.eHistorias.dll',0,'',1) 
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 23 AND IdTitle = 30 AND IdForm = 88857)
INSERT INTO Security.ModuleForm values (23,30,88857,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88857 AND IdAction = 2)
INSERT INTO Security.FormAction VALUES (88857,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88857 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88857,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88857 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88857,40)	
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88857 AND IdAction = 41)
INSERT INTO Security.FormAction VALUES (88857,41)	 

--- Dashboard de Gestion Imagenes Asistidas
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88856)
INSERT INTO Security.Form VALUES (88856,'Gestión imágenes asistidas','SUCA',0,0,1,'frmDashboarGestionImagenesAsistidas','Indigo.eHistorias.dll',0,'',1)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 18 AND IdTitle = 4 AND IdForm = 88856)
INSERT INTO Security.ModuleForm VALUES (18,4,88856,4)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88856 AND IdAction = 2)
INSERT INTO Security.FormAction VALUES (88856, 2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88856 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88856, 3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88856 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88856, 40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88856 AND IdAction = 41)
INSERT INTO Security.FormAction VALUES (88856, 41)


---------------------------------------------------------------  Sprint Week 44 - 45 (2025)  ---------------------------------------------------------------  
--- PBI-31514 2. Crear escala Nutric-Score en el sistema

---Formulario
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88858)
INSERT INTO Security.Form VALUES (88858,'Escala NUTRIC SCORE','SUCA',0,0,1,'FrmHCEscalaNUTRICSCORE','Indigo.Emergentes.dll',0,'',1)
---Acciones del frm
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88858 AND IdAction = 2)
INSERT INTO Security.FormAction values (88858,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88858 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88858,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88858 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88858,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88858 AND IdAction = 23)
INSERT INTO Security.FormAction VALUES (88858,23) 
---Modulo
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 100 AND IdTitle = 5 AND IdForm = 88858)
insert into security.ModuleForm values (100,5,88858,1)

--- PBI-32116 2. Crear escala Braden Q en el sistema
---Formulario
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88859)
INSERT INTO Security.Form VALUES (88859,'Escala Braden Q','SUCA',0,0,1,'FrmHCEscalaBradenQ','Indigo.Emergentes.dll',0,'',1)
---Acciones del frm
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88859 AND IdAction = 2)
INSERT INTO Security.FormAction values (88859,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88859 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88859,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88859 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88859,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88859 AND IdAction = 23)
INSERT INTO Security.FormAction VALUES (88859,23) 
---Modulo
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 100 AND IdTitle = 5 AND IdForm = 88859)
insert into security.ModuleForm values (100,5,88859,1)

---------------------------------------------------------------  Sprint Week 48 - 49 (2025)  ---------------------------------------------------------------  
-- PBI-32350  1. Crear Maestro Tipo de riesgos

IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88860)
INSERT INTO Security.Form VALUES (88860,'Tipos de riesgos','SUCA',0,0,1,'FrmADTipoRiesgo','Indigo.Admisiones.dll',0,'',1) 

IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 30 AND IdTitle = 96 AND IdForm = 88860)
INSERT INTO Security.ModuleForm values (30,96,88860,8)

IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88860 AND IdAction = 2)
INSERT INTO Security.FormAction values (88860,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88860 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88860,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88860 AND IdAction = 40)
INSERT INTO Security.FormAction values (88860,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88860 AND IdAction = 41)
INSERT INTO Security.FormAction VALUES (88860,41)

-- PBI-32355 1. Crear Maestro Tipos de alergia
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88861)
INSERT INTO Security.Form VALUES (88861,'Tipos de alergia','SUCA',0,0,1,'FrmADTipoAlergia','Indigo.Admisiones.dll',0,'',1) 

IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 30 AND IdTitle = 73 AND IdForm = 88861)
INSERT INTO Security.ModuleForm values (30,73,88861,16)

IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88861 AND IdAction = 2)
INSERT INTO Security.FormAction values (88861,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88861 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88861,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88861 AND IdAction = 40)
INSERT INTO Security.FormAction values (88861,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88861 AND IdAction = 41)
INSERT INTO Security.FormAction VALUES (88861,41)

-- PBI-32373 1. Creación de reporte "Circular 022"
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88862)
INSERT INTO Security.Form VALUES (88862,'Circular 022','SUCA',0,0,1,'FrmCircular022','Indigo.eHistorias.dll',0,'',1) 

IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 22 AND IdTitle = 4 AND IdForm = 88862)
INSERT INTO Security.ModuleForm values (22,4,88862,5)

IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88862 AND IdAction = 2)
INSERT INTO Security.FormAction values (88862,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88862 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88862,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88862 AND IdAction = 40)
INSERT INTO Security.FormAction values (88862,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88862 AND IdAction = 41)
INSERT INTO Security.FormAction VALUES (88862,41)

---------------------------------------------------------------  Sprint Week 50 - 51 (2025)  ---------------------------------------------------------------  
--Escala Evaluación de síntomas de Edmonton
---Formulario
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88863)
INSERT INTO Security.Form VALUES (88863,'Escala evaluación de síntomas de Edmonton (ESAS)','SUCA',0,0,1,'FrmHCEscalaEvaluacionSintomasEdmonton','Indigo.Emergentes.dll',0,'',1)
---Acciones del frm
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88863 AND IdAction = 2)
INSERT INTO Security.FormAction values (88863,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88863 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88863,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88863 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88863,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88863 AND IdAction = 23)
INSERT INTO Security.FormAction VALUES (88863,23) 
---Modulo
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 100 AND IdTitle = 5 AND IdForm = 88863)
insert into security.ModuleForm values (100,5,88863,1)

--Escala IDSA NAC
---Formulario
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88864)
INSERT INTO Security.Form VALUES (88864,'Escala IDSA-NAC (Infectious Diseases Society of America - Neumonía Adquirida en la Comunidad)','SUCA',0,0,1,'FrmHCEscalaIDSA_NAC','Indigo.Emergentes.dll',0,'',1)
---Acciones del frm
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88864 AND IdAction = 2)
INSERT INTO Security.FormAction values (88864,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88864 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (88864,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88864 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (88864,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88864 AND IdAction = 23)
INSERT INTO Security.FormAction VALUES (88864,23) 
---Modulo
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 100 AND IdTitle = 5 AND IdForm = 88864)
insert into security.ModuleForm values (100,5,88864,1)



---------------------------------------------------------------  Sprint Week 4 - 5 (2026)  ---------------------------------------------------------------  

--Mapeo CIE 11
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 89038)
INSERT INTO Security.Form VALUES (89038, 'Mapeo CIE 11', 'SUCA', 0, 0, 1, 'frmADMapeoCIE11','Indigo.Admisiones.dll', 0, '', 1);
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 89038 AND IdAction = 2)
INSERT INTO Security.FormAction VALUES (89038, 2);   -- Guardar
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 89038 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (89038, 3);   -- Actualizar
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 89038 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (89038, 40);  -- Consultar
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 89038 AND IdAction = 41)
INSERT INTO Security.FormAction VALUES (89038, 41);  -- Visible

---Modulo
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 40 AND IdTitle = 73 AND IdForm = 89038)
INSERT INTO Security.ModuleForm VALUES (40, 73, 89038, 1); 


---------------------------------------------------------------  Product Backlog Item Sprint Week 4 - 5 (2026) ---------------------------------------------------------------  

--Mapeo Estratificación Socioeconómica
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 89039)
INSERT INTO Security.Form VALUES (89039, 'Estratificación socioeconómica', 'SUCA', 0, 0, 1, 'FrmADEstratificacionSocieconomica','Indigo.Admisiones.dll', 0, '', 1);

IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 89039 AND IdAction = 2)
INSERT INTO Security.FormAction VALUES (89039, 2);   -- Guardar
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 89039 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (89039, 3);   -- Actualizar
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 89039 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (89039, 40);  -- Consultar
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 89039 AND IdAction = 41)
INSERT INTO Security.FormAction VALUES (89039, 41);  -- Visible

---Modulo
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 58 AND IdTitle = 29 AND IdForm = 89039)
INSERT INTO Security.ModuleForm VALUES (58, 29, 89039, 17); 


----------------------------------------------------------------  Sprint Week 16 - 17 (2026)  -----------------------------------------------------------------------

--PBI-35432 1. Crear Escala Columbia (Suicide severity rating scale)

IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 89042)
INSERT INTO Security.Form VALUES (89042,'Escala Columbia (Suicide severity rating scale)','SUCA',0,0,1,'frmHCEscala_Columbia','Indigo.Emergentes.dll',0,'',1)

IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 89042 AND IdAction = 2)
INSERT INTO Security.FormAction VALUES (89042,2)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 89042 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (89042,3)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 89042 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (89042,40)
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 89042 AND IdAction = 23)
INSERT INTO Security.FormAction VALUES (89042,23) 

---Modulo
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 100 AND IdTitle = 5 AND IdForm = 89042)
INSERT INTO security.ModuleForm VALUES (100,5,89042,1)
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 100 AND IdTitle = 5 AND IdForm = 88859)
insert into security.ModuleForm values (100,5,88859,1)

-- PBI-35404 2. Creación de Escala de riesgo suicida de Plutchik 
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 89041)
INSERT INTO Security.Form VALUES (89041, 'Escala de riesgo suicida de Plutchik', 'SUCA', 0, 0, 1, 'FrmHCEscalaSuicidaPlutchik','Indigo.Emergentes.dll', 0, '', 1);

IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 89041 AND IdAction = 2)
INSERT INTO Security.FormAction VALUES (89041, 2);   -- Guardar
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 89041 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (89041, 3);   -- Actualizar
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 89041 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (89041, 40);  -- Consultar
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 89041 AND IdAction = 41)
INSERT INTO Security.FormAction VALUES (89041, 41);  -- Visible

---Modulo
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 58 AND IdTitle = 29 AND IdForm = 89041)
INSERT INTO Security.ModuleForm VALUES (58, 29, 89041, 17); 

---- PBI 36415 FECHA 14/05/2026: Ajuste el label del formulario Parámetros FURIPS
--- Se actualiza el nombre del Formulario

 update security.form set Name = 'Parámetros formularios de reclamaciones' where id = '88027'  -- COMPILADO


 ----------------------------------------------------------------  Sprint Week 20 - 21 (2026)  -----------------------------------------------------------------------

 -- PBI 36232 Creacion del formulario FUR
 IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 89043)
 INSERT INTO Security.Form VALUES (89043, 'FUR - Formato único de reclamaciones', 'SUCA', 0, 0, 1,'frmADFur', 'Indigo.Admisiones.dll', 0, '', 1);

IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 89043 AND IdAction = 2)
INSERT INTO Security.FormAction VALUES (89043, 2);   -- Guardar
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 89043 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (89043, 3);   -- Actualizar
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 89043 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (89043, 40);  -- Consultar
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 89043 AND IdAction = 41)
INSERT INTO Security.FormAction VALUES (89043, 41);  -- Visible

IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 58 AND IdTitle = 42 AND IdForm = 89043)
INSERT INTO Security.ModuleForm VALUES (58, 42, 89043, 9); 


----------------------------------------------------------------

UPDATE Security.Form SET Name = 'TIPOS GASES PARA INHALACIÓN' WHERE Id = 1553

-- 23 de mayo de 2022
--- BUG 4206: Error de diseño
UPDATE Security.Form SET Name = 'Reportes citas de apoyo DX y tratamientos especiales' WHERE Id = '1712'
--PBI 15188	Refactoring "Parametrización de escalas" - "Escala Downton" y "Escala Downton adaptada"																					  
--18/02/2024
--Se renombra la Escala Downton a Escala Downton Adaptada
UPDATE Security.Form SET Name = 'Escala Downton Adaptada' WHERE Id = 88842
--pbi 16693 Actualización label "Incapacidad" por "Incapacidades y licencias" para q aparezca asi en el compilado
 update security.form set Name = 'Incapacidades y licencias' where id = '1581' 
--PBI 16980 4. Refactoring formulario "Criterios de hospitalización" 
--Scrips para actualizar el nombre del formulario "Criterios de hospitalización" a "Criterios de estancia y egreso"
Update Security.Form SET Name = 'Criterios de estancia y egreso' where Id = '1541'
--PBI 18446 Cambiar el nombre del Dashboard de Especialistas por Dashboard de interconsultas
--Scrips para actualizar el nombre del formulario "Dashboard Especialistas" a "Dashboard Interconsultas"
update Security.Form set Name = 'Dashboard Interconsultas' where Id = '1572'

--BUG 19097 Bug de diseño N°36
--Scrips para corregir la ortografia de ciertos formularios en el menu
UPDATE security.form  SET Name = 'Reporte seguridad del paciente' WHERE Id= '2720' AND name = 'Reporte seguridad del paciente'
UPDATE security.form  SET Name = 'Gestión de agenda ambulatoria' WHERE Id= '1665' AND name = 'Gestión de Agenda Ambulatoria'
UPDATE security.form  SET Name = 'Asignación de citas' WHERE Id= '1666' AND name = 'Asignación de Citas'
UPDATE security.form  SET Name = 'Gestión de agenda quirófanos' WHERE Id= '1974' AND name = 'Gestión de agenda quirófanos'
UPDATE security.form  SET Name = 'Radicación cirugías' WHERE Id= '2052' AND name = 'Radicación cirugías'
UPDATE security.form  SET Name = 'Disponibilidad por quirófanos' WHERE Id= '2112' AND name = 'Disponibilidad por quirófanos'
UPDATE security.form  SET Name = 'Asignación de citas apoyo diagnóstico' WHERE Id= '1686' AND name = 'Asignación de Citas Apoyo Diagnóstico'

--PBI 18594 1. Ajuste label formularios - Go / Vie Ambulatory / Agendamiento
--Scrips para corregir la ortografia de ciertos formularios en el menu
UPDATE security.form  SET Name = 'Nota servicio farmacéutico' WHERE Id= '259' AND name = 'Nota Servicio Farmaceutico'

UPDATE security.form  SET Name = 'Nota servicio farmacéutico' WHERE Id= '259' AND name = 'Nota Servicio Farmaceutico'
UPDATE security.form  SET Name = 'Dashboard médico' WHERE Id= '1534' AND name = 'Dashboard Médico'
UPDATE security.form  SET Name = 'Actividades de enfermería' WHERE Id= '1535' AND name = 'Actividades de Enfermería'
UPDATE security.form  SET Name = 'Categorias anestesia' WHERE Id= '1536' AND name = 'Categorias Anestesia'
UPDATE security.form  SET Name = 'Categoría documentos' WHERE Id= '1537' AND name = 'Categoría Documentos'
UPDATE security.form  SET Name = 'Centros de remisión' WHERE Id= '1538' AND name = 'Centros de Remisión'
UPDATE security.form  SET Name = 'Concepto de ajustes' WHERE Id= '1539' AND name = 'Concepto de Ajustes'
UPDATE security.form  SET Name = 'Creación de encuestas' WHERE Id= '1540' AND name = 'Creación de Encuestas'
UPDATE security.form  SET Name = 'Criterios eliminación alertas' WHERE Id= '1542' AND name = 'Criterios Eliminación Alertas'
UPDATE security.form  SET Name = 'Formas del medicamento' WHERE Id= '1544' AND name = 'Formas del Medicamento'
UPDATE security.form  SET Name = 'Liquidos eliminados' WHERE Id= '1545' AND name = 'Liquidos Eliminados'
UPDATE security.form  SET Name = 'Lista de chequeo' WHERE Id= '1546' AND name = 'Lista de Chequeo'
UPDATE security.form  SET Name = 'Medios de eliminación' WHERE Id= '1547' AND name = 'Medios de Eliminación'
UPDATE security.form  SET Name = 'Motivos anulación folios' WHERE Id= '1549' AND name = 'Motivos Anulación Folios'
UPDATE security.form  SET Name = 'Niveles de importancia' WHERE Id= '1552' AND name = 'Niveles de Importancia'
UPDATE security.form  SET Name = 'Subcategorias anestesia' WHERE Id= '1556' AND name = 'Subcategorias Anestesia'
UPDATE security.form  SET Name = 'Tipos arribo a urgencia' WHERE Id= '1557' AND name = 'Tipos Arribo a Urgencia'
UPDATE security.form  SET Name = 'Tipos de planificación' WHERE Id= '1558' AND name = 'Tipos de Planificación'
UPDATE security.form  SET Name = 'Vias de abordaje' WHERE Id= '1559' AND name = 'Vias de Abordaje'
UPDATE security.form  SET Name = 'Consentimiento informado' WHERE Id= '1560' AND name = 'Consentimiento Informado'
UPDATE security.form  SET Name = 'Configuración empresas' WHERE Id= '1562' AND name = 'Configuración Empresas'
UPDATE security.form  SET Name = 'Configuración conexión HIS' WHERE Id= '1563' AND name = 'Configuración Conexión HIS'
UPDATE security.form  SET Name = 'Calificación especialistas' WHERE Id= '1564' AND name = 'Calificación Especialistas'
UPDATE security.form  SET Name = 'Calificación estudiantes' WHERE Id= '1565' AND name = 'Calificación Estudiantes'
UPDATE security.form  SET Name = 'Consulta historias' WHERE Id= '1567' AND name = 'Consulta Historias'
UPDATE security.form  SET Name = 'Dashboard académicos' WHERE Id= '1568' AND name = 'Dashboard Académicos'
UPDATE security.form  SET Name = 'Dashboard atención farmacéutica' WHERE Id= '1569' AND name = 'Dashboard Atención Farmaceutica'
UPDATE security.form  SET Name = 'Dashboard auditores externos' WHERE Id= '1570' AND name = 'Dashboard Auditores Externos'
UPDATE security.form  SET Name = 'Dashboard enfermería' WHERE Id= '1571' AND name = 'Dashboard Enfermería'
UPDATE security.form  SET Name = 'Dashboard interconsultas' WHERE Id= '1572' AND name = 'Dashboard Interconsultas'
UPDATE security.form  SET Name = 'Dashboard pacientes por facturar' WHERE Id= '1573' AND name = 'Dashboard Pacientes por Facturar'
UPDATE security.form  SET Name = 'Dashboard laboratorio' WHERE Id= '1576' AND name = 'Dashboard Laboratorio'
UPDATE security.form  SET Name = 'Dashboard riesgos y necesidades' WHERE Id= '1578' AND name = 'Dashboard Riesgos y Necesidades'
UPDATE security.form  SET Name = 'Dashboard servicios apoyo' WHERE Id= '1579' AND name = 'Dashboard Servicios Apoyo'
UPDATE security.form  SET Name = 'Plantillas documentos' WHERE Id= '1582' AND name = 'Plantillas Documentos'
UPDATE security.form  SET Name = 'Registros de referencia' WHERE Id= '1583' AND name = 'Registros de Referencia'
UPDATE security.form  SET Name = 'Resultados laboratorio' WHERE Id= '1584' AND name = 'Resultados Laboratorio'
UPDATE security.form  SET Name = 'Turno especialidades' WHERE Id= '1585' AND name = 'Turno Especialidades'
UPDATE security.form  SET Name = 'Administración de alertas' WHERE Id= '1586' AND name = 'Administración de Alertas'
UPDATE security.form  SET Name = 'Configurar favoritos' WHERE Id= '1587' AND name = 'Configurar Favoritos'
UPDATE security.form  SET Name = 'Control de cuentas' WHERE Id= '1588' AND name = 'Control Cuentas'
UPDATE security.form  SET Name = 'Corte de cuentas' WHERE Id= '1589' AND name = 'Corte de Cuentas'
UPDATE security.form  SET Name = 'Favoritos quirúrgicos' WHERE Id= '1590' AND name = 'Favoritos Qx'
UPDATE security.form  SET Name = 'Parámetros historias' WHERE Id= '1591' AND name = 'Parámetros Historias'
UPDATE security.form  SET Name = 'Centros de atención' WHERE Id= '1617' AND name = 'Centros de Atención'
UPDATE security.form  SET Name = 'Días festivos' WHERE Id= '1620' AND name = 'Días Festivos'
UPDATE security.form  SET Name = 'Grupos étnicos' WHERE Id= '1625' AND name = 'Grupos Étnicos'
UPDATE security.form  SET Name = 'Grupo poblacional' WHERE Id= '1630' AND name = 'Grupo Poblacional'
UPDATE security.form  SET Name = 'Proveedores de salud' WHERE Id= '1631' AND name = 'Proveedores de Salud'
UPDATE security.form  SET Name = 'Control pacientes' WHERE Id= '1634' AND name = 'Control Pacientes'
UPDATE security.form  SET Name = 'Clasificación triage' WHERE Id= '1635' AND name = 'Clasificación Triage'
UPDATE security.form  SET Name = 'Control triage' WHERE Id= '1636' AND name = 'Control Triage'
UPDATE security.form  SET Name = 'Atención urgencias' WHERE Id= '1637' AND name = 'Atención Urgencias'
UPDATE security.form  SET Name = 'Control consulta externa' WHERE Id= '1638' AND name = 'Control Consulta Externa'
UPDATE security.form  SET Name = 'Autorización de servicios' WHERE Id= '1640' AND name = 'Autorización de Servicios'
UPDATE security.form  SET Name = 'Dashboard autorizaciones intrahospitalario' WHERE Id= '1641' AND name = 'Dashboard Autorizaciones Intrahospitalario'
UPDATE security.form  SET Name = 'Registrar inconsistencias' WHERE Id= '1646' AND name = 'Registrar Inconsistencias'
UPDATE security.form  SET Name = 'Control traslado códigos' WHERE Id= '1648' AND name = 'Control Traslado Códigos'
UPDATE security.form  SET Name = 'Activar ingresos' WHERE Id= '1649' AND name = 'Activar Ingresos'
UPDATE security.form  SET Name = 'Bloquear ingresos' WHERE Id= '1650' AND name = 'Bloquear Ingresos'
UPDATE security.form  SET Name = 'Configurar centros de atención' WHERE Id= '1651' AND name = 'Configurar Centros de Atención'
UPDATE security.form  SET Name = 'Configurar entidades' WHERE Id= '1652' AND name = 'Configurar Entidades'
UPDATE security.form  SET Name = 'Servicios susceptibles de autorización intrahospitalario' WHERE Id= '1653' AND name = 'Servicios Susceptibles de Autorización Intrahospitalario'
UPDATE security.form  SET Name = 'Parámetros urgencias' WHERE Id= '1654' AND name = 'Parámetros Urgencias'
UPDATE security.form  SET Name = 'Impresión de ingreso' WHERE Id= '1655' AND name = 'Impresión de Ingreso'
UPDATE security.form  SET Name = 'Impresión triage' WHERE Id= '1656' AND name = 'Impresión Triage'
UPDATE security.form  SET Name = 'Actividades de agendamiento' WHERE Id= '1657' AND name = 'Actividades de Agendamiento'
UPDATE security.form  SET Name = 'Causas de cancelación de agendas' WHERE Id= '1658' AND name = 'Causas de Cancelación de Agendas'
UPDATE security.form  SET Name = 'Causas de cancelación de citas' WHERE Id= '1659' AND name = 'Causas de Cancelación de Citas'
UPDATE security.form  SET Name = 'Causas de cancelación de citas en espera' WHERE Id= '1660' AND name = 'Causas de Cancelación de Citas en Espera'
UPDATE security.form  SET Name = 'Especialidades - actividades' WHERE Id= '1662' AND name = 'Especialidades-Actividades'
UPDATE security.form  SET Name = 'Citas preasignadas' WHERE Id= '1667' AND name = 'Citas Preasignadas'
UPDATE security.form  SET Name = 'Programación de cirugias múltiples' WHERE Id= '1668' AND name = 'Programación de Cirugias Múltiples'
UPDATE security.form  SET Name = 'Parámetros agendamiento' WHERE Id= '1670' AND name = 'Parámetros Agendamiento'
UPDATE security.form  SET Name = 'Reportes agendamiento' WHERE Id= '1671' AND name = 'Reportes Agendamiento'
UPDATE security.form  SET Name = 'Reportes oportunidad citas' WHERE Id= '1672' AND name = 'Reportes Oportunidad Citas'
UPDATE security.form  SET Name = 'Trazabilidad agendamiento' WHERE Id= '1673' AND name = 'Trazabilidad Agendamiento'
UPDATE security.form  SET Name = 'Gestión material osteosíntesis' WHERE Id= '1680' AND name = 'Gestión Material Osteosíntesis'
UPDATE security.form  SET Name = 'Reportes de usuario' WHERE Id= '1687' AND name = 'Reportes de Usuario'
UPDATE security.form  SET Name = 'Reportes personalizados' WHERE Id= '1688' AND name = 'Reportes Personalizados'
UPDATE security.form  SET Name = 'Reportes citas de apoyo diagnóstico y tratamientos especiales' WHERE Id= '1712' AND name = 'Reportes citas de apoyo DX y tratamientos especiales'
UPDATE security.form  SET Name = 'Asignación de citas tratamiento especiales' WHERE Id= '1783' AND name = 'Asignación de Citas Tratamiento Especiales'
UPDATE security.form  SET Name = 'Citas diálisis' WHERE Id= '1886' AND name = 'Citas Diálisis'
UPDATE security.form  SET Name = 'Agenda salas renal' WHERE Id= '1887' AND name = 'Agenda Salas Renal'
UPDATE security.form  SET Name = 'Motivos de no transfusión' WHERE Id= '1888' AND name = 'Motivos de No Transfusión'
UPDATE security.form  SET Name = 'Motivos no realización pruebas cruzadas' WHERE Id= '1889' AND name = 'Motivos No Realización Pruebas Cruzadas'
UPDATE security.form  SET Name = 'Valores paraclínicos' WHERE Id= '1890' AND name = 'Valores Paraclínicos'
UPDATE security.form  SET Name = 'Motivos de rechazo hemocomponentes' WHERE Id= '1892' AND name = 'Motivos de Rechazo Hemocomponentes'
UPDATE security.form  SET Name = 'Motivos de liberación hemocomponentes reservados' WHERE Id= '1893' AND name = 'Motivos de Liberación Hemocomponentes Reservados'
UPDATE security.form  SET Name = 'Esquema renal' WHERE Id= '1894' AND name = 'Esquema Renal'
UPDATE security.form  SET Name = 'Parametrizador de vacunas' WHERE Id= '1895' AND name = 'Vacunas Adicionales'
UPDATE security.form  SET Name = 'Eventos adversos' WHERE Id= '1896' AND name = 'Eventos Adversos'
UPDATE security.form  SET Name = 'Componentes sanguíneos' WHERE Id= '1897' AND name = 'Componentes Sanguíneos'
UPDATE security.form  SET Name = 'Dashboard hemocomponentes' WHERE Id= '1898' AND name = 'DashBoard Hemocomponentes'
UPDATE security.form  SET Name = 'Dashboard diálisis' WHERE Id= '1900' AND name = 'Dashboard Diálisis'
UPDATE security.form  SET Name = 'Informes patológicos' WHERE Id= '1901' AND name = 'Informes Patológicos'
UPDATE security.form  SET Name = 'Ficha renal' WHERE Id= '1902' AND name = 'Ficha Renal'
UPDATE security.form  SET Name = 'Motivo consulta historia clinica' WHERE Id= '1903' AND name = 'Motivo Consulta Historia Clinica'
UPDATE security.form  SET Name = 'Dashboard recepción de referencias' WHERE Id= '1919' AND name = 'Dashboard Recepción de Referencias'
UPDATE security.form  SET Name = 'Equipos de tratamiento' WHERE Id= '1950' AND name = 'Equipos de Tratamiento'
UPDATE security.form  SET Name = 'Tratamientos y diagnósticos odontológicos' WHERE Id= '1959' AND name = 'Tratamientos y Diagnósticos Odontológicos'
UPDATE security.form  SET Name = 'Cargar base de datos' WHERE Id= '1962' AND name = 'Cargar BD'
UPDATE security.form  SET Name = 'Dashboard programas PYP' WHERE Id= '1963' AND name = 'Dashboard Programas PYP'
UPDATE security.form  SET Name = 'Rutas integrales de atención en salud (RIAS)' WHERE Id= '1964' AND name = 'RIAS'
UPDATE security.form  SET Name = 'Formatos documentación clínica' WHERE Id= '1972' AND name = 'Formatos Documentación Clínica'
UPDATE security.form  SET Name = 'Ficha oncológica' WHERE Id= '1973' AND name = 'Ficha Oncologica'
UPDATE security.form  SET Name = 'Dashboard cuenta alto costo (CAC)' WHERE Id= '1975' AND name = 'DashBoard CAC'
UPDATE security.form  SET Name = 'Parámetro balance acido base' WHERE Id= '1982' AND name = 'Parámetro Balance Acido Base'
UPDATE security.form  SET Name = 'Programación de cirugías' WHERE Id= '1992' AND name = 'Programación de Cirugías'
UPDATE security.form  SET Name = 'Dashboard patologías' WHERE Id= '1993' AND name = 'Dashboard Patologías'
UPDATE security.form  SET Name = 'Niveles educativos' WHERE Id= '1997' AND name = 'Niveles Educativos'
UPDATE security.form  SET Name = 'Control consulta prioritaria' WHERE Id= '1998' AND name = 'Control Consulta Prioritaria'
UPDATE security.form  SET Name = 'Formulario único de reclamación (FURPRO)' WHERE Id= '1999' AND name = 'FURPRO'
UPDATE security.form  SET Name = 'Imprimir stickers - manillas paciente' WHERE Id= '2001' AND name = 'Imprimir Stickers/Manillas Paciente'
UPDATE security.form  SET Name = 'Parámetros consulta externa' WHERE Id= '2002' AND name = 'Parámetros Consulta Externa'
UPDATE security.form  SET Name = 'Parámetros consulta prioritaria' WHERE Id= '2003' AND name = 'Parámetros Consulta Prioritaria'
UPDATE security.form  SET Name = 'Areas institucionales' WHERE Id= '2005' AND name = 'Areas Institucionales'
UPDATE security.form  SET Name = 'Areas otros procedimientos' WHERE Id= '2006' AND name = 'Areas Otros Procedimientos'
UPDATE security.form  SET Name = 'Recomendaciones médicas' WHERE Id= '2008' AND name = 'Recomendaciones Médicas'
UPDATE security.form  SET Name = 'Regiones tratamiento radioterapia' WHERE Id= '2009' AND name = 'Regiones Tratamiento Radioterapia'
UPDATE security.form  SET Name = 'Generar soporte cuenta alto costo (CAC)' WHERE Id= '2010' AND name = 'Generar Soportes CAC'
UPDATE security.form  SET Name = 'Dashboard epidemiología' WHERE Id= '2011' AND name = 'Dashboard Epidemiología'
UPDATE security.form  SET Name = 'Dashboard otros procedimientos' WHERE Id= '2012' AND name = 'Dashboard Otros Procedimientos'
UPDATE security.form  SET Name = 'Justificación no PBS' WHERE Id= '2013' AND name = 'Justificación No PBS'
UPDATE security.form  SET Name = 'Configurar analitos' WHERE Id= '2014' AND name = 'Configurar Analitos'
UPDATE security.form  SET Name = 'Parametrización control de exámen físico' WHERE Id= '2015' AND name = 'Parametrización Control de Exámen Físico'
UPDATE security.form  SET Name = 'Causas cancelación de cirugías' WHERE Id= '2016' AND name = 'Causas Cancelación de Cirugías'
UPDATE security.form  SET Name = 'Mapa quirúrgico' WHERE Id= '2018' AND name = 'Mapa Quirúrgico'
UPDATE security.form  SET Name = 'Reasignación de cirugías' WHERE Id= '2019' AND name = 'Reasignación de Cirugías'
UPDATE security.form  SET Name = 'Reportes medicamentos' WHERE Id= '2021' AND name = 'Reportes Medicamentos'
UPDATE security.form  SET Name = 'Comites - áreas' WHERE Id= '2040' AND name = 'Comites-Areas'
UPDATE security.form  SET Name = 'Niveles de daño' WHERE Id= '2042' AND name = 'Niveles de Daño'
UPDATE security.form  SET Name = 'Tipo y clase de las acciones' WHERE Id= '2043' AND name = 'Tipo y Clase de las Acciones'
UPDATE security.form  SET Name = 'Dashboard de seguimiento calidad' WHERE Id= '2044' AND name = 'Dashboard de Seguimiento Calidad'
UPDATE security.form  SET Name = 'Reporte de acciones inseguras ó situaciones clínicas inesperadas' WHERE Id= '2045' AND name = 'Reporte de Acciones Inseguras o Situaciones Clínicas Inesperadas'
UPDATE security.form  SET Name = 'Descartar fichas sivigila' WHERE Id= '2046' AND name = 'Descartar Fichas Sivigila'
UPDATE security.form  SET Name = 'Notas administrativas' WHERE Id= '2048' AND name = 'Notas Administrativas'
UPDATE security.form  SET Name = 'Motivos no cumplimiento tratamiento odontológico' WHERE Id= '2049' AND name = 'Motivos no Cumplimiento Tratamiento Odontológico'
UPDATE security.form  SET Name = 'Parametrizar notas administrativas' WHERE Id= '2050' AND name = 'Parametrizar Notas Administrativas'
UPDATE security.form  SET Name = 'Disponibilidad sala de apoyo diagnóstico' WHERE Id= '2108' AND name = 'Disponibilidad sala de apoyo DX'
UPDATE security.form  SET Name = 'Gestion rutas integrales de atención en salud (RIAS)' WHERE Id= '2110' AND name = 'Gestion RIAS'
UPDATE security.form  SET Name = 'Reportes programación de cirugía' WHERE Id= '2111' AND name = 'Reportes programación de Cirugía'
UPDATE security.form  SET Name = 'Dashboard radioterapia' WHERE Id= '2127' AND name = 'Dashboard Radioterapia'
UPDATE security.form  SET Name = 'Dashboard quimioterapia' WHERE Id= '2128' AND name = 'Dashboard Quimioterapia'
UPDATE security.form  SET Name = 'Paquete órdenes' WHERE Id= '2137' AND name = 'Paquete Ordenes'
UPDATE security.form  SET Name = 'Autorización múltiple relación código MIPRES' WHERE Id= '2142' AND name = 'Autorización Múltiple Relación Código MIPRES'
UPDATE security.form  SET Name = 'Validación derechos' WHERE Id= '2147' AND name = 'Validación Derechos'
UPDATE security.form  SET Name = 'Diagnósticos de enfermería' WHERE Id= '2154' AND name = 'Diagnósticos de Enfermería'
UPDATE security.form  SET Name = 'Maestro de radioterapia' WHERE Id= '2155' AND name = 'Maestro de Radioterapia'
UPDATE security.form  SET Name = 'Certificados asistenciales' WHERE Id= '2156' AND name = 'Certificados Asistenciales'
UPDATE security.form  SET Name = 'Dashboard atención domiciliaria' WHERE Id= '2157' AND name = 'Dashboard Atención Domiciliaria'
UPDATE security.form  SET Name = 'Histórico medicamentos controlados' WHERE Id= '2162' AND name = 'Histórico Medicamentos Controlados'
UPDATE security.form  SET Name = 'Dashboard braquiterapia' WHERE Id= '2175' AND name = 'Dashboard Braquiterapia'
UPDATE security.form  SET Name = 'CUPS otros procedimientos por centro atención' WHERE Id= '2191' AND name = 'CUPS Otros Procedimientos por Centro Atención'
UPDATE security.form  SET Name = 'Control citas consulta externa' WHERE Id= '2219' AND name = 'Control Citas Consulta Externa'
UPDATE security.form  SET Name = 'Permiso crear ingreso desde confirmar cita apoyo diagnóstico' WHERE Id= '2245' AND name = 'PERMISO CREAR INGRESOS DESDE CONFIRMAR CITA APOYO DX'
UPDATE security.form  SET Name = 'IAAS quirúrgico' WHERE Id= '2246' AND name = 'IAAS-QX'
UPDATE security.form  SET Name = 'Prestamo de medicamentos' WHERE Id= '2254' AND name = 'PRESTAMO DE MEDICAMENTOS'
UPDATE security.form  SET Name = 'Procedimientos de terapia' WHERE Id= '2266' AND name = 'Procedimientos de Terapia'
UPDATE security.form  SET Name = 'Justificación cancelación - suspensión' WHERE Id= '2283' AND name = 'Justificacion Cancelación/Suspensión'
UPDATE security.form  SET Name = 'Motivos cancelación de cirugías' WHERE Id= '2295' AND name = 'Motivos Cancelacion de Cirugias'
UPDATE security.form  SET Name = 'Epicrisis resumida' WHERE Id= '2299' AND name = 'Epicrisis Resumida'
UPDATE security.form  SET Name = 'Justificación no PBS' WHERE Id= '2327' AND name = 'Justificación No PBS'
UPDATE security.form  SET Name = 'Permiso desconfirmar hoja gasto quirúrgico' WHERE Id= '2337' AND name = 'PERMISO DESCONFIRMAR HOJA GASTO QX'
UPDATE security.form  SET Name = 'Personalización historia clínica' WHERE Id= '2368' AND name = 'Personalización HC'
UPDATE security.form  SET Name = 'Actividades de enfermería' WHERE Id= '2380' AND name = 'Actividades de Enfermería'
UPDATE security.form  SET Name = 'Centros de remisión' WHERE Id= '2382' AND name = 'Centros de Remisión'
UPDATE security.form  SET Name = 'Concepto de ajustes' WHERE Id= '2383' AND name = 'Concepto de Ajustes'
UPDATE security.form  SET Name = 'Criterios eliminación alertas' WHERE Id= '2384' AND name = 'Criterios Eliminación Alertas'
UPDATE security.form  SET Name = 'Formas del medicamento' WHERE Id= '2385' AND name = 'Formas del Medicamento'
UPDATE security.form  SET Name = 'Motivos anulación folios' WHERE Id= '2386' AND name = 'Motivos Anulación Folios'
UPDATE security.form  SET Name = 'Niveles de importancia' WHERE Id= '2388' AND name = 'Niveles de Importancia'
UPDATE security.form  SET Name = 'Tipos de planificación' WHERE Id= '2389' AND name = 'Tipos de Planificación'
UPDATE security.form  SET Name = 'Vias de abordaje' WHERE Id= '2390' AND name = 'Vias de Abordaje'
UPDATE security.form  SET Name = 'Plantillas documentos' WHERE Id= '2466' AND name = 'Plantillas Documentos'
UPDATE security.form  SET Name = 'Administración de alertas' WHERE Id= '2481' AND name = 'Administración de Alertas'
UPDATE security.form  SET Name = 'Configurar favoritos' WHERE Id= '2482' AND name = 'Configurar Favoritos'
UPDATE security.form  SET Name = 'Parámetros de historias' WHERE Id= '2483' AND name = 'Parámetros de Historias'
UPDATE security.form  SET Name = 'Centros de atención' WHERE Id= '2488' AND name = 'Centros de Atención'
UPDATE security.form  SET Name = 'Profesionales de la salud' WHERE Id= '2493' AND name = 'Profesionales de la Salud'
UPDATE security.form  SET Name = 'Actividades de agendamiento' WHERE Id= '2495' AND name = 'Actividades de Agendamiento'
UPDATE security.form  SET Name = 'Causas de cancelación de agendas' WHERE Id= '2496' AND name = 'Causas de Cancelación de Agendas'
UPDATE security.form  SET Name = 'Causas de cancelación de citas' WHERE Id= '2497' AND name = 'Causas de Cancelación de Citas'
UPDATE security.form  SET Name = 'Causas de cancelación de citas en espera' WHERE Id= '2498' AND name = 'Causas de Cancelación de Citas en Espera'
UPDATE security.form  SET Name = 'Especialidades - actividades' WHERE Id= '2500' AND name = 'Especialidades-Actividades'
UPDATE security.form  SET Name = 'Asignación de citas' WHERE Id= '2503' AND name = 'Asignación de Citas'
UPDATE security.form  SET Name = 'Citas preasignadas' WHERE Id= '2504' AND name = 'Citas Preasignadas'
UPDATE security.form  SET Name = 'Parámetros agendamiento' WHERE Id= '2505' AND name = 'Parámetros Agendamiento'
UPDATE security.form  SET Name = 'Reportes agendamiento' WHERE Id= '2506' AND name = 'Reportes Agendamiento'
UPDATE security.form  SET Name = 'Reportes oportunidad citas' WHERE Id= '2507' AND name = 'Reportes Oportunidad Citas'
UPDATE security.form  SET Name = 'Trazabilidad agendamiento' WHERE Id= '2508' AND name = 'Trazabilidad Agendamiento'
UPDATE security.form  SET Name = 'Asignación de citas apoyo diagnóstico' WHERE Id= '2510' AND name = 'Asignación de Citas Apoyo Diagnóstico'
UPDATE security.form  SET Name = 'Paraclínicos ambulatorios' WHERE Id= '2513' AND name = 'Paraclínicos Ambulatorios'
UPDATE security.form  SET Name = 'Componentes sanguíneos' WHERE Id= '2514' AND name = 'Componentes Sanguíneos'
UPDATE security.form  SET Name = 'Dashboard recepción de referencias' WHERE Id= '2546' AND name = 'Dashboard Recepción de Referencias'
UPDATE security.form  SET Name = 'Tratamientos y diagnósticos odontológicos' WHERE Id= '2548' AND name = 'Tratamientos y Diagnósticos Odontológicos'
UPDATE security.form  SET Name = 'Areas institucionales' WHERE Id= '2559' AND name = 'Areas Institucionales'
UPDATE security.form  SET Name = 'Recomendaciones médicas' WHERE Id= '2561' AND name = 'Recomendaciones Médicas'
UPDATE security.form  SET Name = 'Configurar analitos' WHERE Id= '2572' AND name = 'Configurar Analitos'
UPDATE security.form  SET Name = 'Motivos no cumplimiento tratamiento odontológico' WHERE Id= '2591'  AND name = 'Motivos no Cumplimiento Tratamiento Odontológico'
UPDATE security.form  SET Name = 'Disponibilidad sala de apoyo diagnóstico' WHERE Id= '2593' AND name = 'Disponibilidad sala de apoyo DX'
UPDATE security.form  SET Name = 'Dashboard atención domiciliaria' WHERE Id= '2600' AND name = 'Dashboard Atención Domiciliaria'
UPDATE security.form  SET Name = 'Registrar eventos' WHERE Id= '2611' AND name = 'Registrar Eventos'
UPDATE security.form  SET Name = 'Control servicios ambulatorios' WHERE Id= '2612' AND name = 'Control Servicios Ambulatorios'
UPDATE security.form  SET Name = 'Equipos de tratamiento' WHERE Id= '2639' AND name = 'Equipos de Tratamiento'
UPDATE security.form  SET Name = 'Unidades funcionales' WHERE Id= '2647' AND name = 'Unidades Funcionales'
UPDATE security.form  SET Name = 'Unidades funcionales' WHERE Id= '2648' AND name = 'Unidades Funcionales'
UPDATE security.form  SET Name = 'Instituciones prestadoras de servicio de salud (IPS)' WHERE Id= '2661' AND name = 'IPS'
UPDATE security.form  SET Name = 'Instituciones prestadoras de servicio de salud (IPS)' WHERE Id= '2662' AND name = 'IPS'
UPDATE security.form  SET Name = 'Paquete órdenes' WHERE Id= '2669' AND name = 'Paquete Ordenes'
UPDATE security.form  SET Name = 'Parametrización control de exámen físico' WHERE Id= '2671' AND name = 'Parametrización Control de Exámen Físico'
UPDATE security.form  SET Name = 'Áreas y otros procedimientos' WHERE Id= '2682' AND name = 'Areas Otros Procedimientos'
UPDATE security.form  SET Name = 'Rutas integrales de atención en salud (RIAS)' WHERE Id= '2685' AND name = 'RIAS'
UPDATE security.form  SET Name = 'Registro egreso plan atención domiciliario (PAD)' WHERE Id= '2691' AND name = 'Registro egreso PAD'
UPDATE security.form  SET Name = 'Modificación de reportes' WHERE Id= '2693' AND name = 'Modificación de Reportes'
UPDATE security.form  SET Name = 'Profesionales de la salud' WHERE Id= '2695' AND name = 'Profesionales de la Salud'
UPDATE security.form  SET Name = 'Clasificación triage' WHERE Id= '2696' AND name = 'Clasificación Triage'
UPDATE security.form  SET Name = 'Control pacientes' WHERE Id= '2697' AND name = 'Control Pacientes'
UPDATE security.form  SET Name = 'Control triage' WHERE Id= '2698' AND name = 'Control Triage'
UPDATE security.form  SET Name = 'Agregar medicamentos esquemas' WHERE Id= '2704' AND name = 'Agregar Medicamentos Esquemas'
UPDATE security.form  SET Name = 'Tipo identificación' WHERE Id= '2705' AND name = 'Tipo Identificación'
UPDATE security.form  SET Name = 'Motivo suspensión' WHERE Id= '2708' AND name = 'Motivo Suspensión'
UPDATE security.form  SET Name = 'Dashboard pacientes por facturar' WHERE Id= '2719' AND name = 'Dashboard Pacientes por Facturar'
UPDATE security.form  SET Name = 'Estructura operativa - centros de atención' WHERE Id= '2721' AND name = 'Estructura Operativa - Centros de Atención'
UPDATE security.form  SET Name = 'Tipos de salarios' WHERE Id= '2725' AND name = 'Tipos de Salarios'
UPDATE security.form  SET Name = 'Hallazgos clínicos' WHERE Id= '2811' AND name = 'Hallazgos Clínicos'
UPDATE security.form  SET Name = 'Registrar pre-triage - pre-consulta' WHERE Id= '88003' AND name = 'Registrar Pre-Triage / Pre-Consulta'
UPDATE security.form  SET Name = 'Parametrizador IPS prestadora triage 4 y 5' WHERE Id= '88004' AND name = 'Parametrizador IPS Prestadora Triage 4 y 5'
UPDATE security.form  SET Name = 'Factores de riesgo' WHERE Id= '88008' AND name = 'Factores de Riesgo'
UPDATE security.form  SET Name = 'Tipos de bombas de infusión' WHERE Id= '88014' AND name = 'Tipos de Bombas de Infusión'
UPDATE security.form  SET Name = 'Modificación de reportes' WHERE Id= '88819' AND name = 'Modificación de Reportes'
UPDATE security.form  SET Name = 'Grupo otros' WHERE Id= '88820' AND name = 'Grupo Otros'
UPDATE security.form  SET Name = 'Variables otros' WHERE Id= '88821' AND name = 'Variables Otros'
UPDATE security.form  SET Name = 'Grupo lista chequeo' WHERE Id= '88824' AND name = 'Grupo Lista Chequeo'
UPDATE security.form  SET Name = 'Variables lista de chequeo' WHERE Id= '88825' AND name = 'Variables Lista de Chequeo'UPDATE security.form  SET Name = 'Dashboard médico' WHERE Id= '1534' AND name = 'Dashboard Médico'
UPDATE security.form  SET Name = 'Actividades de enfermería' WHERE Id= '1535' AND name = 'Actividades de Enfermería'
UPDATE security.form  SET Name = 'Categorias anestesia' WHERE Id= '1536' AND name = 'Categorias Anestesia'
UPDATE security.form  SET Name = 'Categoría documentos' WHERE Id= '1537' AND name = 'Categoría Documentos'
UPDATE security.form  SET Name = 'Centros de remisión' WHERE Id= '1538' AND name = 'Centros de Remisión'
UPDATE security.form  SET Name = 'Concepto de ajustes' WHERE Id= '1539' AND name = 'Concepto de Ajustes'
UPDATE security.form  SET Name = 'Creación de encuestas' WHERE Id= '1540' AND name = 'Creación de Encuestas'
UPDATE security.form  SET Name = 'Criterios eliminación alertas' WHERE Id= '1542' AND name = 'Criterios Eliminación Alertas'
UPDATE security.form  SET Name = 'Formas del medicamento' WHERE Id= '1544' AND name = 'Formas del Medicamento'
UPDATE security.form  SET Name = 'Liquidos eliminados' WHERE Id= '1545' AND name = 'Liquidos Eliminados'

--BUG 20184 Error de diseño n° 34
--Scrips para corregir la ortografia de ciertos formularios 
update security.form set Name = 'Administrar reservas' where Id = '1602' and name = 'Administrar Reservas'
update security.form set Name = 'Disponibilidad de camas' where Id = '1606' and name = 'Disponibilidad de Camas'

-- PBI 21411  Gestión de firma electrónica a través de los Módulos asistenciales  - Consentimiento informado
-- Se actualiza el formulario que se abre al momento de seleccionar 'Consentimiento Informado' desde el menu del aplicativo
Update Security.Form set ClassName = 'frmHCPlantillasConsentimientosInformados', AssemblyName = 'Indigo.Emergentes.dll' where Id = '1560'

--PBI-27171 INT-Ocultar formulario "Traslado de agenda"--
UPDATE Security.Form set State = 0 where id = 2020

--PBI 28017 Modificaciones formulario parametrizar nutriciones parenterales.
Update Security.Form SET Name = 'Plantillas NPT' WHERE id = '2712'


----------------------------------------------------------------  Sprint Week 24 - 25 (2026)  -----------------------------------------------------------------------
-- PBI 32756 Creacion del formulario Tipo de usuario

IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 89044)
INSERT INTO Security.Form VALUES (89044, 'Tipo de usuario', 'SUCA', 0, 0, 1, 'frmADTipoUsuario','Indigo.Admisiones.dll', 0, '', 1);

IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 89044 AND IdAction = 2)
INSERT INTO Security.FormAction VALUES (89044, 2);   -- Guardar
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 89044 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (89044, 3);   -- Actualizar
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 89044 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (89044, 40);  -- Consultar
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 89044 AND IdAction = 41)
INSERT INTO Security.FormAction VALUES (89044, 41);  -- Visible

IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 58 AND IdTitle = 29 AND IdForm = 89044)
INSERT INTO Security.ModuleForm VALUES (58, 29, 89044, 27); 

--------------------------------------------------------------  Type Servicios Susceptible Sprint Week 26 - 27 (2026) ---------------------------------------

-- 1. Actualizar nombre del formulario
UPDATE Security.Form SET Name = 'Servicios Susceptibles de Autorización' WHERE ClassName = 'FrmConfigurationServicesAmbulatory'

-- 2. Eliminar acciones del formulario a borrar (primero los hijos)
DELETE Security.FormAction WHERE IdForm = (SELECT Id FROM Security.Form WHERE ClassName = 'FrmADParametrizarSeriviciosSuceptibles')

-- 3. Eliminar modulos del formulario a borrar (primero los hijos)
DELETE Security.ModuleForm WHERE IdForm = (SELECT Id FROM Security.Form WHERE ClassName = 'FrmADParametrizarSeriviciosSuceptibles')

-- 4. Eliminar el formulario
DELETE Security.Form WHERE ClassName = 'FrmADParametrizarSeriviciosSuceptibles'

----------------------------------------------------------------  Sprint Week 26 - 27 (2026)  -----------------------------------------------------------------------
-- PBI 36565 Creacion de formulario para notas aclaratorias

IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 89047)
INSERT INTO Security.Form VALUES (89047, 'Notas aclaratorias', 'SUCA', 0, 0, 1, 'FrmCALRptNotaAclaratoriaRDA','Indigo-Calidad.dll', 0, '', 1);

IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 89044 AND IdAction = 2)
INSERT INTO Security.FormAction VALUES (89044, 2);   -- Guardar
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 89047 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (89047, 40);  -- Consultar
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 89047 AND IdAction = 41)
INSERT INTO Security.FormAction VALUES (89047, 41);  -- Visible

IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 14 AND IdTitle = 30 AND IdForm = 89047)
INSERT INTO Security.ModuleForm VALUES (14, 30, 89047, 17);

----------------------------------------------------------------  Sprint Week 28 - 29 (2026)  -----------------------------------------------------------------------
-- Formulario principal Dashboard Autorizaciones Intrahospitalarias

IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 89045)
INSERT INTO Security.Form VALUES (89045, 'Dashboard autorizaciones intrahospitalario', 'SUCA', 0, 0, 1, 'FrmDashBoardAutorizacionIntrahospitalarioV2', 'Indigo.Admisiones.dll', 0, 'Authorization', 1);

-- Acciones (mismas que el form legado 1641): 1=Nuevo, 2=Guardar, 3=Eliminar, 40=Consultar, 41=Visible
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 89045 AND IdAction = 1)
INSERT INTO Security.FormAction VALUES (89045, 1);
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 89045 AND IdAction = 2)
INSERT INTO Security.FormAction VALUES (89045, 2);
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 89045 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (89045, 3);
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 89045 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (89045, 40);
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 89045 AND IdAction = 41)
INSERT INTO Security.FormAction VALUES (89045, 41);

-- Módulo: IdModule=60, IdTitle=30 (igual que form 1641), FormOrder a continuación del existente
IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 60 AND IdTitle = 30 AND IdForm = 89045)
INSERT INTO Security.ModuleForm VALUES (60, 30, 89045, 7);

-- Reactivar autorización intrahospitalaria
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88984)
INSERT INTO Security.Form VALUES (88984,'Autorización intrahospitalaria: Reactivar','SUCA','0','0','0',NULL,NULL,'0','','1')
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88984 AND IdAction = 41)
INSERT INTO Security.FormAction VALUES (88984,41)

-- Entregar servicio al área solicitante
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88985)
INSERT INTO Security.Form VALUES (88985,'Autorización intrahospitalaria: Entregar servicio','SUCA','0','0','0',NULL,NULL,'0','','1')
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88985 AND IdAction = 41)
INSERT INTO Security.FormAction VALUES (88985,41)

-- Generar anexo Decreto 3047
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88986)
INSERT INTO Security.Form VALUES (88986,'Autorización intrahospitalaria: Generar anexo','SUCA','0','0','0',NULL,NULL,'0','','1')
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88986 AND IdAction = 41)
INSERT INTO Security.FormAction VALUES (88986,41)

-- Reasignar autorización intrahospitalaria
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88987)
INSERT INTO Security.Form VALUES (88987,'Autorización intrahospitalaria: Reasignar','SUCA','0','0','0',NULL,NULL,'0','','1')
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88987 AND IdAction = 41)
INSERT INTO Security.FormAction VALUES (88987,41)

-- Cancelar solicitud de autorización intrahospitalaria
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88988)
INSERT INTO Security.Form VALUES (88988,'Autorización intrahospitalaria: Cancelar Solicitud','SUCA','0','0','0',NULL,NULL,'0','','1')
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88988 AND IdAction = 41)
INSERT INTO Security.FormAction VALUES (88988,41)

-- Agregar evento de trámite Decreto 3047
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88989)
INSERT INTO Security.Form VALUES (88989,'Autorización intrahospitalaria: Agregar evento','SUCA','0','0','0',NULL,NULL,'0','','1')
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88989 AND IdAction = 41)
INSERT INTO Security.FormAction VALUES (88989,41)

-- Imprimir reporte de autorización intrahospitalaria
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 88990)
INSERT INTO Security.Form VALUES (88990,'Autorización intrahospitalaria: Imprimir reporte','SUCA','0','0','0',NULL,NULL,'0','','1')
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 88990 AND IdAction = 41)
INSERT INTO Security.FormAction VALUES (88990,41)


-- Define el formulario para el 'Parámetros de Gestión de Autorización', lo asocia a múltiples módulos y asigna acciones.
IF NOT EXISTS (SELECT 1 FROM Security.Form WHERE Id = 89048)
INSERT INTO Security.Form VALUES (89048, 'Parámetros de Gestión de Autorización', 'SUCA', 0, 0, 1, 'FrmParametrosGestionAutorizacion', 'Indigo.Admisiones.dll', 0, '', 1);
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 89048 AND IdAction = 2)
INSERT INTO Security.FormAction VALUES (89048, 2);   -- Guardar
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 89048 AND IdAction = 3)
INSERT INTO Security.FormAction VALUES (89048, 3);   -- Actualizar
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 89048 AND IdAction = 40)
INSERT INTO Security.FormAction VALUES (89048, 40);  -- Consultar
IF NOT EXISTS (SELECT 1 FROM Security.FormAction WHERE IdForm = 89048 AND IdAction = 41)
INSERT INTO Security.FormAction VALUES (89048, 41);  -- Visible

IF NOT EXISTS (SELECT 1 FROM Security.ModuleForm WHERE IdModule = 60 AND IdTitle = 5 AND IdForm = 89048)
INSERT INTO Security.ModuleForm VALUES (60, 5, 89048, 5) 

-- Se actualiza el formulario que se abre al momento de seleccionar 'Parámetros enfermeria' desde el menu del aplicativo
UPDATE security.form SET ClassName = 'frmHCParametrosEnfermeria' WHERE Id = '2311' AND Name = 'Parámetros enfermeria'
--- Se actualiza la No vizualizacion de la Escala de riesgo suicida de Plutchik en el compilado  fecha: 5 / 08/ 2026
--Modulo
UPDATE Security.ModuleForm SET IdModule = 100, IdTitle = 5, FormOrder = 1 WHERE IdForm = '89041'
Update Security.FormAction set IdAction = 23  where Id = '55210' and IdForm = '89041'

----------------------------------------------------------------  Sprint Week 32 - 33 (2026)  -----------------------------------------------------------------------
---- PBI 37496 - Agregar proceso de FUR al formulario de secuencias numéricas
UPDATE Security.Form SET HasSequence = 1, SequenceModule = 'Admissions' WHERE Id = '89043'

---------------------se setan permisos a formulario obsoleto ( frmHCParametrosEnfermeria ) e inhabilita

DELETE FROM Security.FormAction WHERE IdForm = 2311 AND IdAction = 41;

update Security.Form set state= 0 where id = 2311

--------------------------------------------------------------  Delete indicaciones manejo Form Sprint Week 34 - 35 (2026) ---------------------------------------
-- 1. Eliminar acciones del formulario a borrar (primero los hijos)
DELETE Security.FormAction WHERE IdForm = (SELECT Id FROM Security.Form WHERE ClassName = 'frmCHIndicacionesManejo')

-- 2. Eliminar modulos del formulario a borrar (primero los hijos)
DELETE Security.ModuleForm WHERE IdForm = (SELECT Id FROM Security.Form WHERE ClassName = 'frmCHIndicacionesManejo')

-- 3. Eliminar el formulario
DELETE Security.Form WHERE ClassName = 'frmCHIndicacionesManejo'

--------------------------------------------------------------  Delete old liquidos e infusiones Sprint Week 34 - 35 (2026) ---------------------------------------

delete from Security.ModuleForm where IdForm = 2314
delete from Security.FormAction where IdForm = 2314
delete from Security.form WHERE id = 2314