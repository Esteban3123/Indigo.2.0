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
ALTER TABLE [dbo].[HCFICHA875] NOCHECK CONSTRAINT [CK_HCFICHA875_JSON];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacenamiento de nuevas columnas en formato JSON validado (ISJSON); extensible para futuras versiones de la ficha de notificación de violencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nuevas columnas en formato JSON ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión de la ficha de notificación; nulo indica primera versión (V01_2020-03-06 o anterior); identifica cambios en ítems y estructuras.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico CIE-10 (4 caracteres) registrado en la atención de la víctima de violencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el codigo del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recolección de evidencia médico-legal (BIT): 1=Sí, 0=No; marca si se documentó evidencia física del evento violento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'EVIDENCIAMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la  Recolección evidencia médico legal:   1 = Si    0 =No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'EVIDENCIAMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'EVIDENCIAMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Informe a autoridades/denuncia a policía judicial (BIT): 1=Sí, 0=No; indica notificación a entidades de investigación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'INFORMEAUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la 9.8 Informe autoridades/denuncia a policía judicial:   1 = Si   0 =No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'INFORMEAUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'INFORMEAUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Remisión a protección/medidas de protección (BIT): 1=Sí, 0=No; especifica si se derivó a servicios de protección de víctima.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'REMIPROTEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la  Remisión a protección:  1 = Si   0 =No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'REMIPROTEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'REMIPROTEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Intervención en salud mental/apoyo psicosocial (BIT): 1=Sí, 0=No; marca atención en salud mental registrada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'SALUDMENTAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la  Salud mental:  1 =Si    0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'SALUDMENTAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'SALUDMENTAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Orientación en interrupción voluntaria del embarazo (IVE) (BIT): 1=Sí, 0=No; si aplica en casos de violencia sexual.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'ORIENTAIVE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la  Orientación IVE:  1 = Si     0= No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'ORIENTAIVE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'ORIENTAIVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Administración de anticoncepción de emergencia (BIT): 1=Sí, 0=No; profilaxis post exposición en violencia sexual.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'ANTICONEMER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Anticoncepción de emergencia:  1 = Si  0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'ANTICONEMER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'ANTICONEMER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Otras profilaxis administradas (BIT): 1=Sí, 0=No; profilaxis distintas a VIH o Hep B tras agresión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'OTRASPROFI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Otras profilaxis:   1 =  Si  0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'OTRASPROFI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'OTRASPROFI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Profilaxis Hepatitis B postexposición (BIT): 1=Sí, 0=No; quimioprofilaxis tras violencia sexual o contacto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'PROFIHEPB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Profilaxis Hep B:  1 = Si   0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'PROFIHEPB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'PROFIHEPB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Profilaxis VIH postexposición (BIT): 1=Sí, 0=No; antirretrovirales preventivos post-agresión sexual.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'PROFIVIH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la  Profilaxis VIH:  1 = Si   0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'PROFIVIH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'PROFIVIH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ámbito de violencia según lugar (INT 1-8): 1=Escolar, 2=Laboral, 3=Institucional, 4=Virtual, 6=Comunitario, 7=Hogar, 8=Otros; clasificación geográfica/social del evento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'VIOLENLUGAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ámbito de violencia según lugar:   1 - Escolar   2 - Laboral   3 - Institucional   4 - Virtual   5 - Otro ---> (item eliminado, desde versión V01_2020-03-06)  6 - Comunitario   7 - Hogar   8 - Otros ámbitos   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'VIOLENLUGAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'VIOLENLUGAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Escenario físico del evento violento (INT 1-10): vía pública, vivienda, educativo, laboral, comercio, espacios abiertos, esparcimiento, salud, deportivo; lugar específico de ocurrencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'ESCENARIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Escenario:   <items 3,4,6 y 7 - modificados desde versión V01_2020-03-06 >  <items 9 y 10 - nuevos desde versión V01_2020-03-06 >  1 - Vía pública   2 - Vivienda   3 - Establecimiento educativo   4 - Lugar de trabajo   5 - Otro   6 - Comercio y áreas de servicios (Tienda, centro comercial, etc)   7 - Otros espacios abiertos (bosques, potreros, etc)   8 - Lugares de esparcimiento con expendido de alcohol   9 - Institución de Salud   10 - Área deportiva y recreativa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'ESCENARIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'ESCENARIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del hecho violento (DATETIME); registro del evento para auditoría clínica y análisis epidemiológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'FECHAHECHO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la  Fecha del hecho', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'FECHAHECHO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'FECHAHECHO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Extensión de quemadura (INT 1-3): 1=≤5%, 2=6-14%, 3=≥15%; porcentaje de superficie corporal afectada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'EXTENSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la  Extensión:  1 = Menor o igual al 5%    2 =Del 6% al 14%    3 =Mayor o igual al 15% ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'EXTENSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'EXTENSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grado de quemadura (INT 1-3): 1=Primer grado, 2=Segundo grado, 3=Tercer grado; profundidad tisular del daño.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'GRADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el grado  1 = Primer grado    2 = Segundo grado    3 = Tercer grado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'GRADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'GRADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sitio anatómico comprometido con quemadura (INT 1-9): cara, cuello, manos, pies, pliegues, genitales, tronco, miembro superior, inferior; localización de lesión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'SITIOANATOMICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Sitio anátomico comprometido con quemadura:   1 =cara   2 =cuello    3 =manos    4 =pies     5 = Pliegues   6 =Genitales  7 =Tronco   8 =Miembro superior  9 = Miembro inferior', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'SITIOANATOMICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'SITIOANATOMICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mecanismo de agresión utilizado (INT 1-10): ahorcamiento, caídas, contundente, cortante, arma de fuego, quemadura, ácido, líquido hirviente, otros, irritantes; especifica cómo se ejecutó la violencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'MECANISMOAGRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Mecanismo utlizado para agresión:   1 =Ahorcamiento / estrangulamiento / sofocación    2 =Caídas    3 =Contundente / cortocondundente    4 = Cortante / cortopunzante / Punzante  5 =Proyectil arma fuego  6 =Quemadura por fuego o llama   7 =Quemadura por ácido, álcalis, o sustancias corrosivas  8 =Quemadura con líquido hirviente   9 =Otros mecanismos  10 =Sustancias de uso doméstico que causan irritación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'MECANISMOAGRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'MECANISMOAGRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hecho violento ocurrido en marco de conflicto armado (BIT): 1=Sí, 0=No; contexto de seguridad pública.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'CONFICTOARM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  ¿Hecho violento ocurrido en el marco del conflicto armado?  1 = Si   0 =No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'CONFICTOARM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'CONFICTOARM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agresor no familiar (INT 1-12): profesor, amigo, compañero trabajo/estudio, desconocido, vecino, conocido, sin info, otro, jefe, sacerdote, servidor público; relación del agresor con víctima.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'AGRESORNOFA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Agresor no familiar:  1 =Profesor (a)    2 =Amigo (a)    3 =Compañero (a) de trabajo    4 = Compañero (a) de estudio   5 =Desconocido (a)    6 =Vecino (a)   7 =Conocido (a) sin ningún trato      8 =Sin información  9 = Otro  10 =Jefe  11 =Sacerdote / pastor   12 =Servidor (a) público   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'AGRESORNOFA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'AGRESORNOFA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Convive con el agresor (BIT): 1=Sí, 0=No; indicador de riesgo de revictimización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'CONVIVEAGRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Convive con el agresor (a):   1 =Si     0 =No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'CONVIVEAGRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'CONVIVEAGRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parentesco del agresor con la víctima (INT 1-6): padre, madre, pareja, ex-pareja, familiar, ninguno; relación consanguínea/legal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'PARENTVICTIMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Parentesco con la víctima:  1 =Padre    2 =Madre    3 =Pareja   4 = Ex-Pareja   5 =Familiar    6 =Ninguno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'PARENTVICTIMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'PARENTVICTIMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sexo del agresor (INT 1-4; nulo=sin dato): 1=Masculino, 2=Femenino, 3=Sin dato, 4=Intersexual; características demográficas del perpetrador.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'SEXO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sexo (Datos del Agresor) :  1 - Masculino   2 - Femenino   < item 3 y 4 modificados desde versión V01_2020-03-06 >  4 - Intersexual   3 o Nulo - Sin Dato   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'SEXO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'SEXO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad del agresor (NUMERIC); rango etario para análisis de perfil delictivo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'EDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la Edad ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'EDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'EDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consumo de alcohol en víctima (BIT): 1=Sí, 0=No; factor de riesgo/contexto del evento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'ALCOHOLVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Alcohol víctima  1 = Si   0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'ALCOHOLVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'ALCOHOLVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente de violencia previa (BIT): 1=Sí, 0=No; historial de reincidencia o ciclo de violencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'ANTEVIOLENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Antecedente de violencia   1 = Si    0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'ANTEVIOLENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'ANTEVIOLENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Persona con jefatura de hogar/sostén económico (BIT): 1=Sí, 0=No; vulnerabilidad socioeconómica de víctima.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'MUJERCABFAMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Persona con jefatura de hogar:  1 = Si   0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'MUJERCABFAMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'MUJERCABFAMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Persona consumidora de sustancias psicoactivas (BIT): 1=Sí, 0=No; factor de vulnerabilidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'CONSUMOSPA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Persona consumidora de SPA:   1 = Si  0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'CONSUMOSPA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'CONSUMOSPA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identidad de género de víctima (INT 1-3): 1=Masculino, 2=Femenino, 3=Transgénero; autodeterminación de género.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'IDENTGENERO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la 6.2.1 Identidad de género:  1 =Masculino   2 =Femenino    3 =Transgénero', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'IDENTGENERO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'IDENTGENERO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Orientación sexual de víctima (INT 1-4): 1=Homosexual, 2=Bisexual, 3=Heterosexual, 4=Asexual; variable de análisis de discriminación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'ORIENTASEXUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la  Orientación sexual:   1 = Homosexual    2 =Bisexual    3 =heterosexual   4 =Asexual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'ORIENTASEXUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'ORIENTASEXUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Actividad u ocupación de víctima (INT 1-9): líder cívico, estudiante, doméstico, prostitución, campesino, cuidado hogar, cuidador, otra, ninguna; caracterización social.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'ACTIVIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Actividad:   <items 5 y 7 - modificados desde versión V01_2020-03-06 >  1 - Líderes(as) cívicos   2 - Estudiante   3 - Otro   4 - Trabajador(a) doméstico(a)   5 - Persona en situación de prostitución   6 - Campesino(a)  7 - Persona dedicada al cuidado del hogar   8 - Persona que cuida a otras   9 - Ninguna ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'ACTIVIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'ACTIVIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de violencia sexual (INT 1-9): abuso, acoso, acceso carnal, explotación, trata, actos sexuales, mutilación genital, otras sexuales; especificación del delito sexual.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'CUALVIOSEXUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cual Violencia Sexual:   <item 1 - eliminado desde versión V01_2020-03-06 >   <items 3, 4, 5, 6 y 7 - modificados desde versión V01_2020-03-06 >   <item 8 - nuevo desde versión V01_2020-03-06 >   1 - Abuso sexual   2 - Acoso sexual   3 - Acceso carnal   4 - Explotación sexual   5 - Trata de personas   6 - Actos sexuales   8 - Otras violencias sexuales   9 - Mutilación genital    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'CUALVIOSEXUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'CUALVIOSEXUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presencia de violencia sexual en el evento (BIT): 1=Sí, 0=No; indicador de delito sexual.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'VIOSEXUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda violencia sexual  1 = Si   0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'VIOSEXUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'VIOSEXUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de violencia no sexual (INT 1-3): 1=Física, 2=Psicológica, 3=Negligencia/abandono; clasificación de agresión no sexual.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'CUALVIONOSEXUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda cual violencia no sexual   1 =Física    2 = Psicológica    3 =Negligencia y abandono', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'CUALVIONOSEXUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'CUALVIONOSEXUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presencia de violencia no sexual en el evento (BIT): 1=Sí, 0=No; indicador de agresión no sexual.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'VIONOSEXUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Violencia no sexual:   1= Si     0= No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'VIONOSEXUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'VIONOSEXUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la ficha de notificación de violencia (FK a HCFICHANOTIFICACION); vinculación a evento de notificación obligatoria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  el Id de la ficha de notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único/consecutivo de registro (INT IDENTITY); clave primaria de la tabla HCFICHA875.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro del formulario 875 de notificación de violencia sexual y de género (Ficha 875 del SIVIGILA). Guarda los datos de la víctima, el agresor, el tipo de violencia, las lesiones, el escenario del hecho y las medidas de atención tomadas en salud pública.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA875';
