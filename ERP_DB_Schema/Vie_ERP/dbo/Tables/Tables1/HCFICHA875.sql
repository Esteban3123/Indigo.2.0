CREATE TABLE [dbo].[HCFICHA875] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [VIONOSEXUAL]         BIT           NULL,
    [CUALVIONOSEXUAL]     INT           NULL,
    [VIOSEXUAL]           BIT           NULL,
    [CUALVIOSEXUAL]       INT           NULL,
    [ACTIVIDAD]           INT           NULL,
    [ORIENTASEXUAL]       INT           NULL,
    [IDENTGENERO]         INT           NULL,
    [CONSUMOSPA]          BIT           NULL,
    [MUJERCABFAMI]        BIT           NULL,
    [ANTEVIOLENCIA]       BIT           NULL,
    [ALCOHOLVIC]          BIT           NULL,
    [EDAD]                NUMERIC (18)  NULL,
    [SEXO]                INT           NULL,
    [PARENTVICTIMA]       INT           NULL,
    [CONVIVEAGRE]         BIT           NULL,
    [AGRESORNOFA]         INT           NULL,
    [CONFICTOARM]         BIT           NULL,
    [MECANISMOAGRE]       INT           NULL,
    [SITIOANATOMICO]      INT           NULL,
    [GRADO]               INT           NULL,
    [EXTENSION]           INT           NULL,
    [FECHAHECHO]          DATETIME      NULL,
    [ESCENARIO]           INT           NULL,
    [VIOLENLUGAR]         INT           NULL,
    [PROFIVIH]            BIT           NULL,
    [PROFIHEPB]           BIT           NULL,
    [OTRASPROFI]          BIT           NULL,
    [ANTICONEMER]         BIT           NULL,
    [ORIENTAIVE]          BIT           NULL,
    [SALUDMENTAL]         BIT           NULL,
    [REMIPROTEC]          BIT           NULL,
    [INFORMEAUTO]         BIT           NULL,
    [EVIDENCIAMED]        BIT           NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA875] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA875_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA875_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA875] NOCHECK CONSTRAINT [CK_HCFICHA875_JSON];


