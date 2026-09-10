CREATE TABLE [dbo].[HCFICHA307] (
    [ID]                     INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION]    INT           NOT NULL,
    [TIPOAGRESION]           INT           NULL,
    [AGRESIONPROVOCADA]      BIT           NULL,
    [TIPOLESION]             INT           NULL,
    [PROFUNDIDAD]            INT           NULL,
    [CABEZA]                 BIT           NULL,
    [MANOS]                  BIT           NULL,
    [TRONCO]                 BIT           NULL,
    [MIEMBROSSUPERI]         BIT           NULL,
    [MIEMBROSINFERI]         BIT           NULL,
    [PIES]                   BIT           NULL,
    [GENITALESEXTERN]        BIT           NULL,
    [FECHAAGRESION]          DATE          NULL,
    [ESPECIEAGRESORA]        INT           NULL,
    [VACUNADO]               INT           NULL,
    [FECHAVACUNACION]        DATE          NULL,
    [PRESENTOCARNE]          INT           NULL,
    [NOMBREPROPIETARIO]      VARCHAR (200) NULL,
    [DIRECCIONPROPIETAR]     VARCHAR (200) NULL,
    [TELEFPROPIETARIO]       VARCHAR (15)  NULL,
    [ESTADOANIMAL]           INT           NULL,
    [UBICACION]              INT           NULL,
    [CLASIEXPOSICION]        INT           NULL,
    [SUEROANTIRRABICO]       INT           NULL,
    [FECHAAPLICACION]        DATE          NULL,
    [VACUNAANTIRRA]          INT           NULL,
    [NUMERODOSIS]            INT           NULL,
    [FECHAULTIMADOSIS]       DATE          NULL,
    [LAVADOHERIDAD]          BIT           NULL,
    [ORDENOSUERO]            BIT           NULL,
    [ORDENOAPLICACION]       BIT           NULL,
    [FIEBRE]                 BIT           NULL,
    [HIPOREXIA]              BIT           NULL,
    [CEFALEA]                BIT           NULL,
    [VOMITO]                 BIT           NULL,
    [PARESIAS]               BIT           NULL,
    [PARESTESIAS]            BIT           NULL,
    [DISFAGIA]               BIT           NULL,
    [ODINOFAGIA]             BIT           NULL,
    [ARREFLEXIA]             BIT           NULL,
    [ALUCINACIONES]          BIT           NULL,
    [EXPRETERROR]            BIT           NULL,
    [SIALORREA]              BIT           NULL,
    [AEROFOBIA]              BIT           NULL,
    [HIDROFOBIA]             BIT           NULL,
    [TRANQUILIDAD]           BIT           NULL,
    [DEPRESION]              BIT           NULL,
    [HIPEREXITABILIDAD]      BIT           NULL,
    [AGRESIVIDAD]            BIT           NULL,
    [ESPASMOSMUSCUL]         BIT           NULL,
    [CONVULSIONES]           BIT           NULL,
    [PARALISIS]              BIT           NULL,
    [CRISISRESPIRATORIA]     BIT           NULL,
    [COMA]                   BIT           NULL,
    [PAROCARDIORESPIRATO]    BIT           NULL,
    [PRUEDIAGNOSTICA]        INT           NULL,
    [RESULTADO]              INT           NULL,
    [IDENTIFICACIONVARIANTE] INT           NULL,
    [VARIANTEIDENTIFICADA]   INT           NULL,
    [OTRA]                   VARCHAR (200) NULL,
    [FECHARESULTADOLABOR]    DATE          NULL,
    [CODDIAGNO]              CHAR (4)      NULL,
    [INFORAMCIONLABORATORIO] INT           NULL,
    [AREAMORDEDURA]          INT           NULL,
    [VERSION]                VARCHAR (20)  NULL,
    [JSON]                   VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA307] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA307_JSON] CHECK (isjson([JSON])=(1))
);


GO
ALTER TABLE [dbo].[HCFICHA307] NOCHECK CONSTRAINT [CK_HCFICHA307_JSON];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Columnas adicionales en formato JSON. Versión antigua (nula). Versiones nuevas desde V01_2020-03-06: ESTADO_ANIMAL_CONSULTA (1=Vivo, 2=Muerto, 3=Desconocido). Tipo: VARCHAR(MAX), validación JSON.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nuevas columnas en formato JSON  -->  (versión antigua) = nulo   /    (versiones nuevas, desde ''''V01_2020-03-06'''' ) =    ESTADO_ANIMAL_CONSULTA = numero (Estado Animal al momento de la Consulta  --> 1= Vivo, 2= Muerto, 3= Desconocido) ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión de la ficha de notificación (VARCHAR 20). Nulo=primera versión. Formato: V01_YYYY-MM-DD. Controla cambios de estructura y reglas de negocio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Área anatómica de la mordedura o contacto. 1=Cubierta (ropa), 2=Descubierta (piel expuesta). INT, clasificación de exposición.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'AREAMORDEDURA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Area de la mordedura:   1=En área cubierta del cuerpo   2=En área descubierta del cuerpo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'AREAMORDEDURA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'AREAMORDEDURA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Disponibilidad de reporte laboratorio. 1=Sí hay resultado, 2=No hay resultado. INT, vinculado a FECHARESULTADOLABOR.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'INFORAMCIONLABORATORIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Infromación del laboratorio:   1=Sí hay información de laboratorio   2=No hay información de laboratorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'INFORAMCIONLABORATORIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'INFORAMCIONLABORATORIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico ICD-10/CIE-10 (CHAR 4). Identificación de patología confirmada, ej: rabbia, mordedura animal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de reporte del resultado laboratorio (DATE). Relacionado con PRUEDIAGNOSTICA, RESULTADO, INFORAMCIONLABORATORIO.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'FECHARESULTADOLABOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha resultado laboratorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'FECHARESULTADOLABOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'FECHARESULTADOLABOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo texto libre (VARCHAR 200) para especificación adicional de variante, especie, diagnóstico u otro dato no categorizado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'OTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ingreso de Otra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'OTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'OTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Variante del virus rábico identificada: 1=Uno, 2=Tres, 3=Cuatro, 4=Cinco, 5=Ocho, 6=Atípica, 7=Otra. INT, resultado diagnóstico específico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'VARIANTEIDENTIFICADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Variante identificada:  1=Uno   2=Tres   3=Cuatro   4=Cinco   5=Ocho   6=Atípica   7=Otra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'VARIANTEIDENTIFICADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'VARIANTEIDENTIFICADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Condición de identificación de variante. 1=Identificada (Sí), 2=No identificada (No), 3=En espera (Pendiente). INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'IDENTIFICACIONVARIANTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación variante:   1=Si   2=No   3=Pendiente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'IDENTIFICACIONVARIANTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'IDENTIFICACIONVARIANTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado prueba diagnóstica. 1=Positivo (rabbia confirmada), 2=Negativo, 3=Inadecuado (muestral), 4=Pendiente. INT. Obsoleto desde V01_2020-03-06.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado:   1=Positivo   2=Negativo   3=Inadecuado   --> (Item eliminado desde versión V01_2020-03-06)  4=Pendiente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'RESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prueba diagnóstica confirmatoria empleada. 1=IFD (Inmunofluorescencia), 2=Prueba biológica, 3=Histopatología, 4=Inmunohistoquímica, 5=Titulación anticuerpos antirrábicos. INT. Nuevo desde V01_2020-03-06.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'PRUEDIAGNOSTICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prueba diagnostica confrimatoria:    1= IFD    2= Prueba biológica    3= Histopatología    4= Inmunohistoquímica    5= Titulación anticuerpos antirrábicos -->  (Item nuevo desde versión V01_2020-03-06)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'PRUEDIAGNOSTICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'PRUEDIAGNOSTICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Paro respiratorio en estadío neurológico avanzado (TRUE=Sí, FALSE=No). BIT. Antes: paro cardiorrespiratorio. Desde V01_2020-03-06: paro respiratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'PAROCARDIORESPIRATO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'(Antes) Paro cardio respiratorio / (Ahora, V01_2020-03-06 ) Paro  respiratorio :   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'PAROCARDIORESPIRATO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'PAROCARDIORESPIRATO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presencia de coma/inconsciencia en paciente expuesto (TRUE=Sí, FALSE=No). BIT, manifestación neurológica grave de rabbia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'COMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Coma:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'COMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'COMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Crisis o insuficiencia respiratoria aguda (TRUE=Sí, FALSE=No). BIT, complicación neurológica de rabbia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'CRISISRESPIRATORIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Crisis respiratoria:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'CRISISRESPIRATORIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'CRISISRESPIRATORIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parálisis motora, debilidad muscular severa o pérdida de movimiento (TRUE=Sí, FALSE=No). BIT, signo neurológico rabbia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'PARALISIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Paralisis:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'PARALISIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'PARALISIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Convulsiones, espasmos tónico-clónicos generalizados (TRUE=Sí, FALSE=No). BIT, manifestación neurológica aguda.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'CONVULSIONES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Convulsiones:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'CONVULSIONES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'CONVULSIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Espasmos musculares, contracciones involuntarias. TRUE=Sí, FALSE=No. BIT. Antes: espasmos musculares. Desde V01_2020-03-06: espasmos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'ESPASMOSMUSCUL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'(Antes) Espasmos musculares / (Ahora, V01_2020-03-06 ) Espasmos :   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'ESPASMOSMUSCUL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'ESPASMOSMUSCUL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Comportamiento agresivo, irritabilidad extrema del paciente (TRUE=Sí, FALSE=No). BIT, signo conductual rabbia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'AGRESIVIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Agresividad:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'AGRESIVIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'AGRESIVIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hiperexcitabilidad, reactividad exagerada a estímulos (TRUE=Sí, FALSE=No). BIT, manifestación neuropsiquiátrica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'HIPEREXITABILIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hiperexitabilidad:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'HIPEREXITABILIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'HIPEREXITABILIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Depresión del estado de ánimo, apatía (TRUE=Sí, FALSE=No). BIT, cambio conductual rabbia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'DEPRESION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Depresión:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'DEPRESION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'DEPRESION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Alternancia tranquilidad-excitación o tranquilidad-agitación (TRUE=Sí, FALSE=No). BIT. Antes: tranquilidad alterna. Desde V01_2020-03-06: tranquilidad-excitación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'TRANQUILIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'(Antes) Tranquilidad alterna con exitación / (Ahora, V01_2020-03-06 )  Tranquilidad - excitación :   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'TRANQUILIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'TRANQUILIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hidrofobia, aversión/miedo al agua (TRUE=Sí, FALSE=No). BIT, signo patognomónico rabbia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'HIDROFOBIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hidrofobia:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'HIDROFOBIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'HIDROFOBIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Aerofobia, miedo a corrientes aire o aireación (TRUE=Sí, FALSE=No). BIT, signo neurológico rabbia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'AEROFOBIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Aerofobia:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'AEROFOBIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'AEROFOBIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sialorrea, salivación excesiva, hipersecreción salival (TRUE=Sí, FALSE=No). BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'SIALORREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sialorrea:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'SIALORREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'SIALORREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fascias de terror, expresión facial de terror/pánico (TRUE=Sí, FALSE=No). BIT. Antes: expresión de terror. Desde V01_2020-03-06: fascias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'EXPRETERROR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'(Antes) Expresión de terror / (Ahora, V01_2020-03-06 )  Fascies :    True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'EXPRETERROR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'EXPRETERROR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Alucinaciones visuales/auditivas o delirio persecutorio (TRUE=Sí, FALSE=No). BIT. Desde V01_2020-03-06: solo alucinaciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'ALUCINACIONES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'(Antes) Alucinaciones o delirio de persecución / (Ahora, V01_2020-03-06 )   Alucinaciones :   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'ALUCINACIONES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'ALUCINACIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Arreflexia o abolición de reflejos (TRUE=Sí, FALSE=No). BIT. Antes: arreflexia-hiporreflexia. Desde V01_2020-03-06: arreflexia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'ARREFLEXIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'(Antes) Arreflexia - hiporreflexia / (Ahora, V01_2020-03-06 )  Arreflexia :   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'ARREFLEXIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'ARREFLEXIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Odinofagia, dolor al tragar (TRUE=Sí, FALSE=No). BIT, síntoma sensorial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'ODINOFAGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Odinofagia:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'ODINOFAGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'ODINOFAGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Disfagia, dificultad para tragar saliva o alimentos (TRUE=Sí, FALSE=No). BIT, síntoma bulbar.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'DISFAGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Disfagia:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'DISFAGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'DISFAGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parestesias, hormigueo, adormecimiento sensorial anormal (TRUE=Sí, FALSE=No). BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'PARESTESIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Parestesias:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'PARESTESIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'PARESTESIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Paresias, debilidad muscular leve o parcial (TRUE=Sí, FALSE=No). BIT. Desde V01_2020-03-06: paresias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'PARESIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'(Antes) Paresias -  debilidad muscular / (Ahora, V01_2020-03-06 ) Paresias :   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'PARESIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'PARESIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Náuseas y vómito (TRUE=Sí, FALSE=No). BIT, síntoma gastrointestinal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'VOMITO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Vomito:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'VOMITO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'VOMITO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cefalea, dolor de cabeza (TRUE=Sí, FALSE=No). BIT, síntoma común exposición.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'CEFALEA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cefalea:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'CEFALEA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'CEFALEA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hiporexia, inapetencia, pérdida de apetito (TRUE=Sí, FALSE=No). BIT. Nota: descripción menciona ''''Hiperoxia'''' pero contexto es hiporexia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'HIPOREXIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'(Antes) Hiporexia - inapetencia  / (Ahora, V01_2020-03-06 ) Hiperoxia :  True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'HIPOREXIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'HIPOREXIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fiebre, elevación temperatura corporal (TRUE=Sí, FALSE=No). BIT, signo vital.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'FIEBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fiebre:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'FIEBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'FIEBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se ordenó/prescribió aplicación de vacuna antirrábica (TRUE=Sí, FALSE=No). BIT, indicador intervención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'ORDENOAPLICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ordeno Aplicación:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'ORDENOAPLICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'ORDENOAPLICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se ordenó/prescribió suero antirrabico (inmunoglobulina) (TRUE=Sí, FALSE=No). BIT, indicador profilaxis.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'ORDENOSUERO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ordeno suero:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'ORDENOSUERO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'ORDENOSUERO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lavado inmediato herida con agua y jabón (TRUE=Sí, FALSE=No). BIT, medida de descontaminación inicial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'LAVADOHERIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Lavado Agua Jabon:  True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'LAVADOHERIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'LAVADOHERIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha administración última dosis vacuna antirrábica (DATE). Fin esquema inmunización post-exposición.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'FECHAULTIMADOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de última dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'FECHAULTIMADOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'FECHAULTIMADOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número total dosis vacuna antirrábica recibidas (INT). Ej: 0-5 dosis, depende esquema (Essen/Milwaukee).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'NUMERODOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'NUMERODOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'NUMERODOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Animal vacunado contra rabbia: 1=Sí, 2=No, 3=Desconocido. INT. Desde V01_2020-03-06.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'VACUNAANTIRRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Vacuna antirrábica:  1=Si   2=No   3=No sabe', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'VACUNAANTIRRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'VACUNAANTIRRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha administración suero o vacuna antirrábica inicial (DATE). Primer intervención post-exposición.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'FECHAAPLICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la aplicación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'FECHAAPLICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'FECHAAPLICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Disponibilidad suero antirrabico (inmunoglobulina): 1=Sí, 2=No, 3=No sabe. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'SUEROANTIRRABICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Suero Antirrabico:  1=Si   2=No   3=No sabe', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'SUEROANTIRRABICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'SUEROANTIRRABICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación exposición (obsoleto desde V01_2020-03-06): 1=Animal Vivo, 2=Animal Muerto, 3=Desconocido. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'CLASIEXPOSICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clasificación exposición: ---> ( Campo obsoleto desde la versión V01_2020-03-06  )  1=Vivo   2=Muerto   3=Desconicido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'CLASIEXPOSICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'CLASIEXPOSICION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ubicación/disponibilidad animal agresor: 1=Observable (accesible), 2=Perdido (no localizado). INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'UBICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ubicacion:   1=Observable   2=Perdido   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'UBICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'UBICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado animal al momento agresión: 1=Con signos de rabbia, 2=Sin signos, 3=Desconocido. INT, evaluación clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'ESTADOANIMAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del animal al momento de la agresión o contacto:   1= Con signos de rabia   2= Sin signos de rabia   3= Desconocido ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'ESTADOANIMAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'ESTADOANIMAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Teléfono contacto propietario animal (VARCHAR 15). PII, máscara recomendada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'TELEFPROPIETARIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Telefono del propietario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'TELEFPROPIETARIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'TELEFPROPIETARIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección domicilio propietario animal (VARCHAR 200). PII, máscara recomendada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'DIRECCIONPROPIETAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Direccion del propietario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'DIRECCIONPROPIETAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'DIRECCIONPROPIETAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo propietario/dueño animal (VARCHAR 200). PII, máscara recomendada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'NOMBREPROPIETARIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del propietario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'NOMBREPROPIETARIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'NOMBREPROPIETARIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Animal presentó carnet/certificado vacunación: 1=Sí, 2=No. INT, verificación documentación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'PRESENTOCARNE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Presento carnet:   1=Si   2=No   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'PRESENTOCARNE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'PRESENTOCARNE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha última vacunación antirrábica animal (DATE). Validación esquema inmunización animal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'FECHAVACUNACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la vacunación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'FECHAVACUNACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'FECHAVACUNACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Animal vacunado contra rabbia: 1=Sí, 2=No, 3=Desconocido. INT. Desde V01_2020-03-06.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'VACUNADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'(Antes) Vacunado / (Ahora, V01_2020-03-06 ) Animal vacunado:   1=Si   2=No   3=Desconocido ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'VACUNADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'VACUNADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especie animal agresor: 1=Perro, 2=Gato, 3=Bovino-Bufalino, 4=Equidos, 5=Porcino, 6=Murciélago, 7=Zorro, 8=Primate, 9=Humano, 11=Silvestres otros, 12=Ovino-Caprino, 13=Roedores grandes, 14=Roedores pequeños. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'ESPECIEAGRESORA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especie agresora  1= Perro   2= Gato   3= (Antes) Bovino  / (Ahora, V01_2020-03-06) Bovino - Bufalino  4= (Antes) Equino /  (Ahora, V01_2020-03-06) Equidos  5= Porcino (cerdo)   6= Murcielago   7= Zorro   8= Mico   9= Humano   10= Otros domésticos --> (Item eliminado desde versión V01_2020-03-06)  11= Otros silvestres   12= Ovino - Caprino   13= Grandes roedores   14= Pequeños roedores  --> (Item eliminado desde versión V01_2020-03-06)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'ESPECIEAGRESORA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'ESPECIEAGRESORA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha agresión, mordedura o contacto riesgo (DATE). Evento inicial. Clave para cálculo profilaxis.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'FECHAAGRESION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la agresión o contacto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'FECHAAGRESION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'FECHAAGRESION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Localización anatómica lesión: Genitales externos (TRUE=Sí, FALSE=No). BIT, área alta exposición.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'GENITALESEXTERN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Localización anatómica  Genitales externos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'GENITALESEXTERN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'GENITALESEXTERN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Localización anatómica lesión: Pies, dedos (TRUE=Sí, FALSE=No). BIT, área exposición.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'PIES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Localización anatómica  Pies, dedos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'PIES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'PIES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Localización anatómica lesión: Miembros inferiores (TRUE=Sí, FALSE=No). BIT, extremidades inferiores.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'MIEMBROSINFERI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Localización anatómica  Miembros inferiores', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'MIEMBROSINFERI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'MIEMBROSINFERI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Localización anatómica lesión: Miembros superiores, brazos (TRUE=Sí, FALSE=No). BIT, extremidades superiores.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'MIEMBROSSUPERI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Localización anatómica  Miembros superiores', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'MIEMBROSSUPERI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'MIEMBROSSUPERI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Localización anatómica lesión: Tronco, pecho, abdomen, espalda (TRUE=Sí, FALSE=No). BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'TRONCO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Localización anatómica  Tronco', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'TRONCO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'TRONCO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Localización anatómica lesión: Manos, dedos (TRUE=Sí, FALSE=No). BIT, zona altamente expuesta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'MANOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Localización anatómica  Manos, dedos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'MANOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'MANOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Localización anatómica lesión: Cabeza, cara, cuello (TRUE=Sí, FALSE=No). BIT, área muy grave.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'CABEZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Localización anatómica  Cabeza, cara, cuello', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'CABEZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'CABEZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Profundidad lesión: 1=Superficial (epidérmica), 2=Profunda (dermis/músculo). INT, clasificación herida.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'PROFUNDIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profundidad  1=superficial  2=profunda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'PROFUNDIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'PROFUNDIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo lesión: 1=Única (una herida), 2=Múltiples (varias heridas). INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'TIPOLESION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de lesión  1= unica  2= multiple', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'TIPOLESION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'TIPOLESION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agresión provocada por la persona expuesta (TRUE=Sí, FALSE=No). BIT, contexto circunstancia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'AGRESIONPROVOCADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Agresión provocada  True = si   false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'AGRESIONPROVOCADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'AGRESIONPROVOCADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo agresión/contacto: 1=Mordedura, 2=Arañazo/rasguño, 3=Contacto mucosa/piel lesionada con saliva infectada, 4=Contacto mucosa/piel con material biológico infectado, 5=Inhalación virus (aerosoles), 6=Trasplante órganos/tejidos infectados. INT. Versión V01_2020-03-06.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'TIPOAGRESION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de agresión o contacto  1=Mordedura  2=Arañazo o rasguño  3= (Antes) Contacto de mucosa o piel lesionada con saliva del agresor / (Ahora, version V01_2020-03-06 ) Contacto de mucosa o piel lesionada con saliva o baba infectada con virus rábico   4=Contacto de mucosa o piel lesionada, con tejido nervioso, material biológico o secreciones infectadas con virus rábico  5=Inhalación en ambientes cargados o virus rábico (aerosoles)  6=Trasplante de órganos o tejidos infectados con virus rábico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'TIPOAGRESION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'TIPOAGRESION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador ficha notificación RIPS (INT FK). Referencia tabla padre notificación epidemiológica rabbia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental registro (INT IDENTITY, PK). Clave primaria tabla HCFICHA307.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha clínica de notificación de accidentes por mordedura o agresión animal (exposición rábica), incluyendo datos del tipo de agresión, lesión, zona del cuerpo afectada, información del animal agresor y su propietario, estado vacunal antirrábico del paciente y el animal, tratamiento aplicado (suero y vacuna antirrábica), síntomas presentados y resultados de pruebas diagnósticas de laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA307';