GO
ALTER TABLE [dbo].[HCFICHA875] NOCHECK CONSTRAINT [FK_HCFICHA875_HCFICHANOTIFICACION];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nuevas columnas en formato JSON ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el codigo del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Recolección evidencia médico legal:   1 = Si    0 =No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'EVIDENCIAMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la 9.8 Informe autoridades/denuncia a policía judicial:   1 = Si   0 =No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'INFORMEAUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Remisión a protección:  1 = Si   0 =No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'REMIPROTEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Salud mental:  1 =Si    0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'SALUDMENTAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Orientación IVE:  1 = Si     0= No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'ORIENTAIVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Anticoncepción de emergencia:  1 = Si  0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'ANTICONEMER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Otras profilaxis:   1 =  Si  0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'OTRASPROFI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Profilaxis Hep B:  1 = Si   0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'PROFIHEPB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Profilaxis VIH:  1 = Si   0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'PROFIVIH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ámbito de violencia según lugar:   1 - Escolar   2 - Laboral   3 - Institucional   4 - Virtual   5 - Otro ---> (item eliminado, desde versión V01_2020-03-06)  6 - Comunitario   7 - Hogar   8 - Otros ámbitos   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'VIOLENLUGAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Escenario:   <items 3,4,6 y 7 - modificados desde versión V01_2020-03-06 >  <items 9 y 10 - nuevos desde versión V01_2020-03-06 >  1 - Vía pública   2 - Vivienda   3 - Establecimiento educativo   4 - Lugar de trabajo   5 - Otro   6 - Comercio y áreas de servicios (Tienda, centro comercial, etc)   7 - Otros espacios abiertos (bosques, potreros, etc)   8 - Lugares de esparcimiento con expendido de alcohol   9 - Institución de Salud   10 - Área deportiva y recreativa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'ESCENARIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Fecha del hecho', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'FECHAHECHO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Extensión:  1 = Menor o igual al 5%    2 =Del 6% al 14%    3 =Mayor o igual al 15% ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'EXTENSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el grado  1 = Primer grado    2 = Segundo grado    3 = Tercer grado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'GRADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Sitio anátomico comprometido con quemadura:   1 =cara   2 =cuello    3 =manos    4 =pies     5 = Pliegues   6 =Genitales  7 =Tronco   8 =Miembro superior  9 = Miembro inferior', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'SITIOANATOMICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Mecanismo utlizado para agresión:   1 =Ahorcamiento / estrangulamiento / sofocación    2 =Caídas    3 =Contundente / cortocondundente    4 = Cortante / cortopunzante / Punzante  5 =Proyectil arma fuego  6 =Quemadura por fuego o llama   7 =Quemadura por ácido, álcalis, o sustancias corrosivas  8 =Quemadura con líquido hirviente   9 =Otros mecanismos  10 =Sustancias de uso doméstico que causan irritación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'MECANISMOAGRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  ¿Hecho violento ocurrido en el marco del conflicto armado?  1 = Si   0 =No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'CONFICTOARM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Agresor no familiar:  1 =Profesor (a)    2 =Amigo (a)    3 =Compañero (a) de trabajo    4 = Compañero (a) de estudio   5 =Desconocido (a)    6 =Vecino (a)   7 =Conocido (a) sin ningún trato      8 =Sin información  9 = Otro  10 =Jefe  11 =Sacerdote / pastor   12 =Servidor (a) público   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'AGRESORNOFA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Convive con el agresor (a):   1 =Si     0 =No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'CONVIVEAGRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Parentesco con la víctima:  1 =Padre    2 =Madre    3 =Pareja   4 = Ex-Pareja   5 =Familiar    6 =Ninguno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'PARENTVICTIMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sexo (Datos del Agresor) :  1 - Masculino   2 - Femenino   < item 3 y 4 modificados desde versión V01_2020-03-06 >  4 - Intersexual   3 o Nulo - Sin Dato   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'SEXO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Edad ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'EDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Alcohol víctima  1 = Si   0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'ALCOHOLVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Antecedente de violencia   1 = Si    0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'ANTEVIOLENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Persona con jefatura de hogar:  1 = Si   0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'MUJERCABFAMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Persona consumidora de SPA:   1 = Si  0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'CONSUMOSPA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la 6.2.1 Identidad de género:  1 =Masculino   2 =Femenino    3 =Transgénero', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'IDENTGENERO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Orientación sexual:   1 = Homosexual    2 =Bisexual    3 =heterosexual   4 =Asexual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'ORIENTASEXUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Actividad:   <items 5 y 7 - modificados desde versión V01_2020-03-06 >  1 - Líderes(as) cívicos   2 - Estudiante   3 - Otro   4 - Trabajador(a) doméstico(a)   5 - Persona en situación de prostitución   6 - Campesino(a)  7 - Persona dedicada al cuidado del hogar   8 - Persona que cuida a otras   9 - Ninguna ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'ACTIVIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cual Violencia Sexual:   <item 1 - eliminado desde versión V01_2020-03-06 >   <items 3, 4, 5, 6 y 7 - modificados desde versión V01_2020-03-06 >   <item 8 - nuevo desde versión V01_2020-03-06 >   1 - Abuso sexual   2 - Acoso sexual   3 - Acceso carnal   4 - Explotación sexual   5 - Trata de personas   6 - Actos sexuales   8 - Otras violencias sexuales   9 - Mutilación genital    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'CUALVIOSEXUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda violencia sexual  1 = Si   0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'VIOSEXUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda cual violencia no sexual   1 =Física    2 = Psicológica    3 =Negligencia y abandono', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'CUALVIONOSEXUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Violencia no sexual:   1= Si     0= No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'VIONOSEXUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  el Id de la ficha de notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'ID';

